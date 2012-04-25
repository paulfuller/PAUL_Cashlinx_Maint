using System;
using System.Diagnostics;
using System.IO;
using Common.Controllers.Database.Couch.Impl;
using Common.Libraries.Objects.Doc;
using CouchConsoleApp.file;
using CouchConsoleApp.vo;
using file;
using log4net;


namespace CouchConsoleApp.couch
{
    class CouchUtil
    {
        public static Document.DocTypeNames DocumentType { set; get; }
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
        private readonly ILog log = LogManager.GetLogger(typeof(CouchUtil));
        public static Process process;

        public CouchUtil(CouchVo vo)
        {
            setCouchValues(vo);

        }

        public CouchUtil()
        {
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

      
        public CouchVo couchLogin(CouchVo vo1)
        {

            //bool result = false;
            string msg = string.Empty;

            SecuredCouchArchiver couchArch = new SecuredCouchArchiver(hostname, portName, secWebPort, dbName, userid, pass, isSec);

            if (!couchArch.Authenticate(vo1, vo1.userName, vo1.pwd, out msg))
            {
                vo1.errorMSG = "Authtication failed for '" + vo1.userName + "':" + msg;
                vo1.isError = true;
                return vo1;
            }
            vo1.isError = false;
            return vo1;

            /*setCouchValues(vo1);
            bool result = false;
            string msg = string.Empty;
            string fileName = "C:\\Program Files\\Phase2App\\templates\\Report031620110858461303709.pdf";
            string docID = null;
            try
            {
                docID = couchLoginVerification(fileName, out msg, out result,false);
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
            return vo1;*/
        }

        public CouchVo couchDualLogin(CouchVo vo1)
        {

           /* setCouchValues(vo1);
            bool result = false;
            string msg = string.Empty;
            string fileName = "C:\\Program Files\\Phase2App\\templates\\Report031620110858461303709.pdf";
            string docID = null;
            try
            {
                docID = couchLoginVerification(fileName, out msg, out result,false);
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
            result = CouchConnectorObj.Document_Delete(GldocName);


            try
            {
                docID = couchLoginVerification(fileName, out msg, out result, true);
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
            CouchConnectorObj = new SecuredCouchConnector(hostname, portName, secWebPort, dbName, userid, pass, isSec);
            result = CouchConnectorObj.Document_Delete(GldocName);
            
            //CouchVo vo = new CouchVo();
            vo1.errorMSG = "";
            if (!result)
                vo1.isError = true;
            return vo1;*/
            return couchDualLogin2(vo1);
        }

        public CouchVo couchDualLogin2(CouchVo vo1)
        {

            setCouchValues(vo1);
            bool result = false;
            string msg = string.Empty;
            SecuredCouchArchiver couchArch = new SecuredCouchArchiver(hostname, portName, secWebPort, dbName, userid, pass, isSec);

            if(!couchArch.Authenticate(vo1, vo1.userName, vo1.pwd,out msg))
            {
                vo1.errorMSG = "Authtication failed for '" + vo1.userName + "':" + msg;
                vo1.isError = true;
                return vo1;
            }

            if (!couchArch.Authenticate(vo1, vo1.adminUserName, vo1.adminPwd, out msg))
            {
                vo1.errorMSG = "Authtication failed for '" + vo1.adminUserName + "':" + msg;
                vo1.isError = true;
                return vo1;
            }
            vo1.isError = false;
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

        public void DeleteDocument(CouchVo sourceVO,string docID, out string msg,out bool deleted)
        {
            msg = "";
            deleted = false;
            setCouchValues(sourceVO);
            var CouchConnectorObj = new SecuredCouchConnector(hostname, portName, secWebPort, dbName, userid, pass, isSec);
            deleted=CouchConnectorObj.Document_Delete(docID);
            msg = CouchConnectorObj.Message;
        }
        

        public void GetDocument(CouchVo vo1, System.Windows.Forms.RichTextBox resultBox)
        {
            setCouchValues(vo1);
            if(process!=null && !process.HasExited)
            {
                process.Kill();
            }
            var dStorage = new DocStorage_CouchDB();
            Document doc = (Document)new Document_Couch(vo1.documentID);
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

        public void addDocumentToCouch(CouchVo vo,Document document,out string msg ,out bool added)
        {
            added = false;
            msg = "";
            var sErrCode = string.Empty;
            var sErrText = string.Empty;
            setCouchValues(vo);
            SecuredCouchConnector CouchConnectorObj = null;
            var dStorage = new DocStorage_CouchDB();
            CouchConnectorObj = new SecuredCouchConnector(hostname, portName, secWebPort, dbName, userid, pass, isSec);
            dStorage.SecuredAddDocument(CouchConnectorObj, ref document, out sErrCode, out sErrText);
            msg = sErrText;
            if (sErrCode == "0")
            {
                added = true;
                //log.Debug("Added Document ID:" + document.FileId);
                   
            }
            else
            {
                added = false;
               // log.Debug("Error adding doc :" + sErrText);
            }
        }


        private static string addDocumentToCouch(String docName, out string msg, out bool added,System.Windows.Forms.RichTextBox textBox)
        {
            added = false;
            msg = string.Empty;

            DocumentType = Document.DocTypeNames.PDF;
            string fileName = docName;
            int lastSlashIdx = fileName.LastIndexOf('\\');
            string fName = fileName.Substring(lastSlashIdx + 1);
            var sErrCode = string.Empty;
            var sErrText = string.Empty;
            byte[] fileBytes = File.ReadAllBytes(fileName);
            Document couchDocObj = (Document)new Document_Couch(fileName, fName, DocumentType);
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

         public void createDBInCouch(String dbName, out string msg,
             out bool created,CouchVo vo)
         {
             msg = "";
             created = false;
             bool dbCreate = false;
             bool permCreate = false;

             var cArch = new SecuredCouchArchiver(vo.serverName, vo.serverport, "1234",
                 vo.dbName, vo.adminUserName, vo.adminPwd, false);

             if (cArch.createArchDB(dbName, vo))
             {
                 dbCreate = true;
                 msg = cArch.Message;
                 
                if(cArch.secureArchDB(dbName, vo))
                {
                    permCreate = true;
                    msg += ":" + cArch.Message;
                }else
                {
                    msg += ":" + cArch.Message;
                }

             }else
             {
                 msg = cArch.Message; 
             }

             log.Info(string.Format("db '{0}' created '{1}' permission created '{2}'", dbName, dbCreate, permCreate));
             if(dbCreate && permCreate)
                 created = true;
             else
             {
                 created = false; 
             }

         }

       public void deleteDBInCouch(String dbName, out string msg,
             out bool deleted,CouchVo vo)
       {
           msg = "";
           deleted = false;
            var cArch = new SecuredCouchArchiver(vo.serverName, vo.serverport, "1234",
                 vo.dbName, vo.adminUserName, vo.adminPwd, false);

            if (cArch.deleteDB(dbName,vo))
            {
                deleted = true;
            }
           msg = cArch.Message;
       }


        /***
         * This method will be used for couch authentication functionality
         ***/
        private static string couchLoginVerification(String docName, out string msg, out bool added,bool isAdminVerify)
        {
            added = false;
            msg = string.Empty;

            DocumentType = Document.DocTypeNames.PDF;
            string fileName = docName;
            int lastSlashIdx = fileName.LastIndexOf('\\');
            string fName = fileName.Substring(lastSlashIdx + 1);
            var sErrCode = string.Empty;
            var sErrText = string.Empty;
            //byte[] fileBytes = File.ReadAllBytes(fileName);
            byte[] fileBytes=SimpleHash.convertToByteArr("This is connection test");
            Document couchDocObj = (Document)new Document_Couch(fileName, fName, DocumentType);
            couchDocObj.SetPropertyData("jak prop1", "value");
            string storageID = System.Guid.NewGuid().ToString();
            GldocName = storageID;
            // Console.WriteLine("Added doc name :" + GldocName);
            SecuredCouchConnector CouchConnectorObj = null;
            //StorageID = "1234";
            if (couchDocObj.SetSourceData(fileBytes, storageID, false))
            {
                var dStorage = new DocStorage_CouchDB();
                if (isAdminVerify)
                {
                    CouchConnectorObj = new SecuredCouchConnector(hostname, portName, secWebPort, dbName, adminUserid, adminPass, isSec);
                    dStorage.SecuredAddDocument(CouchConnectorObj, ref couchDocObj, out sErrCode, out sErrText);
                }else
                {
                    CouchConnectorObj = new SecuredCouchConnector(hostname, portName, secWebPort, dbName, userid, pass, isSec);
                    dStorage.SecuredAddDocument(CouchConnectorObj, ref couchDocObj, out sErrCode, out sErrText);
                }
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

        public Document GetDocument(out string msg, out bool gotDoc, CouchVo vo,PawnDocRegVO regVO)
        {
            setCouchValues(vo);
            var dStorage = new DocStorage_CouchDB();
            var errCode = string.Empty;
            var errTxt = string.Empty;
            Document doc = (Document)new Document_Couch(regVO.StorageID);
           // SecuredCouchConnector CouchConnectorObj = new SecuredCouchConnector(hostname, portName, secWebPort, dbName, userid, pass, isSec);
            SecuredCouchArchiver CouchConnectorObj = new SecuredCouchArchiver(hostname, portName, secWebPort, dbName, userid, pass, isSec);
            dStorage.SecuredGetDocument(CouchConnectorObj, ref doc, out errCode, out errTxt);

            if (CouchConnectorObj.Error)
            {
                //log.Error("errTxt :" + errTxt);
                //log.Error("errCode :" + errCode);
                msg = errTxt;
                gotDoc = false;
            }
            else
            {
                //log.Debug("errTxt :" + errTxt);
                //log.Debug("errCode :" + errCode);
                msg = errTxt;
                gotDoc = true;
                // return true;
            }
            return doc;
        }
    }
}
