using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.UserControls.LoanDetailPanels
{
    public partial class LoanPFIPanel : ProductHistoryPanels.LoanDetailBasePanel
    {

        //public LoanChargesPanel()
        //{
        //    InitializeComponent();
        //}

        public LoanPFIPanel(PawnLoan pawnLoan, int receiptIdx)
            : base(pawnLoan, receiptIdx)
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            decimal extFee = _pawnLoan.Fees.Find(f => f.FeeType == FeeTypes.AUTOEXTEND).Value;

            this.PH_PrincipalAmountText.Text = String.Format("{0:C}", _pawnLoan.Amount);           
            this.PH_ExtFeeText.Text = String.Format("{0:C}", extFee); 
            this.PH_PFIEligibilityDTText.Text = _pawnLoan.PfiEligible.ToShortDateString();
            this.PH_ExtPFIEligibilityDTText.Text = "";
            this.PH_PFINotificationDTText.Text = _pawnLoan.PfiNote.ToShortDateString();
            this.PH_ExtPFINotificationDTText.Text = "";
        }
    }
}
