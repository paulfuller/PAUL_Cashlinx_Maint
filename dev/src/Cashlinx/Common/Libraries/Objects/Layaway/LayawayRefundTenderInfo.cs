
namespace Common.Libraries.Objects.Layaway
{
    public class LayawayRefundTenderInfo
    {
        public decimal AmountPaid { get; set; }
        public decimal AmountReturned { get; set; }
        public string MethodOfPmt { get; set; }
        public string ReceiptNumber { get; set; }
        public string TenderDescription { get; set; }
        public string TenderType { get; set; }
    }
}
