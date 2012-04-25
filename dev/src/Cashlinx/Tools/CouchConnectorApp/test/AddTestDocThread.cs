using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Common.Controllers.Database.Couch.Impl;
using Common.Libraries.Objects.Doc;
using CouchConsoleApp.couch;
using CouchConsoleApp.vo;
using log4net;


namespace CouchConsoleApp.test
{
    public class AddTestDocThread
    {
        public List<string> docList;
        private readonly ILog log = LogManager.GetLogger(typeof(AddTestDocThread));
        private static byte[] fileBytes = null;

        public static string hostname = "couchdb-dev";
        public static string portName = "5985";
        public static string secWebPort = "6984";
        public static string dbName = "clx_cust_docs_dev";
        public static bool isSec = false;
        private static string userid = "clxuser1";
        private static string pass = "mydbuser";

        private static string adminUserid = "mydbuser";
        private static string adminPass = "mydbuser";
        private string fileWithPath = "";

        private CouchVo srcvo;

        private int successCount = 0;
        private int failCount = 0;
        private string docID = null;
        public AddTestDocThread(List<string> docList, CouchVo vo, string getDocID)
        {
            this.docList = docList;
            setCouchValues(vo);
            srcvo = vo;
            docID = getDocID;
        }


        private void setCouchValues(CouchVo vo)
        {
            hostname = vo.serverName;
            portName = vo.serverport;
            dbName = vo.dbName;
            userid = vo.userName;
            pass = vo.pwd;
            adminUserid = vo.adminUserName;
            adminPass = vo.adminPwd;

        }

        public void process()
        {
            //fileWithPath = "C:\\dev\\dev_jak2\\devwork_jkingsley\\Phase2App\\CouchConnectorApp\\test\\sample.pdf";
            bool addedDocError;
            string addError;
            string geterrorText;
            bool getisError;
            PawnDocRegVO regVO = new PawnDocRegVO();
           // regVO.StorageID = "f544ed6c-6c02-4711-9727-626dabe95a4a";
            regVO.StorageID = docID;
            string rdoc = CouchArchiverGetHelper.getInstance().GetRawDocument(out geterrorText, out getisError, srcvo, regVO);

            int successCount = 0;
            int failedCount = 0;

            foreach(var doc in docList)
            {
                //addDocumentToCouch(fileWithPath, doc,out added);
                if (rdoc != null){
                    //rdoc.Replace("f544ed6c-6c02-4711-9727-626dabe95a4a", doc);
                    rdoc.Replace(docID, doc);
                    regVO.StorageID = docID;
            }
                bool added = CouchArchiverAddHelper.getInstance().Document_Add(rdoc, out addedDocError, out addError, srcvo, doc);
                if (addedDocError)
                {
                    log.Error(string.Format("Doc Add failed for docid{0}, addedDocError{1} addError{2}", doc, addedDocError, addError));
                    failedCount++;
                }else
                {
                    successCount++;
                }
                //log.Info(string.Format("Doc Add for docid{0}, addedDocError{1} addError{2}",doc,addedDocError,addError));
                Thread.Sleep(50);
                //log.Debug("Processing ... "+doc);
            }
           log.Info(string.Format(" Success count {0} fail count {1} Total count {2}", successCount, failCount, docList.Count));
        }

        public void processGet()
        {
            //fileWithPath = "C:\\dev\\dev_jak2\\devwork_jkingsley\\Phase2App\\CouchConnectorApp\\test\\sample.pdf";
            bool isError;
            string errorText;
            PawnDocRegVO regVO=new PawnDocRegVO();
            foreach (var doc in docList)
            {
                regVO = new PawnDocRegVO();
                regVO.StorageID = doc;
                //new CouchUtil(srcvo).GetDocument(out msg, out gotDoc, srcvo, regVO);
                CouchArchiverGetHelper.getInstance().GetDocument(out errorText, out isError, srcvo, regVO);

                if (!isError)
                    successCount++;
                else
                {
                    failCount++;
                }
                //log.Debug(string.Format("{0} Is Error ... {1} return {2} ", doc, isError, errorText));
            }
            log.Info(string.Format(" Success count {0} fail count {1} Total count {2}", successCount, failCount, docList.Count));
        }


        public void processDelete()
        {
            CouchUtil cUtil = new CouchUtil();

            bool isError;
            string errorText;
            foreach (var doc in docList)
            {
                bool SouceDocDeleted;
                string exception;
                //cUtil.DeleteDocument(srcvo, doc, out exception, out SouceDocDeleted);
                //new CouchUtil(srcvo).GetDocument(out msg, out gotDoc, srcvo, regVO);
                CouchArchiverDeleteHelper.getInstance().Document_Delete(out errorText, out isError, srcvo, doc);

                if (!isError)
                    successCount++;
                else
                {
                    failCount++;
                }
                //
            }
            log.Info(string.Format(" Success delete count {0} fail count {1} Total count {2}", successCount, failCount, docList.Count));
        }

        private string addDocumentToCouch(String docName, string storageID, out bool added)
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
                fileBytes = File.ReadAllBytes(fileName);
            Document couchDocObj = (Document)new Document_Couch(fileName, fName, Document.DocTypeNames.PDF);
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
                    //log.Info("Added Document ID:" + storageID);
                    successCount++;
                }
                else
                {
                    added = false;
                    log.Info("Error adding doc :" + sErrText);
                    failCount++;
                    // msg = sErrText;
                }

            }
            return storageID;
        }
    }
}
