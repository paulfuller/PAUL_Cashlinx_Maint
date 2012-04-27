using Common.Controllers.Application;
using NUnit.Framework;
using PawnTests.TestEnvironment;

namespace PawnTests.BusinessRules
{
    [TestFixture]
    public class OklahomaBusinessRulesTest : BusinessRulesBaseTest
    {
        protected override void Setup()
        {
            GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId = TestSiteIds.Store00152;
        }

        protected override void Teardown()
        {
            GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId = TestSiteIds.Store00152;
        }

        [Test]
        public void AllowFutureInterestPayments()
        {
            Assert.IsFalse(BusinessRulesProcedures.AllowFutureInterestPayments(CurrentSiteId));
        }

        [Test]
        public void GetPFIMailerFee()
        {
            Assert.AreEqual(0M, BusinessRulesProcedures.GetPFIMailerFee(CurrentSiteId));
        }

        [Test]
        public void GetPFIEligibleDateAdjustmentDirection()
        {
            Assert.AreEqual("F", BusinessRulesProcedures.GetPFIEligibleDateAdjustmentDirection(CurrentSiteId));
        }

        [Test]
        public void GetSalesTaxRoundingAdjustment()
        {
            Assert.AreEqual(0M, BusinessRulesProcedures.GetSalesTaxRoundingAdjustment(CurrentSiteId));
        }
    }
}
