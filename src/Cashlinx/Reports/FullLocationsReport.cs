/**********************************************************************
 * Namespace:       PawnUtilities.Shared
 * Class:           FullLocationsReport
 * 
 * Description      The file to create iTextSharp Full Locations reports
 * 
 * History
 *    S Murphy 1/20/2010, Initial Development
 *    no ticket 2/24/2010 S. Murphy added End of Report line
 *    no ticket SMurphy 3/25/2010 - Store name & Run Date overlapped
 *    no ticket 4/8/2010 SMurphy center justified data on aisle/shelf/location and other formatting changes
 *    no ticket 4/13/2010 SMurphy namespace cleanup
 *    no ticket 4/13/2010 SMurphy moved the item amount field to the description line - not ticket summary line
 *    PWNU00000753 5/21/2010 SMurphy not showing loan amounts on multi-item loans
 *  no ticket SMurphy 4/21/2010 Changes to remove iText from ReportObject per Sree 
 *          and moved common hard-coded strings to ReportConstants
 ***********************************************************************/

using System;
using Common.Controllers.Application;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Common.Libraries.Utility.Logger;

namespace Reports
{

    public class FullLocationsReport : PdfPageEventHelper
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

        public FullLocationsReport()
        {

        }
        //create report
        public bool CreateReport()//ReportObject rptObj)
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.LEGAL);
            try
            {
                //set up RunReport event overrides & create doc
                FullLocationsReport events = new FullLocationsReport();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                //set up tables, etc...
                PdfPTable table = new PdfPTable(21);
                PdfPCell cell = new PdfPCell();
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);

                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);

                gif.ScalePercent(35);
                runReport = new RunReport();

                document.SetPageSize(PageSize.LEGAL.Rotate());
                document.SetMargins(-100, -100, 10, 45);
                document.AddTitle(reportObject.ReportTitle + ": " + DateTime.Now.ToString("MM/dd/yyyy"));

                ReportHeader(table, gif);
                ColumnHeaders(table);
                ReportDetail(table);

                table.HeaderRows = 11;
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

        private void ReportHeader(PdfPTable headingtable, Image gif)
        {

            PdfPCell cell = new PdfPCell();

            //  heading - row 1
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 13;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(reportObject.ReportStoreDesc, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            //no ticket SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 8;
            headingtable.AddCell(cell);

            //  heading - row 2
            cell = new PdfPCell(new Paragraph(ReportHeaders.OPERATIONAL, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingLeft = 12;
            //cell.PaddingTop = -5;
            cell.Colspan = 18;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(ReportHeaders.RUNDATE + DateTime.Now.ToString("MM/dd/yyyy"), _reportFont));
            //no ticket SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.PaddingTop = -15;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            // heading - row 3 
            cell = new PdfPCell(new Paragraph(ReportHeaders.REPORTING + reportObject.ReportParms[(int)ReportEnums.STARTDATE], _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.PaddingLeft = 12;
            cell.Colspan = 18;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(ReportHeaders.REPORTNUM + reportObject.ReportNumber, _reportFont));
            //no ticket SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.PaddingTop = -10;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            // heading - row 4
            cell = new PdfPCell(new Phrase(reportObject.ReportTitle + ReportHeaders.REPORT, _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 21;
            headingtable.AddCell(cell);

            for (int i = 0; i < 3; i++)
            {
                runReport.ReportLines(headingtable, false, " ", false, _reportFont);
            }

        }
        private void ColumnHeaders(PdfPTable headingtable)
        {

            runReport.ReportLines(headingtable, false, StringUtilities.fillString("-", 240), false, _reportFont);

            //set up tables, etc...
            PdfPCell cell = new PdfPCell();
            //row 1
            cell = new PdfPCell(new Phrase("Tran", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Description", _reportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase(StringUtilities.fillString("-", 52) + " Ticket " + StringUtilities.fillString("-", 52), _reportFont));
            cell.Colspan = 9;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Customer", _reportFont));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Amount", _reportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Emp #", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("----------------Location----------------", _reportFont));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);
            //row 2
            cell = new PdfPCell(new Phrase("Type", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("", _reportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Original", _reportFont));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Previous", _reportFont));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Current", _reportFont));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Name", _reportFont));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("", _reportFont));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Aisle", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Shelf", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Location", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);
            //no ticket 4/8/2010 SMurphy center justified data on aisle/shelf/location and other formatting changes
            //runReport.ReportLines(headingtable, false, String.StringUtilities.fillString("-", 240), false, _reportFont);

        }
        private void ReportDetail(PdfPTable datatable)
        {

            PdfPCell cell = new PdfPCell();
            string holdTicketsNumber = string.Empty;

            foreach (ReportObject.FullLocations fullLocations in reportObject.FullLocationsData)
            {
                //no ticket 4/8/2010 SMurphy center justified data on aisle/shelf/location and other formatting changes
                if (holdTicketsNumber != fullLocations.OriginalTicketNumber.ToString() + fullLocations.PreviousTicketNumber.ToString() +
                                    fullLocations.CurrentTicketNumber.ToString())
                {
                    holdTicketsNumber = fullLocations.OriginalTicketNumber.ToString() + fullLocations.PreviousTicketNumber.ToString() +
                                    fullLocations.CurrentTicketNumber.ToString();

                    runReport.ReportLines(datatable, true, "", false, _reportFont);

                    //row 1
                    cell = new PdfPCell(new Phrase(fullLocations.TransactionType.ToString(), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", _reportFont));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);            
                    
                    cell = new PdfPCell(new Phrase(fullLocations.OriginalTicketNumber, _reportFont));
                    cell.Colspan = 2;
                    cell.PaddingRight = 10;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);


                    cell = new PdfPCell(new Phrase(fullLocations.PreviousTicketNumber, _reportFont));
                    cell.Colspan = 3;
                    cell.PaddingRight = 10;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(fullLocations.CurrentTicketNumber, _reportFont));
                    cell.Colspan = 3;
                    cell.PaddingRight = 10;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", _reportFont));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(fullLocations.LastName + ", " + fullLocations.FirstName, _reportFont));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", _reportFont));
                    cell.Colspan = 2;
                    cell.PaddingRight = 30;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(fullLocations.EmployeeID, _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", _reportFont));
                    cell.Colspan = 3;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    //moved the location info to the next line
                }

                //row 2
                switch (fullLocations.RecordType)
                {
                    case "Jewelry":
                        cell = new PdfPCell(new Phrase("CLASS:        " + fullLocations.Class ?? "", _reportFont));
                        cell.Colspan = 3;
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        cell = new PdfPCell(new Phrase("Loan Purchase Range:", _reportFont));
                        cell.Colspan = 3;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        if (fullLocations.PKValue != "")
                        {
                            cell = new PdfPCell(new Phrase("$" + fullLocations.PKValue, _reportFont));
                        }
                        else
                        {
                            cell = new PdfPCell(new Phrase("", _reportFont));
                        }
                        cell.Colspan = 2;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        if (fullLocations.PKValue != "")
                        {
                            cell = new PdfPCell(new Phrase("$" + fullLocations.PKValue, _reportFont));
                        }
                        else
                        {
                            cell = new PdfPCell(new Phrase("", _reportFont));
                        }
                        cell.Colspan = 2;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        cell = new PdfPCell(new Phrase("PKR Value:", _reportFont));
                        cell.Colspan = 3;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        if (fullLocations.ProKnowRetailValue != "")
                        {
                            cell = new PdfPCell(new Phrase("$" + fullLocations.ProKnowRetailValue, _reportFont));
                        }
                        else
                        {
                            cell = new PdfPCell(new Phrase("", _reportFont));
                        }
                        cell.Colspan = 2;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        cell = new PdfPCell(new Phrase("", _reportFont));
                        cell.Colspan = 6;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        cell = new PdfPCell(new Phrase(fullLocations.FullMerchandiseDescription, _reportFont));
                        cell.Colspan = 15;
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        cell = new PdfPCell(new Phrase("$" + fullLocations.TransactionAmount, _reportFont));
                        cell.Colspan = 2;
                        cell.PaddingRight = 30;
                        cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        cell = new PdfPCell(new Phrase("", _reportFont));
                        cell.Colspan = 1;
                        cell.PaddingRight = 30;
                        cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        cell = new PdfPCell(new Phrase(fullLocations.LocationAisle, _reportFont));
                        cell.Colspan = 1;
                        //SMurphy center justified data
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);
                        //SMurphy center justified data
                        cell = new PdfPCell(new Phrase(fullLocations.LocationShelf, _reportFont));
                        cell.Colspan = 1;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);
                        //SMurphy center justified data
                        cell = new PdfPCell(new Phrase(fullLocations.Location, _reportFont));
                        cell.Colspan = 1;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        break;

                    case "Gun":
                        cell = new PdfPCell(new Phrase("IMPORTER:        " + fullLocations.Importer, _reportFont));
                        cell.Colspan = 18;
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        cell = new PdfPCell(new Phrase("", _reportFont));
                        cell.Colspan = 3;
                        //SMurphy center justified data
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        cell = new PdfPCell(new Phrase(fullLocations.FullMerchandiseDescription, _reportFont));
                        cell.Colspan = 15;
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        cell = new PdfPCell(new Phrase("$" + fullLocations.TransactionAmount, _reportFont));
                        cell.Colspan = 2;
                        cell.PaddingRight = 30;
                        cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        cell = new PdfPCell(new Phrase("", _reportFont));
                        cell.Colspan = 1;
                        cell.PaddingRight = 30;
                        cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);
                        
                        cell = new PdfPCell(new Phrase(fullLocations.LocationAisle, _reportFont));
                        cell.Colspan = 1;
                        //SMurphy center justified data
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);
                        //SMurphy center justified data
                        cell = new PdfPCell(new Phrase(fullLocations.LocationShelf, _reportFont));
                        cell.Colspan = 1;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);
                        //SMurphy center justified data
                        cell = new PdfPCell(new Phrase(fullLocations.Location, _reportFont));
                        cell.Colspan = 1;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);
                        break;

                    case "Other":
                        cell = new PdfPCell(new Phrase(fullLocations.FullMerchandiseDescription, _reportFont));
                        cell.Colspan = 15;
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        cell = new PdfPCell(new Phrase("$" + fullLocations.TransactionAmount, _reportFont));
                        cell.Colspan = 2;
                        cell.PaddingRight = 30;
                        cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        cell = new PdfPCell(new Phrase("", _reportFont));
                        cell.Colspan = 1;
                        cell.PaddingRight = 30;
                        cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        cell = new PdfPCell(new Phrase(fullLocations.LocationAisle, _reportFont));
                        cell.Colspan = 1;
                        //SMurphy center justified data
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);
                        //SMurphy center justified data
                        cell = new PdfPCell(new Phrase(fullLocations.LocationShelf, _reportFont));
                        cell.Colspan = 1;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);
                        //SMurphy center justified data
                        cell = new PdfPCell(new Phrase(fullLocations.Location, _reportFont));
                        cell.Colspan = 1;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);
                        break;
                }
            }

            runReport.ReportLines(datatable, false, "  ", false, _reportFont);
            //no ticket 2/24/2010 S. Murphy added End of Report line
            runReport.ReportLines(datatable, false, "", true, _reportFont);
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
