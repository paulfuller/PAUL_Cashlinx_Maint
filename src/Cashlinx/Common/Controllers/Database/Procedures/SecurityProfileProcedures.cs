/**************************************************************************************************************
* CashlinxDesktop.DesktopProcedures
* SecurityProfileProcedures
* SR 2/8/2010 Added wrapper methods that parses the return values for a user's security profile and gives
* back the uservo object.
* SR 6/1/2010 Added wrapper methods for add visiting employee, delete and update.
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Security;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;

namespace Common.Controllers.Database.Procedures
{
    public class SecurityProfileProcedures
    {
        private const string USERPROFILECURRENTLOCATION = "UPDATE USER PROFILE RESOURCES CURRENT LOCATION ONLY";
        private const string USERPROFILEMULTIPLELOCATION = "UPDATE USER PROFILE RESOURCES MULTIPLE LOCATIONS";
        private static readonly string PRODUCTID_FOR_PAWN = "00033";
        private static readonly string PRODUCTID_FOR_BUY = "00034";
        private static readonly string PAWNLOANIP = "NEWPAWNLOAN";
        private static readonly string BUYRESOURCE = "CUSTOMERBUY";
        private static readonly string RETAILDISCOUNTPERCENT = "RETAILDISCOUNTPERCENT";
        private static readonly string LAYAWAY_NUMBER_INCREMENT_LIMIT = "LAYAWAY_NUMBER_INCREMENT_LIMIT";

        /// <summary>
        /// Gets the user security profile from the database
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="storeId"></param>
        /// <param name="storeNumber"></param>
        /// <param name="allProfilesFlag"></param>
        /// <param name="desktopSession"> </param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool GetUserSecurityProfile(
            string userName,
            string storeId,
            string storeNumber,
            string allProfilesFlag,
            DesktopSession desktopSession,
            out string errorCode,
            out string errorText)
        {
            // initialize error output variables
            errorCode = String.Empty;
            errorText = String.Empty;

            // Check the input parameters
            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(storeNumber))
            {
                BasicExceptionHandler.Instance.AddException("GetUserSecurityProfile Failed",
                                                            new ApplicationException("GetUserSecurityProfile Failed: No valid inputs"));
                return (false);
            }

            //Call the SP

            DataTable UserDataList;
            DataTable UserResourcesList;
            DataTable UserLimitList;
            DataTable UserStoreList;
            DataTable ServiceOfferingList;

            bool retVal = SecurityProfileSPCall(userName, storeNumber, storeId, allProfilesFlag,
                                                out UserDataList, out UserResourcesList, out UserLimitList, out UserStoreList, out ServiceOfferingList, out errorCode, out errorText);

            if (retVal)
            {
                //Sp call is a success so parse the returned values into uservo object
                UserVO userSecProfile;
                ParseDataIntoObject(UserDataList, UserResourcesList, UserLimitList, UserStoreList, ServiceOfferingList, desktopSession, out userSecProfile);
                desktopSession.LoggedInUserSecurityProfile = userSecProfile;
                return (true);
            }

            return (false);
        }

        /// <summary>
        /// Returns the uservo object which has the security profiles for the username
        /// passedin in the store number passed in
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="storeNumber"></param>
        /// <param name="storeId"></param>
        /// <param name="allProfilesFlag"></param>
        /// <param name="desktopSession"> </param>
        /// <param name="userProfileObject"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool GetUserSecurityProfile(string userName,
                                                  string storeNumber,
                                                  string storeId,
                                                  string allProfilesFlag,
                                                  DesktopSession desktopSession,
                                                  out UserVO userProfileObject,
                                                  out string errorCode,
                                                  out string errorText)
        {
            // initialize error output variables
            errorCode = String.Empty;
            errorText = String.Empty;
            userProfileObject = null;

            // Check the input parameters
            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(storeNumber))
            {
                BasicExceptionHandler.Instance.AddException("GetUserSecurityProfile Failed",
                                                            new ApplicationException("GetUserSecurityProfile Failed: No valid inputs"));
                return (false);
            }

            //Call the SP
            DataTable UserDataList;
            DataTable UserResourcesList;
            DataTable UserLimitList;
            DataTable UserStoreList;
            DataTable UserServiceOfferingList;

            bool retVal = SecurityProfileSPCall(userName, storeNumber, storeId, allProfilesFlag,
                                                out UserDataList, out UserResourcesList, out UserLimitList, out UserStoreList, out UserServiceOfferingList, out errorCode, out errorText);
            if (retVal)
            {
                //Sp call is a success so parse the returned values into uservo object
                UserVO userSecProfile;
                ParseDataIntoObject(UserDataList, UserResourcesList, UserLimitList, UserStoreList, UserServiceOfferingList, desktopSession, out userSecProfile);
                userProfileObject = userSecProfile;

                return (true);
            }
            return (false);
        }

        /// <summary>
        /// Stored procedure call to add a visiting employee
        /// </summary>
        /// <param name="employeeNumber"></param>
        /// <param name="storeId"></param>
        /// <param name="storeNumber"></param>
        /// <param name="createdBy"></param>
        /// <param name="shopUserRoles"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool AddVisitingEmployee(
            string employeeNumber,
            string storeId,
            string storeNumber,
            string createdBy,
            List<string> shopUserRoles,
            DesktopSession desktopSession,
            out string errorCode,
            out string errorText)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (desktopSession == null || GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "1";
                errorText = "No Data Accessor";
                return (false);
            }

            var inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_employee_number", employeeNumber));
            inParams.Add(new OracleProcParam("p_store_id", storeId));
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_updated_by", createdBy));
            inParams.Add(new OracleProcParam("p_shop_user_roles", true, shopUserRoles));
            
            DataSet outputDataSet;
            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                bool retVal;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_security_profile",
                        "AddVisitingEmployee", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling AddVisitingEmployee stored procedure", oEx);
                    errorCode = "1";
                    errorText = "Database call to add visiting employee failed";
                    return false;
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorText = dA.ErrorDescription;
                    return false;
                }
            }
            errorCode = "0";
            errorText = "Success";
            return true;
        }

        /// <summary>
        /// Stored procedure call to delete profile of an employee
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="storeId"></param>
        /// <param name="storeNumber"></param>
        /// <param name="updatedBy"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool DeleteEmployeeProfile(
            string userId,
            string storeId,
            string storeNumber,
            string updatedBy,
            DesktopSession desktopSession,
            out string errorCode,
            out string errorText)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (desktopSession == null || GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "1";
                errorText = "No Data Accessor";
                return (false);
            }

            var inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_user_id", userId));
            inParams.Add(new OracleProcParam("p_store_id", storeId));
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_updated_by", updatedBy));

            DataSet outputDataSet;
            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                bool retVal;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_security_profile",
                        "DeleteEmployeeProfile", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling DeleteEmployeeProfile stored procedure", oEx);
                    errorCode = "1";
                    errorText = "Database call to delete employee profile failed";
                    return false;
                }

                if (retVal == false)
                {
                    errorCode = dA.ErrorCode;
                    errorText = "Failed to delete employee profile " + dA.ErrorDescription;
                    return false;
                }
                if (dA.ErrorCode != "0")
                {
                    errorCode = dA.ErrorCode;
                    errorText = dA.ErrorDescription;
                    return false;
                }
            }
            errorCode = "0";
            errorText = "Success";
            return true;
        }

        public static bool UpdateEmployeeProfile(
            string userName,
            string storeId,
            string storeNumber,
            string updatedBy,
            List<string> userSecurityProfileList,
            List<string> userLimits,
            DesktopSession desktopSession,
            out string errorCode,
            out string errorText)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (desktopSession == null || GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "1";
                errorText = "No Data Accessor";
                return (false);
            }

            var inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_user_number", userName));
            inParams.Add(new OracleProcParam("p_store_id", storeId));
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_updated_by", updatedBy));

            if (userSecurityProfileList.Count == 0)
                userSecurityProfileList.Add("");
            inParams.Add(new OracleProcParam("p_Usersecurityprofile_list", true, userSecurityProfileList));
            if (userLimits.Count == 0)
                userLimits.Add("");
            inParams.Add(new OracleProcParam("p_userlimitsrolelimits", true, userLimits));

            DataSet outputDataSet;
            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();

                bool retVal;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_security_profile",
                        "UpdateEmployeeProfile", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling UpdateEmployeeProfile stored procedure", oEx);
                    errorCode = "1";
                    errorText = "Database call to update employee profile failed";
                    return false;
                }

                if (retVal == false)
                {
                    errorCode = "1";
                    errorText = "Failed to update employee profile";
                    return false;
                }
            }
            errorCode = "0";
            errorText = "Success";
            return true;
        }

        public static bool GetStoreListForEntity(
            string facNumber,
            DesktopSession desktopSession,
            out DataTable entityList,
            out string errorCode,
            out string errorText)
        {
            entityList = null;
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;

            if (desktopSession == null || GlobalDataAccessor.Instance.OracleDA == null)
            {
                errorCode = "1";
                errorText = "No Data Accessor";
                return (false);
            }

            var inParams = new List<OracleProcParam>();

            inParams.Add(new OracleProcParam("p_facNumber", facNumber));

            DataSet outputDataSet;
            if (inParams.Count > 0)
            {
                List<PairType<string, string>> refCursArr = new List<PairType<string, string>>();
                refCursArr.Add(new PairType<string, string>("o_list_of_stores_for_entity", "listofentities"));
                bool retVal;
                try
                {
                    retVal = dA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_security_profile",
                        "GetStoreListForEntity", inParams,
                        refCursArr, "o_return_code",
                        "o_return_text",
                        out outputDataSet);
                }
                catch (Exception oEx)
                {
                    BasicExceptionHandler.Instance.AddException("Calling GetStoreListForEntity stored procedure", oEx);
                    errorCode = "1";
                    errorText = "Database call to Get store entities failed";
                    return false;
                }

                if (retVal == false)
                {
                    errorCode = "1";
                    errorText = "Failed to get store values";
                    return false;
                }
                if (outputDataSet != null && outputDataSet.IsInitialized)
                {
                    if (outputDataSet.Tables != null && outputDataSet.Tables.Count > 0)
                    {
                        entityList = outputDataSet.Tables["listofentities"];
                        errorCode = "0";
                        errorText = "Success";
                        return (true);
                    }
                }
            }
            errorCode = "1";
            errorText = "Error in getting store list";
            return false;
        }

        private static bool SecurityProfileSPCall(string userName,
                                                  string storeNumber,
                                                  string storeId,
                                                  string allProfilesFlag,
                                                  out DataTable UserDataList,
                                                  out DataTable UserResourcesList,
                                                  out DataTable UserLimitList,
                                                  out DataTable UserStoreList,
                                                  out DataTable UserServiceOfferingList,
                                                  out string errorCode,
                                                  out string errorText)
        {
            // initialize data table output variables
            UserDataList = null;
            UserResourcesList = null;
            UserLimitList = null;
            UserStoreList = null;
            UserServiceOfferingList = null;
            errorCode = string.Empty;
            errorText = string.Empty;

            // Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("SecurityProfileSPCall Failed",
                                                            new ApplicationException("SecurityProfileSPCall Failed: Data accessor instance is invalid"));
                return (false);
            }

            // Set up input variables
            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_user_name", userName));
            inParams.Add(new OracleProcParam("p_store_id", storeId));
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));
            inParams.Add(new OracleProcParam("p_all_profiles_flag", allProfilesFlag));

            // Set up output
            DataSet outputDataSet;
            bool retVal;
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            var refCursArr = new List<PairType<string, string>>
            {
                new PairType<string, string>("o_user_resources_list  ", "user_resources_list"),
                new PairType<string, string>("o_user_limits_list", "user_limit_list"),
                new PairType<string, string>("o_user_details_list", "user_details_list"),
                new PairType<string,string>("o_user_store_number_list", "user_store_number_list"),
                new PairType<string,string>("o_user_service_offering_list", "user_service_offering_list")
            };

            // call the sp
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_security_profile",
                    "sp_getUserSecurityProfile", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("SecurityProfileSPCall Failed", oEx);
                errorCode = " --- SecurityProfileSPCallFailed";
                errorText = " --- OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetUserSecurityProfile Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteGetUserSecurityProfileFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null || !outputDataSet.IsInitialized ||
                (outputDataSet.Tables == null || outputDataSet.Tables.Count <= 0))
            {
                return (false);
            }

            // load the output tables
            UserDataList = outputDataSet.Tables["user_details_list"];
            UserResourcesList = outputDataSet.Tables["user_resources_list"];
            UserLimitList = outputDataSet.Tables["user_limit_list"];
            UserStoreList = outputDataSet.Tables["user_store_number_list"];
            UserServiceOfferingList = outputDataSet.Tables["user_service_offering_list"];
            return true;
        }

        private static void ParseDataIntoObject(
            DataTable UserDataList,
            DataTable UserResourcesList,
            DataTable UserLimitList,
            DataTable UserStoreNumberList,
            DataTable UserServiceOfferingList,
            DesktopSession desktopSession,
            out UserVO userSecurityProfile)
        {
            userSecurityProfile = null;
            try
            {
                UserVO currUser;
                currUser = new UserVO();
                currUser.UserName = desktopSession.FullUserName;
                currUser.StoreNumber = desktopSession.CurrentSiteId.StoreNumber;

                if (UserDataList != null && UserDataList.Rows.Count > 0)
                {
                    string firstName = Utilities.GetStringValue(UserDataList.Rows[0]["fname"], "");
                    string lastName = Utilities.GetStringValue(UserDataList.Rows[0]["lname"], "");
                    string facNumber = Utilities.GetStringValue(UserDataList.Rows[0]["facnumber"], "");
                    string employeeNumber = Utilities.GetStringValue(UserDataList.Rows[0]["employeenumber"], "");
                    currUser.UserFirstName = firstName;
                    currUser.UserLastName = lastName;
                    currUser.UserID = Utilities.GetStringValue(UserDataList.Rows[0]["userid"], "");
                    if (!string.IsNullOrEmpty(facNumber))
                        currUser.FacNumber = facNumber;
                    if (!string.IsNullOrEmpty(employeeNumber))
                        currUser.EmployeeNumber = employeeNumber;
                    currUser.LastUpdatedDate = Utilities.GetDateTimeValue(UserDataList.Rows[0]["v_lastupdatedate"], DateTime.Now);
                    RoleVO userrole = new RoleVO();
                    userrole.RoleName = Utilities.GetStringValue(UserDataList.Rows[0]["role_name"], "");
                    currUser.UserRole = userrole;
                }

                //Add the user resource list
                if (UserResourcesList != null && UserResourcesList.Rows.Count > 0)
                {
                    //Dictionary<string, ResourceSecurityMask> userResources = new Dictionary<string, ResourceSecurityMask>();
                    List<ResourceVO> userResourcesInStores = new List<ResourceVO>();
                    foreach (DataRow dr in UserResourcesList.Rows)
                    {
                        string resourceName = Utilities.GetStringValue(dr["resourcename"], "");
                        string assignedValue = Utilities.GetStringValue(dr["assigned"], "");
                        string resourceId = Utilities.GetStringValue(dr["resourceid"], "");
                        string storeNum = Utilities.GetStringValue(dr["storenumber"], "");
                        ResourceSecurityMask resourceSecurity;
                        if (assignedValue == "Y")
                        {
                            resourceSecurity = (ResourceSecurityMask)Enum.Parse(typeof(ResourceSecurityMask), Utilities.GetStringValue(dr["securitymask"], ""));
                        }
                        else
                        {
                            resourceSecurity = (ResourceSecurityMask)Enum.Parse(typeof(ResourceSecurityMask), "NONE");
                        }
                        //userResources.Add(resourceName, resourceSecurity);
                        ResourceVO userresourcevo = new ResourceVO();
                        userresourcevo.StoreNumber = storeNum;
                        userresourcevo.ResourceID = resourceId;
                        userresourcevo.ResourceName = resourceName;
                        userresourcevo.SecurityMask = resourceSecurity;
                        userresourcevo.ResourceMask = Utilities.GetStringValue(dr["securitymask"], "");
                        //userresourcevo.ResourceData = userResources;
                        userResourcesInStores.Add(userresourcevo);
                    }
                    currUser.UserResources = userResourcesInStores;
                }
                //Add user limit list
                if (UserLimitList != null && UserLimitList.Rows.Count > 0)
                {
                    List<LimitsVO> userLimits = new List<LimitsVO>();
                    foreach (DataRow drow in UserLimitList.Rows)
                    {
                        LimitsVO userlimit = new LimitsVO();
                        userlimit.ProductId = Utilities.GetStringValue(drow["productid"], "");
                        userlimit.ServiceOffering = Utilities.GetStringValue(drow["serviceoffering"], "");
                        userlimit.Limit = Utilities.GetDecimalValue(drow["limit"], 0);
                        userlimit.StoreID = Utilities.GetStringValue(drow["storeid"], "");
                        userlimit.ProdOfferingId = Utilities.GetIntegerValue(drow["prodofferingid"], 0);
                        userlimit.StoreNumber = Utilities.GetStringValue(drow["storenumber"], "");
                        userlimit.ResourceName = Utilities.GetStringValue(drow["resourcename"], "");
                        userlimit.RoleLimitId = Utilities.GetIntegerValue(drow["id"], 0);
                        userLimits.Add(userlimit);
                    }

                    currUser.UserLimits = userLimits;
                }

                //Add store list
                if (UserStoreNumberList != null && UserStoreNumberList.Rows.Count > 0)
                {
                    List<string> storeList = new List<string>();
                    foreach (DataRow dr in UserStoreNumberList.Rows)
                    {
                        storeList.Add(Utilities.GetStringValue(dr["storenumber"], ""));
                    }
                    currUser.ProfileStores = storeList;
                }

                //Add service offerings list
                if (UserServiceOfferingList != null && UserServiceOfferingList.Rows.Count > 0)
                {
                    List<ServiceOffering> serviceOfferingList = new List<ServiceOffering>();
                    foreach (DataRow dr in UserServiceOfferingList.Rows)
                    {
                        ServiceOffering serviceOffering = new ServiceOffering();
                        serviceOffering.ProdOffering = Utilities.GetIntegerValue(dr["prodofferingid"], 0);
                        serviceOffering.ServiceOfferingID = Utilities.GetStringValue(dr["serviceoffering"], "");
                        serviceOfferingList.Add(serviceOffering);
                    }
                    desktopSession.ServiceOfferings = serviceOfferingList;
                }

                //Check if user is a shop level user and set property accordingly
                if (CanUserModifyResource(USERPROFILECURRENTLOCATION, currUser, desktopSession) &&
                    !CanUserModifyResource(USERPROFILEMULTIPLELOCATION, currUser, desktopSession))
                    currUser.ShopLevelUser = true;
                userSecurityProfile = currUser;
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Error parsing the data returned from get security profile", new ApplicationException(ex.Message));
            }
        }

        /// <summary>
        /// Retrieves the profile header of an employee
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="storeId"></param>
        /// <param name="storeNumber"></param>
        /// <param name="profileHeader"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static bool ExecuteGetEmployeeProfileHeader(
            string userName,
            string storeId,
            string storeNumber,
            out DataTable profileHeader,
            out string errorCode,
            out string errorText)
        {
            // initialize error output variables
            errorCode = String.Empty;
            errorText = String.Empty;

            // initialize data table output variables
            profileHeader = new DataTable();

            // Check the input parameters
            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(storeNumber))
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetEmployeeProfileHeader Failed",
                                                            new ApplicationException("ExecuteGetEmployeeProfileHeader Failed: No valid inputs"));
                return (false);
            }

            // Ensure the data accessor is valid
            if (GlobalDataAccessor.Instance.OracleDA == null ||
                GlobalDataAccessor.Instance.OracleDA.Initialized == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetEmployeeProfileHeader Failed",
                                                            new ApplicationException("ExecuteGetEmployeeProfileHeader Failed: Data accessor instance is invalid"));
                return (false);
            }

            // Set up input variables
            List<OracleProcParam> inParams = new List<OracleProcParam>();
            inParams.Add(new OracleProcParam("p_user_name", userName));
            inParams.Add(new OracleProcParam("p_store_id", storeId));
            inParams.Add(new OracleProcParam("p_store_number", storeNumber));

            // Set up output
            DataSet outputDataSet;
            bool retVal;
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            var refCursArr = new List<PairType<string, string>>
            {
                new PairType<string, string>("o_list_of_stores_user_works_in", "user_profile_header")
            };

            // call the sp
            try
            {
                retVal = dA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_security_profile",
                    "sp_getEmployeesProfileHeader", inParams, refCursArr,
                    "o_return_code",
                    "o_return_text", out outputDataSet);
                errorCode = dA.ErrorCode;
                errorText = dA.ErrorDescription;
            }
            catch (OracleException oEx)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetEmployeeProfileHeader Failed", oEx);
                errorCode = " --- ExecuteGetEmployeeProfileHeaderFailed";
                errorText = " --- OracleException thrown: " + oEx.Message;
                return (false);
            }

            if (retVal == false)
            {
                BasicExceptionHandler.Instance.AddException("ExecuteGetEmployeeProfileHeader Failed: return value is false", new ApplicationException());
                errorCode = dA.ErrorCode + " -- ExecuteGetEmployeeProfileHeaderFailed";
                errorText = dA.ErrorDescription + " -- Return value is false";
                return (false);
            }

            if (outputDataSet == null || !outputDataSet.IsInitialized ||
                (outputDataSet.Tables == null || outputDataSet.Tables.Count <= 0))
            {
                return (false);
            }

            // load output DataTables
            profileHeader = outputDataSet.Tables["user_profile_header"];

            return (true);
        }

        /// <summary>
        /// The user whose security profile needs to be checked is passed
        /// along with the resource that needs to be checked. If the user can
        /// view the resource name passed return value is true else false
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="userToCheck"></param>
        /// <returns></returns>
        public static bool CanUserViewResource(string resourceName, UserVO userToCheck, DesktopSession desktopSession)
        {
            //Get the actual resource name retrieved from DB for security profile
            //by calling the resource file
            string actualResourceName = Commons.GetResourceName(resourceName);
            if (actualResourceName != null)
            {
                UserVO currUserProfile = userToCheck;
                if (currUserProfile == null)
                    return false;
                if (currUserProfile.UserResources != null)
                {
                    if (currUserProfile.UserResources.Count > 0)
                    {
                        var currUserResources = (from userresourcedata in currUserProfile.UserResources
                                                 where userresourcedata.StoreNumber == desktopSession.CurrentSiteId.StoreNumber &&
                                                       userresourcedata.ResourceName == actualResourceName
                                                 select userresourcedata).FirstOrDefault();

                        //User has customized list for the store so use that

                        if (currUserResources != null)
                        {
                            if (currUserResources.SecurityMask == ResourceSecurityMask.VIEW)
                                return true;
                            return false;
                        }

                        currUserResources = (from userresourcedata in currUserProfile.UserResources
                                             where userresourcedata.StoreNumber == string.Empty &&
                                                   userresourcedata.ResourceName == actualResourceName
                                             select userresourcedata).FirstOrDefault();
                        
                        if (currUserResources != null)
                        {
                            if (currUserResources.SecurityMask == ResourceSecurityMask.VIEW)
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// The user whose security profile needs to be checked is passed
        /// along with the resource that needs to be checked. If the user can
        /// modify the resource name passed return value is true else false
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="userToCheck"></param>
        /// <returns></returns>
        public static bool CanUserModifyResource(string resourceName, UserVO userToCheck, DesktopSession desktopSession)
        {
            //Get the actual resource name retrieved from DB for security profile
            //by calling the resource file
            string actualResourceName = Commons.GetResourceName(resourceName);
            if (actualResourceName != null)
            {
                UserVO currUserProfile = userToCheck;
                if (currUserProfile == null)
                    return false;

                if (currUserProfile.UserResources != null)
                {
                    if (currUserProfile.UserResources.Count > 0)
                    {
                        var currUserResources = (from userresourcedata in currUserProfile.UserResources
                                                 where userresourcedata.StoreNumber == desktopSession.CurrentSiteId.StoreNumber && 
                                                       userresourcedata.ResourceName == actualResourceName
                                                 select userresourcedata).FirstOrDefault();

                        //User has resources set apart from the default resources
                        //so check the customized list first

                        if (currUserResources != null)
                        {
                            if (currUserResources.SecurityMask == ResourceSecurityMask.MODIFY)
                                return true;
                            return false;
                        }

                        //User did not have any access to the resource in the customized list
                        //Check the default resources to see if they have view access

                        currUserResources = (from userresourcedata in currUserProfile.UserResources
                                             where userresourcedata.StoreNumber == string.Empty &&
                                                   userresourcedata.ResourceName == actualResourceName
                                             select userresourcedata).FirstOrDefault();

                        //User has resources set apart from the default resources
                        //so check the customized list first

                        if (currUserResources != null)
                        {
                            if (currUserResources.SecurityMask == ResourceSecurityMask.MODIFY)
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// When a resource name and uservo is passed
        /// the security access that the user has on the resource
        /// is passed back as resourcesecuritymask enum value(ex:View)
        /// If the user does not have that resource the return value
        /// is the resourcesecuritymask enum value NONE.
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="userToCheck"></param>
        /// <param name="userResourceAccess"></param>
        /// <returns></returns>
        public static bool GetUserResourceAccess(string resourceName, UserVO userToCheck, DesktopSession desktopSession, out ResourceSecurityMask userResourceAccess)
        {
            //Get the actual resource name retrieved from DB for security profile
            //by calling the resource file
            string actualResourceName = Commons.GetResourceName(resourceName);
            if (actualResourceName != null)
            {
                UserVO currUserProfile = userToCheck;

                if (currUserProfile.UserResources != null)
                {
                    if (currUserProfile.UserResources.Count > 0)
                    {
                        var currUserResources = (from userresourcedata in currUserProfile.UserResources
                                                 where userresourcedata.StoreNumber == desktopSession.CurrentSiteId.StoreNumber &&
                                                       userresourcedata.ResourceName == actualResourceName
                                                 select userresourcedata).FirstOrDefault();

                        //User has resources set apart from the role resources
                        //so check the customized list first

                        if (currUserResources != null)
                        {
                            userResourceAccess = currUserResources.SecurityMask;
                            return true;
                        }
                    }
                }

                //User did not have any access to the resource in the customized list
                //Check the role's resources to see if they have any access for this resource
                if (currUserProfile.UserResources != null)
                {
                    var currUserRoleResources = (from userresourcedata in currUserProfile.UserResources
                                                 where userresourcedata.StoreNumber == string.Empty &&
                                                       userresourcedata.ResourceName == actualResourceName
                                                 select userresourcedata).FirstOrDefault();
                    if (currUserRoleResources != null)
                    {
                        userResourceAccess = currUserRoleResources.SecurityMask;
                        return true;
                    }
                }
            }
            userResourceAccess = ResourceSecurityMask.NONE;
            return false;
        }

        /// <summary>
        /// Return only the access specified for the passed in resource in the user's role's resources
        /// ignoring any changes in the customized list
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="userToCheck"></param>
        /// <param name="userRoleResourceAccess"></param>
        /// <returns></returns>
        public static bool GetDefaultResourceAccess(string resourceName, UserVO userToCheck, out ResourceSecurityMask userRoleResourceAccess)
        {
            string actualResourceName = Commons.GetResourceName(resourceName);
            if (actualResourceName != null)
            {
                UserVO currUserProfile = userToCheck;

                if (currUserProfile.UserResources != null)
                {
                    var currUserRoleResources = (from userresourcedata in currUserProfile.UserRole.RoleResources
                                                 where userresourcedata.StoreNumber == string.Empty &&
                                                       userresourcedata.ResourceName == actualResourceName
                                                 select userresourcedata).FirstOrDefault();
                    if (currUserRoleResources != null)
                    {
                        userRoleResourceAccess = currUserRoleResources.SecurityMask;
                        return true;
                    }
                }
            }
            userRoleResourceAccess = ResourceSecurityMask.NONE;
            return false;
        }

        /// <summary>
        /// When a uservo is passed, returns a list of resource names that the 
        /// user has access to(view or modify). This will be a combination of
        /// customized and role resources
        /// </summary>
        /// <param name="userToCheck"></param>
        /// <param name="storeNumber"></param>
        /// <returns></returns>
        public static List<string> GetListOfResources(UserVO userToCheck, string storeNumber)
        {
            List<string> userResourceNames = new List<string>();
            List<ResourceVO> currUserResources = new List<ResourceVO>();

            //Dictionary<string, ResourceSecurityMask> currUserResources = new Dictionary<string, ResourceSecurityMask>();
            if (userToCheck.UserResources.Count > 0)
            {
                currUserResources = (from userresourcedata in userToCheck.UserResources
                                     where userresourcedata.StoreNumber == storeNumber
                                     select userresourcedata).ToList();
            }

            List<ResourceVO> currUserRoleResources = (from userresourcedata in userToCheck.UserResources
                                                      where userresourcedata.StoreNumber == string.Empty
                                                      select userresourcedata).ToList();

            if (currUserResources.Count > 0 && currUserRoleResources.Count > 0)
            {
                //User has resources set apart from the role default resources
                //if the resource name is found in the customized list and set to none
                //then the user does not have that resource assigned to him
                foreach (ResourceVO res in currUserRoleResources)
                {
                    ResourceVO res1 = res;
                    var resObject = (from resource in currUserResources
                                     where resource.ResourceName == res1.ResourceName
                                     select resource).FirstOrDefault();
                    if (resObject != null)
                    {
                        if (resObject.SecurityMask != ResourceSecurityMask.NONE)
                            userResourceNames.Add(res.ResourceName);
                    }
                    else
                    {
                        if (res.SecurityMask != ResourceSecurityMask.NONE)
                            userResourceNames.Add(res.ResourceName);
                    }
                }
                //Go through the customized list to capture any new resources addded
                //that were not part of the role's list
                foreach (ResourceVO res in currUserResources)
                {
                    ResourceVO res2 = res;
                    string resource = (from res1 in userResourceNames
                                       where res1 == res2.ResourceName
                                       select res1).FirstOrDefault();
                    if (string.IsNullOrEmpty(resource))
                    {
                        if (res.SecurityMask != ResourceSecurityMask.NONE)
                            userResourceNames.Add(res.ResourceName);
                    }
                }
            }
            //User does not have customized list so send back all resources in the role's resource list
            else if (currUserRoleResources.Count > 0)
            {
                foreach (ResourceVO res in currUserRoleResources)
                {
                    if (res.SecurityMask != ResourceSecurityMask.NONE)
                        userResourceNames.Add(res.ResourceName);
                }
            }
            else if (currUserResources.Count > 0)
            {
                //User does not have resources assigned at the role level
                //but has resources assigned to him
                foreach (ResourceVO res in currUserResources)
                {
                    if (res.SecurityMask != ResourceSecurityMask.NONE)
                        userResourceNames.Add(res.ResourceName);
                }
            }

            return userResourceNames;
        }

        /// <summary>
        /// For the uservo object passed, returns a list of limits
        /// which will be a combination of the customized and role limits
        /// </summary>
        /// <param name="userToCheck"></param>
        /// <param name="storeNumber"></param>
        /// <returns></returns>
        public static List<LimitsVO> GetListOfLimits(UserVO userToCheck, string storeNumber)
        {
            List<LimitsVO> userLimitList = new List<LimitsVO>();
            if (userToCheck != null)
            {
                List<LimitsVO> roleLimitList = (from userlimit in userToCheck.UserLimits
                                                where userlimit.StoreNumber == string.Empty
                                                select userlimit).ToList();

                List<LimitsVO> userLimits;
                
                if (storeNumber.Length > 0)
                {
                    userLimits = (from userlimit in userToCheck.UserLimits
                                  where userlimit.StoreNumber == storeNumber
                                  select userlimit).ToList();
                }
                else
                {
                    userLimits = userToCheck.UserLimits;
                }
                if (userLimits.Count() > 0)
                {
                    userLimitList = userLimits;
                    if (roleLimitList.Count > 0)
                    {
                        foreach (LimitsVO rLimit in roleLimitList)
                        {
                            LimitsVO limit1 = rLimit;
                            var limit = (from limits in userLimits
                                         where limits.ProdOfferingId == limit1.ProdOfferingId
                                         select limits).FirstOrDefault();
                            if (limit == null)
                                userLimitList.Add(rLimit);
                        }
                    }
                }
                else
                {
                    //user has no customized list so pass back the role limit list
                    userLimitList = roleLimitList;
                }
            }

            return userLimitList;
        }

        /// <summary>
        /// Given a uservo object returns the list of unique store numbers
        /// where the user has resource permissions and limits assignment
        /// </summary>
        /// <param name="userToCheck"></param>
        /// <returns></returns>
        public static List<string> GetStoreListForUser(UserVO userToCheck)
        {
            List<string> storeNumbers = new List<string>();
            if (userToCheck != null)
            {
                //Get the list of stores where the user has resources 
                //This info is in the user's resource list
                List<string> storeNumbersInResources = new List<string>();
                if (userToCheck.UserResources.Count > 0)
                {
                    storeNumbersInResources = (
                    from resourceItem in userToCheck.UserResources 
                    where resourceItem.StoreNumber != string.Empty
                    select resourceItem.StoreNumber)
                    .Distinct().ToList();
                }

                //Get the list of stores where the user has limits
                //This info is in the user's limit list
                List<string> storeNumbersInLimits = new List<string>();
                if (userToCheck.UserLimits.Count > 0)
                {
                    storeNumbersInLimits = (
                    from resourceItem in userToCheck.UserLimits
                    where resourceItem.StoreNumber != string.Empty
                    select resourceItem.StoreNumber).Distinct().ToList();
                }
                if (storeNumbersInLimits.Count > 0)
                    storeNumbers = storeNumbersInResources.Union(storeNumbersInLimits).ToList();
                else
                    storeNumbers = storeNumbersInResources;

                storeNumbers.Add(userToCheck.FacNumber);
            }
            return storeNumbers;
        }

        /// <summary>
        /// Given a uservo will return the max amount of a loan that the user can do
        /// </summary>
        /// <param name="userToCheck"></param>
        /// <param name="storeNumber"></param>
        /// <param name="desktopSession"> </param>
        /// <param name="businessRulesProcedures"> </param>
        /// <returns></returns>
        public static decimal GetPawnLoanLimit(UserVO userToCheck, string storeNumber, DesktopSession desktopSession, IBusinessRulesProcedures businessRulesProcedures)
        {
            decimal maxLoanAmount = 0.0M;
            if (userToCheck != null)
            {
                if (userToCheck.UserLimits != null && userToCheck.UserLimits.Count != 0)
                {
                    List<LimitsVO> currentUserLimits = userToCheck.UserLimits;
                    string newpawnloanResourceName = Commons.GetResourceName(PAWNLOANIP);
                    decimal loanLimit = 0;
                    if (storeNumber.Length > 0 && currentUserLimits.Count > 0)
                    {
                        var limitData = (currentUserLimits.Where(
                            limit =>
                            limit.ProductId == PRODUCTID_FOR_PAWN &&
                            limit.ResourceName == newpawnloanResourceName &&
                            limit.StoreNumber == storeNumber)).FirstOrDefault();
                        if (limitData != null)
                            loanLimit = limitData.Limit;
                    }
                    //If the limit is still 0 meaning there is no store specific limit
                    //then get the default limit or if no store number is passed get default limit
                    if (Utilities.GetDecimalValue(loanLimit, 0) == 0 && currentUserLimits.Count > 0)
                    {
                        var limitData = (currentUserLimits.Where(
                            limit =>
                            limit.ProductId == PRODUCTID_FOR_PAWN &&
                            limit.ResourceName == newpawnloanResourceName)).FirstOrDefault();
                        if (limitData != null)
                            loanLimit = limitData.Limit;
                    }
                    
                    maxLoanAmount = Utilities.GetDecimalValue(loanLimit, 0);
                }
                //If we are here and the maxloanamt is 0 that means neither the store specific or 
                //default limit exists for new pawn loan so get the state limit
                if (maxLoanAmount == 0)
                {
                    //Get State limit
                    decimal maxLoanLimit = 0.0m;
                    if (businessRulesProcedures.GetMaxLoanLimit(desktopSession.CurrentSiteId, out maxLoanLimit))
                        maxLoanAmount = maxLoanLimit;
                }
            }
            return maxLoanAmount;
        }

        public static bool CanUserOverridePawnLoanLimit(UserVO userToCheck, decimal loanLimit, DesktopSession desktopSession, IBusinessRulesProcedures businessRulesProcedures, out decimal canLimitOverride)
        {
            canLimitOverride = 0.0m;
            if (userToCheck != null)
            {
                decimal userLoanLimit = GetPawnLoanLimit(userToCheck, userToCheck.StoreNumber, desktopSession, businessRulesProcedures);
                canLimitOverride = userLoanLimit;

                if (userLoanLimit >= loanLimit)
                    return true;
            }
            return false;
        }

        public static bool CanUserOverrideBuyLimit(UserVO userToCheck, decimal loanLimit, out decimal canLimitOverride)
        {
            canLimitOverride = 0.0m;
            if (userToCheck != null)
            {
                decimal userLoanLimit = GetPawnPurchaseLimit(userToCheck, userToCheck.StoreNumber);
                canLimitOverride = userLoanLimit;

                if (userLoanLimit >= loanLimit)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Given a uservo will return the max amount of a purchase that the user can do
        /// </summary>
        /// <param name="userToCheck"></param>
        /// <param name="storeNumber"></param>
        /// <returns></returns>
        public static decimal GetPawnPurchaseLimit(UserVO userToCheck, string storeNumber)
        {
            decimal maxPurchaseAmount = 0.0M;
            if (userToCheck != null)
            {
                if (userToCheck.UserLimits != null && userToCheck.UserLimits.Count != 0)
                {
                    List<LimitsVO> currentUserLimits = userToCheck.UserLimits;
                    string buyResourceName = Commons.GetResourceName(BUYRESOURCE);
                    decimal purchaseLimit = 0;
                    if (storeNumber.Length > 0 && currentUserLimits.Count > 0)
                    {
                        var limitData = (currentUserLimits.Where(
                            limit =>
                            limit.ProductId == PRODUCTID_FOR_BUY &&
                            limit.ResourceName == buyResourceName &&
                            limit.StoreNumber == storeNumber)).FirstOrDefault();
                        if (limitData != null)
                            purchaseLimit = limitData.Limit;
                    }
                    //If the limit is still 0 meaning there is no store specific limit
                    //then get the default limit or if no store number is passed get default limit
                    if (Utilities.GetDecimalValue(purchaseLimit, 0) == 0 && currentUserLimits.Count > 0)
                    {
                        var limitData = (currentUserLimits.Where(
                            limit =>
                            limit.ProductId == PRODUCTID_FOR_BUY &&
                            limit.ResourceName == buyResourceName)).FirstOrDefault();
                        if (limitData != null)
                            purchaseLimit = limitData.Limit;
                    }

                    maxPurchaseAmount = Utilities.GetDecimalValue(purchaseLimit, 0);
                }
                //If we are here and the maxloanamt is 0 that means neither the store specific or 
                //default limit exists for this user so return the max limit which is 99999.
                if (maxPurchaseAmount == 0)
                {
                    maxPurchaseAmount = 99999;
                }
            }
            return maxPurchaseAmount;
        }

        public static decimal GetRetailDiscountPercentLimit(UserVO userToCheck, string storeNumber)
        {
            decimal retailDiscountPercentLimit = 0.0M;
            if (userToCheck != null)
            {
                if (userToCheck.UserLimits != null && userToCheck.UserLimits.Count != 0)
                {
                    List<LimitsVO> currentUserLimits = userToCheck.UserLimits;
                    string discountPercentResourceName = Commons.GetResourceName(RETAILDISCOUNTPERCENT);
                    decimal purchaseLimit = 0;
                    if (storeNumber.Length > 0 && currentUserLimits.Count > 0)
                    {
                        var limitData = (currentUserLimits.Where(
                            limit =>
                            limit.ResourceName == discountPercentResourceName && 
                            limit.StoreNumber == storeNumber)).FirstOrDefault(); 
                        if (limitData != null)
                            purchaseLimit = limitData.Limit;
                    }
                    //If the limit is still 0 meaning there is no store specific limit
                    //then get the default limit or if no store number is passed get default limit
                    if (Utilities.GetDecimalValue(purchaseLimit, 0) == 0 && currentUserLimits.Count > 0)
                    {
                        var limitData = (currentUserLimits.Where(
                            limit =>
                            limit.ResourceName == discountPercentResourceName)).FirstOrDefault();
                        if (limitData != null)
                            purchaseLimit = limitData.Limit;
                    }

                    retailDiscountPercentLimit = Utilities.GetDecimalValue(purchaseLimit, 0);
                }
                //If we are here and the maxloanamt is 0 that means neither the store specific or 
                //default limit exists for this user so return the max limit which is 99999.
                if (retailDiscountPercentLimit == 0)
                {
                    retailDiscountPercentLimit = 99999;
                }
            }
            return retailDiscountPercentLimit;
        }

        public static int GetLayawayNumberOfPaymentsLimit(UserVO userToCheck, string storeNumber)
        {
            int numberOfPayments = 0;
            if (userToCheck != null)
            {
                if (userToCheck.UserLimits != null && userToCheck.UserLimits.Count != 0)
                {
                    List<LimitsVO> currentUserLimits = userToCheck.UserLimits;
                    string numberOfPaymentsLimitResourceName = Commons.GetResourceName(LAYAWAY_NUMBER_INCREMENT_LIMIT);
                    if (storeNumber.Length > 0 && currentUserLimits.Count > 0)
                    {
                        var limitData = (currentUserLimits.Where(
                            limit =>
                            limit.ResourceName == numberOfPaymentsLimitResourceName && 
                            limit.StoreID == storeNumber)).FirstOrDefault();
                        //&& limit.StoreNumber == storeNumber)).FirstOrDefault();
                        if (limitData != null)
                            numberOfPayments = Utilities.GetIntegerValue(limitData.Limit, 0);
                    }
                    //If the limit is still 0 meaning there is no store specific limit
                    //then get the default limit or if no store number is passed get default limit
                    if (numberOfPayments == 0 && currentUserLimits.Count > 0)
                    {
                        var limitData = (currentUserLimits.Where(
                            limit =>
                            limit.ResourceName == numberOfPaymentsLimitResourceName)).FirstOrDefault();
                        if (limitData != null)
                            numberOfPayments = Utilities.GetIntegerValue(limitData.Limit, 0);
                    }
                }
                //If we are here and the numberOfPayments is 0 that means neither the store specific or 
                //default limit exists for this user so return the max limit which is 99999.
                if (numberOfPayments == 0)
                {
                    numberOfPayments = 99999;
                }
            }
            return numberOfPayments;
        }

        public static void ModifyButtonAccessBasedOnSecurityProfile(
            Control.ControlCollection buttons, 
            UserVO user, 
            string btnSuffix, 
            string tagSep, 
            string leafStr,
            DesktopSession desktopSession)
        {
            //Short circuit if we have invalid inputs
            if (buttons == null || user == null || buttons.Count <= 0 || string.IsNullOrEmpty(btnSuffix))
            {
                return;
            }

            //Determine check all flag
            bool checkAll = (string.IsNullOrEmpty(tagSep) && string.IsNullOrEmpty(leafStr));

            //Check if user has access to buttons in the list
            foreach (Control ctl in buttons)
            {
                if ((ctl == null) || (!(ctl is Button)) || (!ctl.Enabled) || ctl.Tag == null)
                {
                    continue;
                }

                var idx = ctl.Name.IndexOf(btnSuffix, StringComparison.OrdinalIgnoreCase);
                if (idx < 0)
                {
                    continue;
                }

                //Acquire the button name
                var btnName = ctl.Name.Substring(0, idx);
                if (string.IsNullOrEmpty(btnName))
                {
                    continue;
                }

                //Change the name to upper case
                btnName = btnName.ToUpper();

                //Only check leaf buttons
                if (!checkAll)
                {
                    var tagStr = ctl.Tag.ToString();
                    if (string.IsNullOrEmpty(tagStr) || string.IsNullOrEmpty(tagSep))
                    {
                        continue;
                    }

                    //Get pipe index from the tag
                    idx = tagStr.IndexOf(tagSep);
                    var leafFxn = tagStr.Substring(idx + 1);

                    //Only allow leaf buttons to go through (the buttons that lead to specific flows of functionality)
                    if (string.IsNullOrEmpty(leafFxn) || !leafFxn.Equals(leafStr, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    //Set enabled flag of control
                    ctl.Enabled = SecurityProfileProcedures.CanUserViewResource(btnName, user, desktopSession);
                }
                //Otherwise, check all buttons
                else
                {
                    //Set enabled flag of control
                    ctl.Enabled = SecurityProfileProcedures.CanUserViewResource(btnName, user, desktopSession);
                }

            }
        }
    }
}