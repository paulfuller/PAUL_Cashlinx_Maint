using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;
using Common.Controllers.Application;
using Common.Libraries.Objects;
using Common.Libraries.Utility;

namespace Common.Controllers.Database.Procedures
{
    public static class PawnProcedures
    {
        /// <summary>
        /// Retrieves PFI Mailer data based on selected PFI Mailer option and parameters
        /// </summary>
        /// <param name="dataAccessor"></param>
        /// <param name="pfiMailerOption"></param>
        /// <param name="pfiMailerObjects"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <param name="pfiMailerAdjustmentDays"></param>
        /// <param name="startTicketNumber"></param>
        /// <param name="endTicketNumber"></param>
        /// <param name="pfiDate"></param>
        /// <returns></returns>
        public static bool ExecuteGetPFIMailerData(
            OracleDataAccessor dataAccessor,
            PFIMailerOption pfiMailerOption,
            out List<ReportObject.PFIMailer> pfiMailerObjects,
            out string errorCode,
            out string errorText,
            int pfiMailerAdjustmentDays = 0,
            int startTicketNumber = 0,
            int endTicketNumber = 0,
            DateTime pfiDate = new DateTime()
            )
        {
            // initialize error output variables
            pfiMailerObjects = new List<ReportObject.PFIMailer>();
            errorCode = String.Empty;
            errorText = String.Empty;

            // Ensure the data accessor is valid
            if (dataAccessor == null || !dataAccessor.Initialized)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetPFIMailerData Failed",
                    new ApplicationException("ExecuteGetPFIMailerData Failed: Data accessor instance is invalid"));
                return (false);
            }

            // Set up input variables
            var inParams = new List<OracleProcParam>
                                             {
                                                 new OracleProcParam("p_pfi_date", pfiDate.ToShortDateString()),
                                                 new OracleProcParam("p_store_number", GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.StoreNumber),
                                                 new OracleProcParam("p_pfi_mailer_adjustment_days", pfiMailerAdjustmentDays),
                                                 new OracleProcParam("p_startTicketNumber", startTicketNumber),
                                                 new OracleProcParam("p_endTicketNumber", endTicketNumber),
                                                 new OracleProcParam("p_pfiMailerOption", (int)pfiMailerOption)
                                             };


            // Set up output
            DataSet outputDataSet;
            var retVal = false;
            var refCursArr = new List<PairType<string, string>>
            {
                new PairType<string, string>("pfi_mailer_info", "o_pfi_mailer_info")
            };

            try
            {
                retVal = dataAccessor.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_mdse_procs",
                    "get_pfi_mailer_data", inParams, 
                    refCursArr,"o_return_code",
                    "o_return_text", 
                    out outputDataSet);

                errorCode = dataAccessor.ErrorCode;
                errorText = dataAccessor.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetPFIMailerData Failed", oEx);
                errorCode = " --- ExecuteGetPFIMailerData";
                errorText = " --- OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetPFIMailerData Failed: return value is false", new ApplicationException());
                errorCode = dataAccessor.ErrorCode + " -- ExecuteGetPFIMailerData";
                errorText = dataAccessor.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null || !outputDataSet.IsInitialized ||
               (outputDataSet.Tables.Count <= 0))
            {
                return (false);
            }

            pfiMailerObjects = new List<ReportObject.PFIMailer>();

            ReportObject.PFIMailer pfiMailerObject;

            foreach (DataRow pfiMailerDataRow in outputDataSet.Tables[0].Rows)
            {
                var pfiMailerFee = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetPFIMailerFee(GlobalDataAccessor.Instance.CurrentSiteId);

                pfiMailerObject = new ReportObject.PFIMailer();

                pfiMailerObject.ticketNumber = Utilities.GetIntegerValue(pfiMailerDataRow["ticket_number"]);
                pfiMailerObject.customerNumber = Utilities.GetIntegerValue(pfiMailerDataRow["customer_number"]);
                pfiMailerObject.originalPFINote = Utilities.GetDateTimeValue(pfiMailerDataRow["PFI_NOTE"]);
                pfiMailerObject.printNotice = pfiMailerDataRow["PRINT_NOTICE"].ToString();
                pfiMailerObject.customerName = pfiMailerDataRow["customer_name"].ToString();
                pfiMailerObject.customerId = pfiMailerDataRow["customer_id"].ToString();
                pfiMailerObject.customerAddress = pfiMailerDataRow["customer_address"].ToString();
                pfiMailerObject.customerCity = pfiMailerDataRow["customer_city"].ToString();
                pfiMailerObject.customerState = pfiMailerDataRow["customer_state"].ToString();
                pfiMailerObject.customerZipCode = pfiMailerDataRow["customer_zipCode"].ToString();
                pfiMailerObject.pfiEligibleDate = Utilities.GetDateTimeValue(pfiMailerDataRow["pfi_eligible_date"]);
                pfiMailerObject.storeName = pfiMailerDataRow["store_name"].ToString();
                pfiMailerObject.storeNumber = Utilities.GetIntegerValue(pfiMailerDataRow["store_number"]);
                pfiMailerObject.storeAddress = pfiMailerDataRow["store_address"].ToString();
                pfiMailerObject.storeCity = pfiMailerDataRow["store_city"].ToString();
                pfiMailerObject.storeState = pfiMailerDataRow["store_state"].ToString();
                pfiMailerObject.storeZipCode = pfiMailerDataRow["store_zipCode"].ToString();
                pfiMailerObject.storePhone = pfiMailerDataRow["store_phone"].ToString();
                pfiMailerObject.pfiMailerFee = pfiMailerFee;
            
                pfiMailerObjects.Add(pfiMailerObject);
            }

            // method completed successfully
            return (true);

        }

        /// <summary>
        /// Updates a Pawn Loan Record setting the PFI_NOTE and PRINT_NOTICE accourdingly.
        /// </summary>
        /// <param name="dataAccessor"></param>
        /// <param name="storeNumber"></param>
        /// <param name="ticketNumber"></param>
        /// <param name="originalPFINote"></param>
        /// <param name="pfiNotice"></param>
        /// <param name="pfiEligibleDate"></param>
        /// <param name="fee_amount"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteUpdatePFIMailerData(
            OracleDataAccessor dataAccessor,
            out string errorCode,
            out string errorText,
            int storeNumber = 0,
            int ticketNumber = 0,
            decimal fee_amount = 0,
            DateTime originalPFINote = new DateTime(),
            string pfiNotice = "",
            DateTime pfiEligibleDate = new DateTime()
            )
        {
            errorCode = String.Empty;
            errorText = String.Empty;
            
            // Ensure the data accessor is valid
            if (dataAccessor == null || !dataAccessor.Initialized)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetPFIMailerData Failed",
                    new ApplicationException("ExecuteGetPFIMailerData Failed: Data accessor instance is invalid"));

                return (false);
            }

            // Set up input variables
            var inParams = new List<OracleProcParam>
                                    {
                                        new OracleProcParam("p_storeNumber", storeNumber),
                                        new OracleProcParam("p_ticketNumber", ticketNumber),
                                        new OracleProcParam("p_pfi_note", originalPFINote.ToString("MM/dd/yyyy")),
                                        new OracleProcParam("p_eligible_date", pfiEligibleDate.ToString("MM/dd/yyyy")),
                                        new OracleProcParam("p_date_made", ShopDateTime.Instance.ShopDate.ToString("MM/dd/yyyy hh:mm:ss tt")),
                                        new OracleProcParam("p_createdBy", GlobalDataAccessor.Instance.DesktopSession.FullUserName),
                                        new OracleProcParam("p_fee_amount", fee_amount)
                                    };

            var retVal = false;
            var refCursArr = new List<PairType<string, string>>();

            try
            {
                DataSet outputDataSet;
                retVal = dataAccessor.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_mdse_procs",
                    "update_pfi_mailer_data", inParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);

                errorCode = dataAccessor.ErrorCode;
                errorText = dataAccessor.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetPFIMailerData Failed", oEx);
                errorCode = " --- ExecuteGetPFIMailerData";
                errorText = " --- OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetPFIMailerData Failed: return value is false", new ApplicationException());
                errorCode = dataAccessor.ErrorCode + " -- ExecuteGetPFIMailerData";
                errorText = dataAccessor.ErrorDescription + " -- Return value is false";
                return (false);
            }

            errorCode = "0";
            errorText = "Success";
            return true;
        }

        /// <summary>
        /// Updates a Pawn Loan Array setting the PFI_NOTE and PRINT_NOTICE accourdingly.
        /// </summary>
        /// <param name="dataAccessor"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <param name="storeNumber"></param>
        /// <param name="ticketNumber"></param>
        /// <param name="originalPFINote"></param>
        /// <param name="pfiEligibleDate"></param>
        /// <param name="fee_amount"></param>
        /// <returns></returns>
        public static bool ExecuteAddPFIMailerDataArray(
                    OracleDataAccessor dataAccessor,
                out string errorCode,
                out string errorText,
                int storeNumber,
                List<int> ticketNumber,                 // p_ticketNumber
                IEnumerable<DateTime> originalPFINote,  // p_pfi_note
                IEnumerable<DateTime> pfiEligibleDate,  // p_eligible_date
                decimal fee_amount = 0
            )
        {
            errorCode = String.Empty;
            errorText = String.Empty;

            // Ensure the data accessor is valid
            if (dataAccessor == null || !dataAccessor.Initialized)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetPFIMailerDataArray Failed",
                    new ApplicationException("ExecuteGetPFIMailerDataArray Failed: Data accessor instance is invalid"));

                return (false);
            }

            var oPFINote = originalPFINote.Select(dt => dt.ToString("MM/dd/yyyy")).ToList();
            var oPFIEDate = pfiEligibleDate.Select(dt => dt.ToString("MM/dd/yyyy")).ToList();

            // Set up input variables
            var inParams = new List<OracleProcParam>
                                    {
                                        new OracleProcParam("p_storeNumber", storeNumber),
                                        new OracleProcParam("p_ticketNumber", true, ticketNumber),
                                        new OracleProcParam("p_pfi_note", true, oPFINote), //originalPFINote.ToString("MM/dd/yyyy")),
                                        new OracleProcParam("p_eligible_date", true, oPFIEDate), //pfiEligibleDate.ToString("MM/dd/yyyy")),
                                        new OracleProcParam("p_date_made", ShopDateTime.Instance.ShopDate.ToString("MM/dd/yyyy hh:mm:ss tt")),
                                        new OracleProcParam("p_createdBy", GlobalDataAccessor.Instance.DesktopSession.FullUserName),
                                        new OracleProcParam("p_fee_amount", fee_amount)
                                    };

            var retVal = false;
            var refCursArr = new List<PairType<string, string>>();

            try
            {
                DataSet outputDataSet;
                retVal = dataAccessor.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_mdse_procs",
                    "update_pfi_mailer_data_array", inParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);

                errorCode = dataAccessor.ErrorCode;
                errorText = dataAccessor.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetPFIMailerDataArray Failed", oEx);
                errorCode = " --- ExecuteGetPFIMailerDataArray";
                errorText = " --- OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetPFIMailerDataArray Failed: return value is false", new ApplicationException());
                errorCode = dataAccessor.ErrorCode + " -- ExecuteGetPFIMailerDataArray";
                errorText = dataAccessor.ErrorDescription + " -- Return value is false";
                return (false);
            }

            errorCode = "0";
            errorText = "Success";
            return true;
        }

    }
}
