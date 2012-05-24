using System;
using System.Windows.Forms;

namespace Pawn.Forms.Pawn.ShopAdministration.ShopCash
{
    public partial class TransferReject : Form
    {
        public string RejectReason;
        public TransferReject()
        {
            InitializeComponent();
        }

        private void customButtonSubmit_Click(object sender, EventArgs e)
        {
            RejectReason = customTextBox1.Text;
            this.Close();
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
