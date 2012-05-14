using Common.Libraries.Utility.RawTextPrinting;
using NUnit.Framework;

namespace CommonTests.Libraries.Utility.RawTextPrinting
{
    [TestFixture]
    public class RawTextCellTests
    {
        [Test]
        public void TestLeftAlignmentUnderMaxLimit()
        {
            var documentWidth = 10;
            var cellWidth = 10;
            var initialValue = "ABC";
            var expectedValue = "ABC       ";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(initialValue, cellWidth, RawTextFlags.Left);

            var actualValue = cell.GetValue().GetFullValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestRightAlignmentUnderMaxLimit()
        {
            var documentWidth = 10;
            var cellWidth = 10;
            var initialValue = "ABC";
            var expectedValue = "       ABC";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(initialValue, cellWidth, RawTextFlags.Right);

            var actualValue = cell.GetValue().GetFullValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestCenterAlignmentUnderMaxLimit()
        {
            var documentWidth = 10;
            var cellWidth = 10;
            var initialValue = "ABC";
            var expectedValue = "   ABC    ";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(initialValue, cellWidth, RawTextFlags.Center);

            var actualValue = cell.GetValue().GetFullValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestLeftAlignmentOverMaxLimit()
        {
            var documentWidth = 10;
            var cellWidth = 10;
            var initialValue = "ABCDEFGHIJKL";
            var expectedValue = "ABCDEFGHIJ";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(initialValue, cellWidth, RawTextFlags.Left);

            var actualValue = cell.GetValue().GetFullValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestRightAlignmentOverMaxLimit()
        {
            var documentWidth = 10;
            var cellWidth = 10;
            var initialValue = "ABCDEFGHIJKL";
            var expectedValue = "ABCDEFGHIJ";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(initialValue, cellWidth, RawTextFlags.Right);

            var actualValue = cell.GetValue().GetFullValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestCenterAlignmentOverMaxLimit()
        {
            var documentWidth = 10;
            var cellWidth = 10;
            var initialValue = "ABCDEFGHIJKL";
            var expectedValue = "ABCDEFGHIJ";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(initialValue, cellWidth, RawTextFlags.Center);

            var actualValue = cell.GetValue().GetFullValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestLeftAlignmentEqualMaxLimitMinus1()
        {
            var documentWidth = 10;
            var cellWidth = 10;
            var initialValue = "ABCDEFGHI";
            var expectedValue = "ABCDEFGHI ";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(initialValue, cellWidth, RawTextFlags.Left);

            var actualValue = cell.GetValue().GetFullValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestRightAlignmentEqualMaxLimitMinus1()
        {
            var documentWidth = 10;
            var cellWidth = 10;
            var initialValue = "ABCDEFGHI";
            var expectedValue = " ABCDEFGHI";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(initialValue, cellWidth, RawTextFlags.Right);

            var actualValue = cell.GetValue().GetFullValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestCenterAlignmentEqualMaxLimitMinus1()
        {
            var documentWidth = 10;
            var cellWidth = 10;
            var initialValue = "ABCDEFGHI";
            var expectedValue = "ABCDEFGHI ";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(initialValue, cellWidth, RawTextFlags.Center);

            var actualValue = cell.GetValue().GetFullValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestRepeatingText()
        {
            var documentWidth = 5;
            var cellWidth = 5;
            var initialValue = "Abc";
            var expectedValue = "AbcAb";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteRepeatingText(initialValue, cellWidth);

            var actualValue = cell.GetValue().GetFullValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestForceUpper()
        {
            var documentWidth = 5;
            var cellWidth = 5;
            var initialValue = "Abc";
            var expectedValue = "ABC  ";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(initialValue, cellWidth, RawTextFlags.ForceUpper);

            var actualValue = cell.GetValue().GetFullValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestForceLower()
        {
            var documentWidth = 5;
            var cellWidth = 5;
            var initialValue = "Abc";
            var expectedValue = "abc  ";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(initialValue, cellWidth, RawTextFlags.ForceLower);

            var actualValue = cell.GetValue().GetFullValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestBold()
        {
            var documentWidth = 5;
            var cellWidth = 5;
            var initialValue = "Abc";
            var expectedValue = PrinterCode.BoldOn + "Abc  " + PrinterCode.BoldOff;

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(initialValue, cellWidth);
            cell.Bold = true;

            var actualValue = cell.GetValue().GetFullValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestItalic()
        {
            var documentWidth = 5;
            var cellWidth = 5;
            var initialValue = "Abc";
            var expectedValue = PrinterCode.ItalicOn + "Abc  " + PrinterCode.ItalicOff;

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(initialValue, cellWidth);
            cell.Italic = true;

            var actualValue = cell.GetValue().GetFullValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestUnderline()
        {
            var documentWidth = 5;
            var cellWidth = 5;
            var initialValue = "Abc";
            var expectedValue = PrinterCode.UnderlineOn + "Abc  " + PrinterCode.UnderlineOff;

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell = row.WriteText(initialValue, cellWidth);
            cell.Underline = true;

            var actualValue = cell.GetValue().GetFullValue();

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
