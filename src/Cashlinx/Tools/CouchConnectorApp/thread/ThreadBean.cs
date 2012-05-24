using System.Threading;

namespace CouchConsoleApp.thread
{
    class ThreadBean
    {
        private Thread _threadObj;
        private int _docID;
        private ArchiveJob _job;
        
        public Thread ThreadObj
        {
            //set the person name
            set { this._threadObj = value; }
            //get the person name 
            get { return this._threadObj; }
        }

        public int DocumentID
        {
            //set the person name
            set { this._docID = value; }
            //get the person name 
            get { return this._docID; }
        }

        public ArchiveJob Job
        {
            //set the person name
            set { this._job = value; }
            //get the person name 
            get { return this._job; }
        }
    }
}
