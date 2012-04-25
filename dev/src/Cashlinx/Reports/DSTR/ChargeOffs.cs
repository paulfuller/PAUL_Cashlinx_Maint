using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR 
{
    class ChargeOffs : AbstractDSTRGroup
    {
        private decimal _TotalAmount;
        private int _CountNonVoided;

        public ChargeOffs(DSTRReportContext dstrContext, string dataTableName)
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
                HeaderCells.Add(GetCell("User ID", DataFontBold, Element.ALIGN_JUSTIFIED));
                HeaderCells.Add(GetCell("\nItem Description", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Loan or Buy #", DataFontBold, Element.ALIGN_CENTER));
                HeaderCells.Add(GetCell("ICN", DataFontBold, Element.ALIGN_CENTER));
                HeaderCells.Add(GetCell("Item Status", DataFontBold, Element.ALIGN_LEFT));

                HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Charge Off\nAmount", DataFontBold, Element.ALIGN_RIGHT));

                PdfPTable dataTable = new PdfPTable(HeaderCells.Count);
                AddGroupTitle(dataTable, "Charge Offs");

                AddHeaderCells(dataTable);
                dataTable.HeaderRows = 2;

                PrintGroupDetail(dataTable);
                dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(_CountNonVoided.ToString(), DataFont, Element.ALIGN_LEFT, 5, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalAmount), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));

                AddGroupContentsToPdfTable(dataTable);
            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
                ErrorCode = "1";
                throw;
            }
            return;
        }

        protected override void PrintGroupDetailRecord(PdfPTable pdfTable, int index, DataRow dataRow, bool shadeRow)
        {
            decimal amount = GetDecimalValue(dataRow["AMOUNT"]);
            string status = GetStringValue(dataRow["STATUS_REASON"]);

            // row 1
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["ENT_ID"]), DataFont, Element.ALIGN_JUSTIFIED, shadeRow));
            pdfTable.AddCell(GetCell("", DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TICKET_NUMBER"]), DataFont, Element.ALIGN_CENTER, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["ICN"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(status, DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TIME"]), DataFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(amount), DataFont, Element.ALIGN_RIGHT, shadeRow));

            if (status != "VO")
            {
                _CountNonVoided++;
                _TotalAmount += amount;
            }

            // row 2
            pdfTable.AddCell(GetCell("", DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["ITEM_DESC"]), DataFont, Element.ALIGN_LEFT, shadeRow, 6));
        }
    }
}
