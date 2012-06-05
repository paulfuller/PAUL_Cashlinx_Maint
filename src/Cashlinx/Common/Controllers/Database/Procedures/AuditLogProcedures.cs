using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database.Procedures
{
    public static class AuditLogProcedures
    {
        public static bool ManagerOverrideReason(
            DateTime overrideDateTime,
            string storeNumber,
            string overrideID,
            ManagerOverrideTransactionType[] arManagerOverrideTransactionType,
            ManagerOverrideType[] arManagerOverrideType,
            decimal[] arSuggestedValue,
            decimal[] arApprovedValue,
            int[] arTransactionNumber,
            string comment,
            out string errorCode,
            out string errorText,
            string userId = null,
            string workId = null)
        {
            //Set default output values
            errorCode = string.Empty;
            errorText = string.Empty;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance == null ||
                !GlobalDataAccessor.Instance.IsDataAccessorValid())
            {
                errorCode = "ManagerOverrideReason";
                errorText = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("ManagerOverrideReason",
                                                            new ApplicationException(
                                                                "Cannot execute the Insert Override History stored procedure"));
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            OracleProcParam pManagerOverrideTransType = new OracleProcParam(ParameterDirection.Input
                                                                            , DataTypeConstants.PawnDataType.LISTSTRING
                                                                            , "p_ovr_tran_cd"
                                                                            , (arManagerOverrideTransactionType == null) ? 1 : arManagerOverrideTransactionType.Length);
            if (arManagerOverrideTransactionType != null && arManagerOverrideTransactionType.Length > 0)
            {
                for (int i = 0; i < arManagerOverrideTransactionType.Length; i++)
                {
                    pManagerOverrideTransType.AddValue(arManagerOverrideTransactionType[i].ToString());
                }
            }
            else
            {
                pManagerOverrideTransType.AddValue(string.Empty);
            }
            inParams.Add(pManagerOverrideTransType);

            OracleProcParam pManagerOverrideType = new OracleProcParam(ParameterDirection.Input
                                                                       , DataTypeConstants.PawnDataType.LISTSTRING
                                                                       , "p_ovr_sub_type_cd"
                                                                       , (arManagerOverrideType == null) ? 1 : arManagerOverrideType.Length);
            if (arManagerOverrideType != null && arManagerOverrideType.Length > 0)
            {
                for (int i = 0; i < arManagerOverrideType.Length; i++)
                {
                    pManagerOverrideType.AddValue(arManagerOverrideType[i].ToString());
                }
            }
            else
            {
                pManagerOverrideType.AddValue(string.Empty);
            }
            inParams.Add(pManagerOverrideType);

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_override_by", overrideID));

            OracleProcParam psuggestedvalue = new OracleProcParam(ParameterDirection.Input
                                                                  , DataTypeConstants.PawnDataType.LISTFLOAT
                                                                  , "p_sugg_value"
                                                                  , (arSuggestedValue == null) ? 1 : arSuggestedValue.Length);
            if (arSuggestedValue != null && arSuggestedValue.Length > 0)
            {
                for (int i = 0; i < arSuggestedValue.Length; i++)
                {
                    psuggestedvalue.AddValue(arSuggestedValue[i]);
                }
            }
            else
            {
                psuggestedvalue.AddValue(string.Empty);
            }
            inParams.Add(psuggestedvalue);

            OracleProcParam papprovedvalue = new OracleProcParam(ParameterDirection.Input
                                                                 , DataTypeConstants.PawnDataType.LISTFLOAT
                                                                 , "p_appr_value"
                                                                 , (arApprovedValue == null) ? 1 : arApprovedValue.Length);
            if (arApprovedValue != null && arApprovedValue.Length > 0)
            {
                for (int i = 0; i < arApprovedValue.Length; i++)
                {
                    papprovedvalue.AddValue(arApprovedValue[i]);
                }
            }
            else
            {
                papprovedvalue.AddValue(string.Empty);
            }
            inParams.Add(papprovedvalue);

            OracleProcParam ptrxnumber = new OracleProcParam(ParameterDirection.Input
                                                             , DataTypeConstants.PawnDataType.LISTINT
                                                             , "p_trx_number"
                                                             , (arTransactionNumber == null) ? 1 : arTransactionNumber.Length);
            if (arTransactionNumber != null && arTransactionNumber.Length > 0)
            {
                for (var i = 0; i < arTransactionNumber.Length; i++)
                {
                    ptrxnumber.AddValue(arTransactionNumber[i]);
                }
            }
            else
            {
                ptrxnumber.AddValue(string.Empty);
            }
            inParams.Add(ptrxnumber);

            inParams.Add(new OracleProcParam("p_trx_date", overrideDateTime));
            inParams.Add(new OracleProcParam("p_user_id", (!string.IsNullOrEmpty(userId) ? userId :  GlobalDataAccessor.Instance.DesktopSession.UserName)));
            inParams.Add(new OracleProcParam("p_workstation_id", (!string.IsNullOrEmpty(workId) ? workId : GlobalDataAccessor.Instance.CurrentSiteId.TerminalId)));
            inParams.Add(new OracleProcParam("p_ovr_comment", comment));

            //Create output data set names
            bool retVal = false;
            try
            {
                DataSet outputDataSet = null;
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_gen_procs", "insert_override_history",
                    inParams, null, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "ManagerOverrideReasonFailed";
                errorText = "Invocation of insert_override_history stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking insert_override_history stored proc", oEx);
                return (false);
            }

            //See if retVal is false
            if (retVal)
            {
                errorCode = "0";
                errorText = string.Empty;
                return (true);
            }
            else
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (true);
            }
        }

    }
}
