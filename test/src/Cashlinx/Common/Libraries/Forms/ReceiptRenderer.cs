using System;
using System.Windows.Forms;

namespace Common.Libraries.Forms
{
    public partial class ReceiptRenderer : Form
    {
        #region Constructors
        public ReceiptRenderer(byte[] receiptByteArray)
        {
            InitializeComponent();
            LoadReceipt(receiptByteArray);
        }

        #endregion

        #region Private Methods
        private void LoadReceipt(byte[] byteArrayReceipt)
        {
            //string EMPTY_SPACE = String.Empty;
            char ESC = ((char)0x1b); //replace
            char LA_EXPR = ((char)0x61);
            //char CA_EXPR = ((char)0x1);
            char RA_EXPR = ((char)0x2); //replace
            char BOLD_EXPR = ((char)0x45); //replace
            char BOLD_OFF_EXPR = ((char)0x0);
            //string newLineChar = "\n";
            string asciiString = System.Text.Encoding.ASCII.GetString(byteArrayReceipt);
            string escAndLAExpr = ESC.ToString() + LA_EXPR.ToString();
            string escAndBold_Expr = ESC.ToString() + BOLD_EXPR.ToString();
            string strBOLD_OFF_EXPR = BOLD_OFF_EXPR.ToString();

            //remove barCode information from asciiString
            int RA_EXPRLocationWithinString = asciiString.IndexOf(RA_EXPR);
            int lengthOfEntireString = asciiString.Length;
            int escLocationB4Barcode2 = -1;
            int escLocationB4Barcode3 = -1;
            int escLocationB4Barcode = asciiString.LastIndexOf(escAndLAExpr, RA_EXPRLocationWithinString);
            if (escLocationB4Barcode >= 0)
                escLocationB4Barcode2 = asciiString.LastIndexOf(escAndLAExpr, escLocationB4Barcode);

            if (escLocationB4Barcode2 >= 0)
                escLocationB4Barcode3 = asciiString.LastIndexOf(escAndLAExpr, escLocationB4Barcode2);
            int diffLength = lengthOfEntireString - ((escLocationB4Barcode3 > 0) ? escLocationB4Barcode3: 0);

            string subStrintToReplace = asciiString.Substring(((escLocationB4Barcode3 > 0) ? escLocationB4Barcode3 : 0), diffLength);
            //int newLineCharLocationAfterBarcode = asciiString.IndexOf(newLineChar, RA_EXPRLocationWithinString);
            //int lengthofBarcode = newLineCharLocationAfterBarcode - escLocationB4Barcode;
            //string barcodeSubstring = asciiString.Substring(escLocationB4Barcode, lengthofBarcode);
            asciiString = asciiString.Replace(subStrintToReplace, string.Empty);
            asciiString = asciiString.Replace(escAndLAExpr, string.Empty);
            asciiString = asciiString.Replace(escAndBold_Expr, string.Empty);
            asciiString = asciiString.Replace(strBOLD_OFF_EXPR, string.Empty);
            //asciiString = asciiString.Replace("=", string.Empty);
            //ReceiptStringData = asciiString;
            rendererControl.Text = asciiString;
            rendererControl.SelectAll();
            rendererControl.SelectionAlignment = HorizontalAlignment.Center;
            rendererControl.DeselectAll();
        }

        #endregion

        #region Public Static Methods
        public static string GetEPSONprinterString(string detailStr)
        {
            string printerString = null;
            string ONE_SPACE = " ";
            string EMPTY_SPACE = String.Empty;
            string ESC = ((char)0x1b) + EMPTY_SPACE;
            string LA_EXPR = ESC + ((char)0x61) + "" + ((char)0x0);
            //string CA_EXPR = ESC + ((char)0x61) + "" + ((char)0x1);
            //string RA_EXPR = ESC + ((char)0x61) + "" + ((char)0x2);
            string BOLD_EXPR = ESC + ((char)0x45) + "" + ((char)0x1);
            string BOLD_OFF_EXPR = ESC + ((char)0x45) + "" + ((char)0x0);
            //string RA_OFF_EXPR = ESC + ((char)0x61) + "" + ((char)0x0);
            string LF = ((char)0x0a) + EMPTY_SPACE;
            //string CR = ((char)0x0d) + EMPTY_SPACE;
            string CONSTANT_SINGLE_LINEBREAK = " -----------------------------------------";
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
        #endregion
    }
}