using System;
using System.Collections.Generic;
using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Database.Couch;
using Common.Controllers.Database.Couch.Impl;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Reports.Layaway;
using Document = Common.Libraries.Objects.Doc.Document;

namespace Pawn.Forms.Report
{
    class LayawayCreateReportObject
    {
        #region Private Fields
        LayawayReportObject reportObject = null;
        #endregion

        #region Private Methods
        private LayawayReportObject.LayawayHistoryAndScheduleMain GetHistoryAndScheduleReportData(LayawayPaymentHistoryBuilder layawayPaymentHistoryBuilder, LayawayVO layaway)
        {
            var main = new LayawayReportObject.LayawayHistoryAndScheduleMain();
            var historyList = new List<LayawayReportObject.LayawaySchedule>();

            main.AmountOutstanding = layawayPaymentHistoryBuilder.GetBalanceOwed();
            foreach (var history in layawayPaymentHistoryBuilder.ScheduledPayments)
            {
                var paymentHistory = new LayawayReportObject.LayawaySchedule();
                var detailsList = new List<LayawayReportObject.LayawayScheduleDetails>();
                var historyDetail = new LayawayReportObject.LayawayScheduleDetails();
                if (history.Payments.Count == 0)
                {
                    historyDetail.PaymentDateDue = history.PaymentDueDate;
                    historyDetail.PaymentAmountDue = history.PaymentAmountDue;
                    detailsList.Add(historyDetail);
                }
                else
                {
                    for (int i = 0; i < history.Payments.Count; i++)
                    {
                        if (i > 0)
                             historyDetail = new LayawayReportObject.LayawayScheduleDetails();

                        var paymentInfo = history.Payments.OrderBy(p => p.PaymentMadeOn).ToArray()[i];
                        if (i == 0)
                        {
                            historyDetail.PaymentDateDue = history.PaymentDueDate;
                            historyDetail.PaymentAmountDue = history.PaymentAmountDue;
                        }
                        historyDetail.PaymentMadeOn = paymentInfo.PaymentMadeOn;
                        historyDetail.PaymentAmountMade = paymentInfo.PaymentAmountMade;
                        historyDetail.BalanceDue = paymentInfo.BalanceDue;
                        //historyDetail.PaymentType = paymentInfo.PaymentType;
                        historyDetail.ReceiptNumber = paymentInfo.ReceiptNumber;
                        historyDetail.Status = paymentInfo.Status;
                        detailsList.Add(historyDetail);
                    }
                }
                paymentHistory.LayawayScheduleDetailsList = detailsList;
                historyList.Add(paymentHistory);
            }
            //layawayPaymentHistoryBuilder.
            main.LayawayScheduleList = historyList;
            main.Layaway = layaway;
            return main;
        }

        private LayawayReportObject GetReportObject(string reportTitle, int reportNumber, string pathVariable, string formName, LayawayVO layaway)
        {
            var rptObj = new LayawayReportObject();
            try
            {
                rptObj.ReportTitle = reportTitle;
                rptObj.ReportNumber = reportNumber;
                rptObj.FormName = formName;
                //reportObject.LayawayNumber = 101;
                rptObj.LayawayNumber = layaway.TicketNumber;
                rptObj.ReportTempFileFullName = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\" + pathVariable + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";

                rptObj.ReportTempFile = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;
                rptObj.ReportStore = GlobalDataAccessor.Instance.CurrentSiteId.StoreName + "-" + GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber.ToString();
                rptObj.ReportStoreDesc1 = GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress1;//"CASH AMERICA PAWN OF DFW";
                rptObj.ReportStoreDesc2 = GlobalDataAccessor.Instance.CurrentSiteId.StoreCityName + ", " + GlobalDataAccessor.Instance.CurrentSiteId.State + ", " + GlobalDataAccessor.Instance.CurrentSiteId.StoreZipCode;
                rptObj.CustomerName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName + " " + GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.MiddleInitial + " " + GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName;
                rptObj.ReportError = string.Empty;
                rptObj.ReportErrorLevel = 0;
                rptObj.ReportEmployee = GlobalDataAccessor.Instance.DesktopSession.UserName;
                SetCustomerInfo(rptObj, layaway);
            }
            catch (Exception eX)
            {
                return null;
            }
            return rptObj;
        }

        private LayawayReportObject GetReportObject(string reportTitle, int reportNumber, string pathVariable, string formName)
        {
            var layawayReportObject = new LayawayReportObject();
            try
            {
                layawayReportObject.ReportTitle = reportTitle;
                layawayReportObject.ReportNumber = reportNumber;
                layawayReportObject.FormName = formName;
                //reportObject.LayawayNumber = 101;
                layawayReportObject.ReportTempFileFullName = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\" + pathVariable + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";

                layawayReportObject.ReportTempFile = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;
                layawayReportObject.ReportStore = GlobalDataAccessor.Instance.CurrentSiteId.StoreName + "-" + GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber.ToString();
                layawayReportObject.ReportStoreDesc1 = GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress1;//"CASH AMERICA PAWN OF DFW";
                layawayReportObject.ReportStoreDesc2 = GlobalDataAccessor.Instance.CurrentSiteId.StoreCityName + ", " + GlobalDataAccessor.Instance.CurrentSiteId.State + ", " + GlobalDataAccessor.Instance.CurrentSiteId.StoreZipCode;
                layawayReportObject.CustomerName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName + " " + GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.MiddleInitial + " " + GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName;
                layawayReportObject.ReportError = string.Empty;
                layawayReportObject.ReportErrorLevel = 0;
                layawayReportObject.ReportEmployee = GlobalDataAccessor.Instance.DesktopSession.UserName;
            }
            catch (Exception)
            {
                return null;
            }
            return layawayReportObject;
        }

        private void SetLayawaysCustomerInfo(List<LayawayVO> layaways)
        {
            foreach (var layaway in layaways)
            {
                if(!string.IsNullOrEmpty(layaway.CustomerNumber))
                    layaway.CustomerInfo = CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, layaway.CustomerNumber);
            }
        }

        private void SetCustomerInfo(LayawayReportObject rptObj, LayawayVO layaway)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null)
            {
                rptObj.CustomerName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName + ", " + GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName;
                rptObj.CustomerFirstName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName;
                rptObj.CustomerLastName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName;
                ContactVO cVo = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.getPrimaryContact();
                if (cVo != null)
                {
                    rptObj.ContactNumber = Commons.Format10And11CharacterPhoneNumberForUI(cVo.ContactAreaCode + cVo.ContactPhoneNumber);
                }
                else
                    rptObj.ContactNumber = "";
            }
            else
            {
                CustomerVO customerObject = CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, layaway.CustomerNumber);
                rptObj.CustomerFirstName = customerObject.FirstName;
                rptObj.CustomerLastName = customerObject.LastName;
                ContactVO cVo = customerObject.getPrimaryContact();
                if (cVo != null)
                {
                    rptObj.ContactNumber = Commons.Format10And11CharacterPhoneNumberForUI(cVo.ContactAreaCode + cVo.ContactPhoneNumber);
                }
                else
                    rptObj.ContactNumber = "";

            }
        }

        private LayawayReportObject GetReportObject(string reportTitle, int reportNumber, int layawayNumber, string pathVariable, string formName, LayawayVO layaway)
        {
            var rptObj = new LayawayReportObject();
            try
            {
                rptObj.ReportTitle = reportTitle;
                rptObj.ReportNumber = reportNumber;
                rptObj.FormName = formName;
                rptObj.LayawayNumber = layawayNumber;
                rptObj.ReportTempFileFullName = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\" + pathVariable + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";
                rptObj.ReportTempFile = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;
                rptObj.ReportStore = GlobalDataAccessor.Instance.CurrentSiteId.StoreName + "-" + GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber.ToString();
                rptObj.ReportStoreDesc1 = GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress1;//"CASH AMERICA PAWN OF DFW";
                rptObj.ReportStoreDesc2 = GlobalDataAccessor.Instance.CurrentSiteId.State + ", " + GlobalDataAccessor.Instance.CurrentSiteId.StoreZipCode;
                rptObj.CustomerName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName + ", " + GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName;
                rptObj.ReportError = string.Empty;
                rptObj.ReportErrorLevel = 0;
                rptObj.ReportEmployee = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.EmployeeNumber;
                SetCustomerInfo(rptObj, layaway);
            }
            catch (Exception)
            {
                return null;
            }
            return rptObj;
        }

        private void PrintAndStoreReport(LayawayVO layaway)
        {
            var cds = GlobalDataAccessor.Instance.DesktopSession;
            var dA = GlobalDataAccessor.Instance.OracleDA;
            var cC = GlobalDataAccessor.Instance.CouchDBConnector;

            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
            {
                if (FileLogger.Instance.IsLogInfo)
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, "LayawayCreateReportObject", "Printing layaway contract on printer {0}",
                                                   GlobalDataAccessor.Instance.DesktopSession.LaserPrinter);
                }
                
                string errMsg = PrintingUtilities.printDocument(
                    reportObject.ReportTempFileFullName, 
                    GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                    GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 1);

                if (errMsg.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) == -1)
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot print Layaway Contract on {0}", GlobalDataAccessor.Instance.DesktopSession.LaserPrinter);
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
            //pDoc.TicketNumber = cds.ActiveCustomer.c
            //pDoc.DocumentSearchType = CouchDbUtils.DocSearchType.STORE_TICKET;
            pDoc.TicketNumber = layaway.TicketNumber;
            long recNumL = 0L;
            //if (long.TryParse(receiptDetailsVO.ReceiptNumber, out recNumL))
           // {
           //     pDoc.ReceiptNumber = recNumL;
           // }

            //Add this document to the pawn document registry and document storage
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
        public void CreateHistoryAndScheduleReport(LayawayPaymentHistoryBuilder layawayPaymentHistoryBuilder, LayawayVO layaway)
        {

            //First get Report Object
            reportObject = GetReportObject("Layaway History And Schedule", (int)LayawayReportIDs.LayawayHistoryAndSchedule, "LayawayHistoryAndSchedule", "LayawayHistoryAndSchedule.PDF");

            //then load the Data to be displayed into the reportObject
            reportObject.LayawayHistoryAndScheduleMainData = GetHistoryAndScheduleReportData(layawayPaymentHistoryBuilder, layaway);
            reportObject.CurrentLayaway = layaway;
            //with the data loaded, now call to create the report and pass the reportObject with the loaded data
            if (reportObject.LayawayHistoryAndScheduleMainData.LayawayScheduleList.Count > 0)
                LayawayReportProcessing.DoReport(reportObject, true, PdfLauncher.Instance);
            //PrintAndStoreReport(layaway);
        }

        public void GetContractReport()
        {
            //First get Report Object
            reportObject = GetReportObject("Layaway Contract", (int)LayawayReportIDs.LayawayContract, "LayawayContract", "LayawayContract.PDF");
            LayawayReportProcessing.DoReport(reportObject, true, PdfLauncher.Instance);
        }


        public void GetLayawayForfeitPickingSlip(List<LayawayVO> layaways)
        {
            foreach (LayawayVO layaway in layaways)
            {
                string fileName = "LayawayForfeitPickingSlip_" + layaway.TicketNumber + ".PDF";
                reportObject = GetReportObject("Layaway Forfeit Picking Slip", (int)LayawayReportIDs.LayawayForfeitPickingSlip, "LayawayForfeitPickingSlip", fileName, layaway);
                reportObject.LayawayPickingSlip = layaway;
                LayawayReportProcessing.DoReport(reportObject, false, PdfLauncher.Instance);
                //foreach (LayawayVO layaway in layaways)
                PrintAndStoreReport(layaway);
            }
        }

        public void GetForfeitedLayawaysListings(List<LayawayVO> layaways, decimal  restockingFee)
        {
            SetLayawaysCustomerInfo(layaways);
            reportObject = GetReportObject("Forfeited Layaways Listing", (int)LayawayReportIDs.ForfeitedLayawaysListing, "ForfeitedLayawaysListings", "ForfeitedLayawaysListings.PDF", layaways[0]);
            reportObject.ForefeitedLayawaysListingsList = layaways;
            reportObject.RestockingFee = restockingFee;
            LayawayReportProcessing.DoReport(reportObject, false, PdfLauncher.Instance);
            PrintAndStoreReport(layaways[0]);
        }

        public void GetLayawayTerminationPickingSlip(List<LayawayVO> layaways, decimal restockingFee)
        {
            foreach (LayawayVO layaway in layaways)
            {
                string fileName = "LayawayTerminationPickingSlip_" + layaway.TicketNumber + ".PDF";
                reportObject = GetReportObject("Layaway Termination Picking Slip", (int)LayawayReportIDs.TerminatedLayawaysPickingSlip, "LayawayTerminationPickingSlip", "LayawayTerminationPickingSlip.PDF", layaways[0]);
                reportObject.TerminatedLayaway = layaway;
                reportObject.RestockingFee = restockingFee;
                LayawayReportProcessing.DoReport(reportObject, false, PdfLauncher.Instance);
                PrintAndStoreReport(layaway);
            }
        }

        public void GetLayawayTerminatedListings(List<LayawayVO> layaways, decimal restockingFee)
        {
            SetLayawaysCustomerInfo(layaways);
            reportObject = GetReportObject("Terminated Layaways Listing", (int)LayawayReportIDs.TerminatedLayawaysListing, "TerminatedLayawaysListings", "TerminatedLayawaysListings.PDF", layaways[0]);
            reportObject.TerminatedLayawaysListingsList = layaways;
            reportObject.RestockingFee = restockingFee;
            LayawayReportProcessing.DoReport(reportObject, false, PdfLauncher.Instance);
            //foreach (LayawayVO layaway in layaways)
            PrintAndStoreReport(layaways[0]);
        }
        #endregion
    }
}
