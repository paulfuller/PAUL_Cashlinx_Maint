/********************************************************************************
* Namespace: PawnReports.Reports
* FileName: ExtensionMemo
* Prints the extension memo when an extension is complete using itextsharp
* Sreelatha Rengarajan 4/28/2010 Initial version
* no ticket SMurphy changed reference for ReportsObject
* *********************************************************************************/

using System;
using System.IO;
using Common.Libraries.Objects;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;
using ReportObject = Common.Controllers.Application.ReportObject;
using Utilities = Common.Libraries.Utility.Utilities;

namespace Reports
{
    public class ExtensionMemo : AbstractExtensionMemo
    {
 
        public ReportObject RptObject;
        public string Employee;
        public System.Collections.Generic.List<ExtensionMemoInfo> ExtensionMemoData;
        public bool PrintMultipleInOneMemo;

        //Private variables
        private Font rptFont;
        private decimal totDailyAmt = 0.0M;
        private decimal totAmtPaidToday = 0.0M;
        private Document document;
        private int numOfLoans = 0;
        private decimal totalAmtDueAtMaturity = 0;

        public ExtensionMemo(IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {

        }
     
        public override bool Print()
        {

            try
            {

                //Set Font for the report
                rptFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);

                string rptFileName = RptObject.ReportTempFileFullName;
                if (!string.IsNullOrEmpty(rptFileName))
                {
                    document = new Document(PageSize.LETTER, 20, 20, 10, 200);
                    document.AddTitle(RptObject.ReportTitle);
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rptFileName, FileMode.Create));
                    ExtnPageEvents events = new ExtnPageEvents();
                    writer.PageEvent = events;
                    PdfPTable tableData = new PdfPTable(9);
                    tableData.TotalWidth = 300f;
                    //Insert the report header
                    InsertReportHeader(tableData);

                    //Create Column headers
                    InsertColumnHeaders(tableData);

                    //Set number of header rows
                    tableData.HeaderRows = 6;

                    document.Open();

                    //Insert report data
                    var pledgorName = string.Empty;
                    foreach (var extnInfo in ExtensionMemoData)
                    {
                        pledgorName = extnInfo.CustomerName;
                        PdfPCell pCell = new PdfPCell();
                        pCell.Colspan = 1;
                        pCell.NoWrap = true;
                        pCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        pCell.Border = Rectangle.NO_BORDER;
                        pCell.Phrase = new Phrase(Utilities.GetStringValue(extnInfo.TicketNumber), rptFont);
                        tableData.AddCell(pCell);

                        pCell.Phrase = new Phrase(String.Format("{0:C}", extnInfo.Amount), rptFont);
                        pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        pCell.Colspan = 1;
                        tableData.AddCell(pCell);

                        pCell.Phrase = new Phrase(extnInfo.DueDate.FormatDate(), rptFont);
                        pCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        tableData.AddCell(pCell);

                        pCell.Phrase = new Phrase(string.Format("{0:C}", extnInfo.DailyAmount), rptFont);
                        pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        pCell.Colspan = 1;
                        tableData.AddCell(pCell);

                        pCell.Phrase = new Phrase(extnInfo.NewDueDate.FormatDate(), rptFont);
                        pCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        pCell.Colspan = 1;
                        tableData.AddCell(pCell);

                        decimal pawnChargeMaturity = extnInfo.InterestAmount;
                        decimal totalAtMaturity = (extnInfo.Amount + pawnChargeMaturity);
                        pCell.Phrase = new Phrase(string.Format("{0:C}", totalAtMaturity), rptFont);
                        pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        tableData.AddCell(pCell);

                        pCell.Phrase = new Phrase(extnInfo.NewPfiEligible.FormatDate(), rptFont);
                        pCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        pCell.NoWrap = true;
                        tableData.AddCell(pCell);

                        decimal pawnChargeRedeem = (Utilities.GetDecimalValue((extnInfo.NewPfiEligible - extnInfo.NewDueDate).TotalDays) * extnInfo.DailyAmount) + totalAtMaturity;
                        pCell.Phrase = new Phrase(string.Format("{0:C}", pawnChargeRedeem), rptFont);
                        pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        pCell.Colspan = 1;
                        tableData.AddCell(pCell);

                        pCell.Phrase = new Phrase(string.Format("{0:C}", extnInfo.ExtensionAmount), rptFont);
                        pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        pCell.Colspan = 1;
                        tableData.AddCell(pCell);

                        if (PrintMultipleInOneMemo)
                        {
                            totDailyAmt += extnInfo.DailyAmount;
                            totAmtPaidToday += extnInfo.ExtensionAmount;
                            totalAmtDueAtMaturity += totalAtMaturity;
                            numOfLoans++;
                        }
                        else
                        {
                            totDailyAmt = extnInfo.DailyAmount;
                            totAmtPaidToday = extnInfo.ExtensionAmount;
                            totalAmtDueAtMaturity = totalAtMaturity;
                            numOfLoans = 1;
                            AddEndOfReportData(tableData, pledgorName, writer);
                            tableData.DeleteBodyRows();
                            document.NewPage();

                        }

                        AddDocument(extnInfo, rptFileName);
                    }
                    if (PrintMultipleInOneMemo)
                    {
                        AddEndOfReportData(tableData, pledgorName, writer);
                    }

                    document.Close();

                    return true;


                }
                else
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Extension memo printing failed since file name is not set");
                    return false;
                }


            }
            catch (Exception de)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Extension memo printing" + de.Message);
                return false;


            }

        }

        private void AddEndOfReportData(PdfPTable table,string pledgorName,PdfWriter writer)
        {
            //Insert summary for all the loans
            InsertSummaryData(table, numOfLoans.ToString());
            

            //Insert the end of report data
            //PdfPTable finalSummaryTable = new PdfPTable(9);
            //finalSummaryTable.TotalWidth = 500f;

            PdfPCell cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 9;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 9;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 9;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 9;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell);

            InsertFinalSummaryData(table, pledgorName);
            document.Add(table);
            //float yAbsolutePosition = table.CalculateHeights(true);
            //finalSummaryTable.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.Height - yAbsolutePosition - 70, writer.DirectContent);

        }

        private void InsertSummaryData(PdfPTable summaryTable, string total)
        {

            PdfPCell cell = new PdfPCell(new Paragraph(total + " Loan(s)", rptFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.TOP_BORDER;
            cell.Colspan = 3;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Format("{0:C}", totDailyAmt), rptFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.TOP_BORDER;
            cell.Colspan = 1;
            cell.NoWrap = true;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Format("{0:C}", totAmtPaidToday), rptFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.TOP_BORDER;
            cell.Colspan = 5;
            cell.NoWrap = true;
            summaryTable.AddCell(cell);


        }


        private void InsertFinalSummaryData(PdfPTable summaryTable,string custName)
        {
            //Line 1
            PdfPCell cell = new PdfPCell(new Paragraph("Total Amt Due At Maturity (for all loans listed):  " + string.Format("{0:C}",totalAmtDueAtMaturity), rptFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 9;
            summaryTable.AddCell(cell);

            //3 empty lines
            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 9;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 9;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 9;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            summaryTable.AddCell(cell);

            //Rest of the summary
            //Line 2
            cell = new PdfPCell(new Paragraph("Total Number of Loans Extended:  " + numOfLoans, rptFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.NoWrap = true;
            cell.Colspan = 9;
            summaryTable.AddCell(cell);
            //Line 3
            cell = new PdfPCell(new Paragraph(string.Format("Total Finance Charge (Pawn Service Charge(PSC)) Paid Today:  {0}", string.Format("{0:C}",totAmtPaidToday)), rptFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.NoWrap = true;
            cell.Colspan = 9;
            summaryTable.AddCell(cell);
            //Line 4
            cell = new PdfPCell(new Paragraph(string.Format("Total Daily Pawn Service Charges:  {0}", string.Format("{0:C}",totDailyAmt)), rptFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.NoWrap = true;
            cell.Colspan = 9;
            summaryTable.AddCell(cell);
            //Line 5
            cell = new PdfPCell(new Paragraph(string.Format("Pledgor:  {0}", custName), rptFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.NoWrap = true;
            cell.Colspan = 5;
            summaryTable.AddCell(cell);
            //Line 5 Signature line
            cell = new PdfPCell(new Paragraph(" ", rptFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.NoWrap = true;
            cell.Colspan = 4;
            summaryTable.AddCell(cell);
            //Line 6 Signature line caption
            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 5;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            summaryTable.AddCell(cell);


            cell = new PdfPCell(new Paragraph("Pledgor Signature ", rptFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.NoWrap = true;
            cell.Colspan = 4;
            summaryTable.AddCell(cell);

        }



        private void InsertColumnHeaders(PdfPTable headingtable)
        {

            PdfPCell cell = new PdfPCell();
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.Colspan = 1;

            cell = new PdfPCell(new Paragraph("Loan Number", rptFont));
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Loan Amount (Principal)", rptFont));
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Finance Charges (PSC) Paid thru", rptFont));
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Daily Amt of Finance Charges (FSC)", rptFont));
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("New Maturity Date", rptFont));
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Amt Due At Maturity Date", rptFont));
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("New Last Day of Grace", rptFont));
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Amt Due to redeem on Last Day of Grace", rptFont));
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Amt Paid Today", rptFont));
            headingtable.AddCell(cell);

       


        }


        private void InsertReportHeader(PdfPTable headingtable)
        {

            PdfPCell cell = new PdfPCell();
            DateTime dt=DateTime.Now;

            cell = new PdfPCell(new Paragraph(RptObject.ReportTitle, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 6;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Date: " + DateTime.Now.ToString("MM/dd/yyyy"), rptFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Emp No: " + Employee, rptFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Time: " + String.Format("{0:t}", dt), rptFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 7;
            headingtable.AddCell(cell);

            //Store Name line
            cell = new PdfPCell(new Paragraph(RptObject.ReportStore, rptFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 6;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 3;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            headingtable.AddCell(cell);

            //Store Address line
            cell = new PdfPCell(new Paragraph(RptObject.ReportStoreDesc, rptFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 6;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 3;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            headingtable.AddCell(cell);

  
            //empty line
            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 9;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            headingtable.AddCell(cell);

        }


    }



    class ExtnPageEvents : PdfPageEventHelper
    {
        BaseFont bf = null;
        PdfContentByte cb;
        PdfTemplate template;
        private const string UNREDEEMEDDISCLOSURELINE1 = "YOU ARE NOT OBLIGATED TO REPAY THESE PAWN TRANSACTIONS. HOWEVER, TO PREVENT LOSS OF YOUR GOODS DUE TO ";

        private const string UNREDEEMEDDISCLOSURELINE2 = "NON-PAYMENT, YOU MUST EXTEND OR RENEW YOUR PAWN TRANSACTION OR PAY YOUR PAWN TRANSACTION IN FULL ON OR ";

        private const string UNREDEEMEDDISCLOSURELINE3 = "BEFORE THE LAST DAY OF GRACE.";
        private const string FINALSTMT = "KEEP THIS MEMORANDUM WITH YOUR PAWN TICKET(S). BRING YOUR PAWN TICKET(S) TO REDEEM YOUR PLEDGED GOODS.";


        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb = writer.DirectContent;
            template = cb.CreateTemplate(120, 120);
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            template.BeginText();
            template.SetFontAndSize(bf, 8);
            template.ShowText((writer.PageNumber - 1).ToString());
            template.EndText();
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            string sPadding = string.Empty;
   
            bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.BeginText();
            cb = writer.DirectContent;
            float len = bf.GetWidthPoint(UNREDEEMEDDISCLOSURELINE1, 8);
            //Show disclosure line 1
            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(20, 100);
            cb.ShowText(UNREDEEMEDDISCLOSURELINE1);
            cb.EndText();
 

            cb.BeginText();
            len = bf.GetWidthPoint(UNREDEEMEDDISCLOSURELINE2, 8);
            //Show disclosure line 2
            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(20, 90);
            cb.ShowText(UNREDEEMEDDISCLOSURELINE2);
            cb.EndText();


            //Show disclosure line 3
            cb.BeginText();
            len = bf.GetWidthPoint(UNREDEEMEDDISCLOSURELINE3, 8);
            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(20, 80);
            cb.ShowText(UNREDEEMEDDISCLOSURELINE3);
            cb.EndText();


            //Show disclosure final statement
            cb.BeginText();
            len = bf.GetWidthPoint(FINALSTMT, 8);
            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(20, 60);
            cb.ShowText(FINALSTMT);
            cb.EndText();


            var pageN = writer.PageNumber;
            var text = string.Format("Page {0} of ", pageN);
            len = bf.GetWidthPoint(text, 8);

            var pageSize = document.PageSize;
            cb.SetRGBColorFill(0, 0, 0);
            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(pageSize.GetLeft(450), pageSize.GetBottom(30));
            cb.ShowText(text);
            cb.EndText();

            cb.AddTemplate(template, pageSize.GetLeft(450) + len, pageSize.GetBottom(30));

        }
    }




}
