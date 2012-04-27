using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System;
using System.Data;
using System.IO;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Utilities = Common.Libraries.Utility.Utilities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;

namespace Pawn.Logic.PrintQueue
{
    public class ManualCheckDepositSlipPrint
    {
        public string MCDSlipReportTitle;
        public string reportNumber;
        private Document document;
        private string storeNumber;
        private string rptFileName;
        private Font RptFont;
        private Font HeaderTitleFont;
        private const string disclosure = "Disclosure: This report is generated from the cashlinx point of " + 
                                          "sale system and may not equal the monthly income statement due to additional journal entries";

        public bool GenerateMCDSlipDocument()
        {
            storeNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            reportNumber = "CL-OP-68";
            DataTable checkInfoDetails;
            string errorCode;
            string errorText;
            //Get the report data
            ShopProcedures.GetStoreManualCheckDepositData(GlobalDataAccessor.Instance.OracleDA,
                                                          GlobalDataAccessor.Instance.CurrentSiteId.StoreId,
                                                          ShopDateTime.Instance.ShopDate,
                                                          out checkInfoDetails, out errorCode, out errorText);
            if (checkInfoDetails == null || checkInfoDetails.Rows.Count == 0)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "No data returned from stored procedure for manual check deposit slips " + errorText);
                return false;
            }
            //Initialize fonts
            RptFont =
            FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
            this.HeaderTitleFont =
            new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 10);
            
            this.MCDSlipReportTitle =
            @"Check Cash Deposit Slip";

            document = new Document(PageSize.LETTER);
            document.AddTitle(MCDSlipReportTitle);
            PdfPTable table = new PdfPTable(8);
            rptFileName =
            SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\MCD" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rptFileName, FileMode.Create));
            MCDPageEvents events = new MCDPageEvents();
            writer.PageEvent = events;
            document.SetPageSize(PageSize.LETTER);
            document.SetMargins(0, 0, 10, 45);

            Image image = Image.GetInstance(Properties.Resources.logo, BaseColor.WHITE);
            image.ScalePercent(25);

            //Insert the report header
            InsertReportHeader(table, image);

            document.Open();
            document.Add(table);

            PdfPTable newtable = new PdfPTable(4);
            //Insert the column headers
            InsertColumnHeaders(newtable);
            table.HeaderRows = 7;
            decimal totalCheckAmount = 0;
            int totalNumChecks = 0;

            foreach (DataRow dr in checkInfoDetails.Rows)
            {
                string makerName = Common.Libraries.Utility.Utilities.GetStringValue(dr["makername"]);
                string cdName = Utilities.GetStringValue(dr["username"]);
                decimal chkAmount = Utilities.GetDecimalValue(dr["checkamount"]);
                string depositDate = Utilities.GetDateTimeValue(dr["creationdate"]).FormatDate();
                totalNumChecks++;
                totalCheckAmount += chkAmount;
                PdfPCell pCell = new PdfPCell();
                pCell.Colspan = 1;
                pCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                pCell.Border = Rectangle.NO_BORDER;
                pCell.Phrase = new Phrase(makerName, RptFont);
                newtable.AddCell(pCell);
                pCell.Phrase = new Phrase(cdName, RptFont);
                newtable.AddCell(pCell);
                pCell.Phrase = new Phrase(depositDate, RptFont);
                newtable.AddCell(pCell);
                pCell.Phrase = new Phrase(String.Format("{0:n}", chkAmount), RptFont);
                pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                newtable.AddCell(pCell);
            }

            //Add a blank line before summary
            newtable.AddCell(generateBlankLine(8));

            //Insert summary table
            PdfPCell newCell = new PdfPCell(new Paragraph("", RptFont));
            newCell.HorizontalAlignment = Element.ALIGN_LEFT;
            newCell.Border = Rectangle.TOP_BORDER;
            newCell.Colspan = 4;
            newtable.AddCell(newCell);
            newCell.Colspan = 1;
            newCell.HorizontalAlignment = Element.ALIGN_LEFT;
            newCell.Border = Rectangle.NO_BORDER;
            newCell.Phrase = new Phrase("Count of all Checks", RptFont);
            newtable.AddCell(newCell);
            newCell.Phrase = new Phrase(totalNumChecks.ToString(), RptFont);
            newtable.AddCell(newCell);
            newCell.Phrase = new Phrase("Total Amount of all Checks:", RptFont);
            newtable.AddCell(newCell);
            newCell.Phrase = new Phrase(string.Format("{0:n}", totalCheckAmount), RptFont);
            newCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            newtable.AddCell(newCell);
            newCell = new PdfPCell(new Paragraph("", RptFont));
            newCell.HorizontalAlignment = Element.ALIGN_LEFT;
            newCell.Border = Rectangle.BOTTOM_BORDER;
            newCell.Colspan = 4;
            newtable.AddCell(newCell);

            document.Add(newtable);

            //Insert report footer
            PdfPTable finalSummaryTable = new PdfPTable(8);
            finalSummaryTable.TotalWidth = 500f;
            InsertReportFooter(ref finalSummaryTable);

            float yAbsolutePosition = newtable.CalculateHeights(true);
            finalSummaryTable.WriteSelectedRows(0, -1, document.LeftMargin + 30, yAbsolutePosition + 30, writer.DirectContent);

            document.Close();

            //Print and save the document
            PrintDocument();

            return (true);
        }

        private void PrintDocument()
        {
            const string formName = "manualcheck";
            //Default print success flag
            var fLog = FileLogger.Instance;
            //Print manual check
            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
            {
                string res = PrintingUtilities.printDocument(
                    rptFileName,
                    GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                    GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port,
                    1);
                if (string.IsNullOrEmpty(res) || res.IndexOf("SUCCESS") == -1)
                {
                    if (fLog.IsLogError)
                    {
                        fLog.logMessage(LogLevel.ERROR,
                                        this,
                                        "Could not print {0} document.",
                                        MCDSlipReportTitle);
                    }
                }
                else if (res.IndexOf("SUCCESS") != -1)
                {
                    if (fLog.IsLogInfo)
                    {
                        fLog.logMessage(LogLevel.INFO,
                                        this,
                                        "Successfully printed {0} document.",
                                        MCDSlipReportTitle);
                    }
                }
            }
            else
            {
                if (fLog.IsLogWarn)
                {
                    fLog.logMessage(LogLevel.WARN, this, "Did not print lost ticket.  Printing is disabled or printer is invalid: {0}",
                                    GlobalDataAccessor.Instance.DesktopSession.LaserPrinter);
                }
            }
            //TODO: Ensure report is stored prior to deletion
            File.Delete(rptFileName);
        }

        private void InsertReportHeader(PdfPTable headingtable, Image image)
        {
            PdfPCell cell = new PdfPCell();

            cell = new PdfPCell(image);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Run Date/Time Stamp: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), RptFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Store Id: " + storeNumber, RptFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Report #: " + reportNumber, RptFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Report Type: Operational", RptFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            //Date range
            cell = new PdfPCell(new Paragraph("Date Range: " + DateTime.Now.ToShortDateString() + " to " + DateTime.Now.ToShortDateString(), RptFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            //Add a blank line after heading
            headingtable.AddCell(generateBlankLine(8));

            //report title

            cell = new PdfPCell(new Paragraph(MCDSlipReportTitle, HeaderTitleFont));
            cell.Border = Rectangle.BOX;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 8;
            headingtable.AddCell(cell);
            //Add a blank line after heading
            headingtable.AddCell(generateBlankLine(8));
        }

        private void InsertReportFooter(ref PdfPTable footertable)
        {
            PdfPCell cell = new PdfPCell();
            //Beginning line for the footer
            cell = new PdfPCell(new Paragraph("", RptFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.TOP_BORDER;
            cell.Colspan = 8;
            footertable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(MCDSlipReportTitle + "-" + reportNumber, RptFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.NoWrap = true;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 4;
            footertable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(disclosure, RptFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.NoWrap = false;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 4;
            footertable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(DateTime.Now.ToShortDateString() + " to " + DateTime.Now.ToShortDateString(), RptFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.NoWrap = true;
            cell.Colspan = 4;
            footertable.AddCell(cell);

            //Empty Line
            footertable.AddCell(generateBlankLine(8));

            //End Line
            cell = new PdfPCell(new Paragraph("", RptFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.Colspan = 8;
            footertable.AddCell(cell);

            footertable.AddCell(generateBlankLine(8));
        }

        private void InsertColumnHeaders(PdfPTable headingtable)
        {
            PdfPCell cell = new PdfPCell(new Paragraph("Check Maker", RptFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.BOX;
            cell.BorderColorLeft = BaseColor.BLACK;
            cell.BorderColorRight = BaseColor.GRAY;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Originating Teller ID", RptFont));
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Date Of Deposit", RptFont));
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Check Amount($)", RptFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 1;
            headingtable.AddCell(cell);
        }

        public static PdfPCell generateBlankLine(int colSpan)
        {
            var blankLineCell = new PdfPCell(new Phrase(" "));
            blankLineCell.Colspan = colSpan;
            blankLineCell.Border = PdfPCell.NO_BORDER;
            return (blankLineCell);
        }
    }

    class MCDPageEvents : PdfPageEventHelper
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
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Check deposit slip printing" + dex.Message);
            }
            catch (IOException ioe)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Check deposit slip printing" + ioe.Message);
            }
        }

        public override void OnEndPage(PdfWriter writer, Document doc)
        {
            base.OnEndPage(writer, doc);

            int pageN = writer.PageNumber;
            String text = "Page " + pageN + " of ";
            Rectangle pageSize = doc.PageSize;
            contentByte.SetRGBColorFill(100, 100, 100);
            float len = footerBaseFont.GetWidthPoint(text, 8);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            contentByte.SetTextMatrix(pageSize.GetLeft(270), pageSize.GetBottom(15));
            contentByte.ShowText(text);
            contentByte.EndText();
            contentByte.AddTemplate(template, pageSize.GetLeft(270) + len, pageSize.GetBottom(15));
        }

        public override void OnCloseDocument(PdfWriter writer, Document doc)
        {
            base.OnCloseDocument(writer, doc);

            template.BeginText();
            template.SetFontAndSize(footerBaseFont, 8);
            template.SetTextMatrix(0, 0);
            template.ShowText("" + (writer.PageNumber - 1));
            template.EndText();
        }
    }
}
