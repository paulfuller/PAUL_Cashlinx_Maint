using System;
using System.Data;
using System.Linq;
using Common.Controllers.Database;
using Reports;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Audit.Reports
{

    public class PreAuditReport : ReportBase

    {      
        public PreAuditReport(string title)
            : base(title, "", "Total Inventory Counted", Common.Libraries.Utility.Shared.PdfLauncher.Instance)
        {
            base.reportType = "Audit";
            base.showReportDate = false;

            heading aHeading = new heading()
            {
                    align = Element.ALIGN_LEFT, 
                    width = 70f, 
                    format = "{0}", 
                    showTotal = false, 
                    field_align = -1, 
                    header_align = -1,
                    Translator = null
            };

            aHeading.text = "Category #";
            aHeading.fieldName = "CAT_CODE";
            headers.Add(aHeading);

            aHeading.text = "Category Name";
            aHeading.fieldName = "CAT_DESC";
            aHeading.width = 250f;
            headers.Add(aHeading);

            aHeading.text = "Qty";
            aHeading.fieldName = "QTY";
            aHeading.width = 30f;
            aHeading.format = "{0:g}";
            aHeading.showTotal = true;
            aHeading.field_align = Element.ALIGN_RIGHT;
            aHeading.header_align = Element.ALIGN_CENTER;
            headers.Add(aHeading);

            aHeading.text = "Cost";
            aHeading.width = 70f;
            aHeading.format = "{0:n}";
            aHeading.fieldName = "COST";

            headers.Add(aHeading);
        }

        private DataTable CaccInfo;
        private DataTable categoryInfo;

        public bool CreateReport (DataSet ds)
        {

            DataTable resultTable = new DataTable();
            categoryInfo = ds.Tables[AuditReportsProcedures.PREAUDIT_CAT_SMRY];
            CaccInfo = ds.Tables[AuditReportsProcedures.PREAUDIT_CACC_SMRY];

            resultTable.Columns.Add("CAT_CODE");
            resultTable.Columns.Add("CAT_DESC");
            resultTable.Columns.Add("QTY", Type.GetType("System.Decimal"));
            resultTable.Columns.Add("COST", Type.GetType("System.Decimal"));

            var result = from DataRow r in categoryInfo.Rows
                         where r.Field<string>("ISJREFURB") == "N"
                         group r by new {code=r.Field<int>("CAT_CODE"), descr=r.Field<string>("CAT_DESC") } into grpd
                         select resultTable.Rows.Add(new Object[]
                         {   
                            string.Format("{0:d} - {1:d}", grpd.Key.code, grpd.Key.code + 999), 
                            grpd.Key.descr,
                            grpd.Sum(r => r.Field<decimal>("QTY")) , 
                            grpd.Sum(r => r.Field<decimal>("COST")) 
                         })
            ;

            result.Last();



            return base.CreateReport(resultTable);
        }


        protected override void PrintReportFooter(PdfPTable table)
        {
            // TODO: Ensure this works when no data is found

            base.PrintReportFooter(table);

            PdfPCell cell = new PdfPCell(new Phrase("Summary", base.ReportFontLargeBold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingTop = 20f;
            cell.PaddingLeft = -10f;
            cell.Colspan = headers.Count;
            table.AddCell(cell);


        // Total Found
            writeRow(table, "Total Found (Excluding CACC)", headers[2].total, headers[3].total);



        // Get summary totals
            var result = from DataRow r in categoryInfo.Rows
                         where r.Field<string>("HOLD_TYPE") == null 
                         group r by r.Field<string>("STATUS_CD") into grpd
                         select new
                         {   
                            status=grpd.Key,
                            ttl_qty=grpd.Sum(r => r.Field<decimal>("QTY")) , 
                            ttl_cost=grpd.Sum(r => r.Field<decimal>("COST")) 
                         }
            ;

            // CACC 
            decimal count = 0;
            decimal cost = 0;

            foreach (DataRow r in CaccInfo.Rows)
            {
                count += r.Field<Int32>("ORG_QTY");
                decimal thisCost = 0;
                if (decimal.TryParse(r.Field<string>("ORG_COST"), out thisCost))
                    cost += thisCost;
            }

            writeRow(table, "Total CACC Merchandise", count, cost);





         // Active PFI            
            var PFI_details = from r in result
                              where r.status == "PFI"
                              select r;

            count = 0;
            cost = 0;

            if (PFI_details.Count() > 0)
            {
                count = PFI_details.First().ttl_qty;
                cost = PFI_details.First().ttl_cost;
            }

            writeRow(table, "Total Active PFI excluding Holds", count, cost);




        // LAYAWAY Details
            var LAY_details = from r in result
                              where r.status == "LAY"
                              select r;



            if (LAY_details.Count() > 0)
            {
                count = LAY_details.First().ttl_qty;
                cost = LAY_details.First().ttl_cost;
            }
            else
            {
                count = 0;
                cost = 0;
            }

            writeRow(table, "Total Layaway excluding Holds", count, cost);



            // BUY Details
            var BUY_details = from r in result
                              where r.status == "PUR"
                              select r;



            if (BUY_details.Count() > 0)
            {
                count = BUY_details.First().ttl_qty;
                cost = BUY_details.First().ttl_cost;
            }
            else
            {
                count = 0;
                cost = 0;
            }

            writeRow(table, "Total Buy excluding Holds", count, cost);




        // Holds

           var holds = from DataRow r in categoryInfo.Rows
                       where r.Field<string>("HOLD_TYPE") != null
                       group r by r.Field<string>("HOLD_TYPE") into grpd
                         select new
                         {
                             holdType = grpd.Key,
                             ttl_qty = grpd.Sum(r => r.Field<decimal>("QTY")),
                             ttl_cost = grpd.Sum(r => r.Field<decimal>("COST"))
                         }
            ;


        // Police Holds
            var PolHolds = from r in holds
                           where r.holdType == "POLICEHOLD"
                           select r;

            if (PolHolds.Count() > 0)
            {
                count = PolHolds.First().ttl_qty;
                cost = PolHolds.First().ttl_cost;
            }
            else
            {
                count = 0;
                cost = 0;
            }

            writeRow(table, "Total Police Holds", count, cost);


         // Bankruptsy Holds
            var BnkrpsyHolds = from r in holds
                               where r.holdType == "BKHOLD"
                               select r;

            if (BnkrpsyHolds.Count() > 0)
            {
                count = BnkrpsyHolds.First().ttl_qty;
                cost = BnkrpsyHolds.First().ttl_cost;
            }
            else
            {
                count = 0;
                cost = 0;
            }

            writeRow(table, "Total Bankruptcy Holds", count, cost);


        // Referb

            var refurbs = from DataRow r in categoryInfo.Rows
                          where r.Field<string>("ISJREFURB") == "Y"
                          group r by r.Field<string>("ISJREFURB")
                          into grpd select new
                          {
                                  ttl_qty = grpd.Sum(r => r.Field<decimal>("QTY")), ttl_cost = grpd.Sum(r => r.Field<decimal>("COST"))
                          };



            if (refurbs.Count() > 0)
            {
                count = refurbs.First().ttl_qty;
                cost = refurbs.First().ttl_cost;
            }
            else
            {
                count = 0;
                cost = 0;
            }

            writeRow(table, "Total Merchandise in Jewelry Refurbish", count, cost);





        }


        private void writeRow(PdfPTable table, string title, decimal count, decimal cost)
        {
            PdfPCell cell;

            cell = new PdfPCell(new Phrase(title, base.ReportFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 2;
            table.AddCell(cell);



            cell = new PdfPCell(new Phrase(string.Format("{0:g}", count), base.ReportFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 1;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(string.Format("{0:n}", cost), base.ReportFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 1;
            table.AddCell(cell);
        }
    }
}
