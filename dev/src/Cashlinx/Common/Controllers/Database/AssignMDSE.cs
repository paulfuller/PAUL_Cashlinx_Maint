/**************************************************************************
 * Namespace:       CashlinxDesktop.DesktopProcedures
 * Class:           AssignMDSE
 * 
 * Description      The class handles the assignment of Items in the store.
 * 
 * History
 * David D Wise, Initial Development
 * 
 **************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database
{
    public static class AssignMDSE
    {
        private const String SQL_PACKAGE = "PAWN_MDSE_PROCS";



        public static bool GetAssignableItems(
                string StoreNumber,
                int searchType,
                string searchCriteria,
                out DataTable items,
                out string errorCode,
                out string errorText)
        {
            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add cat pointer
            inParams.Add(new OracleProcParam("p_store_number", StoreNumber));
            inParams.Add(new OracleProcParam("p_search_type", searchType));
            inParams.Add(new OracleProcParam("p_search", searchCriteria));

            //return GetItems("get_items2assign", inParams, "GetAssignableItems", out items, out errorCode, out errorText);
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;
            items = new DataTable();

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetAssignableItems";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetAssignableItems", new ApplicationException("Cannot execute the GetAssignableItems retrieval stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("c_mlist", "mdse"));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", SQL_PACKAGE, "get_items2assign",
                    inParams, refCursors, "o_error_code", "o_error_desc",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "get_items2assign";
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
                    if (outputDataSet.Tables.Contains("MDSE"))
                    {
                        items = outputDataSet.Tables["MDSE"];
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


        public static bool UpdateAssignedItems(
            string ItemArray,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdateAssignedItems";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("UpdateAssignedItems", new ApplicationException("Cannot execute the UpdateAssignedItems retrieval stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            OracleProcParam strParam = new OracleProcParam(ParameterDirection.Input, 
                DataTypeConstants.PawnDataType.LISTSTRING, 
                "p_input_string", 
                ItemArray.Length);

            foreach (string s in ItemArray.Split(new string[] {"|"}, StringSplitOptions.None))
            {
                strParam.AddValue(s);
            }

            //Add cat pointer
            inParams.Add(strParam);

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", SQL_PACKAGE, "update_itemsAssignment",
                    inParams, null, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "UpdateAssignedItemsFailed";
                errorText = "Invocation of UpdateAssignedItems stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking UpdateAssignedItems stored proc", oEx);
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

    }
}
