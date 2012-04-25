using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Libraries.Objects.Audit;
using Common.Libraries.Utility.String;
using Reports;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Audit.Reports
{
    public class PostAuditReport : ReportBase
    {
        #region Fields
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        private BaseFont footerBaseFontGunTotals = null;
        private PdfTemplate templateGunTotals;
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

        private List<AuditReportsObject.PostAuditField> ParseFields(int category)
        {
            var fields = from listitems in ReportObject.ListPostAuditField where listitems.Category == category select listitems;
            return fields.ToList();
        }

        private void WriteSections(PdfPTable table, int colspan, string section, int sectionEnum)
        {
            //var chargeOffFields = from chargeoffs in ReportObject.ListPostAuditField where chargeoffs.Category.Equals("Charge Off") select chargeoffs;
            AuditReportSectionDivider(table, colspan, section, Element.ALIGN_CENTER, ReportFontBold);
            int totalQty = 0;
            decimal totalCost = 0.0m;
            decimal totalRetail = 0.0m;

            WriteCell(table, "ICN", ReportFontUnderlined, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            if(sectionEnum == (int)EnumPostAuditReportCategories.ChargeOff)
                WriteCell(table, "Reason", ReportFontUnderlined, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, "Merchandise Description", ReportFontUnderlined, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, "Qty.", ReportFontUnderlined, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, "Cost", ReportFontUnderlined, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, "Retail", ReportFontUnderlined, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            List<AuditReportsObject.PostAuditField> listFields = ParseFields(sectionEnum);
            if (sectionEnum == (int)EnumPostAuditReportCategories.ChargeOff)
            {
                var groupedFields = listFields.GroupBy(reason => reason.Reason);
                foreach (var auditGroup in groupedFields)
                {
                    int groupTotalQty = 0;
                    decimal groupTotalCost = 0.0m;
                    decimal groupTotalRetail = 0.0m;
                    foreach (AuditReportsObject.PostAuditField field in auditGroup)
                    {
                        PdfPCell cell = new PdfPCell();
                        cell = GetFormattedICNCellSmallFont(field.ICN);
                        cell.Colspan = 2;
                        table.AddCell(cell);

                        WriteCell(table, field.Reason, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                        WriteCell(table, field.MerchandiseDescription, ReportFont, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                        WriteCell(table, field.Qty.ToString(), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                        WriteCell(table, field.Cost.ToString("C"), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                        WriteCell(table, field.Retail.ToString("C"), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

                        groupTotalQty += field.Qty;
                        groupTotalCost += field.Cost;
                        groupTotalRetail += field.Retail;

                        totalQty += field.Qty;
                        totalCost += field.Cost;
                        totalRetail += field.Retail;
                    }
                    WriteCell(table, "SubTotals", ReportFontMediumBold, 6, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                    WriteCell(table, groupTotalQty.ToString(), ReportFontMediumBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                    WriteCell(table, groupTotalCost.ToString("C"), ReportFontMediumBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                    WriteCell(table, groupTotalRetail.ToString("C"), ReportFontMediumBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

                    WriteCell(table, string.Empty, ReportFontMediumBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                    //here add code to do the sub totals
                }
            }
            else
            {
                foreach (AuditReportsObject.PostAuditField field in listFields)
                {
                    PdfPCell cell = new PdfPCell();
                    cell = GetFormattedICNCellSmallFont(field.ICN);
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    WriteCell(table, field.MerchandiseDescription, ReportFont, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                    WriteCell(table, field.Qty.ToString(), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                    WriteCell(table, field.Cost.ToString("C"), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                    WriteCell(table, field.Retail.ToString("C"), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                    totalQty += field.Qty;
                    totalCost += field.Cost;
                    totalRetail += field.Retail;
                }
            }
            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.BOTTOM_BORDER, false);
            if (sectionEnum == (int)EnumPostAuditReportCategories.ChargeOff)
                WriteCell(table, "Total Merchandise", ReportFontMediumBold, 6, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            else
                WriteCell(table, "Total Merchandise", ReportFontMediumBold, 5, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, totalQty.ToString(), ReportFontMediumBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, totalCost.ToString("C"), ReportFontMediumBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, totalRetail.ToString("C"), ReportFontMediumBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
        }


        private void SectionInventoryTotalsCountedByStatus(PdfPTable table, int colspan)
        {
            int totalQty = 0;
            decimal totalCost = 0.0m;
            AuditReportSectionDivider(table, colspan, "Inventory Totals Counted by Status", Element.ALIGN_CENTER, ReportFontBold);
            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(table, string.Empty, ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, "Qty", ReportFontUnderlined, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(table, "Cost", ReportFontUnderlined, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            if (ReportObject.ListPostAuditInventoryTotalsField != null && ReportObject.ListPostAuditInventoryTotalsField.Count > 0)
            {
                int rowCount = 1;
                foreach (AuditReportsObject.PostAuditInventoryTotalsField invTotalsField in ReportObject.ListPostAuditInventoryTotalsField)
                {
                    if (!string.IsNullOrEmpty(invTotalsField.InventoryType))
                    {
                        WriteCell(table, string.Empty, ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                        if (rowCount % 2 == 0)
                        {
                            WriteCell(table, invTotalsField.InventoryType, ReportFontMediumBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                            WriteCell(table, invTotalsField.Qty.ToString(), ReportFont, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                            WriteCell(table, invTotalsField.Cost.ToString("C"), ReportFont, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                        }
                        else
                        {
                            WriteCell(table, invTotalsField.InventoryType, ReportFontMediumBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, true);
                            WriteCell(table, invTotalsField.Qty.ToString(), ReportFont, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, true);
                            WriteCell(table, invTotalsField.Cost.ToString("C"), ReportFont, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, true);
                        }
                        WriteCell(table, string.Empty, ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                        totalQty += invTotalsField.Qty;
                        totalCost += invTotalsField.Cost;
                    }
                    rowCount++;
                }
            }

            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.BOTTOM_BORDER, false);

            WriteCell(table, "Total Inventory Counted by Status", ReportFontMediumBold, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, totalQty.ToString(), ReportFontMediumBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(table, totalCost.ToString("C"), ReportFontMediumBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFontBold, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
        }

        private void WriteTempRecociliationSection(PdfPTable table, int colspan)
        {
            decimal physicalInvAdjustmentCost = (ReportObject.PostAuditTempICNReconciliation.TotalNewICNsSoldNotReconciledCost + ReportObject.PostAuditTempICNReconciliation.ChargeOnCost) - ReportObject.PostAuditTempICNReconciliation.ChargeOffCost;
            decimal cosAdjustmentCost = ReportObject.PostAuditTempICNReconciliation.TotalNewICNsSoldReconciledCost - ReportObject.PostAuditTempICNReconciliation.TotalActualICNsReconciledCost;
            decimal netInvAdjustmentCost = physicalInvAdjustmentCost + cosAdjustmentCost;
            int physicalInvAdjustmentQty = (ReportObject.PostAuditTempICNReconciliation.TotalNewICNsSoldNotReconciledQty + ReportObject.PostAuditTempICNReconciliation.ChargeOnQty) - ReportObject.PostAuditTempICNReconciliation.ChargeOffQty;
            int cosAdjustmentQty = ReportObject.PostAuditTempICNReconciliation.TotalNewICNsSoldReconciledQty - ReportObject.PostAuditTempICNReconciliation.TotalActualICNsReconciledQty;
            int netInvAdjustmentQty = physicalInvAdjustmentQty + cosAdjustmentQty;

            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            AuditReportSectionDivider(table, colspan, "Temp ICN Reconciliation", Element.ALIGN_CENTER, ReportFontBold);
            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(table, "Physical Inventory Adjustments", ReportFontBold, colspan, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

            WriteCell(table, string.Empty, ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, "Qty", ReportFontUnderlined, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, "Cost", ReportFontUnderlined, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);


            WriteCell(table, "Total New Temporary ICNs Sold - Not Reconciled", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, ReportObject.PostAuditTempICNReconciliation.TotalNewICNsSoldNotReconciledQty.ToString(), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, ReportObject.PostAuditTempICNReconciliation.TotalNewICNsSoldNotReconciledCost.ToString("C"), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(table, "Charge On", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, ReportObject.PostAuditTempICNReconciliation.ChargeOnQty.ToString(), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, ReportObject.PostAuditTempICNReconciliation.ChargeOnCost.ToString("C"), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(table, "Charge Off", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, ReportObject.PostAuditTempICNReconciliation.ChargeOffQty.ToString(), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, ReportObject.PostAuditTempICNReconciliation.ChargeOffCost.ToString("C"), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(table, "Physical Inventory Adjustment", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, physicalInvAdjustmentQty.ToString(), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, physicalInvAdjustmentCost.ToString("C"), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);



            WriteCell(table, "Cost of Sale(COS) Adjustment", ReportFontBold, colspan, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, "Qty", ReportFontUnderlined, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, "Cost", ReportFontUnderlined, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, "Total New Temporary ICNs Sold - Reconciled", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, ReportObject.PostAuditTempICNReconciliation.TotalNewICNsSoldReconciledQty.ToString(), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, ReportObject.PostAuditTempICNReconciliation.TotalNewICNsSoldReconciledCost.ToString("C"), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(table, "Total Actual ICNs - Reconciled", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, ReportObject.PostAuditTempICNReconciliation.TotalActualICNsReconciledQty.ToString(), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, ReportObject.PostAuditTempICNReconciliation.TotalActualICNsReconciledCost.ToString("C"), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(table, "C.O.S Adjustment - Reconciled Temp/Actual", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, cosAdjustmentQty.ToString(), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, cosAdjustmentCost.ToString("C"), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            //WriteCell(table, "Net Inventory Adjustment", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, Rectangle.BOTTOM_BORDER);
            //WriteCell(table, netInvAdjustmentQty.ToString(), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, Rectangle.BOTTOM_BORDER);
            //WriteCell(table, netInvAdjustmentCost.ToString("C"), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, Rectangle.BOTTOM_BORDER);

        }

        private void WriteFooter(PdfPTable table, int colspan)
        {
            string timeOfTrakkerDownload = ReportObject.ActiveAudit.DownloadDate.ToShortTimeString() + " on " + ReportObject.ActiveAudit.DownloadDate.ToShortDateString();
            string timeOfTrakkerUpload = ReportObject.ActiveAudit.UploadDate.ToShortTimeString() + " on " + ReportObject.ActiveAudit.UploadDate.ToShortDateString();
            string timeAuditCompleted = ReportObject.ActiveAudit.DateCompleted.ToShortTimeString() + " on " + ReportObject.ActiveAudit.DateCompleted.ToShortDateString();

            WriteCell(table, "Time of Trakker Download", ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, timeOfTrakkerDownload, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(table, "Time of Trakker Upload", ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, timeOfTrakkerUpload, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(table, "Time Audit Completed", ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, timeAuditCompleted, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(table, "Signed: X_________________________________________", ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, "Auditor", ReportFont, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(table, "Signed: X_________________________________________", ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, "Shop Manager", ReportFont, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
        }
        #endregion

        #region Constructors
        public PostAuditReport()
            : base(Common.Libraries.Utility.Shared.PdfLauncher.Instance)
        {

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
                //ReportObject.ReportTempFile = "c:\\Program Files\\Phase2App\\logs\\";
                ReportObject.CreateTemporaryFullName("PostAuditReport");
                _pageCount = 1;
                PostAuditReport events = this;
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
                document.Open();
                document.SetPageSize(PageSize.LETTER);
                document.SetMargins(-100, -100, 10, 45);
                document.AddTitle(ReportObject.ReportTitle + ": " + DateTime.Now.ToString("MM/dd/yyyy"));

                PdfPTable tableInventoryTotalsCountedByStatus = new PdfPTable(6);
                tableInventoryTotalsCountedByStatus.WidthPercentage = 100;// document.PageSize.Width;
                SectionInventoryTotalsCountedByStatus(tableInventoryTotalsCountedByStatus, 6);
                columns.AddElement(tableInventoryTotalsCountedByStatus);

                PdfPTable tableChargeOff = new PdfPTable(9);
                tableChargeOff.WidthPercentage = 100;// document.PageSize.Width;
                WriteSections(tableChargeOff, 9, "Charge Off", (int)EnumPostAuditReportCategories.ChargeOff);
                columns.AddElement(tableChargeOff);

                PdfPTable tableReactivation = new PdfPTable(8);
                tableReactivation.WidthPercentage = 100;// document.PageSize.Width;
                WriteSections(tableReactivation, 8, "Reactivation", (int)EnumPostAuditReportCategories.Reactivation);
                columns.AddElement(tableReactivation);

                PdfPTable tableChargeOn = new PdfPTable(8);
                tableChargeOn.WidthPercentage = 100;// document.PageSize.Width;
                WriteSections(tableChargeOn, 8, "Charge On", (int)EnumPostAuditReportCategories.ChargeOn);
                columns.AddElement(tableChargeOn);

                PdfPTable tabletempRecon = new PdfPTable(4);
                tabletempRecon.WidthPercentage = 100;// document.PageSize.Width;
                WriteTempRecociliationSection(tabletempRecon, 4);
                columns.AddElement(tabletempRecon);

                PdfPTable tableFooter = new PdfPTable(3);
                tableFooter.WidthPercentage = 100;// document.PageSize.Width;
                WriteFooter(tableFooter, 3);
                columns.AddElement(tableFooter);

                document.Add(columns);
                document.Close();
                //OpenFile(ReportObject.ReportTempFileFullName);
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
                PdfPTable headerTbl = new PdfPTable(7);
                headerTbl.TotalWidth = document.PageSize.Width - 10;
                AuditReportHeaderFields headerFields = new AuditReportHeaderFields();
                headerFields.ReportNumber = ReportObject.ReportNumber;
                headerFields.ReportStore = ReportObject.ReportStore;
                headerFields.ReportTitle = ReportObject.ReportTitle;
                headerFields.StoreNumber = ReportObject.StoreNumber;
                headerFields.InventoryAuditDate = ReportObject.InventoryAuditDate;
                AuditReportHeader(headerTbl, headerFields);
                //ReportHeader(headerTbl, logo, (PostAuditReport)writer.PageEvent);
                //write the rows out to the PDF output stream. I use the height of the document to position the table. Positioning seems quite strange in iTextSharp and caused me the biggest headache.. It almost seems like it starts from the bottom of the page and works up to the top, so you may ned to play around with this.
                if (PageCount == 1)
                    headerTbl.WriteSelectedRows(0, -1, 16, (document.PageSize.Height - 10), writer.DirectContent);
                else
                    headerTbl.WriteSelectedRows(0, -1, 16, (document.PageSize.Height - 8), writer.DirectContent);
                PageCount++;
            }
            catch (Exception)
            {
            }
        }

        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            int pageN = writer.PageNumber;
            string text = string.Empty;
            string[] reportName = writer.Info.Get(PdfName.TITLE).ToString().Split(':');
            text = StringUtilities.fillString(" ", 80) + " Page " + pageN + "  of ";
            Rectangle pageSize = document.PageSize;

            //add GunTotals
            string gunText = "Post Audit Report";
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
