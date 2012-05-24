using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Common.Libraries.Utility.String;
using CouchConsoleApp.form;
using CouchConsoleApp.thread;
using CouchConsoleApp.vo;


namespace CouchConsoleApp.events
{
    internal class BgWorkerJobAllocationEventHandler
    {
        private ArchProcessForm mainForm = null;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(BgWorkerJobAllocationEventHandler));
        //private JobAllocationHandler2 _work = null;
        private Thread workThread = null;
        private int totalRecords = 0;

        public BgWorkerJobAllocationEventHandler(ArchProcessForm mainForm, int totalRecords)
        {
            this.mainForm = mainForm;
            this.totalRecords = totalRecords;
        }

        public void BackgroundWorkerForGetDocsProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string str = e.UserState as string;

            //this.log.Debug(str);
            string[] strArr = null;
            if (!string.IsNullOrEmpty(str))
            {
                strArr = str.Split(':');
            }
            if (strArr != null)
            {
                mainForm.setUpdates(strArr[0], strArr[1], strArr[2]);
            }
            if (!(e.ProgressPercentage < 0) && !(e.ProgressPercentage > 100))
            {
                mainForm.progressBar1.Value = e.ProgressPercentage;
                mainForm.progText.Text = mainForm.progressBar1.Value.ToString() + "%";
            }
            this.mainForm.Refresh();
        }

        public void BackgroundWorkerForGetDocsDoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            //this.log.Debug("Doing work");
            //e.Argument
            int arg = (int)e.Argument;
            log.Info("Memory usage @ Job Allocation Worker :" +
                     System.Diagnostics.Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024 + "MB");
            if (arg == 1)
            {
                //Perform recovery for documents that are failed
                JobAllocationHandler _work = new JobAllocationHandler(mainForm, worker, e, this, totalRecords, true);
                //_work = new JobAllocationHandler2(mainForm, worker, e, this, totalRecords, true);

                workThread = new Thread(new ThreadStart(_work.process));
                workThread.Start();
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                while(workThread.IsAlive)
                {
                    this.mainForm.appendTime(stopwatch.Elapsed.ToString());
                }
                stopwatch.Stop();
                if (_work.getFailedCount() > 0)
                {
                    MessageBox.Show("Archival process completed with some documents failed");
                }
                else
                {
                    MessageBox.Show("Archival process completed Successfully");
                }
                _work = null;
            }
            else
            {
                JobAllocationHandlerMain _work = new JobAllocationHandlerMain(mainForm, worker, e, this, totalRecords, false);
                workThread = new Thread(new ThreadStart(_work.process));
                workThread.Start();
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                while(workThread.IsAlive)
                {
                    this.mainForm.appendTime(stopwatch.Elapsed.ToString());
                }
                stopwatch.Stop();
                if (_work.getFailedCount() > 0)
                {
                    MessageBox.Show("Archival process completed with some documents failed");
                }
                else
                {
                    MessageBox.Show("Archival process completed Successfully");
                }
                _work = null;
            }
        }

        public void BgWorkGetDocsCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            log.Info("Memory usage @ Job Allocation Handler completed:" +
                     System.Diagnostics.Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024 + "MB");
            if (e.Cancelled)
            {
                //workThread.Abort();
                this.log.Debug("Cancelled!");
            }
            else if (e.Error != null)
            {
                this.log.Debug("Error: " + e.Error.Message);
            }
            else
            {
                this.log.Debug("Done!");
            }
            this.mainForm.stopButton.Visible = false;
            mainForm.enableBack();
            this.mainForm.Text = "Done";
            this.mainForm.setProgressState(false);
            //mainForm.disposeAction();
            int i = 10;
            while(true)
            {
                if(i==10)
                    break;
                printMemoryUsage();
                Thread.Sleep(10000);
                i++;
            }
        }

        private void printMemoryUsage()
        {
            //Update process data
            Process curProcess = System.Diagnostics.Process.GetCurrentProcess();
            TimeSpan curPrivProcTime = curProcess.PrivilegedProcessorTime;
            DateTime startTime = curProcess.StartTime;
            long curNumThreads = curProcess.Threads.Count;
            TimeSpan curProcTime = curProcess.TotalProcessorTime;
            TimeSpan curUsrProcTime = curProcess.UserProcessorTime;
            long curVirtMem = curProcess.VirtualMemorySize64;
            long curPhysMem = curProcess.WorkingSet64;
            log.Info("#######################Back to Job Allocation Event handler##########################");
            log.Info("NonpagedSystemMemorySize64  = " + curProcess.NonpagedSystemMemorySize64 / 1024 / 1024 + " MB");
            log.Info("PagedMemorySize64  = " + curProcess.PagedMemorySize64 / 1024 / 1024 + " MB");
            log.Info("PagedSystemMemorySize64  = " + curProcess.PagedSystemMemorySize64 / 1024 / 1024 + " MB");
            log.Info("PeakPagedMemorySize64  = " + curProcess.PeakPagedMemorySize64 / 1024 / 1024 + " MB");
            log.Info("PeakVirtualMemorySize64 [peak Virtual memory used]  = " + curProcess.PeakVirtualMemorySize64 / 1024 / 1024 + " MB");
            log.Info("curProcess.PeakWorkingSet64[peak physical memory used]  = " + curProcess.PeakWorkingSet64 / 1024 / 1024 + " MB");
            log.Info("Private Memory  = " + curProcess.PrivateMemorySize64 / 1024 / 1024 + " MB");
            log.Info("CPU Time  = " +
                     StringUtilities.TimeSpanToString(curPrivProcTime, StringUtilities.MaxTimeResolution.SECONDS,
                                                      StringUtilities.TimeFormat.WHOLEFRACTIONUNIT));
            log.Info("Current no of threads :" + curNumThreads);
            log.Info("Total Process Time  = " +
                     StringUtilities.TimeSpanToString(curProcTime, StringUtilities.MaxTimeResolution.SECONDS,
                                                      StringUtilities.TimeFormat.WHOLEFRACTIONUNIT));
            log.Info("User CPU Time  = " +
                     StringUtilities.TimeSpanToString(curUsrProcTime, StringUtilities.MaxTimeResolution.SECONDS,
                                                      StringUtilities.TimeFormat.WHOLEFRACTIONUNIT));
            log.Info("Virtual Memory [Page file usage]" + curVirtMem / 1024 / 1024 + " MB");
            log.Info("Physical memory [RAM usage] " + curPhysMem / 1024 / 1024 + " MB");
            log.Info("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
        }
    }
}