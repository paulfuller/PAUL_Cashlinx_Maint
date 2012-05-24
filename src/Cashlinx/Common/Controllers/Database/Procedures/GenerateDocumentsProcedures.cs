using System;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects.Doc;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;
using System.Collections;

namespace Common.Controllers.Database.Procedures
{
    /// <summary>
    /// This class has one single method as of now
    /// which will supply the necessary values to
    /// print a document(New Pawn Loan Ticket)- FOR NOW
    /// This class will be improved as new requirements arise
    /// 8/17 Dee Bailey Added  two methods for Tag Reprint ExecuteGetTagShortCode & ExecuteGetTagLongCode
    /// </summary>
    public static class GenerateDocumentsProcedures
    {
        private static readonly string DOCUMENT_DATA = "documentdata";
        private static readonly string PRINTER_DATA = "printerdata";
        private static readonly string IPADDRESSPORT_DATA = "ipaddressportdata";
        public static readonly string TAGINFORMATION = "mdse_ref_cursor";

        /// <summary>
        /// This method returns a Hashtable with all the 
        /// initial data required for printing a document, when a List of <OracleProcParam>
        /// is passed in. 
        /// </summary>
        /// <param name="inputParams"></param>
        /// <param name="siteId"></param>
        /// <param name="usePda"></param>
        /// <param name="docdetails"></param>
        /// <param name="printerdetails"></param>
        /// <param name="ipportdetails"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public static Hashtable GenerateDocumentsEssentialInformation(List<OracleProcParam> inputParams, bool usePda, out DataTable docdetails, out DataTable printerdetails, out DataTable ipportdetails, out string errorCode, out string errorMesg)
        {
            var dA = GlobalDataAccessor.Instance.OracleDA;
            docdetails = null;
            printerdetails = null;
            ipportdetails = null;
            errorCode = string.Empty;
            errorMesg = string.Empty;

            if (CollectionUtilities.isEmpty(inputParams))
            {
                errorCode = "GenerateDocumentsFailed";
                errorMesg = "No input parameters specified";
            }

            //Add the use pda input param
            inputParams.Add(new OracleProcParam("p_use_pda_map", usePda ? "1" : "0"));

            //the names of the cursors that are being accessed here need to 
            //match with the one's in Stored Procedure.
            //r_doc_cursor, r_printer_cursor, r_multiple_printer_cursor
            string formName = inputParams[1][0];
            string termName = inputParams[0][0];
            var refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("r_doc_cursor", DOCUMENT_DATA));
            refCursArr.Add(new PairType<string, string>("r_printer_cursor", PRINTER_DATA));
            refCursArr.Add(new PairType<string, string>("r_multiple_printer_cursor", IPADDRESSPORT_DATA));
            DataSet outputDataSet;
            bool retVal = false;
            outputDataSet = null;
            try
            {
                retVal = dA.issueSqlStoredProcCommand("ccsowner", "pawn_generate_documents", "gen_doc_initial_details", inputParams, refCursArr, "o_return_code", "o_return_text", out outputDataSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling gen_doc_initial_details stored procedure", oEx);
                errorCode = "GenerateDocumentsSPCallFailed";
                errorMesg = "OracleException: " + oEx.Message;
            }

            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorMesg = dA.ErrorDescription;
            }
            else
            {
                try
                {
                    if (outputDataSet != null && outputDataSet.IsInitialized)
                    {
                        if (outputDataSet.Tables.Count > 0)
                        {
                            docdetails = outputDataSet.Tables[DOCUMENT_DATA];
                            printerdetails = outputDataSet.Tables[PRINTER_DATA];
                            ipportdetails = outputDataSet.Tables[IPADDRESSPORT_DATA];

                            var genDocFormVO = new GenDocFormVO(termName, formName, GlobalDataAccessor.Instance.CurrentSiteId.StoreId);
                            if (genDocFormVO.GetDataAndCompute(docdetails, printerdetails, ipportdetails))
                            {
                                return (genDocFormVO.ComputedHashTable);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    FileLogger.Instance.logMessage(LogLevel.FATAL, null, "Population of generate documents map failed: {0}: {1} ", ex.Message, ex.StackTrace);
                    BasicExceptionHandler.Instance.AddException("Population of generate documents map failed", ex);
                }
            }
            errorCode = "Success";
            errorMesg = "Success";
            return (null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputParams"></param>
        /// <param name="siteId"></param>
        /// <param name="docdetails"></param>
        /// <param name="printerdetails"></param>
        /// <param name="ipportdetails"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMesg"></param>
        /// <returns></returns>
        public static Hashtable GenerateDocumentsEssentialInformation(List<OracleProcParam> inputParams, out DataTable docdetails, out DataTable printerdetails, out DataTable ipportdetails, out string errorCode, out string errorMesg)
        {
            return (GenerateDocumentsEssentialInformation(inputParams,
                                                         false,
                                                         out docdetails,
                                                         out printerdetails,
                                                         out ipportdetails,
                                                         out errorCode,
                                                         out errorMesg));
        }


        public static bool ExecuteGetTagLongCode(
            string icnNumber,
            string storeNumber,
            out DataTable TagInformation,
            out string errorCode,
            out string errorText)
        {
            // Initialize the error code and text output values
            errorCode = string.Empty;
            errorText = string.Empty;
            Int32 int32StoreNumber;
            // Initialize the DataTable objects
            TagInformation = null;

            if (String.IsNullOrEmpty(storeNumber))
            {
                errorCode = "ExecuteGetTagLongCodeFailed";
                errorText = "Invalid store number";
                BasicExceptionHandler.Instance.AddException("ExecuteGetTagLongCodeFailed",
                                                            new ApplicationException("Cannot execute the Tag long code stored procedure"));
                return (false);
            }
            try
            {
                int32StoreNumber = Convert.ToInt32(storeNumber);
            }
            catch
            {
                errorCode = "ExecuteGetTagShortCodeFailed";
                errorText = "Invalid Store Number number";
                BasicExceptionHandler.Instance.AddException("ExecuteGetTagShortCodeFailed",
                                                            new ApplicationException("Cannot execute the Tag Short code stored procedure"));
                return (false);
            }
            if (icnNumber.Length < 18)
            {
                errorCode = "ExecuteGetTagLongCodeFailed";
                errorText = "Invalid icn number";
                BasicExceptionHandler.Instance.AddException("ExecuteGetTagLongCodeFailed",
                                                            new ApplicationException("Cannot execute the Tag long code stored procedure"));
                return (false);
            }
            try
            {
                long longIcnNumber = Convert.ToInt64(icnNumber);
            }
            catch
            {
                errorCode = "ExecuteGetTagLongCodeFailed";
                errorText = "Invalid icn number";
                BasicExceptionHandler.Instance.AddException("ExecuteGetTagLongCodeFailed",
                                                            new ApplicationException("Cannot execute the Tag long code stored procedure"));
                return (false);
            }

            // Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetTagLongCode Failed",
                                                            new ApplicationException("ExecuteGetTagLongCode Failed: Data accessor instance is invalid"));
                return (false);
            }

            // Get a DataAccessor
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            // Create an input parameter list
            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_icn", icnNumber));
            inParams.Add(new OracleProcParam("p_storenumber", storeNumber));

            // Setup ref cursor list
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            refCursors.Add(new PairType<string, string>("r_mdse", TAGINFORMATION));

            // Create data set
            DataSet outputDataSet;

            bool retVal = false;

            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "Pawn_Generate_Documents", "reprint_tags_longcode",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = " -- ExecuteGetTagLongCodeFailed";
                errorText = " --- Invocation of ExecuteGetTagLongCode stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking ExecuteGetTagLongCode stored proc",
                    oEx);
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
                    // Get cash drawer data if it exists
                    if (outputDataSet.Tables.Contains(TAGINFORMATION))
                    {
                        TagInformation = outputDataSet.Tables[TAGINFORMATION];
                    }
                    return (true);
                }
            }

            errorCode = "ExecuteGetTagLongCodeFailed";
            errorText = "Operation failed";
            return (false);
        }

        public static bool ExecuteGetTagShortCode(
            string icnNumber,
            string storeNumber,
            out DataTable TagInformation,
            out string errorCode,
            out string errorText)
        {
            // Initialize the error code and text output values
            errorCode = string.Empty;
            errorText = string.Empty;
            Int32 int32StoreNumber;
            // Initialize the DataTable objects
            TagInformation = null;

            if (String.IsNullOrEmpty(storeNumber))
            {
                errorCode = "ExecuteGetTagShortCodeFailed";
                errorText = "Invalid store number";
                BasicExceptionHandler.Instance.AddException("ExecuteGetTagShortCodeFailed",
                                                            new ApplicationException("Cannot execute the Tag Short code stored procedure"));
                return (false);
            }
            try
            {
                int32StoreNumber = Convert.ToInt32(storeNumber);
            }
            catch
            {
                errorCode = "ExecuteGetTagShortCodeFailed";
                errorText = "Invalid Store Number number";
                BasicExceptionHandler.Instance.AddException("ExecuteGetTagShortCodeFailed",
                                                            new ApplicationException("Cannot execute the Tag Short code stored procedure"));
                return (false);
            }
            if (icnNumber.Length < 8)
            {
                errorCode = "ExecuteGetTagShortCodeFailed";
                errorText = "Invalid icn number";
                BasicExceptionHandler.Instance.AddException("ExecuteGetTagShortCodeFailed",
                                                            new ApplicationException("Cannot execute the Tag Short code stored procedure"));
                return (false);
            }
            // Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetTagShortCode Failed",
                                                            new ApplicationException("ExecuteGetTagShortCode Failed: Data accessor instance is invalid"));
                return (false);
            }

            // Get a DataAccessor
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            // Create an input parameter list
            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_storenumber", int32StoreNumber));
            inParams.Add(new OracleProcParam("p_icn", icnNumber));

            // Setup ref cursor list
            List<PairType<string, string>> refCursors = new List<PairType<string, string>>();
            refCursors.Add(new PairType<string, string>("r_mdse", TAGINFORMATION));

            // Create data set
            DataSet outputDataSet;

            bool retVal = false;

            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "Pawn_Generate_Documents", "reprint_tags_Shortcode",
                    inParams, refCursors, "o_return_code", "o_return_text",
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                errorCode = " -- ExecuteGetTagShortCodeFailed";
                errorText = " --- Invocation of ExecuteGetTagShortCode stored proc failed";
                BasicExceptionHandler.Instance.AddException(
                    "OracleException thrown when invoking ExecuteGetTagShortCode stored proc",
                    oEx);
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
                    // Get cash drawer data if it exists
                    if (outputDataSet.Tables.Contains(TAGINFORMATION))
                    {
                        TagInformation = outputDataSet.Tables[TAGINFORMATION];
                    }
                    return (true);
                }
            }

            errorCode = "ExecuteGetTagShortCodeFailed";
            errorText = "Operation failed";
            return (false);
        }
    }
}
