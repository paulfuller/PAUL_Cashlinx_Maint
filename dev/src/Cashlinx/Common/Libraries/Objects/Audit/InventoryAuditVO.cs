using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;

namespace Common.Libraries.Objects.Audit
{
    public class InventoryAuditVO
    {
        public InventoryAuditVO()
        {
            DownloadDate = DateTime.MinValue;
            UploadDate = DateTime.MinValue;
            Status = AuditStatus.ACTIVE;
            StoreName = "";

            CompactDiscSummary = new CaccSummary();
            DvdDiscSummary = new CaccSummary();
            PremiumVideoGameSummary = new CaccSummary();
            StandardVideoGameSummary = new CaccSummary();
            VideoTapeSummary = new CaccSummary();
            ExptectedJewlery = new CaccSummary();
            ExpectedItems = new CaccSummary();
            ExptedtedGeneral = new CaccSummary();

        }

        public int AuditId { get; set; }
        public AuditReason AuditReason { get; set; }
        public AuditScope AuditScope { get; set; }
        public AuditType AuditType { get; set; }
        public CaccSummary CompactDiscSummary { get; private set; }
        public DateTime DateInitiated { get; set; }
        public DateTime DownloadDate { get; set; }
        public CaccSummary DvdDiscSummary { get; private set; }
        public string InitiatedBy { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime DateCompleted { get; set; }
        public bool MarketManagerPresent { get; set; }
        public CaccSummary PremiumVideoGameSummary { get; private set; }
        public AuditReportDetail ReportDetail { get; set; }
        public CaccSummary StandardVideoGameSummary { get; private set; }
        public string StateCode { get; set; }
        public AuditStatus Status { get; set; }
        public string StoreNumber { get; set; }
        public string StoreName { get; set; }
        public DateTime UploadDate { get; set; }
        public CaccSummary VideoTapeSummary { get; private set; }

        public CaccSummary ExpectedItems { get; private set; }
        public CaccSummary ExptectedJewlery { get; private set; }
        public CaccSummary ExptedtedGeneral { get; private set; }
        public DataSet PreAuditData { get; set; }

        //inventory summary Header fields
        public string Division { get; set; }
        public string Region { get; set; }
        public string ActiveShopManager { get; set; }
        public string RVP { get; set; }
        public string ExitingShopManager { get; set; }
        public string Auditor { get; set; }
        public string MarketManager { get; set; }
        public DateTime AuditStartDate { get; set; }
        public string InventoryType { get; set; }
        public DateTime LastAuditDate { get; set; }
        public string SubType { get; set; }
        public DateTime LayAuditDate { get; set; }
        public int AuditScore { get; set; }
        //Inventory Summary Current inventory
        public decimal YTDShortage { get; set; }
        public decimal CurrentLoanBalance { get; set; }
        public decimal PreviousLoanBalance { get; set; }
        public decimal YTDAdjustments { get; set; }
        public decimal CurrentInventoryBalance { get; set; }
        public decimal PreviousInventoryBalance { get; set; }
        public decimal PreviousYearCoff { get; set; }
        public decimal CashInStore { get; set; }
        public decimal NetYTDShortage { get; set; }
        public decimal TotalChargeOn { get; set; }
        public decimal TotalChargeOff { get; set; }
        public decimal TempICNAdjustment { get; set; }
        public decimal OverShort { get; set; }
        public decimal Adjustment { get; set; }
        public decimal NetOverShort { get; set; }
        public decimal Tolerance { get; set; }

        public bool IsDownloadAndUploadComplete()
        {
            return IsDownloadComplete() && IsUploadComplete();
        }

        public bool IsDownloadComplete()
        {
            return DownloadDate != DateTime.MinValue;
        }

        public bool IsUploadComplete()
        {
            return UploadDate != DateTime.MinValue;
        }

        public decimal GetTotalCaccCost()
        {
            return CompactDiscSummary.Cost + VideoTapeSummary.Cost + StandardVideoGameSummary.Cost + PremiumVideoGameSummary.Cost + DvdDiscSummary.Cost;
        }

        public int GetTotalCaccQty()
        {
            return CompactDiscSummary.Quantity + VideoTapeSummary.Quantity + StandardVideoGameSummary.Quantity + PremiumVideoGameSummary.Quantity + DvdDiscSummary.Quantity;
        }
    }
}
