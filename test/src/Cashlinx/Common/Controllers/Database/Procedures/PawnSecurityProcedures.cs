using System;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database.Procedures
{
    public static class PawnSecurityProcedures
    {
        /// <summary>
        /// Retrieves all security info for a client
        /// </summary>
        /// <param name="dataAccessor"></param>
        /// <param name="key"></param>
        /// <param name="ipAddress"></param>
        /// <param name="machineName"></param>
        /// <param name="macAddress"></param>
        /// <param name="clientHash"></param>
        /// <param name="application"></param>
        /// <param name="clientData"></param>
        /// <param name="esbData"></param>
        /// <param name="dbData"></param>
        /// <param name="macList"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteGetClientConfiguration(
            OracleDataAccessor dataAccessor,
            string key,
            string ipAddress,
            string machineName,
            string macAddress,
            string clientHash,
            PawnSecApplication application,
            out DataTable clientData,
            out DataTable esbData,
            out DataTable dbData,
            out DataTable macList,
            out string errorCode,
            out string errorText)
        {
            // initialize error output variables
            errorCode = String.Empty;
            errorText = String.Empty;

            // initialize data table output variables
            clientData = null;
            esbData = null;
            dbData = null;
            macList = null;

            if (string.IsNullOrEmpty(clientHash))
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetClientConfiguration Failed",
                    new ApplicationException("ExecuteGetClientConfiguration Failed: Invalid hash"));
                return (false);
            }

            // Check that there is at least one non-null, non-empty input parameter
            if (String.IsNullOrEmpty(ipAddress) && String.IsNullOrEmpty(machineName) && String.IsNullOrEmpty(macAddress))
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetClientConfiguration Failed",
                    new ApplicationException("ExecuteGetClientConfiguration Failed: No valid inputs"));
                return (false);
            }

            // Ensure the data accessor is valid
            if (dataAccessor == null || !dataAccessor.Initialized)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetClientConfiguration Failed",
                    new ApplicationException("ExecuteGetClientConfiguration Failed: Data accessor instance is invalid"));
                return (false);
            }

            // Set up input variables
            var inParams = new List<OracleProcParam>
                                             {
                                                 new OracleProcParam("p_ip_address", ipAddress),
                                                 new OracleProcParam("p_machinename", machineName),
                                                 new OracleProcParam("p_macaddress", macAddress),
                                                 new OracleProcParam("p_verchk", clientHash),
                                                 new OracleProcParam("p_verchk_id", PawnSecApplicationResolver.Resolve(application))
                                             };

            // Set up output
            DataSet outputDataSet;
            bool retVal;
            var refCursArr = new List<PairType<string, string>>
            {
                new PairType<string, string>("o_client_data", "client_data"),
                new PairType<string, string>("o_esb_data", "esb_data"),
                new PairType<string, string>("o_dbs_data", "dbs_data"),
                new PairType<string, string>("o_machine_list", "mac_data")
            };

            try
            {
                retVal = dataAccessor.issueSqlStoredProcCommand(
                    "", "pawnsec",
                    "get_client_info", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", key, out outputDataSet);
                errorCode = dataAccessor.ErrorCode;
                errorText = dataAccessor.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetClientConfiguration Failed", oEx);
                errorCode = " --- ExecuteGetClientConfigurationFailed";
                errorText = " --- OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetClientConfiguration Failed: return value is false", new ApplicationException());
                errorCode = dataAccessor.ErrorCode + " -- ExecuteGetClientConfigurationFailed";
                errorText = dataAccessor.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null || !outputDataSet.IsInitialized ||
               (outputDataSet.Tables == null || outputDataSet.Tables.Count <= 0))
            {
                return (false);
            }

            clientData = outputDataSet.Tables["client_data"];
            esbData = outputDataSet.Tables["esb_data"];
            dbData = outputDataSet.Tables["dbs_data"];
            macList = outputDataSet.Tables["mac_data"];

            // method completed successfully
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataAccessor"></param>
        /// <param name="key"></param>
        /// <param name="ipAddress"></param>
        /// <param name="machineName"></param>
        /// <param name="macAddress"></param>
        /// <param name="isAllowed"></param>
        /// <param name="isConnected"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteUpdateClientInfo(
            OracleDataAccessor dataAccessor,
            string key,
            string ipAddress,
            string machineName,
            string macAddress,
            string isAllowed,
            string isConnected,
            out string errorCode,
            out string errorText)
        {
            // initialize error output variables
            errorCode = String.Empty;
            errorText = String.Empty;

            // Check that there is at least one non-null, non-empty input parameter
            if (String.IsNullOrEmpty(ipAddress) && String.IsNullOrEmpty(machineName) && String.IsNullOrEmpty(macAddress))
            {
                BasicExceptionHandler.Instance.AddException(
                    "ExecuteUpdateClientInfo Failed",
                    new ApplicationException("ExecuteUpdateClientInfo Failed: No valid inputs"));
                return (false);
            }

            // Ensure the data accessor is valid
            if (dataAccessor == null || !dataAccessor.Initialized)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateClientInfo Failed",
                    new ApplicationException("ExecuteUpdateClientInfo Failed: Data accessor instance is invalid"));
                return (false);
            }

            // Set up input variables
            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_ip_address", ipAddress));
            inParams.Add(new OracleProcParam("p_machinename", machineName));
            inParams.Add(new OracleProcParam("p_macaddress", macAddress));
            inParams.Add(new OracleProcParam("p_isallowed", isAllowed));
            inParams.Add(new OracleProcParam("p_isconnected", isConnected));

            // Set up output
            DataSet outputDataSet;
            bool retVal;

            try
            {
                retVal = dataAccessor.issueSqlStoredProcCommand(
                    "", "pawnsec",
                    "update_client_info", inParams, null,
                    "o_return_code",
                    "o_return_text", key, out outputDataSet);
                errorCode = dataAccessor.ErrorCode;
                errorText = dataAccessor.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateClientInfo Failed", oEx);
                errorCode = " --- ExecuteUpdateClientInfoFailed";
                errorText = " --- OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateClientInfo Failed: return value is false", new ApplicationException());
                errorCode = dataAccessor.ErrorCode + " -- ExecuteUpdateClientInfoFailed";
                errorText = dataAccessor.ErrorDescription + " -- Return value is false";
                return (false);
            }

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataAccessor"></param>
        /// <param name="name"></param>
        /// <param name="isEnabled"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteUpdateEsbInfo(
            OracleDataAccessor dataAccessor,
            string key,
            string name,
            string isEnabled,
            out string errorCode,
            out string errorText)
        {
            // initialize error output variables
            errorCode = String.Empty;
            errorText = String.Empty;

            // Check input
            if (String.IsNullOrEmpty(name))
            {
                BasicExceptionHandler.Instance.AddException(
                    "ExecuteUpdateEsbInfo Failed",
                    new ApplicationException("ExecuteUpdateEsbInfo Failed: No valid inputs"));
                return (false);
            }

            // Ensure the data accessor is valid
            if (dataAccessor == null || !dataAccessor.Initialized)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateEsbInfo Failed",
                    new ApplicationException("ExecuteUpdateEsbInfo Failed: Data accessor instance is invalid"));
                return (false);
            }

            // Set up input variables
            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_esbservicename", name));
            inParams.Add(new OracleProcParam("p_enabledflag", isEnabled));

            // Set up output
            DataSet outputDataSet;
            bool retVal;

            try
            {
                retVal = dataAccessor.issueSqlStoredProcCommand(
                    "", "pawnsec",
                    "update_esb_info", inParams, null,
                    "o_return_code",
                    "o_return_text", key, out outputDataSet);
                errorCode = dataAccessor.ErrorCode;
                errorText = dataAccessor.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateEsbInfo Failed", oEx);
                errorCode = " --- ExecuteUpdateEsbInfoFailed";
                errorText = " --- OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateEsbInfo Failed: return value is false", new ApplicationException());
                errorCode = dataAccessor.ErrorCode + " -- ExecuteUpdateEsbInfoFailed";
                errorText = dataAccessor.ErrorDescription + " -- Return value is false";
                return (false);
            }

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isEnabled"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteUpdateDatabaseInfo(
            OracleDataAccessor dataAccessor,
            string key,
            string name,
            string isEnabled,
            out string errorCode,
            out string errorText)
        {
            // initialize error output variables
            errorCode = String.Empty;
            errorText = String.Empty;

            // Check input
            if (String.IsNullOrEmpty(name))
            {
                BasicExceptionHandler.Instance.AddException(
                    "ExecuteUpdateDatabaseInfo Failed",
                    new ApplicationException("ExecuteUpdateDatabaseInfo Failed: No valid inputs"));
                return (false);
            }

            // Ensure the data accessor is valid
            if (dataAccessor == null || !dataAccessor.Initialized)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateDatabaseInfo Failed",
                    new ApplicationException("ExecuteUpdateDatabaseInfo Failed: Data accessor instance is invalid"));
                return (false);
            }

            // Set up input variables
            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_dbservicename", name));
            inParams.Add(new OracleProcParam("p_enabledflag", isEnabled));

            // Set up output
            DataSet outputDataSet;
            bool retVal;

            try
            {
                retVal = dataAccessor.issueSqlStoredProcCommand(
                    "", "pawnsec",
                    "update_dbs_info", inParams, null,
                    "o_return_code",
                    "o_return_text", key, out outputDataSet);
                errorCode = dataAccessor.ErrorCode;
                errorText = dataAccessor.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateDatabaseInfo Failed", oEx);
                errorCode = " --- ExecuteUpdateDatabaseInfoFailed";
                errorText = " --- OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteUpdateDatabaseInfo Failed: return value is false", new ApplicationException());
                errorCode = dataAccessor.ErrorCode + " -- ExecuteUpdateDatabaseInfoFailed";
                errorText = dataAccessor.ErrorDescription + " -- Return value is false";
                return (false);
            }

            return (true);
        }
    }
}
