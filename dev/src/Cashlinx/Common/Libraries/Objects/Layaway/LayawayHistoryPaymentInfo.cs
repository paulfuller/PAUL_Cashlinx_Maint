using System;
using System.Collections.Generic;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Layaway
{
    public class LayawayHistoryPaymentInfo
    {
        public decimal BalanceDue { get; set; }
        public bool IsRefund { get; set; }
        public decimal PaymentAmountMade { get; set; }
        public DateTime PaymentMadeOn { get; set; }
        public Receipt Receipt { get; set; }
        public string ReceiptNumber { get; set; }
        public LayawayHistoryPaymentInfo AssociatedPaymentInfo { get; set; }
        public List<TenderData> TenderDataDetails { get; set; }
        public string Status { get; set; }

        public List<LayawayRefundTenderInfo> GetRefundTenderBreakdown()
        {
            var tenderInfo = new List<LayawayRefundTenderInfo>();

            foreach (TenderData td in TenderDataDetails)
            {
                var info = new LayawayRefundTenderInfo
                           {
                               AmountPaid = td.TenderAmount,
                               MethodOfPmt = td.MethodOfPmt,
                               ReceiptNumber = td.ReceiptNumber,
                               TenderDescription = GetTenderDescription(td),
                               TenderType = td.TenderType
                           };
                tenderInfo.Add(info);
            }

            if (AssociatedPaymentInfo != null)
            {
                foreach (TenderData td in AssociatedPaymentInfo.TenderDataDetails)
                {
                    int iDx = tenderInfo.FindIndex(ti => ti.MethodOfPmt.Equals(td.MethodOfPmt, StringComparison.InvariantCultureIgnoreCase));

                    LayawayRefundTenderInfo info;
                    if (iDx >= 0)
                    {
                        info = tenderInfo[iDx];
                    }
                    else
                    {
                        info = new LayawayRefundTenderInfo
                               {
                                   MethodOfPmt = td.MethodOfPmt,
                                   ReceiptNumber = td.ReceiptNumber,
                                   TenderType = td.TenderType,
                                   TenderDescription = GetTenderDescription(td)
                               };
                        tenderInfo.Add(info);
                    }
                    info.AmountReturned = td.TenderAmount;
                }
            }

            return tenderInfo;
        }

        public bool IsRefunded()
        {
            if (AssociatedPaymentInfo == null || IsRefund)
            {
                return false;
            }

            return Receipt.Amount <= Math.Abs(AssociatedPaymentInfo.PaymentAmountMade);
        }

        private string GetTenderDescription(TenderData td)
        {
            return Commons.GetTenderDescription(td);
        }
    }
}
