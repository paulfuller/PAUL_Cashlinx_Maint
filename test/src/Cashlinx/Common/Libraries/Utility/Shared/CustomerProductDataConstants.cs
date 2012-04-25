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
 * SR 2/14/2011 Added enum for StoreCreditStatus
 * **********************************************************************/

using System;
using System.Collections.Generic;
using System.Data;

namespace Common.Libraries.Utility.Shared
{

    public enum PartialPawnPaymentHistoryFormMode
    {
        PartialPawnPaymentHistory,
        VoidPartialPawnPaymentHistory
    }
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
        READ_ONLY,
        CHANGE_RETAIL_PRICE,
        VENDOR_PURCHASE,
        AUDITCHARGEON,
        GUNEDIT,
        GUNREPLACE
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
        PREPAID_CITY,
        PREPARATION,
        PROCESS,
        RESTOCKINGFEE,
        SETUP,
        STORAGE,
        STORAGE_MAXIMUM,
        TICKET,
        LOST_TICKET,
        MINIMUM_INTEREST_AT_REDEMPTION,
        AUTOEXTEND,
        GUNLOCK,
        INTEREST,
        SERVICE,
        BACKGROUND_CHECK_FEE,
        MAILER_CHARGE
    }

    public enum FeeRefTypes
    {
        PAWN,
        LAY,
        EXT,
        PARTP,
        SALE,
        LAYPU
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
        VOIDRENEWAL,
        SALE,
        VOIDSALE,
        LAY,
        VOIDSALEREFUND,
        REFUNDLAYAWAY,
        VOIDLAYAWAY,
        TERMLAYAWAY,
        FORFLAYAWAY,
        LAYPICKUP,
        PARTP,
        VPARTP

    }

    public enum ManagerOverrideReason
    {
        ALL_CUSTOMER_WANTED,
        GOOD_HISTORY,
        NEW_CUSTOMER,
        APPROPRIATE_DOCUMENTS_PROVIDED
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
        SAFE,
        PUR,
        CD,
        UPDATE,
        TAXEXEMPTION,
        RETAILSALE,
        FIREARMSBUYOUT
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
        SOPEN,
        PURO,
        PHOLD,
        OOBAL,
        EXCASH,
        DSCPCT,
        TXEXMP,
        NXT,
        RETAILSALEDISCOUNTPERCENT,
        NOFPMT,
        DWNPMT,
        REOPEN,
        UPDATE,
        FIREARMSBUYOUT,
        SCRDT,
        PWNP72
    }

    public enum ProductStatus
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
        PFC,
        RET,
        ACT,
        REF,
        PAID,
        FORF,
		REN,
        TERM,
        CLOSED,
        EXP,
        PFM,
        CON
    }
    public enum ItemReason
    {
        ADDD,
        BLNK,
        CACC,
        HPFI,
        MERGED,
        NOMD,
        COFFSTLN,
        COFFSTRU,
        COFFBRKN,
        COFFBURGROBB,
        COFFCASUALTY,
        COFFDESTROY,
        COFFLOSTTRNS,
        COFFINTTHEFT,
        COFFMISSING,
        COFFREPLPROP,
        COFFDONATION,
        COFFNXT
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
        IP,
        PUR,
        RET,
        LSALE,
        FOR,
        BYOUT,
        CON,
        GNEDT,
        PPMNT
  
        
    }
    public enum ServiceIndicators
    {
        Pickup,
        Extend,
        Paydown,
        Renew,
        Rollover,
        Blank,
        LT,
        Payment,
        Terminate,
        Buyout,
        LoanPmt
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
        PAYDOWN,
        BUYOUT,
        PARTIALPAYMENT
    }
    public enum MerchandiseStatus
    {
        BUY,
        IP,
        LAY,
        PFI
    }
    public enum ProductType : int
    {
        NONE = 0,
        PAWN = 1,
        BUY = 2,
        SALE = 3,
        LAYAWAY=4,
        ALL = 5
    }

    public enum ReceiptRefTypes
    {
        PAWN,
        PURCHASE,
        SALE
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
        PolSeize,    
        LoanUp,         //Not yet added to DB, just used as placeholder
        New,
        PFI,
        RTC,            
        TO,
        VEX,
        VNL,
        VPD,
        VPU,
        VRN,
        VPR,
        PUR,
        RET,
        VRET,
        PURV,
        REF,
        SALE,
        VSALE,
        VSALEREF,
        LAY,
        LAYPMT,
        VLAYPMT,
        VLAY,
        LAYREF,
        FORF,
        LAYDOWNREF,
        LAYSF,
        VFORF,
        PARTP,
        VPARTP

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

    public enum ItemSearchResultsMode
    {
        RETAIL_SALE,
        CHANGE_RETAIL_PRICE,
        REPRINT_TAG,
        CHANGE_ITEM_ASSIGNMENT_TYPE,
        CHARGEOFF
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
    public enum storeinforecord : int
    {
        STOREID = 0,
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
        ALIAS_ID = 42,
        IS_TOPS_SAFE,
        IS_INTEGRATED,
        IS_PAWN_PRIMARY,
        IS_TOPS_EXIST = 46,
        FFL = 47,
        FORCE_CLOSE_TIME = 48
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
        public FeeRefTypes FeeRefType;
        public int FeeRef;

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
        public string CreatedBy;
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
        public DateTime RefTime;
        public string ReferenceReceiptNumber;
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
        public DateTime OldPfiEligible;
        public DateTime NewPfiEligible;
        public decimal ExtensionAmount;
        public decimal Fees;
        public int ExtendedBy;
    }

    public struct LedgerReport
    {
        public DataTable CashDrawerTransactions;
        public LedgerReportType ReportType;
        public string CashDrawerName;
        public bool IsSafe;
        public string EmployeeName;
        public decimal BeginningBalance;
        public List<string> LedgerDenomination;
        public decimal ActualCashCount;
        public string OverShortComment;
    }


    public struct PartialPaymentTicketData
    {
        public int TicketNumber;
        public List<string> LoanItems;
        public decimal PrincipalReduction;
        public decimal InterestAmount;
        public decimal StorageFee;
        public decimal OtherCharges;
        public decimal SubTotal;
        public DateTime PFIDate;
        
    }

    public struct PartialPaymentReportData
    {
        public string EmployeeName;
        public string CustomerName;
        public string CustomerAddress1;
        public string CustomerAddress2;
        public string CustomerPhone;
        public decimal AmountTendered;
        public decimal TotalDueFromCustomer;
        public decimal ChangeDue;

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
        RACCEPTED
 
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

    public enum BankTransferRecord
    {
        BANKTRANSFERID=0,
        TRANSFERNUMBER,
        TRANSFERTYPE,
        TRANSFERSTATUS,
        BANKNAME,
        BANKCCOUNTNUMBER,
        CHECKNUMBER,
        TRANSFERDATE,
        TRANSFERAMOUNT,
        USERID=9
    }


    public enum OverShortCodes
    {
        O,
        N,
        S
    }

    public enum PurchaseStatus
    {
        RET,
        PUR
    }

    public enum PurchaseTypes
    {
        CUSTOMER,
        VENDOR
    }

    public enum PurchaseTenderTypes
    {
        CASHIN,
        BILLTOAP,
        CASHOUT,
        RBILLTOAP
    }

    public struct ButtonTags
    {
        public string TagName;
        public bool TellerOperation;
    }

    public enum TenderTypes : uint
    {
        CASHIN      = 0,
        BILLTOAP    = 1,     
        CASHOUT     = 2,      
        CHECK       = 3,  
        CREDITCARD  = 4,   
        DEBITCARD   = 5,    
        STORECREDIT = 6,    
        PAYPAL      = 7,
        COUPON      = 8
    }

    public enum CreditCardTypes : uint
    {
        VISA = 0,
        MASTERCARD,
        AMEX,
        DISCOVER
    }

    public enum DebitCardTypes : uint
    {
        VISA = 0,
        MASTERCARD,
        OTHER,
        AMEX,
        DISCOVER
    }

    public class TenderData
    {
        public string TenderType;
        public decimal TenderAmount;
        public string TenderAuth;
        public string MethodOfPmt;
        public string ReceiptNumber;
        public string ReversalInfo;

        public TenderData()
        {
            TenderType = string.Empty;
            TenderAmount = 0.0M;
            TenderAuth = string.Empty;
            MethodOfPmt = string.Empty;
            ReceiptNumber = string.Empty;
            ReversalInfo = string.Empty;
        }
    }

    public class ItemSearchCriteria
    {
        public int CategoryID;
        public string CategoryDescription;
        public string Manufacturer;
        public string Model;
    }

    public class TransactionAmount
    {
        public decimal SubTotalAmount;
        public decimal TotalAmount;
        public decimal SalesTaxPercentage;

        public TransactionAmount()
        {
            SubTotalAmount = 0.0M;
            TotalAmount = 0.0M;
            SalesTaxPercentage = 0.0M;
        }
    }

    public enum LayawayActivity
    {
        PAYMENT,
        FORFEITURE,
        LAYAWAY
    }

    public struct LayawayVoidData
    {
        public string ReceiptDetailNumber;
        public decimal ReceiptAmount;
        public string ReceiptType;
        public string MethodOfPayment;
        public string LoanStatus;
        public string StatusDate;
        public decimal TenderAmount;
        public string LayawayActivity;
        public string VoidMessage;
            

    }

    public enum StoreCreditStatus
    {
        ACT,
        INACT,
        DISP,
        VOID
    }

    public enum CustomerType
    {
        NONE = 1,
        RECEIPT = 2,
        DISPOSITION = 3
    }

}
