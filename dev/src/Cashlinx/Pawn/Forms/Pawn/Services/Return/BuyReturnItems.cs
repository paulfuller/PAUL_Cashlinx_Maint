using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Pawn.Services.Pickup;
using Pawn.Logic;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Services.Return
{
    public partial class BuyReturnItems : CustomBaseForm
    {
        public NavBox NavControlBox;
        public bool Back;
        private PurchaseVO currentPurchase;
        private CustomerVO currentCustomer;
        private const string INELEIGIBLEFORRETURNMESSAGE = "One or more of the items on this ticket are not eligible for Return";
        private const string NOTCUSTOMERPURCHASE = "This number is a vendor buy number. Cannot be retuned";
        private List<string> selectedICNList;
        private List<string> restrictedICNList;
        private bool custPurchaseReturn;

        public BuyReturnItems()
        {
            InitializeComponent();
            NavControlBox = new NavBox();
        }

        private void BuyReturnItems_Load(object sender, EventArgs e)
        {
            Back = false;
            NavControlBox.Owner = this;
            labelErrorMessage.Visible = false;
            currentPurchase = CashlinxDesktopSession.Instance.ActivePurchase;
            currentCustomer = CashlinxDesktopSession.Instance.ActiveCustomer;
            restrictedICNList = new List<string>();
            selectedICNList = new List<string>();
            if (currentPurchase != null)
            {
                labelStoreNo.Text = CashlinxDesktopSession.Instance.BuyReturnIcn ? "" : currentPurchase.StoreNumber.ToString();
                labelBuyNumber.Text = currentPurchase.TicketNumber.ToString();
                if (!string.IsNullOrEmpty(currentPurchase.HoldDesc))
                    labelStatus.Text = currentPurchase.LoanStatus.ToString() + "-" + currentPurchase.HoldDesc;
                else
                    labelStatus.Text = currentPurchase.LoanStatus.ToString();
                if (!string.IsNullOrEmpty(currentPurchase.HoldDesc))
                {
                    labelErrorMessage.Text = INELEIGIBLEFORRETURNMESSAGE;
                    labelErrorMessage.Visible = true;
                    customTextBoxRetReason.Enabled = false;
                    customButtonContinue.Enabled = false;
                }
                //Check if all the items are eligible for return
                if (CashlinxDesktopSession.Instance.HistorySession.Trigger.Equals("returncustomerbuy", StringComparison.OrdinalIgnoreCase))
                {
                    Item otherThanBuyStatus = (from item in currentPurchase.Items
                                               where item.ItemStatus != ProductStatus.PUR
                                               select item).FirstOrDefault();
                    if (otherThanBuyStatus != null && otherThanBuyStatus.mDocNumber != 0)
                    {
                        labelErrorMessage.Text = INELEIGIBLEFORRETURNMESSAGE;
                        labelErrorMessage.Visible = true;
                    }
                }
                else
                {
                    Item otherThanPFIStatus = (from item in currentPurchase.Items
                                               where item.ItemStatus != ProductStatus.PFI
                                               select item).FirstOrDefault();
                    if (otherThanPFIStatus != null && otherThanPFIStatus.mDocNumber != 0)
                    {
                        labelErrorMessage.Text = INELEIGIBLEFORRETURNMESSAGE;
                        labelErrorMessage.Visible = true;
                    }

                }
                if (CashlinxDesktopSession.Instance.HistorySession.Trigger.Equals("returncustomerbuy", StringComparison.OrdinalIgnoreCase))
                {
                    custPurchaseReturn = true;
                    if (currentPurchase.CustomerNumber == null ||
                        currentPurchase.EntityType == "V")
                    {
                        labelErrorMessage.Text = NOTCUSTOMERPURCHASE;
                        labelErrorMessage.Visible = true;
                        customButtonContinue.Enabled = false;
                    }
                    else
                        labelCustName.Text = currentCustomer.CustomerName;
                }
                else
                    labelCustName.Text = GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Name;
                foreach (Item item in currentPurchase.Items)
                {
                    string tranStatus = item.ItemStatus.ToString();
                    string holdDesc = item.HoldDesc;
                    if (!Commons.CanBeReturned(tranStatus, holdDesc))
                    {
                        restrictedICNList.Add(item.Icn);
                    }
                }

                if (currentPurchase.Items != null && currentPurchase.Items.Count > 0)
                {
                    BindingSource _bindingSource1 = new BindingSource { DataSource = currentPurchase.Items };
                    dataGridViewItems.AutoGenerateColumns = false;
                    this.dataGridViewItems.DataSource = _bindingSource1;
                    this.dataGridViewItems.Columns[0].DataPropertyName = "icn";
                    this.dataGridViewItems.Columns[1].DataPropertyName = "ticketdescription";
                    this.dataGridViewItems.Columns[2].DataPropertyName = "itemstatus";
                    this.dataGridViewItems.Columns[3].DataPropertyName = "itemamount";
                    this.dataGridViewItems.Columns[4].DataPropertyName = "holddesc";
                    this.dataGridViewItems.Columns[3].DefaultCellStyle.Format = "c";



                    this.dataGridViewItems.AutoGenerateColumns = false;
                }
                else
                {
                    MessageBox.Show("Error in processing returns");
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "No items found for purchase " + currentPurchase.TicketNumber + " to return.");
                    NavControlBox.Action = NavBox.NavAction.CANCEL;
                }
                if (currentPurchase.Items != null)
                {
                    if (restrictedICNList.Count > 0 && restrictedICNList.Count == currentPurchase.Items.Count)
                        customButtonContinue.Enabled = false;
                }
                foreach (DataGridViewRow dgvr in dataGridViewItems.Rows)
                {
                    if (restrictedICNList.Count > 0)
                    {
                        DataGridViewRow dgvr1 = dgvr;
                        var icn = (from icnItem in restrictedICNList
                                   where
                                       icnItem == dgvr1.Cells[0].Value.ToString()
                                   select icnItem).FirstOrDefault();
                        if (icn != null)
                        {
                            dgvr1.ReadOnly = true;
                            dgvr1.DefaultCellStyle.BackColor = Color.Gray;
                        }
                    }

                }

                if (CashlinxDesktopSession.Instance.BuyReturnIcn)
                {
                    customButtonAddItem.Enabled = true;
                }

            }
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void customButtonBack_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        private void customButtonContinue_Click(object sender, EventArgs e)
        {
            CashlinxDesktopSession.Instance.ActivePurchase.ReturnReason = customTextBoxRetReason.Text.ToString();
            //Get the list of items that were selected by the user for return
            List<Item> itemsToReturn = (from item in CashlinxDesktopSession.Instance.ActivePurchase.Items
                                        let iItem = (from icn in selectedICNList
                                                     where icn == item.Icn
                                                     select icn).FirstOrDefault()
                                        where iItem != null
                                        select item).ToList();
            if (itemsToReturn.Count > 0)
            {
                //Remove all the items that are part of the purchase and add
                //the items that were selected by the user for processing
                GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items = null;
                GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items = itemsToReturn;
                var amtReturned = (from item in itemsToReturn
                                   select item.ItemAmount).Sum();

                DateTime currentDate = ShopDateTime.Instance.ShopDate;
                string strStoreState = GlobalDataAccessor.Instance.CurrentSiteId.State;
                //Check if any of the items being returned is a gun
                var gunItem = GetGunItem();
                if (gunItem != null)
                {
                    if (custPurchaseReturn)
                    {
                        /*if (currentCustomer.HasValidConcealedWeaponsPermitInState(strStoreState, currentDate))
                        {
                            if (CustomerProcedures.IsBackgroundCheckRequired())
                            {
                                FirearmsBackgroundCheck backgroundcheckFrm = new FirearmsBackgroundCheck();
                                backgroundcheckFrm.ShowDialog(this);

                            }
                            else //If the background check is not needed
                                CashlinxDesktopSession.Instance.BackgroundCheckCompleted = true;
                        }
                        //else if they do not have CWP or not a CWP in the store state or expired 
                        //then show the background check form
                        else
                        {
                            FirearmsBackgroundCheck backgroundcheckFrm = new FirearmsBackgroundCheck();
                            backgroundcheckFrm.ShowDialog(this);
                        }*/

                        NavControlBox.IsCustom = true;
                        NavControlBox.CustomDetail = "ManagePawnAppplication";
                        NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

                        return;
                    }
                    else
                    {
                        //If this is not a customer purchase return no need to ask for background check form
                        //FirearmsBackgroundCheck backgroundcheckFrm = new FirearmsBackgroundCheck();
                        //backgroundcheckFrm.ShowDialog(this);
                        GlobalDataAccessor.Instance.DesktopSession.BackgroundCheckCompleted = true;

                        string ffl = GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Ffl;

                        if ((string.IsNullOrWhiteSpace(ffl) || ffl.Length != 15) && GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items.Any(i => i.IsGun) && !CashlinxDesktopSession.Instance.VendorValidated)
                        {
                            GlobalDataAccessor.Instance.DesktopSession.VenderFFLRequired = true;
                            NavControlBox.CustomDetail = "ShowVendor";
                            NavControlBox.IsCustom = true;
                            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                            return;
                        }

                        if (GlobalDataAccessor.Instance.DesktopSession.BackgroundCheckCompleted)
                        {
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                            GlobalDataAccessor.Instance.DesktopSession.showProcessTender(ProcessTenderProcedures.ProcessTenderMode.RETURNBUY);
                        }
                        else
                        {
                            MessageBox.Show(Commons.GetMessageString("PurchaseReturnFirearmMessage"));
                            return;
                        }
                    }
                }
                else
                //not a gun item return proceed toprocess tender
                {
                    if (!custPurchaseReturn)
                    {
                        this.Hide();
                        CashlinxDesktopSession.Instance.TotalServiceAmount = amtReturned;
                        /*DisbursementDetails tenderTypeForm = new DisbursementDetails();
                        tenderTypeForm.ShowDialog();*/
                        //SR 07/28/2011 For a vendor return no need to show disbursement form. It should be the same tender as
                        //what was used for buy
                        if (CashlinxDesktopSession.Instance.ActivePurchase.PurchaseTenderType == PurchaseTenderTypes.BILLTOAP.ToString() ||
                            CashlinxDesktopSession.Instance.ActivePurchase.PurchaseTenderType == PurchaseTenderTypes.CASHOUT.ToString())
                        {
                            CashlinxDesktopSession.Instance.HistorySession.Desktop();
                            CashlinxDesktopSession.Instance.showProcessTender(ProcessTenderProcedures.ProcessTenderMode.RETURNBUY);
                        }
                        else
                        {
                            MessageBox.Show("No tender type selected. Cannot proceed");
                            this.Show();
                            return;
                        }

                    }
                    else
                    {
                        CashlinxDesktopSession.Instance.HistorySession.Desktop();
                        CashlinxDesktopSession.Instance.showProcessTender(ProcessTenderProcedures.ProcessTenderMode.RETURNBUY);
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Items to return and continue");
                return;
            }


        }

        private static Item GetGunItem()
        {
            var gunItem = (from item in CashlinxDesktopSession.Instance.ActivePurchase.Items
                           where item.IsGun
                           select item).FirstOrDefault();
            return gunItem;
        }

        private void dataGridViewItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                return;
            }

            string selectedICN = dataGridViewItems.Rows[e.RowIndex].Cells[0].Value.ToString();
            int idx = selectedICNList.FindIndex(mIcn => mIcn == selectedICN);
            if (idx >= 0)
            {
                selectedICNList.RemoveAt(idx);
                dataGridViewItems.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
            else
            {
                int index = restrictedICNList.FindIndex(iIcn => iIcn == selectedICN);
                if (index < 0)
                {
                    selectedICNList.Add(selectedICN);
                    dataGridViewItems.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Blue;
                }
            }

            customButtonContinue.Enabled = selectedICNList.Count > 0;


        }

        private void customButtonAddItem_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }
    }
}
