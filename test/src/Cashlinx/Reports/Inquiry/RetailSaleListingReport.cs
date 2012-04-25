using System;
using System.Collections.Generic;
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
    public class RetailSaleListingReport : ReportBase
    {
        #region Fields
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        private BaseFont footerBaseFontGunTotals = null;
        private PdfTemplate templateGunTotals;
        public RunReport runReport;
        private int _pageCount = 1;
        //private int _totalNumGuns = 0;
        private String _sortBy = String.Empty;
        #endregion

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
        #endregion

        #region Private Methods

        private void WriteDetail(PdfPTable detailsTable, Int32 columnCount, List<ReportObject.RetailSaleListing> retailSaleListingDataSet)
        {
            Int32 i = 0;
            foreach (ReportObject.RetailSaleListing retailListing in retailSaleListingDataSet)
            {
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailListing.shopNumber.ToString(), ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailListing.date.ToShortDateString(), ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "$" + retailListing.saleAmount.ToString("0.00"), ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "$" + retailListing.cost.ToString("0.00"), ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailListing.status.ToString(), ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailListing.userId.ToString(), ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                i++;
            }
        }

        private void ReportColumns(PdfPTable columnsTable)
        {
            WriteCell(columnsTable, String.Empty, ReportFontBold, 7, Element.ALIGN_CENTER, Rectangle.TOP_BORDER);

            WriteCell(columnsTable, String.Empty, ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Shop", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Made", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Sale Amount", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Cost", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Status", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "User ID", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);

            WriteCell(columnsTable, String.Empty, ReportFontBold, 7, Element.ALIGN_CENTER, Rectangle.BOTTOM_BORDER);
        }

        private void ReportHeader(PdfPTable headingtable, Image gif, RetailSaleListingReport pageEvent, int colspan)
        {
            PdfPCell cell = new PdfPCell();

            //row 1
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 7;
            headingtable.AddCell(cell);

            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            //row 2
            //WriteCell(headingtable, DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString(), ReportFont, 7, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(headingtable, "Retail Sale Listing By <" + _sortBy + ">", ReportFontHeading, colspan, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            
            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
           
            //Write Columns
            //ReportColumns(headingtable);
            
        }

        #endregion

        #region Public Properties
        public ReportObject ReportObject { get; set; }

        #endregion

        #region Public Methods
        public bool CreateReport(string sortBy, List<ReportObject.RetailSaleListing> theData)
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.LETTER.Rotate());

            _sortBy = sortBy;

            try
            {
                //set up RunReport event overrides & create doc
                _pageCount = 1;
                RetailSaleListingReport events = this;
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ReportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                MultiColumnText columns = new MultiColumnText(document.PageSize.Top - 100, document.PageSize.Height - (50));
                float pageLeft = document.PageSize.Left;
                float pageright = document.PageSize.Right;
                columns.AddSimpleColumn(-150, document.PageSize.Width + 76);

                //set up tables, etc...
                PdfPTable table = new PdfPTable(7);
                table.WidthPercentage = 85;// document.PageSize.Width;
                PdfPCell cell = new PdfPCell();
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(25);
                runReport = new RunReport();
                document.Open();
                document.SetPageSize(PageSize.LETTER.Rotate());
                document.SetMargins(-100, -100, 10, 45);
                document.AddTitle(ReportObject.ReportTitle + ": " + DateTime.Now.ToString("MM/dd/yyyy"));

                ReportColumns(table);

                //here add detail
                WriteDetail(table, 7, theData);
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
                //ReportHeader(headerTbl, logo, (RetailSaleListingReport)writer.PageEvent);
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
            template.SetFontAndSize(footerBaseFont, 7);
            template.SetTextMatrix(0, 0);
            template.ShowText(String.Empty + (writer.PageNumber - 1));
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

                PdfPTable headerTbl = new PdfPTable(7);
                headerTbl.TotalWidth = document.PageSize.Width - 10;
                Image logo = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                logo.ScalePercent(25);
                ReportHeader(headerTbl, logo, (RetailSaleListingReport)writer.PageEvent, 7);

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
            Rectangle pageSize = document.PageSize;

            //add pageNumbers
            float len = footerBaseFont.GetWidthPoint(text, 7);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 7);
            //Madhu fix for defexct PWNU00001411
            contentByte.SetTextMatrix(pageSize.GetLeft(205), pageSize.GetBottom(15));
            contentByte.ShowText(text);
            contentByte.EndText();
            //Madhu fix for defexct PWNU00001411
            contentByte.AddTemplate(template, pageSize.GetLeft(205) + len, pageSize.GetBottom(15));
        }
        #endregion

        public RetailSaleListingReport(IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {
            
        }
    }
}
