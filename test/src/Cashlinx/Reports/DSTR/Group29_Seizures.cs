using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group29_Seizures : AbstractDSTRGroup
    {
        private decimal _TotalAmountSeizure;
        private decimal _TotalAmountRestitution;
        private int _CountNonVoided;

        public Group29_Seizures(DSTRReportContext dstrContext, string dataTableName)
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
                HeaderCells.Add(GetCell("CSR #\nItem Description", DataFontBold, Element.ALIGN_JUSTIFIED));
                HeaderCells.Add(GetCell("Loan Status", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Seize #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Loan or Buy #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Customer", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Time\nICN", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Agency", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Seizure Amt", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Restitution", DataFontBold, Element.ALIGN_RIGHT));

                PdfPTable dataTable = new PdfPTable(HeaderCells.Count);
                AddGroupTitle(dataTable, "Police Seizure");

                AddHeaderCells(dataTable);
                dataTable.HeaderRows = 2;

                PrintGroupDetail(dataTable);
                dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(_CountNonVoided.ToString(), DataFont, Element.ALIGN_LEFT, 5, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalAmountSeizure), DataFont, Element.ALIGN_RIGHT, 2, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalAmountRestitution), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));

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
            decimal amountSeizure = GetDecimalValue(dataRow["PFI_AMOUNT"]);
            decimal amountRestitution = GetDecimalValue(dataRow["RESTITUTION_AMT"]);
            string status = GetStringValue(dataRow["STATUS_CD"]);

            // row 1
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["ENT_ID"]), DataFont, Element.ALIGN_JUSTIFIED, shadeRow));
            pdfTable.AddCell(GetCell(status, DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["RECEIPT_NUMBER"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TICKET_NUMBER"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["NAME"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TIME"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["AGENCY"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(amountSeizure), DataFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(amountRestitution), DataFont, Element.ALIGN_RIGHT, shadeRow));

            if (status != "VO")
            {
                _CountNonVoided++;
                _TotalAmountSeizure += amountSeizure;
                _TotalAmountRestitution += amountRestitution;
            }

            // row 2
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["ITEM_DESC"]), DataFont, Element.ALIGN_LEFT, shadeRow, 5));

            Paragraph paragraph = GetICNParagraph(GetStringValue(dataRow["ICN_NUM"]));
            pdfTable.AddCell(GetCell(paragraph, Element.ALIGN_LEFT, shadeRow, 4));
        }
    }
}
