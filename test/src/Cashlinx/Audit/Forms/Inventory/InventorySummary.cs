using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Libraries.Forms;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;
using Common.Libraries.Utility;

namespace Audit.Forms.Inventory
{
    public partial class InventorySummary : AuditWindowBase
    {
        private DataSet ReportSummaryDataSet { get; set; }
        private ProcessingMessage ProcessingMessage { get; set; }

        public InventorySummary()
        {
            InitializeComponent();
        }

        private void tableLayoutPanelMain_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 1 || e.Row == 3 || e.Row == 5)
            {
                int height = 2;
                Rectangle r = new Rectangle(e.CellBounds.X, e.CellBounds.Y - height - height, e.CellBounds.Width, height);
                e.Graphics.FillRectangle(Brushes.Black, r);
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dpLastLayawayDate.Text))
            {
                MessageBox.Show("You must enter the last layaway audit date");
                return;
            }

            ADS.ActiveAudit.LayAuditDate = dpLastLayawayDate.Value;
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void InventorySummary_Load(object sender, EventArgs e)
        {
            BackgroundWorker bwLoadInventorySummary = new BackgroundWorker();
            bwLoadInventorySummary.DoWork += new DoWorkEventHandler(bwLoadInventorySummary_DoWork);
            bwLoadInventorySummary.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwLoadInventorySummary_RunWorkerCompleted);
            bwLoadInventorySummary.RunWorkerAsync();

            ProcessingMessage = new ProcessingMessage("Loading summary...", 0);
            ProcessingMessage.ShowDialog(this);
        }

        private void bwLoadInventorySummary_DoWork(object sender, DoWorkEventArgs e)
        {
            CommonDatabaseContext dataContextSummary = CreateCommonDatabaseContext();
            InventoryAuditProcedures.GetSummary(ADS.ActiveAudit, dataContextSummary);

            if (!dataContextSummary.Result)
            {
                e.Result = dataContextSummary;
            }

            CommonDatabaseContext dataContextAuditInfo = CreateCommonDatabaseContext();
            InventoryAuditProcedures.GetAdditionalAuditInfo(ADS.ActiveAudit, dataContextAuditInfo);

            if (!dataContextAuditInfo.Result)
            {
                e.Result = dataContextAuditInfo;
            }

            dataContextSummary.OutputDataSet2 = dataContextAuditInfo.OutputDataSet1;
            e.Result = dataContextSummary;
        }

        private void bwLoadInventorySummary_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CommonDatabaseContext dataContext = e.Result as CommonDatabaseContext;

            if (dataContext != null && !dataContext.Result)
            {
                ProcessingMessage.Close();
                MessageBox.Show(dataContext.ErrorText);
                NavControlBox.Action = NavBox.NavAction.CANCEL;
                return;
            }

            ReportSummaryDataSet = dataContext.OutputDataSet1;

            PopulateReportSummary();
            PopulateAuditInfo();

            ProcessingMessage.Close();
        }

        private void PopulateAuditInfo()
        {
            InventoryAuditVO audit = ADS.ActiveAudit;
            lblAuditStartDate.Text = audit.AuditStartDate.ToString("MM/dd/yyyy hh:mm tt");
            lblMarketManager.Text = audit.MarketManager;
            lblShopManager.Text = audit.ActiveShopManager;
            lblAuditScope.Text = audit.AuditScope.ToString();
            lblAuditor.Text = audit.Auditor;
            lblAuditReason.Text = audit.AuditReason.ToString();
            lblMarketManagerPresent.Text = audit.MarketManagerPresent ? "Yes" : "No";
            lblLastInventoryAudit.Text = audit.LastAuditDate.ToString("MM/dd/yyyy");
            dpLastLayawayDate.Text = string.Empty;
            lblCurrentInventoryBalance.Text = audit.CurrentInventoryBalance.ToString("c");
            lblCurrentLoanBalance.Text = audit.CurrentLoanBalance.ToString("c");
            lblCurrentCash.Text = audit.CashInStore.ToString("c");
            lblPreviousInventoryBalance.Text = audit.PreviousInventoryBalance.ToString("c");
            lblPreviousLoanBalance.Text = audit.PreviousLoanBalance.ToString("c");
            lblYTDShortage.Text = audit.YTDShortage.ToString("c");
            lblOverShort.Text = audit.OverShort.ToString("c");
            lblPYChgOff.Text = audit.PreviousYearCoff.ToString("c");
            lblHalfPercentTotal.Text = audit.Tolerance.ToString("c");
            lblTotalChargeOn.Text = audit.TotalChargeOn.ToString("c");
            lblTotalChargeOff.Text = audit.TotalChargeOff.ToString("c");
            lblTotalTempIcns.Text = audit.TempICNAdjustment.ToString("c");
        }

        private void PopulateReportSummary()
        {
            if (ReportSummaryDataSet == null)
            {
                return;
            }

            lblActiveEmployees.Text = string.Empty;
            lblTransferTerminatedEmployees.Text = string.Empty;

            foreach (DataTable dt in ReportSummaryDataSet.Tables)
            {
                if (dt.TableName.Equals("output", StringComparison.InvariantCultureIgnoreCase))
                {
                    lblLastInventoryAudit.Text = Utilities.GetStringValue(dt.Rows[0][1]);
                }
                else if (dt.TableName.Equals("o_psnl", StringComparison.InvariantCultureIgnoreCase))
                {
                    PopulateEmployees(dt, lblActiveEmployees);
                }
                else if (dt.TableName.Equals("o_term", StringComparison.InvariantCultureIgnoreCase))
                {
                    PopulateEmployees(dt, lblTransferTerminatedEmployees);
                }
                else if (dt.TableName.Equals("o_chgOff", StringComparison.InvariantCultureIgnoreCase))
                {
                    PopulateCaccSummaries(dt);
                }
                else if (dt.TableName.Equals("o_Hist", StringComparison.InvariantCultureIgnoreCase))
                {
                }
            }
        }

        private void PopulateCaccSummaries(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

            foreach (DataRow dr in dt.Rows)
            {
                int catCode = Utilities.GetIntegerValue(dr["cat_code"]);
                decimal cost = Utilities.GetDecimalValue(dr["nf_amt"]);

                switch (catCode)
                {
                    case 1000:
                        lblJewelry.Text = cost.ToString("c");
                        break;
                    case 2000:
                        lblTools.Text = cost.ToString("c");
                        break;
                    case 3000:
                        lblElectric.Text = cost.ToString("c");
                        break;
                    case 4000:
                        lblFirearms.Text = cost.ToString("c");
                        break;
                    case 5000:
                        lblOpticals.Text = cost.ToString("c");
                        break;
                    case 6000:
                        lblMusic.Text = cost.ToString("c");
                        break;
                    case 7000:
                        lblSportingGoods.Text = cost.ToString("c");
                        break;
                    case 8000:
                        lblHomeEquip.Text = cost.ToString("c");
                        break;
                    case 9000:
                        lblMisc.Text = cost.ToString("c");
                        break;
                }
            }
        }

        private void PopulateEmployees(DataTable dt, Label lbl)
        {
            lbl.Text = string.Empty;

            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                sb.AppendFormat("{0}; ", dr["name"]);
            }

            lbl.Text = sb.ToString();
        }
    }
}
