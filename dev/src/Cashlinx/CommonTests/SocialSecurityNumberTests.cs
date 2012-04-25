using Common.Libraries.Objects.Customer;
using NUnit.Framework;

namespace CommonTests
{
    [TestFixture]
    public class SocialSecurityNumberTests
    {
        public SocialSecurityNumber Ssn { get; set; }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestParsingWithValidValue()
        {
            Ssn = new SocialSecurityNumber("522513545");
            Assert.AreEqual("522", Ssn.AreaNumber);
            Assert.AreEqual("51", Ssn.GroupNumber);
            Assert.AreEqual("3545", Ssn.SerialNumber);
            Assert.AreEqual("522-51-3545", Ssn.FormattedValue);
            Assert.IsTrue(Ssn.IsValid);
        }

        [Test]
        public void TestParsingWithValidValueAndWhitespace()
        {
            Ssn = new SocialSecurityNumber("522 513545");
            Assert.AreEqual("522", Ssn.AreaNumber);
            Assert.AreEqual("51", Ssn.GroupNumber);
            Assert.AreEqual("3545", Ssn.SerialNumber);
            Assert.AreEqual("522-51-3545", Ssn.FormattedValue);
            Assert.IsTrue(Ssn.IsValid);
        }

        [Test]
        public void TestParsingWithValidValueAndHyphen()
        {
            Ssn = new SocialSecurityNumber("522-51-3545");
            Assert.AreEqual("522", Ssn.AreaNumber);
            Assert.AreEqual("51", Ssn.GroupNumber);
            Assert.AreEqual("3545", Ssn.SerialNumber);
            Assert.AreEqual("522-51-3545", Ssn.FormattedValue);
            Assert.IsTrue(Ssn.IsValid);
        }

        [Test]
        public void TestParsingWithInvalidAlpha()
        {
            Ssn = new SocialSecurityNumber("522a51b3545");
            Assert.IsFalse(Ssn.IsValid);
        }

        [Test]
        public void TestParsingWithInvalidLength()
        {
            Ssn = new SocialSecurityNumber("52251354");
            Assert.IsFalse(Ssn.IsValid);
        }

        [Test]
        public void TestParsingWith666Area()
        {
            Ssn = new SocialSecurityNumber("666513545");
            Assert.IsFalse(Ssn.IsValid);
        }

        [Test]
        public void TestParsingWithZeroArea()
        {
            Ssn = new SocialSecurityNumber("000513545");
            Assert.IsFalse(Ssn.IsValid);
        }

        [Test]
        public void TestParsingWithZeroGroup()
        {
            Ssn = new SocialSecurityNumber("522003545");
            Assert.IsFalse(Ssn.IsValid);
        }

        [Test]
        public void TestParsingWithZeroSerial()
        {
            Ssn = new SocialSecurityNumber("522510000");
            Assert.IsFalse(Ssn.IsValid);
        }

        [Test]
        public void TestParsingWithInvalidArea1()
        {
            Ssn = new SocialSecurityNumber("734513545");
            Assert.IsFalse(Ssn.IsValid);
        }

        [Test]
        public void TestParsingWithInvalidArea2()
        {
            Ssn = new SocialSecurityNumber("749513545");
            Assert.IsFalse(Ssn.IsValid);
        }

        [Test]
        public void TestParsingWithInvalidArea3()
        {
            Ssn = new SocialSecurityNumber("740513545");
            Assert.IsFalse(Ssn.IsValid);
        }

        [Test]
        public void TestParsingWithInvalidArea4()
        {
            Ssn = new SocialSecurityNumber("773513545");
            Assert.IsFalse(Ssn.IsValid);
        }
    }
}
