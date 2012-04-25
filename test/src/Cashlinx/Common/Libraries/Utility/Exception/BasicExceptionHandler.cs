using System;
using System.Collections.Generic;
using Common.Libraries.Utility.Logger;

namespace Common.Libraries.Utility.Exception
{
    public sealed class BasicExceptionHandler : MarshalByRefObject
    {
// ReSharper disable InconsistentNaming
        static readonly object mutexObj = new object();
// ReSharper restore InconsistentNaming
        static readonly BasicExceptionHandler instance = new BasicExceptionHandler();
        static BasicExceptionHandler()
        {
        }
        public override object InitializeLifetimeService()
        {
            return null;
        }



        public static BasicExceptionHandler Instance
        {
            get
            {
                return (instance);
            }
        }

        private List<SystemException> systemExceptionList;
        private List<ApplicationException> applicationExceptionList;
        private List<System.Exception> baseExceptionList;
        private ILogger logger;
        private Func<bool> exceptionCallback;
        private bool printStackTrace;

        public List<SystemException> SystemExceptions
        {
            get
            {
                return (this.systemExceptionList);
            }
        }

        public bool AnySystemExceptions
        {
            get
            {
                return (this.systemExceptionList.Count > 0);
            }
        }

        public int NumberSystemExceptionsSinceLastEvent
        {
            private set; get;
        }

        public List<ApplicationException> ApplicationExceptions
        {
            get
            {
                return (this.applicationExceptionList);
            }
        }

        public bool AnyApplicationExceptions
        {
            get
            {
                return (this.applicationExceptionList.Count > 0);
            }
        }

        public int NumberApplicationExceptionsSinceLastEvent
        {
            private set; get;
        }

        public List<System.Exception> BaseExceptions
        {
            get
            {
                return (this.baseExceptionList);
            }
        }

        public bool AnyBaseExceptions
        {
            get
            {
                return (this.baseExceptionList.Count > 0);
            }
        }

        public int NumberBaseExceptionsSinceLastEvent
        {
            private set; get;
        }

        public bool PrintStackTrace
        {
            set
            {
                lock (mutexObj)
                {
                    this.printStackTrace = value;
                }
            }

            get
            {
                return (this.printStackTrace);
            }
        }

        public void clearAllExceptions()
        {
            lock(mutexObj)
            {
                this.systemExceptionList.Clear();
                this.applicationExceptionList.Clear();
                this.baseExceptionList.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="excCallback"></param>
        public void setExceptionCallback(Func<bool> excCallback)
        {
            this.exceptionCallback = excCallback;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logr">If logging, pass in a logger that conforms to the ILogger interface</param>
        public BasicExceptionHandler()
        {
            lock (mutexObj)
            {
                this.systemExceptionList = new List<SystemException>();
                this.applicationExceptionList = new List<ApplicationException>();
                this.baseExceptionList = new List<System.Exception>();
                this.logger = FileLogger.Instance;
                this.printStackTrace = false;
                this.exceptionCallback = null;
                this.NumberSystemExceptionsSinceLastEvent = 0;
                this.NumberApplicationExceptionsSinceLastEvent = 0;
                this.NumberBaseExceptionsSinceLastEvent = 0;
            }
        }

        /// <summary>
        /// Adds an SystemException object to the exception handler.  
        /// Will only log if the logger is valid.  Will only print the stack
        /// trace if the print stack trace flag is enabled (true).
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="sysEx"></param>
        public void AddException(string msg, SystemException sysEx)
        {
            if (sysEx == null)
            {
                return;
            }
            int uniqId = this.systemExceptionList.Count;
            if (this.logger != null)
            {
                if (this.logger.IsLogWarn)
                    this.logger.logMessage(LogLevel.WARN, sysEx.Source, (string)((msg == null) ? "" : "(" + msg + ")") + "(" + uniqId + ")SystemException Thrown: " + sysEx.TargetSite + ":" + sysEx.Message);
            }
            if (this.printStackTrace)
            {
                if (this.logger != null)
                {
                    if (this.logger.IsLogInfo)
                        this.logger.logMessage(LogLevel.INFO, sysEx.Source, (string)((msg == null) ? "" : "(" + msg + ")") + "(" + uniqId + ")SystemException Details: " + sysEx.Data + ":" + sysEx.StackTrace);
                }
            }
            lock (mutexObj)
            {
                this.systemExceptionList.Add(sysEx);                
            }
            lock (mutexObj)
            {
                if (this.exceptionCallback != null)
                {
                    this.exceptionCallback();
                }
                else
                {
                    //Purge the system exceptions
                    this.systemExceptionList.Clear();
                }
            }
        }

        /// <summary>
        /// Adds an ApplicationException object to the exception handler.  
        /// Will only log if the logger is valid.  Will only print the stack
        /// trace if the print stack trace flag is enabled (true).
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="appEx">ApplicationException instance</param>
        public void AddException(string msg, ApplicationException appEx)
        {
            if (appEx == null)
            {
                return;
            }
            int uniqId = this.applicationExceptionList.Count;
            if (this.logger != null)
            {
                if (this.logger.IsLogWarn)
                    this.logger.logMessage(LogLevel.WARN, appEx.Source, (string)((msg == null) ? "" : "(" + msg + ")") + "(" + uniqId + ")ApplicationException Thrown: " + appEx.TargetSite + ":" + appEx.Message);
            }
            if (this.printStackTrace)
            {
                if (this.logger != null)
                {
                    if (this.logger.IsLogInfo)
                        this.logger.logMessage(LogLevel.INFO, appEx.Source, (string)((msg == null) ? "" : "(" + msg + ")") + "(" + uniqId + ")ApplicationException Details: " + appEx.Data + ":" + appEx.StackTrace);
                }
            }
            lock (mutexObj)
            {
                this.applicationExceptionList.Add(appEx);
            }
            lock (mutexObj)
            {
                if (this.exceptionCallback != null)
                {
                    this.exceptionCallback();
                }
                else
                {
                    //Purge application exceptions
                    this.applicationExceptionList.Clear();
                }
            }
        }

        /// <summary>
        /// Adds a base exception (System.Exception)object to the exception handler.  
        /// Will only log if the logger is valid.  Will only print the stack
        /// trace if the print stack trace flag is enabled (true).
        /// </summary>
        public void AddException(string msg, System.Exception eX)
        {
            if (eX == null)
            {
                return;
            }
            int uniqId = this.baseExceptionList.Count;
            if (this.logger != null)
            {
                if (this.logger.IsLogWarn)
                    this.logger.logMessage(LogLevel.WARN, eX.Source, (string)((msg == null) ? "" : "(" + msg + ")") + "(" + uniqId + ")ApplicationException Thrown: " + eX.TargetSite + ":" + eX.Message);
            }
            if (this.printStackTrace)
            {
                if (this.logger != null)
                {
                    if (this.logger.IsLogInfo)
                        this.logger.logMessage(LogLevel.INFO, eX.Source, (string)((msg == null) ? "" : "(" + msg + ")") + "(" + uniqId + ")ApplicationException Details: " + eX.Data + ":" + eX.StackTrace);
                }
            }
            lock (mutexObj)
            {
                this.baseExceptionList.Add(eX);
            }

            lock (mutexObj)
            {
                if (this.exceptionCallback != null)
                {
                    this.exceptionCallback();
                }
                else
                {
                    //Purge exceptions
                    this.baseExceptionList.Clear();
                }
            }
        }
    }
}
