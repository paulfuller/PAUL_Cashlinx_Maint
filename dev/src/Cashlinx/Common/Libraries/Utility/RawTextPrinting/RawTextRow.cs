using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Libraries.Utility.RawTextPrinting
{
    public class RawTextRow
    {
        public RawTextRow(RawTextDocument document)
        {
            Cells = new List<RawTextCell>();
            Document = document;
            MaximumRowWidth = document.Width;
        }

        public List<RawTextCell> Cells { get; private set; }
        public RawTextDocument Document { get; private set; }
        private int MaximumRowWidth { get; set; }

        public RawTextCell WriteRepeatingText(string c, int width)
        {
            string value = string.Empty;
            while (value.Length < width)
            {
                value += c;
            }

            if (value.Length > width)
            {
                value = value.Substring(0, width);
            }

            return WriteText(value);
        }

        public RawTextCell WriteText(string value)
        {
            return WriteText(value, value.Length);
        }

        public RawTextCell WriteText(string value, int width)
        {
            return WriteText(value, width, RawTextFlags.Left);
        }

        public RawTextCell WriteText(int width, RawTextFlags flags)
        {
            return WriteText(string.Empty, width, flags);
        }

        public RawTextCell WriteText(string value, int width, RawTextFlags flags)
        {
            var currentCumulativeWidth = CalculateCurrentCumulativeWidth();
            var cell = new RawTextCell(width, flags);
            cell.Value = value;

            if (currentCumulativeWidth + cell.Width > MaximumRowWidth)
            {
                throw new InvalidOperationException("Current cell width will exceed maximum row width.");
            }

            Cells.Add(cell);
            return cell;
        }

        private int CalculateCurrentCumulativeWidth()
        {
            return Cells.Sum(c => c.Width);
        }

        public string GetValue()
        {
            int currentWidth = 0;
            var rowValue = new StringBuilder();
            for (var cellIndex = 0; cellIndex < Cells.Count; cellIndex++)
            {
                var rawTextCell = Cells[cellIndex];
                var rawTextValue = rawTextCell.GetValue();
                var overageLength = currentWidth + rawTextValue.Length - MaximumRowWidth;

                if (overageLength > 0)
                {
                    rawTextValue.Value = rawTextValue.Value.Substring(0, rawTextValue.Length - overageLength);
                }
                if (cellIndex == Cells.Count - 1 && overageLength < 0)
                {
                    rawTextValue.Value = rawTextValue.Value.PadRight(rawTextValue.Length + Math.Abs(overageLength));
                }

                rowValue.Append(rawTextValue.GetFullValue());
                currentWidth += rawTextValue.Length;
            }

            return rowValue.ToString();
        }
    }
}
