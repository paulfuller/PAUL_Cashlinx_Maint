using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Database.Couch.Impl;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Config;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.String;

namespace Common.Controllers.Application
{
    public sealed class GlobalDataAccessor : MarshalByRefObject, IDisposable
    {
        #region Singleton Fields And Methods
        // ReSharper disable InconsistentNaming
        private static readonly object mutex = new object();
        static readonly GlobalDataAccessor instance = new GlobalDataAccessor();
        // ReSharper restore InconsistentNaming
        static GlobalDataAccessor()
        {
            
        }

        public override object InitializeLifetimeService()
        {
            return (null);
        }

        public static GlobalDataAccessor Instance
        {
            get { return (instance); }
        }

        #endregion

        #region Dispose Fields And Methods

        public void Dispose()
        {
            //Do nothing
        }

        #endregion

        #region Private Fields
        private DesktopSession desktopSession;
        #endregion

        #region Property Methods

        public DesktopSession DesktopSession
        {
            get { return (this.desktopSession); }
            set { if (value != null) this.desktopSession = value; }
        }

        public bool IsDataAccessorValid()
        {
            return OracleDA != null && OracleDA.Initialized;
        }

        public OracleDataAccessor OracleDA
        {
            get; 
            set;
        }

        private SiteId currentSiteId;
        public SiteId CurrentSiteId
        {
            get { return (this.currentSiteId); }
            set { currentSiteId = value; }
        }

        public SecuredCouchConnector CouchDBConnector
        {
            get; set;
        }

        public DateTime DatabaseTime
        {
            get; set;
        }

        public bool ExceptionHandlerBoxEnabled
        {
            get; set;
        }

        public string CashlinxPDAURL { get; set; }

        public Libraries.Objects.Business.ReleaseFingerprintsInfo FingerPrintRelaseAuthorizationInfo { get; set; }
        #endregion

        #region Default Exception Handler
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool exceptionCallbackMethod()
        {
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
                if (GlobalDataAccessor.Instance.ExceptionHandlerBoxEnabled)
                {
                    MessageBox.Show(sF.ToString(), "Error(s) Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return (true);
            }
            return (false);
        }


        #endregion

        #region Security Config Init Method

        /// <summary>
        /// Initializes common secured machine names and ports from PAWNSEC as well as some common init tasks
        /// - Exception Handler
        /// - Oracle connection
        /// - Couch service
        /// - Database time
        /// - Shop date & time
        /// - Site Id
        /// - Main application logger
        /// - Cashlinx PDA URL
        /// </summary>
        /// <param name="dSession"> </param>
        /// <param name="confRef"></param>
        /// <param name="appName"> </param>
        /// <param name="auditLogEnabled"> </param>
        /// <param name="exceptionHandler"> </param>
        /// <param name="multiConnect"></param>
        /// <param name="keyedConnect"></param>
        /// <param name="key"></param>
        /// <param name="auditLogEnabledChangeHandler"> </param>
        /// <param name="auditLogHandler"> </param>
        public void Init(
            DesktopSession dSession,
            EncryptedConfigContainer confRef, 
            string appName,
            AuditLogEnabledChangeHandler auditLogEnabledChangeHandler,
            AuditLogHandler auditLogHandler,
            bool auditLogEnabled,
            Func<bool> exceptionHandler = null,
            bool multiConnect = false, 
            bool keyedConnect = false, 
            string key = null)
        {
            //Get DesktopSession instance
            this.desktopSession = dSession;

            if (this.desktopSession == null)
            {
                throw new ApplicationException("DesktopSession is null! Exiting!");
            }

            //Setup exception handler
            var exHandler = BasicExceptionHandler.Instance;
            exHandler.PrintStackTrace = true;
            if (exceptionHandler != null)
            {
                exHandler.setExceptionCallback(exceptionHandler);
            }
            else
            {
                exHandler.setExceptionCallback(exceptionCallbackMethod);
            }

            //Get client config for DB connection            
            var clientConfigDB = confRef.GetOracleDBService();
            this.OracleDA = new OracleDataAccessor(
                confRef.DecryptValue(clientConfigDB.DbUser),
                confRef.DecryptValue(clientConfigDB.DbUserPwd),
                confRef.DecryptValue(clientConfigDB.Server),
                confRef.DecryptValue(clientConfigDB.Port),
                confRef.DecryptValue(clientConfigDB.AuxInfo),
                confRef.DecryptValue(clientConfigDB.Schema),
                (uint)confRef.ClientConfig.StoreConfiguration.FetchSizeMultiplier,
                multiConnect,
                keyedConnect,
                key);

            if (!this.OracleDA.Initialized)
            {
                throw new ApplicationException("Oracle data accessor is not initialized.  Cannot interact with the database. Exiting!");
            }

            //Get client config for Couch connection
            var clientDocDb = confRef.GetCouchDBService();
            if (clientDocDb != null)
            {
                this.CouchDBConnector = new SecuredCouchConnector(
                        confRef.DecryptValue(clientDocDb.Server),
                        confRef.DecryptValue(clientDocDb.Port),
                        DesktopSession.SSL_PORT,
                        confRef.DecryptValue(clientDocDb.Schema),
                        confRef.DecryptValue(clientDocDb.DbUser),
                        confRef.DecryptValue(clientDocDb.DbUserPwd),
                        DesktopSession.SECURE_COUCH_CONN);
            }
            else
            {
                throw new ApplicationException("Cannot initialize secured document server connection! Exiting!");
            }

            //Retrieve database time
            DateTime time;
            ShopProcedures.ExecuteGetDatabaseTime(this.OracleDA, out time);
            this.DatabaseTime = time;

            //Set shop date time
            var storeConf = confRef.ClientConfig.StoreConfiguration;
            ShopDateTime.Instance.setOffsets(0, 0, 0, 0, 0, 0, 0);
            ShopDateTime.Instance.SetDatabaseTime(this.DatabaseTime);
            ShopDateTime.Instance.SetPawnSecOffsetTime(storeConf);

            //Initialize the site
            this.currentSiteId = new SiteId();
            this.currentSiteId.StoreNumber = confRef.ClientConfig.StoreSite.StoreNumber;

            //Load store information            
            LoadStoreData(currentSiteId.StoreNumber);

            //Finalize site info population
            this.currentSiteId.TerminalId = confRef.ClientConfig.ClientConfiguration.WorkstationId;
            this.currentSiteId.Alias = confRef.ClientConfig.StoreSite.Alias;
            this.currentSiteId.Company = confRef.ClientConfig.StoreSite.CompanyNumber;
            this.currentSiteId.CompanyNumber = confRef.ClientConfig.StoreSite.CompanyNumber;
            this.currentSiteId.Date = ShopDateTime.Instance.ShopDate;
            this.currentSiteId.State = confRef.ClientConfig.StoreSite.State;
            this.currentSiteId.LoanAmount = 0.00M;

            try
            {
                //Initialize the logger
                this.initializeLogger(appName);

                //Initialize audit logger
                this.initializeAuditLogger(auditLogEnabledChangeHandler, auditLogHandler, auditLogEnabled);

            }
            catch (Exception eX)
            {
                throw new ApplicationException("One or both primary loggers failed to initialize!", eX);
            }

            //Retrieve URL
            var pdaUrlObj = confRef.GetURL();
            if (pdaUrlObj != null)
            {
                this.CashlinxPDAURL = confRef.DecryptValue(pdaUrlObj.AuxInfo);
            }
            else
            {
                throw new ApplicationException("Cannot determine CashlinxPDA URL! Exiting!");
            }
        }


        /// <summary>
        /// Initialize and open the logger
        /// </summary>
        private void initializeLogger(string appName)
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
                var fileLogger = FileLogger.Instance;
                //Logger is enabled if and only if the log level is not empty/null AND
                //the internal log enabled flag is set to "true"
                const bool logEnabled = true;
                if (logEnabled)
                {
                    var logFileName = string.Format("{0}\\{1}_{2}", dirInfo, appName, FileLogger.FILENAME);
                    fileLogger.setEnabled(logEnabled);
                    fileLogger.initializeLogger(logFileName,
                                                     DefaultLoggerHandlers.defaultLogLevelCheckHandler,
                                                     DefaultLoggerHandlers.defaultLogLevelGenerator,
                                                     DefaultLoggerHandlers.defaultDateStampGenerator,
                                                     DefaultLoggerHandlers.defaultLogMessageHandler,
                                                     DefaultLoggerHandlers.defaultLogMessageFormatHandler);

                    fileLogger.setLogLevel(confRef.ClientConfig.ClientConfiguration.LogLevel);
                    var asteriskString = StringUtilities.fillString("*", 150);
                    fileLogger.logMessage(LogLevel.INFO, this, asteriskString);
                    fileLogger.logMessage(LogLevel.INFO, this, "GlobalDataAccessor Initialized at {0} - {1} for {2}",
                                               DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), appName + ' ' + confRef.DecryptValue(confRef.GetOracleDBService().Name));
                    fileLogger.logMessage(LogLevel.INFO, this, asteriskString);
                    fileLogger.flush();
                }
            }
            catch (Exception eX)
            {
                //Not necessary to throw a major exception, just dump the information to the console for now
                //throw new ApplicationException("Cannot open log file for writing: " + eX.Message, eX);
                System.Console.WriteLine("An exception was thrown while opening the log file for writing: {0} {1}", eX.Message, eX);
                throw new ApplicationException("Cannot initilaize the main application logger", eX);
            }
        }

        #endregion


        public GlobalDataAccessor()
        {
            this.ExceptionHandlerBoxEnabled = false;
        }

        private void initializeAuditLogger(
            AuditLogEnabledChangeHandler auditLogEnabledChangeHandler,
            AuditLogHandler auditLogHandler,
            bool isEnabled)
        {
            try
            {
                var aLogger = AuditLogger.Instance;
                if (aLogger == null)throw new Exception("AuditLogger Instance is null");
                aLogger.SetAuditLogEnabledChangeHandler(auditLogEnabledChangeHandler);
                if (auditLogEnabledChangeHandler == null)throw new Exception("AuditLogEnabledChangeHandler is null");
                aLogger.SetAuditLogHandler(auditLogHandler);
                if (auditLogHandler == null)throw new Exception("AuditLogHandler is null");
                aLogger.SetEnabled(isEnabled);
            }
            catch (Exception eX)
            {                
                throw new ApplicationException("Cannot initialize AuditLogger", eX);
            }
        }

        private void LoadStoreData(string storeNumber)
        {
            try
            {
                //data table to load store info
                string sErrorCode;
                string sErrorMessage;
                ShopProcedures.ExecuteGetStoreInfo(this.OracleDA, storeNumber, ref currentSiteId,
                                                   out sErrorCode, out sErrorMessage);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException(
                    "LoadStoreData failed", 
                    new ApplicationException("Load store data failed", ex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool beginTransactionBlock()
        {
            if (this.OracleDA == null || this.OracleDA.Initialized != true)
            {
                throw new SystemException("Oracle database accessor is not valid");
            }
            if (!this.OracleDA.StartTransactionBlock(IsolationLevel.ReadCommitted, null))
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                   "Cannot start a database transaction block");
                }
                var aEx = new ApplicationException("Cannot begin database transaction block");
                BasicExceptionHandler.Instance.AddException("Cannot begin database transaction block: ", aEx);
                throw aEx;
            }

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eType"></param>
        public bool endTransactionBlock(EndTransactionType eType)
        {
            if (this.OracleDA == null || this.OracleDA.Initialized != true)
            {
                throw new SystemException("Oracle data accessor is invalid");
            }
            switch (eType)
            {
                case EndTransactionType.COMMIT:
                    if (!this.OracleDA.commitTransactionBlock(null))
                    {
                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, "GlobalDataAccessor",
                                                           string.Format("Cannot commit current database transaction block...rolling back changes ({0}, {1})", this.OracleDA.ErrorCode, this.OracleDA.ErrorDescription));
                        }
                        var aEx = new ApplicationException("Cannot commit database transaction block");
                        BasicExceptionHandler.Instance.AddException("Cannot perform database commit", aEx);
                        throw aEx;
                    }
                    break;
                case EndTransactionType.ROLLBACK:
                    if (!this.OracleDA.rollbackTransactionBlock(null))
                    {
                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, "GlobalDataAccessor",
                                                           "Cannot rollback current database transaction block");
                        }
                        var aEx = new ApplicationException("Cannot rollback database transaction block");
                        BasicExceptionHandler.Instance.AddException("Cannot rollback database transaction block", aEx);
                        throw aEx;
                    }
                    break;
            }

            return (true);
        }

        public bool InTransactionBlock(string key = null)
        {
            if (GlobalDataAccessor.Instance.OracleDA == null || GlobalDataAccessor.Instance.OracleDA.Initialized != true)
            {
                var errMsg = string.Format("Oracle data accessor is invalid or not initialized!");
                if (FileLogger.Instance.IsLogFatal)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                }
                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                return (false);
            }
            if (!GlobalDataAccessor.Instance.OracleDA.inTransactionBlock(key))
            {
                if (FileLogger.Instance.IsLogInfo)
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, this, "Not inside a transaction block");
                }
                return (false);
            }
            //If we get here, we are inside a transaction block
            return (true);
        }

    }
}
