using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Libraries.Objects.PDLoan
{
    [Serializable]
    public class PDLoanHistoryList
    {
        public DateTime Date
        {
            get;
            set;
        }

        public string EventType
        {
            get;
            set;
        }

        public string Details
        {
            get;
            set;
        }

        public decimal Amount
        {
            get;
            set;
        }

        public string Source
        {
            get;
            set;
        }

        public string Receipt
        {
            get;
            set;
        }

    }
}
