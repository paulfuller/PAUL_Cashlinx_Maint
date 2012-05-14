using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common.Controllers.Application;
using Common.Libraries.Utility.Shared;
using NUnit.Framework;
using PawnTests.TestEnvironment;
using Reports;

namespace PawnTests
{
    [TestFixture]
    public class LostTicketAffidavitTests
    {
        private IndianaLostTicketAffidavit LostTicketAffidavit;
        private LostTicketAffidavitContext Context;

        [SetUp]
        public void Setup()
        {
            Context = new LostTicketAffidavitContext();
            LostTicketAffidavit = new IndianaLostTicketAffidavit(Context);

            Context.LoanDateMade = DateTime.Parse("03/14/2012 12:16 PM");

            Context.CustomerName = "WAGES, JOHN Q";
        }

        [Test]
        public void PrintSampleDocumentStore001401()
        {
            var ds = GlobalDataAccessor.Instance.DesktopSession;
            ds.CurrentSiteId = TestSiteIds.Store01401;

            Context.StoreName = ds.CurrentSiteId.StoreName;
            Context.StoreNumber = ds.CurrentSiteId.StoreNumber;
            Context.TicketNumber = 12345;
            Context.ReasonMissing = "LOST";
            Context.MerchandiseDescription = "DIGITAL CAMERA; MFGR INSIGNIA; MOD NS-DCC5SR09; S# 8J27CD; W/ . LENS;";

            Context.OutputPath = Path.Combine(Environment.CurrentDirectory, "IndianaLostTicketAffidavit.pdf");

            Assert.IsTrue(LostTicketAffidavit.Print());

            //var strReturnMessage = PrintingUtilities.printDocument(
            //                Context.OutputPath,
            //                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
            //                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 1);

            //Assert.IsTrue(strReturnMessage.Contains("SUCCESS"));
        }

        [Test]
        public void PrintMultipleItemDocumentStore001401()
        {
            var ds = GlobalDataAccessor.Instance.DesktopSession;
            ds.CurrentSiteId = TestSiteIds.Store01401;

            Context.StoreName = ds.CurrentSiteId.StoreName;
            Context.StoreNumber = ds.CurrentSiteId.StoreNumber;
            Context.TicketNumber = 12345;
            Context.ReasonMissing = "LOST";
            var description = new StringBuilder();
            description.AppendLine("MNS WATCH; MFGR CARAVELLE; S# C923034; MOD BULOVA; GOLD TONE; QUARTZ MVT; LEATHER BAND;");
            description.AppendLine("ROPE CHAIN; Y/G; 10 KT.; APPRX 4.0 GRM; ROPE; 21 IN.; BROKEN;");
            description.AppendLine("WED BAND; Y/G; 14 KT.; APPRX 4.8 GRM; MODERN STYLE; 6; RND SHP; DIA-TEST POS STN; @ .5 PTS EA.;");
            description.AppendLine("WED BAND; Y/G; 10 KT.; APPRX 3.5 GRM; MODERN STYLE; 3; RND SHP; DIA-TEST POS STN; @ 1 PTS EA.;");
            description.AppendLine("CHAIN W/PEND; W/G; 14 KT.; APPRX 14.2 GRM; ROPE; 20 IN.; CROSS DESIGN;");
            description.AppendLine("WED BAND; Y/G; 14 KT.; APPRX 3.3 GRM; MODERN STYLE; 11; RND SHP; DIA-TEST POS STN; @ 3 PTS EA.;");
            description.AppendLine("CHAIN W/DIA PEND; Y/G; 10 KT.; APPRX 2.6 GRM; BOX LINK; 22 IN.; HEART DESIGN; 10; BAG SHP; DIA-TEST POS STN; @ 1 PTS EA.;");
            description.AppendLine("2 PC WED SET; W/G; 14 KT.; APPRX 4.7 GRM; MODERN STYLE; 1; MARQ SHP; DIA-TEST POS STN; @ 20 PTS EA.; 6 ; RND ; DIA-TEST POS ; 3 ; CL2 GOOD ; FAINT TINT ;");
            description.AppendLine("2 PC WED SET; W/G; 14 KT.; APPRX 6.3 GRM; 20; RND SHP; DIA-TEST POS STN; @ 3 PTS EA.; 1; RND SHP; DIA-TEST POS STN; @ 20 PTS EA.; 2; RND SHP; DIA-TEST POS STN; @ 8 PTS EA.;");
            description.AppendLine("2 PC WED SET; Y/G; 14 KT.; APPRX 5.8 GRM; 6; RND SHP; DIA-TEST POS STN; @ 5 PTS EA.; 1 ; FANCY ; DIA-TEST POS ; 50 ; CL2 GOOD ; FAINT TINT ;");

            Context.MerchandiseDescription = description.ToString();

            Context.OutputPath = Path.Combine(Environment.CurrentDirectory, "IndianaLostTicketAffidavit.pdf");

            Assert.IsTrue(LostTicketAffidavit.Print());

            //var strReturnMessage = PrintingUtilities.printDocument(
            //                Context.OutputPath,
            //                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
            //                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 1);

            //Assert.IsTrue(strReturnMessage.Contains("SUCCESS"));
        }
    }
}
