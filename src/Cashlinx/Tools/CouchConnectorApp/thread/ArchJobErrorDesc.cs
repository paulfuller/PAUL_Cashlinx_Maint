using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CouchConsoleApp.db;

namespace CouchConsoleApp.thread
{
    public sealed class ArchJobErrorDesc
    {
        private const string SOURCE_DOC_NOT_FOUND_PATTERN1 = "Specified document";
        private const string SOURCE_DOC_NOT_FOUND_PATTERN2 = "not found";
        private const string SOURCE_DOC_NOT_FOUND_PATTERN3 = "(404) Not Found";

        Dictionary<string, int> errorDict = new Dictionary<string, int>();

        private static readonly ArchJobErrorDesc archJobErrorDescIns = new ArchJobErrorDesc();
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ArchJobErrorDesc));

        private bool _initSuccess = false;

        public static ArchJobErrorDesc getInstance()
        {
            return archJobErrorDescIns;
        }

        private ArchJobErrorDesc()
        {
            populateInitErrors();
        }

        public bool getStatus()
        {
            return _initSuccess;
        }

        private void populateInitErrors()
        {
            errorDict.Add("Source document not found in db", 1);
            errorDict.Add("General Error", 2);
            ErrorMsgDAO dao = ErrorMsgDAO.getInstance();
            if(dao.AddErrorsToDB(errorDict))
            {
                _initSuccess = true;
            }
        }


        public int getErrorCode(string msg)
        {
            //ErrorMsgDAO dao = ErrorMsgDAO.getInstance();
            int msgID = -1;
            if (msg == null) return msgID;
            if ((msg.Contains(SOURCE_DOC_NOT_FOUND_PATTERN1)) && (msg.Contains(SOURCE_DOC_NOT_FOUND_PATTERN2)))
            {
                return 1;
            }
            else if ((msg.Contains(SOURCE_DOC_NOT_FOUND_PATTERN3)))
            {
                return 1;
            }
            return msgID;
        }
    }
}
