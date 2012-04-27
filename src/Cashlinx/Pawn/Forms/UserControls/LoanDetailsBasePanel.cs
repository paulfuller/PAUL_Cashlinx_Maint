using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System.Windows.Forms;
using Common.Libraries.Objects.Pawn;

namespace Pawn.Forms.UserControls
{
    public partial class LoanDetailsBasePanel : UserControl
    {
        protected PawnLoan _pawnLoan;
        protected int _receiptIdx;
        //protected ReceiptEventTypes _eventType;

        public LoanDetailsBasePanel(PawnLoan pawnLoan, int receiptIdx): this(){
            _pawnLoan = pawnLoan;
            _receiptIdx = receiptIdx;
        }

        public LoanDetailsBasePanel()
        {
            InitializeComponent();
        }

        //public void InitLoan(PawnLoan pawnLoan)
        //{
            //_pawnLoan = pawnLoan;
            //_eventType =  _pawnLoan.Receipts[0].Event;
        //}

    }
}
