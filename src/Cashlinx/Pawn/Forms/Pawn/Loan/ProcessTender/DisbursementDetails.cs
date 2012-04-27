using System;
using System.Linq;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Loan.ProcessTender
{
    public partial class DisbursementDetails : CustomBaseForm
    {
        private const string CASH = "Cash";
        private const string ACCOUNTSPAYABLE = "Accounts Payable";



        public DisbursementDetails()
        {
            InitializeComponent();
        }

        private void DisbursementDetails_Load(object sender, EventArgs e)
        {
            //If even one of the items being purchased is an expense item disable cash
            //only Bill To AP payment is allowed
            var expItem = from item in GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.Items
                           where item.IsExpenseItem
                           select item;
            if (expItem.Any())
                customButtonCash.Enabled = false;
            labelPayCustomerAmount.Text = String.Format("{0:C}",GlobalDataAccessor.Instance.DesktopSession.TotalServiceAmount);
            labelTenderSelected.Text = ACCOUNTSPAYABLE;

        }

        private void customButtonCash_Click(object sender, EventArgs e)
        {
            labelTenderSelected.Text = CASH;
        }

        private void customButtonACPayable_Click(object sender, EventArgs e)
        {
            labelTenderSelected.Text = ACCOUNTSPAYABLE;
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customButtonContinue_Click(object sender, EventArgs e)
        {
            GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.PurchaseTenderType =
                labelTenderSelected.Text == CASH ? PurchaseTenderTypes.CASHOUT.ToString() : PurchaseTenderTypes.BILLTOAP.ToString();
            this.Close();
        }
    }
}
