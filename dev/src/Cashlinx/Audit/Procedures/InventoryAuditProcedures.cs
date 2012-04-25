using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database
{
    public class InventoryAuditProcedures
    {
        private static readonly string TBL_AUDITINFO = "o_audit_info";

        public static bool GetAdditionalAuditInfo(InventoryAuditVO audit, CommonDatabaseContext dataContext)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_storenumber", audit.StoreNumber));
            oParams.Add(new OracleProcParam("p_AuditID", audit.AuditId));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_audit_info", "o_audit_info"));
            refCursors.Add(new PairType<string, string>("o_chg_off_info", "o_chg_off_info"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "AUDIT_REPORTS", "getAuditInfo", oParams, refCursors, "o_return_code",
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
                try
                {
                    if (outputDataSet.Tables[TBL_AUDITINFO] != null && outputDataSet.Tables[TBL_AUDITINFO].Rows.Count > 0)
                    {
                        audit.Region = Utilities.GetStringValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["REGION"], string.Empty);
                        audit.Division = Utilities.GetStringValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["DIVISION"], string.Empty);
                        audit.ActiveShopManager = Utilities.GetStringValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["STOREMANAGER"], string.Empty);
                        //audit.RVP = Utilities.GetStringValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["STOREMANAGER"], string.Empty); //need to verify
                        audit.Auditor = Utilities.GetStringValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["AUDITOR"], string.Empty);
                        audit.MarketManager = Utilities.GetStringValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["MKTMANAGER"], string.Empty);
                        audit.AuditStartDate = Utilities.GetDateTimeValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["DATE_INITIATED"], DateTime.MinValue);
                        audit.InventoryType = Utilities.GetStringValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["AUDIT_SCOPE"], string.Empty); 
                        audit.MarketManagerPresent = Utilities.GetStringValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["MARKET_MANAGER_PRESENT"], string.Empty).Equals("Y");
                        audit.LastAuditDate = Utilities.GetDateTimeValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["LAST_INV_AUDIT"], DateTime.MinValue);
                        audit.SubType = Utilities.GetStringValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["AUDIT_REASON"], string.Empty);
                        audit.LayAuditDate = Utilities.GetDateTimeValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["LAST_LAY_AUDIT"], DateTime.MinValue);
                        audit.AuditScore = Utilities.GetIntegerValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["AUDIT_SCORE"], 0);
                        audit.CurrentLoanBalance =Utilities.GetDecimalValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["CUR_LOAN_BAL"], 0.00m);
                        audit.PreviousLoanBalance =Utilities.GetDecimalValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["PREV_LOAN_BAL"], 0.00m);
                        
                        audit.CurrentInventoryBalance =Utilities.GetDecimalValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["CUR_INV_BAL"], 0.00m);
                        audit.PreviousInventoryBalance =Utilities.GetDecimalValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["PREV_INV_BAL"], 0.00m);
                        audit.PreviousYearCoff =Utilities.GetDecimalValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["PREV_YR_CHARGEOFF"], 0.00m);
                        audit.CashInStore =Utilities.GetDecimalValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["CASH"], 0.00m);
                        audit.DateCompleted = Utilities.GetDateTimeValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["DATE_COMPLETED"], DateTime.MinValue);
                        audit.TotalChargeOn =Utilities.GetDecimalValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["TTL_CHGON"], 0.00m);
                        audit.TotalChargeOff = Utilities.GetDecimalValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["TTL_CHGOFF"], 0.00m);
                        audit.TempICNAdjustment = Utilities.GetDecimalValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["TTL_TMPICN"], 0.00m);
                        audit.Tolerance = Utilities.GetDecimalValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["TOLERANCE"], 0.00m);
                        audit.YTDShortage = Utilities.GetDecimalValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["YTD_SHORTAGE"], 0.00m);
                        audit.YTDAdjustments = Utilities.GetDecimalValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["YTD_ADJ"], 0.00m);
                        audit.NetYTDShortage = audit.YTDShortage - audit.YTDAdjustments;

                        audit.OverShort = Utilities.GetDecimalValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["OVER_SHORT"], 0.00m);
                        audit.Adjustment = Utilities.GetDecimalValue(outputDataSet.Tables[TBL_AUDITINFO].Rows[0]["ADJUSTMENT"], 0.00m);
                        audit.NetOverShort = audit.OverShort - audit.Adjustment;
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

        public static bool EditAudit(InventoryAuditVO audit, CommonDatabaseContext dataContext)
        {
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", audit.StoreNumber));
            oParams.Add(new OracleProcParam("p_trak_upload_date", GetDownloadUploadDateValue(audit.UploadDate)));
            oParams.Add(new OracleProcParam("p_trak_download_date", GetDownloadUploadDateValue(audit.DownloadDate)));
            oParams.Add(new OracleProcParam("p_audit_id", audit.AuditId));
            oParams.Add(new OracleProcParam("p_user_name", dataContext.FullUserName));
            oParams.Add(new OracleProcParam("p_status", audit.Status.ToString()));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner", "PAWN_AUDIT", "edit_auditor_record",
                                                                                        oParams, refCursors, "o_return_code",
                                                                                        "o_return_text", out outputDataSet);
                dataContext.Result = retVal;
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("EditAudit Failed", oEx);
                dataContext.Result = false;
                dataContext.ErrorCode = "EditAuditFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("EditAudit Failed: return value is false", new ApplicationException());
                dataContext.Result = false;
                dataContext.ErrorCode = dA.ErrorCode + " --- EditAudit";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            dataContext.ErrorCode = "0";
            dataContext.ErrorText = "Success";

            dataContext.Result = true;
            return true;
        }

        public static bool ChargeOff(ChargeOffDatabaseContext chargeOffDatabaseContext, CommonDatabaseContext dataContext)
        {
            if (chargeOffDatabaseContext == null)
            {
                throw new ArgumentNullException("chargeOffDatabaseContext");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //List<string> icns = (from i in items
            //                     select i.Icn.ToString()).ToList();

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", chargeOffDatabaseContext.StoreNumber));
            oParams.Add(new OracleProcParam("p_status_date", ShopDateTime.Instance.FullShopDateTime.ToString("MM/dd/yyyy")));
            oParams.Add(new OracleProcParam("p_status_time", GetDownloadUploadDateValue(ShopDateTime.Instance.FullShopDateTime)));
            oParams.Add(new OracleProcParam("p_user_id", dataContext.FullUserName));
            oParams.Add(new OracleProcParam("p_icn", true, chargeOffDatabaseContext.Icns));
            oParams.Add(new OracleProcParam("p_status_reason", chargeOffDatabaseContext.ItemReason.ToString()));
            oParams.Add(new OracleProcParam("p_tran_type", chargeOffDatabaseContext.TranType));
            oParams.Add(new OracleProcParam("p_authorized_by", chargeOffDatabaseContext.AuthorizedBy));
            oParams.Add(new OracleProcParam("p_atf_incident_no", chargeOffDatabaseContext.AtfIncidentId));
            oParams.Add(new OracleProcParam("p_police_case_no", chargeOffDatabaseContext.PoliceCaseNumber));
            oParams.Add(new OracleProcParam("p_charitable_organization", chargeOffDatabaseContext.CharitableOrganization));
            oParams.Add(new OracleProcParam("p_charitable_organization_add", chargeOffDatabaseContext.CharitableAddress));
            oParams.Add(new OracleProcParam("p_charitable_organization_city", chargeOffDatabaseContext.CharitableCity));
            oParams.Add(new OracleProcParam("p_charitable_organization_st", chargeOffDatabaseContext.CharitableState));
            oParams.Add(new OracleProcParam("p_charitable_organization_zip", chargeOffDatabaseContext.CharitablePostalCode));
            oParams.Add(new OracleProcParam("p_replaced_icn", chargeOffDatabaseContext.ReplacedIcn));
            oParams.Add(new OracleProcParam("p_comments", chargeOffDatabaseContext.Comments));
            oParams.Add(new OracleProcParam("p_audit_id", chargeOffDatabaseContext.AuditId));
            oParams.Add(new OracleProcParam("p_mdse_destroyed", chargeOffDatabaseContext.Destroyed ? "Y" : "N"));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "PAWN_AUDIT", "audit_charge_off", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                dataContext.Result = retVal;
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ChargeOff Failed", oEx);
                dataContext.Result = false;
                dataContext.ErrorCode = "ChargeOffFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ChargeOff Failed: return value is false", new ApplicationException());
                dataContext.Result = false;
                dataContext.ErrorCode = dA.ErrorCode + " --- ChargeOff";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            dataContext.ErrorCode = "0";
            dataContext.ErrorText = "Success";

            dataContext.Result = true;
            return true;
        }

        public static bool CreateAudit(InventoryAuditVO audit, CommonDatabaseContext dataContext)
        {
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", audit.StoreNumber));
            oParams.Add(new OracleProcParam("p_inventory_type", audit.AuditType.ToString()));
            oParams.Add(new OracleProcParam("p_user_name", audit.InitiatedBy));
            oParams.Add(new OracleProcParam("p_status", audit.Status.ToString()));
            oParams.Add(new OracleProcParam("p_initiated_by", audit.InitiatedBy));
            oParams.Add(new OracleProcParam("p_trak_upload_date", null));
            oParams.Add(new OracleProcParam("p_trak_download_date", null));
            oParams.Add(new OracleProcParam("p_store_manager", audit.ActiveShopManager));
            oParams.Add(new OracleProcParam("p_audit_reason", audit.AuditReason.ToString()));
            oParams.Add(new OracleProcParam("p_audit_scope", audit.AuditScope.ToString()));
            oParams.Add(new OracleProcParam("p_audit_report_detail", audit.ReportDetail.ToString()));
            oParams.Add(new OracleProcParam("p_is_market_mgr_present", audit.MarketManagerPresent ? "Y" : "N"));
            oParams.Add(new OracleProcParam("p_exiting_shop_manager", audit.ExitingShopManager));
            oParams.Add(new OracleProcParam("o_audit_id", OracleDbType.Decimal, DBNull.Value, ParameterDirection.Output, 1));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner", "PAWN_AUDIT", "insert_auditor_record",
                                                                                        oParams, refCursors, "o_return_code",
                                                                                        "o_return_text", out outputDataSet);
                dataContext.Result = retVal;
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("CreateAudit Failed", oEx);
                dataContext.Result = false;
                dataContext.ErrorCode = "CreateAuditFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("CreateAudit Failed: return value is false", new ApplicationException());
                dataContext.Result = false;
                dataContext.ErrorCode = dA.ErrorCode + " --- CreateAudit";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null || outputDataSet.Tables.Count == 0 || outputDataSet.Tables[0].Rows.Count == 0)
            {
                BasicExceptionHandler.Instance.AddException("CreateAudit Failed: invalid DataSet", new ApplicationException());
                dataContext.Result = false;
                dataContext.ErrorCode = dA.ErrorCode + " --- CreateAudit";
                dataContext.ErrorText = dA.ErrorDescription + " -- Invalid DataSet";
                return false;
            }

            audit.AuditId = Utilities.GetIntegerValue(outputDataSet.Tables[0].Rows[0].ItemArray.GetValue(1));

            dataContext.ErrorCode = "0";
            dataContext.ErrorText = "Success";

            dataContext.Result = true;

            GetSummaryInfo(audit, dataContext);

            return dataContext.Result;
        }

        public static bool GetSummaryInfo(InventoryAuditVO audit, CommonDatabaseContext dataContext)
        {
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", audit.StoreNumber));
            oParams.Add(new OracleProcParam("p_AuditID", audit.AuditId));
            oParams.Add(new OracleProcParam("o_store_name", OracleDbType.Varchar2, audit.StoreName, ParameterDirection.Output, 100));
            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_ctg_info", AuditReportsProcedures.PREAUDIT_CAT_SMRY));
            refCursors.Add(new PairType<string, string>("o_cacc_info", AuditReportsProcedures.PREAUDIT_CACC_SMRY));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner", // getCACCInfo
                                                                                        "AUDIT_REPORTS", "preAuditReport", oParams,
                                                                                        refCursors, "o_return_code", "o_return_text",
                                                                                        out outputDataSet);
                dataContext.Result = retVal;
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("GetSummaryInfo Failed", oEx);
                dataContext.Result = false;
                dataContext.ErrorCode = "GetCaccInfoFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("GetSummaryInfo Failed: return value is false", new ApplicationException());
                dataContext.Result = false;
                dataContext.ErrorCode = dA.ErrorCode + " --- GetSummaryInfo";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null)
            {
                return false;
            }

            if (outputDataSet.Tables.Count == 3)
            {
                if (outputDataSet.Tables.Contains("OUTPUT"))
                    audit.StoreName = outputDataSet.Tables["OUTPUT"].Rows[0][1].ToString();

                if (outputDataSet.Tables.Contains(AuditReportsProcedures.PREAUDIT_CACC_SMRY))
                {
                    setCaccTotals(audit, outputDataSet.Tables[AuditReportsProcedures.PREAUDIT_CACC_SMRY].Rows, true);
                }


                if (outputDataSet.Tables.Contains(AuditReportsProcedures.PREAUDIT_CAT_SMRY))
                {

                    foreach (DataRow r in outputDataSet.Tables[AuditReportsProcedures.PREAUDIT_CAT_SMRY].Rows)
                    {
                        if (r.Field<Int32>("CAT_CODE") != 1000 ||
                            (r.Field<Int32>("CAT_CODE") == 1000 && r.Field<String>("ISJREFURB") != "Y"))
                        {
                            audit.ExpectedItems.Quantity += Decimal.ToInt32(r.Field<decimal>("QTY"));
                            audit.ExpectedItems.Cost += r.Field<decimal>("COST");
                        }
                        else
                        {
                            // Do not include Jewlery Refurbs
                        }

                        if (r.Field<Int32>("CAT_CODE") == 1000 && r.Field<String>("ISJREFURB") == "Y")
                        {
                            // Do not include Jewlery Refurbs
                        }
                        else if (r.Field<Int32>("CAT_CODE") == 1000)
                        {
                            audit.ExptectedJewlery.Quantity += Decimal.ToInt32(r.Field<decimal>("QTY"));
                            audit.ExptectedJewlery.Cost += r.Field<decimal>("COST");
                        }
                        else
                        {
                            audit.ExptedtedGeneral.Quantity += Decimal.ToInt32(r.Field<decimal>("QTY"));
                            audit.ExptedtedGeneral.Cost += r.Field<decimal>("COST");
                        }
                    }


                }

                dataContext.ErrorCode = "0";
                dataContext.ErrorText = "Success";
            }

            audit.PreAuditData = outputDataSet;

            dataContext.Result = true;
            return true;
        }


        public static bool persistCACCCounts (CommonDatabaseContext dataContext, 
                string storenumber, int auditID, string nrCD, string nrVideo, string nrGames, string nrPremGame, string nrDVD)
        {
            bool retval = false;

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", storenumber));
            oParams.Add(new OracleProcParam("p_AuditID", auditID));

            oParams.Add(new OracleProcParam("p_cd_count", nrCD));
            oParams.Add(new OracleProcParam("p_video_count", nrVideo));
            oParams.Add(new OracleProcParam("p_std_game_count", nrGames));
            oParams.Add(new OracleProcParam("p_prm_game_count", nrPremGame));
            oParams.Add(new OracleProcParam("p_dvd_count", nrDVD));

            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            DataSet outputDataSet = new DataSet();
            try
            {
                retval = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                        "AUDIT_REPORTS", "setCACCInfo", oParams,
                                                                                        refCursors, "o_return_code", "o_return_text",
                                                                                        out outputDataSet);
                dataContext.Result = retval;
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("SetCACCInfo Failed", oEx);
                dataContext.Result = false;
                dataContext.ErrorCode = "SetCaccInfoFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            return false;
        }

        public static bool GetCACCInfo(InventoryAuditVO audit, CommonDatabaseContext dataContext, out DataSet outputDataSet)
        {
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", audit.StoreNumber));
            oParams.Add(new OracleProcParam("p_AuditID", audit.AuditId));
            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_cacc_info", AuditReportsProcedures.PREAUDIT_CACC_SMRY));

            //Make stored proc call
            bool retVal;
            //DataSet 
            outputDataSet = new DataSet();
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner", 
                                                                                        "AUDIT_REPORTS", "getCACCInfo", oParams,
                                                                                        refCursors, "o_return_code", "o_return_text",
                                                                                        out outputDataSet);
                dataContext.Result = retVal;
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("GetCACCInfo Failed", oEx);
                dataContext.Result = false;
                dataContext.ErrorCode = "GetCaccInfoFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("GetCACCInfo Failed: return value is false", new ApplicationException());
                dataContext.Result = false;
                dataContext.ErrorCode = dA.ErrorCode + " --- GetCACCInfo";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null)
            {
                return false;
            }



            dataContext.Result = true;
            return true;
        }


        public static bool setCaccTotals(InventoryAuditVO audit, DataRowCollection rows, bool preAudit)
        {
            bool retval = false;

            foreach (DataRow dr in rows)
            {
                int catCode = Utilities.GetIntegerValue(dr["cat_code"], 0);
                string description = Utilities.GetStringValue(dr["cat_desc"]);
                int? newQty = Utilities.GetNullableIntegerValue(dr["new_qty"], null);

                string qtyField = "qty";
                string costField = "cost";

                if (!retval && (preAudit || !newQty.HasValue))
                {
                    qtyField = "org_" + qtyField;
                    costField = "org_" + costField;
                }
                else
                {
                    retval = true;
                    qtyField = "new_" + qtyField;
                    costField = "new_" + costField;
                }

                int quantity = Utilities.GetIntegerValue(dr[qtyField], 0);
                decimal cost = Utilities.GetDecimalValue(dr[costField], 0);

                switch (catCode)
                {
                    case 3262:
                        audit.CompactDiscSummary.CatCode = catCode;
                        audit.CompactDiscSummary.Cost = cost;
                        audit.CompactDiscSummary.Description = description;
                        audit.CompactDiscSummary.Quantity = quantity;
                        break;
                    case 3350:
                        audit.VideoTapeSummary.CatCode = catCode;
                        audit.VideoTapeSummary.Cost = cost;
                        audit.VideoTapeSummary.Description = description;
                        audit.VideoTapeSummary.Quantity = quantity;
                        break;
                    case 3362:
                        audit.StandardVideoGameSummary.CatCode = catCode;
                        audit.StandardVideoGameSummary.Cost = cost;
                        audit.StandardVideoGameSummary.Description = description;
                        audit.StandardVideoGameSummary.Quantity = quantity;
                        break;
                    case 3363:
                        audit.PremiumVideoGameSummary.CatCode = catCode;
                        audit.PremiumVideoGameSummary.Cost = cost;
                        audit.PremiumVideoGameSummary.Description = description;
                        audit.PremiumVideoGameSummary.Quantity = quantity;
                        break;
                    case 3380:
                        audit.DvdDiscSummary.CatCode = catCode;
                        audit.DvdDiscSummary.Cost = cost;
                        audit.DvdDiscSummary.Description = description;
                        audit.DvdDiscSummary.Quantity = quantity;
                        break;
                }
            }

            return retval;
        }

        public static bool GetMarketManagerName(ref string marketManagerName, CommonDatabaseContext dataContext)
        {
            marketManagerName = string.Empty;
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_storenumber", dataContext.CurrentSiteId.StoreNumber));
            oParams.Add(new OracleProcParam("o_market_manager", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 510));
            oParams.Add(new OracleProcParam("o_market_descr", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 500));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            //refCursors.Add(new PairType<string, string>("o_loc", "o_loc"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "AUDIT_REPORTS", "getMarketManagerName", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
                dataContext.Result = retVal;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("GetMarketManagerName Failed", oEx);
                dataContext.ErrorCode = "GetMarketManagerNameFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                dataContext.Result = false;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("GetMarketManagerName Failed: return value is false", new ApplicationException());
                dataContext.ErrorCode = dA.ErrorCode + " --- GetMarketManagerName";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                dataContext.Result = false;
                return (false);
            }

            if (outputDataSet == null)
            {
                dataContext.Result = false;
                return false;
            }

            if (outputDataSet.Tables.Count > 0)
            {
                try
                {
                    if (outputDataSet.Tables["output"] != null && outputDataSet.Tables["output"].Rows.Count > 0)
                    {
                        marketManagerName = Utilities.GetStringValue(outputDataSet.Tables["output"].Rows[0][1], string.Empty);
                    }
                }
                catch (Exception e)
                {
                    dataContext.Result = false;
                    return (false);
                }
            }

            dataContext.ErrorCode = "0";
            dataContext.ErrorText = "Success";


            return true;
        }

        public static bool GetPreviousLocation(InventoryAuditVO audit, TrakkerItem item, CommonDatabaseContext dataContext)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_storenumber", audit.StoreNumber));
            oParams.Add(new OracleProcParam("p_AuditID", audit.AuditId));
            oParams.Add(new OracleProcParam("p_icn_shop", item.Icn.ShopNumber));
            oParams.Add(new OracleProcParam("p_icn_year", item.Icn.LastDigitOfYear));
            oParams.Add(new OracleProcParam("p_icn_doc_type", (int)item.Icn.DocumentType));
            oParams.Add(new OracleProcParam("p_icn_doc", item.Icn.DocumentNumber));
            oParams.Add(new OracleProcParam("p_icn_item", item.Icn.ItemNumber));
            oParams.Add(new OracleProcParam("p_icn_sub_item", item.Icn.SubItemNumber));
            oParams.Add(new OracleProcParam("o_loc", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 10));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            //refCursors.Add(new PairType<string, string>("o_loc", "o_loc"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "AUDIT_REPORTS", "getPrevLoc", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
                dataContext.Result = retVal;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("GetPreviousLocation Failed", oEx);
                dataContext.ErrorCode = "GetPreviousLocationFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                dataContext.Result = false;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("GetPreviousLocation Failed: return value is false", new ApplicationException());
                dataContext.ErrorCode = dA.ErrorCode + " --- GetPreviousLocation";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                dataContext.Result = false;
                return (false);
            }

            if (outputDataSet == null)
            {
                dataContext.Result = false;
                return false;
            }
            if (outputDataSet.Tables.Count > 0)
            {
                try
                {
                    if (outputDataSet.Tables["output"] != null && outputDataSet.Tables["output"].Rows.Count > 0)
                    {
                        item.PreviousLocation = Utilities.GetStringValue(outputDataSet.Tables["output"].Rows[0][1], string.Empty);
                    }
                }
                catch (Exception e)
                {
                    dataContext.Result = false;
                    return (false);
                }
            }

            dataContext.ErrorCode = "0";
            dataContext.ErrorText = "Success";


            return true;
        }
        
        public static bool GetStores(List<SiteId> stores, CommonDatabaseContext dataContext)
        {
            if (stores == null)
            {
                throw new ArgumentNullException("stores");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_stores", "o_stores"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "PAWN_AUDIT", "audit_get_stores", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("GetStores Failed", oEx);
                dataContext.ErrorCode = "GetStoresFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("GetStores Failed: return value is false", new ApplicationException());
                dataContext.ErrorCode = dA.ErrorCode + " --- GetStores";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null)
            {
                return false;
            }

            if (outputDataSet.Tables.Count == 1)
            {
                if (outputDataSet.Tables[0].TableName == "o_stores")
                {
                    foreach (DataRow dr in outputDataSet.Tables[0].Rows)
                    {
                        SiteId audit = new SiteId();
                        audit.StoreNumber = Utilities.GetStringValue(dr["storenumber"]);
                        audit.StoreNickName = Utilities.GetStringValue(dr["storenickname"]);

                        stores.Add(audit);
                    }
                }

                dataContext.ErrorCode = "0";
                dataContext.ErrorText = "Success";
            }

            return true;
        }

        public static bool GetSummary(InventoryAuditVO audit, CommonDatabaseContext dataContext)
        {
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", audit.StoreNumber));
            oParams.Add(new OracleProcParam("p_AuditID", audit.AuditId));
            oParams.Add(new OracleProcParam("o_last_inv_audit", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 10));
            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_psnl", "o_psnl"));
            refCursors.Add(new PairType<string, string>("o_term", "o_term"));
            refCursors.Add(new PairType<string, string>("o_chgOff", "o_chgOff"));
            refCursors.Add(new PairType<string, string>("o_Hist", "o_Hist"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = dA.issueSqlStoredProcCommand("ccsowner", "AUDIT_REPORTS", "SummaryReport", oParams,
                                                        refCursors, "o_return_code", "o_return_text",
                                                        out outputDataSet);
                dataContext.Result = retVal;
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("GetSummary Failed", oEx);
                dataContext.Result = false;
                dataContext.ErrorCode = "GetSummaryFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("GetSummary Failed: return value is false", new ApplicationException());
                dataContext.Result = false;
                dataContext.ErrorCode = dA.ErrorCode + " --- GetSummary";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null || outputDataSet.Tables.Count < 3)
            {
                return false;
            }

            dataContext.ErrorCode = "0";
            dataContext.ErrorText = "Success";

            dataContext.OutputDataSet1 = outputDataSet;

            dataContext.Result = true;
            return true;
        }

        public static bool GetAuditInfo(InventoryAuditVO audit, CommonDatabaseContext dataContext)
        {
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", audit.StoreNumber));
            oParams.Add(new OracleProcParam("p_AuditID", audit.AuditId));
            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_audit_info", "o_audit_info"));
            refCursors.Add(new PairType<string, string>("o_chg_off_info", "o_chg_off_info"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = dA.issueSqlStoredProcCommand("ccsowner", "AUDIT_REPORTS", "getAuditInfo", oParams,
                                                        refCursors, "o_return_code", "o_return_text",
                                                        out outputDataSet);
                dataContext.Result = retVal;
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("GetAuditInfo Failed", oEx);
                dataContext.Result = false;
                dataContext.ErrorCode = "GetAuditInfoFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("GetAuditInfo Failed: return value is false", new ApplicationException());
                dataContext.Result = false;
                dataContext.ErrorCode = dA.ErrorCode + " --- GetAuditInfo";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null || outputDataSet.Tables.Count != 2)
            {
                return false;
            }

            dataContext.ErrorCode = "0";
            dataContext.ErrorText = "Success";

            dataContext.OutputDataSet1 = outputDataSet;

            dataContext.Result = true;
            return true;
        }

        public static bool GetInventoryAudits(List<InventoryAuditVO> audits, string storenumber, string status, CommonDatabaseContext dataContext)
        {
            if (audits == null)
            {
                throw new ArgumentNullException("audits");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", storenumber));
            oParams.Add(new OracleProcParam("p_status", status));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_audits", "o_audits"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "PAWN_AUDIT", "get_audits", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
                dataContext.Result = retVal;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("GetInventoryAudits Failed", oEx);
                dataContext.ErrorCode = "GetInventoryAuditsFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                dataContext.Result = false;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("GetInventoryAudits Failed: return value is false", new ApplicationException());
                dataContext.ErrorCode = dA.ErrorCode + " --- GetInventoryAudits";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                dataContext.Result = false;
                return (false);
            }

            if (outputDataSet == null)
            {
                dataContext.Result = false;
                return false;
            }

            if (outputDataSet.Tables.Count == 1)
            {
                if (outputDataSet.Tables[0].TableName == "o_audits")
                {
                    foreach (DataRow dr in outputDataSet.Tables[0].Rows)
                    {
                        InventoryAuditVO audit = new InventoryAuditVO();
                        audit.AuditId = Utilities.GetIntegerValue(dr["audit_id"]);
                        audit.StoreNumber = Utilities.GetStringValue(dr["storenumber"]);
                        audit.AuditType = (AuditType)Utilities.GetEnumFromConstantName<AuditType>(Utilities.GetStringValue(dr["audit_type"]));
                        audit.DateInitiated = Utilities.GetDateTimeValue(dr["date_initiated"]);
                        audit.ExitingShopManager = Utilities.GetStringValue(dr["Exiting_shop_manager"]);
                        audit.InitiatedBy = Utilities.GetStringValue(dr["initiated_by"]);
                        audit.LastUpdated = Utilities.GetDateTimeValue(dr["lastupdatedate"]);
                        audit.DownloadDate = Utilities.GetDateTimeValue(dr["trak_download_date"], DateTime.MinValue);
                        audit.UploadDate = Utilities.GetDateTimeValue(dr["trak_upload_date"], DateTime.MinValue);
                        audit.Status = (AuditStatus)Utilities.GetEnumFromConstantName<AuditStatus>(Utilities.GetStringValue(dr["status_cd"]));
                        audit.StateCode = Utilities.GetStringValue(dr["state_code"]);
                        audits.Add(audit);
                    }
                }

                dataContext.ErrorCode = "0";
                dataContext.ErrorText = "Success";
                dataContext.Result = true;
            }

            dataContext.Result = true;
            return true;
        }

        public static bool GetMissingItems(List<TrakkerItem> items, InventoryAuditVO audit, CommonDatabaseContext dataContext)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", audit.StoreNumber));
            oParams.Add(new OracleProcParam("p_AuditID", audit.AuditId));
            oParams.Add(new OracleProcParam("p_incld_all", "Y"));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_msg_items", "o_msg_items"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "AUDIT_REPORTS", "missingItemsReport", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                dataContext.Result = retVal;
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("GetMissingItems Failed", oEx);
                dataContext.Result = false;
                dataContext.ErrorCode = "GetMissingItemsFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("GetMissingItems Failed: return value is false", new ApplicationException());
                dataContext.Result = false;
                dataContext.ErrorCode = dA.ErrorCode + " --- GetMissingItems";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null)
            {
                return false;
            }

            if (outputDataSet.Tables.Count == 1)
            {
                if (outputDataSet.Tables[0].TableName == "o_msg_items")
                {
                    foreach (DataRow dr in outputDataSet.Tables["o_msg_items"].Rows)
                    {
                        TrakkerItem item = new TrakkerItem();
                        item.Icn = new Icn(Utilities.GetStringValue(dr["icn"]));
                        item.Description = Utilities.GetStringValue(dr["md_desc"]);
                        item.Quantity = Utilities.GetIntegerValue(dr["quantity"]);
                        item.PfiAmount = Utilities.GetDecimalValue(dr["pfi_amount"]);
                        item.RetailAmount = Utilities.GetDecimalValue(dr["retail_price"]);
                        item.Status = Utilities.GetStringValue(dr["STATUS_CD"]);
                        item.TrakkerId = Utilities.GetIntegerValue(dr["trak_id"]);

                        item.SequenceNumber = Utilities.GetIntegerValue(dr["trak_seq"]);
                        item.TrakNewFlag = Utilities.GetCharValue(dr["trak_new_flag"]);
                        item.Location = Utilities.GetStringValue(dr["trak_loc"]);
                        item.LocationCode = Utilities.GetStringValue(dr["trak_loc_code"]);
                        item.ChargeOffReason = Utilities.GetStringValue(dr["STATUS_REASON"]);
                        item.XIcn = new Icn(Utilities.GetStringValue(dr["xref"]));
                        item.TempStatus = Utilities.GetStringValue(dr["temp_status"]);

                        item.CatCode = Utilities.GetIntegerValue(dr["cat_code"]);
                        item.GunNumber = Utilities.GetIntegerValue(dr["gun_number"]);

                        item.IsJewelry = Utilities.IsJewelry(item.CatCode);
                        item.IsGun = Utilities.IsGun(item.GunNumber, item.CatCode, item.IsJewelry, item.MerchandiseType);

                        items.Add(item);
                    }
                }

                dataContext.ErrorCode = "0";
                dataContext.ErrorText = "Success";
            }

            dataContext.Result = true;
            return true;
        }

        public static bool GetTemporaryIcns(List<TrakkerItem> items, InventoryAuditVO audit, CommonDatabaseContext dataContext)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", audit.StoreNumber));
            oParams.Add(new OracleProcParam("p_downtrak_date", audit.DownloadDate.ToString("MM/dd/yyyy")));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_temp_icns", "o_temp_icns"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "PAWN_AUDIT", "get_temp_icns", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                dataContext.Result = retVal;
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("GetTemporaryIcns Failed", oEx);
                dataContext.Result = false;
                dataContext.ErrorCode = "GetTemporaryIcnsFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("GetTemporaryIcns Failed: return value is false", new ApplicationException());
                dataContext.Result = false;
                dataContext.ErrorCode = dA.ErrorCode + " --- GetMissingItems";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null)
            {
                return false;
            }

            if (outputDataSet.Tables.Count == 1)
            {
                if (outputDataSet.Tables[0].TableName == "o_temp_icns")
                {
                    foreach (DataRow dr in outputDataSet.Tables["o_temp_icns"].Rows)
                    {
                        TrakkerItem item = new TrakkerItem();
                        item.Icn = new Icn(Utilities.GetStringValue(dr["icn"]));
                        item.Description = Utilities.GetStringValue(dr["md_desc"]);
                        item.PfiAmount = Utilities.GetDecimalValue(dr["cost"], 0);
                        item.Status = Utilities.GetStringValue(dr["STATUS_CD"]);
                        item.SequenceNumber = Utilities.GetIntegerValue(dr["temp_status"], 0);

                        if (item.SequenceNumber > 0)
                        {
                            items.Add(item);
                        }
                    }
                }

                dataContext.ErrorCode = "0";
                dataContext.ErrorText = "Success";
            }

            dataContext.Result = true;
            return true;
        }

        public static bool GetTrakkerItems(List<TrakkerItem> items, InventoryAuditVO audit, CommonDatabaseContext dataContext)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", audit.StoreNumber));
            oParams.Add(new OracleProcParam("p_audit_id", audit.AuditId));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_audit_mdse", "o_audit_mdse"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "PAWN_AUDIT", "load_audit_mdse", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                dataContext.Result = retVal;
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("GetTrakkerItems Failed", oEx);
                dataContext.Result = false;
                dataContext.ErrorCode = "GetTrakkerItemsFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("GetTrakkerItems Failed: return value is false", new ApplicationException());
                dataContext.Result = false;
                dataContext.ErrorCode = dA.ErrorCode + " --- GetTrakkerItems";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null)
            {
                return false;
            }

            if (outputDataSet.Tables.Count == 1)
            {
                if (outputDataSet.Tables[0].TableName == "o_audit_mdse")
                {
                    int recordNumber = 1;
                    foreach (DataRow dr in outputDataSet.Tables["o_audit_mdse"].Rows)
                    {
                        TrakkerItem item = new TrakkerItem();
                        item.Icn = new Icn(
                            Utilities.GetIntegerValue(dr["ICN_DOC"], 0),
                            Utilities.GetIntegerValue(dr["ICN_DOC_TYPE"], 0),
                            Utilities.GetIntegerValue(dr["ICN_ITEM"], 0),
                            Utilities.GetIntegerValue(dr["ICN_YEAR"], 0),
                            Utilities.GetIntegerValue(dr["ICN_STORE"], 0),
                            Utilities.GetIntegerValue(dr["ICN_SUB_ITEM"], 0));
                        item.Status = Utilities.GetStringValue(dr["STATUS_CD"]);
                        item.CatCode = Utilities.GetIntegerValue(dr["CAT_CODE"], 0);
                        item.Quantity = Utilities.GetIntegerValue(dr["QUANTITY"], 0);
                        item.PfiAmount = Utilities.GetDecimalValue(dr["PFI_AMOUNT"], 0);
                        item.DispDoc = Utilities.GetIntegerValue(dr["DISP_DOC"], 0);
                        item.HomeOffice = Utilities.GetIntegerValue(dr["HOME_OFFICE"], 0);
                        item.RfbNo = Utilities.GetIntegerValue(dr["RFB_NO"], 0);
                        item.RecordNumber = recordNumber++;

                        items.Add(item);
                    }
                }

                dataContext.ErrorCode = "0";
                dataContext.ErrorText = "Success";
            }

            dataContext.Result = true;
            return true;
        }

        public static bool GetUnexpectedItems(List<TrakkerItem> items, InventoryAuditVO audit, CommonDatabaseContext dataContext)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", audit.StoreNumber));
            oParams.Add(new OracleProcParam("p_AuditID", audit.AuditId));
            oParams.Add(new OracleProcParam("p_incld_all", "Y"));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("o_unexp_items", "o_unexp_items"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "AUDIT_REPORTS", "getUnexpectedItems", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                dataContext.Result = retVal;
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("GetUnexpectedItems Failed", oEx);
                dataContext.Result = false;
                dataContext.ErrorCode = "GetUnexpectedItemsFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("GetUnexpectedItems Failed: return value is false", new ApplicationException());
                dataContext.Result = false;
                dataContext.ErrorCode = dA.ErrorCode + " --- GetUnexpectedItems";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null)
            {
                return false;
            }

            if (outputDataSet.Tables.Count == 1)
            {
                if (outputDataSet.Tables[0].TableName == "o_unexp_items")
                {
                    foreach (DataRow dr in outputDataSet.Tables["o_unexp_items"].Rows)
                    {
                        TrakkerItem item = new TrakkerItem();
                        item.Icn = new Icn(Utilities.GetStringValue(dr["icn"]));
                        item.Description = Utilities.GetStringValue(dr["md_desc"]);
                        item.Quantity = Utilities.GetIntegerValue(dr["quantity"], 0);
                        item.PfiAmount = Utilities.GetDecimalValue(dr["pfi_amount"], 0);
                        item.RetailAmount = Utilities.GetDecimalValue(dr["retail_price"], 0);
                        item.Status = Utilities.GetStringValue(dr["STATUS_CD"]);
                        item.TrakkerId = Utilities.GetIntegerValue(dr["trak_id"], 0);

                        item.SequenceNumber = Utilities.GetIntegerValue(dr["trak_seq"], 0);
                        item.TrakNewFlag = Utilities.GetCharValue(dr["trak_new_flag"]);
                        item.Location = Utilities.GetStringValue(dr["trak_loc"]);
                        item.LocationCode = Utilities.GetStringValue(dr["trak_loc_code"]);
                        item.ChargeOffReason = Utilities.GetStringValue(dr["STATUS_REASON"]);
                        item.XIcn = new Icn(Utilities.GetStringValue(dr["xicn"]));
                        item.TempStatus = Utilities.GetStringValue(dr["temp_status"]);

                        items.Add(item);
                    }
                }

                dataContext.ErrorCode = "0";
                dataContext.ErrorText = "Success";
            }

            dataContext.Result = true;
            return true;
        }

        public static bool UploadTrakkerItem(TrakkerItem item, int auditId, int trakkerId, string storenumber, CommonDatabaseContext dataContext)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", storenumber));
            oParams.Add(new OracleProcParam("p_icn_store", item.Icn.ShopNumber));
            oParams.Add(new OracleProcParam("p_icn_year", item.Icn.LastDigitOfYear));
            oParams.Add(new OracleProcParam("p_icn_doc", item.Icn.DocumentNumber));
            oParams.Add(new OracleProcParam("p_icn_doc_type", (int)item.Icn.DocumentType));
            oParams.Add(new OracleProcParam("p_icn_item", item.Icn.ItemNumber));
            oParams.Add(new OracleProcParam("p_icn_sub_item", item.Icn.SubItemNumber));
            oParams.Add(new OracleProcParam("p_TRAK_FLAG", item.TrakFlag.ToString()));
            oParams.Add(new OracleProcParam("p_TRAK_SEQ", item.SequenceNumber));
            oParams.Add(new OracleProcParam("p_TRAK_LOC", item.Location));
            oParams.Add(new OracleProcParam("p_TRAK_ORG_LOC", item.CreateTrackLocation()));
            oParams.Add(new OracleProcParam("p_TRAK_AMOUNT", item.PfiAmount));
            oParams.Add(new OracleProcParam("p_TRAK_ID", trakkerId));
            oParams.Add(new OracleProcParam("p_TRAK_LOC_CODE", item.LocationCode));
            oParams.Add(new OracleProcParam("p_TRAK_NEW_FLAG", null));
            oParams.Add(new OracleProcParam("p_TRAK_CAT_CODE", item.CatCode));
            oParams.Add(new OracleProcParam("p_user_name", dataContext.UserName));
            oParams.Add(new OracleProcParam("p_quantity", item.Quantity));
            oParams.Add(new OracleProcParam("p_audit_id", auditId));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "PAWN_AUDIT", "audit_upload_trakker", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                dataContext.Result = retVal;
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("UploadTrakkerItem Failed", oEx);
                dataContext.Result = false;
                dataContext.ErrorCode = "UploadTrakkerItemFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("UploadTrakkerItem Failed: return value is false", new ApplicationException());
                dataContext.Result = false;
                dataContext.ErrorCode = dA.ErrorCode + " --- UploadTrakkerItem";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            dataContext.ErrorCode = "0";
            dataContext.ErrorText = "Success";

            dataContext.Result = true;
            return true;
        }

        public static bool PostAudit(InventoryAuditVO audit, CommonDatabaseContext dataContext)
        {
            if (audit == null)
            {
                throw new ArgumentNullException("audit");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", audit.StoreNumber));
            oParams.Add(new OracleProcParam("p_icn", true, new List<string>() { "1", "2", "3" }));
            oParams.Add(new OracleProcParam("l_downtrak_date", audit.DownloadDate.ToString("MM/dd/yyyy")));
            oParams.Add(new OracleProcParam("l_downtrak_time", GetDownloadUploadDateValue(audit.DownloadDate)));
            oParams.Add(new OracleProcParam("l_postaudit_date", ShopDateTime.Instance.FullShopDateTime.ToString("MM/dd/yyyy")));
            oParams.Add(new OracleProcParam("l_postaudit_time", GetDownloadUploadDateValue(ShopDateTime.Instance.FullShopDateTime)));
            oParams.Add(new OracleProcParam("l_uptrak_date", audit.UploadDate.ToString("MM/dd/yyyy")));
            oParams.Add(new OracleProcParam("l_uptrak_time", GetDownloadUploadDateValue(audit.UploadDate)));
            oParams.Add(new OracleProcParam("p_user_id", dataContext.FullUserName));
            oParams.Add(new OracleProcParam("p_cash_drawer", string.Empty));
            oParams.Add(new OracleProcParam("p_audit_id", audit.AuditId));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            //refCursors.Add(new PairType<string, string>("o_stores", "o_stores"));

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "PAWN_AUDIT", "post_audit", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                dataContext.Result = retVal;
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("PostAudit Failed", oEx);
                dataContext.Result = false;
                dataContext.ErrorCode = "PostAuditFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("PostAudit Failed: return value is false", new ApplicationException());
                dataContext.Result = false;
                dataContext.ErrorCode = dA.ErrorCode + " --- PostAudit";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            return true;
        }

        public static bool ProcessMissing(ProcessMissingContext processMissingContext, CommonDatabaseContext dataContext)
        {
            if (processMissingContext == null)
            {
                throw new ArgumentNullException("processMissingContext");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            List<string> icns = (from i in processMissingContext.Items
                                 select i.Icn.ToString()).ToList();



            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", processMissingContext.Audit.StoreNumber));
            oParams.Add(new OracleProcParam("p_icn", true, icns));
            oParams.Add(new OracleProcParam("p_coff_reason", processMissingContext.UserOption == ProcessMissingUserOptions.CHARGEOFF ? processMissingContext.ChargeOffReason.ToString() : string.Empty));
            oParams.Add(new OracleProcParam("p_audit_id", processMissingContext.Audit.AuditId));
            oParams.Add(new OracleProcParam("p_temp_icn_seq", processMissingContext.TrakkerSequenceNumber));
            oParams.Add(new OracleProcParam("p_user_option", processMissingContext.UserOption.ToString()));
            oParams.Add(new OracleProcParam("p_user_id", dataContext.FullUserName));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "PAWN_AUDIT", "process_missing_items", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                dataContext.Result = retVal;
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ProcessMissing Failed", oEx);
                dataContext.Result = false;
                dataContext.ErrorCode = "ProcessMissingFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ProcessMissing Failed: return value is false", new ApplicationException());
                dataContext.Result = false;
                dataContext.ErrorCode = dA.ErrorCode + " --- ProcessMissing";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            dataContext.ErrorCode = "0";
            dataContext.ErrorText = "Success";

            dataContext.Result = true;
            return true;
        }

        public static bool ProcessUnexpected(ProcessUnexpectedContext processUnexpectedContext, CommonDatabaseContext dataContext)
        {
            if (processUnexpectedContext == null)
            {
                throw new ArgumentNullException("processUnexpectedContext");
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            List<string> icns = new List<string>();
            List<string> retailPrices = new List<string>();
            List<string> catCodes = new List<string>();
            foreach (TrakkerItem item in processUnexpectedContext.Items)
            {
                icns.Add(item.Icn.ToString());
                retailPrices.Add(item.RetailAmount.ToString());
                catCodes.Add(item.CatCode.ToString());
            }

            //Create parameter list
            List<OracleProcParam> oParams = new List<OracleProcParam>();
            oParams.Add(new OracleProcParam("p_store_number", processUnexpectedContext.Audit.StoreNumber));
            oParams.Add(new OracleProcParam("p_user_option", processUnexpectedContext.UserOption.ToString()));
            oParams.Add(new OracleProcParam("p_icn", true, icns));
            oParams.Add(new OracleProcParam("p_retail_price", true, retailPrices));
            oParams.Add(new OracleProcParam("p_cat_code", true, catCodes));
            oParams.Add(new OracleProcParam("p_user_name", dataContext.FullUserName));
            oParams.Add(new OracleProcParam("p_downtrak_date", processUnexpectedContext.Audit.DownloadDate.ToString("MM/dd/yyyy")));
            oParams.Add(new OracleProcParam("p_downtrak_time", GetDownloadUploadDateValue(processUnexpectedContext.Audit.DownloadDate)));
            oParams.Add(new OracleProcParam("p_audit_id", processUnexpectedContext.Audit.AuditId));


            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Make stored proc call
            bool retVal;
            DataSet outputDataSet;
            try
            {
                retVal = GlobalDataAccessor.Instance.OracleDA.issueSqlStoredProcCommand("ccsowner",
                                                                                            "PAWN_AUDIT", "process_unexpected", oParams, refCursors, "o_return_code",
                                                                                            "o_return_text", out outputDataSet);
                dataContext.Result = retVal;
                dataContext.ErrorCode = dA.ErrorCode;
                dataContext.ErrorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ProcessUnexpected Failed", oEx);
                dataContext.Result = false;
                dataContext.ErrorCode = "ProcessUnexpectedFailed";
                dataContext.ErrorText = "OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ProcessUnexpected Failed: return value is false", new ApplicationException());
                dataContext.Result = false;
                dataContext.ErrorCode = dA.ErrorCode + " --- ProcessUnexpected";
                dataContext.ErrorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            foreach (TrakkerItem item in processUnexpectedContext.Items)
            {
                if (processUnexpectedContext.UserOption == ProcessUnexpectedUserOption.REACTIVATE)
                {
                    item.TrakNewFlag = 'C';
                    item.TempStatus = "PFI";
                }
                else if (processUnexpectedContext.UserOption == ProcessUnexpectedUserOption.CHARGEON)
                {
                    item.Status = "CON";
                    item.TempStatus = "PFI";
                }
                else if (processUnexpectedContext.UserOption == ProcessUnexpectedUserOption.UNSCAN)
                {
                    item.TrakNewFlag = 'N';
                }
                else if (processUnexpectedContext.UserOption == ProcessUnexpectedUserOption.UNDO)
                {
                    item.TrakNewFlag = ' ';
                }
            }

            dataContext.ErrorCode = "0";
            dataContext.ErrorText = "Success";

            dataContext.Result = true;
            return true;
        }




        private static string GetDownloadUploadDateValue(DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue)
            {
                return string.Empty;
            }

            return dateTime.ToShortDateString() + " " + dateTime.TimeOfDay;
        }
    }
}
