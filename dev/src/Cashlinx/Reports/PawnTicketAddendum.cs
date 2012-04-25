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
    public class PawnTicketAddendumDocument : ReportBase
    {
        #region Fields
        //private PdfContentByte contentByte;
        //private PdfTemplate template;
        //private BaseFont footerBaseFont = null;
        public RunReport runReport;
        private int _pageCount = 1;
        #endregion

        public PawnTicketAddendumDocument(IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {

        }

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

        #endregion

        #region Private Methods
        private void WriteDetail(PdfPTable detailsTable, ReportObject.PawnTicketAddendum pawnTicketAddendum)
        {
            WriteCell(detailsTable, "TRANS:# " + pawnTicketAddendum.ticketNumber, ReportFontMedium, 7, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, "CUSTOMER NAME: " + pawnTicketAddendum.customerLastName + ", " + pawnTicketAddendum.customerSuffix + ", " + pawnTicketAddendum.customerFirstName + ", " + pawnTicketAddendum.customerMiddleInitial, ReportFontMedium, 4, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, "CUSTOMER INITIALS:____________", ReportFontMedium, 4, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);

            WriteCell(detailsTable, String.Empty, ReportFontMedium, 8, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);

            WriteCell(detailsTable, String.Empty, ReportFontMedium, 8, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, "LOAN DATE: ", ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, pawnTicketAddendum.dateMade.ToShortDateString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, "DUE DATE: ", ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, pawnTicketAddendum.dueDate.ToShortDateString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, "PFI DATE: ", ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, pawnTicketAddendum.pfiEligibleDate.ToShortDateString(), ReportFontMedium, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, String.Empty, ReportFontMedium, 8, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);

            WriteCell(detailsTable, String.Empty, ReportFontMedium, 8, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, "LOAN AMOUNT: ", ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, pawnTicketAddendum.loanAmount.ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, "INTEREST: ", ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, pawnTicketAddendum.pawnFinanceCharge.ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, "NUM ITEMS: ", ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, pawnTicketAddendum.numberOfItems.ToString(), ReportFontMedium, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, String.Empty, ReportFontMedium, 8, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);

            WriteCell(detailsTable, String.Empty, ReportFontMedium, 8, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, "Description of Pledged Goods and Serial No.(s), If visible (Continued From Loan Document):", ReportFontMedium, 7, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, String.Empty, ReportFontMedium, 8, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);

            WriteCell(detailsTable, String.Empty, ReportFontMedium, 8, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            StringBuilder mdseDescription = new StringBuilder();
            foreach (string str in pawnTicketAddendum.merchandiseDescription)
            {
                mdseDescription.Append(str);
            }
            WriteCell(detailsTable, mdseDescription.ToString(), ReportFontMedium, 8, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, String.Empty, ReportFontMedium, 8, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);

            WriteCell(detailsTable, String.Empty, ReportFontMedium, 8, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            WriteCell(detailsTable, String.Empty, ReportFontMedium, 8, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);

            WriteCell(detailsTable, "END OF ADDENDUM", ReportFontHeading, 8, Element.ALIGN_CENTER, Rectangle.NO_BORDER);

        }

        private void ReportHeader(PdfPTable headingtable, int colspan)
        {
            PdfPCell cell = new PdfPCell();

            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(headingtable, "ADDENDUM", ReportFontHeading, 8, Element.ALIGN_CENTER, Rectangle.NO_BORDER);

            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
        }

        #endregion

        #region Public Properties
        public ReportObject ReportObject { get; set; }

        #endregion


        public bool CreateReport(ReportObject.PawnTicketAddendum pawnTicketAddendum)
        {
            bool isSuccessful = false;
            var document = new iTextSharp.text.Document(PageSize.LETTER);
            try
            {
                //set up RunReport event overrides & create doc
                _pageCount = 1;
                //PawnTicketAddendumDocument events = this;

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ReportObject.ReportTempFileFullName, FileMode.Create));
                AddndmPageEvents events = new AddndmPageEvents();
                writer.PageEvent = events;



                //MultiColumnText columns = new MultiColumnText(document.PageSize.Top - 100, document.PageSize.Height - (50));
                //columns.AddSimpleColumn(-150, document.PageSize.Width + 76);

                //set up tables, etc...
                PdfPTable table = new PdfPTable(8);
                table.WidthPercentage = 85;// document.PageSize.Width;

                runReport = new RunReport();
                document.Open();
                document.SetPageSize(PageSize.LETTER);
                document.SetMargins(-100, -100, 10, 45);

                ReportHeader(table, 8);
                //here add detail
                WriteDetail(table, pawnTicketAddendum);

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

    class AddndmPageEvents : PdfPageEventHelper
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
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Addendum printing" + dex.Message);
            }
            catch (IOException ioe)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Addendum printing" + ioe.Message);
            }
        }

        public override void OnEndPage(PdfWriter writer, Document doc)
        {
            int pageN = writer.PageNumber;
            String text = "Page " + pageN + " of ";
            float len = footerBaseFont.GetWidthPoint(text, 8);

            Rectangle pageSize = doc.PageSize;
            contentByte.SetRGBColorFill(0, 0, 0);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            contentByte.SetTextMatrix(pageSize.GetLeft(500), pageSize.GetTop(30));
            contentByte.ShowText(text + (writer.PageNumber).ToString());
            contentByte.EndText();

            contentByte.AddTemplate(template, pageSize.GetLeft(500) + len, pageSize.GetBottom(30));
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
