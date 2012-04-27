/************************************************************************
 * Namespace:       PawnUtilities.Shared
 * Class:           LoanAuditReport
 * 
 * Description      The file to create iTextSharp Loan Audit reports
 * 
 * History
 * S Murphy 1/20/2010, Initial Development
 *  no ticket SMurphy 3/25/2010 - Store name & Run Date overlapped
 *  no ticket 4/13/2010 SMurphy namespace cleanup
 *  no ticket SMurphy 4/21/2010 Changes to remove iText from ReportObject per Sree 
 *          and moved common hard-coded strings to ReportConstants
**********************************************************************/

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
    public class LoanAuditReport : PdfPageEventHelper
    {
        private int nrLoans = 0;
        private decimal totalPrincipalIncludingPartialPayments = 0;

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

        public LoanAuditReport()
        {

        }

        //create report
        public bool CreateReport()
        {
            bool isSuccessful = false;

            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.LEGAL);

            try
            {
                //set up RunReport event overrides & create doc
                LoanAuditReport events = new LoanAuditReport();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                //set up tables, etc...
                PdfPTable table = new PdfPTable(23);
                PdfPCell cell = new PdfPCell();
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(35);

                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);
                _reportFontLargeUnderline = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.UNDERLINE);

                runReport = new RunReport();

                document.AddTitle(reportObject.ReportTitle + ": " + DateTime.Now.ToString("MM/dd/yyyy"));
                document.SetPageSize(PageSize.LEGAL.Rotate());
                document.SetMargins(-100, -100, 10, 45);
                
                ReportHeader(table, gif);
                ColumnHeaders(table);
                ReportDetail(table);
                ReportSummary(table);

                table.HeaderRows = 8;

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
        private void ColumnHeaders(PdfPTable headingtable)
        {
            //set up tables, etc...
            PdfPCell cell = new PdfPCell();

            cell = new PdfPCell(new Paragraph("   Ticket #", _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("       Original Ticket #", _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Category Name", _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Loan Amount", _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Current Principal Amount", _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Status", _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Loan Origination Date", _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Merchandise Description", _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 5;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Aisle", _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Shelf", _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Location", _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Gun #", _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 1;
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
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            //no ticket SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(reportObject.ReportStoreDesc, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            //no ticket SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 16;
            headingtable.AddCell(cell);

            //  heading - row 2
            cell = new PdfPCell(new Paragraph(ReportHeaders.OPERATIONAL, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingLeft = 12;
            //cell.PaddingTop = -5;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(reportObject.ReportTitle + ReportHeaders.REPORT, _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Colspan = 16;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(ReportHeaders.RUNDATE + DateTime.Now.ToString("MM/dd/yyyy"), _reportFont));
            //no ticket SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.PaddingTop = -15;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            // heading - row 3 
            cell = new PdfPCell(new Paragraph(ReportHeaders.REPORTING + reportObject.ReportParms[(int)ReportEnums.STARTDATE] + " thru " + reportObject.ReportParms[(int)ReportEnums.ENDDATE], _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.PaddingLeft = 12;
            //cell.PaddingTop = -10;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(ReportHeaders.REPORTSORT + reportObject.ReportSort, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Colspan = 16;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(ReportHeaders.REPORTNUM + reportObject.ReportNumber, _reportFont));
            //no ticket SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.PaddingTop = -20;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            // heading - row 4
            cell = new PdfPCell(new Paragraph(string.Empty, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingLeft = 12;
            cell.PaddingTop = -10;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase(ReportHeaders.REPORTDETAIL + reportObject.ReportDetail, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 16;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.PaddingTop = -20;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            for (int iRowCounter = 0; iRowCounter < 3; iRowCounter++)
            {
                runReport.ReportLines(headingtable, false, " ", false, _reportFont, 23);
            }
        }

        private void ReportDetail(PdfPTable datatable)
        {
            string lastLoan = string.Empty;
            runReport.ReportLines(datatable, true, "", false, _reportFont, 23);

            PdfPCell cell = new PdfPCell();
            foreach (ReportObject.LoanAudit loanAuditData in reportObject.LoanAuditData)
            {
                //nrLoans += loanAuditData.IncludeInCount;
                
                if (!loanAuditData.CurrTktNumber.Equals(lastLoan))
                {
                    lastLoan = loanAuditData.CurrTktNumber;
                    nrLoans++;
                    totalPrincipalIncludingPartialPayments += loanAuditData.PrincipalAmount;

                    cell = new PdfPCell(new Phrase(loanAuditData.CurrTktNumber, _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(loanAuditData.OrigTktNumber, _reportFont));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(loanAuditData.CategoryName, _reportFont));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(loanAuditData.TotalLoanAmt.ToString("N"), _reportFont));
                    cell.Colspan = 2;
                    cell.PaddingRight = 30;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(loanAuditData.PrincipalAmount.ToString("N"), _reportFont));
                    cell.Colspan = 2;
                    cell.PaddingRight = 30;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(loanAuditData.ShopLoanStatus, _reportFont));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(loanAuditData.PawnLoanOrigDate, _reportFont));
                    cell.Colspan = 2;
                    cell.PaddingRight = 30;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);
                }
                else
                {
                    cell = new PdfPCell(new Phrase("", _reportFont));
                    cell.Colspan = 14;
                    cell.PaddingRight = 30;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);                    
                }

                cell = new PdfPCell(new Phrase(loanAuditData.FullMdseDescr, _reportFont));
                cell.Colspan = 5;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(loanAuditData.LocationAisle, _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(loanAuditData.Shelf, _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(loanAuditData.Location, _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(loanAuditData.GunNumber, _reportFont));
                cell.Colspan = 1;
                //cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);
            }
        }

        private void ReportSummary(PdfPTable datatable)
        {
            //set up tables, etc...
            PdfPCell cell = new PdfPCell();

            runReport.ReportLines(datatable, true, "", false, _reportFont, 23);

            switch (reportObject.ReportNumber)
            {
                case 206:
                    //row 1
                    cell = new PdfPCell(new Phrase("Summary", _reportFontLargeUnderline));
                    cell.Colspan = 23;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    //row 2
                    cell = new PdfPCell(new Phrase("Loan:", _reportFont));
                    cell.Colspan = 5;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);
                    
                    cell = new PdfPCell(new Phrase(nrLoans.ToString(), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    decimal totalLoans = 0;
                    decimal totalBKHOLD = 0;
                    decimal totalCUSTHOLD = 0;
                    decimal totalPOLICEHOLD = 0;

                    string lastLoan = string.Empty;
                    foreach (ReportObject.LoanAudit loanAudit in reportObject.LoanAuditData)
                    {
                        if (!lastLoan.Equals(loanAudit.CurrTktNumber))
                        {
                            totalLoans += Convert.ToDecimal(loanAudit.TotalLoanAmt);

                            switch(loanAudit.ShopLoanStatus)
                            {
                                case ("IP BKHOLD"):
                                    totalBKHOLD++;
                                    break;
                                case ("IP CUSTHOLD"):
                                    totalCUSTHOLD++;
                                    break;
                                case ("IP POLICEHOLD"):
                                    totalPOLICEHOLD++;
                                    break;
                            }
                        }
                    }

                    cell = new PdfPCell(new Phrase(totalLoans.ToString("N"), _reportFont));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", _reportFont));
                    cell.Colspan = 15;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    // Row 4
                    cell = new PdfPCell(new Phrase("Current Principal Amount:", _reportFont));
                    cell.Colspan = 3;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(totalPrincipalIncludingPartialPayments.ToString("N"), _reportFont));
                    cell.Colspan = 20;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    //row 5
                    cell = new PdfPCell(new Phrase("Loans on Hold:", _reportFont));
                    cell.Colspan = 3;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase((totalBKHOLD + totalCUSTHOLD + totalPOLICEHOLD).ToString(), _reportFont));
                    cell.Colspan = 20;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    //row 6
                    cell = new PdfPCell(new Phrase("Bankruptcy", _reportFont));
                    cell.Colspan = 3;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingLeft = 10;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(totalBKHOLD.ToString(), _reportFont));
                    cell.Colspan = 20;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    //row 7
                    cell = new PdfPCell(new Phrase("Customer", _reportFont));
                    cell.Colspan = 3;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingLeft = 10;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(totalCUSTHOLD.ToString(), _reportFont));
                    cell.Colspan = 20;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    //row 8
                    cell = new PdfPCell(new Phrase("Police", _reportFont));
                    cell.Colspan = 3;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingLeft = 10;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(totalPOLICEHOLD.ToString(), _reportFont));
                    cell.Colspan = 20;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    //row 9
                    cell = new PdfPCell(new Phrase("Loan:", _reportFont));
                    cell.Colspan = 5;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);
                    
                    cell = new PdfPCell(new Phrase(nrLoans.ToString(), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(totalLoans.ToString("N"), _reportFont));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", _reportFont));
                    cell.Colspan = 15;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);
                    break;

                default:
                    break;
            }

            runReport.ReportLines(datatable, true, "", true, _reportFont, 23);

            cell = new PdfPCell(new Phrase("The item amounts indicated in this report reflect the original item loan amounts and are not adjusted to reflect the pay downs.", _reportFont));
            cell.Colspan = datatable.NumberOfColumns;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.PaddingTop = 10f;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);
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
            var pageN = writer.PageNumber;
            var text = string.Empty;
            var reportName = writer.Info.Get(PdfName.TITLE).ToString().Split(':');
            text = string.Format("{0}{1} Page {2} of ", reportName[0], StringUtilities.fillString(" ", 385), pageN);

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
