using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Layaway
{
    [Serializable]
    public class LayawayVO : SaleVO
    {
        public LayawayVO()
        {
            Payments = new List<LayawayPayment>();
        }

        public LayawayVO(LayawayVO layaway)
        {
            try
            {
                foreach (PropertyInfo pi in typeof(LayawayVO).GetProperties())
                    GetType().GetProperty(pi.Name).SetValue
                       (this, pi.GetValue(layaway, null), null);
            }
            catch (Exception ex)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, null, ex.Message);
            }
        }

        public LayawayVO(SaleVO saleVO)
        {
            try
            {
                foreach (PropertyInfo pi in typeof(SaleVO).GetProperties())
                    GetType().GetProperty(pi.Name).SetValue
                       (this, pi.GetValue(saleVO, null), null);
            }
            catch (Exception ex)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, null, ex.Message);
            }
        }

        public decimal DownPayment { get; set; }
        public decimal MonthlyPayment { get; set; }
        public int NumberOfPayments { get; set; }
        public DateTime FirstPayment { get; set; }
        public DateTime LastPayment { get; set; }
        public DateTime NextPayment { get; set; }
        public decimal NextDueAmount { get; set; }
        public DateTime ForfeitureNote { get; set; }
        public bool PrintNotice { get; set; }
        public string Comments { get; set; }
        public CustomerVO CustomerInfo { get; set; }
        public List<LayawayPayment> Payments { get; set; }

        public decimal GetAmountPaid()
        {
            var pmtReceipts = GetPaymentReceipts().FindAll(i=>string.IsNullOrEmpty(i.ReferenceReceiptNumber));

            var amountPaid = pmtReceipts.Sum(p => p.Amount);

            //SR 12/02/2011 If any coupon was tendered at the time of layaway down payment
            //subtract that amount from the amount paid in

            var tenderCouponAmt = (from tenderData in TenderDataDetails
                               where tenderData.TenderType == "CDIN"
                               select tenderData).Sum(t=>t.TenderAmount);

            decimal tenderCouponAmount = Utilities.GetDecimalValue(tenderCouponAmt, 0);

            return amountPaid-tenderCouponAmount;
        }

        public List<Receipt> GetPaymentReceipts()
        {
            return (from receipt in Receipts
                    where receipt.Event == ReceiptEventTypes.LAY.ToString() || receipt.Event == ReceiptEventTypes.LAYPMT.ToString()
                    select receipt).OrderBy(r => r.RefTime).ToList();
        }

        public Receipt GetLastPaymentReceipt()
        {
            List<Receipt> paymentReceipts = GetPaymentReceipts();
            if (paymentReceipts.Count == 0)
            {
                return new Receipt();
            }

            return GetPaymentReceipts().Last();
        }

        public decimal GetLayawayFees()
        {
            return Fees.Where(fee => fee.FeeType == FeeTypes.SERVICE || fee.FeeType == FeeTypes.INTEREST).Sum(fee => fee.OriginalAmount);

        }
    }
}
