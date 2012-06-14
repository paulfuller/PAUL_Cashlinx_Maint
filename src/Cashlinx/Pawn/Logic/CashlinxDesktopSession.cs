using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow;
using Common.Controllers.Database;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Network;
using Common.Controllers.Rules.Interface.Impl;
using Common.Controllers.Security;
using Common.Libraries.Forms;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Config;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Common.Libraries.Utility.Type;
using Pawn.Flows.AppController.Impl;
using Pawn.Forms;
using Pawn.Forms.Pawn.Loan;
using Pawn.Forms.Pawn.Loan.ProcessTender;
using Pawn.Forms.Pawn.ShopAdministration;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Logic
{
    /*
    * Utilizing thread safe lock free singleton strategy 
    * Singleton implementation strategy from http://www.yoda.arachsys.com/csharp/singleton.html
    */
    public sealed class CashlinxDesktopSession : DesktopSession
    {
        public const int USERNAME_MAXLEN = 5;
        public const string USB_SECURE_FILENAME = "PzIISecTrx.XFX";

#if __MULTI__
        // ReSharper disable InconsistentNaming
        static readonly object mutexObj = new object();
        static readonly Dictionary<int, CashlinxDesktopSession> multiInstance = 
        new Dictionary<int, CashlinxDesktopSession>();
        // ReSharper restore InconsistentNaming
#else
        static readonly CashlinxDesktopSession instance = new CashlinxDesktopSession();

#endif

        /// <summary>
        /// Static constructor - forces compiler to initialize the object prior to any code access
        /// </summary>
        static CashlinxDesktopSession()
        {
        }

        /// <summary>
        /// Static instance property accessor
        /// </summary>
        public static CashlinxDesktopSession Instance
        {
            get
            {
#if (!__MULTI__)
                return (instance);
#else
                lock (mutexObj)
                {
                int tId = Thread.CurrentThread.ManagedThreadId;
                if (multiInstance.ContainsKey(tId))
                {
                return (multiInstance[tId]);
                }
                var cds = new CashlinxDesktopSession();
                multiInstance.Add(tId, cds);
                cds.Setup(null);
                return (cds);
                }
#endif
            }
        }

        //SR 8/26/2009
        private BasicExceptionHandler exHandler;
        private FileLogger fileLogger;
        private AuditLogger auditLogger;
        private Timer forceCloseTimer;
        private MainFlowExecutor mainFlowExecutor;
        //private DataTable CashDrawerAuxAssignments;
        private bool skipLDAP;



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool exceptionCallbackMethod()
        {
#if !__MULTI__
            var bEx = BasicExceptionHandler.Instance;
            FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Exception Callback Method Has Executed");
            if (bEx == null)
                return (true);
            if ((bEx.ApplicationExceptions != null && bEx.ApplicationExceptions.Count > 0) ||
                (bEx.SystemExceptions != null && bEx.SystemExceptions.Count > 0) ||
                (bEx.BaseExceptions != null && bEx.BaseExceptions.Count > 0))
            {
                var sF = new StringBuilder();
                var sb = new StringBuilder();
                sb.AppendLine("Exceptions Occurred:");
                int j = 0;
                if (bEx.ApplicationExceptions != null && bEx.ApplicationExceptions.Count > 0)
                {
                    foreach (var aEx in bEx.ApplicationExceptions)
                    {
                        if (aEx == null)
                        {
                            continue;
                        }
                        var msg = string.Format("Application Exception[{0}] = {1}, {2}", j, aEx,
                                                (aEx.StackTrace ?? "NoStackTrace"));
                        if (j == 0)
                            sF.AppendLine(msg);
                        sb.AppendLine(msg);
                        j++;
                    }
                    j = 0;
                }

                if (bEx.SystemExceptions != null && bEx.SystemExceptions.Count > 0)
                {
                    foreach (var aEx in bEx.SystemExceptions)
                    {
                        if (aEx == null)
                        {
                            continue;
                        }
                        var msg = string.Format("System Exception[{0}] = {1}, {2}", j, aEx,
                                                (aEx.StackTrace ?? "NoStackTrace"));
                        if (j == 0)
                        {
                            sF.AppendLine(msg);
                        }
                        sb.AppendLine(msg);
                        j++;
                    }
                    j = 0;
                }

                if (bEx.BaseExceptions != null && bEx.BaseExceptions.Count > 0)
                {
                    foreach (var aEx in bEx.BaseExceptions)
                    {
                        if (aEx == null)
                        {
                            continue;
                        }
                        var msg = string.Format("Base Exception[{0}] = {1}, {2}", j, aEx, (aEx.StackTrace ?? "NoStackTrace"));
                        if (j == 0)
                            sF.AppendLine(msg);
                        sb.AppendLine(msg);
                        j++;
                    }
                }

                FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Errors: {0}", sb);
                FileLogger.Instance.flush();
                if (string.Equals(Properties.Resources.ExceptionBoxEnabled, Boolean.TrueString, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(sF.ToString(), "Error(s) Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //Flush the exception handler
                BasicExceptionHandler.Instance.clearAllExceptions();
                return (true);
            }
#endif
            return (false);
        }

        private void auditLogEnabledChangeHandler(bool oldEnabled, bool newEnabled)
        {
            if (oldEnabled == newEnabled)
            {
                return;
            }
            //Nothing right now...may change when we get to a web service
        }

        private void logAuditMessageHandler(IDictionary<string, object> auditData)
        {
            if (CollectionUtilities.isEmpty(auditData))
            {
                return;
            }

            //Get audit type first
            var auditTypeVal = AuditLogType.OTHER;
            if (auditData.ContainsKey(AuditLogger.TYPEFIELD))
            {
                var auditTypeObj = auditData[AuditLogger.TYPEFIELD];
                if (auditTypeObj != null)
                {
                    auditTypeVal = (AuditLogType)auditTypeObj;
                }
            }

            //Get audit date
            var auditDate = DateTime.Now.Date;
            var auditTime = DateTime.Now.TimeOfDay;
            if (auditData.ContainsKey(AuditLogger.DATEFIELD))
            {
                var auditDateObj = auditData[AuditLogger.DATEFIELD];
                if (auditDateObj != null)
                {
                    auditDate = (DateTime)auditDateObj;
                }
            }

            //Get audit time
            if (auditData.ContainsKey(AuditLogger.TIMEFIELD))
            {
                object auditTimeObj = auditData[AuditLogger.TIMEFIELD];
                if (auditTimeObj != null)
                {
                    auditTime = (TimeSpan)auditTimeObj;
                }
            }

            //Set audit date with added time span value
            auditDate = auditDate.Add(auditTime);

            switch (auditTypeVal)
            {
                case AuditLogType.OVERRIDE:
                    //Retrieve data from the audit data map (mirror the wrapper call for now)
                    var storeNumber =
                    CollectionUtilities.GetIfKeyValid(auditData, AUDIT_STORENUMBER, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);
                    var overrideID =
                    CollectionUtilities.GetIfKeyValid(auditData, AUDIT_OVERRIDE_ID, UserName);
                    var dataArrayCardinality =
                    CollectionUtilities.GetIfKeyValid(auditData, AUDIT_CARDINALITY, 0);
                    ManagerOverrideTransactionType[] arManagerOverrideTransactionType =
                    CollectionUtilities.GetIfKeyValid(auditData, AUDIT_OVERRIDE_TRANS_TYPE, new ManagerOverrideTransactionType(), dataArrayCardinality);
                    var arManagerOverrideType =
                    CollectionUtilities.GetIfKeyValid(auditData, AUDIT_OVERRIDE_TYPE, new ManagerOverrideType(), dataArrayCardinality);
                    var arSuggestedValue =
                    CollectionUtilities.GetIfKeyValid(auditData, AUDIT_OVERRIDE_SUGGVAL, 0.0M, dataArrayCardinality);
                    var arApprovedValue =
                    CollectionUtilities.GetIfKeyValid(auditData, AUDIT_OVERRIDE_APPRVAL, 0.0M, dataArrayCardinality);
                    var arTransactionNumber =
                    CollectionUtilities.GetIfKeyValid(auditData, AUDIT_OVERRIDE_TRANSNUM, 0, dataArrayCardinality);
                    string comment =
                    CollectionUtilities.GetIfKeyValid(auditData, AUDIT_OVERRIDE_COMMENT, string.Empty);

                    //Start transaction block
                    //CashlinxDesktopSession.Instance.beginTransactionBlock();

                    //Invoke stored procedure
                    string errorCode;
                    string errorText;
                    bool sProcSuccess = AuditLogProcedures.ManagerOverrideReason(
                        auditDate, storeNumber, overrideID,
                        arManagerOverrideTransactionType,
                        arManagerOverrideType,
                        arSuggestedValue,
                        arApprovedValue,
                        arTransactionNumber,
                        comment, out errorCode, out errorText);

                    //Verify that the call succeeded
                    if (sProcSuccess == false)
                    {
                        BasicExceptionHandler.Instance.AddException("Cannot invoke override reason stored procedure (Audit Log FAIL)", new ApplicationException("Audit Log Failure: Overrides"));
                        //CashlinxDesktopSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                        return;
                    }
                    //CashlinxDesktopSession.Instance.endTransactionBlock(EndTransactionType.COMMIT);
                    break;
                case AuditLogType.OTHER:
                    //Do nothing now
                    break;
                default:
                    return;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public CashlinxDesktopSession()
        {
            PawnSecApplication = PawnSecApplication.Cashlinx;
            ResourceProperties = new ResourceProperties();
            ButtonResourceManagerHelper = new ButtonResourceManagerHelper();
            ActiveItemSearchData = new ItemSearchCriteria();
            var confRef = SecurityAccessor.Instance.EncryptConfig;
            this.shopCalendar = new List<ShopCalendarVO>(7);
            this.holidayCalendar = new List<ShopCalendarVO>(7);
            this.userState = UserDesktopState.NOTLOGGEDIN;
            this.UserName = string.Empty;
            this.customers = new List<CustomerVO>(1) { new CustomerVO() };
            this.PawnApplications = new List<PawnAppVO>(1);
            this.PawnLoans = new List<PawnLoan>(1) { new PawnLoan() };
            this.Layaways = new List<LayawayVO>();
            this.Purchases = new List<PurchaseVO>(1) { new PurchaseVO() };
            this.Sales = new List<SaleVO>();
            this.CustomerHistoryLoans = new List<PawnLoan>(1);
            this.PawnLoanKeys = new List<PawnLoan>(1);
            this.PawnLoanKeysAuxillary = new List<PawnLoan>(1);
            this.ServiceLoans = new List<PawnLoan>(1);
            this.PartialPaymentLoans = new List<PawnLoan>(1);
            this.assignedEmployees = new List<UserVO>(1);
            this.CashDrawerName = string.Empty;
            this.CashDrawerId =
                (confRef == null ||
                 confRef.ClientConfig == null ||
                 confRef.ClientConfig.ClientConfiguration == null) ?
                 "0" : confRef.ClientConfig.ClientConfiguration.TerminalNumber.ToString();
            this.TTyId = this.CashDrawerId;
            this.IsoLevel = IsolationLevel.ReadCommitted;
            this.TenderTransactionAmount = new TransactionAmount();
            this.MenuEnabled = true;
            this.loanKeysLoaded = new Dictionary<PairType<string, string>, PairType<PawnLoan, bool>>();
            this.PawnReceipt = new List<Receipt>();
            this.CustomerNotPledgor = false;
            this.ReleaseToClaimant = false;
            this.UserIsLockedOut = false;
            this.skipLDAP = false;
            ApplicationExit = false;
            ActiveUserData = new CurrentLoggedInUserData();
            ActiveVendor = null;
            GenerateTemporaryICN = false;
            LastIdUsed = new KeyValuePair<string, string>(String.Empty, String.Empty);
            this.LayawayPaymentCalc = null;
            this.LayawayMode = false;
            this.ForceCloseMessageShown = false;
            if (confRef == null)
            {
                return;
            }

            //Global data initialize and decrypt
            GlobalDataAccessor.Instance.Init(this, confRef, "PawnApp", auditLogEnabledChangeHandler, logAuditMessageHandler, true, this.exceptionCallbackMethod);
            this.DatabaseTime = GlobalDataAccessor.Instance.DatabaseTime;
            //Set file logger
            this.fileLogger = FileLogger.Instance;

            //Create main flow executor
            this.mainFlowExecutor = new MainFlowExecutor();

            //Create the application controller
            this.AppController = new AppWorkflowController(this, this.mainFlowExecutor);

            //Create pawn auxilliary container
            this.PawnLoans_Auxillary = new List<PawnLoan>();

            string errorCode;
            string errorText;
            //Load the button tags that are available for the store
            List<ButtonTags> buttonTagNames;
            var retVal = ShopProcedures.GetButtonTagNames(GlobalDataAccessor.Instance.OracleDA,
                                                           confRef.ClientConfig.StoreSite.StoreNumber, out buttonTagNames,
                                                           out errorCode, out errorText);
            if (retVal && buttonTagNames != null && buttonTagNames.Count > 0)
            {
                CurrentSiteId.AvailableButtons = (from tagName in buttonTagNames select tagName.TagName).ToList();
                TellerOperations = (
                    from tagName in buttonTagNames
                    where tagName.TellerOperation
                    select
                        StringUtilities.removeFromString(tagName.TagName.ToLowerInvariant(), "button")).ToList();
            }

            //Load the sales tax info for the store
            List<StoreTaxVO> storeTaxes;
            var retval = RetailProcedures.GetStoreTaxInfo(GlobalDataAccessor.Instance.OracleDA,
                                                           confRef.ClientConfig.StoreSite.StoreNumber,
                                                           out storeTaxes, out errorCode, out errorText);
            if (retval && storeTaxes != null && storeTaxes.Count > 0)
            {
                CurrentSiteId.StoreTaxes = storeTaxes;
            }

            //Retrieve shop calendar
            List<ShopCalendarVO> calendarDays;
            List<ShopCalendarVO> holidayDays;

            retVal = ShopProcedures.GetShopCalendar(
                GlobalDataAccessor.Instance.OracleDA,
                confRef.ClientConfig.StoreSite.StoreNumber,
                out calendarDays,
                out holidayDays,
                out errorCode,
                out errorText);
            if (retVal)
            {
                this.shopCalendar = calendarDays;
                this.holidayCalendar = holidayDays;
            }

            //Initialize usb storage instance
            this.UsbDriveStorage = new USBUtilities.USBDriveStorage();

            //Init force close timer to null
            this.forceCloseTimer = null;

            //Initialize empty printer entries
            this.PDALaserPrinter = new StorePrinterVO(StorePrinterVO.StorePrinterType.LASER, string.Empty, 0);
            this.LaserPrinter = new StorePrinterVO(StorePrinterVO.StorePrinterType.LASER, string.Empty, 0);
            this.ReceiptPrinter = new StorePrinterVO(StorePrinterVO.StorePrinterType.RECEIPT, string.Empty, 0);
            this.BarcodePrinter = new StorePrinterVO(StorePrinterVO.StorePrinterType.BARCODE, string.Empty, 0);
            this.IndianaPoliceCardPrinter = new StorePrinterVO(StorePrinterVO.StorePrinterType.INDIANAPOLICECARD, string.Empty, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void ClearPawnLoan()
        {
            this.CurrentSiteId.LoanAmount = 0.0M;
            if (PawnLoans != null)
            {
                this.PawnLoans.Clear();
            }
            ProcessTenderController.Instance.ResetLoanData();
            this.loanKeysLoaded.Clear();
            this.PawnLoanKeys.Clear();
            this.PawnLoanKeysAuxillary.Clear();
            this.CustomerHistoryLoans.Clear();
            this.ServiceLoans.Clear();
            this.PartialPaymentLoans.Clear();
            if (this.ServiceLayaways != null)
            {
                var layaways = ServiceLayaways.Where(l => l.TempStatus != StateStatus.BLNK).ToList();
                ClearActiveLayawaysTempStatus(layaways);
                this.ServiceLayaways.Clear();
            }
            this.PawnReceipt.Clear();
            this.PawnLoans_Auxillary.Clear();
            this.BackgroundCheckCompleted = false;
            this.TotalPickupAmount = 0.0M;
            this.TicketLookedUp = 0;
            this.TicketTypeLookedUp = ProductType.NONE;
            this.PH_TicketLookedUp = 0;
            this.PH_TicketLookedUpActive = false;
            this.PH_TicketTypeLookedUp = ProductType.NONE;
            this.CashTenderFromCustomer = 0;
            this.LockProductsTab = false;
            this.CustomerNotPledgor = false;
            this.ShowOnlyHistoryTabs = false;
            this.MPCustomer = null;
            this.CurrentPawnLoan = null;
            CurPawnAppId = null;
            CustStatsDataSet = null;
            if (Purchases != null)
            {
                this.Purchases.Clear();
                this.DescribeItemSelectedProKnowMatch = null;
                this.ActivePurchase = null;
                this.ActiveVendor = null;
            }
            if (Sales != null)
            {
                string errorCode;
                string errorText;
                if (ActiveRetail != null)
                {
                    foreach (RetailItem selectedItem in ActiveRetail.RetailItems)
                    {
                        if (selectedItem.mDocType != "7")
                        {
                            RetailProcedures.LockItem(GlobalDataAccessor.Instance.DesktopSession, selectedItem.Icn, out errorCode, out errorText, "N");
                        }
                    }
                }

                Sales.Clear();
                ActiveRetail = null;
            }

            if (Layaways != null)
            {
                Layaways.Clear();
                ActiveLayaway = null;
            }
            ReceiptToRefund = string.Empty;
            instance.ShopCreditFlow = false;
            LastIdUsed = new KeyValuePair<string, string>(String.Empty, String.Empty);
        }

        private void ClearActiveLayawaysTempStatus(IEnumerable<LayawayVO> layaways)
        {
            foreach (var layaway in layaways)
            {
                string errorCode;
                string errorText;
                RetailProcedures.SetLayawayTempStatus(layaway.TicketNumber,
                                                        layaway.StoreNumber,
                                                        string.Empty,
                                                        out errorCode,
                                                        out errorText);
            }

        }


        /// <summary>
        /// 
        /// </summary>
        public override void ClearCustomerList()
        {
            this.ClearPawnLoan();
            CustStatsDataSet = null;
            ActiveCustomer = null;
            this.customers = new List<CustomerVO> { new CustomerVO() };
            this.ShowDesktopCustomerInformation(this.DesktopForm, false);
        }

        public override void ClearLoggedInUser()
        {
            var encCfg = SecurityAccessor.Instance.EncryptConfig;
            if (encCfg.ClientConfig.ClientConfiguration.CPNHSEnabled)
            {
                CPNHSController.Instance.EndSession(this.UserName, encCfg.ClientConfig.MachineName, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, ShopDateTime.Instance.ShopDateCurTime);
            }
            this.UserName = string.Empty;
            UpdateDesktopUserName(this.DesktopForm);
        }

        /// <summary>
        /// 
        /// </summary>
        public void RestoreMenu()
        {
            if (this.DesktopForm == null)
            {
                return;
            }
            var cdFm = (NewDesktop)this.DesktopForm;
            cdFm.handleEndFlow(null);
        }

        // Global Setup Method for any pre-loading requirements of Desktop Forms or Panels
        public override void Setup(Form deskForm)
        {
#if DEBUG
            //Resets myForm = new Resets();
            //TerminalClient myForm = new TerminalClient();
            //AdminSection myForm = new AdminSection();
            //myForm.ShowDialog();
            //myForm.Dispose();
            /*GetIntermecPrinter();
            IntermecBarcodeTagPrint intermecBarcodeTagPrint = new IntermecBarcodeTagPrint("",
            Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber),
            GlobalDataAccessor.Instance.CurrentSiteId.TerminalId,
            IntermecBarcodeTagPrint.PrinterModel.Intermec_PM4i,
            CashlinxDesktopSession.Instance.IntermecPrinterName);
            intermecBarcodeTagPrint.PrintUserBarCode("G+0lhbyECaiQVcaXxWg0fw==", "test 1");
            intermecBarcodeTagPrint.PrintUserBarCode("G+0lhbyECahyPbICADLsCw==", "test 2");
            intermecBarcodeTagPrint.PrintUserBarCode("G+0lhbyECagVGu/cEMtz335ZD6G4Kw2e", "test 3");
            intermecBarcodeTagPrint.PrintUserBarCode("RroYbj9f1iGQVcaXxWg0fw==", "test 4");
            intermecBarcodeTagPrint.PrintUserBarCode("RroYbj9f1iFyPbICADLsCw==", "test 5");
            intermecBarcodeTagPrint.PrintUserBarCode("RroYbj9f1iEVGu/cEMtz335ZD6G4Kw2e", "test 6");
            intermecBarcodeTagPrint.PrintUserBarCode("lQ/uBFzMuNmWc5aEKWbgTA==", "test 7");
            intermecBarcodeTagPrint.PrintUserBarCode("lQ/uBFzMuNkCG28MgV8Fu35ZD6G4Kw2e", "test 8");
            intermecBarcodeTagPrint.PrintUserBarCode("lQ/uBFzMuNnf+qXdgaWRrHBQPoidDdmZ", "test 9");*/
            //Tender In situation
            /*var tendInForm = new TenderIn();
            tendInForm.SetAmountDueFromCustomer(125.00M);
            tendInForm.ShowDialog();
            //Tender out normal
            var tendOutForm = new TenderOut();
            tendOutForm.SetAmountDueToCustomer(345.00M);
            tendOutForm.ShowDialog();
            //Tender out refund
            var refundEntries = new List<TenderEntryVO>();
            var tendFm = new TenderEntryForm(TenderTypes.CASHOUT);
            var res = tendFm.ShowDialog();
            if (res == DialogResult.OK)refundEntries.Add(tendFm.TenderEntry);
            tendFm = new TenderEntryForm(TenderTypes.CREDITCARD);
            res = tendFm.ShowDialog();
            if (res == DialogResult.OK)refundEntries.Add(tendFm.TenderEntry);
            tendFm = new TenderEntryForm(TenderTypes.DEBITCARD);
            res = tendFm.ShowDialog();
            if (res == DialogResult.OK)refundEntries.Add(tendFm.TenderEntry);
            tendFm = new TenderEntryForm(TenderTypes.CHECK);
            res = tendFm.ShowDialog();
            if (res == DialogResult.OK)refundEntries.Add(tendFm.TenderEntry);
            decimal amt = 0.00M;
            foreach(var j in refundEntries)
            {
            if (j == null)
            continue;
            amt += j.Amount;
            }
            tendOutForm = new TenderOut(refundEntries);
            tendOutForm.SetAmountDueToCustomer(amt);
            tendOutForm.ShowDialog();*/
            //Test barcode socket print
            //MessageBox.Show("Sending format codes to printer");
            //First send the formats
            /*            string msg;
            var res = true;//PrintingUtilities.SendASCIIStringToPrinter("192.168.106.105",
            //                                 9100,
            //                                 Resources.IPL_Print_Formats, out msg);
            if (!res)
            {
            MessageBox.Show("Could not send bar code formats to printer");
            }
            else
            {
            var intermecBarcodeTagPrint = new IntermecBarcodeTagPrint(
            "",
            Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber),
            CashlinxDesktopSession.Instance.IntermecPrinterModel,
            CashlinxDesktopSession.Instance.IntermecPrinterIpAddress,
            CashlinxDesktopSession.Instance.IntermecPrinterPort);
            intermecBarcodeTagPrint.PrintUserBarCode("TEST TEST", "TEST TEST");
            MessageBox.Show("Fired print job");
            }
            var message = "abcdefghijklmnopqrstuvwxyz-0123456789";
            var now = DateTime.Now;
            var key = now.Month.ToString().PadLeft(2, '0') + "-" +
                      now.Day.ToString().PadLeft(2, '0') + "-" +
                      now.Year.ToString();
            var encoder = Encoding.Default;
            var keyMD5 = StringUtilities.GenerateRawMD5Hash(key, encoder);
            var msgHex = StringUtilities.ConverByteArrayToHexString(encoder.GetBytes(message));
            var encMsg = keyMD5 + msgHex + keyMD5;
            var sb = new StringBuilder();
            sb.AppendFormat("PreMsg = {0}{1}EncMsg = {2}", 
                msgHex,
                Environment.NewLine,
                encMsg);
            //MessageBox.Show(sb.ToString());

            */


#endif
            //Allow the user to keep the login screen up until they are tired of trying
            //and retrying to log in to the application or they utilize all of their
            //attempts
#if !__MULTI__
            //Check for skip LDAP flag
            string skipStr = Properties.Resources.SkipLDAP;
            if (!Boolean.TryParse(skipStr, out skipLDAP))
            {
                this.skipLDAP = false;
            }

            //procMsgFormPwd = new ProcessingMessage("* PERFORMING LDAP AUTHENTICATION *");
            //PerformAuthorization();

            /*  if (string.IsNullOrEmpty(this.UserName))
            {
            try
            {
            Application.Exit();
            }
            catch
            {
            throw new ApplicationException(
            "Application has exited due to an invalid user name");
            }
            finally
            {
            throw new ApplicationException(
            "Application has exited due to an invalid user name");
            }
            }*/



#else
            this.userState = UserDesktopState.LOGGEDIN;
            this.UserName = "agent";
            this.FullUserName = "aagent";
#endif

#if !__MULTI__
            // Set up the History Session Object
            HistorySession = new HistoryTrack(deskForm);
            // Set CashlinxDesktopSession's desktop form
            this.DesktopForm = deskForm;
            UpdateVersionLabel(this.DesktopForm);
            UpdateShopDate(this.DesktopForm);

            //Initialize the force close timer
            InitForceCloseTimer();
            procMsgFormPwd = new ProcessingMessage("* INITIALIZING APPLICATION *");
            procMsgFormPwd.Show();


#endif
            // Load barcode formats during startup until Admin section created
            try
            {
                procMsgFormPwd.Message = "* RETRIEVING PRINTER INFORMATION *";
                GetReceiptPrinter();
                GetIntermecPrinter();
                GetLaserPrinter();
                GetPDALaserPrinter();
                GetIndianaPoliceCardPrinter();
                //LoadBarcodeFormats();
                procMsgFormPwd.Message = "* LOADING APPLICATION DATA *";
                GetPawnBusinessRules();
                PopulateCategoryXML();
                GetMerchandiseManufacturers();
                GetMetalStonesValues();
                GetPickListValues();
                GetVarianceRates();
                StartUpWebService();
                PopulatePawnLoanStatus();
                PopulateManagerOverrideReasons();
                PopulateTempStatus();

                // TL 02-09-2010 Call Wipe Drive parsing method
                //Get category list from BR Engine
                var wipeDrvCatVo = PawnBusinessRuleVO["PWN_BR-084"];
                var wipeDriveCategories = String.Empty;

                if (wipeDrvCatVo != null)
                {
                    if (!wipeDrvCatVo.getComponentValue("WIPE_DRIVE_CATEGORIES", ref wipeDriveCategories))
                    {
                        BasicExceptionHandler.Instance.AddException("Could not find WIPE_DRIVE_CATEGORIES for the current site", new ApplicationException());
                        FileLogger.Instance.logMessage(LogLevel.WARN, this, "Wipe Drive Categories are invalid");
                    }
                }

                // initialize the list of categories
                this.WipeDriveCategories = new List<int>();

                if (!String.IsNullOrEmpty(wipeDriveCategories))
                {
                    ParseWipeDriveCategories(wipeDriveCategories);
                }
                else
                {
                    BasicExceptionHandler.Instance.AddException("Wipe Drive Categories are empty for current site", new ApplicationException());
                    FileLogger.Instance.logMessage(LogLevel.WARN, this, "Wipe Drive Categories are empty");
                }

                // Get scrap related information
                var scrapVO =
                    PawnBusinessRuleVO["PWN_BR-000"];
                string scrapItems = String.Empty;
                string scrapAnswerId = String.Empty;

                if (scrapVO != null)
                {
                    if (!scrapVO.getComponentValue("SCRAP_TYPES", ref scrapItems))
                    {
                        BasicExceptionHandler.Instance.AddException("Could not find SCRAP_TYPES for the current site", new ApplicationException());
                        FileLogger.Instance.logMessage(LogLevel.WARN, this, "Scrap types are invalid");
                    }

                    // answer id associated with Poor-to-scrap handling
                    if (!scrapVO.getComponentValue("POOR_TO_SCRAP_ANSWERID", ref scrapAnswerId))
                    {
                        BasicExceptionHandler.Instance.AddException("Could not find POOR_TO_SCRAP_ANSWERID for the current site", new ApplicationException());
                        FileLogger.Instance.logMessage(LogLevel.WARN, this, "Scrap Answer Id is invalid");
                    }
                }

                this.ScrapTypes = new List<string>();
                if (!String.IsNullOrEmpty(scrapItems))
                {
                    ParseScrapItems(scrapItems);
                }
                else
                {
                    BasicExceptionHandler.Instance.AddException("Scrap items are empty for current site", new ApplicationException());
                    FileLogger.Instance.logMessage(LogLevel.WARN, this, "Scrap items are empty");
                }

                this.ScrapAnswerId = String.Empty;
                if (!String.IsNullOrEmpty(scrapAnswerId))
                {
                    this.ScrapAnswerId = scrapAnswerId;
                }
                else
                {
                    BasicExceptionHandler.Instance.AddException("Scrap Answer Id is empty for current site", new ApplicationException());
                    FileLogger.Instance.logMessage(LogLevel.WARN, this, "Scrap Answer Id is empty");
                }
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Error retrieving start up data from the database", new ApplicationException(ex.Message));
                throw new ApplicationException(ex.Message, ex);
            }
            procMsgFormPwd.Hide();
        }

        public void InitForceCloseTimer()
        {
            //init force close timer
            this.forceCloseTimer = new Timer();
            this.forceCloseTimer.Tag = this.DesktopForm;
            this.forceCloseTimer.Tick += forceCloseTimer_Tick;

            //Perform the check every minute 
            //(1000 ms / second) * (60 seconds / minute) = 60000 ms / minute
            this.forceCloseTimer.Interval = 60000;

            //Start the timer
            this.forceCloseTimer.Start();
            ShowForceCloseMessagesWhenNeeded(this.DesktopForm);
        }

        public override void PerformAuthorization()
        {
            this.PerformAuthorization(false);
        }

        //Call to login the user and get their security profile
        public override void PerformAuthorization(bool chgUsrPasswd)
        {
            if (FileLogger.Instance.IsLogInfo)
            {
                FileLogger.Instance.logMessage(LogLevel.INFO, this, "Performing user authorization...");
            }
            var pwdChanged = false;
            var tagPrinted = false;
            var usrName = string.Empty;
            var newPass = string.Empty;
            var password = string.Empty;
            if (!this.skipLDAP)
            {
                LoggedInUserSecurityProfile = new UserVO();
                this.userState = UserDesktopState.NOTLOGGEDIN;
                LoggedInUserSafeAccess = false;
                //Ensure the LDAP is connected
                if (PawnLDAPAccessor.Instance.State ==
                    PawnLDAPAccessor.LDAPState.DISCONNECTED)
                {
                    string loginDN;
                    string pwdPolicyCN;
                    string searchDN;
                    string userIdKey;
                    string userPwd;

                    var conf = SecurityAccessor.Instance.EncryptConfig;
                    var ldapService =
                    conf.GetLDAPService(
                        out loginDN,
                        out searchDN,
                        out userIdKey,
                        out userPwd,
                        out pwdPolicyCN);//,
                    //out userName);
                    if (FileLogger.Instance.IsLogDebug)
                    {
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "- Connecting to LDAP server:{0}{1}",
                            Environment.NewLine, ldapService);
                    }
                    PawnLDAPAccessor.Instance.InitializeConnection(
                        conf.DecryptValue(ldapService.Server),
                        conf.DecryptValue(ldapService.Port),
                        loginDN,
                        userPwd,
                        pwdPolicyCN,
                        searchDN,
                        userIdKey);
                }
                var attemptCount = 1;
                do
                {
                    bool lockedOut;
                    bool needPasswordChange;
                    bool wantsPasswordChange;
                    var username = string.Empty;
                    var fullAuth = this.PerformLDAPAuthentication(
                        ref attemptCount,
                        ref username,
                        ref password,
                        out lockedOut,
                        out needPasswordChange,
                        out wantsPasswordChange);
                    if (FileLogger.Instance.IsLogInfo)
                    {
                        FileLogger.Instance.logMessage(
                            LogLevel.INFO, this,
                            "Authorization attempt: Count = {0}, IsLockedOut = {1}, NeedsPwdChange = {2}, WantsPwdChange = {3}",
                            attemptCount,
                            lockedOut,
                            needPasswordChange,
                            wantsPasswordChange);
                    }
                    int outVal;
                    string errCode, errTxt;
                    if ((!LoginCancel && fullAuth) || (fullAuth && chgUsrPasswd))
                    {
                        //Check if the user wants to change the password
                        if (wantsPasswordChange || chgUsrPasswd)
                        {
                            var chngPwdForm =
                            new UserChangePassword(
                                PawnLDAPAccessor.Instance.PasswordPolicy, password);
                            DialogResult pwdResult = chngPwdForm.ShowDialog();
                            if (pwdResult == DialogResult.OK)
                            {
                                pwdChanged = true;
                                usrName = username;
                                newPass = chngPwdForm.EnteredNewPassword;
                                password = newPass;
                            }
                        }
                        var retVal = ShopProcedures.ExecuteUpdateSelectUserInfoActivated(
                            username.ToLowerInvariant(), -1, out outVal, out errCode, out errTxt);
                        if (retVal == false || outVal == 0 ||
                            errCode != "0")
                        {
                            MessageBox.Show(
                                "The system has determined that you are not an active user.  " +
                                "Please contact Shop System Support. " +
                                "The application will now exit.",
                                "Application Security",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
                            try
                            {
                                Application.Exit();
                            }
                            catch
                            {
                                throw new ApplicationException(
                                    "Application has exited due to security violation");
                            }
                            finally
                            {
                                throw new ApplicationException(
                                    "Application has exited due to security violation");
                            }
                        }
                    }
                    if (!LoginCancel &&
                        fullAuth == false)
                    {
                        if (lockedOut)
                        {
                            ShopProcedures.ExecuteUpdateSelectUserInfoActivated(
                                username.ToLowerInvariant(), 0, out outVal, out errCode, out errTxt);
                            MessageBox.Show(
                                "The maximum number of attempted failed logins has been exceeded.  " +
                                "The user account is now locked. " +
                                "Please contact Shop System Support. " +
                                "The application will now exit.",
                                "Application Security",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop);
                            try
                            {
                                Application.Exit();
                            }
                            catch
                            {
                                throw new ApplicationException(
                                    "Application has exited due to security violation");
                            }
                            finally
                            {
                                throw new ApplicationException(
                                    "Application has exited due to security violation");
                            }
                        }

                        if (needPasswordChange)
                        {
                            //Invoke password change form
                            //Do not increment attempt count
                            var uPwdForm =
                            new UserChangePassword(
                                PawnLDAPAccessor.Instance.PasswordPolicy, password);
                            var pwdResult = uPwdForm.ShowDialog();

                            if (pwdResult == DialogResult.OK)
                            {
                                this.userState = UserDesktopState.LOGGEDIN;
                                pwdChanged = true;
                                usrName = username;
                                newPass = uPwdForm.EnteredNewPassword;
                                password = newPass;
                                //Print barcode if they changed their password
                                //Afterwards, generate employee list for barcode printing and
                                //usb key writing
                                PopulateShopEmployees();

                                //Find the correct employee in the list
                                var empInfo = AssignedEmployees.Find(
                                    x => x.UserName.Equals(LoggedInUserSecurityProfile.UserName));

                                //If the employee was found
                                if (empInfo != null)
                                {
                                    //Generate the encrypted string that goes on the barcode
                                    string encStr = empInfo.UserName + SecurityAccessor.SEP +
                                                    empInfo.UserCurrentPassword;
                                    var sEmployeeFirstName = empInfo.UserFirstName;
                                    var sEncryptData =
                                    SecurityAccessor.Instance.EncryptConfig.EncryptValue(
                                        encStr);
                                    //Acquire intermec printer interface
                                    if (BarcodePrinter.IsValid)
                                    {
                                        var intermecBarcodeTagPrint = new IntermecBarcodeTagPrint("",
                                                Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber),
                                                IntermecBarcodeTagPrint.PrinterModel.Intermec_PM4i,
                                                BarcodePrinter.IPAddress,
                                                (uint)BarcodePrinter.Port, GlobalDataAccessor.Instance.DesktopSession);

                                        //Print new bar code
                                        intermecBarcodeTagPrint.PrintUserBarCode(sEncryptData,
                                                                                 sEmployeeFirstName);

                                        //Set that the tag was printed
                                        tagPrinted = true;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Cannot print barcode tag for user.  No barcode printer configured",
                                            "Application Security", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(
                                    "You must change your password before you will be " +
                                    "allowed to log in to the application. " +
                                    "The application will now exit.",
                                    "Application Security",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                                try
                                {
                                    Application.Exit();
                                }
                                catch
                                {
                                    throw new ApplicationException(
                                        "Application has exited due to user not changing their password");
                                }
                                finally
                                {
                                    throw new ApplicationException(
                                        "Application has exited due to user not changing their password");
                                }
                            }
                        }
                    }
                    /*  if (this.userState ==
                    UserDesktopState.LOGGEDIN)
                    {
                    continue;
                    }*/
                    if (!LoginCancel && fullAuth == false)
                    {
                        this.userState = UserDesktopState.NOTLOGGEDIN;
                        DialogResult dR =
                        MessageBox.Show(
                            string.Format("You have entered invalid credentials. " + "This is your {0} attempt. " + "Would you like to retry?", (attemptCount.FormatNumberWithSuffix())),
                            "Application Security",
                            MessageBoxButtons.RetryCancel,
                            MessageBoxIcon.Stop);
                        attemptCount++;
                        if (dR == DialogResult.Cancel)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                while (this.userState != UserDesktopState.LOGGEDIN);
            }
            else //For cases when LDAP is not available
            {
                this.userState = UserDesktopState.LOGGEDIN;
                this.UserName = Properties.Resources.SkipLDAPUserName;
                if (this.UserName.Length > USERNAME_MAXLEN)
                {
                    this.UserName = this.UserName.Substring(0, USERNAME_MAXLEN);
                }
                this.FullUserName = Properties.Resources.SkipLDAPUserName;
                this.DisplayName = Properties.Resources.SkipLDAPDisplayName;
            }
            if (!LoginCancel && this.userState == UserDesktopState.LOGGEDIN)
            {
                if (FileLogger.Instance.IsLogInfo)
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, this, "User {0} successfully authorized", FullUserName);
                }
                UpdateDesktopUserName(this.DesktopForm);
                //Get role information
                //The logged in user's security profile will be stored in LoggedInUserSecurityProfile object after the call
                string errorCode;
                string errorMesg;
                LoggedInUserSafeAccess = false;
                if (!SecurityProfileProcedures.GetUserSecurityProfile(FullUserName, "", CurrentSiteId.StoreNumber, "N",
                                                                      this, out errorCode, out errorMesg))
                {
                    BasicExceptionHandler.Instance.AddException(
                        "Security Profile could not be loaded for the logged in user. Cannot Authorize",
                        new ApplicationException());
                    MessageBox.Show(
                        "User's security profile could not be loaded. Exiting the application");
                    Application.Exit();
                }
                else
                {
                    if (FileLogger.Instance.IsLogInfo)
                    {
                        FileLogger.Instance.logMessage(LogLevel.INFO, this, "User {0} security profile retrieved", FullUserName);
                    }
                    UpdateDesktopUserName(this.DesktopForm);
                    //Get cash drawer assignments
                    //Check if the logged in user is a manager and has shop cash access
                    /*if (SecurityProfileProcedures.CanUserViewResource(
                        "SHOPADMINMENUCONTROL", this.LoggedInUserSecurityProfile, this))
                        CurrentAppMode = AppMode.MANAGER;
                    else
                        CurrentAppMode = AppMode.CSR;*/
                    if (SecurityProfileProcedures.CanUserModifyResource(
                        "SAFEMANAGEMENT", this.LoggedInUserSecurityProfile, this))
                    {
                        LoggedInUserSafeAccess = true;
                        CurrentAppMode = AppMode.MANAGER;
                    }
                    else
                    {
                        LoggedInUserSafeAccess = false;
                        CurrentAppMode = AppMode.CSR;
                    }

                    GetCashDrawerAssignmentsForStore();
                    if (string.IsNullOrEmpty(StoreSafeID) || string.IsNullOrEmpty(StoreSafeName))
                    {
                        MessageBox.Show("No safe assigned for the store. Exiting the application");
                        Application.Exit();
                    }
                }

                if (userState == UserDesktopState.LOGGEDIN)
                {
                    //Set the password in the users security profile
                    LoggedInUserSecurityProfile.UserCurrentPassword = password;

                    if (string.IsNullOrEmpty(ActiveUserData.CurrentUserName))
                    {
                        ActiveUserData = new CurrentLoggedInUserData
                        {
                            CurrentUserFullName = this.FullUserName,
                            CurrentUserName = this.UserName,
                            CurrentCashDrawerID = CashDrawerId

                        };
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(ActiveUserData.CurrentUserName) && ActiveUserData.CurrentUserName != this.UserName)
                        {
                            //If any shop offsets were done they need to be reversed
                            ShopDateTime.Instance.setOffsets(0, 0, 0, 0, 0, 0, 0);
                            ActiveUserData = new CurrentLoggedInUserData
                            {
                                CurrentUserFullName = this.FullUserName,
                                CurrentUserName = this.UserName,
                                CurrentCashDrawerID = CashDrawerId
                            };
                            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.CPNHSEnabled)
                            {
                                CPNHSController.Instance.StartSession(this.UserName, ShopDateTime.Instance.ShopDateCurTime);
                            }
                        }
                    }

                    if (pwdChanged)
                    {
                        if (!tagPrinted)
                        {
                            //Generate the encrypted string that goes on the barcode
                            string encStr = string.Format("{0}{1}{2}", LoggedInUserSecurityProfile.UserName, SecurityAccessor.SEP, newPass);
                            var sEmployeeFirstName = LoggedInUserSecurityProfile.UserFirstName;
                            var sEncryptData =
                            SecurityAccessor.Instance.EncryptConfig.EncryptValue(encStr);
                            if (BarcodePrinter.IsValid)
                            {
                                //Acquire intermec printer interface
                                var intermecBarcodeTagPrint =
                                    new IntermecBarcodeTagPrint("",
                                        Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber),
                                        IntermecBarcodeTagPrint.PrinterModel.Intermec_PM4i,
                                        BarcodePrinter.IPAddress,
                                        (uint)BarcodePrinter.Port, GlobalDataAccessor.Instance.DesktopSession);

                                //Print new bar code
                                intermecBarcodeTagPrint.PrintUserBarCode(sEncryptData, sEmployeeFirstName);

                                if (FileLogger.Instance.IsLogDebug)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Printed bar code for user {0}",
                                                                   usrName);
                                }
                                tagPrinted = true;
                            }
                            else
                            {
                                if (FileLogger.Instance.IsLogWarn)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.WARN, this,
                                                                   "Could not print bar code tag for user {0}.  Barcode printer is not configured",
                                                                   usrName);
                                }
                            }
                        }

                        if (SecurityAccessor.Instance.WriteUsbAuthentication(GlobalDataAccessor.Instance.DesktopSession, usrName, newPass))
                        {
                            if (FileLogger.Instance.IsLogDebug)
                            {
                                FileLogger.Instance.logMessage(LogLevel.DEBUG,
                                                               this,
                                                               "Wrote usb stick entry for user {0}",
                                                               usrName);
                            }
                        }
                    }
                }
            }

        }

        public override void PerformCashDrawerChecks(out bool checkPassed)
        {
            checkPassed = true;
            //Do the check only if the button clicked is a teller operation
            //which will indicate whether it needs cash drawer or safe
            if (!IsButtonTellerOperation(instance.HistorySession.TriggerName))
                return;
            ////If it is a safe user who has logged in and the user clicked on Balance button
            ////Since an interim screen will be shown that will let them either balance their
            ////cash drawer or the safe we should not open cash drawer at this stage
            ////BZ 112
            //if (instance.HistorySession.TriggerName.Equals("Balance", StringComparison.OrdinalIgnoreCase) && LoggedInUserSafeAccess)
            //    return;

            PriorDateCDBalance = false;

            bool retValue;
            string errorCode;
            string errorMesg;
            procMsgFormPwd = new ProcessingMessage("* LOADING SAFE AND CASHDRAWER *");
            procMsgFormPwd.Show();
            //closedUnverifiedCashDrawers = false;
            //Perform safe and cash drawer related functions

            if (Instance.IsSkipLDAP)
            {
                safeOpen = true;
            }
            //Check if the manager or user has cash drawer assigned
            bool cashdrawerAssigned = CheckCashDrawerAssigned();
            if (!cashdrawerAssigned)
            {
                //If cash drawer is not assigned but the manager has logged in
                //show the cash drawer assignment form
                if (CurrentAppMode == AppMode.MANAGER)
                {
                    try
                    {
                        var shopCashMgrForm = new ShopCashMgr();
                        shopCashMgrForm.ShowDialog();
                        GetCashDrawerAssignmentsForStore();
                        cashdrawerAssigned = CheckCashDrawerAssigned();
                    }
                    catch (Exception ex)
                    {
                        BasicExceptionHandler.Instance.AddException("Error trying to assign cash drawer to manager", new ApplicationException(ex.Message));
                        Application.Exit();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(@"No cash drawer assigned. Cannot proceeed");
                    Application.Exit();
                    return;
                }
            }
            if (cashdrawerAssigned)
            {
                //If the user's cash drawer is closed unverified
                if (bCashDrawerClosedUnverified)
                {
                    var infoMsg = new InfoDialog
                    {
                        MessageToShow =
                        "The previous day's cash was not verified. The " +
                        CashDrawerName +
                        " must be balanced before this transaction can be completed."
                    };
                    procMsgFormPwd.Hide();
                    infoMsg.ShowDialog();
                    PriorDateCDBalance = true;
                    //Show the balance cash drawer form
                    var cashbalanceForm = new BalanceCash
                    {
                        SafeBalance = false
                    };
                    ClosedUnverifiedCashDrawer = true;
                    DialogResult dgr = cashbalanceForm.ShowDialog();
                    if (dgr != DialogResult.Cancel)
                    {
                        ClosedUnverifiedCashDrawer = false;
                        closedUnverifiedCashDrawers = false;
                        bCashDrawerClosedUnverified = false;
                        UnverifiedCashDrawers.Remove(CashDrawerName);
                        PriorDateCDBalance = false;
                    }
                    else
                    {
                        checkPassed = false;
                        return;
                    }
                }
            }
            else
            {
                Application.Exit();
                return;
            }

            string userOpeningSafe = FullUserName;
            if (safeClosedUnverified)
            {
                var infoMsg = new InfoDialog
                {
                    MessageToShow =
                    string.Format("The previous day's cash was not verified. The {0} must be balanced before this transaction can be completed.", StoreSafeName)
                };
                ClosedUnverifiedSafe = true;
                procMsgFormPwd.Hide();
                infoMsg.ShowDialog();
                if (!LoggedInUserSafeAccess)
                {
                    MessageBox.Show("The safe must be balanced by a user with safe access before any monetary transaction can be completed in Cashlinx. The application is now exiting.");
                    checkPassed = false;
                    return;
                }
                //Check if any cash drawer is closed unverified
                if (UnverifiedCashDrawers.Count > 0)
                {
                    StringBuilder sCDNames = new StringBuilder();
                    foreach (string s in UnverifiedCashDrawers)
                    {
                        sCDNames.Append(s);
                        sCDNames.Append(",");
                    }

                    procMsgFormPwd.Hide();
                    MessageBox.Show(string.Format("One or more cash drawers {0} have been closed unverified. Please balance all cash drawers to continue.", sCDNames));
                    checkPassed = false;
                    return;
                    //Application.Exit();
                }
                var cashbalanceForm = new BalanceCash
                {
                    SafeBalance = true
                };
                procMsgFormPwd.Hide();
                DialogResult dgr = cashbalanceForm.ShowDialog();
                if (dgr != DialogResult.Cancel)
                {
                    safeClosedUnverified = false;
                    ClosedUnverifiedSafe = false;
                }
                else
                {
                    //Application.Exit();
                    checkPassed = false;
                    return;
                }
                string errorMsg;

                var cdID = GlobalDataAccessor.Instance.DesktopSession.StoreSafeID;
                const int cashDrawerStatus = (int)CASHDRAWERSTATUS.OPEN;
                var workstationID = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId;
                retValue = ShopCashProcedures.UpdateSafeStatus(cdID, Instance.CurrentSiteId.StoreNumber,
                                                               userOpeningSafe, ShopDateTime.Instance.ShopDate.FormatDate(), cashDrawerStatus.ToString(),
                                                               workstationID, this,
                                                               out errorCode, out errorMsg);
            }
            else if (!safeOpen)
            {
                bool safeBalancedToday;
                retValue = ShopCashProcedures.CheckCashDrawerBalanced(
                    StoreSafeID, ShopDateTime.Instance.ShopDate.FormatDate(), this,
                    out safeBalancedToday, out errorCode, out errorMesg);
                //If safe is not closed balanced today and the user is a safe user
                //open the safe
                userOpeningSafe = FullUserName;
                if (safeBalancedToday && !LoggedInUserSafeAccess)
                {
                    //If safe is balanced today and the user is not a safe user
                    //show manager overrides
                    procMsgFormPwd.Hide();
                    var overrideTypes = new List<ManagerOverrideType>();
                    var transactionTypes = new List<ManagerOverrideTransactionType>();
                    var messageToShow = new StringBuilder();
                    messageToShow.Append("ReOpen Safe ");
                    overrideTypes.Add(ManagerOverrideType.SOPEN);
                    transactionTypes.Add(ManagerOverrideTransactionType.SAFE);
                    var overrideFrm = new ManageOverrides(ManageOverrides.OVERRIDE_TRIGGER)
                    {
                        MessageToShow = messageToShow.ToString(),
                        ManagerOverrideTypes = overrideTypes,
                        ManagerOverrideTransactionTypes = transactionTypes

                    };

                    overrideFrm.ShowDialog();
                    if (!overrideFrm.OverrideAllowed)
                    {
                        procMsgFormPwd.Hide();
                        MessageBox.Show(@"Manager override failed. Cannot open safe. Exiting the application");
                        checkPassed = false;
                        return;
                    }
                    userOpeningSafe = overrideFrm.ManagerUserName;
                }

                string errorMsg;
                const int cashDrawerStatus = (int)CASHDRAWERSTATUS.OPEN;
                string workstationID = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId;
                retValue = ShopCashProcedures.UpdateSafeStatus(StoreSafeID, Instance.CurrentSiteId.StoreNumber,
                                                               userOpeningSafe, ShopDateTime.Instance.ShopDate.FormatDate(), cashDrawerStatus.ToString(),
                                                               workstationID, this,
                                                               out errorCode, out errorMsg);
            }

            if (!IsSafeOperation())
            {
                //If it is not safe operation check if the cash drawer is closed balanced
                //and if it is and was closed today then manager override is needed
                if (!bCashDrawerOpen && !bCashDrawerClosedUnverified)
                {
                    bool cashdrawerbalanced;
                    retValue = ShopCashProcedures.CheckCashDrawerBalanced(CashDrawerId,
                                                                          ShopDateTime.Instance.ShopDate.FormatDate(), this, out cashdrawerbalanced,
                                                                          out errorCode, out errorMesg);
                    var userOpeningCashDrawer = FullUserName;
                    if (cashdrawerbalanced)
                    {
                        procMsgFormPwd.Hide();
                        MessageBox.Show(Commons.GetMessageString("WM02RebalanceDrawerRequired"));
                        var overrideTypes = new List<ManagerOverrideType>();
                        var transactionTypes = new List<ManagerOverrideTransactionType>();
                        var messageToShow = new StringBuilder();
                        messageToShow.Append("ReOpen cashdrawer ");
                        overrideTypes.Add(ManagerOverrideType.REOPEN);
                        transactionTypes.Add(ManagerOverrideTransactionType.CD);
                        var overrideFrm = new ManageOverrides(ManageOverrides.OVERRIDE_TRIGGER)
                        {
                            MessageToShow = messageToShow.ToString(),
                            ManagerOverrideTypes = overrideTypes,
                            ManagerOverrideTransactionTypes = transactionTypes

                        };

                        overrideFrm.ShowDialog();
                        if (!overrideFrm.OverrideAllowed)
                        {
                            MessageBox.Show(@"Manager override failed. Cannot open cash drawer. Exiting the application");
                            Application.Exit();
                            return;
                        }
                        userOpeningCashDrawer = overrideFrm.ManagerUserName;
                    }
                    string errorMsg;
                    const int cashDrawerStatus = (int)CASHDRAWERSTATUS.OPEN;
                    string workstationID = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId;
                    retValue = ShopCashProcedures.UpdateSafeStatus(CashDrawerId, Instance.CurrentSiteId.StoreNumber,
                                                                   userOpeningCashDrawer, ShopDateTime.Instance.ShopDate.FormatDate(), cashDrawerStatus.ToString(),
                                                                   workstationID, this,
                                                                   out errorCode, out errorMsg);
                    procMsgFormPwd.Hide();
                    if (!retValue)
                    {
                        BasicExceptionHandler.Instance.AddException("Cashdrawer could not be opened", new ApplicationException());
                    }
                }

                procMsgFormPwd.Hide();
                //check if the cashdrawer has other teller operation in progress
                string wrkId;
                string cdEvent;

                retValue = ShopCashProcedures.GetTellerEvent(CashDrawerId, GlobalDataAccessor.Instance.DesktopSession, out wrkId, out cdEvent, out errorCode, out errorMesg);
                if (retValue)
                {
                    if (instance.HistorySession.TriggerName.ToUpper().Contains("BALANCE"))
                    {
                        if (errorCode != "100")
                        {

                            MessageBox.Show(cdEvent.ToUpper() + " transaction is in process on " + wrkId + ". Please complete that operation first");
                            checkPassed = false;
                            return;
                        }
                        string workstationID = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId;
                        //Insert an event record
                        retValue = ShopCashProcedures.InsertTellerEvent(CashDrawerId, workstationID, instance.HistorySession.TriggerName, out errorCode, out errorMesg);
                        if (!retValue)
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Teller Event could not be inserted");

                    }
                    else
                    {
                        if (errorCode != "100")
                        {
                            if (cdEvent.ToUpper().Contains("BALANCE"))
                            {

                                MessageBox.Show("There is a cashdrawer balance event in progress. Please complete that operation first");
                                checkPassed = false;
                                return;
                            }
                        }
                        else
                        {
                            string workstationID = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId;
                            //Insert an event record
                            retValue = ShopCashProcedures.InsertTellerEvent(CashDrawerId, workstationID, instance.HistorySession.TriggerName, out errorCode, out errorMesg);
                            if (!retValue)
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Teller Event could not be inserted");
                        }


                    }



                }
                try
                {
                    string errorDesc = string.Empty;
                    bool retVal = ShopCashProcedures.AssignWorkstationtoCashDrawer(CashDrawerId, workStationId,
                                                                                   CurrentSiteId.StoreNumber, FullUserName, ShopDateTime.Instance.ShopDate.FormatDate(), this, out errorCode, out errorDesc);
                    if (!retVal)
                    {
                        throw new Exception("Calling AssignWorkstationtocashdrawer failed");
                    }
                }
                catch (Exception ex)
                {
                    BasicExceptionHandler.Instance.AddException("Error trying to assign workstation to cash drawer", new ApplicationException(ex.Message));
                    Application.Exit();
                    return;
                }

                //Check the cash drawer balance and show error message if it exceeds limit
                //set in PWN_BR-96
                procMsgFormPwd.Hide();
                try
                {
                    decimal BalanceAmount = 0.0m;
                    DataTable cashdrawerAmounts = null;

                    //Call to SP to get the list of transactions against the cash drawer
                    bool retval = ShopCashProcedures.GetCashDrawerAmount(GlobalDataAccessor.Instance.DesktopSession.CashDrawerId, ShopDateTime.Instance.ShopDate.FormatDate(), "N", this,
                                                                         out cashdrawerAmounts, out errorCode, out errorMesg);
                    if (retval && cashdrawerAmounts != null && cashdrawerAmounts.Rows.Count > 0)
                    {
                        //Parse the transactions to add and subtract the amounts based on the
                        //Transactions
                        BalanceAmount = ShopCashProcedures.GetCashDrawerAmount(cashdrawerAmounts);
                    }

                    if (retval)
                    {
                        //Get the beginning cash amount for the cash drawer
                        decimal beginningCashAmount;
                        string cdId = Instance.CashDrawerId;
                        retValue = ShopCashProcedures.GetCashDrawerBeginningAmount(cdId, ShopDateTime.Instance.ShopDate.FormatDate() + " " + ShopDateTime.Instance.ShopTime.ToString(), "N", this,
                                                                                   out beginningCashAmount, out errorCode, out errorMesg);
                        if (retValue)
                        {
                            BalanceAmount += beginningCashAmount;
                            CashDrawerBeginningBalance = beginningCashAmount;
                        }

                        decimal maxCDLimit = 0.0m;
                        new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMaxCashDrawerLimit(GlobalDataAccessor.Instance.CurrentSiteId, out maxCDLimit);
                        if (BalanceAmount > 0 && maxCDLimit > 0 && BalanceAmount > maxCDLimit)
                            UpdateDesktopCashOver(this.DesktopForm, true);
                        else
                            UpdateDesktopCashOver(this.DesktopForm, false);
                    }
                }
                catch (Exception ex)
                {
                    FileLogger.Instance.logMessage(LogLevel.WARN, this, "Error when trying to determine if cash over in cash drawer " + ex.Message);
                }
            }//End if not safe operation
            if (IsSafeOperation())
            {
                //check if the cashdrawer has other teller operation in progress
                string wrkId;
                string cdEvent;

                retValue = ShopCashProcedures.GetTellerEvent(StoreSafeID, GlobalDataAccessor.Instance.DesktopSession, out wrkId, out cdEvent, out errorCode, out errorMesg);
                if (retValue)
                {
                    if (instance.HistorySession.TriggerName.ToUpper().Contains("BALANCE"))
                    {
                        if (errorCode != "100")
                        {

                            MessageBox.Show("There is a safe event in progress. Please complete that operation first");
                            checkPassed = false;
                            return;
                        }

                    }
                    else
                    {
                        if (errorCode != "100")
                        {
                            if (cdEvent.ToUpper().Contains("BALANCE"))
                            {

                                MessageBox.Show("There is a safe balance event in progress. Please complete that operation first");
                                checkPassed = false;
                                return;
                            }
                        }
                        else
                        {
                            string workstationID = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId;
                            //Insert an event record
                            retValue = ShopCashProcedures.InsertTellerEvent(StoreSafeID, workstationID, instance.HistorySession.TriggerName, out errorCode, out errorMesg);
                            if (!retValue)
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Teller Event could not be inserted");

                        }


                    }



                }
            }
            procMsgFormPwd.Hide();
            procMsgFormPwd.Dispose();
        }


        public override void CheckOpenCashDrawers(out bool openCashDrawers)
        {
            string errorCode;
            string errorMesg;
            DataTable ShopCashDetails;
            var openCashDrawerNames = new StringBuilder();
            var openCDNames = new List<PairType<string, string>>();
            var unverifiedCDNames = new List<PairType<string, string>>();

            bool retValue = ShopCashProcedures.GetShopCashPosition(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, this,
                                                                   out ShopCashDetails,
                                                                   out errorCode, out errorMesg);
            //Check if there are any open cash drawers
            foreach (DataRow dr in ShopCashDetails.Rows)
            {
                string cdId = Utilities.GetStringValue(dr["cashdrawerid"]);
                string cdName = Utilities.GetStringValue(dr["cashdrawername"], "");
                string cdStatus = Utilities.GetStringValue(dr["openflag"], "");
                if (cdName != GlobalDataAccessor.Instance.DesktopSession.StoreSafeName &&
                    cdStatus == ((int)CASHDRAWERSTATUS.OPEN).ToString())
                {
                    openCashDrawerNames.Append(cdName);
                    openCDNames.Add(new PairType<string, string>(cdId, cdName));
                }
                if (cdName != GlobalDataAccessor.Instance.DesktopSession.StoreSafeName &&
                    cdStatus == ((int)CASHDRAWERSTATUS.CLOSED_UNVERIFIED).ToString())
                {
                    openCashDrawerNames.Append(cdName);
                    unverifiedCDNames.Add(new PairType<string, string>(cdId, cdName));
                }
            }
            if (openCDNames.Any() || unverifiedCDNames.Any())
            {
                openCashDrawers = true;
                //If there are open cash drawers show the message asking whether its a trial balance or if they wish to balance another user's open cash drawer
                var balanceForm = new CDBalanceOptions();
                balanceForm.ShowDialog();
                //If its not a trial balance show message that cash drawers must be closed
                if (!balanceForm.TrialBalance && !balanceForm.OtherCDBalance)
                {
                    MessageBox.Show(string.Format("{0} {1}", Commons.GetMessageString("EM01CloseCashDrawers"), openCashDrawerNames.ToString()));
                    Instance.BalanceOtherCashDrawerID = string.Empty;
                    TrialBalance = false;
                    return;
                }
                if (balanceForm.TrialBalance)
                    TrialBalance = true;
                else if (balanceForm.OtherCDBalance)
                {
                    TrialBalance = false;
                    BalanceOtherCashDrawerID = string.Empty;
                    BalanceOtherCashDrawerName = string.Empty;
                    var cdStatusForm = new CashDrawerStatus();
                    cdStatusForm.OpenCDs = openCDNames;
                    cdStatusForm.ClosedUnverifiedCDs = unverifiedCDNames;
                    cdStatusForm.ShowDialog();
                }
            }
            else
            {
                openCashDrawers = false;
                if (instance.HistorySession.TriggerName.ToUpper().Equals("BALANCESAFE"))
                {
                    string workstationID = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId;
                    //Insert an event record
                    retValue = ShopCashProcedures.InsertTellerEvent(StoreSafeID, workstationID, instance.HistorySession.TriggerName, out errorCode, out errorMesg);
                    if (!retValue)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Teller Event could not be inserted");
                }

            }

        }
        public bool IsSafeOperation()
        {
            //Check if the trigger is a safe operation
            //If yes then cash drawer need not be open
            //otherwise check for cash drawer status
            bool isSafeOperation = false;
            string fileName = "SafeOperations.xml";
            var doc = new XmlDocument();
            doc.Load(fileName);
            var safeOpsList = doc.GetElementsByTagName("Operation");
            foreach (XmlNode node in safeOpsList)
            {
                var safeOpElement = (XmlElement)node;
                if (safeOpElement != null)
                {
                    var safeOp = safeOpElement.InnerText.ToUpper();
                    if ((instance.HistorySession.Trigger != null && safeOp == instance.HistorySession.Trigger.ToUpper()) || (instance.HistorySession.TriggerName != null && safeOp == instance.HistorySession.TriggerName.ToUpper()))
                    {
                        isSafeOperation = true;
                        break;
                    }
                }
            }
            return isSafeOperation;
        }

        private void UpdateDesktopCashOver(Form deskForm, bool cashOver)
        {
            Control userInfoGroupBox = deskForm.Controls["userInfoGroupBox"];
            if (userInfoGroupBox != null)
            {
                Control cashOverLabel = userInfoGroupBox.Controls["labelCashOver"];
                if (cashOverLabel != null)
                    cashOverLabel.Visible = cashOver;
            }
        }

        private void LoadStoreData(OracleDataAccessor dA,
                                   string storeNumber,
                                   ref SiteId siteId)
        {
            try
            {
                //data table to load store info
                var sErrorCode = string.Empty;
                var sErrorMessage = string.Empty;
                ShopProcedures.ExecuteGetStoreInfo(dA, storeNumber, ref siteId,
                                                   out sErrorCode, out sErrorMessage);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("LoadStoreData failed", new ApplicationException("Load store data failed", ex));
            }
        }

        /// <summary>
        /// Given a user and a cash drawer name, the function will return true if the passed in user
        /// is assigned to the cash drawer passed in and also return information on
        /// the status of the cash drawer whether it is open or closed etc
        /// </summary>
        /// <param name="username"></param>
        /// <param name="cashdrawer"></param>
        /// <param name="cashDrawerflag"></param>
        /// <returns></returns>
        public override bool IsUserAssignedCashDrawer(string username, string cashdrawer, out string cashDrawerflag)
        {
            cashDrawerflag = string.Empty;
            if (CashDrawerAssignments != null)
            {
                foreach (DataRow dr in CashDrawerAssignments.Rows)
                {
                    if (cashdrawer == StoreSafeName)
                    {
                        cashDrawerflag = Utilities.GetStringValue(dr["openflag"], "");
                        return true;
                    }

                    if (Utilities.GetStringValue(dr["name"].ToString()) == cashdrawer &&
                        (Utilities.GetStringValue(dr["username"]).ToUpper() == username.ToUpper()))
                    {
                        cashDrawerflag = Utilities.GetStringValue(dr["openflag"], "");
                        return true;
                    }
                }
            }
            if (CashDrawerAuxAssignments != null)
            {
                foreach (DataRow dr in CashDrawerAuxAssignments.Rows)
                {
                    if (Utilities.GetStringValue(dr["name"].ToString()) == cashdrawer &&
                        (Utilities.GetStringValue(dr["username"]).ToUpper() == username.ToUpper()))
                    {
                        cashDrawerflag = Utilities.GetStringValue(dr["openflag"], "");
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check if the logged in user has a cash drawer assigned
        /// </summary>
        /// <returns></returns>
        private bool CheckCashDrawerAssigned()
        {

            foreach (DataRow dr in CashDrawerAssignments.Rows)
            {
                if ((Utilities.GetStringValue(dr["username"])) == UserName ||
                    (Utilities.GetStringValue(dr["username"])) == FullUserName)
                {
                    CashDrawerId = Utilities.GetStringValue(dr["id"]);
                    CashDrawerName = Utilities.GetStringValue(dr["name"]);
                    if (CashDrawerName == StoreSafeName)
                        continue;
                    workStationId = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId;
                    var cashdrawerFlag = Utilities.GetStringValue(dr["openflag"], "");
                    if (cashdrawerFlag == "1")
                    {
                        bCashDrawerOpen = true;
                        bCashDrawerClosedUnverified = false;
                    }
                    else if (cashdrawerFlag == "2")
                    {
                        bCashDrawerClosedUnverified = true;
                        bCashDrawerOpen = false;
                    }
                    else if (cashdrawerFlag == "0")
                    {
                        bCashDrawerClosedUnverified = false;
                        bCashDrawerOpen = false;
                    }
                    return true;
                }
            }
            if (CashDrawerAuxAssignments != null && CashDrawerAuxAssignments.Rows != null)
            {
                foreach (DataRow dr in CashDrawerAuxAssignments.Rows)
                {
                    if ((Utilities.GetStringValue(dr["username"])) == UserName ||
                        (Utilities.GetStringValue(dr["username"])) == FullUserName)
                    {
                        CashDrawerId = Utilities.GetStringValue(dr["id"]);
                        CashDrawerName = Utilities.GetStringValue(dr["name"]);
                        if (CashDrawerName == StoreSafeName)
                            continue;
                        workStationId = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId;
                        string cashdrawerFlag = Utilities.GetStringValue(dr["openflag"], "");
                        if (cashdrawerFlag == "1")
                        {
                            bCashDrawerOpen = true;
                            bCashDrawerClosedUnverified = false;
                        }
                        else if (cashdrawerFlag == "2")
                        {
                            bCashDrawerClosedUnverified = true;
                            bCashDrawerOpen = false;
                        }
                        else if (cashdrawerFlag == "0")
                        {
                            bCashDrawerClosedUnverified = false;
                            bCashDrawerOpen = false;
                        }
                        return true;
                    }
                }
            }

            return false;
        }

        public override void GetCashDrawerAssignmentsForStore()
        {
            try
            {
                string sErrorCode;
                string sErrorMessage;
                string safeCashdrawerName;
                DataTable dtCashDrawerAssignments;
                DataTable dtCashDrawerAuxAssignments;
                UnverifiedCashDrawers = new List<string>();
                safeClosedUnverified = false;
                closedUnverifiedCashDrawers = false;
                safeOpen = false;
                var retVal = ShopCashProcedures.GetCashDrawerAssignments(
                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, this, out safeCashdrawerName,
                    out dtCashDrawerAssignments, out dtCashDrawerAuxAssignments, out sErrorCode, out sErrorMessage);
                if (retVal == false)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot load Cash drawer assignments");
                    return;
                }
                StoreSafeName = safeCashdrawerName;
                CashDrawerAssignments = dtCashDrawerAssignments;
                CashDrawerAuxAssignments = dtCashDrawerAuxAssignments;
                //Add primary key to the table
                if (CashDrawerAssignments != null)
                {
                    DataColumn[] key = new DataColumn[1];
                    key[0] = CashDrawerAssignments.Columns["NAME"];
                    CashDrawerAssignments.PrimaryKey = key;
                }
                else
                {
                    throw new ApplicationException("Failed to retrieve cash drawer assignments");
                }

                if (StoreSafeName.Length > 0)
                {
                    //check if the safe is open
                    var safeCdRow = CashDrawerAssignments.Select(string.Format("NAME='{0}'", StoreSafeName));
                    //There should be only 1 row returned
                    if (safeCdRow.Length > 0)
                    {
                        StoreSafeID = Utilities.GetStringValue(safeCdRow[0]["ID"]);
                        //Openflag=1 implies open and 0 implies the safe is not open
                        if (Utilities.GetIntegerValue(safeCdRow[0]["OPENFLAG"], 0) == 1)
                            safeOpen = true;
                        else if (Utilities.GetIntegerValue(safeCdRow[0]["OPENFLAG"], 0) == 2)
                            safeClosedUnverified = true;
                    }
                }
                foreach (DataRow dr in CashDrawerAssignments.Rows)
                {
                    if (Utilities.GetStringValue(dr["ID"]) == StoreSafeID)
                        continue;
                    if (Utilities.GetIntegerValue(dr["OPENFLAG"], 0) == 2)
                    {
                        closedUnverifiedCashDrawers = true;
                        UnverifiedCashDrawers.Add(Utilities.GetStringValue(dr["name"]));
                    }
                }
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("GetCashDrawerAssignmentsFailed", new ApplicationException("Cannot execute GetCashDrawerAssignments during StartUp. [" + ex.Message + "]"));
            }
        }

        public void PopulateShopEmployees()
        {
            //Clear any existing shop employees
            this.assignedEmployees.Clear();

            //Load employee profile header
            string sErrorCode, sErrorText;
            DataTable employees;
            if (SecurityProfileProcedures.ExecuteGetEmployeeProfileHeader(
                LoggedInUserSecurityProfile.UserName,
                null,
                CurrentSiteId.StoreNumber,
                out employees,
                out sErrorCode,
                out sErrorText))
            {
                if (sErrorCode == "0")
                {
                    employees.DefaultView.Sort = "employeenumber";
                    DataColumn[] key = new DataColumn[1];
                    key[0] = employees.Columns["userid"];
                    employees.PrimaryKey = key;
                    this.internalPopulateEmployees(employees);
                }
            }
        }

        private void internalPopulateEmployees(DataTable emps)
        {
            string sFilter = string.Format("homestore = '{0}'", CurrentSiteId.StoreNumber);

            DataRow[] dataRows = emps.Select(sFilter);
            foreach (DataRow dataRow in dataRows)
            {
                UserVO newUser = new UserVO();
                newUser.UserLastName = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.LASTNAME]);
                newUser.UserFirstName = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.FIRSTNAME]);
                newUser.EmployeeNumber = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.EMPLOYEENUMBER]);
                newUser.UserID = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.USERID]);
                newUser.UserName = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.NAME]);
                string pwd;
                string displayName;
                if (PawnLDAPAccessor.Instance.GetUserPassword(newUser.UserName, out pwd, out displayName))
                {
                    newUser.UserCurrentPassword = pwd;
                    newUser.UserFirstName = displayName;
                }
                this.assignedEmployees.Add(newUser);
            }
        }

        // Until Admin section created, load the barcode formats during startup
        private void PopulateCategoryXML()
        {
            CategoryXML = new CategoryNode();
            CategoryXML.Setup();
            if (CategoryXML.Error)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Was not able to load the Categories from Category XML!");
#if !__MULTI__
                BasicExceptionHandler.Instance.AddException("PopulateCategoryXML", new ApplicationException("Cannot load Categories from Category XML during StartUp. [" + CategoryXML.ErrorMessage + "]"));
#endif
            }
        }

        private void PopulateManagerOverrideReasons()
        {
            ManagerOverrideReasonCodes = new List<PairType<ManagerOverrideReason, string>>();
            ManagerOverrideReasonCodes.Add(new PairType<ManagerOverrideReason, string>(ManagerOverrideReason.ALL_CUSTOMER_WANTED, "All Customer Wanted"));
            ManagerOverrideReasonCodes.Add(new PairType<ManagerOverrideReason, string>(ManagerOverrideReason.APPROPRIATE_DOCUMENTS_PROVIDED, "Appropriate Documents Provided"));
            ManagerOverrideReasonCodes.Add(new PairType<ManagerOverrideReason, string>(ManagerOverrideReason.GOOD_HISTORY, "Good History"));
            ManagerOverrideReasonCodes.Add(new PairType<ManagerOverrideReason, string>(ManagerOverrideReason.NEW_CUSTOMER, "New Customer"));

            ManagerOverrideTransactionTypes = new List<PairType<ManagerOverrideTransactionType, string>>();
            ManagerOverrideTransactionTypes.Add(new PairType<ManagerOverrideTransactionType, string>(ManagerOverrideTransactionType.NL, "New Pawn Loan"));
            ManagerOverrideTransactionTypes.Add(new PairType<ManagerOverrideTransactionType, string>(ManagerOverrideTransactionType.PFI, "PFI Processed"));
            ManagerOverrideTransactionTypes.Add(new PairType<ManagerOverrideTransactionType, string>(ManagerOverrideTransactionType.PU, "Pawn Loan PickUp"));
            ManagerOverrideTransactionTypes.Add(new PairType<ManagerOverrideTransactionType, string>(ManagerOverrideTransactionType.RN, "Pawn Loan Renewal"));
            ManagerOverrideTransactionTypes.Add(new PairType<ManagerOverrideTransactionType, string>(ManagerOverrideTransactionType.EX, "Pawn Loan Extension"));
            ManagerOverrideTransactionTypes.Add(new PairType<ManagerOverrideTransactionType, string>(ManagerOverrideTransactionType.BUY, "Purchase From Customer"));
            ManagerOverrideTransactionTypes.Add(new PairType<ManagerOverrideTransactionType, string>(ManagerOverrideTransactionType.SALE, "Customer Purchase"));
            ManagerOverrideTransactionTypes.Add(new PairType<ManagerOverrideTransactionType, string>(ManagerOverrideTransactionType.LAY, "Customer Layaway"));
            ManagerOverrideTransactionTypes.Add(new PairType<ManagerOverrideTransactionType, string>(ManagerOverrideTransactionType.SALE, "Sale"));

            ManagerOverrideTypes = new List<PairType<ManagerOverrideType, string>>();
            ManagerOverrideTypes.Add(new PairType<ManagerOverrideType, string>(ManagerOverrideType.PKV, "ProKnow Variance"));
            ManagerOverrideTypes.Add(new PairType<ManagerOverrideType, string>(ManagerOverrideType.PFIE, "PFI Eligible"));
            ManagerOverrideTypes.Add(new PairType<ManagerOverrideType, string>(ManagerOverrideType.WV, "Waive"));
            ManagerOverrideTypes.Add(new PairType<ManagerOverrideType, string>(ManagerOverrideType.PRO, "Prorate"));
            ManagerOverrideTypes.Add(new PairType<ManagerOverrideType, string>(ManagerOverrideType.DOC, "Legal Document Required"));

            ServiceFeeTypes = new List<PairType<FeeTypes, string>>();
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.ADMINISTRATIVE, "Administrative Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.AUTOEXTEND, "Auto Extend Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.CITY, "City Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.FIREARM, "Firearm Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.FIREARM_BACKGROUND, "Firearm Background Check Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.GUNLOCK, "Gun Lock Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.INITIAL, "Initiate Charge Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.LATE, "Late Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.LOAN, "Loan Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.MINIMUM_INTEREST, "Minimum Interest Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.ORIGIN, "Origination Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.MAILER_CHARGE, "PFI Mailer Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.PREPAID_CITY, "Prepaid City Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.PREPARATION, "Preparation Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.PROCESS, "Processing Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.SETUP, "Setup Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.STORAGE, "Storage Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.STORAGE_MAXIMUM, "Storage Fee - Maximum"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.TICKET, "Ticket Fee"));
            ServiceFeeTypes.Add(new PairType<FeeTypes, string>(FeeTypes.LOST_TICKET, "Lost Ticket Fee"));
        }

        private void PopulatePawnLoanStatus()
        {
            LoanStatus = new List<PairType<ProductStatus, string>>();
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.ALL, "All"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.IP, "In Pawn"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.RN, "Renew"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.PD, "Paydown"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.PU, "Pickup"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.LAY, "Layaway"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.OFF, "Charge Off"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.PFI, "PFI"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.PS, "Seize"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.PUR, "Purchase"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.SOLD, "Sold"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.TO, "Transferred Out"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.VO, "Void"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.HIP, "Police Hold/In Pawn"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.HPFC, "Police Hold/PFC"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.HPFI, "Police Hold/PFI"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.RTC, "Release To Claimant"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.PFC, "Pulled For CACC"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.RET, "Return"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.REN, "Renew"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.ACT, "SOLD"));
            LoanStatus.Add(new PairType<ProductStatus, string>(ProductStatus.ACT, "On Layaway"));
        }

        private void PopulateTempStatus()
        {
            TempStatus = new List<PairType<StateStatus, string>>();
            TempStatus.Add(new PairType<StateStatus, string>(StateStatus.BLNK, ""));
            TempStatus.Add(new PairType<StateStatus, string>(StateStatus.PD, "Pay Down"));
            TempStatus.Add(new PairType<StateStatus, string>(StateStatus.E, "Extend"));
            TempStatus.Add(new PairType<StateStatus, string>(StateStatus.L, "Loan Up"));
            TempStatus.Add(new PairType<StateStatus, string>(StateStatus.P, "Pawn Loan Pickup"));
            TempStatus.Add(new PairType<StateStatus, string>(StateStatus.PFI, "PFI Edit"));
            TempStatus.Add(new PairType<StateStatus, string>(StateStatus.PFIE, "PFI Edit"));
            TempStatus.Add(new PairType<StateStatus, string>(StateStatus.PFIL, "PFI Posting"));
            TempStatus.Add(new PairType<StateStatus, string>(StateStatus.PFIS, "PFI Suspend"));
            TempStatus.Add(new PairType<StateStatus, string>(StateStatus.PFIW, "PFI Pick Slip Printed"));
            TempStatus.Add(new PairType<StateStatus, string>(StateStatus.PS, "Police Seize"));
            TempStatus.Add(new PairType<StateStatus, string>(StateStatus.RN, "Renewal"));
            TempStatus.Add(new PairType<StateStatus, string>(StateStatus.RO, "Rollover"));
            TempStatus.Add(new PairType<StateStatus, string>(StateStatus.RTC, "Release to Claimant"));
            TempStatus.Add(new PairType<StateStatus, string>(StateStatus.CH, "Customer Hold"));
            TempStatus.Add(new PairType<StateStatus, string>(StateStatus.IP, "In Pawn"));
        }

        public override void showProcessTender(ProcessTenderProcedures.ProcessTenderMode processTenderMode)
        {
            var procMsgForm = new ProcessingMessage("* PROCESSING PAWN LOAN *");
            Utilities.WaitMillis(500);
            var processTender = new ProcessTender(processTenderMode);
            HistorySession.AddForm(processTender);
            procMsgForm.Close();
            procMsgForm.Dispose();
            processTender.ShowDialog();
            ClearPawnLoan();
        }

        // Pre-Load Pawn Business Rules for Store
        public override void GetPawnBusinessRules()
        {
            try
            {
                this.PawnRulesSys = new PawnRulesSystemImpl();
                var bRules = new List<String>
                {
                    "PWN_BR-000",
                    "PWN_BR-002",
                    "PWN_BR-003",
                    "PWN_BR-004", 
                    "PWN_BR-005",
                    "PWN_BR-007",
                    "PWN_BR-008",
                    "PWN_BR-010",
                    "PWN_BR-013",
                    "PWN_BR-014",
                    "PWN_BR-016",
                    "PWN_BR-017",
                    "PWN_BR-019",
                    "PWN_BR-020",
                    "PWN_BR-021",
                    "PWN_BR-022",
                    "PWN_BR-023",
                    "PWN_BR-024",
                    "PWN_BR-025",
                    "PWN_BR-026",
                    "PWN_BR-027",
                    "PWN_BR-028",
                    "PWN_BR-032",
                    "PWN_BR-033",
                    "PWN_BR-035",
                    "PWN_BR-038",
                    "PWN_BR-042",
                    "PWN_BR-043",
                    "PWN_BR-046",
                    "PWN_BR-047",
                    "PWN_BR-048",
                    "PWN_BR-051",
                    "PWN_BR-053", 
                    "PWN_BR-054",
                    "PWN_BR-057",
                    "PWN_BR-058",
                    "PWN_BR-059",
                    "PWN_BR-061",
                    "PWN_BR-064",
                    "PWN_BR-068",
                    "PWN_BR-069",
                    "PWN_BR-071",
                    "PWN_BR-074",
                    "PWN_BR-075",
                    "PWN_BR-077",
                    "PWN_BR-084", // TL 02-09-2010 Added for Wipe Drive Categories
                    "PWN_BR-089",
                    "PWN_BR-092",
                    "PWN_BR-094",
                    "PWN_BR-096",
                    "PWN_BR-097",
                    "PWN_BR-116",
                    "PWN_BR-117",
                    "PWN_BR-130",
                    "PWN_BR-133",
                    "PWN_BR-148",
                    "PWN_BR-141",
                    "PWN_BR-134",
                    "PWN_BR-142",
                    "PWN_BR-169",
                    "PWN_BR-171",
                    "PWN_BR-172",
                    "PWN_BR-175",
                    "PWN_BR-176",
                    "PWN_BR-179",
                    "PWN_BR_0002",
                    "PWN_BR_0003"
                    
                    

                };
                var pawnBusinessRuleVO = PawnBusinessRuleVO;
                PawnRulesSys.beginSite(this, this.CurrentSiteId, bRules, out pawnBusinessRuleVO);
                PawnRulesSys.getParameters(this, this.CurrentSiteId, ref pawnBusinessRuleVO);
                PawnRulesSys.endSite(this, this.CurrentSiteId);
                PawnBusinessRuleVO = pawnBusinessRuleVO;
                if (FileLogger.Instance.IsLogInfo)
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, this, "Finished loading business rules");
                }
            }
            catch (Exception exp)
            {
                //BasicExceptionHandler.Instance.AddException("GetPawnBusinessRulesFailed", new ApplicationException("Cannot execute the GetPawnBusinessRules during StartUp. [" + exp.Message + "]"));
                //throws exception, read it here so the stack doesnt go awry
                FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Could not process business rules: " + exp);
                throw new ApplicationException("Business rules failure", exp);
            }
        }

        public override void UpdateShopDate(Form deskForm)
        {
            var sdt = ShopDateTime.Instance;
            if (sdt != null)
            {
                //Get shop date label
                var dtLabel = deskForm.Controls["shopDateLabel"];
                //If shop date label is valid, set to current shop date
                if (dtLabel != null)
                {
                    dtLabel.Text = sdt.ShopDate.FormatDate();
                }
            }
            //If shop date time value is invalid, hide the date label
            else
            {
                //Get shop date label
                var dtLabel = deskForm.Controls["shopDateLabel"];
                //If shop date label is valid, hide it
                if (dtLabel != null)
                {
                    dtLabel.Hide();
                }
            }
            if (timer == null)
            {
                timer = new Timer
                        {
                            Tag = deskForm,
                            Interval = 60 * 60 * 1000
                        };
                timer.Tick += timer_Tick;
                timer.Start();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (sender == null)
                return;
            timer = sender as Timer;
            if (timer == null)
                return;
            var deskForm = timer.Tag as Form;
            UpdateShopDate(deskForm);
        }

        void forceCloseTimer_Tick(object sender, EventArgs e)
        {
            if (sender == null)
                return;
            forceCloseTimer = sender as Timer;
            if (forceCloseTimer == null)
                return;
            var deskForm = forceCloseTimer.Tag as Form;
            ShowForceCloseMessagesWhenNeeded(deskForm);
        }

        private void ShowForceCloseMessagesWhenNeeded(Form deskForm)
        {
            var fLog = FileLogger.Instance;
            fLog.logMessage(LogLevel.INFO, this, "ShowForceCloseMessageWhenNeeded executing...");
            //Determine if the force close time of the shop is set to DateTime.MinValue, if so,
            //this means the store's force close time was invalid in the database and that we 
            //should not perform any force close processing
            if (this.CurrentSiteId.ForceCloseTime == DateTime.MinValue)
            {
                fLog.logMessage(LogLevel.WARN,
                                this,
                                "Store force close time is set to DateTime.MinValue, stopping force close timer");
                this.forceCloseTimer.Stop();
                return;
            }

            //Check if we are within 30 minutes of the force close time
            var currentShopDT = ShopDateTime.Instance.FullShopDateTime;
            var cShopTime = currentShopDT.TimeOfDay;
            var fClose = this.CurrentSiteId.ForceCloseTime;
            var fCloseTime = fClose.TimeOfDay;
            if (fLog.IsLogDebug)
            {
                fLog.logMessage(LogLevel.DEBUG, this, "- Shop       Date/Time:{0}", currentShopDT);
                fLog.logMessage(LogLevel.DEBUG, this, "- Shop       Time:     {0}", cShopTime);
                fLog.logMessage(LogLevel.DEBUG, this, "- ForceClose Date/Time:{0}", fClose);
                fLog.logMessage(LogLevel.DEBUG, this, "- ForceClose Time:     {0}", fCloseTime);
            }

            //Determine the state of the store - 
            var storeStatus = string.Empty;
            var errorCode = string.Empty;
            var errorText = string.Empty;
            if (!ShopProcedures.ExecuteGetStoreStatusCode(
                GlobalDataAccessor.Instance.OracleDA,
                GlobalDataAccessor.Instance.CurrentSiteId,
                out storeStatus,
                out errorCode,
                out errorText))
            {
                var strErr = string.Format(
                    "Could not retrieve current store status - ErrorCode:{0} ErrorText:{1}",
                    errorCode,
                    errorText);
                fLog.logMessage(LogLevel.ERROR, this, strErr);
                BasicExceptionHandler.Instance.AddException(strErr, new ApplicationException(strErr));
                return;
            }

            //If we have passed the force close time, show a message for 5 seconds, then kill the client
            if (fCloseTime.CompareTo(cShopTime) <= 0)
            {
                if (!storeStatus.Equals("SIGNEDOFF", StringComparison.OrdinalIgnoreCase))
                {
                    if (fLog.IsLogWarn)
                    {
                        fLog.logMessage(LogLevel.WARN, this, "Store is at or past the force close time, killing application");
                    }
                    //Force close time passed
                    forceCloseTimer.Stop();
                    //Determine if user is running transaction
                    //If the user is running a transaction, do not kill the application
                    //If the user is not running a transaction, kill the application
                    ShowInfoMessageForSpecifiedTime("Cashlinx Force Closure",
                                                    "The store has passed the force close window. " + System.Environment.NewLine +
                                                    " No transactions are allowed at this time. Application Exiting...", 5, deskForm);
                    fLog.logMessage(LogLevel.ERROR, this, "- ShowForceCloseMessageWhenNeeded closing application - Past Force Close Window");
                    Application.Exit();
                    return;
                }

                if (!string.IsNullOrEmpty(storeStatus) && storeStatus.Equals("SIGNEDOFF", StringComparison.OrdinalIgnoreCase))
                {
                    if (fLog.IsLogWarn)
                    {
                        fLog.logMessage(LogLevel.WARN, this,
                                        "Application is still active past the force close time. " +
                                        "The store is signed off.  No transactions can be run at this time.");
                    }

                    //Force close time passed
                    forceCloseTimer.Stop();
                    ShowInfoMessageForSpecifiedTime("Cashlinx Application Closure",
                                                    "The store is now signed off.  The Cashlinx application is closing.",
                                                    5, deskForm);
                    fLog.logMessage(LogLevel.ERROR, this,
                                    "- Closing the Cashlinx application.  Application was still running even though the store is signed off.");
                    Application.Exit();
                    return;
                }
            }

            var diff = fCloseTime.Subtract(cShopTime);
            fLog.logMessage(LogLevel.DEBUG, this, "- The force close window difference = {0} days, {1} hours, {2} minutes, and {3} seconds",
                            diff.Days,
                            diff.Hours,
                            diff.Minutes,
                            diff.Seconds);
            if (diff.Minutes <= 30 && diff.Hours <= 0 && diff.Days <= 0)
            {
                if (fLog.IsLogDebug)
                {
                    fLog.logMessage(LogLevel.DEBUG, this, "- The force close time is in {0} minutes and {1} seconds", diff.Minutes,
                                    diff.Seconds);
                }
                if (storeStatus.Equals("SIGNEDON", StringComparison.OrdinalIgnoreCase))
                {
                    if (diff.Minutes > 0 && !this.ForceCloseMessageShown)
                    {
                        ShowInfoMessageForSpecifiedTime("Cashlinx Force Closure",
                                                        "The store will be automatically forced close in " + diff.Minutes + " minutes. " +
                                                        System.Environment.NewLine, 10, deskForm);
                        this.ForceCloseMessageShown = true;
                    }
                    else if (diff.Minutes == 0 && diff.Seconds >= 0)
                    {
                        fLog.logMessage(LogLevel.WARN, this, "Store is {0} seconds from force closure, killing application", diff.Seconds);
                        forceCloseTimer.Stop();
                        ShowInfoMessageForSpecifiedTime("Cashlinx Force Closure",
                                                        "The store will be automatically forced closed when this window closes.",
                                                        (uint)diff.Seconds, deskForm);
                        fLog.logMessage(LogLevel.ERROR, this,
                                        "- ShowForceCloseMessageWhenNeeded closing application - Seconds From Force Close Window");
                        Application.Exit();
                        return;
                    }
                    /*else 
                    {
                        fLog.logMessage(LogLevel.WARN, this, "Store is now at force closure time, killing application");
                        forceCloseTimer.Stop();
                        ShowInfoMessageForSpecifiedTime("Cashlinx Force Closure",
                                                        "The store will be automatically forced closed when this window closes.", 5, deskForm);
                        fLog.logMessage(LogLevel.ERROR, this,
                                        "- ShowForceCloseMessageWhenNeeded closing application - At Force Close Window");
                        Application.Exit();
                        return;
                    }*/
                }
                else
                {
                    if (diff.Minutes > 0 && !this.ForceCloseMessageShown)
                    {
                        ShowInfoMessageForSpecifiedTime("Cashlinx Application Closure",
                                                        "The application will automatically close in " + diff.Minutes + " minutes. " +
                                                        System.Environment.NewLine, 5, deskForm);
                        this.ForceCloseMessageShown = true;
                    }
                    else if (diff.Minutes == 0 && diff.Seconds >= 0)
                    {
                        fLog.logMessage(LogLevel.WARN, this, "The application is {0} seconds from being closed.", diff.Seconds);
                        forceCloseTimer.Stop();
                        ShowInfoMessageForSpecifiedTime("Cashlinx Application Closure",
                                                        "The application will be automatically closed when this window closes.",
                                                        (uint)diff.Seconds, deskForm);
                        fLog.logMessage(LogLevel.ERROR, this,
                                        "- ShowForceCloseMessageWhenNeeded closing application - Seconds From Application Close Window");
                        Application.Exit();
                        return;
                    }
                    /* else if (diff.Minutes == 0 && diff.Seconds == 0)
                     {
                         fLog.logMessage(LogLevel.WARN, this, "Application has reached automatic closing time, killing application");
                         forceCloseTimer.Stop();
                         ShowInfoMessageForSpecifiedTime("Cashlinx Application Closure",
                                                         "The application will be automatically closed when this window closes.", 5, deskForm);
                         fLog.logMessage(LogLevel.ERROR, this,
                                         "- ShowForceCloseMessageWhenNeeded closing application - At Force Close Window");
                         Application.Exit();
                         return;
                     }*/
                }
            }
            else
            {
                fLog.logMessage(LogLevel.INFO, this, "...ShowForceCloseMessageWhenNeeded finished");
            }
        }

        private static void ShowInfoMessageForSpecifiedTime(string header, string msg, uint timeToShow, Form owner)
        {
            if (timeToShow == 0)
            {
                //Must be at least one second
                timeToShow = 1;
            }
            var cnt = timeToShow;
            var shownAlready = false;
            var iDialog = new InfoDialog();
            iDialog.HeaderToShow = header;
            while (cnt > 0)
            {
                iDialog.MessageToShow =
                msg +
                System.Environment.NewLine +
                " This window will automatically close in " + cnt + " seconds.";
         
                if (!shownAlready)
                {
                    iDialog.Show(owner);
                    iDialog.BringToFront();
                    shownAlready = true;
                }
                else if (iDialog.AlreadyClosed == false)
                {
                    iDialog.Update();
                    iDialog.BringToFront();
                }
                else
                {
                    break;
               }
                //Wait one second, then update
                Utilities.WaitMillis(1000);
                cnt--;
            }
            if (!iDialog.AlreadyClosed)
            {
                iDialog.Close();
            }
            iDialog.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deskForm"></param>
        private void UpdateVersionLabel(Form deskForm)
        {
            if (!string.IsNullOrEmpty(SecurityAccessor.Instance.EncryptConfig.ClientConfig.AppVersion.AppVersion) &&
                deskForm != null)
            {
                //Get version label control
                var verLabel = deskForm.Controls["versionLabel"];
                //If label is valid, set to app version
                if (verLabel != null)
                {
                    verLabel.Text =
                        SecurityAccessor.Instance.EncryptConfig.ClientConfig.AppVersion.AppVersion;
                }
            }
            else if (deskForm != null)
            {
                var verLabel = deskForm.Controls["versionLabel"];
                if (verLabel != null)
                {
                    verLabel.Hide();
                }
            }
        }

        /// <summary>
        /// Update the user name field on the new desktop form
        /// </summary>
        /// <param name="deskForm">The new desktop form</param>
        private void UpdateDesktopUserName(Form deskForm)
        {
#if !__MULTI__
            if (!String.IsNullOrEmpty(this.UserName) && deskForm != null)
            {
                //Update user name on desktop
                var userInfoGroupBox = deskForm.Controls["userInfoGroupBox"];
                var userNameField = userInfoGroupBox.Controls["userNameField"];
                var userEmpIdField = userInfoGroupBox.Controls["userEmpIdField"];
                var userRoleField = userInfoGroupBox.Controls["userRoleField"];
                var shopDateField = userInfoGroupBox.Controls["shopDateField"];

                if (userNameField != null)
                {
                    userNameField.Text = !string.IsNullOrEmpty(DisplayName) ? DisplayName : UserName;
                    userNameField.Update();
                }

                if (userEmpIdField != null)
                {
                    if (this.UserName.Length >= 5)
                    {
                        int idx = this.UserName.Length - 5;
                        userEmpIdField.Text =
                        this.UserName.Substring(this.UserName.Length - 5, this.UserName.Length - idx);
                    }
                    else
                    {
                        userEmpIdField.Text = this.UserName;
                    }
                }
                if (LoggedInUserSecurityProfile != null)
                {
                    if (userRoleField != null && LoggedInUserSecurityProfile.UserRole != null)
                    {
                        userRoleField.Text = LoggedInUserSecurityProfile.UserRole.RoleName;
                    }
                }
                if (shopDateField != null)
                    shopDateField.Text = ShopDateTime.Instance.ShopDate.ToShortDateString();
                if (!userInfoGroupBox.Visible)
                {
                    userInfoGroupBox.Show();
                }
            }
            else if (deskForm != null)
            {
                deskForm.Controls["userInfoGroupBox"].Hide();
                deskForm.Controls["userInfoGroupBox"].Controls["userNameField"].Text = string.Empty;
                deskForm.Controls["userInfoGroupBox"].Controls["userEmpIdField"].Text = string.Empty;
                deskForm.Controls["userInfoGroupBox"].Controls["userRoleField"].Text = string.Empty;
            }
#endif
        }

        /// <summary>
        /// Updates customer information on the desktop
        /// </summary>
        /// <param name="deskForm"></param>
        public override void UpdateDesktopCustomerInformation(Form deskForm)
        {
#if !__MULTI__
            if (this.customers[0] == null)
            {
                return;
            }
            var customerInfoGroupBox = deskForm.Controls["customerInfoGroupBox"];
            var customerNameField = customerInfoGroupBox.Controls["customerNameField"];
            var customerAddressField = customerInfoGroupBox.Controls["customerAddressField"];
            var customerDOBField = customerInfoGroupBox.Controls["customerDOBField"];

            // update Customer Name field
            if (customerNameField != null)
            {
                if (!String.IsNullOrEmpty(this.customers[0].LastName) &&
                    !String.IsNullOrEmpty(this.customers[0].FirstName))
                {
                    customerNameField.Text = string.Format("{0}, {1}", this.customers[0].LastName, this.customers[0].FirstName);
                }
                else
                {
                    customerNameField.Text = string.Empty;
                }
                customerNameField.Update();
            }

            // update Customer Address field
            if (customerAddressField != null)
            {
                AddressVO customerAddress = this.customers[0].getAddress(0);

                if (customerAddress != null &&
                    !String.IsNullOrEmpty(customerAddress.Address1) &&
                    !String.IsNullOrEmpty(customerAddress.City) &&
                    !String.IsNullOrEmpty(customerAddress.State_Code) &&
                    !String.IsNullOrEmpty(customerAddress.ZipCode))
                {
                    customerAddressField.Text = string.Format("{0} {1}, {2} {3}", customerAddress.Address1, customerAddress.City, customerAddress.State_Code, customerAddress.ZipCode);
                }
                else
                {
                    customerAddressField.Text = string.Empty;
                }
                customerAddressField.Update();
            }

            // update Customer Age field
            if (customerDOBField == null)
            {
                return;
            }
            customerDOBField.Text = this.customers[0].DateOfBirth.FormatDate();
            customerDOBField.Update();
#endif
        }

        /// <summary>
        /// Controls whether the Customer Information is shown
        /// </summary>
        /// <param name="deskForm"></param>
        /// <param name="show"></param>
        public override void ShowDesktopCustomerInformation(Form deskForm, bool show)
        {
#if !__MULTI__
            var customerInformation = deskForm.Controls["customerInfoGroupBox"];

            if (customerInformation == null)
            {
                return;
            }
            if (!show)
            {
                if (customerInformation.Visible)
                {
                    customerInformation.Hide();
                    var customerNameField = customerInformation.Controls["customerNameField"];
                    var customerAddressField = customerInformation.Controls["customerAddressField"];
                    var customerDOBField = customerInformation.Controls["customerDOBField"];

                    customerNameField.Text = string.Empty;
                    customerAddressField.Text = string.Empty;
                    customerDOBField.Text = string.Empty;
                }
            }
            else
            {
                if (this.customers[0] != null)
                {
                    if (!customerInformation.Visible)
                    {
                        UpdateDesktopCustomerInformation(deskForm);
                        customerInformation.Show();
                    }
                }
            }
#endif
        }

        // TL 02-09-2010 Wipe Drive Parsing Method
        private void ParseWipeDriveCategories(string categories)
        {
            if (String.IsNullOrEmpty(categories))
                return;

            var categoryList = new List<int>();

            try
            {
                // check for delimiter
                if (categories.Contains("|"))
                {
                    // multiple categories, create array of strings
                    string[] categoryArray = categories.Split(new[] { '|' });

                    // check that the array isn't empty
                    if (categories.Length > 0)
                    {
                        foreach (var s in categoryArray)
                        {
                            // if current string is a range instead of an individual category
                            if (s.Contains("-"))
                            {
                                var dashIndex = s.IndexOf('-');
                                var head = s.Substring(0, dashIndex);
                                var tail = s.Substring(dashIndex + 1);

                                // make sure head and tail are valid strings
                                if (!String.IsNullOrEmpty(head) && !String.IsNullOrEmpty(tail))
                                {
                                    // get numeric versions of categories
                                    int headNumber;
                                    int tailNumber;

                                    Int32.TryParse(head, out headNumber);
                                    Int32.TryParse(tail, out tailNumber);

                                    // add all categories
                                    for (int i = headNumber; i <= tailNumber; ++i)
                                    {
                                        categoryList.Add(i);
                                    }
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                // a single category exists within the delimiter
                                int numericSingleCategory;

                                Int32.TryParse(s, out numericSingleCategory);
                                categoryList.Add(numericSingleCategory);
                            }
                        }
                    }
                    else
                    {
                        // bad data from BR
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Wipe Drive Categories has bad data!");
                        return;
                    }
                }
                else
                {
                    // only one category exists in the categories
                    int numericCategory = 0;

                    Int32.TryParse(categories, out numericCategory);
                    categoryList.Add(numericCategory);
                }

                // check to make sure that category list was populated
                if (categoryList.Count > 0)
                {
                    // everything succeeded at this point, set CDS list
                    this.WipeDriveCategories = categoryList;
                }
            }
            catch (Exception e)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Wipe Drive Categories failed to load properly!");
                BasicExceptionHandler.Instance.AddException("WipeDriveCategoriesFailed",
                                                            new ApplicationException("Wipe Drive Categories failed to load properly. [" + e.Message + "]"));
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scrapItems"></param>
        private void ParseScrapItems(string scrapItems)
        {
            if (String.IsNullOrEmpty(scrapItems))
                return;

            var scrapList = new List<string>();

            try
            {
                // check for delimiter
                if (scrapItems.Contains("|"))
                {
                    // multiple categories, create array of strings
                    string[] scrapArray = scrapItems.Split(new char[] { '|' });

                    // check that the array isn't empty
                    if (scrapItems.Length > 0)
                    {
                        foreach (string s in scrapArray)
                        {
                            scrapList.Add(s);
                        }
                    }
                    else
                    {
                        // bad data from BR
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Scrap items from Business Rule has bad data!");
                        return;
                    }
                }

                // check to make sure that category list was populated
                if (scrapList.Count > 0)
                {
                    // everything succeeded at this point, set CDS list
                    this.ScrapTypes = scrapList;
                }
            }
            catch (Exception e)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Scrap items failed to load properly!");
                BasicExceptionHandler.Instance.AddException("ScrapListFailed",
                                                            new ApplicationException("Scrap items failed to load properly. [" + e.Message + "]"));
                return;
            }
        }

        /// <summary>
        /// Gets the receipt printer details
        /// </summary>
        /// <returns></returns>
        private void GetReceiptPrinter()
        {
            string storeNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            const string formName = "ReceiptPrinter";

            string terminalId = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId;

            //Set default value
            DataTable docinfo;
            DataTable printerinfo;
            DataTable ipportinfo;

            string errorCode;
            string errorMessage;

            //Setup input params
            var inParams = new List<OracleProcParam>
            {
                new OracleProcParam("p_terminalid", terminalId),
                new OracleProcParam("p_form_name", formName),
                new OracleProcParam("p_store_number", storeNumber)
            };

            //Invoke generate documents stored proc
            Hashtable receiptData = GenerateDocumentsProcedures.GenerateDocumentsEssentialInformation(
                inParams, out docinfo,
                out printerinfo,
                out ipportinfo,
                out errorCode,
                out errorMessage);

            //Check receipt data
            if (receiptData == null || receiptData.Count <= 0 ||
                !receiptData.ContainsKey("##IPADDRESS01##") ||
                !receiptData.ContainsKey("##PORTNUMBER01##"))
            {
                string errMsg = string.Format("Cannot get receipt printer information: {0}:{1}", errorCode, errorMessage);
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, errMsg);
                }
                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
            }
            else
            {
                var receiptIpObj = receiptData["##IPADDRESS01##"];
                var receiptPortObj = receiptData["##PORTNUMBER01##"];
                if (receiptIpObj != null && receiptPortObj != null)
                {
                    var receiptIp = Utilities.GetStringValue(receiptIpObj, string.Empty);
                    var receiptPort = Utilities.GetStringValue(receiptPortObj, string.Empty);
                    if (!string.IsNullOrEmpty(receiptIp) && !string.IsNullOrEmpty(receiptPort))
                    {
                        int pPort;
                        if (int.TryParse(receiptPort, out pPort))
                        {
                            this.ReceiptPrinter.SetIPAddressAndPort(receiptIp, pPort);
                            if (FileLogger.Instance.IsLogDebug)
                            {
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, this,
                                                               "Receipt printer information retrieved successfully: {0}",
                                                               this.ReceiptPrinter);
                            }
                        }
                    }
                }
            }

            if (!this.ReceiptPrinter.IsValid)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                   "There is no receipt printer configured for Cashlinx Pawn application");
                }
            }

        }

        /// <summary>
        /// Gets the Intermec printer details
        /// </summary>
        /// <returns></returns>
        private void GetIntermecPrinter()
        {
            var storeNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            const string formName = "BarcodePrinter";

            string terminalId = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId;

            //Set default value
            DataTable docinfo;
            DataTable printerinfo;
            DataTable ipportinfo;

            string errorCode;
            string errorMessage;

            //Setup input params
            var inParams = new List<OracleProcParam>
            {
                new OracleProcParam("p_terminalid", terminalId),
                new OracleProcParam("p_form_name", formName),
                new OracleProcParam("p_store_number", storeNumber)
            };

            //Invoke generate documents stored proc
            Hashtable intermecData = GenerateDocumentsProcedures.GenerateDocumentsEssentialInformation(
                inParams, out docinfo,
                out printerinfo,
                out ipportinfo,
                out errorCode,
                out errorMessage);

            //Check intermec data
            if (intermecData == null || intermecData.Count <= 0 ||
                !intermecData.ContainsKey("##IPADDRESS01##") ||
                !intermecData.ContainsKey("##PORTNUMBER01##"))
            {
                string errMsg = string.Format("Cannot get barcode printer information: {0}:{1}", errorCode, errorMessage);
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, errMsg);
                }
                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
            }
            else
            {
                var barcodeIpObj = intermecData["##IPADDRESS01##"];
                var barcodePortObj = intermecData["##PORTNUMBER01##"];
                if (barcodeIpObj != null && barcodePortObj != null)
                {
                    var barcodeIp = Utilities.GetStringValue(barcodeIpObj, string.Empty);
                    var barcodePort = Utilities.GetStringValue(barcodePortObj, string.Empty);
                    if (!string.IsNullOrEmpty(barcodeIp) && !string.IsNullOrEmpty(barcodePort))
                    {
                        int pPort;
                        if (int.TryParse(barcodePort, out pPort))
                        {
                            this.BarcodePrinter.SetIPAddressAndPort(barcodeIp, pPort);
                            if (FileLogger.Instance.IsLogDebug)
                            {
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, this,
                                                               "Barcode printer information retrieved successfully: {0}",
                                                               this.BarcodePrinter);
                            }
                        }
                    }
                }
            }

            if (!this.BarcodePrinter.IsValid)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                   "There is no barcode printer configured for Cashlinx Pawn application");
                }
            }
        }

        /// <summary>
        /// Get default laser printer for this client
        /// </summary>
        private void GetLaserPrinter()
        {
            string storeNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            const string formName = "LaserPrinter";

            string terminalId = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId;

            //Set default value
            DataTable docinfo;
            DataTable printerinfo;
            DataTable ipportinfo;

            string errorCode;
            string errorMessage;

            //Setup input params
            var inParams = new List<OracleProcParam>
            {
                new OracleProcParam("p_terminalid", terminalId),
                new OracleProcParam("p_form_name", formName),
                new OracleProcParam("p_store_number", storeNumber)
            };

            //Invoke generate documents stored proc
            var laserData = GenerateDocumentsProcedures.GenerateDocumentsEssentialInformation(
                inParams, out docinfo,
                out printerinfo,
                out ipportinfo,
                out errorCode,
                out errorMessage);

            //Check laser printer data
            if (laserData == null || laserData.Count <= 0 ||
                !laserData.ContainsKey("##IPADDRESS01##") ||
                !laserData.ContainsKey("##PORTNUMBER01##"))
            {
                string errMsg = string.Format("Cannot get laser printer information: {0}:{1}", errorCode, errorMessage);
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, errMsg);
                }
                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
            }
            else
            {
                var laserIpObj = laserData["##IPADDRESS01##"];
                var laserPortObj = laserData["##PORTNUMBER01##"];
                if (laserIpObj != null && laserPortObj != null)
                {
                    var laserIp = Utilities.GetStringValue(laserIpObj, string.Empty);
                    var laserPort = Utilities.GetStringValue(laserPortObj, string.Empty);
                    if (!string.IsNullOrEmpty(laserIp) && !string.IsNullOrEmpty(laserPort))
                    {
                        int pPort;
                        if (int.TryParse(laserPort, out pPort))
                        {
                            this.LaserPrinter.SetIPAddressAndPort(laserIp, pPort);
                            if (FileLogger.Instance.IsLogDebug)
                            {
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, this,
                                                               "Laser printer information retrieved successfully: {0}",
                                                               this.LaserPrinter);
                            }
                        }
                    }
                }
            }

            if (!this.LaserPrinter.IsValid)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                   "There is no laser printer configured for Cashlinx Pawn application");
                }
            }
        }

        /// <summary>
        /// Get PDA laser printer for this client if it is mapped
        /// </summary>
        private void GetPDALaserPrinter()
        {
            string storeNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            const string formName = "LaserPrinter";
            string terminalId = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId;

            //Set default value
            DataTable docinfo;
            DataTable printerinfo;
            DataTable ipportinfo;

            string errorCode;
            string errorMessage;

            //Setup input params
            var inParams = new List<OracleProcParam>
            {
                new OracleProcParam("p_terminalid", terminalId),
                new OracleProcParam("p_form_name", formName),
                new OracleProcParam("p_store_number", storeNumber)
            };

            //Invoke generate documents stored proc (set pda flag to true)
            var laserData =
                GenerateDocumentsProcedures.GenerateDocumentsEssentialInformation(
                inParams, true, //<--- setting flag to retrieve PDA mapping
                out docinfo,
                out printerinfo,
                out ipportinfo,
                out errorCode,
                out errorMessage);

            //Check pda laser data
            //Check laser printer data
            if (laserData == null || laserData.Count <= 0 ||
                !laserData.ContainsKey("##IPADDRESS01##") ||
                !laserData.ContainsKey("##PORTNUMBER01##"))
            {
                string errMsg = string.Format("Cannot get PDA laser printer information: {0}:{1}", errorCode, errorMessage);
                if (FileLogger.Instance.IsLogWarn)
                {
                    FileLogger.Instance.logMessage(LogLevel.WARN, this, errMsg);
                }
            }
            else
            {
                var laserIpObj = laserData["##IPADDRESS01##"];
                var laserPortObj = laserData["##PORTNUMBER01##"];
                if (laserIpObj != null && laserPortObj != null)
                {
                    var laserIp = Utilities.GetStringValue(laserIpObj, string.Empty);
                    var laserPort = Utilities.GetStringValue(laserPortObj, string.Empty);
                    if (!string.IsNullOrEmpty(laserIp) && !string.IsNullOrEmpty(laserPort))
                    {
                        int pPort;
                        if (int.TryParse(laserPort, out pPort))
                        {
                            this.PDALaserPrinter.SetIPAddressAndPort(laserIp, pPort);
                            if (FileLogger.Instance.IsLogDebug)
                            {
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, this,
                                                               "PDA Laser printer information retrieved successfully: {0}",
                                                               this.LaserPrinter);
                            }
                        }
                    }
                }
            }

            if (!this.LaserPrinter.IsValid)
            {
                if (FileLogger.Instance.IsLogWarn)
                {
                    FileLogger.Instance.logMessage(LogLevel.WARN, this,
                                                   "There is no PDA laser printer configured for Cashlinx Pawn application");
                }
            }
        }

        /// <summary>
        /// Get default laser printer for this client
        /// </summary>
        private void GetIndianaPoliceCardPrinter()
        {
            string storeNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            const string formName = "IndianaPoliceCardPrinter";

            string terminalId = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId;

            //Set default value
            DataTable docinfo;
            DataTable printerinfo;
            DataTable ipportinfo;

            string errorCode;
            string errorMessage;

            // Setup input params
            var inParams = new List<OracleProcParam>
            {
                new OracleProcParam("p_terminalid", terminalId),
                new OracleProcParam("p_form_name", formName),
                new OracleProcParam("p_store_number", storeNumber)
            };

            // Invoke generate documents stored proc
            var dotmatrixData = GenerateDocumentsProcedures.GenerateDocumentsEssentialInformation(
                inParams, out docinfo,
                out printerinfo,
                out ipportinfo,
                out errorCode,
                out errorMessage);

            // Check dot matrix printer data
            if (dotmatrixData == null || dotmatrixData.Count <= 0 ||
                !dotmatrixData.ContainsKey("##IPADDRESS01##") ||
                !dotmatrixData.ContainsKey("##PORTNUMBER01##"))
            {
                string errMsg = string.Format("Cannot get dot matrix printer information: {0}:{1}", errorCode, errorMessage);
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, errMsg);
                }
                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
            }
            else
            {
                var dotmatrixIpObj = dotmatrixData["##IPADDRESS01##"];
                var dotmatrixPortObj = dotmatrixData["##PORTNUMBER01##"];
                if (dotmatrixIpObj != null && dotmatrixPortObj != null)
                {
                    var dotmatrixIp = Utilities.GetStringValue(dotmatrixIpObj, string.Empty);
                    var dotmatrixPort = Utilities.GetStringValue(dotmatrixPortObj, string.Empty);
                    if (!string.IsNullOrEmpty(dotmatrixIp) && !string.IsNullOrEmpty(dotmatrixPort))
                    {
                        int pPort;
                        if (int.TryParse(dotmatrixPort, out pPort))
                        {
                            this.IndianaPoliceCardPrinter.SetIPAddressAndPort(dotmatrixIp, pPort);
                            if (FileLogger.Instance.IsLogDebug)
                            {
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, this,
                                                               "Indiana Dot Matrix printer information retrieved successfully: {0}",
                                                               this.IndianaPoliceCardPrinter);
                            }
                        }
                    }
                }
            }

            if (!this.IndianaPoliceCardPrinter.IsValid)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                   "There is no Indiana Dot Matrix printer configured for Cashlinx Pawn application");
                }
            }
        }

        // Pre-Load the Metal/Stones for Describe Merchandise processing
        private void GetMetalStonesValues()
        {
            try
            {
                string sErrorCode;
                string sErrorMessage;

                var businessRule = PawnBusinessRuleVO["PWN_BR-000"];
                var sComponentValue = string.Empty;
                if (businessRule != null)
                    businessRule.getComponentValue("PK_PMETAL_FILE", ref sComponentValue);
                var sMetalFile = sComponentValue;
                if (businessRule != null)
                    businessRule.getComponentValue("PK_STONES_FILE", ref sComponentValue);
                var sStoneFile = sComponentValue;

                var pMetalData = PMetalData;
                var stonesData = StonesData;
                var retVal = PMetalStonesProcedures.ExecuteGetMetalStones(sMetalFile, sStoneFile, out pMetalData, out stonesData, out sErrorCode, out sErrorMessage);
                if (retVal == false)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Stored procedure call to get metal and stones failed!");
                    return;
                }
                PMetalData = pMetalData;
                StonesData = stonesData;
            }
            catch (Exception exp)
            {
                BasicExceptionHandler.Instance.AddException("GetMetalStonesValuesFailed", new ApplicationException("Cannot execute the GetMetalStonesValues during StartUp. [" + exp.Message + "]"));
            }
        }

        // Pre-Load the Merchandise Manufacturers for DescribeMerchandise
        private void GetMerchandiseManufacturers()
        {
            try
            {
                string sErrorCode;
                string sErrorMessage;
                DataTable dtMerchandiseManufacturers;
                var retVal = MerchandiseProcedures.ExecuteGetManufacturers(1, out dtMerchandiseManufacturers, out sErrorCode, out sErrorMessage);
                if (retVal == false)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot load manufacturers in CashlinxDesktopSession");
                    return;
                }
                MerchandiseManufacturers = dtMerchandiseManufacturers;
            }
            catch (Exception exp)
            {
                BasicExceptionHandler.Instance.AddException("GetMerchandiseManufacturersFailed", new ApplicationException("Cannot execute the GetMerchandiseManufacturers during StartUp. [" + exp.Message + "]"));
            }
        }

        /// <summary>
        /// call to the database to get all picklist values related to customer use case
        /// </summary>
        private void GetPickListValues()
        {
            try
            {
                //data tables with data that are needed for comboboxes in customer screens
                DataTable countryTable; //stores country data
                DataTable eyeColorTable; //stores eye color info from the db
                DataTable hairColorTable; //stores hair color info from the db
                DataTable idTable; //stores ID type info from the db
                DataTable raceTable; //stores race info from the db
                DataTable stateTable; //stores US states info from the db
                DataTable titleSuffixTable; //stores title suffix data from the db
                DataTable titleTable; //stores title info from the db
                var hearAbtUsTable = new DataTable("HearAboutUs"); //stored Values for Hear about us from the db

                var sErrorCode = string.Empty;
                var sErrorMessage = string.Empty;
                new CustomerDBProcedures(this).getPicklistValues(
                    out countryTable,
                    out eyeColorTable,
                    out hairColorTable,
                    out idTable,
                    out raceTable,
                    out stateTable,
                    out titleSuffixTable,
                    out titleTable,
                    out sErrorCode,
                    out sErrorMessage);
                EyeColorTable = eyeColorTable;
                CountryTable = countryTable;
                StateTable = stateTable;
                RaceTable = raceTable;
                HairColorTable = hairColorTable;
                TitleSuffixTable = titleSuffixTable;
                TitleTable = titleTable;
                IdTypeTable = idTable;
                HearAboutUsTable = hearAbtUsTable;
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("GetPickListValues failed", new ApplicationException("Get Pick list values failed", ex));
            }
        }

        private void GetVarianceRates()
        {
            var varianceRate = new VarianceRate();
            varianceRate.Setup();
            VarianceRates = varianceRate.VarianceRates;
        }

        // Initial Web Service search to instantiate the object to avoid later latency
        private void StartUpWebService()
        {
            try
            {
                this.CallProKnow = new WebServiceProKnow(this);
                this.CallProKnow.GetProKnowDetails("BOSE", "301");
            }
            catch (Exception exp)
            {
                BasicExceptionHandler.Instance.AddException("StartUpWebServiceFailed", new ApplicationException("Cannot connect the StartUpWebService during StartUp. [" + exp.Message + "]"));
            }
        }

        /// <summary>
        /// Will return a false if the user cancels out of the authentication process
        /// </summary>
        /// <returns></returns>
        private bool PerformLDAPAuthentication(
            ref int attemptCount,
            ref string username,
            ref string password,
            out bool lockedOut,
            out bool needToChangePassword,
            out bool wantsPasswordChange)
        {
            needToChangePassword = false;
            wantsPasswordChange = false;
            lockedOut = false;
            if (this.IsSkipLDAP)
            {
                return (true);
            }
            var pawnLDAPAccessor = PawnLDAPAccessor.Instance;
            if (this.userState == UserDesktopState.NOTLOGGEDIN &&
                pawnLDAPAccessor.State == PawnLDAPAccessor.LDAPState.CONNECTED)
            {
                try
                {
                    //Show login form and utilize LDAP for authentication
                    var userLoginForm = new UserLogin(this);

                    userLoginForm.EnteredUserName = "dm42133";
                    userLoginForm.EnteredPassWord = "xyz12345";
                    var dR = DialogResult.OK; //userLoginForm.ShowDialog();
                    if (dR == DialogResult.OK)
                    {
                        LoginCancel = false;
                        username = userLoginForm.EnteredUserName;
                        password = userLoginForm.EnteredPassWord;
                        DateTime initialLastModifiedDate;
                        string userDisplayName;
                        string[] pwdHistory;
                        if (pawnLDAPAccessor.AuthorizeUser(
                            username,
                            password,
                            ref attemptCount,
                            out initialLastModifiedDate,
                            out pwdHistory,
                            out userDisplayName,
                            out lockedOut))
                        {
                            if (lockedOut)
                            {
                                return (false);
                            }

                            //Perform adjustment to last modified date
                            DateTime lastModifiedDate;
                            ShopDateTime.Instance.AdjustDateTime(initialLastModifiedDate,
                                                                 out lastModifiedDate);

                            //Check if the password expired);
                            if (pawnLDAPAccessor.PasswordPolicy.IsExpired(
                                lastModifiedDate, ShopDateTime.Instance.ShopDateInGMT))
                            {
                                needToChangePassword = true;
                            }
                            //check if the password is about to expire
                            else if (pawnLDAPAccessor.PasswordPolicy.IsExpiredWarning(
                                lastModifiedDate, ShopDateTime.Instance.ShopDateInGMT))
                            {
                                DialogResult dgr =
                                MessageBox.Show("Your password is about to expire. Would you like to change it?", "Application Security", MessageBoxButtons.YesNo);
                                if (dgr == DialogResult.Yes)
                                {
                                    wantsPasswordChange = true;
                                }
                            }
                            LoggedInUserSecurityProfile.UserCurrentPassword = password;
                            //Get LDAP info
                            string loginDN;
                            string pwdPolicyCN;
                            string searchDN;
                            string userIdKey;
                            string userPwd;
                            //string userName;

                            var conf = SecurityAccessor.Instance.EncryptConfig;
                            conf.GetLDAPService(
                                out loginDN,
                                out searchDN,
                                out userIdKey,
                                out userPwd,
                                out pwdPolicyCN);//,
                            //out userName);
                            //Store the user DN in user VO object
                            LoggedInUserSecurityProfile.UserDN = userIdKey + "=" + username + "," + searchDN;
                            if (pwdHistory != null)
                            {
                                for (int i = 0; i < pwdHistory.Length; i++)
                                    LoggedInUserSecurityProfile.UserPasswordHistory.Add(pwdHistory[i]);
                            }

                            // Display actual name in desktop display
                            if (!string.IsNullOrEmpty(userDisplayName))
                                this.DisplayName = userDisplayName;

                            if (!string.IsNullOrEmpty(username))
                            {
                                this.FullUserName = username;
                                var usrNameLen = username.Length;
                                if (usrNameLen <= USERNAME_MAXLEN)
                                {
                                    this.UserName = username;
                                }
                                else
                                {
                                    //Get last 5 chars of the string (subtract 5 + 1 from length for index val)
                                    var last5Chars = username.Substring(usrNameLen - USERNAME_MAXLEN, USERNAME_MAXLEN);
                                    this.UserName = !string.IsNullOrEmpty(last5Chars) ? last5Chars : "USER";
                                }
                            }
                            else
                            {
                                return (false);
                            }
                            this.userState = UserDesktopState.LOGGEDIN;
                        }
                        else
                        {
                            return (false);
                        }
                    }
                    else if (dR == DialogResult.Cancel)
                    {
                        //Skipped out of initial authentication dialog, need to
                        //return false to avoid unnecessary "invalid credentials" message
                        LoginCancel = true;
                        return (false);
                    }
                }
                catch (Exception eX)
                {
                    BasicExceptionHandler.Instance.AddException("Exception thrown in PerformLDAPAuthentication" + eX.Message, new ApplicationException("PerformLDAPAuthentication Exception", eX));
                }
            }
            else if (this.userState == UserDesktopState.NOTLOGGEDIN && pawnLDAPAccessor.State == PawnLDAPAccessor.LDAPState.DISCONNECTED)
                return false;

            //Went through a full process
            if (needToChangePassword)
                return false;
            return (true);
        }

        /*public void resetOracleDA()
        {
            lock (GlobalDataAccessor.Instance.OracleDA)
            {
                GlobalDataAccessor.Instance.OracleDA = null;
                //Get client config for DB connection
                var confRef = SecurityAccessor.Instance.EncryptConfig;
                var clientConfigDB = confRef.GetOracleDBService();

#if __MULTI__            
                this.oracleDA = new OracleDataAccessor(
                confRef.DecryptValue(clientConfigDB.DbUser),
                confRef.DecryptValue(clientConfigDB.DbUserPwd),
                confRef.DecryptValue(clientConfigDB.Server),
                confRef.DecryptValue(clientConfigDB.Port),
                confRef.DecryptValue(clientConfigDB.AuxInfo),
                confRef.DecryptValue(clientConfigDB.Schema),
                (uint)confRef.ClientConfig.StoreConfiguration.FetchSizeMultiplier,
                true,
                false,
                null);
#else
                GlobalDataAccessor.Instance.OracleDA = new OracleDataAccessor(
                    confRef.DecryptValue(clientConfigDB.DbUser),
                    confRef.DecryptValue(clientConfigDB.DbUserPwd),
                    confRef.DecryptValue(clientConfigDB.Server),
                    confRef.DecryptValue(clientConfigDB.Port),
                    confRef.DecryptValue(clientConfigDB.AuxInfo),
                    confRef.DecryptValue(clientConfigDB.Schema),
                    (uint)confRef.ClientConfig.StoreConfiguration.FetchSizeMultiplier,
                    false,
                    false,
                    null);
#endif
            }
        }*/

        /// <summary>
        /// 
        /// </summary>
        public static void SpawnCashlinxPDA()
        {
            if (string.IsNullOrEmpty(GlobalDataAccessor.Instance.CashlinxPDAURL))
            {
                var errMsg = string.Format("Cashlinx PDA URL is invalid.  Cannot access Cashlinx PDA from Cashlinx Pawn");
                if (FileLogger.Instance != null && FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "CashlinxDesktopSession", errMsg);
                }
                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                MessageBox.Show(errMsg, "Cashlinx Pawn Error");
                return;
            }
            var iePath = new FileInfo(@"c:\Program Files\Internet Explorer\iexplore.exe");
            if (iePath.Exists)
            {
                if (FileLogger.Instance != null && FileLogger.Instance.IsLogInfo)
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, "CashlinxDesktopSession", "Opening URL = " + GlobalDataAccessor.Instance.CashlinxPDAURL);
                }

                //Construct the login parameter string - encrypt with triple DES
                string msg =
                    GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName +
                    SecurityAccessor.SEP +
                    GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserCurrentPassword;
                DateTime now = DateTime.Now;
                string month = now.Month.ToString().PadLeft(2, '0');
                string dayOfMonth = now.Day.ToString().PadLeft(2, '0');
                string currentYear = now.Year.ToString();
                string md5Msg =
                    StringUtilities.GenerateMD5Hash(string.Format("{0}-{1}-{2}", month, dayOfMonth, currentYear));
                var ieProcess = Process.GetProcessesByName("iexplore");
                if (ieProcess.Length > 0)
                {
                    if (FileLogger.Instance != null && FileLogger.Instance.IsLogWarn)
                    {
                        FileLogger.Instance.logMessage(LogLevel.WARN, "CashlinxDesktopSession",
                                                       "Killing active IE processes.  Total number to kill = {0}", ieProcess.Length);
                    }
                    var idx = 0;
                    foreach (var procIe in ieProcess)
                    {
                        if (FileLogger.Instance != null && FileLogger.Instance.IsLogInfo)
                        {
                            FileLogger.Instance.logMessage(LogLevel.INFO, "CashlinxDesktopSession", "Attempting to kill IE process {0}", idx);
                        }
                        try
                        {
                            if (procIe != null)
                            {
                                procIe.Kill();
                            }
                            if (FileLogger.Instance != null && FileLogger.Instance.IsLogInfo)
                            {
                                FileLogger.Instance.logMessage(LogLevel.WARN, "CashlinxDesktopSession", "Killed IE process {0}", idx);
                            }
                            ++idx;
                        }
                        catch (Exception eX)
                        {
                            var errMsg = string.Format("Could not kill IE process {0} : Exception thrown: {1} {2}", idx, eX,
                                                               eX.StackTrace ?? "No Stack Trace");
                            if (FileLogger.Instance != null && FileLogger.Instance.IsLogError)
                            {
                                FileLogger.Instance.logMessage(LogLevel.ERROR, "CashlinxDesktopSession", errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));
                            MessageBox.Show("Please close all Internet Explorer browsers before attempting to access Cashlinx PDA",
                                            "Cashlinx Pawn Error");
                            return;
                        }
                    }
                }
                Process procHandle = new Process();
                //procHandle = ieProcess.Count() > 0 ? ieProcess[0] : new Process();
                var machineName =
                        SecurityAccessor.Instance.EncryptConfig.ClientConfig.MachineName;
                /*var ipAddress =
                         SecurityAccessor.Instance.HostInfo.IPAddress;*/
                //Fix for IP Address issue on shop machine
                var ipAddress = getIPAddress();
                if (FileLogger.Instance != null && FileLogger.Instance.IsLogDebug)
                {
                    FileLogger.Instance.logMessage(LogLevel.DEBUG,
                                                   "CashlinxDesktopSession",
                                                   "Auth ID is " + msg + " Machine Name is " + machineName + " IPAddress is " + ipAddress);
                }

                var encUrlMsg = HttpUtility.UrlEncode(
                    md5Msg +
                    StringUtilities.ConverByteArrayToHexString(Encoding.Default.GetBytes(msg)) +
                    md5Msg);

                var encMacName = HttpUtility.UrlEncode(
                    md5Msg +
                    StringUtilities.ConverByteArrayToHexString(Encoding.Default.GetBytes(machineName)) +
                    md5Msg);

                var encIpAddress = HttpUtility.UrlEncode(
                    md5Msg +
                    StringUtilities.ConverByteArrayToHexString(Encoding.Default.GetBytes(ipAddress)) +
                    md5Msg);

                procHandle.StartInfo.FileName = iePath.FullName;
                procHandle.StartInfo.CreateNoWindow = true;
                procHandle.StartInfo.Arguments =
                    string.Format("\"{0}&AUTHID={1}&MACNAME={2}&IPADDR={3}&EMSG={4}\"", GlobalDataAccessor.Instance.CashlinxPDAURL, encUrlMsg, encMacName, encIpAddress, md5Msg);


                if (FileLogger.Instance != null && FileLogger.Instance.IsLogDebug)
                {
                    FileLogger.Instance.logMessage(LogLevel.DEBUG,
                                                   "CashlinxDesktopSession",
                                                   "Sending these arguments to iexplore: {0}",
                                                   procHandle.StartInfo.Arguments);
                }
                //procHandle.StartInfo.Arguments = "http://asset3128/Advisor/servlet/RequestHandler?ActionID=Login&Chrome=False";
                procHandle.StartInfo.WindowStyle = ProcessWindowStyle.Normal;

                //Must wait for exit
                procHandle.Start();
                procHandle.WaitForExit();
                procHandle.Dispose();

            }
            else
            {
                MessageBox.Show("Cannot find Internet Explorer.  Please install to allow for proper Cashlinx Pawn <=> PDA integration");
                FileLogger.Instance.logMessage(LogLevel.ERROR,
                                               "CashlinxDesktopSession",
                                               "Internet Explorer executable could not be found!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /*        public bool beginTransactionBlock()
                {
                    if (GlobalDataAccessor.Instance.OracleDA == null || GlobalDataAccessor.Instance.OracleDA.Initialized != true)
                    {
                        var errMsg = string.Format("Oracle data accessor is invalid or not initialized!");
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        return(false);
                    }
                    if (!GlobalDataAccessor.Instance.OracleDA.StartTransactionBlock(IsolationLevel.ReadCommitted, null))
                    {
                        var errMsg = string.Format("Cannot start a database transaction block");
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }                
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        return (false);
                    }
                    //If we get here, the transaction block was successfully started
                    return (true);
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="eType"></param>
                public bool endTransactionBlock(EndTransactionType eType)
                {
                    if (GlobalDataAccessor.Instance.OracleDA == null || GlobalDataAccessor.Instance.OracleDA.Initialized != true)
                    {
                        string errMsg = string.Format("Cannot end transaction block with a {0} - database accessor is invalid or not initialized", eType);
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        return (false);
                    }
                    switch (eType)
                    {
                        case EndTransactionType.COMMIT:
                            if (!GlobalDataAccessor.Instance.OracleDA.commitTransactionBlock(null))
                            {
                                string errMsg = string.Format("Cannot end transaction block - database commit operation failed");
                                if (FileLogger.Instance.IsLogFatal)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                }
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                return (false);
                            }
                            break;
                        case EndTransactionType.ROLLBACK:
                            if (!GlobalDataAccessor.Instance.OracleDA.rollbackTransactionBlock(null))
                            {
                                string errMsg = string.Format("Cannot end transaction block - database rollback operation failed");
                                if (FileLogger.Instance.IsLogFatal)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                }
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                return (false);
                            }
                            break;
                    }

                    return (true);
                }*/

        public override void DeviceArrivedEventHandler(object sender, USBUtilities.DriveDetectorEventArgs e)
        {
            var pMesg = new ProcessingMessage("* READING USB KEY DEVICE *");
            pMesg.Show();
            lock (usbDriveMutex)
            {
                if (this.UsbDriveStorage.IsValid)
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "USB Drive already in use");
                    }
                }
                else
                {
                    //Set that usb key is here
                    try
                    {
                        if (FileLogger.Instance.IsLogDebug)
                        {
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "USB disk drive arrived");
                        }
                        if (sender == null || e == null)
                        {
                            throw new Exception("Sender and/or event arguments are invalid");
                        }

                        //Retrieve and validate the drive serial number
                        var usbSerNumber = USBUtilities.GetVolumeSerial(e.Drive.Substring(0, 1));
                        if (string.IsNullOrEmpty(usbSerNumber))
                        {
                            throw new Exception("USB Serial number is invalid");
                        }

                        if (FileLogger.Instance.IsLogDebug)
                        {
                            FileLogger.Instance.logMessage(LogLevel.DEBUG,
                                                           this,
                                                           "USB disk serial number = {0}",
                                                           usbSerNumber);
                        }

                        //Perform file info check
                        var usbData = string.Empty;
                        Encoding usbDataEnc = null;
                        if (File.Exists(e.Drive + USB_SECURE_FILENAME))
                        {
                            if (FileLogger.Instance.IsLogInfo)
                            {
                                FileLogger.Instance.logMessage(LogLevel.INFO,
                                                               this,
                                                               "Found secure file name at {0}. Reading now...",
                                                               e.Drive + USB_SECURE_FILENAME);
                            }
                            //Point a stream reader at the usb file
                            var usbReader = new StreamReader(e.Drive + USB_SECURE_FILENAME);
                            usbDataEnc = usbReader.CurrentEncoding;
                            if (usbReader != StreamReader.Null &&
                                usbReader.BaseStream != Stream.Null &&
                                usbReader.BaseStream.CanRead)
                            {
                                //Consume the entire file
                                usbData = usbReader.ReadToEnd();

                                //Close the reader
                                usbReader.Close();

                                //Validate the data
                                if (string.IsNullOrEmpty(usbData))
                                {
                                    throw new Exception("USB Data is invalid");
                                }
                                if (FileLogger.Instance.IsLogDebug)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.DEBUG,
                                                                   this,
                                                                   "Read usb data, length = " + usbData.Length);
                                }
                            }
                        }

                        //Set usb storage
                        if (string.IsNullOrEmpty(usbData) || usbDataEnc == null)
                        {
                            this.UsbDriveStorage.Populate(null, usbSerNumber, e.Drive);
                        }
                        else
                        {
                            this.UsbDriveStorage.Populate(usbData, usbSerNumber, e.Drive);
                        }
                    }
                    catch (Exception eX)
                    {
                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR,
                                                           this,
                                                           "Exception occurred while processing device arrived event: {0}, {1}, {2}",
                                                           eX.Message,
                                                           eX.Data,
                                                           eX);
                        }
                        this.UsbDriveStorage.Obliterate();
                    }
                }
            }
            pMesg.Close();
            pMesg.Dispose();
        }

        public override void DeviceRemovedEventHandler(object sender, USBUtilities.DriveDetectorEventArgs e)
        {
            var pMesg = new ProcessingMessage("* REMOVING USB KEY DEVICE *");
            pMesg.Show();
            //Set that usb key is removed
            lock (usbDriveMutex)
            {
                if (sender == null || e == null)
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Device remove event data is null");
                    }
                }

                if (FileLogger.Instance.IsLogInfo)
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO,
                                                   this,
                                                   "USB drive with serial number {0} at path: {1} was removed",
                                                   UsbDriveStorage.DriveSerialNumber,
                                                   UsbDriveStorage.DrivePath);
                }

                this.UsbDriveStorage.Obliterate();
            }
            pMesg.Close();
            pMesg.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool WriteUsbData(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return (false);
            }
            var rt = false;
            lock (usbDriveMutex)
            {
                if (this.UsbDriveStorage.IsValid ||
                    (!string.IsNullOrEmpty(this.UsbDriveStorage.DrivePath) &&
                     !string.IsNullOrEmpty(this.UsbDriveStorage.DriveSerialNumber)))
                {
                    var usbWriter = new StreamWriter(this.UsbDriveStorage.DrivePath + USB_SECURE_FILENAME, false);
                    if (usbWriter != StreamWriter.Null)
                    {
                        usbWriter.Write(data);
                        usbWriter.Close();

                        //Update the usb storage
                        this.UsbDriveStorage.PopulateNewData(data);
                        rt = true;
                    }
                }
            }
            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Item"> </param>
        /// <param name="context"> </param>
        public override void PrintTags(Item _Item, CurrentContext context)
        {
            if (Utilities.GetIntegerValue(_Item.PfiTags) > 0 && _Item.PfiAssignmentType != PfiAssignment.Refurb)
            {
                bool bPrintTag = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled;
                if (bPrintTag && GlobalDataAccessor.Instance.DesktopSession.BarcodePrinter.IsValid)
                {
                    var sPrinterModel = IntermecBarcodeTagPrint.PrinterModel.Intermec_PM4i;

                    var intermecBarcodeTagPrint =
                    new IntermecBarcodeTagPrint(SecurityAccessor.Instance.EncryptConfig.ClientConfig.StoreConfiguration.CompanyName,
                                                Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber),
                                                sPrinterModel,
                                                GlobalDataAccessor.Instance.DesktopSession.BarcodePrinter.IPAddress,
                                                (uint)GlobalDataAccessor.Instance.DesktopSession.BarcodePrinter.Port, GlobalDataAccessor.Instance.DesktopSession);

                    string sErrorCode;
                    string sErrorText = "Success";
                    intermecBarcodeTagPrint.PrintTag(ShopDateTime.Instance.ShopDate,
                                                     _Item,
                                                     false,
                                                     false,
                                                     true,
                                                     true,
                                                     false,
                                                     IntermecBarcodeTagPrint.TagMedias.COMBO,
                                                     context,
                                                     out sErrorCode,
                                                     out sErrorText);
                    if (string.IsNullOrEmpty(sErrorText))
                        sErrorText = "Tag print Success";
                    fileLogger.logMessage(LogLevel.INFO, this, sErrorText);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Item"></param>
        /// <param name="reprint"></param>
        public override void PrintTags(Item _Item, bool reprint)
        {
            if (Utilities.GetIntegerValue(_Item.PfiTags) > 0 && _Item.PfiAssignmentType != PfiAssignment.Refurb)
            {
                bool bPrintTag = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled;
                if (bPrintTag && BarcodePrinter.IsValid)
                {
                    var sPrinterModel = IntermecBarcodeTagPrint.PrinterModel.Intermec_PM4i;

                    var intermecBarcodeTagPrint =
                    new IntermecBarcodeTagPrint(SecurityAccessor.Instance.EncryptConfig.ClientConfig.StoreConfiguration.CompanyName,
                                                Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber),
                                                sPrinterModel,
                                                BarcodePrinter.IPAddress,
                                                (uint)BarcodePrinter.Port, GlobalDataAccessor.Instance.DesktopSession);

                    string sErrorCode;
                    string sErrorText = "Success";
                    intermecBarcodeTagPrint.PrintTag(ShopDateTime.Instance.ShopDate,
                                                     _Item,
                                                     false,
                                                     false,
                                                     true,
                                                     true,
                                                     false,
                                                     IntermecBarcodeTagPrint.TagMedias.COMBO,
                                                     CurrentContext.READ_ONLY,
                                                     out sErrorCode,
                                                     out sErrorText);
                    if (string.IsNullOrEmpty(sErrorText))
                        sErrorText = "Tag print Success";
                    fileLogger.logMessage(LogLevel.INFO, this, sErrorText);
                    if (reprint)
                    {
                        string sReprintErrorCode;
                        string sReprintErrorText = "Success";
                        intermecBarcodeTagPrint.ReprintTag(_Item);
                        if (string.IsNullOrEmpty(sReprintErrorText))
                            sReprintErrorText = "Tag Reprint Success";
                        fileLogger.logMessage(LogLevel.INFO, this, sReprintErrorText);
                    }

                }

            }
        }

        //Clear any session variables declared during a course of a transaction not necessarily
        //those that belong to pawn loan or purchase or customer
        //which are targeted by different calls
        public override void ClearSessionData()
        {
            Instance.DescribeItemContext = CurrentContext.NEW;
            Instance.SelectedStoreCashTransferNumber = 0;
            Instance.CashTransferData = null;
            Instance.LockProductsTab = false;
            Instance.ReleaseToClaimant = false;
            Instance.PoliceInformation = null;
            Instance.MdseTransferData = null;
            Instance.SelectedTransferNumber = string.Empty;
            Instance.GenerateTemporaryICN = false;
            if (instance.ActiveItemSearchData == null)
            {
                instance.ActiveItemSearchData = new ItemSearchCriteria();
            }
            instance.ActiveItemSearchData.CategoryID = 0;
            instance.ActiveItemSearchData.CategoryDescription = string.Empty;
            instance.ActiveItemSearchData.Manufacturer = string.Empty;
            instance.ActiveItemSearchData.Model = string.Empty;
            instance.TenderTransactionAmount.TotalAmount = 0.0M;
            instance.BuyReturnIcn = false;
            instance.DisableCoupon = false;
            Instance.ShowOnlyHistoryTabs = false;
            Instance.LockProductsTab = false;
            Instance.ClearPawnLoan();
            Instance.ActiveLookupCriteria = new LookupCustomerSearchData();
            instance.StartNewPawnLoan = false;
            instance.UpdateRequiredFieldsForCustomer = false;
            instance.TabStateClicked = FlowTabController.State.None;
            instance.CashSaleCustomer = false;
            instance.ShopCreditFlow = false;
            instance.ReceiptToRefund = string.Empty;
            instance.CompleteLayaway = false;
            instance.CompleteSale = false;
            instance.ManagerOverrideBuyLimit = false;
            instance.ManagerOverrideLoanLimit = false;
            instance.ServicePawnLoans = false;
            instance.PurchasePFIAddItem = false;
            instance.BalanceOtherCashDrawerID = string.Empty;
            instance.BalanceOtherCashDrawerName = string.Empty;
            instance.BalanceOtherBegBalance = 0;
            instance.TrialBalance = false;
            instance.ClosedUnverifiedCashDrawer = false;
            instance.ClosedUnverifiedSafe = false;
            instance.BackgroundCheckFeeValue = 0;
            instance.ScannedCredentials = false;
            instance.TotalServiceAmount = 0;
            instance.TotalRolloverAmount = 0;
            instance.TotalExtendAmount = 0;
            instance.TotalPaydownAmount = 0;
            instance.TotalPickupAmount = 0;
            instance.TotalRenewalAmount = 0;
            instance.CustomerValidated = false;

        }

        //Breaks scanned in text into user name and password.  Scanned text must meet
        //minimum size requirements before the decryption attempt is made.
        public void ScanParseInstance(Object sender, string sBarCodeData, TextBoxBase userNameControl, TextBoxBase passwordControl, ButtonBase submitBtn)
        {
            ScanParse(sender, sBarCodeData, userNameControl, passwordControl, submitBtn);
        }

        public override void ScanParse(Object sender, string sBarCodeData, TextBoxBase userNameControl, TextBoxBase passwordControl, ButtonBase submitBtn)
        {
            const int BARCODE_BYTE_BOUNDARY = 8;
            Instance.ScannedCredentials = false;
            if (string.IsNullOrEmpty(sBarCodeData) || (sBarCodeData.Length % BARCODE_BYTE_BOUNDARY) > 0)
                return;
            try
            {
                //Decrypt the barcode scan
                string decryptedStr =
                SecurityAccessor.Instance.EncryptConfig.DecryptValue(sBarCodeData);
                if (string.IsNullOrEmpty(decryptedStr))
                    return;
                //Ensure that it has a separator
                int sepIdx = decryptedStr.IndexOf(SecurityAccessor.SEP, System.StringComparison.Ordinal);
                if (sepIdx < 0)
                    return;
                Instance.ScannedCredentials = true;
                userNameControl.Text = string.Empty;
                passwordControl.Text = string.Empty;
                userNameControl.Text = decryptedStr.Substring(0, sepIdx);
                passwordControl.Text = decryptedStr.Substring(sepIdx + 1);
                submitBtn.Focus();



            }
            catch (Exception eX)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, sender, "Could not parse bar code data: {0} Error = {1}", sBarCodeData, eX);
            }
        }

        /// <summary>
        /// Checks whether the button name coming in as input exists in the available
        /// buttons in the current store object
        /// </summary>
        /// <param name="buttonTagName"></param>
        /// <param name="currentSiteId"></param>
        /// <returns></returns>
        public override bool IsButtonAvailable(string buttonTagName, SiteId currentSiteId)
        {
            if (currentSiteId == null || string.IsNullOrEmpty(buttonTagName))
            {
                return (false);
            }
            if (currentSiteId.AvailableButtons != null && currentSiteId.AvailableButtons.Count > 0)
            {
                try
                {
                    var btnName = (from buttonTag in currentSiteId.AvailableButtons
                                   where buttonTag.ToLower() == buttonTagName.ToLower()
                                   select buttonTag).First();
                    if (btnName != null)
                        return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public override bool IsButtonTellerOperation(string buttonTagName)
        {
            if (string.IsNullOrEmpty(buttonTagName))
            {
                return (false);
            }
            if (instance.TellerOperations != null)
            {
                try
                {
                    var btnName = (from buttonTag in instance.TellerOperations
                                   where buttonTag.ToLower() == buttonTagName.ToLower()
                                   select buttonTag).First();
                    if (btnName != null)
                        return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        private static string getIPAddress()
        {
            string ipval = string.Empty;
            try
            {
                NetworkInterface[] networks = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface network in networks)
                {
                    // Checked for Ethernet IP
                    if (network.NetworkInterfaceType.ToString().Contains("Ethernet"))
                    {
                        foreach (UnicastIPAddressInformation entry in network.GetIPProperties().UnicastAddresses)
                        {
                            ipval = entry.Address.ToString();
                            //Console.WriteLine(ipval);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, "Error in getting IP: ", " Cashlinx PDA Launch will abort", ex);
            }
            return ipval;
        }

        public override void Dispose()
        {

        }

    }
}
