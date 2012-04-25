using System;
using System.Collections.Generic;

namespace Common.Libraries.Utility.RawTextPrinting
{
    public class MerchandiseDescriptionLineSeparator
    {
        public MerchandiseDescriptionLineSeparator(int maxRows, int maxWidth)
        {
            MaxRows = maxRows;
            MaxWidth = maxWidth;
        }

        public int MaxRows { get; set; }
        public int MaxWidth { get; set; }

        public string[] SplitIntoRows(string value)
        {
            var rows = new List<string>();
            var currentPosition = 0;
            while (currentPosition < value.Length - 2)
            {
                var length = Math.Min(value.Length - currentPosition, MaxWidth);
                rows.Add(value.Substring(currentPosition, length));
                currentPosition += length;
            }

            return rows.ToArray();
        }
    }
}
