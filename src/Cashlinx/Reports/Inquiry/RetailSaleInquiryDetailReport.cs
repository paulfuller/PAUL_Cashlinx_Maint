using System;
using System.Collections.Generic;
using Common.Libraries.Objects;
using Common.Libraries.Utility.String;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ReportObject = Common.Controllers.Application.ReportObject;
using Common.Libraries.Utility.Logger;

namespace Reports.Inquiry
{
    public class RetailSaleInquiryDetailReport : ReportBase
    {
        #region Fields
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        private BaseFont footerBaseFontGunTotals = null;
        private PdfTemplate templateGunTotals;
        public RunReport runReport;
        private int _pageCount = 1;
        //private int _totalNumGuns = 0;
        private String _icn = String.Empty;
        private String _description = String.Empty;
        #endregion

        #region Private Properties
        private int PageCount
        {
            get
            {
                return _pageCount;
            }
            set
            {
                _pageCount = value;
            }
        }
        #endregion

        #region Private Methods


        public struct RetailSaleTender
        {
            public String tenderType;
            public decimal refAmount;
        }

        private void WriteRetailSaleCustomerSectionHeader(PdfPTable columnsTable, ReportObject.RetailSaleCustomer retailSaleCustomer)
        {
            if (retailSaleCustomer.customerName != String.Empty)
            {
                WriteCell(columnsTable, String.Empty, ReportFontBold, 12, Element.ALIGN_RIGHT, Rectangle.TOP_BORDER);
                WriteCell(columnsTable, String.Empty, ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);
                WriteCell(columnsTable, "Customer", ReportFontHeading, 11, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(columnsTable, String.Empty, ReportFontBold, 12, Element.ALIGN_RIGHT, Rectangle.BOTTOM_BORDER);
            }
        }

        private void WriteRetailSaleCustomerSection(PdfPTable detailsTable, Int32 columnCount, ReportObject.RetailSaleCustomer retailSaleCustomer)
        {
            if (retailSaleCustomer.customerName != String.Empty)
            {
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Customer #:", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleCustomer.customerNumber, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "DOB:", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleCustomer.DOB.ToShortDateString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Sex:", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleCustomer.height, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Wt:", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleCustomer.weight, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Name:", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleCustomer.customerName, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Phone:", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleCustomer.phone, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Race:", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleCustomer.race, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Ht:", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleCustomer.height, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Address:", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleCustomer.customerAddress, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Customer ID:", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleCustomer.customerId, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Hair:", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleCustomer.hairColorCode, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleCustomer.city + " " + retailSaleCustomer.state + " " + retailSaleCustomer.zipCode, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Eyes:", ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleCustomer.eyeColorCode, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            }
        }

        private void WriteRetailSaleTransactionDetailSectionHeader(PdfPTable columnsTable, ReportObject.RetailSaleListing retailSaleListing)
        {
            if (retailSaleListing.ticketNumber > 0)
            {
                WriteCell(columnsTable, String.Empty, ReportFontBold, 12, Element.ALIGN_RIGHT, Rectangle.TOP_BORDER);
                WriteCell(columnsTable, String.Empty, ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);
                WriteCell(columnsTable, "Transaction Detail", ReportFontHeading, 11, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(columnsTable, String.Empty, ReportFontBold, 12, Element.ALIGN_RIGHT, Rectangle.BOTTOM_BORDER);
            }
        }

        private void WriteRetailSaleTransactionDetailSection(PdfPTable detailsTable, Int32 columnCount, ReportObject.RetailSaleListing retailSaleListing, List<ReportObject.RetailSaleTender> retailSaleTenderList)
        {
            if (retailSaleListing.ticketNumber > 0)
            {
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "MSR #:", ReportFontBold, 2, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleListing.ticketNumber.ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Date and Time:", ReportFontBold, 2, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleListing.date.ToShortDateString() + " " + retailSaleListing.date.ToShortTimeString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Total Sale Amount w/Tax:", ReportFontBold, 2, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "$" + (retailSaleListing.saleAmount + retailSaleListing.tax).ToString("0.00"), ReportFontMedium, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Layaway #:", ReportFontBold, 2, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleListing.layawayTicketNumber.ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Terminal ID:", ReportFontBold, 2, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleListing.terminalId, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Sales Tax Amount:", ReportFontBold, 2, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "$" + retailSaleListing.tax.ToString("0.00"), ReportFontMedium, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Original #:", ReportFontBold, 2, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleListing.originalTicketNumber.ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Tender Types:", ReportFontBold, 2, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                if (retailSaleTenderList != null)
                {
                    WriteCell(detailsTable, retailSaleTenderList[0].tenderType + " " + retailSaleTenderList[0].refAmount.ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                }
                else
                {
                    WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                }
                WriteCell(detailsTable, "Shop Number:", ReportFontBold, 2, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleListing.shopNumber.ToString(), ReportFontMedium, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "User ID:", ReportFontBold, 2, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleListing.userId, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                if (retailSaleTenderList != null && retailSaleTenderList.Count > 1)
                {
                    WriteCell(detailsTable, retailSaleTenderList[1].tenderType + " " + retailSaleTenderList[1].refAmount.ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                }
                else
                {
                    WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                }
                WriteCell(detailsTable, "Status:", ReportFontBold, 2, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleListing.status, ReportFontMedium, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, "Cash Drawer", ReportFontBold, 2, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, retailSaleListing.cashDrawer.ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                if (retailSaleTenderList != null && retailSaleTenderList.Count > 2)
                {
                    WriteCell(detailsTable, retailSaleTenderList[2].tenderType + " " + retailSaleTenderList[2].refAmount.ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                }
                else
                {
                    WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                }
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

                if (retailSaleTenderList != null)
                {
                    for (int i = 3; i < retailSaleTenderList.Count; i++)
                    {
                        WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                        WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                        WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                        WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                        WriteCell(detailsTable, retailSaleTenderList[i].tenderType + " " + retailSaleTenderList[i].refAmount.ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                        WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                        WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                        WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                    }
                }

                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            }
        }

        private void WriteRetailSaleMerchandiseSectionHeader(PdfPTable columnsTable, List<ReportObject.RetailSaleMerchandise> retailSaleMerchandiseList)
        {
            if (retailSaleMerchandiseList != null)
            {
                WriteCell(columnsTable, String.Empty, ReportFontLargeBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(columnsTable, "Item #", ReportFontLargeBold, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(columnsTable, "ICN", ReportFontLargeBold, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(columnsTable, "Status", ReportFontLargeBold, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(columnsTable, "Description", ReportFontLargeBold, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(columnsTable, "Item Amount", ReportFontLargeBold, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(columnsTable, String.Empty, ReportFontLargeBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                
                WriteCell(columnsTable, String.Empty, ReportFontBold, 12, Element.ALIGN_RIGHT, Rectangle.BOTTOM_BORDER);
            }
        }

        private void WriteRetailSaleMerchandiseSection(PdfPTable detailsTable, Int32 columnCount, List<ReportObject.RetailSaleMerchandise> retailSaleMerchandiseList)
        {
            if (retailSaleMerchandiseList != null)
            {
                Int32 i = 0;
                foreach (ReportObject.RetailSaleMerchandise retailSaleMerchandise in retailSaleMerchandiseList)
                {
                    WriteCell(detailsTable, String.Empty, ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                    WriteCell(detailsTable, (i + 1).ToString() + @")", ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                    WriteCell(detailsTable, retailSaleMerchandise.ICN, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                    WriteCell(detailsTable, retailSaleMerchandise.statusCD.ToString(), ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                    WriteCell(detailsTable, retailSaleMerchandise.description, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                    WriteCell(detailsTable, "$" + retailSaleMerchandise.amount.ToString("0.00"), ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                    WriteCell(detailsTable, String.Empty, ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);

                    i++;
                }

                WriteCell(detailsTable, String.Empty, ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);

                WriteCell(detailsTable, String.Empty, ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                WriteCell(detailsTable, String.Empty, ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER, false);
            }
        }

        private void WriteRetailSaleHistorySectionHeader(PdfPTable columnsTable, List<ReportObject.RetailSaleHistory> retailSaleHistoryList)
        {
            if (retailSaleHistoryList.Count > 0)
            {
                WriteCell(columnsTable, String.Empty, ReportFontBold, 12, Element.ALIGN_RIGHT, Rectangle.TOP_BORDER);
                WriteCell(columnsTable, String.Empty, ReportFontBold, 1, Element.ALIGN_RIGHT, Rectangle.NO_BORDER);
                WriteCell(columnsTable, "History", ReportFontHeading, 11, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(columnsTable, String.Empty, ReportFontBold, 12, Element.ALIGN_RIGHT, Rectangle.BOTTOM_BORDER);
            }
        }

        private void WriteRetailSaleHistorySectionColumnHeaders(PdfPTable columnsTable, List<ReportObject.RetailSaleHistory> retailSaleHistoryList)
        {
            if (retailSaleHistoryList.Count > 0)
            {
                WriteCell(columnsTable, String.Empty, ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(columnsTable, "MSR#", ReportFontUnderlined, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(columnsTable, "Receipt#", ReportFontUnderlined, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(columnsTable, "Event Type", ReportFontUnderlined, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(columnsTable, "Date / Time", ReportFontUnderlined, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(columnsTable, "Tran Amount", ReportFontUnderlined, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(columnsTable, "Sales Tax", ReportFontUnderlined, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(columnsTable, "Total Amount", ReportFontUnderlined, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(columnsTable, "User Id", ReportFontUnderlined, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                WriteCell(columnsTable, String.Empty, ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            }
        }

        private void WriteRetailSaleHistorySection(PdfPTable detailsTable, Int32 columnCount, List<ReportObject.RetailSaleHistory> retailSaleHistoryList)
        {
            if (retailSaleHistoryList.Count > 0)
            {
                foreach (ReportObject.RetailSaleHistory retailSaleHistory in retailSaleHistoryList)
                {
                    WriteCell(detailsTable, String.Empty, ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                    WriteCell(detailsTable, retailSaleHistory.receiptNumber.ToString(), ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                    WriteCell(detailsTable, retailSaleHistory.receiptDetailNumber.ToString(), ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                    WriteCell(detailsTable, retailSaleHistory.refEvent, ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                    WriteCell(detailsTable, retailSaleHistory.refTime.ToShortDateString() + " " + retailSaleHistory.refTime.ToShortTimeString(), ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                    WriteCell(detailsTable, "$" + retailSaleHistory.amount.ToString("0.00"), ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                    WriteCell(detailsTable, "$" + retailSaleHistory.tax.ToString("0.00"), ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                    WriteCell(detailsTable, "$" + (retailSaleHistory.amount + retailSaleHistory.tax).ToString("0.00"), ReportFontMedium, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                    WriteCell(detailsTable, retailSaleHistory.entId, ReportFontMedium, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
                    WriteCell(detailsTable, String.Empty, ReportFontBold, 1, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                }
            }
        }

        private void ReportHeader(PdfPTable headingtable, Image gif, RetailSaleInquiryDetailReport pageEvent, int colspan)
        {
            PdfPCell cell = new PdfPCell();

            //row 1
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 8;
            headingtable.AddCell(cell);

            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

            //row 2
            WriteCell(headingtable, DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString(), ReportFont, 8, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            
            WriteCell(headingtable, "Retail Sale Inquiry Detail", ReportFontHeading, 8, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            
            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
            WriteCell(headingtable, string.Empty, ReportFont, colspan, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);      
        }

        #endregion

        #region Public Properties
        public ReportObject ReportObject { get; set; }

        #endregion

        #region Public Methods
        public bool CreateReport(ReportObject.RetailSaleListing RetailSaleListingData, 
            ReportObject.RetailSaleCustomer RetailSaleCustomerData,
            List<ReportObject.RetailSaleMerchandise> RetailSaleMerchandiseList,
            List<ReportObject.RetailSaleTender> RetailSaleTenderList,
            List<ReportObject.RetailSaleHistory> RetailSaleHistoryList)
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.LETTER.Rotate());
            try
            {
                //set up RunReport event overrides & create doc
                _pageCount = 1;
                RetailSaleInquiryDetailReport events = this;
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(ReportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                MultiColumnText columns = new MultiColumnText(document.PageSize.Top - 100, document.PageSize.Height - (50));
                float pageLeft = document.PageSize.Left;
                float pageright = document.PageSize.Right;
                columns.AddSimpleColumn(-75, document.PageSize.Width + 76);

                //set up tables, etc...
                PdfPCell cell = new PdfPCell();
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(25);
                
                runReport = new RunReport();
                document.Open();
                document.SetPageSize(PageSize.LETTER.Rotate());
                document.SetMargins(-100, -100, 10, 45);
                document.AddTitle(ReportObject.ReportTitle + ": " + DateTime.Now.ToString("MM/dd/yyyy"));

                PdfPTable customerTable = new PdfPTable(12);
                customerTable.WidthPercentage = 100;// document.PageSize.Width;
                WriteRetailSaleCustomerSectionHeader(customerTable, RetailSaleCustomerData);
                WriteRetailSaleCustomerSection(customerTable, 12, RetailSaleCustomerData);
                columns.AddElement(customerTable);

                PdfPTable transactionDetailTable = new PdfPTable(12);
                transactionDetailTable.WidthPercentage = 100;// document.PageSize.Width;
                WriteRetailSaleTransactionDetailSectionHeader(transactionDetailTable, RetailSaleListingData);
                WriteRetailSaleTransactionDetailSection(transactionDetailTable, 12, RetailSaleListingData, RetailSaleTenderList);
                columns.AddElement(transactionDetailTable);

                PdfPTable merchandiseTable = new PdfPTable(12);
                merchandiseTable.WidthPercentage = 100;// document.PageSize.Width;
                WriteRetailSaleMerchandiseSectionHeader(merchandiseTable, RetailSaleMerchandiseList);
                WriteRetailSaleMerchandiseSection(merchandiseTable, 12, RetailSaleMerchandiseList);
                columns.AddElement(merchandiseTable);

                PdfPTable retailSaleHistoryTable = new PdfPTable(12);
                retailSaleHistoryTable.WidthPercentage = 100;// document.PageSize.Width;
                WriteRetailSaleHistorySectionHeader(retailSaleHistoryTable, RetailSaleHistoryList);
                WriteRetailSaleHistorySectionColumnHeaders(retailSaleHistoryTable, RetailSaleHistoryList);
                WriteRetailSaleHistorySection(retailSaleHistoryTable, 12, RetailSaleHistoryList);
                columns.AddElement(retailSaleHistoryTable);

                //here add detail
                document.Add(columns);
                document.Close();
                OpenFile(ReportObject.ReportTempFileFullName);
                //CreateReport(_icn, _description, theData);
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

        #region Public Overrides
        public override void OnOpenDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            try
            {
                footerBaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                contentByte = writer.DirectContent;
                template = contentByte.CreateTemplate(50, 50);
                footerBaseFontGunTotals = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                templateGunTotals = contentByte.CreateTemplate(50, 50);
                //PdfPTable headerTbl = new PdfPTable(21);
                //set the width of the table to be the same as the document
                //headerTbl.TotalWidth = document.PageSize.Width - 10;
                //headerTbl.WidthPercentage = 80;
                //I use an image logo in the header so I need to get an instance of the image to be able to insert it. I believe this is something you couldn't do with older versions of iTextSharp
                //Image logo = Image.GetInstance(HttpContext.Current.Server.MapPath("/images/logo.jpg"));
                //Image logo = Image.GetInstance(PawnReportResources.logo, BaseColor.WHITE);
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                //logo.ScalePercent(25);
                //ReportHeader(headerTbl, logo, (RetailSaleInquiryDetailReport)writer.PageEvent);
                //write the rows out to the PDF output stream. I use the height of the document to position the table. Positioning seems quite strange in iTextSharp and caused me the biggest headache.. It almost seems like it starts from the bottom of the page and works up to the top, so you may ned to play around with this.
               // headerTbl.WriteSelectedRows(0, -1, 7, (document.PageSize.Height - 10), writer.DirectContent);
            }
            catch (DocumentException)
            {
            }
            catch (IOException)
            {
            }
        }

        public override void OnCloseDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            template.BeginText();
            template.SetFontAndSize(footerBaseFont, 8);
            template.SetTextMatrix(0, 0);
            template.ShowText(String.Empty + (writer.PageNumber - 1));
            template.EndText();
        }

        public override void OnStartPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnStartPage(writer, document);
            try
            {
                /*PdfPTable headerTbl = new PdfPTable(21);
                //set the width of the table to be the same as the document
                headerTbl.TotalWidth = document.PageSize.Width - 10;
                //headerTbl.WidthPercentage = 80;
                //I use an image logo in the header so I need to get an instance of the image to be able to insert it. I believe this is something you couldn't do with older versions of iTextSharp
                //Image logo = Image.GetInstance(HttpContext.Current.Server.MapPath("/images/logo.jpg"));
                Image logo = Image.GetInstance(PawnReportResources.logo, BaseColor.WHITE);
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                logo.ScalePercent(25);*/

                PdfPTable headerTbl = new PdfPTable(8);
                headerTbl.TotalWidth = document.PageSize.Width - 10;
                Image logo = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                logo.ScalePercent(25);
                ReportHeader(headerTbl, logo, (RetailSaleInquiryDetailReport)writer.PageEvent, 8);

                //ReportColumns(headerTbl);
               
                //write the rows out to the PDF output stream. I use the height of the document to position the table. Positioning seems quite strange in iTextSharp and caused me the biggest headache.. It almost seems like it starts from the bottom of the page and works up to the top, so you may ned to play around with this.
                if(PageCount == 1)
                    headerTbl.WriteSelectedRows(0, -1, 16, (document.PageSize.Height - 10), writer.DirectContent);
                else
                    headerTbl.WriteSelectedRows(0, -1, 16, (document.PageSize.Height - 8), writer.DirectContent);
                PageCount++;
            }
            catch (Exception)
            {
                return;
            }
        }

        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            int pageN = writer.PageNumber;
            string text = string.Empty;
            string[] reportName = writer.Info.Get(PdfName.TITLE).ToString().Split(':');
            text = string.Format("{0} Page {1}  of ", StringUtilities.fillString(" ", 80), pageN);
            var pageSize = document.PageSize;

            //add pageNumbers
            float len = footerBaseFont.GetWidthPoint(text, 8);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            //Madhu fix for defexct PWNU00001411
            contentByte.SetTextMatrix(pageSize.GetLeft(205), pageSize.GetBottom(15));
            contentByte.ShowText(text);
            contentByte.EndText();
            //Madhu fix for defexct PWNU00001411
            contentByte.AddTemplate(template, pageSize.GetLeft(205) + len, pageSize.GetBottom(15));
        }
        #endregion

    
        public RetailSaleInquiryDetailReport (IPdfLauncher pdfLauncher) : base (pdfLauncher)
        {
            
        }
    }
}
