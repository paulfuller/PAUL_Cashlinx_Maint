using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Audit.Logic;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Libraries.Forms;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Pawn;

namespace Audit.Forms.Inventory
{
    public partial class ProcessUnexpectedItems : AuditWindowBase
    {
        private List<TrakkerItem> Items { get; set; }
        private ProcessingMessage ProcessingMessage { get; set; }

        public ProcessUnexpectedItems()
        {
            InitializeComponent();
        }

        # region Event Handlers

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void btnChargeOn_Click(object sender, EventArgs e)
        {
            //if (gvUnexpectedItems.SelectedRows.Count != 1)
            //{
            //    return;
            //}

            //TrakkerItem item = gvUnexpectedItems.SelectedRows[0].Tag as TrakkerItem;

            //if (item == null)
            //{
            //    return;
            //}

            //ADS.SelectedTrakkerItem = item;

            if (AuditDesktopSession.Instance.ActivePawnLoan == null)
            {
                AuditDesktopSession.Instance.PawnLoans = new List<PawnLoan>(1)
                                                                    {
                                                                        new PawnLoan()
                                                                    };
                if (AuditDesktopSession.Instance.ActivePawnLoan != null)
                    AuditDesktopSession.Instance.ActivePawnLoan.Items = new List<Item>();
            }

            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "CHARGEON";
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        private void btnReactivate_Click(object sender, EventArgs e)
        {
            ExecuteUserOption(ProcessUnexpectedUserOption.REACTIVATE);
        }

        private void btnUnscan_Click(object sender, EventArgs e)
        {
            ExecuteUserOption(ProcessUnexpectedUserOption.UNSCAN);
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            ExecuteUserOption(ProcessUnexpectedUserOption.UNDO);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            NavControlBox.IsCustom = false;
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        private void bwLoadUnexpectedItems_DoWork(object sender, DoWorkEventArgs e)
        {
            Items = new List<TrakkerItem>();
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            InventoryAuditProcedures.GetUnexpectedItems(Items, ADS.ActiveAudit, dataContext);

            e.Result = dataContext;
        }

        private void bwLoadUnexpectedItems_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CommonDatabaseContext dataContext = e.Result as CommonDatabaseContext;

            if (dataContext != null && !dataContext.Result)
            {
                ProcessingMessage.Close();
                MessageBox.Show(dataContext.ErrorText);
                NavControlBox.Action = NavBox.NavAction.CANCEL;
                return;
            }

            PopulateUnexpectedItems();

            tableLayoutPanel1.Enabled = true;
            ProcessingMessage.Close();
        }

        # endregion

        # region Helper Methods

        private void ExecuteUserOption(ProcessUnexpectedUserOption userOption)
        {
            if (gvUnexpectedItems.SelectedRows.Count != 1)
            {
                return;
            }

            List<TrakkerItem> items = new List<TrakkerItem>();
            List<TrakkerItem> chargeOnItems = new List<TrakkerItem>();
            foreach (DataGridViewRow row in gvUnexpectedItems.SelectedRows)
            {
                TrakkerItem item = row.Tag as TrakkerItem;
                items.Add(item);

                if (item.GetProcessUnexpectedIndicator() == ProcessUnexpectedIndicator.ChargedOn)  
                {
                    chargeOnItems.Add(item); // building list for undo.  these items will be removed
                }
            }

            if (items.Count != 1)
            {
                return;
            }

            if (userOption == ProcessUnexpectedUserOption.REACTIVATE)
            {
                EnterRetailPrice enterRetailPrice = new EnterRetailPrice();
                if (enterRetailPrice.ShowDialog() == DialogResult.OK)
                {
                    items[0].RetailAmount = enterRetailPrice.RetailPrice;
                }
                else
                {
                    return;
                }
            }

            ProcessUnexpectedContext processUnexpectedContext = new ProcessUnexpectedContext();
            processUnexpectedContext.Audit = ADS.ActiveAudit;
            processUnexpectedContext.Items = items;
            processUnexpectedContext.UserOption = userOption;

            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            InventoryAuditProcedures.ProcessUnexpected(processUnexpectedContext, dataContext);

            if (!dataContext.Result)
            {
                //ProcessingMessage.Close();
                MessageBox.Show(dataContext.ErrorText);
                return;
            }

            if (userOption == ProcessUnexpectedUserOption.UNDO)
            {
                foreach (TrakkerItem item in chargeOnItems)
                {
                    Items.Remove(item);
                }
            }

            PopulateUnexpectedItems();
        }

        private string GetAuditIndicator(TrakkerItem item)
        {
            ProcessUnexpectedIndicator indicator = item.GetProcessUnexpectedIndicator();

            if (indicator == ProcessUnexpectedIndicator.ChargedOn)
            {
                return "Charged On";
            }

            return indicator.ToString();
        }

        private void PopulateUnexpectedItems()
        {
            if (Items == null)
            {
                ProcessingMessage.Close();
                MessageBox.Show("Unable to load unexpected items.");
                NavControlBox.Action = NavBox.NavAction.CANCEL;
                return;
            }

            gvUnexpectedItems.Rows.Clear();

            foreach (TrakkerItem item in Items)
            {
                if (item.GetProcessUnexpectedIndicator() == ProcessUnexpectedIndicator.Unscanned)
                {
                    continue;
                }
                DataGridViewRow row = gvUnexpectedItems.Rows.AddNew();
                row.Cells[colAuditIndicator.Index].Value = GetAuditIndicator(item);
                row.Cells[colIcn.Index].Value = item.Icn.ToString();
                row.Cells[colScannerNumber.Index].Value = item.TrakkerId.ToString();
                row.Cells[colScanSequenceNumber.Index].Value = item.SequenceNumber.ToString();
                row.Cells[colScanLocation.Index].Value = item.Location;
                row.Cells[colStatus.Index].Value = item.Status;
                row.Cells[colCost.Index].Value = item.PfiAmount.ToString("c");
                row.Cells[colDescription.Index].Value = item.Description;
                row.Cells[colRetailAmount.Index].Value = item.RetailAmount > 0 ? item.RetailAmount.ToString("0.00") : string.Empty;
                row.Tag = item;

                if (item.GetProcessUnexpectedIndicator() == ProcessUnexpectedIndicator.Unexpected)
                {
                    row.Cells[colAuditIndicator.Index].Style.ForeColor = Color.Red;
                }
            }

            gvUnexpectedItems.SetupAutoSizeColumns();
            SetButtonsStateBasedOnSelections();
            Refresh();
        }

        private void ProcessUnexpectedItems_Load(object sender, EventArgs e)
        {
            LoadUnexpectedItems();
        }

        private void LoadUnexpectedItems()
        {
            tableLayoutPanel1.Enabled = false;
            gvUnexpectedItems.Rows.Clear();

            BackgroundWorker bwLoadUnexpectedItems = new BackgroundWorker();
            bwLoadUnexpectedItems.DoWork += new DoWorkEventHandler(bwLoadUnexpectedItems_DoWork);
            bwLoadUnexpectedItems.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwLoadUnexpectedItems_RunWorkerCompleted);
            bwLoadUnexpectedItems.RunWorkerAsync();

            ProcessingMessage = new ProcessingMessage("Loading unexpected items...", 0);
            ProcessingMessage.ShowDialog(this);
        }

        private void SetButtonsStateBasedOnSelections()
        {
            bool hasNonUnexpectedSelected = (from DataGridViewRow row in gvUnexpectedItems.SelectedRows
                                             where row.Tag is TrakkerItem && ((TrakkerItem)row.Tag).GetProcessUnexpectedIndicator() != ProcessUnexpectedIndicator.Unexpected
                                             select row).Any();

            bool hasUnexpectedSelected = (from DataGridViewRow row in gvUnexpectedItems.SelectedRows
                                          where row.Tag is TrakkerItem && ((TrakkerItem)row.Tag).GetProcessUnexpectedIndicator() == ProcessUnexpectedIndicator.Unexpected
                                          select row).Any();

            btnChargeOn.Enabled = true;
            btnReactivate.Enabled = !hasNonUnexpectedSelected && gvUnexpectedItems.SelectedRows.Count == 1;
            btnUnscan.Enabled = !hasNonUnexpectedSelected && gvUnexpectedItems.SelectedRows.Count == 1;
            btnUndo.Enabled = !hasUnexpectedSelected && gvUnexpectedItems.SelectedRows.Count == 1;
        }

        # endregion

        private void gvUnexpectedItems_SelectionChanged(object sender, EventArgs e)
        {
            SetButtonsStateBasedOnSelections();
        }
    }
}
