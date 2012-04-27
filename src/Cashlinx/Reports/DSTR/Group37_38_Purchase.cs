using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group37_38_Purchase : AbstractDSTRGroup
    {
        protected string Title { get; set; }
        private decimal _TotalAmount;
        private int _Count;

        public Group37_38_Purchase(DSTRReportContext dstrContext, string dataTableName, string title)
            : base(dstrContext, dataTableName)
        {
            Title = title;
        }

        protected override void OnBuildSection()
        {
            try
            {
                if (GroupData.Rows.Count < 1)
                    return;

                // Header row
                HeaderCells.Add(GetCell("CSR #", DataFontBold, Element.ALIGN_JUSTIFIED));
                HeaderCells.Add(GetCell("Status", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Receipt #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Transaction #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Tender Type", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Customer/Vendor Name", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Amount", DataFontBold, Element.ALIGN_RIGHT));

                PdfPTable dataTable = new PdfPTable(HeaderCells.Count);
                AddGroupTitle(dataTable, Title);

                AddHeaderCells(dataTable);
                dataTable.HeaderRows = 2;

                PrintGroupDetail(dataTable);
                dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(_Count.ToString(), DataFont, Element.ALIGN_LEFT, 6, Rectangle.TOP_BORDER));
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
            string status = GetStringValue(dataRow["STATUS_CD"]);
            decimal amount = GetDecimalValue(dataRow["AMOUNT"]) ;
            string tender = GetStringValue(dataRow["TENDER_TYPE"]);

            amount *= (status == "VO") ? -1 : 1;

            Font detailFont = (status != "VO")? DataFont : DataFontBold;


            pdfTable.AddCell(GetCell(GetStringValue(dataRow["ENT_ID"]), detailFont, Element.ALIGN_JUSTIFIED, shadeRow));
            switch (status)
            {
                case "VO":
                    pdfTable.AddCell(GetCell("VOID", detailFont, Element.ALIGN_LEFT, shadeRow));
                    break;

                case "TO":
                case "PFI":
                case "PUR":
                    pdfTable.AddCell(GetCell("BUY", detailFont, Element.ALIGN_LEFT, shadeRow));
                    break;

                case "RET":
                    pdfTable.AddCell(GetCell("RET", detailFont, Element.ALIGN_LEFT, shadeRow));
                    break;
                default:
                    pdfTable.AddCell(GetCell(status, detailFont, Element.ALIGN_LEFT, shadeRow));
                    break;
            }
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["RECEIPT_NUMBER"]), detailFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TICKET_NUMBER"]), detailFont, Element.ALIGN_LEFT, shadeRow));

            if (tender.Equals("RCREDIT") || tender.Equals("RDEBIT"))
                pdfTable.AddCell(GetCell("CASH", detailFont, Element.ALIGN_LEFT, shadeRow));
            else if (tender.Substring(0, 4).Equals("BILLTOAP"))
                pdfTable.AddCell(GetCell("A/P", detailFont, Element.ALIGN_LEFT, shadeRow));
            else
                pdfTable.AddCell(GetCell(tender, detailFont, Element.ALIGN_LEFT, shadeRow));

            pdfTable.AddCell(GetCell(GetStringValue(dataRow["NAME"]), detailFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TIME_STR"]), detailFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(amount), detailFont, Element.ALIGN_RIGHT, shadeRow));

            if (status != "VO")
            {
                _Count++;
                _TotalAmount += amount;
            }
        }
    }
}
