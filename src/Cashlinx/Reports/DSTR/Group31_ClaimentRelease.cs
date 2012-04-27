using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group31_ClaimentRelease : AbstractDSTRGroup
    {
        private decimal _TotalAmountRelease;
        private decimal _TotalAmountRestitution;
        private int _CountNonVoided;

        public Group31_ClaimentRelease(DSTRReportContext dstrContext, string dataTableName)
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
                HeaderCells.Add(GetCell("Release #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Loan or Buy #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Customer", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Claimant\nICN", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Release Amount", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Restitution", DataFontBold, Element.ALIGN_RIGHT));

                PdfPTable dataTable = new PdfPTable(HeaderCells.Count);
                AddGroupTitle(dataTable, "Item Releases to Claimant");

                AddHeaderCells(dataTable);
                dataTable.HeaderRows = 2;

                PrintGroupDetail(dataTable);
                dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(_CountNonVoided.ToString(), DataFont, Element.ALIGN_LEFT, 6, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalAmountRelease), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
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
            decimal amountRelease = GetDecimalValue(dataRow["PFI_AMOUNT"]);
            decimal amountRestitution = GetDecimalValue(dataRow["RESTITUTION_AMT"]);
            string status = GetStringValue(dataRow["STATUS_CD"]);

            // row 1
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["ENT_ID"]), DataFont, Element.ALIGN_JUSTIFIED, shadeRow));
            pdfTable.AddCell(GetCell(status, DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["RECEIPT_NUMBER"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TICKET_NUMBER"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["CUST_NAME"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TIME"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["CLAIM_NAME"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(amountRelease), DataFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(amountRestitution), DataFont, Element.ALIGN_RIGHT, shadeRow));

            if (status != "VO")
            {
                _CountNonVoided++;
                _TotalAmountRelease += amountRelease;
                _TotalAmountRestitution += amountRestitution;
            }

            // row 2
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["MD_DESC"]), DataFont, Element.ALIGN_LEFT, shadeRow, 6));

            Paragraph paragraph = GetICNParagraph(GetStringValue(dataRow["ICN_NUM"]));
            pdfTable.AddCell(GetCell(paragraph, Element.ALIGN_LEFT, shadeRow, 3));
        }
    }
}
