﻿using System;
using System.IO;
using System.Text;
using System.Data;
using Common.Controllers.Application;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Common.Libraries.Utility.Logger;

namespace Reports.Inquiry
{
    public class CashTransferBankTransferReport : PdfPageEventHelper
    {
        // used by overriden methods for footers
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;

        // used by report
        private Font _reportFont;
        private int _Columns = 4;

        private string _StoreNumber
        {
            get
            {
                return reportObject.ReportStore;
            }
        }

        private DataTable _dataTable;
        private DateTime _ShopTime;
        private string _ReportTitle = "Bank Transfer In Details";
        private string _ReportTitleSecondLine = "";

        private Boolean _BankToShop = false;

        // main objects
        public ReportObject reportObject;

        public CashTransferBankTransferReport()
        {
            _ShopTime = DateTime.Now;
        }

        public bool CreateReport(DataTable dt)
        {
            _dataTable = dt;

            if (_dataTable != null && _dataTable.Rows.Count != 0)
            {

                if (_dataTable.Rows[0]["TRANSFERTYPE"].ToString() == "BANKTOSHOP")
                {
                    _ReportTitle = "Bank Transfer In Details";
                    _BankToShop = true;
                }
                else
                {
                    _ReportTitle = "Bank Transfer Out Details";
                    _BankToShop = false;
                }
            }
            else
            {
                reportObject.ReportError = "No Data in datatable.";
                reportObject.ReportErrorLevel = (int)LogLevel.ERROR;
                return false;
            }

            _ReportTitleSecondLine = string.Format("Transfer # {0}", _dataTable.Rows[0]["TRANSFERNUMBER"]);

            bool isSuccessful = false;

            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.HALFLETTER.Rotate());

            try
            {
                //set up RunReport event overrides & create doc
                string reportFileName = Path.GetFullPath(reportObject.ReportTempFileFullName);
                if (!Directory.Exists(Path.GetDirectoryName(reportFileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(reportFileName));
                }
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportFileName, FileMode.Create));
                writer.PageEvent = this;

                PdfPTable table = new PdfPTable(_Columns);
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(35);

                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);

                document.AddTitle(reportObject.ReportTitle);

                document.SetPageSize(PageSize.HALFLETTER.Rotate());

                document.SetMargins(-50, -55, 10, 45);

                table.HeaderRows = 8;

                PrintReportHeader(table, gif);
                PrintReportDetailHeader(table);
                
                table.SetWidths(new[] { 1.5f, 2, 1.5f, 2 });

                PrintReportDetailValues(table);

                PrintReportDetailFooter(table);

                document.Open();
                document.Add(table);
                document.Close();

                isSuccessful = true;
            }
            catch (DocumentException de)
            {
                reportObject.ReportError = de.Message;
                reportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            catch (IOException ioe)
            {
                reportObject.ReportError = ioe.Message;
                reportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }

            return isSuccessful;
        }

        private void PrintReportHeader(PdfPTable table, Image gif)
        {
            PdfPCell cell = new PdfPCell();
            PdfPTable tmpTable = new PdfPTable(1);

            // Left header - Image, and Inquiry
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = -10;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Inquiry", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(tmpTable);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;
            table.AddCell(cell);

            // Right header - Store name, and Run Date
            tmpTable = new PdfPTable(1);
            cell = new PdfPCell(new Paragraph(reportObject.ReportStoreDesc, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.NoWrap = true;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Store # " + reportObject.ReportStore, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Run Date: " + _ShopTime.ToString("MM/dd/yyyy HH:mm"), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(tmpTable);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = 100;
            cell.Colspan = 2;
            table.AddCell(cell);

            // Center header - Report Title. Main and Secondary.
            cell = new PdfPCell(new Paragraph(_ReportTitle));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;
            cell.PaddingLeft = -10;
            cell.Colspan = _Columns;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(_ReportTitleSecondLine));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;
            cell.PaddingLeft = -10;
            cell.PaddingBottom = 20;
            cell.Colspan = _Columns;
            table.AddCell(cell);

            // Left Sub header Transfer Date/Time, User Id
            tmpTable = new PdfPTable(2);
            cell = new PdfPCell(new Paragraph("Transfer Date / Time: ", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(_dataTable.Rows[0]["TRANSFERDATE"].ToString(), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("User ID: ", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(_dataTable.Rows[0]["USERID"].ToString(), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(tmpTable);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = -120;
            cell.Colspan = 2;
            table.AddCell(cell);

            // Right Sub header Amount, Status
            tmpTable = new PdfPTable(2);
            cell.PaddingLeft = -60;
            cell = new PdfPCell(new Paragraph("Amount: ", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.NoWrap = true;
            tmpTable.AddCell(cell);

            decimal decAmount = 0m;
            decimal.TryParse(_dataTable.Rows[0]["TRANSFERAMOUNT"].ToString(), out decAmount);
            cell = new PdfPCell(new Paragraph(decAmount.ToString("C"), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.NoWrap = true;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Status:", _reportFont));
            cell.PaddingLeft = -60;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(_dataTable.Rows[0]["TRANSFERSTATUS"].ToString(), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(tmpTable);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = 120;
            cell.Colspan = 2;
            table.AddCell(cell);

            // Bottom line 
            cell = new PdfPCell(new Paragraph(string.Empty));
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;
            cell.PaddingLeft = -10;
            cell.PaddingBottom = 20;
            cell.Colspan = _Columns;
            table.AddCell(cell);
        }

        private void PrintReportDetailFooter(PdfPTable table)
        {
            StringBuilder disclaimer = new StringBuilder();
            disclaimer.AppendLine("***End of Report***");

            Paragraph paragraph = new Paragraph(disclaimer.ToString(), _reportFont);
            PdfPCell cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.TOP_BORDER;
            cell.BorderWidthTop = 2;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = _Columns;
            cell.PaddingTop = 15;
            table.AddCell(cell);
        }

        private void PrintReportDetailHeader(PdfPTable table)
        {
            Paragraph paragraph = new Paragraph("Source", _reportFont);
            PdfPCell cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;
            table.AddCell(cell);

            paragraph = new Paragraph("Destination", _reportFont);
            cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;
            table.AddCell(cell);
        }

        private void PrintReportDetailValues(PdfPTable table)
        {
            Paragraph paragraph = new Paragraph("Transfer From: ", _reportFont);
            PdfPCell cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            paragraph = new Paragraph(_dataTable.Rows[0]["SOURCE"].ToString(), _reportFont);
            cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            paragraph = new Paragraph("Transfer To: ", _reportFont);
            cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            paragraph = new Paragraph(_dataTable.Rows[0]["DESTINATION"].ToString(), _reportFont);
            cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            //------------------------------------------------------------
            if (_BankToShop)
            {
                paragraph = new Paragraph("Account Number: ", _reportFont);
                cell = new PdfPCell(paragraph);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;

                table.AddCell(cell);

                paragraph = new Paragraph(_dataTable.Rows[0]["routingnumber"].ToString(), _reportFont);
                cell = new PdfPCell(paragraph);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 3;
                table.AddCell(cell);

                //------------------------------------------------------------
                paragraph = new Paragraph("Check Number: ", _reportFont);
                cell = new PdfPCell(paragraph);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.PaddingBottom = 20;
                table.AddCell(cell);

                paragraph = new Paragraph(_dataTable.Rows[0]["checknumber"].ToString(), _reportFont);
                cell = new PdfPCell(paragraph);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 3;
                table.AddCell(cell);
            }
            else
            {
                paragraph = new Paragraph("Account Number: ", _reportFont);
                cell = new PdfPCell(paragraph);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = 3;
                table.AddCell(cell);

                paragraph = new Paragraph(_dataTable.Rows[0]["routingnumber"].ToString(), _reportFont);
                cell = new PdfPCell(paragraph);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);

                //------------------------------------------------------------
                paragraph = new Paragraph("Deposit Bag Number: ", _reportFont);
                cell = new PdfPCell(paragraph);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.PaddingBottom = 20;
                cell.Colspan = 3;
                table.AddCell(cell);

                paragraph = new Paragraph(_dataTable.Rows[0]["depositbagnumber"].ToString(), _reportFont);
                cell = new PdfPCell(paragraph);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);
            }

        }

        public override void OnOpenDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            try
            {
                footerBaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                contentByte = writer.DirectContent;
                template = contentByte.CreateTemplate(50, 50);
            }
            catch (DocumentException)
            {
            }
            catch (IOException)
            {
            }
        }

        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            int pageN = writer.PageNumber;
            string text = "Page " + pageN.ToString();

            float len = footerBaseFont.GetWidthPoint(text, 8);

            Rectangle pageSize = document.PageSize;

            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            contentByte.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(30));
            contentByte.ShowText(_ReportTitle);
            contentByte.EndText();

            contentByte.AddTemplate(template, pageSize.GetLeft(40) + len, pageSize.GetBottom(30));

            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            contentByte.SetTextMatrix(pageSize.GetRight(40), pageSize.GetBottom(30));
            contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, text, pageSize.GetRight(40), pageSize.GetBottom(30), 0);
            contentByte.EndText();
        }
    }
}
