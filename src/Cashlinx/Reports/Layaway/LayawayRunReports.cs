using Common.Libraries.Objects;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;

namespace Reports.Layaway
{
    public static class LayawayReportProcessing
    {
        public static bool DoReport(LayawayReportObject reportObject, bool open, IPdfLauncher pdfLauncher)
        {

            bool reportOK = false;
            LayawayRunReports runReport = new LayawayRunReports();
            runReport.reportObject = reportObject;

            //runReport.reportObject = this;

            FileLogger.Instance.logMessage(LogLevel.INFO, "ReportProcessing", "- Report " + reportObject.ReportTitle + " has been requested.");

            if (runReport.CreateReport(pdfLauncher))
            {
                if (open)
                {
                    pdfLauncher.ShowPDFFile(reportObject.ReportTempFileFullName, false);
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
                }
            }

            return process;
        }
    }

    public class LayawayRunReports : PdfPageEventHelper
    {
         //main report object
        public LayawayReportObject reportObject;

        public LayawayRunReports()
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
                    case (int)LayawayReportIDs.LayawayHistoryAndSchedule:
                        LayawayHistoryAndSchedule layawayHistoryAndSchedule = new LayawayHistoryAndSchedule();
                        layawayHistoryAndSchedule.reportObject = this.reportObject;
                        isSuccessful = layawayHistoryAndSchedule.CreateReport();
                        break;
                    case (int)LayawayReportIDs.LayawayContract:
                        LayawayContractReport layawayContractRpt = new LayawayContractReport(pdfLauncher);
                        layawayContractRpt.ReportObject = this.reportObject;
                        isSuccessful = layawayContractRpt.CreateReport();
                        break;
                    case (int)LayawayReportIDs.LayawayForfeitPickingSlip:
                        LayawayForefeitPickingSlip layawayForefeitPickingSlip = new LayawayForefeitPickingSlip(pdfLauncher);
                        layawayForefeitPickingSlip.ReportObject = this.reportObject;
                        isSuccessful = layawayForefeitPickingSlip.CreateReport();
                        break;
                    case (int)LayawayReportIDs.ForfeitedLayawaysListing:
                        ForfeitedLayawaysListingReport forfeitedLayawaysListingReport = new ForfeitedLayawaysListingReport(pdfLauncher);
                        forfeitedLayawaysListingReport.ReportObject = this.reportObject;
                        isSuccessful = forfeitedLayawaysListingReport.CreateReport();
                        break;
                    case (int)LayawayReportIDs.TerminatedLayawaysPickingSlip:
                        TerminatedLayawayPickingSlip terminatedLayawayPickingSlip = new TerminatedLayawayPickingSlip(pdfLauncher);
                        terminatedLayawayPickingSlip.ReportObject = this.reportObject;
                        isSuccessful = terminatedLayawayPickingSlip.CreateReport();
                        break;
                    case (int)LayawayReportIDs.TerminatedLayawaysListing:
                        TerminatedLayawaysListingReport terminatedLayawaysListingReport = new TerminatedLayawaysListingReport(pdfLauncher);
                        terminatedLayawaysListingReport.ReportObject = this.reportObject;
                        isSuccessful = terminatedLayawaysListingReport.CreateReport();
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

        public void ReportLayawayHistoryLines(PdfPTable report, bool line, string stringline, bool endOfReport, Font font)
        {
            PdfPCell cell = new PdfPCell();
            //draw a single line
            if (line)
            {
                cell = new PdfPCell(new Phrase(""));
                cell.BorderWidthLeft = Rectangle.NO_BORDER;
                cell.BorderWidthTop = Rectangle.NO_BORDER;
                cell.BorderWidthRight = Rectangle.NO_BORDER;
                cell.Colspan = 8;
                report.AddCell(cell);
            }
            //draw a line from a string
            if (stringline.Length > 0)
            {
                cell = new PdfPCell(new Phrase(stringline));
                cell.Colspan = 8;
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
                cell.Colspan = 8;
                report.AddCell(cell);
            }
        }

        public void ReportLines(PdfPTable report, bool line, string stringline, bool endOfReport, Font font)
        {
            PdfPCell cell = new PdfPCell();
            //draw a single line
            if (line)
            {
                cell = new PdfPCell(new Phrase(""));
                cell.BorderWidthLeft = Rectangle.NO_BORDER;
                cell.BorderWidthTop = Rectangle.NO_BORDER;
                cell.BorderWidthRight = Rectangle.NO_BORDER;
                cell.Colspan = 7;
                report.AddCell(cell);
            }
            //draw a line from a string
            if (stringline.Length > 0)
            {
                cell = new PdfPCell(new Phrase(stringline));
                cell.Colspan = 7;
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
                cell.Colspan = 7;
                report.AddCell(cell);
            }
        }
    }
}
