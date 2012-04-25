/****************************************************************************
* Namespace:       CashlinxDesktop.DesktopForms.Pawn.Products.ProductHistory
* Class:           ProductHistory_Dialog
* 
* Description      Popup Form to show specific information of a Customer
*                  Pawn Loan.
* 
* History
* David D Wise, Initial Development
* 
* Fixes/Mods
*  PWNU00000220 S.Murphy populate PH_SuggestedAmountColumn and PH_LocationColumn
*****************************************************************************/
using System;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
//Odd file lock

namespace Pawn.Forms.Pawn.Products.ProductHistory
{
    public partial class ProductHistory_Dialog : Form
    {
        private PawnLoan _PawnLoan;

        public NavBox NavControlBox;

        public ProductHistory_Dialog(PawnLoan pawnLoan, int receiptIdx)
        {
            InitializeComponent();

            //Does not need this. -- Removed by TLR 6/14/2010
            //this.NavControlBox = new NavBox();
            //this.NavControlBox.Owner = this;

            _PawnLoan = pawnLoan;

            if (_PawnLoan == null)
            {
                MessageBox.Show("Pass in a Pawn Loan to display page.", "Pawn Loan Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                Setup();
        }

        private void Setup()
        {
            PH_PrevLoanNumberText.Text = _PawnLoan.PrevTicketNumber.ToString();
            PH_OriginationDateValue.Text = Utilities.GetStringValue(_PawnLoan.OriginationDate, "");
            PH_DueDateValue.Text = _PawnLoan.DueDate.ToShortDateString();
            PH_EligibilityDateValue.Text = _PawnLoan.PfiEligible.ToShortDateString();
            PH_NotificationDateValue.Text = _PawnLoan.PfiNote.ToShortDateString();

            var activeCustomerIdentity = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.getFirstIdentity();
            PH_CustomerIdentificationValue.Text = activeCustomerIdentity.IdIssuerCode
                                                  + " "
                                                  + activeCustomerIdentity.DatedIdentDesc
                                                  + " " + activeCustomerIdentity.IdValue + System.Environment.NewLine
                                                  + " exp " + activeCustomerIdentity.IdExpiryData.ToShortDateString();
            PH_NotesValue.Text = string.Empty;

            PH_OrigLoanNumberText.Text = _PawnLoan.OrigTicketNumber.ToString();
            PH_LoanValue.Text = String.Format("{0:C}", _PawnLoan.Amount);
            PH_InterestValue.Text = String.Format("{0:C}", _PawnLoan.InterestAmount);
            PH_ServiceChargeValue.Text = String.Format("{0:C}", _PawnLoan.ServiceCharge);
            PH_CurrentLoanNumberText.Text = _PawnLoan.TicketNumber.ToString();
            decimal loanFee = _PawnLoan.Fees.Find(f => f.FeeType == FeeTypes.ORIGIN).Value;
            PH_LoanOrigFeeValue.Text = String.Format("{0:C}", loanFee);
            PH_MadeByEmpValue.Text = _PawnLoan.EntityId;

            PH_TerminalIDValue.Text = _PawnLoan.OrgShopNumber;
            string loanStatus = (from l in GlobalDataAccessor.Instance.DesktopSession.LoanStatus
                                 where l.Left == _PawnLoan.LoanStatus
                                 select l.Right).First<string>();
            PH_LoanStatusValue.Text = loanStatus;
            PH_ApprovedByValue.Text = _PawnLoan.ApprovedBy;
            //PH_TotalLoanAmountText.Text = 
            this.Text = "Pawn Loan " + _PawnLoan.TicketNumber.ToString() + " - "
                        + GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName
                        + " "
                        + GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName;

            foreach (Item pawnItem in _PawnLoan.Items)
            {
                int gvIdx = PH_ItemsDataGridView.Rows.Add();
                var myRow = PH_ItemsDataGridView.Rows[gvIdx];

                myRow.Cells["PH_ItemNumberColumn"].Value = pawnItem.Icn;
                myRow.Cells["PH_DescriptionColumn"].Value = pawnItem.TicketDescription;
                myRow.Cells["PH_LoanAmountColumn"].Value = String.Format("{0:C}", pawnItem.ItemAmount);
                //PWNU00000220 S.Murphy populate PH_SuggestedAmountColumn and PH_LocationColumn
                myRow.Cells["PH_SuggestedAmountColumn"].Value = String.Format("{0:C}", pawnItem.SelectedProKnowMatch.selectedPKData.LoanVarHighAmount);
                myRow.Cells["PH_LocationColumn"].Value = pawnItem.GetFullLocation();
            }
        }

        private void PH_CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
