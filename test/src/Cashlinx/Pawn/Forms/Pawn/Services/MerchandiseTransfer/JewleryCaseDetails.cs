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
    public partial class JewleryCaseDetails : Form
    {

        public JewleryCaseDetails()
        {
            InitializeComponent();

            Setup();
        }

        private void Setup()
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

    }
}
