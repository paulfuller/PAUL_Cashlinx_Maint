using System;
using System.IO;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Utilities = Common.Libraries.Utility.Utilities;
using Common.Libraries.Utility.Logger;
namespace Reports.Layaway
{
    public class LayawayContractReport : ReportBase
    {
        //used by overriden methods for footers
        #region Fields
        private PdfContentByte contentByte;
        private PdfTemplate template;

        private PdfContentByte contentByteFooter;
        private PdfTemplate templateFooter;
        private BaseFont footerBaseFont = null;
        //used by report
        /*private Font ReportFont;
        private Font ReportFontLargeBold;
        private Font ReportFontBold;
        private Font ReportFontHeading;
        private Font ReportFontUnderlined;*/

        //main objects
        public LayawayRunReports runReport;

        private decimal _saleSubTotal = 0.0m;

        #endregion

        public LayawayContractReport(IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {

        }

        #region Private Methods
        private void WriteFooter(PdfPTable footerTable, LayawayContractReport layawayContractRpt)
        {
            PdfPCell cell = new PdfPCell();

            cell = new PdfPCell(new Paragraph(string.Empty, ReportFont));
            cell.Colspan = 7;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.VerticalAlignment = Element.ALIGN_BOTTOM;
            footerTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, ReportFont));
            cell.Colspan = 7;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.VerticalAlignment = Element.ALIGN_BOTTOM;
            footerTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, ReportFont));
            cell.Colspan = 7;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.VerticalAlignment = Element.ALIGN_BOTTOM;
            footerTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, ReportFont));
            cell.Colspan = 7;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.VerticalAlignment = Element.ALIGN_BOTTOM;
            footerTable.AddCell(cell);

            //line 1
            cell = new PdfPCell(new Paragraph(string.Empty, ReportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.VerticalAlignment = Element.ALIGN_BOTTOM;
            footerTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Customer Signature:_______________________", ReportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.VerticalAlignment = Element.ALIGN_BOTTOM;
            footerTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Date:___________________", ReportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.VerticalAlignment = Element.ALIGN_BOTTOM;
            footerTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Emp Signature:_____________________", ReportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.VerticalAlignment = Element.ALIGN_BOTTOM;
            footerTable.AddCell(cell);

            //line2
            cell = new PdfPCell(new Paragraph(string.Empty, ReportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.VerticalAlignment = Element.ALIGN_BOTTOM;
            footerTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Customer Name: " + layawayContractRpt.ReportObject.CustomerName, ReportFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.VerticalAlignment = Element.ALIGN_BOTTOM;
            footerTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Contact Number: " + layawayContractRpt.ReportObject.ContactNumber, ReportFont));
            cell.Colspan = 4;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.VerticalAlignment = Element.ALIGN_BOTTOM;
            footerTable.AddCell(cell);
        }

        private void AddLines(int lineCount, PdfPTable table, bool line, string stringline, bool endofreport, Font font)
        {
            for (int i = 0; i < lineCount; i++)
            {
                runReport.ReportLines(table, line, stringline, endofreport, font);
            }
        }

        private void GetDates(PdfPTable agreementTable)
        {
            string dates = string.Empty;
            int counter = 0;
            //issue with Nulls in reportObject.CurrentLayaway.Payments. When result, the first condition needs to go away
            if (ReportObject.CurrentLayaway.Payments == null)
            {
                dates = ReportObject.CurrentLayaway.NextPayment.ToShortDateString();
            }
            else
            {
                foreach (var layawayPayment in ReportObject.CurrentLayaway.Payments)
                {
                    counter++;
                    if (!string.IsNullOrEmpty(dates))
                        dates = string.Format("{0}, {1}", dates, Convert.ToDateTime(layawayPayment.DueDate).ToShortDateString());
                    else
                        dates = Convert.ToDateTime(layawayPayment.DueDate).ToShortDateString();
                    if (counter == 12)
                    {
                        AgreementLines(agreementTable, dates, ReportFontLargeBold);
                        counter = 0;
                        dates = string.Empty;
                    }
                }
            }
            if (!string.IsNullOrEmpty(dates))
                AgreementLines(agreementTable, dates, ReportFontLargeBold);
        }

        private void AgreementLines(PdfPTable agreementTable, string agreementText, Font reportFontForPayments)
        {
            PdfPCell cell = new PdfPCell();

            //line 1
            cell = new PdfPCell(new Paragraph(string.Empty, reportFontForPayments));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            agreementTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(agreementText, reportFontForPayments));
            cell.Colspan = 6;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            agreementTable.AddCell(cell);
        }

        private void WriteAgreement(PdfPTable agreementTable, LayawayContractReport layawayContractRpt)
        {
            string agreementLine1 = "I understand that my first payment of at least " + layawayContractRpt.ReportObject.CurrentLayaway.MonthlyPayment.ToString("C") + " is due on or before " + layawayContractRpt.ReportObject.CurrentLayaway.NextPayment.ToShortDateString() + " and also agree to pay “MONTHLY ";
            string agreementLine2 = "PAYMENT(S)” of at least " + layawayContractRpt.ReportObject.CurrentLayaway.MonthlyPayment.ToString("C") + " on or before each of the following dates: ";
            string agreementLine4 = "IF I FAIL TO MAKE ANY OF THE “MONTHLY PAYMENT(S)” ON OR BEFORE THE ABOVE DATES I RELINQUISH ALL RIGHTS ";
            string agreementLine5 = "TO THE DESCRIBED ITEM(S) AND I WILL BE ENTITLED TO A CREDIT IN THE AMOUNT OF MONEY I HAVE PAID";
            string agreementLine6 = "(OTHER THAN THE SERVICE FEE) LESS A $10.00 RESTOCKING FEE. THE CREDIT CAN BE USED ONLY BY ME FOR ";
            string agreementLine7 = "A CASH PURCHASE OR LAYAWAY PAYMENT IN THIS STORE FOR A PERIOD OF 6 MONTHS FROM THE DATE OF THE LAST PAYMENT ";
            string agreementLine8 = "MADE ON THIS LAYAWAY AND CANNOT BE USED TO PAY ON A LOAN.";
            string agreementLine9 = "I understand that title to the item(s) will not transfer until I have paid this layaway in full and that there will be no";
            string agreementLine10 = "refunds or exchanges.";

            AgreementLines(agreementTable, agreementLine1, ReportFont);
            AgreementLines(agreementTable, agreementLine2, ReportFont);
            GetDates(agreementTable);
            AgreementLines(agreementTable, agreementLine4, ReportFont);
            AgreementLines(agreementTable, agreementLine5, ReportFont);
            AgreementLines(agreementTable, agreementLine6, ReportFont);
            AgreementLines(agreementTable, agreementLine7, ReportFont);
            AgreementLines(agreementTable, agreementLine8, ReportFont);
            AgreementLines(agreementTable, agreementLine9, ReportFont);
            AgreementLines(agreementTable, agreementLine10, ReportFont);
        }

        private void WriteSingleLine(PdfPTable singleLinesTable, int lineCharacterCount)
        {
            PdfPCell cell = new PdfPCell();
            cell = new PdfPCell(new Phrase(StringUtilities.fillString("_", lineCharacterCount), ReportFont));
            cell.Colspan = 7;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            singleLinesTable.AddCell(cell);
        }

        private void ReportSummary(PdfPTable summaryTable)
        {
            decimal _serviceFee = 0.0m;
            decimal _salesTax = 0.0m;
            decimal _layawayTotal = 0.0m;
            decimal _downPayment = 0.0m;
            decimal _balanceOwed = 0.0m;
            decimal _monthlyPayment = 0.0m;
            decimal taxRate = 0.0m;
            
            int numberOfPayments = 0;
            _serviceFee = ReportObject.CurrentLayaway.GetLayawayFees();

            _saleSubTotal -= ReportObject.CurrentLayaway.CouponAmount;

            if (ReportObject.CurrentLayaway.SalesTaxPercentage > 0.0m)
                taxRate = ReportObject.CurrentLayaway.SalesTaxPercentage;
            _salesTax = ReportObject.CurrentLayaway.SalesTaxAmount; // .taxRate / 100 * _saleSubTotal;
            _layawayTotal = _salesTax  + _saleSubTotal; // + _serviceFee
            _downPayment = ReportObject.CurrentLayaway.DownPayment;
            _balanceOwed = _layawayTotal - _downPayment;
            _monthlyPayment = ReportObject.CurrentLayaway.MonthlyPayment;
            var nextPaymentDue = ReportObject.CurrentLayaway.NextPayment;
            numberOfPayments = ReportObject.CurrentLayaway.NumberOfPayments;

            PdfPCell cell = new PdfPCell();

            //service fee
            cell = new PdfPCell(new Paragraph("Service Fee(Non-refundable)", ReportFontLargeBold));
            cell.Colspan = 6;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(_serviceFee.ToString("C"), ReportFontLargeBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            //Sale Sub-Total
            cell = new PdfPCell(new Paragraph("Sub-Total", ReportFont));
            cell.Colspan = 6;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(Utilities.GetDecimalValue(_saleSubTotal, 0).ToString("C"), ReportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            //Sales tax
            cell = new PdfPCell(new Paragraph("Estimated Sales Tax", ReportFont));
            cell.Colspan = 6;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(Utilities.GetDecimalValue(_salesTax, 0).ToString("C"), ReportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            //Layaway Total
            cell = new PdfPCell(new Paragraph("Layaway Total", ReportFont));
            cell.Colspan = 6;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(Utilities.GetDecimalValue(_layawayTotal, 0).ToString("C"), ReportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            //add blank line here 
            runReport.ReportLines(summaryTable, false, " ", false, ReportFont);

            //Down Payment
            cell = new PdfPCell(new Paragraph("Down Payment", ReportFont));
            cell.Colspan = 6;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(_downPayment.ToString("C"), ReportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            //Balance owed
            cell = new PdfPCell(new Paragraph("Balance Owed", ReportFont));
            cell.Colspan = 6;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(_balanceOwed.ToString("C"), ReportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            //Monthly Payment
            cell = new PdfPCell(new Paragraph("Monthly Payment", ReportFont));
            cell.Colspan = 6;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(_monthlyPayment.ToString("C"), ReportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            //Next Payment Due
            cell = new PdfPCell(new Paragraph("Next Payment Due", ReportFont));
            cell.Colspan = 6;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(nextPaymentDue.ToShortDateString(), ReportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            //Number of Payments
            cell = new PdfPCell(new Paragraph("Number of Payments", ReportFont));
            cell.Colspan = 6;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(numberOfPayments.ToString(), ReportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            summaryTable.AddCell(cell);
        }

        private void ReportHeader(PdfPTable headingtable, Image gif, LayawayContractReport pageEvent)
        {
            PdfPCell cell = new PdfPCell();

            //  heading - row 1
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 6;
            headingtable.AddCell(cell);

            //  heading - row 2
            //date:
            cell = new PdfPCell(new Paragraph(string.Empty, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Date: ", ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            //no ticket SMurphy 3/25/2010 - Store name & Run Date overlapped
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(DateTime.Now.ToString("MM/dd/yyyy"), ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            //  heading - row 3
            //emp#
            cell = new PdfPCell(new Paragraph(string.Empty, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingLeft = 12;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Emp #: ", ReportFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(pageEvent.ReportObject.ReportEmployee, ReportFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            //  heading - row 4
            //empty row
            cell = new PdfPCell(new Paragraph(string.Empty, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.PaddingLeft = 12;
            cell.Colspan = 7;
            headingtable.AddCell(cell);

            //  heading - row 5
            //To:
            cell = new PdfPCell(new Paragraph(string.Empty, ReportFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("To: ", ReportFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(pageEvent.ReportObject.ReportStore, ReportFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            //  heading - row 6
            //Address1
            cell = new PdfPCell(new Phrase(string.Empty, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 5;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(pageEvent.ReportObject.ReportStoreDesc1, ReportFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            //  heading - row 7
            //Address 2
            cell = new PdfPCell(new Phrase(string.Empty, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 5;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(pageEvent.ReportObject.ReportStoreDesc2, ReportFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 2;
            headingtable.AddCell(cell);
            //  heading - row 8
            //empty row
            cell = new PdfPCell(new Phrase(string.Empty, ReportFontHeading));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 7;
            headingtable.AddCell(cell);

            //  heading - row 9
            //empty row
            cell = new PdfPCell(new Phrase(string.Empty, ReportFontHeading));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 7;
            headingtable.AddCell(cell);

            //  heading - row 10
            //Report title row
            cell = new PdfPCell(new Phrase(pageEvent.ReportObject.ReportTitle, ReportFontHeading));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 7;
            headingtable.AddCell(cell);

            //  heading - row 11
            //empty row
            cell = new PdfPCell(new Phrase(string.Empty, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 7;
            headingtable.AddCell(cell);

            //  heading - row 12
            //empty row
            cell = new PdfPCell(new Phrase(string.Empty, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 7;
            headingtable.AddCell(cell);

            //  heading - row 13
            //empty cell
            cell = new PdfPCell(new Phrase("Layaway #:" + pageEvent.ReportObject.CurrentLayaway.TicketNumber, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            //Layaway # row
            cell = new PdfPCell(new Phrase(string.Empty, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 6;
            headingtable.AddCell(cell);
            //  heading - row 14
            //empty row
            /*cell = new PdfPCell(new Phrase(string.Empty, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 7;
            headingtable.AddCell(cell);*/
            /*cell = new PdfPCell(new Phrase(PawnUtilities.String.StringUtilities.fillString("_", 150), ReportFont));
            cell.Colspan = 7;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);*/
            //runReport.ReportLines(headingtable, false, PawnUtilities.String.StringUtilities.fillString("_", 85), false, ReportFont);
        }

        private void ReportDetail(PdfPTable datatable)
        {
            PdfPCell cell;
            WriteSingleLine(datatable, 150);
            //runReport.ReportLines(headingtable, false, PawnUtilities.String.StringUtilities.fillString("_", 85), false, ReportFont);
            //  heading - row 15
            //row 1
            //Phrase phraseICN = new Phrase("ICN", ReportFontLargeBold);
            //Phrase phraseChunkICN = new Phrase(new Chunk("     "));
            //phraseChunkICN.Add(phraseICN);

            //Phrase phraseDescription = new Phrase("ICN", ReportFontLargeBold);
            //Phrase phraseChunkDescription = new Phrase(new Chunk("     "));
            // phraseChunkDescription.Add(phraseDescription);

            cell = new PdfPCell(new Phrase(new Chunk(" ") + "Number", ReportFontLargeBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            cell = new PdfPCell(new Phrase(new Chunk("       ") + "ICN", ReportFontLargeBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            cell = new PdfPCell(new Phrase(new Chunk("      ") + "Merchandise Description", ReportFontLargeBold));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            cell = new PdfPCell(new Phrase(new Chunk("    ") + "Quantity", ReportFontLargeBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            cell = new PdfPCell(new Phrase(new Chunk("           ") + "Unit Price", ReportFontLargeBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            cell = new PdfPCell(new Phrase(new Chunk("   ") + "Extended Price", ReportFontLargeBold));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            //no ticket 4/8/2010 SMurphy center justified data on aisle/shelf/location and other formatting changes
            //runReport.ReportLines(headingtable, false, String.StringUtilities.fillString("-", 240), false, ReportFont);
            //headingtable.AddCell(cell);
            //  heading - row 16
            WriteSingleLine(datatable, 150);
            /*cell = new PdfPCell(new Phrase(PawnUtilities.String.StringUtilities.fillString("_", 150), ReportFont));
            cell.Colspan = 7;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            headingtable.AddCell(cell);*/

            cell = new PdfPCell(new Phrase(string.Empty, ReportFont));
            cell.Colspan = 7;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            datatable.AddCell(cell);

            int counter = 1;
            //for (int i = 0; i <= 69; i++)

                foreach (RetailItem retailItem in ReportObject.CurrentLayaway.RetailItems)
                {
                    decimal _unitPrice = 0.0m;
                    int qty = 1;
                    decimal extendedPrice = retailItem.TotalPrice;

                    _unitPrice = retailItem.NegotiatedPrice;
                    qty = retailItem.Quantity;
                    extendedPrice = _unitPrice * qty - (retailItem.CouponAmount);
                    _saleSubTotal = _saleSubTotal + extendedPrice;
                    //set up tables, etc...

                    //row 1
                    cell = new PdfPCell(new Phrase(new Chunk("    ") + counter.ToString(), ReportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = GetFormattedICNCell(retailItem.Icn);
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = 0;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(retailItem.TicketDescription, ReportFont));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(qty.ToString(), ReportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(Utilities.GetDecimalValue(_unitPrice, 0).ToString("C"), ReportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(Utilities.GetDecimalValue(extendedPrice, 0).ToString("C"), ReportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    datatable.AddCell(cell);

                    if(!string.IsNullOrEmpty(retailItem.UserItemComments))
                    {
                        //here add user comments
                        cell = new PdfPCell(new Phrase(string.Empty, ReportFont));
                        cell.Colspan = 2;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        Phrase Spacing = new Phrase("", ReportFontMedium);
                        Phrase Spacing2 = new Phrase("", ReportFontMedium);
                        Phrase phraseCommentsDescription = new Phrase("Comments: ", ReportFontMediumBold);
                        Phrase phraseComments = new Phrase(retailItem.UserItemComments, ReportFontMedium);

                        Spacing.Add(phraseCommentsDescription);
                        Spacing.Add(Spacing2);
                        Spacing.Add(phraseComments);

                        //phraseCommentsDescription.Add(Spacing);
                        //phraseCommentsDescription.Add(phraseComments);
                        cell = new PdfPCell();
                        cell.Colspan = 2;
                        cell.AddElement(Spacing);
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        cell.VerticalAlignment = Rectangle.ALIGN_TOP;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        cell = new PdfPCell();
                        cell.Colspan = 3;
                        cell.AddElement(Spacing2);
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        cell.VerticalAlignment = Rectangle.ALIGN_TOP;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);

                        cell = new PdfPCell(new Phrase(string.Empty, ReportFont));
                        cell.Colspan = 7;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        cell.Border = Rectangle.NO_BORDER;
                        datatable.AddCell(cell);
                    }
                    counter++;
                }
            
        }

        #endregion

        #region Public Properties
        public LayawayReportObject ReportObject { get; set; }

        #endregion

        #region Public Methods
        public bool CreateReport()
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.LETTER);
            runReport = new LayawayRunReports();
            try
            {
                //set up RunReport event overrides & create doc
                _saleSubTotal = 0.0m;
                LayawayContractReport events = this;
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ReportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                MultiColumnText columns = new MultiColumnText(document.PageSize.Top - 150, document.PageSize.Height - (100));
                columns.AddSimpleColumn(-63, document.PageSize.Width + 63);
                
                //set up tables, etc...
                PdfPTable table = new PdfPTable(7);
                table.WidthPercentage = 80;// document.PageSize.Width;
                table.TotalWidth = document.PageSize.Width;
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(35);
                
                document.SetPageSize(PageSize.LETTER);
                document.SetMargins(-100, -100, 10, 45);
                
                document.AddTitle(ReportObject.ReportTitle + ": " + DateTime.Now.ToString("MM/dd/yyyy"));

                ReportDetail(table);
                AddLines(1, table, false, " ", false, ReportFont);
                ReportSummary(table);
                AddLines(1, table, false, " ", false, ReportFont);
                WriteSingleLine(table, 150);
                AddLines(1, table, false, " ", false, ReportFont);
                //WriteAgreement(table, (LayawayContractReport)writer.PageEvent);
                //AddLines(5, table, false, " ", false, ReportFont);
                //WriteFooter(table, (LayawayContractReport)writer.PageEvent);
                document.Open();
                //CallWriteAgreement(writer, document);
                columns.AddElement(table);
                float tableHeight = table.TotalHeight;
                //agreement stuff
                
                MultiColumnText agreementColumns;
                //table.WriteSelectedRows(0, -1, 10, (document.PageSize.Height - 166), writer.DirectContent);


                //add objects to document
                document.Add(columns);

                float agreementTableHeight = 155f;
                float pageSpan = (int)tableHeight / 542;
                float remainingHeight = 625f - (tableHeight - ((pageSpan * 542) - 50)); ;//document.PageSize.Top - 150 - tableHeight - 75;
                float newPageTop = document.PageSize.Top - 150;
                if (remainingHeight > agreementTableHeight)
                {
                    agreementColumns = new MultiColumnText(remainingHeight + 75, agreementTableHeight);
                }
                else
                {
                    document.NewPage();
                    //agreementColumns = new MultiColumnText(document.PageSize.Bottom + 550, document.PageSize.Height - (166));
                    agreementColumns = new MultiColumnText(document.PageSize.Top - 150, agreementTableHeight);
                }
                agreementColumns.AddSimpleColumn(-63, document.PageSize.Width + 63);
                PdfPTable agreementTable = new PdfPTable(7);
                agreementTable.WidthPercentage = 80;// document.PageSize.Width;
                agreementTable.TotalWidth = document.PageSize.Width;
                WriteAgreement(agreementTable, (LayawayContractReport)writer.PageEvent);
                AddLines(5, table, false, " ", false, ReportFont);
                WriteFooter(agreementTable, (LayawayContractReport)writer.PageEvent);
                agreementColumns.AddElement(agreementTable);

                float agtableHeight = agreementTable.TotalHeight;
                document.Add(agreementColumns);
                document.Close();
                //nnaeme
               //return true;
                //OpenFile(ReportObject.ReportTempFileFullName);
                
                //CreateReport();
                isSuccessful = true;
            }
            catch (DocumentException de)
            {
                ReportObject.ReportError = de.Message; 
                ReportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            catch (IOException ioe)
            {
                ReportObject.ReportError = ioe.Message; 
                ReportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            return isSuccessful;
        }
        #endregion

        #region Public overrides
        public override void OnOpenDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            try
            {
                footerBaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                contentByte = writer.DirectContent;
                template = contentByte.CreateTemplate(50, 50);
                contentByteFooter = writer.DirectContent;
                templateFooter = contentByte.CreateTemplate(50, 50);
            }
            catch (DocumentException)
            {
            }
            catch (IOException)
            {
            }
        }

        public override void OnStartPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnStartPage(writer, document);
            try
            {
                PdfPTable headerTbl = new PdfPTable(7);

                //set the width of the table to be the same as the document
                headerTbl.TotalWidth = document.PageSize.Width - 20;
                headerTbl.WidthPercentage = 80;
                //I use an image logo in the header so I need to get an instance of the image to be able to insert it. I believe this is something you couldn't do with older versions of iTextSharp
                //Image logo = Image.GetInstance(HttpContext.Current.Server.MapPath("/images/logo.jpg"));
                Image logo = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                logo.ScalePercent(25);

                /*create instance of a table cell to contain the logo
                PdfPCell cell = new PdfPCell(logo);

                //align the logo to the right of the cell
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;

                //add a bit of padding to bring it away from the right edge
                cell.PaddingRight = 20;

                //remove the border
                cell.Border = 0;

                //Add the cell to the table
                headerTbl.AddCell(cell);*/

                ReportHeader(headerTbl, logo, (LayawayContractReport)writer.PageEvent);
                //write the rows out to the PDF output stream. I use the height of the document to position the table. Positioning seems quite strange in iTextSharp and caused me the biggest headache.. It almost seems like it starts from the bottom of the page and works up to the top, so you may ned to play around with this.
                headerTbl.WriteSelectedRows(0, -1, 10, (document.PageSize.Height - 10), writer.DirectContent);
            }
            catch (Exception)
            {
                return;
            }
        }

        public override void OnCloseDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            template.BeginText();   
            template.SetFontAndSize(footerBaseFont, 8);
            template.ShowText((writer.PageNumber - 1).ToString());
            template.EndText();
        }

        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            int pageN = writer.PageNumber;
            string text = string.Empty;
            //string[] reportName = writer.Info.Get(PdfName.TITLE).ToString().Split(':');
            text = string.Format("{0} Page {1} of ", StringUtilities.fillString(" ", 80), pageN);
            //PdfContentByte dc = writer.DirectContent;
           
            float len = footerBaseFont.GetWidthPoint(text, 8);

            contentByte.BeginText();
            //if last page, figure out if it is last page
            contentByte.SetFontAndSize(footerBaseFont, 8);
            //Madhu fix for defexct PWNU00001411
            contentByte.SetTextMatrix(len / 2, 30);
            contentByte.ShowText(text);
            contentByte.EndText();
            //Madhu fix for defexct PWNU00001411
            contentByteFooter.AddTemplate(templateFooter, 25, document.PageSize.Height - 500);
            contentByte.AddTemplate(template, len + len / 2, 30);
        }
        #endregion
    }
}
