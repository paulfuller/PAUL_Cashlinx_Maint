using System;
using System.Collections.Generic;
using System.Text;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Oracle.DataAccess.Client;
using System.Data;

namespace Common.Controllers.Database.Oracle
{
    /*
     * Utilizing thread safe singleton strategy 
     * Singleton implementation strategy from http://www.yoda.arachsys.com/csharp/singleton.html
     */
    /// <summary>
    /// 
    /// </summary>
    sealed class OracleMultiDbConnection : MarshalByRefObject, IDisposable
    {
        #region Singleton Data and Methods
        static readonly object mutexObj = new object();
        static readonly OracleMultiDbConnection instance = new OracleMultiDbConnection();
        static OracleMultiDbConnection()
        {
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public static OracleMultiDbConnection Instance
        {
            get
            {
                return (instance);
            }
        }
        #endregion

        #region Static String Constants
        public readonly static string DS_WILDCARD = "?";
        public readonly static string DS_USERID = "user id=" + DS_WILDCARD + ";";
        public readonly static string DS_PASSWORD = "password=" + DS_WILDCARD + ";";
        public readonly static string DS_DATASRC = "data source=";
        public readonly static string SLA = "/";
        public readonly static string CLN = ":";
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
        private Dictionary<int, string> userNames;
        private Dictionary<int, string> passwords;
        private Dictionary<int, string> databaseHosts;
        private Dictionary<int, string> databasePorts;
        private Dictionary<int, string> databaseServices;
        private Dictionary<int, string> databaseSchemas;
        private Dictionary<int, string> dataServices;
        private Dictionary<int, OracleConnection> oracleConnections;
        private Dictionary<int, Status> status;
        private Dictionary<int, bool> transactionBlocks;
        private Dictionary<int, OracleTransaction> transactionObjs;
        #endregion


        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private bool getStatus(int owner, out Status val)
        {
            val = Status.DISCONNECTED;
            if (CollectionUtilities.isNotEmptyContainsKey(this.status, owner))
            {
                val = this.status[owner];
                return (true);
            }
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private void setStatus(int owner, Status val)
        {
            if (CollectionUtilities.isNotEmptyContainsKey(this.status, owner))
            {
                this.status[owner] = val;
            }
            else
            {
                this.status.Add(owner, val);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private bool getTransactionBlockFlag(int owner, out bool val)
        {
            val = false;
            if (CollectionUtilities.isNotEmptyContainsKey(this.transactionBlocks, owner))
            {
                val = this.transactionBlocks[owner];
                return (true);
            }

            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private void setTransactionBlockFlag(int owner, bool val)
        {
            if (CollectionUtilities.isNotEmptyContainsKey(this.transactionBlocks, owner))
            {
                this.transactionBlocks[owner] = val;
            }
            else
            {
                this.transactionBlocks.Add(owner, val);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private bool getOracleConnection(int owner, out OracleConnection val)
        {
            val = null;
            if (CollectionUtilities.isNotEmptyContainsKey(this.oracleConnections, owner))
            {
                val = this.oracleConnections[owner];
                return (val != null);
            }
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private void setOracleConnection(int owner, OracleConnection val)
        {
            if (CollectionUtilities.isNotEmptyContainsKey(this.oracleConnections, owner))
            {
                this.oracleConnections[owner] = val;
            }
            else
            {
                this.oracleConnections.Add(owner, val);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private bool getOracleTransaction(int owner, out OracleTransaction val)
        {
            val = null;
            if (CollectionUtilities.isNotEmptyContainsKey(this.transactionObjs, owner))
            {
                val = this.transactionObjs[owner];                
                return (val != null);
            }
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private void setOracleTransaction(int owner, OracleTransaction val)
        {
            if (CollectionUtilities.isNotEmptyContainsKey(this.transactionObjs, owner))
            {
                this.transactionObjs[owner] = val;
            }
            else
            {
                this.transactionObjs.Add(owner, val);
            }
        }

        /// <summary>
        /// Generates the data service name utilized to
        /// connect to the database
        /// </summary>
        /// <returns>Success of data service string creation</returns>
        private bool generateDataService(int owner)
        {
            if (string.IsNullOrEmpty(this.userNames[owner]) ||
                string.IsNullOrEmpty(this.passwords[owner]) ||
                string.IsNullOrEmpty(this.databaseHosts[owner]) ||
                string.IsNullOrEmpty(this.databasePorts[owner]) ||
                string.IsNullOrEmpty(this.databaseSchemas[owner]) ||
                string.IsNullOrEmpty(this.databaseServices[owner]))
            {
                return (false);
            }

            //Build data service string
            var sbuilder = new StringBuilder();
            sbuilder.Append(DS_USERID.Replace(DS_WILDCARD, this.userNames[owner]));
            sbuilder.Append(DS_PASSWORD.Replace(DS_WILDCARD, this.passwords[owner]));
            sbuilder.Append(DS_DATASRC);
            sbuilder.Append(this.databaseHosts[owner]);
            sbuilder.Append(CLN);
            sbuilder.Append(this.databasePorts[owner]);
            sbuilder.Append(SLA);
            sbuilder.Append(this.databaseServices[owner]);

            //Set the data service
            this.dataServices.Add(owner, sbuilder.ToString());

            return (true);
        }
        #endregion


        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public OracleMultiDbConnection()
        {
            lock (mutexObj)
            {
                this.userNames = new Dictionary<int, string>();
                this.passwords = new Dictionary<int, string>();
                this.databaseHosts = new Dictionary<int, string>();
                this.databasePorts = new Dictionary<int, string>();
                this.databaseServices = new Dictionary<int, string>();
                this.databaseSchemas = new Dictionary<int, string>();
                this.dataServices = new Dictionary<int, string>();
                this.status = new Dictionary<int, Status>();
                this.transactionBlocks = new Dictionary<int, bool>();
                this.oracleConnections = new Dictionary<int, OracleConnection>();
                this.transactionObjs = new Dictionary<int, OracleTransaction>();
            }
        }
        #endregion


        #region Accessors

        public Status getStatus(int owner)
        {
            Status s;
            this.getStatus(owner, out s);
            return (s);
        }

        #endregion

        public bool startTransactionBlock(int owner, IsolationLevel isoLevel)
        {
            return (this.startTransactionBlock(owner, isoLevel, false));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="isoLevel"></param>
        /// <param name="isRetry"></param>
        /// <returns></returns>
        private bool startTransactionBlock(int owner, IsolationLevel isoLevel, bool isRetry)
        {
            bool rt = false;
            bool transactBlockStarted = false;
            lock (mutexObj)
            {
                OracleConnection oraConn;
                if (this.getOracleConnection(owner, out oraConn))
                {
                    bool transactBlock;
                    
                    if (this.getTransactionBlockFlag(owner, out transactBlock))
                    {
                        if (transactBlock == false)
                        {
                            /*if (this.getOracleTransaction(owner, out oraTrans))
                            {*/
                                try
                                {
                                    if (oraConn != null)
                                    {
                                        OracleTransaction oraTrans = oraConn.BeginTransaction();
                                        rt = true;
                                        this.setOracleTransaction(owner, oraTrans);
                                        this.setTransactionBlockFlag(owner, true);
                                    }
                                }
                                catch (OracleException oEx)
                                {
                                    string errMsg = string.Format("Cannot start transaction block - OracleException thrown - error code: {0}, details: {1}, {2}", 
                                        oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No stack trace");
                                    if (FileLogger.Instance.IsLogFatal)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                    }
                                    BasicExceptionHandler.Instance.AddException(
                                        errMsg,
                                        new ApplicationException(errMsg, oEx));
                                }
                                catch (Exception eX)
                                {
                                    string errMsg = string.Format("Cannot start transaction block - Exception thrown: {0}, {1}", 
                                        eX.Message, eX.StackTrace ?? "No stack trace");
                                    if (FileLogger.Instance.IsLogFatal)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                    }
                                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));
                                }

                                //Check flag status and output appropriate log message
                                if (rt)
                                {
                                    if (FileLogger.Instance.IsLogInfo)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.INFO, this,
                                                                       "Started transaction block with isolation level = {0}", isoLevel);
                                    }
                                }
                                else
                                {
                                    string errMsg = string.Format("Cannot start transaction block - Return value false for owner {0}", owner);
                                    if (FileLogger.Instance.IsLogFatal)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                    }
                                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                }
                            /*}
                            else
                            {
                                string errMsg = string.Format("Cannot start transaction block - unable to retrieve oracle transaction object for this owner {0}", owner);
                                if (FileLogger.Instance.IsLogFatal)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                }
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                            }*/
                        }
                        else
                        {
                            string errMsg = string.Format("Cannot start transaction block - transaction flag for this connection {0} is {1} - transaction already in progress", owner, transactBlock);
                            if (FileLogger.Instance.IsLogFatal)
                            {
                                FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                            transactBlockStarted = true;
                        }
                    }
                    else
                    {
                        string errMsg = string.Format("Cannot start transaction block - unable to retrieve transaction flag for this connection {0}", owner);
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));                        
                    }
                }
                else
                {
                    string errMsg = string.Format("Cannot start transaction block - unable to retrieve OracleConnection object for this connection owner {0}", owner);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));                        
                }
            }

            if (!rt && transactBlockStarted && !isRetry)
            {
                if (FileLogger.Instance.IsLogWarn)
                {
                    FileLogger.Instance.logMessage(LogLevel.WARN, this,
                                                   "Attempting to rollback a transaction in progress and then start a new transaction block in connection {0}",
                                                   owner);
                }
                if (this.rollbackTransactionBlock(owner))
                {
                    if (FileLogger.Instance.IsLogInfo)
                    {
                        FileLogger.Instance.logMessage(LogLevel.INFO, this,
                                                       "Attempt to rollback transaction in progress is successful.  Starting new transaction block in connection {0}",
                                                       owner);
                    }
                    if (this.startTransactionBlock(owner, isoLevel, true))
                    {
                        rt = true;
                        if (FileLogger.Instance.IsLogInfo)
                        {
                            FileLogger.Instance.logMessage(LogLevel.INFO, this,
                                                           "Attempt to start a transaction block after the rollback was complete is successful for connection {0}",
                                                           owner);
                        }
                    }
                    else
                    {
                        string errMsg = string.Format("Cannot restart transaction block - start portion has failed for this connection owner {0}", owner);
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));                        
                    }
                }
                else
                {
                    string errMsg = string.Format("Cannot restart transaction block - initial rollback has failed for this connection owner {0}", owner);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
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
        public bool commitTransactionBlock(int owner)
        {
            bool rt = false;
            lock (mutexObj)
            {
                OracleConnection oraConn;
                if (this.getOracleConnection(owner, out oraConn))
                {
                    bool transactBlock;
                    if (this.getTransactionBlockFlag(owner, out transactBlock))
                    {
                        if (transactBlock)
                        {
                            OracleTransaction oraTrans;
                            if (this.getOracleTransaction(owner, out oraTrans))
                            {
                                try
                                {
                                    if (oraTrans != null)
                                    {
                                        oraTrans.Commit();
                                        rt = true;
                                        this.setTransactionBlockFlag(owner, false);
                                        this.setOracleTransaction(owner, null);
                                    }
                                }
                                catch (OracleException oEx)
                                {
                                    string errMsg =
                                            string.Format(
                                                          "Cannot commit transaction block - OracleException thrown - Code: {0}, Details: {1}, {2}",
                                                          oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No stack trace");
                                    if (FileLogger.Instance.IsLogFatal)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                    }
                                    BasicExceptionHandler.Instance.AddException(errMsg,
                                        new ApplicationException(errMsg, oEx));
                                }
                                catch (Exception eX)
                                {
                                    string errMsg = string.Format("Cannot commit transaction block: - Exception thrown: {0}, {1}",
                                                                  eX.Message, eX.StackTrace ?? "No stack trace");
                                    if (FileLogger.Instance.IsLogFatal)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                    }
                                    BasicExceptionHandler.Instance.AddException(errMsg,
                                        new ApplicationException(errMsg, eX));
                                }
                                //Check flag status and output appropriate log message
                                if (rt)
                                {
                                    if (FileLogger.Instance.IsLogInfo)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.INFO, this,
                                                                       "Committed transaction block");
                                    }
                                }
                                else
                                {
                                    string errMsg = string.Format("Cannot commit transaction block - return value false for owner {0}", owner);
                                    if (FileLogger.Instance.IsLogFatal)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                    }
                                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                }
                            }
                            else
                            {
                                //Cannot get oracle transaction object
                                string errMsg = string.Format("Cannot commit transaction block - unable to retrieve oracle transaction object for this owner {0}", owner);
                                if (FileLogger.Instance.IsLogFatal)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                }
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                            }
                        }
                        else
                        {
                            //Transact block flag is false
                            string errMsg = string.Format("Cannot commit transaction block - transaction flag for this connection {0} is {1} - no transaction in progress", owner, transactBlock);
                            if (FileLogger.Instance.IsLogFatal)
                            {
                                FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        }
                    }
                    else
                    {
                        //Cannot get transaction block flag
                        string errMsg = string.Format("Cannot commit transaction block - unable to retrieve transaction flag for this connection {0}", owner);
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    }
                }
                else
                {
                    //Cannot retrieve connection
                    string errMsg = string.Format("Cannot commit transaction block - unable to retrieve OracleConnection object for this connection owner {0}", owner);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                }
            }
            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public bool rollbackTransactionBlock(int owner)
        {
            bool rt = false;
            lock (mutexObj)
            {
                OracleConnection oraConn;
                if (this.getOracleConnection(owner, out oraConn))
                {
                    bool transactBlock;
                    if (this.getTransactionBlockFlag(owner, out transactBlock))
                    {
                        if (transactBlock)
                        {
                            OracleTransaction oraTrans;
                            if (this.getOracleTransaction(owner, out oraTrans))
                            {
                                try
                                {
                                    if (oraTrans != null)
                                    {
                                        oraTrans.Rollback();
                                        rt = true;
                                        this.setTransactionBlockFlag(owner, false);
                                        this.setOracleTransaction(owner, null);
                                    }
                                }
                                catch (OracleException oEx)
                                {
                                    string errMsg =
                                            string.Format(
                                                          "Cannot rollback transaction block - OracleException thrown - Error Code: {0}, Details: {1}, {2}",
                                                          oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No stack trace");
                                    if (FileLogger.Instance.IsLogFatal)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                    }
                                    BasicExceptionHandler.Instance.AddException(errMsg,
                                        new ApplicationException(errMsg, oEx));
                                }
                                catch (Exception eX)
                                {
                                    string errMsg =
                                            string.Format(
                                                          "Cannot rollback transaction block - Exception thrown - {0}, {1}",
                                                          eX.Message, eX.StackTrace ?? "No stack trace");
                                    if (FileLogger.Instance.IsLogFatal)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                    }
                                    BasicExceptionHandler.Instance.AddException(errMsg,
                                        new ApplicationException(errMsg, eX));
                                }
                                //Check flag status and output appropriate log message
                                if (rt)
                                {
                                    if (FileLogger.Instance.IsLogInfo)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.INFO, this,
                                                                       "Rolled back transaction block");
                                    }
                                }
                                else
                                {
                                    string errMsg = string.Format("Cannot rollback transaction block - return value false for owner {0}", owner);
                                    if (FileLogger.Instance.IsLogFatal)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                    }
                                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                }
                            }
                            else
                            {
                                //Cannot get oracle transaction object
                                string errMsg = string.Format("Cannot rollback transaction block - unable to retrieve oracle transaction object for this owner {0}", owner);
                                if (FileLogger.Instance.IsLogFatal)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                }
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                            }
                        }
                        else
                        {
                            //Transact block flag is false
                            string errMsg = string.Format("Cannot rollback transaction block - transaction flag for this connection {0} is {1} - no transaction in progress", owner, transactBlock);
                            if (FileLogger.Instance.IsLogFatal)
                            {
                                FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        }
                    }
                    else
                    {
                        //Cannot get transact block flag
                        string errMsg = string.Format("Cannot rollback transaction block - unable to retrieve transaction flag for this connection {0}", owner);
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    }
                }
                else
                {
                    //Cannot get oracle connection object
                    string errMsg = string.Format("Cannot rollback transaction block - unable to retrieve OracleConnection object for this connection owner {0}", owner);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                }
            }
            return (rt);
        }

        /// <summary>
        /// Returns true/false whether or not a transaction block is currently in place
        /// for the given owner
        /// </summary>
        /// <param name="owner">Owner Id</param>
        /// <returns></returns>
        public bool inTransactionBlock(int owner)
        {
            bool rt = false;
            lock(mutexObj)
            {
                OracleConnection oraConn;
                if (this.getOracleConnection(owner, out oraConn))
                {
                    bool transactBlock;
                    if (this.getTransactionBlockFlag(owner, out transactBlock))
                    {
                        if (transactBlock)
                        {
                            if (FileLogger.Instance.IsLogInfo)
                            {
                                FileLogger.Instance.logMessage(LogLevel.INFO, this, "Transaction block status - Oracle transaction block is in progress");
                            }
                            rt = true;
                        }
                        else
                        {
                            if (FileLogger.Instance.IsLogInfo)
                            {
                                FileLogger.Instance.logMessage(LogLevel.INFO, this, "Transaction block status - Oracle transaction block is not in progress");
                            }
                        }
                    }
                    else
                    {
                        //Cannot get transact block flag
                        string errMsg = string.Format("Cannot determine transaction status - unable to retrieve transaction flag for this connection {0}", owner);
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    }
                }
                else
                {
                    //Cannot get oracle connection object
                    string errMsg = string.Format("Cannot determine transaction status - unable to retrieve OracleConnection object for this connection owner {0}", owner);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                }
            }
            return (rt);
        }


        /// <summary>
        /// Initialize the database connection
        /// </summary>
        /// <param name="usrName">User name credential</param>
        /// <param name="passwd">Password credential</param>
        /// <param name="dbHost">Host name of the database</param>
        /// <param name="dbPort">Service port on the database</param>
        /// <param name="dbService">Service name on the database</param>
        /// <param name="dbSchema">Schema name to access in the database</param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public bool initialize(
            string usrName,
            string passwd,
            string dbHost,
            string dbPort,
            string dbService,
            string dbSchema,
            int owner)
        {
            var rt = false;
            lock (mutexObj)
            {
                //Get data
                this.userNames.Add(owner, usrName);
                this.passwords.Add(owner, passwd);
                this.databaseHosts.Add(owner, dbHost);
                this.databasePorts.Add(owner, dbPort);
                this.databaseServices.Add(owner, dbService);
                this.databaseSchemas.Add(owner, dbSchema);
                this.setTransactionBlockFlag(owner, false);
                this.setOracleTransaction(owner, null);
                Status s;
                this.getStatus(owner, out s);
                switch (s)
                {
                    case Status.DISCONNECTED:
                        if (this.generateDataService(owner))
                        {
                            this.setStatus(owner, Status.INITIALIZED);
                            rt = true;
                        }
                        break;
                    case Status.INITIALIZED:
                        rt = true;
                        break;
                    case Status.CONNECTED:
                        rt = true;
                        break;
                }
            }

            return (rt);
        }

        /// <summary>
        /// Add proper statement cache details to the data service string
        /// </summary>
// ReSharper disable UnusedMember.Local
        private void appendStatementCaching(StringBuilder sb)
// ReSharper restore UnusedMember.Local
        {
            if (sb == null)
            {
                return;
            }

            if (STATEMENT_CACHING_FLAG)
            {
                sb.Append(STATEMENT_CACHE_ON);
            }
            else
            {
                sb.Append(STATEMENT_CACHE_OFF);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"></param>
// ReSharper disable UnusedMember.Local
        private void appendPoolingOptions(StringBuilder sb)
// ReSharper restore UnusedMember.Local
        {
            if (sb == null)
            {
                return;
            }
        }

        /// <summary>
        /// Open the connection to the database if and only if the connection is
        /// not already open
        /// </summary>
        /// <returns>Success of connection open operation</returns>
        public bool connect(int owner)
        {
            bool rt = false;
            lock (mutexObj)
            {
                Status s;
                if (this.getStatus(owner, out s))
                {
                    switch (s)
                    {
                        case Status.DISCONNECTED:
                        case Status.INITIALIZED:
                        {
                            //Create Oracle connection object
                            OracleConnection oraConn;
                            bool fetchFlag = this.getOracleConnection(owner, out oraConn);
                            if (!fetchFlag || oraConn == null)
                            {
                                oraConn = new OracleConnection(this.dataServices[owner]);
                            }
                            //Connect to Oracle data service
                            try
                            {
                                oraConn.Open();
                                rt = true;
                                //Connection succeeded
                                this.setStatus(owner, Status.CONNECTED);
                                this.setOracleConnection(owner, oraConn);
                                if (FileLogger.Instance.IsLogInfo)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.INFO, this,
                                                                   "Database connection successfully opened for owner {0}", owner);
                                }
                            }
                            catch (OracleException oEx)
                            {
                                string errMsg = string.Format("Cannot open a database connection to {0}: OracleException: Code {1}, Details: {2}, {3}",
                                                              owner, oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No stack trace");
                                if (FileLogger.Instance.IsLogFatal)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                }
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, oEx));
                            }
                            catch(Exception eX)
                            {
                                string errMsg = string.Format("Cannot open a database connection to {0}: Exception: Details: {1}, {2}",
                                                              owner, eX.Message, eX.StackTrace ?? "No stack trace");
                                if (FileLogger.Instance.IsLogFatal)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                }
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));
                            }
                            break;
                        }
                        case Status.CONNECTED:
                            if (FileLogger.Instance.IsLogInfo)
                            {
                                FileLogger.Instance.logMessage(LogLevel.INFO, this, "{0} is already connected", owner);
                            }
                            rt = true;
                            break;
                    }
                }
                else
                {
                    //Cannot determine connection status
                    string errMsg = string.Format("Cannot determine connection status - unable to get status flag for this connection owner {0}", owner);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                }
            }

            return (rt);
        }

        /// <summary>
        /// Close any open reader objects and the connection to the database
        /// </summary>
        public void disconnect()
        {
            lock (mutexObj)
            {
                if (CollectionUtilities.isEmpty(this.oracleConnections))
                {
                    string errMsg = string.Format("Disconnect operation failed - no connections to disconnect");
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    return;
                }
                var keys = this.oracleConnections.Keys;
                foreach (var curKey in keys)
                {
                    Status s;
                    if (!this.getStatus(curKey, out s))
                    {
                        string errMsg = string.Format("Disconnect operation not attempted - could not determine connection status for {0}", curKey);
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        continue;
                    }
                    if (s != Status.CONNECTED)
                    {
                        //Connection status is not valid
                        string errMsg = string.Format("Disconnect operation not attempted - connection is not open for {0}", curKey);
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        continue;
                    }
                    try
                    {

                        OracleConnection oraCon;
                        OracleTransaction oraTrans;
                        bool conFetchFlag = this.getOracleConnection(curKey, out oraCon);
                        //Disconnect Oracle connection object
                        if (!conFetchFlag || oraCon == null || oraCon.State != ConnectionState.Open)
                        {
                            //Cannot get oracle transaction object
                            string errMsg = string.Format("Disconnect operation failed - unable to retrieve oracle connection object for this owner {0}, state is not open or object is null", curKey);
                            if (FileLogger.Instance.IsLogFatal)
                            {
                                FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                            continue;
                        }
                        if (this.getOracleTransaction(curKey, out oraTrans))
                        {
                            //disconnect scenario should rollback
                            if (oraTrans != null)
                            {
                                oraTrans.Rollback();
                                oraTrans.Dispose();
                                this.setOracleTransaction(curKey, null);
                                if (FileLogger.Instance.IsLogWarn)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.WARN, this,
                                                                   "Rolled back pending transaction prior to disconnecting from database for this owner {0}",
                                                                   curKey);
                                }
                            }
                            else
                            {
                                //Oracle transaction object is null
                                string errMsg = string.Format("Disconnect operation could not rollback transaction - oracle transaction object for this owner {0} is null", curKey);
                                if (FileLogger.Instance.IsLogFatal)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                }
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                            }
                        }
                        else
                        {
                            //Cannot get oracle transaction object
                            string errMsg = string.Format("Disconnect operation could not rollback transaction - unable to retrieve oracle transaction object for this owner {0}", curKey);
                            if (FileLogger.Instance.IsLogFatal)
                            {
                                FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                            
                        }
                        oraCon.Close();
                        oraCon.Dispose();
                        this.setStatus(curKey, Status.INITIALIZED);
                        this.setOracleConnection(curKey, null);
                        if (FileLogger.Instance.IsLogInfo)
                        {
                            FileLogger.Instance.logMessage(LogLevel.INFO, this, "Successfully disconnected connection for owner {0}", curKey);
                        }
                    }
                    catch (OracleException oEx)
                    {
                        string errMsg = string.Format(
                                                      "Disconnect operation failed on connection {0} - OracleException thrown: Code:{1}, Details:{2} {3}",
                                                      curKey, oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No stack trace");
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, oEx));
                    }
                    catch(Exception eX)
                    {
                        string errMsg = string.Format(
                                                      "Disconnect operation failed on connection {0} - Exception thrown: {1} {2}",
                                                      curKey, eX.Message, eX.StackTrace ?? "No stack trace");
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));
                    }
                }
            }
        }

        /// <summary>
        /// Close any open reader objects and the connection to the database
        /// </summary>
        public void disconnect(int owner)
        {
            lock (mutexObj)
            {
                if (CollectionUtilities.isEmpty(this.oracleConnections))
                {
                    string errMsg = string.Format("Disconnect operation failed - collection of OracleConnection objects is empty");
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    return;
                }
                if (!this.oracleConnections.ContainsKey(owner))
                {
                    string errMsg = string.Format("Disconnect operation failed - could not get OracleConnection object for owner {0}", owner);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    return;
                }
                Status s;
                if (!this.getStatus(owner, out s))
                {
                    string errMsg = string.Format("Disconnect operation failed - could not determine connection status for owner {0}", owner);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    return;
                }
                if (s != Status.CONNECTED)
                {
                    string errMsg = string.Format("Disconnect operation failed - connection is already closed for owner {0}", owner);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    return;
                }
                try
                {
                    OracleConnection oraCon;
                    OracleTransaction oraTrans;
                    bool fetchFlag = this.getOracleConnection(owner, out oraCon);
                    //Disconnect Oracle connection object
                    if (!fetchFlag || oraCon == null || oraCon.State != ConnectionState.Open)
                    {
                        //Cannot get oracle connection object
                        string errMsg = string.Format("Disconnect operation failed - unable to retrieve oracle connection object for this owner {0}, state is not open or object is null", owner);
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        return;
                    }
                    if (this.getOracleTransaction(owner, out oraTrans))
                    {
                        //disconnect scenario should rollback
                        if (oraTrans != null)
                        {
                            oraTrans.Rollback();
                            oraTrans.Dispose();
                            this.setOracleTransaction(owner, null);
                            if (FileLogger.Instance.IsLogWarn)
                            {
                                FileLogger.Instance.logMessage(LogLevel.WARN, this,
                                                               "Rolled back pending transaction prior to disconnecting from database for this owner {0}",
                                                               owner);
                            }
                        }
                        else
                        {
                            //Cannot get oracle transaction object
                            string errMsg = string.Format("Disconnect operation could not rollback transaction - oracle transaction object for this owner {0} is null", owner);
                            if (FileLogger.Instance.IsLogFatal)
                            {
                                FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));                            
                        }
                    } //No else needed, only dispose of transaction block if you have one
                    oraCon.Close();
                    oraCon.Dispose();
                    this.setStatus(owner, Status.INITIALIZED);
                    this.setOracleConnection(owner, null);
                    if (FileLogger.Instance.IsLogInfo)
                    {
                        FileLogger.Instance.logMessage(LogLevel.INFO, this, "Successfully disconnected connection for owner {0}", owner);
                    }
                }
                catch (OracleException oEx)
                {
                    string errMsg = string.Format(
                                                  "Disconnect operation failed on connection {0} - OracleException thrown: Code:{1}, Details:{2} {3}",
                                                  owner, oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No stack trace");
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, oEx));
                }
                catch (Exception eX)
                {
                    string errMsg = string.Format(
                                                  "Disconnect operation failed on connection {0} - Exception thrown: {1} {2}",
                                                  owner, eX.Message, eX.StackTrace ?? "No stack trace");
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));
                }
            }
        }

        /// <summary>
        /// Close any open reader objects and the connection to the database
        /// </summary>
        public void reconnect()
        {
            lock (mutexObj)
            {
                if (CollectionUtilities.isEmpty(this.oracleConnections))
                {
                    string errMsg = string.Format("Reconnect operation failed - collection of OracleConnection objects is empty");
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    return;
                }
                var keys = this.oracleConnections.Keys;
                foreach (var curKey in keys)
                {
                    Status s;
                    if (!this.getStatus(curKey, out s))
                    {
                        string errMsg = string.Format("Reconnect operation failed - could not retrieve status of connection {0}", curKey);
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        continue;
                    }
                    if (s != Status.CONNECTED)
                    {
                        string errMsg = string.Format("Reconnect operation failed - connection {0} already established", curKey);
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        continue;
                    }
                    try
                    {
                        OracleConnection oraCon;
                        OracleTransaction oraTrans;
                        bool fetchFlag = this.getOracleConnection(curKey, out oraCon);
                        //Disconnect Oracle connection object
                        if (!fetchFlag || oraCon == null || oraCon.State != ConnectionState.Open)
                        {
                            string errMsg = string.Format("Reconnect operation failed - unable to retrieve oracle connection object for this owner {0}, state is not open or object is null", curKey);
                            if (FileLogger.Instance.IsLogFatal)
                            {
                                FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                            continue;
                        }
                        if (this.getOracleTransaction(curKey, out oraTrans))
                        {
                            //reconnect scenario should commit
                            if (oraTrans != null)
                            {
                                oraTrans.Commit();
                                oraTrans.Dispose();
                                this.setOracleTransaction(curKey, null);
                                if (FileLogger.Instance.IsLogInfo)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.INFO, this,
                                                                   "Committed transaction for the reconnect operation for this owner {0}",
                                                                   curKey);
                                }
                            }
                            else
                            {
                                //Cannot get transaction object
                                string errMsg = string.Format("Reconnect operation could not commit transaction - oracle transaction object for this owner {0} is null", curKey);
                                if (FileLogger.Instance.IsLogFatal)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                }
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));                                
                            }
                        }
                        else
                        {
                            //Cannot get transaction object
                            string errMsg = string.Format("Reconnect operation could not commit transaction - could not get oracle transaction object for this owner {0}", curKey);
                            if (FileLogger.Instance.IsLogFatal)
                            {
                                FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));                                
                        }
                        oraCon.Close();
                        oraCon.Open();
                        this.setStatus(curKey, Status.CONNECTED);
                        this.setOracleConnection(curKey, oraCon);
                        if (FileLogger.Instance.IsLogInfo)
                        {
                            FileLogger.Instance.logMessage(LogLevel.INFO, this, "Successfully completed reconnect operation for owner {0}", curKey);
                        }
                    }
                    catch(OracleException oEx)
                    {
                        string errMsg = string.Format(
                                                      "Reconnect operation failed on connection {0} - OracleException thrown: Code:{1}, Details:{2} {3}",
                                                      curKey, oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No stack trace");
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, oEx));
                    }
                    catch(Exception eX)
                    {
                        string errMsg = string.Format(
                                                      "Reconnect operation failed on connection {0} - Exception thrown: {1} {2}",
                                                      curKey, eX.Message, eX.StackTrace ?? "No stack trace");
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));
                    }
                }
            }
        }

        /// <summary>
        /// Close any open reader objects and the connection to the database
        /// </summary>
        public void reconnect(int owner)
        {
            lock (mutexObj)
            {
                if (CollectionUtilities.isEmpty(this.oracleConnections))
                {
                    string errMsg = string.Format("Reconnect operation failed - collection of OracleConnection objects is empty");
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    return;
                }
                if (!this.oracleConnections.ContainsKey(owner))
                {
                    string errMsg = string.Format("Reconnect operation failed - could not retrieve OracleConnection object for owner {0}", owner);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    return;
                }
                Status s;
                if (!this.getStatus(owner, out s))
                {
                    string errMsg = string.Format("Reconnect operation failed - could not determine connection status for owner {0}", owner);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    return;
                }
                if (s != Status.CONNECTED)
                {
                    string errMsg = string.Format("Reconnect operation failed - connection is already closed for owner {0}", owner);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    return;
                }
                try
                {
                    OracleConnection oraCon;
                    OracleTransaction oraTrans;
                    bool fetchFlag = this.getOracleConnection(owner, out oraCon);
                    //Disconnect Oracle connection object
                    if (!fetchFlag || oraCon == null || oraCon.State != ConnectionState.Open)
                    {
                        string errMsg = string.Format("Reconnect operation failed - unable to retrieve oracle connection object for this owner {0}, state is not open or object is null", owner);
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        return;
                    }
                    if (this.getOracleTransaction(owner, out oraTrans))
                    {
                        if (oraTrans != null)
                        {
                            //reconnect scenario should commit
                            oraTrans.Commit();
                            oraTrans.Dispose();
                            this.setOracleTransaction(owner, null);
                            if (FileLogger.Instance.IsLogInfo)
                            {
                                FileLogger.Instance.logMessage(LogLevel.INFO, this,
                                                               "Committed transaction for the reconnect operation for this owner {0}",
                                                               owner);
                            }
                        }
                        else
                        {
                            //Cannot get transaction object
                            string errMsg = string.Format("Reconnect operation could not commit transaction - could not get oracle transaction object for this owner {0}", owner);
                            if (FileLogger.Instance.IsLogFatal)
                            {
                                FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));                                
                        }
                    }
                    oraCon.Close();
                    oraCon.Open();
                    this.setStatus(owner, Status.CONNECTED);
                    this.setOracleConnection(owner, oraCon);
                    if (FileLogger.Instance.IsLogInfo)
                    {
                        FileLogger.Instance.logMessage(LogLevel.INFO, this, "Successfully completed reconnect operation for owner {0}", owner);
                    }
                }
                catch (OracleException oEx)
                {
                    string errMsg = string.Format(
                                                  "Reconnect operation failed on connection {0} - OracleException thrown: Code:{1}, Details:{2} {3}",
                                                  owner, oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No stack trace");
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, oEx));
                }
                catch (Exception eX)
                {
                    string errMsg = string.Format(
                                                  "Reconnect operation failed on connection {0} - Exception thrown: {1} {2}",
                                                  owner, eX.Message, eX.StackTrace ?? "No stack trace");
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));
                }
            }
        }

        /// <summary>
        /// Acquire the Oracle command object from the connection utilizing
        /// the passed in query string
        /// </summary>
        /// <param name="query">Query string used to initialize command object</param>
        /// <param name="owner"></param>
        /// <returns>OracleCommand object</returns>
        ///
        public OracleCommand acquireCommand(string query, int owner)
        {
            if (string.IsNullOrEmpty(query))
            {
                return (null);
            }

            bool rt = true;
            OracleCommand oCom = null;
            Status s;
            lock (mutexObj)
            {
                if (!getStatus(owner, out s))
                {
                    rt = false;
                    string errMsg = string.Format("Acquire command failed for query {0} - could not determine connection status for owner {1}", query, owner);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                }
            }
            if (rt && s != Status.CONNECTED)
            {
                if (!connect(owner))
                {
                    rt = false;
                    string errMsg = string.Format("Acquire command failed for query {0} - could not connect to database as owner {1}", query, owner);
                    if (FileLogger.Instance.IsLogFatal)
                    {
                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                    }
                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                }
                //Re-acquire status after the connect operation
                if (rt)
                {
                    lock(mutexObj)
                    {
                        if (!getStatus(owner, out s))
                        {
                            rt = false;
                            string errMsg = string.Format("Acquire command failed for query {0} during internal connect - could not determine connection status for owner {1}", query, owner);
                            if (FileLogger.Instance.IsLogFatal)
                            {
                                FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        }
                    }
                }
            }
            if (rt)
            {
                lock(mutexObj)
                {
                    if (s == Status.CONNECTED)
                    {
                        OracleConnection oraCon;
                        bool fetchFlag = this.getOracleConnection(owner, out oraCon);
                        if (fetchFlag && oraCon != null)
                        {
                            try
                            {
                                oCom = new OracleCommand(query, oraCon);
                            }
                            catch(OracleException oEx)
                            {
                                rt = false;
                                string errMsg = string.Format(
                                                              "Acquire command operation for query {0} failed on connection {1} - OracleException thrown: Code:{2}, Details:{3} {4}",
                                                              query, owner, oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No stack trace");
                                if (FileLogger.Instance.IsLogFatal)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                }
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, oEx));
                            }
                            catch(Exception eX)
                            {
                                rt = false;
                                string errMsg = string.Format(
                                                              "Acquire command operation for query {0} failed on connection {1} - Exception thrown: {2} {3}",
                                                              query, owner, eX.Message, eX.StackTrace ?? "No stack trace");
                                if (FileLogger.Instance.IsLogFatal)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                }
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg, eX));
                            }
                        }
                        else
                        {
                            rt = false;
                            string errMsg = string.Format("Acquire command failed for query {0} - could not acquire OracleConnection object for owner {1}", query, owner);
                            if (FileLogger.Instance.IsLogFatal)
                            {
                                FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                            }
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        }
                    }
                    else
                    {
                        rt = false;
                        string errMsg = string.Format("Acquire command failed for query {0} - owner {1} is not connected", query, owner);
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                    }
                }
            }

            //Only if rt = true will we return a command object
            return ((rt) ? oCom : null);
        }

        #region IDisposable Members

        /// <summary>
        /// Implementation of IDispose interface for this object
        /// </summary>
        public void Dispose()
        {
            if (FileLogger.Instance != null && FileLogger.Instance.IsEnabled && FileLogger.Instance.IsLogWarn)
            {
                FileLogger.Instance.logMessage(LogLevel.WARN, this,
                                               "Disposing OracleMultiDbConnection object, internal call to disconnect for clean up purposes occurring now...");
            }
            this.disconnect();
        }

        #endregion
    }
}
