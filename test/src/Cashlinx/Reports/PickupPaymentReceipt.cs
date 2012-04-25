using System;
using System.IO;
using Common.Libraries.Objects;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Application;
using Common.Libraries.Utility;
using Utilities = Common.Libraries.Utility.Utilities;

namespace Reports
{
    public class PickupPaymentReceipt : ReportBase
    {
        private Font rptFont;
        private Font rptFontBold;
        private Font rptFontHeader;
        private Document document;

        public PickupPaymentContext Context { get; private set; }

        public PickupPaymentReceipt(PickupPaymentContext context, IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {
            Context = context;
            OtherFees = 0;
            PfiMailerFee = 0;
            TicketTotal = 0;
        }

        public decimal OtherFees { get; private set; }
        public decimal PfiMailerFee { get; private set; }
        public decimal TicketTotal { get; private set; }

        public bool Print()
        {
            try
            {
                rptFont = FontFactory.GetFont("Arial", 8, Font.NORMAL);
                rptFontBold = FontFactory.GetFont("Arial", 8, Font.BOLD);
                rptFontHeader = FontFactory.GetFont("Arial", 10, Font.BOLD);

                if (string.IsNullOrWhiteSpace(Context.OutputPath))
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Pickup Payment Receipt printing failed since file name is not set");
                    return false;
                }

                document = new Document(PageSize.HALFLETTER.Rotate());
                document.AddTitle("Pickup Payment Receipt");
                var writer = PdfWriter.GetInstance(document, new FileStream(Context.OutputPath, FileMode.Create));

                const int mainTableColumns = 1;
                var mainTable = new PdfPTable(mainTableColumns)
                {
                    WidthPercentage = 100
                };

                mainTable.AddCell(new PdfPCell(new Paragraph("Pickup Payment Receipt", rptFontHeader)) { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = mainTableColumns });

                var topDataTable = new PdfPTable(5)
                {
                    WidthPercentage = 100
                };

                var ticket = Context.Ticket;

                PfiMailerFee = ticket.PfiMailerSent ? new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetPFIMailerFee(
                    GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId) : 0M;

                topDataTable.SetWidths(new[] { 1f, 1f, 8f, 1f, 1f });
                topDataTable.AddCell(new PdfPCell(new Paragraph("Date:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                topDataTable.AddCell(new PdfPCell(new Paragraph(Context.Time.FormatDate(), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                topDataTable.AddCell(new PdfPCell(new Paragraph(string.Format("{0} # {1}", Context.StoreName, Context.StoreNumber), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, NoWrap = true });
                topDataTable.AddCell(new PdfPCell(new Paragraph("Ticket No:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                topDataTable.AddCell(new PdfPCell(new Paragraph(ticket.TicketNumber.ToString(), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                topDataTable.AddCell(new PdfPCell(new Paragraph("Time:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                topDataTable.AddCell(new PdfPCell(new Paragraph(Context.Time.ToString("t"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                topDataTable.AddCell(new PdfPCell(new Paragraph(Context.StoreAddress, rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, NoWrap = true });
                topDataTable.AddCell(new PdfPCell(new Paragraph("Emp No:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                topDataTable.AddCell(new PdfPCell(new Paragraph(Context.EmployeeNumber, rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                mainTable.AddCell(new PdfPCell(topDataTable) { Colspan = mainTableColumns, Border = Rectangle.NO_BORDER });
                mainTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = mainTableColumns, FixedHeight = 10 }); // empty row

                var ticketTable = new PdfPTable(4)
                    {
                        WidthPercentage = 100
                    };

                ticketTable.SetWidths(new[] { 1f, 6f, 2f, 1f });
                ticketTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                ticketTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                ticketTable.AddCell(new PdfPCell(new Paragraph("Current Principal Amount:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                ticketTable.AddCell(new PdfPCell(new Paragraph(ticket.CurrentPrincipalAmount.ToString("c"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                ticketTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                ticketTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                ticketTable.AddCell(new PdfPCell(new Paragraph("Interest:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                ticketTable.AddCell(new PdfPCell(new Paragraph(ticket.Interest.ToString("c"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                ticketTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                ticketTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                ticketTable.AddCell(new PdfPCell(new Paragraph("Processing Fee:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                ticketTable.AddCell(new PdfPCell(new Paragraph(ticket.ServiceFee.ToString("c"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                OtherFees = PfiMailerFee + ticket.LateFee + ticket.LostTicketFee;

                ticketTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                ticketTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                ticketTable.AddCell(new PdfPCell(new Paragraph("Other:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                ticketTable.AddCell(new PdfPCell(new Paragraph(OtherFees.ToString("c"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                TicketTotal = ticket.CurrentPrincipalAmount + ticket.Interest + ticket.ServiceFee + OtherFees;

                ticketTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                ticketTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                ticketTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                ticketTable.AddCell(new PdfPCell(new Paragraph("------------", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                ticketTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                ticketTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                ticketTable.AddCell(new PdfPCell(new Paragraph("Total Payment Amount:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
                ticketTable.AddCell(new PdfPCell(new Paragraph(TicketTotal.ToString("c"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

                ticketTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = 4 });

                mainTable.AddCell(new PdfPCell(ticketTable) { Colspan = mainTableColumns, Border = Rectangle.NO_BORDER });
                mainTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = mainTableColumns, FixedHeight = 10 }); // empty row

                var bottomTable = new PdfPTable(3)
                {
                    WidthPercentage = 100
                };

                bottomTable.SetWidths(new[] { 7f, 2f, 1f });
                bottomTable.AddCell(new PdfPCell(new Paragraph(Context.CustomerName, rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                bottomTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                bottomTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });

                bottomTable.AddCell(new PdfPCell(new Paragraph(Context.CustomerAddress, rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                bottomTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                bottomTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });

                bottomTable.AddCell(new PdfPCell(new Paragraph(Context.CustomerCityStateZip, rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                bottomTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                bottomTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });

                bottomTable.AddCell(new PdfPCell(new Paragraph(Context.CustomerPhone, rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                bottomTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });
                bottomTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER });

                mainTable.AddCell(new PdfPCell(bottomTable) { Colspan = mainTableColumns, Border = Rectangle.NO_BORDER });
                mainTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = mainTableColumns, FixedHeight = 10 }); // empty row

                document.Open();
                document.Add(mainTable);
                document.Close();
            }
            catch (Exception de)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Pickup Payment Receipt printing" + de.Message);
                return false;
            }

            return true;
        }

        public class PickupPaymentContext
        {
            public string CustomerName { get; set; }
            public string CustomerAddress { get; set; }
            public string CustomerCityStateZip { get; set; }
            public string CustomerPhone { get; set; }
            public string EmployeeNumber { get; set; }
            public string OutputPath { get; set; }
            public string StoreAddress { get; set; }
            public string StoreName { get; set; }
            public string StoreNumber { get; set; }
            public DateTime Time { get; set; }
            public PickupPaymentTicketInfo Ticket { get; set; }
        }

        public class PickupPaymentTicketInfo
        {
            public decimal CurrentPrincipalAmount { get; set; }
            public decimal Interest { get; set; }
            public decimal LateFee { get; set; }
            public decimal LostTicketFee { get; set; }
            public bool PfiMailerSent { get; set; }
            public decimal ServiceFee { get; set; }
            public int TicketNumber { get; set; }
        }
    }
}
