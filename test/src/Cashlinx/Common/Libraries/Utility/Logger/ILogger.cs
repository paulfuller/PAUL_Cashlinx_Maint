using System;
using System.Data;
using System.Globalization;
using System.Text;

namespace Common.Libraries.Utility.Logger
{
    public enum LogLevel
    {
        DEBUG = 0,
        INFO  = 1,
        WARN  = 2,
        ERROR = 3,
        FATAL = 4
    }

    public delegate string DateStampGenerator();
    public delegate bool IsLogLevelChecker(LogLevel chkLevel, LogLevel curLevel);
    public delegate string LogLevelStringGenerator(LogLevel level);
    public delegate bool LogMessageHandler(
        IsLogLevelChecker levelChecker,
        DateStampGenerator dateGen,
        LogLevelStringGenerator levelGen,
        LogLevel chkLevel,
        LogLevel curLevel, 
        object caller, 
        string msg, 
        out string data);

    public delegate bool LogMessageFormatHandler(
        IsLogLevelChecker levelChecker,
        DateStampGenerator dateGen,
        LogLevelStringGenerator levelGen,
        LogLevel chkLevel,
        LogLevel curLevel, 
        object caller, 
        out string data, 
        string fmtMsg, 
        params object[] objs);

    public delegate bool DumpDataTableHandler(
        LogMessageHandler logHandler,
        IsLogLevelChecker levelChecker,
        DateStampGenerator dateGen,
        LogLevelStringGenerator levelGen,
        LogLevel chkLevel,
        LogLevel curLevel,
        object caller,
        string msg,
        DataTable dataTable,
        out string data);

    public static class LoggerConstants
    {
        public const string SPC = " ";
        public const string CLASSUNK = "StaticClass";
        public const string NULLSTR  = "null";
        public const string DEBUGSTR = "DEBUG";
        public const string INFOSTR  = "INFO";
        public const string WARNSTR  = "WARN";
        public const string ERRORSTR = "ERROR";
        public const string FATALSTR = "FATAL";
        public static readonly DateTime epochTime = new DateTime(1970, 1, 1);
        public const string NOFRACMILLIS = "F0";
        public static string NEWLINE = System.Environment.NewLine;

        
        public static bool LogLevelChecker(LogLevel chkLevel, LogLevel curLevel)
        {
            return (curLevel <= chkLevel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GenLogLevelToString(LogLevel lev)
        {
            switch (lev)
            {
                case LogLevel.DEBUG:
                    return (DEBUGSTR);
                case LogLevel.INFO:
                    return (INFOSTR);
                case LogLevel.WARN:
                    return (WARNSTR);
                case LogLevel.ERROR:
                    return (ERRORSTR);
                case LogLevel.FATAL:
                    return (FATALSTR);
                default:
                    return (DEBUGSTR);
            }
        }

        public static string GenDateStamp()
        {
            var sb = new StringBuilder();
            var dNow = DateTime.Now;
            var dNowStr = dNow.ToShortDateString();
            var totalMillis = (dNow - epochTime).TotalMilliseconds;
            //Specify log time in milliseconds with no fractional component
            sb.Append(
                totalMillis.ToString(
                LoggerConstants.NOFRACMILLIS, 
                CultureInfo.InvariantCulture));
            sb.Append(LoggerConstants.SPC);
            sb.Append(dNowStr);
            sb.Append(LoggerConstants.SPC);
            sb.Append(dNow.ToShortTimeString());
            sb.Append(LoggerConstants.SPC);
            return (sb.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levelChecker"></param>
        /// <param name="levStrGen"></param>
        /// <param name="curLevel"></param>
        /// <param name="chkLevel"></param>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <param name="dateGen"></param>
        /// <returns></returns>
        public static bool LogMessage(
            IsLogLevelChecker levelChecker,
            DateStampGenerator dateGen,
            LogLevelStringGenerator levStrGen,
            LogLevel curLevel,
            LogLevel chkLevel, 
            object sender, 
            string msg, 
            out string data)
        {
            data = LoggerConstants.NULLSTR;
            if (levelChecker == null)
            {
                return(false);
            }

            if (!levelChecker(chkLevel, curLevel))
            {
                return(false);
            }

            var sb = new StringBuilder();
            sb.Append((dateGen == null) ? (LoggerConstants.GenDateStamp()) : dateGen());
            sb.Append(LoggerConstants.SPC);
            sb.Append((levStrGen == null) ? LoggerConstants.GenLogLevelToString(chkLevel) : levStrGen(chkLevel));
            sb.Append(LoggerConstants.SPC);
            sb.Append((sender == null) ?
                      LoggerConstants.CLASSUNK :
                      sender.GetType().FullName);
            sb.Append(LoggerConstants.SPC);
            sb.Append((string.IsNullOrEmpty(msg)) ?
                      LoggerConstants.NULLSTR :
                      msg);
            data = sb.ToString();
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levelChecker"></param>
        /// <param name="levStrGen"></param>
        /// <param name="curLevel"></param>
        /// <param name="chkLevel"></param>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /// <param name="msgFmt"></param>
        /// <param name="vars"></param>
        /// <param name="dateGen"></param>
        /// <returns></returns>
        public static bool LogMessageFmt(
            IsLogLevelChecker levelChecker,
            DateStampGenerator dateGen,
            LogLevelStringGenerator levStrGen,
            LogLevel curLevel,
            LogLevel chkLevel, 
            object sender, 
            out string data, 
            string msgFmt, 
            params object[] vars)
        {
            data = LoggerConstants.NULLSTR;
            if (string.IsNullOrEmpty(msgFmt) || 
                vars == null || 
                vars.Length <= 0 || 
                levelChecker == null)
            {
                return(false);
            }
            if (!levelChecker(chkLevel, curLevel))
            {
                return(false);
            }
            var sFmt = string.Format(msgFmt, vars);
            var sb = new StringBuilder();
            sb.Append((dateGen == null) ? 
                (LoggerConstants.GenDateStamp()) : 
                dateGen());
            sb.Append(LoggerConstants.SPC);
            sb.Append((levStrGen == null) ? 
                LoggerConstants.GenLogLevelToString(chkLevel) : 
                levStrGen(chkLevel));
            sb.Append(LoggerConstants.SPC);
            sb.Append((sender == null) ? 
                      LoggerConstants.CLASSUNK : 
                      sender.GetType().FullName);
            sb.Append(LoggerConstants.SPC);
            sb.Append((string.IsNullOrEmpty(sFmt)) ? 
                      LoggerConstants.NULLSTR : 
                      sFmt);
            data = sb.ToString();
            return (true);
        }

        /// <summary>
        /// Dumps a data table to a log string
        /// </summary>
        /// <param name="logHandler"></param>
        /// <param name="levelChecker"></param>
        /// <param name="dateGen"></param>
        /// <param name="levelGen"></param>
        /// <param name="chkLevel"></param>
        /// <param name="curLevel"></param>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <param name="dataTable"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool DumpDataTableHandler(
            LogMessageHandler logHandler,
            IsLogLevelChecker levelChecker,
            DateStampGenerator dateGen,
            LogLevelStringGenerator levelGen,
            LogLevel chkLevel,
            LogLevel curLevel,
            object sender,
            string msg,
            DataTable dataTable,
            out string data)
        {
            data = LoggerConstants.NULLSTR;
            if (levelChecker == null)
            {
                return (false);
            }

            if (!levelChecker(chkLevel, curLevel))
            {
                return (false);
            }

            if (dataTable == null || dataTable.Rows == null || dataTable.Rows.Count <= 0)
            {
                data = "Data table is invalid";
                return (false);
            }


            var sb = new StringBuilder();
            sb.Append((dateGen == null) ? 
                (LoggerConstants.GenDateStamp()) : dateGen());
            sb.Append(LoggerConstants.SPC);
            sb.Append((levelGen == null) ? 
                LoggerConstants.GenLogLevelToString(chkLevel) : 
                levelGen(chkLevel));
            sb.Append(LoggerConstants.SPC);
            sb.Append((sender == null) ?
                      LoggerConstants.CLASSUNK :
                      sender.GetType().FullName);
            sb.Append(LoggerConstants.SPC);
            sb.Append((string.IsNullOrEmpty(msg)) ?
                      LoggerConstants.NULLSTR :
                      msg);
            int idx = 0;
            sb.Append(NEWLINE);
            sb.Append("Data Table ");
            sb.Append(dataTable.TableName);
            sb.AppendFormat(": {0}", NEWLINE);
            var sbCol = new StringBuilder();
            foreach(DataColumn dC in dataTable.Columns)
            {
                if (dC == null || string.IsNullOrEmpty(dC.ColumnName))
                {
                    sbCol.AppendFormat("[{0}]", NULLSTR);
                    continue;
                }
                sbCol.AppendFormat("[{0}]", dC.ColumnName);
            }
            sb.AppendLine("");

            foreach(DataRow dR in dataTable.Rows)
            {
                if (dR == null)
                {
                    sb.AppendFormat("[{0}] is null {1}", idx, NEWLINE);
                }
                else
                {
                    if (dR.ItemArray.Length <= 0)
                    {
                        sb.AppendFormat("[{0}] is empty {1}", idx, NEWLINE);
                    }
                    else
                    {
                        sb.AppendFormat("[{0}]", idx);
                        var colSb = new StringBuilder(32);                        
                        foreach(var curObj in dR.ItemArray)
                        {
                            if (curObj == null)
                            {
                                colSb.Append("|null");                        
                            }
                            else
                            {
                                colSb.AppendFormat("|{0}", curObj);
                            }
                        }
                        sb.AppendLine(colSb.ToString());
                    }
                }
            }
            data = sb.ToString();
            return (true);
        }

    }

    /// <summary>
    /// Default log handlers
    /// </summary>
    public static class DefaultLoggerHandlers
    {
        public static LogLevelStringGenerator defaultLogLevelGenerator = 
            LoggerConstants.GenLogLevelToString;
        public static DateStampGenerator defaultDateStampGenerator = 
            LoggerConstants.GenDateStamp;
        public static LogMessageHandler defaultLogMessageHandler =
            LoggerConstants.LogMessage;
        public static LogMessageFormatHandler defaultLogMessageFormatHandler =
            LoggerConstants.LogMessageFmt;
        public static IsLogLevelChecker defaultLogLevelCheckHandler =
            LoggerConstants.LogLevelChecker;
        public static DumpDataTableHandler defaultLogTableHandler =
            LoggerConstants.DumpDataTableHandler;
    }


    public interface ILogger
    {
        bool IsLogError { get; }
        bool IsLogDebug { get; }
        bool IsLogInfo { get; }
        bool IsLogFatal { get; }
        bool IsLogWarn { get; }

        void setEnabled(bool enabled);
        void addOutputStream(System.IO.StreamWriter writer);
        void setOutputStream(System.IO.StreamWriter writer);
        LogLevel getLogLevel();
        bool isLogLevel(LogLevel level);
        void setLogLevel(LogLevel level);
        void logMessage(LogLevel level, object sender, string msg);
        void flush();
    }
}
