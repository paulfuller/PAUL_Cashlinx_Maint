using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using log4net;

namespace CouchConsoleApp.couch
{
    class PawnCouchConcurrentAccessSecurity
    {
        private static ConcurrentCookieDictionary<string, Cookie> cookiestore = new ConcurrentCookieDictionary<string, Cookie>();

        protected readonly string username;
        protected readonly string password;
        protected readonly string baseUri;
        private const int cookie_timeout = 9;
        private readonly ILog log = LogManager.GetLogger(typeof(PawnCouchConcurrentAccessSecurity));

        private static object lockingObject = new Object();
       // private static object lockingObject1 = new Object();

        private static bool setReqTimeout = false;
        private static int reqTimeoutVal = 180000;

        public PawnCouchConcurrentAccessSecurity(string baseUri, string username, string password)
        {
            this.username = username;
            this.password = password;
            this.baseUri = baseUri;

            if (Properties.Settings.Default.Override_Couch_Request_TimeOut &&
               Properties.Settings.Default.Couch_Request_Timeout > 180000)
            {
                setReqTimeout = true;
                reqTimeoutVal = Properties.Settings.Default.Couch_Request_Timeout;
                log.Info("Couch Request timeout overriden :" + reqTimeoutVal);
            }
            else
            {
                log.Info("Couch Request timeout not overriden :" + reqTimeoutVal);
            }
        }

        public PawnCouchConcurrentAccessSecurity()
        {
            if (Properties.Settings.Default.Override_Couch_Request_TimeOut &&
               Properties.Settings.Default.Couch_Request_Timeout > 18000)
            {
                setReqTimeout = true;
                reqTimeoutVal = Properties.Settings.Default.Couch_Request_Timeout;
                log.Info("Couch Request timeout overriden :" + reqTimeoutVal);
            }
            else
            {
                log.Info("Couch Request timeout overriden :" + reqTimeoutVal);
            }
        }


        private HttpWebRequest CreateSessionRequest(string sGetURL)
        {
            var webRequester = (HttpWebRequest)WebRequest.Create(sGetURL);
            webRequester.Method = "POST";
            webRequester.Headers.Add("Accept-Charset", "utf-8");
            webRequester.Headers.Add("Accept-Language", "en-us");
            webRequester.ContentType = "application/x-www-form-urlencoded";
            //webRequester.KeepAlive = true;
            if (setReqTimeout) webRequester.Timeout = reqTimeoutVal;
            return webRequester;
        }
        public Cookie GetSession(bool newCookie)
        {
            Stopwatch sw=new Stopwatch();
            sw.Start();
            Cookie cook =GetSessionInternal(newCookie);
            sw.Stop();
            //log.Error("Time to get session:"+sw.ElapsedMilliseconds);
            return cook;
        }
        public Cookie GetSessionInternal(bool newCookie) 
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }

            lock(lockingObject)
            {
                if (newCookie && cookiestore.size() > 0)
                {
                    cookiestore.removeCookie("authcookie" + "_" + username + "_" + baseUri);
                    log.Error("Cookie removal explicit :" + "authcookie" + "_" + username + "_" + baseUri);
                }
                else
                {

                    var authCookie = cookiestore["authcookie" + "_" + username + "_" + baseUri];
                    if (authCookie != null)
                    {
                        //log.Error("Got Cookie from cache :" + authCookie + " User " + username);
                        return authCookie;
                    }
                 
                }
                HttpWebRequest request = null;
                try
                {
                    request = CreateSessionRequest(baseUri + "_session");
                    using(var body = request.GetRequestStream())
                    {
                        var encodedData = Encoding.UTF8.GetBytes("name=" + username + "&password=" + password);
                        body.Write(encodedData, 0, encodedData.Length);

                    }
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new Exception("Authentication failed for :" + username);
                        }

                        var header = response.Headers.Get("Set-Cookie");

                        if (header != null)
                        {
                            var parts = header.Split(';')[0].Split('=');
                            Cookie authCookie = new Cookie(parts[0], parts[1]);
                            authCookie.Domain = response.Server;
                            log.Error("Added Cookie to cache :" + authCookie + " User " + username);
                            cookiestore.Add("authcookie" + "_" + username + "_" + baseUri, authCookie, TimeSpan.FromMinutes(cookie_timeout));
                            return authCookie;
                        }

                        response.Close();
                    }
                    
                }catch(Exception e)
                {
                    log.Error(string.Format("Error getting session for user{0} base url {1} exception {2} trace {3}",
                    username,baseUri,e.Message,e.StackTrace));
                    return null;
                }finally
                {
                    if (request != null)
                    {
                        try
                        {
                            request.Abort();
                        }
                        catch (Exception e1)
                        {
                            log.Info("Error aborting webrequst in GetSessionInternal document" + e1.StackTrace + " : " + e1.Message);
                        }
                        request = null;
                    }
                }
                return null;
            }//End of lock
        }
    }
}
