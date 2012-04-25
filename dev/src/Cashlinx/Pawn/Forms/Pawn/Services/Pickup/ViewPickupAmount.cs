using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Objects.Pawn;

namespace Pawn.Forms.Pawn.Services.Pickup
{
    public partial class ViewPickupAmount : Form
    {
        private int _ticketNumber;
        


        public ViewPickupAmount(int ticketNumber)
        {
            InitializeComponent();
            if (!(ticketNumber.Equals(string.Empty)))
            {
                _ticketNumber = ticketNumber;
                LoadData();
            }

        }

        private void LoadData()
        {
            decimal totalPickupAmt = 0.0M;
            List<PawnLoan> pawnloanToView= GlobalDataAccessor.Instance.DesktopSession.PawnLoans;
            PawnLoan _pawnLoan = (from pawnloan in pawnloanToView
                                  where pawnloan.TicketNumber == _ticketNumber
                                  select pawnloan).First();
            if (_pawnLoan != null)
            {
                labelLoanNumber.Text = _ticketNumber.ToString();
                totalPickupAmt += _pawnLoan.Amount;
                labelLoanAmount.Text = string.Format("{0:C}", _pawnLoan.Amount);
                totalPickupAmt += _pawnLoan.InterestAmount;
                labelInterestAmount.Text = string.Format("{0:C}", _pawnLoan.InterestAmount);
                labelPickupAmount.Text = string.Format("{0:C}", totalPickupAmt);
            }

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}