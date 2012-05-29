/************************************************************************
 * Namespace:       ReportsConstants.Shared
 * Class:           ...
 * 
 * Description      The file manages multiple enums, structs, and classes
 *                  associated to Reports.
 * 
 * History
 * SMurphy, Initial Development
 * no ticket SMurphy 5/24/2010 added enums & constants
 * 
 * 12/2/10  Tracy McConnell
 *      Added support for new build-9 reports (CACC Sales and JewelryCount)
 * 12/29/11 Ed Waltmon
 *      CR#INTG100014929/ BZ#1394/ CR# INTG100014281/ BZ#1406
 *      Firearms reports restructuring
 *  1/16/2012 Ed Waltmon
 *      CR 14647
 *      MAL Removed
 * **********************************************************************/

namespace Common.Libraries.Utility.Shared
{
    public enum ReportIDs
    {
        DetailInventory = 223,
        Snapshot = 211,
        Dstr = 96,
        GunDispositionReport = 201, // Multiple Gun Disposition
        RifleDispositionReport=232,
        LoanAuditReport = 206,
        MAL = 208,
        FullLocationsReport = 209,
        InPawnJewelryLocationReport = 213,
        GunAuditReport = 215,
        GunAuditReportATFOpenRecords = 216,
        CashDrawerLedgerReport = 226,
        CACCSales = 219,
        JewelryCount = 217,
        CheckDeposit = 68,
        LoanInquiry = 1,
        ExtInquiry = 2,
        InventoryInquiry = 3,
        RetailInquiry = 4,
        DailySales = 227,
        InventoryChargeOff = 228,
        InquiryItemDetail = 229,
        FirearmsReport = 230,
        CashTransferInquiry = 250,
        PartialPaymentInquiry = 251,// This value may change
        RefurbList = 231
    }

    public enum LayawayReportIDs
    {
        LayawayHistoryAndSchedule = 23,
        LayawayPickingSlip,
        LayawayForfeitPickingSlip,
        LayawayContract = 26,
        LayawayTermination,
        ForfeitedLayawaysListing = 28,
        TerminatedLayawaysPickingSlip = 31,
        TerminatedLayawaysListing = 32
    }

    public enum EnumDetailInventory
    {
        Aisle = 0,
        Shelf = 1,
        Other
    }

    public enum ReportEnums
    {
        STARTDATE = 0,
        ENDDATE
    }

    public enum GunDispEnums
    {
        DIRECTION = 2,
        DAYCOUNT

    }

    public class ReportConstants
    {
        public const int GUNDISPBACKWARDS = 0;
        public const int GUNDISPNUMOFDAYS = 5;
        public const string NODATA = "No data was returned.";

        public static string[] DailyTitles = new string[]
        {
                "Full Locations", "In Pawn Jewelry Locations",
                "Loan Audit", "Snapshot", "CACC Sales Analysis", "Jewelry Count Detail", "Daily Sales",
                "Firearm Reports"//, "Refurb List"
        };

        public static int[] DailyNumbers = new int[]
        {
                209, 213, 206, 211, 219, 217, 227, 230//, 231
        };

        public static string[] MonthlyTitles = new string[]
        {
                "Loan Audit", "Detail Inventory"
        };

        public static int[] MonthlyNumbers = new int[]
        {
                206, 223
        };

        public static string[] InquiryTitles = new string[]
        {
                "Loan", "Loan Servicing", "Inventory", "Partial Payment"/*, "Retail", "Cash Transfer Inquiry"*/
        }; 
    

        public static int [] InquiryNumbers = new int[]
        {
                1, 2, 3, 251/*, 4, 250*/
        } ;
    }


    public class ReportHeaders
    {
        public const string SELECT = "Select";
        public const string OPERATIONAL = "Operational";
        public const string RUNDATE = "Run Date: ";
        public const string REPORTINGCOLON = "Reporting: ";
        public const string REPORTING = "Reporting ";
        public const string REPORTNUM = "Report #: ";
        public const string REPORTDETAIL = "Report Detail: ";
        public const string REPORTSORT = "Sort By: ";
        public const string NA = "N/A";
        public const string REPORT = " Report";
        public const string LOCATION = "Location: ";
    }

    public struct InventoryChargeOffFields
    {
        public string ReasonForChargeOff { get; set; }
        public string ChargeOffNumber { get; set; }
        public string ICN { get; set; }
        public string MerchandiseDescription { get; set; }
        public string Comment { get; set; }
        public string PoliceCaseNumber { get; set; }
        public string AuthorizedBy { get; set; }
        public string EmployeeNumber { get; set; }
        public string ReplacementLoanNumber { get; set; }
        public string CustomerName { get; set; }
        public string CharitableOrganization { get; set; }
        public string ATFIncidentNumber { get; set; }
        public string ChargeOffAmount { get; set; }
        public long GunNumber { get; set; }
        public bool IsGun { get; set; }
    }
}
