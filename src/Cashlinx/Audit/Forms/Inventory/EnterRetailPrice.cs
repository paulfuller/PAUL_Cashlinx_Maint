using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Libraries.Utility;
using PawnUtilities;

namespace Audit.Forms.Inventory
{
    public partial class EnterRetailPrice : AuditWindowBase
    {
        public EnterRetailPrice()
        {
            InitializeComponent();
        }

        public decimal RetailPrice { get; set; }

        private void EnterRetailPrice_Load(object sender, EventArgs e)
        {
            txtRetailPrice.Text = RetailPrice.ToString("f2");
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            RetailPrice = Utilities.GetDecimalValue(txtRetailPrice.Text, 0);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtRetailPrice_Leave(object sender, EventArgs e)
        {
            decimal retailPrice = Utilities.GetDecimalValue(txtRetailPrice.Text, -1);

            if (retailPrice < 0)
            {
                MessageBox.Show("Invalid retail price");
            }

            txtRetailPrice.Text = retailPrice.ToString("f2");
        }
    }
}
