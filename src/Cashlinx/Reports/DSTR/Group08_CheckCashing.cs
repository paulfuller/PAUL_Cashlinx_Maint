using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group08_CheckCashing : AbstractDSTRGroup
    {
        private int _Count;
        private decimal _TotalAmount;
        private decimal _TotalCustHandAmount;
        private decimal _TotalFee;
        private decimal _TotalThirdPartyFee;

        public Group08_CheckCashing(DSTRReportContext dstrContext, string dataTableName)
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
                HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Customer", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Maker", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Check #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Check Date", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Amount", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Fee", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Fee Adj", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("3rd Party Fee", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Customer Hand Amt", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Check Type", DataFontBold, Element.ALIGN_LEFT));

                PdfPTable dataTable = new PdfPTable(HeaderCells.Count);
                AddGroupTitle(dataTable, "Check Cashing");

                AddHeaderCells(dataTable);
                dataTable.HeaderRows = 2;
                dataTable.SetWidths(new float[] { 1, 1, 1.5f, 1.5f, 1, 1, 1, 1, 1, 1, 1, 1 });

                PrintGroupDetail(dataTable);
                dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 2, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(_Count.ToString(), DataFont, Element.ALIGN_LEFT, 3, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalAmount), DataFont, Element.ALIGN_RIGHT, 2, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalFee), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalThirdPartyFee), DataFont, Element.ALIGN_RIGHT, 2, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalCustHandAmount), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
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
            decimal amount = GetDecimalValue(dataRow["CHECKAMOUNT"]);
            decimal custHandAmount = GetDecimalValue(dataRow["CUSTOMERHANDAMT"]);
            decimal fee = GetDecimalValue(dataRow["CHECKFEE"]);
            decimal thirdPartyFee = GetDecimalValue(dataRow["THIRDPARTYFEE"]);

            pdfTable.AddCell(GetCell(GetStringValue(dataRow["USERID"]), DataFont, Element.ALIGN_JUSTIFIED, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["time"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["CUSTOMERNAME"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["MAKERNAME"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["CHECKNUMBER"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["CHECKDATE"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(amount), DataFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(fee), DataFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(dataRow["FEEADJ"]), DataFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(thirdPartyFee), DataFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(custHandAmount), DataFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["CHECKTYPECODE"]), DataFont, Element.ALIGN_LEFT, shadeRow));

            _Count++;
            _TotalAmount += amount;
            _TotalCustHandAmount += custHandAmount;
            _TotalFee += fee;
            _TotalThirdPartyFee += thirdPartyFee;
        }
    }
}
