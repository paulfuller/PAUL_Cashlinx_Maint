using Common.Libraries.Forms.Components;
using System;
using System.Windows.Forms;
using Common.Libraries.Objects.Retail;

namespace Pawn.Forms.Retail
{
    public partial class NxtComments : Form
    {
        public RetailItem Item { get; set; }

        public NxtComments(RetailItem item)
        {
            InitializeComponent();
            Item = item;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();           
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            Item.NxtComments = txtComments.Text;
            this.Close();
        }

        private void txtComments_TextChanged(object sender, EventArgs e)
        {
            continueButton.Enabled = txtComments.Text.Trim().Length > 0;
        }
    }
}
