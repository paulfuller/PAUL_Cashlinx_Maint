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
        public DateTime StatusDate
        {
            get;
            set;
        }

        public PDLoanDetails GetPDLoanDetails
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
        /// <summary>
        /// 
        /// </summary>
        public PDLoan()
        {
            GetPDLoanXPPScheduleList = new List<PDLoanXPPScheduleList>();
            //GetPDLoanEvents = new List<PDLoanEvents>();
            GetPDLoanHistoryList = new List<PDLoanHistoryList>();
        }
    }
}
