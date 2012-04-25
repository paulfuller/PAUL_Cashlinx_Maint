using System;
using System.Text;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Oracle.DataAccess.Client;
using System.Data;

namespace Common.Controllers.Database.Oracle
{
    /*
     * Utilizing thread safe lock free singleton strategy 
     * Singleton implementation strategy from http://www.yoda.arachsys.com/csharp/singleton.html
     */
    /// <summary>
    /// 
    /// </summary>
    sealed class OracleDbConnection : MarshalByRefObject, IDisposable
    {
        #region Singleton Data and Methods 
        // ReSharper disable InconsistentNaming
        private static readonly object mutexObj = new object();
        static readonly OracleDbConnection instance = new OracleDbConnection();
        // ReSharper restore InconsistentNaming
        static OracleDbConnection()
        {
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public static OracleDbConnection Instance
        {
            get
            {
                return (instance);
            }
        }
        #endregion

        #region Static String Constants
        public const string DS_WILDCARD = "?";
        public readonly static string DS_USERID = "user id=" + DS_WILDCARD + ";";
        public readonly static string DS_PASSWORD = "password=" + DS_WILDCARD + ";";
        public const string DS_DATASRC = "data source=";
        public const string SLA = "/";
        public const string CLN = ":";
        public readonly static string STATEMENT_CACHE_ON = "Statement Cache Size=1;";
        public readonly static string STATEMENT_CACHE_OFF = "Statement Cache Size=0;";
        public readonly static bool STATEMENT_CACHING_FLAG = true;
        #endregion



        #region Enumerations

        public enum Status
        {
            INITIALIZED,
            CONNECTED,
            DISCONNECTED
        };

        #endregion

        #region Private Class Fields
        private OracleConnection oracleConnection;
        private string userName;
        private string password;
        private string databaseHost;
        private string databasePort;
        private string databaseService;
        private string databaseSchema;
        private Status status;
        private string dataService;
        private bool transactionBlock;
        private OracleTransaction transactionObj;
        private TempFileLogger auxLogger;
        #endregion 

        /// <summary>
        /// Generates the data service name utilized to
        /// connect to the database
        /// </summary>
        /// <returns>Success of data service string creation</returns>
        private bool generateDataService()
        {
            if (string.IsNullOrEmpty(this.userName) ||
                string.IsNullOrEmpty(this.password) ||
                string.IsNullOrEmpty(this.databaseHost) ||
                string.IsNullOrEmpty(this.databasePort) ||
                string.IsNullOrEmpty(this.databaseSchema) ||
                string.IsNullOrEmpty(this.databaseService))
            {
                return (false);
            }

            //Build data service string
            var sbuilder = new StringBuilder();
            sbuilder.Append(DS_USERID.Replace(DS_WILDCARD, this.userName));
            sbuilder.Append(DS_PASSWORD.Replace(DS_WILDCARD, this.password));
            sbuilder.Append(DS_DATASRC);
            sbuilder.Append(this.databaseHost);
            sbuilder.Append(CLN);
            sbuilder.Append(this.databasePort);
            sbuilder.Append(SLA);
            sbuilder.Append(this.databaseService);

            //Set the data service
            this.dataService = sbuilder.ToString();

            return (true);
        }

        /// <summary>
        /// Current database connection status
        /// </summary>
        public Status DBStatus
        {
            get
            {
                return (status);
            }
        }

        public TempFileLogger AuxLogger
        {
            set
            {
                if (this.auxLogger == null)
                {
                    this.auxLogger = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msgFmt"></param>
        /// <param name="vars"></param>
        private void log(LogLevel level, string msgFmt, params object[] vars)
        {
            FileLogger fileLogger = FileLogger.Instance;
            if (fileLogger != null && fileLogger.isLogLevel(level))
            {
                fileLogger.logMessage(level, this, msgFmt, vars);
            }
            if (this.auxLogger != null && auxLogger.isLogLevel(level))
            {
                auxLogger.logMessage(level, this, msgFmt, vars);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msg"></param>
        private void log(LogLevel level, string msg)
        {
            FileLogger fileLogger = FileLogger.Instance;
            if (fileLogger != null && fileLogger.isLogLevel(level))
            {
                fileLogger.logMessage(level, this, msg);
            }
            if (this.auxLogger != null && auxLogger.isLogLevel(level))
            {
                auxLogger.logMessage(level, this, msg);
            }
        }

        private bool isloglevel(LogLevel level)
        {
            var fileLogger = FileLogger.Instance;
            bool allowedFileLogger = true, allowedAuxLogger = true;
            if (fileLogger != null)
            {
                allowedFileLogger = fileLogger.isLogLevel(level);
            }
            if (this.auxLogger != null)
            {
                allowedAuxLogger = auxLogger.isLogLevel(level);
            }

            return (allowedAuxLogger && allowedFileLogger);
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public OracleDbConnection()
        {
            lock(mutexObj)
            {
                this.oracleConnection = null;
                this.userName = string.Empty;
                this.password = string.Empty;
                this.databaseHost = string.Empty;
                this.databasePort = string.Empty;
                this.databaseService = string.Empty;
                this.databaseSchema = string.Empty;
                this.status = Status.DISCONNECTED;
                this.transactionBlock = false;
                this.transactionObj = null;
                this.auxLogger = null;
            }
        }

        public bool startTransactionBlock(IsolationLevel isoLevel)
        {
            //Starting transaction block with retry set to false
            return (startTransactionBlock(isoLevel, false));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isoLevel"></param>
        /// <param name="isRetry"></param>
        /// <returns></returns>
        private bool startTransactionBlock(IsolationLevel isoLevel, bool isRetry)
        {
            bool rt = false;
            bool alreadyOpenBlock = false;
            lock (mutexObj)
            {
                if (this.oracleConnection != null)
                {
                    if (this.transactionBlock == false)
                    {
                        try
                        {
                            this.transactionObj = this.oracleConnection.BeginTransaction();
                            this.transactionBlock = true;
                            rt = true;
                        }
                        catch (OracleException oEx)
                        {
                            string errMsg = string.Format("Cannot start transaction block - OracleException thrown - error code: {0}, details: {1}, {2}",
                                oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No stack trace");
                            if (this.isloglevel(LogLevel.FATAL))
                            {
                                this.log(LogLevel.FATAL, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg,
                                new ApplicationException(errMsg, oEx));
                        }
                        catch(Exception eX)
                        {
                            string errMsg = string.Format("Cannot start transaction block - Exception thrown: {0}, {1}",
                                eX.Message, eX.StackTrace ?? "No stack trace");
                            if (this.isloglevel(LogLevel.FATAL))
                            {
                                this.log(LogLevel.FATAL, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));
                            
                        }
                        //Check flag status and output appropriate log message
                        if (rt)
                        {
                            if (this.isloglevel(LogLevel.INFO))
                            {
                                this.log(LogLevel.INFO, "Started transaction block with isolation level = {0}", isoLevel);
                            }
                        }
                        else
                        {
                            string errMsg = string.Format("Cannot start transaction block - Return value false");
                            if (this.isloglevel(LogLevel.FATAL))
                            {
                                this.log(LogLevel.FATAL, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        }
                    }
                    else
                    {
                        //Transaction block is not false
                        string errMsg = string.Format("Cannot start transaction block - transaction flag for this connection is {0} - transaction already in progress", this.transactionBlock);
                        if (this.isloglevel(LogLevel.FATAL))
                        {
                            log(LogLevel.FATAL, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        alreadyOpenBlock = true;
                    }
                }
                else
                {
                    //Connection is null
                    string errMsg = string.Format("Cannot start transaction block - OracleConnection object for this connection is null");
                    if (this.isloglevel(LogLevel.FATAL))
                    {
                        this.log(LogLevel.FATAL, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));                        
                }
            }

            if (!rt && alreadyOpenBlock && !isRetry)
            {
                if (this.isloglevel(LogLevel.WARN))
                {
                    this.log(LogLevel.WARN,
                             "Attempting to restart transaction block - first rolling back block in progress then will attempt to start new again");
                }
                //See if the problem can be solved by rolling any potential open transaction blocks back
                //Rollback any transaction that may be in progress
                if (this.rollbackTransactionBlock())
                {
                    if (this.startTransactionBlock(isoLevel, true))
                    {
                        rt = true;
                        if (this.isloglevel(LogLevel.INFO))
                        {
                            this.log(LogLevel.INFO, "Attempt to restart transaction block successful");
                        }
                    }
                    else
                    {
                        string errMsg = string.Format("Cannot restart transaction block - attempt to start a new transaction block failed");
                        if (this.isloglevel(LogLevel.FATAL))
                        {
                            this.log(LogLevel.FATAL, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    }
                }
                else
                {
                    string errMsg = string.Format("Cannot restart transaction block - initial rollback attempt failed");
                    if (this.isloglevel(LogLevel.FATAL))
                    {
                        this.log(LogLevel.FATAL, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                }
            }

            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool commitTransactionBlock()
        {
            var rt = false;
            lock (mutexObj)
            {
                if (this.oracleConnection != null)
                {
                    if (this.transactionBlock && this.transactionObj != null)
                    {
                        try
                        {
                            this.transactionObj.Commit();
                            rt = true;
                            this.transactionBlock = false;
                            this.transactionObj = null;
                        }
                        catch(OracleException oEx)
                        {
                            string errMsg =
                                    string.Format(
                                                  "Cannot commit transaction block - OracleException thrown - error code: {0}, details: {1}, {2}",
                                                  oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No stack trace");
                            if (this.isloglevel(LogLevel.FATAL))
                            {
                                this.log(LogLevel.FATAL, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, oEx));
                        }
                        catch(Exception eX)
                        {
                            string errMsg = string.Format("Cannot commit transaction block - Exception thrown: {0}, {1}", eX.Message,
                                                          eX.StackTrace ?? "No stack trace");
                            if (this.isloglevel(LogLevel.FATAL))
                            {
                                this.log(LogLevel.FATAL, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));

                        }

                        //Check flag status and output appropriate log message
                        if (rt)
                        {
                            if (this.isloglevel(LogLevel.INFO))
                            {
                                this.log(LogLevel.INFO, "Successfully committed transaction block");
                            }
                        }
                        else
                        {
                            string errMsg = string.Format("Cannot commit transaction block - Return value false");
                            if (this.isloglevel(LogLevel.FATAL))
                            {
                                this.log(LogLevel.FATAL, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        }
                    }
                    else
                    {
                        //Transaction block is false OR transaction object is null
                        string errMsg = string.Format("Cannot commit transaction block - transaction flag for this connection is {0} or transaction object is null", this.transactionBlock);
                        if (this.isloglevel(LogLevel.FATAL))
                        {
                            log(LogLevel.FATAL, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        
                    }
                }
                else
                {
                    //Connection is null
                    string errMsg = string.Format("Cannot commit transaction block - OracleConnection object for this connection is null");
                    if (this.isloglevel(LogLevel.FATAL))
                    {
                        this.log(LogLevel.FATAL, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                }
            }
            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool rollbackTransactionBlock()
        {
            var rt = false;
            lock (mutexObj)
            {
                if (this.oracleConnection != null)
                {

                    if (this.transactionBlock && this.transactionObj != null)
                    {
                        try
                        {
                            this.transactionObj.Rollback();
                            rt = true;
                            this.transactionBlock = false;
                            this.transactionObj = null;
                        }
                        catch (OracleException oEx)
                        {
                            string errMsg = string.Format("Cannot rollback transaction block - OracleException thrown - error code: {0}, details: {1}, {2}",
                                oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No stack trace");
                            if (this.isloglevel(LogLevel.FATAL))
                            {
                                this.log(LogLevel.FATAL, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, oEx));
                        }
                        catch (Exception eX)
                        {
                            string errMsg = string.Format("Cannot rollback transaction block - Exception thrown: {0}, {1}",
                                eX.Message, eX.StackTrace ?? "No stack trace");
                            if (this.isloglevel(LogLevel.FATAL))
                            {
                                this.log(LogLevel.FATAL, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));

                        }
                        //Check flag status and output appropriate log message
                        if (rt)
                        {
                            if (this.isloglevel(LogLevel.INFO))
                            {
                                this.log(LogLevel.INFO, "Successfully rolled back transaction block");
                            }
                        }
                        else
                        {
                            string errMsg = string.Format("Cannot rollback transaction block - Return value false");
                            if (this.isloglevel(LogLevel.FATAL))
                            {
                                this.log(LogLevel.FATAL, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        }
                    }
                    else
                    {
                        //Transaction block is false OR transaction object is null
                        string errMsg = string.Format("Cannot rollback transaction block - transaction flag for this connection is {0} or transaction object is null", this.transactionBlock);
                        if (this.isloglevel(LogLevel.FATAL))
                        {
                            log(LogLevel.FATAL, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));

                    }
                }
                else
                {
                    //Connection is null
                    string errMsg = string.Format("Cannot rollback transaction block - OracleConnection object for this connection is null");
                    if (this.isloglevel(LogLevel.FATAL))
                    {
                        this.log(LogLevel.FATAL, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                }
            }
            return (rt);
        }

        /// <summary>
        /// Determines true/false whether within transaction block or not
        /// </summary>
        /// <returns></returns>
        public bool inTransactionBlock()
        {
            var rt = false;
            lock(mutexObj)
            {
                if (this.oracleConnection != null)
                {
                    if (this.transactionBlock && this.transactionObj != null)
                    {
                        if (this.isloglevel(LogLevel.INFO))
                        {
                            this.log(LogLevel.INFO, "Transaction block status - Oracle transaction block is in progress");
                        }
                        rt = true;
                    }
                    else if (!this.transactionBlock)
                    {
                        if (this.isloglevel(LogLevel.INFO))
                        {
                            this.log(LogLevel.INFO, "Transaction block status - Oracle transaction block is not in progress");
                        }
                    }
                    else if (this.transactionObj == null)
                    {
                        //Transaction block is false OR transaction object is null
                        string errMsg = string.Format("Cannot determine transaction block status - transaction object is null");
                        if (this.isloglevel(LogLevel.FATAL))
                        {
                            this.log(LogLevel.FATAL, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    }
                }
                else
                {
                    //Connection is null
                    string errMsg = string.Format("Cannot determine transaction block status - OracleConnection object for this connection is null");
                    if (this.isloglevel(LogLevel.FATAL))
                    {
                        this.log(LogLevel.FATAL, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                }
            }
            return (rt);
        }

        /// <summary>
        /// Initialize the database connection
        /// </summary>
        /// <param name="userNm">User name credential</param>
        /// <param name="passwd">Password credential</param>
        /// <param name="dbaseHost">Host name of the database</param>
        /// <param name="dbasePort">Service port on the database</param>
        /// <param name="dbaseService">Service name on the database</param>
        /// <param name="dbaseSchema">Schema name to access in the database</param>
        /// <returns></returns>
        public bool initialize(
            string userNm,
            string passwd,
            string dbaseHost,
            string dbasePort,
            string dbaseService,
            string dbaseSchema)
        {
            var rt = false;
            lock (mutexObj)
            {
                //Get data
                this.userName = userNm;
                this.password = passwd;
                this.databaseHost = dbaseHost;
                this.databasePort = dbasePort;
                this.databaseService = dbaseService;
                this.databaseSchema = dbaseSchema;
                this.transactionBlock = false;
                this.transactionObj = null;

                if (this.status == Status.DISCONNECTED)
                {
                    if (this.generateDataService())
                    {
                        //this.appendStatementCaching();
                        this.status = Status.INITIALIZED;
                        rt = true;
                    }
                }
                else if (this.status == Status.INITIALIZED)
                {
                    rt = true;
                }
                else if (this.status == Status.CONNECTED)
                {
                    rt = true;
                }
            }
            return (rt);
        }

/*
        /// <summary>
        /// Add proper statement cache details to the data service string
        /// </summary>
        private void appendStatementCaching(StringBuilder sb)
        {
            if (sb == null)
            {
                return;
            }

            if (STATEMENT_CACHING_FLAG == true)
            {
                sb.Append(STATEMENT_CACHE_ON);
            }
            else
            {
                sb.Append(STATEMENT_CACHE_OFF);
            }
        }
*/

/*
        private void appendPoolingOptions(StringBuilder sb)
        {
            if (sb == null)
            {
                return;
            }



        }
*/

        /// <summary>
        /// Open the connection to the database if and only if the connection is
        /// not already open
        /// </summary>
        /// <returns>Success of connection open operation</returns>
        public bool connect()
        {
            bool rt = false;
            lock (mutexObj)
            {
                if (this.status == Status.INITIALIZED || this.status == Status.DISCONNECTED)
                {
                    //Create Oracle connection object
                    if (this.oracleConnection == null)
                    {
                        try
                        {
                            this.oracleConnection = new OracleConnection(this.dataService);
                            if (this.isloglevel(LogLevel.INFO))
                            {
                                this.log(LogLevel.INFO, "Created oracle connection object for database {0}", this.databaseHost);
                            }
                            //this.oracleConnection.ConnectionTimeout = 
                        }
                        catch (OracleException oEx)
                        {
                            string errMsg = string.Format("Cannot create oracle connection object for database {0} - OracleException thrown - error code: {1}, details: {2}, {3}",
                                this.databaseHost, oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No stack trace");
                            if (this.isloglevel(LogLevel.FATAL))
                            {
                                this.log(LogLevel.FATAL, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, oEx));
                        }
                        catch(Exception eX)
                        {
                            string errMsg = string.Format("Cannot create oracle connection object for database {0} - Exception thrown: {1}, {2}",
                                this.dataService, eX.Message, eX.StackTrace ?? "No stack trace");
                            if (this.isloglevel(LogLevel.FATAL))
                            {
                                this.log(LogLevel.FATAL, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));
                        }
                    }

                    if (this.oracleConnection != null)
                    {
                        //Connect to Oracle data service
                        try
                        {
                            this.oracleConnection.Open();
                            rt = true;
                            //Connection succeeded
                            this.status = Status.CONNECTED;
                            if (this.isloglevel(LogLevel.INFO))
                            {
                                this.log(LogLevel.INFO, "Opened oracle connection for database {0}", this.databaseHost);
                            }
                        }
                        catch (OracleException oEx)
                        {
                            string errMsg = string.Format("Cannot open oracle connection for database {0} - OracleException thrown - error code: {1}, details: {2}, {3}",
                                this.databaseHost, oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No stack trace");
                            if (this.isloglevel(LogLevel.FATAL))
                            {
                                this.log(LogLevel.FATAL, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, oEx));
                        }
                        catch (Exception eX)
                        {
                            string errMsg = string.Format("Cannot open oracle connection object for database {0} - Exception thrown: {1}, {2}",
                                this.databaseHost, eX.Message, eX.StackTrace ?? "No stack trace");
                            if (this.isloglevel(LogLevel.FATAL))
                            {
                                this.log(LogLevel.FATAL, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));
                        }
                    }
                    else
                    {
                        //Oracle connection object is null
                        string errMsg = string.Format("Cannot open Oracle connection - OracleConnection object for this connection is null for database {0}", this.databaseHost);
                        if (this.isloglevel(LogLevel.FATAL))
                        {
                            this.log(LogLevel.FATAL, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    }
                }
                else if (this.status == Status.CONNECTED)
                {
                    //Already connected
                    rt = true;
                    if (this.isloglevel(LogLevel.WARN))
                    {
                        this.log(LogLevel.WARN, "Oracle connection for database {0} is already connected", this.databaseHost);
                    }
                }
            }
            return (rt);
        }

        /// <summary>
        /// Close any open reader objects and the connection to the database
        /// </summary>
        public void disconnect()
        {
            bool rt = true;
            lock (mutexObj)
            {
                if (this.status != Status.CONNECTED)
                {
                    //We are not connected to oracle
                    string errMsg = string.Format("Cannot disconnect Oracle connection - not currently connected to database {0}", this.databaseHost);
                    if (this.isloglevel(LogLevel.FATAL))
                    {
                        this.log(LogLevel.FATAL, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    rt = false;                   
                }
                if (rt)
                {
                    try
                    {
                        //Disconnect Oracle connection object only if we are connected
                        if (this.oracleConnection != null && this.oracleConnection.State == ConnectionState.Open)
                        {
                            //Rollback any implicit transactions that may be 
                            //open in the disconnect scenario
                            if (this.transactionBlock)
                            {
                                this.transactionObj.Rollback();
                                this.transactionObj.Dispose();
                                this.transactionObj = null;
                                if (this.isloglevel(LogLevel.WARN))
                                {
                                    this.log(LogLevel.WARN,
                                             "Rolled back pending transaction for the disconnect scenario for database {0}",
                                             this.databaseHost);
                                }
                            }
                            this.transactionBlock = false;
                            this.oracleConnection.Close();
                            this.oracleConnection.Dispose();
                            this.oracleConnection = null;
                            this.status = Status.INITIALIZED;
                            if (this.isloglevel(LogLevel.INFO))
                            {
                                this.log(LogLevel.INFO, "Successfully disconnected from database {0}", this.databaseHost);
                            }
                        }
                        else if (this.oracleConnection == null)
                        {
                            //Oracle connection object is null
                            string errMsg = string.Format("Cannot disconnect Oracle connection - OracleConnection object for this connection is null to database {0}", this.databaseHost);
                            if (this.isloglevel(LogLevel.FATAL))
                            {
                                this.log(LogLevel.FATAL, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        }
                        else if (this.oracleConnection.State != ConnectionState.Open)
                        {
                            //We are not connected to oracle
                            string errMsg = string.Format("Cannot disconnect Oracle connection - not currently connected to database {0}", this.databaseHost);
                            if (this.isloglevel(LogLevel.FATAL))
                            {
                                this.log(LogLevel.FATAL, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        }
                    }
                    catch (OracleException oEx)
                    {
                        string errMsg = string.Format("Cannot disconnect oracle connection for database {0} - OracleException thrown - error code: {1}, details: {2}, {3}",
                            this.databaseHost, oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No stack trace");
                        if (this.isloglevel(LogLevel.FATAL))
                        {
                            this.log(LogLevel.FATAL, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, oEx));
                    }
                    catch (Exception eX)
                    {
                        string errMsg = string.Format("Cannot disconnect oracle connection object for database {0} - Exception thrown: {1}, {2}",
                            this.databaseHost, eX.Message, eX.StackTrace ?? "No stack trace");
                        if (this.isloglevel(LogLevel.FATAL))
                        {
                            this.log(LogLevel.FATAL, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));
                    }
                }
            }
        }

        /// <summary>
        /// Close the connection and re-open the connection
        /// </summary>
        public void reconnect()
        {
            bool rt = true;
            lock(mutexObj)
            {
                if (this.status != Status.CONNECTED)
                {
                    //We are not connected to oracle
                    string errMsg = string.Format("Cannot reconnect Oracle connection - not currently connected to database {0}", this.databaseHost);
                    if (this.isloglevel(LogLevel.FATAL))
                    {
                        this.log(LogLevel.FATAL, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    rt = false;
                }

                if (rt)
                {
                    try
                    {
                        //disconnect/reconnect oracle connection object
                        if (this.oracleConnection != null && this.oracleConnection.State == ConnectionState.Open)
                        {
                            //Commit any pending transactions in a reconnect scenario
                            if (this.transactionBlock)
                            {
                                this.transactionObj.Commit();
                                this.transactionObj.Dispose();
                                this.transactionObj = null;
                                if (this.isloglevel(LogLevel.INFO))
                                {
                                    this.log(LogLevel.INFO, "Committed pending transaction for the disconnect scenario for database {0}",
                                             this.databaseHost);
                                }
                            }
                            this.transactionBlock = false;
                            this.oracleConnection.Close();
                            this.oracleConnection.Open();
                            if (this.isloglevel(LogLevel.INFO))
                            {
                                this.log(LogLevel.INFO, "Successfully reconnected database {0}", this.databaseHost);
                            }
                        }
                        else if (this.oracleConnection == null)
                        {
                            //Oracle connection object is null
                            string errMsg = string.Format("Cannot reconnect Oracle connection - OracleConnection object for this connection is null to database {0}", this.databaseHost);
                            if (this.isloglevel(LogLevel.FATAL))
                            {
                                this.log(LogLevel.FATAL, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        }
                        else if (this.oracleConnection.State != ConnectionState.Open)
                        {
                            //We are not connected to oracle
                            string errMsg = string.Format("Cannot reconnect Oracle connection - not currently connected to database {0}", this.databaseHost);
                            if (this.isloglevel(LogLevel.FATAL))
                            {
                                this.log(LogLevel.FATAL, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        }
                    }
                    catch (OracleException oEx)
                    {
                        string errMsg = string.Format("Cannot reconnect oracle connection for database {0} - OracleException thrown - error code: {1}, details: {2}, {3}",
                            this.databaseHost, oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No stack trace");
                        if (this.isloglevel(LogLevel.FATAL))
                        {
                            this.log(LogLevel.FATAL, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, oEx));
                    }
                    catch (Exception eX)
                    {
                        string errMsg = string.Format("Cannot reconnect oracle connection object for database {0} - Exception thrown: {1}, {2}",
                            this.databaseHost, eX.Message, eX.StackTrace ?? "No stack trace");
                        if (this.isloglevel(LogLevel.FATAL))
                        {
                            this.log(LogLevel.FATAL, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));
                    }
                }
            }
        }

        /// <summary>
        /// Acquire the Oracle command object from the connection utilizing
        /// the passed in query string
        /// </summary>
        /// <param name="query">Query string used to initialize command object</param>
        /// <returns>OracleCommand object</returns>
        public OracleCommand acquireCommand(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                //We are not connected to oracle
                string errMsg = string.Format("Cannot acquire command with an empty/null query");
                if (this.isloglevel(LogLevel.FATAL))
                {
                    this.log(LogLevel.FATAL, errMsg);
                }
                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                return (null);
            }

            OracleCommand rt = null;

            lock (mutexObj)
            {
                if (this.status != Status.CONNECTED)
                {
                    if (this.isloglevel(LogLevel.WARN))
                    {
                        this.log(LogLevel.WARN, "Acquiring command for query {0}, must obtain connection first to database", query, this.databaseHost);
                    }
                    if (!this.connect())
                    {
                        //We are not connected to oracle
                        string errMsg = string.Format("Cannot acquire command - no oracle connection available to database {0} with query {1}", this.databaseHost, query);
                        if (this.isloglevel(LogLevel.FATAL))
                        {
                            this.log(LogLevel.FATAL, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    }
                }

                if (this.status == Status.CONNECTED)
                {
                    try
                    {
                        rt = new OracleCommand(query, this.oracleConnection);
                    }
                    catch (OracleException oEx)
                    {
                        string errMsg = string.Format("Cannot acquire command for query {0} for database {1} - OracleException thrown - error code: {2}, details: {3}, {4}",
                            query, this.databaseHost, oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No stack trace");
                        if (this.isloglevel(LogLevel.FATAL))
                        {
                            this.log(LogLevel.FATAL, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, oEx));
                    }
                    catch (Exception eX)
                    {
                        string errMsg = string.Format("Cannot acquire command for query {0} for database {1} - Exception thrown: {2}, {3}",
                            query, this.databaseHost, eX.Message, eX.StackTrace ?? "No stack trace");
                        if (this.isloglevel(LogLevel.FATAL))
                        {
                            this.log(LogLevel.FATAL, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));
                    }
                }
                else
                {
                    //No connection to Oracle
                    string errMsg = string.Format("Cannot acquire command - not connected via the connection object to database {0} with query {1}", this.databaseHost, query);
                    if (this.isloglevel(LogLevel.FATAL))
                    {
                        this.log(LogLevel.FATAL, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                }
            }

            return (rt);
        }

        #region IDisposable Members

        /// <summary>
        /// Implementation of IDispose interface for this object
        /// </summary>
        public void Dispose()
        {
            if (this.isloglevel(LogLevel.WARN))
            {
                this.log(LogLevel.WARN, "Disposing OracleDbConnection object, internal call to disconnect for clean up purposes occurring now...");
            }
            this.disconnect();
        }

        #endregion
    }
}
