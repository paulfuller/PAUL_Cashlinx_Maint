using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;

namespace Pawn.Forms.Pawn.Services.ChargeOff
{
    public partial class ChargeOffItemSelection : CustomBaseForm
    {
        public PawnLoan ReplaceLoan;
        public CustomerVO ReplaceCustomer;
        public List<string> ReplacedICN;
        private decimal totalLoanAmount = 0.0m;
        public ChargeOffItemSelection()
        {
            InitializeComponent();
        }

        private void ChargeOffItemSelection_Load(object sender, EventArgs e)
        {
            if (ReplaceLoan.Items != null && ReplaceLoan.Items.Count > 0)
            {
                BindingSource _bindingSource1 = new BindingSource
                {
                        DataSource = ReplaceLoan.Items
                };
                customDataGridViewItems.AutoGenerateColumns = false;
                this.customDataGridViewItems.DataSource = _bindingSource1;
                this.customDataGridViewItems.Columns[0].DataPropertyName = "ticketdescription";
                this.customDataGridViewItems.Columns[1].DataPropertyName = "itemamount";
                this.customDataGridViewItems.Columns[2].DataPropertyName = "icn";
                this.customDataGridViewItems.Columns[1].DefaultCellStyle.Format = "c";
                this.customDataGridViewItems.AutoGenerateColumns = false;
            }
            if (ReplaceLoan.Items != null)
                labelTotalLoanAmount.Text = ReplaceLoan.Items.Sum(i => i.ItemAmount).ToString("c");

            labelCustName.Text = ReplaceCustomer.CustomerName;
            var custAddr = ReplaceCustomer.getHomeAddress();
            if (custAddr != null)
            {
                labelCustAddr.Text = custAddr.Address2;
                labelCustAddr2.Text = custAddr.Address1;
                labelCustAddr3.Text = string.Format("{0},{1} {2}", custAddr.City, custAddr.State_Code, custAddr.ZipCode);
            }

            ReplacedICN = new List<string>();
        }

        private void customDataGridViewItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = -1;
            if (ReplacedICN != null)
                idx = ReplacedICN.FindIndex(pl => pl == customDataGridViewItems.Rows[e.RowIndex].Cells[2].Value.ToString());
            else
                ReplacedICN = new List<string>();


            if (idx >= 0)
            {
                ReplacedICN.RemoveAt(idx);
                totalLoanAmount -= Utilities.GetDecimalValue(customDataGridViewItems.Rows[e.RowIndex].Cells[1].Value.ToString(), 0);
                customDataGridViewItems.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
            else
            {
                ReplacedICN.Add(customDataGridViewItems.Rows[e.RowIndex].Cells[2].Value.ToString());
                totalLoanAmount += Utilities.GetDecimalValue(customDataGridViewItems.Rows[e.RowIndex].Cells[1].Value.ToString(), 0);
                customDataGridViewItems.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.DarkBlue;
            }
            labelTotalLoanAmount.Text = totalLoanAmount.ToString("c");
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            ReplacedICN = new List<string>();
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (ReplacedICN != null)
                ReplacedICN = new List<string>();

            if (ReplacedICN.Count == 0 && customDataGridViewItems.Rows.Count == 1)
            {
                ReplacedICN.Add(customDataGridViewItems.Rows[0].Cells[2].Value.ToString());               
            }
            this.Close();
        }
    }
}
