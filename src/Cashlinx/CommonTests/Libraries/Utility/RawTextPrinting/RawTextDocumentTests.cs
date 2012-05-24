using System;
using Common.Libraries.Utility.RawTextPrinting;
using NUnit.Framework;

namespace CommonTests.Libraries.Utility.RawTextPrinting
{
    [TestFixture]
    public class RawTextDocumentTests
    {
        [Test]
        public void TestOneRowTwoCellsWithTrimAndNewLine()
        {
            var documentWidth = 10;
            var cellValue = "ABC";
            var expectedValue = "ABC  ABC\r\n";

            var document = new RawTextDocument(documentWidth);
            var row = document.CreateNewRow();
            var cell1 = row.WriteText(5, RawTextFlags.Left);
            cell1.Value = cellValue;
            var cell2 = row.WriteText(5, RawTextFlags.Left);
            cell2.Value = cellValue;

            var actualValue = document.GetDocumentValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestOneRowTwoCellsWithNewLine()
        {
            var documentWidth = 10;
            var cellValue = "ABC";
            var expectedValue = "ABC  ABC  \r\n";

            var document = new RawTextDocument(documentWidth);
            document.TrimEndOnRow = false;
            document.NewLineValue = "\r\n";
            var row = document.CreateNewRow();
            var cell1 = row.WriteText(5, RawTextFlags.Left);
            cell1.Value = cellValue;
            var cell2 = row.WriteText(5, RawTextFlags.Left);
            cell2.Value = cellValue;

            var actualValue = document.GetDocumentValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestOneRowTwoCellsWithCustomNewLine()
        {
            var documentWidth = 10;
            var cellValue = "ABC";
            var expectedValue = "ABC  ABC  \n";

            var document = new RawTextDocument(documentWidth);
            document.TrimEndOnRow = false;
            document.NewLineValue = "\n";
            var row = document.CreateNewRow();
            var cell1 = row.WriteText(5, RawTextFlags.Left);
            cell1.Value = cellValue;
            var cell2 = row.WriteText(5, RawTextFlags.Left);
            cell2.Value = cellValue;

            var actualValue = document.GetDocumentValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestTwoRowsTwoCellsWithTrimAndNewLine()
        {
            var documentWidth = 10;
            var cellValue = "ABC";
            var expectedValue = "ABC  ABC\r\nABC  ABC\r\n";

            var document = new RawTextDocument(documentWidth);

            for (var rowNumber = 1; rowNumber <= 2; rowNumber++)
            {
                var row = document.CreateNewRow();
                var cell1 = row.WriteText(5, RawTextFlags.Left);
                cell1.Value = cellValue;
                var cell2 = row.WriteText(5, RawTextFlags.Left);
                cell2.Value = cellValue;
            }

            var actualValue = document.GetDocumentValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestTwoRowsTwoCellsWithNewLine()
        {
            var documentWidth = 10;
            var cellValue = "ABC";
            var expectedValue = "ABC  ABC  \r\nABC  ABC  \r\n";

            var document = new RawTextDocument(documentWidth);
            document.TrimEndOnRow = false;
            document.NewLineValue = "\r\n";

            for (var rowNumber = 1; rowNumber <= 2; rowNumber++)
            {
                var row = document.CreateNewRow();
                var cell1 = row.WriteText(5, RawTextFlags.Left);
                cell1.Value = cellValue;
                var cell2 = row.WriteText(5, RawTextFlags.Left);
                cell2.Value = cellValue;
            }

            var actualValue = document.GetDocumentValue();
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void TestTwoRowsTwoCellsWithCustomNewLine()
        {
            var documentWidth = 10;
            var cellValue = "ABC";
            var expectedValue = "ABC  ABC  \nABC  ABC  \n";

            var document = new RawTextDocument(documentWidth);
            document.TrimEndOnRow = false;
            document.NewLineValue = "\n";

            for (var rowNumber = 1; rowNumber <= 2; rowNumber++)
            {
                var row = document.CreateNewRow();
                var cell1 = row.WriteText(5, RawTextFlags.Left);
                cell1.Value = cellValue;
                var cell2 = row.WriteText(5, RawTextFlags.Left);
                cell2.Value = cellValue;
            }

            var actualValue = document.GetDocumentValue();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void SampleIndianaPoliceCardTest()
        {
            const int leftColumnWidth = 45;
            const string horizontalSeparatorValue = "| ";
            const string verticalSeparatorValue = "-";
            const string intersectingSeparatorValue = "+";
            const int rightColumnWidth = 32;
            var document = new RawTextDocument(80);
            document.AddPrinterCode(PrinterCode.LineDensity8Dpi);
            document.AddPrinterCode(PrinterCode.HorizontalSpacing171Cpi);

            var row = document.CreateNewRow();
            row.WriteText("NAME OF CUSTOMER", leftColumnWidth);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("SERIAL NUMBER", rightColumnWidth);

            row = document.CreateNewRow();
            row.WriteText("Hammond, James R", leftColumnWidth, RawTextFlags.ForceUpper);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText(string.Empty, rightColumnWidth);

            row = document.CreateNewRow();
            row.WriteRepeatingText(verticalSeparatorValue, 45);
            row.WriteText(intersectingSeparatorValue);
            row.WriteRepeatingText(verticalSeparatorValue, 28);

            row = document.CreateNewRow();
            row.WriteText("CUSTOMER SIGNATURE", leftColumnWidth);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("ITEM", rightColumnWidth);

            row = document.CreateNewRow();
            row.WriteText(string.Empty, leftColumnWidth);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("NUG DROP", rightColumnWidth);

            row = document.CreateNewRow();
            row.WriteRepeatingText(verticalSeparatorValue, 45);
            row.WriteText(intersectingSeparatorValue);
            row.WriteRepeatingText(verticalSeparatorValue, 28);

            row = document.CreateNewRow();
            row.WriteText("ADDRESS", leftColumnWidth);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("DESIGN / MODEL / MAKE / CAL /", rightColumnWidth);

            row = document.CreateNewRow();
            row.WriteText("8025 W.RUSSELL RD #1125", leftColumnWidth);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("         SIZE / DESC.", rightColumnWidth);

            row = document.CreateNewRow();
            row.WriteRepeatingText(verticalSeparatorValue, 27);
            row.WriteText(intersectingSeparatorValue);
            row.WriteRepeatingText(verticalSeparatorValue, 10);
            row.WriteText(intersectingSeparatorValue);
            row.WriteRepeatingText(verticalSeparatorValue, 6);
            row.WriteText(intersectingSeparatorValue);

            row = document.CreateNewRow();
            row.WriteText("CITY - STATE - ZIP", 27);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("DOB", 9);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("EYES", 5);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("NUG DROP;Y/G;14 KT.;APPRX 8.9 GRM;", rightColumnWidth);

            row = document.CreateNewRow();
            row.WriteText("LAS VEGAS, NV89113", 27);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("09/27/67", 9);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("GRN", 5);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("ORM SHAPED;", rightColumnWidth);

            row = document.CreateNewRow();
            row.WriteRepeatingText(verticalSeparatorValue, 6);
            row.WriteText(intersectingSeparatorValue);
            row.WriteRepeatingText(verticalSeparatorValue, 7);
            row.WriteText(intersectingSeparatorValue);
            row.WriteRepeatingText(verticalSeparatorValue, 12);
            row.WriteText(intersectingSeparatorValue);
            row.WriteRepeatingText(verticalSeparatorValue, 7);
            row.WriteText(intersectingSeparatorValue);
            row.WriteRepeatingText(verticalSeparatorValue, 2);
            row.WriteText(intersectingSeparatorValue);
            row.WriteRepeatingText(verticalSeparatorValue, 6);
            row.WriteText(intersectingSeparatorValue);

            row = document.CreateNewRow();
            row.WriteText("SEX", 6);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("HEIGHT", 6);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("WEIGHT", 11);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("HAIR", 6);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("ORIGIN", 8);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText(string.Empty, rightColumnWidth);

            row = document.CreateNewRow();
            row.WriteText("M", 6);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("5'10", 6);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("215", 11, RawTextFlags.Right);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("BRN", 6);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("W", 8);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText(string.Empty, rightColumnWidth);

            row = document.CreateNewRow();
            row.WriteText("------");
            row.WriteText(intersectingSeparatorValue);
            row.WriteText("-------");
            row.WriteText(intersectingSeparatorValue);
            row.WriteText("------------");
            row.WriteText(intersectingSeparatorValue);
            row.WriteText("-------");
            row.WriteText(intersectingSeparatorValue);
            row.WriteText("---------");
            row.WriteText(intersectingSeparatorValue);

            row = document.CreateNewRow();
            row.WriteText("IDENTIFICATION TYPE DR LIC OR VALID PHOTO ID", leftColumnWidth);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText(string.Empty, rightColumnWidth);

            row = document.CreateNewRow();
            row.WriteText("DL-NV-2000407563", leftColumnWidth);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText(string.Empty, rightColumnWidth);

            row = document.CreateNewRow();
            row.WriteText("-------");
            row.WriteText(intersectingSeparatorValue);
            row.WriteText("-------");
            row.WriteText(intersectingSeparatorValue);
            row.WriteText("------");
            row.WriteText(intersectingSeparatorValue);
            row.WriteText("------");
            row.WriteText(intersectingSeparatorValue);
            row.WriteText("------------");
            row.WriteText(intersectingSeparatorValue);
            row.WriteText("--");
            row.WriteText(intersectingSeparatorValue);
            row.WriteText("--------------");
            row.WriteText(intersectingSeparatorValue);
            row.WriteText("--------------");

            row = document.CreateNewRow();
            row.WriteText("CONS", 7);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("TRADE", 6);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("PURC", 5);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("PAWN", 5);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("TICKET NO.", 11);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("DATE & TIME", 16);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("LOAN AMOUNT", 11);

            row = document.CreateNewRow();
            row.WriteText(string.Empty, 7);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText(string.Empty, 6);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("X", 5);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText(string.Empty, 5);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("361208", 11);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("05/05/09 15:50", 16);
            row.WriteText(horizontalSeparatorValue);
            row.WriteText("90.00", 11);

            row = document.CreateNewRow();
            row.WriteText("-------");
            row.WriteText(intersectingSeparatorValue);
            row.WriteText("-------");
            row.WriteText(intersectingSeparatorValue);
            row.WriteText("------");
            row.WriteText(intersectingSeparatorValue);
            row.WriteText("------");
            row.WriteText(intersectingSeparatorValue);
            row.WriteText("------------");
            row.WriteText(intersectingSeparatorValue);
            row.WriteText("-----------------");
            row.WriteText(intersectingSeparatorValue);
            row.WriteText("--------------");

            row = document.CreateNewRow();
            row.WriteText("Cashland Financial Services, Inc", 45);
            row.WriteText("PAGE 1 OF 1", 30, RawTextFlags.Right);


            var actualValue = document.GetDocumentValue();
            var expectedValue = string.Format("{0}{1}{2}", PrinterCode.LineDensity8Dpi, PrinterCode.HorizontalSpacing171Cpi,
@"NAME OF CUSTOMER                             | SERIAL NUMBER
HAMMOND, JAMES R                             |
---------------------------------------------+----------------------------
CUSTOMER SIGNATURE                           | ITEM
                                             | NUG DROP
---------------------------------------------+----------------------------
ADDRESS                                      | DESIGN / MODEL / MAKE / CAL /
8025 W.RUSSELL RD #1125                      |          SIZE / DESC.
---------------------------+----------+------+
CITY - STATE - ZIP         | DOB      | EYES | NUG DROP;Y/G;14 KT.;APPRX 8.9 GR
LAS VEGAS, NV89113         | 09/27/67 | GRN  | ORM SHAPED;
------+-------+------------+-------+--+------+
SEX   | HEIGHT| WEIGHT     | HAIR  | ORIGIN  |
M     | 5'10  |         215| BRN   | W       |
------+-------+------------+-------+---------+
IDENTIFICATION TYPE DR LIC OR VALID PHOTO ID |
DL-NV-2000407563                             |
-------+-------+------+------+------------+--+--------------+--------------
CONS   | TRADE | PURC | PAWN | TICKET NO. | DATE & TIME     | LOAN AMOUNT
       |       | X    |      | 361208     | 05/05/09 15:50  | 90.00
-------+-------+------+------+------------+-----------------+--------------
Cashland Financial Services, Inc                                PAGE 1 OF 1
");

            //Console.Write(actualValue);
            //string returnValue;
            //SendASCIIStringToPrinter("192.168.106.202", 9100, actualValue, out returnValue);
            Assert.AreEqual(expectedValue, actualValue);
            //Console.WriteLine(returnValue);
        }
    }
}
