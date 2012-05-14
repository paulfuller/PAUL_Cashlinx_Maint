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
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility.Shared;

namespace Support.Forms.Customer.Products.ProductHistory
{
    public partial class ReturnHistory_Dialog : Form
    {
        private PurchaseVO _purchase;
        private int _receiptIdx = -1;
        private string _status;

        public NavBox NavControlBox;

        public ReturnHistory_Dialog(PurchaseVO purchase, int receiptIdx)
        {
            InitializeComponent();

            _purchase = purchase;
            _receiptIdx = receiptIdx;

            if (_purchase == null)
            {
                MessageBox.Show("Pass in a Purchase to display page.", "Purchase Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                Setup();
        }

        public ReturnHistory_Dialog(PurchaseVO purchase, int recieptIdx, string status)
        {
            InitializeComponent();
            _receiptIdx = recieptIdx;
            _purchase = purchase;
            _status = status;
            if (_purchase == null)
            {
                MessageBox.Show("Pass in a Purchase to display page.", "Purchase Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                Setup();
        }

        private void Setup()
        {
            var receiptType = string.Empty;
            PH_OrigPurchaseNumberValue.Text = _purchase.PurchaseOrderNumber;
            IdentificationVO activeCustomerIdentity = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.getFirstIdentity();
            PH_ReturnReasonValue.Text = activeCustomerIdentity.IdIssuerCode
                                                + " "
                                                + activeCustomerIdentity.DatedIdentDesc
                                                + " " + activeCustomerIdentity.IdValue + System.Environment.NewLine
                                                + " exp " + activeCustomerIdentity.IdExpiryData.ToShortDateString();

            PH_ReturnAmountText.Text = String.Format("{0:C}", _purchase.Amount);
            PH_MadeByEmpValue.Text = _purchase.CreatedBy;
            PH_OriginationDateValue.Text = _purchase.OrgDate.ToShortDateString();
            PH_TerminalIDValue.Text = _purchase.OrgShopNumber;

            if (_receiptIdx > -1)
            {
                receiptType = _purchase.Receipts[_receiptIdx].Event ==
                                     ReceiptEventTypes.PUR.ToString()
                                         ? "Return "
                                         : "Void Return ";
            }
            else
            {
                if (_status == "VO")
                {
                    receiptType = "Void Return ";
                }
                else
                {
                    receiptType = "Return ";
                }
            }


            this.Text = receiptType + _purchase.TicketNumber.ToString() + " - "
                        + GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.FirstName
                        + " "
                        + GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.LastName;

            foreach (Item pawnItem in _purchase.Items)
            {
                int gvIdx = PH_ItemsDataGridView.Rows.Add();
                DataGridViewRow myRow = PH_ItemsDataGridView.Rows[gvIdx];

                myRow.Cells["PH_ItemNumberColumn"].Value = pawnItem.Icn;
                myRow.Cells["PH_DescriptionColumn"].Value = pawnItem.TicketDescription;
                myRow.Cells["PH_StatusColumn"].Value = pawnItem.ItemReason.ToString();
                myRow.Cells["PH_LoanAmountColumn"].Value = String.Format("{0:C}", pawnItem.ItemAmount);
            }
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
