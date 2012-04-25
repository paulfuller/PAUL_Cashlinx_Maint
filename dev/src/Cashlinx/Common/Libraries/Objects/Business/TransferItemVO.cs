using System;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Business
{
    public class TransferItemVO
    {
        public string AcctNumber { get; set; }
        public string ClassCode { get; set; }
        public string CreatedBy { get; set; }
        public string CustomerNumber { get; set; }
        public string GunNumber { get; set; }
        public string GunType { get; set; }
        public string ICN { get; set; }
        public string ICNQty { get; set; }
        public decimal ItemCost { get; set; }
        public decimal RetailPrice { get; set; }
        public string ItemDescription { get; set; }
        public ProductStatus ItemStatus { get; set; }
        public int MdseRecordChange { get; set; }
        public DateTime MdseRecordDate { get; set; }
        public string MdseRecordDesc { get; set; }
        public string MdseRecordTime { get; set; }
        public string MdseRecordType { get; set; }
        public string MdseRecordUser { get; set; }
        public decimal PfiAmount { get; set; }
        public string StoreNumber { get; set; }
        public TransferVO Transfer { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransferNumber { get; set; }
        public TransferTypes TransferType { get; set; }
        public int RefurbNumber { get; set; }
        public string RejectedTransfers { get; set; }
        public int TotalItems { get; set; }
        public string TotalCost { get; set; }
        public string Status { get; set; }

        public string GetTransferTypeDescription()
        {
            return TransferVO.GetTransferTypeDescription(TransferType);
        }
    }
}

