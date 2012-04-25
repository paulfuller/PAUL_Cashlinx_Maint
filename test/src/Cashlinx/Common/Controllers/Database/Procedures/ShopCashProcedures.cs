using System;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Security;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database.Procedures
{
    public class ShopCashProcedures
    {
        private const string CASHDRAWER_LIST = "CashdrawerAssignments";
        private const string CASHDRAWER_AUX_LIST = "CashdrawerAuxAssignments";
        private const string CASHDRAWER_TRANSACTIONS = "CashdrawerTransactions";
        private const string CASHDRAWER_PDA_TRANSACTIONS = "CashdrawerPDATransactions";
        private const string SHOP_CASH_POSITIONS = "shopcashpositions";
        private const string CASHDRAWER_AMOUNTS = "cashdraweramounts";
        private const string STORETRANSFERDATA = "storetransfers";
        private const string TRANSFERDETAILS = "storecashtransferdetails";
        private const string DENOMINATIONDETAILS = "storecashdenomdetails";
        private const string BANKINFODETAILS = "bankinfodetails";
        private const string TUBEDENOMINATIONS = "tubesafedenominations";

        /// <summary>
        /// Get the assignment of cash drawer to users and the name of the
        /// safe cash drawer for the store
        /// </summary>
        /// <param name="storeNumber"></param>
        /// <param name="safeCashDrawerName"></param>
        /// <param name="cashdrawerAssignments"></param>
        /// <param name="cashdrawerAuxAssignments"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public static bool GetCashDrawerAssignments(
            string storeNumber,
            DesktopSession desktopSession,
            out string safeCashDrawerName,
            out DataTable cashdrawerAssignments,
            out DataTable cashdrawerAuxAssignments,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Set default output params
            cashdrawerAssignments = null;
            cashdrawerAuxAssignments = null;
            safeCashDrawerName = string.Empty;
            errorCode = string.Empty;
            errorMesg = string.Empty;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetCashDrawerAssignmentsFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("o_safe_name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 100));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_cashdrawer_list", CASHDRAWER_LIST));
                refCursArr.Add(new PairType<string, string>("r_cashdrawer_aux_list", CASHDRAWER_AUX_LIST));
                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "get_cashdrawer_assignments", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling get_Cashdrawer_assignments stored procedure", oEx);
                    errorCode = "GetCashDrawerAssignments";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
                if (outputDataSet.Tables.Count > 0)
                {
                    cashdrawerAssignments = outputDataSet.Tables[CASHDRAWER_LIST];
                    cashdrawerAuxAssignments = outputDataSet.Tables[CASHDRAWER_AUX_LIST];
                    DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                    if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
                    {
                        DataRow dr = outputDt.Rows[0];
                        if (dr != null && dr.ItemArray.Length > 0)
                        {
                            object obj = dr.ItemArray.GetValue(1);
                            if (obj != null)
                            {
                                safeCashDrawerName = (string)obj;
                                return true;
                            }
                        }
                    }
                }

                errorCode = "GetCashDrawerAssignmentsFailed";
                errorMesg = "Operation failed";
                return (false);
            }
            errorCode = "GetCashDrawerAssignmentsFailed";
            errorMesg = "No valid input parameters given";
            return (false);
        }

        /// <summary>
        /// Check if the user name and password supplied are valid as a safe user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="storeNumber"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public static bool VerifySafeUser(
            string userName,
            string password,
            string storeNumber,
            DesktopSession desktopSession,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "VerifySafeUserFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_user_name", userName));

            inParams.Add(new OracleProcParam("p_password", password));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "verify_safe_user", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling verify_safe_user stored procedure", oEx);
                    errorCode = "verifysafeuser";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
            }
            else
            {
                errorCode = "verify_safe_user Failed";
                errorMesg = "No valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorMesg = "Success";
            return true;
        }


        /// <summary>
        /// Call to set the open flag to 1 for the safe cash drawer of the store
        /// </summary>
        /// <param name="cashdrawerId"></param>
        /// <param name="storeNumber"></param>
        /// <param name="modifyUserName"></param>
        /// <param name="transactionDate"></param>
        /// <param name="status"></param>
        /// <param name="workstationId"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public static bool UpdateSafeStatus(
            string cashdrawerId,
            string storeNumber,
            string modifyUserName,
            string transactionDate,
            string status,
            string workstationId,
            DesktopSession desktopSession,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdateSafeStatusFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_cashdrawer_id", cashdrawerId));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_modify_user_name", modifyUserName));

            inParams.Add(new OracleProcParam("p_transaction_date", transactionDate));

            inParams.Add(new OracleProcParam("p_cashdrawerstatus", status));

            inParams.Add(new OracleProcParam("p_workstation_id", workstationId));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "update_safe_status", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling update_safe_status stored procedure", oEx);
                    errorCode = "UpdateSafeStatus";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
            }
            else
            {
                errorCode = "UpdatesafeStatusFailed";
                errorMesg = "No Valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorMesg = "Success";
            return (true);
        }

        /// <summary>
        /// Assign a cashdrawer to a workstation id
        /// Called when a user who is assigned a cash drawer logs in a terminal
        /// </summary>
        /// <param name="cashdrawerId"></param>
        /// <param name="workstationId"></param>
        /// <param name="storeNumber"></param>
        /// <param name="modifyUserName"></param>
        /// <param name="transactionDate"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public static bool AssignWorkstationtoCashDrawer(
            string cashdrawerId,
            string workstationId,
            string storeNumber,
            string modifyUserName,
            string transactionDate,
            DesktopSession desktopSession,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "AssignWorkstationtoCashDrawer";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_cashdrawer_id", cashdrawerId));

            inParams.Add(new OracleProcParam("p_workstation_id", workstationId));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_modify_user_name", modifyUserName));

            inParams.Add(new OracleProcParam("p_transaction_date", transactionDate));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "assign_workstation_to_cd", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling assign_workstation_to_cd stored procedure", oEx);
                    errorCode = "assign_workstation_to_cd";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
            }
            else
            {
                errorCode = "AssignworkstationtoCashDrawerFailed";
                errorMesg = "No Valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorMesg = "Success";
            return (true);
        }


        public static bool GetTellerEvent(
            string cashdrawerId,
            DesktopSession desktopSession,
            out string workstationId,
            out string cashDrawerEvent,
            out string errorCode,
            out string errorMesg)
        {
            workstationId = string.Empty;
            cashDrawerEvent = string.Empty;
            errorMesg=string.Empty;
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetTellerEventFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_cashdrawer_id", cashdrawerId));
            inParams.Add(new OracleProcParam("o_workstation_id", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 47));
            inParams.Add(new OracleProcParam("o_cashdrawer_event", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 20));


            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "getTellerEvent", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling getTellerEvent stored procedure", oEx);
                    errorCode = "getTellerEvent";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
                if (!string.IsNullOrEmpty(dA.ErrorDescription) && dA.ErrorDescription.ToUpper().Contains("NO TELLER EVENT FOUND"))
                {
                    workstationId = string.Empty;
                    cashDrawerEvent = string.Empty;
                    errorCode = "100";
                    errorMesg = "No Other CashDrawer Event entry found";
                    return true;
                }
                //Get output values
                DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
                {
                    DataRow dr = outputDt.Rows[0];
                    if (dr != null && dr.ItemArray.Length > 0)
                    {
                        object obj = dr.ItemArray.GetValue(1);
                        if (obj != null)
                        {
                            var wkId = (string)obj;
                            workstationId = Utilities.GetStringValue(wkId);
                        }
                    }
                    DataRow dr1 = outputDt.Rows[1];
                    if (dr1 != null && dr1.ItemArray.Length > 0)
                    {
                        object obj = dr1.ItemArray.GetValue(1);
                        if (obj != null)
                        {
                            var cdevent = (string)obj;
                            cashDrawerEvent = Utilities.GetStringValue(cdevent);
                        }
                    }

                }

            }
            else
            {
                errorCode = "getTellerEventFailed";
                errorMesg = "No Valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorMesg = "Success";
            return (true);
        }



        public static bool InsertTellerEvent(
            string cashdrawerId,
            string workstationId,
            string cashdrawerEvent,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "InsertTellerEvent";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_cashdrawer_id", cashdrawerId));

            inParams.Add(new OracleProcParam("p_workstation_id", workstationId));

            inParams.Add(new OracleProcParam("p_cashdrawer_event", cashdrawerEvent));


            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "insertTellerEvent", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling insertTellerEvent stored procedure", oEx);
                    errorCode = "insertTellerEvent";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
            }
            else
            {
                errorCode = "insertTellerEventFailed";
                errorMesg = "No Valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorMesg = "Success";
            return (true);
        }


        public static bool RemoveTellerEvent(
            string cashdrawerId,
            string workstationId,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "RemoveTellerEvent";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //inParams.Add(new OracleProcParam("p_cashdrawer_id", cashdrawerId));

            inParams.Add(new OracleProcParam("p_workstation_id", workstationId));




            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "deleteCashDrawerEvent", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling RemoveTellerEvent stored procedure", oEx);
                    errorCode = "RemoveTellerEvent";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
            }
            else
            {
                errorCode = "RemoveTellerEventFailed";
                errorMesg = "No Valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorMesg = "Success";
            return (true);
        }

        /// <summary>
        /// get the cash drawer amount for the cash drawer
        /// </summary>
        /// <param name="cashDrawerID"></param>
        /// <param name="priorBalance"></param>
        /// <param name="cashDrawerAmount"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <param name="accountingdate"></param>
        /// <returns></returns>
        public static bool GetCashDrawerAmount(
            string cashDrawerID,
            string accountingdate,
            string priorBalance,
            DesktopSession desktopSession,
            out DataTable cashDrawerAmount,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Set default output params
            cashDrawerAmount = null;
            errorCode = string.Empty;
            errorMesg = string.Empty;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetCashDrawerAmountFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_cashdrawerid", cashDrawerID));

            inParams.Add(new OracleProcParam("p_accountingdate", accountingdate));

            inParams.Add(new OracleProcParam("p_prior_balance", priorBalance));

            //inParams.Add(new OracleProcParam("o_cashdrawer_amount", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_cashdrawer_amount", CASHDRAWER_AMOUNTS));

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "getamount_for_cashdrawer", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling getamount_for_cashdrawer stored procedure", oEx);
                    errorCode = "GetAmountForCashDrawer";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
                if (outputDataSet.Tables.Count > 0)
                {
                    cashDrawerAmount = outputDataSet.Tables[CASHDRAWER_AMOUNTS];

                    return true;
                }

                errorCode = "GetAmountForCashDrawerFailed-NO output data";
                errorMesg = "Stored procedure succeeded but no data returned";
                return (true);
            }
            errorCode = "GetAmountForCashDrawerFailed";
            errorMesg = "No valid input parameters given";
            return (false);
        }

        /// <summary>
        /// get the cash drawer amount for the cash drawer
        /// </summary>
        /// <param name="cashDrawerID"></param>
        /// <param name="cashDrawerAmount"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public static bool GetCashDrawerAmount(
            string cashDrawerID,
            DesktopSession desktopSession,
            out decimal cashDrawerAmount,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Set default output params
            cashDrawerAmount = 0.0m;
            errorCode = string.Empty;
            errorMesg = string.Empty;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetCashDrawerAmountFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_cashdrawerid", cashDrawerID));

            inParams.Add(new OracleProcParam("o_cashdrawer_amount", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "getamount_for_cashdrawer", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling getamount_for_cashdrawer stored procedure", oEx);
                    errorCode = "GetAmountForCashDrawer";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
                //Get output number
                DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
                {
                    DataRow dr = outputDt.Rows[0];
                    if (dr != null && dr.ItemArray.Length > 0)
                    {
                        object obj = dr.ItemArray.GetValue(1);
                        if (obj != null)
                        {
                            var totAmount = (string)obj;
                            cashDrawerAmount = decimal.Parse(totAmount);
                            errorCode = "0";
                            errorMesg = "Success";
                            return (true);
                        }
                    }
                }

                errorCode = "GetAmountForCashDrawerFailed-NO output data";
                errorMesg = "Stored procedure succeeded but no data returned";
                return (true);
            }
            errorCode = "GetAmountForCashDrawerFailed";
            errorMesg = "No valid input parameters given";
            return (false);
        }

        public static bool GetCashDrawerBeginningAmount(
            string cashDrawerID,
            string balanceDate,
            string priorBalance,
            DesktopSession desktopSession,
            out decimal cashDrawerBegAmount,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Set default output params
            cashDrawerBegAmount = 0.0m;
            errorCode = string.Empty;
            errorMesg = string.Empty;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetCashDrawerBeginningAmountFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_cashdrawerid", cashDrawerID));

            inParams.Add(new OracleProcParam("p_balance_date", balanceDate));

            inParams.Add(new OracleProcParam("p_prior_balance", priorBalance));

            inParams.Add(new OracleProcParam("o_cashdrawer_amount", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "get_beginning_amount_for_CD", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling get_beginning_amount_for_CD stored procedure", oEx);
                    errorCode = "GetCashDrawerBeginningAmount";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
                //Get output number
                DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
                {
                    DataRow dr = outputDt.Rows[0];
                    if (dr != null && dr.ItemArray.Length > 0)
                    {
                        object obj = dr.ItemArray.GetValue(1);
                        if (obj != null)
                        {
                            var totAmount = (string)obj;
                            cashDrawerBegAmount = decimal.Parse(totAmount);
                            errorCode = "0";
                            errorMesg = "Success";
                            return (true);
                        }
                    }
                }

                errorCode = "GetCashDrawerBeginningAmountFailed-NO output data";
                errorMesg = "Stored procedure succeeded but no data returned";
                return (true);
            }
            errorCode = "GetAmountForCashDrawerFailed";
            errorMesg = "No valid input parameters given";
            return (false);
        }

        public static bool TopsTransfer(
            OracleDataAccessor oda,
            string cashDrawerId,
            string userName,
            string transactionTime,
            string workstationId,
            string storeNumber,
            decimal balanceAmount,
            string transactionDate,
            string modifyUserName,
            string comments,
            out string errorCode,
            out string errorMesg)
        {
            string opCode = "";

            opCode = balanceAmount > 0 ? TOPSTRANSFEROPERATIONS.REOUT.ToString() : TOPSTRANSFEROPERATIONS.REIN.ToString();

            if (oda == null)
            {
                errorCode = "BalanceCashDrawer";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_cashdrawer_id", cashDrawerId));

            inParams.Add(new OracleProcParam("p_user_name", userName));

            inParams.Add(new OracleProcParam("p_workstation_id", workstationId));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_cashdrawerbalance", balanceAmount));

            inParams.Add(new OracleProcParam("p_user_id", modifyUserName));

            inParams.Add(new OracleProcParam("p_transaction_date", transactionDate));

            inParams.Add(new OracleProcParam("p_operation_code", opCode));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = oda.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "insert_tops_transfer", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling insert_tops_transfer stored procedure", oEx);
                    errorCode = "insert_tops_transfer";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oda.ErrorCode;
                    errorMesg = oda.ErrorDescription;
                    return (false);
                }
            }
            else
            {
                errorCode = "insert_tops_transferFailed";
                errorMesg = "No Valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorMesg = "Success";
            return (true);
        }

        /// <summary>
        /// Get all the pawn transactions that happened in the cash drawer
        /// on a particular day
        /// </summary>
        /// <param name="cashdrawerId"></param>
        /// <param name="transactionDate"></param>
        /// <param name="storeNumber"></param>
        /// <param name="cashdrawerPawnTransactions"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public static bool GetPawnCashDrawerTransactions(
            string cashdrawerId,
            string transactionDate,
            string storeNumber,
            string PreviousBalance,
            DesktopSession desktopSession,
            out DataTable cashdrawerPawnTransactions,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Set default output params
            cashdrawerPawnTransactions = null;
            errorCode = string.Empty;
            errorMesg = string.Empty;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetPawnCashDrawerTransactionsFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_cashdrawer_id", cashdrawerId));

            inParams.Add(new OracleProcParam("p_transaction_date", transactionDate));

            inParams.Add(new OracleProcParam("p_prev_balance_flag", PreviousBalance));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_receipt_details", CASHDRAWER_TRANSACTIONS));
                //refCursArr.Add(new PairType<string, string>("o_tops_transfers", TOPS_TRANSFERS));
                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "getPawnCashdrawerTransactions", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling getPawnCashdrawerTransactions stored procedure", oEx);
                    errorCode = "GetPawnCashDrawerTransactions";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    cashdrawerPawnTransactions = outputDataSet.Tables[CASHDRAWER_TRANSACTIONS];
                    return true;
                }

                errorCode = "GetcashdrawerPawnTransactionsFailed";
                errorMesg = "Operation failed";
                return (false);
            }
            errorCode = "GetcashdrawerPawnTransactionsFailed";
            errorMesg = "No valid input parameters given";
            return (false);
        }

        /// <summary>
        /// Get all the PDA transactions that happened against the given cash drawer id
        /// for a given date
        /// </summary>
        /// <param name="oda"></param>
        /// <param name="cashdrawerId"></param>
        /// <param name="transactionDate"></param>
        /// <param name="storeId"></param>
        /// <param name="prevBalanceFlag"></param>
        /// <param name="cashdrawerPdaTransactions"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public static bool GetPdaCashDrawerTransactions(
            OracleDataAccessor oda,
            string cashdrawerId,
            string transactionDate,
            string storeId,
            string prevBalanceFlag,
            out DataTable cashdrawerPdaTransactions,
            out string errorCode,
            out string errorMesg)
        {
            //Set default output params
            cashdrawerPdaTransactions = null;
            errorCode = string.Empty;
            errorMesg = string.Empty;

            if (oda == null)
            {
                errorCode = "GetPdaCashDrawerTransactionsFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_id", storeId));

            inParams.Add(new OracleProcParam("p_cdraw_id", cashdrawerId));

            inParams.Add(new OracleProcParam("p_trans_date", transactionDate));

            inParams.Add(new OracleProcParam("p_prev_balance_flag", prevBalanceFlag));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_receipt_details", CASHDRAWER_PDA_TRANSACTIONS));
                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = oda.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "get_pda_trans_for_Cashdrawer", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling get_pda_trans_for_Cashdrawer stored procedure", oEx);
                    errorCode = "GetPdaCashDrawerTransactions";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oda.ErrorCode;
                    errorMesg = oda.ErrorDescription;
                    return (false);
                }
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    cashdrawerPdaTransactions = outputDataSet.Tables[CASHDRAWER_PDA_TRANSACTIONS];
                    return true;
                }

                errorCode = "GetcashdrawerPdaTransactionsFailed";
                errorMesg = "Operation failed";
                return (false);
            }
            errorCode = "GetcashdrawerPdaTransactionsFailed";
            errorMesg = "No valid input parameters given";
            return (false);
        }

        /// <summary>
        /// Get all the cash positions for all the cash drawers of the store passed in
        /// </summary>
        /// <param name="storeNumber"></param>
        /// <param name="cashdrawerPositions"></param>
        /// <param name="cashDrawerDetails"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public static bool GetShopCashPosition(
            string storeNumber,
            DesktopSession desktopSession,
            out DataTable cashdrawerPositions,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Set default output params
            cashdrawerPositions = null;

            errorCode = string.Empty;
            errorMesg = string.Empty;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetShopCashPositionFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_current_date", ShopDateTime.Instance.ShopDate.ToShortDateString()));
            //inParams.Add(new OracleProcParam("p_prior_balance", "N"));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_cash_position_details", SHOP_CASH_POSITIONS));

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "get_shop_cashposition", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling get_shop_cashposition stored procedure", oEx);
                    errorCode = "GetShopCashPosition";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    cashdrawerPositions = outputDataSet.Tables[SHOP_CASH_POSITIONS];

                    return true;
                }

                errorCode = "GetShopCashPositionFailed";
                errorMesg = "Operation failed";
                return (false);
            }
            errorCode = "GetShopCashPositionFailed";
            errorMesg = "No valid input parameters given";
            return (false);
        }

        /// <summary>
        /// Passing a cash drawer name and user info, inserts a cash drawer row
        /// in the cd_cashdrawer table. If the user passed is not valid
        /// returns with an error
        /// </summary>
        /// <param name="cashdrawerName"></param>
        /// <param name="cashdrawerDescription"></param>
        /// <param name="userName"></param>
        /// <param name="userFirstName"></param>
        /// <param name="userLastName"></param>
        /// <param name="storeNumber"></param>
        /// <param name="transactionDate"></param>
        /// <param name="modifyUserName"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public static bool AddCashDrawer(string cashdrawerName,
                                         string cashdrawerDescription,
                                         string userName,
                                         string userFirstName,
                                         string userLastName,
                                         string storeNumber,
                                         string transactionDate,
                                         string modifyUserName,
                                         DesktopSession desktopSession,
                                         out string errorCode,
                                         out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "AddCashDrawer";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_name", cashdrawerName));

            inParams.Add(new OracleProcParam("p_description", cashdrawerDescription));

            inParams.Add(new OracleProcParam("p_user_first_name", userFirstName));

            inParams.Add(new OracleProcParam("p_user_last_name", userLastName));

            inParams.Add(new OracleProcParam("p_user_name", userName));

            inParams.Add(new OracleProcParam("p_transaction_date", transactionDate));

            inParams.Add(new OracleProcParam("p_modify_user_name", modifyUserName));

            inParams.Add(new OracleProcParam("p_objecttype", "CASHDRAWER"));

            inParams.Add(new OracleProcParam("p_bank_id", storeNumber));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_open_flag", "0"));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "addcashdrawer", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling Addcashdrawer stored procedure", oEx);
                    errorCode = "addcashdrawer";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
            }
            else
            {
                errorCode = "AddcashdrawerFailed";
                errorMesg = "No Valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorMesg = "Success";
            return (true);
        }

        /// <summary>
        /// The cash drawer for which the ID is passed in will be deleted
        /// openflag column value will be set to 3 to indicate deletion.
        /// If deletion was successful a deleteStatus value of 0 is passed
        /// If the cash drawer to be deleted is not in a closed - balanced state
        /// a deletestatus of 1 is passed back
        /// if the cash drawer has balance amount a deletestatus value of 2 is passed back
        /// A deletestatus of 3 indicates an error happened in the call
        /// </summary>
        /// <param name="cashDrawerID"></param>
        /// <param name="deleteStatus"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public static bool DeleteCashDrawer(
            string cashDrawerID,
            DesktopSession desktopSession,
            out decimal deleteStatus,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            deleteStatus = 3;
            errorCode = "";
            errorMesg = "";

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "DeleteCashDrawer";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_cashdrawerid", cashDrawerID));
            inParams.Add(new OracleProcParam("o_delete_status", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "deletecashdrawer", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling deletecashdrawer stored procedure", oEx);
                    errorCode = "Deletecashdrawer";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                    if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
                    {
                        DataRow dr = outputDt.Rows[0];
                        if (dr != null && dr.ItemArray.Length > 0)
                        {
                            object obj = dr.ItemArray.GetValue(1);
                            if (obj != null)
                            {
                                deleteStatus = Utilities.GetDecimalValue(obj, 0);
                                return true;
                            }
                        }
                    }
                }

                errorCode = "DeletecashdrawerFailed";
                errorMesg = "Operation failed";
                return (false);
            }
            errorCode = "DeletecashdrawerFailed";
            errorMesg = "No valid input parameters given";
            return (false);
        }

        /// <summary>
        /// Adds a workstation in cd_workstation table
        /// </summary>
        /// <param name="workstationName"></param>
        /// <param name="storeNumber"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public static bool AddWorkstation(
            string workstationName,
            string storeNumber,
            DesktopSession desktopSession,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "AddWorkstation";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_name", workstationName));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "addworkstation", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling AddWorkstation stored procedure", oEx);
                    errorCode = "AddWorkstation";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
            }
            else
            {
                errorCode = "AddWorkstationFailed";
                errorMesg = "No Valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorMesg = "Success";
            return (true);
        }

        //SP to get the list of safe users in a store
        public static bool GetSafeUsersList(
            string storeNumber,
            DesktopSession desktopSession,
            out DataTable safeUsers,
            out string errorCode,
            out string errorMesg)
        {
            safeUsers = null;
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "getSafeUsersList";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_safe_users", "SAFE_USERS_LIST"));

                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_security_profile",
                        "getsafeuserslist", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling getSafeUsersList stored procedure", oEx);
                    errorCode = "getSafeUsersList";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
                if (outputDataSet.Tables.Count > 0)
                {
                    safeUsers = outputDataSet.Tables["SAFE_USERS_LIST"];
                    errorCode = "0";
                    errorMesg = "Success";
                    return true;
                }

            }
            else
            {
                errorCode = "getSafeUsersListFailed";
                errorMesg = "No Valid input parameters given";
                return (false);
            }
            errorCode = "getSafeUsersListFailed";
            errorMesg = "Safe users data not retrieved";
            return (false);
        }


        /// <summary>
        /// SP call to insert shop transfer data
        /// </summary>
        /// <param name="transferType"></param>
        /// <param name="transferNumber"></param>
        /// <param name="sourceShopNumber"></param>
        /// <param name="destShopNumber"></param>
        /// <param name="transferAmt"></param>
        /// <param name="transportedBy"></param>
        /// <param name="bagNumber"></param>
        /// <param name="comment"></param>
        /// <param name="userID"></param>
        /// <param name="transferDate"></param>
        /// <param name="safeCashDrawerID"></param>
        /// <param name="denominationCurrency"></param>
        /// <param name="denominationData"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public static bool InsertShopTransfer(
            string transferType,
            string sourceShopNumber,
            string destShopNumber,
            decimal transferAmt,
            string transportedBy,
            string bagNumber,
            string comment,
            string userID,
            string transferDate,
            string safeCashDrawerID,
            string denominationCurrency,
            List<string> denominationData,
            DesktopSession desktopSession,
            out int transferNumber,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            transferNumber = 0;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "Insertshopcashtransfer";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_transfer_type", transferType));

            inParams.Add(new OracleProcParam("p_source_shop_number", sourceShopNumber));

            inParams.Add(new OracleProcParam("p_destination_shop_number", destShopNumber));

            inParams.Add(new OracleProcParam("p_transfer_amount", transferAmt));

            inParams.Add(new OracleProcParam("p_transported_by", transportedBy));

            inParams.Add(new OracleProcParam("p_deposit_bag_number", bagNumber));

            inParams.Add(new OracleProcParam("p_comment", comment));

            inParams.Add(new OracleProcParam("p_user_id", userID));

            inParams.Add(new OracleProcParam("p_transfer_date", transferDate));

            inParams.Add(new OracleProcParam("p_safe_cash_drawer_id", safeCashDrawerID));

            inParams.Add(new OracleProcParam("p_denomination_currency", denominationCurrency));

            inParams.Add(new OracleProcParam("p_denomination_details", true, denominationData));

            inParams.Add(new OracleProcParam("o_transfer_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                bool retVal;
                DataSet outputDataSet;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "insertshopcashtransfer", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling Insertshopcashtransfer stored procedure", oEx);
                    errorCode = "Insertshopcashtransfer";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
                //Get output number
                DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
                {
                    DataRow dr = outputDt.Rows[0];
                    if (dr != null && dr.ItemArray.Length > 0)
                    {
                        object obj = dr.ItemArray.GetValue(1);
                        if (obj != null)
                        {
                            var nextNumStr = (string)obj;
                            transferNumber = Int32.Parse(nextNumStr);
                            errorCode = "0";
                            errorMesg = "Success";
                            return (true);
                        }
                    }
                }
            }
            else
            {
                errorCode = "InsertshopcashtransferFailed";
                errorMesg = "No Valid input parameters given";
                return (false);
            }
            errorCode = "1";
            errorMesg = "Insert Shop cash transfer failed";

            return false;
        }

        /// <summary>
        /// Call to a stored procedure to get a list
        /// of shop cash transfers that are either pending or rejected
        /// </summary>
        /// <param name="storeNumber"></param>
        /// <param name="storeTransferData"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool GetShopTransfers(
            string storeNumber,
            DesktopSession desktopSession,
            out DataTable storeTransferData,
            out string errorCode,
            out string errorText)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Set default output params
            storeTransferData = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetShopCashPositionFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_transfer_data", STORETRANSFERDATA));

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "getshoptransfers", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling getshoptransfers stored procedure", oEx);
                    errorCode = "GetShopTransfers";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorText = dA.ErrorDescription;
                    return (false);
                }
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    storeTransferData = outputDataSet.Tables[STORETRANSFERDATA];
                    return true;
                }

                errorCode = "GetShopTransfersFailed";
                errorText = "Operation failed";
                return (false);
            }
            errorCode = "GetShopTransfersFailed";
            errorText = "No valid input parameters given";
            return (false);
        }

        /// <summary>
        /// Call to the stored procedure to get details of a 
        /// transfer based on the transfer number passed
        /// </summary>
        /// <param name="transferNumber"></param>
        /// <param name="storeNumber"></param>
        /// <param name="transferDetailsData"></param>
        /// <param name="denominationDetailsData"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <param name="transferId"></param>
        /// <returns></returns>
        public static bool GetShopTransferDetails(
            string transferNumber,
            string transferId,
            string storeNumber,
            DesktopSession desktopSession,
            out DataTable transferDetailsData,
            out DataTable denominationDetailsData,
            out string errorCode,
            out string errorText)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Set default output params
            transferDetailsData = null;
            denominationDetailsData = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetShopTransferDetailsFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_transfer_number", transferNumber));

            inParams.Add(new OracleProcParam("p_transfer_id", transferId));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_transfer_details", TRANSFERDETAILS));
                refCursArr.Add(new PairType<string, string>("r_denomination_details", DENOMINATIONDETAILS));

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "getshoptransferdetails", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling getshoptransferdetails stored procedure", oEx);
                    errorCode = "GetShopTransferdetails";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorText = dA.ErrorDescription;
                    return (false);
                }
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    transferDetailsData = outputDataSet.Tables[TRANSFERDETAILS];
                    denominationDetailsData = outputDataSet.Tables[DENOMINATIONDETAILS];
                    return true;
                }

                errorCode = "GetShopTransferDetailsFailed";
                errorText = "Operation failed";
                return (false);
            }
            errorCode = "GetShopTransferDetailsFailed";
            errorText = "No valid input parameters given";
            return (false);
        }

        /// <summary>
        /// Call to SP to update shop cash transfer to indicate accepted or rejected status
        /// </summary>
        /// <param name="storeNumber"></param>
        /// <param name="transferNumber"></param>
        /// <param name="transferId"></param>
        /// <param name="transferStatus"></param>
        /// <param name="comment"></param>
        /// <param name="rejectReason"></param>
        /// <param name="userID"></param>
        /// <param name="transferStatusDate"></param>
        /// <param name="currencyCode"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool UpdateShopCashTransfer(
            string storeNumber,
            string transferNumber,
            string transferId,
            string transferStatus,
            string comment,
            string rejectReason,
            string userID,
            string transferStatusDate,
            string currencyCode,
            DesktopSession desktopSession,
            out string errorCode,
            out string errorText)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "Insertshopcashtransfer";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_transfer_number", transferNumber));

            inParams.Add(new OracleProcParam("p_transfer_status", transferStatus));

            inParams.Add(new OracleProcParam("p_comment", comment));

            inParams.Add(new OracleProcParam("p_reject_reason", rejectReason));

            inParams.Add(new OracleProcParam("p_user_id", userID));

            inParams.Add(new OracleProcParam("p_transfer_status_date", transferStatusDate));

            inParams.Add(new OracleProcParam("p_currency_code", currencyCode));

            inParams.Add(new OracleProcParam("p_transfer_id", transferId));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                bool retVal;
                try
                {
                    DataSet outputDataSet;
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "updateshopcashtransfer", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling Updateshopcashtransfer stored procedure", oEx);
                    errorCode = "Updateshopcashtransfer";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorText = dA.ErrorDescription;
                    return (false);
                }
            }
            else
            {
                errorCode = "UpdateshopcashtransferFailed";
                errorText = "No Valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorText = "Success";

            return true;
        }

        /// <summary>
        /// call to sp to insert a bank transfer record
        /// </summary>
        /// <param name="storeBankId"></param>
        /// <param name="currencyCode"></param>
        /// <param name="cashDrawerID"></param>
        /// <param name="transferDate"></param>
        /// <param name="comment"></param>
        /// <param name="userID"></param>
        /// <param name="transferAmount"></param>
        /// <param name="transferType"></param>
        /// <param name="storeNumber"></param>
        /// <param name="depositBagNumber"></param>
        /// <param name="bankRoutingNumber"></param>
        /// <param name="transferNumber"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <param name="checkNumber"></param>
        /// <param name="bankName"></param>
        /// <param name="bankAccountNumber"></param>
        /// <returns></returns>
        public static bool InsertBankTransfer(
            string storeBankId,
            string currencyCode,
            string cashDrawerID,
            string transferDate,
            string comment,
            string userID,
            string transferAmount,
            string transferType,
            string storeNumber,
            string checkNumber,
            string depositBagNumber,
            string bankName,
            string bankAccountNumber,
            string bankRoutingNumber,
            DesktopSession desktopSession,
            out int transferNumber,
            out string errorCode,
            out string errorText)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            transferNumber = 0;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "Insertbankcashtransfer";
                errorText = "Invalid desktop session or data accessor";
                transferNumber = 0;
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_bank_id", storeBankId));

            inParams.Add(new OracleProcParam("p_currency_code", currencyCode));

            inParams.Add(new OracleProcParam("p_cash_drawer_id", cashDrawerID));

            inParams.Add(new OracleProcParam("p_transfer_date", transferDate));

            inParams.Add(new OracleProcParam("p_comment", comment));

            inParams.Add(new OracleProcParam("p_user_id", userID));

            inParams.Add(new OracleProcParam("p_transfer_amount", transferAmount));

            inParams.Add(new OracleProcParam("p_transfer_type", transferType));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_check_number", checkNumber));

            inParams.Add(new OracleProcParam("p_deposit_bag_number", depositBagNumber));

            inParams.Add(new OracleProcParam("p_bank_name", bankName));

            inParams.Add(new OracleProcParam("p_bank_acct_number", bankAccountNumber));

            inParams.Add(new OracleProcParam("p_bank_rout_number", bankRoutingNumber));

            inParams.Add(new OracleProcParam("o_transfer_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                bool retVal;
                DataSet outputDataSet;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "insertbankcashtransfer", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling Insertbankcashtransfer stored procedure", oEx);
                    errorCode = "Insertbankcashtransfer";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorText = dA.ErrorDescription;
                    return (false);
                }
                //Get output number
                DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
                {
                    DataRow dr = outputDt.Rows[0];
                    if (dr != null && dr.ItemArray.Length > 0)
                    {
                        object obj = dr.ItemArray.GetValue(1);
                        if (obj != null)
                        {
                            var nextNumStr = (string)obj;
                            transferNumber = Int32.Parse(nextNumStr);
                            errorCode = "0";
                            errorText = "Success";
                            return (true);
                        }
                    }
                }
            }
            else
            {
                errorCode = "InsertbankcashtransferFailed";
                errorText = "No Valid input parameters given";
                return (false);
            }
            errorCode = "1";
            errorText = "Insert Bank Transfer failed";

            return false;
        }

        /// <summary>
        /// Call to SP to insert a new cash transfer
        /// </summary>
        /// <param name="sourceCashDrawerID"></param>
        /// <param name="destCashDrawerID"></param>
        /// <param name="transferAmount"></param>
        /// <param name="currencyCode"></param>
        /// <param name="transferDate"></param>
        /// <param name="sourceUserID"></param>
        /// <param name="destUserID"></param>
        /// <param name="storeNumber"></param>
        /// <param name="transferNumber"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool InsertcashTransfer(
            string sourceCashDrawerID,
            string destCashDrawerID,
            decimal transferAmount,
            string currencyCode,
            string transferDate,
            string sourceUserID,
            string destUserID,
            string storeNumber,
            DesktopSession desktopSession,
            out int transferNumber,
            out string errorCode,
            out string errorText)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            transferNumber = 0;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "Insertbankcashtransfer";
                errorText = "Invalid desktop session or data accessor";
                transferNumber = 0;
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_source_cash_drawer_id", sourceCashDrawerID));

            inParams.Add(new OracleProcParam("p_destin_cash_drawer_id", destCashDrawerID));

            inParams.Add(new OracleProcParam("p_transfer_amount", transferAmount));

            inParams.Add(new OracleProcParam("p_currency_code", currencyCode));

            inParams.Add(new OracleProcParam("p_transfer_date", transferDate));

            inParams.Add(new OracleProcParam("p_source_user_id", sourceUserID));

            inParams.Add(new OracleProcParam("p_destin_user_id", destUserID));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("o_transfer_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                bool retVal;
                DataSet outputDataSet;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "insertcashtransfer", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling Insertcashtransfer stored procedure", oEx);
                    errorCode = "Insertcashtransfer";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorText = dA.ErrorDescription;
                    return (false);
                }
                //Get output number
                DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
                {
                    DataRow dr = outputDt.Rows[0];
                    if (dr != null && dr.ItemArray.Length > 0)
                    {
                        object obj = dr.ItemArray.GetValue(1);
                        if (obj != null)
                        {
                            var nextNumStr = (string)obj;
                            transferNumber = Int32.Parse(nextNumStr);
                            errorCode = "0";
                            errorText = "Success";
                            return (true);
                        }
                    }
                }
            }
            else
            {
                errorCode = "InsertcashtransferFailed";
                errorText = "No Valid input parameters given";
                return (false);
            }
            errorCode = "1";
            errorText = "Insert cash Transfer failed";

            return false;
        }

        /// <summary>
        /// Wrapper call to the stored procedure to get all
        /// the bank information for a given store
        /// </summary>
        /// <param name="pStoreNumber"></param>
        /// <param name="storeBankInfo"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool GetStoreBankInfo(
            string pStoreNumber,
            DesktopSession desktopSession,
            out DataTable storeBankInfo,
            out string errorCode,
            out string errorText)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Set default output params
            storeBankInfo = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetStoreBankInfoFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", pStoreNumber));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_bank_info", BANKINFODETAILS));

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "get_store_bank_info", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling GetstoreBankInfo stored procedure", oEx);
                    errorCode = "GetstoreBankInfo";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorText = dA.ErrorDescription;
                    return (false);
                }
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    storeBankInfo = outputDataSet.Tables[BANKINFODETAILS];
                    return true;
                }

                errorCode = "GetstoreBankInfoFailed";
                errorText = "Operation failed";
                return (false);
            }
            errorCode = "GetstoreBankInfoFailed";
            errorText = "No valid input parameters given";
            return (false);
        }

        /// <summary>
        /// Given a store number set the shop to closed by setting the statuscode to SIGNEDOFF
        /// </summary>
        /// <param name="storeNumber"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool CloseShop(
            string storeNumber,
            DesktopSession desktopSession,
            out string errorCode,
            out string errorText)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "SetShopClose";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "setshopclose", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling setshopclose stored procedure", oEx);
                    errorCode = "Setshopclose";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorText = dA.ErrorDescription;
                    return (false);
                }
            }
            else
            {
                errorCode = "SetshopcloseFailed";
                errorText = "No Valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorText = "Success";
            return (true);
        }

        /// <summary>
        /// Given a cash drawer and a date return information
        /// showing if the cash drawer was balanced that date or not
        /// </summary>
        /// <param name="cashDrawerId"></param>
        /// <param name="balanceDate"></param>
        /// <param name="cashDrawerBalanced"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool CheckCashDrawerBalanced(
            string cashDrawerId,
            string balanceDate,
            DesktopSession desktopSession,
            out bool cashDrawerBalanced,
            out string errorCode,
            out string errorText)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            cashDrawerBalanced = false;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "CheckCashDrawerBalanced";
                errorText = "Invalid desktop session or data accessor";

                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_cash_drawer_id", cashDrawerId));

            inParams.Add(new OracleProcParam("p_balance_date", balanceDate));

            inParams.Add(new OracleProcParam("o_cashdrawer_balanced", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 10));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                bool retVal;
                DataSet outputDataSet;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "check_cashdrawer_balanced", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling CheckCashDrawerBalanced stored procedure", oEx);
                    errorCode = "CheckCashDrawerBalanced";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorText = dA.ErrorDescription;
                    return (false);
                }
                //Get output number
                DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
                {
                    DataRow dr = outputDt.Rows[0];
                    if (dr != null && dr.ItemArray.Length > 0)
                    {
                        object obj = dr.ItemArray.GetValue(1);
                        if (obj != null)
                        {
                            var balanced = (string)obj;
                            if (balanced == "TRUE")
                                cashDrawerBalanced = true;

                            errorCode = "0";
                            errorText = "Success";
                            return (true);
                        }
                    }
                }
            }
            else
            {
                errorCode = "CheckCashDrawerBalanced";
                errorText = "No Valid input parameters given";
                return (false);
            }
            errorCode = "1";
            errorText = "Check cash drawer balanced store procedure call failed";

            return false;
        }

        /// <summary>
        /// Remove the connection between a user and cashdrawer and workstation
        /// Called when a logged in user completes a transaction and goes back to the main menu
        /// </summary>
        /// <param name="cashdrawerId"></param>
        /// <param name="workstationId"></param>
        /// <param name="storeNumber"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public static bool DeleteConnectedCdUser(
            string cashdrawerId,
            string workstationId,
            string storeNumber,
            DesktopSession desktopSession,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "DeleteConnectedCdUser";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_cashdrawer_id", cashdrawerId));

            inParams.Add(new OracleProcParam("p_workstation_id", workstationId));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "remove_connectedcduser", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling remove_connectedcduser stored procedure", oEx);
                    errorCode = "remove_connectedcduser";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
            }
            else
            {
                errorCode = "remove_connectedcduserFailed";
                errorMesg = "No Valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorMesg = "Success";
            return (true);
        }

        public static decimal GetCashDrawerAmount(DataTable cashdrawerTransactions)
        {
            decimal cashdrawerAmount = 0.0m;
            foreach (DataRow dr in cashdrawerTransactions.Rows)
            {
                string opCode = Utilities.GetStringValue(dr["operationcode"], "");
                if (!string.IsNullOrEmpty(opCode))
                {
                    decimal tranAmount = Utilities.GetDecimalValue(dr["amount"], 0);
                    if (tranAmount < 0)
                        tranAmount = -tranAmount;
                    //RDEBIT and REOUT need to be subtracted
                    //and RCREDIT and REIN need to be added
                    //TROUT should be subtracted and TRIN should be added
                    //PCIN(personal check in) should be added
                    //PCOUT(personal check out) should be subtracted
                    if (opCode == "RCREDIT" || opCode == "REIN" || opCode == "TRIN" || opCode == "PCIN")
                        cashdrawerAmount += tranAmount;
                    else if (opCode == "RDEBIT" || opCode == "REOUT" || opCode == "TROUT" || opCode == "PCOUT")
                        cashdrawerAmount -= tranAmount;
                }
            }
            return cashdrawerAmount;
        }

        public static decimal calculateCDAmount(DataRow[] cdRows)
        {
            decimal cashdrawerAmount = 0.0m;
            foreach (DataRow dr in cdRows)
            {
                string opCode = Utilities.GetStringValue(dr["operationcode"], "");
                if (!string.IsNullOrEmpty(opCode))
                {
                    decimal tranAmount = Utilities.GetDecimalValue(dr["amount"], 0);
                    if (tranAmount < 0)
                        tranAmount = -tranAmount;
                    //RDEBIT and REOUT need to be subtracted
                    //and RCREDIT and REIN need to be added
                    //TROUT should be subtracted and TRIN should be added
                    //PCIN(personal check in) should be added
                    //PCOUT(personal check out) should be subtracted
                    if (opCode == "RCREDIT" || opCode == "REIN" || opCode == "TRIN" || opCode == "PCIN")
                        cashdrawerAmount += tranAmount;
                    else if (opCode == "RDEBIT" || opCode == "REOUT" || opCode == "TROUT" || opCode == "PCOUT")
                        cashdrawerAmount -= tranAmount;
                }
            }
            return cashdrawerAmount;
        }

        public static bool GetBankTransferDetails(
            string transferNumber,
            string transferType,
            string storeNumber,
            DesktopSession desktopSession,
            out CashTransferVO bankTransferData,
            out string errorCode,
            out string errorText)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Set default output params
            bankTransferData = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetBankTransferDetailsFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_transfer_number", transferNumber));
            inParams.Add(new OracleProcParam("p_transfer_type", transferType)); //BZ # 419

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_transfer_details", TRANSFERDETAILS));

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "get_bank_transfer_details", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling getbanktransferdetails stored procedure", oEx);
                    errorCode = "GetBankTransferdetails";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorText = dA.ErrorDescription;
                    return (false);
                }
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    //Parse the transferdetails datatable to get all the information to show in the screen
                    //transferdetails should have only row for the selected transfer number
                    CashTransferVO bankTransfer = new CashTransferVO();
                    bankTransfer.TransferId = Utilities.GetStringValue(outputDataSet.Tables[0].Rows[0][(int)BankTransferRecord.BANKTRANSFERID]);
                    bankTransfer.TransferType = Utilities.GetStringValue(outputDataSet.Tables[0].Rows[0][(int)BankTransferRecord.TRANSFERTYPE]);
                    bankTransfer.TransferNumber = Utilities.GetIntegerValue(outputDataSet.Tables[0].Rows[0][(int)BankTransferRecord.TRANSFERNUMBER], 0);
                    bankTransfer.TransferDate = Utilities.GetStringValue(outputDataSet.Tables[0].Rows[0][(int)BankTransferRecord.TRANSFERDATE]);
                    bankTransfer.SourceEmployee = Utilities.GetStringValue(outputDataSet.Tables[0].Rows[0][(int)BankTransferRecord.USERID]);
                    bankTransfer.TransferAmount = Utilities.GetDecimalValue(outputDataSet.Tables[0].Rows[0][(int)BankTransferRecord.TRANSFERAMOUNT], 0.0M);
                    bankTransfer.BankName = Utilities.GetStringValue(outputDataSet.Tables[0].Rows[0][(int)BankTransferRecord.BANKNAME]);
                    bankTransfer.BankAccountNumber = Utilities.GetStringValue(outputDataSet.Tables[0].Rows[0][(int)BankTransferRecord.BANKCCOUNTNUMBER]);
                    bankTransfer.CheckNumber = Utilities.GetStringValue(outputDataSet.Tables[0].Rows[0][(int)BankTransferRecord.CHECKNUMBER]);
                    bankTransferData = bankTransfer;
                    return true;
                }

                errorCode = "GetBankTransferDetailsFailed";
                errorText = "Operation failed";
                return (false);
            }
            errorCode = "GetBankTransferDetailsFailed";
            errorText = "No valid input parameters given";
            return (false);
        }

        public static bool GetShopCashTransferData(
            string transferNumber,
            string transferId,
            string storeNumber,
            DesktopSession desktopSession,
            out CashTransferVO shopCashTransferObj,
            out string errorCode,
            out string errorText)
        {
            DataTable transferDetails;
            DataTable denominationDetailsData;
            shopCashTransferObj = null;
            bool retVal = GetShopTransferDetails(transferNumber, transferId, storeNumber, desktopSession, out transferDetails, out denominationDetailsData, out errorCode, out errorText);
            if (!retVal)
            {
                return false;
            }
            if (transferDetails != null && transferDetails.Rows.Count > 0)
            {
                CashTransferVO shopCashTransfer = new CashTransferVO();
                SiteId sourceStore = new SiteId();
                sourceStore.StoreName = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.STORENAME]);
                sourceStore.StoreNumber = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.STORENUMBER]);
                sourceStore.StoreAddress1 = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.STOREADDRESS1]);
                sourceStore.StoreCityName = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.STORECITYNAME]);
                sourceStore.StoreCityName = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.STORESTATE]);
                sourceStore.StoreZipCode = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.STOREZIPCODE]);
                sourceStore.StoreManager = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.STOREMANAGER]);
                sourceStore.StorePhoneNo = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.STOREPHONENO]);
                shopCashTransfer.SourceShopInfo = sourceStore;
                shopCashTransfer.Transporter = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.TRANSPORTEDBY]);
                shopCashTransfer.DepositBagNo = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.DEPOSITBAGNUMBER]);
                shopCashTransfer.SourceComment = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.SOURCECOMMENT]);
                SiteId destStore = new SiteId();
                destStore.StoreName = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.V_DEST_STORENAME]);
                destStore.StoreNumber = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.V_DEST_STORENUMBER]);
                destStore.StoreAddress1 = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.V_DEST_STOREADDRESS]);
                destStore.StoreCityName = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.V_DEST_STORECITYNAME]);
                destStore.StorePhoneNo = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.V_DEST_STOREPHONENO]);
                destStore.State = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.V_DEST_STORESTATE]);
                destStore.StoreZipCode = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.V_DEST_STOREZIPCODE]);
                destStore.StoreManager = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.V_DEST_STOREMANAGER]);
                shopCashTransfer.DestinationShopInfo = destStore;
                shopCashTransfer.TransferAmount = Utilities.GetDecimalValue(transferDetails.Rows[0][(int)TransferDetailsRecord.TRANSFERAMOUNT]);
                shopCashTransfer.TransferDate = Utilities.GetDateTimeValue(transferDetails.Rows[0][(int)TransferDetailsRecord.TRANSFERDATE]).ToShortDateString();
                shopCashTransfer.TransferStatus = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.TRANSFERSTATUS]);
                shopCashTransfer.TransferNumber = Utilities.GetIntegerValue(transferDetails.Rows[0][(int)TransferDetailsRecord.TRANSFERNUMBER]);
                shopCashTransfer.TransferId = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.SHOPTRANSFERID]);
                shopCashTransfer.SourceEmployeeName = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.USERNAME]);
                shopCashTransfer.SourceEmployeeNumber = Utilities.GetStringValue(transferDetails.Rows[0][(int)TransferDetailsRecord.USEREMPLOYEENUMBER]);
                List<DenominationVO> currencyData = new List<DenominationVO>();
                if (denominationDetailsData != null && denominationDetailsData.Rows.Count > 0)
                {
                    foreach (DataRow dr in denominationDetailsData.Rows)
                    {
                        DenominationVO denom = new DenominationVO();
                        string displayName = Utilities.GetStringValue(dr["displayname"], "");
                        string currencyDenom = Utilities.GetDecimalValue(dr["denomination_amount"]).ToString();
                        //The following is necessary since in the DB both coin and dollar 1 is 
                        //the same as far as amount is concerned but is different only in display name
                        if (displayName == "USD COIN 1")
                            currencyDenom = "1.00";

                        denom.DenominationType = displayName;
                        denom.DenominationValue = currencyDenom;

                        currencyData.Add(denom);
                    }
                }
                shopCashTransfer.TransferDenominations = currencyData;
                shopCashTransferObj = shopCashTransfer;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get the tube safe denomination data for store
        /// </summary>
        /// <param name="pStoreNumber"></param>
        /// <param name="storeTubeSafeInfo"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool GetTubeSafeDataForStore(
            string pStoreNumber,
            DesktopSession desktopSession,
            out DataTable storeTubeSafeInfo,
            out string errorCode,
            out string errorText)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Set default output params
            storeTubeSafeInfo = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            if (desktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetTubeSafeDataForStoreFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_entity_type", "STORE"));

            inParams.Add(new OracleProcParam("p_entity_value", pStoreNumber));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_tube_denomination_info", TUBEDENOMINATIONS));

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "getTubeDenominations", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling getTubeDenominations stored procedure", oEx);
                    errorCode = "GetTubeSafeDataForStore";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorText = dA.ErrorDescription;
                    return (false);
                }
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    storeTubeSafeInfo = outputDataSet.Tables[TUBEDENOMINATIONS];
                    return true;
                }

                errorCode = "GetTubeSafeDataForStoreFailed";
                errorText = "Operation failed";
                return (false);
            }
            errorCode = "GetTubeSafeDataForStoreFailed";
            errorText = "No valid input parameters given";
            return (false);
        }

        /// <summary>
        /// Call to the stored procedure that will write entries in cd_journal and cd_balance
        /// to indicate that the cash drawer or safe is balanced
        /// </summary>
        /// <param name="oda"></param>
        /// <param name="storeNumber"></param>
        /// <param name="cashDrawerId"></param>
        /// <param name="safeBalance"></param>
        /// <param name="balanceDate"></param>
        /// <param name="currencyCode"></param>
        /// <param name="balanceAmount"></param>
        /// <param name="forcedAmount"></param>
        /// <param name="overShortCode"></param>
        /// <param name="userId"></param>
        /// <param name="comments"></param>
        /// <param name="denominationData"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool UpdateCashInfo(
            OracleDataAccessor oda,
            string storeNumber,
            string cashDrawerId,
            string safeBalance,
            string balanceDate,
            string currencyCode,
            decimal balanceAmount,
            decimal forcedAmount,
            string overShortCode,
            string userId,
            string comments,
            List<string> denominationData,
            out string errorCode,
            out string errorText)
        {
            if (oda == null)
            {
                errorCode = "UpdateCashInfoFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_cashdrawer_id", cashDrawerId));

            inParams.Add(new OracleProcParam("p_safe_balance", safeBalance));

            inParams.Add(new OracleProcParam("p_balance_date", balanceDate));

            inParams.Add(new OracleProcParam("p_currency_code", currencyCode));

            inParams.Add(new OracleProcParam("p_balance_amount", balanceAmount));

            inParams.Add(new OracleProcParam("p_forced_amount", forcedAmount));

            inParams.Add(new OracleProcParam("p_overshort_code", overShortCode));

            inParams.Add(new OracleProcParam("p_user_id", userId));

            inParams.Add(new OracleProcParam("p_comments", comments));

            inParams.Add(new OracleProcParam("p_denomination_details", true, denominationData));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = oda.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "update_cash_info", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling update_cash_info stored procedure", oEx);
                    errorCode = "UpdateCashInfo";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oda.ErrorCode;
                    errorText = oda.ErrorDescription;
                    return (false);
                }
            }
            else
            {
                errorCode = "UpdateCashInfoFailed";
                errorText = "No Valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorText = "Success";
            return (true);
        }

        public static bool GetLastBalanceDate(string cashdrawerId,
            out string lastBalanceDate)
        {
            //Set output vars
            string errorCode = string.Empty;
            string errorText = string.Empty;
            lastBalanceDate = string.Empty;



            //Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("GetLastBalanceDate Failed",
                                                            new ApplicationException("GetLastBalanceDate Failed: Data accessor instance is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<string> miscFlags = new List<string>();
            //Create parameter list

            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_cashdrawer_id", cashdrawerId));
            oParams.Add(new OracleProcParam("o_last_balanced_date", OracleDbType.Date, DBNull.Value, ParameterDirection.Output, 1));


            //Execute stored proc
            DataSet outputSet;
            bool retVal;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "pawn_gen_procs", "getLastBalancedDate", oParams, null, "o_return_code",
                                                                                            "o_return_text", out outputSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("getLastBalancedDate Failed", oEx);
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("getLastBalancedDate Failed: return value is false", new ApplicationException());
                return (false);
            }

            //Get output number
            DataTable outputDt = outputSet.Tables["OUTPUT"];
            if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
            {
                DataRow dr = outputDt.Rows[0];
                if (dr != null && dr.ItemArray.Length > 0)
                {
                    object nextNumObj = dr.ItemArray.GetValue(1);
                    if (nextNumObj != null)
                    {
                        var nextNumStr = (string)nextNumObj;
                        lastBalanceDate = Utilities.GetDateTimeValue(nextNumStr).ToShortDateString();
                    }
                }

            }


            return (true);

        }


    }
}
