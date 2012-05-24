using System;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Utility.Exception;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database.Procedures
{
    public static class TellerProcedures
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeNumber"></param>
        /// <param name="userId"></param>
        /// <param name="isCredit"></param>
        /// <param name="cashDrawer"></param>
        /// <param name="amount"></param>
        /// <param name="receiptNumber"></param>
        /// <param name="receiptNumber"></param>
        /// <param name="workstation"></param>
        /// <param name="date"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteUpdateTeller(
            string storeNumber,
            string userId,
            bool isCredit,
            string cashDrawer,
            decimal amount,
            string receiptNumber,
            string workstation,
            DateTime date,
            out string errorCode,
            out string errorText)
        {
            //Initialize outputs
            errorCode = string.Empty;
            errorText = string.Empty;

            //Validate data accessor
            if (GlobalDataAccessor.Instance == null ||
                !GlobalDataAccessor.Instance.IsDataAccessorValid())
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateTellerFailed", new ApplicationException("Data accessor class is invalid"));
                errorCode = "ExecuteUpdateTellerFailed";
                errorText = "Invalid data accessor";
                return(false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Prepare input parameter list
            var iParams = new List<OracleProcParam>
                          {
                              new OracleProcParam("p_store_number", storeNumber),
                              new OracleProcParam("p_user_name", userId),
                              new OracleProcParam("p_is_credit", ((isCredit) ? "T" : "F")),
                              new OracleProcParam("p_cash_drawer", cashDrawer),
                              new OracleProcParam("p_amount", amount),
                              new OracleProcParam("p_loan_number", receiptNumber),
                              new OracleProcParam("p_workstation", workstation),
                              new OracleProcParam("p_sys_date", date)
                          };

            //Make stored proc call
            bool retVal = false;
            try
            {
                DataSet outDSet;
                retVal = dA.issueSqlStoredProcCommand("ccsowner",
                    "pawn_store_procs", "update_teller_new_pawn_loan", 
                    iParams, null, "o_error_code",
                    "o_error_desc", out outDSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateTeller Failed", oEx);
                errorCode = "ExecuteUpdateTellerFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateTeller Failed: return value is false", new ApplicationException(dA.ErrorCode + ":" + dA.ErrorDescription));
                errorCode = "ExecuteUpdateTellerFailed: " + dA.ErrorCode + ": " + dA.ErrorDescription;
                errorText = "Return value is false";
                return (false);
            }

            errorCode = "0";
            errorText = "Success";

            return (true);
        }

        /// <summary>
        /// Updates the teller tables for a void transaction
        /// </summary>
        /// <param name="receiptDtNumber"></param>
        /// <param name="storeNumber"></param>
        /// <param name="userId"></param>
        /// <param name="workstation"></param>
        /// <param name="refNumber"></param>
        /// <param name="tranDate"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool UpdateTellerOnVoid(
            Int64 receiptDtNumber,
            string storeNumber,
            string userId,
            string workstation,
            string refNumber,
            string tranDate,
            out string errorCode,
            out string errorText
            )
        {
            //Initialize outputs
            errorCode = string.Empty;
            errorText = string.Empty;

            //Validate data accessor
            if (GlobalDataAccessor.Instance == null ||
                GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateTellerOnVoidFailed", new ApplicationException("Data accessor class is invalid"));
                errorCode = "ExecuteUpdateTellerOnVoidFailed";
                errorText = "Invalid data accessor";
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Prepare input parameter list
            var iParams = new List<OracleProcParam>
                          {
                              new OracleProcParam("p_receipt_detail_number", receiptDtNumber),
                              new OracleProcParam("p_store_number", storeNumber),
                              new OracleProcParam("p_user_name", userId),
                              new OracleProcParam("p_workstation", workstation),
                              new OracleProcParam("p_loan_number", refNumber),
                              new OracleProcParam("p_transaction_date", tranDate)
                          };

            //Make stored proc call
            bool retVal = false;
            try
            {
                DataSet outDSet;
                retVal = dA.issueSqlStoredProcCommand("ccsowner",
                    "pawn_store_procs", "update_teller_for_void",
                    iParams, null, "o_error_code",
                    "o_error_desc", out outDSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateTellerForVoid Failed", oEx);
                errorCode = "ExecuteUpdateTellerForVoidFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateTellerForVOid Failed: return value is false", new ApplicationException(dA.ErrorCode + ":" + dA.ErrorDescription));
                errorCode = "ExecuteUpdateTellerForVoidFailed: " + dA.ErrorCode + ": " + dA.ErrorDescription;
                errorText = "Return value is false";
                return (false);
            }

            errorCode = "0";
            errorText = "Success";


            return true;
        }
    }
}
