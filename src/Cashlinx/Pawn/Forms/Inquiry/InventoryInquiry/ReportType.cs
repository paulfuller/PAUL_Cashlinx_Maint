using System;
using System.Windows.Forms;

namespace Pawn.Forms.Inquiry.InventoryInquiry
{
    public partial class ReportType : Form
    {
        public ReportType()
        {
            InitializeComponent();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ContinueBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        public string[] getFilterNames ()
        {
            string[] retval = null;
            if (this.report_type_cb.SelectedItems.Count > 0)
            {
                retval = new string[this.report_type_cb.SelectedItems.Count];
                this.report_type_cb.SelectedItems.CopyTo(retval, 0);
            }

            return retval;
        }

        public string[] getFilters ()
        {
            var retval = new string[report_type_cb.SelectedIndices.Count];

            for (int i = 0; i < this.report_type_cb.SelectedIndices.Count; i++)
            {
                switch (this.report_type_cb.SelectedIndices[i])
                {
                    case 0:
                        retval[i] = "CAT_CODE >= 1000 and CAT_CODE <= 1900";
                        break;

                    case 1:
                        retval[i] = "CAT_CODE >= 4000 and CAT_CODE <= 4900";
                        break;

                    case 2:
                        retval[i] = "CAT_CODE >= 0 and CAT_CODE <= 9999";
                        break;

                    case 3:
                        retval[i] = "CAT_CODE >= 2000 and CAT_CODE <= 9999";
                        break;
                }
            }

            return retval;
        }

    }
}
