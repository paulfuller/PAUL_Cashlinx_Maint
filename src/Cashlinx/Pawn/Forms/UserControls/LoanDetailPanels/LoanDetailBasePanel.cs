using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System.Windows.Forms;
using Common.Libraries.Objects.Pawn;

namespace Pawn.Forms.UserControls.LoanDetailPanels
{
    public partial class LoanDetailBasePanel : UserControl
    {
        protected PawnLoan _pawnLoan;
        protected int _receiptIdx;
        //protected ReceiptEventTypes _eventType;

        public LoanDetailBasePanel(PawnLoan pawnLoan, int receiptIdx): this(){
            _pawnLoan = pawnLoan;
            _receiptIdx = receiptIdx;
        }

        public LoanDetailBasePanel()
        {
            InitializeComponent();
        }


    }
}
