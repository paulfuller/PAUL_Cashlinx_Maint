using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Couch;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Components.EventArgs;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Pawn.Products.ProductDetails;
using Pawn.Logic;
using Document = Common.Libraries.Objects.Doc.Document;

namespace Pawn.Forms.Layaway
{
    public partial class LayawayRefund : CustomBaseForm
    {
        private bool SHOWING_TENDER_TYPES_AND_SINGLE_RECEIPT_ENTRIES = false;
        private Controller_ProductServices.ReceiptLookupInfo _currReceiptLookupInfo = null;
        # region Constructors

        public LayawayRefund()
        {
            Layaway = GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway;
            InitializeComponent();
            NavControlBox = new NavBox
            {
                Owner = this
            };
        }

        # endregion

        # region Properties

        public LayawayVO Layaway { get; set; }

        public NavBox NavControlBox { get; set; }

        private LayawayPaymentHistoryBuilder Builder { get; set; }

        
        private LayawayHistoryPaymentInfo PreviousPaymentInfo { get; set; }
        private LayawayHistoryPaymentInfo ReturnPaymentInfo { get; set; }

        # endregion

        # region Event Handlers

        private void cancelButton_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void gvPayments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != colLayawayPaymentNumber.Index)
            {
                return;
            }
            ViewPrintReceipt(Convert.ToInt32(gvPayments.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
        }

        private void gvPayments_GridViewRowSelecting(object sender, GridViewRowSelectingEventArgs e)
        {
            e.Cancel = true;
        }

        private void LayawayRefund_Load(object sender, EventArgs e)
        {
            GlobalDataAccessor.Instance.DesktopSession.ReceiptToRefund = "";

            PopulateGridView();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape || (this.ActiveControl.Equals(cancelButton) && keyData == Keys.Enter))
            {
                this.cancelButton_Click(null, new EventArgs());
                return true;
            }

            if (keyData == Keys.Enter)
            {
                this.refundButton_Click(null, new EventArgs());
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void refundButton_Click(object sender, EventArgs e)
        {
            decimal restockingFee = Utilities.GetDecimalValue(txtRestockingFee.Text);

            AddLayawayRestockingFee(Layaway, restockingFee);

            //Make the Layway object the new activelayaway
            GlobalDataAccessor.Instance.DesktopSession.Layaways.Clear();
            GlobalDataAccessor.Instance.DesktopSession.Layaways.Add(Layaway);

            var refundEntries = new List<TenderEntryVO>();
            bool isInbound;
            TenderTypes tType;
            CreditCardTypes cType;
            DebitCardTypes dType;
            decimal couponLayAmount = 0;
            if (ReturnPaymentInfo.TenderDataDetails != null)
            {
                foreach (var j in ReturnPaymentInfo.TenderDataDetails)
                {
                    //Layaway service fee is never refunded so ignore that tender
                    if (j.ReversalInfo == "F")
                    {
                        continue;
                    }
                    var newRefEntry = new TenderEntryVO();
                    newRefEntry.Amount = j.TenderAmount;
                    if (Commons.GetTenderAndCardTypeFromOpCode(
                            j.TenderType,
                            out tType,
                            out cType,
                            out dType,
                            out isInbound))
                    {
                        newRefEntry.TenderType = tType;
                        if (tType == TenderTypes.COUPON)
                        {
                            couponLayAmount = j.TenderAmount;
                            continue;
                        }
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
                    //Add entry to list
                    newRefEntry.Amount = j.TenderAmount;
                    refundEntries.Add(newRefEntry);
                }
            }




            decimal layawayRefundAmount = 0;
            if (ReturnPaymentInfo.Receipt.Event==ReceiptEventTypes.LAY.ToString())
                layawayRefundAmount=ReturnPaymentInfo.Receipt.Amount - couponLayAmount - restockingFee;
            else
                layawayRefundAmount = ReturnPaymentInfo.Receipt.Amount - restockingFee;
            
            GlobalDataAccessor.Instance.DesktopSession.ReceiptToRefund = ReturnPaymentInfo.ReceiptNumber;


            //Add call to tender out
            GlobalDataAccessor.Instance.DesktopSession.RefundEntries = refundEntries;
            GlobalDataAccessor.Instance.DesktopSession.PartialRefundEntries = null;
            GlobalDataAccessor.Instance.DesktopSession.TotalServiceAmount = layawayRefundAmount;
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "Tender";
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        private void txtRestockingFee_Leave(object sender, EventArgs e)
        {
            decimal restockingFee = 0;

            if (!decimal.TryParse(txtRestockingFee.Text, out restockingFee))
            {
                MessageBox.Show("Invalid Restocking Fee Amount");
                HighlightControl(txtRestockingFee);
                restockingFee = Utilities.GetDecimalValue(txtRestockingFee.PreviousValue);
            }

            decimal maxRestockingFee = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetLayawayRestockingFee(GlobalDataAccessor.Instance.CurrentSiteId);

            if (restockingFee > maxRestockingFee)
            {
                MessageBox.Show("The refund amount cannot exceed the maximum value of restocking fee allowed.");
                HighlightControl(txtRestockingFee);
                restockingFee = Utilities.GetDecimalValue(txtRestockingFee.PreviousValue);
            }

            if (restockingFee > ReturnPaymentInfo.PaymentAmountMade)
            {
                MessageBox.Show("The restocking fee amount cannot exceed the layaway payment refund total.");
                HighlightControl(txtRestockingFee);
                restockingFee = Utilities.GetDecimalValue(txtRestockingFee.PreviousValue);
            }

            UpdateTotals(restockingFee);
        }

        private void txtRestockingFee_TextChanged(object sender, EventArgs e)
        {

        }

        # endregion

        # region Helper Methods
        private void ViewPrintReceipt(int receiptNumber)

        {
            
            List<CouchDbUtils.PawnDocInfo> pawnDocs;
            var errString = string.Empty;
            string strReceiptNumber = receiptNumber.ToString();
            int intReceiptNumber = 0;
            intReceiptNumber = Convert.ToInt32(strReceiptNumber);
            //If a legit ticket number was pulled, then continue.
            if (Layaway.TicketNumber > 0)
            {
                //Instantiate docinfo which will return info we need to be able to 
                //call reprint ticket.
                CouchDbUtils.PawnDocInfo docInfo = new CouchDbUtils.PawnDocInfo();
                docInfo.SetDocumentSearchType(CouchDbUtils.DocSearchType.RECEIPT);
                docInfo.StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                docInfo.TicketNumber = Layaway.TicketNumber;
                //int receiptNumber = 0;
                //if (!string.IsNullOrEmpty(PS_ReceiptNoValue.Text))
                //receiptNumber = Convert.ToInt32(PS_ReceiptNoValue.Text);
                docInfo.ReceiptNumber = (long)intReceiptNumber;

                this._currReceiptLookupInfo = new Controller_ProductServices.ReceiptLookupInfo
                                                {
                                                    DocumentName = "Receipt# " + strReceiptNumber,
                                                    StorageID = docInfo.StorageId,
                                                    DocumentType = docInfo.DocumentType
                                                };
                //Use couch DB to get the document.
                if (CouchDbUtils.GetPawnDocument(GlobalDataAccessor.Instance.OracleDA, GlobalDataAccessor.Instance.CouchDBConnector,
                    docInfo, false, out pawnDocs, out errString))
                {
                    if (pawnDocs != null)
                    {
                        //Find that there is a document with a receipt.
                        var results = from p in pawnDocs
                                        where p.DocumentType ==
                                        Document.DocTypeNames.RECEIPT
                                        select p;
                        if (results.Any())
                        {
                            //Get the only one receipt that should exist.
                            docInfo = (CouchDbUtils.PawnDocInfo)results.First();

                            //Call the reprint screen.
                            ViewPrintDocument docViewPrint = new ViewPrintDocument("Receipt# ", strReceiptNumber, docInfo.StorageId, docInfo.DocumentType, docInfo);
                            docViewPrint.ShowDialog();

                        }
                        else
                        {
                            Console.WriteLine(
                                "Did not find a receipt for ticket: " + Layaway.TicketNumber);
                            //Todo: Log or show problem if one exists here.
                        }
                    }
                }
                else if (this._currReceiptLookupInfo != null)
                {
                    ViewPrintDocument docViewPrint = new ViewPrintDocument("Receipt# ", strReceiptNumber, this._currReceiptLookupInfo.StorageID, this._currReceiptLookupInfo.DocumentType, docInfo);
                    docViewPrint.ShowDialog();
                }
            }
        }

        private void AddLayawayRestockingFee(LayawayVO layaway, decimal restockingFee)
        {
            layaway.Fees = new List<Fee>();

            if (restockingFee <= 0)
            {
                return;
            }

            Fee fee = new Fee()
            {
                FeeType = FeeTypes.RESTOCKINGFEE,
                Value = restockingFee,
                OriginalAmount = restockingFee,
                FeeState = FeeStates.PAID,
                FeeDate = Utilities.GetDateTimeValue(ShopDateTime.Instance.ShopTransactionTime, DateTime.Now)
            };
            layaway.Fees.Add(fee);
        }

        private string GetPaidTenderTypes(List<LayawayRefundTenderInfo> tenderBreakdown)
        {
            StringBuilder sb = new StringBuilder();
            foreach (LayawayRefundTenderInfo tender in tenderBreakdown)
            {
                sb.AppendLine(tender.TenderDescription);
            }
            return sb.ToString();
        }

        private string GetPaidTenderAmounts(List<LayawayRefundTenderInfo> tenderBreakdown)
        {
            StringBuilder sb = new StringBuilder();
            foreach (LayawayRefundTenderInfo tender in tenderBreakdown)
            {
                sb.AppendLine(tender.AmountPaid == 0 ? "-----" : tender.AmountPaid.ToString("c"));
            }
            return sb.ToString();
        }

        private string GetReturnedTenderAmounts(LayawayHistoryPaymentInfo paymentInfo, List<LayawayRefundTenderInfo> tenderBreakdown)
        {
            if (paymentInfo.AssociatedPaymentInfo == null)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            foreach (LayawayRefundTenderInfo tender in tenderBreakdown)
            {
                sb.AppendLine(tender.AmountReturned == 0 ? "-----" : tender.AmountReturned.ToString("c"));
            }
            return sb.ToString();
        }

        private void PopulateGridView()
        {
            ReturnPaymentInfo = null;
            PreviousPaymentInfo = null;

            try
            {
                Builder = new LayawayPaymentHistoryBuilder(Layaway);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error building the payment schedule");
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "LayawayRefund_Load errored:  " + exc.Message);
                refundButton.Enabled = false;
                return;
            }

            lblLayawayNumber.Text = Layaway.TicketNumber.ToString();
            lblCustomerName.Text = Layaway.EntityName;
            lblTransactionDate.Text = Layaway.DateMade.ToString("d");
            lblLayawayAmount.Text = (Layaway.Amount + Layaway.SalesTaxAmount).ToString("c"); // should this include sales tax or anything else
            lblRefundedToDate.Text = Builder.GetTotalRefunded().ToString("c");
            lblPaidToDate.Text = Builder.GetTotalPaid().ToString("c");
            lblBalanceOwed.Text = Builder.GetBalanceOwed().ToString("c");

            gvPayments.AutoGenerateColumns = false;
            string previousReceiptNumber = string.Empty;

            gvPayments.Columns[colTenderType.Index].Visible = SHOWING_TENDER_TYPES_AND_SINGLE_RECEIPT_ENTRIES;
            int lastNonRefundedLayawayPaymentIndex = -1;
            int previousNonRefundedLayawayPaymentIndex = -1;

            foreach (LayawayHistory history in Builder.ScheduledPayments)
            {
                for (int i = 0; i < history.Payments.Count; i++)
                {
                    LayawayHistoryPaymentInfo paymentInfo = history.Payments.OrderBy(p => p.PaymentMadeOn).ToArray()[i];

                    if (paymentInfo.IsRefund)
                    {
                        continue;
                    }

                    if (SHOWING_TENDER_TYPES_AND_SINGLE_RECEIPT_ENTRIES)
                    {
                        if (previousReceiptNumber.Equals(paymentInfo.ReceiptNumber))
                        {
                            continue;
                        }
                        previousReceiptNumber = paymentInfo.ReceiptNumber;

                        DataGridViewRow row = gvPayments.Rows.AddNew();
                        if (!paymentInfo.IsRefunded())
                        {
                            previousNonRefundedLayawayPaymentIndex = lastNonRefundedLayawayPaymentIndex;
                            lastNonRefundedLayawayPaymentIndex = row.Index;
                        }
                        List<LayawayRefundTenderInfo> tenderBreakdown = paymentInfo.GetRefundTenderBreakdown();

                        row.Cells[this.colLayawayPaymentNumber.Index].Value = paymentInfo.ReceiptNumber;
                        row.Cells[this.colDateMade.Index].Value = paymentInfo.PaymentMadeOn.ToString("g");
                        row.Cells[this.colDueDate.Index].Value = history.PaymentDueDate.ToString("d");
                        row.Cells[this.colEmployeeNumber.Index].Value = paymentInfo.Receipt.CreatedBy;
                        row.Cells[this.colTenderType.Index].Value = GetPaidTenderTypes(tenderBreakdown);
                        row.Cells[this.colAmountPaid.Index].Value = GetPaidTenderAmounts(tenderBreakdown);
                        row.Cells[this.colRefundAmount.Index].Value = GetReturnedTenderAmounts(paymentInfo, tenderBreakdown);
                        row.Tag = paymentInfo;
                    }
                    else
                    {
                        DataGridViewRow row = gvPayments.Rows.AddNew();

                        if (!previousReceiptNumber.Equals(paymentInfo.ReceiptNumber))
                        {
                            row.Cells[this.colLayawayPaymentNumber.Index].Value = paymentInfo.ReceiptNumber;
                            if (!paymentInfo.IsRefunded())
                            {
                                previousNonRefundedLayawayPaymentIndex = lastNonRefundedLayawayPaymentIndex;
                                lastNonRefundedLayawayPaymentIndex = row.Index;
                            }
                        }

                        previousReceiptNumber = paymentInfo.ReceiptNumber;

                        row.Cells[this.colDateMade.Index].Value = paymentInfo.PaymentMadeOn.ToString("g");
                        row.Cells[this.colDueDate.Index].Value = history.PaymentDueDate.ToString("d");
                        row.Cells[this.colEmployeeNumber.Index].Value = paymentInfo.Receipt.CreatedBy;
                        //row.Cells[this.colTenderType.Index].Value = GetPaidTenderTypes(tenderBreakdown);
                        row.Cells[this.colAmountPaid.Index].Value = paymentInfo.PaymentAmountMade.ToString("c");
                        row.Cells[this.colRefundAmount.Index].Value = paymentInfo.AssociatedPaymentInfo == null ? string.Empty : paymentInfo.AssociatedPaymentInfo.PaymentAmountMade.ToString("c");
                        row.Tag = paymentInfo;
                    }
                }
            }

            ReturnPaymentInfo = lastNonRefundedLayawayPaymentIndex == -1 ? null : gvPayments.Rows[lastNonRefundedLayawayPaymentIndex].Tag as LayawayHistoryPaymentInfo;
            PreviousPaymentInfo = previousNonRefundedLayawayPaymentIndex == -1 ? null : gvPayments.Rows[previousNonRefundedLayawayPaymentIndex].Tag as LayawayHistoryPaymentInfo;

            if (ReturnPaymentInfo == null)
            {
                refundButton.Enabled = false;
                txtRestockingFee.Enabled = false;
                MessageBox.Show("No payment available to refund");
                return;
            }
            UpdateTotals(0M);

            if (!SHOWING_TENDER_TYPES_AND_SINGLE_RECEIPT_ENTRIES)
            {
                foreach (DataGridViewRow row in gvPayments.Rows)
                {
                    LayawayHistoryPaymentInfo payment = row.Tag as LayawayHistoryPaymentInfo;
                    if (payment.ReceiptNumber.Equals(ReturnPaymentInfo.ReceiptNumber))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                    }
                }
            }

            int maxDaysForRefundEligibility = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMaxDaysForRefundEligibility(GlobalDataAccessor.Instance.CurrentSiteId);
            if (ShopDateTime.Instance.FullShopDateTime.Date > (Utilities.GetDateTimeValue(ReturnPaymentInfo.PaymentMadeOn.ToString())).AddDays(maxDaysForRefundEligibility).Date)
            {
                MessageBox.Show("The number of days eligible for refund has expired for the selected transaction");
                refundButton.Enabled = false;
                txtRestockingFee.Enabled = false;
                return;
            }
            else if (PreviousPaymentInfo != null)
            {
                if (LayawayProcedures.IsLayawayEligibleForForfeiture(PreviousPaymentInfo.PaymentMadeOn, GlobalDataAccessor.Instance.CurrentSiteId))
                {
                    MessageBox.Show("Completing the layaway payment refund makes the layaway eligible for Forfeiture.");
                }
            }
        }

        private void UpdateTotals(decimal restockingFee)
        {
            decimal couponLayAmount=0;
            TenderData couponTender = ReturnPaymentInfo.TenderDataDetails.Find(i => i.MethodOfPmt.ToUpper() == TenderTypes.COUPON.ToString() && i.ReversalInfo=="D");

            if (couponTender != null)
                couponLayAmount = couponTender.TenderAmount;

            decimal refundAmount = ReturnPaymentInfo.Receipt.Amount-couponLayAmount;

            lblRefundAmountValue.Text = refundAmount.ToString("c");
            txtRestockingFee.Text = restockingFee.ToString("f2");
            lblRefundTotalValue.Text = (refundAmount - restockingFee).ToString("c");
            lblBalanceAfterRefundValue.Text = (Builder.GetBalanceOwed() + refundAmount).ToString("c");
        }

        # endregion
    }
}
