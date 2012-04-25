using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System.Linq;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;

namespace Pawn.Forms.UserControls.LoanDetailPanels
{
    public partial class LoanPickupPanel
    : ProductHistoryPanels.LoanDetailBasePanel
    {
        public LoanPickupPanel(PawnLoan pawnLoan, int receiptIdx)
        : base(pawnLoan, receiptIdx)
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            if (!_pawnLoan.GunInvolved)
            {
                PH_ExtAmtPaidToDTText.Visible = false;
                PH_ExtAmtPaidToDTLabel.Visible = false;
            }

            this.PH_RenewDTText.Text = Utilities.GetStringValue(_pawnLoan.Receipts[_receiptIdx].RefTime , "");
            this.PH_TerminalIDShopText.Text = _pawnLoan.OrgShopNumber;
            string loanStatus = (from l in GlobalDataAccessor.Instance.DesktopSession.LoanStatus
                                 where l.Left == _pawnLoan.LoanStatus
                                 select l.Right).First<string>();
            this.PH_CurrentLoanStatusText.Text = loanStatus;
            this.PH_PickupByPledgorText.Text = _pawnLoan.CustomerNumber == _pawnLoan.PuCustNumber ? "Yes" : "No";
            this.PH_MadeByEmployeeText.Text = _pawnLoan.EntityId;
            this.PH_ExtAmtPaidToDTText.Text = "";
        }
    }
}
