//***************************************************************
// class_name   : GunDispositionReport.cs
// created by   : rmcbai1/smurphy
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
//  no ticket SMurphy 4/14/2010 restructured to match other reports
//  PWNU00000759 SMurphy 5/21/2010 pdf erroring after print - EndText() in wrong place in OnEndPage
//  no ticket SMurphy 4/21/2010 Changes to remove iText from ReportObject per Sree 
//  Build9 Tracy 12/2/10  Added support for Layaway and retail sales
//***************************************************************

using System;
using System.IO;
using System.Text;
using Common.Controllers.Application;
using Common.Libraries.Objects;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;

namespace Reports
{
    ///<summary>
    /// this class will create the Report #201 (MULTIPLE  
    //// SALE  OR  OTHER  DISPOSITION  OF  PISTOLS  AND  
    //// REVOLVERS) with a passed in dataset. The dataset
    //// contains two tables, one for the group header
    //// data and one for the detail data
    ///</summary>
    public class GunDispositionReport : ReportBase
    {
        //main objects

        #region private vars and properties
        private iTextSharp.text.Document _document;
        private BaseFont bf;
        private Image jpeg;
        private PdfContentByte cb;
        private PdfTemplate template;
        private BaseFont footerBaseFont = null;
        private PdfTemplate template2;
        private PdfTemplate template3;
        private bool hasData = false;

        const int prompHPos = 17;

        private const string detailHeader =
            "Date       Type       Serial Number               Manufacturer Model     Importer   Caliber	  Trans Type Trans # Gun No.";

        #endregion


        #region Constructors
        public GunDispositionReport(IPdfLauncher pdfLauncher)
            : base(pdfLauncher)
        {
            
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
        public bool CreateReport()
        {

            bool bRetVal = false;


            try
            {
                _document = new iTextSharp.text.Document(PageSize.LETTER, 30, 30, 72, 65);
                var events = this;
                var writer = PdfWriter.GetInstance(_document, new FileStream(reportObject.ReportTempFileFullName, FileMode.Create));
                writer.PageEvent = events;

                MultiColumnText columns = new MultiColumnText(_document.PageSize.Top - 148, _document.PageSize.Height - (170));
                //float pageLeft = document.PageSize.Left;
                //float pageright = document.PageSize.Right;
                columns.AddSimpleColumn(-51, _document.PageSize.Width + 60);

                System.Drawing.Bitmap bitmap = global::Common.Properties.Resources.logo;
                jpeg = Image.GetInstance(bitmap, BaseColor.WHITE);

                _document.Open();
                //cb = writer.DirectContent;
                int tblColumnCount = 11;
                PdfPTable tblCustomer = new PdfPTable(tblColumnCount);

                if (reportObject.GunDispositionData == null || reportObject.GunDispositionData.Tables["GUN_HEADER"] == null ||
                    reportObject.GunDispositionData.Tables["GUN_DETAIL"] == null)
                {
                    //PrintPageHeader(cb, jpeg);
                    /*bf = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    BaseFont boldFont = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);


                    cb.SetFontAndSize(bf, 10);
                {
                    PrintPageHeader(cb, jpeg);
                    bf = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    BaseFont boldFont = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);


                    cb.SetFontAndSize(bf, 10);

                    cb.SetTextMatrix(200, 600);
                    cb.ShowText("** NO QUALIFYING TRANSACTIONS ** ");
                    //cb.EndText();*/
                    WriteNotransactions(tblCustomer, tblColumnCount);
                    columns.AddElement(tblCustomer);
                    _document.Add(columns);
                    //_document.Close();
                }
                else
                {
                    WriteData(cb, jpeg, tblCustomer, tblColumnCount);
                    columns.AddElement(tblCustomer);
                    _document.Add(columns);
                    if (!hasData)
                    {
                        //PrintPageHeader(cb, jpeg);
                        /*bf = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        BaseFont boldFont = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        cb.SetFontAndSize(bf, 10);
                        cb.SetTextMatrix(200, 600);
                        cb.ShowText("** NO QUALIFYING TRANSACTIONS ** ");
                        //cb.EndText();*/
                        WriteNotransactions(tblCustomer, tblColumnCount);
                        columns.AddElement(tblCustomer);
                        _document.Add(columns);
                        //_document.Close();
                    }
                    //else if (reportObject.ReportError.Length.Equals(0))
                    //{
                        //_document.Close();
                    //}
                    
                }
               _document.Close();
                //OpenFile(reportObject.ReportTempFileFullName);
                //CreateReport();
                bRetVal = true;
            }
            catch (Exception ex) 
            {
                reportObject.ReportError = ex.Message;
                reportObject.ReportErrorLevel = 1;
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
        private void WriteNotransactions(PdfPTable table, int tblColumnCount)
        {
            WriteCell(table, "** NO QUALIFYING TRANSACTIONS ** ", ReportFont, tblColumnCount, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
        }

        private void WriteData(PdfContentByte cb, Image jpeg, PdfPTable tblCustomer, int tblColumnCount)
        {

            string prtRow;
            string custName;
            int vPos;

            //const int dataHPos = 100;
            DataTable custTbl;
            try
            {
                custTbl = reportObject.GunDispositionData.Tables["GUN_HEADER"];
                string startDate = string.Format("{0:d}", reportObject.ReportParms[(int)ReportEnums.STARTDATE]);
                string today = string.Format ("{0:d}",reportObject.ReportParms[(int)ReportEnums.ENDDATE]); //DateTime.Now.ToShortDateString();
                //here print page header
                //PrintPageHeader(cb, jpeg);
                foreach (DataRow custRow in custTbl.Rows)
                {

                    if (reportObject.GunDispositionData.Tables["GUN_DETAIL"].Select("DISPOSITION_CUSTOMER_NUMBER='" + custRow["CUSTOMERNUMBER"].ToString() + "' AND DISPOSITION_DATE >= #" + startDate + "# and DISPOSITION_DATE <= #" + today + "#").GetLength(0) == 0)
                        continue;

                    if (reportObject.GunDispositionData.Tables["GUN_DETAIL"].Select("DISPOSITION_CUSTOMER_NUMBER='" + custRow["CUSTOMERNUMBER"].ToString() + "'").GetLength(0) < 2)
                        continue;

                    hasData = true;
                    getRelevantGunDetails(custRow["CUSTOMERNUMBER"].ToString(), custRow["DISPOSITION_ID_NUMBER"].ToString(),
                                          custRow["DISPOSITION_STATE"].ToString()).GetLength(0);

                    WriteCell(tblCustomer, "Customer Name:", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                    if(!string.IsNullOrEmpty(custRow["DISPOSITION_MIDDLE_INITIAL"].ToString()))
                        WriteCell(tblCustomer, custRow["DISPOSITION_LAST_NAME"].ToString() + " " + custRow["DISPOSITION_FIRST_NAME"].ToString() + " " + custRow["DISPOSITION_MIDDLE_INITIAL"].ToString(), ReportFont, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                    else
                        WriteCell(tblCustomer, custRow["DISPOSITION_LAST_NAME"].ToString() + " " + custRow["DISPOSITION_FIRST_NAME"].ToString(), ReportFont, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                    prtRow = custRow["DISPOSITION_ADDRESS"].ToString() + ", "
                                + custRow["DISPOSITION_CITY"].ToString() + ", "
                                + custRow["DISPOSITION_STATE2"].ToString() + " "
                                + custRow["DISPOSITION_POSTAL_CODE"].ToString();
                    WriteCell(tblCustomer, "Address:", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                    WriteCell(tblCustomer, prtRow, ReportFont, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                    WriteCell(tblCustomer, "ID:", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                    WriteCell(tblCustomer, custRow["DISPOSITION_STATE"].ToString() + "  " + custRow["DISPOSITION_ID_NUMBER"].ToString(), ReportFont, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                    WriteCell(tblCustomer, "ID Type:", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                    WriteCell(tblCustomer, custRow["DISPOSITION_ID_TYPE"].ToString(), ReportFont, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                    WriteCell(tblCustomer, "DOB:", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                    WriteCell(tblCustomer, Convert.ToDateTime(custRow["BIRTHDATE"]).ToShortDateString(), ReportFont, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                    WriteCell(tblCustomer, "Race:", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                    WriteCell(tblCustomer, custRow["RACEDESC"].ToString(), ReportFont, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                    WriteCell(tblCustomer, "Sex:", ReportFont, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                    WriteCell(tblCustomer, custRow["GENDERCODE"].ToString(), ReportFont, 9, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                    vPos = 600;
                    custName = custRow["FAMILYNAME"] + ", " + custRow["FIRSTNAME"];

                    WriteCell(tblCustomer, string.Empty, ReportFont, tblColumnCount, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                    WriteCell(tblCustomer, string.Empty, ReportFont, tblColumnCount, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                    WriteDetail(cb, vPos, custName, custRow["CUSTOMERNUMBER"].ToString(),
                            custRow["DISPOSITION_ID_NUMBER"].ToString(),
                            custRow["DISPOSITION_STATE"].ToString(), tblCustomer, tblColumnCount);

                    WriteCell(tblCustomer, string.Empty, ReportFont, tblColumnCount, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                    WriteCell(tblCustomer, string.Empty, ReportFont, tblColumnCount, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                    WriteCell(tblCustomer, string.Empty, ReportFont, tblColumnCount, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                    
                    //
                   /* vPos = 600;
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
                    prtRow = string.Format("{0}  {1}", custRow["DISPOSITION_STATE"], custRow["DISPOSITION_ID_NUMBER"]);
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

                    //if customernumber is the same 

                    WriteDetail(cb, vPos, custName, custRow["CUSTOMERNUMBER"].ToString(),
                                custRow["DISPOSITION_ID_NUMBER"].ToString(),
                                custRow["DISPOSITION_STATE"].ToString(), tblCustomer, tblColumnCount);
                    //cb.EndText();

                    _document.NewPage();*/
                }
            }
            //}
            catch (Exception ex)
            {
                reportObject.ReportError = ex.Message;
                reportObject.ReportErrorLevel = 1;
                throw;
            }
            return;
        }

        private DataRow[] getRelevantGunDetails(string custNum, string dispID, string dispState)
        {
            var detailTbl = reportObject.GunDispositionData.Tables["GUN_DETAIL"];
            string selectStr = " 1 = 0";

            //************************************************************
            // since it is possible for the DISPOSITION_ID_NUMBER or the
            // DISPOSITION_STATE to be null we need to craft the select 
            // filter to deal with those scenarios
            //************************************************************
            if (dispID.Trim().Length > 0 && dispState.Trim().Length > 0)
            {
                selectStr = "DISPOSITION_CUSTOMER_NUMBER = '" + custNum 
                            + "' AND DISPOSITION_ID_NUMBER = '" + dispID
                            + "' AND DISPOSITION_STATE = '" + dispState + "'";
            }
            else if (dispID.Trim().Length == 0 && dispState.Trim().Length == 0)
            {
                selectStr = string.Format("DISPOSITION_CUSTOMER_NUMBER = '{0}' AND DISPOSITION_ID_NUMBER IS NULL " + " AND DISPOSITION_STATE IS NULL ", custNum);
            }
            else if (dispID.Trim().Length == 0 && dispState.Trim().Length > 0)
            {
                selectStr = "DISPOSITION_CUSTOMER_NUMBER = '" + custNum
                            + "' AND DISPOSITION_ID_NUMBER IS NULL "
                            + " AND DISPOSITION_STATE = '" + dispState + "'"; ;
            }
            else if (dispID.Trim().Length > 0 && dispState.Trim().Length == 0)
            {
                selectStr = string.Format("DISPOSITION_CUSTOMER_NUMBER = '{0}' AND DISPOSITION_ID_NUMBER = '{1}' AND DISPOSITION_STATE IS NULL ", custNum, dispID);
            }

            return detailTbl.Select(selectStr);            
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
                                 string custNum, string dispID, string dispState, PdfPTable tblCustomer, int tblColumnCount)
        {

            string selectStr = string.Empty;
            string sRow = string.Empty;
            string dividerOne = string.Empty;
            string dividerTwo = string.Empty;
            //DataTable detailTbl;
            DataRow[] foundRows;

            try
            {
                //show header
                WriteCell(tblCustomer, "Date", ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(tblCustomer, "Type", ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(tblCustomer, "Serial Number", ReportFontMedium, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(tblCustomer, "Manufacturer", ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(tblCustomer, "Model", ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(tblCustomer, "Importer", ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(tblCustomer, "Caliber", ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(tblCustomer, "Trans Type", ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(tblCustomer, "Trans #", ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(tblCustomer, "Gun No.", ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                StringBuilder underlines = new StringBuilder();
                for (int i = 0; i <= 109; i++)
                    underlines.Append("_ ");
                //WriteCell(tblCustomer, PawnUtilities.String.StringUtilities.fillString("-", 230), ReportFont, tblColumnCount, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(tblCustomer, underlines.ToString(), ReportFontMedium, tblColumnCount, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                
                //then show lines
                /*bf = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
               BaseFont boldFont = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

               cb.SetFontAndSize(bf, 8);
               dividerOne = dividerOne.PadLeft(120, '-');
               dividerTwo = dividerTwo.PadLeft(120, '=');

               vPos -= 30;
               cb.SetTextMatrix(prompHPos, vPos);
               cb.ShowText(detailHeader);

               vPos -= 10;
               cb.SetTextMatrix(prompHPos, vPos);
               cb.ShowText(dividerOne);
              */
                /*detailTbl = reportObject.GunDispositionData.Tables["GUN_DETAIL"];
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
                */
                foundRows = getRelevantGunDetails(custNum, dispID, dispState);
                foreach (DataRow detailRow in foundRows)
                {
                    BuildDetailRow(detailRow, tblCustomer);
                }

                WriteCell(tblCustomer, StringUtilities.fillString("=", 157), ReportFontMedium, tblColumnCount, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                /*
                foreach (DataRow detailRow in foundRows)
                {
                    vPos -= 10;
                    cb.SetTextMatrix(prompHPos, vPos);
                    sRow = BuildDetailRow(detailRow);

                    // Bold Capitals of status
                    cb.ShowText(sRow.Substring(0, 95));

                    cb.SetFontAndSize(boldFont, 8);
                    int len = 1;
                    if (detailRow["STATUS_CD"].ToString().Equals("RTC"))
                        len = 3;

                    cb.ShowText(sRow.Substring(95, len));

                    cb.SetFontAndSize(bf, 8);
                    cb.ShowText(sRow.Substring(95 + len));
                    //cb.ShowText(sRow);
                    //--------------------

                    if (vPos <= 80)
                    {
                        _document.NewPage();
                        PrintPageHeader(cb, jpeg);
                        vPos = 540;
                        cb.SetTextMatrix(prompHPos + 20, vPos + 30);
                        cb.ShowText(string.Format("{0} Continued", custName));
                    }

                }

                vPos -= 10;
                cb.SetTextMatrix(prompHPos, vPos);
                cb.ShowText(dividerTwo);*/
            }
            catch (Exception ex)
            {
                reportObject.ReportError = ex.Message;
                reportObject.ReportErrorLevel = 1;
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
        private void BuildDetailRow(DataRow detailRow, PdfPTable tblCustomer)
        {
            //const int maxLen = 10;
            string sRetVal;
            string dateStr;
            //StringBuilder detailData;
            try
            {
                //detailData = new StringBuilder();
                dateStr = Convert.ToDateTime(detailRow[0]).ToShortDateString();
                WriteCell(tblCustomer, dateStr, ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                //detailData.Append(dateStr + " ");
                /*
                if (detailRow["GUN_TYPE"].ToString().Trim().Length < maxLen)
                    detailData.Append(detailRow["GUN_TYPE"].ToString().Trim().PadRight(maxLen) + " ");
                else
                    detailData.Append(detailRow["GUN_TYPE"].ToString().Trim().Substring(0, maxLen) + " ");
                */
                WriteCell(tblCustomer, detailRow["GUN_TYPE"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                //detailData.Append(detailRow["SERIAL_NUMBER"].ToString().Trim().PadRight(30));
                WriteCell(tblCustomer, detailRow["SERIAL_NUMBER"].ToString(), ReportFontMedium, 2, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                /*
                if (detailRow["MANUFACTURER"].ToString().Trim().Length < maxLen)
                    detailData.Append(detailRow["MANUFACTURER"].ToString().Trim().PadRight(maxLen) + " ");
                else
                    detailData.Append(detailRow["MANUFACTURER"].ToString().Trim().Substring(0, maxLen) + " ");
                */
                WriteCell(tblCustomer, detailRow["MANUFACTURER"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                /*
                if (detailRow["MODEL"].ToString().Trim().Length < maxLen)
                    detailData.Append(detailRow["MODEL"].ToString().Trim().PadRight(maxLen) + " ");
                else
                    detailData.Append(detailRow["MODEL"].ToString().Trim().Substring(0, maxLen) + " ");
                */
                WriteCell(tblCustomer, detailRow["MODEL"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                /*
                if (detailRow["IMPORTER"].ToString().Trim().Length < maxLen)
                    detailData.Append(detailRow["IMPORTER"].ToString().Trim().PadRight(maxLen) + " ");
                else
                    detailData.Append(detailRow["IMPORTER"].ToString().Trim().Substring(0, maxLen) + " ");
                */
                WriteCell(tblCustomer, detailRow["IMPORTER"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                /*
                if (detailRow["CALIBER"].ToString().Trim().Length < maxLen)
                    detailData.Append(detailRow["CALIBER"].ToString().Trim().PadRight(maxLen) + " ");
                else
                    detailData.Append(detailRow["CALIBER"].ToString().Trim().Substring(0, maxLen) + " ");
                */
                WriteCell(tblCustomer, detailRow["CALIBER"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                /*
                if (detailRow["STATUS_CD"].ToString().Trim().Length < maxLen)
                    detailData.Append(detailRow["STATUS_CD"].ToString().Trim().PadRight(maxLen) + " ");
                else
                    detailData.Append(detailRow["STATUS_CD"].ToString().Trim().Substring(0, maxLen) + " ");
                */
                WriteCell(tblCustomer, detailRow["STATUS_CD"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);


                //detailData.Append(detailRow["DISPOSITION_DOCUMENT_NUMBER"].ToString().Trim().PadRight(9));
                WriteCell(tblCustomer, detailRow["DISPOSITION_DOCUMENT_NUMBER"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                //detailData.Append(detailRow["GUN_NUMBER"].ToString().Trim().PadRight(9));
                WriteCell(tblCustomer, detailRow["GUN_NUMBER"].ToString(), ReportFontMedium, 1, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                //sRetVal = detailData.ToString();
            }
            catch (Exception ex)
            {
                reportObject.ReportError = ex.Message;
                reportObject.ReportErrorLevel = 1;
                sRetVal = string.Empty;
                throw;
            }
            //return sRetVal;
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
        private void PrintPageHeader2(PdfPTable headingtable, Image jpeg, int columnCount)
        {
            try
            {
                /*jpeg.ScalePercent(84);
                jpeg.SetAbsolutePosition(1, 730);
                _document.Add(jpeg);
                bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb.BeginText();
                cb.SetFontAndSize(bf, 10);

                cb.SetTextMatrix(400, 732);
                cb.ShowText(reportObject.GunDispositionData.Tables["OUTPUT"].Rows[1][1].ToString());
                cb.SetTextMatrix(400, 720);
                cb.ShowText("Store #: " + reportObject.ReportStore);
                cb.SetTextMatrix(400, 708);
                cb.ShowText("RunDate: " + DateTime.Now.ToShortDateString());
                cb.SetTextMatrix(400, 696);

                cb.ShowText("Report #  " + reportObject.ReportNumber);

                // 12/2/10 Tracy -- updated x argument so all text was displayed
                cb.SetTextMatrix(12, 700);
                cb.ShowText(ReportHeaders.OPERATIONAL);
                cb.SetTextMatrix(12, 690);
                //  PWNU00000582 4/1/2010 SMurphy added date info
                //cb.ShowText(_dateRange);
                cb.ShowText(reportObject.ReportParms[(int)ReportEnums.STARTDATE] + " to " + reportObject.ReportParms[(int)ReportEnums.ENDDATE]);
                bf = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                //Font titleFont = new Font(bf, 12, Font.BOLD);
                cb.SetTextMatrix(100, 648);*/


                PdfPCell cell = new PdfPCell();

                //  row 1
                jpeg.ScalePercent(50);
                cell = new PdfPCell(jpeg);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.Colspan = columnCount;
                headingtable.AddCell(cell);

                //row 2
                WriteCell(headingtable, string.Empty, ReportFont, 8, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(headingtable, reportObject.GunDispositionData.Tables["OUTPUT"].Rows[1][1].ToString(), ReportFont, 3, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);

                WriteCell(headingtable, string.Empty, ReportFont, 8, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(headingtable, "Store #: " + reportObject.ReportStore, ReportFont, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                //row 3
                WriteCell(headingtable, ReportHeaders.OPERATIONAL, ReportFont, 8, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(headingtable, "Run Date: " + DateTime.Now.ToShortDateString(), ReportFont, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                //row 4
                WriteCell(headingtable, reportObject.ReportParms[(int)ReportEnums.STARTDATE].ToString() + " to " + reportObject.ReportParms[(int)ReportEnums.ENDDATE].ToString(), ReportFont, 8, Element.ALIGN_LEFT, Element.ALIGN_TOP, Rectangle.NO_BORDER);
                WriteCell(headingtable, "Report #: " + reportObject.ReportNumber, ReportFont, 3, Element.ALIGN_LEFT, Rectangle.NO_BORDER);

                WriteCell(headingtable, string.Empty, ReportFont, columnCount, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(headingtable, string.Empty, ReportFont, columnCount, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(headingtable, string.Empty, ReportFont, columnCount, Element.ALIGN_LEFT, Rectangle.NO_BORDER);
                WriteCell(headingtable, string.Empty, ReportFont, columnCount, Element.ALIGN_LEFT, Rectangle.NO_BORDER);


                if (reportObject.ReportNumber == (int)ReportIDs.GunDispositionReport)
                    WriteCell(headingtable, "MULTIPLE  SALE  OR  OTHER  DISPOSITION  OF  PISTOLS  AND  REVOLVERS  ", ReportFontHeading, columnCount, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
                else
                    WriteCell(headingtable, "MULTIPLE  SALE  OR  OTHER  DISPOSITION  OF  CERTAIN RIFLES  ", ReportFontHeading, columnCount, Element.ALIGN_CENTER, Rectangle.NO_BORDER);
            }
            catch (Exception ex)
            {
                reportObject.ReportError = ex.Message;
                reportObject.ReportErrorLevel = 1;
                throw;
            }
            return;
        }

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
                cb.ShowText(reportObject.GunDispositionData.Tables["OUTPUT"].Rows[1][1].ToString());
                cb.SetTextMatrix(400, 720);
                cb.ShowText("Store #: " + reportObject.ReportStore);
                cb.SetTextMatrix(400, 708);
                cb.ShowText("RunDate: " + DateTime.Now.ToShortDateString());
                cb.SetTextMatrix(400, 696);

                cb.ShowText("Report #  " + reportObject.ReportNumber);

                // 12/2/10 Tracy -- updated x argument so all text was displayed
                cb.SetTextMatrix(12, 700);
                cb.ShowText(ReportHeaders.OPERATIONAL);
                cb.SetTextMatrix(12, 690);
                //  PWNU00000582 4/1/2010 SMurphy added date info
                //cb.ShowText(_dateRange);
                cb.ShowText(reportObject.ReportParms[(int)ReportEnums.STARTDATE] + " to " + reportObject.ReportParms[(int)ReportEnums.ENDDATE]);
                bf = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                //Font titleFont = new Font(bf, 12, Font.BOLD);
                cb.SetTextMatrix(100, 648);
                if (reportObject.ReportNumber == (int)ReportIDs.GunDispositionReport)
                    cb.ShowText("MULTIPLE  SALE  OR  OTHER  DISPOSITION  OF  PISTOLS  AND  REVOLVERS  ");
                else
                    cb.ShowText("MULTIPLE  SALE  OR  OTHER  DISPOSITION  OF  CERTAIN RIFLES  ");
            }
            catch (Exception ex)
            {
                reportObject.ReportError = ex.Message;
                reportObject.ReportErrorLevel = 1;
                throw;
            }
            return;
        }
        public override void OnOpenDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            try
            {
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                template = cb.CreateTemplate(50, 50);
                template2 = cb.CreateTemplate(580, 50);
                template3 = cb.CreateTemplate(400, 50);
                footerBaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            }
            catch (DocumentException)
            {
            }
            catch (IOException)
            {
            }
        }
        

        public override void OnStartPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnStartPage(writer, document);
            //PageCount++;
            try
            {
                int colspan = 11;
                PdfPTable headerTbl = new PdfPTable(colspan);
                headerTbl.TotalWidth = document.PageSize.Width - 55;
                //Image logo = Image.GetInstance(PawnReportResources.logo, BaseColor.WHITE);
                //I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
                System.Drawing.Bitmap bitmap = global::Common.Properties.Resources.logo;
                jpeg = Image.GetInstance(bitmap, BaseColor.WHITE);

                PrintPageHeader2(headerTbl, jpeg, colspan);

                //ReportColumns(headerTbl, colspan);

                //write the rows out to the PDF output stream. I use the height of the document to position the table. Positioning seems quite strange in iTextSharp and caused me the biggest headache.. It almost seems like it starts from the bottom of the page and works up to the top, so you may ned to play around with this.
                
                headerTbl.WriteSelectedRows(0, -10, 25, (document.PageSize.Height - 20), writer.DirectContent);
                //PageCount++;
            }
            catch (Exception)
            {
                return;
            }
        }

        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            /*
            string sPadding = string.Empty;
            sPadding = sPadding.PadLeft(90, ' ');
            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb = writer.DirectContent;
            int pageN = writer.PageNumber;
            string title = " MULTIPLE SALE OR OTHER DISPOSITION OF PISTOLS AND REVOLVERS";

            if (reportObject.ReportNumber == (int)ReportIDs.RifleDispositionReport)
                title = " MULTIPLE SALE OR OTHER DISPOSITION OF CERTAIN RIFLES";

            string text = title 
                            + sPadding
                            + "  Page " + pageN + " of ";

            float len = bf.GetWidthPoint(text, 8);

            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(10, 60);
            cb.ShowText("THIS REPORT DOES NOT REPLACE FORM ATF F 3310.4. YOU MUST STILL FILL OUT AND MAIL/FAX ATF F 3310.4 AS INSTRUCTED ON THAT FORM.");
            cb.SetTextMatrix(10, 30);
            cb.ShowText(text);
            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(280, 820);
            cb.EndText();
            cb.AddTemplate(template, 10 + len, 30);
            */
            int pageN = writer.PageNumber;
            string text = string.Empty;
            //string[] reportName = writer.Info.Get(PdfName.TITLE).ToString().Split(':');
            //text = PawnUtilities.String.StringUtilities.fillString(" ", 90) + " Page " + pageN + "  of  ";
            text = " Page " + pageN + "  of  ";
            Rectangle pageSize = document.PageSize;

            //add GunTotals
             string reportFooterText = "";
             //float len2 = footerBaseFont2.GetWidthPoint(reportFooterText, 8);
             cb.BeginText();
             cb.SetFontAndSize(footerBaseFont, 6);
             //Madhu fix for defexct PWNU00001411
             cb.SetTextMatrix(pageSize.GetLeft(35), pageSize.GetBottom(40));
             cb.ShowText(reportFooterText);
             cb.EndText();
             //Madhu fix for defexct PWNU00001411
             cb.AddTemplate(template2, pageSize.GetLeft(35), pageSize.GetBottom(40));

             //float len3 = footerBaseFont2.GetWidthPoint(reportFooterText2, 8);
             cb.BeginText();
             cb.SetFontAndSize(footerBaseFont, 6);
             //Madhu fix for defexct PWNU00001411
             cb.SetTextMatrix(pageSize.GetLeft(35), pageSize.GetBottom(20));
             cb.ShowText(reportFooterText);
             cb.EndText();
             //Madhu fix for defexct PWNU00001411
             cb.AddTemplate(template3, pageSize.GetLeft(35), pageSize.GetBottom(20));

            //add pageNumbers
            float len = footerBaseFont.GetWidthPoint(text, 8);
            cb.BeginText();
            cb.SetFontAndSize(footerBaseFont, 7);
            //Madhu fix for defexct PWNU00001411
            cb.SetTextMatrix(pageSize.GetLeft(540), pageSize.GetBottom(20));
            cb.ShowText(text);
            cb.EndText();
            //Madhu fix for defexct PWNU00001411
            cb.AddTemplate(template, pageSize.GetLeft(540) + (len - 5), pageSize.GetBottom(20));
        }

        public override void OnCloseDocument(PdfWriter writer, iTextSharp.text.Document document)
        {

            template2.BeginText();
            template2.SetFontAndSize(footerBaseFont, 6);
            template2.ShowText("THIS REPORT DOES NOT REPLACE FORM ATF F 3310.4. YOU MUST STILL FILL OUT AND MAIL/FAX ATF F 3310.4 AS INSTRUCTED ON THAT FORM.");
            template2.EndText();

            string title = " MULTIPLE SALE OR OTHER DISPOSITION OF PISTOLS AND REVOLVERS";

            if (reportObject.ReportNumber == (int)ReportIDs.RifleDispositionReport)
                title = " MULTIPLE SALE OR OTHER DISPOSITION OF CERTAIN RIFLES";

            template3.BeginText();
            template3.SetFontAndSize(footerBaseFont, 6);
            template3.ShowText(title);
            template3.EndText();

            template.BeginText();
            template.SetFontAndSize(footerBaseFont, 7);
            template.ShowText((writer.PageNumber - 1).ToString());
            template.EndText();
        }
    }
}
