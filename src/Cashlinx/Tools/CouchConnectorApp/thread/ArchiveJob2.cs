using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using CouchConsoleApp.couch;
using CouchConsoleApp.db;
using CouchConsoleApp.vo;


namespace CouchConsoleApp.thread
{
    public class ArchiveJob2 : IDisposable
    {
        //private PawnDocRegVO vo = null;
        //private CouchVo srcCouchVO;
        //private CouchVo targetCouchVO;
        private bool GotDoc = false;
        private bool SavedDoc = false;
        //private bool DBTransComp = false;
        private bool SouceDocDeleted = false;
        private bool isRecovery = false;

        //private string storageID = "";
        //private string rDocument = null;
        private string exception = "";
        public const char CHAR_CODE_SUCCESS = 'Y';

        private bool criticalError = false;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ArchiveJob2));

        private bool GAD_Error_Occured = false;

        private bool _jobDone = false;

        private string docID;

        private Thread currentThread;

        private List<PawnDocRegVO> voList;

        private int _successCount = 0;
        private int _failCount = 0;

        private int totalJobCount = 0;


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

        /*public ArchiveJob2(PawnDocRegVO vo, bool isRecovery)
        {
            this.vo = vo;
            this.isRecovery = isRecovery;
            docID = vo.DocID.ToString();
        }*/



        public ArchiveJob2(List<PawnDocRegVO> voListNew, bool isRecovery)
        {
            this.voList=new List<PawnDocRegVO>();
            voList.AddRange(voListNew);
            this.isRecovery = isRecovery;
            totalJobCount = voListNew.Count;
            //docID = vo.DocID.ToString();
        }

        /*public vo.PawnDocRegVO getVO()
        {
            return this.vo;
        }*/

        public string getDocID()
        {
            return docID;
        }

       /* public bool isJobSuccess()
        {
            return DBTransComp;
        }*/

        public bool criticalErrorState()
        {
            return criticalError;
        }

        public int getCompletedCount()
        {
           return totalJobCount;
        }

        public bool JobEnd
        {
            //set the person name
            set { this._jobDone = value; }
            //get the person name 
            get { return this._jobDone; }
        }

        public int SucessCount
        {
            //set the person name
            set { this._successCount = value; }
            //get the person name 
            get { return this._successCount; }
        }
        public int FailCount
        {
            //set the person name
            set { this._failCount = value; }
            //get the person name 
            get { return this._failCount; }
        }

        public void process()
        {
            var totalTime = new Stopwatch();
            var timeForexec = new Stopwatch();
            long getTime = 0;
            long addTime = 0;
            long delTime = 0;
            long finalUpdate = 0;
            string rDocument = null;
            JobEnd = false;
            try
            {
                totalTime.Start();
                foreach (var pawnDocRegVO in voList)
                {
                    this.docID = pawnDocRegVO.DocID.ToString();
                    timeForexec.Start();
                    rDocument = getDocumentFromCouch(pawnDocRegVO);
                    timeForexec.Stop();
                    getTime = timeForexec.ElapsedMilliseconds;
                    if (!GotDoc)
                    {
                        updateDBForError(ArchiveJobError.ErrorType.G, pawnDocRegVO);
                        log.Info("GetError :Total time taken :" + getTime + ": storage ID :" + pawnDocRegVO.StorageID);
                        return;
                    }
                    timeForexec.Restart();
                    AddDocumentToCouch(rDocument, pawnDocRegVO);
                    rDocument = null;
                    timeForexec.Stop();
                    addTime = timeForexec.ElapsedMilliseconds;
                    if (!SavedDoc)
                    {
                        updateDBForError(ArchiveJobError.ErrorType.A, pawnDocRegVO);
                        log.Info("AddError :Total time taken :" + addTime + ": storage ID :" + pawnDocRegVO.StorageID + " : TARGET DB :" + pawnDocRegVO.TargetCouchDBName);
                        return;
                    }
                    timeForexec.Restart();
                    SouceDocDeleted = true;
                    //delete stubbed
                    deleteDocFromSource(pawnDocRegVO);
                    timeForexec.Stop();
                    delTime = timeForexec.ElapsedMilliseconds;
                    if (!SouceDocDeleted)
                    {
                        updateDBForError(ArchiveJobError.ErrorType.D, pawnDocRegVO);
                        log.Info("DelError :Total time taken :" + delTime + ": storage ID :" + pawnDocRegVO.StorageID + " : TARGET DB :" + pawnDocRegVO.TargetCouchDBName);
                        return;
                    }
                    timeForexec.Restart();
                    updateTablesForSuccess(pawnDocRegVO);
                    timeForexec.Stop();
                    finalUpdate = timeForexec.ElapsedMilliseconds;
                    if (criticalError == true)
                    {
                        log.Error("Critical Error notified during doc :" + docID + ": Storage ID :" + pawnDocRegVO.StorageID);
                        break;
                    }
                }
                //Thread.Sleep(10);
            }
            catch (Exception e)
            {
                log.Error("Exception occured in Archive Job1 :" + e.Message);
            }
            finally
            {
                log.Info(string.Format("Total time for processing {0} documents is {1} Msec", voList.Count, totalTime.ElapsedMilliseconds));
                if (totalTime != null)
                    totalTime.Stop();
                if (timeForexec != null)
                  timeForexec.Stop();
               totalTime = null;
               timeForexec = null;
               this.voList = null;
               JobEnd = true;
            }
        }

        public string getDocumentFromCouch(PawnDocRegVO pawnDocRegVO)
        {
            string rDocument = null;
            try
            {
                bool error;
                string retMSG;
                rDocument = CouchArchiverGetHelper.getInstance().GetRawDocument(out retMSG, out error, JobAllocationHandler3.getSourceCouch(), pawnDocRegVO);
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
                log.Error(string.Format("Get Doc failed for {0}, archive skipped for {1}", pawnDocRegVO.StorageID, pawnDocRegVO.DocID), e);
            }
            return rDocument;
        }

        public void AddDocumentToCouch(string rDocument, PawnDocRegVO pawnDocRegVO)
        {
            try
            {
                bool error;
                string retMSG;
                //  this.targetCouchVO.dbName = vo.TargetCouchDBName;
                SavedDoc = CouchArchiverAddHelper.getInstance().Document_Add(rDocument, out error, out retMSG,
                JobAllocationHandler3.getTargetCouch(), pawnDocRegVO.StorageID, pawnDocRegVO.TargetCouchDBName);
                //this.storageID = vo.StorageID;
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
                log.Error(string.Format("Save Doc failed for {0} target db{1}", pawnDocRegVO.StorageID, pawnDocRegVO.TargetCouchDBName), e);
                SavedDoc = false;
                exception = e.Message;
            }
            finally
            {
                rDocument = null;
            }
        }

        public void deleteDocFromSource(PawnDocRegVO pawnDocRegVO)
        {
            try
            {
                bool error;
                string retMSG;
                SouceDocDeleted = CouchArchiverDeleteHelper.getInstance().Document_Delete(out retMSG, out error, JobAllocationHandler3.getSourceCouch(), pawnDocRegVO.StorageID);

            }
            catch (Exception e)
            {
                log.Error(string.Format("Document delete failed for {0} source db{1}", pawnDocRegVO.StorageID, JobAllocationHandler3.getSourceCouch().dbName), e);
                SouceDocDeleted = false;
            }
        }

        public void updateTablesForSuccess(PawnDocRegVO pawnDocRegVO)
        {
            try
            {
                if (PwnArchLogDAO.getInstance().CreateArchLog(pawnDocRegVO, CHAR_CODE_SUCCESS, isRecovery))
                {
                    //pawnDocRegVO.JobSuccess= true;
                    _successCount++;
                }
                else
                {
                    criticalError = true;
                   // pawnDocRegVO.JobSuccess = false;
                    //_failCount++;
                }
            }
            catch (Exception e)
            {
                log.Error("updateTablesForSuccess Exception", e);
            }
        }

        public void updateDBForError(ArchiveJobError.ErrorType errorType, PawnDocRegVO pawnDocRegVO)
        {
            string errorMsg = "";
            int errorCode = ArchJobErrorDesc.getInstance().getErrorCode(exception);
            bool success = false;
            GAD_Error_Occured = true;
            _failCount++;
            if (errorType == ArchiveJobError.ErrorType.G)
            {
                errorMsg = generateErrorMessage("Source Doc not found:");
                success = PwnArchLogDAO.getInstance().CreateArchLogWithError(pawnDocRegVO, errorCode, errorMsg, 'G', isRecovery);
                if (!success)
                {
                    this.criticalError = true;
                }

                //make db call to update pawn doc reg
            }
            else if (errorType == ArchiveJobError.ErrorType.A)
            {
                errorMsg = generateErrorMessage("Add doc to target failed:");
                success = PwnArchLogDAO.getInstance().CreateArchLogWithError(pawnDocRegVO, errorCode, errorMsg, 'A', isRecovery);
                if (!success)
                {
                    this.criticalError = true;
                }
            }
            else if (errorType == ArchiveJobError.ErrorType.D)
            {
                errorMsg = generateErrorMessage("Delete source failed:");
                success = PwnArchLogDAO.getInstance().CreateArchLogWithError(pawnDocRegVO, errorCode, errorMsg, 'D', isRecovery);
                if (!success)
                {
                    this.criticalError = true;
                }
            }
            else
            {
                errorMsg = generateErrorMessage("Archive failed:");
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
            this.voList = null;
            this.docID = null;
            this.log = null;
            this.exception = null;

        }
    }
}