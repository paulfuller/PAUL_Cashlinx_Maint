using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Objects.Pawn;

namespace Pawn.Forms.UserControls
{
    public partial class LoanStatusInfoPanel : LoanDetailsBasePanel
    {
        //public LoanStatusInfoPanel()
        //{
        //    InitializeComponent();
        //}

        public LoanStatusInfoPanel(PawnLoan pawnLoan, int receiptIdx)
            : base(pawnLoan, receiptIdx)
        {
            InitializeComponent();
        }
    }
}
