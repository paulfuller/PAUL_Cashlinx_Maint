using System;
using System.IO;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.String;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.Audit
{
    public class InventorySummaryReport: ReportBase
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

        private void ReportHeader(PdfPTable headingtable, Image gif, InventorySummaryReport pageEvent)
        {
            PdfPCell cell = new PdfPCell();

            //  row 1
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 6;
            headingtable.AddCell(cell);

            WriteCell(headingtable, string.Empty, ReportFontMedium, 5, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Cash America Pawn of Nashville", ReportFontRegular, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(headingtable, string.Empty, ReportFont, 5, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Shop Number: #00003", ReportFontRegular, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(headingtable, string.Empty, ReportFont, 5, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Inventory Audit Date: 6/27/2011", ReportFontRegular, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(headingtable, string.Empty, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Inventory Summary Report", ReportFontHeading, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Report #: xxx", ReportFontRegular, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(headingtable, string.Empty, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Division: FLD-C.O.O", ReportFontRegular, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);


            WriteCell(headingtable, string.Empty, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Region: FLD-SE", ReportFontRegular, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(headingtable, string.Empty, ReportFont, 7, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 7, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

            //WriteCell(headingtable, string.Empty, ReportFont, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            //WriteCell(headingtable, string.Empty, ReportFont, 5, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            //WriteCell(headingtable, string.Empty, ReportFont, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);


        }

        private void WriteEmployeeSection(PdfPTable employeeSectionTable)
        {
            //WriteCell(employeeSectionTable, string.Empty, ReportFontBold, 12, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            AuditReportSectionDivider(employeeSectionTable, 12, string.Empty, Element.ALIGN_CENTER, ReportFontBold);
            AuditReportSectionDivider(employeeSectionTable, 12, string.Empty, Element.ALIGN_CENTER, ReportFontBold);
            AuditReportSectionDivider(employeeSectionTable, 12, string.Empty, Element.ALIGN_CENTER, ReportFontBold);

            WriteCell(employeeSectionTable, "Active Shop Manager:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "Orin Jackson", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "RVP:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "M.Hatchel", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "Exiting Shop Manager:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "M.Hatchel bbbbdfsssssssssssaaee", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(employeeSectionTable, "Auditor:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "L. Hicks", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "Market Manager:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "D.Whitmore", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "Audit Start Date:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "12/13/2010", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(employeeSectionTable, "Inventory Type:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "Full", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "Market Manager Present:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "No", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "Last Audit Date:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "12/13/2010", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(employeeSectionTable, "SubType:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "Routine", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "Lay Audit Date:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "07/13/2011", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, string.Empty, ReportFontBold, 4, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(employeeSectionTable, string.Empty, ReportFontBold, 12, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, string.Empty, ReportFontBold, 12, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(employeeSectionTable, "Employees Present:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "Black, Geoffery Jackson, Orrin, Magee Jennifer", ReportFont, 10, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(employeeSectionTable, "Term Employees:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "York, Brandy Lee", ReportFont, 10, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

        }

        private void WriteCurrentInventorySection(PdfPTable currentInventorySectionTable)
        {
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 12, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 12, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 12, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            //AuditReportSectionDivider(employeeSectionTable, 12, string.Empty, Element.ALIGN_CENTER, ReportFontBold);
            AuditReportSectionDivider(currentInventorySectionTable, 12, "Current Inventory", Element.ALIGN_CENTER, ReportFontBold);

            WriteCell(currentInventorySectionTable, "YTD Shortage:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "($2124.08)", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "Current Loan Bal:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "$151,252.50", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "Prev Loan Balance:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "$103,656.35", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, "YTD Adjustments:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "$0.00", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "Current Inventory Bal:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "$160,032.35", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "Prev Inv Balance:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "$129,103.76", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, "Prev Year Coff:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "($2124.08)", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "Cash In Store:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "$2124.08", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 4, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);


            WriteCell(currentInventorySectionTable, "Net YTD Shortage:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "$2124.08", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 8, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 8, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "Adjustment Reason:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "None", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 8, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "Adjustment Type:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "None", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, "Over/(Short):", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "($0.00)", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "Total Charge-On:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "$160,032.35", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "Procedural Audit Score:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "255", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, "Adjustment:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "($0.00)", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "Total Charge-Off:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "$160,032.35", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 4, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, "Net Over/(Short):", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "($0.00)", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "Temp ICN Adjustment:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "$160,032.35", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 4, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, "Tolerance:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "($0.00)", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 8, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

        }

        private void WriteChargeOffsSection(PdfPTable chargeOffsSectionTable)
        {
            WriteCell(chargeOffsSectionTable, string.Empty, ReportFontBold, 12, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, string.Empty, ReportFontBold, 12, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, string.Empty, ReportFontBold, 12, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            //AuditReportSectionDivider(employeeSectionTable, 12, string.Empty, Element.ALIGN_CENTER, ReportFontBold);
            AuditReportSectionDivider(chargeOffsSectionTable, 12, "Charge Offs", Element.ALIGN_CENTER, ReportFontBold);
            ChargeOffsSectionHeaders(chargeOffsSectionTable);
            var totalItems = 0;
            var totalItemsNF =0;
            var totalPercentItemsNF=0.0m;
            var totalAmount = 0.0m;
            var totalAmountNF = 0.0m;
            var totalPercentNFAmount = 0.0m;

            foreach (var invField in ReportObject.ListInventorySummaryChargeOffsField)
            {
                //compute Totals Here
                WriteCell(chargeOffsSectionTable, invField.Category.ToString(), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.BOX, false);
                WriteCell(chargeOffsSectionTable, invField.CategoryDescription, ReportFont, 4, Element.ALIGN_LEFT, Rectangle.BOX, false);
                WriteCell(chargeOffsSectionTable, invField.TotalItems.ToString(), ReportFont, 1, Element.ALIGN_RIGHT, Rectangle.BOX, false);
                WriteCell(chargeOffsSectionTable, invField.TotalItemsNF.ToString(), ReportFont, 1, Element.ALIGN_RIGHT, Rectangle.BOX, false);
                WriteCell(chargeOffsSectionTable, invField.PercentItemsNF.ToString(), ReportFont, 1, Element.ALIGN_RIGHT, Rectangle.BOX, false);
                WriteCell(chargeOffsSectionTable, invField.TotalAmount.ToString("C"), ReportFont, 2, Element.ALIGN_RIGHT, Rectangle.BOX, false);
                WriteCell(chargeOffsSectionTable, invField.TotalAmountNF.ToString("C"), ReportFont, 1, Element.ALIGN_RIGHT, Rectangle.BOX, false);
                WriteCell(chargeOffsSectionTable, invField.PercentNFAmount.ToString(), ReportFont, 1, Element.ALIGN_RIGHT, Rectangle.BOX, false);

                totalItems += invField.TotalItems;
                totalItemsNF += invField.TotalItemsNF;
                totalPercentItemsNF += invField.PercentItemsNF;
                totalAmount += invField.TotalAmount;
                totalAmountNF += invField.TotalAmountNF;
                totalPercentNFAmount += invField.PercentNFAmount;
            }

            WriteCell(chargeOffsSectionTable, string.Empty, ReportFont, 5, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, StringUtilities.fillString("_", 80), ReportFontBold, 7, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            
            WriteCell(chargeOffsSectionTable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, "Totals", ReportFontBold, 4, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, totalItems.ToString(), ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, totalItemsNF.ToString(), ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, totalPercentItemsNF.ToString(), ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, totalAmount.ToString("C"), ReportFontBold, 2, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, totalAmountNF.ToString("C"), ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, totalPercentNFAmount.ToString(), ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
        }

     
        private void ChargeOffsSectionHeaders(PdfPTable chargeOffsSectionTable)
        {
            WriteCell(chargeOffsSectionTable, "Category", ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, "Category Description", ReportFontBold, 4, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, "Total Items", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, "Total Items N/F", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, "% Items N/F", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, "Total Amount", ReportFontBold, 2, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, "Total Amount N/F", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, "% N/F Amount", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);

        }

        private new void AuditReportSectionDivider(PdfPTable sectionTable, int colspan, string sectionText, int horizontalAlignment, Font reportFont)
        {
            var cell = new PdfPCell(new Phrase(sectionText, reportFont))
                       {
                           Colspan = colspan,
                           HorizontalAlignment = horizontalAlignment,
                           BackgroundColor = new BaseColor(System.Drawing.Color.LightGray),
                           Border = Rectangle.NO_BORDER
                       };
            sectionTable.AddCell(cell);
        }

        

        #endregion

        #region Constructors
        public InventorySummaryReport(IPdfLauncher pdfLauncher) : base(pdfLauncher)
        {
             //_ReportObject = new AuditReportsObject();
             //ReportObject = _ReportObject;
        }
        #endregion

        #region Public Methods
        public bool CreateReport()
        {
            
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.LETTER);
            try
            {
                //set up RunReport event overrides & create doc
                //ReportObject.BuildChargeOffsList();
                ReportObject.ReportTempFile = "c:\\Program Files\\Phase2App\\logs\\AuditReportsTemp\\";
                ReportObject.CreateTemporaryFullName("InventorySummaryReport");
                _pageCount = 1;
                InventorySummaryReport events = this;
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ReportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                MultiColumnText columns = new MultiColumnText(document.PageSize.Top - 120, document.PageSize.Height);
                float pageLeft = document.PageSize.Left;
                float pageright = document.PageSize.Right;
                columns.AddSimpleColumn(20, document.PageSize.Width - 20);

                //set up tables, etc...
              
                PdfPCell cell = new PdfPCell();
                Image gif = Image.GetInstance(Common.Properties.Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(25);
                runReport = new RunReport();
                document.Open();
                document.SetPageSize(PageSize.LETTER);
                document.SetMargins(-100, -100, 10, 45);
                document.AddTitle(ReportObject.ReportTitle + ": " + DateTime.Now.ToString("MM/dd/yyyy"));


                PdfPTable employeeSectionTable = new PdfPTable(12);
                employeeSectionTable.WidthPercentage = 100;// document.PageSize.Width;
                WriteEmployeeSection(employeeSectionTable);
                columns.AddElement(employeeSectionTable);

                PdfPTable currentInventorySectionTable = new PdfPTable(12);
                currentInventorySectionTable.WidthPercentage = 100;// document.PageSize.Width;
                WriteCurrentInventorySection(currentInventorySectionTable);
                columns.AddElement(currentInventorySectionTable);

                PdfPTable chargeOffsSectionTable = new PdfPTable(12);
                chargeOffsSectionTable.WidthPercentage = 100;// document.PageSize.Width;
                WriteChargeOffsSection(chargeOffsSectionTable);
                columns.AddElement(chargeOffsSectionTable);

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
                ReportHeader(headerTbl, logo, (InventorySummaryReport)writer.PageEvent);

                //write the rows out to the PDF output stream. I use the height of the document to position the table. Positioning seems quite strange in iTextSharp and caused me the biggest headache.. It almost seems like it starts from the bottom of the page and works up to the top, so you may ned to play around with this.
                if (PageCount == 1)
                    headerTbl.WriteSelectedRows(0, -1, 16, (document.PageSize.Height - 10), writer.DirectContent);
                else
                    headerTbl.WriteSelectedRows(0, -1, 16, (document.PageSize.Height - 8), writer.DirectContent);
                PageCount++;
            }
            catch (Exception eX)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Exception: {0}", eX);
                }
                BasicExceptionHandler.Instance.AddException("InventorySummaryReport", eX);
            }
        }

        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            var pageN = writer.PageNumber;
            var text = string.Empty;
            var reportName = writer.Info.Get(PdfName.TITLE).ToString().Split(':');
            text = string.Format("{0} Page {1}  of ", StringUtilities.fillString(" ", 80), pageN);
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
