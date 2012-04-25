using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Libraries.Objects.Audit
{
    #region Enumerations
    public enum EnumTrakkerUploadReportCategories
    {
        TempICNsSinceLastInventory,
        MissingItems,
        NXTICNsSinceLastInventory,
        UnexpectedItems,
        DuplicateScans
    }

    public enum EnumPostAuditReportCategories
    {
        InventoryTotalsCountedByStatus,
        ChargeOff,
        Reactivation,
        ChargeOn,
        TempICNReconciliationPhysical,
        TempICNReconciliationCost
    }
    #endregion

    public class AuditReportsObject
    {
        #region Public Fields
        public string ReportTitle { set; get; }
        public string ReportStore { set; get; }
        public int ReportNumber { set; get; }
        public DateTime InventoryAuditDate { set; get; }
        public string StoreNumber { set; get; }
        public StringBuilder InventoryQuestionsAdditionalComments { get; set; }
        public string ReportSort { set; get; }
        public string ReportDetail { set; get; }
        public string MerchandiseLocation { set; get; }
        
        public string ReportStoreDesc { set; get; }
       
        public string RunDate
        {
            set;
            get;
        }

        public InventoryAuditVO ActiveAudit { get; set; }
        public string ReportTempFile { get; set; }
        public string ReportTempFileFullName { set; get; }
        public string ReportSortSQL { set; get; }
        public string ReportFilter { set; get; }
        public string ReportError { set; get; }
        public string FormName { get; set; }
        public Int16 ReportErrorLevel { set; get; }
        public List<object> ReportParms { set; get; }
        public System.Drawing.Bitmap ReportImage;
        public List<InventorySummaryHistoryField> ListInventorySummaryHistoryField { set; get; }
        public List<InventorySummaryChargeOffsField> ListInventorySummaryChargeOffsField { set; get; }
        public List<InventorySummaryChargeOffsField> ListInventorySummaryChargeOffsFieldCACC { set; get; }
        public List<PostAuditField> ListPostAuditField { set; get; }
        public List<PostAuditInventoryTotalsField> ListPostAuditInventoryTotalsField { set; get; }
        public PostAuditTempICNReconciliationField PostAuditTempICNReconciliation { set; get; }
        public List<TrakkerUploadReportSinceLastInventory> ListTrakkerUploadReportField { set; get; }
        public StringBuilder StringbuilderInvSummTermEmployees { get; set; }
        public StringBuilder StringbuilderInvSummEmployeesPresent { get; set; }
        public List<InventoryQuestion> ListInventoryQuestions { get; set; }
        #endregion

        
        #region Trakker Upload
        public class TrakkerUploadReportSinceLastInventory
        {
            public string ICN;
            public string MerchandiseDescription;
            public decimal Cost;
            public decimal Retail;
            public string Status;
            public int Category;
        }

        public class TrakkerUploadReportMissingItems : TrakkerUploadReportSinceLastInventory
        {
            public string PostAuditStatus;
        }

        public class TrakkerUploadReportNXTsSinceLastInventory : TrakkerUploadReportSinceLastInventory
        {
            public string TransactionNumber;
        }


        public class TrakkerUploadReportUnexpectedItems : TrakkerUploadReportMissingItems
        {
            public int TrakID;
            public int ScanSequence;
            public string AuditIndicator;
            public string TrakFlag;
        }

        public class TrakkerUploadReportDuplicateScans : TrakkerUploadReportUnexpectedItems
        {
            public string AuditLocation;
        }
        #endregion

        #region General
        public struct InventoryQuestion
        {
            public bool Answer;
            public string Response;
            public string Question;
            public int QuestionNumber;
        }
        #endregion

        #region InventorySummary
        public struct InventorySummaryHistoryField
        {
            public DateTime InventoryDate;
            public string Manager;
            public string InvType;
            public string InvSubType;
            public decimal OverShort;
            public decimal Adjustment;
            public decimal NetOverShort;
            public int PAScore;
        }

        public struct InventorySummaryChargeOffsField
        {
            public int Category;
            public string CategoryDescription;
            public int TotalItems;
            public int TotalItemsNF;
            public decimal PercentItemsNF;
            public decimal TotalAmount;
            public decimal TotalAmountNF;
            public decimal PercentNFAmount;
        }
        #endregion

        #region Post Audit
        public struct PostAuditField
        {
            public string ICN;
            public string Reason;
            public string MerchandiseDescription;
            public int Qty;
            public decimal Cost;
            public decimal Retail;
            public int Category;
        }

        public struct PostAuditInventoryTotalsField
        {
            public string InventoryType;
            public int Qty;
            public decimal Cost;
            public int Category;
        }

        public struct PostAuditTempICNReconciliationField
        {
            public int TotalNewICNsSoldNotReconciledQty;
            public decimal TotalNewICNsSoldNotReconciledCost;
            public int TotalOldICNsSoldNotReconciledQty;
            public decimal TotalOldICNsSoldNotReconciledCost;
            public int ChargeOffQty;
            public decimal ChargeOffCost;
            public int ChargeOnQty;
            public decimal ChargeOnCost;
            public int TotalNewICNsSoldReconciledQty;
            public decimal TotalNewICNsSoldReconciledCost;
            public int TotalActualICNsReconciledQty;
            public decimal TotalActualICNsReconciledCost;

        }
        #endregion

        #region Constructors
        public AuditReportsObject()
        {
            ReportTitle = string.Empty;
            ReportStore = string.Empty;
            StoreNumber = string.Empty;
            ReportNumber = 0;
            InventoryAuditDate = DateTime.Today;
            ReportSort = string.Empty;
            ReportDetail = string.Empty;
            ReportStoreDesc = string.Empty;
            ReportTempFile = string.Empty;
            ReportTempFileFullName = string.Empty;
            ReportSortSQL = string.Empty;
            ReportFilter = string.Empty;
            ReportError = string.Empty;
            ReportErrorLevel = 0;
            ReportParms = new List<object>();
            ReportImage = null;
        }
        #endregion

        #region Public Methods
        public void CreateTemporaryFullName(string ReportName)
        {
            //ReportTempFileFullName = ReportTempFile + "tmp\\Report" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";
            ReportTempFileFullName = ReportTempFile + "\\" + ReportName + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";
        }

        public void BuildTrakkerUploadFieldsList()
        {
            /*List<TrakkerUploadReportSinceLastInventory> lstTrakkerUploadReportField = new List<TrakkerUploadReportSinceLastInventory>();
            TrakkerUploadReportSinceLastInventory sinceLast1 = new TrakkerUploadReportSinceLastInventory();
            sinceLast1.ICN = "001521033874100100";
            sinceLast1.MerchandiseDescription = "This is a merechandise description Since the last inventory 1";
            sinceLast1.Cost = 17.97m;
            sinceLast1.Retail = 29.95m;
            sinceLast1.Status = "Sold";
            sinceLast1.Category = "Since Last Inventory";
            lstTrakkerUploadReportField.Add(sinceLast1);

            TrakkerUploadReportSinceLastInventory sinceLast2 = new TrakkerUploadReportSinceLastInventory();
            sinceLast2.ICN = "001521033874100101";
            sinceLast2.MerchandiseDescription = "This is a merechandise description Since the last inventory 2";
            sinceLast2.Cost = 17.97m;
            sinceLast2.Retail = 29.95m;
            sinceLast2.Status = "Refund";
            sinceLast2.Category = "Since Last Inventory";
            lstTrakkerUploadReportField.Add(sinceLast2);

            TrakkerUploadReportMissingItems missing1 = new TrakkerUploadReportMissingItems();
            missing1.ICN = "001521033874100102";
            missing1.MerchandiseDescription = "This is a merechandise description for missing items 1";
            missing1.Cost = 17.97m;
            missing1.Retail = 29.95m;
            missing1.Status = "PFI";
            missing1.PostAuditStatus = "None";
            missing1.Category = "Missing Items";
            lstTrakkerUploadReportField.Add(missing1);

            TrakkerUploadReportMissingItems missing2 = new TrakkerUploadReportMissingItems();
            missing2.ICN = "001521033874100103";
            missing2.MerchandiseDescription = "This is a merechandise description for missing items 3";
            missing2.Cost = 17.97m;
            missing2.Retail = 29.95m;
            missing2.Status = "PFI";
            missing2.PostAuditStatus = "None";
            missing2.Category = "Missing Items";
            lstTrakkerUploadReportField.Add(missing2);

            TrakkerUploadReportNXTsSinceLastInventory nxtsSinceLast1 = new TrakkerUploadReportNXTsSinceLastInventory();
            nxtsSinceLast1.ICN = "001521033874100104";
            nxtsSinceLast1.MerchandiseDescription = "This is a merechandise description for NXTs since Last Inventory 1";
            nxtsSinceLast1.Retail = 29.95m;
            nxtsSinceLast1.Status = "Sold";
            nxtsSinceLast1.TransactionNumber = "1234755";
            nxtsSinceLast1.Category = "NXTs Since Last";
            lstTrakkerUploadReportField.Add(nxtsSinceLast1);

            TrakkerUploadReportNXTsSinceLastInventory nxtsSinceLast2 = new TrakkerUploadReportNXTsSinceLastInventory();
            nxtsSinceLast2.ICN = "001521033874100105";
            nxtsSinceLast2.MerchandiseDescription = "This is a merechandise description for NXTs since Last Inventory 2";
            nxtsSinceLast2.Retail = 29.95m;
            nxtsSinceLast2.Status = "Sold";
            nxtsSinceLast2.TransactionNumber = "7654755";
            nxtsSinceLast2.Category = "NXTs Since Last";
            lstTrakkerUploadReportField.Add(nxtsSinceLast2);

            TrakkerUploadReportUnexpectedItems unexpected1 = new TrakkerUploadReportUnexpectedItems();
            unexpected1.ICN = "001521033874100106";
            unexpected1.MerchandiseDescription = "This is a merechandise description Unexpected Items 1";
            unexpected1.Cost = 29.95m;
            unexpected1.TrakID = 29;
            unexpected1.ScanSequence = 29;
            unexpected1.Status = "Sold";
            unexpected1.AuditIndicator = "Charge On";
            unexpected1.PostAuditStatus = "None";
            unexpected1.TrakFlag = "F";
            unexpected1.Category = "Unexpected Items";
            lstTrakkerUploadReportField.Add(unexpected1);

            TrakkerUploadReportUnexpectedItems unexpected2 = new TrakkerUploadReportUnexpectedItems();
            unexpected2.ICN = "001521033874100107";
            unexpected2.MerchandiseDescription = "This is a merechandise description Unexpected Items 2";
            unexpected2.Cost = 29.95m;
            unexpected2.TrakID = 29;
            unexpected2.ScanSequence = 29;
            unexpected2.Status = "Sold";
            unexpected2.AuditIndicator = "Charge On";
            unexpected2.PostAuditStatus = "None";
            unexpected2.TrakFlag = "F";
            unexpected2.Category = "Unexpected Items";
            lstTrakkerUploadReportField.Add(unexpected2);

            TrakkerUploadReportDuplicateScans duplicate1 = new TrakkerUploadReportDuplicateScans();
            duplicate1.ICN = "001521033874100108";
            duplicate1.MerchandiseDescription = "This is a merechandise description Unexpected Items 1";
            duplicate1.Cost = 29.95m;
            duplicate1.TrakID = 29;
            duplicate1.AuditLocation = "Locker";
            duplicate1.ScanSequence = 29;
            duplicate1.Status = "PFI";
            duplicate1.AuditIndicator = "Charge On";
            duplicate1.PostAuditStatus = "None";
            duplicate1.TrakFlag = "D";
            duplicate1.Category = "Duplicate Scans";
            lstTrakkerUploadReportField.Add(duplicate1);

            TrakkerUploadReportDuplicateScans duplicate2 = new TrakkerUploadReportDuplicateScans();
            duplicate2.ICN = "001521033874100109";
            duplicate2.MerchandiseDescription = "This is a merechandise description Unexpected Items 2";
            duplicate2.Cost = 29.95m;
            duplicate2.TrakID = 29;
            duplicate2.AuditLocation = "Locker";
            duplicate2.ScanSequence = 29;
            duplicate2.Status = "PFI";
            duplicate2.AuditIndicator = "Charge On";
            duplicate2.PostAuditStatus = "None";
            duplicate2.TrakFlag = "F";
            duplicate2.Category = "Duplicate Scans";
            lstTrakkerUploadReportField.Add(duplicate2);

            ListTrakkerUploadReportField = lstTrakkerUploadReportField;*/
        }


        #endregion
    }
}
