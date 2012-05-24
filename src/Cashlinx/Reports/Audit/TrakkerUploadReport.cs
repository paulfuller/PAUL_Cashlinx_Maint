using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;
using Common.Libraries.Utility.String;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.Audit
{
    public class TrakkerUploadReport : ReportBase
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

        #region Report Repeating Cells
        private PdfPCell CellScanSequenceNumber(int scanSequenceNumber)
        {
            PdfPCell cell = new PdfPCell(new Phrase(scanSequenceNumber.ToString(), ReportFontSmall));
            cell.Colspan = 1;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            return cell;
        }

        private PdfPCell CellScanSequenceNumberHeader()
        {
            PdfPCell cell = new PdfPCell(new Phrase("Scan Sequence #", ReportFontUnderlined));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            return cell;
        }

        private PdfPCell CellTrakFlag(string trakFlag)
        {
            PdfPCell cell = new PdfPCell(new Phrase(trakFlag, ReportFontSmall));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            return cell;
        }

        private PdfPCell CellTrakFlagHeader()
        {
            PdfPCell cell = new PdfPCell(new Phrase("Trak Flag", ReportFontUnderlined));
            cell.Colspan = 1;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            return cell;
        }

        private PdfPCell CellTrakID(int trakID)
        {
            PdfPCell cell = new PdfPCell(new Phrase(trakID.ToString(), ReportFontSmall));
            cell.Colspan = 1;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            return cell;
        }

        private PdfPCell CellTrakIDHeader()
        {
            PdfPCell cell = new PdfPCell(new Phrase("Trak ID", ReportFontUnderlined));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            return cell;
        }

        private PdfPCell CellPostAuditStatus(string postAuditStatus)
        {
            PdfPCell cell = new PdfPCell(new Phrase(postAuditStatus, ReportFontSmall));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            return cell;
        }

        private PdfPCell CellPostAuditStatusHeader()
        {
            PdfPCell cell = new PdfPCell(new Phrase("Post Audit Status", ReportFontUnderlined));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            return cell;
        }

        private PdfPCell CellRetail(decimal retail)
        {
            PdfPCell cell = new PdfPCell(new Phrase(retail.ToString("C"), ReportFontSmall));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            return cell;
        }

        private PdfPCell CellRetailHeader()
        {
            PdfPCell cell = new PdfPCell(new Phrase("Retail", ReportFontUnderlined));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            return cell;
        }

        private PdfPCell CellCost(decimal cost)
        {
            PdfPCell cell = new PdfPCell(new Phrase(cost.ToString("C"), ReportFontSmall));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            return cell;
        }

        private PdfPCell CellCostHeader()
        {
            PdfPCell cell = new PdfPCell(new Phrase("Cost", ReportFontUnderlined));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            return cell;
        }

        private PdfPCell CellStatus(string status)
        {
            PdfPCell cell = new PdfPCell(new Phrase(status, ReportFontSmall));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            return cell;
        }

        private PdfPCell CellStatusHeader()
        {
            PdfPCell cell = new PdfPCell(new Phrase("Status", ReportFontUnderlined));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            return cell;
        }

        private PdfPCell CellMerchandiseDescription(string merchDescription)
        {
            PdfPCell cell = new PdfPCell(new Phrase(merchDescription, ReportFontSmall));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            return cell;
        }

        private PdfPCell CellMerchandiseDescriptionHeader()
        {
            PdfPCell cell = new PdfPCell(new Phrase("Merchandise Description", ReportFontUnderlined));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            return cell;
        }

        private PdfPCell CellICNHeader()
        {
            PdfPCell cell = new PdfPCell(new Phrase("ICN", ReportFontUnderlined));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Border = Rectangle.NO_BORDER;
            return cell;
        }

        private PdfPCell CellICN(string ICN)
        {
            PdfPCell icnCell = GetFormattedICNCellSmallFont(ICN);
            icnCell.Colspan = 2;
            return icnCell;
        }
        #endregion

        #region Private Methods
        private void ReportHeader(PdfPTable headingtable, Image gif, TrakkerUploadReport pageEvent)
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
            WriteCell(headingtable, "Trakker Upload Report", ReportFontHeading, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Report #: xxx", ReportFontRegular, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(headingtable, string.Empty, ReportFont, 7, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 7, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

        }

        private void SectionICNsSinceLastInventory(PdfPTable table, int columnsCount)
        {
            AuditReportSectionDivider(table, 9, "Section I - Temporary ICN's Added Since Last Inventory", Element.ALIGN_CENTER, ReportFontBold);

            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

            WriteCell(table, string.Empty, ReportFont, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            table.AddCell(CellICNHeader());
            table.AddCell(CellMerchandiseDescriptionHeader());
            table.AddCell(CellCostHeader());
            table.AddCell(CellRetailHeader());
            table.AddCell(CellStatusHeader());

            var listFields = GetTrakkerUploadFields("Since Last Inventory");
            int counter = 0;
            decimal totalCost = 0.0m;
            decimal totalRetail = 0.0m;
            foreach (var field in listFields)
            {
                counter++;
                WriteCell(table, counter.ToString() + ".", ReportFontSmall, 1, Element.ALIGN_LEFT, Element.ALIGN_BOTTOM, Rectangle.NO_BORDER);
                WriteSectionDetails(table, field.ICN, field.MerchandiseDescription, field.Cost, field.Retail, field.Status);
                totalCost += field.Cost;
                totalRetail += field.Retail;
            }

            //WriteCell(tableIcnsSinceLastInventory, PawnUtilities.String.StringUtilities.fillString("_", 144), ReportFont, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, "Total Merchandise Added:", ReportFontBold, 5, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
            WriteCell(table, counter.ToString(), ReportFontBold, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
            WriteCell(table, totalCost.ToString("C"), ReportFontBold, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
            WriteCell(table, totalRetail.ToString("C"), ReportFontBold, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
            WriteCell(table, string.Empty, ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.TOP_BORDER, false);
            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.TOP_BORDER);

            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

            WriteCell(table, "1.", ReportFontSmall, 1, Element.ALIGN_LEFT, Element.ALIGN_BOTTOM, Rectangle.NO_BORDER);
            WriteCell(table, "Total New Temporary ICN's", ReportFontSmall, 4, Element.ALIGN_CENTER, Element.ALIGN_BOTTOM, Rectangle.NO_BORDER);
            WriteCell(table, "10", ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_BOTTOM, Rectangle.NO_BORDER);
            WriteCell(table, "53.96", ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_BOTTOM, Rectangle.NO_BORDER);
            WriteCell(table, "53.96", ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_BOTTOM, Rectangle.NO_BORDER);
            WriteCell(table, string.Empty, ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_BOTTOM, Rectangle.NO_BORDER);

            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
        }

        private void SectionMissingItems(PdfPTable table, int columnsCount)
        {
            AuditReportSectionDivider(table, columnsCount, "Section II - Missing Items", Element.ALIGN_CENTER, ReportFontBold);

            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

            WriteCell(table, string.Empty, ReportFont, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            table.AddCell(CellICNHeader());
            table.AddCell(CellMerchandiseDescriptionHeader());
            table.AddCell(CellCostHeader());
            table.AddCell(CellRetailHeader());
            table.AddCell(CellStatusHeader());
            WriteCell(table, "Post-Audit Status", ReportFontUnderlined, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            var listFields = GetTrakkerUploadFields("Missing Items");
            var counter = 0;
            var totalCost = 0.0m;
            var totalRetail = 0.0m;
            foreach (AuditReportsObject.TrakkerUploadReportMissingItems field in listFields)
            {
                counter++;
                WriteCell(table, counter.ToString() + ".", ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_BOTTOM, Rectangle.NO_BORDER);
                WriteSectionDetails(table, field.ICN, field.MerchandiseDescription, field.Cost, field.Retail, field.Status);
                WriteCell(table, field.PostAuditStatus, ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                totalCost += field.Cost;
                totalRetail += field.Retail;
            }

            
            //WriteCell(tableIcnsSinceLastInventory, PawnUtilities.String.StringUtilities.fillString("_", 144), ReportFont, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, "Total Merchandise Not Found:", ReportFontBold, 5, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
            WriteCell(table, counter.ToString(), ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
            WriteCell(table, totalCost.ToString("C"), ReportFontBold, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
            WriteCell(table, totalRetail.ToString("C"), ReportFontBold, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
            WriteCell(table, string.Empty, ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.TOP_BORDER, false);
            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
        }

        private void SectionNXTsSinceLastInventory(PdfPTable table, int columnsCount)
        {
            AuditReportSectionDivider(table, columnsCount, "Section III - NXT ICN's Create Since Last Inventory", Element.ALIGN_CENTER, ReportFontBold);

            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

            table.AddCell(CellICNHeader());
            table.AddCell(CellMerchandiseDescriptionHeader());
            table.AddCell(CellRetailHeader());
            table.AddCell(CellStatusHeader());
            WriteCell(table, "Transaction #", ReportFontUnderlined, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            var listFields = GetTrakkerUploadFields("NXTs Since Last");
            var counter = 0;
            var totalRetail = 0.0m;
            foreach (AuditReportsObject.TrakkerUploadReportNXTsSinceLastInventory field in listFields)
            {
                counter++;
                WriteSectionDetails(table, field.ICN, field.MerchandiseDescription, field.Retail, field.Status);
                WriteCell(table, field.TransactionNumber, ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                totalRetail += field.Retail;
            }

            //WriteCell(tableIcnsSinceLastInventory, PawnUtilities.String.StringUtilities.fillString("_", 144), ReportFont, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, "Total New NXT ICN's:", ReportFontBold, 4, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
            WriteCell(table, counter.ToString(), ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
            WriteCell(table, totalRetail.ToString("C"), ReportFontBold, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
            WriteCell(table, string.Empty, ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.TOP_BORDER, false);
            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
        }

        private void SectionUnexpectedItems(PdfPTable table, int columnsCount)
        {
            AuditReportSectionDivider(table, columnsCount, "Section IV - Unexpected Items", Element.ALIGN_CENTER, ReportFontBold);

            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

            table.AddCell(CellICNHeader());
            table.AddCell(CellMerchandiseDescriptionHeader());
            table.AddCell(CellCostHeader());
            table.AddCell(CellTrakIDHeader());
            table.AddCell(CellScanSequenceNumberHeader());
            table.AddCell(CellStatusHeader());
            table.AddCell(CellTrakFlagHeader());
            table.AddCell(CellPostAuditStatusHeader());

            var listFields = GetTrakkerUploadFields("Unexpected Items");
            var counter = 0;
            var totalCost = 0.0m;
            foreach (AuditReportsObject.TrakkerUploadReportUnexpectedItems field in listFields)
            {
                counter++;
                WriteSectionDetails(table, field.ICN, field.MerchandiseDescription, field.Cost);
                WriteCell(table, field.TrakID.ToString(), ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(table, field.ScanSequence.ToString(), ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(table, field.Status, ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(table, field.TrakFlag, ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(table, field.PostAuditStatus.ToString(), ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                totalCost += field.Cost;
            }

            //WriteCell(tableIcnsSinceLastInventory, PawnUtilities.String.StringUtilities.fillString("_", 144), ReportFont, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, "Total Merchandise Found:", ReportFontBold, 4, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
            WriteCell(table, counter.ToString(), ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
            WriteCell(table, totalCost.ToString("C"), ReportFontBold, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
            WriteCell(table, string.Empty, ReportFontBold, 5, Element.ALIGN_LEFT, Rectangle.TOP_BORDER, false);
            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
        }

        private void SectionDuplicateScans(PdfPTable table, int columnsCount)
        {
            AuditReportSectionDivider(table, columnsCount, "Section IV - Duplicate Scans", Element.ALIGN_CENTER, ReportFontBold);

            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

            table.AddCell(CellICNHeader());
            table.AddCell(CellMerchandiseDescriptionHeader());
            table.AddCell(CellCostHeader());
            table.AddCell(CellTrakIDHeader());
            WriteCell(table, "Location", ReportFontUnderlined, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            table.AddCell(CellScanSequenceNumberHeader());
            table.AddCell(CellStatusHeader());
            table.AddCell(CellTrakFlagHeader());
            table.AddCell(CellPostAuditStatusHeader());

            var listFields = GetTrakkerUploadFields("Duplicate Scans");
            var counter = 0;
            var totalCost = 0.0m;
            foreach (AuditReportsObject.TrakkerUploadReportDuplicateScans field in listFields)
            {
                counter++;
                WriteSectionDetails(table, field.ICN, field.MerchandiseDescription, field.Cost);
                WriteCell(table, field.TrakID.ToString(), ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(table, field.AuditLocation, ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(table, field.ScanSequence.ToString(), ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(table, field.Status, ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(table, field.TrakFlag, ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(table, field.PostAuditStatus.ToString(), ReportFontSmall, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                totalCost += field.Cost;
            }

            //WriteCell(tableIcnsSinceLastInventory, PawnUtilities.String.StringUtilities.fillString("_", 144), ReportFont, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, "Total Duplicate Scans:", ReportFontBold, 4, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
            WriteCell(table, counter.ToString(), ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
            WriteCell(table, totalCost.ToString("C"), ReportFontBold, 1, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
            WriteCell(table, string.Empty, ReportFontBold, 6, Element.ALIGN_LEFT, Rectangle.TOP_BORDER, false);
            WriteCell(table, string.Empty, ReportFont, columnsCount, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.TOP_BORDER);
        }
        private void WriteSectionDetails(PdfPTable table, string icn, string description, decimal cost, decimal retail, string status)
        {
            table.AddCell(CellICN(icn));
            table.AddCell(CellMerchandiseDescription(description));
            table.AddCell(CellCost(cost));
            table.AddCell(CellRetail(retail));
            table.AddCell(CellStatus(status));
        }

        private void WriteSectionDetails(PdfPTable table, string icn, string description, decimal retail, string status)
        {
            table.AddCell(CellICN(icn));
            table.AddCell(CellMerchandiseDescription(description));
            table.AddCell(CellRetail(retail));
            table.AddCell(CellStatus(status));
        }

        private void WriteSectionDetails(PdfPTable table, string icn, string description, decimal cost)
        {
            table.AddCell(CellICN(icn));
            table.AddCell(CellMerchandiseDescription(description));
            table.AddCell(CellRetail(cost));
        }

        private IEnumerable<AuditReportsObject.TrakkerUploadReportSinceLastInventory> GetTrakkerUploadFields(string category)
        {
            var trakkerUploadFieldsList = from trakkerUploadFields in ReportObject.ListTrakkerUploadReportField where trakkerUploadFields.Category.Equals(category) select trakkerUploadFields;
            return trakkerUploadFieldsList.ToList();
        }
        #endregion

        #region Constructors
        public TrakkerUploadReport(IPdfLauncher pdfLauncher) : base(pdfLauncher)
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
                ReportObject.BuildTrakkerUploadFieldsList();
                ReportObject.ReportTempFile = "c:\\Program Files\\Phase2App\\logs\\AuditReportsTemp\\";
                ReportObject.CreateTemporaryFullName("TrakkerUploadReport");
                _pageCount = 1;
                var events = this;
                var writer = PdfWriter.GetInstance(document, new FileStream(ReportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                var columns = new MultiColumnText(document.PageSize.Top - 120, document.PageSize.Height);
                var pageLeft = document.PageSize.Left;
                var pageright = document.PageSize.Right;
                columns.AddSimpleColumn(20, document.PageSize.Width - 20);

                //set up tables, etc...

                var gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(25);
                runReport = new RunReport();
                document.Open();
                document.SetPageSize(PageSize.LETTER);
                document.SetMargins(-100, -100, 10, 45);
                document.AddTitle(ReportObject.ReportTitle + ": " + DateTime.Now.ToString("MM/dd/yyyy"));

                //here get the List with all the data, if list 
                PdfPTable tableIcnsSinceLastInventory = new PdfPTable(9);
                tableIcnsSinceLastInventory.WidthPercentage = 100;// document.PageSize.Width;
                SectionICNsSinceLastInventory(tableIcnsSinceLastInventory, 9);
                columns.AddElement(tableIcnsSinceLastInventory);

                PdfPTable tableMissingitems = new PdfPTable(10);
                tableMissingitems.WidthPercentage = 100;// document.PageSize.Width;
                SectionMissingItems(tableMissingitems, 10);
                columns.AddElement(tableMissingitems);

                PdfPTable tableNXTsSinceLast = new PdfPTable(8);
                tableNXTsSinceLast.WidthPercentage = 100;// document.PageSize.Width;
                SectionNXTsSinceLastInventory(tableNXTsSinceLast, 8);
                columns.AddElement(tableNXTsSinceLast);

                PdfPTable tableUnexpectedItems = new PdfPTable(11);
                tableUnexpectedItems.WidthPercentage = 100;// document.PageSize.Width;
                SectionUnexpectedItems(tableUnexpectedItems, 11);
                columns.AddElement(tableUnexpectedItems);

                PdfPTable tableDuplicateScans = new PdfPTable(12);
                tableDuplicateScans.WidthPercentage = 100;// document.PageSize.Width;
                SectionDuplicateScans(tableDuplicateScans, 12);
                columns.AddElement(tableDuplicateScans);

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
                var logo = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                logo.ScalePercent(25);
                ReportHeader(headerTbl, logo, (TrakkerUploadReport)writer.PageEvent);

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

        #region Helper Class
        #endregion
    }
}
