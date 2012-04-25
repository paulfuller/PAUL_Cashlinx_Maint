using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System;
using System.Linq;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.UserControls.LoanDetailPanels
{
    public partial class LoanRenewPanel : ProductHistoryPanels.LoanDetailBasePanel
    {

        public LoanRenewPanel(PawnLoan pawnLoan, int receiptIdx)
            : base(pawnLoan, receiptIdx)
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            this.PH_RenewDTText.Text = Utilities.GetStringValue(_pawnLoan.Receipts[_receiptIdx].AuxillaryDate, "");
            this.PH_TerminalIDShopText.Text = _pawnLoan.OrgShopNumber;
            string loanStatus = (from l in GlobalDataAccessor.Instance.DesktopSession.LoanStatus
                                 where l.Left == _pawnLoan.LoanStatus
                                 select l.Right).First<string>();
            this.PH_CurrentLoanStatusText.Text = loanStatus;
            this.PH_PickupByPledgorText.Text = _pawnLoan.CustomerNumber == _pawnLoan.PuCustNumber ? "Yes" : "No";
            this.PH_MadeByEmployeeText.Text = _pawnLoan.EntityId;
            this.PH_ExtAmtPaidToDTText.Text = String.Format("{0:C}", _pawnLoan.ExtensionAmount);

            if (_pawnLoan.Receipts[_receiptIdx].Event ==
                ReceiptEventTypes.Paydown.ToString())
            {
                this.PH_PickupByPledgorLabel.Text = "Paydown By Pledgor?:";
            }

        }
    }
}
