using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Audit.Flows.Impl;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow;
using Common.Controllers.Database;
using Common.Controllers.Database.Couch.Impl;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Network;
using Common.Controllers.Rules.Interface;
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
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;

namespace Audit.Logic
{
    public sealed class AuditDesktopSession : DesktopSession
    {
        # region Constants

        public const string USB_SECURE_FILENAME = "PzIISecTrx.XFX";
        public const int USERNAME_MAXLEN = 5;

        # endregion

        # region Singleton Code

        private static AuditDesktopSession auditDesktopSession;
        private static readonly object padlock = new object();

        public static AuditDesktopSession Instance
        {
            get
            {
                lock (padlock)
                {
                    if (auditDesktopSession == null)
                    {
                        auditDesktopSession = new AuditDesktopSession();
                    }
                    return auditDesktopSession;
                }
            }
        }

        static AuditDesktopSession()
        {
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        # endregion        

        # region Constructors

        public AuditDesktopSession()
        {
            PawnSecApplication = PawnSecApplication.Audit;
            ResourceProperties = new ResourceProperties();
            ButtonResourceManagerHelper = new ButtonResourceManagerHelper();
            var confRef = SecurityAccessor.Instance.EncryptConfig;
            ShopCalendar = new List<ShopCalendarVO>(7);
            HolidayCalendar = new List<ShopCalendarVO>(7);
            this.userState = UserDesktopState.NOTLOGGEDIN;
            this.UserName = string.Empty;
            this.CashDrawerId =
            (confRef == null ||
             confRef.ClientConfig == null ||
             confRef.ClientConfig.ClientConfiguration == null) ?
             "0" : confRef.ClientConfig.ClientConfiguration.TerminalNumber.ToString();
            this.IsSkipLDAP = false;
            this.initializeLogger();
            ActiveUserData = new CurrentLoggedInUserData();
            GenerateTemporaryICN = false;

            if (confRef == null)
            {
                return;
            }
            //Get client config for DB connection            
            var clientConfigDB = confRef.GetOracleDBService();

#if __MULTI__            
            GlobalDataAccessor.Instance.OracleDA = new OracleDataAccessor(
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
            if (!GlobalDataAccessor.Instance.OracleDA.Initialized)
            {
                throw new ApplicationException("Oracle data accessor is not initialized.  Cannot interact with the database. Exiting!");
            }

            //Retrieve database time
            DateTime time;
            ShopProcedures.ExecuteGetDatabaseTime(GlobalDataAccessor.Instance.OracleDA, out time);
            DatabaseTime = time;
            //Setup exception handler
            this.exHandler = BasicExceptionHandler.Instance;
            this.exHandler.PrintStackTrace = true;
            this.exHandler.setExceptionCallback(this.exceptionCallbackMethod);

            //Set shop date time
            var storeConf = confRef.ClientConfig.StoreConfiguration;
            ShopDateTime.Instance.setOffsets(0, 0, 0, 0, 0, 0, 0);
            ShopDateTime.Instance.SetDatabaseTime(DatabaseTime);
            ShopDateTime.Instance.SetPawnSecOffsetTime(storeConf);

            //Initialize audit logger);
            this.initializeAuditLogger();

            this.mainFlowExecutor = new MainFlowExecutor(this);

            //Create the application controller
            this.AppController = new AppWorkflowController(this, mainFlowExecutor);

            //Create pawn auxilliary container
            //this.PawnLoans_Auxillary = new List<PawnLoan>();

            //Initialize the site
            this.CurrentSiteId = new SiteId();
            this.CurrentSiteId.StoreNumber = confRef.ClientConfig.StoreSite.StoreNumber;

            //Load store information            
            var site = CurrentSiteId;
            LoadStoreData(GlobalDataAccessor.Instance.OracleDA, CurrentSiteId.StoreNumber, ref site);

            //Finalize site info population
            this.CurrentSiteId.TerminalId = confRef.ClientConfig.ClientConfiguration.WorkstationId;
            this.CurrentSiteId.Alias = confRef.ClientConfig.StoreSite.Alias;
            this.CurrentSiteId.Company = confRef.ClientConfig.StoreSite.CompanyNumber;
            this.CurrentSiteId.CompanyNumber = confRef.ClientConfig.StoreSite.CompanyNumber;
            this.CurrentSiteId.Date = ShopDateTime.Instance.ShopDate;
            this.CurrentSiteId.State = confRef.ClientConfig.StoreSite.State;
            this.CurrentSiteId.LoanAmount = 0.00M;

            //Set into global accessor
            GlobalDataAccessor.Instance.CurrentSiteId = CurrentSiteId;

            string errorCode;
            string errorText;
            //Load the button tags that are available for the store
            List<ButtonTags> buttonTagNames;
            var retVal = ShopProcedures.GetButtonTagNames(GlobalDataAccessor.Instance.OracleDA,
                                                           confRef.ClientConfig.StoreSite.StoreNumber, out buttonTagNames,
                                                           out errorCode, out errorText);
            //if (retVal && buttonTagNames != null && buttonTagNames.Count > 0)
            //{
            //    CurrentSiteId.AvailableButtons = (from tagName in buttonTagNames select tagName.TagName).ToList();
            //    TellerOperations = (from tagName in buttonTagNames where tagName.TellerOperation select StringUtilities.removeFromString(tagName.TagName.ToLowerInvariant(), "button")).ToList();
            //}

            //Load the sales tax info for the store
            List<StoreTaxVO> storeTaxes;
            //var retval = RetailProcedures.GetStoreTaxInfo(GlobalDataAccessor.Instance.OracleDA,
            //                                               confRef.ClientConfig.StoreSite.StoreNumber,
            //                                               out storeTaxes, out errorCode, out errorText);
            //if (retval && storeTaxes != null && storeTaxes.Count > 0)
            //{
            //    CurrentSiteId.StoreTaxes = storeTaxes;
            //}

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
                ShopCalendar = calendarDays;
                HolidayCalendar = holidayDays;
            }

            //Initialize couch db connector
            var clientDocDb = confRef.GetCouchDBService();
            if (clientDocDb != null)
            {
                GlobalDataAccessor.Instance.CouchDBConnector = new SecuredCouchConnector(
                    confRef.DecryptValue(clientDocDb.Server),
                    confRef.DecryptValue(clientDocDb.Port),
                    SSL_PORT,
                    confRef.DecryptValue(clientDocDb.Schema),
                    confRef.DecryptValue(clientDocDb.DbUser),
                    confRef.DecryptValue(clientDocDb.DbUserPwd),
                    SECURE_COUCH_CONN);
            }
            else
            {
                throw new SystemException("Cannot find Couch DB connection information! Exiting application!");
            }

            //Initialize usb storage instance
            this.CurrentUSBStorage = new USBUtilities.USBDriveStorage();

            //Retrieve FTP
            var ftpObj = confRef.GetFTP();
            if (ftpObj != null)
            {
                this.FtpUser = confRef.DecryptValue(ftpObj.DbUser);
                this.FtpPassword = confRef.DecryptValue(ftpObj.DbUserPwd);
                this.FtpHost = confRef.DecryptValue(ftpObj.Server);
            }
            else
            {
                throw new SystemException("Cannot find FTP connection information! Exiting application!");
            }

            //Init force close timer to null
            //this.forceCloseTimer = null;

            //Initialize empty printer entries
            this.PDALaserPrinter = new StorePrinterVO(StorePrinterVO.StorePrinterType.LASER, string.Empty, 0);
            this.LaserPrinter = new StorePrinterVO(StorePrinterVO.StorePrinterType.LASER, string.Empty, 0);
            this.ReceiptPrinter = new StorePrinterVO(StorePrinterVO.StorePrinterType.RECEIPT, string.Empty, 0);
            this.BarcodePrinter = new StorePrinterVO(StorePrinterVO.StorePrinterType.BARCODE, string.Empty, 0);
            LoginCancel = false;
        }

        # endregion

        # region Properties
        
        public List<InventoryQuestion> InventoryQuestions { get; set; }
        public List<InventoryQuestion> InventoryQuestionsWithResponses { get; set; }
        public List<AuditReportsObject.InventorySummaryChargeOffsField> InventorySummaryReportFieldsCACC { get; set; }
        public StringBuilder InventoryQuestionsAdditionalComments { get; set; }
        public InventoryAuditVO ActiveAudit { get; set; }
        public InventoryAuditVO ReportActiveAudit { get; set; }
        public string FtpHost { get; set; }
        public string FtpPassword { get; set; }
        public string FtpUser { get; set; }
        public bool RefreshAuditList { get; set; }
        public TrakkerItem SelectedTrakkerItem { get; set; }

        # endregion

        # region Member Variables

        private AuditLogger auditLogger;
        private BasicExceptionHandler exHandler;
        private FileLogger fileLogger;
        private MainFlowExecutor mainFlowExecutor;
        # endregion

        # region Public Methods

        public override void ClearLoggedInUser()
        {
            this.LoggedInUserSecurityProfile = null;
            this.UserName = string.Empty;
            //UpdateDesktopUserName(this.DesktopForm);
        }

        public override void DeviceArrivedEventHandler(object sender, USBUtilities.DriveDetectorEventArgs e)
        {
            var pMesg = new ProcessingMessage("* READING USB KEY DEVICE *");
            pMesg.Show();
            lock (usbDriveMutex)
            {
                if (CurrentUSBStorage.IsValid)
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
                            CurrentUSBStorage.Populate(null, usbSerNumber, e.Drive);
                        }
                        else
                        {
                            CurrentUSBStorage.Populate(usbData, usbSerNumber, e.Drive);
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
                        CurrentUSBStorage.Obliterate();
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
                                                   CurrentUSBStorage.DriveSerialNumber,
                                                   CurrentUSBStorage.DrivePath);
                }

                CurrentUSBStorage.Obliterate();
            }
            pMesg.Close();
            pMesg.Dispose();
        }
        
        public override bool IsButtonAvailable(string buttonTagName, SiteId currentSiteId)
        {
            return true;
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
                    AuditDesktopSession.Instance.CurrentSiteId.StoreNumber, this, out safeCashdrawerName,
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
                    DataRow[] safeCdRow = CashDrawerAssignments.Select("NAME='" + StoreSafeName + "'");
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
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("GetCashDrawerAssignmentsFailed", new ApplicationException("Cannot execute GetCashDrawerAssignments during StartUp. [" + ex.Message + "]"));
            }
        }

        public override void PerformAuthorization()
        {
            PerformAuthorization(false);
        }

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
            if (!IsSkipLDAP)
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
                            System.Environment.NewLine, ldapService);
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
                    var outVal = 0;
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
                            DialogResult pwdResult = uPwdForm.ShowDialog();

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
                                    if (AuditDesktopSession.Instance.BarcodePrinter.IsValid)
                                    {
                                        var intermecBarcodeTagPrint = new IntermecBarcodeTagPrint("",
                                                Convert.ToInt32(AuditDesktopSession.Instance.CurrentSiteId.StoreNumber),
                                                IntermecBarcodeTagPrint.PrinterModel.Intermec_PM4i,
                                                AuditDesktopSession.Instance.BarcodePrinter.IPAddress,
                                                (uint)AuditDesktopSession.Instance.BarcodePrinter.Port, this);

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
                            "You have entered invalid credentials. " +
                            "This is your " + (attemptCount.FormatNumberWithSuffix()) +
                            " attempt. " +
                            "Would you like to retry?",
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
                //UpdateDesktopUserName(this.DesktopForm);
                //Get role information
                //The logged in user's security profile will be stored in LoggedInUserSecurityProfile object after the call
                string errorCode;
                string errorMesg;
                LoggedInUserSafeAccess = false;
                if (!SecurityProfileProcedures.GetUserSecurityProfile(FullUserName, "", CurrentSiteId.StoreNumber, "N", this,
                                                                      out errorCode, out errorMesg))
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

                    if (!SecurityProfileProcedures.CanUserViewResource("AuditAppAccess", this.LoggedInUserSecurityProfile, this))
                    {
                        BasicExceptionHandler.Instance.AddException(
                        "This user does not have access to view the Audit Application",
                        new ApplicationException());
                        MessageBox.Show(
                            "This user does not have access to view the Audit Application.  Exiting the application.");
                        Application.Exit();
                        return;
                    }

                    //UpdateDesktopUserName(this.DesktopForm);
                    //Get cash drawer assignments
                    //Check if the logged in user is a manager and has shop cash access
                    if (SecurityProfileProcedures.CanUserViewResource(
                        "SHOPADMINMENUCONTROL", this.LoggedInUserSecurityProfile, this))
                        CurrentAppMode = AppMode.MANAGER;
                    else
                        CurrentAppMode = AppMode.CSR;
                    if (SecurityProfileProcedures.CanUserModifyResource(
                        "SAFEMANAGEMENT", this.LoggedInUserSecurityProfile, this))
                        LoggedInUserSafeAccess = true;
                    else
                        LoggedInUserSafeAccess = false;

                    //GetCashDrawerAssignmentsForStore();
                    //if (string.IsNullOrEmpty(StoreSafeID) || string.IsNullOrEmpty(StoreSafeName))
                    //{
                    //    MessageBox.Show("No safe assigned for the store. Exiting the application");
                    //    Application.Exit();
                    //}
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
                        }
                    }

                    if (pwdChanged)
                    {
                        if (!tagPrinted)
                        {
                            //Generate the encrypted string that goes on the barcode
                            string encStr = LoggedInUserSecurityProfile.UserName + SecurityAccessor.SEP + newPass;
                            var sEmployeeFirstName = LoggedInUserSecurityProfile.UserFirstName;
                            var sEncryptData =
                            SecurityAccessor.Instance.EncryptConfig.EncryptValue(encStr);
                            if (AuditDesktopSession.Instance.BarcodePrinter.IsValid)
                            {
                                //Acquire intermec printer interface
                                var intermecBarcodeTagPrint =
                                    new IntermecBarcodeTagPrint("",
                                        Convert.ToInt32(AuditDesktopSession.Instance.CurrentSiteId.StoreNumber),
                                        IntermecBarcodeTagPrint.PrinterModel.Intermec_PM4i,
                                        AuditDesktopSession.Instance.BarcodePrinter.IPAddress,
                                        (uint)AuditDesktopSession.Instance.BarcodePrinter.Port, this);

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

                        if (SecurityAccessor.Instance.WriteUsbAuthentication(this, usrName, newPass))
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

        public void PopulateShopEmployees()
        {
            //Clear any existing shop employees
            this.AssignedEmployees.Clear();

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

        /// <summary>
        /// Breaks scanned in text into user name and password.  Scanned text must meet 
        /// minimum size requirements before the decryption attempt is made.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="sBarCodeData"></param>
        /// <param name="userNameControl"></param>
        /// <param name="passwordControl"></param>
        /// <param name="submitBtn"></param>
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
                int sepIdx = decryptedStr.IndexOf(SecurityAccessor.SEP);
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

        public override void Setup(Form deskForm)
        {
            ResourceProperties.vistabutton_blue = Properties.Resources.vistabutton_blue;
            ResourceProperties.newDialog_400_BlueScale = Properties.Resources.newDialog_400_BlueScale;
            ResourceProperties.newDialog_512_BlueScale = Properties.Resources.newDialog_512_BlueScale;

            ResourceProperties.newDialog_600_BlueScale = Properties.Resources.newDialog_600_BlueScale;
#if !__MULTI__
            procMsgFormPwd = new ProcessingMessage("* INITIALIZING APPLICATION *");
            procMsgFormPwd.Show();
            HistorySession = new HistoryTrack(deskForm);
            this.DesktopForm = deskForm;
            UpdateVersionLabel(this.DesktopForm);
            UpdateShopDate(this.DesktopForm);
#endif

            try
            {
                procMsgFormPwd.Message = "* RETRIEVING PRINTER INFORMATION *";
                GetReceiptPrinter();
                GetIntermecPrinter();
                GetLaserPrinter();
                GetPDALaserPrinter();
                procMsgFormPwd.Message = "* LOADING APPLICATION DATA *";
                GetPawnBusinessRules();
                PopulateCategoryXML();
                GetMerchandiseManufacturers();
                GetMetalStonesValues();
                GetVarianceRates();
                StartUpWebService();

                // Get scrap related information
                BusinessRuleVO scrapVO =
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

        public override bool WriteUsbData(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return (false);
            }
            var rt = false;
            lock (usbDriveMutex)
            {
                if (CurrentUSBStorage.IsValid ||
                    (!string.IsNullOrEmpty(CurrentUSBStorage.DrivePath) &&
                     !string.IsNullOrEmpty(CurrentUSBStorage.DriveSerialNumber)))
                {
                    var usbWriter = new StreamWriter(CurrentUSBStorage.DrivePath + USB_SECURE_FILENAME, false);
                    if (usbWriter != StreamWriter.Null)
                    {
                        usbWriter.Write(data);
                        usbWriter.Close();

                        //Update the usb storage
                        CurrentUSBStorage.PopulateNewData(data);
                        rt = true;
                    }
                }
            }
            return (rt);
        }

        public override void Dispose()
        {
        }

        # endregion

        # region Helper Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool exceptionCallbackMethod()
        {
#if !__MULTI__
            BasicExceptionHandler bEx = BasicExceptionHandler.Instance;
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
                    foreach (ApplicationException aEx in bEx.ApplicationExceptions)
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
                    foreach (SystemException aEx in bEx.SystemExceptions)
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
                    foreach (Exception aEx in bEx.BaseExceptions)
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
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot load manufacturers in AuditDesktopSession");
                    return;
                }
                MerchandiseManufacturers = dtMerchandiseManufacturers;
            }
            catch (Exception exp)
            {
                BasicExceptionHandler.Instance.AddException("GetMerchandiseManufacturersFailed", new ApplicationException("Cannot execute the GetMerchandiseManufacturers during StartUp. [" + exp.Message + "]"));
            }
        }

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

                List<PMetalInfo> pMetalData = PMetalData;
                List<StoneInfo> stonesData = StonesData;
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

        private void GetVarianceRates()
        {
            var varianceRate = new VarianceRate();
            varianceRate.Setup();
            VarianceRates = varianceRate.VarianceRates;
        }

        private void initializeAuditLogger()
        {
            this.auditLogger = AuditLogger.Instance;
            //this.auditLogger.SetAuditLogEnabledChangeHandler(auditLogEnabledChangeHandler);
            //this.auditLogger.SetAuditLogHandler(logAuditMessageHandler);
            //this.auditLogger.SetEnabled(string.Equals(Properties.Resources.AuditLogEnabled, Boolean.TrueString, StringComparison.OrdinalIgnoreCase));
        }

        private void initializeLogger()
        {
            var confRef = SecurityAccessor.Instance.EncryptConfig;
            try
            {
                //Ensure that the log directory is open
                var dirInfo = confRef.ClientConfig.GlobalConfiguration.BaseLogPath;
                if (!Directory.Exists(dirInfo))
                {
                    Directory.CreateDirectory(dirInfo);
                }
                this.fileLogger = FileLogger.Instance;
                //Logger is enabled if and only if the log level is not empty/null AND
                //the internal log enabled flag is set to "true"
                var logEnabled =
                (!string.IsNullOrEmpty(confRef.ClientConfig.ClientConfiguration.LogLevel) &&
                 (Properties.Resources.LogEnabled.Equals(
                     Boolean.TrueString, StringComparison.OrdinalIgnoreCase)));
                if (logEnabled)
                {
                    var logFileName = dirInfo + "\\" + FileLogger.FILENAME;
                    this.fileLogger.setEnabled(logEnabled);
                    this.fileLogger.initializeLogger(logFileName,
                                                     DefaultLoggerHandlers.defaultLogLevelCheckHandler,
                                                     DefaultLoggerHandlers.defaultLogLevelGenerator,
                                                     DefaultLoggerHandlers.defaultDateStampGenerator,
                                                     DefaultLoggerHandlers.defaultLogMessageHandler,
                                                     DefaultLoggerHandlers.defaultLogMessageFormatHandler);

                    this.fileLogger.setLogLevel(confRef.ClientConfig.ClientConfiguration.LogLevel);
                    var asteriskString = StringUtilities.fillString("*", 150);
                    this.fileLogger.logMessage(LogLevel.INFO, this, asteriskString);
                    this.fileLogger.logMessage(LogLevel.INFO, this, "AuditDesktopSession Initialized at {0} - {1}",
                                               DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
                    this.fileLogger.logMessage(LogLevel.INFO, this, asteriskString);
                    this.fileLogger.flush();
                }
            }
            catch (Exception eX)
            {
                //Not necessary to throw a major exception, just dump the information to the console for now
                //throw new ApplicationException("Cannot open log file for writing: " + eX.Message, eX);
                System.Console.WriteLine("An exception was thrown while opening the log file for writing: {0} {1}", eX.Message, eX);
            }
        }

        private void internalPopulateEmployees(DataTable emps)
        {
            string sFilter = "homestore = '" + CurrentSiteId.StoreNumber + "'";

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
                AssignedEmployees.Add(newUser);
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

        public override void GetPawnBusinessRules()
        {
            try
            {
                this.PawnRulesSys = new PawnRulesSystemImpl();
                var bRules = new List<String>
                {
                    "PWN_BR-000",

                };
                Dictionary<string, BusinessRuleVO> pawnBusinessRuleVO = PawnBusinessRuleVO;
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

        /// <summary>
        /// Gets the receipt printer details
        /// </summary>
        /// <returns></returns>
        private void GetReceiptPrinter()
        {
            string storeNumber = CurrentSiteId.StoreNumber;
            const string formName = "ReceiptPrinter";

            string terminalId = !string.IsNullOrEmpty(Properties.Resources.OverrideMachineName) ?
                                Properties.Resources.OverrideMachineName :
                                CurrentSiteId.TerminalId;

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
                object receiptIpObj = receiptData["##IPADDRESS01##"];
                object receiptPortObj = receiptData["##PORTNUMBER01##"];
                if (receiptIpObj != null && receiptPortObj != null)
                {
                    string receiptIp = Utilities.GetStringValue(receiptIpObj, string.Empty);
                    string receiptPort = Utilities.GetStringValue(receiptPortObj, string.Empty);
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
            string storeNumber = CurrentSiteId.StoreNumber;
            const string formName = "BarcodePrinter";

            string terminalId = !string.IsNullOrEmpty(Properties.Resources.OverrideMachineName) ?
                                Properties.Resources.OverrideMachineName :
                                CurrentSiteId.TerminalId;

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
                object barcodeIpObj = intermecData["##IPADDRESS01##"];
                object barcodePortObj = intermecData["##PORTNUMBER01##"];
                if (barcodeIpObj != null && barcodePortObj != null)
                {
                    string barcodeIp = Utilities.GetStringValue(barcodeIpObj, string.Empty);
                    string barcodePort = Utilities.GetStringValue(barcodePortObj, string.Empty);
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
            string storeNumber = CurrentSiteId.StoreNumber;
            const string formName = "LaserPrinter";

            string terminalId = !string.IsNullOrEmpty(Properties.Resources.OverrideMachineName) ?
                                Properties.Resources.OverrideMachineName :
                                CurrentSiteId.TerminalId;

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
            Hashtable laserData = GenerateDocumentsProcedures.GenerateDocumentsEssentialInformation(
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
                object laserIpObj = laserData["##IPADDRESS01##"];
                object laserPortObj = laserData["##PORTNUMBER01##"];
                if (laserIpObj != null && laserPortObj != null)
                {
                    string laserIp = Utilities.GetStringValue(laserIpObj, string.Empty);
                    string laserPort = Utilities.GetStringValue(laserPortObj, string.Empty);
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
            string storeNumber = CurrentSiteId.StoreNumber;
            const string formName = "LaserPrinter";

            string terminalId = !string.IsNullOrEmpty(Properties.Resources.OverrideMachineName) ?
                                Properties.Resources.OverrideMachineName :
                                CurrentSiteId.TerminalId;

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
            Hashtable laserData = GenerateDocumentsProcedures.GenerateDocumentsEssentialInformation(
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
                object laserIpObj = laserData["##IPADDRESS01##"];
                object laserPortObj = laserData["##PORTNUMBER01##"];
                if (laserIpObj != null && laserPortObj != null)
                {
                    string laserIp = Utilities.GetStringValue(laserIpObj, string.Empty);
                    string laserPort = Utilities.GetStringValue(laserPortObj, string.Empty);
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
            if (AuditDesktopSession.Instance.IsSkipLDAP)
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
                    DialogResult dR = userLoginForm.ShowDialog();
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
                Control userInfoGroupBox = deskForm.Controls["userInfoGroupBox"];
                Control userNameField = userInfoGroupBox.Controls["userNameField"];
                Control userEmpIdField = userInfoGroupBox.Controls["userEmpIdField"];
                Control userRoleField = userInfoGroupBox.Controls["userRoleField"];

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

                if (!userInfoGroupBox.Visible)
                {
                    userInfoGroupBox.Show();
                }
            }
            else if (deskForm != null && deskForm.Controls.ContainsKey("userInfoGroupBox"))
            {
                deskForm.Controls["userInfoGroupBox"].Hide();
                deskForm.Controls["userInfoGroupBox"].Controls["userNameField"].Text = string.Empty;
                deskForm.Controls["userInfoGroupBox"].Controls["userEmpIdField"].Text = string.Empty;
                deskForm.Controls["userInfoGroupBox"].Controls["userRoleField"].Text = string.Empty;
            }
#endif
        }

        public override void UpdateShopDate(Form deskForm)
        {
            ShopDateTime sdt = ShopDateTime.Instance;
            if (sdt != null)
            {
                //Get shop date label
                Control dtLabel = deskForm.Controls["shopDateLabel"];
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
                Control dtLabel = deskForm.Controls["shopDateLabel"];
                //If shop date label is valid, hide it
                if (dtLabel != null)
                {
                    dtLabel.Hide();
                }
            }
            if (timer == null)
            {
                timer = new Timer();
                timer.Tag = deskForm;
                timer.Interval = 60 * 60 * 1000;
                timer.Tick += new EventHandler(timer_Tick);
                timer.Start();
            }
        }

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

        void timer_Tick(object sender, EventArgs e)
        {
            if (sender == null)
                return;
            timer = sender as Timer;
            if (timer == null)
                return;
            var deskForm = timer.Tag as Form;
            UpdateShopDate(deskForm);
        }

        private void UpdateVersionLabel(Form deskForm)
        {
            if (!string.IsNullOrEmpty(SecurityAccessor.Instance.EncryptConfig.ClientConfig.AppVersion.AppVersion) &&
                deskForm != null)
            {
                //Get version label control
                Control verLabel = deskForm.Controls["versionLabel"];
                //If label is valid, set to app version
                if (verLabel != null)
                {
                    verLabel.Text =
                        SecurityAccessor.Instance.EncryptConfig.ClientConfig.AppVersion.AppVersion;
                }
            }
            else if (deskForm != null)
            {
                Control verLabel = deskForm.Controls["versionLabel"];
                if (verLabel != null)
                {
                    verLabel.Hide();
                }
            }
        }

        # endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pdfFilePath"></param>
        /// <param name="waitForExit"></param>
        public static void ShowPDFFile(string pdfFilePath, bool waitForExit)
        {
            PdfLauncher.Instance.ShowPDFFile(pdfFilePath, waitForExit);
        }


        #region IDesktopSession Members

        public CustomerVO ActiveCustomer
        {
            get { return null; }
        }

        public ItemSearchCriteria ActiveItemSearchData { get; set; }

        public LayawayVO ActiveLayaway
        {
            get { return null; }
        }

        public PawnLoan ActivePawnLoan
        {
            get
            {
                return (this.PawnLoans == null || this.PawnLoans.Count == 0 ? null : this.PawnLoans[0]);
            }
            set
            {
                if (value != null)
                {
                    if (this.PawnLoans != null && PawnLoans.Count > 0)
                        this.PawnLoans[0] = value;
                }
            }
        }


        public PurchaseVO ActivePurchase { get; set; }

        public SaleVO ActiveRetail { get; set; }

        public WebServiceProKnow CallProKnow { get; set; }

        public string CashDrawerName
        {
            get { return null; }
        }

        public CategoryNode CategoryXML { get; set; }

        public List<LayawayVO> CustomerHistoryLayaways { get; set; }

        public List<PawnLoan> CustomerHistoryLoans { get; set; }

        public List<SaleVO> CustomerHistorySales { get; set; }

        public CurrentContext DescribeItemContext { get; set; }

        public List<LayawayVO> Layaways { get; set; }

        public DataTable MerchandiseManufacturers { get; set; }

        public Dictionary<string, BusinessRuleVO> PawnBusinessRuleVO { get; set; }
        public List<PawnLoan> PawnLoans { get; set; }

        public List<PMetalInfo> PMetalData { get; set; }

        public PawnRulesSystemInterface PawnRulesSys { get; private set; }

        public bool PurchasePFIAddItem { get; set; }


        public List<PurchaseVO> Purchases
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ReplaceICN
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int ReplaceDocNumber
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public long ReplaceGunNumber
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int ReplaceNumberOfTags
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ReplaceDocType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void ClearCustomerList()
        {
            throw new NotImplementedException();
        }

        public override void ClearPawnLoan()
        {
            this.PawnLoans = null;
        }

        public override void ClearSessionData()
        {
            DescribeItemContext = CurrentContext.NEW;
            GenerateTemporaryICN = false;
            if (ActiveItemSearchData == null)
            {
                ActiveItemSearchData = new ItemSearchCriteria();
            }
            ActiveItemSearchData.CategoryID = 0;
            ActiveItemSearchData.CategoryDescription = string.Empty;
            ActiveItemSearchData.Manufacturer = string.Empty;
            ActiveItemSearchData.Model = string.Empty;

        }

        public override void PrintTags(Item _Item, CurrentContext context)
        {
            if (Utilities.GetIntegerValue(_Item.PfiTags) > 0 && _Item.PfiAssignmentType != PfiAssignment.Refurb)
            {
                bool bPrintTag = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled;
                if (bPrintTag && BarcodePrinter.IsValid)
                {
                    IntermecBarcodeTagPrint.PrinterModel sPrinterModel = IntermecBarcodeTagPrint.PrinterModel.Intermec_PM4i;

                    var intermecBarcodeTagPrint =
                    new IntermecBarcodeTagPrint(SecurityAccessor.Instance.EncryptConfig.ClientConfig.StoreConfiguration.CompanyName,
                                                Convert.ToInt32(CurrentSiteId.StoreNumber),
                                                sPrinterModel,
                                                BarcodePrinter.IPAddress,
                                                (uint)BarcodePrinter.Port, this);

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
                    IntermecBarcodeTagPrint.PrinterModel sPrinterModel = IntermecBarcodeTagPrint.PrinterModel.Intermec_PM4i;

                    var intermecBarcodeTagPrint =
                    new IntermecBarcodeTagPrint(SecurityAccessor.Instance.EncryptConfig.ClientConfig.StoreConfiguration.CompanyName,
                                                Convert.ToInt32(CurrentSiteId.StoreNumber),
                                                sPrinterModel,
                                                BarcodePrinter.IPAddress,
                                                (uint)BarcodePrinter.Port, this);

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

        public void ScanParseInstance(object sender, string sBarCodeData, TextBoxBase userNameControl, TextBoxBase passwordControl, ButtonBase submitBtn)
        {
            throw new NotImplementedException();
        }

        #endregion

        public override void UpdateDesktopCustomerInformation(Form form)
        {
            throw new NotImplementedException();
        }

        public override void ShowDesktopCustomerInformation(Form form, bool b)
        {
            throw new NotImplementedException();
        }

        public override bool IsButtonTellerOperation(string buttonTagName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserAssignedCashDrawer(string username, string cashdrawer, out string cashDrawerflag)
        {
            throw new NotImplementedException();
        }

        public override void showProcessTender(ProcessTenderProcedures.ProcessTenderMode processTenderMode)
        {
            throw new NotImplementedException();
        }

        public override void PerformCashDrawerChecks(out bool checkPassed)
        {
            throw new NotImplementedException();
        }

        public override void CheckOpenCashDrawers(out bool openCD)
        {
            throw new NotImplementedException();
        }
    }
}
