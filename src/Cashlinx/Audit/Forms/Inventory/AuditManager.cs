using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Audit.Reports;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Libraries.Objects.Audit;
using Common.Libraries.Utility;

namespace Audit.Forms.Inventory
{
    public partial class AuditManager : AuditWindowBase
    {
        private const string DATE_FORMAT_STRING = "MM/dd/yyyy hh:mm tt";
        private const string ACTUAL_CACC = "Actual CACC";
        private Boolean _CurrentlyInUpdateStatus = false;
        DataSet dsAuditStatusSummary;

        public AuditManager()
        {
            InitializeComponent();
        }

        private void tableLayoutPanelContainer_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 0)
            {
                int height = 2;
                Rectangle r = new Rectangle(e.CellBounds.X, e.CellBounds.Y + e.CellBounds.Height - height, e.CellBounds.Width, height);
                e.Graphics.FillRectangle(Brushes.Black, r);
            }
            else if (e.Row == 2 && e.Column == 0)
            {
                int height = 2;
                Rectangle r = new Rectangle(e.CellBounds.X, e.CellBounds.Y + e.CellBounds.Height - height, e.CellBounds.Width, height);
                e.Graphics.FillRectangle(Brushes.Black, r);
            }

            if (e.Row != 0 && e.Column == 0)
            {
                int width = 2;
                Rectangle r = new Rectangle(e.CellBounds.X + e.CellBounds.Width - width, e.CellBounds.Y, width, e.CellBounds.Width);
                e.Graphics.FillRectangle(Brushes.Black, r);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnPostAudit_Click(object sender, EventArgs e)
        {
            ADS.ReportActiveAudit = ADS.ActiveAudit;
            if (Utilities.GetIntegerValue(lblItemsMissingQty.Text) > 0)
            {
                MessageBox.Show("All missing items must be handled prior to posting audit.");
                return;
            }

            bool actualCaccCompleted = true;
            bool actualCaccHasZeros = false;

            if (dsAuditStatusSummary.Tables.Contains(AuditReportsProcedures.PREAUDIT_CACC_SMRY) && dsAuditStatusSummary.Tables[AuditReportsProcedures.PREAUDIT_CACC_SMRY].Rows.Count > 0)
            {
                foreach (DataRow dr in dsAuditStatusSummary.Tables[AuditReportsProcedures.PREAUDIT_CACC_SMRY].Rows)
                {
                    int? newQty = Utilities.GetNullableIntegerValue(dr["new_qty"], null);
                    if (!newQty.HasValue)
                    {
                        actualCaccCompleted = false;
                        continue;
                    }

                    if (newQty.Value.Equals(0))
                    {
                        actualCaccHasZeros = true;
                    }
                }
            }
            else
            {
                actualCaccCompleted = false;
            }

            if (!actualCaccCompleted)
            {
                MessageBox.Show("Each CACC item is required to have a count.");
                return;
            }

            if (actualCaccHasZeros)
            {
                if (MessageBox.Show("Some CACC values are set to 0.\r\n\r\nAre you sure you want to continue?", "Audit Manager", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

            }

            NavControlBox.IsCustom = false;
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        private void btnDownloadToTrakker_Click(object sender, EventArgs e)
        {
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "DOWNLOAD";
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        private void btnUploadFromTrakker_Click(object sender, EventArgs e)
        {

            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "UPLOAD";
            NavControlBox.Action = NavBox.NavAction.SUBMIT;

            isActivated = false;
        }

        private void btnProcessMissing_Click(object sender, EventArgs e)
        {
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "PROCESSMISSING";
            NavControlBox.Action = NavBox.NavAction.SUBMIT;

            isActivated = false;
        }

        private void btnProcessUnexpected_Click(object sender, EventArgs e)
        {
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "PROCESSUNEXPECTED";
            NavControlBox.Action = NavBox.NavAction.SUBMIT;

            isActivated = false;
        }

        private void btnCountCacc_Click(object sender, EventArgs e)
        {
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "COUNTCACC";
            NavControlBox.Action = NavBox.NavAction.SUBMIT;

            isActivated = false;
        }

        private void updateStats(InventoryAuditVO audit, bool isNewAudit )
        {
            if (_CurrentlyInUpdateStatus)
            {
                // Prevent update stats from being called twice. 
                return;
            }

            try
            {
                if ( AuditReportsProcedures.getAuditStatusSummary(audit.StoreNumber, audit.AuditId, isNewAudit, out dsAuditStatusSummary))
                {

                    if (dsAuditStatusSummary.Tables.Contains("OUTPUT") && dsAuditStatusSummary.Tables["OUTPUT"].Rows.Count == 3)
                    {
                        lblItemsMissingQty.Text = dsAuditStatusSummary.Tables["OUTPUT"].Rows[0][1].ToString();
                        lblCaccItemsMissingQty.Text = dsAuditStatusSummary.Tables["OUTPUT"].Rows[2][1].ToString();
                        ItemsUnexpectedQty.Text = dsAuditStatusSummary.Tables["OUTPUT"].Rows[1][1].ToString();
                    }


                    // perhaps label20 || 
                    bool isCACCActual = InventoryAuditProcedures.setCaccTotals(audit, dsAuditStatusSummary.Tables[AuditReportsProcedures.PREAUDIT_CACC_SMRY].Rows, label20.Text != ACTUAL_CACC && isNewAudit);

                    if (isCACCActual)
                    {
                        label20.Text = ACTUAL_CACC;
                    }


                    lblCompactDiscsCost.Text = audit.CompactDiscSummary.Cost.ToString("c");
                    lblCompactDiscsQty.Text = audit.CompactDiscSummary.Quantity.ToString();

                    lblVideoTapesCost.Text = audit.VideoTapeSummary.Cost.ToString("c");
                    lblVideoTapesQty.Text = audit.VideoTapeSummary.Quantity.ToString();

                    lblStandardVideoGamesCost.Text = audit.StandardVideoGameSummary.Cost.ToString("c");
                    lblStandardVideoGamesQty.Text = audit.StandardVideoGameSummary.Quantity.ToString();

                    lblPremiumVideoGamesCost.Text = audit.PremiumVideoGameSummary.Cost.ToString("c");
                    lblPremiumVideoGamesQty.Text = audit.PremiumVideoGameSummary.Quantity.ToString();

                    lblDvdDiscsCost.Text = audit.DvdDiscSummary.Cost.ToString("c");
                    lblDvdDiscsQty.Text = audit.DvdDiscSummary.Quantity.ToString();

                    lblExpectedCaccCost.Text = audit.GetTotalCaccCost().ToString("c");
                    lblExpectedCaccQty.Text = audit.GetTotalCaccQty().ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, "ERROR " + ex.Message);
            }            
        }

        private void AuditManager_Load(object sender, EventArgs e)
        {
            _CurrentlyInUpdateStatus = true;
            //this.DialogResult = DialogResult.Cancel;
            InventoryAuditVO audit = ADS.ActiveAudit;
            //// update to actual details, if audit is in progress
            updateStats(audit, (audit.UploadDate == DateTime.MinValue));
            _CurrentlyInUpdateStatus = false;

            lblAuditCompleteDate.Text = string.Empty;
            lblAuditor.Text = audit.InitiatedBy;
            lblAuditScope.Text = audit.AuditScope.ToString();
            lblAuditStartDate.Text = audit.DateInitiated.ToString(DATE_FORMAT_STRING);

            lblExpectedItemsCost.Text = audit.ExpectedItems.Cost.ToString("c");
            lblExpectedItemsQty.Text = audit.ExpectedItems.Quantity.ToString();

            lblExpectedGeneralMerchandiseCost.Text = audit.ExptedtedGeneral.Cost.ToString("c");
            lblExpectedGeneralMerchandiseQty.Text = audit.ExptedtedGeneral.Quantity.ToString();

            lblExpectedJewelryCost.Text = audit.ExptectedJewlery.Cost.ToString("c");
            lblExpectedJewelryQty.Text = audit.ExptectedJewlery.Quantity.ToString();

            lblExpectedCaccCost.Text = audit.GetTotalCaccCost().ToString("c");
            lblExpectedCaccQty.Text = audit.GetTotalCaccQty().ToString();
        }

        private void AuditManager_Shown(object sender, EventArgs e)
        {
            
        }

        private bool isActivated = false;

        private void AuditManager_Activated(object sender, EventArgs e)
        {
            if (isActivated)
                return;
            btnProcessMissing.Enabled = ADS.ActiveAudit.IsDownloadAndUploadComplete();
            btnProcessUnexpected.Enabled = ADS.ActiveAudit.IsDownloadAndUploadComplete();
            btnCountCacc.Enabled = ADS.ActiveAudit.IsDownloadAndUploadComplete();
            btnPostAudit.Enabled = ADS.ActiveAudit.IsDownloadAndUploadComplete();
            btnTrakkerUploadReport.Enabled = ADS.ActiveAudit.IsDownloadAndUploadComplete();

            if (ADS.ActiveAudit.IsDownloadComplete())
            {
                lblDownloadToTrakker.Text = ADS.ActiveAudit.DownloadDate.ToString(DATE_FORMAT_STRING);
            }
            else
            {
                lblDownloadToTrakker.Text = string.Empty;
            }

            if (ADS.ActiveAudit.IsUploadComplete())
            {
                lblUploadFromTrakker.Text = ADS.ActiveAudit.UploadDate.ToString(DATE_FORMAT_STRING);
            }
            else
            {
                lblUploadFromTrakker.Text = string.Empty;
            }

            updateStats(ADS.ActiveAudit, ADS.ActiveAudit.UploadDate == DateTime.MinValue);

            isActivated = true;
        }

        private void btnTrakkerUploadReport_Click(object sender, EventArgs e)
        {
            Reports.CallAuditReport car = new Reports.CallAuditReport();
            car.GetTrakkerUploadReport(false, true);
        }

        private void btnPreAuditReport_Click(object sender, EventArgs e)
        {

             Reports.CallAuditReport callReport = new CallAuditReport();
             callReport.GetPreAuditReport(false, true);
        }
    }
}
