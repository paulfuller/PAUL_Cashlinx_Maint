using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;

namespace Audit.Forms.Inventory
{
    public partial class InitiateAudit : AuditWindowBase
    {
        public InitiateAudit()
        {
            InitializeComponent();
            PopulateAuditReasons();
            PopulateAuditScopes();
            PopulateAuditTypes();
            PopulateReportDetails();
        }

        private void PopulateAuditScopes()
        {
            List<EnumPair<AuditScope>> scopes = new List<EnumPair<AuditScope>>();
            scopes.Add(new EnumPair<AuditScope>("Full", AuditScope.FULL));
            scopes.Add(new EnumPair<AuditScope>("Partial", AuditScope.PARTIAL));

            cboAuditScope.DataSource = scopes;
        }

        private void PopulateAuditTypes()
        {
            List<EnumPair<AuditType>> types = new List<EnumPair<AuditType>>();
            types.Add(new EnumPair<AuditType>("Inventory", AuditType.INVENTORY));
            types.Add(new EnumPair<AuditType>("Loan", AuditType.LOAN));

            cboTypeOfAudit.DataSource = types;
        }

        private void PopulateAuditReasons()
        {
            List<EnumPair<AuditReason>> reasons = new List<EnumPair<AuditReason>>();
            reasons.Add(new EnumPair<AuditReason>("Routine", AuditReason.ROUTINECHANGE));
            reasons.Add(new EnumPair<AuditReason>("Special", AuditReason.SPECIAL));
            reasons.Add(new EnumPair<AuditReason>("Manager Change", AuditReason.MANAGERCHANGE));

            cboAuditReason.DataSource = reasons;
        }

        private void PopulateReportDetails()
        {
            List<EnumPair<AuditReportDetail>> reasons = new List<EnumPair<AuditReportDetail>>();
            reasons.Add(new EnumPair<AuditReportDetail>("Include Buy", AuditReportDetail.BUY));
            reasons.Add(new EnumPair<AuditReportDetail>("Include Layaway", AuditReportDetail.LAYAWAY));
            reasons.Add(new EnumPair<AuditReportDetail>("Include Both", AuditReportDetail.BOTH));

            cboReportDetail.DataSource = reasons;
        }

        private void InitiateAudit_Load(object sender, EventArgs e)
        {
            lblShopNumber.Text = ADS.ActiveAudit.StoreNumber;
            lblAuditorName.Text = ADS.ActiveAudit.InitiatedBy;
            lblMarketManagerName.Text = "Unknown";
            cboTypeOfAudit.SelectedIndex = 0;
            cboAuditScope.SelectedIndex = 0;
            cboAuditReason.SelectedIndex = 0;
            cboReportDetail.SelectedIndex = 2;
            
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            string marketManagerName = string.Empty;
            InventoryAuditProcedures.GetMarketManagerName(ref marketManagerName, dataContext);

            if (dataContext.Result)
            {
                lblMarketManagerName.Text = marketManagerName;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            InventoryAuditVO audit = ADS.ActiveAudit;
            audit.AuditScope = ((EnumPair<AuditScope>)cboAuditScope.SelectedItem).Value;
            audit.AuditType = ((EnumPair<AuditType>)cboTypeOfAudit.SelectedItem).Value;
            audit.DateInitiated = ShopDateTime.Instance.FullShopDateTime;
            audit.ExitingShopManager = txtExitingShopManagerName.Text.Trim();
            audit.MarketManagerPresent = cboMarketManagerPresent.AnswerYes;
            audit.AuditReason = ((EnumPair<AuditReason>)cboAuditReason.SelectedItem).Value;
            audit.ReportDetail = ((EnumPair<AuditReportDetail>)cboReportDetail.SelectedItem).Value;

            CommonDatabaseContext dataContext = new CommonDatabaseContext();
            InventoryAuditProcedures.CreateAudit(audit, dataContext);

            if (!dataContext.Result)
            {
                MessageBox.Show("Error initiating audit.");
                return;
            }

            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        private void cboAuditReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnumPair<AuditReason> reason = cboAuditReason.SelectedItem as EnumPair<AuditReason>;

            txtExitingShopManagerName.Enabled = reason.Value == AuditReason.MANAGERCHANGE;
        }
    }
}
