using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Database.Couch.Impl;
using Common.Libraries.Objects.Doc;
using CouchConsoleApp.vo;
using JsonFx.Json;
using log4net;


namespace CouchConsoleApp.couch
{
    public class CouchArchiverGetHelper
    {
        private static readonly CouchArchiverGetHelper inst = new CouchArchiverGetHelper();

        private readonly ILog log = LogManager.GetLogger(typeof(CouchArchiverGetHelper));

        private static bool setReqTimeout = false;
        private static int reqTimeoutVal = 180000;


        private CouchArchiverGetHelper()
        {
             if (Properties.Settings.Default.Override_Couch_Request_TimeOut &&
                Properties.Settings.Default.Couch_Request_Timeout > 180000)
            {
                setReqTimeout = true;
                reqTimeoutVal = Properties.Settings.Default.Couch_Request_Timeout;
                log.Info("Couch Request timeout overriden :" + reqTimeoutVal);
            }else
            {
                log.Info("Couch Request timeout not overriden :" + reqTimeoutVal);
            }
        }


        public static CouchArchiverGetHelper getInstance()
        {
            return inst;
        }

        public Document GetDocument(out string errTxt, out bool isError, CouchVo vo, PawnDocRegVO regVO)
        {
            var dStorage = new DocStorage_CouchDB();
            Document doc = (Document)new Document_Couch(regVO.StorageID);
            constructDoc(vo, ref doc, out isError, out errTxt);
            return doc;
        }

        public string GetRawDocument(out string errTxt, out bool isError, CouchVo vo, PawnDocRegVO regVO)
        {
            try
            {
                byte[] btData = DocumentGet(regVO.StorageID, false, out isError, out errTxt, vo);

                if (!isError && btData != null)
                {
                    string sResult = ByteArrayToString(btData, out isError, out errTxt);
                    return sResult;
                }
                else
                {
                    return "";
                }
            }
            catch(Exception exp)
            {
                isError = true;
                errTxt = "Error converting document" + exp.Message;
            }
            return "";
        }

        public void constructDoc(CouchVo cvo, ref Document document, out bool isError, out string sErrorText)
        {
            sErrorText = "";
            isError = false;
            if (document != null)
            {
                IDictionary objParms = GetDocFrmCouch(document.FileId, out isError, out sErrorText, cvo);
                /*if (objParms==null)
                    return false;*/
                if (isError)
                {
                    return;
                }
                foreach(DictionaryEntry objParm in objParms)
                {
                    string sKey = objParm.Key.ToString();
                    string sValue = objParm.Value.ToString();

                    switch(sKey)
                    {
                        case "_id":
                            document.FileId = sValue;
                            break;
                        case "_rev":
                            document.SetPropertyData(Document_Couch.DocumentRevison, sValue);
                            break;
                        case "FileData":
                            document.FileData = sValue;
                            break;
                        case "FileName":
                            document.FileName = sValue;
                            break;
                        case "FilePath":
                            document.FilePath = sValue;
                            break;
                        case "FileType":
                            var typeName = (Document.DocTypeNames)Enum.Parse(typeof(Document.DocTypeNames), sValue);
                            document.TypeName = typeName;
                            break;
                        case "ChildDocuments":
                            var lstChildDocs = new List<string>();
                            if (!string.IsNullOrEmpty(sValue))
                            {
                                string[] arChildDocs = sValue.Split(',');
                                if (arChildDocs.Length > 0)
                                {
                                    lstChildDocs.AddRange(arChildDocs);
                                }
                            }
                            document.ChildDocs.AddRange(lstChildDocs);
                            break;
                        case Document_Couch.MetaTag:
                            document.AddMetaData(sValue);
                            break;
                        default:
                            document.SetPropertyData(sKey, sValue);
                            break;
                    }
                }
            }
        }

        public IDictionary GetDocFrmCouch(string sDocumentId, out bool sError, out string sErrorText, CouchVo cvo)
        {
            IDictionary objParms = null;

            try
            {
                byte[] btData = DocumentGet(sDocumentId, false, out sError, out sErrorText, cvo);

                if (!sError && btData != null)
                {
                    string sResult = ByteArrayToString(btData, out sError, out sErrorText);
                    objParms = (IDictionary)JsonReader.Deserialize(sResult);
                }
            }
            catch(Exception exp)
            {
                sError = true;
                sErrorText = exp.Message;
            }
            return objParms;
        }

        private HttpWebRequest CreateRequest(string sGetURL, string baseURL, string _userId, string _password, bool isNewCookie)
        {
            var sec = new PawnCouchConcurrentAccessSecurity(baseURL, _userId, _password);

            if (sec.GetSession(isNewCookie) == null)
            {
                return null;
            }
            var webRequester = (HttpWebRequest)WebRequest.Create(sGetURL);
            webRequester.Headers.Add("Cookie", "AuthSession=" + sec.GetSession(isNewCookie).Value);
            webRequester.Method = "GET";
            webRequester.Headers.Add("Accept-Charset", "utf-8");
            webRequester.Headers.Add("Accept-Language", "en-us");
            webRequester.ContentType = "application/json";
            //webRequester.KeepAlive = true;
            if (setReqTimeout) webRequester.Timeout = reqTimeoutVal;
            //webRequester.Timeout = 150000;
            return webRequester;
        }

        private void setRequestTimeOut(HttpWebRequest request)
        {
            if (Properties.Settings.Default.Override_Couch_Request_TimeOut)
            {
                
            }
        }

        private byte[] DocumentGet(string sDocumentId, bool isRetry, out bool sError, out string sErrorText, CouchVo cvo)
        {
            sError = false;
            sErrorText = "";
            byte[] btData = null;
            HttpWebRequest webRequester = null;
            string sGetURL = null;
            try
            {
                string _connUri = String.Format("http://{0}:{1}/", cvo.serverName, cvo.serverport);
                string _dbName = cvo.dbName;

                sGetURL = _connUri + _dbName + "/" + sDocumentId;
                try
                {
                    if (isRetry)
                    {
                        webRequester = CreateRequest(sGetURL, _connUri, cvo.userName, cvo.pwd, true);
                    }
                    else
                    {
                        webRequester = CreateRequest(sGetURL, _connUri, cvo.userName, cvo.pwd, false);
                    }

                    if (webRequester == null)
                    {
                        sError = true;
                        sErrorText = "Document get aborted:" + sGetURL + ":" +"Session failure" + " : DocID :" + sDocumentId;
                        return btData;
                    }
                }
                catch(Exception e)
                {
                    if (webRequester != null)
                    {
                        try
                        {
                            webRequester.Abort();
                        }catch(Exception e1)
                        {
                            log.Info("Error aborting webrequst in get document"+e1.StackTrace+" : "+e1.Message);
                        }
                        webRequester = null;
                    }
                    sGetURL = null;
                    sError = true;
                    sErrorText = "Document get aborted:" + sGetURL + ":" + e.Message + " : DocID :" + sDocumentId;
                    log.Error("Document get error:" + sDocumentId +":"+ e.Message);
                    return btData;
                }
                using(var response = (HttpWebResponse)webRequester.GetResponse())
                {
                    if (DocGetErrAndRetry(response.ResponseUri.ToString(), sDocumentId, out sError, out sErrorText, cvo, false))
                    {
                        if ((!isRetry))
                        {
                            //Console.WriteLine("Doing re-try");
                            btData = DocumentGet(sDocumentId, true, out sError, out sErrorText, cvo);
                                    //perform re-try since session expired unexpectedly
                            return btData;
                        }
                        else
                        {
                            return btData; //return error since re-try also failed
                        }
                    }
                    // Get the stream.
                    using(Stream stream = response.GetResponseStream())
                    {
                        // Use the ReadFully method from the link above:
                        btData = CouchConnector.ReadFully(stream, response.ContentLength);
                        sError = false;
                        sErrorText = "Got Document";
                    }
                }
            }
            catch(Exception exp)
            {
                /* log.Debug(string.Format("Get Doc Failed:{0} url{1} user{2} pwd {3}", exp.StackTrace,
                     sGetURL,_userId,_password));*/
                log.Error("Document get error:" + sDocumentId + ":" + exp.Message);
                DocGetErrAndRetry(exp.Message, sDocumentId, out sError, out sErrorText, cvo, true);
            }finally
            {
                if (webRequester != null)
                {
                    try
                    {
                        webRequester.Abort();
                    }
                    catch (Exception e1)
                    {
                        log.Info("Error aborting webrequst in get document" + e1.StackTrace + " : " + e1.Message);
                    }
                    webRequester = null;
                }
            }
            return btData;
        }

        public bool DocGetErrAndRetry(string respExp, string docID, out bool sError, out string sErrorText, CouchVo cvo, bool isException)
        {
            sError = true;

            if (isException)
            {
                //if exception retry
                sErrorText = respExp;
                return true;
            }

            if ((respExp != null) && (respExp.ToLower().Contains("not authorized")))
            {
                sErrorText = String.Format(
                                           "Specified user '{0}' does not have access to DB '{1}' for document '{2}' or session is invalid",
                                           cvo.userName, cvo.dbName, docID);
                return true; //do re-try
            }
            else if ((respExp != null) && respExp.Contains("404"))
            {
                sErrorText = String.Format("Specified document '{0}' not found in DB '{1}'", docID, cvo.dbName);
                return false; //No re-try
            }
            else
            {
                sErrorText = "";
                return false;
            }
        }

        protected string ByteArrayToString(byte[] btData, out bool sError, out string sErrorText)
        {
            sError = false;
            sErrorText = "";
            string sStringData = "";

            try
            {
                Encoding enc = Encoding.ASCII;
                sStringData = enc.GetString(btData);
            }
            catch(Exception exp)
            {
                sError = true;
                sErrorText = exp.Message;
            }
            return sStringData;
        }
    }
}