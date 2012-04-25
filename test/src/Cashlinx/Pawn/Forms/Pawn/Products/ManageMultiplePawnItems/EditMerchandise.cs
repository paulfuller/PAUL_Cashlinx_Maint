using System;
using System.Windows.Forms;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Logger;

namespace Pawn.Forms.Pawn.Products.ManageMultiplePawnItems
{
    public partial class EditMerchandise : Form
    {
        private Item pawnItem;
        private const string EMPTY = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pawnItem"></param>
        public EditMerchandise(ref Item pawnItem)
        {
            InitializeComponent();

            if (pawnItem != null)
            {
                this.pawnItem = pawnItem;
            }
            else
            {
                MessageBox.Show("Error with Pawn Item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditMerchandise_Load(object sender, EventArgs e)
        {
            /*this.manufacturerTextBox.Text = this.pawnItem.Manufacturer;
  //          this.modelTextBox.Text = this.pawnItem.Model;
  //          this.loanRangeTextBox.Text = this.pawnItem.LoanRange;
   //         this.retailAmountTextBox.Text = this.pawnItem.SuggestedRetailAmount;
            this.loanAmountTextBox.Text = this.pawnItem.LoanAmount.ToString();*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doneButton_Click(object sender, EventArgs e)
        {
            if (this.errorLabel.Visible)
            {
                this.errorLabel.Visible = false;
            }

            try
            {
                double newLoanAmount = 0.0d;
                if (this.loanAmountTextBox.Text != EMPTY)
                {
                    if (double.TryParse(this.loanAmountTextBox.Text, out newLoanAmount))
                    {
                        this.pawnItem.ItemAmount = Convert.ToDecimal(newLoanAmount);
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        this.errorLabel.Visible = true;
                    }
                }
                else
                {
                    this.errorLabel.Visible = true;
                }
            }
            catch (Exception exc)
            {
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Exception thrown when done button clicked: " + exc.Message);
                this.errorLabel.Visible = true;
            }
                
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
