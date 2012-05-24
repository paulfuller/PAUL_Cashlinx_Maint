using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Common.Libraries.Objects.Doc;
using Common.Libraries.Utility;
using CouchConsoleApp.db;
using CouchConsoleApp.vo;
using log4net;
using Oracle.DataAccess.Client;


namespace CouchConsoleApp.test
{
    internal class TestPopulateData
    {
        public static Document.DocTypeNames DocumentType
        {
            set;
            get;
        }
        int lastitem = 0;
        private static string GldocName = "";
        public static string hostname = "localhost";
        public static string portName = "5984";
        public static string secWebPort = "6984";
        public static string dbName = "perfdb";
        public static bool isSec = false;
        private static string userid = "mydbuser";
        private static string pass = "mydbuser";

        private static string adminUserid = "mydbuser";
        private static string adminPass = "mydbuser";
        private readonly ILog log = LogManager.GetLogger(typeof(TestPopulateData));

        private OracleConnection connection = null;
        private OracleCommand oracleCommand = null;
        private OracleDataReader reader = null;
        private byte[] fileBytes = null;

        private int noOfThreads = 500;

        private int getAllowedCount(ref List<Thread> runningList)
        {
            List<Thread> clearList = new List<Thread>();
            foreach(var thread in runningList)
            {
                if (!thread.IsAlive)
                {
                    clearList.Add(thread);
                }
            }

            foreach(var thread in clearList)
            {
                runningList.Remove(thread);
            }

            int allowedNumber = 0;

            if (runningList.Count < noOfThreads)
            {
                allowedNumber = noOfThreads - runningList.Count;
                if (allowedNumber < 0)
                {
                    return 0;
                }
                else
                {
                    return allowedNumber;
                }
            }
            else
            {
                return allowedNumber;
            }
        }

        /*public void populateDataOld(CouchVo vo, bool isDelete)
        {
            setCouchValues(vo);
            string fileWithPath = "C:\\dev\\dev_jak2\\devwork_jkingsley\\Phase2App\\CouchConnectorApp\\test\\sample.pdf";
            bool added = false;
            List<string> docIds = null;
            List<string> totList = new List<string>();

            GetDocumentSets(ref docIds, 0, 1000);
            //totList.AddRange(docIds);
            AddTestDocThread th = new AddTestDocThread(docIds, vo, this.d);
            if (isDelete)
            {
                th.processDelete();
            }
            else
            {
                th.process();
            }
        }*/


        private List<string> loopAndGetDocs2()
        {
            List<string> millionList = new List<string>();
            DocumentDAO dao = DocumentDAO.getInstance();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            int pendCount = 0;
            int errorCode = 0;
            string errorMsg = "";
            int lastitem = 0;
            int initCount = 0;
            List<PawnDocRegVO> vo1 = new List<PawnDocRegVO>();
           // List<PawnDocRegVO> millionList = new List<PawnDocRegVO>();
            vo1 = new List<PawnDocRegVO>();
            dao.GetTempGetDocsIDs_ForAdd(ref vo1, 20000, true, 0);
            lastitem = vo1[vo1.Count - 1].DocID;
            foreach (var pawnDocRegVO in vo1)
            {
                millionList.Add(pawnDocRegVO.StorageID);
            }
            while (true)
            {
                vo1 = new List<PawnDocRegVO>();
                dao.GetTempGetDocsIDs_ForAdd(ref vo1, 20000, false, lastitem);
                foreach (var pawnDocRegVO in vo1)
                {
                    millionList.Add(pawnDocRegVO.StorageID);
                    if (millionList.Count >= 1000000)
                    {
                        log.Info("Got million 1");
                        return millionList;
                    }
                }
                if (millionList.Count >= 1000000)
                {
                    log.Info("Got million 1-1");
                    return millionList;
                }
                lastitem = vo1[vo1.Count - 1].DocID;
                log.Info("Count so far.............." + millionList.Count);
            }
        }

      /*  private List<string> loopAndGetDocs()
        {
            List<string> totList = new List<string>();
            DocumentDAO dao = DocumentDAO.getInstance();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            int pendCount = 0;
            int errorCode = 0;
            string errorMsg = "";
            int lastitem = 0;
            int initCount = 0;
            List<PawnDocRegVO> vo1 = new List<PawnDocRegVO>();
            while(true)
            {
                vo1 = new List<PawnDocRegVO>();
                dao.GetDocumentSets(lastitem, out vo1, out pendCount, out errorCode, out errorMsg);
               
                foreach (var pawnDocRegVO in vo1)
                {
                   totList.Add(pawnDocRegVO.StorageID);
                   log.Info("DocID :"+pawnDocRegVO.DocID+": storage :"+pawnDocRegVO.StorageID);
                   // totList.Add("Input :"+lastitem+": DocID :"+pawnDocRegVO.DocID+": storage :"+pawnDocRegVO.StorageID);
                }
                if (pendCount == 0)
                {
                    break;
                }
                lastitem = vo1[vo1.Count - 1].DocID;
               // log.Debug("Break in between...................");
               // break;
            }
            initCount = totList.Count;
            log.Info("Initial count:" + initCount);
            foreach (string VARIABLE in totList)
            {
                if (!dict.ContainsKey(VARIABLE))
                {
                    dict.Add(VARIABLE, VARIABLE);
                }
                else
                {
                    log.Debug(VARIABLE);
                }
            }
            totList = new List<string>(dict.Values);
            log.Info("No of dupl=" + (initCount - totList.Count));
           // log.Info("Filtered Count :" + totList.Count);
            return totList;
        }


        private List<string> loopAndGetDocs4()
        {
            List<string> totList = new List<string>();
            DocumentDAO dao = DocumentDAO.getInstance();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            int pendCount = 0;
            int errorCode = 0;
            string errorMsg = "";
           
            int initCount = 0;
            List<PawnDocRegVO> vo1 = new List<PawnDocRegVO>();
            vo1 = new List<PawnDocRegVO>();
            
            dao.GetDocumentSets(lastitem, out vo1, out pendCount, out errorCode, out errorMsg);

            foreach (var pawnDocRegVO in vo1)
            {
                totList.Add(pawnDocRegVO.StorageID);
                log.Info("DocID :" + pawnDocRegVO.DocID + ": storage :" + pawnDocRegVO.StorageID);
                // totList.Add("Input :"+lastitem+": DocID :"+pawnDocRegVO.DocID+": storage :"+pawnDocRegVO.StorageID);
            }
           lastitem = vo1[vo1.Count - 1].DocID;
           log.Info("Initial count:" + totList.Count);
           return totList;
        }

        //For 2 new procs from sree
        private List<string> loopAndGetDocs3()
        {
            List<string> totList = new List<string>();
            DocumentDAO dao = DocumentDAO.getInstance();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            int pendCount = 0;
            int errorCode = 0;
            string errorMsg = "";
            int lastitem = 0;
            int initCount = 0;
            List<PawnDocRegVO> vo1 = new List<PawnDocRegVO>();

            //First call to get count
            dao.CountProcTest(0, out vo1, out pendCount, out errorCode, out errorMsg);
            log.Info("Total Count Retruned by New Proc : " + pendCount);
            while (true)
            {
                vo1 = new List<PawnDocRegVO>();
                dao.GetDocumentSets(lastitem, out vo1, out pendCount, out errorCode, out errorMsg);
                log.Info("#################################");
                log.Info("Input Value :" + lastitem);
                log.Info("#################################");
                foreach (var pawnDocRegVO in vo1)
                {
                    totList.Add(pawnDocRegVO.StorageID);
                    log.Info("DocID :" + pawnDocRegVO.DocID + ": storage :" + pawnDocRegVO.StorageID);
                    // totList.Add("Input :"+lastitem+": DocID :"+pawnDocRegVO.DocID+": storage :"+pawnDocRegVO.StorageID);
                }
                if (vo1.Count == 0)
                {
                    break;
                }
                lastitem = vo1[vo1.Count - 1].DocID;
            }
            initCount = totList.Count;
            log.Info("Initial count By Proc2:" + initCount);
            foreach (string VARIABLE in totList)
            {
                if (!dict.ContainsKey(VARIABLE))
                {
                    dict.Add(VARIABLE, VARIABLE);
                }
                else
                {
                    log.Debug(VARIABLE);
                }
            }
            totList = new List<string>(dict.Values);
            log.Info("No of dupl=" + (initCount - totList.Count));
            // log.Info("Filtered Count :" + totList.Count);
            return totList;
        }*/

        public void populateData(CouchVo vo, bool isDelete, string getDocID)
        {
            setCouchValues(vo);
            string fileWithPath = "C:\\dev\\dev_jak2\\devwork_jkingsley\\Phase2App\\CouchConnectorApp\\test\\sample.pdf";
            bool added = false;
            List<string> docIds = null;
            List<string> totList = new List<string>();

            //totList = loopAndGetDocs();
            //log.Info("Calling million docs");
            //totList = loopAndGetDocs4();
            // proc list below
            //totList = loopAndGetDocs();
            //million doc by temp query below
            totList = loopAndGetDocs2();
            //log.Debug("Aborted total exec...");
            //return;
            /*GetDocumentSets(ref docIds, 0,1000);
            totList.AddRange(docIds);
            GetDocumentSets(ref docIds, 2000, 3000);
            totList.AddRange(docIds);
            GetDocumentSets(ref docIds, 9000, 10000);
            totList.AddRange(docIds);
            GetDocumentSets(ref docIds, 15000,20000);
            totList.AddRange(docIds);
            GetDocumentSets(ref docIds, 15000, 20000);
            totList.AddRange(docIds);*/

            /*foreach (var docId in docIds)
            {
                addDocumentToCouch(fileWithPath, docId, out added);
            }*/

            List<Thread> runningList = new List<Thread>();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //var addJob = null;
            int threadNameCount = 1;
            int allocatedCount = 0;
            int totCount = totList.Count;
            while(true)
            {
                if (totList.Count == 0)
                {
                    break;
                }

                int allowedCount = getAllowedCount(ref runningList);
                int jobCount = 0;
                int endCount = 1000;
                List<string> subList = new List<string>();
                while(jobCount < allowedCount)
                {
                    if (totList.Count < endCount)
                    {
                        endCount = totList.Count;
                    }
                    subList = new List<string>();
                    for(int i = 0; i < endCount; i++)
                    {
                        subList.Add(totList[i]);
                    }
                    AddTestDocThread th = new AddTestDocThread(subList, vo, getDocID);
                    Thread jobThread = null;
                    if (isDelete)
                    {
                        jobThread = new Thread(new ThreadStart(th.processDelete));
                    }
                    else
                    {
                        jobThread = new Thread(new ThreadStart(th.process));
                    }
                    string threhName = "Thread" + threadNameCount;
                    threadNameCount++;
                    jobThread.Name = threhName;
                    runningList.Add(jobThread);
                    log.Info(string.Format("Thread {0} started with count {1}", threhName, subList.Count));
                    allocatedCount += subList.Count;
                    jobThread.Start();
                    foreach(var VARIABLE in subList)
                    {
                        totList.Remove(VARIABLE);
                    }
                    if (totList.Count == 0)
                    {
                        break;
                    }
                    jobCount++;
                }

                Thread.Sleep(250);
            }
            while(true)
            {
                List<Thread> clearList = new List<Thread>();
                foreach(var thread in runningList)
                {
                    if (!thread.IsAlive)
                    {
                        clearList.Add(thread);
                    }
                }

                foreach(var thread in clearList)
                {
                    runningList.Remove(thread);
                }

                if (runningList.Count == 0)
                {
                    break;
                }
                Thread.Sleep(100);
            }
            sw.Stop();
            log.Info("Total jobs............................." + totList.Count);
            log.Info("Allocated count :" + allocatedCount);
            log.Info("Total Time" + sw.Elapsed);

            log.Info("All Done.........");
        }


        /*public void populateData2(CouchVo vo, bool isDelete, string getDocID)
        {
            setCouchValues(vo);
            string fileWithPath = "C:\\dev\\dev_jak2\\devwork_jkingsley\\Phase2App\\CouchConnectorApp\\test\\sample.pdf";
            bool added = false;
            List<string> docIds = null;
            List<string> totList = new List<string>();

          
            totList = loopAndGetDocs4();
            List<Thread> runningList = new List<Thread>();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //var addJob = null;
            int threadNameCount = 1;
            int allocatedCount = 0;
            //int totCount = totList.Count;
            while (true)
            {
                if (totList.Count == 0)
                {
                    break;
                }

                int allowedCount = getAllowedCount(ref runningList);
                int jobCount = 0;
                int endCount = 1000;
                List<string> subList = new List<string>();
                while (jobCount < allowedCount)
                {
                    if (totList.Count < endCount)
                    {
                        endCount = totList.Count;
                    }
                    subList = new List<string>();
                    for (int i = 0; i < endCount; i++)
                    {
                        subList.Add(totList[i]);
                    }
                    AddTestDocThread th = new AddTestDocThread(subList, vo, getDocID);
                    Thread jobThread = null;
                    if (isDelete)
                    {
                        jobThread = new Thread(new ThreadStart(th.processDelete));
                    }
                    else
                    {
                        jobThread = new Thread(new ThreadStart(th.process));
                    }
                    string threhName = "Thread" + threadNameCount;
                    threadNameCount++;
                    jobThread.Name = threhName;
                    runningList.Add(jobThread);
                    log.Info(string.Format("Thread {0} started with count {1}", threhName, subList.Count));
                    allocatedCount += subList.Count;
                    jobThread.Start();
                    foreach (var VARIABLE in subList)
                    {
                        totList.Remove(VARIABLE);
                    }
                    if (totList.Count == 0)
                    {
                        break;
                    }
                    jobCount++;
                }

                Thread.Sleep(250);
            }
            while (true)
            {
                List<Thread> clearList = new List<Thread>();
                foreach (var thread in runningList)
                {
                    if (!thread.IsAlive)
                    {
                        clearList.Add(thread);
                    }
                }

                foreach (var thread in clearList)
                {
                    runningList.Remove(thread);
                }

                if (runningList.Count == 0)
                {
                    break;
                }
                Thread.Sleep(100);
            }
            sw.Stop();
            log.Info("Total jobs............................." + totList.Count);
            log.Info("Allocated count :" + allocatedCount);
            log.Info("Total Time" + sw.Elapsed);
            log.Info("All Done.........");
        }*/

     /*   private void addDocsWithThreads(List<string> totList)
        {
            double dblVal = totList.Count / noOfThreads;
            decimal roundedVal = decimal.Ceiling((decimal)dblVal);
            List<string> docIds = null;
            List<List<string>> contList = new List<List<string>>();

            for(int i = 0; i < roundedVal; i++)
            {
            }
        }*/

        private void setCouchValues(CouchVo vo)
        {
            
            hostname = Properties.Settings.Default.CouchServerName;
            portName = Properties.Settings.Default.CouchPort;
            dbName =  Properties.Settings.Default.DBName;
         
            userid = vo.userName;
            pass = vo.pwd;
            adminUserid = vo.adminUserName;
            adminPass = vo.adminPwd;
        }

       /* private string addDocumentToCouch(String docName, string storageID, out bool added)
        {
            added = false;
            // msg = string.Empty;

            //DocumentType = PawnObjects.Doc.Document.DocTypeNames.PDF;
            string fileName = docName;
            int lastSlashIdx = fileName.LastIndexOf('\\');
            string fName = fileName.Substring(lastSlashIdx + 1);
            var sErrCode = string.Empty;
            var sErrText = string.Empty;
            if (fileBytes == null)
            {
                fileBytes = File.ReadAllBytes(fileName);
            }
            Document couchDocObj = (PawnObjects.Doc.Document)new Document_Couch(fileName, fName, PawnObjects.Doc.Document.DocTypeNames.PDF);
            couchDocObj.SetPropertyData("jak prop1", "value");
            //string storageID = System.Guid.NewGuid().ToString();

            // Console.WriteLine("Added doc name :" + GldocName);
            SecuredCouchConnector CouchConnectorObj = null;
            //StorageID = "1234";
            if (couchDocObj.SetSourceData(fileBytes, storageID, false))
            {
                var dStorage = new DocStorage_CouchDB();

                CouchConnectorObj = new SecuredCouchConnector(hostname, portName, secWebPort, dbName, userid, pass, isSec);
                dStorage.SecuredAddDocument(CouchConnectorObj, ref couchDocObj, out sErrCode, out sErrText);
                if (sErrCode == "0")
                {
                    added = true;
                    log.Info("Added Document ID:" + storageID);
                }
                else
                {
                    added = false;
                    log.Info("Error adding doc :" + sErrText);
                    // msg = sErrText;
                }
            }
            return storageID;
        }*/

        public bool GetDocumentSets(ref List<string> docList, int begin, int end)
        {
            bool ret = false;
            string retVal = "";
            OracleDataReader reader = null;
            if (DBConnector.getInstance().getStatus() != DBConnector.Status.INITIALIZED)
            {
                log.Info(" from docListGet :DB Connection not established: Search aborted");
                return false;
            }

            try
            {
                using(connection = new OracleConnection())
                {
                    connection.ConnectionString = DBConnector.getInstance().databaseServiceName();
                    connection.Open();
                    string sql = "";
                    sql = "SELECT REG.STORAGE_ID FROM CCSOWNER.PAWNDOCUMENTREGISTRY REG" +
                          " WHERE REG.CREATIONDATE <= TRUNC(SYSDATE) - 90 AND REG.ARCH_STATUS IS NULL" + " AND ID>=" + begin + " AND ID<=" +
                          end + " ORDER BY ID";

                    using(oracleCommand = new OracleCommand(sql, connection))
                    {
                        log.Info(string.Format("Executing: {0}", sql));
                        reader = oracleCommand.ExecuteReader();

                        if (reader.RowSize > 0 && reader.HasRows)
                        {
                            docList = new List<string>();
                            while(reader.Read())
                            {
                                docList.Add(Utilities.GetStringValue(reader["STORAGE_ID"]));
                            }
                            ret = true;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                log.Info("docListGet Failed:", e);
                log.Info(e.StackTrace.ToString());
            }
            finally
            {
                // log.Info(string.Format("Time Taken for docListGet  : {0} Msec", stopwatch.Elapsed));
                if (reader != null)
                {
                    reader.Dispose();
                    reader.Close();
                }
                oracleCommand = null;
            }
            return ret;
        }
    }
}