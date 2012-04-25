using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using NUnit.Framework;
using PawnTests.TestEnvironment;
using Reports;

namespace PawnTests
{
    [TestFixture]
    public class PFIMailerDocumentTests
    {
        private PFIMailerDocument pfiMailerDocument { get; set; }
        private ReportObject reportObject { get; set; }
        private ReportObject.PFIMailer pfiMailer;

        [SetUp]
        public void Setup()
        {
            pfiMailerDocument = new PFIMailerDocument(PdfLauncher.Instance);
            reportObject = new ReportObject
            {
                ReportTempFileFullName = Path.Combine(Environment.CurrentDirectory, "PFIMailer.pdf")
            };
            pfiMailerDocument.ReportObject = reportObject;
        }

        [Test]
        public void PrintBASample()
        {
            var ds = GlobalDataAccessor.Instance.DesktopSession;
            ds.CurrentSiteId = TestSiteIds.Store00901;

            pfiMailer.customerAddress = "100 OHIO STREET";
            pfiMailer.customerCity = "CINCINNATI";
            pfiMailer.customerId = "XXXXXXXX";
            pfiMailer.customerName = "WAGES, JOHN Q";
            pfiMailer.customerNumber = 99999;
            pfiMailer.customerState = "OH";
            pfiMailer.customerZipCode = "45239";
            pfiMailer.pfiEligibleDate = DateTime.Parse("03/19/2012");
            pfiMailer.pfiMailerFee = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetPFIMailerFee(
                        GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId);
            pfiMailer.storeAddress = ds.CurrentSiteId.StoreAddress1;
            pfiMailer.storeCity = ds.CurrentSiteId.StoreCityName;
            pfiMailer.storeName = ds.CurrentSiteId.StoreName;
            pfiMailer.storeNumber = 0;
            pfiMailer.storePhone = ds.CurrentSiteId.StorePhoneNo;
            pfiMailer.storeState = ds.CurrentSiteId.State;
            pfiMailer.storeZipCode = ds.CurrentSiteId.StoreZipCode;
            pfiMailer.ticketNumber = 287549;

            pfiMailerDocument.CreateReport(pfiMailer);

            //Uncomment to print document
            //var strReturnMessage =
            //    PrintingUtilities.printDocument(
            //        reportObject.ReportTempFileFullName,
            //        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
            //        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 1);

            //Assert.IsTrue(strReturnMessage.Contains("SUCCESS"));
        }

        [TearDown]
        public void Teardown()
        {
            GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId = TestSiteIds.Store00152;
        }

    }
}
