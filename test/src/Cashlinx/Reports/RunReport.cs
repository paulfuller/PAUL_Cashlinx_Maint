/************************************************************************
 * Namespace:       PawnReports.Reports
 * Class:           RunReport
 * 
 * Description      The file to create iTextSharp reports
 * 
 * History
 * S Murphy 1/20/2010, Initial Development
 *  no ticket 3/4/2010 S.Murphy Added Gun Disposition
 *  no ticket SMurphy 4/2/2010 - bypass, Gun Disposition is populated in a different way
 *  no ticket SMurphy 4/21/2010 Changes to remove iText from ReportObject per Sree 
 *          and moved common hard-coded strings to ReportConstants
 **********************************************************************/

using System;
using Common.Libraries.Objects;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using ReportObject = Common.Controllers.Application.ReportObject;

namespace Reports
{

    public static class ReportProcessing
    {
        public static bool DoReport(ReportObject reportObject, IPdfLauncher pdfLauncher)
        {

            bool reportOK = false;

            RunReport runReport = new RunReport();
            runReport.reportObject = reportObject;

            //runReport.reportObject = this;
            if (FileLogger.Instance.IsLogInfo)
            {
                FileLogger.Instance.logMessage(LogLevel.INFO, "ReportProcessing",
                                               "- Report " + reportObject.ReportTitle + " has been requested.");
            }

            if (runReport.CreateReport(pdfLauncher))
            {
                try
                {
                    //SMurphy 4/13/2010 problem when Adobe is already open
                    Process adbProcess = AdobeReaderOpen();
                    if (adbProcess != null)
                    {
                        adbProcess.Kill();
                    }
                }
                catch (Exception eX)
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, "RunReport", "Exception thrown when killing Adobe processes: {0}:{1} {2}",
                                                       eX, eX.Data, eX.StackTrace ?? "NoStackTrace");
                    }
                }

                try
                {
                    pdfLauncher.ShowPDFFile(reportObject.ReportTempFileFullName, false);
                    reportOK = true;
                }
                catch (Exception /*exc*/)
                {
                    reportOK = false;
                }
            }

            return reportOK;

        }
        public static Process AdobeReaderOpen()
        {
            Process process = null;

            foreach (Process currentProcess in Process.GetProcesses())
            {
                if (currentProcess.ProcessName.ToUpper().Contains("ACRORD"))
                {
                    process = currentProcess;
                    break;
                }
            }

            return process;
        }
    }

    public class RunReport : PdfPageEventHelper
    {
        //main report object
        public ReportObject reportObject;

        public RunReport()
        {

        }
        //create report
        public bool CreateReport(IPdfLauncher pdfLauncher)
        {
            bool isSuccessful = false; 

            try
            {
                //set up fonts
                reportObject.CreateTemporaryFullName();
                switch (reportObject.ReportNumber)
                {
                    //no ticket 3/4/2010 S.Murphy Added Gun Disposition
                    case (int)ReportIDs.RifleDispositionReport://Multi Rifle Disposition report
                    case (int)ReportIDs.GunDispositionReport://Gun Disposition report
                        GunDispositionReport gunDispositionReport = new GunDispositionReport(pdfLauncher);
                        gunDispositionReport.reportObject = this.reportObject;
                        isSuccessful = gunDispositionReport.CreateReport();
                        break;
                    case (int)ReportIDs.GunAuditReportATFOpenRecords://Gun Disposition report
                        ATFOpenRecordsReport atfOpenRecordsReport = new ATFOpenRecordsReport(pdfLauncher);
                        reportObject.CreateTemporaryFullName("ATFReport");
                        atfOpenRecordsReport.ReportObject = this.reportObject;
                        isSuccessful = atfOpenRecordsReport.CreateReport();
                        break;
                    case (int)ReportIDs.LoanAuditReport://Loan Audit report
                        LoanAuditReport loanAuditReport = new LoanAuditReport();
                        loanAuditReport.reportObject = this.reportObject;
                        isSuccessful = loanAuditReport.CreateReport();
                        break;

                    case (int)ReportIDs.FullLocationsReport://Loan Audit report
                        FullLocationsReport fullLocationsReport = new FullLocationsReport();
                        fullLocationsReport.reportObject = this.reportObject;
                        isSuccessful = fullLocationsReport.CreateReport();
                        break;

                    case (int)ReportIDs.Snapshot://Snapshot report
                        SnapshotReport snapshotReport = new SnapshotReport();
                        snapshotReport.reportObject = this.reportObject;
                        isSuccessful = snapshotReport.CreateReport();
                        break;

                    case (int)ReportIDs.InPawnJewelryLocationReport://In Pawn Jewelry Locations report
                        InPawnJewelryLocationsReport inPawnJewelryLocationsReport = new InPawnJewelryLocationsReport();
                        inPawnJewelryLocationsReport.reportObject = this.reportObject;
                        isSuccessful = inPawnJewelryLocationsReport.CreateReport();
                        break;

                    case (int)ReportIDs.GunAuditReport://Gun Audit report
                        GunAuditReport gunAuditReport = new GunAuditReport();
                        gunAuditReport.reportObject = this.reportObject;
                        isSuccessful = gunAuditReport.CreateReport();
                        break;
                    case (int)ReportIDs.DetailInventory://Gun Disposition report
                        DetailInventoryReport detailInventoryReport = new DetailInventoryReport();
                        detailInventoryReport.reportObject = this.reportObject;
                        isSuccessful = detailInventoryReport.CreateReport();
                        break;
                    case (int)ReportIDs.CashDrawerLedgerReport://Ledger report
                        CashDrawerLedger ledgerFrm = new CashDrawerLedger();
                        ledgerFrm.RptObject = reportObject;
                        ledgerFrm.Print();
                        isSuccessful = true;
                        break;

                    case (int)ReportIDs.CACCSales://CACC Sales report
                        CACCSalesReport caccReport = new CACCSalesReport(pdfLauncher);
                        caccReport.reportObject = this.reportObject;
                        isSuccessful = caccReport.CreateReport(reportObject.CACCSalesData);

                        break;

                    case (int)ReportIDs.JewelryCount:
                        if (this.reportObject.ReportDetail.Equals("Detailed"))
                        {
                            JewelryCountDetailReport jewelryCountWorksheet = new JewelryCountDetailReport(pdfLauncher);
                            reportObject.ReportTitle = "Jewelry Count Worksheet";
                            jewelryCountWorksheet.reportObject = this.reportObject;
                            isSuccessful = jewelryCountWorksheet.CreateReport(reportObject.JewleryCountDetailData);
                        }
                        else
                        {
                            JewelryCountSummaryReport jewelryCountSummary = new JewelryCountSummaryReport(pdfLauncher);
                            reportObject.ReportTitle = "Jewelry Count Summary";
                            jewelryCountSummary.reportObject = this.reportObject;
                            isSuccessful = jewelryCountSummary.CreateReport(reportObject.JewleryCountSummaryData);
                        }
                        break;

                    case (int)ReportIDs.DailySales:
                        if (this.reportObject.ReportDetail.Equals("Detail"))
                        {

                            DailySalesReport dailySalesReport = new DailySalesReport(pdfLauncher);
                            dailySalesReport.reportObject = this.reportObject;
                            isSuccessful = dailySalesReport.CreateReport(reportObject.DailySalesData);
                        }
                        else
                        {
                            DailySalesReport_Summary summary = new DailySalesReport_Summary(pdfLauncher);
                            summary.reportObject = this.reportObject;
                            isSuccessful = summary.CreateReport(reportObject.DailySalesSummaryData);
                        }
                        break;

                }

            }
            catch (DocumentException de)
            {
                reportObject.ReportError = de.Message;
                reportObject.ReportErrorLevel = (int)LogLevel.WARN;
            }
            catch (IOException ioe)
            {
                reportObject.ReportError = ioe.Message;
                reportObject.ReportErrorLevel = (int)LogLevel.WARN;
            }

            return isSuccessful;
        }
        public void ReportLines(PdfPTable report, bool line, string stringline, bool endOfReport, Font font, int numColumns = 21)
        {
            PdfPCell cell = new PdfPCell();
            //draw a single line
            if (line)
            {
                cell = new PdfPCell(new Phrase(""));
                cell.BorderWidthLeft = Rectangle.NO_BORDER;
                cell.BorderWidthTop = Rectangle.NO_BORDER;
                cell.BorderWidthRight = Rectangle.NO_BORDER;
                cell.Colspan = numColumns;

                report.AddCell(cell);
            }
            //draw a line from a string
            if (stringline.Length > 0)
            {
                cell = new PdfPCell(new Phrase(stringline));
                cell.Colspan = numColumns;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;

                report.AddCell(cell);
            }
            //print End of Report
            if (endOfReport)
            {
                cell = new PdfPCell(new Phrase("***End of Report***", font));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = numColumns;
                report.AddCell(cell);
            }
        }

    }
}
