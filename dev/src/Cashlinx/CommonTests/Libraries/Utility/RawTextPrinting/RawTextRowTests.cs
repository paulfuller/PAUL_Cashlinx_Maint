using Common.Libraries.Utility.RawTextPrinting;
using NUnit.Framework;

namespace CommonTests.Libraries.Utility.RawTextPrinting
{
    [TestFixture]
    public class RawTextRowTests
    {
        [Test]
        public void TestOneCellAtMaxWidth()
        {
            var documentWidth = 10;
            var cell1Value = "ABC";
            var expectedValue = "ABC       ";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            row.WriteText(cell1Value, 10, RawTextFlags.Left);

            var actualValue = row.GetValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestOneCellUnderMaxWidth()
        {
            var documentWidth = 10;
            var cell1Value = "ABC";
            var expectedValue = "ABC       ";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            row.WriteText(cell1Value, 5, RawTextFlags.Left);

            var actualValue = row.GetValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestTwoCellsAtMaxWidth()
        {
            var documentWidth = 10;
            var cellValue = "ABC";
            var expectedValue = "ABC  ABC  ";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            row.WriteText(cellValue, 5, RawTextFlags.Left);
            row.WriteText(cellValue, 5, RawTextFlags.Left);

            var actualValue = row.GetValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestTwoCellsUnderMaxWidth()
        {
            var documentWidth = 10;
            var cellValue = "ABC";
            var expectedValue = "ABC ABC   ";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            row.WriteText(cellValue, 4, RawTextFlags.Left);
            row.WriteText(cellValue, 4, RawTextFlags.Left);

            var actualValue = row.GetValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestOneCellAtMaxWidthBold()
        {
            var documentWidth = 10;
            var cell1Value = "ABC";
            var expectedValue = PrinterCode.BoldOn + "ABC       " + PrinterCode.BoldOff;

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(cell1Value, 10, RawTextFlags.Left);
            cell.Bold = true;

            var actualValue = row.GetValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestOneCellUnderMaxWidthBold()
        {
            var documentWidth = 10;
            var cell1Value = "ABC";
            var expectedValue = PrinterCode.BoldOn + "ABC       " + PrinterCode.BoldOff;

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(cell1Value, 5, RawTextFlags.Left);
            cell.Bold = true;

            var actualValue = row.GetValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestTwoCellsAtMaxWidthBoldFirstCell()
        {
            var documentWidth = 10;
            var cellValue = "ABC";
            var expectedValue = PrinterCode.BoldOn + "ABC  " + PrinterCode.BoldOff + "ABC  ";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(cellValue, 5, RawTextFlags.Left);
            cell.Bold = true;
            row.WriteText(cellValue, 5, RawTextFlags.Left);

            var actualValue = row.GetValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestTwoCellsUnderMaxWidthBoldFirstCell()
        {
            var documentWidth = 10;
            var cellValue = "ABC";
            var expectedValue = PrinterCode.BoldOn + "ABC " + PrinterCode.BoldOff + "ABC   ";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(cellValue, 4, RawTextFlags.Left);
            cell.Bold = true;
            row.WriteText(cellValue, 4, RawTextFlags.Left);

            var actualValue = row.GetValue();

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
