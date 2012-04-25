using Common.Controllers.Security;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System;
using System.Data;
using System.Windows.Forms;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;
using Reports.Inquiry;

namespace Pawn.Forms.Inquiry.CashTransferInquiry
{
    public class CashTransferSearchResults : SearchResults
    {
        private System.Windows.Forms.DataGridViewTextBoxColumn TransferNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransferType;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransferDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransferAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Source;
        private System.Windows.Forms.DataGridViewTextBoxColumn Destination;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserId;

        public CashTransferSearchResults(DataSet s, Inquiry criteria, string dataTableName)
            : base(s, criteria, dataTableName, "Items")
        {
            #region Data Grid Initialization
            windowHeading_lb.Text = "Cash Transfer Inquiry Search Results";
            searchCriteria_lb.Text = "Cash Transfer Search Criteria";

            criteriaDetails_panel.Width += 200;
            this.Width += 200;
            this.resultsGrid_dg.Width += 200;

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();

            this.TransferNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransferType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransferDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransferAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Destination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserId = new System.Windows.Forms.DataGridViewTextBoxColumn();

            resultsGrid_dg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //resultsGrid_dg.ShowCellToolTips = true;

            this.resultsGrid_dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.TransferNumber,
                this.TransferType,
                this.TransferDate,
                this.TransferAmount,
                this.Source,
                this.Destination,
                this.Status,
                this.UserId});

            // 
            // TransferNumber
            // 
            this.TransferNumber.DataPropertyName = "TRANSFERNUMBER";
            this.TransferNumber.HeaderText = "Transfer #";
            this.TransferNumber.Name = "TRANSFERNUMBER";
            this.TransferNumber.ReadOnly = true;
            this.TransferNumber.Width = 75;

            // 
            // TransferType
            // 
            this.TransferType.DataPropertyName = "TRANSFERTYPE";
            this.TransferType.HeaderText = "Transfer Type";
            this.TransferType.Name = "TRANSFERTYPE";
            this.TransferNumber.ReadOnly = true;
            this.TransferNumber.Width = 100;
            //this.TransferType.Visible = false;

            // 
            // Status Date
            // 
            this.TransferDate.DataPropertyName = "TRANSFERDATE";
            dataGridViewCellStyle3.Format = "g";
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.TransferDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.TransferDate.HeaderText = "Transfer Date";
            this.TransferDate.Name = "TRANSFERDATE";
            this.TransferDate.ReadOnly = true;
            this.TransferDate.Width = 125;

            // 
            // TransferAmount
            // 
            this.TransferAmount.DataPropertyName = "TRANSFERAMOUNT";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.TransferAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.TransferAmount.HeaderText = "Transfer Amount";
            this.TransferAmount.Name = "TRANSFERAMOUNT";
            this.TransferAmount.ReadOnly = true;
            this.TransferAmount.Width = 125;

            // 
            // Source
            // 
            this.Source.DataPropertyName = "source";
            this.Source.HeaderText = "Source";
            this.Source.Name = "source";
            this.Source.ReadOnly = true;
            this.Source.Width = 150;

            // 
            // Destination
            // 
            this.Destination.DataPropertyName = "destination";
            this.Destination.HeaderText = "Destination";
            this.Destination.Name = "destination";
            this.Destination.ReadOnly = true;
            this.Destination.Width = 150;

            // 
            // Status
            // 
            this.Status.DataPropertyName = "TRANSFERSTATUS";
            this.Status.HeaderText = "Status";
            this.Status.Name = "TRANSFERSTATUS";
            this.Status.ReadOnly = true;
            this.Status.Width = 100;

            // 
            // UserId
            // 
            this.UserId.DataPropertyName = "userid";
            this.UserId.HeaderText = "User Id";
            this.UserId.Name = "userid";
            this.UserId.ReadOnly = true;
            this.UserId.Width = 75;
            

            this.resultsGrid_dg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.resultsGrid_dg_CellContentClick);
            this.resultsGrid_dg.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(resultsGrid_dg_CellContentMouseOver);


            #endregion


            this.Print_btn.Enabled = true;
            this.Print_btn.Click += new System.EventHandler(this.Print_btn_Click);

        }

        protected void Print_btn_Click(object sender, EventArgs e)
        {
            string rptDir =
                SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.
                BaseLogPath;

            var report = new CashTransferListingReport(PdfLauncher.Instance);
            var ro = new ReportObject();
            ro.CreateTemporaryFullName();
            ro.ReportTempFileFullName = rptDir + ro.ReportTempFileFullName;
            ro.ReportTitle = "Cash Transfer Listing";
            ro.ReportStore = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            ro.ReportStoreDesc = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
            ro.ReportSort = "USERID";
            report.reportObject = ro;

            var dv = _theData.DefaultViewManager.CreateDataView(_theData.Tables[0]);
            var isSuccessful = report.CreateReport(_theData.Tables[0]);

            DesktopSession.ShowPDFFile(report.reportObject.ReportTempFileFullName, false);
            this.TopMost = false;
        }

        private void resultsGrid_dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                this.Visible = false;

                Form details = null;
                if (_theData.Tables.Contains("SHOP_TO_SHOP_TRANSFERS"))
                {
                    var xferNumber = resultsGrid_dg.Rows[e.RowIndex].Cells["TRANSFERNUMBER"].Value.ToString();
                    var ds = global::Pawn.Forms.Inquiry.CashTransferInquiry.CashTransferInquiry.getStoreToStoreCashXferDetails(xferNumber);
                    details = new CashTransferDetails(xferNumber, ds, e.RowIndex, CashTransferDetails.TransferTypeEnum.SHOP);
                }
                else if (_theData.Tables.Contains("STORE_TRANSFERS"))
                {
                    // SAFE is a special case of INTERNAL
                    var xferNumber = resultsGrid_dg.Rows[e.RowIndex].Cells["TRANSFERNUMBER"].Value.ToString();
                    var ds = global::Pawn.Forms.Inquiry.CashTransferInquiry.CashTransferInquiry.getInternalCashXferDetails(xferNumber);
                    details = new CashTransferDetails(xferNumber, ds, e.RowIndex, CashTransferDetails.TransferTypeEnum.INTERNAL);
                }
                else
                {
                    var xferNumber = resultsGrid_dg.Rows[e.RowIndex].Cells["TRANSFERNUMBER"].Value.ToString();
                    var ds = global::Pawn.Forms.Inquiry.CashTransferInquiry.CashTransferInquiry.getBankCashXferDetails(xferNumber);
                    details = new CashTransferDetails(xferNumber, ds, e.RowIndex, CashTransferDetails.TransferTypeEnum.BANK);
                }

                if (details != null)
                {

                    details.ShowDialog();

                    switch (details.DialogResult)
                    {
                        case DialogResult.OK:
                            this.Visible = true;
                            break;

                        default:
                            this.DialogResult = details.DialogResult;
                            this.Close();
                            break;
                    }
                }
            }
        }

        private void resultsGrid_dg_CellContentMouseOver(object sender, DataGridViewCellToolTipTextNeededEventArgs args)
        {
            //if (args.RowIndex >= 0)
            //{
            //    try
            //    {
            //        if (args.ToolTipText == "")
            //        {
            //            //args.ToolTipText =
            //            //        ((System.Windows.Forms.DataGridView)(sender)).Rows[args.RowIndex].Cells["ICN"].Value.ToString();

            //            //((DataGridView)(sender)).CurrentCell.ToolTipText = args.ToolTipText;

            //            switch (args.ColumnIndex)
            //            {
            //                case 1:
            //                    args.ToolTipText =
            //                            ((System.Windows.Forms.DataGridView)(sender)).Rows[args.RowIndex].Cells["TRANSFERNUMBER"].Value.ToString();

            //                    ((DataGridView)(sender)).CurrentCell.ToolTipText = args.ToolTipText;
            //                    break;

            //                case 4:
            //                case 10:
            //                    args.ToolTipText = ((System.Windows.Forms.DataGridView)(sender)).Rows[args.RowIndex].Cells[args.ColumnIndex].Value.ToString();

            //                    ((DataGridView)(sender)).CurrentCell.ToolTipText = args.ToolTipText;
            //                    break;
            //            }

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Exception occurred accessing Transfer Number Column");
            //    }
            //}
        }

    }
}
