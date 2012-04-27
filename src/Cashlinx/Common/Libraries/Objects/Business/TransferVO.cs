using System;
using System.Collections.Generic;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Business
{
    public class TransferVO
    {
        public TransferVO()
        {
            Items = new List<TransferItemVO>();
        }

        public decimal Amount { get; set; }
        public string Carrier { get; set; }
        public string DestinationStoreNumber { get; set; }
        public List<TransferItemVO> Items { get; set; }
        public int NumberOfItems { get; set; }
        public string OriginalStoreNumber { get; set; }
        public string RejectReason { get; set; }
        public TransferStatus Status { get; set; }
        public DateTime StatusDate { get; set; }
        public SiteId StoreInfo { get; set; }
        public string StoreNickName { get; set; }
        public TransferTempStatus TempStatus { get; set; }
        public int TransferTicketNumber { get; set; }
        public TransferSource TransferSource { get; set; }
        public TransferTypes TransferType { get; set; }

        public string GetTransferTypeDatabaseValue()
        {
            return TransferType == TransferTypes.STORETOSTORE ? string.Empty : TransferType.ToString();
        }

        public string GetTransferInStatusDescription()
        {
            if (Status == TransferStatus.TI)
            {
                return "TI";
            }
            else if (Status == TransferStatus.TO)
            {
                if (TempStatus == TransferTempStatus.REJCT)
                {
                    return "Rejected";
                }
                return "Created";
            }
            else if (Status == TransferStatus.VO)
            {
                return "Voided";
            }
            else
            {
                return "Unknown";
            }
        }

        public string GetTransferTypeDescription()
        {
            return GetTransferTypeDescription(TransferType);
        }

        public static string GetTransferTypeDescription(TransferTypes type)
        {
            if (type == TransferTypes.STORETOSTORE)
            {
                return "SHOP TO SHOP";
            }
            else if (type == TransferTypes.JORET)
            {
                return "REFURB";
            }
            return type.ToString();
        }
    }
}
