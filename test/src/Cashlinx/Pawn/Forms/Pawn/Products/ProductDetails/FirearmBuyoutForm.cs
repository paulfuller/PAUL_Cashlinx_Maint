using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Pawn.Loan;
using Pawn.Forms.UserControls;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Products.ProductDetails
{
    public partial class FirearmBuyoutForm : Form
    {
        #region Private Properties
        private DesktopSession CDS
        {
            get { return GlobalDataAccessor.Instance.DesktopSession; }
        }
        #endregion

        #region Private Event Handlers
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            StringBuilder messageToShow = new StringBuilder();
            messageToShow.Append("A manager's authorization is required." + System.Environment.NewLine);
            //bring up dialog
            List<ManagerOverrideType> overrideTypes = new List<ManagerOverrideType>();
            List<ManagerOverrideTransactionType> transactionTypes = new List<ManagerOverrideTransactionType>();
            transactionTypes.Add(ManagerOverrideTransactionType.FIREARMSBUYOUT);
            overrideTypes.Add(ManagerOverrideType.FIREARMSBUYOUT);
            ManageOverrides overrideFrm = new ManageOverrides(ManageOverrides.OVERRIDE_TRIGGER)
            {
                MessageToShow = messageToShow.ToString(),
                ManagerOverrideTypes = overrideTypes,
                ManagerOverrideTransactionTypes = transactionTypes
            };
            overrideFrm.ShowDialog();
            if (overrideFrm.OverrideAllowed)
            {
               //add code here to set items to processtender
                GlobalDataAccessor.Instance.DesktopSession.BuyoutLoans = SelectedLoan;
            }
        }

        private void FirearmBuyoutForm_Load(object sender, EventArgs e)
        {

        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            LoadEditableMode();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            LoadReviewMode();
        }
        #endregion

        #region Private Helper Methods
        private void LoadEditableMode()
        {
            panelTotals.Visible = false;
            btnGoBack.Visible = false;
            btnContinue.Visible = true;
            btnSubmit.Visible = false;
            btnCancel.Visible = true;
            LoadUsercontrols(true);
        }

        private void LoadReviewMode()
        {
            panelTotals.Visible = true;
            btnGoBack.Visible = true;
            btnContinue.Visible = false;
            btnSubmit.Visible = true;
            btnCancel.Visible = false;
            LoadUsercontrols(false);
        }

        private void LoadUsercontrols(bool editable)
        {
            try
            {
                tableLayoutPanel1.Controls.Clear();
                tableLayoutPanel1.Height = 0;
                if (SelectedLoan.Count > 0)
                {
                    tableLayoutPanel1.Controls.Clear();
                    tableLayoutPanel1.Height = 0;
                    foreach (PawnLoan pl in SelectedLoan)
                    {
                        foreach (Item pItem in pl.Items)
                        {
                            if (pItem.IsGun)
                            {
                                FirearmBuyoutItem firearmBuyoutItem = new FirearmBuyoutItem(pItem);
                                firearmBuyoutItem.SetItem(editable);
                                tableLayoutPanel1.Controls.Add(firearmBuyoutItem);
                                if (!editable)
                                    SetTotals();
                            }
                        }

                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void SetTotals()
        {
            decimal firearmsItemsTotal = 0.0m;
            decimal pickupAmount = 0.0m;
            decimal payToCustomer = 0.0m;
            decimal otherPickupItemsTotal = 0.0m;
            foreach (PawnLoan pl in SelectedLoan)
            {
                pickupAmount += pl.PickupAmount;
                foreach (Item pItem in pl.Items)
                {
                    if (pItem.IsGun)
                        firearmsItemsTotal += pItem.ItemAmount;
                    else
                        otherPickupItemsTotal += pItem.ItemAmount;
                }
            }
            lblTotalBuyAmount.Text = Math.Round(firearmsItemsTotal, 2).ToString("C");
            lblTotalAmountDue.Text = Math.Round(pickupAmount, 2).ToString("C");
            payToCustomer = firearmsItemsTotal - pickupAmount;
            lblPayToCustomer.Text = Math.Round(payToCustomer, 2).ToString("C");
            GlobalDataAccessor.Instance.DesktopSession.TotalBuyoutAmount += firearmsItemsTotal;
            GlobalDataAccessor.Instance.DesktopSession.TotalOtherPickupItems += otherPickupItemsTotal;
            if (payToCustomer < 0)
            {
                //payToCustomer  = Math.Abs(payToCustomer);
                GlobalDataAccessor.Instance.DesktopSession.TotalPickupAmount = payToCustomer;
                lblDueFromCustomerDescription.Visible = true;
                lblPayToCustomerDescription.Visible = false;
            }
            else
            {
                GlobalDataAccessor.Instance.DesktopSession.TotalPickupAmount = payToCustomer;
                lblDueFromCustomerDescription.Visible = false;
                lblPayToCustomerDescription.Visible = true;
            }
        }
        #endregion

        #region Constructors
        public FirearmBuyoutForm(List<PawnLoan> selectedLoan, string referenceNumber)
        {
            InitializeComponent();
            SelectedLoan = selectedLoan;
            ReferenceNumber = referenceNumber;
            LoadEditableMode();
        }
        #endregion  

        #region Public Properties
        public string ReferenceNumber { get; set; }
        public List<PawnLoan> SelectedLoan { get; set; }
        #endregion
    }
}
