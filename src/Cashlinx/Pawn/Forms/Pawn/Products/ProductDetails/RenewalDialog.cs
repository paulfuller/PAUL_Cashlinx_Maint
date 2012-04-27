using System;
using System.Windows.Forms;

namespace Pawn.Forms.Pawn.Products.ProductDetails
{
    public partial class RenewalDialog : Form
    {
        private string _displayMessage;

        public RenewalDialog(string sDisplayMessage)
        {
            InitializeComponent();
            _displayMessage = sDisplayMessage;
        }


        private void OverrideReason_Load(object sender, EventArgs e)
        {
            messageTextLabel.Text = _displayMessage;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
