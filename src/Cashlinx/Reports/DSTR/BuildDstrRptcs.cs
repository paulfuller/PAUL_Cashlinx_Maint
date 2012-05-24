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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;

namespace Reports.DSTR
{
    public class BuildDstrRpt
    {
        private Document _document;
        private Image jpeg;
        private string _rptFileName;
        private string _reportDir;
        private DateTime _reportDate;
        private DataSetOutput _reportData;
        private PdfPTable _dataTable;
        private string _storeNumber;

        private DSTRReportContext DSTRContext { get; set; }

        private PdfPCell _CellColumnRecord;

        private PdfContentByte _cb;

        public DataSetOutput ReportData
        {
            set { _reportData = value; }
        }

        public string StoreName
        {
            get { return DSTRContext.StoreName; }
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
            get { return DSTRContext.ErrorCode; }
        }

        public string ErrorText
        {
            get { return DSTRContext.ErrorText; }
        }

        #region contructors and intializers
        public BuildDstrRpt(string StoreNum, DateTime dateTime, DataSetOutput reportData, string reportDir, string rptFileName)
        {
            DSTRContext = new DSTRReportContext();
            _reportDate = dateTime;
            _reportData = reportData;
            _storeNumber = StoreNum;
            SetDefaults(reportDir, rptFileName);
        }

        public BuildDstrRpt(string StoreNum, DateTime dateTime, DataSetOutput reportData)
        {
            DSTRContext = new DSTRReportContext();
            _reportDate = dateTime;
            _reportData = reportData;
            _storeNumber = StoreNum;
            SetDefaults(".\\", "rpt_096.pdf");
        }

        private void SetDefaults(string reportDir, string rptFileName)
        {
            _rptFileName = rptFileName;
            _reportDir = reportDir;
            DSTRContext.ErrorText = string.Empty;
            DSTRContext.ErrorCode = "OK";
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
                DSTRContext.ErrorText = "No Data Found...";
                DSTRContext.ErrorCode = "1";
                return false;
            }

            jpeg = Image.GetInstance(Common.Properties.Resources.logo, BaseColor.WHITE);
            DSTRContext.ErrorCode = "OK";
            DSTRContext.ErrorText = "OK";

            if (_reportData.GetOutputTable(out dataTable))
            {
                DSTRContext.StoreName = dataTable.Rows[0][1].ToString();
            }

            try
            {
                fullName = _reportDir + "\\" + _rptFileName;

                _document = new Document(PageSize.LETTER.Rotate());
                _dataTable = new PdfPTable(1);
                _dataTable.WidthPercentage = 100;

                DSTRContext.Document = _document;
                DSTRContext.PdfTable = _dataTable;
                DSTRContext.ReportData = _reportData;

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

                List<AbstractDSTRGroup> groups = new List<AbstractDSTRGroup>();
                groups.Add(new Group01_CashAdvance(DSTRContext, "CASH_ADV_1"));
                groups.Add(new Group02_XPP(DSTRContext, "XPP_2"));
                groups.Add(new Group03_Payments(DSTRContext, "PAYMENT_3"));
                groups.Add(new Group04_Recissions(DSTRContext, "RECISSIONS_4"));
                groups.Add(new Group05_DebitCards(DSTRContext, "DEBIT_CARDS_5"));
                groups.Add(new Group06_PhoneCards(DSTRContext, "PHONE_CARDS_6"));
                groups.Add(new Group07_CashTransfers(DSTRContext, "DRAWER_TRANSFER_7"));
                groups.Add(new Group08_CheckCashing(DSTRContext, "CHECK_CASHING_8"));
                groups.Add(new Group09_Insurance(DSTRContext, "INSURANCE_9"));
                groups.Add(new Group11_ConvenienceItems(DSTRContext, "CONVENIENCE_11"));
                groups.Add(new Group12_TaxPreparation(DSTRContext, "TAX_PREP_12"));
                groups.Add(new Group13_OtherGoodServices(DSTRContext, "OTHER_13"));
                groups.Add(new Group14_MoneyOrder(DSTRContext, "MONEY_ORDER_14"));
                groups.Add(new Group15_WireTransfer(DSTRContext, "WIRE_TRANSFER_15"));
                groups.Add(new Group16_Coupons(DSTRContext, "COUPONS_16"));
                groups.Add(new Group17_PettyCash(DSTRContext, "PETTY_CASH_17"));
                groups.Add(new Group18_PaidInsPaidOuts(DSTRContext, "PAID_INOUT18"));
                groups.Add(new Group19_Extensions(DSTRContext, "EXTENSIONS_19"));
                groups.Add(new Group20_CancelCSO(DSTRContext, "CANCEL_CSO_20"));
                groups.Add(new Group21_AchRevoke(DSTRContext, "ACH_RVK_21"));
                groups.Add(new Group22_WaiveWriteOff(DSTRContext, "WAIVE_OFF_22"));
                groups.Add(new Group23_Reimbursements(DSTRContext, "REIMBURSEMENTS_23"));
                groups.Add(new Group24_NewLoans(DSTRContext, "NEW_LOANS_24"));
                groups.Add(new Group25_Extensions(DSTRContext, "EXT_25"));
                groups.Add(new Group26_Renewals(DSTRContext, "RENEW_26"));
                groups.Add(new Group27_PayDowns(DSTRContext, "PAYDOWN_27"));
                groups.Add(new Group28_Pickups(DSTRContext, "PICKUP_28"));

                //--> bz0097     TM --------------------
                groups.Add(new Group32_PFI(DSTRContext, "PFI_32"));
                groups.Add(new Group33_TransfersOut(DSTRContext, "TRANSFER_OUT_33", "Transfers Out"));  // see report detail if title updated, there are some title dependencies elsewhere in report
                groups.Add(new Group33_TransfersOut(DSTRContext, "TRANSFER_OUT_33", "Transfers In"));
                groups.Add(new Group37_38_Purchase(DSTRContext, "PURCHASE_37", "Buys"));
                groups.Add(new Group37_38_Purchase(DSTRContext, "RETURN_38", "Buy Returns"));
                groups.Add(new Group40_Retail(DSTRContext, "RETAIL_40", "Retail Sales (including Paid Out Layaways)"));
                groups.Add(new Group40_Retail(DSTRContext, "SALE_REFUND", "Retail Sale Refunds"));
                groups.Add(new Group40_Retail(DSTRContext, "LAYAWAY_41", "Layaway Payments"));
                groups.Add(new Retail_Refund(DSTRContext, "LAYAWAY_REFUND", "Layaway Payment Refunds"));
                groups.Add(new LayawayTermination(DSTRContext, "LAYAWAY_TERMATION", "Layaway Forfeitures / Terminations"));

                groups.Add(new ChargeOffs(DSTRContext, "CHARGE_OFF"));

                groups.Add(new Group29_Seizures(DSTRContext, "SEIZURE_29"));
                groups.Add(new Group31_ClaimentRelease(DSTRContext, "CLAIM_REL_31"));

                groups.Add(new Group30_PoliceReturns(DSTRContext, "POLICE_RET_30"));
                //----------<bz0097-------------------

                groups.Add(new Group41_PartialPayments(DSTRContext, "O_PARPYMT_45"));

                foreach (AbstractDSTRGroup group in groups)
                {
                    if (group.DataLoaded)
                    {
                        group.BuildSection();
                    }
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

                if (DSTRContext.HasData)
                {
                    _document.Close();
                }
                else
                {
                    DSTRContext.ErrorText = "No records found.";
                    myFile.Close();
                    if (File.Exists(fullName))
                        File.Delete(fullName);
                }
                bRetVal = true;
            }
            catch (Exception ex)
            {
                DSTRContext.ErrorText = ex.Message;
                DSTRContext.ErrorCode = "1";
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
                cellColumnHeader.Phrase = new Phrase("Reporting Date: ", fontHeader);
                dataTable.AddCell(cellColumnHeader);

                cellColumnHeader.Colspan = 2;
                cellColumnHeader.Phrase = new Phrase(_reportDate.ToShortDateString(), fontHeader);
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
                cellColumnHeader.Colspan = 3;
                cellColumnHeader.Phrase = new Phrase("", fontHeader);
                cellColumnHeader.Border = Rectangle.NO_BORDER;
                dataTable.AddCell(cellColumnHeader);

                cellColumnHeader.Colspan = 1;
                cellColumnHeader.Phrase = new Phrase("Report # 96", fontHeader);
                dataTable.AddCell(cellColumnHeader);
                cellColumnHeader.HorizontalAlignment = Element.ALIGN_CENTER;

                cellColumnHeader.Phrase = new Phrase("", fontHeader);
                dataTable.AddCell(cellColumnHeader);


                // Title
                cellColumnHeader.Colspan = 5;
                cellColumnHeader.Phrase = new Phrase("Daily Transaction Report", fontHeader);
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
                DSTRContext.ErrorText = ex.Message;
                DSTRContext.ErrorCode = "1";
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

            string text = " DAILY Sales and Transaction Report"
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

    public class DSTRReportContext
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
