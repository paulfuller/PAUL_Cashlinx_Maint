using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports.DSTR
{
    public class Group30_PoliceReturns : AbstractDSTRGroup
    {
        public Group30_PoliceReturns(DSTRReportContext dstrContext, string dataTableName)
        : base(dstrContext, dataTableName)
        {
        }

        protected override void OnBuildSection()
        {
            try
            {
                if (GroupData.Rows.Count < 1)
                    return;

                PdfPTable dataTable = new PdfPTable(9);

                _CellSection.Phrase = new Phrase("Police Returns", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.BOLD));
                _CellSection.Colspan = 9;
                dataTable.AddCell(_CellSection);

                Font fontRow = new Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.NORMAL));
                Font fontHeader = new Font(FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.BOLD));

                _CellColumnRecordHeader.Phrase = new Phrase("CSR #", fontHeader);
                _CellColumnRecordHeader.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                dataTable.AddCell(_CellColumnRecordHeader);

                _CellColumnRecordHeader.Phrase = new Phrase("Loan or Buy #", fontHeader);
                dataTable.AddCell(_CellColumnRecordHeader);

                _CellColumnRecordHeader.Phrase = new Phrase("ICN", fontHeader);
                dataTable.AddCell(_CellColumnRecordHeader);

                _CellColumnRecordHeader.Phrase = new Phrase("Item Description", fontHeader);
                dataTable.AddCell(_CellColumnRecordHeader);

                _CellColumnRecordHeader.Phrase = new Phrase("Item Status Loan Status", fontHeader);
                dataTable.AddCell(_CellColumnRecordHeader);

                _CellColumnRecordHeader.Phrase = new Phrase("Loan Status", fontHeader);
                dataTable.AddCell(_CellColumnRecordHeader);

                _CellColumnRecordHeader.Phrase = new Phrase("Customer", fontHeader);
                dataTable.AddCell(_CellColumnRecordHeader);

                _CellColumnRecordHeader.Phrase = new Phrase("Time", fontHeader);
                dataTable.AddCell(_CellColumnRecordHeader);

                _CellColumnRecordHeader.Phrase = new Phrase("Agency", fontHeader);
                dataTable.AddCell(_CellColumnRecordHeader);

                dataTable.HeaderRows = 2;

                int iNonVoided = 0;
                //foreach (DataRow dataRow in Data.Rows)
                //{
                //    iRecords++;

                //    if (iRecords % 2 == 1)
                //        _CellColumnRecord.GrayFill = _CellNormal;
                //    else
                //        _CellColumnRecord.GrayFill = _CellAlternating;

                //    _CellColumnRecord.Phrase = new Phrase(GetStringValue(dataRow["ENT_ID"]), fontRow);
                //    _CellColumnRecord.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                //    dataTable.AddCell(_CellColumnRecord);

                //    _CellColumnRecord.Phrase = new Phrase(GetStringValue(dataRow["TICKET_NUMBER"]), fontRow);
                //    dataTable.AddCell(_CellColumnRecord);

                //    Paragraph paragraph = new Paragraph();
                //    paragraph.Add(new Phrase(string.Concat(GetStringValue(dataRow["ICN_NUM"]).Substring(0, 5), " ", GetStringValue(dataRow["ICN_NUM"]).Substring(5, 1), " "), fontRow));
                //    paragraph.Add(new Phrase(GetStringValue(dataRow["ICN_NUM"]).Substring(6, 6) + " ", fontHeader));
                //    paragraph.Add(new Phrase(GetStringValue(dataRow["ICN_NUM"]).Substring(12, 1) + " ", fontRow));
                //    paragraph.Add(new Phrase(GetStringValue(dataRow["ICN_NUM"]).Substring(13), fontHeader));

                //    _CellColumnRecord.Phrase = new Phrase(paragraph);
                //    _CellColumnRecord.HorizontalAlignment = Element.ALIGN_LEFT;
                //    dataTable.AddCell(_CellColumnRecord);

                //    _CellColumnRecord.Phrase = new Phrase(GetStringValue(dataRow["ITEM_DESC"]), fontRow);
                //    dataTable.AddCell(_CellColumnRecord);

                //    _CellColumnRecord.Phrase = new Phrase(GetStringValue(dataRow["ITEM_DESC"]), fontRow);
                //    dataTable.AddCell(_CellColumnRecord);

                //    _CellColumnRecord.Phrase = new Phrase(GetStringValue(dataRow["ITEM_STATUS"]), fontRow);
                //    dataTable.AddCell(_CellColumnRecord);

                //    _CellColumnRecord.Phrase = new Phrase(GetStringValue(dataRow["NAME"]), fontRow);
                //    dataTable.AddCell(_CellColumnRecord);

                //    _CellColumnRecord.Phrase = new Phrase("TIME", fontRow);
                //    dataTable.AddCell(_CellColumnRecord);

                //    _CellColumnRecord.Phrase = new Phrase(GetStringValue(dataRow["AGENCY"]), fontRow);
                //    dataTable.AddCell(_CellColumnRecord);

                //    if (GetStringValue(dataRow["STATUS_CD"]) != "VO")
                //    {
                //        iNonVoided++;
                //    }
                //}

                _CellFooterRecord.Phrase = new Phrase("Total", fontRow);
                _CellFooterRecord.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                _CellFooterRecord.Colspan = 1;
                dataTable.AddCell(_CellFooterRecord);

                _CellFooterRecord.Phrase = new Phrase(iNonVoided.ToString(), fontRow);
                _CellFooterRecord.Colspan = 7;
                dataTable.AddCell(_CellFooterRecord);

                PdfPCell cellTable = new PdfPCell();
                cellTable.Border = Rectangle.NO_BORDER;
                cellTable.HorizontalAlignment = Element.ALIGN_LEFT;
                cellTable.VerticalAlignment = Element.ALIGN_BOTTOM;
                cellTable.Table = dataTable;

                PdfTable.AddCell(cellTable);
            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
                ErrorCode = "1";
                throw;
            }
            return;
        }
    }
}