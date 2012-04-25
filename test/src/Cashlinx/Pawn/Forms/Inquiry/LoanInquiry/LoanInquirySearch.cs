using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Inquiry.LoanInquiry
{
    public partial class LoanInquirySearch : Form
    {
        public NavBox NavControlBox;

        public LoanInquirySearch()
        {
            InitializeComponent();

            NavControlBox = new NavBox();
            NavControlBox.Owner = this;

            dateCalendarStart.PositionPopupCalendarOverTextbox = true;
            dateCalendarEnd.PositionPopupCalendarOverTextbox = true;
            dateCalendarStart.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
            dateCalendarEnd.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");

            dateOptions_CB.DataSource = StringDBMap_Enum<PawnInquiry.searchDateType_enum>.displayValues();
            TicketNumberType_CB.DataSource = StringDBMap_Enum<PawnInquiry.searchTicketType_enum>.displayValues();
            status_cb.DataSource = StringDBMap_Enum<PawnInquiry.searchStatus_enum>.displayValues();

            sortBy_cb.DataSource = StringDBMap_Enum<PawnInquiry.sortField_enum>.displayValues ();
            sortBy_cb.SelectedIndex = 0;

            sortDir_cb.SelectedIndex = 0;
            status_cb.SelectedIndex = 0;
            mailer_cb.SelectedIndex = -1;
        }
             
        private void dateOption_rb_CheckedChanged(object sender, EventArgs e)
        {
            dateOptions_CB.Enabled = dateOption_rb.Checked;
            dateCalendarStart.Enabled = dateOption_rb.Checked;
            dateCalendarEnd.Enabled = dateOption_rb.Checked;

            if (dateOption_rb.Checked)
            {
                clear_fields();
            }
        }

        private void TicketOption_rb_CheckedChanged(object sender, EventArgs e)
        {
            TicketNumberType_CB.Enabled = TicketOption_rb.Checked;
            fromTicket_tb.Enabled = TicketOption_rb.Checked;
            toTicket_tb.Enabled = TicketOption_rb.Checked;

            if (TicketOption_rb.Checked)
            {
                clear_fields();
            }

        }

        private void Clear_btn_Click(object sender, EventArgs e)
        {
            clear_fields();
        }

        private void clear_fields()
        {
            dateOptions_CB.SelectedIndex = 0;
            TicketNumberType_CB.SelectedIndex = 0;
            status_cb.SelectedIndex = 0;
            mailer_cb.SelectedIndex = -1;

            dateCalendarStart.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
            dateCalendarEnd.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
            fromTicket_tb.Text = string.Empty;
            toTicket_tb.Text = string.Empty;
            lowLoanAmt_tb.Text = string.Empty;
            highLoanAmt_tb.Text = string.Empty;
            sortBy_cb.SelectedIndex = 0;
            userID_tb.Text = string.Empty;
            sortDir_cb.SelectedIndex = 0;
        }

        private void Find_btn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PawnInquiry.searchDateType_enum dateSearchType = (PawnInquiry.searchDateType_enum)this.dateOptions_CB.SelectedIndex;
            PawnInquiry.searchTicketType_enum ticketSearchType = (PawnInquiry.searchTicketType_enum)TicketNumberType_CB.SelectedIndex;

            var loanData = new PawnInquiry()
            {
                byDate = dateOption_rb.Checked,
                dateType = dateSearchType,
                startDate = dateCalendarStart.SelectedDate,
                endDate = dateCalendarEnd.SelectedDate,
                ticketType = ticketSearchType,
                status = (PawnInquiry.searchStatus_enum)status_cb.SelectedIndex,
                userID = userID_tb.Text,
                sortBy = (PawnInquiry.sortField_enum) sortBy_cb.SelectedIndex,
                sortDir = (PawnInquiry.sortDir_enum) sortDir_cb.SelectedIndex
            };


            if (fromTicket_tb.Text.Length > 0)
                int.TryParse(fromTicket_tb.Text, out loanData.lowTicketNumber);

            if (toTicket_tb.Text.Length > 0)
                int.TryParse(toTicket_tb.Text, out loanData.highTicketNumber);

            if (lowLoanAmt_tb.Text.Length > 0)
                double.TryParse(lowLoanAmt_tb.Text, out loanData.lowAmount);

            if (highLoanAmt_tb.Text.Length > 0)
                double.TryParse(highLoanAmt_tb.Text, out loanData.highAmount);

            switch (mailer_cb.SelectedIndex)
            {
                case 0:
                    loanData.pfiMailer = "Y";
                    break;

                case 1:
                    loanData.pfiMailer = "N";
                    break;

                default:
                    loanData.pfiMailer = string.Empty;
                    break;
            }

            DataSet s = null;
            try
            {
                s = loanData.getData();

                if (s.IsNullOrEmpty())
                    throw new BusinessLogicException(ReportConstants.NODATA);
            }
            catch (BusinessLogicException blex)
            {
                MessageBox.Show(blex.Message);
                return;
            }
            //this.NavControlBox.Action = NavBox.NavAction.HIDEANDSHOW;
            Cursor.Current = Cursors.Default;

            this.Visible = false;
//            var resultsDisplay = new LoanSearchResults(s, loanData,"PAWN_INFO");

            if (loanData.errorLevel != 0)
            {
                MessageBox.Show(loanData.errorMessage);
            }
            else
            {
                this.Visible = false;
                var resultsDisplay = new LoanSearchResults(s, loanData,"PAWN_INFO");

                resultsDisplay.ShowDialog();

                if (resultsDisplay.DialogResult == DialogResult.Cancel)
                {
                    this.Close();
                }
                else
                {
                    this.Visible = true;
                }

            }
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
            //this.NavControlBox.Action = NavBox.NavAction.BACK;
            //CashlinxDesktopSession.Instance.HistorySession.Desktop();
        }       

    }
}
