/**************************************************************************
*PendingTransReport.cs 
* Class that will be used for Transfer in Pending transfer report
* History:
*  291 jkingsly 03/09/2011 Initial Version
* **************************************************************************/
using System;
using System.Collections.Generic;
using Common.Controllers.Application;
using Common.Libraries.Objects.Business;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace Reports.TransferIN
{
    public class PendingTransReport : PdfPageEventHelper
    {
        private List<TransferVO> _transferList;
        private ReportObject.TransferINReportStruct _reportObj;

        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;

        private Document _document;
        private Font _reportFont;
        private Font _reportFontBold;
        private Font _reportFontLargeBold;
        private string _logPath = null;
        string reportFileName = null;
        //main objects
        public ReportObject _ReportObject;

        public PendingTransReport(List<TransferVO> transferList, ReportObject.TransferINReportStruct reportObj)
        {
            _transferList = transferList;
            _reportObj = reportObj;
            _logPath = reportObj.logPath;
        }

        public string getReportWithPath()
        {
            return reportFileName;
        }

        public bool CreateReport()
        {
            bool isSuccessful = false;

            _document = new iTextSharp.text.Document(PageSize.LETTER);

            try
            {
                string reportTitle = "Pending Shop Transfer Summary";

                _ReportObject = new ReportObject();
                _ReportObject.CreateTemporaryFullName();

                if (!Directory.Exists(_logPath))
                    Directory.CreateDirectory(_logPath);

                reportFileName = _logPath + "\\" + _ReportObject.ReportTempFileFullName;

                PdfWriter writer = PdfWriter.GetInstance(_document, new FileStream(reportFileName, FileMode.Create));
                writer.PageEvent = this;
                Image gif = Image.GetInstance(Common.Properties.Resources.logo, BaseColor.WHITE);
                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
                _reportFontBold = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD);
                gif.ScalePercent(35);
                _document.AddTitle(reportTitle);
                _document.SetMargins(-50, -55, 10, 45);
                _document.Open();
                createPendingSummaryHeader(gif, reportTitle);
                PrintReportDetail();
                addTotalRow(_transferList.Count);
                _document.Close();
                isSuccessful = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error generating report :" + e);
                isSuccessful = false;
            }
            return isSuccessful;
        }

        private void AddTwoBlankRows(PdfPTable headerTable, PdfPCell containerCell, int times)
        {
            //Adding Two Blank Rows
            for (int i = 0; i < times; i++)
            {
                containerCell = getBlankCell();
                containerCell.Colspan = 6;
                headerTable.AddCell(containerCell);
            }
        }

        private PdfPCell getBlankCell()
        {
            Paragraph parah = new Paragraph("");
            PdfPCell textCell = new PdfPCell(parah);
            textCell.Border = Rectangle.NO_BORDER;
            return textCell;
        }

        private void createPendingSummaryHeader(Image gif, string rptTitleText)
        {
            PdfPTable headerTable = new PdfPTable(6);
            PdfPCell containerCell = new PdfPCell(gif);

            containerCell.Colspan = 4;
            containerCell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(containerCell);

            PdfPTable dateTable = new PdfPTable(1);

            Paragraph parah = new Paragraph("Run As Of  :" + DateTime.Now.ToString(), _reportFont);
            parah.Alignment = Rectangle.ALIGN_RIGHT;
            PdfPCell textCell = new PdfPCell(parah);
            textCell.Border = Rectangle.NO_BORDER;
            dateTable.AddCell(textCell);
            containerCell = new PdfPCell(dateTable);
            containerCell.Colspan = 2;
            containerCell.Border = Rectangle.NO_BORDER;
            //Finished adding Logo And Right Side Text
            headerTable.AddCell(containerCell);

            parah = new Paragraph("Shop No. " + _reportObj.FromShopName, _reportFont);
            parah.Alignment = Rectangle.ALIGN_LEFT;
            textCell = new PdfPCell(parah);
            textCell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(textCell);
            //Shop No. [Shop Number]

            //Adding Two Blank Rows
            AddTwoBlankRows(headerTable, containerCell, 4);

            // Adding the Title
            parah = new Paragraph(rptTitleText, _reportFontLargeBold);
            parah.Alignment = Rectangle.ALIGN_CENTER;
            containerCell = new PdfPCell(parah);
            containerCell.Colspan = 6;
            containerCell.Border = Rectangle.NO_BORDER;
            containerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            containerCell.VerticalAlignment = Element.ALIGN_TOP;
            headerTable.AddCell(containerCell);

            //Section for Populating Transfer In Summary Middle Section
            AddTwoBlankRows(headerTable, containerCell, 4);

            _document.Add(headerTable);
        }

        private void PrintReportDetail()
        {
            int row = 1;
            int page = 1;
            PdfPTable table = new PdfPTable(9);

            foreach (TransferVO vo in _transferList)
            {
                if (row / page >= 30 || row == 1)
                {
                    //Not the first page so have it go to a new page.
                    if (row != 1)
                    {
                        _document.Add(table);
                        table.DeleteBodyRows();
                        _document.NewPage();
                        //For Adding space on new page
                        PdfPTable headerTable1 = new PdfPTable(9);
                        PdfPCell containerCell1 = new PdfPCell();
                        AddTwoBlankRows(headerTable1, containerCell1, 4);
                        _document.Add(headerTable1);
                        table = new PdfPTable(9);
                        page++;
                    }

                    //ADD HEADER FOR EACH TABLE
                    PdfPCell cell;
                    cell = new PdfPCell(new Paragraph("Number", _reportFont));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Shop", _reportFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Transfer Type", _reportFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Status", _reportFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Reject Transfers", _reportFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Transfer Number", _reportFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Date", _reportFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Total#Items", _reportFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Total Cost", _reportFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);
                }
                PrintDetailRow(vo, table, row);
                row++;
            }
            //Add the last table to the document.
            _document.Add(table);
            PdfPTable headerTable = new PdfPTable(9);

            /* containerCell = new PdfPCell(new Paragraph("BOLD = Transfer Items Over 14 days", _reportFontBold));
            containerCell.Border = Rectangle.NO_BORDER;
            containerCell.Colspan = 9;
            headerTable.AddCell(containerCell);*/

            _document.Add(headerTable);
        }

        private void PrintDetailRow(TransferVO vo, PdfPTable table, int row)
        {
            PdfPCell cell;
            Font fontToUSE = _reportFont;

            if (isBoldRow(vo.StatusDate))
                fontToUSE = _reportFontBold;

            cell = new PdfPCell(new Paragraph(row.ToString(), fontToUSE));
            //cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(vo.StoreNickName, fontToUSE));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(vo.GetTransferTypeDescription(), fontToUSE));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(vo.GetTransferInStatusDescription(), fontToUSE));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            //For Rejected Transfers will be avail later
            cell = new PdfPCell(new Paragraph("", fontToUSE));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);
            cell = new PdfPCell(new Paragraph(vo.TransferTicketNumber.ToString(), fontToUSE));

            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(vo.StatusDate.ToString("MM/dd/yyyy"), fontToUSE));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);
            cell = new PdfPCell(new Paragraph(vo.NumberOfItems.ToString(), fontToUSE));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(String.Format("{0:C}", vo.Amount), fontToUSE));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);
        }

        private bool isBoldRow(DateTime dt)
        {
            if (dt == null) return false;

            TimeSpan elapsed = DateTime.Now.Subtract(dt);
            double daysAgo = elapsed.TotalDays;
            if (daysAgo > 14) return true;
            return false;
        }

        private void addTotalRow(int count)
        {
            PdfPTable headerTable = new PdfPTable(9);
            PdfPCell containerCell = new PdfPCell();

            containerCell = new PdfPCell(new Paragraph(count + " Transfer In", _reportFont));
            containerCell.Border = Rectangle.NO_BORDER;
            containerCell.Colspan = 9;
            headerTable.AddCell(containerCell);
            _document.Add(headerTable);
        }

        #region pdf page event handler with for adding page numbers in the footer
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

        public override void OnEndPage(PdfWriter writer, Document doc)
        {
            OnEndPageNew(writer, doc);
            base.OnEndPage(writer, doc);

            int pageN = writer.PageNumber;
            String text = "Page " + pageN + " of ";
            Rectangle pageSize = doc.PageSize;
            contentByte.SetRGBColorFill(100, 100, 100);
            float len = footerBaseFont.GetWidthPoint(text, 8);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            contentByte.SetTextMatrix(pageSize.GetLeft(270), pageSize.GetBottom(15));
            contentByte.ShowText(text);
            contentByte.EndText();
            contentByte.AddTemplate(template, pageSize.GetLeft(270) + len, pageSize.GetBottom(15));
            OnEndPageNew(writer, doc);
        }

        public override void OnCloseDocument(PdfWriter writer, Document doc)
        {
            base.OnCloseDocument(writer, doc);

            template.BeginText();
            template.SetFontAndSize(footerBaseFont, 8);
            template.SetTextMatrix(0, 0);
            template.ShowText("" + (writer.PageNumber - 1));
            template.EndText();
        }

        public void OnEndPageNew(PdfWriter writer, Document doc)
        {
            //I use a PdfPtable with 2 columns to position my footer where I want it
            PdfPTable footerTbl = new PdfPTable(1);

            //set the width of the table to be the same as the document
            footerTbl.TotalWidth = doc.PageSize.Width;

            //Center the table on the page
            footerTbl.HorizontalAlignment = Element.ALIGN_CENTER;

            //Create a paragraph that contains the footer text
            Paragraph para = new Paragraph("BOLD = Transfer Items Over 14 days", _reportFontBold);

            //add a carriage return
            para.Add(Environment.NewLine);
            //para.Add("Some more footer text");

            //create a cell instance to hold the text
            PdfPCell cell = new PdfPCell(para);

            //set cell border to 0
            cell.Border = 0;

            //add some padding to bring away from the edge
            cell.PaddingLeft = 20;

            //add cell to table
            footerTbl.AddCell(cell);

            //write the rows out to the PDF output stream.
            footerTbl.WriteSelectedRows(0, -1, 0, (doc.BottomMargin + 10), writer.DirectContent);
        }
        #endregion pdf page event handler with for adding page numbers in the footer
        /*
        * Temporary testing method for the report can be used from any form
        private void callTempPendingReports()
        {
        List<TransferItemVO> transferList = new List<TransferItemVO>();
        ReportObject.TransferINReportStruct reportObj = new ReportObject.TransferINReportStruct();
        reportObj.FromShopName = "M & M Services";
        reportObj.logPath =
        SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration
        .BaseLogPath;
        TransferItemVO vo = null;
        for (int i = 0; i < 10; i++)
        {
        vo = new TransferItemVO();
        vo.StoreNumber = "1000" + i;
        vo.TransferType = "Refurb";
        vo.Status = "Completed";
        vo.RejectedTransfers = "32322";
        vo.TransferNumber = 12345 + i;
        vo.TransactionDate = DateTime.Now;
        if (i > 7)
        {
        DateTime startDate = DateTime.Parse("02-22-2011");
        vo.TransactionDate = startDate;
        }
        if (i > 8)
        {
        DateTime startDate = DateTime.Parse("02-23-2011");
        vo.TransactionDate = startDate;
        }
        vo.TotalItems = 10;
        vo.TotalCost = "32245.45";
        transferList.Add(vo);
        }
        PawnReports.Reports.TransferIN.PendingTransReport transreport =
        new PawnReports.Reports.TransferIN.PendingTransReport(transferList, reportObj);
        transreport.CreateReport();
        MessageBox.Show("Calling reports1");
        }*/
    }
}
