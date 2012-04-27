using System;
using System.Collections.Generic;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Config;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;
using System.Data;

namespace Common.Controllers.Database.Procedures
{
    public static class ShopProcedures
    {
        public static readonly int MAX_STORE_NUM_LEN = 5;
        public static readonly string CASHDRAWERS = "CashDrawers";
        public static readonly string AVAILABLECDUSERS = "AvailableCashDrawerUsers";
        public static readonly string ASSIGNEDCDUSERS = "AssignedCashDrawerUsers";
        public static readonly string AUXILIARYCDUSERS = "AuxiliaryCashDrawerUsers";

        public static readonly string STORECAL = "StoreCalendar";
        public static readonly string OPENTIME = "opentime";
        public static readonly string CLOSETIME = "closetime";
        public static readonly string DAYOFWEEK = "dayofweek";
        public static readonly string CALENDARDATE = "calenderdate";
        public static readonly string STOREINFO = "STOREINFO";

        /*
        *
        PROCEDURE orgcalendar_open_close_times  (p_store_number         IN VARCHAR2,
        o_store_calendar       OUT calendar_ref_cursor,
        o_return_code          OUT NUMBER,
        o_return_text          OUT VARCHAR2);
        * 
        */
        public static bool GetShopCalendar(
            OracleDataAccessor dA,
            string storeNumber,
            out List<ShopCalendarVO> openCloseTimes,
            out List<ShopCalendarVO> holidayList,
            out string errorCode,
            out string errorText)
        {
            //Initialize output variables
            openCloseTimes = null;
            holidayList = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            //Verify that the accessor is valid
            if (dA == null ||
                dA.Initialized == false)
            {
                errorCode = "GetShopCalendarFailed";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetShopCalendarFailed", new ApplicationException("Cannot execute the shop calendar retrieval stored procedure"));
                return (false);
            }

            //Verify that the store number is valid
            if (string.IsNullOrEmpty(storeNumber))
            {
                errorCode = "GetShopCalendarFailed";
                errorText = "Invalid store number";
                BasicExceptionHandler.Instance.AddException("GetShopCalendarFailed", new ApplicationException("Cannot execute the shop calendar retrieval stored procedure"));
                return (false);
            }

            //If the store number is not of length 5, pad it left with zeroes
            if (storeNumber.Length < MAX_STORE_NUM_LEN)
            {
                storeNumber = storeNumber.PadLeft(MAX_STORE_NUM_LEN, '0');
            }

            //Create input parameters
            var iParams = new List<OracleProcParam>();
            iParams.Add(new OracleProcParam("p_store_number", storeNumber));

            //Setup ref cursor array
            var refCursors = new List<PairType<string, string>>();
            refCursors.Add(new PairType<string, string>("o_store_calendar", STORECAL));

            //Execute stored procedure
            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_store_procs", "orgcalendar_open_close_times",
                    iParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = " -- GetShopCalendarFailed";
                errorText = "Invocation of GetShopCalendar stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking get_shop_calendar stored proc", oEx);
                return (false);
            }

            //If the stored proc returned false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode + "---GetShopCalendarFailed";
                errorText = dA.ErrorDescription + "--Return value from stored proc call is false";
                return (false);
            }

            //Populate the output list of shop calendar vo objects
            if (outputDataSet != null && outputDataSet.IsInitialized)
            {
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    DataTable shopCalendarTable = outputDataSet.Tables[STORECAL];
                    if (shopCalendarTable != null && shopCalendarTable.IsInitialized &&
                        shopCalendarTable.Rows != null && shopCalendarTable.Rows.Count > 0)
                    {
                        //Create output list
                        openCloseTimes = new List<ShopCalendarVO>(7);
                        holidayList = new List<ShopCalendarVO>(4);
                        foreach (DataRow dR in shopCalendarTable.Rows)
                        {
                            var openTimeObj = dR[OPENTIME];
                            var closeTimeObj = dR[CLOSETIME];
                            var dayOfWeekObj = dR[DAYOFWEEK];
                            var storeDateObj = dR[CALENDARDATE];

                            var openTime = DateTime.MinValue;
                            var closeTime = DateTime.MinValue;
                            var dayOfWeek = string.Empty;
                            var storeDate = DateTime.MinValue;
                            if (openTimeObj != System.DBNull.Value)
                                openTime = (DateTime)openTimeObj;
                            if (closeTimeObj != System.DBNull.Value)
                                closeTime = (DateTime)closeTimeObj;
                            if (dayOfWeekObj != System.DBNull.Value)
                                dayOfWeek = dayOfWeekObj.ToString();
                            if (storeDateObj != System.DBNull.Value)
                                storeDate = (DateTime)storeDateObj;

                            //Create ShopCalendarVO
                            var svo = new ShopCalendarVO();
                            if (openTime != DateTime.MinValue)
                                svo.setOpenTime(openTime);
                            if (closeTime != DateTime.MinValue)
                                svo.setCloseTime(closeTime);
                            if (!string.IsNullOrEmpty(dayOfWeek))
                                svo.setNameOfDay(dayOfWeek);
                            if (storeDate != DateTime.MinValue)
                                svo.setCalendarDate(storeDate);

                            if (openTime == DateTime.MinValue ||
                                closeTime == DateTime.MinValue ||
                                string.IsNullOrEmpty(dayOfWeek))
                            {
                                svo.setHolidayFlag(true);
                                svo.setWorkdayFlag(false);
                                holidayList.Add(svo);
                            }
                            else if (
                                openTime != DateTime.MinValue &&
                                closeTime != DateTime.MinValue &&
                                !string.IsNullOrEmpty(dayOfWeek))
                            {
                                svo.setHolidayFlag(false);
                                svo.setWorkdayFlag(true);
                                //Add to outgoing workday list
                                openCloseTimes.Add(svo);
                            }
                        }
                    }
                }
            }

            return (true);
        }

        /// <summary>
        /// Executes the void_pawn_loan stored procedure to void a new pawn loan
        /// </summary>
        /// <param name="icnStoreNumber"></param>
        /// <param name="ticketNumber"></param>
        /// <param name="dateMade"></param>
        /// <param name="timeMade"></param>
        /// <param name="createdBy"></param>
        /// <param name="updatedBy"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteVoidPawnLoan(
            int icnStoreNumber,
            int ticketNumber,
            DateTime dateMade,
            DateTime timeMade,
            string createdBy,
            string updatedBy,
            out string errorCode,
            out string errorText)
        {
            if (icnStoreNumber <= 0 || ticketNumber == 0 || string.IsNullOrEmpty(createdBy) ||
                string.IsNullOrEmpty(updatedBy))
            {
                errorCode = "ExecuteVoidPawnLoanFailed";
                errorText = "Invalid input parameter(s)";
                BasicExceptionHandler.Instance.AddException("ExecuteVoidPawnLoanFailed",
                                                            new ApplicationException("Cannot execute the void pawn loan stored procedure"));
                return (false);
            }

            // Initialize the error code and text output values
            errorCode = string.Empty;
            errorText = string.Empty;

            // Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteVoidPawnLoan Failed",
                                                            new ApplicationException("ExecuteVoidPawnLoan Failed: Data accessor instance is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            // Create parameter list
            OracleProcParam.TimeStampType tsType = OracleProcParam.TimeStampType.TIMESTAMP_TZ;
            List<OracleProcParam> oParams = new List<OracleProcParam>();

            oParams.Add(new OracleProcParam("p_icn_store_number", icnStoreNumber));
            oParams.Add(new OracleProcParam("p_ticket_number", ticketNumber.ToString()));
            oParams.Add(new OracleProcParam("p_date_made", dateMade));
            oParams.Add(new OracleProcParam("p_time_made", timeMade, tsType));
            oParams.Add(new OracleProcParam("p_created_by", createdBy));
            oParams.Add(new OracleProcParam("p_updated_by", updatedBy));

            // Make stored proc call
            bool retVal = false;
            try
            {
                DataSet outDSet;
                retVal = dA.issueSqlStoredProcCommand("ccsowner",
                                                      "pawn_store_procs", "void_pawn_loan", oParams, null, "o_return_code",
                                                      "o_return_text", out outDSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteVoidPawnLoan Failed", oEx);
                errorCode = " -- ExecuteVoidPawnLoanFailed";
                errorText = " --- OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteVoidPawnLoan Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteVoidPawnLoanFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            errorCode = "ExecuteVoidPawnLoanSuccess";
            errorText = "Success";

            return (true);
        }

        /// <summary>
        /// Updates Cash Drawer details
        /// </summary>
        /// <param name="deletionList"></param>
        /// <param name="primaryUserId"></param>
        /// <param name="auxList"></param>
        /// <param name="cashRegisterId"></param>
        /// <param name="branchId"></param>
        /// <param name="workstationId"></param>
        /// <param name="transactionDate"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <param name="modifyUserId"></param>
        /// <returns></returns>
        public static bool ExecuteUpdateCashDrawerDetails(
            string[] deletionList,
            string primaryUserId,
            string[] auxList,
            string cashRegisterId,
            string branchId,
            string workstationId,
            string modifyUserId,
            string transactionDate,
            out string errorCode,
            out string errorText)
        {
            if (String.IsNullOrEmpty(cashRegisterId) || String.IsNullOrEmpty(branchId) ||
                String.IsNullOrEmpty(workstationId))
            {
                errorCode = "ExecuteUpdateCashDrawerDetailsFailed";
                errorText = "Invalid input parameter(s)";
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateCashDrawerDetailsFailed",
                                                            new ApplicationException("Cannot execute the cash drawer details update stored procedure"));
                return (false);
            }

            // Initialize the error code and text output values
            errorCode = string.Empty;
            errorText = string.Empty;

            // Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateCashDrawerDetails Failed",
                                                            new ApplicationException("ExecuteUpdateCashDrawerDetails Failed: Data accessor instance is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            // Create parameter list
            //OracleProcParam.TimeStampType tsType = OracleProcParam.TimeStampType.TIMESTAMP_TZ;
            List<OracleProcParam> oParams = new List<OracleProcParam>();

            OracleProcParam delListParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_del_list", (deletionList == null) ? 1 : deletionList.Length);
            if (CollectionUtilities.isNotEmpty(deletionList))
            {
                for (int i = 0; i < deletionList.Length; ++i)
                {
                    delListParam.AddValue(deletionList[i]);
                }
            }
            else
            {
                delListParam.AddValue(string.Empty);
            }
            oParams.Add(delListParam);
            oParams.Add(new OracleProcParam("p_primary_id", primaryUserId));

            OracleProcParam auxListParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_aux_list", (auxList == null) ? 1 : auxList.Length);
            if (CollectionUtilities.isNotEmpty(auxList))
            {
                for (int i = 0; i < auxList.Length; ++i)
                {
                    auxListParam.AddValue(auxList[i]);
                }
            }
            else
            {
                auxListParam.AddValue(string.Empty);
            }
            oParams.Add(auxListParam);

            oParams.Add(new OracleProcParam("p_cashregisterid", cashRegisterId));
            //oParams.Add(new OracleProcParam("p_date_made", dateMade));
            oParams.Add(new OracleProcParam("p_branch_id", branchId));
            oParams.Add(new OracleProcParam("p_workstationid", workstationId));
            oParams.Add(new OracleProcParam("p_modify_user_name", modifyUserId));
            oParams.Add(new OracleProcParam("p_transaction_date", transactionDate));

            // Make stored proc call
            bool retVal = false;
            try
            {
                DataSet outDSet;
                retVal = dA.issueSqlStoredProcCommand("ccsowner",
                                                      "pawn_store_procs", "update_cashdrawer_details", oParams, null, "o_return_code",
                                                      "o_return_text", out outDSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateCashDrawerDetails Failed", oEx);
                errorCode = " -- ExecuteUpdateCashDrawerDetailsFailed";
                errorText = " -- OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateCashDrawerDetails Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteUpdateCashDrawerDetailsFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            errorCode = "ExecuteUpdateCashDrawerDetailsSuccess";
            errorText = "Success";

            return (true);
        }

        public static bool ExecuteGetCashDrawerDetails(
            OracleDataAccessor dA,
            string storeNumber,
            out DataTable storeCashDrawerList,
            out DataTable availableCashDrawerUsersList,
            out DataTable assignedCashDrawerUsersList,
            out DataTable auxiliaryCashDrawerUsersList,
            out string errorCode,
            out string errorText)
        {
            return (ExecuteGetCashDrawerDetails(dA,
                                                null,
                                                storeNumber,
                                                out storeCashDrawerList,
                                                out availableCashDrawerUsersList,
                                                out assignedCashDrawerUsersList,
                                                out auxiliaryCashDrawerUsersList,
                                                out errorCode,
                                                out errorText));
        }

        /// <summary>
        /// Get cash drawers and their associated user details for a store
        /// </summary>
        /// <param name="key"></param>
        /// <param name="storeNumber"></param>
        /// <param name="storeCashDrawerList"></param>
        /// <param name="availableCashDrawerUsersList"></param>
        /// <param name="assignedCashDrawerUsersList"></param>
        /// <param name="auxiliaryCashDrawerUsersList"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <param name="dA"></param>
        /// <returns></returns>
        public static bool ExecuteGetCashDrawerDetails(
            OracleDataAccessor dA,
            string key,
            string storeNumber,
            out DataTable storeCashDrawerList,
            out DataTable availableCashDrawerUsersList,
            out DataTable assignedCashDrawerUsersList,
            out DataTable auxiliaryCashDrawerUsersList,
            out string errorCode,
            out string errorText)
        {
            // Initialize the error code and text output values
            errorCode = string.Empty;
            errorText = string.Empty;

            // Initialize the DataTable objects
            storeCashDrawerList = null;
            availableCashDrawerUsersList = null;
            assignedCashDrawerUsersList = null;
            auxiliaryCashDrawerUsersList = null;

            if (String.IsNullOrEmpty(storeNumber))
            {
                errorCode = "ExecuteGetCashDrawerDetailsFailed";
                errorText = "Invalid store number";
                BasicExceptionHandler.Instance.AddException("ExecuteGetCashDrawerDetailsFailed",
                                                            new ApplicationException("Cannot execute the cash drawer details retrieval stored procedure"));
                return (false);
            }

            // Ensure the data accessor is valid
            if (dA == null ||
                dA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetCashDrawerDetails Failed",
                                                            new ApplicationException("ExecuteGetCashDrawerDetails Failed: Data accessor instance is invalid"));
                return (false);
            }

            // Create an input parameter list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            // Setup ref cursor list
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            refCursors.Add(new PairType<string, string>("o_store_cd_list", CASHDRAWERS));
            refCursors.Add(new PairType<string, string>("o_avail_cd_users_list", AVAILABLECDUSERS));
            refCursors.Add(new PairType<string, string>("o_assigned_cd_users_list", ASSIGNEDCDUSERS));
            refCursors.Add(new PairType<string, string>("o_auxiliar_cd_users_list", AUXILIARYCDUSERS));

            // Create data set
            DataSet outputDataSet;

            bool retVal = false;

            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_store_procs", "get_cashdrawer_details",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    key, out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = " -- ExecuteGetCashDrawerDetailsFailed";
                errorText = " --- Invocation of ExecuteGetCashDrawerDetails stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking ExecuteGetCashDrawerDetails stored proc",
                    oEx);
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
                    // Get cash drawer data if it exists
                    if (outputDataSet.Tables.Contains(CASHDRAWERS))
                    {
                        storeCashDrawerList = outputDataSet.Tables[CASHDRAWERS];
                    }
                    // Get available users data if it exists
                    if (outputDataSet.Tables.Contains(AVAILABLECDUSERS))
                    {
                        availableCashDrawerUsersList = outputDataSet.Tables[AVAILABLECDUSERS];
                    }
                    // Get assigned users data if it exists
                    if (outputDataSet.Tables.Contains(ASSIGNEDCDUSERS))
                    {
                        assignedCashDrawerUsersList = outputDataSet.Tables[ASSIGNEDCDUSERS];
                    }
                    // Get auxiliary users if it exists
                    if (outputDataSet.Tables.Contains(AUXILIARYCDUSERS))
                    {
                        auxiliaryCashDrawerUsersList = outputDataSet.Tables[AUXILIARYCDUSERS];
                    }
                    return (true);
                }
            }

            errorCode = "ExecuteGetCashDrawerDetailsFailed";
            errorText = "Operation failed";
            return (false);
        }

        /// <summary>
        /// Get store info
        /// </summary>
        /// <param name="dA"></param>
        /// <param name="storeNumber"></param>
        /// <param name="storeInfo"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteGetStoreInfo(
            OracleDataAccessor dA,
            string storeNumber,
            ref SiteId storeInfo,
            out string errorCode,
            out string errorText)
        {
            return (ExecuteGetStoreInfo(null, dA,
                                        storeNumber,
                                        ref storeInfo,
                                        out errorCode,
                                        out errorText));
        }

        /// <summary>
        /// Get store info
        /// </summary>
        /// <param name="key">Connection key when utilizing multiple database connections</param>
        /// <param name="dA"></param>
        /// <param name="storeNumber"></param>
        /// <param name="storeInfo"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteGetStoreInfo(
            string key,
            OracleDataAccessor dA,
            string storeNumber,
            ref SiteId storeInfo,
            out string errorCode,
            out string errorText)
        {
            // Initialize the error code and text output values
            errorCode = string.Empty;
            errorText = string.Empty;

            //Ensure the store number is valid
            if (string.IsNullOrEmpty(storeNumber))
            {
                errorCode = "ExecuteGetStoreInfoFailed";
                errorText = "Invalid store number";
                return (false);
            }

            // Ensure the data accessor is valid
            if (dA == null ||
                dA.Initialized == false)
            {
                return (false);
            }

            // Create an input parameter list
            var inParams = new List<OracleProcParam>
            {
                new OracleProcParam("p_store_number", storeNumber)
            };

            // Setup ref cursor list
            var refCursors = new List<PairType<string, string>>
            {
                new PairType<string, string>("o_store_info", STOREINFO)
            };

            //Execute stored proc
            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner",
                    "pawn_store_procs",
                    "get_store_info",
                    inParams,
                    refCursors,
                    "o_return_code",
                    "o_return_text",
                    key,
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = " -- ExecuteGetStoreInfoFailed";
                errorText = " --- Invocation of ExecuteGetStoreInfo stored proc failed:" + oEx.Message;
                return (false);
            }
            catch (Exception eX)
            {
                errorCode = " -- ExecuteGetStoreInfoFailed";
                errorText = " --- Invocation of ExecuteGetStoreInfo stored proc failed:" + eX.Message;
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
                if (outputDataSet.Tables != null &&
                    outputDataSet.Tables.Count > 0)
                {
                    if (outputDataSet.Tables.Contains(STOREINFO))
                    {
                        //Parse the outpur dataset and populate the siteid object
                        ParseStoreDataSet(outputDataSet, ref storeInfo);
                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }

            //No data set found with store information if we get here
            errorCode = "1";
            errorText = "Output data set invalid and/or does not contain store information";
            return (false);
        }

        /// <summary>
        /// Get store info
        /// </summary>
        /// <param name="dA"></param>
        /// <param name="clxShopInfoDict"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <param name="topsShopInfoList"></param>
        /// <returns></returns>
        public static bool ExecuteGetStoreInfoWithShortName(
            OracleDataAccessor dA,
            //out List<PairType<string, string>> shopInfoList,
            out Dictionary<string, string> topsShopInfoList,
            out Dictionary<string, string> clxShopInfoDict,
            out string errorCode,
            out string errorText)
        {
            // Initialize the error code and text output values
            errorCode = string.Empty;
            errorText = string.Empty;
            topsShopInfoList = new Dictionary<string, string>();
            clxShopInfoDict = new Dictionary<string, string>();
            // Ensure the data accessor is valid
            if (dA == null ||
                dA.Initialized == false)
            {
                return (false);
            }

            // Create an input parameter list
            var inParams = new List<OracleProcParam> { };
            inParams.Add(new OracleProcParam("p_transfer_eligible_date", ShopDateTime.Instance.ShopDate.ToShortDateString()));
            // Setup ref cursor list
            var refCursors = new List<PairType<string, string>>
            {
                new PairType<string, string>("o_store_info", STOREINFO)
            };
            //Execute stored proc
            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner",
                    "pawn_store_procs",
                    "list_of_to_stores",
                    inParams,
                    refCursors,
                    "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException)
            {
                errorCode = " -- ExecuteGetStoreInfoFailed";
                errorText = " --- Invocation of ExecuteGetStoreInfo stored proc failed";
                return (false);
            }
            catch (Exception)
            {
                errorCode = " -- ExecuteGetStoreInfoFailed";
                errorText = " --- Invocation of ExecuteGetStoreInfo stored proc failed";
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
                if (outputDataSet.Tables != null &&
                    outputDataSet.Tables.Count > 0)
                {
                    if (outputDataSet.Tables.Contains(STOREINFO))
                    {
                        string storeNumber = null;
                        string shortName = null;
                        bool isClxStore = false;
                        if (outputDataSet.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drow in outputDataSet.Tables[0].Rows)
                            {
                                storeNumber = Utilities.GetStringValue(drow["storenumber"], string.Empty);
                                shortName = Utilities.GetStringValue(drow["storenickname"], string.Empty);
                                isClxStore = Utilities.GetBooleanValue(drow["clx_store"], false);
                                try
                                {
                                    if (!isClxStore)
                                    {
                                        if (!topsShopInfoList.ContainsKey(shortName.ToUpper()))
                                            topsShopInfoList.Add(shortName.ToUpper(), storeNumber);
                                        else
                                            topsShopInfoList.Add(shortName.ToUpper() + storeNumber, storeNumber);
                                    }
                                    else
                                    {
                                        if (!clxShopInfoDict.ContainsKey(shortName.ToUpper()))
                                            clxShopInfoDict.Add(shortName.ToUpper(), storeNumber);
                                        else
                                            clxShopInfoDict.Add(shortName.ToUpper() + storeNumber, storeNumber);
                                    }
                                }
                                catch (Exception eX)
                                {
                                    if (FileLogger.Instance.IsLogError)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.ERROR, "ShopProcedures", "Exception thrown: {0}", eX);
                                    }
                                    BasicExceptionHandler.Instance.AddException("ATFOpenRecordsReport", eX);
                                }
                            }
                        }
                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }

                    //No data set found with store information if we get here
                    errorCode = "1";
                    errorText = "Output data set invalid and/or does not contain store information";
                    return (false);
                }
            }

            errorCode = "0";
            errorText = "Success";
            return (true);
        }

        private static void ParseStoreDataSet(DataSet outputDataSet, ref SiteId storeInfo)
        {
            if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
            {
                if (outputDataSet.Tables[0].Rows.Count > 0)
                {
                    DataRow drow = outputDataSet.Tables[0].Rows[0];

                    storeInfo.AliasID = Utilities.GetIntegerValue(drow[(int)storeinforecord.ALIAS_ID], 0);
                    storeInfo.BusinessUnit = Utilities.GetStringValue(drow[(int)storeinforecord.BUSINESSUNIT], string.Empty);
                    storeInfo.AchFileName = Utilities.GetStringValue(drow[(int)storeinforecord.ACHFILENAME], string.Empty);
                    storeInfo.Collection800 = Utilities.GetStringValue(drow[(int)storeinforecord.COLLECTION800], string.Empty);
                    storeInfo.CompanyNumber = Utilities.GetStringValue(drow[(int)storeinforecord.COMPANYNUMBER], string.Empty);
                    storeInfo.ConversionDate = Utilities.GetDateTimeValue(drow[(int)storeinforecord.CONVERSIONDATE], DateTime.MaxValue);
                    storeInfo.CountryCode = Utilities.GetStringValue(drow[(int)storeinforecord.COUNTRYCODE], string.Empty);
                    storeInfo.DeferScanDate = Utilities.GetDateTimeValue(drow[(int)storeinforecord.DEFERSCANDATE], DateTime.MaxValue);
                    storeInfo.EffDateChange = Utilities.GetDateTimeValue(drow[(int)storeinforecord.EFFDATECHANG], DateTime.MaxValue);
                    storeInfo.LocalTimeZone = Utilities.GetStringValue(drow[(int)storeinforecord.LOCALTIMEZONE], string.Empty);
                    storeInfo.Market = Utilities.GetStringValue(drow[(int)storeinforecord.MARKET], string.Empty);
                    storeInfo.Region = Utilities.GetStringValue(drow[(int)storeinforecord.REGION], string.Empty);
                    storeInfo.StoreAddress1 = Utilities.GetStringValue(drow[(int)storeinforecord.STOREADDRESS1], string.Empty);
                    storeInfo.StoreAddress2 = Utilities.GetStringValue(drow[(int)storeinforecord.STOREADDRESS2], string.Empty);
                    storeInfo.StoreBrandName = Utilities.GetStringValue(drow[(int)storeinforecord.STOREBRANDNAME], string.Empty);
                    storeInfo.StoreCityName = Utilities.GetStringValue(drow[(int)storeinforecord.STORECITYNAME], string.Empty);
                    storeInfo.StoreEffin = Utilities.GetStringValue(drow[(int)storeinforecord.STOREEFIN], string.Empty);
                    storeInfo.StoreEmail = Utilities.GetStringValue(drow[(int)storeinforecord.STOREEMAIL], string.Empty);
                    storeInfo.StoreFaxNo = Utilities.GetStringValue(drow[(int)storeinforecord.STOREFAXNO], string.Empty);
                    storeInfo.StoreId = Utilities.GetStringValue(drow[(int)storeinforecord.STOREID], string.Empty);
                    storeInfo.StoreLocator = Utilities.GetStringValue(drow[(int)storeinforecord.SHOPLOCATOR], string.Empty);
                    storeInfo.StoreManager = Utilities.GetStringValue(drow[(int)storeinforecord.STOREMANAGER], string.Empty);
                    storeInfo.StoreModemNo = Utilities.GetStringValue(drow[(int)storeinforecord.STOREMODEMNO], string.Empty);
                    storeInfo.StoreName = Utilities.GetStringValue(drow[(int)storeinforecord.STORENAME], string.Empty);
                    storeInfo.StoreNickName = Utilities.GetStringValue(drow[(int)storeinforecord.STORENICKNAME], string.Empty);
                    storeInfo.StoreOpenDate = Utilities.GetDateTimeValue(drow[(int)storeinforecord.STOREOPENDATE], DateTime.Now);
                    storeInfo.StorePhoneNo = Utilities.GetStringValue(drow[(int)storeinforecord.STOREPHONENO], string.Empty);
                    storeInfo.StorePhoneNo1 = Utilities.GetStringValue(drow[(int)storeinforecord.STOREPHONENO1], string.Empty);
                    storeInfo.StorePhoneNo2 = Utilities.GetStringValue(drow[(int)storeinforecord.STOREPHONENO2], string.Empty);
                    storeInfo.StorePhoneNo3 = Utilities.GetStringValue(drow[(int)storeinforecord.STOREPHONENO3], string.Empty);
                    //Madhu - un-commented line BZ # 238
                    storeInfo.State = Utilities.GetStringValue(drow[(int)storeinforecord.STORESTATE], string.Empty);
                    storeInfo.StoreTax1 = Utilities.GetDecimalValue(drow[(int)storeinforecord.STORETAX1], 0);
                    storeInfo.StoreTax2 = Utilities.GetDecimalValue(drow[(int)storeinforecord.STORETAX2], 0);
                    storeInfo.StoreTax3 = Utilities.GetDecimalValue(drow[(int)storeinforecord.STORETAX3], 0);
                    storeInfo.StoreTaxID = Utilities.GetStringValue(drow[(int)storeinforecord.STORETAXID], string.Empty);
                    storeInfo.StoreZipCode = Utilities.GetStringValue(drow[(int)storeinforecord.STOREZIPCODE], string.Empty);

                    //Get store type
                    const string zero = "0";
                    const string one = "1";
                    storeInfo.IsTopsExist =
                    Utilities.GetStringValue(drow[(int)storeinforecord.IS_TOPS_EXIST], zero).Equals(one);
                    storeInfo.IsTopsSafe =
                    Utilities.GetStringValue(drow[(int)storeinforecord.IS_TOPS_SAFE], zero).Equals(one);
                    storeInfo.IsPawnPrimary =
                    Utilities.GetStringValue(drow[(int)storeinforecord.IS_PAWN_PRIMARY], zero).Equals(one);
                    storeInfo.IsIntegrated =
                    Utilities.GetStringValue(drow[(int)storeinforecord.IS_INTEGRATED], zero).Equals(one);

                    //Get firearm license information
                    storeInfo.FireArmLicenceNo = Utilities.GetStringValue(drow[(int)storeinforecord.FFL], string.Empty);

                    //Get store force close time
                    storeInfo.ForceCloseTime =
                    Utilities.GetTimestampValue(
                        drow[(int)storeinforecord.FORCE_CLOSE_TIME], DateTime.MinValue);

                    //Get store party role id
                    storeInfo.PartyRoleId =
                    Utilities.GetStringValue(drow[(int)storeinforecord.PARTYROLEID], string.Empty);
                }
            }
        }

        /// <summary>
        /// Get store info 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="activeValue"></param>
        /// <param name="activeFlag"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteUpdateSelectUserInfoActivated(
            string userName,
            int activeValue,
            out int activeFlag,
            out string errorCode,
            out string errorText)
        {
            // Initialize the error code and text output values
            errorCode = string.Empty;
            errorText = string.Empty;

            // Initialize the output objects
            activeFlag = 0;

            if (string.IsNullOrEmpty(userName))
            {
                errorCode = "ExecuteUpdateSelectUserInfoActivatedFailed";
                errorText = "Invalid user name";
                BasicExceptionHandler.Instance.AddException(
                    "ExecuteUpdateSelectUserInfoActivatedFailed",
                    new ApplicationException("Cannot execute the get user activation stored procedure"));
                return (false);
            }

            // Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException(
                    "ExecuteUpdateSelectUserInfoActivatedFailed",
                    new ApplicationException(
                        "ExecuteUpdateSelectUserInfoActivatedFailed: Data accessor instance is invalid"));
                return (false);
            }

            // Get a DataAccessor
            var dA = GlobalDataAccessor.Instance.OracleDA;

            // Create an input parameter list
            var inParams = new List<OracleProcParam>
            {
                new OracleProcParam("p_user_name", userName),
                new OracleProcParam("p_active_value", activeValue),
                new OracleProcParam(
                    "o_active_flag",
                    OracleDbType.Decimal,
                    DBNull.Value,
                    ParameterDirection.Output,
                    1)
            };

            //Execute stored proc
            DataSet outputDataSet;
            bool retVal;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner",
                    "pawn_store_procs",
                    "update_retrieve_usrinfo_active",
                    inParams,
                    null,
                    "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = " -- ExecuteUpdateSelectUserInfoActivatedFailed";
                errorText = " --- Invocation of ExecuteUpdateSelectUserInfoActivated stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking ExecuteUpdateSelectUserInfoActivated stored proc",
                    oEx);
                return (false);
            }
            catch (Exception eX)
            {
                errorCode = " -- ExecuteUpdateSelectUserInfoActivatedFailed";
                errorText = " --- Invocation of ExecuteUpdateSelectUserInfoActivated stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "Exception thrown when invoking ExecuteUpdateSelectUserInfoActivated stored proc", eX);
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
                if (outputDataSet.Tables != null &&
                    outputDataSet.Tables.Count > 0)
                {
                    if (outputDataSet.Tables.Contains("OUTPUT"))
                    {
                        DataTable outputDataTable = outputDataSet.Tables["OUTPUT"];
                        if (outputDataTable.IsInitialized &&
                            outputDataTable.Rows != null &&
                            outputDataTable.Rows.Count > 0)
                        {
                            DataRow dR = outputDataTable.Rows[0];
                            if (dR != null && !dR.HasErrors &&
                                dR.ItemArray.Length > 0)
                            {
                                object dRVal = dR.ItemArray.GetValue(1);
                                if (dRVal != null)
                                {
                                    activeFlag = Utilities.GetIntegerValue(dRVal, 0);
                                    errorCode = "0";
                                    errorText = "Success";
                                    return (true);
                                }
                            }
                        }
                    }
                }
            }

            //No data set found with store information if we get here
            errorCode = "1";
            errorText = "Output data set invalid and/or could not find user information";
            return (false);
        }

        /// <summary>
        /// Get store info 
        /// NOTE: MUST PASS IN DATA ACCESSSOR AS 
        /// THIS IS CALLED FROM WITHIN CASHLINXDESKTOPSESSION CONSTRUCTOR
        /// </summary>
        /// <param name="dataAccessor"></param>
        /// <param name="databaseTime"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static void ExecuteGetDatabaseTime(
            OracleDataAccessor dataAccessor,
            out DateTime databaseTime)
        {
            ExecuteGetDatabaseTime(null, dataAccessor, out databaseTime);
        }

        public static void ExecuteGetDatabaseTime(
            string key,
            OracleDataAccessor dataAccessor,
            out DateTime databaseTime)
        {
            // Initialize the output objects
            databaseTime = DateTime.Now;
            try
            {
                // Ensure the data accessor is valid
                if (dataAccessor == null ||
                    dataAccessor.Initialized == false)
                {
                    return;
                }

                // Get a DataAccessor
                var dA = dataAccessor;

                //Output parameters
                var oParam = new List<OracleProcParam>
                {
                    new OracleProcParam(
                        "o_sysdate",
                        OracleDbType.Varchar2,
                        DBNull.Value,
                        ParameterDirection.Output,
                        256)
                };

                //Execute stored proc
                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner",
                        "pawn_gen_procs",
                        "get_sysdate",
                        oParam,
                        null,
                        "o_return_code",
                        "o_return_text",
                        key,
                        out outputDataSet);
                }
                catch (OracleException)
                {
                    return;
                }
                catch (Exception)
                {
                    return;
                }

                //See if retVal is false
                if (retVal == false)
                {
                    return;
                }

                if (outputDataSet == null)
                {
                    return;
                }
                if (outputDataSet.Tables == null || outputDataSet.Tables.Count == 0)
                {
                    return;
                }
                if (!outputDataSet.Tables.Contains("OUTPUT"))
                {
                    return;
                }

                DataTable outputDataTable = outputDataSet.Tables["OUTPUT"];

                if (!outputDataTable.IsInitialized || outputDataTable.Rows == null || outputDataTable.Rows.Count == 0)
                {
                    return;
                }

                DataRow dR = outputDataTable.Rows[0];

                if (dR == null || dR.HasErrors || dR.ItemArray.Length == 0)
                {
                    return;
                }
                var dRVal = dR.ItemArray.GetValue(1);

                if (dRVal == null)
                {
                    return;
                }
                //activeFlag = Utilities.GetIntegerValue(dRVal, 0);
                var dateTimeStr = Utilities.GetStringValue(dRVal, string.Empty);
                if (string.IsNullOrEmpty(dateTimeStr))
                {
                    return;
                }

                DateTime.TryParse(dateTimeStr, out databaseTime);
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Get store info 
        /// NOTE: MUST PASS IN DATA ACCESSSOR AS 
        /// THIS IS CALLED FROM WITHIN CASHLINXDESKTOPSESSION CONSTRUCTOR
        /// </summary>
        /// <returns></returns>
        public static bool ExecuteGetStoreStatusCode(
            OracleDataAccessor dataAccessor,
            SiteId curStore,
            out string storeStatus,
            out string errorCode,
            out string errorText)
        {
            return (ExecuteGetStoreStatusCode(
                null, dataAccessor, curStore,
                out storeStatus, out errorCode, out errorText));
        }

        public static bool ExecuteGetStoreStatusCode(
            string key,
            OracleDataAccessor dataAccessor,
            SiteId curStore,
            out string storeStatus,
            out string errorCode,
            out string errorText)
        {
            // Initialize the output objects
            storeStatus = string.Empty;
            errorCode = string.Empty;
            errorText = string.Empty;

            try
            {
                // Ensure the data accessor is valid
                if (dataAccessor == null ||
                    dataAccessor.Initialized == false)
                {
                    return (false);
                }

                // Get a DataAccessor
                var dA = dataAccessor;
                /*
                * 
                * get_sto_fininst_status (p_sto_party_role_id  IN VARCHAR2,
                o_status_code        OUT VARCHAR2,
                o_return_code        OUT NUMBER,
                o_return_text        OUT VARCHAR2)
                */
                //Input/Output parameters
                var ioParam = new List<OracleProcParam>
                {
                    new OracleProcParam(
                        "p_sto_party_role_id",
                        curStore.PartyRoleId),

                    new OracleProcParam(
                        "o_status_code",
                        OracleDbType.Varchar2,
                        DBNull.Value,
                        ParameterDirection.Output,
                        256)
                };

                //Execute stored proc
                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner",
                        "pawn_store_procs",
                        "get_sto_fininst_status",
                        ioParam,
                        null,
                        "o_return_code",
                        "o_return_text",
                        key,
                        out outputDataSet);
                }
                catch (OracleException oEx)
                {
                    errorCode = " -- ExecuteGetStoreStatusCode Failed";
                    errorText = " --- Invocation of ExecuteGetStoreStatusCode stored proc failed";
                    BasicExceptionHandler.Instance.AddException(
                        "OracleException thrown when invoking ExecuteGetStoreStatusCode stored proc",
                        oEx);
                    return (false);
                }
                catch (Exception eX)
                {
                    errorCode = " -- ExecuteGetStoreStatusCode Failed";
                    errorText = " --- Invocation of ExecuteGetStoreStatusCode stored proc failed";
                    BasicExceptionHandler.Instance.AddException(
                        "OracleException thrown when invoking ExecuteGetStoreStatusCode stored proc",
                        eX);
                    return (false);
                }

                //See if retVal is false
                if (retVal == false)
                {
                    errorCode = dA.ErrorCode ?? "1";
                    errorText = dA.ErrorDescription ?? "ExecuteGetStoreStatusCode failed";
                    return (false);
                }

                if (outputDataSet != null)
                {
                    DataTable outputTable = null;
                    object outputObject = null;
                    if (PawnDataAccessUtilities.ValidateOutputTableAndRetrieveObject(
                        outputDataSet, 0, out outputTable, out outputObject))
                    {
                        storeStatus = outputObject.ToString();
                        if (!string.IsNullOrEmpty(storeStatus))
                        {
                            errorCode = "0";
                            errorText = "Success";
                            return (true);
                        }
                    }
                }
            }
            catch (Exception eX)
            {
                errorCode = " -- ExecuteGetStoreStatusCode Failed";
                errorText = " --- Process of ExecuteGetStoreStatusCode failed";
                BasicExceptionHandler.Instance.AddException(
                    "Exception thrown when processing ExecuteGetStoreStatusCode stored proc",
                    eX);
                return (false);
            }

            errorCode = "1";
            errorText = "Failure";
            return (false);
        }

        /// <summary>
        /// Get the list of terminals in the store
        /// </summary>
        /// <param name="dA"></param>
        /// <param name="storeNumber"></param>
        /// <param name="workstationsData"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public static bool GetAllWorkstations(
            OracleDataAccessor dA,
            string storeNumber,
            out DataTable workstationsData,
            out string errorCode,
            out string errorMesg)
        {
            return (GetAllWorkstations(null,
                                       dA,
                                       storeNumber,
                                       out workstationsData,
                                       out errorCode,
                                       out errorMesg));
        }

        /// <summary>
        /// Get the list of terminals in the store
        /// </summary>
        /// <param name="dA"></param>
        /// <param name="storeNumber"></param>
        /// <param name="workstationsData"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetAllWorkstations(
            string key,
            OracleDataAccessor dA,
            string storeNumber,
            out DataTable workstationsData,
            out string errorCode,
            out string errorMesg)
        {
            //Set default output params
            workstationsData = null;

            if (dA == null)
            {
                errorCode = "GetAllWorkstationsFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>
            {
                new OracleProcParam("p_store_number", storeNumber)
            };

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_workstation_list", "workstation_details"));
                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "getallworkstations", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        key,
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling getallworkstations stored procedure", oEx);
                    errorCode = "Getallworkstations";
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
                    workstationsData = outputDataSet.Tables["workstation_details"];
                    errorCode = "";
                    errorMesg = "";
                    return true;
                }

                errorCode = "GetallworkstationsFailed";
                errorMesg = "Operation failed";
                return (false);
            }
            errorCode = "GetallworkstationsFailed";
            errorMesg = "No valid input parameters given";
            return (false);
        }

        /// <summary>
        /// SP wrapper call to get all the button tag names that are valid for the store
        /// </summary>
        /// <param name="oda"></param>
        /// <param name="pStoreNumber"></param>
        /// <param name="buttonTagNames"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool GetButtonTagNames(
            OracleDataAccessor oda,
            string pStoreNumber,
            out List<ButtonTags> buttonTagNames,
            out string errorCode,
            out string errorText)
        {
            //Set default output params
            buttonTagNames = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            if (oda == null)
            {
                errorCode = "GetButtonTagNamesFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", pStoreNumber));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_button_tags", "BUTTONTAGNAMES"));

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = oda.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_store_procs",
                        "getButtonTagNames", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling GetButtonTagNames stored procedure", oEx);
                    errorCode = "GetButtonTagNames";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oda.ErrorCode;
                    errorText = oda.ErrorDescription;
                    return (false);
                }
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    if (outputDataSet.Tables[0].Rows.Count > 0)
                    {
                        List<ButtonTags> buttonTags = new List<ButtonTags>();
                        foreach (DataRow dr in outputDataSet.Tables[0].Rows)
                        {
                            string buttonTag = Utilities.GetStringValue(dr["tag_name"], "");
                            int tellerOp = Utilities.GetIntegerValue(dr["TellerOperation"], 0);
                            ButtonTags btnTag = new ButtonTags();
                            btnTag.TagName = buttonTag;
                            btnTag.TellerOperation = tellerOp == 0 ? false : true;
                            buttonTags.Add(btnTag);
                        }
                        buttonTagNames = buttonTags;
                    }

                    return true;
                }

                errorCode = "GetButtonTagNamesFailed";
                errorText = "Operation failed";
                return (false);
            }
            errorCode = "GetButtonTagNamesFailed";
            errorText = "No valid input parameters given";
            return (false);
        }

        /// <summary>
        /// Call to the stored procedure to fetch check deposit data
        /// </summary>
        /// <param name="dA"></param>
        /// <param name="storeId"></param>
        /// <param name="curDate"></param>
        /// <param name="checkInfoDetails"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool GetStoreManualCheckDepositData(
            OracleDataAccessor dA,
            string storeId,
            DateTime curDate,
            out DataTable checkInfoDetails,
            out string errorCode,
            out string errorText)
        {
            //Set default output params
            checkInfoDetails = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            if (dA == null)
            {
                errorCode = "GetStoreManualCheckDepositDataFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_id", storeId));
            string dateToUse = curDate.FormatDate();
            inParams.Add(new OracleProcParam("p_to_date", dateToUse));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_check_info_details", "CHECKINFODETAILS"));

                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "PDA_STORE_PROCS",
                        "get_check_cashing_deposit_slip", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling get_check_cashing_deposit_slip stored procedure", oEx);
                    errorCode = "GetStoreManualCheckDepositData";
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
                    checkInfoDetails = outputDataSet.Tables[0];
                    return true;
                }

                errorCode = "GetStoreManualCheckDepositDataFailed";
                errorText = "Operation failed";
                return (false);
            }
            errorCode = "GetStoreManualCheckDepositDataFailed";
            errorText = "No valid input parameters given";

            return false;
        }
    }
}