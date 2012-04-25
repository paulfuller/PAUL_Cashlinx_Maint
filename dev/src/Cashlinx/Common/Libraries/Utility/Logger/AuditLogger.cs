using System;
using System.Collections.Generic;

namespace Common.Libraries.Utility.Logger
{
    public sealed class AuditLogger : MarshalByRefObject, IAuditLogger, IDisposable
    {
        #region Static Constant Fields
        /// <summary>
        /// Audit date field
        /// </summary>
        public static readonly string DATEFIELD = "AUDITDATE";
        /// <summary>
        /// Audit time field
        /// </summary>
        public static readonly string TIMEFIELD = "AUDITTIME";
        /// <summary>
        /// Audit type field
        /// </summary>
        public static readonly string TYPEFIELD = "AUDITTYPE";
        #endregion

        #region Singleton Fields
        static readonly object mutexObj = new object();
        static readonly AuditLogger instance = new AuditLogger();
        static AuditLogger()
        {

        }
        public override object InitializeLifetimeService()
        {
            return null;
        }

        public static AuditLogger Instance
        {
            get
            {
                return (instance);
            }
        }
        #endregion

        #region Private Fields
        private bool isEnabled;
        private AuditLogHandler auditHandler;
        private AuditLogEnabledChangeHandler auditChangeHandler;

        #endregion

        #region Implementation of IAuditLogger

        public bool IsEnabled
        {
            get { return this.isEnabled; } 
        }

        public void SetEnabled(bool enabled)
        {
            bool oldEnabled = this.isEnabled;
            this.isEnabled = enabled;
            //Must restart the logging back-end upon receiving
            //a true to this method if the logger had been
            //shut-off previously
            if (this.auditChangeHandler != null)
            {
                this.auditChangeHandler(oldEnabled, this.isEnabled);
            }
        }

        public void SetAuditLogHandler(AuditLogHandler aHandler)
        {
            this.auditHandler = aHandler;
        }

        public void SetAuditLogEnabledChangeHandler(AuditLogEnabledChangeHandler aEnHandler)
        {
            this.auditChangeHandler = aEnHandler;
        }

        public void LogAuditMessage(object aType, IDictionary<string, object> auditData)
        {
            //Add the type to the data dictionary
            auditData.Add(new KeyValuePair<string,object>(TYPEFIELD, aType));
            
            //Add date and time to the audit data
            DateTime curDate = DateTime.Now;
            auditData.Add(new KeyValuePair<string,object>(DATEFIELD, curDate.Date));
            auditData.Add(new KeyValuePair<string,object>(TIMEFIELD, curDate.TimeOfDay));

            if (this.auditHandler != null)
            {
                this.auditHandler(auditData);
            }
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            this.auditChangeHandler = null;
            this.auditHandler = null;
        }

        #endregion
    }
}
