﻿using System;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group28_Pickups : AbstractDSTRGroup
    {
        private decimal _TotalAmount;
        private decimal _TotalAmountService;
        private int _CountNonVoided;

        public Group28_Pickups(DSTRReportContext dstrContext, string dataTableName)
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
                HeaderCells.Add(GetCell("Transaction Status", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Receipt #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Loan #", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Customer", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Time", DataFontBold, Element.ALIGN_LEFT));
                HeaderCells.Add(GetCell("Loan Amt.", DataFontBold, Element.ALIGN_RIGHT));
                HeaderCells.Add(GetCell("Pawn Service Charge", DataFontBold, Element.ALIGN_RIGHT));

                PdfPTable dataTable = new PdfPTable(HeaderCells.Count);
                AddGroupTitle(dataTable, "Pawn Loan Pickup");

                AddHeaderCells(dataTable);
                dataTable.HeaderRows = 2;

                PrintGroupDetail(dataTable);
                dataTable.AddCell(GetCell("Total", DataFont, Element.ALIGN_JUSTIFIED, 1, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(_CountNonVoided.ToString(), DataFont, Element.ALIGN_LEFT, 3, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalAmount), DataFont, Element.ALIGN_RIGHT, 3, Rectangle.TOP_BORDER));
                dataTable.AddCell(GetCell(GetCurrencyValue(_TotalAmountService), DataFont, Element.ALIGN_RIGHT, 1, Rectangle.TOP_BORDER));

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
            decimal amount = GetDecimalValue(dataRow["TTL"]);
            decimal amountService = GetDecimalValue(dataRow["TTLFEE"]);
            string status = GetStringValue(dataRow["STATUS_CD"]);
            Font rowFont = DataFont;
            bool isVoided = false;

            if (status == "VO")
            {
                rowFont = DataFontBold;
                isVoided = true;
            }

            pdfTable.AddCell(GetCell(GetStringValue(dataRow["ENT_ID"]), rowFont, Element.ALIGN_JUSTIFIED, shadeRow));
            pdfTable.AddCell(GetCell(status, rowFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["RECEIPT_NUMBER"]), rowFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TICKET_NUMBER"]), rowFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["NAME"]), rowFont, Element.ALIGN_LEFT, shadeRow));
            pdfTable.AddCell(GetCell(GetStringValue(dataRow["TIME"]), rowFont, Element.ALIGN_LEFT, shadeRow));

            if (isVoided)
            {
                pdfTable.AddCell(GetCell("(" + GetCurrencyValue(amount) + ")", rowFont, Element.ALIGN_RIGHT, shadeRow));
                pdfTable.AddCell(GetCell("(" + GetCurrencyValue(amountService) + ")", rowFont, Element.ALIGN_RIGHT, shadeRow));
            }
            else
            {
                pdfTable.AddCell(GetCell(GetCurrencyValue(amount), rowFont, Element.ALIGN_RIGHT, shadeRow));
                pdfTable.AddCell(GetCell(GetCurrencyValue(amountService), rowFont, Element.ALIGN_RIGHT, shadeRow));
            }

            if (!isVoided)
            {
                _CountNonVoided++;
                _TotalAmount += amount;
                _TotalAmountService += amountService;
            }
        }
    }
}