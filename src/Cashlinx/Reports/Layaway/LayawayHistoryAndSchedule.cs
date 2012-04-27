using System;
using System.Collections.Generic;
using System.IO;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Utility.String;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Common.Libraries.Utility.Logger;

namespace Reports.Layaway
{
    public class LayawayHistoryAndSchedule : PdfPageEventHelper
    {
        #region Fields
        //used by overriden methods for footers
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        //used by report
        private Font _reportFont;
        private Font _reportFontLargeBold;
        private Font _reportFontSmallBold;
        //main objects
        public LayawayReportObject reportObject;
        public LayawayRunReports runReport;
        #endregion

        #region Constructors
        public LayawayHistoryAndSchedule()
        {
        }
        #endregion

      

        #region Private Methods
        private void WriteSchedule(PdfPTable detailsTable)
        {
            if (reportObject.LayawayHistoryAndScheduleMainData.LayawayScheduleList == null)
                return;
            int lineCount = 0;
            int scheduleCount = reportObject.LayawayHistoryAndScheduleMainData.LayawayScheduleList.Count;
            foreach (LayawayReportObject.LayawaySchedule schedule in reportObject.LayawayHistoryAndScheduleMainData.LayawayScheduleList)
            {
                lineCount++;
                WriteScheduleDetails(schedule.LayawayScheduleDetailsList, detailsTable);
                if (lineCount < scheduleCount)
                    DrawLine(detailsTable, _reportFontLargeBold);
            }
        }

        private void WriteScheduleDetails(List<LayawayReportObject.LayawayScheduleDetails> layawayScheduleDetailsList, PdfPTable detailsTable)
        {
            foreach (LayawayReportObject.LayawayScheduleDetails scheduleDetail in layawayScheduleDetailsList)
            {
                PdfPCell cell = new PdfPCell();
                //row 1
                if (scheduleDetail.PaymentDateDue.ToShortDateString() == DateTime.MinValue.ToShortDateString())
                    cell = new PdfPCell(new Phrase(string.Empty, _reportFont));
                else
                    cell = new PdfPCell(new Phrase(scheduleDetail.PaymentDateDue.ToShortDateString(), _reportFont));

                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                detailsTable.AddCell(cell);

                if (scheduleDetail.PaymentAmountDue > 0.0m)
                    cell = new PdfPCell(new Phrase(scheduleDetail.PaymentAmountDue.ToString("C"), _reportFont));
                else
                    cell = new PdfPCell(new Phrase(string.Empty, _reportFont));

                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                detailsTable.AddCell(cell);

                if (scheduleDetail.PaymentMadeOn.ToShortDateString() == DateTime.MinValue.ToShortDateString())
                    cell = new PdfPCell(new Phrase(string.Empty, _reportFont));
                else
                    cell = new PdfPCell(new Phrase(scheduleDetail.PaymentMadeOn.ToShortDateString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                detailsTable.AddCell(cell);

                if (scheduleDetail.PaymentAmountMade > 0.0m)
                    cell = new PdfPCell(new Phrase(scheduleDetail.PaymentAmountMade.ToString("C"), _reportFont));
                else
                    cell = new PdfPCell(new Phrase(string.Empty, _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                detailsTable.AddCell(cell);

                if (scheduleDetail.BalanceDue > 0.0m)
                    cell = new PdfPCell(new Phrase(scheduleDetail.BalanceDue.ToString("C"), _reportFont));
                else
                    cell = new PdfPCell(new Phrase(string.Empty, _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                detailsTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(scheduleDetail.ReceiptNumber, _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                detailsTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(scheduleDetail.ReferenceNumber, _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                detailsTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(scheduleDetail.Status, _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                detailsTable.AddCell(cell);
            }
        }

        private void DrawLine(PdfPTable pdfTable, Font _lineFont)
        {
            PdfPCell cell = new PdfPCell();
            cell = new PdfPCell(new Paragraph(StringUtilities.fillString("_", 125), _lineFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 8;
            pdfTable.AddCell(cell); 
        }

        private void ReportTitleAndOtherInfo(PdfPTable pdfTable)
        {
            PdfPCell cell = new PdfPCell();
            cell = new PdfPCell(new Paragraph(reportObject.ReportTitle, _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 8;
            pdfTable.AddCell(cell);

            AddLines(2, pdfTable);

            cell = new PdfPCell(new Paragraph("Layaway #:", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(reportObject.CurrentLayaway.TicketNumber.ToString(), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Customer:", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(reportObject.CustomerName, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 4;
            pdfTable.AddCell(cell);

            //next row
            cell = new PdfPCell(new Paragraph("Paid To Date:", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(reportObject.LayawayHistoryAndScheduleMainData.Layaway.GetAmountPaid().ToString("C"), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Amount Outstanding:", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            decimal outstanding = reportObject.LayawayHistoryAndScheduleMainData.AmountOutstanding;
            cell = new PdfPCell(new Paragraph(outstanding.ToString("C"), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Total Layaway: ", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph((reportObject.LayawayHistoryAndScheduleMainData.Layaway.Amount + reportObject.LayawayHistoryAndScheduleMainData.Layaway.SalesTaxAmount).ToString("C"), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 2;
            pdfTable.AddCell(cell);
        }

        private void ColumnHeaders(PdfPTable headingtable)
        {
            //set up tables, etc...
            PdfPCell cell = new PdfPCell();
            //row 1

            cell = new PdfPCell(new Phrase("Payment Date Due", _reportFontSmallBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Payment Amount Due", _reportFontSmallBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Payment Made On", _reportFontSmallBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Payment Amount Made", _reportFontSmallBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Balance Due", _reportFontSmallBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Receipt #", _reportFontSmallBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Reference #", _reportFontSmallBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Status", _reportFontSmallBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);
        }

        private void ReportHeader(PdfPTable headingtable, Image gif)
        {
            PdfPCell cell = new PdfPCell();

            //  heading - row 1
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 8;
            headingtable.AddCell(cell);

            //  heading - row 2
            //date:
            cell = new PdfPCell(new Paragraph(string.Empty, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 6;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Date: ", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            //no ticket SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(DateTime.Now.ToString("MM/dd/yyyy"), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            //  heading - row 3
            //emp#
            cell = new PdfPCell(new Paragraph(string.Empty, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingLeft = 12;
            cell.Colspan = 6;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Emp #: ", _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(reportObject.ReportEmployee, _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            //  heading - row 4
            //empty row
            cell = new PdfPCell(new Paragraph(string.Empty, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.PaddingLeft = 12;
            cell.Colspan = 8;
            headingtable.AddCell(cell);

            //  heading - row 5
            cell = new PdfPCell(new Paragraph(reportObject.ReportStore, _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 8;
            headingtable.AddCell(cell);

            //  heading - row 6
            //Address1
            cell = new PdfPCell(new Paragraph(reportObject.ReportStoreDesc1, _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 8;
            headingtable.AddCell(cell);

            //  heading - row 7
            //Address 2
            cell = new PdfPCell(new Paragraph(reportObject.ReportStoreDesc2, _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 8;
            headingtable.AddCell(cell); 
        }

        private void AddLines(int numLines, PdfPTable table)
        {
            for (int i = 0; i < numLines; i++)
            {
                runReport.ReportLayawayHistoryLines(table, false, " ", false, _reportFont);
            }
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
                LayawayHistoryAndSchedule events = new LayawayHistoryAndSchedule();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                //set up tables, etc...
                PdfPTable table = new PdfPTable(8);
                Image gif = Image.GetInstance(Common.Properties.Resources.logo, BaseColor.WHITE);

                _reportFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD);
                _reportFontSmallBold = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD);
                gif.ScalePercent(25);
                runReport = new LayawayRunReports();

                document.SetPageSize(PageSize.LETTER);
                document.SetMargins(-50, -50, 10, 45);
                document.AddTitle(reportObject.ReportTitle + ": " + DateTime.Now.ToString("MM/dd/yyyy"));

                ReportHeader(table, gif);
                AddLines(1, table);
                ReportTitleAndOtherInfo(table);
                AddLines(1, table);
                ColumnHeaders(table);
                DrawLine(table, _reportFontLargeBold);
                //ReportDetail(table);
                //ReportSummary(table);
                WriteSchedule(table);
                table.HeaderRows = 11;
                document.Open();
                document.Add(table);
                document.Close();
                //OpenFile();
                //CreateReport();
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
        #endregion

        #region Public Overrides
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

        public override void OnCloseDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            template.BeginText();
            template.SetFontAndSize(footerBaseFont, 8);
            template.ShowText((writer.PageNumber - 1).ToString());
            template.EndText();
        }

        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            int pageN = writer.PageNumber;
            string text = string.Empty;
            text = " Page " + pageN + " of ";

            float len = footerBaseFont.GetWidthPoint(text, 8);

            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            //Madhu fix for defexct PWNU00001411
            contentByte.SetTextMatrix(260, 30);
            contentByte.ShowText(text);
            contentByte.EndText();
            //Madhu fix for defexct PWNU00001411
            contentByte.AddTemplate(template, 260 + len, 30);
        }
        #endregion

    }
}
