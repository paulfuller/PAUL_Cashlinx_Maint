//***************************************************************
// class_name   : BuildDstrRpt
// created by   : rmcbai1
// date created : 1/14/2010 10:54:09 AM
//
// Copyright  2010 Cash America International
//---------------------------------------------------------------
//   description:  this class will create the Report #201 (MULTIPLE  
//                 SALE  OR  OTHER  DISPOSITION  OF  PISTOLS  AND  
//                 REVOLVERS) with a passed in dataset. The dataset
//                 contains two tables, one for the group header
//                 data and one for the detail data
//
//----------------------------------------------------------------
// change history
//3/31/2010 SR Changed column name Receipt to Transticketnum in TOPS transfer
//              Changed column name RESTI to RESTI_AMOUNT in claimant release  
//4/20/2010 SR Fixed formatting issues
//CR # 685 SMurphy 4/14/2010 multiple changes on fields displayed
//8/25/2010 TM Adding support for Purchase related sections
//bz97  1/18/2011   TMcConnell Resequenced DSTR sections per request
//****************************************************************

using System;
using System.Data;
using System.IO;
using Common.Controllers.Database.DataAccessLayer;
using Common.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;

namespace Reports.MAL
{
    public class BuildRpt
    {
        private Document _document;
        private Image jpeg;
        private string _rptFileName;
        private string _reportDir;
        private DateTime _reportDate;
        private DateTime _reportStartDate;
        private DataSetOutput _reportData;
        private PdfPTable _dataTable;
        private string _storeNumber;

        private ReportContext Context { get; set; }

        private PdfPCell _CellColumnRecord;

        private PdfContentByte _cb;

        public DataSetOutput ReportData
        {
            set { _reportData = value; }
        }

        public string StoreName
        {
            get { return Context.StoreName; }
        }
        public string ReportDir
        {
            get { return _reportDir; }
            set { _reportDir = value; }
        }

        public string RptFileName
        {
            get { return _rptFileName; }
            set { _rptFileName = value; }
        }
        public string ErrorCode
        {
            get { return Context.ErrorCode; }
        }

        public string ErrorText
        {
            get { return Context.ErrorText; }
        }

        #region contructors and intializers
        public BuildRpt(string StoreNum, DateTime dateTime, DataSetOutput reportData, string reportDir, string rptFileName)
        {
            Context = new ReportContext();
            _reportDate = dateTime;
            _reportData = reportData;
            _storeNumber = StoreNum;
            SetDefaults(reportDir, rptFileName);
        }

        public BuildRpt(string StoreNum, DateTime dateTime, DateTime startDate, DataSetOutput reportData, string reportDir, string rptFileName)
        {
            Context = new ReportContext();
            _reportDate = dateTime;
            _reportStartDate = startDate;
            _reportData = reportData;
            _storeNumber = StoreNum;
            SetDefaults(reportDir, rptFileName);
        }

        public BuildRpt(string StoreNum, DateTime dateTime, DataSetOutput reportData)
        {
            Context = new ReportContext();
            _reportDate = dateTime;
            _reportData = reportData;
            _storeNumber = StoreNum;
            SetDefaults(".\\", "rpt_096.pdf");
        }

        private void SetDefaults(string reportDir, string rptFileName)
        {
            _rptFileName = rptFileName;
            _reportDir = reportDir;
            Context.ErrorText = string.Empty;
            Context.ErrorCode = "OK";
        }

        #endregion

        //*************************************************
        //** Date created: Thursday, Jan 19, 2010
        //** Created by  : PAWN\rmcbai1
        //*************************************************
        /// <summary>
        ///    Here we setup the PDF file and call the methods
        ///    used to fill the report
        /// </summary>
        ///
        /// <returns> (bool) indicating success or failure</returns>
        //*************************************************
        public bool CreateRpt()
        {
            bool bRetVal;
            string fullName;

            PdfWriter writer;
            DataTable dataTable;

            if (_reportData == null)
            {
                Context.ErrorText = "No Data Found...";
                Context.ErrorCode = "1";
                return false;
            }

            jpeg = Image.GetInstance(Resources.logo, BaseColor.WHITE);
            Context.ErrorCode = "OK";
            Context.ErrorText = "OK";

            if (_reportData.GetOutputTable(out dataTable))
            {
                Context.StoreName = dataTable.Rows[0][1].ToString();
            }

            try
            {
                fullName = _reportDir + "\\" + _rptFileName;

                _document = new Document(PageSize.LEGAL.Rotate());
                _dataTable = new PdfPTable(1);
                _dataTable.WidthPercentage = 100;

                Context.Document = _document;
                Context.PdfTable = _dataTable;
                Context.ReportData = _reportData;

                var myFile = new FileStream(fullName, FileMode.Create);

                var events = new MyPageEvents();
                writer = PdfWriter.GetInstance(_document, myFile);
                writer.PageEvent = events;

                _dataTable.HeaderRows = 1;
                _dataTable.SplitLate = false;
                _document.Open();

                _cb = writer.DirectContent;

                // code here to do the different groups in the report
                PrintPageHeader();

                List<AbstractGroup> groups = new List<AbstractGroup>();
                groups.Add(new GunDispositionRpt(Context, "DISP_DATA"));

                //----------<bz0097-------------------


                foreach (AbstractGroup group in groups)
                {
                    if (group.DataLoaded)
                    {
                        group.BuildSection();
                    }
                }


                System.Data.DataTable dt;
                _reportData.GetTable("DISP_DATA", out dt);
                if (dt == null)
                {
                    _CellColumnRecord = new PdfPCell();
                    _CellColumnRecord.Border = Rectangle.NO_BORDER;
                    _CellColumnRecord.Phrase = new Phrase("NO DATA FOUND", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.BOLD));
                    _CellColumnRecord.HorizontalAlignment = Element.ALIGN_CENTER;
                    _dataTable.AddCell(_CellColumnRecord);
                }


                _CellColumnRecord = new PdfPCell();
                _CellColumnRecord.Border = Rectangle.NO_BORDER;
                _CellColumnRecord.Phrase = new Phrase(" ");
                _dataTable.AddCell(_CellColumnRecord);
                    
                

                _CellColumnRecord = new PdfPCell();
                _CellColumnRecord.Border = Rectangle.NO_BORDER;
                _CellColumnRecord.Phrase = new Phrase("***End of Report***", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.BOLD));
                _CellColumnRecord.HorizontalAlignment = Element.ALIGN_CENTER;
                _dataTable.AddCell(_CellColumnRecord);

                _document.Add(_dataTable);

                if (Context.HasData)
                {
                    _document.Close();
                }
                else
                {
                    _document.Close();
                    //Context.ErrorText = "No records found.";
                    myFile.Close();
                    //if (File.Exists(fullName))
                    //    File.Delete(fullName);
                }
                bRetVal = true;
            }
            catch (Exception ex)
            {
                Context.ErrorText = ex.Message;
                Context.ErrorCode = "1";
                bRetVal = false;
            }
            return bRetVal;
        }

        //*************************************************
        //** Date created: Tuesday, January 05, 2010
        //** Created by  : PAWN\rmcbai1
        //**************************************************
        /// <summary>
        ///    Create and print our Page header. I should 
        ///    probably find a way to do this via the overrides 
        ///    list below, but not today.
        /// </summary>
        ///
        //*************************************************
        private void PrintPageHeader()
        {
            try
            {
                jpeg.ScalePercent(40);

                Font fontHeader = new Font(FontFactory.GetFont(BaseFont.HELVETICA, 10, Font.BOLD));

                float[] headerwidths = { 15, 10, 40, 10, 20 };

                PdfPTable dataTable = new PdfPTable(5);
                dataTable.SetWidths(headerwidths);
                dataTable.WidthPercentage = 100;

                PdfPCell cellColumnHeader = new PdfPCell();
                cellColumnHeader.Border = Rectangle.NO_BORDER;
                cellColumnHeader.HorizontalAlignment = Element.ALIGN_LEFT;
                cellColumnHeader.VerticalAlignment = Element.ALIGN_BOTTOM;

                // Row 1
                PdfPCell cellImage = new PdfPCell(jpeg);
                cellImage.Colspan = 3;
                cellImage.HorizontalAlignment = Element.ALIGN_LEFT;
                cellImage.VerticalAlignment = Element.ALIGN_BOTTOM;
                cellImage.Border = Rectangle.NO_BORDER;
                dataTable.AddCell(cellImage);

                //cellColumnHeader.Colspan = 1;
                //cellColumnHeader.Phrase = new Phrase("Store ID: ", fontHeader);
                //dataTable.AddCell(cellColumnHeader);

                cellColumnHeader.Colspan = 2;
                cellColumnHeader.Phrase = new Phrase(StoreName, fontHeader);
                dataTable.AddCell(cellColumnHeader);

                // Row 2
                //cellColumnHeader.Colspan = 1;
                //cellColumnHeader.Phrase = new Phrase("Report Type: ", fontHeader);
                //dataTable.AddCell(cellColumnHeader);

                cellColumnHeader.Colspan = 3;
                cellColumnHeader.Phrase = new Phrase("Operational", fontHeader);
                dataTable.AddCell(cellColumnHeader);

                //cellColumnHeader.Colspan = 1;
                //cellColumnHeader.Phrase = new Phrase("Store Number: ", fontHeader);
                //dataTable.AddCell(cellColumnHeader);

                cellColumnHeader.Colspan = 2;
                cellColumnHeader.Phrase = new Phrase(_storeNumber, fontHeader);
                dataTable.AddCell(cellColumnHeader);

                /*
                cellColumnHeader.Colspan = 1;
                cellColumnHeader.Phrase = new Phrase("Run Date: ", fontHeader);
                dataTable.AddCell(cellColumnHeader);
                


                cellColumnHeader.Colspan = 1;
                cellColumnHeader.Phrase = new Phrase(DateTime.Now.ToShortDateString(), fontHeader);
                dataTable.AddCell(cellColumnHeader);
*/
                // Row 3
                cellColumnHeader.Colspan = 1;
                //cellColumnHeader.Phrase = new Phrase("Start Date: ", fontHeader);
                cellColumnHeader.Phrase = new Phrase(string.Empty, fontHeader);
                dataTable.AddCell(cellColumnHeader);

                cellColumnHeader.Colspan = 2;
                cellColumnHeader.HorizontalAlignment = Element.ALIGN_LEFT;
                //cellColumnHeader.Phrase = new Phrase(_reportStartDate.ToShortDateString(), fontHeader);
                cellColumnHeader.Phrase = new Phrase(string.Empty, fontHeader);
                
                dataTable.AddCell(cellColumnHeader);

                //cellColumnHeader.Colspan = 1;
                //cellColumnHeader.Phrase = new Phrase("Report #: ", fontHeader);
                //dataTable.AddCell(cellColumnHeader);

                cellColumnHeader.Colspan = 1;
                cellColumnHeader.Phrase = new Phrase("Run Date: ", fontHeader);
                dataTable.AddCell(cellColumnHeader);



                cellColumnHeader.Colspan = 1;
                cellColumnHeader.Phrase = new Phrase(DateTime.Now.ToShortDateString(), fontHeader);
                dataTable.AddCell(cellColumnHeader);
                

                // Row 4
                // Line
                //cellColumnHeader.Colspan = 3;
                //cellColumnHeader.Phrase = new Phrase("", fontHeader);
                //cellColumnHeader.Border = Rectangle.NO_BORDER;
                //dataTable.AddCell(cellColumnHeader);
                cellColumnHeader.Colspan = 2;
                //cellColumnHeader.Phrase = new Phrase("End Date: ", fontHeader);
                cellColumnHeader.Phrase = new Phrase("Reporting from " + _reportStartDate.ToShortDateString() + " through " + _reportDate.ToShortDateString(), fontHeader);
                dataTable.AddCell(cellColumnHeader);
                

                cellColumnHeader.Colspan = 1;
                cellColumnHeader.HorizontalAlignment = Element.ALIGN_LEFT;
                cellColumnHeader.Phrase = new Phrase(string.Empty, fontHeader);
                dataTable.AddCell(cellColumnHeader);

                cellColumnHeader.Colspan = 1;
                cellColumnHeader.Phrase = new Phrase("Report # 208", fontHeader);
                dataTable.AddCell(cellColumnHeader);
                cellColumnHeader.HorizontalAlignment = Element.ALIGN_CENTER;

                cellColumnHeader.Phrase = new Phrase("", fontHeader);
                dataTable.AddCell(cellColumnHeader);


                // Title
                cellColumnHeader.Colspan = 5;
                cellColumnHeader.Phrase = new Phrase("Gun Disposition", fontHeader);
                cellColumnHeader.Border = Rectangle.NO_BORDER;
                dataTable.AddCell(cellColumnHeader);

                // Line
                cellColumnHeader.Colspan = 5;
                cellColumnHeader.Phrase = new Phrase("", fontHeader);
                cellColumnHeader.Border = Rectangle.NO_BORDER;
                dataTable.AddCell(cellColumnHeader);

                cellColumnHeader.HorizontalAlignment = Element.ALIGN_LEFT;

                PdfPCell cellTable = new PdfPCell();
                cellTable.Border = Rectangle.NO_BORDER;
                cellTable.HorizontalAlignment = Element.ALIGN_LEFT;
                cellTable.VerticalAlignment = Element.ALIGN_BOTTOM;
                cellTable.Table = dataTable;

                _dataTable.AddCell(cellTable);
            }
            catch (Exception ex)
            {
                Context.ErrorText = ex.Message;
                Context.ErrorCode = "1";
                throw;
            }
            return;
        }
    }

    class MyPageEvents : PdfPageEventHelper
    {
        BaseFont bf = null;
        PdfContentByte cb;
        PdfTemplate template;

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
            sPadding = sPadding.PadLeft(170, ' ');

            string text = " Gun Disposition"
                          + sPadding
                          + "  Page " + pageN + " of ";

            float len = bf.GetWidthPoint(text, 8);

            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(20, 30);
            cb.ShowText(text);

            cb.EndText();
            cb.AddTemplate(template, 20 + len, 30);
            cb.SetFontAndSize(bf, 8);
            //       cb.SetTextMatrix(280, 820);

        }
    }

    public class ReportContext
    {
        public DataSetOutput ReportData { get; set; }
        public Document Document { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorText { get; set; }
        public bool HasData { get; set; }
        public PdfPTable PdfTable { get; set; }
        public string StoreName { get; set; }
    }
}
