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
    public class RetailPriceChangeReport : ReportBase
    {
        #region Fields
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        private BaseFont footerBaseFontGunTotals = null;
        private PdfTemplate templateGunTotals;
        public RunReport runReport;
        private int _pageCount = 1;
        private int _totalNumGuns = 0;
        private String _icn = String.Empty;
        private String _description = String.Empty;
        #endregion

        public RetailPriceChangeReport(IPdfLauncher pdfLauncher)
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

        private void WriteDetail(PdfPTable detailsTable, Int32 columnCount, DataSet retailPriceChangeDataSet)
        {
            DataTable retailPriceChangeTable = retailPriceChangeDataSet.Tables[0];

            for (int i = 0; i < retailPriceChangeTable.Rows.Count; i++)
            {
                System.Data.DataRow retailPriceChange = retailPriceChangeTable.Rows[i];

                WriteCell(detailsTable, (i + 1).ToString() + @")", ReportFontMedium, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, Convert.ToDateTime(retailPriceChange["transaction_date"]).ToShortDateString() + " " + Convert.ToDateTime(retailPriceChange["transaction_date"]).ToShortTimeString(), ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailPriceChange["percent_change"].ToString(), ReportFontMedium, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailPriceChange["previous_price"].ToString(), ReportFontMedium, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailPriceChange["current_price"].ToString(), ReportFontMedium, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailPriceChange["created_by"].ToString(), ReportFontMedium, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);

            }
        }

        private void ReportColumns(PdfPTable columnsTable)
        {
            WriteCell(columnsTable, "", ReportFontBold, 6, Element.ALIGN_RIGHT, Rectangle.TOP_BORDER);
           
            WriteCell(columnsTable, "", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Transaction Date", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Pct Change", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Retail Before", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Retail After", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "User ID", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);

            WriteCell(columnsTable, "", ReportFontBold, 6, Element.ALIGN_RIGHT, Rectangle.BOTTOM_BORDER);
        }

        private void ReportHeader(PdfPTable headingtable, Image gif, RetailPriceChangeReport pageEvent, int colspan)
        {
            PdfPCell cell = new PdfPCell();

            //row 1
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 6;
            headingtable.AddCell(cell);

            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            //row 2
            WriteCell(headingtable, DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString(), ReportFont, 6, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(headingtable, "Retail Price Change", ReportFontHeading, 6, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            
            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
           
            //row 5
            WriteCell(headingtable, "ICN:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, _icn, ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, "Description:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, _description, ReportFontBold, 2, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
           
            //Write Columns
            //ReportColumns(headingtable);
            
        }

    
        #endregion

        #region Public Properties
        public ReportObject ReportObject { get; set; }

        #endregion

        #region Public Methods
        public bool CreateReport(String icn, String description, System.Data.DataSet theData)
        {
            _icn = icn;
            _description = description;

            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.LETTER.Rotate());
            try
            {
                //set up RunReport event overrides & create doc
                _pageCount = 1;
                RetailPriceChangeReport events = this;
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ReportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                MultiColumnText columns = new MultiColumnText(document.PageSize.Top - 100, document.PageSize.Height - (50));
                float pageLeft = document.PageSize.Left;
                float pageright = document.PageSize.Right;
                columns.AddSimpleColumn(-150, document.PageSize.Width + 76);

                //set up tables, etc...
                PdfPTable table = new PdfPTable(6);
                table.WidthPercentage = 85;// document.PageSize.Width;
                PdfPCell cell = new PdfPCell();
                Image gif = Image.GetInstance(Common.Properties.Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(25);
                runReport = new RunReport();
                document.Open();
                document.SetPageSize(PageSize.LETTER.Rotate());
                document.SetMargins(-100, -100, 10, 45);
                document.AddTitle(ReportObject.ReportTitle + ": " + DateTime.Now.ToString("MM/dd/yyyy"));

                ReportColumns(table);

                //here add detail
                WriteDetail(table, 6, theData);
                columns.AddElement(table);
                document.Add(columns);
                document.Close();
                OpenFile(ReportObject.ReportTempFileFullName);
                //CreateReport(_icn, _description, theData);
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
                footerBaseFontGunTotals = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                templateGunTotals = contentByte.CreateTemplate(50, 50);
                //PdfPTable headerTbl = new PdfPTable(21);
                //set the width of the table to be the same as the document
                //headerTbl.TotalWidth = document.PageSize.Width - 10;
                //headerTbl.WidthPercentage = 80;
                //I use an image logo in the header so I need to get an instance of the image to be able to insert it. I believe this is something you couldn't do with older versions of iTextSharp
                //Image logo = Image.GetInstance(HttpContext.Current.Server.MapPath("/images/logo.jpg"));
                //Image logo = Image.GetInstance(PawnReportResources.logo, BaseColor.WHITE);
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                //logo.ScalePercent(25);
                //ReportHeader(headerTbl, logo, (RetailPriceChangeReport)writer.PageEvent);
                //write the rows out to the PDF output stream. I use the height of the document to position the table. Positioning seems quite strange in iTextSharp and caused me the biggest headache.. It almost seems like it starts from the bottom of the page and works up to the top, so you may ned to play around with this.
               // headerTbl.WriteSelectedRows(0, -1, 7, (document.PageSize.Height - 10), writer.DirectContent);
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
            template.SetTextMatrix(0, 0);
            template.ShowText("" + (writer.PageNumber - 1));
            template.EndText();
        }

        public override void OnStartPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnStartPage(writer, document);
            try
            {
                /*PdfPTable headerTbl = new PdfPTable(21);
                //set the width of the table to be the same as the document
                headerTbl.TotalWidth = document.PageSize.Width - 10;
                //headerTbl.WidthPercentage = 80;
                //I use an image logo in the header so I need to get an instance of the image to be able to insert it. I believe this is something you couldn't do with older versions of iTextSharp
                //Image logo = Image.GetInstance(HttpContext.Current.Server.MapPath("/images/logo.jpg"));
                Image logo = Image.GetInstance(PawnReportResources.logo, BaseColor.WHITE);
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                logo.ScalePercent(25);*/

                PdfPTable headerTbl = new PdfPTable(6);
                headerTbl.TotalWidth = document.PageSize.Width - 10;
                Image logo = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                logo.ScalePercent(25);
                ReportHeader(headerTbl, logo, (RetailPriceChangeReport)writer.PageEvent, 6);

                //ReportColumns(headerTbl);
               
                //write the rows out to the PDF output stream. I use the height of the document to position the table. Positioning seems quite strange in iTextSharp and caused me the biggest headache.. It almost seems like it starts from the bottom of the page and works up to the top, so you may ned to play around with this.
                if(PageCount == 1)
                    headerTbl.WriteSelectedRows(0, -1, 16, (document.PageSize.Height - 10), writer.DirectContent);
                else
                    headerTbl.WriteSelectedRows(0, -1, 16, (document.PageSize.Height - 8), writer.DirectContent);
                PageCount++;
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
            text = string.Format("{0} Page {1}  of ", StringUtilities.fillString(" ", 80), pageN);
            var pageSize = document.PageSize;

            //add GunTotals
            string gunText = "Total Number of Guns: " + TotalNumberOfGuns;
            float len2 = footerBaseFontGunTotals.GetWidthPoint(gunText, 9);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFontGunTotals, 9);
            //Madhu fix for defexct PWNU00001411
            contentByte.SetTextMatrix(pageSize.GetLeft(50), pageSize.GetBottom(15));
            contentByte.ShowText(gunText);
            contentByte.EndText();
            //Madhu fix for defexct PWNU00001411
            contentByte.AddTemplate(templateGunTotals, pageSize.GetLeft(50) + len2, pageSize.GetBottom(15));

            //add pageNumbers
            float len = footerBaseFont.GetWidthPoint(text, 8);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            //Madhu fix for defexct PWNU00001411
            contentByte.SetTextMatrix(pageSize.GetLeft(205), pageSize.GetBottom(15));
            contentByte.ShowText(text);
            contentByte.EndText();
            //Madhu fix for defexct PWNU00001411
            contentByte.AddTemplate(template, pageSize.GetLeft(205) + len, pageSize.GetBottom(15));
        }
        #endregion
    }
}
