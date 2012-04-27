using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group33_TransfersOut : AbstractDSTRGroup
    {
        private decimal _TotalAmount;
        private decimal _CountNonVoided;
        private string _title;

        public Group33_TransfersOut(DSTRReportContext dstrContext, string dataTableName, string title)
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

                DataRow[] rows;
                string colTitle = "Transfer To";

                if (_title.Equals("Transfers Out") )
                    rows = GroupData.Select("STATUS='TO' or STATUS='VOut'");
                else
                {
                    rows = GroupData.Select("STATUS='TI' or STATUS='VIn'"); 
                    colTitle = "Transfer From";
                }
                

                if (rows.GetLength(0) < 1 )
                    return;

                // Header row
                HeaderCells.Add(GetCell("CSR #", DataFontBold, Element.ALIGN_JUSTIFIED));
                HeaderCells.Add(GetCell("Transfer #", DataFontBold, Element.ALIGN_CENTER));
                HeaderCells.Add(GetCell("Status", DataFontBold, Element.ALIGN_CENTER));
                HeaderCells.Add(GetCell("Type", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell(colTitle, DataFontBold, Element.ALIGN_CENTER));
                HeaderCells.Add(GetCell("Total\nCost", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Total #\nItems", DataFontBold, Element.ALIGN_CENTER));

                

                PrintDetails(_title, rows);

            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
                ErrorCode = "1";
                throw;
            }
            return;
        }


        protected void PrintDetails (string title, DataRow [] rows)
        {
            PdfPTable dataTable = new PdfPTable(HeaderCells.Count);
            dataTable.SetWidths(new[] { 9f, 12f, 17f, 14f, 14f, 12f, 12f, 9f });


            AddGroupTitle(dataTable, title);

            AddHeaderCells(dataTable);
            dataTable.HeaderRows = 2;

            for (int i = 0; i < rows.GetLength(0); i++)
            {
                PrintGroupDetailRecord(dataTable, i, rows[i], !(i % 2 == 0));
            }


            dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 5, Rectangle.TOP_BORDER));
            dataTable.AddCell(GetCell(GetCurrencyValue(_TotalAmount), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
            dataTable.AddCell(GetCell("", DataFont, Element.ALIGN_LEFT, 1, Rectangle.TOP_BORDER));
            dataTable.AddCell(GetCell(_CountNonVoided.ToString(), DataFont, Element.ALIGN_CENTER, 1, Rectangle.TOP_BORDER));

            AddGroupContentsToPdfTable(dataTable);
        }



        protected override void PrintGroupDetailRecord(PdfPTable pdfTable, int index, DataRow dataRow, bool shadeRow)
        {
            decimal amount = GetDecimalValue(dataRow["AMOUNT"]);
            string status = GetStringValue(dataRow["TRANSFER_STATUS"]);
            Font rowFont = DataFont;
            bool isVoided = false;

            if (status == "VOID" || status == "REJECTED")
            {
                rowFont = DataFontBold;
                isVoided = true;
            }


            // row 1
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["ENT_ID"]), rowFont, Element.ALIGN_JUSTIFIED, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TRANSTICKETNUM"]), rowFont, Element.ALIGN_CENTER, shadeRow));
            pdfTable.AddCell(GetCell(status, rowFont, Element.ALIGN_CENTER, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TRANS_TYPE"]), rowFont, Element.ALIGN_LEFT, shadeRow));

            pdfTable.AddCell(GetCell(GetStringValue(dataRow["DESTINATION"]), rowFont, Element.ALIGN_CENTER, shadeRow));

            if (isVoided)
                pdfTable.AddCell(GetCell("(" + GetCurrencyValue(amount) + ")", rowFont, Element.ALIGN_RIGHT, shadeRow));
            else
                pdfTable.AddCell(GetCell(GetCurrencyValue(amount), rowFont, Element.ALIGN_RIGHT, shadeRow));

            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TIME"]), rowFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["CNT"]), rowFont, Element.ALIGN_CENTER, shadeRow));

            if (!isVoided)
            {
                _CountNonVoided += GetDecimalValue(dataRow["CNT"]);
                _TotalAmount += amount;
            }

            // row 2
            //pdfTable.AddCell(GetCell(GetStringValue(dataRow["DESCRIPTION"]), DataFont, Element.ALIGN_LEFT, shadeRow, 4));

            //Paragraph paragraph = GetICNParagraph(GetStringValue(dataRow["ICN"]));
            //pdfTable.AddCell(GetCell(paragraph, Element.ALIGN_LEFT, shadeRow, 2));
        }
    }
}
