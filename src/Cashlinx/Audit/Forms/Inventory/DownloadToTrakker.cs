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
using Common.Libraries.Utility.Exception;

namespace Audit.Forms.Inventory
{
    public partial class DownloadToTrakker : TrakkerTransferBase
    {
        public DownloadToTrakker()
        {
            InitializeComponent();
        }

        private bool Downloaded { get; set; }
        private List<TrakkerItem> TrakkerItems { get; set; }
        private ProcessingMessage ProcessingMessage { get; set; }

        protected override void SetUILabels()
        {
            ChangeHeadingMessage("Inventory Audit Trakker Download");
            ChangeStatusMessage("Click Continue when trakker ready to receive", Color.Black);
            ChangeStatusMessage1("Records to be Downloaded");
            ChangeStatusMessage2(string.Empty);
            ChangeStatusValue2(string.Empty);
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (Downloaded)
            {
                NavControlBox.Action = NavBox.NavAction.SUBMIT;
                return;
            }

            //ConfirmTrakker confirmTrakker = new ConfirmTrakker(TrakkerId);
            //if (confirmTrakker.ShowDialog() == DialogResult.Cancel)
            //{
            //    return;
            //}

            UploadFiles();
            SetButtonStates();

            if (!Downloaded)
            {
                return;
            }

            ADS.ActiveAudit.DownloadDate = ShopDateTime.Instance.FullShopDateTime;
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            InventoryAuditProcedures.EditAudit(ADS.ActiveAudit, dataContext);

            if (!dataContext.Result)
            {
                MessageBox.Show(dataContext.ErrorText);
                return;
            }
        }

        private void UploadFiles()
        {
            try
            {
                string tmpPath = Path.Combine(Application.StartupPath, "tmp");
                string trakkerPath = Path.Combine(tmpPath, "downtrak");
                string trakkerScPath = Path.Combine(tmpPath, "downtrak.sc");
                string tagXRefPath = Path.Combine(tmpPath, "tagxref.dat");

                if (!Directory.Exists(tmpPath))
                {
                    Directory.CreateDirectory(tmpPath);
                }

                int trakkerId = 0;

                TrakkerFileWriter trakkerFileWriter = new TrakkerFileWriter(trakkerPath);
                trakkerFileWriter.WriteFile(TrakkerItems, trakkerId);

                TrakkerScFileWriter trakkerScFileWriter = new TrakkerScFileWriter(trakkerScPath);
                trakkerScFileWriter.WriteFile(TrakkerItems);

                TagXRefWriter tagXRefWriter = new TagXRefWriter(tagXRefPath);
                tagXRefWriter.WriteFile(TrakkerItems);

                FtpHelper ftpHelper = new FtpHelper(FtpHost, FtpUser, FtpPassword);
                UploadFile(ftpHelper, trakkerPath, "tmp_" + ADS.ActiveAudit.StoreNumber + "/downtrak");
                UploadFile(ftpHelper, trakkerScPath, "tmp_" + ADS.ActiveAudit.StoreNumber + "/downtrak.sc");
                UploadFile(ftpHelper, tagXRefPath, "tmp_" + ADS.ActiveAudit.StoreNumber + "/tagxref.dat");

                //ChangeStatusMessage2("Records Downloaded to Trakker " + TrakkerId);
                ChangeStatusMessage2("Records Downloaded to Trakker");
                ChangeStatusValue2(TrakkerItems.Count.ToString());
                Downloaded = true;
            }
            catch (Exception exc)
            {
                BasicExceptionHandler.Instance.AddException("Failed to upload trakker files", exc);
                MessageBox.Show(exc.Message);
            }
        }

        private void UploadFile(FtpHelper ftpHelper, string localPath, string remotePath)
        {
            ftpHelper.UploadFile(remotePath, localPath, false);
        }

        private void DownloadToTrakker_Load(object sender, EventArgs e)
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
            Downloaded = false;
            ChangeStatusMessage2(string.Empty);
            ChangeStatusValue2(string.Empty);
            SetButtonStates();
        }

        private void SetButtonStates()
        {
            btnAdditionalTracker.Enabled = Downloaded;
            btnContinue.Text = Downloaded ? "Close" : "Continue";
        }
    }
}
