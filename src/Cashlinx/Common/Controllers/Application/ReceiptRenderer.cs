using System;

namespace Common.Controllers.Application
{
    public class ReceiptRenderer
    {
        public static string GetEPSONprinterString(string detailStr)
        {
            string printerString = null;
            string ONE_SPACE = " ";
            string EMPTY_SPACE = string.Empty;
            string ESC = ((char)0x1b) + EMPTY_SPACE;
            string LA_EXPR = ESC + ((char)0x61) + "" + ((char)0x0);
            //string CA_EXPR = ESC + ((char)0x61) + "" + ((char)0x1);
            //string RA_EXPR = ESC + ((char)0x61) + "" + ((char)0x2);
            string BOLD_EXPR = ESC + ((char)0x45) + "" + ((char)0x1);
            string BOLD_OFF_EXPR = ESC + ((char)0x45) + "" + ((char)0x0);
  
            //string RA_OFF_EXPR = ESC + ((char)0x61) + "" + ((char)0x0);
            string LF = ((char)0x0a) + EMPTY_SPACE;
            //string CR = ((char)0x0d) + EMPTY_SPACE;
            const string CONSTANT_SINGLE_LINEBREAK = " -----------------------------------------";

            try
            {
                //StringBuilder buffer = new StringBuilder();
                if (detailStr.Substring(0, 3).Equals("<B>") ||
                    detailStr.Substring(0, 3).Equals("<b>"))
                {
                    printerString = (LA_EXPR + BOLD_EXPR + ONE_SPACE +
                                     detailStr.Substring(3, (detailStr.Length - 3)) + BOLD_OFF_EXPR + LF);
                    return printerString;
                }
                if (detailStr.Substring(0, 3).Equals("<R>") ||
                    detailStr.Substring(0, 3).Equals("<r>"))
                {
                    printerString = (LA_EXPR + ONE_SPACE +
                                     detailStr.Substring(3, (detailStr.Length - 3)) + LF);
                    return printerString;
                }
                if (detailStr.Substring(0, 3).Equals("<L>") ||
                    detailStr.Substring(0, 3).Equals("<l>"))
                {
                    printerString = (LA_EXPR + CONSTANT_SINGLE_LINEBREAK + LF);
                    return printerString;
                }
                if (detailStr.Substring(0, 3).Equals("<S>") ||
                    detailStr.Substring(0, 3).Equals("<s>"))
                {
                    printerString = (LA_EXPR + LF);
                    return printerString;
                }
                printerString = (LA_EXPR + detailStr + LF);
                return printerString;
            }
            catch (Exception)
            {
                return printerString;
            }
        }
    }
}