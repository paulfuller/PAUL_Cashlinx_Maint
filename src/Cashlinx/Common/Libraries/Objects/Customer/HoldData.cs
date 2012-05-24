/********************************************************************
* PawnObjects.VO.Customer
* HoldsData
* Class to denote the transaction data selected to be put on hold or release
* Sreelatha Rengarajan 8/6/2009 Updated
*******************************************************************/

using System;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Pawn;

namespace Common.Libraries.Objects.Customer
{
    public class HoldData:PawnLoan
    {
        public const string CUSTOMER_HOLD = "CUSTHOLD";
        public const string POLICE_HOLD = "POLICEHOLD";
        public const string BANKRUPTCY_HOLD = "BKHOLD";
        public const string ACTIVE_HOLDS = "ACT";
        public const string RELEASE_HOLDS = "REL";
        public const string POLICE_CUSTOMER_HOLD = "POL_CUST";
        public const string POLICE_BANKRUPTCY_HOLD = "POL_BK";
        public const string PURCHASES_POLICE_HOLD = "PURPOLICEHOLD";

        public string HoldType
        {
            get;
            set;
        }


        public string StatusCode
        {
            get;
            set;
        }

 

        public string HoldStatus
        {
            get;
            set;
        }

        public string UserId
        {
            get;
            set;
        }

        public string HoldComment
        {
            get;
            set;
        }

        public string RefType
        {
            get;
            set;
        }


        public DateTime ReleaseDate
        {
            get;
            set;
        }

        public DateTime TransactionDate
        {
            get;
            set;
        }
        public DateTime HoldDate
        {
            get;
            set;
        }
        public int HoldTypeId
        {
            get;
            set;
        }
        public PoliceInfo PoliceInformation
        {
            get;
            set;
        }

        // 03-09-2012 - Tré G. --> Added Restitution information for Release to Claimant report.
        public bool RestitutionPaid { get; set; }
        public double RestitutionAmount { get; set; }
        


        public HoldData()
        {
            this.HoldType = "";
            this.HoldStatus = "";
            this.HoldComment = "";
            this.RefType = "";
            this.StatusCode = "";
            this.UserId = "";
            this.ReleaseDate = DateTime.MaxValue;
            this.TransactionDate = DateTime.MaxValue;
            this.PrevTicketNumber = 0;
            this.HoldDate = DateTime.MaxValue;
            this.HoldTypeId = 0;
            
        }

    }
}
