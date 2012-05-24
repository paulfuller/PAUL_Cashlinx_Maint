/********************************************************************
* CashlinxDesktop
* PickupAmountDetails
* This form shows the pickup amount details for a loan 
* Sreelatha Rengarajan 9/9/2009 Initial version
 * SR 4/22/2010 Fixed PWNU00000669 wherein System was not displaying 
 * Lost Ticket Amount when a rolled over loan was set for pickup
*******************************************************************/

using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility.Shared;
using Support.Logic.DesktopProcedures;

namespace Support.Forms.Customer.Services.Pickup
{
    public partial class PickupAmountDetails : Form
    {
        private readonly int _ticketNumber;

        public PickupAmountDetails(int ticketNumber)
        {
            InitializeComponent();
            if (!(ticketNumber.Equals(string.Empty)))
            {
                this._ticketNumber = ticketNumber;
                LoadData();
            }
            this.customButtonCancel.Text = "Close";
        }

        private void LoadData()
        {
            var totalPickupAmt = 0.0M;
            PawnLoan pawnLoan = null;

            //If the loan is in service loan get the data from there else
            //get from pawnloans list
            var iDx = -1;
            var pawnloanToView = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans;
            if (pawnloanToView.Count > 0)
            {
                iDx = pawnloanToView.FindIndex(pl => pl.TicketNumber == this._ticketNumber);
            }
            if (iDx < 0)
            {
                pawnloanToView = GlobalDataAccessor.Instance.DesktopSession.PawnLoans;

                iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans.FindIndex(pl => pl.TicketNumber == this._ticketNumber);
                if (iDx < 0)
                {
                    pawnloanToView = GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary;
                    iDx =
                        GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.FindIndex(
                            pl => pl.TicketNumber == this._ticketNumber);
                }
            }
            if (iDx >= 0)
            {
                pawnLoan = pawnloanToView[iDx];
            }

            if (pawnLoan != null)
            {
                var pickupCalculator = new PfiPickupCalculator(pawnLoan, GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId, ShopDateTime.Instance.FullShopDateTime);
                pickupCalculator.Calculate();
                totalPickupAmt += pickupCalculator.PickupAmount;

                this.labelLoanNumber.Text = this._ticketNumber.ToString();
                this.labelLoanAmount.Text = pawnLoan.Amount.ToString("c");
                this.labelCurrentPrincipal.Text = pawnLoan.CurrentPrincipalAmount.ToString("c");

                var currentRow = 0;
                CustomLabel feeName;
                CustomLabel feeValue;
                foreach (var feedata in pickupCalculator.ApplicableFees)
                {
                    feeName = new CustomLabel();
                    feeValue = new CustomLabel();
                    if (feedata.FeeType == FeeTypes.LATE && feedata.Value < 0)
                    {
                        feeName.Text = "Refund:";
                    }
                    else
                    {
                        feeName.Text = Commons.GetFeeName(feedata.FeeType) + ":";
                    }

                    feeName.AutoSize = true;
                    feeValue.AutoSize = true;
                    feeName.Anchor = AnchorStyles.Right;
                    feeValue.Anchor = AnchorStyles.Left;
                    feeValue.Text = string.Format("{0:C}", feedata.Value);

                    if (!string.IsNullOrEmpty(feeName.Text))
                    {
                        this.tableLayoutPanelFees.Controls.Add(feeName, 0, currentRow);
                        this.tableLayoutPanelFees.Controls.Add(feeValue, 1, currentRow);
                        currentRow++;
                    }
                }

                feeName = new CustomLabel();
                feeValue = new CustomLabel();
                feeName.AutoSize = true;
                feeValue.AutoSize = true;
                feeName.Anchor = AnchorStyles.Right;
                feeValue.Anchor = AnchorStyles.Left;
                feeName.Text = "Total Pickup Amount";
                feeValue.Text = string.Format("{0:C}", totalPickupAmt);
                this.tableLayoutPanelFees.Controls.Add(feeName, 0, currentRow);
                this.tableLayoutPanelFees.Controls.Add(feeValue, 1, currentRow);

            }
            else
            {
                //no loan data to show
                Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}