/****************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Pawn.Products.ProductHistory
 * Class:           Sale_Dialog
 * 
 * Description      Popup Form to show specific information of a retail sale void.
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
    public partial class VoidSale_dialog : CustomBaseForm
    {
        private readonly SaleVO saleDataToShow;
        public VoidSale_dialog(SaleVO saleData)
        {
            InitializeComponent();
            saleDataToShow = saleData;
            LoadSaleData();
        }

        private void LoadSaleData()
        {
            string custName=GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerName;
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null)
            {

                labelHeading.Text = "Void Retail Sale - " + saleDataToShow.TicketNumber + " " + custName;
                                    
            }
            this.customLabelCashDrawer.Text = saleDataToShow.CashDrawerID;
            //this.customLabelCustID.Text
            this.customLabelDateTime.Text = Utilities.GetDateTimeValue(saleDataToShow.StatusDate).ToShortDateString() + " " +
                Utilities.GetDateTimeValue(saleDataToShow.StatusTime).ToShortTimeString();
            this.customLabelEmpNo.Text = Utilities.GetStringValue(saleDataToShow.CreatedBy);
            customLabelOrigMSRNo.Text = saleDataToShow.TicketNumber.ToString();
            //this.customLabelMSRNo.Text = saleDataToShow.TicketNumber.ToString();
            customLabelSaleStoreNo.Text = string.Format("{0:C}", saleDataToShow.Amount);
            //customLabelSalesTaxAmount.Text = string.Format("{0:C}", saleDataToShow.SalesTaxAmount);
            //customLabelShopNumber.Text = saleDataToShow.OrgShopNumber;
            
            //customLabelTenderData.Text=
            //customLabelTerminalId.Text=

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
