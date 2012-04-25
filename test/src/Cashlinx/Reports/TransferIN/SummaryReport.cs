/**************************************************************************
 *SummaryReport.cs 
 * Class that will be used for Transfer in Pending transfer report
 * History:
 *  290 jkingsly 03/09/2011 Initial Version
 * **************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ReportObject = Common.Controllers.Application.ReportObject;

namespace Reports.TransferIN
{
   public class SummaryReport : ReportBase
    {
        private List<TransferItemVO> _transferList;
        private ReportObject.TransferINReportStruct _reportObj;


        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;

        private Document _document;
        private new Font _reportFont;
        private Font _reportFontLargeBold;
        private Font _reportFontLargeUnderline;
        private string _logPath = null;
        private string reportFileName = "";
        //main objects
        public ReportObject _ReportObject;
        private decimal totalAmt = 0;
        
        public string getReportFileName()
        {
            return reportFileName;
        }

        public SummaryReport(List<TransferItemVO> transferList, ReportObject.TransferINReportStruct reportObj, IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {
            _transferList = transferList;
            _reportObj = reportObj;
          _logPath=  reportObj.logPath;
        }

        public bool CreateReport()
        {
            bool isSuccessful = false;

            _document = new iTextSharp.text.Document(PageSize.LETTER);

            try
            {
                
                string reportTitle = "Transfer In Merchandise Summary";
                int columns = 6;

                _ReportObject = new ReportObject();
                _ReportObject.CreateTemporaryFullName();

                if (!Directory.Exists(_logPath))
                    Directory.CreateDirectory(_logPath);

                reportFileName = _logPath + "\\" + _ReportObject.ReportTempFileFullName;

                PdfWriter writer = PdfWriter.GetInstance(_document, new FileStream(reportFileName, FileMode.Create));
                writer.PageEvent = this;
                PdfPTable table = new PdfPTable(columns);
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD);
                _reportFontLargeUnderline = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.UNDERLINE);
                gif.ScalePercent(35);
                _document.AddTitle(reportTitle);
                _document.SetMargins(-50, -55, 10, 45);
                _document.Open();
                createTransInSummaryHeader(gif, reportTitle);
                PrintReportDetail();
                addTotalCostRow();
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

        private PdfPCell getBlankCell()
        {
            Paragraph parah = new Paragraph("");
            PdfPCell textCell = new PdfPCell(parah);
            textCell.Border = Rectangle.NO_BORDER;
            return textCell;
        }

        private void createTransInSummaryHeader(Image gif, string rptTitleText)
        {
            PdfPTable headerTable = new PdfPTable(6);
            PdfPCell containerCell = new PdfPCell(gif);

            containerCell.Colspan=4;
            containerCell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(containerCell);

            PdfPTable dateTable = new PdfPTable(1);

            Paragraph parah = new Paragraph("Date :" + _reportObj.transDate, _reportFont);
            parah.Alignment = Rectangle.ALIGN_RIGHT;
            PdfPCell textCell = new PdfPCell(parah);
            textCell.Border = Rectangle.NO_BORDER;
            dateTable.AddCell(textCell);
            parah = new Paragraph("UserID :" + _reportObj.userID, _reportFont);
            parah.Alignment = Rectangle.ALIGN_RIGHT;
            textCell = new PdfPCell(parah);
            textCell.Border = Rectangle.NO_BORDER;
            dateTable.AddCell(textCell);
            containerCell = new PdfPCell(dateTable);
            containerCell.Colspan = 2;
            containerCell.Border = Rectangle.NO_BORDER;
            //Finished adding Logo And Right Side Text
            headerTable.AddCell(containerCell);

            //Adding Two Blank Rows
            AddTwoBlankRows(headerTable, containerCell, 2);

            parah = new Paragraph("To :" + _reportObj.ToStoreName + " " + _reportObj.ToStoreNo, _reportFont);
            //parah.Alignment = Rectangle.ALIGN_LEFT;
            containerCell = new PdfPCell(parah);
            containerCell.Colspan = 3;
            containerCell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(containerCell);

            dateTable = new PdfPTable(1);
            parah = new Paragraph("From :" + "	" + _reportObj.FromShopName + " " + _reportObj.FromShopNo, _reportFont);
            string space = "          ";                     
           // parah.Alignment = Rectangle.ALIGN_RIGHT;
            textCell = new PdfPCell(parah);
            textCell.Border = Rectangle.NO_BORDER;
            dateTable.AddCell(textCell);
            parah = new Paragraph(space + _reportObj.FromStoreAddrLine1, _reportFont);
            //parah.Alignment = Rectangle.ALIGN_RIGHT;
            textCell = new PdfPCell(parah);
            textCell.Border = Rectangle.NO_BORDER;
            dateTable.AddCell(textCell);

            parah = new Paragraph(space + _reportObj.FromStoreAddrLine2, _reportFont);
            //parah.Alignment = Rectangle.ALIGN_RIGHT;
            textCell = new PdfPCell(parah);
            textCell.Border = Rectangle.NO_BORDER;
            dateTable.AddCell(textCell);

            /*parah = new Paragraph(space + "[Shop State] [Shop Zip Code]", _reportFont);
            //parah.Alignment = Rectangle.ALIGN_RIGHT;
            textCell = new PdfPCell(parah);
            textCell.Border = Rectangle.NO_BORDER;
            dateTable.AddCell(textCell);*/

            //Blank Cell For space
            dateTable.AddCell(getBlankCell());

            
            parah = new Paragraph(space+_reportObj.storeMgrPhone, _reportFont);
            //parah.Alignment = Rectangle.ALIGN_RIGHT;
            textCell = new PdfPCell(parah);
            textCell.Border = Rectangle.NO_BORDER;
            dateTable.AddCell(textCell);
            parah = new Paragraph(space +_reportObj.storeMgrName, _reportFont);
            //parah.Alignment = Rectangle.ALIGN_RIGHT;
            textCell = new PdfPCell(parah);
            textCell.Border = Rectangle.NO_BORDER;
            dateTable.AddCell(textCell);

            containerCell = new PdfPCell(dateTable);
            containerCell.Colspan = 3;
            containerCell.Border = Rectangle.NO_BORDER;
            //Finished adding Right Side From text Content
            headerTable.AddCell(containerCell);

            AddTwoBlankRows(headerTable, containerCell,4);
         
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

            populateMiddleSection(headerTable);

            AddTwoBlankRows(headerTable, containerCell, 4);

            //finally add header table
            _document.Add(headerTable);
        }

        private void populateMiddleSection(PdfPTable headerTable)
        {
            PdfPTable leftTable = new PdfPTable(1);
            PdfPTable rightTable = new PdfPTable(1);
            PdfPCell lCell = new PdfPCell(leftTable);
            lCell.Colspan = 3;
            lCell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(lCell);
            PdfPCell rCell = new PdfPCell(rightTable);
            rCell.Colspan = 3;
            rCell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(rCell);

            // Add Left Table Contents

            PdfPTable carrTable = new PdfPTable(3);

            Paragraph parahT = new Paragraph("Transfer #:", _reportFont);
            lCell = new PdfPCell(parahT);
            lCell.Border = Rectangle.NO_BORDER;
            carrTable.AddCell(lCell);
            lCell = new PdfPCell(new Paragraph(_reportObj.transNum, _reportFont));
            lCell.Border = Rectangle.NO_BORDER;
            carrTable.AddCell(lCell);
            carrTable.AddCell(getBlankCell());

            // Adding blank row
            lCell = new PdfPCell();
            lCell.Colspan = 3;
            lCell.Border = Rectangle.NO_BORDER;
            carrTable.AddCell(lCell);


            parahT = new Paragraph("Carrier:", _reportFont);
            lCell = new PdfPCell(parahT);
            lCell.Border = Rectangle.NO_BORDER;
            carrTable.AddCell(lCell);
            lCell = new PdfPCell(new Paragraph(_reportObj.Carrier, _reportFont));
            lCell.Border = Rectangle.NO_BORDER;
            carrTable.AddCell(lCell);

            // Adding blank row
            lCell = new PdfPCell();
            lCell.Colspan = 3;
            lCell.Border = Rectangle.NO_BORDER;
            carrTable.AddCell(lCell);


            parahT = new Paragraph("Carrier Signature :", _reportFont);
            lCell = new PdfPCell(parahT);
            lCell.Border = Rectangle.NO_BORDER;
            carrTable.AddCell(lCell);
            lCell= new PdfPCell();
            lCell.Border =Rectangle.BOTTOM_BORDER;
            carrTable.AddCell(lCell);
            carrTable.AddCell(getBlankCell());
            lCell = new PdfPCell(carrTable);
            lCell.Border = Rectangle.NO_BORDER;
            leftTable.AddCell(getBlankCell());
            leftTable.AddCell(lCell);


            //Add Right Table Contents
           carrTable = new PdfPTable(3);

           parahT = new Paragraph("Transfer Reference#:", _reportFont);
           rCell = new PdfPCell(parahT);
           rCell.Border = Rectangle.NO_BORDER;
           carrTable.AddCell(rCell);
           rCell = new PdfPCell(new Paragraph(_reportObj.TransferReference, _reportFont));
           //rCell = new PdfPCell();
           rCell.Border = Rectangle.BOTTOM_BORDER;
           carrTable.AddCell(rCell);
           carrTable.AddCell(getBlankCell());

           // Adding blank row
           rCell = new PdfPCell();
           rCell.Colspan = 3;
           rCell.Border= Rectangle.NO_BORDER;
           carrTable.AddCell(rCell);

           parahT = new Paragraph("Date Received:", _reportFont);
           rCell = new PdfPCell(parahT);
           rCell.Border = Rectangle.NO_BORDER;
           carrTable.AddCell(rCell);
           rCell = new PdfPCell(new Paragraph(_reportObj.DateReceived, _reportFont));
           //rCell = new PdfPCell();
           rCell.Border = Rectangle.BOTTOM_BORDER;
           carrTable.AddCell(rCell);
           carrTable.AddCell(getBlankCell());

           // Adding blank row
           rCell = new PdfPCell();
           rCell.Colspan = 3;
           rCell.Border = Rectangle.NO_BORDER;
           carrTable.AddCell(rCell);

           parahT = new Paragraph("Received By:", _reportFont);
           rCell = new PdfPCell(parahT);
           rCell.Border = Rectangle.NO_BORDER;
           carrTable.AddCell(rCell);
           rCell = new PdfPCell(new Paragraph(_reportObj.ReceivedBy, _reportFont));
           //rCell = new PdfPCell();
           rCell.Border = Rectangle.BOTTOM_BORDER;
           carrTable.AddCell(rCell);
           carrTable.AddCell(getBlankCell());

           rCell = new PdfPCell(carrTable);
           rCell.Border = Rectangle.NO_BORDER;
           rightTable.AddCell(rCell);

        }


        private void PrintReportDetail()
        {
            int row = 1;
            int page = 1;
            PdfPTable table = new PdfPTable(9);

            foreach (TransferItemVO vo in _transferList)
            {
                if (row / page >= 30 || row == 1)
                {
                    //Not the first page so have it go to a new page.
                    if (row != 1)
                    {
                        _document.Add(table);
                        table.DeleteBodyRows();
                        _document.NewPage();

                        table = new PdfPTable(6);
                        page++;
                    }


                    //ADD HEADER FOR EACH TABLE
                    PdfPCell cell;
                    cell = new PdfPCell(new Paragraph("Number", _reportFont));
                    //cell.Border = Rectangle.TOP_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);


                    cell = new PdfPCell(new Paragraph("ICN", _reportFont));
                    //cell.Border = Rectangle.TOP_BORDER;
                    // cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);


                    cell = new PdfPCell(new Paragraph("Merchandise Description", _reportFont));
                    //cell.Border = Rectangle.TOP_BORDER;
                    //cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    cell.Colspan = 2;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Qty", _reportFont));
                    //cell.Border = Rectangle.TOP_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Cost", _reportFont));
                    //cell.Border = Rectangle.TOP_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Total Cost", _reportFont));
                    //cell.Border = Rectangle.TOP_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Retail", _reportFont));
                    //cell.Border = Rectangle.TOP_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Total Retail", _reportFont));
                    //cell.Border = Rectangle.TOP_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);
                }
                PrintDetailRow(vo, table, row);
                row++;
            }
            //Add the last table to the document.
            _document.Add(table);
        }

        private void PrintDetailRow(TransferItemVO vo, PdfPTable table, int row)
        {
            decimal totalCost = vo.PfiAmount;
            decimal totalRetail = vo.RetailPrice;
            if (Convert.ToInt32(vo.ICNQty) > 0 && vo.PfiAmount > 0.0m)
                totalCost = Convert.ToInt32(vo.ICNQty) * vo.PfiAmount;
            if (Convert.ToInt32(vo.ICNQty) > 0 && vo.RetailPrice > 0.0m)
                totalRetail = Convert.ToInt32(vo.ICNQty) * vo.RetailPrice;

            PdfPCell cell;
            cell = new PdfPCell(new Paragraph(row.ToString(), _reportFont));
            //cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            Phrase icnPhrase = new Phrase(GetFormattedICNPhrase(vo.ICN));
            //cell = GetFormattedICNCellSmallFont(vo.ICN);
            cell = new PdfPCell(icnPhrase);
            //cell.Border = Rectangle.NO_BORDER;
            //cell.AddElement(icnPhrase);
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(vo.MdseRecordDesc, _reportFont));
            //cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 2;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(vo.ICNQty, _reportFont));
            //cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            totalAmt += vo.PfiAmount;

            cell = new PdfPCell(new Paragraph(String.Format("{0:C}", vo.PfiAmount), _reportFont));
            //cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(String.Format("{0:C}", totalCost), _reportFont));
            //cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            //cell = new PdfPCell(new Paragraph(String.Format("{0:C}", vo.RetailPrice), _reportFont));
            cell = new PdfPCell(new Paragraph(String.Format("{0:C}", vo.RetailPrice), _reportFont));
            //cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(String.Format("{0:C}", totalRetail), _reportFont));
            //cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);
        }

        private void addTotalCostRow()
        {
            PdfPTable headerTable = new PdfPTable(6);
            PdfPCell containerCell = new PdfPCell();

            containerCell = new PdfPCell(new Paragraph(_transferList.Count + " items", _reportFont));
            containerCell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(containerCell);


            for (int i = 0; i <= 2; i++)
            {
                containerCell = new PdfPCell(new Paragraph(""));
                containerCell.Border = Rectangle.NO_BORDER;
                headerTable.AddCell(containerCell);
            }
            containerCell = new PdfPCell();
            containerCell.Border = Rectangle.NO_BORDER;
            Paragraph parah = new Paragraph("Total: " + String.Format("{0:C}", totalAmt), _reportFont);

            parah.Alignment = Element.ALIGN_RIGHT;
            containerCell.AddElement(parah);
            containerCell.Colspan = 3;
            headerTable.AddCell(containerCell);
            _document.Add(headerTable);

        }

        
        private void AddTwoBlankRows(PdfPTable headerTable, PdfPCell containerCell,int times)
        {
            //Adding Two Blank Rows
            for (int i = 0; i < times;i++ ){
                containerCell = getBlankCell();
                containerCell.Colspan = 6;
                headerTable.AddCell(containerCell);
            }
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

        #endregion pdf page event handler with for adding page numbers in the footer

       /* Temporary Test Method for report Can be used from any form
        * private void callTempReports()
        {

            List<TransferItemVO> transferList = new List<TransferItemVO>();
            ReportObject.TransferINReportStruct reportObj = new ReportObject.TransferINReportStruct();

            reportObj.transDate = "02/03/1967";
            reportObj.userID = "testUser";
            reportObj.ToStoreName = "DFW #115";
            reportObj.ToStoreNo = "00115";
            reportObj.FromShopName = "M & M Services";
            reportObj.FromShopNo = "43443";
            reportObj.FromStoreAddrLine1 = "12121 dsds fdffdsfsdfds";
            reportObj.FromStoreAddrLine2 = "Fortworth 434343";

            reportObj.storeMgrPhone = "433-4343-434343";
            reportObj.storeMgrName = "test manager";
            reportObj.transNum = "1234";
            reportObj.Carrier = "UPS";
            reportObj.DateReceived = "12/12/12121";
            reportObj.TransferReference = "3223223";
            reportObj.ReceivedBy = "fdfddfd";
            reportObj.logPath =
            SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration
                                .BaseLogPath;

            TransferItemVO vo = null;
            for (int i = 0; i < 10; i++)
            {
                vo = new TransferItemVO();
                vo.ICN = "123000023232332";
                vo.ItemDescription = "RIFLE; MFGR N/A; MOD N/A; S# 325R2546; BOLT ACTION; DOUBLE BRL.; BLUE STEEL FINISH; 07.65MM CAL.;";
                vo.ItemCost = 54;
                vo.RetailPrice = 100;
                transferList.Add(vo);
            }
            PawnReports.Reports.TransferIN.SummaryReport transreport =
                new PawnReports.Reports.TransferIN.SummaryReport(transferList, reportObj);
            transreport.CreateReport();
            MessageBox.Show("Calling reports");

        }*/
    }
}

