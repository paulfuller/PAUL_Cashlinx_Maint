using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Audit.Forms;
using Audit.Logic;
using Common.Controllers.Application;
using Common.Controllers.Database;
using Common.Controllers.Database.Couch;
using Common.Controllers.Database.Couch.Impl;
using Common.Controllers.Database.Oracle;
using Common.Controllers.Security;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Document = Common.Libraries.Objects.Doc.Document;

namespace Audit.Reports
{
    public class CallAuditReport : AuditWindowBase
    {
        #region Private fields
        private string _reportDirectoryPath = string.Empty;
        #endregion

        #region Public Properties
        public string ReportDirectoryPath 
        {
            get
            {
                return _reportDirectoryPath;
            }
            set
            {
                _reportDirectoryPath = value;
            }
        }
        #endregion

        #region Constructors
        public CallAuditReport(string reportDirectoryPath)
        {
            if(string.IsNullOrEmpty(reportDirectoryPath))
                _reportDirectoryPath = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;
        }

        public CallAuditReport()
        {
             _reportDirectoryPath = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;
        }
        #endregion

        #region Public Methods
        public void GetPreAuditReport(bool store, bool display)
        {
            var reportObject = new ReportObject();
            DataSet outputDataSet;
            reportObject.ReportTempFile = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;
            //reportObject.ReportTempFileFullName = "PreAuditReport_" + DateTime.Now.Ticks + ".pdf";
            reportObject.CreateTemporaryFullName("PreAuditReport_");
            string _storeName = ADS.ActiveAudit.StoreName;

            reportObject.ReportStore = ADS.ActiveAudit.StoreNumber;
            //reportObject.ReportParms.Add(DateTime.Now);

            bool hasData = (ADS.ActiveAudit.PreAuditData != null && ADS.ActiveAudit.PreAuditData.Tables.Count == 3);

            if (hasData)
                outputDataSet = ADS.ActiveAudit.PreAuditData;
            else
                AuditReportsProcedures.GetPreAuditData(ADS.ActiveAudit.StoreNumber, ADS.ActiveAudit.AuditId, out outputDataSet, ref _storeName);

            if (hasData)
            {
                // Create Report
                reportObject.ReportTitle = "Full Pre-Audit";
                reportObject.ReportStoreDesc = string.Format("{0} \n #{1}", _storeName, ADS.ActiveAudit.StoreNumber);
                var preReport = new PreAuditReport("Full Pre-Audit");
                preReport.reportObject = reportObject;
                if (preReport.CreateReport(outputDataSet))
                {
                    //string fullPath = System.IO.Path.GetFullPath(preReport.reportObject.ReportTempFileFullName);
                    if(display)
                        AuditDesktopSession.ShowPDFFile(reportObject.ReportTempFileFullName, false);
                }
                if (store)
                    PrintAndStoreReport(reportObject, "Pre-Audit");
            }
        }

        public void SetInventorySummaryCACCData()
        {
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            List<AuditReportsObject.InventorySummaryChargeOffsField> fieldsCacc = new List<AuditReportsObject.InventorySummaryChargeOffsField>();
            AuditReportsProcedures.GetInventorySummaryReportFieldsCACC(ref fieldsCacc, ADS.ActiveAudit.StoreNumber, ADS.ActiveAudit.AuditId, dataContext);
            ADS.InventorySummaryReportFieldsCACC = fieldsCacc;
        }

        public void GetInventorySummaryReport()
        {
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            List<AuditReportsObject.InventorySummaryChargeOffsField> fields = new List<AuditReportsObject.InventorySummaryChargeOffsField>();
            //List<AuditReportsObject.InventorySummaryChargeOffsField> fieldsCacc = new List<AuditReportsObject.InventorySummaryChargeOffsField>();
            List<AuditReportsObject.InventorySummaryHistoryField> invHistoryfields = new List<AuditReportsObject.InventorySummaryHistoryField>();
            StringBuilder employeesPresent = new StringBuilder();
            StringBuilder termEmployees = new StringBuilder();
            //AuditReportsProcedures.GetInventorySummaryReportFields(ADS.ActiveAudit.StoreNumber, ADS.ActiveAudit.AuditId, dataContext);
            AuditReportsProcedures.GetInventorySummaryReportFields(ref fields, ref invHistoryfields, ref employeesPresent, ref termEmployees, ADS.ActiveAudit.StoreNumber, ADS.ActiveAudit.AuditId, dataContext);
            //AuditReportsProcedures.GetInventorySummaryReportFieldsCACC(ref fieldsCacc, ADS.ActiveAudit.StoreNumber, ADS.ActiveAudit.AuditId, dataContext);
            List<AuditReportsObject.InventoryQuestion> listQuestions = new List<AuditReportsObject.InventoryQuestion>();
            foreach (InventoryQuestion question in ADS.InventoryQuestionsWithResponses)
            {
                AuditReportsObject.InventoryQuestion roQuestion = new AuditReportsObject.InventoryQuestion()
                {
                    Question = question.Question,
                    QuestionNumber = question.QuestionNumber,
                    Response = question.Response,
                };
                listQuestions.Add(roQuestion);
            }
            var newListQuestions = listQuestions.OrderBy(x => x.QuestionNumber);
            AuditReportsObject ro = new AuditReportsObject();
            ro.InventoryQuestionsAdditionalComments = ADS.InventoryQuestionsAdditionalComments;
            ro.ReportNumber = 22;
            ro.ReportStore = ADS.ActiveAudit.StoreName;
            //ro.StoreNumber = ADS.ActiveAudit.StoreNumber;
            ro.StoreNumber = ADS.ActiveAudit.StoreNumber;
            ro.InventoryAuditDate = DateTime.Today;
            ro.ListInventorySummaryChargeOffsField = fields;
            ro.ListInventorySummaryChargeOffsFieldCACC = ADS.InventorySummaryReportFieldsCACC;
            ro.ListInventorySummaryHistoryField = invHistoryfields;
            ro.ListInventoryQuestions = newListQuestions.ToList();
            ro.StringbuilderInvSummEmployeesPresent = employeesPresent;
            ro.StringbuilderInvSummTermEmployees = termEmployees;
            ro.ActiveAudit = ADS.ReportActiveAudit;
            ro.ReportTempFile = ReportDirectoryPath;
            InventorySummaryReport invRpt = new InventorySummaryReport();
            invRpt.ReportObject = ro;
            invRpt.CreateReport();
            PrintAndStoreReport(ro, "Inventory Summary");
        }

        public void GetTrakkerUploadReport(bool store, bool display)
        {
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            List<AuditReportsObject.TrakkerUploadReportSinceLastInventory> fields = new List<AuditReportsObject.TrakkerUploadReportSinceLastInventory>();
            AuditReportsProcedures.GetTrakkerUploadReportFields(ref fields, ADS.ActiveAudit.StoreNumber, ADS.ActiveAudit.AuditId, dataContext);
            AuditReportsObject ro = new AuditReportsObject();
            ro.ReportNumber = 22;
            ro.ReportStore = ADS.ActiveAudit.StoreName;
            ro.ReportTitle = "Trakker Upload Report";
            ro.StoreNumber = ADS.ActiveAudit.StoreNumber;
            ro.InventoryAuditDate = DateTime.Today;
            ro.ListTrakkerUploadReportField = fields;
            ro.ActiveAudit = ADS.ActiveAudit;
            ro.ReportTempFile = ReportDirectoryPath;
            TrakkerUploadReport trpt = new TrakkerUploadReport();
            trpt.ReportObject = ro;
            trpt.CreateReport();
            if(store)
                PrintAndStoreReport(ro, ro.ReportTitle);
            if(display)
                AuditDesktopSession.ShowPDFFile(ro.ReportTempFileFullName, false);
        }

   
        public void GetPostAuditReport()
        {
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            List<AuditReportsObject.PostAuditField> postAuditfields = new List<AuditReportsObject.PostAuditField>();
            List<AuditReportsObject.PostAuditInventoryTotalsField> postAuditInventoryTotalsfields = new List<AuditReportsObject.PostAuditInventoryTotalsField>();
            AuditReportsObject.PostAuditTempICNReconciliationField postAuditAdjustmentsfields = new AuditReportsObject.PostAuditTempICNReconciliationField();
            AuditReportsProcedures.GetPostAuditReportFields(ref postAuditfields, ref postAuditInventoryTotalsfields, ref postAuditAdjustmentsfields, ADS.ActiveAudit.StoreNumber, ADS.ActiveAudit.AuditId, dataContext);
            AuditReportsObject ro = new AuditReportsObject();
            ro.ReportNumber = 22;
            ro.ReportStore = ADS.ActiveAudit.StoreName;
            ro.ReportTitle = "Post Audit";
            ro.StoreNumber = ADS.ActiveAudit.StoreNumber;
            ro.InventoryAuditDate = DateTime.Today;
           
            ro.ListPostAuditField = postAuditfields;
            ro.ListPostAuditInventoryTotalsField = postAuditInventoryTotalsfields;
            ro.PostAuditTempICNReconciliation = postAuditAdjustmentsfields;
            ADS.ReportActiveAudit.DateCompleted = DateTime.Now;
            ro.ActiveAudit = ADS.ReportActiveAudit;
            ro.ReportTempFile = ReportDirectoryPath;
            PostAuditReport rpt = new PostAuditReport();
            rpt.ReportObject = ro;
            rpt.CreateReport();
            PrintAndStoreReport(ro, ro.ReportTitle);
        }

        public void PrintAndStoreReport(ReportObject ro, string report)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            SecuredCouchConnector cC = GlobalDataAccessor.Instance.CouchDBConnector;
            try
            {
                if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                    ADS.LaserPrinter.IsValid)
                {
                    if (FileLogger.Instance.IsLogInfo)
                    {
                        FileLogger.Instance.logMessage(LogLevel.INFO, "CallAuditReport", "Printing " + report + " on printer {0}",
                                                       ADS.LaserPrinter);
                    }

                    string errMsg = PrintingUtilities.printDocument(
                        ro.ReportTempFileFullName,
                        ADS.LaserPrinter.IPAddress,
                        ADS.LaserPrinter.Port, 1);

                    if (errMsg.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) == -1)
                    {
                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot print " + report + " on {0}", ADS.LaserPrinter);
                        }
                    }
                }

                var pDoc = new CouchDbUtils.PawnDocInfo();

                //Set document add calls
                pDoc.UseCurrentShopDateTime = true;
                pDoc.StoreNumber = ADS.ActiveAudit.StoreNumber;
                pDoc.CustomerNumber = "0";
                pDoc.DocumentType = Document.DocTypeNames.PDF;
                pDoc.DocFileName = ro.ReportTempFileFullName;
                //pDoc.TicketNumber = cds.ActiveCustomer.c
                //pDoc.DocumentSearchType = CouchDbUtils.DocSearchType.STORAGE.ToString();
                pDoc.TicketNumber = ADS.ActiveAudit.AuditId;
                pDoc.AuxInfo = report;
                long recNumL = 0L;
                //if (long.TryParse(receiptDetailsVO.ReceiptNumber, out recNumL))
                // {
                //     pDoc.ReceiptNumber = recNumL;
                // }

                //Add this document to the pawn document registry and document storage
                string errText;
                if (!CouchDbUtils.AddPawnDocument(dA, cC, ADS.UserName, ref pDoc, out errText))
                {
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                            "Could not store " + report + " in document storage: {0} - FileName: {1}", errText, ro.ReportTempFileFullName);
                    BasicExceptionHandler.Instance.AddException(
                        "Could not store " + report + " in document storage",
                        new ApplicationException("Could not store " + report + " in document storage: " + errText));
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void PrintAndStoreReport(AuditReportsObject ro, string report)
        {
            OracleDataAccessor dA = GlobalDataAccessor.Instance.OracleDA;
            SecuredCouchConnector cC = GlobalDataAccessor.Instance.CouchDBConnector;
            try
            {
                if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                    ADS.LaserPrinter.IsValid)
                {
                    if (FileLogger.Instance.IsLogInfo)
                    {
                        FileLogger.Instance.logMessage(LogLevel.INFO, "CallAuditReport", "Printing " + report + " on printer {0}",
                                                       ADS.LaserPrinter);
                    }

                    string errMsg = PrintingUtilities.printDocument(
                        ro.ReportTempFileFullName,
                        ADS.LaserPrinter.IPAddress,
                        ADS.LaserPrinter.Port, 1);

                    if (errMsg.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) == -1)
                    {
                        if (FileLogger.Instance.IsLogError)
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot print " + report + " on {0}", ADS.LaserPrinter);
                        }
                    }
                }

                var pDoc = new CouchDbUtils.PawnDocInfo();

                //Set document add calls
                pDoc.UseCurrentShopDateTime = true;
                pDoc.StoreNumber = ADS.ActiveAudit.StoreNumber;
                pDoc.CustomerNumber = "0";
                pDoc.DocumentType = Document.DocTypeNames.PDF;
                pDoc.DocFileName = ro.ReportTempFileFullName;
                pDoc.TicketNumber = ADS.ActiveAudit.AuditId;
                pDoc.AuxInfo = report;
                //pDoc.DocumentSearchType = CouchDbUtils.DocSearchType.STORE_TICKET;
                //pDoc.TicketNumber = layaway.TicketNumber;
                long recNumL = 0L;
                //if (long.TryParse(receiptDetailsVO.ReceiptNumber, out recNumL))
                // {
                //     pDoc.ReceiptNumber = recNumL;
                // }

                //Add this document to the pawn document registry and document storage
                string errText;
                if (!CouchDbUtils.AddPawnDocument(dA, cC, ADS.UserName, ref pDoc, out errText))
                {
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                            "Could not store " + report + " in document storage: {0} - FileName: {1}", errText, ro.ReportTempFileFullName);
                    BasicExceptionHandler.Instance.AddException(
                        "Could not store " + report + " in document storage",
                        new ApplicationException("Could not store " + report + " in document storage: " + errText));
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
