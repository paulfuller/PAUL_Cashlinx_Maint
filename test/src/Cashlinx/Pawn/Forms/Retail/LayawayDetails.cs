using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using System;
using System.Windows.Forms;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Utility;
using Pawn.Logic;

namespace Pawn.Forms.Retail
{
    public partial class LayawayDetails : CustomBaseForm
    {
        private bool _SuppressDatePickerTextChangedEvent;

        public LayawayPaymentCalculator LayawayPaymentCalculator
        {
            get { return CDS.LayawayPaymentCalc; }
            set { CDS.LayawayPaymentCalc = value; }
        }

        public decimal MaximumDownPayment { get; set; }
        public DateTime PreviousFirstPaymentDate { get; set; }

        public DesktopSession CDS
        {
            get { return GlobalDataAccessor.Instance.DesktopSession; }
        }

        public LayawayDetails()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LayawayDetails_Load(object sender, EventArgs e)
        {
            PreviousFirstPaymentDate = LayawayPaymentCalculator.FirstPaymentDate;
            UpdateCalculatedValues();
        }

        private void UpdateCalculatedValues()
        {
            LayawayPaymentCalculator.ReCalculate();

            txtNumberOfPayments.Text = LayawayPaymentCalculator.NumberOfPayments.ToString();
            txtDownPayment.Text = LayawayPaymentCalculator.DownPayment.ToString("f2");
            dateFirstPaymentDate.SetSelectedDate(LayawayPaymentCalculator.FirstPaymentDate);
            dateFirstPaymentDate.Refresh();
            txtServiceFee.Text = LayawayPaymentCalculator.ServiceFeeTotal.ToString("c");
            txtMonthlyPaymentAmount.Text = LayawayPaymentCalculator.MonthlyPaymentAmount.ToString("c");

            lblPayment01.Text = LayawayPaymentCalculator.Payments[0].BuildPaymentStatement();
            lblPayment02.Text = LayawayPaymentCalculator.Payments[1].BuildPaymentStatement();
            lblPayment03.Text = LayawayPaymentCalculator.Payments[2].BuildPaymentStatement();
            lblPayment04.Text = LayawayPaymentCalculator.Payments[3].BuildPaymentStatement();
            lblPayment05.Text = LayawayPaymentCalculator.Payments[4].BuildPaymentStatement();
            lblPayment06.Text = LayawayPaymentCalculator.Payments[5].BuildPaymentStatement();
            lblPayment07.Text = LayawayPaymentCalculator.Payments[6].BuildPaymentStatement();
            lblPayment08.Text = LayawayPaymentCalculator.Payments[7].BuildPaymentStatement();
            lblPayment09.Text = LayawayPaymentCalculator.Payments[8].BuildPaymentStatement();
            lblPayment10.Text = LayawayPaymentCalculator.Payments[9].BuildPaymentStatement();
            lblPayment11.Text = LayawayPaymentCalculator.Payments[10].BuildPaymentStatement();
            lblPayment12.Text = LayawayPaymentCalculator.Payments[11].BuildPaymentStatement();

            continueButton.Enabled = true;
            Refresh();
        }

        private void txtDownPayment_Leave(object sender, EventArgs e)
        {
            decimal downPayment = LayawayPaymentCalculator.DownPayment;

            if (!decimal.TryParse(txtDownPayment.Text, out downPayment))
            {
                MessageBox.Show("Invalid down payment amount.");
                HighlightControl(txtDownPayment);
                RevertToPreviousDownPayment();
                txtDownPayment.Focus();
                txtDownPayment.SelectAll();
                return;
            }

            if (downPayment < 0 || downPayment >= MaximumDownPayment)
            {
                MessageBox.Show(string.Format("Down Payment cannot be negative and must be less than {0:c}.", MaximumDownPayment));
                HighlightControl(txtDownPayment);
                RevertToPreviousDownPayment();
                txtDownPayment.Focus();
                txtDownPayment.SelectAll();
                return;
            }

            if (!LayawayPaymentCalculator.CanDistributeIfDownpaymentSetTo(downPayment))
            {
                MessageBox.Show(string.Format("This is an invalid value because it will not distribute accross {0} payments.", LayawayPaymentCalculator.NumberOfPayments));
                HighlightControl(txtDownPayment);
                RevertToPreviousDownPayment();
                txtDownPayment.Focus();
                txtDownPayment.SelectAll();
                return;
            }

            if (LayawayPaymentCalculator.DownPayment == downPayment)
            {
                return;
            }
            LayawayPaymentCalculator.DownPayment = downPayment;
            UpdateCalculatedValues();
        }

        private void txtNumberOfPayments_Leave(object sender, EventArgs e)
        {
            int numberOfPayments = 0;

            if (!int.TryParse(txtNumberOfPayments.Text, out numberOfPayments) || numberOfPayments <= 0)
            {
                MessageBox.Show("Invalid number of payments.");
                HighlightControl(txtNumberOfPayments);
                RevertToPreviousNumberOfPayments();
                txtNumberOfPayments.Focus();
                txtNumberOfPayments.SelectAll();
                return;
            }

            if (LayawayPaymentCalculator.NumberOfPayments == numberOfPayments)
            {
                return;
            }

            int maxNumberOfPayments = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMaxLayawayNumberOfPayments(CDS.CurrentSiteId);
            if (numberOfPayments > maxNumberOfPayments)
            {
                MessageBox.Show(string.Format("Maximum number of payments is {0}", maxNumberOfPayments));
                HighlightControl(txtNumberOfPayments);
                RevertToPreviousNumberOfPayments();
                txtNumberOfPayments.Focus();
                txtNumberOfPayments.SelectAll();
                return;
            }

            if (!LayawayPaymentCalculator.CanDistributeIfNumberOfPaymentsSetTo(numberOfPayments))
            {
                MessageBox.Show(string.Format("This is an invalid value because the layaway amount cannot distribute accross {0} payments.", numberOfPayments));
                HighlightControl(txtNumberOfPayments);
                RevertToPreviousNumberOfPayments();
                txtNumberOfPayments.Focus();
                txtNumberOfPayments.SelectAll();
                return;
            }

            LayawayPaymentCalculator.NumberOfPayments = numberOfPayments;
            UpdateCalculatedValues();
        }

        private void dateFirstPaymentDate_Leave(object sender, EventArgs e)
        {
            SetFirstPaymentOnChange();
        }

        private void SetFirstPaymentOnChange()
        {
            DateTime firstPaymentDate;

            if (!DateTime.TryParse(dateFirstPaymentDate.SelectedDate, out firstPaymentDate))
            {
                MessageBox.Show("Invalid first payment date.");
                HighlightControl(dateFirstPaymentDate.DateText.DateTextBox);
                RevertToPreviousFirstPaymentDate();
                dateFirstPaymentDate.Focus();
                return;
            }

            if (LayawayPaymentCalculator.FirstPaymentDate == firstPaymentDate)
            {
                continueButton.Enabled = true;
                return;
            }

            int minimumDaysPriorToDefaultPaymentDate = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMinimumPaymentDateLimit(CDS.CurrentSiteId);
            if (firstPaymentDate < LayawayPaymentCalculator.DefaultFirstPaymentDate.AddDays(-minimumDaysPriorToDefaultPaymentDate))
            {
                MessageBox.Show(string.Format("You may only reduce up to {0} days", minimumDaysPriorToDefaultPaymentDate));
                HighlightControl(dateFirstPaymentDate.DateText.DateTextBox);
                RevertToPreviousFirstPaymentDate();
                dateFirstPaymentDate.Focus();
                return;
            }

            int maximumDaysAfterToDefaultPaymentDate = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMaxPaymentDateLimit(CDS.CurrentSiteId);
            if (firstPaymentDate > LayawayPaymentCalculator.DefaultFirstPaymentDate.AddDays(maximumDaysAfterToDefaultPaymentDate))
            {
                MessageBox.Show(string.Format("You may only increase up to {0} days", maximumDaysAfterToDefaultPaymentDate));
                HighlightControl(dateFirstPaymentDate.DateText.DateTextBox);
                RevertToPreviousFirstPaymentDate();
                dateFirstPaymentDate.Focus();
                return;
            }

            PreviousFirstPaymentDate = firstPaymentDate;
            LayawayPaymentCalculator.FirstPaymentDate = firstPaymentDate;
            UpdateCalculatedValues();
        }

        private void dateFirstPaymentDate_SelectedDateChanging(object sender, EventArgs e)
        {
            _SuppressDatePickerTextChangedEvent = true;
        }

        private void dateFirstPaymentDate_SelectedDateChanged(object sender, EventArgs e)
        {
            SetFirstPaymentOnChange();
            _SuppressDatePickerTextChangedEvent = false;
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            continueButton.Enabled = false;
        }

        private void dateFirstPaymentDate_TextBoxTextChanged(object sender, EventArgs e)
        {
            if (_SuppressDatePickerTextChangedEvent)
            {
                return;
            }

            continueButton.Enabled = false;
        }

        private void dateFirstPaymentDate_CalendarMouseLeave(object sender, EventArgs e)
        {
            _SuppressDatePickerTextChangedEvent = false;
        }

        private void dateFirstPaymentDate_CalendarMouseLeaving(object sender, EventArgs e)
        {
            _SuppressDatePickerTextChangedEvent = true;
        }

        private void RevertToPreviousFirstPaymentDate()
        {
            dateFirstPaymentDate.SetSelectedDate(PreviousFirstPaymentDate);
            dateFirstPaymentDate.Refresh();
        }

        private void RevertToPreviousNumberOfPayments()
        {
            txtNumberOfPayments.Text = Utilities.GetIntegerValue(txtNumberOfPayments.PreviousValue, LayawayPaymentCalculator.DefaultNumberOfPayments).ToString();
        }

        private void RevertToPreviousDownPayment()
        {
            txtDownPayment.Text = Utilities.GetDecimalValue(txtDownPayment.PreviousValue, LayawayPaymentCalculator.DefaultDownPayment).ToString("f2");
        }
    }
}
