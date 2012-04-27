using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Common.Controllers.Application;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;

namespace Reports
{
    public class PartialPaymentReceipt
    {

        public ReportObject RptObject;
        public List<PartialPaymentTicketData> PartialPaymentTktData;
        public PartialPaymentReportData PartialPaymentRptData;
        public static string ReportTitle;

        private Font rptFont;
        private Font rptBoldFont;
        private Font rptLargeFont;
        private Document document;
        private PdfWriter writer;




        public bool Print()
        {
            DateTime lastDayGrace=DateTime.MaxValue;
            rptLargeFont = FontFactory.GetFont("Tahoma", 10, iTextSharp.text.Font.BOLD);
            ReportTitle = RptObject.ReportTitle;
            try
            {
                document = new Document(PageSize.HALFLETTER.Rotate());
                //Set Font for the report
                rptFont = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL);
                rptBoldFont = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD);

                string rptFileName = RptObject.ReportTempFileFullName;
                if (!string.IsNullOrEmpty(rptFileName))
                {
                    document.AddTitle(RptObject.ReportTitle);
                    writer = PdfWriter.GetInstance(document, new FileStream(rptFileName, FileMode.Create));
                    var table = new PdfPTable(3);
                    document.SetPageSize(PageSize.HALFLETTER.Rotate());
                    document.SetMargins(0, 00, 10, 45);

                    table.TotalWidth = 1000f;

                    //Insert the report header
                    InsertReportHeader(table);
                    foreach (PartialPaymentTicketData pmtTktData in PartialPaymentTktData)
                    {

                        var newtable = new PdfPTable(2);
                        
                        newtable.TotalWidth = 500f;
                        PdfPCell cell = new PdfPCell(new Paragraph("Ticket No :" + pmtTktData.TicketNumber, rptFont));
                        cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 2;
                        newtable.AddCell(cell);

                        int i = 1;
                        foreach (string itemData in pmtTktData.LoanItems)
                        {
                            cell = new PdfPCell(new Paragraph("[" + i + "]" + " " + itemData, rptFont));
                            cell.Border = Rectangle.NO_BORDER;
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.Colspan = 2;
                            newtable.AddCell(cell);
                            i++;
                        }

                        var nestFirstTable = new PdfPCell(newtable)
                        {
                            Colspan = 2,
                            BorderWidth=0
                        };
                        table.AddCell(nestFirstTable);

                        newtable = new PdfPTable(2);
                        newtable.WidthPercentage = 100;
                        newtable.TotalWidth = 500f;
                        cell = new PdfPCell(new Paragraph("Principal Reduction:", rptFont));
                        cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 1;
                        newtable.AddCell(cell);

                        cell = new PdfPCell(new Paragraph(string.Format("{0:C}", pmtTktData.PrincipalReduction), rptFont));
                        cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell.Colspan = 1;
                        newtable.AddCell(cell);



                        cell = new PdfPCell(new Paragraph("Interest:", rptFont));
                        cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 1;
                        newtable.AddCell(cell);

                        cell = new PdfPCell(new Paragraph(string.Format("{0:C}", pmtTktData.InterestAmount), rptFont));
                        cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell.Colspan = 1;
                        newtable.AddCell(cell);


                        cell = new PdfPCell(new Paragraph("Processing Fee:", rptFont));
                        cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 1;
                        newtable.AddCell(cell);

                        cell = new PdfPCell(new Paragraph(string.Format("{0:C}", pmtTktData.StorageFee), rptFont));
                        cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell.Colspan = 1;
                        newtable.AddCell(cell);


                        cell = new PdfPCell(new Paragraph("Other Charges:", rptFont));
                        cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 1;
                        newtable.AddCell(cell);


                        cell = new PdfPCell(new Paragraph(string.Format("{0:C}", pmtTktData.OtherCharges), rptFont));
                        cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell.Colspan = 1;
                        newtable.AddCell(cell);

                        cell = new PdfPCell(new Paragraph("Total Payment Amount:", rptBoldFont));
                        cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Colspan = 1;
                        newtable.AddCell(cell);

                        cell = new PdfPCell(new Paragraph(string.Format("{0:C}", PartialPaymentRptData.TotalDueFromCustomer), rptBoldFont));
                        cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        cell.Colspan = 1;
                        newtable.AddCell(cell);


                        var nestSecondTable = new PdfPCell(newtable)
                        {
                            Colspan = 2,
                            BorderWidth=0
                        };
                        table.AddCell(nestSecondTable);
                        lastDayGrace=pmtTktData.PFIDate;


                    }




                    PdfPCell cell1 = new PdfPCell(new Paragraph("Last Day of Grace Remains :" + lastDayGrace.ToShortDateString(), rptLargeFont));
                    cell1.Border = Rectangle.NO_BORDER;
                    cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell1.Colspan = 3;
                    table.AddCell(cell1);


                    cell1 = new PdfPCell(new Paragraph("*Partial Payment does not change last day of grace! :", rptFont));
                    cell1.Border = Rectangle.NO_BORDER;
                    cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell1.Colspan = 3;
                    table.AddCell(cell1);


                    cell1 = new PdfPCell(new Paragraph(PartialPaymentRptData.CustomerName, rptFont));
                    cell1.Border = Rectangle.NO_BORDER;
                    cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell1.Colspan = 3;
                    table.AddCell(cell1);

                    cell1 = new PdfPCell(new Paragraph(PartialPaymentRptData.CustomerAddress1, rptFont));
                    cell1.Border = Rectangle.NO_BORDER;
                    cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell1.Colspan = 3;
                    table.AddCell(cell1);


                    cell1 = new PdfPCell(new Paragraph(PartialPaymentRptData.CustomerAddress2, rptFont));
                    cell1.Border = Rectangle.NO_BORDER;
                    cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell1.Colspan = 3;
                    table.AddCell(cell1);

                    //Separating line

                    cell1 = new PdfPCell(new Phrase(""));
                    cell1.Colspan = 3;
                    cell1.Border = Rectangle.BOTTOM_BORDER;
                    cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell1);




                    document.Open();
                    document.Add(table);
                    document.Close();
                    return true;
                }
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Partial Payment Receipt printing failed since file name is not set");
                return false;
            }
            catch (Exception de)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Partial Payment Receipt printing {0}", de.Message);
                return false;

            }

        }


        private void InsertReportHeader(PdfPTable headingtable)
        {

            PdfPCell cell = new PdfPCell();


            //Line 1
            cell = new PdfPCell(new Paragraph(RptObject.ReportTitle, rptLargeFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            //separting line - line 2
            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 3;
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            headingtable.AddCell(cell);


            //Line 3
            cell = new PdfPCell(new Paragraph("Date: " + ShopDateTime.Instance.ShopDate.ToShortDateString(), rptFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 1;
            headingtable.AddCell(cell);


            cell = new PdfPCell(new Paragraph(RptObject.ReportStore, rptFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            //Line 4
            cell = new PdfPCell(new Paragraph("Time: " + DateTime.Now.ToShortTimeString(), rptFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 1;
            headingtable.AddCell(cell);


            cell = new PdfPCell(new Paragraph(RptObject.ReportStoreDesc, rptFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            headingtable.AddCell(cell);


            cell = new PdfPCell(new Paragraph("Emp No: " + PartialPaymentRptData.EmployeeName, rptFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            //empty line
            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 3;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            headingtable.AddCell(cell);

        }


    }
}
