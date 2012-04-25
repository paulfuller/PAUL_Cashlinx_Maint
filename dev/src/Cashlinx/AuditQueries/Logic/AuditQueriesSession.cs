using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AuditQueries.Properties;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Network;
using Common.Controllers.Security;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;

namespace AuditQueries.Logic
{
    public sealed class AuditQueriesSession : DesktopSession
    {
        #region Constants
        public const int USERNAME_MAXLEN = 5;
        #endregion

        #region Singleton Implementation
        static readonly AuditQueriesSession instance = new AuditQueriesSession();
        static AuditQueriesSession()
        {
        }

        /// <summary>
        /// Static instance property accessor
        /// </summary>
        public static AuditQueriesSession Instance
        {
            get
            {
                return (instance);
            }
        }
        #endregion

        #region Interface implemented fields

        public override void GetCashDrawerAssignmentsForStore()
        {           
        }

        public override void Setup(Form desktopForm)
        {
        }

        public override void UpdateDesktopCustomerInformation(Form form)
        {
        }

        public override void ShowDesktopCustomerInformation(Form form, bool b)
        {            
        }

        public override void ClearCustomerList()
        {
        }

        public override void ClearPawnLoan()
        {
        }

        public override void ClearLoggedInUser()
        {
            return;
        }

        public override void ClearSessionData()
        {
            return;
        }

        public override void PerformAuthorization(bool chgUsrPasswd)
        {            
        }

        public override bool IsButtonAvailable(string buttonTagName, SiteId currentSiteId)
        {
            //Should always return true from this method unless
            //there are role restrictions at the button/store level
            return true;
        }

        public override bool IsButtonTellerOperation(string buttonTagName)
        {
            return true;
        }

        public override bool IsUserAssignedCashDrawer(string username, string cashdrawer, out string cashDrawerflag)
        {
            cashDrawerflag = string.Empty;
            return true;
        }

        public override void PrintTags(Item _Item, bool reprint)
        {
        }

        public override void PrintTags(Item _Item, CurrentContext context)
        {
        }

        public override void ScanParse(object sender, string sBarCodeData, TextBoxBase userNameControl, TextBoxBase passwordControl, ButtonBase submitBtn)
        {
        }

        public override bool WriteUsbData(string data)
        {
            return false;
        }

        public override void DeviceRemovedEventHandler(object sender, USBUtilities.DriveDetectorEventArgs e)
        {
            return;
        }

        public override void DeviceArrivedEventHandler(object sender, USBUtilities.DriveDetectorEventArgs e)
        {
            return;
        }

        #endregion

        public override void Dispose()
        {
        }

        public override void showProcessTender(ProcessTenderProcedures.ProcessTenderMode processTenderMode)
        {
        }

        public override void PerformCashDrawerChecks(out bool checkPassed)
        {
            checkPassed = true;
        }

        public override void CheckOpenCashDrawers(out bool openCD)
        {
            openCD = false;
        }

        public override void UpdateShopDate(Form fm)
        {
            return;
        }

        public override void GetPawnBusinessRules()
        {
            return;
        }

        //Const query resource names
        private const string QUERY_PREFIX = "Query_";
        private const string QUERY_NAME_SUFFIX = "_Name";
        private const string QUERY_DESC_SUFFIX = "_Desc";
        private const string QUERY_PARAMS_SUFFIX = "_Params";
        private const string QUERY_SQL_SUFFIX = "_SQL";

        //Audit queries fields
        private readonly QueryStorage queryStorage;
        private int selectedQuery;
        private List<int> ignoreableQueries;

        //Other fields
        private string username;
        private string password;

        public void SetUserNameAndPassword(string uName, string pwd)
        {
            this.username = uName;
            this.password = pwd;
        }

        public bool IsIgnoredQuery(int id)
        {
            if (CollectionUtilities.isNotEmpty(this.ignoreableQueries) && ignoreableQueries.Contains(id))
            {
                return (true);
            }

            return (false);
        }

        public int SelectedQueryId
        {
            get
            {
                return (this.selectedQuery);
            }
        }


        private bool populateQueryStorageWithResources()
        {
            //Acquire number of queries and the ignore list
            int numQueries = Utilities.GetIntegerValue(Resources.NumberQueries, 0);
            var ignoreQueries = Utilities.GetStringValue(Resources.IgnoreQueries, string.Empty);
            if (!string.IsNullOrEmpty(ignoreQueries))
            {
                //See if we only have one entry
                if (!ignoreQueries.Contains(","))
                {
                    //We only have one entry...convert to int and add
                    int queryToIgnore = Utilities.GetIntegerValue(ignoreQueries, 0);
                    if (queryToIgnore > 0 && queryToIgnore <= numQueries)
                    {
                        this.ignoreableQueries.Add(queryToIgnore);
                    }
                }
                else
                {
                    //Parse ignore queries
                    var ignoreTokens = ignoreQueries.Split(',');
                    if (ignoreTokens.Length > 0)
                    {
                        foreach(string s in ignoreTokens)
                        {
                            if (string.IsNullOrEmpty(s))
                                continue;
                            int queryToIgnore = Utilities.GetIntegerValue(s, 0);
                            if (queryToIgnore > 0 && queryToIgnore <= numQueries)
                            {
                                this.ignoreableQueries.Add(queryToIgnore);
                            }
                        }
                    }
                }
            }

            //Make sure we have queries to run through
            if (numQueries <= 0)return (false);

            //Now we need to run through the resources with particular name patterns to pull the queries
            for (int i = 1; i <= numQueries; ++i)
            {
                //Ensure we are not adding an ignored query
                if (this.ignoreableQueries.Contains(i))
                {
                    continue;
                }
                //Construct keys
                var queryNameKey = string.Format("{0}{1}{2}", QUERY_PREFIX, i, QUERY_NAME_SUFFIX);
                var queryDescKey = string.Format("{0}{1}{2}", QUERY_PREFIX, i, QUERY_DESC_SUFFIX);
                var queryParamsKey = string.Format("{0}{1}{2}", QUERY_PREFIX, i, QUERY_PARAMS_SUFFIX);
                var querySQLKey = string.Format("{0}{1}{2}", QUERY_PREFIX, i, QUERY_SQL_SUFFIX);

                //Pull resources
                string queryName = Resources.ResourceManager.GetString(queryNameKey);
                string queryDesc = Resources.ResourceManager.GetString(queryDescKey);
                string queryParams = Resources.ResourceManager.GetString(queryParamsKey);
                string querySQL = Resources.ResourceManager.GetString(querySQLKey);

                //If the three primary keys are invalid in any way, exit method
                if (string.IsNullOrEmpty(queryName) || string.IsNullOrEmpty(queryDesc) || string.IsNullOrEmpty(querySQL))
                {
                    //Invalid query specified
                    //Log error and report exception
                    return (false);
                }

                //Determine if the query needs parameter data and then determine if we have enough parameter data
                bool needsParameters = querySQL.Contains("?");
                if (needsParameters && string.IsNullOrEmpty(queryParams))
                {
                    //Query needs parameter data and no data was specified
                    //Log error and report exception
                    return (false);
                }

                //Add base query data to storage
                if (!queryStorage.AddQuery(i, querySQL, queryName, queryDesc))
                {
                    //Could not add current query to storage
                    //Log error and report exception
                    return (false);
                }

                if (needsParameters && !string.IsNullOrEmpty(queryParams))
                {
                    //Break apart query params into sections
                    var queryParamCompartments = queryParams.Split(new[]
                    {
                        ',',
                        ':'
                    });
                    //Length must be greater than zero and must be an even number (paramname:paramtype)
                    //as the parameters are specified in pairs
                    if (queryParamCompartments.Length <= 0 || queryParamCompartments.Length % 2 != 0)
                    {
                        //No tokens found
                        //Log error and report exception
                        return (false);
                    }
                    for (int qcIdx=0; qcIdx < queryParamCompartments.Length; qcIdx += 2)
                    {
                        var qcName = queryParamCompartments[qcIdx];
                        var qcType = queryParamCompartments[qcIdx + 1];
                        
                        //Value passed in (last parameter) must be initially empty!!
                        if (!queryStorage.AddQueryParameter(i, qcName, qcType, ""))
                        {
                            //Could not add parameter
                            //Log error and report exception
                            return (false);
                        }
                    }
                }
            }

            //If we get here, we are successful
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        public AuditQueriesSession()
        {
            queryStorage = new QueryStorage();
            selectedQuery = -1;
            ignoreableQueries = new List<int>();
            populateQueryStorageWithResources();
            LoginCancel = false;
        }

        public QueryStorage GetQueryStorage()
        {
            return (this.queryStorage);
        }

        public bool SetSelectedQuery(int selQ)
        {
            //Should be a one-based index coming in
            if (selQ < 1)
                return (false);
            bool rt = false;
            //Determine actual selected query by traversing the query list and skipping ignored queries (if any exist)
            if (CollectionUtilities.isEmpty(ignoreableQueries))
            {
                selectedQuery = selQ;
                rt = true;
            }
            else
            {
                int actualQ = selQ;
                if (!ignoreableQueries.Contains(actualQ))
                {
                    //Set the selected query if we are still at a valid index
                    if (queryStorage.GetQueryIds().Contains(actualQ) && !ignoreableQueries.Contains(actualQ))
                    {
                        selectedQuery = actualQ;
                        rt = true;
                    }
                }
            }
            return (rt);
        }

        public void ClearSelectedQuery()
        {
            this.selectedQuery = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Setup()
        {
            //Processing message
            var pMesg = new ProcessingMessage("* AUDIT QUERY APP SETUP *");
            pMesg.Show();

            //Initialize database connection, LDAP connection, and ShopTime
            var confRef = SecurityAccessor.Instance.EncryptConfig;
            if (confRef == null)
                return (false);

            this.userState = UserDesktopState.NOTLOGGEDIN;
            this.UserName = string.Empty;

            GlobalDataAccessor.Instance.Init(
                this, confRef, "AuditQueryApp", 
                auditLogEnabledChangeHandlerBase,
                auditLogMessageHandlerBase, false);

            pMesg.Close();
            pMesg.Dispose();
            return (true);
        }

        public void Shutdown()
        {
            GlobalDataAccessor.Instance.Dispose();
            Application.Exit();
        }

        public bool AuthenticateUser(string uName, string passwd)
        {
            SetUserNameAndPassword(uName, passwd);
            PerformAuthorization();
            if (this.userState != UserDesktopState.LOGGEDIN)
            {
                return (false);
            }
            return (true);
        }

        /// <summary>
        /// Will return a false if the user cancels out of the authentication process
        /// </summary>
        /// <returns></returns>
        private bool PerformLDAPAuthentication(
            ref int attemptCount,
            out bool lockedOut,
            out bool needToChangePassword,
            out bool wantsPasswordChange)
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
                catch (Exception eX)
                {
                    BasicExceptionHandler.Instance.AddException("Exception thrown in PerformLDAPAuthentication" + eX.Message, new ApplicationException("PerformLDAPAuthentication Exception", eX));
                }
            }
            else if (this.userState == UserDesktopState.NOTLOGGEDIN && pawnLDAPAccessor.State == PawnLDAPAccessor.LDAPState.DISCONNECTED)
            {
                return false;
            }

            return (true);
        }

        public override void PerformAuthorization()
        {
            if (FileLogger.Instance.IsLogInfo)
            {
                FileLogger.Instance.logMessage(LogLevel.INFO, this, "Performing user authorization...");
            }
            LoggedInUserSecurityProfile = new UserVO();
            this.userState = UserDesktopState.NOTLOGGEDIN;
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
                var fullAuth = this.PerformLDAPAuthentication(
                    ref attemptCount,
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
                if (!LoginCancel && fullAuth)
                {
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
                        /*ShopProcedures.ExecuteUpdateSelectUserInfoActivated(
                            username.ToLowerInvariant(), 0, out outVal, out errCode, out errTxt);*/
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
                        MessageBox.Show(
                            "Your password has expired. " +
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
                                "Application has exited due to an expired password");
                        }
                        finally
                        {
                            throw new ApplicationException(
                                "Application has exited due to an expired password");
                        }
                    }
                }
                if (!LoginCancel && fullAuth == false)
                {
                    this.userState = UserDesktopState.NOTLOGGEDIN;
                    break;
                }
                else
                {
                    break;
                }
            }
            while (this.userState != UserDesktopState.LOGGEDIN);

            if (!LoginCancel && this.userState == UserDesktopState.LOGGEDIN)
            {
                if (FileLogger.Instance.IsLogInfo)
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, this, "User {0} successfully authorized", FullUserName);
                }

                //Get role information
                //The logged in user's security profile will be stored in LoggedInUserSecurityProfile object after the call
                string errorCode;
                string errorMesg;
                if (!SecurityProfileProcedures.GetUserSecurityProfile(FullUserName, string.Empty, 
                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, "N",
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
                    //If the logged in user is not an auditor, display an error message and exit the application
                    if (!SecurityProfileProcedures.CanUserViewResource(
                        "AuditAppAccess", this.LoggedInUserSecurityProfile, this))
                    {
                        MessageBox.Show("You do not have sufficient security privileges to utilize this Audit application. Exiting the application");
                        Application.Exit();
                    }
                }

                if (userState == UserDesktopState.LOGGEDIN)
                {
                    //Set the password in the users security profile
                    LoggedInUserSecurityProfile.UserCurrentPassword = password;

                }
            }
        }
    }
}
