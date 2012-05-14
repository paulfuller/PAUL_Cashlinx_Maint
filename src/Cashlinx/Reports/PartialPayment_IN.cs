using System;
using System.IO;
using Common.Controllers.Application;
using Common.Libraries.Utility.Logger;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;
namespace Reports
{
    public class PartialPayment_IN
    {

        private Font rptFont;
        private Font rptBoldFont;
        private Font rptLargeFont;
        private Document document;

        public IndianaPartialPaymentContext Context { get; private set; }

        public PartialPayment_IN(IndianaPartialPaymentContext dataContext)
        {
            Context = dataContext;

            //Font setup
            rptFont = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.NORMAL);
            rptBoldFont = FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD);
            rptLargeFont = FontFactory.GetFont("Tahoma", 10, iTextSharp.text.Font.BOLD);

        }

        public bool Print()
        {
            // Create a return value so there is only one way out of this function
            bool retval;
            try
            {
                if (string.IsNullOrWhiteSpace(Context.FilePath))
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "No file name is set for Indiana Partial Payment Receipt. Printing did not complete successfully!");
                    retval = false;
                }
                else
                {
                    document = new Document(PageSize.HALFLETTER.Rotate());
                    document.AddTitle("Partial Payment Receipt");
                    var writer = PdfWriter.GetInstance(document, new FileStream(Context.FilePath, FileMode.Create));
                    var MainTable = new PdfPTable(2) { TotalWidth = 1500f };

                    // Add ticket information
                    PdfPCell cell;
                    var blankCell = new PdfPCell(new Phrase("")) { Border = Rectangle.NO_BORDER };

                    // Insert spacer row
                    MainTable.AddCell(blankCell);
                    MainTable.AddCell(blankCell);

                    MainTable.AddCell(new PdfPCell(GenerateItemListTable()) { Border = Rectangle.NO_BORDER });
                    MainTable.AddCell(new PdfPCell(GenerateCostTable()) { Border = Rectangle.NO_BORDER });

                    MainTable.AddCell(new PdfPCell(GenerateCustomerInformationTable()) { Border = Rectangle.NO_BORDER });
                    MainTable.AddCell(blankCell);

                    document.Open();
                    document.Add(GenerateHeaderTable());

                    document.Add(MainTable);
                    document.Close();
                    retval = true;
                }

            }
            catch (Exception de)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Partial Payment Receipt printing {0}", de.Message);
                retval = false;

            }
            return retval;
        }

        private PdfPTable GenerateHeaderTable()
        {
            var headingtable = new PdfPTable(3);
            var cell = new PdfPCell();

            //Line 1
            cell = new PdfPCell(new Paragraph("Partial Payment Receipt", rptLargeFont))
                   {
                       Border = Rectangle.NO_BORDER,
                       HorizontalAlignment = Element.ALIGN_CENTER,
                       Colspan = 3
                   };
            headingtable.AddCell(cell);

            //separting line - line 2
            cell = new PdfPCell(new Phrase(""))
                   {
                       Colspan = 3,
                       Border = Rectangle.BOTTOM_BORDER,
                       HorizontalAlignment = Element.ALIGN_LEFT
                   };
            headingtable.AddCell(cell);


            //Line 3 (Date, Store Name, Ticket Number)
            //Context Date 
            //var dateTable = new PdfPTable(2) { WidthPercentage = 20, HorizontalAlignment = Element.ALIGN_LEFT };
            //dateTable.AddCell(new PdfPCell(new Phrase("Date: ", rptFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = Rectangle.NO_BORDER });
            //dateTable.AddCell(new PdfPCell(new Phrase(Context.PrintDateTime.ToString("MM/dd/yyyy"), rptFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = Rectangle.NO_BORDER });

            cell = new PdfPCell(new PdfPCell(new Phrase("Date: " + Context.PrintDateTime.ToString("MM/dd/yyyy"), rptFont)))
                   {
                       HorizontalAlignment = Element.ALIGN_LEFT,
                       Border = Rectangle.NO_BORDER,
                       Colspan = 1
                   };
            headingtable.AddCell(cell);

            // Store Name
            cell =
                new PdfPCell(
                    new Paragraph(
                        Context.StoreName + (string.IsNullOrEmpty(Context.StoreNumber) == false ? "# " + this.Context.StoreNumber : string.Empty), rptFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_TOP,
                    Colspan = 1,
                    NoWrap = true
                };
            headingtable.AddCell(cell);

            // Ticket Number
            cell = new PdfPCell(new Paragraph("Ticket No: " + Context.TicketNumber, rptFont))
                   {
                       HorizontalAlignment = Element.ALIGN_RIGHT,
                       Border = Rectangle.NO_BORDER,
                       VerticalAlignment = Element.ALIGN_TOP,
                       Colspan = 1
                   };

            headingtable.AddCell(cell);

            //Line 4 (Time, Store Street address, Employee Number
            // Time 
            //var timeTable = new PdfPTable(2);
            //timeTable.AddCell(new PdfPCell(new Paragraph("", rptFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = Rectangle.NO_BORDER });
            //timeTable.AddCell(new PdfPCell(new Paragraph(Context.PrintDateTime.ToString("HH:mm"), rptFont)) { HorizontalAlignment = Element.ALIGN_LEFT, Border = Rectangle.NO_BORDER });

            cell = new PdfPCell(new PdfPCell(new Phrase("Time: " + Context.PrintDateTime.ToString("HH:mm"),rptFont)))
                   {
                       HorizontalAlignment = Element.ALIGN_LEFT,
                       Border = Rectangle.NO_BORDER,
                       Colspan = 1
                   };
            headingtable.AddCell(cell);

            // Store street address information
            cell = new PdfPCell(new Paragraph(Context.StoreAddress, rptFont))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_TOP,
                Colspan = 1
            };
            headingtable.AddCell(cell);

            // Employee Number ( Should be coming from GlobalDataAccessor.Instance.DesktopSession.UserName)
            cell = new PdfPCell(new Paragraph("Emp No: " + Context.EmployeeNumber, rptFont))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Colspan = 1
            };
            headingtable.AddCell(cell);

            // Line 5 (Blank, City State for shop, blank)

            cell = new PdfPCell(new Paragraph(""))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Colspan = 1
            };
            headingtable.AddCell(cell);
            // City and state information
            cell = new PdfPCell(new Paragraph(Context.StoreCity + ", " + Context.StoreState + ' ' + Context.StoreZip, rptFont))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Colspan = 1
            };
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(""))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Colspan = 1
            };
            headingtable.AddCell(cell);

            return headingtable;
        }
        private PdfPTable GenerateCostTable()
        {
            var retval = new PdfPTable(2) { WidthPercentage = 0.50f, HorizontalAlignment = Element.ALIGN_RIGHT };

            retval.AddCell(new PdfPCell(new Phrase("Principal Reduction: ", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
            retval.AddCell(new PdfPCell(new Phrase(Context.PrincipalReduction.ToString("C"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            retval.AddCell(new PdfPCell(new Phrase("Interest: ", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
            retval.AddCell(new PdfPCell(new Phrase(Context.Interest.ToString("C"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            retval.AddCell(new PdfPCell(new Phrase("Service Fee: ", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
            retval.AddCell(new PdfPCell(new Phrase(Context.ServiceFees.ToString("C"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            retval.AddCell(new PdfPCell(new Phrase("Other Charges: ", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
            retval.AddCell(new PdfPCell(new Phrase(Context.OtherCharges.ToString("C"), rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            retval.AddCell(new PdfPCell(new Phrase("")) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
            retval.AddCell(new PdfPCell(new Phrase("------------", rptFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            retval.AddCell(new PdfPCell(new Phrase("Total Due From Customer: ", rptBoldFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
            retval.AddCell(new PdfPCell(new Phrase(Context.TotalPaymentAmount.ToString("C"), rptBoldFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            return retval;
        }
        private PdfPTable GenerateCustomerInformationTable()
        {
            var retval = new PdfPTable(1);
            retval.AddCell(new PdfPCell(new Phrase(Context.CustomerName, rptFont)) { Border = Rectangle.NO_BORDER });
            retval.AddCell(new PdfPCell(new Phrase(Context.CustomerAddress, rptFont)) { Border = Rectangle.NO_BORDER });
            retval.AddCell(new PdfPCell(new Phrase(Context.CustomerCity + ", " + Context.CustomerState + ' ' + Context.CustomerZip, rptFont)) { Border = Rectangle.NO_BORDER });
            retval.AddCell(new PdfPCell(new Phrase(Context.CustomerPhone, rptFont)) { Border = Rectangle.NO_BORDER });

            return retval;
        }
        private PdfPTable GenerateItemListTable()
        {
            var retval = new PdfPTable(1);

            var cell = new PdfPCell();

            //Line 1
            cell = new PdfPCell(new Paragraph(Context.ItemDescription, rptFont))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_LEFT
            };
            retval.AddCell(cell);

            return retval;
        }
    }
}