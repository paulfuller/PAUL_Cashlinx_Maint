using System;
using System.Collections.Generic;
using System.Windows.Documents;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Shared;

namespace Support.Libraries.Objects.PDLoan
{
    [Serializable]
    public class PDLoan
    {
        private bool sqldataretrieved;
        private int pdlstate;

        public PDLoan()
        {
            GetPDLoanXPPScheduleList = new List<PDLoanXPPScheduleList>();
            //GetPDLoanEvents = new List<PDLoanEvents>();
            GetPDLoanHistoryList = new List<PDLoanHistoryList>();
            PdlState = 0;
        }

        public bool SqlDataRetrieved { get; set; }
        public int PdlState { get; set; }

        public string LoanApplicationId
        {
            get;
            set;
        }
        public string PDLLoanNumber
        {
            get;
            set;
        }
        public string Type
        {
            get;
            set;
        }
        public string Status
        {
            get;
            set;
        }
        public string open_closed
        {
            get;
            set;
        }
        public DateTime StatusDate
        {
            get;
            set;
        }
        public string Decline_Reason
        {
            get;
            set;
        }
        public string Decline_Description
        {
            get;
            set;
        }
        public PDLoanDetails GetPDLoanDetails
        {
            get;
            set;
        }

        public PDLoanOtherDetails GetPDLoanOtherDetails
        {
            get;
            set;
        }

        public List<PDLoanXPPScheduleList> GetPDLoanXPPScheduleList
        {
            get;
            set;
        }

        //public List<PDLoanEvents> GetPDLoanEvents
        //{
        //    get;
        //    set;
        //}

        public List<PDLoanHistoryList> GetPDLoanHistoryList
        {
            get;
            set;
        }

    }
}
