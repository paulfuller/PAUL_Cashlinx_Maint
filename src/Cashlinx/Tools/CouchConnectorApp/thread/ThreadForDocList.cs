using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using CouchConsoleApp.db;
using CouchConsoleApp.vo;

namespace CouchConsoleApp.thread
{
    public class ThreadForDocList
    {
        private BackgroundWorker backgroundWorkerForCount = null;
        private DoWorkEventArgs e = null;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ThreadForDocList));
        //private DataTable resultTable = null;
        
        private DocRegListDAO docRegDao = null;
        private int totalRecords = 0;
        string errorMsg = "";
        private bool execSuccess = false;
        private DocumentDAO dao = null;
        private List<PawnDocRegVO> vo1;
        public ThreadForDocList(BackgroundWorker backgroundWorkerForCount, DoWorkEventArgs e)
        {
            this.backgroundWorkerForCount = backgroundWorkerForCount;
            this.e = e;
            //this.resultTable = resultTable;
        }


        /*public void run()
        {
            log.Debug("Executing List query");
            //Thread.Sleep(250);
            int errorCode = 0;
           // docRegDao = DocRegListDAO.getInstance();
           // execSuccess = docRegDao.docListGet(ref resultTable, 100);

            var dao = new DocumentProcDAO();

            log.Info("Stubbed get docs from proc");
            //resultTable=dao.GetDocuments(0, out totalRecords, out errorCode, out errorMsg););

            if (errorCode != 0)
            {
                execSuccess = false;
            }
            else
            {
                execSuccess = true;
            }
            log.Info("SP Count and SP Doc Search Done");
        }*/

        public  void run()
        {

            int pendCount = 0;
            int errorCode = 0;
            string errorMsg = "";
            dao = DocumentDAO.getInstance();
         
            //List<PawnDocRegVO> vo1 = null;

            log.Info("Calling proc for count");
            try
            {
                //dao.GetDocumentSetsForCount(0,  out pendCount, out errorCode, out errorMsg);
                dao.GetDocumentSets(0, out vo1, out pendCount, out errorCode, out errorMsg);
               
                if (errorCode != 0)
                {
                    execSuccess = false;
                }
                else
                {
                    execSuccess = true;
                    this.totalRecords = pendCount;
                }
            }
            catch (Exception e)
            {
                log.Error("Exception in TestDocumentDAOThread" + e.Message);
                return;
            }
         
        }

         public  void testRun()
        {

            int pendCount = 0;
            int errorCode = 0;
            string errorMsg = "";
            dao = DocumentDAO.getInstance();
         
            //List<PawnDocRegVO> vo1 = null;

            log.Info("Calling proc for count");
            try
            {
                //dao.GetDocumentSetsForCount(0,  out pendCount, out errorCode, out errorMsg);
                dao.GetTempGetDocsIDs_ForAdd(ref vo1, 20000, true, 0);
                if (errorCode != 0)
                {
                    execSuccess = false;
                }
                else
                {
                    execSuccess = true;
                    this.totalRecords = 1000000;
                }
            }
            catch (Exception e)
            {
                log.Error("Exception in TestDocumentDAOThread" + e.Message);
                return;
            }
           
        }

       

        public void abortQuery()
        {
            if (docRegDao!=null)
            dao.killCommand(); 
        }

        public List<PawnDocRegVO> getResultData()
        {
            return this.vo1;
        }

        public static DataTable getDataTableFromVO(List<PawnDocRegVO> voList)
        {
            DataTable table = new DataTable();
            table.Columns.Add("DocumentID", typeof(int));
            table.Columns.Add("Storage ID", typeof(string));
            table.Columns.Add("Storage Date", typeof(DateTime));
            try
            {
                foreach(var pawnDocRegVO in voList)
                {
                    table.Rows.Add(pawnDocRegVO.DocID, pawnDocRegVO.StorageID, pawnDocRegVO.CreationDate);
                }
            }catch(Exception e)
            {
                return (table);
            }
            return table;
        }

        public int getTotalRecords()
        {
            return this.totalRecords;
        }

        public bool isExecSuccess()
        {
            return this.execSuccess;
        }

        public string getError()
        {
            return this.errorMsg;
        }

    }
}
