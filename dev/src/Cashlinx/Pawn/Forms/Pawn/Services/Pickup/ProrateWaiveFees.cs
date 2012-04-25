using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Pawn.Services.Pickup
{
    public partial class ProrateWaiveFees : Form
    {
        private readonly int _ticketNumber;
        public ProrateWaiveFees(int ticketNumber)
        {
            InitializeComponent();
            if (!(ticketNumber.Equals(string.Empty)))
            {
                _ticketNumber = ticketNumber;
                LoadData();
            }
            //To DO: remove it when this form will be used for all 
            //the states and we know how to find out if the 
            //prorate and waive for fees is available for that state
            buttonCancel.Text = "Close";
            
        }

        private void LoadData()
        {
            decimal totalPickupAmt = 0.0M;
            List<PawnLoan> pawnloanToView = GlobalDataAccessor.Instance.DesktopSession.PawnLoans;
            PawnLoan pawnLoan = (from pawnloan in pawnloanToView
                                  where pawnloan.TicketNumber == _ticketNumber
                                  select pawnloan).First();
            if (pawnLoan != null)
            {
                labelLoanNumber.Text = _ticketNumber.ToString();
                totalPickupAmt += pawnLoan.Amount;
                labelLoanAmount.Text = string.Format("{0:C}", pawnLoan.Amount);
                totalPickupAmt += pawnLoan.InterestAmount;
                labelInterest.Text = string.Format("{0:C}", pawnLoan.InterestAmount);
                var feeData = (from pawnloanFee in pawnLoan.Fees
                               where pawnloanFee.FeeType == FeeTypes.LATE
                               select pawnloanFee).First();
                labelLateFees.Text = string.Format("{0:C}", feeData.Value);
                totalPickupAmt += feeData.Value;
                
                if (pawnLoan.LostTicketInfo != null)
                {
                    decimal lostTicketFeeAmt = pawnLoan.LostTicketInfo.LostTicketFee;
                    labelLostTicketFeeAmount.Text = string.Format("{0:C}", lostTicketFeeAmt);
                    labelLostTktFeeHeading.Visible = true;
                    labelLostTicketFeeAmount.Visible = true;
                    totalPickupAmt += lostTicketFeeAmt;
                }
                else
                {
                    labelLostTktFeeHeading.Visible = false;
                    labelLostTicketFeeAmount.Visible = false;

                }
                labelPickupAmount.Text = string.Format("{0:C}", totalPickupAmt);
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

 
    }
}