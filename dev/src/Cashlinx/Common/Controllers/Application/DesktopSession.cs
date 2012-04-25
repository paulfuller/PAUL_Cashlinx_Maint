using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow;
using Common.Controllers.Database;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Rules.Interface;
using Common.Libraries.Forms;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Config;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;

namespace Common.Controllers.Application
{
    public abstract class DesktopSession : MarshalByRefObject, IDisposable
    {
        public const string AUDIT_CARDINALITY = DesktopSessionConstants.AUDIT_CARDINALITY;
        public const string AUDIT_OVERRIDE_ID = DesktopSessionConstants.AUDIT_OVERRIDE_ID;
        public const string AUDIT_STORENUMBER = DesktopSessionConstants.AUDIT_STORENUMBER;
        public const string AUDIT_OVERRIDE_TRANS_TYPE = DesktopSessionConstants.AUDIT_OVERRIDE_TRANS_TYPE;
        public const string AUDIT_OVERRIDE_TYPE = DesktopSessionConstants.AUDIT_OVERRIDE_TYPE;
        public const string AUDIT_OVERRIDE_SUGGVAL = DesktopSessionConstants.AUDIT_OVERRIDE_SUGGVAL;
        public const string AUDIT_OVERRIDE_APPRVAL = DesktopSessionConstants.AUDIT_OVERRIDE_APPRVAL;
        public const string AUDIT_OVERRIDE_TRANSNUM = DesktopSessionConstants.AUDIT_OVERRIDE_TRANSNUM;
        public const string AUDIT_OVERRIDE_COMMENT = DesktopSessionConstants.AUDIT_OVERRIDE_COMMENT;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        //SR 09/08/2011 Added for the new couch db implementation
        public const string SSL_PORT = "6984";
        public const bool SECURE_COUCH_CONN = false;
        public List<HoldData> CustHoldsData;
        public CurrentLoggedInUserData ActiveUserData { get; protected set; }
        public ItemSearchCriteria ActiveItemSearchData { get; set; }

        public List<UserVO> assignedEmployees { set; get; }

        public PawnLoan ActivePawnLoan
        {
            get
            {
                return (this.PawnLoans == null || this.PawnLoans.Count == 0 ? null : this.PawnLoans[0]);
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                if (this.PawnLoans != null && PawnLoans.Count > 0)
                    this.PawnLoans[0] = value;
            }
        }

        public PurchaseVO ActivePurchase
        {
            get
            {
                return (this.Purchases == null || this.Purchases.Count == 0 ? null : this.Purchases[0]);
            }
            set
            {
                if (value != null)
                {
                    if (this.Purchases != null && Purchases.Count > 0)
                        this.Purchases[0] = value;
                }
            }
        }
        public CustomerVO ActiveCustomer
        {
            get
            {
                return (this.customers.Count == 0 ? null : this.customers[0]);
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                this.customers[0] = value;
                this.UpdateDesktopCustomerInformation(this.DesktopForm);

                if (!String.IsNullOrEmpty(this.customers[0].LastName))
                {
                    // see if a mandatory field (lastName) is null or empty before showing
                    this.ShowDesktopCustomerInformation(this.DesktopForm, true);
                }
            }
        }
        public SaleVO ActiveRetail
        {
            get
            {
                return (this.Sales == null || this.Sales.Count == 0 ? null : this.Sales[0]);
            }
            set
            {
                if (value != null)
                {
                    if (this.Sales != null && Sales.Count >= 0)
                        this.Sales[0] = value;
                }
            }
        }

        public LayawayVO ActiveLayaway
        {
            get
            {
                return (this.Layaways == null || this.Layaways.Count == 0 ? null : this.Layaways[0]);
            }
            set
            {
                if (value != null)
                {
                    if (this.Layaways != null && Layaways.Count >= 0)
                        this.Layaways[0] = value;
                }
            }
        }

        public TransferVO ActiveTransferIn
        {
            get
            {
                return (this.Transfers == null || this.Transfers.Count == 0 ? null : this.Transfers[0]);
            }
            set
            {
                if (value != null)
                {
                    if (this.Transfers != null && Transfers.Count >= 0)
                        this.Transfers[0] = value;
                }
            }
        }

        //Store the tab clicked on
        public FlowTabController.State TabStateClicked { get; set; }
        public bool SafeMode = false;
        public bool LockProductsTab { get; set; }
        public bool ShowOnlyHistoryTabs { get; set; }


        //App data tables for the user controls
        public DataTable EyeColorTable;
        public DataTable HairColorTable;
        public DataTable IdTypeTable;
        public DataTable CountryTable;
        public DataTable StateTable;
        public DataTable TitleSuffixTable;
        public DataTable TitleTable;
        public DataTable RaceTable;
        public DataTable HearAboutUsTable;
        public DataTable VendDataTable;
        public DataTable CustDataTable;
        public DataTable CustIdentDataTable;
        public DataTable CustPhoneDataTable;
        public DataTable CustAddrDataTable;
        public DataTable CustEmailDataTable;
        public DataTable CustNotesDataTable;
        public DataTable CustStoreCreditDataTable;

        public List<PawnAppVO> PawnApplications { get; set; }
        public List<LayawayVO> ServiceLayaways { get; set; }
        public List<PawnLoan> ServiceLoans { get; set; }
        public List<int> SkippedTicketNumbers { get; set; }
        public UserVO SelectedUserProfile { get; set; }
        public PoliceInfo PoliceInformation { get; set; }
        // TL 02-09-2010 Wipe Drive Category List
        public List<int> WipeDriveCategories { get; set; }
        public DateTime DatabaseTime { get; set; }
        public Boolean UserIsLockedOut { get; set; }
        public InquiryCriteria InquirySelectionCriteria;
        public List<UserVO> AssignedEmployees
        {
            get
            {
                return (this.assignedEmployees);
            }
        }

        //Session Variables for extensionof loans
        public bool PrintSingleMemoOfExtension { get; set; }
        public decimal CashTenderFromCustomer { get; set; }
        public decimal CashTenderToCustomer { get; set; }
        public decimal TotalExtendAmount { get; set; }
        public decimal TotalServiceAmount { get; set; }
        public decimal TotalPickupAmount { get; set; }
        public decimal TotalBuyoutAmount { get; set; }
        public decimal TotalOtherPickupItems { get; set; }
        public decimal TotalRenewalAmount { get; set; }
        public decimal TotalPaydownAmount { get; set; }
        public decimal TotalRolloverAmount { get; set; }
        //end
        //The ticket number searched for in lookup ticket
        public int TicketLookedUp { get; set; }
        public ProductType TicketTypeLookedUp { get; set; }
        public int PH_TicketLookedUp { get; set; }
        public bool PH_TicketLookedUpActive { get; set; }
        public ProductType PH_TicketTypeLookedUp { get; set; }
        //Set by lookup ticket results if Customer is not the original pledgor
        public bool CustomerNotPledgor { get; set; }

        //Stats data cache
        public DataSet CustStatsDataSet;
        public List<PairType<ProductStatus, string>> LoanStatus;
        public List<PairType<ManagerOverrideTransactionType, string>> ManagerOverrideTransactionTypes;
        public List<PairType<ManagerOverrideType, string>> ManagerOverrideTypes;
        public List<PairType<FeeTypes, string>> ServiceFeeTypes;
        public List<PairType<StateStatus, string>> TempStatus;

        internal void InitStatTypesVars()
        {
            CustStatsDataSet = null;
            LoanStatus = null;
            ManagerOverrideReasonCodes = null;
            ManagerOverrideTypes = null;
            ServiceFeeTypes = null;
            TempStatus = null;
        }

        //Gun Book edit related data
        public DataTable GunData { get; set; }
        public bool GunAcquireCustomer { get; set; }
        public CustomerVO GunBookCustomerData { get; set; }
        public string GunNumber { get; set; }

        internal void InitGunVars()
        {
            this.GunData = null;
            this.GunAcquireCustomer = false;
            this.GunBookCustomerData = null;
            this.GunNumber = string.Empty;
        }

        //Flow controller variables
        public CustomerVO MPCustomer { get; set; }
        public bool MPNameCheck { get; set; }
        public CustomerVO EXNewCustomer { get; set; }
        public DataTable EXExistingCustomers { get; set; }
        public CustomerVO EXCurrentCustomer { get; set; }
        public string EXMessageToShow { get; set; }
        public bool EXNameCheck { get; set; }
        public string EXErrorMessage { get; set; }

        internal void InitFlowControllerVars()
        {
            MPCustomer = null;
            MPNameCheck = false;
            EXNewCustomer = null;
            EXExistingCustomers = null;
            EXCurrentCustomer = null;
            EXMessageToShow = string.Empty;
            EXNameCheck = false;
            EXErrorMessage = string.Empty;
        }

        public CustomerType CustomerEditType;
        public Form DesktopForm { get; set; }
        public PawnLoan CurrentPawnLoan { get; set; }
        public StorePrinterVO BarcodePrinter { get; set; }
        public IButtonResourceManagerHelper ButtonResourceManagerHelper { get; set; }
        public WebServiceProKnow CallProKnow { get; set; }
        public string CashDrawerName { get; set; }
        public CategoryNode CategoryXML { get; set; }
        public USBUtilities.USBDriveStorage CurrentUSBStorage { get; set; }
        protected USBUtilities.USBDriveStorage UsbDriveStorage;
        public List<LayawayVO> CustomerHistoryLayaways { get; set; }
        public List<PawnLoan> CustomerHistoryLoans { get; set; }
        public List<SaleVO> CustomerHistorySales { get; set; }
        public CurrentContext DescribeItemContext { get; set; }
        public int DescribeItemPawnItemIndex { get; set; }
        public ProKnowMatch DescribeItemSelectedProKnowMatch { get; set; }
        public string DisplayName { get; set; }
        public string FullUserName { get; set; }
        public bool GenerateTemporaryICN { get; set; }
        public HistoryTrack HistorySession { get; set; }
        public List<ShopCalendarVO> HolidayCalendar
        {
            get { return this.holidayCalendar; }
            set { this.holidayCalendar = value; }
        }
        public bool IsSkipLDAP { get; set; }
        public StorePrinterVO LaserPrinter { get; set; }
        public StorePrinterVO DotMatrixPrinter { get; set; }
        public List<LayawayVO> Layaways { get; set; }
        public UserVO LoggedInUserSecurityProfile { get; set; }
        public List<PairType<ManagerOverrideReason, string>> ManagerOverrideReasonCodes { get; set; }
        public DataTable MerchandiseManufacturers { get; set; }
        public Dictionary<string, BusinessRuleVO> PawnBusinessRuleVO { get; set; }
        public List<PawnLoan> PawnLoans { get; set; }
        public PawnSecApplication PawnSecApplication { get; set; }
        public StorePrinterVO PDALaserPrinter { get; set; }
        public List<PMetalInfo> PMetalData { get; set; }
        public PawnRulesSystemInterface PawnRulesSys { get; set; }
        public bool PurchasePFIAddItem { get; set; }
        public decimal PFIMailerFee { get; set; }
        public List<PurchaseVO> Purchases { get; set; }
        public StorePrinterVO ReceiptPrinter { get; set; }
        public string ReplaceICN { get; set; }
        public int ReplaceDocNumber { get; set; }
        public long ReplaceGunNumber { get; set; }
        public int ReplaceNumberOfTags { get; set; }
        public string ReplaceDocType { get; set; }
        public ResourceProperties ResourceProperties { get; set; }
        public bool ScannedCredentials { get; set; }
        public string ScrapAnswerId { get; set; }
        public List<string> ScrapTypes { get; set; }
        public int SelectedItemOrder { get; set; }
        public List<int> SelectedPFIMergeItemIndex { get; set; }
        public List<ServiceOffering> ServiceOfferings { get; set; }

        public List<ShopCalendarVO> ShopCalendar
        {
            get { return this.shopCalendar; }
            set { this.shopCalendar = value; }
        }
        public List<StoneInfo> StonesData { get; set; }
        public bool UpdateRequiredFieldsForCustomer { get; set; }
        public string UserName { get; set; }
        public List<Variance> VarianceRates { get; set; }
        public List<PawnLoan> ExtensionLoans { get; set; }
        public List<PawnLoan> BuyoutLoans { get; set; }
        public List<PawnLoan> PickupLoans { get; set; }
        public List<PawnLoan> RolloverLoans { get; set; }
        public List<PawnLoan> RenewalLoans { get; set; }
        public List<PawnLoan> PaydownLoans { get; set; }
        public List<PawnLoan> PartialPaymentLoans { get; set; }
        public List<SaleVO> Sales { get; set; }
        public List<TransferVO> Transfers { get; set; }
        public TransferMethod TransferMethod { get; set; }
        public List<PurchaseVO> CustomerHistoryPurchases { get; set; }
        public List<PawnLoan> PawnLoanKeys { get; set; }
        public List<PawnLoan> PawnLoanKeysAuxillary { get; set; }
        public List<Receipt> PawnReceipt { get; set; }
        public List<HoldData> HoldsData { get; set; }
        public AppMode CurrentAppMode { get; set; }
        public bool ApplicationExit { get; set; }
        public List<CustomerVO> customers { get; set; }
        public VendorVO ActiveVendor { get; set; }
        public Timer timer { get; set; }
        public IsolationLevel IsoLevel { set; get; }
        public ProductType Product { set; get; }
        public string CashDrawerId { set; get; }
        public LookupCustomerSearchData ActiveLookupCriteria { get; set; }
        public LookupVendorSearchData LookupCriteria { get; set; }
        public List<PawnLoan> PawnLoans_Auxillary;
        protected List<ShopCalendarVO> shopCalendar;
        protected List<ShopCalendarVO> holidayCalendar;
        public List<string> TellerOperations { get; set; }
        public List<string> TenderTypes;
        public List<string> TenderAmounts;
        public List<string> TenderReferenceNumber;
        public List<TenderEntryVO> TenderData;
        public List<TenderEntryVO> RefundEntries;
        public List<TenderEntryVO> PartialRefundEntries;
        public List<string> UnverifiedCashDrawers;
        public bool ForceCloseMessageShown;
        //Start
        //Global variables for pickup process
        //and total pickup amount to be used by process tender after checkout from pickup

        public bool BackgroundCheckCompleted { get; set; }

        public decimal BackgroundCheckFeeValue { get; set; }

        public bool PickupProcessContinue { get; set; }





        /// <summary>
        /// 
        /// </summary>



        public string StoreSafeID { get; set; }
        public string StoreSafeName;
        public int SelectedStoreCashTransferNumber { get; set; }
        public string SelectedStoreCashTransferID { get; set; }
        public CashTransferVO CashTransferData { get; set; }
        public DataSet MdseTransferData { get; set; }
        public string SelectedTransferNumber { get; set; }
        public decimal CashDrawerBeginningBalance { get; set; }
        public decimal SafeBeginningBalance { get; set; }
        //private AppWorkflowController appController;
        protected bool LoginCancel;
        protected string workStationId;
        protected DataTable CashDrawerAssignments;
        protected DataTable CashDrawerAuxAssignments;
        protected ProcessingMessage procMsgFormPwd;
        protected bool bCashDrawerOpen;
        protected bool bCashDrawerClosedUnverified;
        protected bool safeOpen;
        protected bool safeClosedUnverified = false;
        protected bool closedUnverifiedCashDrawers = false;
        public string CashDrawerUserId { get; set; }
        public string TTyId { set; get; }
        public DataTable StoreCashDrawerAssignments
        {
            get
            {
                GetCashDrawerAssignmentsForStore();
                return CashDrawerAssignments;
            }
        }

        public bool LoggedInUserSafeAccess { get; set; }
        public string CurPawnAppId { set; get; }
        public string Clothing { set; get; }
        public bool MenuEnabled { set; get; }
        public bool ReleaseToClaimant { get; set; }
        public bool CompleteLayaway;
        public bool LayawayMode;
        public LayawayPaymentCalculator LayawayPaymentCalc;
        public bool CompleteSale;
        public TransactionAmount TenderTransactionAmount;
        public string ReceiptToRefund;
        public bool ClosedUnverifiedCashDrawer;
        public bool ClosedUnverifiedSafe;
        public bool ManagerOverrideBuyLimit;
        public bool ManagerOverrideLoanLimit;
        public bool ShopCreditFlow;
        public bool BuyReturnIcn;
        public bool DisableCoupon;
        public bool CashSaleCustomer;
        public bool StartNewPawnLoan;
        public bool PriorDateCDBalance;
        public bool ServicePawnLoans;
        public string BalanceOtherCashDrawerID = string.Empty;
        public string BalanceOtherCashDrawerName = string.Empty;
        public decimal BalanceOtherBegBalance = 0.0m;
        public bool OtherCDBalanced;
        public bool TrialBalance;

        public List<int> OverrideTransactionNumbers { get; set; }
        protected Dictionary<PairType<string, string>, PairType<PawnLoan, bool>> loanKeysLoaded;
        public Dictionary<PairType<string, string>, PairType<PawnLoan, bool>> LoanKeysLoaded
        {
            get
            {
                return (this.loanKeysLoaded);
            }
        }

        public TenderEntryVO CouponTender;
        protected UserDesktopState userState;
        protected object usbDriveMutex = new object();
        public Item GunItemData;
        public bool VendorValidated;
        public bool VenderFFLRequired;


        public SiteId CurrentSiteId
        {
            get { return (GlobalDataAccessor.Instance.CurrentSiteId); }
            set
            {
                if (value != null)
                {
                    GlobalDataAccessor.Instance.CurrentSiteId = value;
                }
            }
        }
        public AppWorkflowController AppController { get; set; }

        public KeyValuePair<string, string> LastIdUsed
        {
            get; 
            set;
        }

        public bool beginTransactionBlock()
        {
            return (GlobalDataAccessor.Instance.beginTransactionBlock());
        }

        public bool endTransactionBlock(EndTransactionType eType)
        {
            return (GlobalDataAccessor.Instance.endTransactionBlock(eType));
        }

        public bool InTransactionBlock()
        {
            return (GlobalDataAccessor.Instance.InTransactionBlock());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pdfFilePath"></param>
        /// <param name="waitForExit"></param>
        public static void ShowPDFFile(string pdfFilePath, bool waitForExit)
        {
            PdfLauncher.Instance.ShowPDFFile(pdfFilePath, waitForExit);
        }

        protected void auditLogEnabledChangeHandlerBase(bool oldEnabled, bool newEnabled)
        {
            if (oldEnabled == newEnabled)
            {
                return;
            }
            //Nothing right now...may change when we get to a web service
        }

        protected void auditLogMessageHandlerBase(IDictionary<string, object> auditData)
        {
            if (CollectionUtilities.isEmpty(auditData))
            {
                return;
            }
        }

        public abstract void GetCashDrawerAssignmentsForStore();
        public abstract void Setup(Form desktopForm);
        public abstract void UpdateDesktopCustomerInformation(Form form);
        public abstract void ShowDesktopCustomerInformation(Form form, bool b);
        public abstract void ClearCustomerList();
        public abstract void ClearPawnLoan();
        public abstract void ClearLoggedInUser();
        public abstract void ClearSessionData();
        public abstract void PerformAuthorization();
        public abstract void PerformAuthorization(bool chgUsrPasswd);
        public abstract bool IsButtonAvailable(string buttonTagName, SiteId currentSiteId);
        public abstract bool IsButtonTellerOperation(string buttonTagName);
        public abstract bool IsUserAssignedCashDrawer(string username, string cashdrawer, out string cashDrawerflag);
        public abstract void PrintTags(Item _Item, bool reprint);
        public abstract void PrintTags(Item _Item, CurrentContext context);
        public abstract void ScanParse(Object sender, string sBarCodeData, TextBoxBase userNameControl, TextBoxBase passwordControl, ButtonBase submitBtn);
        public abstract bool WriteUsbData(string data);
        public abstract void DeviceRemovedEventHandler(object sender, USBUtilities.DriveDetectorEventArgs e);
        public abstract void DeviceArrivedEventHandler(object sender, USBUtilities.DriveDetectorEventArgs e);
        public abstract void Dispose();
        public abstract void showProcessTender(ProcessTenderProcedures.ProcessTenderMode processTenderMode);
        public abstract void PerformCashDrawerChecks(out bool checkPassed);
        public abstract void CheckOpenCashDrawers(out bool openCD);
        public abstract void UpdateShopDate(Form fm);
        public abstract void GetPawnBusinessRules();
    }
}
