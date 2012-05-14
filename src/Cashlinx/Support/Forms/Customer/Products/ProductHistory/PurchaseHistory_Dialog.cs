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
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility.Shared;

namespace Support.Forms.Customer.Products.ProductHistory
{
    public partial class PurchaseHistory_Dialog : Form
    {
        private PurchaseVO _purchase;
        private int _receiptIdx =-1;
        private string _status;
        public bool isSetup = false;

        public NavBox NavControlBox;

        public PurchaseHistory_Dialog(PurchaseVO purchase, int receiptIdx)
        {
            InitializeComponent();

            //Does not need this. -- Removed by TLR 6/14/2010
            //this.NavControlBox = new NavBox();
            //this.NavControlBox.Owner = this;

            _purchase = purchase;
            _receiptIdx = receiptIdx;
            if (_purchase == null)
            {
                MessageBox.Show("Pass in a Purchase to display page.", "Purchase Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                Setup();
        }
            
        public PurchaseHistory_Dialog(PurchaseVO purchase, int receiptIdx, string status)
        {
            InitializeComponent();

            //Does not need this. -- Removed by TLR 6/14/2010
            //this.NavControlBox = new NavBox();
            //this.NavControlBox.Owner = this;

            _purchase = purchase;
            _status = status;
            _receiptIdx = receiptIdx;
            if (_purchase == null)
            {
                MessageBox.Show("Pass in a Purchase to display page.", "Purchase Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Setup();
            }
                
        }

        private void Setup()
        {
            var receiptType = string.Empty;

            PH_EligibilityDateValue.Text = _purchase.PfiEligible.ToShortDateString();
            IdentificationVO activeCustomerIdentity = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.getFirstIdentity();
            PH_CustomerIdentificationValue.Text = activeCustomerIdentity.IdIssuerCode
                                                + " "
                                                + activeCustomerIdentity.DatedIdentDesc
                                                + " " + activeCustomerIdentity.IdValue + System.Environment.NewLine
                                                + " exp " + activeCustomerIdentity.IdExpiryData.ToShortDateString();

            PH_BuyAmountText.Text = String.Format("{0:C}", _purchase.Amount);
            PH_MadeByEmpValue.Text = _purchase.EntityId;         
            
            string loanStatus = (from l in GlobalDataAccessor.Instance.DesktopSession.LoanStatus
                                where l.Left == _purchase.LoanStatus
                                select l.Right).First<string>();
            PH_LoanStatusValue.Text =  loanStatus;
            PH_ApprovedByValue.Text = _purchase.ApprovedBy;
            if (_receiptIdx > -1 && _purchase.Receipts.Count > 0)
            {
                PH_OriginationDateValue.Text = _purchase.Receipts[_receiptIdx].Date.ToShortDateString();
                PH_TerminalIDValue.Text = _purchase.Receipts[_receiptIdx].StoreNumber;

                receiptType = _purchase.Receipts[_receiptIdx].Event ==
                                     ReceiptEventTypes.PUR.ToString()
                                         ? "Buy "
                                         : "Void Buy ";
            }
            else
            {
                if (_status == "VO")
                {
                    receiptType = "Void Buy ";
                }
                else
                {
                    receiptType = "Buy ";
                }
                PH_TerminalIDValue.Text = _purchase.StoreNumber;
                PH_OriginationDateValue.Text = _purchase.DateMade.Date.ToShortDateString();
            }
            this.Text = string.Format("{0}{1} - {2} {3}", receiptType, _purchase.TicketNumber, 
                GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName, 
                GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName);

            foreach (Item pawnItem in _purchase.Items)
            {
                int gvIdx = PH_ItemsDataGridView.Rows.Add();
                DataGridViewRow myRow = PH_ItemsDataGridView.Rows[gvIdx];

                myRow.Cells["PH_ItemNumberColumn"].Value = pawnItem.Icn;
                myRow.Cells["PH_DescriptionColumn"].Value = pawnItem.TicketDescription;
                myRow.Cells["PH_LoanAmountColumn"].Value = String.Format("{0:C}", pawnItem.ItemAmount);
                myRow.Cells["PH_SuggestedAmountColumn"].Value = String.Format("{0:C}", pawnItem.SelectedProKnowMatch.selectedPKData.LoanVarHighAmount);
                myRow.Cells["PH_LocationColumn"].Value = pawnItem.Location_Aisle.ToString() + " " + pawnItem.Location_Shelf.ToString() + " " + pawnItem.Location.ToString();
            }

            isSetup = true;

        }

        private void PH_CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PH_DueDateValue_Click(object sender, EventArgs e)
        {

        }

        private void PH_DueDateLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
