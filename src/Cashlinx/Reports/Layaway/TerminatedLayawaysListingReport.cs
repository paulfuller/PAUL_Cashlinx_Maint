using System;
using System.Linq;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Common.Libraries.Utility.Logger;

namespace Reports.Layaway
{
    public class TerminatedLayawaysListingReport : ReportBase
    {

        #region Private Fields
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        private LayawayReportObject _reportObject;
        private decimal _amountTotal = 0.0m;
        private decimal _totPaidInTotal = 0.0m;
        private decimal _restockingTotal = 0.0m;
        private decimal _shopCreditTotal = 0.0m;
        private decimal _couponsTotal = 0.0m;
        #endregion

        public TerminatedLayawaysListingReport(IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {

        }

        #region Public Fields
        public LayawayRunReports runReport;
        #endregion

        #region Private Methods
        private void WriteColumns(PdfPTable columntable)
        {
            WriteCell(columntable, string.Empty, ReportFontUnderlined, 9, (int)Element.ALIGN_LEFT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
            WriteCell(columntable, "Doc#/Pay#", ReportFontUnderlined, 1, (int)Element.ALIGN_LEFT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
            WriteCell(columntable, "Customer Name", ReportFontUnderlined, 1, (int)Element.ALIGN_LEFT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
            WriteCell(columntable, "Tr. Date", ReportFontUnderlined, 1, (int)Element.ALIGN_CENTER, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
            WriteCell(columntable, "Amount", ReportFontUnderlined, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
            WriteCell(columntable, "Tot pd. In", ReportFontUnderlined, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
            WriteCell(columntable, "Restocking", ReportFontUnderlined, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
            WriteCell(columntable, "Shop Cred.", ReportFontUnderlined, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
            WriteCell(columntable, "Coupons", ReportFontUnderlined, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
            WriteCell(columntable, "Payments", ReportFontUnderlined, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
        }

        private void WriteDetail(PdfPTable detailTable)
        {

            foreach (LayawayVO listing in ReportObject.TerminatedLayawaysListingsList)
            {
                int currentRow = 1;
                var paymentReceipts = (from r in listing.Receipts
                                       where (r.Event == ReceiptEventTypes.LAY.ToString() || r.Event == ReceiptEventTypes.LAYPMT.ToString()) && 
                                              r.ReferenceReceiptNumber.Length == 0
                                       select r).OrderBy(rn => rn.ReceiptNumber).ThenBy(rt => rt.RefTime).ToList();
                decimal totalPaidIn = paymentReceipts.Sum(amt => amt.Amount);
                //need to get more info on these three variables, for now using temp variable to store
                decimal restockingFee = 0.0m;
                decimal shopCredit = 0.0m;
                decimal coupons = 0.0m;
                decimal forfeitAmount = listing.GetAmountPaid();
                //var restockingFeeField = (from r in listing.Fees where r.FeeType == FeeTypes.RESTOCKINGFEE select r);
                //restockingFee = restockingFeeField.Sum(amt => amt.OriginalAmount);
                restockingFee = ReportObject.RestockingFee;
                if (restockingFee > forfeitAmount)
                {
                    restockingFee = forfeitAmount;
                }
                else
                {
                    //restockingFee = ReportObject.RestockingFee;
                    shopCredit = totalPaidIn - restockingFee;
                }

                var couponTenders = (from r in listing.TenderDataDetails where r.TenderType == TenderTypes.COUPON.ToString() || r.TenderType == "C_CDIN" || r.TenderType == "CDIN" select r);
                coupons = couponTenders.Sum(amt => amt.TenderAmount);

                _amountTotal += listing.Amount;
                _totPaidInTotal += totalPaidIn;
                _restockingTotal += restockingFee;
                _shopCreditTotal += shopCredit;
                _couponsTotal += coupons;
                CustomerVO currentCust = listing.CustomerInfo;
                string customerName = string.Empty;
                if (currentCust != null)
                    customerName = currentCust.LastName + ", " + currentCust.FirstName;

                foreach (Receipt receipt in paymentReceipts)
                {
                    if (currentRow == 1)
                    {
                        WriteCell(detailTable, listing.TicketNumber.ToString(), ReportFontSmall, 1, (int)Element.ALIGN_LEFT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                        WriteCell(detailTable, customerName, ReportFontSmall, 1, (int)Element.ALIGN_LEFT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                        WriteCell(detailTable, receipt.Date.ToString("d"), ReportFontSmall, 1, (int)Element.ALIGN_CENTER, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                        WriteCell(detailTable, listing.Amount.ToString("C"), ReportFontSmall, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                        WriteCell(detailTable, totalPaidIn.ToString("C"), ReportFontSmall, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                        WriteCell(detailTable, restockingFee.ToString("C"), ReportFontSmall, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                        WriteCell(detailTable, shopCredit.ToString("C"), ReportFontSmall, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                        WriteCell(detailTable, coupons.ToString("C"), ReportFontSmall, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                        WriteCell(detailTable, receipt.Amount.ToString("c"), ReportFontSmall, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                    }
                    else
                    {
                        WriteCell(detailTable, "    " + receipt.ReceiptNumber, ReportFontSmall, 1, (int)Element.ALIGN_LEFT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                        WriteCell(detailTable, string.Empty, ReportFontSmall, 1, (int)Element.ALIGN_LEFT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                        WriteCell(detailTable, receipt.Date.ToString("d"), ReportFontSmall, 1, (int)Element.ALIGN_CENTER, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                        WriteCell(detailTable, string.Empty, ReportFontSmall, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                        WriteCell(detailTable, string.Empty, ReportFontSmall, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                        WriteCell(detailTable, string.Empty, ReportFontSmall, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                        WriteCell(detailTable, string.Empty, ReportFontSmall, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                        WriteCell(detailTable, string.Empty, ReportFontSmall, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                        WriteCell(detailTable, receipt.Amount.ToString("c"), ReportFontSmall, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                    }
                    currentRow++;
                }
                WriteCell(detailTable, String.Empty, ReportFontSmall, 9, (int)Element.ALIGN_LEFT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                WriteCell(detailTable, String.Empty, ReportFontSmall, 9, (int)Element.ALIGN_LEFT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                WriteCell(detailTable, String.Empty, ReportFontSmall, 9, (int)Element.ALIGN_LEFT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
                WriteCell(detailTable, String.Empty, ReportFontSmall, 9, (int)Element.ALIGN_LEFT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
            }
        }

        private void WriteSummary(PdfPTable summaryTable)
        {
            WriteCell(summaryTable, "Total", ReportFont, 1, (int)Element.ALIGN_LEFT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
            WriteCell(summaryTable, ReportObject.TerminatedLayawaysListingsList.Count.ToString() + " Layaway Terminations", ReportFont, 2, (int)Element.ALIGN_LEFT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
            WriteCell(summaryTable, _amountTotal.ToString("C"), ReportFont, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
            WriteCell(summaryTable, _totPaidInTotal.ToString("C"), ReportFont, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
            WriteCell(summaryTable, _restockingTotal.ToString("C"), ReportFont, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
            WriteCell(summaryTable, _shopCreditTotal.ToString("C"), ReportFont, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
            WriteCell(summaryTable, _couponsTotal.ToString("C"), ReportFont, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
            WriteCell(summaryTable, string.Empty, ReportFont, 1, (int)Element.ALIGN_RIGHT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);
        }

        private new void DrawLine(PdfPTable linestable)
        {
            WriteCell(linestable, StringUtilities.fillString("_", 142), ReportFont, 9, (int)Element.ALIGN_LEFT, (int)Rectangle.NO_BORDER);
        }

        private void ReportHeader(PdfPTable headingtable, Image gif, TerminatedLayawaysListingReport pageEvent)
        {
            PdfPCell cell = new PdfPCell();

            //  heading - row 1
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            WriteCell(headingtable, string.Empty, ReportFont, 8, (int)Element.ALIGN_CENTER, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);

            WriteCell(headingtable, string.Empty, ReportFont, 9, (int)Element.ALIGN_CENTER, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);

            WriteCell(headingtable, "Date: " + DateTime.Now.ToString(), ReportFont, 9, (int)Element.ALIGN_LEFT, (int)Element.ALIGN_TOP, (int)Rectangle.NO_BORDER);

            WriteCell(headingtable, pageEvent.ReportObject.ReportTitle, ReportFontHeading, 9, (int)Element.ALIGN_CENTER, (int)Rectangle.NO_BORDER);

            //draw line
            DrawLine(headingtable);

            WriteColumns(headingtable);
        }
        #endregion

        #region Public Properties
        public LayawayReportObject ReportObject
        {
            get
            {
                return _reportObject;
            }
            set
            {
                _reportObject = value;
            }
        }
        #endregion

        #region Public Methods
        public bool CreateReport()
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.LETTER);
            try
            {
                //set up RunReport event overrides & create doc
                TerminatedLayawaysListingReport events = this;
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ReportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                MultiColumnText columns = new MultiColumnText(document.PageSize.Top - 90, document.PageSize.Height - (90));
                float pageLeft = document.PageSize.Left;
                float pageright = document.PageSize.Right;
                columns.AddSimpleColumn(-22, document.PageSize.Width + 24);

                //set up tables, etc...
                PdfPTable table = new PdfPTable(9);
                table.WidthPercentage = 85;// document.PageSize.Width;
                PdfPCell cell = new PdfPCell();
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(25);
                runReport = new LayawayRunReports();
                document.Open();
                document.SetPageSize(PageSize.LETTER);
                document.SetMargins(-100, -100, 10, 45);
                document.AddTitle(string.Format("{0}: {1}", ReportObject.ReportTitle, DateTime.Now.ToString("MM/dd/yyyy")));
                WriteDetail(table);
                DrawLine(table);
                WriteSummary(table);

                DrawLine(table);
                columns.AddElement(table);
                document.Add(columns);
                document.Close();
                //OpenFile();
                //CreateReport();
                isSuccessful = true;
            }
            catch (DocumentException de)
            {
                ReportObject.ReportError = de.Message; ;
                ReportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            catch (IOException ioe)
            {
                ReportObject.ReportError = ioe.Message; ;
                ReportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            return isSuccessful;
        }
        #endregion

        #region Public Overrides
        public override void OnOpenDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            try
            {
                footerBaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                contentByte = writer.DirectContent;
                template = contentByte.CreateTemplate(50, 50);
            }
            catch (DocumentException)
            {
            }
            catch (IOException)
            {
            }
        }

        public override void OnCloseDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            template.BeginText();
            template.SetFontAndSize(footerBaseFont, 8);
            template.ShowText((writer.PageNumber - 1).ToString());
            template.EndText();
        }

        public override void OnStartPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnStartPage(writer, document);
            try
            {
                PdfPTable headerTbl = new PdfPTable(9);
                //set the width of the table to be the same as the document
                headerTbl.TotalWidth = document.PageSize.Width - 42;
                //I use an image logo in the header so I need to get an instance of the image to be able to insert it. I believe this is something you couldn't do with older versions of iTextSharp
                //Image logo = Image.GetInstance(HttpContext.Current.Server.MapPath("/images/logo.jpg"));
                Image logo = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                logo.ScalePercent(25);
                ReportHeader(headerTbl, logo, (TerminatedLayawaysListingReport)writer.PageEvent);
                //write the rows out to the PDF output stream. I use the height of the document to position the table. Positioning seems quite strange in iTextSharp and caused me the biggest headache.. It almost seems like it starts from the bottom of the page and works up to the top, so you may ned to play around with this.
                headerTbl.WriteSelectedRows(0, -1, 25, (document.PageSize.Height - 10), writer.DirectContent);
            }
            catch (Exception)
            {
                return;
            }
        }

        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            int pageN = writer.PageNumber;
            string text = string.Empty;
            string[] reportName = writer.Info.Get(PdfName.TITLE).ToString().Split(':');
            text = string.Format("{0} Page {1} of ", StringUtilities.fillString(" ", 80), pageN);
            float len = footerBaseFont.GetWidthPoint(text, 8);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            //Madhu fix for defexct PWNU00001411
            contentByte.SetTextMatrix(len / 2, 30);
            contentByte.ShowText(text);
            contentByte.EndText();
            //Madhu fix for defexct PWNU00001411
            contentByte.AddTemplate(template, len + len / 2, 30);
        }
        #endregion
    }
}
