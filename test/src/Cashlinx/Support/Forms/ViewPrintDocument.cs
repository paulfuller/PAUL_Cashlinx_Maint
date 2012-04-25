using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Couch;
using Common.Controllers.Database.Couch.Impl;
using Common.Controllers.Security;
using Common.Libraries.Forms;
using Common.Libraries.Forms.Components;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Support.Logic;
using Document = Common.Libraries.Objects.Doc.Document;
using ReceiptRenderer = Common.Libraries.Forms.ReceiptRenderer;

namespace Support.Forms
{
    public partial class ViewPrintDocument : CustomBaseForm
    {
        private bool hasLaserInfo;
        private bool hasReceiptInfo;
        private string laserIp;
        private string receiptIp;
        private int laserPort;
        private int receiptPort;
        private string _receiptNo = string.Empty;
        private CouchDbUtils.PawnDocInfo _docInfo = null;

        #region Public Properties
        public CouchDbUtils.PawnDocInfo DocInfo
        {
            get
            {
                return _docInfo;
            }
            set
            {
                _docInfo = value;
            }
        }

        public string DocumentName { private set; get; }

        public string StorageId { private set; get; }

        public Document.DocTypeNames DocumentType { private set; get; }

        #endregion

        public ViewPrintDocument(
            string docLabelName, 
            string docStorageId,
            Document.DocTypeNames dType)
        {
            InitializeComponent();
            this.DocumentName = docLabelName;
            this.DocumentType = dType;
            this.StorageId = docStorageId;
            this.hasLaserInfo = false;
            this.hasReceiptInfo = false;
            this.laserIp = string.Empty;
            this.receiptIp = string.Empty;
            this.laserPort = 0;
            this.receiptPort = 0;
        }

        public ViewPrintDocument(
            string docLabelName,
            string receiptNo,
            string docStorageId,
            Document.DocTypeNames dType,
            CouchDbUtils.PawnDocInfo docInfo)
        {
            InitializeComponent();
            this.DocumentName = docLabelName + receiptNo;
            this.DocumentType = dType;
            this.StorageId = docStorageId;
            this.hasLaserInfo = false;
            this.hasReceiptInfo = false;
            this.laserIp = string.Empty;
            this.receiptIp = string.Empty;
            this.laserPort = 0;
            this.receiptPort = 0;
            this.DocInfo = docInfo;
        }

        private void ReprintDocument_Load(object sender, EventArgs e)
        {
            //Clear document label
            this.documentNameLabel.Text = " ";

            if (!string.IsNullOrEmpty(this.DocumentName))
            {
                //Set document label string
                this.documentNameLabel.Text = this.DocumentName;
            }

            //If printing is disabled, disable print button
            if (!SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
            {
                this.printDocButton.Enabled = false;
            }

            //Get laser printer info
            var dSession = GlobalDataAccessor.Instance.DesktopSession;
            this.hasLaserInfo = dSession.LaserPrinter.IsValid;
            this.laserIp = dSession.LaserPrinter.IPAddress;
            this.laserPort = dSession.LaserPrinter.Port;

            //Get receipt printer info
            this.hasReceiptInfo = dSession.ReceiptPrinter.IsValid;
            this.receiptIp = dSession.ReceiptPrinter.IPAddress;
            this.receiptPort = dSession.ReceiptPrinter.Port;
        }

        private string ByteArrayToString(byte[] btData)
        {
            var sStringData = string.Empty;

            try
            {
                var enc = Encoding.ASCII;
                sStringData = enc.GetString(btData);
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return sStringData;
        }

        private void viewDocButton_Click(object sender, EventArgs e)
        {
            //Ensure that the button is not clicked twice
            this.viewDocButton.Enabled = false;

            //Get the accessors
            var cds = GlobalDataAccessor.Instance.DesktopSession;
            var cC = GlobalDataAccessor.Instance.CouchDBConnector;

            //Retrieve the document data
            var pLoadMesg = new ProcessingMessage("* Loading Document *");

            //Connect to couch and retrieve document
            Document doc;
            string errText;
            if (!CouchDbUtils.GetDocument(
                this.StorageId,
                cC, out doc, out errText))
            {
                pLoadMesg.Close();
                pLoadMesg.Dispose();
                this.showErrorMessage("view", errText);
                this.Close();
            }
            else if (doc != null)
            {
                pLoadMesg.Message = "* Document Loaded...Displaying *";
                //Fetch data
                string tmpFileName;
                byte[] fileData;
                if (!doc.GetSourceData(out fileData))
                {
                    this.showErrorMessage("view", "Cannot retrieve file data to show file");
                    pLoadMesg.Close();
                    pLoadMesg.Dispose();
                    this.Close();
                    return;
                }

                //Create temporary file
                if (!this.createTempFile(fileData, out tmpFileName))
                {
                    this.showErrorMessage("view", "Cannot generate file data to show file");
                    pLoadMesg.Close();
                    pLoadMesg.Dispose();
                    this.Close();
                    return;
                }
                pLoadMesg.Close();
                pLoadMesg.Dispose();
                switch (this.DocumentType)
                {
                    case Document.DocTypeNames.PDF:
                        DesktopSession.ShowPDFFile(tmpFileName, true);
                        break;
                    case Document.DocTypeNames.RECEIPT:
                        //GJL - do nothing for now until complete receipt renderer
                        ReceiptRenderer receiptR = new ReceiptRenderer(fileData);
                        //Desktop.PrintQueue.ReceiptRenderer receiptR = new Desktop.PrintQueue.ReceiptRenderer(fileData);
                        //receiptDataRetrieved = receiptR.ExecuteServicePawnLoanForReceiptRenderer(this.DocInfo);
                        
                        //receiptR.RichTextBoxContent = bbyteArrayString;
                        receiptR.ShowDialog();
                        //MessageBox.Show("Show receipt renderer");
                        //Invoke View receipt rendering form
                        break;
                    case Document.DocTypeNames.BARCODE:
                        MessageBox.Show("Nothing to show");
                        //Nothing for now)
                        break;
                    case Document.DocTypeNames.TEXT:
                        MessageBox.Show("Nothing to show");
                        //Nothing for now)
                        //Notepad??
                        break;
                    case Document.DocTypeNames.BINARY:
                        MessageBox.Show("Nothing to show");
                        //Nothing for now)
                        //Nothing for now
                        break;
                    case Document.DocTypeNames.INVALID:
                        MessageBox.Show("Nothing to show");
                        //Nothing for now)
                        //Nothing for now
                        break;
                }

                //Delete temporary file
                try
                {
                    File.Delete(tmpFileName);
                }
                catch (Exception eX)
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not delete file: {0}  Exception: {1}", tmpFileName, eX);
                    }
                }
            }

            //Re-enable show button
            this.viewDocButton.Enabled = true;
        }

        private void printDocButton_Click(object sender, EventArgs e)
        {
            //TODO: Finalize printing
            this.printDocButton.Enabled = false;

            //Get the accessors
            SecuredCouchConnector cC = GlobalDataAccessor.Instance.CouchDBConnector;

            //Retrieve the document data
            var pLoadMesg = new ProcessingMessage("* Printing Document *");

            //Connect to couch and retrieve document
            Document doc;
            string errText;
            if (!CouchDbUtils.GetDocument(
                this.StorageId,
                cC, out doc, out errText))
            {
                pLoadMesg.Close();
                pLoadMesg.Dispose();
                this.showErrorMessage("print", errText);
                this.Close();
            }
            else if (doc != null)
            {
                //Fetch data
                byte[] fileData;
                if (!doc.GetSourceData(out fileData))
                {
                    pLoadMesg.Close();
                    pLoadMesg.Dispose();
                    this.showErrorMessage("print", "Cannot retrieve file data to print file");
                    this.Close();
                    return;
                }

                //Send byte stream to printer based on type
                bool printSuccess = false;
                switch (this.DocumentType)
                {
                    case Document.DocTypeNames.PDF:
                        //If we do not have the laser printer info, get out
                        if (!hasLaserInfo)
                        {
                            pLoadMesg.Close();
                            pLoadMesg.Dispose();
                            this.showErrorMessage("print", "Cannot determine laser printer IP/Port");
                            this.Close();
                            return;
                        }
                        //Send raw pdf data to printer
                        string resStrPdf = GenerateDocumentsPrinter.printDocument(fileData, laserIp, laserPort, 1);
                        if (!string.IsNullOrEmpty(resStrPdf) && resStrPdf.Contains("SUCCESS"))
                        {
                            printSuccess = true;
                        }
                        break;
                    case Document.DocTypeNames.RECEIPT:
                        //If we do not have the receipt printer info, get out
                        if (!hasReceiptInfo)
                        {
                            pLoadMesg.Close();
                            pLoadMesg.Dispose();
                            this.showErrorMessage("print", "Cannot determine receipt printer IP/Port");
                            this.Close();
                            return;
                        }
                        //Send raw receipt data to receipt printer
                        string resStrRec = GenerateDocumentsPrinter.printDocument(fileData,
                                                                                  receiptIp,
                                                                                  receiptPort,
                                                                                  1);
                        if (!string.IsNullOrEmpty(resStrRec) && resStrRec.Contains("SUCCESS"))
                        {
                            printSuccess = true;
                        }
                        break;
                    case Document.DocTypeNames.BARCODE:
                        MessageBox.Show("Nothing to print");
                        //Nothing for now - should go to Intermec printer
                        break;
                    case Document.DocTypeNames.TEXT:
                        //If we do not have the laser printer info, get out
                        if (!hasLaserInfo)
                        {
                            pLoadMesg.Close();
                            pLoadMesg.Dispose();
                            this.showErrorMessage("print", "Cannot determine laser printer IP/Port");
                            this.Close();
                            return;
                        }
                        //Send raw text data to laser printer
                        string resStrTxt = GenerateDocumentsPrinter.printDocument(
                            fileData,
                            laserIp,
                            laserPort,
                            1);
                        if (!string.IsNullOrEmpty(resStrTxt) && resStrTxt.Contains("SUCCESS"))
                        {
                            printSuccess = true;
                        }
                        break;
                    case Document.DocTypeNames.BINARY:
                        MessageBox.Show("Nothing to print");
                        //Nothing for now)
                        //Nothing for now
                        break;
                    case Document.DocTypeNames.INVALID:
                        MessageBox.Show("Nothing to print");
                        //Nothing for now)
                        //Nothing for now
                        break;
                }

                pLoadMesg.Close();
                pLoadMesg.Dispose();
                if (!printSuccess)
                {
                    this.showErrorMessage("print", "Reprint operation failed");
                }
                else
                {
                    MessageBox.Show("Document has successfully reprinted",
                                    "Pawn Application Message");
                }
            }

            //Re-enable print button
            this.printDocButton.Enabled = true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errOp"></param>
        /// <param name="errText"></param>
        private void showErrorMessage(string errOp, string errText)
        {
            string errMsg = string.Format("Could not {0} document: {1} : Storage Id = {2}",
                                          errOp,
                                          errText,
                                          this.StorageId);
            if (FileLogger.Instance.IsLogError)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, errMsg);
            }
            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
            MessageBox.Show("Cannot " + errOp + " document: " + errMsg, "Pawn Error Message");            
        }

        /// <summary>
        /// Creates a temporary file for viewing
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool createTempFile(byte[] fileData, out string fileName)
        {
            fileName = string.Empty;
            if (fileData == null || fileData.Length <= 0)
            {
                return (false);
            }

            try
            {
                //Create temp file name
                fileName =
                string.Format("{0}\\filedata{1}.out", SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath, DateTime.Now.Ticks);

                //Create the file
                var fStream = File.Create(fileName);

                //Write data to the file stream
                fStream.Write(fileData, 0, fileData.Length);

                //Close the file stream
                fStream.Close();
            }
            catch (Exception)
            {
                return (false);    
            }

            return (true);
        }
    }
}
