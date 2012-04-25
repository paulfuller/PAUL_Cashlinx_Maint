using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Components.EventArgs;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Report;
using Pawn.Logic;

namespace Pawn.Forms.Layaway
{
    public partial class LayawayForfeitureResults : CustomBaseForm
    {
        # region Constructors

        public LayawayForfeitureResults(DateTime eligibilityDate, List<LayawayVO> eligibleLayaways)
        {
            InitializeComponent();

            EligibilityDate = eligibilityDate;
            EligibleLayaways = eligibleLayaways;
            LayawayLastPaymentMap = new Dictionary<LayawayVO, DateTime>();
            WaitDaysForLayawayForfeitureEligibility = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetWaitDaysForLayawayForfeitureEligibility(CDS.CurrentSiteId);
        }

        # endregion

        # region Properties

        public DesktopSession CDS
        {
            get { return GlobalDataAccessor.Instance.DesktopSession; }
        }

        public DateTime EligibilityDate { get; private set; }
        public List<LayawayVO> EligibleLayaways { get; private set; }

        private Dictionary<LayawayVO, DateTime> LayawayLastPaymentMap { get; set; }
        private int WaitDaysForLayawayForfeitureEligibility { get; set; }

        # endregion

        # region Public Methods

        public List<LayawayVO> GetSelectedLayaways()
        {
            return (from DataGridViewRow row in gvEligibleLayaways.SelectedRows
                    where row.Tag is LayawayVO
                    select row.Tag as LayawayVO).OrderBy(l => l.TicketNumber).ToList();
        }

        # endregion

        # region Events

        public event EventHandler CancelRequested;
        public event EventHandler ForfeitCompleted;

        # endregion
        
        # region Event Handlers

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CancelRequested != null)
            {
                CancelRequested(this, EventArgs.Empty);
            }
            else
            {
                this.Close();
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            RaiseForfeitCompleted();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            List<LayawayVO> layaways = GetSelectedLayaways();
            if (layaways.Count == 0)
            {
                return;
            }

            LayawayDetailViewer detailViewer = new LayawayDetailViewer(layaways);
            detailViewer.ForfeitCompleted += new EventHandler(detailViewer_ForfeitCompleted);
            detailViewer.ShowDialog();
        }

        void detailViewer_ForfeitCompleted(object sender, EventArgs e)
        {
            RaiseForfeitCompleted();
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            SelectDeselectAll(false);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            List<LayawayVO> layaways = GetSelectedLayaways();
            if (layaways.Count == 0)
            {
                
                return;
            }
            LayawayCreateReportObject lco = new LayawayCreateReportObject();
            var restockingFee = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetLayawayRestockingFee(GlobalDataAccessor.Instance.CurrentSiteId);
            //lco.GetLayawayTerminatedListings(layaways, restockingFee);
            lco.GetLayawayForfeitPickingSlip(layaways);
            MessageBox.Show("Printing Complete.");
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            SelectDeselectAll(true);
        }

        private void gvEligibleLayaways_SelectionChanged(object sender, EventArgs e)
        {
            lblTotalSelectedValue.Text = gvEligibleLayaways.SelectedRows.Count.ToString();
            //btnDetail.Enabled = gvEligibleLayaways.SelectedRows.Count > 0;
           // btnPrint.Enabled = gvEligibleLayaways.SelectedRows◘.Count > 0;
        }

        private bool IsLayawayEligibleToForfeit(DateTime lastPaymentDate)
        {
            return LayawayProcedures.IsLayawayEligibleForForfeiture(lastPaymentDate, CDS.CurrentSiteId);
        }

        private void LayawayForfeitureSearch_Shown(object sender, EventArgs e)
        {
            PopulateHeaderText();
            gvEligibleLayaways_SelectionChanged(this, EventArgs.Empty);
            lblTotalEligibleValue.Text = EligibleLayaways.Count.ToString();
            PopulateLayawayRows();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.btnBack_Click(null, new EventArgs());
                return true;
            }

            if (this.ActiveControl.Equals(btnCancel) && keyData == Keys.Enter)
            {
                this.btnCancel_Click(null, EventArgs.Empty);
                return true;
            }

            if (keyData == Keys.Enter)
            {
                this.btnComplete_Click(null, new EventArgs());
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        # endregion

        # region Helper Methods

        private void PopulateHeaderText()
        {
            lblTransactionDate.Text = string.Format("Select Eligible Record to Forfeit As Of {0}", EligibilityDate.ToString("MM/dd/yyyy"));
        }

        private void PopulateLayawayRows()
        {
            gvEligibleLayaways.Rows.Clear();

            gvEligibleLayaways.Columns[colDateMade.Index].DefaultCellStyle.Format = "d";
            gvEligibleLayaways.Columns[colAmount.Index].DefaultCellStyle.Format = "c";
            gvEligibleLayaways.Columns[colLastPaymentAmount.Index].DefaultCellStyle.Format = "c";
            gvEligibleLayaways.Columns[colLastPaymentDate.Index].DefaultCellStyle.Format = "d";
            gvEligibleLayaways.Columns[colPaidIn.Index].DefaultCellStyle.Format = "c";

            foreach (LayawayVO layaway in EligibleLayaways)
            {
                DataGridViewRow row = gvEligibleLayaways.Rows.AddNew();
                Receipt lastReceipt = layaway.GetLastPaymentReceipt();

                row.Cells[colTicket.Index].Value = layaway.TicketNumber;
                row.Cells[colCustomer.Index].Value = layaway.EntityName;
                row.Cells[colDateMade.Index].Value = layaway.DateMade;
                row.Cells[colAmount.Index].Value = layaway.Amount;
                row.Cells[colLastPaymentAmount.Index].Value = lastReceipt.Amount;
                row.Cells[colLastPaymentDate.Index].Value = lastReceipt.Date;
                row.Cells[colPaidIn.Index].Value = layaway.GetAmountPaid();
                row.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
                try
                {
                    if (!IsLayawayEligibleToForfeit(layaway.NextPayment))
                    {
                        row.DefaultCellStyle.BackColor = Color.LemonChiffon;
                        row.DefaultCellStyle.SelectionBackColor = Color.SandyBrown;
                    }
                }
                catch (ApplicationException)
                {
                    gvEligibleLayaways.Rows.Remove(row);
                }

                row.Tag = layaway;
            }

            if (LayawayLastPaymentMap.Values.Any(d => !IsLayawayEligibleToForfeit(d)))
            {
                lblWarningMessage.Text = "You have future date record(s) which cannot be selected and therefore cannot be printed or forfeited.";
            }
            else
            {
                lblWarningMessage.Text = string.Empty;
            }
        }

        private DateTime GetLastPaymentDate(LayawayVO layaway)
        {
            return layaway.NextPayment;
            //if (!LayawayLastPaymentMap.ContainsKey(layaway))
            //{
            //    LayawayPaymentHistoryBuilder builder = new LayawayPaymentHistoryBuilder(layaway);
            //    LayawayHistory payment = builder.GetFirstUnpaidPayment();

            //    if (payment == null)
            //    {
            //        throw new ApplicationException();
            //    }

            //    LayawayLastPaymentMap.Add(layaway, payment.PaymentDueDate);
            //}

            //return LayawayLastPaymentMap[layaway];
        }

        private void RaiseForfeitCompleted()
        {
            if (ForfeitCompleted != null)
            {
                ForfeitCompleted(this, EventArgs.Empty);
            }
        }

        private void SelectDeselectAll(bool selected)
        {
            foreach (DataGridViewRow row in gvEligibleLayaways.Rows)
            {
                row.Selected = selected;
            }
        }

        # endregion

        private void gvEligibleLayaways_GridViewRowSelecting(object sender, GridViewRowSelectingEventArgs e)
        {
            if (e.Row == null)
            {
                e.Cancel = true;
                return;
            }

            LayawayVO layaway = e.Row.Tag as LayawayVO;
            if (layaway == null)
            {
                e.Cancel = true;
                return;
            }
            //if (!IsLayawayEligibleToForfeit(layaway.NextPayment))
            //{
                //e.Cancel = true;
            //}
        }
    }
}
