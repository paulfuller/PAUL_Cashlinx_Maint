using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using CashlinxDesktop.Desktop;
using CashlinxDesktop.DesktopForms;
using CashlinxDesktop.DesktopProcedures;
using CashlinxDesktopLoadTester.Scenarios;
using CashlinxDesktopLoadTester.Scenarios.Impl;
using PawnUtilities;
using PawnUtilities.Collection;
using PawnUtilities.Type;

namespace CashlinxDesktopLoadTester.ThreadController
{
    public class CashlinxLoadThreadController
    {
        public enum ProgressionRate
        {
            ARITHMETIC,
            GEOMETRIC,
            RANDOM
        }

        public enum ExecuteFlow
        {
            NEWLOAN_EXIST_CUSTOMER,
            NEWLOAN_NEW_CUSTOMER
        }

        public delegate void MsgFunction(string s);

        private ProgressionRate rate;
        private uint delayBetweenUsers;
        private uint maxNumberThreads;
        private uint curIteration;
        private uint numberIterations;
        private List<Thread> threadList;
        private LoadTestInputVO threadInput;
        private List<uint> threadRateList;
        private ThreadPriority threadPriority;
        private double progressionScale;
        private ExecuteFlow flowToExecute;
        private bool fullyComplete;
        private MsgFunction msgFunction;
        private Dictionary<int, List<TupleType<int, string, double>>> timings;
        private StreamWriter fileOutput;

        public static void threadExecutorNewLoanExistCustomer(object data)
        {
            if (data == null)
            {
                return;
            }
            var threadInput = (TupleType<int, LoadTestInputVO, List<TupleType<int, string, double>>>)data;
            var eFlow = new ExecuteNewLoanExistCustomer(ProcessTenderController.Instance,
                CashlinxDesktopSession.Instance, threadInput);
            eFlow.execute();
        }

        private void genThreadRateList()
        {
            int numUsersLeft = (int)this.maxNumberThreads;
            double increment = 1.0d;
            double curAmount = 0.0d;
            switch(this.rate)
            {
                case ProgressionRate.ARITHMETIC:
                    increment = progressionScale;
                    curAmount = 0.0d;
                    while(numUsersLeft > 0)
                    {
                        curAmount = increment;
                        int amountToAllocate = (int)Math.Ceiling(curAmount);
                        threadRateList.Add((uint)amountToAllocate);
                        numUsersLeft -= amountToAllocate;
                    }
                    break;
                case ProgressionRate.GEOMETRIC:
                    increment = progressionScale;
                    curAmount = 1.0d;
                    while(numUsersLeft > 0)
                    {
                        int amountToAllocate = (int)Math.Ceiling(Math.Pow(curAmount, progressionScale));
                        threadRateList.Add((uint) amountToAllocate);
                        numUsersLeft -= amountToAllocate;
                        curAmount++;
                    }
                    break;
                case ProgressionRate.RANDOM:
                    Random r = new Random();
                    increment = 1.0d + (r.NextDouble() * progressionScale);
                    curAmount = 1.0d;
                    while(numUsersLeft > 0)
                    {
                        int amountToAllocate = (int) Math.Ceiling(curAmount);
                        threadRateList.Add((uint) amountToAllocate);
                        numUsersLeft -= amountToAllocate;
                        curAmount += increment;
                        increment = 1.0d + (r.NextDouble() * progressionScale);
                    }
                    break;
            }
        }

        private void prepareThreads()
        {
            this.threadList = new List<Thread>();
            ParameterizedThreadStart pThreadSt = null;
            switch(this.flowToExecute)
            {
                case ExecuteFlow.NEWLOAN_EXIST_CUSTOMER :
                    pThreadSt = new ParameterizedThreadStart(CashlinxLoadThreadController.threadExecutorNewLoanExistCustomer);
                    break;
                default:
                    throw new ApplicationException("Execution flow not implemented in load tester");
            }
            for (int j = 0; j < this.maxNumberThreads; ++j)
            {
                var t = new Thread(pThreadSt);
                t.Priority = this.threadPriority;
                this.threadList.Add(t);
            }
        }

        private void dumpThreadOutput()
        {
            for (int curIter = 0; curIter < this.numberIterations; ++curIter)
            {
                int normalizedIteration = curIter + 1;
                for (int curThread = 0; curThread < this.maxNumberThreads; ++curThread)
                {
                    List<TupleType<int, string, double>> timingList = this.timings[curThread];
                    if (CollectionUtilities.isEmpty(timingList))
                    {
                        continue;
                    }

                    foreach(TupleType<int, string, double> curT in timingList)
                    {
                        if (curT.Left != normalizedIteration) continue;
                        fileOutput.WriteLine(normalizedIteration + "," + curThread + "," + curT.Mid + "," + curT.Right);
                    }
                }
            }
        }


        private void startThreadProgression()
        {
            int threadIdx = 0;
            bool doneWithThreads = false;
            //this.msgFunction(
            //    "*** Starting iteration " +
            //    (this.curIteration + 1) + " of " + this.numberIterations + " ***");
            //this.msgFunction("*** Number of threads to initialize = " + this.threadList.Count);
            foreach (int pCnt in this.threadRateList)
            {
                //this.msgFunction("*** Number of threads in this step of the progression = " + pCnt);
                for (int i = 0; i < pCnt; i++)
                {
                    List<TupleType<int, string, double>> timingList = null;
                    if (timings.ContainsKey(threadIdx))
                    {
                        timingList = timings[threadIdx];
                    }
                    else
                    {                            
                        timingList = new List<TupleType<int, string, double>>();
                        timings.Add(threadIdx, timingList);
                    }
                    //this.msgFunction("---- Starting thread " + threadIdx);
                    var threadParam = 
                        new TupleType<int, LoadTestInputVO, List<TupleType<int, string, double>>>(
                            (int)(this.curIteration+1),
                            this.threadInput,
                            timingList);

                    this.threadList[threadIdx].Start(threadParam);
                    Thread.Sleep((int)this.delayBetweenUsers);
                    threadIdx++;
                    if (threadIdx < this.threadList.Count)
                    {
                        continue;
                    }
                    doneWithThreads = true;
                    break;
                }
                if (doneWithThreads)
                {
                    break;
                }
            }

            //this.msgFunction(
            //    "*** All threads for iteration " + 
            //    (this.curIteration+1) + " of " + this.numberIterations + " started ***");
        }

        private void resetIteration()
        {
            this.threadRateList.Clear();
            this.threadList.Clear();
            this.genThreadRateList();
            this.prepareThreads();
        }

        public void finishIteration()
        {
            //Wait until current iteration has completed
            while (AreThreadsAlive())
            {
                Thread.Sleep(1000);
            }

//            this.msgFunction(
//                "*** All threads for iteration " +
//                (curIteration + 1) + " of " + numberIterations + " completed ***");
            this.curIteration++;
            this.resetIteration();
            if (this.curIteration >= this.numberIterations)
            {
                this.dumpThreadOutput();
                this.fileOutput.Flush();
                this.fileOutput.Close();
                this.fullyComplete = true;
            }
            else
            {
                this.startThreadProgression();                
            }
        }

        public bool AreThreadsAlive()
        {
            foreach (Thread t in this.threadList)
            {
                if (t == null)
                    continue;
                if (t.ThreadState != ThreadState.Stopped &&
                    t.ThreadState != ThreadState.Aborted &&
                    t.ThreadState != ThreadState.Suspended)
                {
                    return (true);
                }
            }
            return (false);
        }        

        public CashlinxLoadThreadController(
            ProgressionRate pRate,
            uint delayUsers,
            uint maxThreads,
            double scale,
            uint numIters,
            LoadTestInputVO input,
            ThreadPriority priority, 
            ExecuteFlow scen,
            MsgFunction mFunc,
            StreamWriter fOutput)
        {
            this.rate = pRate;
            this.delayBetweenUsers = delayUsers;
            this.maxNumberThreads = maxThreads;
            this.threadRateList = new List<uint>();
            this.threadList = new List<Thread>();
            this.threadPriority = priority;
            this.progressionScale = scale;
            this.numberIterations = numIters;
            this.flowToExecute = scen;
            this.threadInput = input;
            this.msgFunction = mFunc;
            if (this.progressionScale < 0.0d)
            {
                this.progressionScale *= -1.0d;
            }
            this.genThreadRateList();
            this.prepareThreads();
            this.fullyComplete = false;
            this.curIteration = 0;
            this.timings = new Dictionary<int, List<TupleType<int, string, double>>>();
            this.fileOutput = fOutput;
            this.fileOutput.WriteLine("Iteration,  ThreadID  ,  Area  , Duration (seconds) ");
        }

        public void startThreads()
        {
            this.startThreadProgression();
        }


        public void endThreads()
        {
            foreach(Thread t in this.threadList)
            {
                if (t == null) continue;
                t.Join();
            }

            this.finishIteration();
        }

        public void resetAll()
        {
            this.fullyComplete = false;
            this.threadRateList.Clear();
            this.threadList.Clear();
            this.genThreadRateList();
            this.prepareThreads();
        }

        public bool IsFullyComplete()
        {
            return(this.fullyComplete);
        }
    }
}
