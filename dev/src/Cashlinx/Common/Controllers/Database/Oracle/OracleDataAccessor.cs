using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using Common.Controllers.Application;
using Common.Controllers.Network;
using Common.Controllers.Security;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Config;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Common.Controllers.Database.Oracle
{
    public sealed class OracleDataAccessor
    {

        #region Static Fields
        private const string NAME = "NAME";
        private const string VALUE = "VALUE";
        private const string OUTPUT = "OUTPUT";

        public const string DOT = ".";
        public const string SPC = " ";
        public const string AST = "*";


        public const string Q_SELECT = "select ";
        public const string Q_FROM = " from ";
        public const string Q_WHERE = " where ";
        public const uint FETCHSZ_DEFAULT = 65536;
        public const uint FETCHROWS_DEFAULT = 100;
        public const string DEFAULT_SUCCESS = "0";

        #endregion


        #region Enumerations

        public enum Status
        {
            INITIALIZED,
            CONNECTED,
            DISCONNECTED
        };

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Static method to generate native Oracle date string from .NET DateTime object
        /// </summary>
        /// <param name="d">DateTime object to convert</param>
        /// <returns>Oracle native Date string</returns>
        public static string GenerateOracleDateString(DateTime d)
        {
            var oraDate = new OracleDate(d);
            return (oraDate.ToString());
        }

        /// <summary>
        /// Static method to generate native Oracle timestamp string from .NET DateTime object
        /// </summary>
        /// <param name="d">DateTime object to convert</param>
        /// <returns>Oracle native Date string</returns>
        public static string GenerateOracleTimestampString(DateTime d)
        {
            var oraTimeStamp = new OracleTimeStamp(d);
            return (oraTimeStamp.ToString());
        }

        /// <summary>
        /// Static method to generate native Oracle timestamp(LTZ) string from .NET DateTime object
        /// </summary>
        /// <param name="d">DateTime object to convert</param>
        /// <returns>Oracle native Date string</returns>
        public static string GenerateOracleTimestampLTZString(DateTime d)
        {
            var oraTimeStamp = new OracleTimeStampLTZ(d);
            return (oraTimeStamp.ToString());
        }

        /// <summary>
        /// Static method to generate native Oracle timestamp(TZ) string from .NET DateTime object
        /// </summary>
        /// <param name="d">DateTime object to convert</param>
        /// <returns>Oracle native Date string</returns>
        public static string GenerateOracleTimestampTZString(DateTime d)
        {
            var oraTimeStamp = new OracleTimeStampTZ(d);
            return (oraTimeStamp.ToString());
        }

        #endregion

        #region Private Fields

        private readonly bool useMultiConnect;
        private readonly bool useKeyedConnect;
        private readonly uint fetchSizeMx;
        private readonly string userName;
        private readonly string password;
        private readonly string dbHost;
        private readonly string dbPort;
        private readonly string dbService;
        private readonly string dbSchema;
        private double averageTxferRate;
        private double totalTxferTime;
        private long totalBytesTransferred;
        private long totalTransactions;
        private readonly Dictionary<string, int> connectKeys;
        private TempFileLogger auxLogger;

        #endregion

        #region Public Property Fields

        public bool Initialized { get; private set; }

        public string ErrorDescription { get; private set; }

        public string ErrorCode { get; private set; }

        public bool CallSuccess { get; private set; }

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


        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userNm"></param>
        /// <param name="passwd"></param>
        /// <param name="databaseHost"></param>
        /// <param name="databasePort"></param>
        /// <param name="databaseService"></param>
        /// <param name="databaseSchema"></param>
        /// <param name="fetchSzMx"></param>
        /// <param name="multiConnect"></param>
        /// <param name="keyedConnect"></param>
        /// <param name="key"></param>
        public OracleDataAccessor(
            string userNm,
            string passwd,
            string databaseHost,
            string databasePort,
            string databaseService,
            string databaseSchema,
            uint fetchSzMx,
            bool multiConnect,
            bool keyedConnect,
            string key)
        {
            this.Initialized = false;
            this.fetchSizeMx = fetchSzMx;
            this.useMultiConnect = multiConnect;
            this.useKeyedConnect = keyedConnect;
            this.userName = userNm;
            this.password = passwd;
            this.dbHost = databaseHost;
            this.dbPort = databasePort;
            this.dbService = databaseService;
            this.dbSchema = databaseSchema;
            this.connectKeys = new Dictionary<string, int>();
            this.auxLogger = null;
            this.averageTxferRate = 0;
            this.totalBytesTransferred = 0;
            this.totalTransactions = 0;
            //Override to always use the same size fetch buffer
            if (this.fetchSizeMx >= 0)
            {
                this.fetchSizeMx = FETCHSZ_DEFAULT;
            }

            this.InitializeDbConnection(key);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        private void InitializeDbConnection(string key)
        {
            if (this.useMultiConnect)
            {
                if (!this.useKeyedConnect)
                {
                    var oMulti = OracleMultiDbConnection.Instance;
                    var threadId = Thread.CurrentThread.ManagedThreadId;
                    var dbStatus = oMulti.getStatus(threadId);
                    if (dbStatus == OracleMultiDbConnection.Status.CONNECTED ||
                        dbStatus == OracleMultiDbConnection.Status.INITIALIZED)
                    {
                        this.Initialized = true;
                        return;
                    }

                    if (dbStatus != OracleMultiDbConnection.Status.DISCONNECTED)
                    {
                        return;
                    }
                    this.Initialized = oMulti.initialize(
                        this.userName,
                        this.password,
                        this.dbHost,
                        this.dbPort,
                        this.dbService,
                        this.dbSchema,
                        threadId);
                }
                else if (this.useKeyedConnect && !string.IsNullOrEmpty(key) && !this.connectKeys.ContainsKey(key))
                {
                    var oMulti = OracleMultiDbConnection.Instance;
                    var connectId = key.GetHashCode();
                    this.connectKeys.Add(key, connectId);
                    var dbStatus = oMulti.getStatus(connectId);
                    if (dbStatus == OracleMultiDbConnection.Status.CONNECTED ||
                        dbStatus == OracleMultiDbConnection.Status.INITIALIZED)
                    {
                        this.Initialized = true;
                        return;
                    }

                    if (dbStatus != OracleMultiDbConnection.Status.DISCONNECTED)
                    {
                        return;
                    }
                    this.Initialized = oMulti.initialize(
                        this.userName,
                        this.password,
                        this.dbHost,
                        this.dbPort,
                        this.dbService,
                        this.dbSchema,
                        connectId);
                }
                else
                {
                    return;
                }
            }
            else
            {
                var oConn = OracleDbConnection.Instance;
                var dbStatus = oConn.DBStatus;
                if (dbStatus == OracleDbConnection.Status.CONNECTED ||
                    dbStatus == OracleDbConnection.Status.INITIALIZED)
                {
                    this.Initialized = true;
                    return;
                }

                if (dbStatus != OracleDbConnection.Status.DISCONNECTED)
                {
                    return;
                }
                this.Initialized = oConn.initialize(
                    this.userName, this.password, this.dbHost,
                    this.dbPort, this.dbService, this.dbSchema);                
            }
        }

        public void DisconnectDbConnection(string key)
        {
            if (!this.Initialized)
                return;

            if (this.useMultiConnect)
            {
                if (!this.useKeyedConnect)
                {
                    var oMulti = OracleMultiDbConnection.Instance;
                    var threadId = Thread.CurrentThread.ManagedThreadId;
                    var dbStatus = oMulti.getStatus(threadId);
                    if (dbStatus == OracleMultiDbConnection.Status.CONNECTED ||
                        dbStatus == OracleMultiDbConnection.Status.INITIALIZED)
                    {
                        oMulti.disconnect();
                        this.Initialized = false;
                        return;
                    }
                }
                else if (this.useKeyedConnect && !string.IsNullOrEmpty(key))
                {
                    var oMulti = OracleMultiDbConnection.Instance;
                    var keyId = CollectionUtilities.GetIfKeyValid(this.connectKeys, key, Int32.MinValue);
                    if (keyId != Int32.MinValue)
                    {
                        var dbStatus = oMulti.getStatus(keyId);
                        if (dbStatus == OracleMultiDbConnection.Status.CONNECTED ||
                            dbStatus == OracleMultiDbConnection.Status.INITIALIZED)
                        {
                            oMulti.disconnect(keyId);
                            this.Initialized = false;
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                var oConn = OracleDbConnection.Instance;
                var dbStatus = oConn.DBStatus;
                if (dbStatus == OracleDbConnection.Status.CONNECTED ||
                    dbStatus == OracleDbConnection.Status.INITIALIZED)
                {
                    oConn.disconnect();
                    this.Initialized = false;
                    return;
                }
            }
        }

        public void ReconnectDbConnection(string key)
        {
            if (!this.Initialized)
                return;

            if (this.useMultiConnect)
            {
                if (!this.useKeyedConnect)
                {
                    var oMulti = OracleMultiDbConnection.Instance;
                    var threadId = Thread.CurrentThread.ManagedThreadId;
                    var dbStatus = oMulti.getStatus(threadId);
                    if (dbStatus == OracleMultiDbConnection.Status.CONNECTED ||
                        dbStatus == OracleMultiDbConnection.Status.INITIALIZED)
                    {
                        oMulti.reconnect();
                        this.Initialized = true;
                        return;
                    }
                }
                else if (this.useKeyedConnect && !string.IsNullOrEmpty(key))
                {
                    var oMulti = OracleMultiDbConnection.Instance;
                    var keyId = CollectionUtilities.GetIfKeyValid(
                        this.connectKeys, key, Int32.MinValue);
                    if (keyId != Int32.MinValue)
                    {
                        var dbStatus = oMulti.getStatus(keyId);
                        if (dbStatus == OracleMultiDbConnection.Status.CONNECTED ||
                            dbStatus == OracleMultiDbConnection.Status.INITIALIZED)
                        {
                            oMulti.reconnect(keyId);
                            this.Initialized = true;
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                var oConn = OracleDbConnection.Instance;
                var dbStatus = oConn.DBStatus;
                if (dbStatus == OracleDbConnection.Status.CONNECTED ||
                    dbStatus == OracleDbConnection.Status.INITIALIZED)
                {
                    oConn.disconnect();
                    this.Initialized = false;
                    return;
                }
            }
        }

        /// <summary>
        /// Processes the client generic parameter structure list and converts
        /// each element into a valid corresponding OracleParameters
        /// </summary>
        /// <param name="parameters">Input parameters for the stored procedure call</param>
        /// <param name="command"></param>
        private bool ProcessOracleInputParameters(List<OracleProcParam> parameters,
            ref OracleCommand command)
        {
            if (CollectionUtilities.isEmpty(parameters))
            {
                return (false);
            }

            var i = 0;
            foreach (var curOParam in parameters)
            {
                if (curOParam == null)
                    continue;
                if (this.isloglevel(LogLevel.DEBUG))
                {                    
                    this.log(LogLevel.DEBUG, "OracleProcParam[{0}] = ({1},{2},{3},{4})", i, curOParam.Direction, (object)curOParam.DataName, curOParam.Name, curOParam.OracleType);
                    ++i;
                }

                curOParam.AddToCommand(ref command);
            }

            return (true);
        }

        /// <summary>
        /// Convenience method to set expected error fields easily to success or failure
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="eCode"></param>
        /// <param name="eText"></param>
        private void setErrorFieldsSuccess(bool isSuccess, 
            string eCode, string eText)
        {
            if (isSuccess)
            {
                this.ErrorCode = eCode ?? "0";
                this.ErrorDescription = eText ?? "Success";
            }
            else
            {
                this.ErrorCode = eCode ?? "-1";
                this.ErrorDescription = eText ?? "Failure";
            }
        }
        #endregion

        #region Flexible log methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msgFmt"></param>
        /// <param name="vars"></param>
        private void log(LogLevel level, string msgFmt, params object[] vars)
        {
            var fileLogger = FileLogger.Instance;
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
            var fileLogger = FileLogger.Instance;
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

        #endregion

        #region Public Command Methods

        public Status GetConnectionStatus(string key)
        {
            if (this.Initialized == false)
            {
                return (Status.DISCONNECTED);
            }

            if (this.useMultiConnect)
            {
                if (OracleMultiDbConnection.Instance != null)
                {
                    OracleMultiDbConnection.Status mStatus;
                    if (!this.useKeyedConnect)
                    {
                        mStatus = OracleMultiDbConnection.Instance.getStatus(
                                Thread.CurrentThread.ManagedThreadId);
                    }
                    else if (!string.IsNullOrEmpty(key))
                    {
                        mStatus = OracleMultiDbConnection.Instance.getStatus(
                                this.connectKeys[key]);
                    }
                    else
                    {
                        return (Status.DISCONNECTED);
                    }

                    if (mStatus == OracleMultiDbConnection.Status.INITIALIZED)
                        return (Status.INITIALIZED);
                    if (mStatus == OracleMultiDbConnection.Status.CONNECTED)
                        return (Status.CONNECTED);
                }
            }
            else
            {
                if (OracleDbConnection.Instance != null)
                {
                    var status = OracleDbConnection.Instance.DBStatus;
                    if (status == OracleDbConnection.Status.INITIALIZED)
                        return (Status.INITIALIZED);
                    if (status == OracleDbConnection.Status.CONNECTED)
                        return (Status.CONNECTED);
                }
            }

            return (Status.DISCONNECTED);
        }
        /// <summary>
        /// Begin database transaction
        /// </summary>
        /// <param name="isoLevel">Isolation level of the transaction block</param>
        /// <param name="key"></param>
        /// <returns>Success value of the operation</returns>
        public bool StartTransactionBlock(IsolationLevel isoLevel, string key)
        {
            if (this.Initialized == false)
                return (false);

            var rt = false;
            try
            {
                if (this.useMultiConnect)
                {
                    if (!this.useKeyedConnect)
                    {                        
                        rt = OracleMultiDbConnection.Instance.startTransactionBlock(
                                Thread.CurrentThread.ManagedThreadId, isoLevel);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(key) && this.connectKeys.ContainsKey(key))
                        {
                            rt = OracleMultiDbConnection.Instance.startTransactionBlock(
                                this.connectKeys[key], isoLevel);
                        }
                    }
                }
                else
                {
                    rt = OracleDbConnection.Instance.startTransactionBlock(isoLevel);
                }
            }
            catch (Exception eX)
            {
                if (this.isloglevel(LogLevel.ERROR))
                {
                    this.log(LogLevel.ERROR, "Cannot start transaction block. Exception thrown: Msg = {0} StackTrace = {1}", eX.Message,
                             eX.StackTrace ?? "No Stack Trace");
                }
                rt = false;
            }
            finally
            {
                if (this.isloglevel(LogLevel.DEBUG))
                {
                    this.log(LogLevel.DEBUG, "Status of start transaction block = {0}", rt);
                }
            }
            return (rt);
        }

        /// <summary>
        /// Commit the current transaction block
        /// </summary>
        /// <returns>Success value of the transaction commit operation</returns>
        public bool commitTransactionBlock(string key)
        {
            if (this.Initialized == false)
                return (false);

            var rt = false;
            try
            {
                if (this.useMultiConnect)
                {
                    if (!this.useKeyedConnect)
                    {
                        rt = OracleMultiDbConnection.Instance.commitTransactionBlock(
                                Thread.CurrentThread.ManagedThreadId);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(key) && this.connectKeys.ContainsKey(key))
                        {
                            rt = OracleMultiDbConnection.Instance.commitTransactionBlock(
                                this.connectKeys[key]);
                        }
                    }
                }
                else
                {
                    rt = OracleDbConnection.Instance.commitTransactionBlock();
                }
            }
            catch (Exception eX)
            {
                if (this.isloglevel(LogLevel.ERROR))
                {
                    this.log(LogLevel.ERROR, "Cannot commit transaction block. Exception thrown: Msg = {0} StackTrace = {1}", eX.Message,
                             eX.StackTrace ?? "No Stack Trace");
                }
                rt = false;
            }
            finally
            {
                if (this.isloglevel(LogLevel.DEBUG))
                {
                    this.log(LogLevel.DEBUG, "Status of commit transaction block = {0}", rt);
                }
            }
            return (rt);
        }

        /// <summary>
        /// Roll back the current transaction block
        /// </summary>
        /// <returns>Success value of the transaction roll back operation</returns>
        public bool rollbackTransactionBlock(string key)
        {
            if (this.Initialized == false)
                return (false);

            var rt = false;
            try
            {
                if (this.useMultiConnect)
                {
                    if (!this.useKeyedConnect)
                    {
                        rt = OracleMultiDbConnection.Instance.rollbackTransactionBlock(
                                Thread.CurrentThread.ManagedThreadId);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(key) && this.connectKeys.ContainsKey(key))
                        {
                            rt = OracleMultiDbConnection.Instance.rollbackTransactionBlock(
                                this.connectKeys[key]);
                        }
                    }
                }
                else
                {
                    rt = OracleDbConnection.Instance.rollbackTransactionBlock();
                }

            }
            catch (OracleException eX)
            {
                this.log(LogLevel.ERROR, "Cannot rollback transaction block. OracleException thrown: Msg = {0} StackTrace = {1}", eX.Message, eX.StackTrace ?? "No Stack Trace");
                this.ErrorCode = "ORA-" + eX.Number;
                this.ErrorDescription = "Cannot rollback transaction block (OracleException)";
                return (false);
            }
            catch(Exception e)
            {
                this.log(LogLevel.ERROR, "Cannot rollback transaction block. OracleException thrown: Msg = {0} StackTrace = {1}", e.Message, e.StackTrace ?? "No Stack Trace");
                this.ErrorCode = e.ToString();
                this.ErrorDescription = "Cannot rollback transaction block (Exception)";
                return (false);
            }
            finally
            {
                this.log(LogLevel.DEBUG, "Status of rollback transaction block = {0}", rt);
            }
            return (rt);
        }

        /// <summary>
        /// Determines whether we are in a transaction block or not
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool inTransactionBlock(string key)
        {
            if (this.Initialized == false)
                return (false);

            var rt = false;
            try
            {
                if (this.useMultiConnect)
                {
                    if (!this.useKeyedConnect)
                    {
                        rt = OracleMultiDbConnection.Instance.inTransactionBlock(
                                Thread.CurrentThread.ManagedThreadId);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(key) && this.connectKeys.ContainsKey(key))
                        {
                            rt = OracleMultiDbConnection.Instance.inTransactionBlock(
                                this.connectKeys[key]);
                        }
                    }
                }
                else
                {
                    rt = OracleDbConnection.Instance.inTransactionBlock();
                }

            }
            catch (OracleException eX)
            {
                this.log(LogLevel.ERROR, "Cannot determine if within a transaction block. OracleException thrown: Msg = {0} StackTrace = {1}", eX.Message, eX.StackTrace ?? "No Stack Trace");
                this.ErrorCode = "ORA-" + eX.Number;
                this.ErrorDescription = "Cannot determine if within a transaction block (OracleException)";
                return (false);
            }
            catch(Exception e)
            {
                this.log(LogLevel.ERROR, "Cannot determine if within a transaction block. OracleException thrown: Msg = {0} StackTrace = {1}", e.Message, e.StackTrace ?? "No Stack Trace");
                this.ErrorCode = e.ToString();
                this.ErrorDescription = "Cannot determine if within a transaction block (Exception)";
                return (false);
            }
            finally
            {
                this.log(LogLevel.DEBUG, "Status of within transaction block = {0}", rt);
            }
            return (rt);
        }

        /// <summary>
        /// Issues a sql select to the oracle database.  Allows user to pass
        /// a simple string sql statement
        /// </summary>
        /// <param name="schemaName"></param>
        /// <param name="tableName"></param>
        /// <param name="columnSelector"></param>
        /// <param name="joinClause"></param>
        /// <param name="whereConditions"></param>
        /// <param name="comBehave"></param>
        /// <param name="key"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public bool issueSqlTextSelectCommand(
            string schemaName,
            string tableName,
            string columnSelector,
            string joinClause,
            string whereConditions,
            CommandBehavior comBehave,
            string key,
            out DataTable dataTable)
        {
            dataTable = null;
            if (string.IsNullOrEmpty(schemaName) ||
                string.IsNullOrEmpty(tableName))
            {
                this.CallSuccess = false;
                return (false);
            }

            //Build the query string
            var sbuilder = new StringBuilder();
            sbuilder.Append(Q_SELECT);
            sbuilder.Append(columnSelector ?? AST);
            sbuilder.Append(Q_FROM);
            sbuilder.Append(schemaName);
            sbuilder.Append(DOT);
            sbuilder.Append(tableName);
            if (!string.IsNullOrEmpty(joinClause))
            {
                sbuilder.Append(SPC);
                sbuilder.Append(joinClause);
            }
            if (!string.IsNullOrEmpty(whereConditions))
            {
                sbuilder.Append(Q_WHERE);
                sbuilder.Append(whereConditions);
            }

            //Create query string
            var queryString = sbuilder.ToString();
            return (this.issueSqlTextSelectCommand(queryString, tableName, comBehave, key, out dataTable));
        }

        /// <summary>
        /// Issues a sql select to the oracle database.  Allows user to pass
        /// a simple string sql statement
        /// </summary>
        /// <param name="sqlString">The sql query</param>
        /// <param name="tableName">The table name</param>
        /// <param name="comBehave">The .NET command behavior specification for the query</param>
        /// <param name="key"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public bool issueSqlTextSelectCommand(
            string sqlString, 
            string tableName,
            CommandBehavior comBehave,
            string key,
            out DataTable dataTable)
        {
            //Set outward data table to null initially
            dataTable = null;
            //Ensure that incoming query string is valid
            if (string.IsNullOrEmpty(sqlString))
            {
                this.CallSuccess = false;
                return (false);
            }

            //Create query string
            var queryString = sqlString;

            //Create data adapter
            OracleDataReader oraDataReader;
            this.CallSuccess = false;
            OracleCommand command = null;
            try
            {
                //Build command
                if (this.useMultiConnect)
                {
                    if (!this.useKeyedConnect)
                    {
                        command = OracleMultiDbConnection.Instance.acquireCommand(queryString,
                                Thread.CurrentThread.ManagedThreadId);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(key) && this.connectKeys.ContainsKey(key))
                        {
                            command = OracleMultiDbConnection.Instance.acquireCommand(queryString,
                                this.connectKeys[key]);
                        }
                    }
                }
                else
                {
                    command = OracleDbConnection.Instance.acquireCommand(queryString);
                }

                if (command == null)
                {
                    this.CallSuccess = false;
                    return (false);
                }

                //Execute command
                oraDataReader = command.ExecuteReader(comBehave);

                //Change fetch size dynamically based on row size
                if (command.RowSize > 0L)
                {
 
                        oraDataReader.FetchSize = command.RowSize * FETCHROWS_DEFAULT;
                    
  
                }

                //Set call success
                this.CallSuccess = true;
            }
            catch (OracleException oEx)
            {
                this.log(LogLevel.ERROR, 
                    "Could not execute query: {0} Oracle exception thrown: Code: {1} Details: {2} {3}", 
                    sqlString, oEx.ErrorCode, oEx.Message, oEx.StackTrace ?? "No Stack Trace");
                BasicExceptionHandler.Instance.AddException(string.Format("Trying to execute simple SQL statement: {0}", oEx.Message), oEx);
                return (false);
            }

            //Process reader, capture data, and return offline data table
            if (this.CallSuccess && oraDataReader.RowSize > 0 && oraDataReader.HasRows)
            {
                //Create data table with same table name as the one being queried
                dataTable = new DataTable(tableName);

                //Load the data table with the reader data
                dataTable.Load(oraDataReader, LoadOption.OverwriteChanges);
            }

            return (this.CallSuccess);
        }

        /// <summary>
        /// Issues a sql select to the oracle database.  Allows user to pass
        /// a simple string sql statement
        /// </summary>
        /// <param name="sqlString">The sql query</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool issueSqlTextInsertUpdateDeleteCommand(
            string sqlString,
            string key)
        {
            //Ensure that incoming query string is valid
            if (string.IsNullOrEmpty(sqlString))
            {
                this.CallSuccess = false;
                return (false);
            }

            //Create query string
            var queryString = sqlString;

            this.CallSuccess = false;
            OracleCommand command = null;
            try
            {
                //Build command
                if (this.useMultiConnect)
                {
                    if (!this.useKeyedConnect)
                    {
                        command = OracleMultiDbConnection.Instance.acquireCommand(queryString,
                                Thread.CurrentThread.ManagedThreadId);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(key) && this.connectKeys.ContainsKey(key))
                        {
                            command = OracleMultiDbConnection.Instance.acquireCommand(queryString,
                                this.connectKeys[key]);
                        }
                    }
                }
                else
                {
                    command = OracleDbConnection.Instance.acquireCommand(queryString);
                }

                if (command == null)
                {
                    this.CallSuccess = false;
                    return (false);
                }

                //Execute command
                int rowsAffected = command.ExecuteNonQuery();

                //Log number rows affected
                this.log(LogLevel.INFO, "IUD Type Query {0} executed.  Affected {1} row(s).", queryString, rowsAffected);

                //Set call success);
                this.CallSuccess = true;
            }
            catch (OracleException oEx)
            {
                this.log(LogLevel.ERROR, "Could not execute query: {0} Oracle exception thrown: {1}", sqlString, oEx.ToString());
                BasicExceptionHandler.Instance.AddException("Trying to execute simple SQL statement: " + oEx.Message, oEx);
                return (false);
            }

            return (this.CallSuccess);
        }

        /// <summary>
        /// Wrapper to stored procedure call that leaves out the schema name
        /// and assumes a default success value
        /// </summary>
        /// <param name="procPackage"></param>
        /// <param name="procName"></param>
        /// <param name="inParameters"></param>
        /// <param name="refCursorParamNames"></param>
        /// <param name="errorNumberParamName"></param>
        /// <param name="errorDescParamName"></param>
        /// <param name="keyValue"></param>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public bool issueSqlStoredProcCommand(
            string procPackage,
            string procName,
            List<OracleProcParam> inParameters,
            List<PairType<string, string>> refCursorParamNames,
            string errorNumberParamName,
            string errorDescParamName,
            string keyValue,
            out DataSet dataSet)
        {
            return (this.issueSqlStoredProcCommand(
                string.Empty, procPackage, procName,
                inParameters, refCursorParamNames,
                errorNumberParamName,
                errorDescParamName, DEFAULT_SUCCESS,
                keyValue,
                out dataSet));
        }

        /// <summary>
        /// Wrapper to stored procedure call that leaves out the schema name,
        /// assumes a default success value, and uses the null key
        /// </summary>
        /// <param name="procPackage"></param>
        /// <param name="procName"></param>
        /// <param name="inParameters"></param>
        /// <param name="refCursorParamNames"></param>
        /// <param name="errorNumberParamName"></param>
        /// <param name="errorDescParamName"></param>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public bool issueSqlStoredProcCommand(
            string procPackage,
            string procName,
            List<OracleProcParam> inParameters,
            List<PairType<string, string>> refCursorParamNames,
            string errorNumberParamName,
            string errorDescParamName,
            out DataSet dataSet)
        {
            return (this.issueSqlStoredProcCommand(
                string.Empty, procPackage, procName,
                inParameters, refCursorParamNames,
                errorNumberParamName,
                errorDescParamName, DEFAULT_SUCCESS,
                null,
                out dataSet));
        }

        /// <summary>
        /// Wrapper to stored procedure call that assumes
        /// a default success value and allows
        /// for a key specification
        /// </summary>
        /// <param name="procSchema"></param>
        /// <param name="procPackage"></param>
        /// <param name="procName"></param>
        /// <param name="inParameters"></param>
        /// <param name="refCursorParamNames"></param>
        /// <param name="errorNumberParamName"></param>
        /// <param name="errorDescParamName"></param>
        /// <param name="keyValue"></param>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public bool issueSqlStoredProcCommand(
            string procSchema,
            string procPackage,
            string procName,
            List<OracleProcParam> inParameters,
            List<PairType<string, string>> refCursorParamNames,
            string errorNumberParamName,
            string errorDescParamName,            
            string keyValue,
            out DataSet dataSet)
        {
            return (this.issueSqlStoredProcCommand(
                procSchema, procPackage, procName,
                inParameters, refCursorParamNames,
                errorNumberParamName,
                errorDescParamName, DEFAULT_SUCCESS,
                keyValue,
                out dataSet));
        }

        /// <summary>
        /// Wrapper to stored procedure call that utilizes
        /// default success value and no key specification
        /// </summary>
        /// <param name="procSchema"></param>
        /// <param name="procPackage"></param>
        /// <param name="procName"></param>
        /// <param name="inParameters"></param>
        /// <param name="refCursorParamNames"></param>
        /// <param name="errorNumberParamName"></param>
        /// <param name="errorDescParamName"></param>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public bool issueSqlStoredProcCommand(
            string procSchema,
            string procPackage,
            string procName,
            List<OracleProcParam> inParameters,
            List<PairType<string, string>> refCursorParamNames,
            string errorNumberParamName,
            string errorDescParamName,
            out DataSet dataSet)
        {
            return (this.issueSqlStoredProcCommand(
                procSchema, procPackage, procName,
                inParameters, refCursorParamNames,
                errorNumberParamName,
                errorDescParamName, DEFAULT_SUCCESS,
                null,
                out dataSet));
        }

        /// <summary>
        /// Executes an Oracle stored procedure
        /// </summary>
        /// <param name="procSchema"></param>
        /// <param name="procPackage"></param>
        /// <param name="procName"></param>
        /// <param name="inParameters"></param>
        /// <param name="refCursorParamNames"></param>
        /// <param name="errorNumberParamName"></param>
        /// <param name="errorDescParamName"></param>
        /// <param name="successVal"></param>
        /// <param name="keyValue"></param>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public bool issueSqlStoredProcCommand(
            string procSchema,
            string procPackage,
            string procName,
            List<OracleProcParam> inParameters,
            List<PairType<string, string>> refCursorParamNames,
            string errorNumberParamName,
            string errorDescParamName,
            string successVal,
            string keyValue,
            out DataSet dataSet)
        {
            //Set data set to null initially
            dataSet = null;
            var successValue = DEFAULT_SUCCESS; 
            if (string.IsNullOrEmpty(procName))
            {
                return (false);
            }

            //Determine if we are in normal operational
            //mode of the app.  If we are not, we need to
            //occlude the schema name
            var pSchema = procSchema;
            var storeMode = string.Empty;
            if (SecurityAccessor.Instance == null ||
                SecurityAccessor.Instance.EncryptConfig == null ||
                SecurityAccessor.Instance.EncryptConfig.ClientConfig == null ||
                SecurityAccessor.Instance.EncryptConfig.ClientConfig.StoreConfiguration == null)
            {
                storeMode = EncryptedConfigContainer.STOREMODE_NORMAL;
            }
            else
            {
                storeMode = SecurityAccessor.Instance.EncryptConfig.ClientConfig.StoreConfiguration.StoreMode;
            }
            if (!string.IsNullOrEmpty(storeMode) && 
                !storeMode.Equals(EncryptedConfigContainer.STOREMODE_NORMAL, 
                    StringComparison.Ordinal))
            {
                pSchema = string.Empty;
            }

            if (this.isloglevel(LogLevel.DEBUG))
            {
                if (string.IsNullOrEmpty(pSchema))
                {
                    this.log(LogLevel.DEBUG,
                             "Stored Proc Call({0}.{1}) - Store Mode -{2}", 
                             (procPackage ?? "null"), (procName), storeMode);
                }
                else
                {
                    this.log(LogLevel.DEBUG,
                             "Stored Proc Call({0}.{1}.{2}) - Store Mode -{3}", 
                             (procSchema ?? "null"), (procPackage ?? "null"), (procName), storeMode);
                }
            }

            var sbuilder = new StringBuilder();
            //Use the potentially occluded schema name based
            //on the comparison made above
            if (!string.IsNullOrEmpty(pSchema))
            {
                sbuilder.Append(pSchema);
                sbuilder.Append(DOT);
            }
            if (!string.IsNullOrEmpty(procPackage))
            {
                sbuilder.Append(procPackage);
                sbuilder.Append(DOT);
            }
            sbuilder.Append(procName);
            var encCfg = SecurityAccessor.Instance.EncryptConfig;
            var capData = (encCfg != null && encCfg.AppType == PawnSecApplication.Cashlinx && encCfg.ClientConfig.ClientConfiguration.CPNHSEnabled) || isloglevel(LogLevel.DEBUG);
            //Start overall method timer
            var issueTimeStart = DateTime.MinValue;
            if (capData)
            {
                issueTimeStart = DateTime.Now;
            }

            //Get success value if valid
            if (!string.IsNullOrEmpty(successVal))
            {
                successValue = successVal;
            }

            //Create query string
            var queryString = sbuilder.ToString();
            if (this.isloglevel(LogLevel.DEBUG))
            {
                this.log(LogLevel.DEBUG, "- Query string used to acquire command = {0}", queryString);
            }

            //Create oracle command
            OracleCommand command;
            try
            {
                if (this.useMultiConnect)
                {
                    if (!this.useKeyedConnect)
                    {
                        //Build command
                        command = OracleMultiDbConnection.Instance.acquireCommand(
                            queryString, Thread.CurrentThread.ManagedThreadId);
                    }
                    else if (this.useKeyedConnect && !string.IsNullOrEmpty(keyValue))
                    {
                        //Build command
                        command = OracleMultiDbConnection.Instance.acquireCommand(
                            queryString, this.connectKeys[keyValue]);
                    }
                    else
                    {
                        if (this.isloglevel(LogLevel.ERROR))
                        {
                            this.log(LogLevel.ERROR,
                                     "Exception thrown while attempting to acquire command object in stored proc call, no key/thread id found in multi-connect {0}",
                                     queryString);
                            this.setErrorFieldsSuccess(false, null, null);
                        }
                        return (false);
                    }
                }
                else
                {
                    command = OracleDbConnection.Instance.acquireCommand(queryString);
                }
            }
            catch (OracleException oEx)
            {
                this.log(LogLevel.ERROR, "OracleException thrown while attempting to acquire command object in stored proc call {0}: Msg = {1}, Code = {2}, StackTrace = {3}", queryString, oEx.Message, oEx.ErrorCode, oEx.StackTrace ?? "No Stack Trace");
                this.setErrorFieldsSuccess(false, null, null);
                return (false);
            }
            catch(Exception eX)
            {
                this.log(
                    LogLevel.ERROR,
                    "Exception thrown while attempting to acquire command object in stored proc call {0}: Msg = {1}, StackTrace = {2}",
                    queryString,
                    eX.Message,
                    eX.StackTrace ?? "No Stack Trace");
                this.setErrorFieldsSuccess(false, null, null);
                return (false);
            }
            finally
            {
                this.log(LogLevel.DEBUG, "Finally block executed during command retrieval in stored proc call {0}", queryString);
            }

            //If the command is null, return false
            if (command == null)
            {
                this.log(LogLevel.FATAL, "Command object is null in stored proc call: {0}", queryString);
                this.setErrorFieldsSuccess(false, null, null);
                return (false);
            }

            //Set command to execute procedure
            command.CommandType = CommandType.StoredProcedure;

            //Create procedure parameter inputs if necessary
            var hasOutputParams = false;
            List<PairType<int, string>> outputParamNames = null;
            if (CollectionUtilities.isNotEmpty(inParameters))
            {
                //Process the parameters passed in
                if (!this.ProcessOracleInputParameters(inParameters, ref command))
                {
                    this.log(LogLevel.ERROR, "Could not process stored procedure input parameters in stored proc call: {0}", queryString);
                    this.setErrorFieldsSuccess(false, null, null);
                    return (false);
                }
                var i = 0;
                foreach (var o in inParameters)
                {
                    if (o == null || o.Direction == ParameterDirection.Input)
                    {
                        if (o != null && this.isloglevel(LogLevel.DEBUG))
                        {
                            this.log(LogLevel.DEBUG, "Input Param Name [{0}] = {1}", i, o.Name);
                        }
                        i++;
                        continue;
                    }
                    if (outputParamNames == null)
                    {
                        outputParamNames = new List<PairType<int, string>>();
                    }
                    outputParamNames.Add(new PairType<int, string>(i, o.Name));
                    this.log(LogLevel.DEBUG, "Output Param Name [{0}] = {1}", i, o.Name);
                    i++;
                }
            }
            if (CollectionUtilities.isNotEmpty(outputParamNames))
            {
                hasOutputParams = true;
            }

            //Add the oracle ref cursor if required
            var useRefCursors = false;
            if (CollectionUtilities.isNotEmpty(refCursorParamNames))
            {
                useRefCursors = true;
                var refIdx=0;
                foreach (var pS in refCursorParamNames)
                {
                    if (pS == null)
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(pS.Left))
                    {
                        continue;
                    }
                    refIdx++;
                    command.Parameters.Add(pS.Left, OracleDbType.RefCursor, DBNull.Value, ParameterDirection.Output);
                    if (this.isloglevel(LogLevel.DEBUG))
                    {
                        this.log(LogLevel.DEBUG, "RefCursor Param Name [{0}] = ({1}, {2})", refIdx, pS.Left, pS.Right);
                    }
                }
            }

            //Add error code if required
            var useErrorParamName = false;
            var errorParamNameIdx = -1;
            if (!string.IsNullOrEmpty(errorNumberParamName))
            {
                useErrorParamName = true;
                errorParamNameIdx = command.Parameters.Count;
                command.Parameters.Add(errorNumberParamName, OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output);
                this.log(LogLevel.DEBUG, "Error Code Param Name = {0}", errorNumberParamName);
            }

            //Add error description if required
            var useErrorDescName = false;
            var errorDescNameIdx = -1;
            if (!string.IsNullOrEmpty(errorDescParamName))
            {
                useErrorDescName = true;
                errorDescNameIdx = command.Parameters.Count;
                command.Parameters.Add(errorDescParamName, OracleDbType.Varchar2, 32768, DBNull.Value, ParameterDirection.Output);
                this.log(LogLevel.DEBUG, "Error Description Param Name = {0}", errorDescParamName);
            }

            if (this.isloglevel(LogLevel.DEBUG))
            {
                var paramDump = new StringBuilder();
                paramDump.AppendLine();
                paramDump.AppendLine("ParamDump:");
                paramDump.AppendLine("Name | Type | Value");
                var k = 0;
                foreach (OracleParameter zParam in command.Parameters)
                {
                    if (zParam == null)
                    {
                        ++k;
                        continue;
                    }
                    paramDump.Append(zParam.ParameterName);
                    paramDump.Append(" | ");
                    paramDump.Append(zParam.OracleDbType.ToString());
                    paramDump.Append(" | ");
                    if (zParam.Value != null && zParam.Value != DBNull.Value)
                    {
                        if (!inParameters[k].IsArray && zParam.Value != null)
                        {
                            paramDump.AppendLine(zParam.Value.ToString());
                        }
                        else
                        {
                            if (zParam != null && zParam.Size > 0)
                            {
                                for (var j = 0; j < zParam.Size; ++j)
                                {
                                    paramDump.Append(inParameters[k][j] ?? "null,");
                                }
                            }
                            paramDump.AppendLine();
                        }

                    }
                    else
                    {
                        paramDump.AppendLine("null");
                    }
                    ++k;
                }
                this.log(LogLevel.DEBUG, paramDump.ToString());
            }

            //Execute the stored procedure
            var dtStart = DateTime.Now;
            this.CallSuccess = false;
            try
            {
                command.ExecuteNonQuery();
                this.CallSuccess = true;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("Exception thrown while calling stored procedure " + queryString + ": " + oEx.Message, oEx);
                this.log(LogLevel.ERROR, "Stored procedure call {0} failed: {1}", queryString, oEx.Message);
                return (false);
            }

            //Short circuit the method if we did not succeed with the call
            if (!this.CallSuccess)
            {
                BasicExceptionHandler.Instance.AddException(null, new ApplicationException(string.Format("Call to the stored procedure: {0} failed.", queryString)));
                this.log(LogLevel.ERROR, "Call to stored procedure {0} failed, call success flag set to false", queryString);
                return(false);
            }
            var dtEnd = DateTime.Now;
            var dtTotal = dtEnd - dtStart;

            var totalDataBytes = 0L;
            var refCursorTimeStart = dtEnd;
            var refCursorTimeEnd = dtEnd;
            if (useRefCursors)
            {
                //Create the data set based on the proc name
                dataSet = new DataSet(procName);
                var i = 0;
                foreach (var pS in refCursorParamNames)
                {
                    if (pS == null)
                    {
                        continue;
                    }

                    //Acquire oracle ref cursor
                    var oraRefCursor = (OracleRefCursor)command.Parameters[inParameters.Count + i].Value;
                    if (oraRefCursor != null && oraRefCursor.IsNull == false)
                    {
                        var odr = oraRefCursor.GetDataReader();
                        if (odr.HasRows)
                        {
                            //Modify the fetch size based on the row size
                            odr.FetchSize = oraRefCursor.RowSize * this.fetchSizeMx;
                            //Create the data table within the data set named based on the 
                            //passed in data table name maps
                            var dataTable = dataSet.Tables.Add(pS.Right);
                            //Load the data table using the data reader from the cursor
                            dataTable.Load(odr, LoadOption.OverwriteChanges);
                            var dataBytes = oraRefCursor.RowSize * dataTable.Rows.Count;
                            totalDataBytes += dataBytes;
                            if (this.isloglevel(LogLevel.DEBUG))
                            {
                                this.log(LogLevel.DEBUG, "REFCURSOR({0}, {1}) is not null and contains data - row size {2}, number rows {3}, total bytes {4}", pS.Left, pS.Right, oraRefCursor.RowSize, dataTable.Rows.Count, dataBytes);
                            }
                        }
                        else
                        {
                            this.log(LogLevel.WARN, "REFCURSOR({0}, {1}) is not null and does not contain data", pS.Left, pS.Right);
                        }

                        //Cleanup reader and cursor resources
                        odr.Close();
                        odr.Dispose();
                        oraRefCursor.Dispose();
                    }
                    else
                    {
                        this.log(LogLevel.WARN, "REFCURSOR({0}, {1}) is null", pS.Left, pS.Right);
                    }
                    i++;
                }
                //Set time after refcursor load completed
                refCursorTimeEnd = DateTime.Now;
                if (this.isloglevel(LogLevel.DEBUG))
                {
                    this.log(LogLevel.DEBUG, "Total bytes received from stored procedure {0} = {1}", queryString, totalDataBytes);
                }
            }
            var refCursorTotal = refCursorTimeEnd - refCursorTimeStart;

            //Get output parameters if they exist
            if (hasOutputParams)
            {
                if (dataSet == null)
                {
                    dataSet = new DataSet();                    
                }
                var dT = dataSet.Tables.Add(OUTPUT);
                dT.Columns.Add(NAME);
                dT.Columns.Add(VALUE);
                foreach (var p in outputParamNames)
                {
                    if (p == null)
                        continue;

                    var oParam = command.Parameters[p.Left];
                    var dR = dT.NewRow();
                    dR[NAME] = p.Left;
                    dR[VALUE] = oParam.Value.ToString();
                    dT.Rows.Add(dR);
                    if (this.isloglevel(LogLevel.DEBUG))
                    {
                        this.log(LogLevel.DEBUG,
                                "Output Param ({0} = {1}) - Successfully passed back",
                                p.Right ?? (p.Left.ToString()),
                                oParam.Value);
                    }
                }
            }

            //Get the error codes and descriptions
            if (useErrorDescName)
            {
                var oraStr = (OracleString)command.Parameters[errorDescNameIdx].Value;
                this.ErrorDescription = oraStr.ToString();
                this.log(LogLevel.DEBUG, "Error Description = " + this.ErrorDescription);
            }

            if (useErrorParamName)
            {
                var oraErrCode = command.Parameters[errorParamNameIdx].Value;
                if (oraErrCode != null)
                {
                    this.ErrorCode = oraErrCode.ToString();
                }
                else
                {
                    this.ErrorCode = string.Empty;
                }
                this.log(LogLevel.DEBUG, "Error Code = " + this.ErrorCode);
            }

            //Check the error code for the proper success value
            if (!string.IsNullOrEmpty(this.ErrorCode))
            {
                if (!this.ErrorCode.Equals(successValue))
                {
                    this.log(LogLevel.ERROR, "Error code from stored procedure indicates that an error has occurred (code={0}, desc={1})", ErrorCode, ErrorDescription);
                    this.CallSuccess = false;
                    var msg = string.Format("Stored procedure call {0} failed internally (code={1}, {2})", queryString, ErrorCode, ErrorDescription);
                    BasicExceptionHandler.Instance.AddException(msg, new ApplicationException(msg));
                }
            }

            //Finish issue time calculation
            if (capData)
            {
                var issueTimeEnd = DateTime.Now;
                var issueSpan = issueTimeEnd - issueTimeStart;
                var issueMs = issueSpan.TotalMilliseconds;
                var dtMs = dtTotal.TotalMilliseconds;
                var issueOverhead = issueMs - dtMs;
                if (this.isloglevel(LogLevel.DEBUG))
                {
                    this.log(LogLevel.DEBUG, "Stored proc issue time         = {0} ms", issueMs);
                    this.log(LogLevel.DEBUG, "Stored proc call time          = {0} ms", dtMs);
                    this.log(LogLevel.DEBUG, "Issue overhead (total - call)  = {0} ms", issueOverhead);
                }
                /*if (totalDataBytes > 0 && dtMs >= 1)
                {
                    this.log(LogLevel.DEBUG, "Total data output            = {0} bytes", totalDataBytes);
                    var transmitRate = totalDataBytes / (dtMs * SEC_MX);
                    this.log(LogLevel.DEBUG, "Data transfer rate           = {0} bytes per second", transmitRate);
                }*/
                if (useRefCursors && totalDataBytes > 0 && refCursorTotal.TotalMilliseconds > 0)
                {
                    //Increment performance amounts
                    this.totalTransactions++;
                    this.totalBytesTransferred += totalDataBytes;
                    this.totalTxferTime += refCursorTotal.TotalSeconds;
                    var avgBytesTxferred = this.totalBytesTransferred / this.totalTransactions;
                    var avgTxferTime = this.totalTxferTime / this.totalTransactions;
                    this.averageTxferRate = avgBytesTxferred / avgTxferTime;
                    var txferRate = totalDataBytes / refCursorTotal.TotalSeconds;
                    
                    //Add CPNHS call
                    var cpCtrl = CPNHSController.Instance;
                    var cpNHSEnabled = (GlobalDataAccessor.Instance != null && GlobalDataAccessor.Instance.DesktopSession != null && encCfg != null && encCfg.ClientConfig.ClientConfiguration.CPNHSEnabled);
                    var userId = (cpNHSEnabled) ? (GlobalDataAccessor.Instance.DesktopSession.UserName ?? "admin") : string.Empty;
                    var prepTime = dtStart - issueTimeStart;
                    if (!string.IsNullOrEmpty(userId))
                    {
                        if (procName.IndexOf("get_cust_detail", StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            if (cpNHSEnabled)
                            {
                                cpCtrl.AddGranularStoredProcData(
                                    userId, procName,
                                    (long)Math.Ceiling(prepTime.TotalMilliseconds),
                                    (long)Math.Ceiling(dtMs),
                                    (long)Math.Ceiling(refCursorTotal.TotalMilliseconds),
                                    (long)(Math.Ceiling(issueMs)),
                                    totalDataBytes);
                            }                        
                        }
                        cpCtrl.AddStoredProcCall(userId, procName, (long)Math.Ceiling(issueMs), totalDataBytes, (long)Math.Ceiling(dtMs));
                    }

                    //Log performance data
                    if (this.isloglevel(LogLevel.DEBUG))
                    {
                        var sb = new StringBuilder(256);
                        sb.AppendLine();
                        sb.AppendLine("*** Current Ref Cursor Rates & Totals ***");
                        sb.AppendFormat("Inst. ref cursor txfer size   = {0} bytes{1}", totalDataBytes, Environment.NewLine);
                        sb.AppendFormat("Inst. ref cursor txfer time   = {0} ms{1}", refCursorTotal.TotalMilliseconds, Environment.NewLine);
                        sb.AppendFormat("Inst. ref cursor txfer rate   = {0} bytes per second{1}", txferRate, Environment.NewLine);

                        sb.AppendLine("*** Running Ref Cursor Averages & Totals ***");
                        sb.AppendFormat("Average bytes transferred     = {0} bytes{1}", avgBytesTxferred, Environment.NewLine);
                        sb.AppendFormat("Average transfer time         = {0} seconds{1}", avgTxferTime, Environment.NewLine);
                        sb.AppendFormat("Average ref cursor txfer rate = {0} bytes per second{1}", this.averageTxferRate, Environment.NewLine);
                        sb.AppendFormat("Total bytes transferred       = {0} bytes{1}", this.totalBytesTransferred, Environment.NewLine);
                        sb.AppendFormat("Total transfer time           = {0} seconds{1}", this.totalTxferTime, Environment.NewLine);
                        sb.AppendFormat("Total ref cursor transactions = {0} transactions{1}", this.totalTransactions, Environment.NewLine);
                        sb.AppendLine();
                        this.log(LogLevel.DEBUG, "Performance Data: {0}", sb.ToString());
                    }
                }
            }

            if (this.isloglevel(LogLevel.DEBUG))
            {
                this.log(LogLevel.DEBUG, 
                    "Stored Proc Call({0}.{1}.{2}) Complete - Returning {3}", 
                    (procSchema ?? "null"), (procPackage ?? "null"), (procName ?? "null"), 
                    this.CallSuccess);
            }
            return (this.CallSuccess);
        }

        #endregion
    }
}
 