using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CouchConsoleApp.vo

{
    public struct CouchVo : IDisposable
    {
        public string serverName;
        public string serverport;
        public string dbName;
        public string userName;
        public string pwd;
        public string errorMSG;
        public string GeneralMSG;
        public string documentID;
        public bool isError;
        public string fileWithPath;
        public string adminUserName;
        public string adminPwd;

        public void Dispose()
        {
            serverName = null;
            serverport = null;
            userName=null;
            pwd=null;
            errorMSG=null;
            GeneralMSG=null;
            documentID=null;
            fileWithPath=null;
            adminUserName=null;
            adminPwd = null;
        }
    }
}
