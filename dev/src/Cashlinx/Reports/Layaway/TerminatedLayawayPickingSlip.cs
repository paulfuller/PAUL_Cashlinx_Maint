using System;
using System.Linq;
using Common.Libraries.Objects;
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
    public class TerminatedLayawayPickingSlip : ReportBase
    {
        #region Fields
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        public LayawayRunReports runReport;

        #endregion

        public TerminatedLayawayPickingSlip(IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {

        }

        #region Private Methods
        private void WriteDetail(PdfPTable detailTable, LayawayVO layaway)
        {
            WriteCell(detailTable, string.Empty, ReportFontUnderlined, 7, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(detailTable, "ICN", ReportFontUnderlined, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
            WriteCell(detailTable, "Description", ReportFontUnderlined, 3, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(detailTable, "Qty", ReportFontUnderlined, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            WriteCell(detailTable, "Aisle/Shelf Location", ReportFontUnderlined, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);

            foreach (var item in layaway.RetailItems)
            {
                //WriteCell(detailTable, item.Icn, ReportFontSmall, 2, (int)Element.ALIGN_LEFT, (int)Rectangle.NO_BORDER);
                PdfPCell cell = new PdfPCell();
                cell = GetFormattedICNCellSmallFont(item.Icn);
                cell.Colspan = 2;
                detailTable.AddCell(cell);
                WriteCell(detailTable, item.TicketDescription, ReportFontSmall, 3, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(detailTable, item.Quantity.ToString(), ReportFontSmall, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                if (!string.IsNullOrEmpty(item.Location_Aisle))
                    WriteCell(detailTable, item.Location_Aisle, ReportFontSmall, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                else if (!string.IsNullOrEmpty(item.Location_Shelf))
                    WriteCell(detailTable, item.Location_Shelf, ReportFontSmall, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                else
                    WriteCell(detailTable, string.Empty, ReportFontSmall, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            }
        }

        private void WritePaymentList(PdfPTable listTable, LayawayVO layaway)
        {
            int paymentsCount = 1;
            //loop thru all payments
            var paymentReceipts = (from r in layaway.Receipts
                                   where (r.Event == ReceiptEventTypes.LAY.ToString() || r.Event == ReceiptEventTypes.LAYPMT.ToString()) && r.ReferenceReceiptNumber.Length == 0
                                   select r).OrderBy(rn => rn.ReceiptNumber).ThenBy(rt => rt.RefTime).ToList();

            WriteCell(listTable, "Payment List", ReportFontUnderlined, 7, Rectangle.ALIGN_CENTER, Rectangle.NO_BORDER);
            foreach (Receipt receipt in paymentReceipts)
            {
                WriteCell(listTable, "[" + paymentsCount + "]  " + receipt.Date.ToString("d") + "   " + receipt.Amount.ToString("c"), ReportFontSmall, 1, Rectangle.ALIGN_LEFT, Rectangle.NO_BORDER);
                paymentsCount++;
            }
            int cellsToAdd = paymentReceipts.Count % 7;
            WriteCell(listTable, string.Empty, ReportFontUnderlined, 7 - cellsToAdd, Rectangle.ALIGN_CENTER, Rectangle.NO_BORDER);
        }

        protected override void DrawLine(PdfPTable linestable)
        {
            WriteCell(linestable, StringUtilities.fillString("_", 144), ReportFont, 7, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
        }


        private void WriteInfo(PdfPTable infoTable, LayawayVO layaway)
        {
            //row Customer Name
            string customerName = string.Empty;
            if (!string.IsNullOrEmpty(ReportObject.CustomerFirstName) && !string.IsNullOrEmpty(ReportObject.CustomerLastName))
                customerName = ReportObject.CustomerLastName + "," + ReportObject.CustomerFirstName;
            if (!string.IsNullOrEmpty(customerName))
                WriteCell(infoTable, "Customer: " + customerName, ReportFont, 2, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            else
                WriteCell(infoTable, string.Empty, ReportFont, 2, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(infoTable, string.Empty, ReportFont, 3, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(infoTable, "Amount of Sale:", ReportFont, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);

            WriteCell(infoTable, layaway.Amount.ToString("C"), ReportFont, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);

            //row empty 
            WriteCell(infoTable, string.Empty, ReportFont, 7, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            //row customer phone 
            if (!string.IsNullOrEmpty(ReportObject.ContactNumber))
                WriteCell(infoTable, "Customer Phone: " + ReportObject.ContactNumber, ReportFont, 2, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            else
                WriteCell(infoTable, string.Empty, ReportFont, 2, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(infoTable, string.Empty, ReportFont, 3, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(infoTable, "Sales Tax: ", ReportFont, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            decimal salesTax = layaway.SalesTaxAmount;
            WriteCell(infoTable, salesTax.ToString("C"), ReportFont, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);

            //row empty 
            WriteCell(infoTable, string.Empty, ReportFont, 7, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            //row Layaway Amount
            WriteCell(infoTable, string.Empty, ReportFont, 5, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(infoTable, "Layaway Amount: ", ReportFont, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);

            decimal layawayAmount = layaway.SalesTaxAmount + layaway.Amount;
            WriteCell(infoTable, layawayAmount.ToString("C"), ReportFont, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);

            //row empty 
            WriteCell(infoTable, string.Empty, ReportFont, 7, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            //row Payment Due Date
            WriteCell(infoTable, "Payment Due Date: " + layaway.NextPayment.ToShortDateString(), ReportFont, 2, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(infoTable, string.Empty, ReportFont, 2, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(infoTable, "Total of Payments Terminated:", ReportFont, 2, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);

            WriteCell(infoTable, layaway.GetAmountPaid().ToString("C"), ReportFont, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);

            //row empty 
            WriteCell(infoTable, string.Empty, ReportFont, 7, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);
        }

        private void ReportHeader(PdfPTable headingtable, Image gif, TerminatedLayawayPickingSlip pageEvent)
        {
            PdfPCell cell = new PdfPCell();

            //  heading - row 1
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            WriteCell(headingtable, string.Empty, ReportFont, 6, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(headingtable, string.Empty, ReportFont, 7, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(headingtable, DateTime.Now.ToString("MM/dd/yyyy"), ReportFont, 6, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(headingtable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

            WriteCell(headingtable, pageEvent.ReportObject.ReportTitle, ReportFontHeading, 7, Element.ALIGN_CENTER, Rectangle.NO_BORDER);

            WriteCell(headingtable, "Employee #:" + pageEvent.ReportObject.ReportEmployee, ReportFont, 7, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

            //draw line
            DrawLine(headingtable);
        }
        #endregion

        #region Public Properties
        public LayawayReportObject ReportObject { get; set; }

        #endregion

        #region Public Methods
        public bool CreateReport()
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.HALFLETTER.Rotate());
            try
            {
                //set up RunReport event overrides & create doc
                TerminatedLayawayPickingSlip events = this;
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ReportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                MultiColumnText columns = new MultiColumnText(document.PageSize.Top - 90, document.PageSize.Height - (90));
                float pageLeft = document.PageSize.Left;
                float pageright = document.PageSize.Right;
                columns.AddSimpleColumn(-27, document.PageSize.Width + 29);

                //set up tables, etc...
                PdfPTable table = new PdfPTable(7);
                table.WidthPercentage = 85;// document.PageSize.Width;
                PdfPCell cell = new PdfPCell();
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(25);
                runReport = new LayawayRunReports();
                document.Open();
                document.SetPageSize(PageSize.HALFLETTER.Rotate());
                document.SetMargins(-100, -100, 10, 45);
                document.AddTitle(ReportObject.ReportTitle + ": " + DateTime.Now.ToString("MM/dd/yyyy"));

                //int layawayCount = ReportObject.TerminatedLayawayPickingSlipList.Count;
                //int counter = 1;
                //foreach (LayawayVO layaway in ReportObject.TerminatedLayawayPickingSlipList)
                //{
                WriteCell(table, "Ticket #: " + ReportObject.TerminatedLayaway.TicketNumber.ToString(), ReportFontBold, 9, Rectangle.ALIGN_CENTER, Rectangle.NO_BORDER);

                WriteInfo(table, ReportObject.TerminatedLayaway);

                DrawLine(table);

                WritePaymentList(table, ReportObject.TerminatedLayaway);

                DrawLine(table);

                WriteDetail(table, ReportObject.TerminatedLayaway);

                    /*while (counter < layawayCount)
                    {
                        WriteCell(table, string.Empty, ReportFont, 7, Rectangle.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(table, string.Empty, ReportFont, 7, Rectangle.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(table, string.Empty, ReportFont, 7, Rectangle.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(table, string.Empty, ReportFont, 7, Rectangle.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(table, string.Empty, ReportFont, 7, Rectangle.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(table, string.Empty, ReportFont, 7, Rectangle.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(table, string.Empty, ReportFont, 7, Rectangle.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(table, string.Empty, ReportFont, 7, Rectangle.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(table, string.Empty, ReportFont, 7, Rectangle.ALIGN_CENTER, Rectangle.NO_BORDER);
                        WriteCell(table, string.Empty, ReportFont, 7, Rectangle.ALIGN_CENTER, Rectangle.NO_BORDER);
                        break;
                    }
                    counter++;
                }*/
                columns.AddElement(table);
                document.Add(columns);
                document.Close();
                //OpenFile();
                //CreateReport();
                isSuccessful = true;
            }
            catch (DocumentException de)
            {
                ReportObject.ReportError = de.Message; 
                ReportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            catch (IOException ioe)
            {
                ReportObject.ReportError = ioe.Message; 
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
                PdfPTable headerTbl = new PdfPTable(7);
                //set the width of the table to be the same as the document
                headerTbl.TotalWidth = document.PageSize.Width - 10;
                //headerTbl.WidthPercentage = 80;
                //I use an image logo in the header so I need to get an instance of the image to be able to insert it. I believe this is something you couldn't do with older versions of iTextSharp
                //Image logo = Image.GetInstance(HttpContext.Current.Server.MapPath("/images/logo.jpg"));
                Image logo = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                logo.ScalePercent(25);
                ReportHeader(headerTbl, logo, (TerminatedLayawayPickingSlip)writer.PageEvent);
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
