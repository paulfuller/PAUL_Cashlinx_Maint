using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Audit.Logic;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Libraries.Forms;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;

namespace Audit.Forms.Inventory
{
    public partial class UploadFromTrakker : TrakkerTransferBase
    {
        public UploadFromTrakker()
        {
            InitializeComponent();
        }

        private bool Uploaded { get; set; }
        private List<TrakkerItem> TrakkerItems { get; set; }
        private ProcessingMessage ProcessingMessage { get; set; }

        protected override void SetUILabels()
        {
            ChangeHeadingMessage("Inventory Audit Trakker Upload");
            ChangeStatusMessage("Click Continue when trakker ready to send", Color.Black);
            ChangeStatusMessage1("Records to be Uploaded");
            ChangeStatusMessage2(string.Empty);
            ChangeStatusValue2(string.Empty);
        }

        private void btnContinue_Click(object sender, System.EventArgs e)
        {
            if (Uploaded)
            {
                NavControlBox.Action = NavBox.NavAction.SUBMIT;
                return;
            }

            DownloadFiles();
            SetButtonStates();

            if (!Uploaded)
            {
                return;
            }
            ADS.ActiveAudit.UploadDate = ShopDateTime.Instance.FullShopDateTime;
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            InventoryAuditProcedures.EditAudit(ADS.ActiveAudit, dataContext);

            if (!dataContext.Result)
            {
                MessageBox.Show(dataContext.ErrorText);
                return;
            }
        }

        private void DownloadFiles()
        {
            ProcessingMessage = new ProcessingMessage("Retrieving upload files", 0);
            ProcessingMessage.Show();
            try
            {
                string tmpPath = Path.Combine(Application.StartupPath, "tmp");
                string trakkerPath = Path.Combine(tmpPath, "uptrak");
                string trakkerIdPath = Path.Combine(tmpPath, "trakker.id");

                if (!Directory.Exists(tmpPath))
                {
                    Directory.CreateDirectory(tmpPath);
                }

                FtpHelper ftpHelper = new FtpHelper(FtpHost, FtpUser, FtpPassword);
                ftpHelper.DownloadFile("tmp_" + ADS.ActiveAudit.StoreNumber + "/uptrak", trakkerPath, false);
                ftpHelper.DownloadFile("tmp_" + ADS.ActiveAudit.StoreNumber + "/trakker.id", trakkerIdPath, false);

                string[] fileContents = File.ReadAllLines(trakkerIdPath);

                if (fileContents.Length == 0 || string.IsNullOrWhiteSpace(fileContents[0]))
                {
                    ProcessingMessage.Close();
                    throw new ApplicationException("trakker.id file is empty");
                }

                int trakkerId = Utilities.GetIntegerValue(fileContents[0].Substring(fileContents[0].IndexOf('|') + 1), 0);

                ProcessingMessage.Hide();
                ConfirmTrakker confirmTrakker = new ConfirmTrakker(trakkerId);
                if (confirmTrakker.ShowDialog() == DialogResult.Cancel)
                {
                    ProcessingMessage.Close();
                    return;
                }
                ProcessingMessage.Show();

                TrakkerFileReader trakkerFileReader = new TrakkerFileReader(trakkerPath);
                List<TrakkerItem> uploadedItems = trakkerFileReader.ReadFile();

                int count = 0;
                if (uploadedItems.Count > 0)
                {
                    ADS.beginTransactionBlock();
                    foreach (TrakkerItem item in uploadedItems)
                    {
                        count++;
                        ProcessingMessage.Message = string.Format("Uploading record {0} of {1}", count, uploadedItems.Count);
                        CommonDatabaseContext dataContext = new CommonDatabaseContext();

                        InventoryAuditProcedures.UploadTrakkerItem(item, ADS.ActiveAudit.AuditId, trakkerId, ADS.ActiveAudit.StoreNumber, dataContext);

                        if (!dataContext.Result)
                        {
                            ADS.endTransactionBlock(EndTransactionType.ROLLBACK);
                            MessageBox.Show(dataContext.ErrorText);
                            return;
                        }
                    }
                    ADS.endTransactionBlock(EndTransactionType.COMMIT);
                }

                ProcessingMessage.Close();
                ChangeStatusMessage2("Records Uploaded from Trakker " + trakkerId);
                ChangeStatusValue2(uploadedItems.Count.ToString());
                Uploaded = true;
            }
            catch (Exception exc)
            {
                ProcessingMessage.Close();
                BasicExceptionHandler.Instance.AddException("Failed to upload trakker files", exc);
                MessageBox.Show(exc.Message);
            }
        }

        private void UploadFromTrakker_Load(object sender, EventArgs e)
        {
            SetButtonStates();
            BackgroundWorker bwLoadTrakkerItems = new BackgroundWorker();
            bwLoadTrakkerItems.DoWork += new DoWorkEventHandler(bwLoadTrakkerItems_DoWork);
            bwLoadTrakkerItems.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwLoadTrakkerItems_RunWorkerCompleted);
            bwLoadTrakkerItems.RunWorkerAsync();

            ProcessingMessage = new ProcessingMessage("Loading merchandise...", 0);
            ProcessingMessage.ShowDialog(this);
        }

        void bwLoadTrakkerItems_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProcessingMessage.Close();
            CommonDatabaseContext dataContext = e.Result as CommonDatabaseContext;

            if (dataContext != null && !dataContext.Result)
            {
                MessageBox.Show(dataContext.ErrorText);
                NavControlBox.Action = NavBox.NavAction.CANCEL;
                return;
            }

            if (TrakkerItems == null || TrakkerItems.Count == 0)
            {
                MessageBox.Show("Unable to load merchandise.");
                NavControlBox.Action = NavBox.NavAction.CANCEL;
                return;
            }

            ChangeStatusValue1(TrakkerItems.Count.ToString());
        }

        void bwLoadTrakkerItems_DoWork(object sender, DoWorkEventArgs e)
        {
            TrakkerItems = new List<TrakkerItem>();
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            InventoryAuditProcedures.GetTrakkerItems(TrakkerItems, ADS.ActiveAudit, dataContext);

            e.Result = dataContext;
        }

        private void btnAdditionalTracker_Click(object sender, EventArgs e)
        {
            Uploaded = false;
            ChangeStatusMessage2(string.Empty);
            ChangeStatusValue2(string.Empty);
            SetButtonStates();
        }

        private void SetButtonStates()
        {
            btnAdditionalTracker.Enabled = Uploaded;
            btnContinue.Text = Uploaded ? "Close" : "Continue";
        }
    }
}
