using System;
using System.Collections.Generic;
using System.IO;
using Common.Libraries.Utility.Collection;

namespace Common.Libraries.Utility.Logger
{
    public sealed class FileLogger : MarshalByRefObject, ILogger, IDisposable
    {
        static readonly object mutexObj = new object();
        public static readonly DateTime NOWDATE = DateTime.Now;
        public static readonly string FILENAME = "ph2_filelog_" + NOWDATE.Year + "_" + NOWDATE.Month + "_" + NOWDATE.Day + "_" + NOWDATE.Hour + "_" + NOWDATE.Minute + ".log";
        public static readonly string DEBUGSTR = "DEBUG";
        public static readonly string INFOSTR = "INFO";
        public static readonly string WARNSTR = "WARN";
        public static readonly string ERRORSTR = "ERROR";
        public static readonly string FATALSTR = "FATAL";
        public static readonly string SPC = " ";
        public static readonly string CLASSUNK = "StaticClass";
        public static readonly string NULLSTR = "null";
        static readonly FileLogger instance = new FileLogger();
        static readonly DateTime epochTime = new DateTime(1970, 1, 1);
        static FileLogger()
        {
        }
        public override object InitializeLifetimeService()
        {
            return null;
        }



        /// <summary>
        /// 
        /// </summary>
        public static FileLogger Instance
        {
            get
            {
                return (instance);
            }
        }

        #region Private Fields
        private FileInfo fileInfo;
        private StreamWriter sWriter;
        private LogLevel logLevel;
        private string overrideFileName;
        private bool enabledFlag;
        private IsLogLevelChecker logLevelChecker;
        private LogLevelStringGenerator logLevelStringGen;
        private LogMessageHandler logMsg;
        private LogMessageFormatHandler logMsgFmt;
        private DateStampGenerator dateStampGenerator;
        private List<StreamWriter> auxWriters;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void openStream()
        {
            if (this.enabledFlag == false ||
                (fileInfo != null && this.sWriter != StreamWriter.Null))
                return;
            lock (mutexObj)
            {

                try
                {
                    if (string.IsNullOrEmpty(this.overrideFileName))
                    {
                        this.fileInfo = new FileInfo(FILENAME);
                    }
                    else
                    {
                        this.fileInfo = new FileInfo(this.overrideFileName);
                    }
                }
                catch (System.Security.SecurityException)
                {
                    //Cannot write to the log file
                    //Set enabled to false
                    this.enabledFlag = false;
                    //Exit method immediately
                    return;
                }
                catch (System.Exception)
                {
                    //Exception thrown when opening log file
                    //Set enabled to false
                    this.enabledFlag = false;
                    //Exit method immediately
                    return;
                }

                if (this.fileInfo.Exists)
                {
                    this.sWriter = fileInfo.AppendText();
                }
                else
                {
                    //Ensure the path exists prior to calling fileInfo.CreateText
                    var idx = this.overrideFileName.LastIndexOf(@"\", StringComparison.OrdinalIgnoreCase);
                    if (idx == -1)
                    {
                        idx = this.overrideFileName.LastIndexOf(@"/", StringComparison.OrdinalIgnoreCase);
                    }
                    var dirName = string.Empty;
                    if (idx == -1)
                    {
                        //We have no directory name, must assume current directory
                        dirName = Environment.CurrentDirectory;
                    }
                    else
                    {
                        dirName = this.overrideFileName.Substring(0, idx);
                    }

                    try
                    {
                        Directory.CreateDirectory(dirName);
                    }
                    catch (System.Exception)
                    {
                        this.sWriter = StreamWriter.Null;
                        this.enabledFlag = false;
                        this.fileInfo = null;
                        return;
                    }

                    try
                    {
                        this.sWriter = this.fileInfo.CreateText();
                    }
                    catch (System.Exception)
                    {
                        this.sWriter = StreamWriter.Null;
                        this.enabledFlag = false;
                        this.fileInfo = null;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void closeStream()
        {
            if (this.sWriter == StreamWriter.Null)
                return;
            //Do not check enabled flag here as it might
            //close the stream if the stream is open when
            //the logger is disabled.
            lock (mutexObj)
            {
                try
                {
                    this.sWriter.Flush();
                    this.sWriter.Close();
                    if (CollectionUtilities.isNotEmpty(this.auxWriters))
                    {
                        foreach (var st in this.auxWriters)
                        {
                            if (st == null) continue;
                            try
                            {
                                st.Flush();
                                st.Close();
                            }
                            catch (System.Exception)
                            {
                                //Can't do anything here Vern...just ponder, and move on ;)
                                return;
                            }
                        }
                    }
                }
                catch (System.Exception)
                {
                    //Exception caught upon
                    //closing file logger - cannot
                    //do anything with it at this point
                    return;
                }
                this.sWriter.Dispose();
                this.sWriter = StreamWriter.Null;
                this.fileInfo = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public FileLogger()
        {
            this.sWriter = StreamWriter.Null;
            this.enabledFlag = true;
            this.fileInfo = null;
            this.overrideFileName = string.Empty;
            this.auxWriters = new List<StreamWriter>(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="levelChk"></param>
        /// <param name="genStr"></param>
        /// <param name="dateGen"></param>
        /// <param name="msg"></param>
        /// <param name="msgFmt"></param>
        public void initializeLogger(
            string fileName,
            IsLogLevelChecker levelChk,
            LogLevelStringGenerator genStr,
            DateStampGenerator dateGen,
            LogMessageHandler msg,
            LogMessageFormatHandler msgFmt)
        {
            if (this.enabledFlag == false ||
                levelChk == null ||
                genStr == null ||
                msgFmt == null ||
                dateGen == null)
            {
                return;
            }
            lock (mutexObj)
            {
                this.overrideFileName = null;
                if (!string.IsNullOrEmpty(fileName))
                {
                    this.overrideFileName = fileName;
                }
                this.logLevelChecker = levelChk;
                this.logLevelStringGen = genStr;
                this.logMsg = msg;
                this.logMsgFmt = msgFmt;
                this.dateStampGenerator = dateGen;
            }
            this.openStream();
        }

        /// <summary>
        /// Enables / disables the logger
        /// </summary>
        /// <param name="enabled"></param>
        public void setEnabled(bool enabled)
        {
            lock (mutexObj)
            {
                this.enabledFlag = enabled;
            }

            if (this.enabledFlag == false &&
                this.sWriter != StreamWriter.Null)
            {
                this.closeStream();
            }
        }

        public void addOutputStream(StreamWriter writer)
        {
            if (writer == null) return;
            lock (mutexObj)
            {
                this.auxWriters.Add(writer);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public void setOutputStream(StreamWriter writer)
        {
            //Do nothing in this method - required to satisfy
            //logger interface only
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public LogLevel getLogLevel()
        {
            return (this.logLevel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool isLogLevel(LogLevel level)
        {
            if (this.logLevelChecker == null) return (false);
            return (this.logLevelChecker(level, this.logLevel));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsLogDebug
        {
            get
            {
                return (this.logLevel <= LogLevel.DEBUG);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsLogInfo
        {
            get
            {
                return (this.logLevel <= LogLevel.INFO);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsLogWarn
        {
            get
            {
                return (this.logLevel <= LogLevel.WARN);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsLogError
        {
            get
            {
                return (this.logLevel <= LogLevel.ERROR);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsLogFatal
        {
            get
            {
                return (this.logLevel <= LogLevel.FATAL);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        public void setLogLevel(LogLevel level)
        {
            lock (mutexObj)
            {
                this.logLevel = level;
            }
        }

        public void setLogLevel(string level)
        {
            lock (mutexObj)
            {
                if (!string.IsNullOrEmpty(level))
                {
                    if (level.Equals(DEBUGSTR))
                    {
                        this.logLevel = LogLevel.DEBUG;
                    }
                    else if (level.Equals(INFOSTR))
                    {
                        this.logLevel = LogLevel.INFO;
                    }
                    else if (level.Equals(WARNSTR))
                    {
                        this.logLevel = LogLevel.WARN;
                    }
                    else if (level.Equals(ERRORSTR))
                    {
                        this.logLevel = LogLevel.ERROR;
                    }
                    else if (level.Equals(FATALSTR))
                    {
                        this.logLevel = LogLevel.FATAL;
                    }
                    else
                    {
                        this.logLevel = LogLevel.DEBUG;
                    }
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return (this.enabledFlag);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        public void logMessage(LogLevel level, object sender, string msg)
        {
            if (this.enabledFlag == false ||
                this.sWriter == StreamWriter.Null ||
                string.IsNullOrEmpty(msg) ||
                this.logMsg == null)
                return;
            //Generate string data...only lock against the writer when
            //performing the write operation    
            string outData;
            if (this.logMsg(
                this.logLevelChecker,
                this.dateStampGenerator,
                this.logLevelStringGen,
                this.logLevel, level,
                sender, msg, out outData))
            {
                lock (mutexObj)
                {
                    try
                    {
                        this.sWriter.WriteLine(outData);
                        this.sWriter.Flush();
                        if (CollectionUtilities.isNotEmpty(this.auxWriters))
                        {
                            foreach (var st in this.auxWriters)
                            {
                                if (st == null) continue;
                                try
                                {
                                    st.WriteLine(outData);
                                    st.Flush();
                                }
                                catch (System.Exception)
                                {
                                    //Can't do anything here Vern...just ponder, and move on ;)
                                    return;
                                }
                            }
                        }
                    }
                    catch (System.Exception)
                    {
                        //Cannot do anything with exception at this point Vern...
                        return;
                    }
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="sender"></param>
        /// <param name="msgFmt"></param>
        /// <param name="vars"></param>
        public void logMessage(LogLevel level, object sender, string msgFmt, params object[] vars)
        {
            if (this.enabledFlag == false || this.sWriter == StreamWriter.Null ||
                string.IsNullOrEmpty(msgFmt) || vars == null || vars.Length <= 0 ||
                this.logMsgFmt == null)
                return;
            //Generate string data...only lock against the writer when
            //performing the write operation
            string outData;
            if (this.logMsgFmt(
                this.logLevelChecker,
                this.dateStampGenerator,
                this.logLevelStringGen,
                this.logLevel, level,
                sender, out outData, msgFmt, vars))
            {
                lock (mutexObj)
                {
                    try
                    {
                        this.sWriter.WriteLine(outData);
                        this.sWriter.Flush();
                        if (CollectionUtilities.isNotEmpty(this.auxWriters))
                        {
                            foreach (StreamWriter st in this.auxWriters)
                            {
                                if (st == null) continue;
                                try
                                {
                                    st.WriteLine(outData);
                                    st.Flush();
                                }
                                catch (System.Exception)
                                {
                                    //Can't do anything here Vern...just ponder, and move on ;)
                                    return;
                                }
                            }
                        }
                    }
                    catch (System.Exception)
                    {
                        //Cannot do anything with the exception here Vern...
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void flush()
        {
            if (this.enabledFlag == false || this.sWriter == StreamWriter.Null)
                return;
            lock (mutexObj)
            {
                try
                {
                    this.sWriter.Flush();
                    if (CollectionUtilities.isNotEmpty(this.auxWriters))
                    {
                        foreach (StreamWriter st in this.auxWriters)
                        {
                            if (st == null) continue;
                            try
                            {
                                st.Flush();
                            }
                            catch (System.Exception)
                            {
                                //Can't do anything here Vern...just ponder, and move on ;)
                                return;
                            }
                        }
                    }
                }
                catch (System.Exception)
                {
                    //Cannot do anything with the exception here Vern...
                    return;
                }
            }
        }

        #region IDisposable Members

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.closeStream();
        }

        #endregion
    }
}
