using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using CouchConsoleApp.db;
using CouchConsoleApp.form;
using CouchConsoleApp.thread;
using CouchConsoleApp.vo;

namespace CouchConsoleApp.events
{
    public class GoEventHandler
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(GoEventHandler));

        private Form1 mainForm = null;

        private ListDocsForm listForm = null;
        private DataTable resultTable = null;
        //private List<SourceDocTreeVO> srcListForTree = null;
        //private List<SourceDocTreeVO> srcListForTargetTree = null;
        private PreviousArchStatVO prevVO = null;
        private bool qresult = false;
        private ThreadForDocListTree job1 = null;
        private ThreadForDocList job2 = null;
        private int totalDoc = 0;
        private List<PawnDocRegVO> dataList = null;
        private bool exeSuccess = false;
        private string  errorMsg = "";

        private Thread oThread = null;
        private Thread oThread1 = null;
        public GoEventHandler(Form1 mainForm)
        {
            this.mainForm = mainForm;
        }

        public void BackgroundWorkerForGetDocsProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.log.Debug(e.UserState as string);
        }


        public void BackgroundWorkerForGetDocsDoWork(object sender, DoWorkEventArgs e)
        {
            //prepateDataToDisplay();
           
            BackgroundWorker worker = sender as BackgroundWorker;
            job1 = new ThreadForDocListTree(worker, e);
            oThread = new Thread(new ThreadStart(job1.run));
            oThread.Name = "T1";
            oThread.Start();


            job2 = new ThreadForDocList(worker, e);
            if (Properties.Settings.Default.TempFetch)
            {
                oThread1 = new Thread(new ThreadStart(job2.testRun));
            }
            else
            {
                oThread1 = new Thread(new ThreadStart(job2.run));
            }
            oThread1.Name = "T2";
            oThread1.Start();
            
            while (oThread.IsAlive || oThread1.IsAlive)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                //log.Debug(string.Format("Waiting on threads Thread1:{0} Thread2:{1} ", oThread.IsAlive, oThread1.IsAlive));
            }
            log.Info("Threads completed...");
            prevVO = job1.getPreviousArchData();
            totalDoc = job2.getTotalRecords();
            dataList = job2.getResultData();
            resultTable = CouchConsoleApp.thread.ThreadForDocList.getDataTableFromVO(dataList); ;
            
            if(job1.isExecSuccess()&& job2.isExecSuccess())
            {
                exeSuccess = true;
            }else
            {
                exeSuccess = false;
                if (!job1.isExecSuccess())
                {
                    errorMsg = job1.getError();
                }
                else
                {
                    errorMsg = job2.getError();
                }
            }

        }

        public void prepateDataToDisplay()
        {
            
            //ConnDAO dao = ConnDAO.getInstance();
            //DocRegListDAO docRegDao = DocRegListDAO.getInstance();
            //qresult = docRegDao.docListGet(ref resultTable,100);
            //qresult = docRegDao.docListLast(ref resultTable, 100);

            

        }
        

        private void showDocList()
        {
            listForm = new ListDocsForm(mainForm, resultTable, prevVO, totalDoc, dataList);
            if(exeSuccess)
            {
            listForm.Show();
            this.mainForm.docListShowDisposeCntrls();
            }else
            {
                MessageBox.Show("Error getting data:" + errorMsg);
                this.mainForm.docListShowDisposeCntrls();
                this.mainForm.constrollSet1Enable();
            }
        }

  

        public void BgWork_GetDocs_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.log.Debug("Canceled!");
                //job1.abortQuery();
                oThread.Abort();
                oThread1.Abort();
                //job2.abortQuery();
                mainForm.enableGo();
            }
            else if (!(e.Error == null))
            {
                this.log.Debug("Error: " + e.Error.Message);
                mainForm.enableGo();
            }
            else
            {
                this.log.Debug("Done!");
                mainForm.enableGo();
                showDocList();
                //this.tbProgress.Text = "Done!";
            }
           
        }
    }

}
