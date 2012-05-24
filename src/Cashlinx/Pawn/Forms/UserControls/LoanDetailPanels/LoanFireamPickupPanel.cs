using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;

namespace Pawn.Forms.UserControls.LoanDetailPanels
{
    public partial class LoanFireamPickupPanel : ProductHistoryPanels.LoanDetailBasePanel
    {

        public LoanFireamPickupPanel(PawnLoan pawnLoan, int receiptIdx)
            : base(pawnLoan, receiptIdx)
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            IdentificationVO activeCustomerIdentity = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.getFirstIdentity();

            this.PH_FirearmIDText.Text = activeCustomerIdentity.IdIssuerCode
                                        + " "
                                        + activeCustomerIdentity.DatedIdentDesc
                                        + " " + activeCustomerIdentity.IdValue + System.Environment.NewLine
                                        + " exp " + activeCustomerIdentity.IdExpiryData.ToShortDateString();
        }
    }
}
