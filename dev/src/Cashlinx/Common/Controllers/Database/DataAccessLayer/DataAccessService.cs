using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Performance;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

//using Oracle.DataAccess.Types;

namespace Common.Controllers.Database.DataAccessLayer
{
    /// <summary>
    /// Container class for a row instance of data from the data result set
    /// </summary>
    public class DataReturnSetRow
    {
        private DataReturnSet parentRef;
        private List<object> currentRow;

        public DataReturnSetRow(DataReturnSet rS, List<object> rowData)
        {
            this.parentRef = rS;
            this.currentRow = rowData;
        }

        public object GetData(string name)
        {
            if (string.IsNullOrEmpty(name)) return (null);
            var idx = parentRef.Columns[name];
            return (this.currentRow[idx]);
        }

        public object GetData(int nameIdx)
        {
            if (nameIdx < 0 || nameIdx >= this.currentRow.Count) return (null);
            return (this.currentRow[nameIdx]);
        }

        public bool GetData(int nameIdx, out object data)
        {
            data = null;
            if (nameIdx < 0 || nameIdx >= this.currentRow.Count) return (false);
            data = this.currentRow[nameIdx];
            return (true);
        }
    }

    /// <summary>
    /// Class which holds the data returned from the database accessor
    /// </summary>
    public class DataReturnSet : IEnumerable
    {
        private DataTable returnTable;
        private readonly Dictionary<string, int> columns;
        private readonly List<List<object>> data;
        private readonly int numRows;
        private bool tableOnly;

        public Dictionary<string, int> Columns
        {
            get
            {
                if (!this.tableOnly)
                {
                    return (this.columns);
                }
                return (null);
            }
        }

        public List<List<object>> Rows
        {
            get
            {
                if (!this.tableOnly)
                {
                    return (this.data);
                }
                return (null);
            }
        }

        public int NumberRows
        {
            get
            {
                return (this.numRows);
            }
        }

        public bool IsTableOnly
        {
            get
            {
                return (this.tableOnly);
            }
        }

        public DataTable ReturnTable
        {
            get
            {
                if (this.tableOnly)
                {
                    return (this.returnTable);
                }
                return (null);
            }
        }

        public DataReturnSet(DataTable rTable)
        {
            this.returnTable = rTable;
            this.tableOnly = true;
            this.numRows = this.returnTable.Columns.Count;
        }

        public DataReturnSet(Dictionary<string, int> cols,
            List<List<object>> drows)
        {
            this.columns = cols;
            this.data = drows;
            this.numRows = this.data.Count;
            this.tableOnly = false;
        }

        public bool GetRow(int rowIdx, out List<object> rowData)
        {
            rowData = null;
            if (this.tableOnly) return (false);
            if (rowIdx < 0 || rowIdx >= this.numRows) return (false);

            rowData = this.data[rowIdx];

            return (true);
        }

        public bool GetRow(int rowIdx, out DataReturnSetRow returnSetRow)
        {
            returnSetRow = null;
            if (this.tableOnly) return (false);
            if (rowIdx < 0 || rowIdx >= this.numRows) return (false);
            returnSetRow = new DataReturnSetRow(this, data[rowIdx]);
            return (true);
        }

        public bool GetCell(int rowIdx, string name, out object cell)
        {
            cell = null;
            if (this.tableOnly) return (false);
            if (rowIdx < 0 || rowIdx >= this.numRows) return (false);
            if (!this.columns.ContainsKey(name)) return (false);

            cell = this.data[rowIdx][this.columns[name]];
            return (true);
        }

        [Obsolete]
        public object GetCell(int rowIdx, string name)
        {
            if (rowIdx < 0 || rowIdx >= this.numRows) return (null);
            if (!this.columns.ContainsKey(name)) return (null);
            return (this.data[rowIdx][this.columns[name]]);
        }

        public int GetColumnIndex(string colName)
        {
            if (this.tableOnly) return (-1);
            if (!this.columns.ContainsKey(colName)) return (-1);
            return (this.columns[colName]);
        }

        public bool ContainsColumn(string colName)
        {
            if (this.tableOnly) return (false);
            return (this.columns.ContainsKey(colName));
        }

        public IEnumerator GetEnumerator()
        {
            return new DataReturnSetEnum(this);
        }

    }

    /// <summary>
    /// Class for enumerating across the data set row by row
    /// </summary>
    public class DataReturnSetEnum : IEnumerator
    {
        private readonly DataReturnSet returnSetInstance;
        private int currentRowIdx;

        public DataReturnSetEnum(DataReturnSet returnSet)
        {
            this.returnSetInstance = returnSet;
            this.currentRowIdx = -1;
        }

        public bool MoveNext()
        {
            this.currentRowIdx++;
            return (this.currentRowIdx < this.returnSetInstance.NumberRows);
        }

        public void Reset()
        {
            this.currentRowIdx = -1;
        }

        public object Current
        {
            get
            {
                try
                {
                    return (this.returnSetInstance.Rows[this.currentRowIdx]);
                }
                catch (IndexOutOfRangeException iEx)
                {
                    throw new InvalidOperationException(iEx.ToString());
                }
            }
        }
    }

    /// <summary>
    /// Wrapper class for stored proc output
    /// </summary>
    public class DataSetOutput
    {
        /// <summary>
        /// Default name of OUTPUT table in stored proc result set
        /// for output parameters
        /// </summary>
        public static readonly string OUTPUTTABLENAME = "OUTPUT";
        /// <summary>
        /// Return set from stored proc output
        /// </summary>
        private readonly DataSet results;
        /// <summary>
        /// Returns true if the data set is not null, the data set is initialized,
        /// and the data set contains no errors
        /// </summary>
        public bool IsValid { get; private set; }
        /// <summary>
        /// Retrieves a table from the stored procedure output data set
        /// </summary>
        /// <param name="tableName">Name of the table to retrieve</param>
        /// <param name="dTable">Data table object</param>
        /// <returns>True if and only if the table exists in the DataSet</returns>
        public bool GetTable(string tableName, out DataTable dTable)
        {
            dTable = null;
            if (string.IsNullOrEmpty(tableName))
            {
                return (false);
            }

            if (this.IsValid && results.Tables != null && results.Tables.Contains(tableName))
            {
                dTable = this.results.Tables[tableName];
                return (true);
            }

            return (false);
        }

        /// <summary>
        /// Retrieves the OUTPUT table from the stored procedure output data set
        /// The OUTPUT table is utilized only when individual output 
        /// parameters exist in the stored proc signature (other than the error code
        /// and error description)
        /// </summary>
        /// <param name="dTable">Data table object</param>
        /// <returns>True if and only if the table exists in the DataSet</returns>
        public bool GetOutputTable(out DataTable dTable)
        {
            dTable = null;
            if (this.IsValid && results.Tables != null && results.Tables.Contains(OUTPUTTABLENAME))
            {
                dTable = this.results.Tables[OUTPUTTABLENAME];
                return (true);
            }
            return (false);
        }

        public DataSetOutput(DataSet dSet)
        {
            this.results = dSet;
            this.IsValid = this.results != null &&
                           this.results.IsInitialized &&
                           this.results.HasErrors == false;
        }
    }


    public class DataAccessTools : IDisposable
    {

        #region Enumerations
        public enum ConnectMode
        {
            SINGLE = 0,
            MULTIPLE = 1
        }

        public enum LogMode
        {
            OFF = 0,
            FATAL = 1,
            ERROR = 2,
            WARN = 3,
            INFO = 4,
            DEBUG = 5
        }
        #endregion

        #region Accessors

        public Credentials CurrentCredentials
        {
            get
            {
                return (this.currentCredentials);
            }
        }
        public TempFileLogger AuxLogger
        {
            set
            {
                this.logger = value;
            }
        }

        public OracleDataAccessor OracleDA
        {
            get
            {
                return (this.oracleDA);
            }
        }

        public ConnectMode ConnectionMode { get; private set; }
        //public LogMode LoggerMode { get; private set; }

        #endregion

        #region Private/Internal Fields

        internal OracleDataAccessor oracleDA;
        private Credentials currentCredentials;
        private ConnectMode connectionMode;
        private TempFileLogger logger;
        //private LogMode loggerMode;
        #endregion

        #region Private Methods
        #region Flexible log methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msgFmt"></param>
        /// <param name="vars"></param>
        public void log(LogLevel level, string msgFmt, params object[] vars)
        {
            FileLogger fileLogger = FileLogger.Instance;
            if (fileLogger != null && fileLogger.isLogLevel(level))
            {
                fileLogger.logMessage(level, this, msgFmt, vars);
            }
            if (this.logger != null && this.logger.isLogLevel(level))
            {
                this.logger.logMessage(level, this, msgFmt, vars);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msg"></param>
        public void log(LogLevel level, string msg)
        {
            FileLogger fileLogger = FileLogger.Instance;
            if (fileLogger != null && fileLogger.isLogLevel(level))
            {
                fileLogger.logMessage(level, this, msg);
            }
            if (this.logger != null && this.logger.isLogLevel(level))
            {
                this.logger.logMessage(level, this, msg);
            }
        }

        public bool isloglevel(LogLevel level)
        {
            var fileLogger = FileLogger.Instance;
            bool allowedFileLogger = true, allowedAuxLogger = true;
            if (fileLogger != null)
            {
                allowedFileLogger = fileLogger.isLogLevel(level);
            }
            if (this.logger != null)
            {
                allowedAuxLogger = logger.isLogLevel(level);
            }

            return (allowedAuxLogger && allowedFileLogger);
        }

        #endregion

        #endregion

        public DataAccessTools()
        {
            this.oracleDA = null;
            this.currentCredentials = new Credentials();
            this.connectionMode = ConnectMode.MULTIPLE;
            //this.loggerMode = LogMode.OFF;
        }

        protected internal bool Initialize(Credentials cred,
            ConnectMode cMode, string key)
        {
            //Set properties
            this.currentCredentials = cred;
            this.connectionMode = cMode;
            //this.loggerMode = logMode;

            var pT = new PerfTimer(this, "Initialize");

            if (cMode == ConnectMode.SINGLE)
            {

                if (GlobalDataAccessor.Instance == null ||
                    GlobalDataAccessor.Instance.OracleDA == null ||
                    GlobalDataAccessor.Instance.OracleDA.Initialized == false)
                {
                    //Initialize data accessor
                    this.oracleDA = new OracleDataAccessor(
                        this.currentCredentials.UserName,
                        this.currentCredentials.PassWord,
                        this.currentCredentials.DBHost,
                        this.currentCredentials.DBPort,
                        this.currentCredentials.DBService,
                        this.currentCredentials.DBSchema,
                        65535, cMode == ConnectMode.MULTIPLE,
                        !string.IsNullOrEmpty(key), key);

                    GlobalDataAccessor.Instance.OracleDA = this.oracleDA;
                }
                else
                {
                    this.oracleDA = GlobalDataAccessor.Instance.OracleDA;
                }
            }
            else if (cMode == ConnectMode.MULTIPLE)
            {
                if (this.oracleDA == null)
                {
                    this.oracleDA = new OracleDataAccessor(
                        this.currentCredentials.UserName,
                        this.currentCredentials.PassWord,
                        this.currentCredentials.DBHost,
                        this.currentCredentials.DBPort,
                        this.currentCredentials.DBService,
                        this.currentCredentials.DBSchema,
                        65535, cMode == ConnectMode.MULTIPLE,
                        !string.IsNullOrEmpty(key), key);

                    if (GlobalDataAccessor.Instance == null ||
                        GlobalDataAccessor.Instance.OracleDA == null ||
                        GlobalDataAccessor.Instance.OracleDA.Initialized == false)
                    {
                        GlobalDataAccessor.Instance.OracleDA = this.oracleDA;
                    }
                }
            }
            if (this.isloglevel(LogLevel.DEBUG))
                    this.log(LogLevel.DEBUG, pT.ToString());
            

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTableOnly"></param>
        /// <param name="sqlQuery"></param>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <param name="data"></param>
        /// <param name="dOut"></param>
        /// <returns></returns>
        protected internal bool ExecuteQuery(
            bool dataTableOnly,
            string sqlQuery,
            string tableName,
            string key,
            out Dictionary<string, int> columns,
            out List<List<object>> data,
            out DataTable dOut)
        {
            data = null;
            columns = null;
            dOut = null;
            if (this.oracleDA == null ||
                this.oracleDA.Initialized == false)
            {
                return (false);
            }

            var pT = new PerfTimer(this, "ExecuteQuery");
            var pTQueryOnly = new PerfTimer("- (bound)Query only");
            //Execute query
            DataTable dataTable;
            bool callSuccess;
            try
            {
                this.log(
                    LogLevel.INFO, 
                    "Executing query: {0}", sqlQuery);
                callSuccess =
                    this.oracleDA.issueSqlTextSelectCommand(
                    sqlQuery,
                    tableName,
                    CommandBehavior.SingleResult,
                    key,
                    out dataTable);
            }
            catch (Exception eX)
            {
                //Do something here
                this.log(LogLevel.ERROR, "Could not execute sql statement: " + sqlQuery + ": Exception: " + eX);
                return (false);
            }

            if (callSuccess == false)
            {
                return (false);
            }
            if (this.isloglevel(LogLevel.DEBUG))
                this.log(LogLevel.DEBUG, pTQueryOnly.ToString());


            //Extract the data from the data table
            if (dataTable != null && dataTable.IsInitialized &&
                dataTable.Columns != null && dataTable.Columns.Count > 0 &&
                dataTable.Rows != null && dataTable.Rows.Count > 0)
            {
                if (!dataTableOnly)
                {
                    PerfTimer pTCust = new PerfTimer(this, "- (bound)Custom data set creation");
                    //Get the columns
                    columns = new Dictionary<string, int>(dataTable.Columns.Count);
                    for (var c = 0; c < dataTable.Columns.Count; ++c)
                    {
                        columns.Add(dataTable.Columns[c].ColumnName, c);
                    }

                    //Get the data from each row
                    data = new List<List<object>>(dataTable.Rows.Count);
                    foreach (DataRow curDr in dataTable.Rows)
                    {
                        var objRow = new List<object>(dataTable.Columns.Count);
                        for (var c = 0; c < dataTable.Columns.Count; ++c)
                        {
                            objRow.Add(curDr[c]);
                        }
                        data.Add(objRow);
                    }
                    if (this.isloglevel(LogLevel.DEBUG))
                        this.log(LogLevel.DEBUG, pTCust.ToString());
                }
                else
                {
                    PerfTimer pTCust = new PerfTimer(this, "- (bound)Just data table");
                    dOut = dataTable;
                    if (this.isloglevel(LogLevel.DEBUG))
                        this.log(LogLevel.DEBUG, pTCust.ToString());
                }
            }
            if (this.isloglevel(LogLevel.DEBUG))
                this.log(LogLevel.DEBUG, pT.ToString());
            return (true);
        }


        protected internal bool ExecuteInsertUpdateDeleteQuery(
            string sqlQuery,
            string key)
        {
            if (this.oracleDA == null ||
                this.oracleDA.Initialized == false)
            {
                return (false);
            }
            var pT = new PerfTimer(this, "ExecuteInsertUpdateDeleteQuery");
            bool callSuccess;
            try
            {
                this.log(
                    LogLevel.INFO,
                    "Executing query: {0}", sqlQuery);
                callSuccess =
                    this.oracleDA.issueSqlTextInsertUpdateDeleteCommand(
                    sqlQuery,
                    key);
            }
            catch (Exception eX)
            {
                //Do something here
                this.log(LogLevel.ERROR, "Could not execute sql statement: " + sqlQuery + ": Exception: " + eX);
                return (false);
            }

            if (callSuccess == false)
            {
                return (false);
            }
            if (this.isloglevel(LogLevel.DEBUG))
            {
                this.log(LogLevel.DEBUG, pT.ToString());
            }

            return (true);
        }

        protected internal bool startTransactionBlock(string key)
        {
            if (this.oracleDA == null ||
                this.oracleDA.Initialized == false)
            {
                return (false);
            }

            bool res;
            try
            {
                res = this.oracleDA.StartTransactionBlock(IsolationLevel.ReadCommitted, key);
            }
            catch (Exception eX)
            {
                this.log(LogLevel.ERROR, "Could not start the transaction block: {0}", eX);
                return (false);
            }

            return (res);
        }

        protected internal bool commitTransactionBlock(string key)
        {
            if (this.oracleDA == null ||
                this.oracleDA.Initialized == false)
            {
                return (false);
            }

            bool res;
            try
            {
                res = this.oracleDA.commitTransactionBlock(key);
            }
            catch (Exception eX)
            {
                this.log(LogLevel.ERROR, "Could not commit the transaction block: {0}", eX);
                return (false);
            }

            return (res);            
        }

        protected internal bool rollbackTransactionBlock(string key)
        {
            if (this.oracleDA == null ||
                this.oracleDA.Initialized == false)
            {
                return (false);
            }

            bool res;
            try
            {
                res = !inTransactionBlock(key) || this.oracleDA.rollbackTransactionBlock(key);
            }
            catch (Exception eX)
            {
                this.log(LogLevel.ERROR, "Could not rollback the transaction block: {0}", eX);
                return (false);
            }

            return (res);            
        }

        /// <summary>
        /// Check to see if there are open transaction blocks
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected internal bool inTransactionBlock(string key)
        {
            if (this.oracleDA == null ||
                this.oracleDA.Initialized == false)
            {
                return (false);
            }

            bool res;
            try
            {
                res = this.oracleDA.inTransactionBlock(key);
            }
            catch (Exception eX)
            {
                this.log(LogLevel.ERROR, "Could not find whether we have an open transaction block: {0}", eX);
                return (false);
            }

            return (res);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="procSchema"></param>
        /// <param name="procPackage"></param>
        /// <param name="procName"></param>
        /// <param name="inputParameters"></param>
        /// <param name="outputParameters"></param>
        /// <param name="refCursorParamNames"></param>
        /// <param name="errorNumberParamName"></param>
        /// <param name="errorDescParamName"></param>
        /// <param name="errorNumber"></param>
        /// <param name="errorDesc"></param>
        /// <param name="dOut"></param>
        /// <returns></returns>
        protected internal bool ExecuteStoredProc(
            string procSchema,
            string procPackage,
            string procName,
            List<OracleProcParam> inputParameters,
            List<TupleType<string, DataTypeConstants.PawnDataType, int>> outputParameters,
            List<PairType<string, string>> refCursorParamNames,
            string errorNumberParamName,
            string errorDescParamName,
            string key,
            out string errorNumber,
            out string errorDesc,            
            out DataSet dOut)
        {
            errorNumber = null;
            errorDesc = null;
            dOut = null;
            if (this.oracleDA == null ||
                this.oracleDA.Initialized == false)
            {
                return (false);
            }
            var storedProcId = procSchema + "." + procPackage + "." + procName;

            //Formulate output OracleProcParam objects if necessary
            if (outputParameters != null && outputParameters.Count > 0)
            {
                foreach (var oP in outputParameters)
                {
                    inputParameters.Add(
                        new OracleProcParam(
                            oP.Left, (OracleDbType)oP.Mid, DBNull.Value,
                            ParameterDirection.Output, oP.Right));
                }
            }

            var pT = new PerfTimer(this, "ExecuteStoredProc");
            var pTQueryOnly = new PerfTimer("- (bound)Stored Proc only");

            //Execute stored proc
            bool callSuccess;
            try
            {
                this.log(
                    LogLevel.INFO, 
                    "Executing stored proc: {0}", storedProcId);
                if (string.IsNullOrEmpty(key))
                {
                    callSuccess =
                            this.oracleDA.issueSqlStoredProcCommand(
                                    procSchema,
                                    procPackage,
                                    procName,
                                    inputParameters,
                                    refCursorParamNames,
                                    errorNumberParamName,
                                    errorDescParamName,
                                    out dOut
                                    );
                }
                else
                {
                    callSuccess =
                            this.oracleDA.issueSqlStoredProcCommand(
                                    procSchema,
                                    procPackage,
                                    procName,
                                    inputParameters,
                                    refCursorParamNames,                                    
                                    errorNumberParamName,
                                    errorDescParamName,
                                    key,
                                    out dOut
                                    );
                }
            }
            catch (Exception eX)
            {
                this.log(LogLevel.ERROR, 
                    "Could not execute stored proc: " + storedProcId + ": Exception: " + eX);
                errorNumber = this.oracleDA.ErrorCode ?? "Failure";
                errorDesc = this.oracleDA.ErrorDescription ?? "Could not execute stored proc " + storedProcId;
                return (false);
            }

            if (callSuccess == false)
            {
                errorNumber = this.oracleDA.ErrorCode ?? "0";
                errorDesc = this.oracleDA.ErrorDescription ?? "Could not execute stored proc " + storedProcId;
                return (false);
            }
            if (this.isloglevel(LogLevel.DEBUG))
            {
                this.log(LogLevel.DEBUG, pTQueryOnly.ToString());
            }

            //Set default success output
            errorNumber = "1";
            errorDesc = "Success";

            if (this.isloglevel(LogLevel.DEBUG))
                this.log(LogLevel.DEBUG, pT.ToString());
            return (true);
        }

        public enum OracleDateTimeFormat
        {
            ORACLE_DATE,
            ORACLE_TIMESTAMP,
            ORACLE_TIMESTAMP_TZ,
            ORACLE_TIMESTAMP_LTZ,
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="date"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        protected internal bool ConvertToDateTimeFromOracleInternal(OracleDateTimeFormat format, object date, out DateTime dateTime)
        {
            dateTime = DateTime.MinValue;
            if (date == null) return (false);

            try
            {
                switch (format)
                {
                    case OracleDateTimeFormat.ORACLE_DATE:
                        var oracleDate = new OracleDate((string)date);
                        if (oracleDate != OracleDate.Null)
                            dateTime = oracleDate.Value;
                        break;
                    case OracleDateTimeFormat.ORACLE_TIMESTAMP:
                        var oracleTSDate = new OracleTimeStamp((string)date);
                        if (oracleTSDate != OracleTimeStamp.Null)
                            dateTime = oracleTSDate.Value;
                        break;
                    case OracleDateTimeFormat.ORACLE_TIMESTAMP_TZ:
                        var oracleTSZDate = new OracleTimeStampTZ((string)date);
                        if (oracleTSZDate != OracleTimeStampTZ.Null)
                            dateTime = oracleTSZDate.Value;
                        break;
                    case OracleDateTimeFormat.ORACLE_TIMESTAMP_LTZ:
                        var oracleLTSZDate = new OracleTimeStampLTZ((string)date);
                        if (oracleLTSZDate != OracleTimeStampLTZ.Null)
                            dateTime = oracleLTSZDate.Value;
                        break;
                    default:
                        throw new ApplicationException("Invalid format specified");
                }
            }
            catch (Exception)
            {
                return (false);
            }
            return (true);
        }

        /// <summary>
        /// Disconnect the database
        /// </summary>
        protected internal void Disconnect()
        {
            if (this.oracleDA != null)
            {
                this.oracleDA.DisconnectDbConnection(null);
                this.oracleDA = null;
            }
        }

        protected internal void Reconnect()
        {
            if (this.oracleDA != null)
            {
                this.oracleDA.ReconnectDbConnection(null);
            }
        }

        /// <summary>
        /// Implement IDisposable interface
        /// </summary>
        public void Dispose()
        {
            if (this.oracleDA != null)
            {
                this.oracleDA.DisconnectDbConnection(null);
                this.oracleDA = null;
            }
        }
    }

    public static class DataAccessService
    {
        public const string PARAM_SEP = "?";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataAccessTools CreateDataAccessTools()
        {
            //Allocate tools object
            var rt = new DataAccessTools();

            //Return initialized object
            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="connectMode"></param>
        /// <param name="logMode"></param>
        /// <param name="tools"></param>
        /// <returns></returns>
        public static bool Connect(
            Credentials credentials,
            DataAccessTools.ConnectMode connectMode,
            DataAccessTools.LogMode logMode,
            ref DataAccessTools tools)
        {
            if (tools == null) return (false);
            bool rt;
            try
            {
                //Initialize tools / connect
                //tools.AuxLogger = auxLogger;
                rt = tools.Initialize(credentials,
                    connectMode, null);
            }
            catch (Exception eX)
            {
                throw new ApplicationException(
                    "Could not create connection to " + 
                    credentials.DBSchema + " @ " + 
                    credentials.DBHost + ":" + 
                    credentials.DBPort + ". Exception thrown: " + 
                    eX);
            }

            return (rt);
        }

        public static bool Connect(
            string key,
            Credentials credentials,
            DataAccessTools.ConnectMode connectMode,
            DataAccessTools.LogMode logMode,
            ref DataAccessTools tools)
        {
            if (tools == null) return (false);
            bool rt;
            try
            {
                //Initialize tools / connect
                //tools.AuxLogger = auxLogger;
                rt = tools.Initialize(credentials,
                    connectMode, key);
            }
            catch (Exception eX)
            {
                throw new ApplicationException(
                    "Could not create connection to " +
                    credentials.DBSchema + " @ " +
                    credentials.DBHost + ":" +
                    credentials.DBPort + ". Exception thrown: " +
                    eX);
            }

            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTableOnly"></param>
        /// <param name="query"></param>
        /// <param name="tableName"></param>
        /// <param name="key"></param>
        /// <param name="returnSet"></param>
        /// <param name="tools"></param>
        /// <returns></returns>
        public static bool ExecuteQuery(
            bool dataTableOnly,
            string query,
            string tableName,
            string key,
            out DataReturnSet returnSet,
            ref DataAccessTools tools)
        {
            returnSet = null;
            if (tools == null) return (false);
            if (string.IsNullOrEmpty(query)) return (false);

            Dictionary<string, int> columns;
            List<List<object>> data;
            DataTable dTable;
            if (!tools.ExecuteQuery(dataTableOnly, query, tableName, key,
                out columns, out data, out dTable))
            {
                return (false);
            }

            if (data != null)
            {
                //Create return set
                if (!dataTableOnly)
                {
                    returnSet = new DataReturnSet(columns, data);
                }
                else
                {
                    returnSet = new DataReturnSet(dTable);
                }
            }

            //Return true
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTableOnly"></param>
        /// <param name="query"></param>
        /// <param name="tableName"></param>
        /// <param name="key"></param>
        /// <param name="returnSet"></param>
        /// <param name="tools"></param>
        /// <param name="vars">
        /// The zero index and even indices are param names
        /// and the Odd indices are param values
        /// </param>
        /// <returns></returns>
        public static bool ExecuteVariableQuery(
            bool dataTableOnly,
            string query,
            string tableName,
            string key,
            out DataReturnSet returnSet,
            ref DataAccessTools tools,
            params PairType<string,string>[] vars)
        {
            returnSet = null;
            if (tools == null) return (false);
            tools.log(LogLevel.DEBUG, "DataAccessTools::ExecuteVariableQuery");
            if (string.IsNullOrEmpty(query))
            {
                tools.log(LogLevel.ERROR, "- Query is invalid");
                return (false);
            }
            tools.log(LogLevel.DEBUG, "- Executing query: {0}", query);

            Dictionary<string, int> columns;
            List<List<object>> data;
            DataTable dTable;

            //Prepare variable query
            string finalQuery = PrepareVariableQuery(ref tools, query, vars);

            if (string.IsNullOrEmpty(finalQuery))
            {
                tools.log(LogLevel.ERROR, "- Variable query could not be generated");
                return (false);
            }

            //Log transformed query
            tools.log(LogLevel.INFO, "Transformed query: {0}", finalQuery);

            //Execute variable query
            if (!tools.ExecuteQuery(dataTableOnly, finalQuery, tableName, key,
                out columns, out data, out dTable))
            {
                return (false);
            }

            if (data != null)
            {
                //Create return set
                if (!dataTableOnly)
                {
                    returnSet = new DataReturnSet(columns, data);
                }
                else
                {
                    returnSet = new DataReturnSet(dTable);
                }
            }

            //Return true
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="tools"></param>
        /// <param name="vars">
        /// The zero index and even indices are param names
        /// and the Odd indices are param values
        /// </param>
        /// <returns></returns>
        public static string PrepareVariableQuery(
            ref DataAccessTools tools,
            string query,
            params PairType<string, string>[] vars)
        {
            tools.log(LogLevel.DEBUG, "DataAccessTools::PrepareVariableQuery");
            if (string.IsNullOrEmpty(query))
            {
                tools.log(LogLevel.ERROR, "- Query is invalid");
                return (string.Empty);
            }
            //Must be valid and not empty);
            if (vars == null || vars.Length <= 0)
            {
                tools.log(LogLevel.ERROR, "- No variables provided for query");
                return (string.Empty);
            }
            tools.log(LogLevel.DEBUG, "- {0} variables provided", vars.Length);

            //Perform string replacement operations
            foreach (var p in vars)
            {
                if (p == null || string.IsNullOrEmpty(p.Left))
                {
                    tools.log(LogLevel.ERROR, "-- Found null variable name");
                    return (string.Empty);
                }
                var paramName = PARAM_SEP + p.Left + PARAM_SEP;
                //Null is valid in SQL, so we must allow for native null in pair right values
                var paramValue = p.Right ?? "null";
                tools.log(LogLevel.DEBUG, "-- {0} => {1} specified", paramName, paramValue);
                try
                {
                    query = query.Replace(paramName, paramValue);
                }
                catch (Exception eX)
                {
                    tools.log(LogLevel.ERROR, "-- Exception thrown when performing replacement: {0}", eX);
                    return (string.Empty);
                }
            }

            return (query);
        }

        /// <summary>
        /// Executes any form of a query other than a select (insert,update,delete,merge)
        /// </summary>
        /// <param name="query"></param>
        /// <param name="key"></param>
        /// <param name="tools"></param>
        /// <returns></returns>
        public static bool ExecuteInsertUpdateDeleteQuery(
            string query,
            string key,
            ref DataAccessTools tools)
        {
            if (string.IsNullOrEmpty(query) || string.IsNullOrEmpty(key) || tools == null)
            {
                return (false);
            }
            tools.log(LogLevel.INFO, "Executing query {0}", query);

            if (!tools.ExecuteInsertUpdateDeleteQuery(query, key))
            {
                tools.log(LogLevel.WARN, "Could not execute query {0}", query);
                return (false);
            }

            return (true);
        }

        /// <summary>
        /// Starts a transaction block
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="tools"></param>
        /// <returns></returns>
        public static bool StartTransactionBlock(string key, string name, ref DataAccessTools tools)
        {
            if (string.IsNullOrEmpty(key) || tools == null)
            {
                return (false);
            }
            name = name ?? "NoName";
            tools.log(LogLevel.INFO, "Starting {0} transaction block with key {1}", name, key);
            if (!tools.startTransactionBlock(key))
            {
                tools.log(LogLevel.WARN, "Could not start {0} transaction block with key {1}", name, key);
                return (false);
            }

            return (true);
        }

        /// <summary>
        /// Commits a transaction block
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="tools"></param>
        /// <returns></returns>
        public static bool CommitTransactionBlock(string key, string name, ref DataAccessTools tools)
        {
            if (string.IsNullOrEmpty(key) || tools == null)           
            {
                return (false);
            }
            name = name ?? "NoName";
            tools.log(LogLevel.INFO, "Committing {0} transaction block with key {1}", name, key);
            if (!tools.commitTransactionBlock(key))
            {
                tools.log(LogLevel.ERROR, "Could not commit {0} transaction block with key {1}", name, key);
                return (false);
            }
            return (true);
        }

        /// <summary>
        /// Rolls back any changes made in a non-committed transaction block
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="tools"></param>
        /// <returns></returns>
        public static bool RollbackTransactionBlock(string key, string name, ref DataAccessTools tools)
        {
            if (string.IsNullOrEmpty(key) || tools == null)
            {
                return (false);
            }
            name = name ?? "NoName";
            tools.log(LogLevel.INFO, "Rolling back {0} transaction block with key {1}", name, key);
            if (!tools.rollbackTransactionBlock(key))
            {
                tools.log(LogLevel.ERROR, "Could not rollback {0} transaction block with key {1}", name, key);
                return (false);
            }
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procSchema"></param>
        /// <param name="procPackage"></param>
        /// <param name="procStoredProc"></param>
        /// <param name="inputParameters"></param>
        /// <param name="outputParameters"></param>
        /// <param name="refCursorParamNames"></param>
        /// <param name="errorNumberParamName"></param>
        /// <param name="errorDescParamName"></param>
        /// <param name="key"></param>
        /// <param name="errorNumber"></param>
        /// <param name="errorDesc"></param>
        /// <param name="datasetOutput"></param>
        /// <param name="tools"></param>
        /// <returns></returns>
        public static bool ExecuteStoredProc(
            string procSchema,
            string procPackage,
            string procStoredProc,
            List<OracleProcParam> inputParameters,
            List<TupleType<string, DataTypeConstants.PawnDataType, int>> outputParameters,
            List<PairType<string, string>> refCursorParamNames,
            string errorNumberParamName,
            string errorDescParamName,
            string key,
            out string errorNumber,
            out string errorDesc,
            out DataSetOutput datasetOutput,
            ref DataAccessTools tools)
        {
            datasetOutput = null;
            errorNumber = string.Empty;
            errorDesc = string.Empty;
            if (tools == null) return (false);
            if (string.IsNullOrEmpty(procSchema) || 
                string.IsNullOrEmpty(procPackage) || 
                string.IsNullOrEmpty(procStoredProc))
            {
                return (false);
            }

            DataSet dSet;            
            if (!tools.ExecuteStoredProc(procSchema, procPackage, procStoredProc,
                inputParameters, outputParameters, refCursorParamNames, 
                errorNumberParamName,
                errorDescParamName,
                key,
                out errorNumber, 
                out errorDesc, 
                out dSet))
            {
                return (false);
            }

            //Allocate and initialize data return set
            datasetOutput = new DataSetOutput(dSet);

            //Return true
            return (true);
        }

        /// <summary>
        /// Disconnects the application from the database. 
        /// </summary>
        /// <param name="tools"></param>
        public static void Disconnect(ref DataAccessTools tools)
        {
            if (tools == null) return;
            tools.Disconnect();
        }

        /// <summary>
        /// Reconnects the application to the database
        /// </summary>
        /// <param name="tools"></param>
        public static void Reconnect(ref DataAccessTools tools)
        {
            if (tools == null) return;
            tools.Reconnect();
        }
      
        

        /// <summary>
        /// Retrieves a .NET standard DateTime from a various assortment of oracle date time types
        /// </summary>
        /// <param name="format"></param>
        /// <param name="date">Input date object</param>
        /// <param name="tools"></param>
        /// <returns>Returns valid DateTime if successful, otherwise DateTime.MinValue</returns>
        public static DateTime GetDateTimeFromOracleObject(
            DataAccessTools.OracleDateTimeFormat format, 
            object date, 
            ref DataAccessTools tools)
        {
            if (date == null || tools == null) return (DateTime.MinValue);
            DateTime dateTime;
            if (!tools.ConvertToDateTimeFromOracleInternal(format, date, out dateTime))
            {
                return (DateTime.MinValue);
            }
            return (dateTime);
        }
    }
}
