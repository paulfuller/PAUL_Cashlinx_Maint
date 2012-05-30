using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Components.EventArgs;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Services.Void
{
    public partial class VoidLayawayActivity : CustomBaseForm
    {
        private Int64 maxVoidDays;
        private LayawayVO currentLayaway;
        private bool maxVoidPassed;
        private bool storeCreditUsed;
        private bool forceOverride = false;

        private DataGridViewRow voidRow;
        ProcessTenderProcedures.ProcessTenderMode mode;

        private const string INVALIDCONVERSIONVOIDSALEMESSAGE = "This transaction occurred prior to conversion and is not eligible for void. Consider using a refund transaction.";

        public VoidLayawayActivity(bool hasVoidResource)
        {
            forceOverride = !hasVoidResource;
            InitializeComponent();
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void customButtonVoid_Click(object sender, EventArgs e)
        {
            if (currentLayaway.Receipts == null || currentLayaway.Receipts.Count == 0 || voidRow == null)
            {
                return;
            }

            try
            {
                bool retValue = false;
                int receiptNumber = 0;

                maxVoidPassed = false;
                storeCreditUsed = false;

                string receiptID = string.Empty;
                switch (mode)
                {
                    case ProcessTenderProcedures.ProcessTenderMode.LAYAWAYVOID:
                        VoidLayaway(out retValue, out receiptNumber, out receiptID);
                        break;
                    case ProcessTenderProcedures.ProcessTenderMode.LAYPAYMENTVOID:
                        VoidLayawayPayment(out retValue, out receiptNumber, out receiptID);
                        break;
                    case ProcessTenderProcedures.ProcessTenderMode.LAYFORFVOID:
                        VoidLayawayForf(out retValue, out receiptNumber, out receiptID);
                        break;
                }
                if (!retValue && maxVoidPassed)
                {
                    return;
                }
                if (!retValue && storeCreditUsed)
                    return;
                if (retValue)
                    MessageBox.Show("Void Layaway completed successfully");
                else
                {
                    DialogResult dgr;
                    dgr = MessageBox.Show("Void transaction failed. Do you want to retry?", "Void Error", MessageBoxButtons.OKCancel);
                    if (dgr == DialogResult.OK)
                        return;
                    Close();
                }
                if (mode != ProcessTenderProcedures.ProcessTenderMode.LAYFORFVOID)
                {
                    //code here to print void Layaway Receipt
                    List<string> refNumber = new List<string>();
                    List<string> refType = new List<string>();
                    List<string> refEvent = new List<string>();
                    List<string> refStore = new List<string>();
                    List<string> refTime = new List<string>();

                    if (mode == ProcessTenderProcedures.ProcessTenderMode.LAYAWAYVOID)
                        refEvent.Add(ReceiptEventTypes.LAY.ToString());
                    else if (mode == ProcessTenderProcedures.ProcessTenderMode.LAYPAYMENTVOID || mode == ProcessTenderProcedures.ProcessTenderMode.LAYFORFVOID)
                        refEvent.Add(ReceiptEventTypes.LAYPMT.ToString());
                    refStore.Add(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);
                    refType.Add("4");
                    refTime.Add(ShopDateTime.Instance.ShopTransactionTime);
                    refNumber.Add(currentLayaway.TicketNumber.ToString());

                    ReceiptDetailsVO rdVo = new ReceiptDetailsVO();
                    rdVo.ReceiptDate = ShopDateTime.Instance.ShopDate;
                    rdVo.RefNumbers = refNumber;
                    rdVo.ReceiptNumber = receiptNumber.ToString();
                    rdVo.RefEvents = refEvent;
                    rdVo.RefStores = refStore;
                    rdVo.RefTypes = refType;


                    if (currentLayaway.TenderDataDetails != null)
                    {
                        ProcessPaidTenderData(currentLayaway.TenderDataDetails, receiptID);
                    }

                    //NOTE: Only use ProcessTenderController.Instance!!
                    ProcessTenderController ptCntrl = ProcessTenderController.Instance;
                    ptCntrl.GenerateLayawayPaymentReceipt(mode, GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant(),
                                                          GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer,
                                                          currentLayaway,
                                                          GlobalDataAccessor.Instance.DesktopSession.ActiveVendor != null ? 
                                                          GlobalDataAccessor.Instance.DesktopSession.ActiveVendor.Name : "",
                                                          rdVo);

                }

                Close();

            }
            catch (Exception ex)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error in void layaway activity " + ex.Message);
                MessageBox.Show("Error occurred in void layaway activity processing.");
                Close();
            }
        }

        private void dataGridViewMdse_GridViewRowSelecting(object sender, GridViewRowSelectingEventArgs e)
        {
            e.Cancel = true;
        }

        private void gvTransactions_GridViewRowSelecting(object sender, GridViewRowSelectingEventArgs e)
        {
            e.Cancel = true;
            return;
        }

        private void VoidLayawayActivity_Load(object sender, EventArgs e)
        {
            currentLayaway = GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway;
            labelDate.Text = currentLayaway.MadeTime.ToString();
            labelUserID.Text = currentLayaway.CreatedBy;

            gvTransactions.Columns[colStatusDate.Index].DefaultCellStyle.Format = "d";
            gvTransactions.Columns[colAmount.Index].DefaultCellStyle.Format = "c";

            DataGridViewRow row = gvTransactions.Rows.AddNew();
            row.Cells[colTransactionNumber.Index].Value = currentLayaway.TicketNumber;
            row.Cells[colTransactionType.Index].Value = "Layaway";
            row.Cells[colTenderType.Index].Value = string.Empty;
            row.Cells[colStatus.Index].Value = "Layaway";
            row.Cells[colStatusDate.Index].Value = currentLayaway.StatusDate;
            row.Cells[colAmount.Index].Value = currentLayaway.Amount;
            row.Tag = currentLayaway;
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

            voidRow = row;
            mode = ProcessTenderProcedures.ProcessTenderMode.LAYAWAYVOID;

            var receiptsToProcess = (from rcpt in currentLayaway.Receipts
                                     where string.IsNullOrWhiteSpace(rcpt.ReferenceReceiptNumber) && !rcpt.Event.Contains("VLAY") && !rcpt.Event.Contains("REF")
                                     && !rcpt.Event.Contains("VFORF")
                                     select rcpt).OrderBy(r => r.RefTime);

            foreach (var rdVo in receiptsToProcess)
            {
                if (rdVo.Event == ReceiptEventTypes.LAY.ToString())
                {
                    continue;
                }

                int index = 1;
                DataGridViewRow paymentRow = gvTransactions.Rows.AddNew();
                paymentRow.Cells[colTransactionNumber.Index].Value = rdVo.ReceiptDetailNumber;
                paymentRow.Cells[colTransactionType.Index].Value = rdVo.TypeDescription;
                paymentRow.Cells[colStatus.Index].Value = "ACT";
                paymentRow.Cells[colStatusDate.Index].Value = rdVo.Date;
                paymentRow.Tag = rdVo;
                voidRow = paymentRow;

                if (rdVo.Event == ReceiptEventTypes.LAYPMT.ToString())
                {
                    mode = ProcessTenderProcedures.ProcessTenderMode.LAYPAYMENTVOID;
                }
                else if (rdVo.Event == ReceiptEventTypes.FORF.ToString())
                {
                    mode = ProcessTenderProcedures.ProcessTenderMode.LAYFORFVOID;
                }

                foreach (TenderData tdVo in currentLayaway.TenderDataDetails)
                {
                    if (tdVo.ReceiptNumber == rdVo.ReceiptNumber && rdVo.RefNumber == currentLayaway.TicketNumber.ToString())
                    {
                        if (index > 1)
                        {
                            paymentRow = gvTransactions.Rows.AddNew();
                        }

                        paymentRow.Cells[colTenderType.Index].Value = tdVo.MethodOfPmt;
                        paymentRow.Cells[colAmount.Index].Value = tdVo.TenderAmount;
                        index++;
                    }
                }
            }

            voidRow.Cells[colStatus.Index].Value = currentLayaway.LoanStatus.ToString();
            voidRow.DefaultCellStyle.BackColor = Color.LightGray;

            if (currentLayaway.RetailItems != null && currentLayaway.RetailItems.Count > 0)
            {
                var _bindingSource1 = new BindingSource { DataSource = currentLayaway.RetailItems };
                dataGridViewMdse.AutoGenerateColumns = false;
                this.dataGridViewMdse.DataSource = _bindingSource1;
                this.dataGridViewMdse.Columns[0].DataPropertyName = "icn";
                this.dataGridViewMdse.Columns[1].DataPropertyName = "gunnumber";
                this.dataGridViewMdse.Columns[2].DataPropertyName = "ticketdescription";
                this.dataGridViewMdse.Columns[3].DataPropertyName = "retailprice";
                this.dataGridViewMdse.Columns[3].DefaultCellStyle.Format = "c";

                this.dataGridViewMdse.AutoGenerateColumns = false;
            }
            else
            {
                MessageBox.Show("Error in void transaction processing");
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "No items found for the transaction " + currentLayaway.TicketNumber + " to void.");
                Close();
            }
            //SR 07/28/2011 Disable void if the layaway has been terminated
            if (currentLayaway.LoanStatus == ProductStatus.TERM || currentLayaway.LoanStatus == ProductStatus.REF)
            {
                customButtonVoid.Enabled = false;
            }
        }

        private static void ProcessPaidTenderData(IEnumerable<TenderData> tenderDataDetails, string receiptID)
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
                if (j.ReceiptNumber == receiptID)
                {
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
            }
            GlobalDataAccessor.Instance.DesktopSession.TenderData = new List<TenderEntryVO>();
            if (voidTenderEntries.Count > 0)

                GlobalDataAccessor.Instance.DesktopSession.TenderData = voidTenderEntries;
        }

        private void VoidLayaway(out bool retValue, out int receiptNumber, out string receiptID)
        {
            string errorCode;
            string errorText;
            retValue = false;
            receiptNumber = 0;
            receiptID = string.Empty;
            List<string> tenderTypes = new List<string>();
            List<string> tenderAmount = new List<string>();
            List<string> tenderAuth = new List<string>();

            LayawayVO layaway = voidRow.Tag as LayawayVO;

            if (layaway == null)
            {
                return;
            }

            string rcptId = (from receipt in currentLayaway.Receipts
                             where (receipt.Event == ReceiptEventTypes.LAY.ToString()
                                    && receipt.RefNumber == currentLayaway.TicketNumber.ToString())
                             select receipt).First().ReceiptDetailNumber;

            var selectedReceipt = currentLayaway.Receipts.Find(r => r.ReceiptDetailNumber == rcptId);
            receiptID = selectedReceipt.ReceiptNumber;
            if (Utilities.GetDateTimeValue(selectedReceipt.Date).AddDays(maxVoidDays) < ShopDateTime.Instance.ShopDate)
            {
                MessageBox.Show("The maximum number of days to void has passed for this transaction");
                maxVoidPassed = true;
                return;
            }

            decimal layawayVoidAmount = (from receipt in currentLayaway.Receipts
                                         where (receipt.Event == ReceiptEventTypes.LAY.ToString()
                                                && receipt.RefNumber == currentLayaway.TicketNumber.ToString())
                                         select receipt).First().Amount;

            foreach (TenderData tdvo in currentLayaway.TenderDataDetails)
            {
                if (tdvo.ReceiptNumber == rcptId)
                {
                    tenderTypes.Add(tdvo.TenderType);
                    tenderAmount.Add(tdvo.TenderAmount.ToString());
                    tenderAuth.Add(tdvo.TenderAuth);
                }
            }

            GlobalDataAccessor.Instance.beginTransactionBlock();
            retValue = VoidProcedures.VoidLayaway(GlobalDataAccessor.Instance.OracleDA,
                                                  currentLayaway.TicketNumber,
                                                  GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                  GlobalDataAccessor.Instance.CurrentSiteId.TerminalId,
                                                  GlobalDataAccessor.Instance.DesktopSession.CashDrawerId,
                                                  "",
                                                  "",
                                                  currentLayaway.TicketNumber,
                                                  layawayVoidAmount.ToString(),
                                                  1,
                                                  Utilities.GetIntegerValue(rcptId),
                                                  ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                                  ShopDateTime.Instance.ShopTransactionTime.ToString(),
                                                  GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName,
                                                  out receiptNumber,
                                                  out errorCode,
                                                  out errorText);

            GlobalDataAccessor.Instance.endTransactionBlock(!retValue ? EndTransactionType.ROLLBACK : EndTransactionType.COMMIT);
        }

        private void VoidLayawayForf(out bool retValue, out int receiptNumber, out string receiptID)
        {
            string errorCode;
            string errorText;
            retValue = false;
            receiptNumber = 0;

            var rcptVo = (Common.Libraries.Utility.Shared.Receipt)voidRow.Tag;
            var rcptId = (from receipt in currentLayaway.Receipts
                             where (receipt.Event == ReceiptEventTypes.FORF.ToString()
                                    && receipt.RefNumber == currentLayaway.TicketNumber.ToString() &&
                                    receipt.ReceiptDetailNumber == rcptVo.ReceiptDetailNumber.ToString())
                             select receipt).First().ReceiptNumber;

            var selectedReceipt = currentLayaway.Receipts.Find(r => r.ReceiptNumber == rcptId);
            receiptID = selectedReceipt.ReceiptNumber;
            if (Utilities.GetDateTimeValue(selectedReceipt.Date).AddDays(maxVoidDays) < ShopDateTime.Instance.ShopDate)
            {
                MessageBox.Show("The maximum number of days to void has passed for this transaction");
                maxVoidPassed = true;
                return;
            }

            GlobalDataAccessor.Instance.beginTransactionBlock();
            retValue = VoidProcedures.VoidLayawayForfeiture(GlobalDataAccessor.Instance.OracleDA,
                                                            currentLayaway.TicketNumber, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                            GlobalDataAccessor.Instance.CurrentSiteId.TerminalId,
                                                            GlobalDataAccessor.Instance.DesktopSession.CashDrawerId,
                                                            Utilities.GetIntegerValue(rcptId), ShopDateTime.Instance.ShopDate.FormatDate(), ShopDateTime.Instance.ShopTransactionTime.ToString(),
                                                            GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                                                            out receiptNumber,
                                                            out errorCode,
                                                            out errorText);
            if (!retValue && errorCode == "104")
            {
                MessageBox.Show(Commons.GetMessageString("VoidForfeitureFailure"));
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                storeCreditUsed = true;
                return;
            }
            GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(!retValue ? EndTransactionType.ROLLBACK : EndTransactionType.COMMIT);
        }

        private void VoidLayawayPayment(out bool retValue, out int receiptNumber, out string receiptID)
        {
            string errorCode;
            string errorText;
            retValue = false;
            receiptNumber = 0;
            List<string> tenderTypes = new List<string>();
            List<string> tenderAmount = new List<string>();
            List<string> tenderAuth = new List<string>();

            var rcptVo = (Common.Libraries.Utility.Shared.Receipt)voidRow.Tag;
            string rcptId = (from receipt in currentLayaway.Receipts
                             where (receipt.Event == ReceiptEventTypes.LAYPMT.ToString()
                                    && receipt.RefNumber == currentLayaway.TicketNumber.ToString() &&
                                    receipt.ReceiptDetailNumber == rcptVo.ReceiptDetailNumber.ToString())
                             select receipt).First().ReceiptNumber;
            var selectedReceipt = currentLayaway.Receipts.Find(r => string.Equals(r.ReceiptNumber, rcptId, StringComparison.Ordinal));
            receiptID = rcptId;
            if (Utilities.GetDateTimeValue(selectedReceipt.Date).AddDays(maxVoidDays) < ShopDateTime.Instance.ShopDate)
            {
                MessageBox.Show("The maximum number of days to void has passed for this transaction");
                maxVoidPassed = true;
                return;
            }

            if (selectedReceipt.CreatedBy == "CONV")
            {
                MessageBox.Show(INVALIDCONVERSIONVOIDSALEMESSAGE);
                return;
            }
            decimal layawayPaymentAmount = (from tender in currentLayaway.TenderDataDetails
                                            where tender.ReceiptNumber == rcptId
                                            select tender).Sum(p => p.TenderAmount);

            decimal layawayVoidAmount = (from receipt in currentLayaway.Receipts
                                         where (receipt.Event == ReceiptEventTypes.LAYPMT.ToString()
                                                && receipt.RefNumber == currentLayaway.TicketNumber.ToString() &&
                                                receipt.ReceiptDetailNumber == rcptVo.ReceiptDetailNumber)
                                         select receipt).First().Amount;
            decimal distributionPercentage = layawayVoidAmount / layawayPaymentAmount;

            if (currentLayaway.TenderDataDetails != null)
            {
                foreach (TenderData tdvo in currentLayaway.TenderDataDetails)
                {
                    if (tdvo.ReceiptNumber == rcptId)
                    {
                        tenderTypes.Add(tdvo.TenderType);
                        tenderAmount.Add((Math.Round((tdvo.TenderAmount * distributionPercentage), 2)).ToString());
                        tenderAuth.Add(tdvo.TenderAuth);
                    }
                }
            }
            int numberOfTenderEntries = tenderAmount.Count;

            if (numberOfTenderEntries > 1)
            {
                var totalTenderAmt = 0.0M;
                for (int i = 0; i < numberOfTenderEntries - 1; i++)
                    totalTenderAmt += Utilities.GetDecimalValue(tenderAmount[i], 0);
                tenderAmount.RemoveAt(numberOfTenderEntries - 1);
                tenderAmount.Insert(numberOfTenderEntries - 1, (layawayVoidAmount - totalTenderAmt).ToString());
            }

            GlobalDataAccessor.Instance.beginTransactionBlock();
            retValue = VoidProcedures.VoidLayawayPayment(GlobalDataAccessor.Instance.OracleDA,
                                                         currentLayaway.TicketNumber, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                                         GlobalDataAccessor.Instance.CurrentSiteId.TerminalId,
                                                         GlobalDataAccessor.Instance.DesktopSession.CashDrawerId,
                                                         layawayVoidAmount.ToString(),
                                                         Utilities.GetIntegerValue(rcptId), ShopDateTime.Instance.ShopDate.FormatDate(), ShopDateTime.Instance.ShopTransactionTime.ToString(),
                                                         GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                                                         "", "", tenderTypes,
                                                         tenderAmount,
                                                         tenderAuth,
                                                         ShopDateTime.Instance.ShopDate.FormatDate(),
                                                         out receiptNumber,
                                                         out errorCode,
                                                         out errorText);
            GlobalDataAccessor.Instance.endTransactionBlock(!retValue ? EndTransactionType.ROLLBACK : EndTransactionType.COMMIT);
        }
    }
}
