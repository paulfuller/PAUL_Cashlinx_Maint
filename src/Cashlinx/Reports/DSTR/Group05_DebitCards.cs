using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group05_DebitCards : AbstractDSTRGroup
    {
        private int _CountOfGiftCards;
        private int _CountOfNewCards;
        private int _CountOfReloads;
        private int _CountOfCustomers;
        private decimal _TotalAmount;
        private decimal _TotalFee;
        private decimal _TotalReloadFee;

        public Group05_DebitCards(DSTRReportContext dstrContext, string dataTableName)
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
                HeaderCells.Add(GetCell("Manual #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("New Card", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Reload", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Gift Card", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Customer", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Fee", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Reload Fee", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Trans Amount", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_LEFT));

                PdfPTable dataTable = new PdfPTable(HeaderCells.Count);
                AddGroupTitle(dataTable, "Debit Card");

                AddHeaderCells(dataTable);
                dataTable.HeaderRows = 2;

                PrintGroupDetail(dataTable);
                dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 2, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(_CountOfNewCards.ToString(), DataFont, Element.ALIGN_LEFT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(_CountOfReloads.ToString(), DataFont, Element.ALIGN_LEFT, 2, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(_CountOfCustomers.ToString(), DataFont, Element.ALIGN_LEFT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalFee), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalReloadFee), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalAmount), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
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
            decimal amount = GetDecimalValue(dataRow["AMOUNT"]);
            decimal fee = GetDecimalValue(dataRow["FEE"]);
            decimal reloadFee = GetDecimalValue(dataRow["RELOAD_FEE"]);
            string customerName = GetStringValue(dataRow["CUSTOMERNAME"]);
            string newCard = GetStringValue(dataRow["NEWCARD"]);
            string reload = GetStringValue(dataRow["RELOAD"]);
            string gift = GetStringValue(dataRow["GIFTCARD"]);

            pdfTable.AddCell(GetCell(GetStringValue(dataRow["USERID"]), DataFont, Element.ALIGN_JUSTIFIED, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["MANUALNO"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(newCard, DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(reload, DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(gift, DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(customerName, DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(fee), DataFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(reloadFee), DataFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(amount), DataFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TRANSACTIONDATE"]), DataFont, Element.ALIGN_LEFT, shadeRow));

            _TotalAmount += amount;
            _TotalFee += fee;
            _TotalReloadFee += reloadFee;
            _CountOfCustomers++;
            if (newCard == "Y")
            {
                _CountOfNewCards++;
            }
            if (reload == "Y")
            {
                _CountOfReloads++;
            }
            if (gift == "Y")
            {
                _CountOfGiftCards++;
            }
        }
    }
}
