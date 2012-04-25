/************************************************************************
 * Namespace:       CashlinxDesktop.DesktopProcedures
 * Class:           PMetalStonesProcedures
 * 
 * Description      The class retrieves all the Metal and Stones data
 *                  from Oracle.
 * 
 * History
 * David D Wise, 4-10-2009, Initial Development
 * 
 * **********************************************************************/

using System;
using System.Collections.Generic;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using System.Data;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database.Procedures
{
    public static class PMetalStonesProcedures
    {
        public static readonly string PMETAL = "PMETAL";
        public static readonly string STONES = "STONES";

        public static bool ExecuteGetMetalStones(
            string                  PMetal_File_Name,
            string                  Stones_File_Name,
            out List<PMetalInfo>    PMETAL_INFO,
            out List<StoneInfo>     STONES_INFO,
            out string              errorCode,
            out string              errorText)
        {
            //Set default output values
            errorCode               = string.Empty;
            errorText               = string.Empty;
            PMETAL_INFO             = new List<PMetalInfo>();
            STONES_INFO             = new List<StoneInfo>();
            DataSet outputDataSet   = null;

            //Verify that the accessor is valid
            if (GlobalDataAccessor.Instance == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode   = "GetMetalStonesFailed";
                errorText   = "Invalid desktop session or data accessor instance";
                BasicExceptionHandler.Instance.AddException("GetMetalStonesFailed", new ApplicationException("Cannot execute the GetMetalStones retrieval stored procedure"));
                return      (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Create input list
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add cat pointer
            inParams.Add(new OracleProcParam("pmetalfile", PMetal_File_Name));
            inParams.Add(new OracleProcParam("stonefile", Stones_File_Name));

            //Setup ref cursor array
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();

            //Add general ref cursors
            refCursors.Add(new PairType<string, string>("c_pmlist", PMETAL));
            refCursors.Add(new PairType<string, string>("c_slist", STONES));

            //Create output data set names
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "PROKNOW", "proknow_procs", "get_metal_stone_values",
                    inParams, refCursors, "o_error_code", "o_error_desc",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = "GetMetalStonesFailed";
                errorText = "Invocation of Get_Metal_Stone_Values stored proc failed";
                BasicExceptionHandler.Instance.AddException("OracleException thrown when invoking Get_Metal_Stone_Values stored proc", oEx);
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
                if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                {
                    //Get Metal information and add to List<Metal>
                    if (outputDataSet.Tables.Contains(PMETAL))
                    {
                        PMETAL_INFO = new List<PMetalInfo>();
                        foreach (DataRow dataRow in outputDataSet.Tables[PMETAL].Rows)
                        {
                            PMetalInfo metalInfo        = new PMetalInfo();
                            metalInfo.Record_Type       = Utilities.GetStringValue  (dataRow["RECORD_TYPE"], "");
                            metalInfo.Category          = Utilities.GetIntegerValue (dataRow["CAT_CODE"], 0);
                            metalInfo.Class             = Utilities.GetStringValue  (dataRow["CLASS"], "");
                            metalInfo.Metal             = Utilities.GetStringValue  (dataRow["METAL"], "");
                            metalInfo.Karats            = Utilities.GetIntegerValue (dataRow["KARATS"], 0);
                            metalInfo.Gram_Low          = Utilities.GetDecimalValue (dataRow["GRAM_LOW"], 0);
                            metalInfo.Gram_High         = Utilities.GetDecimalValue (dataRow["GRAM_HIGH"], 0);
                            metalInfo.Loan_Buy_Per_Gram = Utilities.GetDecimalValue (dataRow["LOAN_BUY_PER_GRAM"], 0);
                            metalInfo.Retail_Per_Gram   = Utilities.GetDecimalValue (dataRow["RETAIL_PER_GRAM"], 0);
                            
                            PMETAL_INFO.Add(metalInfo);
                        }
                    }
                    // Get Stone information adn add to List<Stones>
                    if (outputDataSet.Tables.Contains(STONES))
                    {
                        STONES_INFO = new List<StoneInfo>();
                        foreach (DataRow dataRow in outputDataSet.Tables[STONES].Rows)
                        {
                            StoneInfo stoneInfo     = new StoneInfo();
                            stoneInfo.Record_Type   = Utilities.GetStringValue  (dataRow["RECORDTYPE"], "");
                            stoneInfo.Min_Points    = Utilities.GetDecimalValue (dataRow["MIN_POINTS"], 0);
                            stoneInfo.Max_Points    = Utilities.GetDecimalValue (dataRow["MAX_POINTS"], 0);
                            stoneInfo.Clarity       = Utilities.GetIntegerValue (dataRow["CLARITY"], 0);
                            stoneInfo.Color         = Utilities.GetIntegerValue (dataRow["COLOR"], 0);
                            stoneInfo.Loan          = Utilities.GetDecimalValue (dataRow["LOAN"], 0);
                            stoneInfo.Purchase      = Utilities.GetDecimalValue (dataRow["PURCHASE"], 0);
                            stoneInfo.Retail        = Utilities.GetDecimalValue (dataRow["RETAIL"], 0);

                            STONES_INFO.Add(stoneInfo);
                        }
                    }

                    errorCode = "0";
                    errorText = string.Empty;
                    return (true);
                }
            }

            errorCode = "GetMetalStonesFailed";
            errorText = "Operation failed";
            return (false);
        }
    }
}
