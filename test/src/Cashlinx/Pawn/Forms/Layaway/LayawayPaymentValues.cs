using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Pawn.Logic;

namespace Pawn.Forms.Layaway
{
    public partial class LayawayPaymentValues : CustomBaseForm
    {
        public List<LayawayVO> Layaways { get; set; }

        public decimal LayawayServiceAmount { get; private set; }

        private bool setRedColor = false;

        public LayawayPaymentValues(List<LayawayVO> layaways)
        {
            Layaways = layaways;

            InitializeComponent();

            gvPayments.AutoGenerateColumns = false;

            foreach (LayawayVO layaway in layaways)
            {
                LayawayPaymentHistoryBuilder builder;
                try
                {
                    builder = new LayawayPaymentHistoryBuilder(layaway);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error building the payment schedule");
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "gvPayments_CellLeave errored:  " + exc.Message);
                    continueButton.Enabled = false;
                    gvPayments.ReadOnly = true;
                    return;
                }

                DataGridViewRow row = gvPayments.Rows.AddNew();
                row.Cells[colNumber.Index].Value = layaway.TicketNumber.ToString();
                DateTime nextPayment = layaway.NextPayment;
                LayawayPaymentHistoryBuilder _builder = new LayawayPaymentHistoryBuilder(layaway);
                if (_builder.ScheduledPayments != null && _builder.ScheduledPayments.Count > 0)
                {
                    var lPaymentDate = (from lPayment in _builder.ScheduledPayments
                                        where lPayment.PaymentDueDate >= ShopDateTime.Instance.ShopDate
                                        select lPayment.PaymentDueDate).FirstOrDefault();
                    if (nextPayment <= ShopDateTime.Instance.ShopDate)
                    {
                        nextPayment = lPaymentDate != DateTime.MinValue ? lPaymentDate : layaway.LastPayment;
                    }
                }
                row.Cells[colDueDate.Index].Value = nextPayment.ToString("d");
                row.Cells[colPayment.Index].Value = builder.GetTotalDueNextPayment(ShopDateTime.Instance.FullShopDateTime).ToString("f2");
                row.Tag = layaway;
            }
        }

        private void LayawayPaymentValues_Load(object sender, EventArgs e)
        {
            EnableDisableContinueButton();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gvPayments.Rows)
            {
                LayawayVO layawaySrc = row.Tag as LayawayVO;
                LayawayVO layaway;
                int index = GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways.FindIndex(sl => sl.TicketNumber == layawaySrc.TicketNumber);
                if (index >= 0)
                {
                    layaway = GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways[index];
                }
                else
                {
                    layaway = new LayawayVO(layawaySrc);
                    if (layawaySrc != null)
                    {
                        layaway.SalesTaxAmount = layawaySrc.SalesTaxAmount;
                    }
                    GlobalDataAccessor.Instance.DesktopSession.ServiceLayaways.Add(layaway);
                }

                decimal paymentAmount = Convert.ToDecimal(row.Cells["colPayment"].Value);
                LayawayServiceAmount += paymentAmount;

                layaway.Payments.Add(new LayawayPayment() { Amount = paymentAmount });
            }

            this.Close();
        }

        private void gvPayments_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 2)
            {
                return;
            }

            DataGridViewCell paymentAmountCell = gvPayments[2, e.RowIndex];
            LayawayVO layaway = gvPayments.Rows[e.RowIndex].Tag as LayawayVO;
            decimal currentValue = Utilities.GetDecimalValue(paymentAmountCell.EditedFormattedValue, -1);
            if (currentValue <= 0)
            {
                MessageBox.Show("Invalid payment amount.");
                gvPayments.CancelEdit();
                return;
            }

            LayawayPaymentHistoryBuilder builder;
            try
            {
                builder = new LayawayPaymentHistoryBuilder(layaway);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error building the payment schedule");
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "gvPayments_CellLeave errored:  " + exc.Message);
                return;
            }

            if (currentValue > builder.GetBalanceOwed())
            {
                MessageBox.Show("Payment Cannot Exceed Total Owed");
                gvPayments.CancelEdit();
                return;
            }

            gvPayments.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void EnableDisableContinueButton()
        {
            foreach (DataGridViewRow row in gvPayments.Rows)
            {
                DataGridViewCell cell = row.Cells[colPayment.Index];
                if (cell.IsInEditMode)
                {
                    if (cell.EditedFormattedValue == null || cell.EditedFormattedValue.ToString().Trim() == string.Empty)
                    {
                        continueButton.Enabled = false;
                        return;
                    }
                }
                else if (cell.Value == null || cell.Value.ToString().Trim() == string.Empty)
                {
                    continueButton.Enabled = false;
                    return;
                }
            }

            continueButton.Enabled = true;
        }

        private void gvPayments_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyUp += new KeyEventHandler(Control_KeyUp);
        }

        void Control_KeyUp(object sender, KeyEventArgs e)
        {
            EnableDisableContinueButton();
        }

        private void gvPayments_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == colDueDate.Index)
            {
                LayawayVO layaway = gvPayments.Rows[e.RowIndex].Tag as LayawayVO;
                if (layaway.NextPayment < ShopDateTime.Instance.ShopDate)
                {
                    e.CellStyle.ForeColor = Color.Red;
                    gvPayments[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;
                }
            }
        }

        private void gvPayments_Paint(object sender, PaintEventArgs e)
        {
            if (!setRedColor)
            {
                foreach (DataGridViewRow row in gvPayments.Rows)
                {
                    if (row.Index == -1)
                    {
                        continue;
                    }

                    LayawayVO layaway = row.Tag as LayawayVO;
                    if (layaway.NextPayment < ShopDateTime.Instance.ShopDate)
                    {
                        row.Cells[colDueDate.Index].Style = new DataGridViewCellStyle() { ForeColor = Color.Red };
                    }
                }

                if (gvPayments.Rows.Count > 0)
                {
                    setRedColor = true;
                }
            }
        }

        private void LayawayPaymentValues_Shown(object sender, EventArgs e)
        {
            setRedColor = false;
        }
    }
}
