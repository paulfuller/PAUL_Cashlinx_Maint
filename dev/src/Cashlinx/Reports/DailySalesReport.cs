using System;
using Common.Libraries.Objects;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports
{
    class DailySalesReport : ReportBase
    {
        public DailySalesReport(IPdfLauncher pdfLauncher)
            : base("Daily Sales Report", "", "Totals:", pdfLauncher)
        {

            // TODO: add sales header for criteria, and gross profit to totals; total format??
            //

            showReportDate = false;
            reportPageSize = PageSize.LEGAL.Rotate();

            GroupByFooterTitle = "Total for User ID: {0}";
            showGroupCount = false;

            heading aHeading = new heading()
                               {
                                   align = Element.ALIGN_LEFT,
                                   width = 30f,
                                   format = "{0}",
                                   showTotal = false,
                                   field_align = Element.ALIGN_CENTER,
                                   header_align = Element.ALIGN_CENTER,
                                   Translator = null
                               };

            aHeading.text = "User ID";
            aHeading.fieldName = "USERID";
            aHeading.groupBy = true;
            headers.Add(aHeading);

            aHeading.text = "Date";
            aHeading.fieldName = "REF_DATE";
            aHeading.width = 50f;
            aHeading.groupBy = false;
            aHeading.field_align = Element.ALIGN_RIGHT;
            headers.Add(aHeading);

            aHeading.text = "PFI Date";
            aHeading.fieldName = "PFI_DATE";
            headers.Add(aHeading);

            aHeading.text = "ICN";
            aHeading.fieldName = "ICN";
            aHeading.field_align = Element.ALIGN_CENTER;
            aHeading.width = 100f;
            headers.Add(aHeading);

            aHeading.text = "Merchandise Description";
            aHeading.fieldName = "MD_DESC";
            aHeading.field_align = Element.ALIGN_LEFT;
            aHeading.align = Element.ALIGN_LEFT;
            aHeading.width = 370f;
            headers.Add(aHeading);


            aHeading.text = "Type";
            aHeading.fieldName = "DISP_TYPE";
            aHeading.align = Element.ALIGN_CENTER;
            aHeading.field_align = Element.ALIGN_CENTER;
            aHeading.width = 40f;
            headers.Add(aHeading);

            aHeading.text = "Transaction\nNumber";
            aHeading.fieldName = "DISP_DOC";
            aHeading.width = 45f;
            headers.Add(aHeading);

            aHeading.text = "Cost";
            aHeading.fieldName = "ITEM_AMT";
            aHeading.align = Element.ALIGN_CENTER;
            aHeading.field_align = Element.ALIGN_RIGHT;
            aHeading.width = 40f;
            aHeading.format = "{0:F2}";
            aHeading.showTotal = true;
            headers.Add(aHeading);

            aHeading.text = "Retail\nAmount";
            aHeading.fieldName = "ORIGINAL_PRICE";
            headers.Add(aHeading);

            aHeading.text = "Sale\nAmount";
            aHeading.fieldName = "SOLD_FOR_PRICE";
            headers.Add(aHeading);

            aHeading.text = "Absolute Total $ Variance";
            aHeading.fieldName = "VARNC";
            aHeading.align = Element.ALIGN_RIGHT;
            headers.Add(aHeading);

            aHeading.text = "R/S % Variance"; 
            aHeading.fieldName = "VARNC_PCT";
            aHeading.format = "{0:F0} %";
            headers.Add(aHeading);
        }

        private decimal convertParam(int i)
        {
            decimal retval = 0M;

            if (reportObject.ReportParms[i].ToString().Length > 0)
            {
                decimal.TryParse(reportObject.ReportParms[i].ToString(),
                    out retval);
            }

            return retval;
        }


// ReSharper disable OptionalParameterHierarchyMismatch
        protected override void PrintReportHeader(PdfPTable table, Image gif, bool showTitle) 
// ReSharper restore OptionalParameterHierarchyMismatch
        {

            base.PrintReportHeader(table, gif, false);

            decimal[] salesRange = new decimal[2] { 
                convertParam(2), 
                convertParam(3)
            };

            decimal[] varianceRange = new decimal[2] { 
                convertParam(4), 
                convertParam(5)
            };

            PdfPTable hdrTable = new PdfPTable(3);
            PdfPTable tmpTable = new PdfPTable(2);

            PdfPCell cell;

            cell = new PdfPCell(new Paragraph(reportObject.ReportTitle));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = -10;
            cell.PaddingBottom = 20;
            cell.Colspan = 2;
            tmpTable.AddCell(cell);   

            cell = new PdfPCell(new Paragraph("Sales Range:", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(String.Format("{0:f2} to {1:f2}", salesRange[0], salesRange[1]), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);


            cell = new PdfPCell(new Paragraph("Variance Range:", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(String.Format("{0:f2} to {1:f2}", varianceRange[0], varianceRange[1]), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);



            cell = new PdfPCell(new Paragraph("Sort By:", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(reportObject.ReportSort, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);


            cell = new PdfPCell(new Paragraph("Option:", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(reportObject.ReportParms[6].ToString(), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);


            cell = new PdfPCell(new Paragraph(String.Format("Reporting: {0:d} to {1:d}",
                    reportObject.ReportParms[0], reportObject.ReportParms[1]
                ), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingTop = -4;
            cell.Colspan = 1;
            hdrTable.AddCell(cell);


            cell = new PdfPCell(tmpTable);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = 10;
            cell.PaddingTop = -14;
            hdrTable.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            hdrTable.AddCell(cell);

            cell = new PdfPCell(hdrTable);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_MIDDLE;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = -10;
            cell.PaddingBottom = 20;
            cell.Colspan = headers.Count;
            table.AddCell(cell);
        }

        protected override void printGroupFooter(PdfPTable table, string lastGroup, int group_count)
        {
            string groupFooter;

            if (showGroupCount)
                groupFooter = String.Format(GroupByFooterTitle + "\t{1}", lastGroup, group_count);
            else
                groupFooter = String.Format(GroupByFooterTitle, lastGroup);


            PdfPCell cell = new PdfPCell(new Phrase(groupFooter, _reportFont));
            cell.Border = Rectangle.TOP_BORDER;
            cell.Colspan = 2;
            cell.PaddingBottom = 13f;
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = Rectangle.TOP_BORDER;
            cell.Colspan = 2;
            cell.PaddingBottom = 13f;
            table.AddCell(cell);

            decimal costAmt = headers[7].groupTotal;
            decimal retailAmt = headers[8].groupTotal;
            decimal saleAmt = headers[9].groupTotal;

            cell = new PdfPCell(new Phrase(string.Format( "Gross Profit: {0:p0}", (saleAmt-costAmt)/saleAmt), _reportFont));
            cell.Border = Rectangle.TOP_BORDER;
            cell.Colspan = 3;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.PaddingLeft = -20f;
            cell.PaddingBottom = 13f;
            table.AddCell(cell);



            decimal variance = 0;

            for (int i = 1; i < headers.Count; i++)
            {
                heading h = headers[i];

                string value = String.Empty;

                if (h.fieldName.Equals("VARNC_PCT"))
                {
                    h.groupTotal = 0;
                    break;
                }

                if (h.fieldName.Equals("VARNC"))
                    variance = h.groupTotal;

                if (h.showTotal)
                {
                    value = String.Format(h.format, h.groupTotal);
                    h.groupTotal = 0;

                    headers[i] = h;

                    cell = new PdfPCell(new Phrase(value, _reportFont));
                    cell.HorizontalAlignment = h.field_align;
                    cell.Border = Rectangle.TOP_BORDER;
                    cell.PaddingBottom = 13f;

                    table.AddCell(cell);
                }


            }


            decimal varncPct = 1;

            if (retailAmt != 0)
            {
                varncPct = Math.Abs(retailAmt - saleAmt) / retailAmt;

                // if we want to use an absolute amount, then use the following formula instead
                //varncPct = variance / retailAmt;
            }
            cell = new PdfPCell(new Phrase(string.Format("{0:p0}", varncPct), _reportFont));
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.TOP_BORDER;
            cell.PaddingBottom = 13f;

            table.AddCell(cell);

        }

        protected override void PrintReportFooter(PdfPTable table)
        {
            return;
 
        }
    }
}
