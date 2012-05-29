using System;
using System.Collections.Generic;
using System.Linq;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ReportObject = Common.Controllers.Application.ReportObject;
using Common.Libraries.Utility.Logger;

namespace Reports
{
    public class RefurbList : ReportBase
    {
        #region Fields
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        private BaseFont footerBaseFontGunTotals = null;
        private PdfTemplate templateGunTotals;
        public RunReport runReport;
        private int _pageCount = 0;
        private int _totalNumGuns = 0;
        #endregion

        #region Constructors
        public RefurbList(IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {
            
        }
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
        private void WriteDetail(PdfPTable detailsTable, int tableColumnsSpan, List<ReportObject.RefurbItem> listRefurbItems)
        {
            PdfPCell cell; 
            foreach (ReportObject.RefurbItem item in listRefurbItems)
            {
                WriteCell(detailsTable, string.Empty, ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(detailsTable, item.TransferDate.ToShortDateString(), ReportFont, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(detailsTable, item.TransferNumber, ReportFont, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(detailsTable, item.RefurbNumber, ReportFont, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                Phrase icnPhrase = new Phrase(GetFormattedICNPhrase(item.ICN));
                cell = new PdfPCell(icnPhrase);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                cell.Border = Rectangle.NO_BORDER;
                detailsTable.AddCell(cell);

                //WriteCell(detailsTable, GetFormattedICNPhrase(item.ICN), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(detailsTable, item.Description, ReportFont, 4, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(detailsTable, item.Cost.ToString("C"), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(detailsTable, string.Empty, ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            }
        }

        private void ReportColumns(PdfPTable columnsTable, int colspan, string section)
        {
            WriteCell(columnsTable, section, ReportFontBold, colspan, Element.ALIGN_CENTER, Rectangle.NO_BORDER);


            WriteCell(columnsTable, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

            WriteCell(columnsTable, string.Empty, ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, string.Empty, ReportFontBold, 10, Element.ALIGN_CENTER, Rectangle.BOTTOM_BORDER);
            WriteCell(columnsTable, string.Empty, ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);

            WriteCell(columnsTable, string.Empty, ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Date", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.LEFT_BORDER, System.Drawing.Color.LightGray);
            WriteCell(columnsTable, "Transfer#", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, System.Drawing.Color.LightGray);
            WriteCell(columnsTable, "Refurb#", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, System.Drawing.Color.LightGray);
            WriteCell(columnsTable, "ICN", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, System.Drawing.Color.LightGray);
            WriteCell(columnsTable, "Merchandise Description", ReportFontBold, 4, Element.ALIGN_LEFT, Rectangle.NO_BORDER, System.Drawing.Color.LightGray);
            WriteCell(columnsTable, "Cost", ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.RIGHT_BORDER, System.Drawing.Color.LightGray);
            WriteCell(columnsTable, string.Empty, ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);

            WriteCell(columnsTable, string.Empty, ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, string.Empty, ReportFontBold, 10, Element.ALIGN_CENTER, Rectangle.TOP_BORDER);
            WriteCell(columnsTable, string.Empty, ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
        }

        private void WriteTotals(PdfPTable totalsTable, int colspan, List<ReportObject.RefurbItem> listRefurbItems)
        {
            WriteCell(totalsTable, string.Empty, ReportFontBold, colspan, Element.ALIGN_CENTER, Rectangle.TOP_BORDER);
            WriteCell(totalsTable, string.Empty, ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(totalsTable, "Total Items: " + listRefurbItems.Count.ToString(), ReportFontBold, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
            WriteCell(totalsTable, GetTotalCost(listRefurbItems).ToString("C"), ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
        }

        private decimal GetTotalCost(List<ReportObject.RefurbItem> listRefurbItems)
        {
            decimal totalCost = 0.0m;
            totalCost = listRefurbItems.Sum(a => a.Cost);
            return totalCost;
        }

        protected override void DrawLine(PdfPTable linestable)
        {
            WriteCell(linestable, StringUtilities.fillString("_", 195), ReportFont, 21, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
        }

        private void ReportHeader(PdfPTable headingtable, Image gif, RefurbList pageEvent, int colspan)
        {
            //  row 1
            var cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 6;
            headingtable.AddCell(cell);

            WriteCell(headingtable, ReportObject.ReportStoreDesc, ReportFont, 2, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            //row 2

            WriteCell(headingtable, ReportHeaders.OPERATIONAL, ReportFont, 6, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, "Shop #: " + ReportObject.ReportStore, ReportFont, 2, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(headingtable, "Report Date: " + ReportObject.RunDate, ReportFont, 6, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, "Run Date: " + ReportObject.RunDate, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

            //row 3
            WriteCell(headingtable, string.Empty, ReportFont, 6, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, "Report #: " + ReportObject.ReportNumber.ToString(), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);


            //row 7
            WriteCell(headingtable, string.Empty, ReportFontBold, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
            WriteCell(headingtable, ReportObject.ReportTitle, ReportFontBold, 5, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

            //row 8
            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            //Write Columns
            //ReportColumns(headingtable);

            //DrawLine(headingtable);
            
        }

        
        #endregion

        #region Public Properties
        public ReportObject ReportObject { get; set; }

        #endregion

        #region Public Methods
        public bool CreateReport()
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.LETTER);
            try
            {
                //set up RunReport event overrides & create doc
                //_pageCount = 1;
                RefurbList events = this;
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ReportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                MultiColumnText columns = new MultiColumnText(document.PageSize.Top - 90, document.PageSize.Height - (100));
                columns.AddSimpleColumn(-5, document.PageSize.Width - 15);

                //set up tables, etc...
                int colspan = 12;
                var table = new PdfPTable(colspan);
                table.WidthPercentage = 95;// document.PageSize.Width;
                //table.TotalHeight = 95;

                var cell = new PdfPCell();
                var gif = Image.GetInstance(Common.Properties.Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(25);
                runReport = new RunReport();
                document.Open();
                document.SetPageSize(PageSize.LETTER);
                document.SetMargins(-100, -20, 10, 35);
                document.AddTitle(string.Format("{0}: {1}", ReportObject.ReportTitle, DateTime.Now.ToString("MM/dd/yyyy")));
                

                ReportColumns(table, colspan, "Merchandise Expected to be Received from Refurb");
                WriteDetail(table, colspan, ReportObject.ListRefurbItemsExpected);
                WriteTotals(table, colspan, ReportObject.ListRefurbItemsExpected);

                WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                ReportColumns(table, colspan, "Merchandise Not Expected to be Received from Refurb");
                WriteDetail(table, colspan, ReportObject.ListRefurbItemsNotExpected);
                WriteTotals(table, colspan, ReportObject.ListRefurbItemsNotExpected);

                WriteCell(table, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

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

        public override void OnStartPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnStartPage(writer, document);
            PageCount++;
            try
            {
                int colspan = 8;
                var headerTbl = new PdfPTable(colspan);
                Image logo = Image.GetInstance(global::Common.Properties.Resources.logoCasham, BaseColor.WHITE);
                headerTbl = new PdfPTable(colspan)
                                {
                                    TotalWidth = document.PageSize.Width - 10
                                };
                //headerTbl.TotalWidth = document.PageSize.Width - 55;
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                logo.ScalePercent(25);
                ReportHeader(headerTbl, logo, (RefurbList)writer.PageEvent, colspan);

                
               
                //write the rows out to the PDF output stream. I use the height of the document to position the table. Positioning seems quite strange in iTextSharp and caused me the biggest headache.. It almost seems like it starts from the bottom of the page and works up to the top, so you may ned to play around with this.
                headerTbl.WriteSelectedRows(0, -10, 25, (document.PageSize.Height - 20), writer.DirectContent);
                //PageCount++;
            }
            catch (Exception eX)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Exception thrown: {0}", eX);
                }
                BasicExceptionHandler.Instance.AddException("RefurbListReport", eX);
            }
        }

        public override void OnCloseDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            template.BeginText();
            template.SetFontAndSize(footerBaseFont, 8);
            template.ShowText((writer.PageNumber - 1).ToString());
            template.EndText();
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
