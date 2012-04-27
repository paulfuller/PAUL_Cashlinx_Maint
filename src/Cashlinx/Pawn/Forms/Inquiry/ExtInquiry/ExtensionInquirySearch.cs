using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Inquiry.ExtInquiry
{
    public partial class ExtensionInquirySearch : Form
    {
        public NavBox NavControlBox;


        public ExtensionInquirySearch()
        {
            InitializeComponent();

            NavControlBox = new NavBox();
            NavControlBox.Owner = this;


            dateCalendarStart.PositionPopupCalendarOverTextbox = true;
            dateCalendarEnd.PositionPopupCalendarOverTextbox = true;
            dateCalendarStart.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
            dateCalendarEnd.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");


            sortBy_cb.DataSource = StringDBMap_Enum<PawnExtInquiry.sortField_enum>.displayValues();
            sortBy_cb.SelectedIndex = 0;

            sortDir_cb.SelectedIndex = 0;
        }
             
        private void dateOption_rb_CheckedChanged(object sender, EventArgs e)
        {
            dateCalendarStart.Enabled = dateOption_rb.Checked;
            dateCalendarEnd.Enabled = dateOption_rb.Checked;

            if (dateOption_rb.Checked)
                clear_fields();
        }

        private void TicketOption_rb_CheckedChanged(object sender, EventArgs e)
        {
            fromTicket_tb.Enabled = TicketOption_rb.Checked;
            toTicket_tb.Enabled = TicketOption_rb.Checked;

            if (TicketOption_rb.Checked)
                clear_fields();

        }



        private void Clear_btn_Click(object sender, EventArgs e)
        {
            clear_fields();
        }

        private void clear_fields()
        {
            dateCalendarStart.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
            dateCalendarEnd.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
            fromTicket_tb.Text = "";
            toTicket_tb.Text = "";
            fromTicket_tb.Text = "";
            toTicket_tb.Text = "";
            lowLoanAmt_tb.Text = "";
            highLoanAmt_tb.Text = "";
            sortBy_cb.SelectedIndex = -1;
            sortDir_cb.SelectedIndex = 0;
            userID_tb.Text = "";

        }

        private void Find_btn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            var loanData = new PawnExtInquiry()
            {
                byDate = dateOption_rb.Checked,
                startDate = dateCalendarStart.SelectedDate,
                endDate = dateCalendarEnd.SelectedDate,
                userID = userID_tb.Text,
                sortBy = (PawnExtInquiry.sortField_enum)sortBy_cb.SelectedIndex,
                sortDir = (PawnExtInquiry.sortDir_enum)sortDir_cb.SelectedIndex
            };


            int.TryParse(fromTicket_tb.Text, out loanData.lowTicketNumber);
            int.TryParse(toTicket_tb.Text, out loanData.highTicketNumber);

            double.TryParse(lowLoanAmt_tb.Text, out loanData.lowAmount);
            double.TryParse(highLoanAmt_tb.Text, out loanData.highAmount);

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
            var resultsDisplay = new ExtensionSearchResults(s, loanData, "EXT_INFO");

            resultsDisplay.ShowDialog();

            if (resultsDisplay.DialogResult == DialogResult.Cancel)
                this.Close();
            else
                this.Visible = true;
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
            //this.NavControlBox.Action = NavBox.NavAction.BACK;
            //CashlinxDesktopSession.Instance.HistorySession.Desktop();
        }       

    }
}
