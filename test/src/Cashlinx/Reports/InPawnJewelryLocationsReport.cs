/************************************************************************
 * Namespace:       PawnUtilities.Shared
 * Class:           InPawnJewelryLocationsReport
 * 
 * Description      The file to create iTextSharp In Pawn Jewelry Locations reports
 * 
 * History
 * S Murphy 1/20/2010, Initial Development
 * PWN00000411 SMurphy 3/25/2010 - Store name & Run Date overlapped
 * PWNU00000577 SMurphy 3/31/2010 put in date under opertational reporting
 *  no ticket 4/13/2010 SMurphy namespace cleanup
 *  no ticket SMurphy 4/21/2010 Changes to remove iText from ReportObject per Sree 
 *          and moved common hard-coded strings to ReportConstants
**********************************************************************/

using System;
using System.Collections.Generic;
using Common.Controllers.Application;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Common.Libraries.Utility.Logger;

namespace Reports
{
    public class InPawnJewelryLocationsReport : PdfPageEventHelper
    {
        
        //used by overriden methods for footers
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        //used by report
        private Font _reportFont;
        private Font _reportFontLargeBold;

        //main objects
        public ReportObject reportObject;
        public RunReport runReport;

        public InPawnJewelryLocationsReport()
        {

        }
        //create report
        public bool CreateReport()//ReportObject rptObj)
        {
            bool isSuccessful = false;

            var document = new iTextSharp.text.Document(PageSize.LEGAL);

            try
            {
                //set up RunReport event overrides & create doc
                InPawnJewelryLocationsReport events = new InPawnJewelryLocationsReport();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                //set up tables, etc...
                PdfPTable table = new PdfPTable(21);
                PdfPCell cell = new PdfPCell();
                Image gif = Image.GetInstance(Common.Properties.Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(35);

                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);

                runReport = new RunReport(); 
                
                document.AddTitle(reportObject.ReportTitle + ": " + DateTime.Now.ToString("MM/dd/yyyy"));
                document.SetPageSize(PageSize.LETTER);
                document.SetMargins(-50, -50, 10, 45);

                ReportHeader(table, gif);
                ColumnHeaders(table);
                ReportDetail(table);

                table.HeaderRows = 10;

                document.Open(); 
                document.Add(table);
                document.Close();

                isSuccessful = true;

            }
            catch (DocumentException de)
            {
                reportObject.ReportError = de.Message; ;
                reportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            catch (IOException ioe)
            {
                reportObject.ReportError = ioe.Message; ;
                reportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }

            return isSuccessful;
        }
        //individual report sections
        private void ReportHeader(PdfPTable headingtable, Image gif)
        {

            PdfPCell cell = new PdfPCell();

            //  heading - row 1
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = -10;
            cell.Colspan = 5;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            //PWN00000411 SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.Colspan = 5;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(reportObject.ReportStoreDesc, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            //PWN00000411 SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 11;
            headingtable.AddCell(cell);

            //  heading - row 2
            cell = new PdfPCell(new Paragraph(ReportHeaders.OPERATIONAL, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            //cell.PaddingTop = -5;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 5;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 11;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(ReportHeaders.RUNDATE + DateTime.Now.ToString("MM/dd/yyyy"), _reportFont));
            //PWN00000411 SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.PaddingTop = -15;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 5;
            headingtable.AddCell(cell);

            // heading - row 3 
            //PWNU00000577 3/31/2010 SMurphy put in date under opertational reporting
            cell = new PdfPCell(new Paragraph(ReportHeaders.REPORTINGCOLON + DateTime.Now.ToString("MM/dd/yyyy"), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 5;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 11;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(ReportHeaders.REPORTNUM + reportObject.ReportNumber.ToString(), _reportFont));
            //PWN00000411 SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.PaddingTop = -8;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 5;
            headingtable.AddCell(cell);

            // heading - row 4
            cell = new PdfPCell(new Phrase(reportObject.ReportTitle, _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 21;
            headingtable.AddCell(cell);

            for (int i = 0; i < 3; i++)
            {
                cell = new PdfPCell(new Paragraph(" ", _reportFontLargeBold));
                cell.Colspan = 21;
                cell.Border = Rectangle.NO_BORDER;
                headingtable.AddCell(cell);
            }

            runReport.ReportLines(headingtable, true, string.Empty, false, _reportFont);

        }
        private void ColumnHeaders(PdfPTable headingtable)
        {
            //set up tables, etc...
            PdfPCell cell = new PdfPCell();

            runReport.ReportLines(headingtable, false, " ", false, _reportFont);

            cell = new PdfPCell(new Phrase("Location", _reportFont));
            cell.Colspan = 5;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Empty, _reportFont));
            cell.Colspan = 6;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Number of Loans", _reportFont));
            cell.Colspan = 10;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            runReport.ReportLines(headingtable, false, " ", false, _reportFont);

        }
        private void ReportDetail(PdfPTable datatable)
        {

            PdfPCell cell = new PdfPCell();

            List<Int16> listCounts = new List<Int16>();

            foreach (ReportObject.InPawnJewelry inPawnJewelry in reportObject.InPawnJewelryData)
            {
                cell = new PdfPCell(new Phrase(string.Empty, _reportFont));
                cell.Colspan = 2;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(inPawnJewelry.Location, _reportFont));
                cell.Colspan = 3;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Empty, _reportFont));
                cell.Colspan = 6;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(inPawnJewelry.ItemCount.ToString(), _reportFont));
                cell.Colspan = 10;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);
            }

            runReport.ReportLines(datatable, true, string.Empty, true, _reportFont);

        }
        
        // we override the OnOpenDocument, OnCloseDocument & OnEndPage methods to get footers
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
            string[] reportName = writer.Info.Get(PdfName.TITLE).ToString().Split(':');
            text = string.Format("{0}{1} Page {2} of ", reportName[0], StringUtilities.fillString(" ", 180), pageN);

            float len = footerBaseFont.GetWidthPoint(text, 8);

            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            contentByte.SetTextMatrix(30, 30);
            contentByte.ShowText(text);
            contentByte.EndText();
            contentByte.AddTemplate(template, 30 + len, 30);
        }
    }
}
