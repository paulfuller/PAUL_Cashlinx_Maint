using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
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

            //Only show current principal amount if partial payments are allowed.
            if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(GlobalDataAccessor.Instance.CurrentSiteId))
            {
                this.PH_CurrentPrincipalAmountText.Text = String.Format("{0:C}", _pawnLoan.CurrentPrincipalAmount);
            }
            else
            {
                this.PH_CurrentPrincipalAmountText.Visible = false;
                this.PH_CurrentPrincipalAmountLabel.Visible = false;
                //Move the principal amount down for cosmetic purposes if we're not displaying current principal.
                this.PH_PrincipalAmountLabel.Top = PH_CurrentPrincipalAmountLabel.Top;
                this.PH_PrincipalAmountText.Top = PH_CurrentPrincipalAmountText.Top;
            }
        }
    }
}
