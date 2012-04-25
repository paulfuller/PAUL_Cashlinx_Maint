using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.String;
using log4net.Config;


namespace CouchConsoleApp.log
{
    public class CConnLogHelper
    {
        private FileLogger fileLogger;
        private static readonly  CConnLogHelper instance=new CConnLogHelper();

        private log4net.ILog log = null;

        private CConnLogHelper()
        {
            initCLXLog();
            
        }

        private void initCLXLog()
        {
            this.fileLogger = FileLogger.Instance;
            var logFileName = FileLogger.FILENAME;
            this.fileLogger.setEnabled(true);
            this.fileLogger.initializeLogger(logFileName,
                            DefaultLoggerHandlers.defaultLogLevelCheckHandler,
                            DefaultLoggerHandlers.defaultLogLevelGenerator,
                            DefaultLoggerHandlers.defaultDateStampGenerator,
                            DefaultLoggerHandlers.defaultLogMessageHandler,
                            DefaultLoggerHandlers.defaultLogMessageFormatHandler);

            this.fileLogger.setLogLevel("ALL");
            var asteriskString = StringUtilities.fillString("*", 150);
            this.fileLogger.logMessage(LogLevel.INFO, this, asteriskString);
            this.fileLogger.logMessage(LogLevel.INFO, this, "Couch Connector Initialized at {0} - {1}",
                                        DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
            this.fileLogger.logMessage(LogLevel.INFO, this, asteriskString);
            this.fileLogger.flush(); 
        }

        public static CConnLogHelper Instance
        {
            get
            {
                return (instance);
            }
        }

        public void Debug(string msg,Type type)
        {
            this.fileLogger.logMessage(LogLevel.DEBUG, type, msg); 
        }

        public void Error(string msg,Exception exp,Type type)
        {
            if (exp!=null)
                this.fileLogger.logMessage(LogLevel.ERROR, type, msg+" : "+exp.ToString());
            else
            {
                this.fileLogger.logMessage(LogLevel.ERROR, type, msg);
            }
        }

        public void Info(string msg, Type type)
        {
            this.fileLogger.logMessage(LogLevel.INFO, type,msg); 
        }

        public void closeLogger()
        {
            this.fileLogger.Dispose();
        }

    }
}
