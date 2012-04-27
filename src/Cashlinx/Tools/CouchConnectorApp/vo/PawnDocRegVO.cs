using System;

namespace CouchConsoleApp.vo
{
    public class PawnDocRegVO
    {
        private int _Id;
        private int _DocType;
        private string _StorageID;
        private DateTime _Date;
        private string _TargetCouchDBName;
        private string _TargetCouchDBInfo;
        private int _TargetCouchDBID;
        private string _userID="testUSer";
        //private bool _jobSuccess;



        public int DocID
        {
            //set the person name
            set { this._Id = value; }
            //get the person name 
            get { return this._Id; }
        }

        public int DocType
        {
            //set the person name
            set { this._DocType = value; }
            //get the person name 
            get { return this._DocType; }
        }

        public string StorageID
        {
            //set the person name
            set { this._StorageID = value; }
            //get the person name 
            get { return this._StorageID; }
        }

        public DateTime CreationDate
        {
            //set the person name
            set { this._Date = value; }
            //get the person name 
            get { return this._Date; }
        }

        public string TargetCouchDBName
        {
            //set the person name
            set { this._TargetCouchDBName = value; }
            //get the person name 
            get { return this._TargetCouchDBName; }
        }

        public string TargetCouchDBInfo
        {
            //set the person name
            set { this._TargetCouchDBInfo = value; }
            //get the person name 
            get { return this._TargetCouchDBInfo; }
        }

        public string UserID
        {
            //set the person name
            set { this._userID = value; }
            //get the person name 
            get { return this._userID; }
        }
        public int TargetCouchDBID
        {
            //set the person name
            set { this._TargetCouchDBID = value; }
            //get the person name 
            get { return this._TargetCouchDBID; }
        }

        /*public bool JobSuccess
        {
            //set the person name
            set { this._jobSuccess = value; }
            //get the person name 
            get { return this._jobSuccess; }
        }*/
    }
}
