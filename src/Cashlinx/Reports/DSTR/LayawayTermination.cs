using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class LayawayTermination : AbstractDSTRGroup
    {
        private int count;
        private decimal total;
        private string _title;

        public LayawayTermination(DSTRReportContext dstrContext, string dataTableName, string title)
            : base(dstrContext, dataTableName)
        {
            _title = title;
        }

        protected override void OnBuildSection()
        {
            try
            {
                if (GroupData.Rows.Count < 1)
                    return;

                // Header row
                HeaderCells.Add(GetCell("CSR #", DataFontBold, Element.ALIGN_JUSTIFIED));
                HeaderCells.Add(GetCell("Layaway #", DataFontBold, Element.ALIGN_CENTER));
                HeaderCells.Add(GetCell("Layaway Amount", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Type", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Customer or \nVendor Name", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_CENTER));
                HeaderCells.Add(GetCell("Forfeit / Termination Amount", DataFontBold, Element.ALIGN_RIGHT));

                PdfPTable dataTable = new PdfPTable(HeaderCells.Count);

                dataTable.SetWidths(new[] { 6f, 10f, 10f, 10f, 45f, 7f, 7f });
                AddGroupTitle(dataTable, _title);
                AddHeaderCells(dataTable);    
                dataTable.HeaderRows = 2;


                PrintGroupDetail(dataTable);
                dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(count.ToString(), DataFont, Element.ALIGN_LEFT, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell("", DataFont, Element.ALIGN_RIGHT, 4, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(total), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));

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
            Font font = DataFont;

            if (GetStringValue(dataRow["HOW_TERMINATED"]).Substring(0, 1) == "V")
            {
                font = DataFontBold;
                amount *= -1;
            }

            else
            {
                count++;
                total += amount;                
            }

            pdfTable.AddCell(GetCell(GetStringValue(dataRow["CSR"]), font, Element.ALIGN_JUSTIFIED, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["LAYAWAY_NR"]), font, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["LAYAWAY_AMT"]), font, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["HOW_TERMINATED"]), font, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["CUST_NAME"]), font, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TIME"]), font, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(amount), font, Element.ALIGN_RIGHT, shadeRow));


        }

    }
}
