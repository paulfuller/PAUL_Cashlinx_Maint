/****************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Pawn.Products.ProductHistory
 * Class:           VoidSaleRefund_Dialog
 * 
 * Description      Popup Form to show specific information of a retail sale refund void.
 * 
 * History
 * Sreelatha Rengarajan, Initial Development
 *  
 *****************************************************************************/

using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;

namespace Support.Forms.Customer.Products.ProductHistory
{
    public partial class VoidSaleRefund_Dialog : CustomBaseForm
    {
        private readonly SaleVO saleDataToShow;
        public VoidSaleRefund_Dialog(SaleVO saleData)
        {
            InitializeComponent();
            saleDataToShow = saleData;
            LoadSaleData();
        }

        private void LoadSaleData()
        {
            string custName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerName;
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null)
            {

                labelHeading.Text = "Void Refund - " + saleDataToShow.TicketNumber + " " + custName;

            }
            this.customLabelCashDrawer.Text = saleDataToShow.CashDrawerID;
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null)
            {
                this.customLabelCustID.Text = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.getFirstIdentity().IDData;
            }
            this.customLabelDateTime.Text = Utilities.GetDateTimeValue(saleDataToShow.StatusDate).ToShortDateString() + " " +
                Utilities.GetDateTimeValue(saleDataToShow.StatusTime).ToShortTimeString();
            this.customLabelEmpNo.Text = Utilities.GetStringValue(saleDataToShow.CreatedBy);
            this.customLabelOrigMSRRefNo.Text = saleDataToShow.TicketNumber.ToString();
            customLabelRefundAmt.Text = string.Format("{0:C}", saleDataToShow.Amount);
            customLabelSaleStoreNo.Text = saleDataToShow.OrgShopNumber;

            //customLabelTerminalNo.Text=

            foreach (Item item in saleDataToShow.Items)
            {
                int gvIdx = dataGridViewItems.Rows.Add();
                DataGridViewRow myRow = dataGridViewItems.Rows[gvIdx];

                myRow.Cells["icn"].Value = item.Icn;
                myRow.Cells["description"].Value = item.TicketDescription;
                myRow.Cells["status"].Value = item.ItemStatus;
                myRow.Cells["amount"].Value = String.Format("{0:C}", item.RetailPrice);

            }
        }
    }
}
