/****************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Pawn.Products.ProductHistory
 * Class:           SaleRefund_Dialog
 * 
 * Description      Popup Form to show specific information of a retail sale refund.
 * 
 * History
 * Sreelatha Rengarajan, Initial Development
 *  
 *****************************************************************************/

using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;

namespace Pawn.Forms.Pawn.Products.ProductHistory
{
    public partial class SaleRefund_Dialog : CustomBaseForm
    {
        private readonly SaleVO saleDataToShow;
        public SaleRefund_Dialog(SaleVO saleData)
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

                labelHeading.Text = "Sale Refund - " + saleDataToShow.TicketNumber + " " + custName;

            }
            this.customLabelCashDrawer.Text = saleDataToShow.CashDrawerID;
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null)
            {
                this.customLabelCustID.Text = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.getFirstIdentity().IDData;
            }
            this.customLabelDateTime.Text = Utilities.GetDateTimeValue(saleDataToShow.StatusDate).ToShortDateString() + " " +
                Utilities.GetDateTimeValue(saleDataToShow.StatusTime).ToShortTimeString();
            this.customLabelEmpNo.Text = Utilities.GetStringValue(saleDataToShow.EntityId);
            this.customLabelOrigMSRRefNo.Text = saleDataToShow.TicketNumber.ToString();
            customLabelRefundAmt.Text = string.Format("{0:C}", saleDataToShow.Amount);
            customLabelSaleStoreNo.Text = saleDataToShow.StoreNumber;//OrgShopNumber;

            //customLabelTerminalNo.Text=

            foreach (RetailItem item in saleDataToShow.RetailItems)
            {
                int gvIdx = dataGridViewItems.Rows.Add();
                DataGridViewRow myRow = dataGridViewItems.Rows[gvIdx];

                myRow.Cells["icn"].Value = item.Icn;
                myRow.Cells["description"].Value = item.TicketDescription;
                myRow.Cells["status"].Value = item.ItemStatus;
                myRow.Cells["amount"].Value = String.Format("{0:C}", item.RetailPrice);

            }
        }

        private void PH_CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
