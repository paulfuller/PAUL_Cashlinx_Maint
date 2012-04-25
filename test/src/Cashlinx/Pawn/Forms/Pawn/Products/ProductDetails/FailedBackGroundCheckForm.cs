using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Libraries.Objects.Pawn;

namespace Pawn.Forms.Pawn.Products.ProductDetails
{
    public partial class FailedBackGroundCheckForm : Form
    {

        #region Private Event Handlers
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReferenceNumber.Text))
            {
                
                FirearmBuyoutForm firearmBuyoutForm = new FirearmBuyoutForm(SelectedLoan, txtReferenceNumber.Text);
                firearmBuyoutForm.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("You must provide a Reference Number.");
            }
        }
        #endregion

        #region Public Properties
        public List<PawnLoan> SelectedLoan { get; set; }
        #endregion

        #region Constructors
        public FailedBackGroundCheckForm(List<PawnLoan> selectedLoan)
        {
            InitializeComponent();
            SelectedLoan = selectedLoan;
        }
        #endregion
    }
}
