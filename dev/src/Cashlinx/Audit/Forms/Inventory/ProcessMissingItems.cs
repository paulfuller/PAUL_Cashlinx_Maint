using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Audit.Logic;
using Audit.Reports;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Controllers.Security;
using Common.Libraries.Forms;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;
using Common.Libraries.Objects.Business;

namespace Audit.Forms.Inventory
{
    public partial class ProcessMissingItems : AuditWindowBase
    {
        private ChargeOffContext ChargeOffContext { get; set; }

        private List<TrakkerItem> TrakkerItems { get; set; }

        private ProcessingMessage ProcessingMessage { get; set; }

        public ProcessMissingItems()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        private void btnPreviousAuditTrakker_Click(object sender, EventArgs e)
        {
            if (gvMissingItems.SelectedRows.Count != 1)
            {
                return;
            }

            TrakkerItem item = gvMissingItems.SelectedRows[0].Tag as TrakkerItem;
            if (item == null)
            {
                return;
            }

            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            InventoryAuditProcedures.GetPreviousLocation(ADS.ActiveAudit, item, dataContext);

            if (!dataContext.Result)
            {
                MessageBox.Show("No previous location");
                return;
            }

            PreviousAuditTrakker previousAuditTrakker = new PreviousAuditTrakker(item);
            previousAuditTrakker.ShowDialog();
        }

        private void btnFound_Click(object sender, EventArgs e)
        {
            ExecuteUserOption(ProcessMissingUserOptions.FOUND);
        }

        private void btnChargeOff_Click(object sender, EventArgs e)
        {
            if (gvMissingItems.SelectedRows.Count == 0)
            {
                return;
            }

            ChargeOffContext = new ChargeOffContext(ADS);

            List<string> icns = new List<string>();
            if (gvMissingItems.SelectedRows.Count > 1)
            {
                bool hasGun = false;
                bool hasNonGun = false;
                foreach (DataGridViewRow row in gvMissingItems.SelectedRows)
                {
                    TrakkerItem item = row.Tag as TrakkerItem;
                    if (item.IsGun)
                    {
                        hasGun = true;
                    }
                    else
                    {
                        hasNonGun = true;
                    }
                    icns.Add(item.Icn.ToString());
                }

                if (hasGun && hasNonGun)
                {
                    MessageBox.Show("Cannot charge off a firearm and a non-firearm at the same time.");
                    return;
                }

                ChargeOffContext.IsGun = hasGun;
                ChargeOffContext.MultipleItems = true;
            }
            else
            {
                ChargeOffContext.MultipleItems = false;
                TrakkerItem item = gvMissingItems.SelectedRows[0].Tag as TrakkerItem;
                ChargeOffContext.ChargeOffAmount = item.PfiAmount;
                ChargeOffContext.Description = item.Description;
                ChargeOffContext.Icn = item.Icn;
                ChargeOffContext.IsGun = item.IsGun;
                icns.Add(item.Icn.ToString());
            }

            ChargeOff chargeOff = new ChargeOff(ChargeOffContext);
            if (chargeOff.ShowDialog() == DialogResult.OK)
            {
                ChargeOffContext.ChargeOffDatabaseContext.AuditId = ADS.ActiveAudit.AuditId;
                ChargeOffContext.ChargeOffDatabaseContext.StoreNumber = ADS.ActiveAudit.StoreNumber;
                ChargeOffContext.ChargeOffDatabaseContext.Icns = icns;

                CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
                InventoryAuditProcedures.ChargeOff(ChargeOffContext.ChargeOffDatabaseContext, dataContext);

                if (dataContext != null && !dataContext.Result)
                {
                    MessageBox.Show(dataContext.ErrorText);
                    return;
                }

                LoadMissingItems();
            }
        }

        private void btnReconcile_Click(object sender, EventArgs e)
        {
            ExecuteUserOption(ProcessMissingUserOptions.RECONCILE);
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            ExecuteUserOption(ProcessMissingUserOptions.UNDO);
        }

        private void btnPrintShortList_Click(object sender, EventArgs e)
        {
            var reportObject = new ReportObject(SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath);
            DataSet outputDataSet;
            //reportObject.ReportTempFile = "c:\\Program Files\\Audit\\logs\\";
            reportObject.CreateTemporaryFullName("MissingItemsReport_");
            string _storeName = ADS.ActiveAudit.StoreName;

            reportObject.ReportStore = ADS.ActiveAudit.StoreNumber;
            //reportObject.ReportParms.Add(DateTime.Now);

            bool hasData = AuditReportsProcedures.GetMissingItemsData(ADS.ActiveAudit.StoreNumber, ADS.ActiveAudit.AuditId, out outputDataSet);

            if (hasData)
            {
                // Create Report
                reportObject.ReportTitle = "Missing Items Short List";
                reportObject.ReportStoreDesc = string.Format("{0} \n #{1}", _storeName, ADS.ActiveAudit.StoreNumber);

                var MsgItemsReport = new MissingItemsReport("Missing Items Short List");

                MsgItemsReport.reportObject = reportObject;

                if (MsgItemsReport.CreateReport(outputDataSet.Tables[AuditReportsProcedures.MISSING_ITEMS]))
                    AuditDesktopSession.ShowPDFFile(System.IO.Path.GetFullPath(MsgItemsReport.reportObject.ReportTempFileFullName)
                                                    , false);
            }
        }

        private void bwLoadMissingItems_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CommonDatabaseContext dataContext = e.Result as CommonDatabaseContext;

            if (dataContext != null && !dataContext.Result)
            {
                ProcessingMessage.Close();
                MessageBox.Show(dataContext.ErrorText);
                NavControlBox.Action = NavBox.NavAction.CANCEL;
                return;
            }

            PopulateMissingItems();

            tableLayoutPanel1.Enabled = true;
            ProcessingMessage.Close();
        }

        private void ddlFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateMissingItems();
        }

        private void ProcessMissingItems_Load(object sender, EventArgs e)
        {
            PopulateAuditIndicatorFilter();

            LoadMissingItems();
        }

        void bwLoadMissingItems_DoWork(object sender, DoWorkEventArgs e)
        {
            TrakkerItems = new List<TrakkerItem>();
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            InventoryAuditProcedures.GetMissingItems(TrakkerItems, ADS.ActiveAudit, dataContext);

            e.Result = dataContext;
        }

        private void gvMissingItems_SelectionChanged(object sender, EventArgs e)
        {
            SetButtonsStateBasedOnSelections();
        }

        # region Helper Methods

        private void ExecuteUserOption(ProcessMissingUserOptions processMissingUserOption)
        {
            if (gvMissingItems.SelectedRows.Count == 0)
            {
                return;
            }

            if ((processMissingUserOption == ProcessMissingUserOptions.RECONCILE) && gvMissingItems.SelectedRows.Count != 1)
            {
                return;
            }

            List<TrakkerItem> items = new List<TrakkerItem>();
            foreach (DataGridViewRow row in gvMissingItems.SelectedRows)
            {
                TrakkerItem item = row.Tag as TrakkerItem;
                items.Add(item);
            }

            ProcessMissingContext processMissingContext = new ProcessMissingContext();
            processMissingContext.Audit = ADS.ActiveAudit;
            processMissingContext.Items = items;
            processMissingContext.UserOption = processMissingUserOption;

            if (processMissingUserOption == ProcessMissingUserOptions.RECONCILE)
            {
                SelectTempIcn selectTempIcn = new SelectTempIcn();
                if (selectTempIcn.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                processMissingContext.TrakkerSequenceNumber = selectTempIcn.TrakkerSequenceNumber;
            }
            else if (processMissingUserOption == ProcessMissingUserOptions.CHARGEOFF)
            {
                processMissingContext.ChargeOffReason = ChargeOffContext.ItemReason;
            }

            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            InventoryAuditProcedures.ProcessMissing(processMissingContext, dataContext);

            if (!dataContext.Result)
            {
                //ProcessingMessage.Close();
                MessageBox.Show(dataContext.ErrorText);
                return;
            }

            LoadMissingItems();
        }

        private string GetAuditIndicator(TrakkerItem item)
        {
            return item.GetProcessMissingIndicator().ToString();
        }

        private IEnumerable<TrakkerItem> GetFilteredItems()
        {
            ProcessMissingIndicator selectedFilter = ((EnumPair<ProcessMissingIndicator>)ddlFilterBy.SelectedItem).Value;

            return TrakkerItems.FindAll(t => (t.GetProcessMissingIndicator() & selectedFilter) != 0);
        }

        private void LoadMissingItems()
        {
            tableLayoutPanel1.Enabled = false;
            gvMissingItems.Rows.Clear();

            BackgroundWorker bwLoadMissingItems = new BackgroundWorker();
            bwLoadMissingItems.DoWork += new DoWorkEventHandler(bwLoadMissingItems_DoWork);
            bwLoadMissingItems.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwLoadMissingItems_RunWorkerCompleted);
            bwLoadMissingItems.RunWorkerAsync();

            ProcessingMessage = new ProcessingMessage("Loading missing items...", 0);
            ProcessingMessage.ShowDialog(this);
        }

        private void PopulateAuditIndicatorFilter()
        {
            List<EnumPair<ProcessMissingIndicator>> userOptions = new List<EnumPair<ProcessMissingIndicator>>();
            userOptions.Add(new EnumPair<ProcessMissingIndicator>("All", ProcessMissingIndicator.Found | ProcessMissingIndicator.Reconciled | ProcessMissingIndicator.ChargedOff | ProcessMissingIndicator.Missing));
            userOptions.Add(new EnumPair<ProcessMissingIndicator>("Charged Off", ProcessMissingIndicator.ChargedOff));
            userOptions.Add(new EnumPair<ProcessMissingIndicator>("Found", ProcessMissingIndicator.Found));
            userOptions.Add(new EnumPair<ProcessMissingIndicator>("Missing", ProcessMissingIndicator.Missing));
            userOptions.Add(new EnumPair<ProcessMissingIndicator>("Reconciled", ProcessMissingIndicator.Reconciled));

            ddlFilterBy.DataSource = userOptions;
            ddlFilterBy.SelectedIndexChanged += new EventHandler(ddlFilterBy_SelectedIndexChanged);
        }

        private void PopulateMissingItems()
        {
            if (TrakkerItems == null)
            {
                ProcessingMessage.Close();
                MessageBox.Show("Unable to load missing items.");
                NavControlBox.Action = NavBox.NavAction.CANCEL;
                return;
            }

            gvMissingItems.Rows.Clear();

            foreach (TrakkerItem item in GetFilteredItems())
            {
                DataGridViewRow row = gvMissingItems.Rows.AddNew();
                row.Cells[colAuditIndicator.Index].Value = GetAuditIndicator(item);
                row.Cells[colIcn.Index].Value = item.Icn.ToString();
                row.Cells[colStatus.Index].Value = item.Status;
                row.Cells[colCost.Index].Value = item.PfiAmount;
                row.Cells[colDescription.Index].Value = item.Description;
                row.Cells[colRefNumber.Index].Value = item.XIcn.GetShortCode();
                if (item.GetProcessMissingIndicator() == ProcessMissingIndicator.ChargedOff)
                {
                    row.Cells[colReason.Index].Value = item.ChargeOffReason;
                }
                else
                {
                    row.Cells[colReason.Index].Value = string.Empty;
                }
                row.Tag = item;

                if (item.GetProcessMissingIndicator() == ProcessMissingIndicator.Missing)
                {
                    row.Cells[colAuditIndicator.Index].Style.ForeColor = Color.Red;
                }
            }

            gvMissingItems.Columns[colCost.Index].DefaultCellStyle.Format = "c";
            gvMissingItems.SetupAutoSizeColumns();
            SetButtonsStateBasedOnSelections();
            Refresh();
        }

        private void SetButtonsStateBasedOnSelections()
        {
            bool hasNonMissingSelected = (from DataGridViewRow row in gvMissingItems.SelectedRows
                                          where row.Tag is TrakkerItem && ((TrakkerItem)row.Tag).GetProcessMissingIndicator() != ProcessMissingIndicator.Missing
                                          select row).Any();

            bool hasMissingSelected = (from DataGridViewRow row in gvMissingItems.SelectedRows
                                       where row.Tag is TrakkerItem && ((TrakkerItem)row.Tag).GetProcessMissingIndicator() == ProcessMissingIndicator.Missing
                                       select row).Any();

            btnChargeOff.Enabled = !hasNonMissingSelected && gvMissingItems.SelectedRows.Count >= 1;
            btnReconcile.Enabled = !hasNonMissingSelected && gvMissingItems.SelectedRows.Count == 1;
            btnFound.Enabled = !hasNonMissingSelected && gvMissingItems.SelectedRows.Count >= 1;
            btnUndo.Enabled = !hasMissingSelected && gvMissingItems.SelectedRows.Count >= 1;
            btnPreviousAuditTrakker.Enabled = gvMissingItems.SelectedRows.Count == 1;
        }
        # endregion

        private void gvMissingItems_AutoSizeColumnsModeChanged(object sender, DataGridViewAutoSizeColumnsModeEventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Icn icn = new Icn(txtICN.Text);
            if (!icn.Initialized)
            {
                MessageBox.Show("Invalid ICN");
                return;
            }

            var rows = (from DataGridViewRow row in gvMissingItems.Rows
                     where row.Tag is TrakkerItem && ((TrakkerItem)row.Tag).Icn.GetShortCode() == icn.GetShortCode()
                     select row).ToList();

            if (rows.Count == 0)
            {
                MessageBox.Show("ICN not found");
                return;
            }

            txtICN.Text = string.Empty;

            gvMissingItems.ClearSelection();

            rows[0].Selected = true;
            gvMissingItems.FirstDisplayedScrollingRowIndex = rows[0].Index;
        }

        private void txtICN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(sender, e);
            }
        }

        private void gvMissingItems_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column == colReason)
            {
                if (!string.IsNullOrEmpty(e.CellValue1.ToString()) && string.IsNullOrEmpty(e.CellValue2.ToString()))
                {
                    e.Handled = true;
                    e.SortResult = -1;
                }
                else if (string.IsNullOrEmpty(e.CellValue1.ToString()) && !string.IsNullOrEmpty(e.CellValue2.ToString()))
                {
                    e.Handled = true;
                    e.SortResult = 1;
                }
            }
        }
    }
}
