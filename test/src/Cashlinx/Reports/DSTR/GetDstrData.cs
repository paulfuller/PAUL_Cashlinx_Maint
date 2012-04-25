//***************************************************************
// class_name   :  GetFirearmData
// created by   : rmcbai1
// date created : 1/13/2010 10:54:09 AM
//
// Copyright  2010 Cash America International
//---------------------------------------------------------------
//   description:  this class will gather up all the data
//                 needed for creating the Daily Sales Transaction
//                 Report (096). It will create a dataset that is used
//                 by the DLL that creates the actual PDF file.  
//
//----------------------------------------------------------------
// change history
//
// 12/2/10 Tracy - Added support for DSTR Sections 33, 40 & 41
//****************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Database.DataAccessLayer;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Reports.DSTR
{
    public class GetDstrData
    {
        private string _errCode;
        private string _errTxt;
        private string _storeName;
        private string _storeNum;
        private DataSetOutput _reportData;
        private DateTime _runDate;
        private Credentials _Credentials;

        public string StoreName
        {
            get { return _storeName; }
        }

        public DateTime RunDate
        {
            get { return _runDate; }
            set { _runDate = value; }
        }

        public string StoreNum
        {
            get { return _storeNum; }
        }

        public DataSetOutput ReportData
        {
            get { return _reportData; }
        }
        public string ErrCode
        {
            get { return _errCode; }
        }

        public string ErrTxt
        {
            get { return _errTxt; }
        }

        //***************************************************************
        /// <summary>
        /// One of 2 constructors: This is default contructor
        /// accepting all of the needed parameters for data 
        /// gathering. 
        /// </summary>
        //***************************************************************
        public GetDstrData(DateTime RunDate
                              , string storeNum
                              , Credentials credentials)
        {
            _errTxt = string.Empty;
            _errCode = "OK";
            _runDate = RunDate;
            _storeNum = storeNum;
            _Credentials = credentials;
            _storeName = storeNum;

        }

        //***************************************************************
        /// <summary>
        /// One of 2 constructors: This is one uses todays date
        /// for the rundate
        ///
        /// </summary>
        //***************************************************************
        public GetDstrData(string storeNum, Credentials credentials)
        {
            _errTxt = string.Empty;
            _errCode = "OK";
            _runDate = DateTime.Now;
            _storeNum = storeNum;
            _Credentials = credentials;
            _storeName = storeNum;
        }

        //*********************************************************************
        //** Date created: Tuesday, Jan 19, 2010
        //** Created by  : PAWN\rmcbai1
        //*********************************************************************
        /// <summary>
        ///    build the datasets needed to generate the report
        /// </summary>
        ///
        /// <returns> the data needed to build the report ( DataSet)</returns>
        //*********************************************************************
        public DataSetOutput BuildDataset()
        {
            _reportData = null;

            try
            {
                DataAccessTools dataAccessTools = DataAccessService.CreateDataAccessTools();

                DataAccessService.Connect(_Credentials,
                                          DataAccessTools.ConnectMode.SINGLE,
                                          DataAccessTools.LogMode.DEBUG,
                                          ref dataAccessTools);


                List<OracleProcParam> inParams = new List<OracleProcParam>();

                inParams.Add(new OracleProcParam("store_num", _storeNum));
                string date_in = _runDate.ToString("d");
                inParams.Add(new OracleProcParam("date_in", date_in));

                List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
                refCursors.Add(new PairType<string, string>("o_cash_adv_1", "CASH_ADV_1"));
                refCursors.Add(new PairType<string, string>("o_xpp_2", "XPP_2"));
                refCursors.Add(new PairType<string, string>("o_payment_3", "PAYMENT_3"));
                refCursors.Add(new PairType<string, string>("o_recissions_4", "RECISSIONS_4"));
                refCursors.Add(new PairType<string, string>("o_debit_card_5", "DEBIT_CARDS_5"));
                refCursors.Add(new PairType<string, string>("o_phone_card_6", "PHONE_CARDS_6"));
                refCursors.Add(new PairType<string, string>("o_safe_transfer_7", "SAFE_TRANSFER_7"));
                refCursors.Add(new PairType<string, string>("o_drawer_transfer_7", "DRAWER_TRANSFER_7"));
                refCursors.Add(new PairType<string, string>("o_check_cashing_8", "CHECK_CASHING_8"));
                refCursors.Add(new PairType<string, string>("o_insurance_9", "INSURANCE_9"));
                refCursors.Add(new PairType<string, string>("o_convenience_11", "CONVENIENCE_11"));
                refCursors.Add(new PairType<string, string>("o_tax_prep_12", "TAX_PREP_12"));
                refCursors.Add(new PairType<string, string>("o_other_13", "OTHER_13"));
                refCursors.Add(new PairType<string, string>("o_money_order_14", "MONEY_ORDER_14"));
                refCursors.Add(new PairType<string, string>("o_wire_transfer_15", "WIRE_TRANSFER_15"));
                refCursors.Add(new PairType<string, string>("o_coupons_16", "COUPONS_16"));
                refCursors.Add(new PairType<string, string>("o_petty_cash_17", "PETTY_CASH_17"));
                refCursors.Add(new PairType<string, string>("o_paid_inout_18", "PAID_INOUT18"));
                refCursors.Add(new PairType<string, string>("o_extensions_19", "EXTENSIONS_19"));
                refCursors.Add(new PairType<string, string>("o_cancel_cso_20", "CANCEL_CSO_20"));
                refCursors.Add(new PairType<string, string>("o_ach_rvk_21", "ACH_RVK_21"));
                refCursors.Add(new PairType<string, string>("o_waive_off_22", "WAIVE_OFF_22"));
                refCursors.Add(new PairType<string, string>("o_reimbsmts_23", "REIMBURSEMENTS_23"));
                refCursors.Add(new PairType<string, string>("o_new_loans_24", "NEW_LOANS_24"));
                refCursors.Add(new PairType<string, string>("o_ext_25", "EXT_25"));
                refCursors.Add(new PairType<string, string>("o_renew_26", "RENEW_26"));
                refCursors.Add(new PairType<string, string>("o_paydown_27", "PAYDOWN_27"));
                refCursors.Add(new PairType<string, string>("o_pickup_28", "PICKUP_28"));
                refCursors.Add(new PairType<string, string>("o_seizure_29", "SEIZURE_29"));
                refCursors.Add(new PairType<string, string>("o_police_ret_30", "POLICE_RET_30"));
                refCursors.Add(new PairType<string, string>("o_claim_rel_31", "CLAIM_REL_31"));
                refCursors.Add(new PairType<string, string>("o_pfi_32", "PFI_32"));
                refCursors.Add(new PairType<string, string>("o_transfer_out_33", "TRANSFER_OUT_33"));
                refCursors.Add(new PairType<string, string>("o_purchase_37", "PURCHASE_37"));
                refCursors.Add(new PairType<string, string>("o_return_38", "RETURN_38"));

                refCursors.Add(new PairType<string, string>("O_RETAIL_40", "RETAIL_40"));
                refCursors.Add(new PairType<string, string>("O_RETAIL_40_DETL", "RETAIL_40_DETL"));
                refCursors.Add(new PairType<string, string>("O_LAYAWAY_41", "LAYAWAY_41"));
                refCursors.Add(new PairType<string, string>("O_LAYAWAY_41_DETL", "LAYAWAY_41_DETL"));
                refCursors.Add(new PairType<string, string>("O_SALE_REFUND", "SALE_REFUND"));
                refCursors.Add(new PairType<string, string>("O_SALE_REFUND_DETL", "SALE_REFUND_DETL"));
                refCursors.Add(new PairType<string, string>("O_LAYAWAY_REFUND", "LAYAWAY_REFUND"));
                refCursors.Add(new PairType<string, string>("o_forfeited", "LAYAWAY_TERMATION"));
                refCursors.Add(new PairType<string, string>("o_charge_off", "CHARGE_OFF"));
                refCursors.Add(new PairType<string, string>("O_PARPYMT_45", "O_PARPYMT_45"));
                /* var outParamList = new List<TupleType<string, DataTypeConstants.PawnDataType, int>>(1)
                                         {
                                             new TupleType<string, DataTypeConstants.PawnDataType, int>
                                                     (
                                                         "o_store_name",
                                                         DataTypeConstants.PawnDataType.STRING, 40
                                                     )
                                         };*/
                inParams.Add(new OracleProcParam("o_store_name", OracleDbType.Varchar2, _storeName, ParameterDirection.Output, 100));
                bool retval = DataAccessService.ExecuteStoredProc(_Credentials.DBSchema
                                                    , "PAWN_DSTR"
                                                    , "get_dstr_data"
                                                    , inParams
                                                    , null
                                                    , refCursors
                                                    , "o_error_code"
                                                    , "o_error_text"
                                                    , null
                                                    , out _errCode
                                                    , out _errTxt
                                                    , out _reportData
                                                    , ref dataAccessTools);

                if (!retval)
                {
                    retval = DataAccessService.ExecuteStoredProc(_Credentials.DBSchema
                                                                        , "PAWN_DSTR"
                                                                        , "get_dstr_data"
                                                                        , inParams
                                                                        , null
                                                                        , refCursors
                                                                        , "o_error_code"
                                                                        , "o_error_text"
                                                                        , null
                                                                        , out _errCode
                                                                        , out _errTxt
                                                                        , out _reportData
                                                                        , ref dataAccessTools);
                }


                if (!retval)
                {
                    _errCode = "1";
                    _errTxt = "An error occurred retrieving the report data.  Please try again, if this error persists then contact support.";
                }
            }
            catch (Exception ex)
            {
                _reportData = null;
                _errCode = "1";
                _errTxt = ex.Message;
            }


            return _reportData;
        }
    }
}
