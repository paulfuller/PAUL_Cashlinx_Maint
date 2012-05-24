using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports
{
    public class IndianaPartialPaymentContext
    {
        public string FilePath { get; set; }
        #region Customer Contact information
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerZip { get; set; }
        public string CustomerPhone { get; set; }
        #endregion

        #region Store Information
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string StoreCity { get; set; }
        public string StoreState { get; set; }
        public string StoreZip { get; set; }
        public string StoreNumber { get; set; }
        #endregion

        #region Ticket Information
        public int TicketNumber { get; set; }
        public string EmployeeNumber { get; set; }
        public double PrincipalReduction { get; set; }
        public double Interest { get; set; }
        public double ServiceFees { get; set; }
        public double OtherCharges { get; set; }
        public double TotalPaymentAmount
        {
            get
            { return PrincipalReduction + Interest + ServiceFees + OtherCharges; }
        }
        public DateTime PrintDateTime { get; set; }
        public string ItemDescription { get; set; }
        #endregion
    }
}
