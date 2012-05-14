using Common.Libraries.Utility.RawTextPrinting;
using NUnit.Framework;

namespace CommonTests.Libraries.Utility.RawTextPrinting
{
    [TestFixture]
    public class MerchandiseDescriptionLineSeparatorTests
    {
        [Test]
        public void Test1RowUnderMaxWidth()
        {
            var lineSepartor = new MerchandiseDescriptionLineSeparator(8, 10);
            const string fullDescription = "12345";
            var expectedResults = new string[] { "12345" };

            var actualResults = lineSepartor.SplitIntoRows(fullDescription);

            Assert.AreEqual(expectedResults, actualResults);
        }
        
        [Test]
        public void Test1RowAtMaxWidth()
        {
            var lineSepartor = new MerchandiseDescriptionLineSeparator(8, 10);
            const string fullDescription = "1234567890";
            var expectedResults = new string[] { "1234567890" };

            var actualResults = lineSepartor.SplitIntoRows(fullDescription);

            Assert.AreEqual(expectedResults, actualResults);
        }

        [Test]
        public void Test2RowsUnderMaxWidth()
        {
            var lineSepartor = new MerchandiseDescriptionLineSeparator(8, 10);
            const string fullDescription = "123456789012345";
            var expectedResults = new string[] { "1234567890", "12345" };

            var actualResults = lineSepartor.SplitIntoRows(fullDescription);

            Assert.AreEqual(expectedResults, actualResults);
        }

        [Test]
        public void Test2RowsAtMaxWidth()
        {
            var lineSepartor = new MerchandiseDescriptionLineSeparator(8, 10);
            const string fullDescription = "12345678901234567890";
            var expectedResults = new string[] { "1234567890", "1234567890" };

            var actualResults = lineSepartor.SplitIntoRows(fullDescription);

            Assert.AreEqual(expectedResults, actualResults);
        }

        [Test]
        public void TestUnderMaxRowsBy1Char()
        {
            var lineSepartor = new MerchandiseDescriptionLineSeparator(2, 10);
            const string fullDescription = "1234567890123456789";
            var expectedResults = new string[] { "1234567890", "123456789" };

            var actualResults = lineSepartor.SplitIntoRows(fullDescription);

            Assert.AreEqual(expectedResults, actualResults);
        }

        [Test]
        public void TestUnderMaxRowsBy2Chars()
        {
            var lineSepartor = new MerchandiseDescriptionLineSeparator(2, 10);
            const string fullDescription = "123456789012345678";
            var expectedResults = new string[] { "1234567890", "12345678" };

            var actualResults = lineSepartor.SplitIntoRows(fullDescription);

            Assert.AreEqual(expectedResults, actualResults);
        }

        [Test]
        public void TestOverMaxRowsBy1Char()
        {
            var lineSepartor = new MerchandiseDescriptionLineSeparator(2, 10);
            const string fullDescription = "123456789012345678901";
            var expectedResults = new string[] { "1234567890", "1234567890" };

            var actualResults = lineSepartor.SplitIntoRows(fullDescription);

            Assert.AreEqual(expectedResults, actualResults);
        }

        [Test]
        public void TestOverMaxRowsBy2Chars()
        {
            var lineSepartor = new MerchandiseDescriptionLineSeparator(2, 10);
            const string fullDescription = "1234567890123456789012";
            var expectedResults = new string[] { "1234567890", "1234567890" };

            var actualResults = lineSepartor.SplitIntoRows(fullDescription);

            Assert.AreEqual(expectedResults, actualResults);
        }
    }
}
