using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace CouchConsoleApp.thread
{
    internal class ThreadJob
    {
        private string str = "";
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ThreadJob));
        private BackgroundWorker backgroundWorkerForCount = null;
        private DoWorkEventArgs e = null;

        public ThreadJob(BackgroundWorker backgroundWorkerForCount, DoWorkEventArgs e)
        {
            this.backgroundWorkerForCount = backgroundWorkerForCount;
            this.e = e;
        }

        public string getVal()
        {
            return str;
        }

        public void loop()
        {
            if (backgroundWorkerForCount.CancellationPending)
            {
                e.Cancel = true;
            }
            else
            {
                for(int i = 0; i < 1000; i++)
                {
                    if (backgroundWorkerForCount.CancellationPending == false)
                    {
                        str = (string.Format("Scroll Test...........{0}........{1}",Thread.CurrentThread.Name,i));
                        backgroundWorkerForCount.ReportProgress(0, str);
                        //log.Debug(str);
                        Thread.Sleep(50);
                    }
                    else
                    {
                        e.Cancel = true;
                        break;
                    }
                }
            }
        }
    }
}