/*************************************************************************
 * Namespace:       PawnUtilities.Shared
 * Class:           DataGridPrinter
 * 
 * Description      The class keeps assist in printing out a DataGrid using
 *                  the DataGrid properties for formatting
 * 
 * History
 * David D Wise, 5-14-2009, Initial Development
 * 
 * ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Common.Libraries.Utility.Shared
{
	/// <summary>
	/// Summary description for DataGridPrinter.
	/// </summary>
	public class DataGridPrinter
	{
        private int                     _BottomMargin;
        private List<float>             _ColumnWidthList;           // List of Exact Widths of Columns of DataGrid
        private List<float>             _Lines;
        private float                   _PageWidth;                 // Calculated Total Page With from DataGrid Columns
        private float                   _PageHeight;
        private PrintDocument           _PrintDocument;
        private DataGridView            _PrintDataGrid;
        private Point                   _PrintDataGrid_XY;
        private int                     _RowCount;                  // current count of rows;
        private int                     _RowCountCutoff;            // Set after first page is cut off. Used for remaining pages
        private int                     _TopMargin;
        private const int               _VerticalCellLeeway = 10;

		public int                      PageNumber      {get; set;}
        public int                      TotalPages                 // Total Number of Pages (_RowCount of DataGrid)
        {
            get;
            private set;
        }                                                           

        public struct ReportHeader
        {
            public Image Logo;
            public List<ReportHeaderRow> ReportHeaderRow;
        }

        public struct ReportHeaderRow
        {
            public ReportHeaderColumn LeftColumn;
            public ReportHeaderColumn MiddleColumn;
            public ReportHeaderColumn RightColumn;
        }

        public struct ReportHeaderColumn
        {
            public string Text;
            public Font   Font;
            public ReportAlignment ReportAlignment;
        }

        public enum ReportAlignment
        {
            Left,
            Center,
            Right
        }

		public DataGridPrinter(DataGridView dataGridView, PrintDocument printDocument)
		{
            PageNumber          = 1;
            _RowCount           = 0;
            _RowCountCutoff     = 0;
            _Lines              = new List<float>();

            _PrintDataGrid      = dataGridView;
            _PrintDocument      = printDocument;

			float fPageWidth        = _PrintDocument.DefaultPageSettings.PrintableArea.Width;
            _PageHeight             = _PrintDocument.DefaultPageSettings.PrintableArea.Height;
			_TopMargin              = _PrintDocument.DefaultPageSettings.Margins.Top;
            _BottomMargin           = _PrintDocument.DefaultPageSettings.Margins.Bottom;

            GetExactPageWidth(fPageWidth);
		}

        public void SetForPrintJob()
        {
            PageNumber = 1;
            _RowCount = 0;
            _RowCountCutoff = 0;
            _Lines.Clear();
        }

        private void GetExactPageWidth(float fPageWidth)
        {
            _ColumnWidthList = new List<float>();
            _PageWidth = 0;
            float fWidthAdj = 0;

            for (int j = 0; j < _PrintDataGrid.Columns.Count; j++)
            {
                if (!_PrintDataGrid.Columns[j].Visible)
                    fWidthAdj -= (fPageWidth - _PrintDataGrid.Location.X) * _PrintDataGrid.Columns[j].Width / _PrintDataGrid.Width;
            }

            for (int j = 0; j < _PrintDataGrid.Columns.Count; j++)
            {
                float fColumnWidth = 0;
                if (_PrintDataGrid.Columns[j].Visible)
                {
                    fColumnWidth = (fPageWidth - _PrintDataGrid.Location.X) * _PrintDataGrid.Columns[j].Width / ((_PrintDataGrid.Width + fWidthAdj) + 2);
                    _PageWidth += fColumnWidth;
                }
                _ColumnWidthList.Add(fColumnWidth);
            }

            int iPageNumber = 1;
            TotalPages = 1;

            for (int i = 0; i < _PrintDataGrid.Rows.Count; i++)
            {
                if ((i + 1) * (_PrintDataGrid.Font.SizeInPoints + _VerticalCellLeeway) > (_PageHeight * iPageNumber) - (_BottomMargin + _TopMargin))
                {
                    TotalPages++;
                    iPageNumber++;
                }
            }
        }

        private void SetReportHeaderRows(ReportHeader reportHeader, Graphics g)
        {
            if (reportHeader.Logo != null)
            {
                g.DrawImage(reportHeader.Logo, new Point(0, 0));
            }

            float _UpperHeaderRowHeight = _TopMargin;

            if (reportHeader.ReportHeaderRow.Count > 0)
            {
                Point pY = _PrintDataGrid.Location;
                pY.Y     = (reportHeader.ReportHeaderRow.Count - 1) * (Convert.ToInt32(_PrintDataGrid.Font.SizeInPoints) + _VerticalCellLeeway) + _VerticalCellLeeway / 3;
                _PrintDataGrid_XY = pY;

                foreach (ReportHeaderRow HeaderRow in reportHeader.ReportHeaderRow)
                {
                    g.DrawString(HeaderRow.LeftColumn.Text,
                        HeaderRow.LeftColumn.Font,
                        new SolidBrush(Color.Black),
                        new PointF(EstablishReportX(g, HeaderRow.LeftColumn.Text, 0, HeaderRow.LeftColumn.Font, HeaderRow.LeftColumn.ReportAlignment), _UpperHeaderRowHeight));

                    g.DrawString(HeaderRow.MiddleColumn.Text,
                        HeaderRow.MiddleColumn.Font,
                        new SolidBrush(Color.Black),
                        new PointF(EstablishReportX(g, HeaderRow.MiddleColumn.Text, 1, HeaderRow.MiddleColumn.Font, HeaderRow.MiddleColumn.ReportAlignment), _UpperHeaderRowHeight));

                    g.DrawString(HeaderRow.RightColumn.Text,
                        HeaderRow.RightColumn.Font,
                        new SolidBrush(Color.Black),
                        new PointF(EstablishReportX(g, HeaderRow.RightColumn.Text, 2, HeaderRow.RightColumn.Font, HeaderRow.RightColumn.ReportAlignment), _UpperHeaderRowHeight));

                    _UpperHeaderRowHeight += (_VerticalCellLeeway) / 2 + Math.Max(HeaderRow.RightColumn.Font.Size, Math.Max(HeaderRow.LeftColumn.Font.Size, HeaderRow.MiddleColumn.Font.Size));
                }
            }
            else
            {
                Point pY = _PrintDataGrid.Location;
                pY.Y = (Convert.ToInt32(_PrintDataGrid.Font.SizeInPoints) + _VerticalCellLeeway) + _VerticalCellLeeway / 3;
                _PrintDataGrid_XY = pY;
            }
        }

        private float EstablishReportX(Graphics g, string sText, int iColumnIndex, Font font, ReportAlignment reportAlignment)
        {
            float resultPoint   = 0;
            float ColumnLeft      = _PrintDocument.DefaultPageSettings.PrintableArea.Left;
            float ColumnMiddle = _PrintDocument.DefaultPageSettings.PrintableArea.Width / 3;
            float ColumnRight = (_PrintDocument.DefaultPageSettings.PrintableArea.Right * 2) / 3;
            float fTextWidth      = g.MeasureString(sText, font).Width;

            switch (iColumnIndex)
            {
                case 0:
                    resultPoint = EstablishAlignmentLocation(ColumnLeft, ColumnMiddle, fTextWidth, reportAlignment);
                    break;
                case 1:
                    resultPoint = EstablishAlignmentLocation(ColumnMiddle, ColumnRight, fTextWidth, reportAlignment);
                    break;
                case 2:
                    resultPoint = EstablishAlignmentLocation(ColumnRight, _PrintDocument.DefaultPageSettings.PrintableArea.Right, fTextWidth, reportAlignment);
                    break;
                default:
                    resultPoint = ColumnLeft;
                    break;
            }

            return resultPoint;
        }

        private float EstablishAlignmentLocation(float fStartPoint, float fEndPoint, float fTextWidth, ReportAlignment reportAlignment)
        {
            float fDerivedStart = fStartPoint;

            if (fEndPoint >= _PrintDocument.DefaultPageSettings.PrintableArea.Right)
                fEndPoint = _PageWidth;

            switch (reportAlignment)
            {
                case ReportAlignment.Left:
                    break;
                case ReportAlignment.Center:
                    fDerivedStart = (fStartPoint + fEndPoint) / 2 - (fTextWidth / 2);
                    break;
                case ReportAlignment.Right:
                    fDerivedStart = fEndPoint - fTextWidth;
                    break;
            }
            return fDerivedStart;
        }

		private void DrawHeader(Graphics g)
		{
			SolidBrush ForeBrush    = new SolidBrush(_PrintDataGrid.ColumnHeadersDefaultCellStyle.ForeColor);
			SolidBrush BackBrush    = new SolidBrush(_PrintDataGrid.ColumnHeadersDefaultCellStyle.BackColor);
			Pen TheLinePen          = new Pen(_PrintDataGrid.GridColor, 1);
			StringFormat CellFormat = new StringFormat();
			CellFormat.Trimming     = StringTrimming.EllipsisCharacter;
			CellFormat.FormatFlags  = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit;

			int initialRowCount     = _RowCount;

			// Draw the table header
			float Start_Xposition       = _PrintDataGrid.Location.X;
			RectangleF NextCellBounds   = new RectangleF(0, 0, 0, 0);
			RectangleF HeaderBounds     = new RectangleF(0, 0, 0, 0);

			HeaderBounds.X      = _PrintDataGrid.Location.X;
            HeaderBounds.Y      = _PrintDataGrid_XY.Y + _TopMargin + (_RowCount - initialRowCount) * (_PrintDataGrid.Font.SizeInPoints + _VerticalCellLeeway);
			HeaderBounds.Height = _PrintDataGrid.Font.SizeInPoints + _VerticalCellLeeway;
            HeaderBounds.Width  = _PageWidth;

			g.FillRectangle(BackBrush, HeaderBounds);

            for (int k = 0; k < _PrintDataGrid.Columns.Count; k++)
			{
                if (_PrintDataGrid.Columns[k].Visible)
                {
                    string nextcolumn = _PrintDataGrid.Columns[k].HeaderText;

                    RectangleF CellBounds = new RectangleF(Start_Xposition,
                            _PrintDataGrid_XY.Y + _TopMargin + (_RowCount - initialRowCount) * (_PrintDataGrid.Font.SizeInPoints + _VerticalCellLeeway),
                            _ColumnWidthList[k],
                            _PrintDataGrid.ColumnHeadersDefaultCellStyle.Font.SizeInPoints + _VerticalCellLeeway);
                    NextCellBounds = CellBounds;

                    g.DrawString(nextcolumn, _PrintDataGrid.ColumnHeadersDefaultCellStyle.Font, ForeBrush, CellBounds, CellFormat);

                    if (_PrintDataGrid.GridColor != Color.Transparent)
                        g.DrawLine(TheLinePen, Start_Xposition, NextCellBounds.Bottom, _ColumnWidthList[k], NextCellBounds.Bottom);

                    Start_Xposition = Start_Xposition + _ColumnWidthList[k];
                }
			}
		}

		private bool DrawRows(Graphics g)
		{
			float lastRowBottom = _TopMargin;

			try
			{
				SolidBrush ForeBrush            = new SolidBrush(_PrintDataGrid.ForeColor);
				SolidBrush BackBrush            = new SolidBrush(_PrintDataGrid.BackColor);
				SolidBrush AlternatingBackBrush = new SolidBrush(_PrintDataGrid.AlternatingRowsDefaultCellStyle.BackColor);
				Pen TheLinePen                  = new Pen(_PrintDataGrid.GridColor, 1);
				StringFormat CellFormat         = new StringFormat();
				CellFormat.Trimming             = StringTrimming.EllipsisCharacter;
				CellFormat.FormatFlags          = StringFormatFlags.NoWrap | StringFormatFlags.LineLimit;
				int initialRowCount             = _RowCount;
				RectangleF RowBounds            = new RectangleF(0, 0, 0, 0);
                int DataGridWidth               = _PrintDataGrid.Width;

				// draw the rows of the table
                for (int i = initialRowCount; i < _PrintDataGrid.Rows.Count; i++)
				{
					float Start_Xposition = _PrintDataGrid.Location.X;
					RowBounds.X           = _PrintDataGrid.Location.X;
                    RowBounds.Y           = _PrintDataGrid_XY.Y + _TopMargin + ((_RowCount - initialRowCount) + 1) * (_PrintDataGrid.Font.SizeInPoints + _VerticalCellLeeway);
					RowBounds.Height      = _PrintDataGrid.Font.SizeInPoints + _VerticalCellLeeway;
                    RowBounds.Width       = _PageWidth;

					_Lines.Add(RowBounds.Bottom);

					if (i%2 == 0)
					{
						g.FillRectangle(BackBrush, RowBounds);
					}
					else
					{
						g.FillRectangle(AlternatingBackBrush, RowBounds);
					}

                    for (int j = 0; j < _PrintDataGrid.Columns.Count; j++)
					{
                        if (_PrintDataGrid.Columns[j].Visible)
                        {
                            string sCellValue = Utilities.GetStringValue(_PrintDataGrid.Rows[i].Cells[j].Value);
                            if (sCellValue.Length > _ColumnWidthList[j])
                                sCellValue = sCellValue.Substring(0, Convert.ToInt32(_ColumnWidthList[j] / _PrintDataGrid.Font.SizeInPoints));

                            RectangleF CellBounds = new RectangleF(Start_Xposition,
                                _PrintDataGrid_XY.Y + _TopMargin + ((_RowCount - initialRowCount) + 1) * (_PrintDataGrid.Font.SizeInPoints + _VerticalCellLeeway),
                                _ColumnWidthList[j],
                                _PrintDataGrid.Font.SizeInPoints + _VerticalCellLeeway);

                            g.DrawString(sCellValue, _PrintDataGrid.Font, ForeBrush, CellBounds, CellFormat);
                            lastRowBottom = CellBounds.Bottom;
                            Start_Xposition = Start_Xposition + _ColumnWidthList[j];
                        }
					}

					_RowCount++;

                    float fPageSize = (_PageHeight * PageNumber) - (_BottomMargin + _TopMargin);
                    float fCurrentPageSize = _RowCount * (_PrintDataGrid.Font.SizeInPoints + _VerticalCellLeeway);

                    if (fCurrentPageSize > fPageSize || (_RowCountCutoff == _RowCount / PageNumber && _RowCountCutoff > 0))
					{
                        DrawHorizontalLines(g, _Lines);
                        DrawVerticalGridLines(g, TheLinePen, _ColumnWidthList, lastRowBottom);
                        _RowCountCutoff = _RowCountCutoff > 0 ? _RowCountCutoff : _RowCount;
						return true;
					}
				}
                DrawHorizontalLines(g, _Lines);
                DrawVerticalGridLines(g, TheLinePen, _ColumnWidthList, lastRowBottom);
				return false;
			}
            catch
			{
				return false;
			}
		}

        private void DrawHorizontalLines(Graphics g, List<float> lines)
		{
			Pen TheLinePen = new Pen(_PrintDataGrid.GridColor, 1);

            if (_PrintDataGrid.GridColor == Color.Transparent)
				return;

            int iStartIdx   = (PageNumber - 1) * _RowCountCutoff;
            int iEndIdx     = PageNumber > 1 ? iStartIdx + _RowCountCutoff : lines.Count;
            iEndIdx         = iEndIdx > _RowCount ? _RowCount : iEndIdx;

            for (int i = iStartIdx; i < iEndIdx; i++)
            {
                g.DrawLine(TheLinePen, _PrintDataGrid.Location.X, lines[i], _PrintDataGrid.Location.X + _PageWidth, lines[i]);
            }
		}

		private void DrawVerticalGridLines(Graphics g, Pen TheLinePen, List<float> columnwidth, float bottom)
		{
            if (_PrintDataGrid.GridColor == Color.Transparent)
				return;

            float fTotalColumnsWidth = 0;

            int iGridCount = 0;
            for (int k = 0; k < _PrintDataGrid.Columns.Count; k++)
			{
                if (_PrintDataGrid.Columns[k].Visible)
                {
                    fTotalColumnsWidth += columnwidth[k];

                    if (iGridCount == 0)
                    {
                        g.DrawLine(TheLinePen, _PrintDataGrid.Location.X,
                            _PrintDataGrid_XY.Y + _TopMargin,
                            _PrintDataGrid.Location.X,
                            bottom);
                    }

                    g.DrawLine(TheLinePen, _PrintDataGrid.Location.X + fTotalColumnsWidth,
                        _PrintDataGrid_XY.Y + _TopMargin,
                        _PrintDataGrid.Location.X + fTotalColumnsWidth,
                        bottom);
                    iGridCount++;
                }
			}
		}

        public bool DrawDataGrid(Graphics g, ReportHeader reportHeader)
        {
            try
            {
                SetReportHeaderRows(reportHeader, g);
                DrawHeader(g);
                bool bContinue = DrawRows(g);
                return bContinue;
            }
            catch
            {
                return false;
            }
        }
	}
}
