using System;
using System.Collections.Generic;
using System.IO;
using Common.Libraries.Utility.ISharp;
using NUnit.Framework;

namespace PawnTests
{
    [TestFixture]
    public class MailBookTests
    {
        private string CashlinxDirectory { get; set; }
        private string OutputFile { get; set; }
        private string TemplateFilePathPage3 { get; set; }

        [SetUp]
        public void Setup()
        {
            CashlinxDirectory = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            OutputFile = Path.Combine(Environment.CurrentDirectory, "ps3877_output.pdf");
            TemplateFilePathPage3 = Path.Combine(CashlinxDirectory, @"PawnTests\bin\Debug\Libraries\Resources\ps3877_3.pdf");

        }

        [Test]
        public void BlueSkyStampMailBook()
        {
            var eDeviceData = new Dictionary<string, string>();
            eDeviceData.Add("shop_name_shop_no", "Unit Test Shop 1");
            eDeviceData.Add("shop_address", "1600 W 7th St.");
            eDeviceData.Add("shop_city_state_zip", "Fort Worth, TX 76108");

            const int numberOfRecords = 8;

            for (var i = 1; i <= numberOfRecords; i++)
            {
                string rowNumKey = string.Format("row{0}_num", i);
                string rowAddress1Key = string.Format("row{0}_address1", i);
                string rowAddress2Key = string.Format("row{0}_address2", i);
                string rowAddress3Key = string.Format("row{0}_address3", i);
                string rowAddress4Key = string.Format("row{0}_address4", i);


                eDeviceData.Add(rowNumKey, i.ToString());
                eDeviceData.Add(rowAddress1Key, "Name " + i.ToString());
                eDeviceData.Add(rowAddress2Key, "Street " + i.ToString());
                eDeviceData.Add(rowAddress3Key, "Address1 " + i.ToString());
                eDeviceData.Add(rowAddress4Key, "Address2 " + i.ToString());
            }

            eDeviceData.Add("#_mailers_printed", numberOfRecords.ToString());
            eDeviceData.Add("page_x_of_x_1", "(Page 1 of 2)");
            //eDeviceData.Add("page_x_of_x_2", "(Page 2 of 2)");

            PDFITextSharpUtilities.PdfSharpTools tools;
            PDFITextSharpUtilities.OpenPDFFile(TemplateFilePathPage3, out tools);



            Assert.IsTrue(PDFITextSharpUtilities.StampSimplePDFWithFormFields(tools,
                                                                             OutputFile, false, eDeviceData));
        }
    }
}
