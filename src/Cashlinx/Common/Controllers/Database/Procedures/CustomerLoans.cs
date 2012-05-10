/******************************************************************************************************************
* Namespace:       CashlinxDesktop.DesktopProcedures
* Class:           CustomerLoans
* 
* Description      The class retrieves Customer Loan information as well
*                  as parses the results back into associated Class 
*                  objects.
* 
* History
* David D Wise, 7-1-2009, Initial Development
* 
*  No ticket SMurphy 3/31/2010 - Location_Aisle was being populated with LOC_SHELF field
* SR 4/6/2010 Changed parsedataset to put status date coming back from getloankeys SP call in the status date
* property of the pawn loan object 
*  no ticket SMurphy 4/19/2010 Proknow flag for readonly on DescribeItem was never displayed
*  SR 6/13/2010 Changed to show type description in the receipts list
*  SR 05/03/2011 Added wrapper call for get_pawn_loan_lite
*  BZ# 1289 10/10/2011 - EWaltmon - Show Time Stamp 
* ***************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Security;
using Common.Libraries.Forms;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database.Procedures
{
    public static class CustomerLoans
    {
        public static readonly string PAWN = "m";
        public static readonly string PAWN_APP = "o_pawn_app" + "_" + PAWN;
        public static readonly string PAWN_FEE = "o_pawn_fees" + "_" + PAWN;
        public static readonly string PAWN_LOAN = "o_pawn_loan" + "_" + PAWN;
        public static readonly string PAWN_MDSE_LIST = "o_pawn_mdselist" + "_" + PAWN;
        public static readonly string PAWN_MDHIST_LIST = "o_pawn_mdhistlist" + "_" + PAWN;
        public static readonly string PAWN_GUN_LIST = "o_pawn_gunlist" + "_" + PAWN;
        public static readonly string PAWN_OTHERDSC_LIST = "o_pawn_otherdsclist" + "_" + PAWN;
        //       public static readonly string PAWN_RECEIPT_LIST = "o_pawn_receiptlist" + "_" + PAWN;
        public static readonly string PAWN_RECEIPTDET_LIST = "o_pawn_receiptdetlist" + "_" + PAWN;
        public static readonly string PAWN_PAYMENTDETAILS_LIST = "o_pawn_pawnpaymentlist" + "_" + PAWN;
        public static readonly string PAWN_STATS = "o_pawn_stats" + "_" + PAWN;
        public static readonly string PAWN_KEYS = "o_loan_keys_" + PAWN;
        public static readonly string FINANCES_KEYS = "o_fin_charges" + PAWN;
        public static readonly string OFF_CONSTANT = "OFF-";

        /// <summary>
        /// Provides back stored Loan Key data into a DataSet.
        /// </summary>
        /// <param name="customerNumber"></param>
        /// <param name="serviceDate"></param>
        /// <param name="loanKeys"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <param name="financeCharge"></param>
        /// <returns></returns>
        public static bool GetLoanKeys(
            string customerNumber,
            decimal financeCharge,
            DateTime serviceDate,
            out List<PawnLoan> loanKeys,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            loanKeys = new List<PawnLoan>();

            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetLoanKeys";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetLoanKeysFailed", new ApplicationException("Cannot execute the Get Loan Keys stored procedure"));
                return (false);
            }

            //Get data accessor object
            var dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            var inParams = new List<OracleProcParam>();

            //Add cat pointer
            inParams.Add(new OracleProcParam("p_cust_number", customerNumber));
            inParams.Add(new OracleProcParam("p_finance_chg", financeCharge));
            inParams.Add(new OracleProcParam("p_pick_up_date", serviceDate));
            // string currentdate = ShopDateTime.Instance.ShopDate.ToShortDateString() + " " +
            //         ShopDateTime.Instance.ShopTime.ToString();
            DateTime currentDateTime = Utilities.GetDateTimeValue(ShopDateTime.Instance.ShopTransactionTime, DateTime.Now);
            inParams.Add(new OracleProcParam("p_current_date_time", currentDateTime.FormatDateWithTimeZone()));

            //Setup ref cursor array
            var refCursors = new List<PairType<string, string>>
            {
                new PairType<string, string>("o_loan_keys", PAWN_KEYS),
                new PairType<string, string>("o_fin_charges", FINANCES_KEYS)
            };

            //Create output data set names
            bool retVal = false;
            try
            {
                // EDW - CR#11333
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_store_procs", "Get_Loan_Keys",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "GetLoanKeys";
                errorText = "Invocation of GetLoanKeys stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking Get_Loan_Keys stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }

            if (outputDataSet != null && outputDataSet.IsInitialized)
            {
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    int iRowIdx = 0;
                    DataTable dT = outputDataSet.Tables[PAWN_KEYS];
                    DataTable dTF = outputDataSet.Tables[FINANCES_KEYS];

                    foreach (DataRow curDr in dT.Rows)
                    {
                        PawnLoan pVO = new PawnLoan();
                        pVO.Amount = Utilities.GetDecimalValue(curDr["loan_amount"]);
                        string loanStatus = Utilities.GetStringValue(curDr["loan_status"]);
                        pVO.OriginationDate = Utilities.GetDateTimeValue(curDr["org_date"]);
                        pVO.OrgShopNumber = Utilities.GetStringValue(curDr["store_number"]);
                        pVO.TicketNumber = Utilities.GetIntegerValue(curDr["ticket_number"]);
                        //pVO.OrigTicketNumber = Utilities.GetIntegerValue(curDr["org_ticket"]);
                        pVO.DueDate = Utilities.GetDateTimeValue(curDr["due_date"]);
                        pVO.OrgShopState = Utilities.GetStringValue(curDr["store_state"]);
                        pVO.OrgShopAlias = Utilities.GetStringValue(curDr["store_alias"]);
                        pVO.PfiEligible = Utilities.GetDateTimeValue(curDr["pfi_elig"]);
                        pVO.StatusDate = Utilities.GetDateTimeValue(curDr["status_date"]);
                        pVO.IsExtended = Utilities.GetStringValue(curDr["EXTEND_FLAG"], "") == "Y" ? true : false;
                        pVO.PartialPaymentPaid = Utilities.GetStringValue(curDr["parpmt_flag"], "") == "Y" ? true : false;
                        pVO.LastPartialPaymentDate = pVO.PartialPaymentPaid ? Utilities.GetDateTimeValue(curDr["v_last_partial_payment"]) : DateTime.MaxValue;
                        pVO.StatusTime = Utilities.GetDateTimeValue(curDr["status_date"]);
                        //Set the customer number passed in to the Stored proc
                        pVO.CustomerNumber = customerNumber;
                        pVO.ExtensionAmount = Utilities.GetDecimalValue(curDr["extension_charge"]);
                        pVO.LoanStatus = (ProductStatus)Enum.Parse(typeof(ProductStatus), Utilities.GetStringValue(loanStatus, ""));
                        pVO.TotalExtensionAmount = Utilities.GetDecimalValue(curDr["EXTENSION_CHARGE"], 0);
                        pVO.DocType = Utilities.GetIntegerValue(curDr["DOC_TYPE"]);
                        if (dTF != null && dTF.Rows.Count > 0)
                        {
                            DataRow[] financeRows = dTF.Select("loan_number='" + pVO.TicketNumber.ToString() + "'");
                            if (financeRows.Length > 0)
                            {
                                pVO.PickupLateFinAmount = Utilities.GetDecimalValue(financeRows[0]["late_pu_fin"], 0);
                                pVO.PickupLateServAmount = Utilities.GetDecimalValue(financeRows[0]["late_pu_serv"], 0);
                                pVO.OtherTranLateFinAmount = Utilities.GetDecimalValue(financeRows[0]["late_fin"], 0);
                                pVO.OtherTranLateServAmount = Utilities.GetDecimalValue(financeRows[0]["late_serv"], 0);
                                pVO.LateRefPick = Utilities.GetDecimalValue(financeRows[0]["late_ref_pick_date"], 0);
                                pVO.ExtensionAmount = Utilities.GetDecimalValue(financeRows[0]["refund"], 0);
                            }
                        }
                        pVO.PickupLateRef = pVO.PickupLateFinAmount + pVO.PickupLateServAmount;


                        //Add to outbound array
                        loanKeys.Add(pVO);
                        iRowIdx++;
                    }
                }

                errorCode = "0";
                errorText = string.Empty;

                return (true);
            }

            errorCode = "GetLoanKeysFailed";
            errorText = "Operation failed";
            return (false);
        }

        /// <summary>
        /// Get all the pawn loans based on the id number passed
        /// </summary>
        /// <param name="storeNumber"></param>
        /// <param name="idNumber"></param>
        /// <param name="idType"></param>
        /// <param name="pawnApp"></param>
        /// <param name="pawnLoan"></param>
        /// <param name="pawnMDSEList"></param>
        /// <param name="pawnGunList"></param>
        /// <param name="pawnMDHistList"></param>
        /// <param name="pawnOtherDSCList"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool GetPawnLoan(
            DesktopSession desktopSession,
            Int32 storeNumber,
            Int32 idNumber,
            string idType,
            StateStatus TempStatus,
            bool bGetCustomerInfo,
            out PawnLoan pawnLoan,
            out PawnAppVO pawnApplication,
            out CustomerVO customerVO,
            out string errorCode,
            out string errorText)
        {
            //Set default output params
            pawnApplication = null;
            customerVO = null;
            pawnLoan = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetPawnLoanFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add store number
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_id_number", idNumber));
            inParams.Add(new OracleProcParam("p_id_type", idType));

            //Add temp type 
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
            refCursArr.Add(new PairType<string, string>("o_pawn_fees", PAWN_FEE));
            refCursArr.Add(new PairType<string, string>("o_pawn_paymentdetails", PAWN_PAYMENTDETAILS_LIST));

            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_store_procs",
                    "get_pawn_loan", inParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling get_pawn_loan stored procedure", oEx);
                errorCode = " -- getpawnloan failed";
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
                        List<PawnLoan> pawnLoans;
                        List<PawnAppVO> pawnApps;
                        try
                        {
                            ParseDataSet(outputDataSet, out pawnLoans, out pawnApps);
                            if (CollectionUtilities.isEmpty(pawnLoans) ||
                                CollectionUtilities.isEmpty(pawnApps))
                            {
                                errorCode = "Parsing the data from the stored procedure failed";
                                errorText = "Pawn Loans or the PawnApplications object is null";
                                return false;
                            }
                            pawnLoan = pawnLoans.First();
                            pawnApplication = pawnApps.First();

                            if (bGetCustomerInfo)
                                customerVO = CustomerProcedures.getCustomerDataByCustomerNumber(desktopSession, pawnLoan.CustomerNumber);

                            return (true);
                        }
                        catch (Exception ex)
                        {
                            errorCode = "Parsing the data from the stored procedure failed";
                            errorText = ex.Message;
                            return false;
                        }
                    }
                }
            }

            errorCode = "GETPAWNLOANFAIL";
            errorText = "Operation failed";
            return (false);
        }


        public static bool GetPawnLoanLite(
            DesktopSession desktopSession,
            Int32 storeNumber,
            Int32 idNumber,
            string idType,
            StateStatus TempStatus,
            bool bGetCustomerInfo,
            out PawnLoan pawnLoan,
            out CustomerVO customerVO,
            out string errorCode,
            out string errorText)
        {
            //Set default output params
            customerVO = null;
            pawnLoan = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetPawnLoanLiteFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add store number
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_id_number", idNumber));
            inParams.Add(new OracleProcParam("p_id_type", idType));

            //Add temp type 
            inParams.Add(TempStatus != StateStatus.BLNK
                                 ? new OracleProcParam("p_temp_type", TempStatus.ToString())
                                 : new OracleProcParam("p_temp_type", "0"));

            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("o_pawn_loan", PAWN_LOAN));
            refCursArr.Add(new PairType<string, string>("o_pawn_mdselist", PAWN_MDSE_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_mdhistlist", PAWN_MDHIST_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_otherdsclist", PAWN_OTHERDSC_LIST));

            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_store_procs",
                    "get_pawn_loan_lite", inParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling get_pawn_loan_lite stored procedure", oEx);
                errorCode = " -- getpawnloanlite failed";
                errorText = " --- Exception: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            if (outputDataSet != null && outputDataSet.IsInitialized)
            {
                if (outputDataSet.Tables.Count > 0)
                {
                    List<PawnLoan> pawnLoans;
                    try
                    {
                        List<PawnAppVO> pawnApps;
                        ParseDataSet(outputDataSet, out pawnLoans, out pawnApps);
                        if (CollectionUtilities.isEmpty(pawnLoans))
                        {
                            errorCode = "Parsing the data from the stored procedure failed";
                            errorText = "Pawn Loans object is null";
                            return false;
                        }
                        pawnLoan = pawnLoans.First();

                        if (bGetCustomerInfo)
                            customerVO = CustomerProcedures.getCustomerDataByCustomerNumber(desktopSession, pawnLoan.CustomerNumber);

                        return (true);
                    }
                    catch (Exception ex)
                    {
                        errorCode = "Parsing the data from the stored procedure failed";
                        errorText = ex.Message;
                        return false;
                    }
                }
            }

            errorCode = "GETPAWNLOANLITEFAIL";
            errorText = "Operation failed";
            return (false);
        }

        /// <summary>
        /// Provides back stored Pawn Loan data into a DataSet.  If dates are not needed for search, pass null.
        /// </summary>
        /// <param name="customerNumber"></param>
        /// <param name="status"></param>
        /// <param name="TempStatus"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <param name="outputDataSet"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool GetCustomerLoans(
            string customerNumber,
            ProductStatus status,
            StateStatus TempStatus,
            string fromdate,
            string todate,
            out DataSet outputDataSet,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;

            outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetCustomerLoans";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetCustomerLoansFailed", new ApplicationException("Cannot execute the Get Customer Loans stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add cust number
            inParams.Add(new OracleProcParam("p_cust_number", customerNumber));
            //Pass loan status. If status needed is ALL then pass empty string
            if (status == ProductStatus.ALL)
                inParams.Add(new OracleProcParam("p_status", null));
            else
                inParams.Add(new OracleProcParam("p_status", status.ToString()));
            if (fromdate != null)
                inParams.Add(new OracleProcParam("p_fromdate", Convert.ToDateTime(fromdate)));
            else
                inParams.Add(new OracleProcParam("p_fromdate", null));

            if (todate != null)
                inParams.Add(new OracleProcParam("p_todate", Convert.ToDateTime(todate)));
            else
                inParams.Add(new OracleProcParam("p_todate", null));

            if (TempStatus != StateStatus.BLNK)
                inParams.Add(new OracleProcParam("p_temp_type", TempStatus.ToString()));
            else
                inParams.Add(new OracleProcParam("p_temp_type", null));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_pawn_app", PAWN_APP));
            refCursors.Add(new PairType<string, string>("o_pawn_loan", PAWN_LOAN));
            refCursors.Add(new PairType<string, string>("o_pawn_mdselist", PAWN_MDSE_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_gunlist", PAWN_GUN_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_mdhistlist", PAWN_MDHIST_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_otherdsclist", PAWN_OTHERDSC_LIST));
            //            refCursors.Add(new PairType<string, string>("o_pawn_receiptlist", PAWN_RECEIPT_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_receiptdetlist", PAWN_RECEIPTDET_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_fees", PAWN_FEE));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_store_procs", "Get_Customer_Loans",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = " -- GetCustomerLoans";
                errorText = " -- Invocation of GetCustomerLoans stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking Get_Customer_Loans stored proc", oEx);
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
                errorCode = "0";
                errorText = string.Empty;
                return (true);
            }

            errorCode = "GetCustomerLoansFailed";
            errorText = "Operation failed";
            return (false);
        }

        /// <summary>
        /// Provides back stored Pawn Loan stats into a DataSet.  If dates are not needed for search, pass null.
        /// </summary>
        /// <param name="customerNumber"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <param name="lstPawnLoans"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool GetLoanStats(
            string customerNumber,
            string fromdate,
            string todate,
            out DataSet outputDataSet,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;

            outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetLoanStats";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetLoanStatsFailed", new ApplicationException("Cannot execute the Get Loan Stats stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add cat pointer
            inParams.Add(new OracleProcParam("p_cust_number", customerNumber));
            if (fromdate != null)
                inParams.Add(new OracleProcParam("p_fromdate", Convert.ToDateTime(fromdate)));
            else
                inParams.Add(new OracleProcParam("p_fromdate", null));

            if (todate != null)
                inParams.Add(new OracleProcParam("p_todate", Convert.ToDateTime(todate)));
            else
                inParams.Add(new OracleProcParam("p_todate", null));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_pawn_stats", PAWN_APP));
            refCursors.Add(new PairType<string, string>("o_pawn_fees_store", PAWN_FEE));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_store_procs", "Get_Loan_Stats",
                    inParams, refCursors, "o_error_code", "o_error_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = " -- GetLoanStats";
                errorText = " -- Invocation of GetLoanStats stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking Get_Loan_Stats stored proc", oEx);
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
                errorCode = "0";
                errorText = string.Empty;
                return (true);
            }

            errorCode = "GetLoanStatsFailed";
            errorText = "Operation failed";
            return (false);
        }

        /// <summary>
        /// Create a receipt based on a data table
        /// </summary>
        /// <param name="dT"></param>
        /// <returns></returns>
        public static List<Receipt> CreateReceipt(DataTable dT)
        {
            List<Receipt> rt = null;
            if (dT != null)
            {
                rt = new List<Receipt>();
                for (int i = 0; i < dT.Rows.Count; i++)
                {
                    Receipt myReceipt = new Receipt()
                    {
                        Amount = Utilities.GetDecimalValue(dT.Rows[i]["REF_AMT"]),
                        AuxillaryDate = Utilities.GetDateTimeValue(dT.Rows[i]["REF_DATE"]),
                        Date = Utilities.GetDateTimeValue(dT.Rows[i]["RECEIPT_DATE"]),
                        EntID = Utilities.GetStringValue(dT.Rows[i]["ENT_ID"]),
                        Event = Utilities.GetStringValue(dT.Rows[i]["REF_EVENT"]),
                        ReceiptNumber = Utilities.GetStringValue(dT.Rows[i]["RECEIPT_NUMBER"]),
                        RefStoreNumber = Utilities.GetStringValue(dT.Rows[i]["REF_STORE"]),
                        StoreNumber = Utilities.GetStringValue(dT.Rows[i]["STORENUMBER"]),
                        RefNumber = Utilities.GetStringValue(dT.Rows[i]["REF_NUMBER"]),
                        Type = Utilities.GetStringValue(dT.Rows[i]["REF_TYPE"]),
                        TypeDescription = Utilities.GetStringValue(dT.Rows[i]["REF_DESC"]),
                        RefTime = dT.Columns.Contains("REF_TIME") ? Utilities.GetDateTimeValue(dT.Rows[i]["REF_TIME"]) : DateTime.MaxValue
                    };
                    rt.Add(myReceipt);
                }
            }
            return (rt);
        }

        /// <summary>
        /// Create a receipt based on a passed in loan and a data table
        /// </summary>
        /// <param name="pLoan"></param>
        /// <param name="pawnObj"></param>
        /// <param name="dT"></param>
        public static List<Receipt> CreateReceipt(CustomerProductDataVO pawnObj, DataTable dT)
        {
            if (dT != null)
            {
                if (pawnObj.Receipts == null)
                    pawnObj.Receipts = new List<Receipt>();


                DataRow[] dataRows;

                if (pawnObj is Common.Libraries.Objects.Retail.SaleVO && ((Common.Libraries.Objects.Retail.SaleVO)pawnObj).RefType == "4")
                    dataRows = dT.Select("REF_NUMBER=" + ((Common.Libraries.Objects.Retail.SaleVO)pawnObj).RefNumber);
                else
                    dataRows = dT.Select("REF_NUMBER=" + pawnObj.TicketNumber);

                if (dataRows != null)
                {
                    bool createdByColumnExists = dT.Columns.Contains("CREATEDBY");
                    bool extRefIdColumnExists = dT.Columns.Contains("EXT_REF_ID");
                    foreach (DataRow dataRow in dataRows)
                    {
                        //if (pawnObj.TicketNumber == Utilities.GetIntegerValue(dataRow["REF_NUMBER"], 0))
                        //{
                        Receipt myReceipt = new Receipt()
                        {
                            Amount = Utilities.GetDecimalValue(dataRow["REF_AMT"]),
                            AuxillaryDate = dT.Columns.Contains("REF_TIME") ? Utilities.GetDateTimeValue(dataRow["REF_TIME"]) :
                                        Utilities.GetDateTimeValue(dataRow["REF_DATE"]),
                            Date = Utilities.GetDateTimeValue(dataRow["RECEIPT_DATE"]),
                            EntID = Utilities.GetStringValue(dataRow["ENT_ID"]),
                            Event = Utilities.GetStringValue(dataRow["REF_EVENT"]),
                            ReceiptNumber = Utilities.GetStringValue(dataRow["RECEIPT_NUMBER"]),
                            RefStoreNumber = Utilities.GetStringValue(dataRow["REF_STORE"]),
                            RefNumber = Utilities.GetStringValue(dataRow["REF_NUMBER"]),
                            StoreNumber = Utilities.GetStringValue(dataRow["STORENUMBER"]),
                            Type = Utilities.GetStringValue(dataRow["REF_TYPE"]),
                            TypeDescription = Utilities.GetStringValue(dataRow["REF_DESC"]),
                            ReceiptDetailNumber = Utilities.GetStringValue(dataRow["RECEIPTDETAIL_NUMBER"]),
                            RefTime = dT.Columns.Contains("REF_TIME") ? Utilities.GetDateTimeValue(dataRow["REF_TIME"]) :
                                        Utilities.GetDateTimeValue(dataRow["REF_DATE"]),
                            ReferenceReceiptNumber = extRefIdColumnExists ? Utilities.GetStringValue(dataRow["EXT_REF_ID"]) : ""
                        };

                        if (createdByColumnExists)
                        {
                            myReceipt.CreatedBy = Utilities.GetStringValue(dataRow["CREATEDBY"]);
                        }

                        pawnObj.Receipts.Add(myReceipt);
                        //}
                    }
                }
            }
            return pawnObj.Receipts;
        }

        /// <summary>
        /// Pass in a returned DataSet from GetPawnLoan,GetCustomerLoans or 
        /// GetLoanKeys to return a List of PawnLoans and PawnAppVO
        /// </summary>
        /// <param name="outputDataSet">GetCustomerLoans DataSet</param>
        /// <param name="lstPawnLoans">List of PawnLoans</param>
        /// <param name="lstPawnApp"></param>
        public static void ParseDataSet(DataSet outputDataSet, out List<PawnLoan> lstPawnLoans, out List<PawnAppVO> lstPawnApp)
        {
            string sDATA = "";

            if (outputDataSet.Tables[PAWN_OTHERDSC_LIST] != null &&
                outputDataSet.Tables[PAWN_OTHERDSC_LIST].Rows.Count > 0)
            {
                foreach (DataColumn myColumn in outputDataSet.Tables[PAWN_OTHERDSC_LIST].Columns)
                {
                    sDATA += myColumn.ColumnName + ",";
                }
                sDATA = sDATA.Substring(0, sDATA.Length - 1);
                sDATA += Environment.NewLine;
                foreach (DataRow myRow in outputDataSet.Tables[PAWN_OTHERDSC_LIST].Rows)
                {
                    foreach (DataColumn myColumn in outputDataSet.Tables[PAWN_OTHERDSC_LIST].Columns)
                    {
                        sDATA += myRow[myColumn.ColumnName] + ",";
                    }
                    sDATA = sDATA.Substring(0, sDATA.Length - 1);
                    sDATA += Environment.NewLine;
                }
            }

            lstPawnApp = new List<PawnAppVO>();
            //Get the Pawn App Data from the output and store in PawnAppVO object
            if (outputDataSet.Tables.Count > 0)
            {
                if (outputDataSet.Tables[PAWN_APP] != null)
                {
                    foreach (DataRow pawnappRow in outputDataSet.Tables[PAWN_APP].Rows)
                    {
                        PawnAppVO pawnapp = new PawnAppVO();
                        pawnapp.PawnAppID = Utilities.GetLongValue(pawnappRow[(int)pawnapprecord.PAWNAPPID], 0);
                        pawnapp.CustomerNumber = Utilities.GetStringValue(pawnappRow[(int)pawnapprecord.CUSTOMERNUMBER], "");
                        pawnapp.StoreNumber = Utilities.GetStringValue(pawnappRow[(int)pawnapprecord.STORENUMBER], "");
                        pawnapp.Clothing = Utilities.GetStringValue(pawnappRow[(int)pawnapprecord.CLOTHING], "");
                        pawnapp.Comments = Utilities.GetStringValue(pawnappRow[(int)pawnapprecord.COMMENTS], "");
                        pawnapp.PawnAppCustIDNumber = Utilities.GetStringValue(pawnappRow[(int)pawnapprecord.IDISSUEDNUMBER], "");
                        pawnapp.PawnAppCustIDIssueDate = Utilities.GetDateTimeValue(pawnappRow[(int)pawnapprecord.IDISSUEDDATE], DateTime.MaxValue);
                        pawnapp.PawnAppCustIDIssuer = Utilities.GetStringValue(pawnappRow[(int)pawnapprecord.IDISSUERNAME], "");
                        pawnapp.PawnAppCustIDType = Utilities.GetStringValue(pawnappRow[(int)pawnapprecord.IDTYPECODE], "");
                        pawnapp.PawnAppCustIDExpDate = Utilities.GetDateTimeValue(pawnappRow[(int)pawnapprecord.IDEXPIRATION], DateTime.MaxValue);
                        pawnapp.PawnAppStatus = Utilities.GetStringValue(pawnappRow[(int)pawnapprecord.APPSTATUS], "");
                        pawnapp.PawnAppCreatedDate = Utilities.GetDateTimeValue(pawnappRow[(int)pawnapprecord.CREATIONDATE], DateTime.MaxValue);
                        lstPawnApp.Add(pawnapp);
                    }
                }
            }

            lstPawnLoans = new List<PawnLoan>();

            if (outputDataSet.Tables.Count > 0)
            {
                if (outputDataSet.Tables[PAWN_LOAN] != null)
                {
                    // Iterate through the Pawn Loan records returned from Search
                    foreach (DataRow dataPawnLoanRow in outputDataSet.Tables[PAWN_LOAN].Rows)
                    {
                        PawnLoan storedPawnLoan = new PawnLoan();
                        storedPawnLoan.Fees = new List<Fee>();
                        storedPawnLoan.Amount = Utilities.GetDecimalValue(dataPawnLoanRow["PRIN_AMOUNT"], new decimal());
                        storedPawnLoan.CreatedBy = Utilities.GetStringValue(dataPawnLoanRow["CREATEDBY"], "");
                        storedPawnLoan.EntityId = Utilities.GetStringValue(dataPawnLoanRow["ENT_ID"], "");
                        storedPawnLoan.CustomerNumber = Utilities.GetStringValue(dataPawnLoanRow["CUSTOMERNUMBER"], "");
                        storedPawnLoan.DueDate = Utilities.GetDateTimeValue(dataPawnLoanRow["DATE_DUE"], DateTime.MinValue);
                        storedPawnLoan.HoldDesc = Utilities.GetStringValue(dataPawnLoanRow["HOLD_DESC"], "");
                        //storedPawnLoan.InterestAmount = Utilities.GetDecimalValue(dataPawnLoanRow["FIN_CHG"], 0.00M);
                        storedPawnLoan.InterestRate = Utilities.GetDecimalValue(dataPawnLoanRow["INT_PCT"], 0.00000M);
                        storedPawnLoan.IsExtended = Utilities.GetStringValue(dataPawnLoanRow["EXTEND_FLAG"], "") == "Y" ? true : false;
                        //                        storedPawnLoan.LastDayOfGrace = Utilities.GetDateTimeValue(dataPawnLoanRow["DATE_DUE"], DateTime.MinValue).Date.AddDays(1);
                        storedPawnLoan.LastDayOfGrace = Utilities.GetDateTimeValue(dataPawnLoanRow["PFI_ELIG"], DateTime.MinValue);
                        storedPawnLoan.LastUpdatedBy = Utilities.GetStringValue(dataPawnLoanRow["UPDATEDBY"], "");
                        storedPawnLoan.LoanStatus = (ProductStatus)Enum.Parse(typeof(ProductStatus), Utilities.GetStringValue(dataPawnLoanRow["STATUS_CD"], ""));
                        storedPawnLoan.NegotiableFinanceCharge = Utilities.GetCharValue(dataPawnLoanRow["FIN_NEG"], new char());
                        storedPawnLoan.NegotiableServiceCharge = Utilities.GetCharValue(dataPawnLoanRow["SERV_NEG"], new char());
                        storedPawnLoan.OrgShopNumber = Utilities.GetStringValue(dataPawnLoanRow["STORENUMBER"], "");
                        storedPawnLoan.OrgShopState = Utilities.GetStringValue(dataPawnLoanRow["STATE_CODE"], "");
                        storedPawnLoan.OrgAmount = Utilities.GetDecimalValue(dataPawnLoanRow["ORG_AMOUNT"], new decimal());
                        storedPawnLoan.OriginationDate = Utilities.GetDateTimeValue(dataPawnLoanRow["ORG_DATE"], DateTime.MinValue);
                        storedPawnLoan.OrigTicketNumber = Utilities.GetIntegerValue(dataPawnLoanRow["ORG_TICKET"], 0);
                        storedPawnLoan.PawnAppId = Utilities.GetStringValue(dataPawnLoanRow["PAWN_APP_ID"], "");
                        storedPawnLoan.PfiEligible = Utilities.GetDateTimeValue(dataPawnLoanRow["PFI_ELIG"], DateTime.MinValue);
                        storedPawnLoan.PfiNote = Utilities.GetDateTimeValue(dataPawnLoanRow["PFI_NOTE"], DateTime.MinValue);
                        storedPawnLoan.PrevTicketNumber = Utilities.GetIntegerValue(dataPawnLoanRow["PREV_TICKET"], 0);
                        storedPawnLoan.PrintNotice = Utilities.GetCharValue(dataPawnLoanRow["PRINT_NOTICE"], new char());
                        storedPawnLoan.PuCustNumber = Utilities.GetStringValue(dataPawnLoanRow["PU_CUST_NUM"], "");
                        storedPawnLoan.ServiceCharge = Utilities.GetDecimalValue(dataPawnLoanRow["SERV_CHG"], 0.00M);
                        storedPawnLoan.TicketNumber = Utilities.GetIntegerValue(dataPawnLoanRow["TICKET_NUMBER"], 0);
                        storedPawnLoan.StatusDate = Utilities.GetDateTimeValue(dataPawnLoanRow["STATUS_DATE"], DateTime.MinValue);
                        storedPawnLoan.TempStatus = (StateStatus)Enum.Parse(typeof(StateStatus),
                                                                            Utilities.GetStringValue(dataPawnLoanRow["TEMP_STATUS"]) != ""
                                                                            ? Utilities.GetStringValue(dataPawnLoanRow["TEMP_STATUS"])
                                                                            : StateStatus.BLNK.ToString());
                        storedPawnLoan.GunInvolved = Utilities.GetStringValue(dataPawnLoanRow["GUN_INVOLVED"], "") == "Y" ? true : false;

                        //Get time made timestamp
                        storedPawnLoan.DateMade = Utilities.GetDateTimeValue(dataPawnLoanRow["DATE_MADE"], DateTime.MaxValue);
                        storedPawnLoan.MadeTime = Utilities.GetTimestampValue(dataPawnLoanRow["TIME_MADE"]);
                        storedPawnLoan.UpdatedDate = Utilities.GetDateTimeValue(dataPawnLoanRow["LASTUPDATEDATE"]);

                        //Set the pickup amount to be principal + interest
                        storedPawnLoan.PickupAmount = storedPawnLoan.Amount + storedPawnLoan.InterestAmount;

                        //set the ref type
                        storedPawnLoan.ProductType = ProductType.PAWN.ToString();

                        //Set whether partial payments were made
                        storedPawnLoan.PartialPaymentPaid = Utilities.GetStringValue(dataPawnLoanRow["PARPMT_FLAG"], "") == "Y" ? true : false;

                        if (dataPawnLoanRow.Table.Columns.Contains("v_count_pfi_mailer"))
                        {
                            storedPawnLoan.PfiMailerSent = Utilities.GetIntegerValue(dataPawnLoanRow["v_count_pfi_mailer"], 0) > 0;
                        }
                        else
                        {
                            storedPawnLoan.PfiMailerSent = false;
                        }

                        //Create receipt from receipt details cursor
                        storedPawnLoan.Receipts = CreateReceipt(storedPawnLoan, outputDataSet.Tables[PAWN_RECEIPTDET_LIST]);

                        var rcptData = from rcpt in storedPawnLoan.Receipts
                                       where rcpt.Event == ReceiptEventTypes.Extend.ToString()
                                       && (rcpt.ReferenceReceiptNumber== null || string.IsNullOrEmpty(rcpt.ReferenceReceiptNumber))
                                       select rcpt;

                        var rcptVoidData = from rcpt in storedPawnLoan.Receipts
                                           where rcpt.Event == ReceiptEventTypes.VEX.ToString()
                                           && (rcpt.ReferenceReceiptNumber == null || string.IsNullOrEmpty(rcpt.ReferenceReceiptNumber))
                                           select rcpt;
                        storedPawnLoan.ExtensionAmount = rcptData.Sum(r => r.Amount)-rcptVoidData.Sum(r=>r.Amount);
                        storedPawnLoan.LastExtensionPaid = rcptData.OrderByDescending(r=>r.RefTime).FirstOrDefault().RefTime;


                        //Get all the fee data
                        Fee newFee;
                        if (outputDataSet.Tables[PAWN_FEE] != null)
                        {
                            foreach (DataRow dr in outputDataSet.Tables[PAWN_FEE].Rows)
                            {
                                newFee = new Fee
                                {
                                    FeeType = (FeeTypes)Enum.Parse(typeof(FeeTypes),
                                                                   Utilities.GetStringValue(dr["FEE_TYPE"])),
                                    Value = Utilities.GetDecimalValue(dr["FEE_AMOUNT"], 0.00M),
                                    OriginalAmount = Utilities.GetDecimalValue(dr["FEE_ORIGINAL_AMT"], 0.00M),
                                    FeeState = (FeeStates)Enum.Parse(typeof(FeeStates), Utilities.GetStringValue(dr["FEE_STATE_CODE"])),
                                    Prorated = Utilities.GetStringValue(dr["FEE_IS_PRORATED"]) == "1" ? true : false,
                                    FeeDate = Utilities.GetDateTimeValue(dr["FEE_DATE"]),
                                    FeeRefType = (FeeRefTypes)Enum.Parse(typeof(FeeRefTypes),
                                                                   Utilities.GetStringValue(dr["FEE_REF_TYPE"])),
                                    FeeRef = Utilities.GetIntegerValue(dr["fee_ref"])
                                    //FeeGroup=(FeeGroups)Enum.Parse(typeof(FeeGroups),Utilities.GetStringValue(dr["FEE_GROUP_CODE"]))
                                };
                                storedPawnLoan.Fees.Add(newFee);
                            }
                        }
                        var feeAmt = (from feeData in storedPawnLoan.Fees
                                      where feeData.FeeType == FeeTypes.INTEREST
                                      && feeData.FeeRefType == FeeRefTypes.PAWN
                                      select feeData.Value).FirstOrDefault();
                        storedPawnLoan.InterestAmount = feeAmt;


                        var srvcfeeAmt = (from feeData in storedPawnLoan.Fees
                                          where feeData.FeeType == FeeTypes.STORAGE
                                          && feeData.FeeRefType == FeeRefTypes.PAWN
                                          select feeData.Value).FirstOrDefault();
                        storedPawnLoan.ServiceCharge = srvcfeeAmt;
                        storedPawnLoan.LastPartialPaymentDate = DateTime.MaxValue;


                        //Get Partial Payment Details
                        var currentPrincipal = 0.0m;

                        PartialPayment pPayment;
                        if (outputDataSet.Tables[PAWN_PAYMENTDETAILS_LIST] != null)
                        {
                            foreach (DataRow dr in outputDataSet.Tables[PAWN_PAYMENTDETAILS_LIST].Rows)
                            {
                                pPayment = new PartialPayment
                                               {
                                                   PMT_ID = Utilities.GetIntegerValue(dr["pmt_id"]),
                                                   PMT_AMOUNT = Utilities.GetDecimalValue(dr["pmt_amount"]),
                                                   PMT_PRIN_AMT = Utilities.GetDecimalValue(dr["pmt_prin_amt"]),
                                                   PMT_INT_AMT = Utilities.GetDecimalValue(dr["pmt_int_amt"]),
                                                   PMT_SERV_AMT = Utilities.GetDecimalValue(dr["pmt_serv_amt"]),
                                                   CUR_AMOUNT = Utilities.GetDecimalValue(dr["cur_amount"]),
                                                   CUR_FIN_CHG = Utilities.GetDecimalValue(dr["cur_fin_chg"]),
                                                   Cur_Srv_Chg = Utilities.GetDecimalValue(dr["cur_serv_chg"]),
                                                   Cur_Term_Fin = Utilities.GetIntegerValue(dr["cur_term_fin"]),
                                                   Cur_Int_Pct = Utilities.GetDecimalValue(dr["cur_int_pct"]),
                                                   Status_cde = Utilities.GetStringValue(dr["status_cd"]),
                                                   Pmt_Type = Utilities.GetStringValue(dr["pmt_type"]),
                                                   ReceiptDetail_Number =
                                                       Utilities.GetIntegerValue(dr["receiptdetail_number"]),
                                                   Date_Made = Utilities.GetDateTimeValue(dr["date_made"]),
                                                   StoreNumber = Utilities.GetStringValue(dr["storenumber"]),
                                                   CreatedBy = Utilities.GetStringValue(dr["createdby"]),
                                                   UpdatedBy = Utilities.GetStringValue(dr["updatedby"]),
                                                   Time_Made = Utilities.GetDateTimeValue(dr["time_made"])

                             
                                               };
                                storedPawnLoan.PartialPayments.Add(pPayment);
                            }

                            var lastPartialPayment = storedPawnLoan.PartialPayments.OrderByDescending(pp => pp.Time_Made).FirstOrDefault();
                            currentPrincipal = Utilities.GetDecimalValue(lastPartialPayment.CUR_AMOUNT);
                            if (storedPawnLoan.PartialPayments.Count > 0)
                            {
                                storedPawnLoan.LastPartialPaymentDate = lastPartialPayment.Time_Made;
                                storedPawnLoan.ServiceCharge = lastPartialPayment.Cur_Srv_Chg;
                                storedPawnLoan.InterestAmount = lastPartialPayment.CUR_FIN_CHG;
                            }
                        }
                        else
                        {
                            currentPrincipal = Utilities.GetDecimalValue(dataPawnLoanRow["PRIN_AMOUNT"]);
                        }

                        storedPawnLoan.CurrentPrincipalAmount = currentPrincipal;





                        // Pull from Merchandise List Table
                        //if (storedPawnLoan.TicketNumber == 1168)
                        //{

                        //}
                        if (outputDataSet.Tables[PAWN_MDSE_LIST] != null)
                        {
                            #region nonJewelry
                            // Check for Non_Jewelry Items first
                            string sMDSEFilter = "STORENUMBER = '" + storedPawnLoan.OrgShopNumber + "' ";
                            //sMDSEFilter += " and ICN_STORE = " + Utilities.GetStringValue(dataPawnLoanRow["ICN_STORENUMBER"], "");
                            sMDSEFilter += " and ICN_STORE = " + storedPawnLoan.OrgShopNumber;
                            sMDSEFilter += " and ICN_DOC = '" + storedPawnLoan.OrigTicketNumber + "' ";
                            sMDSEFilter += " and ICN_DOC_TYPE = 1";
                            sMDSEFilter += " and ICN_SUB_ITEM = 0";

                            DataRow[] dataMsdeRows = outputDataSet.Tables[PAWN_MDSE_LIST].Select(sMDSEFilter);

                            foreach (DataRow dataMsdeRow in dataMsdeRows)
                            {
                                Item storedPawnItem = new Item();
                                storedPawnItem.CaccLevel = Utilities.GetIntegerValue(dataMsdeRow["CACC_LEV"], -1);
                                storedPawnItem.CategoryCode = Utilities.GetIntegerValue(dataMsdeRow["CAT_CODE"], 0);
                                storedPawnItem.CategoryDescription = Utilities.GetStringValue(dataMsdeRow["CAT_DESC"], "");
                                storedPawnItem.GunNumber = Utilities.GetIntegerValue(dataMsdeRow["GUN_NUMBER"], 0);
                                storedPawnItem.HoldDesc = Utilities.GetStringValue(dataMsdeRow["HOLD_DESC"], "");
                                storedPawnItem.HoldType = Utilities.GetStringValue(dataMsdeRow["HOLD_TYPE"], "");
                                storedPawnItem.IsJewelry = Utilities.IsJewelry(storedPawnItem.CategoryCode);

                                //GJL 04/15/2010 - Fix for status reason with OFF- prefix
                                //Parse status reason.  If we detect an "OFF-" then we know
                                //to strip that off prior to performing the Enum.parse call
                                object mdseStatusObj = dataMsdeRow["STATUS_REASON"];
                                bool setMdseStatus = false;
                                if (mdseStatusObj != null)
                                {
                                    string mdseStatusStr = Utilities.GetStringValue(
                                        mdseStatusObj, string.Empty);
                                    if (!string.IsNullOrEmpty(mdseStatusStr))
                                    {
                                        if (mdseStatusStr.StartsWith(OFF_CONSTANT))
                                        {
                                            mdseStatusStr =
                                            mdseStatusStr.Substring(OFF_CONSTANT.Length);
                                        }
                                        storedPawnItem.ItemReason =
                                        (ItemReason)Enum.Parse(
                                            typeof(ItemReason), mdseStatusStr);
                                        setMdseStatus = true;
                                    }
                                }
                                if (!setMdseStatus)
                                {
                                    storedPawnItem.ItemReason = ItemReason.BLNK;
                                }
                                storedPawnItem.ItemAmount = Utilities.GetDecimalValue(dataMsdeRow["AMOUNT"], 0.00M);
                                storedPawnItem.ItemAmount_Original = Utilities.GetDecimalValue(dataMsdeRow["AMOUNT"], 0.00M);
                                storedPawnItem.Location = Utilities.GetStringValue(dataMsdeRow["LOCATION"], "");
                                //No ticket SMurphy 3/31/2010 - Location_Aisle was being populated with LOC_SHELF field
                                storedPawnItem.Location_Aisle = Utilities.GetStringValue(dataMsdeRow["LOC_AISLE"], "");
                                storedPawnItem.Location_Assigned = Utilities.GetStringValue(dataMsdeRow["LOC"]) == "" ? false : Utilities.GetStringValue(dataMsdeRow["LOC"]) == "Y" ? true : false;
                                storedPawnItem.Location_Shelf = Utilities.GetStringValue(dataMsdeRow["LOC_SHELF"], "");
                                storedPawnItem.mDocNumber = Utilities.GetIntegerValue(dataMsdeRow["ICN_DOC"], 0);
                                storedPawnItem.mDocType = Utilities.GetStringValue(dataMsdeRow["ICN_DOC_TYPE"], "1");
                                storedPawnItem.MerchandiseType = Utilities.GetStringValue(dataMsdeRow["MD_TYPE"], "");
                                storedPawnItem.mItemOrder = Utilities.GetIntegerValue(dataMsdeRow["ICN_ITEM"], 0);
                                storedPawnItem.mStore = Utilities.GetIntegerValue(dataMsdeRow["ICN_STORE"], 0);
                                storedPawnItem.mYear = Utilities.GetIntegerValue(dataMsdeRow["ICN_YEAR"], 0);
                                storedPawnItem.ItemStatus = (ProductStatus)Enum.Parse(typeof(ProductStatus), Utilities.GetStringValue(dataMsdeRow["STATUS_CD"], ""));

                                storedPawnItem.StatusDate = Utilities.GetDateTimeValue(dataMsdeRow["STATUS_DATE"], DateTime.MinValue);

                                if (outputDataSet.Tables[PAWN_OTHERDSC_LIST] != null)
                                {
                                    string sCommentFilter = "STORENUMBER = '" + storedPawnLoan.OrgShopNumber + "' ";
                                    sCommentFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                                    sCommentFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                    sCommentFilter += " and ICN_DOC = '" + storedPawnLoan.OrigTicketNumber + "' ";
                                    sCommentFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                    sCommentFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                    sCommentFilter += " and ICN_SUB_ITEM = 0";
                                    sCommentFilter += " and MASK_SEQ = 999";

                                    DataRow[] dataOtherCommentRows = outputDataSet.Tables[PAWN_OTHERDSC_LIST].Select(sCommentFilter);

                                    if (dataOtherCommentRows.Count() > 0)
                                    {
                                        storedPawnItem.Comment = Utilities.GetStringValue(dataOtherCommentRows[0]["OD_DESC"], "");
                                    }
                                    else
                                    {
                                        storedPawnItem.Comment = "";
                                    }
                                }

                                storedPawnItem.Icn = Utilities.IcnGenerator(storedPawnItem.mStore,
                                                                            storedPawnItem.mYear,
                                                                            storedPawnItem.mDocNumber,
                                                                            storedPawnItem.mDocType,
                                                                            storedPawnItem.mItemOrder,
                                                                            0);

                                storedPawnItem.Attributes = new List<ItemAttribute>();

                                for (int iMask = 1; iMask <= 15; iMask++)
                                {
                                    ItemAttribute itemAttribute = new ItemAttribute();

                                    if (Utilities.GetIntegerValue(dataMsdeRow["MASK" + iMask.ToString()], 0) > 0)
                                    {
                                        itemAttribute.MaskOrder = iMask;

                                        Answer answer = new Answer();
                                        answer.AnswerCode = Utilities.GetIntegerValue(dataMsdeRow["MASK" + iMask.ToString()], 0);
                                        answer.AnswerText = Utilities.GetStringValue(dataMsdeRow["MASK_DESC" + iMask.ToString()], "");

                                        // Pull from Other Description List Table
                                        if (outputDataSet.Tables[PAWN_OTHERDSC_LIST] != null && answer.AnswerCode == 999)
                                        {
                                            string sOtherDscFilter = "STORENUMBER = '" + storedPawnLoan.OrgShopNumber + "' ";
                                            sOtherDscFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                                            sOtherDscFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                            sOtherDscFilter += " and ICN_DOC = '" + storedPawnLoan.OrigTicketNumber + "' ";
                                            sOtherDscFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                            sOtherDscFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                            sOtherDscFilter += " and ICN_SUB_ITEM = 0";
                                            sOtherDscFilter += " and MASK_SEQ = " + iMask.ToString();

                                            DataRow[] dataOtherDScRows = outputDataSet.Tables[PAWN_OTHERDSC_LIST].Select(sOtherDscFilter);

                                            if (dataOtherDScRows.Count() > 0)
                                            {
                                                answer.AnswerCode = 999;
                                                answer.AnswerText = Utilities.GetStringValue(dataOtherDScRows[0]["OD_DESC"], "");
                                            }
                                            else
                                            {
                                                answer.AnswerCode = 0;
                                                answer.AnswerText = "";
                                            }
                                        }
                                        itemAttribute.Answer = answer;
                                    }
                                    if (itemAttribute.Answer.AnswerCode == 999 || itemAttribute.Answer.AnswerCode > 0)
                                        storedPawnItem.Attributes.Add(itemAttribute);
                                }

                                storedPawnItem.StorageFee = Utilities.GetDecimalValue(dataMsdeRow["STORAGE_FEE"], 0);
                                storedPawnItem.TempStatus = (StateStatus)Enum.Parse(typeof(StateStatus),
                                                                                    Utilities.GetStringValue(dataMsdeRow["TEMP_STATUS"]) != ""
                                                                                    ? Utilities.GetStringValue(dataMsdeRow["TEMP_STATUS"])
                                                                                    : StateStatus.BLNK.ToString());
                                storedPawnItem.TicketDescription = Utilities.GetStringValue(dataMsdeRow["MD_DESC"], "");

                                if (dataMsdeRow["RETAIL_PRICE"] != null)
                                {
                                    storedPawnItem.SelectedProKnowMatch = new ProKnowMatch();
                                    storedPawnItem.SelectedProKnowMatch.manufacturerModelInfo = new List<Answer>();
                                    storedPawnItem.SelectedProKnowMatch.manufacturerModelInfo.Add(new Answer());
                                    storedPawnItem.SelectedProKnowMatch.manufacturerModelInfo.Add(new Answer());
                                    storedPawnItem.SelectedProKnowMatch.manufacturerModelInfo.Add(new Answer());
                                    storedPawnItem.SelectedProKnowMatch.manufacturerModelInfo.Add(new Answer());

                                    ProKnowData proKnowData = new ProKnowData();
                                    ProCallData proCallData = new ProCallData();

                                    proKnowData.RetailAmount = Utilities.GetDecimalValue(dataMsdeRow["RETAIL_PRICE"], 0.00M);

                                    string sMDHistoryFilter = "STORENUMBER = '" + storedPawnLoan.OrgShopNumber + "' ";
                                    sMDHistoryFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                                    sMDHistoryFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                    sMDHistoryFilter += " and ICN_DOC = '" + storedPawnLoan.OrigTicketNumber + "' ";
                                    sMDHistoryFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                    sMDHistoryFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                    sMDHistoryFilter += " and ICN_SUB_ITEM = 0";

                                    if (outputDataSet.Tables[PAWN_MDHIST_LIST] != null)
                                    {
                                        DataRow[] dataMDHistoryRows = outputDataSet.Tables[PAWN_MDHIST_LIST].Select(sMDHistoryFilter);

                                        if (dataMDHistoryRows != null && dataMDHistoryRows.Count() > 0)
                                        {
                                            for (int iMDHistoryCount = 0; iMDHistoryCount < dataMDHistoryRows.Count(); iMDHistoryCount++)
                                            {
                                                switch (Utilities.GetStringValue(dataMDHistoryRows[iMDHistoryCount]["OLD_STATUS"], ""))
                                                {
                                                    case "PK":
                                                        proKnowData.LoanVarLowAmount = Utilities.GetDecimalValue(dataMDHistoryRows[iMDHistoryCount]["NEW_AMOUNT"], 0.00M);
                                                        proKnowData.LoanVarHighAmount = Utilities.GetDecimalValue(dataMDHistoryRows[iMDHistoryCount]["OLD_AMOUNT"], 0.00M);
                                                        break;
                                                    case "PKR":
                                                        proKnowData.RetailAmount = Utilities.GetDecimalValue(dataMDHistoryRows[iMDHistoryCount]["NEW_AMOUNT"], 0.00M);
                                                        proKnowData.RetailAmount = Utilities.GetDecimalValue(dataMDHistoryRows[iMDHistoryCount]["OLD_AMOUNT"], 0.00M);
                                                        break;
                                                    case "PC":
                                                        proCallData.NewRetail = Utilities.GetDecimalValue(dataMDHistoryRows[iMDHistoryCount]["NEW_AMOUNT"], 0.00M);
                                                        proCallData.NewRetail = Utilities.GetDecimalValue(dataMDHistoryRows[iMDHistoryCount]["OLD_AMOUNT"], 0.00M);
                                                        break;
                                                    case "STONE":
                                                        storedPawnItem.TotalLoanStoneValue = Utilities.GetDecimalValue(dataMDHistoryRows[iMDHistoryCount]["NEW_AMOUNT"], 0.00M);
                                                        storedPawnItem.TotalLoanStoneValue = Utilities.GetDecimalValue(dataMDHistoryRows[iMDHistoryCount]["OLD_AMOUNT"], 0.00M);
                                                        break;
                                                    case "PMETL":
                                                        storedPawnItem.TotalLoanGoldValue = Utilities.GetDecimalValue(dataMDHistoryRows[iMDHistoryCount]["NEW_AMOUNT"], 0.00M);
                                                        storedPawnItem.TotalLoanGoldValue = Utilities.GetDecimalValue(dataMDHistoryRows[iMDHistoryCount]["OLD_AMOUNT"], 0.00M);
                                                        break;
                                                }
                                            }
                                        }
                                    }

                                    storedPawnItem.SelectedProKnowMatch.proCallData = proCallData;
                                    storedPawnItem.SelectedProKnowMatch.selectedPKData = proKnowData;
                                    //Smurphy - Proknow flag for readonly on DescribeItem was never displayed
                                    if (proKnowData.RetailAmount != 0 && proKnowData.LoanVarHighAmount != 0 && proKnowData.LoanVarLowAmount != 0)
                                    {
                                        storedPawnItem.SelectedProKnowMatch.transitionStatus = TransitionStatus.MAN_MODEL_PROKNOW;
                                    }
                                }
                                //SR 12/07/09 No need for these fees since they should have been stored in
                                //Fee table when the loan was created
                                /*Fee feeLate;
                                if (dataMsdeRow["MISC1"] != null)
                                {
                                feeLate = new Fee
                                {
                                FeeType = FeeTypes.STORAGE,
                                Value = Utilities.GetDecimalValue(dataMsdeRow["MISC1"], 0.00M)
                                };
                                storedPawnLoan.Fees.Add(feeLate);
                                }
                                if (dataMsdeRow["STORAGE_FEE"] != null)
                                {
                                feeLate = new Fee
                                {
                                FeeType = FeeTypes.STORAGE,
                                Value = Utilities.GetDecimalValue(dataMsdeRow["STORAGE_FEE"], 0.00M)
                                };
                                storedPawnLoan.Fees.Add(feeLate);
                                }*/

                                if (outputDataSet.Tables[PAWN_GUN_LIST] != null)
                                {
                                    string sCommentFilter = "STORENUMBER = '" + storedPawnLoan.OrgShopNumber + "' ";
                                    sCommentFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                                    sCommentFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                    sCommentFilter += " and ICN_DOC = '" + storedPawnLoan.OrigTicketNumber + "' ";
                                    sCommentFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                    sCommentFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                    sCommentFilter += " and ICN_SUB_ITEM = 0";

                                    DataRow[] dataGunListRows = outputDataSet.Tables[PAWN_GUN_LIST].Select(sCommentFilter);

                                    if (dataGunListRows.Count() > 0)
                                    {
                                        storedPawnItem.HasGunLock = Utilities.GetStringValue(dataGunListRows[0]["GUNLOCK"], "") == "1"
                                                                    ? true : false;
                                    }
                                    else
                                    {
                                        storedPawnItem.HasGunLock = false;
                                    }
                                }

                                QuickCheck QuickInfo = new QuickCheck();
                                QuickInfo.Manufacturer = Utilities.GetStringValue(dataMsdeRow["MANUFACTURER"], "");
                                QuickInfo.Model = Utilities.GetStringValue(dataMsdeRow["MODEL"], "");
                                QuickInfo.SerialNumber = Utilities.GetStringValue(dataMsdeRow["SERIAL_NUMBER"], "");
                                QuickInfo.Weight = Utilities.GetDecimalValue(dataMsdeRow["WEIGHT"], 0);
                                QuickInfo.Quantity = Utilities.GetIntegerValue(dataMsdeRow["QUANTITY"], 0);
                                storedPawnItem.QuickInformation = QuickInfo;

                                #region Jewelry
                                if (storedPawnItem.Jewelry == null)
                                    storedPawnItem.Jewelry = new List<JewelrySet>();

                                string sMDSEJewelryFilter = "STORENUMBER = '" + storedPawnLoan.OrgShopNumber + "' ";
                                //sMDSEJewelryFilter += " and ICN_STORE = '" + Utilities.GetStringValue(dataPawnLoanRow["ICN_STORENUMBER"], "") + "' ";
                                sMDSEJewelryFilter += " and ICN_STORE = '" + storedPawnLoan.OrgShopNumber + "' ";
                                sMDSEJewelryFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                sMDSEJewelryFilter += " and ICN_DOC = '" + storedPawnLoan.OrigTicketNumber + "' ";
                                sMDSEJewelryFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                sMDSEJewelryFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                sMDSEJewelryFilter += " and ICN_SUB_ITEM > 0";

                                DataRow[] dataMsdeJewelryRows = outputDataSet.Tables[PAWN_MDSE_LIST].Select(sMDSEJewelryFilter);

                                for (int iJewelryCount = 0; iJewelryCount < dataMsdeJewelryRows.Count(); iJewelryCount++)
                                {
                                    JewelrySet jewelrySet = new JewelrySet();
                                    DataRow dataMsdeJewelryRow = dataMsdeJewelryRows[iJewelryCount];
                                    jewelrySet.CaccLevel = Utilities.GetIntegerValue(dataMsdeJewelryRow["CACC_LEV"], -1);
                                    jewelrySet.Category = Utilities.GetIntegerValue(dataMsdeJewelryRow["CAT_CODE"], 0);
                                    storedPawnItem.IsJewelry = true;
                                    jewelrySet.SubItemNumber = Utilities.GetIntegerValue(dataMsdeJewelryRow["ICN_SUB_ITEM"], 0);
                                    jewelrySet.TotalStoneValue = Utilities.GetDecimalValue(dataMsdeJewelryRow["AMOUNT"], 0.00M);
                                    jewelrySet.Icn = Utilities.IcnGenerator(storedPawnItem.mStore,
                                                                            storedPawnItem.mYear,
                                                                            storedPawnItem.mDocNumber,
                                                                            storedPawnItem.mDocType,
                                                                            storedPawnItem.mItemOrder,
                                                                            Utilities.GetIntegerValue(dataMsdeJewelryRow["ICN_SUB_ITEM"], 0));

                                    for (int iMask = 1; iMask <= 6; iMask++)
                                    {
                                        ItemAttribute itemAttribute = new ItemAttribute();

                                        if (Utilities.GetIntegerValue(dataMsdeJewelryRow["MASK" + iMask.ToString()], 0) > 0)
                                        {
                                            itemAttribute.MaskOrder = iMask;

                                            Answer answer = new Answer();
                                            answer.AnswerCode = Utilities.GetIntegerValue(dataMsdeJewelryRow["MASK" + iMask.ToString()], 0);
                                            answer.AnswerText = Utilities.GetStringValue(dataMsdeJewelryRow["MASK_DESC" + iMask.ToString()], "");

                                            // Pull from Other Description List Table
                                            if (outputDataSet.Tables[PAWN_OTHERDSC_LIST] != null && answer.AnswerCode == 999)
                                            {
                                                string sOtherDscFilter = "STORENUMBER = '" + storedPawnLoan.OrgShopNumber + "' ";
                                                sOtherDscFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                                                sOtherDscFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                                sOtherDscFilter += " and ICN_DOC = '" + storedPawnLoan.OrigTicketNumber + "' ";
                                                sOtherDscFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                                sOtherDscFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                                sOtherDscFilter += " and ICN_SUB_ITEM = " + Utilities.GetIntegerValue(dataMsdeJewelryRow["ICN_SUB_ITEM"], 0).ToString();
                                                sOtherDscFilter += " and MASK_SEQ = " + iMask.ToString();

                                                DataRow[] dataOtherDScRows = outputDataSet.Tables[PAWN_OTHERDSC_LIST].Select(sOtherDscFilter);

                                                if (dataOtherDScRows.Count() > 0)
                                                {
                                                    answer.AnswerCode = 999;
                                                    answer.AnswerText = Utilities.GetStringValue(dataOtherDScRows[0]["OD_DESC"], "");
                                                }
                                                else
                                                {
                                                    answer.AnswerCode = 0;
                                                    answer.AnswerText = "";
                                                }
                                            }
                                            itemAttribute.Answer = answer;
                                        }
                                        if (jewelrySet.ItemAttributeList == null)
                                            jewelrySet.ItemAttributeList = new List<ItemAttribute>();

                                        if (itemAttribute.Answer.AnswerCode == 999 || itemAttribute.Answer.AnswerCode > 0)
                                        {
                                            jewelrySet.ItemAttributeList.Add(itemAttribute);
                                        }
                                    }

                                    // jewelrySet.TicketDescription = Utilities.GetStringValue(dataMsdeJewelryRow["MD_DESC"], "");
                                    storedPawnItem.Jewelry.Add(jewelrySet);
                                }
                                #endregion

                                storedPawnItem.PfiAssignmentType = storedPawnItem.IsJewelry ? PfiAssignment.Scrap : PfiAssignment.Normal;
                                storedPawnItem.IsGun = Utilities.IsGun(storedPawnItem.GunNumber, storedPawnItem.CategoryCode, storedPawnItem.IsJewelry, storedPawnItem.MerchandiseType);

                                if (storedPawnLoan.Items == null)
                                    storedPawnLoan.Items = new List<Item>();
                                storedPawnLoan.Items.Add(storedPawnItem);
                            }
                            #endregion
                        }
                        lstPawnLoans.Add(storedPawnLoan);
                    }
                }
            }
        }

        public static bool GetCustomerInfo(
            string storeNumber,
            int ticketNumber,
            string refType,
            out string firstName,
            out string middleName,
            out string lastName,
            out string customerNumber,
            out string address,
            out string city,
            out string state,
            out string zipcode,
            out string idAgency,
            out string idType,
            out string idValue,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            firstName = string.Empty;
            middleName = string.Empty;
            lastName = string.Empty;
            customerNumber = string.Empty;
            address = string.Empty;
            city = string.Empty;
            state = string.Empty;
            zipcode = string.Empty;
            idAgency = string.Empty;
            idType = string.Empty;
            idValue = string.Empty;
            errorCode = string.Empty;
            errorText = string.Empty;

            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetCustomerInfo";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetCustomerInfoFailed", new ApplicationException("Cannot execute the Get Customer Info By Ticket stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            if (refType == ProductType.PAWN.ToString())
                refType = "1";
            else if (refType == ProductType.BUY.ToString())
                refType = "2";

            //Add cat pointer
            inParams.Add(new OracleProcParam("p_store_number", OracleDbType.Varchar2, storeNumber));
            inParams.Add(new OracleProcParam("p_ticket_number", OracleDbType.Int32, ticketNumber.ToString()));
            inParams.Add(new OracleProcParam("p_ref_type", OracleDbType.Varchar2, refType));
            inParams.Add(new OracleProcParam("o_first_name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 40));
            inParams.Add(new OracleProcParam("o_middle_name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 40));
            inParams.Add(new OracleProcParam("o_last_name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 40));
            inParams.Add(new OracleProcParam("o_customer_number", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 40));
            inParams.Add(new OracleProcParam("o_address", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 80));
            inParams.Add(new OracleProcParam("o_city", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 80));
            inParams.Add(new OracleProcParam("o_state", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 40));
            inParams.Add(new OracleProcParam("o_zipcode", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 40));
            inParams.Add(new OracleProcParam("o_id_agency", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 80));
            inParams.Add(new OracleProcParam("o_id_type", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 40));
            inParams.Add(new OracleProcParam("o_id_value", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 40));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_cust_procs", "get_customer_info_by_ticket",
                    inParams, null, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "GetCustomerInfo";
                errorText = "Invocation of GetCustomerInfo stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking Get_Customer_Info_By_Ticket stored proc", oEx);
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
                    for (int i = 0; i < outputDataSet.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = outputDataSet.Tables[0].Rows[i];
                        switch (i)
                        {
                            case 0:
                                firstName = Utilities.GetStringValue(dr[1], "");
                                break;
                            case 1:
                                middleName = Utilities.GetStringValue(dr[1], "") == "null" ? "" : Utilities.GetStringValue(dr[1], "");
                                break;
                            case 2:
                                lastName = Utilities.GetStringValue(dr[1], "");
                                break;
                            case 3:
                                customerNumber = Utilities.GetStringValue(dr[1], "");
                                break;
                            case 4:
                                address = Utilities.GetStringValue(dr[1], "");
                                break;
                            case 5:
                                city = Utilities.GetStringValue(dr[1], "");
                                break;
                            case 6:
                                state = Utilities.GetStringValue(dr[1], "");
                                break;
                            case 7:
                                zipcode = Utilities.GetStringValue(dr[1], "");
                                break;
                            case 8:
                                idAgency = Utilities.GetStringValue(dr[1], "");
                                break;
                            case 9:
                                idType = Utilities.GetStringValue(dr[1], "");
                                break;
                            case 10:
                                idValue = Utilities.GetStringValue(dr[1], "");
                                break;
                        }
                    }

                    errorCode = "0";
                    errorText = string.Empty;
                    return (true);
                }
            }

            errorCode = "GetCustomerInfoFailed";
            errorText = "Operation failed";
            return (false);
        }

        public static bool GetCustomerName(
            string storeNumber,
            int ticketNumber,
            string refType,
            out string firstName,
            out string middleName,
            out string lastName,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            firstName = string.Empty;
            middleName = string.Empty;
            lastName = string.Empty;
            errorCode = string.Empty;
            errorText = string.Empty;

            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetCustomerName";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetCustomerNameFailed", new ApplicationException("Cannot execute the Get Customer Name By Ticket stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add cat pointer
            inParams.Add(new OracleProcParam("p_store_number", OracleDbType.Varchar2, storeNumber));
            inParams.Add(new OracleProcParam("p_ticket_number", OracleDbType.Int32, ticketNumber.ToString()));
            if (refType == ProductType.PAWN.ToString())
                inParams.Add(new OracleProcParam("p_ref_type", "1"));
            else if (refType == ProductType.BUY.ToString())
                inParams.Add(new OracleProcParam("p_ref_type", "2"));
            inParams.Add(new OracleProcParam("o_first_name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 40));
            inParams.Add(new OracleProcParam("o_middle_name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 40));
            inParams.Add(new OracleProcParam("o_last_name", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 40));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_cust_procs", "get_customer_name_by_ticket",
                    inParams, null, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "GetCustomerName";
                errorText = "Invocation of GetCustomerName stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking Get_Customer_Name_By_Ticket stored proc", oEx);
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
                    for (int i = 0; i < outputDataSet.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = outputDataSet.Tables[0].Rows[i];
                        switch (i)
                        {
                            case 0:
                                firstName = Utilities.GetStringValue(dr[1], "");
                                break;
                            case 1:
                                middleName = Utilities.GetStringValue(dr[1], "") == "null" ? "" : Utilities.GetStringValue(dr[1], "");
                                break;
                            case 2:
                                lastName = Utilities.GetStringValue(dr[1], "");
                                break;
                        }
                    }

                    errorCode = "0";
                    errorText = string.Empty;
                    return (true);
                }
            }

            errorCode = "GetCustomerNameFailed";
            errorText = "Operation failed";
            return (false);
        }

        /// <summary>
        /// Check for temp status of the given loans
        /// </summary>
        /// <param name="lstRefNumbers"></param>
        /// <param name="storeNumbers"></param>
        /// <param name="tempStatusTable"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool CheckForTempStatus(
            List<int> lstRefNumbers,
            List<string> storeNumbers,
            out DataTable tempStatusTable,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            tempStatusTable = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetTempStatus";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetTempStatus", new ApplicationException("Cannot execute the GetTempStatus update stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            OracleProcParam maskParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_loan_number", lstRefNumbers.Count());
            foreach (int i in lstRefNumbers)
            {
                maskParam.AddValue(i.ToString());
            }
            inParams.Add(maskParam);

            OracleProcParam storeNumParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_store_number", storeNumbers.Count());
            foreach (string s in storeNumbers)
            {
                storeNumParam.AddValue(s);
            }
            inParams.Add(storeNumParam);

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            refCursors.Add(new PairType<string, string>("o_temp_status", "temp_status"));

            DataSet outputDataSet;
            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MANAGE_HOLDS", "check_for_temp_status",
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
                if (outputDataSet != null && outputDataSet.IsInitialized)
                {
                    if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                    {
                        tempStatusTable = outputDataSet.Tables["temp_status"];
                        errorCode = "0";
                        errorText = string.Empty;
                        return (true);
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Checks the temp status of the passed in loans and if the same
        /// as the temp status passed in updates it to CH
        /// </summary>
        /// <param name="lstRefNumbers"></param>
        /// <param name="lstTempStatus"></param>
        /// <param name="storeNumbers"></param>
        /// <param name="callFrom"></param>
        /// <param name="userId"></param>
        /// <param name="tempStatusTable"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool CheckAndUpdateTempStatus(
            List<int> lstRefNumbers,
            List<string> lstTempStatus,
            List<string> storeNumbers,
            string callFrom,
            string userId,
            out DataTable tempStatusTable,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            tempStatusTable = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "CheckAndUpdateTempStatus";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("CheckAndUpdateTempStatus", new ApplicationException("Cannot execute the GetTempStatus update stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            OracleProcParam maskParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_loan_number", lstRefNumbers.Count());
            foreach (int i in lstRefNumbers)
            {
                maskParam.AddValue(i.ToString());
            }
            inParams.Add(maskParam);

            OracleProcParam maskTempStatusParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_temp_status", lstTempStatus.Count());
            foreach (string s in lstTempStatus)
            {
                if (s == StateStatus.BLNK.ToString())
                    maskTempStatusParam.AddValue("");
                else
                    maskTempStatusParam.AddValue(s);
            }
            inParams.Add(maskTempStatusParam);

            OracleProcParam maskStoreNumberParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_store_number", storeNumbers.Count());
            foreach (string s in storeNumbers)
            {
                maskStoreNumberParam.AddValue(s);
            }
            inParams.Add(maskStoreNumberParam);

            inParams.Add(new OracleProcParam("p_call_from", callFrom));

            inParams.Add(new OracleProcParam("p_user_id", userId));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            refCursors.Add(new PairType<string, string>("o_temp_status", "temp_status"));

            DataSet outputDataSet;
            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MANAGE_HOLDS", "check_for_temp_status",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "check_for_temp_status Failed";
                errorText = "Invocation of check_for_temp_status stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking check_for_temp_status stored proc", oEx);
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
                if (outputDataSet != null && outputDataSet.IsInitialized)
                {
                    if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                    {
                        tempStatusTable = outputDataSet.Tables["temp_status"];

                        return (true);
                    }
                }
                errorCode = "1";
                errorText = dA.ErrorDescription;
                return false;
            }
        }

        public static bool GetExtensionDetails(
            string storeNumber,
            int ticketNumber,
            string receiptNumber,
            out DataSet data)
        {
            data = null;
            try
            {
                var dA = GlobalDataAccessor.Instance.OracleDA;

                var inParams = new List<OracleProcParam>();

                inParams.Add(new OracleProcParam("p_store_nr", storeNumber));
                inParams.Add(new OracleProcParam("p_ticket_nr", ticketNumber));
                inParams.Add(new OracleProcParam("p_receipt_nr", receiptNumber));

                var refCursors = new List<PairType<string, string>>();
                refCursors.Add(new PairType<string, string>("o_data", "extension"));

                dA.issueSqlStoredProcCommand("ccsowner", "service_pawn_loans"
                                             , "get_extension_data"
                                             , inParams
                                             , refCursors
                                             , "o_error_code"
                                             , "o_error_text"
                                             , out data);
            }
            catch (Exception eX)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "CustomerLoans::GetExtensionDetails", "Exception thrown: {0}", eX);
                }
                BasicExceptionHandler.Instance.AddException("CustomerLoans::GetExtensionDetails", eX);
            }

            return true;
        }

        public static bool getActiveLoanCount(
            string storeNumber,
            string currentDate,
            string customerNumber,
            out int loanCount,
            out string errorCode,
            out string errorMessage)
        {
            //Set default output params
            errorCode = string.Empty;
            errorMessage = string.Empty;
            loanCount = 0;

            if (GlobalDataAccessor.Instance == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "getActiveLoanCount Failed";
                errorMessage = "Invalid desktop session or data accessor";

                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_current_date", currentDate));

            inParams.Add(new OracleProcParam("p_customer_number", customerNumber));

            inParams.Add(new OracleProcParam("o_total_loans", OracleDbType.Int32, loanCount, ParameterDirection.Output, 9));

            bool rt = false;
            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();


            DataSet outputDataSet;
            bool returnVal;
            try
            {
                returnVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_store_procs",
                    "getActiveLoanCount", inParams,
                    refCursors, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling getActiveLoanCount stored procedure", oEx);
                return (false);
            }

            if (returnVal == false)
            {
                errorCode = dA.ErrorCode;
                errorMessage = dA.ErrorDescription;
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
                        loanCount = Int32.Parse(nextNumStr);
                        errorCode = "0";
                        errorMessage = "Success";
                        return (true);
                    }
                }
            }


            errorCode = "1";
            errorMessage = "Get Active loan count stored procedure failed";
            return (false);
        }



    }
}
