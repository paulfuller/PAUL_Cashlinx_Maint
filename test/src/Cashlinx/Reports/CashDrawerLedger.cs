/********************************************************************************
* Namespace: PawnReports.Reports
* FileName: CashDrawerLedger
* Prints the cash drawer ledger - preliminary and final versions using itextsharp
* Sreelatha Rengarajan 4/15/2010 Initial version
 * SR 4/19/2010 Made sure that report header appeared in every page
* no ticket SMurphy changed reference for ReportsObject
 * SR 5/25/2010 Changed from legal to letter
 * SR 11/09/2010 Added denomination data and ending balance data if its a final ledger
*********************************************************************************/

using System;
using System.Data;
using System.IO;
using System.Linq;
using Common.Controllers.Application;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;
using Utilities = Common.Libraries.Utility.Utilities;

namespace Reports
{
    public class CashDrawerLedger
    {
        private const string SHOPTOSHOPTRANSFER = "Shop To Shop Transfer";
        private const string REIN = "REIN";
        private const string REOUT = "REOUT";
        private const string TRIN = "TRIN";
        private const string TROUT = "TROUT";
        private const string CASH = "CASH";
        private const string USDCOIN = "USD COIN";
        private const string USD = "USD";
        private const string OTHER = "OTHER";
        private const string TUBE = "TUBE";

        public ReportObject RptObject;
        public LedgerReport LedgerReportData;
        public static string ReportTitle;

        private Font rptFont;
        private Font rptLargeFont;
        private decimal totalReceiptAmount = 0.0M;
        private decimal totalDisbursedAmount = 0.0M;
        private DataTable cashdrawerTransactions;
        private decimal receiptAmount = 0.0M;
        private decimal disbursedAmount = 0.0M;
        private Document document;
        private PdfWriter writer;




        public bool Print()
        {

            rptLargeFont = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);
            ReportTitle = RptObject.ReportTitle;
            cashdrawerTransactions = LedgerReportData.CashDrawerTransactions;
            DataTable transactionData = null;
            if (cashdrawerTransactions != null)
                transactionData = cashdrawerTransactions.Clone();
            if (cashdrawerTransactions != null && cashdrawerTransactions.Rows.Count > 0)
            {
                var distinctRows = (from DataRow dRow in cashdrawerTransactions.Rows
                                    select new { col1 = dRow["trantype"] }).Distinct();


                //Get individual transaction rows
                //Merge all the rows into one datatable
                foreach (var row in distinctRows)
                {
                    var tranRows = cashdrawerTransactions.Select(string.Format("trantype='{0}'", row.col1));
                    if (tranRows != null)
                    {
                        foreach (var dr in tranRows)
                            if (transactionData != null)
                            {
                                transactionData.ImportRow(dr);
                            }
                    }
                }
            }



            try
            {
                document = new Document(PageSize.A4.Rotate());
                //Set Font for the report
                rptFont = rptFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);

                string rptFileName = RptObject.ReportTempFileFullName;
                if (!string.IsNullOrEmpty(rptFileName))
                {
                    document.AddTitle(RptObject.ReportTitle);
                    writer = PdfWriter.GetInstance(document, new FileStream(rptFileName, FileMode.Create));
                    var events = new LedgerPageEvents();
                    writer.PageEvent = events;
                    var table = new PdfPTable(12);
                    document.SetPageSize(PageSize.A4.Rotate());
                    document.SetMargins(0, 00, 10, 45);

                    table.TotalWidth = 1000f;

                    var image = Image.GetInstance(RptObject.ReportImage, BaseColor.WHITE);
                    image.ScalePercent(25);
                    //Insert the report header
                    InsertReportHeader(table, image);

                    //Create Column headers
                    InsertColumnHeaders(table);

                    //Set number of header rows
                    table.HeaderRows = 7;
                    if (transactionData != null && transactionData.Rows.Count > 0)
                    {
                        //Insert report data
                        string tranType = Utilities.GetStringValue(transactionData.Rows[0]["trantype"]).Trim();
                        var currTranType = string.Empty;

                        foreach (DataRow dr in transactionData.Rows)
                        {
                            if (Utilities.GetDecimalValue(dr["amount"], 0) == 0)
                                continue;
                            currTranType = Utilities.GetStringValue(dr["trantype"]).Trim();

                            if (tranType.Length > 0 && tranType != currTranType)
                            {

                                //write summary data
                                InsertSummaryData(table, tranType);
                                receiptAmount = 0;
                                disbursedAmount = 0;
                                tranType = currTranType;

                            }
                            var pCell = new PdfPCell
                                             {
                                                 Colspan = 1,
                                                 HorizontalAlignment = Element.ALIGN_JUSTIFIED,
                                                 Border = Rectangle.NO_BORDER,
                                                 Phrase =
                                                     new Phrase(
                                                     Common.Libraries.Utility.Utilities.GetStringValue(dr["empnumber"]),
                                                     rptFont)
                                             };

                            pCell.Border = Rectangle.NO_BORDER;
                            pCell.Phrase = new Phrase(Utilities.GetStringValue(dr["empnumber"]), rptFont);
                            table.AddCell(pCell);
                            pCell.Phrase = new Phrase(Utilities.GetStringValue(dr["trannumber"]), rptFont);
                            table.AddCell(pCell);
                            pCell.Phrase = new Phrase(Utilities.GetStringValue(dr["prevtkt"]), rptFont);
                            table.AddCell(pCell);
                            pCell.Phrase = new Phrase(Utilities.GetStringValue(dr["name"]), rptFont);
                            pCell.Colspan = 2;
                            table.AddCell(pCell);
                            string textToShow;
                            //If the transaction was shop to shop and the ledger being printed is for 
                            //cashdrawer it has to be TOPS transfer since other shop to shop happens 
                            //from safe
                            var transactionType = Common.Libraries.Utility.Utilities.GetStringValue(dr["tendertype"]);
                            if (tranType == SHOPTOSHOPTRANSFER && !LedgerReportData.IsSafe)
                            {


                                textToShow = transactionType == TOPSTRANSFEROPERATIONS.REIN.ToString() ? "XFER From TOPS" : "XFER To TOPS";
                            }
                            else
                            {
                                CreditCardTypes cType;
                                TenderTypes tType;
                                DebitCardTypes dType;
                                bool isInbound;
                                var retVal = Commons.GetTenderAndCardTypeFromOpCode(transactionType, out tType, out cType, out dType, out isInbound);
                                var description = Common.Libraries.Utility.Utilities.GetStringValue(dr["tendertype"]);
                                if (retVal)
                                {

                                    if (tType == TenderTypes.CREDITCARD)
                                    {
                                        description = "CC" + " " + cType.ToString();
                                    }
                                    else if (tType == TenderTypes.DEBITCARD)
                                    {
                                        description = "DEBIT" + " " + dType.ToString();
                                    }
                                    else
                                    {
                                        description = tType.ToString();
                                    }


                                }
                                else
                                {
                                    if (description == REIN || description == REOUT || description == TRIN || description == TROUT)
                                        description = CASH;
                                }
                                textToShow = description.ToUpper();
                            }

                            pCell.Phrase = new Phrase(textToShow, rptFont);
                            pCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            pCell.Colspan = 1;
                            table.AddCell(pCell);
                            pCell.Phrase = new Phrase(Common.Libraries.Utility.Utilities.GetStringValue(dr["status_cd"]), rptFont);
                            table.AddCell(pCell);
                            pCell.Phrase = new Phrase(currTranType, rptFont);
                            pCell.NoWrap = true;
                            table.AddCell(pCell);
                            pCell.Phrase = new Phrase(string.Format("{0:C}", dr["amount"]), rptFont);
                            pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            pCell.Colspan = 2;
                            table.AddCell(pCell);
                            decimal currRecptAmt = Common.Libraries.Utility.Utilities.GetDecimalValue(dr["receiptamount"], 0);
                            decimal currDisbursedAmt = Common.Libraries.Utility.Utilities.GetDecimalValue(dr["disbursedamount"], 0);
                            pCell.Phrase = new Phrase(string.Format("{0:C}", currRecptAmt), rptFont);
                            pCell.Colspan = 1;
                            pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            pCell.NoWrap = true;
                            table.AddCell(pCell);
                            pCell.Phrase = new Phrase(string.Format("{0:C}", currDisbursedAmt), rptFont);
                            pCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            pCell.NoWrap = true;
                            table.AddCell(pCell);
                            totalReceiptAmount += currRecptAmt;
                            totalDisbursedAmount += currDisbursedAmt;
                            receiptAmount += currRecptAmt;
                            disbursedAmount += currDisbursedAmt;
                        }
                        //Insert summary for the last transaction type 

                        InsertSummaryData(table, tranType);
                    }
                    else
                    {
                        var cell = new PdfPCell(new Phrase(string.Empty))
                                        {
                                            Colspan = 12,
                                            Border = Rectangle.NO_BORDER,
                                            HorizontalAlignment = Element.ALIGN_RIGHT
                                        };
                        table.AddCell(cell);

                    }



                    //Insert the end of report data
                    InsertFinalSummaryData(ref table);


                    var cellData = new PdfPCell(new Paragraph("***End of Report***", rptFont))
                                        {
                                            HorizontalAlignment = Element.ALIGN_CENTER,
                                            Border = Rectangle.TOP_BORDER,
                                            Colspan = 12
                                        };
                    table.AddCell(cellData);
                    document.Open();
                    document.Add(table);
                    document.Close();
                    return true;
                }
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cash drawer ledger printing failed since file name is not set");
                return false;
            }
            catch (Exception de)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cash drawer ledger printing {0}", de.Message);
                return false;

            }

        }

        private void InsertSummaryData(PdfPTable summaryTable, string tranType)
        {

            var cell = new PdfPCell(new Paragraph(string.Format("Total For {0}", tranType), rptFont))
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER, Border = Rectangle.BOX, Colspan = 10
                            };
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Format("{0:C}", receiptAmount), rptFont))
                   {
                       HorizontalAlignment = Element.ALIGN_RIGHT, Border = Rectangle.BOX, Colspan = 1, NoWrap = true
                   };
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Format("{0:C}", disbursedAmount), rptFont))
                   {
                       HorizontalAlignment = Element.ALIGN_RIGHT, Border = Rectangle.BOX, Colspan = 1, NoWrap = true
                   };
            summaryTable.AddCell(cell);


        }


        private void InsertFinalSummaryData(ref PdfPTable summaryTable)
        {

            PdfPCell cell = new PdfPCell(new Paragraph("Total for this Cash Drawer:", rptFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.BOX;
            cell.Colspan = 10;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Format("{0:C}", totalReceiptAmount), rptFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.BOX;
            cell.NoWrap = true;
            cell.Colspan = 1;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Format("{0:C}", totalDisbursedAmount), rptFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.BOX;
            cell.NoWrap = true;
            cell.Colspan = 1;
            summaryTable.AddCell(cell);

            //empty line
            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 12;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            summaryTable.AddCell(cell);

            int numOfCells = 0;
            int i = 0;
            int totalDenomCount = 0;
            bool bRowsAdded = false;
            if (LedgerReportData.LedgerDenomination != null)
            {
                totalDenomCount = LedgerReportData.LedgerDenomination.Count;


                if (totalDenomCount > 0)
                {
                    while (i < totalDenomCount)
                    {
                        var table = new PdfPTable(2);
                        for (int j = 1; j < 5; j++)
                        {
                            if ((i + 1) < totalDenomCount)
                            {
                                string denomLabel = LedgerReportData.LedgerDenomination[i];
                                if (denomLabel.StartsWith("USD COIN"))
                                    denomLabel = denomLabel.Substring(8);
                                else if (denomLabel.StartsWith("USD"))
                                    denomLabel = denomLabel.Substring(4);
                                else if (denomLabel.StartsWith("OTHER"))
                                    denomLabel = "1";
                                else
                                    denomLabel = "TUBE " + denomLabel;
                                decimal amount = 0;
                                amount = denomLabel.StartsWith("TUBE")
                                                 ? Utilities.GetDecimalValue(
                                                         LedgerReportData.LedgerDenomination[
                                                                 i + 1])
                                                 : Utilities.GetDecimalValue(
                                                         denomLabel) *
                                                   Utilities.GetDecimalValue(
                                                           LedgerReportData.LedgerDenomination[
                                                                   i + 1]);
                                cell = denomLabel.StartsWith("TUBE") ? new PdfPCell(new Paragraph(denomLabel, rptFont)) : new PdfPCell(new Paragraph(Commons.GetDenominationHeading(LedgerReportData.LedgerDenomination[i]), rptFont));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.Border = Rectangle.NO_BORDER;
                                cell.Colspan = 1;
                                cell.NoWrap = true;
                                table.AddCell(cell);

                                cell = new PdfPCell(new Paragraph(string.Format("{0:C}", amount), rptFont))
                                       {
                                           HorizontalAlignment = Element.ALIGN_RIGHT,
                                           Border = Rectangle.NO_BORDER,
                                           Colspan = 1,
                                           NoWrap = true
                                       };
                                table.AddCell(cell);
                                bRowsAdded = true;
                                i = i + 2;
                            }
                            else
                            {
                                j = 5;
                                i++;
                            }
                        }
                        if (bRowsAdded)
                        {
                            var nestTable = new PdfPCell(table)
                                                 {
                                                     Colspan = 2
                                                 };
                            summaryTable.AddCell(nestTable);
                            numOfCells = numOfCells + 2;
                        }

                    }
                }

                if (numOfCells > 0)
                {

                    cell = new PdfPCell(new Paragraph(string.Empty));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.Colspan = 12 - numOfCells;
                    summaryTable.AddCell(cell);

                }


                //empty line
                cell = new PdfPCell(new Phrase(string.Empty));
                cell.Colspan = 12;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(string.Empty));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                summaryTable.AddCell(cell);


                cell = new PdfPCell(new Paragraph("(1)Beginning CASH ON Hand", rptFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(string.Format("{0:C}", LedgerReportData.BeginningBalance), rptFont));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 1;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(string.Empty));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(string.Empty));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                summaryTable.AddCell(cell);


                cell = new PdfPCell(new Paragraph("(2)TOTAL BEG CASH AND RECEIPTS", rptFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(string.Format("{0:C}", totalReceiptAmount), rptFont));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 1;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(string.Empty));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(string.Empty));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph("(3)TOTAL CASH PAID OUT", rptFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(string.Format("{0:C}", totalDisbursedAmount), rptFont));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 1;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(string.Empty));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(string.Empty));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph("(4)EXPECTED BALANCE", rptFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(string.Format("{0:C}", LedgerReportData.BeginningBalance + totalReceiptAmount - totalDisbursedAmount), rptFont));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 1;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(""));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(""));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph("(5)ACTUAL CASH COUNT", rptFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(string.Format("{0:C}", LedgerReportData.ActualCashCount), rptFont));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 1;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(""));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(""));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph("(6)CASH OVER OR SHORT", rptFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                summaryTable.AddCell(cell);

                decimal overShortAmt = LedgerReportData.ActualCashCount - (LedgerReportData.BeginningBalance + totalReceiptAmount - totalDisbursedAmount);

                cell = new PdfPCell(new Paragraph(string.Format("{0:C}", overShortAmt), rptFont));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 1;
                summaryTable.AddCell(cell);

                cell = new PdfPCell(new Paragraph(""));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                summaryTable.AddCell(cell);

                if (overShortAmt > 0 || overShortAmt < 0)
                {
                    cell = new PdfPCell(new Paragraph(""));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.Colspan = 4;
                    summaryTable.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Over Short Comment:", rptFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.Colspan = 3;
                    summaryTable.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(LedgerReportData.OverShortComment, rptFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.Colspan = 5;
                    summaryTable.AddCell(cell);


                }

            }



        }



        private void InsertColumnHeaders(PdfPTable headingtable)
        {

            PdfPCell cell = new PdfPCell();
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.Colspan = 1;

            cell = new PdfPCell(new Paragraph("Emp #", rptFont));
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(" Tran #", rptFont));
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Prev. #", rptFont));
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Customer Name", rptFont));
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("MOP", rptFont));
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Status", rptFont));
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Tran. Type", rptFont));
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Tran. Amount", rptFont));
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Receipt", rptFont));
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Disbursement", rptFont));
            headingtable.AddCell(cell);


        }


        private void InsertReportHeader(PdfPTable headingtable, Image image)
        {

            PdfPCell cell = new PdfPCell();


            cell = new PdfPCell(image);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 4;
            headingtable.AddCell(cell);



            cell = new PdfPCell(new Paragraph(RptObject.ReportStoreDesc, rptFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 8;
            headingtable.AddCell(cell);

            //cell = new PdfPCell(new Paragraph("Run Date: " + DateTime.Now.ToString("MM/dd/yyyy"), rptFont));
            cell = new PdfPCell(new Paragraph("Run Date: " + DateTime.Now.ToString("g"), rptFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 12;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Report #: " + RptObject.ReportNumber, rptFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 12;
            headingtable.AddCell(cell);



            cell = new PdfPCell(new Paragraph(RptObject.ReportTitle, rptLargeFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 12;
            headingtable.AddCell(cell);



            cell = new PdfPCell(new Paragraph("Drawer ID: " + LedgerReportData.CashDrawerName, rptFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Employee: " + LedgerReportData.EmployeeName, rptFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 8;
            headingtable.AddCell(cell);

            //empty line
            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 12;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            headingtable.AddCell(cell);

        }

    }

    class LedgerPageEvents : PdfPageEventHelper
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
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cash drawer ledger printing" + dex.Message);
            }
            catch (IOException ioe)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cash drawer ledger printing" + ioe.Message);
            }
        }

        public override void OnEndPage(PdfWriter writer, Document doc)
        {
            base.OnEndPage(writer, doc);

            int pageN = writer.PageNumber;
            String text = "Page " + pageN + " of ";
            float len = footerBaseFont.GetWidthPoint(text, 8);

            Rectangle pageSize = doc.PageSize;



            contentByte.SetRGBColorFill(100, 100, 100);

            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            contentByte.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(30));
            contentByte.ShowText(text);
            contentByte.EndText();

            contentByte.AddTemplate(template, pageSize.GetLeft(40) + len, pageSize.GetBottom(30));

            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT,
                CashDrawerLedger.ReportTitle,
                pageSize.GetRight(40),
                pageSize.GetBottom(30), 0);
            contentByte.EndText();


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
