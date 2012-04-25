using System;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Services.Void
{
    public partial class VoidPurchaseReturn : CustomBaseForm
    {
        private PurchaseVO currentPurchase;
        private long maxVoidDays;
        private bool voidReturnFlow;



        private const string INVALIDVOIDMESSAGE = "The number entered is not eligible for void.";
        private const string NOTCUSTOMERPURCHASE = "This buy number is a vendor purchase number. Cannot void";
        private const string RETURNEDPURCHASE = "This purchase cannot be voided since it has returned items";


        public VoidPurchaseReturn()
        {
            InitializeComponent();
        }

        private void VoidPurchaseReturn_Load(object sender, EventArgs e)
        {
            labelMessage.Text = "";

            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VOIDBUYRETURN, StringComparison.OrdinalIgnoreCase))
                voidReturnFlow = true;

            if (voidReturnFlow)
            {
                labelHeading.Text = "Void Return";
                labelTransactionNoHeading.Text = "Return Number";

            }

            maxVoidDays = 0L;
            if (!new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMaxVoidDays(GlobalDataAccessor.Instance.CurrentSiteId, out maxVoidDays))
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                        "Cannot retrieve maximum void days. Defaulting to {0}", maxVoidDays);
                }
            }
            currentPurchase = GlobalDataAccessor.Instance.DesktopSession.ActivePurchase;
            if (currentPurchase != null)
            {
                labelDate.Text = currentPurchase.MadeTime.ToString();
                labelPurchaseNo.Text = currentPurchase.TicketNumber.ToString();
                labelUserID.Text = currentPurchase.CreatedBy.ToString();
                labelTotal.Text = currentPurchase.Amount.ToString("c");
                if (currentPurchase.DateMade.AddDays(maxVoidDays) < ShopDateTime.Instance.ShopDate)
                {
                    labelMessage.Text = INVALIDVOIDMESSAGE;
                    labelMessage.Visible = true;
                    customButtonVoid.Enabled = false;
                }
                if (currentPurchase.CustomerNumber == null ||
                    currentPurchase.EntityType == "V")
                {
                    labelMessage.Text = NOTCUSTOMERPURCHASE;
                    labelMessage.Visible = true;
                    customButtonVoid.Enabled = false;
                }

                if (!voidReturnFlow)
                {
                    var itemData = (from item in currentPurchase.Items
                                    where item.ItemStatus == ProductStatus.RET
                                    select item).FirstOrDefault();
                    if (itemData != null)
                    {
                        labelMessage.Text = RETURNEDPURCHASE;
                        labelMessage.Visible = true;
                        customButtonVoid.Enabled = false;
                    }
                }

                if (currentPurchase.Items != null && currentPurchase.Items.Count > 0)
                {
                    BindingSource _bindingSource1 = new BindingSource { DataSource = currentPurchase.Items };
                    dataGridViewMdse.AutoGenerateColumns = false;
                    this.dataGridViewMdse.DataSource = _bindingSource1;
                    this.dataGridViewMdse.Columns[0].DataPropertyName = "ticketdescription";
                    this.dataGridViewMdse.Columns[1].DataPropertyName = "itemamount";
                    this.dataGridViewMdse.Columns[1].DefaultCellStyle.Format = "c";


                    this.dataGridViewMdse.AutoGenerateColumns = false;
                }
                else
                {
                    MessageBox.Show("Error in void transaction processing");
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "No items found for purchase " + currentPurchase.TicketNumber + " to void.");
                    Close();
                }


            }
        }

        private void customButtonVoid_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPurchase.Receipts != null)
                {
                    string rcptId;
                    if (voidReturnFlow)
                    {
                        rcptId = (from receipt in currentPurchase.Receipts
                                  where (receipt.Event == ReceiptEventTypes.RET.ToString()
                                  && receipt.RefNumber == currentPurchase.TicketNumber.ToString())
                                  select receipt).First().ReceiptDetailNumber;
                    }
                    else
                    {
                        rcptId = (from receipt in currentPurchase.Receipts
                                  where (receipt.Event == ReceiptEventTypes.PUR.ToString()
                                  && receipt.RefNumber == currentPurchase.TicketNumber.ToString())
                                  select receipt).First().ReceiptDetailNumber;
                    }

                    var lvd = new VoidLoanForm.LoanVoidDetails();
                    lvd.TickNum = labelPurchaseNo.Text;
                    lvd.StoreNum = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                    lvd.OpRef = labelPurchaseNo.Text;
                    lvd.OpCode = voidReturnFlow ? "Return" : "Purchase";
                    lvd.OpCd = voidReturnFlow ? ReceiptEventTypes.VRET.ToString() : ReceiptEventTypes.VPR.ToString();
                    lvd.Amount = Utilities.GetDecimalValue(currentPurchase.Amount, 0.0M);
                    lvd.HoldType = "";
                    lvd.RecId = Utilities.GetLongValue(rcptId);
                    lvd.PfiEligDate = Utilities.GetDateTimeValue(currentPurchase.PfiEligible, DateTime.MaxValue);
                    lvd.CreatedBy = Utilities.GetStringValue(currentPurchase.CreatedBy, string.Empty);

                    string errorCode;
                    string errorText;
                    bool retValue = VoidProcedures.PerformVoid(lvd, out errorCode, out errorText);
                    if (retValue)
                    {
                        MessageBox.Show(voidReturnFlow
                                                ? "Void purchase return completed successfully"
                                                : "Void purchase completed successfully");
                        this.Close();
                    }
                    else
                    {
                        DialogResult dgr;
                        dgr = MessageBox.Show("Void transaction failed. Do you want to retry?", "Void Error", MessageBoxButtons.OKCancel);
                        if (dgr == DialogResult.OK)
                            return;
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Error in  void transaction processing");
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "No receipts found for purchase " + currentPurchase.TicketNumber + " to void.");
                    Close();
                }
            }
            catch (Exception ex)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error when voiding buy return " + ex.Message);
            }


        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
