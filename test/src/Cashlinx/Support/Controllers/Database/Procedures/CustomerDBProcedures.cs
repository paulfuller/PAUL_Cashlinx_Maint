/**************************************************************************************************************
* CashlinxDesktop.DesktopProcedures
* CustomerDBProcedures 
* DB Layer - Makes calls to the oracledata accessor
* Madhu Veldanda 4/3/2012 Initial version
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Security;
using System.Data;
using Support.Libraries.Objects.Customer;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Support.Controllers.Database.Procedures
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
        private static readonly string CUSTOMER_CHANGE_STATUS = "Customer_Change_status";
        private static readonly string PERSONAL_INFORMATION_HISTORY = "personal_information_history";
        private static readonly string SUPPORT_CUSTOMER_COMMENT= "Support_Customer_Comment";

        public DesktopSession DesktopSession { get; private set; }

        public CustomerDBProcedures(DesktopSession desktopSession)
        {
            DesktopSession = desktopSession;
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
                string marital_status,
                string spouse_first_name,
                string spouse_last_name,
                string spouse_ssn,
                string cust_sequence_number,
                string privacy_notification_date,
                string opt_out_flag,
                string status,
                string reason_code,
                string last_verification_date,
                string next_verification_date,
                string cooling_off_date_pdl,
                string customer_since_pdl,
                string spanish_form,
                string prbc,
                string planbankruptcy_protection,
                string years,
                string months,
                string own_home,
                string monthly_rent,
                string military_stationed_local,
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
                inParams.Add(new OracleProcParam("p_marital_status", marital_status));
                inParams.Add(new OracleProcParam("p_cust_sequence_number", cust_sequence_number));
                inParams.Add(new OracleProcParam("p_opt_out_flag", opt_out_flag));
                inParams.Add(new OracleProcParam("p_last_verification_date", last_verification_date));
                inParams.Add(new OracleProcParam("p_next_verification_date", next_verification_date));
                inParams.Add(new OracleProcParam("p_years", years));
                inParams.Add(new OracleProcParam("p_months", months));
                inParams.Add(new OracleProcParam("p_own_home", own_home));
                inParams.Add(new OracleProcParam("p_military_stationed_local", military_stationed_local));



                    /*inParams.Add(new OracleProcParam("", spouse_first_name));
            inParams.Add(new OracleProcParam("", spouse_last_name));
            inParams.Add(new OracleProcParam("", spouse_ssn));
            
            inParams.Add(new OracleProcParam("", privacy_notification_date));
            inParams.Add(new OracleProcParam("", status));
            inParams.Add(new OracleProcParam("", reason_code));
            inParams.Add(new OracleProcParam("", cooling_off_date_pdl));
            inParams.Add(new OracleProcParam("", customer_since_pdl));
            inParams.Add(new OracleProcParam("", spanish_form));
            inParams.Add(new OracleProcParam("", prbc));
            inParams.Add(new OracleProcParam("", planbankruptcy_protection));
            inParams.Add(new OracleProcParam("", monthly_rent));
            */

                bool rt = false;
                if (inParams.Count > 0)
                {
                    rt = ExecuteUpdatePersonalInfoDetails(inParams, out errorCode, out errorMessage);
                }
                return (rt);
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
                        "ccsowner", "pawn_support_cust_procs",
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
        /// <summary>
        /// method for invoking get_cust_details stored procedure from support application
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
        public bool ExecuteLSupporApptookupCustomer(
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
            string customerCity,
            string customerState,
            string bankAcctNbr,
            string searchType,
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

            //Add search type
            inParams.Add(new OracleProcParam("p_search_type", searchType));

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

            //Add Customer City
            inParams.Add(new OracleProcParam("p_customer_city", customerCity));

            //Add Customer State
            inParams.Add(new OracleProcParam("p_customer_state", customerState));

            //Add Customer bank account number
            inParams.Add(new OracleProcParam("p_bank_acct_nbr", bankAcctNbr));

            bool rt = false;
            if (inParams.Count > 0)
            {
                rt = internalExecuteSupportAppLookupCustomer(inParams, out customer, out customerIdentities, out customerContacts, out customerAddresses, out customerEmails, out customerNotes, out customerStoreCredit, out errorCode, out errorMesg);
            }
            else
            {
                errorCode = "LookupCustomerFail";
                errorMesg = "No valid input parameters given";
            }
            return (rt);
        }
#region REMOVE
        // WCM 4/11/12 Written and not tested and not in use
                /*__________________________________________________________________________________________*/
        public bool GetPersonalInformationHistoryDBData(
            OracleDataAccessor oDa,
            string customerNumber, 
            out DataSet personalInformationData,
            out string errorCode,
            out string errorMesg )
             {

                personalInformationData = null;
                errorCode = string.Empty;
                errorMesg = string.Empty;
                if (oDa == null)
                    {
                        errorCode = "personalInformationDataFailed";
                        errorMesg = "Invalid desktop session or data accessor";
                        return (false);
                    }

            //    Boolean StatusChanged, 
            //string CurrentStatus, 
            //Boolean ReasonChanged, 
            //string CurrentReason,

                List<OracleProcParam> inParams = new List<OracleProcParam>();
                inParams.Add(new OracleProcParam("p_customer_number", customerNumber));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("o_personal_information_history", PERSONAL_INFORMATION_HISTORY));

                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_support_cust_procs",
                        "get_Customer_Stats", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling GetCustomerStatusDBData stored procedure", oEx);
                    errorCode = "personalInformationData";
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
                    personalInformationData = outputDataSet;
                    errorMesg = string.Empty;
                    return (true);
                }
                errorCode = "personalInformationDataFailedFailed";
                errorMesg = "No data returned for stats for the customer";
                return (false);
            }
            errorCode = "GetCustomerStatusDBDataFailedFailed";
            errorMesg = "No valid input parameters given";
            return (false);
            }
#endregion
        /*__________________________________________________________________________________________*/
        public bool GetChangeCustomerStatusDBData( 
            string customerNumber, 
            string StatusChanged, 
            string CurrentStatus, 
            string ReasonChanged, 
            string CurrentReason,
            out string errorCode,
            out string errorMessage) //        out string errorCode,
            {  // uncomment the code below and comment the DbReturnBool and errorMesg WCM 

                OracleDataAccessor oDa = GlobalDataAccessor.Instance.OracleDA;

                //customerStatusData = null;
                errorCode = string.Empty;
                errorMessage = string.Empty;

                //if (DesktopSession == null ||
                //    GlobalDataAccessor.Instance.OracleDA == null)
                //{
                //    //customer = null;
                //    //errorCode = "getcustomerStatsDataFailed";
                //    errorMesg = "Invalid desktop session or data accessor";
                //    return (false);
                //}

                List<OracleProcParam> inParams = new List<OracleProcParam>();
                inParams.Add(new OracleProcParam("p_customer_number", customerNumber));
                inParams.Add(new OracleProcParam("p_status_changed", StatusChanged));
                inParams.Add(new OracleProcParam("p_current_status", CurrentStatus));
                inParams.Add(new OracleProcParam("p_reason_changed", ReasonChanged));
                inParams.Add(new OracleProcParam("p_current_reason", CurrentReason));
                bool retVal = false;
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                //refCursArr.Add(new PairType<string, string>("o_customer_change_stats", CUSTOMER_CHANGE_STATUS));

                DataSet outputDataSet;

                try
                {
                    DesktopSession.beginTransactionBlock();

                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner",
                        "pawn_support_cust_procs",
                        "update_customer_status",
                        inParams,
                        refCursArr,
                        "o_return_code",
                        "o_return_text", out outputDataSet);

                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling GetCustomerStatusDBData stored procedure", oEx);
                    errorCode = "GetCustomerChangeStatus";
                    errorMessage = "Exception: " + oEx.Message;
                    //return (false);
                }

                if (retVal == false)
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return (false);
                }
                else
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                    return (true);
                }
            }

        /*__________________________________________________________________________________________*/
        public bool ReadSupportCustomerCommentToDBData(
            string customerNumber,
            SupportCommentVO CommentRecord,
            out string errorCode,
            out string errorMessage) 
        {  
            OracleDataAccessor oDa = GlobalDataAccessor.Instance.OracleDA;

            errorCode = string.Empty;
            errorMessage = string.Empty;

            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_customer_number", OracleDbType.Varchar2,customerNumber));
            inParams.Add(new OracleProcParam("p_comments", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 4000)); // CommentRecord.CommentNote));
            inParams.Add(new OracleProcParam("p_updatedby", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 40));//CommentRecord.UpDatedBy));
            inParams.Add(new OracleProcParam("p_lastupdatedate", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 10));// CommentRecord.LastUpDateDATE));

            bool retVal = false;
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

            DataSet outputDataSet;

            try
            {
                
                retVal = oDa.issueSqlStoredProcCommand(
                    "ccsowner",
                    "pawn_support_cust_procs",
                    "get_customer_comments",
                    inParams,
                    refCursArr,
                    "o_return_code",
                    "o_return_text",
                    out outputDataSet);

                retVal = true;

            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling GetCustomerStatusDBData stored procedure", oEx);
                errorCode = "GetCustomerChangeStatus";
                errorMessage = "Exception: " + oEx.Message;
                return (retVal);
            }

            if (outputDataSet != null)
            {
                if (outputDataSet != null && outputDataSet.Tables.Count > 0)
                {
                    for (int i = 0; i < outputDataSet.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = outputDataSet.Tables[0].Rows[i];
                        switch (i)
                        {
                            case 0:
                                if (!(dr[1] == "null"))
                                    CommentRecord.CommentNote = Utilities.GetStringValue(dr[1], "");
                                break;
                            case 1:
                                if (!(dr[1] == "null"))
                                    CommentRecord.UpDatedBy = Utilities.GetStringValue(dr[1], "");
                                break;
                            case 2:
                                if (!(dr[1] == "null"))
                                    CommentRecord.LastUpDateDATE = DateTime.Parse(Utilities.GetStringValue(dr[1], ""));
                                /*
                                DateTime testDate;
                                bool DateIsGood = DateTime.TryParse(Utilities.GetStringValue(dr[1], ""),out testDate);
                                if (DateIsGood == true)
                                    CommentRecord.LastUpDateDATE = DateTime.Parse(Utilities.GetStringValue(dr[1], ""));
                                else
                                    CommentRecord.LastUpDateDATE = DateTime.Now;
                                */

                                    break;
                        }
                    }
                }
            }
                errorCode = "0"; 
                errorMessage = string.Empty; 
            return ( retVal );

        }    

        /*__________________________________________________________________________________________*/
        public Boolean WriteSupportCustomerCommentToDBData(
            string customerNumber,
            string commentText,
            string userId,
            out string errorCode,
            out string errorMessage) 
        {
            Boolean retVal = false;

            OracleDataAccessor oDa = GlobalDataAccessor.Instance.OracleDA;

            errorCode = string.Empty;
            errorMessage = string.Empty;

            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_customer_number", OracleDbType.Varchar2,customerNumber));
            inParams.Add(new OracleProcParam("p_comments",  OracleDbType.Varchar2,commentText));
            inParams.Add(new OracleProcParam("p_updatedby",  OracleDbType.Varchar2,userId));
          
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

            DataSet outputDataSet;
                try
                {
                    DesktopSession.beginTransactionBlock();

                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner",
                        "pawn_support_cust_procs",
                        "update_customer_comments",
                        inParams,
                        refCursArr,
                        "o_return_code",
                        "o_return_text", out outputDataSet);

                    retVal = true;
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling Update Customer Comments stored procedure", oEx);
                    errorCode = "UpdateCustomerComment";
                    errorMessage = "Exception: " + oEx.Message;
               }

                if (retVal == false)
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                }
                else
                {
                    DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                }

            return (retVal );
        }

        #region Private Methods
        
     /*__________________________________________________________________________________________*/
     private bool internalExecuteSupportAppLookupCustomer(
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
                    "ccsowner", "pawn_support_cust_procs",
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

        # endregion

        public static CustomerDBProcedures CreateInstance(DesktopSession desktopSession)
        {
            return new CustomerDBProcedures(desktopSession);
        }
    }
}
