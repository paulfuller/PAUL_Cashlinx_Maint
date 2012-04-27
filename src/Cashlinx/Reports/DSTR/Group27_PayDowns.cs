using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group27_PayDowns : AbstractDSTRGroup
    {
        private decimal _TotalAmount;
        private decimal _TotalLoanAmt;
        private decimal _TotalPscAmt;
        private int _CountNonVoided;

        public Group27_PayDowns(DSTRReportContext dstrContext, string dataTableName)
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
                HeaderCells.Add(GetCell("CSR #", DataFontBold, Element.ALIGN_JUSTIFIED));
                HeaderCells.Add(GetCell("Transaction Status", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Receipt #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Loan #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Customer", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Loan Amount", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Paydown Amount", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("PSC", DataFontBold, Element.ALIGN_RIGHT));

                PdfPTable dataTable = new PdfPTable(HeaderCells.Count);
                AddGroupTitle(dataTable, "Paydowns");

                AddHeaderCells(dataTable);
                dataTable.HeaderRows = 2;

                PrintGroupDetail(dataTable);
                dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(_CountNonVoided.ToString(), DataFont, Element.ALIGN_LEFT, 5, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalLoanAmt), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalAmount), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalPscAmt), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));

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
            decimal amount = GetDecimalValue(dataRow["PAYDOWNAMOUNT"]);
            decimal loanAmt = GetDecimalValue(dataRow["LOANAMOUNT"]);
            decimal pscAmt = GetDecimalValue(dataRow["PSC"]);
            string status = GetStringValue(dataRow["STATUS_CD"]);
            Font rowFont = DataFont;
            bool isVoided = false;

            if (status == "VO")
            {
                rowFont = DataFontBold;
                isVoided = true;
            }

            pdfTable.AddCell(GetCell(GetStringValue(dataRow["ENT_ID"]), rowFont, Element.ALIGN_JUSTIFIED, shadeRow));
            pdfTable.AddCell(GetCell(status, rowFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["RECEIPT_NUMBER"]), rowFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TICKET_NUMBER"]), rowFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["NAME"]), rowFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TIME"]), rowFont, Element.ALIGN_LEFT, shadeRow));
            if (isVoided)
            {
                pdfTable.AddCell(GetCell("(" + GetCurrencyValue(loanAmt) + ")", rowFont, Element.ALIGN_RIGHT, shadeRow));
                pdfTable.AddCell(GetCell("(" + GetCurrencyValue(amount) + ")", rowFont, Element.ALIGN_RIGHT, shadeRow));
                pdfTable.AddCell(GetCell("(" + GetCurrencyValue(pscAmt) + ")", rowFont, Element.ALIGN_RIGHT, shadeRow));
            }
            else
            {
                pdfTable.AddCell(GetCell(GetCurrencyValue(loanAmt), rowFont, Element.ALIGN_RIGHT, shadeRow));
                pdfTable.AddCell(GetCell(GetCurrencyValue(amount), rowFont, Element.ALIGN_RIGHT, shadeRow));
                pdfTable.AddCell(GetCell(GetCurrencyValue(pscAmt), rowFont, Element.ALIGN_RIGHT, shadeRow));
            }

            if (!isVoided)
            {
                _CountNonVoided++;
                _TotalAmount += amount;
                _TotalLoanAmt += loanAmt;
                _TotalPscAmt += pscAmt;
            }
        }
    }
}
