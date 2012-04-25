using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using CouchConsoleApp.vo;

namespace CouchConsoleApp.test
{
    internal class TestPopulateData2
    {
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(TestPopulateData2));

        private List<Thread> runningList = new List<Thread>();
        private List<string> completedList = null;
        private List<string> erroredList = null;
        private int lastRecord = 0;

        private int noOfThreads = 100;

        //
        private bool fetchInProg = false;
        private TestDocumentDAOThread daoThread = null;
        private Thread jobThread = null;

        //
        private string getDocID = null;
        private CouchVo vo;

        public void populateData(CouchVo vo1, bool isDelete, string getDocID1)
        {
            var tempList = new List<vo.PawnDocRegVO>();
            var totList = new List<vo.PawnDocRegVO>();
            bool completed = false;
            bool docGetError = false;
            bool isNextFetch = false;
            this.getDocID = getDocID1;
            this.vo = vo1;
            GetDocumentToArchTemp();
            Stopwatch overalTime=new Stopwatch();
            overalTime.Start();
            int fetchSoFar = 0;
            while(true)
            {
               GetData(out totList, out docGetError, out completed);
               if (totList == null || totList.Count == 0 || docGetError || completed)
               {
                   waitAndCompleteRunningList();
                   log.Info(string.Format("Completed main loop docGetError{0} completed{1}",docGetError,completed));
                   break;
               }
                fetchSoFar += totList.Count;
                if (fetchSoFar >= 300000)
                {
                    log.Info("Count so far :" + fetchSoFar + " breaking,......");
                    break;
                }
                else
                {
                    log.Info("Count so far :" + fetchSoFar);
                }
                loopWithTotalCount(totList);
            }
            overalTime.Stop();
            log.Info("Total time" + overalTime.Elapsed);
        }

        private void loopWithTotalCount(List<PawnDocRegVO> totList)
        {
            int threadNameCount = 1;
            int allocatedCount = 0;
            
            //Start dao thread
            GetDocumentToArchTempAsync();

            while(true)
            {
                if (totList.Count == 0)
                {
                    break;
                }

                int allowedCount = getAllowedCount(ref runningList);
                int jobCount = 0;
                int endCount = 20;
                List<string> subList = new List<string>();
                List<PawnDocRegVO> subList1 = new List<PawnDocRegVO>();
                while(jobCount < allowedCount)
                {
                    if (totList.Count < endCount)
                    {
                        endCount = totList.Count;
                    }
                    subList = new List<string>();
                    for(int i = 0; i < endCount; i++)
                    {
                        subList.Add(totList[i].StorageID);
                        subList1.Add(totList[i]);
                    }
                    AddTestDocThread th = new AddTestDocThread(subList, vo, getDocID);
                    Thread jobThread = null;
                    jobThread = new Thread(new ThreadStart(th.process));

                    string threhName = "Thread" + threadNameCount;
                    threadNameCount++;
                    jobThread.Name = threhName;
                    runningList.Add(jobThread);
                    log.Info(string.Format("Thread {0} started with count {1}", threhName, subList.Count));
                    allocatedCount += subList.Count;
                    jobThread.Start();
                    foreach(var VARIABLE in subList1)
                    {
                        totList.Remove(VARIABLE);
                    }
                    if (totList.Count == 0)
                    {
                        break;
                    }
                    jobCount++;
                }
                Thread.Sleep(250);
            }
            
        }

        private void waitAndCompleteRunningList()
        {
            log.Info("Came to wait for jobs to complet..");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while(true)
            {
                List<Thread> clearList = new List<Thread>();
                foreach(var thread in runningList)
                {
                    if (!thread.IsAlive)
                    {
                        clearList.Add(thread);
                    }
                }

                foreach(var thread in clearList)
                {
                    runningList.Remove(thread);
                }

                if (runningList.Count == 0)
                {
                    log.Info("All done");
                    break;
                }else
                {
                    log.Info("Running count" + runningList.Count);
                }
                Thread.Sleep(100);
            }
            sw.Stop();
        }

        private int getAllowedCount(ref List<Thread> runningList)
        {
            List<Thread> clearList = new List<Thread>();
            foreach(var thread in runningList)
            {
                if (!thread.IsAlive)
                {
                    clearList.Add(thread);
                }
            }

            foreach(var thread in clearList)
            {
                runningList.Remove(thread);
            }

            int allowedNumber = 0;

            if (runningList.Count < noOfThreads)
            {
                allowedNumber = noOfThreads - runningList.Count;
                if (allowedNumber < 0)
                {
                    return 0;
                }
                else
                {
                    return allowedNumber;
                }
            }
            else
            {
                return allowedNumber;
            }
        }

        private void GetData(out List<PawnDocRegVO> totList, out bool docGetError, out bool completed)
        {
            totList = new List<PawnDocRegVO>();
            docGetError = false;
            completed = false;

            while(true)
            {
                if (!daoThread.IsDataReady)
                {
                    log.Info("Data not ready sleepding");
                    Thread.Sleep(5000);
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

        private void GetDocumentToArchTemp()
        {
            List<PawnDocRegVO> newList = new List<PawnDocRegVO>();
            daoThread = new TestDocumentDAOThread(lastRecord);
            jobThread = new Thread(new ThreadStart(daoThread.execute));
            jobThread.Name = "T" + lastRecord;
            jobThread.Start();
            log.Info("First fetch join..");
            jobThread.Join();
            log.Info("join completed..");
        }

        private void GetDocumentToArchTempAsync()
        {
            List<PawnDocRegVO> newList = new List<PawnDocRegVO>();
            daoThread = new TestDocumentDAOThread(lastRecord);
            jobThread = new Thread(new ThreadStart(daoThread.execute));
            jobThread.Name = "T" + lastRecord;
            jobThread.Start();
        }

      /*  private void WaitUntillAllJobsCompleted()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            while(true)
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
            }
        }

        public void ClearCompletedThreads()
        {
            List<Thread> removeList = new List<Thread>();

            foreach(var threadBean in runningList)
            {
                if (!threadBean.IsAlive)
                {
                    this.completedList.Add(threadBean.Name);
                    removeList.Add(threadBean);
                }
            }

            foreach(var vo in removeList)
            {
                runningList.Remove(vo);
            }
        }*/
    }
}