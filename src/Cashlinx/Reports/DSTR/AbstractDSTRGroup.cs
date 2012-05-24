using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Collections.Generic;

namespace Reports.DSTR
{
    public abstract class AbstractDSTRGroup
    {
        public bool DataLoaded { get; set; }

        protected DataTable GroupData { get; set; }
        protected Document Document { get; set; }
        protected DSTRReportContext DSTRContext { get; set; }

        protected PdfPCell _CellSection;
        protected PdfPCell _CellColumnRecordHeader;
        protected PdfPCell _CellColumnRecord;
        protected PdfPCell _CellFooterRecord;
        protected float _CellShadingNormal;
        protected float _CellShadingAlternating;
        protected float _CellShadingHeader;
        protected Font DataFont { get; set; }
        protected Font DataFontBold { get; set; }
        protected Font GroupTitleFont { get; set; }
        protected List<PdfPCell> HeaderCells { get; set; }
        protected PdfPTable PdfTable;

        protected string ErrorCode
        {
            get { return DSTRContext.ErrorCode; }
            set { DSTRContext.ErrorCode = value; }
        }

        protected string ErrorText
        {
            get { return DSTRContext.ErrorText; }
            set { DSTRContext.ErrorText = value; }
        }

        public AbstractDSTRGroup(DSTRReportContext dstrContext, string dataTableName)
        {
            DSTRContext = dstrContext;
            HeaderCells = new List<PdfPCell>();
            Document = DSTRContext.Document;
            PdfTable = DSTRContext.PdfTable;

            LoadDataTable(dataTableName);

            InitializeReportComponents();
        }

        # region Public Methods

        public void BuildSection()
        {
            if (GroupData == null || GroupData.Rows.Count == 0)
            {
                return;
            }

            DSTRContext.HasData = true;
            OnBuildSection();
        }

        # endregion

        # region Abstract Methods

        protected abstract void OnBuildSection();

        # endregion

        # region Virtual Methods

        protected virtual void LoadDataTable(string dataTableName)
        {
            DataTable dataTable;
            DataLoaded = DSTRContext.ReportData.GetTable(dataTableName, out dataTable);

            GroupData = dataTable;
        }

        protected virtual void PrintGroupDetailRecord(PdfPTable pdfTable, int index, DataRow dataRow, bool shadeRow)
        {
        }
        # endregion

        # region Shared Methods

        protected void AddColumn(PdfPTable dataTable, String dataValue, Font rowStyle, int alignment, int colSpan)
        {
            _CellColumnRecord.Phrase = new Phrase(dataValue, rowStyle);
            _CellColumnRecord.HorizontalAlignment = alignment;
            _CellColumnRecord.Colspan = colSpan;
            dataTable.AddCell(_CellColumnRecord);
        }

        protected void AddColumn(PdfPTable dataTable, String dataValue, Font rowStyle)
        {
            AddColumn(dataTable, dataValue, rowStyle, Element.ALIGN_LEFT, 1);
        }

        protected void AddGroupContentsToPdfTable(PdfPTable dataTable)
        {
            PdfPCell cellTable = new PdfPCell();
            cellTable.Border = Rectangle.NO_BORDER;
            cellTable.HorizontalAlignment = Element.ALIGN_LEFT;
            cellTable.VerticalAlignment = Element.ALIGN_BOTTOM;
            cellTable.Table = dataTable;

            PdfTable.AddCell(cellTable);
        }

        protected void AddGroupTitle(PdfPTable dataTable, string groupTitle)
        {
            PdfPCell cell = GetCell(groupTitle + ":", GroupTitleFont, Element.ALIGN_LEFT, dataTable.NumberOfColumns);
            cell.Border = Rectangle.NO_BORDER;
            dataTable.AddCell(cell);
        }

        protected void AddHeaderCells(PdfPTable dataTable)
        {
            for (int i = 0; i < HeaderCells.Count; i++)
            {
                PdfPCell cell = HeaderCells[i];
                cell.GrayFill = _CellShadingHeader;

                int border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                if (i == 0) // left cell
                {
                    cell.Border = border | Rectangle.LEFT_BORDER;
                }
                else if (i == HeaderCells.Count - 1) // right cell
                {
                    cell.Border = border | Rectangle.RIGHT_BORDER;
                }
                else // inner cells
                {
                    cell.Border = border;
                }

                dataTable.AddCell(cell);
            }
        }

        protected PdfPCell GetCell(string value, Font font, int horizontalAlignment)
        {
            return GetCell(value, font, horizontalAlignment, false, 1);
        }

        protected PdfPCell GetCell(string value, Font font, bool noWrap, int horizontalAlignment)
        {
            return GetCell(value, font, horizontalAlignment, false, 1, noWrap);
        }

        protected PdfPCell GetCell(string value, Font font, int horizontalAlignment, int columnSpan)
        {
            return GetCell(value, font, horizontalAlignment, false, columnSpan);
        }

        protected PdfPCell GetCell(string value, Font font, int horizontalAlignment, int columnSpan, bool noWrap)
        {
            return GetCell(value, font, horizontalAlignment, false, columnSpan, noWrap);
        }

        protected PdfPCell GetCell(string value, Font font, int horizontalAlignment, int columnSpan, int border)
        {
            return GetCell(value, font, horizontalAlignment, false, columnSpan, border);
        }

        protected PdfPCell GetCell(string value, Font font, int horizontalAlignment, int columnSpan, int border, bool noWrap)
        {
            return GetCell(value, font, horizontalAlignment, false, columnSpan, border, noWrap);
        }

        protected PdfPCell GetCell(string value, Font font, int horizontalAlignment, bool shadeCell)
        {
            return GetCell(value, font, horizontalAlignment, shadeCell, 1);
        }

        protected PdfPCell GetCell(string value, Font font, int horizontalAlignment, bool shadeCell, bool noWrap)
        {
            return GetCell(value, font, horizontalAlignment, shadeCell, 1, noWrap);
        }

        protected PdfPCell GetCell(string value, Font font, int horizontalAlignment, bool shadeCell, int columnSpan)
        {
            return GetCell(value, font, horizontalAlignment, shadeCell, columnSpan, Rectangle.NO_BORDER);
        }

        protected PdfPCell GetCell(string value, Font font, int horizontalAlignment, bool shadeCell, int columnSpan, bool noWrap)
        {
            return GetCell(value, font, horizontalAlignment, shadeCell, columnSpan, Rectangle.NO_BORDER, noWrap);
        }

        protected PdfPCell GetCell(Paragraph paragraph, int horizontalAlignment, bool shadeCell, int columnSpan)
        {
            PdfPCell cell = new PdfPCell(paragraph);
            cell.HorizontalAlignment = horizontalAlignment;
            cell.GrayFill = shadeCell ? _CellShadingAlternating : _CellShadingNormal;
            cell.Colspan = columnSpan;
            cell.Border = Rectangle.NO_BORDER;
            return cell;
        }

        protected PdfPCell GetCell(string value, Font font, int horizontalAlignment, bool shadeCell, int columnSpan, int border)
        {
            return GetCell(value, font, horizontalAlignment, shadeCell, columnSpan, border, false);
        }

        protected PdfPCell GetCell(string value, Font font, int horizontalAlignment, bool shadeCell, int columnSpan, int border, bool noWrap)
        {
            PdfPCell cell = new PdfPCell(new Phrase(value, font));
            cell.HorizontalAlignment = horizontalAlignment;
            cell.GrayFill = shadeCell ? _CellShadingAlternating : _CellShadingNormal;
            cell.Colspan = columnSpan;
            cell.NoWrap = noWrap;
            cell.Border = border;
            return cell;
        }

        protected static string GetCurrencyValue(object oValueToParse)
        {
            decimal value = GetDecimalValue(oValueToParse);
            if (value < 0)
            {
                return string.Format("({0:C})", Math.Abs(value));
            }

            return value.ToString("c");
        }

        // Provides String value back as string.Empty
        protected static string GetStringValue(object oValueToParse)
        {
            try
            {
                string sValueControl = oValueToParse.ToString();
                return sValueControl;
            }
            catch
            {
                return string.Empty;
            }
        }

        protected string GetDateValue(object oValueToParse)
        {
            DateTime dt;

            if (DateTime.TryParse(oValueToParse.ToString(), out dt))
            {
                return dt.ToString("d");
            }

            return string.Empty;
        }

        protected static decimal GetDecimalValue(object oValueToParse)
        {
            try
            {
                return decimal.Parse(oValueToParse.ToString());
            }
            catch
            {
                return 0M;
            }
        }

        protected Paragraph GetICNParagraph(string icnNumber)
        {
            Paragraph paragraph = new Paragraph();
            paragraph.Add(new Phrase(string.Concat(icnNumber.Substring(0, 5), " ", icnNumber.Substring(5, 1), " "), DataFont));
            paragraph.Add(new Phrase(icnNumber.Substring(6, 6) + " ", DataFontBold));
            paragraph.Add(new Phrase(icnNumber.Substring(12, 1) + " ", DataFont));
            paragraph.Add(new Phrase(icnNumber.Substring(13), DataFontBold));

            return paragraph;
        }

        protected static int GetIntValue(object oValueToParse)
        {
            try
            {
                return int.Parse(oValueToParse.ToString());
            }
            catch
            {
                return 0;
            }
        }

        protected string GetTimeValue(object oValueToParse)
        {
            DateTime dt;

            if (DateTime.TryParse(oValueToParse.ToString(), out dt))
            {
                return dt.ToString("t");
            }

            return string.Empty;
        }

        protected void InitializeReportComponents()
        {
            DataFont = new Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.NORMAL));
            DataFontBold = new Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.BOLD));
            GroupTitleFont = new Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.BOLD));

            _CellSection = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.BOLD)));
            _CellSection.HorizontalAlignment = Element.ALIGN_LEFT;
            _CellSection.Colspan = 6;
            _CellSection.Border = Rectangle.NO_BORDER;

            _CellColumnRecordHeader = new PdfPCell();
            _CellColumnRecordHeader.BackgroundColor = BaseColor.WHITE;
            _CellColumnRecordHeader.Colspan = 1;
            _CellColumnRecordHeader.PaddingTop = 5;
            _CellColumnRecordHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            _CellColumnRecordHeader.VerticalAlignment = Element.ALIGN_BOTTOM;
            _CellColumnRecordHeader.Border = Rectangle.BOTTOM_BORDER;

            _CellColumnRecord = new PdfPCell();
            _CellColumnRecord.BackgroundColor = BaseColor.WHITE;
            _CellColumnRecord.Colspan = 1;
            _CellColumnRecord.PaddingLeft = 5;
            _CellColumnRecord.HorizontalAlignment = Element.ALIGN_LEFT;
            _CellColumnRecord.VerticalAlignment = Element.ALIGN_TOP;
            _CellColumnRecord.Border = Rectangle.NO_BORDER;

            _CellFooterRecord = new PdfPCell();
            _CellFooterRecord.BackgroundColor = BaseColor.WHITE;
            _CellFooterRecord.Colspan = 1;
            _CellFooterRecord.PaddingLeft = 5;
            _CellFooterRecord.HorizontalAlignment = Element.ALIGN_LEFT;
            _CellFooterRecord.VerticalAlignment = Element.ALIGN_TOP;
            _CellFooterRecord.Border = Rectangle.TOP_BORDER;

            _CellShadingNormal = 1.0f;
            _CellShadingAlternating = 0.9f;
            _CellShadingHeader = 0.7f;
        }

        protected void PrintGroupDetail(PdfPTable pdfTable)
        {
            if (GroupData == null || GroupData.Rows.Count == 0)
            {
                return;
            }

            for (int i = 0; i < GroupData.Rows.Count; i++)
            {
                PrintGroupDetailRecord(pdfTable, i, GroupData.Rows[i], !(i % 2 == 0));
            }
        }

        # endregion
    }
}
