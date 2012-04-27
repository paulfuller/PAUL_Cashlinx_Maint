using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group11_ConvenienceItems : AbstractDSTRGroup
    {
        private int _Count;
        private decimal _Total;

        public Group11_ConvenienceItems(DSTRReportContext dstrContext, string dataTableName)
            : base(dstrContext, dataTableName)
        {
        }

        protected override void OnBuildSection()
        {
            try
            {
                if (GroupData.Rows.Count < 1)
                    return;

                // Header row
                HeaderCells.Add(GetCell("CSR #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Name of Item", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Quantity", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Amount", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_LEFT));

                PdfPTable dataTable = new PdfPTable(HeaderCells.Count);
                AddGroupTitle(dataTable, "Convenience Items");

                AddHeaderCells(dataTable);
                dataTable.HeaderRows = 2;

                PrintGroupDetail(dataTable);
                dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(_Count.ToString(), DataFont, Element.ALIGN_LEFT, 2, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_Total), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(string.Empty, DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));

                AddGroupContentsToPdfTable(dataTable);

            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
                ErrorCode = "1";
                throw;
            }
        }

        protected override void PrintGroupDetailRecord(PdfPTable pdfTable, int index, DataRow dataRow, bool shadeRow)
        {
            decimal amount = GetDecimalValue(dataRow["TRANSACTIONAMT"]);

            pdfTable.AddCell(GetCell(GetStringValue(dataRow["USERID"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["PRODUCTDESC"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["QUANTITY"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(amount), DataFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["time"]), DataFont, Element.ALIGN_LEFT, shadeRow));

            _Count++;
            _Total += amount;
        }
    }
}
