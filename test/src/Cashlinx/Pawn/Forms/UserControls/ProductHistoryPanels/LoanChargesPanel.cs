using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System;
using Common.Libraries.Objects.Pawn;

namespace Pawn.Forms.UserControls.ProductHistoryPanels
{
    public partial class LoanChargesPanel : LoanDetailBasePanel
    {

        //public LoanChargesPanel()
        //{
        //    InitializeComponent();
        //}

        public LoanChargesPanel(PawnLoan pawnLoan, int receiptIdx)
            : base(pawnLoan, receiptIdx)
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            this.PH_PrincipalAmountText.Text = String.Format("{0:C}", _pawnLoan.Amount);
            
        }
    }
}
