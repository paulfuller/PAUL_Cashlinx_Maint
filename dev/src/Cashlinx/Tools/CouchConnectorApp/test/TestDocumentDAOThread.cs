using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CouchConsoleApp.db;
using CouchConsoleApp.vo;
using log4net;

namespace CouchConsoleApp.test
{
    class TestDocumentDAOThread
    {
        private List<PawnDocRegVO> vo1;
        private bool hasReachedEnd;
        private bool hasErrorOccured;
        private bool _isDataReady;
        private int lastRecord = 0;

        private bool _isDataConsumed;
        private DocumentDAO dao = null;
        private readonly ILog log = LogManager.GetLogger(typeof(TestDocumentDAOThread));

        public TestDocumentDAOThread(int lastRecord)
        {
            vo1 = new List<PawnDocRegVO>();
            _isDataReady = false;
            hasReachedEnd = false;
            hasErrorOccured = false;
            _isDataConsumed = false;
            this.lastRecord = lastRecord;
        }

        public List<PawnDocRegVO> getData()
        {
            return vo1;
        }

        public void setData(List<PawnDocRegVO> vo)
        {
            this.vo1 = vo;
        }

        public void abortExection()
        {
            if(dao!=null)
                dao.killCommand();
        }

        public void execute()
        {
            int pendCount = 0;
            int errorCode = 0;
            string errorMsg = "";
            dao = DocumentDAO.getInstance();
            _isDataReady = false;

            log.Info("Data Fetch thread started....input" + lastRecord);
            try
            {
                dao.GetDocumentSets(lastRecord, out vo1, out pendCount, out errorCode, out errorMsg);
            }catch(Exception e)
            {
                log.Error("Exception in TestDocumentDAOThread"+e.Message);
                hasErrorOccured = true;
                return;
            }finally
            {
                _isDataReady = true;    
            }
            
            if (errorCode == 0)
            {
                hasErrorOccured = false;

                if (vo1.Count == 0)
                {
                    hasReachedEnd = true;
                }
            }else
            {
                hasErrorOccured = true; 
            }
            log.Info("Data Fetch thread Ended....input" + lastRecord + "Record Count " + vo1.Count + "Pending Count:" + pendCount);
        }

        public void executeTempFetch()
        {
            int pendCount = 0;
            int errorCode = 0;
            string errorMsg = "";
            dao = DocumentDAO.getInstance();
            _isDataReady = false;

            log.Info("Data Fetch thread started....input" + lastRecord);
            try
            {
                if (lastRecord == 0)
                {
                    dao.GetTempGetDocsIDs_ForAdd(ref vo1, 20000, true, lastRecord);
                }
                else
                {
                    dao.GetTempGetDocsIDs_ForAdd(ref vo1, 20000, false, lastRecord);
                }
            }
            catch (Exception e)
            {
                log.Error("Exception in TestDocumentDAOThread" + e.Message);
                hasErrorOccured = true;
                return;
            }
            finally
            {
                _isDataReady = true;
            }

            if (errorCode == 0)
            {
                hasErrorOccured = false;

                if (vo1.Count == 0)
                {
                    hasReachedEnd = true;
                }
            }
            else
            {
                hasErrorOccured = true;
            }
            log.Info("Data Fetch thread Ended....input" + lastRecord + "Record Count " + vo1.Count + "Pending Count:" + pendCount);
        }


        public bool HasErrorOccured()
        {
            return this.hasErrorOccured;
        }

        public bool HasReachedEnd()
        {
            return this.hasReachedEnd;
        }

        
        public bool IsDataReady
        {
            set{this._isDataReady = value;}
            get {return this._isDataReady;}
        }

        public bool IsDataConsumed
        {
            //set the person name
            set { this._isDataConsumed = value; }
            //get the person name 
            get { return this._isDataConsumed; }
        }

       
    }
}
