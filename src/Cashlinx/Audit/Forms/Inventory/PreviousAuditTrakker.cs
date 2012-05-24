using System;
using Common.Libraries.Objects.Audit;

namespace Audit.Forms.Inventory
{
    public partial class PreviousAuditTrakker : AuditWindowBase
    {
        public PreviousAuditTrakker(TrakkerItem item)
        {
            Item = item;
            InitializeComponent();
        }

        public TrakkerItem Item { get; private set; }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PreviousAuditTrakker_Load(object sender, EventArgs e)
        {
            lblPreviousLocation.Text = Item.PreviousLocation;
        }
    }
}
