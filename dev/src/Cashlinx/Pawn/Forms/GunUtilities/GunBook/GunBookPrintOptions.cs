/********************************************************************************************
* Namespace: CashlinxDesktop.DesktopForms.GunUtilities
* FileName: GunBookPrintOptions
*This class displays the print options for Gun Book report. After selecting the report option, 
*this class gets the gun records from database and forwards to the class to print the reprot.
* Sreeny Chintha   Initial version 02/05/10
 * SR 6/14/2010 Changed the call to print to call the itextsharp version. 
 * SR 6/15/2010 Added couchdb call to insert the document
****************************************************************************************************/

using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Controllers.Database.Couch;
using Common.Controllers.Security;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;
using Reports;
using Document = Common.Libraries.Objects.Doc.Document;

namespace Pawn.Forms.GunUtilities.GunBook
{
     /// <summary/>
  
    public partial class GunBookPrintOptions : Form
    {
        public const string NEW = "NEW";
        public const string REPRINT = "REPRINT";
        public const string OPEN = "OPEN";
        public const string CLOSE = "CLOSE";
        public const string ALL = "ALL";
        private string message = "";

        public NavBox NavControlBox;
        public GunBookPrintOptions()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();

        }

       private void printButton_Click(object sender, EventArgs e)
        {

            int printOption = -1;
            int startPage = -1;
            int endPage = -1;

            if (radioButton1.Checked)
                printOption = 1;
            else if (radioButton2.Checked)
                printOption = 2;
            else if (radioButton3.Checked)
            {
                if (!validate())
               {
                   MessageBox.Show(message, "Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   return;
               }
               startPage = Int32.Parse(startPageTextBox.Text);
               endPage = Int32.Parse(endPageTextBox.Text);
               printOption = 3;

            }
            else if (radioButton4.Checked)
                printOption = 4;
            else if (radioButton5.Checked)
                printOption = 5;
            else if (radioButton6.Checked)
                printOption = 6;

            DataTable data = null;
           var errorCode = string.Empty;
           var errorMessage = string.Empty;
           String reportTitle = "Gun Book";
           var reportType = string.Empty;
           bool reprintGunRecords = false;
           String userName = GlobalDataAccessor.Instance.DesktopSession.UserName;
            string storeNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            switch (printOption)
            {
                case 1:
                    GunBookUtilities.getGunbookRecords(NEW, -1, -1, "", storeNumber, userName, out data, out errorCode, out errorMessage);
                    break;
                case 2:
                    GunBookUtilities.getGunbookRecords(REPRINT, -1, -1, "", storeNumber, userName, out data, out errorCode, out errorMessage);
                    //reprintGunRecords = true;
                    break;
                case 3:
                    GunBookUtilities.getGunbookRecords("", startPage, endPage, "", storeNumber, userName, out data, out errorCode, out errorMessage);
                    break;
                case 4:
                    GunBookUtilities.getGunbookRecords("", -1, -1, ALL, storeNumber, userName, out data, out errorCode, out errorMessage);
                    break;
                case 5:
                    reportTitle = "Report of All Open Gun Records as of "+ ShopDateTime.Instance.ShopDateCurTime;
                    GunBookUtilities.getGunbookRecords("", -1, -1, OPEN, storeNumber, userName, out data, out errorCode, out errorMessage);
                    reportType="Open";
                    break;
                case 6:
                    reportTitle = "Report of All Closed Gun Records as of "+ShopDateTime.Instance.ShopDateCurTime;
                    GunBookUtilities.getGunbookRecords("", -1, -1, CLOSE, storeNumber, userName, out data, out errorCode, out errorMessage);
                    reportType="Closed";
                    break;
                default:
                    MessageBox.Show("Please select one option","Message", MessageBoxButtons.OK);
                    return;
                    
            }
            if (data == null || data.Rows.Count == 0)
            {
                if (printOption == 3)
                    MessageBox.Show("The page range indicated does not exist."); 
                else
                    MessageBox.Show("There are no new or updated records since the last print date");
            }
            else
            {

                Cursor = Cursors.WaitCursor;
                //PrintGunBook myForm = new PrintGunBook(data, reportTitle);
                //myForm.ShowDialog();
                GunBookUtility gunBookPrinting = new GunBookUtility(PdfLauncher.Instance);

                ReportObject rptObj = new ReportObject();
                rptObj.ReportTitle = reportTitle;
                rptObj.ReportTempFileFullName = string.Format("{0}\\GunBook{1}.pdf", SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath, DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF"));
                rptObj.ReportStore = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                rptObj.ReportStoreDesc = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
                rptObj.ReportError = string.Empty;
                rptObj.ReportErrorLevel = 0;
                rptObj.ReportParms.Add(reportType);
                gunBookPrinting.GunBookData = data;
                gunBookPrinting.RptObject = rptObj;
                if (gunBookPrinting.Print())
                {
                    //Print the Gun Book
                    if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
                    {
                        string strReturnMessage;
                        if (GlobalDataAccessor.Instance.DesktopSession.PDALaserPrinter.IsValid)
                        {
                            if (FileLogger.Instance.IsLogInfo)
                            {
                                FileLogger.Instance.logMessage(LogLevel.INFO, this, "Printing gunbook report on PDA Laser printer: {0}",
                                                               GlobalDataAccessor.Instance.DesktopSession.PDALaserPrinter);
                            }
                            strReturnMessage = PrintingUtilities.printDocument(
                                rptObj.ReportTempFileFullName, 
                                GlobalDataAccessor.Instance.DesktopSession.PDALaserPrinter.IPAddress,
                                GlobalDataAccessor.Instance.DesktopSession.PDALaserPrinter.Port, 
                                1);
                        }
                        else if (GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
                        {
                            if (FileLogger.Instance.IsLogWarn)
                            {
                                FileLogger.Instance.logMessage(LogLevel.WARN, this,
                                                               "Could not find valid PDA laser printer to print the gunbook report." + Environment.NewLine + 
                                                               " Printing on default pawn laser printer: {0}", 
                                                               GlobalDataAccessor.Instance.DesktopSession.LaserPrinter);
                            }                            
                            strReturnMessage = PrintingUtilities.printDocument(
                                rptObj.ReportTempFileFullName,
                                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port,
                                1);
                        }
                        else
                        {
                            if (FileLogger.Instance.IsLogError)
                            {
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                               "Could not find a valid laser printer to print the gunbook report");
                            }
                            strReturnMessage = "FAIL - NO PRINTER FOUND";
                        }
                        if (strReturnMessage.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) == -1)
                        {
                            if (FileLogger.Instance.IsLogError)
                            {
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                               "Cannot print Gun Book utility report: " + strReturnMessage);
                            }
                        }
                        //Store Gun Book report
                        var cds = GlobalDataAccessor.Instance.DesktopSession;
                        if (cds != null)
                        {
                            var pDoc = new CouchDbUtils.PawnDocInfo
                            {
                                UseCurrentShopDateTime = true,
                                StoreNumber = cds.CurrentSiteId.StoreNumber,
                                DocumentType = Document.DocTypeNames.PDF,
                                DocFileName = rptObj.ReportTempFileFullName
                            };

                            //Set document add calls

                            //Add this document to the pawn document registry and document storage
                            string errText;
                            if (!CouchDbUtils.AddPawnDocument(GlobalDataAccessor.Instance.OracleDA, GlobalDataAccessor.Instance.CouchDBConnector,
                                cds.UserName, ref pDoc, out errText))
                            {
                                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot store Gun Book utility report!");
                            }
                        }

                        // File.Delete(rptObj.ReportTempFileFullName);
                        MessageBox.Show(@"Gun Book Utility Printing Complete");
                    }
                }
                else
                {
                    MessageBox.Show(@"Failed to generate gun book document");
                }
            }
            Cursor = Cursors.Default;

        }

        private bool validate()
        {
            bool isValid = true;
            message = "";
            Regex pageNumber = new Regex("^[1-9][0-9]*$");
            if (!pageNumber.IsMatch(startPageTextBox.Text))
            {
                isValid = false;
                message = "Please enter valid Page number for start page ";
            }
            if (!pageNumber.IsMatch(endPageTextBox.Text))
            {
                isValid = false;
                message = message+"\nPlease enter valid Page number for end page ";
            }

            if (isValid)
            {   
                if (int.Parse(startPageTextBox.Text) > int.Parse(endPageTextBox.Text))
                {
                    isValid = false;
                    message = "Start page should be less than or Equal to end page Number";
                }

            }

            return isValid;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            //this.NavControlBox.Owner = this;
            //this.NavControlBox.Action = NavBox.NavAction.CANCEL;
            this.Close();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton3.Checked)
            {
                this.startPageTextBox.Enabled = true;
                this.endPageTextBox.Enabled = true;
                this.startPageTextBox.BackColor = System.Drawing.Color.White;
                this.endPageTextBox.BackColor = System.Drawing.Color.White;
            }
            else
            {
                this.startPageTextBox.Enabled = false;
                this.endPageTextBox.Enabled = false;
                this.startPageTextBox.BackColor = System.Drawing.Color.LightGray ;
                this.endPageTextBox.BackColor = System.Drawing.Color.LightGray;
            }

        }
    }
}
