using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group15_WireTransfer : AbstractDSTRGroup
    {
        private bool _PrintingReceived = false;

        private WireTransferGroupTotals _SentTotals = new WireTransferGroupTotals();
        private WireTransferGroupTotals _ReceivedTotals = new WireTransferGroupTotals();

        public Group15_WireTransfer(DSTRReportContext dstrContext, string dataTableName)
            : base(dstrContext, dataTableName)
        {

        }

        protected override void OnBuildSection()
        {
            try
            {
                if (GroupData.Rows.Count < 1)
                    return;

                if (DataIsAvailable(true))
                {
                    PrintInternalGroup("Wire Transfer - Sent");
                }
                if (DataIsAvailable(false))
                {
                    _PrintingReceived = true;
                    PrintInternalGroup("Wire Transfer - Received");
                }

            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
                ErrorCode = "1";
                throw;
            }
        }

        private void PrintInternalGroup(string groupTitle)
        {
            // Header row
            HeaderCells.Clear();
            HeaderCells.Add(GetCell("CSR #", DataFontBold, Element.ALIGN_JUSTIFIED));
            if (_PrintingReceived)
            {
                HeaderCells.Add(GetCell("Amount Recieved", DataFontBold, Element.ALIGN_RIGHT));
            }
            else
            {
                HeaderCells.Add(GetCell("Amount Sent", DataFontBold, Element.ALIGN_RIGHT));
            }
            HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_LEFT));
            if (!_PrintingReceived)
            {
                HeaderCells.Add(GetCell("Fee", DataFontBold, Element.ALIGN_RIGHT));
            }
            HeaderCells.Add(GetCell("Customer Name", DataFontBold, Element.ALIGN_LEFT));
            if (!_PrintingReceived)
            {
                HeaderCells.Add(GetCell("Send Type", DataFontBold, Element.ALIGN_LEFT));
            }
            HeaderCells.Add(GetCell("MTCN", DataFontBold, Element.ALIGN_LEFT));

            PdfPTable dataTable = new PdfPTable(HeaderCells.Count);
            AddGroupTitle(dataTable, groupTitle);

            AddHeaderCells(dataTable);
            dataTable.HeaderRows = 2;

            PrintGroupDetail(dataTable);

            WireTransferGroupTotals totals = _PrintingReceived ? _ReceivedTotals : _SentTotals;

            dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));
            dataTable.AddCell(GetCell(GetCurrencyValue(totals.Total), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
            dataTable.AddCell(GetCell(string.Empty, DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));
            if (!_PrintingReceived)
            {
                dataTable.AddCell(GetCell(GetCurrencyValue(totals.TotalFees), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
            }
            dataTable.AddCell(GetCell(totals.Count.ToString(), DataFont, Element.ALIGN_LEFT, _PrintingReceived ? 2 : 3, Rectangle.TOP_BORDER));
            
            dataTable.AddCell(GetCell(string.Empty, DataFont, Element.ALIGN_JUSTIFIED, 2, Rectangle.TOP_BORDER));

            AddGroupContentsToPdfTable(dataTable);
        }

        protected override void PrintGroupDetailRecord(PdfPTable pdfTable, int index, DataRow dataRow, bool shadeRow)
        {
            bool isSent = GetStringValue(dataRow["isSent"]) == "1";
            if ((_PrintingReceived && isSent) || (!_PrintingReceived && !isSent))
            {
                return;
            }

            decimal amount = GetDecimalValue(dataRow["AMOUNT"]);
            decimal fee = GetDecimalValue(dataRow["FEE"]);

            pdfTable.AddCell(GetCell(GetStringValue(dataRow["USERID"]), DataFont, Element.ALIGN_JUSTIFIED, shadeRow));
            pdfTable.AddCell(GetCell(GetCurrencyValue(amount), DataFont, Element.ALIGN_RIGHT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["time"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            if (!_PrintingReceived)
            {
                pdfTable.AddCell(GetCell(GetCurrencyValue(fee), DataFont, Element.ALIGN_RIGHT, shadeRow));
            }
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["CUSTOMER"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            if (!_PrintingReceived)
            {
                pdfTable.AddCell(GetCell(GetStringValue(dataRow["SENDTYPE"]), DataFont, Element.ALIGN_LEFT, shadeRow));
            }
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["MTCN"]), DataFont, Element.ALIGN_LEFT, shadeRow));

            WireTransferGroupTotals totals = _PrintingReceived ? _ReceivedTotals : _SentTotals;
            totals.Count++;
            totals.Total += amount;
            if (!_PrintingReceived)
            {
                totals.TotalFees += fee;
            }
        }

        private bool DataIsAvailable(bool isSent)
        {
            foreach (DataRow dr in GroupData.Rows)
            {
                if (GetStringValue(dr["isSent"]) == "1" == isSent)
                {
                    return true;
                }
            }

            return false;
        }

        public class WireTransferGroupTotals
        {
            public decimal Total { get; set; }
            public decimal TotalFees { get; set; }
            public int Count { get; set; }
        }
    }
}
