using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CouchConsoleApp.couch;
using CouchConsoleApp.db;
using CouchConsoleApp.events;
using CouchConsoleApp.form;
using CouchConsoleApp.vo;
using MainForm;

namespace CouchConsoleApp.thread
{
    class JobAllocationHandler
    {
        private ArchProcessForm mainForm = null;
        private BackgroundWorker worker = null;
        private DoWorkEventArgs e = null;
        private BgWorkerJobAllocationEventHandler _jobAllocationEventHandler=null;
        private List<ThreadBean> runningList = null;
        private List<string> completedList = null;
        private List<string> erroredList = null;
        private int fetchCount = 20000;
        private int totalCount = 0;
        private int archDocIndex = 0;
        private int archLogID = 0;
        private bool isRecovery = false;
        private bool isRecoveryOnceExecuted = false;
        private bool isCancelPressed = false;
        private bool criticalErrorOccured = false;
        // Current wait time 1 min before timeout for threads
        private int Timeout = 60000;
        Dictionary<string, PwnDocArchDBRepVO> dbDictionary = new Dictionary<string, PwnDocArchDBRepVO>();
       
        
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(JobAllocationHandler));

     

        private int noOfThreads = 100;


        private int successCount = 0;
        private int failedCount = 0;

        private int tempSoFarCount = 0;

        //duplicate ell
        //private int duplCount=0;
        //private Dictionary<int, string> filteredDocDict = new Dictionary<int, string>();
        //duplicate ell
        public JobAllocationHandler(ArchProcessForm mainForm, BackgroundWorker worker,
            DoWorkEventArgs e, BgWorkerJobAllocationEventHandler _jobAllocationEventHandler, int totalRecords,bool isRecovery)
        {
            this.mainForm = mainForm;
            this.worker = worker;
            this.e = e;
            this._jobAllocationEventHandler = _jobAllocationEventHandler;
            archDocIndex = 0;
            this.totalCount = totalRecords;
            this.isRecovery = isRecovery;
        }

        public void process()
        {
            log.Info("WorkAllocationThread process called..");
            completedList=new List<string>();
            runningList=new List<ThreadBean>();
            erroredList=new List<string>();
            isRecoveryOnceExecuted = false; //set this flag for recovery to fetch only once
            bool isSuccess;
            string msg;
            createArchLog(out isSuccess, out msg);
            if (!isSuccess)
            {
                log.Error("Create Arch Log Failed, please check the log file :" + msg);
                return;
            }
            Execute();
            worker.ReportProgress(computePercent(this.completedList.Count(), totalCount),
                                    string.Format("{0}:{1}:{2}", totalCount, completedList.Count(),
                                                  (totalCount - completedList.Count())));
            //duplicate count
            /*worker.ReportProgress(computePercent(this.completedList.Count() + duplCount, totalCount),
                                    string.Format("{0}:{1}:{2}", totalCount, completedList.Count() + this.duplCount,
                                                  (totalCount - (completedList.Count() + this.duplCount))));*/
            //duplicate count
            var archLogDAO = ArchLogDAO.getInstance();
            archLogDAO.UpdateArchLog(archLogID, successCount, failedCount, out isSuccess, out msg);
            log.Info("WorkAllocationThread process Finished..");
            if (!isSuccess)
            {
                log.Error("Update Arch Log Failed, please check the log file :" + msg);
            }
        }

        public int getFailedCount()
        {
            return failedCount;
        }

        private void createArchLog(out bool isSuccess, out string msg)
        {
            var archLogDAO = ArchLogDAO.getInstance();
            string srcCouch = this.mainForm.getMainForm().getSourceCouch().serverName + ":" +
                              this.mainForm.getMainForm().getSourceCouch().dbName;
            string tarCouch = this.mainForm.getMainForm().getTargetCouch().serverName + ":" +
                              this.mainForm.getMainForm().getTargetCouch().dbName;
            archLogID=archLogDAO.CreateArchLog(this.totalCount, srcCouch, tarCouch, this.mainForm.getMainForm().couchVO.userName, out isSuccess, out msg);
        }

        public void stopProcess()
        {
            log.Info("WorkAllocationThread stop process called..");
            Thread.CurrentThread.Abort();
            log.Info("WorkAllocationThread Abort Completed..");
        }

        private int computePercent(int Compl, int Total)
        {
            double db1 = Compl;
            double db2 = Total;

            double res = (db1 / db2) * 100;
           // log.Debug(string.Format("{0}/{1} val in double {2}",db1,db2,res));
            return (int)Math.Round(res);
        }

        private void Execute()
        {
            Thread jobThread=null;
            ArchiveJob archJob = null;
            ThreadBean tBean = null;
            initDBNameDictionary();
            //Init Error Messages
            log.Info(string.Format("Execution started with no of threads {0}",this.totalCount));
            if (!ArchJobErrorDesc.getInstance().getStatus())
            {
                log.Error("Problem in initializing Error Messages , stopping process");
                return;
            }
            var tempList = new List<vo.PawnDocRegVO>();
            var totList=new List<vo.PawnDocRegVO>();
            bool dbCreateError = false;
            bool docGetError = false;
            totList = GetDocumentToArchOnlyRecovery(out dbCreateError, out docGetError);
            if (totList==null || totList.Count() == 0)
            {
                log.Info("No data found to do retry");
                return;
            }

            while(true)
            {
                if (worker.CancellationPending)
                {
                    log.Info("cancel pressed felt in : main workder :step1 ");
                    WaitUntillAllJobsCompleted();
                    return;
                }

                if (totList.Count() == 0)
                {
                    //totList = GetDocumentToArch(out dbCreateError,out docGetError);
                    //totList = GetDocumentToArchTemp(out dbCreateError, out docGetError);
                    //totList = GetDocumentToArchOnlyRecovery(out dbCreateError, out docGetError);
                    
                    if (docGetError)
                    {
                        log.Error("Execution aborted due to error in getting doc from SP");
                        return; 
                    }
                    if (dbCreateError) //check for error's during db creation
                    {
                        log.Error("Execution aborted due to error in DB creation in couch or db");
                        return;
                    }
                    if (totList == null || totList.Count == 0)
                    {
                        WaitUntillAllJobsCompleted();
                        log.Debug("All jobs completed , Exiting");
                        log.Debug("******************************************Reutne");
                        return;
                    }
                    
                }
                int allowedJob = GetAllowedJob();
                /*if (this.completedList.Count()==119)
                {
                    log.Info("Reached 119");
                }*/
                worker.ReportProgress(computePercent(this.completedList.Count(), totalCount),
                                     string.Format("{0}:{1}:{2}", totalCount, completedList.Count(),
                                                   (totalCount - completedList.Count())));
                //compl count with duplicate
                /*worker.ReportProgress(computePercent(this.completedList.Count()+duplCount, totalCount),
                                    string.Format("{0}:{1}:{2}", totalCount, completedList.Count()+this.duplCount,
                                                  (totalCount - (completedList.Count()+this.duplCount))));*/
                //compl count with duplicate
                int jobCount = 0;
                foreach (var pawnDocRegVo in totList)
                {
                    if(jobCount<allowedJob)
                    {
                        archJob = new ArchiveJob(pawnDocRegVo,this.mainForm.getMainForm().getSourceCouch(),
                            this.mainForm.getMainForm().getTargetCouch(),isRecovery);
                        jobThread = new Thread(new ThreadStart(archJob.process));
                        jobThread.Name = "T"+pawnDocRegVo.DocID.ToString();
                        tBean=new ThreadBean();
                        tBean.Job = archJob;
                        tBean.DocumentID = pawnDocRegVo.DocID;
                        tBean.ThreadObj = jobThread;
                        runningList.Add(tBean);
                        tempList.Add(pawnDocRegVo);
                        jobThread.Start();
                        jobCount++;
                    }
                    else
                    {
                        break;
                    }  
                }
                //clear allocated job from totlist
                /*foreach (var threadBean in runningList)
                {
                    totList.Remove(threadBean.Job.getVO());
                }*/
                if (tempList.Count > 0)
                {
                    foreach(var tempBeanVo in tempList)
                    {
                        totList.Remove(tempBeanVo);
                    }
                    tempList.Clear();
                }
                if(criticalErrorOccured)
                {
                    log.Error("Process Cancelled################### ");
                    this.worker.CancelAsync();
                }
                Thread.Sleep(50);
            }

        }

        private void WaitUntillAllJobsCompleted()
        {
            Stopwatch watch=new Stopwatch();
            watch.Start();
            while (true)
            {
                
                if (runningList.Count == 0)
                {
                    log.Info("All Pending Jobs Completed" + runningList.Count);
                    ClearCompletedThreads();
                    return;
                }else
                {
                    log.Info("Waiting for all jobs to completed : running count :" + runningList.Count);
                    ClearCompletedThreads();
                }
               Thread.Sleep(50);
               if(watch.ElapsedMilliseconds>Timeout)
               {
                   killThreads(runningList);
                   break;
               }
            }
        }

        private void killThreads(List<ThreadBean> threadBeans)
        {
            log.Info("Aborting threads since wait time exceeded");
            foreach(var threadBean in threadBeans)
            {
                threadBean.ThreadObj.Abort();
                log.Info(string.Format("Thread {0} aborted", threadBean.ThreadObj.Name));
            }
        }

        private void initDBNameDictionary()
        {
            List<PwnDocArchDBRepVO> list=new List<PwnDocArchDBRepVO>();
            CouchDAO.getInstance().GetCouchDBRepos(ref list);
            if (list==null || list.Count==0)
                return;
            foreach(var pwnDocArchDbRepVO in list)
            {
                if(this.dbDictionary.ContainsKey(pwnDocArchDbRepVO.DBName))
                {
                    dbDictionary[pwnDocArchDbRepVO.DBName] = pwnDocArchDbRepVO;
                }else
                {
                    dbDictionary.Add(pwnDocArchDbRepVO.DBName,pwnDocArchDbRepVO);
                }
            }
        }

        

        public bool getDBNames(List<PawnDocRegVO> list)
        {
            int month = 0;
            int year = 0;
            string prefix = "clx_";
            string name = "";

            if (list == null)
            {
                return false;
            }
            PwnDocArchDBRepVO dbRepVo = null;
            
            foreach (var pawnDocRegVO in list)
            {
                DateTime dt = pawnDocRegVO.CreationDate;
                month = dt.Month;
                year = dt.Year;
                name = prefix + month + "_" + year;

                if (dbDictionary.ContainsKey(name))
                {
                    dbRepVo = dbDictionary[name];
                    pawnDocRegVO.TargetCouchDBID = dbRepVo.Id;
                    pawnDocRegVO.TargetCouchDBName = dbRepVo.DBName;
                    pawnDocRegVO.TargetCouchDBInfo = dbRepVo.DBInfo;
                   // log.Debug(string.Format("DB {0} existed in database set id {1}",dbRepVo.DBName,dbRepVo.Id));
                }
                else
                {
                    if (addDBNameInCouchAndDB(name,out dbRepVo))
                    {
                        dbDictionary.Add(name, dbRepVo);
                        pawnDocRegVO.TargetCouchDBID = dbRepVo.Id;
                        pawnDocRegVO.TargetCouchDBName = dbRepVo.DBName;
                        pawnDocRegVO.TargetCouchDBInfo = dbRepVo.DBInfo;
                        continue;
                    }else
                    {
                        return false;
                    }
                    
                }
            }
            return true;

        }

        private bool addDBNameInCouchAndDB(string name,out PwnDocArchDBRepVO repvo)
        {
            CouchVo vo = this.mainForm.getMainForm().getTargetCouch();
            repvo = new PwnDocArchDBRepVO();
            if(string.IsNullOrEmpty(vo.adminUserName))
            {
                log.Error("Vo values are not set DB Creation Aborted");
                return false;
            }
            CouchUtil util = new CouchUtil();
            string retMsg = null;
            bool retBool = false;
            try
            {
                util.createDBInCouch(name, out retMsg, out retBool, vo);

                if(!retBool)
                {
                    log.Error("Couch DB Creation failed due to :" + retMsg);
                    return false;
                }

            }catch(Exception e)
            {
                log.Error("Couch DB Creation failed",e);
                log.Error(e.StackTrace);
                return false;
            }

            try
            {
                
                List<PwnDocArchDBRepVO> list = new List<PwnDocArchDBRepVO>();
                repvo.DBName = name;
                repvo.DBInfo = vo.serverName + ":" + vo.serverport+":"+vo.userName;
                string str = "";
                //repvo.Id = ConnDAO.getInstance().getUniqueID(ref str,connection1);
                list.Add(repvo);
                /*if (repvo.Id == 0)
                {
                    log.Error("Did not get unique id for db");
                    return false;
                }*/
                int dbID = 0;
                if (!CouchDAO.getInstance().AddCouchDbToRepos(list, out dbID))
                {
                    log.Error("create dbname in database failed");
                    return false;
                }
                repvo.Id = dbID;

            }
            catch(Exception e)
            {
                log.Error("Couch DB Creation in DB failed", e);
                log.Error(e.StackTrace);
                return false;
            }

            return true;
        }

        private List<PawnDocRegVO> GetDocumentToArchTemp(out bool dbCreateError, out bool docGetError)
        {
            List<PawnDocRegVO> list = null;
            dbCreateError = false;

            //tmp logic
            if (tempSoFarCount > 1000000)
            {
                docGetError = false;
                return null;
            }

            if(this.archDocIndex==0)
            {
                //DocumentDAO.getInstance().GetDocumentSets(ref list, fetchCount,true,0);
               docGetError= DocumentDAO.getInstance().GetTempGetDocsIDs(ref list, fetchCount, true, 0);
                if (list != null && list.Count > 0)
                {
                    archDocIndex = (list[list.Count - 1]).DocID;
                }
               // return list;
            }else
            {
                //DocumentDAO.getInstance().GetDocumentSets(ref list, fetchCount, false, archDocIndex);
                docGetError= DocumentDAO.getInstance().GetTempGetDocsIDs(ref list, fetchCount, false, archDocIndex);
                if(list!=null && list.Count>0)
                {
                    archDocIndex = (list[list.Count - 1]).DocID;
                }
            }
            if (!getDBNames(list))
            {
                dbCreateError = true;
            }

            //tmp logic
            if(list!=null)
                tempSoFarCount += list.Count;
            if (tempSoFarCount>1000000)
            {
                return null;
            }
            //tmp logic
            return list;
        }


         private List<PawnDocRegVO> GetDocumentToArchOnlyRecovery(out bool dbCreateError,out bool docGetError)
         {
             List<PawnDocRegVO> list = null;
             dbCreateError = false;
             docGetError = false;
             int errorCode;

             if (!isRecoveryOnceExecuted) //check the once executed flag and quit the second time
                {                           //to avoid infinite loop
                    isRecoveryOnceExecuted = true;
                }else
                {
                    return null; //return null second time
                }
             if (!DocumentDAO.getInstance().GetDocumentSets(ref list))
             {
                 docGetError = true;
                 return null;
             }
             else
             {
                 if (!getDBNames(list))
                 {
                     dbCreateError = true;
                 }
                 return list;
             }
        }

        private List<PawnDocRegVO> GetDocumentToArch(out bool dbCreateError,out bool docGetError)
        {
            List<PawnDocRegVO> list = null;
            dbCreateError = false;
            docGetError = false;
            int errorCode;
            string errorMSG;
            int totalCount;

            if (isRecovery)
            {
                if (!isRecoveryOnceExecuted) //check the once executed flag and quit the second time
                {                           //to avoid infinite loop
                    isRecoveryOnceExecuted = true;
                }else
                {
                    return null; //return null second time
                }
                if (!DocumentDAO.getInstance().GetDocumentSets(ref list))
                {
                    docGetError = true;
                    return null;
                }
                else
                {
                    return list;
                }
            }

            if (this.archDocIndex == 0)
            {
                /*if (isRecovery)
                {
                    log.Info("Doing recovery init fetch");
                    if(!DocumentDAO.getInstance().GetDocumentSets(ref list, fetchCount, true, 0))
                    {
                        docGetError = true;
                        return list;
                    }
                }
                else
                {*/
                    log.Info("Doing normal init fetch");
                    DocumentDAO.getInstance().GetDocumentSets(0, out list, out totalCount, out errorCode, out errorMSG);
                    if (errorCode != 0)
                    {
                        docGetError = true;
                        return list;
                    }
              //  }
                if (list != null && list.Count > 0)
                {
                    archDocIndex = (list[list.Count - 1]).DocID;
                }
            }
            else
            {
                /*if (isRecovery)
                {
                    log.Info("Doing recovery next fetch");
                    if (!DocumentDAO.getInstance().GetDocumentSets(ref list, fetchCount, false, archDocIndex))
                    {
                        docGetError = true;
                        return list;
                    }
                }
                else
                {*/
                    log.Info("Doing normal next fetch");
                    DocumentDAO.getInstance().GetDocumentSets(archDocIndex, out list, out totalCount, out errorCode, out errorMSG);
                    if (errorCode != 0)
                    {
                        docGetError = true;
                        return list;
                    }
                //}
                if (list != null && list.Count > 0)
                {
                    archDocIndex = (list[list.Count - 1]).DocID;
                }
            }
            if (!getDBNames(list))
            {
                dbCreateError = true;
            }
            return list;
        }

        /*private List<PawnDocRegVO> filterList(List<PawnDocRegVO> srcList)
        {
            List<PawnDocRegVO> tmpList=new List<PawnDocRegVO>();
            foreach(var pawnDocRegVO in srcList)
            {
                if(filteredDocDict.ContainsKey(pawnDocRegVO.DocID))
                {
                    this.duplCount++;
                    tmpList.Add(pawnDocRegVO);
                }else
                {
                    filteredDocDict.Add(pawnDocRegVO.DocID,string.Empty);
                }
            }

            foreach(var pawnDocRegVO in tmpList)
            {
                srcList.Remove(pawnDocRegVO);
            }
            return srcList;
        }*/

        //get no of allowed jobs and do cleanup of completed jobs
        private int GetAllowedJob()
        {
            //List<ThreadBean> removeList = new List<ThreadBean>();

            ClearCompletedThreads();
            int allowedNumber = 0;

            if (runningList.Count < noOfThreads)
            {
                allowedNumber = noOfThreads - runningList.Count;
                if (allowedNumber < 0) return 0;
                else return allowedNumber;

            }else
            {
                return allowedNumber;
            }
        }

        public void ClearCompletedThreads()
        {
            List<ThreadBean> removeList = new List<ThreadBean>();

            foreach (var threadBean in runningList)
            {
                if (!threadBean.ThreadObj.IsAlive)
                {
                    this.completedList.Add(threadBean.DocumentID.ToString());
                    removeList.Add(threadBean);
                    if(threadBean.Job.isJobSuccess())
                    {
                        successCount++;
                    }else
                    {
                        failedCount++;
                    }
                }

                if (threadBean.Job.criticalErrorState())
                {
                    this.criticalErrorOccured = threadBean.Job.criticalErrorState();
                    log.Error(string.Format("Thread {0} notified critical error", threadBean.ThreadObj.Name));
                }
            }

            foreach (var vo in removeList)
            {
                runningList.Remove(vo);
            }
        }

    }
}
