/**************************************************************************
 * PFI_Verify.cs 
 * 
 * History:
 *  no ticket SMurphy 5/6/2010 issues with PFI Merge, PFI Add and navigation
 * 
 **************************************************************************/

using System;
using System.Windows.Forms;

namespace Pawn.Forms.Pawn.Services.MerchandiseTransfer
{
    public partial class JewelryCaseDetails : Form
    {
        private string _caseNumber = "";
        private bool _shouldContinue = false;


        public JewelryCaseDetails()
        {
            InitializeComponent();

            Setup();
        }

        public string JewelryCaseNumber
        {
            get { return _caseNumber; }
        }
        public bool ShouldContinue
        {
            get { return _shouldContinue; }
        }


        private void Setup()
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();           
        }

        private void continueButton_Click(object sender, EventArgs e)
        {

            if (this.txtJewelryCaseNumber.Text.Trim() != "")
            {
                _caseNumber = this.txtJewelryCaseNumber.Text;
                _shouldContinue = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please makes sure the case number is filled out.");
            }

        }


    }
}
