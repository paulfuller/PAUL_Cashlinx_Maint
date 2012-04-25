using System.Linq;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.UserControls.LoanDetailPanels
{
    public partial class LoanChargesPanel : ProductHistoryPanels.LoanDetailBasePanel
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
            this.PH_InterestChargeText.Text = String.Format("{0:C}", _pawnLoan.InterestAmount);
            this.PH_ServiceChargeText.Text = String.Format("{0:C}", _pawnLoan.ServiceCharge);
            
            //Other fees include everything that's not interest and late fees.
            var otherFees = from f in _pawnLoan.Fees
                            where f.FeeType != FeeTypes.LATE
                                && f.FeeType != FeeTypes.INTEREST
                            select f;

            this.PH_FeesText.Text = String.Format("{0:C}", (from f in otherFees
                                                                select f.Value).Sum());

            var lateFee = (from feeData in _pawnLoan.Fees
                           where feeData.FeeType == FeeTypes.LATE
                           select feeData).FirstOrDefault().Value;

            this.PH_LateChargeText.Text = String.Format("{0:C}", lateFee);
            
            //Set labels to values related to this event type.
            this.PH_Amount1Label.Text = "Extension Amount (til now):";
            if (_pawnLoan.Receipts[_receiptIdx].Event ==
                     ReceiptEventTypes.Renew.ToString())
            {
                this.PH_Amount2Label.Text = "Total renew Amount Paid:";
            }
            else if (_pawnLoan.Receipts[_receiptIdx].Event ==
                     ReceiptEventTypes.Paydown.ToString())
            {
                this.PH_Amount2Label.Text = "Total paydown Amount Paid:";
            }

            else
            {
                this.PH_Amount2Label.Text = "Total pickup Amount Paid:";
            }
            //this.PH_Amount1Text.Text = String.Format("{0:C}", (from e in extensions
            //                                                   select e.Amount).Sum());
            this.PH_Amount1Text.Text = String.Format("{0:C}", _pawnLoan.ExtensionAmount);
            this.PH_Amount2Text.Text = String.Format("{0:C}", _pawnLoan.Receipts[_receiptIdx].Amount);

        }
    }
}
