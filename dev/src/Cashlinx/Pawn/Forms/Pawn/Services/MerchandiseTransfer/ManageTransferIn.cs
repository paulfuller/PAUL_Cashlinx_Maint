using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Components.EventArgs;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Shared;
//using ReportObject = Common.Libraries.Objects.ReportObject;

namespace Pawn.Forms.Pawn.Services.MerchandiseTransfer
{
    public partial class ManageTransferIn : CustomBaseForm
    {
        public ManageTransferIn()
        {
            InitializeComponent();
            NavControlBox = new NavBox
            {
                Owner = this
            };
        }

        # region Properties

        public NavBox NavControlBox { get; set; }

        private DesktopSession CDS
        {
            get
            {
                return GlobalDataAccessor.Instance.DesktopSession;
            }
        }

        private List<TransferVO> Transfers { get; set; }

        # endregion

        # region Event Handlers

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            TransferVO transfer = gvTransfers.SelectedRows[0].Tag as TransferVO;

            if (transfer == null)
            {
                return;
            }

            string errorCode = null;
            string errorText = null;
            string storeNumber = transfer.OriginalStoreNumber;

            if (transfer.TempStatus == TransferTempStatus.REJCT)
            {
                storeNumber = transfer.DestinationStoreNumber;
            }
            
            SiteId storeInfo = new SiteId();
            bool retValue = ShopProcedures.ExecuteGetStoreInfo(GlobalDataAccessor.Instance.OracleDA,
                    storeNumber, ref storeInfo, out errorCode, out errorText);

            if (!retValue || storeInfo == null || string.IsNullOrWhiteSpace(storeInfo.StoreAddress1))
            {
                MessageBox.Show("Error while loading store information.");
                return;
            }

            transfer.StoreInfo = storeInfo;


            retValue = TransferProcedures.GetTransferInItems(CDS, transfer, out errorCode, out errorText);

            if (!retValue)
            {
                MessageBox.Show("Error while loading transfer items.");
                return;
            }

            CDS.Transfers = new List<TransferVO>(1) { transfer };

            NavControlBox.IsCustom = false;
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        private void btnRejectTransfer_Click(object sender, EventArgs e)
        {
            TransferVO transfer = gvTransfers.SelectedRows[0].Tag as TransferVO;

            if (transfer == null)
            {
                return;
            }

            CDS.Transfers = new List<TransferVO>(1) { transfer };

            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "RejectComments";
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        private void gvTransfers_SelectionChanged(object sender, EventArgs e)
        {
            var selectedTransfers = (from DataGridViewRow row in gvTransfers.SelectedRows
                                     where row.Tag is TransferVO
                                     select row.Tag as TransferVO);

            btnContinue.Enabled = btnPrint.Enabled = gvTransfers.SelectedRows.Count > 0;
            btnRejectTransfer.Enabled = selectedTransfers.Any() && !selectedTransfers.Any(t => t.TempStatus == TransferTempStatus.REJCT);
        }

        # endregion

        private void ManageTransferIn_Load(object sender, EventArgs e)
        {
        }

        private void ManageTransferIn_Shown(object sender, EventArgs e)
        {
            gvTransfers.Rows.Clear();

            BackgroundWorker workerLoadTransfers = new BackgroundWorker();
            workerLoadTransfers.DoWork += new DoWorkEventHandler(workerLoadTransfers_DoWork);
            workerLoadTransfers.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerLoadTransfers_RunWorkerCompleted);
            workerLoadTransfers.RunWorkerAsync();
        }

        void workerLoadTransfers_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gvTransfers_SelectionChanged(sender, e);
        }

        void workerLoadTransfers_DoWork(object sender, DoWorkEventArgs e)
        {
            string errorCode, errorText;
            Transfers = new List<TransferVO>();
            if (!TransferProcedures.GetTransferInTickets(CDS, Transfers, out errorCode, out errorText))
            {
                if (errorText != "Success")
                {
                    Action<string> action = s => MessageBox.Show(s);
                    this.Invoke(action, new object[] { errorText });
                }
                return;
            }

            foreach (TransferVO transfer in Transfers.FindAll(nvt => nvt.Status != TransferStatus.VO).OrderByDescending(t => t.StatusDate))
            {
                Action actionAddRow = () =>
                {
                    DataGridViewRow row = gvTransfers.Rows.AddNew();
                    row.Cells[colShop.Index].Value = transfer.StoreNickName;
                    row.Cells[colTransferType.Index].Value = transfer.GetTransferTypeDescription();
                    row.Cells[colStatus.Index].Value = transfer.GetTransferInStatusDescription();
                    row.Cells[colTransferNumber.Index].Value = transfer.TransferTicketNumber;
                    row.Cells[colStatusDate.Index].Value = transfer.StatusDate.ToString("d");
                    row.Cells[colTotalNumberOfItems.Index].Value = transfer.NumberOfItems;
                    row.Cells[colTotalCost.Index].Value = transfer.Amount.ToString("c");
                    row.Tag = transfer;
                };
                this.Invoke(actionAddRow);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Calling reports1");
            ReportObject.TransferINReportStruct reportObj = new ReportObject.TransferINReportStruct();
            reportObj.FromShopName = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            reportObj.logPath =
            SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration
            .BaseLogPath;

            Reports.TransferIN.PendingTransReport transreport = new
            Reports.TransferIN.PendingTransReport(Transfers, reportObj);
            new Reports.TransferIN.PendingTransReport(Transfers, reportObj);
            transreport.CreateReport();
            //TODO: Store report in couch db

            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
            {
                string laserPrinterIp = GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress;
                int laserPrinterPort = GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port;
                PrintingUtilities.printDocument(transreport.getReportWithPath(), laserPrinterIp, laserPrinterPort, 1);
            }//end if (PrintEnabled)
        }

        private void gvTransfers_GridViewRowSelected(object sender, GridViewRowSelectedEventArgs e)
        {
            TransferVO transfer = e.Row.Tag as TransferVO;

            if (transfer == null)
            {
                return;
            }

            btnRejectTransfer.Enabled = transfer.TempStatus != TransferTempStatus.REJCT;
        }
    }
}
