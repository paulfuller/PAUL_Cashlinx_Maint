using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using CouchConsoleApp.file;
using file;
using PawnDataAccess.Couch.Impl;
using PawnObjects.Doc;
using PawnObjects.Doc.Couch;

namespace CouchConsoleApp
{
    class CouchUtil
    {
        public static PawnObjects.Doc.Document.DocTypeNames DocumentType { set; get; }
        private static string GldocName = "";
        public static string hostname = "localhost";
        public static string portName = "5984";
        public static string secWebPort = "6984";
        public static string dbName = "perfdb";
        public static bool isSec = false;
        private static string userid = "mydbuser";
        private static string pass = "mydbuser";

        public static Process process;

        public CouchUtil(CouchVo vo)
        {
            setCouchValues(vo);

        }

        private void setCouchValues(CouchVo vo)
        {
            hostname = vo.serverName;
            portName = vo.serverport;
            dbName = vo.dbName;
            userid = vo.userName;
            pass = vo.pwd; 
        }

      
        public CouchVo couchLogin(CouchVo vo1)
        {

            setCouchValues(vo1);
            bool result = false;
            string msg = string.Empty;
            string fileName = "C:\\Program Files\\Phase2App\\templates\\Report031620110858461303709.pdf";
            string docID = null;
            try
            {
                docID = couchLoginVerification(fileName, out msg, out result);
            }
            catch (Exception)
            {
                vo1.errorMSG = msg;
                vo1.isError = result;
                return vo1;
            }
            if (!result)
            {
                vo1.errorMSG = msg;
                vo1.isError = true;
                return vo1;
            }

            Console.WriteLine("Added doc :" + docID);
            SecuredCouchConnector CouchConnectorObj = new SecuredCouchConnector(hostname, portName, secWebPort, dbName, userid, pass, isSec);
            result=CouchConnectorObj.Document_Delete(GldocName);
            //CouchVo vo = new CouchVo();
            vo1.errorMSG = "";
            if(!result)
                vo1.isError = true;
            return vo1;
        }

        public CouchVo storeDocument(CouchVo vo1, System.Windows.Forms.RichTextBox resultBox)
        {

            setCouchValues(vo1);
            bool result = false;
            string msg = string.Empty;
            string fileName = vo1.fileWithPath;
            string docID = null;
            try
            {
                docID = addDocumentToCouch(fileName, out msg, out result, resultBox);
            }
            catch (Exception e)
            {
                vo1.errorMSG = msg;
                vo1.isError = result;
                return vo1;
            }
            resultBox.AppendText("\n");
            resultBox.AppendText("Added document :" + docID + " as property");
            resultBox.AppendText("\n");
            return vo1;
        }


        public void GetDocument(CouchVo vo1, System.Windows.Forms.RichTextBox resultBox)
        {
            setCouchValues(vo1);
            if(process!=null && !process.HasExited)
            {
                process.Kill();
            }
            var dStorage = new DocStorage_CouchDB();
            PawnObjects.Doc.Document doc = (PawnObjects.Doc.Document)new Document_Couch(vo1.documentID);
            var errCode = string.Empty;
            var errTxt = string.Empty;
            byte[] fileData;
            FileStream fStream = null;

            SecuredCouchConnector CouchConnectorObj = new SecuredCouchConnector(hostname, portName, secWebPort, dbName, userid, pass, isSec);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            dStorage.SecuredGetDocument(CouchConnectorObj, ref doc, out errCode, out errTxt);
            sw.Stop();
            resultBox.AppendText("\n");
            resultBox.AppendText("Time Taken to get :"+ sw.ElapsedMilliseconds);
            resultBox.AppendText("\n");
           // dStorage.SecuredGetDocumentWithAttachment(CouchConnectorObj, ref doc, out errCode, out errTxt);
            if (CouchConnectorObj.Error)
            {
                Console.WriteLine(errTxt);
                resultBox.AppendText(errTxt);
                resultBox.AppendText("\n");
                //return false;
            }
            else
            {
                resultBox.AppendText(string.Format("Got Doc '{0}'  from db '{1}' by userID '{2}'", doc.FileId, dbName, userid));
                resultBox.AppendText("\n");
                doc.GetSourceData(out fileData);
                string path=FileHandler.createPDFDir();
                string fileWithPath = path + "\\" + doc.FileName;
                if (fileData != null)
                {
                    fStream = File.Create(fileWithPath);
                    fStream.Write(fileData, 0, fileData.Length);
                    resultBox.AppendText(("written to file :" + fileWithPath));
                    resultBox.AppendText("\n");
                    fStream.Flush();
                    fStream.Close();
                   process=Process.Start(fileWithPath);
                   
                }
               // return true;
            }
        }

        private static string addDocumentToCouch(String docName, out string msg, out bool added,System.Windows.Forms.RichTextBox textBox)
        {
            added = false;
            msg = string.Empty;

            DocumentType = PawnObjects.Doc.Document.DocTypeNames.PDF;
            string fileName = docName;
            int lastSlashIdx = fileName.LastIndexOf('\\');
            string fName = fileName.Substring(lastSlashIdx + 1);
            var sErrCode = string.Empty;
            var sErrText = string.Empty;
            byte[] fileBytes = File.ReadAllBytes(fileName);
            Document couchDocObj = (PawnObjects.Doc.Document)new Document_Couch(fileName, fName, DocumentType);
            couchDocObj.SetPropertyData("jak prop1", "value");
            string storageID = System.Guid.NewGuid().ToString();
            GldocName = storageID;
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
                    msg = "Added" + fileName;
                    textBox.AppendText("Document ID:"+GldocName);
                    textBox.AppendText("\n");
                }
                else
                {
                    added = false;
                    msg = sErrText;
                }

            }
            return storageID;
        }


       

        /***
         * This method will be used for couch authentication functionality
         ***/
        private static string couchLoginVerification(String docName, out string msg, out bool added)
        {
            added = false;
            msg = string.Empty;

            DocumentType = PawnObjects.Doc.Document.DocTypeNames.PDF;
            string fileName = docName;
            int lastSlashIdx = fileName.LastIndexOf('\\');
            string fName = fileName.Substring(lastSlashIdx + 1);
            var sErrCode = string.Empty;
            var sErrText = string.Empty;
            //byte[] fileBytes = File.ReadAllBytes(fileName);
            byte[] fileBytes=SimpleHash.convertToByteArr("This is connection test");
            Document couchDocObj = (PawnObjects.Doc.Document)new Document_Couch(fileName, fName, DocumentType);
            couchDocObj.SetPropertyData("jak prop1", "value");
            string storageID = System.Guid.NewGuid().ToString();
            GldocName = storageID;
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
                    msg = "Added" + fileName;
       
                }
                else
                {
                    added = false;
                    msg = sErrText;
                }

            }
            return storageID;
        }
    }
}
