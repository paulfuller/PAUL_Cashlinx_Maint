using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Timers;
using Common.Libraries.Utility.String;
using CouchConsoleApp.couch;
using CouchConsoleApp.db;
using CouchConsoleApp.events;
using CouchConsoleApp.form;
using CouchConsoleApp.test;
using CouchConsoleApp.vo;
using Timer = System.Threading.Timer;

namespace CouchConsoleApp.thread
{
    class JobAllocationHandler5
    {
        private ArchProcessForm mainForm = null;
        private BackgroundWorker worker = null;
        private DoWorkEventArgs e = null;
        private BgWorkerJobAllocationEventHandler _jobAllocationEventHandler = null;
        private List<ArchiveJob3> runningList = null;
        private int completedCount = 0;

        private int totalCount = 0;
        private int archDocIndex = 0;
        private int archLogID = 0;
        private bool isRecovery = false;
        private bool isRecoveryOnceExecuted = false;
        private bool isCancelPressed = false;
        private bool criticalErrorOccured = false;
        // Current wait time 1 min before timeout for threads

        private Dictionary<string, PwnDocArchDBRepVO> dbDictionary = new Dictionary<string, PwnDocArchDBRepVO>();
        private static CouchVo srcCouchVO;
        private static CouchVo targetCouchVO;

        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(JobAllocationHandler5));
        private System.Timers.Timer aTimer = null;

        private int successCount = 0;
        private int failedCount = 0;
        private int tempSoFarCount = 0;
        private int lastRecord = 0;
        private TestDocumentDAOThread daoThread = null;
        private Thread jobThread = null;

        //private int fetchCount = 20000;
        //private int Timeout = 60000;
        //private int threadCoolOffCount = 100;
        private int noOfThreads = 700;
        private int maxAllowedMemory = 1300;

        public JobAllocationHandler5(ArchProcessForm mainForm, BackgroundWorker worker, DoWorkEventArgs e,
                                     BgWorkerJobAllocationEventHandler _jobAllocationEventHandler, int totalRecords, bool isRecovery)
        {
            this.mainForm = mainForm;
            this.worker = worker;
            this.e = e;
            this._jobAllocationEventHandler = _jobAllocationEventHandler;
            archDocIndex = 0;
            this.totalCount = totalRecords;
            this.isRecovery = isRecovery;

            srcCouchVO = this.mainForm.getMainForm().getSourceCouch();
            targetCouchVO = this.mainForm.getMainForm().getTargetCouch();
            //maxAllowedThread = noOfThreads - 20;
            //maxAllowedThread = noOfThreads + 10;
            log.Info("Max process threads: " + noOfThreads);
            log.Info("Max Threads Allowed including sys threads: " + noOfThreads + 20);
        }

        public void process()
        {
            runningList = new List<ArchiveJob3>();
            isRecoveryOnceExecuted = false; //set this flag for recovery to fetch only once
            bool isSuccess;
            string msg;
            createArchLog(out isSuccess, out msg);
            if (!isSuccess)
            {
                log.Error("Create Arch Log Failed, please check the log file :" + msg);
                return;
            }
            startTimerToPrintMemoryStat();
            var sw = new Stopwatch();
            sw.Start();
            Execute();
            sw.Stop();
            log.Info("Overall Process Time...." + sw.Elapsed);
            worker.ReportProgress(computePercent(completedCount, totalCount),
                                  string.Format("{0}:{1}:{2}", totalCount, completedCount, (totalCount - completedCount)));
            this.aTimer.Stop();
            ArchLogDAO.getInstance().UpdateArchLog(archLogID, successCount, failedCount, out isSuccess, out msg);
            log.Info("WorkAllocationThread process Finished..");
            if (!isSuccess)
            {
                log.Error("Update Arch Log Failed, please check the log file :" + msg);
            }
        }

        private void startTimerToPrintMemoryStat()
        {
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(printMemoryUsage);
            //.5 Minutes
            aTimer.Interval = 30000;
            aTimer.Enabled = true;
        }

        public static CouchVo getSourceCouch()
        {
            return srcCouchVO;
        }

        public static CouchVo getTargetCouch()
        {
            return targetCouchVO;
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
            archLogID = archLogDAO.CreateArchLog(this.totalCount, srcCouch, tarCouch, this.mainForm.getMainForm().couchVO.userName,
                                                 out isSuccess, out msg);
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

        private void doWaitedFetch()
        {
            
        }

        private void checkAndStartFetchThread()
        {
            if (daoThread == null) //only one time
            {
                log.Info("Data Fetch initiated@@@@@@First Fetch@@@");
                daoThread = new TestDocumentDAOThread(lastRecord);
                /*daoThread = new TestDocumentDAOThread(lastRecord);
                jobThread = new Thread(new ThreadStart(daoThread.execute));
                jobThread.Start();
                while (jobThread.IsAlive)
                {
                    Thread.Sleep(1000);
                    if (worker.CancellationPending)
                    {
                        daoThread.abortExection();
                        return;
                    }
                }*/
                return;
            }

            if (jobThread.IsAlive)
            {
                //already fetch in progress
                return;
            }
            else
            {
                if (!daoThread.IsDataConsumed) //since data not consumed return
                {
                    return;
                }
                else
                {
                    log.Info("Data Fetch initiated@@@@@@@@@@@@");
                    //List<PawnDocRegVO> newList = new List<PawnDocRegVO>();
                    daoThread = null;
                    jobThread = null;
                    daoThread = new TestDocumentDAOThread(lastRecord);
                    jobThread = new Thread(new ThreadStart(daoThread.execute));
                    jobThread.Name = "T" + lastRecord;
                    jobThread.Start();
                 
                }
            }
        }

        private void Execute()
        {
            ArchiveJob3 archJob = null;
            initDBNameDictionary();
            log.Info(string.Format("Execution started with no of threads {0}", this.totalCount));
            if (!ArchJobErrorDesc.getInstance().getStatus())
            {
                log.Error("Problem in initializing Error Messages , stopping process");
                return;
            }
            List<vo.PawnDocRegVO> tempList = null;
            List<vo.PawnDocRegVO> totList = null;
            //Get data from form
            totList = this.mainForm.getData();
            if (totList.Count > 0)
            {
                lastRecord = totList[totList.Count - 1].DocID;

                if (!getDBNames(totList))
                {
                  log.Error("DB creation failed , execution aborted");
                  return;
                }
            }
            //ParameterizedThreadStart pThreadSt = new ParameterizedThreadStart();
            while (true)
            {
                checkAndStartFetchThread();
                //doWaitedFetch();
                if (worker.CancellationPending)
                {
                    log.Info("cancel pressed felt in : main workder :step1 ");
                    WaitUntillAllJobsCompleted();
                    return;
                }

                if (totList == null || totList.Count() == 0)
                {
                    log.Info("Going to get data............................");
                    //totList = GetDocumentToArch(out dbCreateError,out docGetError);
                    bool dbCreateError = false;
                    bool docGetError = false;
                    totList = GetDocumentToArchTemp(out dbCreateError, out docGetError);
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

                //critical error occured
                if(allowedJob==-1)
                {
                    log.Info("********Critical error, waiting to complete");
                    WaitUntillAllJobsCompleted();
                    return; 
                }

                int jobCount = 0;
                tempList = new List<PawnDocRegVO>();
                Thread.Sleep(100);

                log.Info("Memory usage :" + System.Diagnostics.Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024 + "MB");

                foreach (var pawnDocRegVo in totList)
                {
                    int currentThreadCount = System.Diagnostics.Process.GetCurrentProcess().Threads.Count;
                    long maxMemory = System.Diagnostics.Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024;

                    if (maxMemory > maxAllowedMemory)
                    {
                        log.Info("Max memory reached , allocation aborted current:" + maxMemory + " ,allowed :" + maxAllowedMemory);
                        break;
                    }
                    if (currentThreadCount >= noOfThreads)
                    {
                        log.Info("Max thread count reached , allocation aborted current:" + currentThreadCount + " ,allowed :" + noOfThreads);
                        break;
                    }

                    if (jobCount < allowedJob)
                    {
                        archJob = new ArchiveJob3(pawnDocRegVo, isRecovery);
                        jobThread = new Thread(new ThreadStart(archJob.process));
                        jobThread.Name = "T" + pawnDocRegVo.DocID;
                        archJob.JobThread = jobThread;
                        runningList.Add(archJob);
                        tempList.Add(pawnDocRegVo);
                        jobThread.Start();
                        jobCount++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (tempList.Count > 0)
                {
                    foreach (var tempBeanVo in tempList)
                    {
                        totList.Remove(tempBeanVo);
                    }
                    tempList.Clear();
                }

                tempList = null;
                if (criticalErrorOccured)
                {
                    log.Error("Process Cancelled################### ");
                    this.worker.CancelAsync();
                }
            }
        }

        private void WaitUntillAllJobsCompleted()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            if (this.jobThread.IsAlive)
            {
                //this.daoThread.abortExection();
                jobThread.Abort();
            }

            while (true)
            {
                if (runningList.Count == 0)
                {
                    log.Info("All Pending Jobs Completed" + runningList.Count);
                    ClearCompletedThreads();
                    return;
                }
                else
                {
                    log.Info("Waiting for all jobs to completed : running count :" + runningList.Count);
                    ClearCompletedThreads();
                }
                Thread.Sleep(50);
                /*if (watch.ElapsedMilliseconds > Timeout)
                {
                    killThreads(runningList);
                    break;
                }*/
            }
        }

        /*private void killThreads(List<ThreadBean> threadBeans)
        {
            log.Info("Aborting threads since wait time exceeded");
            foreach (var threadBean in threadBeans)
            {
                threadBean.ThreadObj.Abort();
                log.Info(string.Format("Thread {0} aborted", threadBean.ThreadObj.Name));
            }
        }*/

        private void initDBNameDictionary()
        {
            List<PwnDocArchDBRepVO> list = new List<PwnDocArchDBRepVO>();
            CouchDAO.getInstance().GetCouchDBRepos(ref list);
            if (list == null || list.Count == 0)
            {
                return;
            }
            foreach (var pwnDocArchDbRepVO in list)
            {
                if (this.dbDictionary.ContainsKey(pwnDocArchDbRepVO.DBName))
                {
                    dbDictionary[pwnDocArchDbRepVO.DBName] = pwnDocArchDbRepVO;
                }
                else
                {
                    dbDictionary.Add(pwnDocArchDbRepVO.DBName, pwnDocArchDbRepVO);
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
                    if (addDBNameInCouchAndDB(name, out dbRepVo))
                    {
                        dbDictionary.Add(name, dbRepVo);
                        pawnDocRegVO.TargetCouchDBID = dbRepVo.Id;
                        pawnDocRegVO.TargetCouchDBName = dbRepVo.DBName;
                        pawnDocRegVO.TargetCouchDBInfo = dbRepVo.DBInfo;
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool addDBNameInCouchAndDB(string name, out PwnDocArchDBRepVO repvo)
        {
            CouchVo vo = this.mainForm.getMainForm().getTargetCouch();
            repvo = new PwnDocArchDBRepVO();
            if (string.IsNullOrEmpty(vo.adminUserName))
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

                if (!retBool)
                {
                    log.Error("Couch DB Creation failed due to :" + retMsg);
                    return false;
                }
            }
            catch (Exception e)
            {
                log.Error("Couch DB Creation failed", e);
                log.Error(e.StackTrace);
                return false;
            }

            try
            {
                List<PwnDocArchDBRepVO> list = new List<PwnDocArchDBRepVO>();
                repvo.DBName = name;
                repvo.DBInfo = vo.serverName + ":" + vo.serverport + ":" + vo.userName;
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
            catch (Exception e)
            {
                log.Error("Couch DB Creation in DB failed", e);
                log.Error(e.StackTrace);
                return false;
            }

            return true;
        }

        private void GetData(out List<PawnDocRegVO> totList, out bool docGetError, out bool completed)
        {
            totList = new List<PawnDocRegVO>();
            docGetError = false;
            completed = false;

            while (true)
            {
                if (!daoThread.IsDataReady)
                {
                    log.Info("Data not ready sleepding");
                    Thread.Sleep(5000);

                    if (worker.CancellationPending)
                    {
                        log.Info("cancel pressed felt in GetData: main workder :step1");
                        WaitUntillAllJobsCompleted();
                        return;
                    }
                }
                else
                {
                    log.Info("Data Ready.. Transferring");
                    if (daoThread.HasReachedEnd() || daoThread.HasErrorOccured())
                    {
                        docGetError = daoThread.HasErrorOccured();
                        completed = daoThread.HasReachedEnd();
                        return;
                    }
                    totList.AddRange(daoThread.getData());
                    daoThread.setData(null);
                    //set daoThread is read so that next search can begin
                    daoThread.IsDataConsumed = true;
                    if (totList.Count > 0)
                    {
                        lastRecord = totList[totList.Count - 1].DocID;
                    }
                    else
                    {
                        completed = true;
                    }
                    return;
                }
            }
        }

        private List<PawnDocRegVO> GetDocumentToArchTemp(out bool dbCreateError, out bool docGetError)
        {
            List<PawnDocRegVO> totList = new List<PawnDocRegVO>();
            bool completed;
            dbCreateError = false;

            GetData(out totList, out docGetError, out completed);

            if (!getDBNames(totList))
            {
                dbCreateError = true;
            }

            //tmp logic
            if (totList != null)
            {
                tempSoFarCount += totList.Count;
            }
            if (tempSoFarCount > 1000000)
            {
                return null;
            }
            //tmp logic
            return totList;
        }

        private List<PawnDocRegVO> GetDocumentToArch(out bool dbCreateError, out bool docGetError)
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
                {
                    //to avoid infinite loop
                    isRecoveryOnceExecuted = true;
                }
                else
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

        private void coolOffThreads()
        {
            int minCount = (this.noOfThreads - 200);
            Stopwatch watch = new Stopwatch();
            Process curProcess = null;
            watch.Start();
            while (true)
            {
                Thread.Sleep(1000);
                ClearCompletedThreads();
                curProcess = System.Diagnostics.Process.GetCurrentProcess();
                int threadCount = curProcess.Threads.Count;
                if (threadCount <= minCount)
                {
                    break;
                }
                if (worker.CancellationPending || criticalErrorOccured)
                {
                    log.Info("cancel or critical occured, no more allocation during cooloff ........");
                    WaitUntillAllJobsCompleted();
                    return;
                }
                log.Info("Cool off wait, time elapsed" + watch.ElapsedMilliseconds +
                    ": Current thread Count :" + threadCount +
                    ": Completed Process Count :" + runningList.Count +
                    " : Memory :" + curProcess.PrivateMemorySize64 / 1024 / 1024 + " MB");
            }
            watch.Stop();
            //GC.Collect();
            log.Info(string.Format("Cool of completed {0} Msec", watch.ElapsedMilliseconds));
            watch = null;
            curProcess = null;
        }

        //get no of allowed jobs and do cleanup of completed jobs
        private int GetAllowedJob()
        {
            int allowedNumber = 0;
            ClearCompletedThreads();
            Process curProcess = System.Diagnostics.Process.GetCurrentProcess();
            int currentthreadCount = curProcess.Threads.Count;

            long memory = curProcess.PagedMemorySize64 / 1024 / 1024;
            // int threadLowerBound = maxAllowedThread - 50;
            allowedNumber = noOfThreads - currentthreadCount;
            if (allowedNumber < 0) allowedNumber = 0;
            if (allowedNumber == 0 || memory >= maxAllowedMemory)
            {
                log.Info("Doing cool off : Thread count:" + currentthreadCount + ", Memory " + memory);
                coolOffThreads();
            }
            else
            {
                log.Info("Returning allowed no:" + allowedNumber);
                return allowedNumber;
            }
            if(criticalErrorOccured)
            {
                return -1;
            }

            currentthreadCount = curProcess.Threads.Count;
            allowedNumber = noOfThreads - currentthreadCount;
            if (allowedNumber < 0) allowedNumber = 0;
            log.Info(string.Format("Returning {0} after cool off memory {1}", allowedNumber,
                                   curProcess.PrivateMemorySize64 / 1024 / 1024 + " MB"));
            return allowedNumber;
        }

        public void ClearCompletedThreads()
        {
            List<ArchiveJob3> removeList = new List<ArchiveJob3>();

            foreach (var archJob in runningList)
            {
                if (archJob.JobEnd)
                {
                    completedCount++;
                    removeList.Add(archJob);
                    
                    if (archJob.isJobSuccess())
                    {
                        successCount++;
                    }
                    else
                    {
                        failedCount++;
                    }
                }

                if (archJob.criticalErrorState())
                {
                    this.criticalErrorOccured = archJob.criticalErrorState();
                    log.Error(string.Format("Thread {0} notified critical error", "T" + archJob.getDocID()));
                }
            }
            if (removeList.Count > 0)
            {
                log.Info("Doing DB update");
                Stopwatch timeForDB = new Stopwatch();
                timeForDB.Start();
                completeDBUpdates(removeList);
                timeForDB.Stop();
                log.Info("Time taken for updating to DB :"+timeForDB.ElapsedMilliseconds);
                timeForDB = null;
            }else
            {
                log.Info("No items to remove");
            }
            log.Info("Clearing .....Running count :" + runningList.Count + "Items cleared :" + removeList.Count);

            foreach (var vo in removeList)
            {
                if (vo != null)
                {
                    try
                    {
                        vo.JobThread.Abort();
                    }
                    catch (Exception)
                    {
                        log.Error("Error while aborting thread :" + vo.getDocID());
                    }
                }

                runningList.Remove(vo);
                vo.Dispose();
            }
            //GC.Collect();
            log.Info("Clearing .....Running count :" + runningList.Count + "Items cleared :" + removeList.Count);
            worker.ReportProgress(computePercent(completedCount, totalCount),
                                  string.Format("{0}:{1}:{2}", totalCount, completedCount, (totalCount - completedCount)));
        }

        private bool completeDBUpdates(List<ArchiveJob3> removeList)
        {
            bool critErrDB = false;
            List<PawnDocRegVO> succList=new List<PawnDocRegVO>();
            
            foreach(var archiveJob3 in removeList)
            {
                if (archiveJob3.IsDocMarkedForSuccess())
                {
                    succList.Add(archiveJob3.getDocObject());
                }
            }

            try
            {
                PwnArchLogDAO.getInstance().CreateArchLogForBatch(succList, ArchiveJob3.CHAR_CODE_SUCCESS, isRecovery);
            }catch(Exception e)
            {
                log.Error("Error occured during success log creation", e);
                dumpSuccessListToLog(succList);
                this.criticalErrorOccured = true;
                critErrDB = true;

            }
            succList = null;
            try
            {
                PwnArchLogDAO.getInstance().CreateBatchArchLogWithError(removeList, isRecovery);
            }
            catch(Exception e)
            {
                log.Error("Error occured during failure log creation",e);
                dumpFailureListToLog(removeList);
                this.criticalErrorOccured = true;
                critErrDB = true;
            }
            return critErrDB;
        }

        private void dumpSuccessListToLog(List<PawnDocRegVO> succList)
        {
            log.Info("---------------------Error Occurred During DB update for Success--------------");
            foreach(var pawnDocRegVO in succList)
            {
                log.Info(string.Format("Doc ID '{0}' Storage ID '{1}' Archived '{2}' ", pawnDocRegVO.DocID,
                    pawnDocRegVO.StorageID, "Yes"));
            }
            log.Info("---------------------Error Occurred During DB update for Success--------------");
        }


        private void dumpFailureListToLog(List<ArchiveJob3> removeList)
        {
            log.Info("---------------------Error Occurred During DB update for Failed Docs--------------");
            foreach (var archiveJob3 in removeList)
            {
                log.Info(string.Format("Doc ID '{0}' Storage ID '{1}' Archived '{2}'  ErrorCode Char '{3}' Error Message '{4}'",
                archiveJob3.getDocObject().DocID, archiveJob3.getDocObject().StorageID, "No",
                archiveJob3.getErrorCodeChar(),archiveJob3.getErrorMessageForDB()));
            }
            log.Info("---------------------Error Occurred During DB update for Failed Docs--------------");
        }

        private void printMemoryUsage(object source, ElapsedEventArgs e)
        {
            //Update process data
            Process curProcess = System.Diagnostics.Process.GetCurrentProcess();
            TimeSpan curPrivProcTime = curProcess.PrivilegedProcessorTime;
            DateTime startTime = curProcess.StartTime;
            long curNumThreads = curProcess.Threads.Count;
            TimeSpan curProcTime = curProcess.TotalProcessorTime;
            TimeSpan curUsrProcTime = curProcess.UserProcessorTime;
            long curVirtMem = curProcess.VirtualMemorySize64;
            long curPhysMem = curProcess.WorkingSet64;
            log.Info("####################################################################################");
            log.Info("NonpagedSystemMemorySize64  = " + curProcess.NonpagedSystemMemorySize64 / 1024 / 1024 + " MB");
            log.Info("PagedMemorySize64  = " + curProcess.PagedMemorySize64 / 1024 / 1024 + " MB");
            log.Info("PagedSystemMemorySize64  = " + curProcess.PagedSystemMemorySize64 / 1024 / 1024 + " MB");
            log.Info("PeakPagedMemorySize64  = " + curProcess.PeakPagedMemorySize64 / 1024 / 1024 + " MB");
            log.Info("PeakVirtualMemorySize64 [peak Virtual memory used]  = " + curProcess.PeakVirtualMemorySize64 / 1024 / 1024 + " MB");
            log.Info("curProcess.PeakWorkingSet64[peak physical memory used]  = " + curProcess.PeakWorkingSet64 / 1024 / 1024 + " MB");
            log.Info("Private Memory  = " + curProcess.PrivateMemorySize64 / 1024 / 1024 + " MB");
            log.Info("CPU Time  = " +
                     StringUtilities.TimeSpanToString(curPrivProcTime, StringUtilities.MaxTimeResolution.SECONDS,
                                                      StringUtilities.TimeFormat.WHOLEFRACTIONUNIT));
            log.Info("Current no of threads :" + curNumThreads);
            log.Info("Total Process Time  = " +
                     StringUtilities.TimeSpanToString(curProcTime, StringUtilities.MaxTimeResolution.SECONDS,
                                                      StringUtilities.TimeFormat.WHOLEFRACTIONUNIT));
            log.Info("User CPU Time  = " +
                     StringUtilities.TimeSpanToString(curUsrProcTime, StringUtilities.MaxTimeResolution.SECONDS,
                                                      StringUtilities.TimeFormat.WHOLEFRACTIONUNIT));
            log.Info("Virtual Memory [Page file usage]" + curVirtMem / 1024 / 1024 + " MB");
            log.Info("Physical memory [RAM usage] " + curPhysMem / 1024 / 1024 + " MB");
            log.Info("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            printSystemMemoryStat();
        }

        private void printSystemMemoryStat()
        {
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            //new Computer
            foreach (ManagementObject result in results)
            {
                log.Info(string.Format("Free Physical Memory {0} MB: Free Virtual Memory {1} MB :" + " Used Physical Memory {2} MB",
                                       long.Parse((result["FreePhysicalMemory"]).ToString()) / 1024,
                                       long.Parse((result["TotalVirtualMemorySize"]).ToString()) / 1024,
                                       (long.Parse((result["TotalVisibleMemorySize"]).ToString()) / 1024 -
                                        long.Parse((result["FreePhysicalMemory"]).ToString()) / 1024)));
            }
        }
    }
}