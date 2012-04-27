using System;
using System.Data;
using System.IO;
using System.Linq;
using Common.Controllers.Application;
using Common.Libraries.Objects;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using Common.Libraries.Utility.Logger;
using Utilities = Common.Libraries.Utility.Utilities;

namespace Reports
{
    public class AssignPhysicalLocationsReport : PdfPageEventHelper
    {
        private const int COLUMNS = 7;

        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;

        private Font ReportFont { get; set; }
        private Font ReportFontBold { get; set; }
        private AssignPhysicalLocationsReportContext ReportContext { get; set; }

        private int numberOfRows = 0;
        private List<int> rowNumbers;
        private int _RecordCount;

        private Document document;

        private ReportObject ReportObject
        {
            get { return ReportContext.ReportObject; }
        }

        public AssignPhysicalLocationsReport(AssignPhysicalLocationsReportContext reportContext)
        {
            ReportContext = reportContext;
            _RecordCount = reportContext.Data.Rows.Count;
        }

        public bool CreateReport()
        {
            var isSuccessful = false;
            document = new iTextSharp.text.Document(PageSize.LETTER);

            try
            {
                rowNumbers = new List<int>();
                var reportFileName = Path.GetFullPath(ReportObject.ReportTempFileFullName);
                if (!Directory.Exists(Path.GetDirectoryName(reportFileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(reportFileName));
                }
                var writer = PdfWriter.GetInstance(document, new FileStream(reportFileName, FileMode.Create));
                writer.PageEvent = this;

                ReportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
                ReportFontBold = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD);
                footerBaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                var table = new PdfPTable(COLUMNS);
                table.HeaderRows = 3;
                table.SetWidths(new float[] { 1, 1, 3, 1, 1, 1, 1 });

                var gif = Image.GetInstance(Common.Properties.Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(35);

                document.SetMargins(-30, -30, 10, 45);
                document.Open();
                PrintReportHeader(table, gif);
                PrintContentHeader(table);
                table.TotalWidth = PageSize.LETTER.Width - 72;

                foreach (DataRow dr in ReportContext.Data.Rows)
                {
                    PrintDetailRecord(dr, table);
                }

                if (table.Rows.Count > 0)
                {
                    rowNumbers.Add(numberOfRows);
                    document.Add(table);
                }

                document.Close();

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

        private void PrintContentHeader(PdfPTable table)
        {
            var cell = new PdfPCell(new Paragraph("User", ReportFontBold))
                       {
                           Border = Rectangle.BOTTOM_BORDER,
                           HorizontalAlignment = Element.ALIGN_LEFT,
                           VerticalAlignment = Element.ALIGN_TOP
                       };
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Ticket No.", ReportFontBold))
                   {
                       Border = Rectangle.BOTTOM_BORDER,
                       HorizontalAlignment = Element.ALIGN_LEFT,
                       VerticalAlignment = Element.ALIGN_TOP
                   };
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Ticket Description", ReportFontBold))
                   {
                       Border = Rectangle.BOTTOM_BORDER,
                       HorizontalAlignment = Element.ALIGN_LEFT,
                       VerticalAlignment = Element.ALIGN_TOP
                   };
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Aisle", ReportFontBold))
                   {
                       Border = Rectangle.BOTTOM_BORDER,
                       HorizontalAlignment = Element.ALIGN_CENTER,
                       VerticalAlignment = Element.ALIGN_TOP
                   };
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Shelf", ReportFontBold))
                   {
                       Border = Rectangle.BOTTOM_BORDER,
                       HorizontalAlignment = Element.ALIGN_CENTER,
                       VerticalAlignment = Element.ALIGN_TOP
                   };
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Other", ReportFontBold))
                   {
                       Border = Rectangle.BOTTOM_BORDER,
                       HorizontalAlignment = Element.ALIGN_CENTER,
                       VerticalAlignment = Element.ALIGN_TOP
                   };
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Gun No.", ReportFontBold))
                   {
                       Border = Rectangle.BOTTOM_BORDER,
                       HorizontalAlignment = Element.ALIGN_LEFT,
                       VerticalAlignment = Element.ALIGN_TOP
                   };
            table.AddCell(cell);
        }

        private void PrintDetailRecord(DataRow dr, PdfPTable table)
        {
            numberOfRows++;
            var cell = new PdfPCell(new Paragraph(Utilities.GetStringValue(dr["userID"]), ReportFont))
                       {
                           Border = Rectangle.NO_BORDER,
                           HorizontalAlignment = Element.ALIGN_LEFT,
                           VerticalAlignment = Element.ALIGN_TOP
                       };
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(Utilities.GetStringValue(dr["icn_doc"]), ReportFont))
                   {
                       Border = Rectangle.NO_BORDER,
                       HorizontalAlignment = Element.ALIGN_LEFT,
                       VerticalAlignment = Element.ALIGN_TOP
                   };
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(Utilities.GetStringValue(dr["md_desc"]), ReportFont))
                   {
                       Border = Rectangle.NO_BORDER,
                       HorizontalAlignment = Element.ALIGN_LEFT,
                       VerticalAlignment = Element.ALIGN_TOP
                   };
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(Utilities.GetStringValue(dr["loc_aisle"]), ReportFont))
                   {
                       Border = Rectangle.NO_BORDER,
                       HorizontalAlignment = Element.ALIGN_CENTER,
                       VerticalAlignment = Element.ALIGN_TOP
                   };
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(Utilities.GetStringValue(dr["loc_shelf"]), ReportFont))
                   {
                       Border = Rectangle.NO_BORDER,
                       HorizontalAlignment = Element.ALIGN_CENTER,
                       VerticalAlignment = Element.ALIGN_TOP
                   };
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(Utilities.GetStringValue(dr["location"]), ReportFont))
                   {
                       Border = Rectangle.NO_BORDER,
                       HorizontalAlignment = Element.ALIGN_CENTER,
                       VerticalAlignment = Element.ALIGN_TOP
                   };
            table.AddCell(cell);

            var sGunNumber = Utilities.GetStringValue(dr["gun_number"], string.Empty);
            sGunNumber = sGunNumber == "0" ? string.Empty : sGunNumber;
            cell = new PdfPCell(new Paragraph(sGunNumber, ReportFont))
                   {
                       Border = Rectangle.NO_BORDER,
                       HorizontalAlignment = Element.ALIGN_LEFT,
                       VerticalAlignment = Element.ALIGN_TOP,
                       PaddingLeft = -10
                   };
            table.AddCell(cell);

            var tableHeight = table.Rows.Cast<object>().Select((t, i) => table.GetRowHeight(i)).Sum();

            if (tableHeight <= PageSize.LETTER.Height - 72)
            {
                return;
            }
            rowNumbers.Add(numberOfRows);
            document.Add(table);
            numberOfRows = 0;
            table.DeleteBodyRows();
            document.NewPage();
        }

        private void PrintReportHeader(PdfPTable table, Image gif)
        {
            var tmpTable = new PdfPTable(1);

            //left header

            var cell = new PdfPCell(gif)
                       {
                           Border = Rectangle.NO_BORDER,
                           HorizontalAlignment = Element.ALIGN_LEFT,
                           VerticalAlignment = Element.ALIGN_TOP,
                           PaddingLeft = -10
                       };
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Store:" + ReportObject.ReportStore, ReportFont))
                   {
                       Border = Rectangle.NO_BORDER,
                       HorizontalAlignment = Element.ALIGN_LEFT,
                       VerticalAlignment = Element.ALIGN_TOP
                   };
            tmpTable.AddCell(cell);

            cell = new PdfPCell(tmpTable)
                   {
                       Border = Rectangle.NO_BORDER,
                       HorizontalAlignment = Element.ALIGN_LEFT,
                       VerticalAlignment = Element.ALIGN_TOP,
                       PaddingLeft = -10,
                       Colspan = 3
                   };
            table.AddCell(cell);

            // right header

            tmpTable = new PdfPTable(2);

            cell = new PdfPCell(new Paragraph("Run: ", ReportFont))
                   {
                       Border = Rectangle.NO_BORDER,
                       HorizontalAlignment = Element.ALIGN_RIGHT,
                       VerticalAlignment = Element.ALIGN_TOP
                   };
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(ReportContext.RunDate.ToString("MM/dd/yyyy HH:mm"), ReportFont))
                   {
                       Border = Rectangle.NO_BORDER,
                       HorizontalAlignment = Element.ALIGN_LEFT,
                       VerticalAlignment = Element.ALIGN_TOP
                   };
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("As Of:", ReportFont))
                   {
                       Border = Rectangle.NO_BORDER,
                       HorizontalAlignment = Element.ALIGN_RIGHT,
                       VerticalAlignment = Element.ALIGN_TOP
                   };
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(ReportContext.RunDate.ToString("d"), ReportFont))
                   {
                       Border = Rectangle.NO_BORDER,
                       HorizontalAlignment = Element.ALIGN_LEFT,
                       VerticalAlignment = Element.ALIGN_TOP
                   };
            tmpTable.AddCell(cell);

            cell = new PdfPCell(tmpTable)
                   {
                       Border = Rectangle.NO_BORDER,
                       HorizontalAlignment = Element.ALIGN_RIGHT,
                       VerticalAlignment = Element.ALIGN_TOP,
                       PaddingLeft = -10,
                       Colspan = 4
                   };
            table.AddCell(cell);

            // center header

            cell = new PdfPCell(new Paragraph(ReportObject.ReportTitle))
                   {
                       Border = Rectangle.BOTTOM_BORDER,
                       HorizontalAlignment = Element.ALIGN_CENTER,
                       VerticalAlignment = Element.ALIGN_TOP,
                       PaddingLeft = -10,
                       PaddingBottom = 20,
                       Colspan = COLUMNS
                   };
            table.AddCell(cell);
        }

        public override void OnCloseDocument(PdfWriter writer, Document documentParam)
        {
            template.BeginText();
            template.SetFontAndSize(footerBaseFont, 8);
            template.EndText();
        }

        public override void OnOpenDocument(PdfWriter writer, iTextSharp.text.Document documentParam)
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

        public override void OnEndPage(PdfWriter writer, Document documentParam)
        {
            var pageN = writer.PageNumber;
            var text = string.Format("Page {0}", pageN);
            string rowNumText;
            if (rowNumbers.Count > 1 && rowNumbers.Count >= pageN)
            {
                var startPage = (rowNumbers[pageN - 2] + 1);
                var endPage = rowNumbers[pageN - 1] + rowNumbers[pageN - 2];
                rowNumText = string.Format("Records: {0} through {1} of {2}", startPage, endPage, _RecordCount);
            }
            else
                rowNumText = string.Format("Records: 1 through {0} of {1}", rowNumbers[0], _RecordCount);


            var len = footerBaseFont.GetWidthPoint(text, 8);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            contentByte.SetTextMatrix(30, 30);
            contentByte.ShowText(text);
            contentByte.EndText();
            contentByte.AddTemplate(template, 30 + len, 30);
            len = footerBaseFont.GetWidthPoint(text, 10);
            var pageSize = documentParam.PageSize;
            contentByte.SetRGBColorFill(0, 0, 0);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            contentByte.SetTextMatrix(pageSize.GetRight(195), pageSize.GetTop(44));
            contentByte.ShowText(rowNumText);
            contentByte.EndText();
            contentByte.AddTemplate(template, pageSize.GetRight(150) + len, pageSize.GetTop(70));
        }
    }

    public class AssignPhysicalLocationsReportContext
    {
        public DataTable Data { get; set; }
        public ReportObject ReportObject { get; set; }
        public DateTime RunDate { get; set; }
    }
}
