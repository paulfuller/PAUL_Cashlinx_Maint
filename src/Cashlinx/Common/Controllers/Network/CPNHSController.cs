using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Performance;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Type;

namespace Common.Controllers.Network
{
    public sealed class CPNHSController: MarshalByRefObject, IDisposable
    {
        #region Singleton related fields
        // ReSharper disable InconsistentNaming
        private static readonly object mutex = new object();
        static readonly CPNHSController instance = new CPNHSController();
        // ReSharper restore InconsistentNaming
        static CPNHSController()
        {
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public static CPNHSController Instance
        {
            get
            {
                return (instance);
            }
        }

        #endregion

        #region Implementation of IDisposable
        public void Dispose()
        {
            
        }
        #endregion

        
        #region Static Fields
        /// <summary>
        /// Default capture interval of 1 minute (1000 ms/s * 60 s/min * 1 min)
        /// </summary>
        public const int DEFAULT_CAPTURE_INTERVAL = 1000 * 60 * 1;
        public const long TICKS_TO_MS = 10000;
        public const string DEFAULT_USER = "admin";
        #endregion

        #region CPNHS Data Fields
        /// <summary>
        /// Active Data Capture Storage
        /// - Map Key = User Id
        /// - List Type
        /// -- List contains CPNHSDataVO objects
        /// </summary>
        private Dictionary<string, List<CPNHSDataVO>> activeDataCaptureObject;
        /// <summary>
        /// Active User Session Flag Storage
        /// - Map Key = User Id
        /// - Dictionary
        /// -- Value = Whether or not user is in an active session
        /// </summary>
        private Dictionary<string, bool> activeSessionFlag;
        /// <summary>
        /// Active User Session Data Storage
        /// - Map Key = User Id
        /// - Quad Type Tuple
        /// -- X = start time
        /// -- Y = end time
        /// -- Z = last active check time
        /// -- W = total active session time
        /// </summary>
        private Dictionary<string, QuadType<DateTime, DateTime, DateTime, long>> activeSessionData;
        /// <summary>
        /// Active User Transactional Data Storage
        /// - Map Key = User Id
        /// - Pair Type Tuple
        /// -- X = number transactions tendered
        /// -- Y = total amount tendered
        /// </summary>
        private Dictionary<string, PairType<long, decimal>> activeUserTransactionData;
        /// <summary>
        /// Active User Granular Stored Procedure Counts
        /// - Map Key = User Id
        /// - List contains Quad Type Tuples
        /// -- X = Call Prep Time
        /// -- Y = Call Wait Time
        /// -- Z = Call Process Time
        /// -- W = Total Call Time
        /// </summary>
        private Dictionary<string, List<QuadType<long, long, long, long>>> granularStoredProcCounts;
        ///// <summary>
        ///// Stored procedure with the highest data usage
        ///// </summary>
        //private Dictionary<string, PairType<string, long>> storedProcHighestData;
        ///// <summary>
        ///// Stored procedure with the highest time taken
        ///// </summary>
        //private Dictionary<string, PairType<string, long>> storedProcHighestTime;
        ///// <summary>
        ///// Stored procedure with the highest call frequency
        ///// </summary>
        //private Dictionary<string, PairType<string, long>> storedProcHighestFreq;
        /// <summary>
        /// Active User Stored Procedure Counts
        /// - Map Key = User Id
        /// - Value Type = Map
        /// -- Value Map Key = Stored Proc Name
        /// -- Value Map Stores Tuple Type
        /// --- X = Number of times stored procedure called
        /// --- Y = Total processing time utilized by the stored procedure
        /// --- Z = Total bytes processed by the stored procedure
        /// </summary>
        private Dictionary<string, Dictionary<string, TupleType<long, long, long>>> storedProcCounts;
        private DateTime initialTimeStamp;
        private DateTime lastTimeStamp;
        #endregion

        #region Private Methods
        private bool UpdateCPNHSData(CPNHSDataVO cvo, bool force = false)
        {
            var rt = false;
            lock (mutex)
            {
                var timeDiff = this.lastTimeStamp - DateTime.Now;
                if (force || timeDiff.TotalMinutes > DEFAULT_CAPTURE_INTERVAL)
                {
                    //Insert CPNHS data
                    if (cvo != null)
                    {
                        try
                        {
                            CPNHSProcedures.InsertCPNHSData(cvo);
                        }
                        catch (Exception eX)
                        {
                            FileLogger.Instance.logMessage(LogLevel.WARN, "CPNHSController", "InsertCPNHSData threw exception: {0}", eX);
                            return (false);
                        }
                    }
                    //Update last time stamp only after data insert
                    if (!force)
                    {
                        this.lastTimeStamp = DateTime.Now;
                        rt = true;
                    }
                }
            }
            return (rt);
        }

        private void computeStoredProcCounts(string userId, ref CPNHSDataVO cvo)
        {
            if ((cvo.PackType & CPNHSDataVO.PackDataType.STOREDPROC) > 0)
            {
                if (CollectionUtilities.isNotEmptyContainsKey(this.storedProcCounts, userId))
                {
                    var curStorage = this.storedProcCounts[userId];
                    if (CollectionUtilities.isNotEmpty(curStorage))
                    {
                        var maxCount = 0L;
                        var maxCountProc = string.Empty;
                        var maxTime = 0L;
                        var maxTimeProc = string.Empty;
                        var maxBytes = 0L;
                        var maxBytesProc = string.Empty;
                        foreach(var key in curStorage.Keys)
                        {
                            var curEntry = curStorage[key];
                            if (curEntry == null) continue;
                            if (curEntry.Left > maxCount)
                            {
                                maxCount = curEntry.Left;
                                maxCountProc = key;
                            }
                            if (curEntry.Mid > maxTime)
                            {
                                maxTime = curEntry.Mid;
                                maxTimeProc = key;
                            }
                            if (curEntry.Right > maxBytes)
                            {
                                maxBytes = curEntry.Right;
                                maxBytesProc = key;
                            }                            
                        }
                        //Update stats
                        cvo.ClientProcTopCalledName = maxCountProc;
                        cvo.ClientProcTopDataName = maxBytesProc;
                        cvo.ClientProcTopTimeName = maxTimeProc;
                    }
                }
            }
        }

        private void computeDataTransferIn(string userId, ref CPNHSDataVO cvo)
        {
            if ((cvo.PackType & CPNHSDataVO.PackDataType.DATAXFERIN) > 0)
            {
                var cvoList = this.activeDataCaptureObject[userId];
                var cvoListSz = cvoList.Count;
                if (cvoListSz <= 1)
                {
                    //There must be at least two entries
                    return;
                }
                //Setup the accumulator copy
                var tmp = new CPNHSDataVO(cvo);
                var cvoCntLat = (cvo.CurrentLatency > 0.0M) ? 1 : 0;
                var cvoCntIn = (cvo.CurrentDataRateIn > 0.0M) ? 1 : 0;
                for (var j = 0; j < cvoListSz - 1; ++j)
                {
                    var curCvo = cvoList[j];
                    if (curCvo == null || (curCvo.PackType & CPNHSDataVO.PackDataType.DATAXFERIN) == 0) continue;
                    
                    if (curCvo.CurrentDataRateIn > 0.0M)
                    {
                        cvoCntIn++;
                        tmp.NumberTransactionsIn++;
                        tmp.AverageDataRateIn = tmp.CurrentDataRateIn + curCvo.CurrentDataRateIn;
                    }
                    if (curCvo.CurrentLatency > 0.0M)
                    {
                        cvoCntLat++;
                        tmp.AverageLatency = tmp.CurrentLatency + curCvo.CurrentLatency;
                    }
                    tmp.TotalTimeTxferDataIn = tmp.TotalTimeTxferDataIn + curCvo.TotalTimeTxferDataIn;
                }

                //Compute data transfer averages now that they are accumulated
                if (cvoCntIn > 0)
                {
                    tmp.AverageDataRateIn /= cvoCntIn;
                }
                if (cvoCntLat > 0)
                {
                    tmp.AverageLatency /= cvoCntLat;
                }

                //Set the ref cvo to the tmp object
                cvo = tmp;
            }
        }

        private void computeDataTransferOut(string userId, ref CPNHSDataVO cvo)
        {
            if ((cvo.PackType & CPNHSDataVO.PackDataType.DATAXFEROUT) > 0)
            {
                var cvoList = this.activeDataCaptureObject[userId];
                var cvoListSz = cvoList.Count;
                if (cvoListSz <= 1)
                {
                    //There must be at least two entries
                    return;
                }
                //Setup the accumulator copy
                var tmp = new CPNHSDataVO(cvo);
                var cvoCntLat = (cvo.CurrentLatency > 0.0M) ? 1 : 0; 
                var cvoCntOut = (cvo.CurrentDataRateOut > 0.0M) ? 1 : 0;
                for (var j = 0; j < cvoListSz - 1; ++j)
                {
                    var curCvo = cvoList[j];
                    if (curCvo == null || (curCvo.PackType & CPNHSDataVO.PackDataType.DATAXFEROUT) == 0) continue;

                    if (curCvo.CurrentDataRateOut > 0.0M)
                    {
                        cvoCntOut++;
                        tmp.NumberTransactionsOut++;
                        tmp.AverageDataRateOut = tmp.CurrentDataRateOut + curCvo.CurrentDataRateOut;
                    }
                    if (curCvo.CurrentLatency > 0.0M)
                    {
                        cvoCntLat++;
                        tmp.AverageLatency = tmp.CurrentLatency + curCvo.CurrentLatency;
                    }
                    tmp.TotalTimeTxferDataOut = tmp.TotalTimeTxferDataOut + curCvo.TotalTimeTxferDataOut;
                }

                //Compute data transfer averages now that they are accumulated
                if (cvoCntOut > 0)
                {
                    tmp.AverageDataRateOut /= cvoCntOut;
                }
                if (cvoCntLat > 0)
                {
                    tmp.AverageLatency /= cvoCntLat;
                }

                //Set the ref cvo to the tmp object
                cvo = tmp;
            }
        }

        private void UpdateInternalData(string userId, bool force = false)
        {
            //Ensure the user id is valid
            if (string.IsNullOrEmpty(userId))
            {
                return;
            }
            //Should at least have one entry in the dictionary
            if (!CollectionUtilities.AddIfNoEntryExists(this.activeDataCaptureObject, userId, new List<CPNHSDataVO>()))
            {
                //Cannot process CPNHS data
                return;
            }
            var cpList = this.activeDataCaptureObject[userId];

            //Check to see if the list is empty
            if (CollectionUtilities.isEmpty(cpList))
            {
                return;
            }

            //This list contains all data points recorded for this user since they last logged on
            //Use the last one to get the latest statistics and then average them into the containers
            var lastCpVo = cpList.Last();
            if (lastCpVo == null)
            {
                return;
            }

            //Compute data pieces internally using the last interval worth of data
            computeDataTransferIn(userId, ref lastCpVo);
            computeDataTransferOut(userId, ref lastCpVo);
            computeStoredProcCounts(userId, ref lastCpVo);

            //Update to the database
            var res = UpdateCPNHSData(lastCpVo, force);

            //Clear the current capture objects if the insert was successful
            //and it was not forced
            if (!force && res)
            {
                cpList.Clear();
            }
            //If successful and forced, remove the last object only
            else if (force && res)
            {
                //Clear the last object
                cpList.RemoveAt(cpList.Count - 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        private void AddUserToDataMaps(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return;
            CollectionUtilities.AddIfNoEntryExists(this.activeDataCaptureObject, userId, new List<CPNHSDataVO>());
            CollectionUtilities.AddIfNoEntryExists(this.activeSessionFlag, userId, false);
            CollectionUtilities.AddIfNoEntryExists(this.activeSessionData, userId, new QuadType<DateTime, DateTime, DateTime, long>());
            CollectionUtilities.AddIfNoEntryExists(this.granularStoredProcCounts, userId, new List<QuadType<long, long, long, long>>());
            CollectionUtilities.AddIfNoEntryExists(this.storedProcCounts, userId, new Dictionary<string, TupleType<long, long, long>>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool IsUserActive(string userId)
        {
            var active = false;
            if (!string.IsNullOrEmpty(userId) &&
                CollectionUtilities.isNotEmptyContainsKey(this.activeSessionFlag, userId))
            {
                active = this.activeSessionFlag[userId];
            }
            else if (!string.IsNullOrEmpty(userId))
            {
                //Ensure the user is setup properly in the data maps
                AddUserToDataMaps(userId);
            }
            return (active);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clockInTime"></param>
        private void ActivateUser(string userId, DateTime clockInTime)
        {
            if (!IsUserActive(userId) && !string.IsNullOrEmpty(userId))
            {
                this.activeSessionFlag[userId] = true;
                var activeSessionDataObj = this.activeSessionData[userId];
                activeSessionDataObj.X = clockInTime;
                activeSessionDataObj.Y = clockInTime;
                activeSessionDataObj.Z = clockInTime;
                activeSessionDataObj.W = 0L;            
            }
        }

        private void StartCaptureActiveUser(string userId, DateTime currentClock)
        {
            if (IsUserActive(userId))
            {
                var activeSessionDataObj = this.activeSessionData[userId];
                activeSessionDataObj.Z = currentClock;
            }
        }

        private void EndCaptureActiveUser(string userId, DateTime currentClock)
        {
            if (IsUserActive(userId))
            {
                var activeSessionDataObj = this.activeSessionData[userId];
                activeSessionDataObj.W += (long)Math.Ceiling((currentClock - activeSessionDataObj.Z).TotalMilliseconds);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="storeNumber"> </param>
        /// <param name="clockOutTime"></param>
        /// <param name="workstationName"> </param>
        private void DeActivateUser(string userId, string workstationName, string storeNumber, DateTime clockOutTime)
        {
            if (IsUserActive(userId))
            {
                //Update session data
                this.activeSessionFlag[userId] = false;
                var activeSessionObj = this.activeSessionData[userId];
                //var activeUserTransObj = this.activeUserTransactionData[userId];
                activeSessionObj.Y = clockOutTime;

                //Add capture object 
                CPNHSDataVO dvo;
                AddCaptureObject((CPNHSDataVO.PackDataType.SESSINFO | CPNHSDataVO.PackDataType.TENDER), userId, out dvo);

                //Set user data point
                dvo.PackType |= CPNHSDataVO.PackDataType.SESSINFO;
                dvo.PackType |= CPNHSDataVO.PackDataType.TENDER;
                dvo.UserID = userId;
                dvo.WorkstationName = workstationName;
                dvo.StoreNumber = storeNumber;
                //dvo.NumberTransactionsTendered += activeUserTransObj.Left;
                //dvo.TotalAmountTendered += activeUserTransObj.Right;
                var totSessTime = (activeSessionObj.Y - activeSessionObj.X).TotalMilliseconds;
                var totInActTime = totSessTime - activeSessionObj.W;
                
                dvo.TotalSessionTime += (long)Math.Ceiling(totSessTime);
                dvo.TotalSessionTimeActive += activeSessionObj.W;
                dvo.TotalSessionTimeInActive += (long)Math.Ceiling(totInActTime);

                //Insert user data point
                UpdateInternalData(userId, true);
            }
        }

        private void AddCaptureObject(CPNHSDataVO.PackDataType dataType, string userId, out CPNHSDataVO cvo)
        {
            cvo = null;
            List<CPNHSDataVO> activeDataCapture = null;
            if (CollectionUtilities.isNotEmptyContainsKey(this.activeDataCaptureObject, userId))
            {
                activeDataCapture = this.activeDataCaptureObject[userId];
            }
            else
            {
                activeDataCapture = new List<CPNHSDataVO>();
            }

            if (CollectionUtilities.isNotEmpty(activeDataCapture))
            {
                cvo = activeDataCapture.Last();
                if ((cvo.PackType & dataType) > 0)
                {
                    cvo = new CPNHSDataVO();
                    activeDataCapture.Add(cvo);
                }
            }
            else
            {
                cvo = new CPNHSDataVO();
                activeDataCapture.Add(cvo);
            }
        }

        #endregion


        #region Constructor
        public CPNHSController()
        {
            this.lastTimeStamp = DateTime.Now;
            this.initialTimeStamp = DateTime.Now;
            this.activeDataCaptureObject = new Dictionary<string, List<CPNHSDataVO>>();
            this.activeSessionData = new Dictionary<string, QuadType<DateTime, DateTime, DateTime, long>>();
            this.activeSessionFlag = new Dictionary<string, bool>();
            this.granularStoredProcCounts = new Dictionary<string, List<QuadType<long, long, long, long>>>();
            this.storedProcCounts = new Dictionary<string, Dictionary<string, TupleType<long, long, long>>>();
            this.activeUserTransactionData = new Dictionary<string, PairType<long, decimal>>();
        }
        #endregion

        #region Public Methods For Data Accumulation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="startTime"> </param>
        public void StartSession(string userId, DateTime startTime)
        {
            if (string.IsNullOrEmpty(userId))
            {
                this.initialTimeStamp = startTime;
            }
            else
            {
                if (!this.IsUserActive(userId))
                {
                    ActivateUser(userId, startTime);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="workstationName"> </param>
        /// <param name="storeNumber"> </param>
        /// <param name="endTime"> </param>
        public void EndSession(string userId, string workstationName, string storeNumber, DateTime endTime)
        {
            if (string.IsNullOrEmpty(userId))
            {
                this.lastTimeStamp = endTime;
            }
            else
            {
                if (IsUserActive(userId))
                {
                    DeActivateUser(userId, workstationName, storeNumber, endTime);
                }
            }
        }

        public void AddStoredProcCall(string userId, string procName, long totalTime, long totalData, long latency, bool force = false)
        {
            Dictionary<string, TupleType<long, long, long>> innerSet;
            if (CollectionUtilities.isNotEmptyContainsKey(this.storedProcCounts, userId))
            {
                innerSet = this.storedProcCounts[userId];
            }
            else
            {
                innerSet = new Dictionary<string, TupleType<long, long, long>>();
                this.storedProcCounts.Add(userId, innerSet);
            }

            TupleType<long, long, long> procStats;
            if (CollectionUtilities.isNotEmptyContainsKey(innerSet, procName))
            {
                procStats = innerSet[procName];
            }
            else
            {
                procStats = new TupleType<long, long, long>(0L,0L,0L);
                innerSet.Add(procName, procStats);
            }

            //---- Update stats ----//

            //Update count
            procStats.Left += 1;

            //Update total time
            procStats.Mid += ((totalTime > 0) ? totalTime : 1); //Min is 1 ms

            //Update data amount
            procStats.Right += totalData;

            //Store the data
            CPNHSDataVO cvo;
            AddCaptureObject(CPNHSDataVO.PackDataType.STOREDPROC, userId, out cvo);

            //Fill in capture data
            cvo.PackType |= CPNHSDataVO.PackDataType.STOREDPROC;
            cvo.PackType |= CPNHSDataVO.PackDataType.DATAXFERIN;
            //Data rate must be in bytes per second
            cvo.CurrentDataRateIn += (decimal)procStats.Right / (procStats.Mid * 1000);
            cvo.CurrentLatency += latency;
            cvo.TotalTimeTxferDataIn += procStats.Mid;
            cvo.NumberTransactionsIn += 1;
            //Submit the data
            UpdateInternalData(userId, force);
        }

        public void AddGranularStoredProcData(string userId, string procName, long callPrep, long callWait, long callProcess, long callTotal, long dataTransferAmt)
        {
            List<QuadType<long, long, long, long>> innerList;
            if (CollectionUtilities.isNotEmptyContainsKey(this.granularStoredProcCounts, userId))
            {
                innerList = this.granularStoredProcCounts[userId];
            }
            else
            {
                innerList = new List<QuadType<long, long, long, long>>();
                this.granularStoredProcCounts.Add(userId, innerList);
            }

            var procStats = new QuadType<long, long, long, long>(callPrep, callWait, callProcess, callTotal);
            innerList.Add(procStats);

            //Get the capture object
            CPNHSDataVO cvo = null;
            AddCaptureObject(CPNHSDataVO.PackDataType.GRANSTOREDPROC, userId, out cvo);

            //Fill in capture data
            cvo.PackType |= CPNHSDataVO.PackDataType.GRANSTOREDPROC;
            cvo.PackType |= CPNHSDataVO.PackDataType.DATAXFERIN;
            cvo.ClientProcName = procName;
            cvo.ClientCallPrepTime = callPrep;
            cvo.ClientCallProcessTime = callProcess;
            cvo.ClientCallTotalTime = callTotal;
            cvo.ClientCallWaitTime = callWait;
            cvo.NumberTransactionsIn += 1;
            cvo.TotalTimeTxferDataIn += callTotal;
            cvo.CurrentLatency += callWait;
            //Must be in bytes per second
            cvo.CurrentDataRateIn += (decimal)dataTransferAmt / (callTotal * 1000);

            //Force this call through as we need every granular proc data point
            UpdateInternalData(userId, true);
        }

        public void AddDataTransIn(string userId, long amount, long latency, long timeToReceive, bool force)
        {
            //Get the capture object
            CPNHSDataVO cvo;
            AddCaptureObject(CPNHSDataVO.PackDataType.DATAXFERIN, userId, out cvo);

            //Fill in capture data
            cvo.PackType |= CPNHSDataVO.PackDataType.DATAXFERIN;
            if (timeToReceive <= 0) timeToReceive = 1; //Min receive time is 1 ms
            cvo.TotalTimeTxferDataIn += timeToReceive;
            //Must be in bytes per second
            cvo.CurrentDataRateIn = (decimal)amount / (timeToReceive * 1000);
            cvo.CurrentLatency += latency;
            cvo.NumberTransactionsIn += 1;

            //Update internal data
            UpdateInternalData(userId, force);
        }

        public void AddDataTransOut(string userId, long amount, long latency, long timeToSend, bool force = false)
        {
            //Get the capture object
            CPNHSDataVO cvo;
            AddCaptureObject(CPNHSDataVO.PackDataType.DATAXFEROUT, userId, out cvo);

            //Fill in capture data
            cvo.PackType |= CPNHSDataVO.PackDataType.DATAXFEROUT;
            if (timeToSend <= 0) timeToSend = 1; //Min send time is 1 ms
            cvo.TotalTimeTxferDataOut += timeToSend;
            //Must be in bytes per second
            cvo.CurrentDataRateOut = (decimal)amount / (timeToSend * 1000);
            cvo.CurrentLatency += latency;
            cvo.NumberTransactionsOut += 1;

            //Update the internal data
            UpdateInternalData(userId, force);
        }
        #endregion
    }
}
