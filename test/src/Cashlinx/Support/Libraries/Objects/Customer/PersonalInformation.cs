using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Libraries.Objects.Customer
{
    class PersonalInformation
    {
        public enum personalinformation
        {
            SHOP = 0,
            TRANSACTIONDATE = 1,
            BEFORECHANGE = 2,
            AFTERCHANGE = 3,
            USERID = 4
        }


        private string shop;
        private DateTime transactiondate;
        private string beforechange;
        private string afterchange;
        private string userid;

        public string Shop { get; set; }
        public DateTime TransactionDate { get; set; }
        public string BeforeChange { get; set; }
        public string AfterChange { get; set; }
        public string UserId { get; set; }

    }
}
