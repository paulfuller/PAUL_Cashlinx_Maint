using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Common.Controllers.Database.Couch.Impl;
using CouchConsoleApp.vo;
using JsonFx.Json;
using log4net;


namespace CouchConsoleApp.couch
{
    internal class SecuredCouchArchiver : SecuredCouchConnector
    {
        private readonly ILog log = LogManager.GetLogger(typeof(SecuredCouchArchiver));
        protected readonly string _userId = String.Empty;
        protected readonly string _password = String.Empty;
        protected readonly string _dbName = String.Empty;
        protected readonly string _connUri = string.Empty;

        public SecuredCouchArchiver(string sWebDomain, string sWebPort, string secWebPort, string sCouchDb, string userId, string password,
                                    bool connSecured) : base(sWebDomain, sWebPort, secWebPort, sCouchDb, userId, password, connSecured)
        {
        }

        public bool createArchDB(string dbName, CouchVo cvo)
        {
            return _createArchDB(dbName,cvo, false);
        }

       

        public bool secureArchDB(string dbName, CouchVo vo)
        {
            //log.Debug(string.Format("Conn URI {0} DB name {1} user id {2} pwd {3} ", _connUri, _dbName, base._userId, base._password));
            //log.Debug("DB name to create :" + dbName);
            return _secureArchDB(dbName, false, vo);
        }

        public bool deleteDB(string dbName, CouchVo vo)
        {
            return _deleteDB(dbName,vo, false);
        }

        public bool Authenticate(CouchVo cvo, string user, string pwd, out string errorMessage)
        {
            WebRequest request = null;
            HttpWebResponse response = null;
            errorMessage = "";
            bool authenticated = false;
            try
            {
                string url = string.Format("http://{0}:{1}", cvo.serverName, cvo.serverport);
                request = WebRequest.Create(url + "/_session");
                request.Timeout = 10000;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                using(var body = request.GetRequestStream())
                {
                    var encodedData = Encoding.UTF8.GetBytes("name=" + user + "&password=" + pwd);
                    body.Write(encodedData, 0, encodedData.Length);
                }
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        log.Debug("Auth Success..." + user);
                        authenticated = true;
                    }
                    else
                    {
                        log.Debug("Auth Failed..." + user);
                        log.Error(string.Format("Authentication failure server {0}, port {1}, user {2} admin user {3}", cvo.serverName,
                                                cvo.serverport, cvo.userName, cvo.adminUserName));
                        authenticated = false;
                    }
                }
            }
            catch(Exception e)
            {
                log.Error(string.Format("Authentication failure server {0}, port {1}, user {2} admin user {3}",
                    cvo.serverName,cvo.serverport,cvo.userName,cvo.adminUserName), e);
                errorMessage = e.Message;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return authenticated;
        }

        private bool _secureArchDB(string dbName, bool isRetry, CouchVo vo)
        {
            HttpWebRequest webRequester = null;
            HttpWebResponse webResponser = null;
            string baseURL = String.Format("http://{0}:{1}/", vo.serverName, vo.serverport);
            string sPostUrl = baseURL + dbName + "/" + "_security";
            string _WebResult = "";
            List<string> users = new List<string>();
            users.Add(vo.userName);

            string sJSON = CouchExprHelper.getInstance().adminSecExpr(null, null, users, null);
            //log.Debug(sJSON);
            byte[] bytes = Encoding.ASCII.GetBytes(sJSON);

            try
            {
                if (isRetry)
                {
                    webRequester = CreateRequest(sPostUrl, baseURL, vo.adminUserName, vo.adminPwd,"PUT",true);
                }
                else
                {
                    webRequester = CreateRequest(sPostUrl, baseURL, vo.adminUserName, vo.adminPwd,"PUT",false);
                }
                if (webRequester == null)
                {
                    return false;
                }
                try
                {
                    webRequester.ContentLength = bytes.Length;
                    Stream writer = webRequester.GetRequestStream();
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Flush();
                    writer.Close();

                    using (webResponser = (HttpWebResponse)webRequester.GetResponse())
                    {
                        using(var sr = new StreamReader(webResponser.GetResponseStream()))
                        {
                            _WebResult = sr.ReadToEnd();
                            this.Message = "Permission added successfully :" + _WebResult;
                            this.Error = false;
                            return true;
                        }
                    }
                }
                catch(Exception ex)
                {
                    log.Error(ex.Message);

                    if (isRetry)
                    {
                        this.Message = string.Format("Failed to add Permission to db {0} due to {1}", dbName, ex.Message);
                        return false;
                    }
                    else
                    {
                        return _secureArchDB(dbName, true, vo);
                    }
                }
            }
            catch(Exception e)
            {
                log.Error("Failed to add Permission to db", e);
                Error = true;
                this.Message = string.Format("Failed to add Permission to db {0} due to {1}", dbName, e.Message);
                log.Debug(e.StackTrace);
                return false;
            }
        }

        private HttpWebRequest CreateRequest(string sPostUrl, string baseURL, string _userId,
            string _password,string method, bool isNewCookie)
        {
            var sec = new PawnCouchConcurrentAccessSecurity(baseURL, _userId, _password);
            var webRequester = (HttpWebRequest)WebRequest.Create(sPostUrl);
            if(sec.GetSession(isNewCookie)==null)
            {
                log.Error("DB Creation/DB Secure failed due to session failure");
                log.Error("User:" + _userId + " : post url" + sPostUrl);
                return null;
            }
            webRequester.Headers.Add("Cookie", "AuthSession=" + sec.GetSession(isNewCookie).Value);
            webRequester.Method = method;
            webRequester.Headers.Add("Accept-Charset", "utf-8");
            webRequester.Headers.Add("Accept-Language", "en-us");
            webRequester.ContentType = "application/json";
            webRequester.KeepAlive = true;
            return webRequester;
        }
       
        private bool _createArchDB(string dbName, CouchVo cVO, bool isRetry)
        {
            HttpWebRequest webRequester = null;
            HttpWebResponse webResponser = null;
            string baseURL = String.Format("http://{0}:{1}/", cVO.serverName, cVO.serverport);
            string sPostUrl = baseURL + dbName;
            string _WebResult = "";

            try
            {
                if (isRetry)
                {
                    webRequester = CreateRequest(sPostUrl, baseURL, cVO.adminUserName, cVO.adminPwd,"PUT",true);
                    
                }
                else
                {
                    webRequester = CreateRequest(sPostUrl, baseURL, cVO.adminUserName, cVO.adminPwd,"PUT",false);
                    
                }
                if (webRequester == null)
                {
                    return false;
                }

                try
                {
                    using (webResponser = (HttpWebResponse)webRequester.GetResponse())
                    {
                        using(var sr = new StreamReader(webResponser.GetResponseStream()))
                        {
                            _WebResult = sr.ReadToEnd();
                            this.Message = "DB Created :" + _WebResult;
                            this.Error = false;
                            return true;
                        }
                    }
                }
                catch(Exception ex)
                {
                    if (webResponser != null)
                    {
                        webResponser.Close();
                    }

                    log.Error(ex.StackTrace);
                    log.Error(ex.Message);

                    if (isRetry)
                    {
                        string str1 = "";
                        if (webRequester != null && webRequester.Headers != null)
                        {
                            str1 = webRequester.Headers.ToString();
                        }
                        log.Error("User:" + _userId + " : pwd" + _password + " Header :" + str1);
                        this.Message = string.Format("Failed to add db {0} due to {1}", dbName, ex.Message);
                        return false;
                    }
                    if (ex.Message.Contains("412"))
                    {
                        //log.Info(string.Format("DB {0} Already Exists", dbName));
                        this.Message = string.Format("DB {0} Already Exists", dbName);
                        this.Error = false;
                        return true;
                    }
                    else
                    {
                        return _createArchDB(dbName,cVO,isRetry);
                    }
                }
            }
            catch(Exception e)
            {
                log.Error("createArchDB Error:", e);
                Error = true;
                this.Message = string.Format("Failed to add db {0} due to {1}", dbName, e.Message);
                log.Debug(e.StackTrace);
                return false;
            }

            return false;
        }

        private bool _deleteDB(string dbName, CouchVo cVO, bool isRetry)
        {
            HttpWebRequest webRequester = null;
            string baseURL = String.Format("http://{0}:{1}/", cVO.serverName, cVO.serverport);
            string sPostUrl = baseURL + dbName;
            string _WebResult = "";

            try
            {
                if (isRetry)
                {
                    webRequester = CreateRequest(sPostUrl, baseURL, cVO.adminUserName, cVO.adminPwd, "DELETE", true);
                }
                else
                {
                    webRequester = CreateRequest(sPostUrl, baseURL, cVO.adminUserName, cVO.adminPwd, "DELETE", false);
                }

                try
                {
                    _WebResult = readResponse(webRequester);
                    this.Error = false;
                    this.Message = "DeleteDB Success" + _WebResult;
                    return true;
                }
                catch(Exception ex)
                {
                    log.Error(ex.Message);

                    if (isRetry)
                    {
                        this.Message = string.Format("Failed to DeleteDB db {0} due to {1}", dbName, ex.Message);
                        return false;
                    }
                    return _deleteDB(dbName,cVO, true);
                }
            }
            catch(Exception e)
            {
                log.Error("deleteDB Error:", e);
                Error = true;
                this.Message = string.Format("Failed to deleteDB db {0} due to {1}", dbName, e.Message);
                log.Debug(e.StackTrace);
                return false;
            }

            return false;
        }

        private string readResponse(HttpWebRequest webRequester)
        {
            HttpWebResponse webResponser;
            string _WebResult;
            webResponser = (HttpWebResponse)webRequester.GetResponse();
            //Check out the html.    

            using(var sr = new StreamReader(webResponser.GetResponseStream()))
            {
                _WebResult = sr.ReadToEnd();
                return _WebResult;
            }
        }
    }
}