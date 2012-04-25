using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CouchConsoleApp.vo
{
    class PwnDocArchDBRepVO
    {
        private int _Id;
        private string _DBName;
        private string _DBInfo;


        public int Id
        {
            //set the person name
            set { this._Id = value; }
            //get the person name 
            get { return this._Id; }
        }

        public string DBName
        {
            //set the person name
            set { this._DBName = value; }
            //get the person name 
            get { return this._DBName; }
        }

        public string DBInfo
        {
            //set the person name
            set { this._DBInfo = value; }
            //get the person name 
            get { return this._DBInfo; }
        }

    }
}
