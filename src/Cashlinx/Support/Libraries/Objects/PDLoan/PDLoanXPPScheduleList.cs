using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Libraries.Objects.PDLoan
{
    [Serializable]
    public class PDLoanXPPScheduleList
    {
        //public string xppLineItem { get; set; }

        public string xppPaymentSeqNumber
        {
            get;
            set;
        }

        public string xppPaymentNumber
        {
            get;
            set;
        }

        public DateTime xppDate
        {
            get;
            set;
        }

        public decimal xppAmount
        {
            get;
            set;
        }

        public string xppPDF
        {
            get;
            set;
        }

    }
}
