using System;
using System.Collections.Generic;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;
using System.Data;

namespace Common.Controllers.Database.Procedures
{
    public static class ProcessTenderProcedures
    {
        public enum ProcessTenderMode
        {
            INITIALIZED,
            NEWLOAN,
            VOIDLOAN,
            SERVICELOAN,
            PURCHASE,
            RETURNBUY,
            VOIDBUY,
            VENDORPURCHASE,
            RETAILSALE,
            RETAILREFUND,
            RETAILVOID,
            RETAILVOIDREFUND,
            RETURNSALE,
            LAYAWAY,
            LAYPAYMENT,
            LAYPICKUP,
            LAYAWAYVOID,
            LAYPAYMENTREF,
            LAYPAYMENTVOID,
            LAYPAYMENTREFVOID,
            LAYFORFVOID
        }

        public static readonly int MASK_SIZE = 15;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="feeType"></param>
        /// <param name="feeRefType"></param>
        /// <param name="ticketNumber"></param>
        /// <param name="storeNumber"></param>
        /// <param name="amount"></param>
        /// <param name="userId"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorDesc"></param>
        /// <returns></returns>
        /*    public static bool ExecuteInsertPawnFee(
        List<string> feeType,
        string feeRefType,
        int ticketNumber,
        string storeNumber,
        List<string> amount,
        List<string> orgAmount,
        List<string> isProrated,
        List<string> feeDates,
        List<string> feeStateCodes,
        string userId,
        out string errorCode,
        out string errorDesc)
        {
        errorCode = string.Empty;
        errorDesc = string.Empty;

        if (feeType.Count == 0 ||
        amount.Count == 0 ||
        ticketNumber == 0 ||
        storeNumber==string.Empty || 
        string.IsNullOrEmpty(userId))
        {
        errorCode = "ExecuteInsertPawnFee";
        errorDesc = "ExecuteInsertPawnFee invalid parameters";
        return (false);    
        }

        if (!CashlinxDesktopSession.Instance.IsDataAccessorValid())
        {
        errorCode = "ExecuteInsertPawnFee";
        errorDesc = "ExecuteInsertPawnFee data accessor is invalid";
        return (false);
        }

        OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
        var inParams = new List<OracleProcParam>();
        OracleProcParam feeTypeParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_fee_type", feeType.Count);
        foreach (string s in feeType)
        {
        feeTypeParam.AddValue(s);
        }
        inParams.Add(feeTypeParam);

        inParams.Add(new OracleProcParam("p_fee_ref", ticketNumber));

        inParams.Add(new OracleProcParam("p_fee_ref_type", feeRefType));
            
        inParams.Add(new OracleProcParam("p_store_number", storeNumber));

        inParams.Add(new OracleProcParam("p_fee_amount",true,amount));

        inParams.Add(new OracleProcParam("p_fee_original_amount", true, orgAmount));

        inParams.Add(new OracleProcParam("p_fee_is_prorated", true, isProrated));

        inParams.Add(new OracleProcParam("p_fee_dates", true, feeDates));

        inParams.Add(new OracleProcParam("p_fee_state_code", true, feeStateCodes));

        //inParams.Add(new OracleProcParam("p_fee_group_code", true, feeGroupCodes));

        inParams.Add(new OracleProcParam("p_user_id", userId));

        bool retVal = false;
        try
        {
        DataSet dataSet;
        retVal = dA.issueSqlStoredProcCommand(
        "ccsowner",
        "SERVICE_PAWN_LOANS",
        "insert_pawn_fee",
        inParams,
        null,
        "o_return_code",
        "o_return_text",
        out dataSet);
        }
        catch (OracleException oEx)
        {
        BasicExceptionHandler.Instance.AddException("ExecuteInsertPawnFee Failed", oEx);
        errorCode = dA.ErrorCode + ": ExecuteInsertPawnFeeFailed";
        errorDesc = dA.ErrorDescription + ": OracleException thrown: " + oEx.Message;
        return (false);
        }

        if (retVal == false)
        {
        BasicExceptionHandler.Instance.AddException("ExecuteInsertPawnFee Failed: return value is false", new ApplicationException());
        errorCode = dA.ErrorCode + " --- ExecuteInsertPawnFailed";
        errorDesc = dA.ErrorDescription + " -- Return value is false";
        return (false);
        }

        errorCode = "0";
        errorDesc = "Success";
        return (true);
        }*/

        /// <summary>
        /// Call to update or insert fee
        /// </summary>
        /// <param name="feeType"></param>
        /// <param name="feeRefType"></param>
        /// <param name="ticketNumber"></param>
        /// <param name="storeNumber"></param>
        /// <param name="amount"></param>
        /// <param name="orgAmount"></param>
        /// <param name="isProrated"></param>
        /// <param name="feeDates"></param>
        /// <param name="feeStateCodes"></param>
        /// <param name="userId"></param>
        /// <param name="feeRevOpCode"></param>
        /// <param name="receiptNumber"></param>/// 
        /// <param name="errorCode"></param>
        /// <param name="errorDesc"></param>
        /// <returns></returns>
        public static bool ExecuteInsertUpdatePawnFee(
            List<string> feeType,
            string feeRefType,
            int ticketNumber,
            string storeNumber,
            List<string> amount,
            List<string> orgAmount,
            List<string> isProrated,
            List<string> feeDates,
            List<string> feeStateCodes,
            string userId,
            string feeRevOpCode,
            long receiptNumber,
            out string errorCode,
            out string errorDesc)
        {
            errorCode = string.Empty;
            errorDesc = string.Empty;

            if (feeType.Count == 0 ||
                amount.Count == 0 ||
                ticketNumber == 0 ||
                string.IsNullOrEmpty(userId))
            {
                errorCode = "ExecuteInsertUpdatePawnFee";
                errorDesc = "ExecuteInsertUpdatePawnFee invalid parameters";
                return (false);
            }

            /*if (!CashlinxDesktopSession.Instance.IsDataAccessorValid())
            {
                errorCode = "ExecuteInsertUpdatePawnFee";
                errorDesc = "ExecuteInsertUpdatePawnFee data accessor is invalid";
                return (false);
            }*/

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            var inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_fee_ref", ticketNumber));

            inParams.Add(new OracleProcParam("p_fee_store", storeNumber));

            inParams.Add(new OracleProcParam("p_fee_types", true, feeType));

            inParams.Add(new OracleProcParam("p_fee_ref_type", feeRefType));

            inParams.Add(new OracleProcParam("p_fee_amount", true, amount));

            inParams.Add(new OracleProcParam("p_fee_original_amount", true, orgAmount));

            inParams.Add(new OracleProcParam("p_fee_state_code", true, feeStateCodes));
            
            inParams.Add(new OracleProcParam("p_fee_is_prorated", true, isProrated));

            inParams.Add(new OracleProcParam("p_fee_date", true, feeDates));

            inParams.Add(new OracleProcParam("p_user_id", userId));

            inParams.Add(new OracleProcParam("p_fee_op_rev_code", feeRevOpCode));

            inParams.Add(new OracleProcParam("p_receipt_number", receiptNumber));

            bool retVal = false;
            try
            {
                DataSet dataSet;
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner",
                    "SERVICE_PAWN_LOANS",
                    "update_insert_fees",
                    inParams,
                    null,
                    "o_return_code",
                    "o_return_text",
                    out dataSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertUpdatePawnFee Failed", oEx);
                errorCode = dA.ErrorCode + ": ExecuteInsertUpdatePawnFeeFailed";
                errorDesc = dA.ErrorDescription + ": OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertUpdatePawnFee Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- ExecuteInsertUpdatePawnFeeFailed";
                errorDesc = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            errorCode = "0";
            errorDesc = "Success";
            return (true);
        }

        /*    public static bool ExecuteUpdatePawnFee(
        List<string> feeType,
        string feeRefType,
        int ticketNumber,
        string storeNumber,
        List<string> amount,
        List<string> orgAmount,
        List<string> isProrated,
        List<string> feeDates,
        List<string> feeStateCodes,
        string userId,
        out string errorCode,
        out string errorDesc)
        {
        errorCode = string.Empty;
        errorDesc = string.Empty;

        if (feeType.Count == 0 ||
        amount.Count == 0 ||
        ticketNumber == 0 ||
        string.IsNullOrEmpty(userId))
        {
        errorCode = "ExecuteUpdatePawnFee";
        errorDesc = "ExecuteUpdatePawnFee invalid parameters";
        return (false);
        }

        if (!CashlinxDesktopSession.Instance.IsDataAccessorValid())
        {
        errorCode = "ExecuteUpdatePawnFee";
        errorDesc = "ExecuteUpdatePawnFee data accessor is invalid";
        return (false);
        }

        OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
        var inParams = new List<OracleProcParam>();

        inParams.Add(new OracleProcParam("p_fee_ref", ticketNumber));

        inParams.Add(new OracleProcParam("p_store_number", storeNumber));

        inParams.Add(new OracleProcParam("p_fee_type", true, feeType));

        inParams.Add(new OracleProcParam("p_fee_ref_type",feeRefType));

        inParams.Add(new OracleProcParam("p_fee_amount", true, amount));

        inParams.Add(new OracleProcParam("p_fee_original_amount", true, orgAmount));

        inParams.Add(new OracleProcParam("p_fee_state_code", true, feeStateCodes));

        //inParams.Add(new OracleProcParam("p_fee_group_code", true, feeGroupCodes));

        inParams.Add(new OracleProcParam("p_fee_is_prorated", true, isProrated));
        inParams.Add(new OracleProcParam("p_fee_dates", true, feeDates));
            
        inParams.Add(new OracleProcParam("p_user_id", userId));
        inParams.Add(new OracleProcParam("p_trx_date",ShopDateTime.Instance.ShopDate,OracleProcParam.TimeStampType.TIMESTAMP_TZ));

        bool retVal = false;
        try
        {
        DataSet dataSet;
        retVal = dA.issueSqlStoredProcCommand(
        "ccsowner",
        "SERVICE_PAWN_LOANS",
        "update_fees",
        inParams,
        null,
        "o_return_code",
        "o_return_text",
        out dataSet);
        }
        catch (OracleException oEx)
        {
        BasicExceptionHandler.Instance.AddException("ExecuteUpdatePawnFee Failed", oEx);
        errorCode = dA.ErrorCode + ": ExecuteUpdatePawnFeeFailed";
        errorDesc = dA.ErrorDescription + ": OracleException thrown: " + oEx.Message;
        return (false);
        }

        if (retVal == false)
        {
        BasicExceptionHandler.Instance.AddException("ExecuteUpdatePawnFee Failed: return value is false", new ApplicationException());
        errorCode = dA.ErrorCode + " --- ExecuteUpdateFeeFailed";
        errorDesc = dA.ErrorDescription + " -- Return value is false";
        return (false);
        }

        errorCode = "0";
        errorDesc = "Success";
        return (true);
        }*/

        /// <summary>
        /// Execute the stored procedure to insert the pawn header for new loans
        /// </summary>
        /// <param name="pawnAppId"></param>
        /// <param name="ticketNum"></param>
        /// <param name="storeNum"></param>
        /// <param name="customerNum"></param>
        /// <param name="madeDate"></param>
        /// <param name="madeTime"></param>
        /// <param name="dueDate"></param>
        /// <param name="pfiElig"></param>
        /// <param name="pfiNote"></param>
        /// <param name="amount"></param>
        /// <param name="interestPercent"></param>
        /// <param name="financeCharge"></param>
        /// <param name="serviceCharge"></param>
        /// <param name="otherCharge"></param>
        /// <param name="statusCd"></param>
        /// <param name="lastLine"></param>
        /// <param name="userId"></param>
        /// <param name="printNotice"></param>
        /// <param name="isGunInvolved"></param>
        /// <param name="clothing"></param>
        /// <param name="financeFeeNegotiate"></param>
        /// <param name="serviceFeeNegotiate"></param>
        /// <param name="cashDrawer"></param>
        /// <param name="miscFlags"></param>
        /// <param name="ttyId"></param>
        /// <param name="prePaidCityFee"></param>
        /// <param name="termFinanced"></param>
        /// <param name="storageFeePaid"></param>
        /// <param name="processFeePaid"></param>
        /// <param name="extendAutoFeePaid"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteInsertNewPawnLoanRecord(
            Int64 pawnAppId, 
            Int64 ticketNum,
            Int32 storeNum,
            string customerNum,
            DateTime madeDate,
            DateTime madeTime,
            DateTime dueDate,
            DateTime pfiElig,
            DateTime pfiNote,
            decimal amount,
            decimal interestPercent,
            decimal financeCharge,
            decimal serviceCharge,
            decimal otherCharge, 
            string statusCd, 
            Int32 lastLine,
            string userId, 
            string printNotice,
            bool isGunInvolved,
            string clothing,
            string financeFeeNegotiate,
            string serviceFeeNegotiate,
            string cashDrawer,
            string miscFlags,
            string ttyId,
            decimal prePaidCityFee,
            decimal termFinanced,
            decimal storageFeePaid,
            decimal processFeePaid,
            decimal extendAutoFeePaid,
            out string errorCode,
            out string errorText)
        {
            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;

            //Validate the string inputs
            /*if (!areStringsValid(
            customerNum, statusCd, userId,
            printNotice, financeFeeNegotiate,
            serviceFeeNegotiate, cashDrawer, 
            ttyId))
            {
            BasicExceptionHandler.Instance.AddException("ExecuteInsertNewPawnLoanRecord Failed", 
            new ApplicationException("ExecuteInsertNewPawnLoanRecord Failed: Inputs are invalid"));
            return (false);
            }*/

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
            OracleProcParam.TimeStampType tsType = OracleProcParam.TimeStampType.TIMESTAMP_TZ;
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_pwnapp_id", pawnAppId));
            oParams.Add(new OracleProcParam("p_ticket_num", ticketNum));
            oParams.Add(new OracleProcParam("p_store_num", storeNum));
            oParams.Add(new OracleProcParam("p_cust_num", customerNum));
            oParams.Add(new OracleProcParam("p_made_date", madeDate));
            oParams.Add(new OracleProcParam("p_made_time", madeTime, tsType));
            oParams.Add(new OracleProcParam("p_due_date", dueDate));
            oParams.Add(new OracleProcParam("p_pfi_elig", pfiElig));
            oParams.Add(new OracleProcParam("p_pfi_note", pfiNote));
            oParams.Add(new OracleProcParam("p_amount", amount));
            oParams.Add(new OracleProcParam("p_int_pct", interestPercent));
            oParams.Add(new OracleProcParam("p_fin_chg", 0));
            oParams.Add(new OracleProcParam("p_serv_chg", serviceCharge));
            oParams.Add(new OracleProcParam("p_other_chg", otherCharge));
            oParams.Add(new OracleProcParam("p_status_cd", statusCd));
            oParams.Add(new OracleProcParam("p_last_line", lastLine));
            oParams.Add(new OracleProcParam("p_user_id", userId));
            oParams.Add(new OracleProcParam("p_print_notice", printNotice));
            oParams.Add(new OracleProcParam("p_gun_involved", (isGunInvolved ? "Y" : "N")));
            oParams.Add(new OracleProcParam("p_clothing", clothing));
            oParams.Add(new OracleProcParam("p_fin_negot", financeFeeNegotiate));
            oParams.Add(new OracleProcParam("p_srv_negot", serviceFeeNegotiate));
            oParams.Add(new OracleProcParam("p_cash_drawer", cashDrawer));
            oParams.Add(new OracleProcParam("p_misc_flags", miscFlags));
            oParams.Add(new OracleProcParam("p_tty_id", ttyId));
            oParams.Add(new OracleProcParam("p_ppd_cityfee", prePaidCityFee));
            oParams.Add(new OracleProcParam("p_term_fin", termFinanced));
            oParams.Add(new OracleProcParam("p_storage_pd", storageFeePaid));
            oParams.Add(new OracleProcParam("p_procfee_pd", processFeePaid));
            oParams.Add(new OracleProcParam("p_extauto_pd", extendAutoFeePaid));

            //Make stored proc call
            bool retVal;
            try
            {
                DataSet outDSet;
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "pawn_store_procs", "insert_pawnheader_newloan", oParams, null, "o_return_code",
                                                                                            "o_return_text", out outDSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertNewPawnLoanRecord Failed", oEx);
                errorCode = "ExecuteInsertNewPawnLoanRecordFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertNewPawnLoanRecord Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- ExecuteInsertNewPawnLoanRecordFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeNumber"></param>
        /// <param name="icnStore"></param>
        /// <param name="icnYear"></param>
        /// <param name="icnDoc"></param>
        /// <param name="icnDocType"></param>
        /// <param name="icnItem"></param>
        /// <param name="icnSubItem"></param>
        /// <param name="catCode"></param>
        /// <param name="customerNum"></param>
        /// <param name="amount"></param>
        /// <param name="gunNumber"></param>
        /// <param name="manuf"></param>
        /// <param name="model"></param>
        /// <param name="serial"></param>
        /// <param name="weight"></param>
        /// <param name="masks"></param>
        /// <param name="desc"></param>
        /// <param name="proKnowRetail"></param>
        /// <param name="storageFee"></param>
        /// <param name="userId"></param>
        /// <param name="madeDate"></param>
        /// <param name="madeTime"></param>
        /// <param name="entityNumber"></param>
        /// <param name="refType"></param>
        /// <param name="expenseFlag"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static bool ExecuteInsertMDSERecord(
            Int32 storeNumber,
            Int32 icnStore,
            Int32 icnYear,
            Int64 icnDoc,
            string icnDocType,
            Int32 icnItem,
            Int32 icnSubItem,
            Int32 catCode,
            string customerNum,
            decimal amount,
            Int64 gunNumber,
            string manuf,
            string model,
            string serial,
            decimal weight,
            Int64[] masks,
            string desc,
            decimal proKnowRetail,
            decimal storageFee,
            string userId,
            string madeDate,
            string madeTime,
            string entityType,
            string entityNumber,
            string refType,
            string expenseFlag,
            out string errorCode,
            out string errorText)
        {
            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;

            //Validate the string inputs
            /*if (!areStringsValid(
            icnDocType, customerNum, userId,
            manuf, model, serial,
            desc))
            {
            BasicExceptionHandler.Instance.AddException("ExecuteInsertMDSERecord Failed",
            new ApplicationException("ExecuteInsertMDSERecord Failed: Inputs are invalid"));
            return (false);
            }*/

            //Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertMDSERecord Failed",
                                                            new ApplicationException("ExecuteInsertMDSERecord Failed: Data accessor instance is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_icn_store", icnStore));
            oParams.Add(new OracleProcParam("p_icn_year", icnYear));
            oParams.Add(new OracleProcParam("p_icn_doc", icnDoc));
            oParams.Add(new OracleProcParam("p_icn_doc_type", icnDocType));
            oParams.Add(new OracleProcParam("p_icn_item", icnItem));
            oParams.Add(new OracleProcParam("p_icn_subitem", icnSubItem));
            oParams.Add(new OracleProcParam("p_catg", catCode));
            oParams.Add(new OracleProcParam("p_custnum", customerNum));
            oParams.Add(new OracleProcParam("p_amount", amount));
            oParams.Add(new OracleProcParam("p_gun_no", gunNumber));
            oParams.Add(new OracleProcParam("p_manuf", manuf));
            oParams.Add(new OracleProcParam("p_model", model));
            oParams.Add(new OracleProcParam("p_serial", serial));
            oParams.Add(new OracleProcParam("p_weight", weight));
            OracleProcParam maskParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTINT, "p_masks", masks.Length);
            for (int i = 0; i < masks.Length; ++i)
            {
                maskParam.AddValue(masks[i]);
            }
            oParams.Add(maskParam);
            oParams.Add(new OracleProcParam("p_md_desc", desc));
            oParams.Add(new OracleProcParam("p_pkr", proKnowRetail));
            oParams.Add(new OracleProcParam("p_storage_fee", storageFee));
            oParams.Add(new OracleProcParam("p_user_id", userId));
            oParams.Add(new OracleProcParam("p_made_date", madeDate));
            oParams.Add(new OracleProcParam("p_made_time", madeTime));
            oParams.Add(new OracleProcParam("p_entity_type", entityType));
            oParams.Add(new OracleProcParam("p_entity_number", entityNumber));
            oParams.Add(new OracleProcParam("p_ref_type", icnDocType));
            oParams.Add(new OracleProcParam("p_expense_flag", expenseFlag));
            
            //Make stored proc call
            bool retVal;
            try
            {
                DataSet outDSet;
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "pawn_mdse_procs", "insert_mdse_generic", oParams, null, "o_error_code",
                                                                                            "o_error_desc", out outDSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertMDSERecord Failed", oEx);
                errorCode = "ExecuteInsertMDSERecordFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertMDSERecord Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteInsertMDSERecordFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeNumber"></param>
        /// <param name="icnYear"></param>
        /// <param name="icnDoc"></param>
        /// <param name="icnDocType"></param>
        /// <param name="icnItem"></param>
        /// <param name="icnSubItem"></param>
        /// <param name="newStatusCode"></param>
        /// <param name="oldStatusCode"></param>
        /// <param name="proKLow"></param>
        /// <param name="proKHigh"></param>
        /// <param name="proKRetail"></param>
        /// <param name="proCRetail"></param>
        /// <param name="userId"></param>
        /// <param name="madeDate"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteInsertMDHistRecord(
            Int32 storeNumber,
            Int32 icnYear,
            Int64 icnDoc,
            string icnDocType,
            Int32 icnItem,
            Int32 icnSubItem,
            string newStatusCode,
            string oldStatusCode,
            decimal proKLow,
            decimal proKHigh,
            decimal proKRetail,
            decimal proCRetail,
            string userId, 
            DateTime madeDate,
            out string errorCode,
            out string errorText)
        {
            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;

            //Validate the string inputs
            /*if (!areStringsValid(
            icnDocType, statusCode, userId))
            {
            BasicExceptionHandler.Instance.AddException("ExecuteInsertMDHistRecord Failed",
            new ApplicationException("ExecuteInsertMDHistRecord Failed: Inputs are invalid"));
            return (false);
            }*/

            //Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertMDHistRecord Failed",
                                                            new ApplicationException("ExecuteInsertMDHistRecord Failed: Data accessor instance is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_icn_year", icnYear));
            oParams.Add(new OracleProcParam("p_icn_doc", icnDoc));
            oParams.Add(new OracleProcParam("p_icn_doc_type", icnDocType));
            oParams.Add(new OracleProcParam("p_icn_item", icnItem));
            oParams.Add(new OracleProcParam("p_icn_subitem", icnSubItem));
            oParams.Add(new OracleProcParam("p_oldstatus_code", oldStatusCode));
            oParams.Add(new OracleProcParam("p_status_code", newStatusCode));
            oParams.Add(new OracleProcParam("p_prok_low", proKLow));
            oParams.Add(new OracleProcParam("p_prok_high", proKHigh));
            oParams.Add(new OracleProcParam("p_prok_retail", proKRetail));
            oParams.Add(new OracleProcParam("p_proc_retail", proCRetail));
            oParams.Add(new OracleProcParam("p_user_id", userId));
            oParams.Add(new OracleProcParam("p_made_date", madeDate));

            //Make stored proc call
            bool retVal;
            try
            {
                DataSet outDSet;
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "pawn_mdse_procs", "insert_mdhist", oParams, null, "o_error_code",
                                                                                            "o_error_desc", out outDSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertMDHistRecord Failed", oEx);
                errorCode = "ExecuteInsertMDHistRecordFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertMDHistRecord Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteInsertMDHistRecordFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storePeripheralId"></param>
        /// <param name="transactionDate"></param>
        /// <param name="userId"></param>
        /// <param name="mdseId"></param>
        /// <param name="storeNumber"></param>
        /// <param name="icnStore"></param>
        /// <param name="icnYear"></param>
        /// <param name="icnDoc"></param>
        /// <param name="icnDocType"></param>
        /// <param name="icnItem"></param>
        /// <param name="icnSubItem"></param>
        /// <param name="manuf"></param>
        /// <param name="importer"></param>
        /// <param name="serial"></param>
        /// <param name="caliber"></param>
        /// <param name="gunType"></param>
        /// <param name="gunModel"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="middleInitial"></param>
        /// <param name="customerNumber"></param>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zipCode"></param>
        /// <param name="idType"></param>
        /// <param name="idAgency"></param>
        /// <param name="idNumber"></param>
        /// <param name="createDate"></param>
        /// <param name="createUser"></param>
        /// <param name="gunlock"></param>
        /// <param name="gunStatus"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteInsertGunBookRecord(
            string storePeripheralId,
            DateTime transactionDate,
            string userId,
            Int64 mdseId,
            string storeNumber,
            Int32 icnStore,
            Int32 icnYear,
            Int64 icnDoc,
            string icnDocType,
            Int32 icnItem,
            Int32 icnSubItem,
            string manuf,
            string importer,
            string serial,
            string caliber,
            string gunType,
            string gunModel,
            string lastName,
            string firstName,
            string middleInitial,
            string customerNumber,
            string address,
            string city,
            string state,
            string zipCode,
            string idType,
            string idAgency,
            string idNumber,
            DateTime createDate,
            string createUser,
            int gunlock,
            string gunStatus,
            out string errorCode,
            out string errorText)
        {
            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;

            //Validate the string inputs
            /*if (!areStringsValid(
            storePeripheralId, userId, storeNumber, icnDocType, manuf,
            importer, serial, caliber, gunType, gunModel, lastName,
            firstName, middleInitial, customerNumber, address, city,
            state, zipCode, idType, idAgency, idNumber, createUser))
            {
            BasicExceptionHandler.Instance.AddException("ExecuteInsertGunBookRecord Failed",
            new ApplicationException("ExecuteInsertGunBookRecord Failed: Inputs are invalid"));
            return (false);
            }*/

            //Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertGunBookRecord Failed",
                                                            new ApplicationException("ExecuteInsertGunBookRecord Failed: Data accessor instance is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            OracleProcParam.TimeStampType tsType = OracleProcParam.TimeStampType.TIMESTAMP_TZ;
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            if (gunType != null)
            {
                gunType = gunType.Length > 10 ? gunType.Substring(0, 10) : gunType;
            }
            oParams.Add(new OracleProcParam("p_store_peripheral_id", storePeripheralId));
            oParams.Add(new OracleProcParam("p_transaction_date", transactionDate, tsType));
            oParams.Add(new OracleProcParam("p_user_id", userId));
            oParams.Add(new OracleProcParam("p_mdse_id", mdseId));
            oParams.Add(new OracleProcParam("p_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_icn_store", icnStore));
            oParams.Add(new OracleProcParam("p_icn_year", icnYear));
            oParams.Add(new OracleProcParam("p_icn_doc", icnDoc));
            oParams.Add(new OracleProcParam("p_icn_doc_type", icnDocType));
            oParams.Add(new OracleProcParam("p_icn_item", icnItem));
            oParams.Add(new OracleProcParam("p_icn_subitem", icnSubItem));
            oParams.Add(new OracleProcParam("p_manufacturer", manuf));
            oParams.Add(new OracleProcParam("p_importer", importer));
            oParams.Add(new OracleProcParam("p_serial_number", serial));
            oParams.Add(new OracleProcParam("p_caliber", caliber));
            oParams.Add(new OracleProcParam("p_gun_type", gunType));
            oParams.Add(new OracleProcParam("p_gun_model", gunModel));
            oParams.Add(new OracleProcParam("p_last_name", lastName));
            oParams.Add(new OracleProcParam("p_first_name", firstName));
            oParams.Add(new OracleProcParam("p_middle_initial", middleInitial));
            oParams.Add(new OracleProcParam("p_customer_number", customerNumber));
            oParams.Add(new OracleProcParam("p_address", address));
            oParams.Add(new OracleProcParam("p_city", city));
            oParams.Add(new OracleProcParam("p_state", state));
            oParams.Add(new OracleProcParam("p_postal_code", zipCode));
            oParams.Add(new OracleProcParam("p_id_type", idType));
            oParams.Add(new OracleProcParam("p_id_agency", idAgency));
            oParams.Add(new OracleProcParam("p_id_number", idNumber));
            oParams.Add(new OracleProcParam("p_create_date", createDate, tsType));
            oParams.Add(new OracleProcParam("p_create_user", createUser));
            oParams.Add(new OracleProcParam("p_gun_lock", gunlock));
            oParams.Add(new OracleProcParam("p_gun_status", gunStatus));

            //Make stored proc call
            bool retVal;
            try
            {
                DataSet outDSet;
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "pawn_mdse_procs", "insert_gun_book", oParams, null, "o_error_code",
                                                                                            "o_error_desc", out outDSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertGunBookRecord Failed", oEx);
                errorCode = "ExecuteInsertGunBookRecordFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertGunBookRecord Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteInsertGunBookRecordFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            errorCode = "0";
            errorText = "Success";

            return (true);
        }

        public static bool ExecuteUpdateGunBookRecord(
            string loanNumber,
            string storeNumber,
            string statusCd,
            string customerNumber,
            string lastName,
            string firstName,
            string middleInitial,
            string address,
            string city,
            string state,
            string zipCode,
            string idNumber,
            string idType,
            string idAgency,
            string transactionType,
            string userId,
            string transactionDate,
            string transactionTime,
            string backgroundCheck,
            string caliber,
            string importer,
            string serialNumber,
            string model,
            string manufacturer,
            int icnStore,
            int icnYear,
            int icnDoc,
            string icnDocType,
            int icnItem,
            int icnSubItem,
            long gunNumber,
            out string errorCode,
            out string errorText)
        {
            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;

            //Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateGunBookRecord Failed",
                                                            new ApplicationException("ExecuteUpdateGunBookRecord Failed: Data accessor instance is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_loan_number", loanNumber));
            oParams.Add(new OracleProcParam("p_storenumber", storeNumber));
            oParams.Add(new OracleProcParam("p_status_cd", statusCd));
            oParams.Add(new OracleProcParam("p_customer_number", customerNumber));
            oParams.Add(new OracleProcParam("p_cust_last_name", lastName));
            oParams.Add(new OracleProcParam("p_cust_first_name", firstName));
            oParams.Add(new OracleProcParam("p_cust_middle_in", middleInitial));
            oParams.Add(new OracleProcParam("p_cust_address", address));
            oParams.Add(new OracleProcParam("p_cust_city", city));
            oParams.Add(new OracleProcParam("p_cust_state", state));
            oParams.Add(new OracleProcParam("p_cust_zip", zipCode));
            oParams.Add(new OracleProcParam("p_cust_id_number", idNumber));
            oParams.Add(new OracleProcParam("p_cust_id_type", idType));
            oParams.Add(new OracleProcParam("p_cust_id_agency", idAgency));
            oParams.Add(new OracleProcParam("p_transaction_type", transactionType));
            oParams.Add(new OracleProcParam("p_user_id", userId));
            oParams.Add(new OracleProcParam("p_trx_date", transactionDate));
            oParams.Add(new OracleProcParam("p_trx_time", transactionTime));
            oParams.Add(new OracleProcParam("p_bckgrnd_ck", backgroundCheck));
            oParams.Add(new OracleProcParam("p_caliber", caliber));
            oParams.Add(new OracleProcParam("p_importer", importer));
            oParams.Add(new OracleProcParam("p_serial_number", serialNumber));
            oParams.Add(new OracleProcParam("p_model", model));
            oParams.Add(new OracleProcParam("p_manufacturer", manufacturer));
            oParams.Add(new OracleProcParam("p_icn_store", icnStore));
            oParams.Add(new OracleProcParam("p_icn_year", icnYear));
            oParams.Add(new OracleProcParam("p_icn_doc", icnDoc));
            oParams.Add(new OracleProcParam("p_icn_doc_type", icnDocType));
            oParams.Add(new OracleProcParam("p_icn_item", icnItem));
            oParams.Add(new OracleProcParam("p_icn_sub_item", icnSubItem));
            oParams.Add(new OracleProcParam("p_gun_number", gunNumber));

            //Make stored proc call
            bool retVal = false;
            try
            {
                DataSet outDSet;
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "service_pawn_loans", "update_gun_book", oParams, null, "o_error_code",
                                                                                            "o_error_desc", out outDSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateGunBookRecord Failed", oEx);
                errorCode = "ExecuteUpdateGunBookRecordFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateGunBookRecord Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteUpdateGunBookRecordFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            errorCode = "0";
            errorText = string.Empty;

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeNumber"></param>
        /// <param name="icnStore"></param>
        /// <param name="icnYear"></param>
        /// <param name="icnDoc"></param>
        /// <param name="icnDocType"></param>
        /// <param name="icnItem"></param>
        /// <param name="icnSubItem"></param>
        /// <param name="maskSeq"></param>
        /// <param name="odDesc"></param>
        /// <param name="userId"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteInsertOtherDscRecord(
            string storeNumber,
            Int32 icnStore,
            Int32 icnYear,
            Int64 icnDoc,
            string icnDocType,
            Int32 icnItem,
            Int32 icnSubItem,
            Int32 maskSeq,
            string odDesc,
            string userId,
            out string errorCode,
            out string errorText)
        {
            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;

            //Validate the string inputs
            /*if (!areStringsValid(
            icnDocType, odDesc, userId))
            {
            BasicExceptionHandler.Instance.AddException("ExecuteInsertOtherDscRecord Failed",
            new ApplicationException("ExecuteInsertOtherDscRecord Failed: Inputs are invalid"));
            return (false);
            }*/

            //Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertOtherDscRecord Failed",
                                                            new ApplicationException("ExecuteInsertOtherDscRecord Failed: Data accessor instance is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            //OracleProcParam.TimeStampType tsType = OracleProcParam.TimeStampType.TIMESTAMP_TZ;
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_icn_store", icnStore));
            oParams.Add(new OracleProcParam("p_icn_year", icnYear));
            oParams.Add(new OracleProcParam("p_icn_doc", icnDoc));
            oParams.Add(new OracleProcParam("p_icn_doc_type", icnDocType));
            oParams.Add(new OracleProcParam("p_icn_item", icnItem));
            oParams.Add(new OracleProcParam("p_icn_subitem", icnSubItem));
            oParams.Add(new OracleProcParam("p_mask_seq", maskSeq));
            oParams.Add(new OracleProcParam("p_od_desc", odDesc));
            oParams.Add(new OracleProcParam("p_user_id", userId));

            //Make stored proc call
            bool retVal;
            try
            {
                DataSet outDSet;
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "pawn_mdse_procs", "insert_otherdsc", oParams, null, "o_error_code",
                                                                                            "o_error_desc", out outDSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertOtherDscRecord Failed", oEx);
                errorCode = "ExecuteInsertOtherDscRecordFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertOtherDscRecord Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteInsertOtherDscRecordFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeNumber"></param>
        /// <param name="nextType"></param>
        /// <param name="date"></param>
        /// <param name="number"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteGetNextNumber(
            string storeNumber,
            string nextType,
            DateTime date,
            out Int64 number,
            out string errorCode,
            out string errorText)
        {
            //Set output vars
            number = 0;
            errorCode = string.Empty;
            errorText = string.Empty;

            //Validate inputs
            if (string.IsNullOrEmpty(storeNumber) ||
                string.IsNullOrEmpty(nextType))
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetNextNumber Failed", new ApplicationException("Invalid inputs"));
                errorCode = "ExecuteGetNextNumberFailed";
                errorText = "Invalid Inputs";
                return (false);
            }

            //Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetNextNumber Failed",
                                                            new ApplicationException("ExecuteGetNextNumber Failed: Data accessor instance is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            OracleProcParam.TimeStampType tsType = OracleProcParam.TimeStampType.TIMESTAMP_TZ;
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_next_type", nextType));
            oParams.Add(new OracleProcParam("p_date_time", date, tsType));
            oParams.Add(new OracleProcParam("o_next_num", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1)); 

            //Execute stored proc
            DataSet outputSet;
            bool retVal;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "pawn_store_procs", "get_next_num", oParams, null, "o_return_code",
                                                                                            "o_return_text", out outputSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetNextNumber Failed", oEx);
                errorCode = "ExecuteGetNextNumber";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetNextNumber Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteGetNextNumber";
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
                    object nextNumObj = dr.ItemArray.GetValue(1);
                    if (nextNumObj != null)
                    {
                        var nextNumStr = (string)nextNumObj;
                        number = Int64.Parse(nextNumStr);
                        errorCode = "0";
                        errorText = "Success";
                        return(true);
                    }
                }
            }
            
            errorCode = "ExecuteGetNextNumber";
            errorText = "Stored procedure did not return any data";
            return(false);
        }

        public static bool ExecuteInsertReceiptDetails(
            string storeNumber,
            string entId,
            string receiptDate,
            string userId,
            ref ReceiptDetailsVO receiptDetailsVO,
            out string errorCode,
            out string errorText)
        {
            // Initialize the error code and text output values
            errorCode = string.Empty;
            errorText = string.Empty;

            // Initialize the receipt number output
            receiptDetailsVO.ReceiptNumber = "0";

            var inParams = new List<OracleProcParam>();

            // Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertReceiptDetails Failed",
                                                            new ApplicationException("ExecuteInsertReceiptDetails Failed: Data accessor instance is invalid"));
                errorCode = " --- ExecuteInsertReceiptDetailsFailed";
                errorText = " --- Data accessor instance is invalid";
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            inParams.Add(new OracleProcParam("instorenumber", storeNumber));
            inParams.Add(new OracleProcParam("inentid", entId));
            inParams.Add(new OracleProcParam("inreceiptdate", receiptDate));
            inParams.Add(new OracleProcParam("inuserid", userId));

            //Call private static convenience method to populate receipt
            //data
            ProcedureUtilities.addReceiptDetailsToOraParamList(
                ref inParams, 
                "p_ref_date",
                "p_ref_time",
                "p_ref_number", 
                "p_ref_type", 
                "p_ref_event",
                "p_ref_amt", 
                "p_ref_store",
                "o_receipt_number",
                receiptDetailsVO);

            DataSet outputDataSet;
            bool retVal;

            try
            {
                retVal = dA.issueSqlStoredProcCommand("ccsowner",
                                                      "pawn_receipt_procs", "insert_receipt_receipt_details", inParams, null, "o_return_code",
                                                      "o_return_text", out outputDataSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertReceiptDetails Failed", oEx);
                errorCode = " --- ExecuteInsertReceiptDetailsFailed";
                errorText = " --- OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertReceiptDetails Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- ExecuteInsertReceiptDetailsFailed";
                errorText = dA.ErrorDescription + " --- Return value is false";
                return (false);
            }

            if (outputDataSet == null)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertReceiptDetails Failed: No receipt number created", new ApplicationException());
                errorCode = dA.ErrorCode + " --- ExecuteInsertReceiptDetailsFailed";
                errorText = dA.ErrorDescription + " --- Receipt Number Not Created";
                return (false);
            }
            //Get output number
            DataTable outputDt = outputDataSet.Tables["OUTPUT"];
            if (outputDt != null && outputDt.IsInitialized && 
                outputDt.Rows != null && outputDt.Rows.Count > 0)
            {
                DataRow dr = outputDt.Rows[0];
                if (dr != null && dr.ItemArray.Length > 0)
                {
                    object receiptNumberObj = dr.ItemArray.GetValue(1);
                    if (receiptNumberObj != null)
                    {
                        receiptDetailsVO.ReceiptNumber = (string)receiptNumberObj;
                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }

            errorCode = "Error";
            errorText = "NoSuccess";

            return (false);
        }

        /// <summary>
        /// Executes the insert_receipt stored procedure
        /// </summary>
        /// <param name="storeNumber"></param>
        /// <param name="entId"></param>
        /// <param name="receiptDate"></param>
        /// <param name="userId"></param>
        /// <param name="refDates"></param>
        /// <param name="refNumbers"></param>
        /// <param name="refTypes"></param>
        /// <param name="refEvents"></param>
        /// <param name="refAmounts"></param>
        /// <param name="refStores"></param>
        /// <param name="receiptNumber"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>        
        public static bool ExecuteInsertReceiptDetails(
            string storeNumber,
            string entId,
            string receiptDate,
            string userId,
            string[] refDates,
            string[] refTimes,
            string[] refNumbers,
            string[] refTypes,
            string[] refEvents,
            string[] refAmounts,
            string[] refStores,
            out Int64 receiptNumber,
            out string errorCode,
            out string errorText)
        {
            // Initialize the error code and text output values
            errorCode = string.Empty;
            errorText = string.Empty;

            // Initialize the receipt number output
            receiptNumber = 0;

            var inParams = new List<OracleProcParam>();

            // Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertReceiptDetails Failed",
                                                            new ApplicationException("ExecuteInsertReceiptDetails Failed: Data accessor instance is invalid"));
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            inParams.Add(new OracleProcParam("instorenumber", storeNumber));
            inParams.Add(new OracleProcParam("inentid", entId));
            inParams.Add(new OracleProcParam("inreceiptdate", receiptDate));
            inParams.Add(new OracleProcParam("inuserid", userId));

            //Call private static convenience method to populate receipt
            //data
            ProcedureUtilities.addReceiptDetailsToOraParamList(
                ref inParams, "p_ref_date", refDates, "p_ref_time", refTimes,
                "p_ref_number", refNumbers,
                "p_ref_type", refTypes,
                "p_ref_event", refEvents,
                "p_ref_amt", refAmounts,
                "p_ref_store", refStores,
                "o_receipt_number");

            DataSet outputDataSet;
            bool retVal;

            try
            {
                retVal = dA.issueSqlStoredProcCommand("ccsowner",
                                                      "pawn_receipt_procs", "insert_receipt_receipt_details", inParams, null, "o_return_code",
                                                      "o_return_text", out outputDataSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertReceiptDetails Failed", oEx);
                errorCode = " --- ExecuteInsertReceiptDetailsFailed";
                errorText = " --- OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertReceiptDetails Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- ExecuteInsertReceiptDetailsFailed";
                errorText = dA.ErrorDescription + " --- Return value is false";
                return (false);
            }

            if (outputDataSet == null)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertReceiptDetails Failed: No receipt number created", new ApplicationException());
                errorCode = dA.ErrorCode + " --- ExecuteInsertReceiptDetailsFailed";
                errorText = dA.ErrorDescription + " --- Receipt Number Not Created";
                return (false);
            }
            //Get output number
            DataTable outputDt = outputDataSet.Tables["OUTPUT"];
            if (outputDt != null && outputDt.IsInitialized && 
                outputDt.Rows != null && outputDt.Rows.Count > 0)
            {
                DataRow dr = outputDt.Rows[0];
                if (dr != null && dr.ItemArray.Length > 0)
                {
                    object receiptNumberObj = dr.ItemArray.GetValue(1);
                    if (receiptNumberObj != null)
                    {
                        receiptNumber = Int64.Parse((string)receiptNumberObj);

                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }

            errorCode = "0";
            errorText = "Success";

            return (true);
        }

        /// <summary>
        /// Get Receipt Details stored procedure wrapper
        /// </summary>
        /// <param name="refNumber"></param>
        /// <param name="storeNumber"></param>
        /// <param name="refType"></param>
        /// <param name="receiptNumber"></param>
        /// <param name="receiptDetails"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteGetReceiptDetails(
            int refNumber,
            string storeNumber,
            string refType,
            string receiptNumber,
            out DataTable receiptDetails,
            out string errorCode,
            out string errorText)
        {
            // Initialize the error code and text output values
            errorCode = string.Empty;
            errorText = string.Empty;

            receiptDetails = new DataTable();

            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_ref_number", refNumber));
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_ref_type", refType));
            inParams.Add(new OracleProcParam("p_receipt_number", receiptNumber));

            // Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetReceiptDetails Failed",
                                                            new ApplicationException("ExecuteGetReceiptDetails Failed: Data accessor instance is invalid"));
                return (false);
            }

            DataSet outputDataSet;
            bool retVal;
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            var refCursArr = new List<PairType<string, string>>
            {
                new PairType<string, string>("r_receipt_details_list", "receipt")
            };

            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_receipt_procs",
                    "get_receipt_details", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetReceiptDetails Failed", oEx);
                errorCode = " --- ExecuteGetDetailsFailed";
                errorText = " --- OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetReceiptDetails Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteGetReceiptDetailsFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }
            if (outputDataSet == null || !outputDataSet.IsInitialized ||
                (outputDataSet.Tables == null || outputDataSet.Tables.Count <= 0))
            {
                return (false);
            }

            receiptDetails = outputDataSet.Tables["receipt"];
            return (true);
        }
    }
}
