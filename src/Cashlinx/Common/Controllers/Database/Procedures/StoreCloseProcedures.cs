using System;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Type;

namespace Common.Controllers.Database.Procedures
{
    public static class StoreCloseProcedures
    {

        /// <summary>
        /// Call to trigger the auto store credit forfeiture stored procedure
        /// This will be called by the end of day process
        /// </summary>
        /// <param name="oda"></param>
        /// <param name="storeNumber"></param>
        /// <param name="currentDate"></param>
        /// <param name="userId"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool AutoForfeitStoreCredit(
            OracleDataAccessor oda,
            string storeNumber,
            string currentDate,
            string userId,
            out string errorCode,
            out string errorText)
        {

            //Check if auto store credit forfeiture is allowed in the store
            //if not allowerd return 
            bool autoSCForfeiture = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsAutoForfeitStoreCreditAllowed(GlobalDataAccessor.Instance.CurrentSiteId);
            if (!autoSCForfeiture)
            {
                errorCode = "1";
                errorText = "Auto store credit forfeiture is not allowed in this store";
                return false;
            }

            if (oda == null)
            {
                errorCode = "AutoForfeitStoreCreditFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            //Get the sale store credit duration from business rule
            SiteId site = GlobalDataAccessor.Instance.CurrentSiteId;
            int saleStoreCreditDuration = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetSaleStoreCreditDuration(site);

            //Get layaway store credit duration from business rule
            int layStoreCreditDuration = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetLayStoreCreditDuration(site);

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_sale_sc_duration", saleStoreCreditDuration));

            inParams.Add(new OracleProcParam("p_lay_sc_duration", layStoreCreditDuration));

            inParams.Add(new OracleProcParam("p_forfeiture_date", currentDate));

            inParams.Add(new OracleProcParam("p_user_id", userId));


            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                bool retVal = false;
                try
                {
                    DataSet outputDataSet;
                    retVal = oda.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_retail",
                        "auto_forfeiture_storecredit", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling auto_forfeiture_storecredit stored procedure", oEx);
                    errorCode = "Auto Forfeiture Store Credit";
                    errorText = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = oda.ErrorCode;
                    errorText = oda.ErrorDescription;
                    return (false);
                }
            }
            else
            {
                errorCode = "auto_forfeiture_storecreditFailed";
                errorText = "No Valid input parameters given";
                return (false);
            }
            errorCode = "0";
            errorText = "Success";
            return (true);
        }


    }
}
