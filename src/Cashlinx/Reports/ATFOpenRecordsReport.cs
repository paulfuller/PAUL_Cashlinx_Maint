using System;
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
    public class ATFOpenRecordsReport : ReportBase
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
        public ATFOpenRecordsReport(IPdfLauncher pdfLauncher)
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
        private void WriteDetail(PdfPTable detailsTable, int tableColumnsSpan)
        {
            int rowCount = 1;

            foreach (ReportObject.GunAuditATFOpenRecords GunAuditATFOpenRecord in ReportObject.GunAuditDataATFOpenRecordsData)
            {
                //if (rowCount <= 20)
                //{
                    Icn icn = new Icn(GunAuditATFOpenRecord.ICN);
                    string icnShortCode = icn.GetShortCode();
                    if (rowCount % 2 == 0)
                    {
                        WriteCell(detailsTable, GunAuditATFOpenRecord.GunNumber.ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, true);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.Manufacturer, ReportFontMedium, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, true);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.Importer, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, true);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.Model, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, true);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.SerialNumber, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, true);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.GaugeOrCaliber, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, true);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.GunType, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, true);
                        WriteCell(detailsTable, icnShortCode, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, true);
                        /*PdfPCell cell;
                        cell = GetFormattedICNCellSmallFont(GunAuditATFOpenRecord.ICN);
                        cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                        cell.Border = Rectangle.NO_BORDER;
                        cell.BackgroundColor = new BaseColor(System.Drawing.Color.LightGray);
                        cell.Colspan = 2;
                        cell.VerticalAlignment = Element.ALIGN_TOP;
                        detailsTable.AddCell(cell);*/
                        //WriteCell(detailsTable, GunAuditATFOpenRecord.ICN, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, true);
                        if(GunAuditATFOpenRecord.HoldType == "POLICEHOLD")
                            WriteCell(detailsTable, GunAuditATFOpenRecord.TransactionNumber + " - " + GunAuditATFOpenRecord.Status + " - " + GunAuditATFOpenRecord.HoldDesc, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, true);
                        else    
                            WriteCell(detailsTable, GunAuditATFOpenRecord.TransactionNumber + " - " + GunAuditATFOpenRecord.Status, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, true);
                        
                        WriteCell(detailsTable, GunAuditATFOpenRecord.LocationAisle, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, true);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.Shelf, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, true);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.Location, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, true);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.GunDate.ToShortDateString(), ReportFontMedium, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, true);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.CustomerName, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, true);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.CustomerAddress, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, true);

                        //write id and address
                        WriteCell(detailsTable, string.Empty, ReportFontMedium, 17, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, true);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.CustomerID, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, true);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.CustomerAddress1, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, true);
                        WriteCell(detailsTable, string.Empty, ReportFontMedium, tableColumnsSpan, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, true);
                    }
                    else
                    {
                        WriteCell(detailsTable, GunAuditATFOpenRecord.GunNumber.ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.Manufacturer, ReportFontMedium, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.Importer, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.Model, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.SerialNumber, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.GaugeOrCaliber, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.GunType, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(detailsTable, icnShortCode, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                        /*PdfPCell cell;
                        cell = GetFormattedICNCellSmallFont(GunAuditATFOpenRecord.ICN);
                        cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                        cell.Border = Rectangle.NO_BORDER;

                        cell.Colspan = 2;
                        cell.VerticalAlignment = Element.ALIGN_TOP;
                        detailsTable.AddCell(cell);*/
                        // WriteCell(detailsTable, GunAuditATFOpenRecord.ICN, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                        
                        if (GunAuditATFOpenRecord.HoldType == "POLICEHOLD")
                            WriteCell(detailsTable, GunAuditATFOpenRecord.TransactionNumber + " - " + GunAuditATFOpenRecord.Status + " - " + GunAuditATFOpenRecord.HoldDesc, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                        else
                            WriteCell(detailsTable, GunAuditATFOpenRecord.TransactionNumber + " - " + GunAuditATFOpenRecord.Status, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.LocationAisle, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.Shelf, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.Location, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.GunDate.ToShortDateString(), ReportFontMedium, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.CustomerName, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.CustomerAddress, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);

                        //write id and address
                        WriteCell(detailsTable, string.Empty, ReportFontMedium, 17, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.CustomerID, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(detailsTable, GunAuditATFOpenRecord.CustomerAddress1, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(detailsTable, string.Empty, ReportFontMedium, tableColumnsSpan, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);
                    }
                //}
                rowCount++;
                _totalNumGuns++;
            }
        }


        protected override void DrawLine(PdfPTable linestable)
        {
            WriteCell(linestable, StringUtilities.fillString("_", 195), ReportFont, 21, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
        }
   
        private void ReportHeader(PdfPTable headingtable, Image gif, ATFOpenRecordsReport pageEvent, int colspan)
        {
            //  row 1
            var cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = colspan;
            headingtable.AddCell(cell);

            //row 2
            WriteCell(headingtable, ReportHeaders.OPERATIONAL, ReportFont, 18, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, ReportObject.ReportStoreDesc, ReportFont, 3, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            //row 3
            WriteCell(headingtable, string.Empty, ReportFont, 18, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, "Run Date: " + ReportObject.RunDate, ReportFont, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

            //row 4
            WriteCell(headingtable, string.Empty, ReportFont, 18, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, "Report #: " + ReportObject.ReportNumber.ToString(), ReportFont, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

            //row 5 
            WriteCell(headingtable, ReportObject.ReportTitle, ReportFontLargeBold, colspan, Element.ALIGN_CENTER, Rectangle.NO_BORDER);

            //row 6
            WriteCell(headingtable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, "Do Not File In Gun Book", ReportFontLargeBold, 20, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

            //row 7
            WriteCell(headingtable, ReportObject.ReportTitle, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
            WriteCell(headingtable, "Sort Type: " + ReportObject.ReportSort, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

            //row 8
            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            //Write Columns
            //ReportColumns(headingtable);

            //DrawLine(headingtable);
            
        }

        private void ReportColumns(PdfPTable columnsTable, int colspan)
        {
            WriteCell(columnsTable, string.Empty, ReportFontBold, 11, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Transaction", ReportFontBold, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "-Location-", ReportFontBold, 3, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, string.Empty, ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Name", ReportFontBold, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, string.Empty, ReportFontBold, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);

            WriteCell(columnsTable, "Gun#", ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Manufacturer", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Importer", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Model", ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Serial#", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Gauge or Caliber", ReportFontBold, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Type", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "ICN", ReportFontBold, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Number/Status", ReportFontBold, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);

            WriteCell(columnsTable, "Aisle", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Shelf", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Other", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);

            WriteCell(columnsTable, "Date", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "ID#", ReportFontBold, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(columnsTable, "Address", ReportFontBold, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            //DrawLine(columnsTable);
            WriteCell(columnsTable, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.TOP_BORDER);
        }
        #endregion

        #region Public Properties
        public ReportObject ReportObject { get; set; }

        #endregion

        #region Public Methods
        public bool CreateReport()
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.LEGAL.Rotate());
            try
            {
                //set up RunReport event overrides & create doc
                //_pageCount = 1;
                ATFOpenRecordsReport events = this;
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ReportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                MultiColumnText columns = new MultiColumnText(document.PageSize.Top - 148, document.PageSize.Height - (170));
                columns.AddSimpleColumn(-51, document.PageSize.Width + 60);

                //set up tables, etc...
                var table = new PdfPTable(21);
                table.WidthPercentage = 85;// document.PageSize.Width;
                var cell = new PdfPCell();
                var gif = Image.GetInstance(Common.Properties.Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(25);
                runReport = new RunReport();
                document.Open();
                document.SetPageSize(PageSize.LEGAL.Rotate());
                document.SetMargins(-100, -100, 10, 35);
                document.AddTitle(string.Format("{0}: {1}", ReportObject.ReportTitle, DateTime.Now.ToString("MM/dd/yyyy")));

                //here add detail
                WriteDetail(table, 21);
                columns.AddElement(table);
                document.Add(columns);

                string gunText = "Total Number of Guns: " + TotalNumberOfGuns;
                MultiColumnText columnBottomPage = new MultiColumnText(document.PageSize.Bottom, 25);
                columnBottomPage.AddSimpleColumn(-51, document.PageSize.Width + 60);
                PdfPTable tableBottom = new PdfPTable(1);
                tableBottom.WidthPercentage = 85;// document.PageSize.Width;
                WriteCell(tableBottom, gunText, ReportFontLargeBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                columnBottomPage.AddElement(tableBottom);
                document.Add(columnBottomPage);

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
                //ReportHeader(headerTbl, logo, (ATFOpenRecordsReport)writer.PageEvent);
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

        public override void OnStartPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnStartPage(writer, document);
            PageCount++;
            try
            {
                int colspan = 21;
                var headerTbl = new PdfPTable(colspan);
                Image logo = Image.GetInstance(global::Common.Properties.Resources.logo, BaseColor.WHITE);
                headerTbl = new PdfPTable(21)
                                {
                                    TotalWidth = document.PageSize.Width - 10
                                };
                //headerTbl.TotalWidth = document.PageSize.Width - 55;
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                logo.ScalePercent(25);
                ReportHeader(headerTbl, logo, (ATFOpenRecordsReport)writer.PageEvent, colspan);

                ReportColumns(headerTbl, colspan);
               
                //write the rows out to the PDF output stream. I use the height of the document to position the table. Positioning seems quite strange in iTextSharp and caused me the biggest headache.. It almost seems like it starts from the bottom of the page and works up to the top, so you may ned to play around with this.
                if(PageCount == 1)
                    headerTbl.WriteSelectedRows(0, -10, 25, (document.PageSize.Height - 20), writer.DirectContent);
                else
                    headerTbl.WriteSelectedRows(0, -10, 25, (document.PageSize.Height - 18), writer.DirectContent);
                //PageCount++;
            }
            catch (Exception eX)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Exception thrown: {0}", eX);
                }
                BasicExceptionHandler.Instance.AddException("ATFOpenRecordsReport", eX);
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

        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            int pageN = writer.PageNumber;
            string text = string.Empty;
            string[] reportName = writer.Info.Get(PdfName.TITLE).ToString().Split(':');
            text = StringUtilities.fillString(" ", 80) + " Page " + pageN + "  of  ";
            Rectangle pageSize = document.PageSize;

            //add GunTotals
            var gunText = string.Format("Total Number of Guns: {0}", TotalNumberOfGuns);
            var len2 = footerBaseFontGunTotals.GetWidthPoint(gunText, 9);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFontGunTotals, 9);
            //Madhu fix for defexct PWNU00001411
            contentByte.SetTextMatrix(pageSize.GetLeft(50), pageSize.GetBottom(25));
            contentByte.ShowText(gunText);
            contentByte.EndText();
            //Madhu fix for defexct PWNU00001411
            contentByte.AddTemplate(templateGunTotals, pageSize.GetLeft(50) + len2, pageSize.GetBottom(25));

            //add pageNumbers
            var len = footerBaseFont.GetWidthPoint(text, 8);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            //Madhu fix for defexct PWNU00001411
            contentByte.SetTextMatrix(pageSize.GetLeft(305), pageSize.GetBottom(25));
            contentByte.ShowText(text);
            contentByte.EndText();
            //Madhu fix for defexct PWNU00001411
            contentByte.AddTemplate(template, pageSize.GetLeft(305) + len, pageSize.GetBottom(25));

           
        }
        #endregion
    }
}
