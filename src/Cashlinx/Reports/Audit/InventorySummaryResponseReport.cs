using System;
using System.IO;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;
using Common.Libraries.Utility.String;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.Audit
{
    public class InventorySummaryResponseReport : ReportBase
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

        #endregion

        #region Properties
        public AuditReportsObject ReportObject { get; set; }
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
        private void WriteDetail(PdfPTable detailsTable)
        {
           
        }

        private void ReportHeader(PdfPTable headingtable, Image gif, InventorySummaryResponseReport pageEvent)
        {
            //  row 1
            var cell = new PdfPCell(gif)
                       {
                           Border = Rectangle.NO_BORDER,
                           HorizontalAlignment = Element.ALIGN_LEFT,
                           VerticalAlignment = Element.ALIGN_TOP,
                           Colspan = 1
                       };
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, ReportFont))
                   {
                       Border = Rectangle.NO_BORDER,
                       HorizontalAlignment = Element.ALIGN_CENTER,
                       VerticalAlignment = Element.ALIGN_TOP,
                       Colspan = 6
                   };
            headingtable.AddCell(cell);

            WriteCell(headingtable, string.Empty, ReportFontMedium, 5, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Cash America Pawn of Nashville", ReportFontRegular, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(headingtable, string.Empty, ReportFont, 5, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Shop Number: #00003", ReportFontRegular, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(headingtable, string.Empty, ReportFont, 5, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Inventory Audit Date: 6/27/2011", ReportFontRegular, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(headingtable, string.Empty, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Inventory Summary Responses", ReportFontHeading, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Report #: xxx", ReportFontRegular, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(headingtable, string.Empty, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Division: FLD-C.O.O", ReportFontRegular, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);


            WriteCell(headingtable, string.Empty, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Region: FLD-SE", ReportFontRegular, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(headingtable, string.Empty, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Reg Manager: M.Hachtel", ReportFontRegular, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(headingtable, string.Empty, ReportFont, 7, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 7, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
        }

        private void WriteAdditionalCommentsSection(PdfPTable additionalCommentsSectionTable)
        {
            WriteCell(additionalCommentsSectionTable, string.Empty, ReportFontBold, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(additionalCommentsSectionTable, string.Empty, ReportFontBold, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(additionalCommentsSectionTable, string.Empty, ReportFontBold, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            //WriteCell(employeeSectionTable, string.Empty, ReportFontBold, 12, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            string comments ="First exception list - 153 at $165. A ring at $150 was stolen by employee Deneen Rogeua. She was fired today ";
            comments += " Sam Sandoval is handling tje investigation. A misc drum at $114 was missing from an M&m transfer. CACC was short in ";
            comments += " DVD's. CACC was unorganized and hard to count. All mixed together. Several Items were found in the back";
            AuditReportSectionDivider(additionalCommentsSectionTable, 1, "Additional Comments", Element.ALIGN_CENTER, ReportFontBold);

            WriteCell(additionalCommentsSectionTable, comments, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
        }

        private void  WriteDeficiencesSection(PdfPTable deficiencesSectionTable)
        {
            WriteCell(deficiencesSectionTable, string.Empty, ReportFontBold, 4, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(deficiencesSectionTable, string.Empty, ReportFontBold, 4, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(deficiencesSectionTable, string.Empty, ReportFontBold, 4, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            AuditReportSectionDivider(deficiencesSectionTable, 4, "Deficiences", Element.ALIGN_CENTER, ReportFontBold);

            WriteCell(deficiencesSectionTable, "1.", ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(deficiencesSectionTable, "Excess Jewelry Count:", ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(deficiencesSectionTable, "133", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(deficiencesSectionTable, "15.", ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(deficiencesSectionTable, "Master Cash Locked/Secure Location:", ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(deficiencesSectionTable, "Depository safe is being used correctly", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
        }

        private void WriteInventoryHistorySection(PdfPTable inventoryHistorySectionTable)
        {
            WriteCell(inventoryHistorySectionTable, string.Empty, ReportFontBold, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, string.Empty, ReportFontBold, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, string.Empty, ReportFontBold, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            AuditReportSectionDivider(inventoryHistorySectionTable, 9, "Inventory History", Element.ALIGN_CENTER, ReportFontBold);

            WriteCell(inventoryHistorySectionTable, "Date", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, "Manager", ReportFontBold, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, "Inv Type", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, "Inv Sub Type", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, "Over/(Short)", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, "Adjustment", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, "Net Over/(Short)", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, "PA Score", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

            WriteCell(inventoryHistorySectionTable, "04/14/2011", ReportFont, 1, Element.ALIGN_CENTER, Rectangle.BOX, false);
            WriteCell(inventoryHistorySectionTable, "Orin Jackson", ReportFont, 2, Element.ALIGN_CENTER, Rectangle.BOX, false);
            WriteCell(inventoryHistorySectionTable, "Full", ReportFont, 1, Element.ALIGN_CENTER, Rectangle.BOX, false);
            WriteCell(inventoryHistorySectionTable, "Routine", ReportFont, 1, Element.ALIGN_CENTER, Rectangle.BOX, false);
            WriteCell(inventoryHistorySectionTable, "($75016)", ReportFont, 1, Element.ALIGN_CENTER, Rectangle.BOX, false);
            WriteCell(inventoryHistorySectionTable, "$0.00", ReportFont, 1, Element.ALIGN_CENTER, Rectangle.BOX, false);
            WriteCell(inventoryHistorySectionTable, "($750.16)", ReportFont, 1, Element.ALIGN_CENTER, Rectangle.BOX, false);
            WriteCell(inventoryHistorySectionTable, "59", ReportFont, 1, Element.ALIGN_CENTER, Rectangle.BOX, false);
        }     
        #endregion

        #region Constructors
        public InventorySummaryResponseReport(IPdfLauncher pdfLauncher) : base(pdfLauncher)
        {
             //_ReportObject = new AuditReportsObject();
             //ReportObject = _ReportObject;
        }
        #endregion

        #region Public Methods
        public bool CreateReport()
        {
            
            bool isSuccessful = false;
            var document = new iTextSharp.text.Document(PageSize.LETTER);
            try
            {
                //set up RunReport event overrides & create doc
                //ReportObject.BuildChargeOffsList();
                ReportObject.ReportTempFile = "c:\\Program Files\\Phase2App\\logs\\AuditReportsTemp\\";
                ReportObject.CreateTemporaryFullName("InventorySummaryResponseReport");
                _pageCount = 1;
                var events = this;
                var writer = PdfWriter.GetInstance(document, new FileStream(ReportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                MultiColumnText columns = new MultiColumnText(document.PageSize.Top - 120, document.PageSize.Height);
                float pageLeft = document.PageSize.Left;
                float pageright = document.PageSize.Right;
                columns.AddSimpleColumn(20, document.PageSize.Width - 20);

                //set up tables, etc...
              
                var cell = new PdfPCell();
                var gif = Image.GetInstance(Common.Properties.Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(25);
                runReport = new RunReport();
                document.Open();
                document.SetPageSize(PageSize.LETTER);
                document.SetMargins(-100, -100, 10, 45);
                document.AddTitle(ReportObject.ReportTitle + ": " + DateTime.Now.ToString("MM/dd/yyyy"));


                var additionalCommentsSectionTable = new PdfPTable(1);
                additionalCommentsSectionTable.WidthPercentage = 100;// document.PageSize.Width;
                WriteAdditionalCommentsSection(additionalCommentsSectionTable);
                columns.AddElement(additionalCommentsSectionTable);

                var deficiencesSectionTable = new PdfPTable(4);
                deficiencesSectionTable.WidthPercentage = 100;// document.PageSize.Width;
                WriteDeficiencesSection(deficiencesSectionTable);
                columns.AddElement(deficiencesSectionTable);

                var inventoryHistorySectionTable = new PdfPTable(9);
                inventoryHistorySectionTable.WidthPercentage = 100;// document.PageSize.Width;
                WriteInventoryHistorySection(inventoryHistorySectionTable);
                columns.AddElement(inventoryHistorySectionTable);

                document.Add(columns);
                document.Close();
                OpenFile(ReportObject.ReportTempFileFullName);
                //CreateReport();
                isSuccessful = true;
            }
            catch (DocumentException de)
            {
                ReportObject.ReportError = de.Message;
                //ReportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            catch (IOException ioe)
            {
                ReportObject.ReportError = ioe.Message;
                //ReportObject.ReportErrorLevel = (int)LogLevel.ERROR;
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
                var headerTbl = new PdfPTable(7);
                headerTbl.TotalWidth = document.PageSize.Width - 10;
                var logo = Image.GetInstance(Common.Properties.Resources.logo, BaseColor.WHITE);
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                logo.ScalePercent(25);
                ReportHeader(headerTbl, logo, (InventorySummaryResponseReport)writer.PageEvent);
                //write the rows out to the PDF output stream. I use the height of the document to position the table. Positioning seems quite strange in iTextSharp and caused me the biggest headache.. It almost seems like it starts from the bottom of the page and works up to the top, so you may ned to play around with this.
                if (PageCount == 1)
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
            var text = string.Empty;
            var reportName = writer.Info.Get(PdfName.TITLE).ToString().Split(':');
            text = StringUtilities.fillString(" ", 80) + " Page " + pageN + "  of ";
            var pageSize = document.PageSize;

            //add GunTotals
            const string gunText = "Inventory Report";
            var len2 = footerBaseFontGunTotals.GetWidthPoint(gunText, 9);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFontGunTotals, 9);
            //Madhu fix for defexct PWNU00001411
            contentByte.SetTextMatrix(pageSize.GetLeft(50), pageSize.GetBottom(15));
            contentByte.ShowText(gunText);
            contentByte.EndText();
            //Madhu fix for defexct PWNU00001411
            contentByte.AddTemplate(templateGunTotals, pageSize.GetLeft(50) + len2, pageSize.GetBottom(15));

            //add pageNumbers
            var len = footerBaseFont.GetWidthPoint(text, 8);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            //Madhu fix for defexct PWNU00001411
            contentByte.SetTextMatrix(pageSize.GetLeft(330), pageSize.GetBottom(15));
            contentByte.ShowText(text);
            contentByte.EndText();
            //Madhu fix for defexct PWNU00001411
            contentByte.AddTemplate(template, pageSize.GetLeft(330) + len, pageSize.GetBottom(15));


        }
        #endregion
    }
}
