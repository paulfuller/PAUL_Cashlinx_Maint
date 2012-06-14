using System.Collections.Generic;
using System.Linq;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application;
using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.UserControls;

//EDW DSTR TEst

namespace Pawn.Forms.Inquiry.GunBookInquiry
{
    public partial class GunBookInquirySearch : Form
    {
        public NavBox NavControlBox;

        public GunBookInquirySearch()
        {
            InitializeComponent();

            NavControlBox = new NavBox();
            NavControlBox.Owner = this;

            this.Controls.OfType<DateCalendar>().Select(
                c =>
                {
                    c.PositionPopupCalendarOverTextbox = true;
                    c.SelectedDate = "mm/dd/yyyy";
                    return c;
                });

            sortBy_cb.DataSource = StringDBMap_Enum<GunBookInquiry.sortField_enum>.displayValues();
            sortBy_cb.SelectedIndex = 0;

            sortDir_cb.SelectedIndex = 0;
        }

        private void Clear_btn_Click(object sender, EventArgs e)
        {
            #region EDW - DSTR Test
            //var confRef = SecurityAccessor.Instance.EncryptConfig;
            //var clientConfigDB = confRef.GetOracleDBService();

            ////Print end of day reports
            //var credentials = new Credentials
            //{
            //    UserName = confRef.DecryptValue(clientConfigDB.DbUser),
            //    PassWord = confRef.DecryptValue(clientConfigDB.DbUserPwd),
            //    DBHost = confRef.DecryptValue(clientConfigDB.Server),
            //    DBPort = confRef.DecryptValue(clientConfigDB.Port),
            //    DBService = confRef.DecryptValue(clientConfigDB.AuxInfo),
            //    DBSchema = confRef.DecryptValue(clientConfigDB.Schema)
            //};
            //var o = new BalanceCash();
            //o.ExecuteDSTR(credentials, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, true);
            #endregion

            #region EDW Gunbook Inquiry test code
            // EDW - Gunbook Inquiry test code
            int gunnumber_begin = this.txtGunNumber.X<int>();
            int gunnumber_end = this.txtGunNumberTo.X<int>();
            int orggunnumber_begin = this.txtOriginalGunNumber.X<int>();
            int orggunnumber_end = this.txtOriginalGunNumberTo.X<int>();
            int ticketnumber = txtLoanTicketNumber.X<int>();
            int icn_store = ucICN.Shop;
            int icn_year = ucICN.Year;
            int icn_doc = ucICN.Doc;
            string icn_doc_type = ucICN.DocType;
            int icn_item = ucICN.Item;
            int icn_sub_item = ucICN.SubItem;
            string bound = this.cbBound.X<string>();
            string model = this.txtModel.X<string>();
            string serialnumber = txtSerialNumber.X<string>();
            string type = cbType.X<string>();
            string status = cbStatus.X<string>();
            int gunbook_begin = this.txtGunBookPage.X<int>();
            int gunbook_end = this.txtGunBookPageTo.X<int>();
            string acq_startDate = this.ucAcq.StartDate;
            string acq_endDate = this.ucAcq.EndDate;
            string acq_lastname = this.ucAcq.LastName;
            string acq_firstname = this.ucAcq.FirstName;
            string acq_customernumber = this.ucAcq.CustomerNumber;
            string disp_startDate = this.ucDep.StartDate; // MM/DD/YYYY
            string disp_endDate = this.ucDep.EndDate;
            string disp_lastname = this.ucDep.LastName;
            string disp_firstname = this.ucDep.FirstName;
            string disp_customernumber = this.ucDep.CustomerNumber;
            GunBookInquiry.sortField_enum sortBy = GunBookInquiry.sortField_enum.GUNNUMBER;
            Inquiry.sortDir_enum sortDir = Inquiry.sortDir_enum.DESCENDING;

            // EDW - Gunbook Inquiry COUNT test code
            //DataSet ds = GunBookInquiry.getGunBookTestDataCount(
            //    gunnumber_begin,
            //    gunnumber_end,
            //    orggunnumber_begin,
            //    orggunnumber_end,
            //    ticketnumber,
            //    icn_store,
            //    icn_year,
            //    icn_doc,
            //    icn_doc_type,
            //    icn_item,
            //    icn_sub_item,
            //    bound,
            //    model,
            //    serialnumber,
            //    type,
            //    status,
            //    gunbook_begin,
            //    gunbook_end,
            //    acq_startDate         , // MM/DD/YYYY
            //    acq_endDate           ,
            //    acq_lastname          ,
            //    acq_firstname         ,
            //    acq_customernumber    ,
            //    disp_startDate        , // MM/DD/YYYY
            //    disp_endDate          ,
            //    disp_lastname         ,
            //    disp_firstname        ,
            //    disp_customernumber   
            //    );

            //var columnNum = ds.Tables[0].Rows[0][0];
            //var value = ds.Tables[0].Rows[0][1]; // <-- Count

            // EDW - Gunbook Inquiry DATA test code
            DataSet ds = GunBookInquiry.getData(
                gunnumber_begin,
                gunnumber_end,
                orggunnumber_begin,
                orggunnumber_end,
                ticketnumber,
                icn_store,
                icn_year,
                icn_doc,
                icn_doc_type,
                icn_item,
                icn_sub_item,
                bound,
                model,
                serialnumber,
                type,
                status,
                gunbook_begin,
                gunbook_end,
                acq_startDate, // MM/DD/YYYY
                acq_endDate,
                acq_lastname,
                acq_firstname,
                acq_customernumber,
                disp_startDate, // MM/DD/YYYY
                disp_endDate,
                disp_lastname,
                disp_firstname,
                disp_customernumber,
                sortBy,
                sortDir
                );

            if (ds != null)
            {
                var columnNum = ds.Tables[0].Rows[0][0];
                var value = ds.Tables[0].Rows[0][1];
            }

            #endregion

            // actual button code (e.g. non-test code)
            clear_fields();
        }

        private void clear_fields()
        {
            this.Controls.OfType<DateCalendar>().Select(
                c =>
                {
                    c.SelectedDate = "mm/dd/yyyy";
                    return c;
                });

            this.Controls.OfType<TextBox>().Select(
                c =>
                {
                    c.Text = string.Empty;
                    return c;
                });

            this.Controls.OfType<ComboBox>().Select(
                c =>
                {
                    c.SelectedIndex = 0;
                    return c;
                });
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Find_btn_Click(object sender, EventArgs e)
        {
            #region Dates
            string dateStartString = "01/01/2012";// dateCalendarStart.SelectedDate;
            string dateEndString = "05/01/2012";// dateCalendarEnd.SelectedDate;

            //if (dateCalendarStart.SelectedDate == "mm/dd/yyyy")
            //{
            //    dateStartString = string.Empty;
            //}
            //if (dateCalendarEnd.SelectedDate == "mm/dd/yyyy")
            //{
            //    dateEndString = string.Empty;
            //}

            DateTime dtStart = DateTime.MaxValue;
            DateTime dtEnd = DateTime.MinValue;
            DateTime.TryParse(dateStartString, out dtStart);
            DateTime.TryParse(dateStartString, out dtEnd);
            if (dtStart > dtEnd && dtEnd != DateTime.MinValue)
            {
                MessageBox.Show("'To:' date is greater than the 'From:' date. Please adjust the date.");
                return;
            }
            #endregion

            #region Amounts
            decimal amtFrom = -1;
            decimal amtTo = -1;

            //if (fromAmount_tb.Text.Length != 0 && !decimal.TryParse(fromAmount_tb.Text, out amtFrom))
            //{
            //    MessageBox.Show("'From' amount has an error. Please correct.");
            //    return;
            //}

            //if (toAmount_tb.Text.Length != 0 && !decimal.TryParse(toAmount_tb.Text, out amtTo))
            //{
            //    MessageBox.Show("'To' amount has an error. Please correct.");
            //    return;
            //}

            if (amtFrom > amtTo)
            {
                MessageBox.Show("'To:' amount is greater than the 'From:' amount. Please adjust the amounts.");
                return;
            }
            #endregion

            #region TicketNumber
            int loanTicketNumber = -1;

            if (this.txtLoanTicketNumber.Text.Length != 0 && !int.TryParse(this.txtLoanTicketNumber.Text, out loanTicketNumber))
            {
                MessageBox.Show("'Loan Ticket Number:' is not a valid number. Please correct.");
                return;
            }
            #endregion

            #region DOB
            string dob = string.Empty;
            DateTime dtDob = DateTime.MinValue;
            //if (txtDOB.DateTextBox.Text == "mm/dd/yyyy" || txtDOB.DateTextBox.Text.Length == 0)
            //{
            //    dob = string.Empty;
            //}
            //else if (DateTime.TryParse(txtDOB.DateTextBox.Text, out dtDob))
            //{
            //    if (dtDob.Year > DateTime.Now.Year)
            //    {
            //        dtDob = dtDob.AddYears(-100);
            //    }
            //    dob = dtDob.ToShortDateString();//.ToString("dd/MM/yyyy");
            //}
            //else
            //{
            //    MessageBox.Show("'Date of Birth' is not a valid date. Please correct.");
            //    return;
            //}
            #endregion

            Cursor.Current = Cursors.WaitCursor;

            var gunBookInquiry = new GunBookInquiry()
            {
                startDate = dateStartString,
                endDate = dateEndString,
                lowAmount = amtFrom,
                highAmount = amtTo,
                //TODO
//                lastName = this.txtLastName.Text,
//                firstName = this.txtFirstName.Text,
                dob = dob,
  //              customerNumber = this.txtCustomerNumber.Text,
                loanTicketNumber = loanTicketNumber,
                sortBy = (GunBookInquiry.sortField_enum)sortBy_cb.SelectedIndex,
                sortDir = (Inquiry.sortDir_enum)sortDir_cb.SelectedIndex
            };

            DataSet dataSetReturned = null;
            try
            {
                dataSetReturned = gunBookInquiry.getData();

                if (dataSetReturned.IsNullOrEmpty())
                    throw new BusinessLogicException(ReportConstants.NODATA);
            }
            catch (BusinessLogicException blex)
            {
                MessageBox.Show(blex.Message);
                return;
            }

            Cursor.Current = Cursors.Default;

            this.Visible = false;

            const string tableName = "PARTIAL_PAYMENT_INQ";

            var resultsDisplay = new Forms.Inquiry.GunBookInquiry.GunBookSearchResults(dataSetReturned, gunBookInquiry, tableName);

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

        private void gunOwner1_Load(object sender, EventArgs e)
        {

        }
    }
}
