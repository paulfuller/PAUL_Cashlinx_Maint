using System;
using System.Collections.Generic;
using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using System.Data;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Type;

namespace Common.Controllers.Database
{
    public class GunBookUtilities
    {
        public static readonly string GUN_BOOK_TABLE = "gun";
        public static bool getGunbookRecords(String gun_bound, int startPage, int endPage, String status, String storeNumber,String userName, out DataTable gunbook, out string errorCode,out string errorMesg)
        {
           bool returnVal = false;
           gunbook = new DataTable();
           List<OracleProcParam> inputParamsList = new List<OracleProcParam>();
           inputParamsList.Add(new OracleProcParam("p_gun_bound", gun_bound));
           inputParamsList.Add(new OracleProcParam("p_gun_book_start_page", startPage));
           inputParamsList.Add(new OracleProcParam("p_gun_book_end_page", endPage));
           inputParamsList.Add(new OracleProcParam("p_gun_status", status));
           inputParamsList.Add(new OracleProcParam("p_store_number", storeNumber));
           inputParamsList.Add(new OracleProcParam("p_user_name", userName)); 
           returnVal = internalGetGunbookRecords(inputParamsList, out gunbook, out  errorCode,out  errorMesg);
           return returnVal;
        }

        private static bool internalGetGunbookRecords(List<OracleProcParam> inputParams, out DataTable gunbook, out string errorCode, out string errorMesg)
        {
            bool returnVal = false;
            gunbook =  new DataTable();
            errorCode = string.Empty;
            errorMesg = string.Empty;
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA; 

            List<PairType<string, string>> refCursList = new List<PairType<string, string>>();
            refCursList.Add(new PairType<String, String>("r_gunbook_records", GUN_BOOK_TABLE));
            DataSet outputDataSet;
            
            try
            {
                returnVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "PAWN_GUNBOOK_UTILITIES",
                    "get_gunbook_records", inputParams,
                    refCursList, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling get_gunbook_records stored procedure", oEx);
                //errorCode = "GetCustDetails";
                //errorMesg = "Exception: " + oEx.Message;
                return (false);
            }

            if (returnVal == false)
            {
                errorCode = dA.ErrorCode;
                errorMesg = dA.ErrorDescription;
                return (false);
            }

            if (outputDataSet != null)
            {
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    gunbook = outputDataSet.Tables[GUN_BOOK_TABLE];
                }
            }


            return returnVal;
        }


        public static bool UpdateGunDescriptionData()
        {
            GlobalDataAccessor.Instance.beginTransactionBlock();
            string errorCode;
            string errorMessage;
            bool gunMdseUpdate=MerchandiseProcedures.UpdateGunData(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[0],
                GlobalDataAccessor.Instance.DesktopSession.UserName,
                out errorCode,
                out errorMessage);
            if (gunMdseUpdate)
            {
                bool deleteOtherdsc = MerchandiseProcedures.DeleteOtherDesc(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                    GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[0].mStore,
                    GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[0].mYear,
                    GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[0].mDocNumber,
                    GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[0].mDocType,
                    GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[0].mItemOrder,
                    out errorCode,
                    out errorMessage);
                if (deleteOtherdsc)
                {

                    if ((from iAttr in GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[0].Attributes
                         where iAttr.Answer.AnswerCode == 999
                         select ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                         GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber, 
                         GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[0].mStore, 
                         GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[0].mYear, 
                         GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[0].mDocNumber, 
                         string.Format("{0}", GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[0].mDocType), 
                         GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[0].mItemOrder, 0, iAttr.MaskOrder, 
                         iAttr.Answer.AnswerText, GlobalDataAccessor.Instance.DesktopSession.UserName, 
                         out errorCode, out errorMessage)).Any(otherDscVal => !otherDscVal))
                    {
                        GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                        return false;
                    }
                    Item pawnItem = GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[0];
                    string sErrorText = string.Empty;
                    string sErrorCode = string.Empty;
                    if (!string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[0].Comment))
                    {
                        ProcessTenderProcedures.ExecuteInsertOtherDscRecord(
                            GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                            pawnItem.mStore, pawnItem.mYear, pawnItem.mDocNumber,
                            pawnItem.mDocType, pawnItem.mItemOrder, 0, 999,
                            pawnItem.Comment, GlobalDataAccessor.Instance.DesktopSession.UserName, out sErrorCode, out sErrorText);
                        if (sErrorCode != "0")
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, null, "Could not insert comment data when updating gun description for " + pawnItem.GunNumber.ToString());
                        }

                    }


                }
                else
                {
                    GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return false;
                }
                DataTable gunTableData;
                Item gunItemData;
                MerchandiseProcedures.GetGunData(GlobalDataAccessor.Instance.OracleDA,
                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                    Utilities.GetIntegerValue(GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items[0].GunNumber),
                                    out gunTableData,
                                    out gunItemData,
                                    out errorCode,
                                    out errorMessage);
                GlobalDataAccessor.Instance.DesktopSession.GunData = gunTableData;
                GlobalDataAccessor.Instance.DesktopSession.GunItemData = gunItemData;
                GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT);
                return true;
            }
            GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
            return false;
        }
    }
}
