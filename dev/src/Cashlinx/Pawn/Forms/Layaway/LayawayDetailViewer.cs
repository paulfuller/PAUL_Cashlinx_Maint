using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Layaway
{
    public partial class LayawayDetailViewer : CustomBaseForm
    {
        # region Constructors

        public LayawayDetailViewer(List<LayawayVO> layaways)
        {
            InitializeComponent();
            this.Layaways = layaways;
            this.CurrentLayawayIndex = 0;
        }

        # endregion

        # region Events

        public event EventHandler ForfeitCompleted;

        # endregion

        # region Properties

        private int CurrentLayawayIndex { get; set; }
        public List<LayawayVO> Layaways { get; private set; }

        # endregion

        # region Event Handlers

        private void LayawayDetailViewer_Shown(object sender, EventArgs e)
        {
            SetDetailsBasedOnNumberOfLayaways();
            DisplayCurrentLayaway();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (ForfeitCompleted != null)
            {
                ForfeitCompleted(this, EventArgs.Empty);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (CurrentLayawayIndex == 0)
            {
                return;
            }

            ChangeCurrentLayaway(CurrentLayawayIndex - 1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (CurrentLayawayIndex == Layaways.Count - 1)
            {
                return;
            }

            ChangeCurrentLayaway(CurrentLayawayIndex + 1);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        # endregion

        # region Helper Methods

        private void ChangeCurrentLayaway(int layawayIndex)
        {
            CurrentLayawayIndex = layawayIndex;

            btnPrevious.Enabled = layawayIndex > 0;
            btnNext.Enabled = layawayIndex < Layaways.Count - 1;
            DisplayCurrentLayaway();
        }

        private void DisplayCurrentLayaway()
        {
            LayawayVO layaway = Layaways[CurrentLayawayIndex];

            lblLayawayDetailDescription.Text = string.Format("Layaway Detail Description - {0} {1}", layaway.TicketNumber, layaway.EntityName);
            lblPage.Text = string.Format("{0} of {1}", CurrentLayawayIndex + 1, Layaways.Count);

            lblLayawayNumberValue.Text = layaway.TicketNumber.ToString();
            lblDateAndTimeValue.Text = layaway.TimeMade.ToString("g");
            lblTotalLayawayCostValue.Text = layaway.Amount.ToString("c");
            lblCurrentStatusValue.Text = layaway.LoanStatus.ToString();
            lblForfeitureAmountValue.Text = layaway.GetAmountPaid().ToString("c");
            PopulatePaymentList(layaway);
            PopulateLayawayItems(layaway);

            try
            {
                LayawayPaymentHistoryBuilder layawayPaymentHistoryBuilder = new LayawayPaymentHistoryBuilder(layaway);
                LayawayHistory nextPaymentDue = layawayPaymentHistoryBuilder.GetFirstUnpaidPayment();

                if (nextPaymentDue != null)
                {
                    lblPaymentDueDateValue.Text = string.Format("{0} {1}", nextPaymentDue.PaymentDueDate.ToString("d"), layawayPaymentHistoryBuilder.GetTotalDueNextPayment(ShopDateTime.Instance.FullShopDateTime).ToString("c"));
                }
                else
                {
                    lblPaymentDueDateValue.Text = string.Empty;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error building the payment schedule");
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "DisplayCurrentLayaway errored:  " + exc.Message);
                return;
            }
        }

        private void PopulateLayawayItems(LayawayVO layaway)
        {
            gvLayawayItems.Rows.Clear();

            if (layaway == null || layaway.RetailItems == null || layaway.RetailItems.Count == 0)
            {
                return;
            }

            foreach (RetailItem item in layaway.RetailItems)
            {
                DataGridViewRow row = gvLayawayItems.Rows.AddNew();

                row.Cells[colIcn.Index].Value = item.Icn;
                row.Cells[colDescription.Index].Value = item.TicketDescription;
                row.Cells[colAisleShelfLocation.Index].Value = item.GetFullLocation();
                row.Cells[colCostAmount.Index].Value = (item.RetailPrice - item.ProratedCouponAmount - item.CouponAmount).ToString("c");
                row.Tag = item;
            }
        }

        private void PopulatePaymentList(LayawayVO layaway)
        {
            flowLayoutPanelPayments.Controls.Clear();

            if (layaway == null || layaway.Receipts == null)
            {
                return;
            }

            LayawayPaymentHistoryBuilder builder = new LayawayPaymentHistoryBuilder(layaway);
            var paymentReceipts = builder.GetPaymentReceipts(true, true).OrderBy(rn => rn.ReceiptNumber).ThenBy(rt => rt.RefTime);

            int receiptCounter = 0;
            foreach (Receipt receipt in paymentReceipts)
            {
                receiptCounter++;

                Label label = new Label();
                label.AutoSize = true;
                label.Text = string.Format("{0}] {1}  {2}  ", receiptCounter, receipt.Date.ToString("d"), receipt.Amount.ToString("c"));
                flowLayoutPanelPayments.Controls.Add(label);
            }
        }

        private void SetDetailsBasedOnNumberOfLayaways()
        {
            if (Layaways.Count == 0)
            {
                btnClose.Visible = true;
                btnPrevious.Visible = true;
                btnNext.Visible = true;
                btnComplete.Visible = true;

                lblPage.Text = "No layaways available";
                lblPage.Visible = true;
            }
            else if (Layaways.Count == 1)
            {
                btnPrevious.Visible = false;
                btnNext.Visible = false;
                btnBack.Visible = true;
                btnBack.Location = new Point(btnPrevious.Location.X, btnPrevious.Location.Y);

                btnClose.Visible = false;
                btnComplete.Visible = true;
                btnComplete.Location = new Point(btnClose.Location.X, btnClose.Location.Y);

                lblPage.Visible = false;
            }
            else
            {
                btnPrevious.Visible = true;
                btnNext.Visible = true;
                btnBack.Visible = false;

                btnClose.Visible = true;
                btnComplete.Visible = false;

                lblPage.Visible = true;
            }

            btnClose.Text = Layaways.Count > 1 ? "Close" : "Complete";
        }

        # endregion
    }
}
