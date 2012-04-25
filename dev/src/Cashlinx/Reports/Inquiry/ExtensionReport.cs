using System;
using System.Data;
using System.IO;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Common.Libraries.Utility.Logger;

namespace Reports.Inquiry
{
    public class ExtensionReport : PdfPageEventHelper
    {
        //used by overriden methods for footers
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        //used by report
        private Font _reportFont;
        private Font _reportFontLargeBold;

        //main objects
        string _storeNr;
        string _storeName;
        DateTime _runDate;
        string _title;
        string reportTempFileFullName;

        public string ReportError;
        public int ReportErrorLevel;

        public ExtensionReport(string reportName, string storeNumber, string storeName, DateTime runDate, string title)
        {
            _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
            _reportFontLargeBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);


            _storeNr = storeNumber;
            _storeName = storeName;
            _runDate = runDate;
            _title = title;

            reportTempFileFullName = reportName;

        }

        public bool CreateReport(System.Data.DataSet theData)
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.LETTER);
            try
            {

                //set up RunReport event overrides & create doc
                ExtensionReport events = this;

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                //set up tables, etc...
                PdfPTable table = new PdfPTable(17);
                PdfPCell cell = new PdfPCell();
                Image gif = Image.GetInstance(Common.Properties.Resources.logo, BaseColor.WHITE);

                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);

                gif.ScalePercent(35);

                document.SetPageSize(PageSize.LETTER);
                document.SetMargins(20, 20, 10, 45);
                document.AddTitle("Loan Extension Detail Report : " + DateTime.Now.ToString("MM/dd/yyyy"));

                ReportHeader(table, gif);
                CustomerInfo(table, theData);
                //ColumnHeaders(table);


                //table.HeaderRows = 8;

                //ReportDetail(table, theData);
                ReportLines(table, true, "", true, _reportFont);
                document.Open();
                document.Add(table);
                document.Close();

                isSuccessful = true;
            }
            catch (DocumentException de)
            {
                ReportError = de.Message; ;
                ReportErrorLevel = (int)LogLevel.ERROR;
            }
            catch (IOException ioe)
            {
                ReportError = ioe.Message; ;
                ReportErrorLevel = (int)LogLevel.ERROR;
            }




            return isSuccessful;
        }


        private void ReportHeader(PdfPTable headingtable, Image gif)
        {

            PdfPCell cell = new PdfPCell();

            //  heading - row 1
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 11;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(_storeName + "\n Store Nr: " + _storeNr, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 6;
            headingtable.AddCell(cell);

            //  heading - row 2
            cell = new PdfPCell(new Paragraph("Inquiry", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingLeft = 12;
            cell.Colspan = 11;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(ReportHeaders.RUNDATE + DateTime.Now.ToString("MM/dd/yyyy"), _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.PaddingTop = -15;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 6;
            headingtable.AddCell(cell);


            // heading - row 4
            cell = new PdfPCell(new Phrase(_title, _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 17;
            headingtable.AddCell(cell);


        }

        private void CustomerInfo (PdfPTable table, System.Data.DataSet theData)
        {
            //System.Data.DataTable extData = theData.Tables["PAWN_CUST"];

            DataView extData = theData.DefaultViewManager.CreateDataView(theData.Tables["EXT_INFO"]);       
            PdfPTable customerRow = new PdfPTable(4);
            customerRow.SetWidthPercentage(new float[] { 50F, 50F, 50F, 50F },
                PageSize.LETTER);

            for (int i = 0; i < extData.Count; i++)
            {

                System.Data.DataRowView aRow = extData[i];
                System.Data.DataRow custRow = aRow.Row.GetParentRow("customerRelation");

                //set up tables, etc...
                PdfPCell cell ;
                //row 1
                cell = new PdfPCell(new Phrase(string.Format("Cust#: {0}", custRow["CUSTOMERNUMBER"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cell.VerticalAlignment = Rectangle.ALIGN_BOTTOM;
                cell.Border = Rectangle.TOP_BORDER;
                cell.PaddingTop = 10f;
                customerRow.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("Name: {0}", custRow["CUST_NAME"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cell.VerticalAlignment = Rectangle.ALIGN_BOTTOM;
                cell.Border = Rectangle.TOP_BORDER;
                cell.PaddingTop = 10f;
                customerRow.AddCell(cell);


                cell = new PdfPCell(new Phrase(string.Format("ID: {0}", custRow["ID"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cell.VerticalAlignment = Rectangle.ALIGN_BOTTOM;
                cell.Border = Rectangle.TOP_BORDER;
                cell.PaddingTop = 10f;
                customerRow.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("DOB: {0}", custRow["BIRTHDATE"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cell.VerticalAlignment = Rectangle.ALIGN_BOTTOM;
                cell.Border = Rectangle.TOP_BORDER;
                cell.PaddingTop = 10f;
                customerRow.AddCell(cell);

                cell = new PdfPCell(customerRow);
                cell.Colspan = table.NumberOfColumns;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell();
                cell.Colspan = table.NumberOfColumns;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 15F;
                table.AddCell(cell);

                LoanInfo(table, aRow);

                cell = new PdfPCell();
                cell.Colspan = table.NumberOfColumns;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 15F;
                table.AddCell(cell);

                ExtInfo(table, aRow);


            }
        }


        private void LoanInfo(PdfPTable theTableRow, DataRowView extension)
        {

            // Print Column Headers
            PdfPTable LoanHeader = new PdfPTable(8);
            LoanHeader.SetWidthPercentage(new float[] {35F, 35F, 35F, 35F, 35F, 35F, 35F, 35F },
                PageSize.LETTER);
            PdfPCell cell;


            cell = new PdfPCell(new Phrase("Ticket#", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Border = Rectangle.NO_BORDER;
            LoanHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date Made", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Border = Rectangle.NO_BORDER;
            LoanHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date Due", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Border = Rectangle.NO_BORDER;
            LoanHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase("PFI Elig", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Border = Rectangle.NO_BORDER;
            LoanHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase("Loan Amt", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Border = Rectangle.NO_BORDER;
            LoanHeader.AddCell(cell);



            cell = new PdfPCell(new Phrase("Finance", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Border = Rectangle.NO_BORDER;
            LoanHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase("Service", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Border = Rectangle.NO_BORDER;
            LoanHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase("Status", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Border = Rectangle.NO_BORDER;
            LoanHeader.AddCell(cell);





            // DATA Row
            cell = new PdfPCell(new Phrase(extension.Row["TICKET_NUMBER"].ToString(), _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            LoanHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:d}", extension.Row["DATE_MADE"]), _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            LoanHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:d}", extension.Row["DATE_DUE"]), _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            LoanHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:d}", extension.Row["PFI_ELIG"]), _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            LoanHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:c}",extension.Row["PRIN_AMOUNT"]), _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            LoanHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:c}",extension.Row["FIN_CHG"]), _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            LoanHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:c}",extension.Row["SERV_CHG"]), _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            LoanHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase(extension.Row["LOAN_STATUS"].ToString(), _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            LoanHeader.AddCell(cell);

            cell = new PdfPCell(LoanHeader);
            cell.Colspan = theTableRow.NumberOfColumns;
            cell.PaddingLeft = 50f;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            theTableRow.AddCell(cell);
            
        }

        private void ExtInfo(PdfPTable theTableRow, DataRowView extension)
        {
            PdfPTable extHeader = new PdfPTable(8);
            extHeader.SetWidthPercentage(new float[] { 35F, 35F, 35F, 35F, 35F, 35F, 35F, 35F },
                PageSize.LETTER);
            PdfPCell cell;


            cell = new PdfPCell(new Phrase("Ext Ticket#", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Border = Rectangle.NO_BORDER;
            extHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date Ext", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Border = Rectangle.NO_BORDER;
            extHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase("New Made", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Border = Rectangle.NO_BORDER;
            extHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase("New Due", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Border = Rectangle.NO_BORDER;
            extHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase("New PFI", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Border = Rectangle.NO_BORDER;
            extHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase("Ext Amt", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Border = Rectangle.NO_BORDER;
            extHeader.AddCell(cell);



            cell = new PdfPCell(new Phrase("Status", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Border = Rectangle.NO_BORDER;
            extHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase("St Date", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Border = Rectangle.NO_BORDER;
            extHeader.AddCell(cell);

            // DATA Row
            cell = new PdfPCell(new Phrase(extension.Row["RECEIPT_NUMBER"].ToString(), _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            extHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:d}", extension.Row["CREATIONDATE"]), _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            extHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:d}", extension.Row["NEW_MADE"]), _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            extHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:d}", extension.Row["NEW_DUE"]), _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            extHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:d}", extension.Row["NEW_PFI"]), _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            extHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:c}", extension.Row["REF_AMT"]), _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            extHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase(extension.Row["EXT_STATUS"].ToString(), _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            extHeader.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:d}",extension.Row["EXT_STATUS_DATE"]), _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            extHeader.AddCell(cell);

            cell = new PdfPCell(extHeader);
            cell.Colspan = theTableRow.NumberOfColumns;
            cell.PaddingLeft = 85f;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            theTableRow.AddCell(cell);


        }

        public void ReportLines(PdfPTable report, bool line, string stringline, bool endOfReport, Font font)
        {
            PdfPCell cell = new PdfPCell();
            //draw a single line
            if (line)
            {
                cell = new PdfPCell(new Phrase(""));
                cell.BorderWidthLeft = Rectangle.NO_BORDER;
                cell.BorderWidthTop = Rectangle.NO_BORDER;
                cell.BorderWidthRight = Rectangle.NO_BORDER;
                cell.Colspan = 21;

                report.AddCell(cell);
            }
            //draw a line from a string
            if (stringline.Length > 0)
            {
                cell = new PdfPCell(new Phrase(stringline));
                cell.Colspan = 21;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;

                report.AddCell(cell);
            }
            //print End of Report
            if (endOfReport)
            {
                cell = new PdfPCell(new Phrase("***End of Report***", font));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 21;
                report.AddCell(cell);
            }
        }

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
        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            int pageN = writer.PageNumber;
            string text = string.Empty;
            string[] reportName = writer.Info.Get(PdfName.TITLE).ToString().Split(':');
            text = string.Format("{0}{1} Page {2} of ", reportName[0], StringUtilities.fillString(" ", 150), pageN);

            float len = footerBaseFont.GetWidthPoint(text, 8);

            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            contentByte.SetTextMatrix(30, 30);
            contentByte.ShowText(text);
            contentByte.EndText();
            contentByte.AddTemplate(template, 30 + len, 30);
        }
    }
}
