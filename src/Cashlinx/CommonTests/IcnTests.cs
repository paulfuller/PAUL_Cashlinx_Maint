using Common.Libraries.Objects.Business;
using NUnit.Framework;

namespace CommonTests
{
    [TestFixture]
    public class IcnTests
    {
        private Icn Icn { get; set; }

        [Test]
        public void TestShortCodeSingleItemNumber()
        {
            Icn = new Icn("5293.1");
            Assert.IsTrue(Icn.Initialized);
        }
    }
}
