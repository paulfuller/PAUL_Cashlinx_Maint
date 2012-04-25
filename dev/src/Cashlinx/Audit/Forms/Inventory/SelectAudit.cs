using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Audit.Logic;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Libraries.Forms;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;

namespace Audit.Forms.Inventory
{
    public partial class SelectAudit : AuditWindowBase
    {
        public SelectAudit()
        {
            InitializeComponent();
        }

        private List<SiteId> CachedStores { get; set; }
        private ProcessingMessage ProcessingMessage { get; set; }

        public event EventHandler CreateNewAudit;

        private void SelectAudit_Load(object sender, EventArgs e)
        {
            ddlAuditStatus.SelectedIndex = 0;
            //ddlAuditStatus_SelectedIndexChanged(sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            if (gvAudits.SelectedRows.Count != 1)
            {
                return;
            }

            InventoryAuditVO audit = gvAudits.SelectedRows[0].Tag as InventoryAuditVO;

            if (audit == null)
            {
                return;
            }

            if (MessageBox.Show("All data will be lost. Are you sure you want to abort this audit?", "Abort Audit", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

            audit.Status = AuditStatus.ABORT;

            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            InventoryAuditProcedures.EditAudit(audit, dataContext);

            if (!dataContext.Result)
            {
                MessageBox.Show("Error while aborting audit.");
                return;
            }

            LoadAudits();
        }

        private void btnNewAudit_Click(object sender, EventArgs e)
        {
            List<InventoryAuditVO> audits = new List<InventoryAuditVO>();
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            if (!InventoryAuditProcedures.GetInventoryAudits(audits, null, "ACTIVE", dataContext))
            {
                MessageBox.Show("Error loading active audits.");
                return;
            }

            if (audits.Any(a => a.StoreNumber == ADS.LoggedInUserSecurityProfile.StoreNumber))
            {
                MessageBox.Show("There is already an active audit for this store.  Complete the existing audit and try again.");
                return;
            }

            ADS.ActiveAudit = new InventoryAuditVO();
            ADS.ActiveAudit.StoreNumber = ADS.LoggedInUserSecurityProfile.StoreNumber;
            ADS.ActiveAudit.InitiatedBy = ADS.ActiveUserData.CurrentUserFullName;

            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "INITIATEAUDIT";
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (gvAudits.SelectedRows.Count != 1)
            {
                return;
            }

            InventoryAuditVO audit = gvAudits.SelectedRows[0].Tag as InventoryAuditVO;

            if (audit == null)
            {
                return;
            }

            panelButtons.Enabled = false;
            ProcessingMessage = new ProcessingMessage("Loading Audit Information", 0);
            ProcessingMessage.Show();
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            InventoryAuditProcedures.GetSummaryInfo(audit, dataContext);
            InventoryAuditProcedures.GetAdditionalAuditInfo(audit, dataContext);
            ProcessingMessage.Close();
            
            if (!dataContext.Result)
            {
                MessageBox.Show("Error loading CACC information");
                panelButtons.Enabled = true;
                return;
            }

            ADS.ActiveAudit = audit;

            switch (audit.Status)
            {
                case AuditStatus.ACTIVE:
                    panelButtons.Enabled = true;
                    NavControlBox.IsCustom = true;
                    NavControlBox.CustomDetail = "VIEWACTIVEAUDIT";
                    NavControlBox.Action = NavBox.NavAction.SUBMIT;
                    break;
                case AuditStatus.CLOSED:
                    panelButtons.Enabled = true;
                    NavControlBox.IsCustom = true;
                    NavControlBox.CustomDetail = "VIEWCLOSEDAUDIT";
                    NavControlBox.Action = NavBox.NavAction.SUBMIT;
                    break;
                default:
                    MessageBox.Show("Status not implemented: " + audit.Status.ToString());
                    panelButtons.Enabled = true;
                    break;
            }
        }

        private void ddlAuditStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlShopNumber.Visible = IsAuditStatusClosed();
            lblShopNumber.Visible = IsAuditStatusClosed();

            if (IsAuditStatusClosed())
            {
                LoadStoresIfNecessary();
            }

            LoadAudits();
        }

        private void ddlShopNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAudits();
        }

        private void LoadStoresIfNecessary()
        {
            if (CachedStores != null)
            {
                return;
            }

            CachedStores = new List<SiteId>();
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            bool retVal = InventoryAuditProcedures.GetStores(CachedStores, dataContext);

            if (!retVal)
            {
                MessageBox.Show(dataContext.ErrorText);
                return;
            }

            ddlShopNumber.DisplayMember = "StoreNumber";
            ddlShopNumber.DataSource = CachedStores;
        }

        private void LoadAudits()
        {
            List<InventoryAuditVO> audits = new List<InventoryAuditVO>();
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            bool retVal = false;

            gvAudits.Rows.Clear();

            if (IsAuditStatusClosed())
            {
                retVal = InventoryAuditProcedures.GetInventoryAudits(audits, ddlShopNumber.Text, null, dataContext); //Closed
            }
            else
            {
                retVal = InventoryAuditProcedures.GetInventoryAudits(audits, null, "ACTIVE", dataContext);
            }

            if (!retVal)
            {
                MessageBox.Show(dataContext.ErrorText);
                return;
            }

            List<InventoryAuditVO> sortedAudits = new List<InventoryAuditVO>();
            foreach (var audit in audits.FindAll(a => a.StoreNumber == ADS.CurrentSiteId.StoreNumber).OrderBy(ao => ao.DateInitiated).ToList())
            {
                sortedAudits.Add(audit);
            }

            foreach (var audit in audits.FindAll(a => a.StateCode == ADS.CurrentSiteId.State && !sortedAudits.Any(sa => sa.AuditId == a.AuditId)).OrderBy(ao => ao.StoreNumber).ThenBy(at => at.DateInitiated).ToList())
            {
                sortedAudits.Add(audit);
            }

            foreach (var audit in audits.FindAll(a => !sortedAudits.Any(sa => sa.AuditId == a.AuditId)).OrderBy(ao => ao.StoreNumber).ThenBy(at => at.DateInitiated).ToList())
            {
                sortedAudits.Add(audit);
            }

            foreach (InventoryAuditVO audit in sortedAudits)
            {
                DataGridViewRow row = gvAudits.Rows.AddNew();
                row.Cells[colAuditNumber.Index].Value = audit.AuditId;
                row.Cells[colAuditType.Index].Value = audit.AuditType.ToString();
                row.Cells[colShopNumber.Index].Value = audit.StoreNumber;
                row.Cells[colDateInitiated.Index].Value = audit.DateInitiated;
                row.Cells[colLastUpdated.Index].Value = audit.LastUpdated;
                row.Cells[colInitiatedBy.Index].Value = audit.InitiatedBy;
                row.Tag = audit;
            }
        }

        private bool IsAuditStatusClosed()
        {
            return ddlAuditStatus.Text.Equals("Closed");
        }

        private void SelectAudit_Activated(object sender, EventArgs e)
        {
            if (ADS.RefreshAuditList)
            {
                ADS.RefreshAuditList = false;
                LoadAudits();
            }
        }
    }
}
