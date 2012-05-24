using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;

namespace Pawn.Forms.UserControls.ProductHistoryPanels
{
    public partial class LoanBasicInfoPanel : LoanDetailBasePanel
    {

        public LoanBasicInfoPanel(PawnLoan pawnLoan, int receiptIdx)
            : base(pawnLoan, receiptIdx) 
        {
            InitializeComponent();

            Setup();
        }

        private void Setup()
        {
            this.PH_OriginationDTText.Text = Utilities.GetStringValue(_pawnLoan.OriginationDate, "");
            this.PH_DueDTText.Text = _pawnLoan.DueDate.ToShortDateString();
            this.PH_LoanAmountText.Text = String.Format("{0:C}", _pawnLoan.Amount);
            this.PH_OriginalLoanNumberText.Text = _pawnLoan.OrigTicketNumber.ToString();
            this.PH_PreviousLoanNumberText.Text = _pawnLoan.PrevTicketNumber.ToString();
            this.PH_CurrentLoanNumberText.Text = _pawnLoan.TicketNumber.ToString();
        }
    }
}
