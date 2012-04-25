using System;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Security.Permissions;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;

namespace Common.Controllers.Network
{
    [DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
    public sealed class PawnLDAPAccessor : MarshalByRefObject, IDisposable
    {
        #region Singleton related fields
        // ReSharper disable InconsistentNaming
        private static readonly object mutex = new object();
        static readonly PawnLDAPAccessor instance = new PawnLDAPAccessor();
        // ReSharper restore InconsistentNaming
        static PawnLDAPAccessor()
        {
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public static PawnLDAPAccessor Instance
        {
            get
            {
                return (instance);
            }
        }

        #endregion

        #region Sub Classes

        #endregion

        #region Constants and Enumerations
        public enum LDAPState
        {
            DISCONNECTED,
            CONNECTED
        };

        private static string[] PWD_POLICY_ATTRIBS_STR =
        {
            "pwdmustchange",
            "pwdlockout",
            "pwdchecksyntax",
            "pwdsafemodify",
            "pwdgraceloginlimit",
            "pwdlockoutduration",
            "pwdexpirewarning",
            "pwdinhistory",
            "pwdminlength",
            "passwordminalphachars",
            "pwdattribute",
            "pwdmaxfailure",
            "passwordmaxrepeatedchars",
            "passwordminotherchars",
            "pwdfailurecountinterval",
            "pwdallowuserchange",
            "passwordmindiffchars",
            "pwdminage",
            "pwdmaxage"
        };

        public enum PWD_POLICY_ATTRIBS
        {
            PWDMUSTCHANGE = 0,
            PWDLOCKOUT    = 1,
            PWDCHECKSYNTAX = 2,
            PWDSAFEMODIFY = 3,
            PWDGRACELOGINLIMIT = 4,
            PWDLOCKOUTDURATION = 5,
            PWDEXPIREWARNING = 6,
            PWDINHISTORY = 7,
            PWDMINLENGTH = 8,
            PWDMINALPHACHARS = 9,
            PWDATTRIBUTE = 10,
            PWDMAXFAILURE = 11,
            PWDMAXREPCHARS = 12,
            PWDMINOTHERCHARS = 13,
            PWDFAILUREACCTINTERVAL = 14,
            PWDALLOWUSERCHANGE = 15,
            PWDMINDIFFCHARS = 16,
            PWDMINAGE = 17,
            PWDMAXAGE = 18
        }

        private const string OBJECT_CLASS_FILTER = "(objectClass=*)";
        private const int LDAP_DEFAULT_PORT = 389;
        
        #endregion


        #region Private Class Fields
        private LdapConnection ldapConnection;
        private string ldapServer;
        private string ldapPort;
        private LDAPState state;
        private string ldapLoginUsr;
        private string ldapLoginPwd;
        private string ldapUserSearchDN;
        private string ldapUserIdKey;
        private string ldapPwdPolicyCN;
        private string errorMessage;
        private PasswordPolicyVO pwdPolicyData;
        private readonly TempFileLogger ldapLogger;
        #endregion

        private static bool getAllPasswordDirectoryAttributes(SearchResultEntry ldapSearchEntry, out Dictionary<PWD_POLICY_ATTRIBS, object> attribs)
        {
            attribs = null;
            if (ldapSearchEntry == null || ldapSearchEntry.Attributes == null || ldapSearchEntry.Attributes.Count == 0)
            {
                return (false);
            }
            
            attribs = new Dictionary<PWD_POLICY_ATTRIBS, object>();
            for (var i = 0; i < PWD_POLICY_ATTRIBS_STR.Length; ++i)
            {
                //Set string key
                string key = PWD_POLICY_ATTRIBS_STR[i];
                //Set proper enumeration key
                var eKey = (PWD_POLICY_ATTRIBS)i;
                //Fetch directory attribute
                var curDirAttrib = (ldapSearchEntry.Attributes.Contains(key)) ? ldapSearchEntry.Attributes[key] : null;
                if (curDirAttrib == null) continue;

                //Get directory attribute value and add to map
                if (curDirAttrib.Name.Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    var dirAttribVal = curDirAttrib[0];
                    if (dirAttribVal != null)
                    {
                        attribs.Add(eKey, dirAttribVal);
                    }
                }
            }
            return (true);

        }
        string lastUsername = null;


        #region Class Accessors and Setter/Add Methods
        public LDAPState State
        {
            get
            {
                return (this.state);
            }
        }

        public string ErrorMessage
        {
            get
            {
                return (this.errorMessage);
            }
        }

        public PasswordPolicyVO PasswordPolicy
        {
            get
            {
                return (this.pwdPolicyData);
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructs the object and sets values to default
        /// </summary>
        public PawnLDAPAccessor()
        {
            this.ldapConnection = null;
            this.ldapServer = string.Empty;
            this.state = LDAPState.DISCONNECTED;
            this.errorMessage = string.Empty;
            this.ldapLoginUsr = string.Empty;
            this.ldapLoginPwd = string.Empty;
            this.ldapUserSearchDN = string.Empty;
            this.ldapUserIdKey = string.Empty;
            this.pwdPolicyData = null;
            var dNow = DateTime.Now;
            var yearStr = dNow.Date.Year.ToString().PadLeft(4, '0');
            var monthStr = dNow.Date.Month.ToString().PadLeft(2, '0');
            var dayStr = dNow.Date.Day.ToString().PadLeft(2, '0');
            var hrStr = dNow.Hour.ToString().PadLeft(2, '0');
            var minStr = dNow.Minute.ToString().PadLeft(2, '0');
            var sb = new StringBuilder(64);
            string curDir = Directory.GetCurrentDirectory();
            sb.Append(curDir + @"\logs\ldap_details_");
            sb.AppendFormat("{0}_{1}_{2}-{3}_{4}.log", yearStr, monthStr, dayStr, hrStr, minStr);
            this.ldapLogger = new TempFileLogger(sb.ToString(), 
                DefaultLoggerHandlers.defaultLogLevelCheckHandler,
                DefaultLoggerHandlers.defaultLogLevelGenerator,
                DefaultLoggerHandlers.defaultLogMessageHandler,
                DefaultLoggerHandlers.defaultLogMessageFormatHandler,
                DefaultLoggerHandlers.defaultDateStampGenerator);
            this.ldapLogger.setLogLevel(LogLevel.DEBUG);
            this.ldapLogger.logMessage(LogLevel.INFO, this, "PAWNLDAPAccessor instance constructed");
        }

        #endregion

        #region Connection Related Methods

        public void InitializeConnection(
            string ldapSrv, string ldapPrt,
            string cxnUsr, string cxnPwd,
            string ldapPwdCN, string ldapUsrDN,
            string userKey)
        {
            
            if (this.state == LDAPState.CONNECTED)
            {
                return;
            }
            if (string.IsNullOrEmpty(cxnUsr) || 
                string.IsNullOrEmpty(cxnPwd) ||
                string.IsNullOrEmpty(ldapSrv) ||
                string.IsNullOrEmpty(ldapPrt) ||
                string.IsNullOrEmpty(ldapPwdCN) ||
                string.IsNullOrEmpty(ldapUsrDN) ||
                string.IsNullOrEmpty(userKey))
            {
                return;
            }
            //Set main login pwd and user
            this.ldapLoginUsr = cxnUsr;
            this.ldapLoginPwd = cxnPwd;
            this.ldapServer = ldapSrv;
            this.ldapPort = ldapPrt;
            this.ldapPwdPolicyCN = ldapPwdCN;
            this.ldapUserSearchDN = ldapUsrDN;
            this.ldapUserIdKey = userKey;

            var ldapPortNum = Utilities.GetIntegerValue(this.ldapPort, LDAP_DEFAULT_PORT);

            //Login to the LDAP server and retrieve password policy
            try
            {
                //Initialize LDAP connection
                this.ldapConnection = new LdapConnection(
                    new LdapDirectoryIdentifier(this.ldapServer, ldapPortNum),
                    new NetworkCredential(cxnUsr, cxnPwd),
                    AuthType.Basic);
                this.ldapConnection.Bind();
                this.state = LDAPState.CONNECTED;

                //Retrieve password policy
                this.getPwdPolicy();
                
            }
            catch (LdapException lEx)
            {
                this.ldapLogger.logMessage(LogLevel.FATAL, this, "Could not connect to the ldap server: {0}:{1} with {2}/{3}: {4}",
                    ldapSrv, ldapPrt, cxnUsr, cxnPwd, lEx.Message);
                this.Disconnect();
            }
            catch (Exception eX)
            {
                this.ldapLogger.logMessage(LogLevel.FATAL, this, "Could not connect to the ldap server: {0}:{1} with {2}/{3}: {4}",
                    ldapSrv, ldapPrt, cxnUsr, cxnPwd, eX.Message);
                this.Disconnect();
            }
            finally
            {
                if (this.state == LDAPState.DISCONNECTED)
                {
                    this.ldapLogger.Dispose();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void getPwdPolicy()
        {
            if (this.state == LDAPState.DISCONNECTED)
            {
                return;
            }

            this.ldapLogger.logMessage(LogLevel.INFO, this, "Retrieving password policy");

            //Clear error message 
            this.errorMessage = string.Empty;

            //Only fetch the password policy if it has not been populated
            if (this.pwdPolicyData == null)
            {
                try
                {
                    //Define search to retrieve password policy
                    var ldapSearch =
                        new SearchRequest(
                            this.ldapPwdPolicyCN,
                            OBJECT_CLASS_FILTER,
                            System.DirectoryServices.Protocols.SearchScope.Subtree);

                    //Execute actual search to retrieve password policy
                    var ldapResponse = 
                        (SearchResponse)this.ldapConnection.SendRequest(ldapSearch);

                    if (ldapResponse == null || ldapResponse.Entries == null || ldapResponse.Entries.Count <= 0)
                    {
                        this.ldapLogger.logMessage(LogLevel.FATAL, this, "Could not find password policy");
                        this.Disconnect();
                        this.ldapLogger.Dispose();
                        return;
                    }
                    

                    //Retreive all password policy settings
                    SearchResultEntry ldapSearchEntry = ldapResponse.Entries[0];
                    this.pwdPolicyData = new PasswordPolicyVO();
                    Dictionary<PWD_POLICY_ATTRIBS, object> attribMap;
                    if (getAllPasswordDirectoryAttributes(ldapSearchEntry, out attribMap))
                    {
                        this.pwdPolicyData.AllowUserChange =
                            Utilities.GetBooleanValue(
                                attribMap[PWD_POLICY_ATTRIBS.PWDALLOWUSERCHANGE], false);
                        this.pwdPolicyData.AttributeName =
                            Utilities.GetStringValue(
                                attribMap[PWD_POLICY_ATTRIBS.PWDATTRIBUTE], string.Empty);
                        this.pwdPolicyData.CheckSyntax =
                            (short)Utilities.GetIntegerValue(
                                       attribMap[PWD_POLICY_ATTRIBS.PWDCHECKSYNTAX], 0);
                        this.pwdPolicyData.ExpireWarningSeconds =
                            Utilities.GetLongValue(
                                attribMap[PWD_POLICY_ATTRIBS.PWDEXPIREWARNING], 0L);
                        this.pwdPolicyData.FailureCountIntervalSeconds =
                            Utilities.GetLongValue(
                                attribMap[PWD_POLICY_ATTRIBS.PWDFAILUREACCTINTERVAL], 0L);
                        this.pwdPolicyData.GraceLoginLimit =
                            Utilities.GetLongValue(
                                attribMap[PWD_POLICY_ATTRIBS.PWDGRACELOGINLIMIT], 0L);
                        this.pwdPolicyData.InHistoryCount =
                            (short)Utilities.GetIntegerValue(
                                       attribMap[PWD_POLICY_ATTRIBS.PWDINHISTORY], 0);
                        this.pwdPolicyData.LockoutDurationMillis =
                            Utilities.GetLongValue(
                                attribMap[PWD_POLICY_ATTRIBS.PWDLOCKOUTDURATION], 0L);
                        this.pwdPolicyData.LockoutEnabled =
                            Utilities.GetBooleanValue(
                                attribMap[PWD_POLICY_ATTRIBS.PWDLOCKOUT], false);
                        this.pwdPolicyData.MaxAgeSeconds =
                            Utilities.GetLongValue(
                                attribMap[PWD_POLICY_ATTRIBS.PWDMAXAGE], 0L);
                        this.pwdPolicyData.MaxFailedLoginAttempts =
                            (short)Utilities.GetIntegerValue(
                                       attribMap[PWD_POLICY_ATTRIBS.PWDMAXFAILURE], 0);
                        this.pwdPolicyData.MaxRepeatedChars =
                            (short)Utilities.GetIntegerValue(
                                       attribMap[PWD_POLICY_ATTRIBS.PWDMAXREPCHARS], 0);
                        this.pwdPolicyData.MinAgeSeconds =
                            Utilities.GetLongValue(
                                attribMap[PWD_POLICY_ATTRIBS.PWDMINAGE], 0L);
                        this.pwdPolicyData.MinAlphaChars =
                            (short)Utilities.GetIntegerValue(
                                       attribMap[PWD_POLICY_ATTRIBS.PWDMINALPHACHARS], 0);
                        this.pwdPolicyData.MinDiffChars =
                            (short)Utilities.GetIntegerValue(
                                       attribMap[PWD_POLICY_ATTRIBS.PWDMINDIFFCHARS], 0);
                        this.pwdPolicyData.MinLength =
                            (short)Utilities.GetIntegerValue(
                                       attribMap[PWD_POLICY_ATTRIBS.PWDMINLENGTH], 0);
                        this.pwdPolicyData.MinOtherChars =
                            (short)Utilities.GetIntegerValue(
                                       attribMap[PWD_POLICY_ATTRIBS.PWDMINOTHERCHARS], 0);
                        this.pwdPolicyData.MustChange =
                            Utilities.GetBooleanValue(
                                attribMap[PWD_POLICY_ATTRIBS.PWDMUSTCHANGE], false);
                        this.pwdPolicyData.SafeModify =
                            Utilities.GetBooleanValue(
                                attribMap[PWD_POLICY_ATTRIBS.PWDSAFEMODIFY], false);
                    }
                }
                catch (Exception eX)
                {
                    this.pwdPolicyData = null;
                    this.ldapLogger.logMessage(LogLevel.FATAL, "Could not find the password policy: {0}",eX.Message);
                }
                finally
                {
                    if (this.pwdPolicyData == null)
                    {
                        this.ldapLogger.logMessage(LogLevel.FATAL, this, "Could not populate the password policy object");
                        this.Disconnect();
                        this.ldapLogger.Dispose();
                    }
                }
            }
        }

        public bool GetUserPassword(string userName, out string pwd, out string dispName)
        {
            pwd = string.Empty;
            dispName = string.Empty;
            try
            {
                this.ldapLogger.logMessage(LogLevel.INFO, this, "Executing password lookup with user id: {0}", userName);
                //search for the user
                var ldapSearch =
                    new SearchRequest(this.ldapUserIdKey + "=" + userName + "," + this.ldapUserSearchDN,
                        OBJECT_CLASS_FILTER,
                        System.DirectoryServices.Protocols.SearchScope.Subtree,
                        "uid", "displayName", this.pwdPolicyData.AttributeName);

                var ldapResponse =
                    (SearchResponse)this.ldapConnection.SendRequest(ldapSearch);

                if (ldapResponse == null || ldapResponse.Entries == null || ldapResponse.Entries.Count <= 0)
                {
                    this.ldapLogger.logMessage(LogLevel.ERROR, this, "Could not find user {0} in the directory specified", userName);
                    this.Disconnect();
                    return (false);
                }

                //If we get here with no exception, then the user name is valid
                this.ldapLogger.logMessage(LogLevel.DEBUG, this, "LDAP Entry found for user {0}", userName);
                SearchResultEntry ldapSearchEntry = ldapResponse.Entries[0];

                //Validate password
                var dirAttrib = ldapSearchEntry.Attributes[this.pwdPolicyData.AttributeName];
                if (dirAttrib != null && dirAttrib.Count > 0)
                {
                    var passwdObj = dirAttrib[0];
                    if (passwdObj != null)
                    {
                        var passwdVal = (string)passwdObj;
                        if (!string.IsNullOrEmpty(passwdVal))
                        {
                            this.ldapLogger.logMessage(LogLevel.DEBUG, this, "Found user {0} password", userName);
                            //Set outgoing password value
                            pwd = passwdVal;
                        }
                    }
                }

                //Get display name
                dirAttrib = ldapSearchEntry.Attributes["displayName"];
                if (dirAttrib != null && dirAttrib.Count > 0)
                {
                    var nameObj = dirAttrib[0];
                    if (nameObj != null)
                    {
                        var nameVal = (string)nameObj;
                        if (!string.IsNullOrEmpty(nameVal))
                        {
                            this.ldapLogger.logMessage(LogLevel.DEBUG,
                                                       this,
                                                       "Found user {0} name = {1}",
                                                       userName,
                                                       nameVal);
                            //Set outgoing value
                            dispName = nameVal;
                        }
                    }
                }
            }
            catch
            {
                this.ldapLogger.logMessage(LogLevel.ERROR, this, "Could not find user {0} password and/or display name", userName);
                return (false);
            }
            return (true);
        }

        /// <summary>
        /// Disconnects the user from the LDAP server
        /// </summary>
        private void Disconnect()
        {
            try
            {
                if (ldapConnection != null && this.state == LDAPState.CONNECTED)
                {
                    this.ldapConnection.Dispose();
                    this.state = LDAPState.DISCONNECTED;
                    this.ldapConnection = null;
                }
            }
            catch (Exception eX)
            {
                this.ldapLogger.logMessage(LogLevel.FATAL, this, "Exception thrown when disconnecting LDAP: " + eX.Message);
            }
            finally
            {
                this.state = LDAPState.DISCONNECTED;
                this.ldapConnection = null;
            }
        }

        /// <summary>
        /// Disconnects the user from the LDAP server
        /// </summary>
        public void Reconnect()
        {
            try
            {
                if (ldapConnection != null && this.state == LDAPState.CONNECTED)
                {
                    this.ldapConnection.Dispose();
                    this.state = LDAPState.DISCONNECTED;
                    this.ldapConnection = null;
                }
                //Set main login pwd and user
/*                this.ldapLoginUsr = cxnUsr;
                this.ldapLoginPwd = cxnPwd;
                this.ldapServer = ldapSrv;
                this.ldapPort = ldapPrt;
                this.ldapPwdPolicyCN = ldapPwdCN;
                this.ldapUserSearchDN = ldapUsrDN;
                this.ldapUserIdKey = userKey;*/
                var ldapPortNum = Utilities.GetIntegerValue(this.ldapPort, LDAP_DEFAULT_PORT);

                //Login to the LDAP server and retrieve password policy
                try
                {
                    //Initialize LDAP connection
                    this.ldapConnection = new LdapConnection(
                        new LdapDirectoryIdentifier(this.ldapServer, ldapPortNum),
                        new NetworkCredential(this.ldapLoginUsr, this.ldapLoginPwd),
                        AuthType.Basic);
                    this.ldapConnection.Bind();
                    this.state = LDAPState.CONNECTED;

                    //Retrieve password policy
                    this.getPwdPolicy();

                }
                catch (LdapException lEx)
                {
                    this.ldapLogger.logMessage(LogLevel.FATAL, this, "Could not connect to the ldap server: {0}:{1} with {2}/{3}: {4}",
                        this.ldapServer, this.ldapPort, this.ldapLoginUsr, this.ldapLoginPwd, lEx.Message);
                    this.Disconnect();
                }
                catch (Exception eX)
                {
                    this.ldapLogger.logMessage(LogLevel.FATAL, this, "Could not connect to the ldap server: {0}:{1} with {2}/{3}: {4}",
                        this.ldapServer, this.ldapPort, this.ldapLoginUsr, this.ldapLoginPwd, eX.Message);
                    this.Disconnect();
                }
                finally
                {
                    if (this.state == LDAPState.DISCONNECTED)
                    {
                        this.ldapLogger.Dispose();
                    }
                }
            }
            catch (Exception eX)
            {
                this.ldapLogger.logMessage(LogLevel.FATAL, this, "Exception thrown when disconnecting LDAP: " + eX.Message);
            }
            finally
            {
                this.state = LDAPState.DISCONNECTED;              
                this.ldapConnection = null;
            }
        }
        #endregion

        #region Class specific Methods
        /// <summary>
        /// Authorizes a user against the LDAP server
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPass"></param>
        /// <param name="attemptCount"></param>
        /// <param name="pwdLastModified"></param>
        /// <param name="pwdHistory"></param>
        /// <param name="displayName"></param>
        /// <param name="lockedOut"></param>
        /// <returns></returns>
        public bool AuthorizeUser(
            string userName, string userPass, 
            ref int attemptCount, 
            out DateTime pwdLastModified,
            out string[] pwdHistory,
            out string displayName,
            out bool lockedOut)
        {

            pwdLastModified = DateTime.Now;
            lockedOut = false;
            pwdHistory = null;
            displayName = string.Empty;

            if (lastUsername == null) lastUsername = userName;
            if (!lastUsername.Equals(userName))
            {
                lastUsername = userName;
                attemptCount = 1;
            }

            if (this.state != LDAPState.CONNECTED)
            {
                this.ldapLogger.logMessage(LogLevel.FATAL, this, "Cannot authenticate user {0}, LDAP server is disconnected", userName);
                return (false);
            }

            if (attemptCount > this.pwdPolicyData.MaxFailedLoginAttempts)
            {
                lockedOut = true;
                this.ldapLogger.logMessage(LogLevel.WARN, this, "User {0} has exceeded the maximum number of login attempts and has been locked out", userName);
                return (false);
            }

            
            try
            {
                this.ldapLogger.logMessage(LogLevel.INFO, this, "Executing search with user id: {0}", userName);
                //search for the user
                var ldapSearch =
                    new SearchRequest(this.ldapUserIdKey + "=" + userName + "," + this.ldapUserSearchDN,
                        OBJECT_CLASS_FILTER,
                        System.DirectoryServices.Protocols.SearchScope.Subtree,
                        "uid", "cn", "displayName", "employeeNumber", "sn", "pwdChangedTime", "pwdHistory","givenName", this.pwdPolicyData.AttributeName);

                var ldapResponse =
                    (SearchResponse)this.ldapConnection.SendRequest(ldapSearch);

                if (ldapResponse == null || ldapResponse.Entries == null || ldapResponse.Entries.Count <= 0)
                {
                    this.ldapLogger.logMessage(LogLevel.ERROR, this, "Could not find user {0} in the directory specified", userName);
                    this.Disconnect();
                    lockedOut = (attemptCount + 1 > this.pwdPolicyData.MaxFailedLoginAttempts);

                    return (false);
                }

                //If we get here with no exception, then the user name is valid
                this.ldapLogger.logMessage(LogLevel.DEBUG, this, "LDAP Entry found for user {0}", userName);
                SearchResultEntry ldapSearchEntry = ldapResponse.Entries[0];

                //Validate password
                var dirAttrib = ldapSearchEntry.Attributes[this.pwdPolicyData.AttributeName];
                if (dirAttrib != null && dirAttrib.Count > 0)
                {
                    var passwdObj = dirAttrib[0];
                    if (passwdObj != null)
                    {
                        var passwdVal = (string)passwdObj;
                        if (!string.IsNullOrEmpty(passwdVal) &&
                            string.Equals(userPass, passwdVal))
                        {
                            //Password is valid
                            this.ldapLogger.logMessage(LogLevel.INFO, this, "LDAP user {0} is now authenticated", userName);

                            //Get last modified date
                            var lastModPwdAttrib = ldapSearchEntry.Attributes["pwdChangedTime"];
                            if (lastModPwdAttrib != null && lastModPwdAttrib.Count > 0)
                            {
                                object lastModTimeObj = lastModPwdAttrib[0];
                                if (lastModTimeObj != null)
                                {
                                    //Build the date time string from the value returned
                                    string pwdModifiedTimeData = lastModTimeObj.ToString();
                                    string pwdModifiedDate = pwdModifiedTimeData.Substring(4, 2) + "/" + pwdModifiedTimeData.Substring(6, 2) + "/" +
                                        pwdModifiedTimeData.Substring(0, 4) + " " + pwdModifiedTimeData.Substring(8, 2) + ":" +
                                        pwdModifiedTimeData.Substring(10, 2) + ":" + pwdModifiedTimeData.Substring(12, 2);
                                    pwdLastModified = Utilities.GetDateTimeValue(pwdModifiedDate);
                                    
                                }
                            }
 
                            var pwdHistoryAttrib = ldapSearchEntry.Attributes["pwdHistory"];
                            if (pwdHistoryAttrib != null && pwdHistoryAttrib.Count > 0)
                            {
                                pwdHistory = new string[pwdHistoryAttrib.Count];
                                for (var i = 0; i < pwdHistoryAttrib.Count; ++i)
                                {
                                    //36 to 24 is the actual encrypted value of old password
                      
                                    var curPwdHistAttribObj = pwdHistoryAttrib[i].ToString().Substring(36, 24);

                                    pwdHistory[i] = !string.IsNullOrEmpty(curPwdHistAttribObj) ? curPwdHistAttribObj : null;
                                }
                            }
                            //Get Display Name
                            var dispName = ldapSearchEntry.Attributes["displayName"];
                            if (dispName != null && dispName.Count > 0)
                            {
                                displayName = dispName[0].ToString();
                            }
                            //Get first and last name if display name is empty
                            if (string.IsNullOrEmpty(displayName))
                            {
                                var firstName = ldapSearchEntry.Attributes["givenName"];
                                var lastName = ldapSearchEntry.Attributes["sn"];
                                if (firstName.Count > 0 && lastName.Count > 0)
                                displayName = firstName[0] + " " + lastName[0];
                            }

                            return (true);
                        }
                    }
                }
            }
            catch (LdapException lEx)
            {
                this.ldapLogger.logMessage(LogLevel.FATAL, this, "LDAPException thrown while authenticating user {0}: {1}", userName, lEx.Message);
                lockedOut = (attemptCount + 1 > this.pwdPolicyData.MaxFailedLoginAttempts);
                return (false);
            }
            catch (Exception eX)
            {
                this.ldapLogger.logMessage(LogLevel.FATAL, this, "Exception thrown during LDAP authentication. Cause: " + eX.Message);
                lockedOut = (attemptCount + 1 > this.pwdPolicyData.MaxFailedLoginAttempts);

                return (false);
            }

            lockedOut = (attemptCount + 1 > this.pwdPolicyData.MaxFailedLoginAttempts);
            return (false);
        }

        /// <summary>
        /// For the username passed, change the password to the new value passed in newpassword
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ChangePassword(
             string userName,
             string newPassword
             )
        {
            string attributeName = this.PasswordPolicy.AttributeName;
            string attributeValue = newPassword;

            var modRequest = new ModifyRequest(
                 userName, DirectoryAttributeOperation.Replace,
                 attributeName, attributeValue);
 
            try
            {
                DirectoryResponse response = this.ldapConnection.SendRequest(modRequest);
                if (string.IsNullOrEmpty(response.ErrorMessage))
                    return true;
            }
            catch (LdapException ex)
            {
                this.ldapLogger.logMessage(LogLevel.FATAL, this, "LDAPException thrown while updating password for user {0}: {1}", userName, ex.Message);
                return (false);
            }
            catch (DirectoryOperationException dex)
            {
                this.ldapLogger.logMessage(LogLevel.FATAL, this, "Directory operation exception thrown while updating password for user {0}: {1}", userName, dex.Message);
                return (false);

            }
            
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <param name="displayName"></param>
        /// <param name="employeeId"></param>
        /// <param name="employeeType"></param>
        /// <param name="errMessage"></param>
        /// <returns></returns>
        public bool CreateUser(
            string userName,
            string userPassword,
            string displayName,
            string employeeId,
            string employeeType,
            out string errMessage)
        {
            errMessage = string.Empty;
            var dn = string.Format("uid={0},ou=People,dc=cashamerica", userName);
            try
            {
                if (ldapConnection != null)
                {
                    var dAttrib = new DirectoryAttribute("objectClass")
                                  {
                                      "top", "person", "organizationalPerson", "inetOrgPerson"
                                  };
                    var dGivenName = new DirectoryAttribute("givenName", userName);
                    var dSn = new DirectoryAttribute("sn", userName);
                    var dCn = new DirectoryAttribute("cn", userName);
                    var dPwd = new DirectoryAttribute("userPassword", userPassword);
                    var dDisp = new DirectoryAttribute("displayName", displayName);
                    var dEmpId = new DirectoryAttribute("employeeNumber", employeeId);
                    var dEmpType = new DirectoryAttribute("employeeType", employeeType);
                    var addRequest = 
                        new AddRequest(
                            dn, 
                            dAttrib, 
                            dGivenName, 
                            dSn, 
                            dCn, 
                            dPwd, 
                            dDisp, 
                            dEmpId,
                            dEmpType);
                    DirectoryResponse response = ldapConnection.SendRequest(addRequest);
                    errMessage = response.ErrorMessage;
                    if (string.IsNullOrEmpty(response.ErrorMessage))
                    {
                        return true;
                    }
                }
            }
            catch (LdapException ex)
            {
                this.ldapLogger.logMessage(LogLevel.FATAL, this, "LDAPException thrown while creating user" + ex.Message);
                errMessage = ex.Message;
                return (false);
            }
            catch (DirectoryOperationException dex)
            {
                this.ldapLogger.logMessage(LogLevel.FATAL, this, "Directory operation exception thrown while creating user {0}", dex.Message);
                errMessage = dex.Message;
                return (false);
            }
            catch(Exception eX)
            {
                this.ldapLogger.logMessage(LogLevel.ERROR, this, "Exception thrown while adding user to LDAP: {0}", eX.Message);
                errMessage = eX.Message;
            }
            
            return false;

        }
 
        #endregion


        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                this.state = LDAPState.DISCONNECTED;
                this.ldapConnection.Dispose();
                this.ldapConnection = null;
            }
            catch (Exception eX)
            {
                this.errorMessage = "Exception thrown while disposing accessor:" + eX.Message;
            }
            finally
            {
                this.ldapConnection = null;
                this.ldapLogger.Dispose();
            }
        }

        #endregion
    }
}
