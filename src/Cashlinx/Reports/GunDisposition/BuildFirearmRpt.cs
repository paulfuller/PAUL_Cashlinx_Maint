//***************************************************************
// class_name   : BuildFirearmRpt
// created by   : rmcbai1
// date created : 12/30/2009 10:54:09 AM
//
// Copyright  2009 Cash America International
//---------------------------------------------------------------
//   description:  this class will create the Report #201 (MULTIPLE  
//                 SALE  OR  OTHER  DISPOSITION  OF  PISTOLS  AND  
//                 REVOLVERS) with a passed in dataset. The dataset
//                 contains two tables, one for the group header
//                 data and one for the detail data
//
//----------------------------------------------------------------
// change history
//  no ticket S. Murphy 3/4/2010 change from stream to bitmap for use of project resources
//  no ticket SMurphy 3/8/2010 added no data in WriteData()
//  no ticket SMurphy 3/12/2010 moved this check to ReportProcedures - ExecuteGunDispositionReport 
//  PWNU00000582 4/1/2010 SMurphy added date info
//****************************************************************

using System;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Reflection;

namespace Reports.GunDisposition
{
    ///<summary>
    /// this class will create the Report #201 (MULTIPLE  
    //// SALE  OR  OTHER  DISPOSITION  OF  PISTOLS  AND  
    //// REVOLVERS) with a passed in dataset. The dataset
    //// contains two tables, one for the group header
    //// data and one for the detail data
    ///</summary>
    public class BuildFirearmRpt
    {
        //  PWNU00000582 4/1/2010 SMurphy added date info
        public string StartDate;
        public string EndDate;

        #region private vars and properties
        private string _dateRange;
        private string _rptFileName;
        private string _reportDir;
        private DataSet _reportData;
        private int _errCode;
        private string _errTxt;
        private string _storeNum;

        private DateTime _runDate;
        private Document _document;
        private BaseFont bf;
        private Image jpeg;
        //private PdfContentByte cb;
        const int prompHPos = 10;

        private const string detailHeader =
            "Date      Type         Serial Number               Manufacturer Model     Importer   Caliber	  Trans Type Trans # Gun No.";

        public string DateRange
        {
            get { return _dateRange; }
            set { _dateRange = value; }
        }
        public string RptFileName
        {
            get { return _rptFileName; }
            set { _rptFileName = value; }
        }

        public string StoreNum
        {
            get { return _storeNum; }
            set { _storeNum = value; }
        }

        public DateTime RunDate
        {
            get { return _runDate; }
            set { _runDate = value; }
        }

        public string ReportDir
        {
            get { return _reportDir; }
            set { _reportDir = value; }
        }
        public int ErrCode
        {
            get { return _errCode; }
        }

        public string ErrTxt
        {
            get { return _errTxt; }
        }

        public DataSet ReportData
        {
            set { _reportData = value; }
        }
        #endregion

        #region contructors and intializers
        public BuildFirearmRpt(DataSet reportData, string reportDir, string rptFileName)
        {
            _reportData = reportData;
            SetDefaults(reportDir, rptFileName);
        }

        public BuildFirearmRpt(DataSet reportData)
        {
            _reportData = reportData;
            SetDefaults(".\\", "rpt_201.pdf");
        }

        private void SetDefaults(string reportDir, string rptFileName)
        {
            _rptFileName = rptFileName;
            _reportDir = reportDir;
            _errTxt = string.Empty;
            _errCode = 0;
        }

        #endregion


        //*************************************************
        //** Date created: Thursday, December 31, 2009
        //** Created by  : PAWN\rmcbai1
        //*************************************************
        /// <summary>
        ///    Here we setup the PDF file and call the method
        ///    used to fill the report
        /// </summary>
        ///
        /// <returns> (bool) indicating success or failure</returns>
        //*************************************************
        public bool CreateRpt()
        {


            bool bRetVal;
            string fullName;

            bRetVal = false;

            Assembly thisDLL;
            PdfWriter writer;
            MyPageEvents events;
            PdfContentByte cb;

            thisDLL = Assembly.GetExecutingAssembly();

            //S. Murphy 3/4/2010 change from stream to bitmap for use of project resources
            var bitmap = Common.Properties.Resources.logo;
            jpeg = Image.GetInstance(bitmap, BaseColor.WHITE);

            try
            {
                fullName = _reportDir + "\\" + _rptFileName;
                _document = new Document(PageSize.LETTER, 30, 30, 72, 65);
                events = new MyPageEvents();
                writer = PdfWriter.GetInstance(_document, new FileStream(fullName, FileMode.Create));
                writer.PageEvent = events;

                _document.Open();
                cb = writer.DirectContent;

                WriteData(cb, jpeg);
                if (_errTxt.Length.Equals(0))
                {
                    _document.Close();
                }
                bRetVal = true;
            }
            catch (Exception ex)
            {
                _errTxt = ex.Message;
                _errCode = 1;
                bRetVal = false;
                throw;
            }
            return bRetVal;
        }

        //*************************************************
        //** Date created: Tuesday, January 05, 2010
        //** Created by  : PAWN\rmcbai1
        //*************************************************
        /// <summary>
        ///    Here we print our page header 
        ///    and loop thru the customers printing our info
        ///    
        /// </summary>
        ///
        //*************************************************
        private void WriteData(PdfContentByte cb, Image jpeg)
        {

            string prtRow;
            string custName;
            int vPos;

            const int dataHPos = 100;
            DataTable custTbl;
            try
            {
                custTbl = _reportData.Tables["GUN_HEADER"];
                //no ticket SMurphy 3/8/2010 added no data in WriteData()
                //no ticket SMurphy 3/12/2010 moved this check to ReportProcedures - ExecuteGunDispositionReport 
                //if (custTbl == null)
                //{
                //    _errTxt = "No Data was returned";
                //    _errCode = 0;
                //}
                //else
                //{
                foreach (DataRow custRow in custTbl.Rows)
                {
                    PrintPageHeader(cb, jpeg);
                    vPos = 600;
                    cb.SetTextMatrix(prompHPos, vPos);
                    cb.ShowText("Customer Name:");
                    cb.SetTextMatrix(dataHPos, vPos);
                    custName = custRow["FAMILYNAME"] + ", " + custRow["FIRSTNAME"];
                    cb.ShowText(custName);

                    vPos -= 20;
                    cb.SetTextMatrix(prompHPos, vPos);
                    cb.ShowText("Address:");
                    cb.SetTextMatrix(dataHPos, vPos);
                    prtRow = string.Format("{0}, {1}, {2} {3}", custRow["GET_HOME_ADDRESS(PR.PARTYID)"], custRow["GET_HOME_CITY(PR.PARTYID)"], custRow["GET_HOME_STATE(PR.PARTYID)"], custRow["GET_HOME_ZIP(PR.PARTYID)"]);
                    cb.ShowText(prtRow);

                    vPos -= 10;
                    cb.SetTextMatrix(prompHPos, vPos);
                    cb.ShowText("ID:");
                    cb.SetTextMatrix(dataHPos, vPos);
                    prtRow = custRow["DISPOSITION_STATE"] + "  " + custRow["DISPOSITION_ID_NUMBER"];
                    cb.ShowText(prtRow);

                    vPos -= 10;
                    cb.SetTextMatrix(prompHPos, vPos);
                    cb.ShowText("DOB:");
                    cb.SetTextMatrix(dataHPos, vPos);
                    cb.ShowText(Convert.ToDateTime(custRow["BIRTHDATE"]).ToShortDateString());

                    vPos -= 10;
                    cb.SetTextMatrix(prompHPos, vPos);
                    cb.ShowText("Race:");
                    cb.SetTextMatrix(dataHPos, vPos);
                    cb.ShowText(custRow["RACEDESC"].ToString());

                    vPos -= 10;
                    cb.SetTextMatrix(prompHPos, vPos);
                    cb.ShowText("Sex:");
                    cb.SetTextMatrix(dataHPos, vPos);
                    cb.ShowText(custRow["GENDERCODE"].ToString());

                    WriteDetail(cb, vPos, custName, custRow["CUSTOMERNUMBER"].ToString(),
                                custRow["DISPOSITION_ID_NUMBER"].ToString(),
                                custRow["DISPOSITION_STATE"].ToString());
                    //cb.EndText();

                    _document.NewPage();
                }
            }
            //}
            catch (Exception ex)
            {
                _errTxt = ex.Message;
                _errCode = 1;
                throw;
            }
            return;
        }
        //*************************************************
        //** Date created: Wednesday, January 06, 2010
        //** Created by  : PAWN\rmcbai1
        //*************************************************
        /// <summary>
        ///    Here we pull the data out of the GUN_DETAIL 
        ///    for the a customer
        /// </summary>
        ///
        ///<param name=" custNum">search criteria for finding the correct records </param>
        ///<param name=" dispID">search criteria for finding the correct records</param>
        ///<param name=" dispState">search criteria for finding the correct records</param>
        //*************************************************
        private void WriteDetail(PdfContentByte cb, int vPos, string custName,
                                 string custNum, string dispID, string dispState)
        {

            string selectStr = string.Empty;
            string sRow = string.Empty;
            string dividerOne = string.Empty;
            string dividerTwo = string.Empty;
            DataTable detailTbl;
            DataRow[] foundRows;

            try
            {
                bf = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb.SetFontAndSize(bf, 8);
                dividerOne = dividerOne.PadLeft(120, '-');
                dividerTwo = dividerTwo.PadLeft(120, '=');

                vPos -= 30;
                cb.SetTextMatrix(prompHPos, vPos);
                cb.ShowText(detailHeader);

                vPos -= 10;
                cb.SetTextMatrix(prompHPos, vPos);
                cb.ShowText(dividerOne);

                detailTbl = _reportData.Tables["GUN_DETAIL"];
                //************************************************************
                // since it is possible for the DISPOSITION_ID_NUMBER or the
                // DISPOSITION_STATE to be null we need to craft the select 
                // filter to deal with those scenarios
                //************************************************************
                if (dispID.Trim().Length > 0 && dispState.Trim().Length > 0)
                {
                    selectStr = "ACQUIRE_CUSTOMER_NUMBER = '" + custNum
                                + "' AND DISPOSITION_ID_NUMBER = '" + dispID
                                + "' AND DISPOSITION_STATE = '" + dispState + "'";
                }
                else if (dispID.Trim().Length == 0 && dispState.Trim().Length == 0)
                {
                    selectStr = "ACQUIRE_CUSTOMER_NUMBER = '" + custNum
                                + "' AND DISPOSITION_ID_NUMBER IS NULL "
                                + " AND DISPOSITION_STATE IS NULL ";
                }
                else if (dispID.Trim().Length == 0 && dispState.Trim().Length > 0)
                {
                    selectStr = "ACQUIRE_CUSTOMER_NUMBER = '" + custNum
                                + "' AND DISPOSITION_ID_NUMBER IS NULL "
                                + "' AND DISPOSITION_STATE = '" + dispState + "'"; ;
                }
                else if (dispID.Trim().Length > 0 && dispState.Trim().Length == 0)
                {
                    selectStr = "ACQUIRE_CUSTOMER_NUMBER = '" + custNum
                                + "' AND DISPOSITION_ID_NUMBER = '" + dispID
                                + "' AND DISPOSITION_STATE IS NULL ";
                }

                foundRows = detailTbl.Select(selectStr);
                foreach (DataRow detailRow in foundRows)
                {
                    vPos -= 10;
                    cb.SetTextMatrix(prompHPos, vPos);
                    sRow = BuildDetailRow(detailRow);
                    cb.ShowText(sRow);
                    if (vPos <= 80)
                    {
                        _document.NewPage();
                        PrintPageHeader(cb, jpeg);
                        vPos = 540;
                        cb.SetTextMatrix(prompHPos + 20, vPos + 30);
                        cb.ShowText(custName + " Continued");
                    }

                }
                // test for page over runs
                //if (custNum == "1-000000-038951-0")
                //{
                //   for (int Idx = 0; Idx < 100; ++Idx)
                //   {
                //      vPos -= 10;
                //      cb.SetTextMatrix(prompHPos, vPos);
                //      cb.ShowText("Test for page over run -[" + Idx.ToString() + "]");
                //      if (vPos <= 80)
                //      {
                //         _document.NewPage();
                //         PrintPageHeader(cb, jpeg);
                //         vPos = 540;
                //         cb.SetTextMatrix(prompHPos+20, vPos+30);
                //         cb.ShowText(custName + " Continued");
                //      }
                //   }
                //}
                vPos -= 10;
                cb.SetTextMatrix(prompHPos, vPos);
                cb.ShowText(dividerTwo);
            }
            catch (Exception ex)
            {
                _errTxt = ex.Message;
                _errCode = 1;
                throw;
            }
            return;
        }
        //*************************************************
        //** Date created: Wednesday, January 06, 2010
        //** Created by  : PAWN\rmcbai1
        //*************************************************
        /// <summary>
        ///   This method builds a formatted string containing
        ///   all the data for a row properly spaced out to fit
        ///   on a line in the pdf file. Some elements will be
        ///   truncated to maxlen while others will not
        /// </summary>
        ///
        /// <returns>the formatted row (string)</returns>
        ///<param name=" DetailData">the raw data</param>
        //*************************************************
        private string BuildDetailRow(DataRow detailRow)
        {
            const int maxLen = 10;
            string sRetVal;
            string dateStr;
            StringBuilder detailData;
            try
            {
                detailData = new StringBuilder();
                dateStr = Convert.ToDateTime(detailRow[0]).ToShortDateString();

                detailData.Append(dateStr + " ");

                if (detailRow["GUN_TYPE"].ToString().Trim().Length < maxLen)
                    detailData.Append(detailRow["GUN_TYPE"].ToString().Trim().PadRight(maxLen) + " ");
                else
                    detailData.Append(detailRow["GUN_TYPE"].ToString().Trim().Substring(0, maxLen) + " ");

                detailData.Append(detailRow["SERIAL_NUMBER"].ToString().Trim().PadRight(30));

                if (detailRow["MANUFACTURER"].ToString().Trim().Length < maxLen)
                    detailData.Append(detailRow["MANUFACTURER"].ToString().Trim().PadRight(maxLen) + " ");
                else
                    detailData.Append(detailRow["MANUFACTURER"].ToString().Trim().Substring(0, maxLen) + " ");

                if (detailRow["MODEL"].ToString().Trim().Length < maxLen)
                    detailData.Append(detailRow["MODEL"].ToString().Trim().PadRight(maxLen) + " ");
                else
                    detailData.Append(detailRow["MODEL"].ToString().Trim().Substring(0, maxLen) + " ");

                if (detailRow["IMPORTER"].ToString().Trim().Length < maxLen)
                    detailData.Append(detailRow["IMPORTER"].ToString().Trim().PadRight(maxLen) + " ");
                else
                    detailData.Append(detailRow["IMPORTER"].ToString().Trim().Substring(0, maxLen) + " ");

                if (detailRow["CALIBER"].ToString().Trim().Length < maxLen)
                    detailData.Append(detailRow["CALIBER"].ToString().Trim().PadRight(maxLen) + " ");
                else
                    detailData.Append(detailRow["CALIBER"].ToString().Trim().Substring(0, maxLen) + " ");

                if (detailRow["STATUS_CD"].ToString().Trim().Length < maxLen)
                    detailData.Append(detailRow["STATUS_CD"].ToString().Trim().PadRight(maxLen) + " ");
                else
                    detailData.Append(detailRow["STATUS_CD"].ToString().Trim().Substring(0, maxLen) + " ");

                detailData.Append(detailRow["DISPOSITION_DOCUMENT_NUMBER"].ToString().Trim().PadRight(9));

                detailData.Append(detailRow["GUN_NUMBER"].ToString().Trim().PadRight(9));

                sRetVal = detailData.ToString();
            }
            catch (Exception ex)
            {
                _errTxt = ex.Message;
                _errCode = 1;
                sRetVal = string.Empty;
                throw;
            }
            return sRetVal;
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
        private void PrintPageHeader(PdfContentByte cb, Image jpeg)
        {
            try
            {
                jpeg.ScalePercent(84);
                jpeg.SetAbsolutePosition(1, 730);
                _document.Add(jpeg);
                bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb.BeginText();
                cb.SetFontAndSize(bf, 10);

                cb.SetTextMatrix(400, 732);
                cb.ShowText(_reportData.Tables["OUTPUT"].Rows[1][1].ToString());
                cb.SetTextMatrix(400, 720);
                cb.ShowText("RunDate: " + DateTime.Now.ToShortDateString());
                cb.SetTextMatrix(400, 708);
                cb.ShowText("Report #  201");

                cb.SetTextMatrix(10, 700);
                cb.ShowText("Operational");
                cb.SetTextMatrix(10, 690);
                //  PWNU00000582 4/1/2010 SMurphy added date info
                //cb.ShowText(_dateRange);
                cb.ShowText(this.StartDate + " to " + this.EndDate);
                bf = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                //Font titleFont = new Font(bf, 12, Font.BOLD);
                cb.SetTextMatrix(100, 648);
                cb.ShowText("MULTIPLE  SALE  OR  OTHER  DISPOSITION  OF  PISTOLS  AND  REVOLVERS  ");
            }
            catch (Exception ex)
            {
                _errTxt = ex.Message;
                _errCode = 1;
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
            sPadding = sPadding.PadLeft(90, ' ');
            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb = writer.DirectContent;
            int pageN = writer.PageNumber;
            string text = " MULTIPLE SALE OR OTHER DISPOSITION OF PISTOLS AND REVOLVERS"
                          + sPadding
                          + "  Page " + pageN + " of ";

            float len = bf.GetWidthPoint(text, 8);

            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(10, 60);
            cb.ShowText("THIS REPORT DOES NOT REPLACE FORM ATF F 3310.4. YOU MUST STILL FILL OUT AND MAIL ATF F 3310.4 AS INSTRUCTED ON THAT FORM.");
            cb.SetTextMatrix(10, 30);
            cb.ShowText(text);

            cb.AddTemplate(template, 10 + len, 30);
            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(280, 820);

            cb.EndText();
        }
    }
}