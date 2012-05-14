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
using Support.Libraries.Objects.PDLoan;

namespace Support.Controllers.Database.Procedures
{
    public static class CustomerLoans
    {
        public static readonly string PDLOAN_KEYS = "o_loan_header_loan";
        public static readonly string PDLOAN_DETAILS = "o_loan_detail_loandetails";
        public static readonly string PDLOAN_XPP = "o_xpp_detail_xpp";
        public static readonly string PDLOAN_event = "o_loan_event_event";
        /// <summary>
        /// Provides back stored Loan Key data into a DataSet.
        /// </summary>
        /// <param name="customerNumber"></param>
        /// <param name="loanKeys"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool GetPDLoanKeys(
            string customerNumber,
            out List<PDLoan> loanKeys,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            loanKeys = new List<PDLoan>();

            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "get_loan_header";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetLoanKeysFailed", new ApplicationException("Cannot execute the Get PD Loan Keys stored procedure"));
                return (false);
            }

            //Get data accessor object
            var dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            var inParams = new List<OracleProcParam>();

            //Add cat pointer
            inParams.Add(new OracleProcParam("p_customer_number", customerNumber));

            //Setup ref cursor array
            var refCursors = new List<PairType<string, string>>
            {
                new PairType<string, string>("o_loan_header", PDLOAN_KEYS),
            };

            //Create output data set names
            bool retVal = false;
            try
            {
                // EDW - CR#11333
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_SUPPORT_PRODUCTS", "GET_LOAN_HEADER",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "GetPDLoanKeys";
                errorText = "Invocation of GetPDLoanKeys stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking GET_LOAN_HEADER stored proc", oEx);
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
                    DataTable dT = outputDataSet.Tables[PDLOAN_KEYS];

                    foreach (DataRow curDr in dT.Rows)
                    {
                        PDLoan pVO = new PDLoan();
                        pVO.Status = Utilities.GetStringValue(curDr["open_closed"]);
                        pVO.StatusDate = Utilities.GetDateTimeValue(curDr["loanstatuschangedate"]);
                        pVO.PDLLoanNumber = Utilities.GetStringValue(curDr["loannumber"]);
                        pVO.Type = Utilities.GetStringValue(curDr["product_type"]);
                        loanKeys.Add(pVO);
                    }
                }

                errorCode = "0";
                errorText = string.Empty;

                return (true);
            }

            errorCode = "GetPDLoanKeysFailed";
            errorText = "Operation failed";
            return (false);
        }

        /// <summary>
        /// Provides back stored Loan Key data into a DataSet.
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool GetPDLoanDetails(
            PDLoan pdLoan,
            out string errorCode,
            out string errorText)
        {

            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            if (pdLoan == null)
            {
                errorCode = "NullLoanNumber";
                errorText = "The PDLoan object is null.";
                return false;
            }            
            DataSet outputDataSet = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "get_loan_details";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetPDLoanDetailsFailed", new ApplicationException("Cannot execute the Get PD Loan Details stored procedure"));
                return (false);
            }

            //Get data accessor object
            var dA = GlobalDataAccessor.Instance.OracleDA;

            var current_xpp = string.Empty;
            DateTime xpp_start_date = DateTime.Now;
            DateTime xpp_end_date = DateTime.Now;
            int xpp_fee_amt = 0;
            //Create input list
            var inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_loannumber", pdLoan.PDLLoanNumber));
            //inParams.Add(new OracleProcParam("o_current_xpp", current_xpp));
            //inParams.Add(new OracleProcParam("o_xpp_start_date", xpp_start_date));
            //inParams.Add(new OracleProcParam("o_xpp_end_date", xpp_end_date));
            //inParams.Add(new OracleProcParam("o_xpp_fee_amt", xpp_fee_amt));
            inParams.Add(new OracleProcParam("o_current_xpp", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 256));
            inParams.Add(new OracleProcParam("o_xpp_start_date", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 256));
            inParams.Add(new OracleProcParam("o_xpp_end_date", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 256));
            inParams.Add(new OracleProcParam("o_xpp_fee_amt", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 256));


            //Setup ref cursor array
            var refCursors = new List<PairType<string, string>>
            {
                new PairType<string, string>("o_loan_detail", PDLOAN_DETAILS),
                new PairType<string, string>("o_xpp_detail", PDLOAN_XPP),
                new PairType<string, string>("o_loan_event", PDLOAN_event),
            };

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_SUPPORT_PRODUCTS", "get_loan_detail", 
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "GetPDLoanDetails";
                errorText = "Invocation of GetPDLoanDetails stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking get_loan_detail stored proc", oEx);
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
                    var _PDLoanKeys = Support.Logic.CashlinxPawnSupportSession.Instance.PDLoanKeys;
                    DataTable dT = outputDataSet.Tables[PDLOAN_DETAILS];

                    foreach (DataRow curDr in dT.Rows)
                    {
                        var loanDetails = new PDLoanDetails();
                        //loanDetails.CustomerSSN = Utilities.GetStringValue(curDr["open_closed"]);
                        loanDetails.UWName = Utilities.GetStringValue(curDr["uw_name"]);
                        loanDetails.LoanRequestDate = Utilities.GetDateTimeValue(curDr["requested_time"]);
                        loanDetails.LoanAmt = Utilities.GetDecimalValue(curDr["loan_amt"]);
                        loanDetails.LoanPayOffAmt = Utilities.GetDecimalValue(curDr["payoff_amt"]);
                        loanDetails.ActualLoanAmt = Utilities.GetDecimalValue(curDr["actual_amt"]);
                        loanDetails.LoanNumberOrig = Utilities.GetStringValue(curDr["orig_loan_nbr"]);
                        loanDetails.LoanNumberPrev = Utilities.GetStringValue(curDr["prev_loan_nbr"]);

                        loanDetails.DeclineReasonDescription = Utilities.GetStringValue(curDr["reason_type"]);
                        //loanDetails.DeclineReasonCode = Utilities.GetStringValue(curDr["open_closed"]);
                        loanDetails.LoanRolloverNotes = Utilities.GetStringValue(curDr["rollovers_rem"]);
                        loanDetails.LoanRollOverAmt = Utilities.GetDecimalValue(curDr["rollover_amt"]);
                        var revokeACH = Utilities.GetStringValue(curDr["revoke_ach"]);
                        if (revokeACH.Equals("Y") || revokeACH.Equals("YES"))
                            loanDetails.RevokeACH = true;
                        else
                            loanDetails.RevokeACH = false;

                        var xppAvailable = Utilities.GetStringValue(curDr["xpp_available"]);
                        if (xppAvailable.Equals("Y") || xppAvailable.Equals("YES"))
                            loanDetails.XPPAvailable = true;
                        else
                            loanDetails.XPPAvailable = false;

                        loanDetails.ActualFinanceChrgAmt = Utilities.GetDecimalValue(curDr["actual_fc"]);
                        loanDetails.AcutalServiceChrgAmt = Utilities.GetDecimalValue(curDr["actual_sc"]);
                        loanDetails.AccruedFinanceAmt = Utilities.GetDecimalValue(curDr["accrued_finance"]);
                        loanDetails.LateFeeAmt = Utilities.GetDecimalValue(curDr["late_fee"]);
                        loanDetails.NSFFeeAmt = Utilities.GetDecimalValue(curDr["nsf_fee"]);
                        loanDetails.ACHWaitingToClear = Utilities.GetStringValue(curDr["ACH_wating_clear"]);
                        loanDetails.EstRolloverAmt = Utilities.GetDecimalValue(curDr["est_rollover_amt"]);
                        loanDetails.OrginationDate = Utilities.GetDateTimeValue(curDr["origination_date"]);
                        loanDetails.DueDate = Utilities.GetDateTimeValue(curDr["due_date"]);
                        loanDetails.OrigDepDate = Utilities.GetDateTimeValue(curDr["orig_dep_date"]);
                        loanDetails.ExtendedDate = Utilities.GetDateTimeValue(curDr["extended_date"]);
                        loanDetails.LastUpdatedBy = Utilities.GetStringValue(curDr["last_upd_by"]);
                        loanDetails.ShopNo = Utilities.GetStringValue(curDr["shop_number"]);
                        loanDetails.ShopName = Utilities.GetStringValue(curDr["shop_name"]);
                        loanDetails.ShopState = Utilities.GetStringValue(curDr["shop_state"]);
                        pdLoan.GetPDLoanDetails = loanDetails;
                    }

                    DataTable dTXPP = outputDataSet.Tables[PDLOAN_XPP];
                    foreach (DataRow curDr in dTXPP.Rows)
                    {
                        var XPPDetail = new PDLoanXPPScheduleList();
                        XPPDetail.xppPaymentSeqNumber = Utilities.GetStringValue(curDr["schedule_nbr"]);
                        XPPDetail.xppPaymentNumber = Utilities.GetStringValue(curDr["payment_nbr"]);
                        XPPDetail.xppDate = Utilities.GetDateTimeValue(curDr["payment_date"]);
                        XPPDetail.xppAmount = Utilities.GetDecimalValue(curDr["payment_amount"]);
                        pdLoan.GetPDLoanXPPScheduleList.Add(XPPDetail);
                    }

                }
                    
                errorCode = "0";
                errorText = string.Empty;

                return (true);
            }

            errorCode = "GetPDLoanKeysFailed";
            errorText = "Operation failed";
            return (false);
        }
    }
}
