using System;
using System.IO;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Common.Libraries.Utility.Logger;

namespace Reports.Inquiry
{
    public class ExtensionListingReport : PdfPageEventHelper
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
        string _groupByField;

        public ExtensionListingReport(string reportName, string storeNumber, string storeName, DateTime runDate, string title, string groupBy)
        {
            _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
            _reportFontLargeBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);


            _storeNr = storeNumber;
            _storeName = storeName;
            _runDate = runDate;
            _title = title;
            _groupByField = groupBy;

            reportTempFileFullName = reportName;

        }


        public bool CreateReport(System.Data.DataSet theData)
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.LETTER);
            try
            {

                //set up RunReport event overrides & create doc
                ExtensionListingReport events = this;

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                //set up tables, etc...
                PdfPTable table = new PdfPTable(9);
                Image gif = Image.GetInstance(Common.Properties.Resources.logo, BaseColor.WHITE);

                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);

                gif.ScalePercent(35);

                document.SetPageSize(PageSize.LETTER);
                document.SetMargins(-50, -50, 10, 45);
                document.AddTitle("Extension Inquiry Listing Report : " + DateTime.Now.ToString("MM/dd/yyyy"));
                table.SetWidthPercentage(new float[] { 35F, 50F, 50F, 50F, 50F, 50F, 50F, 50F, 55F },
                        PageSize.LETTER);

                ReportHeader(table, gif);
                ColumnHeaders(table);                //CustomerInfo(table, theData);



                table.HeaderRows = table.Rows.Count;

                ReportDetail(table, theData);
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
            cell.Colspan = 6;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(_storeName + "\n Store Nr: " + _storeNr, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            //  heading - row 2
            cell = new PdfPCell(new Paragraph("Inquiry", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingLeft = 12;
            cell.Colspan = 6;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(ReportHeaders.RUNDATE + DateTime.Now.ToString("MM/dd/yyyy"), _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.PaddingTop = -15;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 3;
            headingtable.AddCell(cell);


            // heading - row 4
            cell = new PdfPCell(new Phrase(_title, _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 9;
            headingtable.AddCell(cell);
        }

        private void ColumnHeaders(PdfPTable headingtable)
        {

            ReportLines(headingtable, false, StringUtilities.fillString("-", 127), false, _reportFont);

            //set up tables, etc...
            PdfPCell cell;

            //row 1
            cell = new PdfPCell(new Phrase("Cust #", _reportFont));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Customer Name", _reportFont));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("ID", _reportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("DOB", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            //row 2
            cell = new PdfPCell();
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Ticket #", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date Made", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date Due", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("PFI Elig", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Loan Amt", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Finance", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Service", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Status", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            //row 3
            cell = new PdfPCell();
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Ext Ticket #", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date Ext.", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("New Made", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("New Due", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("New PFI", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Ext. Amt", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Status", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("St. Date", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);
        }

        private void ReportDetail(PdfPTable pdfTable, System.Data.DataSet theData)
        {
            PdfPCell cell = new PdfPCell();
            System.Data.DataTable pawnData = theData.Tables["EXT_INFO"];
            string last_group = "";

            for (int i = 0; i < pawnData.Rows.Count; i++)
            {
                System.Data.DataRow pawnLoan = pawnData.Rows[i];
                System.Data.DataRow customer = pawnLoan.GetParentRow("customerRelation");

                object groupBy = pawnLoan[_groupByField];
                string groupByValue;
                if (groupBy is DateTime)
                {
                    groupByValue = ((DateTime)pawnLoan[_groupByField]).FormatDate();
                }
                else if (groupBy is double)
                {
                    groupByValue = string.Format("{0:c}", pawnLoan[_groupByField]);
                }
                else
                    groupByValue = pawnLoan[_groupByField].ToString();

                if (groupByValue != last_group)
                {
                    cell = new PdfPCell(new Phrase("", _reportFont));
                    cell.Colspan = 9;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    //row 1
                    cell = new PdfPCell(new Phrase("", _reportFont));
                    cell.Colspan = 9;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.BOTTOM_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(groupByValue, _reportFont));
                    cell.Colspan = 9;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.BOTTOM_BORDER;
                    pdfTable.AddCell(cell);

                    last_group = groupByValue;
                }

                //row 2

                cell = new PdfPCell(new Phrase(customer["CUSTOMERNUMBER"].ToString(), _reportFont));
                cell.Colspan = 3;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer["CUST_NAME"].ToString(), _reportFont));
                cell.Colspan = 3;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer["ID"].ToString(), _reportFont));
                cell.Colspan = 2;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer["BIRTHDATE"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                // row 3
                cell = new PdfPCell();
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["TICKET_NUMBER"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:d}",pawnLoan["DATE_MADE"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:d}",pawnLoan["DATE_DUE"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:d}",pawnLoan["PFI_ELIG"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["PRIN_AMOUNT"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["FIN_CHG"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["SERV_CHG"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["LOAN_STATUS"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);


                // row 4
                cell = new PdfPCell();
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["RECEIPT_NUMBER"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:d}",pawnLoan["REF_DATE"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:d}",pawnLoan["NEW_MADE"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:d}",pawnLoan["NEW_DUE"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:d}",pawnLoan["NEW_PFI"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["REF_AMT"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["EXT_STATUS"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:d}",pawnLoan["CREATIONDATE"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);


                // blank line after record
                cell = new PdfPCell();
                cell.Colspan = 9;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);
            }

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
                cell.Colspan = 9;

                report.AddCell(cell);
            }
            //draw a line from a string
            if (stringline.Length > 0)
            {
                cell = new PdfPCell(new Phrase(stringline));
                cell.Colspan = 9;
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
                cell.Colspan = 9;
                report.AddCell(cell);
            }
        }


        // we override the OnOpenDocument, OnCloseDocument & OnEndPage methods to get footers
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
