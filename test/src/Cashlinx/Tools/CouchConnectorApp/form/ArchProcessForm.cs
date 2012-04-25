using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CouchConsoleApp.events;
using CouchConsoleApp.vo;
using log4net.Config;
//using MainForm.event1;
//using MainForm.vo;

namespace CouchConsoleApp.form
{
    public partial class ArchProcessForm : Form
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ArchProcessForm));
        private Form1 mainForm = null;
        private List<PawnDocRegVO> dataList = null;
        public ArchProcessForm()
        {
            /*InitializeComponent();
            XmlConfigurator.Configure();
            this.Show();
            var sH = new BgWorkerJobAllocationEventHandler(this);
            this.backgroundWorker1.ProgressChanged += sH.BackgroundWorkerForGetDocsProgressChanged;
            this.backgroundWorker1.DoWork += sH.BackgroundWorkerForGetDocsDoWork;
            this.backgroundWorker1.RunWorkerCompleted += sH.BgWorkGetDocsCompleted;
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            initProgBar();*/
        }

        public List<PawnDocRegVO> getData()
        {
            return dataList;
        }

        public ArchProcessForm(Form1 mainForm, int totalRecords, bool isRecovery, List<PawnDocRegVO> dataList)
        {
            this.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
            this.mainForm = mainForm;
            //this.CenterToParent();
            this.dataList = dataList;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.mainForm.Enabled = false;
            InitializeComponent();
            XmlConfigurator.Configure();
            this.ControlBox = false;
            this.Show();
            var sH = new BgWorkerJobAllocationEventHandler(this, totalRecords);
            this.backgroundWorker1.ProgressChanged += sH.BackgroundWorkerForGetDocsProgressChanged;
            this.backgroundWorker1.DoWork += sH.BackgroundWorkerForGetDocsDoWork;
            this.backgroundWorker1.RunWorkerCompleted += sH.BgWorkGetDocsCompleted;
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            initProgBar();
            if (isRecovery)
                this.backgroundWorker1.RunWorkerAsync(1);
            else
            {
                this.backgroundWorker1.RunWorkerAsync(0);
            }
            this.toolStripProgressBar1.Text = "In Progress...";
            this.toolStripProgressBar1.Visible = true;
            //this.startButton.Enabled = false;
            this.exitButton.Enabled = false;
            
        }

        

        public Form1 getMainForm()
        {
            return this.mainForm;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.RunWorkerAsync();
            this.toolStripProgressBar1.Text = "In Progress...";
            this.toolStripProgressBar1.Visible = true;
            this.startButton.Enabled = false;
            this.exitButton.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            disposeAction();
        }

        public void disposeAction()
        {
            this.mainForm.Enabled = true;
            this.mainForm.enableGo();
            this.mainForm.constrollSet1Enable();
            this.Dispose();
        }

        private void initProgBar()
        {
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            toolStripProgressBar1.MarqueeAnimationSpeed = 1;
            Application.EnableVisualStyles();
            this.toolStripProgressBar1.Visible = false;
            this.toolStripProgressBar1.Text = "Ready";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync();
            this.toolStripProgressBar1.Visible = false;
            this.statusLabelForProg.Text = "Waiting for threads to complete...";
           // this.Enabled = false;
        }

        public void setProgressState(bool visible)
        {
            this.toolStripProgressBar1.Visible = visible;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int comp = 13;
            int tot = 3000;
            
            int num = (comp/tot)*100;
            log.Info(string.Format("({0}/{1})*100={2}",comp,tot, num));

            double db1 = 50;
            double db2 = 3000;

            double res=(db1/db2)*100;

            log.Info(string.Format("({0}/{1})*100={2}", db1, db2, Math.Round(res)));
        }

        public void setUpdates(string total,string completed,string pending)
        {
            this.totalCntLbl.Text = total;
            this.CompletedLbl.Text = completed;
            this.PendingLbl.Text = pending;
        }

        public void enableBack()
        {
            this.exitButton.Enabled = true;
            this.startButton.Enabled = true;

        }

        public void setStatusLabel(string str)
        {
            this.statusLabelForProg.Text = str;
            //this.stopButton.Enabled = false;
        }

        private void ArchProcessForm_Load(object sender, EventArgs e)
        {
           
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.mainForm.Enabled = true;
            this.mainForm.enableGo();
        }
        private delegate void SetTextCallback(string text);
        public void appendTime(string text)
        {

            if (this.timeElapLbl != null)
            {
                if (this.timeElapLbl.InvokeRequired)
                {
                    SetTextCallback handler = appendTime;
                    Invoke(handler, new object[]
                    {
                            text
                    });
                }
                else
                {
                    this.timeElapLbl.Text = text;
                }
            }
        }

        private void ArchProcessForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           // log.Info("Form closed.....");
        }
    }
}
