using System;
using System.IO;
using Common.Controllers.Application;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports
{
    public class PoliceHoldReport : PdfPageEventHelper
    {
        private string _reportTempFileFullName = string.Empty;
        public string ReportTempFileFullName
        {
            get { return _reportTempFileFullName; }
            set { _reportTempFileFullName = value; }
        }

        private string _STORE_NAME = string.Empty;
        public string STORE_NAME
        {
            get { return _STORE_NAME; }
            set { _STORE_NAME = value; }
        }

        private string _STORE_ADDRESS = string.Empty;
        public string STORE_ADDRESS
        {
            get { return _STORE_ADDRESS; }
            set { _STORE_ADDRESS = value; }
        }

        private string _STORE_CITY = string.Empty;
        public string STORE_CITY
        {
            get { return _STORE_CITY; }
            set { _STORE_CITY = value; }
        }

        private string _STORE_STATE = string.Empty;
        public string STORE_STATE
        {
            get { return _STORE_STATE; }
            set { _STORE_STATE = value; }
        }

        private string _STORE_ZIP = string.Empty;
        public string STORE_ZIP
        {
            get { return _STORE_ZIP; }
            set { _STORE_ZIP = value; }
        }

        private CustomerVO _currentCust = null;
        public CustomerVO CurrentCust
        {
            get { return _currentCust; }
            set { _currentCust = value; }
        }

        private AddressVO _custHomeAddr = null;
        public AddressVO CustHomeAddr
        {
            get { return _custHomeAddr; }
            set { _custHomeAddr = value; }
        }

        private int _numOfSeizedLoans;
        public int NumOfSeizedLoans
        {
            get { return _numOfSeizedLoans; }
            set { _numOfSeizedLoans = value; }
        }

        private string _empNo = string.Empty;
        public string EmpNo
        {
            get { return _empNo; }
            set { _empNo = value; }
        }


        public HoldData HoldData
        {
            get;
            set;
        }

        //string empNo = "";
        //int numofSeizeLoans = 0;
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        //used by report
        private Font _reportFont;
        private Font _reportFontLargeBold;
        private Font _reportFontUnderlined;

        //main objects
        public ReportObject reportObject;
        public RunReport runReport;

        public bool CreateReport()
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.HALFLETTER);
            try
            {
                //ReportTempFileFullName = @"C:\pdfs\PoliceHoldSample.pdf";
                //set up RunReport event overrides & create doc
                PoliceSeizeReport events = new PoliceSeizeReport();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                //set up tables, etc...
                PdfPTable headerTableNoBorders = new PdfPTable(11);
                PdfPTable headerTableWithBorders = new PdfPTable(11);
                PdfPTable detailsTable = new PdfPTable(11);
                PdfPTable footerTableWithBorders = new PdfPTable(11);
                PdfPTable footerTableNoBorders = new PdfPTable(11);
                PdfPCell cell = new PdfPCell();
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                _reportFont = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD);
                _reportFontUnderlined = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.UNDERLINE);

                gif.ScalePercent(20);
                runReport = new RunReport();

                document.SetPageSize(PageSize.HALFLETTER.Rotate());
                document.SetMargins(-40, -40, 5, 23);
                document.AddTitle(string.Empty);

                ReportHeaderNoBorder(headerTableNoBorders);
                ReportHeaderWithBorder(headerTableWithBorders);
                ReportDetails(detailsTable);
                ReportFooterNoBorders(footerTableNoBorders);
                ReportFooterWithBorders(footerTableWithBorders);

                //headerTableNoBorders.HeaderRows = 15;

                document.Open();
                document.Add(headerTableNoBorders);
                document.Add(headerTableWithBorders);
                document.Add(detailsTable);
                document.Add(footerTableWithBorders);
                document.Add(footerTableNoBorders);
                document.Close();

                isSuccessful = true;

            }
            catch (DocumentException /*de*/)
            {
                //reportObject.ReportError = de.Message; ;
                //reportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            catch (IOException /*ioe*/)
            {
                //reportObject.ReportError = ioe.Message; ;
                //reportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            return isSuccessful;
        }

        private void ReportHeaderNoBorder(PdfPTable headerTableNoBorder)
        {
            PdfPCell cell = null;
            cell = new PdfPCell(new Paragraph("Hold Ticket", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            //no ticket SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 11;
            headerTableNoBorder.AddCell(cell);
            WriteBlankRow(headerTableNoBorder, 1);

            WriteColumn(headerTableNoBorder, 11, STORE_NAME, Element.ALIGN_CENTER, _reportFont);
            WriteColumn(headerTableNoBorder, 11, STORE_ADDRESS, Element.ALIGN_CENTER, _reportFont);
            WriteColumn(headerTableNoBorder, 11, STORE_CITY + " " + STORE_STATE + " " + STORE_ZIP, Element.ALIGN_CENTER, _reportFont);
            //put code in here to get the other 3 lines of headers
            WriteBlankRow(headerTableNoBorder, 3);
            cell = new PdfPCell(new Paragraph("Date: " + DateTime.Now.ToShortDateString(), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            //no ticket SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 11;
            headerTableNoBorder.AddCell(cell);
        }

        private void ReportHeaderWithBorder(PdfPTable headerTableNoBorder)
        {
            WriteBorderColumn(headerTableNoBorder, 2, "Customer First, Last:", Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(headerTableNoBorder, 1, "DOB:", Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(headerTableNoBorder, 4, "Address:", Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(headerTableNoBorder, 1, "Employee Number:", Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(headerTableNoBorder, 1, "Transaction Number:", Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(headerTableNoBorder, 2, "Transaction Amt/Cost:", Element.ALIGN_LEFT, _reportFont);

            WriteBorderColumn(headerTableNoBorder, 2, CurrentCust.CustomerName, Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(headerTableNoBorder, 1, CurrentCust.DateOfBirth.FormatDate(), Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(headerTableNoBorder, 4, CurrentCust.CustHomeAddress, Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(headerTableNoBorder, 1, GlobalDataAccessor.Instance.DesktopSession.UserName, Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(headerTableNoBorder, 1, HoldData.TicketNumber.ToString(), Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(headerTableNoBorder, 2, HoldData.Amount.ToString("C"), Element.ALIGN_LEFT, _reportFont);
        }

        private void ReportDetails(PdfPTable detailTable)
        {
            WriteBlankRow(detailTable, 5);
            WriteColumn(detailTable, 11, "Description of Items", Element.ALIGN_CENTER, _reportFontLargeBold);
            int i = 0;
            foreach (Item pawnItemData in HoldData.Items)
            {
                i++;
                WriteColumn(detailTable, 11, pawnItemData.TicketDescription, Element.ALIGN_CENTER, _reportFont);
            }
            WriteBlankRow(detailTable, 5);
        }

        private void ReportFooterWithBorders(PdfPTable footerTableWithBorders)
        {
            WriteBorderColumn(footerTableWithBorders, 2, "Hold Expires:", Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(footerTableWithBorders, 2, "Officer's Name:", Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(footerTableWithBorders, 2, "Officer's Phone Number:", Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(footerTableWithBorders, 1, "Badge Number:", Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(footerTableWithBorders, 1, "Agency:", Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(footerTableWithBorders, 2, "Case Number:", Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(footerTableWithBorders, 1, "Request Type::", Element.ALIGN_LEFT, _reportFont);

            WriteBorderColumn(footerTableWithBorders, 2, HoldData.ReleaseDate.ToShortDateString(), Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(footerTableWithBorders, 2, HoldData.PoliceInformation.OfficerFirstName + " " +
                                        HoldData.PoliceInformation.OfficerLastName, Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(footerTableWithBorders, 2, Common.Libraries.Utility.Utilities.GetPhoneNumber(
                                            Common.Libraries.Utility.Utilities.GetStringValue(HoldData.PoliceInformation.PhoneAreaCode, "")
                                                + "-"
                                                + Common.Libraries.Utility.Utilities.GetStringValue(HoldData.PoliceInformation.PhoneNumber, ""
                                            )
                                         ), Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(footerTableWithBorders, 1, HoldData.PoliceInformation.BadgeNumber, Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(footerTableWithBorders, 1, HoldData.PoliceInformation.Agency, Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(footerTableWithBorders, 2, HoldData.PoliceInformation.CaseNumber, Element.ALIGN_LEFT, _reportFont);
            WriteBorderColumn(footerTableWithBorders, 1, HoldData.PoliceInformation.RequestType, Element.ALIGN_LEFT, _reportFont);

        }

        private void ReportFooterNoBorders(PdfPTable footerTable)
        {
            WriteBlankRow(footerTable, 5);
            WriteColumn(footerTable, 11, "Reason for hold:", Element.ALIGN_LEFT, _reportFont);
            WriteBlankRow(footerTable, 2);
            WriteColumn(footerTable, 11, HoldData.HoldComment, Element.ALIGN_LEFT, _reportFont);
        }

        private void WriteBorderColumn(PdfPTable table, int colspan, string textToWrite, int align, Font _font)
        {
            PdfPCell cell = null;
            cell = new PdfPCell(new Phrase(textToWrite, _font));
            cell.HorizontalAlignment = align;
            cell.Colspan = colspan;
            table.AddCell(cell);
        }

        private void WriteColumn(PdfPTable table, int colspan, string textToWrite, int align, Font _font)
        {
            PdfPCell cell = null;
            cell = new PdfPCell(new Phrase(textToWrite, _font));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = align;
            cell.Colspan = colspan;
            table.AddCell(cell);

        }

        private void WriteDescriptionRows(PdfPTable detailsTable, int colspan, string textToWrite, int align, Font _font, int itemCount)
        {
            PdfPCell cell = null;
            cell = new PdfPCell(new Paragraph(itemCount + ". " + textToWrite, _font));
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingLeft = 11;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = colspan;
            detailsTable.AddCell(cell);
        }

        private void WriteBlankRow(PdfPTable headingtable, int numRows)
        {
            PdfPCell cell = null;
            if (numRows > 1)
            {
                for (int i = 1; i <= numRows; i++)
                {
                    cell = new PdfPCell(new Paragraph(string.Empty, _reportFont));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Colspan = 11;
                    headingtable.AddCell(cell);
                }
            }
            else
            {
                cell = new PdfPCell(new Paragraph(string.Empty, _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 11;
                headingtable.AddCell(cell);
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
            text = string.Format("{0}{1} Page {2} of ", reportName[0], StringUtilities.fillString(" ", 180), pageN);

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
