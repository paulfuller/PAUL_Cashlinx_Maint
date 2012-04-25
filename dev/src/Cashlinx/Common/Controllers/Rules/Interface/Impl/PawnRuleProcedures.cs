using System;
using System.Collections.Generic;
using System.Data;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.String;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Rules.Interface.Impl
{
    public static class PawnRuleProcedures
    {
        #region Static Fields
        public static readonly string PROCSCHEMA = "ccsowner";
        public static readonly string PROCPACKAGE = "pawn_procs";

        public static readonly string BUSINESSRULECOMPONENTS = "BusinessRuleComponents";
        public static readonly string BUSINESSRULECOMPPROC = "get_business_rule_components";

        public static readonly string ALIASES = "Aliases";
        public static readonly string ALIASESPROC = "get_aliases";

        public static readonly string PARAMETER = "Parameter";
        public static readonly string PARAMETERPROC = "get_parameter";
        
        public static readonly string PARAMETERS = "Parameters";
        public static readonly string PARAMETERSPROC = "get_parameters";

        public static readonly string FEES = "Fees";
        public static readonly string FEESPROC = "get_fees";

        public static readonly string INTEREST = "Interest";
        public static readonly string INTERESTPROC = "get_interest";

        public static readonly string RETURNCODE = "p_return_code";
        public static readonly string RETURNDESC = "p_return_value";

        public static readonly string ALIAS_SEP_STR = "|";
        #endregion

        #region Private Convenience Methods
        /// <summary>
        /// Convenience method for executing a stored proc with
        /// the OracleDataAccessor and preparing any necessary
        /// error messages
        /// </summary>
        /// <param name="oraDA"></param>
        /// <param name="procName"></param>
        /// <param name="oraParams"></param>
        /// <param name="refCursArr"></param>
        /// <param name="errCodeParamName"></param>
        /// <param name="errCodeDescName"></param>
        /// <param name="outputDataSet"></param>
        /// <param name="returnCode"></param>
        /// <param name="returnDesc"></param>
        /// <returns></returns>
        private static bool executeStoredProc(
            OracleDataAccessor oraDA,
            string procName,
            ref List<OracleProcParam> oraParams,
            ref List<PairType<string, string>> refCursArr,
            string errCodeParamName,
            string errCodeDescName,
            out DataSet outputDataSet,
            out string returnCode, 
            out string returnDesc)
        {
            outputDataSet = null;
            returnCode = string.Empty;
            returnDesc = string.Empty;
            bool callSuccess = false;
            try
            {
                callSuccess = oraDA.issueSqlStoredProcCommand(
                    PROCSCHEMA,
                    PROCPACKAGE,
                    procName,
                    oraParams, refCursArr,
                    errCodeParamName,
                    errCodeDescName,
                    out outputDataSet);
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException(procName + " cannot be executed", new ApplicationException("OracleException", oEx));
                return (false);
            }
            //Set outgoing return code and error description
            returnCode = oraDA.ErrorCode;
            returnDesc = oraDA.ErrorDescription;

            //Ensure data set is valid before determining return value
            if (callSuccess)
            {
                if (CollectionUtilities.isNotEmpty(refCursArr) &&
                    outputDataSet != null &&
                    outputDataSet.IsInitialized &&
                    outputDataSet.Tables.Count > 0)
                {
                    return (true);
                }
                else if (CollectionUtilities.isEmpty(refCursArr))
                {
                    return (true);
                }
            }
            return (false);
        }

        /// <summary>
        /// Convenience method for validating a variable parameter list of
        /// strings and preparing any necessary error messages
        /// </summary>
        /// <param name="context"></param>
        /// <param name="returnCode"></param>
        /// <param name="returnDesc"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static bool validateStringInputs(
            string context, 
            out string returnCode, 
            out string returnDesc, 
            params string[] args)
        {
            returnCode = string.Empty;
            returnDesc = string.Empty;

            foreach (string s in args)
            {
                if (string.IsNullOrEmpty(s))
                {
                    BasicExceptionHandler.Instance.AddException(context + ": Input(s) => (null or empty)",
                        new ApplicationException("PawnRulesSystem: " + context + ": Input(s) => null or empty"));
                    returnCode = context + ": Failed";
                    returnDesc = context + ": Input(s) => null or empty";
                    
                    return (false);
                }
            }
            return (true);
        }

        /// <summary>
        /// Convenience method for validating a list of strings and
        /// for preparing any associated error messages
        /// </summary>
        /// <param name="context"></param>
        /// <param name="strings"></param>
        /// <param name="returnCode"></param>
        /// <param name="returnDesc"></param>
        /// <returns></returns>
        private static bool validateStringList(
            string context,
            List<string> strings, 
            out string returnCode,
            out string returnDesc)
        {
            returnCode = string.Empty;
            returnDesc = string.Empty;

            //Verify list contains at least one parameter
            bool isValid = true;
            if (CollectionUtilities.isNotEmpty(strings))
            {
                //Loop through string list
                foreach (string s in strings)
                {
                    if (string.IsNullOrEmpty(s))
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            if (!isValid)
            {
                BasicExceptionHandler.Instance.AddException(context + ": Input(s) => (null or empty)",
                    new ApplicationException("PawnRulesSystem: " + context + ": Input(s) => null or empty"));
                returnCode = context + ": Failed";
                returnDesc = context + ": Input(s) => null or empty";
                return (false);
            }
            return (true);
        }

        /// <summary>
        /// Convenience method for validating the data accessor class and setting up 
        /// any necessary error messages
        /// </summary>
        /// <param name="context"></param>
        /// <param name="oraDA"></param>
        /// <param name="returnCode"></param>
        /// <param name="returnDesc"></param>
        /// <returns></returns>
        private static bool validateDataAccessor(
            string context, 
            OracleDataAccessor oraDA,
            out string returnCode,
            out string returnDesc)
        {
            returnCode = string.Empty;
            returnDesc = string.Empty;
            if (oraDA == null)
            {
                BasicExceptionHandler.Instance.AddException(context + ": Data accessor instance is invalid",
                    new SystemException("PawnRulesSystem: " + context + ": Data accessor is not available or is null"));
                returnCode = context + ": Failed";
                returnDesc = context + ": Data accessor instance is invalid";
                return (false);
            }
            return (true);
        }
        #endregion


        #region Stored Procedure Wrappers
        /*
         * PROCEDURE get_business_rule_components (
         *                              p_business_rule_code  IN VARCHAR2,
                                        p_date IN DATE,
                                        p_business_components OUT bus_component_ref_cursor,
                                        p_return_code OUT NUMBER,
                                        p_return_value OUT VARCHAR2);
         * 
         */
        /// <summary>
        /// get_business_rule_components SP Wrapper
        /// </summary>
        /// <param name="oraDA"></param>
        /// <param name="busRuleCode"></param>
        /// <param name="date"></param>
        /// <param name="busComponents"></param>
        /// <param name="returnCode"></param>
        /// <param name="returnDesc"></param>
        /// <returns></returns>
        public static bool GetBusinessRuleComponents(
            OracleDataAccessor oraDA,
            string busRuleCode,
            DateTime date,
            out DataTable busComponents,
            out string returnCode,
            out string returnDesc) 
        {
            //Initialize outputs
            busComponents = null;
            returnCode = string.Empty;
            returnDesc = string.Empty;

            //Verify inputs
            if (!validateStringInputs(BUSINESSRULECOMPONENTS, out returnCode, out returnDesc, busRuleCode))
            {
                return (false);
            }

            //Verify that the data accessor is available
            if (!validateDataAccessor(BUSINESSRULECOMPONENTS, oraDA, out returnCode, out returnDesc))
            {
                return (false);
            }

            //Create input parameters
            List<OracleProcParam> oraParams = new List<OracleProcParam>();

            //Pass in the business rule code
            oraParams.Add(new OracleProcParam("p_business_rule_code", busRuleCode));

            //Pass in the date
            oraParams.Add(new OracleProcParam("p_date", date));

            //Create ref cursor string list 
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("p_business_components", BUSINESSRULECOMPONENTS));

            //Invoke the data accessor
            DataSet outputDataSet;
            if (executeStoredProc(oraDA, BUSINESSRULECOMPPROC, ref oraParams, ref refCursArr,
                RETURNCODE, RETURNDESC, out outputDataSet,
                out returnCode, out returnDesc))
            {
                //Retrieve ref cursor output
                busComponents = outputDataSet.Tables[BUSINESSRULECOMPONENTS];
                return (true);
            }

            return (false); 
        }

        /*
         * PROCEDURE get_aliases (
         *             p_alias  IN VARCHAR2,
                       p_company IN VARCHAR2,
                       p_store IN VARCHAR2,
                       p_state IN VARCHAR2,
                       p_date IN DATE,
                       p_alias_record OUT alias_ref_cursor,
                       p_return_code OUT NUMBER,
                       p_return_value OUT VARCHAR2); 
         
         * 
         */
        /// <summary>
        /// get_aliases SP Wrapper
        /// </summary>
        /// <param name="oraDA"></param>
        /// <param name="alias"></param>
        /// <param name="company"></param>
        /// <param name="store"></param>
        /// <param name="state"></param>
        /// <param name="date"></param>
        /// <param name="aliases"></param>
        /// <param name="returnCode"></param>
        /// <param name="returnDesc"></param>
        /// <returns></returns>
        public static bool GetAliases(
            OracleDataAccessor oraDA,
            string alias,
            string company,
            string store,
            string state,
            DateTime date,
            out DataTable aliases,
            out string returnCode,
            out string returnDesc) 
        {
            //Initialize outputs
            aliases = null;
            returnCode = string.Empty;
            returnDesc = string.Empty;

            //Validate inputs
            if (!validateStringInputs(ALIASES, out returnCode, out returnDesc, 
                alias, company, store, state))
            {
                return (false);
            }

            //Validate data accessor
            if (!validateDataAccessor(ALIASES, oraDA, out returnCode, out returnDesc))
            {
                return (false);
            }

            //Create input parameters
            List<OracleProcParam> oraParams = new List<OracleProcParam>();

            //Pass in the specific alias to lookup
            oraParams.Add(new OracleProcParam("p_alias", alias));

            //Pass in the business unit (the company)
            oraParams.Add(new OracleProcParam("p_company", company));

            //Pass in the store number
            oraParams.Add(new OracleProcParam("p_store", store));

            //Pass in the state
            oraParams.Add(new OracleProcParam("p_state", state));

            //Pass in the date
            oraParams.Add(new OracleProcParam("p_date", date));

            //Create ref cursor string list 
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("p_alias_record", ALIASES));

            //Invoke the data accessor
            DataSet outputDataSet;
            if (executeStoredProc(oraDA, ALIASESPROC, ref oraParams, ref refCursArr,
                RETURNCODE, RETURNDESC, out outputDataSet,
                out returnCode, out returnDesc))
            {
                aliases = outputDataSet.Tables[ALIASES];
                return (true);
            }

            return (false); 
        }

        /*
         * 
         *PROCEDURE get_parameter (
         *               p_parameter  IN VARCHAR2,
                         p_alias IN VARCHAR2,
                         p_date IN DATE,
                         p_param OUT VARCHAR2,
                         p_return_code OUT NUMBER,
                         p_return_value OUT VARCHAR2);  
         * 
         */
        /// <summary>
        /// get_parameter SP Wrapper
        /// </summary>
        /// <param name="oraDA"></param>
        /// <param name="paramName"></param>
        /// <param name="alias"></param>
        /// <param name="date"></param>
        /// <param name="paramOutput"></param>
        /// <param name="returnCode"></param>
        /// <param name="returnDesc"></param>
        /// <returns></returns>
        public static bool GetParameter(
            OracleDataAccessor oraDA,
            string paramName,
            string alias,
            DateTime date,
            out string paramOutput,
            out string returnCode,
            out string returnDesc) 
        {
            //Initialize outputs
            paramOutput = null;
            returnCode = null;
            returnDesc = null;

            //Validate inputs
            if (!validateStringInputs(PARAMETER, out returnCode, out returnDesc, paramName, alias))
            {
                return (false);
            }

            //Validate data accessor
            if (!validateDataAccessor(PARAMETER, oraDA, out returnCode, out returnDesc))
            {
                return (false);
            }

            //Create parameter list
            List<OracleProcParam> oraParams = new List<OracleProcParam>();

            //Create output params
            oraParams.Add(new OracleProcParam("p_param", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 500));

            //Create input params
            oraParams.Add(new OracleProcParam("p_parameter", paramName));
            oraParams.Add(new OracleProcParam("p_alias", alias));
            oraParams.Add(new OracleProcParam("p_date", date));

            //Create empty ref cursor array
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            DataSet outputDataSet;

            if (executeStoredProc(
                oraDA, PARAMETERPROC, ref oraParams, ref refCursArr,
                RETURNCODE, RETURNDESC,
                out outputDataSet, out returnCode, out returnDesc))
            {
                DataTable outputTable = outputDataSet.Tables["OUTPUT"];
                if (outputTable != null && outputTable.IsInitialized)
                {
                    DataRow dataRow = outputTable.Rows[0];
                    if (dataRow != null && 
                        dataRow.ItemArray != null && dataRow.ItemArray.Length > 0)
                    {
                        string paramNmVar = (string)dataRow["NAME"];
                        if (!string.IsNullOrEmpty(paramNmVar) && paramNmVar.Equals("p_param"))
                        {
                            object paramObjOutput = dataRow["VALUE"];
                            paramOutput = (string)(paramObjOutput == null ? string.Empty : paramObjOutput.ToString());
                            return (true);
                        }
                    }
                }
            }
            return (false); 
        }

        /*
         * 
         * PROCEDURE get_parameters (
         *               p_parameter  IN VARCHAR2,
                         p_alias_string IN VARCHAR2,
                         p_date IN DATE,
                         p_param_list OUT param_ref_cursor,
                         p_return_code OUT NUMBER,
                         p_return_value OUT VARCHAR2); 
         */
        /// <summary>
        /// get_parameters SP Wrapper
        /// </summary>
        /// <param name="oraDA"></param>
        /// <param name="paramName"></param>
        /// <param name="aliases"></param>
        /// <param name="date"></param>
        /// <param name="paramData"></param>
        /// <param name="returnCode"></param>
        /// <param name="returnDesc"></param>
        /// <returns></returns>
        public static bool GetParameters(
            OracleDataAccessor oraDA,
            string paramName, 
            List<string> aliases,
            DateTime date,
            out DataTable paramData, 
            out string returnCode,
            out string returnDesc) 
        {
            //Initialize outputs
            paramData = null;
            returnCode = string.Empty;
            returnDesc = string.Empty;

            //Validate inputs
            if (validateStringInputs(PARAMETERS, out returnCode, out returnDesc, paramName))
            {
                //Validate aliases
                if (!validateStringList(PARAMETERS, aliases, out returnCode, out returnDesc))
                {
                    return (false);
                }
            }
            else
            {
                return (false);
            }

            //Validate data accessor
            if (!validateDataAccessor(PARAMETERS, oraDA, out returnCode, out returnDesc))
            {
                return (false);
            }

            //Create joint aliases input string
            String aliasJoin = StringUtilities.joinListIntoString(aliases, ALIAS_SEP_STR);
            
            //Create parameter list
            List<OracleProcParam> oraParams = new List<OracleProcParam>();

            //Add parameter specifier
            oraParams.Add(new OracleProcParam("p_parameter", paramName));

            //Add alias joint string
            oraParams.Add(new OracleProcParam("p_alias_string", aliasJoin));

            //Add date
            oraParams.Add(new OracleProcParam("p_date", date));

            //Create ref cursor list
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("p_param_list", PARAMETERS));            
            DataSet outputDataSet;

            //Execute stored proc
            if (executeStoredProc(
                oraDA, PARAMETERSPROC, ref oraParams,
                ref refCursArr, RETURNCODE, RETURNDESC,
                out outputDataSet, out returnCode, out returnDesc))
            {
                //Get data table
                paramData = outputDataSet.Tables[PARAMETERS];
                return (true);
            }
            return (false); 
        }

        /*
         * PROCEDURE get_fees ( 
         *           p_fee_type IN VARCHAR2,
                     p_param_key_code IN VARCHAR2,
                     p_alias_list IN VARCHAR2,
                     p_date IN DATE,
                     p_fee_cursor OUT fee_ref_cursor,
                     p_return_code OUT NUMBER,
                     p_return_value OUT VARCHAR2);
         */
        /// <summary>
        /// get_fees SP Wrapper
        /// </summary>
        /// <param name="oraDA"></param>
        /// <param name="feeType"></param>
        /// <param name="paramKey"></param>
        /// <param name="aliases"></param>
        /// <param name="date"></param>
        /// <param name="feeData"></param>
        /// <param name="returnCode"></param>
        /// <param name="returnDesc"></param>
        /// <returns></returns>
        public static bool GetFees(
            OracleDataAccessor oraDA,
            string feeType,
            string paramKey,
            List<string> aliases,
            DateTime date,
            out DataTable feeData,
            out string returnCode,
            out string returnDesc) 
        {
            //Initialize outputs
            feeData = null;
            returnCode = string.Empty;
            returnDesc = string.Empty;

            //Validate inputs
            if (validateStringInputs(FEES, out returnCode, out returnDesc, feeType, paramKey))
            {
                //Validate alias list
                if (!validateStringList(FEES, aliases, out returnCode, out returnDesc))
                {
                    return (false);
                }
            }
            else
            {
                return (false);
            }

            //Validate data accessor
            if (!validateDataAccessor(FEES, oraDA, out returnCode, out returnDesc))
            {
                return (false);
            }

            //Create joint aliases input string
            String aliasJoin = StringUtilities.joinListIntoString(aliases, ALIAS_SEP_STR);

            //Create parameter list
            List<OracleProcParam> oraParams = new List<OracleProcParam>();

            //Add fee type
            oraParams.Add(new OracleProcParam("p_fee_type", feeType));

            //Add parameter key code
            oraParams.Add(new OracleProcParam("p_param_key_code", paramKey));

            //Add alias joint string
            oraParams.Add(new OracleProcParam("p_alias_string", aliasJoin));

            //Add date
            oraParams.Add(new OracleProcParam("p_date", date));

            //Create ref cursor list
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("p_fee_cursor", FEES));
            DataSet outputDataSet;

            //Execute stored proc
            if (executeStoredProc(
                oraDA, FEESPROC, ref oraParams,
                ref refCursArr, RETURNCODE, RETURNDESC,
                out outputDataSet, out returnCode, out returnDesc))
            {
                //Get data table
                feeData = outputDataSet.Tables[FEES];
                return (true);
            }
            return (false);
        }

        /*
         * PROCEDURE get_interest ( p_alias_list IN VARCHAR2,
                         p_date IN DATE,
                         p_interest_list OUT interest_ref_cursor,
                         p_return_code OUT NUMBER,
                         p_return_value OUT VARCHAR2);  */
        /// <summary>
        /// get_interest SP Wrapper
        /// </summary>
        /// <param name="oraDA"></param>
        /// <param name="aliases"></param>
        /// <param name="date"></param>
        /// <param name="interestData"></param>
        /// <param name="returnCode"></param>
        /// <param name="returnDesc"></param>
        /// <returns></returns>
        public static bool GetInterest(
            OracleDataAccessor oraDA, 
            List<string> aliases, 
            DateTime date,
            out DataTable interestData, 
            out string returnCode,
            out string returnDesc)
        {
            //Initialize outputs
            interestData = null;
            returnCode = string.Empty;
            returnDesc = string.Empty;

            //Validate alias list
            if (!validateStringList(INTEREST, aliases, out returnCode, out returnDesc))
            {
                return (false);
            }

            //Validate data accessor
            if (!validateDataAccessor(INTEREST, oraDA, out returnCode, out returnDesc))
            {
                return (false);
            }

            //Create joint aliases input string
            String aliasJoin = StringUtilities.joinListIntoString(aliases, ALIAS_SEP_STR);

            //Create parameter list
            List<OracleProcParam> oraParams = new List<OracleProcParam>();

            //Add alias joint string
            oraParams.Add(new OracleProcParam("p_alias_list", aliasJoin));

            //Add date parameter
            oraParams.Add(new OracleProcParam("p_date", date));

            //Create ref cursor list
            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("p_interest_list", INTEREST));
            DataSet outputDataSet;

            //Execute stored proc
            if (executeStoredProc(
                oraDA, INTERESTPROC, ref oraParams,
                ref refCursArr, RETURNCODE, RETURNDESC,
                out outputDataSet, out returnCode, out returnDesc))
            {
                //Get data table
                interestData = outputDataSet.Tables[INTEREST];
                return (true);
            }
            return (false);
        }
        #endregion
    }
}
