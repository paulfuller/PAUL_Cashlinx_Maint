using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Network;
using Common.Controllers.Security;
using Common.Libraries.Forms;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Common.Libraries.Utility;
using Support.Flows.AppController.Impl;
using Support.Forms;
using Support.Libraries.Objects.Customer;
using Support.Libraries.Objects.PDLoan;
using Support.Libraries.Utility;

namespace Support.Logic
{
    /*
     * Utilizing thread safe lock free singleton strategy 
     * Singleton implementation strategy from http://www.yoda.arachsys.com/csharp/singleton.html
     */
    public sealed class CashlinxPawnSupportSession : DesktopSession
    {
        private OracleDataAccessor oracleDA;

        public DataTable EyeColorTable;
        public DataTable HairColorTable;
        public DataTable IdTypeTable;
        public DataTable CountryTable;
        public DataTable StateTable;
        public DataTable TitleSuffixTable;
        public DataTable TitleTable;
        public DataTable RaceTable;
        public DataTable HearAboutUsTable;

        //public List<PawnLoan> PawnLoanKeys { get; set; }
        //public List<PawnLoan> PawnLoanKeysAuxillary { get; set; }
        public const int USERNAME_MAXLEN = 5;

        public bool SafeMode = false;
        public bool LockProductsTab { get; set; }
        public bool ShowOnlyHistoryTabs { get; set; }

        private FileLogger fileLogger;
        private bool skipLDAP;
        private CategoryVO CategoryVOObject = null;
        private string formdisplaytype;

        private Form desktopForm;
        private MainFlowExecutor mainFlowExecutor;

        //Code added for the Product and Services tab
        public List<PDLoan> PDLoanKeys { get; set; }
        public PDLoan ActivePDLoan { get; set; }
        public DepositDateExtensionDetails DepositDateExtensionDetailsObject { get; set; }
        public SupportProductType TicketTypeLookedUp { get; set; }

        /// Singleton instance variable
        static readonly CashlinxPawnSupportSession instance = new CashlinxPawnSupportSession();
        
        #region APPLICATION

        /// <summary>
        /// Static constructor - forces compiler to initialize the object prior to any code access
        /// </summary>
        /*__________________________________________________________________________________________*/
        static CashlinxPawnSupportSession()
        {
        }

        /// <summary>
        /// Static instance property accessor
        /// </summary>
        /*__________________________________________________________________________________________*/
        public static CashlinxPawnSupportSession Instance
        {
            get
            {
                return (instance);
            }
        }
        /*__________________________________________________________________________________________*/
        public CashlinxPawnSupportSession()
        {
            var confRef = SecurityAccessor.Instance.EncryptConfig;
            this.userState = UserDesktopState.NOTLOGGEDIN;
            this.UserName = string.Empty;
            this.IsoLevel = IsolationLevel.ReadCommitted;
            ButtonResourceManagerHelper = new ButtonResourceManagerHelper();
            this.MenuEnabled = true;
            this.skipLDAP = false;
            this.timer = null;
            this.HistorySession = null;
            ApplicationExit = false;
            if (confRef == null)
            {
                return;
            }

            this.customers = new List<CustomerVO>(1) { new CustomerVO() };
            this.PawnLoans = new List<PawnLoan>(1) { new PawnLoan() };
            this.PawnLoanKeys = new List<PawnLoan>(1);
            this.PawnLoanKeysAuxillary = new List<PawnLoan>(1);
            this.PawnLoans_Auxillary = new List<PawnLoan>();

            this.PDLoanKeys = new List<PDLoan>(1) { new PDLoan() };
            //Init the items requiring encrypted configuration and use default audit log handlers, disabling audit logging for now
            GlobalDataAccessor.Instance.Init(this, confRef, "SupportApp",
                auditLogEnabledChangeHandlerBase, auditLogMessageHandlerBase, false);

            //Create main flow executor
            this.mainFlowExecutor = new MainFlowExecutor();

            //Create the application controller
            this.AppController = new AppWorkflowController(this, this.mainFlowExecutor);

            // get the list of available buttons for support app
            string errorCode;
            string errorText;

            List<string> buttonTagNames;
            var retVal = getButtonNames(GlobalDataAccessor.Instance.OracleDA,
                                           out buttonTagNames, out errorCode, out errorText);

            CurrentSiteId.AvailableButtons = buttonTagNames;
            

        }
        /*__________________________________________________________________________________________*/
        public CustomerVOForSupportApp ActiveCustomer
        {
            get
            {
                return (CustomerVOForSupportApp)(this.customers.Count == 0 ? null : this.customers[0]);
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

        /*__________________________________________________________________________________________*/
        public override void Setup(Form deskForm)
        {
            procMsgFormPwd = new ProcessingMessage("* INITIALIZING SUPPORT APPLICATION *");
            // Set up the History Session Object
            HistorySession = new HistoryTrack(deskForm);
            // Set CashlinxPawnSupportSession's desktop form
            this.desktopForm = deskForm;

            // Load barcode formats during startup until Admin section created
            try
            {
                procMsgFormPwd.Message = "* LOADING SUPPORT APPLICATION DATA *";
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Error retrieving start up data from the database", new ApplicationException(ex.Message));
            }
            procMsgFormPwd.Hide();
            CashlinxPawnSupportSession.instance.PerformAuthorization();
            GetPickListValues();

        }
        #endregion
        #region PUBLIC PROPERTIES & METHODS
        /*__________________________________________________________________________________________*/
        public string FormDisplayType { get; set; }

        /*__________________________________________________________________________________________*/
        public Support.Libraries.Forms.FlowTabController.State TabStateClicked { get; set; }
        // Global Setup Method for any pre-loading requirements of Desktop Forms or Panels
        /*__________________________________________________________________________________________*/
        public override void GetCashDrawerAssignmentsForStore()
        {
        }

        /*__________________________________________________________________________________________*/
        public override void UpdateDesktopCustomerInformation(Form form)
        {
        }
        /*__________________________________________________________________________________________*/
        public override void ShowDesktopCustomerInformation(Form form, bool b)
        {
        }
        /*__________________________________________________________________________________________*/
        public override void PerformAuthorization()
        {
            this.PerformAuthorization(false);
        }
        public override void ClearCustomerList()
        {
        }
        /*__________________________________________________________________________________________*/
        public override void ClearPawnLoan()
        {
        }
        #endregion
        #region MISC METHODS
        //public System.Windows.Forms.Form.Screen MainWindowScreenBoundry { get; set; }

        public int xPosition { get; set; }
        public int yPosition {get;set;}
        //WCM Testing Active Screen
        /*__________________________________________________________________________________________*/
        public void SetPrimaryScreenToMain()  //System.Drawing.Rectangle rect, Form ActiveForm)
        {

            Screen screen = Screen.AllScreens[0];
            if ( screen.Primary)
            {
                Console.WriteLine("test");
            }

        }

        ///*__________________________________________________________________________________________*/
        //public void ActiveWindowsScreen(System.Windows.Forms.Form currentform)
        //{
        //    System.Windows.Forms.Screen[] allScreens = System.Windows.Forms.Screen.AllScreens;
        //    System.Windows.Forms.Screen myScreen = allScreens[0];

        //    System.

        //    int screenId = RegistryManager.ScreenId;
        //    if (screenId > 0)
        //    {
        //        myScreen = allScreens[screenId - 1];
        //    }

        //    Point centerOfScreen = new Point((myScreen.WorkingArea.Left + myScreen.WorkingArea.Right) / 2,
        //                                     (myScreen.WorkingArea.Top + myScreen.WorkingArea.Bottom) / 2);
        //    currentform.Location = new Point(centerOfScreen.X - this.Width / 2, centerOfScreen.Y - this.Height / 2);

        //    currentform.StartPosition = FormStartPosition.Manual;
        //}

        /*__________________________________________________________________________________________*/
        public void RestoreMenu()
        {
            if (this.desktopForm == null)
            {
                return;
            }
            var cdFm = (SupportAppDesktop)this.desktopForm;
            cdFm.handleEndFlow(null);
        }
        /*__________________________________________________________________________________________*/
        public bool getButtonNames(OracleDataAccessor da,
                                   out List<string> buttonNames,
                                   out string errorCode,out string errorText )
        {
            errorCode = string.Empty;
            errorText = string.Empty;
            buttonNames = null;
            const string Smsg = "Please Contact Support";
            bool Retval;
            string msg;

            //create input parameters
            List<OracleProcParam> iParams = new List<OracleProcParam>();
            //iParams.Add(new OracleProcParam("p_shop_number", "12345"));

            //setup ref cursor
            var RefCursor = new List<PairType<string, string>>();
            RefCursor.Add(new PairType<string, string>("o_button_data", "OUT_DATA"));


            //execute stored procedure
            DataSet OutDataSet;
            try
            {
                Retval = da.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_support_gen_procs", "getButtonTagNames", iParams,
                    RefCursor, "o_return_code", "o_return_text", out OutDataSet);
            }
            catch (Exception oEx)
            {
                msg = string.Format("Invocation of getButtonTagNames stored proc failed{0}{1}{0}{2}",
                         Environment.NewLine, oEx.Message, Smsg);
                MessageBox.Show(msg, "Error");
                return false;
            }

            if (Retval == false)
            {
                errorCode = da.ErrorCode;
                errorText = da.ErrorDescription;
                return (false);
            }

            if (OutDataSet.Tables != null && OutDataSet.Tables.Count > 0)
            {
                if (OutDataSet.Tables[0].Rows.Count > 0)
                {
                    buttonNames = new List<string>();
                    foreach (DataRow dr in OutDataSet.Tables[0].Rows)
                    {
                        string buttonTag = Utilities.GetStringValue(dr["tag_name"], "");
                        buttonNames.Add(buttonTag);
                    }

                }

            }

            return true;
        }
        /*__________________________________________________________________________________________*/
        public override void ClearLoggedInUser()
        {
            this.UserName = string.Empty;
            UpdateDesktopUserName(this.desktopForm);
        }
        /*__________________________________________________________________________________________*/
        public override void ClearSessionData()
        {
        }
        #endregion
        #region LOGON METHODS
        /// <summary>
        /// Update the user name field on the new desktop form
        /// </summary>
        /// <param name="deskForm">The new desktop form</param>
        /*__________________________________________________________________________________________*/
        private void UpdateDesktopUserName(Form deskForm)
        {
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
            else if (deskForm != null)
            {
                deskForm.Controls["userInfoGroupBox"].Hide();
                deskForm.Controls["userInfoGroupBox"].Controls["userNameField"].Text = "";
                deskForm.Controls["userInfoGroupBox"].Controls["userEmpIdField"].Text = "";
                deskForm.Controls["userInfoGroupBox"].Controls["userRoleField"].Text = "";
            }
        }

        /// <summary>
        /// Will return a false if the user cancels out of the authentication process
        /// </summary>
        /// <returns></returns>
        /*__________________________________________________________________________________________*/
        private bool PerformLDAPAuthentication(
            ref int attemptCount,
            ref string username,
            ref string password,
            out bool lockedOut,
            out bool needToChangePassword,
            out bool wantsPasswordChange
            )
        {
            needToChangePassword = false;
            wantsPasswordChange = false;
            lockedOut = false;
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

                            //Check if the password expired);
                            if (pawnLDAPAccessor.PasswordPolicy.IsExpired(
                                initialLastModifiedDate, DateTime.UtcNow))
                            {
                                needToChangePassword = true;
                            }
                            //check if the password is about to expire
                            else if (pawnLDAPAccessor.PasswordPolicy.IsExpiredWarning(
                                initialLastModifiedDate, DateTime.UtcNow))
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
                            var ldapService =
                                conf.GetLDAPService(
                                    out loginDN,
                                    out searchDN,
                                    out userIdKey,
                                    out userPwd,
                                    out pwdPolicyCN);
                            if (ldapService == null)
                                return (false);
                            //,
                            //out userName);
                            //Store the user DN in user VO object
                            LoggedInUserSecurityProfile.UserDN = userIdKey + "=" + username + "," + searchDN;
                            if (pwdHistory != null)
                            {
                                foreach (var t in pwdHistory)
                                {
                                    LoggedInUserSecurityProfile.UserPasswordHistory.Add(t);
                                }
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
        #endregion
        #region OVERRIDE METHODS
             //Call to login the user and get their security profile
        /*__________________________________________________________________________________________*/
        public override void PerformAuthorization(bool chgUsrPasswd)
        {
            var password = string.Empty;
            var username = string.Empty;
            if (!this.skipLDAP)
            {
                LoggedInUserSecurityProfile = new UserVO();
                this.userState = UserDesktopState.NOTLOGGEDIN;
                //procMsgFormPwd.Show();
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
                            out pwdPolicyCN);
                    //Initialize LDAP connection
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
                    //var username = string.Empty;
                    var fullAuth = this.PerformLDAPAuthentication(
                        ref attemptCount,
                        ref username,
                        ref password,
                        out lockedOut,
                        out needPasswordChange,
                        out wantsPasswordChange);
                    int outVal = 1;
                    string errCode = string.Empty, errTxt;
                    if (LoginCancel)
                    {
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
                                password = chngPwdForm.EnteredNewPassword;
                            }
                        }
                        var retVal = true;
                        //int outVal = 1;
                        //var errCode = "0";
                        //TODO: Update with reference to shared data procedure project when ready
                        //                        retVal = ShopProcedures.ExecuteUpdateSelectUserInfoActivated(
                        //                            username.ToLowerInvariant(), -1, out outVal, out errCode, out errTxt);
                        errCode = "0";
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
                            //TODO: Link to shared data procedures project when ready
                            //ShopProcedures.ExecuteUpdateSelectUserInfoActivated(
                            //    username.ToLowerInvariant(), 0, out outVal, out errCode, out errTxt);
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
                                password = uPwdForm.EnteredNewPassword;
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
                    else
                    {
                        break;
                    }

                }
                while (this.userState != UserDesktopState.LOGGEDIN);
            }

            if (!LoginCancel && this.userState == UserDesktopState.LOGGEDIN)
            {
                //UpdateDesktopUserName(this.desktopForm);
                //Get role information
                //The logged in user's security profile will be stored in LoggedInUserSecurityProfile object after the call
                //TODO: Change to select store prior to getting user security profile
                //UpdateDesktopUserName(this.desktopForm);

                if (userState == UserDesktopState.LOGGEDIN)
                {
                    //Set the password in the users security profile
                    LoggedInUserSecurityProfile.UserCurrentPassword = password;
                    LoggedInUserSecurityProfile.UserID = username;
                }
            }

            procMsgFormPwd.Close();
            procMsgFormPwd.Dispose();
        }

        /*__________________________________________________________________________________________*/
        public override bool IsButtonAvailable(string buttonTagName, SiteId currentSiteId)
        {
            if (currentSiteId == null || string.IsNullOrEmpty(buttonTagName))
            {
                return false;
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


            return true;
        }
        /*__________________________________________________________________________________________*/
        public override bool IsButtonTellerOperation(string buttonTagName)
        {
            //No teller level checks here
            //must return true !!! DO NOT CHANGE !!!
            return true;
        }
        /*__________________________________________________________________________________________*/
        public override bool IsUserAssignedCashDrawer(string username, string cashdrawer, out string cashDrawerflag)
        {
            cashDrawerflag = string.Empty;
            return true;
        }
        /*__________________________________________________________________________________________*/
        public override void PrintTags(Item _Item, bool reprint)
        {
        }
        /*__________________________________________________________________________________________*/
        public override void PrintTags(Item _Item, CurrentContext context)
        {
        }
        /*__________________________________________________________________________________________*/
        public override void ScanParse(object sender, string sBarCodeData, TextBoxBase userNameControl, TextBoxBase passwordControl, ButtonBase submitBtn)
        {
        }
        /*__________________________________________________________________________________________*/
        public override bool WriteUsbData(string data)
        {
            return false;
        }
        /*__________________________________________________________________________________________*/
        public override void DeviceRemovedEventHandler(object sender, USBUtilities.DriveDetectorEventArgs e)
        {
            return;
        }
        /*__________________________________________________________________________________________*/
        public override void DeviceArrivedEventHandler(object sender, USBUtilities.DriveDetectorEventArgs e)
        {
            return;
        }
        /*__________________________________________________________________________________________*/
        public override void Dispose()
        {
        }
        /*__________________________________________________________________________________________*/
        public override void showProcessTender(ProcessTenderProcedures.ProcessTenderMode processTenderMode)
        {
            
        }
        /*__________________________________________________________________________________________*/
        public override void PerformCashDrawerChecks(out bool checkPassed)
        {
            checkPassed = true;
        }
        /*__________________________________________________________________________________________*/
        public override void CheckOpenCashDrawers(out bool openCD)
        {
            openCD = false;
        }
        /*__________________________________________________________________________________________*/
        public override void UpdateShopDate(Form fm)
        {
            
        }
        /*__________________________________________________________________________________________*/
        public override void GetPawnBusinessRules()
        {
            
        }
        #endregion
        #region DATAT OBJECTS
        /*__________________________________________________________________________________________*/
        public CategoryVO CustomerCommentCategories
        {
            get
            {
                if (CategoryVOObject == null)
                {
                    string errorCode = string.Empty;
                    string errorMessage = string.Empty;

                    CategoryVOObject = new CategoryVO();

                    var customerDBProceduresForSupport = new Support.Controllers.Database.Procedures.CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession);
                    DataTable categoryDatatable = new DataTable();
                    customerDBProceduresForSupport.GetCustomerCommentCategories(out categoryDatatable, out errorCode, out errorMessage);

                    DataRow[] result = categoryDatatable.Select();
                    foreach (DataRow row in result)
                    {
                        var categoryObj = new Category();

                        categoryObj.CategoryId = Utilities.GetIntegerValue(row.ItemArray[(int)categoryrecord.CATEGORYID], 0);
                        categoryObj.CategoryName = Utilities.GetStringValue(row.ItemArray[(int)categoryrecord.CATEGORYNAME], "");
                        //categoryObj.Description = Utilities.GetStringValue(row.ItemArray[(int)categoryrecord.DESCRIPTION], "");
                        CategoryVOObject.CommentCategories.Add(categoryObj);
                    }

                    var categoryBlank = new Category();
                    categoryBlank.CategoryId = 0;
                    categoryBlank.CategoryName = "Select";
                    CategoryVOObject.CommentCategories.Insert(0, categoryBlank);
                }
                return CategoryVOObject;
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                this.CategoryVOObject = value;
            }
        }
        /// <summary>
        /// call to the database to get all picklist values related to customer use case
        /// </summary>
        /*__________________________________________________________________________________________*/
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
        #endregion
    }
}
