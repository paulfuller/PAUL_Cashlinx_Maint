#region FileHeaderRegion

// /************************************************************************
//  * Namespace: CashlinxDesktop.DesktopProcedures 
//  * Class    : ServiceLoanDBProcedures     
//  * 
//  * Description: Contains wrapper calls to service loan related stored
//  *              procedures.
//  * 
//  * History:
//  * Date                Author            Reason                                 
//  *------------------------------------------------------------------
//  * 09/08/2009          GJL               Initial version
//  * 
//  * **********************************************************************/

#endregion

#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
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

#endregion

namespace Common.Controllers.Database.Procedures
{
    public static class ServiceLoanDBProcedures
    {
        public static readonly string CLAZZ = "ServiceLoanDBProcedures";
        private static readonly string RENEW_LIST = "RenewList";
        private static readonly string EXTEND_LIST = "ExtendList";
        private static readonly string PAYMENT_LIST = "PaymentList";


        public enum ServiceLoanType
        {
            PICKUP,
            RENEWAL,
            EXTENSION,
            POLICESEIZE,
            PAYDOWN,
            PARTPAYMENT
        }

        public static bool ExecuteServicePawnLoan(
            ServiceLoanType serviceType,
            List<PawnLoan> loans,
            CustomerVO customer,
            ProductStatus newStatus,
            string storeNumber,
            string userId,
            DateTime trxDate,
            ref ReceiptDetailsVO receiptData,
            out string errorCode,
            out string errorText)
        {
            //Initialize outputs
            errorCode = string.Empty;
            errorText = string.Empty;

            if (CollectionUtilities.isEmpty(loans))
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans. Loan list is empty.");
                BasicExceptionHandler.Instance.AddException("Cannot service loans. Loan list is empty.",
                    new ApplicationException("Cannot service an empty loan list"));
                return (false);
            }

            if (customer == null)
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans. Customer is invalid");
                BasicExceptionHandler.Instance.AddException("Cannot service loans. Customer is invalid",
                    new ApplicationException("Cannot service loans with an invalid customer"));
                return (false);
            }

            if (string.IsNullOrEmpty(storeNumber))
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans. Store number is invalid");
                BasicExceptionHandler.Instance.AddException("Cannot service loans.  Store number is invalid",
                    new ApplicationException("Cannot service loans with an invalid store number"));
            }

            if (string.IsNullOrEmpty(userId))
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans.  User id is invalid");
                BasicExceptionHandler.Instance.AddException("Cannot service loans.  User id is invalid",
                    new ApplicationException("Cannot service loans with an invalid user id"));
            }

            GlobalDataAccessor desk = GlobalDataAccessor.Instance;
            if (desk == null)// || !desk.IsDataAccessorValid())
            {
                if (FileLogger.Instance.IsLogFatal)
                    FileLogger.Instance.logMessage(LogLevel.FATAL, CLAZZ,
                        "CashlinxDesktopSession and/or its database connection is/are invalid");
                BasicExceptionHandler.Instance.AddException(
                    "Cannot acquire the CashlinxDesktopSession and/or its database connection is invalid",
                    new ApplicationException("CashlinxDesktopSession is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create oracle input parameters
            //OracleProcParam
            /*
             *    PROCEDURE service_pawn_loan (p_loan_number IN pawn_receipt_procs.t_array,
                                p_status_cd IN varchar2,
                                p_store_number IN varchar2,
                                p_customer_number IN varchar2,
                                p_cust_last_name IN varchar2,
                                p_cust_first_name IN varchar2,
                                p_cust_middle_in IN varchar2,
                                p_cust_address IN varchar2,
                                p_cust_city IN varchar2,
                                p_cust_state IN varchar2,
                                p_cust_zip IN varchar2,
                                p_cust_id_num IN varchar2,
                                p_cust_id_type IN varchar2,
                                p_cust_id_agency IN varchar2,
                                p_transaction_type IN varchar2,
                                p_user_id IN varchar2,
                                p_receipt_date IN varchar2,
                                p_ref_date IN pawn_receipt_procs.t_array,
                                p_ref_type IN pawn_receipt_procs.t_array,
                                p_ref_event IN pawn_receipt_procs.t_array,
                                p_ref_amt IN pawn_receipt_procs.t_array,
                                p_ref_store IN pawn_receipt_procs.t_array,
                                p_bckgrnd_ck IN varchar2,
                                p_trx_date IN date,
                                o_receipt_number OUT number,
                                o_return_code OUT number,
                                o_return_text OUT varchar2
   );

   */
            var inParams = new List<OracleProcParam>();

            //Setup input params
            var loansParam = new OracleProcParam(ParameterDirection.Input,
                                                 DataTypeConstants.PawnDataType.LISTSTRING, "p_loan_number",
                                                 loans.Count);
            for (int j = 0; j < loans.Count; ++j)
            {
                loansParam.AddValue(loans[j].TicketNumber);
            }
            inParams.Add(loansParam);
            inParams.Add(new OracleProcParam("p_status_cd", newStatus.ToString()));
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_customer_number", customer.CustomerNumber));
            /* inParams.Add(new OracleProcParam("p_cust_last_name", customer.LastName));
             inParams.Add(new OracleProcParam("p_cust_first_name", customer.FirstName));
             inParams.Add(new OracleProcParam("p_cust_middle_in", customer.MiddleInitial));
             //We are assuming there will be at least one address
             //Since this is a pawn customer and you have to go through the manage pawn app
             //screen before a pawn is created and address cannot be blank there
             AddressVO custAddr = customer.getAddress(0);
             //Same logic as address for identity

             inParams.Add(new OracleProcParam("p_cust_address", custAddr.Address1));
             inParams.Add(new OracleProcParam("p_cust_city", custAddr.City));
             inParams.Add(new OracleProcParam("p_cust_state", custAddr.State_Code));
             inParams.Add(new OracleProcParam("p_cust_zip", custAddr.ZipCode));*/
            IdentificationVO id = customer.getFirstIdentity();
            if (id != null)
            {
                inParams.Add(new OracleProcParam("p_cust_id_num", id.IdValue));
                inParams.Add(new OracleProcParam("p_cust_id_type", id.IdType));
                inParams.Add(new OracleProcParam("p_cust_id_agency", id.IdIssuerCode));
            }
            else
            {
                inParams.Add(new OracleProcParam("p_cust_id_num", ""));
                inParams.Add(new OracleProcParam("p_cust_id_type", ""));
                inParams.Add(new OracleProcParam("p_cust_id_agency", ""));

            }
            //inParams.Add(new OracleProcParam("p_transaction_type", serviceType.ToString()));
            inParams.Add(new OracleProcParam("p_user_id", userId));
            inParams.Add(new OracleProcParam("p_receipt_date", receiptData.ReceiptDate.FormatDate()));
            inParams.Add(new OracleProcParam("p_ref_date", true, receiptData.RefDates));
            inParams.Add(new OracleProcParam("p_ref_time", true, receiptData.RefTimes));
            inParams.Add(new OracleProcParam("p_ref_event", true, receiptData.RefEvents));
            inParams.Add(new OracleProcParam("p_ref_amt", true, receiptData.RefAmounts));
            //inParams.Add(new OracleProcParam("p_ref_store", true, receiptData.RefStores));
            inParams.Add(new OracleProcParam("p_bckgrnd_ck", customer.BackgroundCheckRefNumber));
            //inParams.Add(new OracleProcParam("p_trx_date", ShopDateTime.Instance.ShopDate));

            //Add output param
            inParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value,
                                                  ParameterDirection.Output, 1));

            //Execute stored proc
            DataSet outputSet;
            bool retVal;
            try
            {
                retVal = dA.issueSqlStoredProcCommand("ccsowner",
                    "service_pawn_loans", "service_pawn_loan_pickup", inParams, null, "o_return_code",
                    "o_return_text", out outputSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("Service Pawn Loan Failed", oEx);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription + ": OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteServicePawnLoanFailed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteServicePawnLoan";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            //Get output number
            DataTable outputDt = outputSet.Tables["OUTPUT"];
            if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
            {
                DataRow dr = outputDt.Rows[0];
                if (dr != null && dr.ItemArray.Length > 0)
                {
                    object recNumObj = dr.ItemArray.GetValue(1);
                    if (recNumObj.ToString() != "null")
                    {
                        receiptData.ReceiptNumber = (string)recNumObj;
                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }

            errorCode = "ServicePawnLoan Failed";
            errorText = "ServicePawnLoan Failed - Could not retreive receipt number";
            return (false);
        }


        public static bool ExecuteServicePawnLoanExtend(
            ServiceLoanType serviceType,
            List<PawnLoan> loans,
            CustomerVO customer,
            ProductStatus newStatus,
            List<string> storeNumber,
            string userId,
            DateTime trxDate,
            ref ReceiptDetailsVO receiptData,
            string extensionStore,
            out DataTable extensionData,
            out string errorCode,
            out string errorText)
        {
            //Initialize outputs
            errorCode = string.Empty;
            errorText = string.Empty;
            extensionData = null;

            List<string> newDueDate = new List<string>();
            List<string> newPFIDate = new List<string>();
            List<string> newPFINoteDate = new List<string>();
            List<string> newMadeDate = new List<string>();
            List<string> mdseICN = new List<string>();

            if (CollectionUtilities.isEmpty(loans))
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans. Loan list is empty.");
                BasicExceptionHandler.Instance.AddException("Cannot service loans. Loan list is empty.",
                    new ApplicationException("Cannot service an empty loan list"));
                return (false);
            }

            if (customer == null)
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans. Customer is invalid");
                BasicExceptionHandler.Instance.AddException("Cannot service loans. Customer is invalid",
                    new ApplicationException("Cannot service loans with an invalid customer"));
                return (false);
            }

            if (CollectionUtilities.isEmpty(storeNumber))
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans. Store number is invalid");
                BasicExceptionHandler.Instance.AddException("Cannot service loans.  Store number is invalid",
                    new ApplicationException("Cannot service loans with an invalid store number"));
            }

            if (string.IsNullOrEmpty(userId))
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans.  User id is invalid");
                BasicExceptionHandler.Instance.AddException("Cannot service loans.  User id is invalid",
                    new ApplicationException("Cannot service loans with an invalid user id"));
            }

            GlobalDataAccessor desk = GlobalDataAccessor.Instance;
            if (desk == null)// || !desk.IsDataAccessorValid())
            {
                if (FileLogger.Instance.IsLogFatal)
                    FileLogger.Instance.logMessage(LogLevel.FATAL, CLAZZ,
                        "CashlinxDesktopSession and/or its database connection is/are invalid");
                BasicExceptionHandler.Instance.AddException(
                    "Cannot acquire the CashlinxDesktopSession and/or its database connection is invalid",
                    new ApplicationException("CashlinxDesktopSession is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;


            var inParams = new List<OracleProcParam>();

            //Setup input params
            var loansParam = new OracleProcParam(ParameterDirection.Input,
                                                 DataTypeConstants.PawnDataType.LISTSTRING, "p_loan_number",
                                                 loans.Count);
            //Manual ticket is needed only for manual entry
            //but the SP expects it to have as many elements as loan number
            var manTicketParam = new OracleProcParam(ParameterDirection.Input,
                                                 DataTypeConstants.PawnDataType.LISTSTRING, "p_man_ticket",
                                                 loans.Count);
            for (int j = 0; j < loans.Count; ++j)
            {
                loansParam.AddValue(loans[j].TicketNumber);
                manTicketParam.AddValue("");
                if (loans[j].NewDueDate != DateTime.MaxValue)
                    newDueDate.Add(loans[j].NewDueDate.FormatDate());
                if (loans[j].NewPfiEligible != DateTime.MaxValue)
                    newPFIDate.Add(loans[j].NewPfiEligible.FormatDate());
                if (loans[j].NewMadeDate != DateTime.MaxValue)
                    newMadeDate.Add(loans[j].NewMadeDate.FormatDate());
                if (loans[j].NewPfiNote != DateTime.MaxValue)
                    newPFINoteDate.Add(loans[j].NewPfiNote.FormatDate());

                foreach (Item pItem in loans[j].Items)
                    mdseICN.Add(pItem.Icn.ToString());

            }


            inParams.Add(manTicketParam);

            inParams.Add(new OracleProcParam("p_store_number", true, storeNumber));
            inParams.Add(new OracleProcParam("p_customer_number", customer.CustomerNumber));

            inParams.Add(new OracleProcParam("p_user_id", userId));
            inParams.Add(new OracleProcParam("p_status_cd", newStatus.ToString()));
            inParams.Add(new OracleProcParam("p_new_made", true, newMadeDate));
            inParams.Add(new OracleProcParam("p_new_due", true, newDueDate));
            inParams.Add(new OracleProcParam("p_new_pfi", true, newPFIDate));
            inParams.Add(new OracleProcParam("p_new_pfi_note", true, newPFINoteDate));
            inParams.Add(new OracleProcParam("p_tran_date", ShopDateTime.Instance.ShopDate));
            inParams.Add(new OracleProcParam("p_receipt_date", receiptData.ReceiptDate.FormatDate()));
            inParams.Add(loansParam);
            inParams.Add(new OracleProcParam("p_ref_date", true, receiptData.RefDates));
            inParams.Add(new OracleProcParam("p_ref_time", true, receiptData.RefTimes));
            inParams.Add(new OracleProcParam("p_ref_event", true, receiptData.RefEvents));
            inParams.Add(new OracleProcParam("p_ref_amt", true, receiptData.RefAmounts));
            inParams.Add(new OracleProcParam("p_mdse_icn", true, mdseICN));
            string tranTime=ShopDateTime.Instance.ShopDate.ToShortDateString() + " " + 
                ShopDateTime.Instance.ShopTime.ToString();
            inParams.Add(new OracleProcParam("p_status_time", Utilities.GetDateTimeValue(tranTime,DateTime.Now),OracleProcParam.TimeStampType.TIMESTAMP_TZ));
            inParams.Add(new OracleProcParam("p_extend_store", extensionStore));
            //Add output param
            inParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value,
                                                  ParameterDirection.Output, 1));

            var refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("o_extend_list", EXTEND_LIST));

            //Execute stored proc
            DataSet outputSet;
            bool retVal;
            try
            {
                retVal = dA.issueSqlStoredProcCommand("ccsowner",
                    "service_pawn_loans", "service_pawn_loan_extend", inParams, refCursArr, "o_return_code",
                    "o_return_text", out outputSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("Service Pawn Loan Extend Failed", oEx);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription + ": OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteServicePawnLoanExtendFailed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteServicePawnLoanExtend";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }
            //Get extend data
            if (outputSet != null && outputSet.IsInitialized)
            {
                if (outputSet.Tables != null && outputSet.Tables.Count > 0)
                {
                    extensionData = outputSet.Tables[EXTEND_LIST];
                }
            }
            //Get output number
            DataTable outputDt = outputSet.Tables["OUTPUT"];
            if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
            {
                DataRow dr = outputDt.Rows[0];
                if (dr != null && dr.ItemArray.Length > 0)
                {
                    object recNumObj = dr.ItemArray.GetValue(1);
                    if (recNumObj.ToString() != "null")
                    {
                        receiptData.ReceiptNumber = (string)recNumObj;
                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }

            errorCode = "ServicePawnLoanExtend Failed";
            errorText = "ServicePawnLoanExtend Failed - Could not retreive receipt number";
            return (false);
        }

        public static bool ExecuteServicePawnLoanRenewPaydown(
            ServiceTypes serviceType,
            List<string> oldLoanNumber,
            List<string> pawnAppId,
            List<PawnLoan> newloans,
            string customerNumber,
            List<string> storeNumber,
            string storeNumMake,
            string userId,
            DateTime trxDate,
            string ttyId,
            string cashDrawer,
            ref ReceiptDetailsVO receiptData,
            out DataTable ServedLoans,
            out int receiptNumber,
            out string errorCode,
            out string errorText)
        {
            //Initialize outputs
            errorCode = string.Empty;
            errorText = string.Empty;
            receiptNumber = 0;
            ServedLoans = null;

            if (serviceType != ServiceTypes.RENEW && serviceType != ServiceTypes.PAYDOWN)
            {
                BasicExceptionHandler.Instance.AddException("Invalid service type passed",
                    new ApplicationException("Cannot service this service type"));
                return (false);

            }
            DateTime dueDate;
            DateTime pfiEligDate;
            DateTime pfiNoteDate;
            List<string> amount = new List<string>();
            List<string> int_pct = new List<string>();
            List<string> fin_chg = new List<string>();
            List<string> serv_chg = new List<string>();
            List<string> other_chg = new List<string>();
            List<string> status_cd = new List<string>();
            List<string> last_line = new List<string>();
            List<string> user_id = new List<string>();
            List<string> print_notice = new List<string>();
            List<string> gun_involved = new List<string>();
            List<string> clothing = new List<string>();
            List<string> fin_negot = new List<string>();
            List<string> srv_negot = new List<string>();
            List<string> cash_drawer = new List<string>();
            List<string> misc_flags = new List<string>();
            List<string> tty_id = new List<string>();
            List<string> ppd_cityfee = new List<string>();
            List<string> term_fin = new List<string>();
            List<string> storage_pd = new List<string>();
            List<string> procfee_pd = new List<string>();
            List<string> extauto_pd = new List<string>();

            if (CollectionUtilities.isEmpty(newloans))
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans. Loan list is empty.");
                BasicExceptionHandler.Instance.AddException("Cannot service loans. Loan list is empty.",
                    new ApplicationException("Cannot service an empty loan list"));
                return (false);
            }


            if (CollectionUtilities.isEmpty(storeNumber))
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans. Store number is invalid");
                BasicExceptionHandler.Instance.AddException("Cannot service loans.  Store number is invalid",
                    new ApplicationException("Cannot service loans with an invalid store number"));
            }

            if (string.IsNullOrEmpty(userId))
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans.  User id is invalid");
                BasicExceptionHandler.Instance.AddException("Cannot service loans.  User id is invalid",
                    new ApplicationException("Cannot service loans with an invalid user id"));
            }

            GlobalDataAccessor desk = GlobalDataAccessor.Instance;
            if (desk == null)// || !desk.IsDataAccessorValid())
            {
                if (FileLogger.Instance.IsLogFatal)
                    FileLogger.Instance.logMessage(LogLevel.FATAL, CLAZZ,
                        "CashlinxDesktopSession and/or its database connection is/are invalid");
                BasicExceptionHandler.Instance.AddException(
                    "Cannot acquire the CashlinxDesktopSession and/or its database connection is invalid",
                    new ApplicationException("CashlinxDesktopSession is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;


            var inParams = new List<OracleProcParam>();

            //Setup input params
            //Manual ticket is needed only for manual entry
            //but the SP expects it to have as many elements as loan number
            var manTicketParam = new OracleProcParam(ParameterDirection.Input,
                                                 DataTypeConstants.PawnDataType.LISTSTRING, "p_man_ticket",
                                                 newloans.Count);
            for (int j = 0; j < newloans.Count; ++j)
            {
                PawnLoan curPawnLoan = newloans[j];
                decimal dPrePaidCityFee = curPawnLoan.Fees.Where
                (f => f.FeeType == FeeTypes.PREPAID_CITY).Sum(ppf => ppf.Value);

                decimal dStorageFee = curPawnLoan.Fees.Where
                (f => f.FeeType == FeeTypes.STORAGE).Sum(sf => sf.Value);

                manTicketParam.AddValue("");
                amount.Add(curPawnLoan.Amount.ToString());
                int_pct.Add(curPawnLoan.InterestRate.ToString());
                fin_chg.Add("0"); 
                serv_chg.Add(curPawnLoan.ServiceCharge.ToString());
                other_chg.Add("0");
                if (serviceType == ServiceTypes.RENEW)
                {
                    status_cd.Add(ProductStatus.RN.ToString());
                }
                else
                {
                    status_cd.Add(ProductStatus.PD.ToString());
                }
                last_line.Add(curPawnLoan.Items.Count.ToString());
                user_id.Add(userId);
                print_notice.Add(newloans[j].PrintNotice.ToString());
                gun_involved.Add(newloans[j].GunInvolved ? "Y":"N");
                clothing.Add(""); 
                fin_negot.Add(newloans[j].NegotiableFinanceCharge.ToString());
                srv_negot.Add(newloans[j].NegotiableServiceCharge.ToString());
                cash_drawer.Add(cashDrawer);
                misc_flags.Add("");
                tty_id.Add(ttyId);
                ppd_cityfee.Add(dPrePaidCityFee.ToString()); 
                term_fin.Add(newloans[j].InterestAmount.ToString());
                storage_pd.Add(dStorageFee.ToString());
                procfee_pd.Add("0.0");
                extauto_pd.Add("0.0");



            }
            dueDate = newloans[0].DueDate;
            pfiEligDate = newloans[0].PfiEligible;
            pfiNoteDate = newloans[0].PfiNote;

            OracleProcParam.TimeStampType tsType = OracleProcParam.TimeStampType.TIMESTAMP_TZ;
            inParams.Add(new OracleProcParam("p_loan_number", true, oldLoanNumber));
            inParams.Add(new OracleProcParam("p_pwnapp_id", true, pawnAppId));
            inParams.Add(new OracleProcParam("p_store_number", true, storeNumber));
            inParams.Add(new OracleProcParam("p_store_num_make", storeNumMake));
            inParams.Add(new OracleProcParam("p_cust_num", customerNumber));
            inParams.Add(new OracleProcParam("p_made_date", trxDate));
            inParams.Add(new OracleProcParam("p_made_time", trxDate, tsType));
            inParams.Add(new OracleProcParam("p_due_date", dueDate));
            inParams.Add(new OracleProcParam("p_pfi_elig", pfiEligDate));
            inParams.Add(new OracleProcParam("p_pfi_note", pfiNoteDate));
            inParams.Add(new OracleProcParam("p_amount", true, amount));
            inParams.Add(new OracleProcParam("p_int_pct", true, int_pct));
            inParams.Add(new OracleProcParam("p_fin_chg", true, fin_chg));
            inParams.Add(new OracleProcParam("p_serv_chg", true, serv_chg));
            inParams.Add(new OracleProcParam("p_other_chg", true, other_chg));
            inParams.Add(new OracleProcParam("p_status_cd", true, status_cd));
            inParams.Add(new OracleProcParam("p_last_line", true, last_line));
            inParams.Add(new OracleProcParam("p_user_id", true, user_id));
            inParams.Add(new OracleProcParam("p_print_notice", true, print_notice));
            inParams.Add(new OracleProcParam("p_gun_involved", true, gun_involved));
            inParams.Add(new OracleProcParam("p_clothing", true, clothing));
            inParams.Add(new OracleProcParam("p_fin_negot", true, fin_negot));
            inParams.Add(new OracleProcParam("p_srv_negot", true, srv_negot));
            inParams.Add(new OracleProcParam("p_cash_drawer", true, cash_drawer));
            inParams.Add(new OracleProcParam("p_misc_flags", true, misc_flags));
            inParams.Add(new OracleProcParam("p_tty_id", true, tty_id));
            inParams.Add(new OracleProcParam("p_ppd_cityfee", true, ppd_cityfee));
            inParams.Add(new OracleProcParam("p_term_fin", true, term_fin));
            inParams.Add(new OracleProcParam("p_storage_pd", true, storage_pd));
            inParams.Add(new OracleProcParam("p_procfee_pd", true, procfee_pd));
            inParams.Add(new OracleProcParam("p_extauto_pd", true, extauto_pd));
            inParams.Add(new OracleProcParam("p_ref_date", true, receiptData.RefDates));
            inParams.Add(new OracleProcParam("p_ref_time", true, receiptData.RefTimes));
            inParams.Add(new OracleProcParam("p_ref_event", true, receiptData.RefEvents));
            inParams.Add(new OracleProcParam("p_ref_amt", true, receiptData.RefAmounts));
            inParams.Add(new OracleProcParam("p_created_by", userId));
            if (serviceType == ServiceTypes.RENEW)
            {
                inParams.Add(new OracleProcParam("p_call_flag", "RN"));
            }
            else if (serviceType == ServiceTypes.PAYDOWN)
            {
                inParams.Add(new OracleProcParam("p_call_flag", "PD"));
            }
            //Add output params

            inParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value,
                                                  ParameterDirection.Output, 1));
            var refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("o_renew_list", RENEW_LIST));

            //Execute stored proc
            DataSet outputSet = null;
            bool retVal = false;
            try
            {
                if (serviceType == ServiceTypes.RENEW)
                {
                    retVal = dA.issueSqlStoredProcCommand("ccsowner",
                                                          "service_pawn_loans",
                                                          "service_pawn_loan_renew", inParams,
                                                          refCursArr, "o_return_code",
                                                          "o_return_text", out outputSet);
                }
                else if (serviceType == ServiceTypes.PAYDOWN)
                {
                    retVal = dA.issueSqlStoredProcCommand("ccsowner",
                                                          "service_pawn_loans",
                                                          "service_pawn_loan_paydown", inParams,
                                                          refCursArr, "o_return_code",
                                                          "o_return_text", out outputSet);
                }
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("Service Pawn Loan Renew/Paydown Failed", oEx);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription + ": OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteServicePawnLoanRenewPaydown: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteServicePawnLoanRenewPaydown";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputSet != null && outputSet.IsInitialized)
            {
                if (outputSet.Tables != null && outputSet.Tables.Count > 0)
                {
                    ServedLoans = outputSet.Tables[RENEW_LIST];
                }
            }
            DataTable outputDt = outputSet.Tables["OUTPUT"];
            if (outputDt != null && outputDt.IsInitialized && 
                outputDt.Rows != null && outputDt.Rows.Count > 0)
            {
                //Get output number
                DataRow dr = outputDt.Rows[0];
                if (dr != null && dr.ItemArray.Length > 0)
                {
                    object recNumObj = dr.ItemArray.GetValue(1);
                    if (recNumObj != null && recNumObj.ToString() != "null")
                    {
                        receiptData.ReceiptNumber = (string)recNumObj;
                        receiptNumber = Utilities.GetIntegerValue(recNumObj, 0);
                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }

            errorCode = "ExecuteServicePawnLoanRenewPaydown Failed";
            errorText = "ExecuteServicePawnLoanRenewPaydown Failed - Could not retreive receipt number";
            return (false);
        }


        public static bool ExecuteServicePawnLoanGeneric(
            ServiceLoanType serviceType,
            List<PawnLoan> loans,
            CustomerVO customer,
            ProductStatus newStatus,
            List<string> storeNumber,
            string userId,
            DateTime trxDate,
            List<string> policeInfoArray,
            ref ReceiptDetailsVO receiptData,
            out string errorCode,
            out string errorText)
        {
            //Initialize outputs
            errorCode = string.Empty;
            errorText = string.Empty;

            List<string> newDueDate = new List<string>();
            List<string> newPFIDate = new List<string>();
            List<string> newMadeDate = new List<string>();
            List<string> mdseICN = new List<string>();

            if (CollectionUtilities.isEmpty(loans))
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans. Loan list is empty.");
                BasicExceptionHandler.Instance.AddException("Cannot service loans. Loan list is empty.",
                    new ApplicationException("Cannot service an empty loan list"));
                return (false);
            }

            if (customer == null)
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans. Customer is invalid");
                BasicExceptionHandler.Instance.AddException("Cannot service loans. Customer is invalid",
                    new ApplicationException("Cannot service loans with an invalid customer"));
                return (false);
            }

            if (CollectionUtilities.isEmpty(storeNumber))
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans. Store number is invalid");
                BasicExceptionHandler.Instance.AddException("Cannot service loans.  Store number is invalid",
                    new ApplicationException("Cannot service loans with an invalid store number"));
            }

            if (string.IsNullOrEmpty(userId))
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans.  User id is invalid");
                BasicExceptionHandler.Instance.AddException("Cannot service loans.  User id is invalid",
                    new ApplicationException("Cannot service loans with an invalid user id"));
            }

            GlobalDataAccessor desk = GlobalDataAccessor.Instance;
            if (desk == null)// || !desk.IsDataAccessorValid())
            {
                if (FileLogger.Instance.IsLogFatal)
                    FileLogger.Instance.logMessage(LogLevel.FATAL, CLAZZ,
                        "CashlinxDesktopSession and/or its database connection is/are invalid");
                BasicExceptionHandler.Instance.AddException(
                    "Cannot acquire the CashlinxDesktopSession and/or its database connection is invalid",
                    new ApplicationException("CashlinxDesktopSession is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            var inParams = new List<OracleProcParam>();

            //Setup input params
            var loansParam = new OracleProcParam(ParameterDirection.Input,
                                                 DataTypeConstants.PawnDataType.LISTSTRING, "p_loan_number",
                                                 loans.Count);
            //Manual ticket is needed only for manual entry
            //but the SP expects it to have as many elements as loan number
            var manTicketParam = new OracleProcParam(ParameterDirection.Input,
                                                 DataTypeConstants.PawnDataType.LISTSTRING, "p_man_ticket",
                                                 loans.Count);
            for (int j = 0; j < loans.Count; ++j)
            {
                loansParam.AddValue(loans[j].TicketNumber);
                manTicketParam.AddValue("");
                if (loans[j].NewDueDate != DateTime.MaxValue)
                    newDueDate.Add(loans[j].NewDueDate.FormatDate());
                if (loans[j].NewPfiEligible != DateTime.MaxValue)
                    newPFIDate.Add(loans[j].NewPfiEligible.FormatDate());
                if (loans[j].NewMadeDate != DateTime.MaxValue)
                    newMadeDate.Add(loans[j].NewMadeDate.FormatDate());
                foreach (Item pItem in loans[j].Items)
                    mdseICN.Add(pItem.Icn.ToString());

            }


            inParams.Add(manTicketParam);

            inParams.Add(new OracleProcParam("p_store_number", true, storeNumber));
            inParams.Add(new OracleProcParam("p_customer_number", customer.CustomerNumber));


            IdentificationVO id = customer.getFirstIdentity();
            inParams.Add(new OracleProcParam("p_cust_id_num", id.IdValue));
            inParams.Add(new OracleProcParam("p_cust_id_type", id.IdType));
            inParams.Add(new OracleProcParam("p_cust_id_agency", id.IdIssuerCode));

            inParams.Add(new OracleProcParam("p_user_id", userId));
            inParams.Add(new OracleProcParam("p_status_cd", newStatus.ToString()));
            inParams.Add(new OracleProcParam("p_generic_processname", serviceType.ToString()));
            inParams.Add(new OracleProcParam("p_new_made", true, newMadeDate));
            inParams.Add(new OracleProcParam("p_new_due", true, newDueDate));
            inParams.Add(new OracleProcParam("p_new_pfi", true, newPFIDate));
            inParams.Add(new OracleProcParam("p_police_info_array", true, policeInfoArray));
            //inParams.Add(new OracleProcParam("p_ref_date", true, receiptData.RefDates));
            inParams.Add(new OracleProcParam("p_tran_date", ShopDateTime.Instance.ShopDate));
            inParams.Add(new OracleProcParam("p_bckgrnd_ck", customer.BackgroundCheckRefNumber));
            inParams.Add(new OracleProcParam("p_mdse_icn", true, mdseICN));

            inParams.Add(new OracleProcParam("p_receipt_date", receiptData.ReceiptDate.FormatDate()));
            inParams.Add(loansParam);
            inParams.Add(new OracleProcParam("p_ref_date", true, receiptData.RefDates));
            inParams.Add(new OracleProcParam("p_ref_event", true, receiptData.RefEvents));
            inParams.Add(new OracleProcParam("p_ref_amt", true, receiptData.RefAmounts));
            //inParams.Add(new OracleProcParam("p_ref_store", true, receiptData.RefStores));



            //Add output param
            inParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value,
                                                  ParameterDirection.Output, 1));

            //Execute stored proc
            DataSet outputSet;
            bool retVal;
            try
            {
                retVal = dA.issueSqlStoredProcCommand("ccsowner",
                    "service_pawn_loans", "service_pawn_loan_generic", inParams, null, "o_return_code",
                    "o_return_text", out outputSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("Service Pawn Loan Generic Failed", oEx);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription + ": OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteServicePawnLoanGenericFailed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteServicePawnLoanGeneric";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            //Get output number
            DataTable outputDt = outputSet.Tables["OUTPUT"];
            if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
            {
                DataRow dr = outputDt.Rows[0];
                if (dr != null && dr.ItemArray.Length > 0)
                {
                    object recNumObj = dr.ItemArray.GetValue(1);
                    if (recNumObj.ToString() != "null")
                    {
                        receiptData.ReceiptNumber = (string)recNumObj;
                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                    else
                    {
                        errorCode = "ServicePawnLoanGeneric Failed";
                        errorText = "ServicePawnLoanGeneric Failed - Could not retreive receipt number";
                        return (false);
                    }
                }
            }

            errorCode = "ServicePawnLoanGeneric Failed";
            errorText = "ServicePawnLoanGeneric Failed - Could not retreive receipt number";
            return (false);
        }


        public static bool ExecuteServicePawnLoanPartPayment(
            List<PawnLoan> loans,
            CustomerVO customer,
            string storeNumber,
            string userId,
            string trxDate,
            string trxTime,
            string cashDrawer,
            ref ReceiptDetailsVO receiptData,
            out DataTable dtPayments,
            out string errorCode,
            out string errorText)
        {
            //Initialize outputs
            errorCode = string.Empty;
            errorText = string.Empty;
            dtPayments = null;


            if (CollectionUtilities.isEmpty(loans))
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans. Loan list is empty.");
                BasicExceptionHandler.Instance.AddException("Cannot service loans. Loan list is empty.",
                    new ApplicationException("Cannot service an empty loan list"));
                return (false);
            }


            if (CollectionUtilities.isEmpty(storeNumber))
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans. Store number is invalid");
                BasicExceptionHandler.Instance.AddException("Cannot service loans.  Store number is invalid",
                    new ApplicationException("Cannot service loans with an invalid store number"));
            }

            if (string.IsNullOrEmpty(userId))
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, CLAZZ, "Cannot service loans.  User id is invalid");
                BasicExceptionHandler.Instance.AddException("Cannot service loans.  User id is invalid",
                    new ApplicationException("Cannot service loans with an invalid user id"));
            }

            GlobalDataAccessor desk = GlobalDataAccessor.Instance;
            if (desk == null)
            {
                if (FileLogger.Instance.IsLogFatal)
                    FileLogger.Instance.logMessage(LogLevel.FATAL, CLAZZ,
                        "CashlinxDesktopSession and/or its database connection is/are invalid");
                BasicExceptionHandler.Instance.AddException(
                    "Cannot acquire the CashlinxDesktopSession and/or its database connection is invalid",
                    new ApplicationException("CashlinxDesktopSession is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;


            List<string> paymentAmount= new List<string>();
            List<string> pmtPrinAmount=new List<string>();
            List<string> pmtIntAmount=new List<string>();
            List<string> pmtServAmount=new List<string>();
            List<string> curAmount=new List<string>();
            List<string> curFinCharge=new List<string>();
            List<string> curServCharge=new List<string>();
            List<string> curTermFin=new List<string>();
            List<string> curIntPct=new List<string>();
            



            var inParams = new List<OracleProcParam>();

            //Setup input params
            var loansParam = new OracleProcParam(ParameterDirection.Input,
                                                 DataTypeConstants.PawnDataType.LISTSTRING, "p_ticket_number",
                                                 loans.Count);
            foreach (PawnLoan t in loans)
            {
                loansParam.AddValue(t.TicketNumber);
                PartialPayment partialPaymentRecord = (from ppmt in t.PartialPayments
                                           where ppmt.Status_cde == "New"
                                           select ppmt).FirstOrDefault();
                paymentAmount.Add(partialPaymentRecord.PMT_AMOUNT.ToString());
                pmtPrinAmount.Add(partialPaymentRecord.PMT_PRIN_AMT.ToString());
                pmtIntAmount.Add(partialPaymentRecord.PMT_INT_AMT.ToString());
                pmtServAmount.Add(partialPaymentRecord.PMT_SERV_AMT.ToString());
                curAmount.Add(partialPaymentRecord.CUR_AMOUNT.ToString());
                curFinCharge.Add(partialPaymentRecord.CUR_FIN_CHG.ToString());
                curIntPct.Add(partialPaymentRecord.Cur_Int_Pct.ToString());
                curServCharge.Add(partialPaymentRecord.Cur_Srv_Chg.ToString());
                curTermFin.Add(partialPaymentRecord.Cur_Term_Fin.ToString());
            }
            
            inParams.Add(loansParam);
            inParams.Add(new OracleProcParam("p_cash_drawer", cashDrawer));
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_pmt_amount", true, paymentAmount));
            inParams.Add(new OracleProcParam("p_pmt_prin_amt", true, pmtPrinAmount));
            inParams.Add(new OracleProcParam("p_pmt_int_amt", true, pmtIntAmount));
            inParams.Add(new OracleProcParam("p_pmt_serv_amt", true, pmtServAmount));
            inParams.Add(new OracleProcParam("p_cur_amount", true, curAmount));
            inParams.Add(new OracleProcParam("p_cur_fin_chg", true, curFinCharge));
            inParams.Add(new OracleProcParam("p_cur_serv_chg", true, curServCharge));
            inParams.Add(new OracleProcParam("p_cur_term_fin", true, curTermFin));
            inParams.Add(new OracleProcParam("p_cur_int_pct", true, curIntPct));
            inParams.Add(new OracleProcParam("p_status_cd", "ACT"));
            inParams.Add(new OracleProcParam("p_pmt_type", "PARTP"));
            inParams.Add(new OracleProcParam("p_pmt_doc_type", "1"));
            inParams.Add(new OracleProcParam("p_date_made", trxDate));
            inParams.Add(new OracleProcParam("p_time_made", trxTime));

            
            inParams.Add(new OracleProcParam("p_ref_date", true, receiptData.RefDates));
            inParams.Add(new OracleProcParam("p_ref_time", true, receiptData.RefTimes));
            inParams.Add(new OracleProcParam("p_ref_event", true, receiptData.RefEvents));
            inParams.Add(new OracleProcParam("p_ref_amt", true, receiptData.RefAmounts));
            inParams.Add(new OracleProcParam("p_user_id", userId));
            //Add output param
            inParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value,
                                                  ParameterDirection.Output, 1));

            var refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("o_pmt_list", PAYMENT_LIST));

            //Execute stored proc
            DataSet outputSet;
            bool retVal;
            try
            {
                retVal = dA.issueSqlStoredProcCommand("ccsowner",
                    "service_pawn_loans", "service_pawn_loan_partpayment", inParams, refCursArr, "o_return_code",
                    "o_return_text", out outputSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("Service Pawn Loan Partial Payment Failed", oEx);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription + ": OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteServicePawnLoanPartPaymentFailed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteServicePawnLoanPartialPayment";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }
            //Get payment id list for each ticket
            if (outputSet != null && outputSet.IsInitialized)
            {
                if (outputSet.Tables.Count > 0)
                {
                    dtPayments = outputSet.Tables[PAYMENT_LIST];
                }
            }
            //Get output number
            DataTable outputDt = outputSet.Tables["OUTPUT"];
            if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
            {
                DataRow dr = outputDt.Rows[0];
                if (dr != null && dr.ItemArray.Length > 0)
                {
                    object recNumObj = dr.ItemArray.GetValue(1);
                    if (recNumObj.ToString() != "null")
                    {
                        receiptData.ReceiptNumber = (string)recNumObj;
                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }

            errorCode = "ServicePawnLoanPartPayment Failed";
            errorText = "ServicePawnLoanPartPayment Failed - Could not retreive receipt number";
            return (false);
        }



    }
}
