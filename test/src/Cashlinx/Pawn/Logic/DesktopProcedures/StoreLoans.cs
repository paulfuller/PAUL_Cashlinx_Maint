/************************************************************************
* Namespace:       CashlinxDesktop.DesktopProcedures
* Class:           StoreLoans
* 
* Description      The class retrieves Store Loan information as well
*                  as parses the results back into associated Class 
*                  objects.
* 
* History
* David D Wise, 8-5-2009, Initial Development
* 
* **********************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Database;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Pawn.Logic.DesktopProcedures
{
    public static class StoreLoans
    {
        public static readonly string PAWN = "m";
        public static readonly string PAWN_APP = "o_pawn_app" + "_" + PAWN;
        public static readonly string PAWN_LOAN = "o_pawn_loan" + "_" + PAWN;
        public static readonly string PAWN_MDSE_LIST = "o_pawn_mdselist" + "_" + PAWN;
        public static readonly string PAWN_MDHIST_LIST = "o_pawn_mdhistlist" + "_" + PAWN;
        public static readonly string PAWN_GUN_LIST = "o_pawn_gunlist" + "_" + PAWN;
        public static readonly string PAWN_OTHERDSC_LIST = "o_pawn_otherdsclist" + "_" + PAWN;
        public static readonly string PAWN_RECEIPTDET_LIST = "o_pawn_receiptdetlist" + "_" + PAWN;
        public static readonly string PAWN_FEE_LIST = "o_pawn_feelist" + "_" + PAWN;
        public static readonly string PAWN_PAYMENTDETAILS_LIST = "o_pawn_pawnpaymentlist" + "_" + PAWN;

        public static bool CheckForPriorPFI(
            string storeNumber,
            out List<int> refNumbers,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;
            refNumbers = new List<int>();

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "CheckForPriorPFI";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("CheckForPriorPFI", new ApplicationException("Cannot execute the check_for_posts stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            refCursors.Add(new PairType<string, string>("o_check_for_posts", "pfi_data"));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_STORE_PROCS", "check_for_posts",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "CheckForPriorPFIFailed";
                errorText = "Invocation of check_for_posts stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking check_for_posts stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;

            //Get the Data
            if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
            {
                if (outputDataSet.Tables["pfi_data"] != null)
                {
                    foreach (DataRow dataRow in outputDataSet.Tables["pfi_data"].Rows)
                    {
                        int iData = Utilities.GetIntegerValue(dataRow["ref_number"]);
                        refNumbers.Add(iData);
                    }
                }
            }

            return (true);
        }

        public static bool CheckStoreRefurbNumber(
            string storeNumber,
            DateTime storeDate,
            int refurbNumber,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "CheckStoreRefurbNumber";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("CheckStoreRefurbNumber", new ApplicationException("Cannot execute the Check_Store_Refurb stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_date", storeDate));
            inParams.Add(new OracleProcParam("p_refurb", refurbNumber));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_STORE_PROCS", "check_store_refurb",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "CheckStoreRefurbNumberFailed";
                errorText = "Invocation of check_store_refurb stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking check_store_refurb stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            else
            {
                errorCode = "0";
                errorText = string.Empty;
                return (true);
            }
        }

        public static bool DeleteLoanTransition(
            string storeNumber,
            int refNumber,
            ProductType refType,
            //           StateStatus transType,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetLoanTransition";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("DeleteLoanTransition", new ApplicationException("Cannot execute the Delete_Loan_Transition stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(refType == ProductType.ALL
                         ? new OracleProcParam("p_ref_type", "")
                         : new OracleProcParam("p_ref_type", (int)refType));
            inParams.Add(new OracleProcParam("p_ref_number", refNumber));
            inParams.Add(new OracleProcParam("p_ref_store", storeNumber));
            //           inParams.Add(new OracleProcParam("p_trans_type", transType.ToString()));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_STORE_PROCS", "Delete_Loan_Transition",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "DeleteLoanTransitionFailed";
                errorText = "Invocation of Delete_Loan_Transition stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking Delete_Loan_Transition stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;
            return (true);
        }



        /// <summary>
        /// Get all the pawn loans based on the id number passed
        /// </summary>
        /// <param name="storeNumber"></param>
        /// <param name="pawnLoanStatus"></param>
        /// <param name="TempStatus"></param>
        /// <param name="date"></param>
        /// <param name="bGetCustomerInfo"></param>
        /// <param name="pawnLoans"></param>
        /// <param name="pawnApplications"></param>
        /// <param name="customerVOs"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool GetStoreLoans(
            string storeNumber,
            ProductStatus pawnLoanStatus,
            StateStatus TempStatus,
            DateTime date,
            bool bGetCustomerInfo,
            out List<PawnLoan> pawnLoans,
            out List<PawnAppVO> pawnApplications,
            out List<CustomerVO> customerVOs,
            out string errorCode,
            out string errorText)
        {
            //Set default output params
            pawnLoans = new List<PawnLoan>();
            pawnApplications = new List<PawnAppVO>();
            customerVOs = new List<CustomerVO>();
            errorCode = string.Empty;
            errorText = string.Empty;

            if (GlobalDataAccessor.Instance == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetStoreLoansFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add store number
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_status", pawnLoanStatus.ToString()));
            inParams.Add(new OracleProcParam("p_date", date));
            if (TempStatus != StateStatus.BLNK)
                inParams.Add(new OracleProcParam("p_temp_type", TempStatus.ToString()));
            else
                inParams.Add(new OracleProcParam("p_temp_type", "0"));

            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("o_pawn_app", PAWN_APP));
            refCursArr.Add(new PairType<string, string>("o_pawn_loan", PAWN_LOAN));
            refCursArr.Add(new PairType<string, string>("o_pawn_mdselist", PAWN_MDSE_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_gunlist", PAWN_GUN_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_mdhistlist", PAWN_MDHIST_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_otherdsclist", PAWN_OTHERDSC_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_receiptdetlist", PAWN_RECEIPTDET_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_feelist", PAWN_FEE_LIST));
            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_store_procs",
                    "get_store_loans", inParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling get_store_loans stored procedure", oEx);
                errorCode = " -- GetStoreLoans failed";
                errorText = " --- Exception: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            else
            {
                if (outputDataSet != null && outputDataSet.IsInitialized)
                {
                    if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                    {
                        try
                        {
                            CustomerLoans.ParseDataSet(outputDataSet, out pawnLoans, out pawnApplications);
                            if (CollectionUtilities.isEmpty(pawnLoans) ||
                                CollectionUtilities.isEmpty(pawnApplications))
                            {
                                errorCode = "Parsing the data from the stored procedure failed";
                                errorText = "Pawn Loans or the PawnApplications object is null";
                                return false;
                            }

                            if (bGetCustomerInfo)
                            {
                                customerVOs.AddRange(pawnLoans.Select(pLoan => CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, pLoan.CustomerNumber)));
                            }

                            return (true);
                        }
                        catch (Exception ex)
                        {
                            errorCode = "Parsing the data from the stored procedure failed";
                            errorText = ex.Message;
                            return false;
                        }
                    }
                    else
                    {
                        errorText = "No records found.";
                        return false;
                    }
                }
            }

            errorCode = "GETSTOREDLOANSFAIL";
            errorText = "Operation failed";
            return (false);
        }

        public static bool GetLoanTransition(
            string storeNumber,
            int refNumber,
            ProductType refType,
            StateStatus transType,
            out List<PFI_TransitionData> transitionData,
            out decimal statusCode,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;
            transitionData = new List<PFI_TransitionData>();
            statusCode = 0;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetLoanTransition";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetLoanTransition", new ApplicationException("Cannot execute the Get_Loan_Transition stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(refType == ProductType.ALL
                         ? new OracleProcParam("p_ref_type", "")
                         : new OracleProcParam("p_ref_type", (int)refType));
            inParams.Add(new OracleProcParam("p_ref_number", refNumber));
            inParams.Add(new OracleProcParam("p_ref_store", storeNumber));
            inParams.Add(new OracleProcParam("p_trans_type", transType.ToString()));
            inParams.Add(new OracleProcParam("o_status_code", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            refCursors.Add(new PairType<string, string>("o_transition_data", "transition_data"));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_STORE_PROCS", "Get_Loan_Transition",
                    inParams, refCursors, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "GetLoanTransitionFailed";
                errorText = "Invocation of Get_Loan_Transition stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking Get_Loan_Transition stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;

            //Get the Data
            if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
            {
                DataRow statusRow = outputDataSet.Tables["OUTPUT"].Rows[0];

                if (statusRow != null && statusRow.ItemArray.Length > 0)
                {
                    object nextNumObj = statusRow.ItemArray.GetValue(1);
                    if (nextNumObj != null)
                    {
                        string nextNumStr = (string)nextNumObj;
                        statusCode = Utilities.GetDecimalValue(nextNumStr);
                    }
                }

                if (outputDataSet.Tables["transition_data"] != null)
                {
                    foreach (DataRow dataRow in outputDataSet.Tables["transition_data"].Rows)
                    {
                        string sData = Utilities.GetStringValue(dataRow["transdata"]);
                        DateTime transDate = Utilities.GetDateTimeValue(dataRow["trans_date"], DateTime.MinValue);

                        PFI_TransitionData tData = new PFI_TransitionData();
                        tData.pfiLoan = Utilities.DeSerialize<PFI_ProductData>(sData);
                        tData.TransitionDate = transDate;

                        transitionData.Add(tData);
                    }
                }
            }

            return (true);
        }



        public static bool ManagerOverrideReason(
            DateTime overrideDateTime,
            string storeNumber,
            string overrideID,
            ManagerOverrideTransactionType[] arManagerOverrideTransactionType,
            ManagerOverrideType[] arManagerOverrideType,
            decimal[] arSuggestedValue,
            decimal[] arApprovedValue,
            int[] arTransactionNumber,
            string comment,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance == null ||
                !GlobalDataAccessor.Instance.IsDataAccessorValid())
            {
                errorCode = "ManagerOverrideReason";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("ManagerOverrideReason",
                                                            new ApplicationException(
                                                                "Cannot execute the Insert Override History stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            OracleProcParam pManagerOverrideTransType = new OracleProcParam(ParameterDirection.Input
                                                                            , DataTypeConstants.PawnDataType.LISTSTRING
                                                                            , "p_ovr_tran_cd"
                                                                            , arManagerOverrideTransactionType.Length);
            for (int i = 0; i < arManagerOverrideTransactionType.Length; i++)
            {
                pManagerOverrideTransType.AddValue(arManagerOverrideTransactionType[i].ToString());
            }
            inParams.Add(pManagerOverrideTransType);

            OracleProcParam pManagerOverrideType = new OracleProcParam(ParameterDirection.Input
                                                                       , DataTypeConstants.PawnDataType.LISTSTRING
                                                                       , "p_ovr_sub_type_cd"
                                                                       , arManagerOverrideType.Length);
            for (int i = 0; i < arManagerOverrideType.Length; i++)
            {
                pManagerOverrideType.AddValue(arManagerOverrideType[i].ToString());
            }
            inParams.Add(pManagerOverrideType);

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_override_by", overrideID));

            OracleProcParam psuggestedvalue = new OracleProcParam(ParameterDirection.Input
                                                                  , DataTypeConstants.PawnDataType.LISTFLOAT
                                                                  , "p_sugg_value"
                                                                  , arSuggestedValue.Length);
            for (int i = 0; i < arSuggestedValue.Length; i++)
            {
                psuggestedvalue.AddValue(arSuggestedValue[i]);
            }
            inParams.Add(psuggestedvalue);

            OracleProcParam papprovedvalue = new OracleProcParam(ParameterDirection.Input
                                                                 , DataTypeConstants.PawnDataType.LISTFLOAT
                                                                 , "p_appr_value"
                                                                 , arApprovedValue.Length);
            for (int i = 0; i < arApprovedValue.Length; i++)
            {
                papprovedvalue.AddValue(arApprovedValue[i]);
            }
            inParams.Add(papprovedvalue);

            OracleProcParam ptrxnumber = new OracleProcParam(ParameterDirection.Input
                                                             , DataTypeConstants.PawnDataType.LISTINT
                                                             , "p_trx_number"
                                                             , arTransactionNumber.Length);
            for (var i = 0; i < arTransactionNumber.Length; i++)
            {
                ptrxnumber.AddValue(arTransactionNumber[i]);
            }
            inParams.Add(ptrxnumber);

            inParams.Add(new OracleProcParam("p_trx_date", overrideDateTime));
            inParams.Add(new OracleProcParam("p_user_id", GlobalDataAccessor.Instance.DesktopSession.UserName));
            inParams.Add(new OracleProcParam("p_workstation_id", GlobalDataAccessor.Instance.CurrentSiteId.TerminalId));
            inParams.Add(new OracleProcParam("p_ovr_comment", comment));

            //Create output data set names
            bool retVal = false;
            try
            {
                DataSet outputDataSet = null;
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_gen_procs", "insert_override_history",
                    inParams, null, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "ManagerOverrideReasonFailed";
                errorText = "Invocation of insert_override_history stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking insert_override_history stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal)
            {
                errorCode = "0";
                errorText = string.Empty;
                return (true);
            }
            else
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeNumber"></param>
        /// <param name="refType"></param>
        /// <param name="refNumber"></param>
        /// <param name="transitionData"></param>
        /// <param name="tempStatus"></param>
        /// <param name="transDate"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool SetLoanTransition(
            string storeNumber,
            ProductType refType,
            int refNumber,
            string transitionData,
            StateStatus tempStatus,
            DateTime transDate,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "InsertLoanTransition";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("SetLoanTransition",
                                                            new ApplicationException(
                                                                "Cannot execute the InsertLoanTransition update stored procedure"));
                return (false);
            }
            if (string.IsNullOrEmpty(transitionData))
            {
                errorCode = "InsertLoanTransition";
                errorText = "No Transition data sent to insert";
                BasicExceptionHandler.Instance.AddException("SetLoanTransition",
                                                            new ApplicationException(
                                                                "Cannot execute the InsertLoanTransition update stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_ref_type", (int)refType));
            inParams.Add(new OracleProcParam("p_ref_number", refNumber));
            inParams.Add(new OracleProcParam("p_ref_store", storeNumber));
            inParams.Add(new OracleProcParam("p_trans_data", transitionData, true));
            inParams.Add(new OracleProcParam("p_trans_type", tempStatus.ToString()));
            OracleProcParam.TimeStampType tsType = OracleProcParam.TimeStampType.TIMESTAMP_TZ;
            inParams.Add(new OracleProcParam("p_trans_date", transDate, tsType));
            inParams.Add(new OracleProcParam("p_created_by", GlobalDataAccessor.Instance.DesktopSession.UserName));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_STORE_PROCS", "insert_loan_transition",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "InsertLoanTransitionFailed";
                errorText = "Invocation of InsertLoanTransition stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking InsertLoanTransition stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            errorCode = "0";
            errorText = string.Empty;
            return (true);
        }

        public static bool UpdateTempStatus(
            List<int> lstRefNumbers,
            StateStatus TempStatus,
            string storeNumber,
            bool bFlag,
            List<string> refType,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetTempStatus";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetTempStatus", new ApplicationException("Cannot execute the UpdateTempStatus update stored procedure"));
                return (false);
            }

            //Get data accessor object
            var dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            OracleProcParam maskParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_ref_numbers", lstRefNumbers.Count);
            foreach (int i in lstRefNumbers)
            {
                maskParam.AddValue(i.ToString());
            }
            inParams.Add(maskParam);

            if (TempStatus == StateStatus.BLNK)
                inParams.Add(new OracleProcParam("p_temp_status", ""));
            else
                inParams.Add(new OracleProcParam("p_temp_status", TempStatus.ToString()));
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_flag", (bFlag ? "Y" : "N")));
            inParams.Add(new OracleProcParam("p_updated_by", GlobalDataAccessor.Instance.DesktopSession.UserName));
            inParams.Add(new OracleProcParam("p_ref_type", true, refType));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            refCursors.Add(new PairType<string, string>("r_temp_status", "temp_status"));

            DataSet outputDataSet;
            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "SERVICE_PAWN_LOANS", "Update_temp_status",
                    inParams, refCursors, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "UpdateTempStatusFailed";
                errorText = "Invocation of UpdateTempStatus stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking UpdateTempStatus stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            else
            {
                errorCode = "0";
                errorText = string.Empty;
                return (true);
            }
        }
        
        //----------
        public static bool GetPFIableItems(
            string aStoreNumber,
            DateTime date_eligible,
            out DataTable items,
            out string errorCode,
            out string errorText)
        {
            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add cat pointer
            inParams.Add(new OracleProcParam("p_store_nr", aStoreNumber));
            inParams.Add(new OracleProcParam("p_pfi_eligible", date_eligible));

            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;
            items = new DataTable();

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetPFIableItems";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetAssignableItems", new ApplicationException("Cannot execute the GetAssignableItems retrieval stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_items2PFI", "items2PFI"));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_mdse_procs", "get_items2pfi",
                    inParams, refCursors, "o_error_code", "o_error_desc",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "get_items2pfi";
                errorText = "Invocation of GetAssignableItems stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking GetAssignableItems stored proc", oEx);
                outputDataSet = null;
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }

            if (outputDataSet != null)
            {
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    //Get information and add to List
                    if (outputDataSet.Tables.Contains("items2PFI"))
                    {
                        items = outputDataSet.Tables["items2PFI"];
                    }

                    errorCode = "0";
                    errorText = string.Empty;
                    return (true);
                }
                else
                {
                    errorCode = "1";
                    errorText = "No Data Found";
                    return (true);
                }
            }

            errorCode = "GetAssignableItems";
            errorText = "Operation failed";
            return (false);          
        }

        public static bool Get_PFI_Details(
            string aStoreNumber, DateTime pfiDate,
            List<int> ticketNumber,
            out List<PawnLoan> pawnLoans,
            out List<PawnAppVO> pawnApplications,
            out List<CustomerVO> customerVOs,
            out List<PurchaseVO> purchases,
            out string errorCode,
            out string errorMsg)
        {
            errorCode = string.Empty;
            errorMsg = string.Empty;
            purchases = new List<PurchaseVO>();
            pawnLoans = new List<PawnLoan>();
            customerVOs = new List<CustomerVO>();
            pawnApplications = new List<PawnAppVO>();

            if (GlobalDataAccessor.Instance.DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "updatePawnLostTicketFlag Failed";
                errorMsg = "Invalid desktop session or data accessor";
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            List<OracleProcParam> inParams = new List<OracleProcParam>();
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add store number as param
            inParams.Add(new OracleProcParam("p_store_nr", aStoreNumber));
            inParams.Add(new OracleProcParam("p_pfi_eligible", pfiDate));

            //Add ticket number array as param
            OracleProcParam orpmTicketNumber = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTINT, "p_ticketList", ticketNumber.Count);
            foreach (int i in ticketNumber)
            {
                orpmTicketNumber.AddValue(i);
            }
            inParams.Add(orpmTicketNumber);

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_pawn_app", PAWN_APP));
            refCursors.Add(new PairType<string, string>("o_pawn_loan", PAWN_LOAN));
            refCursors.Add(new PairType<string, string>("o_pawn_mdselist", PAWN_MDSE_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_gunlist", PAWN_GUN_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_mdhistlist", PAWN_MDHIST_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_otherdsclist", PAWN_OTHERDSC_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_receiptdetlist", PAWN_RECEIPTDET_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_feelist", PAWN_FEE_LIST));
            refCursors.Add(new PairType<string, string>("o_purch", PurchaseProcedures.PURCHASE_DATA));
            refCursors.Add(new PairType<string, string>("o_pawn_partpaymentist", PAWN_PAYMENTDETAILS_LIST));

            DataSet outputDataSet;

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_mdse_procs", "get_PFIDetails_a",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            else
            {
                errorCode = "updatePawnLostTicketFlag";
                errorMsg = "No valid input parameters given";
                return (false);
            }

            if (outputDataSet != null && rt)
            {
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    CustomerLoans.ParseDataSet(outputDataSet, out pawnLoans, out pawnApplications);

                    // map dataSet tablenames to enable Purchase parsing
                    reassignName(outputDataSet,
                                 CustomerLoans.PAWN_MDSE_LIST,
                                 PurchaseProcedures.PAWN_MDSE_LIST);
                    reassignName(outputDataSet,
                                 CustomerLoans.PAWN_MDHIST_LIST,
                                 PurchaseProcedures.PAWN_MDHIST_LIST);
                    reassignName(outputDataSet,
                                 CustomerLoans.PAWN_RECEIPTDET_LIST,
                                 PurchaseProcedures.PAWN_RECEIPTDET_LIST);
                    reassignName(outputDataSet,
                                 CustomerLoans.PAWN_GUN_LIST,
                                 PurchaseProcedures.PAWN_GUN_LIST);            

                    PurchaseProcedures.ParseDataSet(outputDataSet, out purchases);

                    /*
                    if (CollectionUtilities.isEmpty(pawnLoans))
                    {
                    errorCode = "Parsing the data from the stored procedure failed";
                    errorMsg = "loan object is null";
                    return false;
                    }

                    if (CollectionUtilities.isEmpty(purchases))
                    {
                    errorCode = "Parsing the data from the stored procedure failed";
                    errorMsg = "purchases object is null";
                    return false;
                    }
                    */
                    errorCode = "0";
                    errorMsg = string.Empty;
                    return (true);
                }
                else
                {
                    errorCode = "1";
                    errorMsg = "No Data Found";
                    return (true);
                }
            }

            return (rt);
        }

        private static void reassignName(DataSet data, string oldName, string newName)
        {
            if (data.Tables[oldName] != null)
                data.Tables[oldName].TableName = newName;         
        }

        //-------

        public static PawnLoan GetCurrentLoanFees(SiteId siteId, PawnLoan pawnLoan, out UnderwritePawnLoanVO underwritePawnLoanVO)
        {
            decimal currentValue = 0.0M;

            PawnLoan _PawnLoan = Utilities.CloneObject(pawnLoan);
            // call UnderWrite Pawn Loan
            UnderwritePawnLoanUtility upw = new UnderwritePawnLoanUtility(GlobalDataAccessor.Instance.DesktopSession);
            upw.RunUWP(siteId);

            underwritePawnLoanVO = upw.PawnLoanVO;

            // CL_PWN_0013_MININTAMT
            underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0013_MININTAMT", out currentValue);
            Fee fee = new Fee()
            {
                FeeType = FeeTypes.MINIMUM_INTEREST,
                Value = currentValue
            };
            UpdatePawnLoanFee(_PawnLoan, fee);

            // CL_PWN_0018_SETUPFEEAMT
            underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0018_SETUPFEEAMT", out currentValue);
            fee = new Fee()
            {
                FeeType = FeeTypes.SETUP,
                Value = currentValue
            };
            UpdatePawnLoanFee(_PawnLoan, fee);

            // CL_PWN_0022_CITYFEEAMT
            underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0022_CITYFEEAMT", out currentValue);
            fee = new Fee()
            {
                FeeType = FeeTypes.CITY,
                Value = currentValue
            };
            UpdatePawnLoanFee(_PawnLoan, fee);

            // CL_PWN_0026_FIREARMFEEAMT
            underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0026_FIREARMFEEAMT", out currentValue);
            fee = new Fee()
            {
                FeeType = FeeTypes.FIREARM,
                Value = currentValue
            };
            UpdatePawnLoanFee(_PawnLoan, fee);

            // CL_PWN_0040_PFIMAILFEE
            underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0040_PFIMAILFEE", out currentValue);
            fee = new Fee()
            {
                FeeType = FeeTypes.MAILER_CHARGE,
                Value = currentValue
            };
            UpdatePawnLoanFee(_PawnLoan, fee);

            // CL_PWN_0101_LOANFEEAMT
            underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0101_LOANFEEAMT", out currentValue);
            fee = new Fee()
            {
                FeeType = FeeTypes.LOAN,
                Value = currentValue
            };
            UpdatePawnLoanFee(_PawnLoan, fee);

            // CL_PWN_0103_ORIGINFEEAMT
            underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0103_ORIGINFEEAMT", out currentValue);
            fee = new Fee()
            {
                FeeType = FeeTypes.ORIGIN,
                Value = currentValue
            };
            UpdatePawnLoanFee(_PawnLoan, fee);

            // CL_PWN_0104_ADMINFEEAMT
            underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0104_ADMINFEEAMT", out currentValue);
            fee = new Fee()
            {
                FeeType = FeeTypes.ADMINISTRATIVE,
                Value = currentValue
            };
            UpdatePawnLoanFee(_PawnLoan, fee);

            // CL_PWN_0105_INITCHGFEEAMT
            underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0105_INITCHGFEEAMT", out currentValue);
            fee = new Fee()
            {
                FeeType = FeeTypes.INITIAL,
                Value = currentValue
            };
            UpdatePawnLoanFee(_PawnLoan, fee);

            // CL_PWN_0106_PROCFEEAMT
            underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0106_PROCFEEAMT", out currentValue);
            fee = new Fee()
            {
                FeeType = FeeTypes.PROCESS,
                Value = currentValue
            };
            UpdatePawnLoanFee(_PawnLoan, fee);

            // CL_PWN_0115_PPCITYFEEAMT
            underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0115_PPCITYFEEAMT", out currentValue);
            fee = new Fee()
            {
                FeeType = FeeTypes.PREPAID_CITY,
                Value = currentValue
            };
            UpdatePawnLoanFee(_PawnLoan, fee);

            // CL_PWN_0030_STRGFEE
            underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0030_STRGFEE", out currentValue);
            fee = new Fee()
            {
                FeeType = FeeTypes.STORAGE,
                Value = currentValue
            };
            UpdatePawnLoanFee(_PawnLoan, fee);

            // CL_PWN_0033_MAXSTRGFEE
            underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0033_MAXSTRGFEE", out currentValue);
            fee = new Fee()
            {
                FeeType = FeeTypes.STORAGE_MAXIMUM,
                Value = currentValue
            };
            UpdatePawnLoanFee(_PawnLoan, fee);

            // CL_PWN_0037_TICKETFEE
            underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0037_TICKETFEE", out currentValue);
            fee = new Fee()
            {
                FeeType = FeeTypes.TICKET,
                Value = currentValue
            };
            UpdatePawnLoanFee(_PawnLoan, fee);

            // CL_PWN_0102_PREPFEEAMT
            underwritePawnLoanVO.feeDictionary.TryGetValue("CL_PWN_0102_PREPFEEAMT", out currentValue);
            fee = new Fee()
            {
                FeeType = FeeTypes.PREPARATION,
                Value = currentValue
            };
            UpdatePawnLoanFee(_PawnLoan, fee);

            BusinessRuleVO _BusinessRule = GlobalDataAccessor.Instance.DesktopSession.PawnBusinessRuleVO["PWN_BR-054"];
            var sComponentValue = string.Empty;

            if (sComponentValue.Equals("ROUNDED"))
            {
                _BusinessRule.getComponentValue("CL_PWN_0021_APRCALCTODEC", ref sComponentValue);
                underwritePawnLoanVO.APR = Math.Round(underwritePawnLoanVO.APR, Convert.ToInt32(sComponentValue));
            }
            else
            {
                _BusinessRule.getComponentValue("CL_PWN_0025_APRCALCRNDFAC", ref sComponentValue);
            }

            return _PawnLoan;
        }

        private static void UpdatePawnLoanFee(PawnLoan pawnLoan, Fee fee)
        {
            int iDx = pawnLoan.Fees.FindIndex(l => l.FeeType == fee.FeeType);

            if (iDx < 0)
                pawnLoan.Fees.Add(fee);
            else
            {
                pawnLoan.Fees.RemoveAt(iDx);
                pawnLoan.Fees.Insert(iDx, fee);
            }
        }
    }
}
