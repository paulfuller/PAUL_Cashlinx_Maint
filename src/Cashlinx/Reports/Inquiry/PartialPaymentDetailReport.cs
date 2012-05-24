using System;
using System.Data;
using System.IO;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Common.Libraries.Utility.Logger;

namespace Reports.Inquiry
{
    public class PartialPaymentDetailReport : PdfPageEventHelper
    {
        //used by overriden methods for footers
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        //used by report
        private Font _reportFont;
        private Font _reportFontLargeBold;

        //main objects
        private string _storeNr;
        private string _storeName;
        private DateTime _runDate;
        private string _title = "Partial Payment Inquiry Detail";
        private string reportTempFileFullName;

        public string ReportError;
        public int ReportErrorLevel;
        //string _groupByField;

        public PartialPaymentDetailReport(string reportName, string storeNumber, string storeName, DateTime runDate)
        {
            _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
            _reportFontLargeBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);

            _storeNr = storeNumber;
            _storeName = storeName;
            _runDate = runDate;

            reportTempFileFullName = reportName;
        }

        public bool CreateReport(DataView theData)
        {
            bool isSuccessful = false;
            var document = new iTextSharp.text.Document(PageSize.HALFLETTER.Rotate());
            try
            {
                //set up RunReport event overrides & create doc
                PartialPaymentDetailReport events = this;

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                //set up tables, etc...
                var table = new PdfPTable(10);
                var cell = new PdfPCell();
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);

                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);

                gif.ScalePercent(35);

                document.SetPageSize(PageSize.HALFLETTER.Rotate());
                document.SetMargins(5, 5, 10, 45);
                document.AddTitle(_title + ": " + DateTime.Now.ToString("MM/dd/yyyy"));

                ReportHeader(table, gif);

                CustomerData(table, theData);
                PartialPaymentData(table, theData);
                table.SetWidthPercentage(new float[]
                {
                        50F, 100F, 20F, 70F, 90F, 50F, 50F, 50F, 70F, 70F
                }, PageSize.HALFLETTER.Rotate());

                ReportDetailHeader(table);

                ReportDetail(table, theData);

                ReportLines(table, true, string.Empty, true, _reportFont);
                document.Open();
                document.Add(table);
                document.Close();

                isSuccessful = true;

            }
            catch(DocumentException de)
            {
                ReportError = de.Message;
                ReportErrorLevel = (int)LogLevel.ERROR;
            }
            catch(IOException ioe)
            {
                ReportError = ioe.Message;
                ReportErrorLevel = (int)LogLevel.ERROR;
            }

            return isSuccessful;
        }

        private void ReportHeader(PdfPTable headingtable, Image gif)
        {
            PdfPCell cell;

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
            cell.Colspan = 4;
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
            cell.Colspan = 4;
            headingtable.AddCell(cell);


            // heading - row 4
            cell = new PdfPCell(new Phrase(_title, _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 10;
            cell.PaddingBottom = 20;
            headingtable.AddCell(cell);

        }

        private void CustomerData(PdfPTable headingtable, System.Data.DataView theData)
        {
            DataRowView pawnLoan = theData[0];

            var cell = new PdfPCell(new Phrase("Customer", _reportFontLargeBold));
            cell.Border = Rectangle.TOP_BORDER + Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 10;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("", _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 10;
            headingtable.AddCell(cell);

            #region ROW1
            // Row 1
                cell = new PdfPCell(new Phrase("Customer #:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["CUSTOMERNUMBER"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Supp ID:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["SUP_ID"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);


                cell = new PdfPCell(new Phrase("DOB:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:d}", pawnLoan["BIRTHDATE"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Wt:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["WEIGHT"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase());
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);
            #endregion

            #region ROW2
                // Row 2
                cell = new PdfPCell(new Phrase("Name:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["CUST_NAME"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("ID:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["ID"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Sex:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["GENDERCODE"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Ht:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["HEIGHT"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase());
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);
            #endregion

            #region ROW3
                // Row 3
                cell = new PdfPCell(new Phrase("Address:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["ADDR1"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("SSN:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["SSN"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);


                cell = new PdfPCell(new Phrase("Race:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["RACEDESC"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Hair:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["HAIR_COLOR_CODE"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase());
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);
            #endregion

            #region ROW4
                // Row 4+
                cell = new PdfPCell(new Phrase());
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(
                        string.Format("{0}\n{1},{2}  {3}",
                            pawnLoan["ADDR2"].ToString(),
                            pawnLoan["CITY"].ToString(),
                            pawnLoan["STATE"].ToString(),
                            pawnLoan["ZIPCODE"].ToString()), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Phone:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["PHONE"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 3;
                headingtable.AddCell(cell);


                cell = new PdfPCell(new Phrase("Eyes:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["EYE_COLOR_CODE"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase());
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

            #endregion

            cell = new PdfPCell(new Phrase("", _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 10;
            headingtable.AddCell(cell);
        }

        private void PartialPaymentData(PdfPTable headingtable, System.Data.DataView theData)
        {
            var cell = new PdfPCell(new Phrase("Partial Payment Detail", _reportFontLargeBold));
            cell.Border = Rectangle.TOP_BORDER + Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 10;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("", _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 10;
            headingtable.AddCell(cell);

            DataRowView pawnLoan = theData[0];

            #region ROW1
                // Row 1
                cell = new PdfPCell(new Phrase("Principal Reduction:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["pmt_prin_amount"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 3;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Payment Date:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:d}", pawnLoan["PAYMENT_DATE_MADE"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 3;
                headingtable.AddCell(cell);
            #endregion

            #region ROW2
                // Row 2
                cell = new PdfPCell(new Phrase("Service Charge:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["PMT_SERV_AMT"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 3;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Status:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["STATUS_CD"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 3;
                headingtable.AddCell(cell);
            #endregion

            #region ROW3
                // Row 3
                cell = new PdfPCell(new Phrase("Interest:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["PMT_INT_AMT"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 3;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Payment Method:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["PAYMENT_METHOD"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 3;
                headingtable.AddCell(cell);
            #endregion

            #region ROW4
                // Row 4
                cell = new PdfPCell(new Phrase("Late Fee:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["LATE_FEE"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 3;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Empl Id:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["CAI_EMPL_NUM"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 3;
                headingtable.AddCell(cell);

            #endregion

            #region ROW5
                // Row 5
                cell = new PdfPCell(new Phrase("Total Payment:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["PMT_AMOUNT"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 3;
                headingtable.AddCell(cell);


                cell = new PdfPCell(new Phrase("", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 5;
                headingtable.AddCell(cell);
            #endregion

            // Blank Line
            cell = new PdfPCell(new Phrase("", _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 10;
            headingtable.AddCell(cell);

        }

        private void ReportDetailHeader(PdfPTable pdfTable)
        {
            var cell = new PdfPCell(new Phrase("Loan #", _reportFontLargeBold));
            cell.Border = Rectangle.TOP_BORDER + Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Loan Date Made", _reportFontLargeBold));
            cell.Border = Rectangle.TOP_BORDER + Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Due Date", _reportFontLargeBold));
            cell.Border = Rectangle.TOP_BORDER + Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("PFI Eligible", _reportFontLargeBold));
            cell.Border = Rectangle.TOP_BORDER + Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Status", _reportFontLargeBold));
            cell.Border = Rectangle.TOP_BORDER + Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Loan Amount", _reportFontLargeBold));
            cell.Border = Rectangle.TOP_BORDER + Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Current Principal Amount", _reportFontLargeBold));
            cell.Border = Rectangle.TOP_BORDER + Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("", _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 10;
            pdfTable.AddCell(cell);
        }

        private void ReportDetail(PdfPTable pdfTable, System.Data.DataView theData)
        {
            DataRowView pawnLoan = theData[0];

            var cell = new PdfPCell(new Phrase(pawnLoan["TICKET_NUMBER"].ToString(), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:d}", pawnLoan["DATE_MADE"]), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:d}", pawnLoan["DATE_DUE"]), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:d}", pawnLoan["PFI_ELIG"]), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase(pawnLoan["LOAN_STATUS_CD"].ToString(), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["LOAN_AMOUNT"]), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;
            pdfTable.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["CURRENT_PRIN_AMOUNT"]), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;
            pdfTable.AddCell(cell);
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
                cell.Colspan = 10;

                report.AddCell(cell);
            }
            //draw a line from a string
            if (stringline.Length > 0)
            {
                cell = new PdfPCell(new Phrase(stringline));
                cell.Colspan = 10;
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
                cell.Colspan = 10;
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
            var pageN = writer.PageNumber;
            var reportName = writer.Info.Get(PdfName.TITLE).ToString().Split(':');
            string text = reportName[0] + StringUtilities.fillString(" ", 150) + " Page " + pageN + " of ";

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

