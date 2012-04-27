using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.String;
using Common.Libraries.Utility.Type;

namespace Common.Controllers.Database
{
    public static class LoanTicketLengthCalculator
    {
        public const long GUN_NUM_HOLD = 111;
        public const string GUN_NUM_SHORT = "G#";
        public const string GUN_NUM_LONG = "Gun #";
        public static readonly char[] SPLITS = { ' ', ';', ']', '.' };
        public static readonly PairType<string, string> COMPRESS_PASS_1 = new PairType<string, string>("; ", ";");

        public static bool ComputeDescription(PawnLoan pLoan, out string desc)
        {
            desc = string.Empty;

            if (pLoan == null || pLoan.Items == null ||
                pLoan.Items.Count <= 0)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "LoanTicketLengthCalculator",
                                                   "Cannot compute loan description - invalid method inputs");
                }
                return (false);
            }

            //Generate place holder gun numbers for pre-process tender line length computation
            var gunNumberIndices = new List<PairType<int, long>>();
            for (int i = 0; i < pLoan.Items.Count; ++i)
            {
                Item p = pLoan.Items[i];
                if (p == null)
                    continue;

                gunNumberIndices.Add(new PairType<int, long>(-1, -1L));
                if (!p.IsGun)continue;
                gunNumberIndices.Last().Left = i;
                //No valid gun number yet, use place holder
                gunNumberIndices.Last().Right = GUN_NUM_HOLD;
            }
            return (ComputeDescription(pLoan, gunNumberIndices, out desc));
        }

        public static bool ComputeDescription(PawnLoan pLoan, List<PairType<int, long>> gunItemNumberIndices, out string desc)
        {
            desc = string.Empty;
            if (pLoan == null || gunItemNumberIndices == null || pLoan.Items == null || 
                pLoan.Items.Count <= 0 || gunItemNumberIndices.Count != pLoan.Items.Count)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "LoanTicketLengthCalculator",
                                                   "Cannot compute loan description - invalid method inputs");
                }
                return (false);
            }

            var descBuilder = new StringBuilder();
            for (int i = 0; i < pLoan.Items.Count; ++i)
            {
                Item p = pLoan.Items[i];
                if (p == null)
                    continue;

                PairType<int, long> curGunItemNumber = gunItemNumberIndices[i];
                //Are we at a gun indice

                //Insert gun number into ticket description for the current
                //pawn item
                string gunNumberStr = null;
                string ticketSubNumStr = "[" + (i + 1) + "]";
                if (curGunItemNumber.Left == i)
                {
                    long gunNumber = curGunItemNumber.Right;
                    gunNumberStr = GUN_NUM_SHORT + gunNumber + ";";
                }
                //SR 08/05/2011 Added logic to remove brackets which do not have matching ending pair
                int begBracketIndex = p.TicketDescription.IndexOf("(");
                int endBracketIndex = p.TicketDescription.IndexOf(")");
                if ((begBracketIndex >=0 && (endBracketIndex < 0 || endBracketIndex < begBracketIndex)) ||
                    endBracketIndex > begBracketIndex)
                {
                    p.TicketDescription = p.TicketDescription.Replace("(", " ");
                    p.TicketDescription = p.TicketDescription.Replace(")", " ");

                }
                if (string.IsNullOrEmpty(gunNumberStr))
                {
                    descBuilder.Append(ticketSubNumStr + p.TicketDescription);
                }
                else
                {
 
                    string fullDesc = ticketSubNumStr + p.TicketDescription;
                    string ticketNumberSubStr = fullDesc.Substring(0, 3);
                    string remainingDescStr = fullDesc.Substring(3);
                    descBuilder.Append(ticketNumberSubStr + gunNumberStr + remainingDescStr);
                }
            }
            
            if (descBuilder.Length > 0)
            {
                desc = descBuilder.ToString();
                return (true);
            }

            return (false);
        }

        public static bool SplitDescription(string description, int lineLength, out List<string> descLines, out List<PairType<int, long>> validGunNumberAndLineIdx)
        {
            descLines = null;
            validGunNumberAndLineIdx = null;
            if (lineLength <= 0 || string.IsNullOrEmpty(description))
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "LoanTicketLengthCalculator",
                                                   "Cannot split loan description - invalid line length or description");
                }
                return (false);
            }
            //Compress the string using pass 1 values
            var descriptionVal = StringUtilities.CompressString(description, COMPRESS_PASS_1.Left, COMPRESS_PASS_1.Right);

            if (string.IsNullOrWhiteSpace(descriptionVal))
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "LoanTicketLengthCalculator",
                                                   "Cannot split loan description - compression pass invalidated description");
                }
                return (false);
            }

            //After compression - attempt to split the string
            if (!StringUtilities.SplitLongString(descriptionVal, lineLength, SPLITS, out descLines))
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "LoanTicketLengthCalculator",
                                                   "Cannot split loan description - split string algorithm failed");
                }
                return (false);
            }
            
            //Ensure that the output is valid
            if (CollectionUtilities.isEmpty(descLines))
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "LoanTicketLengthCalculator",
                                                   "Cannot split loan description - output of split string algorithm is invalid");
                }
                return (false);
            }

            //Loop through each line and look for a gun number, if it is found, add the line number and the gun number
            int lineNumber = 1;
            foreach (string s in descLines)
            {
                if (string.IsNullOrEmpty(s))
                {
                    ++lineNumber;
                    continue;
                }

                //Potentially multiple guns on a single line - lets find them all
                //First see if we have a gun on this line
                bool noGunLeft = false;
                int startIdx = 0;
                while(!noGunLeft)
                {
                    var idx = s.IndexOf(GUN_NUM_SHORT, startIdx, System.StringComparison.Ordinal);
                    if (idx == -1)
                    {
                        noGunLeft = true;
                        continue;
                    }
                    if (validGunNumberAndLineIdx == null)
                    {
                        validGunNumberAndLineIdx = new List<PairType<int, long>>();
                    }
                    var semiIdx = s.IndexOf(";", idx, StringComparison.OrdinalIgnoreCase);
                    if (semiIdx == -1)
                    {
                        //We have a problem, issue error and return false
                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, "LoanTicketLengthCalculator",
                                                           "Error in split loan description - gun number determination failed on line {0} - could not find terminating semicolon",
                                                           s);
                        }
                        //Set no gun left and continue to next line
                        noGunLeft = true;
                        continue;
                    }
                    //Extract the substring that represents the gun number
                    //Position index to the character just right of G#
                    idx += 2;
                    var gunNumStr = s.Substring(idx, semiIdx - idx);
                    if (!string.IsNullOrEmpty(gunNumStr))
                    {
                        long gunNumberFound = Utilities.GetLongValue(gunNumStr, -1L);
                        if (gunNumberFound != -1L)
                        {
                            validGunNumberAndLineIdx.Add(new PairType<int, long>(lineNumber, gunNumberFound));
                        }
                    }

                    //Set new starting index
                    startIdx = idx;
                    if (startIdx >= s.Length)
                    {
                        noGunLeft = true;
                    }
                }

                //Increment line index number
                ++lineNumber;
            }

            //If we reach this point, we are successful
            return (true);
        }
    }
}
