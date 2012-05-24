using System;
using System.IO;

namespace Common.Libraries.Utility.Logger
{
    public class TempFileLogger : ILogger, IDisposable
    {
        #region Private Fields

        private readonly string logFileName;
        private LogLevel logLevel;
        private StreamWriter outputFileStream;
        private FileInfo fileInfo;
        private readonly bool initialized;
        private bool enabledFlag;
        private readonly IsLogLevelChecker levelChecker;
        private readonly LogLevelStringGenerator levelStringGen;
        private readonly LogMessageHandler logMsgHandler;
        private readonly LogMessageFormatHandler logMsgFmtHandler;
        private readonly DateStampGenerator dateStampHandler;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void openStream()
        {
            if (this.enabledFlag == false ||
                (fileInfo != null && this.outputFileStream != StreamWriter.Null) ||
                string.IsNullOrEmpty(this.logFileName))
            {
                return;
            }
            try
            {
                this.fileInfo = new FileInfo(this.logFileName);                    
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
                this.outputFileStream = fileInfo.AppendText();
            }
            else
            {
                //Ensure the path exists prior to calling fileInfo.CreateText
                var idx = this.logFileName.LastIndexOf(@"\", StringComparison.OrdinalIgnoreCase);
                if (idx == -1)
                {
                    idx = this.logFileName.LastIndexOf(@"/", StringComparison.OrdinalIgnoreCase);
                }
                var dirName = string.Empty;
                if (idx == -1)
                {
                    //We have no directory name, must assume current directory
                    dirName = Environment.CurrentDirectory;
                }
                else
                {
                    dirName = this.logFileName.Substring(0, idx);
                }

                try
                {
                    Directory.CreateDirectory(dirName);
                }
                catch (System.Exception)
                {
                    this.outputFileStream = StreamWriter.Null;
                    this.enabledFlag = false;
                    this.fileInfo = null;
                    return;
                }

                try
                {
                    this.outputFileStream = this.fileInfo.CreateText();
                }
                catch (System.Exception)
                {
                    this.outputFileStream = StreamWriter.Null;
                    this.enabledFlag = false;
                    this.fileInfo = null;
                    return;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void closeStream()
        {
            if (this.outputFileStream == StreamWriter.Null)
                return;
            //Do not check enabled flag here as it might
            //close the stream if the stream is open when
            //the logger is disabled.
            try
            {
                this.outputFileStream.Flush();
                this.outputFileStream.Close();
            }
            catch (System.Exception)
            {
                //Exception caught upon
                //closing file logger - cannot
                //do anything with it at this point Vern...
                return;
            }
            this.outputFileStream.Dispose();
            this.outputFileStream = StreamWriter.Null;                
            this.fileInfo = null;
        }


        #region Implementation of ILogger

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
            return (this.levelChecker(level, this.logLevel));
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
        public bool IsInitialized
        {
            get
            {
                return (this.initialized);
            }
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileNamePrefix"></param>
        /// <param name="levelChk"></param>
        /// <param name="genStr"></param>
        /// <param name="msg"></param>
        /// <param name="msgFmt"></param>
        /// <param name="dateGen"></param>
        public TempFileLogger(
            string fileNamePrefix,
            IsLogLevelChecker levelChk,
            LogLevelStringGenerator genStr,
            LogMessageHandler msg,
            LogMessageFormatHandler msgFmt,
            DateStampGenerator dateGen
            )
        {
            this.initialized = false;
            this.fileInfo = null;
            if (string.IsNullOrEmpty(fileNamePrefix) ||
                levelChk == null ||
                genStr == null ||
                msg == null ||
                msgFmt == null ||
                dateGen == null)
            {
                return;
            }
            try
            {
                this.logFileName = fileNamePrefix;
                this.levelChecker = levelChk;
                this.levelStringGen = genStr;
                this.logMsgHandler = msg;
                this.logMsgFmtHandler = msgFmt;
                this.dateStampHandler = dateGen;
                this.enabledFlag = true;
                this.openStream();
                this.initialized = true;
            }
            catch
            {
                this.initialized = false;
            }
        }

        
        public void setEnabled(bool enabled)
        {
            this.enabledFlag = enabled;
            if (!this.enabledFlag && this.outputFileStream != StreamWriter.Null)
            {
                this.closeStream();
            }
        }

        public void addOutputStream(StreamWriter writer)
        {
            //Do nothing, not accepting aux output streams
        }

        public void setOutputStream(StreamWriter writer)
        {
            //Do nothing, not accepting output streams
        }

        public void setLogLevel(LogLevel level)
        {
            this.logLevel = level;
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
                this.outputFileStream == StreamWriter.Null ||
                string.IsNullOrEmpty(msg) ||
                this.logMsgHandler == null)
            {
                return;
            }
            //Generate string data...only lock against the writer when
            //performing the write operation
            string outData;
            if (this.logMsgHandler(
                    this.levelChecker, 
                    this.dateStampHandler, 
                    this.levelStringGen,
                    this.logLevel, level,
                    sender, msg, out outData))
            {
                try
                {
                    this.outputFileStream.WriteLine(outData);
                    this.outputFileStream.Flush();
                }
                catch (System.Exception)
                {
                    //Cannot do anything with exception at this point Vern...
                    return;
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
            if (this.enabledFlag == false ||
                this.outputFileStream == StreamWriter.Null ||
                string.IsNullOrEmpty(msgFmt) ||
                vars == null ||
                vars.Length <= 0 ||
                this.logMsgFmtHandler == null)
            {
                return;
            }
            //Generate string data...only lock against the writer when
            //performing the write operation           
            string outData;
            if (this.logMsgFmtHandler(
                    this.levelChecker, 
                    this.dateStampHandler,
                    this.levelStringGen,
                    this.logLevel, level, sender, out outData, msgFmt, vars))
            {
                try
                {
                    this.outputFileStream.WriteLine(outData);
                    this.outputFileStream.Flush();
                }
                catch (System.Exception)
                {
                    //Cannot do anything with the exception here Vern...
                    return;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void flush()
        {
            if (this.enabledFlag == false || this.outputFileStream == StreamWriter.Null)
            {
                return;
            }
            try
            {
                this.outputFileStream.Flush();
            }
            catch (System.Exception)
            {
                //Cannot do anything with the exception here Vern...
                return;
            }
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            if (this.outputFileStream != StreamWriter.Null)
            {
                this.closeStream();
            }
        }

        #endregion
    }
}
