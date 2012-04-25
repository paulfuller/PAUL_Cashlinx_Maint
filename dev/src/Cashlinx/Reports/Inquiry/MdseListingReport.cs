using System;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Common.Libraries.Utility.Logger;

namespace Reports.Inquiry
{
    public class MdseListingReport : PdfPageEventHelper
    {
        //used by overriden methods for footers
        private PdfContentByte contentByte;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        //used by report
        private Font _reportFont;
        private Font _reportFontLargeBold;

        //main objects
        string _storeNr;
        string _storeName;
        DateTime _runDate;
        string _title;
        string reportTempFileFullName;

        public string ReportError;
        public int ReportErrorLevel;
        string _groupByField, _groupByName;

        public MdseListingReport(string report_name, string storeNumber, string storeName, DateTime runDate, string title, string groupByField, string groupByName)
        {
            _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
            _reportFontLargeBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);


            _storeNr = storeNumber;
            _storeName = storeName;
            _runDate = runDate;
            _title = title;
            _groupByField = groupByField;
            _groupByName = groupByName;

            reportTempFileFullName = report_name;

        }

        public bool CreateReport(System.Data.DataView theData)
        {
            bool isSuccessful = false;
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.LETTER.Rotate());
            try
            {

                //set up RunReport event overrides & create doc
                MdseListingReport events = this;

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(reportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                //set up tables, etc...
                PdfPTable table = new PdfPTable(12);
                PdfPCell cell = new PdfPCell();
                Image gif = Image.GetInstance(Resources.logo, BaseColor.WHITE);

                _reportFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL);
                _reportFontLargeBold = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);

                gif.ScalePercent(35);

                document.SetPageSize(PageSize.LETTER.Rotate());
                document.SetMargins(-10, 0, 10, 45);
                document.AddTitle("Inventory Listing Report : " + DateTime.Now.ToString("MM/dd/yyyy"));

                ReportHeader(table, gif);
                ColumnHeaders(table);
                table.SetWidthPercentage(new float[] {100F, 100F, 50F, 50F, 50F, 50F, 50F, 25F, 50F, 50F, 50F, 50F },
                        PageSize.LETTER.Rotate());

                table.HeaderRows = 6;


                ReportDetail(table, theData);

                ReportLines(table, true, "", true, _reportFont);
                document.Open();
                document.Add(table);
                document.Close();

                isSuccessful = true;

            }
            catch (DocumentException de)
            {
                ReportError = de.Message; ;
                ReportErrorLevel = (int)LogLevel.ERROR;
            }
            catch (IOException ioe)
            {
                ReportError = ioe.Message; ;
                ReportErrorLevel = (int)LogLevel.ERROR;
            }

            return isSuccessful;
        }

        private void ReportHeader(PdfPTable headingtable, Image gif)
        {

            PdfPCell cell = new PdfPCell();

            //  heading - row 1
            cell = new PdfPCell(gif);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 8;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(_storeName + "\n Store Nr: " + _storeNr, _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            //  heading - row 2
            cell = new PdfPCell(new Paragraph("Inquiry", _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingLeft = 12;
            cell.Colspan = 8;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(ReportHeaders.RUNDATE + DateTime.Now.ToString("MM/dd/yyyy"), _reportFont));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.PaddingTop = -15;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 4;
            headingtable.AddCell(cell);


            // heading - row 4
            string [] title = _title.Split(new string[] {"\n\n"},2,StringSplitOptions.RemoveEmptyEntries);

            cell = new PdfPCell(new Phrase(title[0], _reportFontLargeBold));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 12;
            cell.Rowspan = 1;
            headingtable.AddCell(cell);


            cell = new PdfPCell();
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 1;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase(title[1], _reportFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = 11;
            headingtable.AddCell(cell);


        }

        private void ColumnHeaders(PdfPTable headingtable)
        {

            ReportLines(headingtable, false, StringUtilities.fillString("-", 170), false, _reportFont);

            //set up tables, etc...
            PdfPCell cell = new PdfPCell();

            cell = new PdfPCell(new Phrase(_groupByName, _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            //row 1
            cell = new PdfPCell(new Phrase("ICN\nDescription", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Category", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Status", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Disp Type", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Disp Doc", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Disp Date", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Qty", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Item Amt", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Loan Amt", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);
            cell = new PdfPCell(new Phrase("PFI Amt", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Retail Amt", _reportFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.BOTTOM_BORDER;
            headingtable.AddCell(cell);


        }

        private void ReportDetail(PdfPTable pdfTable, System.Data.DataView theData)
        {
            PdfPCell cell = new PdfPCell();
            
            string last_group = "";
            string last_category = "";
            string groupField = _groupByField;

            if (groupField == "CAT_CODE")
                groupField = "CAT_DESC";

            if (theData.Count == 0)
            {
                cell = new PdfPCell(new Phrase("\n\n** No Qualifying Data Found **\n\n", _reportFont));
                cell.Colspan = 11;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                pdfTable.AddCell(cell);
            }

            else
                for (int i = 0; i < theData.Count; i++)
                {
                    System.Data.DataRowView mdse = theData[i];
                    if (last_group != mdse[_groupByField].ToString())
                    {
                        if (_groupByField == "CAT_CODE")
                        {
                            int cat_code = int.Parse(mdse["CAT_CODE"].ToString());
                            string top_category = "";

                            if  (cat_code >= 1000 && cat_code < 2000 ) top_category = "JEWELRY";

                            else if (cat_code >= 2000 && cat_code < 3000 ) top_category = "TOOLS";
                            else if (cat_code >= 3000 && cat_code < 4000 ) top_category = "ELECTRONICS";
                            else if (cat_code >= 4000 && cat_code < 5000 ) top_category = "FIREARMS";
                            else if (cat_code >= 5000 && cat_code < 6000 ) top_category = "CAMERAS, BINOCULARS, TELESCOPES";
                            else if (cat_code >= 6000 && cat_code < 7000 ) top_category = "MUSICAL INSTRUMENTS";
                            else if (cat_code >= 7000 && cat_code < 8000 ) top_category = "SPORTING GOODS";
                            else if (cat_code >= 8000 && cat_code < 9000 ) top_category = "HOME EQUIPMENT";
                            else if (cat_code >= 8000) top_category = "MISC.";

                            if (top_category != last_category)
                            {
                                cell = new PdfPCell();
                                cell.Colspan = 12;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;

                                cell.Border = Rectangle.NO_BORDER;
                                pdfTable.AddCell(cell);


                                cell = new PdfPCell(new Phrase(top_category, _reportFontLargeBold));
                                cell.Colspan = 12;
                                cell.Left = -20;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;

                                cell.Border = Rectangle.BOTTOM_BORDER;
                                pdfTable.AddCell(cell);

                                last_category = top_category;
                            }

                        }


                        cell = new PdfPCell();
                        cell.Colspan = 12;
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;

                        cell.Border = Rectangle.NO_BORDER;
                        pdfTable.AddCell(cell);



                        switch (groupField)
                        {
                            case "RETAIL_PRICE":
                            case "ITEM_AMT":
                                cell = new PdfPCell(new Phrase(string.Format("{0:c}", mdse[groupField]), _reportFont));
                                break;
                            case "STATUS_TIME":
                                cell = new PdfPCell(new Phrase(string.Format("{0:g}", mdse[groupField]), _reportFont));
                                break;
                            default:
                                cell = new PdfPCell(new Phrase(string.Format("{0}", mdse[groupField]), _reportFont));
                                break;
                        }
                        
                        cell.Colspan = 1;
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;

                        cell.Border = Rectangle.NO_BORDER;
                        pdfTable.AddCell(cell);

                        last_group = mdse[_groupByField].ToString();
                    }
                    else
                    {
                        cell = new PdfPCell();
                        cell.Colspan = 1;
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        cell.Border = Rectangle.NO_BORDER;
                        pdfTable.AddCell(cell);
                    }

                    cell = new PdfPCell(new Phrase(string.Format("{0}", mdse["ICN"]), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(string.Format("{0}", mdse["CAT_CODE"]), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(string.Format("{0}", mdse["STATUS_CD"]), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(string.Format("{0}", mdse["DISP_TYPE"]), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(string.Format("{0}", mdse["DISP_DOC"]), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(string.Format("{0:d}", mdse["DISP_DATE"]), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(string.Format("{0}", mdse["QUANTITY"]), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(string.Format("{0:c}", mdse["ITEM_AMT"]), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(string.Format("{0:c}", mdse["AMOUNT"]), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(string.Format("{0:c}", mdse["PFI_AMOUNT"]), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(string.Format("{0:c}", mdse["RETAIL_PRICE"]), _reportFont));
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Colspan = 1;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(string.Format("{0}", mdse["MD_DESC"]), _reportFont));
                    cell.Colspan = 11;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                    //BLANK LINE
                    cell = new PdfPCell(new Phrase("", _reportFont));
                    cell.Colspan = 12;
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    pdfTable.AddCell(cell);

                }
        }

        public void ReportLines(PdfPTable report, bool line, string stringline, bool endOfReport, Font font)
        {
            PdfPCell cell = new PdfPCell();
            //draw a single line
            if (line)
            {
                cell = new PdfPCell(new Phrase(""));
                cell.BorderWidthLeft = Rectangle.NO_BORDER;
                cell.BorderWidthTop = Rectangle.NO_BORDER;
                cell.BorderWidthRight = Rectangle.NO_BORDER;
                cell.Colspan = 21;

                report.AddCell(cell);
            }
            //draw a line from a string
            if (stringline.Length > 0)
            {
                cell = new PdfPCell(new Phrase(stringline));
                cell.Colspan = 21;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;

                report.AddCell(cell);
            }
            //print End of Report
            if (endOfReport)
            {
                cell = new PdfPCell(new Phrase("***End of Report***", font));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 21;
                report.AddCell(cell);
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
            string[] reportName = writer.Info.Get(PdfName.TITLE).ToString().Split(':');
            text = string.Format("{0}{1} Page {2} of ", reportName[0], StringUtilities.fillString(" ", 250), pageN);

            float len = footerBaseFont.GetWidthPoint(text, 8);

            contentByte.BeginText();
            contentByte.SetFontAndSize(footerBaseFont, 8);
            contentByte.SetTextMatrix(30, 30);
            contentByte.ShowText(text);
            contentByte.EndText();
            contentByte.AddTemplate(template, 30 + len, 30);
        }
    }
}
