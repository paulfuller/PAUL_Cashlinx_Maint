using System;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Utility.Logger;
using System.Drawing;
using Common.Libraries.Utility.Shared;
//using Pawn.Forms.Report;

namespace Support.Forms.Customer.Products.Layaway
{
    public partial class LayawayPaymentHistory : CustomBaseForm
    {
        const string CELL_TOOLTIP_FORMAT = "{0,-20} {1,-20}";

        LayawayPaymentHistoryBuilder _builder;

        public LayawayPaymentHistory(LayawayVO layaway)
        {
            Layaway = layaway;
            InitializeComponent();
        }

        public LayawayVO Layaway { get; set; }

        private void LayawayPaymentHistory_Load(object sender, EventArgs e)
        {
            lblTotalLayaway.Text = (Layaway.Amount + Layaway.SalesTaxAmount).ToString("c");

            try
            {
                _builder = new LayawayPaymentHistoryBuilder(Layaway);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error building the payment schedule");
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "LayawayPaymentHistory_Load errored:  " + exc.Message);
                return;
            }

            lblAmountOutstanding.Text = _builder.GetBalanceOwed().ToString("c");
            lblPaidToDate.Text = _builder.GetTotalPaid().ToString("c");

            gvPayments.AutoGenerateColumns = false;

            foreach (LayawayHistory history in _builder.ScheduledPayments)
            {
                if (history.Payments.Count == 0)
                {
                    int idx = gvPayments.Rows.Add();
                    DataGridViewRow row = gvPayments.Rows[idx];
                    row.Cells[this.colPaymentDueDate.Index].Value = history.PaymentDueDate.ToString("d");
                    row.Cells[this.colPaymentAmountDue.Index].Value = history.PaymentAmountDue.ToString("c");
                }
                else
                {
                    for (int i = 0; i < history.Payments.Count; i++)
                    {
                        LayawayHistoryPaymentInfo paymentInfo = history.Payments.OrderBy(p => p.PaymentMadeOn).ToArray()[i];

                        int idx = gvPayments.Rows.Add();
                        DataGridViewRow row = gvPayments.Rows[idx];
                        if (i == 0)
                        {
                            row.Cells[this.colPaymentDueDate.Index].Value = history.PaymentDueDate.ToString("d");
                            row.Cells[this.colPaymentAmountDue.Index].Value = history.PaymentAmountDue.ToString("c");
                        }
                        row.Cells[this.colPaymentMadeOn.Index].Value = paymentInfo.PaymentMadeOn.ToString("d");
                        row.Cells[this.colPaymentAmountMade.Index].Value = paymentInfo.PaymentAmountMade.ToString("c");
                        row.Cells[this.colBalanceDue.Index].Value = paymentInfo.BalanceDue.ToString("c");
                        row.Cells[this.colReceiptNumber.Index].Value = paymentInfo.ReceiptNumber;
                        row.Cells[this.colStatus.Index].Value = paymentInfo.Status;
                        row.Tag = paymentInfo;
                    }
                }
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            //TODO - revisit if required - Madhu
            //LayawayCreateReportObject lco = new LayawayCreateReportObject();
            //lco.CreateHistoryAndScheduleReport(_builder, Layaway);
        }

        private void gvPayments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void gvPayments_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != colReceiptNumber.Index || e.RowIndex < 0)
            {
                return;
            }
            tooltipPanel.Visible = false;

            DataGridViewRow row = gvPayments.Rows[e.RowIndex];
            LayawayHistoryPaymentInfo paymentInfo = row.Tag as LayawayHistoryPaymentInfo;

            if (paymentInfo == null || paymentInfo.TenderDataDetails.Count == 0)
            {
                return;
            }

            tooltipPanel.Controls.Clear();
            tooltipPanel.ColumnCount = 2;
            tooltipPanel.RowCount = paymentInfo.TenderDataDetails.Count + 1;
            tooltipPanel.Height = (paymentInfo.TenderDataDetails.Count + 2) * 20;
            tooltipPanel.Width = 275;

            tooltipPanel.Controls.Add(new Label() { Text = "Amount" });
            tooltipPanel.Controls.Add(new Label() { Text = "Tender Type" });

            foreach (TenderData tenderData in paymentInfo.TenderDataDetails)
            {
                string tenderDescription = Commons.GetTenderDescription(tenderData);
                tooltipPanel.Controls.Add(new Label() { Text = tenderData.TenderAmount.ToString("c") });
                tooltipPanel.Controls.Add(new Label() { Text = tenderDescription, AutoSize = true, AutoEllipsis = true });
            }

            tooltipPanel.ColumnStyles[0].SizeType = SizeType.Percent;
            tooltipPanel.ColumnStyles[0].Width = 30;

            tooltipPanel.ColumnStyles[1].SizeType = SizeType.Percent;
            tooltipPanel.ColumnStyles[1].Width = 70;

            foreach (RowStyle rowStyle in tooltipPanel.RowStyles)
            {
                rowStyle.SizeType = SizeType.Absolute;
                rowStyle.Height = 20;
            }

            Rectangle cellPostition = gvPayments.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
            tooltipPanel.Location = new Point(gvPayments.Location.X + cellPostition.Left, gvPayments.Location.Y + cellPostition.Bottom);
            tooltipPanel.Visible = true;
        }

        private void gvPayments_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            tooltipPanel.Visible = false;
        }
    }
}
