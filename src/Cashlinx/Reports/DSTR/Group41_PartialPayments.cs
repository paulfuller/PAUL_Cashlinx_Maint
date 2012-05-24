using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group41_PartialPayments : AbstractDSTRGroup
    {
        private int _Count;
        private decimal _Total;
        private decimal _PrincipalReductionTotal;
        private decimal _FinanceChargesTotal;
        private decimal _PartialPaymentAmountTotal;

        public Group41_PartialPayments(DSTRReportContext dstrContext, string dataTableName)
            : base(dstrContext, dataTableName)
        {

        }

        protected override void OnBuildSection()
        {
            try
            {
                // Header row
                HeaderCells.Add(GetCell("User ID", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Transaction Status", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Receipt #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Loan #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Customer", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Principal Reduction", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Finance Charges", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Partial Payment Amount", DataFontBold, Element.ALIGN_LEFT));

                PdfPTable dataTable = new PdfPTable(HeaderCells.Count);
                AddGroupTitle(dataTable, "Partial Payments");

                AddHeaderCells(dataTable);
                dataTable.HeaderRows = 2;

                PrintGroupDetail(dataTable);
                dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(_Count.ToString(), DataFont, Element.ALIGN_LEFT, 5, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_PrincipalReductionTotal), DataFont, Element.ALIGN_LEFT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_FinanceChargesTotal), DataFont, Element.ALIGN_LEFT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_PartialPaymentAmountTotal), DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));

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
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["ent_id"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["STATUS_CD"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["RECEIPT_NUMBER"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TICKET_NUMBER"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["NAME"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TIME"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(dataRow["PMT_PRIN_AMT"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(dataRow["TTLFEE"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(dataRow["TTL"]), DataFont, Element.ALIGN_LEFT, shadeRow));

            _Total += GetDecimalValue(dataRow["TTL"]);
            _PrincipalReductionTotal += GetDecimalValue(dataRow["PMT_PRIN_AMT"]);
            _FinanceChargesTotal += GetDecimalValue(dataRow["TTLFEE"]);
            _PartialPaymentAmountTotal += GetDecimalValue(dataRow["TTL"]);

            _Count++;
        }
    }
}
