using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using CouchConsoleApp.vo;
using JsonFx.Json;
using log4net;


namespace CouchConsoleApp.couch
{
    public class CouchArchiverAddHelper
    {
        private static readonly CouchArchiverAddHelper inst = new CouchArchiverAddHelper();

        private readonly ILog log = LogManager.GetLogger(typeof(CouchArchiverAddHelper));
        
        private static bool setReqTimeout = false;
        private static int reqTimeoutVal = 180000;


        private CouchArchiverAddHelper()
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

        public static CouchArchiverAddHelper getInstance()
        {
            return inst;
        }

        public bool Document_Add(string rawDocument, out bool isError, out string errorMSG, CouchVo cvo, string storageID)
        {
            if (string.IsNullOrEmpty(rawDocument))
            {
                isError = true;
                errorMSG = "Empty document data add aborted.";
                return false;
            }
            return DocumentAdd(rawDocument, out isError, out errorMSG, false, cvo, storageID);
        }

        public bool Document_Add(string rawDocument, out bool isError, out string errorMSG, CouchVo cvo, string storageID,string dbName)
        {
            if (string.IsNullOrEmpty(rawDocument))
            {
                isError = true;
                errorMSG = "Empty document data add aborted.";
                return false;
            }
            return DocumentAdd(rawDocument, out isError, out errorMSG, false, cvo, storageID,dbName);
        }

        private HttpWebRequest CreateRequest(string sPostUrl, string baseURL, string _userId, string _password, bool isNewCookie)
        {
            var sec = new PawnCouchConcurrentAccessSecurity(baseURL, _userId, _password);

            if (sec.GetSession(isNewCookie) == null)
            {
                return null;
            }
            var webRequester = (HttpWebRequest)WebRequest.Create(sPostUrl);
            webRequester.Headers.Add("Cookie", "AuthSession=" + sec.GetSession(isNewCookie).Value);
            webRequester.Method = "PUT";
            webRequester.Headers.Add("Accept-Charset", "utf-8");
            webRequester.Headers.Add("Accept-Language", "en-us");
            webRequester.ContentType = "application/json";
            //webRequester.KeepAlive = true;
            if (setReqTimeout)webRequester.Timeout = reqTimeoutVal;
            return webRequester;
        }

        private bool DocumentAdd(string rawDocument, out bool isError, out string errorMSG, bool isRetry, CouchVo cvo, string storageID,string dbName)
        {
            isError = false;
            errorMSG = "";
            string _WebResult = "";
            HttpWebRequest webRequester = null;
            string sGetURL = null;
            IDictionary iDictionary = null;
            //log.Error(rawDocument);

            string _connUri = String.Format("http://{0}:{1}/", cvo.serverName, cvo.serverport);
            string _dbName = dbName;
            cvo.dbName = _dbName;

            string sPostURL = _connUri + _dbName + "/" + storageID;
            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes(rawDocument);
                if (isRetry)
                {
                    webRequester = CreateRequest(sPostURL, _connUri, cvo.userName, cvo.pwd, true);
                }
                else
                {
                    webRequester = CreateRequest(sPostURL, _connUri, cvo.userName, cvo.pwd, false);
                }

                if (webRequester == null)
                {
                    isError = true;
                    errorMSG = "Document Add aborted:" + sPostURL + ":" + "Session failure" + " : DocID :" + storageID;
                    return false;
                }
                webRequester.ContentLength = bytes.Length;
                Stream writer = webRequester.GetRequestStream();
                writer.Write(bytes, 0, bytes.Length);
                writer.Flush();
                writer.Close();
            }
            catch (Exception e)
            {
                isError = true;
                errorMSG = "Document Add aborted:" + sPostURL + ":" + e.Message + " : DocID :" + storageID;
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
                return false;
                
            }
            try
            {
                using (var response = (HttpWebResponse)webRequester.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        _WebResult = sr.ReadToEnd();
                        iDictionary = (IDictionary)JsonReader.Deserialize(_WebResult);
                        if (iDictionary != null)
                        {
                            isError = false;
                            errorMSG = "Document Added : rev ID" + (string)iDictionary["rev"];
                            return true;
                        }
                        else
                        {
                            isError = true;
                            errorMSG = "Add returned with diff response :" + _WebResult;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Doc Add Error : p2" + e.StackTrace + "  " + e.Message + "User " + cvo.userName + " pwd " + cvo.pwd + " URL " +
                          sPostURL);
                string msg = string.Empty;
                // Perform single re-try incase session or access
                if ((DocAddErrReasonAndRetry(e.Message, storageID, out isError, out errorMSG, cvo)) && !isRetry)
                {
                    return DocumentAdd(rawDocument, out isError, out errorMSG, true, cvo, storageID);
                }
                else
                {
                    return false;
                }
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

            return false;
        }

        private bool DocumentAdd(string rawDocument, out bool isError, out string errorMSG, bool isRetry, CouchVo cvo, string storageID)
        {
            isError = false;
            errorMSG = "";
            string _WebResult = "";
            HttpWebRequest webRequester = null;
            string sGetURL = null;
            IDictionary iDictionary = null;
            //log.Error(rawDocument);

            string _connUri = String.Format("http://{0}:{1}/", cvo.serverName, cvo.serverport);
            string _dbName = cvo.dbName;

            string sPostURL = _connUri + _dbName + "/" + storageID;
            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes(rawDocument);
                if (isRetry)
                {
                    webRequester = CreateRequest(sPostURL, _connUri, cvo.userName, cvo.pwd, true);
                }
                else
                {
                    webRequester = CreateRequest(sPostURL, _connUri, cvo.userName, cvo.pwd, false);
                }

                if (webRequester == null)
                {
                    isError = true;
                    errorMSG = "Document Add aborted:" + sPostURL + ":" + "Session failure" + " : DocID :" + storageID;
                    return false;
                }
                webRequester.ContentLength = bytes.Length;
                Stream writer = webRequester.GetRequestStream();
                writer.Write(bytes, 0, bytes.Length);
                writer.Flush();
                writer.Close();
            }
            catch(Exception e)
            {
                isError = true;
                errorMSG = "Document Add aborted:" + sPostURL + ":" + e.Message + " : DocID :" + storageID;
                return false;
            }
            try
            {
                using(var response = (HttpWebResponse)webRequester.GetResponse())
                {
                    using(StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        _WebResult = sr.ReadToEnd();
                        iDictionary = (IDictionary)JsonReader.Deserialize(_WebResult);
                        if (iDictionary != null)
                        {
                            isError = false;
                            errorMSG = "Document Added : rev ID" + (string)iDictionary["rev"];
                            return true;
                        }
                        else
                        {
                            isError = true;
                            errorMSG = "Add returned with diff response :" + _WebResult;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                log.Error("Doc Add Error : p2" + e.StackTrace + "  " + e.Message + "User " + cvo.userName + " pwd " + cvo.pwd + " URL " +
                          sPostURL);
                string msg = string.Empty;
                // Perform single re-try incase session or access
                if ((DocAddErrReasonAndRetry(e.Message, storageID, out isError, out errorMSG, cvo)) && !isRetry)
                {
                    return DocumentAdd(rawDocument, out isError, out errorMSG, true, cvo, storageID);
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        private bool DocAddErrReasonAndRetry(string msg, string storageID, out bool isError, out string errorMsg, CouchVo cvo)
        {
            isError = true;
            if (msg.Contains("404"))
            {
                errorMsg = String.Format("Specified DB '{0}' not found ", cvo.dbName);
                return false;
            }
            else if (msg.Contains("405"))
            {
                errorMsg = String.Format("Specified user '{0}' does not have access to DB '{1}' or session is invalid", cvo.userName,
                                         cvo.dbName);
                return true;
            }
            else if (msg.Contains("409"))
            {
                errorMsg = String.Format("Document with same id '{0}' already exists in DB '{1}'", storageID, cvo.dbName);
                return false;
            }
            else
            {
                //Console.WriteLine("#########################"+msg);
                errorMsg = msg;
                return true;
            }
        }
    }
}