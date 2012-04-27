using System;
using System.Drawing;
using System.Windows.Forms;
using Common.Libraries.Objects.Business;

namespace Common.Libraries.Forms.Components
{
    public partial class CustomDataGridViewIcnCell : DataGridViewCell
    {
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            var parent = this.OwningColumn as CustomDataGridViewIcnColumn;

            if (parent == null)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
                return;
            }

            var drawingContext = new CellDrawingContext
                                 {
                                     BackColor = cellStyle.BackColor,
                                     ForeColor = cellStyle.ForeColor,
                                     Graphics = graphics,
                                     CellBounds = cellBounds,
                                     PreviousTextSize = new SizeF(0, 0),
                                     TextSize = new SizeF(0, 0),
                                     TextStart = new PointF(cellBounds.X, ((cellBounds.Height - cellBounds.Height)/2.0f))
                                 };


            if ((cellState & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
            {
                drawingContext.BackColor = cellStyle.SelectionBackColor;
                drawingContext.ForeColor = cellStyle.SelectionForeColor;
            }
            using (Brush backColorBrush = new SolidBrush(drawingContext.BackColor))
            {
                graphics.FillRectangle(backColorBrush, cellBounds);
            }

            base.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);

            Font font = cellStyle.Font;
            var boldFont = new Font(font, FontStyle.Bold);

            string cellText = formattedValue == null ? string.Empty : formattedValue.ToString();

            var icn = new Icn(cellText);

            if (!icn.Initialized)
            {
                return;
            }
            
            DrawText(drawingContext, font, icn.FormattedShopNumber + " " + icn.FormattedLastDigitOfYear + " ");
            DrawText(drawingContext, boldFont, icn.FormattedDocumentNumber);
            DrawText(drawingContext, font, " " + icn.FormattedDocumentType + " ");
            DrawText(drawingContext, boldFont, icn.FormattedItemNumber);
            DrawText(drawingContext, font, " " + icn.FormattedSubItemNumber);
            
            drawingContext.OverallWidth += 8; // add a little padding

            if (drawingContext.OverallWidth > parent.Width)
            {
                parent.Width = drawingContext.OverallWidth;
            }
        }

        private void DrawText(CellDrawingContext drawingContext, Font font, string cellText)
        {
            drawingContext.PreviousTextSize = drawingContext.TextSize;
            drawingContext.TextSize = drawingContext.Graphics.MeasureString(cellText, font);
            drawingContext.OverallWidth += (int)drawingContext.TextSize.Width;
            drawingContext.TextStart = new PointF(drawingContext.TextStart.X + drawingContext.PreviousTextSize.Width,
                drawingContext.TextStart.Y);

            using (var brush = new SolidBrush(drawingContext.ForeColor))
            {
                drawingContext.Graphics.DrawString(cellText, font, brush,
                  drawingContext.TextStart.X,
                  drawingContext.CellBounds.Y + drawingContext.TextStart.Y);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return string.Empty;
            }
        }

        private class CellDrawingContext
        {
            public Color BackColor { get; set; }
            public Rectangle CellBounds { get; set; }
            public string CellText { get; set; }
            public Color ForeColor { get; set; }
            public Graphics Graphics { get; set; }
            public int OverallWidth { get; set; }
            public SizeF PreviousTextSize { get; set; }
            public SizeF TextSize { get; set; }
            public PointF TextStart { get; set; }
        }
    }
}
