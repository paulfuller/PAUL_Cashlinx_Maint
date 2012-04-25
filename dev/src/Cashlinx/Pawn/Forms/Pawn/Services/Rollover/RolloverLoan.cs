/************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Pawn.Services.Rollover
 * Class:           RolloverLoan
 * 
 * Description      PayDown Rollover Page
 * 
 * History
 * David D Wise, Initial Development
 * 
 * **********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Services.Rollover
{
    public partial class RolloverLoan : CustomBaseForm
    {
        private List<PawnLoan> pawnLoans;
        private PawnLoan pawnLoan;
        private PawnLoan newPawnLoan;
        private int currentLoanIndex;
        private SiteId currentStoreSiteId;
        private bool pawnLoanRenewalAllowed;
        private bool paydownServiceAllowed;
        private decimal customerPaydownAmount;
        private bool rolloverError;

        public bool RolloverSuccess { get; set; }
        public List<PawnLoan> SkippedPawnLoans { get; set; }
        public List<PawnLoan> RolloverPawnLoans { get; set; }
        public decimal MinLoanAmt { get; set; }
        public decimal MaxLoanAmt { get; set; }

        public RolloverLoan()
        {
            InitializeComponent();
            this.RolloverSuccess = false;
            this.SkippedPawnLoans = new List<PawnLoan>();
            this.RolloverPawnLoans = new List<PawnLoan>();
        }

        private void RolloverLoan_Load(object sender, EventArgs e)
        {
            try
            {
                this.pawnLoans = GlobalDataAccessor.Instance.DesktopSession.RolloverLoans;
                if (this.pawnLoans.Count < 1)
                {
                    MessageBox.Show(
                        "There are no loans available for Renewal/Paydown.",
                        "Rollover Loan Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    int pwnLoanCnt = this.pawnLoans.Count;
                    this.rolloverLoanPageIndexHeaderLabel.Visible = pwnLoanCnt > 1;
                    this.skipButton.Visible = pwnLoanCnt > 1;
                    this.submitButton.Text = pwnLoanCnt > 1 ? "Continue" : "Submit";
                    this.currentLoanIndex = 0;
                    this.BringToFront();
                    this.Setup();
                }
            }
            catch (Exception eX)
            {
                MessageBox.Show("Exception occurred: " + eX + ": " + eX.StackTrace);
                this.Close();
            }
        }

        private void Setup()
        {
            this.currentStoreSiteId = new SiteId
            {
                Alias = GlobalDataAccessor.Instance.CurrentSiteId.Alias,
                Company = GlobalDataAccessor.Instance.CurrentSiteId.Company,
                Date = ShopDateTime.Instance.ShopDate,
                LoanAmount = 0,
                State = GlobalDataAccessor.Instance.CurrentSiteId.State,
                StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                TerminalId = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId
            };

            this.pawnLoanRenewalAllowed =
                new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsRenewalAllowed(this.currentStoreSiteId);
            this.paydownServiceAllowed =
                new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPayDownAllowed(this.currentStoreSiteId);

            this.ButtonVisible(false);
            this.DisplayCurrentLoanInfo();
            this.customerPaydownAmount = 0.0M;
            this.updateAllLoanAmounts();
        }

        private void ButtonVisible(bool bIsVisible)
        {
            this.amountFinanced_NewLoan.Visible = bIsVisible;
            this.interest_NewLoan.Visible = bIsVisible;
            this.service_NewLoan.Visible = bIsVisible;
            this.fee_NewLoan.Visible = bIsVisible;
            this.pickupAmount_NewLoan.Visible = bIsVisible;
            this.apr_NewLoan.Visible = bIsVisible;
            this.dueDate_NewLoan.Visible = bIsVisible;
            this.lastDatePickup_NewLoan.Visible = bIsVisible;
        }

        private void updateAllLoanAmounts()
        {
            //Compute new loan amounts
            this.CalculateRolloverAmount();
        }

        private void DisplayCurrentLoanInfo(/*int iLoanIndex*/)
        {
            this.submitButton.Enabled = false;
            this.calculateButton.Enabled = false;

            this.renewalAmountLabel.Visible = false;
            this.renewalAmountValue.Visible = false;
            this.radRenew.Enabled = false;

            this.totalReceivedFromCustomerTextBox.Enabled = false;
            this.totalReceivedFromCustomerTextBox.Text = "";
            this.totalReceivedFromCustomerLabel.Visible = false;
            this.totalReceivedFromCustomerTextBox.Visible = false;
            this.radPaydown.Enabled = false;
            this.pawnLoan = this.pawnLoans[this.currentLoanIndex];

            this.rolloverLoanHeaderLabel.Text = "Rollover Pawn Loan " +
                this.pawnLoan.TicketNumber;

            //Calculate current loan fee amount
            decimal currentLoanFee = 0.0M;
            currentLoanFee = (from ploan in pawnLoan.Fees
                              where ploan.FeeType != FeeTypes.INTEREST
                              select ploan).Sum(s => s.Value);


            //Update UI
            this.amountFinanced_CurrentLoan.Text = String.Format("{0:C}", this.pawnLoan.Amount);
            this.interest_CurrentLoan.Text = String.Format("{0:C}", this.pawnLoan.InterestAmount);
            this.service_CurrentLoan.Text = String.Format("{0:C}", this.pawnLoan.ServiceCharge);
            this.fee_CurrentLoan.Text = String.Format("{0:C}", currentLoanFee);
            this.pickupAmount_CurrentLoan.Text = String.Format("{0:C}", this.pawnLoan.PickupAmount);
            this.apr_CurrentLoan.Text = CashlinxDesktopSession.Instance.CurrentSiteId.Alias=="OK" ? String.Format("{0:N2}%", this.pawnLoan.InterestRate) : String.Format("{0}%", this.pawnLoan.InterestRate);
            this.dueDate_CurrentLoan.Text = this.pawnLoan.DueDate.ToShortDateString();
            this.lastDatePickup_CurrentLoan.Text = this.pawnLoan.PfiEligible.ToShortDateString();

            this.radPaydown.Enabled = paydownServiceAllowed;
            this.calculateButton.Enabled = paydownServiceAllowed;

            this.totalReceivedFromCustomerLabel.Visible = paydownServiceAllowed;
            this.totalReceivedFromCustomerTextBox.Visible = paydownServiceAllowed;

            this.radRenew.Enabled = pawnLoanRenewalAllowed;
            this.renewalAmountLabel.Visible = pawnLoanRenewalAllowed;
            this.renewalAmountValue.Visible = pawnLoanRenewalAllowed;
            pawnLoan.RenewalAmount = pawnLoan.PickupAmount - pawnLoan.Amount;
            this.renewalAmountValue.Text = String.Format("{0:C}", this.pawnLoan.RenewalAmount);

            /*            if (this.pawnLoan.RenewalAmount > 0.00M)
                        {
                            this.newPawnLoanAmount = this.pawnLoan.RenewalAmount;
                        }*/

            if (this.rolloverLoanPageIndexHeaderLabel.Visible)
            {
                this.rolloverLoanPageIndexHeaderLabel.Text = "Loan " + (this.currentLoanIndex + 1) + " of " +
                                                        this.pawnLoans.Count;
            }
        }

        private void CalculateRolloverAmount()
        {
            SiteId siteId = Utilities.CloneObject(GlobalDataAccessor.Instance.CurrentSiteId);
            UnderwritePawnLoanVO underwritePawnLoanVO;

            decimal totalFinCharge=0.0M;
            decimal totSrvCharge=0.0M;
            decimal newLoanFee = 0.0M;
            const decimal dLateFee = 0.00M;
            decimal apr = 0.0M;
            DateTime dueDate=DateTime.MaxValue;
            DateTime pfiDate=DateTime.MaxValue;

            //If renewal is the action
            if (radRenew.Checked)
            {
 
                //Compute renewal amount 
                decimal renAmt = this.pawnLoanRenewalAllowed ?
                                            this.pawnLoan.PickupAmount - this.pawnLoan.Amount
                                            : 0.0M;


                decimal renewedLoanAmt = pawnLoan.PickupAmount - renAmt;
                siteId.Date = ShopDateTime.Instance.ShopDate;

                siteId.LoanAmount = renewedLoanAmt;

                // Call Business Rules for Fees
                PawnLoan newloan = Utilities.CloneObject(this.pawnLoan);
                newloan.Amount = renewedLoanAmt;
               

                this.newPawnLoan = ServiceLoanProcedures.GetLoanFees(
                                   siteId,
                                   ServiceTypes.RENEW,
                                   0,0,0,0,
                                   newloan,
                                   out underwritePawnLoanVO);
                this.newPawnLoan.OriginalFees = Utilities.CloneObject(newloan.Fees);
                this.newPawnLoan.ObjectUnderwritePawnLoanVO = underwritePawnLoanVO;

                this.newPawnLoan.RenewalAmount = renAmt;
                this.newPawnLoan.Amount = renewedLoanAmt;

                //Calculate the fees for the new loan which should
                //not include the interest or gun lock fee
                //TO do: Figure out how to determine which fees need
                //to be used for renew from the database and use that

                newLoanFee = (from ploan in newPawnLoan.Fees
                              where ploan.FeeType != FeeTypes.INTEREST &&
                              ploan.FeeType != FeeTypes.GUN_LOCK
                              select ploan).Sum(s => s.Value);

                totalFinCharge = underwritePawnLoanVO.totalFinanceCharge;
                totSrvCharge = underwritePawnLoanVO.totalServiceCharge;
                dueDate = underwritePawnLoanVO.DueDate;
                pfiDate = underwritePawnLoanVO.PFIDate;
                apr = underwritePawnLoanVO.APR;

                this.newPawnLoan.PickupAmount = this.newPawnLoan.Amount 
                                       + underwritePawnLoanVO.totalFinanceCharge
                                       + newLoanFee;
                this.newPawnLoan.DueDate = underwritePawnLoanVO.DueDate;
                this.newPawnLoan.PfiNote = underwritePawnLoanVO.PFINotifyDate;
                this.newPawnLoan.PfiEligible = underwritePawnLoanVO.PFIDate;
                this.newPawnLoan.DateMade = underwritePawnLoanVO.MadeDate;
                this.newPawnLoan.InterestAmount = Math.Round(totalFinCharge,2);
                this.newPawnLoan.InterestRate = Math.Round(apr,2);

                //Enable submit button
                this.submitButton.Enabled = true;
                this.calculateButton.Enabled = false;
            }
            else if (radPaydown.Checked)
            {
                this.submitButton.Enabled = false;
                this.calculateButton.Enabled = true;
                siteId.Date = ShopDateTime.Instance.ShopDate;
                decimal paydownLoanAmount = this.pawnLoan.PickupAmount - this.customerPaydownAmount;
                
                siteId.LoanAmount = paydownLoanAmount;
                PawnLoan newloan = Utilities.CloneObject(this.pawnLoan);
                newloan.Amount = paydownLoanAmount;

                this.newPawnLoan = ServiceLoanProcedures.GetLoanFees(
                                   siteId,
                                   ServiceTypes.PAYDOWN,
                                   0,0,0,0,
                                   newloan,
                                   out underwritePawnLoanVO);

                this.newPawnLoan.OriginalFees = Utilities.CloneObject(this.pawnLoan.Fees);
                this.newPawnLoan.ObjectUnderwritePawnLoanVO = underwritePawnLoanVO;

                //Calculate the fees for the new loan which should
                //not include the interest
                //TO do: Figure out how to determine which fees need
                //to be used for paydown from the database and use that                
                newLoanFee = (from ploan in newPawnLoan.Fees
                              where ploan.FeeType != FeeTypes.INTEREST
                              select ploan).Sum(s => s.Value);

                this.newPawnLoan.Amount = paydownLoanAmount;
                //Verify new loan renewal amount against min state loan amount
                if (this.newPawnLoan.Amount < MinLoanAmt)
                {
                    MessageBox.Show(
                                    "The new loan amount for this transaction is less than the minimum amount required for a new loan. " +
                                    "Please adjust the terms of the paydown to continue.",
                                    "Paydown Loan Validation",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    rolloverError = true;
                    return;
                }
                //Compute pickup amount, required for determining rollover amounts
                totalFinCharge = underwritePawnLoanVO.totalFinanceCharge;
                totSrvCharge = underwritePawnLoanVO.totalServiceCharge;
                dueDate = underwritePawnLoanVO.DueDate;
                pfiDate = underwritePawnLoanVO.PFIDate;
                apr = underwritePawnLoanVO.APR;

                this.newPawnLoan.PaydownAmount = this.customerPaydownAmount;
                this.newPawnLoan.PickupAmount = this.newPawnLoan.Amount
                                            + underwritePawnLoanVO.totalFinanceCharge
                                            + newLoanFee;

                this.newPawnLoan.DueDate = underwritePawnLoanVO.DueDate;
                this.newPawnLoan.PfiNote = underwritePawnLoanVO.PFINotifyDate;
                this.newPawnLoan.PfiEligible = underwritePawnLoanVO.PFIDate;
                this.newPawnLoan.DateMade = underwritePawnLoanVO.MadeDate;
                this.newPawnLoan.InterestAmount = Math.Round(totalFinCharge,2);
                this.newPawnLoan.InterestRate = Math.Round(apr,2);
               
                this.submitButton.Enabled = true;
            }
            //Set previous ticket number attribute
            this.newPawnLoan.PrevTicketNumber = this.pawnLoan.TicketNumber;
            this.newPawnLoan.OrigTicketNumber = this.pawnLoan.OrigTicketNumber;
 
            //****End Section
 


            // populate form fields
            this.ButtonVisible(true);

            this.amountFinanced_NewLoan.Text = String.Format("{0:C}", this.newPawnLoan.Amount);
            this.interest_NewLoan.Text = String.Format("{0:C}", totalFinCharge);
            this.service_NewLoan.Text = String.Format("{0:C}", totSrvCharge);
            this.fee_NewLoan.Text = String.Format("{0:C}", newLoanFee);
            this.pickupAmount_NewLoan.Text = String.Format("{0:C}", this.newPawnLoan.PickupAmount);
            this.apr_NewLoan.Text = CashlinxDesktopSession.Instance.CurrentSiteId.Alias == "OK" ? String.Format("{0:N2}%", apr) : String.Format("{0}%", apr);
            this.dueDate_NewLoan.Text = dueDate.ToShortDateString();
            this.lastDatePickup_NewLoan.Text = pfiDate.ToShortDateString();
            this.renewalAmountValue.Text = String.Format("{0:C}", this.newPawnLoan.RenewalAmount);
            this.Update();
        }

        private void totalReceivedFromCustomerTextBox_TextChanged(object sender, EventArgs e)
        {
            this.submitButton.Enabled = false;
        }

        private void totalReceivedFromCustomerTextBox_Enter(object sender, EventArgs e)
        {
            //this.radPaydown.Checked = true;
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            //this.radPaydown.Checked = true;
            if (!StringUtilities.IsDecimal(totalReceivedFromCustomerTextBox.Text))
            {
                MessageBox.Show(
                    "Amount entered is not valid.",
                    "Paydown Amount Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                this.totalReceivedFromCustomerTextBox.Text = "";
                this.totalReceivedFromCustomerTextBox.Focus();
                return;
            }

            this.customerPaydownAmount = Utilities.GetDecimalValue(totalReceivedFromCustomerTextBox.Text);

            //SR 01/04/10 The following is not a valid check
            /*
            if (this.customerPaydownAmount >= this.pawnLoan.Amount)
            {
                MessageBox.Show(
                    "Amount entered is required to be smaller than Original Loan Amount.",
                    "Paydown Amount Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                this.customerPaydownAmount = 0;
                this.totalReceivedFromCustomerTextBox.Text = "";
                this.totalReceivedFromCustomerTextBox.Focus();
                return;
            }*/

            //Check if the paydown amount is less than min
            decimal minPayDownAmt=((this.pawnLoan.PickupAmount - this.pawnLoan.Amount) + 1);
            //Check if paydown amt is more than max
            decimal maxPaydownAmt = (this.pawnLoan.PickupAmount - MinLoanAmt);
            //SR 03/02/2010 IF the minimum pay down amt exceeds the max pay down amt
            //set the minimum to be the same as maximum.
            if (minPayDownAmt > maxPaydownAmt)
                minPayDownAmt = maxPaydownAmt;
            if (Utilities.GetDecimalValue(totalReceivedFromCustomerTextBox.Text,0) < minPayDownAmt)
            {
                MessageBox.Show(
     "Minimum allowed amount is " + string.Format("{0:C}",minPayDownAmt),
     "Paydown Amount Validation",
     MessageBoxButtons.OK,
     MessageBoxIcon.Warning);
                this.customerPaydownAmount = 0;
                this.totalReceivedFromCustomerTextBox.Text = "";
                this.totalReceivedFromCustomerTextBox.Focus();
                return;

            }

            if (Utilities.GetDecimalValue(totalReceivedFromCustomerTextBox.Text, 0) > maxPaydownAmt)
                {
                    MessageBox.Show(
                        "Maximum allowed amount is " + string.Format("{0:C}",maxPaydownAmt),
                        "Paydown Amount Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    this.customerPaydownAmount = 0;
                    this.totalReceivedFromCustomerTextBox.Text = "";
                    this.totalReceivedFromCustomerTextBox.Focus();
                    return;

                
                }
                this.updateAllLoanAmounts();
            
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.RolloverSuccess = false;
            this.Close();
        }

        private void radioCheckedChanged(object sender, EventArgs e)
        {
            this.ButtonVisible(false);
            this.submitButton.Enabled = false;

            if (((RadioButton)sender).Checked && ((RadioButton)sender).Name == "radPaydown")
            {
                this.calculateButton.Enabled = true;
                this.totalReceivedFromCustomerTextBox.Enabled = true;
                this.totalReceivedFromCustomerTextBox.Focus();
            }
            else
            {
                this.calculateButton.Enabled = false;
                this.totalReceivedFromCustomerTextBox.Enabled = false;
                this.totalReceivedFromCustomerTextBox.Text = "";
                this.customerPaydownAmount = 0.00M;
                if (this.newPawnLoan != null && this.newPawnLoan.Amount > 0)
                {
                    this.ButtonVisible(true);
                    this.submitButton.Enabled = true;
                    this.updateAllLoanAmounts();
                }
            }
        }

        private void feeLabel_Click(object sender, EventArgs e)
        {
            var loanFeesForm = new LoanFees(this.pawnLoan, this.newPawnLoan);
            loanFeesForm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitButton_Click(object sender, EventArgs e)
        {
            StateStatus stateStatus = StateStatus.BLNK;
            if (this.radRenew.Checked)
            {
                stateStatus = StateStatus.RN;
            }
            else if (radPaydown.Checked)
            {
                stateStatus = StateStatus.PD;
            }

            this.newPawnLoan.TempStatus = stateStatus;

            // Make Update to current PawnLoan from NewPawnLoan
            this.RolloverPawnLoans.Add(this.newPawnLoan);
            //this.pawnLoans[this.currentLoanIndex] = this.newPawnLoan;

            if (this.currentLoanIndex + 1 == this.pawnLoans.Count)
            {
                this.RolloverSuccess = true;
                this.Close();
            }
            else
            {
                this.currentLoanIndex++;
                this.radRenew.Checked = true;
                this.updateButtonsAndLoanInfo();
                if (rolloverError)
                    return;
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skipButton_Click(object sender, EventArgs e)
        {
            this.SkippedPawnLoans.Add(this.pawnLoans[this.currentLoanIndex]);

            this.currentLoanIndex++;
            if (this.currentLoanIndex >= this.pawnLoans.Count)
            {
                //If all the loans were skipped, rollover did not happen on any of them
                if (SkippedPawnLoans.Count == this.pawnLoans.Count)
                    this.RolloverSuccess = false;
                else
                    this.RolloverSuccess = true;
                
                this.Close();
            }
            else
            this.updateButtonsAndLoanInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        private void updateButtonsAndLoanInfo()
        {
            //this.skipButton.Visible = (this.currentLoanIndex + 1 == this.pawnLoans.Count);
            this.skipButton.Visible = this.pawnLoans.Count > 1;
            this.submitButton.Text = (this.currentLoanIndex + 1 == this.pawnLoans.Count) ?
                "Submit" : submitButton.Text;

            this.DisplayCurrentLoanInfo();
            this.updateAllLoanAmounts();
        }
    }
}
