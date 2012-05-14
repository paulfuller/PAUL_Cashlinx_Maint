using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility.Shared;

namespace Support.Forms.Customer.Products.ProductHistory
{
    public partial class PartialPawnPaymentHistory : Form
    {
        #region Public Properties
        public PawnLoan Loan { get; set; }
        public PartialPawnPaymentHistoryFormMode Mode { get; set; }
        public PartialPayment Payment { get; set; }
        #endregion

        #region Constructors
        public PartialPawnPaymentHistory(PartialPawnPaymentHistoryFormMode mode, PartialPayment pPayment, PawnLoan loan)
        {
            InitializeComponent();
            Mode = mode;
            Payment = pPayment;
            Loan = loan;
            SetFormFields();
        }
        #endregion

        #region Private Methods
        private void SetFormFields()
        {
            if (Mode == PartialPawnPaymentHistoryFormMode.VoidPartialPawnPaymentHistory)
            {
                this.labelHeaderText.Text = "Void Partial Pawn Payment History";
            }
            this.labelShopNumber.Text = Payment.StoreNumber;
            this.labelUserID.Text = Payment.UpdatedBy;
            this.labelDate.Text = Payment.Date_Made.ToShortDateString();
            this.labelTotalPayment.Text = Payment.PMT_AMOUNT.ToString("C");
            this.labelInterest.Text = Payment.PMT_INT_AMT.ToString("C");
            this.labelLoanNumber.Text = Payment.LoanNumber.ToString();
            this.labelNewPrincipalAmount.Text = Payment.CUR_AMOUNT.ToString("C");
            this.labelServiceCharge.Text = Payment.PMT_SERV_AMT.ToString("C");

            this.labelPrincipalReduction.Text = Payment.PMT_PRIN_AMT.ToString("C");
            this.labelLateFee.Text = GetLateFee().ToString("c");
        }

        private decimal GetLateFee()
        {
            if (Loan.Fees == null || Loan.Fees.Count == 0)
            {
                return 0M;
            }

            var fee = Loan.Fees.Find(f => f.FeeType == FeeTypes.LATE && f.FeeRefType == FeeRefTypes.PARTP && f.FeeRef == Payment.PMT_ID);
            return fee.Value;
        }
        #endregion
    }
}