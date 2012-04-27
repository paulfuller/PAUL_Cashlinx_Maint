using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group01_CashAdvance : AbstractDSTRGroup
    {
        private int _CustomersCount;
        private decimal _SumOfLoanAmount;
        private decimal _SumOfFinanceCharge;
        private decimal _SumOfCSOFee;
        private decimal _SumOfNetTotal;

        public Group01_CashAdvance(DSTRReportContext dstrContext, string dataTableName)
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
                HeaderCells.Add(GetCell("Loan Status", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Loan #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Prev Loan", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Customer", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Loan Amt.", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Finance Charge", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("CSO Fee", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Total", DataFontBold, Element.ALIGN_RIGHT));

                PdfPTable dataTable = new PdfPTable(HeaderCells.Count);
                AddGroupTitle(dataTable, "Cash Advance");
                dataTable.SetWidths(new float[] { 1, 1, 1, 1, 1.5f, 1, 1, 1, 1, 1 });

                AddHeaderCells(dataTable);
                dataTable.HeaderRows = 2;

                PrintGroupDetail(dataTable);
                dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 4, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(_CustomersCount.ToString(), DataFont, Element.ALIGN_LEFT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_SumOfLoanAmount), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_SumOfFinanceCharge), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_SumOfCSOFee), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_SumOfNetTotal), DataFont, Element.ALIGN_RIGHT, 2, Rectangle.TOP_BORDER));

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
            string customerName = GetStringValue(dataRow["cust_name"]);
            decimal amount = GetDecimalValue(dataRow["amount"]);
            decimal csoFee = GetDecimalValue(dataRow["cso_fee"]);
            decimal financeCharge = GetDecimalValue(dataRow["finc_chg"]);
            decimal total = GetDecimalValue(dataRow["total"]);

            pdfTable.AddCell(GetCell(GetStringValue(dataRow["csr_nr"]), DataFont, Element.ALIGN_JUSTIFIED, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["status"]), DataFont, Element.ALIGN_LEFT, shadeRow, true));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["LOANNUMBER"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["prev_loan_nr"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(customerName, DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(amount), DataFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(financeCharge), DataFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(csoFee), DataFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["time"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(total), DataFont, Element.ALIGN_RIGHT, shadeRow));

            _CustomersCount++;
            _SumOfLoanAmount += amount;
            _SumOfFinanceCharge += financeCharge;
            _SumOfCSOFee += csoFee;
            _SumOfNetTotal += total;
        }
    }
}
