/***********************************************************************************************************
* Namespace: PawnReports.Reports
* FileName: GunBookUtility
* Prints the gun book utility report using itextsharp
* Sreelatha Rengarajan 6/14/2010 Initial version 
* SR 6/17/2010 Added fix for page number and the issue of store name getting cut off
* SR 6/24/2010 Fixed the issue of serial number going over the next field
* SR 8/19/2010 Fixed the issue wherein the page number was not reflective of the gun_page column value
* TM 9/3/2010  Added Agency to the dispensation section of report data (defect #781)
***********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Common.Controllers.Application;
using Common.Libraries.Objects;
using Common.Libraries.Utility.Logger;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Utilities = Common.Libraries.Utility.Utilities;

namespace Reports
{
    public class GunBookUtility : ReportBase
    {
        public ReportObject RptObject;
        public DataTable GunBookData;
        public static string ReportTitle;

        private Font rptFont;
        private Font rptLargeFont;
        private Document document;
        private int gunPageNumber = 1;
        private int pageNum = 1;
        private bool FillUpPages = false;

        #region Constructor
        public GunBookUtility(IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {
        }
        #endregion

        public bool Print()
        {                       
            ReportTitle = RptObject.ReportTitle;
            if (RptObject.ReportParms != null && RptObject.ReportParms.Count > 0)
            {
                if (RptObject.ReportParms[0].ToString() == "Open" || RptObject.ReportParms[0].ToString() == "Closed")
                    FillUpPages = true;
            }            
            rptLargeFont = FontFactory.GetFont("Microsoft Sans Serif", 10, iTextSharp.text.Font.BOLD);
            try
            {
                document = new Document(PageSize.LEGAL.Rotate());

                //Set Font for the report
                rptFont = rptFont = FontFactory.GetFont("Microsoft Sans Serif", 6, iTextSharp.text.Font.NORMAL);

                string rptFileName = RptObject.ReportTempFileFullName;
                if (!string.IsNullOrEmpty(rptFileName))
                {
                    document.AddTitle(RptObject.ReportTitle);
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rptFileName, FileMode.Create));
                    PdfPTable table = new PdfPTable(31);
                    document.SetPageSize(PageSize.LEGAL.Rotate());
                    document.SetMargins(-100, -100, 10, 50);
                    GunBookEvents eventdata = new GunBookEvents();
                    writer.PageEvent = eventdata;
                    eventdata.PageNumber = new List<int>();
                    table.TotalWidth = 1500f;
                    //Insert the report header
                    InsertReportHeader(table);

                    //Create Column headers
                    InsertColumnHeaders(table);

                    //Set number of header rows
                    table.HeaderRows = 6;
                    //Insert report data

                    document.Open();
                    bool firstRow = true;
                    foreach (DataRow dr in GunBookData.Rows)
                    {
                        gunPageNumber = Utilities.GetIntegerValue(Utilities.GetStringValue(dr["GUN_PAGE"], ""));
                        if (!FillUpPages)
                        {
                            if (firstRow)
                            {
                                eventdata.PageNumber.Add(gunPageNumber);
                                pageNum = gunPageNumber;
                                firstRow = false;
                            }
                            else
                            {
                                if (gunPageNumber != pageNum)
                                {
                                    eventdata.PageNumber.Add(gunPageNumber);
                                    document.Add(table);
                                    table.DeleteBodyRows();
                                    document.NewPage();
                                    pageNum = gunPageNumber;
                                }
                            }
                        }
                        PdfPCell pCell = new PdfPCell();

                        pCell.Colspan = 1;
                        pCell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        pCell.Border = Rectangle.BOX;
                        pCell.Phrase = new Phrase(Utilities.GetStringValue(dr["GUN_NUMBER"]), rptFont);
                        table.AddCell(pCell);
                        //-----
                        string manufr = Utilities.GetStringValue(dr["MANUFACTURER"]);
                        string importr = Utilities.GetStringValue(dr["IMPORTER"]);

                        PdfPTable man_import = new PdfPTable(1);
                        if (!string.IsNullOrEmpty(manufr))
                            man_import.AddCell(new Phrase(manufr, rptFont));
                        if (!string.IsNullOrEmpty(importr))
                            man_import.AddCell(new Phrase(importr, rptFont));
                        PdfPCell pcell0 = new PdfPCell(man_import);
                        pcell0.Colspan = 2;
                        table.AddCell(pcell0);
                        //----

                        pCell.Phrase = new Phrase(Utilities.GetStringValue(dr["MODEL"]), rptFont);
                        pCell.Colspan = 2;
                        pCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.AddCell(pCell);

                        pCell.Phrase = new Phrase(Utilities.GetStringValue(dr["SERIAL_NUMBER"]), rptFont);
                        pCell.NoWrap = false;
                        pCell.Colspan = 2;
                        table.AddCell(pCell);

                        pCell.Phrase = new Phrase(Utilities.GetStringValue(dr["CALIBER"]), rptFont);
                        pCell.Colspan = 2;
                        table.AddCell(pCell);

                        pCell.Phrase = new Phrase(Utilities.GetStringValue(dr["GUN_TYPE"]), rptFont);
                        pCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        pCell.Colspan = 2;
                        table.AddCell(pCell);

                        //Acquire data
                        string transType = Utilities.GetStringValue(dr["ACQUIRE_TRANSACTION_TYPE"], "");
                        string transNumber = Utilities.GetStringValue(dr["ACQUIRE_DOCUMENT_NUMBER"], "");
                        pCell.Phrase = new Phrase(transType + " " + transNumber , rptFont);
                        pCell.NoWrap = true;
                        pCell.Colspan = 2;
                        table.AddCell(pCell);

                        string tmpAcqDate = Utilities.GetStringValue(dr["ACQUIRE_DATE"], "");
                        if (string.IsNullOrEmpty(tmpAcqDate))
                            tmpAcqDate = "";
                        else
                            tmpAcqDate = Utilities.GetDateTimeValue(tmpAcqDate).ToShortDateString();
                        pCell.Phrase = new Phrase(tmpAcqDate, rptFont);
                        pCell.Colspan = 2;
                        table.AddCell(pCell);

                        string acqFirstName = Utilities.GetStringValue(dr["ACQUIRE_FIRST_NAME"], "");
                        string acqLastName = Utilities.GetStringValue(dr["ACQUIRE_LAST_NAME"], "");

                        string acqIdType = Utilities.GetStringValue(dr["ACQUIRE_ID_TYPE"], "");
                        string acqIdNumber = Utilities.GetStringValue(dr["ACQUIRE_ID_NUMBER"], "");
                        string acqIdAgency = Utilities.GetStringValue(dr["ACQUIRE_ID_AGENCY"], "");

                        PdfPTable nameID = new PdfPTable(1);
                        if (!string.IsNullOrEmpty(acqFirstName) || !string.IsNullOrEmpty(acqLastName))
                            nameID.AddCell(new Phrase(acqFirstName + " " +
                                Utilities.GetStringValue(dr["ACQUIRE_MIDDLE_INITIAL"], "") + " " + 
                                acqLastName, rptFont));
                        if (!string.IsNullOrEmpty(acqIdType) || !string.IsNullOrEmpty(acqIdNumber))
                            nameID.AddCell(new Phrase(acqIdType + " " + acqIdAgency + acqIdNumber, rptFont));
                        PdfPCell pcell1 = new PdfPCell(nameID);
                        pcell1.Colspan = 3;
                        table.AddCell(pcell1);

                        string acqAddress = Utilities.GetStringValue(dr["ACQUIRE_ADDRESS"], "");
                        string acqCity = Utilities.GetStringValue(dr["ACQUIRE_CITY"], "");
                        string acqState = Utilities.GetStringValue(dr["ACQUIRE_STATE"], "");
                        string acqPostalCode = Utilities.GetStringValue(dr["ACQUIRE_POSTAL_CODE"], "");
                        PdfPTable address = new PdfPTable(1);
                        if (!string.IsNullOrEmpty(acqAddress))
                            address.AddCell(new Phrase(acqAddress, rptFont));
                        if (!string.IsNullOrEmpty(acqCity) || !string.IsNullOrEmpty(acqState) || !string.IsNullOrEmpty(acqPostalCode))
                            address.AddCell(new Phrase(acqCity + " " + acqState + " " + acqPostalCode, rptFont));
                        PdfPCell pcell = new PdfPCell(address);
                        pcell.Colspan = 3;
                        table.AddCell(pcell);

                        //Disposition Data
                        string dispTransType = Utilities.GetStringValue(dr["DISPOSITION_TRANSACTION_TYPE"], "");
                        string dispTransNumber = Utilities.GetStringValue(dr["DISPOSITION_DOCUMENT_NUMBER"], "");
                        dispTransNumber = dispTransNumber == "0" ? "" : dispTransNumber;
                        
                        pCell.Phrase = new Phrase(dispTransType + " "
                                                  + dispTransNumber, rptFont);
                        pCell.Colspan = 2;
                        pCell.NoWrap = true;
                        table.AddCell(pCell);

                        // Disp Trans Date
                        string tmpDispDate = Utilities.GetStringValue(dr["DISPOSITION_DATE"], "");
                        if (string.IsNullOrEmpty(tmpDispDate))
                            tmpDispDate = "";
                        else
                            tmpDispDate = Utilities.GetDateTimeValue(tmpDispDate).ToShortDateString();
                        pCell.Phrase = new Phrase(tmpDispDate, rptFont);
                        pcell.Colspan = 2;
                        table.AddCell(pCell);

                        // Disp Name and ID
                        string dispFirstName = Utilities.GetStringValue(dr["DISPOSITION_FIRST_NAME"], "");
                        string dispLastName = Utilities.GetStringValue(dr["DISPOSITION_LAST_NAME"], "");

                        string dispIdType = Utilities.GetStringValue(dr["DISPOSITION_ID_TYPE"], "");
                        string dispIdAgency = Utilities.GetStringValue(dr["DISPOSITION_ID_AGENCY"], "");
                        string dispIdNumber = Utilities.GetStringValue(dr["DISPOSITION_ID_NUMBER"], "");
                        PdfPTable dispNameID = new PdfPTable(1);
                        if (!string.IsNullOrEmpty(dispFirstName) || !string.IsNullOrEmpty(dispLastName))
                            dispNameID.AddCell(new Phrase(dispFirstName + " " +
                                Utilities.GetStringValue(dr["DISPOSITION_MIDDLE_INITIAL"], "") + " " + 
                                dispLastName, rptFont));
                        if (!string.IsNullOrEmpty(dispIdType) || !string.IsNullOrEmpty(dispIdNumber))
                            dispNameID.AddCell(new Phrase(dispIdType + " " + dispIdAgency + dispIdNumber, rptFont));

                        //here add FFL
                        PdfPCell pcell2 = new PdfPCell(dispNameID);
                        pcell2.Colspan = 3;
                        table.AddCell(pcell2);

                        // Disp Address

                        string dispAddress = Utilities.GetStringValue(dr["DISPOSITION_ADDRESS"], "");
                        string dispCity = Utilities.GetStringValue(dr["DISPOSITION_CITY"], "");
                        string dispState = Utilities.GetStringValue(dr["DISPOSITION_STATE"], "");
                        string dispPostalCode = Utilities.GetStringValue(dr["DISPOSITION_POSTAL_CODE"], "");

                        PdfPTable dispAddressTable = new PdfPTable(1);
                        if (!string.IsNullOrEmpty(dispAddress))
                            dispAddressTable.AddCell(new Phrase(dispAddress, rptFont));
                        if (!string.IsNullOrEmpty(dispCity) || !string.IsNullOrEmpty(dispState) || !string.IsNullOrEmpty(dispPostalCode))
                            dispAddressTable.AddCell(new Phrase(dispCity + " " + dispState + " " + dispPostalCode, rptFont));
                        PdfPCell pcell3 = new PdfPCell(dispAddressTable);
                        pcell3.Colspan = 3;
                        table.AddCell(pcell3);
                    }

                    document.Add(table);
                    document.Close();
                    //OpenFile(rptFileName);
                    //Print();
                    return true;
                }
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Gun Book printing failed since file name is not set");
                return false;
            }
            catch (Exception de)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Gun Book printing" + de.Message);
                return false;
            }
        }

        private void InsertColumnHeaders(PdfPTable headingtable)
        {
            PdfPCell cell = new PdfPCell();
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.Colspan = 1;
            //First Row of column header
            cell = new PdfPCell(new Paragraph(" ", rptFont));
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("DESCRIPTION OF FIREARM", rptLargeFont));
            cell.Colspan = 10;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("RECEIPT", rptLargeFont));
            cell.Colspan = 10;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("DISPOSITION", rptLargeFont));
            cell.Colspan = 10;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            headingtable.AddCell(cell);

            //Second row of column header
            cell = new PdfPCell(new Paragraph("Gun", rptFont));
            cell.Colspan = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Manufacturer", rptFont));
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Model", rptFont));
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Serial", rptFont));
            cell.Colspan = 2;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Gauge or", rptFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Type", rptFont));
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Transaction", rptFont));
            cell.Colspan = 4;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Name", rptFont));
            cell.Colspan = 3;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Address", rptFont));
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Transaction", rptFont));
            cell.Colspan = 4;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Name", rptFont));
            cell.Colspan = 3;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Address", rptFont));
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            //3rd row of header
            cell = new PdfPCell(new Paragraph("Number", rptFont));
            cell.Colspan = 1;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.NoWrap = true;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Importer", rptFont));
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(" ", rptFont));
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Number", rptFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Caliber", rptFont));
            cell.Colspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(" ", rptFont));
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 2;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Type/Number", rptFont));
            cell.Colspan = 2;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Date", rptFont));
            cell.Colspan = 2;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("ID", rptFont));
            cell.Colspan = 3;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(" ", rptFont));
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 3;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Type/Number", rptFont));
            cell.Colspan = 2;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("Date", rptFont));
            cell.Colspan = 2;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph("ID", rptFont));
            cell.Colspan = 3;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(" ", rptFont));
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 3;
            headingtable.AddCell(cell);
        }

        private void InsertReportHeader(PdfPTable headingtable)
        {
            PdfPCell cell = new PdfPCell();

            cell = new PdfPCell(new Paragraph(RptObject.ReportStore + " " + RptObject.ReportStoreDesc, rptFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Colspan = 31;
            headingtable.AddCell(cell);

            /*  cell = new PdfPCell(new Paragraph("Page " + pageNum.ToString(), rptFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Colspan = 11;
            headingtable.AddCell(cell);*/

            cell = new PdfPCell(new Paragraph(DateTime.Now.ToString("MM/dd/yyyy hh:m:ss"), rptFont));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 4;
            headingtable.AddCell(cell);

            cell = new PdfPCell(new Paragraph(RptObject.ReportTitle, rptLargeFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Colspan = 27;
            headingtable.AddCell(cell);

            //empty line
            cell = new PdfPCell(new Phrase(""));
            cell.Colspan = 31;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            headingtable.AddCell(cell);
        }
    }

    class GunBookEvents : PdfPageEventHelper
    {
        BaseFont footerBaseFont = null;
        PdfContentByte contentByte;
        PdfTemplate template;
        public List<int> PageNumber;

        public override void OnOpenDocument(PdfWriter writer, Document doc)
        {
            try
            {
                footerBaseFont = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1252, BaseFont.EMBEDDED);
                contentByte = writer.DirectContent;
                template = contentByte.CreateTemplate(50, 50);
            }
            catch (DocumentException dex)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Gun Book printing" + dex.Message);
            }
            catch (IOException ioe)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Gun Book printing" + ioe.Message);
            }
        }

        public override void OnEndPage(PdfWriter writer, Document doc)
        {
            try
            {
                String text;

                if (doc.PageNumber - 1 < PageNumber.Count)
                {
                    text = "Page " + PageNumber[doc.PageNumber - 1];
                }
                else
                {
                    text = "Page " + doc.PageNumber; //PageNumber[doc.PageNumber - 1];
                }

                float len = footerBaseFont.GetWidthPoint(text, 10);
                Rectangle pageSize = doc.PageSize;
                contentByte.SetRGBColorFill(0, 0, 0);
                contentByte.BeginText();
                contentByte.SetFontAndSize(footerBaseFont, 8);
                contentByte.SetTextMatrix(pageSize.GetRight(100), pageSize.GetTop(25));
                contentByte.ShowText(text);
                contentByte.EndText();
                contentByte.AddTemplate(template, pageSize.GetRight(100) + len, pageSize.GetTop(25));
                
            }
            catch (DocumentException dex)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Gun Book printing" + dex.Message);
            }
            catch (IOException ioe)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Gun Book printing" + ioe.Message);
            }
        }
    }
}
