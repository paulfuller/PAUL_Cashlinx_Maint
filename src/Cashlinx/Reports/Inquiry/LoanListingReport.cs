using System;
using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Common.Libraries.Utility.Logger;

namespace Reports.Inquiry
{
    public class LoanListingReport : PdfPageEventHelper
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
        private string _groupByField;

        private bool isPartialPaymentAllowed;

        public LoanListingReport(string report_name, string storeNumber, string storeName, DateTime runDate, string title, string groupBy)
        {
            _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
            _reportFontLargeBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);

            _storeNr = storeNumber;
            _storeName = storeName;
            _runDate = runDate;
            _title = title;
            _groupByField = groupBy;

            reportTempFileFullName = report_name;

            isPartialPaymentAllowed =
                new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(
                    GlobalDataAccessor.Instance.CurrentSiteId);
        }

        public bool CreateReport(System.Data.DataSet theData)
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.LETTER.Rotate());

            try
            {

                //set up RunReport event overrides & create doc
                LoanListingReport events = this;

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                //set up tables, etc...
                PdfPTable table = new PdfPTable(18);
                PdfPCell cell = new PdfPCell();
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);

                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);

                gif.ScalePercent(35);

                document.SetPageSize(PageSize.LETTER.Rotate());
                document.SetMargins(20, 20, 10, 45);
                document.AddTitle("Loan Inquiry Listing Report : " + DateTime.Now.ToString("MM/dd/yyyy"));

                ReportHeader(table, gif);
                ColumnHeaders(table);

                table.SetWidthPercentage (new float[] {35F, 45F, 45F, 45F, 75F, 40F, 45F, 55F, 40F, 55F, 45F, 45F, 40F, 50F, 25F, 25F, 25F, 25F}, 
                        PageSize.LETTER.Rotate());

                table.HeaderRows = 8;

                ReportDetail(table, theData);
                ReportLines(table, true, "", true, _reportFont);

                document.Open();
                document.Add(table);
                document.Close();

                isSuccessful = true;
            }
            catch (DocumentException de)
            {
                ReportError = de.Message;
                ReportErrorLevel = (int)LogLevel.ERROR;
            }
            catch (IOException ioe)
            {
                ReportError = ioe.Message;
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
            cell.Colspan = 12;
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
            cell.Colspan = 12;
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
            cell.Colspan = 18;
            headingtable.AddCell(cell);
        }

        private void ColumnHeaders(PdfPTable headingtable)
        {
            ReportLines(headingtable, false, StringUtilities.fillString("-", 184), false, _reportFont);

            //set up tables, etc...
            PdfPCell cell = new PdfPCell();

            //----------------------------------------------------------------------
            //row 1
            cell = new PdfPCell(new Phrase("Shop", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Customer Name", _reportFont));
            cell.Colspan = 3;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("ID", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Address", _reportFont));
            cell.Colspan = 4;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("City", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("State", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Zip Code", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;           
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("DOB", _reportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Wt.", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Ht.", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("S.", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("R.", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            //----------------------------------------------------------------------
            //row 2
            cell = new PdfPCell(new Phrase("", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Ticket #", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Previous #", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Orig #", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date / Time Made", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date Due", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            if (isPartialPaymentAllowed)
            {
                cell = new PdfPCell(new Phrase("Loan Amt.", _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                headingtable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Prin Amt.", _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                headingtable.AddCell(cell);
            }
            else
            {
                cell = new PdfPCell(new Phrase("Loan Amt.", _reportFont));
                cell.Colspan = 2;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                headingtable.AddCell(cell);
            }

            cell = new PdfPCell(new Phrase("Interest", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Service Chg", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Late Chg", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Other Chg", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Status", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Date", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Emp # Mad", _reportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Emp # Disp", _reportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);

            //----------------------------------------------------------------------
            //row 3
            cell = new PdfPCell(new Phrase("", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Catg Description", _reportFont));
            cell.Colspan = 9;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);   
    
            cell = new PdfPCell(new Phrase("Aisle", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell); 

            cell = new PdfPCell(new Phrase("Shelf", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Other", _reportFont));
            cell.Colspan = 4;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);
            
            cell = new PdfPCell(new Phrase("Gun #", _reportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);
        }

        private void ReportDetail(PdfPTable pdfTable, System.Data.DataSet theData)
        {
            PdfPCell cell = new PdfPCell();
            System.Data.DataTable pawnData = theData.Tables["PAWN_INFO"];
            string last_group = string.Empty;          

            for (int index = 0; index < pawnData.Rows.Count; index ++) 
            {
                System.Data.DataRow pawnLoan = pawnData.Rows[index];
                System.Data.DataRow [] customer = pawnLoan.GetChildRows("customerRelation");

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
                {
                    groupByValue = pawnLoan[_groupByField].ToString();
                }

                if (groupByValue != last_group)
                {
                    //row 1
                    cell = new PdfPCell(new Phrase("", _reportFont));
                    cell.Colspan = 18;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.BOTTOM_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(groupByValue, _reportFont));
                    cell.Colspan = 18;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.BOTTOM_BORDER;
                    pdfTable.AddCell(cell);

                    last_group = groupByValue;
                }

                //-----------------------------------------------------------------------------------
                //row 2
                cell = new PdfPCell(new Phrase(pawnLoan["STORENUMBER"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer[0]["CUST_NAME"].ToString(), _reportFont));
                cell.Colspan = 3;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer[0]["ID"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer[0]["ADDR1"].ToString(), _reportFont));
                cell.Colspan = 4;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer[0]["CITY"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer[0]["STATE"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer[0]["ZIPCODE"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer[0]["BIRTHDATE"].ToString(), _reportFont));
                cell.Colspan = 2;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer[0]["WEIGHT"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer[0]["HEIGHT"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer[0]["GENDERCODE"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(customer[0]["RACEDESC"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                //-----------------------------------------------------------------------------------
                //row 3
                cell = new PdfPCell(new Phrase("", _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0}", pawnLoan["TICKET_NUMBER"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0}", pawnLoan["PREV_TICKET"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0}", pawnLoan["ORG_TICKET"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:g}", pawnLoan["DATE_MADE"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:d}", pawnLoan["DATE_DUE"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                if (isPartialPaymentAllowed)
                {
                    cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["PRIN_AMOUNT"]), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["PartPymtPrinAmt"]), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);
                }
                else
                {
                    cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["PRIN_AMOUNT"]), _reportFont));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);
                }

                cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["INT_AMT"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["SERV_CHG"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["LATE_CHG"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:c}", pawnLoan["OTH_CHG"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["STATUS_CD"].ToString(), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(string.Format("{0:d}", pawnLoan["STATUS_DATE"]), _reportFont));
                cell.Colspan = 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["ENT_ID"].ToString(), _reportFont));
                cell.Colspan = 2;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(pawnLoan["DISP_ID"].ToString(), _reportFont));
                cell.Colspan = 2;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                //-----------------------------------------------------------------------------------
                //row 4
                System.Data.DataRow[] mdseList = pawnLoan.GetChildRows("merchandiseRelation");
                for (int j = 0; j < mdseList.Count<System.Data.DataRow>(); j++)
                {

                    cell = new PdfPCell(new Phrase("", _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(mdseList[j]["MD_DESC"].ToString(), _reportFont));
                    cell.Colspan = 9;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(mdseList[j]["LOC_AISLE"].ToString(), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(mdseList[j]["LOC_SHELF"].ToString(), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(mdseList[j]["LOCATION"].ToString(), _reportFont));
                    cell.Colspan = 6;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);
                }

                //-----------------------------------------------------------------------------------
                //row 5
                cell = new PdfPCell(new Phrase("", _reportFont));
                cell.Colspan = 18;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);

                //-----------------------------------------------------------------------------------
                //row 6
                cell = new PdfPCell(new Phrase("", _reportFont));
                cell.Colspan = 18;
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
                cell.Colspan = 18;

                report.AddCell(cell);
            }

            //draw a line from a string
            if (stringline.Length > 0)
            {
                cell = new PdfPCell(new Phrase(stringline));
                cell.Colspan = 18;
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
                cell.Colspan = 18;
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
            text = string.Format("{0}{1} Page {2} of ", reportName[0], StringUtilities.fillString(" ", 250), pageN);

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
