using System;
using System.IO;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility.String;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Common.Libraries.Utility.Logger;

namespace Reports.Layaway
{
    public class LayawayPickingSlip : ReportBase
    {
        //used by overriden methods for footers
        #region Fields
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;

        //main objects
        public LayawayRunReports runReport;

        #endregion

        public LayawayPickingSlip(IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {

        }

        #region Protected Properties
        public LayawayReportObject ReportObject { get; set; }

        #endregion

        #region Private Methods
        private void ReportHeader(PdfPTable headingtable, Image gif, LayawayPickingSlip pageEvent)
        {
            PdfPCell cell = new PdfPCell();

            //  heading - row 1
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 6;
            headingtable.AddCell(cell);

            //  heading - row 2 -empty row
            cell = new PdfPCell(new Paragraph(string.Empty, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 7;
            headingtable.AddCell(cell);

            //row 3 - date
            cell = new PdfPCell(new Paragraph(DateTime.Now.ToString("MM/dd/yyyy"), ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 6;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Ticket #:" + pageEvent.ReportObject.CurrentLayaway.TicketNumber.ToString(), ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            //  heading - row 11
            //empty row
            cell = new PdfPCell(new Phrase(pageEvent.ReportObject.ReportTitle, ReportFontHeading));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 7;
            headingtable.AddCell(cell);

            //  heading - row 12
            //empty row
            cell = new PdfPCell(new Phrase("Employee #:" + pageEvent.ReportObject.ReportEmployee, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 7;
            headingtable.AddCell(cell);

            //draw line
            cell = new PdfPCell(new Phrase(StringUtilities.fillString("_", 140), ReportFont));
            cell.Colspan = 7;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            ColumnHeaders(headingtable);
        }

        private void ColumnHeaders(PdfPTable headerTable)
        {
            PdfPCell cell;
            cell = new PdfPCell(new Phrase("Customer: " + ReportObject.CustomerName, ReportFontBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 5;
            headerTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Total Layaway Amt: $ " + ReportObject.CurrentLayaway.Amount.ToString(), ReportFontBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 2;
            headerTable.AddCell(cell);

            //draw line
            cell = new PdfPCell(new Phrase(StringUtilities.fillString("_", 140), ReportFont));
            cell.Colspan = 7;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(cell);

            //Column headers
            cell = new PdfPCell(new Phrase("ICN", ReportFontBold));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(cell);

            Phrase descPhraseText = new Phrase("", ReportFontSmall);
            descPhraseText.Add(new Phrase("          ", ReportFontBold));
            descPhraseText.Add(new Phrase("Description", ReportFontBold));
            //cell = new PdfPCell(new Phrase("Description", ReportFontBold));
            //descPhrase.Add(descPhraseText);
            cell = new PdfPCell();
            cell.Colspan = 3;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.VerticalAlignment = Rectangle.ALIGN_TOP;
            cell.Border = Rectangle.NO_BORDER;
            cell.AddElement(descPhraseText);
            headerTable.AddCell(cell);

            Phrase qtyPhrase = new Phrase("", ReportFontSmall);
            qtyPhrase.Add(new Phrase(" ", ReportFontSmall));
            qtyPhrase.Add(new Phrase("Qty", ReportFontBold));
            //qtyPhrase.Add(qtyPhraseText);
            cell = new PdfPCell();
            //cell = new PdfPCell(new Phrase("Qty", ReportFontBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.VerticalAlignment = Rectangle.ALIGN_TOP;
            cell.Border = Rectangle.NO_BORDER;
            cell.AddElement(qtyPhrase);
            headerTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Aisle/Shelf Loc.", ReportFontBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(cell);

            cell = new PdfPCell(new Phrase(StringUtilities.fillString("-", 233), ReportFont));
            cell.Colspan = 7;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(cell);
        }

        private void ReportDetail(PdfPTable datatable)
        {
            int counter = 1;
            foreach (var retailItem in ReportObject.CurrentLayaway.RetailItems)
            {
                //set up tables, etc...
                //row 1
                Phrase counterPhrase = new Phrase(string.Format("[{0}]", counter), ReportFont);
                Chunk chunk = new Chunk("  ");
                counterPhrase.Add(chunk);
                var icnPhrase = GetFormattedICNPhrase(retailItem.Icn);
                counterPhrase.Add(icnPhrase);
                PdfPCell cell = new PdfPCell(counterPhrase);
                //cell.AddElement(counterPhrase);
                cell.Colspan = 2;
                cell.VerticalAlignment = Rectangle.ALIGN_TOP;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(retailItem.TicketDescription, ReportFont));
                cell.Colspan = 3;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                cell = new PdfPCell(new Phrase(retailItem.Quantity.ToString(), ReportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);

                Phrase aislePhrase = new Phrase(string.Empty, ReportFont);
                if (!string.IsNullOrEmpty(retailItem.Location_Aisle))
                {
                    aislePhrase.Add(retailItem.Location_Aisle);
                    //cell = new PdfPCell(new Phrase(retailItem.Location_Aisle, ReportFont));
                }

                if (!string.IsNullOrEmpty(retailItem.Location_Shelf))
                {
                    if (!string.IsNullOrEmpty(retailItem.Location_Aisle))
                        aislePhrase.Add("/");
                    aislePhrase.Add(retailItem.Location_Shelf);
                }

                if (!string.IsNullOrEmpty(retailItem.Location))
                {
                    if (!string.IsNullOrEmpty(retailItem.Location_Aisle) || !string.IsNullOrEmpty(retailItem.Location_Shelf))
                        aislePhrase.Add("/");
                    aislePhrase.Add(retailItem.Location);
                }
                cell = new PdfPCell(aislePhrase);
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                datatable.AddCell(cell);
                counter++;
            }
        }

        #endregion

        #region Public Methods
        public bool CreateReport()
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.HALFLETTER.Rotate());
            try
            {
                //set up RunReport event overrides & create doc
                LayawayPickingSlip events = this;
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ReportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                MultiColumnText columns = new MultiColumnText(document.PageSize.Top - 125, MultiColumnText.AUTOMATIC);
                columns.AddSimpleColumn(-30, document.PageSize.Width + 20);

                //set up tables, etc...
                PdfPTable table = new PdfPTable(7);
                table.WidthPercentage = 85;// document.PageSize.Width;
                var gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(25);
                runReport = new LayawayRunReports();
                document.Open();
                document.SetPageSize(PageSize.HALFLETTER.Rotate());
                document.SetMargins(-100, -100, 10, 45);
                document.AddTitle(string.Format("{0}: {1}", ReportObject.ReportTitle, DateTime.Now.ToString("MM/dd/yyyy")));

                ReportDetail(table);
                
                columns.AddElement(table);
                document.Add(columns);
                document.Close();
                //nnaeme
                //OpenFile(ReportObject.ReportTempFileFullName);
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
                headerTbl.TotalWidth = document.PageSize.Width - 60;
                headerTbl.WidthPercentage = 85;
                //I use an image logo in the header so I need to get an instance of the image to be able to insert it. I believe this is something you couldn't do with older versions of iTextSharp
                //Image logo = Image.GetInstance(HttpContext.Current.Server.MapPath("/images/logo.jpg"));
                Image logo = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                logo.ScalePercent(25);

                ReportHeader(headerTbl, logo, (LayawayPickingSlip)writer.PageEvent);
                //write the rows out to the PDF output stream. I use the height of the document to position the table. Positioning seems quite strange in iTextSharp and caused me the biggest headache.. It almost seems like it starts from the bottom of the page and works up to the top, so you may ned to play around with this.
                headerTbl.WriteSelectedRows(0, -1, 20, (document.PageSize.Height - 20), writer.DirectContent);
            }
            catch (Exception)
            {
                return;
            }
        }

        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            var pageN = writer.PageNumber;
            var text = string.Empty;
            //string[] reportName = writer.Info.Get(PdfName.TITLE).ToString().Split(':');
            text = string.Format("{0} Page {1} of ", StringUtilities.fillString(" ", 80), pageN);
            //PdfContentByte dc = writer.DirectContent;

            float len = footerBaseFont.GetWidthPoint(text, 8);

            contentByte.BeginText();
            //if last page, figure out if it is last page
            contentByte.SetFontAndSize(footerBaseFont, 8);
            //Madhu fix for defexct PWNU00001411
            contentByte.SetTextMatrix(len / 2, 30);
            contentByte.ShowText(text);
            contentByte.EndText();
            //Madhu fix for defexct PWNU00001411
            //contentByteFooter.AddTemplate(templateFooter, 25, document.PageSize.Height - 500);
            contentByte.AddTemplate(template, len + len / 2, 30);
        }
        #endregion
    }
}
