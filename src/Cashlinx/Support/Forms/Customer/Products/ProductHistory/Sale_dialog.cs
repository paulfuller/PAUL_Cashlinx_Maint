/****************************************************************************
* Namespace:       CashlinxDesktop.DesktopForms.Pawn.Products.ProductHistory
* Class:           Sale_Dialog
* 
* Description      Popup Form to show specific information of a retail sale.
* 
* History
* Sreelatha Rengarajan, Initial Development
*  
*****************************************************************************/

using System;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Support.Forms.Customer.Products.ProductHistory
{
    public partial class Sale_dialog : CustomBaseForm
    {
        private readonly SaleVO saleDataToShow;

        public Sale_dialog(SaleVO saleData)
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
                labelHeading.Text = "Retail Sale - " + saleDataToShow.TicketNumber + " " + custName;
            }
            this.customLabelCashDrawer.Text = saleDataToShow.CashDrawerID;
            //this.customLabelCustID.Text
            this.customLabelDateTime.Text = Utilities.GetDateTimeValue(saleDataToShow.StatusDate).ToShortDateString() + " " +
                                            Utilities.GetDateTimeValue(saleDataToShow.StatusTime).ToShortTimeString();

            if (saleDataToShow.EntityId.Length > 0)
                this.customLabelEmpNo.Text = Utilities.GetStringValue(saleDataToShow.EntityId);
            else
                this.customLabelEmpNo.Text = Utilities.GetStringValue(saleDataToShow.CreatedBy);

            decimal fees = (from f in saleDataToShow.Fees where f.FeeType == FeeTypes.BACKGROUND_CHECK_FEE select f.Value).Sum();
            customLabelSaleAmtWTax.Text = string.Format("{0:C}", saleDataToShow.Amount + saleDataToShow.SalesTaxAmount + fees);
            customLabelSalesTaxAmount.Text = string.Format("{0:C}", saleDataToShow.SalesTaxAmount);
            customLabelShopNumber.Text = saleDataToShow.StoreNumber; //saleDataToShow.OrgShopNumber;
            customLabelMSRNo.Text = saleDataToShow.TicketNumber.ToString();

            if (saleDataToShow.LayawayTicketNumber != null)
            {
                customLabelLayawayNo.Text = saleDataToShow.LayawayTicketNumber;
            }
            else
            {
                customLabelLayawayNo.Text = "";
            }

            customLabelTenderData.Text = "";
            if (saleDataToShow.TenderDataDetails != null)
                foreach (TenderData td in saleDataToShow.TenderDataDetails)
                {
                    if (customLabelTenderData.Text.Length > 0)
                        customLabelTenderData.Text += "\n";
                    customLabelTenderData.Text += string.Format("{0}  {1:c}", td.MethodOfPmt, td.TenderAmount);
                }
            //customLabelTerminalId.Text=
            foreach (Item item in saleDataToShow.RetailItems)
            {
                int gvIdx = dataGridViewItems.Rows.Add();
                DataGridViewRow myRow = dataGridViewItems.Rows[gvIdx];

                myRow.Cells["icn"].Value = item.Icn;
                myRow.Cells["description"].Value = item.TicketDescription;
                myRow.Cells["status"].Value = item.ItemStatus;
                myRow.Cells["amount"].Value = String.Format("{0:C}", item.RetailPrice * item.Quantity);
            }
        }

        private void PH_CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.Size.Height > 103)
            {
                dataGridViewItems.Top += tableLayoutPanel1.Size.Height - 103;
                if (dataGridViewItems.Bottom > 320)
                {
                    this.Height += tableLayoutPanel1.Size.Height - 103;
                }
            }
        }
    }
}
