using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Audit.Logic;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Couch;
using Common.Controllers.Database.Couch.Impl;
using Common.Controllers.Security;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Audit;
using Common.Libraries.Objects.Doc;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;

namespace Audit.Forms.Inventory
{
    public partial class ClosedAudit : AuditWindowBase
    {
        public ClosedAudit()
        {
            InitializeComponent();

        }

        private void AuditResults_Load(object sender, EventArgs e)
        {
            if (ADS.ActiveAudit == null)
            {
                throw new NullReferenceException("ActiveAudit cannot be null.");
            }
            
            InventoryAuditVO audit = ADS.ActiveAudit;
            lblHeaderText.Text = lblHeaderText.Text.Replace("<Shop>", audit.StoreNumber);
            lblAuditCompleteDate.Text = audit.DateCompleted.ToString("MM/dd/yyyy h:mm tt");
            lblAuditor.Text = audit.InitiatedBy;
            lblAuditScope.Text = audit.AuditScope.ToString();
            lblAuditStartDate.Text = audit.DateInitiated.ToString("MM/dd/yyyy h:mm tt");
            lblUploadFromTrakker.Text = audit.UploadDate.ToString("MM/dd/yyyy h:mm tt");
            
            //lblItemsMissing.Text = string.Empty;
            //lblItemsUnexpected.Text = string.Empty;
            //lblCACCItemsMissing.Text = string.Empty;
            documentsPanel.Width = this.Width;
            LoadDocs();
        }

        private void LoadDocs()
        {
            List<CouchDbUtils.PawnDocInfo> pawnDocs;
            string errString = "";

            //If a legit ticket number was pulled, then continue.
            if (ADS.ActiveAudit.AuditId > 0)
            {
                //Instantiate docinfo which will return info we need to be able to 
                //call reprint ticket.
                CouchDbUtils.PawnDocInfo docInfo = new CouchDbUtils.PawnDocInfo();
                docInfo.SetDocumentSearchType(CouchDbUtils.DocSearchType.STORE_TICKET);
                docInfo.StoreNumber = ADS.ActiveAudit.StoreNumber; // CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber;
                docInfo.TicketNumber = ADS.ActiveAudit.AuditId;
                //Use couch DB to get the document.
                if (CouchDbUtils.GetPawnDocument(GlobalDataAccessor.Instance.OracleDA, GlobalDataAccessor.Instance.CouchDBConnector,
                                                    docInfo, false, out pawnDocs, out errString))
                {
                    if (pawnDocs != null)
                    {
                        int columnCount = 0;
                        int rowCount = 0;
                        int docCount = 0;
                        int panelHeight = 0;
                        documentsPanel.Controls.Clear();

                        foreach (CouchDbUtils.PawnDocInfo document in pawnDocs)
                        {
                            docCount++;
                            if (columnCount == 3)
                            {
                                   
                                rowCount = rowCount + 2;
                                columnCount = 0;
                                //documentsPanel.Height = documentsPanel.Height + panelHeight;
                            }
                            Label fileLabel = new Label();
                            fileLabel.Font = new Font(lblAuditScope.Font, FontStyle.Regular);
                            fileLabel.Dock = System.Windows.Forms.DockStyle.Fill;
                            PictureBox pic = new PictureBox();
                            pic.BackgroundImageLayout = ImageLayout.None;
                            //Label fileLabel = new Label();
                            //fileLabel.Font = new Font(PS_OriginationDateLabel.Font, FontStyle.Bold);
                            pic.Click += pic_Click;
                            pic.Cursor = Cursors.Hand;
                            pic.Tag = document.DocumentType.ToString();
                            pic.Name = document.StorageId;

                            fileLabel.Text = docCount + "." + document.AuxInfo;
                            
                            string tagText = "";
                            if (document.DocumentType ==
                                Document.DocTypeNames.PDF)
                            {
                                pic.BackgroundImage = Properties.Resources.pdf_icon;
                                    
                            }
                            documentsPanel.Controls.Add(pic, columnCount, rowCount);
                            int incrow = rowCount + 1;
                            documentsPanel.Controls.Add(fileLabel, columnCount, incrow);
                            columnCount++;
                            //if(panelHeight == 0)
                                //panelHeight = documentsPanel.Height;
                        }
                        
                    }
                }
            }
        }

        void pic_Click(object sender, EventArgs e)
        {

            if (ADS.ActiveAudit.AuditId > 0)
            {
                //Instantiate docinfo which will return info we need to be able to 
                //call reprint ticket.
                CouchDbUtils.PawnDocInfo docInfo = new CouchDbUtils.PawnDocInfo();
                docInfo.DocumentType = Document.DocTypeNames.PDF;
                /*docInfo.SetDocumentSearchType(CouchDbUtils.DocSearchType.STORAGE);
                docInfo.StoreNumber = ADS.ActiveAudit.StoreNumber;
                docInfo.TicketNumber = ADS.ActiveAudit.AuditId;
                int receiptNumber = 0;*/

                string storageID = ((PictureBox)sender).Name.ToString();

                if (!string.IsNullOrEmpty(storageID))
                {
                    //Get the accessors
                    SecuredCouchConnector cC = GlobalDataAccessor.Instance.CouchDBConnector;

                    //Retrieve the document data
                    var pLoadMesg = new ProcessingMessage("* Loading Document *");

                    //Connect to couch and retrieve document
                    Document doc;
                    string errText;
                    if (!CouchDbUtils.GetDocument(
                        storageID,
                        cC, out doc, out errText))
                    {
                        pLoadMesg.Close();
                        pLoadMesg.Dispose();
                        this.showErrorMessage("view", errText, storageID);
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
                            this.showErrorMessage("view", "Cannot retrieve file data to show file", storageID);
                            pLoadMesg.Close();
                            pLoadMesg.Dispose();
                            this.Close();
                            return;
                        }

                        //Create temporary file
                        if (!this.createTempFile(fileData, out tmpFileName))
                        {
                            this.showErrorMessage("view", "Cannot generate file data to show file", storageID);
                            pLoadMesg.Close();
                            pLoadMesg.Dispose();
                            this.Close();
                            return;
                        }
                        pLoadMesg.Close();
                        pLoadMesg.Dispose();
                        switch (docInfo.DocumentType)
                        {
                            case Document.DocTypeNames.PDF:
                                AuditDesktopSession.ShowPDFFile(tmpFileName, true);
                                break;
                        }
                    }
                }
            }
        }

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
                SecurityAccessor.Instance.EncryptConfig.ClientConfig.
                GlobalConfiguration.BaseLogPath +
                @"\filedata" + DateTime.Now.Ticks + ".out";

                //Create the file
                FileStream fStream = File.Create(fileName);

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

        private void showErrorMessage(string errOp, string errText, string storageId)
        {
            string errMsg = string.Format("Could not {0} document: {1} : Storage Id = {2}",
                                          errOp,
                                          errText,
                                          storageId);
            if (FileLogger.Instance.IsLogError)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, errMsg);
            }
            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
            MessageBox.Show("Cannot " + errOp + " document: " + errMsg, "Pawn Error Message");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }
    }
}
