using System.Collections.Generic;
using System.IO;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports
{
    public class PoliceSeizeReport : PdfPageEventHelper
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
        private string _caseNo = string.Empty;

        private string CaseNo
        {

            get { return _caseNo; }
            set { _empNo = value; }

        }

        public string EmpNo
        {
            get { return _empNo; }
            set { _empNo = value; }
        }

        private string _transactionDate = string.Empty;

        public string TransactionDate
        {
            get { return _transactionDate; }
            set { _transactionDate = value; }
        }

        public HoldData SeizeData { get; set; }

        private HoldData _holdData;

        public HoldData HoldData
        {
            get { return _holdData; }
            set { _holdData = value; }
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
        private string rptfile;
        private Common.Libraries.Utility.Shared.PdfLauncher pdfLauncher;

        public PoliceSeizeReport(Common.Libraries.Utility.Shared.PdfLauncher pdfLauncher)
        {
            // TODO: Complete member initialization
            this.pdfLauncher = pdfLauncher;
        }

        public PoliceSeizeReport()
        {
            // TODO: Complete member initialization
        }
        
        public bool CreateReport()
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.HALFLETTER);
            try
            {
                //set up RunReport event overrides & create doc
                PoliceSeizeReport events = new PoliceSeizeReport();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                //set up tables, etc...
                PdfPTable table = new PdfPTable(11);
                PdfPTable footerTable = new PdfPTable(11);
                PdfPCell cell = new PdfPCell();
                Image gif = Image.GetInstance(Common.Properties.Resources.logo, BaseColor.WHITE);
                footerTable.HorizontalAlignment = Rectangle.ALIGN_BOTTOM;
                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD);
                _reportFontUnderlined = FontFactory.GetFont("Arail", 8, iTextSharp.text.Font.UNDERLINE);

                gif.ScalePercent(20);
                runReport = new RunReport();
                
                document.SetPageSize(PageSize.HALFLETTER.Rotate());
                document.SetMargins(-40, -40, 5, 23);
                document.AddTitle(string.Empty);
              
                ReportHeader(table,writer);
                ReportDetail(table);
                ReportSummary(footerTable);

                table.HeaderRows = 11;
                document.Open();
                document.Add(table);
                document.Add(footerTable);
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

        
        private void ReportHeader(PdfPTable headingtable, PdfWriter pdfWriter)
        {
            PdfPCell cell = null;
            cell = new PdfPCell(new Paragraph("Police Seized Items", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 11;
            headingtable.AddCell(cell);
            

            int pageN = pdfWriter.CurrentPageNumber;
            string p = "Page:" + pageN.ToString();
            cell = new PdfPCell(new Paragraph(p, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 11;
            headingtable.AddCell(cell);

            WriteBlankRow(headingtable,2);
        
            WriteColumn(headingtable, 4, "Date: " + TransactionDate, Element.ALIGN_LEFT, _reportFont);
            WriteColumn(headingtable, 7, "Employee Number: " + EmpNo, Element.ALIGN_RIGHT, _reportFont);
            WriteColumn(headingtable, 11, "Case Number: " + HoldData.PoliceInformation.CaseNumber, Element.ALIGN_RIGHT, _reportFont);

           //row 4
            WriteColumn(headingtable, 2, "Received From:", Element.ALIGN_LEFT, _reportFont);
            WriteColumn(headingtable, 9, STORE_NAME, Element.ALIGN_LEFT, _reportFont);

            //row 5
            WriteColumn(headingtable, 2, string.Empty, Element.ALIGN_LEFT, _reportFont);
            WriteColumn(headingtable, 9, STORE_ADDRESS, Element.ALIGN_LEFT, _reportFont);

            //row6 
            WriteColumn(headingtable, 2, string.Empty, Element.ALIGN_LEFT, _reportFont);
            WriteColumn(headingtable, 9, STORE_CITY + " " + STORE_STATE + " " + STORE_ZIP, Element.ALIGN_LEFT, _reportFont);

            //row7
            WriteBlankRow(headingtable, 1);
        }
      
      
        private void ReportDetail(PdfPTable datatable)
        {
            
            WriteBlankRow(datatable,2);
            WriteColumn(datatable, 11, "Items Descriptions", Element.ALIGN_LEFT, _reportFont);
            WriteBlankRow(datatable, 3);

            int i = 0;
            foreach (Item pawnItemData in HoldData.Items)
            {
                i++;
                WriteDescriptionRows(datatable, 11, pawnItemData.TicketDescription, Element.ALIGN_LEFT, _reportFont, i);

              
            }
            WriteBlankRow(datatable, 15);

        }

        private void ReportFooter(PdfPTable datatable)
        {
            //seize# row
            WriteColumn(datatable, 2, "Seize #: " + HoldData.PoliceInformation.SeizeNumber.ToString(), Element.ALIGN_LEFT, _reportFont);
            WriteColumn(datatable, 2, "Loan Number: " + HoldData.TicketNumber.ToString(), Element.ALIGN_LEFT, _reportFont);
            WriteColumn(datatable, 1, string.Empty, Element.ALIGN_LEFT, _reportFont);
            WriteColumn(datatable, 2, "Amount: " + string.Format("{0:C}", HoldData.Amount), Element.ALIGN_LEFT, _reportFont);
            if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId))
            {
                WriteColumn(datatable, 1, string.Empty, Element.ALIGN_LEFT, _reportFont);
                WriteColumn(datatable, 3, "Current Principal Amount: " + string.Format("{0:C}", HoldData.CurrentPrincipalAmount), Element.ALIGN_LEFT, _reportFont);
            }
          
            WriteBlankRow(datatable, 3);

            WriteColumn(datatable, 2, "Sold or Pawned By: ", Element.ALIGN_LEFT, _reportFont);
            WriteColumn(datatable, 9, CurrentCust.CustomerName, Element.ALIGN_LEFT, _reportFont);

            WriteColumn(datatable, 2, string.Empty, Element.ALIGN_LEFT, _reportFont);
            WriteColumn(datatable, 9, CustHomeAddr.Address1, Element.ALIGN_LEFT, _reportFont);
            
            WriteColumn(datatable, 2, string.Empty, Element.ALIGN_LEFT, _reportFont);
            WriteColumn(datatable, 9, CustHomeAddr.City + "," + CustHomeAddr.State_Code + "," + CustHomeAddr.ZipCode, Element.ALIGN_LEFT, _reportFont);

            WriteBlankRow(datatable, 4);

            WriteColumn(datatable, 11, "Agency Seizing the merchandise: " + HoldData.PoliceInformation.Agency, Element.ALIGN_LEFT, _reportFont);

            WriteBlankRow(datatable, 4);
        }

        private void ReportSummary(PdfPTable summaryTable)
        {
            ReportFooter(summaryTable);

            WriteColumn(summaryTable, 3, "Employee Signature", Element.ALIGN_LEFT, _reportFont, PdfPCell.ALIGN_BOTTOM);
            WriteColumn(summaryTable, 4, string.Empty, Element.ALIGN_LEFT, _reportFont, PdfPCell.ALIGN_BOTTOM);
            WriteColumn(summaryTable, 4, "Officer Signature", Element.ALIGN_LEFT, _reportFont, PdfPCell.ALIGN_BOTTOM);

            WriteBlankRow(summaryTable, 3);

            WriteColumn(summaryTable, 3, StringUtilities.fillString("-", 50), Element.ALIGN_LEFT, _reportFont, PdfPCell.ALIGN_BOTTOM);
            WriteColumn(summaryTable, 4, string.Empty, Element.ALIGN_LEFT, _reportFont, PdfPCell.ALIGN_BOTTOM);
            WriteColumn(summaryTable, 4, StringUtilities.fillString("-", 50), Element.ALIGN_LEFT, _reportFont, PdfPCell.ALIGN_BOTTOM);
         
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

        private void WriteColumn(PdfPTable table, int colspan, string textToWrite, int align, Font _font, int verticalAlign)
        {
            PdfPCell cell = null;
            cell = new PdfPCell(new Phrase(textToWrite, _font));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = align;
            cell.VerticalAlignment = verticalAlign;
            cell.Colspan = colspan;
            table.AddCell(cell);
           
        }

        private void WriteDescriptionRows(PdfPTable detailsTable, int colspan, string textToWrite, int align, Font _font, int itemCount)
        {
            PdfPCell cell = null;
            cell = new PdfPCell(new Paragraph(itemCount + "] " + textToWrite, _font));
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingLeft = 11;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
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
           
        }

    }
}
