/************************************************************************
* Namespace:       PawnUtilities.Graphics
* Class:           GraphicsUtilities
* 
* Description      Class has re-useable method(s) for various routines
* 
* History
* David D Wise, 4-08-2009, Initial Development
* David D Wise, 6-30-2010, Added GetPhoneNumber
* 
* **********************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Utility
{
    //DO NOT RENAME THIS CLASS TO MATCH THE FILENAME - GJL 01/17/2012
    //DO NOT RENAME THE FILE TO MATCH THE CLASS NAME - GJL 01/17/2012
    public static class Utilities
    {
        public static PfiAssignment AssignmentTypeFromString(string assignment)
        {
            PfiAssignment sPfiAssignmentType = PfiAssignment.Normal;
            switch (assignment)
            {
                case "C":
                    sPfiAssignmentType = PfiAssignment.CAF;
                    break;
                case "R":
                    sPfiAssignmentType = PfiAssignment.Refurb;
                    break;
                case "S":
                    sPfiAssignmentType = PfiAssignment.Scrap;
                    break;
                case "SB":
                    sPfiAssignmentType = PfiAssignment.Sell_Back;
                    break;
                case "E":
                    sPfiAssignmentType = PfiAssignment.Excess;
                    break;
            }
            return sPfiAssignmentType;
        }

        public static string AssignmentTypeFullNameFromString(string assignment)
        {
            string sPfiAssignmentType = "Normal";
            switch (assignment)
            {
                case "C":
                    sPfiAssignmentType = "CAF";
                    break;
                case "R":
                    sPfiAssignmentType = "Refurb";
                    break;
                case "S":
                    sPfiAssignmentType = "Scrap";
                    break;
                case "SB":
                    sPfiAssignmentType = "Sell_Back";
                    break;
                case "E":
                    sPfiAssignmentType = "Excess";
                    break;
            }
            return sPfiAssignmentType;
        }

        public static string AssignmentTypeFullName(PfiAssignment assignment)
        {
            string sPfiAssignmentType = "Normal";
            switch (assignment)
            {
                case PfiAssignment.CAF:
                    sPfiAssignmentType = "CAF";
                    break;
                case PfiAssignment.Refurb:
                    sPfiAssignmentType = "Refurb";
                    break;
                case PfiAssignment.Scrap:
                    sPfiAssignmentType = "Scrap";
                    break;
                case PfiAssignment.Sell_Back:
                    sPfiAssignmentType = "Sell_Back";
                    break;
                case PfiAssignment.Excess:
                    sPfiAssignmentType = "Excess";
                    break;
            }
            return sPfiAssignmentType;
        }

        public static string AssignmentTypeToString(PfiAssignment assignment)
        {
            string sPfiAssignmentType = "N";
            switch (assignment)
            {
                case PfiAssignment.CAF:
                    sPfiAssignmentType = "C";
                    break;
                case PfiAssignment.Refurb:
                    sPfiAssignmentType = "R";
                    break;
                case PfiAssignment.Scrap:
                    sPfiAssignmentType = "S";
                    break;
                case PfiAssignment.Sell_Back:
                    sPfiAssignmentType = "SB";
                    break;
                case PfiAssignment.Excess:
                    sPfiAssignmentType = "E";
                    break;
            }
            return sPfiAssignmentType;
        }

        // Draws a border around Control.  Used for Controls having focus
        public static void BorderColor(Control parentControl, Control tmpControl, Color tmpColor)
        {
            Graphics g = parentControl.CreateGraphics();
            using (var pen = new Pen(tmpColor, 0.25f))
            {
                Point point = tmpControl.Location;
                point.X = point.X - 1;
                point.Y = point.Y - 1;
                Size size = tmpControl.Size;
                size.Width = size.Width + 1;
                size.Height = size.Height + 1;
                g.DrawRectangle(pen, new Rectangle(point, size));
            }
        }

        /// <summary>
        /// Get a byte[] from a stream of data
        /// </summary>
        /// <param name="stream">data stream</param>
        /// <returns>byte[]</returns>
        public static byte[] ReadStream(Stream stream)
        {
            var buffer = new byte[32768];
            using (var ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }

        /// <summary>
        /// Get a byte[] from a stream of data
        /// </summary>
        /// <param name="stream">data stream</param>
        /// <param name="initialLength">length of byte buffer to use</param>
        /// <returns>byte[]</returns>
        public static byte[] ReadStream(Stream stream, long initialLength)
        {
            // If we've been passed an unhelpful initial length, just
            // use 32K.
            if (initialLength < 1)
            {
                initialLength = 32768;
            }

            var buffer = new byte[initialLength];
            int read = 0;

            int chunk;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;

                // If we've reached the end of our buffer, check to see if there's
                // any more information
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();

                    // End of stream? If so, we're done
                    if (nextByte == -1)
                    {
                        return buffer;
                    }

                    // Nope. Resize the buffer, put in the byte we've just
                    // read, and continue
                    var newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }
            // Buffer is now too big. Shrink it.
            var ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }

        // Provides Decimal value back.  
        public static decimal GetCurrencyValueFromMaskedTextBox(object oValueToParse, int iDecimalPositions, decimal dDefaultValue)
        {
            try
            {
                string decSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
                string sValueToParse = oValueToParse.ToString();
                int index = sValueToParse.IndexOf(decSeparator, StringComparison.OrdinalIgnoreCase);
                int decPlaces;

                if (index == -1)
                    decPlaces = 0;
                else
                    decPlaces = sValueToParse.Length - (index + decSeparator.Length);

                decPlaces = decPlaces > iDecimalPositions ? iDecimalPositions : decPlaces;

                if (iDecimalPositions - decPlaces > 0)
                {
                    sValueToParse += new string('0', iDecimalPositions - decPlaces);
                }

                return Convert.ToDecimal(sValueToParse);
            }
            catch
            {
                return dDefaultValue;
            }
        }

        // Provides Phone Number back
        public static string GetPhoneNumber(string sPhoneNumber)
        {
            try
            {
                sPhoneNumber = sPhoneNumber.Replace("(", "");
                sPhoneNumber = sPhoneNumber.Replace(")", "");
                sPhoneNumber = sPhoneNumber.Replace(" ", "");
                sPhoneNumber = sPhoneNumber.Replace("-", "");

                int iLength = sPhoneNumber.Length;
                switch (iLength)
                {
                    case 3:
                        sPhoneNumber = sPhoneNumber + "-XXXX";
                        break;
                    case 4:
                        sPhoneNumber = "XXX-" + sPhoneNumber;
                        break;
                    case 7:
                        sPhoneNumber = sPhoneNumber.Substring(0, 3) + "-" + sPhoneNumber.Substring(3);
                        break;
                    case 10:
                        //Madhu fix for BZ 9
                        sPhoneNumber = sPhoneNumber.Substring(0, 3) + " " + sPhoneNumber.Substring(3, 3) + "-" + sPhoneNumber.Substring(6);
                        break;
                }
            }
            catch (System.Exception eX)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, null, "Error occurred while formatting phone number: " + eX);
                }
            }
            return sPhoneNumber;
        }

        public static bool IsJewelry(int catCode)
        {
            if (catCode != 0 && catCode <= 1999)
            {
                return true;
            }
            return false;
        }

        // Provides Boolean value back.  
        public static bool GetBooleanValue(object oValueToParse, bool dDefaultValue)
        {
            try
            {
                if (oValueToParse == null || oValueToParse == DBNull.Value)
                {
                    return dDefaultValue;
                }

                return bool.Parse(oValueToParse.ToString());
            }
            catch
            {
                return dDefaultValue;
            }
        }

        // Provides Boolean value back with default false
        public static bool GetBooleanValue(object oValueToParse)
        {
            return GetBooleanValue(oValueToParse, false);
        }

        // Provides Char value back.  
        public static char GetCharValue(object oValueToParse, char dDefaultValue)
        {
            try
            {
                if (oValueToParse == null || oValueToParse == DBNull.Value)
                {
                    return dDefaultValue;
                }

                return char.Parse(oValueToParse.ToString());
            }
            catch
            {
                return dDefaultValue;
            }
        }

        // Provides Char value back with default Char.MinValue.  
        public static char GetCharValue(object oValueToParse)
        {
            return GetCharValue(oValueToParse, Char.MinValue);
        }

        // Provides Date value back.  
        public static DateTime GetDateTimeValue(object oValueToParse, DateTime dDefaultValue)
        {
            try
            {
                if (oValueToParse == null || oValueToParse == DBNull.Value)
                {
                    return dDefaultValue;
                }

                return DateTime.Parse(oValueToParse.ToString());
            }
            catch
            {
                return dDefaultValue;
            }
        }

        // Provides Date value back with default MinValue
        public static DateTime GetDateTimeValue(object oValueToParse)
        {
            return GetDateTimeValue(oValueToParse, DateTime.MinValue);
        }

        // Provides Decimal value back.  
        public static decimal GetDecimalValue(object oValueToParse, decimal dDefaultValue)
        {
            try
            {
                if (oValueToParse == null || oValueToParse == DBNull.Value)
                {
                    return dDefaultValue;
                }

                return decimal.Parse(oValueToParse.ToString());
            }
            catch
            {
                return dDefaultValue;
            }
        }

        // Provides Decimal value back with min value
        public static decimal GetDecimalValue(object oValueToParse)
        {
            return GetDecimalValue(oValueToParse, 0);
        }

        // Provides double value back.  
        public static double GetDoubleValue(object oValueToParse, double dDefaultValue)
        {
            try
            {
                if (oValueToParse == null || oValueToParse == DBNull.Value)
                {
                    return dDefaultValue;
                }

                return double.Parse(oValueToParse.ToString());
            }
            catch
            {
                return dDefaultValue;
            }
        }

        // Provides Double value back with MinValue
        public static double GetDoubleValue(object oValueToParse)
        {
            return GetDoubleValue(oValueToParse, double.MinValue);
        }

        // Provides Float value back.  
        public static float GetFloatValue(object oValueToParse, float fDefaultValue)
        {
            try
            {
                if (oValueToParse == null || oValueToParse == DBNull.Value)
                {
                    return fDefaultValue;
                }

                return float.Parse(oValueToParse.ToString());
            }
            catch
            {
                return fDefaultValue;
            }
        }

        // Provides Float value back with default minvalue
        public static float GetFloatValue(object oValueToParse)
        {
            return GetFloatValue(oValueToParse, float.MinValue);
        }

        // Provides Integer value back.  
        public static int GetIntegerValue(object oValueToParse, int iDefaultValue)
        {
            try
            {
                if (oValueToParse == null || oValueToParse == DBNull.Value)
                {
                    return iDefaultValue;
                }

                return Int32.Parse(oValueToParse.ToString());
            }
            catch
            {
                return iDefaultValue;
            }
        }

        // Provides Integer value back with MinValue
        public static int GetIntegerValue(object oValueToParse)
        {
            return GetIntegerValue(oValueToParse, int.MinValue);
        }

        // Provides Integer value back.  
        public static int? GetNullableIntegerValue(object oValueToParse, int? iDefaultValue)
        {
            try
            {
                if (oValueToParse == null || oValueToParse == DBNull.Value)
                {
                    return iDefaultValue;
                }

                return Int32.Parse(oValueToParse.ToString());
            }
            catch
            {
                return iDefaultValue;
            }
        }

        // Provides Integer value back with MinValue
        public static int? GetNullableIntegerValue(object oValueToParse)
        {
            return GetNullableIntegerValue(oValueToParse, null);
        }

        // Provides String value back. 
        public static string GetStringValue(object oValueToParse, string sDefaultValue)
        {
            try
            {
                if (oValueToParse == null || oValueToParse == DBNull.Value)
                {
                    return sDefaultValue;
                }

                return oValueToParse.ToString();
            }
            catch
            {
                return sDefaultValue;
            }
        }

        // Provides String value back as string.Empty
        public static string GetStringValue(object oValueToParse)
        {
            return GetStringValue(oValueToParse, string.Empty);
        }

        //provides long value back
        public static long GetLongValue(object oValueToParse, long iDefaultValue)
        {
            try
            {
                if (oValueToParse == null || oValueToParse == DBNull.Value)
                {
                    return iDefaultValue;
                }

                return long.Parse(oValueToParse.ToString());
            }
            catch
            {
                return iDefaultValue;
            }
        }

        //provides long value back with MinValue
        public static long GetLongValue(object oValueToParse)
        {
            return GetLongValue(oValueToParse, long.MinValue);
        }

        public static ulong GetULongValue(object oValueToParse)
        {
            return GetULongValue(oValueToParse, ulong.MinValue);
        }

        public static ulong GetULongValue(object oValueToParse, ulong iDefaultValue)
        {
            if (oValueToParse == null || oValueToParse == DBNull.Value)
            {
                return iDefaultValue;
            }

            ulong rt;
            if (!ulong.TryParse(oValueToParse.ToString(), out rt))
            {
                rt = iDefaultValue;
            }
            return (rt);
        }

        public static DateTime GetTimestampValue(object oValueTimestamp, DateTime defaultVal)
        {
            DateTime tStampVal;
            if (oValueTimestamp != null && oValueTimestamp is DateTime)
            {
                tStampVal = (DateTime)oValueTimestamp;
            }
            else if (oValueTimestamp != null)
            {
                try
                {
                    tStampVal = DateTime.Parse(oValueTimestamp.ToString());
                }
                catch (System.Exception)
                {
                    tStampVal = defaultVal;
                }
            }
            else
            {
                tStampVal = defaultVal;
            }

            return (tStampVal);
        }

        public static DateTime GetTimestampValue(object oValueTimestamp)
        {
            DateTime tStampVal;
            if (oValueTimestamp != null && oValueTimestamp is DateTime)
            {
                tStampVal = (DateTime)oValueTimestamp;
            }
            else if (oValueTimestamp != null)
            {
                try
                {
                    tStampVal = DateTime.Parse(oValueTimestamp.ToString());
                }
                catch (System.Exception)
                {
                    tStampVal = DateTime.MinValue;
                }
            }
            else
            {
                tStampVal = DateTime.MinValue;
            }

            return (tStampVal);
        }

        public static void WaitMillis(int millisecondsToWait)
        {
            if (millisecondsToWait < 0)
                return;

            DateTime start = DateTime.Now;
            while (true)
            {
                DateTime finish = DateTime.Now;
                TimeSpan span = finish - start;
                double totalMillis = span.TotalMilliseconds;
                var totalMillisWhole = (int)(Math.Floor(totalMillis));
                if (totalMillisWhole >= millisecondsToWait)
                {
                    break;
                }
            }
        }

        public static T DeepCopy<T>(T obj)
        {
            object result;

            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                result = (T)formatter.Deserialize(ms);
                ms.Close();
            }

            return (T)result;
        }

        public static T CloneObject<T>(T tempObject) where T : class
        {
            System.Type tp = tempObject.GetType();
            object clonePawnLoan = Activator.CreateInstance(tp);

            try
            {
                StringWriter outStream = new StringWriter();
                XmlSerializer ser = new XmlSerializer(tp);
                ser.Serialize(outStream, tempObject);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(outStream.ToString());

                XmlNodeReader reader = new XmlNodeReader(doc.DocumentElement);
                T myObject = (T)ser.Deserialize(reader);

                // Need to cast obj into whatever type it is eg:
                if (myObject != null)
                {
                    clonePawnLoan = myObject;
                }
            }
// ReSharper disable EmptyGeneralCatchClause
            catch
// ReSharper restore EmptyGeneralCatchClause
            {
            }
            return (T)clonePawnLoan;
        }

        public static T DeSerialize<T>(string sSerializedData)
        {
            object clonePawnLoan = Activator.CreateInstance(typeof(T));

            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(sSerializedData);

                XmlNodeReader reader = new XmlNodeReader(doc.DocumentElement);
                T myObject = (T)ser.Deserialize(reader);

                // Need to cast obj into whatever type it is eg:
                if (myObject != null)
                {
                    clonePawnLoan = myObject;
                }
            }
// ReSharper disable EmptyGeneralCatchClause
            catch
// ReSharper restore EmptyGeneralCatchClause
            {
            }
            return (T)clonePawnLoan;
        }

        public static string Serialize<T>(T tempObject) where T : class
        {
            string sSerializedString = "";
            StringBuilder sb = new StringBuilder();

            System.Type tp = tempObject.GetType();

            try
            {
                XmlSerializer ser = new XmlSerializer(tp);
                using (XmlWriter writer = XmlWriter.Create(sb))
                {
                    ser.Serialize(writer, tempObject);
                    writer.Flush();
                }

                sSerializedString = sb.ToString();
            }
            catch (System.Exception ex)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, null, ex.Message);
            }
            return sSerializedString;
        }

        public static string IcnGenerator(int iStore, int iYear, int iDocNumber, string sDocType, int iItemOrder, int iSubItemOrder)
        {
            var sYear = string.Empty;

            if (iYear.ToString().Length > 0)
                sYear = iYear.ToString().Substring(iYear.ToString().Length - 1);

            var sICN = iStore.ToString().PadLeft(5, '0');
            sICN += sYear;
            sICN += iDocNumber.ToString().PadLeft(6, '0');
            sICN += sDocType;
            sICN += iItemOrder.ToString().PadLeft(3, '0');
            sICN += iSubItemOrder.ToString().PadLeft(2, '0');
            return sICN;
        }

        public static bool IsIcnValid(string icn)
        {
            if (icn == null || icn.Trim().Length == 0)
            {
                return false;
            }

            bool isValidICN = false;

            if (icn.Length <= 10)// Short ICN Search
            {
                if (!Regex.IsMatch(icn, "^[\\d]+\\.[\\d]+$"))
                {
                    return false;
                }

                int shortICN_dotLoctn = icn.IndexOf('.');

                if (shortICN_dotLoctn > 0 && shortICN_dotLoctn < icn.Length)
                {
                    string[] shortICN = icn.Split(new char[] { '.' });

                    if (shortICN.Length == 2)
                    {
                        string ICNStore = shortICN[0].PadLeft(6, '0');
                        string ICNItem = shortICN[1];

                        isValidICN = (ICNItem.Length > 0 && ICNItem.Length <= 3);

                        if (ICNItem.Length > 0 && ICNItem.Length <= 3)
                            ICNItem = ICNItem.PadLeft(3, '0');

                        string ICN = ICNStore + ICNItem;

                        icn = ICN;

                        isValidICN = isValidICN && (icn.Length == 9);
                    }
                }
            }
            else if (icn.Length == 18)
            {
                if (Regex.IsMatch(icn, "^[\\d]{18}$"))
                {
                    isValidICN = true;
                }
            }

            return isValidICN;
        }

        public static bool IsGun(Int64 lGunNumber, int iCatCode, bool bIsJewelry, string sMerchandiseType)
        {
            // proceed if Merchandise was flagged as non Jewelry
            if (!bIsJewelry)
            {
                if (lGunNumber > 0)
                    return true;
                // See if Merchandise was flagged as Gun
                if (sMerchandiseType == "H" || sMerchandiseType == "L")
                    return true;
                // See if Merchandise Category Code began with Gun-related prefix
                if (iCatCode > 0)
                {
                    string sCategoryCodePrefix = iCatCode.ToString().Substring(0, 2);

                    if (sCategoryCodePrefix == "41"
                              || sCategoryCodePrefix == "42"
                              || sCategoryCodePrefix == "43")
                        return true;
                }
            }



            return false;
        }

        /// <summary>
        /// Checks whether a temp status is a PFI status
        /// </summary>
        /// <param name="tempstatus"></param>
        /// <returns></returns>
        public static bool IsPFI(StateStatus tempstatus)
        {
            if (tempstatus == StateStatus.PFIE ||
                tempstatus == StateStatus.PFIW ||
                tempstatus == StateStatus.PFI ||
                tempstatus == StateStatus.PFIS ||
                tempstatus == StateStatus.PFIL)
                return true;
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks whether an object/value type is null/default.  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNullOrDefault<T>(T input)
        {
            return (Equals(input, default(T)));
        }

        public static object GetEnumFromConstantName<T>(string inputStr)
        {
            if (string.IsNullOrEmpty(inputStr))
                return (null);

            return ((T)Enum.Parse(typeof(T), inputStr, true));
        }

        public static bool CanCurrencyValueDistibuteOverItems(decimal currencyValue, int numberOfItems)
        {
            if (numberOfItems <= 0)
            {
                return false;
            }

            if (currencyValue == 0)
            {
                return true;
            }

            int numberOfCents = (int)(Math.Abs(Math.Round(currencyValue, 2)) * 100);
            return numberOfCents >= numberOfItems;
        }

        public static decimal[] GetDistributeValuesForCurrencyOverItems(decimal currencyValue, int numberOfItems)
        {
            List<decimal> values = new List<decimal>();

            if (numberOfItems == 0)
            {
                return new decimal[0];
            }

            if (currencyValue != 0)
            {
                int numberOfCents = (int)(Math.Round(currencyValue, 2) * 100);
                int amountPerItem = numberOfCents / numberOfItems;

                if (amountPerItem != 0)
                {
                    for (int i = 0; i < numberOfItems && Math.Abs(numberOfCents) > 0; i++)
                    {
                        values.Add((decimal)amountPerItem / 100M);
                        numberOfCents -= amountPerItem;
                    }
                }

                int idx = 0;
                decimal incrementValue = currencyValue > 0M ? .01M : -.01M;
                while (Math.Abs(numberOfCents) > 0)
                {
                    if (values.Count < numberOfItems)
                    {
                        values.Add(incrementValue);
                    }
                    else
                    {
                        values[idx] += incrementValue;
                        idx++;
                    }
                    numberOfCents -= currencyValue > 0M ? 1 : -1;
                }
            }

            while (numberOfItems > values.Count)
            {
                values.Add(0M);
            }

            return values.ToArray();
        }

        public static T ParseEnum<T>(string value, T defaultValue)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool IsValidFFL(string ffl)
        {
            if (string.IsNullOrEmpty(ffl))
            {
                return false;
            }

            return ffl.Trim().Length == 15;
        }
    }
}