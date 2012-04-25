using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Utility.RawTextPrinting
{
    public class RawTextDocument : RawTextElement
    {
        public const int DefaultWidth = 80;
        

        public RawTextDocument()
            : this(DefaultWidth)
        {
            
        }

        public RawTextDocument(int width)
        {
            Rows = new List<RawTextRow>();
            NewLineValue = Environment.NewLine;
            AppendNewLineToRows = true;
            TrimEndOnRow = true;
            Width = width;
        }

        public bool AppendNewLineToRows { get; set; }
        public string NewLineValue { get; set; }

        public int RowCount
        {
            get { return Rows.Count; }
        }

        public List<RawTextRow> Rows { get; private set; }
        public bool TrimEndOnRow { get; set; }
        public int Width { get; private set; }

        public RawTextRow CreateNewRow()
        {
            var row = new RawTextRow(this);
            Rows.Add(row);
            return row;
        }

        public string GetDocumentValue()
        {
            var docValue = new StringBuilder();

            docValue.Append(GetRawPrinterCodes());

            foreach (var row in Rows )
            {
                var rowValue = row.GetValue();

                if (TrimEndOnRow)
                {
                    rowValue = rowValue.TrimEnd();
                }

                docValue.Append(rowValue);

                if (AppendNewLineToRows)
                {
                    docValue.Append(NewLineValue);
                }
            }

            return docValue.ToString();
        }

        public bool Print(string ipAddress, uint port)
        {
            string retValue = null;
            return PrintingUtilities.SendASCIIStringToPrinter(ipAddress, port, GetDocumentValue(), out retValue);
        }

        public bool Save(string fileName)
        {
            try
            {
                var value = GetDocumentValue();

                using (var sw = new StreamWriter(fileName, false))
                {
                    sw.Write(value);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
