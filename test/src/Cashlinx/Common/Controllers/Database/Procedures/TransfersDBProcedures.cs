using System;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database.Procedures
{
    public static class TransfersDBProcedures
    {
        public static readonly string CLAZZ = "TransfersDBProcedures";

        public static bool ExecuteTransferOutOfStore(
            string storeNumber,
            List<string> mdseicn,
            List<string> p_icn_qty,
            string p_customer_number,
            DateTime g_tx_date,
            DateTime p_mr_date,
            string p_mr_time,
            string p_mr_user,
            string p_mr_desc,
            int p_mr_change,
            string p_mr_type,
            string p_class_code,
            string p_acct_num,
            string p_created_by,
            List<string> p_gun_number,
            List<string> p_gun_type,
            int scrapTransferFacilityNumber,
            int excessTransferFacilityNumber,
            int refurbTransferFacilityNumber,
            string gunFacilityName,
            bool isClxToClx,
            out int o_transfer_ticket_no,
            string carrier,
            out string errorCode,
            out string errorText)
        {
            //Initialize outputs
            errorCode = string.Empty;
            errorText = string.Empty;
            o_transfer_ticket_no = 0;

            if (GlobalDataAccessor.Instance == null ||
                !GlobalDataAccessor.Instance.IsDataAccessorValid())// || !desk.IsDataAccessorValid())
            {
                if (FileLogger.Instance.IsLogFatal)
                    FileLogger.Instance.logMessage(LogLevel.FATAL, CLAZZ,
                        "Data accessor and/or its database connection is/are invalid");
                BasicExceptionHandler.Instance.AddException(
                    "Cannot acquire the data accessor and/or its database connection is invalid",
                    new ApplicationException("GlobalDataAccessor is invalid"));
                errorCode = "ExecuteTransferOutOfStore";
                errorText = "GlobalDataAccessor and/or its database connection is/are invalid";
                return (false);
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            var inParams = new List<OracleProcParam>();

            //Setup input params
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            //will be gun facility name or to shop name
            inParams.Add(new OracleProcParam("p_destination_store", gunFacilityName));
            inParams.Add(new OracleProcParam("p_carrier", carrier));
            inParams.Add(new OracleProcParam("p_tran_scrap_facility", scrapTransferFacilityNumber));
            inParams.Add(new OracleProcParam("p_tran_refrb_facility", refurbTransferFacilityNumber));
            inParams.Add(new OracleProcParam("p_tran_exces_facility", excessTransferFacilityNumber));

            inParams.Add(new OracleProcParam("p_icn", true, mdseicn));
            inParams.Add(new OracleProcParam("p_icn_qty", true, p_icn_qty));
            inParams.Add(new OracleProcParam("p_customer_number", p_customer_number));
            inParams.Add(new OracleProcParam("g_tx_date", g_tx_date));
            inParams.Add(new OracleProcParam("p_mr_date", p_mr_date));
            inParams.Add(new OracleProcParam("p_mr_time",p_mr_time));
            //inParams.Add(new OracleProcParam("p_mr_time", p_mr_time,OracleProcParam.TimeStampType.TIMESTAMP_TZ));
            inParams.Add(new OracleProcParam("p_mr_user", p_mr_user));
            inParams.Add(new OracleProcParam("p_mr_desc", p_mr_desc));
            inParams.Add(new OracleProcParam("p_mr_change", p_mr_change));
            inParams.Add(new OracleProcParam("p_mr_type", p_mr_type));
            inParams.Add(new OracleProcParam("p_class_code", p_class_code));
            inParams.Add(new OracleProcParam("p_acct_num", p_acct_num));
            inParams.Add(new OracleProcParam("p_created_by", p_created_by));
            inParams.Add(new OracleProcParam("p_gun_number", true,p_gun_number));
            inParams.Add(new OracleProcParam("p_gun_type", true,p_gun_type));
            if (isClxToClx)
            {
                inParams.Add(new OracleProcParam("p_destination_store_type","CLXTICKET"));
            }
            else
            {
                inParams.Add(new OracleProcParam("p_destination_store_type", "TOPSTICKET"));
            }
            inParams.Add(new OracleProcParam("o_transfer_ticket_no", OracleDbType.Int64, DBNull.Value, ParameterDirection.Output, 1)); 

            //Execute stored proc
            DataSet outputSet;
            bool retVal;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "Transfers", "transfer_out_of_store", 
                    inParams, null, "o_error_code", "o_error_desc", 
                    out outputSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("Execute Transfer out of store Failed", oEx);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription + ": OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException(
                    "ExecuteTransferOutOfStore: return value is false", 
                    new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteTransferOutOfStore";
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
                    object obj = dr.ItemArray.GetValue(1);
                    if (obj != null)
                    {
                        var nextNumStr = (string)obj;
                        o_transfer_ticket_no = Int32.Parse(nextNumStr);
                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }

            errorCode = "1";
            errorText = "Transfer stored procedure failed";
            return (false);
        }

        //public static bool ExecuteTransferScrapOfStore(
        //string storeNumber,
        //List<string> mdseicn,
        //List<string> p_icn_qty,
        //string p_customer_number,
        //DateTime g_tx_date,
        //DateTime p_mr_date,
        //string p_mr_time,
        //string p_mr_user,
        //string p_mr_desc,
        //int p_mr_change,
        //string p_mr_type,
        //string p_class_code,
        //string p_acct_num,
        //string p_created_by,
        //List<string> p_gun_number,
        //List<string> p_gun_type,
        //    out int o_transfer_ticket_no,
        //out string errorCode,
        //out string errorText)
        //{
        //    //Initialize outputs
        //    errorCode = string.Empty;
        //    errorText = string.Empty;
        //    o_transfer_ticket_no = 0;


        //    CashlinxDesktopSession desk = CashlinxDesktopSession.Instance;
        //    if (desk == null || !desk.IsDataAccessorValid())
        //    {
        //        if (FileLogger.Instance.IsLogFatal)
        //            FileLogger.Instance.logMessage(LogLevel.FATAL, CLAZZ,
        //                "CashlinxDesktopSession and/or its database connection is/are invalid");
        //        BasicExceptionHandler.Instance.AddException(
        //            "Cannot acquire the CashlinxDesktopSession and/or its database connection is invalid",
        //            new ApplicationException("CashlinxDesktopSession is invalid"));
        //        return (false);
        //    }
        //    OracleDataAccessor dA = desk.OracleDA;



        //    var inParams = new List<OracleProcParam>();

        //    //Setup input params
        //    inParams.Add(new OracleProcParam("p_store_number", storeNumber));
        //    inParams.Add(new OracleProcParam("p_icn", true, mdseicn));
        //    inParams.Add(new OracleProcParam("p_icn_qty", true, p_icn_qty));
        //    inParams.Add(new OracleProcParam("p_customer_number", p_customer_number));
        //    inParams.Add(new OracleProcParam("g_tx_date", g_tx_date));
        //    inParams.Add(new OracleProcParam("p_mr_date", p_mr_date));
        //    inParams.Add(new OracleProcParam("p_mr_time", p_mr_time));
        //    //inParams.Add(new OracleProcParam("p_mr_time", p_mr_time,OracleProcParam.TimeStampType.TIMESTAMP_TZ));
        //    inParams.Add(new OracleProcParam("p_mr_user", p_mr_user));
        //    inParams.Add(new OracleProcParam("p_mr_desc", p_mr_desc));
        //    inParams.Add(new OracleProcParam("p_mr_change", p_mr_change));
        //    inParams.Add(new OracleProcParam("p_mr_type", p_mr_type));
        //    inParams.Add(new OracleProcParam("p_class_code", p_class_code));
        //    inParams.Add(new OracleProcParam("p_acct_num", p_acct_num));
        //    inParams.Add(new OracleProcParam("p_created_by", p_created_by));
        //    inParams.Add(new OracleProcParam("p_gun_number", true, p_gun_number));
        //    inParams.Add(new OracleProcParam("p_gun_type", true, p_gun_type));
        //    inParams.Add(new OracleProcParam("o_transfer_ticket_no", OracleDbType.Int64, DBNull.Value, ParameterDirection.Output, 1));

        //    //Execute stored proc
        //    DataSet outputSet;
        //    bool retVal;
        //    try
        //    {
        //        retVal = dA.issueSqlStoredProcCommand("ccsowner",
        //            "Transfers", "transfer_out_of_store", inParams, null, "o_error_code",
        //            "o_error_desc", out outputSet);
        //    }
        //    catch (OracleException oEx)
        //    {
        //        BasicExceptionHandler.Instance.AddException("Execute Transfer out of store Failed", oEx);
        //        errorCode = dA.ErrorCode;
        //        errorText = dA.ErrorDescription + ": OracleException thrown: " + oEx.Message;
        //        return (false);
        //    }

        //    if (retVal == false)
        //    {
        //        BasicExceptionHandler.Instance.AddException("ExecuteTransferOutOfStore: return value is false", new ApplicationException());
        //        errorCode = dA.ErrorCode + " -- ExecuteTransferOutOfStore";
        //        errorText = dA.ErrorDescription + " -- Return value is false";
        //        return (false);
        //    }

        //    //Get output number
        //    DataTable outputDt = outputSet.Tables["OUTPUT"];
        //    if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
        //    {
        //        DataRow dr = outputDt.Rows[0];
        //        if (dr != null && dr.ItemArray.Length > 0)
        //        {
        //            object obj = dr.ItemArray.GetValue(1);
        //            if (obj != null)
        //            {
        //                var nextNumStr = (string)obj;
        //                o_transfer_ticket_no = Int32.Parse(nextNumStr);
        //                errorCode = "0";
        //                errorText = "Success";
        //                return (true);
        //            }
        //        }
        //    }

        //    errorCode = "1";
        //    errorText = "Transfer stored procedure failed";
        //    return (false);
        //}

        public static bool ExecuteGetTOTickets(            
            string storeNumber, string transDate, 
            out DataTable items, out string errorCode, out string errorText,string trantype)
        {
            //Create input list
            var inParams = new List<OracleProcParam>();

            //ShopDateTime.Instance.ShopDate.ToShortDateString();
            //Add cat pointer
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_trans_date", transDate));
            
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;
            items = new DataTable();
            
            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance == null ||
                !GlobalDataAccessor.Instance.IsDataAccessorValid())
            {
                errorCode = "GetScrapTOTickets";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException(errorCode, new ApplicationException(errorText));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors            
            refCursors.Add(new PairType<string, string>("scrap_to_tickets_ref_cursor", "scrap_to_tickets_ref_cursor"));

            bool retVal = false;
            try
            {
                if(trantype.Equals(TransferProcedures._TRAN_TYPE_SCRAP)){
                    retVal = dA.issueSqlStoredProcCommand( "ccsowner", "TRANSFERS", "get_TO_scrap_tickets",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
                }else if(trantype.Equals(TransferProcedures._TRAN_TYPE_EXCESS))
                {
                    //not available
                    retVal = dA.issueSqlStoredProcCommand("ccsowner", "TRANSFERS", "get_TO_excess_tickets",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
                }else
                {
                    retVal = dA.issueSqlStoredProcCommand("ccsowner", "TRANSFERS", "get_TO_refurb_tickets",
                     inParams, refCursors, "o_return_code", "o_return_text",
                     out outputDataSet); 
                }
            }
            catch (OracleException oEx)
            {
                if (trantype.Equals(TransferProcedures._TRAN_TYPE_SCRAP))
                {
                    errorCode = "get_TO_scrap_tickets";
                }
                else if (trantype.Equals(TransferProcedures._TRAN_TYPE_EXCESS))
                {
                    errorCode = "get_TO_excess_tickets";
                }
                else
                {
                    errorCode = "get_TO_refurb_tickets";
                }

                errorText = "Invocation of ExecuteGetTOTickets stored proc failed";

                BasicExceptionHandler.Instance.AddException(
                errorText,
                oEx);
                
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
                if (//outputDataSet.Tables != null && 
                    outputDataSet.Tables.Count > 0)
                {
                    ////Get information and add to List
                   
                    if (outputDataSet.Tables.Contains("scrap_to_tickets_ref_cursor"))
                    {
                        items = outputDataSet.Tables["scrap_to_tickets_ref_cursor"];
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

            errorCode = "GetScrapTOTickets";
            errorText = "Operation failed";
            return (false);
        }

        public static bool ExecuteGetCommonTOTickets(string storeNumber, string transDate,
          out DataTable items, out List<int> cashlinxStores, out string errorCode, out string errorText)
        {
            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //ShopDateTime.Instance.ShopDate.ToShortDateString();
            //Add cat pointer
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_trans_date", transDate));

            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            DataSet outputDataSet = null;
            items = new DataTable();
            cashlinxStores = new List<int>();

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance.DesktopSession == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetScrapTOTickets";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException(errorCode, new ApplicationException(errorText));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors            
            refCursors.Add(new PairType<string, string>("o_to_tickets", "o_to_tickets"));
            refCursors.Add(new PairType<string, string>("o_stores", "o_stores"));

            bool retVal = false;
            try
            {
               
                retVal = dA.issueSqlStoredProcCommand("ccsowner", "TRANSFERS", "get_to_tickets",
                inParams, refCursors, "o_return_code", "o_return_text",
                out outputDataSet);
               
            }
            catch (OracleException oEx)
            {

                errorCode = "get_to_tickets";
                errorText = "Invocation of ExecuteGetTOTickets stored proc failed";

                BasicExceptionHandler.Instance.AddException(
                errorText,
                oEx);

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
                if (//outputDataSet.Tables != null && 
                    outputDataSet.Tables.Count > 0)
                {
                    ////Get information and add to List

                    if (outputDataSet.Tables.Contains("o_to_tickets"))
                    {
                        items = outputDataSet.Tables["o_to_tickets"];
                    }
                    if (outputDataSet.Tables.Contains("o_stores"))
                    {
                        foreach (DataRow dr in outputDataSet.Tables["o_stores"].Rows)
                        {
                            DateTime conversionDate = Convert.ToDateTime(dr["pawn_conversion"]);
                            if (conversionDate <= ShopDateTime.Instance.FullShopDateTime)
                            {
                                cashlinxStores.Add(Convert.ToInt32(dr["storenumber"]));
                            }
                        }
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

            errorCode = "GetScrapTOTickets";
            errorText = "Operation failed";
            return (false);
        }

        public static bool ExecuteGetJsupMerchandise(string storeNumber, string transferType,
            out DataTable items, out DataTable itemsDesc, out string errorCode, out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            //Create output data set names
            DataSet outputDataSet = null;
            items = new DataTable();
            itemsDesc = new DataTable();

            //Verify that the accessor is valid.
            if (GlobalDataAccessor.Instance == null ||
                !GlobalDataAccessor.Instance.IsDataAccessorValid())
            {
                errorCode = "ExecuteGetJsupMerchandise";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException(errorText,
                    new ApplicationException("Cannot execute the retrieval stored procedure for ExecuteGetJsupMerchandise"));
                return (false);
            }

            // Verify transferType value, setting local variable to single-character code to be passed to stored proc
            string tranType = "";
            if (transferType.Equals("SCRAP", StringComparison.CurrentCultureIgnoreCase))
                tranType = "S";
            else if (transferType.Equals("REFURB", StringComparison.CurrentCultureIgnoreCase))
                tranType = "R";
            else if (transferType.Equals("EXCESS", StringComparison.CurrentCultureIgnoreCase))
                tranType = "E";
            else
            {
                errorCode = "ExecuteGetJsupMerchandise";
                errorText = "Invalid TransferType value";
                BasicExceptionHandler.Instance.AddException(errorText,
                    new ApplicationException("Cannot execute the retrieval stored procedure for ExecuteGetJsupMerchandise"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();
            //Add input parameters
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_tran_type", tranType));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            //Add general ref cursors            
            refCursors.Add(new PairType<string, string>("pawn_mdselist_ref_cursor", "pawn_mdselist_ref_cursor"));
            refCursors.Add(new PairType<string, string>("pawn_otherdsclist_ref_cursor", "pawn_otherdsclist_ref_cursor"));

            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MDSE_PROCS", "get_jsup_mdse",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "get_jsup_mdse";
                errorText = "Invocation of get_jsup_mdse stored procedure failed";
                BasicExceptionHandler.Instance.AddException(errorText, oEx);
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
                if (//outputDataSet.Tables != null && 
                    outputDataSet.Tables.Count > 0)
                {
                    //Get information and add to List
                    if (outputDataSet.Tables.Contains("pawn_mdselist_ref_cursor"))
                    {
                        items = outputDataSet.Tables["pawn_mdselist_ref_cursor"];
                    }
                    if(outputDataSet.Tables.Contains("pawn_otherdsclist_ref_cursor"))
                    {
                        itemsDesc = outputDataSet.Tables["pawn_otherdsclist_ref_cursor"];
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

            errorCode = "ExecuteGetJsupMerchandise";
            errorText = "Operation failed";
            return (false);
        }

        /*      public static bool ExecuteMaintainNonCacc(
                  int storeNumber,
                  int p_icn_store,
                  int p_icn_year,
                  int p_icn_doc,
                  string p_icn_doc_type,
                  int p_icn_item,
                  int p_icn_sub_item,
                  int p_tros_count,
                  int p_next_count,
                  DateTime g_tx_date,
                  int l_tran_ticket,
                  DateTime p_mr_date,
                  int p_mr_change,
                  int p_ref_type,
                  string p_mr_desc,
                  string p_mr_type,
                  string p_class_code,
                  string p_acct_num,
                  string p_created_by,
                  int p_gun_number,
                  string p_gun_type,
                  string p_user_id,
                  out string errorCode,
                  out string errorText)
              {
                  //Initialize outputs
                  errorCode = string.Empty;
                  errorText = string.Empty;


                  CashlinxDesktopSession desk = CashlinxDesktopSession.Instance;
                  if (desk == null || !desk.IsDataAccessorValid())
                  {
                      if (FileLogger.Instance.IsLogFatal)
                          FileLogger.Instance.logMessage(LogLevel.FATAL, CLAZZ,
                              "CashlinxDesktopSession and/or its database connection is/are invalid");
                      BasicExceptionHandler.Instance.AddException(
                          "Cannot acquire the CashlinxDesktopSession and/or its database connection is invalid",
                          new ApplicationException("CashlinxDesktopSession is invalid"));
                      return (false);
                  }
                  OracleDataAccessor dA = desk.OracleDA;



                  var inParams = new List<OracleProcParam>();

                  //Setup input params
                  inParams.Add(new OracleProcParam("p_store_number", storeNumber));
                  inParams.Add(new OracleProcParam("p_icn_store", p_icn_store));
                  inParams.Add(new OracleProcParam("p_icn_year", p_icn_year));
                  inParams.Add(new OracleProcParam("p_icn_doc", p_icn_doc));
                  inParams.Add(new OracleProcParam("p_icn_doc_type", p_icn_doc_type));
                  inParams.Add(new OracleProcParam("p_icn_item", p_icn_item));
                  inParams.Add(new OracleProcParam("p_icn_sub_item", p_icn_sub_item));

                  inParams.Add(new OracleProcParam("p_tros_count", p_tros_count));
                  inParams.Add(new OracleProcParam("p_next_count", p_next_count));
                  inParams.Add(new OracleProcParam("g_tx_date", g_tx_date));
                  inParams.Add(new OracleProcParam("l_tran_ticket", l_tran_ticket));
                  inParams.Add(new OracleProcParam("p_mr_date", p_mr_date));
                  inParams.Add(new OracleProcParam("p_mr_change", p_mr_change));
                  inParams.Add(new OracleProcParam("p_ref_type", p_ref_type));
                  inParams.Add(new OracleProcParam("p_mr_desc", p_mr_desc));
                  inParams.Add(new OracleProcParam("p_mr_type", p_mr_type));
                  inParams.Add(new OracleProcParam("p_class_code", p_class_code));
                  inParams.Add(new OracleProcParam("p_acct_num", p_acct_num));
                  inParams.Add(new OracleProcParam("p_created_by", p_created_by));

                  inParams.Add(new OracleProcParam("p_gun_number", p_gun_number));
                  inParams.Add(new OracleProcParam("p_gun_type", p_gun_type));
                  inParams.Add(new OracleProcParam("p_user_id", p_user_id));

                  //Execute stored proc
                  DataSet outputSet;
                  bool retVal;
                  try
                  {
                      retVal = dA.issueSqlStoredProcCommand("ccsowner",
                          "Transfers", "maintain_non_cacc", inParams, null, "o_return_code",
                          "o_return_text", out outputSet);
                  }
                  catch (OracleException oEx)
                  {
                      BasicExceptionHandler.Instance.AddException("Execute Maintain Non CACC call Failed", oEx);
                      errorCode = dA.ErrorCode;
                      errorText = dA.ErrorDescription + ": OracleException thrown: " + oEx.Message;
                      return (false);
                  }

                  if (retVal == false)
                  {
                      BasicExceptionHandler.Instance.AddException("ExecuteMaintainNonCACC: return value is false", new ApplicationException());
                      errorCode = dA.ErrorCode + " -- ExecuteMaintainNonCACC";
                      errorText = dA.ErrorDescription + " -- Return value is false";
                      return (false);
                  }



                  return (false);
              }
              */

    }
}
