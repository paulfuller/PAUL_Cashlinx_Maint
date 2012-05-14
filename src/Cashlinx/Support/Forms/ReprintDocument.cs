using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database;
using Common.Controllers.Database.Couch;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Forms;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Document = Common.Libraries.Objects.Doc.Document;

namespace Support.Forms
{
    public partial class ReprintDocument : CustomBaseForm
    {
        public string DocumentName { private set; get; }

        public string StorageId { private set; get; }

        public Document.DocTypeNames DocumentType { private set; get; }

        private bool hasLaserInfo;
        private bool hasReceiptInfo;
        private string laserIp;
        private string receiptIp;
        private int laserPort;
        private int receiptPort;
        private bool _isLookup = false;

        private DocumentHelper documentHelper;

        /// <summary>
        /// Expects document information so as to know what to print.
        /// </summary>
        /// <param name="docLabelName"></param>
        /// <param name="docStorageId"></param>
        /// <param name="dType"></param>
        public ReprintDocument(
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
            this._isLookup = false;

            InitControl();
        }

        /// <summary>
        /// Assumes the document is being called for lookup.
        /// </summary>
        public ReprintDocument()
        {
            InitializeComponent();
            //this.DocumentName = docLabelName;
            //this.DocumentType = dType;
            //this.StorageId = docStorageId;
            this.hasLaserInfo = false;
            this.hasReceiptInfo = false;
            this.laserIp = string.Empty;
            this.receiptIp = string.Empty;
            this.laserPort = 0;
            this.receiptPort = 0;
            this._isLookup = true;

            InitControl();
        }

        private void InitControl()
        {
            documentHelper = new DocumentHelper();
            if (!_isLookup)
            {
                //Enable the lookup controls.
                this.productTypeList1.Visible = false;
                this.txtTicketNumber.Visible = false;
                this.lblTicket.Visible = false;
                this.lblProduct.Visible = false;
            }
            else
            {
                //Hide document name as it provides no value here.
                this.customLabel1.Visible = false;
            }
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
            this.hasLaserInfo = GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid;
            this.laserIp = GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress;
            this.laserPort = GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port;

            //Get receipt printer info
            this.hasReceiptInfo = GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.IsValid;
            this.receiptIp = GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.IPAddress;
            this.receiptPort = GlobalDataAccessor.Instance.DesktopSession.ReceiptPrinter.Port;
        }

        private void viewDocButton_Click(object sender, EventArgs e)
        {
            if (_isLookup)
            {
                if (!SetStorageIDFromLookup()) return;
            }

            //Ensure that the button is not clicked twice
            this.viewDocButton.Enabled = false;

            documentHelper.StorageId = StorageId;
            documentHelper.DocumentType = DocumentType;
            if (documentHelper.View(GlobalDataAccessor.Instance.DesktopSession))
            {
                this.Close();
            }

            //Re-enable show button
            this.viewDocButton.Enabled = true;
        }

        /// <summary>
        /// Sets the storage id from the lookup data.
        /// </summary>
        /// <returns>True upon success.</returns>
        private bool SetStorageIDFromLookup()
        {
            var productsList =
            (System.Windows.Forms.ComboBox)productTypeList1.Controls[0];
            var ticketNumber = -1;

            if (productsList.SelectedIndex == 0
                || !int.TryParse(txtTicketNumber.Text.Trim(), out ticketNumber))
            {
                System.Windows.Forms.MessageBox.Show(
                    "Please ensure you have selected a product and ticket number.");
                return false;
            }

            var docInfo = new CouchDbUtils.PawnDocInfo();

            string errString;
            docInfo.StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            if (productsList.SelectedValue.ToString() == DocumentHelper.RECIEPT)
            {
                docInfo.ReceiptNumber = ticketNumber;
                docInfo.SetDocumentSearchType(CouchDbUtils.DocSearchType.RECEIPT);
            }
            else
            {
                docInfo.TicketNumber = ticketNumber;
                docInfo.SetDocumentSearchType(CouchDbUtils.DocSearchType.STORE_TICKET);
            }

            //Also set the meta tag here as well as search type.
            if (productsList.SelectedValue.Equals(DocumentHelper.CUSTOMER_BUY) ||
                productsList.SelectedValue.Equals(DocumentHelper.PURCHASE_RETURN))
            {
                docInfo.AuxInfo = PurchaseDocumentGenerator.PURCHASE_AUXINFOTAG;
            }
            else if (productsList.SelectedValue.Equals(DocumentHelper.PAWN_LOAN))
            {
                PawnLoan pawnLoan;
                PawnAppVO pApp;
                CustomerVO customerVO;
                string errorCode;
                string errorText;

                if (!CustomerLoans.GetPawnLoan(GlobalDataAccessor.Instance.DesktopSession, Utilities.GetIntegerValue(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber), ticketNumber, "0", StateStatus.BLNK, true,
                                                   out pawnLoan, out pApp, out customerVO, out errorCode, out errorText))
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error trying to get loan data" + errorText);
                    MessageBox.Show("Error trying to get loan details for the number entered.");
                    return false;
                }

                if (pawnLoan.DateMade.Date != ShopDateTime.Instance.FullShopDateTime.Date)
                {
                    FileLogger.Instance.logMessage(LogLevel.WARN, this, "Cannot reprint loan documents for " + ticketNumber.ToString() + " on a previous date");
                    MessageBox.Show("This document is not eligible for reprint after the loan date.");
                    return false;
                }
            }
            else if (productsList.SelectedValue.Equals(DocumentHelper.RECIEPT))
            {
                this.DocumentType = Document.DocTypeNames.RECEIPT;
            }
            else
            {
                throw new NotImplementedException("This product has not been implemented yet.");
            }

            var lastStorageId = documentHelper.GetStorageId(Utilities.GetStringValue(productsList.SelectedValue, null), docInfo, out errString);

            if (!string.IsNullOrEmpty(lastStorageId))
            {
                this.StorageId = lastStorageId;
            }
            else
            {
                MessageBox.Show("Could not find the document.");
                return false;
            }

            return true;
        }

        private void printDocButton_Click(object sender, EventArgs e)
        {
            if (productTypeList1.ComboBox.SelectedValue != null)
            {
                if (productTypeList1.ComboBox.SelectedValue.ToString() == DocumentHelper.REPRINT_TAGS)
                {
                    this.Hide();
                    var reprintTag = new ReprintTag();
                    reprintTag.ShowDialog(this);
                    this.Close();
                    return;
                }
            }
            if (_isLookup)
            {
                if (!SetStorageIDFromLookup())
                    return;
            }

            //TODO: Finalize printing
            this.printDocButton.Enabled = false;

            //Get the accessors
            var cC = GlobalDataAccessor.Instance.CouchDBConnector;

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
                this.ShowErrorMessage("print", errText);
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
                    this.ShowErrorMessage("print", "Cannot retrieve file data to print file");
                    this.Close();
                    return;
                }

                //Send byte stream to printer based on type
                var printSuccess = false;
                switch (this.DocumentType)
                {
                    case Document.DocTypeNames.PDF:
                        //If we do not have the laser printer info, get out
                        if (!hasLaserInfo)
                        {
                            pLoadMesg.Close();
                            pLoadMesg.Dispose();
                            this.ShowErrorMessage("print", "Cannot determine laser printer IP/Port");
                            this.Close();
                            return;
                        }
                        //Send raw pdf data to printer
                        var resStrPdf = GenerateDocumentsPrinter.printDocument(fileData, laserIp, laserPort, 1);
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
                            this.ShowErrorMessage("print", "Cannot determine receipt printer IP/Port");
                            this.Close();
                            return;
                        }
                        //Send raw receipt data to receipt printer
                        var resStrRec = GenerateDocumentsPrinter.printDocument(fileData,
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
                            this.ShowErrorMessage("print", "Cannot determine laser printer IP/Port");
                            this.Close();
                            return;
                        }
                        //Send raw text data to laser printer
                        var resStrTxt = GenerateDocumentsPrinter.printDocument(
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
                    this.ShowErrorMessage("print", "Reprint operation failed");
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

        private void ShowErrorMessage(string errOp, string errText)
        {
            DocumentHelper.ShowErrorMessage(errOp, errText, this, StorageId);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customLabel3_Click(object sender, EventArgs e)
        {
        }

        private void productTypeList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (productTypeList1.ComboBox.SelectedValue.ToString() != DocumentHelper.REPRINT_TAGS)
            {
                txtTicketNumber.Visible = true;
                lblTicket.Visible = true;
                printDocButton.Text = "Print";
                viewDocButton.Enabled = true;
            }
            else
            {
                txtTicketNumber.Visible = false;
                lblTicket.Visible = false;
                printDocButton.Text = "Continue";
                viewDocButton.Enabled = false;
            }
            //txtTicketNumber.Enabled = productTypeList1.ComboBox.SelectedIndex == 0 || productTypeList1.ComboBox.SelectedValue.ToString() != DocumentHelper.REPRINT_TAGS;
            //viewDocButton.Enabled = txtTicketNumber.Enabled;
        }
    }
}
