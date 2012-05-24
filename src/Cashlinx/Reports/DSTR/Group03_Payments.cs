using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group03_Payments : AbstractDSTRGroup
    {
        private int _Count;
        private decimal _Total;

        public Group03_Payments(DSTRReportContext dstrContext, string dataTableName)
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
                HeaderCells.Add(GetCell("MOP", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("CSR #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Receipt #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Manual #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Customer", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Trans Amount", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Transaction Type", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_LEFT));

                PdfPTable dataTable = new PdfPTable(HeaderCells.Count);
                AddGroupTitle(dataTable, "PayDay & Brokered Loan Payments");

                AddHeaderCells(dataTable);
                dataTable.HeaderRows = 2;
                dataTable.SetWidths(new float[] { 1, 1, 1, 1, 1.5f, 1, 1.5f, 1 });

                PrintGroupDetail(dataTable);
                dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(_Count.ToString(), DataFont, Element.ALIGN_LEFT, 4, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_Total), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(string.Empty, DataFont, Element.ALIGN_JUSTIFIED, 2, Rectangle.TOP_BORDER));

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
            decimal amount = GetDecimalValue(dataRow["TRANSAMT"]);

            pdfTable.AddCell(GetCell(GetStringValue(dataRow["MOP"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["USERID"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["RECEIPTNO"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["MANUALNO"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["CUSTOMER"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(amount), DataFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TRANSTYPE"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TRANSDATE"]), DataFont, Element.ALIGN_LEFT, shadeRow));

            _Count++;
            _Total += amount;
        }
    }
}
