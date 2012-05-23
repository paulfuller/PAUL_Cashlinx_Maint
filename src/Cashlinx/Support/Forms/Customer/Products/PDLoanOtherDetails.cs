using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Utility.Shared;

namespace Support.Forms.Customer.Products
{
    public partial class PDLoanOtherDetails : Form
    {

        private Form ownerfrm;
        public NavBox NavControlBox;

        #region FORMSTARTUP
        /*__________________________________________________________________________________________*/
        public PDLoanOtherDetails()
        {
            this.NavControlBox = new NavBox();
            InitializeComponent();

            var otherDetails = Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan.GetPDLoanOtherDetails;
            var CustToEdit = Support.Logic.CashlinxPawnSupportSession.Instance.ActiveCustomer;
            this.CustomerLoanNumberAndName.Text = Support.Logic.CashlinxPawnSupportSession.Instance.ActivePDLoan.PDLLoanNumber + " " + CustToEdit.FirstName + " " + CustToEdit.MiddleInitial + " " + CustToEdit.LastName;
            this.ApprovalNo.Text = otherDetails.ApprovalNumber;
            this.VeritecTranNumber.Text = otherDetails.ThirdPartyLoanNumber;
            this.LoanAPR.Text = otherDetails.LoanApr;
            this.MultipleLoan.Text = otherDetails.MultipleLoan;
            this.ApprFC.Text = otherDetails.ApprovedFinanceChrgRate;
            this.RefundSCAmount.Text = otherDetails.RefunedServiceChrgAmt;
            this.CountFeesAssessed.Text = otherDetails.CourtCostAmt;
            this.RequestedAmount.Text = otherDetails.RequestedLoanAmt;
            //otherDetails.ApprovedLoanAmt ;
            this.CheckNumber.Text = otherDetails.CheckNumber;
            this.ActualFCRate.Text = otherDetails.ActualFinanceChrg;
            this.OrigFeesMaint.Text = otherDetails.OrigMastringFee;
            this.ActualFCRate.Text = otherDetails.ActualFinanceChrgRate;
            this.APR.Text = otherDetails.APR;
            this.EstRefinanceAmt.Text = otherDetails.EstRefinanceAmt;
            //otherDetails.StatusChgDate ;
            this.CollectionStatus.Text = otherDetails.CollectionStatusDesc;
            if (!checkNull(otherDetails.InsOnDate).Equals(string.Empty))
            {
                this.InsOn.Text = DateTime.Parse(otherDetails.InsOnDate).FormatDate();//otherDetails.InsOnDate;
            }
            this.RefisRemaining.Text = otherDetails.RefisAvailable;
            this.FrequencyPaid.Text = otherDetails.PaymentFrequency;
            this.AutoSuspendACH.Text = otherDetails.SuspendACH;
            this.ActualBrokerAmt.Text = otherDetails.ActualBrokerAmt;
            this.BrokerRate.Text = otherDetails.BrokerRate;

        }
        private string checkNull(string value)
        {
            if (string.IsNullOrEmpty(value))
                value = string.Empty;

            return value;
        }
        /*__________________________________________________________________________________________*/

        /*__________________________________________________________________________________________*/
        private void PDLoanOtherDetails_Load(object sender, EventArgs e)
        {
            ownerfrm = this.Owner;
            this.NavControlBox.Owner = this;

        }

        #endregion

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "ProductsAndServices";
            this.NavControlBox.Action = NavBox.NavAction.BACK;

        }
    }
}
