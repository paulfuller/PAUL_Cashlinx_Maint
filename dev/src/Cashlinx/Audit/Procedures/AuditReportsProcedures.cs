using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;
using Reports.Audit;

namespace Common.Controllers.Database
{
    public class AuditReportsProcedures
    {
        private static readonly string TBL_TRAKKER_TMP_ICN = "o_tmp_icn";
        private static readonly string TBL_TRAKKER_MISSING_ITEMS = "o_msg_items";
        private static readonly string TBL_TRAKKER_NXT_ITEMS = "o_nxt_items";
        private static readonly string TBL_TRAKKER_UNEXP_ITEMS = "o_unexp_items";
        private static readonly string TBL_TRAKKER_DUP_ITEMS = "o_dup_items";

        private static readonly string TBL_POSTAUDIT_INVTOTALS = "o_stat_ttl";
        private static readonly string TBL_POSTAUDIT_CHARGEOFF = "o_chgOff";
        private static readonly string TBL_POSTAUDIT_REACTIVATION = "o_reactvt";
        private static readonly string TBL_POSTAUDIT_CHARGEON = "o_chg_on";
        private static readonly string TBL_POSTAUDIT_TEMPICNRECONCILLIATION = "o_adjust";

 
        private static readonly string TBL_INVENTORYSUMMARY_PSNL = "o_psnl";
        private static readonly string TBL_INVENTORYSUMMARY_TERM = "o_term";
        private static readonly string TBL_INVENTORYSUMMARY_CHARGEOFF = "o_chgOff";
        private static readonly string TBL_INVENTORYSUMMARY_HIST = "o_Hist";
        private static readonly string TBL_INVENTORYSUMMARY_CHARGEOFF_CACC = "o_cacc_info";

        public static readonly string PREAUDIT_CAT_SMRY = "Ctgry_summary";
        public static readonly string PREAUDIT_CACC_SMRY = "CACC_summary";
        public static readonly string MISSING_ITEMS = "ItemList";

       

        public static bool GetInventorySummaryReportFieldsCACC(ref List<AuditReportsObject.InventorySummaryChargeOffsField> chargeoffields, string storenumber, int auditID, CommonDatabaseContext dataContext)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            string lastInvAudit = string.Empty;
            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_storenumber", storenumber));
            oParams.Add(new OracleProcParam("p_AuditID", auditID));
            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            //refCursors.Add(new PairType<string, string>("o_last_inv_audit", "o_last_inv_audit"));
            refCursors.Add(new PairType<string, string>("o_cacc_info", "o_cacc_info"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "AUDIT_REPORTS", "getCACCInfo", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("GetInventorySummaryReportFields Failed", oEx);
                dataContext.ErrorCode = "GetInventorySummaryReportFields";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("GetInventorySummaryReportFields Failed: return value is false", new ApplicationException());
                dataContext.ErrorCode = dA.ErrorCode + " --- GetInventorySummaryReportFields";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null)
            {
                return false;
            }
            if (outputDataSet.Tables.Count > 0)
            {
                try
                {
                    if (outputDataSet.Tables[TBL_INVENTORYSUMMARY_CHARGEOFF_CACC] != null && outputDataSet.Tables[TBL_INVENTORYSUMMARY_CHARGEOFF_CACC].Rows.Count > 0)
                    {
                        foreach (DataRow dr in outputDataSet.Tables[TBL_INVENTORYSUMMARY_CHARGEOFF_CACC].Rows)
                        {
                            AuditReportsObject.InventorySummaryChargeOffsField field = new AuditReportsObject.InventorySummaryChargeOffsField();
                            field.Category = Utilities.GetIntegerValue(dr["CAT_CODE"], 0);
                            field.CategoryDescription = Utilities.GetStringValue(dr["CAT_DESC"], string.Empty);
                            field.TotalItems = Utilities.GetIntegerValue(dr["ORG_QTY"], 0);
                            field.TotalAmount = Utilities.GetDecimalValue(dr["ORG_COST"], 0.00M);
                            field.TotalItemsNF = Utilities.GetIntegerValue(dr["NEW_QTY"], 0);
                            field.TotalAmountNF = Utilities.GetDecimalValue(dr["NEW_COST"], 0.00M);

                            if (field.TotalItems > field.TotalItemsNF)
                                field.TotalItemsNF = field.TotalItems - field.TotalItemsNF;
                            else
                                field.TotalItemsNF = 0;

                            if (field.TotalAmount > field.TotalAmountNF)
                                field.TotalAmountNF = field.TotalAmount - field.TotalAmountNF;
                            else
                                field.TotalAmountNF = 0.00m;
                            

                            field.PercentItemsNF = (field.TotalItemsNF > 0 && field.TotalItems > 0) ? Math.Round((decimal)field.TotalItemsNF / (decimal)field.TotalItems * 100, 2) : 0.00M;
                            field.PercentNFAmount = (field.TotalAmountNF > 0.00m & field.TotalAmount > 0.00m) ? Math.Round(field.TotalAmountNF / field.TotalAmount * 100, 2) : 0.00M;
                            chargeoffields.Add(field);
                        }
                    }
                }
                catch (Exception e)
                {
                    return (false);
                }
            }
            dataContext.ErrorCode = "0";
            dataContext.ErrorText = "Success";
            return true;
        }

        public static bool GetInventorySummaryReportFields(ref List<AuditReportsObject.InventorySummaryChargeOffsField> listInvChargeoffields,
            ref List<AuditReportsObject.InventorySummaryHistoryField> listInvHistoryfields,
            ref StringBuilder stringbuilderEmployeesPresent,
            ref StringBuilder stringbuilderTermEmployees,
            string storenumber, int auditID, CommonDatabaseContext dataContext)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            string lastInvAudit = string.Empty;
            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_storenumber", storenumber));
            oParams.Add(new OracleProcParam("p_AuditID", auditID));
            oParams.Add(new OracleProcParam("o_last_inv_audit", OracleDbType.Varchar2, lastInvAudit, ParameterDirection.Output, 100));
            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            //refCursors.Add(new PairType<string, string>("o_last_inv_audit", "o_last_inv_audit"));
            refCursors.Add(new PairType<string, string>("o_psnl", "o_psnl"));
            refCursors.Add(new PairType<string, string>("o_term", "o_term"));
            refCursors.Add(new PairType<string, string>("o_chgOff", "o_chgOff"));
            refCursors.Add(new PairType<string, string>("o_Hist", "o_Hist"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "AUDIT_REPORTS", "SummaryReport", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("GetInventorySummaryReportFields Failed", oEx);
                dataContext.ErrorCode = "GetInventorySummaryReportFields";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("GetInventorySummaryReportFields Failed: return value is false", new ApplicationException());
                dataContext.ErrorCode = dA.ErrorCode + " --- GetInventorySummaryReportFields";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null)
            {
                return false;
            }


            if (outputDataSet.Tables.Count > 0)
            {
                try
                {
                    if (outputDataSet.Tables[TBL_INVENTORYSUMMARY_CHARGEOFF] != null && outputDataSet.Tables[TBL_INVENTORYSUMMARY_CHARGEOFF].Rows.Count > 0)
                    {
                        foreach (DataRow dr in outputDataSet.Tables[TBL_INVENTORYSUMMARY_CHARGEOFF].Rows)
                        {
                            AuditReportsObject.InventorySummaryChargeOffsField field = new AuditReportsObject.InventorySummaryChargeOffsField();
                            field.Category = Utilities.GetIntegerValue(dr["CAT_CODE"], 0);
                            field.CategoryDescription = Utilities.GetStringValue(dr["CAT_DESC"], string.Empty);
                            field.TotalItems = Utilities.GetIntegerValue(dr["QTY"], 0);
                            field.TotalAmount = Utilities.GetDecimalValue(dr["COST"], 0.00M);
                            field.TotalItemsNF = Utilities.GetIntegerValue(dr["NF_CNT"], 0);
                            field.TotalAmountNF = Utilities.GetDecimalValue(dr["NF_AMT"], 0.00M);
                            field.PercentItemsNF = (field.TotalItemsNF > 0 && field.TotalItems > 0) ? Math.Round((decimal)field.TotalItemsNF / (decimal)field.TotalItems * 100, 2) : 0.00M;
                            field.PercentNFAmount = (field.TotalAmountNF > 0.00m & field.TotalAmount > 0.00m) ? Math.Round(field.TotalAmountNF / field.TotalAmount * 100, 2) : 0.00M;
                            listInvChargeoffields.Add(field);
                        }
                    }
                    if (outputDataSet.Tables[TBL_INVENTORYSUMMARY_HIST] != null && outputDataSet.Tables[TBL_INVENTORYSUMMARY_HIST].Rows.Count > 0)
                    {
                        foreach (DataRow dr in outputDataSet.Tables[TBL_INVENTORYSUMMARY_HIST].Rows)
                        {
                            listInvHistoryfields.Add(new AuditReportsObject.InventorySummaryHistoryField()
                            {
                                InventoryDate = Utilities.GetDateTimeValue(dr["DATE_INITIATED"], DateTime.MinValue),
                                Manager = Utilities.GetStringValue(dr["STORE_MANAGER"], string.Empty),
                                InvType = Utilities.GetStringValue(dr["AUDIT_SCOPE"], string.Empty),
                                InvSubType = Utilities.GetStringValue(dr["AUDIT_REASON"], string.Empty),
                                OverShort = Utilities.GetDecimalValue(dr["OVER_UNDER"], 0.00M),
                                Adjustment = Utilities.GetDecimalValue(dr["ADJ"], 0.00M),
                                NetOverShort = Utilities.GetDecimalValue(dr["NET"], 0.00M),
                                PAScore = Utilities.GetIntegerValue(dr["AUDIT_SCORE"], 0),
                            });
                        }
                    }

                    if (outputDataSet.Tables[TBL_INVENTORYSUMMARY_PSNL] != null && outputDataSet.Tables[TBL_INVENTORYSUMMARY_PSNL].Rows.Count > 0)
                    {
                        foreach (DataRow dr in outputDataSet.Tables[TBL_INVENTORYSUMMARY_PSNL].Rows)
                        {
                            string name = Utilities.GetStringValue(dr["NAME"], string.Empty);
                            if(!string.IsNullOrEmpty(name))
                            {
                                if (string.IsNullOrEmpty(stringbuilderEmployeesPresent.ToString()))
                                    stringbuilderEmployeesPresent.Append(name);
                                else
                                    stringbuilderEmployeesPresent.Append(", " + name);
                            }
                        }
                        
                    }
                    if (outputDataSet.Tables[TBL_INVENTORYSUMMARY_TERM] != null && outputDataSet.Tables[TBL_INVENTORYSUMMARY_TERM].Rows.Count > 0)
                    {
                        foreach (DataRow dr in outputDataSet.Tables[TBL_INVENTORYSUMMARY_TERM].Rows)
                        {
                            string name = Utilities.GetStringValue(dr["NAME"], string.Empty);
                            if(!string.IsNullOrEmpty(name) || !name.Equals(" "))
                            {
                                if(string.IsNullOrEmpty(stringbuilderTermEmployees.ToString()))
                                    stringbuilderTermEmployees.Append(name);
                                else
                                    stringbuilderTermEmployees.Append(", " + name);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    return (false);
                }
            }
            dataContext.ErrorCode = "0";
            dataContext.ErrorText = "Success";
           
            return true;
        }

        public static bool GetPostAuditReportFields(ref List<AuditReportsObject.PostAuditField> postAuditFields,
            ref List<AuditReportsObject.PostAuditInventoryTotalsField> postAuditInventoryTotalsFields,
            ref AuditReportsObject.PostAuditTempICNReconciliationField postAuditAdjustmentsFields, 
            string storenumber, int auditID, CommonDatabaseContext dataContext)
        {
            //CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            if (postAuditFields == null || postAuditInventoryTotalsFields == null)
            {
                throw new ArgumentNullException("PostAuditReportFields");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_storenumber", storenumber));
            oParams.Add(new OracleProcParam("p_AuditID", auditID));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_stat_ttl", "o_stat_ttl"));
            refCursors.Add(new PairType<string, string>("o_chgOff", "o_chgOff"));
            refCursors.Add(new PairType<string, string>("o_reactvt", "o_reactvt"));
            refCursors.Add(new PairType<string, string>("o_chg_on", "o_chg_on"));
            refCursors.Add(new PairType<string, string>("o_adjust", "o_adjust"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "AUDIT_REPORTS", "postAuditReport", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("GetPostAuditReportFields Failed", oEx);
                dataContext.ErrorCode = "GetPostAuditReportFieldsFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("GetPostAuditReportFields Failed: return value is false", new ApplicationException());
                dataContext.ErrorCode = dA.ErrorCode + " --- GetPostAuditReportFields";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null)
            {
                return false;
            }

            if (outputDataSet.Tables.Count > 0)
            {

                if (outputDataSet.Tables[TBL_POSTAUDIT_INVTOTALS] != null && outputDataSet.Tables[TBL_POSTAUDIT_INVTOTALS].Rows.Count > 0)
                {
                    foreach (DataRow dr in outputDataSet.Tables[TBL_POSTAUDIT_INVTOTALS].Rows)
                    {
                        postAuditInventoryTotalsFields.Add(new AuditReportsObject.PostAuditInventoryTotalsField()
                        {
                            InventoryType = Utilities.GetStringValue(dr["STATUS_CD"], string.Empty),
                            Qty = Utilities.GetIntegerValue(dr["QUANTITY"], 0),
                            Cost = Utilities.GetDecimalValue(dr["PFI_AMOUNT"], 0.00M),
                            Category = (int)EnumPostAuditReportCategories.InventoryTotalsCountedByStatus
                        });
                    }
                }

                if (outputDataSet.Tables[TBL_POSTAUDIT_CHARGEOFF] != null && outputDataSet.Tables[TBL_POSTAUDIT_CHARGEOFF].Rows.Count > 0)
                {
                     int counter = 1;
                    foreach (DataRow dr in outputDataSet.Tables[TBL_POSTAUDIT_CHARGEOFF].Rows)
                    {
                        //if (counter <= 5)
                        //{

                        //this needs to be the only section within this if statement
                        postAuditFields.Add(new AuditReportsObject.PostAuditField()
                        {
                            ICN = Utilities.GetStringValue(dr["ICN"], string.Empty),
                            MerchandiseDescription = Utilities.GetStringValue(dr["MD_DESC"], string.Empty),
                            Qty = Utilities.GetIntegerValue(dr["QUANTITY"], 0),
                            Cost = Utilities.GetDecimalValue(dr["PFI_AMOUNT"], 0.00M),
                            Reason = Utilities.GetStringValue(dr["STATUS_REASON"], string.Empty),
                            Retail = Utilities.GetDecimalValue(dr["RETAIL_PRICE"], 0.00M),
                            Category = (int)EnumPostAuditReportCategories.ChargeOff
                        });
                    }
                }

                if (outputDataSet.Tables[TBL_POSTAUDIT_REACTIVATION] != null && outputDataSet.Tables[TBL_POSTAUDIT_REACTIVATION].Rows.Count > 0)
                {
                    foreach (DataRow dr in outputDataSet.Tables[TBL_POSTAUDIT_REACTIVATION].Rows)
                    {
                        postAuditFields.Add(new AuditReportsObject.PostAuditField()
                        {
                            ICN = Utilities.GetStringValue(dr["ICN"], string.Empty),
                            MerchandiseDescription = Utilities.GetStringValue(dr["MD_DESC"], string.Empty),
                            Qty = Utilities.GetIntegerValue(dr["QUANTITY"], 0),
                            Cost = Utilities.GetDecimalValue(dr["PFI_AMOUNT"], 0.00M),
                            Retail = Utilities.GetDecimalValue(dr["RETAIL_PRICE"], 0.00M),
                            Category = (int)EnumPostAuditReportCategories.Reactivation
                        });
                    }
                }

                if (outputDataSet.Tables[TBL_POSTAUDIT_CHARGEON] != null && outputDataSet.Tables[TBL_POSTAUDIT_CHARGEON].Rows.Count > 0)
                {
                    foreach (DataRow dr in outputDataSet.Tables[TBL_POSTAUDIT_CHARGEON].Rows)
                    {
                        postAuditFields.Add(new AuditReportsObject.PostAuditField()
                        {
                            ICN = Utilities.GetStringValue(dr["ICN"], string.Empty),
                            MerchandiseDescription = Utilities.GetStringValue(dr["MD_DESC"], string.Empty),
                            Qty = Utilities.GetIntegerValue(dr["QUANTITY"], 0),
                            Cost = Utilities.GetDecimalValue(dr["PFI_AMOUNT"], 0.00M),
                            Retail = Utilities.GetDecimalValue(dr["RETAIL_PRICE"], 0.00M),
                            Category = (int)EnumPostAuditReportCategories.ChargeOn
                        });                     
                    }
                }

                if (outputDataSet.Tables[TBL_POSTAUDIT_TEMPICNRECONCILLIATION] != null && outputDataSet.Tables[TBL_POSTAUDIT_TEMPICNRECONCILLIATION].Rows.Count > 0)
                {
                    foreach (DataRow dr in outputDataSet.Tables[TBL_POSTAUDIT_TEMPICNRECONCILLIATION].Rows)
                    {
                        postAuditAdjustmentsFields.TotalNewICNsSoldNotReconciledQty = Utilities.GetIntegerValue(dr["TMP_ICN_NOT_REC"], 0);
                        postAuditAdjustmentsFields.TotalNewICNsSoldNotReconciledCost = Utilities.GetDecimalValue(dr["TMP_ICN_NOT_REC_AMT"], 0.00M);
                        postAuditAdjustmentsFields.TotalOldICNsSoldNotReconciledQty = Utilities.GetIntegerValue(dr["TMP_ICN_NOT_REC"], 0);
                        postAuditAdjustmentsFields.TotalOldICNsSoldNotReconciledCost = Utilities.GetDecimalValue(dr["TMP_ICN_NOT_REC_AMT"], 0.00M);
                        postAuditAdjustmentsFields.ChargeOffQty = Utilities.GetIntegerValue(dr["CHARGE_OFF"], 0);
                        postAuditAdjustmentsFields.ChargeOffCost = Utilities.GetDecimalValue(dr["CHARGE_OFF_AMT"], 0.00M);
                        postAuditAdjustmentsFields.ChargeOnQty = Utilities.GetIntegerValue(dr["CHARGE_ON"], 0);
                        postAuditAdjustmentsFields.ChargeOnCost = Utilities.GetDecimalValue(dr["CHARGE_ON_AMT"], 0.00M);
                        postAuditAdjustmentsFields.TotalNewICNsSoldReconciledQty = Utilities.GetIntegerValue(dr["TMP_ICN_REC"], 0);
                        postAuditAdjustmentsFields.TotalNewICNsSoldReconciledCost = Utilities.GetDecimalValue(dr["TMP_ICN_REC_AMT"], 0.00M);
                        postAuditAdjustmentsFields.TotalActualICNsReconciledQty = Utilities.GetIntegerValue(dr["ICNS_REC"], 0);
                        postAuditAdjustmentsFields.TotalActualICNsReconciledCost = Utilities.GetDecimalValue(dr["ICNS_REC_AMT"], 0.00M);
                    }
                }

              
                dataContext.ErrorCode = "0";
                dataContext.ErrorText = "Success";
            }
                
            return true;
        }

        public static bool GetTrakkerUploadReportFields(ref List<AuditReportsObject.TrakkerUploadReportSinceLastInventory> uploadFields, string storenumber, int auditID, CommonDatabaseContext dataContext)
        {
            //CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            if (uploadFields == null)
            {
                throw new ArgumentNullException("TrakkerUploadReportFields");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_storenumber", storenumber));
            oParams.Add(new OracleProcParam("p_AuditID", auditID));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_tmp_icn", "o_tmp_icn"));
            refCursors.Add(new PairType<string, string>("o_msg_items", "o_msg_items"));
            refCursors.Add(new PairType<string, string>("o_nxt_items", "o_nxt_items"));
            refCursors.Add(new PairType<string, string>("o_unexp_items", "o_unexp_items"));
            refCursors.Add(new PairType<string, string>("o_dup_items", "o_dup_items"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "AUDIT_REPORTS", "TrakkerUploadReport", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("GetTrakkerUploadReportFields Failed", oEx);
                dataContext.ErrorCode = "GetTrakkerUploadReportFieldsFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("GetTrakkerUploadReportFields Failed: return value is false", new ApplicationException());
                dataContext.ErrorCode = dA.ErrorCode + " --- GetTrakkerUploadReportFields";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null)
            {
                return false;
            }

            if (outputDataSet.Tables.Count > 0)
            {
                if (outputDataSet.Tables[TBL_TRAKKER_TMP_ICN] != null && outputDataSet.Tables[TBL_TRAKKER_TMP_ICN].Rows.Count > 0)
                {
                    foreach (DataRow dr in outputDataSet.Tables[TBL_TRAKKER_TMP_ICN].Rows)
                    {
                        uploadFields.Add(new AuditReportsObject.TrakkerUploadReportSinceLastInventory()
                        {
                            ICN = Utilities.GetStringValue(dr["ICN"], string.Empty),
                            MerchandiseDescription = Utilities.GetStringValue(dr["MD_DESC"], string.Empty),
                            Cost = Utilities.GetDecimalValue(dr["PFI_AMOUNT"], 0.00M),
                            Retail = Utilities.GetDecimalValue(dr["RETAIL_PRICE"], 0.00M),
                            Status = Utilities.GetStringValue(dr["STATUS_CD"], string.Empty),
                            Category = (int)EnumTrakkerUploadReportCategories.TempICNsSinceLastInventory
                        });
                    }
                }

                if (outputDataSet.Tables[TBL_TRAKKER_MISSING_ITEMS] != null && outputDataSet.Tables[TBL_TRAKKER_MISSING_ITEMS].Rows.Count > 0)
                {
                    //int rowcount = 1;
                    foreach (DataRow dr in outputDataSet.Tables[TBL_TRAKKER_MISSING_ITEMS].Rows)
                    {
                        uploadFields.Add(new AuditReportsObject.TrakkerUploadReportMissingItems()
                        {
                            ICN = Utilities.GetStringValue(dr["ICN"], string.Empty),
                            MerchandiseDescription = Utilities.GetStringValue(dr["MD_DESC"], string.Empty),
                            Cost = Utilities.GetDecimalValue(dr["PFI_AMOUNT"], 0.00M),
                            Retail = Utilities.GetDecimalValue(dr["RETAIL_PRICE"], 0.00M),
                            Status = Utilities.GetStringValue(dr["STATUS_CD"], string.Empty),
                            Category = (int)EnumTrakkerUploadReportCategories.MissingItems
                        });
                    }
                }

                if (outputDataSet.Tables[TBL_TRAKKER_NXT_ITEMS] != null && outputDataSet.Tables[TBL_TRAKKER_NXT_ITEMS].Rows.Count > 0)
                {
                    foreach (DataRow dr in outputDataSet.Tables[TBL_TRAKKER_NXT_ITEMS].Rows)
                    {
                        uploadFields.Add(new AuditReportsObject.TrakkerUploadReportNXTsSinceLastInventory()
                        {
                            ICN = Utilities.GetStringValue(dr["ICN"], string.Empty),
                            MerchandiseDescription = Utilities.GetStringValue(dr["MD_DESC"], string.Empty),
                            Retail = Utilities.GetDecimalValue(dr["RETAIL_PRICE"], 0.00M),
                            Status = Utilities.GetStringValue(dr["STATUS_CD"], string.Empty),
                            TransactionNumber = Utilities.GetStringValue(dr["MSR"], string.Empty),
                            Category = (int)EnumTrakkerUploadReportCategories.NXTICNsSinceLastInventory
                        });
                    }
                }

                if (outputDataSet.Tables[TBL_TRAKKER_UNEXP_ITEMS] != null && outputDataSet.Tables[TBL_TRAKKER_UNEXP_ITEMS].Rows.Count > 0)
                {
                    foreach (DataRow dr in outputDataSet.Tables[TBL_TRAKKER_UNEXP_ITEMS].Rows)
                    {
                        uploadFields.Add(new AuditReportsObject.TrakkerUploadReportUnexpectedItems()
                        {
                            ICN = Utilities.GetStringValue(dr["ICN"], string.Empty),
                            MerchandiseDescription = Utilities.GetStringValue(dr["MD_DESC"], string.Empty),
                            Cost = Utilities.GetDecimalValue(dr["PFI_AMOUNT"], 0.00M),
                            TrakID = Utilities.GetIntegerValue(dr["TRAK_ID"], 0),
                            ScanSequence = Utilities.GetIntegerValue(dr["TRAK_SEQ"], 0),
                            Status = Utilities.GetStringValue(dr["STATUS_CD"], string.Empty),
                            TrakFlag = Utilities.GetStringValue(dr["TRAK_NEW_FLAG"], string.Empty),
                            Category = (int)EnumTrakkerUploadReportCategories.UnexpectedItems
                        });
                    }
                }

                if (outputDataSet.Tables[TBL_TRAKKER_DUP_ITEMS] != null && outputDataSet.Tables[TBL_TRAKKER_DUP_ITEMS].Rows.Count > 0)
                {
                    foreach (DataRow dr in outputDataSet.Tables[TBL_TRAKKER_DUP_ITEMS].Rows)
                    {
                        uploadFields.Add(new AuditReportsObject.TrakkerUploadReportDuplicateScans()
                        {
                            ICN = Utilities.GetStringValue(dr["ICN"], string.Empty),
                            MerchandiseDescription = Utilities.GetStringValue(dr["MD_DESC"], string.Empty),
                            Cost = Utilities.GetDecimalValue(dr["PFI_AMOUNT"], 0.00M),
                            TrakID = Utilities.GetIntegerValue(dr["TRAK_ID"], 0),
                            AuditLocation = Utilities.GetStringValue(dr["TRAK_LOC"], string.Empty),
                            ScanSequence = Utilities.GetIntegerValue(dr["TRAK_SEQ"], 0),
                            Status = Utilities.GetStringValue(dr["STATUS_CD"], string.Empty),
                            TrakFlag = Utilities.GetStringValue(dr["TRAK_NEW_FLAG"], string.Empty),
                            Category = (int)EnumTrakkerUploadReportCategories.DuplicateScans
                        });
                    }
                }
                dataContext.ErrorCode = "0";
                dataContext.ErrorText = "Success";
            }

            return true;
        }

        public static bool GetMissingItemsData(string storenumber, int auditID, out DataSet outputDataSet)
        {
            outputDataSet = new DataSet("MISSING_ITEMS");

            string package = string.Empty;
            string procedure = string.Empty;

            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            outputDataSet = new DataSet("PREAUDIT");

            //---- Get Data
            package = "AUDIT_REPORTS";
            procedure = "missingItemsReport";


            inParams.Add(new OracleProcParam("p_storenumber", storenumber));
            inParams.Add(new OracleProcParam("p_audit_id", auditID));
            inParams.Add(new OracleProcParam("p_incld_all", "N"));
            refCursors = new List<PairType<string, string>>
                    {
                        new PairType<string, string>("o_msg_items", MISSING_ITEMS)
                    };


            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            bool retval = false;

            try
            {

                retval = dA.issueSqlStoredProcCommand(
                    "ccsowner", package, procedure,
                    inParams, refCursors, "o_error_code", "o_error_desc",
                    out outputDataSet);

                if (!retval) // try again, just in case it was caused by the database package being out of synch
                {
                    retval = dA.issueSqlStoredProcCommand(
                        "ccsowner", package, procedure,
                        inParams, refCursors, "o_error_code", "o_error_desc",
                        out outputDataSet);
                }


            }

            catch (System.Exception)
            {
                MessageBox.Show("There was an error processing your request. Please contact your administrator.");
                //report.ReportError = "There was an error processing your request. Please contact your administrator.";
                // report.ReportErrorLevel = (int)LogLevel.ERROR;
                return false;
            }




            if (!retval)
            {
                //report.ReportErrorLevel = (int)LogLevel.INFO;
                //report.ReportError = "There was an error retrieving data from the database, please try again.  If this error persists, please contact customer support.";
                MessageBox.Show("There was an error retrieving data from the database, please try again.");

            }

            else if (outputDataSet == null || !outputDataSet.IsInitialized || outputDataSet.Tables.Count == 0)  // 1 as there is output parameters
            {
                //report.ReportErrorLevel = (int)LogLevel.INFO;
                //report.ReportError = ReportConstants.NODATA;
                MessageBox.Show("No Data");
                retval = false;
            }


            return retval;

        }

        public static bool GetPreAuditData(string storenumber, int auditID, out DataSet outputDataSet, ref string storeName)
        {
            string package = string.Empty;
            string procedure = string.Empty;

            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            outputDataSet = new DataSet("PREAUDIT");

            //---- Get Data
            package = "AUDIT_REPORTS";
            procedure = "preAuditReport";


            inParams.Add(new OracleProcParam("p_storenumber", storenumber));
            inParams.Add(new OracleProcParam("p_audit_id", auditID));
            inParams.Add(new OracleProcParam("o_store_name", OracleDbType.Varchar2, storeName, ParameterDirection.Output, 100));
            refCursors = new List<PairType<string, string>>
                    {
                        new PairType<string, string>("o_ctg_info", PREAUDIT_CAT_SMRY),
                        new PairType<string, string>("o_cacc_info", PREAUDIT_CACC_SMRY)
                    };


            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            bool retval = false;

            try
            {

                retval = dA.issueSqlStoredProcCommand(
                    "ccsowner", package, procedure,
                    inParams, refCursors, "o_error_code", "o_error_desc",
                    out outputDataSet);

                if (!retval) // try again, just in case it was caused by the database package being out of synch
                {
                    retval = dA.issueSqlStoredProcCommand(
                        "ccsowner", package, procedure,
                        inParams, refCursors, "o_error_code", "o_error_desc",
                        out outputDataSet);
                }


            }

            catch (System.Exception)
            {
                MessageBox.Show("There was an error processing your request. Please contact your administrator.");
                //report.ReportError = "There was an error processing your request. Please contact your administrator.";
                // report.ReportErrorLevel = (int)LogLevel.ERROR;
                return false;
            }

            if (!retval)
            {
                //report.ReportErrorLevel = (int)LogLevel.INFO;
                //report.ReportError = "There was an error retrieving data from the database, please try again.  If this error persists, please contact customer support.";
                MessageBox.Show("There was an error retrieving data from the database, please try again.");

            }

            else if (outputDataSet == null || !outputDataSet.IsInitialized || outputDataSet.Tables.Count == 1)  // 1 as there is output parameters
            {
                //report.ReportErrorLevel = (int)LogLevel.INFO;
                //report.ReportError = ReportConstants.NODATA;
                MessageBox.Show("No Data");
                retval = false;
            }
            else if (outputDataSet.Tables.Contains("OUTPUT") && storeName.Length == 0)
                storeName = (string)outputDataSet.Tables["OUTPUT"].Rows[0][1];


            return retval;
        }

        public static bool getAuditStatusSummary(string storenumber, int audit_id, bool isPreAudit, out DataSet outputDataSet)
        {
            bool retval = false;
            outputDataSet = new DataSet();

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_storenumber", storenumber));
            oParams.Add(new OracleProcParam("p_AuditID", audit_id));
            oParams.Add(new OracleProcParam("p_isPreAudit", (isPreAudit ? "Y" : "N")));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_summary", "Stats"));
            refCursors.Add(new PairType<string, string>("o_cacc_info", PREAUDIT_CACC_SMRY));
            decimal qty = 0, nr_unexp = 0, nr_cacc = 0;

            oParams.Add(new OracleProcParam("o_qty_missing", OracleDbType.Decimal, qty, ParameterDirection.Output, 100));
            oParams.Add(new OracleProcParam("o_nr_unexp", OracleDbType.Decimal, nr_unexp, ParameterDirection.Output, 100));
            oParams.Add(new OracleProcParam("o_nr_cacc_missing", OracleDbType.Decimal, nr_cacc, ParameterDirection.Output, 100));

            bool retVal;

            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "AUDIT_REPORTS", "getAuditStatusSummary", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                //dataContext.ErrorCode = dA.ErrorCode;
                // dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("GetStats Failed", oEx);
                // dataContext.ErrorCode = "GetTrakkerUploadReportFieldsFailed";
                // dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("GetStats Failed: return value is false", new ApplicationException());
                //dataContext.ErrorCode = dA.ErrorCode + " --- GetTrakkerUploadReportFields";
                //dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null)
            {
                return false;
            }

            if (outputDataSet.Tables.Count > 0)
            {
            }

            return true;
        }
    }
}
