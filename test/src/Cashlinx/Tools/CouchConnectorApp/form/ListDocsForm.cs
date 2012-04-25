using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Libraries.Utility;
using CouchConsoleApp.db;
using CouchConsoleApp.events;
using CouchConsoleApp.vo;

namespace CouchConsoleApp.form
{
    public partial class ListDocsForm : Form
    {
        private readonly ListDocsFormEvents eventHnadler;
        private Form1 mainForm;
        //private List<SourceDocTreeVO> srcListTree = null;
        //private List<SourceDocTreeVO> srcListForTargetTree = null;
        private PreviousArchStatVO prevVO = null;
        private DataTable resultTable;
        private BindingSource resultBindingSource;
        private BindingSource resultBindingSource1;
        private List<PawnDocRegVO> dataList = null; 
        private bool dataSet = false;
        private int totalDoc = 0;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ListDocsForm));
        private int startIndex = 0;
        public ListDocsForm(Form1 mainForm, DataTable table, PreviousArchStatVO prevVO, int totalDocs, List<PawnDocRegVO> dataList)
        {
            InitializeComponent();
            resultTable = table;
            //this.srcListTree = srcListTreeList;
            this.mainForm = mainForm;
            this.eventHnadler = new ListDocsFormEvents(mainForm,this);
            //this.srcListForTargetTree = srcListForTargetTree;
            this.prevVO = prevVO;
            FormClosing += this.eventHnadler.ListDocForm_FormClosing;
            if(table!=null)
                setDataTable();
            //setSourceTrees();
            setPrevStat();
            this.totalDoc = totalDocs;
            this.dataList = dataList;
            this.totalRecLbL.Text = totalDoc.ToString();
            this.backgroundWorker1.ProgressChanged += eventHnadler.BackgroundWorkerForGetDocsProgressChanged;
            this.backgroundWorker1.DoWork += eventHnadler.BackgroundWorkerForGetDocsDoWork;
            this.backgroundWorker1.RunWorkerCompleted += eventHnadler.BgWork_GetDocs_Completed;
            this.mainForm.constrollSet1Disable();

        }

        private void setPrevStat()
        {
            resultBindingSource1=new BindingSource();
            this.dataGridView2.DataSource = this.resultBindingSource1;
            this.resultBindingSource1.DataSource = prevVO.DataTableValue;
            this.dataGridView2.Update();

            this.successCountLbl.Text = prevVO.SucessCount;
            this.notFoundCountLbl.Text = prevVO.GetErrorCount;
            this.addFailCountLbl.Text = prevVO.AddErrorCount;
            this.deleteCountLbl.Text = prevVO.DelErrorCount;
        }

        private void ListDocsForm_Load(object sender, EventArgs e)
        {
 
        }

       public void setDataTable()
       {
           resultBindingSource=new BindingSource();
           this.dataGridView1.DataSource = this.resultBindingSource;
           this.resultBindingSource.DataSource = resultTable;
           this.dataGridView1.Update();
       }


        /*private void setSourceTrees()
        {
            if (srcListTree != null || srcListTree.Count != 0)
            {
                foreach (var vo in srcListTree)
                {
                    //log.Debug(string.Format("Date {0} : Count {1}",vo.date,vo.count));
                    TreeNode treeNode = new TreeNode(vo.date.ToShortDateString(), new TreeNode[] { new TreeNode(vo.count.ToString()) });
                    treeView1.Nodes.Add(treeNode);
                }
            }


            if(srcListForTargetTree==null || srcListForTargetTree.Count==0)
                return;

            foreach (var vo in srcListForTargetTree)
            {
                //log.Debug(string.Format("Date {0} : Count {1}",vo.date,vo.count));
                TreeNode treeNode = new TreeNode(vo.date.ToShortDateString(), new TreeNode[] { new TreeNode(vo.count.ToString()) });
                this.treeView2.Nodes.Add(treeNode);
            }
        }*/

       private void moveFirst_Click(object sender, EventArgs e)
       {

       }

       private void button3_Click(object sender, EventArgs e)
       {
           /*var dao = new DocumentProcDAO();
           this.resultBindingSource.DataSource = dao.GetDocuments(9803);
           this.totalRecLbL.Text = resultBindingSource.Count.ToString();
           log.Debug("Total Count :" + resultBindingSource.Count.ToString());
           // this.dataGridView1.
           this.dataGridView1.Update();*/
           
       }

       private void button2_Click(object sender, EventArgs e)
       {
           this.mainForm.constrollSet1Enable();
           this.Dispose();
       }

       private void button1_Click(object sender, EventArgs e)
       {
           this.Dispose();
           //this.Close();
           var jobProcessForm = new ArchProcessForm(mainForm, totalDoc, false, dataList);
           //jobProcessForm.Show();
          // jobProcessForm.ParentForm(this.mainForm);
          
       }

       private void Next_Click(object sender, EventArgs e)
       {
           int errorCode = 0;
           string errorMsg = "";
           var dao = new DocumentProcDAO();
           getLastIndex();
           //resultTable = dao.GetDocuments(startIndex, out totalDoc, out errorCode, out errorMsg);
           List<PawnDocRegVO> docList = null;
           int pendCount = 0;
           this.Enabled = false;
           if (Properties.Settings.Default.TempFetch)
           {
               DocumentDAO.getInstance().GetTempGetDocsIDs_ForAdd(ref docList, 20000, false, startIndex);
           }
           else
           {
               DocumentDAO.getInstance().GetDocumentSets(startIndex, out docList, out pendCount, out errorCode, out errorMsg);
           }

           this.Enabled = true;
           if (errorCode != 0 || totalDoc == 0)
           {
               MessageBox.Show("No Documents found :" + errorMsg);
               return;
           }
           resultTable = CouchConsoleApp.thread.ThreadForDocList.getDataTableFromVO(docList);
           this.resultBindingSource.DataSource = resultTable;
         
           //enable below for continues looping of proc
           //loopThroughProc();
       }

        private void getLastIndex()
        {
            if (this.resultBindingSource != null && this.resultBindingSource.Count > 0)
            {
                this.resultBindingSource.MoveLast();
                DataRowView view = (DataRowView)this.resultBindingSource.Current;
                this.startIndex = Utilities.GetIntegerValue(view.Row.ItemArray[0]);
            }
        }

        private void loopThroughProc()
        {
            int errorCode = 0;
            string errorMsg = "";
            var dao = new DocumentProcDAO();
            //startIndex = 0;

            int noOfFetches = 0;
            int totRec = 1000;
            while (true)
            {
                noOfFetches++;
                this.log.Debug("Doing fetch :" + noOfFetches + "  :  " + totRec);

                this.resultTable = dao.GetDocuments(this.startIndex, out this.totalDoc, out errorCode, out errorMsg);


                if (errorCode != 0 || this.totalDoc == 0)
                {
                    MessageBox.Show("No Documents found :" + errorMsg);
                    return;
                }
                this.resultBindingSource.DataSource = this.resultTable;
                totRec += this.resultBindingSource.Count;
                this.resultBindingSource.MoveLast();
                DataRowView view = (DataRowView)this.resultBindingSource.Current;
                this.startIndex = Utilities.GetIntegerValue(view.Row.ItemArray[0]);

            }
        }

        private void button4_Click(object sender, EventArgs e)
       {
           int pendCount = 0;
           int errorCode = 0;
           string errorMsg = "";
           //var dao = new DocumentProcDAO();
           startIndex = 0;
           //resultTable = dao.GetDocuments(startIndex, out totalDoc, out errorCode, out errorMsg);
            List<PawnDocRegVO> docList = null;
            this.Enabled = false;
           if (Properties.Settings.Default.TempFetch)
           {
               DocumentDAO.getInstance().GetTempGetDocsIDs_ForAdd(ref docList, 20000, true, 0);
           }else
           {
               DocumentDAO.getInstance().GetDocumentSets(0, out docList, out pendCount, out errorCode, out errorMsg);
           }
           this.Enabled = true;
           if (errorCode != 0 || docList == null || docList.Count==0)
           {
               MessageBox.Show("No Documents found :" + errorMsg);
               return;
           }
           this.resultBindingSource.DataSource = CouchConsoleApp.thread.ThreadForDocList.getDataTableFromVO(docList);
           resultBindingSource.MoveLast();
           DataRowView view = (DataRowView)resultBindingSource.Current;
           startIndex = Utilities.GetIntegerValue(view.Row.ItemArray[0]);
              
       }

        private void RetryButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
            int totCount = Convert.ToInt32(prevVO.GetErrorCount) + Convert.ToInt32(prevVO.AddErrorCount);
            var jobProcessForm = new ArchProcessForm(this.mainForm, totCount, true, dataList);
        }
       
       
    }
}
