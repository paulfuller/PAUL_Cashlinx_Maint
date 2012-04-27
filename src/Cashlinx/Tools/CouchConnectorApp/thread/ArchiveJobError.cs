using System;

namespace CouchConsoleApp.thread
{
    public class ArchiveJobError
    {
        private ErrorType _errorTypeForDoc;
        private String _msg;


        public enum ErrorType
        {
            G,// Get Failed
            A, // Add Failed
            D, // Delete Failed
            O  // Failed due to other exception
        };

        public ErrorType ErrorTypeForDoc
        {
            //set the person name
            set { this._errorTypeForDoc = value; }
            //get the person name 
            get { return this._errorTypeForDoc; }
        }

        public string Msg
        {
            //set the person name
            set { this._msg = value; }
            //get the person name 
            get { return this._msg; }
        }

    }
}
