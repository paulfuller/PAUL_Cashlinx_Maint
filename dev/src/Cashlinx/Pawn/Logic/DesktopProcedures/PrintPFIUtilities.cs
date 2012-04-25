using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Forms;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Reports;

namespace Pawn.Logic.DesktopProcedures
{
    public class PrintPFIUtilities
    {
        public Form ControllerForm { get; set; }
        public string PrintFormName { get; set; }
        public DataTable ReportData { get; set; }
        public string ReportTitle { get; set; }

        public PrintPFIUtilities(DataTable reportData, Form controllerForm, string printFormName, string reportTitle)
        {
            ReportData = reportData;
            ControllerForm = controllerForm;
            PrintFormName = printFormName;
            ReportTitle = reportTitle;
        }

        public void Print(string totalCost, string totalTags, bool showMsg = true)
        {
            if (ReportData == null || ReportData.Rows.Count == 0)
            {
                if (showMsg) MessageBox.Show("No records available to print", ReportTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ControllerForm.Cursor = Cursors.WaitCursor;

            ProcessingMessage processingForm = new ProcessingMessage("Please wait while we generate report.");
            processingForm.Show();

            ReportObject reportObject = new ReportObject();
            reportObject.ReportTitle = ReportTitle;
            reportObject.ReportStore = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            reportObject.ReportTempFile = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;
            reportObject.CreateTemporaryFullName();
            PfiPostReport report = new PfiPostReport(ReportData, totalCost, totalTags, ShopDateTime.Instance.ShopDate.ToShortDateString(), Convert.ToDateTime(ShopDateTime.Instance.ShopTime.ToString()), reportObject, PdfLauncher.Instance);
            if (!report.CreateReport())
            {
                processingForm.Close();
                processingForm.Dispose();
                ControllerForm.Cursor = Cursors.Default;
                MessageBox.Show("Failed to generate report", "PFI POST", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Dictionary<string, string> eDeviceData = GetPrintDeviceData();
            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
            {
                if (FileLogger.Instance.IsLogInfo)
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, "PrintPFIUtilities", "Printing {0} on {1}", ReportTitle,
                                                   GlobalDataAccessor.Instance.DesktopSession.LaserPrinter);
                }
                string strReturnMessage = 
                    PrintingUtilities.printDocument(
                        reportObject.ReportTempFileFullName,
                        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port,
                        1);
                if (strReturnMessage.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) == -1)
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot print " + ReportTitle + " report " + strReturnMessage);
                    }
                }
            }

            processingForm.Close();
            processingForm.Dispose();
            ControllerForm.Cursor = Cursors.Default;

            if (showMsg) MessageBox.Show("Printing Complete", ReportTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private Dictionary<string, string> GetPrintDeviceData()
        {
            var errorCode = string.Empty;
            var errorMessage = string.Empty;

            string terminalId = GetTerminalId();

            var inParams = new List<OracleProcParam>
                                       {
                                           new OracleProcParam("p_terminalid", terminalId),
                                           new OracleProcParam("p_form_name", PrintFormName),
                                           new OracleProcParam("p_store_number", GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber)
                                       };

            DataTable documentInfo;
            DataTable ipportInfo;
            DataTable printerInfo;

            Hashtable eDeviceDataHash = GenerateDocumentsProcedures.
                    GenerateDocumentsEssentialInformation(
                    inParams,
                    out documentInfo,
                    out printerInfo,
                    out ipportInfo,
                    out errorCode,
                    out errorMessage);

            Dictionary<string, string> eDeviceData = new Dictionary<string, string>();
            foreach (string s in eDeviceDataHash.Keys)
            {
                if (string.IsNullOrEmpty(s))
                    continue;
                object hashVal = eDeviceDataHash[s];
                if (hashVal == null)
                    continue;
                eDeviceData.Add(s, hashVal.ToString());
            }

            return eDeviceData;
        }

        private string GetTerminalId()
        {
            if (!string.IsNullOrEmpty(global::Pawn.Properties.Resources.OverrideMachineName))
            {
                return global::Pawn.Properties.Resources.OverrideMachineName;
            }
            else
            {
                return GlobalDataAccessor.Instance.CurrentSiteId.TerminalId;
            }
        }
    }
}
