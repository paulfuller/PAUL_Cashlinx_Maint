using System;
using System.Data;
using Common.Libraries.Objects;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reports
{
    class DailySalesReport_Summary : ReportBase
    {
        public DailySalesReport_Summary(IPdfLauncher pdfLauncher)
            : base("Daily Sales Employee Summary", "", "Totals:", pdfLauncher)
        {

            // TODO: add sales header for criteria, and gross profit to totals; total format??
            //

            showReportDate = false;
            reportPageSize = PageSize.LETTER;

            GroupByFooterTitle = "Total for User ID: {0}";
            showGroupCount = false;

            heading aHeading = new heading()
                               {
                                   align = Element.ALIGN_LEFT,
                                   width = 70f,
                                   format = "{0}",
                                   showTotal = false,
                                   field_align = Element.ALIGN_RIGHT,
                                   header_align = Element.ALIGN_RIGHT,
                                   Translator = null
                               };

            aHeading.text = "User ID";
            aHeading.fieldName = "USERID";
            headers.Add(aHeading);

            aHeading.text = "Total Cost";
            aHeading.fieldName = "TOTAL_COST";
            aHeading.showTotal = true;
            aHeading.format = "{0:F2}";
            headers.Add(aHeading);

            aHeading.text = "Total Retail";
            aHeading.fieldName = "TOTAL_RETAIL";
            headers.Add(aHeading);

            aHeading.text = "Gross Profit";
            aHeading.fieldName = "GROSS_PROFIT";
            aHeading.format = "{0:p0}";
            headers.Add(aHeading);

            aHeading.text = " R/S % Variance";
            aHeading.fieldName = "AVG_VARNC_PCT";
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
        protected override void PrintReportHeader(PdfPTable table, Image gif, bool endHeader)
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

            PdfPCell cell = new PdfPCell(new Paragraph(reportObject.ReportTitle));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = -10;
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
            cell.PaddingTop = -8;
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
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = -10;
            cell.PaddingBottom = 20;
            cell.Colspan = headers.Count;
            table.AddCell(cell);
        }

        protected override void PrintReportFooter(PdfPTable table)
        {
            return;
            //float total_cost = 0;
            //float total_retail = 0;

            //string value = String.Format(_totalLabel);
            //PdfPCell cell = new PdfPCell(new Phrase(value, _reportFont));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = Rectangle.TOP_BORDER;
            //cell.PaddingTop = 10f;

            //table.AddCell(cell);

            //for (int i = 1; i < headers.Count; i++)
            //{
            //    heading h = headers[i];

            //    value = String.Empty;

            //    if (h.showTotal)
            //    {
            //        value = String.Format(h.format, h.total);
            //    }


            //    if (h.fieldName.Equals("TOTAL_COST"))
            //        total_cost = float.Parse(value);

            //    if (h.fieldName.Equals("TOTAL_RETAIL"))
            //        total_retail = float.Parse(value);

            //    if (h.fieldName.Equals("GROSS_PROFIT"))
            //        break;
                
            //    cell = new PdfPCell(new Phrase(value, _reportFont));
            //    cell.HorizontalAlignment = h.field_align;
            //    cell.Border = Rectangle.TOP_BORDER;
            //    cell.PaddingTop = 10f;

            //    table.AddCell(cell);
            //}

            //value = string.Format("{0:p0}", (total_retail - total_cost) / total_retail);

            //cell = new PdfPCell(new Phrase(value, _reportFont));
            //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.Border = Rectangle.TOP_BORDER;
            //cell.PaddingTop = 10f;

            //table.AddCell(cell);

            //decimal ttl_variance = 0;
            //decimal ttl_org_price = 0;

            //foreach (DataRow r in this.reportObject.DailySalesSummaryData.Rows)
            //{
            //    ttl_variance += r.Field<decimal>("TTL_VARIANCE");
            //    ttl_org_price += r.Field<decimal>("TTL_ORG_PRICE");
            //}

            //if (ttl_org_price > 0)
            //    value = string.Format("{0:p0}", Math.Abs(ttl_variance / ttl_org_price));

            //else
            //    value = "100 %";

            //cell = new PdfPCell(new Phrase(value, _reportFont));
            //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.Border = Rectangle.TOP_BORDER;
            //cell.PaddingTop = 10f;

            //table.AddCell(cell);
         
        }

    }
}
