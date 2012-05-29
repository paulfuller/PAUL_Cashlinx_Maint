/************************************************************************
 * Namespace:       PawnObjects.VO.Business
 * Class:           ReportObject
 * 
 * Description      The file manages multiple enums, structs, and classes
 *                  associated to Report data
 * 
 * History
 * S Murphy 1/20/2010, Initial Development
 *  no ticket SMurphy 4/13/2010 problem when Adobe is already open
 *  no ticket SMurphy 4/15/2010 added Gun Disposition pieces
 *  no ticket SMurphy 4/21/2010 Changes to remove iText from ReportObject per Sree 
 *  12/2/10 Tracy  Added support for CACCSalesData and JewleryCount Data
 * **********************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using Common.Libraries.Utility.Shared;

namespace Common.Controllers.Application
{
    public class ReportObject
    {
        public string ReportTitle { set; get; }
        public string ReportSort { set; get; }
        public string ReportDetail { set; get; }
        public string MerchandiseLocation { set; get; }
        public string ReportStore { set; get; }
        public string ReportStoreDesc { set; get; }
        public int ReportNumber { set; get; }

        public string RunDate
        {
            set;
            get;
        }

        public string ReportTempFile { set; get; }
        public string ReportTempFileFullName { set; get; }
        public string ReportSortSQL { set; get; }
        public string ReportFilter { set; get; }
        public string ReportError { set; get; }
        public string FormName { get; set; }
        public Int16 ReportErrorLevel { set; get; }
        public List<object> ReportParms { set; get; }

        public System.Drawing.Bitmap ReportImage { set; get; }
        public InventoryChargeOffFields InventoryChargeOffFields { get; set; }
        public List<LoanAudit> LoanAuditData { set; get; }
        public List<InPawnJewelry> InPawnJewelryData { set; get; }
        public List<GunAudit> GunAuditData { set; get; }
        public List<GunAuditATFOpenRecords> GunAuditDataATFOpenRecordsData { set; get; }
        public DataTable CACCSalesData { set; get; }
        public DataTable DailySalesData { set; get; }
        public DataTable DailySalesSummaryData { set; get; }
        public DataTable JewleryCountSummaryData { set; get; }
        public DataTable JewleryCountDetailData { set; get; }
        public DataView InquiryItemDetails_theData { get; set; }
        public DataView InquiryItemDetails_thisData { get; set; }
        public int InquiryItemDetails_RowNumber { get; set; }
        public string InquiryItemDetails_DOB { get; set; }
        public string InquiryItemDetails_Since { get; set; }
        public List<FullLocations> FullLocationsData { set; get; }
        public List<RetailSaleListing> RetailSaleListingData { set; get; }
        public List<RetailSaleCustomer> RetailSaleCustomerData { set; get; }
        public List<RetailSaleMerchandise> RetailSaleMerchandiseData { set; get; }
        public List<RetailSaleTender> RetailSaleTenderData { set; get; }
        public List<RetailSaleHistory> RetailSaleHistoryData { set; get; }
        public SnapshotContext SnapshotData { get; set; }
        public List<DetailInventorySummary> DetailInventorySummaryList { set; get; }
        public List<DetailInventoryLines> DetailInventoryLinesList { set; get; }
        public DetailInventoryLinesAndSummary DetailInventoryData { get; set; }
        public List<RefurbItem> ListRefurbItemsExpected { get; set; }
        public List<RefurbItem> ListRefurbItemsNotExpected { get; set; }
        //report #206 Loan Audit report
        //report #201 Gun Disposition 
        public DataSet GunDispositionData { set; get; }
        //report #206 Loan Audit 

        public struct RefurbItem
        {
            public DateTime TransferDate;
            public String TransferNumber;
            public String RefurbNumber;
            public string ICN;
            public String Description;
            public decimal Cost;
        }

        public struct PFIMailer
        {
            public Int32 ticketNumber;
            public Int32 customerNumber;
            public DateTime originalPFINote;
            public String printNotice;
            public String customerName;
            public String customerId;
            public String customerAddress;
            public String customerCity;
            public String customerState;
            public String customerZipCode;
            public DateTime pfiEligibleDate;
            public string storeName;
            public Int32 storeNumber;
            public String storeAddress;
            public String storeCity;
            public String storeState;
            public String storeZipCode;
            public String storePhone;
            public Decimal pfiMailerFee;
       }

        public struct PawnTicketAddendum
        {
            public Int32 ticketNumber;
            public String customerLastName;
            public String customerSuffix;
            public String customerFirstName;
            public String customerMiddleInitial;
            public String customerInitials;
            public DateTime dateMade;
            public DateTime dueDate;
            public DateTime pfiEligibleDate;
            public Decimal loanAmount;
            public Decimal pawnFinanceCharge;
            public Int32 numberOfItems;
            public Int32 itemNumber;
            public List<string> merchandiseDescription;
        }

        public struct RetailSaleCustomer 
        {
            public Int32 ticketNumber;    
            public String customerNumber;
            public String customerName;
            public String customerId;
            public String customerAddress;
            public String customerAddress2;
            public String city;
            public String state;
            public String zipCode;
            public DateTime DOB;
            public String weight;
            public String height;
            public String gender;
            public String race;
            public DateTime since;
            public String eyeColorCode;
            public String hairColorCode;
            public String phone;
        }

        public struct RetailSaleMerchandise
        {
            public Int32 ticketNumber;
            public String shortCode;
            public String ICN;
            public String description;
            public String isLocated;
            public String location;
            public String locationShelf;
            public String locationAisle;
            public decimal amount;
            public decimal itemAmount;
            public String gunNumber;
            public String jcase;
            public String statusCD;
            public DateTime statusDate;
            public Int32 quantity;
            public decimal pfiAmount;
            public decimal retailPrice;
            public String xicn;
            public Int32 categoryCode;
            public Int32 rfbNumber;
            public Int32 rfbStore;
            public DateTime dispostionDate;
            public String dispostionType; 
            public Int32 dispostionItem;
            public Int32 dispostionDoc;
            public String manufacturer;
            public String model;
            public String serialNumber;
            public String isCacc;
            public String transactionType;
            public Int32 invAge;
            public Int32 daysSinceSale;
            public String categoryDescription;
            public String customerNumber;
            public String purchType;
            public String weight;
            public String caccLevel;
        }

        public struct RetailSaleTender 
        {
            public String tenderType;
            public decimal refAmount;
        }

        public struct RetailSaleHistory
        {
            public Int32 receiptNumber;
            public Int32 receiptDetailNumber;
            public String refEvent;
            public DateTime refTime;
            public decimal amount;
            public String entId;
            public decimal tax;
        }

        public struct RetailSaleListing
        {
            public Int32 layawayTicketNumber;
            public Int32 ticketNumber;
            public Int32 originalTicketNumber;
            public decimal tax;
            public string cashDrawer;
            public DateTime date;
            public Int32 shopNumber;
            public decimal saleAmount;
            public decimal cost;
            public string terminalId;
            public string status;
            public string createType;
            public string userId;
            public string entId;
        }

        public struct StatusChange
        {
            public DateTime transactionDate;
            public String status;
        }

        public struct CostRevision
        {
            public DateTime date;
            public decimal currentCost;
            public decimal addedCost;
            public decimal newCost;
            public decimal currentRetail;
            public decimal newRetail;
        }

        public struct RetailPriceChange
        {
            public DateTime transactionDate;
            public string percentageChange;
            public decimal retailBefore;
            public decimal retailAfter;
            public string userId;
        }

        //report 223 Detail Inventory report
        public struct DetailInventoryLinesAndSummary
        {
            public List<DetailInventorySummary> SummaryData;
            public List<DetailInventoryLines> InventoryData;
        }

        public struct DetailInventorySummary
        {
            public string status_cd;
            public string on_Hold;
            public int quantity;
            public decimal item_amt;
        }

        public struct DetailInventoryLines
        {
            public string status_cd;
            public string hold_desc;
            public string ICN;
            public string md_desc;
            public string loc_aisle;
            public string loc_shelf;
            public string location;
            public string gun_number;
            public int quantity;
            public decimal item_amt;
            public string xicn;
        }

        //report #206 Loan Audit report
        public struct LoanAudit
        {
            public string OrigTktNumber;
            public string CurrTktNumber;
            public string CategoryName;
            public string LoanNumber;
            public decimal MdseLoanAmount;
            public string ShopLoanStatus;
            public string PawnLoanOrigDate;
            public string FullMdseDescr;
            public string LocationAisle;
            public string Shelf;
            public string Location;
            public string GunNumber;
            public decimal CurrentLoanAmount;
            public decimal TotalLoanAmt;
            public int IncludeInCount;
            public decimal PrincipalAmount;
        }
        //report #209 Full Location
        public struct FullLocations
        {
            //from rollover, pawn, & extension cursors
            public string TransactionType;
            public string OriginalTicketNumber;
            public string PreviousTicketNumber;
            public string CurrentTicketNumber;
            public string LastName;
            public string FirstName;
            public string Status;
            public string EmployeeID;
            //from mdse cursor
            public string LocationAisle;
            public string LocationShelf;
            public string Location;
            public string PKValue;
            public string ProKnowRetailValue;
            public string Importer;
            public string FullMerchandiseDescription;
            public string Class;
            public string TransactionAmount;
            public string CategoryCode;
            //for internal use
            public int SortOrder;
            public string RecordType;
        }
        //report #211 Snapshot report
        public struct Snapshot
        {
            public double SequenceNumber;
            public int Count;
            public decimal Amount;
        }
        public struct SnapshotContext
        {
            public Dictionary<double, ReportObject.Snapshot> Data;
            public List<SnapshotCategory> AvailableCategories;
        }
        public struct SnapshotCategory
        {
            public double MinSequenceNumber { get; set; }
            public double MaxSequenceNumber { get; set; }
        }
        //report #213 In Pawn Jewelry Loacations report
        public struct InPawnJewelry
        {
            public string Location;
            public Int32 ItemCount;
        }
        //report #215 Gun Audit report
        public struct GunAudit
        {
            public Int32 GunNumber;
            public string CurrTktNumber;
            public string OrigTktNumber;
            public string ICN;
            public string GunType;
            public string MdseLoanAmount;
            public string Cost;
            public string ShopLoanStatus;
            public string GunImporter;
            public string LocationAisle;
            public string Shelf;
            public string Location;
            public string FullMdseDescr;
            public int Category;
        }

        //report #216 Gun Audit ATF Open Records
        public struct GunAuditATFOpenRecords
        {
            public Int32 GunNumber;
            public string Manufacturer;
            public string Importer;
            public string Model;
            public string SerialNumber;
            public string GaugeOrCaliber;
            public string GunType;
            public string ICN;
            public string TransactionNumber;
            public string Status;
            public string StatusDescription;
            public string LocationAisle;
            public string Shelf;
            public string Location;
            public DateTime GunDate;
            public string CustomerName;
            public string CustomerAddress;
            public string CustomerAddress1;
            public string CustomerID;
            public string HoldDesc;
            public string HoldType;
        }

        //for ShopTransferReport
        public struct TransferReport
        {
            public string FromStoreName;
            public string FromStoreAddrLine1;
            public string FromStoreAddrLine2;
            public string FromStoreAddrLine3;
            public string FromTelephone;
            public string FromFax;

            public string ToStoreName;
            public string ToStoreAddrLine1;
            public string ToStoreAddrLine2;
            public string ToStoreAddrLine3;
            public string storeFax;
            public string storeMgrPhone;
            public string storeMgrName;
            public string transferAmount;

            public string TransferTypeDepartmentName;
            public string TransferTypeFacilityNumber;
            public string TransferTypeFacilityPhone;
            public string TransferTypeFacilityManagerName;
            //public string OrigTktNumber;
        }


        //for ShopTransferINReport
        public struct TransferINReportStruct
        {

            public string transDate;
            public string userID;
            public string ToStoreName;
            public string ToStoreNo;


            public string FromShopName;
            public string FromShopNo;
            public string FromStoreAddrLine1;
            public string FromStoreAddrLine2;

            public string storeMgrPhone;
            public string storeMgrName;

            public string transNum;
            public string Carrier;
            public string TransferReference;
            public string DateReceived;
            public string ReceivedBy;

            public string transferAmount;

            public string logPath;
            //public string OrigTktNumber;
        }



        public ReportObject()
        {
            ReportTitle = string.Empty;
            ReportSort = string.Empty;
            ReportDetail = string.Empty;
            ReportStore = string.Empty;
            ReportStoreDesc = string.Empty;
            ReportNumber = 0;
            ReportTempFile = string.Empty;
            ReportTempFileFullName = string.Empty;
            ReportSortSQL = string.Empty;
            ReportFilter = string.Empty;
            ReportError = string.Empty;
            ReportErrorLevel = 0;
            ReportParms = new List<object>();
            ReportImage = null;
        }

        public ReportObject(string reportTempFile)
        {
            ReportTitle = string.Empty;
            ReportSort = string.Empty;
            ReportDetail = string.Empty;
            ReportStore = string.Empty;
            ReportStoreDesc = string.Empty;
            ReportNumber = 0;
            ReportTempFile = reportTempFile;
            ReportTempFileFullName = string.Empty;
            ReportSortSQL = string.Empty;
            ReportFilter = string.Empty;
            ReportError = string.Empty;
            ReportErrorLevel = 0;
            ReportParms = new List<object>();
            ReportImage = null;
        }

       

        public void CreateTemporaryFullName()
        {
            //ReportTempFileFullName = ReportTempFile + "tmp\\Report" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";
            ReportTempFileFullName = ReportTempFile + "\\Report" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";
        }

        public void CreateTemporaryFullName(string ReportName)
        {
            //ReportTempFileFullName = ReportTempFile + "tmp\\Report" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";
            ReportTempFileFullName = ReportTempFile + "\\" + ReportName + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";
        }

    }
}
