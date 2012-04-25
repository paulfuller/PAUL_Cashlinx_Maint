using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Common.Controllers.Application;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Common.Libraries.Utility.Logger;

namespace Reports
{
    public class SnapshotReport : PdfPageEventHelper
    {
        //used by overriden methods for footers
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;

        //used by report
        private Font _reportFont;
        private int _Columns = 5;

        private string _StoreNumber
        {
            get { return reportObject.ReportStore; }
        }
        private DateTime _ShopTime;
        private List<PotentialCategoryInfo> _PotentialCategories = new List<PotentialCategoryInfo>();
        private string _ReportTitle = "Snapshot Performance Report";
        private ReportObject.Snapshot _TotalIncome;

        //main objects
        public ReportObject reportObject;

        public SnapshotReport()
        {
            _ShopTime = DateTime.Now;
        }

        public bool CreateReport()
        {
            LoadPotentialCategories();
            _TotalIncome = new ReportObject.Snapshot();
            bool isSuccessful = false;

            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.LEGAL);

            try
            {
                //set up RunReport event overrides & create doc
                string reportFileName = Path.GetFullPath(reportObject.ReportTempFileFullName);
                if (!Directory.Exists(Path.GetDirectoryName(reportFileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(reportFileName));
                }
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportFileName, FileMode.Create));
                writer.PageEvent = this;

                PdfPTable table = new PdfPTable(_Columns);
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(35);

                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);

                document.AddTitle(reportObject.ReportTitle);

                document.SetPageSize(PageSize.LETTER);
                document.SetMargins(-50, -55, 10, 45);

                table.HeaderRows = 2;
                PrintReportHeader(table, gif);
                PrintReportDetailHeader(table);
                table.SetWidths(new float[] { 1.5f, 2, 1, 1, 1.5f });
                foreach (PotentialCategoryInfo categoryInfo in _PotentialCategories)
                {
                    PrintReportDetail(table, categoryInfo);
                }
                PrintReportDetailDisclaimer(table);

                document.Open();
                document.Add(table);
                document.Close();

                isSuccessful = true;
            }
            catch (DocumentException de)
            {
                reportObject.ReportError = de.Message;
                reportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            catch (IOException ioe)
            {
                reportObject.ReportError = ioe.Message;
                reportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }

            return isSuccessful;
        }

        private void PrintReportDetailDisclaimer(PdfPTable table)
        {
            PrintReportDetailValues(table, string.Empty, string.Empty, string.Empty, 0);

            StringBuilder disclaimer = new StringBuilder();
            disclaimer.AppendLine("***End of Report***");
            disclaimer.AppendLine(GetFooterDate());
            disclaimer.AppendLine("This report is generated from the Cashlinx point of sale system and may not");
            disclaimer.AppendLine("equal the monthly income statement due to additional journal entries.");

            Paragraph paragraph = new Paragraph(disclaimer.ToString(), _reportFont);
            PdfPCell cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.TOP_BORDER;
            cell.BorderWidthTop = 2;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = _Columns;
            cell.PaddingTop = 15;
            table.AddCell(cell);
        }

        private void PrintReportDetailHeader(PdfPTable table)
        {
            Paragraph paragraph = new Paragraph(string.Empty, _reportFont);
            PdfPCell cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.RIGHT_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;
            table.AddCell(cell);

            paragraph = new Paragraph("Current Day", _reportFont);
            cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = 20;
            cell.PaddingRight = 20;
            cell.Colspan = 2;
            table.AddCell(cell);

            paragraph = new Paragraph(string.Empty, _reportFont);
            cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            paragraph = new Paragraph(string.Empty, _reportFont);
            cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.RIGHT_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingRight = 20;
            cell.Colspan = 2;
            table.AddCell(cell);

            paragraph = new Paragraph("Number", _reportFont);
            cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = 20;
            table.AddCell(cell);

            paragraph = new Paragraph("Amount", _reportFont);
            cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingRight = 20;
            table.AddCell(cell);

            paragraph = new Paragraph(string.Empty, _reportFont);
            cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);
        }

        private void PrintReportDetail(PdfPTable table, PotentialCategoryInfo categoryInfo)
        {
            ReportObject.Snapshot snapshotData = GetSnapshotData(categoryInfo);

            if (categoryInfo.SequenceNumber >= 8 && categoryInfo.SequenceNumber < 9 && categoryInfo.SequenceNumber != 8.7)
            {
                _TotalIncome.Amount += snapshotData.Amount;
                _TotalIncome.Count += snapshotData.Count;
            }

            string[] values;
            if (categoryInfo.SequenceNumber == 0)
            {
                values = new string[] { categoryInfo.Category, string.Empty, string.Empty };
            }
            else if (categoryInfo.SequenceNumber == 8.7)
            {
                values = new string[] { categoryInfo.Category, (snapshotData.Amount / 100).ToString("0.00%"), string.Empty };
            }
            else if (categoryInfo.SequenceNumber == 6.2 ||
                categoryInfo.SequenceNumber == 6.4 ||
                (categoryInfo.SequenceNumber >= 8 && categoryInfo.SequenceNumber <= 8.6) ||
                (categoryInfo.SequenceNumber > 8.7 && categoryInfo.SequenceNumber < 8.8))

            {
                values = new string[] { categoryInfo.Category, string.Empty, snapshotData.Amount.ToString("c") };
            }
            else if (categoryInfo.SequenceNumber == 9)
            {
                values = new string[] { categoryInfo.Category, string.Empty, _TotalIncome.Amount.ToString("c") };
            }
            else
            {
                values = new string[] { categoryInfo.Category, snapshotData.Count.ToString(), snapshotData.Amount.ToString("c") };
            }

            if (categoryInfo.SequenceNumber == 9)
            {
                PrintReportDetailValues(table, string.Empty, string.Empty, "---------------", 0);
            }

            PrintReportDetailValues(table, values[0], values[1], values[2], categoryInfo.Tabs);

        }

        private void PrintReportDetailValues(PdfPTable table, string category, string count, string amount, int tabs)
        {
            Paragraph paragraph = new Paragraph(string.Empty, _reportFont);
            PdfPCell cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = 20;
            table.AddCell(cell);

            paragraph = new Paragraph(category, _reportFont);
            cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.RIGHT_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = 15 * tabs;
            table.AddCell(cell);

            paragraph = new Paragraph(count, _reportFont);
            cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = 20;
            table.AddCell(cell);

            paragraph = new Paragraph(amount, _reportFont);
            cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingRight = 20;
            table.AddCell(cell);

            paragraph = new Paragraph(string.Empty, _reportFont);
            cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = 20;
            table.AddCell(cell);
        }

        private void PrintReportHeader(PdfPTable table, Image gif)
        {
            PdfPCell cell = new PdfPCell();
            PdfPTable tmpTable = new PdfPTable(1);

            //left header

            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = -10;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Operational", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Report Date: " + this.reportObject.ReportParms[0].ToString(), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(tmpTable);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = -10;
            cell.Colspan = 3;
            table.AddCell(cell);

            // right header

            tmpTable = new PdfPTable(1);
            cell = new PdfPCell(new Paragraph(reportObject.ReportStoreDesc, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.NoWrap = true;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Run Date: " + _ShopTime.ToString("MM/dd/yyyy HH:mm"), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Report # 211", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(tmpTable);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = -10;
            cell.Colspan = 2;
            table.AddCell(cell);

            // center header

            cell = new PdfPCell(new Paragraph(_ReportTitle));
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = -10;
            cell.PaddingBottom = 20;
            cell.Colspan = _Columns;
            table.AddCell(cell);
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            Rectangle pageSize = document.PageSize;

            template.BeginText();
            template.SetFontAndSize(footerBaseFont, 8);
            //template.SetTextMatrix(pageSize.GetRight(0), 0);
            //template.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, (writer.PageNumber - 1).ToString(), pageSize.GetRight(0), 0, 0);
            template.EndText();
        }

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

        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            int pageN = writer.PageNumber;
            string text = "Page " + pageN.ToString();

            float len = footerBaseFont.GetWidthPoint(text, 8);

            Rectangle pageSize = document.PageSize;

            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            contentByte.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(30));
            contentByte.ShowText(_ReportTitle);
            contentByte.EndText();

            contentByte.AddTemplate(template, pageSize.GetLeft(40) + len, pageSize.GetBottom(30));

            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            contentByte.SetTextMatrix(pageSize.GetRight(40), pageSize.GetBottom(30));
            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, text, pageSize.GetRight(40), pageSize.GetBottom(30), 0);
            contentByte.EndText();
        }

        private string GetFooterDate()
        {
            DateTime reportDate = DateTime.Parse(this.reportObject.ReportParms[0].ToString());

            DateTime nextMonth;
            if (reportDate.Month == 12)
            {
                nextMonth = new DateTime(reportDate.Year + 1, 1, 1);
            }
            else
            {
                nextMonth = new DateTime(reportDate.Year, reportDate.Month + 1, 1);
            }

            int currentDay = reportDate.Day;
            int daysInMonth = nextMonth.AddDays(-1).Day;

            string suffix = string.Empty;
            if (currentDay >= 11 && currentDay <= 13)
            {
                suffix = "th";
            }
            else
            {
                switch (currentDay.ToString()[currentDay.ToString().Length - 1])
                {
                    case '1':
                        suffix = "st";
                        break;
                    case '2':
                        suffix = "nd";
                        break;
                    case '3':
                        suffix = "rd";
                        break;
                    default:
                        suffix = "th";
                        break;
                }
            }

            return string.Format("{0}{1} Day of {2}", currentDay, suffix, daysInMonth);
        }

        private ReportObject.Snapshot GetSnapshotData(PotentialCategoryInfo categoryInfo)
        {
            if (reportObject.SnapshotData.Data.ContainsKey(categoryInfo.SequenceNumber))
            {
                return reportObject.SnapshotData.Data[categoryInfo.SequenceNumber];
            }

            return new ReportObject.Snapshot();
        }

        private void LoadPotentialCategories()
        {
            if (SequenceNumberIsAllowed(new double[] { 1.1, 1.2, 1.3, 1.4, 1.5 }))
            {
                AddPotentialCategory(0, "PAWN LOANS", 0);
                AddPotentialCategory(1.1, "Loans Written", 1);
                AddPotentialCategory(1.2, "Pickups / Refinance", 1);
                AddPotentialCategory(1.3, "PFI's", 1);
                AddPotentialCategory(1.4, "Extensions", 1);
                AddPotentialCategory(1.5, "Partial Payments", 1);
                AddPotentialCategory(0, string.Empty, 0);
            }

            if (SequenceNumberIsAllowed(new double[] { 2.01, 2.02, 2.03, 2.04, 2.05, 2.06, 2.07, 2.08, 2.09, 2.10, 2.11, 2.12, 2.13, 2.14, 2.15, 2.16 }))
            {
                AddPotentialCategory(0, "CASH ADVANCES", 0);
            }

            if (SequenceNumberIsAllowed(new double[] { 2.01, 2.02, 2.03, 2.04 }))
            {
                AddPotentialCategory(0, "PayDay Loan", 1);
                AddPotentialCategory(2.01, "Loans Written", 2);
                AddPotentialCategory(2.02, "Payoff's / Refinance", 2);
                AddPotentialCategory(2.03, "XPP Payments", 2);
                AddPotentialCategory(2.04, "Recoveries", 2);
                AddPotentialCategory(0, string.Empty, 0);
            }

            if (SequenceNumberIsAllowed(new double[] { 2.05, 2.06, 2.07, 2.08 }))
            {
                AddPotentialCategory(0, "OMLA Loans", 1);
                AddPotentialCategory(2.05, "Loans Written", 2);
                AddPotentialCategory(2.06, "Payoff's / Refinance", 2);
                AddPotentialCategory(2.07, "XPP Payments", 2);
                AddPotentialCategory(2.08, "Recoveries", 2);
                AddPotentialCategory(0, string.Empty, 0);
            }

            if (SequenceNumberIsAllowed(new double[] { 2.09, 2.10, 2.11, 2.12 }))
            {
                AddPotentialCategory(0, "Brokered Loans", 1);
                AddPotentialCategory(2.09, "Loans Written", 2);
                AddPotentialCategory(2.10, "Payoff's / Refinance", 2);
                AddPotentialCategory(2.11, "XPP Payments", 2);
                AddPotentialCategory(2.12, "Recoveries", 2);
                AddPotentialCategory(0, string.Empty, 0);
            }

            if (SequenceNumberIsAllowed(new double[] { 2.13, 2.14, 2.15, 2.16 }))
            {
                AddPotentialCategory(0, "Installment", 1);
                AddPotentialCategory(2.13, "Loans Written", 2);
                AddPotentialCategory(2.14, "Pay's / Refinance", 2);
                AddPotentialCategory(2.15, "Partial Payments", 2);
                AddPotentialCategory(2.16, "Recoveries", 2);
                AddPotentialCategory(0, string.Empty, 0);
            }

            if (SequenceNumberIsAllowed(new double[] { 3.1, 3.2, 3.3, 3.4 }))
            {
                AddPotentialCategory(0, "NEIGHBORHOOD FSC", 0);
                AddPotentialCategory(3.1, "Checks Cashed", 1);
                AddPotentialCategory(3.2, "Money Order Sales", 1);
                AddPotentialCategory(3.3, "Money Transfers Sent", 1);
                AddPotentialCategory(3.4, "Money Transfers Received", 1);
                AddPotentialCategory(0, string.Empty, 0);
            }

            if (SequenceNumberIsAllowed(new double[] { 4.1, 4.2, 4.3 }))
            {
                AddPotentialCategory(0, "BUYS", 0);
                AddPotentialCategory(4.1, "Customer Buys", 1);
                AddPotentialCategory(4.2, "Vendor Buys", 1);
                AddPotentialCategory(4.3, "Returns", 1);
                AddPotentialCategory(0, string.Empty, 0);
            }

            if (SequenceNumberIsAllowed(new double[] { 5.1, 5.2, 5.3, 5.4, 6.1, 6.2, 6.3, 6.4, 7.1, 7.2, 7.3, 7.4, 7.5 }))
            {
                //AddPotentialCategory(5.0, "INVENTORY", 0);
                //AddPotentialCategory(0, string.Empty, 0);

                AddPotentialCategory(0, "LAYAWAYS", 0);
                AddPotentialCategory(5.1, "New Layaways", 1);
                AddPotentialCategory(5.2, "Payments", 1);
                AddPotentialCategory(5.3, "Paid Outs", 1);
                AddPotentialCategory(5.4, "Refunds", 1);
                AddPotentialCategory(0, string.Empty, 0);

                AddPotentialCategory(0, "SALES (includes refunds and voids)", 0);
                AddPotentialCategory(0, "Net In Store", 1);
                AddPotentialCategory(6.1, "Net Sales", 2);
                AddPotentialCategory(6.2, "Net Cost of Sales", 2);
                AddPotentialCategory(0, string.Empty, 0);

                AddPotentialCategory(0, "Net Web", 1);
                AddPotentialCategory(6.3, "Net Sales", 2);
                AddPotentialCategory(6.4, "Net Cost of Sales", 2);
                AddPotentialCategory(0, string.Empty, 0);

                AddPotentialCategory(0, "STORE CREDIT", 0);
                AddPotentialCategory(0, "Layaways", 1);
                AddPotentialCategory(7.1, "New", 2);
                AddPotentialCategory(7.2, "Used", 2);
                AddPotentialCategory(7.3, "Forfeited", 2);
                AddPotentialCategory(0, string.Empty, 0);

                AddPotentialCategory(0, "Retail", 1);
                AddPotentialCategory(7.4, "New", 2);
                AddPotentialCategory(7.5, "Used", 2);
                AddPotentialCategory(0, string.Empty, 0);
            }

            AddPotentialCategory(0, "INCOME", 0);
            AddPotentialCategory(8.1, "Pawn PSC", 1);
            AddPotentialCategory(8.101, "Debit Card Fee", 1);
            AddPotentialCategory(8.102, "Insurance", 1);
            AddPotentialCategory(8.2, "Cash Advance", 1);
            AddPotentialCategory(8.3, "Check Cashing Fees", 1);
            AddPotentialCategory(8.4, "Money Order Fees", 1);
            //---
            AddPotentialCategory(8.5, "In Store Gross Profit", 1);
            AddPotentialCategory(8.6, "Web Gross Profit", 1);
            AddPotentialCategory(8.7, "Gross Profit %", 1);
            AddPotentialCategory(8.73, "Layaway Service Fee", 1);
            AddPotentialCategory(8.75, "Layaway Restocking Fee", 1);
            //---
            AddPotentialCategory(8.8, "Miscellaneous", 1);

            AddPotentialCategory(9, "Total Income", 1);
        }

        private void AddPotentialCategory(double sequenceNumber, string category, int tabs)
        {
            if (sequenceNumber == 0 || sequenceNumber == 9 || SequenceNumberIsAllowed(new double[] { sequenceNumber }))
            {
                _PotentialCategories.Add(new PotentialCategoryInfo(sequenceNumber, category, tabs));
            }
        }

        private bool SequenceNumberIsAllowed(double[] sequenceNumbers)
        {
            foreach (double sequenceNumber in sequenceNumbers)
            {
                foreach (ReportObject.SnapshotCategory category in reportObject.SnapshotData.AvailableCategories)
                {
                    if (sequenceNumber >= category.MinSequenceNumber && sequenceNumber <= category.MaxSequenceNumber)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        internal class PotentialCategoryInfo
        {
            internal PotentialCategoryInfo(double sequenceNumber, string category, int tabs)
            {
                SequenceNumber = sequenceNumber;
                Category = category;
                Tabs = tabs;
            }

            public double SequenceNumber { get; set; }
            public string Category { get; set; }
            public int Tabs { get; set; }
        }
    }
}
