using System;
using System.Data;
using System.Windows.Forms;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Inquiry.InventoryInquiry
{
    public partial class MsdeDescriptionRevisionHistory : Form
    {
        public MsdeDescriptionRevisionHistory()
        {
            InitializeComponent();
        }

        public MsdeDescriptionRevisionHistory(String icn, String description)
        {
            InitializeComponent();

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
            MessageBox.Show("TODO: Report Here");
        }

        private void ItemCostRevisionHistory_Load(object sender, EventArgs e)
        {
            // TODO: load Item Cost Revision History from the database.
            DataSet ds = InventoryInquiries.getItemDescRevision(new Icn(this.labelICN.Text.Replace(" ", "")));

            if (ds.IsNullOrEmpty())
            {
                MessageBox.Show("No Results");
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            this.resultsGrid_dg.DataSource = ds.Tables[0];
            this.resultsGrid_dg.Columns[0].Width = 200;
            this.resultsGrid_dg.Columns[1].Width = 500;
            this.resultsGrid_dg.Columns[2].Width = 100;

            this.resultsGrid_dg.Columns[0].HeaderText = "Transaction Date";
            this.resultsGrid_dg.Columns[1].HeaderText = "Merchandise Description";
            this.resultsGrid_dg.Columns[2].HeaderText = "User Id";
        }
    }
}
