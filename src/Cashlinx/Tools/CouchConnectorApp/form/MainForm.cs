using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Timers;
using System.Windows.Forms;
using Common.Libraries.Objects.Doc;
using Common.Libraries.Utility.String;
using CouchConsoleApp.couch;
using CouchConsoleApp.db;
using CouchConsoleApp.events;
using CouchConsoleApp.Properties;
using CouchConsoleApp.test;
using CouchConsoleApp.thread;
using CouchConsoleApp.vo;
using form.CouchConsoleApp;
using log4net;


namespace CouchConsoleApp.form
{
    public partial class Form1 : Form
    {
        private static readonly Form1 mainForm = new Form1();
        private readonly ILog log = LogManager.GetLogger(typeof(Form1));
        private ArchiveVO archvo;
        private BackgroundWorker backgroundWorkerForGetDocs;


        private CouchUtil cUtil;
        public CouchVo couchVO;
        private int requestedCount;
        private CouchVo targetCouchVO;

        private Form1()
        {
           //SetAutoSizeMode(
        }

        public void init(CouchVo vo)
        {
            InitializeComponent();
            this.couchVO = vo;
            this.tabControl.SelectedIndex = 2;
            tabControl.TabPages.Remove(testTab1);
            if(!Properties.Settings.Default.TestMode)
            {
                tabControl.TabPages.Remove(testTab2);
            }
            this.srcCouchDBName.Text = this.couchVO.dbName;
            this.serverNameLabel.Text = this.couchVO.serverName;
            this.dbNameLabel.Text = this.couchVO.dbName;
            this.srcCouchLBL.Text = this.couchVO.serverName;
            this.archvo = new ArchiveVO();
            this.archvo.souceSet = true;
            initBackWorkers();
            registerTempDocPopBgWorker();
            initProgBar();
            //toolStripProgressBar1.LocationChanged()
            //log.Error("Test Log from Form for error");
            log.Info("Memory usage :" +
                    System.Diagnostics.Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024 + "MB");
            log.Info("Archival Form Started");
            printMemoryUsage(null, null);
        }

        private void printMemoryUsage(object source, ElapsedEventArgs e)
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
            log.Info("####################################################################################");
            log.Info("NonpagedSystemMemorySize64  = " + curProcess.NonpagedSystemMemorySize64 / 1024 / 1024 + " MB");
            log.Info("PagedMemorySize64  = " + curProcess.PagedMemorySize64 / 1024 / 1024 + " MB");
            log.Info("PagedSystemMemorySize64  = " + curProcess.PagedSystemMemorySize64 / 1024 / 1024 + " MB");
            log.Info("PeakPagedMemorySize64  = " + curProcess.PeakPagedMemorySize64 / 1024 / 1024 + " MB");
            log.Info("PeakVirtualMemorySize64 [peak Virtual memory used]  = " + curProcess.PeakVirtualMemorySize64 / 1024 / 1024 + " MB");
            log.Info("curProcess.PeakWorkingSet64[peak physical memory used]  = " + curProcess.PeakWorkingSet64 / 1024 / 1024 + " MB");
            log.Info("Private Memory  = " + curProcess.PrivateMemorySize64 / 1024 / 1024 + " MB");
            log.Info("CPU Time  = " +StringUtilities.TimeSpanToString(curPrivProcTime,
                                                                       StringUtilities.MaxTimeResolution.SECONDS,
                                                                       StringUtilities.TimeFormat.WHOLEFRACTIONUNIT));
            log.Info("Current no of threads :"+ curNumThreads);
            log.Info("Total Process Time  = " +StringUtilities.TimeSpanToString(curProcTime,
                                                                    StringUtilities.MaxTimeResolution.SECONDS,
                                                                    StringUtilities.TimeFormat.WHOLEFRACTIONUNIT));
            log.Info("User CPU Time  = " +StringUtilities.TimeSpanToString(curUsrProcTime,
                                                                       StringUtilities.MaxTimeResolution.SECONDS,
                                                                       StringUtilities.TimeFormat.WHOLEFRACTIONUNIT));
            log.Info("Virtual Memory [Page file usage]" + curVirtMem / 1024 / 1024 + " MB");
            log.Info("Physical memory [RAM usage] " + curPhysMem / 1024 / 1024 + " MB");
            log.Info("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            printSystemMemoryStat();
        }

        private void printSystemMemoryStat()
        {
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            //new Computer
            foreach (ManagementObject result in results)
            {

                log.Info(string.Format("Free Physical Memory {0} MB: Free Virtual Memory {1} MB :" +
                                                 " Used Physical Memory {2} MB",
                                                 long.Parse((result["FreePhysicalMemory"]).ToString()) / 1024,
                                                 long.Parse((result["TotalVirtualMemorySize"]).ToString()) / 1024,
                                                 (long.Parse((result["TotalVisibleMemorySize"]).ToString()) / 1024 - long.Parse((result["FreePhysicalMemory"]).ToString()) / 1024)));
            }
        }

        public void initBackWorkers()
        {
            this.backgroundWorkerForGetDocs = new BackgroundWorker();
            this.backgroundWorkerForGetDocs.WorkerReportsProgress = true;
            this.backgroundWorkerForGetDocs.WorkerSupportsCancellation = true;
            var sH=new GoEventHandler(this);
            this.backgroundWorkerForGetDocs.ProgressChanged += sH.BackgroundWorkerForGetDocsProgressChanged;
            this.backgroundWorkerForGetDocs.DoWork += sH.BackgroundWorkerForGetDocsDoWork;
            this.backgroundWorkerForGetDocs.RunWorkerCompleted += sH.BgWork_GetDocs_Completed;

            this.bgWorkerForJobAllocation=new BackgroundWorker();
            this.backgroundWorkerForGetDocs.WorkerReportsProgress = true;
            this.backgroundWorkerForGetDocs.WorkerSupportsCancellation = true;
        }

        private void registerTempDocPopBgWorker()
        {
            this.bgWorkerForDocPop=new BackgroundWorker();
            this.bgWorkerForDocPop.WorkerReportsProgress = true;
            this.bgWorkerForDocPop.WorkerSupportsCancellation = true;
            var docPopEv = new AddTempDocEventHandler(this);
            this.bgWorkerForDocPop.ProgressChanged += docPopEv.BackgroundWorkerForGetDocsProgressChanged;
            this.bgWorkerForDocPop.DoWork += docPopEv.BackgroundWorkerForGetDocsDoWork;
            this.bgWorkerForDocPop.RunWorkerCompleted += docPopEv.BgWork_GetDocs_Completed;
        }

        private void initProgBar()
        {
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            toolStripProgressBar1.MarqueeAnimationSpeed = 1;
            Application.EnableVisualStyles();
            this.toolStripProgressBar1.Visible = false;
            this.toolStripStatusLabel2.Text = "Ready";
        }

        public static Form1 Instance()
        {
            return mainForm;
        }

    //Thread safe append console
        public void appendConsole(string text)
        {
    
            if (this.resultText != null)
            {
                if (this.resultText.InvokeRequired)
                {
                    SetTextCallback handler = appendConsole;
                    Invoke(handler, new object[]
                    {
                            text
                    });
                }
                else
                {
                    this.resultText.AppendText("\n");
                    this.resultText.AppendText(text);
                    //this.resultText.Focus();
                    this.resultText.WordWrap = false;
                    this.resultText.SelectionStart = this.resultText.Text.Length;
                    this.resultText.ScrollToCaret();
                }
            }
        }

        public void setTargetDB(DBConnector dbconn)
        {
            //archvo.dbConnector = dbconn;
            if (dbconn.getStatus() == DBConnector.Status.INITIALIZED)
            {
                this.archvo.dbSet = true;
                this.targetDBServerLbl.Text = dbconn.serverName();
                this.targetOrclDBName.Text = dbconn.serverSID();
                this.targetDBPIC.Image = Resources.tick;
            }else
            {
                this.archvo.dbSet = false;
                this.targetDBServerLbl.Text = "";
                this.targetOrclDBName.Text = "";
                this.targetDBPIC.Image = Resources.cross;
            }

            enableGO();
        }

        public void setTargetCouchVO(CouchVo targetCouchVO)
        {
            if (!(String.IsNullOrEmpty(targetCouchVO.serverName)))
            {
                this.targetCouchVO = targetCouchVO;
                this.targetCouchLBL.Text = targetCouchVO.serverName;
                this.targetCouchDBName.Text = targetCouchVO.dbName;
                this.targetCouchPic.Image = Resources.tick;
                this.archvo.targetSet = true;
                
            }else
            {
                this.targetCouchVO = new CouchVo();
                this.targetCouchLBL.Text = "";
                this.targetCouchDBName.Text = "";
                this.targetCouchPic.Image = Resources.cross;
                this.archvo.targetSet = true;
            }
            enableGO();
        }

        public CouchVo getTargetCouch()
        {
            return targetCouchVO;
        }

        public CouchVo getSourceCouch()
        {
            return couchVO;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.resultText.Clear();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //this.resultText.AppendText("\n");
            //this.resultText.AppendText("Getting document..." + DateTime.Now);
            if (string.IsNullOrEmpty(this.docIDText.Text))
            {
                MessageBox.Show("Please enter document ID to search.");
                return;
            }
            Enabled = false;
            this.cUtil = new CouchUtil(this.couchVO);
            this.couchVO.documentID = this.docIDText.Text;
            this.cUtil.GetDocument(this.couchVO, this.resultText);
            Enabled = true;
            //this.resultText.AppendText("Completed..." + DateTime.Now);
        }

        private void AppExitClick(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show("Closing window....");
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            var fDialog = new OpenFileDialog();
            fDialog.Title = "Open PDF/PS File";
            fDialog.Filter = "PDF Files|*.pdf|Post scr Files|*.ps";
            fDialog.InitialDirectory = @"C:\";

            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                this.fileNameText.Text = fDialog.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.fileNameText.Text))
            {
                MessageBox.Show("Please select a valid file");
                return;
            }
            if (!File.Exists(this.fileNameText.Text))
            {
                MessageBox.Show("Entered file does not exist");
                return;
            }

            this.couchVO.fileWithPath = this.fileNameText.Text;
            var couchUtil = new CouchUtil(this.couchVO);
            this.resultText.AppendText("\n");
            this.resultText.AppendText("Adding document..." + DateTime.Now);
            Enabled = false;
            var sw = new Stopwatch();

            sw.Start();

            couchUtil.storeDocument(this.couchVO, this.resultText);

            sw.Stop();
            Enabled = true;
            this.resultText.AppendText("\n");
            this.resultText.AppendText("Completed At..." + DateTime.Now);
            this.resultText.AppendText("\n");
            this.resultText.AppendText("Time Taken :" + sw.ElapsedMilliseconds + " Msec's");
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var conn = new CouchConnector(this);
            conn.FormBorderStyle = FormBorderStyle.FixedDialog;
            //conn.MaximizeBox = false;
            //conn.MinimizeBox = true;
            conn.StartPosition = FormStartPosition.CenterScreen;
            conn.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var dbForm = new DBParamsForm(this);
            //Console.Write(this.GetType());
            // dbForm.Parent = this;
            dbForm.Show();
        }

        private void enableGO()
        {
            if (archvo.dbSet && archvo.targetSet)
            {
                this.goButton.Enabled = true;
            }
            else
            {
                this.goButton.Enabled = false;
            }
        }

        private void Gobutton_Click(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "In Progress...";
            this.toolStripProgressBar1.Visible = true;
            this.backgroundWorkerForGetDocs.RunWorkerAsync();
            this.goButton.Visible = false;
            this.stopButton.Visible = true;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.backgroundWorkerForGetDocs.CancelAsync();

          // DocRegListDAO.getInstance().killCommand();
            this.toolStripProgressBar1.Visible = false;
            this.toolStripStatusLabel2.Text = "Ready";
        }

        public void enableGo()
        {
            this.goButton.Visible = true;
            this.stopButton.Visible = false;
        }

        public void constrollSet1Disable()
        {
            this.dbConnectButton.Enabled = false;
            this.couchConnButton.Enabled = false;
            this.exitButton.Enabled = false;
            this.tabControl.Enabled = false;
            this.goButton.Enabled = false;
        }

        public void constrollSet1Enable()
        {
            this.dbConnectButton.Enabled = true;
            this.couchConnButton.Enabled = true;
            this.exitButton.Enabled = true;
            this.tabControl.Enabled = true;
            this.goButton.Enabled = true;
        }

        public void docListShowBeginCntrls()
        {
            this.goButton.Visible = false;
            this.stopButton.Visible = false;
            this.toolStripProgressBar1.Visible = false;
            this.toolStripStatusLabel2.Text = "Ready";
        }

        public void docListShowDisposeCntrls()
        {
            this.goButton.Visible = true;
            this.stopButton.Visible = false;
            this.toolStripProgressBar1.Visible = false;
            this.toolStripStatusLabel2.Text = "Ready";
        }

        #region Nested type: SetTextCallback
        private delegate void SetTextCallback(string text);
        #endregion  

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void archiveTab_Click(object sender, EventArgs e)
        {

        }

        private void fileNameText_TextChanged(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //CouchVo vo = getTargetCouch();
            CouchVo vo = new CouchVo();
            vo.userName = "mydbuser";
            vo.pwd = "mydbuser";
            vo.serverName = "localhost";
            vo.serverport = "5984";
            vo.dbName = "mydb";
            vo.adminUserName = "admin";
            vo.adminPwd = "adminadmin";
           

            CouchUtil util=new CouchUtil();
            string retMsg = null;
            bool retBool = false;
            for(int i = 0; i < 1; i++)
            {
                util.createDBInCouch("clx_jan_2011"+i, out retMsg, out retBool, vo);
                appendConsole("%%%%%%%%%%%%%%%%%%%%%%%");
                appendConsole("Ret MSG" + retMsg);
                appendConsole("Ret retBool" + retBool);
                appendConsole("%%%%%%%%%%%%%%%%%%%%%%%");    
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CouchVo vo = new CouchVo();
            vo.userName = "mydbuser";
            vo.pwd = "mydbuser";
            vo.serverName = "localhost";
            vo.serverport = "5984";
            vo.dbName = "mydb";
            vo.adminUserName = "admin";
            vo.adminPwd = "adminadmin";


            CouchUtil util = new CouchUtil();
            string retMsg = null;
            bool retBool = false;
            for (int i = 0; i < 1; i++)
            {
                util.deleteDBInCouch("clx_jan_2011" + i, out retMsg, out retBool, vo);
                appendConsole("_____________________________");
                appendConsole("Ret MSG" + retMsg);
                appendConsole("Ret retBool" + retBool);
                appendConsole("_____________________________");
            }
        }
        
        

        private void button9_Click(object sender, EventArgs e)
        {
            List<vo.PawnDocRegVO> list = null;
            DocumentDAO.getInstance().GetDocumentSets(ref list, 10000, true, 0);
            int lastIndex = 0;
            for(int i = 0; i < 100; i++)
            {
                list = GetDocumentToArch();
                if(list==null)
                    break;
                loopAndPrint(list);
            }
           

            foreach(var i in dict)
            {
                log.Debug(string.Format("key {0}, value {1}",i.Key,i.Value));
            }
            
        }

        private int archDocIndex = 0;
        Dictionary<string, int> dict = new Dictionary<string, int>();
        private int fetchCount = 1000;

        private List<vo.PawnDocRegVO> GetDocumentToArch()
        {
            List<vo.PawnDocRegVO> list = null;

            if (this.archDocIndex == 0)
            {
                DocumentDAO.getInstance().GetDocumentSets(ref list, fetchCount, true, 0);
                //list = DataClass.Instance().getFirstSet();
                if (list != null && list.Count > 0)
                {
                    archDocIndex = (list[list.Count - 1]).DocID;
                }
                return list;
            }
            else
            {
                DocumentDAO.getInstance().GetDocumentSets(ref list, fetchCount, false, archDocIndex);
                if (list != null && list.Count > 0)
                {
                    archDocIndex = (list[list.Count - 1]).DocID;
                }
            }
            return list;
        }


        public void loopAndPrint(List<vo.PawnDocRegVO> list)
        {
            int month = 0;
            int year = 0;
            string prefix = "CLX_";
            string name = "";
            if (list == null)
            {
                MessageBox.Show("No Records");
                return;
            }

            foreach (var pawnDocRegVO in list)
            {
                // log.Debug(string.Format("ID:{0} StorageID {1} Date{2}",
                //    pawnDocRegVO.Id, pawnDocRegVO.StorageID,pawnDocRegVO.Date));

                DateTime dt = pawnDocRegVO.CreationDate;
                month = dt.Month;
                year = dt.Year;
                name = prefix + month + "_" + year;
                if (dict.ContainsKey(name))
                {
                    int val = dict[name];
                    dict[name] = val + 1;
                }
                else
                {
                    dict.Add(name, 1);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            /*string str = null;
            ConnDAO.getInstance().getUniqueID(ref str);
            log.Debug(ConnDAO.getInstance().getUniqueID(ref str));*/
            List<PwnDocArchDBRepVO> list=new List<PwnDocArchDBRepVO>();
            for(int i = 0; i < 100; i++)
            {
                var vo = new PwnDocArchDBRepVO();
                vo.DBName = "sdddsd"+i;
                list.Add(vo);
            }
           
            Stopwatch wat=new Stopwatch();
            wat.Start();
            //CouchDAO.getInstance().AddCouchDbToRepos(list);
            wat.Stop();
            log.Info("Total time :"+wat.Elapsed);
        }

        private void button11_Click(object sender, EventArgs e)
        {

           ArchJobErrorDesc edesc= ArchJobErrorDesc.getInstance();
           edesc.getErrorCode("");
             CouchUtil util=new CouchUtil();

              CouchVo vo = new CouchVo();
            vo.userName = "mydbuser";
            vo.pwd = "mydbuser";
            vo.serverName = "localhost";
            vo.serverport = "5984";
            vo.dbName = "mydb";
            vo.adminUserName = "admin";
            vo.adminPwd = "adminadmin";
            PawnDocRegVO docRegVO=new PawnDocRegVO();
            docRegVO.DocID = 1234;
            docRegVO.StorageID = "05f4b578-d22b-4d1e-8e24-6fa0ee581b38";
            docRegVO.TargetCouchDBID = 4444;
            string msg = null;
            bool retFlag = false;
            Document doc=util.GetDocument(out msg, out retFlag, vo, docRegVO);
            log.Debug("Got Doc "+doc);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string str = null;
            //ConnDAO.getInstance().testUpdates();
        }

        private void addDocumentClick(object sender, EventArgs e)
        {
            CouchVo vo = new CouchVo();
            vo.userName = "clxuser1";
            vo.pwd = "pa55w0rd1";
            vo.serverName = Properties.Settings.Default.CouchServerName;
            vo.serverport = Properties.Settings.Default.CouchPort;
            vo.dbName = "clx_cust_docs_dev";
            vo.adminUserName = "admin";
            vo.adminPwd = "adminadmin";

            TestPopulateData test=new TestPopulateData();
            test.populateData(vo,false, this.docid123.Text);
           // this.couchVO.fileWithPath = "C:\\dev\\dev_jak2\\devwork_jkingsley\\Phase2App\\CouchConnectorApp\\test\\sample.pdf";
           // var couchUtil = new CouchUtil(this.couchVO);
            //couchUtil.storeDocument(this.couchVO, this.resultText);
        }


        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            button11_Click(sender, e);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {

        }
       private void button8_Click_1(object sender, EventArgs e)
       {
           
       }

       private void button14_Click(object sender, EventArgs e)
       {
           ArchJobErrorDesc edesc = ArchJobErrorDesc.getInstance();
           edesc.getErrorCode("");
           CouchUtil util = new CouchUtil();


           CouchVo vo = new CouchVo();
           vo.userName = "clxuser1";
           vo.pwd = "pa55w0rd1";
           vo.serverName = "couchdb-dev";
           vo.serverport = "5985";
           vo.dbName = "clx_cust_docs_dev";
           vo.adminUserName = "admin";
           vo.adminPwd = "adminadmin";

           PawnDocRegVO docRegVO = new PawnDocRegVO();
           docRegVO.DocID = 1234;
           docRegVO.StorageID = "30fcdd50-b81a-4dfa-b837-9da56e4c9e3f";
           docRegVO.TargetCouchDBID = 4444;
           docRegVO.TargetCouchDBName = "clx_7_2010";
           string msg = null;
           bool retFlag = false;
           Document doc = util.GetDocument(out msg, out retFlag, vo, docRegVO);
           log.Debug("Got Doc " + doc);
           
           
           vo = new CouchVo();
           vo.userName = "mydbuser";
           vo.pwd = "mydbuser";
           vo.serverName = "localhost";
           vo.serverport = "5984";
           vo.dbName = docRegVO.TargetCouchDBName;
           vo.adminUserName = "admin";
           vo.adminPwd = "adminadmin";
           string msg1 = "";
           bool ret = false;
           util.addDocumentToCouch(vo,doc,out msg1,out ret);
       }

       private void button15_Click(object sender, EventArgs e)
       {
          // DocumentProcDAO dao = DocumentProcDAO.getInstance();
          // dao.GetDocuments();
       }

       private void reverseArchAction(object sender, EventArgs e)
       {

       }

       private void addTempDocsAction(object sender, EventArgs e)
       {
           this.bgWorkerForDocPop.RunWorkerAsync();

       }

      public string getDocID()
      {
          return this.docid123.Text;
      }
       private void createDBClick(object sender, EventArgs e)
       {
           string dbName = this.dbNameText.Text;
           CouchVo vo = new CouchVo();
           vo.userName = "clxuser1";
           vo.pwd = "pa55w0rd1";
           vo.serverName = Properties.Settings.Default.CouchServerName;
           vo.serverport = Properties.Settings.Default.CouchPort;
           vo.dbName = "clx_cust_docs_dev";
           vo.adminUserName = "admin";
           vo.adminPwd = "adminadmin";
           CouchUtil util=new CouchUtil();
            string msg = null;
           bool retFlag = false;

           util.createDBInCouch(dbName,out msg,out retFlag,vo);
           MessageBox.Show("DB Created " + retFlag + " Message " + msg);
           //util.
       }

       private void button6_Click_1(object sender, EventArgs e)
       {
           CouchVo vo = new CouchVo();
           vo.userName = "clxuser1";
           vo.pwd = "pa55w0rd1";
           vo.serverName = Properties.Settings.Default.CouchServerName;
           vo.serverport = Properties.Settings.Default.CouchPort;
           vo.dbName = "clx_cust_docs_dev";
           vo.adminUserName = "admin";
           vo.adminPwd = "adminadmin";

           TestPopulateData test = new TestPopulateData();
           test.populateData(vo,true, this.docid123.Text);
       }

       private void button1_Click_1(object sender, EventArgs e)
       {
           string dbName = this.dbNameText.Text;
           CouchVo vo = new CouchVo();
           vo.userName = "clxuser1";
           vo.pwd = "pa55w0rd1";
           vo.serverName = Properties.Settings.Default.CouchServerName;
           vo.serverport = Properties.Settings.Default.CouchPort;
           vo.dbName = "clx_cust_docs_dev";
           vo.adminUserName = "admin";
           vo.adminPwd = "adminadmin";
           CouchUtil util = new CouchUtil();
           string msg = null;
           bool retFlag = false;
           List<string> dbNames=new List<string>();
           dbNames.Add("clx_10_2010");
           dbNames.Add("clx_10_2011");
           dbNames.Add("clx_11_2010");
           dbNames.Add("clx_11_2011");
           dbNames.Add("clx_12_2010");
           dbNames.Add("clx_1_2011");
           dbNames.Add("clx_2_2011");
           dbNames.Add("clx_3_2011");
           dbNames.Add("clx_4_2008");
           dbNames.Add("clx_4_2011");
           dbNames.Add("clx_5_2010");
           dbNames.Add("clx_5_2011");
           dbNames.Add("clx_6_2011");
           dbNames.Add("clx_7_2010");
           dbNames.Add("clx_7_2011");
           dbNames.Add("clx_8_2010");
           dbNames.Add("clx_8_2011");
           dbNames.Add("clx_8_2010");
           dbNames.Add("clx_9_2010");
           dbNames.Add("clx_9_2011");
           foreach(var name in dbNames)
           {
               util.deleteDBInCouch(name, out msg, out retFlag, vo);
             
           }
           MessageBox.Show("DB deleted ");
           
       }

       private void countProc_Click(object sender, EventArgs e)
       {
           List<PawnDocRegVO> vo1 = new List<PawnDocRegVO>();
           DocumentDAO dao = DocumentDAO.getInstance();
           int pendCount = 0;
           int errorCode = 0;
           string errorMsg = "";
           dao.CountProcTest(0, out vo1, out pendCount, out errorCode, out errorMsg);
           dao.GetDocumentSets(0, out vo1, out pendCount, out errorCode, out errorMsg);
       }
    }
}