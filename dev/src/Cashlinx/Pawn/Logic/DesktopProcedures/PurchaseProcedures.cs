using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Pawn.Logic.DesktopProcedures
{
    public class PurchaseProcedures
    {
        public static readonly string PURCHASE_DATA = "o_purchase_data";
        public static readonly string PAWN_MDSE_LIST = "o_pawn_mdselist";
        public static readonly string PAWN_MDHIST_LIST = "o_pawn_mdhistlist";
        public static readonly string PAWN_GUN_LIST = "o_pawn_gunlist";
        public static readonly string PAWN_OTHERDSC_LIST = "o_pawn_otherdsclist";
        public static readonly string PAWN_RECEIPTDET_LIST = "o_pawn_receiptdetlist";
        public static readonly string PURCHASE_MDSE_LIST = "o_pawn_mdselist";
        public static readonly string PURCHASE_MDHIST_LIST = "o_pawn_mdhistlist";
        public static readonly string PURCHASE_GUN_LIST = "o_pawn_gunlist";
        public static readonly string PURCHASE_OTHERDSC_LIST = "o_pawn_otherdsclist";
        public static readonly string PURCHASE_RECEIPTDET_LIST = "o_pawn_receiptdetlist";

        public static readonly string OFF_CONSTANT = "OFF-";

        /// <summary>
        /// Provides back stored Pawn Loan data into a DataSet.  If dates are not needed for search, pass null.
        /// </summary>
        /// <param name="dA"> </param>
        /// <param name="customerNumber"></param>
        /// <param name="status"></param>
        /// <param name="TempStatus"> </param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <param name="outputDataSet"> </param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool GetCustomerPurchases(
            OracleDataAccessor dA,
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
            if (dA == null)
            {
                errorCode = "GetCustomerPurchases";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetCustomerPurchasesFailed", new ApplicationException("Cannot execute the Get Customer Loans stored procedure"));
                return (false);
            }

            //Get data accessor object
            //OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

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
            inParams.Add(new OracleProcParam("p_transaction_type", null));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_purchase_data", PURCHASE_DATA));
            refCursors.Add(new PairType<string, string>("o_pawn_mdselist", PAWN_MDSE_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_gunlist", PAWN_GUN_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_mdhistlist", PAWN_MDHIST_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_otherdsclist", PAWN_OTHERDSC_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_receiptdetlist", PAWN_RECEIPTDET_LIST));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_purchases", "Get_Customer_Purchases",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = " -- GetCustomerPurchases";
                errorText = " -- Invocation of GetCustomerPurchases stored proc failed";
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

            errorCode = "GetCustomerPurchasesFailed";
            errorText = "Operation failed";
            return (false);
        }

        internal static bool GetCustomerPurchases(string custNumber)
        {
            bool retValue = false;
            DataSet custPurchase;
            string errorCode;
            string errorText;
            List<PurchaseVO> purchases;
            //Set end date to be shopdate
            string toDate = ShopDateTime.Instance.ShopDate.ToShortDateString();

            retValue = GetCustomerPurchases(GlobalDataAccessor.Instance.OracleDA, custNumber, ProductStatus.ALL, StateStatus.BLNK, null, toDate, out custPurchase, out errorCode, out errorText);
            if (retValue)
            { 
                ParseDataSet(custPurchase, out purchases);
                if (purchases != null)
                    GlobalDataAccessor.Instance.DesktopSession.CustomerHistoryPurchases = purchases;
            }

            return retValue;
        }

        public static bool InsertPurchaseRecord(
            string purchaseType,
            string storeNumber,
            string customerNumber,
            string entityNumber,
            string entityType,
            string pfiElig,
            decimal amount,
            decimal netAmount,
            string purchaseStatus,
            string statusDate,
            string statusTime,
            string userID,
            string cashDrawer,
            string orgDate,
            decimal orgAmount,
            Int32 lastLine,
            string purchaseOrderNumber,
            string purchaseRefNumber,
            decimal freight,
            decimal tax,
            int manualTicket,
            string miscFlags,
            string ttyId,
            string shipType,
            string shipNumber,
            string shipComment,
            string comments,
            out int purchaseTicketNumber,
            out string errorCode,
            out string errorText)
        {
            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;
            purchaseTicketNumber = 0;

            //Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertNewPawnLoanRecord Failed",
                                                            new ApplicationException("ExecuteInsertNewPawnLoanRecord Failed: Data accessor instance is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_purchase_type", purchaseType));
            oParams.Add(new OracleProcParam("p_purchase_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_purchase_cust_num", customerNumber));
            oParams.Add(new OracleProcParam("p_entity_number", entityNumber));
            oParams.Add(new OracleProcParam("p_entity_type", entityType));
            oParams.Add(new OracleProcParam("p_purchase_pfi_elig", pfiElig));
            oParams.Add(new OracleProcParam("p_purchase_amount", amount));
            oParams.Add(new OracleProcParam("p_purchase_net_amount", netAmount));
            oParams.Add(new OracleProcParam("p_purchase_status_cd", purchaseStatus));
            oParams.Add(new OracleProcParam("p_purchase_status_date", statusDate));
            oParams.Add(new OracleProcParam("p_purchase_status_time", statusTime));
            oParams.Add(new OracleProcParam("p_user_id", userID));
            oParams.Add(new OracleProcParam("p_cash_drawer", cashDrawer));

            oParams.Add(new OracleProcParam("p_org_date", orgDate));
            oParams.Add(new OracleProcParam("p_org_amount", orgAmount));
            oParams.Add(new OracleProcParam("p_last_line", lastLine));
            oParams.Add(new OracleProcParam("p_purchase_order_number", purchaseOrderNumber));
            oParams.Add(new OracleProcParam("p_purchase_ref_number", purchaseRefNumber));
            oParams.Add(new OracleProcParam("p_purchase_freight_amt", freight));
            oParams.Add(new OracleProcParam("p_purchase_tax_amt", tax));
            oParams.Add(new OracleProcParam("p_purchase_man_ticket", manualTicket));
            oParams.Add(new OracleProcParam("p_purchase_misc_flags", miscFlags));
            oParams.Add(new OracleProcParam("p_purchase_tty_id", ttyId));
            oParams.Add(new OracleProcParam("p_purchase_ship_type", shipType));
            oParams.Add(new OracleProcParam("p_purchase_ship_number", shipNumber));
            oParams.Add(new OracleProcParam("p_purchase_ship_comment", shipComment));
            oParams.Add(new OracleProcParam("p_icn_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_comments", comments));
            oParams.Add(new OracleProcParam("o_purchase_ticket_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1)); 

            //Make stored proc call
            bool retVal;
            DataSet outDSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "pawn_purchases", "insert_purchase_header", oParams, null, "o_return_code",
                                                                                            "o_return_text", out outDSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertPurchaseRecord Failed", oEx);
                errorCode = "ExecuteInsertPurchaseRecordFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertPurchaseRecord Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- ExecuteInsertPurchaseRecordFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            //Get output number
            DataTable outputDt = outDSet.Tables["OUTPUT"];
            if (outputDt != null && outputDt.IsInitialized &&
                outputDt.Rows != null && outputDt.Rows.Count > 0)
            {
                DataRow dr = outputDt.Rows[0];
                if (dr != null && dr.ItemArray.Length > 0)
                {
                    object purchaseNumber = dr.ItemArray.GetValue(1);
                    if (purchaseNumber != null)
                    {
                        purchaseTicketNumber = Int32.Parse((string)purchaseNumber);

                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }
            return (false);
        }

        public static bool InsertPurchasePaymentDetails(
            string storeNumber,
            string cashDrawer,
            string userName,
            string receiptDate,
            List<string> purchaseNumber,
            List<string> refDate,
            List<string> refTime,
            List<string> refEvent,
            List<string> refAmount,
            string statusTime,
            string tenderType,
            out int receiptNumber,
            out string errorCode,
            out string errorText)
        {
            receiptNumber = 0;
            errorCode = "";
            errorText = "";
            //Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("InsertPurchasePaymentDetails Failed",
                                                            new ApplicationException("InsertPurchasePaymentDetails Failed: Data accessor instance is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_cashdrawer", cashDrawer));
            oParams.Add(new OracleProcParam("p_user_name", userName));
            oParams.Add(new OracleProcParam("p_receipt_date", receiptDate));
            oParams.Add(new OracleProcParam("p_purchase_number", true, purchaseNumber));
            oParams.Add(new OracleProcParam("p_ref_date", true, refDate));
            oParams.Add(new OracleProcParam("p_ref_time", true, refTime));
            oParams.Add(new OracleProcParam("p_ref_event", true, refEvent));
            oParams.Add(new OracleProcParam("p_ref_amt", true, refAmount));
            oParams.Add(new OracleProcParam("p_status_time", statusTime));
            oParams.Add(new OracleProcParam("p_payment_type", tenderType));
            oParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            //Make stored proc call
            bool retVal;
            DataSet outDSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "pawn_purchases", "insert_purch_payment_details", oParams, null, "o_return_code",
                                                                                            "o_return_text", out outDSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("InsertPurchasePaymentDetails Failed", oEx);
                errorCode = "InsertPurchasePaymentDetailsFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("InsertPurchasePaymentDetails Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- InsertPurchasePaymentDetailsFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            //Get output number
            DataTable outputDt = outDSet.Tables["OUTPUT"];
            if (outputDt != null && outputDt.IsInitialized &&
                outputDt.Rows != null && outputDt.Rows.Count > 0)
            {
                DataRow dr = outputDt.Rows[0];
                if (dr != null && dr.ItemArray.Length > 0)
                {
                    object receiptNumberData = dr.ItemArray.GetValue(1);
                    if (receiptNumberData != null)
                    {
                        receiptNumber = Int32.Parse(receiptNumberData.ToString());

                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }

            return false;
        }

        public static bool GetPurchaseData(
            Int32 storeNumber,
            Int32 idNumber,
            string idType,
            StateStatus tempStatus,
            string transactionType,
            bool bGetCustomerInfo,
            out PurchaseVO purchase,
            out CustomerVO customerVO,
            out string tenderType,
            out string errorCode,
            out string errorText)
        {
            //Set default output params
            customerVO = null;
            purchase = null;
            errorCode = string.Empty;
            errorText = string.Empty;
            tenderType = string.Empty;

            if (GlobalDataAccessor.Instance.DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetPurchaseDataFailed";
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
            if (tempStatus != StateStatus.BLNK)
                inParams.Add(new OracleProcParam("p_temp_type", tempStatus.ToString()));
            else
                inParams.Add(new OracleProcParam("p_temp_type", "0"));
            inParams.Add(new OracleProcParam("p_transaction_type", transactionType));
            inParams.Add(new OracleProcParam("o_tender", OracleDbType.Varchar2, tenderType, ParameterDirection.Output, 255));

            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("o_purchase_data", PURCHASE_DATA));
            refCursArr.Add(new PairType<string, string>("o_pawn_mdselist", PAWN_MDSE_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_gunlist", PAWN_GUN_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_mdsehistlist", PAWN_MDHIST_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_otherdsclist", PAWN_OTHERDSC_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_receiptdetlist", PAWN_RECEIPTDET_LIST));
            
            
            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_purchases",
                    "return_purchase_data", inParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling return_purchase_data stored procedure", oEx);
                errorCode = " -- return_purchase_data failed";
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
                        List<PurchaseVO> purchases;

                        try
                        {
                            ParseDataSet(outputDataSet, out purchases);
                            if (outputDataSet.Tables["OUTPUT"].Rows.Count > 0)
                            {
                                tenderType = outputDataSet.Tables["OUTPUT"].Rows[0].ItemArray[1].ToString();
                            }

                            if (CollectionUtilities.isEmpty(purchases))
                            {
                                errorCode = "Parsing the data from the stored procedure failed";
                                errorText = "purchase object is null";
                                return false;
                            }
                            purchase = purchases.First();
                            
                            if (bGetCustomerInfo)
                                customerVO = CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, purchase.CustomerNumber);

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

        public static bool GetPurchaseDataFromIcn(
            OracleDataAccessor oDa,
            string icn,
            string icn_store,
            bool bGetCustomerInfo,
            out PurchaseVO purchase,
            out CustomerVO customerVO,
            out string tenderType,
            out string errorCode,
            out string errorText)
        {
            //Set default output params
            customerVO = null;
            purchase = null;
            errorCode = string.Empty;
            errorText = string.Empty;
            tenderType = string.Empty;

            if (oDa == null)
            {
                errorCode = "GetPurchaseDataFromICNFailed";
                errorText = "Invalid data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add store number
            inParams.Add(new OracleProcParam("p_full_icn", icn));
            //BZ # 301 - add new param store number to work with icn short code
            inParams.Add(new OracleProcParam("p_store_number", icn_store));
            inParams.Add(new OracleProcParam("o_tender", OracleDbType.Varchar2, tenderType, ParameterDirection.Output, 255));

            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("o_purchase_data", PURCHASE_DATA));
            refCursArr.Add(new PairType<string, string>("o_pawn_mdselist", PAWN_MDSE_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_gunlist", PAWN_GUN_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_mdsehistlist", PAWN_MDHIST_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_otherdsclist", PAWN_OTHERDSC_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_receiptdetlist", PAWN_RECEIPTDET_LIST));

            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                retVal = oDa.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_purchases",
                    "get_purchase_by_icn", inParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling get_purchase_by_icn stored procedure", oEx);
                errorCode = " -- get_purchase_by_icn failed";
                errorText = " --- Exception: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                errorCode = oDa.ErrorCode;
                errorText = oDa.ErrorDescription;
                return (false);
            }
            if (outputDataSet != null && outputDataSet.IsInitialized)
            {
                if (outputDataSet.Tables.Count > 0)
                {
                    List<PurchaseVO> purchases;

                    try
                    {
                        ParseDataSet(outputDataSet, out purchases);
                        if (CollectionUtilities.isEmpty(purchases))
                        {
                            errorCode = "Parsing the data from the stored procedure failed";
                            errorText = "purchase object is null";
                            return false;
                        }
                        purchase = purchases.First();
                        if (outputDataSet.Tables["OUTPUT"].Rows.Count > 0)
                        {
                            tenderType = outputDataSet.Tables["OUTPUT"].Rows[0].ItemArray[1].ToString();
                        }

                        if (bGetCustomerInfo)
                            customerVO = CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, purchase.CustomerNumber);

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

            errorCode = "GetPurchaseDataFromICNFailed";
            errorText = "Operation failed";
            return (false);
        }

        public static bool GetStorePurchases(
            OracleDataAccessor oDa,
            string storeNumber,
            ProductStatus productStatus,
            StateStatus TempStatus,
            string TransactionType,
            DateTime date,
            bool bGetCustomerInfo,
            out List<PurchaseVO> purchases,
            out List<CustomerVO> customerVOs,
            out string errorCode,
            out string errorText)
        {
            //Set default output params
            purchases = new List<PurchaseVO>();
            customerVOs = new List<CustomerVO>();
            errorCode = string.Empty;
            errorText = string.Empty;

            if (oDa == null)
            {
                errorCode = "GetStoreLoansFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            //Get data accessor object

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add store number
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_status", productStatus.ToString()));
            inParams.Add(new OracleProcParam("p_date", date));
            if (TempStatus != StateStatus.BLNK)
                inParams.Add(new OracleProcParam("p_temp_type", TempStatus.ToString()));
            else
                inParams.Add(new OracleProcParam("p_temp_type", ""));
            inParams.Add(new OracleProcParam("p_transaction_type", TransactionType));

            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("o_pawn_purchase", PURCHASE_DATA));
            refCursArr.Add(new PairType<string, string>("o_pawn_mdselist", PURCHASE_MDSE_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_gunlist", PURCHASE_GUN_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_mdhistlist", PURCHASE_MDHIST_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_otherdsclist", PURCHASE_OTHERDSC_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_receiptdetlist", PURCHASE_RECEIPTDET_LIST));
            
            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                // EDW - CR#11333 - This method is not currently used, so no need to update
                //  "ccsowner", "ed_pawn_purchases",
                retVal = oDa.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_purchases",
                    "get_store_purchases", inParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling get_store_purchases stored procedure", oEx);
                errorCode = " -- GetStorePurchases failed";
                errorText = " --- Exception: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                errorCode = oDa.ErrorCode;
                errorText = oDa.ErrorDescription;
                return (false);
            }
            if (outputDataSet != null && outputDataSet.IsInitialized)
            {
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    try
                    {
                        ParseDataSet(outputDataSet, out purchases);
                        if (CollectionUtilities.isEmpty(purchases))
                        {
                            errorCode = "Parsing the data from the stored procedure failed";
                            errorText = "purchases object is null";
                            return false;
                        }

                        if (bGetCustomerInfo)
                        {
                            foreach (PurchaseVO purchaseObj in purchases)
                            {
                                customerVOs.Add(CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, purchaseObj.CustomerNumber));
                            }
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

            errorCode = "GETSTOREPURCHASESFAIL";
            errorText = "Operation failed";
            return (false);
        }

        public static void ParseDataSet(DataSet outputDataSet, out List<PurchaseVO> lstPurchases)
        {
            var sDATA = string.Empty;

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

            lstPurchases = new List<PurchaseVO>();

            if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
            {
                if (outputDataSet.Tables[PURCHASE_DATA] != null)
                {
                    // Iterate through the Pawn Loan records returned from Search
                    foreach (DataRow dataPawnLoanRow in outputDataSet.Tables[PURCHASE_DATA].Rows)
                    {
                        PurchaseVO storedPurchase = new PurchaseVO
                        {
                            Amount =
                            Utilities.GetDecimalValue(
                                dataPawnLoanRow[
                                "AMOUNT"],
                                new decimal()),
                            EntityId =
                            Utilities.GetStringValue(
                                dataPawnLoanRow[
                                "ENT_ID"],
                                ""),
                            CreatedBy =
                            Utilities.GetStringValue(
                                dataPawnLoanRow[
                                "CREATEDBY"],
                                ""),
                            CustomerNumber =
                            Utilities.GetStringValue(
                                dataPawnLoanRow[
                                "CUSTOMERNUMBER"],
                                ""),
                            EntityType =
                            Utilities.GetStringValue(
                                dataPawnLoanRow[
                                "Entity_type"], ""),
                            EntityNumber =
                            Utilities.GetStringValue(
                                dataPawnLoanRow[
                                "Entity_number"], ""),


                            LastUpdatedBy =
                            Utilities.GetStringValue(
                                dataPawnLoanRow[
                                "UPDATEDBY"],
                                ""),
                            LoanStatus =
                            (ProductStatus)
                            Enum.Parse(
                                typeof (ProductStatus),
                                Utilities.GetStringValue(
                                    dataPawnLoanRow[
                                        "STATUS_CD"
                                    ],
                                    "")),
                            StoreNumber =
                            Utilities.GetStringValue(
                                dataPawnLoanRow[
                                "STORENUMBER"],
                                ""),
                            OrgShopState =
                            Utilities.GetStringValue(
                                dataPawnLoanRow[
                                "STATE_CODE"],
                                ""),
                            PfiEligible =
                            Utilities.GetDateTimeValue(
                                dataPawnLoanRow["PFI_ELIG"
                                ],
                                DateTime.MinValue),
                            TicketNumber =
                            Utilities.GetIntegerValue(
                                dataPawnLoanRow[
                                "TICKET_NUMBER"],
                                0),
                            RefNumber =
                            Utilities.GetIntegerValue(
                                dataPawnLoanRow[
                                "REF_NUMBER"],
                                0),
                            RefType = Utilities.GetStringValue(dataPawnLoanRow["REF_TYPE"], ""),
                            ProductType = ProductType.BUY.ToString(),

                            StatusDate =
                            Utilities.GetDateTimeValue(
                                dataPawnLoanRow[
                                "STATUS_DATE"],
                                DateTime.MinValue),
                            TempStatus =
                            (StateStatus)
                            Enum.Parse(typeof (StateStatus),
                                       Utilities.
                                       GetStringValue(
                                           dataPawnLoanRow
                                           [
                                               "TEMP_STATUS"
                                           ]) !=
                                       ""
                                       ? Utilities.
                                       GetStringValue(dataPawnLoanRow
                                                      [
                                                          "TEMP_STATUS"
                                                      ])
                                       : StateStatus.
                                       BLNK.
                                       ToString()),
                            DateMade =
                            Utilities.GetDateTimeValue(
                                dataPawnLoanRow[
                                "DATE_MADE"],
                                DateTime.MaxValue),
                            MadeTime =
                            Utilities.GetTimestampValue(
                                dataPawnLoanRow[
                                "TIME_MADE"]),
                            OriginationDate = Utilities.GetDateTimeValue(dataPawnLoanRow["ORG_DATE"], DateTime.MinValue),
                            UpdatedDate =
                            Utilities.GetDateTimeValue(
                                dataPawnLoanRow[
                                "LASTUPDATEDATE"]),
                            Receipts = new List<Receipt>(),
                            HoldDesc = Utilities.GetStringValue(
                                dataPawnLoanRow["HOLD_DESC"], "")
                        };

                        storedPurchase.OrgShopNumber = storedPurchase.StoreNumber;

                        //Create receipt from receipt details cursor
                        storedPurchase.Receipts = CustomerLoans.CreateReceipt(storedPurchase, outputDataSet.Tables[PAWN_RECEIPTDET_LIST]);

                        List<Item> storedPawnItems = new List<Item>();

                        // Pull from Merchandise List Table
                        if (outputDataSet.Tables[PAWN_MDSE_LIST] != null)
                        {
                            #region nonJewelry
                            // Check for Non_Jewelry Items first
                            //If the purchase header ref type is null then it is PUR then use ticket number 
                            //otherwise use ref number
                            var sMDSEFilter = string.Empty;
                            if (string.IsNullOrEmpty(storedPurchase.RefType))
                            {
                                sMDSEFilter = "STORENUMBER = '" + storedPurchase.StoreNumber + "' ";
                                //sMDSEFilter += " and ICN_STORE = " + Utilities.GetStringValue(dataPawnLoanRow["ICN_STORENUMBER"], "");
                                sMDSEFilter += " and ICN_STORE = " + storedPurchase.StoreNumber;
                                sMDSEFilter += " and ICN_DOC = '" + storedPurchase.TicketNumber + "' ";
                                sMDSEFilter += " and ICN_DOC_TYPE = 2";
                                sMDSEFilter += " and ICN_SUB_ITEM = 0";
                            }
                            else
                            {
                                sMDSEFilter = "STORENUMBER = '" + storedPurchase.StoreNumber + "' ";
                                //sMDSEFilter += " and ICN_STORE = " + Utilities.GetStringValue(dataPawnLoanRow["ICN_STORENUMBER"], "");
                                sMDSEFilter += " and ICN_STORE = " + storedPurchase.StoreNumber;
                                sMDSEFilter += " and ICN_DOC = '" + storedPurchase.RefNumber + "' ";
                                sMDSEFilter += " and ICN_DOC_TYPE = 2";
                                sMDSEFilter += " and ICN_SUB_ITEM = 0";
                            }

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
                                if (storedPawnItem.CategoryCode != 0 && storedPawnItem.CategoryCode <= 1999)
                                    storedPawnItem.IsJewelry = true;
                                else
                                    storedPawnItem.IsJewelry = false;

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
                                    var sCommentFilter = string.Empty;
                                    if (string.IsNullOrEmpty(storedPurchase.RefType))
                                    {
                                        sCommentFilter = "STORENUMBER = '" + storedPurchase.StoreNumber + "' ";
                                        sCommentFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                                        sCommentFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                        sCommentFilter += " and ICN_DOC = '" + storedPurchase.TicketNumber + "' ";
                                        sCommentFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                        sCommentFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                        sCommentFilter += " and ICN_SUB_ITEM = 0";
                                        sCommentFilter += " and MASK_SEQ = 999";
                                    }
                                    else
                                    {
                                        sCommentFilter = "STORENUMBER = '" + storedPurchase.StoreNumber + "' ";
                                        sCommentFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                                        sCommentFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                        sCommentFilter += " and ICN_DOC = '" + storedPurchase.RefNumber + "' ";
                                        sCommentFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                        sCommentFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                        sCommentFilter += " and ICN_SUB_ITEM = 0";
                                        sCommentFilter += " and MASK_SEQ = 999";
                                    }

                                    DataRow[] dataOtherCommentRows = outputDataSet.Tables[PAWN_OTHERDSC_LIST].Select(sCommentFilter);

                                    if (dataOtherCommentRows.Any())
                                    {
                                        storedPawnItem.Comment = Utilities.GetStringValue(dataOtherCommentRows[0]["OD_DESC"], "");
                                    }
                                    else
                                    {
                                        storedPawnItem.Comment = string.Empty;
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
                                            var sOtherDscFilter = string.Empty;
                                            if (string.IsNullOrEmpty(storedPurchase.RefType))
                                            {
                                                sOtherDscFilter = "STORENUMBER = '" + storedPurchase.StoreNumber + "' ";
                                                sOtherDscFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                                                sOtherDscFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                                sOtherDscFilter += " and ICN_DOC = '" + storedPurchase.TicketNumber + "' ";
                                                sOtherDscFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                                sOtherDscFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                                sOtherDscFilter += " and ICN_SUB_ITEM = 0";
                                                sOtherDscFilter += " and MASK_SEQ = " + iMask.ToString();
                                            }
                                            else
                                            {
                                                sOtherDscFilter = "STORENUMBER = '" + storedPurchase.StoreNumber + "' ";
                                                sOtherDscFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                                                sOtherDscFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                                sOtherDscFilter += " and ICN_DOC = '" + storedPurchase.RefNumber + "' ";
                                                sOtherDscFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                                sOtherDscFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                                sOtherDscFilter += " and ICN_SUB_ITEM = 0";
                                                sOtherDscFilter += " and MASK_SEQ = " + iMask.ToString();
                                            }

                                            DataRow[] dataOtherDScRows = outputDataSet.Tables[PAWN_OTHERDSC_LIST].Select(sOtherDscFilter);

                                            if (dataOtherDScRows.Any())
                                            {
                                                answer.AnswerCode = 999;
                                                answer.AnswerText = Utilities.GetStringValue(dataOtherDScRows[0]["OD_DESC"], "");
                                            }
                                            else
                                            {
                                                answer.AnswerCode = 0;
                                                answer.AnswerText = string.Empty;
                                            }
                                        }
                                        itemAttribute.Answer = answer;
                                    }
                                    if (itemAttribute.Answer.AnswerCode == 999 || itemAttribute.Answer.AnswerCode > 0)
                                        storedPawnItem.Attributes.Add(itemAttribute);
                                }

                                storedPawnItem.TempStatus = (StateStatus)Enum.Parse(typeof(StateStatus),
                                                                                    Utilities.GetStringValue(dataMsdeRow["TEMP_STATUS"]) != string.Empty
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

                                    var sMDHistoryFilter = string.Empty;
                                    if (string.IsNullOrEmpty(storedPurchase.RefType))
                                    {
                                        sMDHistoryFilter = "STORENUMBER = '" + storedPurchase.StoreNumber + "' ";
                                        sMDHistoryFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                                        sMDHistoryFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                        sMDHistoryFilter += " and ICN_DOC = '" + storedPurchase.TicketNumber + "' ";
                                        sMDHistoryFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                        sMDHistoryFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                        sMDHistoryFilter += " and ICN_SUB_ITEM = 0";
                                    }
                                    else
                                    {
                                        sMDHistoryFilter = "STORENUMBER = '" + storedPurchase.StoreNumber + "' ";
                                        sMDHistoryFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                                        sMDHistoryFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                        sMDHistoryFilter += " and ICN_DOC = '" + storedPurchase.RefNumber + "' ";
                                        sMDHistoryFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                        sMDHistoryFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                        sMDHistoryFilter += " and ICN_SUB_ITEM = 0";
                                    }

                                    if (outputDataSet.Tables[PAWN_MDHIST_LIST] != null)
                                    {
                                        DataRow[] dataMDHistoryRows = outputDataSet.Tables[PAWN_MDHIST_LIST].Select(sMDHistoryFilter);

                                        if (dataMDHistoryRows != null && dataMDHistoryRows.Any())
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
                                    storedPawnItem.SelectedProKnowMatch.transitionStatus = TransitionStatus.MAN_MODEL_NO_PROKNOW;
                                }
                                if (outputDataSet.Tables[PAWN_GUN_LIST] != null)
                                {
                                    var sCommentFilter = string.Empty;
                                    if (string.IsNullOrEmpty(storedPurchase.RefType))
                                    {
                                        sCommentFilter = "STORENUMBER = '" + storedPurchase.StoreNumber + "' ";
                                        sCommentFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                                        sCommentFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                        sCommentFilter += " and ICN_DOC = '" + storedPurchase.TicketNumber + "' ";
                                        sCommentFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                        sCommentFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                        sCommentFilter += " and ICN_SUB_ITEM = 0";
                                    }
                                    else
                                    {
                                        sCommentFilter = "STORENUMBER = '" + storedPurchase.StoreNumber + "' ";
                                        sCommentFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                                        sCommentFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                        sCommentFilter += " and ICN_DOC = '" + storedPurchase.RefNumber + "' ";
                                        sCommentFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                        sCommentFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                        sCommentFilter += " and ICN_SUB_ITEM = 0";
                                    }

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

                                var sMDSEJewelryFilter = string.Empty;
                                if (string.IsNullOrEmpty(storedPurchase.RefType))
                                {
                                    sMDSEJewelryFilter = "STORENUMBER = '" + storedPurchase.StoreNumber + "' ";
                                    //sMDSEJewelryFilter += " and ICN_STORE = '" + Utilities.GetStringValue(dataPawnLoanRow["ICN_STORENUMBER"], "") + "' ";
                                    sMDSEJewelryFilter += " and ICN_STORE = '" + storedPurchase.StoreNumber + "' ";
                                    sMDSEJewelryFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                    sMDSEJewelryFilter += " and ICN_DOC = '" + storedPurchase.TicketNumber + "' ";
                                    sMDSEJewelryFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                    sMDSEJewelryFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                    sMDSEJewelryFilter += " and ICN_SUB_ITEM > 0";
                                }
                                else
                                {
                                    sMDSEJewelryFilter = "STORENUMBER = '" + storedPurchase.StoreNumber + "' ";
                                    //sMDSEJewelryFilter += " and ICN_STORE = '" + Utilities.GetStringValue(dataPawnLoanRow["ICN_STORENUMBER"], "") + "' ";
                                    sMDSEJewelryFilter += " and ICN_STORE = '" + storedPurchase.StoreNumber + "' ";
                                    sMDSEJewelryFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                    sMDSEJewelryFilter += " and ICN_DOC = '" + storedPurchase.RefNumber + "' ";
                                    sMDSEJewelryFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                    sMDSEJewelryFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                    sMDSEJewelryFilter += " and ICN_SUB_ITEM > 0";
                                }

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
                                                var sOtherDscFilter = string.Empty;
                                                if (string.IsNullOrEmpty(storedPurchase.RefType))
                                                {
                                                    sOtherDscFilter = "STORENUMBER = '" + storedPurchase.StoreNumber + "' ";
                                                    sOtherDscFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                                                    sOtherDscFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                                    sOtherDscFilter += " and ICN_DOC = '" + storedPurchase.TicketNumber + "' ";
                                                    sOtherDscFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                                    sOtherDscFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                                    sOtherDscFilter += " and ICN_SUB_ITEM = " + Utilities.GetIntegerValue(dataMsdeJewelryRow["ICN_SUB_ITEM"], 0).ToString();
                                                    sOtherDscFilter += " and MASK_SEQ = " + iMask.ToString();
                                                }
                                                else
                                                {
                                                    sOtherDscFilter = "STORENUMBER = '" + storedPurchase.StoreNumber + "' ";
                                                    sOtherDscFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                                                    sOtherDscFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                                    sOtherDscFilter += " and ICN_DOC = '" + storedPurchase.RefNumber + "' ";
                                                    sOtherDscFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                                    sOtherDscFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                                    sOtherDscFilter += " and ICN_SUB_ITEM = " + Utilities.GetIntegerValue(dataMsdeJewelryRow["ICN_SUB_ITEM"], 0).ToString();
                                                    sOtherDscFilter += " and MASK_SEQ = " + iMask.ToString();
                                                }

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

                                storedPawnItems.Add(storedPawnItem);
                            }
                            #endregion
                        }
                        if (storedPurchase.Items == null)
                            storedPurchase.Items = new List<Item>();
                        storedPurchase.Items = storedPawnItems;
                        lstPurchases.Add(storedPurchase);
                    }
                }
            }
        }

        public static bool InsertReturnRecord(
            string purchaseType,
            string storeNumber,
            string customerNumber,
            string entityNumber,
            string entityType,
            string pfiElig,
            decimal amount,
            string purchaseStatus,
            string statusDate,
            string statusTime,
            string userID,
            string cashDrawer,
            string orgDate,
            decimal orgAmount,
            Int32 lastLine,
            string purchaseOrderNumber,
            string purchaseRefNumber,
            decimal freight,
            decimal tax,
            int manualTicket,
            string miscFlags,
            string ttyId,
            string shipType,
            string shipNumber,
            string shipComment,
            List<string> returnICN,
            string comments,
            string backgroundCheckNumber,
            string custDispIdNum,
            string custDispIdType,
            string custDispIDCode, 
            out int purchaseTktNumber,
            out string errorCode,
            out string errorText)
        {
            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;
            purchaseTktNumber = 0;

            //Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("InsertReturnRecord Failed",
                                                            new ApplicationException("InsertReturnRecord Failed: Data accessor instance is invalid"));
                return (false);
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_disp_id_type", custDispIdType));
            oParams.Add(new OracleProcParam("p_disp_id_agency", custDispIDCode));
            oParams.Add(new OracleProcParam("p_disp_id_number", custDispIdNum));

            oParams.Add(new OracleProcParam("p_purchase_type", purchaseType));
            oParams.Add(new OracleProcParam("p_purchase_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_purchase_cust_num", customerNumber));
            oParams.Add(new OracleProcParam("p_entity_number", entityNumber));
            oParams.Add(new OracleProcParam("p_entity_type", entityType));
            oParams.Add(new OracleProcParam("p_purchase_pfi_elig", pfiElig));
            oParams.Add(new OracleProcParam("p_purchase_amount", amount));
            oParams.Add(new OracleProcParam("p_purchase_status_cd", purchaseStatus));
            oParams.Add(new OracleProcParam("p_purchase_status_date", statusDate));
            oParams.Add(new OracleProcParam("p_purchase_status_time", statusTime));
            oParams.Add(new OracleProcParam("p_user_id", userID));
            oParams.Add(new OracleProcParam("p_cash_drawer", cashDrawer));

            oParams.Add(new OracleProcParam("p_org_date", orgDate));
            oParams.Add(new OracleProcParam("p_org_amount", orgAmount));
            oParams.Add(new OracleProcParam("p_last_line", lastLine));
            oParams.Add(new OracleProcParam("p_purchase_order_number", purchaseOrderNumber));
            oParams.Add(new OracleProcParam("p_purchase_ref_number", purchaseRefNumber));
            oParams.Add(new OracleProcParam("p_purchase_freight_amt", freight));
            oParams.Add(new OracleProcParam("p_purchase_tax_amt", tax));
            oParams.Add(new OracleProcParam("p_purchase_man_ticket", manualTicket));
            oParams.Add(new OracleProcParam("p_purchase_misc_flags", miscFlags));
            oParams.Add(new OracleProcParam("p_purchase_tty_id", ttyId));
            oParams.Add(new OracleProcParam("p_purchase_ship_type", shipType));
            oParams.Add(new OracleProcParam("p_purchase_ship_number", shipNumber));
            oParams.Add(new OracleProcParam("p_purchase_ship_comment", shipComment));
            oParams.Add(new OracleProcParam("p_icn_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_icn", true, returnICN));
            oParams.Add(new OracleProcParam("p_comments", comments));
            oParams.Add(new OracleProcParam("p_background_check_number", backgroundCheckNumber));
            oParams.Add(new OracleProcParam("o_purchase_ticket_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1)); 

            //Make stored proc call
            bool retVal;
            DataSet outDSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "pawn_purchases", "insert_purchase_return", oParams, null, "o_return_code",
                                                                                            "o_return_text", out outDSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("InsertReturnRecord Failed", oEx);
                errorCode = "InsertReturnRecordFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("InsertReturnRecord Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- InsertReturnRecordFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            //Get output number
            DataTable outputDt = outDSet.Tables["OUTPUT"];
            if (outputDt != null && outputDt.IsInitialized &&
                outputDt.Rows != null && outputDt.Rows.Count > 0)
            {
                DataRow dr = outputDt.Rows[0];
                if (dr != null && dr.ItemArray.Length > 0)
                {
                    object purchaseNumber = dr.ItemArray.GetValue(1);
                    if (purchaseNumber != null)
                    {
                        purchaseTktNumber = Int32.Parse((string)purchaseNumber);

                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }
            return (false);
        }
        /* For test purposes only
        * public static bool GetCustomerPurchases(
        }*/
    }
}
