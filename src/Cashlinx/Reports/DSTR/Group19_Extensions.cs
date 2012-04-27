using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group19_Extensions : AbstractDSTRGroup
    {
        private int _Count;

        public Group19_Extensions(DSTRReportContext dstrContext, string dataTableName)
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
                HeaderCells.Add(GetCell("CSR #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Status", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Customer", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Orig. Dep. Date", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("New Dep. Date", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("# Days Extended", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Reason", DataFontBold, Element.ALIGN_LEFT));

                PdfPTable dataTable = new PdfPTable(HeaderCells.Count);
                AddGroupTitle(dataTable, "Extensions");

                AddHeaderCells(dataTable);
                dataTable.HeaderRows = 2;

                PrintGroupDetail(dataTable);
                dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 2, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(_Count.ToString(), DataFont, Element.ALIGN_LEFT, 6, Rectangle.TOP_BORDER));

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
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["USERID"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["STATUS"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["CUSTOMERNAME"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["ORIDEPOSITDATE"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["DEPOSITDATE"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["DAYSEXTENTED"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["time"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["REASON"]), DataFont, Element.ALIGN_LEFT, shadeRow));

            _Count++;
        }
    }
}
