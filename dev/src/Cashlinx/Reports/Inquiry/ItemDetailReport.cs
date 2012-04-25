using System;
using System.Data;
using Common.Libraries.Objects;
using Common.Libraries.Utility.String;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ReportObject = Common.Controllers.Application.ReportObject;
using Common.Libraries.Utility.Logger;

namespace Reports.Inquiry
{
    public class ItemDetailReport : ReportBase
    {
        #region Fields
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        //private BaseFont footerBaseFontGunTotals = null;
        //private PdfTemplate templateGunTotals;
        public RunReport runReport;
        private int _pageCount = 1;
        private int _totalNumGuns = 0;
        private DataView _pawnInfo;
        private DataView _pawnMdse;
        private int _pawnRowNum;
        private int _pawnInfoRowNum = 0;
        #endregion

        public ItemDetailReport(IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {

            
        }

        #region Private Properties
        private int PageCount
        {
            get
            {
                return _pageCount;
            }
            set
            {
                _pageCount = value;
            }
        }

        private int TotalNumberOfGuns
        {
            get
            {
                return _totalNumGuns;
            }
            set
            {
                _totalNumGuns = value;
            }
        }
        #endregion

        #region Private Methods
        private void WriteDetail(PdfPTable table, int colspan)
        {
            decimal itemAmt = _pawnMdse[_pawnRowNum]["ITEM_AMT"] is System.DBNull ? 0.00m : Convert.ToDecimal(_pawnMdse[_pawnRowNum]["ITEM_AMT"]);
            decimal totItemAmt = _pawnMdse[_pawnRowNum]["AMOUNT"] is System.DBNull ? 0.00m : Convert.ToDecimal(_pawnMdse[_pawnRowNum]["AMOUNT"]);
            decimal suggestedLoan = _pawnMdse[_pawnRowNum]["SUGGESTED_LOAN"] is System.DBNull ? 0.00m : Convert.ToDecimal(_pawnMdse[_pawnRowNum]["SUGGESTED_LOAN"]);
            decimal cost = _pawnMdse[_pawnRowNum]["PFI_AMOUNT"] is System.DBNull ? 0.00m : Convert.ToDecimal(_pawnMdse[_pawnRowNum]["PFI_AMOUNT"]);
            decimal suggestedRetail = _pawnMdse[_pawnRowNum]["SUGGESTED_RETAIL"] is System.DBNull ? 0.00m : Convert.ToDecimal(_pawnMdse[_pawnRowNum]["SUGGESTED_RETAIL"]);
            decimal retailPrice = _pawnMdse[_pawnRowNum]["RETAIL_PRICE"] is System.DBNull ? 0.00m : Convert.ToDecimal(_pawnMdse[_pawnRowNum]["RETAIL_PRICE"]);
            string statusDate = _pawnInfo[_pawnInfoRowNum]["STATUS_DATE"] is System.DBNull ? string.Empty : Convert.ToDateTime(_pawnInfo[_pawnInfoRowNum]["STATUS_DATE"]).ToShortDateString();
            WriteCell(table, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(table, "Shop:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnInfo[_pawnInfoRowNum]["STORENUMBER"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "ICN: ", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            PdfPCell cell = new PdfPCell();
            cell = GetFormattedICNCellSmallFont(_pawnMdse[_pawnRowNum]["ICN"].ToString());
            cell.Colspan = 3;
            table.AddCell(cell);
            WriteCell(table, "Status: ", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["STATUS_CD"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(table, "Quantity:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["QUANTITY"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "Temp/Real XREF: ", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["XICN"].ToString(), ReportFontMedium, 3, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "Status Date: ", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, statusDate, ReportFontMedium, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(table, "Amount Per Item:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, itemAmt.ToString("C"), ReportFontMedium, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "Category: ", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["CAT_CODE"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "Days Since Sale:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["DAYS_SINCE_SALE"].ToString(), ReportFontMedium, 3, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(table, "Total Item Amount:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, totItemAmt.ToString("C"), ReportFontMedium, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "Suggested Loan: ", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, suggestedLoan.ToString("C"), ReportFontMedium, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "CACC:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["ISCACC"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "CACC Level:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["CACC_LEVEL"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER); //NEED TO RESOLVE

            WriteCell(table, "Cost:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, cost.ToString("C"), ReportFontMedium, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "Suggested Retail: ", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, suggestedRetail.ToString("C"), ReportFontMedium, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "RFB #:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["RFB_NO"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            //WriteCell(table, "RFB Type:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, string.Empty, ReportFontMedium, 2, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);//NEED TO RESOLVE

            WriteCell(table, "Retail Amount:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, retailPrice.ToString("C"), ReportFontMedium, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "Gun Number: ", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["GUN_NUMBER"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "Jewelry Case:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["JCASE"].ToString(), ReportFontMedium, 3, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(table, "Disp Type:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["DISP_TYPE"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "Disp Date: ", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["DISP_DATE"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "Disp Doc#:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["DISP_DOC"].ToString(), ReportFontMedium, 3, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(table, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.TOP_BORDER);

            WriteCell(table, "Manufacturer:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["MANUFACTURER"].ToString(), ReportFontMedium, 3, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "WT: ", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["WEIGHT"].ToString(), ReportFontMedium, 3, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(table, "Model:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["MODEL"].ToString(), ReportFontMedium, 3, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "Located?: ", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            string locShelf = string.Empty;
            string locAisle = string.Empty;
            string location = string.Empty;
            string located = string.Empty;
            if (!(_pawnMdse[_pawnRowNum]["LOC_SHELF"] is System.DBNull))
                locShelf = _pawnMdse[_pawnRowNum]["LOC_SHELF"].ToString();
            if (!(_pawnMdse[_pawnRowNum]["LOC_AISLE"] is System.DBNull))
                locAisle = _pawnMdse[_pawnRowNum]["LOC_AISLE"].ToString();
            if (!(_pawnMdse[_pawnRowNum]["LOCATION"] is System.DBNull))
                location = _pawnMdse[_pawnRowNum]["LOCATION"].ToString();
            if (!string.IsNullOrEmpty(locShelf))
                located = " Shelf: " + locShelf;
            if (!string.IsNullOrEmpty(locAisle))
                located += " Aisle: " + locAisle;
            if (!string.IsNullOrEmpty(location))
                located += " " + location;
            WriteCell(table, _pawnMdse[_pawnRowNum]["LOC"].ToString(), ReportFontMedium, 3, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(table, "Serial Number:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["SERIAL_NUMBER"].ToString(), ReportFontMedium, 3, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "Location: ", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, location, ReportFontMedium, 3, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(table, "Description:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, _pawnMdse[_pawnRowNum]["MD_DESC"].ToString(), ReportFontMedium, 7, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

        }


        private void ReportHeader(PdfPTable table, Image gif, ItemDetailReport pageEvent, int colspan)
        {
            PdfPCell cell = new PdfPCell();

            //  row 1
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = colspan;
            table.AddCell(cell);
            string dateMade = _pawnInfo[_pawnInfoRowNum]["DATE_MADE"] is System.DBNull ? string.Empty : Convert.ToDateTime(_pawnInfo[_pawnInfoRowNum]["DATE_MADE"]).ToShortDateString();
            //row 2
            WriteCell(table, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(table, DateTime.Now.ToString(), ReportFontSmall, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, "Item Detail", ReportFontHeading, colspan, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(table, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WritePhrase(table, CombinePhrases("Name:", _pawnInfo[_pawnInfoRowNum]["CUST_NAME"].ToString()), ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WritePhrase(table, CombinePhrases("Cust #:", _pawnInfo[_pawnInfoRowNum]["CUSTOMERNUMBER"].ToString()), ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WritePhrase(table, CombinePhrases("DOB:", ReportObject.InquiryItemDetails_DOB), ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WritePhrase(table, CombinePhrases("Customer Since:", ReportObject.InquiryItemDetails_Since), ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.BOTTOM_BORDER);

            WritePhrase(table, CombinePhrases("Ticket #:", _pawnInfo[_pawnInfoRowNum]["TICKET_NUMBER"].ToString()), ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WritePhrase(table, CombinePhrases("Date Made:", dateMade), ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WritePhrase(table, CombinePhrases("Loan Amount:", Convert.ToDecimal(_pawnInfo[_pawnInfoRowNum]["PRIN_AMOUNT"]).ToString("C")), ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WritePhrase(table, CombinePhrases("Status:", _pawnInfo[_pawnInfoRowNum]["STATUS_CD"].ToString()), ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(table, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.BOTTOM_BORDER);

        }
        #endregion

        #region Public Properties
        public ReportObject ReportObject { get; set; }
        #endregion

        #region Public Methods
        public bool CreateReport()
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.HALFLETTER.Rotate());
            try
            {
                //set up RunReport event overrides & create doc
                _pageCount = 1;
                _pawnInfo = ReportObject.InquiryItemDetails_theData;
                _pawnMdse = ReportObject.InquiryItemDetails_thisData;
                _pawnRowNum = ReportObject.InquiryItemDetails_RowNumber;
                ItemDetailReport events = this;
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ReportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                MultiColumnText columns = new MultiColumnText(document.PageSize.Top - 100, document.PageSize.Height - (50));
                float pageLeft = document.PageSize.Left;
                float pageright = document.PageSize.Right;
                columns.AddSimpleColumn(-38, document.PageSize.Width + 50);

                //set up tables, etc...
                int detailTablecolspan = 8;
                PdfPTable table = new PdfPTable(detailTablecolspan);
                table.WidthPercentage = 85;// document.PageSize.Width;
                PdfPCell cell = new PdfPCell();
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(25);
                runReport = new RunReport();
                document.Open();
                document.SetPageSize(PageSize.HALFLETTER.Rotate());
                document.SetMargins(-100, -100, 10, 45);
                document.AddTitle(ReportObject.ReportTitle + ": " + DateTime.Now.ToString("MM/dd/yyyy"));

                //here add detail

                WriteDetail(table, detailTablecolspan);
                columns.AddElement(table);
                document.Add(columns);


                document.Close();
                //OpenFile(ReportObject.ReportTempFileFullName);
                //CreateReport();
                isSuccessful = true;
            }
            catch (DocumentException de)
            {
                ReportObject.ReportError = de.Message;
                ReportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            catch (IOException ioe)
            {
                ReportObject.ReportError = ioe.Message;
                ReportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            catch (Exception e)
            {
                ReportObject.ReportError = e.Message;
                ReportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            return isSuccessful;
        }

        #endregion

        #region Public Overrides
        public override void OnOpenDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            try
            {
                footerBaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                contentByte = writer.DirectContent;
                template = contentByte.CreateTemplate(50, 50);
            }
            catch (DocumentException)
            {
            }
            catch (IOException)
            {
            }
        }

        public override void OnCloseDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            template.BeginText();
            template.SetFontAndSize(footerBaseFont, 8);
            template.ShowText((writer.PageNumber - 1).ToString());
            template.EndText();
        }

        public override void OnStartPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnStartPage(writer, document);
            try
            {
                int headerTblColspan = 4;
                PdfPTable headerTbl = new PdfPTable(headerTblColspan);
                headerTbl.TotalWidth = document.PageSize.Width - 2;
                Image logo = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                logo.ScalePercent(25);
                ReportHeader(headerTbl, logo, (ItemDetailReport)writer.PageEvent, headerTblColspan);
                headerTbl.WriteSelectedRows(0, -1, 22, (document.PageSize.Height - 8), writer.DirectContent);

            }
            catch (Exception)
            {
                return;
            }
        }

        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            int pageN = writer.PageNumber;
            string text = string.Empty;
            string[] reportName = writer.Info.Get(PdfName.TITLE).ToString().Split(':');
            text = string.Format("{0} Page {1} of ", StringUtilities.fillString(" ", 80), pageN);
            float len = footerBaseFont.GetWidthPoint(text, 8);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            //Madhu fix for defexct PWNU00001411
            contentByte.SetTextMatrix(len / 2, 30);
            contentByte.ShowText(text);
            contentByte.EndText();
            //Madhu fix for defexct PWNU00001411
            contentByte.AddTemplate(template, len + len / 2, 30);

        }
        #endregion
    }
}
