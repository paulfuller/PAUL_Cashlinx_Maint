using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Shared;
using Reports.Inquiry;

namespace Pawn.Forms.Inquiry.InventoryInquiry
{
    public partial class ItemCostRevisionHistory : Form
    {
        private DataSet _theData;
        private String _icn;
        private String _description;
        
        private ItemCostRevisionHistory()
        {
            InitializeComponent();
        }

        public ItemCostRevisionHistory(String icn, String description)
        {
            InitializeComponent();

            _icn = icn;
            _description = description;

            this.labelICN.Text = icn;
            this.labelMerchandiseDescription.Text = description;
        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Print_btn_Click(object sender, EventArgs e)
        {
            var rpt = new CostRevisionReport(PdfLauncher.Instance);

            ReportObject reportObject = new ReportObject();

            reportObject.ReportTempFileFullName = "Cost Revision";

            rpt.ReportObject = reportObject;

            rpt.CreateReport(_icn, _description, _theData);
        }

        private void ItemCostRevisionHistory_Load(object sender, EventArgs e)
        {
            DataSet ds = InventoryInquiries.getItemCostRevision(new Icn(this.labelICN.Text.Replace(" ", "")));

            if (ds.IsNullOrEmpty())
            {
                MessageBox.Show("No Results");
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            _theData = ds;

            this.resultsGrid_dg.DataSource = ds.Tables[0];
            this.resultsGrid_dg.Columns[0].Width = 150;
            this.resultsGrid_dg.Columns[1].Width = 100;
            this.resultsGrid_dg.Columns[2].Width = 100;
            this.resultsGrid_dg.Columns[3].Width = 150;
            this.resultsGrid_dg.Columns[4].Width = 200;
            this.resultsGrid_dg.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            this.resultsGrid_dg.Columns[0].HeaderText = "Transaction Date";
            this.resultsGrid_dg.Columns[1].HeaderText = "Current Cost";
            this.resultsGrid_dg.Columns[2].HeaderText = "Added Cost";
            this.resultsGrid_dg.Columns[3].HeaderText = "New Cost";
            this.resultsGrid_dg.Columns[4].HeaderText = "Retail Cost";
            this.resultsGrid_dg.Columns[5].HeaderText = "User Id";
        }

    }
}
