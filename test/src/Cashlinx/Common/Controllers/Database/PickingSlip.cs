using System;
using System.Collections.Generic;
using System.IO;
using Common.Controllers.Application;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using Common.Libraries.Utility.Logger;

namespace Common.Controllers.Database
{
    public class PickingSlip : PdfPageEventHelper
    {
        private const int COLUMNS = 6;

        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;

        private Font ReportFont { get; set; }
        private Font ReportFontBold { get; set; }
        private PickingSlipReportContext ReportContext { get; set; }

        private ReportObject ReportObject
        {
            get { return ReportContext.ReportObject; }
        }

        public PickingSlip(PickingSlipReportContext reportContext)
        {
            ReportContext = reportContext;
        }

        public bool CreateReport()
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.HALFLETTER.Rotate());

            try
            {
                string reportFileName = Path.GetFullPath(ReportObject.ReportTempFileFullName);
                if (!Directory.Exists(Path.GetDirectoryName(reportFileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(reportFileName));
                }
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportFileName, FileMode.Create));
                writer.PageEvent = this;

                ReportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
                ReportFontBold = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD);
                footerBaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                document.SetMargins(-30, -30, 10, 45);
                document.Open();
                foreach (PickingSlipReportService service in ReportContext.Service)
                {
                    PrintPickingSlip(service, document, writer);
                }

                document.Close();

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

        private void PrintPickingSlip(PickingSlipReportService service, Document document, PdfWriter writer)
        {
            PdfPTable table = new PdfPTable(COLUMNS);
            table.HeaderRows = 7;

            PdfPCell cell;

            string reportTitle = "** LOAN PFI PICKING SLIP **";
            if (service.IsPurchase)
            {
                reportTitle = "** PURCHASE PFI PICKING SLIP **";
            }
            cell = new PdfPCell(new Phrase(reportTitle, ReportFontBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.PaddingTop = 10f;
            cell.PaddingBottom = 10f;
            cell.Colspan = COLUMNS;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(ReportContext.RunDate.ToString("d"), ReportFont));
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.BorderWidth = 2f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.PaddingTop = 10f;
            cell.PaddingBottom = 10f;
            cell.Colspan = 4;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Org. #:", ReportFont));
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.BorderWidth = 2f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.PaddingTop = 10f;
            cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(service.OrgNumber.ToString(), ReportFont));
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.BorderWidth = 2f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.PaddingTop = 10f;
            cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            table.AddCell(cell);

            if (service.IsPurchase)
            {
                PrintSummaryRow("Customer:", service.Customer, string.Empty, string.Empty, "Purchase Amt:", service.LoanAmount.ToString("c"), table);
                PrintSummaryRow(string.Empty, string.Empty, "PFI Elig:", service.PfiEligible.ToShortDateString(), string.Empty, string.Empty, table);
                PrintSummaryRow(string.Empty, string.Empty, "Purchase #:", service.CurrentLoanNumber.ToString(), string.Empty, string.Empty, table);
            }
            else
            {
                if (service.PartialPayments)
                    PrintSummaryRow("Customer:", service.Customer, "Loan Amt:", service.LoanAmount.ToString("c"), "Current Principal:", service.CurrentLoanAmount.ToString("c"), table);
                else
                    PrintSummaryRow("Customer:", service.Customer, string.Empty, string.Empty, "Loan Amt:", service.LoanAmount.ToString("c"), table);

                PrintSummaryRow("Date Due:", service.DateDue.ToShortDateString(), "PFI Elig:", service.PfiEligible.ToShortDateString(), "Finance:", service.Finance.ToString("c"), table);
                PrintSummaryRow("Previous Loan #:", service.PreviousLoanNumber.ToString(), "Current Loan #:", service.CurrentLoanNumber.ToString(), "Service:", service.Service.ToString("c"), table);
            }
            PrintSummaryRow(string.Empty, string.Empty, string.Empty, string.Empty, "Total:", service.Total.ToString("c"), table);

            cell = new PdfPCell(new Phrase("Description", ReportFontBold));
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            cell.BorderWidth = 2f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.PaddingTop = 10f;
            cell.PaddingBottom = 10f;
            cell.Colspan = 4;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Location", ReportFontBold));
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            cell.BorderWidth = 2f;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.PaddingTop = 10f;
            cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Item Amount", ReportFontBold));
            cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            cell.BorderWidth = 2f;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.PaddingTop = 10f;
            cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            table.AddCell(cell);

            foreach (PickingSlipReportItem item in service.Items)
            {
                cell = new PdfPCell(new Phrase(item.GetItemDescription(), ReportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(item.GetLocation(), ReportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 1;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(item.Amount.ToString("c"), ReportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Colspan = 1;
                table.AddCell(cell);
            }

            document.Add(table);

            string text = "EMP#: ___________________      Date#:___________________";

            float len = footerBaseFont.GetWidthPoint(text, 8);

            Rectangle pageSize = document.PageSize;

            contentByte = writer.DirectContent;
            template = contentByte.CreateTemplate(50, 50);

            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            contentByte.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(30));
            contentByte.ShowText(text);
            contentByte.EndText();

            contentByte.AddTemplate(template, pageSize.GetLeft(40) + len, pageSize.GetBottom(30));
            document.NewPage();
        }

        private void PrintSummaryRow(string leftLabel, string leftValue, string centerLabel, string centerValue, string rightLabel, string rightValue, PdfPTable table)
        {
            PdfPCell cell;
            cell = new PdfPCell(new Phrase(leftLabel, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(leftValue, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(centerLabel, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(centerValue, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(rightLabel, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(rightValue, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 1;
            table.AddCell(cell);
        }
    }

    public class PickingSlipReportContext
    {
        public PickingSlipReportContext()
        {
            Service = new List<PickingSlipReportService>();
        }

        public DateTime RunDate { get; set; }
        public List<PickingSlipReportService> Service { get; private set; }
        public ReportObject ReportObject { get; set; }
    }

    public class PickingSlipReportService
    {
        public PickingSlipReportService()
        {
            Items = new List<PickingSlipReportItem>();
        }

        public int CurrentLoanNumber { get; set; }
        public string Customer { get; set; }
        public DateTime DateDue { get; set; }
        public decimal Finance { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal CurrentLoanAmount { get; set; }
        public bool PartialPayments { get; set; }
        public int OrgNumber { get; set; }
        public DateTime PfiEligible { get; set; }
        public int PreviousLoanNumber { get; set; }
        public decimal Service { get; set; }
        public bool IsPurchase { get; set; }
        public decimal Total
        {
            get
            {
                if (PartialPayments)
                    return CurrentLoanAmount + Finance + Service;
                return LoanAmount + Finance + Service;
            }
        }

        public List<PickingSlipReportItem> Items { get; private set; }
    }

    public class PickingSlipReportItem
    {
        public decimal Amount { get; set; }
        public string ItemDescription { get; set; }
        public string ItemLocation { get; set; }
        public string ItemLocationAisle { get; set; }
        public string ItemLocationShelf { get; set; }
        public int ItemNumber { get; set; }

        public string GetLocation()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(ItemLocation))
            {
                sb.Append("Other:");
                sb.AppendLine(ItemLocation);
            }

            if (!string.IsNullOrEmpty(ItemLocationAisle))
            {
                sb.Append("Aisle:");
                sb.AppendLine(ItemLocationAisle);
            }

            if (!string.IsNullOrEmpty(ItemLocationShelf))
            {
                sb.Append("Shelf:");
                sb.AppendLine(ItemLocationShelf);
            }

            return sb.ToString();
        }

        public string GetItemDescription()
        {
            return string.Format("[{0}] {1}", ItemNumber, ItemDescription);
        }
    }
}
