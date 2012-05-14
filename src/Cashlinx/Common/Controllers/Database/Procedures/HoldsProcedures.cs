/**************************************************************************************************************
* CashlinxDesktop.DesktopProcedures
* HoldsProcedures
* Sreelatha Rengarajan 8/5/2009 Initial version
* EDW 1-9-2012 CR#15278
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database.Procedures
{
    public static class HoldsProcedures
    {
        private static readonly string CUSTOMER_HOLDS_LIST = "CustomerHolds";
        private static readonly string MERCHANDISE_LIST = "MerchandiseList";

        public static readonly string PAWN_LOAN = "o_pawn_loan_m";
        public static readonly string PAWN_MDSE_LIST = "o_pawn_mdselist_m";
        public static readonly string PAWN_MDHIST_LIST = "o_pawn_mdhistlist_m";
        public static readonly string PAWN_GUN_LIST = "o_pawn_gunlist_m";
        public static readonly string PAWN_OTHERDSC_LIST = "o_pawn_otherdsclist_m";
        public static readonly string POLICE_HOLD_DATA = "o_police_data";
        public static readonly string RELEASE_AUTHORIZATION_DATA = "o_fingerprint_auth";
        public static bool ExecuteGetHolds(
            string storeNumber,
            string customerNumber,
            string holdType,
            out DataTable customerHolds,
            out DataTable merchandiseList,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Set default output params
            customerHolds = null;
            merchandiseList = null;
            errorCode = string.Empty;
            errorMesg = string.Empty;

            if (GlobalDataAccessor.Instance == null ||
                !GlobalDataAccessor.Instance.IsDataAccessorValid())
            {
                customerHolds = null;
                merchandiseList = null;
                errorCode = "GetCustomerHoldsFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_customer_number", customerNumber));

            inParams.Add(new OracleProcParam("p_hold_info_needed", holdType));

            bool rt = false;
            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_cust_holds_list", CUSTOMER_HOLDS_LIST));
                refCursArr.Add(new PairType<string, string>("r_mdse_holds_list", MERCHANDISE_LIST));
                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_manage_holds",
                        "get_holds", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling get_customer_holds stored procedure", oEx);
                    errorCode = "GetCustomerHolds";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
                else
                {
                    if (outputDataSet != null && outputDataSet.IsInitialized)
                    {
                        if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                        {
                            customerHolds = outputDataSet.Tables[CUSTOMER_HOLDS_LIST];
                            merchandiseList = outputDataSet.Tables[MERCHANDISE_LIST];
                            return (true);
                        }
                    }
                }

                errorCode = "GetCustomerHoldsFail";
                errorMesg = "Operation failed";
                return (false);
            }
            else
            {
                errorCode = "GetCustomerHoldsFail";
                errorMesg = "No valid input parameters given";
            }
            return (rt);
        }

        private static bool ExecuteMaintainHolds(
            string holdType,
            string customerNumber,
            List<string> mdseICN,
            string storeNumber,
            string userId,
            string holdComments,
            string[] refType,
            string[] refNumber,
            string[] releaseDate,
            string[] policeInfo,
            string releaseFlag,
            string[] jCase,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Set default output params
            errorCode = string.Empty;
            errorMesg = string.Empty;

            if (GlobalDataAccessor.Instance == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "MaintainHoldsFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_hold_type", holdType));

            inParams.Add(new OracleProcParam("p_customer_number", customerNumber));

            inParams.Add(new OracleProcParam("p_mdse_icn", true, mdseICN));

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_user_id", userId));

            inParams.Add(new OracleProcParam("p_hold_comment", holdComments));

            OracleProcParam orpmRefType = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_ref_type", refType.Length);
            for (int i = 0; i < refType.Length; i++)
            {
                orpmRefType.AddValue(refType[i]);
            }

            inParams.Add(orpmRefType);

            OracleProcParam orpmrefNumber = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_ref_number", refNumber.Length);
            for (int i = 0; i < refNumber.Length; i++)
            {
                orpmrefNumber.AddValue(refNumber[i]);
            }

            inParams.Add(orpmrefNumber);

            OracleProcParam orpmrelDate = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_release_date", releaseDate.Length);
            for (int i = 0; i < releaseDate.Length; i++)
            {
                orpmrelDate.AddValue(releaseDate[i]);
            }

            inParams.Add(orpmrelDate);

            OracleProcParam orpmHoldInfo = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_police_info_array", policeInfo.Length);
            for (int i = 0; i < policeInfo.Length; i++)
            {
                orpmHoldInfo.AddValue(policeInfo[i]);
            }

            inParams.Add(orpmHoldInfo);

            inParams.Add(new OracleProcParam("p_release_flag", releaseFlag));

            OracleProcParam orpmjCase = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_jewelry_case", jCase.Length);
            for (int i = 0; i < jCase.Length; i++)
            {
                orpmjCase.AddValue(jCase[i]);
            }

            inParams.Add(orpmjCase);

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_manage_holds",
                        "maintain_holds", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling maintain_holds stored procedure", oEx);
                    errorCode = "MaintainHolds";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
                else
                {
                    return (true);
                }
            }
            else
            {
                errorCode = "MaintainHoldsFail";
                errorMesg = "No valid input parameters given";
                return (false);
            }
        }

        /// <summary>
        /// Call to add holds on selected transactions
        /// </summary>
        /// <param name="customerHolds"></param>
        /// <param name="holdComment"></param>
        /// <param name="releaseDate"></param>
        /// <returns></returns>
        public static bool AddCustomerHolds(List<HoldData> customerHolds, string holdComment, DateTime releaseDate)
        {
            string errorCode = "";
            string errorMsg = "";
            bool retVal = false;
            List<string> mdseICN = new List<string>();

            string customerNumber = "";
            string storeNumber = "";
            string userId = "";
            string[] refType = new string[customerHolds.Count];
            string[] refNumber = new string[customerHolds.Count];
            string[] relDate = new string[1];
            string[] holdInfo = new string[1];
            string[] jCase = new string[1];
            jCase[0] = "";

            relDate[0] = releaseDate.FormatDate();
            holdInfo[0] = "";
            int i = 0;
            foreach (HoldData custHoldData in customerHolds)
            {
                if (customerNumber == string.Empty)
                    customerNumber = custHoldData.CustomerNumber;
                if (userId == string.Empty)
                    userId = custHoldData.UserId;
                if (storeNumber == string.Empty)
                    storeNumber = custHoldData.OrgShopNumber;

                refType[i] = custHoldData.RefType;
                refNumber[i] = custHoldData.TicketNumber.ToString();
                foreach (Item pItem in custHoldData.Items)
                {
                    mdseICN.Add(pItem.Icn);
                }

                i++;
            }

            retVal = ExecuteMaintainHolds(HoldData.CUSTOMER_HOLD, customerNumber,
                                          mdseICN,
                                          storeNumber,
                                          userId, holdComment, refType,
                                          refNumber, relDate, holdInfo, "N", jCase, out errorCode,
                                          out errorMsg);
            return retVal;
        }

        /// <summary>
        /// Call to add holds on selected transactions
        /// </summary>
        /// <param name="customerHolds"></param>
        /// <param name="holdComment"></param>
        /// <param name="releaseDate"></param>
        /// <returns></returns>
        public static bool AddPoliceHolds(
            List<HoldData> policeHolds,
            string holdComment,
            DateTime releaseDate,
            string officerFirstName,
            string officerLastName,
            string badgeNumber,
            string officerPhoneAreaCode,
            string officerPhoneExtension,
            string officerPhoneNumber,
            string holdRequestType,
            string caseNumber,
            string agency)
        {
            string errorCode = "";
            string errorMsg = "";
            bool retVal = false;
            List<string> mdseIcn = new List<string>();

            string customerNumber = "";
            string storeNumber = "";
            string userId = "";
            string[] refType = new string[policeHolds.Count];
            string[] refNumber = new string[policeHolds.Count];
            string[] relDate = new string[1];
            string[] holdOfficerInfo = new string[9];

            string[] jCase = new string[1];
            jCase[0] = "";

            relDate[0] = releaseDate.FormatDate();
            int i = 0;
            foreach (HoldData holdData in policeHolds)
            {
                if (customerNumber == string.Empty)
                    customerNumber = holdData.CustomerNumber;
                if (userId == string.Empty)
                    userId = holdData.UserId;
                if (storeNumber == string.Empty)
                    storeNumber = holdData.OrgShopNumber;

                refType[i] = holdData.RefType;
                refNumber[i] = holdData.TicketNumber.ToString();
                for (int j = 0; j < holdData.Items.Count; j++)
                    mdseIcn.Add(holdData.Items[j].Icn);

                i++;
            }

            holdOfficerInfo[0] = officerLastName;
            holdOfficerInfo[1] = officerFirstName;
            holdOfficerInfo[2] = badgeNumber;
            holdOfficerInfo[3] = officerPhoneAreaCode;
            holdOfficerInfo[4] = officerPhoneNumber;
            holdOfficerInfo[5] = officerPhoneExtension;
            holdOfficerInfo[6] = holdRequestType;
            holdOfficerInfo[7] = caseNumber;
            holdOfficerInfo[8] = agency;

            retVal = ExecuteMaintainHolds(HoldData.POLICE_HOLD, customerNumber,
                                          mdseIcn,
                                          storeNumber,
                                          userId, holdComment, refType,
                                          refNumber, relDate, holdOfficerInfo, "N", jCase, out errorCode,
                                          out errorMsg);
            return retVal;
        }

        /// <summary>
        /// Call to remove the police holds on transactions
        /// </summary>
        /// <param name="customerHolds"></param>
        /// <param name="releaseComment"></param>
        /// <returns></returns>
        public static bool RemovePoliceHolds(List<HoldData> policeHolds, string releaseComment,
                                             string officerFirstName,
                                             string officerLastName,
                                             string badgeNumber,
                                             string agency,
                                             string caseNumber,
                                             string phoneAreaCode,
                                             string phoneNumber,
                                             string phoneExtension)
        {
            string errorCode = "";
            string errorMsg = "";
            bool retVal = false;
            List<string> mdseICN = new List<string>();
            string customerNumber = "";
            string storeNumber = "";
            string userId = "";
            string[] refType = new string[policeHolds.Count];
            string[] refNumber = new string[policeHolds.Count];
            string[] releaseDate = new string[policeHolds.Count];
            string[] holdInfo = new string[9];
            holdInfo[0] = officerLastName;
            holdInfo[1] = officerFirstName;
            holdInfo[2] = badgeNumber;
            holdInfo[3] = phoneAreaCode;
            holdInfo[4] = phoneNumber;
            holdInfo[5] = phoneExtension;
            //request type is not populated when releasing hold
            holdInfo[6] = "";
            holdInfo[7] = caseNumber;
            holdInfo[8] = agency;
            string[] jCase = new string[1];
            jCase[0] = "";

            int i = 0;
            foreach (HoldData holdData in policeHolds)
            {
                if (customerNumber == string.Empty)
                    customerNumber = holdData.CustomerNumber;
                if (userId == string.Empty)
                    userId = holdData.UserId;
                if (storeNumber == string.Empty)
                    storeNumber = holdData.OrgShopNumber;
                refType[i] = holdData.RefType;
                refNumber[i] = holdData.TicketNumber.ToString();
                releaseDate[i] = holdData.ReleaseDate.FormatDate();
                foreach (Item pItem in holdData.Items)
                    mdseICN.Add(pItem.Icn);
                i++;
            }

            retVal = ExecuteMaintainHolds(HoldData.POLICE_HOLD, customerNumber,
                                          mdseICN,
                                          storeNumber,
                                          userId, releaseComment, refType,
                                          refNumber, releaseDate, holdInfo, "Y", jCase, out errorCode,
                                          out errorMsg);
            return retVal;
        }

        /// <summary>
        /// Call to remove the holds on transactions
        /// </summary>
        /// <param name="customerHolds"></param>
        /// <param name="releaseComment"></param>
        /// <returns></returns>
        public static bool RemoveCustomerHolds(List<HoldData> customerHolds, string releaseComment)
        {
            string errorCode = "";
            string errorMsg = "";
            bool retVal = false;
            List<string> mdseICN = new List<string>();
            string customerNumber = "";
            string storeNumber = "";
            string userId = "";
            string[] refType = new string[customerHolds.Count];
            string[] refNumber = new string[customerHolds.Count];
            string[] releaseDate = new string[customerHolds.Count];
            string[] holdInfo = new string[1];
            holdInfo[0] = "";
            string[] jCase = new string[1];
            jCase[0] = "";

            int i = 0;
            foreach (HoldData custHoldData in customerHolds)
            {
                if (customerNumber == string.Empty)
                    customerNumber = custHoldData.CustomerNumber;
                if (userId == string.Empty)
                    userId = custHoldData.UserId;
                if (storeNumber == string.Empty)
                    storeNumber = custHoldData.OrgShopNumber;
                refType[i] = custHoldData.RefType;
                refNumber[i] = custHoldData.TicketNumber.ToString();
                releaseDate[i] = custHoldData.ReleaseDate.FormatDate();
                foreach (Item pItem in custHoldData.Items)
                    mdseICN.Add(pItem.Icn);
                i++;
            }

            retVal = ExecuteMaintainHolds(HoldData.CUSTOMER_HOLD, customerNumber,
                                          mdseICN,
                                          storeNumber,
                                          userId, releaseComment, refType,
                                          refNumber, releaseDate, holdInfo, "Y", jCase, out errorCode,
                                          out errorMsg);
            return retVal;
        }

        /// <summary>
        /// Call to update the release dates on selected transactions
        /// </summary>
        /// <param name="Holds"></param>
        /// <returns></returns>
        public static bool UpdateReleaseDateOnHolds(List<HoldData> Holds)
        {
            string errorCode = "";
            string errorMsg = "";
            bool retVal = false;
            List<string> mdseICN = new List<string>();

            string customerNumber = "";
            string storeNumber = "";
            string userId = "";
            string[] refType = new string[Holds.Count];
            string[] refNumber = new string[Holds.Count];
            string[] relDate = new string[Holds.Count];
            string[] holdInfo = new string[1];
            holdInfo[0] = "";
            string holdComment = "";
            bool callCustHold = false;
            bool callPoliceHold = false;
            string[] jCase = new string[1];
            jCase[0] = "";

            int i = 0;
            foreach (HoldData holdData in Holds)
            {
                if (customerNumber == string.Empty)
                    customerNumber = holdData.CustomerNumber;
                if (userId == string.Empty)
                    userId = holdData.UserId;
                if (storeNumber == string.Empty)
                    storeNumber = holdData.OrgShopNumber;
                refType[i] = holdData.RefType;
                refNumber[i] = holdData.TicketNumber.ToString();
                relDate[i] = holdData.ReleaseDate.FormatDate();
                holdComment = holdData.HoldComment;

                if (holdData.HoldType == HoldTypes.CUSTHOLD.ToString())

                    callCustHold = true;
                if (holdData.HoldType == HoldTypes.POLICEHOLD.ToString())
                {
                    callPoliceHold = true;
                }
                foreach (Item pitem in holdData.Items)
                    mdseICN.Add(pitem.Icn);
                i++;
            }

            if (callCustHold)
                retVal = ExecuteMaintainHolds(HoldData.CUSTOMER_HOLD, customerNumber,
                                              mdseICN,
                                              storeNumber,
                                              userId, holdComment, refType,
                                              refNumber, relDate, holdInfo, "N", jCase, out errorCode,
                                              out errorMsg);
            if (callPoliceHold)
                retVal = ExecuteMaintainHolds(HoldData.POLICE_HOLD, customerNumber,
                                              mdseICN,
                                              storeNumber,
                                              userId, holdComment, refType,
                                              refNumber, relDate, holdInfo, "N", jCase, out errorCode,
                                              out errorMsg);

            return retVal;
        }

        /// <summary>
        /// Call to update the release dates on selected mdse
        /// </summary>
        /// <param name="Holds"></param>
        /// <returns></returns>
        public static bool UpdateReleaseDateOnMdse(HoldData holdData, List<DateTime> releaseDates)
        {
            string errorCode = "";
            string errorMsg = "";
            bool retVal = false;
            List<string> mdseICN = new List<string>();

            string customerNumber = "";
            string storeNumber = "";
            string userId = "";
            string[] refType = new string[1];
            string[] refNumber = new string[1];

            string[] relDate = new string[releaseDates.Count];
            string[] holdInfo = new string[1];
            holdInfo[0] = "";
            const string holdComment = "";
            string[] jCase = new string[1];
            jCase[0] = "";

            if (customerNumber == string.Empty)
                customerNumber = holdData.CustomerNumber;
            if (userId == string.Empty)
                userId = holdData.UserId;
            if (storeNumber == string.Empty)
                storeNumber = holdData.OrgShopNumber;
            refType[0] = holdData.RefType;
            refNumber[0] = holdData.TicketNumber.ToString();

            foreach (Item pitem in holdData.Items)
                mdseICN.Add(pitem.Icn);
            int i = 0;
            foreach (DateTime releaseDate in releaseDates)
            {
                relDate[i] = releaseDate.FormatDate();
                i++;
            }

            retVal = ExecuteMaintainHolds(HoldData.POLICE_HOLD, customerNumber,
                                          mdseICN,
                                          storeNumber,
                                          userId, holdComment, refType,
                                          refNumber, relDate, holdInfo, "N", jCase, out errorCode,
                                          out errorMsg);

            return retVal;
        }

        /// <summary>
        /// call to release items to claimant
        /// </summary>
        /// <param name="policeHolds"></param>
        /// <param name="releaseComment"></param>
        /// <param name="policeInformation"></param>
        /// <param name="claimantCustomer"></param>
        /// <returns></returns>

        public static bool AddReleaseToClaimant(List<HoldData> policeHolds, string releaseComment,
                                                PoliceInfo policeInformation, CustomerVO claimantCustomer)
        {
            bool retVal = false;
            List<string> mdseICN = new List<string>();
            List<string> storeNumber = new List<string>();
            List<string> refType = new List<string>();
            List<string> jCase = new List<string>();

            string customerNumber = "";

            string userId = "";

            List<string> holdInfo = new List<string>();
            holdInfo.Add(policeInformation.OfficerLastName);
            holdInfo.Add(policeInformation.OfficerFirstName);
            holdInfo.Add(policeInformation.BadgeNumber);
            holdInfo.Add(policeInformation.PhoneAreaCode);
            holdInfo.Add(policeInformation.PhoneNumber);
            holdInfo.Add(policeInformation.PhoneExtension);
            //request type is not populated for release to claimant
            holdInfo.Add("");
            holdInfo.Add(policeInformation.CaseNumber);
            holdInfo.Add(policeInformation.Agency);
            int i = 0;
            foreach (HoldData holdData in policeHolds)
            {
                if (customerNumber == string.Empty)
                    customerNumber = holdData.CustomerNumber;
                if (userId == string.Empty)
                    userId = holdData.UserId;
                storeNumber.Add(holdData.OrgShopNumber);
                refType.Add(holdData.RefType);
                foreach (Item pItem in holdData.Items)
                {
                    mdseICN.Add(pItem.Icn);
                }
                foreach (Item pItem in holdData.Items)
                {
                    if (string.IsNullOrEmpty(pItem.JeweleryCaseNumber))
                        jCase.Add(string.Empty);
                    else
                        jCase.Add(pItem.JeweleryCaseNumber);
                }
                i++;
            }

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (GlobalDataAccessor.Instance == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                return (false);
            }

            var inParams = new List<OracleProcParam>();

            //Setup input params
            var loansParam = new OracleProcParam(ParameterDirection.Input,
                                                 DataTypeConstants.PawnDataType.LISTSTRING, "p_loan_number",
                                                 policeHolds.Count);
            //Manual ticket is needed only for manual entry
            //but the SP expects it to have as many elements as loan number
            var manTicketParam = new OracleProcParam(ParameterDirection.Input,
                                                     DataTypeConstants.PawnDataType.LISTSTRING, "p_man_ticket",
                                                     policeHolds.Count);
            for (int j = 0; j < policeHolds.Count; ++j)
            {
                loansParam.AddValue(policeHolds[j].TicketNumber);
                manTicketParam.AddValue("");
            }
            inParams.Add(manTicketParam);

            inParams.Add(new OracleProcParam("p_store_number", true, storeNumber));
            inParams.Add(new OracleProcParam("p_customer_number", claimantCustomer.CustomerNumber));

            // EDW - CR#15278
            KeyValuePair<string, string> lastIdUsed = GlobalDataAccessor.Instance.DesktopSession.LastIdUsed;
            IdentificationVO id = claimantCustomer.getIdByTypeandIssuer(lastIdUsed.Key, lastIdUsed.Value);
            if (id == null)
            {
                id = claimantCustomer.getFirstIdentity();
            }

            string custIdNum = "";
            string custIdType = "";
            string custIDCode = "";
            if (id != null)
            {
                custIDCode = id.IdIssuerCode;
                custIdNum = id.IdValue;
                custIdType = id.IdType;
            }

            inParams.Add(new OracleProcParam("p_cust_id_num", custIdNum));
            inParams.Add(new OracleProcParam("p_cust_id_type", custIdType));
            inParams.Add(new OracleProcParam("p_cust_id_agency", custIDCode));

            inParams.Add(new OracleProcParam("p_user_id", userId));
            inParams.Add(new OracleProcParam("p_status_cd", ProductStatus.RTC.ToString()));
            //inParams.Add(new OracleProcParam("p_generic_processname", serviceType.ToString()));

            inParams.Add(new OracleProcParam("p_police_info_array", true, holdInfo));
            //inParams.Add(new OracleProcParam("p_ref_date", true, receiptData.RefDates));
            string currDate = ShopDateTime.Instance.ShopDate.FormatDate();
            string currDateTime = currDate + " " + ShopDateTime.Instance.ShopTime.ToString();
            inParams.Add(new OracleProcParam("p_tran_date", currDateTime));
            inParams.Add(new OracleProcParam("p_bckgrnd_ck", claimantCustomer.BackgroundCheckRefNumber));
            inParams.Add(new OracleProcParam("p_mdse_icn", true, mdseICN));
            inParams.Add(loansParam);
            inParams.Add(new OracleProcParam("p_ref_type_in", true, refType));
            inParams.Add(new OracleProcParam("p_process_flag", ProductStatus.RTC.ToString()));
            inParams.Add(new OracleProcParam("p_comment", releaseComment));
            inParams.Add(new OracleProcParam("p_jewelry_case", true, jCase));
            inParams.Add(new OracleProcParam("o_seize_number", OracleDbType.Int32, DBNull.Value, ParameterDirection.Output, 1));
            GlobalDataAccessor.Instance.beginTransactionBlock();
            DataSet outputDataSet;
            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "service_pawn_loans",
                        "service_police_actions", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                    BasicExceptionHandler.Instance.AddException("Calling service_police_actions stored procedure", oEx);
                    return (false);
                }

                if (retVal == false)
                {
                    GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                    return (false);
                }
                GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.COMMIT);

                ReceiptDetailsVO rDVO = new ReceiptDetailsVO();
                if (!insertPoliceReceipt(policeHolds, ref rDVO))
                    FileLogger.Instance.logMessage(LogLevel.ERROR, null, "Receipt details could not be entered for release to claimant");

                return (true);
            }

            return (false);
        }

        /// <summary>
        /// Call to add police seize on items
        /// </summary>
        /// <param name="policeHolds"></param>
        /// <param name="seizeComment"></param>
        /// <param name="policeInformation"></param>
        /// <param name="currentCustomer"></param>
        /// <param name="seizeNumber"></param>
        /// <returns></returns>
        public static bool AddPoliceSeize(List<HoldData> policeHolds, string seizeComment,
                                          PoliceInfo policeInformation, CustomerVO currentCustomer, out int seizeNumber)
        {
            bool retVal = false;
            List<string> mdseICN = new List<string>();
            List<string> storeNumber = new List<string>();
            List<string> refType = new List<string>();
            List<string> jCase = new List<string>();

            string userId = "";
            seizeNumber = 0;

            List<string> holdInfo = new List<string>();
            holdInfo.Add(policeInformation.OfficerLastName);
            holdInfo.Add(policeInformation.OfficerFirstName);
            holdInfo.Add(policeInformation.BadgeNumber);
            holdInfo.Add(policeInformation.PhoneAreaCode);
            holdInfo.Add(policeInformation.PhoneNumber);
            holdInfo.Add(policeInformation.PhoneExtension);
            //request type is not populated for seize
            holdInfo.Add(" ");
            holdInfo.Add(policeInformation.CaseNumber);
            holdInfo.Add(policeInformation.Agency);
            int i = 0;
            foreach (HoldData holdData in policeHolds)
            {
                if (userId == string.Empty)
                    userId = holdData.UserId;
                storeNumber.Add(holdData.OrgShopNumber);
                refType.Add(holdData.RefType);
                mdseICN.AddRange(holdData.Items.Select(pItem => pItem.Icn));
                foreach (Item pItem in holdData.Items)
                {
                    if (string.IsNullOrEmpty(pItem.JeweleryCaseNumber))
                        jCase.Add(string.Empty);
                    else
                        jCase.Add(pItem.JeweleryCaseNumber);
                }
                i++;
            }
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            if (GlobalDataAccessor.Instance == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                seizeNumber = 0;
                return (false);
            }

            var inParams = new List<OracleProcParam>();

            //Setup input params
            var loansParam = new OracleProcParam(ParameterDirection.Input,
                                                 DataTypeConstants.PawnDataType.LISTSTRING, "p_loan_number",
                                                 policeHolds.Count);
            //Manual ticket is needed only for manual entry
            //but the SP expects it to have as many elements as loan number
            var manTicketParam = new OracleProcParam(ParameterDirection.Input,
                                                     DataTypeConstants.PawnDataType.LISTSTRING, "p_man_ticket",
                                                     policeHolds.Count);
            for (int j = 0; j < policeHolds.Count; ++j)
            {
                loansParam.AddValue(policeHolds[j].TicketNumber);
                manTicketParam.AddValue("");
            }
            inParams.Add(manTicketParam);

            inParams.Add(new OracleProcParam("p_store_number", true, storeNumber));
            inParams.Add(new OracleProcParam("p_customer_number", currentCustomer.CustomerNumber));
            string sIdValue = string.Empty;
            string sIdType = string.Empty;
            string sIdIssuerCode = string.Empty;
            IdentificationVO id = currentCustomer.getFirstIdentity();
            if (id != null)
            {
                sIdValue = id.IdValue;
                sIdType = id.IdType;
                sIdIssuerCode = id.IdIssuerCode;
            }
            inParams.Add(new OracleProcParam("p_cust_id_num", sIdValue));
            inParams.Add(new OracleProcParam("p_cust_id_type", sIdType));
            inParams.Add(new OracleProcParam("p_cust_id_agency", sIdIssuerCode));


            inParams.Add(new OracleProcParam("p_user_id", userId));
            inParams.Add(new OracleProcParam("p_status_cd", ProductStatus.PS.ToString()));

            inParams.Add(new OracleProcParam("p_police_info_array", true, holdInfo));
            //inParams.Add(new OracleProcParam("p_ref_date", true, receiptData.RefDates));
            string currDate = ShopDateTime.Instance.ShopDate.FormatDate();
            string currDateTime = currDate + " " + ShopDateTime.Instance.ShopTime.ToString();
            inParams.Add(new OracleProcParam("p_tran_date", currDateTime));
            inParams.Add(new OracleProcParam("p_bckgrnd_ck", currentCustomer.BackgroundCheckRefNumber));
            inParams.Add(new OracleProcParam("p_mdse_icn", true, mdseICN));
            inParams.Add(loansParam);
            inParams.Add(new OracleProcParam("p_ref_type_in", true, refType));
            inParams.Add(new OracleProcParam("p_process_flag", ProductStatus.PS.ToString()));
            inParams.Add(new OracleProcParam("p_comment", seizeComment));
            inParams.Add(new OracleProcParam("p_jewelry_case", true, jCase));
            inParams.Add(new OracleProcParam("o_seize_number", OracleDbType.Int32, DBNull.Value, ParameterDirection.Output, 1));
            GlobalDataAccessor.Instance.beginTransactionBlock();

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                DataSet outputDataSet;

                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "service_pawn_loans",
                        "service_police_actions", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                    BasicExceptionHandler.Instance.AddException("Calling service_police_actions stored procedure failed when trying to add police seize", oEx);
                    seizeNumber = 0;
                    return (false);
                }

                if (retVal == false)
                {
                    GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                    seizeNumber = 0;
                    return (false);
                }
                GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.COMMIT);
                //Get seize number
                DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
                {
                    DataRow dr = outputDt.Rows[0];
                    if (dr != null && dr.ItemArray.Length > 0)
                    {
                        object obj = dr.ItemArray.GetValue(1);
                        if (obj.ToString() != "null")
                        {
                            var nextNumStr = (string)obj;
                            seizeNumber = Int32.Parse(nextNumStr);
                            return (true);
                        }
                    }
                }
            }
            seizeNumber = 0;
            return (retVal);
        }

        public static bool AddReleaseFingerprints(ReleaseFingerprintsInfo releaseFingerprintsInfo, out int seizeNumber)
        {
            bool retVal = false;
            seizeNumber = 0;

            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            if (GlobalDataAccessor.Instance == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                seizeNumber = 0;
                return (false);
            }

            var inParams = new List<OracleProcParam>();

            //Setup input params
            inParams.Add(new OracleProcParam("p_ref_type", releaseFingerprintsInfo.RefType));
            inParams.Add(new OracleProcParam("p_ref_number", releaseFingerprintsInfo.RefNumber));
            inParams.Add(new OracleProcParam("p_comment", releaseFingerprintsInfo.Comment));
            inParams.Add(new OracleProcParam("p_store_number", releaseFingerprintsInfo.StoreNumber));
            inParams.Add(new OracleProcParam("p_subpoena_no", releaseFingerprintsInfo.SubpoenaNumber));
            inParams.Add(new OracleProcParam("p_officer_first_name", releaseFingerprintsInfo.OfficerFirstName));
            inParams.Add(new OracleProcParam("p_officer_last_name", releaseFingerprintsInfo.OfficerLastName));
            inParams.Add(new OracleProcParam("p_badge", releaseFingerprintsInfo.BadgeNumber));
            inParams.Add(new OracleProcParam("p_case_number", releaseFingerprintsInfo.CaseNumber));
            inParams.Add(new OracleProcParam("p_agency", releaseFingerprintsInfo.Agency));
            inParams.Add(new OracleProcParam("p_user_id", GlobalDataAccessor.Instance.DesktopSession.UserName));
            inParams.Add(new OracleProcParam("p_seize_status", "FPRNT"));
            inParams.Add(new OracleProcParam("p_tran_date", releaseFingerprintsInfo.TransactionDate));

            inParams.Add(new OracleProcParam("o_seize_number", OracleDbType.Int32, DBNull.Value, ParameterDirection.Output, 1));

            GlobalDataAccessor.Instance.beginTransactionBlock();

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                DataSet outputDataSet;

                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_manage_holds",
                        "insert_release_fingerprints", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                    BasicExceptionHandler.Instance.AddException("Calling insert_release_fingerprints stored procedure failed when trying to add data", oEx);
                    seizeNumber = 0;
                    return (false);
                }

                if (retVal == false)
                {
                    GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                    seizeNumber = 0;
                    return (false);
                }
                GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.COMMIT);
                //Get seize number
                DataTable outputDt = outputDataSet.Tables["OUTPUT"];
                if (outputDt != null && outputDt.IsInitialized && outputDt.Rows != null && outputDt.Rows.Count > 0)
                {
                    DataRow dr = outputDt.Rows[0];
                    if (dr != null && dr.ItemArray.Length > 0)
                    {
                        object obj = dr.ItemArray.GetValue(1);
                        if (obj.ToString() != "null")
                        {
                            var nextNumStr = (string)obj;
                            seizeNumber = Int32.Parse(nextNumStr);
                            return (true);
                        }
                    }
                }
            }
            seizeNumber = 0;
            return (false);
        }

        public static bool GetPawnLoanHolds(
            Int32 storeNumber,
            Int32 idNumber,
            string idType,
            out PawnLoan pawnLoan,
            out string errorCode,
            out string errorText)
        {
            //Set default output params
            pawnLoan = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            if (GlobalDataAccessor.Instance == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "GetPawnLoanHoldsFailed";
                errorText = "Invalid desktop session or data accessor";
                return (false);
            }

            //Get data accessor object
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            List<OracleProcParam> inParams = new List<OracleProcParam>();

            //Add store number
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            //Add id number
            inParams.Add(new OracleProcParam("p_id_number", idNumber));

            //Add id type
            inParams.Add(new OracleProcParam("p_id_type", idType));

            List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
            refCursArr.Add(new PairType<string, string>("o_pawn_loan", PAWN_LOAN));
            refCursArr.Add(new PairType<string, string>("o_pawn_mdselist", PAWN_MDSE_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_gunlist", PAWN_GUN_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_mdhistlist", PAWN_MDHIST_LIST));
            refCursArr.Add(new PairType<string, string>("o_pawn_otherdsclist", PAWN_OTHERDSC_LIST));
            DataSet outputDataSet;
            bool retVal = false;
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_manage_holds",
                    "get_pawn_loan_holds", inParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling get_pawn_loan_holds stored procedure", oEx);
                errorCode = " -- getpawnloanhold failed";
                errorText = " --- Exception: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
                return (false);
            }
            else
            {
                if (outputDataSet != null && outputDataSet.IsInitialized)
                {
                    if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                    {
                        List<PawnLoan> pawnLoans;
                        List<PawnAppVO> pawnApps;
                        try
                        {
                            CustomerLoans.ParseDataSet(outputDataSet, out pawnLoans, out pawnApps);
                            if (CollectionUtilities.isEmpty(pawnLoans))
                            {
                                errorCode = "Parsing the data from the stored procedure failed";
                                errorText = "Pawn Loans is null";
                                return false;
                            }
                            pawnLoan = pawnLoans.First();
                            return (true);
                        }
                        catch (Exception ex)
                        {
                            errorCode = "Parsing the data from the stored procedure failed";
                            errorText = ex.Message;
                            return false;
                        }
                    }
                }
            }

            errorCode = "GETPAWNLOANHOLDSFAIL";
            errorText = "Operation failed";
            return (false);
        }

        public static bool ExecuteGetReleases(
            string storeNumber,
            string customerNumber,
            string statusCode,
            string holdType,
            out DataTable customerHolds,
            out DataTable merchandiseList,
            out string errorCode,
            out string errorMesg)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            //Set default output params
            customerHolds = null;
            merchandiseList = null;
            errorCode = string.Empty;
            errorMesg = string.Empty;

            if (GlobalDataAccessor.Instance == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                customerHolds = null;
                merchandiseList = null;
                errorCode = "GetCustomerReleasesFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            inParams.Add(new OracleProcParam("p_customer_number", customerNumber));

            inParams.Add(new OracleProcParam("p_status_cd", statusCode));

            inParams.Add(new OracleProcParam("p_hold_type", holdType));

            bool rt = false;
            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_holds_list", CUSTOMER_HOLDS_LIST));
                refCursArr.Add(new PairType<string, string>("r_mdse_list", MERCHANDISE_LIST));
                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_manage_holds",
                        "get_holds_for_release", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling get_holds_for_release stored procedure", oEx);
                    errorCode = "GetHoldsForRelease";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
                else
                {
                    if (outputDataSet != null && outputDataSet.IsInitialized)
                    {
                        if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                        {
                            customerHolds = outputDataSet.Tables[CUSTOMER_HOLDS_LIST];
                            merchandiseList = outputDataSet.Tables[MERCHANDISE_LIST];
                            return (true);
                        }
                    }
                }

                errorCode = "GetHoldsForReleasesFail";
                errorMesg = "Operation failed";
                return (false);
            }
            else
            {
                errorCode = "GetHoldsForReleasesFail";
                errorMesg = "No valid input parameters given";
            }
            return (rt);
        }

        public static bool insertPoliceReceipt(List<HoldData> pVos, ref ReceiptDetailsVO rDVO)
        {
            GlobalDataAccessor cds = GlobalDataAccessor.Instance;
            SiteId curSiteId = cds.CurrentSiteId;
            //long rcptNum;

            try
            {
                rDVO.ReceiptDate = ShopDateTime.Instance.ShopDate;
                int totCount = pVos.Count;
                string[] refDate = new string[totCount];
                string[] refNumber = new string[totCount];
                string[] refType = new string[totCount];
                string[] refEvent = new string[totCount];
                string[] refAmount = new string[totCount];
                string[] refStore = new string[totCount];
                string[] refTime = new string[totCount];
                int i = 0;
                foreach (HoldData pVo in pVos)
                {
                    //ref time
                    //rDVO.RefTimes.Add(rDVO.ReceiptDate.FormatDateAsTimestampWithTimeZone());
                    refDate[i] = ShopDateTime.Instance.ShopDate.ToShortDateString();

                    //ref time
                    refTime[i] = ShopDateTime.Instance.ShopDate.ToShortDateString() + " " +
                                 ShopDateTime.Instance.ShopTime.ToString();

                    // ref number for service pawn loan is the ticket number
                    refNumber[i] = "" + pVo.TicketNumber;

                    // ref type for pawn loan is "PAWN" which is "1" in the db
                    //ref type for pawn purchase is "PURCHASE" which is 2 in the db
                    refType[i] = pVo.RefType;

                    switch (pVo.TempStatus)
                    {
                        case StateStatus.RTC:
                            refEvent[i] = ReceiptEventTypes.RTC.ToString();
                            //TODO Change it when we have restituion amount added
                            refAmount[i] = "0";
                            break;
                        case StateStatus.PS:
                            refEvent[i] = ReceiptEventTypes.PolSeize.ToString();
                            //TODO Change it when we have restituion amount added
                            refAmount[i] = "0";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    // ref store for service is the store the receipt was printed at
                    refStore[i] = curSiteId.StoreNumber;
                    i++;
                }
                long rcptNum;
                string errorCode;
                string errorText;
                ProcessTenderProcedures.ExecuteInsertReceiptDetails(
                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                    GlobalDataAccessor.Instance.DesktopSession.UserName, ShopDateTime.Instance.ShopDate.ToShortDateString(),
                    GlobalDataAccessor.Instance.DesktopSession.UserName, refDate, refTime, refNumber, refType, refEvent, refAmount, refStore,
                    out rcptNum, out errorCode, out errorText);
            }
            catch (Exception eX)
            {
                BasicExceptionHandler.Instance.AddException("Creating receipt data failed!!", new ApplicationException("Exception", eX));
                return (false);
            }
            return true;
        }

        /// <summary>
        /// Gives back the police and hold related information
        /// for a given receipt detail id
        /// </summary>
        /// <param name="receipt_detail_id"></param>
        /// <param name="ref_event"></param>
        /// <param name="PoliceInfo"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool getRTCSeizeInfo(
            string receipt_detail_id,
            string ref_event,
            out DataTable PoliceInfo,
            out string errorCode,
            out string errorMesg)
        {
            PoliceInfo = null;
            errorCode = "";
            errorMesg = "";
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (GlobalDataAccessor.Instance == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "getRTCSeizeInfoFailed";
                errorMesg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_receipt_detail_id", receipt_detail_id));

            inParams.Add(new OracleProcParam("p_ref_event", ref_event));

            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("r_police_data", POLICE_HOLD_DATA));

                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_manage_holds",
                        "get_rtcseizeinfo", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling get_rtcseizeinfo stored procedure", oEx);
                    errorCode = "Get_rtcseizeinfo";
                    errorMesg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorMesg = dA.ErrorDescription;
                    return (false);
                }
                if (outputDataSet != null && outputDataSet.IsInitialized)
                {
                    if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                    {
                        PoliceInfo = outputDataSet.Tables[POLICE_HOLD_DATA];

                        return (true);
                    }
                }

                errorCode = "Get_rtcseizeinfoFail";
                errorMesg = "Operation failed";
                return (false);
            }
            errorCode = "Get_rtcseizeinfoFail";
            errorMesg = "No valid input parameters given";
            return false;
        }

        public static bool ExecuteAutoReleaseHolds(
            string storeNumber,
            out string errorCode,
            out string errorMsg)
        {
            errorCode = string.Empty;
            errorMsg = string.Empty;
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (GlobalDataAccessor.Instance == null ||
                GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "ExecuteAutoReleaseHoldsFailed";
                errorMsg = "Invalid desktop session or data accessor";
                return (false);
            }

            List<OracleProcParam> inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            if (inParams.Count > 0)
            {
                DataSet outputDataSet;
                bool retVal = false;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_manage_holds",
                        "auto_release_of_customer_holds", inParams,
                        null, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling auto_release_of_customer_holds stored procedure", oEx);
                    errorCode = dA.ErrorCode + " --- ExecuteAutoReleaseHoldsFailed";
                    errorMsg = "Exception: " + oEx.Message;
                    return (false);
                }

                if (retVal == false)
                {
                    BasicExceptionHandler.Instance.AddException("ExecuteAutoReleaseHolds Failed: return value is false", new ApplicationException());
                    errorCode = dA.ErrorCode + " --- ExecuteAutoReleaseHoldsFailed";
                    errorMsg = dA.ErrorDescription + " -- Return value is false";
                    return (false);
                }

                errorCode = "0";
                errorMsg = "Success";
                return (true);
            }

            errorCode = "ExecuteAutoReleaseHoldsFail";
            errorMsg = "No valid input parameters given";
            return (false);
        }

        public static ReleaseFingerprintsInfo GetReleaseFingerprintAuthorization(
            string seizeNumber,
            string storeNumber,
            out string errorCode,
            out string errorText)
        {
            ReleaseFingerprintsInfo retval = new ReleaseFingerprintsInfo();
            var oDa = GlobalDataAccessor.Instance.OracleDA;
            var inParams = new List<OracleProcParam>
                                   {
                                       new OracleProcParam("p_seize_number", seizeNumber),
                                       new OracleProcParam("p_store_number", storeNumber),
                                   };


            var refCursArr = new List<PairType<string, string>>
                             {
                                 new PairType<string, string>("o_fingerprint_auth", RELEASE_AUTHORIZATION_DATA),
                                
                             };

            try
            {
                DataSet outputDataSet;
                bool wasSuccessful = oDa.issueSqlStoredProcCommand(
                    "ccsowner", "Service_Pawn_Loans",
                    "get_release_fingerprint_auth", inParams,
                    refCursArr, "o_return_code",
                    "o_return_text",
                    out outputDataSet);

                if (wasSuccessful)
                {

                    if (outputDataSet.Tables[0].Rows.Count > 0)
                    {
                        // There is a seize to work with
                        retval = new ReleaseFingerprintsInfo();
                        DataRow row = outputDataSet.Tables[0].Rows[0];
                        retval.SeizeNumber = int.Parse(row["Seize_no"].ToString());
                        retval.RefNumber = int.Parse(row["ref_Number"].ToString());
                        retval.RefType = row["ref_type"].ToString();
                        retval.SeizeStatus = (row["seize_status"] ?? "").ToString();
                        retval.Comment = (row["sz_comment"] ?? "").ToString();
                        retval.StoreNumber = storeNumber;
                        retval.SubpoenaNumber = row["subpoena_no"].ToString();
                        retval.OfficerFirstName = row["Officer_First_Name"].ToString();
                        retval.OfficerLastName = row["Officer_Last_Name"].ToString();
                        retval.BadgeNumber = row["Badge"].ToString();
                        retval.Agency = row["Agency"].ToString();
                        retval.CaseNumber = row["Case_Number"].ToString();

                    }
                }
                else
                {
                    retval = null;
                }

                errorCode = oDa.ErrorCode;
                errorText = oDa.ErrorDescription;
            }
            catch (Exception oEx)
            {
                BasicExceptionHandler.Instance.AddException("Calling voidPartialPayment stored procedure", oEx);
                errorCode = "VoidReleaseFingerprints";
                errorText = "Exception: " + oEx.Message;
                retval = null;
            }


            return retval;
        }
    }
}
