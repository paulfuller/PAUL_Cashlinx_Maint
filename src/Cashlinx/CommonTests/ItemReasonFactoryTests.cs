using System.Collections.Generic;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Shared;
using NUnit.Framework;

namespace CommonTests
{
    [TestFixture]
    public class ItemReasonFactoryTests
    {
        private ItemReasonFactory ItemReasonFactory
        {
            get { return ItemReasonFactory.Instance; }
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestGetChargeOffCodesForCashlinx()
        {
            List<ItemReasonCode> reasonCodes = ItemReasonFactory.GetChargeOffCodes(PawnSecApplication.Cashlinx);

            Assert.Greater(reasonCodes.Count, 0);

            foreach (ItemReasonCode rc in reasonCodes)
            {
                Assert.IsTrue(rc.ChargeOff);
                Assert.IsTrue(rc.IsApplicationAllowed(PawnSecApplication.Cashlinx));
            }
        }

        [Test]
        public void TestGetChargeOffCodesForAudit()
        {
            List<ItemReasonCode> reasonCodes = ItemReasonFactory.GetChargeOffCodes(PawnSecApplication.Audit);

            Assert.Greater(reasonCodes.Count, 0);
            //Assert.Greater(reasonCodes.Count, ItemReasonFactory.GetChargeOffCodes(false).Count);

            foreach (ItemReasonCode rc in reasonCodes)
            {
                Assert.IsTrue(rc.ChargeOff);
                Assert.IsTrue(rc.IsApplicationAllowed(PawnSecApplication.Audit));
            }
        }

        [Test]
        public void TestFindByDescription()
        {
            ItemReasonCode reasonCode = ItemReasonFactory.FindByDescription("CACC");

            Assert.IsNotNull(reasonCode);
            Assert.AreEqual(ItemReason.CACC, reasonCode.Reason);
        }

        [Test]
        public void TestFindByReasonEnum()
        {
            ItemReasonCode reasonCode = ItemReasonFactory.FindByReason(ItemReason.CACC);

            Assert.IsNotNull(reasonCode);
            Assert.AreEqual(ItemReason.CACC, reasonCode.Reason);
        }

        [Test]
        public void TestFindByReasonString()
        {
            ItemReasonCode reasonCode = ItemReasonFactory.FindByReason(ItemReason.CACC.ToString());

            Assert.IsNotNull(reasonCode);
            Assert.AreEqual(ItemReason.CACC, reasonCode.Reason);
        }

        [Test]
        public void TestFindByDescriptionInvalid()
        {
            ItemReasonCode reasonCode = ItemReasonFactory.FindByDescription("ABC");

            Assert.IsNull(reasonCode);
        }

        [Test]
        public void TestFindByReasonStringInvalid()
        {
            ItemReasonCode reasonCode = ItemReasonFactory.FindByReason("ABC");

            Assert.IsNull(reasonCode);
        }
    }
}
