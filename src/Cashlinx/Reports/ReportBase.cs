using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.IO;
using Common.Libraries.Objects;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using Document = iTextSharp.text.Document;
using Font = iTextSharp.text.Font;
using Image = iTextSharp.text.Image;
using Rectangle = iTextSharp.text.Rectangle;
using ReportObject = Common.Controllers.Application.ReportObject;
using Common.Libraries.Utility.Logger;

namespace Reports
{
    public abstract class ReportBase : PdfPageEventHelper
    {
        #region Fields
        //private PdfContentByte contentByte;
        //private PdfTemplate template;
        //private BaseFont footerBaseFont = null;
        private string _footer;
        private string _disclaimer;
        protected string _totalLabel;
        protected bool showReportDate = true;
        protected bool showGroupCount = true;
        private Font _reportFontReg;
        private Font _reportFontSmall;
        private Font _reportFontSmallBold;
        private Font _reportFontLargeBold;
        private Font _reportFontUnderlined;
        private Font _reportFontHeading;
        private Font _reportFontRegular;
        private Font _reportFontBold;
        private Rectangle _pageSize;
        protected string reportType = "Operational";
        #endregion

        #region Protected Properties

        protected Rectangle reportPageSize
        {
            set
            {
                _pageSize = value;
            }
        }

        protected Font ReportFont
        {
            get
            {
                _reportFontReg = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL);
                return _reportFontReg;
            }
        }

        protected Font ReportFontSmall
        {
            get
            {
                _reportFontSmall = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.NORMAL);
                return _reportFontSmall;
            }

        }

        protected Font ReportFontMedium
        {
            get
            {
                _reportFontSmall = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.NORMAL);
                return _reportFontSmall;
            }

        }

        protected Font ReportFontMediumBold
        {
            get
            {
                _reportFontSmallBold = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD);
                return _reportFontSmallBold;
            }
        }

        protected Font ReportFontSmallBold
        {
            get
            {
                _reportFontSmallBold = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.BOLD);
                return _reportFontSmallBold;
            }
        }
        protected Font ReportFontLargeBold
        {
            get
            {
                _reportFontLargeBold = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD);
                return _reportFontLargeBold;
            }
        }

        protected Font ReportFontUnderlined
        {
            get
            {
                _reportFontUnderlined = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.UNDERLINE);
                return _reportFontUnderlined;
            }
        }

        protected Font ReportFontSmallUnderlined
        {
            get
            {
                _reportFontUnderlined = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.UNDERLINE);
                return _reportFontUnderlined;
            }
        }

        protected Font ReportFontHeading
        {
            get
            {
                _reportFontHeading = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD);
                return _reportFontHeading;
            }
        }

        protected Font ReportFontBold
        {
            get
            {
                _reportFontBold = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.BOLD);
                return _reportFontBold;
            }

        }

        protected Font ReportFontRegular
        {
            get
            {
                _reportFontRegular = FontFactory.GetFont("Arial", 7, iTextSharp.text.Font.NORMAL);
                return _reportFontRegular;
            }
        }
        #endregion
        //used by report
        #region Misc Variables
        protected Font _reportFont;

        private string _StoreNumber
        {
            get { return reportObject.ReportStore; }
        }



        private DateTime _ShopTime;

        //main objects
        public ReportObject reportObject;

        protected delegate string translate(object Field);
        protected struct heading
        {
            public string text;
            public float width;
            public int field_align;
            public int header_align;
            public int align;       // This alignment applies for both header and field if provided
            public string fieldName;
            public string format;
            public bool formatICN;
            public bool showTotal;
            public decimal total;
            public bool groupBy;
            public decimal groupTotal;
            public translate Translator;
        }

        protected struct AuditReportHeaderFields
        {
            public string ReportStore;
            public string StoreNumber;
            public DateTime InventoryAuditDate;
            public int ReportNumber;
            public string ReportTitle;
        }

        protected List<heading> headers = new List<heading>();
        private float[] columnWidths;
        public string GroupByFooterTitle { get; set; }

        public int nrTotalFields = 0;
        #endregion

        public IPdfLauncher PdfLauncher { get; private set; }

        #region Constructors

        protected ReportBase(string footer, string disclaimer, string totalLabel, IPdfLauncher pdfLauncher)
            : this(pdfLauncher)
        {
            _ShopTime = DateTime.Now;
            _footer = footer;
            _disclaimer = disclaimer;
            _totalLabel = totalLabel;
            _pageSize = PageSize.LETTER;
        }

        public ReportBase(IPdfLauncher pdfLauncher)
        {
            PdfLauncher = pdfLauncher;
        }
        #endregion

        #region Protected Methods
        protected virtual void DrawLine(PdfPTable linestable)
        {
            WriteCell(linestable, StringUtilities.fillString("_", 195), ReportFont, 21, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
        }

        protected void WritePhrase(PdfPTable table, Phrase phrase, Font font, int colSpan, int horizontalAlignment, int verticalAlignment, int border)
        {
            PdfPCell cell = new PdfPCell();
            cell.AddElement(phrase);
            cell.Colspan = colSpan;
            cell.HorizontalAlignment = horizontalAlignment;
            cell.Border = border;
            table.AddCell(cell);
        }

        protected void WriteCell(PdfPTable table, string cellvalue, Font font, int colSpan, int horizontalAlignment, int verticalAlignment, int border)
        {
            PdfPCell cell = new PdfPCell();
            cell = new PdfPCell(new Phrase(cellvalue, font));
            cell.Colspan = colSpan;
            cell.HorizontalAlignment = horizontalAlignment;
            cell.Border = border;
            table.AddCell(cell);
        }

        protected void WriteCell(PdfPTable table, string cellvalue, Font font, int colSpan, int horizontalAlignment, int border)
        {
            PdfPCell cell = new PdfPCell();

            cell = new PdfPCell(new Phrase(cellvalue, font));
            cell.Colspan = colSpan;
            cell.HorizontalAlignment = horizontalAlignment;
            cell.Border = border;
            table.AddCell(cell);
        }

        protected void WriteCell(PdfPTable table, string cellvalue, Font font, int colSpan, int horizontalAlignment, int border, bool backgroundcolor)
        {
            PdfPCell cell = new PdfPCell();

            cell = new PdfPCell(new Phrase(cellvalue, font));
            cell.Colspan = colSpan;
            cell.HorizontalAlignment = horizontalAlignment;
            if (backgroundcolor)
                cell.BackgroundColor = new BaseColor(System.Drawing.Color.LightGray);
            cell.Border = border;
            table.AddCell(cell);
        }

        protected PdfPCell WriteBlankCell(int colspan)
        {
            //generates a blank cell
            var blankCell = new PdfPCell(new Phrase(string.Empty));

            blankCell.Colspan = colspan;
            blankCell.Border = PdfPCell.NO_BORDER;
            blankCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            blankCell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;

            return (blankCell);
        }

        protected PdfPCell WriteBlankCellWithMinimumHeight(int colspan, int height)
        {
            //generates a blank cell with minimum height
            var blankCell = new PdfPCell(new Phrase(string.Empty));

            blankCell.Colspan = colspan;
            blankCell.Border = PdfPCell.NO_BORDER;
            blankCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            blankCell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            blankCell.MinimumHeight = height;

            return (blankCell);
        }

        protected void AuditReportHeader(PdfPTable headingtable, AuditReportHeaderFields headerFields)
        {
            Image logo = Image.GetInstance(global::Common.Properties.Resources.logo, BaseColor.WHITE);
            //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
            logo.ScalePercent(25);

            PdfPCell cell = new PdfPCell();
            //  row 1
            cell = new PdfPCell(logo);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(string.Empty, ReportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 6;
            headingtable.AddCell(cell);

            WriteCell(headingtable, string.Empty, ReportFontMedium, 5, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, headerFields.ReportStore, ReportFontRegular, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(headingtable, string.Empty, ReportFont, 5, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Shop Number: #" + headerFields.StoreNumber.ToString(), ReportFontRegular, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(headingtable, string.Empty, ReportFont, 5, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Inventory Audit Date: " + headerFields.InventoryAuditDate.ToShortDateString(), ReportFontRegular, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(headingtable, string.Empty, ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, headerFields.ReportTitle, ReportFontHeading, 2, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, "Report #: " + headerFields.ReportNumber.ToString(), ReportFontRegular, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER, false);

            WriteCell(headingtable, string.Empty, ReportFont, 7, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);
            WriteCell(headingtable, string.Empty, ReportFont, 7, Element.ALIGN_CENTER, Rectangle.NO_BORDER, false);

        }

        protected static Process AdobeReaderOpen()
        {
            Process process = null;

            foreach (Process currentProcess in Process.GetProcesses())
            {
                if (currentProcess.ProcessName.ToUpper().Contains("ACRORD"))
                {
                    process = currentProcess;
                }
            }

            return process;
        }

        protected void OpenFile(string filePath)
        {
            PdfLauncher.ShowPDFFile(filePath, false);
        }

        protected virtual void printGroupFooter(PdfPTable table, string lastGroup, int group_count)
        {
            string groupFooter;

            if (showGroupCount)
                groupFooter = String.Format(GroupByFooterTitle + "\t{1}", lastGroup, group_count);
            else
                groupFooter = String.Format(GroupByFooterTitle, lastGroup);


            PdfPCell cell = new PdfPCell(new Phrase(groupFooter, _reportFont));
            cell.Border = Rectangle.TOP_BORDER;
            cell.Colspan = 1;// table.NumberOfColumns - nrTotalFields;
            cell.NoWrap = true;
            cell.PaddingBottom = 13f;
            table.AddCell(cell);


            for (int i = 1; i < headers.Count; i++)
            {
                heading h = headers[i];

                string value = String.Empty;

                if (h.showTotal)
                {
                    value = String.Format(h.format, h.groupTotal);
                    h.groupTotal = 0;

                    headers[i] = h;

                    cell = new PdfPCell(new Phrase(value, _reportFont));
                    cell.HorizontalAlignment = h.field_align;
                    cell.Border = Rectangle.TOP_BORDER;
                    cell.PaddingBottom = 13f;

                    table.AddCell(cell);
                }

                else
                {
                    cell = new PdfPCell(new Phrase("", _reportFont));
                    cell.HorizontalAlignment = h.field_align;
                    cell.Border = Rectangle.TOP_BORDER;
                    cell.PaddingBottom = 13f;

                    table.AddCell(cell);
                }


            }
        }


        protected virtual void PrintReportDetailDisclaimer(PdfPTable table)
        {

            Paragraph paragraph = new Paragraph("***End of Report***", _reportFont);
            PdfPCell cell = new PdfPCell(paragraph);
            cell.Border = Rectangle.TOP_BORDER;
            cell.BorderWidthTop = 2;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = headers.Count;
            cell.PaddingTop = 15;
            table.AddCell(cell);

        }

        protected virtual void PrintReportHeader(PdfPTable table, Image gif, bool showTitle = true)
        {
            PdfPCell cell = new PdfPCell();
            PdfPTable tmpTable = new PdfPTable(1);
            PdfPTable hdrTable = new PdfPTable(3);
            hdrTable.SetWidthPercentage(new float[]
            {
                    15F, 70F, 30F
            }, this._pageSize);
            //hdrTable.WidthPercentage = 1.3f;
            //left header

            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            //cell.PaddingLeft = -10;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(reportType, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            if (showReportDate)
            {
                cell = new PdfPCell(new Paragraph("Report Date: " + this.reportObject.ReportParms[0].ToString(), _reportFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                tmpTable.AddCell(cell);
            }
            cell = new PdfPCell(tmpTable);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 2;

            hdrTable.AddCell(cell);



            // right header
            tmpTable = new PdfPTable(1);
            cell = new PdfPCell(new Paragraph(reportObject.ReportStoreDesc, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.NoWrap = true;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Run Date: " + _ShopTime.ToString("MM/dd/yyyy HH:mm"), _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            if (reportObject.ReportNumber > 0)
            {
                cell = new PdfPCell(new Paragraph("Report # " + reportObject.ReportNumber, _reportFont));
            }

            else
            {
                cell = new PdfPCell();
            }

            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            tmpTable.AddCell(cell);

            cell = new PdfPCell(tmpTable);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            //cell.PaddingLeft = 40;

            cell.Colspan = 1;
            hdrTable.AddCell(cell);


            // center header

            if (showTitle)
            {
                cell = new PdfPCell(new Paragraph(reportObject.ReportTitle));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.PaddingLeft = -10;
                cell.PaddingBottom = 20;
                cell.Colspan = 3;
                hdrTable.AddCell(cell);
            }

            cell = new PdfPCell(hdrTable);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.PaddingLeft = -10;
            if (showTitle)
                cell.PaddingBottom = 20;
            else
            {

            }
            cell.Colspan = headers.Count;
            table.AddCell(cell);


        }

        protected virtual void PrintReportFooter(PdfPTable table)
        {
            string value = String.Format(_totalLabel);
            PdfPCell cell = new PdfPCell(new Phrase(value, _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.TOP_BORDER;
            cell.PaddingTop = 10f;
            cell.NoWrap = true;

                

            table.AddCell(cell);

            for (int i = 1; i < headers.Count; i++)
            {
                heading h = headers[i];

                value = String.Empty;

                if (h.showTotal)
                {
                    value = String.Format(h.format, h.total);
                }

                cell = new PdfPCell(new Phrase(value, _reportFont));
                cell.HorizontalAlignment = h.field_align;
                cell.Border = Rectangle.TOP_BORDER;
                cell.PaddingTop = 10f;

                table.AddCell(cell);
            }
        }

        

        #endregion

        #region Public Audit Reports Methods
        public void AuditReportSectionDivider(PdfPTable sectionTable, int colspan, string sectionText, int horizontalAlignment, Font reportFont)
        {
            PdfPCell cell = new PdfPCell(new Phrase(sectionText, reportFont));
            cell.Colspan = colspan;
            cell.HorizontalAlignment = horizontalAlignment;
            cell.BackgroundColor = new BaseColor(System.Drawing.Color.LightGray);
            cell.Border = Rectangle.NO_BORDER;
            sectionTable.AddCell(cell);
        }
        #endregion

        #region Public Methods
        public bool CreateReport(DataTable rows)
        {
            bool isSuccessful = false;

            if (rows == null)
            {
                reportObject.ReportError = ReportConstants.NODATA;
                reportObject.ReportErrorLevel = (int)LogLevel.INFO;
                return false;
            }

            iTextSharp.text.Document document = new iTextSharp.text.Document(_pageSize);

            try
            {
                //set up RunReport event overrides & create doc
                string reportFileName = Path.GetFullPath(reportObject.ReportTempFileFullName);
                if (!Directory.Exists(Path.GetDirectoryName(reportFileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(reportFileName));
                }
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportFileName, FileMode.Create));
                MyPageEvents eventMgr = new MyPageEvents
                {
                    footer = _footer,
                    disclaimer = _disclaimer
                };
                writer.PageEvent = eventMgr;

                PdfPTable table = new PdfPTable(headers.Count);
                Image gif = Image.GetInstance(Common.Properties.Resources.logo, BaseColor.WHITE);
                gif.ScalePercent(35);

                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);

                document.AddTitle(reportObject.ReportTitle);

                document.SetPageSize(_pageSize);
                document.SetMargins(-50, -50, 10, 45);

                PrintReportHeader(table, gif);

                // Print Headings
                columnWidths = new float[headers.Count];
                for (int i = 0; i < headers.Count; i++)
                    columnWidths[i] = headers[i].width;

                table.SetWidthPercentage(columnWidths, _pageSize);


                foreach (heading h in headers)
                {
                    int alignment = (h.header_align >= 0) ? h.header_align : h.align;
                    PdfPCell cell = new PdfPCell(new Paragraph(h.text, _reportFont))
                    {
                        Border = Rectangle.BOTTOM_BORDER,
                        HorizontalAlignment = alignment,
                        VerticalAlignment = Element.ALIGN_TOP,
                    };
                    table.AddCell(cell);
                }

                table.HeaderRows = table.Rows.Count;



                int group_count = 0; // only used if report has a groupByTitle & groupBy Fields
                string lastGroup = "";
                string nxtGroup = "";
                bool breakGroup = false;
                int nrGroupByFields = 0;


                // Print Report Details  
                for (int rowIter = 0; rowIter < rows.Rows.Count; rowIter++)
                {
                    DataRow row = rows.Rows[rowIter];

                    for (int i = 0; i < headers.Count; i++)
                    {
                        heading h = headers[i];

                        int alignment = (h.field_align >= 0) ? h.field_align : h.align;                       

                        if (!rows.Columns.Contains(h.fieldName))
                        {
                            reportObject.ReportError = "Application Field (" + h.fieldName +
                                                       ") not in the returned data.";
                            reportObject.ReportErrorLevel = (int)LogLevel.FATAL;
                            throw new Exception("FieldConfig");
                        }

                        string value = "";
                        
                        if (h.Translator != null)
                            value = h.Translator(row[h.fieldName]);
                        else
                            value = String.Format(h.format, row[h.fieldName]);

                        PdfPCell cell = new PdfPCell(new Phrase(value, _reportFont));

                        if (h.formatICN)
                            cell = this.GetFormattedICNCell(value);

                        cell.HorizontalAlignment = alignment;
                        cell.Border = Rectangle.NO_BORDER;

                        table.AddCell(cell);


                        if (h.groupBy)
                        {
                            group_count++;

                            if (lastGroup.Length == 0)
                            {
                                nrGroupByFields++;
                                lastGroup = row[h.fieldName].ToString();
                            }

                            if (rowIter + 1 < rows.Rows.Count &&
                                !lastGroup.Equals(rows.Rows[rowIter + 1][h.fieldName].ToString()))
                            {
                                breakGroup = true;
                                nxtGroup = rows.Rows[rowIter + 1][h.fieldName].ToString();
                            }

                        }


                        if (h.showTotal)
                        {
                            decimal ttl = 0;
                            bool didParse = decimal.TryParse(row[h.fieldName].ToString(), out ttl);
                            if (didParse)
                            {
                                h.total += ttl;
                                h.groupTotal += ttl;

                                headers[i] = h;

                                if (rowIter == 0)
                                    nrTotalFields++;
                            }

                        }


                    }


                    if (breakGroup)
                    {
                        printGroupFooter(table, lastGroup, group_count);
                        group_count = 0;

                        lastGroup = nxtGroup;
                        breakGroup = false;

                    }
                }

                if (!string.IsNullOrEmpty(GroupByFooterTitle) && nrGroupByFields > 0)
                {
                    printGroupFooter(table, lastGroup, group_count);
                }


                if (!String.IsNullOrEmpty(_totalLabel))
                {
                    PrintReportFooter(table);
                }

                table.AddCell(new PdfPCell { Colspan = headers.Count, Border = Rectangle.NO_BORDER });
                new RunReport().ReportLines(table, true, "", true, _reportFont); // End of Report message

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
            catch (Exception e)
            {
                reportObject.ReportError = e.Message;
                reportObject.ReportErrorLevel = (int)LogLevel.ERROR;
            }

            return isSuccessful;
        }

        public PdfPCell GetFormattedICNCell(string icn)
        {
            int lengthICN = icn.Length;
            PdfPCell cell = new PdfPCell();
            cell.Border = 0;
            cell.HorizontalAlignment = (int)Element.ALIGN_LEFT;
            if (lengthICN != 18)
                return cell;
            string firstFIve = icn.Substring(0, 5);
            string afterFirstFIve = icn.Substring(5, 1);
            string nextSix = icn.Substring(6, 6);
            string nextOne = icn.Substring(12, 1);
            string threeAfterNextOne = icn.Substring(13, 3);
            string lastTwo = icn.Substring(icn.Length - 2, 2);

            Phrase Spacing = new Phrase(" ", ReportFont);
            Phrase phraseFirstFive = new Phrase(firstFIve, ReportFont);
            Phrase phraseAfterFirstFIve = new Phrase(afterFirstFIve, ReportFont);
            Phrase phraseNextSix = new Phrase(nextSix, ReportFontBold);
            Phrase phraseNextOne = new Phrase(nextOne, ReportFont);
            Phrase phraseThreeAfterNextOne = new Phrase(threeAfterNextOne, ReportFontBold);
            Phrase phraseLastTwo = new Phrase(lastTwo, ReportFont);

            phraseFirstFive.Add(Spacing);
            phraseFirstFive.Add(phraseAfterFirstFIve);
            phraseFirstFive.Add(Spacing);
            phraseFirstFive.Add(phraseNextSix);
            phraseFirstFive.Add(Spacing);
            phraseFirstFive.Add(phraseNextOne);
            phraseFirstFive.Add(Spacing);
            phraseFirstFive.Add(phraseThreeAfterNextOne);
            phraseFirstFive.Add(Spacing);
            phraseFirstFive.Add(phraseLastTwo);
            phraseFirstFive.Add(Spacing);
            cell.AddElement(phraseFirstFive);
            cell.VerticalAlignment = (int)Element.ALIGN_TOP;
            return cell;
        }

        public Phrase CombinePhrases(string boldFontPhrase, string regularFontPhrase)
        {
            Phrase returnPhrase = new Phrase("", ReportFontMedium);
            Phrase boldPhrase = new Phrase(boldFontPhrase, ReportFontBold);
            Phrase Spacing = new Phrase("  ", ReportFontSmall);
            Phrase regularPhrase = new Phrase(regularFontPhrase, ReportFontMedium);
            returnPhrase.Add(boldPhrase);
            returnPhrase.Add(Spacing);
            returnPhrase.Add(regularPhrase);
            return returnPhrase;
        }

        public Phrase GetFormattedICNPhrase(string icn)
        {
            string firstFIve = icn.Substring(0, 5);
            string afterFirstFIve = icn.Substring(5, 1);
            string nextSix = icn.Substring(6, 6);
            string nextOne = icn.Substring(12, 1);
            string threeAfterNextOne = icn.Substring(13, 3);
            string lastTwo = icn.Substring(icn.Length - 2, 2);

            Phrase Spacing = new Phrase(" ", ReportFont);

            Phrase phraseFirstFive = new Phrase(firstFIve, ReportFont);
            if (icn.Length != 18)
                return phraseFirstFive;
            Phrase phraseAfterFirstFIve = new Phrase(afterFirstFIve, ReportFont);
            Phrase phraseNextSix = new Phrase(nextSix, ReportFontBold);
            Phrase phraseNextOne = new Phrase(nextOne, ReportFont);
            Phrase phraseThreeAfterNextOne = new Phrase(threeAfterNextOne, ReportFontBold);
            Phrase phraseLastTwo = new Phrase(lastTwo, ReportFont);

            phraseFirstFive.Add(Spacing);
            phraseFirstFive.Add(phraseAfterFirstFIve);
            phraseFirstFive.Add(Spacing);
            phraseFirstFive.Add(phraseNextSix);
            phraseFirstFive.Add(Spacing);
            phraseFirstFive.Add(phraseNextOne);
            phraseFirstFive.Add(Spacing);
            phraseFirstFive.Add(phraseThreeAfterNextOne);
            phraseFirstFive.Add(Spacing);
            phraseFirstFive.Add(phraseLastTwo);
            phraseFirstFive.Add(Spacing);
            return phraseFirstFive;
        }

        public PdfPCell GetFormattedICNCellSmallFont(string icn)
        {
            int lengthICN = icn.Length;
            PdfPCell cell = new PdfPCell();
            cell.Border = 0;
            cell.HorizontalAlignment = (int)Element.ALIGN_LEFT;
            if (lengthICN != 18)
                return cell;
            string firstFIve = icn.Substring(0, 5);
            string afterFirstFIve = icn.Substring(5, 1);
            string nextSix = icn.Substring(6, 6);
            string nextOne = icn.Substring(12, 1);
            string threeAfterNextOne = icn.Substring(13, 3);
            string lastTwo = icn.Substring(icn.Length - 2, 2);

            Phrase Spacing = new Phrase(" ", ReportFontSmall);
            Phrase phraseFirstFive = new Phrase(firstFIve, ReportFontSmall);
            Phrase phraseAfterFirstFIve = new Phrase(afterFirstFIve, ReportFontSmall);
            Phrase phraseNextSix = new Phrase(nextSix, ReportFontSmallBold);
            Phrase phraseNextOne = new Phrase(nextOne, ReportFontSmall);
            Phrase phraseThreeAfterNextOne = new Phrase(threeAfterNextOne, ReportFontSmallBold);
            Phrase phraseLastTwo = new Phrase(lastTwo, ReportFontSmall);

            phraseFirstFive.Add(Spacing);
            phraseFirstFive.Add(phraseAfterFirstFIve);
            phraseFirstFive.Add(Spacing);
            phraseFirstFive.Add(phraseNextSix);
            phraseFirstFive.Add(Spacing);
            phraseFirstFive.Add(phraseNextOne);
            phraseFirstFive.Add(Spacing);
            phraseFirstFive.Add(phraseThreeAfterNextOne);
            phraseFirstFive.Add(Spacing);
            phraseFirstFive.Add(phraseLastTwo);
            phraseFirstFive.Add(Spacing);
            cell.AddElement(phraseFirstFive);

            return cell;
        }

        #endregion

        #region Helper Classes
        class MyPageEvents : PdfPageEventHelper
        {
            BaseFont bf = null;
            PdfContentByte cb;
            PdfTemplate template;
            public string disclaimer { set; get; }

            public string footer { set; get; }

            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                template = cb.CreateTemplate(50, 50);
            }

            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                template.BeginText();
                template.SetFontAndSize(bf, 8);
                template.ShowText((writer.PageNumber - 1).ToString());
                template.EndText();
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                string sPadding = string.Empty;

                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb.BeginText();
                cb = writer.DirectContent;

                int pageN = writer.PageNumber;

                sPadding = sPadding.PadLeft((int)Math.Truncate(document.PageSize.Width / 3 + document.RightMargin / 3), ' ');

                string text = footer
                              + sPadding
                              + "  Page " + pageN + " of ";

                float len = bf.GetWidthPoint(text, 8);

                cb.SetFontAndSize(bf, 8);
                //cb.SetTextMatrix(20, 60);
                cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, disclaimer, document.PageSize.Width / 2, 60, 0);
                //cb.ShowText(disclaimer);
                cb.SetTextMatrix(20, 20);
                cb.ShowText(text);
                cb.EndText();
                cb.AddTemplate(template, 23 + len, 20);
                cb.SetFontAndSize(bf, 8);
                //       cb.SetTextMatrix(280, 820);

            }
        }
        #endregion


    }
}
