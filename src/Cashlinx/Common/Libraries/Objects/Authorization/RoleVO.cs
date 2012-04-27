using System;
using System.Collections.Generic;
using System.Xml;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Authorization
{
    public class RoleVO
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<ResourceVO> RoleResources;
        public List<LimitsVO> RoleLimits;


        /// <summary>
        /// For the roleVO object calling this method passing a role name
        /// will return either EQUAL,GREATER or LESSER depending on
        /// whether the passed in role's level is equal,greater or lesser
        /// to the level of the current role object's role. If it cannot be determined
        /// NONE is passed back
        /// </summary>
        /// <param name="roleNameToCheck"></param>
        /// <returns></returns>
        public ROLEHIERARCHYLEVEL CheckRoleHierarchy(string roleNameToCheck)
        {

            try
            {
                //To do: fix the path of the file
                //Right now its in debug folder..should be in resources
                //string filePath = Assembly.GetEntryAssembly().GetName().Name;
                const string fileName = "RoleHierarchy.xml";
                var doc = new XmlDocument();
                doc.Load(fileName);
                XmlNodeList roleDataList = doc.GetElementsByTagName("Role");
                int roleLevel = 0;
                int roleToCheckRoleLevel = 0;
                if (RoleName == roleNameToCheck)
                    return ROLEHIERARCHYLEVEL.EQUAL;
                foreach (XmlNode node in roleDataList)
                {
                    var roleNameElement = (XmlElement)node;
                    string roleName = roleNameElement.GetElementsByTagName("Tag")[0].InnerText;
                    //Get the role level of the current object
                    if (roleName == RoleName)
                    {
                        roleLevel = Utilities.GetIntegerValue(roleNameElement.GetElementsByTagName("Value")[0].InnerText);
                    }
                    else if (roleName == roleNameToCheck)
                    {
                        //Get the role level of the role name passed in
                        roleToCheckRoleLevel = Utilities.GetIntegerValue(roleNameElement.GetElementsByTagName("Value")[0].InnerText);
                    }
                    if (roleLevel > 0 && roleToCheckRoleLevel > 0)
                        break;


                }
                if (roleLevel > roleToCheckRoleLevel)
                    return ROLEHIERARCHYLEVEL.GREATER;
                if (roleLevel < roleToCheckRoleLevel)
                    return ROLEHIERARCHYLEVEL.LESSER;
                if (roleLevel == roleToCheckRoleLevel)
                    return ROLEHIERARCHYLEVEL.EQUAL;

                return ROLEHIERARCHYLEVEL.NONE;
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Cannot check role level", new ApplicationException(ex.Message));
                return ROLEHIERARCHYLEVEL.NONE;
            }
        }
    }

}
