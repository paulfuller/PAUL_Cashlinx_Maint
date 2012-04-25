using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Components.EventArgs;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Services.Void
{
    public partial class VoidTransactionDetail : CustomBaseForm
    {
        private PurchaseVO currentPurchase;
        private SaleVO currentSale;
        private long maxVoidDays;
        private bool voidReturnFlow;
        private bool voidBuyFlow;
        private bool voidSaleFlow;
        private bool voidSaleRefundFlow;
        private bool voidLayawayFlow;
        private bool voidVendorBuy;
        
        private const string INVALIDVOIDMESSAGE = "The number entered is not eligible for void.";
        private const string INVALIDVOIDSALEMESSAGE = "The time eligible for void has expired.  Please contact Shop System Support";
        private const string INVALIDCONVERSIONVOIDSALEMESSAGE = "This transaction occurred prior to conversion and is not eligible for void. Consider using a refund transaction.";
        private const string NOTCUSTOMERPURCHASE = "This buy number is a vendor purchase number. Cannot void";
        private const string NOTVENDORPURCHASE = "This buy number is a customer purchase number. Cannot void";
        private const string RETURNEDPURCHASE = "This purchase cannot be voided since it has returned items";
        private const string RETURNEDSALE = "This sale cannot be voided since it has returned items";
        private const string RTCITEMS = "This purchase cannot be voided since some items have been released to claimant or police seized";
        private const string RETAILITEMS = "This purchase has items that are sold or on layaway. Cannot void";
        
        public VoidTransactionDetail()
        {
            InitializeComponent();
        }

        private void VoidPurchaseReturn_Load(object sender, EventArgs e)
        {
            labelMessage.Text = "";

            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VOIDBUYRETURN, StringComparison.OrdinalIgnoreCase))
                voidReturnFlow = true;
            else if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VOIDSALE, StringComparison.Ordinal))
                voidSaleFlow = true;
            else if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VOIDBUY, StringComparison.OrdinalIgnoreCase))
                voidBuyFlow = true;
            else if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VOIDREFUND, StringComparison.OrdinalIgnoreCase))
                voidSaleRefundFlow = true;
            else if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VOIDLAYAWAY, StringComparison.OrdinalIgnoreCase))
                voidLayawayFlow = true;


            if (voidReturnFlow)
            {
                labelHeading.Text = "Void Return";
                labelTransactionNoHeading.Text = "Return Number";
            }
            else if (voidBuyFlow)
            {
                labelHeading.Text = "Void Buy";
                labelTransactionNoHeading.Text = "Buy Number";
                //BZ # 619
                comboBoxReason.Items.Clear();
                comboBoxReason.Items.Add("Wrong Payment Method");
                comboBoxReason.Items.Add("PFI'd In Error");
                comboBoxReason.Items.Add("Wrong Customer");
                comboBoxReason.Items.Add("Incorrect Transaction Detail");
                comboBoxReason.Items.Add("Test Transaction");
                comboBoxReason.Items.Add("System Issue");
            }
            else if (voidSaleFlow)
            {
                labelHeading.Text = "Void Sale";
                labelTransactionNoHeading.Text = "MSR #";
                this.dataGridViewMdse.Columns[1].HeaderText = "Amount";
                comboBoxReason.Items.Clear();
                comboBoxReason.Items.Add("Wrong Payment Method");
                comboBoxReason.Items.Add("Wrong Customer");
                comboBoxReason.Items.Add("Wrong ICN");
                comboBoxReason.Items.Add("Customer Changed Mind");
                comboBoxReason.Items.Add("Test Transaction");
                comboBoxReason.Items.Add("System Issue");
            }
            else if (voidSaleRefundFlow)
            {
                labelHeading.Text = "Void Sale Refund";
                labelTransactionNoHeading.Text = "MSR Refund #";
            }
            else if (voidLayawayFlow)
            {
                labelHeading.Text = "Void Layaway";
                labelTransactionNoHeading.Text = "Layaway #";
            }

            maxVoidDays = 0L;
            if (!new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMaxVoidDays(GlobalDataAccessor.Instance.CurrentSiteId,
                out maxVoidDays))
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                        "Cannot retrieve maximum void days. Defaulting to {0}", maxVoidDays);
                }
            }

            currentPurchase = GlobalDataAccessor.Instance.DesktopSession.ActivePurchase;
            currentSale = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail;
            if (currentPurchase != null)
            {
                if (currentPurchase.EntityType == "V")
                    voidVendorBuy = true;
                labelDate.Text = currentPurchase.MadeTime.ToString();
                labelTransactionNo.Text = currentPurchase.TicketNumber.ToString();
                labelUserID.Text = currentPurchase.CreatedBy.ToString();
                labelTotal.Text = currentPurchase.Amount.ToString("c");
                if (currentPurchase.DateMade.AddDays(maxVoidDays) < ShopDateTime.Instance.ShopDate)
                {
                    labelMessage.Text = INVALIDVOIDMESSAGE;
                    labelMessage.Visible = true;
                    customButtonVoid.Enabled = false;
                }

               /* if (voidBuyFlow && (currentPurchase.CustomerNumber == null ||
                    currentPurchase.EntityType == "V"))
                {
                    labelMessage.Text = NOTCUSTOMERPURCHASE;
                    labelMessage.Visible = true;
                    customButtonVoid.Enabled = false;
                }
                if (voidVendorBuy && currentPurchase.EntityType != "V")
                {
                    labelMessage.Text = NOTVENDORPURCHASE;
                    labelMessage.Visible = true;
                    customButtonVoid.Enabled = false;

                }*/
                //if we are voiding vendor buy make sure that the items are not sold or on layaway
                if (voidBuyFlow)
                {

                    var itemData = (from item in currentPurchase.Items
                                    where item.ItemStatus == ProductStatus.SOLD ||
                                    item.ItemStatus == ProductStatus.LAY
                                    select item).FirstOrDefault();
                    if (itemData != null)
                    {
                        labelMessage.Text = RETAILITEMS;
                        labelMessage.Visible = true;
                        customButtonVoid.Enabled = false;
                    }

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
                    var itemRTCData = (from item in currentPurchase.Items
                                    where item.ItemStatus == ProductStatus.RTC || 
                                    item.ItemStatus == ProductStatus.PS
                                    select item).FirstOrDefault();
                    if (itemRTCData != null)
                    {
                        labelMessage.Text = RTCITEMS;
                        labelMessage.Visible = true;
                        customButtonVoid.Enabled = false;
                    }

                }

                if (currentPurchase.Items != null && currentPurchase.Items.Count > 0)
                {
                    dataGridViewMdse.AutoGenerateColumns = false;
                    this.dataGridViewMdse.Columns[1].DefaultCellStyle.Format = "c";
                    foreach (Item item in currentPurchase.Items)
                    {
                        DataGridViewRow row = dataGridViewMdse.Rows.AddNew();
                        row.Cells[mdseDesc.Index].Value = item.TicketDescription;
                        row.Cells[cost.Index].Value = item.ItemAmount;
                    }
                }
                else
                {
                    MessageBox.Show("Error in void transaction processing");
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "No items found for purchase " + currentPurchase.TicketNumber + " to void.");
                    Close();
                }


            }
            else if (currentSale != null)
            {
                labelDate.Text = currentSale.MadeTime.ToString();
                labelTransactionNo.Text = currentSale.TicketNumber.ToString();
                labelUserID.Text = currentSale.CreatedBy.ToString();
                labelTotal.Text = currentSale.Amount.ToString("c");
                if (Utilities.GetDateTimeValue(currentSale.MadeTime.ToShortDateString()).AddDays(maxVoidDays) < ShopDateTime.Instance.ShopDate)
                {
                    labelMessage.Text = INVALIDVOIDSALEMESSAGE;
                    labelMessage.Visible = true;
                    customButtonVoid.Enabled = false;
                }

                string rcptId = (from receipt in currentSale.Receipts
                                 where (receipt.Event == ReceiptEventTypes.SALE.ToString()
                                        && receipt.RefNumber == currentSale.TicketNumber.ToString())
                                 select receipt).First().ReceiptDetailNumber;

                Common.Libraries.Utility.Shared.Receipt selectedReceipt = currentSale.Receipts.Find(r => r.ReceiptDetailNumber == rcptId);

                if (selectedReceipt.CreatedBy == "CONV" || currentSale.CreatedBy == "CONV")
                {
                    labelMessage.Text = INVALIDCONVERSIONVOIDSALEMESSAGE;
                    labelMessage.Visible = true;
                    customButtonVoid.Enabled = false;
                }
                
                if (voidSaleFlow)
                {
                    var itemData = (from item in currentSale.RetailItems
                                    where item.DispDoc != currentSale.TicketNumber
                                    select item).FirstOrDefault();
                    if (itemData != null)
                    {
                        labelMessage.Text = RETURNEDSALE;
                        labelMessage.Visible = true;
                        customButtonVoid.Enabled = false;
                    }
                    //SR 12/08/2011 Added the logic below to not allow void of cacc sales that
                    //were refunded
                    var itemCaccData = (from item in currentSale.RetailItems
                                    where item.mDocType == "5" && item.Quantity != item.RefundQuantity
                                    select item).FirstOrDefault();
                    if (itemCaccData != null)
                    {
                        labelMessage.Text = RETURNEDSALE;
                        labelMessage.Visible = true;
                        customButtonVoid.Enabled = false;
                    }

                }

                if (currentSale.RetailItems != null && currentSale.RetailItems.Count > 0)
                {
                    dataGridViewMdse.AutoGenerateColumns = false;
                    this.dataGridViewMdse.Columns[1].DefaultCellStyle.Format = "c";
                    foreach (RetailItem item in currentSale.RetailItems)
                    {
                        DataGridViewRow row = dataGridViewMdse.Rows.AddNew();
                        row.Cells[mdseDesc.Index].Value = item.TicketDescription;
                        row.Cells[cost.Index].Value = item.RetailPrice * item.Quantity;
                    }
                }
                else
                {
                    MessageBox.Show("Error in void transaction processing");
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "No items found for the transaction " + currentSale.TicketNumber + " to void.");
                    Close();
                }

            }
        }

        private void customButtonVoid_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(comboBoxReason.Text))
            {
                MessageBox.Show("You must Select a void Reason");
                comboBoxReason.Focus();
                return;
            }
            ProcessTenderProcedures.ProcessTenderMode mode;
            List<PurchaseVO> purchaseList = new List<PurchaseVO>();
            purchaseList.Add(currentPurchase);
            bool transactionStarted = false;
            try
            {
                if ((voidBuyFlow && !voidVendorBuy) || voidReturnFlow)
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

                        VoidLoanForm.LoanVoidDetails lvd = new VoidLoanForm.LoanVoidDetails();
                        lvd.TickNum = labelTransactionNo.Text;
                        lvd.StoreNum = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                        lvd.OpRef = labelTransactionNo.Text;
                        lvd.OpCode = voidReturnFlow ? "Return" : "Purchase";
                        lvd.OpCd = voidReturnFlow ? ReceiptEventTypes.VRET.ToString() : ReceiptEventTypes.VPR.ToString();
                        lvd.Amount = Utilities.GetDecimalValue(currentPurchase.Amount, 0.0M);
                        lvd.HoldType = "";
                        lvd.RecId = Utilities.GetLongValue(rcptId);
                        lvd.PfiEligDate = Utilities.GetDateTimeValue(currentPurchase.PfiEligible, DateTime.MaxValue);
                        lvd.CreatedBy = Utilities.GetStringValue(currentPurchase.CreatedBy, string.Empty);

                        //BZ # 512
                        lvd.VoidReason = Utilities.GetStringValue(comboBoxReason.Text, string.Empty);
                        lvd.VoidComment = Utilities.GetStringValue(customTextBoxComment.Text, string.Empty);
                        //BZ # 512 - end
                        string errorCode;
                        string errorText;
                        bool retValue = VoidProcedures.PerformVoid(lvd, out errorCode, out errorText);
                        if (retValue)
                        {
                            MessageBox.Show(voidReturnFlow
                                                    ? "Void purchase return completed successfully"
                                                    : "Void purchase completed successfully");
                            
                        }
                        else
                        {
                            DialogResult dgr;
                            dgr = MessageBox.Show("Void transaction failed. Do you want to retry?", "Void Error", MessageBoxButtons.OKCancel);
                            if (dgr == DialogResult.OK)
                                return;
                        }
                        GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();
                        GlobalDataAccessor.Instance.DesktopSession.ClearSessionData();
                        Close();

                    }
                    else
                    {
                        MessageBox.Show("Error in  void transaction processing");
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "No receipts found for purchase " + currentPurchase.TicketNumber + " to void.");
                        Close();
                    }

                }
                if (voidVendorBuy)
                {
                    string errorCode;
                    string errorText;
                    int receiptNumber;
                    mode = ProcessTenderProcedures.ProcessTenderMode.VOIDBUY;
                    GlobalDataAccessor.Instance.beginTransactionBlock();
                    transactionStarted = true;
                    bool retValue = VoidProcedures.VoidVendorPurchase(GlobalDataAccessor.Instance.OracleDA,
                                Utilities.GetIntegerValue(labelTransactionNo.Text, 0),
                                GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                ShopDateTime.Instance.ShopDate.ToShortDateString() + " " + ShopDateTime.Instance.ShopTime.ToString(),
                                comboBoxReason.SelectedItem.ToString(),
                                customTextBoxComment.Text,
                                GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName,
                                out receiptNumber,
                                out errorCode,
                                out errorText);

                    //here add receipt printing code
                    //here call process tender to print void receipt //
                    List<string> refDate = new List<string>();
                    List<string> refNumber = new List<string>();
                    List<string> refType = new List<string>();
                    List<string> refEvent = new List<string>();
                    List<string> refAmount = new List<string>();
                    List<string> refStore = new List<string>();
                    List<string> refTime = new List<string>();

                    refEvent.Add(ReceiptEventTypes.VPR.ToString());
                    refStore.Add(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);
                    refType.Add("4");
                    refAmount.Add(currentPurchase.Amount.ToString());
                    refTime.Add(ShopDateTime.Instance.ShopTransactionTime);
                    refNumber.Add(GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.TicketNumber.ToString());
                    ReceiptDetailsVO rdVo = new ReceiptDetailsVO();
                    rdVo.ReceiptDate = ShopDateTime.Instance.ShopDate;
                    rdVo.RefNumbers = refNumber;
                    rdVo.ReceiptNumber = receiptNumber.ToString();
                    rdVo.RefEvents = refEvent;
                    rdVo.RefAmounts = refAmount;
                    rdVo.RefStores = refStore;
                    rdVo.RefTypes = refType;
                    //NOTE: Only use process tender controller instance, do not allocate a new one
                    ProcessTenderController pct = ProcessTenderController.Instance;
                    pct.generatePurchaseReceipt(mode, GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant(),
                    GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer,
                    purchaseList,
                    GlobalDataAccessor.Instance.DesktopSession.ActiveVendor != null ? GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Name : "",
                    rdVo);

                    if (!retValue)
                    {
                        GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                        transactionStarted = false;
                        MessageBox.Show("Failed to void vendor purchase " + errorText);
                        Close();
                    }
                    GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.COMMIT);
                    transactionStarted = false;
                    MessageBox.Show("Void vendor purchase completed successfully");
                    GlobalDataAccessor.Instance.DesktopSession.ClearSessionData();
                    this.Close();


                }
                if (voidSaleFlow || voidSaleRefundFlow)
                {
                    if (currentSale.Receipts != null)
                    {
                        string rcptId;
                        if (voidSaleFlow)
                        {
                            rcptId = (from receipt in currentSale.Receipts
                                      where (receipt.Event == ReceiptEventTypes.SALE.ToString()
                                      && receipt.RefNumber == currentSale.TicketNumber.ToString())
                                      select receipt).First().ReceiptDetailNumber;
                        }
                        else
                        {
                            rcptId = (from receipt in currentSale.Receipts
                                      where (receipt.Event == ReceiptEventTypes.REF.ToString()
                                      && receipt.RefNumber == currentSale.TicketNumber.ToString())
                                      select receipt).First().ReceiptDetailNumber;
                        }


                        string errorCode;
                        string errorText;
                        int receiptNumber;
                        GlobalDataAccessor.Instance.beginTransactionBlock();
                        transactionStarted = true;
                        bool retValue;
                       // = new ProcessTenderController.ProcessTenderMode();
                        if (voidSaleFlow)
                        {
                            mode = ProcessTenderProcedures.ProcessTenderMode.RETAILVOID;
                            retValue = VoidProcedures.VoidSale(GlobalDataAccessor.Instance.OracleDA,
                                Utilities.GetIntegerValue(labelTransactionNo.Text, 0),
                                GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                GlobalDataAccessor.Instance.CurrentSiteId.TerminalId,
                                GlobalDataAccessor.Instance.DesktopSession.CashDrawerId,
                                comboBoxReason.SelectedItem.ToString(),
                                customTextBoxComment.Text,
                                Utilities.GetIntegerValue(labelTransactionNo.Text, 0),
                                currentSale.TotalSaleAmount.ToString(),
                                1,
                                Utilities.GetIntegerValue(rcptId),
                                ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                ShopDateTime.Instance.ShopTransactionTime.ToString(),
                                GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName,
                                out receiptNumber,
                                out errorCode,
                                out errorText);
                        }
                        else
                        {
                            mode = ProcessTenderProcedures.ProcessTenderMode.RETAILVOIDREFUND;
                            retValue = VoidProcedures.VoidSaleRefund(GlobalDataAccessor.Instance.OracleDA,
                                Utilities.GetIntegerValue(labelTransactionNo.Text, 0),
                                GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                GlobalDataAccessor.Instance.CurrentSiteId.TerminalId,
                                GlobalDataAccessor.Instance.DesktopSession.CashDrawerId,
                                comboBoxReason.SelectedItem.ToString(),
                                customTextBoxComment.Text,
                                Utilities.GetIntegerValue(labelTransactionNo.Text, 0),
                                currentSale.Amount.ToString(),
                                1,
                                Utilities.GetIntegerValue(rcptId),
                                ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                ShopDateTime.Instance.ShopTransactionTime.ToString(),
                                GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName,
                                out receiptNumber,
                                out errorCode,
                                out errorText);

                        }

                        if (retValue)
                        {
                            GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                            transactionStarted = false;
                            if (currentSale.TenderDataDetails != null)
                            {
                                ProcessPaidTenderData(currentSale.TenderDataDetails);
                            }
                            //here call process tender to print void receipt //
                            var refDate = new List<string>();
                            var refNumber = new List<string>();
                            var refType = new List<string>();
                            var refEvent = new List<string>();
                            var refAmount = new List<string>();
                            var refStore = new List<string>();
                            var refTime = new List<string>();

                            refEvent.Add(ReceiptEventTypes.VRET.ToString());
                            refStore.Add(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);
                            refType.Add("4");
                            refTime.Add(ShopDateTime.Instance.ShopTransactionTime);
                            refNumber.Add(GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.TicketNumber.ToString());
                            ReceiptDetailsVO rdVo = new ReceiptDetailsVO();
                            rdVo.ReceiptDate = ShopDateTime.Instance.ShopDate;
                            rdVo.RefNumbers = refNumber;
                            rdVo.ReceiptNumber = receiptNumber.ToString();
                            rdVo.RefEvents = refEvent;
                            rdVo.RefStores = refStore;
                            rdVo.RefTypes = refType;
                            //NOTE: Only use process tender controller instance, do not allocate a new one
                            ProcessTenderController pct = ProcessTenderController.Instance;
                            pct.GenerateSaleReceipt(mode, GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant(),
                            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer,
                            GlobalDataAccessor.Instance.DesktopSession.ActiveRetail,
                            GlobalDataAccessor.Instance.DesktopSession.ActiveVendor != null ? GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Name : "",
                            rdVo);
                            MessageBox.Show(voidSaleFlow
                                                    ? "Void Sale completed successfully"
                                                    : "Void Sale Refund completed successfully");
                            GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();
                            GlobalDataAccessor.Instance.DesktopSession.ClearSessionData();
                            this.Close();
                        }
                        else
                        {
                            GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                            transactionStarted = false;
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
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "No receipts found for sale " + currentSale.TicketNumber + " to void.");
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                if (transactionStarted)
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    transactionStarted = false;
                }
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error when voiding " + ex.Message);
                }
            }
        }

        private static void ProcessPaidTenderData(IEnumerable<TenderData> tenderDataDetails)
        {
            TenderTypes tType;
            CreditCardTypes cType;
            DebitCardTypes dType;
            var voidTenderEntries = new List<TenderEntryVO>(); 
            foreach (var j in tenderDataDetails)
            {
                var newRefEntry = new TenderEntryVO();
                newRefEntry.Amount = j.TenderAmount;
                bool isInbound;
                if (Commons.GetTenderAndCardTypeFromOpCode(
                        j.TenderType,
                        out tType,
                        out cType,
                        out dType,
                        out isInbound))
                {
                    newRefEntry.TenderType = tType;
                    if (newRefEntry.TenderType == TenderTypes.CREDITCARD)
                    {
                        newRefEntry.CardTypeString = cType.ToString();
                        newRefEntry.CreditCardType = cType;
                    }
                    else if (newRefEntry.TenderType == TenderTypes.DEBITCARD)
                    {
                        newRefEntry.CardTypeString = dType.ToString();
                        newRefEntry.DebitCardType = dType;
                    }
                }
                else
                {
                    //Assume it is cash out being refunded
                    newRefEntry.TenderType = TenderTypes.CASHOUT;
                }

                if (newRefEntry.TenderType != TenderTypes.CASHIN &&
                    newRefEntry.TenderType != TenderTypes.CASHOUT)
                {
                    newRefEntry.ReferenceNumber = j.TenderAuth ?? " ";
                }
                voidTenderEntries.Add(newRefEntry);

            }
            GlobalDataAccessor.Instance.DesktopSession.TenderData = new List<TenderEntryVO>();
            if (voidTenderEntries.Count > 0)
            GlobalDataAccessor.Instance.DesktopSession.TenderData = voidTenderEntries;

        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();
            GlobalDataAccessor.Instance.DesktopSession.ClearSessionData();
            this.Close();
        }

        private void dataGridViewMdse_GridViewRowSelecting(object sender, GridViewRowSelectingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
