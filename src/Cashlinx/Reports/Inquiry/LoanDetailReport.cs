using System;
using System.Data;
using System.IO;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Common.Libraries.Utility.Logger;

namespace Reports.Inquiry
{
    public class LoanDetailReport : PdfPageEventHelper
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
        private string _title;
        private string reportTempFileFullName;

        public string ReportError;
        public int ReportErrorLevel;
        //string _groupByField;

        private bool isPartialPaymentAllowed;

        public LoanDetailReport(string reportName, string storeNumber, string storeName, DateTime runDate, string title)
        {
            _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
            _reportFontLargeBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);

            _storeNr = storeNumber;
            _storeName = storeName;
            _runDate = runDate;
            _title = title;

            reportTempFileFullName = reportName;

            isPartialPaymentAllowed =
                new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(
                    GlobalDataAccessor.Instance.CurrentSiteId);
        }

        public bool CreateReport(DataView theData)
        {
            bool isSuccessful = false;
            var document = new iTextSharp.text.Document(PageSize.LETTER);
            try
            {
                //set up RunReport event overrides & create doc
                LoanDetailReport events = this;

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                //set up tables, etc...
                var table = new PdfPTable(10);
                var cell = new PdfPCell();
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);

                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);

                gif.ScalePercent(35);

                document.SetPageSize(PageSize.LETTER.Rotate());
                document.SetMargins(5, 5, 10, 45);
                document.AddTitle(_title + ": " + DateTime.Now.ToString("MM/dd/yyyy"));

                ReportHeader(table, gif);

                CustomerData(table, theData);
                LoanData(table, theData);
                ItemHeader(table);
                table.SetWidthPercentage(new float[]
                    {
                            50F, 50F, 20F, 70F, 70F, 50F, 50F, 50F, 70F, 70F
                    }, PageSize.LETTER);

                table.HeaderRows = 14;

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
            DataView customer = theData[0].CreateChildView("customerRelation");

            var cell = new PdfPCell(new Phrase("Customer", _reportFontLargeBold));
            cell.Border = Rectangle.TOP_BORDER;
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


                cell = new PdfPCell(new Phrase("ID:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer[0]["ID"].ToString(), _reportFont));
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

                cell = new PdfPCell(new Phrase(string.Format( "{0:d}", customer[0]["BIRTHDATE"]), _reportFont));
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

                cell = new PdfPCell(new Phrase(customer[0]["WEIGHT"].ToString(), _reportFont));
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


                cell = new PdfPCell(new Phrase("Phone:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer[0]["PHONE"].ToString(), _reportFont));
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

                cell = new PdfPCell(new Phrase(customer[0]["GENDERCODE"].ToString(), _reportFont));
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

                cell = new PdfPCell(new Phrase(customer[0]["HEIGHT"].ToString(), _reportFont));
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
                cell = new PdfPCell(new Phrase("Adress:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer[0]["ADDR1"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 4;
                headingtable.AddCell(cell);




                cell = new PdfPCell(new Phrase("Race:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer[0]["GENDERCODE"].ToString(), _reportFont));
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

                cell = new PdfPCell(new Phrase(customer[0]["HAIR_COLOR_CODE"].ToString(), _reportFont));
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
                            customer[0]["ADDR2"].ToString(),
                            customer[0]["CITY"].ToString(), customer[0]["STATE"].ToString(),
                            customer[0]["ZIPCODE"].ToString()), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 6;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Eyes:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer[0]["EYE_COLOR_CODE"].ToString(), _reportFont));
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

        private void LoanData(PdfPTable headingtable, System.Data.DataView theData)
        {
            var cell = new PdfPCell(new Phrase("Ticket #", _reportFontLargeBold));
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
            DataView customer = theData[0].CreateChildView("customerRelation");

            #region ROW1
                // Row 1
                cell = new PdfPCell(new Phrase("Shop:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["STORENUMBER"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Date / Time Made:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:g}", pawnLoan["DATE_MADE"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                // if partial payments are not allowed, make these cells blank and the loan amount will be moved down.
                if (isPartialPaymentAllowed)
                {
                    cell = new PdfPCell(new Phrase("Loan Amount:", _reportFont));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Colspan = 1;
                    headingtable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["PRIN_AMOUNT"]), _reportFont));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Colspan = 1;
                    headingtable.AddCell(cell);
                }
                else
                {
                    cell = new PdfPCell(new Phrase("", _reportFont));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Colspan = 2;
                    headingtable.AddCell(cell);
                }

            cell = new PdfPCell(new Phrase("Status:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["STATUS_CD"].ToString(), _reportFont));
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
                cell = new PdfPCell(new Phrase("Previous #:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["PREV_TICKET"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Date Due:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:g}", pawnLoan["DATE_DUE"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                // if partial payments are not allowed, show the loan amount in these cells
                if (isPartialPaymentAllowed)
                {
                    cell = new PdfPCell(new Phrase("Prin. Amt:", _reportFont));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Colspan = 1;
                    headingtable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["PartPymtPrinAmt"]), _reportFont));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Colspan = 1;
                    headingtable.AddCell(cell);
                }
                else
                {
                    cell = new PdfPCell(new Phrase("Loan Amount:", _reportFont));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Colspan = 1;
                    headingtable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["PRIN_AMOUNT"]), _reportFont));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Colspan = 1;
                    headingtable.AddCell(cell);                    
                }


            cell = new PdfPCell(new Phrase("Negotiated?:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["FIN_NEG"].ToString(), _reportFont));
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
                // Row 4
                cell = new PdfPCell(new Phrase("Original #:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["ORG_TICKET"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("PFI Eligible:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:g}", pawnLoan["PFI_ELIG"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Interest:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["INT_AMT"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);


                cell = new PdfPCell(new Phrase("Negotiated?:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["SERV_NEG"].ToString(), _reportFont));
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

            #region ROW5
                // Row 5
                cell = new PdfPCell(new Phrase("Made Drawer:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["CASH_DRAWER"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("PFI Notice:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:g}", pawnLoan["PFI_NOTE"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Service Charge:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["SERV_CHG"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);


                cell = new PdfPCell(new Phrase("Hold?:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["HOLD_TYPE"].ToString(), _reportFont));
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

            #region ROW6
                // Row 6
                cell = new PdfPCell(new Phrase("Emp # Made:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["ENT_ID"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Status Date / Time:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:g}", pawnLoan["STATUS_DATE"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Late Charge:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["LATE_CHG"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Extend?", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["EXTEND"].ToString(), _reportFont));
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

            #region ROW7
                // Row 7
                cell = new PdfPCell(new Phrase("Emp # Disp:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["DISP_ID"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Manual Entry Date:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase());
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Refund Amount:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["REFUND_AMOUNT"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Clothing:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["CLOTHING"].ToString(), _reportFont));
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

            #region ROW8
                // Row 8
                cell = new PdfPCell(new Phrase("Disp Drawer:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["DISP_DRAWER"].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Original Cust #:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase());
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Other Charges:", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["OTH_CHG"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase());
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 3;
                headingtable.AddCell(cell);
            #endregion

            #region Last Row
            cell = new PdfPCell(new Phrase());
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("PU / RN Customer:", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase(pawnLoan["PU_CUST_NUM"].ToString(), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Original Cust #:", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase());
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase());
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("", _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 10;
            headingtable.AddCell(cell);
            #endregion
        }

        private void ItemHeader(PdfPTable headingtable)
        {
            var cell = new PdfPCell(new Phrase("Item #", _reportFontLargeBold));
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Category", _reportFontLargeBold));
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Description", _reportFontLargeBold));
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 5;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Location", _reportFontLargeBold));
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Gun Number", _reportFontLargeBold));
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            headingtable.AddCell(cell);
        }

        private void ReportDetail(PdfPTable pdfTable, System.Data.DataView theData)
        {
            DataView mdse = theData[0].CreateChildView("merchandiseRelation");

            for (int recordIndex = 0; recordIndex < mdse.Count; recordIndex++) 
            {
                var cell = new PdfPCell(new Phrase(string.Format("{0}", recordIndex), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0}", mdse[recordIndex]["CAT_CODE"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0}", mdse[recordIndex]["MD_DESC"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 5;
                pdfTable.AddCell(cell);

                string shelf = string.Empty;

                if (!(mdse[recordIndex]["LOC_SHELF"] is DBNull))
                {
                    shelf = string.Format("shelf: {0}", mdse[recordIndex]["LOC_SHELF"]);
                }

                string aisle = string.Empty;

                if (!(mdse[recordIndex]["LOC_AISLE"] is DBNull))
                {
                    aisle = string.Format("aisle: {0}", mdse[recordIndex]["LOC_AISLE"]);
                }

                cell = new PdfPCell(new Phrase(string.Format("{0}{1}{2}", shelf, aisle, mdse[recordIndex]["LOCATION"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 2;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0}", mdse[recordIndex]["GUN_NUMBER"]), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 1;
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
