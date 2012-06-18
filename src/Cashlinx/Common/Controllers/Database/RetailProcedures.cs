using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database
{
    public class RetailProcedures
    {
        private static readonly string SALE_DATA = "o_sale_data";
        private static readonly string MDSE_LIST = "o_mdselist";
        private static readonly string MDHIST_LIST = "o_mdhistlist";
        private static readonly string GUN_LIST = "o_gunlist";
        private static readonly string OTHERDSC_LIST = "o_otherdsclist";
        private static readonly string RECEIPTDET_LIST = "o_receiptdetlist";
        private static readonly string TENDER_LIST = "o_tenderlist";
        private static readonly string REFUND_TENDER_LIST = "o_refundtenderlist";
        private static readonly string FEE_LIST = "o_feelist";
        private static readonly string LAYAWAY_DATA = "o_layaway_data";
        private static readonly string ADDLMDSE_LIST = "o_addlmdseinfolist";


        private static readonly string OFF_CONSTANT = "OFF-";
        public static readonly string DOC_TYPE_FOR_TEMPICN = "8";

        public static bool GetSaleData(
            DesktopSession desktopSession,
            OracleDataAccessor oDa,
            Int32 storeNumber,
            Int32 idNumber,
            string idType,
            StateStatus tempStatus,
            string transactionType,
            bool bGetCustomerInfo,
            out SaleVO sale,
            out CustomerVO customerVO,
            out string errorCode,
            out string errorText)
        {
            //Set default output params
            customerVO = null;
            sale = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            if (oDa == null)
            {
                errorCode = "GetSaleDataFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

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

            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("o_sale_data", SALE_DATA));
            refCursArr.Add(new PairType<string, string>("o_sale_mdselist", MDSE_LIST));
            refCursArr.Add(new PairType<string, string>("o_sale_gunlist", GUN_LIST));
            refCursArr.Add(new PairType<string, string>("o_sale_mdsehistlist", MDHIST_LIST));
            refCursArr.Add(new PairType<string, string>("o_sale_otherdsclist", OTHERDSC_LIST));
            refCursArr.Add(new PairType<string, string>("o_sale_receiptdetlist", RECEIPTDET_LIST));
            refCursArr.Add(new PairType<string, string>("o_sale_tenderlist", TENDER_LIST));
            refCursArr.Add(new PairType<string, string>("o_refund_tender_list", REFUND_TENDER_LIST));
            refCursArr.Add(new PairType<string, string>("o_sale_feelist", FEE_LIST));
            refCursArr.Add(new PairType<string, string>("o_addlmdseinfo", ADDLMDSE_LIST));

            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                retVal = oDa.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_retail",
                    "return_sales_data", inParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling return_sale_data stored procedure", oEx);
                errorCode = " -- return_sale_data failed";
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
                    List<SaleVO> sales;

                    try
                    {
                        ParseSaleData(outputDataSet, out sales);
                        if (CollectionUtilities.isEmpty(sales))
                        {
                            errorCode = "Parsing the data from the stored procedure failed";
                            errorText = "sale object is null";
                            return false;
                        }
                        sale = sales.First();

                        if (bGetCustomerInfo && !string.IsNullOrEmpty(sale.CustomerNumber))
                            customerVO = CustomerProcedures.getCustomerDataByCustomerNumber(desktopSession, sale.CustomerNumber);

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

            errorCode = "GETSALEDATAFAIL";
            errorText = "Operation failed";
            return (false);
        }

        public static bool GetLayawayData(
            DesktopSession desktopSession,
            OracleDataAccessor oDa,
            Int32 storeNumber,
            Int32 idNumber,
            string idType,
            StateStatus tempStatus,
            string transactionType,
            bool bGetCustomerInfo,
            out LayawayVO layaway,
            out CustomerVO customerVO,
            out string errorCode,
            out string errorText)
        {
            //Set default output params
            customerVO = null;
            layaway = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            if (oDa == null)
            {
                errorCode = "GetLayawayDataFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

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

            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("o_layaway_data", LAYAWAY_DATA));
            refCursArr.Add(new PairType<string, string>("o_layaway_mdselist", MDSE_LIST));
            refCursArr.Add(new PairType<string, string>("o_layaway_gunlist", GUN_LIST));
            refCursArr.Add(new PairType<string, string>("o_layaway_mdsehistlist", MDHIST_LIST));
            refCursArr.Add(new PairType<string, string>("o_layaway_otherdsclist", OTHERDSC_LIST));
            refCursArr.Add(new PairType<string, string>("o_layaway_receiptdetlist", RECEIPTDET_LIST));
            refCursArr.Add(new PairType<string, string>("o_layaway_tenderlist", TENDER_LIST));
            refCursArr.Add(new PairType<string, string>("o_layaway_feelist", FEE_LIST));
            refCursArr.Add(new PairType<string, string>("o_layaway_addlmdseinfoist", ADDLMDSE_LIST));

            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                retVal = oDa.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_retail",
                    "return_layaway_data", inParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling return_layaway_data stored procedure", oEx);
                errorCode = " -- return_layaway_data failed";
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
                    List<LayawayVO> layaways;

                    try
                    {
                        ParseLayawayData(outputDataSet, out layaways);
                        if (CollectionUtilities.isEmpty(layaways))
                        {
                            errorCode = "Parsing the data from the stored procedure failed";
                            errorText = "layaway object is null";
                            return false;
                        }
                        layaway = layaways.First();

                        if (bGetCustomerInfo)
                            customerVO = CustomerProcedures.getCustomerDataByCustomerNumber(desktopSession, layaway.CustomerNumber);

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

            errorCode = "GETSALEDATAFAIL";
            errorText = "Operation failed";
            return (false);
        }

        public static bool SearchForLayaways(
            OracleDataAccessor oDa,
            Int32 storeNumber,
            DateTime eligibleDate,
            out List<LayawayVO> layaways,
            out string errorCode,
            out string errorText)
        {
            //Set default output params
            layaways = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            if (oDa == null)
            {
                errorCode = "SearchForLayawayFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_layaway_eligible_date", eligibleDate.ToString("MM/dd/yyyy")));

            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("o_layaway_forfeiture_data", LAYAWAY_DATA));
            refCursArr.Add(new PairType<string, string>("o_layaway_forfeiture_mdse", MDSE_LIST));
            refCursArr.Add(new PairType<string, string>("o_layaway_receiptdetlist", RECEIPTDET_LIST));
            refCursArr.Add(new PairType<string, string>("o_layaway_payment_types", TENDER_LIST));

            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                retVal = oDa.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_RETAIL",
                    "return_eligible_lay_forfeiture", inParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling return_eligible_lay_forfeiture stored procedure", oEx);
                errorCode = " -- return_eligible_lay_forfeiture failed";
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
                        ParseLayawayData(outputDataSet, out layaways);
                        if (CollectionUtilities.isEmpty(layaways))
                        {
                            errorCode = "Parsing the data from the stored procedure failed";
                            errorText = "layaway object is null";
                            return false;
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
            }

            errorCode = "SEARCHFORLAYAWAYFAIL";
            errorText = "Operation failed";
            return (false);
        }

        public static void ParseSaleData(DataSet outputDataSet, out List<SaleVO> lstSales)
        {
            lstSales = new List<SaleVO>();

            if (outputDataSet.Tables.Count > 0)
            {
                if (outputDataSet.Tables[SALE_DATA] != null)
                {
                    // Iterate through the Sale records returned from Search
                    foreach (DataRow dataSaleRow in outputDataSet.Tables[SALE_DATA].Rows)
                    {
                        SaleVO saleData = new SaleVO();
                        saleData.Receipts = new List<Receipt>();

                        saleData.Amount =
                        Utilities.GetDecimalValue(
                            dataSaleRow[
                            "AMOUNT"],
                            new decimal());
                        saleData.CreatedBy =
                        Utilities.GetStringValue(
                            dataSaleRow[
                            "CREATEDBY"],
                            "");
                        saleData.CustomerNumber =
                        Utilities.GetStringValue(
                            dataSaleRow[
                            "CUSTOMERNUMBER"],
                            "");
                        saleData.EntityId =
                        Utilities.GetStringValue(
                            dataSaleRow[
                            "ENT_ID"], "");
                        saleData.EntityType =
                        Utilities.GetStringValue(
                            dataSaleRow[
                            "Entity_type"], "");
                        saleData.EntityNumber =
                        Utilities.GetStringValue(
                            dataSaleRow[
                            "Entity_number"], "");
                        saleData.EntityName = Utilities.GetStringValue(dataSaleRow["Entity_Name"], "");

                        saleData.LastUpdatedBy =
                        Utilities.GetStringValue(
                            dataSaleRow[
                            "UPDATEDBY"],
                            "");
                        saleData.LoanStatus =
                        (ProductStatus)
                        Enum.Parse(
                            typeof(ProductStatus),
                            Utilities.GetStringValue(
                                dataSaleRow[
                                    "STATUS_CD"
                                ],
                                ""));
                        saleData.StoreNumber =
                        Utilities.GetStringValue(
                            dataSaleRow[
                            "STORENUMBER"],
                            "");
                        saleData.OrgShopNumber = saleData.StoreNumber;

                        saleData.OrgShopState =
                        Utilities.GetStringValue(
                            dataSaleRow[
                            "STATE_CODE"],
                            "");
                        saleData.TicketNumber =
                        Utilities.GetIntegerValue(
                            dataSaleRow[
                            "TICKET_NUMBER"],
                            0);
                        saleData.ProductType = ProductType.SALE.ToString();

                        saleData.StatusDate =
                        Utilities.GetDateTimeValue(
                            dataSaleRow[
                            "STATUS_DATE"],
                            DateTime.MinValue);

                        saleData.StatusTime =
                        Utilities.GetDateTimeValue(
                            dataSaleRow[
                            "STATUS_TIME"],
                            DateTime.MinValue);

                        saleData.TempStatus =
                        (StateStatus)
                        Enum.Parse(typeof(StateStatus),
                                   Utilities.
                                   GetStringValue(
                                       dataSaleRow
                                       [
                                           "TEMP_STATUS"
                                       ]) !=
                                   ""
                                   ? Utilities.
                                   GetStringValue(dataSaleRow
                                                  [
                                                      "TEMP_STATUS"
                                                  ])
                                   : StateStatus.
                                   BLNK.
                                   ToString());
                        saleData.DateMade =
                        Utilities.GetDateTimeValue(
                            dataSaleRow[
                            "DATE_MADE"],
                            DateTime.MaxValue);
                        saleData.MadeTime =
                        Utilities.GetTimestampValue(
                            dataSaleRow[
                            "TIME_MADE"]);

                        saleData.UpdatedDate =
                        Utilities.GetDateTimeValue(
                            dataSaleRow[
                            "LASTUPDATEDATE"]);
                        saleData.CashDrawerID = Utilities.GetStringValue(dataSaleRow["CASH_DRAWER"], "");
                        saleData.SalesTaxAmount = Utilities.GetDecimalValue(dataSaleRow["SALES_TAX"], 0);
                        /*decimal salesTaxRate = 0.0M;
                        if (saleData.Amount > 0)
                            salesTaxRate = Math.Round(saleData.SalesTaxAmount / saleData.Amount * 100, 2);
                        saleData.SalesTaxPercentage = salesTaxRate;*/
                        saleData.RefNumber = Utilities.GetIntegerValue(dataSaleRow["REF_NUMBER"]);
                        saleData.RefType = Utilities.GetStringValue(dataSaleRow["REF_TYPE"]);

                        saleData = ParseDataSet(outputDataSet, dataSaleRow, saleData);

                        lstSales.Add(saleData);
                    }
                }
            }
        }

        public static void ParseLayawayData(DataSet outputDataSet, out List<LayawayVO> lstLayaways)
        {
            lstLayaways = new List<LayawayVO>();

            if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
            {
                if (outputDataSet.Tables[LAYAWAY_DATA] != null)
                {
                    // Iterate through the Sale records returned from Search
                    foreach (DataRow dataLayawayRow in outputDataSet.Tables[LAYAWAY_DATA].Rows)
                    {
                        LayawayVO layawayData = new LayawayVO();
                        layawayData.Receipts = new List<Receipt>();
                        layawayData = (LayawayVO)ParseDataSet(outputDataSet, dataLayawayRow, layawayData);

                        layawayData.Amount =
                        Utilities.GetDecimalValue(
                            dataLayawayRow[
                            "AMOUNT"],
                            new decimal());
                        layawayData.CreatedBy =
                        Utilities.GetStringValue(
                            dataLayawayRow[
                            "CREATEDBY"],
                            "");

                        layawayData.CustomerNumber =
                        Utilities.GetStringValue(
                            dataLayawayRow[
                            "CUSTOMERNUMBER"],
                            "");

                        layawayData.EntityId =
                        Utilities.GetStringValue(
                            dataLayawayRow[
                            "ENT_ID"], "");
                        layawayData.EntityType =
                        Utilities.GetStringValue(
                            dataLayawayRow[
                            "Entity_type"], "");
                        layawayData.EntityNumber =
                        Utilities.GetStringValue(
                            dataLayawayRow[
                            "Entity_number"], "");
                        layawayData.EntityName = Utilities.GetStringValue(dataLayawayRow["Entity_Name"], "");

                        layawayData.LastUpdatedBy =
                        Utilities.GetStringValue(
                            dataLayawayRow[
                            "UPDATEDBY"],
                            "");
                        layawayData.LoanStatus =
                        (ProductStatus)
                        Enum.Parse(
                            typeof(ProductStatus),
                            Utilities.GetStringValue(
                                dataLayawayRow[
                                    "STATUS_CD"
                                ],
                                ""));
                        layawayData.StoreNumber =
                        Utilities.GetStringValue(
                            dataLayawayRow[
                            "STORENUMBER"],
                            "");
                        layawayData.OrgShopState =
                        Utilities.GetStringValue(
                            dataLayawayRow[
                            "STATE_CODE"],
                            "");

                        layawayData.OrgShopNumber = layawayData.StoreNumber;
                        layawayData.TicketNumber =
                        Utilities.GetIntegerValue(
                            dataLayawayRow[
                            "TICKET_NUMBER"],
                            0);
                        layawayData.TimeMade =
                        Utilities.GetDateTimeValue(
                            dataLayawayRow["time_made"]);
                        layawayData.ProductType = ProductType.LAYAWAY.ToString();

                        layawayData.StatusDate =
                        Utilities.GetDateTimeValue(
                            dataLayawayRow[
                            "STATUS_DATE"],
                            DateTime.MinValue);

                        layawayData.StatusTime =
                        Utilities.GetDateTimeValue(
                            dataLayawayRow[
                            "STATUS_TIME"],
                            DateTime.MinValue);

                        layawayData.TempStatus =
                        (StateStatus)
                        Enum.Parse(typeof(StateStatus),
                                   Utilities.
                                   GetStringValue(
                                       dataLayawayRow
                                       [
                                           "TEMP_STATUS"
                                       ]) !=
                                   ""
                                   ? Utilities.
                                   GetStringValue(dataLayawayRow
                                                  [
                                                      "TEMP_STATUS"
                                                  ])
                                   : StateStatus.
                                   BLNK.
                                   ToString());
                        layawayData.DateMade =
                        Utilities.GetDateTimeValue(
                            dataLayawayRow[
                            "DATE_MADE"],
                            DateTime.MaxValue);
                        layawayData.MadeTime =
                        Utilities.GetTimestampValue(
                            dataLayawayRow[
                            "TIME_MADE"]);

                        layawayData.UpdatedDate =
                        Utilities.GetDateTimeValue(
                            dataLayawayRow[
                            "LASTUPDATEDATE"]);
                        layawayData.CashDrawerID = Utilities.GetStringValue(dataLayawayRow["CASH_DRAWER"], "");
                        layawayData.DownPayment = Utilities.GetDecimalValue(dataLayawayRow["down_payment"], 0.0M);
                        layawayData.MonthlyPayment = Utilities.GetDecimalValue(dataLayawayRow["monthly_payment"], 0.0M);
                        layawayData.NumberOfPayments = Utilities.GetIntegerValue(dataLayawayRow["NUMBER_OF_PAYMENTS"], 0);
                        layawayData.FirstPayment = Utilities.GetDateTimeValue(dataLayawayRow["FIRST_PAYMENT"]);
                        layawayData.LastPayment = Utilities.GetDateTimeValue(dataLayawayRow["LAST_PAYMENT"]);
                        layawayData.NextPayment = Utilities.GetDateTimeValue(dataLayawayRow["NEXT_PAYMENT"]);
                        layawayData.ForfeitureNote = Utilities.GetDateTimeValue(dataLayawayRow["FORFEITURE_NOTE"]);
                        layawayData.Comments = Utilities.GetStringValue(dataLayawayRow["COMMENTS"]);
                        layawayData.NextDueAmount = Utilities.GetDecimalValue(dataLayawayRow["NEXT_DUE_AMT"]);
                        layawayData.SalesTaxAmount = Utilities.GetDecimalValue(dataLayawayRow["SALES_TAX"], 0);

                        lstLayaways.Add(layawayData);
                    }
                }
            }
        }

        private static bool FindReceiptData(SaleVO retailData, string receiptNumber)
        {
            bool found = false;
            foreach (Receipt receipt in retailData.Receipts)
            {
                if (receipt.ReceiptNumber.Equals(receiptNumber))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        public static SaleVO ParseDataSet(DataSet outputDataSet, DataRow dataRetailRow, SaleVO retailData)
        {
            string sDATA = string.Empty;
            if (outputDataSet.Tables[OTHERDSC_LIST] != null &&
                outputDataSet.Tables[OTHERDSC_LIST].Rows.Count > 0)
            {
                sDATA = outputDataSet.Tables[OTHERDSC_LIST].Columns.Cast<DataColumn>().Aggregate(sDATA, (current, myColumn) => current + (myColumn.ColumnName + ","));
                sDATA = sDATA.Substring(0, sDATA.Length - 1);
                sDATA += Environment.NewLine;
                foreach (DataRow myRow in outputDataSet.Tables[OTHERDSC_LIST].Rows)
                {
                    sDATA = outputDataSet.Tables[OTHERDSC_LIST].Columns.Cast<DataColumn>().Aggregate(sDATA, (current, myColumn) => current + (myRow[myColumn.ColumnName] + ","));
                    sDATA = sDATA.Substring(0, sDATA.Length - 1);
                    sDATA += Environment.NewLine;
                }
            }

            if (outputDataSet.Tables.Count > 0)
            {
                //Create receipt from receipt details cursor
                retailData.TicketNumber = Utilities.GetIntegerValue(dataRetailRow["TICKET_NUMBER"], 0);
                retailData.Receipts = CustomerLoans.CreateReceipt(retailData, outputDataSet.Tables[RECEIPTDET_LIST]);

                //Get all the fee data
                Fee newFee;
                if (outputDataSet.Tables[FEE_LIST] != null)
                {
                    foreach (DataRow dr in outputDataSet.Tables[FEE_LIST].Rows)
                    {
                        newFee = new Fee
                        {
                            FeeType = (FeeTypes)Enum.Parse(typeof(FeeTypes),
                                                           Utilities.GetStringValue(dr["FEE_TYPE"])),
                            Value = Utilities.GetDecimalValue(dr["FEE_AMOUNT"], 0.00M),
                            OriginalAmount = Utilities.GetDecimalValue(dr["FEE_ORIGINAL_AMT"], 0.00M),
                            FeeState = (FeeStates)Enum.Parse(typeof(FeeStates), Utilities.GetStringValue(dr["FEE_STATE_CODE"])),
                            Prorated = Utilities.GetStringValue(dr["FEE_IS_PRORATED"]) == "1" ? true : false,
                            FeeDate = Utilities.GetDateTimeValue(dr["FEE_DATE"])
                            //FeeGroup=(FeeGroups)Enum.Parse(typeof(FeeGroups),Utilities.GetStringValue(dr["FEE_GROUP_CODE"]))
                        };
                        retailData.Fees.Add(newFee);
                    }
                }

                if (outputDataSet.Tables[TENDER_LIST] != null && outputDataSet.Tables[TENDER_LIST].Rows.Count > 0)
                {
                    retailData.TenderDataDetails = new List<TenderData>();
                    string tenderListFilter = "TICKET_NUMBER = " + Utilities.GetIntegerValue(dataRetailRow["TICKET_NUMBER"]);
                    DataRow[] dataTenderRows = outputDataSet.Tables[TENDER_LIST].Select();
                    foreach (DataRow dr in dataTenderRows)
                    {
                        if (FindReceiptData(retailData, Utilities.GetStringValue(dr["receipt_number"])))
                        {
                            var tender = new TenderData
                                         {
                                             TenderType = Utilities.GetStringValue(dr["operationcode"]),
                                             TenderAmount = Utilities.GetDecimalValue(dr["amount"]),
                                             TenderAuth = Utilities.GetStringValue(dr["payment_auth"]),
                                             MethodOfPmt = Utilities.GetStringValue(dr["method_of_payment"]),
                                             ReceiptNumber = Utilities.GetStringValue(dr["receipt_number"])
                                         };
                            if (outputDataSet.Tables[TENDER_LIST].Columns.Contains("reversalinfo"))
                                tender.ReversalInfo = Utilities.GetStringValue(dr["reversalinfo"], "");
                            retailData.TenderDataDetails.Add(tender);
                        }
                    }
                }

                if (outputDataSet.Tables[REFUND_TENDER_LIST] != null && outputDataSet.Tables[REFUND_TENDER_LIST].Rows.Count > 0)
                {
                    retailData.RefundTenderData = new List<TenderData>();
                    DataRow[] dataTenderRows = outputDataSet.Tables[REFUND_TENDER_LIST].Select();
                    foreach (DataRow dr in dataTenderRows)
                    {
                        var tender = new TenderData
                                     {
                                         TenderType = Utilities.GetStringValue(dr["operationcode"]),
                                         TenderAmount = Utilities.GetDecimalValue(dr["amount"]),
                                         TenderAuth = Utilities.GetStringValue(dr["payment_auth"]),
                                         MethodOfPmt = Utilities.GetStringValue(dr["method_of_payment"]),
                                         ReceiptNumber = Utilities.GetStringValue(dr["receipt_number"])
                                     };
                        retailData.RefundTenderData.Add(tender);
                    }
                }


                List<RetailItem> storedPawnItems = new List<RetailItem>();

                // Pull from Merchandise List Table
                if (outputDataSet.Tables[MDSE_LIST] != null)
                {
                    #region nonJewelry
                    string sMDSEFilter = "STORENUMBER = '" + Utilities.GetStringValue(dataRetailRow["STORENUMBER"]) + "' ";
                    if (outputDataSet.Tables[MDSE_LIST].Columns.Contains("Ticket_Number"))
                        sMDSEFilter += " AND TICKET_NUMBER = " + Utilities.GetIntegerValue(dataRetailRow["TICKET_NUMBER"]);

                    DataRow[] dataMsdeRows = outputDataSet.Tables[MDSE_LIST].Select(sMDSEFilter);

                    foreach (DataRow dataMsdeRow in dataMsdeRows)
                    {
                        RetailItem storedPawnItem = new RetailItem();
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
                        storedPawnItem.RetailPrice = Utilities.GetDecimalValue(dataMsdeRow["RETAIL_PRICE"], 0);
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
                        storedPawnItem.ItemAmount = Utilities.GetDecimalValue(dataMsdeRow["ITEM_AMT"], 0.00M);
                        storedPawnItem.ItemAmount_Original = Utilities.GetDecimalValue(dataMsdeRow["AMOUNT"], 0.00M);
                        storedPawnItem.Location = Utilities.GetStringValue(dataMsdeRow["LOCATION"], "");
                        storedPawnItem.Location_Aisle = Utilities.GetStringValue(dataMsdeRow["LOC_AISLE"], "");
                        storedPawnItem.Location_Assigned = Utilities.GetStringValue(dataMsdeRow["LOC"]) == string.Empty ? false : Utilities.GetStringValue(dataMsdeRow["LOC"]) == "Y" ? true : false;
                        storedPawnItem.Location_Shelf = Utilities.GetStringValue(dataMsdeRow["LOC_SHELF"], "");
                        storedPawnItem.mDocNumber = Utilities.GetIntegerValue(dataMsdeRow["ICN_DOC"], 0);
                        storedPawnItem.mDocType = Utilities.GetStringValue(dataMsdeRow["ICN_DOC_TYPE"], "1");
                        storedPawnItem.MerchandiseType = Utilities.GetStringValue(dataMsdeRow["MD_TYPE"], "");
                        storedPawnItem.mItemOrder = Utilities.GetIntegerValue(dataMsdeRow["ICN_ITEM"], 0);
                        storedPawnItem.mStore = Utilities.GetIntegerValue(dataMsdeRow["ICN_STORE"], 0);
                        storedPawnItem.mYear = Utilities.GetIntegerValue(dataMsdeRow["ICN_YEAR"], 0);
                        storedPawnItem.Quantity = Utilities.GetIntegerValue(dataMsdeRow["QUANTITY"], 1);
                        if (dataMsdeRow.Table.Columns.Contains("REFUND_QUANTITY"))
                            storedPawnItem.RefundQuantity = Utilities.GetIntegerValue(dataMsdeRow["REFUND_QUANTITY"], 1);
                        storedPawnItem.ItemStatus = (ProductStatus)Enum.Parse(typeof(ProductStatus), Utilities.GetStringValue(dataMsdeRow["STATUS_CD"], ""));
                        //TODO When discount percent comes from DB
                        storedPawnItem.DiscountPercent = 0;
                        storedPawnItem.StatusDate = Utilities.GetDateTimeValue(dataMsdeRow["STATUS_DATE"], DateTime.MinValue);

                        if (Utilities.GetStringValue(dataMsdeRow["ORG_DISP_DOC"]).Length > 0 &&
                            Utilities.GetStringValue(dataMsdeRow["ORG_DISP_DOC"]) != "0" &&
                            retailData.LayawayTicketNumber == null)
                        {
                            retailData.LayawayTicketNumber = Utilities.GetStringValue(dataMsdeRow["ORG_DISP_DOC"]);
                        }
                        storedPawnItem.DispDoc = Utilities.GetIntegerValue(dataMsdeRow["DISP_DOC"], 0);
                        storedPawnItem.DispDocType = Utilities.GetStringValue(dataMsdeRow["DISP_TYPE"], "");

                        if (outputDataSet.Tables[OTHERDSC_LIST] != null)
                        {
                            string sCommentFilter = string.Empty;
                            sCommentFilter = "STORENUMBER = '" + dataRetailRow["StoreNumber"] + "' ";
                            sCommentFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                            sCommentFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                            sCommentFilter += " and ICN_DOC = '" + storedPawnItem.mDocNumber + "' ";
                            sCommentFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                            sCommentFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                            sCommentFilter += " and ICN_SUB_ITEM = 0";
                            sCommentFilter += " and MASK_SEQ = 999";

                            DataRow[] dataOtherCommentRows = outputDataSet.Tables[OTHERDSC_LIST].Select(sCommentFilter);

                            if (dataOtherCommentRows.Count() > 0)
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
                                if (outputDataSet.Tables[OTHERDSC_LIST] != null && answer.AnswerCode == 999)
                                {
                                    string sOtherDscFilter = string.Empty;
                                    sOtherDscFilter = "STORENUMBER = '" + dataRetailRow["StoreNumber"] + "' ";
                                    sOtherDscFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                                    sOtherDscFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                    sOtherDscFilter += " and ICN_DOC = '" + storedPawnItem.mDocNumber + "' ";
                                    sOtherDscFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                    sOtherDscFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                    sOtherDscFilter += " and ICN_SUB_ITEM = 0";
                                    sOtherDscFilter += " and MASK_SEQ = " + iMask.ToString();
                                    DataRow[] dataOtherDScRows = outputDataSet.Tables[OTHERDSC_LIST].Select(sOtherDscFilter);

                                    if (dataOtherDScRows.Count() > 0)
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

                            string sMDHistoryFilter = string.Empty;
                            sMDHistoryFilter = "STORENUMBER = '" + dataRetailRow["StoreNumber"] + "' ";
                            sMDHistoryFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                            sMDHistoryFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                            sMDHistoryFilter += " and ICN_DOC = '" + storedPawnItem.mDocNumber + "' ";
                            sMDHistoryFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                            sMDHistoryFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                            sMDHistoryFilter += " and ICN_SUB_ITEM = 0";

                            if (outputDataSet.Tables[MDHIST_LIST] != null)
                            {
                                DataRow[] dataMDHistoryRows = outputDataSet.Tables[MDHIST_LIST].Select(sMDHistoryFilter);

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
                            if (proKnowData.RetailAmount != 0 && proKnowData.LoanVarHighAmount != 0 && proKnowData.LoanVarLowAmount != 0)
                            {
                                storedPawnItem.SelectedProKnowMatch.transitionStatus = TransitionStatus.MAN_MODEL_PROKNOW;
                            }
                        }
                        //Add additional mdse info
                        if (outputDataSet.Tables[ADDLMDSE_LIST] != null && outputDataSet.Tables[ADDLMDSE_LIST].Rows.Count > 0)
                        {
                            string sAddlInfoFilter = string.Empty;
                            sAddlInfoFilter = "STORENUMBER = '" + dataRetailRow["StoreNumber"] + "' ";
                            sAddlInfoFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                            sAddlInfoFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                            sAddlInfoFilter += " and ICN_DOC = '" + storedPawnItem.mDocNumber + "' ";
                            sAddlInfoFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                            sAddlInfoFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                            sAddlInfoFilter += " and ICN_SUB_ITEM = 0";



                            DataRow[] dataAddlMdseInfoRows = outputDataSet.Tables[ADDLMDSE_LIST].Select(sAddlInfoFilter);
                            //retailData.SalesTaxAmount = Utilities.GetDecimalValue(dataSaleRow["SALES_TAX"], 0);
                            decimal salesTaxRate = 0.0M;
                            decimal retailFinalAmount = 0.0m;
                            decimal couponAmounts = 0.0m;
                            DataRow[] ADDLMDSE_LISTRows = outputDataSet.Tables[ADDLMDSE_LIST].Select();

                            foreach (DataRow addlMdseInfoRows in ADDLMDSE_LISTRows)
                                couponAmounts += Utilities.GetDecimalValue(addlMdseInfoRows["TRANSACTION_COUPON_AMT"], 0) + Utilities.GetDecimalValue(addlMdseInfoRows["COUPON_AMT"], 0);

                            retailFinalAmount = retailData.Amount - couponAmounts;
                            if (retailData.Amount > 0)
                                salesTaxRate = Math.Round(retailData.SalesTaxAmount / retailFinalAmount * 100, 2);
                            retailData.SalesTaxPercentage = salesTaxRate;
                            if (dataAddlMdseInfoRows.Count() > 0)
                            {
                                retailData.CouponPercentage = Utilities.GetDecimalValue(dataAddlMdseInfoRows[0]["COUPON_AMT"], 0);

                                storedPawnItem.CouponAmount = Utilities.GetDecimalValue(dataAddlMdseInfoRows[0]["COUPON_AMT"], 0);
                                storedPawnItem.CouponCode = Utilities.GetStringValue(dataAddlMdseInfoRows[0]["COUPON_CODE"], "");
                                storedPawnItem.ProratedCouponAmount = Utilities.GetDecimalValue(dataAddlMdseInfoRows[0]["TRANSACTION_COUPON_AMT"], 0);
                                storedPawnItem.TranCouponCode = Utilities.GetStringValue(dataAddlMdseInfoRows[0]["TRANSACTION_COUPON_CODE"], "");
                            }

                        }
                        else
                        {
                            //retailData.SalesTaxAmount = Utilities.GetDecimalValue(dataSaleRow["SALES_TAX"], 0);
                            decimal salesTaxRate = 0.0M;
                            decimal retailFinalAmount = retailData.Amount;
                            if (retailData.Amount > 0)
                                salesTaxRate = Math.Round(retailData.SalesTaxAmount / retailFinalAmount * 100, 2);
                            retailData.SalesTaxPercentage = salesTaxRate;
                        }
                        if (outputDataSet.Tables[GUN_LIST] != null)
                        {
                            string sCommentFilter = string.Empty;
                            sCommentFilter = "STORENUMBER = '" + dataRetailRow["StoreNumber"] + "' ";
                            sCommentFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                            sCommentFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                            sCommentFilter += " and ICN_DOC = '" + storedPawnItem.mDocNumber + "' ";
                            sCommentFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                            sCommentFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                            sCommentFilter += " and ICN_SUB_ITEM = 0";

                            DataRow[] dataGunListRows = outputDataSet.Tables[GUN_LIST].Select(sCommentFilter);

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

                        string sMDSEJewelryFilter = string.Empty;
                        sMDSEJewelryFilter = "STORENUMBER = '" + dataRetailRow["StoreNumber"] + "' ";
                        sMDSEJewelryFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                        sMDSEJewelryFilter += " and ICN_DOC = '" + retailData.TicketNumber + "' ";
                        sMDSEJewelryFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                        sMDSEJewelryFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                        sMDSEJewelryFilter += " and ICN_SUB_ITEM > 0";

                        DataRow[] dataMsdeJewelryRows = outputDataSet.Tables[MDSE_LIST].Select(sMDSEJewelryFilter);

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
                                    if (outputDataSet.Tables[OTHERDSC_LIST] != null && answer.AnswerCode == 999)
                                    {
                                        string sOtherDscFilter = string.Empty;
                                        sOtherDscFilter = "STORENUMBER = '" + dataRetailRow["StoreNumber"] + "' ";
                                        sOtherDscFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                                        sOtherDscFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                                        sOtherDscFilter += " and ICN_DOC = '" + retailData.TicketNumber + "' ";
                                        sOtherDscFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                                        sOtherDscFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                                        sOtherDscFilter += " and ICN_SUB_ITEM = " + Utilities.GetIntegerValue(dataMsdeJewelryRow["ICN_SUB_ITEM"], 0).ToString();
                                        sOtherDscFilter += " and MASK_SEQ = " + iMask.ToString();
                                        DataRow[] dataOtherDScRows = outputDataSet.Tables[OTHERDSC_LIST].Select(sOtherDscFilter);

                                        if (dataOtherDScRows.Count() > 0)
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

                        # region Previous Retail Price

                        if (outputDataSet.Tables[MDHIST_LIST] != null)
                        {
                            string sMdHistFilter = string.Empty;
                            sMdHistFilter = "STORENUMBER = '" + dataRetailRow["StoreNumber"] + "' ";
                            sMdHistFilter += " and ICN_STORE = " + storedPawnItem.mStore.ToString();
                            sMdHistFilter += " and ICN_YEAR = " + storedPawnItem.mYear.ToString();
                            sMdHistFilter += " and ICN_DOC = '" + storedPawnItem.mDocNumber + "' ";
                            sMdHistFilter += " and ICN_DOC_TYPE = " + storedPawnItem.mDocType;
                            sMdHistFilter += " and ICN_ITEM = " + storedPawnItem.mItemOrder;
                            sMdHistFilter += " and ICN_SUB_ITEM = 0";
                            sMdHistFilter += " and MOD_DOC = '" + retailData.TicketNumber + "'";
                            sMdHistFilter += " and NEW_STATUS = 'SOLD'";
                            sMdHistFilter += " and AMT_TYPE = 'R'";
                            sMdHistFilter += " and MOD_TYPE = 'PBOS'";
                            DataRow[] dataMdHistRows = outputDataSet.Tables[MDHIST_LIST].Select(sMdHistFilter);

                            if (dataMdHistRows.Length == 1)
                            {
                                storedPawnItem.PreviousRetailPrice = Utilities.GetDecimalValue(dataMdHistRows[0]["OLD_AMOUNT"]);
                                storedPawnItem.DiscountPercent = storedPawnItem.CalculateDiscountPercentage(storedPawnItem.PreviousRetailPrice, storedPawnItem.RetailPrice);
                            }
                            else
                            {
                                storedPawnItem.PreviousRetailPrice = storedPawnItem.RetailPrice;
                                storedPawnItem.DiscountPercent = 0;
                            }
                        }

                        # endregion

                        storedPawnItems.Add(storedPawnItem);
                    }
                    #endregion
                }
                if (retailData.RetailItems == null)
                    retailData.RetailItems = new List<RetailItem>();
                retailData.RetailItems = storedPawnItems;
            }
            return retailData;
        }

        public static bool FilterBadLanguage(string comment,
                                out string errorCode,
                                out string errorText)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            if (dA == null)
            {
                errorCode = "FilterBadLanguage";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("LockItem", new ApplicationException("Cannot execute the Bad Language Filter stored procedure"));
                return (false);
            }
            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_comment", comment));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
    
            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_filteredWords", "filterList"));
            DataSet outputDataSet;

            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_GEN_PROCs", "isFiltered",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = " -- SearchForItem";
                errorText = " -- Invocation of SearchForItem stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when FilterBadLanguage stored proc", oEx);
                return (false);
            }
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }

            if (outputDataSet != null && outputDataSet.Tables.Count > 0)
            {
                retVal = true;
            }
            else
            {
                retVal = false;
            }
            return retVal;
        }

        public static bool UpdateItem(DesktopSession desktopSession,
                                      Item item,
                                      out string errorCode,
                                      out string errorText)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            if (dA == null)
            {
                /*
                * p_icn IN VARCHAR,
                p_tran_type      IN     VARCHAR2,
                p_store_number   IN     VARCHAR2,
                p_status_date    IN     VARCHAR2,
                p_status_time    IN     VARCHAR2,
                p_user_id        IN     VARCHAR2,*/
                errorCode = "Change Assignment Type";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("ChangeAssignmentType", new ApplicationException("Cannot execute the Lock Item stored procedure"));
                return (false);
            }
            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_icn", item.Icn));
            inParams.Add(new OracleProcParam("p_tran_type", Utilities.AssignmentTypeToString(item.PfiAssignmentType)));
            inParams.Add(new OracleProcParam("p_store_number", desktopSession.CurrentSiteId.StoreNumber));
            inParams.Add(new OracleProcParam("p_status_date", DateTime.Now.ToShortDateString()));
            inParams.Add(new OracleProcParam("p_status_time", DateTime.Now.ToShortTimeString()));
            inParams.Add(new OracleProcParam("p_user_id", desktopSession.UserName));
            //inParams.Add(new OracleProcParam("p_user_id", username));
            inParams.Add(new OracleProcParam("p_refurb_number", item.RefurbNumber));
            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            DataSet outputDataSet;

            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_retail", "update_mdse_trantype",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = " -- ChangeAssignmentType";
                errorText = " -- Invocation of SearchForItem stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking update_mdse_trantype stored proc", oEx);
                return (false);
            }
            return retVal;
        }

        public static bool LockItem(DesktopSession desktopSession,
                                    string icn,
                                    out string errorCode,
                                    out string errorText, string strLock)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            if (dA == null)
            {
                errorCode = "LockItem";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("LockItem", new ApplicationException("Cannot execute the Lock Item stored procedure"));
                return (false);
            }
            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_store_number", desktopSession.CurrentSiteId.StoreNumber));
            inParams.Add(new OracleProcParam("p_icn", icn));
            inParams.Add(new OracleProcParam("p_lock", strLock));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            DataSet outputDataSet;

            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_retail", "lock_unlock_item",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = " -- SearchForItem";
                errorText = " -- Invocation of SearchForItem stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking item_search stored proc", oEx);
                return (false);
            }
            return retVal;
        }

        /// <summary>
        ///  Check to see if this ICN is Sellable if not 
        ///  returns a string containing the reason that it is on Hold 
        ///  DaveG code
        /// </summary>
        /// <param name="searchFor"></param>
        /// <param name="searchValues"></param>
        /// <param name="HoldType"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool isSearchItemSellable(
        string icn,
        out string unsellableReason,
        out string errorCode,
        out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;
            unsellableReason = string.Empty;

            DataSet outputDataSet = null;
            // searchItems = new List<RetailItem>();

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Verify that the accessor is valid
            if (dA == null)
            {
                errorCode = "isSearchItemSellable";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("is_item_onHold",
                                                            new ApplicationException("Can not execute the Search For Item stored procedure"));
                unsellableReason = string.Empty;
                return (false);
            }

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_icn", icn));
            //  inParams.Add(new OracleProcParam("p_search_for", true, searchFor));
            inParams.Add(new OracleProcParam("o_Unsellable_Reason", OracleDbType.Varchar2, unsellableReason, ParameterDirection.Output, 255));
            //  OracleDbType.Varchar2, ParameterDirection.Output, string.Empty));
            //    oParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            //  inParams.Add(new OracleProcParam("p_search_flag", searchFlag));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            //Add general ref cursors
           // refCursors.Add(new PairType<string, string>("o_hold_desc", "unsellableReason"));
            //refCursors.Add(new PairType<string, string>("o_return_code", "onHolditem_return_code"));
            //refCursors.Add(new PairType<string, string>("o_return_text", "onHolditem_text"));


            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_retail", "isItem_Sellable",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = " -- is_item_onHold";
                errorText = " -- Invocation of SearchForItem stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking item_search stored proc", oEx);
                unsellableReason = string.Empty;
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                unsellableReason = string.Empty;
                return (false);
            }


            //if (errorCode != "0")
            //{
            //    errorCode = "isSearchItemOnHold";
            //    errorText = "Operation failed";
            //    unsellableReason = string.Empty;
            //    return (false);
            //}


            errorCode = "0";
            errorText = string.Empty;

            unsellableReason = outputDataSet.Tables[0].Rows[0].ItemArray[1].ToString();

            return (true);
        }
        // *** End Dave Code ***





        public static bool SearchForItem(
            List<string> searchFor,
            List<string> searchValues,
            DesktopSession desktopSession,
            string searchFlag,
            bool loadActualQuantity,
            out List<RetailItem> searchItems,
            out string errorCode,
            out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;

            DataSet outputDataSet = null;
            searchItems = new List<RetailItem>();

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Verify that the accessor is valid
            if (dA == null)
            {
                errorCode = "SearchForItem";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("SearchForItem", new ApplicationException("Cannot execute the Search For Item stored procedure"));
                return (false);
            }

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_search_val", true, searchValues));
            inParams.Add(new OracleProcParam("p_search_for", true, searchFor));
            inParams.Add(new OracleProcParam("p_store_number", desktopSession.CurrentSiteId.StoreNumber));
            inParams.Add(new OracleProcParam("p_search_flag", searchFlag));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_item_details", "item_details_ref_cursor"));
            refCursors.Add(new PairType<string, string>("o_item_otherdscdetails", "item_otherdsc_ref_cursor"));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_retail", "item_search",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = " -- SearchForItem";
                errorText = " -- Invocation of SearchForItem stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking item_search stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }

            if (outputDataSet != null && outputDataSet.Tables.Count > 0)
            {
                if (outputDataSet.Tables[0].TableName == "item_details_ref_cursor")
                {
                    foreach (DataRow dr in outputDataSet.Tables[0].Rows)
                    {
                        StateStatus tmpStatus;
                        if (string.IsNullOrEmpty(Utilities.GetStringValue(dr["TEMP_STATUS"])))
                            tmpStatus = StateStatus.BLNK;
                        else
                            tmpStatus = (StateStatus)Enum.Parse(typeof(StateStatus), Utilities.GetStringValue(dr["TEMP_STATUS"], ""));

                        if (tmpStatus == StateStatus.BLNK || tmpStatus == StateStatus.LSALE)
                        {
                            RetailItem tmpItem = new RetailItem();
                            tmpItem.TicketDescription = Utilities.GetStringValue(dr["MD_DESC"]);
                            tmpItem.ItemStatus = (ProductStatus)Enum.Parse(typeof(ProductStatus), Utilities.GetStringValue(dr["STATUS_CD"], ""));
                            tmpItem.RetailPrice = Utilities.GetDecimalValue(dr["RETAIL_PRICE"], 0);
                            /*if (tmpItem.RetailPrice <= 0)
                            tmpItem.RetailPrice = Utilities.GetDecimalValue(dr["ITEM_AMT"], 0);*/
                            tmpItem.ItemAmount = Utilities.GetDecimalValue(dr["AMOUNT"], 0.00M);
                            tmpItem.PfiAmount = Utilities.GetDecimalValue(dr["pfi_amount"], 0M);
                            tmpItem.CategoryCode = Utilities.GetIntegerValue(dr["CAT_CODE"]);
                            tmpItem.mDocNumber = Utilities.GetIntegerValue(dr["ICN_DOC"], 0);
                            tmpItem.mDocType = Utilities.GetStringValue(dr["ICN_DOC_TYPE"], "1");
                            tmpItem.MerchandiseType = Utilities.GetStringValue(dr["MD_TYPE"], "");
                            tmpItem.mItemOrder = Utilities.GetIntegerValue(dr["ICN_ITEM"], 0);
                            tmpItem.mStore = Utilities.GetIntegerValue(dr["ICN_STORE"], 0);
                            tmpItem.TranType = Utilities.GetStringValue(dr["TRAN_TYPE"]);
                            tmpItem.mYear = Utilities.GetIntegerValue(dr["ICN_YEAR"], 0);
                            tmpItem.GunNumber = Utilities.GetIntegerValue(dr["GUN_NUMBER"], 0);
                            string sStoreNumber = Utilities.GetStringValue(dr["STORENUMBER"], "");
                            tmpItem.IsJewelry = Utilities.IsJewelry(tmpItem.CategoryCode);
                            tmpItem.Icn = Utilities.IcnGenerator(tmpItem.mStore,
                                                                 tmpItem.mYear,
                                                                 tmpItem.mDocNumber,
                                                                 tmpItem.mDocType,
                                                                 tmpItem.mItemOrder,
                                                                 Utilities.GetIntegerValue(dr["ICN_SUB_ITEM"], 0));

                            tmpItem.HoldType = Utilities.GetStringValue(dr["HOLD_DESC"], "");
                            tmpItem.CategoryDescription = Utilities.GetStringValue(dr["CAT_DESC"], "");
                            tmpItem.PfiDate = Utilities.GetDateTimeValue(dr["PFI_DATE"]);
                            tmpItem.Md_Date = Utilities.GetDateTimeValue(dr["MD_DATE"]);
                            if (loadActualQuantity)
                            {
                                tmpItem.Quantity = Math.Max(1, Utilities.GetIntegerValue(dr["QUANTITY"]));
                            }
                            else
                            {
                                tmpItem.Quantity = 1;//Utilities.GetIntegerValue(dr["QUANTITY"]);
                            }
                            tmpItem.PfiAssignmentType = tmpItem.IsJewelry ? PfiAssignment.Scrap : PfiAssignment.Normal;
                            if (!string.IsNullOrEmpty(Utilities.GetStringValue(dr["TEMP_STATUS"])))
                                tmpItem.TempStatus = (StateStatus)Enum.Parse(typeof(StateStatus), Utilities.GetStringValue(dr["TEMP_STATUS"], ""));
                            tmpItem.IsGun = Utilities.IsGun(tmpItem.GunNumber, tmpItem.CategoryCode, tmpItem.IsJewelry, tmpItem.MerchandiseType);

                            tmpItem.Attributes = new List<ItemAttribute>();

                            for (int iMask = 1; iMask <= 15; iMask++)
                            {
                                ItemAttribute itemAttribute = new ItemAttribute();

                                if (Utilities.GetIntegerValue(dr["MASK" + iMask.ToString()], 0) > 0)
                                {
                                    itemAttribute.MaskOrder = iMask;

                                    Answer answer = new Answer();
                                    answer.AnswerCode = Utilities.GetIntegerValue(dr["MASK" + iMask.ToString()], 0);
                                    answer.AnswerText = Utilities.GetStringValue(dr["MASK_DESC" + iMask.ToString()], "");

                                    // Pull from Other Description List Table
                                    if (outputDataSet.Tables.Count > 1 && outputDataSet.Tables[1] != null && answer.AnswerCode == 999)
                                    {
                                        string sOtherDscFilter = string.Empty;
                                        sOtherDscFilter = "STORENUMBER = '" + sStoreNumber + "' ";
                                        sOtherDscFilter += " and ICN_STORE = " + tmpItem.mStore.ToString();
                                        sOtherDscFilter += " and ICN_YEAR = " + tmpItem.mYear.ToString();
                                        sOtherDscFilter += " and ICN_DOC = '" + tmpItem.mDocNumber + "' ";
                                        sOtherDscFilter += " and ICN_DOC_TYPE = " + tmpItem.mDocType;
                                        sOtherDscFilter += " and ICN_ITEM = " + tmpItem.mItemOrder;
                                        sOtherDscFilter += " and ICN_SUB_ITEM = 0";
                                        sOtherDscFilter += " and MASK_SEQ = " + iMask.ToString();

                                        DataRow[] dataOtherDScRows = outputDataSet.Tables[1].Select(sOtherDscFilter);

                                        if (dataOtherDScRows.Count() > 0)
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
                                    tmpItem.Attributes.Add(itemAttribute);
                            }
                            if (outputDataSet.Tables.Count > 1 && outputDataSet.Tables[1] != null)
                            {
                                string sCommentFilter = "STORENUMBER = '" + sStoreNumber + "' ";
                                sCommentFilter += " and ICN_STORE = " + tmpItem.mStore.ToString();
                                sCommentFilter += " and ICN_YEAR = " + tmpItem.mYear.ToString();
                                sCommentFilter += " and ICN_DOC = '" + tmpItem.mDocNumber + "' ";
                                sCommentFilter += " and ICN_DOC_TYPE = " + tmpItem.mDocType;
                                sCommentFilter += " and ICN_ITEM = " + tmpItem.mItemOrder;
                                sCommentFilter += " and ICN_SUB_ITEM = 0";
                                sCommentFilter += " and MASK_SEQ = 999";

                                DataRow[] dataOtherCommentRows = outputDataSet.Tables[1].Select(sCommentFilter);

                                if (dataOtherCommentRows.Count() > 0)
                                {
                                    tmpItem.Comment = Utilities.GetStringValue(dataOtherCommentRows[0]["OD_DESC"], "");
                                }
                                else
                                {
                                    tmpItem.Comment = "";
                                }
                            }




                            if (tmpItem.IsJewelry)
                            {
                                if (tmpItem.Jewelry == null)
                                    tmpItem.Jewelry = new List<JewelrySet>();

                                string sMDSEJewelryFilter = "STORENUMBER = '" + sStoreNumber + "' ";
                                sMDSEJewelryFilter += " and ICN_STORE = '" + tmpItem.mStore + "' ";
                                sMDSEJewelryFilter += " and ICN_YEAR = " + tmpItem.mYear.ToString();
                                sMDSEJewelryFilter += " and ICN_DOC = '" + tmpItem.mDocNumber + "' ";
                                sMDSEJewelryFilter += " and ICN_DOC_TYPE = " + tmpItem.mDocType;
                                sMDSEJewelryFilter += " and ICN_ITEM = " + tmpItem.mItemOrder;
                                sMDSEJewelryFilter += " and ICN_SUB_ITEM > 0";

                                DataRow[] dataMsdeJewelryRows = outputDataSet.Tables[0].Select(sMDSEJewelryFilter);

                                for (int iJewelryCount = 0; iJewelryCount < dataMsdeJewelryRows.Count(); iJewelryCount++)
                                {
                                    JewelrySet jewelrySet = new JewelrySet();
                                    DataRow dataMsdeJewelryRow = dataMsdeJewelryRows[iJewelryCount];
                                    jewelrySet.CaccLevel = Utilities.GetIntegerValue(dataMsdeJewelryRow["CACC_LEV"], -1);
                                    jewelrySet.Category = Utilities.GetIntegerValue(dataMsdeJewelryRow["CAT_CODE"], 0);
                                    //tmpItem.IsJewelry = true;
                                    jewelrySet.SubItemNumber = Utilities.GetIntegerValue(dataMsdeJewelryRow["ICN_SUB_ITEM"], 0);
                                    jewelrySet.TotalStoneValue = Utilities.GetDecimalValue(dataMsdeJewelryRow["AMOUNT"], 0.00M);
                                    jewelrySet.Icn = Utilities.IcnGenerator(tmpItem.mStore,
                                                                            tmpItem.mYear,
                                                                            tmpItem.mDocNumber,
                                                                            tmpItem.mDocType,
                                                                            tmpItem.mItemOrder,
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
                                            if (outputDataSet.Tables.Count > 1 && outputDataSet.Tables[1] != null && answer.AnswerCode == 999)
                                            {
                                                string sOtherDscFilter = "STORENUMBER = '" + sStoreNumber + "' ";
                                                sOtherDscFilter += " and ICN_STORE = " + tmpItem.mStore.ToString();
                                                sOtherDscFilter += " and ICN_YEAR = " + tmpItem.mYear.ToString();
                                                sOtherDscFilter += " and ICN_DOC = '" + tmpItem.mDocNumber + "' ";
                                                sOtherDscFilter += " and ICN_DOC_TYPE = " + tmpItem.mDocType;
                                                sOtherDscFilter += " and ICN_ITEM = " + tmpItem.mItemOrder;
                                                sOtherDscFilter += " and ICN_SUB_ITEM = " + Utilities.GetIntegerValue(dataMsdeJewelryRow["ICN_SUB_ITEM"], 0).ToString();
                                                sOtherDscFilter += " and MASK_SEQ = " + iMask.ToString();

                                                DataRow[] dataOtherDScRows = outputDataSet.Tables[1].Select(sOtherDscFilter);

                                                if (dataOtherDScRows.Count() > 0)
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
                                        if (jewelrySet.ItemAttributeList == null)
                                            jewelrySet.ItemAttributeList = new List<ItemAttribute>();

                                        if (itemAttribute.Answer.AnswerCode == 999 || itemAttribute.Answer.AnswerCode > 0)
                                        {
                                            jewelrySet.ItemAttributeList.Add(itemAttribute);
                                        }
                                    }

                                    // jewelrySet.TicketDescription = Utilities.GetStringValue(dataMsdeJewelryRow["MD_DESC"], "");
                                    tmpItem.Jewelry.Add(jewelrySet);
                                }
                            }

                            searchItems.Add(tmpItem);
                        }
                    }
                }

                errorCode = "0";
                errorText = string.Empty;
                return (true);
            }

            errorCode = "SearchForItem";
            errorText = "Operation failed";
            return (false);
        }

        public static bool SearchForItemPUR(
           List<string> searchFor,
           List<string> searchValues,
           DesktopSession desktopSession,
           string searchFlag,
           out List<RetailItem> searchItems,
           out string errorCode,
           out string errorText)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;

            DataSet outputDataSet = null;
            searchItems = new List<RetailItem>();

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Verify that the accessor is valid
            if (dA == null)
            {
                errorCode = "SearchForItem";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("SearchForItem", new ApplicationException("Cannot execute the Search For Item stored procedure"));
                return (false);
            }

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_search_val", true, searchValues));
            inParams.Add(new OracleProcParam("p_search_for", true, searchFor));
            inParams.Add(new OracleProcParam("p_store_number", desktopSession.CurrentSiteId.StoreNumber));
            inParams.Add(new OracleProcParam("p_search_flag", searchFlag));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_item_details", "item_details_ref_cursor"));
            refCursors.Add(new PairType<string, string>("o_item_otherdscdetails", "item_otherdsc_ref_cursor"));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "Pawn_retail", "item_search_tbd",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = " -- SearchForItem";
                errorText = " -- Invocation of SearchForItem stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking item_search stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }

            if (outputDataSet != null && outputDataSet.Tables.Count > 0)
            {
                if (outputDataSet.Tables[0].TableName == "item_details_ref_cursor")
                {
                    foreach (DataRow dr in outputDataSet.Tables[0].Rows)
                    {
                        StateStatus tmpStatus;
                        if (string.IsNullOrEmpty(Utilities.GetStringValue(dr["TEMP_STATUS"])))
                            tmpStatus = StateStatus.BLNK;
                        else
                            tmpStatus = (StateStatus)Enum.Parse(typeof(StateStatus), Utilities.GetStringValue(dr["TEMP_STATUS"], ""));

                        if (tmpStatus == StateStatus.BLNK || tmpStatus == StateStatus.LSALE)
                        {
                            RetailItem tmpItem = new RetailItem();
                            tmpItem.TicketDescription = Utilities.GetStringValue(dr["MD_DESC"]);
                            tmpItem.ItemStatus = (ProductStatus)Enum.Parse(typeof(ProductStatus), Utilities.GetStringValue(dr["STATUS_CD"], ""));
                            tmpItem.RetailPrice = Utilities.GetDecimalValue(dr["RETAIL_PRICE"], 0);
                            /*if (tmpItem.RetailPrice <= 0)
                            tmpItem.RetailPrice = Utilities.GetDecimalValue(dr["ITEM_AMT"], 0);*/
                            tmpItem.ItemAmount = Utilities.GetDecimalValue(dr["AMOUNT"], 0.00M);
                            tmpItem.PfiAmount = Utilities.GetDecimalValue(dr["pfi_amount"], 0M);
                            tmpItem.CategoryCode = Utilities.GetIntegerValue(dr["CAT_CODE"]);
                            tmpItem.mDocNumber = Utilities.GetIntegerValue(dr["ICN_DOC"], 0);
                            tmpItem.mDocType = Utilities.GetStringValue(dr["ICN_DOC_TYPE"], "1");
                            tmpItem.MerchandiseType = Utilities.GetStringValue(dr["MD_TYPE"], "");
                            tmpItem.mItemOrder = Utilities.GetIntegerValue(dr["ICN_ITEM"], 0);
                            tmpItem.mStore = Utilities.GetIntegerValue(dr["ICN_STORE"], 0);
                            tmpItem.TranType = Utilities.GetStringValue(dr["TRAN_TYPE"]);
                            tmpItem.mYear = Utilities.GetIntegerValue(dr["ICN_YEAR"], 0);
                            tmpItem.GunNumber = Utilities.GetIntegerValue(dr["GUN_NUMBER"], 0);
                            tmpItem.CaccLevel = Utilities.GetIntegerValue(dr["CACC_LEV"], -1);
                            string sStoreNumber = Utilities.GetStringValue(dr["STORENUMBER"], "");
                            tmpItem.IsJewelry = Utilities.IsJewelry(tmpItem.CategoryCode);
                            tmpItem.Icn = Utilities.IcnGenerator(tmpItem.mStore,
                                                                 tmpItem.mYear,
                                                                 tmpItem.mDocNumber,
                                                                 tmpItem.mDocType,
                                                                 tmpItem.mItemOrder,
                                                                 Utilities.GetIntegerValue(dr["ICN_SUB_ITEM"], 0));

                            tmpItem.HoldType = Utilities.GetStringValue(dr["HOLD_DESC"], "");
                            tmpItem.PfiDate = Utilities.GetDateTimeValue(dr["PFI_DATE"]);
                            tmpItem.Md_Date = Utilities.GetDateTimeValue(dr["MD_DATE"]);
                            tmpItem.Quantity = 1;//Utilities.GetIntegerValue(dr["QUANTITY"]);
                            tmpItem.PfiAssignmentType = tmpItem.IsJewelry ? PfiAssignment.Scrap : PfiAssignment.Normal;
                            tmpItem.IsGun = Utilities.IsGun(tmpItem.GunNumber, tmpItem.CategoryCode, tmpItem.IsJewelry, tmpItem.MerchandiseType);
                            if (!string.IsNullOrEmpty(Utilities.GetStringValue(dr["TEMP_STATUS"])))
                                tmpItem.TempStatus = (StateStatus)Enum.Parse(typeof(StateStatus), Utilities.GetStringValue(dr["TEMP_STATUS"], ""));
                            if (outputDataSet.Tables.Count > 1 && outputDataSet.Tables[1] != null)
                            {
                                tmpItem.Attributes = new List<ItemAttribute>();

                                for (int iMask = 1; iMask <= 15; iMask++)
                                {
                                    ItemAttribute itemAttribute = new ItemAttribute();

                                    if (Utilities.GetIntegerValue(dr["MASK" + iMask.ToString()], 0) > 0)
                                    {
                                        itemAttribute.MaskOrder = iMask;

                                        Answer answer = new Answer();
                                        answer.AnswerCode = Utilities.GetIntegerValue(dr["MASK" + iMask.ToString()], 0);
                                        answer.AnswerText = Utilities.GetStringValue(dr["MASK_DESC" + iMask.ToString()], "");

                                        // Pull from Other Description List Table
                                        if (outputDataSet.Tables[1] != null && answer.AnswerCode == 999)
                                        {
                                            string sOtherDscFilter = string.Empty;
                                            sOtherDscFilter = "STORENUMBER = '" + sStoreNumber + "' ";
                                            sOtherDscFilter += " and ICN_STORE = " + tmpItem.mStore.ToString();
                                            sOtherDscFilter += " and ICN_YEAR = " + tmpItem.mYear.ToString();
                                            sOtherDscFilter += " and ICN_DOC = '" + tmpItem.mDocNumber + "' ";
                                            sOtherDscFilter += " and ICN_DOC_TYPE = " + tmpItem.mDocType;
                                            sOtherDscFilter += " and ICN_ITEM = " + tmpItem.mItemOrder;
                                            sOtherDscFilter += " and ICN_SUB_ITEM = 0";
                                            sOtherDscFilter += " and MASK_SEQ = " + iMask.ToString();

                                            DataRow[] dataOtherDScRows = outputDataSet.Tables[1].Select(sOtherDscFilter);

                                            if (dataOtherDScRows.Count() > 0)
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
                                        tmpItem.Attributes.Add(itemAttribute);
                                }


                            }

                            if (tmpItem.IsJewelry)
                            {
                                if (tmpItem.Jewelry == null)
                                    tmpItem.Jewelry = new List<JewelrySet>();

                                string sMDSEJewelryFilter = "STORENUMBER = '" + sStoreNumber + "' ";
                                sMDSEJewelryFilter += " and ICN_STORE = '" + tmpItem.mStore + "' ";
                                sMDSEJewelryFilter += " and ICN_YEAR = " + tmpItem.mYear.ToString();
                                sMDSEJewelryFilter += " and ICN_DOC = '" + tmpItem.mDocNumber + "' ";
                                sMDSEJewelryFilter += " and ICN_DOC_TYPE = " + tmpItem.mDocType;
                                sMDSEJewelryFilter += " and ICN_ITEM = " + tmpItem.mItemOrder;
                                sMDSEJewelryFilter += " and ICN_SUB_ITEM > 0";

                                DataRow[] dataMsdeJewelryRows = outputDataSet.Tables[0].Select(sMDSEJewelryFilter);

                                for (int iJewelryCount = 0; iJewelryCount < dataMsdeJewelryRows.Count(); iJewelryCount++)
                                {
                                    JewelrySet jewelrySet = new JewelrySet();
                                    DataRow dataMsdeJewelryRow = dataMsdeJewelryRows[iJewelryCount];
                                    jewelrySet.CaccLevel = Utilities.GetIntegerValue(dataMsdeJewelryRow["CACC_LEV"], -1);
                                    jewelrySet.Category = Utilities.GetIntegerValue(dataMsdeJewelryRow["CAT_CODE"], 0);
                                    //tmpItem.IsJewelry = true;
                                    jewelrySet.SubItemNumber = Utilities.GetIntegerValue(dataMsdeJewelryRow["ICN_SUB_ITEM"], 0);
                                    jewelrySet.TotalStoneValue = Utilities.GetDecimalValue(dataMsdeJewelryRow["AMOUNT"], 0.00M);
                                    jewelrySet.Icn = Utilities.IcnGenerator(tmpItem.mStore,
                                                                            tmpItem.mYear,
                                                                            tmpItem.mDocNumber,
                                                                            tmpItem.mDocType,
                                                                            tmpItem.mItemOrder,
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
                                            if (outputDataSet.Tables.Count > 1 && outputDataSet.Tables[1] != null && answer.AnswerCode == 999)
                                            {
                                                string sOtherDscFilter = "STORENUMBER = '" + sStoreNumber + "' ";
                                                sOtherDscFilter += " and ICN_STORE = " + tmpItem.mStore.ToString();
                                                sOtherDscFilter += " and ICN_YEAR = " + tmpItem.mYear.ToString();
                                                sOtherDscFilter += " and ICN_DOC = '" + tmpItem.mDocNumber + "' ";
                                                sOtherDscFilter += " and ICN_DOC_TYPE = " + tmpItem.mDocType;
                                                sOtherDscFilter += " and ICN_ITEM = " + tmpItem.mItemOrder;
                                                sOtherDscFilter += " and ICN_SUB_ITEM = " + Utilities.GetIntegerValue(dataMsdeJewelryRow["ICN_SUB_ITEM"], 0).ToString();
                                                sOtherDscFilter += " and MASK_SEQ = " + iMask.ToString();

                                                DataRow[] dataOtherDScRows = outputDataSet.Tables[1].Select(sOtherDscFilter);

                                                if (dataOtherDScRows.Count() > 0)
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
                                        if (jewelrySet.ItemAttributeList == null)
                                            jewelrySet.ItemAttributeList = new List<ItemAttribute>();

                                        if (itemAttribute.Answer.AnswerCode == 999 || itemAttribute.Answer.AnswerCode > 0)
                                        {
                                            jewelrySet.ItemAttributeList.Add(itemAttribute);
                                        }
                                    }

                                    // jewelrySet.TicketDescription = Utilities.GetStringValue(dataMsdeJewelryRow["MD_DESC"], "");
                                    tmpItem.Jewelry.Add(jewelrySet);
                                }
                            }

                            searchItems.Add(tmpItem);
                        }

                    }
                }

                errorCode = "0";
                errorText = string.Empty;
                return (true);
            }

            errorCode = "SearchForItem";
            errorText = "Operation failed";
            return (false);
        }
        /// <summary>
        /// Wrapper call to insert sale related data
        /// </summary>
        /// <param name="oDa"></param>
        /// <param name="storeNumber"></param>
        /// <param name="statusDate"></param>
        /// <param name="statusTime"></param>
        /// <param name="entityNumber"></param>
        /// <param name="userID"></param>
        /// <param name="icn"></param>
        /// <param name="qty"></param>
        /// <param name="retailPrice"></param>
        /// <param name="entityType"></param>
        /// <param name="refNumber"></param>
        /// <param name="refType"></param>
        /// <param name="salesTax"></param>
        /// <param name="cashDrawer"></param>
        /// <param name="tranType"></param>
        /// <param name="saleAmount"></param>
        /// <param name="shippingHandling"></param>
        /// <param name="jewelryCase"></param>
        /// <param name="saleTicketNumber"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool InsertSaleRecord(
            OracleDataAccessor oDa,
            string storeNumber,
            string statusDate,
            string statusTime,
            string entityNumber,
            string userID,
            List<string> icn,
            List<int> qty,
            List<string> retailPrice,
            string entityType,
            string refNumber,
            string refType,
            string salesTax,
            string cashDrawer,
            string tranType,
            string saleAmount,
            string shippingHandling,
            List<string> jewelryCase,
            string custDispIdNum,
            string custDispIdType,
            string custDispIDCode, 
            out int saleTicketNumber,
            out string errorCode,
            out string errorText)
        {
            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;
            saleTicketNumber = 0;

            OracleDataAccessor dA = oDa;

            //Create parameter list

            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_status_date", statusDate));
            oParams.Add(new OracleProcParam("p_status_time", statusTime));
            oParams.Add(new OracleProcParam("p_entity_number", entityNumber));
            oParams.Add(new OracleProcParam("p_user_id", userID));
            oParams.Add(new OracleProcParam("p_icn", true, icn));
            oParams.Add(new OracleProcParam("p_qty", true, qty));
            oParams.Add(new OracleProcParam("p_retail_price", true, retailPrice));
            oParams.Add(new OracleProcParam("p_entity_type", entityType));
            oParams.Add(new OracleProcParam("p_ref_number", refNumber));
            oParams.Add(new OracleProcParam("p_ref_type", refType));
            oParams.Add(new OracleProcParam("p_sales_tax", salesTax));
            oParams.Add(new OracleProcParam("p_cash_drawer", cashDrawer));
            oParams.Add(new OracleProcParam("p_tran_type", tranType));
            oParams.Add(new OracleProcParam("p_sale_amount", saleAmount));
            oParams.Add(new OracleProcParam("p_shipping_handling", shippingHandling));
            oParams.Add(new OracleProcParam("p_jewelry_case", true, jewelryCase));
            // CR# 15166
            oParams.Add(new OracleProcParam("p_cust_id_num", custDispIdNum));
            oParams.Add(new OracleProcParam("p_cust_id_type", custDispIdType));
            oParams.Add(new OracleProcParam("p_cust_id_agency", custDispIDCode));
            
            oParams.Add(new OracleProcParam("o_sale_ticket_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            //Make stored proc call
            bool retVal;
            DataSet outDSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            //"ed_pawn_retail_15166", "insert_sale_header", oParams, null, "o_return_code",
                                                                                            "pawn_retail", "insert_sale_header", oParams, null, "o_return_code",
                                                                                            "o_return_text", out outDSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertSaleRecord Failed", oEx);
                errorCode = "ExecuteInsertSaleRecordFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertSaleRecord Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- ExecuteInsertSaleRecordFailed";
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
                    object saleNumber = dr.ItemArray.GetValue(1);
                    if (saleNumber != null)
                    {
                        saleTicketNumber = Int32.Parse((string)saleNumber);

                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }
            return (false);
        }

        public static bool InsertRetailPaymentDetails(
            OracleDataAccessor Oda,
            string storeNumber,
            string cashDrawer,
            string userName,
            string receiptDate,
            List<string> saleTktNumber,
            List<string> refDate,
            List<string> refTime,
            List<string> refEvent,
            List<string> refAmount,
            string statusTime,
            List<string> tenderType,
            List<string> tenderAmount,
            List<string> paymentAuth,
            List<string> topsDocType,
            List<string> topsPaymentType,
            out int receiptNumber,
            out string errorCode,
            out string errorText)
        {
            receiptNumber = 0;
            errorCode = string.Empty;
            errorText = string.Empty;
            OracleDataAccessor dA = Oda;
            //Create parameter list

            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_cashdrawer", cashDrawer));
            oParams.Add(new OracleProcParam("p_user_name", userName));
            oParams.Add(new OracleProcParam("p_receipt_date", receiptDate));
            oParams.Add(new OracleProcParam("p_sale_ticket", true, saleTktNumber));
            oParams.Add(new OracleProcParam("p_ref_date", true, refDate));
            oParams.Add(new OracleProcParam("p_ref_time", true, refTime));
            oParams.Add(new OracleProcParam("p_ref_event", true, refEvent));
            oParams.Add(new OracleProcParam("p_ref_amt", true, refAmount));
            oParams.Add(new OracleProcParam("p_status_time", statusTime));
            oParams.Add(new OracleProcParam("p_payment_type", true, tenderType));
            oParams.Add(new OracleProcParam("p_payment_amt", true, tenderAmount));
            oParams.Add(new OracleProcParam("p_payment_auth", true, paymentAuth));
            oParams.Add(new OracleProcParam("p_tops_payment_type", true, topsPaymentType));
            oParams.Add(new OracleProcParam("p_tops_doc_type", true, topsDocType));
            oParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            //Make stored proc call
            bool retVal;
            DataSet outDSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "pawn_retail", "insert_retail_payment_details", oParams, null, "o_return_code",
                                                                                            "o_return_text", out outDSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("InsertRetailPaymentDetails Failed", oEx);
                errorCode = "InsertRetailPaymentDetailsFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("InsertRetailPaymentDetails Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- InsertRetailPaymentDetailsFailed";
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

        public static bool RefundLayawayPayment(
            OracleDataAccessor Oda,
            string storeNumber,
            string cashDrawer,
            string userName,
            string receiptDate,
            string receiptId,
            int layawayNumber,
            List<string> refDate,
            List<string> refTime,
            List<string> refEvent,
            List<string> refAmount,
            string statusTime,
            List<string> tenderType,
            List<string> tenderAmount,
            List<string> paymentAuth,
            out int receiptNumber,
            out string errorCode,
            out string errorText)
        {
            receiptNumber = 0;
            errorCode = "0";
            errorText = "Success";
            OracleDataAccessor dA = Oda;

            //Create parameter list

            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_cashdrawer", cashDrawer));
            oParams.Add(new OracleProcParam("p_user_name", userName));
            oParams.Add(new OracleProcParam("p_receipt_date", receiptDate));
            oParams.Add(new OracleProcParam("p_receipt_number", receiptId));
            oParams.Add(new OracleProcParam("p_layaway_number", layawayNumber));
            oParams.Add(new OracleProcParam("p_ref_date", true, refDate));
            oParams.Add(new OracleProcParam("p_ref_time", true, refTime));
            oParams.Add(new OracleProcParam("p_ref_event", true, refEvent));
            oParams.Add(new OracleProcParam("p_ref_amt", true, refAmount));
            oParams.Add(new OracleProcParam("p_status_time", statusTime));
            oParams.Add(new OracleProcParam("p_payment_type", true, tenderType));
            oParams.Add(new OracleProcParam("p_payment_amt", true, tenderAmount));
            oParams.Add(new OracleProcParam("p_payment_auth", true, paymentAuth));

            oParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));
            //Make stored proc call
            bool retVal;
            DataSet outDSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "pawn_retail", "refund_layaway_payment", oParams, null, "o_return_code",
                                                                                            "o_return_text", out outDSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("RefundLayawayPayment Failed", oEx);
                errorCode = "IRefundLayawayPaymentFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("RefundLayawayPayment Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- RefundLayawayPaymentFailed";
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
                    object rcptNumber = dr.ItemArray.GetValue(1);
                    if (rcptNumber != null)
                    {
                        receiptNumber = Int32.Parse((string)rcptNumber);

                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }
            return (false);
        }

        public static string GenerateTempICN(DesktopSession desktopSession, int mStore, int mYear)
        {
            return GenerateTempICN(desktopSession, desktopSession.CurrentSiteId.StoreNumber, mStore, mYear);
        }

        public static string GenerateTempICN(DesktopSession desktopSession, string storenumber, int mStore, int mYear)
        {
            string newICN = string.Empty;
            long nextTktNumber;
            //Start transaction block
            desktopSession.beginTransactionBlock();

            string errorCode, errorDesc;
            string storeNum = storenumber;
            bool rt = ProcessTenderProcedures.ExecuteGetNextNumber(
                storeNum,
                "TEMP", ShopDateTime.Instance.ShopDate,
                out nextTktNumber,
                out errorCode,
                out errorDesc);

            if (!rt)
            {
                desktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                BasicExceptionHandler.Instance.AddException("Failed to retrieve ticket number", new ApplicationException());
                return (newICN);
            }
            desktopSession.endTransactionBlock(EndTransactionType.COMMIT);
            newICN = Utilities.IcnGenerator(mStore,
                                            mYear,
                                            (int)nextTktNumber,
                                            DOC_TYPE_FOR_TEMPICN,
                                            1,
                                            0);

            return newICN;
        }

        public static bool GetStoreTaxInfo(
            OracleDataAccessor oDa,
            string storeNumber,
            out List<StoreTaxVO> storeTaxInfo,
            out string errorCode,
            out string errorMesg)
        {
            //Set default output params
            storeTaxInfo = new List<StoreTaxVO>();

            if (oDa == null)
            {
                errorCode = "GetStoreTaxInfoFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>
            {
                new OracleProcParam("p_store_number", storeNumber)
            };

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_store_salestax_list", "store_salestax_details"));
                DataSet outputDataSet;
                bool retVal;
                try
                {
                    retVal = oDa.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_retail",
                        "extract_sales_tax_details", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling getstorebankinfo stored procedure", oEx);
                    errorCode = "GetStoreTaxInfo";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oDa.ErrorCode;
                    errorMesg = oDa.ErrorDescription;
                    return (false);
                }
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    foreach (DataRow dr in outputDataSet.Tables[0].Rows)
                    {
                        StoreTaxVO storeTax = new StoreTaxVO();
                        storeTax.TaxAuth = dr["tax_auth"].ToString();
                        storeTax.TaxCode = dr["tax_code"].ToString();
                        storeTax.TaxRate = Utilities.GetDecimalValue(dr["tax_rate"], 0);
                        storeTax.TaxAmount = Utilities.GetDecimalValue(dr["tax_amount"], 0);
                        storeTaxInfo.Add(storeTax);
                    }
                    errorCode = string.Empty;
                    errorMesg = string.Empty;
                    return true;
                }

                errorCode = "GetStoreTaxInfoFailed";
                errorMesg = "Operation failed";
                return (false);
            }
            errorCode = "GetStoreTaxInfoFailed";
            errorMesg = "No valid input parameters given";
            return (false);
        }

        /// <summary>
        /// Wrapper call to insert layaway related data
        /// </summary>
        /// <param name="oDa"></param>
        /// <param name="storeNumber"></param>
        /// <param name="statusDate"></param>
        /// <param name="statusTime"></param>
        /// <param name="customerNumber"></param>
        /// <param name="layawayAmount"></param>
        /// <param name="numberOfPayments"></param>
        /// <param name="downPayment"></param>
        /// <param name="monthlyPayment"></param>
        /// <param name="firstPaymentDate"></param>
        /// <param name="nextPaymentDate"></param>
        /// <param name="lastPaymentDate"></param>
        /// <param name="userID"></param>
        /// <param name="icn"></param>
        /// <param name="qty"></param>
        /// <param name="retailPrice"></param>
        /// <param name="entityType"></param>
        /// <param name="refNumber"></param>
        /// <param name="refType"></param>
        /// <param name="salesTax"></param>
        /// <param name="cashDrawer"></param>
        /// <param name="tranType"></param>
        /// <param name="layawayTicketNumber"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool InsertLayawayRecord(
            OracleDataAccessor oDa,
            string storeNumber,
            string statusDate,
            string statusTime,
            string customerNumber,
            string layawayAmount,
            int numberOfPayments,
            decimal downPayment,
            decimal monthlyPayment,
            string firstPaymentDate,
            string nextPaymentDate,
            string lastPaymentDate,
            string userID,
            List<string> icn,
            List<int> qty,
            List<string> retailPrice,
            string entityType,
            string refNumber,
            string refType,
            string salesTax,
            string cashDrawer,
            string tranType,
            List<string> jewelryCase,
            out int layawayTicketNumber,
            out string errorCode,
            out string errorText)
        {
            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;
            layawayTicketNumber = 0;

            OracleDataAccessor dA = oDa;

            //Create parameter list

            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_status_date", statusDate));
            oParams.Add(new OracleProcParam("p_status_time", statusTime));
            oParams.Add(new OracleProcParam("p_customer_number", customerNumber));
            oParams.Add(new OracleProcParam("p_layaway_amount", layawayAmount));
            oParams.Add(new OracleProcParam("p_number_of_payments", numberOfPayments));
            oParams.Add(new OracleProcParam("p_down_payment", downPayment));
            oParams.Add(new OracleProcParam("p_monthly_payment", monthlyPayment));
            oParams.Add(new OracleProcParam("p_first_payment", firstPaymentDate));
            oParams.Add(new OracleProcParam("p_next_payment", nextPaymentDate));
            oParams.Add(new OracleProcParam("p_last_payment", lastPaymentDate));
            oParams.Add(new OracleProcParam("p_user_id", userID));
            oParams.Add(new OracleProcParam("p_icn", true, icn));
            oParams.Add(new OracleProcParam("p_qty", true, qty));
            oParams.Add(new OracleProcParam("p_retail_price", true, retailPrice));
            oParams.Add(new OracleProcParam("p_entity_type", entityType));
            oParams.Add(new OracleProcParam("p_ref_number", refNumber));
            oParams.Add(new OracleProcParam("p_ref_type", refType));
            oParams.Add(new OracleProcParam("p_sales_tax", salesTax));
            oParams.Add(new OracleProcParam("p_cash_drawer", cashDrawer));
            oParams.Add(new OracleProcParam("p_tran_type", tranType));
            oParams.Add(new OracleProcParam("p_jewelry_case", true, jewelryCase));
            oParams.Add(new OracleProcParam("o_layaway_ticket_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            //Make stored proc call
            bool retVal;
            DataSet outDSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "pawn_retail", "insert_layaway_header", oParams, null, "o_return_code",
                                                                                            "o_return_text", out outDSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertLayawayRecord Failed", oEx);
                errorCode = "ExecuteInsertLayawayRecordFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteInsertLayawayRecord Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- ExecuteInsertLayawayRecordFailed";
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
                    object layawayNumber = dr.ItemArray.GetValue(1);
                    if (layawayNumber != null)
                    {
                        layawayTicketNumber = Int32.Parse((string)layawayNumber);

                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }
            return (false);
        }

        public static bool ProcessLayawayPickup(
            OracleDataAccessor oDa,
            string storeNumber,
            string customerNumber,
            string layawayTicketNumber,
            string statusDate,
            string statusTime,
            string userName,
            List<string> icn,
            string cashDrawer,
            string backgroundCheckNumber,
            out int saleTicketNumber,
            out string errorCode,
            out string errorText)
        {
            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;
            saleTicketNumber = 0;

            OracleDataAccessor dA = oDa;

            //Create parameter list

            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_ticket_number", layawayTicketNumber));
            oParams.Add(new OracleProcParam("p_cash_drawer", cashDrawer));
            oParams.Add(new OracleProcParam("p_customer_number", customerNumber));
            oParams.Add(new OracleProcParam("p_icn", true, icn));
            oParams.Add(new OracleProcParam("p_status_date", statusDate));
            oParams.Add(new OracleProcParam("p_status_time", statusTime));
            oParams.Add(new OracleProcParam("p_user_name", userName));
            oParams.Add(new OracleProcParam("p_background_chk", backgroundCheckNumber));

            oParams.Add(new OracleProcParam("o_sale_ticket_number", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            //Make stored proc call
            bool retVal;
            DataSet outDSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "pawn_retail", "process_layaway_payout", oParams, null, "o_return_code",
                                                                                            "o_return_text", out outDSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteProcessLayawayPayout Failed", oEx);
                errorCode = "ExecuteProcessLayawayPayoutFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteProcessLayawayPayout Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- ExecuteProcessLayawayPayoutFailed";
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
                    object saleNumber = dr.ItemArray.GetValue(1);
                    if (saleNumber != null)
                    {
                        saleTicketNumber = Int32.Parse((string)saleNumber);

                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }
            return (false);
        }

        public static bool LayawayPaymentRefund(DesktopSession desktopSession, LayawayVO currentlayaway, string receiptToRefund, decimal refundAmount)
        {
            List<string> tenderTypes = new List<string>();
            List<string> tenderAmount = new List<string>();
            List<string> tenderAuth = new List<string>();

            desktopSession.beginTransactionBlock();
            int receiptNumber;
            string errorCode;
            string errorText;

            List<string> refDate = new List<string>();
            List<string> refNumber = new List<string>();
            List<string> refType = new List<string>();
            List<string> refEvent = new List<string>();
            List<string> refAmount = new List<string>();
            List<string> refStore = new List<string>();
            List<string> refTime = new List<string>();

            refDate.Add(ShopDateTime.Instance.ShopDate.FormatDate());

            //ref time for layaway is the current time
            refTime.Add(ShopDateTime.Instance.ShopTransactionTime);

            // ref number for layaway is the ticket number
            refNumber.Add(currentlayaway.TicketNumber.ToString());

            // ref type for layaway is 4
            refType.Add("4");

            // ref event for layaway payment is "LAYPMT"
            refEvent.Add(ReceiptEventTypes.LAYREF.ToString());

            // ref amount is the refund amount
            refAmount.Add(refundAmount.ToString());

            // ref store for layaway is the store the refund is being processed
            refStore.Add(desktopSession.CurrentSiteId.StoreNumber);

            List<string> topsPaymentType = tenderTypes.Select(s => "R").ToList();
            List<string> topsDocType = tenderTypes.Select(s => "4").ToList();

            bool retValue = InsertRetailPaymentDetails(
                GlobalDataAccessor.Instance.OracleDA, desktopSession.CurrentSiteId.StoreNumber,
                desktopSession.CashDrawerName,
                desktopSession.FullUserName,
                ShopDateTime.Instance.ShopDate.FormatDate(),
                refNumber,
                refDate, refTime, refEvent, refAmount, ShopDateTime.Instance.ShopTransactionTime.ToString(),
                tenderTypes,
                tenderAmount,
                tenderAuth,
                topsDocType,
                topsPaymentType,
                out receiptNumber,
                out errorCode,
                out errorText);

            desktopSession.endTransactionBlock(!retValue ? EndTransactionType.ROLLBACK : EndTransactionType.COMMIT);
            if (!retValue)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, null, "Error in Layaway refund payment " + errorText);
            }
            return retValue;
        }

        public static bool MaintainStoreCredit(
            OracleDataAccessor oDa,
            string customerNumber,
            string storeNumber,
            decimal amount,
            Int32 refNumber,
            string refType,
            Int32 ticketNumber,
            string docType,
            string userId,
            string statusDate,
            string statusTime,
            out string errorCode,
            out string errorText)
        {
            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;

            OracleDataAccessor dA = oDa;

            //Create parameter list

            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_customer_number", customerNumber));
            oParams.Add(new OracleProcParam("p_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_status_date", statusDate));
            oParams.Add(new OracleProcParam("p_status_time", statusTime));
            oParams.Add(new OracleProcParam("p_ref_number", refNumber));
            oParams.Add(new OracleProcParam("p_ref_type", refType));
            oParams.Add(new OracleProcParam("p_ticket_number", ticketNumber));
            oParams.Add(new OracleProcParam("p_doc_type", docType));
            oParams.Add(new OracleProcParam("p_user_id", userId));
            oParams.Add(new OracleProcParam("p_amount", amount));

            //Make stored proc call
            bool retVal;
            DataSet outDSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "pawn_retail", "maintain_store_credit", oParams, null, "o_return_code",
                                                                                            "o_return_text", out outDSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("MaintainStoreCredit Failed", oEx);
                errorCode = "MaintainStoreCreditFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("MaintainStoreCredit Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- MaintainStoreCreditFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }
            return true;
        }

        public static bool ProcessLayawayServices(
            DesktopSession desktopSession,
            List<LayawayVO> layaways,
            string storeNumber,
            string statusDate,
            string statusTime,
            ProductStatus productStatus,
            out decimal restockingFee,
            out string errorCode,
            out string errorText)
        {
            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;

            const string feeRefType = "LAY";
            string feeOpRevCode = FeeRevOpCodes.LAY.ToString();
            if (productStatus == ProductStatus.TERM)
                feeOpRevCode = FeeRevOpCodes.TERMLAYAWAY.ToString();
            else if (productStatus == ProductStatus.FORF)
                feeOpRevCode = FeeRevOpCodes.FORFLAYAWAY.ToString();
            string feeStateCode = FeeStates.PAID.ToString();

            List<int> ticketNumbers = new List<int>();
            List<decimal> forfeitureAmounts = new List<decimal>();
            List<decimal> restockingFeeAmounts = new List<decimal>();

            restockingFee = new BusinessRulesProcedures(desktopSession).GetLayawayRestockingFee(desktopSession.CurrentSiteId);
            foreach (LayawayVO layaway in layaways)
            {
                ticketNumbers.Add(layaway.TicketNumber);
                decimal forfeitAmount = layaway.GetAmountPaid();
                forfeitureAmounts.Add(forfeitAmount);
                decimal rFee = restockingFee;
                if (restockingFee > forfeitAmount)
                    rFee = forfeitAmount;
                restockingFeeAmounts.Add(rFee);
            }

            desktopSession.beginTransactionBlock();

            try
            {
                long receiptNumber;
                if (!UpdateLayawayServices(desktopSession, ticketNumbers, forfeitureAmounts, restockingFeeAmounts, storeNumber, statusDate, statusTime, productStatus, out receiptNumber, out errorCode, out errorText))
                {
                    throw new ApplicationException("Could not forfeit layaways");
                }

                for (int i = 0; i < ticketNumbers.Count; i++)
                {
                    if (!ProcessTenderProcedures.ExecuteInsertUpdatePawnFee(new List<string>() { FeeTypes.RESTOCKINGFEE.ToString() },
                                                                            feeRefType, ticketNumbers[i], storeNumber, new List<string>() { restockingFeeAmounts[i].ToString() }, new List<string>() { "0" }, new List<string>() { "0" },
                                                                            new List<string>() { statusDate }, new List<string>() { feeStateCode }, desktopSession.FullUserName.ToLowerInvariant(), feeOpRevCode, receiptNumber, out errorCode, out errorText))
                    {
                        throw new ApplicationException("Could not forfeit or terminate layaways");
                    }
                }
            }
            catch (Exception e)
            {
                desktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, typeof(RetailProcedures), "Could not update or insert fee data");
                }
                BasicExceptionHandler.Instance.AddException("Could not forfeit layaways - update fees failed :" + errorText + ": " + errorText, e);
                return false;
            }

            desktopSession.endTransactionBlock(EndTransactionType.COMMIT);
            return true;
        }

        private static bool UpdateLayawayServices(
            DesktopSession desktopSession,
            List<int> ticketNumbers,
            List<decimal> forfeitureAmounts,
            List<decimal> restockingFeeAmounts,
            string storeNumber,
            string statusDate,
            string statusTime,
            ProductStatus productStatus,
            out long receiptNumber,
            out string errorCode,
            out string errorText)
        {
            //Initialize output vars
            errorCode = string.Empty;
            errorText = string.Empty;
            receiptNumber = 0;

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            List<string> refEvents = new List<string>();
            List<string> refDates = new List<string>();
            List<string> refTimes = new List<string>();

            for (int i = 0; i < ticketNumbers.Count; i++)
            {
                refEvents.Add(productStatus.ToString());
                refDates.Add(statusDate);
                refTimes.Add(statusTime);
            }

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_ticket_number", true, ticketNumbers.ToStringList()));
            oParams.Add(new OracleProcParam("p_forfeiture_amount", true, forfeitureAmounts.ToStringList()));
            oParams.Add(new OracleProcParam("p_misc_inc", true, restockingFeeAmounts.ToStringList()));
            oParams.Add(new OracleProcParam("p_store_number", storeNumber));
            oParams.Add(new OracleProcParam("p_status_date", statusDate));
            oParams.Add(new OracleProcParam("p_status_time", statusTime));
            oParams.Add(new OracleProcParam("p_ref_date", true, refDates));
            oParams.Add(new OracleProcParam("p_ref_time", true, refTimes));
            oParams.Add(new OracleProcParam("p_ref_event", true, refEvents));
            oParams.Add(new OracleProcParam("p_ref_amt", true, forfeitureAmounts.ToStringList()));
            oParams.Add(new OracleProcParam("p_user_id", desktopSession.FullUserName));
            oParams.Add(new OracleProcParam("p_cashdrawer_name", desktopSession.CashDrawerName));
            oParams.Add(new OracleProcParam("o_receipt_number", OracleDbType.Int64, DBNull.Value, ParameterDirection.Output, 1));

            //Make stored proc call
            bool retVal;
            DataSet outDSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "pawn_retail", "process_layaway_services", oParams, null, "o_return_code",
                                                                                            "o_return_text", out outDSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteProcessLayawayForfeiture Failed", oEx);
                errorCode = "ExecuteProcessLayawayForfeitureFailed";
                errorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteProcessLayawayForfeiture Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " --- ExecuteProcessLayawayForfeiture";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            DataTable outputDt = outDSet.Tables["OUTPUT"];
            if (outputDt != null && outputDt.IsInitialized &&
                outputDt.Rows != null && outputDt.Rows.Count > 0)
            {
                DataRow dr = outputDt.Rows[0];
                if (dr != null && dr.ItemArray.Length > 0)
                {
                    object receiptNumberObj = dr.ItemArray.GetValue(1);
                    if (receiptNumberObj != null)
                    {
                        receiptNumber = Utilities.GetLongValue(dr.ItemArray.GetValue(1));
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

        //----TLM to support Item History
        public static bool GetCustomerSales(
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
                errorCode = "GetCustomerSales";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetCustomerSalesFailed", new ApplicationException("Cannot execute the Get Customer Sales stored procedure"));
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
            inParams.Add(new OracleProcParam("p_transaction_type", "SALE"));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_sales_data", SALE_DATA));
            refCursors.Add(new PairType<string, string>("o_pawn_mdselist", MDSE_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_gunlist", GUN_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_mdhistlist", MDHIST_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_otherdsclist", OTHERDSC_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_receiptdetlist", RECEIPTDET_LIST));
            refCursors.Add(new PairType<string, string>("o_sale_payment_types", TENDER_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_feeslist", FEE_LIST));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner",
                    "pawn_retail", "Get_Customer_Sales",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = " -- GetCustomerSales";
                errorText = " -- Invocation of GetCustomerSales stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking Get_Customer_Sales stored proc", oEx);
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

            errorCode = "GetCustomerSalesFailed";
            errorText = "Operation failed";
            return (false);
        }

        public static bool GetCustomerSales(DesktopSession desktopSession, string custNumber)
        {
            bool retValue = false;
            DataSet custSale;
            string errorCode;
            string errorText;
            List<SaleVO> sales;
            //Set end date to be shopdate
            string toDate = ShopDateTime.Instance.ShopDate.ToShortDateString();

            retValue = GetCustomerSales(GlobalDataAccessor.Instance.OracleDA, custNumber, ProductStatus.ALL, StateStatus.BLNK, null, toDate, out custSale, out errorCode, out errorText);
            if (retValue)
            {
                ParseSaleData(custSale, out sales);

                if (sales != null)
                    desktopSession.CustomerHistorySales = sales;
            }

            return retValue;
        }

        public static bool GetCustomerLayaways(
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
                errorCode = "GetCustomerLayaways";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetCustomerLayawaysFailed", new ApplicationException("Cannot execute the Get Customer Layaways stored procedure"));
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
            inParams.Add(new OracleProcParam("p_transaction_type", "LAYAWAY"));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_layaway_data", LAYAWAY_DATA));
            refCursors.Add(new PairType<string, string>("o_pawn_mdselist", MDSE_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_gunlist", GUN_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_mdhistlist", MDHIST_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_otherdsclist", OTHERDSC_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_receiptdetlist", RECEIPTDET_LIST));
            refCursors.Add(new PairType<string, string>("o_sale_payment_types", TENDER_LIST));
            refCursors.Add(new PairType<string, string>("o_pawn_feeslist", FEE_LIST));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_retail", "Get_Customer_Layaways",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = " -- GetCustomerSales";
                errorText = " -- Invocation of GetCustomerLayaways stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking Get_Customer_Layaways stored proc", oEx);
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

            errorCode = "GetCustomerLayawaysFailed";
            errorText = "Operation failed";
            return (false);
        }

        public static bool GetCustomerLayaways(DesktopSession desktopSession, string custNumber)
        {
            bool retValue = false;
            DataSet custSale;
            string errorCode;
            string errorText;
            List<LayawayVO> layaways;
            //Set end date to be shopdate
            string toDate = ShopDateTime.Instance.ShopDate.ToShortDateString();

            retValue = GetCustomerLayaways(GlobalDataAccessor.Instance.OracleDA, custNumber, ProductStatus.ALL, StateStatus.BLNK, null, toDate, out custSale, out errorCode, out errorText);
            if (retValue)
            {
                ParseLayawayData(custSale, out layaways);

                if (layaways != null)
                    desktopSession.CustomerHistoryLayaways = layaways;
            }

            return retValue;
        }

        /// <summary>
        /// Executes the insert_receipt_details stored procedure
        /// </summary>
        /// <param name="oDa"></param>
        /// <param name="storeNumber"></param>
        /// <param name="entId"></param>
        /// <param name="receiptDate"></param>
        /// <param name="userId"></param>
        /// <param name="refDates"></param>
        /// <param name="refTimes"></param>
        /// <param name="refNumbers"></param>
        /// <param name="refTypes"></param>
        /// <param name="refEvents"></param>
        /// <param name="refAmounts"></param>
        /// <param name="refStores"></param>
        /// <param name="receiptNumber"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool InsertReceiptDetail(
            OracleDataAccessor oDa,
            string storeNumber,
            string entId,
            string receiptDate,
            string userId,
            List<string> refDates,
            List<string> refTimes,
            List<string> refNumbers,
            List<string> refTypes,
            List<string> refEvents,
            List<string> refAmounts,
            List<string> refStores,
            Int64 receiptNumber,
            out string errorCode,
            out string errorText)
        {
            // Initialize the error code and text output values
            errorCode = string.Empty;
            errorText = string.Empty;

            var inParams = new List<OracleProcParam>();

            // Ensure the data accessor is valid
            if (oDa == null)
            {
                BasicExceptionHandler.Instance.AddException("InsertReceiptDetail Failed",
                                                            new ApplicationException("InsertReceiptDetail Failed: Data accessor instance is invalid"));
                return (false);
            }

            inParams.Add(new OracleProcParam("instorenumber", storeNumber));
            inParams.Add(new OracleProcParam("inentid", entId));
            inParams.Add(new OracleProcParam("inreceiptdate", receiptDate));
            inParams.Add(new OracleProcParam("inuserid", userId));
            inParams.Add(new OracleProcParam("p_ref_date", true, refDates));
            inParams.Add(new OracleProcParam("p_ref_time", true, refTimes));
            inParams.Add(new OracleProcParam("p_ref_number", true, refNumbers));
            inParams.Add(new OracleProcParam("p_ref_type", true, refTypes));
            inParams.Add(new OracleProcParam("p_ref_event", true, refEvents));
            inParams.Add(new OracleProcParam("p_ref_amt", true, refAmounts));
            inParams.Add(new OracleProcParam("p_ref_store", true, refStores));
            inParams.Add(new OracleProcParam("p_receipt_number", receiptNumber));

            DataSet outputDataSet;
            bool retVal;

            try
            {
                retVal = oDa.issueSqlStoredProcCommand("ccsowner",
                                                       "pawn_receipt_procs", "insert_receipt_details", inParams, null, "o_return_code",
                                                       "o_return_text", out outputDataSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("InsertReceiptDetail Failed", oEx);
                errorCode = " --- InsertReceiptDetailFailed";
                errorText = " --- OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("InsertReceiptDetail Failed: return value is false", new ApplicationException());
                errorCode = oDa.ErrorCode + " --- InsertReceiptDetailFailed";
                errorText = oDa.ErrorDescription + " --- Return value is false";
                return (false);
            }

            errorCode = "0";
            errorText = "Success";

            return (true);
        }

        public static bool SetLayawayTempStatus(
            int ticketNumber,
            string storeNumber,
            string tempStatus,
            out string errorCode,
            out string errorText)
        {
            bool retval = true;
            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            OracleProcParam maskParam = new OracleProcParam("p_ticket_number", ticketNumber);
            inParams.Add(maskParam);

            OracleProcParam maskTempStatusParam = new OracleProcParam("p_temp_status", tempStatus);
            inParams.Add(maskTempStatusParam);

            OracleProcParam maskStoreNumberParam = new OracleProcParam("p_store_number", storeNumber);
            inParams.Add(maskStoreNumberParam);


            ////Setup ref cursor array
            //List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            //refCursors.Add(new PairType<string, string>("o_temp_status", "temp_status"));

            DataSet outputDataSet;
            //Create output data set names
            try
            {
                retval = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_MANAGE_HOLDS", "set_layaway_temp_status",
                    inParams, null, "o_return_code", "o_return_text",
                    out outputDataSet);

            }
            catch (OracleException oEx)
            {
                errorCode = "check_for_temp_status Failed";
                errorText = "Invocation of check_for_temp_status stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking check_for_temp_status stored proc", oEx);
                return (false);
            }

            errorCode = dA.ErrorCode;
            errorText = dA.ErrorDescription;

            return retval;
        }


    }
}
