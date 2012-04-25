/************************************************************************
 * Namespace:       PawnReports.Reports.DetailInventoryReport
 * Class:           DetailInventoryReport
 * 
 * History
 *  Madhu fix for defexct PWNU00001411
 *  12/2/10 Tracy added support for layaway and retail sales
  ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
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
    public class DetailInventoryReport : PdfPageEventHelper
    {
        //used by overriden methods for footers
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        //used by report
        private Font _reportFont;
        private Font _reportFontLargeBold;
        private Font _reportFontUnderlined;

        //main objects
        public ReportObject reportObject;
        public RunReport runReport;
        private int totalHolds = 0;
        private decimal totalAmtHolds = 0.0m;
        private int totalBuy = 0;
        private decimal totalAmtBuy = 0.0m;
        private int totalLayaway = 0;
        private decimal totalAmtLayaway = 0.0m;
        private int totalQtySellableInventory = 0;
        private decimal totalAmtSellableInventory = 0.0m;

        public DetailInventoryReport()
        {

        }

        public bool CreateReport()
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.LEGAL);
            try
            {
                //set up RunReport event overrides & create doc
                DetailInventoryReport events = new DetailInventoryReport();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                //set up tables, etc...
                var table = new PdfPTable(21);
                PdfPCell cell = new PdfPCell();
                var gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);

                _reportFont = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD);
                _reportFontUnderlined = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.UNDERLINE);

                gif.ScalePercent(35);
                runReport = new RunReport();

                document.SetPageSize(PageSize.LEGAL.Rotate());
                document.SetMargins(-100, -100, 10, 45);
                document.AddTitle(string.Format("{0}: {1}", reportObject.ReportTitle, DateTime.Now.ToString("MM/dd/yyyy")));

                ReportHeader(table, gif);
                ColumnHeaders(table);
                ReportDetail(table);
                ReportSummary(table);

                table.HeaderRows = 11;
                document.Open();
                document.Add(table);
                document.Close();

                isSuccessful = true;

            }
            catch (DocumentException de)
            {
                reportObject.ReportError = de.Message;
                ;
                reportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            catch (IOException ioe)
            {
                reportObject.ReportError = ioe.Message;
                ;
                reportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            return isSuccessful;
        }

        private void ReportSummary(PdfPTable summaryTable)
        {

            int totalInventory = 0;
            decimal totalAmtInventory = 0.0m;

            totalInventory = totalQtySellableInventory + totalBuy + totalHolds + totalLayaway;
            totalAmtInventory = totalAmtSellableInventory + totalAmtBuy + totalAmtHolds + totalAmtLayaway;
            PdfPCell cell = new PdfPCell();
            cell = new PdfPCell(new Paragraph("Summary", _reportFontLargeBold));
            //cell = new PdfPCell(new Phrase("Summary", _reportFontUnderlined));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            runReport.ReportLines(summaryTable, false, " ", false, _reportFont);

            //sellable Inventory
            cell = new PdfPCell(new Paragraph("Sellable Inventory", _reportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("" + totalQtySellableInventory, _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("$" + totalAmtSellableInventory, _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            runReport.ReportLines(summaryTable, false, " ", false, _reportFont);
            //BUY
            cell = new PdfPCell(new Paragraph("Buy", _reportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("" + totalBuy, _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("$" + totalAmtBuy, _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            // 12/2/2010 Tracy
            runReport.ReportLines(summaryTable, false, " ", false, _reportFont);
            //LAYAWAY
            cell = new PdfPCell(new Paragraph("Layaway", _reportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("" + totalLayaway, _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("$" + totalAmtLayaway, _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);
            //---------------

            runReport.ReportLines(summaryTable, false, " ", false, _reportFont);
            //HOLDS
            cell = new PdfPCell(new Paragraph("Holds", _reportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("" + totalHolds, _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("$" + totalAmtHolds, _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            runReport.ReportLines(summaryTable, false, " ", false, _reportFont);
            runReport.ReportLines(summaryTable, false, " ", false, _reportFont);
            //GrandTotal
            cell = new PdfPCell(new Paragraph("Total Inventory", _reportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("" + totalInventory, _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("$" + totalAmtInventory, _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);
            runReport.ReportLines(summaryTable, false, " ", false, _reportFont);
            runReport.ReportLines(summaryTable, true, "", false, _reportFont);
        }

        private void ColumnHeaders(PdfPTable headingtable)
        {
            //set up tables, etc...
            PdfPCell cell = new PdfPCell();
            //row 1
            cell = new PdfPCell(new Phrase("Status", _reportFontLargeBold));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("ICN", _reportFontLargeBold));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);


            cell = new PdfPCell(new Phrase("Merchandise Description", _reportFontLargeBold));
            cell.Colspan = 8;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Quantity", _reportFontLargeBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Cost", _reportFontLargeBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Aisle", _reportFontLargeBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Shelf", _reportFontLargeBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Other", _reportFontLargeBold));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Gun Number", _reportFontLargeBold));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("ICN Xref", _reportFontLargeBold));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);
            //no ticket 4/8/2010 SMurphy center justified data on aisle/shelf/location and other formatting changes
            //runReport.ReportLines(headingtable, false, String.StringUtilities.fillString("-", 240), false, _reportFont);
            runReport.ReportLines(headingtable, false, StringUtilities.fillString("-", 240), false, _reportFont);
        }

        private void AddDetailLine(ReportObject.DetailInventoryLines inventoryLine, PdfPTable datatable)
        {
            PdfPCell cell = null;
            string statusCD = string.Empty;
            if (inventoryLine.status_cd == "PUR")
                statusCD = "BUY";
            else
                statusCD = inventoryLine.status_cd;
            if (string.IsNullOrEmpty(inventoryLine.hold_desc))
            {
                cell = new PdfPCell(new Phrase(statusCD, _reportFont));
            }
            else
            {
                cell = new PdfPCell(new Phrase(statusCD + "-" + inventoryLine.hold_desc, _reportFont));
            }
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            cell = new PdfPCell(new Phrase(inventoryLine.ICN, _reportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);


            cell = new PdfPCell(new Phrase(inventoryLine.md_desc, _reportFont));
            cell.Colspan = 8;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            cell = new PdfPCell(new Phrase(inventoryLine.quantity.ToString(), _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            cell = new PdfPCell(new Phrase("$" + inventoryLine.item_amt, _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            if (inventoryLine.status_cd == "PFI")
                cell = new PdfPCell(new Phrase(string.Empty, _reportFont));
            else
                cell = new PdfPCell(new Phrase(inventoryLine.loc_aisle, _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            if (inventoryLine.status_cd == "PFI")
                cell = new PdfPCell(new Phrase(string.Empty, _reportFont));
            else
                cell = new PdfPCell(new Phrase(inventoryLine.loc_shelf, _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            if (inventoryLine.status_cd == "PFI")
                cell = new PdfPCell(new Phrase(string.Empty, _reportFont));
            else
                cell = new PdfPCell(new Phrase(inventoryLine.location, _reportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            if (inventoryLine.gun_number != "0")
                cell = new PdfPCell(new Phrase(inventoryLine.gun_number, _reportFont));
            else
                cell = new PdfPCell(new Phrase(string.Empty, _reportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);


            cell = new PdfPCell(new Phrase(inventoryLine.xicn, _reportFont));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);
        }

        private void ReportDetail(PdfPTable datatable)
        {
            IEnumerable<ReportObject.DetailInventoryLines> iEnumInventoryLinesPFI = reportObject.DetailInventoryData.InventoryData.Where(p => p.status_cd == "PFI" || p.status_cd == "LREC" || p.status_cd == "HPFI" || p.status_cd == "OS");
            int rowCount = iEnumInventoryLinesPFI.Count();
            if (rowCount > 0)
            {
                foreach (ReportObject.DetailInventoryLines inventoryLine in iEnumInventoryLinesPFI)
                {
                    if (!string.IsNullOrEmpty(inventoryLine.hold_desc))
                    {
                        totalAmtHolds += inventoryLine.item_amt;
                        totalHolds += 1; // inventoryLine.quantity;
                    }
                    else
                    {
                        totalQtySellableInventory += 1; // inventoryLine.quantity;
                        totalAmtSellableInventory += inventoryLine.item_amt;
                    }
                    AddDetailLine(inventoryLine, datatable);
                }
                runReport.ReportLines(datatable, true, "", false, _reportFont);
            }
            IEnumerable<ReportObject.DetailInventoryLines> iEnumInventoryLinesBUY = reportObject.DetailInventoryData.InventoryData.Where(p => p.status_cd == "PUR" || p.status_cd == "HPUR");
            if (iEnumInventoryLinesBUY.Count() > 0)
            {
                foreach (ReportObject.DetailInventoryLines inventoryLine in iEnumInventoryLinesBUY)
                {
                    if (!string.IsNullOrEmpty(inventoryLine.hold_desc))
                    {
                        totalAmtHolds += inventoryLine.item_amt;
                        totalHolds += 1; // inventoryLine.quantity;
                    }
                    else
                    {
                        totalBuy += 1; // inventoryLine.quantity;
                        totalAmtBuy += inventoryLine.item_amt;
                    }
                    AddDetailLine(inventoryLine, datatable);
                }
                runReport.ReportLines(datatable, true, "", false, _reportFont);
            }

            // 12/2/10 Tracy
            IEnumerable<ReportObject.DetailInventoryLines> iEnumInventoryLinesLAY = reportObject.DetailInventoryData.InventoryData.Where(p => p.status_cd == "LAY" || p.status_cd == "HLAY");
            if (iEnumInventoryLinesLAY.Count() > 0)
            {
                foreach (ReportObject.DetailInventoryLines inventoryLine in iEnumInventoryLinesLAY)
                {
                    if (!string.IsNullOrEmpty(inventoryLine.hold_desc))
                    {
                        totalAmtHolds += inventoryLine.item_amt;
                        totalHolds += 1; // inventoryLine.quantity;
                    }
                    else
                    {
                        totalLayaway += 1; // inventoryLine.quantity;
                        totalAmtLayaway += inventoryLine.item_amt;
                    }

                    AddDetailLine(inventoryLine, datatable);
                }
                runReport.ReportLines(datatable, true, "", false, _reportFont);
            }

            IEnumerable<ReportObject.DetailInventoryLines> iEnumInventoryLinesHOLD = reportObject.DetailInventoryData.InventoryData.Where(p => p.status_cd == "HOLD");
            if (iEnumInventoryLinesHOLD.Count() > 0)
            {
                foreach (ReportObject.DetailInventoryLines inventoryLine in iEnumInventoryLinesHOLD)
                {
                    AddDetailLine(inventoryLine, datatable);
                }
                runReport.ReportLines(datatable, true, "", false, _reportFont);
            }


            //-----------------
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
            cell = new PdfPCell(new Paragraph(string.Empty, _reportFont));
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

            cell = new PdfPCell(new Paragraph(ReportHeaders.LOCATION + reportObject.MerchandiseLocation, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 21;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(ReportHeaders.REPORTDETAIL + reportObject.ReportDetail, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 21;
            headingtable.AddCell(cell);

            for (int i = 0; i < 3; i++)
            {
                runReport.ReportLines(headingtable, false, " ", false, _reportFont);
            }
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
            //Madhu fix for defexct PWNU00001411
            contentByte.SetTextMatrix(470, 30);
            contentByte.ShowText(text);
            contentByte.EndText();
            //Madhu fix for defexct PWNU00001411
            contentByte.AddTemplate(template, 470 + len, 30);
        }
    }
}
