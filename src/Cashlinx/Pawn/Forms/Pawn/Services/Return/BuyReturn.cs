using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Services.Return
{
    public partial class BuyReturn : CustomBaseForm
    {
        public NavBox NavControlBox;
        public BuyReturn()
        {
            InitializeComponent();
            NavControlBox = new NavBox();
        }

        private void BuyReturn_Load(object sender, EventArgs e)
        {
            NavControlBox.Owner = this;
            labelStoreNo.Text = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            if (GlobalDataAccessor.Instance.DesktopSession.BuyReturnIcn)
            {
                radioButtonBuyNo.Enabled = false;
                customTextBoxBuyNo.Enabled = false;
                this.SelectNextControl(customTextBoxBuyNo, true, true, true, true);
            }
            else
            {
                radioButtonBuyNo.Checked = true;
            }
        }

        private void customTextBoxBuyNo_Click(object sender, EventArgs e)
        {
            this.radioButtonBuyNo.Checked = true;
        }

        private void customTextBoxICN_Click(object sender, EventArgs e)
        {
            this.radioButtonICN.Checked = true;
        }

        private void customButtonContinue_Click(object sender, EventArgs e)
        {
            labelErrorMessage.Visible = false;
            PurchaseVO purchaseObj;
            CustomerVO customerObj;
            string errorCode;
            string errorText;
            bool retValue;
            bool vendorReturn = false;

            string tenderType;
            if (radioButtonBuyNo.Checked)
            {
                CashlinxDesktopSession.Instance.Purchases = new List<PurchaseVO>();
                if (customTextBoxBuyNo.Text.Length > 0)
                {
                    if (CashlinxDesktopSession.Instance.HistorySession.Trigger.Equals("returnvendorbuy", StringComparison.OrdinalIgnoreCase))
                    {
                        retValue = PurchaseProcedures.GetPurchaseData(Utilities.GetIntegerValue(CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber, 0),
                            Utilities.GetIntegerValue(customTextBoxBuyNo.Text, 0), "2", StateStatus.BLNK, ProductStatus.PFI.ToString(), false, out purchaseObj, out customerObj, out tenderType, out errorCode, out errorText);
                        vendorReturn = true;
                        if (retValue)
                            purchaseObj.PurchaseTenderType = tenderType.ToUpper() == "CASH" ? PurchaseTenderTypes.CASHOUT.ToString() : PurchaseTenderTypes.BILLTOAP.ToString();
                    }
                    else
                        retValue = PurchaseProcedures.GetPurchaseData(Utilities.GetIntegerValue(CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber, 0),
                            Utilities.GetIntegerValue(customTextBoxBuyNo.Text, 0), "2", StateStatus.BLNK, ProductStatus.PUR.ToString(), true, out purchaseObj, out customerObj, out tenderType, out errorCode, out errorText);

                    if (retValue && purchaseObj != null)
                    {

                        CashlinxDesktopSession.Instance.Purchases.Add(purchaseObj);
                        if (vendorReturn)
                        {
                            CashlinxDesktopSession.Instance.ActiveVendor = VendorProcedures.getVendorDataByID(purchaseObj.EntityNumber, CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber);
                        }
                        else
                        {
                            CashlinxDesktopSession.Instance.ActiveCustomer = customerObj;
                        }
                        CashlinxDesktopSession.Instance.BuyReturnIcn = false;
                        NavControlBox.IsCustom = true;
                        NavControlBox.CustomDetail = "SHOWITEMS";
                        NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

                    }
                    else
                    {
                        labelErrorMessage.Text = "The buy number was not found. ";
                        labelErrorMessage.Visible = true;
                        return;
                    }
                }
                else
                    return;
            }
            else
            {
                if (customTextBoxICN.Text.Length > 0)
                {
                    bool isProperIcn = customTextBoxICN.Text.Contains(".") && !customTextBoxICN.Text.EndsWith(".");
                    if (!isProperIcn)
                    {
                        MessageBox.Show("Invalid ICN entered");
                        return;
                    }
                    if (CashlinxDesktopSession.Instance.HistorySession.Trigger.Equals("returnvendorbuy", StringComparison.OrdinalIgnoreCase))
                    {
                        //BZ # 301 - add new param store number to work with icn short code
                        retValue = PurchaseProcedures.GetPurchaseDataFromIcn(GlobalDataAccessor.Instance.OracleDA, customTextBoxICN.Text.ToString(), CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber,
                            false, out purchaseObj, out customerObj, out tenderType, out errorCode, out errorText);
                        vendorReturn = true;
                        if (retValue)
                            purchaseObj.PurchaseTenderType = tenderType.ToUpper() == "CASH" ? PurchaseTenderTypes.CASHOUT.ToString() : PurchaseTenderTypes.BILLTOAP.ToString();
                    }
                    else
                    {
                        //BZ # 301 - add new param store number to work with icn short code
                        retValue = PurchaseProcedures.GetPurchaseDataFromIcn(GlobalDataAccessor.Instance.OracleDA, customTextBoxICN.Text.ToString(), CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber,
                            true, out purchaseObj, out customerObj, out tenderType, out errorCode, out errorText);
                    }

                    if (retValue && purchaseObj != null)
                    {
                        if (!CashlinxDesktopSession.Instance.BuyReturnIcn)
                            CashlinxDesktopSession.Instance.Purchases.Add(purchaseObj);
                        else
                        {
                            //Check if the new icn belongs to the purchase that was already selected
                            if (CashlinxDesktopSession.Instance.ActivePurchase.TicketNumber != purchaseObj.TicketNumber)
                            {
                                DialogResult dgr = MessageBox.Show("The ticket number for this item does not match items previously found. Please process each ticket number separately. Do you want to search for another item?", "Buy Return Error", MessageBoxButtons.YesNo);
                                if (dgr == DialogResult.Yes)
                                    return;
                                else
                                    NavControlBox.Action = NavBox.NavAction.CANCEL;
                            }
                            int idx = CashlinxDesktopSession.Instance.ActivePurchase.Items.FindIndex(iItem => iItem.Icn == purchaseObj.Items[0].Icn);
                            if (idx < 0)
                                CashlinxDesktopSession.Instance.ActivePurchase.Items.Add(purchaseObj.Items[0]);
                            else
                            {
                                MessageBox.Show("The ICN is already selected. Please enter another ICN number");
                                return;
                            }
                        }
                        if (vendorReturn)
                        {
                            CashlinxDesktopSession.Instance.ActiveVendor = VendorProcedures.getVendorDataByID(purchaseObj.EntityNumber, CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber);
                        }
                        else
                        {
                            CashlinxDesktopSession.Instance.ActiveCustomer = customerObj;
                        }
                        if (!CashlinxDesktopSession.Instance.BuyReturnIcn)
                            CashlinxDesktopSession.Instance.BuyReturnIcn = true;
                        NavControlBox.IsCustom = true;
                        NavControlBox.CustomDetail = "SHOWITEMS";
                        NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                    }
                    else
                    {
                        labelErrorMessage.Text = "The ICN number was not found.";
                        labelErrorMessage.Visible = true;
                        return;
                    }
                }
                else
                    return;

            }
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();
            GlobalDataAccessor.Instance.DesktopSession.Purchases = null;
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

 
    }
}
