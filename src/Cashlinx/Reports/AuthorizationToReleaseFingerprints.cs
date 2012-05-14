using System;
using System.IO;
using Common.Libraries.Objects;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;

namespace Reports
{
    public class AuthorizationToReleaseFingerprints : ReportBase
    {
        private Font rptFont;
        private Font rptFontBold;
        private Font rptFontHeader;
        private Document document;

        private ReleaseFingerprintsContext _context;
        private int _seizeNumber;
        private ProductType _productType;

        public AuthorizationToReleaseFingerprints(int seizeNumber, ProductType productType, ReleaseFingerprintsContext context, IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {
            _context = context;
            _seizeNumber = seizeNumber;
            _productType = productType;
        }

        public bool Print()
        {
            try
            {
                rptFont = FontFactory.GetFont("Arial", 8, Font.NORMAL);
                rptFontBold = FontFactory.GetFont("Arial", 8, Font.BOLD);
                rptFontHeader = FontFactory.GetFont("Arial", 10, Font.BOLD);

                if (string.IsNullOrWhiteSpace(_context.OutputPath))
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Authorization to release fingerprints printing failed since file name is not set");
                    return false;
                }

                document = new Document(PageSize.HALFLETTER.Rotate());
                document.AddTitle("Authorization to Release Fingerprints");
                var writer = PdfWriter.GetInstance(document, new FileStream(_context.OutputPath, FileMode.Create));

                const int headerTableColumns = 2;
                var headerTable = new PdfPTable(headerTableColumns)
                {
                    WidthPercentage = 100
                };

                headerTable.AddCell(
                    new PdfPCell(new Paragraph("Date: " + string.Format("{0:MM/dd/yyyy}", DateTime.Parse(_context.TransactionDate)), rptFont))
                        {Border = Rectangle.NO_BORDER});

                headerTable.AddCell(
                    new PdfPCell(new Paragraph("Employee #: " + _context.EmployeeNumber, rptFont))
                        {Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT});

                headerTable.AddCell(new PdfPCell(new Paragraph("Authorization to Release Fingerprints", rptFontHeader)) { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = headerTableColumns });

                headerTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = headerTableColumns, FixedHeight = 10 }); // empty row

                const int bodyTableColumns = 2;

                var bodyTable = new PdfPTable(bodyTableColumns)
                                    {
                                        WidthPercentage = 100
                                    };

                bodyTable.SetWidths(new [] { .5f, 2.5f });

                bodyTable.AddCell(new PdfPCell(new Paragraph("Received From:", rptFont)){ Border = Rectangle.NO_BORDER,VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                bodyTable.AddCell(new PdfPCell(new Paragraph(_context.ShopName, rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });

                bodyTable.AddCell(new PdfPCell(new Paragraph("", rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                bodyTable.AddCell(new PdfPCell(new Paragraph(_context.ShopAddress, rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });

                bodyTable.AddCell(new PdfPCell(new Paragraph("", rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                bodyTable.AddCell(new PdfPCell(new Paragraph(string.Format("{0}, {1} {2}", _context.ShopCity, _context.ShopState, _context.ShopZipCode ) , rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });

                bodyTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = headerTableColumns, FixedHeight = 10 }); // empty row

                bodyTable.AddCell(new PdfPCell(new Paragraph("Pawned By:", rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                bodyTable.AddCell(new PdfPCell(new Paragraph(_context.CustomerName, rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });

                bodyTable.AddCell(new PdfPCell(new Paragraph("", rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                bodyTable.AddCell(new PdfPCell(new Paragraph(FormatCustomerAddress(), rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });

                bodyTable.AddCell(new PdfPCell(new Paragraph("", rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                bodyTable.AddCell(new PdfPCell(new Paragraph(string.Format("{0}, {1}  {2}", _context.CustomerCity, _context.CustomerState, _context.CustomerZipCode), rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });

                bodyTable.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = bodyTableColumns, FixedHeight = 10 }); // empty row

                const int bodyTable2Columns = 3;
                var bodyTable2 = new PdfPTable(bodyTable2Columns)
                {
                    WidthPercentage = 100
                };

                bodyTable2.SetWidths(new[] { .5f, 1f, 1.5f });

                bodyTable2.AddCell(new PdfPCell(new Paragraph("Authorization #:", rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                bodyTable2.AddCell(new PdfPCell(new Paragraph(_seizeNumber.ToString(), rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });

                string loanBuyLabel = _productType == ProductType.BUY ? "Buy" : "Loan";
                bodyTable2.AddCell(new PdfPCell(new Paragraph(loanBuyLabel + "#: " + _context.TicketNumber, rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });

                bodyTable2.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = bodyTable2Columns, FixedHeight = 10 }); // empty row

                const int bodyTable3Columns = 2;
                var bodyTable3 = new PdfPTable(bodyTable3Columns)
                {
                    WidthPercentage = 100
                };

                bodyTable3.SetWidths(new[]{.65f, 2.35f});

                bodyTable3.AddCell(new PdfPCell(new Paragraph("Agency Receiving Fingerprint", rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                bodyTable3.AddCell(new PdfPCell(new Paragraph(":  " + _context.Agency, rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });

                bodyTable3.AddCell(new PdfPCell(new Paragraph("Case #", rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                bodyTable3.AddCell(new PdfPCell(new Paragraph(":  " + _context.CaseNumber, rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });

                bodyTable3.AddCell(new PdfPCell(new Paragraph("Subpoena #", rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                bodyTable3.AddCell(new PdfPCell(new Paragraph(":  " + _context.SubpoenaNumber, rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });

                bodyTable3.AddCell(new PdfPCell(new Paragraph("Officer Name / Badge #:", rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                bodyTable3.AddCell(new PdfPCell(new Paragraph(string.Format(":  {0} / {1}", _context.OfficerName, _context.BadgeNumber), rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });

                bodyTable3.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = bodyTable3Columns, FixedHeight = 10 }); // empty row
                bodyTable3.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = bodyTable3Columns, FixedHeight = 10 }); // empty row
                bodyTable3.AddCell(new PdfPCell(new Phrase()) { Border = Rectangle.NO_BORDER, Colspan = bodyTable3Columns, FixedHeight = 10 }); // empty row

                const int signatureTableColumns = 3;
                var signatureTable = new PdfPTable(signatureTableColumns)
                {
                    WidthPercentage = 100
                };

                signatureTable.SetWidths(new[] {3F, 1F, 3F});

                signatureTable.AddCell(new PdfPCell(new Paragraph("X", rptFont)) { Border = Rectangle.BOTTOM_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                signatureTable.AddCell(new PdfPCell(new Paragraph("", rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                signatureTable.AddCell(new PdfPCell(new Paragraph("X", rptFont)) { Border = Rectangle.BOTTOM_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });

                signatureTable.AddCell(new PdfPCell(new Paragraph(" Employee Signature", rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                signatureTable.AddCell(new PdfPCell(new Paragraph("", rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                signatureTable.AddCell(new PdfPCell(new Paragraph(" Officer Signature", rptFont)) { Border = Rectangle.NO_BORDER, VerticalAlignment = Element.ALIGN_LEFT, Colspan = 1 });
                
                document.Open();
                document.Add(headerTable);
                document.Add(bodyTable);
                document.Add(bodyTable2);
                document.Add(bodyTable3);
                document.Add(signatureTable);
                document.Close();
            }
            catch (Exception de)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Authorization to release fingerprints printing " + de.Message);
                return false;
            }

            return true;
        }

        private string FormatCustomerAddress()
        {
            return string.Format("{0}{1} {2}",
                _context.CustomerAddress1,
                string.IsNullOrEmpty(_context.CustomerAddress2) ? string.Empty : " " + _context.CustomerAddress2,
                _context.CustomerAddressUnit
                );
        }

        public class ReleaseFingerprintsContext
        {
            public string OutputPath { get; set; }
            public string EmployeeNumber { get; set; }
            public string ShopName { get; set; }
            public string ShopAddress { get; set; }
            public string ShopCity { get; set; }
            public string ShopState { get; set; }
            public string ShopZipCode { get; set; }
            public string CustomerName { get; set; }
            public string CustomerAddress1 { get; set; }
            public string CustomerAddress2 { get; set; }
            public string CustomerAddressUnit { get; set; }
            public string CustomerCity { get; set; }
            public string CustomerState { get; set; }
            public string CustomerZipCode { get; set; }
            public int TicketNumber { get; set; }
            public string Agency { get; set; }
            public string CaseNumber { get; set; }
            public string SubpoenaNumber { get; set; }
            public string OfficerName { get; set; }
            public string BadgeNumber { get; set; }
            public string TransactionDate { get; set; }
        }
    }
}
