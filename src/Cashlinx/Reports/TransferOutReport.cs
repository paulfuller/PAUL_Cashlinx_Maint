using System;
using System.Collections.Generic;
using System.IO;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.BarcodeGenerator;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Barcode = Common.Libraries.Utility.BarcodeGenerator.Barcode;
using ReportObject = Common.Controllers.Application.ReportObject;
using Common.Libraries.Utility.Logger;

namespace Reports
{
    public class TransferOutReport : ReportBase
    {
        #region Private Members
        //used by overriden methods for footers
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private PdfTemplate template2;
        private BaseFont footerBaseFont = null;
        private BaseFont footerBaseFontBold = null;
        //used by report
        private Font _reportFontLargeBold;
        
        private iTextSharp.text.Document _document;
        //main objects
        public ReportObject _ReportObject;

        private List<TransferItemVO> _mdseTransfer;

        private DateTime _shopDT;
        private string _userName;
        private string _storeName;
        private string _storeNumber;
        private string _transferNumber;
        private string _logPath = null;
        //private string _wholesaleOrAppraisal = null;
        private string reportFileName = null;

        private const string _SHOP_NO = "Shop No.";
        private const string _GUN_ROOM = "Gun Room";
        private const string ARIAL = "Arial";
        private ReportObject.TransferReport _reportObj;
        private string _type = null;

        #endregion Private Members

        #region Constructor
        public TransferOutReport(List<TransferItemVO> t, DateTime shopDT, string storeName, string storeNumber,
                                 string userName, string transferNumber, string logpath, string type, ReportObject.TransferReport reportObj, IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {
            _mdseTransfer = t;
            _shopDT = shopDT;
            _userName = userName;
            _storeName = storeName;
            _storeNumber = storeNumber;
            _transferNumber = transferNumber;
            _logPath = logpath;
            _type = type;
            _reportObj = reportObj;
        } 
        #endregion

        #region Public Methods

        public string getReportWithPath()
        {
            return reportFileName;
        }

        public bool CreateReport()
        {
            bool isSuccessful = false;

            //_document = new iTextSharp.text.Document(PageSize.HALFLETTER.Rotate());
            _document = new iTextSharp.text.Document(PageSize.LETTER);

            try
            {
                //set up RunReport event overrides & create doc
                _ReportObject = new ReportObject();
                _ReportObject.CreateTemporaryFullName();

                if (!Directory.Exists(_logPath))
                    Directory.CreateDirectory(_logPath);

                reportFileName = string.Format("{0}\\{1}", _logPath, _ReportObject.ReportTempFileFullName);

                PdfWriter writer = PdfWriter.GetInstance(_document, new FileStream(reportFileName, FileMode.Create));
                writer.PageEvent = this;

                //set up tables, etc...
                int columns = 6;
                PdfPTable table = new PdfPTable(columns);
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);
                //Image gif = Image.GetInstance(PawnReportResources.calogo, BaseColor.WHITE); 

                _reportFont = FontFactory.GetFont(ARIAL, 8, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont(ARIAL, 12, iTextSharp.text.Font.BOLD);

                gif.ScalePercent(35);
                //gif.ScalePercent(75);

                _document.AddTitle("Transfer Out Report");

                //_document.SetPageSize(PageSize.HALFLETTER.Rotate());
                _document.SetMargins(-50, -55, 10, 45);
                _document.Open();   //Go ahead and open the document so tables can be added.
                var _reportTitle = string.Format("Transfer Out {0} Summary", _type);
                if ((_SHOP_NO).Equals(_type) || (_GUN_ROOM).Equals(_type))
                {
                    if ((_SHOP_NO).Equals(_type)) _reportTitle = String.Format("Transfer Out {0} to Shop Summary", "Shop");
                    PrintShopReportHeader(gif, _reportTitle);
                }
                else
                {
                    PrintReportHeader(table, gif, columns);
                }
                PrintReportDetail();

                table.HeaderRows = 3;

                _document.Add(table);
                addTotalCostRow();
                //_document.SetPageSize(PageSize.HALFLETTER.Rotate());
                _document.Close();

                //OpenFile(reportFileName);
                //CreateReport();
                isSuccessful = true;
            }
            catch (DocumentException de)
            {
                _ReportObject.ReportError = de.Message;
                _ReportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }
            catch (IOException ioe)
            {
                _ReportObject.ReportError = ioe.Message;
                _ReportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }

            return isSuccessful;
        }

        #endregion Public Methods

        #region Private Methods

        private Image getBarCodeImage()
        {
            var barcodeImage = Barcode.DoEncode(EncodingType.CODE128C, _transferNumber);
            var barcodeGif = Image.GetInstance(barcodeImage, BaseColor.WHITE);
            barcodeGif.ScaleAbsoluteHeight(35);
            barcodeGif.ScaleAbsoluteWidth(200);
            return barcodeGif;
        }

        private void addTotalCostRow()
        {
            PdfPTable headerTable = new PdfPTable(6);
            PdfPCell containerCell = new PdfPCell();
            for (int i = 0; i <= 3; i++)
            {
                containerCell = new PdfPCell(new Paragraph(""));
                containerCell.Border = Rectangle.NO_BORDER;
                headerTable.AddCell(containerCell);
            }
            containerCell = new PdfPCell();
            containerCell.Border = Rectangle.NO_BORDER;
            Paragraph parah = new Paragraph("Total: $" + string.Format("{0:C}", _reportObj.transferAmount), _reportFont);
            
            parah.Alignment = Element.ALIGN_RIGHT;
            containerCell.AddElement(parah);
            containerCell.Colspan = 3;
            headerTable.AddCell(containerCell);
            _document.Add(headerTable);
        }

        private void PrintShopReportHeader(Image gif, string rptTitleText)
        {
            String spacing = "  ";
            PdfPTable headerTable = new PdfPTable(6);
            PdfPCell containerCell = null;
            // # add image and initial text
            PdfPCell LCell = new PdfPCell();
            PdfPTable imageTable = new PdfPTable(1);
            containerCell = new PdfPCell(gif);
            containerCell.Border = Rectangle.NO_BORDER;
            imageTable.AddCell(containerCell);
            containerCell = new PdfPCell(getBarCodeImage());
            containerCell.Border = Rectangle.NO_BORDER;
            imageTable.AddCell(containerCell);

            // set properties for Lcell for alignment
            LCell.Border = Rectangle.NO_BORDER;
            LCell.AddElement(imageTable);
            LCell.HorizontalAlignment = Element.ALIGN_LEFT;
            LCell.VerticalAlignment = Element.ALIGN_TOP;
            LCell.PaddingLeft = -20;
            LCell.Colspan = 4;
            // Add left cell to header table
            headerTable.AddCell(LCell);

            // Add text information for the right side

            PdfPCell RCell = new PdfPCell();

            PdfPTable textTable = new PdfPTable(1);
            containerCell = new PdfPCell(new Paragraph("Date: " + _shopDT.ToString(), _reportFont));
            containerCell.Border = Rectangle.NO_BORDER;
            textTable.AddCell(containerCell);

            containerCell = new PdfPCell(new Paragraph("User ID: " + _userName, _reportFont));
            containerCell.Border = Rectangle.NO_BORDER;
            textTable.AddCell(containerCell);

            containerCell = new PdfPCell(new Paragraph("Transfer #: " + _transferNumber, _reportFontLargeBold));
            containerCell.Border = Rectangle.NO_BORDER;
            textTable.AddCell(containerCell);

            RCell.AddElement(textTable);
            RCell.Border = Rectangle.NO_BORDER;
            RCell.HorizontalAlignment = Element.ALIGN_LEFT;
            RCell.VerticalAlignment = Element.ALIGN_TOP;
            RCell.Colspan = 2;
            headerTable.AddCell(RCell);

            //prepare cell for From and To Texts
            LCell = new PdfPCell();
            PdfPTable fromTable = new PdfPTable(1);
            containerCell = new PdfPCell(new PdfPCell(new Paragraph("From:" + spacing + _reportObj.FromStoreName, _reportFont)));
            containerCell.Border = Rectangle.NO_BORDER;
            fromTable.AddCell(containerCell);
            containerCell = new PdfPCell(new PdfPCell(new Paragraph("       " + spacing + _reportObj.FromStoreAddrLine1, _reportFont)));
            containerCell.Border = Rectangle.NO_BORDER;
            fromTable.AddCell(containerCell);
            containerCell = new PdfPCell(new PdfPCell(new Paragraph("       " + spacing + _reportObj.FromStoreAddrLine2, _reportFont)));
            containerCell.Border = Rectangle.NO_BORDER;
            fromTable.AddCell(containerCell);
            containerCell = new PdfPCell(new PdfPCell(new Paragraph("       " + spacing + string.Empty, _reportFont)));
            containerCell.Border = Rectangle.NO_BORDER;
            fromTable.AddCell(containerCell);
            containerCell = new PdfPCell(new PdfPCell(new Paragraph("Telephone: " + spacing + _reportObj.FromTelephone, _reportFont)));
            containerCell.Border = Rectangle.NO_BORDER;
            fromTable.AddCell(containerCell);
            containerCell = new PdfPCell(new PdfPCell(new Paragraph("Fax: " + spacing + _reportObj.FromFax, _reportFont)));
            containerCell.Border = Rectangle.NO_BORDER;
            fromTable.AddCell(containerCell);

            LCell.AddElement(fromTable);
            LCell.Border = Rectangle.NO_BORDER;
            LCell.HorizontalAlignment = Element.ALIGN_LEFT;
            LCell.VerticalAlignment = Element.ALIGN_TOP;
            LCell.PaddingLeft = -10;
            LCell.Colspan = 3;
            headerTable.AddCell(LCell);

            RCell = new PdfPCell();
            PdfPTable ToTable = new PdfPTable(1);
            containerCell = new PdfPCell(new Paragraph("To:" + spacing + _reportObj.ToStoreName, _reportFont));
            containerCell.Border = Rectangle.NO_BORDER;
            ToTable.AddCell(containerCell);
            containerCell = new PdfPCell(new Paragraph("     " + spacing + _reportObj.ToStoreAddrLine1, _reportFont));
            containerCell.Border = Rectangle.NO_BORDER;
            ToTable.AddCell(containerCell);
            containerCell = new PdfPCell(new Paragraph("     " + spacing + _reportObj.ToStoreAddrLine2, _reportFont));
            containerCell.Border = Rectangle.NO_BORDER;
            ToTable.AddCell(containerCell);
            containerCell = new PdfPCell(new Paragraph("     ", _reportFont));
            containerCell.Border = Rectangle.NO_BORDER;
            ToTable.AddCell(containerCell);
            containerCell = new PdfPCell(new Paragraph("     " + spacing + _reportObj.storeMgrPhone, _reportFont));
            containerCell.Border = Rectangle.NO_BORDER;
            ToTable.AddCell(containerCell);
            containerCell = new PdfPCell(new Paragraph("     " + spacing + _reportObj.storeMgrName, _reportFont));
            containerCell.Border = Rectangle.NO_BORDER;
            ToTable.AddCell(containerCell);

            RCell.AddElement(ToTable);
            RCell.Border = Rectangle.NO_BORDER;
            RCell.HorizontalAlignment = Element.ALIGN_LEFT;
            RCell.VerticalAlignment = Element.ALIGN_TOP;
            RCell.Colspan = 3;
            headerTable.AddCell(RCell);

            // Add Heading 
            containerCell = new PdfPCell(new Paragraph(rptTitleText, _reportFontLargeBold));
            containerCell.Border = Rectangle.NO_BORDER;
            containerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            containerCell.VerticalAlignment = Element.ALIGN_TOP;
            containerCell.Padding = 20f;
            containerCell.Colspan = 6;
            headerTable.AddCell(containerCell);

            //Add text above table
            containerCell = new PdfPCell(new Paragraph(string.Empty, _reportFont));
            containerCell.Border = Rectangle.NO_BORDER;
            containerCell.HorizontalAlignment = Element.ALIGN_LEFT;
            containerCell.VerticalAlignment = Element.ALIGN_TOP;
            containerCell.Padding = 20f;
            containerCell.Colspan = 6;
            headerTable.AddCell(containerCell);

            _document.Add(headerTable);
        }

        private void PrintReportHeader(PdfPTable table, Image gif, int columns)
        {
            PdfPTable images = new PdfPTable(1);
            PdfPTable headerDetails = new PdfPTable(1);

            var barcodeImage = Barcode.DoEncode(EncodingType.CODE128C, _transferNumber);
            Image barcodeGif = Image.GetInstance(barcodeImage, BaseColor.WHITE);
            barcodeGif.ScaleAbsoluteHeight(35);
            barcodeGif.ScaleAbsoluteWidth(200);

            PdfPCell subCell = new PdfPCell(gif);
            subCell.Border = Rectangle.NO_BORDER;
            images.AddCell(new PdfPCell(subCell));

            subCell = new PdfPCell(barcodeGif);
            subCell.Border = Rectangle.NO_BORDER;
            images.AddCell(new PdfPCell(subCell));

            //  Heading - Row 1
            PdfPCell cell = new PdfPCell(images);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = -10;
            cell.Colspan = 4;
            table.AddCell(cell);

            subCell = new PdfPCell(new PdfPCell(new Paragraph("Date: " + _shopDT.ToShortDateString(), _reportFont)));
            subCell.Border = Rectangle.NO_BORDER;
            headerDetails.AddCell(subCell);
            subCell = new PdfPCell(new PdfPCell(new Paragraph("From: " + _storeName + "#" + _storeNumber, _reportFont)));
            subCell.Border = Rectangle.NO_BORDER;
            headerDetails.AddCell(subCell);
            subCell = new PdfPCell(new PdfPCell(new Paragraph("Destination: CATCO", _reportFont)));
            subCell.Border = Rectangle.NO_BORDER;
            headerDetails.AddCell(subCell);
            subCell = new PdfPCell(new PdfPCell(new Paragraph("Employee ID#: " + _userName, _reportFont)));
            subCell.Border = Rectangle.NO_BORDER;
            headerDetails.AddCell(subCell);

            cell = new PdfPCell(headerDetails);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;
            table.AddCell(cell);

            // heading - Row 2
            cell = new PdfPCell(new Paragraph("Transfer Out Report", _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Padding = 20f;
            cell.Colspan = columns;
            table.AddCell(cell);

            // Heading - Row 3
            cell = new PdfPCell(new Paragraph("Transfer #" + _transferNumber, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Padding = 20f;
            cell.Colspan = columns;
            table.AddCell(cell);

            _document.Add(table);
        }

        private void PrintReportDetail()
        {
            int row = 1;
            int page = 1;
            PdfPTable table = new PdfPTable(9);

            foreach (TransferItemVO vo in _mdseTransfer)
            {
                if (row / page >= 30 || row == 1)
                {
                    //Not the first page so have it go to a new page.
                    if (row != 1)
                    {
                        _document.Add(table);
                        table.DeleteBodyRows();
                        _document.NewPage();

                        table = new PdfPTable(9);
                        page++;
                    }

                    //ADD HEADER FOR EACH TABLE
                    PdfPCell cell;
                    cell = new PdfPCell(new Paragraph("Number", _reportFont));
                    //cell.Border = Rectangle.TOP_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Gun Number", _reportFont));
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

                    if (_type.Equals("Gun Room"))
                    {
                        cell = new PdfPCell(new Paragraph("Merchandise Description", _reportFont));
                        //cell.Border = Rectangle.TOP_BORDER;
                        //cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_TOP;
                        cell.Colspan = 5;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Paragraph("Cost", _reportFont));
                        //cell.Border = Rectangle.TOP_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_TOP;
                        table.AddCell(cell);
                    }
                    else
                    {
                        cell = new PdfPCell(new Paragraph("Merchandise Description", _reportFont));
                        //cell.Border = Rectangle.TOP_BORDER;
                        //cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_TOP;
                        cell.Colspan = 3;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Paragraph("Quantity", _reportFont));
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
                    }
                }
                PrintDetailRow(vo, table, row);
                row++;
            }
            //Add the last table to the document.
            _document.Add(table);
        }

        private void PrintDetailRow(TransferItemVO vo, PdfPTable table, int row)
        {
            decimal totalCost = vo.ItemCost;
            if (Convert.ToInt32(vo.ICNQty) > 0 && vo.ItemCost > 0.0m)
                totalCost = Convert.ToInt32(vo.ICNQty) * vo.ItemCost;
            PdfPCell cell;

            cell = new PdfPCell(new Paragraph(row.ToString(), _reportFont));
            //cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            cell = new PdfPCell(new Paragraph(vo.GunNumber == null ? string.Empty : vo.GunNumber.ToString(), _reportFont));
            //cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            Phrase icnPhrase = new Phrase(GetFormattedICNPhrase(vo.ICN));
            cell = new PdfPCell(icnPhrase);
            //cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);

            if (_type.Equals("Gun Room"))
            {
                cell = new PdfPCell(new Paragraph(vo.ItemDescription, _reportFont));
                //cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 5;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(String.Format("{0:C}", vo.ItemCost), _reportFont));
                //cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);
            }
            else
            {
                cell = new PdfPCell(new Paragraph(vo.ItemDescription, _reportFont));
                //cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 3;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(vo.ICNQty, _reportFont));
                //cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(String.Format("{0:C}", vo.ItemCost), _reportFont));
                //cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(String.Format("{0:C}", totalCost), _reportFont));
                //cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);
            }
        }

        #endregion Private Methods

        #region pdf page event handler with for adding page numbers in the footer

        public override void OnOpenDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            try
            {
                footerBaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                footerBaseFontBold = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                contentByte = writer.DirectContent;
                template = contentByte.CreateTemplate(50, 50);
                template2 = contentByte.CreateTemplate(50, 50);
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
            string text1 = "Transfer #: " + _transferNumber;
            String text = "Page " + pageN + " of ";
            Rectangle pageSize = doc.PageSize;
            contentByte.SetRGBColorFill(100, 100, 100);

            float len1 = footerBaseFontBold.GetWidthPoint(text1, 11);
            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFontBold, 11);
            contentByte.SetTextMatrix(pageSize.GetLeft(50), pageSize.GetBottom(15));
            contentByte.ShowText(text1);
            contentByte.EndText();
            contentByte.AddTemplate(template2, pageSize.GetLeft(50) + len1, pageSize.GetBottom(15));


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
    }
}
