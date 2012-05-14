using System;

namespace Reports
{
    public class LostTicketAffidavitContext
    {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCityStateZip { get; set; }
        public string CustomerPhone { get; set; }
        public string EmployeeNumber { get; set; }
        public string MerchandiseDescription { get; set; }
        public string OutputPath { get; set; }
        public string StoreAddress { get; set; }
        public string StoreName { get; set; }
        public string StoreNumber { get; set; }
        public DateTime LoanDateMade { get; set; }
        public string ReasonMissing { get; set; }
        public int TicketNumber { get; set; }
        public DateTime Time { get; set; }
    }
}
