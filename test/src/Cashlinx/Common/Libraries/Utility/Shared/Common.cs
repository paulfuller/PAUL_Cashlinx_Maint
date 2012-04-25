/********************************************************************
* PawnUtilities.Shared
* Common
* This class has all the common functions and extension methods
* used in the customer related use cases
* Sreelatha Rengarajan 4/3/2009 Initial version
*******************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Resources;
using System.Windows.Forms;
using Common.Libraries.Utility.Exception;

namespace Common.Libraries.Utility.Shared
{
    //DO NOT RENAME THIS CLASS TO MATCH THE FILENAME - GJL 01/17/2012
    //DO NOT RENAME THE FILE TO MATCH THE CLASS NAME - GJL 01/17/2012
    public static class Commons
    {
        static ResourceManager m_ResourceManager = new ResourceManager("Common.Properties.ApplicationMessages",
    System.Reflection.Assembly.GetExecutingAssembly());




        //Follwing are Pawn Application Business rule components
        public const string PAWNTKTFEELOSTFEEBR = "PWN_BR-038";
        public const string PAWNTKTLOSTFEECOMPONENT = "CL_PWN_0044_LOSTPWNTKTFEE";
        public const string PAWNGENERALBR = "PWN_BR-000";
        public const string PAWNLEGALAGECOMPONENT = "PWNAPP_AGE";
        public const string PAWNGUNMIMIMUMAGEBR = "PWN_BR-059";
        public const string PAWNBACKGROUNDCHECKBR = "PWN_BR-071";
        public const string PAWNRETAILNOFIREARM = "PWN_BR_0001";
        public const string PAWNRETAILWITHFIREARM = "PWN_BR_0002";
        public const string PAWNRETAILOVER10KCTR = "PWN_BR_0003";
        public const string CURRENCYSIGN = "$";
        private const string THIRTEEN = "13";
        private const string TWELVE = "12";
        private const string ELEVEN = "11";
        private const string NUM_TH_SUFFIX = "th";
        private const char CHAR_1 = '1';
        private const char CHAR_2 = '2';
        private const char CHAR_3 = '3';
        private const string NUM_RD_SUFFIX = "rd";
        private const string FDATE = "d";
        private const string FDATETIME = "MM/dd/yyyy HH:mm:ss K";
        private const string NUM_ST_SUFFIX = "st";
        private const string NUM_ND_SUFFIX = "nd";
        private const string DATE_SLASH = "/";
        public static MD5CryptoServiceProvider MD5_CRYPTO = new MD5CryptoServiceProvider();

        public static bool CheckICNSubItem(string icn)
        {
            bool subitem = false;
            string subItemNum = icn.Substring(icn.Length - 2);
            if (Convert.ToInt32(subItemNum) > 0)
                subitem = true;
            return subitem;
        }

        public static string FormatDate(this DateTime dateTime)
        {
            //Automatically converts date to string of format "MM/dd/yyyy"
            return (dateTime.ToString(FDATE, DateTimeFormatInfo.InvariantInfo));
        }

        public static string FormatDateWithTimeZone(this DateTime dateTime)
        {
            //Automatically converts date to string of format "MM/dd/yyyy HH:mm:ss tz" 
            return (dateTime.ToString(FDATETIME, DateTimeFormatInfo.InvariantInfo));
        }

        /// <summary>
        /// Format string into slash MM/DD/YYYY format
        /// </summary>
        /// <param name="dollarString"> </param>
        /// <param name="formattedValue"> </param>
        /// <returns></returns>
        public static bool FormatDollarStringAsDecimal(string dollarString, out decimal formattedValue)
        {
            const NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            var formatted = Decimal.TryParse(dollarString, style, culture, out formattedValue);
            return formatted;
        }

        public static bool FormatStringAsDecimal(string dollarString, out decimal formattedValue)
        {
            const NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            var formatted = Decimal.TryParse(dollarString, style, culture, out formattedValue);
            return formatted;
        }

        public static decimal FormatPercentStringAsDecimal(this string value)
        {
            decimal result;
            if (decimal.TryParse(value.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, string.Empty), out result))
            {
                return (result);
            }
            return (0.0M);
        }

        public static string FormatStringAsDate(this string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length != 8)
            {
                return s;
            }
            var dateString = new System.Text.StringBuilder();
            dateString.Append(s.Substring(0, 2));
            dateString.Append(DATE_SLASH);
            dateString.Append(s.Substring(2, 2));
            dateString.Append(DATE_SLASH);
            dateString.Append(s.Substring(4, 4));
            return dateString.ToString();
        }


        public class StringValue
        {
            public StringValue(string s)
            {
                Value = s;
            }
            public string Value;


        }


        /// <summary>
        /// Take date time day of month and properly suffix the number
        /// Exceptions are 11th, 12th, and 13th
        /// Any month day ending with 1 will be suffixed with 'st'
        /// Any month day ending with 2 will be suffixed with 'nd'
        /// Any month day ending with 3 will be suffixed with 'rd'
        /// Any month day ending with any other number will be suffixed  with 'th'
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string FormatDayOfMonthWithSuffix(this DateTime dateTime)
        {
            var dateStr = dateTime.Day.ToString();
            return (FormatNumberStringWithSuffix(dateStr));
        }

        public static string FormatNumberWithSuffix(this Int32 number)
        {
            var numStr = "" + number;
            return (FormatNumberStringWithSuffix(numStr));
        }
        public static string FormatNumberWithSuffix(this Int64 number)
        {
            var numStr = "" + number;
            return (FormatNumberStringWithSuffix(numStr));
        }
        public static string FormatNumberWithSuffix(this Int16 number)
        {
            var numStr = "" + number;
            return (FormatNumberStringWithSuffix(numStr));
        }

        public static object GetDataObject(this DataRow dRow, string name)
        {
            if (string.IsNullOrEmpty(name)) return (string.Empty);
            if (dRow.ItemArray.Length <= 0 ||
                dRow.ItemArray.LongLength <= 0L)
            {
                return (string.Empty);
            }
            if (dRow.Table.Columns.Contains(name))
            {
                return (dRow[name]);
            }
            return (string.Empty);
        }
        /// <summary>
        /// String extension to convert into an MD5 hash string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertToMD5(this string s)
        {
            var keyArray = MD5_CRYPTO.ComputeHash(Encoding.ASCII.GetBytes(s));
            var sb = new StringBuilder();
            foreach (var b in keyArray)
            {
                var curBx = b.ToString("x");
                sb.Append(curBx);
            }
            return (sb.ToString());
        }

        /// <summary>
        /// Common method for number suffix appending
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string FormatNumberStringWithSuffix(string str)
        {
            if (string.IsNullOrEmpty(str)) return (string.Empty);
            if (string.IsNullOrEmpty(str))
                return (string.Empty);
            var suffixStr = NUM_TH_SUFFIX;
            if ((str.Equals(ELEVEN)) || (str.Equals(TWELVE)) || (str.Equals(THIRTEEN)))
            {
                return (str + suffixStr);
            }
            var lastChar = ((str.Length == 1) ? str[0] : str[1]);

            switch (lastChar)
            {
                case CHAR_1:
                    suffixStr = NUM_ST_SUFFIX;
                    break;
                case CHAR_2:
                    suffixStr = NUM_ND_SUFFIX;
                    break;
                case CHAR_3:
                    suffixStr = NUM_RD_SUFFIX;
                    break;
            }
            return (str + suffixStr);
        }

        public static bool CanBePutOnPoliceHold(string status, string holdtype)
        {
            var allowPolicehold = status.Equals("PFI") || status.Equals("IP") || status.Equals("PUR") || status.Equals("LAY");
            if (holdtype == GetHoldDescription(HoldTypes.POLICEHOLD))
                allowPolicehold = false;
            return allowPolicehold;
        }

        public static bool CanBeReleased(string status, string holdtype)
        {
            var allowRelease = (status.Equals("PFI") || status.Equals("IP") || status.Equals("PUR") || status.Equals("LAY")) &&
                                (holdtype == GetHoldDescription(HoldTypes.POLICEHOLD));
            return allowRelease;
        }


        public static bool IsLockedStatus(string tempStatus, ref string lockedProcess)
        {
            //Any temp status other than Blank, PFI,PFIS or PFIW is locked
            lockedProcess = "";



            if (tempStatus.Equals(StateStatus.PFIE.ToString()) ||
                tempStatus.Equals(StateStatus.PFIL.ToString()))
            {
                lockedProcess = "PFI";
                return true;
            }
            if (tempStatus.Equals(StateStatus.P.ToString()))
            {
                lockedProcess = "Pickup";
                return true;
            }
            if (tempStatus.Equals(StateStatus.E.ToString()))
            {
                lockedProcess = "Extend";
                return true;
            }
            if (tempStatus.Equals(StateStatus.RO.ToString()))
            {
                lockedProcess = "Rollover";
                return true;
            }
            if (tempStatus.Equals(StateStatus.CH.ToString()))
            {
                lockedProcess = "Customer Hold";
                return true;
            }
            if (tempStatus.Equals(StateStatus.PH.ToString()))
            {
                lockedProcess = "Police Hold";
                return true;
            }
            if (tempStatus.Equals(StateStatus.BH.ToString()))
            {
                lockedProcess = "Bankruptcy Hold";
                return true;
            }
            return false;
        }

        public static string Format10And11CharacterPhoneNumberForUI(string strPhone)
        {
            if (strPhone.Length != 10 && strPhone.Length != 11)
            {
                return strPhone;
            }
            else
            {
                if (strPhone.Length == 10)
                {
                    strPhone = "(" + strPhone.Substring(0, 3) + ")" + " " + strPhone.Substring(3, 3) + "-" + strPhone.Substring(6, 4);
                }
                else
                {
                    strPhone = strPhone.Substring(0, 1) + "(" + strPhone.Substring(1, 3) + ")" + " " + strPhone.Substring(4, 3) + "-" + strPhone.Substring(7, 4);
                }
                return strPhone;
            }
        }

        public static string FormatPhoneNumberForUI(string strPhone)
        {
            if (strPhone.Length != 7)
            {
                return strPhone;
            }
            return strPhone.Substring(0, 3) + "-" + strPhone.Substring(3, 4);
        }

        public static string FormatPhoneNumberForDB(string strPhone)
        {
            if (strPhone.Length != 8)
                return string.Empty;
            return strPhone.Substring(0, 3) + strPhone.Substring(4, 4);
        }


        public static string FormatSSN(string ssnText)
        {
            if (ssnText != "" && ssnText.Length == 9)
                return "xxxxx" + ssnText.Substring(5, 4);
            return ssnText;
        }

        public static int getAge(string birthdate, DateTime currDate)
        {
            var customerDOB = DateTime.Parse(birthdate);
            var yearsAge = currDate.Year - customerDOB.Year;
            // If the birthday hasn't occured this year, subtract one year from the age
            if (currDate.Month < customerDOB.Month ||
                (currDate.Month == customerDOB.Month && currDate.Day < customerDOB.Day))
            {
                --yearsAge;
            }

            return yearsAge;

        }


        public static void CustomPaint(Control ctrl, Rectangle rect)
        {
            var hwnd = ctrl.Parent.Handle;
            var g = Graphics.FromHwndInternal(hwnd);
            var p = new Pen(new SolidBrush(Color.DarkBlue), 1);
            g.DrawRectangle(p, rect);
            g.Dispose();
            //TODO: Is this disposal in the right order?  Shouldn't "g" get disposed last?
            p.Dispose();
        }

        public static void RemoveBorder(Control ctrl, Rectangle rect)
        {
            var hwnd = ctrl.Parent.Handle;
            var g = Graphics.FromHwndInternal(hwnd);
            var p = new Pen(new SolidBrush(Color.White), 1);
            g.DrawRectangle(p, rect);
            //TODO: Is this disposal in the right order?  Shouldn't "g" get disposed last?
            g.Dispose();
            p.Dispose();
        }

        public static void DrawAsterisk(Control ctrl, float x, float y)
        {
            if (ctrl.Parent == null)
            {
                return;
            }
            var hwnd = ctrl.Parent.Handle;
            var g = Graphics.FromHwndInternal(hwnd);
            const string strAsterisk = "*";
            var newFont = new Font("Arial", 9, FontStyle.Bold);
            g.DrawString(strAsterisk, newFont, new SolidBrush(Color.Red), x, y);
            newFont.Dispose();
            g.Dispose();
        }

        public static void RemoveAsterisk(Control ctrl, float x, float y)
        {
            if (ctrl.Parent == null)
            {
                return;
            }
            var hwnd = ctrl.Parent.Handle;
            var g = Graphics.FromHwndInternal(hwnd);
            const string strNoAsterisk = " ";
            var newFont = new Font("Arial", 1, FontStyle.Regular);
            g.DrawString(strNoAsterisk, newFont, new SolidBrush(Color.White), x, y);
            newFont.Dispose();
            g.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static string GetMessageString(string keyName)
        {
            if (m_ResourceManager != null)
            {
                return m_ResourceManager.GetString(keyName);
            }
            return (string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyname"></param>
        /// <returns></returns>
        public static string GetResourceName(string keyname)
        {
            //value for our return value
            string resourceValue;
            try
            {
                var filePath = System.AppDomain.CurrentDomain.BaseDirectory;
                // create a resource manager for reading from
                //the resx file
                var resourceManager = ResourceManager.CreateFileBasedResourceManager("ResourceNames", filePath, null);
                // retrieve the value of the specified key
                resourceValue = resourceManager.GetString(keyname);
            }
            catch (System.Exception eX)
            {
                BasicExceptionHandler.Instance.AddException(string.Format("GetResourceName({0}) failed", keyname), eX);
                resourceValue = string.Empty;
            }
            return resourceValue;
        }

        /// <summary>
        /// Retrieves all pawn application 
        /// </summary>
        /// <returns></returns>
        public static List<string> GetPawnAppBusinessRuleComponents()
        {
            var businessRuleComponents = new List<string>
                                         {
                                             "PWNAPP_LASTNAME",
                                             "PWNAPP_FIRSTNAME",
                                             "PWNAPP_MIDDLEINITIAL",
                                             "PWNAPP_IDENTIFICATIONTYPE",
                                             "PWNAPP_IDENTIFICATIONSTATE",
                                             "PWNAPP_IDENTIFICATIONCOUNTRY",
                                             "PWNAPP_IDENTIFICATIONNUMBER",
                                             "PWNAPP_IDENTIFICATIONEXPIRATIONDATE",
                                             "PWNAPP_SECONDARYIDTYPE",
                                             "PWNAPP_SECONDARYIDSTATE",
                                             "PWNAPP_SECONDARYIDCOUNTRY",
                                             "PWNAPP_SECONDARYIDNUMBER",
                                             "PWNAPP_SECONDARYIDEXPIRATIONDATE",
                                             "PWNAPP_SUPPLEMENTALID",
                                             "PWNAPP_FFLNUMBER",
                                             "PWNAPP_PHONENUMBER",
                                             "PWNAPP_ADDRESS",
                                             "PWNAPP_ZIP",
                                             "PWNAPP_CITY",
                                             "PWNAPP_STATE",
                                             "PWNAPP_DATEOFBIRTH",
                                             "PWNAPP_AGE",
                                             "PWNAPP_SOCIALSECURITYNUMBER",
                                             "PWNAPP_SEX",
                                             "PWNAPP_RACE",
                                             "PWNAPP_HEIGHT",
                                             "PWNAPP_WEIGHT",
                                             "PWNAPP_HAIR",
                                             "PWNAPP_EYES",
                                             "PWNAPP_WORKPLACE",
                                             "PWNAPP_WORKPHONE",
                                             "PWNAPP_CLOTHING",
                                             "PWNAPP_COMMENTS"
                                         };
            return businessRuleComponents;
        }

        public static string GetGenderData(string code)
        {
            string genderDesc = "";
            switch (code)
            {
                case "M":
                    genderDesc = "Male";
                    break;
                case "F":
                    genderDesc = "Female";
                    break;
                default:
                    genderDesc = code;
                    break;
            }
            return genderDesc;
        }

        public static string GetPrefCallTime(string code)
        {
            string callTimeDesc = "";
            switch (code)
            {
                case "MOR":
                    callTimeDesc = "Morning";
                    break;
                case "EVN":
                    callTimeDesc = "Evening";
                    break;
                case "AFT":
                    callTimeDesc = "Afternoon";
                    break;
                default:
                    callTimeDesc = code;
                    break;
            }
            return callTimeDesc;
        }

        public static string GetHearAboutUsData(string code)
        {
            string hearAboutUsDesc = "";
            switch (code)
            {
                case "MA":
                    hearAboutUsDesc = "Mail";
                    break;
                case "RA":
                    hearAboutUsDesc = "Radio";
                    break;
                case "TV":
                    hearAboutUsDesc = "TV";
                    break;
                case "YP":
                    hearAboutUsDesc = "Yellow Pages";
                    break;
                case "SS":
                    hearAboutUsDesc = "Store Signs";
                    break;
                case "OT":
                    hearAboutUsDesc = "Other";
                    break;
                default:
                    hearAboutUsDesc = code;
                    break;
            }
            return hearAboutUsDesc;
        }

        public static string GetCodeDesc(string code)
        {
            string codeDescription = "";
            switch (code)
            {
                case "YS":
                    codeDescription = "Yes";
                    break;
                case "NO":
                    codeDescription = "No";
                    break;
                default:
                    codeDescription = code;
                    break;
            }
            return codeDescription;
        }

        public static string GetPreferredContactData(string code)
        {
            string prefContactData = "";
            switch (code)
            {
                case "HPH":
                    prefContactData = "Home Phone";
                    break;
                case "WPH":
                    prefContactData = "Work Phone";
                    break;
                case "CPH":
                    prefContactData = "Cell Phone";
                    break;
                case "EML":
                    prefContactData = "Email";
                    break;
                default:
                    prefContactData = code;
                    break;
            }
            return prefContactData;
        }

        public static string GetCountryData(string Text)
        {
            string CountryCode = "";
            switch (Text)
            {
                case "United States":
                    CountryCode = "US";
                    break;
                case "Mexico":
                    CountryCode = "MX";
                    break;
                case "Canada":
                    CountryCode = "CD";
                    break;
                default:
                    CountryCode = "US";
                    break;
            }
            return CountryCode;

        }

        public static string GetLostTicketType(string code)
        {
            string codeDesc = "";
            switch (code)
            {
                case CustLoanLostTicketFee.LOSTTICKETTYPE:
                    codeDesc = "Lost";
                    break;
                case CustLoanLostTicketFee.DESTROYEDTICKETTYPE:
                    codeDesc = "Destroyed";
                    break;
                case CustLoanLostTicketFee.STOLENTICKETTYPE:
                    codeDesc = "Stolen";
                    break;
                default:
                    codeDesc = "Lost";
                    break;
            }
            return codeDesc;
        }

        public static bool IsStateID(string IdTypeCode)
        {
            if (IdTypeCode == StateIdTypes.STATE_IDENTIFICATION_ID ||
                IdTypeCode == StateIdTypes.DRIVERLICENSE ||
                IdTypeCode == StateIdTypes.CONCEALED_WEAPONS_PERMIT)
                return true;
            else
                return false;
        }

        public static bool IsStateIdDescription(string IdTypeDesc)
        {
            if (IdTypeDesc == StateIdTypeDescription.CW ||
                IdTypeDesc == StateIdTypeDescription.DRIVER_LIC ||
                IdTypeDesc == StateIdTypeDescription.STATE_ID)
                return true;

            else
                return false;
        }




        public static string GetFeeName(FeeTypes FeeType)
        {
            string feeTypeDesc = "";
            switch (FeeType)
            {
                case FeeTypes.ADMINISTRATIVE:
                    feeTypeDesc = "Administrative Fee";
                    break;
                case FeeTypes.GUNLOCK:
                    feeTypeDesc = "Gun Lock Fee";
                    break;
                case FeeTypes.FIREARM:
                    feeTypeDesc = "Firearm Fee";
                    break;
                case FeeTypes.LOST_TICKET:
                    feeTypeDesc = "Lost Ticket Fee";
                    break;
                case FeeTypes.LATE:
                    feeTypeDesc = "Late Fee";
                    break;
                case FeeTypes.AUTOEXTEND:
                    feeTypeDesc = "Auto Extend Fee";
                    break;
                case FeeTypes.CITY:
                    feeTypeDesc = "City Fee";
                    break;
                case FeeTypes.INITIAL:
                    feeTypeDesc = "Initial Fee";
                    break;
                case FeeTypes.LOAN:
                    feeTypeDesc = "Loan Fee";
                    break;
                case FeeTypes.ORIGIN:
                    feeTypeDesc = "Origination Fee";
                    break;
                case FeeTypes.MAILER_CHARGE:
                    feeTypeDesc = "PFI Mailer Fee";
                    break;
                case FeeTypes.PREPAID_CITY:
                    feeTypeDesc = "Prepaid City Fee";
                    break;
                case FeeTypes.SETUP:
                    feeTypeDesc = "Setup Fee";
                    break;
                case FeeTypes.STORAGE:
                    feeTypeDesc = "Storage Fee";
                    break;
                case FeeTypes.INTEREST:
                    feeTypeDesc = "Interest Fee";
                    break;
                case FeeTypes.SERVICE:
                    feeTypeDesc = "Service Fee";
                    break;
            }
            return feeTypeDesc;

        }

        public static string GetHoldDescription(HoldTypes typeOfHold)
        {
            string holdTypeDesc = "";
            switch (typeOfHold)
            {
                case HoldTypes.CUSTHOLD:
                    holdTypeDesc = "Customer Hold";
                    break;
                case HoldTypes.POLICEHOLD:
                    holdTypeDesc = "Police Hold";
                    break;
                case HoldTypes.BKHOLD:
                    holdTypeDesc = "Bankruptcy Hold";
                    break;
            }
            return holdTypeDesc;
        }

        public static string GetDenominationHeading(string denominationLabel)
        {
            string denominationHeading = "";
            switch (denominationLabel)
            {
                case "USD 1":
                    denominationHeading = "ONES";
                    break;
                case "USD COIN 1":
                    denominationHeading = "DOLLAR COINS";
                    break;
                case "USD 2":
                    denominationHeading = "TWOS";
                    break;
                case "USD 5":
                    denominationHeading = "FIVES";
                    break;
                case "USD 10":
                    denominationHeading = "TENS";
                    break;
                case "USD 20":
                    denominationHeading = "TWENTIES";
                    break;
                case "USD 50":
                    denominationHeading = "FIFTIES";
                    break;
                case "USD 100":
                    denominationHeading = "HUNDREDS";
                    break;
                case "USD 0.01":
                    denominationHeading = "PENNIES";
                    break;
                case "USD 0.05":
                    denominationHeading = "NICKELS";
                    break;
                case "USD 0.10":
                    denominationHeading = "DIMES";
                    break;
                case "USD 0.25":
                    denominationHeading = "QUARTERS";
                    break;
                case "USD 0.50":
                    denominationHeading = "HALF DOLLARS";
                    break;
                default:
                    denominationHeading = denominationLabel;
                    break;

            }
            return denominationHeading;
        }



        public static bool CanBePoliceSeized(string status)
        {
            return status == "IP" || status == "PUR" || status == "PFI" || status == "LAY";
        }

        public static string GetPfiAssignmentAbbreviation(PfiAssignment pfiAssignment)
        {
            return GetPfiAssignmentAbbreviation(pfiAssignment.ToString());
        }

        public static string GetPfiAssignmentAbbreviation(string pfiAssignment)
        {
            if (pfiAssignment.Equals(PfiAssignment.CAF.ToString()))
            {
                return "C";
            }
            else if (pfiAssignment.Equals(PfiAssignment.Refurb.ToString()))
            {
                return "R";
            }
            else if (pfiAssignment.Equals(PfiAssignment.Scrap.ToString()))
            {
                return "S";
            }
            else if (pfiAssignment.Equals(PfiAssignment.Sell_Back.ToString()))
            {
                return "SB";
            }
            else
            {
                return "N";
            }
        }

        public struct TriggerTypes
        {
            public const string LOOKUPTICKET = "LookupTicket";
            public const string MANAGEMULTIPLEPAWNITEMS = "ManageMultiplePawnItems";
            public const string NEWPAWNLOAN = "NewPawnLoan";
            public const string VIEWPAWNCUSTPRODDETAILS = "ViewPawnCustomerProductDetails";
            public const string ADDNEWCUSTOMER = "Addnewcustomer";
            public const string MANAGEITEMRELEASE = "ManageItemRelease";
            public const string LOOKUPCUSTOMER = "LookupCustomer";
            public const string EXISTINGCUSTOMER = "ExistingCustomer";
            public const string LOOKUPRECEIPT = "LookupReceipt";
            public const string CUSTOMERPURCHASE = "customerpurchase";
            public const string DESCRIBEITEMCUSTOMERPURCHASE = "describeitemcustomerpurchase";
            public const string VENDORPURCHASE = "vendorpurchase";
            public const string CUSTOMERPURCHASEPFI = "customerpurchasepfi";
            public const string VOIDBUYRETURN = "voidbuyreturns";
            public const string VOIDBUY = "voidbuy";
            public const string VOIDCASHTRANSFER = "voidcashtransfer";
            public const string VOIDMERCHANDISETRANSFER = "voidmerchandisetransfer";
            public const string INTERNALSAFETRANSFER = "internalsafetransfer";
            public const string SHOPCASHMANAGEMENT = "shopcashmanagement";
            public const string CUSTOMERHOLD = "customerhold";
            public const string CUSTOMERHOLDRELEASE = "customerholdrelease";
            public const string SECURITY = "security";
            public const string POLICEHOLDRELEASE = "policeholdrelease";
            //public const string VOIDBANKTRANSFER = "voidbanktransfer";
            public const string VOIDBANKTOSHOP = "voidbanktoshop";
            public const string VOIDSHOPTOBANK = "voidshoptobank";
            public const string VOIDSHOPTOSHOPTRANSFER = "voidshoptoshoptransfer";
            public const string TOPSTRANSFER = "topstransfer";
            public const string VOIDMDSETRANSFER = "voidmdsetransfer";
            public const string VOIDSALE = "voidsale";
            public const string VOIDREFUND = "voidrefundsale";
            public const string RETAIL = "retail";
            public const string CHANGERETAILPRICE = "changeretailprice";
            public const string VOIDLAYAWAY = "voidlayaway";
            public const string DESCRIBEMERCHANDISE = "describemerchandise";
            public const string VOIDVENDORBUY = "voidvendorbuy";
            public const string GUNBOOKEDIT = "gunbookedit";
            public const string RETURNCUSTOMERBUY = "returncustomerbuy";


        }



        public static bool IsMoneyInOpCode(string opCode)
        {
            if (opCode == "REIN" || opCode == "TRIN" || opCode == "RCREDIT" ||
                opCode == "PCIN" || opCode == "VSCCIN" || opCode == "VSCCOUT" ||
                opCode == "MCCCIN" || opCode == "MCCCOUT" || opCode == "AMEXCCIN" ||
                opCode == "AMEXCCOUT" || opCode == "DSCCIN" || opCode == "DSCCOUT" ||
                    opCode == "CCIN" || opCode == "CCOUT" || opCode == "VSDCIN" ||
                    opCode == "VSDCOUT" || opCode == "MCDCIN" || opCode == "MCDCOUT" ||
                    opCode == "CDIN" || opCode == "CDOUT" || opCode == "SCIN" ||
                    opCode == "SCOUT" || opCode == "PPIN" || opCode == "PPOUT" ||
                opCode == "MVSIN" || opCode == "CMCIN" || opCode == "DCIN" || opCode == "DCOUT" || opCode == "BILLTOAP" || opCode == "RBILLTOAP")
                return true;
            return false;
        }


        public static bool IsMoneyOutOpCode(string opCode)
        {
            if (opCode == "REOUT" || opCode == "TROUT" || opCode == "RDEBIT" ||
                opCode == "PCOUT" || opCode == "VSCCIN" || opCode == "VSCCOUT" ||
                opCode == "MCCCIN" || opCode == "MCCCOUT" || opCode == "AMEXCCIN" ||
                opCode == "AMEXCCOUT" || opCode == "DSCCIN" || opCode == "DSCCOUT" ||
                    opCode == "CCIN" || opCode == "CCOUT" || opCode == "VSDCIN" ||
                    opCode == "VSDCOUT" || opCode == "MCDCIN" || opCode == "MCDCOUT" ||
                    opCode == "CDIN" || opCode == "CDOUT" || opCode == "SCIN" ||
                    opCode == "SCOUT" || opCode == "PPIN" || opCode == "PPOUT" ||
                opCode == "MVSIN" || opCode == "CMCIN" || opCode == "DCIN" || opCode == "DCOUT" || opCode == "BILLTOAP" || opCode == "RBILLTOAP")
                return true;
            return false;
        }

        public static string GetInOpCode(string tenderType, string cardType)
        {
            if (tenderType == TenderTypes.CASHIN.ToString())
                return "RCREDIT";
            if (tenderType == TenderTypes.CHECK.ToString())
                return "PCIN";
            if (tenderType == TenderTypes.CREDITCARD.ToString() && cardType == CreditCardTypes.AMEX.ToString())
                return "AMEXCCIN";
            if (tenderType == TenderTypes.CREDITCARD.ToString() && cardType == CreditCardTypes.VISA.ToString())
                return "VSCCIN";
            if (tenderType == TenderTypes.CREDITCARD.ToString() && cardType == CreditCardTypes.MASTERCARD.ToString())
                return "MCCCIN";
            if (tenderType == TenderTypes.CREDITCARD.ToString() && cardType == CreditCardTypes.DISCOVER.ToString())
                return "DSCCIN";
            if (tenderType == TenderTypes.DEBITCARD.ToString() && cardType == DebitCardTypes.VISA.ToString())
                return "VSDCIN";
            if (tenderType == TenderTypes.DEBITCARD.ToString() && cardType == DebitCardTypes.MASTERCARD.ToString())
                return "MCDCIN";

            if (tenderType == TenderTypes.DEBITCARD.ToString() && cardType == DebitCardTypes.AMEX.ToString())
                return "AMEXCCIN";
            if (tenderType == TenderTypes.DEBITCARD.ToString() && cardType == DebitCardTypes.DISCOVER.ToString())
                return "DSCCIN";

            if (tenderType == TenderTypes.DEBITCARD.ToString() && cardType == DebitCardTypes.OTHER.ToString())
                return "DCIN";
            if (tenderType == TenderTypes.STORECREDIT.ToString())
                return "SCIN";
            if (tenderType == TenderTypes.COUPON.ToString())
                return "CDIN";
            if (tenderType == TenderTypes.PAYPAL.ToString())
                return "PPIN";
            return string.Empty;
        }

        public static string GetTenderDescription(TenderData tenderData)
        {
            TenderTypes tType;
            CreditCardTypes cType;
            DebitCardTypes dType;
            bool isInbound;

            if (!GetTenderAndCardTypeFromOpCode(tenderData.TenderType, out tType, out cType, out dType, out isInbound))
            {
                return string.Empty;
            }

            string description = string.Empty;
            if (tType == TenderTypes.CREDITCARD)
            {
                description = tenderData.MethodOfPmt + " " + cType.ToString();
            }
            else if (tType == TenderTypes.DEBITCARD)
            {
                description = tenderData.MethodOfPmt + " " + dType.ToString();
            }
            else
            {
                description = tenderData.MethodOfPmt;
            }

            return description.ToUpper();
        }

        public static bool GetTenderAndCardTypeFromOpCode(
            string opCode,
            out TenderTypes tType,
            out CreditCardTypes cType,
            out DebitCardTypes dType,
            out bool isInbound)
        {
            tType = TenderTypes.CASHIN;
            cType = CreditCardTypes.VISA;
            dType = DebitCardTypes.VISA;
            isInbound = true;

            if (string.IsNullOrEmpty(opCode))
            {
                return (false);
            }

            if (opCode == "RDEBIT")
            {
                tType = TenderTypes.CASHOUT;
                isInbound = false;
            }
            else if (opCode == "RCREDIT")
            {
                tType = TenderTypes.CASHIN;
            }
            else if (opCode == "PCOUT")
            {
                tType = TenderTypes.CHECK;
                isInbound = false;
            }
            else if (opCode == "DCIN")
            {
                tType = TenderTypes.DEBITCARD;
                dType = DebitCardTypes.OTHER;
            }
            else if (opCode == "DCOUT")
            {
                tType = TenderTypes.DEBITCARD;
                dType = DebitCardTypes.OTHER;
                isInbound = false;
            }
            else if (opCode == "AMEXCCOUT")
            {
                tType = TenderTypes.CREDITCARD;
                cType = CreditCardTypes.AMEX;
                isInbound = false;
            }
            else if (opCode == "VSCCOUT")
            {
                tType = TenderTypes.CREDITCARD;
                cType = CreditCardTypes.VISA;
                isInbound = false;
            }
            else if (opCode == "MCCCOUT")
            {
                tType = TenderTypes.CREDITCARD;
                cType = CreditCardTypes.MASTERCARD;
                isInbound = false;
            }
            else if (opCode == "DSCCOUT")
            {
                tType = TenderTypes.CREDITCARD;
                cType = CreditCardTypes.DISCOVER;
                isInbound = false;
            }
            else if (opCode == "VSDCOUT")
            {
                tType = TenderTypes.DEBITCARD;
                dType = DebitCardTypes.VISA;
                isInbound = false;
            }
            else if (opCode == "MCDCOUT")
            {
                tType = TenderTypes.DEBITCARD;
                dType = DebitCardTypes.MASTERCARD;
                isInbound = false;
            }
            else if (opCode == "CDOUT")
            {
                tType = TenderTypes.COUPON;
                isInbound = false;
            }
            else if (opCode == "PPOUT")
            {
                tType = TenderTypes.PAYPAL;
                isInbound = false;
            }
            else if (opCode == "PCIN")
            {
                tType = TenderTypes.CHECK;
                isInbound = true;
            }
            else if (opCode == "AMEXCCIN")
            {
                tType = TenderTypes.CREDITCARD;
                cType = CreditCardTypes.AMEX;
                isInbound = true;
            }
            else if (opCode == "VSCCIN")
            {
                tType = TenderTypes.CREDITCARD;
                cType = CreditCardTypes.VISA;
                isInbound = true;
            }
            else if (opCode == "MCCCIN")
            {
                tType = TenderTypes.CREDITCARD;
                cType = CreditCardTypes.MASTERCARD;
                isInbound = true;
            }
            else if (opCode == "DSCCIN")
            {
                tType = TenderTypes.CREDITCARD;
                cType = CreditCardTypes.DISCOVER;
                isInbound = true;
            }
            else if (opCode == "VSDCIN")
            {
                tType = TenderTypes.DEBITCARD;
                dType = DebitCardTypes.VISA;
                isInbound = true;
            }
            else if (opCode == "MCDCIN")
            {
                tType = TenderTypes.DEBITCARD;
                dType = DebitCardTypes.MASTERCARD;
                isInbound = true;
            }
            else if (opCode == "CDIN")
            {
                tType = TenderTypes.COUPON;
                isInbound = true;
            }
            else if (opCode == "PPIN")
            {
                tType = TenderTypes.PAYPAL;
                isInbound = true;
            }
            else if (opCode == "SCIN")
            {
                tType = TenderTypes.STORECREDIT;
                isInbound = false;
            }
            else if (opCode == "SCOUT")
            {
                tType = TenderTypes.STORECREDIT;
                isInbound = true;
            }
            else
            {
                return (false);
            }

            return (true);
        }

        public static string GetOutOpCode(string tenderType, string cardType)
        {
            if (tenderType == TenderTypes.CASHOUT.ToString())
                return "RDEBIT";
            if (tenderType == TenderTypes.CHECK.ToString())
                return "PCOUT";
            if (tenderType == TenderTypes.CREDITCARD.ToString() && cardType == CreditCardTypes.AMEX.ToString())
                return "AMEXCCOUT";
            if (tenderType == TenderTypes.CREDITCARD.ToString() && cardType == CreditCardTypes.VISA.ToString())
                return "VSCCOUT";
            if (tenderType == TenderTypes.CREDITCARD.ToString() && cardType == CreditCardTypes.MASTERCARD.ToString())
                return "MCCCOUT";
            if (tenderType == TenderTypes.CREDITCARD.ToString() && cardType == CreditCardTypes.DISCOVER.ToString())
                return "DSCCOUT";
            if (tenderType == TenderTypes.DEBITCARD.ToString() && cardType == DebitCardTypes.VISA.ToString())
                return "VSDCOUT";
            if (tenderType == TenderTypes.DEBITCARD.ToString() && cardType == DebitCardTypes.MASTERCARD.ToString())
                return "MCDCOUT";

            if (tenderType == TenderTypes.DEBITCARD.ToString() && cardType == DebitCardTypes.AMEX.ToString())
                return "AMEXCCOUT";
            if (tenderType == TenderTypes.DEBITCARD.ToString() && cardType == DebitCardTypes.DISCOVER.ToString())
                return "DSCCOUT";

            if (tenderType == TenderTypes.DEBITCARD.ToString() && cardType == DebitCardTypes.OTHER.ToString())
                return "DCOUT";
            if (tenderType == TenderTypes.STORECREDIT.ToString())
                return "SCOUT";
            if (tenderType == TenderTypes.COUPON.ToString())
                return "CDOUT";
            if (tenderType == TenderTypes.PAYPAL.ToString())
                return "PPOUT";

            return string.Empty;
        }

        public static bool CanBeReturned(string status, string holdType)
        {
            bool allowReturn = false;
            if (status.Equals("PUR") || status.Equals("PFI"))
                allowReturn = true;
            if (holdType == GetHoldDescription(HoldTypes.POLICEHOLD))
                allowReturn = false;
            return allowReturn;

        }

        public static bool IsLayawayEvent(string refEvent)
        {
            return refEvent == ReceiptEventTypes.LAYPMT.ToString() || refEvent == ReceiptEventTypes.LAY.ToString();
        }



        public static State GetStateByName(string fullStateName)
        {
            switch (fullStateName.ToUpper())
            {
                case "ALABAMA":
                    return State.AL;
                case "ALASKA":
                    return State.AK;
                case "AMERICAN SAMOA":
                    return State.AS;
                case "ARIZONA":
                    return State.AZ;
                case "ARKANSAS":
                    return State.AR;
                case "CALIFORNIA":
                    return State.CA;
                case "COLORADO":
                    return State.CO;
                case "CONNECTICUT":
                    return State.CT;
                case "DELAWARE":
                    return State.DE;
                case "DISTRICT OF COLUMBIA":
                    return State.DC;
                case "FEDERATED STATES OF MICRONESIA":
                    return State.FM;
                case "FLORIDA":
                    return State.FL;
                case "GEORGIA":
                    return State.GA;
                case "GUAM":
                    return State.GU;
                case "HAWAII":
                    return State.HI;
                case "IDAHO":
                    return State.ID;
                case "ILLINOIS":
                    return State.IL;
                case "INDIANA":
                    return State.IN;
                case "IOWA":
                    return State.IA;
                case "KANSAS":
                    return State.KS;
                case "KENTUCKY":
                    return State.KY;
                case "LOUISIANA":
                    return State.LA;
                case "MAINE":
                    return State.ME;
                case "MARSHALL ISLANDS":
                    return State.MH;
                case "MARYLAND":
                    return State.MD;
                case "MASSACHUSETTS":
                    return State.MA;
                case "MICHIGAN":
                    return State.MI;
                case "MINNESOTA":
                    return State.MN;
                case "MISSISSIPPI":
                    return State.MS;
                case "MISSOURI":
                    return State.MO;
                case "MONTANA":
                    return State.MT;
                case "NEBRASKA":
                    return State.NE;
                case "NEVADA":
                    return State.NV;
                case "NEW HAMPSHIRE":
                    return State.NH;
                case "NEW JERSEY":
                    return State.NJ;
                case "NEW MEXICO":
                    return State.NM;
                case "NEW YORK":
                    return State.NY;
                case "NORTH CAROLINA":
                    return State.NC;
                case "NORTH DAKOTA":
                    return State.ND;
                case "NORTHERN MARIANA ISLANDS":
                    return State.MP;
                case "OHIO":
                    return State.OH;
                case "OKLAHOMA":
                    return State.OK;
                case "OREGON":
                    return State.OR;
                case "PALAU":
                    return State.PW;
                case "PENNSYLVANIA":
                    return State.PA;
                case "PUERTO RICO":
                    return State.PR;
                case "RHODE ISLAND":
                    return State.RI;
                case "SOUTH CAROLINA":
                    return State.SC;
                case "SOUTH DAKOTA":
                    return State.SD;
                case "TENNESSEE":
                    return State.TN;
                case "TEXAS":
                    return State.TX;
                case "UTAH":
                    return State.UT;
                case "VERMONT":
                    return State.VT;
                case "VIRGIN ISLANDS":
                    return State.VI;
                case "VIRGINIA":
                    return State.VA;
                case "WASHINGTON":
                    return State.WA;
                case "WEST VIRGINIA":
                    return State.WV;
                case "WISCONSIN":
                    return State.WI;
                case "WYOMING":
                    return State.WY;
            }
            throw new System.Exception("State with the name " + fullStateName + " Not Available");
        }
        public enum State { AL, AK, AS, AZ, AR, CA, CO, CT, DE, DC, FM, FL, GA, GU, HI, ID, IL, IN, IA, KS, KY, LA, ME, MH, MD, MA, MI, MN, MS, MO, MT, NE, NV, NH, NJ, NM, NY, NC, ND, MP, OH, OK, OR, PW, PA, PR, RI, SC, SD, TN, TX, UT, VT, VI, VA, WA, WV, WI, WY }

    }


}
