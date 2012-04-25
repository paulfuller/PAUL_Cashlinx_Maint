using System;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;

namespace Common.Libraries.Objects.Performance
{
    public class CPNHSDataVO
    {
        [Flags]
        public enum PackDataType
        {
            SESSINFO       = 0x01,
            DATAXFERIN     = 0x02,
            DATAXFEROUT    = 0x04,
            TENDER         = 0x08,
            STOREDPROC     = 0x10,
            GRANSTOREDPROC = 0x20
        }

        public PackDataType PackType { set; get; }
        public decimal CurrentDataRateIn { set; get; }
        public decimal CurrentDataRateOut { set; get; }
        public decimal AverageDataRateIn { set; get; }
        public decimal AverageDataRateOut { set; get; }
        public decimal CurrentLatency { set; get; }
        public decimal AverageLatency { set; get; }
        public long TotalTimeTxferDataOut { set; get; }
        public long TotalTimeTxferDataIn { set; get; }
        public long NumberTransactionsIn { set; get; }
        public long NumberTransactionsOut { set; get; }
        public long ClientCallPrepTime { set; get; }
        public long ClientCallWaitTime { set; get; }
        public long ClientCallProcessTime { set; get; }
        public long ClientCallTotalTime { set; get; }
        public string StoreNumber { set; get; }
        public string WorkstationName { set; get; }
        public string UserID { set; get; }
        public long TotalSessionTime { set; get; }
        public long TotalSessionTimeActive { set; get; }
        public long TotalSessionTimeInActive { set; get; }
        public long NumberTransactionsTendered { set; get; }
        public decimal TotalAmountTendered { set; get; }
        public string ClientProcName { set; get; }
        public string ClientProcTopCalledName { set; get; }
        public string ClientProcTopTimeName { set; get; }
        public string ClientProcTopDataName { set; get; }

        public CPNHSDataVO()
        {
            this.PackType = 0;
            this.CurrentDataRateIn = 0.0M;
            this.CurrentDataRateOut = 0.0M;
            this.AverageDataRateIn = 0.0M;
            this.AverageDataRateOut = 0.0M;
            this.CurrentLatency = 0.0M;
            this.AverageLatency = 0.0M;
            this.TotalTimeTxferDataOut = 0L;
            this.TotalTimeTxferDataIn = 0L;
            this.NumberTransactionsIn = 0L;
            this.NumberTransactionsOut = 0L;
            this.ClientCallPrepTime = 0L;
            this.ClientCallWaitTime = 0L;
            this.ClientCallProcessTime = 0L;
            this.ClientCallTotalTime = 0L;
            this.StoreNumber = string.Empty;
            this.WorkstationName = string.Empty;
            this.UserID = string.Empty;
            this.TotalSessionTime = 0L;
            this.TotalSessionTimeActive = 0L;
            this.TotalSessionTimeInActive = 0L;
            this.NumberTransactionsTendered = 0L;
            this.TotalAmountTendered = 0.0M;
            this.ClientProcName = string.Empty;
            this.ClientProcTopCalledName = string.Empty;
            this.ClientProcTopTimeName = string.Empty;
            this.ClientProcTopDataName = string.Empty;
        }

        public CPNHSDataVO(CPNHSDataVO cvo)
        {
            this.PackType = cvo.PackType;
            this.CurrentDataRateIn = cvo.CurrentDataRateIn;
            this.CurrentDataRateOut = cvo.CurrentDataRateOut;
            this.AverageDataRateIn = cvo.AverageDataRateIn;
            this.AverageDataRateOut = cvo.AverageDataRateOut;
            this.CurrentLatency = cvo.CurrentLatency;
            this.AverageLatency = cvo.AverageLatency;
            this.TotalTimeTxferDataOut = cvo.TotalTimeTxferDataOut;
            this.TotalTimeTxferDataIn = cvo.TotalTimeTxferDataIn;
            this.NumberTransactionsIn = cvo.NumberTransactionsIn;
            this.NumberTransactionsOut = cvo.NumberTransactionsOut;
            this.ClientCallPrepTime = cvo.ClientCallPrepTime;
            this.ClientCallWaitTime = cvo.ClientCallWaitTime;
            this.ClientCallProcessTime = cvo.ClientCallProcessTime;
            this.ClientCallTotalTime = cvo.ClientCallTotalTime;
            this.StoreNumber = cvo.StoreNumber;
            this.WorkstationName = cvo.WorkstationName;
            this.UserID = cvo.UserID;
            this.TotalSessionTime = cvo.TotalSessionTime;
            this.TotalSessionTimeActive = cvo.TotalSessionTimeActive;
            this.TotalSessionTimeInActive = cvo.TotalSessionTimeInActive;
            this.NumberTransactionsTendered = cvo.NumberTransactionsTendered;
            this.TotalAmountTendered = cvo.TotalAmountTendered;
            this.ClientProcName = cvo.ClientProcName;
            this.ClientProcTopCalledName = cvo.ClientProcTopCalledName;
            this.ClientProcTopTimeName = cvo.ClientProcTopTimeName;
            this.ClientProcTopDataName = cvo.ClientProcTopDataName;
        }
    }
}