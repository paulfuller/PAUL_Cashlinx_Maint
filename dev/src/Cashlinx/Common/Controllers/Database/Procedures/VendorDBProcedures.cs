/**************************************************************************************************************
* CashlinxDesktop.DesktopProcedures
* VendorDBProcedures 
* DB Layer - Makes calls to the oracledata accessor
* Tracy McConnell 8/4/2010 Initial version
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database.Procedures
{
    public static class VendorDBProcedures
    {
        private const string VENDOR_DATA_NAME = "Vendor";
        private const string OUTPUT_DATA_NAME = "OUTPUT";

        #region Private Methods

        private static bool internalExecuteLookupVendor(
            List<OracleProcParam> inputParams,
            out DataTable vendor,
            out string errorCode,
            out string errorMesg)
        {
            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Setup output defaults
            vendor = null;
            errorCode = string.Empty;
            errorMesg = string.Empty;

            if (CollectionUtilities.isEmpty(inputParams))
            {
                errorCode = "LookupVendorFailed";
                errorMesg = "No input parameters specified";
            }

            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("r_vend_list", VENDOR_DATA_NAME));
            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_PURCHASES",
                    "get_vendor_details", inputParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling get_vendor_details stored procedure", oEx);
                errorCode = "GetVendDetails";
                errorMesg = "Exception: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorMesg = dA.ErrorDescription;
                return (false);
            }
            else
            {
                if (outputDataSet != null && outputDataSet.IsInitialized)
                {
                    if (outputDataSet.Tables.Count > 0)
                    {
                        vendor = outputDataSet.Tables[VENDOR_DATA_NAME];
                        return (true);
                    }
                }
            }

            errorCode = "LookupVendorFail";
            errorMesg = "Operation failed";
            return (false);
        }

        /// <summary>
        /// Internal method to call the InsertVendor stored procedure
        /// </summary>
        /// <param name="inParams"></param>
        /// <param name="vendorID"></param>/// 
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static bool ExecuteInsertVendor(List<OracleProcParam> inParams, out string vendorID, out string errorCode, out string errorMessage)
        {
            errorCode = string.Empty;
            errorMessage = string.Empty;
            vendorID = string.Empty;

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

            DataSet outputDataSet;
            bool retVal = false;

            try
            {
                GlobalDataAccessor.Instance.beginTransactionBlock();
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_purchases",
                    "addUpdateVendorInfo", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Calling insert_vendor stored procedure failed ", ex);
                GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }

            errorCode = dA.ErrorCode;
            errorMessage = dA.ErrorDescription;

            if (retVal == false)
            {
                GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            else
            {
                if (errorCode != "0")
                {
                    GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return false;
                }
                else  
                // This section of code is not working ...
                if (outputDataSet != null && outputDataSet.IsInitialized) 
                {
                    if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                    {
                        if (outputDataSet.Tables[OUTPUT_DATA_NAME].Rows.Count > 0)
                        {
                            vendorID = outputDataSet.Tables[OUTPUT_DATA_NAME].Rows[0].ItemArray[1].ToString(); // 1 is out of range.
                            GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.COMMIT);
                            return (true);
                        }
                    }
                }
                GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
        }

        # endregion

        # region Public Methods

        public static bool InsertVendor(
            string name,
            string taxID,
            string ffl,
            string address,
            string address2,
            string zip,
            string zip4,
            string city,
            string state,
            string phone,
            string phone2,
            string fax,
            string contact,
            string comment,
            string store,
            string modDate,
            string vendor_id,
            out string id,
            string userId,
            out string errorCode,
            out string errorMessage)
        {
            //Set default output params
            errorCode = string.Empty;
            errorMessage = string.Empty;
            id = vendor_id;

            if (GlobalDataAccessor.Instance == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "InsertVendor Failed";
                errorMessage = "Invalid desktop session or data accessor";
                id = string.Empty;
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();
            // if (id.Length == 0)
            //     id = "                ";

            // inParams.Add(new OracleProcParam ("p_id", OracleDbType.Varchar2, id, ParameterDirection.Input, 15));

            inParams.Add(new OracleProcParam("p_id", id));

            inParams.Add(new OracleProcParam("p_tax_id", taxID));

            inParams.Add(new OracleProcParam("p_name", name));

            inParams.Add(new OracleProcParam("p_address_1", address));

            inParams.Add(new OracleProcParam("p_address_2", address2));

            inParams.Add(new OracleProcParam("p_zip_code", zip));

            inParams.Add(new OracleProcParam("p_zip_plus4", zip4));

            inParams.Add(new OracleProcParam("p_city", city));

            inParams.Add(new OracleProcParam("p_state", state));

            inParams.Add(new OracleProcParam("p_comment", comment));

            inParams.Add(new OracleProcParam("p_ffl", ffl));

            inParams.Add(new OracleProcParam("p_store_number", store));

            inParams.Add(new OracleProcParam("p_contact_name", contact));

            inParams.Add(new OracleProcParam("p_contact_phone", phone));

            inParams.Add(new OracleProcParam("p_contact_phone_2", phone2));

            inParams.Add(new OracleProcParam("p_fax_phone", fax));

            inParams.Add(new OracleProcParam("p_user", userId));

            inParams.Add(new OracleProcParam("p_mod_date", modDate));
            inParams.Add(new OracleProcParam("o_id", OracleDbType.Varchar2, id, ParameterDirection.Output, 15));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = VendorDBProcedures.ExecuteInsertVendor(inParams, out id, out errorCode, out errorMessage);
            }
            else
            {
                id = "";
                errorCode = "InsertVendor";
                errorMessage = "No valid input parameters given";
            }

            return (rt);
        }

        /// <summary>
        /// Main Pawn method for invoking get_cust_details stored procedure
        /// </summary>
        /// <param name="id">unique ID of a vendor - Can be null or partial</param>
        /// <param name="vendorName">Name of vendor - Can be null or partial</param>
        /// <param name="taxID">Federal TaxID of vendor - Can be null or partial</param>
        /// <param name="shopNumber">Number of the store doing the lookup</param>
        /// <param name="vendor">Out param containing the vendor row / rows</param>
        /// <param name="errorCode">Out param containing the error code (only if return is false)</param>
        /// <param name="errorMesg">Out param containing the error message (only if return is false)</param>
        /// <returns>Success of the operation.  If false, check errorCode, errorMsg</returns>
        public static bool ExecuteLookupVendor(
            string vendorName,
            string taxID,
            string shopNumber,
            out DataTable vendor,
            out string errorCode,
            out string errorMesg)
        {
            //Set default output params
            vendor = null;
            errorCode = string.Empty;
            errorMesg = string.Empty;

            if (GlobalDataAccessor.Instance == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                vendor = null;
                errorCode = "LookupVendorFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //inParams.Add(new OracleProcParam("p_id", id));
            inParams.Add(new OracleProcParam("p_vend_name", vendorName));
            inParams.Add(new OracleProcParam("p_tax_id", taxID));
            inParams.Add(new OracleProcParam("p_shop_number", shopNumber));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = VendorDBProcedures.internalExecuteLookupVendor(inParams, out vendor, out errorCode, out errorMesg);
            }
            else
            {
                errorCode = "LookupVendorFail";
                errorMesg = "No valid input parameters given";
            }
            return (rt);
        }
        #endregion
    }
}
