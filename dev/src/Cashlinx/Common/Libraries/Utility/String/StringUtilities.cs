using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Common.Libraries.Utility.Type;

namespace Common.Libraries.Utility.String
{
    public static class StringUtilities
    {
        private const int IPADDRESS_MAXVALUE = 255;
        private const int MAC_ADDRESS_ITERATELENGTH = 3;
        private const string HEX_CODE = "x";
        private const string FULLHEX_CODE = "x2";
        private const int MAC_ADDRESS_LENGTH = 17;
        private const char MAC_ADDRESS_COLON = ':';
        private const int MAC_ADDRESS_NUMCOLONS = 5;
        private const int MAC_ADDRESS_VALIDENTRYCOUNT = 6;
        private const char SPLIT_SPACER = '.';
        private const int IPADDRESS_NUMBERPERIODS = 3;
        private const char FIRST_VALIDHEXCHAR = '0';
        private const char LAST_VALIDHEXCHAR = 'f';
        private const int IPADDRESS_MINLENGTH = 7;
        private const int IPADDRESS_MAXLENGTH = 15;
        private const int IPADDRESS_MINVALUE = 0;
        private const int IPADDRESS_NUMVALIDENTRIES = 4;

        public enum MaxTimeResolution
        {
            MILLISECONDS,
            SECONDS,
            MINUTES,
            HOURS,
            DAYS
        }

        public enum TimeFormat
        {
            WHOLE,
            WHOLEUNIT,
            FRACTION,
            FRACTIONUNIT,
            WHOLEFRACTION,
            WHOLEFRACTIONUNIT
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool isEmpty(string s)
        {
            if(s == null || s.Length <= 0) return (true);
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool isNotEmpty(string s)
        {
            if(!string.IsNullOrEmpty(s)) return (true);
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theValue"></param>
        /// <returns></returns>
        public static bool IsDate(string theValue)
        {
            DateTime dT;
            return (DateTime.TryParse(theValue, out dT));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theValue"></param>
        /// <returns></returns>
        public static bool IsDecimal(string theValue)
        {
            decimal d;
            return (decimal.TryParse(theValue, out d));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theValue"></param>
        /// <returns></returns>
        public static bool IsFloat(string theValue)
        {
            float f;
            return (float.TryParse(theValue, out f));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theValue"></param>
        /// <returns></returns>
        public static bool IsInteger(string theValue)
        {
            int i;
            return (int.TryParse(theValue, out i));
        }
 
        /// <summary>
        /// Returns whether or not the string is a valid
        /// MAC address (Format: XX:XX:XX:XX:XX:XX)
        /// </summary>
        /// <param name="theValue"></param>
        /// <returns></returns>
        public static bool IsMACAddress(string theValue)
        {
            if (string.IsNullOrEmpty(theValue))
                return (false);

            //Every mac address must be of the same length
            //Two characters plus spacer for all entries except the last = 17
            if (theValue.Length != MAC_ADDRESS_LENGTH)
                return (false);

            var strArray = theValue.ToLower().ToCharArray();
            var colonCount = strArray.Count(c => c == MAC_ADDRESS_COLON);
            if (colonCount != MAC_ADDRESS_NUMCOLONS)
            {
                return (false);
            }
            var validEntriesFound = 0;
            for (var i = 0; i < strArray.Length; i += MAC_ADDRESS_ITERATELENGTH)
            {
                var firstChar = strArray[i];
                var secondChar = strArray[i + 1];
                var thirdChar = strArray[i + 2];
                if ((firstChar >= FIRST_VALIDHEXCHAR && firstChar <= LAST_VALIDHEXCHAR) &&
                    (secondChar >= FIRST_VALIDHEXCHAR && secondChar <= LAST_VALIDHEXCHAR) &&
                    (thirdChar == MAC_ADDRESS_COLON))
                {
                    validEntriesFound++;
                }
            }

            return (validEntriesFound == MAC_ADDRESS_VALIDENTRYCOUNT);
        }

        /// <summary>
        /// Returns whether or not the string is a valid
        /// IPv4 address (Format: XXX.XXX.XXX.XXX)
        /// </summary>
        /// <param name="theValue"></param>
        /// <returns></returns>
        public static bool ISIPv4Address(string theValue)
        {
            if (string.IsNullOrEmpty(theValue))
                return (false);

            //Minimum length is 1 character plus period per entry, except for the last = 7
            //Maximum length is 3 characters plus period per entry, except for the last = 15
            if (theValue.Length < IPADDRESS_MINLENGTH || 
                theValue.Length > IPADDRESS_MAXLENGTH)
                return (false);

            var strArray = theValue.ToLower().ToCharArray();
            var periodCount = strArray.Count(c => c == SPLIT_SPACER);
            if (periodCount != IPADDRESS_NUMBERPERIODS)
            {
                return (false);
            }
            var validEntriesFound = 0;
            var nextPerIdx = theValue.IndexOf(SPLIT_SPACER);
            var curIdx = 0;

            while(nextPerIdx != -1)
            {
                var curSubStr = theValue.Substring(curIdx, (nextPerIdx - curIdx));
                int outNum;
                
                if (Int32.TryParse(curSubStr, out outNum) &&
                    outNum >= IPADDRESS_MINVALUE && outNum <= IPADDRESS_MAXVALUE)
                {
                    validEntriesFound++;
                }
                curIdx = nextPerIdx + 1;
                nextPerIdx = theValue.IndexOf(SPLIT_SPACER, nextPerIdx + 1);

                if (nextPerIdx == -1 && curIdx < theValue.Length)
                {
                    if (Int32.TryParse(curSubStr, out outNum) &&
                        outNum >= IPADDRESS_MINVALUE && outNum <= IPADDRESS_MAXVALUE)
                    {
                        validEntriesFound++;
                        break;
                    }
                }
            }

            return (validEntriesFound == IPADDRESS_NUMVALIDENTRIES);
        }

        public static string EscapePath(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return (s);
            }
            //First check to see if the path ends with a path separator
            int pathSepLastIdx = s.LastIndexOf('\\');
            if (pathSepLastIdx < 0 || pathSepLastIdx != s.Length - 1)
            {
                return (s);
            }

            //If it does, make sure that it is not already part of an escaped sequence
            if (pathSepLastIdx - 1 < 0)
            {
                return (s);
            }
            char prePathSepLastChar = s[pathSepLastIdx - 1];
            //If the previous character is a backslash, then the end slash is already escaped
            //properly
            if (prePathSepLastChar == '\\')
            {
                return (s);
            }

            //If we are here, just add a slash to the end of the string and the path
            //should be escaped properly
            return (s + "\\");
        }
        
        /// <summary>
        /// Joins a list of strings together using the specified separator
        /// </summary>
        /// <param name="input"></param>
        /// <param name="sep"></param>
        /// <returns></returns>
        public static string joinListIntoString(List<string> input, string sep)
        {
            if (input == null || input.Count <= 0 || string.IsNullOrEmpty(sep))
            {
                return (string.Empty);
            }

            //Create joint aliases input string
            var joiner = new StringBuilder();
            int innerLen = input.Count - 1;
            for (int i = 0; i < input.Count; ++i)
            {
                string s = input[i];
                if (string.IsNullOrEmpty(s))
                    continue;

                joiner.Append(s);
                if (i < innerLen)
                {
                    joiner.Append(sep);
                }
            }            

            //Return result
            return (joiner.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origStr"></param>
        /// <param name="toRemoveStr"></param>
        /// <returns></returns>
        public static string removeFromString(string origStr, string toRemoveStr)
        {
            if (string.IsNullOrEmpty(origStr) || string.IsNullOrEmpty(toRemoveStr))
            {
                return (origStr);
            }
            var rt = origStr.Replace(toRemoveStr, string.Empty);
            return(rt);
        }

        /// <summary>
        /// Retrieves all indices of specified characters from input string
        /// </summary>
        /// <param name="str">Input string</param>
        /// <param name="searchChars">Characters to search for</param>
        /// <param name="indices">Indices found, if any</param>
        /// <returns>Whether or not any indices were found</returns>
        public static bool retrieveIndicesOfCharsOrig(string str, char[] searchChars, ref List<int> indices)
        {
            if (string.IsNullOrEmpty(str) || searchChars == null || searchChars.Length <= 0 || indices == null)
            {
                return (false);
            }
            int cnt = 0;
            for (int j = 0; j < str.Length; ++j)
            {
                char curStrChar = str[j];
                for (int k = 0; k < searchChars.Length; ++k)
                {
                    char curSearchChar = searchChars[k];    
                    if (curStrChar == curSearchChar)
                    {
                        indices.Add(j);
                        ++cnt;
                    }
                }
            }
            return (cnt > 0);
        }

        /// <summary>
        /// Splits a long string based on valid split characters with the 
        /// added constraint that no split may exceed the split length
        /// </summary>
        /// <param name="str">String to split</param>
        /// <param name="splitLength">Maximum split length</param>
        /// <param name="splitChars">Characters to split across</param>
        /// <param name="splits">List of split strings</param>
        /// <returns>Whether or not the string was split</returns>
        public static bool splitLongStringOrig(string str, int splitLength, char[] splitChars, out List<string> splits)
        {
            //Setup output
            splits = new List<string>();

            //Validate inputs
            if (string.IsNullOrEmpty(str) || splitChars == null || splitChars.Length <= 0)
            {
                return (false);
            }

            //See if input string is shorter than split length
            if (str.Length <= splitLength)
            {
                //Add it to the output list and return true
                splits.Add(str);
                return (true);
            }

            //Otherwise, process the list
            int strLength = str.Length;
            var splitIndices = new List<int>();
            if (!retrieveIndicesOfCharsOrig(str, splitChars, ref splitIndices))
            {
                return (false);
            }

            int actualStrIdx = 0;
            int projectedSplitIdx = splitLength;
            int curSplitIdx = 0;
            for (int j = 0; j < splitIndices.Count; ++j)
            {
                //Set the last split index to the
                //value held in current split index, prior to
                //updating current split
                int lastSplitIdx = curSplitIdx;
                curSplitIdx = splitIndices[j];

                //If the current split is on or passed the projection,
                //we need to compute and extract the next split
                if ((curSplitIdx >= projectedSplitIdx) ||
                    ((j == splitIndices.Count - 1) && (curSplitIdx <= projectedSplitIdx)))
                {
                    //If the current split is past the projected limit
                    //back the current split to the last found split index
                    if (curSplitIdx > projectedSplitIdx)
                    {
                        curSplitIdx = lastSplitIdx;
                        --j;
                    }
                    //Compute the length of the split
                    int captureLength = (curSplitIdx - actualStrIdx) + 1;

                    //If the split exceeds the split length,
                    //given that we have potentially gone back to the last index,
                    //kill the loop
                    if (captureLength > splitLength && str[curSplitIdx] != ' ')
                        break;

                    //Split the string 
                    string curSplitStr = str.Substring(actualStrIdx, captureLength);

                    //Add the split to the output list
                    splits.Add(curSplitStr);

                    //Update the critical index values
                    actualStrIdx += captureLength;
                    lastSplitIdx = actualStrIdx - 1;
                    projectedSplitIdx = lastSplitIdx + splitLength;

                    //If our new projections puts us past the end of the string
                    //cut the last portion of the string off, add it as a split,
                    //and kill the loop
                    if (projectedSplitIdx > strLength)
                    {
                        string lastSplitStr = str.Substring(actualStrIdx, (strLength - actualStrIdx));
                        splits.Add(lastSplitStr);
                        break;
                    }
                }
            }

            return (splits.Count > 0);
        }

        /// <summary>
        /// Retrieves all indices of specified characters from input string
        /// </summary>
        /// <param name="str">Input string</param>
        /// <param name="searchChars">Characters to search for</param>
        /// <param name="indices">Indices found, if any</param>
        /// <returns>Whether or not any indices were found</returns>
        public static bool RetrieveIndicesOfChars(string str, char[] searchChars, ref List<int> indices)
        {
            if (string.IsNullOrEmpty(str) || searchChars == null || searchChars.Length <= 0 || indices == null)
            {
                return (false);
            }
            int cnt = 0;
            for (int j = 0; j < str.Length; ++j)
            {
                char curStrChar = str[j];
                for (int k = 0; k < searchChars.Length; ++k)
                {
                    char curSearchChar = searchChars[k];
                    if (curStrChar == curSearchChar)
                    {
                        //If a period is being utilized as a search character, do not allow any numbers to be compromised
                        if (curSearchChar == '.' && j > 0 &&
                            Char.IsDigit(str[j - 1]) && Char.IsDigit(str[j + 1]))
                        {
                            continue;
                        }
                        indices.Add(j);
                        ++cnt;
                    }
                }
            }
            return (cnt > 0);
        }

        /// <summary>
        /// Takes all occurrences of "compress" target from the string and replaces them with
        /// the "compress" result.  Useful if you want to transform, for example, "; " into ";"
        /// or "  " into " "
        /// </summary>
        /// <param name="str"></param>
        /// <param name="compressTarget"></param>
        /// <param name="compressResult"></param>
        /// <returns></returns>
        public static string CompressString(string str, string compressTarget, string compressResult)
        {
            return (str.Replace(compressTarget, compressResult));
        }

        /// <summary>
        /// Splits a long string based on valid split characters with the 
        /// added constraint that no split may exceed the split length
        /// </summary>
        /// <param name="str">String to split</param>
        /// <param name="splitLength">Maximum split length</param>
        /// <param name="splitChars">Characters to split across</param>
        /// <param name="splits">List of split strings</param>
        /// <returns>Whether or not the string was split</returns>
        public static bool SplitLongString(string str, int splitLength, char[] splitChars, out List<string> splits)
        {
            //Setup output
            splits = new List<string>();

            //Validate inputs
            if (string.IsNullOrEmpty(str) || splitChars == null || splitChars.Length <= 0)
            {
                return (false);
            }

            //See if input string is shorter than split length
            if (str.Length <= splitLength)
            {
                //Add it to the output list and return true
                splits.Add(str);
                return (true);
            }

            //Otherwise, process the list
            var splitIndices = new List<int>();
            if (!RetrieveIndicesOfChars(str, splitChars, ref splitIndices))
            {
                return (false);
            }

            int actualStrIdx = 0;
            int projectedSplitIdx = splitLength;
            int curSplitIdx = 0;
            for (int j = 0; j < splitIndices.Count; ++j)
            {
                //Set the last split index to the
                //value held in current split index, prior to
                //updating current split
                int lastSplitIdx = curSplitIdx;
                curSplitIdx = splitIndices[j];

                //If the current split is on or passed the projection,
                //we need to compute and extract the next split
                if ((curSplitIdx >= projectedSplitIdx) ||
                    ((j == splitIndices.Count - 1) && (curSplitIdx <= projectedSplitIdx)))
                {
                    //If the current split is past the projected limit
                    //back the current split to the last found split index
                    if (curSplitIdx > projectedSplitIdx)
                    {
                        curSplitIdx = lastSplitIdx;
                        --j;
                    }
                    //Compute the length of the split
                    int captureLength = (curSplitIdx - actualStrIdx) + 1;

                    //If the split exceeds the split length,
                    //given that we have potentially gone back to the last index,
                    //kill the loop
                    if (captureLength > splitLength && (!splitChars.Contains(str[curSplitIdx])))
                    {
                        break;
                    }

                    //If we've exceeded the split length, reverse until we find a better
                    //split point or we run out of split points
                    while (captureLength > splitLength && j >= 1)
                    {
                        //Adjust the current split index and recalculate the capture length
                        --j;
                        curSplitIdx = splitIndices[j];
                        captureLength = (curSplitIdx - actualStrIdx) + 1;
                    }

                    //Split the string 
                    string curSplitStr = str.Substring(actualStrIdx, captureLength);

                    //Remove any beginning space as each string starts a new line
                    //but DO NOT adjust the actual string index as you will lose
                    //string data
                    if (curSplitStr.StartsWith(" "))
                    {
                        curSplitStr = curSplitStr.Substring(1, curSplitStr.Length - 1);
                    }

                    //Add the split to the output list
                    splits.Add(curSplitStr);

                    //Update the critical index values
                    actualStrIdx += captureLength;
                    lastSplitIdx = actualStrIdx - 1;
                    projectedSplitIdx += splitLength;
                }
            }

            return (splits.Count > 0);
        }


        /// <summary>
        /// Returns a string filled with the specified pattern a
        /// specified number of times
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="numTimes"></param>
        /// <returns></returns>
        public static string fillString(string pattern, int numTimes)
        {
            if (numTimes <= 0 || string.IsNullOrEmpty(pattern))
            {
                return (string.Empty);
            }

            var sb = new StringBuilder();
            for (int i = 0; i < numTimes; ++i)
            {
                sb.Append(pattern);
            }

            return (sb.ToString());
        }

        /// <summary>
        /// Creates a new string of specified width with data centered
        /// </summary>
        /// <param name="data"></param>
        /// <param name="lineWidth"></param>
        /// <returns></returns>
        public static string centerString(string data, int lineWidth)
        {
            if (string.IsNullOrEmpty(data))
            {
                return (string.Empty);
            }

            if (lineWidth < data.Length || lineWidth <= 1)
            {
                return (data);
            }

            var sb = new StringBuilder();
            int halfLineWidth = lineWidth / 2;
            int spacingLength = halfLineWidth / 2;
            if (halfLineWidth == 1)
            {
                spacingLength = 1;
            }
            string spacingStr = StringUtilities.fillString(" ", spacingLength);
            sb.Append(spacingStr);
            sb.Append(data);
            sb.Append(spacingStr);
            return (sb.ToString());
        }

        /// <summary>
        /// Creates a new string of specified width with data centered.
        /// This version allows you to clip the data if it exceeds the
        /// given line width
        /// </summary>
        /// <param name="data"></param>
        /// <param name="lineWidth"></param>
        /// <param name="clip"></param>
        /// <returns></returns>
        public static string centerString(string data, int lineWidth, bool clip)
        {
            string dataStr = data;
            if (clip)
            {
                if (lineWidth <= data.Length)
                {
                    //Clip down by 4 characters to allow centering
                    dataStr = data.Substring(0, lineWidth-4);
                }
            }

            return (centerString(dataStr, lineWidth));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="splitChar"></param>
        /// <param name="components"></param>
        /// <returns></returns>
        public static bool SplitFractionIntoComponents(string data, char splitChar, out PairType<string, string> components)
        {
            components = new PairType<string, string>(string.Empty, string.Empty);    
            if (string.IsNullOrEmpty(data)) return(false);
            int splitIdx = data.IndexOf(splitChar);
            if (splitIdx == -1) return(false);
            string[] splits = 
                data.Split(new[]
                           {
                               splitChar
                           }, 2);

            if (splits.Length == 2)
            {
                components.Left = splits[0];
                components.Right = splits[1];
                return (true);
            }
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="res"></param>
        /// <param name="fmt"></param>
        /// <returns></returns>
        public static string TimeSpanToString(TimeSpan t, MaxTimeResolution res, TimeFormat fmt)
        {
            string fullStr;
            string unitStr;
            PairType<string, string> comps;

            switch(res)
            {
                case MaxTimeResolution.MILLISECONDS:
                    fullStr = "" + t.TotalMilliseconds;
                    unitStr = "milliseconds";
                    break;
                case MaxTimeResolution.SECONDS:
                    fullStr = "" + t.TotalSeconds;
                    unitStr = "seconds";
                    break;
                case MaxTimeResolution.MINUTES:
                    fullStr = "" + t.TotalMinutes;
                    unitStr = "minutes";
                    break;
                case MaxTimeResolution.HOURS:
                    fullStr = "" + t.TotalHours;
                    unitStr = "hours";
                    break;
                case MaxTimeResolution.DAYS:
                    fullStr = "" + t.TotalDays;
                    unitStr = "days";
                    break;
                default:
                    return (string.Empty);
            }

            if (!SplitFractionIntoComponents(fullStr, SPLIT_SPACER, out comps))
                return (string.Empty);
            string wholeComp = comps.Left;
            string fractComp = comps.Right;

            switch (fmt)
            {
                case TimeFormat.WHOLE:
                    return (wholeComp);
                case TimeFormat.WHOLEUNIT:
                    return (wholeComp + " " + unitStr);
                case TimeFormat.FRACTION:
                    return (fractComp);
                case TimeFormat.FRACTIONUNIT:
                    return ("0." + fractComp + " " + unitStr);
                case TimeFormat.WHOLEFRACTION:
                    return (wholeComp + "." + fractComp);
                case TimeFormat.WHOLEFRACTIONUNIT:
                    return (wholeComp + "." + fractComp + " " + unitStr);
                default:
                    return (string.Empty);
            }
        }

        /// <summary>
        /// Default MD5 Hash with ASCII encoding specified
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static string GenerateMD5Hash(string seed)
        {
            return (GenerateMD5Hash(seed, Encoding.ASCII));
        }

        /// <summary>
        /// Specialized MD5 Hash generator which requires specification
        /// of the encoder
        /// Truncates the front of the hexadecimal code due to use
        /// of "x" conversion character i.e. 0x04 => 4
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="encoder"></param>
        /// <returns></returns>
        public static string GenerateMD5Hash(string seed, Encoding encoder)
        {
            if (encoder == null)
            {
                return (string.Empty);
            }
            var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(encoder.GetBytes(seed));
            var sb = new StringBuilder();

            foreach (byte b in keyArray)
            {
                var s = b.ToString(HEX_CODE);
                sb.Append(s);
            }

            return (sb.ToString());
        }

        /// <summary>
        /// Raw MD5 hash generator that does not truncate zeroes from the
        /// front of a hexadecimal byte due to use of "x2" conversion character
        ///  i.e. 0x04 => 04
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="encoder"></param>
        /// <returns></returns>
        public static string GenerateRawMD5Hash(string seed, Encoding encoder)
        {
            if (encoder == null)
            {
                return (string.Empty);
            }
            var hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(encoder.GetBytes(seed));
            var sb = new StringBuilder();

            for (int i = 0; i < keyArray.Length; i++)
            {
                sb.Append(keyArray[i].ToString(FULLHEX_CODE));
            }

            return (sb.ToString());
        }
        /// <summary>
        /// Uses TripleDES encryption to encrypt a string passed in with given key
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <param name="useHashing"></param>
        /// <returns></returns>
        public static string Encrypt(string message, string key, bool useHashing)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrEmpty(key))
            {
                return (string.Empty);
            }
            try
            {
                byte[] keyArray;
                var toEncryptArray = Encoding.UTF8.GetBytes(message);

                //Validate the array to encrypt
                if (toEncryptArray.Length <= 0)
                {
                    return (string.Empty);
                }

                if (useHashing)
                {
                    var hashmd5 = new MD5CryptoServiceProvider();
                    var tmpArray = Encoding.UTF8.GetBytes(key);
                    if (tmpArray.Length <= 0)
                    {
                        return (string.Empty);
                    }
                    keyArray = hashmd5.ComputeHash(tmpArray);
                }
                else
                {
                    keyArray = Encoding.UTF8.GetBytes(key);
                }

                //Validate key array
                if (keyArray.Length <= 0)
                {
                    return (string.Empty);
                }

                var tdes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                var cTransform = tdes.CreateEncryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray,
                                                                 0,
                                                                 toEncryptArray.Length);

                //Validate result array
                if (resultArray == null || resultArray.Length <= 0)
                {
                    return (string.Empty);
                }

                return(Convert.ToBase64String(resultArray, 0, resultArray.Length));
            }
            catch(System.Exception)
            {
                return(string.Empty);
            }
        }

        /// <summary>
        /// Uses TripleDES encryption to encrypt a string passed in with given key
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <param name="useHashing"></param>
        /// <param name="encoder"></param>
        /// <returns></returns>
        public static byte[] RawEncrypt(string message, string key, bool useHashing, Encoding encoder)
        {
            if (string.IsNullOrEmpty(message) || string.IsNullOrEmpty(key) || encoder == null)
            {
                return (null);
            }
            try
            {
                byte[] keyArray;
                var toEncryptArray = encoder.GetBytes(message);

                //Validate the array to encrypt
                if (toEncryptArray.Length <= 0)
                {
                    return (null);
                }

                if (useHashing)
                {
                    var hashmd5 = new MD5CryptoServiceProvider();
                    var tmpArray = encoder.GetBytes(key);
                    if (tmpArray.Length <= 0)
                    {
                        return (null);
                    }
                    keyArray = hashmd5.ComputeHash(tmpArray);
                }
                else
                {
                    keyArray = encoder.GetBytes(key);
                }

                //Validate key array
                if (keyArray == null || keyArray.Length <= 0)
                {
                    return (null);
                }

                var tdes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.Zeros
                };

                var cTransform = tdes.CreateEncryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray,
                                                                 0,
                                                                 toEncryptArray.Length);

                //Validate result array
                if (resultArray == null || resultArray.Length <= 0)
                {
                    return (null);
                }

                return (resultArray);
            }
            catch (System.Exception)
            {
                return (null);
            }
        }


        /// <summary>
        /// Uses TripleDES decryption to decrypt a string passed in with given key
        /// </summary>
        /// <param name="encryptedMessage"></param>
        /// <param name="key"></param>
        /// <param name="useHashing"></param>
        /// <returns></returns>
        public static string Decrypt(string encryptedMessage, string key, bool useHashing)
        {
            if (string.IsNullOrEmpty(encryptedMessage) || string.IsNullOrEmpty(key))
            {
                return (string.Empty);
            }
            try
            {
                byte[] keyArray;
                var toEncryptArray = Convert.FromBase64String(encryptedMessage);

                //Validate the array we are going to decrypt
                if (toEncryptArray.Length <= 0)
                {
                    return (string.Empty);
                }

                if (useHashing)
                {
                    var hashmd5 = new MD5CryptoServiceProvider();
                    var tmpKeys = Encoding.UTF8.GetBytes(key);
                    if (tmpKeys.Length > 0)
                    {
                        keyArray = hashmd5.ComputeHash(tmpKeys);
                    }
                    else
                    {
                        return (string.Empty);
                    }
                }
                else
                {
                    keyArray = Encoding.UTF8.GetBytes(key);
                }

                //Validate the key array we are going to use to decrypt
                //the message
                if (keyArray.Length <= 0)
                {
                    return (string.Empty);
                }

                //Create TripleDES decryption provider
                var tdes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                //Decrypt the message
                var cTransform = tdes.CreateDecryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                //Validate result array
                if (resultArray == null || resultArray.Length <= 0)
                {
                    return (string.Empty);
                }

                return (Encoding.UTF8.GetString(resultArray));

            }
            catch (System.Exception)
            {
                //Catches exceptions and returns empty string
                return (string.Empty);
            }
        }

        /// <summary>
        /// Uses TripleDES decryption to decrypt a string passed in with given key
        /// </summary>
        /// <param name="encryptedMessage"></param>
        /// <param name="key"></param>
        /// <param name="useHashing"></param>
        /// <param name="encoder"></param>
        /// <returns></returns>
        public static string RawDecrypt(byte[] encryptedMessage, string key, bool useHashing, Encoding encoder)
        {
            if (encryptedMessage == null || encryptedMessage.Length <= 0 || string.IsNullOrEmpty(key))
            {
                return (string.Empty);
            }
            try
            {
                byte[] keyArray;
                var toEncryptArray = encryptedMessage;

                //Validate the array we are going to decrypt
                if (toEncryptArray == null || toEncryptArray.Length <= 0)
                {
                    return (string.Empty);
                }

                if (useHashing)
                {
                    var hashmd5 = new MD5CryptoServiceProvider();
                    var tmpKeys = encoder.GetBytes(key);
                    if (tmpKeys.Length > 0)
                    {
                        keyArray = hashmd5.ComputeHash(tmpKeys);
                    }
                    else
                    {
                        return (string.Empty);
                    }
                }
                else
                {
                    keyArray = encoder.GetBytes(key);
                }

                //Validate the key array we are going to use to decrypt
                //the message
                if (keyArray == null || keyArray.Length <= 0)
                {
                    return (string.Empty);
                }

                //Create TripleDES decryption provider
                var tdes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.Zeros
                };

                //Decrypt the message
                var cTransform = tdes.CreateDecryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                //Validate result array
                if (resultArray == null || resultArray.Length <= 0)
                {
                    return (string.Empty);
                }

                return (encoder.GetString(resultArray));

            }
            catch (System.Exception)
            {
                //Catches exceptions and returns empty string
                return (string.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static string ConverByteArrayToHexString(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                return (string.Empty);
            }

            var rt = new StringBuilder(32);
            for (var i = 0; i < byteArray.Length; ++i)
            {
                var curByte = byteArray[i];
                rt.Append(curByte.ToString(FULLHEX_CODE));
            }

            return (rt.ToString());
        }
    }
}
