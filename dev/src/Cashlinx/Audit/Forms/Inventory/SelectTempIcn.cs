using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Audit.Logic;
using Common.Controllers.Database;
using Common.Libraries.Forms;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;

namespace Audit.Forms.Inventory
{
    public partial class SelectTempIcn : AuditWindowBase
    {
        public int TrakkerSequenceNumber { get; private set; }

        private List<TrakkerItem> TempItems { get; set; }
        private ProcessingMessage ProcessingMessage { get; set; }

        public SelectTempIcn()
        {
            InitializeComponent();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (gvItems.SelectedRows.Count != 1)
            {
                return;
            }

            TrakkerItem item = gvItems.SelectedRows[0].Tag as TrakkerItem;

            if (item == null)
            {
                return;
            }

            TrakkerSequenceNumber = item.SequenceNumber;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            TrakkerSequenceNumber = 0;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SelectTempIcn_Load(object sender, EventArgs e)
        {
            gvItems.Rows.Clear();

            BackgroundWorker bwLoadTempIcns = new BackgroundWorker();
            bwLoadTempIcns.DoWork += new DoWorkEventHandler(bwLoadTempIcns_DoWork);
            bwLoadTempIcns.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwLoadTempIcns_RunWorkerCompleted);
            bwLoadTempIcns.RunWorkerAsync();

            ProcessingMessage = new ProcessingMessage("Loading temporary items...", 0);
            ProcessingMessage.ShowDialog(this);
        }

        void bwLoadTempIcns_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CommonDatabaseContext dataContext = e.Result as CommonDatabaseContext;

            if (dataContext != null && !dataContext.Result)
            {
                ProcessingMessage.Close();
                MessageBox.Show(dataContext.ErrorText);
                TrakkerSequenceNumber = 0;
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            if (TempItems == null)
            {
                ProcessingMessage.Close();
                MessageBox.Show("Unable to load temporary icn's.");
                TrakkerSequenceNumber = 0;
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            foreach (TrakkerItem item in TempItems)
            {
                DataGridViewRow row = gvItems.Rows.AddNew();
                row.Cells[colIcn.Index].Value = item.Icn.ToString();
                row.Cells[colStatus.Index].Value = item.Status;
                row.Cells[colCost.Index].Value = item.PfiAmount.ToString("c");
                row.Cells[colDescription.Index].Value = item.Description;
                row.Tag = item;
            }

            gvItems.SetupAutoSizeColumns();
            Refresh();
            ProcessingMessage.Close();
        }

        void bwLoadTempIcns_DoWork(object sender, DoWorkEventArgs e)
        {
            TempItems = new List<TrakkerItem>();
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            InventoryAuditProcedures.GetTemporaryIcns(TempItems, ADS.ActiveAudit, dataContext);

            e.Result = dataContext;
        }
    }
}
