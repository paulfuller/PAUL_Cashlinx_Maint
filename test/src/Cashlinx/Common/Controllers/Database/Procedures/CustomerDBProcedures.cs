/**************************************************************************************************************
* CashlinxDesktop.DesktopProcedures
* CustomerDBProcedures 
* DB Layer - Makes calls to the oracledata accessor
* Sreelatha Rengarajan 7/1/2009 Initial version
* SR 11/30/2010 Added wrapper for get_customer_stats Stored procedure
* SR 2/14/2011 Added store credit cursor for get cust details
* SR 4/05/2011 Added call to get ctr data for a customer
* EDW 1/13/2012 Added state code as a IN param to PICKLIST_DETAILS as part of OK changes.
* EDW 1/20/2012 Added state code as a IN param to get_cust_details and update_identification as part of OK changes.
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Security;
using System.Data;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database.Procedures
{
    public class CustomerDBProcedures
    {
        private static readonly string CUSTOMER_DATA_NAME = "Customer";
        private static readonly string OUTPUT_DATA_NAME = "OUTPUT";
        private static readonly string CUSTOMER_PHONE_DATA_NAME = "CustomerPhoneNum";
        private static readonly string CUSTOMER_IDENTITY_DATA_NAME = "CustomerIdentities";
        private static readonly string CUSTOMER_ADDRESS_DATA_NAME = "CustomerAddress";
        private static readonly string CUSTOMER_EMAIL_DATA_NAME = "CustomerEmails";
        private static readonly string CUSTOMER_NOTES_DATA_NAME = "CustomerNotes";
        private static readonly string COUNTRY_DATA_NAME = "country";
        private static readonly string EYECOLOR_DATA_NAME = "eyecolor";
        private static readonly string HAIRCOLOR_DATA_NAME = "haircolor";
        private static readonly string ID_DATA_NAME = "id";
        private static readonly string RACE_DATA_NAME = "race";
        private static readonly string STATE_DATA_NAME = "state";
        private static readonly string TITLESUFFIX_DATA_NAME = "titlesuffix";
        private static readonly string TITLE_DATA_NAME = "title";
        private static readonly string CUSTOMER_STATS = "customer_stats";
        private static readonly string CUSTOMER_STORE_CREDIT = "customer_store_credit";

        public DesktopSession DesktopSession { get; private set; }

        public CustomerDBProcedures(DesktopSession desktopSession)
        {
            DesktopSession = desktopSession;
        }
 
        #region Private Methods

        private bool internalExecuteLookupCustomer(
            List<OracleProcParam> inputParams,
            out DataTable customer,
            out DataTable customerIdentities,
            out DataTable customerContacts,
            out DataTable customerAddress,
            out DataTable customerEmails,
            out DataTable customerNotes,
            out DataTable customerStoreCredit,
            out string errorCode,
            out string errorMesg)
        {
            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Setup output defaults
            customer = null;
            customerIdentities = null;
            customerContacts = null;
            customerAddress = null;
            customerEmails = null;
            customerNotes = null;
            customerStoreCredit = null;
            errorCode = string.Empty;
            errorMesg = string.Empty;

            if (CollectionUtilities.isEmpty(inputParams))
            {
                errorCode = "LookupCustomerFailed";
                errorMesg = "No input parameters specified";
            }

            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("r_cust_list", CUSTOMER_DATA_NAME));
            refCursArr.Add(new PairType<string, string>("r_phone_list", CUSTOMER_PHONE_DATA_NAME));
            refCursArr.Add(new PairType<string, string>("r_ident_list", CUSTOMER_IDENTITY_DATA_NAME));
            refCursArr.Add(new PairType<string, string>("r_address_list", CUSTOMER_ADDRESS_DATA_NAME));
            refCursArr.Add(new PairType<string, string>("r_email_list", CUSTOMER_EMAIL_DATA_NAME));
            refCursArr.Add(new PairType<string, string>("r_notes_list", CUSTOMER_NOTES_DATA_NAME));
            refCursArr.Add(new PairType<string, string>("r_store_credit_list", CUSTOMER_STORE_CREDIT));
            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                // EDW - OK change
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_cust_procs",
                    "get_cust_details", inputParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling get_cust_details stored procedure", oEx);
                errorCode = "GetCustDetails";
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
                    if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                    {
                        customer = outputDataSet.Tables[CUSTOMER_DATA_NAME];
                        customerContacts = outputDataSet.Tables[CUSTOMER_PHONE_DATA_NAME];
                        customerIdentities = outputDataSet.Tables[CUSTOMER_IDENTITY_DATA_NAME];
                        customerAddress = outputDataSet.Tables[CUSTOMER_ADDRESS_DATA_NAME];
                        customerEmails = outputDataSet.Tables[CUSTOMER_EMAIL_DATA_NAME];
                        customerNotes = outputDataSet.Tables[CUSTOMER_NOTES_DATA_NAME];
                        customerStoreCredit = outputDataSet.Tables[CUSTOMER_STORE_CREDIT];
                        return (true);
                    }
                }
            }

            errorCode = "LookupCustomerFail";
            errorMesg = "Operation failed";
            return (false);
        }

        private bool ExecuteCheckCustForDupID(List<OracleProcParam> inParams, out DataTable customer, out string errorCode, out string errorMessage)
        {
            customer = null;
            errorCode = string.Empty;
            errorMessage = string.Empty;

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("r_cust_list", CUSTOMER_DATA_NAME));
            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_cust_procs",
                    "sp_check_cust_for_dup_id", inParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling sp_check_cust_for_dup_id stored procedure", oEx);
                errorCode = "CheckDuplicateID";
                errorMessage = "Exception: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorMessage = dA.ErrorDescription;
                return (false);
            }
            else
            {
                if (outputDataSet != null && outputDataSet.IsInitialized)
                {
                    if (outputDataSet.Tables != null)
                    {
                        customer = outputDataSet.Tables[CUSTOMER_DATA_NAME];
                        return (true);
                    }
                }
            }

            errorCode = "CheckDuplicateID";
            errorMessage = "Operation failed";
            return (false);
        }

        private bool ExecuteCheckCustForNameDOB(List<OracleProcParam> inParams, out DataTable customer, out string errorCode, out string errorMessage)
        {
            customer = null;
            errorCode = string.Empty;
            errorMessage = string.Empty;

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("r_cust_list", CUSTOMER_DATA_NAME));
            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_cust_procs",
                    "sp_check_cust_for_dup_name_dob", inParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling sp_check_cust_for_dup_name_dob stored procedure", oEx);
                errorCode = "CheckDuplicateNameDOB";
                errorMessage = "Exception: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorMessage = dA.ErrorDescription;
                return (false);
            }
            else
            {
                if (outputDataSet != null && outputDataSet.IsInitialized)
                {
                    if (outputDataSet.Tables != null)
                    {
                        customer = outputDataSet.Tables[CUSTOMER_DATA_NAME];
                        return (true);
                    }
                }
            }

            errorCode = "CheckDuplicateNameDOB";
            errorMessage = "Operation failed";
            return (false);
        }

        /// <summary>
        /// Internal method to call the InsertCustomer stored procedure
        /// </summary>
        /// <param name="inParams"></param>
        /// <param name="partyID"></param>
        /// <param name="customerNumber"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private bool ExecuteInsertCustomer(List<OracleProcParam> inParams, out string partyID, out string customerNumber, out string errorCode, out string errorMessage)
        {
            errorCode = string.Empty;
            errorMessage = string.Empty;
            customerNumber = string.Empty;
            partyID = string.Empty;

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

            DataSet outputDataSet;
            bool retVal = false;

            try
            {
                DesktopSession.beginTransactionBlock();
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_cust_procs",
                    "insert_customer", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Calling insert_customer stored procedure failed ", ex);
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }

            errorCode = dA.ErrorCode;
            errorMessage = dA.ErrorDescription;

            if (retVal == false)
            {
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            else
            {
                if (errorCode != "0")
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return false;
                }
                else
                    if (outputDataSet != null && outputDataSet.IsInitialized)
                    {
                        if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                        {
                            if (outputDataSet.Tables[OUTPUT_DATA_NAME].Rows.Count > 0)
                            {
                                partyID = outputDataSet.Tables[OUTPUT_DATA_NAME].Rows[0].ItemArray[1].ToString();
                                customerNumber = outputDataSet.Tables[OUTPUT_DATA_NAME].Rows[1].ItemArray[1].ToString();
                                DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                                return (true);
                            }
                        }
                    }
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
        }

        private bool ExecuteUpdateCustPersonalInformation(List<OracleProcParam> inParams, out string errorCode, out string errorMessage)
        {
            errorCode = string.Empty;
            errorMessage = string.Empty;

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

            DataSet outputDataSet;
            bool retVal = false;

            try
            {
                DesktopSession.beginTransactionBlock();
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_cust_procs",
                    "update_person_info_postal_info", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Calling update_person_info_postal_info  stored procedure failed", ex);
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }

            errorCode = dA.ErrorCode;
            errorMessage = dA.ErrorDescription;

            if (retVal == false)
            {
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            else
            {
                if (errorCode != "0")
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return false;
                }
                else
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    return (true);
                }
            }
        }

        private bool ExecuteUpdateCustPersonalIdentification(List<OracleProcParam> inParams, out string errorCode, out string errorMessage)
        {
            errorCode = string.Empty;
            errorMessage = string.Empty;

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

            DataSet outputDataSet;
            bool retVal = false;

            try
            {
                DesktopSession.beginTransactionBlock();
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_cust_procs",
                    "update_person_ident", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Calling update_person_ident stored procedure failed ", ex);
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            errorCode = dA.ErrorCode;
            errorMessage = dA.ErrorDescription;

            if (retVal == false)
            {
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            else
            {
                if (errorCode != "0")
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return false;
                }
                else
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    return (true);
                }
            }
        }

        private bool ExecuteUpdateIdentifications(List<OracleProcParam> inParams, out DataTable custIdentities, out string errorCode, out string errorMessage)
        {
            errorCode = string.Empty;
            errorMessage = string.Empty;
            custIdentities = null;

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("p_ident_list", CUSTOMER_IDENTITY_DATA_NAME));

            DataSet outputDataSet;
            bool retVal = false;

            try
            {
                DesktopSession.beginTransactionBlock();
                // EDW - OK change
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_cust_procs",
                    "update_identification", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Calling update_identification stored procedure failed", ex);
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            errorCode = dA.ErrorCode;
            errorMessage = dA.ErrorDescription;

            if (retVal == false)
            {
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            else
            {
                if (errorCode != "0")
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return false;
                }
                else
                    if (outputDataSet != null && outputDataSet.IsInitialized)
                    {
                        if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                        {
                            custIdentities = outputDataSet.Tables[CUSTOMER_IDENTITY_DATA_NAME];
                            DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                            return (true);
                        }
                    }
            }
            errorCode = "-1";
            errorMessage = "ExecuteUpdateIdentificationsFailed";
            DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
            return (false);
        }

        private bool ExecuteUpdatePersonalDescription(List<OracleProcParam> inParams, out string errorCode, out string errorMessage)
        {
            errorCode = string.Empty;
            errorMessage = string.Empty;

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

            DataSet outputDataSet;
            bool retVal = false;

            try
            {
                DesktopSession.beginTransactionBlock();
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_cust_procs",
                    "update_person_desc", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Calling update_person_desc stored procedure failed ", ex);
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            errorCode = dA.ErrorCode;
            errorMessage = dA.ErrorDescription;

            if (retVal == false)
            {
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            else
            {
                if (errorCode != "0")
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return false;
                }
                else
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    return (true);
                }
            }
        }

        private bool ExecuteUpdateCustPhysicalDescription(List<OracleProcParam> inParams, out string cust_prod_note_id, out string errorCode, out string errorMessage)
        {
            errorCode = string.Empty;
            errorMessage = string.Empty;
            cust_prod_note_id = string.Empty;

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

            DataSet outputDataSet;
            bool retVal = false;

            try
            {
                DesktopSession.beginTransactionBlock();
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_cust_procs",
                    "update_physical_desc", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Calling update_physical_desc stored procedure failed", ex);
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            errorCode = dA.ErrorCode;
            errorMessage = dA.ErrorDescription;

            if (retVal == false)
            {
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            else
            {
                if (errorCode != "0")
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return false;
                }
                else
                {
                    if (outputDataSet != null && outputDataSet.IsInitialized)
                    {
                        if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                        {
                            cust_prod_note_id = outputDataSet.Tables[OUTPUT_DATA_NAME].Rows[0].ItemArray[1].ToString();
                            DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                            return (true);
                        }
                    }
                    DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    return (true);
                }
            }
        }

        private bool ExecuteUpdatePhoneDetails(List<OracleProcParam> inParams, out string errorCode, out string errorMessage)
        {
            errorCode = string.Empty;
            errorMessage = string.Empty;

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

            DataSet outputDataSet;
            bool retVal = false;

            try
            {
                DesktopSession.beginTransactionBlock();
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_cust_procs",
                    "update_person_telecomnum", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Calling update_person_telecomnum stored procedure failed", ex);
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            errorCode = dA.ErrorCode;
            errorMessage = dA.ErrorDescription;

            if (retVal == false)
            {
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            else
            {
                if (errorCode != "0")
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return false;
                }
                else
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    return (true);
                }
            }
        }

        private bool ExecuteInsertPawnApplication(List<OracleProcParam> inParams, out string pawnAppId, out string errorCode, out string errorMessage)
        {
            pawnAppId = string.Empty;
            errorCode = string.Empty;
            errorMessage = string.Empty;

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

            DataSet outputDataSet;
            bool retVal = false;

            try
            {
                DesktopSession.beginTransactionBlock();
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_cust_procs",
                    "insert_pawn_application", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Calling insert_pawn_application stored procedure failed", ex);
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            errorCode = dA.ErrorCode;
            errorMessage = dA.ErrorDescription;

            if (retVal == false)
            {
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            else
            {
                if (errorCode != "0")
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return false;
                }
                else
                {
                    DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                    if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
                    {
                        DataRow dr = outputDt.Rows[0];
                        if (dr != null && dr.ItemArray != null && dr.ItemArray.Length > 0)
                        {
                            object pawnAppObj = dr.ItemArray.GetValue(1);
                            if (pawnAppObj != null)
                            {
                                pawnAppId = (string)pawnAppObj;
                            }
                        }
                    }
                    DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    return (true);
                }
            }
        }

        private bool ExecutePicklistDetails(List<OracleProcParam> inParams, out DataTable country,
                                                   out DataTable eyeColor, out DataTable hairColor, out DataTable idTypeTable,
                                                   out DataTable raceTable, out DataTable stateTable, out DataTable titleSuffixTable,
                                                   out DataTable titleTable, out string errorCode, out string errorMessage)
        {
            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Setup output defaults
            country = null;
            eyeColor = null;
            hairColor = null;
            idTypeTable = null;
            raceTable = null;
            stateTable = null;
            titleSuffixTable = null;
            titleTable = null;
            errorCode = string.Empty;
            errorMessage = string.Empty;

            inParams.Add(new OracleProcParam("p_storestatecode", DesktopSession.CurrentSiteId.State));

            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("country_cursor", COUNTRY_DATA_NAME));
            refCursArr.Add(new PairType<string, string>("eyecolor_cursor", EYECOLOR_DATA_NAME));
            refCursArr.Add(new PairType<string, string>("haircolor_cursor", HAIRCOLOR_DATA_NAME));
            refCursArr.Add(new PairType<string, string>("id_cursor", ID_DATA_NAME));
            refCursArr.Add(new PairType<string, string>("race_cursor", RACE_DATA_NAME));
            refCursArr.Add(new PairType<string, string>("state_cursor", STATE_DATA_NAME));
            refCursArr.Add(new PairType<string, string>("suffix_cursor", TITLESUFFIX_DATA_NAME));
            refCursArr.Add(new PairType<string, string>("title_cursor", TITLE_DATA_NAME));

            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    // EDW - OK change
                    "ccsowner", "PAWN_PICKLIST_EXTRACTS",
                    "picklist_details", inParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling picklist_details stored procedure", oEx);
                errorCode = "Picklist details Failed";
                errorMessage = "Exception: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorMessage = dA.ErrorDescription;
                return (false);
            }
            else
            {
                if (outputDataSet != null && outputDataSet.IsInitialized)
                {
                    if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                    {
                        country = outputDataSet.Tables[COUNTRY_DATA_NAME];
                        eyeColor = outputDataSet.Tables[EYECOLOR_DATA_NAME];
                        hairColor = outputDataSet.Tables[HAIRCOLOR_DATA_NAME];
                        idTypeTable = outputDataSet.Tables[ID_DATA_NAME];
                        raceTable = outputDataSet.Tables[RACE_DATA_NAME];
                        stateTable = outputDataSet.Tables[STATE_DATA_NAME];
                        titleSuffixTable = outputDataSet.Tables[TITLESUFFIX_DATA_NAME];
                        titleTable = outputDataSet.Tables[TITLE_DATA_NAME];
                        return (true);
                    }
                }
            }

            errorCode = "GetPicklistDetailsFailed";
            errorMessage = "Operation failed";
            return (false);
        }

        private bool ExecuteUpdateAddress(List<OracleProcParam> inParams, out string errorCode, out string errorMessage)
        {
            errorCode = string.Empty;
            errorMessage = string.Empty;

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

            DataSet outputDataSet;
            bool retVal = false;

            try
            {
                DesktopSession.beginTransactionBlock();
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_cust_procs",
                    "update_address", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Calling update_address  stored procedure failed", ex);
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            errorCode = dA.ErrorCode;
            errorMessage = dA.ErrorDescription;

            if (retVal == false)
            {
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            else
            {
                if (errorCode != "0")
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return false;
                }
                else
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    return (true);
                }
            }
        }

        private bool ExecuteUpdateCustomerContactInfo(List<OracleProcParam> inParams, out DataTable custEmail, out DataTable custPhone, out string errorCode, out string errorMessage)
        {
            errorCode = string.Empty;
            errorMessage = string.Empty;
            custEmail = null;
            custPhone = null;

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("o_email_list", CUSTOMER_EMAIL_DATA_NAME));
            refCursArr.Add(new PairType<string, string>("o_phone_cursor", CUSTOMER_PHONE_DATA_NAME));

            DataSet outputDataSet;
            bool retVal = false;

            try
            {
                DesktopSession.beginTransactionBlock();
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_cust_procs",
                    "update_customer_contactinfo", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Calling update_customer_contactinfo  stored procedure failed", ex);
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            errorCode = dA.ErrorCode;
            errorMessage = dA.ErrorDescription;

            if (retVal == false)
            {
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            else
            {
                if (errorCode != "0")
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return false;
                }
                else
                {
                    if (outputDataSet != null && outputDataSet.IsInitialized)
                    {
                        if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                        {
                            custEmail = outputDataSet.Tables[CUSTOMER_EMAIL_DATA_NAME];
                            custPhone = outputDataSet.Tables[CUSTOMER_PHONE_DATA_NAME];
                        }
                    }
                    DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    return (true);
                }
            }
        }

        private bool ExecuteUpdatePersonalInfoDetails(List<OracleProcParam> inParams, out string errorCode, out string errorMessage)
        {
            errorCode = string.Empty;
            errorMessage = string.Empty;

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

            DataSet outputDataSet;
            bool retVal = false;

            try
            {
                DesktopSession.beginTransactionBlock();
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_cust_procs",
                    "update_customer_details", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Calling update_customer_details  stored procedure failed", ex);
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            errorCode = dA.ErrorCode;
            errorMessage = dA.ErrorDescription;

            if (retVal == false)
            {
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            else
            {
                if (errorCode != "0")
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return false;
                }
                else
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    return (true);
                }
            }
        }

        private bool ExecuteUpdateLostPawnTicketFlag(List<OracleProcParam> inParams, out string errorCode, out string errorMsg)
        {
            errorCode = string.Empty;
            errorMsg = string.Empty;

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

            DataSet outputDataSet;
            bool retVal = false;

            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_store_procs",
                    "update_pawn_lost_ticket_flag", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Calling update_pawn_lost_ticket_flag  stored procedure", ex);
                return (false);
            }
            errorCode = dA.ErrorCode;
            errorMsg = dA.ErrorDescription;

            if (retVal == false)
            {
                return (false);
            }
            else
            {
                return (true);
            }
        }

        private bool ExecuteUpdateComments(List<OracleProcParam> inParams, out DataTable custNotes, out string errorCode, out string errorMessage)
        {
            errorCode = string.Empty;
            errorMessage = string.Empty;
            custNotes = null;

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("r_notes_list", CUSTOMER_NOTES_DATA_NAME));

            DataSet outputDataSet;
            bool retVal = false;

            try
            {
                DesktopSession.beginTransactionBlock();
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_store_procs",
                    "update_comments", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Calling update_comments stored procedure failed", ex);
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            errorCode = dA.ErrorCode;
            errorMessage = dA.ErrorDescription;

            if (retVal == false)
            {
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            else
            {
                if (errorCode != "0")
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return false;
                }
                else
                {
                    if (outputDataSet != null && outputDataSet.IsInitialized)
                    {
                        if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                        {
                            custNotes = outputDataSet.Tables[CUSTOMER_NOTES_DATA_NAME];
                        }
                    }
                    DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    return (true);
                }
            }
        }

        # endregion

        # region Public Methods

        /// <summary>
        /// Public method called by callers to determine if another customer
        /// with the same ID exists in the system
        /// </summary>
        /// <param name="Identtypecode"></param>
        /// <param name="IssuedNumber"></param>
        /// <param name="IssuerName"></param>
        /// <param name="customer"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool CheckDuplicateID(
            string Identtypecode,
            string IssuedNumber,
            string IssuerName,
            out DataTable customer,
            out string errorCode,
            out string errorMessage)
        {
            //Set default output params
            customer = null;
            errorCode = string.Empty;
            errorMessage = string.Empty;

            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                customer = null;
                errorCode = "CheckDuplicateID Failed";
                errorMessage = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add Identity Type
            inParams.Add(new OracleProcParam("p_ident_type_code", Identtypecode));

            //Add Issued Number
            inParams.Add(new OracleProcParam("p_issued_number", IssuedNumber));

            //Add Issuer Name
            inParams.Add(new OracleProcParam("p_issuer_name", IssuerName));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = ExecuteCheckCustForDupID(inParams, out customer, out errorCode, out errorMessage);
            }
            else
            {
                errorCode = "CheckDuplicateId";
                errorMessage = "No valid input parameters given";
            }
            return (rt);
        }

        /// <summary>
        /// Public method called by callers to check if a customerr
        /// Exists in the system with the same first name, last name and DOB
        /// </summary>
        /// <param name="Firstname"></param>
        /// <param name="Lastname"></param>
        /// <param name="Birthdate"></param>
        /// <param name="customer"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool CheckDuplicateNameDOB(
            string Firstname,
            string Lastname,
            string Birthdate,
            out DataTable customer,
            out string errorCode,
            out string errorMessage)
        {
            //Set default output params
            customer = null;
            errorCode = string.Empty;
            errorMessage = string.Empty;

            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                customer = null;
                errorCode = "CheckDuplicateNameDOB Failed";
                errorMessage = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add First Name
            inParams.Add(new OracleProcParam("p_first_name", Firstname));

            //Add Last Name
            inParams.Add(new OracleProcParam("p_last_name", Lastname));

            //Add Birth Date
            inParams.Add(new OracleProcParam("p_birth_date", Birthdate));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = ExecuteCheckCustForNameDOB(inParams, out customer, out errorCode, out errorMessage);
            }
            else
            {
                errorCode = "CheckDuplicateNameDOB";
                errorMessage = "No valid input parameters given";
            }
            return (rt);
        }

        public bool InsertCustomer(
            string inobjecttypecode,
            string inpersonfirstname,
            string inpersonmiddlename,
            string inpersonfamilyname,
            string inpersondob,
            string inuserid,
            string inpersoneyecolor,
            string inpersonhaircolor,
            string inpersonheight,
            string inpersonweight,
            string inpersonrace,
            string[] contactTypeCode,
            string[] phoneNumber,
            string[] areaCode,
            string[] countryCode,
            string[] phoneExtension,
            string[] telecomNumType,
            string[] isPrimary,
            string inunitnumber,
            string inaddress1text,
            string incityname,
            string instateprovincename,
            string inpostalcodetext,
            string[] inidissuednumber,
            string[] inidissueddate,
            string[] inidexpirationdate,
            string[] inidissuername,
            string[] inidentitytypecode,
            string insocialsecurity,
            string innameprefix,
            string ingendercode,
            string innamesuffix,
            string inprimaryemail,
            string incomments,
            string inhowdiduhrabtus,
            string inrcvprmooffr,
            string instorenumber,
            out string partyID,
            out string customerNumber,
            out string errorCode,
            out string errorMessage)
        {
            //Set default output params
            errorCode = string.Empty;
            errorMessage = string.Empty;
            customerNumber = string.Empty;
            partyID = string.Empty;

            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "InsertCustomer Failed";
                errorMessage = "Invalid desktop session or data accessor";
                customerNumber = "";
                partyID = "";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("inobjecttypecode", inobjecttypecode));

            inParams.Add(new OracleProcParam("inpersonfirstname", inpersonfirstname));

            inParams.Add(new OracleProcParam("inpersonmiddlename", inpersonmiddlename));

            inParams.Add(new OracleProcParam("inpersonfamilyname", inpersonfamilyname));

            inParams.Add(new OracleProcParam("inpersondob", inpersondob));

            inParams.Add(new OracleProcParam("inuserid", inuserid));

            inParams.Add(new OracleProcParam("inpersoneyecolor", inpersoneyecolor));

            inParams.Add(new OracleProcParam("inpersonhaircolor", inpersonhaircolor));

            inParams.Add(new OracleProcParam("inpersonheight", inpersonheight));

            inParams.Add(new OracleProcParam("inpersonweight", inpersonweight));

            inParams.Add(new OracleProcParam("inpersonrace", inpersonrace));

            //Add contact type array as param

            OracleProcParam orpmContactType = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_contact_type_code", contactTypeCode.Length);
            for (int i = 0; i < contactTypeCode.Length; i++)
            {
                orpmContactType.AddValue(contactTypeCode[i]);
            }

            inParams.Add(orpmContactType);

            //Add phone number array as param

            OracleProcParam orpmPhoneNum = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_phone_number", phoneNumber.Length);
            for (int i = 0; i < phoneNumber.Length; i++)
            {
                orpmPhoneNum.AddValue(phoneNumber[i]);
            }

            inParams.Add(orpmPhoneNum);

            //Add area code array as parameter

            OracleProcParam orpmAreaCode = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_area_code", areaCode.Length);
            for (int i = 0; i < areaCode.Length; i++)
            {
                orpmAreaCode.AddValue(areaCode[i]);
            }

            inParams.Add(orpmAreaCode);

            //Add country code array as parameter

            OracleProcParam orpmCountryCode = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_country_code", countryCode.Length);
            for (int i = 0; i < countryCode.Length; i++)
            {
                orpmCountryCode.AddValue(countryCode[i]);
            }

            inParams.Add(orpmCountryCode);

            //Add phone extension as parameter

            OracleProcParam orpmPhoneExtn = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_phone_extension", phoneExtension.Length);
            for (int i = 0; i < phoneExtension.Length; i++)
            {
                orpmPhoneExtn.AddValue(phoneExtension[i]);
            }

            inParams.Add(orpmPhoneExtn);

            //Add Telecomnumtypecode array as parameter

            OracleProcParam orpmTelecomNumType = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_telecom_num_type", telecomNumType.Length);
            for (int i = 0; i < telecomNumType.Length; i++)
            {
                orpmTelecomNumType.AddValue(telecomNumType[i]);
            }

            inParams.Add(orpmTelecomNumType);

            //add isprimary array as parameter

            OracleProcParam orpmIsPrimary = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_is_primary", isPrimary.Length);
            for (int i = 0; i < isPrimary.Length; i++)
            {
                orpmIsPrimary.AddValue(isPrimary[i]);
            }

            inParams.Add(orpmIsPrimary);

            inParams.Add(new OracleProcParam("inunitnumber", inunitnumber));

            inParams.Add(new OracleProcParam("inaddress1text", inaddress1text));

            inParams.Add(new OracleProcParam("incityname", incityname));

            inParams.Add(new OracleProcParam("instateprovincename", instateprovincename));

            inParams.Add(new OracleProcParam("inpostalcodetext", inpostalcodetext));

            OracleProcParam orpmIdNumber = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "inidissuednumber", inidissuednumber.Length);
            for (int i = 0; i < inidissuednumber.Length; i++)
            {
                orpmIdNumber.AddValue(inidissuednumber[i]);
            }

            inParams.Add(orpmIdNumber);

            OracleProcParam orpmIdIssueDate = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "inidissueddate", inidissueddate.Length);
            for (int i = 0; i < inidissueddate.Length; i++)
            {
                orpmIdIssueDate.AddValue(inidissueddate[i]);
            }

            inParams.Add(orpmIdIssueDate);

            OracleProcParam orpmIdExpirationDate = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "inidexpirationdate", inidexpirationdate.Length);
            for (int i = 0; i < inidexpirationdate.Length; i++)
            {
                orpmIdExpirationDate.AddValue(inidexpirationdate[i]);
            }

            inParams.Add(orpmIdExpirationDate);

            OracleProcParam orpmIdIssuerName = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "inidissuername", inidissuername.Length);
            for (int i = 0; i < inidissuername.Length; i++)
            {
                orpmIdIssuerName.AddValue(inidissuername[i]);
            }

            inParams.Add(orpmIdIssuerName);

            OracleProcParam orpmIdTypeCode = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "inidentitytypecode", inidentitytypecode.Length);
            for (int i = 0; i < inidentitytypecode.Length; i++)
            {
                orpmIdTypeCode.AddValue(inidentitytypecode[i]);
            }

            inParams.Add(orpmIdTypeCode);

            inParams.Add(new OracleProcParam("insocialsecurity", insocialsecurity));

            inParams.Add(new OracleProcParam("innameprefix", innameprefix));

            inParams.Add(new OracleProcParam("ingendercode", ingendercode));

            inParams.Add(new OracleProcParam("innamesuffix", innamesuffix));

            inParams.Add(new OracleProcParam("inprimaryemail", inprimaryemail));

            inParams.Add(new OracleProcParam("incomments", incomments));

            inParams.Add(new OracleProcParam("inhowdiduhrabtus", inhowdiduhrabtus));

            inParams.Add(new OracleProcParam("inrcvprmooffr", inrcvprmooffr));

            inParams.Add(new OracleProcParam("instorenumber", instorenumber));

            inParams.Add(new OracleProcParam("p_transaction_date", ShopDateTime.Instance.ShopDate.ToShortDateString()));

            inParams.Add(new OracleProcParam("o_party_id", OracleDbType.Varchar2, partyID, ParameterDirection.Output, 47));

            inParams.Add(new OracleProcParam("o_customer_num", OracleDbType.Varchar2, customerNumber, ParameterDirection.Output, 20));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = ExecuteInsertCustomer(inParams, out partyID, out customerNumber, out errorCode, out errorMessage);
            }
            else
            {
                customerNumber = "";
                errorCode = "InsertCustomer";
                errorMessage = "No valid input parameters given";
            }

            return (rt);
        }

        /// <summary>
        /// Public method to update personal information for a customer
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="middlename"></param>
        /// <param name="familyname"></param>
        /// <param name="titlecode"></param>
        /// <param name="namesuffix"></param>
        /// <param name="address1"></param>
        /// <param name="unitnum"></param>
        /// <param name="cityname"></param>
        /// <param name="state"></param>
        /// <param name="postalcode"></param>
        /// <param name="userid"></param>
        /// <param name="partyid"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool UpdateCustPersonalInformation(
            string firstname,
            string middlename,
            string familyname,
            string titlecode,
            string namesuffix,
            string address1,
            string unitnum,
            string cityname,
            string state,
            string postalcode,
            string userid,
            string partyid,
            string dateofbirth,
            out string errorCode,
            out string errorMessage)
        {
            //Set default output params
            errorCode = string.Empty;
            errorMessage = string.Empty;

            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdateCustPersonalInformation Failed";
                errorMessage = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_first_name", firstname));

            inParams.Add(new OracleProcParam("p_middle_name", middlename));

            inParams.Add(new OracleProcParam("p_family_name", familyname));

            inParams.Add(new OracleProcParam("p_title_code", titlecode));

            inParams.Add(new OracleProcParam("p_name_suffix", namesuffix));

            inParams.Add(new OracleProcParam("p_address1", address1));

            inParams.Add(new OracleProcParam("p_unit_num", unitnum));

            inParams.Add(new OracleProcParam("p_city_name", cityname));

            inParams.Add(new OracleProcParam("p_state", state));

            inParams.Add(new OracleProcParam("p_postal_code", postalcode));

            inParams.Add(new OracleProcParam("p_user_id", userid));

            inParams.Add(new OracleProcParam("p_party_id", partyid));

            inParams.Add(new OracleProcParam("p_date_of_birth", dateofbirth));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = ExecuteUpdateCustPersonalInformation(inParams, out errorCode, out errorMessage);
            }
            else
            {
                errorCode = "UpdateCustPersonalInformation";
                errorMessage = "No valid input parameters given";
            }
            return (rt);
        }

        /// <summary>
        /// Public method to update datedident primary flag
        /// </summary>
        /// <param name="IdentId"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <returns> bool </returns>

        public bool UpdatePrimaryFlagDatedIdent(
            string IdentId,
            out string errorCode,
            out string errorMessage)
        {
            //Set default output params
            errorCode = string.Empty;
            errorMessage = string.Empty;

            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdatePrimaryFlagDatedIdent Failed";
                errorMessage = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_datedident_id", IdentId));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = ExecuteUpdatePrimaryFlagDatedIdent(inParams, out errorCode, out errorMessage);
            }
            else
            {
                errorCode = "UpdateCustPersonalIdentification";
                errorMessage = "No valid input parameters given";
            }
            return (rt);
        }

        /// <summary>
        /// method to update datedident primary flag
        /// </summary>
        /// <returns> bool </returns>
        private bool ExecuteUpdatePrimaryFlagDatedIdent(List<OracleProcParam> inParams, out string errorCode, out string errorMessage)
        {
            errorCode = string.Empty;
            errorMessage = string.Empty;

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

            DataSet outputDataSet;
            bool retVal = false;

            try
            {
                DesktopSession.beginTransactionBlock();
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_cust_procs",
                    "update_primary_flag_datedident", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Calling update_primary_flag_datedident stored procedure failed ", ex);
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            errorCode = dA.ErrorCode;
            errorMessage = dA.ErrorDescription;

            if (retVal == false)
            {
                DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                return (false);
            }
            else
            {
                if (errorCode != "0")
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return false;
                }
                else
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    return (true);
                }
            }
        }

        public bool UpdateCustPersonalIdentification(
            string partyid,
            string issuednumber,
            string expiredate,
            string identtypecode,
            string issuername,
            string createdby,
            out string errorCode,
            out string errorMessage)
        {
            //Set default output params
            errorCode = string.Empty;
            errorMessage = string.Empty;

            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdateCustPersonalIdentification Failed";
                errorMessage = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_party_id", partyid));

            inParams.Add(new OracleProcParam("p_issued_number", issuednumber));

            inParams.Add(new OracleProcParam("p_expire_date", expiredate));

            inParams.Add(new OracleProcParam("p_ident_type_code", identtypecode));

            inParams.Add(new OracleProcParam("p_issuer_name", issuername));

            inParams.Add(new OracleProcParam("p_created_by", createdby));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = ExecuteUpdateCustPersonalIdentification(inParams, out errorCode, out errorMessage);
            }
            else
            {
                errorCode = "UpdateCustPersonalIdentification";
                errorMessage = "No valid input parameters given";
            }
            return (rt);
        }

        public bool UpdateCustomerPersonalIdentifications(
            string userid,
            string partyid,
            string[] issuednumber,
            string[] expiredate,
            string[] issuername,
            string[] identtypecode,
            string[] identid,
            out DataTable custIdentities,
            out string errorCode,
            out string errorMessage)
        {
            //Set default output params
            errorCode = string.Empty;
            errorMessage = string.Empty;
            custIdentities = null;

            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdateCustomerPersonalIdentifications Failed";
                errorMessage = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_user_id", userid));

            inParams.Add(new OracleProcParam("p_party_id", partyid));

            //Add issued number array as param
            OracleProcParam orpmContactType = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_issued_number", issuednumber.Length);
            for (int i = 0; i < issuednumber.Length; i++)
            {
                orpmContactType.AddValue(issuednumber[i]);
            }
            inParams.Add(orpmContactType);

            //Add expiry date array as param
            orpmContactType = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_expire_date", expiredate.Length);
            for (int i = 0; i < expiredate.Length; i++)
            {
                orpmContactType.AddValue(expiredate[i]);
            }
            inParams.Add(orpmContactType);

            //Add issuer name array as param
            orpmContactType = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_issuer_name", issuername.Length);
            for (int i = 0; i < issuername.Length; i++)
            {
                orpmContactType.AddValue(issuername[i]);
            }
            inParams.Add(orpmContactType);

            //Add ident type code array as param

            orpmContactType = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_ident_type_code", identtypecode.Length);
            for (int i = 0; i < identtypecode.Length; i++)
            {
                orpmContactType.AddValue(identtypecode[i]);
            }
            inParams.Add(orpmContactType);

            //Add ident id array as param
            orpmContactType = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_ident_id", identid.Length);
            for (int i = 0; i < identid.Length; i++)
            {
                orpmContactType.AddValue(identid[i]);
            }
            inParams.Add(orpmContactType);

            inParams.Add(new OracleProcParam("p_storestatecode", DesktopSession.CurrentSiteId.State));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = ExecuteUpdateIdentifications(inParams, out custIdentities, out errorCode, out errorMessage);
            }
            else
            {
                errorCode = "UpdateCustomerPersonalIdentifications";
                errorMessage = "No valid input parameters given";
            }
            return (rt);
        }

        public bool UpdateCustomerComments(
            string[] userid,
            string customernumber,
            string[] comments,
            string[] custproductnoteid,
            string storenumber,
            out DataTable custNotes,
            out string errorCode,
            out string errorMessage)
        {
            //Set default output params
            errorCode = string.Empty;
            errorMessage = string.Empty;
            custNotes = null;

            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdateCustomerComments Failed";
                errorMessage = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add userid array as param
            OracleProcParam orpmContactType = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_user_id", userid.Length);
            for (int i = 0; i < userid.Length; i++)
            {
                orpmContactType.AddValue(userid[i]);
            }
            inParams.Add(orpmContactType);

            inParams.Add(new OracleProcParam("p_customer_number", customernumber));

            //Add comments array as param
            orpmContactType = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_comments", comments.Length);
            for (int i = 0; i < comments.Length; i++)
            {
                orpmContactType.AddValue(comments[i]);
            }
            inParams.Add(orpmContactType);

            //Add customerproductnoteid array as param
            orpmContactType = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_customer_product_note_id", custproductnoteid.Length);
            for (int i = 0; i < custproductnoteid.Length; i++)
            {
                orpmContactType.AddValue(custproductnoteid[i]);
            }
            inParams.Add(orpmContactType);
            inParams.Add(new OracleProcParam("p_store_number", storenumber));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = ExecuteUpdateComments(inParams, out custNotes, out errorCode, out errorMessage);
            }
            else
            {
                errorCode = "UpdateCustomerComments";
                errorMessage = "No valid input parameters given";
            }
            return (rt);
        }

        public bool UpdatePersonalDescription(
            string gendercode,
            string height,
            string weight,
            string eyecolor,
            string haircolor,
            string userid,
            string partyid,
            string race,
            out string errorCode,
            out string errorMessage)
        {
            //Set default output params
            errorCode = string.Empty;
            errorMessage = string.Empty;

            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdatePersonalDescription Failed";
                errorMessage = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_gender_code", gendercode));

            inParams.Add(new OracleProcParam("p_height", height));

            inParams.Add(new OracleProcParam("p_weight", weight));

            inParams.Add(new OracleProcParam("p_eye_color", eyecolor));

            inParams.Add(new OracleProcParam("p_hair_color", haircolor));

            inParams.Add(new OracleProcParam("p_user_id", userid));

            inParams.Add(new OracleProcParam("p_party_id", partyid));

            inParams.Add(new OracleProcParam("p_race", race));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = ExecuteUpdatePersonalDescription(inParams, out errorCode, out errorMessage);
            }
            else
            {
                errorCode = "UpdatePersonalDescription";
                errorMessage = "No valid input parameters given";
            }
            return (rt);
        }

        public bool UpdateCustPhysicalDescription(
            string race,
            string gendercode,
            string haircolor,
            string eyecolor,
            string height,
            string weight,
            string userid,
            string partyid,
            string customernumber,
            string contactnote,
            string custproductnoteid,
            string storenumber,
            out string cust_prod_note_id,
            out string errorCode,
            out string errorMessage)
        {
            //Set default output params
            errorCode = string.Empty;
            errorMessage = string.Empty;
            cust_prod_note_id = "";

            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdateCustPhysicalDescription Failed";
                errorMessage = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_race", race));

            inParams.Add(new OracleProcParam("p_gender_code", gendercode));

            inParams.Add(new OracleProcParam("p_hair_color", haircolor));

            inParams.Add(new OracleProcParam("p_eye_color", eyecolor));

            inParams.Add(new OracleProcParam("p_height", height));

            inParams.Add(new OracleProcParam("p_weight", weight));

            inParams.Add(new OracleProcParam("p_user_id", userid));

            inParams.Add(new OracleProcParam("p_party_id", partyid));

            inParams.Add(new OracleProcParam("p_customer_number", customernumber));

            inParams.Add(new OracleProcParam("p_contact_note", contactnote));

            inParams.Add(new OracleProcParam("p_cust_product_note_id", custproductnoteid));

            inParams.Add(new OracleProcParam("p_store_number", storenumber));

            inParams.Add(new OracleProcParam("o_customer_product_note_id", OracleDbType.Varchar2, cust_prod_note_id, ParameterDirection.Output, 47));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = ExecuteUpdateCustPhysicalDescription(inParams, out cust_prod_note_id, out errorCode, out errorMessage);
            }
            else
            {
                errorCode = "UpdateCustPhysicalDescription";
                errorMessage = "No valid input parameters given";
            }
            return (rt);
        }

        public bool UpdatePhoneDetails(
            string partyid,
            string[] contactTypeCode,
            string[] phoneNumber,
            string[] areaCode,
            string[] countryCode,
            string[] phoneExtension,
            string[] telecomNumType,
            string[] isPrimary,
            string userid,
            out string errorCode,
            out string errorMessage)
        {
            //Set default output params
            errorCode = string.Empty;
            errorMessage = string.Empty;

            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdatePhoneDetails Failed";
                errorMessage = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add contact type array as param

            OracleProcParam orpmContactType = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_contact_type_code", contactTypeCode.Length);
            for (int i = 0; i < contactTypeCode.Length; i++)
            {
                orpmContactType.AddValue(contactTypeCode[i]);
            }

            inParams.Add(orpmContactType);

            //Add phone number array as param

            OracleProcParam orpmPhoneNum = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_phone_number", phoneNumber.Length);
            for (int i = 0; i < phoneNumber.Length; i++)
            {
                orpmPhoneNum.AddValue(phoneNumber[i]);
            }

            inParams.Add(orpmPhoneNum);

            //Add area code array as parameter

            OracleProcParam orpmAreaCode = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_area_code", areaCode.Length);
            for (int i = 0; i < areaCode.Length; i++)
            {
                orpmAreaCode.AddValue(areaCode[i]);
            }

            inParams.Add(orpmAreaCode);

            //Add country code array as parameter

            OracleProcParam orpmCountryCode = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_country_code", countryCode.Length);
            for (int i = 0; i < countryCode.Length; i++)
            {
                orpmCountryCode.AddValue(countryCode[i]);
            }

            inParams.Add(orpmCountryCode);

            //Add phone extension as parameter

            OracleProcParam orpmPhoneExtn = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_phone_extension", phoneExtension.Length);
            for (int i = 0; i < phoneExtension.Length; i++)
            {
                orpmPhoneExtn.AddValue(phoneExtension[i]);
            }

            inParams.Add(orpmPhoneExtn);

            //Add Telecomnumtypecode array as parameter

            OracleProcParam orpmTelecomNumType = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_telecom_num_type", telecomNumType.Length);
            for (int i = 0; i < telecomNumType.Length; i++)
            {
                orpmTelecomNumType.AddValue(telecomNumType[i]);
            }

            inParams.Add(orpmTelecomNumType);

            //add isprimary array as parameter

            OracleProcParam orpmIsPrimary = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_is_primary", isPrimary.Length);
            for (int i = 0; i < isPrimary.Length; i++)
            {
                orpmIsPrimary.AddValue(isPrimary[i]);
            }

            inParams.Add(orpmIsPrimary);

            inParams.Add(new OracleProcParam("p_party_id", partyid));

            inParams.Add(new OracleProcParam("p_user_id", userid));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = ExecuteUpdatePhoneDetails(inParams, out errorCode, out errorMessage);
            }
            else
            {
                errorCode = "UpdatePhoneDetails";
                errorMessage = "No valid input parameters given";
            }
            return (rt);
        }

        public bool UpdateAddress(
            string[] contactTypeCode,
            string[] address,
            string[] unit,
            string[] cityName,
            string[] postalCode,
            string[] state,
            string[] country,
            string[] notes,
            string[] alternate2String,
            string customerNumber,
            string partyId,
            string userid,
            out string errorCode,
            out string errorMessage)
        {
            //Set default output params
            errorCode = string.Empty;
            errorMessage = string.Empty;

            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdateAddress Failed";
                errorMessage = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();
            OracleProcParam orpmContTypeCode = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_contact_type_code", contactTypeCode.Length);
            for (int i = 0; i < contactTypeCode.Length; i++)
            {
                orpmContTypeCode.AddValue(contactTypeCode[i]);
            }

            inParams.Add(orpmContTypeCode);

            OracleProcParam orpmAddr = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_address", address.Length);
            for (int i = 0; i < address.Length; i++)
            {
                orpmAddr.AddValue(address[i]);
            }

            inParams.Add(orpmAddr);

            OracleProcParam orpmUnit = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_unit", unit.Length);
            for (int i = 0; i < unit.Length; i++)
            {
                orpmUnit.AddValue(unit[i]);
            }

            inParams.Add(orpmUnit);

            OracleProcParam orpmCity = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_city_name", cityName.Length);
            for (int i = 0; i < cityName.Length; i++)
            {
                orpmCity.AddValue(cityName[i]);
            }

            inParams.Add(orpmCity);
            OracleProcParam orpmZip = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_postal_code", postalCode.Length);
            for (int i = 0; i < postalCode.Length; i++)
            {
                orpmZip.AddValue(postalCode[i]);
            }

            inParams.Add(orpmZip);

            OracleProcParam orpmState = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_state", state.Length);
            for (int i = 0; i < state.Length; i++)
            {
                orpmState.AddValue(state[i]);
            }

            inParams.Add(orpmState);

            OracleProcParam orpmCountry = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_country", country.Length);
            for (int i = 0; i < country.Length; i++)
            {
                orpmCountry.AddValue(country[i]);
            }

            inParams.Add(orpmCountry);
            OracleProcParam orpmNotes = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_notes", notes.Length);
            for (int i = 0; i < notes.Length; i++)
            {
                orpmNotes.AddValue(notes[i]);
            }

            inParams.Add(orpmNotes);
            OracleProcParam orpmAltstring = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_alternate_string", alternate2String.Length);
            for (int i = 0; i < alternate2String.Length; i++)
            {
                orpmAltstring.AddValue(alternate2String[i]);
            }

            inParams.Add(orpmAltstring);

            inParams.Add(new OracleProcParam("p_customer_number", customerNumber));
            inParams.Add(new OracleProcParam("p_party_id", partyId));
            inParams.Add(new OracleProcParam("p_user_id", userid));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = ExecuteUpdateAddress(inParams, out errorCode, out errorMessage);
            }
            else
            {
                errorCode = "UpdateAddress";
                errorMessage = "No valid input parameters given";
            }
            return (rt);
        }

        public bool UpdatePersonalInfoDetails(
            string title,
            string firstname,
            string middleinitial,
            string lastname,
            string titlesuffix,
            string negotiationlanguage,
            string dateofbirth,
            string socialsecuritynumber,
            string userid,
            string partyId,
            out string errorCode,
            out string errorMessage)
        {
            //Set default output params
            errorCode = string.Empty;
            errorMessage = string.Empty;

            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdatePersonalInfoDetails Failed";
                errorMessage = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_title", title));
            inParams.Add(new OracleProcParam("p_firstname", firstname));
            inParams.Add(new OracleProcParam("p_middleinitial", middleinitial));
            inParams.Add(new OracleProcParam("p_lastname", lastname));
            inParams.Add(new OracleProcParam("p_titlesuffix", titlesuffix));
            inParams.Add(new OracleProcParam("p_negotiation_lang", negotiationlanguage));
            inParams.Add(new OracleProcParam("p_birth_date", dateofbirth));
            inParams.Add(new OracleProcParam("p_ssn", socialsecuritynumber));
            inParams.Add(new OracleProcParam("p_user_id", userid));
            inParams.Add(new OracleProcParam("p_party_id", partyId));
            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = ExecuteUpdatePersonalInfoDetails(inParams, out errorCode, out errorMessage);
            }
            return (rt);
        }

        public bool UpdateCustomerContactInfo(
            string[] contactTypeCode,
            string[] phoneNumber,
            string[] areaCode,
            string[] countryCode,
            string[] phoneExtension,
            string[] telecomNumType,
            string[] isPrimary,
            string[] email,
            string[] emailContactInfoId,
            string[] emailType,
            string nocalls,
            string noemails,
            string nofaxes,
            string nomails,
            string optout,
            string remindpmtdue,
            string prefcontact,
            string prefcalltime,
            string howdidyouhear,
            string receivepromotions,
            string customernumber,
            string partyId,
            string userid,
            out DataTable custEmail,
            out DataTable custPhone,
            out string errorCode,
            out string errorMessage)
        {
            //Set default output params
            errorCode = string.Empty;
            errorMessage = string.Empty;
            custEmail = null;
            custPhone = null;

            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "UpdateCustomerContactInfo Failed";
                errorMessage = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();
            //Add contact type array as param
            OracleProcParam orpmContactType = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_contact_type_code", contactTypeCode.Length);
            for (int i = 0; i < contactTypeCode.Length; i++)
            {
                orpmContactType.AddValue(contactTypeCode[i]);
            }
            inParams.Add(orpmContactType);

            //Add phone number array as param
            OracleProcParam orpmPhoneNum = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_phone_number", phoneNumber.Length);
            for (int i = 0; i < phoneNumber.Length; i++)
            {
                orpmPhoneNum.AddValue(phoneNumber[i]);
            }
            inParams.Add(orpmPhoneNum);

            //Add area code array as parameter
            OracleProcParam orpmAreaCode = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_area_code", areaCode.Length);
            for (int i = 0; i < areaCode.Length; i++)
            {
                orpmAreaCode.AddValue(areaCode[i]);
            }
            inParams.Add(orpmAreaCode);

            //Add country code array as parameter
            OracleProcParam orpmCountryCode = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_country_code", countryCode.Length);
            for (int i = 0; i < countryCode.Length; i++)
            {
                orpmCountryCode.AddValue(countryCode[i]);
            }
            inParams.Add(orpmCountryCode);

            //Add phone extension as parameter
            OracleProcParam orpmPhoneExtn = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_phone_extension", phoneExtension.Length);
            for (int i = 0; i < phoneExtension.Length; i++)
            {
                orpmPhoneExtn.AddValue(phoneExtension[i]);
            }
            inParams.Add(orpmPhoneExtn);

            //Add Telecomnumtypecode array as parameter
            OracleProcParam orpmTelecomNumType = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_telecom_num_type", telecomNumType.Length);
            for (int i = 0; i < telecomNumType.Length; i++)
            {
                orpmTelecomNumType.AddValue(telecomNumType[i]);
            }
            inParams.Add(orpmTelecomNumType);

            //add isprimary array as parameter
            OracleProcParam orpmIsPrimary = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_is_primary", isPrimary.Length);
            for (int i = 0; i < isPrimary.Length; i++)
            {
                orpmIsPrimary.AddValue(isPrimary[i]);
            }
            inParams.Add(orpmIsPrimary);

            //add email array as parameter
            OracleProcParam orpmEmail = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_email", email.Length);
            for (int i = 0; i < email.Length; i++)
            {
                orpmEmail.AddValue(email[i]);
            }
            inParams.Add(orpmEmail);

            //add email contact info id array as parameter
            OracleProcParam orpmEmailContactInfoId = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_email_contact_id", emailContactInfoId.Length);
            for (int i = 0; i < emailContactInfoId.Length; i++)
            {
                orpmEmailContactInfoId.AddValue(emailContactInfoId[i]);
            }
            inParams.Add(orpmEmailContactInfoId);

            //add email type array as parameter
            OracleProcParam orpmEmailType = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_email_contact_type", emailType.Length);
            for (int i = 0; i < emailType.Length; i++)
            {
                orpmEmailType.AddValue(emailType[i]);
            }
            inParams.Add(orpmEmailType);

            inParams.Add(new OracleProcParam("p_nocalls", nocalls));
            inParams.Add(new OracleProcParam("p_noemails", noemails));
            inParams.Add(new OracleProcParam("p_nofaxes", nofaxes));
            inParams.Add(new OracleProcParam("p_nomails", nomails));
            inParams.Add(new OracleProcParam("p_optout", optout));
            inParams.Add(new OracleProcParam("p_remind_pmt_due", remindpmtdue));
            inParams.Add(new OracleProcParam("p_pref_contact", prefcontact));
            inParams.Add(new OracleProcParam("p_pref_call_time", prefcalltime));
            inParams.Add(new OracleProcParam("p_how_did_you_hear_about_us", howdidyouhear));
            inParams.Add(new OracleProcParam("p_receive_promotions", receivepromotions));
            inParams.Add(new OracleProcParam("p_customer_number", customernumber));
            inParams.Add(new OracleProcParam("p_party_id", partyId));
            inParams.Add(new OracleProcParam("p_user_id", userid));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = ExecuteUpdateCustomerContactInfo(inParams, out custEmail, out custPhone, out errorCode, out errorMessage);
            }
            else
            {
                errorCode = "UpdateCustomerContactInfo";
                errorMessage = "No valid input parameters given";
            }
            return (rt);
        }

        /// <summary>
        /// Main Pawn method for invoking get_cust_details stored procedure
        /// </summary>
        /// <param name="firstName">First name of customer - Can be null or partial</param>
        /// <param name="lastName">Last name of customer - Can be null or partial</param>
        /// <param name="birthDate">String representation of customer's birthdate - Must be in MM/DD/YYYY format - Can be null</param>
        /// <param name="socialSecurityNumber">SSN of customer - Can be null</param>
        /// <param name="customerNumber">Customer number - Can be null</param>
        /// <param name="loanNumber">Customer loan number - Can be null</param>
        /// <param name="idType">Id type - If not null, must be a valid code</param>
        /// <param name="idNumber">Id number - Can be null</param>
        /// <param name="idIssuer">Id issuer - Can be null</param>
        /// <param name="phoneAreaCode">Customer phone area code - Can be null</param>
        /// <param name="phoneNumber">Customer phone number - Can be null</param>
        /// <param name="customer">Out param containing the customer row / rows</param>
        /// <param name="errorCode">Out param containing the error code (only if return is false)</param>
        /// <param name="errorMesg">Out param containing the error message (only if return is false)</param>
        /// <returns>Success of the operation.  If false, check errorCode, errorMsg</returns>
        public bool ExecuteLookupCustomer(
            string firstName,
            string lastName,
            string birthDate,
            string socialSecurityNumber,
            string customerNumber,
            string loanNumber,
            string idType,
            string idNumber,
            string idIssuer,
            string phoneAreaCode,
            string phoneNumber,
            string shopNumber,
            string shopState,
            out DataTable customer,
            out DataTable customerIdentities,
            out DataTable customerContacts,
            out DataTable customerAddresses,
            out DataTable customerEmails,
            out DataTable customerNotes,
            out DataTable customerStoreCredit,
            out string errorCode,
            out string errorMesg)
        {
            //Set default output params
            customer = null;
            customerIdentities = null;
            customerContacts = null;
            customerAddresses = null;
            customerEmails = null;
            customerNotes = null;
            customerStoreCredit = null;
            errorCode = string.Empty;
            errorMesg = string.Empty;

            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                customer = null;
                customerIdentities = null;
                customerContacts = null;
                customerAddresses = null;
                customerEmails = null;
                customerNotes = null;
                errorCode = "LookupCustomerFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add social security number
            inParams.Add(new OracleProcParam("p_ssn", socialSecurityNumber));

            //Add customer phone number
            inParams.Add(new OracleProcParam("p_cust_phone", phoneNumber));

            //Add last name
            inParams.Add(new OracleProcParam("p_cust_last_name", lastName));

            //Add first name
            inParams.Add(new OracleProcParam("p_cust_first_name", firstName));

            bool addedDate = false;
            //Add birth date - if it is null or cannot be parsed, add its null counterpart to the list
            if (!string.IsNullOrEmpty(birthDate))
            {
                bool useDate = true;
                DateTime birthDateDT;
                useDate = DateTime.TryParse(birthDate, out birthDateDT);
                if (useDate == true)
                {
                    inParams.Add(new OracleProcParam("p_cust_dob", birthDateDT));
                    addedDate = true;
                }
            }
            //If the date was not used and/or added, use DateTime.MaxValue to specify
            //that the date should be set as null (DateTime object cannot be forced to null)
            if (!addedDate)
            {
                inParams.Add(new OracleProcParam("p_cust_dob", DateTime.MaxValue));
            }

            //Add id type
            inParams.Add(new OracleProcParam("p_ident_type", idType));

            //Add id number
            inParams.Add(new OracleProcParam("p_issued_number", idNumber));

            //Add id issuer
            inParams.Add(new OracleProcParam("p_issuer_name", idIssuer));

            //Add customer number
            inParams.Add(new OracleProcParam("p_cust_number", customerNumber));

            //Add loan number
            inParams.Add(new OracleProcParam("p_loan_nbr", loanNumber));

            //Add phone area code
            inParams.Add(new OracleProcParam("p_phone_area_code", phoneAreaCode));

            //Add store number
            inParams.Add(new OracleProcParam("p_shop_number", shopNumber));

            //Add Store State
            inParams.Add(new OracleProcParam("p_storestatecode", shopState));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = internalExecuteLookupCustomer(inParams, out customer, out customerIdentities, out customerContacts, out customerAddresses, out customerEmails, out customerNotes, out customerStoreCredit, out errorCode, out errorMesg);
            }
            else
            {
                errorCode = "LookupCustomerFail";
                errorMesg = "No valid input parameters given";
            }
            return (rt);
        }

        public bool InsertPawnApplication(
            string customernumber,
            string storenumber,
            string clothing,
            string comments,
            string identtypecode,
            string idissuednumber,
            string idissuername,
            string idexpirydate,
            string userid,
            out string pawnAppId,
            out string errorCode,
            out string errorMessage)
        {
            //Set default output params
            errorCode = string.Empty;
            errorMessage = string.Empty;
            pawnAppId = string.Empty;

            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "InsertPawnApplication Failed";
                errorMessage = "Invalid desktop session or data accessor";
                pawnAppId = "";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_customer_number", customernumber));

            inParams.Add(new OracleProcParam("p_store_number", storenumber));

            inParams.Add(new OracleProcParam("p_clothing", clothing));

            inParams.Add(new OracleProcParam("p_comments", comments));

            inParams.Add(new OracleProcParam("p_ident_type_code", identtypecode));

            inParams.Add(new OracleProcParam("p_id_issued_number", idissuednumber));

            inParams.Add(new OracleProcParam("p_id_issuer_name", idissuername));

            inParams.Add(new OracleProcParam("p_id_expire_date", idexpirydate));

            inParams.Add(new OracleProcParam("p_user_id", userid));

            inParams.Add(new OracleProcParam("o_pawn_app_id", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = ExecuteInsertPawnApplication(inParams, out pawnAppId, out errorCode, out errorMessage);
            }
            else
            {
                errorCode = "InsertPawnApplication";
                errorMessage = "No valid input parameters given";
            }

            return (rt);
        }

        public bool updatePawnLostTicketFlag(
            List<string> storeNumber,
            List<int> ticketNumber,
            string userId,
            List<string> lsdFlag,
            out string errorCode,
            out string errorMsg)
        {
            errorCode = string.Empty;
            errorMsg = string.Empty;
            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "updatePawnLostTicketFlag Failed";
                errorMsg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();
            //Add store number array as param
            OracleProcParam orpmStoreNumber = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_store_number", storeNumber.Count);
            foreach (string s in storeNumber)
            {
                orpmStoreNumber.AddValue(s);
            }
            inParams.Add(orpmStoreNumber);

            //Add ticket number array as param
            OracleProcParam orpmTicketNumber = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTINT, "p_ticket_number", ticketNumber.Count);
            foreach (int i in ticketNumber)
            {
                orpmTicketNumber.AddValue(i);
            }
            inParams.Add(orpmTicketNumber);

            inParams.Add(new OracleProcParam("p_user_id", userId));

            //Add LSD flag array as param
            OracleProcParam orpmLSDFlag = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_lsdflag", lsdFlag.Count);
            foreach (string s in lsdFlag)
            {
                orpmLSDFlag.AddValue(s);
            }
            inParams.Add(orpmLSDFlag);

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = ExecuteUpdateLostPawnTicketFlag(inParams, out errorCode, out errorMsg);
            }
            else
            {
                errorCode = "updatePawnLostTicketFlag";
                errorMsg = "No valid input parameters given";
            }

            return (rt);
        }

        public bool getPicklistValues(
            out DataTable country,
            out DataTable eyeColor,
            out DataTable hairColor,
            out DataTable idTypeTable,
            out DataTable raceTable,
            out DataTable stateTable,
            out DataTable titleSuffixTable,
            out DataTable titleTable,
            out string errorCode,
            out string errorMesg)
        {
            errorCode = string.Empty;
            errorMesg = string.Empty;
            country = null;
            eyeColor = null;
            hairColor = null;
            idTypeTable = null;
            raceTable = null;
            stateTable = null;
            titleSuffixTable = null;
            titleTable = null;

            if (DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetPickListValues Failed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }
            bool rt = false;
            List<OracleProcParam> inParams = new List<OracleProcParam>();
            rt = ExecutePicklistDetails(inParams, out country,
                                                             out eyeColor, out hairColor, out idTypeTable,
                                                             out raceTable, out stateTable, out titleSuffixTable,
                                                             out titleTable, out errorCode, out errorMesg);
            return (rt);
        }

        /// <summary>
        /// Wrapper to the stored procedure that fetches all customer stats data 
        /// given a customer number
        /// </summary>
        /// <param name="oDa"></param>
        /// <param name="customerNumber"></param>
        /// <param name="customerStatsData"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public bool GetCustomerStatsData(
            OracleDataAccessor oDa,
            string customerNumber,
            out DataSet customerStatsData,
            out string errorCode,
            out string errorMesg)
        {
            //Set default output params
            customerStatsData = null;
            errorCode = string.Empty;
            errorMesg = string.Empty;

            if (oDa == null)
            {
                errorCode = "getcustomerStatsDataFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_customer_number", customerNumber));
            inParams.Add(new OracleProcParam("p_store_number", DesktopSession.CurrentSiteId.StoreNumber));

            inParams.Add(new OracleProcParam("o_store_credit", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));
            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("o_customer_stats", CUSTOMER_STATS));

                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_cust_procs",
                        "get_Customer_Stats", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling getcustomerStatsData stored procedure", oEx);
                    errorCode = "GetCustomerStats";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oDa.ErrorCode;
                    errorMesg = oDa.ErrorDescription;
                    return (false);
                }

                if (outputDataSet != null)
                {
                    errorCode = "0";
                    customerStatsData = outputDataSet;
                    errorMesg = string.Empty;
                    return (true);
                }
                errorCode = "getcustomerStatsDataFailedFailed";
                errorMesg = "No data returned for stats for the customer";
                return (false);
            }
            errorCode = "getcustomerStatsDataFailedFailed";
            errorMesg = "No valid input parameters given";
            return (false);
        }


        /// <summary>
        /// Get Customer CTR Data
        /// </summary>
        /// <param name="oDa"></param>
        /// <param name="customerNumber"></param>
        /// <param name="transactionDate"></param>
        /// <param name="ctrAmount"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>

        public bool GetCTRData(
            OracleDataAccessor oDa,
            string customerNumber,
            string transactionDate,
            out decimal ctrAmount,
            out string errorCode,
            out string errorMesg)
        {
            //Set default output params
            ctrAmount = 0;
            errorCode = string.Empty;
            errorMesg = string.Empty;

            if (oDa == null)
            {
                errorCode = "getcustomerCTRDataFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_customer_number", customerNumber));
            inParams.Add(new OracleProcParam("p_transaction_date", transactionDate));
            inParams.Add(new OracleProcParam("o_ctr_amount", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));


            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();


                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_cust_procs",
                        "get_ctr_for_customer", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling get_ctr_for_customer stored procedure", oEx);
                    errorCode = "GetCustomerCTR";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oDa.ErrorCode;
                    errorMesg = oDa.ErrorDescription;
                    return (false);
                }

            }
            errorCode = "get_ctr_for_customerFailed";
            errorMesg = "No valid input parameters given";
            return (false);
        }


        #endregion

        public static CustomerDBProcedures CreateInstance(DesktopSession desktopSession)
        {
            return new CustomerDBProcedures(desktopSession);
        }
    }
}
