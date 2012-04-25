using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Services.MerchandiseTransfer
{
    public partial class SelectTransferInItems : CustomBaseForm
    {
        # region Constructors
        public SelectTransferInItems()
        {
            InitializeComponent();
            NavControlBox = new NavBox
            {
                Owner = this
            };
            ReceivedItems = new List<TransferItemVO>();
        }

        # endregion

        # region Properties

        public NavBox NavControlBox { get; set; }

        private DesktopSession CDS
        {
            get
            {
                return GlobalDataAccessor.Instance.DesktopSession;
            }
        }

        private List<TransferItemVO> ReceivedItems { get; set; }

        # endregion

        # region Event Handlers

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //if (txtShopNumber.Text.Trim().Length != 5 || txtTransactionNumber.Text.Trim().Length != 6)
            //{
            //    MessageBox.Show("The ICN# you entered is not the required digit length.  Please try again.");
            //    return;
            //}
            SearchForItem(txtTransactionNumber.Text.Trim());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        /*private void btnPrint_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("report");
            List<TransferItemVO> transferList = new List<TransferItemVO>();
            ReportObject.TransferINReportStruct reportObj = new ReportObject.TransferINReportStruct();

            reportObj.transDate = ShopDateTime.Instance.ShopDate.ToString();
            reportObj.userID = CashlinxDesktopSession.Instance.FullUserName;
            reportObj.ToStoreName = GlobalDataAccessor.Instance.CurrentSiteId.StoreNickName;
            reportObj.ToStoreNo = "";
            reportObj.FromShopName = lblShopNumber.Text;
            reportObj.FromShopNo = "";
            reportObj.FromStoreAddrLine1 = "";
            reportObj.FromStoreAddrLine2 = "";

            reportObj.storeMgrPhone = "";
            reportObj.storeMgrName = "";
            reportObj.transNum = "";
            reportObj.Carrier = CDS.ActiveTransferIn.Carrier;
            reportObj.DateReceived = CDS.ActiveTransferIn.StatusDate.ToString();
            reportObj.TransferReference = CDS.ActiveTransferIn.TransferTicketNumber.ToString();
            reportObj.ReceivedBy = "";
            reportObj.logPath =
            SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration
            .BaseLogPath;

            PawnReports.Reports.TransferIN.SummaryReport transreport =
            new PawnReports.Reports.TransferIN.SummaryReport(ReceivedItems, reportObj);
            transreport.CreateReport();

            //TODO: Store report in couch db

            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                CashlinxDesktopSession.Instance.LaserPrinter.IsValid)
            {
                string laserPrinterIp = CashlinxDesktopSession.Instance.LaserPrinter.IPAddress;
                int laserPrinterPort = CashlinxDesktopSession.Instance.LaserPrinter.Port;
                PrintingUtilities.printDocument(transreport.getReportFileName(),
                                                laserPrinterIp,
                                                laserPrinterPort,
                                                1);
            }//end if (PrintEnabled)
        }*/

        private void btnRejectTransfer_Click(object sender, EventArgs e)
        {
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape || (this.ActiveControl.Equals(btnCancel) && keyData == Keys.Enter))
            {
                this.btnCancel_Click(null, new EventArgs());
                return true;
            }

            if (keyData == Keys.Enter && (this.ActiveControl.Equals(btnAdd) || this.ActiveControl.Equals(txtShopNumber) || this.ActiveControl.Equals(txtTransactionNumber)))
            {
                this.btnAdd_Click(null, new EventArgs());
                return true;
            }
            else if (keyData == Keys.Enter && this.ActiveControl.Equals(txtComments))
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
            /*else if (keyData == Keys.Enter && this.ActiveControl.Equals(btnPrint))
            {
                this.btnPrint_Click(null, new EventArgs());
                return true;
            }*/
            else if (keyData == Keys.Enter && this.ActiveControl.Equals(btnRejectTransfer))
            {
                this.btnRejectTransfer_Click(null, new EventArgs());
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                this.btnComplete_Click(null, new EventArgs());
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SelectTransferInItems_Shown(object sender, EventArgs e)
        {
            btnComplete.Enabled = false;
            SetupUIForTransferMethod();
            if (CDS.TransferMethod == TransferMethod.QuickReceive)
            {
                PopulateGridRows();
            }
            PopulateTransferDetails();
        }

        private void txtTransactionNumber_TextChanged(object sender, EventArgs e)
        {
            if (CheckForScannedEntry(txtTransactionNumber.Text))
            {
                ClearIcnEntry();
            }
        }

        # endregion

        # region Helper Methods

        private void AddItem(TransferItemVO item)
        {
            if (ReceivedItems.Contains(item))
            {
                MessageBox.Show("This item has already been scanned for transfer.");
                return;
            }

            ReceivedItems.Add(item);

            var row = gvTransfers.Rows.AddNew();
            row.Cells[colNumber.Index].Value = gvTransfers.Rows.Count;
            row.Cells[colIcn.Index].Value = item.ICN;
            row.Cells[colRefurbNumber.Index].Value = item.RefurbNumber.ToString();
            row.Cells[colDescription.Index].Value = item.MdseRecordDesc;
            row.Cells[colCost.Index].Value = item.PfiAmount.ToString("c");
            row.Cells[colQuantity.Index].Value = item.ICNQty;

            gvTransfers.AutoResizeColumns();
            //btnComplete.Enabled = gvTransfers.Rows.Count == CDS.ActiveTransferIn.NumberOfItems;
            btnComplete.Enabled = ReceivedItems.Count == CDS.ActiveTransferIn.Items.Count;

            PopulateTransferDetails();
        }

        private bool CheckForScannedEntry(string textValue)
        {
            if (textValue.Trim().Length == Icn.ICN_LENGTH)
            {
                Icn icn = new Icn(textValue.Trim());

                var item = CDS.ActiveTransferIn.Items.Find(i => i.ICN == icn.GetFullIcn());

                if (item == null)
                {
                    MessageBox.Show("The ICN# entered is not in the list of received transfers.");
                    return false;
                }

                AddItem(item);
                return true;
            }
            return false;
        }

        private void ClearIcnEntry()
        {
            txtTransactionNumber.Text = string.Empty;
        }

        private void PopulateGridRows()
        {
            gvTransfers.Rows.Clear();
            if (CDS.ActiveTransferIn.TransferType == TransferTypes.JORET)
            {
                colRefurbNumber.Visible = true;
                lblEnterOrScanIcn.Text = "Enter or Scan Refurb #";
            }
            else
            {
                colRefurbNumber.Visible = false;
                lblEnterOrScanIcn.Text = "Enter or Scan ICN #";
            }

            foreach (TransferItemVO item in CDS.ActiveTransferIn.Items)
            {
                AddItem(item);
            }
        }

        private void PopulateTransferDetails()
        {
            lblTransferNumber.Text = string.Format("Transfer # {0}", CDS.ActiveTransferIn.TransferTicketNumber);
            lblShopNumber.Text = string.Format("Shop # - {0}", CDS.ActiveTransferIn.StoreInfo.StoreNickName);
            lblAddress.Text = CDS.ActiveTransferIn.StoreInfo.StoreAddress1;
            lblCityStateZip.Text = string.Format("{0}, {1} {2}", CDS.ActiveTransferIn.StoreInfo.StoreCityName, CDS.ActiveTransferIn.StoreInfo.State, CDS.ActiveTransferIn.StoreInfo.StoreZipCode );
            txtShopNumber.Text = CDS.ActiveTransferIn.DestinationStoreNumber;

            lblCarrier.Text = CDS.ActiveTransferIn.Carrier;
            lblTransferType.Text = CDS.ActiveTransferIn.GetTransferTypeDescription();

            lblTotalItemsTransfered.Text = CDS.ActiveTransferIn.NumberOfItems.ToString();
            lblTotalCostTransfered.Text = CDS.ActiveTransferIn.Amount.ToString("c");

            int totalItemsReceived = ReceivedItems.Count;
            decimal totalCostReceived = (from ri in ReceivedItems
                                         select ri.PfiAmount).Sum();

            lblTotalItemsReceived.Text = totalItemsReceived.ToString();
            lblTotalCostReceived.Text = totalCostReceived.ToString("c");

            if (CDS.ActiveTransferIn.Status == TransferStatus.TO)
            {
                lblStatus.Text = CDS.ActiveTransferIn.GetTransferInStatusDescription();
                if (CDS.ActiveTransferIn.TempStatus == TransferTempStatus.REJCT)
                {
                    txtComments.Enabled = false;
                    txtComments.Visible = lblComments.Visible = true;
                    txtComments.Text = CDS.ActiveTransferIn.RejectReason;
                }
                else
                {
                    txtComments.Visible = lblComments.Visible = false;
                }
            }
            else
            {
                btnComplete.Enabled = false;
            }
        }

        private void SearchForItem(string shortCode)
        {
            Icn icn = new Icn(shortCode);
            var items = CDS.ActiveTransferIn.Items.FindAll(i => new Icn(i.ICN).GetShortCode() == icn.GetShortCode());

            if (items == null || items.Count == 0)
            {
                MessageBox.Show("The ICN# entered is not in the list of received transfers.");
                return;
            }
            else if (items.Count > 1)
            {
                MessageBox.Show("The short code entered is a duplicate.  Please make your selection from the list.");
                return;
            }
            else
            {
                AddItem(items[0]);
            }
        }

        private void SetupUIForTransferMethod()
        {
            flowLayoutPanelIcnEntry.Visible = CDS.TransferMethod == TransferMethod.Manual;
        }
        # endregion
    }
}
