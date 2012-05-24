/************************************************************************
 * Namespace:       PawnUtilities.Shared
 * Class:           GunAuditReport
 * 
 * Description      The file to create iTextSharp Gun Audit reports
 * 
 * History
 * S Murphy 1/20/2010, Initial Development
 * S Murphy 2/27/2010, issue with correct column ordering
 * PWN00000407 SMurphy 3/25/2010 - Store name & Run Date overlapped
 *  no ticket 4/13/2010 SMurphy namespace cleanup
 *  no ticket SMurphy 4/21/2010 Changes to remove iText from ReportObject per Sree 
 *          and moved common hard-coded strings to ReportConstants
 * EDW 1/30/2012 - BZ 1430 Move Operational down, so it does not overlap with the logo.         
 * **********************************************************************/

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
    enum GunReportCategories
    {
        Rifles, Shotguns, Revolvers, Pistols, OtherHandguns, Miscellaneous
    }
    enum GunReportStatus
    {
        IP, PFI, Buy, Layaway
    }
    public class GunAuditReport : PdfPageEventHelper
    {

        //used by overriden methods for footers
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        //used by report
        private Font _reportFont;
        private Font _reportFontLargeBold;
        private Font _reportFontLargeUnderline;

        //main objects
        public ReportObject reportObject;
        public RunReport runReport;

        public GunAuditReport()
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
                GunAuditReport events = new GunAuditReport();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                //set up tables, etc...
                PdfPTable table = new PdfPTable(21);
                PdfPCell cell = new PdfPCell();
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);

                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);
                _reportFontLargeUnderline = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.UNDERLINE);

                gif.ScalePercent(35);
                runReport = new RunReport();

                if (reportObject.ReportDetail.Equals("Summary"))
                {
                    document.AddTitle(reportObject.ReportTitle + " - Summary: " + DateTime.Now.ToString("MM/dd/yyyy"));

                    document.SetPageSize(PageSize.LETTER);
                    document.SetMargins(-50, -55, 10, 45);

                    SummaryReportHeader(table, gif);
                    Int32[,] gunStatus;
                    gunStatus = new Int32[6, 4];
                    SummaryReportDetail(table, out gunStatus, false);
                    SummaryReportSummary(table, gunStatus, false);

                    table.HeaderRows = 10;
                }
                else//detail version
                {
                    document.AddTitle(reportObject.ReportTitle + " - Detailed: " + DateTime.Now.ToString("MM/dd/yyyy"));

                    document.SetPageSize(PageSize.LEGAL.Rotate());
                    document.SetMargins(-100, -100, 10, 45);

                    DetailReportHeader(table, gif);
                    DetailColumnHeaders(table);
                    DetailReportDetail(table);
                    DetailReportSummary(table);//calls summary methods

                    table.HeaderRows = 7;
                }

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
        //  common
        private Int32[,] SummaryReportData()
        {
            Int32[,] gunStatus;
            gunStatus = new Int32[6, 4];

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    gunStatus[i, j] = 0;
                }
            }

            foreach (ReportObject.GunAudit gunAuditData in reportObject.GunAuditData)
            {
                int index = -1;

                //switch (gunAuditData.Category)
                //{
                //    case 4110:
                //    case 4120:
                //    case 4130:
                //        index = (int)GunReportCategories.Rifles;
                //        break;

                //    case 4200:
                //        index = (int)GunReportCategories.Shotguns; 
                //        break;

                //    case 4320:
                //        index = (int)GunReportCategories.Revolvers;
                //        break;

                //    case 4310:
                //        index = (int)GunReportCategories.Pistols;
                //        break;

                //    case 4330:
                //    case 4340:
                //        index = (int)GunReportCategories.OtherHandguns;
                //        break;

                //    default: // if in this bucket -- try looking at gun_type to set the bucket
                        if (gunAuditData.GunType.Equals("RIFLE"))
                            index = (int)GunReportCategories.Rifles;
                        else if (gunAuditData.GunType.Equals("RIFLE W/SC"))
                            index = (int)GunReportCategories.Rifles;
                        else if (gunAuditData.GunType.Equals("SHOTGUN"))
                            index = (int)GunReportCategories.Shotguns;
                        else if (gunAuditData.GunType.Equals("REVOLVER"))
                            index = (int)GunReportCategories.Revolvers;
                        else if (gunAuditData.GunType.Equals("PISTOL"))
                            index = (int)GunReportCategories.Pistols;
                        else if (gunAuditData.GunType.Equals("MISC. HAND"))
                            index = (int)GunReportCategories.OtherHandguns;
                        else if (gunAuditData.GunType.Equals("DERRINGER"))
                            index = (int)GunReportCategories.OtherHandguns;
                        else if (gunAuditData.GunType.Equals("TARGET HAN"))
                            index = (int)GunReportCategories.OtherHandguns; 
                        else
                            index = (int)GunReportCategories.Miscellaneous;
                //        break;
                //}
                switch (gunAuditData.ShopLoanStatus)
                {
                    case "IP":
                        gunStatus[index, (int)GunReportStatus.IP]++;
                        break;

                    case "PFI":
                        gunStatus[index, (int)GunReportStatus.PFI]++;
                        break;

                    case "LAY":
                        gunStatus[index, (int)GunReportStatus.Layaway]++;
                        break;

                    case "PUR":
                        gunStatus[index, (int)GunReportStatus.Buy]++;
                        break;
                }
            }

            return gunStatus;

        }
        private decimal SummaryReportTotal(Int32[,] statusArr, int status)
        {
            decimal total = 0;

            for (int i = 0; i < 6; i++)
            {
                total += statusArr[i, status];
            }
            return total;
        }
        private decimal SummaryReportGrandTotal(Int32[,] statusArr)
        {
            //do grand total
            Int32 total = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    total += statusArr[i, j];
                }
            }

            return total;
        }
        //individual report sections
        //  Summary version
        private void SummaryReportHeader(PdfPTable headingtable, Image gif)
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
            //PWN00000407 SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.Colspan = 5;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(reportObject.ReportStoreDesc, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            //PWN00000407 SMurphy 3/25/2010 - Store name & Run Date overlapped
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
            //PWN00000407 SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.PaddingTop = -15;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 5;
            headingtable.AddCell(cell);

            // heading - row 3 
            cell = new PdfPCell(new Paragraph(ReportHeaders.REPORTINGCOLON + ReportHeaders.NA, _reportFont));
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
            //PWN00000407 SMurphy 3/25/2010 - Store name & Run Date overlapped
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

            // heading - row 5
            cell = new PdfPCell(new Phrase(ReportHeaders.REPORTDETAIL + reportObject.ReportDetail, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 21;
            headingtable.AddCell(cell);

            for (int i = 0; i < 3; i++)
            {
                runReport.ReportLines(headingtable, false, StringUtilities.fillString(" ", 81), false, _reportFont);
            }

            runReport.ReportLines(headingtable, false, StringUtilities.fillString("=", 81), false, _reportFont);
        }
        private void SummaryReportDetail(PdfPTable datatable, out Int32[,] statusArr, bool isDetail)
        {

            int colSpan;
            var colon = ":";

            if (isDetail)
            {
                colSpan = 1;
                colon = string.Empty;
            }
            else
            {
                colSpan = 2;
            }

            //have to get counts by gun type & status
            statusArr = SummaryReportData();

            PdfPCell cell = new PdfPCell();
            foreach (int gunReportCategory in Enum.GetValues(typeof(GunReportCategories)))
            {
                //row 1
                runReport.ReportLines(datatable, false, " ", false, _reportFont);

                //row 2
                if (gunReportCategory == (int)GunReportCategories.OtherHandguns)
                {
                    cell = new PdfPCell(new Phrase("Other Handguns" + colon, _reportFont));
                }
                else
                {
                    cell = new PdfPCell(new Phrase(Enum.GetName(typeof(GunReportCategories), gunReportCategory) + colon, _reportFont));
                }
                if (isDetail)
                {
                    cell.Colspan = 2;
                }
                else
                {
                    cell.Colspan = 3;
                }
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                if (isDetail)
                {
                    cell = new PdfPCell(new Phrase(":", _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);
                }

                cell = new PdfPCell(new Phrase("Active Loans  ", _reportFont));
                cell.Colspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(statusArr[gunReportCategory, 0].ToString(), _reportFont));
                cell.Colspan = colSpan;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase("PFI  ", _reportFont));
                cell.Colspan = colSpan;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(statusArr[gunReportCategory, 1].ToString(), _reportFont));
                cell.Colspan = colSpan;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase("BUY  ", _reportFont));
                cell.Colspan = colSpan;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(statusArr[gunReportCategory, 2].ToString(), _reportFont));
                cell.Colspan = colSpan;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Layaway  ", _reportFont));
                cell.Colspan = colSpan;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(statusArr[gunReportCategory, 3].ToString(), _reportFont));
                cell.Colspan = colSpan;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Empty, _reportFont));
                if (isDetail)
                {
                    cell.Colspan = 9;
                }
                else
                {
                    cell.Colspan = 2;
                }
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

            }
        }
        private void SummaryReportSummary(PdfPTable datatable, Int32[,] statusArr, bool isDetail)
        {

            int colSpan = 0;
            int lineWidth = 0;

            if (isDetail)
            {
                colSpan = 1;
                lineWidth = 137;
            }
            else
            {
                colSpan = 2;
                lineWidth = 81;
            }

            PdfPCell cell = new PdfPCell();

            runReport.ReportLines(datatable, true, string.Empty, false, _reportFont);
            runReport.ReportLines(datatable, false, " ", false, _reportFont);

            //row 1 
            cell = new PdfPCell(new Phrase("Totals", _reportFont));
            if (isDetail)
            {
                cell.Colspan = 2;
            }
            else
            {
                cell.Colspan = 3;
            }
            if (!isDetail)
            {
                cell.PaddingLeft = -5;
            }
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = colSpan;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            if (isDetail)
            {
                cell = new PdfPCell(new Phrase(string.Empty, _reportFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 2;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);
            }
            //gun status totals by gun type
            foreach (int gunReportStatus in Enum.GetValues(typeof(GunReportStatus)))
            {
                cell = new PdfPCell(new Phrase(SummaryReportTotal(statusArr, gunReportStatus).ToString(), _reportFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = colSpan;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Empty, _reportFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = colSpan;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);
            }

            if (isDetail)
            {
                cell = new PdfPCell(new Phrase(string.Empty, _reportFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 8;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);
            }
            //row 2 & 3 grand total
            runReport.ReportLines(datatable, false, StringUtilities.fillString(" ", lineWidth), false, _reportFont);

            cell = new PdfPCell(new Phrase("Grand Total", _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            if (isDetail)
            {
                cell.Colspan = 2;
            }
            else
            {
                cell.Colspan = 3;
            }
            cell.Border = Rectangle.NO_BORDER;
            if (!isDetail)
            {
                cell.PaddingLeft = -5;
            }
            datatable.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = colSpan;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);
            if (isDetail)
            {
                cell = new PdfPCell(new Phrase(string.Empty, _reportFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 2;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);
            }
            cell = new PdfPCell(new Phrase(SummaryReportGrandTotal(statusArr).ToString(), _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 16;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            runReport.ReportLines(datatable, false, StringUtilities.fillString(" ", lineWidth), false, _reportFont);

            //final line 
            if (!isDetail)
            {
                runReport.ReportLines(datatable, false, StringUtilities.fillString("=", lineWidth), true, _reportFont);
            }
            else
            {
                runReport.ReportLines(datatable, true, string.Empty, true, _reportFont);
            }

        }
        //  Detail version
        private void DetailColumnHeaders(PdfPTable headingtable)
        {
            PdfPCell cell = new PdfPCell();
            //no ticket S.Murphy 2/27/2010 issue with correct column ordering
            if (reportObject.ReportSort.Equals("Status"))
            {
                cell = new PdfPCell(new Phrase("Status", _reportFont));
                cell.Colspan = 1;
                cell.PaddingLeft = 5;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                headingtable.AddCell(cell);
            }
            else
            {
                cell = new PdfPCell(new Phrase("Gun #", _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                headingtable.AddCell(cell);
            }

            cell = new PdfPCell(new Phrase("Ticket #", _reportFont));
            cell.Colspan = 1;
            cell.PaddingLeft = 15;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Original Ticket #", _reportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("ICN", _reportFont));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Category Name", _reportFont));
            cell.PaddingLeft = 15;
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Amount", _reportFont));
            cell.Colspan = 1;
            cell.PaddingLeft = 15;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Cost", _reportFont));
            cell.Colspan = 1;
            cell.PaddingLeft = 20;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            //no ticket S.Murphy 2/27/2010 issue with correct column ordering
            if (reportObject.ReportSort.Equals("Status"))
            {
                cell = new PdfPCell(new Phrase("Gun #", _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                headingtable.AddCell(cell);
            }
            else
            {
                cell = new PdfPCell(new Phrase("Status", _reportFont));
                cell.Colspan = 1;
                cell.PaddingLeft = 20;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                headingtable.AddCell(cell);
            }

            cell = new PdfPCell(new Phrase("Importer", _reportFont));
            cell.Colspan = 2;
            cell.PaddingLeft = 25;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Aisle", _reportFont));
            cell.Colspan = 1;
            cell.PaddingLeft = 15;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Shelf", _reportFont));
            cell.Colspan = 1;
            cell.PaddingLeft = 15;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Location", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Merchandise Description", _reportFont));
            cell.Colspan = 4;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            runReport.ReportLines(headingtable, false, StringUtilities.fillString("=", 137), false, _reportFont);

        }
        private void DetailReportHeader(PdfPTable headingtable, Image gif)
        {

            PdfPCell cell = new PdfPCell();

            //  heading - row 1
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            //PWN00000407 SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.Colspan = 5;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(reportObject.ReportStoreDesc, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            //PWN00000407 SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 12;
            headingtable.AddCell(cell);

            //  heading - row 2
            cell = new PdfPCell(new Paragraph(ReportHeaders.OPERATIONAL, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingLeft = 12;
            //cell.PaddingTop = -5;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Concat(reportObject.ReportTitle, ReportHeaders.REPORT), _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 14;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(ReportHeaders.RUNDATE + DateTime.Now.ToString("MM/dd/yyyy"), _reportFont));
            //PWN00000407 SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.PaddingTop = -15;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            // heading - row 3 
            cell = new PdfPCell(new Paragraph(string.Concat(ReportHeaders.REPORTINGCOLON, ReportHeaders.NA), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.PaddingLeft = 12;
            //cell.PaddingTop = -10;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(ReportHeaders.REPORTSORT + reportObject.ReportSort, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 14;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(ReportHeaders.REPORTNUM + reportObject.ReportNumber, _reportFont));
            //PWN00000407 SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.PaddingTop = -20;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            // heading - row 4
            cell = new PdfPCell(new Paragraph(string.Empty, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingLeft = 12;
            //cell.PaddingTop = -10;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase(ReportHeaders.REPORTDETAIL + reportObject.ReportDetail, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 14;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, _reportFont));
            //PWN00000407 SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.PaddingTop = -20;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            runReport.ReportLines(headingtable, false, " ", false, _reportFont);

        }
        private void DetailReportDetail(PdfPTable datatable)
        {

            PdfPCell cell = new PdfPCell();

            foreach (ReportObject.GunAudit gunAuditData in reportObject.GunAuditData)
            {
                //no ticket S.Murphy 2/27/2010 issue with correct column ordering
                if (reportObject.ReportSort.Equals("Status"))
                {
                    cell = new PdfPCell(new Phrase(gunAuditData.ShopLoanStatus.ToString(), _reportFont));
                    cell.Colspan = 1;
                    cell.PaddingRight = 20;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);
                }
                else
                {
                    cell = new PdfPCell(new Phrase(gunAuditData.GunNumber.ToString(), _reportFont));
                    cell.Colspan = 1;
                    //cell.PaddingRight = 20;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);
                }

                cell = new PdfPCell(new Phrase(gunAuditData.CurrTktNumber, _reportFont));
                cell.Colspan = 1;

                //no ticket S.Murphy 2/27/2010 issue with correct column ordering
                if (reportObject.ReportSort.Equals("Status"))
                {
                    cell.PaddingRight = 10;
                }
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(gunAuditData.OrigTktNumber, _reportFont));
                cell.Colspan = 2;
                cell.PaddingRight = 20;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(gunAuditData.ICN, _reportFont));
                cell.Colspan = 3;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(gunAuditData.GunType, _reportFont));
                cell.PaddingRight = 25;
                cell.Colspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(gunAuditData.MdseLoanAmount.ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(gunAuditData.Cost.ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                if (reportObject.ReportSort.Equals("Status"))
                {
                    cell = new PdfPCell(new Phrase(gunAuditData.GunNumber.ToString(), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);
                }
                else
                {
                    cell = new PdfPCell(new Phrase(gunAuditData.ShopLoanStatus.ToString(), _reportFont));
                    cell.Colspan = 1;
                    //cell.PaddingRight = 20;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);
                }

                cell = new PdfPCell(new Phrase(gunAuditData.GunImporter, _reportFont));
                cell.Colspan = 2;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(gunAuditData.LocationAisle.ToUpper(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(gunAuditData.Shelf.ToUpper(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(gunAuditData.Location.ToUpper(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(gunAuditData.FullMdseDescr, _reportFont));
                cell.Colspan = 4;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                runReport.ReportLines(datatable, true, string.Empty, false, _reportFont);
            }
        }
        private void DetailReportSummary(PdfPTable datatable)
        {

            PdfPCell cell = new PdfPCell();

            //row 1 
            cell = new PdfPCell(new Phrase("Summary", _reportFontLargeUnderline));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 21;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            Int32[,] gunStatus;
            gunStatus = new Int32[6, 4];
            SummaryReportDetail(datatable, out gunStatus, true);
            SummaryReportSummary(datatable, gunStatus, true);

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

            if (reportName[0] == "Gun Audit - Detailed")
            {
                text = string.Format("{0}{1} Page {2} of ", reportName[0].Replace(" - Detailed", ""), StringUtilities.fillString(" ", 395), pageN);
            }
            else
            {
                text = string.Format("{0}{1} Page {2} of ", reportName[0].Replace(" - Summary", ""), StringUtilities.fillString(" ", 205), pageN);
            }

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
