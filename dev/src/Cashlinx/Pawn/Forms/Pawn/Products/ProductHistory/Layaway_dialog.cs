using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Pawn.Products.ProductHistory
{
    partial class Layaway_dialog : CustomBaseForm
    {
        private readonly LayawayVO dataToShow;
        private readonly Receipt receiptToShow;

        public Layaway_dialog(LayawayVO data, Receipt r)
        {
            InitializeComponent();
            dataToShow = data;
            receiptToShow = r;
            LoadData();
        }

        private void LoadData()
        {
            string custName = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerName;
            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null)
            {
                if (receiptToShow.Event == ReceiptEventTypes.LAY.ToString())
                    labelHeading.Text = "New Layaway - " + dataToShow.TicketNumber + " " + custName;
                else
                    labelHeading.Text = "Layaway Payment - " + dataToShow.TicketNumber + " " + custName;
            }
            this.customLabelCashDrawer.Text = dataToShow.CashDrawerID;
            //this.customLabelCustID.Text
            this.customLabelDateTime.Text = Utilities.GetDateTimeValue(dataToShow.StatusDate).ToShortDateString() + " " +
                                            Utilities.GetDateTimeValue(dataToShow.StatusTime).ToShortTimeString();

            if (receiptToShow.EntID.Length > 0)
                this.customLabelEmpNo.Text = Utilities.GetStringValue(receiptToShow.EntID);

            else if (dataToShow.EntityId.Length > 0)
                this.customLabelEmpNo.Text = Utilities.GetStringValue(dataToShow.EntityId);

            else
                this.customLabelEmpNo.Text = Utilities.GetStringValue(dataToShow.CreatedBy);

            customLabelSaleAmtWTax.Text = string.Format("{0:C}", dataToShow.Amount);
            customLabelShopNumber.Text = receiptToShow.RefStoreNumber; //dataToShow.StoreNumber; //dataToShow.OrgShopNumber;

            if (dataToShow is LayawayVO)
            {
                customLabelLayawayNo.Text = dataToShow.TicketNumber.ToString();
            }
            else
            {
                customLabelLayawayNo.Text = "";
            }

            customLabelTenderData.Text = "";
            if (dataToShow.TenderDataDetails != null)
            {
                IEnumerable<TenderData> receiptTenders = from TenderData tdd in dataToShow.TenderDataDetails
                                                         where (tdd.ReceiptNumber.CompareTo(
                                                             receiptToShow.ReceiptNumber) == 0)
                                                         select tdd;

                foreach (TenderData td in receiptTenders)
                {
                    if (customLabelTenderData.Text.Length > 0)
                        customLabelTenderData.Text += "\n";                    
                    customLabelTenderData.Text += string.Format("{0}  {1:c}", td.MethodOfPmt, td.TenderAmount);
                }
            }
            //customLabelTerminalId.Text=dataToShow
        }

        private void PH_CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.Height > 108)
            {
                this.Height += tableLayoutPanel1.Height - 108;

                tableLayoutPanel1.Location = new Point(tableLayoutPanel1.Location.X, tableLayoutPanel1.Location.Y + 2);
                //PH_CloseButton.Location.Offset(0, tableLayoutPanel1.Height - 108);
            }
        }
    }
}
