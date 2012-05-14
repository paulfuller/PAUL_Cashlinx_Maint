using System;
using System.IO;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;

namespace Reports
{
    public class IndianaLostTicketAffidavit
    {
        private Font rptFont;
        private Font rptFontBold;
        private Font rptFontHeader;
        private Document document;

        public LostTicketAffidavitContext Context { get; private set; }

        public IndianaLostTicketAffidavit(LostTicketAffidavitContext context)
        {
            Context = context;
        }

        public bool Print()
        {
            try
            {
                rptFont = FontFactory.GetFont("Arial", 8, Font.NORMAL);
                rptFontBold = FontFactory.GetFont("Arial", 8, Font.BOLD);
                rptFontHeader = FontFactory.GetFont("Arial", 10, Font.BOLD);

                if (string.IsNullOrWhiteSpace(Context.OutputPath))
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Indiana Lost Ticket Affidavit printing failed since file name is not set");
                    return false;
                }

                document = new Document(PageSize.HALFLETTER.Rotate());
                document.AddTitle("Lost Ticket Affidavit");
                var writer = PdfWriter.GetInstance(document, new FileStream(Context.OutputPath, FileMode.Create));

                const int mainTableColumns = 2;
                var mainTable = new PdfPTable(mainTableColumns)
                {
                    WidthPercentage = 100
                };
                mainTable.SetWidths(new float[] { .5F, 10 });

                mainTable.AddCell(new PdfPCell(new Paragraph("Lost Ticket Affidavit", rptFontHeader)) { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = mainTableColumns });

                mainTable.AddCell(new PdfPCell() { Border = Rectangle.NO_BORDER });
                mainTable.AddCell(new PdfPCell(new Paragraph("I, " + Context.CustomerName + ", hereby affirm as follows:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE});
                mainTable.AddCell(new PdfPCell(new Phrase("1.", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                mainTable.AddCell(new PdfPCell(new Paragraph("I am the pledgor of a pawn ticket issued by", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                mainTable.AddCell(new PdfPCell() { Border = Rectangle.NO_BORDER });
                mainTable.AddCell(new PdfPCell(new Paragraph(Context.StoreName + " " + Context.StoreNumber + ", IN", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                mainTable.AddCell(new PdfPCell() { Border = Rectangle.NO_BORDER });
                mainTable.AddCell(new PdfPCell(new Paragraph("number " + Context.TicketNumber + " and dated " + Context.LoanDateMade.FormatDate(), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                mainTable.AddCell(new PdfPCell(new Phrase("2.", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                mainTable.AddCell(new PdfPCell(new Paragraph("The above-described pawn ticket has not been sold, negotiated or transferred in any other manner by me.", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                mainTable.AddCell(new PdfPCell(new Phrase("3.", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                mainTable.AddCell(new PdfPCell(new Paragraph("The above-described pawn ticket was " + Context.ReasonMissing + " as follows:", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                mainTable.AddCell(new PdfPCell() { Border = Rectangle.NO_BORDER });
                mainTable.AddCell(new PdfPCell(new Paragraph(string.Empty, rptFont)) { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 15});
                mainTable.AddCell(new PdfPCell(new Phrase("4.", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                mainTable.AddCell(new PdfPCell(new Paragraph("The pledge represented by this pawn ticket is", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, FixedHeight = 20 });
                mainTable.AddCell(new PdfPCell() { Border = Rectangle.NO_BORDER });
                mainTable.AddCell(new PdfPCell(new Paragraph(Context.MerchandiseDescription, rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, MinimumHeight = 50 });
                mainTable.AddCell(new PdfPCell(new Phrase("5.", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                mainTable.AddCell(new PdfPCell(new Paragraph("I affirm, under the penalties for perjury, that the foregoing representations are true.", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });
                mainTable.AddCell(new PdfPCell(new Paragraph("", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT });

                var signatureTable = new PdfPTable(2)
                {
                    WidthPercentage = 100
                };

                signatureTable.SetWidths(new float[] { 1, 1 });

                signatureTable.AddCell(new PdfPCell() { Border = Rectangle.NO_BORDER });
                signatureTable.AddCell(new PdfPCell(new Phrase(string.Empty)) { Border = Rectangle.BOTTOM_BORDER, FixedHeight = 30});
                signatureTable.AddCell(new PdfPCell() { Border = Rectangle.NO_BORDER });
                signatureTable.AddCell(new PdfPCell(new Phrase("Affiant", rptFont)) { Border = Rectangle.NO_BORDER });

                mainTable.AddCell(new PdfPCell(signatureTable) { Colspan = 2, Border = Rectangle.NO_BORDER });

                document.Open();
                document.Add(mainTable);
                document.Close();
            }
            catch (Exception de)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Indiana Lost Ticket Affidavit printing" + de.Message);
                return false;
            }

            return true;
        }
    }
}
