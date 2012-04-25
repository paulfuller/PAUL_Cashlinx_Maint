using System;
using System.Net;

namespace Common.Controllers.Database.Couch.CouchSecurity
{
   

    class PawnCouchSecurity
    {
        public static CookieDictionary<string, Cookie> cookiestore = new CookieDictionary<string, Cookie>();

        protected readonly string username;
        protected readonly string password;
        protected readonly string baseUri;
        private const int cookie_timeout = 9;

        public PawnCouchSecurity(string baseUri,string username, string password)
        {
            this.username = username;
            this.password = password;
            this.baseUri = baseUri;
        }

        public bool Authenticate(string baseUri, string userName, string password)
        {
            if (!baseUri.Contains("http://"))
                baseUri = "http://" + baseUri;
            var request = new PawnCouchRequest(baseUri + "/_session");
            request.Timeout = 3000;
            var response = request.Post()
                .ContentType("application/x-www-form-urlencoded")
                .Data("name=" + userName + "&password=" + password)
                .GetResponse();
            return response.StatusCode == HttpStatusCode.OK;
        }

        public Cookie GetSession()
        {
            var authCookie = cookiestore["authcookie"];

            if (authCookie != null)
            {
                return authCookie;
            }
            if (string.IsNullOrEmpty(username)) return null;
            var request = new PawnCouchRequest(baseUri + "_session");
            var response = request.Post()
                .ContentType("application/x-www-form-urlencoded")
                .Data("name=" + username + "&password=" + password)
                .GetResponse();
            if (response != null)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Authentication failed for :" + username);
                }
            }

            if (response != null)
            {
                var header = response.Headers.Get("Set-Cookie");

                if (header != null)
                {
                    var parts = header.Split(';')[0].Split('=');
                    authCookie = new Cookie(parts[0], parts[1]);
                    authCookie.Domain = response.Server;
                    cookiestore.Add("authcookie", authCookie, TimeSpan.FromMinutes(cookie_timeout));
                }

                response.Close();
            }
            return authCookie;
        }
        public PawnCouchRequest GetRequest(string uri,bool newSession)
        {
            //if new session is requested clean the old cookies before new request
            if (newSession)cookiestore.clearAll();
            return GetRequest(uri, null);
        }
        public PawnCouchRequest GetRequest(string uri, string etag)
        {
            Cookie authCookie = null;
            try
            {

                authCookie = GetSession();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error in creating Session:" + ex);
                throw ex;
            }
           

            var request = new PawnCouchRequest(uri, authCookie, etag);
            return request;
        }
    }
}
