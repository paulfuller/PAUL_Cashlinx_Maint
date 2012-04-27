using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group07_CashTransfers : AbstractDSTRGroup
    {
        public Group07_CashTransfers(DSTRReportContext dstrContext, string dataTableName)
            : base(dstrContext, dataTableName)
        {
        }

        private CashTransferGroupTotals _SafeTotals = new CashTransferGroupTotals();
        private CashTransferGroupTotals _DrawerTotals = new CashTransferGroupTotals();

        private bool _PrintDrawerTotals = false;

        protected override void OnBuildSection()
        {
            try
            {
                if (GroupData.Rows.Count < 1)
                    return;

                if (DataContainsTransferType("SAFE_TRANSFER_7"))
                {
                    PrintInternalGroup("Safe Transfers");
                }
                if (DataContainsTransferType("DRAWER_TRANSFER_7"))
                {
                    _PrintDrawerTotals = true;
                    PrintInternalGroup("Drawer Transfers");
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
            HeaderCells.Add(GetCell("CSR #", DataFontBold, Element.ALIGN_LEFT));
            HeaderCells.Add(GetCell("Description", DataFontBold, Element.ALIGN_LEFT));
            HeaderCells.Add(GetCell("Amount", DataFontBold, Element.ALIGN_RIGHT));
            if (!_PrintDrawerTotals)
            {
                HeaderCells.Add(GetCell("Comment", DataFontBold, Element.ALIGN_LEFT));
            }
            HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_LEFT));

            PdfPTable dataTable = new PdfPTable(HeaderCells.Count);

            AddGroupTitle(dataTable, groupTitle);

            AddHeaderCells(dataTable);
            dataTable.HeaderRows = 2;

            PrintGroupDetail(dataTable);

            CashTransferGroupTotals totals = _PrintDrawerTotals ? _DrawerTotals : _SafeTotals;

            dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));
            dataTable.AddCell(GetCell(totals.Count.ToString(), DataFont, Element.ALIGN_LEFT, 1, Rectangle.TOP_BORDER));
            dataTable.AddCell(GetCell(GetCurrencyValue(totals.Total), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));
            dataTable.AddCell(GetCell(string.Empty, DataFont, Element.ALIGN_JUSTIFIED, HeaderCells.Count - 3, Rectangle.TOP_BORDER));

            AddGroupContentsToPdfTable(dataTable);
        }

        protected override void PrintGroupDetailRecord(PdfPTable pdfTable, int index, DataRow dataRow, bool shadeRow)
        {
            decimal amount = GetDecimalValue(dataRow["AMOUNT"]);
            string srcTable = GetStringValue(dataRow["SRC_TABLE"]);
            string status = GetStringValue(dataRow["STATUS"]);
            Font theFont = DataFont;
            string amtFormat = "{0:c}";
            if ((srcTable == "SAFE_TRANSFER_7" && _PrintDrawerTotals) || (srcTable == "DRAWER_TRANSFER_7" && !_PrintDrawerTotals))
            {
                return;
            }

            if (status == "VOID")
            {
                theFont.SetStyle(Font.BOLD);
                amtFormat = "({0:c})";
            }
                
            else
            {
                if (_PrintDrawerTotals)
                {
                    _DrawerTotals.Count++;
                    _DrawerTotals.Total += amount;
                }
                else
                {
                    _SafeTotals.Count++;
                    _SafeTotals.Total += amount;
                }
            }

            pdfTable.AddCell(GetCell(GetStringValue(dataRow["USERID"]), theFont, Element.ALIGN_JUSTIFIED, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["DESCRIPTION"]), theFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(string.Format(amtFormat, amount), theFont, Element.ALIGN_RIGHT, shadeRow));
            if (!_PrintDrawerTotals)
                pdfTable.AddCell(GetCell(GetStringValue(dataRow["COMMENTS"]), theFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TRANSACTIONDATE"]), theFont, Element.ALIGN_LEFT, shadeRow));


        }

        protected override void LoadDataTable(string dataTableName)
        {
            DataTable dtSafeTransfer;
            DataTable dtDrawerTransfer;

            DSTRContext.ReportData.GetTable("SAFE_TRANSFER_7", out dtSafeTransfer);
            DSTRContext.ReportData.GetTable("DRAWER_TRANSFER_7", out dtDrawerTransfer);

            DataTable dt = new DataTable();
            dt.Columns.Add("USERID", typeof(System.String));
            dt.Columns.Add("DESCRIPTION", typeof(System.String));
            dt.Columns.Add("AMOUNT", typeof(System.Decimal));
            dt.Columns.Add("TRANSACTIONDATE", typeof(System.String));
            dt.Columns.Add("COMMENTS", typeof(System.String));
            dt.Columns.Add("amount", typeof(System.Decimal));
            dt.Columns.Add("SRC_TABLE", typeof(System.String));
            dt.Columns.Add("STATUS", typeof(System.String));

            if (dtSafeTransfer != null)
            {
                foreach (DataRow drSafeTransfer in dtSafeTransfer.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["USERID"] = drSafeTransfer["USERID"];
                    dr["DESCRIPTION"] = drSafeTransfer["DESCRIPTION"];
                    dr["AMOUNT"] = drSafeTransfer["AMOUNT"];
                    dr["TRANSACTIONDATE"] = drSafeTransfer["TRANSACTIONDATE"];
                    dr["COMMENTS"] = drSafeTransfer["COMMENTS"];
                    dr["amount"] = drSafeTransfer["amount"];
                    dr["SRC_TABLE"] = "SAFE_TRANSFER_7";
                    dr["STATUS"] = drSafeTransfer["TRANSFERSTATUS"];
                    dt.Rows.Add(dr);
                }
            }

            if (dtDrawerTransfer != null)
            {
                foreach (DataRow drDrawerTransfer in dtDrawerTransfer.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["USERID"] = drDrawerTransfer["USERID"];
                    dr["DESCRIPTION"] = drDrawerTransfer["DESCRIPTION"];
                    dr["AMOUNT"] = drDrawerTransfer["AMOUNT"];
                    dr["TRANSACTIONDATE"] = drDrawerTransfer["TRANSACTIONDATE"];
                    dr["COMMENTS"] = string.Empty;
                    dr["amount"] = drDrawerTransfer["amount"];
                    dr["SRC_TABLE"] = "DRAWER_TRANSFER_7";
                    dr["STATUS"] = drDrawerTransfer["TRANSFERSTATUS"];

                    dt.Rows.Add(dr);
                }
            }

            if (dt.Rows.Count > 0)
            {
                GroupData = dt;
                DataLoaded = true;
            }
        }

        private bool DataContainsTransferType(string transferType)
        {
            foreach (DataRow dr in GroupData.Rows)
            {
                if (GetStringValue(dr["SRC_TABLE"]) == transferType)
                {
                    return true;
                }
            }

            return false;
        }

        public class CashTransferGroupTotals
        {
            public decimal Total { get; set; }
            public int Count { get; set; }
        }
    }
}
