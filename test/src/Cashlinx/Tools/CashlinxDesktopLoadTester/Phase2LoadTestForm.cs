using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CashlinxDesktopLoadTester.ThreadController;
using CashlinxDesktopLoadTester.WhiteBoxController;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PawnUtilities.Collection;
using PawnUtilities.String;

namespace CashlinxDesktopLoadTester
{
    public partial class Phase2LoadTestForm : Form
    {
        public static readonly uint DEFAULT_DELAY = 10;
        public static readonly uint DEFAULT_THREADS = 5;
        public static readonly int MAX_UI_THREADS = 2;
        private static List<ParameterizedThreadStart> threadParams = new List<ParameterizedThreadStart>();
        public enum ProgressionRate
        {
            
        }
        
        private LoadTestInputVO loadVo;
        private DateTime startTestTime;
        private DateTime currentTestTime;
        private List<Thread> uiThreads;
        private List<TimeSpan> timedEvents;
        private CashlinxLoadThreadController loadController;
        private uint numIterations;
        private uint curIteration;
        private bool inputsValid;
        private Process curProcess;
        

        public Phase2LoadTestForm()
        {
            InitializeComponent();
            this.loadVo = new LoadTestInputVO();
            this.uiThreads = new List<Thread>();
            this.timedEvents = new List<TimeSpan>();
            this.loadController = null;
            this.numberUsersComboBox.SelectedIndex = 0;
            this.progressionRateComboBox.SelectedIndex = 0;
            this.delayUserTextBox.Text = "100";
            this.executeFlowComboBox.SelectedIndex = 0;
            this.numIterations = 1;
            //this.curIteration = 0;
            this.inputsValid = false;
        }

        //Thread runner delegate for new pawn loan UI flow
        static void executeNewPawnLoanFlow(Object data)
        {
            if (data == null) return;
            var inputData = (LoadTestInputVO) data;
            //Startup cashlinx desktop loader
            var cdLoad = new CashlinxDesktopLoader(@"c:\p2testapp\CashlinxDesktop.exe");
            Thread.Sleep(100);
            //Thread.Sleep(0);
            if (cdLoad.Initialized)
            {
                cdLoad.ExecuteNewPawnLoanFlow(inputData);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sz"></param>
        /// <returns></returns>
        private bool CreateUIThreads(int sz)
        {
            if (this.uiThreads == null || 
                this.uiThreads.Count > 0 || 
                sz < 1 || sz > MAX_UI_THREADS)
            {
                return(false);
            }
            var parameterizedThreadStart = 
                new ParameterizedThreadStart(Phase2LoadTestForm.executeNewPawnLoanFlow);

            for (int i = 0; i < sz; ++i)
            {
                uiThreads.Add(new Thread(parameterizedThreadStart));
            }
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool StartUIThreads()
        {
            /*if (CollectionUtilities.isEmpty(this.uiThreads))
            {
                return (false);
            }

            foreach(Thread t in this.uiThreads)
            {
                if (t == null) continue;
                t.Start(this.loadVo);
            }
            Thread.Sleep(0);*/
            return (true);
        }

        private bool PauseUIThreads()
        {
            /*if (CollectionUtilities.isEmpty(this.uiThreads))
            {
                return (false);
            }

            foreach(Thread t in this.uiThreads)
            {
                //Suspend has been deprecated...
            }*/
            return (true);
        }

        private bool StopUIThreads()
        {
            /*if (CollectionUtilities.isEmpty(this.uiThreads))
            {
                return (false);
            }
            foreach(Thread t in this.uiThreads)
            {
                if (t == null) continue;
                t.Join();
            }*/
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enabled"></param>
        private void setStartButtonEnabled(bool enabled)
        {
            this.startButton.Enabled = enabled;
            this.startButton.Update();
        }

        private void setStopButtonEnabled(bool enabled)
        {
            this.stopLoadTest.Enabled = enabled;
            this.stopLoadTest.Update();
        }

        private void setPauseButtonEnabled(bool enabled)
        {
            this.pauseButton.Enabled = enabled;
            this.pauseButton.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startButton_Click(object sender, EventArgs e)
        {
            this.setStartButtonEnabled(false);
            this.setPauseButtonEnabled(false);
            this.setStopButtonEnabled(true);

            //Determine progression rate
            var progRate = (string)this.progressionRateComboBox.SelectedItem;
            var pRate = CashlinxLoadThreadController.ProgressionRate.RANDOM;
            if (progRate.Equals("Arithmetic", StringComparison.OrdinalIgnoreCase))
            {
                pRate = CashlinxLoadThreadController.ProgressionRate.ARITHMETIC;
            }
            else if (progRate.Equals("Geometric", StringComparison.OrdinalIgnoreCase))
            {
                pRate = CashlinxLoadThreadController.ProgressionRate.GEOMETRIC;
            }
            var eFlow = CashlinxLoadThreadController.ExecuteFlow.NEWLOAN_EXIST_CUSTOMER;
            uint delayUsers = 0;
            if (!UInt32.TryParse(this.delayUserTextBox.Text, out delayUsers))
            {
                //Use default
                delayUsers = DEFAULT_DELAY;
            }
            uint numThreads;
            if (!UInt32.TryParse((string)this.numberUsersComboBox.SelectedItem, out numThreads))
            {
                //Use default
                numThreads = DEFAULT_THREADS;
            }
            uint numIters;
            if (!UInt32.TryParse((string)this.numberIterationsTextBox.Text, out numIters))
            {
                numIters = 1;
            }

            DateTime dateNow = DateTime.Now;
            string fileDateMonth = ("" + dateNow.Date.Month).PadLeft(2, '0');
            string fileDateDay = ("" + dateNow.Date.Day).PadLeft(2, '0');
            string fileDateYear = ""+dateNow.Date.Year;
            string fileDateHour = ("" + dateNow.TimeOfDay.Hours).PadLeft(2, '0');
            string fileDateMin = ("" + dateNow.TimeOfDay.Minutes).PadLeft(2, '0');
            string fileDateSec = ("" + dateNow.TimeOfDay.Seconds).PadLeft(2, '0');
            string fileName = "LoadTestOutput_" + fileDateMonth + fileDateDay + fileDateYear + "_" +
                              fileDateHour + fileDateMin + fileDateSec + ".csv";
            StreamWriter fileOutput = new StreamWriter(@"c:\tmp\logs\" + fileName);
            try
            {
                fileOutput.AutoFlush = true;
            }
            catch (Exception)
            {
            }

            //Create thread load controller
            this.loadController =
                new CashlinxLoadThreadController(pRate, delayUsers, numThreads,
                                                 1.0d, numIters, this.loadVo, 
                                                 ThreadPriority.Normal, eFlow,
                                                 this.addMessage, fileOutput);
            
            this.startTestTime = DateTime.Now;
            //this.startTimer();

            //Start UI threads if enabled
            //NOTE: Disabling UI threads -----------------------------------------
            /*if (this.uiThreadsCheckBox.Checked == true)
            {
                if (!this.CreateUIThreads(MAX_UI_THREADS))
                {
                    this.setStartButtonEnabled(true);
                    MessageBox.Show("Could not create UI threads", "Load Test Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }
                if (!this.StartUIThreads())
                {
                    this.setStartButtonEnabled(true);
                    MessageBox.Show("Could not start UI threads", "Load Test Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }
                Thread.Sleep(100);
            }*/

            //Perform startup of other threads
            this.addMessage("Starting load test with " + numThreads + " thread(s)");
            this.loadController.startThreads();
            Thread.Sleep(1000);
            this.statusChecker.RunWorkerAsync();
            this.statusChecker.RunWorkerCompleted += statusChecker_RunWorkerCompleted;
        }

        private void statusChecker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                this.addMessage("All threads have completed");
                this.setStopButtonEnabled(false);
                this.setStartButtonEnabled(true);
                DateTime finishDt = DateTime.Now;
                TimeSpan totalTime = finishDt - this.startTestTime;
                MessageBox.Show("Completed Test.  Total time: " +
                                StringUtilities.TimeSpanToString(totalTime, StringUtilities.MaxTimeResolution.SECONDS,
                                                                 StringUtilities.TimeFormat.WHOLEFRACTIONUNIT));
                this.stopTimer(true);
                this.loadController.endThreads();
                this.loadController.resetAll();
            }
            catch(Exception)
            {
                
            }
        }

        private void Phase2LoadTestForm_Load(object sender, EventArgs e)
        {
            this.inputObjectPropertyGrid.SelectedObject = this.loadVo;
            this.inputObjectPropertyGrid.Update();

            //Set start button enabled
            //Disable pause and stop buttons
            this.setStartButtonEnabled(true);
            this.setPauseButtonEnabled(false);
            this.setStopButtonEnabled(false);

            //ensure that timer is stopped
            this.stopTimer(false);
            this.curProcess = Process.GetCurrentProcess();

            //Start process data timer
            this.procDataUpdateTimer.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.procDataUpdateTimer.Stop();
            DialogResult dR = MessageBox.Show("Do you want to exit the application?", "Load Tester Message",
                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dR == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void loadTestExecuteTimer_Tick(object sender, EventArgs e)
        {
            DateTime curTime = DateTime.Now;
            this.currentTestTime = curTime;
            TimeSpan dTime = curTime - this.startTestTime;
            //hours
            double totalHours = dTime.TotalHours;
            double wholeTotalHours = Math.Floor(totalHours);
            double fracTotalHours = totalHours - wholeTotalHours;
            //minutes
            double totalMinutes = fracTotalHours * 60.0d;
            double wholeTotalMinutes = Math.Floor(totalMinutes);
            double fracTotalMinutes = totalMinutes - wholeTotalMinutes;
            //seconds
            double totalSeconds = fracTotalMinutes * 60.0d;
            double wholeTotalSeconds = Math.Floor(totalSeconds);
            double fracTotalSeconds = totalSeconds - wholeTotalSeconds;
            //milliseconds
            double totalMilliseconds = fracTotalSeconds * 1000.0d;
            double wholeTotalMillis = Math.Floor(totalMilliseconds);
            double fracTotalMillis = totalMilliseconds - wholeTotalMillis;

            string hoursStr = (wholeTotalHours < 10) ? "0" + wholeTotalHours : ""+wholeTotalHours;
            if (hoursStr.Length > 2) hoursStr = hoursStr.Substring(0, 2);
            string minsStr = (wholeTotalMinutes < 10) ? "0" + wholeTotalMinutes : "" + wholeTotalMinutes;
            if (minsStr.Length > 2) minsStr = minsStr.Substring(0, 2);
            string secsStr = (wholeTotalSeconds < 10) ? "0" + wholeTotalSeconds : "" + wholeTotalSeconds;
            if (secsStr.Length > 2) secsStr = secsStr.Substring(0, 2);
            string millStr;
            if (wholeTotalMillis < 100 && wholeTotalMillis >= 10)
            {
                millStr = "0" + wholeTotalMillis;
            }
            else
            {
                millStr = (wholeTotalMillis < 10) ? "00" + wholeTotalMillis : "" + wholeTotalMillis;
            }

            //Update field text
            executionTimeDataField.Text = hoursStr + ":" + minsStr + ":" + secsStr + "." + millStr;
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            this.PauseUIThreads();
        }

        private void stopLoadTest_Click(object sender, EventArgs e)
        {
            this.addMessage("Clicked stop load test");
            this.loadController.endThreads();
            this.StopUIThreads();
            //this.stopTimer(true);
        }

        private void startTimer()
        {
            //this.executionTimeDataField.Text = "";
            //this.loadTestExecuteTimer.Start();
        }

        private void stopTimer(bool addMsg)
        {
            //this.loadTestExecuteTimer.Stop();
            //this.addMessage("Timer ran for " + this.executionTimeDataField.Text);
            //this.executionTimeDataField.Text = "HH:MM:SS.mmm";
        }

        private void addMessage(string msg)
        {
            DateTime curTime = DateTime.Now;
            string curTimeStr = curTime.ToShortTimeString();
            string curDateStr = curTime.Date.ToShortDateString();
            try
            {
                this.loadTestMessageListBox.Items.Add(curDateStr + " " + curTimeStr + ": " + msg);
                this.loadTestMessageListBox.Update();
            }
            catch (Exception)
            {
            }
        }

        private void numberUsersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numberIterationsTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void inputBrowseButton_Click(object sender, EventArgs e)
        {

        }

        private void executeFlowComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void progressionRateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Background worker that awakes every second to see if the threads are done
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statusChecker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(!this.loadController.IsFullyComplete())
            {                
                Thread.Sleep(1000);
                this.loadController.finishIteration();
            }
        }

        private void procDataUpdateTimer_Tick(object sender, EventArgs e)
        {
            //Update process data
            this.curProcess.Refresh();
            long curNonPagedMem = curProcess.NonpagedSystemMemorySize64;
            long curPagedMem = curProcess.PagedMemorySize64;
            long curPagedSysMem = curProcess.PagedSystemMemorySize64;
            long curPeakPagedMem = curProcess.PeakPagedMemorySize64;
            long curPeakVirtMem = curProcess.PeakVirtualMemorySize64;
            long curPeakMem = curProcess.PeakWorkingSet64;
            long curPrivMem = curProcess.PrivateMemorySize64;
            TimeSpan curPrivProcTime = curProcess.PrivilegedProcessorTime;
            DateTime startTime = curProcess.StartTime;
            long curNumThreads = curProcess.Threads.Count;
            TimeSpan curProcTime = curProcess.TotalProcessorTime;
            TimeSpan curUsrProcTime = curProcess.UserProcessorTime;
            long curVirtMem = curProcess.VirtualMemorySize64;
            long curPhysMem = curProcess.WorkingSet64;

            nonPagedMemTextBox.Text = "" + curNonPagedMem;
            pagedMemTextBox.Text = "" + curPagedMem;
            pagedSysMemTextBox.Text = "" + curPagedSysMem;
            peakPagedMemTextBox.Text = "" + curPeakPagedMem;
            peakVirtualMemTextBox.Text = "" + curPeakVirtMem;
            peakPhysicalMemTextBox.Text = "" + curPeakMem;
            privateMemTextBox.Text = "" + curPrivMem;
            privCpuTimeTextBox.Text = StringUtilities.TimeSpanToString(curPrivProcTime,
                                                                       StringUtilities.MaxTimeResolution.SECONDS,
                                                                       StringUtilities.TimeFormat.WHOLEFRACTIONUNIT);
            numThreadsTextBox.Text = "" + curNumThreads;
            totalProcTimeTextbox.Text = StringUtilities.TimeSpanToString(curProcTime,
                                                                    StringUtilities.MaxTimeResolution.SECONDS,
                                                                    StringUtilities.TimeFormat.WHOLEFRACTIONUNIT);
            userCpuTimeTextBox.Text = StringUtilities.TimeSpanToString(curUsrProcTime,
                                                                       StringUtilities.MaxTimeResolution.SECONDS,
                                                                       StringUtilities.TimeFormat.WHOLEFRACTIONUNIT);
            virtMemTextBox.Text = "" + curVirtMem;
            physicalMemTextBox.Text = "" + curPhysMem;
        }
    }
}
