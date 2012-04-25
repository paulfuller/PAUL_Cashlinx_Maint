using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility.BarcodeGenerator;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Common.Libraries.Utility.Logger;
using Barcode = Common.Libraries.Utility.BarcodeGenerator.Barcode;
using ReportObject = Common.Controllers.Application.ReportObject;

namespace Reports
{
    public class TransferOutScrapsReport : ReportBase
    {
        //used by overriden methods for footers
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private PdfTemplate template2;
        private BaseFont footerBaseFont = null;
        private BaseFont footerBaseFontBold = null;

        //used by report
        private new Font _reportFont;
        private Font _reportFontLargeBold;

        private decimal _totalCost;
        private iTextSharp.text.Document _document;

        //main objects
        public ReportObject _ReportObject;

        private List<TransferItemVO> _mdseTransfer;
        private List<IItem> _trnsfrItems;

        private List<AggScrapItemInfo> _aggData = new List<AggScrapItemInfo>();

        private DateTime _shopDT;
        private string _userName;
        private string _transferNumber;
        private BusinessRuleVO brMetalType = null;
        private string _catcoTransferType;
        private string _logPath = null;
        private ReportObject.TransferReport _reportObj;
        private List<string> _listOtherTypes = new List<string>();
        private List<string> _listGoldTypes = new List<string>();

        private string _reportTitle = "Transfer Out Merchandise Report";

        private string reportFileName = null;

        #region Constructor

        public TransferOutScrapsReport(List<TransferItemVO> t, List<IItem> s, BusinessRuleVO rule, DateTime shopDT, string userName, string transferNumber,
            string catcoTransferType, string logPath, ReportObject.TransferReport reportObj, IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {
            _mdseTransfer = t;
            _trnsfrItems = s;
            _shopDT = shopDT;
            _userName = userName;
            _transferNumber = transferNumber;
            brMetalType = rule;
            _catcoTransferType = catcoTransferType;
            _logPath = logPath;
            _reportTitle = String.Format("Transfer Out {0} Summary", _catcoTransferType);
            _reportObj = reportObj;
        }

        #endregion Constructor

        #region Public Methods

        public string getReportWithPath()
        {
            return reportFileName;
        }

        public bool CreateReport()
        {
            bool isSuccessful = false;

            _document = new iTextSharp.text.Document(PageSize.LEGAL);

            try
            {
                //set up RunReport event overrides & create doc
                _ReportObject = new ReportObject();
                _ReportObject.CreateTemporaryFullName();

                if (!Directory.Exists(_logPath))
                    Directory.CreateDirectory(_logPath);
                reportFileName = _logPath + "\\" + _ReportObject.ReportTempFileFullName;
                PdfWriter writer = PdfWriter.GetInstance(_document, new FileStream(reportFileName, FileMode.Create));
                //writer.PageEvent = this;

                //set up tables, etc...
                int columns = 6;
                PdfPTable table = new PdfPTable(columns);
                Image gif = Image.GetInstance(Common.Properties.Resources.logo, BaseColor.WHITE);
                // Image gif = Image.GetInstance(PawnReportResources.calogo, BaseColor.WHITE); 
                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD);

                gif.ScalePercent(35);
                //gif.ScalePercent(75);

                _document.AddTitle(_reportTitle);

                _document.SetPageSize(PageSize.LETTER);
                _document.SetMargins(-50, -55, 10, 45);
                writer.PageEvent = this;
                _document.Open();   //Go ahead and open the document so tables can be added.

                //PrintReportHeader(table, gif, columns);
                PrintReportHeader(gif);
                PrintReportDetail();

                if (!_catcoTransferType.Equals("Appraisal", StringComparison.CurrentCultureIgnoreCase) && !_catcoTransferType.Equals("Wholesale", StringComparison.CurrentCultureIgnoreCase))
                {
                    PopulateMetalTypes();
                    AggregateData();
                    PrintTotalSummaryRow(table, writer);
                }
                table.HeaderRows = 3;

                _document.Add(table);
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

        private void PrintTotalSummaryRow(PdfPTable firstTable, PdfWriter writer)
        {
            //Add some space.  There's got to be a better way to do this.
            PdfPTable spacingTable = new PdfPTable(1);
            PdfPCell spaceCell = new PdfPCell(new Paragraph(" "));
            spaceCell.Border = Rectangle.NO_BORDER;
            spacingTable.AddCell(spaceCell);
            _document.Add(spacingTable);

            PdfPTable table = new PdfPTable(new float[] { 2f, 2f, 2f, .5f, 2f, 2f, 2f });
            PdfPCell cell = new PdfPCell(new Paragraph("Gold: ", _reportFont));

            table.AddCell(cell);
            cell = new PdfPCell(new Paragraph("Total Weight: ", _reportFont));
            table.AddCell(cell);
            cell = new PdfPCell(new Paragraph("Total Cost: ", _reportFont));
            table.AddCell(cell);
            cell = new PdfPCell(new Paragraph("", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
            cell = new PdfPCell(new Paragraph("Other: ", _reportFont));
            table.AddCell(cell);
            cell = new PdfPCell(new Paragraph("Total Weight: ", _reportFont));
            table.AddCell(cell);
            cell = new PdfPCell(new Paragraph("Total Cost: ", _reportFont));
            table.AddCell(cell);

            List<AggScrapItemInfo> goldItems = (from a in _aggData
                                                where a.IsGold
                                                select a).ToList();
            List<AggScrapItemInfo> otherItems = (from a in _aggData
                                                 where !a.IsGold
                                                 select a).ToList();
            int count = GetMax(new List<int> { otherItems.Count, goldItems.Count });

            for (int i = 0; i < count; i++)
            {
                //If gold items and other items count
                //is less than the count then go ahead and print the details.
                if (goldItems.Count > i && goldItems.Count != 0)
                {
                    cell = new PdfPCell(new Paragraph(goldItems[i].ApproximateKarats, _reportFont));
                    table.AddCell(cell);
                    cell = new PdfPCell(new Paragraph(goldItems[i].Weight.ToString(), _reportFont));
                    table.AddCell(cell);
                    cell = new PdfPCell(new Paragraph(String.Format("{0:C}", goldItems[i].Cost), _reportFont));
                    table.AddCell(cell);

                    _totalCost += goldItems[i].Cost;
                }
                else
                {
                    AddBlankCells(table, true);
                }

                //Always print out a spacing cell.
                cell = new PdfPCell(new Paragraph(""));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                if (otherItems.Count > i && otherItems.Count != 0)
                {
                    cell = new PdfPCell(new Paragraph(otherItems[i].TypeOfMetal, _reportFont));
                    table.AddCell(cell);
                    cell = new PdfPCell(new Paragraph(otherItems[i].Weight.ToString(), _reportFont));
                    table.AddCell(cell);
                    cell = new PdfPCell(new Paragraph(String.Format("{0:C}", otherItems[i].Cost), _reportFont));
                    table.AddCell(cell);

                    _totalCost += otherItems[i].Cost;
                }
                else
                {
                    AddBlankCells(table, false);
                }
            }
            AddBlankCells(table, true);
            AddBlankCells(table, true);
            cell = new PdfPCell(new Paragraph("Total: " + String.Format("{0:C}", _totalCost), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
            float yAbsolutePosition = firstTable.CalculateHeights(true);
            table.TotalWidth = 500f;
            table.WriteSelectedRows(0, -1, 24, yAbsolutePosition + 100, writer.DirectContent);
            //_document.Add(table);
        }

        private void AddBlankCells(PdfPTable table, bool noBorder)
        {
            PdfPCell cell = new PdfPCell(new Paragraph(""));
            if (noBorder)
                cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
            cell = new PdfPCell(new Paragraph(""));
            if (noBorder)
                cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
            cell = new PdfPCell(new Paragraph(""));
            if (noBorder)
                cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
        }

        private int GetMax(List<int> DoubleCollection)
        {
            int max = int.MinValue;

            foreach (int i in DoubleCollection)
            {
                if (i > max)
                {
                    max = i;
                }
            }

            return max;
        }

        /* private void PrintReportHeader(PdfPTable table, Image gif, int columns)
         {
             PdfPTable images = new PdfPTable(1);
             PdfPTable headerDetails = new PdfPTable(1);

             Image barcodeGif = getBarCodeImage();
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

             subCell = new PdfPCell(new PdfPCell(new Paragraph("Date: " + _shopDT.ToShortDateString())));
             subCell.Border = Rectangle.NO_BORDER;            
             headerDetails.AddCell(subCell);
             subCell = new PdfPCell(new PdfPCell(new Paragraph("From: " + _storeName +  "#" + _storeNumber)));
             subCell.Border = Rectangle.NO_BORDER;
             headerDetails.AddCell(subCell);
             subCell = new PdfPCell(new PdfPCell(new Paragraph("Destination: CATCO")));
             subCell.Border = Rectangle.NO_BORDER;
             headerDetails.AddCell(subCell);
             subCell = new PdfPCell(new PdfPCell(new Paragraph("Employee: " + _userName)));
             subCell.Border = Rectangle.NO_BORDER;
             headerDetails.AddCell(subCell);
            
             cell = new PdfPCell(headerDetails);
             cell.Border = Rectangle.NO_BORDER;
             cell.HorizontalAlignment = Element.ALIGN_LEFT;
             cell.VerticalAlignment = Element.ALIGN_TOP;
             cell.Colspan = 2;           
             table.AddCell(cell);

             // heading - Row 2
             cell = new PdfPCell(new Paragraph(_reportTitle, _reportFontLargeBold));
             cell.Border = Rectangle.NO_BORDER;
             cell.HorizontalAlignment = Element.ALIGN_CENTER;
             cell.VerticalAlignment = Element.ALIGN_TOP;
             cell.Padding = 20f;
             cell.Colspan = columns;
             table.AddCell(cell);

             // Heading - Row 3
             cell = new PdfPCell(new Paragraph("Transfer #" + _transferNumber));
             cell.Border = Rectangle.NO_BORDER;
             cell.HorizontalAlignment = Element.ALIGN_LEFT;
             cell.VerticalAlignment = Element.ALIGN_TOP;
             cell.Padding = 20f;
             cell.Colspan = columns;
             table.AddCell(cell);

             _document.Add(table);
         }*/

        private Image getBarCodeImage()
        {
            System.Drawing.Image barcodeImage = Barcode.DoEncode(EncodingType.CODE128C, _transferNumber);
            Image barcodeGif = Image.GetInstance(barcodeImage, BaseColor.WHITE);
            barcodeGif.ScaleAbsoluteHeight(35);
            barcodeGif.ScaleAbsoluteWidth(200);
            return barcodeGif;
        }

        private void PrintReportHeader(Image gif)
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
            containerCell = new PdfPCell(new Paragraph("Date: " + _shopDT.ToShortDateString(), _reportFont));
            containerCell.Border = Rectangle.NO_BORDER;
            textTable.AddCell(containerCell);

            containerCell = new PdfPCell(new Paragraph("User ID: " + _userName, _reportFont));
            containerCell.Border = Rectangle.NO_BORDER;
            textTable.AddCell(containerCell);

            containerCell = new PdfPCell(new Paragraph("Transfer #: " + _transferNumber, ReportFontHeading));
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
            containerCell = new PdfPCell(new PdfPCell(new Paragraph("Telephone: " + spacing + _reportObj.storeMgrPhone, _reportFont)));
            containerCell.Border = Rectangle.NO_BORDER;
            fromTable.AddCell(containerCell);
            containerCell = new PdfPCell(new PdfPCell(new Paragraph("Fax: " + spacing + _reportObj.storeFax, _reportFont)));
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
            containerCell = new PdfPCell(new Paragraph("To:" + spacing + _reportObj.ToStoreName + " - " + _reportObj.TransferTypeDepartmentName + " " + _reportObj.TransferTypeFacilityNumber, _reportFont));
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
            containerCell = new PdfPCell(new Paragraph("     " + spacing + _reportObj.TransferTypeFacilityPhone, _reportFont));
            containerCell.Border = Rectangle.NO_BORDER;
            ToTable.AddCell(containerCell);
            containerCell = new PdfPCell(new Paragraph("     " + spacing + _reportObj.TransferTypeFacilityManagerName, _reportFont));
            containerCell.Border = Rectangle.NO_BORDER;
            ToTable.AddCell(containerCell);

            RCell.AddElement(ToTable);
            RCell.Border = Rectangle.NO_BORDER;
            RCell.HorizontalAlignment = Element.ALIGN_LEFT;
            RCell.VerticalAlignment = Element.ALIGN_TOP;
            RCell.Colspan = 3;
            headerTable.AddCell(RCell);

            // Add Heading 
            containerCell = new PdfPCell(new Paragraph(_reportTitle, _reportFontLargeBold));
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


        private void PrintReportDetail()
        {
            int row = 1;
            int page = 1;
            PdfPTable table = new PdfPTable(8);

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

                        table = new PdfPTable(8);
                        page++;
                    }

                    //ADD HEADER FOR EACH TABLE
                    PdfPCell cell;
                    cell = new PdfPCell(new Paragraph("Number", _reportFont));
                    //cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);

                    if (_catcoTransferType == "Scrap")
                    {
                        cell = new PdfPCell(new Paragraph("ICN", _reportFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_TOP;
                        cell.Colspan = 1;
                        table.AddCell(cell);


                        cell = new PdfPCell(new Paragraph("Merchandise Description", _reportFont));
                        //cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_TOP;
                        cell.Colspan = 3;
                        table.AddCell(cell);

                    }
                    else if (_catcoTransferType == "Refurb")
                    {
                        cell = new PdfPCell(new Paragraph("Refurb #", _reportFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_TOP;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Paragraph("ICN", _reportFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_TOP;
                        cell.Colspan = 1;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Paragraph("Merchandise Description", _reportFont));
                        //cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_TOP;
                        cell.Colspan = 3;
                        table.AddCell(cell);
                    }
                    else
                    {
                        cell = new PdfPCell(new Paragraph("ICN", _reportFont));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_TOP;
                        cell.Colspan = 2;
                        table.AddCell(cell);


                        cell = new PdfPCell(new Paragraph("Merchandise Description", _reportFont));
                        //cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_TOP;
                        cell.Colspan = 2;
                        table.AddCell(cell);
                    }
                    //cell.Border = Rectangle.NO_BORDER;

                    if (_catcoTransferType != "Refurb")
                    {
                        cell = new PdfPCell(new Paragraph("Quantity", _reportFont));
                        //cell.Border = Rectangle.NO_BORDER;
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.VerticalAlignment = Element.ALIGN_TOP;
                        table.AddCell(cell);
                    }

                    cell = new PdfPCell(new Paragraph("Cost", _reportFont));
                    //cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_TOP;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph("Total Cost", _reportFont));
                    //cell.Border = Rectangle.NO_BORDER;
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
            PdfPCell cell;
            decimal totalCost = 0.0m;
            if (Convert.ToInt32(vo.ICNQty) > 0 && vo.ItemCost > 0.0m)
                totalCost = Convert.ToInt32(vo.ICNQty) * vo.ItemCost;
            cell = new PdfPCell(new Paragraph(row.ToString(), _reportFont));
            //cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            table.AddCell(cell);
            Icn icn = new Icn(vo.ICN);
            string icnShortCode = icn.GetShortCode();

            if (_catcoTransferType == "Scrap")
            {
                cell = new PdfPCell(new Paragraph(icnShortCode, ReportFontBold));
                //cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 1;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(vo.ItemDescription, _reportFont));
                //cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 3;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);
            }
            else if (_catcoTransferType == "Refurb")
            {
                cell = new PdfPCell(new Paragraph(vo.RefurbNumber.ToString(), _reportFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(icnShortCode, ReportFontBold));
                //cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 1;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);

                cell = new PdfPCell(new Paragraph(vo.ItemDescription, _reportFont));
                //cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 3;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);
            }
            else
            {
                cell = new PdfPCell(new Paragraph(icnShortCode, ReportFontBold));
                //cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 1;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);


                cell = new PdfPCell(new Paragraph(vo.ItemDescription, _reportFont));
                //cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 3;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);
            }

            //cell.Border = Rectangle.NO_BORDER;
            if (_catcoTransferType != "Refurb")
            {
                cell = new PdfPCell(new Paragraph(vo.ICNQty, _reportFont));
                //cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                table.AddCell(cell);
            }

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

        private void PopulateMetalTypes()
        {
            bool bPawnValue;
            string sMetalType = String.Empty;
            string sComponentValue = String.Empty;

            BusinessRuleVO brMETAL_TYPES = brMetalType;
            bPawnValue = brMETAL_TYPES.getComponentValue("GOLD_TYPES", ref sComponentValue);
            if (bPawnValue)
            {
                _listGoldTypes.AddRange(sComponentValue.Split('|'));

                //if (listGoldTypes.FindIndex(delegate(string s)
                //{
                //    return s == _Item.Attributes[iMetalIdx].Answer.AnswerText;
                //}) >= 0)
                //{
                //    sMetalType = "GOLD";
                //}
            }

            if (sMetalType == String.Empty)
            {
                bPawnValue = brMETAL_TYPES.getComponentValue("OTHER_TYPES", ref sComponentValue);
                if (bPawnValue)
                {
                    _listOtherTypes.AddRange(sComponentValue.Split('|'));

                    //if (listOtherTypes.FindIndex(delegate(string s)
                    //{
                    //    return s == _Item.Attributes[iMetalIdx].Answer.AnswerText;
                    //}) >= 0)
                    //{
                    //    sMetalType = "PLATINUM";
                    //}
                }
            }


        }

        private void AggregateData()
        {
            if (_trnsfrItems == null) return;

            bool isGold;
            foreach (IItem item in _trnsfrItems)
            {
                isGold = false;
                if (item is ScrapItem)
                {
                    AggScrapItemInfo info;

                    if (_listGoldTypes.Contains(((ScrapItem)item).TypeOfMetal))
                    {
                        isGold = true;
                    }

                    var results = from a in _aggData
                                  where a.IsGold == isGold
                                        && a.TypeOfMetal == ((ScrapItem)item).TypeOfMetal
                                        && a.ApproximateKarats == (((ScrapItem)item).ApproximateKarats + "K")
                                  select a;

                    if (results.Count() > 0)
                    {
                        info = results.First<AggScrapItemInfo>();
                        info.Cost += item.ItemAmount;
                        try
                        {
                            info.Weight += Convert.ToDecimal(((ScrapItem)item).ApproximateWeight);
                        }
                        catch (Exception)
                        {
                            info.Weight = 0;
                        }
                    }
                    else
                    {
                        info = new AggScrapItemInfo();
                        info.IsGold = isGold;
                        info.TypeOfMetal = ((ScrapItem)item).TypeOfMetal;
                        info.ApproximateKarats = ((ScrapItem)item).ApproximateKarats + "K";
                        try// handle for formatting error
                        {
                            info.Weight = ((ScrapItem)item).ApproximateWeight == null
                                                  ? 0 : Convert.ToDecimal(((ScrapItem)item).ApproximateWeight);
                        }
                        catch (Exception)
                        {
                            info.Weight = 0;
                        }
                        info.Cost = item.ItemAmount;
                        _aggData.Add(info);
                    }
                }
            }
        }

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
        #endregion Private Methods

        #region Class

        /// <summary>
        /// Class for aggregating scrap data for a report.
        /// </summary>
        internal class AggScrapItemInfo
        {
            public AggScrapItemInfo()
            {
            }

            public string TypeOfMetal { get; set; }
            public string ApproximateKarats { get; set; }
            public decimal Weight { get; set; }
            public decimal Cost { get; set; }
            public bool IsGold { get; set; }
        }

        #endregion Class

    }

}
