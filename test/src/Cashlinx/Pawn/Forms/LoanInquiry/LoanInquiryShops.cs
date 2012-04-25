/**************************************************************************************************************
* CashlinxDesktop
* LoanInquiryLocations
* This form is used to show the available locations for a Loan inquiry
* S.Murphy 2/14/2010 Initial version
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Utility.Shared;
using CashlinxDesktop.UserControls;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.LoanInquiry
{
    public partial class LoanInquiryShops : Form
    {
        public NavBox NavControlBox;
        Form _ownerfrm;
        public static List<Shops> Shops = new List<Shops>();
        public static List<ShopsTreenode> LocationsTreeview = new List<ShopsTreenode>();

        public LoanInquiryShops()
        {
            NavControlBox = new NavBox();
            InitializeComponent();
            this.customButtonClear.Enabled = false;
            this.customButtonContinue.Enabled = false;
        }
        //show multiple stores
        private void LoanInquiryMultStore_Load(object sender, EventArgs e)
        {
            _ownerfrm = Owner;
            NavControlBox.Owner = this;

            ShopsTreenode locTreenode = new ShopsTreenode();
            //load store info & show it
            locTreenode.LoadTreeview();
            locTreenode.ShowVisible(this.panelLocations);

            this.PerformLayout();
        }
        //clear all checked checkboxes
        private void customButtonClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear all selections?", "Clear Location Selections", MessageBoxButtons.YesNo).Equals(DialogResult.Yes))
            {
                ShopsTreenode locTreenode = new ShopsTreenode();
                locTreenode.ClearSelection();
                locTreenode.ShowVisible(this.panelLocations);

                this.customButtonClear.Enabled = false;
                this.customButtonContinue.Enabled = false;

                this.PerformLayout();
            }
        }
        //load up selected locations and return to LoanInquiry
        private void customButtonContinue_Click(object sender, EventArgs e)
        {
            LoanInquiry loanInquiry = (LoanInquiry) this._ownerfrm;
            List<Shops> selected = new List<Shops>();

            loanInquiry.selectedShops.Clear();

            foreach (ShopsTreenode node in LocationsTreeview)
            {
                if (node.Level.Equals(ShopsTreenode.NodeLevel.Shop) && node.NodeCheckBox.Checked)
                {
                    Shops location = new Shops();
                    location.Market = LocationsTreeview[node.NodeMarket].NodeValue;
                    location.Region = LocationsTreeview[node.NodeRegion].NodeValue;
                    location.Shop = node.NodeValue;

                    loanInquiry.selectedShops.Add(location);
                }
            }

            loanInquiry.Size = new System.Drawing.Size(961, 646);
            loanInquiry.Location = new System.Drawing.Point(159, 57);

            this.Close();
        }
        //back to main menu via parent form
        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            LoanInquiry loanInquiry = (LoanInquiry)this._ownerfrm;
            loanInquiry.NavControlBox.Action = NavBox.NavAction.CANCEL;
        }
    }
}
