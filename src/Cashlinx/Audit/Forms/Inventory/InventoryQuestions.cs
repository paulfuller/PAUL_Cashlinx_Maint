using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Audit.Logic;
using Audit.UserControls;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Libraries.Forms;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Audit;

namespace Audit.Forms.Inventory
{
    public partial class InventoryQuestions : AuditWindowBase
    {
        public List<InventoryQuestion> Questions { get; set; }
        private ProcessingMessage ProcessingMessage { get; set; }

        public InventoryQuestions()
        {
            InitializeComponent();   
        }

        private void PopulateQuestions()
        {
            foreach (InventoryQuestion question in Questions)
            {
                InventoryResponse response = new InventoryResponse(question);
                tableLayoutPanelQuestions.Controls.Add(response);
            }
        }

        private void LoadQuestions()
        {
            Questions = new List<InventoryQuestion>();
            Questions.Add(new InventoryQuestion(1, "Excess Jewelry Count"));
            Questions.Add(new InventoryQuestion(13, "Refurb Not Transferred in Count"));
            Questions.Add(new InventoryQuestion(2, "Excess Jewelry Total"));
            Questions.Add(new InventoryQuestion(14, "Refurb Not Transferred in Total"));
            Questions.Add(new InventoryQuestion(3, "Backroom Overstock Count"));
            Questions.Add(new InventoryQuestion(15, "Master Cash Locked/Secure Location"));
            Questions.Add(new InventoryQuestion(4, "Backroom Overstock Total"));
            Questions.Add(new InventoryQuestion(16, "One Person/One Cash Drawer"));
            Questions.Add(new InventoryQuestion(5, "Refurb Not Processed Count"));
            Questions.Add(new InventoryQuestion(17, "Nightly Jewelry Storage"));
            Questions.Add(new InventoryQuestion(6, "Refurb Not Processed Total"));
            Questions.Add(new InventoryQuestion(18, "Current Copy of CA Yellow Pages"));
            Questions.Add(new InventoryQuestion(7, "Scrap Not Processed Count"));
            Questions.Add(new InventoryQuestion(19, "Dye Paks Tested/Working Properly"));
            Questions.Add(new InventoryQuestion(8, "Scrap Not Processed Total"));
            Questions.Add(new InventoryQuestion(20, "Broken/Excess CCTV Equipment"));
            Questions.Add(new InventoryQuestion(9, "Broken Merchandise Count"));
            Questions.Add(new InventoryQuestion(21, "All Cash Tran to Master to COB"));
            Questions.Add(new InventoryQuestion(10, "Broken Merchandise Total"));
            Questions.Add(new InventoryQuestion(22, "All Cash Tran to Master at COB"));
            Questions.Add(new InventoryQuestion(11, "Police Hold Count"));
            Questions.Add(new InventoryQuestion(23, "What Message is on the Marquee"));
            Questions.Add(new InventoryQuestion(12, "Police Hold Total"));
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            StringBuilder comments = new StringBuilder();
            comments.Append(commentsTextbox.Text);
            ADS.InventoryQuestionsAdditionalComments = comments;
            Reports.CallAuditReport callReport = new Reports.CallAuditReport();
            callReport.SetInventorySummaryCACCData();
            callReport.GetPreAuditReport(true, false);
            btnContinue.Enabled = false;
            BackgroundWorker bwPostAudit = new BackgroundWorker();
            bwPostAudit.DoWork += new DoWorkEventHandler(bwPostAudit_DoWork);
            bwPostAudit.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwPostAudit_RunWorkerCompleted);
            bwPostAudit.RunWorkerAsync();

            ProcessingMessage = new ProcessingMessage("Posting audit...", 0);
            ProcessingMessage.ShowDialog(this);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void bwPostAudit_DoWork(object sender, DoWorkEventArgs e)
        {
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            ADS.beginTransactionBlock();
            InventoryAuditProcedures.PostAudit(ADS.ActiveAudit, dataContext);

            if (!dataContext.Result)
            {
                ADS.endTransactionBlock(EndTransactionType.ROLLBACK);
            }
            else
            {
                ADS.endTransactionBlock(EndTransactionType.COMMIT);
            }

            e.Result = dataContext;
        }

        private void bwPostAudit_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CommonDatabaseContext dataContext = e.Result as CommonDatabaseContext;
            ProcessingMessage.Close();
            if (!dataContext.Result)
            {
                btnContinue.Enabled = true;
                MessageBox.Show("Error while posting audit");
                return;
            }
            ADS.InventoryQuestionsWithResponses = Questions.FindAll(q => !string.IsNullOrEmpty(q.Response));
            Reports.CallAuditReport callReport = new Reports.CallAuditReport();
            callReport.GetInventorySummaryReport();
            callReport.GetPostAuditReport();
            callReport.GetTrakkerUploadReport(true, false);

            btnContinue.Enabled = true;
            ADS.RefreshAuditList = true;
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        private void InventoryQuestions_Load(object sender, EventArgs e)
        {
            InventoryAuditVO audit = ADS.ActiveAudit;
            lblAuditor.Text = audit.Auditor;
            lblDiv.Text = audit.Division;
            lblMarketManager.Text = audit.MarketManager;
            lblOverShort.Text = audit.OverShort.ToString("c");
            lblStoreManager.Text = audit.ActiveShopManager;
            lblYTDShortage.Text = audit.YTDShortage.ToString("c");
            LoadQuestions();
            PopulateQuestions();
        }
    }
}
