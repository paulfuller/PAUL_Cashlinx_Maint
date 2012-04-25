/************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Pawn.Services.Rollover
 * Class:           ViewNewLoanDetails
 * 
 * Description      View ReadOnly New Pawn Loan Details
 * 
 * History
 * David D Wise, Initial Development
 * 
 * **********************************************************************/

using System;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Support.Forms.Customer.Services.Rollover
{
    public partial class ViewNewLoanDetails : CustomBaseForm
    {
        private PawnLoan pawnLoan;
        
        private readonly int ticketNumber;
        private decimal pawnLoanAmount;
        private PawnLoan newPawnLoan = new PawnLoan();

        public ViewNewLoanDetails(int iTicketNumber)
        {
            InitializeComponent();
            ticketNumber = iTicketNumber;

        }

        private void Setup()
        {
            int iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans.FindIndex(p => p.TicketNumber == ticketNumber);

            if (iDx < 0)
            {
                iDx = GlobalDataAccessor.Instance.DesktopSession.PawnLoans_Auxillary.FindIndex(p => p.TicketNumber == ticketNumber);
                if (iDx < 0)
                {
                    MessageBox.Show(
                        "Ticket Number is not associated to a Loan.",
                        "Ticket Number Lookup",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }
            }
            rolloverLoanHeaderLabel.Text = "Rollover Pawn Loan " + ticketNumber;
            pawnLoan = GlobalDataAccessor.Instance.DesktopSession.PawnLoans[iDx];

            pawnLoanAmount = pawnLoan.Amount;
            decimal currLoanFee = (from ploan in pawnLoan.Fees
                                                where ploan.FeeType != FeeTypes.INTEREST
                                                select ploan).Sum(s => s.Value);
            decimal intFee = (from ploan in pawnLoan.Fees
                             where ploan.FeeType == FeeTypes.INTEREST
                             select ploan).Sum(s=>s.Value);

            decimal srvFee = (from ploan in pawnLoan.Fees
                              where ploan.FeeType == FeeTypes.SERVICE
                              select ploan).Sum(s => s.Value);

            // populate form fields
            if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId))
            {
                currentPrincipal_CurrentLoan.Text = pawnLoan.CurrentPrincipalAmount.ToString("c");
            }
            else
            {
                currentPrincipal_CurrentLoan.Visible = false;
                currentPrincipalLabel.Visible = false;
            }

            amountFinanced_CurrentLoan.Text = String.Format("{0:C}", pawnLoanAmount);
            interest_CurrentLoan.Text = String.Format("{0:C}", intFee);
            service_CurrentLoan.Text = String.Format("{0:C}", srvFee);
            fee_CurrentLoan.Text = String.Format("{0:C}", currLoanFee);
            pickupAmount_CurrentLoan.Text = String.Format("{0:C}", pawnLoan.PickupAmount);
            apr_CurrentLoan.Text = String.Format("{0}%", pawnLoan.InterestRate);
            dueDate_CurrentLoan.Text = pawnLoan.DueDate.ToShortDateString();
            lastDatePickup_CurrentLoan.Text = pawnLoan.PfiEligible.ToShortDateString();

            CalculateNewLoan();
        }

        private void ViewNewLoanDetails_Load(object sender, EventArgs e)
        {
            Setup();
        }

        private void CalculateNewLoan()
        {
            
            int Idx = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans.FindIndex(pl => pl.TicketNumber == ticketNumber);
            if (Idx >= 0)
                newPawnLoan = GlobalDataAccessor.Instance.DesktopSession.ServiceLoans[Idx];
            else
                newPawnLoan = Utilities.CloneObject(pawnLoan);
            decimal newLoanFee = (from ploan in newPawnLoan.Fees
                          where ploan.FeeType != FeeTypes.INTEREST 
                          select ploan).Sum(s => s.Value);

            decimal intFee = (from ploan in newPawnLoan.Fees
                              where ploan.FeeType == FeeTypes.INTEREST
                              select ploan).Sum(s => s.Value);

            decimal srvFee = (from ploan in newPawnLoan.Fees
                              where ploan.FeeType == FeeTypes.SERVICE
                              select ploan).Sum(s => s.Value);

            
            decimal dPickupAmount = newPawnLoan.PickupAmount;

            // populate form fields
            if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId))
            {
                currentPrincipal_NewLoan.Text = newPawnLoan.CurrentPrincipalAmount.ToString("c");
            }
            else
            {
                currentPrincipal_NewLoan.Visible = false;
                currentPrincipalLabel.Visible = false;
            }

            amountFinanced_NewLoan.Text = String.Format("{0:C}", newPawnLoan.Amount);
            interest_NewLoan.Text = String.Format("{0:C}", intFee);
            service_NewLoan.Text = String.Format("{0:C}", srvFee);
            fee_NewLoan.Text = String.Format("{0:C}", newLoanFee);
            pickupAmount_NewLoan.Text = String.Format("{0:C}", dPickupAmount);
            apr_NewLoan.Text = String.Format("{0}%", Math.Round(newPawnLoan.InterestRate, 0));
            dueDate_NewLoan.Text = newPawnLoan.DueDate.ToShortDateString();
            lastDatePickup_NewLoan.Text = newPawnLoan.PfiEligible.ToShortDateString();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void feeLabel_Click(object sender, EventArgs e)
        {
            var myForm = new LoanFees(pawnLoan, newPawnLoan);
            myForm.ShowDialog();
        }
    }
}
