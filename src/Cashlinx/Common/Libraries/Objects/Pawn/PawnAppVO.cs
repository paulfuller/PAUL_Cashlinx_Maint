using System;

namespace Common.Libraries.Objects.Pawn
{
    public class PawnAppVO
    {
        public string CustomerNumber
        {
            get;
            set;
        }
        public string StoreNumber
        {
            get;
            set;
        }
        public long PawnAppID
        {
            get;
            set;
        }
        public string Clothing
        {
            get;
            set;
        }
        public string Comments
        {
            get;
            set;
        }
        public string PawnAppCustIDNumber
        {
            get;
            set;
        }
        public DateTime PawnAppCustIDIssueDate
        {
            get;
            set;
        }
        public string PawnAppCustIDIssuer
        {
            get;
            set;
        }
        public string PawnAppCustIDType
        {
            get;
            set;
        }
        public DateTime PawnAppCustIDExpDate
        {
            get;
            set;
        }
        public string PawnAppStatus
        {
            get;
            set;
        }
        public DateTime PawnAppCreatedDate
        {
            get;
            set;
        }


        public PawnAppVO()
        {
            this.CustomerNumber = "";
            this.StoreNumber = "";
            this.PawnAppStatus = "";
            this.PawnAppID = 0;
            this.Clothing = "";
            this.Comments = "";
            this.PawnAppCreatedDate = DateTime.MaxValue;
            this.PawnAppCustIDExpDate = DateTime.MaxValue;
            this.PawnAppCustIDIssueDate = DateTime.MaxValue;
            this.PawnAppCustIDIssuer = "";
            this.PawnAppCustIDNumber = "";
            this.PawnAppCustIDType = "";
            
        }


        

    }
}
