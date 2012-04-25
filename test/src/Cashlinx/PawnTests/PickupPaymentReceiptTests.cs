using System;
using System.IO;
using Common.Controllers.Application;
using Common.Libraries.Utility.Shared;
using NUnit.Framework;
using PawnTests.TestEnvironment;
using Reports;

namespace PawnTests
{
    [TestFixture]
    public class PickupPaymentReceiptTests
    {
        private PickupPaymentReceipt PickupPaymentReceipt;
        private PickupPaymentReceipt.PickupPaymentContext Context;

        [SetUp]
        public void Setup()
        {
            Context = new PickupPaymentReceipt.PickupPaymentContext();
            PickupPaymentReceipt = new PickupPaymentReceipt(Context, null);

            Context.Time = DateTime.Parse("03/14/2012 12:16 PM");
            Context.EmployeeNumber = "000002";

            Context.CustomerName = "WAGES, JOHN Q";
            Context.CustomerAddress = "100 OHIO STREET";
            Context.CustomerCityStateZip = "CINCINNATI, OH 45239";
            Context.CustomerPhone = "123-555-5555";
        }

        [Test]
        public void PrintBASourceDocumentStore00901()
        {
            var ds = GlobalDataAccessor.Instance.DesktopSession;
            ds.CurrentSiteId = TestSiteIds.Store00901;

            Context.StoreName = ds.CurrentSiteId.StoreName;
            Context.StoreNumber = ds.CurrentSiteId.StoreNumber;
            Context.StoreAddress = string.Format("{0}, {1} {2}", ds.CurrentSiteId.StoreCityName.Trim(), ds.CurrentSiteId.State.Trim(), ds.CurrentSiteId.StoreZipCode.Trim());

            Context.OutputPath = Path.Combine(Environment.CurrentDirectory, "PickupPaymentReceipt.pdf");
            Context.Ticket = new PickupPaymentReceipt.PickupPaymentTicketInfo
                                          {
                                              TicketNumber = 200215,
                                              CurrentPrincipalAmount = 100,
                                              Interest = 7.59M,
                                              PfiMailerSent = true,
                                              ServiceFee = 4M,
                                              LateFee = 1.92M,
                                              LostTicketFee = 0M
                                          };

            Assert.AreEqual(0M, PickupPaymentReceipt.OtherFees);
            Assert.AreEqual(0M, PickupPaymentReceipt.PfiMailerFee);
            Assert.AreEqual(0M, PickupPaymentReceipt.TicketTotal);

            Assert.IsTrue(PickupPaymentReceipt.Print());

            Assert.AreEqual(3.92M, PickupPaymentReceipt.OtherFees);
            Assert.AreEqual(2M, PickupPaymentReceipt.PfiMailerFee);
            Assert.AreEqual(115.51M, PickupPaymentReceipt.TicketTotal);

            //var strReturnMessage = PrintingUtilities.printDocument(
            //                Context.OutputPath,
            //                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
            //                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 1);

            //Assert.IsTrue(strReturnMessage.Contains("SUCCESS"));
        }

        [Test]
        public void PrintBASourceDocumentStore00152()
        {
            var ds = GlobalDataAccessor.Instance.DesktopSession;
            ds.CurrentSiteId = TestSiteIds.Store00152; // this store does not charge for a pfi mailer fee so testing to make sure the values are being retrieved properly

            Context.StoreName = ds.CurrentSiteId.StoreName;
            Context.StoreNumber = ds.CurrentSiteId.StoreNumber;
            Context.StoreAddress = string.Format("{0}, {1} {2}", ds.CurrentSiteId.StoreCityName.Trim(), ds.CurrentSiteId.State.Trim(), ds.CurrentSiteId.StoreZipCode.Trim());

            Context.OutputPath = Path.Combine(Environment.CurrentDirectory, "PickupPaymentReceipt.pdf");
            Context.Ticket = new PickupPaymentReceipt.PickupPaymentTicketInfo
            {
                TicketNumber = 200215,
                CurrentPrincipalAmount = 100,
                Interest = 7.59M,
                PfiMailerSent = false,
                ServiceFee = 4M,
                LateFee = 1.92M,
                LostTicketFee = 0M
            };

            Assert.AreEqual(0M, PickupPaymentReceipt.OtherFees);
            Assert.AreEqual(0M, PickupPaymentReceipt.PfiMailerFee);
            Assert.AreEqual(0M, PickupPaymentReceipt.TicketTotal);

            Assert.IsTrue(PickupPaymentReceipt.Print());

            Assert.AreEqual(1.92M, PickupPaymentReceipt.OtherFees);
            Assert.AreEqual(0M, PickupPaymentReceipt.PfiMailerFee);
            Assert.AreEqual(113.51M, PickupPaymentReceipt.TicketTotal);
        }

        [TearDown]
        public void Teardown()
        {
            GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId = TestSiteIds.Store00152;
        }
    }
}
