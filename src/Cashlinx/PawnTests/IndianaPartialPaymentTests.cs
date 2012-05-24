using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Controllers.Application;
using Common.Libraries.Utility.Shared;
using NUnit.Framework;
using PawnTests.TestEnvironment;
using Reports;
using System.IO;

namespace PawnTests
{
    [TestFixture]
    class IndianaPartialPaymentTests
    {
        private PartialPayment_IN IndianaPartialPayment;
        private IndianaPartialPaymentContext DataContext;
        
        [SetUp]
        public void Setup()
        {
            var ds = GlobalDataAccessor.Instance.DesktopSession;
            ds.CurrentSiteId = TestSiteIds.Store01401;

            DataContext = new IndianaPartialPaymentContext {PrintDateTime = new DateTime(2012, 07, 13, 19, 50,00)};

        }
        
        [Test]
        public void PrintPartialPaymentReceiptToPDF()
        {
            DataContext.CustomerName = "WAGES, JOHN Q";
            DataContext.CustomerAddress = "100 OHIO STREET";
            DataContext.CustomerCity = "CINCINNATI";
            DataContext.CustomerState = "OH";
            DataContext.CustomerZip = "45239";
            DataContext.CustomerPhone = "123-555-5555";

            DataContext.StoreName = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
            DataContext.StoreAddress = GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress1;
            DataContext.StoreCity = GlobalDataAccessor.Instance.CurrentSiteId.StoreCityName;
            DataContext.StoreState = GlobalDataAccessor.Instance.CurrentSiteId.Alias;
            DataContext.StoreZip = GlobalDataAccessor.Instance.CurrentSiteId.StoreZipCode;
            DataContext.PrincipalReduction = 20;
            DataContext.EmployeeNumber = "000002";
            DataContext.FilePath = Path.Combine("C:\\Program Files\\Phase2App\\logs\\", "PartialPaymentReceipt" + DateTime.Now.ToString("mmddyyyyhhmmss") + ".pdf");

            DataContext.ItemDescription = "REVOLVER; MFGR COLT; MOD PEACEMAKER; DOUBLE ACTION; 40 CAL";
            DataContext.Interest = 7.59;
            DataContext.OtherCharges= 1.92;
            DataContext.ServiceFees = 4;
            DataContext.TicketNumber = 200215;
            IndianaPartialPayment = new PartialPayment_IN(DataContext);
            Assert.Inconclusive("Unknown what path to write to on server.");
            Assert.IsTrue(IndianaPartialPayment.Print());
            
            //var strReturnMessage = PrintingUtilities.printDocument(
            //    DataContext.FilePath,
            //    GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
            //    GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 1);

            //Assert.IsTrue(strReturnMessage.Contains("SUCCESS"));
        }

        [TearDown]
        public void Teardown()
        {
            var ds = GlobalDataAccessor.Instance.DesktopSession;
            ds.CurrentSiteId = TestSiteIds.Store00152;

        }
    }
}
