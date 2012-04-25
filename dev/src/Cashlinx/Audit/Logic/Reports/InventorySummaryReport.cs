using System;
using System.Collections.Generic;
using System.IO;
using Common.Libraries.Objects.Audit;
using Common.Libraries.Utility.String;
using Reports;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Audit.Reports
{
    public class InventorySummaryReport : ReportBase
    {
        #region Fields
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        private BaseFont footerBaseFontGunTotals = null;
        private PdfTemplate templateGunTotals;
        private int _pageCount = 1;
        private int _totalNumGuns = 0;
        private decimal totalAmount = 0.0m;
        private decimal _currentInventoryBalance = 0.0m;
        #endregion

        #region Properties
        public AuditReportsObject ReportObject { get; set; }
        private bool NewPage = false;
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

        private void ReportHeader(PdfPTable headingtable, AuditReportHeaderFields headerFields)
        {
            AuditReportHeader(headingtable, headerFields);

            WriteCell(headingtable, string.Empty, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Division: " + ReportObject.ActiveAudit.Division, ReportFontRegular, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);


            WriteCell(headingtable, string.Empty, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Region: " + ReportObject.ActiveAudit.Region, ReportFontRegular, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            if (NewPage)
            {
                WriteCell(headingtable, string.Empty, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(headingtable, "Reg Manager: " + ReportObject.ActiveAudit.ActiveShopManager, ReportFontRegular, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(headingtable, string.Empty, ReportFont, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            }

            WriteCell(headingtable, string.Empty, ReportFont, 7, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 7, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

            //WriteCell(headingtable, string.Empty, ReportFont, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            //WriteCell(headingtable, string.Empty, ReportFont, 5, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            //WriteCell(headingtable, string.Empty, ReportFont, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);


        }

        private void WriteEmployeeSection(PdfPTable employeeSectionTable)
        {
            //WriteCell(employeeSectionTable, string.Empty, ReportFontBold, 12, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            string layAuditDate = ReportObject.ActiveAudit.LayAuditDate == DateTime.MinValue ? string.Empty : ReportObject.ActiveAudit.LayAuditDate.ToShortDateString();
            string auditStartDate = ReportObject.ActiveAudit.AuditStartDate == DateTime.MinValue ? string.Empty : ReportObject.ActiveAudit.AuditStartDate.ToShortDateString();
            string lastAuditDate = ReportObject.ActiveAudit.LastAuditDate == DateTime.MinValue ? string.Empty : ReportObject.ActiveAudit.LastAuditDate.ToShortDateString();
            
            AuditReportSectionDivider(employeeSectionTable, 12, string.Empty, Element.ALIGN_CENTER, ReportFontBold);
            AuditReportSectionDivider(employeeSectionTable, 12, string.Empty, Element.ALIGN_CENTER, ReportFontBold);
            AuditReportSectionDivider(employeeSectionTable, 12, string.Empty, Element.ALIGN_CENTER, ReportFontBold);

            WriteCell(employeeSectionTable, "Active Shop Manager:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, ReportObject.ActiveAudit.ActiveShopManager, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "RVP:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, ReportObject.ActiveAudit.RVP, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "Exiting Shop Manager:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, ReportObject.ActiveAudit.ExitingShopManager, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(employeeSectionTable, "Auditor:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, ReportObject.ActiveAudit.Auditor, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "Market Manager:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, ReportObject.ActiveAudit.MarketManager, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "Audit Start Date:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, auditStartDate, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(employeeSectionTable, "Inventory Type:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, ReportObject.ActiveAudit.InventoryType, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "Market Manager Present:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, ReportObject.ActiveAudit.MarketManagerPresent ? "Y" : "N", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "Last Audit Date:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, lastAuditDate, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(employeeSectionTable, "SubType:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, ReportObject.ActiveAudit.SubType, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, "Lay Audit Date:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, layAuditDate, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, string.Empty, ReportFontBold, 4, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(employeeSectionTable, string.Empty, ReportFontBold, 12, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, string.Empty, ReportFontBold, 12, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(employeeSectionTable, "Employees Present:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, ReportObject.StringbuilderInvSummEmployeesPresent.ToString(), ReportFont, 10, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(employeeSectionTable, "Term Employees:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(employeeSectionTable, ReportObject.StringbuilderInvSummTermEmployees.ToString(), ReportFont, 10, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

        }

        private void WriteCurrentInventorySection(PdfPTable currentInventorySectionTable, int colspan)
        {

            _currentInventoryBalance = ReportObject.ActiveAudit.CurrentInventoryBalance;
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            //AuditReportSectionDivider(employeeSectionTable, 12, string.Empty, Element.ALIGN_CENTER, ReportFontBold);
            AuditReportSectionDivider(currentInventorySectionTable, colspan, "Current Inventory", Element.ALIGN_CENTER, ReportFontBold);

            WriteCell(currentInventorySectionTable, "YTD Shortage:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, ReportObject.ActiveAudit.YTDShortage.ToString("C"), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "Current Loan Bal:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, ReportObject.ActiveAudit.CurrentLoanBalance.ToString("C"), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "Prev Loan Balance:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, ReportObject.ActiveAudit.PreviousLoanBalance.ToString("C"), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, "YTD Adjustments:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, ReportObject.ActiveAudit.YTDAdjustments.ToString("C"), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "Current Inventory Bal:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, _currentInventoryBalance.ToString("C"), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "Prev Inv Balance:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, ReportObject.ActiveAudit.PreviousInventoryBalance.ToString("C"), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, "Prev Year Coff:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, ReportObject.ActiveAudit.PreviousYearCoff.ToString("C"), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, "Cash In Store:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, ReportObject.ActiveAudit.CashInStore.ToString("C"), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 4, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);


            WriteCell(currentInventorySectionTable, "Net YTD Shortage:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, ReportObject.ActiveAudit.NetYTDShortage.ToString("C"), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 8, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 8, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 8, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 4, Element.ALIGN_LEFT, Rectangle.NO_BORDER, Rectangle.TOP_BORDER);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 8, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, "Over/(Short):", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, Rectangle.LEFT_BORDER);
            WriteCell(currentInventorySectionTable, ReportObject.ActiveAudit.OverShort.ToString("C"), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, Rectangle.RIGHT_BORDER);
            WriteCell(currentInventorySectionTable, "Total Temp ICN Unreconciled:", ReportFontBold, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, ReportObject.ActiveAudit.TempICNAdjustment.ToString("C"), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            //WriteCell(currentInventorySectionTable, "Procedural Audit Score:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            //WriteCell(currentInventorySectionTable, ReportObject.ActiveAudit.AuditScore.ToString(), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, "Adjustment:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, Rectangle.LEFT_BORDER);
            WriteCell(currentInventorySectionTable, ReportObject.ActiveAudit.Adjustment.ToString("C"), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, Rectangle.RIGHT_BORDER);
            WriteCell(currentInventorySectionTable, "Total Charge-On:", ReportFontBold, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, ReportObject.ActiveAudit.TotalChargeOn.ToString("C"), ReportFont, 5, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);


            WriteCell(currentInventorySectionTable, "Net Over/(Short):", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, Rectangle.LEFT_BORDER);
            WriteCell(currentInventorySectionTable, ReportObject.ActiveAudit.NetOverShort.ToString("C"), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, Rectangle.RIGHT_BORDER);
            WriteCell(currentInventorySectionTable, "Total Charge-Off:", ReportFontBold, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(currentInventorySectionTable, ReportObject.ActiveAudit.TotalChargeOff.ToString("C"), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, "Tolerance:", ReportFontBold, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, Rectangle.LEFT_BORDER);
            WriteCell(currentInventorySectionTable, ReportObject.ActiveAudit.Tolerance.ToString("C"), ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, Rectangle.RIGHT_BORDER);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 8, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 4, Element.ALIGN_LEFT, Rectangle.NO_BORDER, Rectangle.BOTTOM_BORDER);
            WriteCell(currentInventorySectionTable, string.Empty, ReportFontBold, 8, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
        }

        private void WriteChargeOffHeaders(PdfPTable chargeOffsSectionTable, int colspan)
        {
            WriteCell(chargeOffsSectionTable, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            //AuditReportSectionDivider(employeeSectionTable, 12, string.Empty, Element.ALIGN_CENTER, ReportFontBold);
            AuditReportSectionDivider(chargeOffsSectionTable, colspan, "Charge Offs", Element.ALIGN_CENTER, ReportFontBold);
            ChargeOffsSectionHeaders(chargeOffsSectionTable);
        }

        private void WriteChargeOffsSection(PdfPTable chargeOffsSectionTable, int colspan, List<AuditReportsObject.InventorySummaryChargeOffsField> chargeoffields, bool inclTotals)
        {
            int totalItems = 0;
            int totalItemsNF =0;
            decimal totalPercentItemsNF=0.0m;
            decimal totalPercentNFAmount = 0.0m;
            decimal totalAmountNF = 0.0m;
            decimal totalAmountLocal = 0.0m;
            foreach (AuditReportsObject.InventorySummaryChargeOffsField invField in chargeoffields)
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
                if (inclTotals)
                    totalAmount += invField.TotalAmount;

                totalAmountLocal += invField.TotalAmount;
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
            WriteCell(chargeOffsSectionTable, totalAmountLocal.ToString("C"), ReportFontBold, 2, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, totalAmountNF.ToString("C"), ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(chargeOffsSectionTable, totalPercentNFAmount.ToString(), ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
        }


        private void WriteAdditionalCommentsSection(PdfPTable table, int colspan)
        {
            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(table, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            AuditReportSectionDivider(table, 1, "Auditor Comments", Element.ALIGN_CENTER, ReportFontBold);
            if(ReportObject.InventoryQuestionsAdditionalComments != null)
                WriteCell(table, ReportObject.InventoryQuestionsAdditionalComments.ToString(), ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            else
                WriteCell(table, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
        }

        private void WriteDeficiencesSection(PdfPTable deficiencesSectionTable, int colspan)
        {
            WriteCell(deficiencesSectionTable, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(deficiencesSectionTable, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(deficiencesSectionTable, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            AuditReportSectionDivider(deficiencesSectionTable, colspan, "Additional Audit Information", Element.ALIGN_CENTER, ReportFontBold);

            if (ReportObject.ListInventoryQuestions != null && ReportObject.ListInventoryQuestions.Count > 0)
            {
                //var list = ReportObject.ListInventoryQuestions.OrderBy(x => x.QuestionNumber);
                foreach (AuditReportsObject.InventoryQuestion question in ReportObject.ListInventoryQuestions)
                {
                    WriteCell(deficiencesSectionTable, question.QuestionNumber.ToString() + ".", ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                    WriteCell(deficiencesSectionTable, question.Question + ":", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                    WriteCell(deficiencesSectionTable, question.Response, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                }
            }
        }

        private void WriteInventoryHistorySection(PdfPTable inventoryHistorySectionTable, int colspan)
        {
            WriteCell(inventoryHistorySectionTable, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, string.Empty, ReportFontBold, colspan, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            AuditReportSectionDivider(inventoryHistorySectionTable, colspan, "Inventory History", Element.ALIGN_CENTER, ReportFontBold);

            WriteCell(inventoryHistorySectionTable, "Date", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, "Manager", ReportFontBold, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, "Inv Type", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, "Inv Sub Type", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, "Over/(Short)", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, "Adjustment", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, "Net Over/(Short)", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(inventoryHistorySectionTable, "PA Score", ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

            foreach (AuditReportsObject.InventorySummaryHistoryField field in ReportObject.ListInventorySummaryHistoryField)
            {
                WriteCell(inventoryHistorySectionTable, field.InventoryDate.ToShortDateString(), ReportFont, 1, Element.ALIGN_CENTER, Rectangle.BOX, false);
                WriteCell(inventoryHistorySectionTable, field.Manager, ReportFont, 2, Element.ALIGN_CENTER, Rectangle.BOX, false);
                WriteCell(inventoryHistorySectionTable, field.InvType, ReportFont, 1, Element.ALIGN_CENTER, Rectangle.BOX, false);
                WriteCell(inventoryHistorySectionTable, field.InvSubType, ReportFont, 1, Element.ALIGN_CENTER, Rectangle.BOX, false);
                WriteCell(inventoryHistorySectionTable, field.OverShort.ToString("C"), ReportFont, 1, Element.ALIGN_CENTER, Rectangle.BOX, false);
                WriteCell(inventoryHistorySectionTable, field.Adjustment.ToString("C"), ReportFont, 1, Element.ALIGN_CENTER, Rectangle.BOX, false);
                WriteCell(inventoryHistorySectionTable, field.NetOverShort.ToString("C"), ReportFont, 1, Element.ALIGN_CENTER, Rectangle.BOX, false);
                WriteCell(inventoryHistorySectionTable, field.PAScore.ToString(), ReportFont, 1, Element.ALIGN_CENTER, Rectangle.BOX, false);
            }
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

        private void AuditReportSectionDivider(PdfPTable sectionTable, int colspan, string sectionText, int horizontalAlignment, Font reportFont)
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
        public InventorySummaryReport()
            : base(Common.Libraries.Utility.Shared.PdfLauncher.Instance)
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

                document.Open();
                document.SetPageSize(PageSize.LETTER);
                document.SetMargins(-100, -100, 10, 45);
                document.AddTitle(ReportObject.ReportTitle + ": " + DateTime.Now.ToString("MM/dd/yyyy"));


                PdfPTable employeeSectionTable = new PdfPTable(12);
                employeeSectionTable.WidthPercentage = 100;// document.PageSize.Width;
                WriteEmployeeSection(employeeSectionTable);
                columns.AddElement(employeeSectionTable);

                PdfPTable chargeOffsSectionTable = new PdfPTable(12);
                chargeOffsSectionTable.WidthPercentage = 100;// document.PageSize.Width;
                WriteChargeOffHeaders(chargeOffsSectionTable, 12);
                WriteChargeOffsSection(chargeOffsSectionTable, 12, ReportObject.ListInventorySummaryChargeOffsField, true);
                WriteCell(chargeOffsSectionTable, string.Empty, ReportFontBold, 12, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(chargeOffsSectionTable, string.Empty, ReportFontBold, 12, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteChargeOffsSection(chargeOffsSectionTable, 12, ReportObject.ListInventorySummaryChargeOffsFieldCACC, false);

                PdfPTable currentInventorySectionTable = new PdfPTable(12);
                currentInventorySectionTable.WidthPercentage = 100;// document.PageSize.Width;
                WriteCurrentInventorySection(currentInventorySectionTable, 12);

                columns.AddElement(currentInventorySectionTable);
                columns.AddElement(chargeOffsSectionTable);

                //PdfPTable chargeOffsCACCSectionTable = new PdfPTable(12);
                //chargeOffsCACCSectionTable.WidthPercentage = 100;// document.PageSize.Width;
                //WriteChargeOffsSection(chargeOffsCACCSectionTable, 12, ReportObject.ListInventorySummaryChargeOffsFieldCACC);
                //columns.AddElement(chargeOffsCACCSectionTable);

                document.Add(columns);
                NewPage = true;
                document.NewPage();
                MultiColumnText columns2 = new MultiColumnText(document.PageSize.Top - 120, document.PageSize.Height);
                columns2.AddSimpleColumn(20, document.PageSize.Width - 20);

                PdfPTable additionalCommentsSectionTable = new PdfPTable(1);
                additionalCommentsSectionTable.WidthPercentage = 100;// document.PageSize.Width;
                WriteAdditionalCommentsSection(additionalCommentsSectionTable, 1);
                columns2.AddElement(additionalCommentsSectionTable);

                PdfPTable deficiencesSectionTable = new PdfPTable(5);
                deficiencesSectionTable.WidthPercentage = 100;// document.PageSize.Width;
                WriteDeficiencesSection(deficiencesSectionTable, 5);
                columns2.AddElement(deficiencesSectionTable);

                PdfPTable inventoryHistorySectionTable = new PdfPTable(9);
                inventoryHistorySectionTable.WidthPercentage = 100;// document.PageSize.Width;
                WriteInventoryHistorySection(inventoryHistorySectionTable, 9);
                columns2.AddElement(inventoryHistorySectionTable);

                document.Add(columns2);

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
                if(NewPage)
                    headerFields.ReportTitle = "Inventory Summary Responses";
                else
                    headerFields.ReportTitle = "Inventory Summary Report";
                headerFields.StoreNumber = ReportObject.StoreNumber;
                headerFields.InventoryAuditDate = ReportObject.InventoryAuditDate;
                ReportHeader(headerTbl, headerFields);

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
            string gunText = "Inventory Report";
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
