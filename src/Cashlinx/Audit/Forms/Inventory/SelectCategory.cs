using System;
using System.Windows.Forms;

namespace Audit.Forms.Inventory
{
    public partial class SelectCategory : AuditWindowBase
    {
        public SelectCategory()
        {
            InitializeComponent();
        }

        private void SelectCategory_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
