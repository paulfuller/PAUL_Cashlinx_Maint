using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Objects.Business;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Services.PFI
{
    public partial class PFI_AssignmentTypeRefurb : Form
    {
        public Item Item { get;set; }
        public List<int> CurrentRefurbNumbers;

        public PFI_AssignmentTypeRefurb()
        {
            InitializeComponent();
            
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            string sErrorCode = "0";
            var sErrorText = string.Empty;

            if (refurbNumber.Text == "")
            {
                MessageBox.Show("A Refurb Number is required to be entered before continuing.", "Refurb Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int iRefurbNumber = Convert.ToInt32(refurbNumber.Text);

            //TODO: The -60 days should be a rule!!!
            StoreLoans.CheckStoreRefurbNumber(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,                 
                                              ShopDateTime.Instance.ShopDate.AddDays(-60),
                                              iRefurbNumber,
                                              out sErrorCode,
                                              out sErrorText);
            
            if (sErrorCode != "0")
            {
                refurbNumber.Text = "";
                refurbNumber.Focus();
                inputLabel.Text = "The number entered is already in use.\n  Please enter another number.";
            }
            else
            {
                bool exists = false;
                if (CurrentRefurbNumbers != null)
                {
                    if (CurrentRefurbNumbers.Any(i => i == iRefurbNumber))
                    {
                        inputLabel.Text = "The number entered is already in use.\n  Please enter another number.";
                        exists = true;
                    }
                }
                if (!exists)
                {
                    Item.RefurbNumber = iRefurbNumber;
                    MessageBox.Show("Refurb Number available and updated to Pawn Item.", "Refurb Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }
    }
}
