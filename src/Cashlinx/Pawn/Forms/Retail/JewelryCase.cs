using Common.Libraries.Forms.Components;
using System;
using System.Windows.Forms;
using Common.Libraries.Objects.Retail;

namespace Pawn.Forms.Retail
{
    public partial class JewelryCase : Form
    {
        public RetailItem Item { get; set; }

        public JewelryCase(RetailItem item)
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
            if (txtCaseNumber.TextLength == 0)
            {
                MessageBox.Show("Jewelry Case Number cannot be blank");
                DialogResult = DialogResult.None;
            }
            else
            {
                Item.JeweleryCaseNumber = txtCaseNumber.Text;
                this.Close();
            }
        }
    }
}
