using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Security;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Doc;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using ReceiptRenderer = Common.Libraries.Forms.ReceiptRenderer;

namespace Common.Controllers.Database.Couch
{
    public class DocumentHelper
    {
        public const string CUSTOMER_BUY = "Buy";
        //public const string _VENDOR_BUY = "Vendor Buy";
        public const string PAWN_LOAN = "Pawn Loan";
        public const string PURCHASE_RETURN = "Purchase Return";
        public const string RECIEPT = "Receipt";
        public const string REPRINT_TAGS = "Reprint Tags";

        public DocumentHelper()
        {
        }

        public Document.DocTypeNames DocumentType { set; get; }

        public string StorageId { set; get; }

        public bool View(DesktopSession dSession)
        {
            //Get the accessors
            Document doc;
            var cds = dSession;
            var cC = GlobalDataAccessor.Instance.CouchDBConnector;

            //Retrieve the document data
            var pLoadMesg = new ProcessingMessage("* Loading Document *");

            //Connect to couch and retrieve document
            string errText;
            if (!CouchDbUtils.GetDocument(
                this.StorageId,
                cC, out doc, out errText))
            {
                pLoadMesg.Close();
                pLoadMesg.Dispose();
                this.ShowErrorMessage("view", errText);
                return true;
            }
            else if (doc != null)
            {
                pLoadMesg.Message = "* Document Loaded...Displaying *";
                //Fetch data
                string tmpFileName;
                byte[] fileData;
                if (!doc.GetSourceData(out fileData))
                {
                    this.ShowErrorMessage("view", "Cannot retrieve file data to show file");
                    pLoadMesg.Close();
                    pLoadMesg.Dispose();
                    return true;
                }

                //Create temporary file
                if (!this.CreateTempFile(fileData, out tmpFileName))
                {
                    this.ShowErrorMessage("view", "Cannot generate file data to show file");
                    pLoadMesg.Close();
                    pLoadMesg.Dispose();
                    return true;
                }
                pLoadMesg.Close();
                pLoadMesg.Dispose();
                string bbyteArrayString = ByteArrayToString(fileData);
                char[] buf = bbyteArrayString.ToCharArray();
                if (this.DocumentType == Document.DocTypeNames.RECEIPT && buf[0] == '%' && buf[1] == 'P' && buf[2] == 'D' && buf[3] == 'F')
                    this.DocumentType = Document.DocTypeNames.PDF;

                switch (this.DocumentType)
                {
                    case Document.DocTypeNames.PDF:
                        DesktopSession.ShowPDFFile(tmpFileName, true);
                        break;
                    case Document.DocTypeNames.RECEIPT:
                        //GJL - do nothing for now until complete receipt renderer
                        var receiptR = new ReceiptRenderer(fileData);
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

                //Reset the storage id in case it's called again.
                //this.StorageId = "";

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

            return false;
        }

        public void ShowErrorMessage(string errOp, string errText)
        {
            ShowErrorMessage(errOp, errText, this, StorageId);
        }

        public static void ShowErrorMessage(string errOp, string errText, object sender, string storageId)
        {
            string errMsg = string.Format("Could not {0} document: {1} : Storage Id = {2}",
                                          errOp,
                                          errText,
                                          storageId);
            if (FileLogger.Instance.IsLogError)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, sender, errMsg);
            }
            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
            MessageBox.Show("Cannot " + errOp + " document: " + errMsg, "Pawn Error Message");
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

        private bool CreateTempFile(byte[] fileData, out string fileName)
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
                string.Format("{0}\\filedata{1}.out", 
                    SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath, 
                    DateTime.Now.Ticks);

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

        public string GetStorageId(string selectedProduct, CouchDbUtils.PawnDocInfo docInfo, out string errString)
        {
            if (selectedProduct == null)
            {
                throw new ArgumentNullException("selectedProduct");
            }

            List<CouchDbUtils.PawnDocInfo> pawnDocs;
            var lastStorageId = string.Empty;
            if (CouchDbUtils.GetPawnDocument(GlobalDataAccessor.Instance.OracleDA, GlobalDataAccessor.Instance.CouchDBConnector,
                                             docInfo, false, out pawnDocs, out errString))
            {
                foreach (var doc in pawnDocs)
                {
                    if (selectedProduct == CUSTOMER_BUY ||
                        selectedProduct == PURCHASE_RETURN
                        && doc.AuxInfo == PurchaseDocumentGenerator.PURCHASE_AUXINFOTAG)
                    {
                        lastStorageId = doc.StorageId;
                    }

                    if (selectedProduct == PAWN_LOAN
                        && doc.AuxInfo == "" && doc.DocumentType == Document.DocTypeNames.PDF)
                    {
                        lastStorageId = doc.StorageId;
                        //Do not break here -- allow the storage id to be 
                        //set to the last storage id for PDF files returned.
                        //break;
                    }
                    if (selectedProduct == RECIEPT
                        && doc.DocumentType == Document.DocTypeNames.RECEIPT)
                    {
                        lastStorageId = doc.StorageId;
                    }
                }
            }

            return lastStorageId;
        }
    }
}
