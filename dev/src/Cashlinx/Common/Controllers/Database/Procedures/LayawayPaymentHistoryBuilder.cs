using System;
using System.Collections.Generic;
using System.Linq;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Common.Controllers.Database.Procedures
{
    public class LayawayPaymentHistoryBuilder
    {
        private LayawayVO _layaway;

        # region Constructors

        public LayawayPaymentHistoryBuilder()
        {
        }

        public LayawayPaymentHistoryBuilder(LayawayVO layaway)
        {
            Layaway = layaway;
            Calculate();
        }

        # endregion

        # region Properties

        public LayawayVO Layaway
        {
            get
            {
                return _layaway;
            }
            set
            {
                _layaway = value;
                Receipts = Utilities.CloneObject<List<Receipt>>(value.Receipts);
            }
        }

        public List<LayawayHistory> ScheduledPayments { get; set; }
        public List<Receipt> Receipts { get; set; }
        public List<Receipt> RefundedReceipts { get; private set; }
        public List<Receipt> VoidedReceipts { get; private set; }
        public List<LayawayHistoryPaymentInfo> RefundPayments { get; set; }

        private decimal totalPaymentsAmount { get; set; }

        # endregion

        # region Public Methods

        public void Calculate()
        {
            ScheduledPayments = new List<LayawayHistory>();
            RefundPayments = new List<LayawayHistoryPaymentInfo>();
            RefundedReceipts = new List<Receipt>();
            VoidedReceipts = new List<Receipt>();
            BuildInitialPayments();

            if (ScheduledPayments == null || ScheduledPayments.Count == 0)
            {
                return;
            }
            InsertPayments();
        }

        public decimal GetDelinquentAmount(DateTime currentShopDate)
        {
            decimal amountShouldHaveBeenPaid = (from p in ScheduledPayments
                                                where p.PaymentDueDate < currentShopDate && (currentShopDate - p.PaymentDueDate).Days > 0
                                                select p.PaymentAmountDue).Sum();

            decimal amountOfPaymentsPaid = (from p in ScheduledPayments
                                            where p.PaymentDueDate < currentShopDate
                                            select p.GetTotalPayments()).Sum();

            decimal delinquentAmount = amountOfPaymentsPaid >= amountShouldHaveBeenPaid ? 0M : amountShouldHaveBeenPaid - amountOfPaymentsPaid;
            return delinquentAmount;
        }

        public void AddTemporaryReceipt(decimal amount, ReceiptEventTypes receiptTypes, DateTime receiptDate)
        {
            Receipts.Add(new Receipt() { ReceiptNumber = int.MaxValue.ToString(), Amount = amount, Event = receiptTypes.ToString(), Date = receiptDate, RefTime = receiptDate });
        }

        public decimal GetBalanceOwed()
        {
            decimal amountShouldHaveBeenPaid = GetTotalLayawayAmount();

            decimal amountPaid = GetTotalPaid();

            return amountShouldHaveBeenPaid - amountPaid;
        }

        public LayawayHistory GetFirstUnpaidPayment(string currentDate = null)
        {
            if (ScheduledPayments == null)
            {
                return null;
            }
            DateTime cDate = DateTime.MaxValue;
            if (currentDate != null)
                cDate = Utilities.GetDateTimeValue(Utilities.GetDateTimeValue(currentDate).ToShortDateString());

            LayawayHistory firstUnpaidPayment;
            if (cDate != DateTime.MaxValue)
                firstUnpaidPayment = (from p in ScheduledPayments
                                      where p.IsPaid() == false
                                      && p.PaymentDueDate >= cDate
                                      orderby p.PaymentDueDate
                                      select p).FirstOrDefault();
            else
                firstUnpaidPayment = (from p in ScheduledPayments
                                      where p.IsPaid() == false
                                      orderby p.PaymentDueDate
                                      select p).FirstOrDefault();
            return firstUnpaidPayment;

        }

        public decimal GetFirstUnpaidPaymentBalance(string currentDate = null)
        {
            LayawayHistory currentScheduledPayment = GetFirstUnpaidPayment(currentDate);
            if (currentScheduledPayment == null)
            {
                return 0;
            }

            decimal firstUnpaidBalance = currentScheduledPayment.GetRemainingBalance();
            return firstUnpaidBalance;
        }

        public List<Receipt> GetPaymentReceipts(bool excludeVoided, bool excludeRefunded)
        {
            var receipts = Receipts.FindAll(r => r.Event == ReceiptEventTypes.LAY.ToString() || r.Event == ReceiptEventTypes.LAYPMT.ToString());
            if (excludeVoided)
            {
                receipts = receipts.FindAll(r => !VoidedReceipts.Contains(r));
            }

            if (excludeRefunded)
            {
                receipts = receipts.FindAll(r => !RefundedReceipts.Contains(r));
            }

            return receipts;
        }

        public decimal GetTotalDueNextPayment(DateTime currentShopDate)
        {
            decimal totalNextpayment = 0.0M;
            //Check if there is any payment that is due today if not only the delinquent amount needs to be paid

            totalNextpayment = GetFirstUnpaidPaymentBalance(currentShopDate.ToString()) + GetDelinquentAmount(currentShopDate);
            return totalNextpayment;
        }

        public decimal GetTotalPaid()
        {
            return (from p in ScheduledPayments
                    select p.GetTotalPayments()).Sum();
        }

        public decimal GetTotalRefunded()
        {
            return (from rp in RefundPayments
                    select rp.PaymentAmountMade).Sum();
        }

        public bool IsLayawayPaidOff()
        {
            decimal amountShouldHaveBeenPaid = GetTotalLayawayAmount();

            decimal amountPaid = GetTotalPaid();

            return amountPaid == amountShouldHaveBeenPaid;
        }

        # endregion

        # region Helper Methods

        private LayawayHistoryPaymentInfo AddPaymentInfo(LayawayHistory currentPayment, Receipt receipt, decimal paymentAmount, decimal totalAllocated, bool isRefund)
        {
            LayawayHistoryPaymentInfo paymentInfo = new LayawayHistoryPaymentInfo()
            {
                BalanceDue = totalPaymentsAmount - totalAllocated,
                PaymentAmountMade = paymentAmount,
                PaymentMadeOn = receipt.RefTime,
                Receipt = receipt,
                ReceiptNumber = receipt.ReceiptNumber,
                TenderDataDetails = GetTenderDataDetails(receipt, isRefund),
                Status = GetPaymentStatus(receipt)
            };

            currentPayment.Payments.Add(paymentInfo);
            return paymentInfo;
        }

        private List<TenderData> GetTenderDataDetails(Receipt receipt, bool isRefund)
        {
            if (Layaway.TenderDataDetails == null)
            {
                return new List<TenderData>();
            }

            List<TenderData> tenderDetails;
            if (isRefund)
            {
                tenderDetails = Layaway.TenderDataDetails.FindAll(t => t.ReceiptNumber == receipt.ReceiptNumber
                    && (t.TenderType.Contains("OUT") || t.TenderType.Equals("RDEBIT")));
            }
            else
            {
                tenderDetails = Layaway.TenderDataDetails.FindAll(t => t.ReceiptNumber == receipt.ReceiptNumber
                    && !t.TenderType.Contains("OUT") && !t.TenderType.Equals("RDEBIT"));
            }

            Dictionary<string, TenderData> groupedTenderDetails = new Dictionary<string, TenderData>();
            foreach (TenderData td in tenderDetails)
            {
                string key = td.MethodOfPmt + td.TenderType + td.ReversalInfo;
                if (!groupedTenderDetails.ContainsKey(key))
                {
                    groupedTenderDetails.Add(key, new TenderData() { MethodOfPmt = td.MethodOfPmt, ReceiptNumber = td.ReceiptNumber, TenderAmount = 0, TenderType = td.TenderType, ReversalInfo = td.ReversalInfo });
                }

                TenderData td2 = groupedTenderDetails[key];
                td2.TenderAmount += td.TenderAmount;
                groupedTenderDetails[key] = td2;
            }

            return new List<TenderData>(groupedTenderDetails.Values.ToArray());
        }

        private bool AddRefundReceiptIfNeeded(Receipt receipt, LayawayHistory currentPayment, decimal totalAmountAllocated)
        {
            if (!string.IsNullOrWhiteSpace(receipt.Event) && receipt.Event == ReceiptEventTypes.LAYPMT.ToString())
            {
                var refundedReceipt = Receipts.Find(r => r.Event == ReceiptEventTypes.LAYREF.ToString() && receipt.ReferenceReceiptNumber == r.ReceiptDetailNumber);
                if (receipt.ReferenceReceiptNumber == refundedReceipt.ReceiptDetailNumber && !string.IsNullOrWhiteSpace(receipt.ReferenceReceiptNumber))
                {
                    var payment = AddPaymentInfo(currentPayment, receipt, receipt.Amount, totalAmountAllocated + receipt.Amount, false);
                    var refundPayment = AddPaymentInfo(currentPayment, refundedReceipt, -receipt.Amount, totalAmountAllocated, true);
                    payment.AssociatedPaymentInfo = refundPayment;
                    refundPayment.AssociatedPaymentInfo = payment;
                    refundPayment.IsRefund = true;
                    RefundPayments.Add(refundPayment);
                    RefundedReceipts.Add(receipt);
                    return true;
                }
            }
            else if (!string.IsNullOrWhiteSpace(receipt.Event) && receipt.Event == ReceiptEventTypes.LAY.ToString())
            {
                var refundedReceipt = Receipts.Find(r => r.Event == ReceiptEventTypes.LAYDOWNREF.ToString() && receipt.ReferenceReceiptNumber == r.ReceiptDetailNumber);
                if (receipt.ReferenceReceiptNumber == refundedReceipt.ReceiptDetailNumber && !string.IsNullOrWhiteSpace(receipt.ReferenceReceiptNumber))
                {
                    var payment = AddPaymentInfo(currentPayment, receipt, receipt.Amount, totalAmountAllocated + receipt.Amount, false);
                    var refundPayment = AddPaymentInfo(currentPayment, refundedReceipt, -receipt.Amount, totalAmountAllocated, true);
                    payment.AssociatedPaymentInfo = refundPayment;
                    refundPayment.AssociatedPaymentInfo = payment;
                    refundPayment.IsRefund = true;
                    RefundPayments.Add(refundPayment);
                    RefundedReceipts.Add(receipt);
                    return true;
                }
            }
            else if (receipt.Event == ReceiptEventTypes.LAYREF.ToString())
            {
                return true;
            }

            return false;
        }
         
        private void BuildInitialPayments()
        {
            decimal serviceFee = Layaway.Fees.FindAll(f => f.FeeType == FeeTypes.SERVICE).Sum(f => f.Value);
//            ScheduledPayments.Add(new LayawayHistory(Layaway.DownPayment + serviceFee, Layaway.DateMade));
            ScheduledPayments.Add(new LayawayHistory(Layaway.DownPayment, Layaway.DateMade));
            decimal totalPaymentsAllocated = Layaway.DownPayment + serviceFee;

            decimal monthlyPayment = Layaway.MonthlyPayment;

            for (int i = 0; i < Layaway.NumberOfPayments - 1; i++)
            {
                totalPaymentsAllocated += monthlyPayment;
                ScheduledPayments.Add(new LayawayHistory(monthlyPayment, LayawayPaymentCalculator.CalculatePaymentDate(Layaway.FirstPayment, i)));
            }

            ScheduledPayments.Add(new LayawayHistory((Layaway.Amount + serviceFee + Layaway.SalesTaxAmount) - totalPaymentsAllocated, LayawayPaymentCalculator.CalculatePaymentDate(Layaway.FirstPayment, Layaway.NumberOfPayments - 1)));

            totalPaymentsAmount = (from p in ScheduledPayments
                                   select p.PaymentAmountDue).Sum();
        }

        private LayawayHistory GetNextPayment(int paymentsToSkip)
        {
            return ScheduledPayments.Skip(paymentsToSkip).FirstOrDefault();
        }

        private string GetPaymentStatus(Receipt receipt)
        {
            if (receipt.Event == ReceiptEventTypes.LAY.ToString() || receipt.Event == ReceiptEventTypes.LAYPMT.ToString())
            {
                return "Payment";
            }
            else if (receipt.Event == ReceiptEventTypes.LAYREF.ToString())
            {
                return "Refund";
            }

            return receipt.Event;
        }

        private decimal GetTotalLayawayAmount()
        {
            return (from p in ScheduledPayments
                    select p.PaymentAmountDue).Sum();
        }

        private void InsertPayments()
        {
            decimal totalAmountAllocated = 0;
            decimal amountToAllocate = 0;
            int paymentsToSkip = 0;
            LayawayHistory currentPayment = GetNextPayment(paymentsToSkip);

            //var receipts = (from r in Receipts
            //                where r.Event != ReceiptEventTypes.LAY.ToString()
            //                select r).OrderBy(rn => rn.ReceiptNumber).ThenBy(rt => rt.RefTime);

            var receipts = (from r in Receipts
                            where r.Event != ReceiptEventTypes.LAY.ToString()
                            select r).OrderBy(rt => rt.RefTime);

            var downPaymentReceipt = (from r in Receipts
                                      where r.Event == ReceiptEventTypes.LAY.ToString()
                                      select r).FirstOrDefault();

            totalAmountAllocated += downPaymentReceipt.Amount;
            
            if (!AddRefundReceiptIfNeeded(downPaymentReceipt, currentPayment, totalAmountAllocated))
            {
                AddPaymentInfo(currentPayment, downPaymentReceipt, currentPayment.PaymentAmountDue, totalAmountAllocated, false);
            }
            currentPayment = GetNextPayment(++paymentsToSkip);

            foreach (Receipt receipt in receipts)
            {
                if (string.IsNullOrEmpty(receipt.ReceiptNumber))
                {
                    continue;
                }

                if (AddRefundReceiptIfNeeded(receipt, currentPayment, totalAmountAllocated))
                {
                    continue;
                }

                if (IsReceiptVoidOrVoided(receipt))
                {
                    if (receipt.Event == ReceiptEventTypes.LAY.ToString() || receipt.Event == ReceiptEventTypes.LAYPMT.ToString())
                    {
                        VoidedReceipts.Add(receipt);
                    }
                    continue;
                }

                decimal paymentAmount = (receipt.Event == ReceiptEventTypes.LAYPMT.ToString()) ? receipt.Amount : -receipt.Amount;
                amountToAllocate += paymentAmount;

                while (currentPayment != null && amountToAllocate >= currentPayment.GetRemainingBalance())
                {
                    decimal remainingBalance = currentPayment.GetRemainingBalance();
                    totalAmountAllocated += remainingBalance;
                    AddPaymentInfo(currentPayment, receipt, remainingBalance, totalAmountAllocated, false);
                    amountToAllocate -= remainingBalance;
                    currentPayment = GetNextPayment(++paymentsToSkip);
                }

                if (currentPayment != null)
                {
                    if (amountToAllocate > 0 && amountToAllocate < currentPayment.GetRemainingBalance())
                    {
                        totalAmountAllocated += amountToAllocate;
                        AddPaymentInfo(currentPayment, receipt, amountToAllocate, totalAmountAllocated, false);
                        amountToAllocate = 0;
                    }
                }
            }
        }

        private bool IsReceiptVoidOrVoided(Receipt receipt)
        {
            if (!string.IsNullOrWhiteSpace(receipt.ReferenceReceiptNumber))
            {
                return true;
            }
            else if (receipt.Event == ReceiptEventTypes.LAYPMT.ToString())
            {
                var voidedReceipt = Receipts.Find(r => r.Event == ReceiptEventTypes.VLAYPMT.ToString() && receipt.ReferenceReceiptNumber == r.ReceiptDetailNumber);
                if (receipt.ReferenceReceiptNumber == voidedReceipt.ReceiptDetailNumber && !string.IsNullOrWhiteSpace(receipt.ReferenceReceiptNumber))
                {
                    return true;
                }
            }
            else if (receipt.Event == ReceiptEventTypes.VLAYPMT.ToString() || receipt.Event == ReceiptEventTypes.VFORF.ToString())
            {
                return true;
            }

            return false;
        }

        # endregion
    }
}
