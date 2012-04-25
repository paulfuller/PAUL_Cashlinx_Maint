using System;
using System.Collections.Generic;
using System.Linq;
using Common.Libraries.Utility.Collection;

namespace Common.Libraries.Objects.Authorization
{
    /// <summary>
    /// Simple VO class for storing password policy values retrieved
    /// from the LDAP server and/or some other authentication directory
    /// </summary>
    public class PasswordPolicyVO
    {
        public Int16 MaxRepeatedChars { set; get; }
        public Int16 MinAlphaChars { set; get; }
        public Int16 MinDiffChars { set; get; }
        public Int16 MinOtherChars { set; get; }
        public Boolean AllowUserChange { set; get; }
        public String AttributeName { set; get; }
        public Int16 CheckSyntax { set; get; }
        public Int64 ExpireWarningSeconds { set; get; }
        public Int64 FailureCountIntervalSeconds { set; get; }
        public Int64 GraceLoginLimit { set; get; }
        public Int16 InHistoryCount { set; get; }
        public Boolean LockoutEnabled { set; get; }
        public Int64 LockoutDurationMillis { set; get; }
        public Int64 MaxAgeSeconds { set; get; }
        public Int64 MinAgeSeconds { set; get; }
        public Int16 MaxFailedLoginAttempts { set; get; }
        public Int16 MinLength { set; get; }
        public Boolean MustChange { set; get; }
        public Boolean SafeModify { set; get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PasswordPolicyVO()
        {
            MaxRepeatedChars = 0;
            MinAlphaChars = 0;
            MinDiffChars = 0;
            MinOtherChars = 0;
            AllowUserChange = true;
            AttributeName = string.Empty;
            CheckSyntax = 0;
            ExpireWarningSeconds = 0L;
            FailureCountIntervalSeconds = 0L;
            GraceLoginLimit = 0L;
            InHistoryCount = 0;
            LockoutEnabled = false;
            LockoutDurationMillis = 0L;
            MaxAgeSeconds = 0L;
            MinAgeSeconds = 0L;
            MaxFailedLoginAttempts = 0;
            MinLength = 0;
            MustChange = false;
            SafeModify = false;
        }

        public bool IsExpired(DateTime pwdAge,DateTime shopDate)
        {
            //Check TimeSpan(ShopDateTime - pwdAge) against MaxAgeMillis
            return (shopDate - pwdAge).TotalSeconds > MaxAgeSeconds;
        }

        public bool IsExpiredWarning(DateTime pwdAge,DateTime shopDate)
        {
            //Check pwdAge against ExpireWarningMillis
            //if the time left for the password to expire
            //is less than or equal to the expirewarning milliseconds 
            //then we need to start showing 
            //the warning message to the user
            var pwdExpiryDate=pwdAge.AddSeconds(MaxAgeSeconds);
            var millisecondLeftForPwdExpiry=(pwdExpiryDate-shopDate).TotalSeconds;
            return millisecondLeftForPwdExpiry <= ExpireWarningSeconds;
        }
        
        /// <summary>
        /// Validates the password against a set of rules for password strength
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="pastPwds"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public bool IsValid(string pwd, List<string> pastPwds, out string errMsg)
        {
            errMsg = string.Empty;
            var valid = false;
            if (!string.IsNullOrEmpty(pwd))
            {
                var matchesLength = (pwd.Length >= MinLength);
                var matchesAlpha = pwd.Count(Char.IsLetter) >= MinAlphaChars;
                var matchesOther = pwd.Count(x => (Char.IsDigit(x) || 
                                                    Char.IsSymbol(x))) >= MinOtherChars;
                var maxCnt = 0;
                var letterCnts = new Dictionary<char, int>();
                const int startingCount = 1;
                foreach (var c in pwd)
                {
                    //If the letter has not yet been mapped,
                    //initialize it with a count of one in the map
                    if (!letterCnts.ContainsKey(c))
                    {
                        letterCnts.Add(c, startingCount);
                        if (maxCnt <= startingCount)
                        {
                            maxCnt = startingCount;
                        }
                    }
                        //The letter was found, increment its count
                        //in the map
                    else
                    {                       
                        var iC = letterCnts[c];
                        ++iC;
                        letterCnts[c] = iC;
                        if (maxCnt <= iC)
                        {
                            maxCnt = iC;
                        }
                    }
                }

                //Out of all of the letters which repeat (maximum found stored in maxCnt),
                //determine if that count exceeds the rule value for maximum repeated characters
                var matchesRepeat = maxCnt <= MaxRepeatedChars;
                var inHistoryPwd = false;
                if (CollectionUtilities.isNotEmpty(pastPwds))
                {
                    inHistoryPwd = pastPwds.Contains(pwd);
                }

                valid = matchesLength && matchesAlpha && matchesOther && matchesRepeat && !inHistoryPwd;
                if (!matchesLength)
                {
                    errMsg = string.Format("{0}Password does not meet the minimum length requirements. ", errMsg);
                }
                if (!matchesAlpha)
                {
                    errMsg = string.Format("{0}Password does not contain enough alphabetic characters. ", errMsg);
                }
                if (!matchesOther)
                {
                    errMsg = string.Format("{0}Password does not contain enough non-alphabetic characters. ", errMsg);
                }
                if (!matchesRepeat)
                {
                    errMsg = string.Format("{0}Password contains characters which repeat more times than allowed. ", errMsg);
                }
                if (inHistoryPwd)
                {
                    errMsg = string.Format("{0}Password cannot match your most recent {1} passwords.", errMsg, InHistoryCount);
                }
            }
            else
            {
                errMsg = string.Format("{0}Password cannot be empty or null. ", errMsg);
            }

            if (!valid)
            {
                var criteriaStr = errMsg + Environment.NewLine + 
                    " New passwords must meet the following criteria:" +
                    Environment.NewLine +
                    "-- Minimum Required Length  : " + MinLength +
                    Environment.NewLine +
                    "-- Minimum Alpha Characters : " + MinAlphaChars +
                    Environment.NewLine +
                    "-- Minimum Other Characters : " + MinOtherChars +
                    Environment.NewLine +
                    "-- Maximum Repeat Characters: " + MaxRepeatedChars +
                    Environment.NewLine +
                    "-- Your new password cannot match your past " + InHistoryCount +
                    " passwords.";
            }

            return (valid);
        }


    }
}
