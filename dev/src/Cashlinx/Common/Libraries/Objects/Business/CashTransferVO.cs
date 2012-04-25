using System.Collections.Generic;

namespace Common.Libraries.Objects.Business
{
    public class CashTransferVO
    {
        public SiteId SourceShopInfo
        {
            get;
            set;
        }
        public SiteId DestinationShopInfo
        {
            get;
            set;
        }
        public string Transporter
        {
            get;
            set;
        }
        public string DepositBagNo
        {
            get;
            set;
        }
        public string SourceComment
        {
            get;
            set;
        }
        public string DestinationComment
        {
            get;
            set;
        }
        public decimal TransferAmount
        {
            get;
            set;
        }
        public string TransferStatus
        {
            get;
            set;
        }
        public string TransferType
        {
            get;
            set;
        }
        public string RejectReason
        {
            get;
            set;
        }
        public string TransferDate
        {
            get;
            set;
        }
        public List<DenominationVO> TransferDenominations
        {
            get;
            set;
        }
        public int TransferNumber
        {
            get;
            set;
        }
        public string TransferId
        {
            get;
            set;
        }
        public string SourceDrawerName
        {
            get;
            set;
        }
        public string DestinationDrawerName
        {
            get;
            set;
        }
        public string SourceEmployee
        {
            get;
            set;
        }
        public string SourceEmployeeNumber
        {
            get;
            set;
        }
        public string SourceEmployeeName
        {
            get;
            set;
        }
        public string DestinationEmployeeNumber
        {
            get;
            set;
        }
        public string DestinationEmployeeName
        {
            get;
            set;
        }
        public string DestinationEmployee
        {
            get;
            set;
        }
        public string BankName
        {
            get;
            set;
        }
        public string CheckNumber
        {
            get;
            set;
        }
        public string BankAccountNumber
        {
            get;
            set;
        }



    }

}
