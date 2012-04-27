using System;

namespace Audit.Forms.Inventory
{
    public partial class ConfirmTrakker : AuditWindowBase
    {
        public ConfirmTrakker(int trakkerId)
        {
            InitializeComponent();
            lblTrakkerId.Text = string.Format("Trakker Id {0}?", trakkerId);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
