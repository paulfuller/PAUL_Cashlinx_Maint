using System;
using System.Collections.Generic;
using System.IO;
using Common.Libraries.Objects;
using Common.Libraries.Utility.Shared;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using ReportObject = Common.Controllers.Application.ReportObject;
using Common.Libraries.Utility.Logger;
using Document = iTextSharp.text.Document;
using Utilities = Common.Libraries.Utility.Utilities;

namespace Reports
{
    public class PfiPostReport : ReportBase
    {
        //used by overriden methods for footers
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;

        //used by report
        private new Font _reportFont;
        private Font _reportFontLargeBold;
        private Font _reportFontLargeUnderline;

        private string _AsOf;
        private DataRowCollection _Records;
        private DateTime _ShopTime;
        private string _StoreNumber;
        private string _Tags;
        private string _TotalCost;
        private bool isRefurb = false;
        private int numberOfRows = 0;
        private List<int> rowNumbers;
        private int _RecordCount;
        
        private Document document;

        //main objects
        public ReportObject _ReportObject;

        public PfiPostReport(DataTable dataTable, string sTotalCost, string sTags, string sAsOf, DateTime shopTime, ReportObject reportObject, IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {
            if (dataTable != null)
            {
                _Records = dataTable.Rows;
                _RecordCount = _Records.Count;
                _ShopTime = shopTime;
                _StoreNumber = reportObject.ReportStore;
                _Tags = sTags;
                _TotalCost = sTotalCost;
                _AsOf = sAsOf;
                _ReportObject = reportObject;
            }
        }

        public bool CreateReport()
        {
            bool isSuccessful = false;

            document = new Document(PageSize.LETTER);

            try
            {
                rowNumbers = new List<int>();
                //set up RunReport event overrides & create doc
                string reportFileName = Path.GetFullPath(_ReportObject.ReportTempFileFullName);
                if (!Directory.Exists(Path.GetDirectoryName(reportFileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(reportFileName));
                }
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportFileName, FileMode.Create));
                writer.PageEvent = this;

                //set up tables, etc...
                int columns = 8;
                if (!_Records[0].Table.Columns.Contains("colRefurb"))
                {
                    columns = 7;
                }
                else
                    isRefurb = true;
                PdfPTable table = new PdfPTable(columns);
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);


                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);
                _reportFontLargeUnderline = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.UNDERLINE);

                gif.ScalePercent(35);

                document.AddTitle(_ReportObject.ReportTitle);

                document.SetPageSize(PageSize.LETTER);
                document.SetMargins(-50, -55, 10, 45);
                document.Open();
                PrintReportHeader(table, gif);
                table.HeaderRows = 4;
                document.Add(table);
                table.TotalWidth=PageSize.LETTER.Width - 72;

                PrintReportDetail(table);
                if (table.Rows.Count > 0)
                {
                    rowNumbers.Add(numberOfRows);
                    PrintTotalSummaryRow(table);
                    document.Add(table);
                }
                document.Close();
                isSuccessful = true;
            }
            catch (DocumentException de)
            {
                _ReportObject.ReportError = de.Message;
                _ReportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            catch (IOException ioe)
            {
                _ReportObject.ReportError = ioe.Message;
                _ReportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }

            return isSuccessful;
        }

        private void PrintDetailRow(DataRow dataRow, PdfPTable table)
        {
            PdfPCell cell;
            numberOfRows++;
            if (dataRow.Table.Columns.Contains("colRefurb"))
            {
                cell = new PdfPCell(new Paragraph(Utilities.GetStringValue(dataRow["colRefurb"], ""), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);
            }

            string pAssignmentType;
            try
            {
                pAssignmentType = dataRow["colAssignmentType"] != null ? Utilities.GetStringValue(dataRow["colAssignmentType"], "") : "";
            }
            catch (Exception)
            {
                pAssignmentType = "";
            }
            cell = new PdfPCell(new Paragraph(Commons.GetPfiAssignmentAbbreviation(pAssignmentType), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(Utilities.GetStringValue(dataRow["colNumber"], ""), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(Utilities.GetStringValue(dataRow["colDescription"], ""), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 2;//10.7 NAM: Added this line of code was added to cater for BZ 1313
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            //10.7 NAM: chunk of code below was commented out to cater for BZ 1313
            /*
            cell = new PdfPCell(new Paragraph(PawnUtilities.Utilities.GetStringValue(dataRow["colTags"], ""), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);*/

            cell = new PdfPCell(new Paragraph(Utilities.GetStringValue(dataRow["colCost"], ""), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(Utilities.GetStringValue(dataRow["colRetail"], ""), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(Utilities.GetStringValue(dataRow["colReason"], ""), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);
            float tableHeight=0.0f;
            for(int i=0;i<table.Rows.Count;i++)
                tableHeight += table.GetRowHeight(i);

            if (tableHeight > PageSize.LETTER.Height-72)
            {
                rowNumbers.Add(numberOfRows);
                document.Add(table);
                numberOfRows = 0;
                table.DeleteBodyRows();
                document.NewPage();
            }
            


        }

        private void PrintReportDetail(PdfPTable table)
        {
            foreach (DataRow dataRow in _Records)
            {
                PrintDetailRow(dataRow, table);
            }
        }

        private void PrintReportHeader(PdfPTable table, Image gif)
        {
            PdfPCell cell = new PdfPCell();

            //  heading - row 1
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = -10;
            cell.Colspan = isRefurb ? 8 : 7;
            table.AddCell(cell);

            // heading - row 2
            cell = new PdfPCell(new Paragraph("Store: " + _StoreNumber, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = isRefurb ? 4 : 3;
            table.AddCell(cell);

            PdfPTable summaryTable = new PdfPTable(2);
            summaryTable.SetWidths(new float[] { 2f, 1f });
            cell = new PdfPCell(new Paragraph("Run:", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(_ShopTime.ToString("MM/dd/yyyy HH:mm"), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("As Of:", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(_AsOf, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            summaryTable.AddCell(cell);



            cell = new PdfPCell(summaryTable);
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 4;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            // heading - row 5
            cell = new PdfPCell(new Paragraph(_ReportObject.ReportTitle, _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Padding = 20f;
            cell.Colspan = isRefurb ? 8 : 7;
            table.AddCell(cell);

            // column headers
            table.SetWidths(isRefurb ? new float[]
                                       {
                                               1f, .3f, 1f, 3f, 1f, 1.5f, 1.5f, 2f
                                       } : new float[]
                                           {
                                                   .3f, 1f, 3f, 1f, 1.5f, 1.5f, 2f
                                           });

            // column headers
            if (_Records[0].Table.Columns.Contains("colRefurb"))
            {
                cell = new PdfPCell(new Paragraph("Refurb#", _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);
            }

            cell = new PdfPCell(new Paragraph(" ", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Loan#", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Ticket Description", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;//10.7 NAM: Added this line of code was added to cater for BZ 1313
            table.AddCell(cell);

            //10.7 NAM: this chunk of code below was commented out to cater for BZ 1313
            /*
            cell = new PdfPCell(new Paragraph("#Tag", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);*/

            cell = new PdfPCell(new Paragraph("Cost", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Retail", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Reason", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);
        }

        private void PrintTotalSummaryRow(PdfPTable table)
        {
            PdfPCell cell = new PdfPCell(new Paragraph("Total:", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 4;
            cell.PaddingTop = 20f;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(_Tags, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.NoWrap = true;
            cell.PaddingTop = 20f;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(_TotalCost, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.NoWrap = true;
            cell.PaddingTop = 20f;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;
            cell.PaddingTop = 20f;
            table.AddCell(cell);
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            template.BeginText();
            template.SetFontAndSize(footerBaseFont, 8);
            template.EndText();
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

        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            int pageN = writer.PageNumber;
            string text = "Page " + pageN.ToString();
            string rowNumText;
            if (rowNumbers.Count > 1 && rowNumbers.Count >= pageN)
            {
                int startPage = (rowNumbers[pageN - 2] + 1);
                int endPage = rowNumbers[pageN - 1] + rowNumbers[pageN - 2];
                rowNumText = "Records: " + startPage + " through " + endPage  + " of " + _RecordCount;
            }
            else
                rowNumText = "Records: 1 through " + rowNumbers[0].ToString() + " of " + _RecordCount;


            float len = footerBaseFont.GetWidthPoint(text, 8);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            contentByte.SetTextMatrix(30, 30);
            contentByte.ShowText(text);
            contentByte.EndText();
            contentByte.AddTemplate(template, 30 + len, 30);
            len = footerBaseFont.GetWidthPoint(text, 10);
            Rectangle pageSize = document.PageSize;
            contentByte.SetRGBColorFill(0, 0, 0);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            contentByte.SetTextMatrix(pageSize.GetRight(150), pageSize.GetTop(70));
            contentByte.ShowText(rowNumText);
            contentByte.EndText();
            contentByte.AddTemplate(template, pageSize.GetRight(150) + len, pageSize.GetTop(70));

        }
    }
}
