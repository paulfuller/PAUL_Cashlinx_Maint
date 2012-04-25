using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using CouchConsoleApp.db;
using CouchConsoleApp.vo;

namespace CouchConsoleApp.thread
{
    class ThreadForDocListTree
    {
        private BackgroundWorker backgroundWorkerForCount = null;
        private DoWorkEventArgs e = null;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ThreadForDocListTree));
        private List<SourceDocTreeVO> resultList = null;
        private List<SourceDocTreeVO> resultTargetList = null;
        private PreviousArchStatVO prevVO = null;
        private bool execSuccess = false;
        private SourceDocByDateDAO docRegDao = null;
       // private int totalRecords = 0;
        string errorMsg = "";

        public ThreadForDocListTree(BackgroundWorker backgroundWorkerForCount, DoWorkEventArgs e)
        {
            this.backgroundWorkerForCount = backgroundWorkerForCount;
            this.e = e;
            
        }   


        public void run()
        {
            log.Debug("Executing tree query ");
            //Thread.Sleep(250);
            int errorCode = 0;
           

            docRegDao = SourceDocByDateDAO.getInstance();
            //execSuccess = docRegDao.docSourceDocsListByDate(ref resultList,ref resultTargetList,out errorMsg);
            execSuccess = docRegDao.getPreviousRunStats(out prevVO);

           /* var dao = new DocumentProcDAO();
            dao.GetDocuments(0, out totalRecords, out errorCode, out errorMsg);
            if (errorCode != 0)
            {
                execSuccess = false;
            }else
            {
                execSuccess = true;
            }*/
            log.Debug("Source Tree call done");
        }

        public bool isExecSuccess()
        {
            return this.execSuccess;
        }

        public string getError()
        {
            return this.errorMsg;
        }

        /*public void abortQuery()
        {
           docRegDao.killCommand(); 
        }*/

        public List<SourceDocTreeVO> getTable()
        {
            return this.resultList;
        }

        public List<SourceDocTreeVO> getTargetTreeTable()
        {
            return this.resultTargetList;
        }

        public PreviousArchStatVO getPreviousArchData()
        {
            return this.prevVO;
        }
        /*public int getTotalRecords()
        {
            return this.totalRecords;
        }*/
    }
}
