using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;
using Common.Libraries.Utility.String;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.Audit
{
    public class PostAuditReport : ReportBase
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

        private void ReportHeader(PdfPTable headingtable, Image gif, PostAuditReport pageEvent)
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
            WriteCell(headingtable, "Post Audit", ReportFontHeading, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Report #: xxx", ReportFontRegular, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

           
            WriteCell(headingtable, string.Empty, ReportFont, 7, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 7, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
        }

        private void SectionTempICnReconciliation(PdfPTable tableTempICnReconciliation)
        {
            WriteCell(tableTempICnReconciliation, "Category", ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
        }

        private void SectionChargeOn(PdfPTable tableChargeOn)
        {
            WriteCell(tableChargeOn, "Category", ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
        }

        private void SectionReactivation(PdfPTable tableReactivation)
        {
            WriteCell(tableReactivation, "Category", ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
        }

        private IEnumerable<AuditReportsObject.PostAuditField> ChargeOffFields()
        {
            var chargeOffFields = from chargeoffs in ReportObject.ListPostAuditField where chargeoffs.Category.Equals("Charge Off") select chargeoffs;
            return chargeOffFields.ToList();
        }

        private void SectionChargeOff(PdfPTable tableChargeOff)
        {
            //var chargeOffFields = from chargeoffs in ReportObject.ListPostAuditField where chargeoffs.Category.Equals("Charge Off") select chargeoffs;
            AuditReportSectionDivider(tableChargeOff, 10, "Charge Off", Element.ALIGN_CENTER, ReportFontBold);
            int totalQty = 0;
            decimal totalCost = 0.0m;
            decimal totalRetail = 0.0m;

            WriteCell(tableChargeOff, "ICN", ReportFontUnderlined, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(tableChargeOff, "Reason", ReportFontUnderlined, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(tableChargeOff, "Merchandise Description", ReportFontUnderlined, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(tableChargeOff, "Qty.", ReportFontUnderlined, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(tableChargeOff, "Cost", ReportFontUnderlined, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(tableChargeOff, "Retail", ReportFontUnderlined, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(tableChargeOff, String.Empty, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            var listFields = ChargeOffFields();
            foreach (var chargeOffField in listFields)
            {
                PdfPCell cell = new PdfPCell();
                cell = GetFormattedICNCell(chargeOffField.ICN);
                cell.Colspan = 2;
                tableChargeOff.AddCell(cell);

                //WriteCell(tableChargeOff, GetFormattedICNCellSmallFont(chargeOffField.ICN).ToString(), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(tableChargeOff, chargeOffField.Reason, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(tableChargeOff, chargeOffField.MerchandiseDescription, ReportFont, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(tableChargeOff, chargeOffField.Qty.ToString(), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(tableChargeOff, chargeOffField.Cost.ToString("C"), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(tableChargeOff, chargeOffField.Retail.ToString("C"), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(tableChargeOff, String.Empty, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

                totalQty += chargeOffField.Qty;
                totalCost += chargeOffField.Cost;
                totalRetail += chargeOffField.Retail;
            }

            WriteCell(tableChargeOff, StringUtilities.fillString("_", 144), ReportFont, 10, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

            WriteCell(tableChargeOff, "Total Merchandise", ReportFontBold, 6, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(tableChargeOff, totalQty.ToString(), ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(tableChargeOff, totalCost.ToString(), ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(tableChargeOff, totalRetail.ToString(), ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(tableChargeOff, String.Empty, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
        }


        private void SectionInventoryTotalsCountedByStatus(PdfPTable tableInventoryTotalsCountedByStatus)
        {
            int totalQty = 0;
            decimal totalCost = 0.0m;
            AuditReportSectionDivider(tableInventoryTotalsCountedByStatus, 6, "Inventory Totals Counted by Status", Element.ALIGN_CENTER, ReportFontBold);
            WriteCell(tableInventoryTotalsCountedByStatus, string.Empty, ReportFontBold, 6, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(tableInventoryTotalsCountedByStatus, string.Empty, ReportFontBold, 6, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(tableInventoryTotalsCountedByStatus, string.Empty, ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(tableInventoryTotalsCountedByStatus, string.Empty, ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(tableInventoryTotalsCountedByStatus, "Qty", ReportFontUnderlined, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(tableInventoryTotalsCountedByStatus, "Cost", ReportFontUnderlined, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(tableInventoryTotalsCountedByStatus, string.Empty, ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(tableInventoryTotalsCountedByStatus, string.Empty, ReportFontBold, 6, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            foreach (AuditReportsObject.PostAuditInventoryTotalsField invTotalsField in ReportObject.ListPostAuditInventoryTotalsField)
            {
                WriteCell(tableInventoryTotalsCountedByStatus, string.Empty, ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(tableInventoryTotalsCountedByStatus, invTotalsField.InventoryType, ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(tableInventoryTotalsCountedByStatus, invTotalsField.Qty.ToString(), ReportFont, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(tableInventoryTotalsCountedByStatus, invTotalsField.Cost.ToString("C"), ReportFont, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(tableInventoryTotalsCountedByStatus, string.Empty, ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                totalQty += invTotalsField.Qty;
                totalCost += invTotalsField.Cost;
            }

            WriteCell(tableInventoryTotalsCountedByStatus, StringUtilities.fillString("_", 144), ReportFont, 6, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

            WriteCell(tableInventoryTotalsCountedByStatus, "Total Inventory Counted by Status", ReportFontBold, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(tableInventoryTotalsCountedByStatus, totalQty.ToString(), ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(tableInventoryTotalsCountedByStatus, totalCost.ToString("C"), ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(tableInventoryTotalsCountedByStatus, string.Empty, ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
        }

        private new void AuditReportSectionDivider(PdfPTable sectionTable, int colspan, string sectionText, int horizontalAlignment, Font reportFont)
        {
            PdfPCell cell = new PdfPCell(new Phrase(sectionText, reportFont));
            cell.Colspan = colspan;
            cell.HorizontalAlignment = horizontalAlignment;
            cell.BackgroundColor = new BaseColor(System.Drawing.Color.LightGray);
            cell.Border = Rectangle.NO_BORDER;
            sectionTable.AddCell(cell);
        }
        #endregion

        #region Constructors
        public PostAuditReport(IPdfLauncher pdfLauncher) : base(pdfLauncher)
        {

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
                //ReportObject.BuildPostAuditFieldList();
                ReportObject.ReportTempFile = "c:\\Program Files\\Phase2App\\logs\\AuditReportsTemp\\";
                ReportObject.CreateTemporaryFullName("PostAuditReport");
                _pageCount = 1;
                var events = this;
                var writer = PdfWriter.GetInstance(document, new FileStream(ReportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                var columns = new MultiColumnText(document.PageSize.Top - 120, document.PageSize.Height);
                var pageLeft = document.PageSize.Left;
                var pageright = document.PageSize.Right;
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

                var tableInventoryTotalsCountedByStatus = new PdfPTable(6)
                                                          {
                                                              WidthPercentage = 100
                                                          };
                // document.PageSize.Width;
                SectionInventoryTotalsCountedByStatus(tableInventoryTotalsCountedByStatus);
                columns.AddElement(tableInventoryTotalsCountedByStatus);

                var tableChargeOff = new PdfPTable(10)
                                     {
                                         WidthPercentage = 100
                                     };
                // document.PageSize.Width;
                SectionChargeOff(tableChargeOff);
                columns.AddElement(tableChargeOff);

                document.Add(columns);
                document.Close();
                OpenFile(ReportObject.ReportTempFileFullName);
                CreateReport();
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
            template.ShowText(string.Format("{0}", (writer.PageNumber - 1)));
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
                ReportHeader(headerTbl, logo, (PostAuditReport)writer.PageEvent);
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
            string text = string.Empty;
            string[] reportName = writer.Info.Get(PdfName.TITLE).ToString().Split(':');
            text = StringUtilities.fillString(" ", 80) + " Page " + pageN + "  of ";
            var pageSize = document.PageSize;

            //add GunTotals
            const string gunText = "Inventory Report";
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
            contentByte.SetTextMatrix(pageSize.GetLeft(330), pageSize.GetBottom(15));
            contentByte.ShowText(text);
            contentByte.EndText();
            //Madhu fix for defexct PWNU00001411
            contentByte.AddTemplate(template, pageSize.GetLeft(330) + len, pageSize.GetBottom(15));


        }
        #endregion
    }
}
