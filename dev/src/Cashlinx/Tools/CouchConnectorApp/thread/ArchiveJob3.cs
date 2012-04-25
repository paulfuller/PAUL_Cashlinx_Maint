using System;
using System.Diagnostics;
using System.Threading;
using CouchConsoleApp.couch;
using CouchConsoleApp.db;
using CouchConsoleApp.vo;


namespace CouchConsoleApp.thread
{
    public class ArchiveJob3 : IDisposable
    {
        private PawnDocRegVO vo = null;
        //private CouchVo srcCouchVO;
        //private CouchVo targetCouchVO;
        private bool GotDoc = false;
        private bool SavedDoc = false;
        private bool DBTransComp = false;
        private bool SouceDocDeleted = false;
        private bool isRecovery = false;

        private string storageID = "";
        //private string rDocument = null;
        private string exception = "";
        public const char CHAR_CODE_SUCCESS = 'Y';

        private bool criticalError = false;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ArchiveJob3));

        private bool GAD_Error_Occured = false;

        private bool _jobDone = false;

        private string docID;

        private Thread currentThread;

        private bool docMarkedForSuccess = false;
        private char errorCodeChar;
        private string errorMessage;
        private int errorCode;

        public string getErrorMessageForDB()
        {
            return errorMessage;
        }

        public char getErrorCodeChar()
        {
            return errorCodeChar;
        }

        public bool IsDocMarkedForSuccess()
        {
            return docMarkedForSuccess;
        }

        public PawnDocRegVO getDocObject()
        {
            return vo;
        }

        public int getErrorCode()
        {
            return errorCode;
        }

        public Thread JobThread
        {
            //set the person name
            set { this.currentThread = value; }
            //get the person name 
            get { return this.currentThread; }
        }

        /*public ArchiveJob(PawnDocRegVO vo, CouchVo srcCouchVO, CouchVo targetCouchVO, bool isRecovery)
        {
            this.vo = vo;
            this.srcCouchVO = srcCouchVO;
            this.targetCouchVO = targetCouchVO;
            this.isRecovery = isRecovery;
        }*/

        public ArchiveJob3(PawnDocRegVO vo, bool isRecovery)
        {
            this.vo = vo;
            this.isRecovery = isRecovery;
            docID = vo.DocID.ToString();
        }

        /*public vo.PawnDocRegVO getVO()
        {
            return this.vo;
        }*/

        public string getDocID()
        {
            return docID;
        }

        public bool isJobSuccess()
        {
            return DBTransComp;
        }

        public bool criticalErrorState()
        {
            return criticalError;
        }


        public bool JobEnd
        {
            //set the person name
            set { this._jobDone = value; }
            //get the person name 
            get { return this._jobDone; }
        }


        public void process()
        {
            var totalTime = new Stopwatch();
            var timeForexec = new Stopwatch();
            long getTime = 0;
            long addTime = 0;
            long delTime = 0;
            long finalUpdate = 0;
            JobEnd = false;
            string rDocument = null;
            try
            {
                totalTime.Start();
                timeForexec.Start();
                rDocument = getDocumentFromCouch();
                timeForexec.Stop();
                getTime = timeForexec.ElapsedMilliseconds;
                if (!GotDoc)
                {
                    docMarkedForSuccess = false;
                    updateForCriticalError(ArchiveJobError.ErrorType.G);
                    return;
                }
                timeForexec.Restart();
                AddDocumentToCouch(rDocument);
                rDocument = null;
                timeForexec.Stop();
                addTime = timeForexec.ElapsedMilliseconds;
                if (!SavedDoc)
                {
                    updateForCriticalError(ArchiveJobError.ErrorType.A);
                    return;
                }
                timeForexec.Restart();
                SouceDocDeleted = true;
                //delete stubbed
                deleteDocFromSource();
                timeForexec.Stop();
                delTime = timeForexec.ElapsedMilliseconds;
                if (!SouceDocDeleted)
                {
                    updateForCriticalError(ArchiveJobError.ErrorType.D);
                    return;
                }
                timeForexec.Restart();
                this.docMarkedForSuccess = true;
                //updateTablesForSuccess();
                timeForexec.Stop();
                finalUpdate = timeForexec.ElapsedMilliseconds;
                //Thread.Sleep(10);
            }
            catch (Exception e)
            {
                log.Error("Exception occured in Archive Job1 :" + e.Message);
            }
            finally
            {

                string str1 = string.Format("Doc ID {0} Storage ID {1} Target DBID {2} Got DOC {3} Added DOC {4} Delete DOC {5} Updated {6}",
                                     vo.DocID, vo.StorageID, vo.TargetCouchDBName, GotDoc, SavedDoc, SouceDocDeleted, DBTransComp);
                string str2 = string.Format("Doc ID {0} Get Time {1} add time {2} del time {3} final update {4}", vo.DocID, getTime, addTime, delTime,
                                            finalUpdate);
                //log.Debug("Total time: " + totalTime.ElapsedMilliseconds + " : " + str1);


                if (GAD_Error_Occured)
                {
                    log.Error("<<<<Total time: " + totalTime.ElapsedMilliseconds + " : " + str1 + ">>>");
                }
                rDocument = null;
                //for object clear
                if (totalTime != null)
                    totalTime.Stop();
                if (timeForexec != null)
                    timeForexec.Stop();
                totalTime = null;
                timeForexec = null;
                //this.vo = null;
                JobEnd = true;
            }
        }

        public string getDocumentFromCouch()
        {
            string rDocument = null;
            try
            {
                bool error;
                string retMSG;
                rDocument = CouchArchiverGetHelper.getInstance().GetRawDocument(out retMSG, out error, JobAllocationHandler5.getSourceCouch(), vo);
                if (error)
                {
                    GotDoc = false;
                }
                else
                {
                    GotDoc = true;
                }
                exception = retMSG;
            }
            catch (Exception e)
            {
                GotDoc = false;
                exception = e.Message;
                log.Error(string.Format("Get Doc failed for {0}, archive skipped for {1}", vo.StorageID, vo.DocID), e);
            }
            return rDocument;
        }

        public void AddDocumentToCouch(string rDocument)
        {
            try
            {
                bool error;
                string retMSG;
               // this.targetCouchVO.dbName = vo.TargetCouchDBName;
                SavedDoc = CouchArchiverAddHelper.getInstance().Document_Add(rDocument, out error, out retMSG, JobAllocationHandler5.getTargetCouch(), vo.StorageID, vo.TargetCouchDBName);
                this.storageID = vo.StorageID;
                if (!SavedDoc)
                {
                    if (retMSG != null && retMSG.Contains("already exists"))
                    {
                        SavedDoc = true;
                    }
                    log.Error("Add Document failed:" + retMSG);
                }

            }
            catch (Exception e)
            {
                log.Error(string.Format("Save Doc failed for {0} target db{1}", vo.StorageID, vo.TargetCouchDBName), e);
                SavedDoc = false;
                exception = e.Message;
            }
            finally
            {
                rDocument = null;
            }
        }

        public void deleteDocFromSource()
        {
            try
            {
                bool error;
                string retMSG;
                SouceDocDeleted = CouchArchiverDeleteHelper.getInstance().Document_Delete(out retMSG, out error, JobAllocationHandler5.getSourceCouch(), vo.StorageID);

            }
            catch (Exception e)
            {
                log.Error(string.Format("Document delete failed for {0} source db{1}", storageID, JobAllocationHandler5.getSourceCouch().dbName), e);
                SouceDocDeleted = false;
            }
        }

        public void updateTablesForSuccess()
        {
            try
            {
                if (PwnArchLogDAO.getInstance().CreateArchLog(vo, CHAR_CODE_SUCCESS, isRecovery))
                {
                    DBTransComp = true;
                }
                else
                {
                    criticalError = true;
                    DBTransComp = false;
                }
            }
            catch (Exception e)
            {
                log.Error("updateTablesForSuccess Exception", e);
            }
        }

        public void updateForCriticalError(ArchiveJobError.ErrorType errorType)
        {
            //string errorMsg = "";
            this.docMarkedForSuccess = false;
            errorCode = ArchJobErrorDesc.getInstance().getErrorCode(exception);
            bool success = false;
            GAD_Error_Occured = true;
            if (errorType == ArchiveJobError.ErrorType.G)
            {
                this.errorMessage = generateErrorMessage("Source Doc not found:");
                errorCodeChar = 'G';
                /*success = PwnArchLogDAO.getInstance().CreateArchLogWithError(vo, errorCode, errorMsg, 'G', isRecovery);
                if (!success)
                {
                    this.criticalError = true;
                }*/

                //make db call to update pawn doc reg
            }
            else if (errorType == ArchiveJobError.ErrorType.A)
            {
                this.errorMessage = generateErrorMessage("Add doc to target failed:");
                errorCodeChar = 'A';
                /*success = PwnArchLogDAO.getInstance().CreateArchLogWithError(vo, errorCode, errorMsg, 'A', isRecovery);
                if (!success)
                {
                    this.criticalError = true;
                }*/
            }
            else if (errorType == ArchiveJobError.ErrorType.D)
            {
                this.errorMessage = generateErrorMessage("Delete source failed:");
                errorCodeChar = 'D';
                /*success = PwnArchLogDAO.getInstance().CreateArchLogWithError(vo, errorCode, errorMsg, 'D', isRecovery);
                if (!success)
                {
                    this.criticalError = true;
                }*/
            }
            else
            {
                this.errorMessage = generateErrorMessage("Archive failed:");
                this.criticalError = true;
            }
        }

        private string generateErrorMessage(string msgPrefix)
        {
            string errorStr = "No info available";

            if (!string.IsNullOrEmpty(exception))
            {
                exception = msgPrefix + exception;

                if (exception.Length > 256)
                {
                    errorStr = exception.Substring(0, 256);
                }
                else
                {
                    errorStr = exception;
                }
            }
            return errorStr;
        }

        public void Dispose()
        {
            this.JobThread = null;
            this.vo = null;
            this.storageID = null;
            this.log = null;
            this.exception = null;

        }
    }
}