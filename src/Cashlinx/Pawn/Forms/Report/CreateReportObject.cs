using System;
using System.Data;
using Common.Controllers.Application;
using Common.Controllers.Database.Couch;
using Common.Controllers.Database.Couch.Impl;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Security;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Reports;
using Reports.Inquiry;
using Document = Common.Libraries.Objects.Doc.Document;

namespace Pawn.Forms.Report
{
    public class CreateReportObject
    {
        #region Private Fields
        ReportObject reportObject = null;
        #endregion

        #region Private Methods
        private ReportObject GetReportObject(string reportTitle, int reportNumber, string pathVariable, string formName)
        {
            ReportObject reportObject = new ReportObject();
            try
            {
                reportObject.ReportTitle = reportTitle;
                reportObject.ReportNumber = reportNumber;
                reportObject.FormName = formName;
                //reportObject.LayawayNumber = 101;
                reportObject.ReportTempFileFullName = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\" + pathVariable + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";

                reportObject.ReportTempFile = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;
                reportObject.ReportStore = GlobalDataAccessor.Instance.CurrentSiteId.StoreName + "-" + GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber.ToString();
                //reportObject.ReportStoreDesc1 = GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress1;//"CASH AMERICA PAWN OF DFW";
                //reportObject.ReportStoreDesc2 = GlobalDataAccessor.Instance.CurrentSiteId.State + ", " + GlobalDataAccessor.Instance.CurrentSiteId.StoreZipCode;
                //reportObject.CustomerName = CashlinxDesktopSession.Instance.ActiveCustomer.FirstName + " " + CashlinxDesktopSession.Instance.ActiveCustomer.MiddleInitial + " " + CashlinxDesktopSession.Instance.ActiveCustomer.LastName;
                reportObject.ReportError = string.Empty;
                reportObject.ReportErrorLevel = 0;
                //reportObject.ReportEmployee = CashlinxDesktopSession.Instance.UserName;
            }
            catch (Exception)
            {
                return null;
            }
            return reportObject;
        }

        private void PrintAndStoreReport(int ticketNumber, string reportName, bool store, int numCopies)
        {
            var cds = GlobalDataAccessor.Instance.DesktopSession;
            var dA = GlobalDataAccessor.Instance.OracleDA;
            var cC = GlobalDataAccessor.Instance.CouchDBConnector;

            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                cds.LaserPrinter.IsValid)
            {
                if (FileLogger.Instance.IsLogInfo)
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, "CreateReportObject", "Printing {0} on printer {1}", reportName,
                                                   cds.LaserPrinter);
                }
                string errMsg =
                    PrintingUtilities.printDocument(
                        reportObject.ReportTempFileFullName,
                        cds.LaserPrinter.IPAddress,
                        cds.LaserPrinter.Port, numCopies);
                if (errMsg.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) == -1)
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot print {0} on {1}", reportName, cds.LaserPrinter);
                    }
                }
            }

            if (store)
            {
                var pDoc = new CouchDbUtils.PawnDocInfo();

                //Set document add calls
                pDoc.UseCurrentShopDateTime = true;
                pDoc.StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                pDoc.CustomerNumber = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber;
                pDoc.DocumentType = Document.DocTypeNames.PDF;
                pDoc.DocFileName = reportObject.ReportTempFileFullName;
                pDoc.TicketNumber = ticketNumber;
                long recNumL = 0L;
                string errText;
                if (!CouchDbUtils.AddPawnDocument(dA, cC, cds.UserName, ref pDoc, out errText))
                {
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                            "Could not store Layaway Contract in document storage: {0} - FileName: {1}", errText, reportObject.ReportTempFileFullName);
                    BasicExceptionHandler.Instance.AddException(
                        "Could not store Layaway Contract in document storage",
                        new ApplicationException("Could not store receipt in document storage: " + errText));
                }
            }
        }

        private void PrintAndStoreReport(int ticketNumber, string reportName)
        {
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            SecuredCouchConnector cC = GlobalDataAccessor.Instance.CouchDBConnector;

            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                cds.LaserPrinter.IsValid)
            {
                if (FileLogger.Instance.IsLogInfo)
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, "CreateReportObject", "Printing {0} on printer {1}", reportName,
                                                   cds.LaserPrinter);
                }
                string errMsg = 
                    PrintingUtilities.printDocument(
                        reportObject.ReportTempFileFullName, 
                        cds.LaserPrinter.IPAddress,
                        cds.LaserPrinter.Port, 2);
                if (errMsg.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) == -1)
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot print {0} on {1}", reportName, cds.LaserPrinter);
                    }
                }
            }

            var pDoc = new CouchDbUtils.PawnDocInfo();

            //Set document add calls
            pDoc.UseCurrentShopDateTime = true;
            pDoc.StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            pDoc.CustomerNumber = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber;
            pDoc.DocumentType = Document.DocTypeNames.PDF;
            pDoc.DocFileName = reportObject.ReportTempFileFullName;
            pDoc.TicketNumber = ticketNumber;
            long recNumL = 0L;
            string errText;
            if (!CouchDbUtils.AddPawnDocument(dA, cC, cds.UserName, ref pDoc, out errText))
            {
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                        "Could not store Layaway Contract in document storage: {0} - FileName: {1}", errText, reportObject.ReportTempFileFullName);
                BasicExceptionHandler.Instance.AddException(
                    "Could not store Layaway Contract in document storage",
                    new ApplicationException("Could not store receipt in document storage: " + errText));
            }
        }
        #endregion

        #region Public Methods
        public void GetItemDetailsReport(DataView theData, DataView thisItem, int rowNum, bool print, string since, string dob)
        {
            reportObject = GetReportObject("Inquiry Item Detail", (int)ReportIDs.InquiryItemDetail, "InquiryItemDetail", "InquiryItemDetail.PDF");
            reportObject.InquiryItemDetails_RowNumber = rowNum;
            reportObject.InquiryItemDetails_thisData = thisItem;
            reportObject.InquiryItemDetails_theData = theData;
            reportObject.InquiryItemDetails_DOB = dob;
            reportObject.InquiryItemDetails_Since = since;
            var itd = new ItemDetailReport(PdfLauncher.Instance);
            itd.ReportObject = reportObject;
            try
            {
                int ticketNumber = Convert.ToInt32(theData[0]["TICKET_NUMBER"]);
                if (print)
                {
                    itd.CreateReport();
                    PrintAndStoreReport(ticketNumber, "Inquiry Item Detail Report", false, 1);
                }
                else
                {
                    itd.CreateReport();
                }
            }
            catch (Exception e)
            {
                BasicExceptionHandler.Instance.AddException(
                    e.Message,
                    new ApplicationException("Could not generate Inquiry item Details Report: " + e.Message));
            }
        }

        public void GetInventoryChargeOffReport(InventoryChargeOffFields invFields)
        {
            //First get Report Object
            bool print = true;
            reportObject = GetReportObject("Inventory Charge Off", (int)ReportIDs.InventoryChargeOff, "InventoryChargeOff", "InventoryChargeOff.PDF");
            reportObject.InventoryChargeOffFields = invFields;
            InventoryChargeOffReport inv = new InventoryChargeOffReport(PdfLauncher.Instance);
            inv.ReportObject = reportObject;

            if (print)
            {
                inv.CreateReport(false);
                PrintAndStoreReport(Convert.ToInt32(invFields.ChargeOffNumber), "Inventory Charge Off");
            }
            else
            {
                inv.CreateReport(true);
            }
        }
        #endregion
    }
}
