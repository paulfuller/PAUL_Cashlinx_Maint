using System;
using NAnt.Core;
using NAnt.Core.Attributes;
using System.Runtime.InteropServices;
using System.Text;

namespace Cashlinx.Build.Tasks
{
    [FunctionSet("Cashlinx", "Cashlinx")]
    public class UtilityFunctions : FunctionSetBase
    {
        [DllImport("secur32.dll", CharSet = CharSet.Auto)]
        public static extern int GetUserNameEx(EXTENDED_NAME_FORMAT nameFormat, StringBuilder userName,
           ref uint userNameSize);

        public enum EXTENDED_NAME_FORMAT
        {
            NameUnknown = 0,
            NameFullyQualifiedDN = 1,
            NameSamCompatible = 2,
            NameDisplay = 3,
            NameUniqueId = 6,
            NameCanonical = 7,
            NameUserPrincipal = 8,
            NameCanonicalEx = 9,
            NameServicePrincipal = 10,
            NameDnsDomain = 12
        }

        public UtilityFunctions(Project project, PropertyDictionary properties)
            : base(project, properties)
        {
        }

        [Function("GetUserFullName")]
        public string GetUserFullName()
        {
            StringBuilder sb = new StringBuilder(1024);
            uint size = (uint)sb.Capacity;
            GetUserNameEx(EXTENDED_NAME_FORMAT.NameDisplay, sb, ref size);
            string username = sb.ToString();

            string[] parts = username.Split(new char[] { ',', ' ' });
            
            if (parts.Length == 3)
            {
                return parts[2] + " " + parts[0];
            }
            else
            {
                return username;
            }
        }

        [Function("GetUserEmailAddress")]
        public string GetUserEmailAddress()
        {
            StringBuilder username = new StringBuilder(1024);
            uint size = (uint)username.Capacity;
            GetUserNameEx(EXTENDED_NAME_FORMAT.NameUserPrincipal, username, ref size);
            return username.ToString();
        }

        [Function("FormatDateTime")]
        public string FormatDateTime(DateTime date, string format)
        {
            if (!string.IsNullOrEmpty(format))
            {
                return date.ToString(format);
            }
            return date.ToString();
        }

        [Function("MatchesDeployPassword")]
        public bool MatchesDeployPassword(string password)
        {
            if (password == null)
            {
                return false;
            }

            return password.Equals("clxPhase22011");
        }
    }
}
