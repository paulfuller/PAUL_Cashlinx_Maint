using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Appender;
using log4net.Core;

namespace CouchConsoleApp.log
{
   public class CConnectorCustomAppender : IAppender
    {
        private string CConnectorCustomAppenderName = "CConnectorCustomAppender";
        public void Close()
        {
           CConnLogHelper.Instance.closeLogger();
        }

       //private readonly Object lockObj=new object();

        public void DoAppend(LoggingEvent loggingEvent)
        {
            //loggingEvent.
            //lock (lockObj)
            //{
                string loggerName = loggingEvent.LoggerName;
                if(loggingEvent.MessageObject==null)
                    return;
                string msg = loggingEvent.MessageObject.ToString();
                //form.Form1.Instance().appendConsole(msg);
                loggingEvent.GetType();
                if (loggingEvent.Level.DisplayName.Equals("DEBUG"))
                {
                    CConnLogHelper.Instance.Debug(msg, Type.GetType(loggerName));
                }
                else if (loggingEvent.Level.DisplayName.Equals("INFO"))
                {
                    CConnLogHelper.Instance.Info(msg, Type.GetType(loggerName));
                }
                else if (loggingEvent.Level.DisplayName.Equals("ERROR"))
                {
                    CConnLogHelper.Instance.Error(msg, null, Type.GetType(loggerName));
                }
                else
                {
                    CConnLogHelper.Instance.Debug(msg, Type.GetType(loggerName));
                }
           // }

        }

        public string Name
        {
            get
            {
                return CConnectorCustomAppenderName;
            }
            set
            {
                CConnectorCustomAppenderName = value; 
            }
        }
    }
}
