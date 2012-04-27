using Common.Libraries.Forms.Components;
using Common.Controllers.Application;
using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Inquiry.Retail
{
    public partial class RetailInquirySearch : Form
    {
        public NavBox NavControlBox;

        public RetailInquirySearch()
        {
            InitializeComponent();

            NavControlBox = new NavBox();
            NavControlBox.Owner = this;

            dateCalendarStart.PositionPopupCalendarOverTextbox = true;
            dateCalendarEnd.PositionPopupCalendarOverTextbox = true;
            dateCalendarStart.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
            dateCalendarEnd.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");

            status_cb.DataSource = StringDBMap_Enum<RetailInquiry.searchStatus_enum>.displayValues();

            sortBy_cb.DataSource = StringDBMap_Enum<RetailInquiry.sortField_enum>.displayValues();
            sortBy_cb.SelectedIndex = 0;

            sortDir_cb.SelectedIndex = 0;
            status_cb.SelectedIndex = 0;
        }
             
        private void dateOption_rb_CheckedChanged(object sender, EventArgs e)
        {
            dateCalendarStart.Enabled = dateOption_rb.Checked;
            dateCalendarEnd.Enabled = dateOption_rb.Checked;
            fromMSR_tb.Enabled = MSROption_rb.Checked;
            toMSR_tb.Enabled = MSROption_rb.Checked;

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
            fromMSR_tb.Text = "";
            toMSR_tb.Text = "";
            dateCalendarStart.Text = "";
            dateCalendarEnd.Text = "";
        }

        private void validate()
        {
            if (dateOption_rb.Checked)
            {
                if (DateTime.Parse(dateCalendarStart.SelectedDate) > DateTime.Parse(dateCalendarEnd.SelectedDate))
                    throw new BusinessLogicException("To: date is greater than the “From” date. Please adjust the date.");
            }
            else
            {
                if (string.IsNullOrEmpty(fromMSR_tb.Text) && string.IsNullOrEmpty(toMSR_tb.Text))
                    throw new BusinessLogicException("An MSR number must be entered.");
            }
        }

        private void Find_btn_Click(object sender, EventArgs e)
        {
            try { validate(); }
            catch (BusinessLogicException blex)
            { 
                MessageBox.Show(blex.Message);
                return;
            }

            Cursor.Current = Cursors.WaitCursor; 
            
            var retailData = new RetailInquiry()
            {
                byDate = dateOption_rb.Checked,
                status = (RetailInquiry.searchStatus_enum)status_cb.SelectedIndex,
                userID = userID_tb.Text,
                sortBy = (RetailInquiry.sortField_enum) sortBy_cb.SelectedIndex,
                sortDir = (RetailInquiry.sortDir_enum) sortDir_cb.SelectedIndex
            };

            if (dateOption_rb.Checked)
            {
                retailData.startDate = dateCalendarStart.SelectedDate;
                retailData.endDate = dateCalendarEnd.SelectedDate;
            }
            else
            {
                if (!string.IsNullOrEmpty(fromMSR_tb.Text))
                    int.TryParse(fromMSR_tb.Text, out retailData.lowMSR);

                if (!string.IsNullOrEmpty(toMSR_tb.Text))
                    int.TryParse(toMSR_tb.Text, out retailData.highMSR);
            }

            if (!string.IsNullOrEmpty(lowSaleAmt_tb.Text))
                double.TryParse(lowSaleAmt_tb.Text, out retailData.lowSaleAmount);

            if (!string.IsNullOrEmpty(highSaleAmt_tb.Text))
                double.TryParse(highSaleAmt_tb.Text, out retailData.highSaleAmount);

            if (!string.IsNullOrEmpty(lowCostAmt_tb.Text))
                double.TryParse(lowCostAmt_tb.Text, out retailData.lowCostAmount);

            if (!string.IsNullOrEmpty(highCostAmt_tb.Text))
                double.TryParse(highCostAmt_tb.Text, out retailData.highCostAmount);

            switch (layawayOriginated_cb.SelectedIndex)
            {
                case 0:
                    retailData.layawayOriginated = "Y";
                    break;

                case 1:
                    retailData.layawayOriginated = "N";
                    break;

                default:
                    retailData.layawayOriginated = "";
                    break;
            }

            switch (includeVoids_cb.SelectedIndex)
            {
                case 0:
                    retailData.includeVoids = "Y";
                    break;

                case 1:
                    retailData.includeVoids = "N";
                    break;

                default:
                    retailData.includeVoids = "";
                    break;
            }

            DataSet s = null;
            try
            {
                s = retailData.getData();

                if (s.IsNullOrEmpty())
                    throw new BusinessLogicException(ReportConstants.NODATA);
            }
            catch (BusinessLogicException blex)
            {
                MessageBox.Show(blex.Message);
                return;
            }

            Cursor.Current = Cursors.Default;

            this.Visible = false;
            var resultsDisplay = new RetailSearchResults(s, retailData, "RETAIL_INFO");

            resultsDisplay.ShowDialog();

            if (resultsDisplay.DialogResult == DialogResult.Cancel)
                this.Close();

            else
                this.Visible = true;
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }       

    }
}
