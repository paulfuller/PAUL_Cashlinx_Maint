using System;
using System.Windows.Forms;

//-------------------------------------------------------
// "Merchandise Description Change" - was removed from list because 
// database does not support tracking of this field.
// To reenable, add "Merchandise Description Change" to the list box.
//-------------------------------------------------------
namespace Pawn.Forms.Inquiry.InventoryInquiry
{
    public partial class RevisionsType : Form
    {
        public RevisionsType()
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

        public string getFilterName()
        {
            string retval = String.Empty;
            if (this.report_type_cb.SelectedItems.Count > 0)
            {
                retval = this.report_type_cb.SelectedItem.ToString();
            }

            return retval;
        }
    }
}
