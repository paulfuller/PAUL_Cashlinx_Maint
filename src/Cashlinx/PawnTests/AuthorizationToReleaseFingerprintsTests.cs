using System;
using System.IO;
using System.Runtime.Remoting.Contexts;
using Common.Controllers.Application;
using Common.Libraries.Utility.Shared;
using NUnit.Framework;
using PawnTests.TestEnvironment;
using Reports;

namespace PawnTests
{
    [TestFixture]
    public class AuthorizationToReleaseFingerprintsTest
    {
        private AuthorizationToReleaseFingerprints _authorizationToReleaseFingerprints;
        private AuthorizationToReleaseFingerprints.ReleaseFingerprintsContext _context;

        [SetUp]
        public void Setup()
        {
            _context = new AuthorizationToReleaseFingerprints.ReleaseFingerprintsContext();

            _context.EmployeeNumber = "user2";
            _context.ShopName = "Test Shop";
            _context.ShopAddress = "123 Test Street";
            _context.ShopCity = "Test City";
            _context.ShopState = "TX";
            _context.ShopZipCode = "11111";
            _context.CustomerName = "Bob Testcustomer";
            _context.CustomerAddress1 = "1122 Customer Street";
            _context.CustomerCity = "Customer Test City";
            _context.CustomerState = "TX";
            _context.CustomerZipCode = "22222";
            _context.TicketNumber = 831;
            _context.Agency = "FWPD";
            _context.CaseNumber = "5555";
            _context.SubpoenaNumber = "P122";
            _context.OfficerName = "Joe Friday";
            _context.BadgeNumber = "714";

            string currDate = ShopDateTime.Instance.ShopDate.FormatDate();
            string currDateTime = currDate + " " + ShopDateTime.Instance.ShopTime.ToString();
            _context.TransactionDate = currDateTime;

            _authorizationToReleaseFingerprints = new AuthorizationToReleaseFingerprints(12345, ProductType.BUY,
                                                                                         _context, null);

        }

        [Test]
        public void PrintAuthorizationToReleaseFingerprintsStore00901()
        {
            var ds = GlobalDataAccessor.Instance.DesktopSession;
            ds.CurrentSiteId = TestSiteIds.Store00901;

            _context.OutputPath = Path.Combine(Environment.CurrentDirectory, "AuthorizationToReleaseFingerprints.pdf");

            Assert.IsTrue(_authorizationToReleaseFingerprints.Print());

            //Uncomment to print document
            //var strReturnMessage = PrintingUtilities.printDocument(
            //                _context.OutputPath,
            //                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
            //                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 2);

            //Assert.IsTrue(strReturnMessage.Contains("SUCCESS"));
        }

        [TearDown]
        public void Teardown()
        {
            GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId = TestSiteIds.Store00152;
        }
    }
}
