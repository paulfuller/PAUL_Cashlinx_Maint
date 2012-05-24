using System;
using Common.Libraries.Objects;
using Common.Libraries.Utility.String;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ReportObject = Common.Controllers.Application.ReportObject;
using Common.Libraries.Utility.Logger;

namespace Reports
{
    public class InventoryChargeOffReport : ReportBase
    {
        #region Fields
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        public RunReport runReport;
        #endregion

        public InventoryChargeOffReport(IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {

        }

        #region Private Methods
        private void WriteDetail(PdfPTable detailsTable, int tableColspan)
        {
            //WriteCell(detailsTable, "ICN:", ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            Phrase spacePhrase = new Phrase(new Chunk(" ") + "", ReportFontBold);
            Phrase icnLinePhrase = new Phrase("", ReportFont);
            Phrase icnLabelPhrase = new Phrase("ICN:", ReportFontBold);
            Phrase icnPhrase = new Phrase(GetFormattedICNPhrase(ReportObject.InventoryChargeOffFields.ICN));

            icnLinePhrase.Add(icnLabelPhrase);
            icnLinePhrase.Add(spacePhrase);
            icnLinePhrase.Add(icnPhrase);

            PdfPCell cell = new PdfPCell();
            cell.Colspan = 2;
            cell.AddElement(icnLinePhrase);
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            detailsTable.AddCell(cell);

            WriteCell(detailsTable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(detailsTable, "Emp#:", ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(detailsTable, ReportObject.InventoryChargeOffFields.EmployeeNumber, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            WriteCell(detailsTable, string.Empty, ReportFont, tableColspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(detailsTable, ReportObject.InventoryChargeOffFields.MerchandiseDescription, ReportFont, 2, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(detailsTable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(detailsTable, "Charge Off Amount:", ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(detailsTable, ReportObject.InventoryChargeOffFields.ChargeOffAmount, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(detailsTable, string.Empty, ReportFont, tableColspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            //new line
            WriteCell(detailsTable, string.Empty, ReportFont, tableColspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(detailsTable, "Reason for Charge Off:", ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(detailsTable, ReportObject.InventoryChargeOffFields.ReasonForChargeOff, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(detailsTable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            if (string.IsNullOrEmpty(ReportObject.InventoryChargeOffFields.ReplacementLoanNumber))
            {
                WriteCell(detailsTable, string.Empty, ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(detailsTable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            }
            else
            {
                WriteCell(detailsTable, "Replacement Loan#:", ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(detailsTable, ReportObject.InventoryChargeOffFields.ReplacementLoanNumber, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            }

            //new line
            WriteCell(detailsTable, string.Empty, ReportFont, tableColspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(detailsTable, "Comment:", ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(detailsTable, ReportObject.InventoryChargeOffFields.Comment, ReportFont, 2, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            if (string.IsNullOrEmpty(ReportObject.InventoryChargeOffFields.CustomerName))
            {
                WriteCell(detailsTable, string.Empty, ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(detailsTable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            }
            else
            {
                WriteCell(detailsTable, "Customer Name:", ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(detailsTable, ReportObject.InventoryChargeOffFields.CustomerName, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            }

            //new line
            WriteCell(detailsTable, string.Empty, ReportFont, tableColspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(detailsTable, "Authorized By:", ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(detailsTable, ReportObject.InventoryChargeOffFields.AuthorizedBy, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
          
            WriteCell(detailsTable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            if (string.IsNullOrEmpty(ReportObject.InventoryChargeOffFields.CharitableOrganization))
            {
                WriteCell(detailsTable, string.Empty, ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(detailsTable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            }
            else
            {
                WriteCell(detailsTable, "Charitable Organization:", ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(detailsTable, ReportObject.InventoryChargeOffFields.CharitableOrganization, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            }

            //new line
            WriteCell(detailsTable, string.Empty, ReportFont, tableColspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            if (!string.IsNullOrEmpty(ReportObject.InventoryChargeOffFields.PoliceCaseNumber))
            {
                WriteCell(detailsTable, "Police Case #:", ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(detailsTable, ReportObject.InventoryChargeOffFields.PoliceCaseNumber, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            }
            else
            {
                WriteCell(detailsTable, string.Empty, ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(detailsTable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            }
            WriteCell(detailsTable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            if (!string.IsNullOrEmpty(ReportObject.InventoryChargeOffFields.ATFIncidentNumber))
            {
                WriteCell(detailsTable, "ATF Incident #:", ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(detailsTable, ReportObject.InventoryChargeOffFields.ATFIncidentNumber, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            }
            else
            {
                WriteCell(detailsTable, string.Empty, ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(detailsTable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            }
            //add gun number here if it exists

            if (ReportObject.InventoryChargeOffFields.IsGun)
            {
                WriteCell(detailsTable, string.Empty, ReportFont, tableColspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(detailsTable, "Gun #:", ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(detailsTable, ReportObject.InventoryChargeOffFields.GunNumber.ToString(), ReportFont, 4, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            }
        }

        private void ReportHeader(PdfPTable headingtable, Image gif, InventoryChargeOffReport pageEvent)
        {
            PdfPCell cell = new PdfPCell();

            //  row 1
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 5;
            headingtable.AddCell(cell);

            //row 2
            Phrase dateLabelPhrase = new Phrase("Date:", ReportFontBold);
            Phrase spacePhrase = new Phrase(new Chunk(" ") + "", ReportFont);
            Phrase datePhrase = new Phrase(DateTime.Now.ToShortDateString(), ReportFontBold);
            dateLabelPhrase.Add(spacePhrase);
            dateLabelPhrase.Add(datePhrase);
            cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;
            cell.AddElement(dateLabelPhrase);
            headingtable.AddCell(cell);

            
            //WriteCell(headingtable, "Date:", ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            //WriteCell(headingtable, "4/27/2011", ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, "Charge Off#:", ReportFontBold, 1, Element.ALIGN_RIGHT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, pageEvent.ReportObject.InventoryChargeOffFields.ChargeOffNumber, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            //row 3
            WriteCell(headingtable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, "INVENTORY CHARGE OFF", ReportFontHeading, 3, Element.ALIGN_CENTER, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            Phrase timeLabelPhrase = new Phrase("Time:", ReportFontBold);
            Phrase timePhrase = new Phrase(DateTime.Now.ToShortTimeString(), ReportFont);
            timeLabelPhrase.Add(spacePhrase);
            timeLabelPhrase.Add(timePhrase);
            cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;
            cell.AddElement(timeLabelPhrase);
            headingtable.AddCell(cell);

            //WriteCell(headingtable, "Time:", ReportFontBold, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            //WriteCell(headingtable, "4:08 P.M", ReportFont, 1, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, string.Empty, ReportFont, 3, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, string.Empty, ReportFont, 5, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.BOTTOM_BORDER);
            //row 4
            //WriteCell(headingtable, string.Empty, ReportFont, 5, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            //DrawLine(headingtable);
        }
        #endregion

        #region Public Properties
        public ReportObject ReportObject { get; set; }

        #endregion

        #region Public Methods
        public bool CreateReport(bool openFile)
        {
            var isSuccessful = false;
            //openFile = true; //comment out
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.HALFLETTER.Rotate());
            try
            {
                //set up RunReport event overrides & create doc
                InventoryChargeOffReport events = this;
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ReportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                MultiColumnText columns = new MultiColumnText(document.PageSize.Top - 80, MultiColumnText.AUTOMATIC);
                float pageLeft = document.PageSize.Left;
                float pageright = document.PageSize.Right;
                columns.AddSimpleColumn(1, document.PageSize.Width + 40);

                //set up tables, etc...
                PdfPTable table = new PdfPTable(5);
                table.WidthPercentage = 85;// document.PageSize.Width;
                PdfPCell cell = new PdfPCell();
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(25);
                runReport = new RunReport();
                document.Open();
                document.SetPageSize(PageSize.HALFLETTER.Rotate());
                document.SetMargins(-100, -100, 10, 45);
                document.AddTitle(string.Format("{0}: {1}", ReportObject.ReportTitle, DateTime.Now.ToString("MM/dd/yyyy")));

                //here add detail
                WriteDetail(table, 5);
                columns.AddElement(table);
                document.Add(columns);
                document.Close();
                if(openFile)
                    OpenFile(ReportObject.ReportTempFileFullName);
                
                //CreateReport(true); //comment out
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
                PdfPTable headerTbl = new PdfPTable(5);
                //set the width of the table to be the same as the document
                headerTbl.TotalWidth = document.PageSize.Width - 50;
                //headerTbl.WidthPercentage = 80;
                //I use an image logo in the header so I need to get an instance of the image to be able to insert it. I believe this is something you couldn't do with older versions of iTextSharp
                //Image logo = Image.GetInstance(HttpContext.Current.Server.MapPath("/images/logo.jpg"));
                Image logo = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                logo.ScalePercent(25);
                ReportHeader(headerTbl, logo, (InventoryChargeOffReport)writer.PageEvent);
                //write the rows out to the PDF output stream. I use the height of the document to position the table. Positioning seems quite strange in iTextSharp and caused me the biggest headache.. It almost seems like it starts from the bottom of the page and works up to the top, so you may ned to play around with this.
                headerTbl.WriteSelectedRows(0, -1, 35, (document.PageSize.Height - 10), writer.DirectContent);
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
            var reportName = writer.Info.Get(PdfName.TITLE).ToString().Split(':');
            text = string.Format("{0} Page {1} of ", StringUtilities.fillString(" ", 80), pageN);
            var len = footerBaseFont.GetWidthPoint(text, 8);
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
