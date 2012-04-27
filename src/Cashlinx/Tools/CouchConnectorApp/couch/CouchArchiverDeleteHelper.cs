using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CouchConsoleApp.vo;
using log4net;


namespace CouchConsoleApp.couch
{
    public class CouchArchiverDeleteHelper
    {
        private static readonly CouchArchiverDeleteHelper inst = new CouchArchiverDeleteHelper();

        private readonly ILog log = LogManager.GetLogger(typeof(CouchArchiverDeleteHelper));

        private static bool setReqTimeout = false;
        private static int reqTimeoutVal = 180000;


        private CouchArchiverDeleteHelper()
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


        public static CouchArchiverDeleteHelper getInstance()
        {
            return inst;
        }

        public bool Document_Delete(out string errTxt, out bool isError, CouchVo vo, string sDocumentId)
        {
            isError = false;
            errTxt = "";

            return DeleteDocument(sDocumentId, false, out isError, out errTxt, vo);
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
            webRequester.Method = "DELETE";
            webRequester.Headers.Add("Accept-Charset", "utf-8");
            webRequester.Headers.Add("Accept-Language", "en-us");
            webRequester.ContentType = "application/json";
            //webRequester.KeepAlive = true;
            if (setReqTimeout) webRequester.Timeout = reqTimeoutVal;
            return webRequester;
        }

        private bool DeleteDocument(string sDocumentId, bool isRetry, out bool sError, out string sErrorText, CouchVo cvo)
        {
            HttpWebRequest _WebRequester = null;
            try
            {
                bool getError = false;
                string getMsg = "";

                IDictionary iDictionary = CouchArchiverGetHelper.getInstance().GetDocFrmCouch(sDocumentId, out getError, out getMsg, cvo);

                if (getError)
                {
                    sError = true;
                    sErrorText = "Deleted Aborted since doc not found " + getMsg;
                    return true;
                }
                string _connUri = String.Format("http://{0}:{1}/", cvo.serverName, cvo.serverport);
                string _dbName = cvo.dbName;

                
                if (iDictionary != null)
                {
                    //DELETE http://localhost:5984/example/some_doc?rev=1582603387
                    string sGetURL = _connUri + _dbName + "/" + sDocumentId + "?rev=" + iDictionary["_rev"];
                    try
                    {
                        if (isRetry)
                        {
                            _WebRequester = CreateRequest(sGetURL, _connUri, cvo.userName, cvo.pwd, true);
                        }
                        else
                        {
                            _WebRequester = CreateRequest(sGetURL, _connUri, cvo.userName, cvo.pwd, false);
                        }

                        if (_WebRequester == null)
                        {
                            sError = true;
                            sErrorText="Document delete aborted:" + sGetURL + ":" + "Session failure" + " : DocID :" + sDocumentId;
                            return false;
                        }
                    }
                    catch(Exception e)
                    {
                        sError = true;
                        sErrorText = "Document delete aborted:" + sGetURL + ":" + e.Message + " : DocID :" + sDocumentId;
                        if (_WebRequester != null)
                        {
                            try
                            {
                                _WebRequester.Abort();
                            }
                            catch (Exception e1)
                            {
                                log.Info("Error aborting webrequst in get document" + e1.StackTrace + " : " + e1.Message);
                            }
                            _WebRequester = null;
                        }
                        return true;
                    }

                    using(HttpWebResponse response = (HttpWebResponse)_WebRequester.GetResponse())
                    {
                        if (CouchArchiverGetHelper.getInstance().DocGetErrAndRetry(response.ResponseUri.ToString(), sDocumentId, out sError, out sErrorText, cvo, false))
                        {
                            if ((!isRetry))
                            {
                                // Console.WriteLine("Doing re-try");
                                return DeleteDocument(sDocumentId, true, out sError, out sErrorText, cvo); //perform re-try since session expired unexpectedly
                            }
                            else
                            {
                                return false; //return error since re-try also failed
                            }
                        }
                        // Get the stream.
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            sError = false;
                            sErrorText = "Document deleted.";
                        }

                    }
                }
                else
                {
                    sError = true;
                    sErrorText = "Specified Document not found,delete aborted.";
                }
            }
            catch(Exception exp)
            {
                sError = true;
                sErrorText = exp.Message;
                return false;
            }finally
            {
                if (_WebRequester != null)
                {
                    try
                    {
                        _WebRequester.Abort();
                    }
                    catch (Exception e1)
                    {
                        log.Info("Error aborting webrequst in get document" + e1.StackTrace + " : " + e1.Message);
                    }
                    _WebRequester = null;
                }
            }
            return true;
        }
    }
}