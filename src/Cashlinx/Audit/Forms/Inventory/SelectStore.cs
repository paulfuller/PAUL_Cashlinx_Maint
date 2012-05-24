using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;

namespace Audit.Forms.Inventory
{
    public partial class SelectStore : AuditWindowBase
    {
        public SelectStore()
        {
            InitializeComponent();
        }

        private void SelectStore_Load(object sender, EventArgs e)
        {
            List<SiteId> stores = new List<SiteId>();
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            bool retVal = InventoryAuditProcedures.GetStores(stores, dataContext);

            if (!retVal)
            {
                MessageBox.Show(dataContext.ErrorText);
                return;
            }

            ddlStore.DisplayMember = "StoreNumber";
            ddlStore.DataSource = stores;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            SiteId store = ddlStore.SelectedItem as SiteId;
            if (store == null)
            {
                return;
            }

            List<InventoryAuditVO> audits = new List<InventoryAuditVO>();
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            if (!InventoryAuditProcedures.GetInventoryAudits(audits, null, "ACTIVE", dataContext))
            {
                MessageBox.Show("Error loading active audits.");
                return;
            }

            if (audits.Any(a => a.StoreNumber == store.StoreNumber))
            {
                MessageBox.Show("There is already an active audit for this store.  Complete the existing audit and try again.");
                return;
            }

            ADS.ActiveAudit = new InventoryAuditVO();
            ADS.ActiveAudit.StoreNumber = store.StoreNumber;
            ADS.ActiveAudit.InitiatedBy = ADS.ActiveUserData.CurrentUserFullName;
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }
    }
}
