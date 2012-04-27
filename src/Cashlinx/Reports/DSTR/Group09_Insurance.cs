using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group09_Insurance : AbstractDSTRGroup
    {
        private int _Count;
        private decimal _TotalPaymentAmount;
        private decimal _TotalProcessingFee;
        private decimal _TotalWaivedAmount;
        private decimal _Total;

        public Group09_Insurance(DSTRReportContext dstrContext, string dataTableName)
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
                HeaderCells.Add(GetCell("Customer", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Policy Number", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Type", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Payment Type", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Payment Amount", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Processing Fee", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Waived Amount", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Total", DataFontBold, Element.ALIGN_RIGHT));

                PdfPTable dataTable = new PdfPTable(HeaderCells.Count);
                AddGroupTitle(dataTable, "Insurance");

                AddHeaderCells(dataTable);
                dataTable.HeaderRows = 2;

                PrintGroupDetail(dataTable);
                dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(_Count.ToString(), DataFont, Element.ALIGN_LEFT, 4, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalPaymentAmount), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalProcessingFee), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalWaivedAmount), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(string.Empty, DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_Total), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));

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
            decimal paymentAmount = GetDecimalValue(dataRow["TRANSAMT"]);
            decimal fee = GetDecimalValue(dataRow["FEE"]);
            decimal waivedfee = GetDecimalValue(dataRow["WAIVEFEE"]);
            decimal total = GetDecimalValue(dataRow["TOTAL"]);

            pdfTable.AddCell(GetCell(GetStringValue(dataRow["USERID"]), DataFont, Element.ALIGN_JUSTIFIED, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["CUSTOMER"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["POLICYNUMBER"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["INSTYPE"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["PAYMENTTYPE"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(paymentAmount), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(fee), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(waivedfee), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TRANSDATE"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(total), DataFont, Element.ALIGN_LEFT, shadeRow));

            _Count++;
            _TotalPaymentAmount += paymentAmount;
            _TotalProcessingFee += fee;
            _TotalWaivedAmount += waivedfee;
            _Total += total;
        }
    }
}
