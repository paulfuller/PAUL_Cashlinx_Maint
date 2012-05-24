using System;
using System.Collections.Generic;

namespace Common.Libraries.Objects.Layaway
{
    public class LayawayReportObject
    {
        #region Public Fields
        public System.Drawing.Bitmap ReportImage;
        #endregion

        #region Public Properties
        public string FormName { set; get; }
        public string ReportTitle { set; get; }
        public string ReportSort { set; get; }
        public string ReportDetail { set; get; }
        public string MerchandiseLocation { set; get; }
        public string ReportStore { set; get; }
        public string ReportEmployee { set; get; }
        public string CustomerFirstName { set; get; }
        public string CustomerLastName { set; get; }
        public string CustomerName { set; get; }
        public string ContactNumber { set; get; }
        public string ReportStoreDesc1 { set; get; }
        public string ReportStoreDesc2 { set; get; }
        public string ReportStoreDesc3 { set; get; }
        public int ReportNumber { set; get; }
        public string ReportTempFile { set; get; }
        public string ReportTempFileFullName { set; get; }
        public string ReportSortSQL { set; get; }
        public string ReportFilter { set; get; }
        public string ReportError { set; get; }
        public Int16 ReportErrorLevel { set; get; }
        public List<object> ReportParms { set; get; }
        public LayawayVO CurrentLayaway { get; set; }
        public int LayawayNumber { get; set; }
        public decimal RestockingFee { get; set; }
        public LayawayReportObject.LayawayHistoryAndScheduleMain LayawayHistoryAndScheduleMainData { get; set; }
        public List<LayawayVO> ForefeitedLayawaysListingsList { get; set; }
        public List<LayawayVO> LayawayPickingSlipList { get; set; }
        public LayawayVO LayawayPickingSlip { get; set; }
        public LayawayVO TerminatedLayaway { get; set; }
        public List<LayawayVO> TerminatedLayawaysListingsList { get; set; }
        public List<LayawayVO> TerminatedLayawayPickingSlipList { get; set; }
        #endregion

        #region Constructor
        public LayawayReportObject()
        {
            ReportTitle = string.Empty;
            ReportSort = string.Empty;
            ReportDetail = string.Empty;
            ReportStore = string.Empty;
            ReportStoreDesc1 = string.Empty;
            ReportStoreDesc2 = string.Empty;
            ReportStoreDesc3 = string.Empty;
            ReportNumber = 0;
            ReportTempFile = string.Empty;
            ReportTempFileFullName = string.Empty;
            CustomerName = string.Empty;
            ContactNumber = string.Empty;
            ReportSortSQL = string.Empty;
            ReportFilter = string.Empty;
            ReportError = string.Empty;
            ReportErrorLevel = 0;
            ReportParms = new List<object>();
            ReportImage = null;
        }
        #endregion

        #region Public Methods
        public void CreateTemporaryFullName()
        {
            if (string.IsNullOrEmpty(ReportTempFileFullName))
                ReportTempFileFullName = ReportTempFile + "tmp\\LayawayReport" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";
        }
        #endregion

        #region Helper Classes
        //report 223 Detail Inventory report
        #region LayawayHistoryAndSchedule Classes
        public struct LayawayHistoryAndScheduleMain
        {
            public List<LayawaySchedule> LayawayScheduleList;
            public LayawayVO Layaway { get; set; }
            public decimal AmountOutstanding { get; set; }
        }

        public struct LayawaySchedule
        {
            public List<LayawayScheduleDetails> LayawayScheduleDetailsList { get; set; }
            public DateTime PaymentDateDue { get; set; }
            public decimal PaymentAmountDue { get; set; }
        }

        public class LayawayScheduleDetails
        {
            public DateTime PaymentDateDue { get; set; }
            public decimal PaymentAmountDue { get; set; }
            public DateTime PaymentMadeOn { get; set; }
            public decimal PaymentAmountMade { get; set; }
            public decimal BalanceDue { get; set; }
            public string PaymentType { get; set; }
            public string ReferenceNumber { get; set; }
            public string Status { get; set; }
            public string ReceiptNumber { get; set; }
            public int GroupDataBy { get; set; }
        }
        #endregion
        #endregion
      
    }
}
