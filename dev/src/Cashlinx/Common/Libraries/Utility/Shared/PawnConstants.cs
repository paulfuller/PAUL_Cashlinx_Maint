/************************************************************************
 * Namespace:       PawnUtilities.Shared
 * Class:           ...
 * 
 * Description      The file manages multiple enums, structs, and classes
 *                  associated to Pawn Loan data, analytics, and rules.
 * 
 * History
 * David D Wise, Initial Development
 * Sreelatha Rengarajan 10/23/2009 Added  properties in the fee struct
 * SR 6/2/2010 Added enum for user info record
 * SR 7/26/2010 Added enum for cash transfer types and cash transfer status and
 * transfer details record
 * **********************************************************************/

using System;
using System.Collections.Generic;
using System.Data;

namespace Common.Libraries.Utilities.Shared
{

    // Used for ProKnow Primary and Secondary Manufacturers during a ProKnow Match
    public enum ActiveManufacturer : int
    {
        PRIMARY     = 0,
        SECONDARY   = 2
    }
    // Code returned by the Database to indicate what type for Form Control to use
    public enum ControlType : int
    {
        COMBOBOX_ONLY           = 0,
        COMBOBOX_TEXT_ENABLED   = 1,
        TEXTFIELD               = 2,
        READ_ONLY               = 3
    }
    // Part of the Describe Merchandise Pawn Entry Point
    public enum CurrentContext : int
    {
        EDIT_MMP,
        NEW,
        HISTORY,
        PFI_ADD,
        PFI_MERGE,
        PFI_REDESCRIBE,
        PFI_REPLACE,
        READ_ONLY
    }
    public enum FeeTypes
    {
        ADMINISTRATIVE,
        CITY,
        FIREARM,
        FIREARM_BACKGROUND,
        GUN_LOCK,
        INITIAL,
        LATE,
        LOAN,
        MINIMUM_INTEREST,
        ORIGIN,
        PFI_MAILER,
        PREPAID_CITY,
        PREPARATION,
        PROCESS,
        SETUP,
        STORAGE,
        STORAGE_MAXIMUM,
        TICKET,
        LOST_TICKET,
        MINIMUM_INTEREST_AT_REDEMPTION,
        AUTOEXTEND,
        GUNLOCK,
        INTEREST,
        SERVICE
        
    }

    public enum FeeRevOpCodes
    {
        EXTENSION,           
        NEWLOAN,             
        PAYDOWN,             
        PICKUP,              
        RENEWAL,             
        VOIDEXTENSION,       
        VOIDLOAN,            
        VOIDPAYDOWN,         
        VOIDPICKUP,          
        VOIDRENEWAL 
    }

    public enum ManagerOverrideReason
    {
        ALL_CUSTOMER_WANTED,
        GOOD_HISTORY,
        NEW_CUSTOMER
    }

    public enum ManagerOverrideTransactionType
    {
        NL,
        PFI,
        PU,
        RN,
        EX,
        BUY,
        SALE,
        LAY,
        PD,
        RO,
        SOPEN
    }

    public enum ManagerOverrideType
    {
        PKV,
        PFIE,
        PFIP,
        WV,
        PRO,
        DOC,
        OSID,
        NLO,
        SAFE
    }

    public enum PawnLoanStatus
    {
        ALL,
        IP,
        PU,
        LAY,
        OFF,
        PFI,
        PS,
        PUR,
        SOLD,
        TO,
        VO,
        HIP,
        HPFC,
        HPFI,
        RTC,
        RN,
        PD,
        PFC
        
    }
    public enum PawnItemReason
    {
        ADDD,
        BLNK,
        BRKN,
        CACC,
        HPFI,
        MERGED,
        NOMD,
        NXT,
        STLN,
        STRU
    }

    public enum PfiAssignment
    {
        CAF,
        Excess,
        Normal,
        Refurb,
        Scrap,
        Sell_Back,
        Wholesale
    }
    public enum StateStatus
    {
        BLNK,
        BH,
        CH,
        PD,
        E,
        L,
        P,
        PFIE,
        PFIS,
        PFI,
        PFIW,
        PFIL,
        PH,
        PS,
        RN,
        RO,
        RTC,
        IP
    }
    public enum ServiceIndicators
    {
        Pickup,
        Extend,
        Paydown,
        Renew,
        Rollover,
        Blank,
        LT
    }
    public enum ServiceTypes
    {
        PICKUP,
        EXTEND,
        RENEW,
        CUSTHOLD,
        POLICEHOLD,
        ROLLOVER,
        POLICESEIZE,
        PAYDOWN
    }
    public enum MerchandiseStatus
    {
        BUY,
        IP,
        LAY
    }
    public enum ProductType : int
    {
        PAWN = 1,
        BUY = 2
    }

    public enum ReceiptRefTypes
    {
        PAWN
    }

    /// <summary>
    /// Represents the receipt event types for a loan.
    /// </summary>
    public enum ReceiptEventTypes
    {
        Pickup,
        Extend,
        Renew,
        Paydown,
        PolSeize,    //Not yet added to DB, just used as placeholder
        LoanUp,         //Not yet added to DB, just used as placeholder
        New,
        PFI,
        RTC,            //Not yet added to DB, just used as placeholder
        TO,
        VEX,
        VNL,
        VPD,
        VPU,
        VRN
    }


    public enum ExtensionTerms
    {
        DAILY,
        MONTHLY
    }

    //Different states of a Fee
    public enum FeeStates
    {
        WAIVED,
        ASSESSED,
        PAID,
        VOID
    }

    //Different Hold Types
    public enum HoldTypes
    {
        CUSTHOLD,
        POLICEHOLD,
        BKHOLD
    }

    // Pawn Item status during Describe Merchandise
    public enum TransitionStatus : int
    {
        NO_PROKNOW              = 0,
        MANUFACTURER_ONLY       = 1,
        MAN_MODEL_NO_PROKNOW    = 2,
        MAN_MODEL_PROKNOW       = 3,
        MAN_MODEL_PROKNOW_COMBO = 4
    }
    //Ledger report types
    public enum LedgerReportType
    {
        Preliminary,
        Final
    }

    //used for pawn application
    public enum pawnapprecord : int
    {
        PAWNAPPID = 0,
        CUSTOMERNUMBER,
        STORENUMBER,
        CLOTHING,
        COMMENTS,
        IDISSUEDNUMBER,
        IDISSUEDDATE,
        IDISSUERNAME,
        IDTYPECODE,
        IDEXPIRATION,
        APPSTATUS,
        CREATIONDATE = 11



    }
    //Used for store info
    public enum storeinforecord
    {
         STOREID =0,
         STORENUMBER,
         STORENAME,
         STOREADDRESS1,
         STOREADDRESS2,
         STORECITYNAME,
         STORESTATE,
         STOREZIPCODE,
         LOCALTIMEZONE,
         REGION,
         MARKET,
         BUSINESSUNIT,
         USERID,
         CREATIONDATE,
         LASTUPDATEDATE,
         COUNTRYCODE,
         STORETAX1,
         STORETAX2,
         STORETAX3,
         STORENICKNAME,
         STOREBRANDNAME,
         STOREOPENDATE,
         STORETAXID,
         STOREEFIN,
         SHOPLOCATOR,
         EFFDATECHANG,
         STOREMANAGER,
         STOREPHONENO,
         STOREFAXNO,
         STOREMODEMNO,
         STOREEMAIL,
         PARTYROLEID,
         STOREPHONENO1,
         STOREPHONENO2,
         STOREPHONENO3,
         DAYLIGHTSAVINGS,
         COLLECTION800,
         DEFERSCANNING,
         COMPANYNUMBER,
         ACHFILENAME,
         DEFERSCANDATE,
         CONVERSIONDATE,
         ALIAS_ID=42
    }
    //Values that can be assigned to a resource for a role/user
    public enum ResourceSecurityMask : int
    {
        NONE=0,
        VIEW = 2,
        MODIFY = 6
    }
    //Operations allowed in safe
    public enum SAFEOPERATION
    {
        NONE,
        OPEN,
        CLOSEBALANCED,
        CLOSEUNVERIFIED
    }
    //TOPS transfer operations
    public enum TOPSTRANSFEROPERATIONS
    {
        REIN,
        REOUT
    }
    //Safe/cash drawer states
    public enum CASHDRAWERSTATUS
    {
        CLOSED_BALANCED=0,
        OPEN=1,
        CLOSED_UNVERIFIED=2
    }

    //Role Level values
    public enum ROLEHIERARCHYLEVEL
    {
        NONE,
        GREATER,
        EQUAL,
        LESSER

    }
    public enum EmployeeProfileRecord
    {
        USERID = 0,
        EMPLOYEENUMBER,
        LASTNAME,
        FIRSTNAME,
        EMPLOYEEROLE,
        HOMESTORE,
        PROFILES,
        NOOFPROFILES,
        NAME=8

    }
    // Structure of an Answer result from CAT5 Database
    public struct Answer
    {
        public int      AnswerCode;
        public string   AnswerText;
        public int      DisplayOrder;
        public string   InputKey;
        public string   OutputKey;
    }
    public struct Document
    {
        public DateTime DocumentDate;
        public string   DocumentId;
        public string   DocumentName;
        public string   DocumentType;
    }
    public struct Fee
    {
        public FeeTypes FeeType;
        public string   Tag;
        public decimal  Value;
        public bool CanBeWaived;
        public bool CanBeProrated;
        public bool Waived;
        public bool Prorated;
        public decimal OriginalAmount;
        public FeeStates FeeState;
        public DateTime FeeDate;

    }
    // Structure of a Fixed Feature result from CAT5 Database
    public struct FixedFeature
    {
        public int      AnswerCode;
        public string   AnswerText;
    }
    // Structure for easy retrieval of Information
    public struct QuickCheck
    {
        public string   Caliber;
        public string   Gauge;
        public string   GunType;
        public bool     IsGun;
        public string   Importer;
        public string   Manufacturer;
        public string   Model;
        public int      Quantity;
        public string   SerialNumber;
        public decimal  Weight;
    }
    // Structure for a Pawn Item Attribute
    public struct ItemAttribute
    {
        public Answer       Answer;
        public List<Answer> AnswerList;
        public int          AttributeCode;
        public string       Description;
        public int          DescriptionOrder;
        public ControlType  InputControl;
        public string       InputType;
        public bool         IsIncludedInDescription;
        public bool         IsPreAnswered;
        public bool         IsRequired;
        public bool         IsRestricted;
        public int          LoanOrder;
        public string       MaskDefault;
        public int          MaskLevel;
        public int          MaskOrder;
        public char         MdseField;
        public int          PFIOrder;
        public string       Prefix;
        public string       Suffix;
        public string       ValidationDataType;
    }
    public struct LoanRevision
    {
        public decimal Amount;
        public string EmployeeId;
        public string Event;
        public string ProductRevisionId;
        public string ReceiptId;
    }
    // Stored information for Merchandise Location Assignment
    public struct LocationData
    {
        public int    RecordID;
        public string Aisle;
        public string Description;
        public string Icn_Store;
        public string Icn_Doc_Type;
        public string Icn_Doc;
        public string Icn_Item;
        public string Icn_Sub_Item;
        public string Icn_Year;
        public string Location;
        public string Shelf;
        public string Status;
        public string Store_Number;
        public string User_ID;
    }
    // PMetal stored database Information
    public struct PMetalInfo
    {
        public string   Record_Type;
        public int      Category;
        public string   Class;
        public string   Metal;
        public int      Karats;
        public decimal  Gram_Low;
        public decimal  Gram_High;
        public decimal  Loan_Buy_Per_Gram;
        public decimal  Retail_Per_Gram;
    }
    // Structure to manage ProCall Data returned from ProKnow Database
    public struct ProCallData
    {
        public string   LastUpdateDate;  // No Time Stamp
        public decimal  NewRetail;
        public string   YearDiscontinued;
        public string   YearIntroduced;
    }
    // Structure to manage ProKnow Data returned while getting ProKnow Details of
    // a Manufacturer's Model
    public struct ProKnowData
    {
        public int      ConditionLevel;
        public decimal  LoanAmount;
        public decimal  LoanVarHighAmount;
        public decimal  LoanVarLowAmount;
        public decimal  PurchaseAmount;
        public decimal  RetailAmount;
        public decimal  RetailVarHighAmount;
        public string   RetailVarHighRetailer;
        public decimal  RetailVarLowAmount;
        public string   RetailVarLowRetailer;
    }
    public struct Receipt
    {
        public decimal Amount;
        public DateTime AuxillaryDate;
        public DateTime Date;
        public string EntID;
        public string Event;
        public string ReceiptNumber;
        public string RefNumber;
        public string RefStoreNumber;
        public string StoreNumber;
        public string Type;
        public string TypeDescription;
        public string ReceiptDetailNumber;
    }
    // Stones stored database Information
    public struct StoneInfo
    {
        public string   Record_Type;
        public decimal  Min_Points;
        public decimal  Max_Points;
        public int      Clarity;
        public int      Color;
        public decimal  Loan;
        public decimal  Purchase;
        public decimal  Retail;
    } 
    // Structure to manage information stored in variance.xml Resource file
    public struct Variance
    {
        public char     DocType;
        public decimal  MaxAmount;
        public decimal  MinAmount;
        public decimal  Percent;
    }

    public struct ExtensionMemoInfo
    {
        public string CustomerName;
        public int TicketNumber;
        public decimal Amount;
        public decimal DailyAmount;
        public DateTime DueDate;
        public DateTime NewDueDate;
        public decimal InterestAmount;
        public DateTime NewPfiEligible;
        public decimal ExtensionAmount;
        

    }

    public struct LedgerReport
    {
        public DataTable CashDrawerTransactions;
        public DataTable TopsTransfers;
        public LedgerReportType ReportType;
        public string CashDrawerName;
        public string EmployeeName;
    }

    public struct ServiceOffering
    {
        public int ProdOffering;
        public string ServiceOfferingID;
    }

    public struct CurrentLoggedInUserData
    {
        public string CurrentCashDrawerID;
        public string CurrentUserName;
        public string CurrentUserFullName;
        
    }

    //Cash transfer types
    public enum CashTransferTypes
    {
        SHOPTOSHOP,
        INSHOP,
        SHOPTOBANK,
        BANKTOSHOP
    }

    //Cash Transfer Status codes
    public enum CashTransferStatusCodes
    {
        PENDING,
        ACCEPTED,
        REJECTED,
        RECALLED
 
    }

    public enum TransferDetailsRecord
    {
        SHOPTRANSFERID = 0,
        TRANSFERNUMBER,
        TRANSFERTYPE,
        TRANSFERSTATUS,
        SOURCESHOPID,
        DESTINATIONSHOPID,
        TRANSFERAMOUNT,
        TRANSPORTEDBY,
        DEPOSITBAGNUMBER,
        SOURCECOMMENT,
        DESTINATIONCOMMENT,
        REJECTREASON,
        TRANSFERDATE,
        CREATEDBY,
        CREATIONDATE,
        UPDATEDBY,
        LASTUPDATEDATE,
        STOREADDRESS1,
        STORECITYNAME,
        STORESTATE,
        STOREZIPCODE,
        STOREMANAGER,
        STOREPHONENO,
        STORENUMBER,
        STORENAME,
        V_DEST_STOREADDRESS,
        V_DEST_STORECITYNAME,
        V_DEST_STORESTATE,
        V_DEST_STOREZIPCODE,
        V_DEST_STOREMANAGER,
        V_DEST_STOREPHONENO,
        V_DEST_STORENUMBER,
        V_DEST_STORENAME,
        USERNAME,
        USEREMPLOYEENUMBER=34
    }

    public enum OverShortCodes
    {
        O,
        N,
        S
    }

}
