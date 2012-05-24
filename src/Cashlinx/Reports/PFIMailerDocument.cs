using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System;
using System.Text;
using Common.Controllers.Application;
using Common.Libraries.Objects;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Common.Libraries.Utility.Logger;

namespace Reports
{
    public class PFIMailerDocument : ReportBase
    {
        #region Fields
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        public RunReport runReport;
        #endregion

        public PFIMailerDocument(IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {

        }

        #region Private Properties

        #endregion

        #region Private Methods
        private void WriteDetail(PdfPTable detailsTable, ReportObject.PFIMailer pfiMailer)
        {
            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8, 50));
            WriteCell(detailsTable, "Dear Valued Cash America Customer,", new Font(ReportFont.BaseFont, 9), 7, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8,20));

            WriteCell(detailsTable, "Please be advised that you have until " + pfiMailer.pfiEligibleDate.ToShortDateString() + " to redeem the property you pledged under Pawn ", new Font(ReportFont.BaseFont, 9), 7, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8, 5));
           
            WriteCell(detailsTable, "Ticket No. " + pfiMailer.ticketNumber + ", otherwise it will be sold.  We hope this reminder is helpful to you.", new Font(ReportFont.BaseFont, 9), 7, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8, 20));
           
            WriteCell(detailsTable, "Thank you for doing business with Cash America Pawn.", new Font(ReportFont.BaseFont, 9), 7, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8, 10));
           
            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8, 5));
            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8, 130));
           
            WriteCell(detailsTable, "YOU MAY EXTEND THIS LOAN", new Font(ReportFont.BaseFont, 9),8, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8, 20));
           
            WriteCell(detailsTable, "EFFECTIVE AFTER JANUARY 1, 1998, A $" + pfiMailer.pfiMailerFee + " NOTIFICATION FEE WILL BE COLLECTED WHEN REDEEMING THIS LOAN.", new Font(ReportFont.BaseFont, 9), 7, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8, 100));
           
            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8, 50));

            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8, 5));
            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8, 5));

            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8, 5));
            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8, 5));

            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8, 5));
            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8, 5));

            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8, 5));
            detailsTable.AddCell(WriteBlankCellWithMinimumHeight(8, 25));

            detailsTable.AddCell(WriteBlankCell(8));
            WriteCell(detailsTable, "F", new Font(ReportFont.BaseFont, 9), 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, pfiMailer.storeName, new Font(ReportFont.BaseFont, 9), 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            detailsTable.AddCell(WriteBlankCell(1));
            detailsTable.AddCell(WriteBlankCell(2));
            detailsTable.AddCell(WriteBlankCell(8));

            detailsTable.AddCell(WriteBlankCell(8));
            WriteCell(detailsTable, "R", new Font(ReportFont.BaseFont, 9), 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, pfiMailer.storeAddress, new Font(ReportFont.BaseFont, 9), 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            detailsTable.AddCell(WriteBlankCell(1));
            detailsTable.AddCell(WriteBlankCell(2));
            detailsTable.AddCell(WriteBlankCell(8));

            detailsTable.AddCell(WriteBlankCell(8));
            WriteCell(detailsTable, "O", new Font(ReportFont.BaseFont, 9), 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, pfiMailer.storeCity + ", " + pfiMailer.storeState + " " + pfiMailer.storeZipCode, new Font(ReportFont.BaseFont, 9), 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            detailsTable.AddCell(WriteBlankCell(1));
            detailsTable.AddCell(WriteBlankCell(2));
            detailsTable.AddCell(WriteBlankCell(8));

            detailsTable.AddCell(WriteBlankCell(8));
            WriteCell(detailsTable, "M", new Font(ReportFont.BaseFont, 9), 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, pfiMailer.storePhone, new Font(ReportFont.BaseFont, 9), 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            detailsTable.AddCell(WriteBlankCell(1));
            detailsTable.AddCell(WriteBlankCell(2));
            detailsTable.AddCell(WriteBlankCell(8));

            detailsTable.AddCell(WriteBlankCell(8));
            detailsTable.AddCell(WriteBlankCell(1));
            detailsTable.AddCell(WriteBlankCell(3));
            WriteCell(detailsTable, "T", new Font(ReportFont.BaseFont, 9), 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, pfiMailer.customerName, new Font(ReportFont.BaseFont, 9), 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            detailsTable.AddCell(WriteBlankCell(1));
            detailsTable.AddCell(WriteBlankCell(8));

            detailsTable.AddCell(WriteBlankCell(8));
            detailsTable.AddCell(WriteBlankCell(1));
            detailsTable.AddCell(WriteBlankCell(3));
            WriteCell(detailsTable, "O", new Font(ReportFont.BaseFont, 9), 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, pfiMailer.customerAddress, new Font(ReportFont.BaseFont, 9), 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            detailsTable.AddCell(WriteBlankCell(1));
            detailsTable.AddCell(WriteBlankCell(8));

            detailsTable.AddCell(WriteBlankCell(8));
            detailsTable.AddCell(WriteBlankCell(1));
            detailsTable.AddCell(WriteBlankCell(3));
            detailsTable.AddCell(WriteBlankCell(1));
            WriteCell(detailsTable, pfiMailer.customerCity + ", " + pfiMailer.customerState + " " + pfiMailer.customerZipCode, new Font(ReportFont.BaseFont, 9), 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            detailsTable.AddCell(WriteBlankCell(1));
            detailsTable.AddCell(WriteBlankCell(8));
        }

        private void ReportHeader(PdfPTable headingtable, int colspan)
        {
            PdfPCell cell = new PdfPCell();

            headingtable.AddCell(WriteBlankCell(colspan));
            headingtable.AddCell(WriteBlankCell(colspan));
            headingtable.AddCell(WriteBlankCell(colspan));
            WriteCell(headingtable, "Date Mailed: " + ShopDateTime.Instance.FullShopDateTime.ToShortDateString(), new Font(ReportFont.BaseFont, 9), 8, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);
            headingtable.AddCell(WriteBlankCell(colspan));
            headingtable.AddCell(WriteBlankCell(colspan));
        }

        #endregion

        #region Public Properties
        public ReportObject ReportObject { get; set; }

        #endregion


        public bool CreateReport(ReportObject.PFIMailer pfiMailer)
        {
            bool isSuccessful = false;
            var document = new iTextSharp.text.Document(PageSize.LETTER);
            try
            {

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ReportObject.ReportTempFileFullName, FileMode.Create));
                PFIMailerDocumentPageEvents events = new PFIMailerDocumentPageEvents();
                writer.PageEvent = events;

                //MultiColumnText columns = new MultiColumnText(document.PageSize.Top - 100, document.PageSize.Height - (50));
                //columns.AddSimpleColumn(-150, document.PageSize.Width + 76);

                //set up tables, etc...
                PdfPTable table = new PdfPTable(8);
                table.WidthPercentage = 100;// document.PageSize.Width;

                runReport = new RunReport();
                //document.SetPageSize(PageSize.LETTER);
                document.SetMargins(50, 50, 30, 45);
                document.Open();

                ReportHeader(table, 8);
                //here add detail
                WriteDetail(table, pfiMailer);

                document.Add(table);
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
    }

    class PFIMailerDocumentPageEvents : PdfPageEventHelper
    {
        BaseFont footerBaseFont = null;
        PdfContentByte contentByte;
        PdfTemplate template;
        public override void OnOpenDocument(PdfWriter writer, Document doc)
        {
            try
            {
                footerBaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                contentByte = writer.DirectContent;
                template = contentByte.CreateTemplate(50, 50);

            }
            catch (DocumentException dex)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "PFI Mailer Document Printing" + dex.Message);
            }
            catch (IOException ioe)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "PFI Mailer Document Printing" + ioe.Message);
            }
        }

        public override void OnEndPage(PdfWriter writer, Document doc)
        {
        }

        public override void OnCloseDocument(PdfWriter writer, Document doc)
        {
            template.BeginText();
            template.SetFontAndSize(footerBaseFont, 8);
            template.ShowText((writer.PageNumber - 1).ToString());
            template.EndText();
        }
    }
}
