using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects.Performance;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database.Procedures
{
    public static class CPNHSProcedures
    {
        /*
         * PROCEDURE insert_cpnhs (p_curdata_in      IN NUMBER,  -- bytes/sec
                                   p_curdata_out     IN NUMBER,  -- bytes/sec
                                   p_avgdata_in      IN NUMBER,  -- bytes/sec
                                   p_avgdata_out     IN NUMBER,  -- bytes/sec
                                   p_cur_latency     IN NUMBER,  -- ms
                                   p_avg_latency     IN NUMBER,  -- ms
                                   p_tottime_in      IN NUMBER,  -- ms
                                   p_tottime_out     IN NUMBER,  -- ms
                                   p_numtrans_in     IN NUMBER,  -- count - number times received data
                                   p_numtrans_out    IN NUMBER,  -- count - number times sent data
                                   p_totdata_in      IN NUMBER,  -- bytes - number bytes received
                                   p_totdata_out     IN NUMBER,  -- bytes - number bytes sent
                                   p_cli_procname    IN VARCHAR2,-- procedure name
                                   p_cli_hifreq_proc IN VARCHAR2,-- procedure called the most during the session
                                   p_cli_hitime_proc IN VARCHAR2,-- procedure consuming the most time during the session
                                   p_cli_hidata_proc IN VARCHAR2,-- procedure consuming the most data 
                                   p_cli_callprep    IN NUMBER,  -- ms
                                   p_cli_callwait    IN NUMBER,  -- ms
                                   p_cli_callproc    IN NUMBER,  -- ms
                                   p_cli_calltot     IN NUMBER,  -- ms
                                   p_store_number    IN VARCHAR2,-- store name
                                   p_wkst_name       IN VARCHAR2,-- workstation name
                                   p_user_id         IN VARCHAR2,-- user id
                                   p_tot_sess_time   IN NUMBER,  -- seconds - session is alive
                                   p_tot_sess_act    IN NUMBER,  -- seconds - session is active
                                   p_tot_sess_inact  IN NUMBER,  -- seconds - session is inactive
                                   p_num_trans_tend  IN NUMBER,  -- count  - number transactions tendered
                                   p_tot_trans_tend  IN NUMBER,  -- amount - total tendered during session
                                   o_error_code      OUT NUMBER, -- error code
                                   o_error_text      OUT VARCHAR2
         */

        public static bool InsertCPNHSData(CPNHSDataVO cpnhsData)
        {
            if (cpnhsData == null)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "CPNHSProcedures", "Invalid data capture object");
                }
                BasicExceptionHandler.Instance.AddException(
                    "InsertCPNHSData Failed",
                    new ApplicationException("InsertCPNHSData Failed: Data capture object is null"));
            }

            //
            if (GlobalDataAccessor.Instance == null ||
                !GlobalDataAccessor.Instance.IsDataAccessorValid())
            {
                if (FileLogger.Instance.IsLogWarn)
                {
                    FileLogger.Instance.logMessage(LogLevel.WARN, "CPNHSProcedures", "Invalid data accessor");
                }
                BasicExceptionHandler.Instance.AddException(
                    "InsertCPNHSData Failed",
                    new ApplicationException("InsertCPNHSData Failed: Data accessor instance is invalid"));
                return (false);
            }

            var inParams = new List<OracleProcParam>
                           {
                               new OracleProcParam("p_curdata_in", cpnhsData.CurrentDataRateIn),
                               new OracleProcParam("p_curdata_out", cpnhsData.CurrentDataRateOut),
                               new OracleProcParam("p_avgdata_in", cpnhsData.AverageDataRateIn),
                               new OracleProcParam("p_avgdata_out", cpnhsData.AverageDataRateOut),
                               new OracleProcParam("p_cur_latency", cpnhsData.CurrentLatency),
                               new OracleProcParam("p_avg_latency", cpnhsData.AverageLatency),
                               new OracleProcParam("p_tottime_in", cpnhsData.TotalTimeTxferDataIn),
                               new OracleProcParam("p_tottime_out", cpnhsData.TotalTimeTxferDataOut),
                               new OracleProcParam("p_numtrans_in", cpnhsData.NumberTransactionsIn),
                               new OracleProcParam("p_numtrans_out", cpnhsData.NumberTransactionsOut),
                               new OracleProcParam("p_totdata_in", cpnhsData.NumberTransactionsIn),
                               new OracleProcParam("p_totdata_out", cpnhsData.NumberTransactionsOut),
                               new OracleProcParam("p_cli_procname", cpnhsData.ClientProcName),
                               new OracleProcParam("p_cli_highfreq_proc", cpnhsData.ClientProcTopCalledName),
                               new OracleProcParam("p_cli_hitime_proc", cpnhsData.ClientProcTopTimeName),
                               new OracleProcParam("p_cli_hidata_proc", cpnhsData.ClientProcTopDataName),
                               new OracleProcParam("p_cli_callprep", cpnhsData.ClientCallPrepTime),
                               new OracleProcParam("p_cli_callwait", cpnhsData.ClientCallWaitTime),
                               new OracleProcParam("p_cli_callproc", cpnhsData.ClientCallProcessTime),
                               new OracleProcParam("p_cli_calltot", cpnhsData.ClientCallTotalTime),
                               new OracleProcParam("p_store_number", cpnhsData.StoreNumber),
                               new OracleProcParam("p_wkst_name", cpnhsData.WorkstationName),
                               new OracleProcParam("p_user_id", cpnhsData.UserID),
                               new OracleProcParam("p_tot_sess_time", cpnhsData.TotalSessionTime),
                               new OracleProcParam("p_tot_sess_act", cpnhsData.TotalSessionTimeActive),
                               new OracleProcParam("p_tot_sess_inact", cpnhsData.TotalSessionTimeInActive),
                               new OracleProcParam("p_num_trans_tend", cpnhsData.NumberTransactionsTendered),
                               new OracleProcParam("p_tot_trans_tend", cpnhsData.TotalAmountTendered),
                           };
            bool retVal;
            DataSet outputDataSet;
            var errorCode = string.Empty;
            var errorText = string.Empty;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand(
                    "ccsowner", 
                    "cpnhs",
                    "insert_cpnhs", 
                    inParams,
                    null, 
                    "o_error_code",
                    "o_error_text",
                    out outputDataSet);

                errorCode = GlobalDataAccessor.Instance.OracleDA.ErrorCode;
                errorText = GlobalDataAccessor.Instance.OracleDA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("InsertCPNHS Failed", oEx);
                errorCode += " --- InsertCPNHS Failed";
                errorText += " --- OracleException thrown: " + oEx.Message;
                FileLogger.Instance.logMessage(LogLevel.ERROR, 
                    "CPNHSProcedures.InsertCPNHSData - Oracle exception - Code {0} Message {1}", 
                    oEx.Number.ToString(), oEx.Message);
                return (false);
            }

            return (true);
        }
    }
}
