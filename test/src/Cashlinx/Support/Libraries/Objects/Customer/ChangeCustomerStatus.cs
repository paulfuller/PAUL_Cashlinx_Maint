using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Libraries.Objects.Customer
{
    class ChangeCustomerStatus
    {
                public enum CustomerChangeStatus
        {
            CurrentCustomerStatus = 0,
            ChangeStatusTo,
            CurrentReasonCode,
            ChangeReasonTo,

        }


        private string currentcustomerstatus;
        private string changestatusto;
        private string currentreasoncode;
        private string changereasonto;

         /*__________________________________________________________________________________________*/
        public string CurrentCustomerStatus { get; set; }
        public string ChangeStatusTo { get; set; }
        public string CurrentReasonCode { get; set; }
        public string ChangeReasonTo { get; set; }

    }
}
