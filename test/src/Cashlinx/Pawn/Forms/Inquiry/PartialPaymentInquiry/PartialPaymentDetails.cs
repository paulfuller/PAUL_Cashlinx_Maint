using System;
using System.Data;
using System.Windows.Forms;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Security;
using Reports.Inquiry;

namespace  Pawn.Forms.Inquiry.PartialPaymentInquiry
{
    public partial class PartialPaymentDetails : Form
    {
        private DataView    _theData;
        private DataView    _selectedData;
        private int         _rowNum;

        public PartialPaymentDetails(int pawn_ticket_number, DataView s, int rowIdx)
        {
            InitializeComponent();

            #region DATABINDING
            _theData = s;
            _rowNum = rowIdx;

            ItemsList_dg.ScrollBars = ScrollBars.None;
            ItemsList_dg.RowTemplate.MinimumHeight = 100;
            ItemsList_dg.RowTemplate.Height = 100;

            pageInd.Text = string.Format(pageInd.Text, rowIdx + 1, _theData.Count);

            //_theData.DataViewManager.DataViewSettings["PARTIAL_PAYMENT_INQ"].RowFilter = "TICKET_NUMBER='" + pawn_ticket_number + "'";
            
            _selectedData = _theData.DataViewManager.CreateDataView(_theData.Table);
            BindingContext[_selectedData].Position = _rowNum;

            lblCustomerNumber.DataBindings.Add("Text", _selectedData, "CUSTOMERNUMBER");
            lblCustomerName.DataBindings.Add("Text", _selectedData, "CUST_NAME");
            lblCustomerAddress.DataBindings.Add("Text", _selectedData, "ADDR");
            
            lblCustomerCity.DataBindings.Add("Text", _selectedData, "CITY");
            lblCustomerState.DataBindings.Add("Text", _selectedData, "STATE");
            lblCustomerZip.DataBindings.Add("Text", _selectedData, "ZIPCODE");
            lblCustomerPhone.DataBindings.Add("Text", _selectedData, "PHONE");

            lblCustomerID.DataBindings.Add("Text", _selectedData, "ID");
            lblCustomerSuppId.DataBindings.Add("Text", _selectedData, "SUP_ID");
            lblCustomerSSN.DataBindings.Add("Text", _selectedData, "SSN");
            lblCustomerDOB.DataBindings.Add("Text", _selectedData, "BIRTHDATE");
            lblCAIEmpNum.DataBindings.Add("Text", _selectedData, "CAI_EMPL_NUM");

            lblCustomerWeight.DataBindings.Add("Text", _selectedData, "WEIGHT");
            lblCustomerSex.DataBindings.Add("Text", _selectedData, "GENDERCODE");
            lblCustomerRace.DataBindings.Add("Text", _selectedData, "RACEDESC");
            lblCustomerHeight.DataBindings.Add("Text", _selectedData, "HEIGHT");
            lblCustomerHair.DataBindings.Add("Text", _selectedData, "HAIR_COLOR_CODE");
            lblCustomerEyes.DataBindings.Add("Text", _selectedData, "EYE_COLOR_CODE");

            lblPartialPaymentPrincipalReduction.DataBindings.Add("Text", _selectedData, "pmt_prin_amount", true);
            lblPartialPaymentPrincipalReduction.DataBindings[0].FormatString = "c";

            lblPartialPaymentServiceCharge.DataBindings.Add("Text", _selectedData, "PMT_SERV_AMT", true);
            lblPartialPaymentServiceCharge.DataBindings[0].FormatString = "c";

            lblPartialPaymentInterest.DataBindings.Add("Text", _selectedData, "PMT_INT_AMT", true);
            lblPartialPaymentInterest.DataBindings[0].FormatString = "c";

            lblPartialPaymentLateFee.DataBindings.Add("Text", _selectedData, "LATE_FEE", true);
            lblPartialPaymentLateFee.DataBindings[0].FormatString = "c";

            lblPartialPaymentTotalPayment.DataBindings.Add("Text", _selectedData, "PMT_AMOUNT", true);
            lblPartialPaymentTotalPayment.DataBindings[0].FormatString = "c";

            lblPartialPaymentPaymentDate.DataBindings.Add("Text", _selectedData, "PAYMENT_DATE_MADE", true);
            lblPartialPaymentPaymentDate.DataBindings[0].FormatString = "d";

            lblPartialPaymentStatus.DataBindings.Add("Text", _selectedData, "STATUS_CD");
            lblPartialPaymentPaymentMethod.DataBindings.Add("Text", _selectedData, "PAYMENT_METHOD");

            ItemsList_dg.AutoGenerateColumns = false;
            ItemsList_dg.DataSource = _selectedData;

            #endregion

            if (_theData.Count > 0)
            {
                nextPage.Enabled = true;
                lastPage.Enabled = true;
                firstPage.Enabled = true;
            }

            if (_rowNum > 0)
            {
                prevPage.Enabled = true;
            }

        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Refine_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Print_btn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var dInitDate = Convert.ToDateTime(string.Format("{0:d}", ShopDateTime.Instance.ShopDate));
            var stoNum = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;

            string fileName = @"partial_payment_detail_report_" + dInitDate.Ticks + ".pdf";

            string rptDir = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;

            var rpt = new PartialPaymentDetailReport(
                rptDir + "\\" + fileName, stoNum,
                GlobalDataAccessor.Instance.CurrentSiteId.StoreName,
                dInitDate);

            var key = _selectedData[_rowNum].Row.Field<Int64>("RECEIPTDETAIL_NUMBER");

            DataView reportView = _selectedData.DataViewManager.CreateDataView(_selectedData.Table);

            reportView.RowFilter = "RECEIPTDETAIL_NUMBER='" + key + "'";

            rpt.CreateReport(reportView);
            this.TopMost = false;
            this.BringToFront();
            Cursor.Current = Cursors.Default;
            DesktopSession.ShowPDFFile(rptDir + "\\" + fileName, false);
        }

        private void prevPage_Click(object sender, EventArgs e)
        {
            if (_rowNum - 1 >= 0 && _theData.Count > 0)
            {
                pageInd.Text = string.Format("Page {0} of {1}", --_rowNum + 1, _theData.Count);
                BindingContext[_selectedData].Position = _rowNum;
                //var pawnTicketNumber = _theData[_rowNum].Row.Field<int>("TICKET_NUMBER").ToString();
                //_theData.DefaultViewManager.DataViewSettings["PAWN_INFO"].RowFilter  
                //_selectedData.RowFilter = "TICKET_NUMBER='" + pawnTicketNumber + "'";

                setEnableDisableFirstLastNextPrevButtons();

                ResetBindings();
            }
        }

        private void lastPage_Click(object sender, EventArgs e)
        {
            if (_theData.Count > 0)
            {
                pageInd.Text = string.Format("Page {0} of {0}", _theData.Count);

                _rowNum = _theData.Count - 1;
                BindingContext[_selectedData].Position = _rowNum;
                //var pawnTicketNumber = _theData[_rowNum].Row.Field<int>("TICKET_NUMBER").ToString();
                //_theData.DefaultViewManager.DataViewSettings["PAWN_INFO"].RowFilter  
                //_selectedData.RowFilter = "TICKET_NUMBER='" + pawnTicketNumber + "'";

                setEnableDisableFirstLastNextPrevButtons();

                ResetBindings();
            }
        }

        private void nextPage_Click(object sender, EventArgs e)
        {
            if (_rowNum + 1 < _theData.Count)
            {
                pageInd.Text = string.Format("Page {0} of {1}", ++_rowNum + 1, _theData.Count);
                BindingContext[_selectedData].Position = _rowNum;
                //var pawnTicketNumber = _theData[_rowNum].Row.Field<int>("TICKET_NUMBER").ToString();
                //_theData.DefaultViewManager.DataViewSettings["PAWN_INFO"].RowFilter  
                // _selectedData.RowFilter = "TICKET_NUMBER='" + pawnTicketNumber + "'";

                setEnableDisableFirstLastNextPrevButtons();

                ResetBindings();
            }

        }

        private void firstPage_Click(object sender, EventArgs e)
        {
            if (_theData.Count > 0)
            {
                pageInd.Text = string.Format("Page {0} of {1}", 1, _theData.Count);

                _rowNum = 0;
                BindingContext[_selectedData].Position = _rowNum;
                //var pawnTicketNumber = _theData[_rowNum].Row.Field<int>("TICKET_NUMBER").ToString();
                //_theData.DefaultViewManager.DataViewSettings["PAWN_INFO"].RowFilter  
                //_selectedData.RowFilter = "TICKET_NUMBER='" + pawnTicketNumber + "'";

                setEnableDisableFirstLastNextPrevButtons();

                ResetBindings();
            }
        }

        private void setEnableDisableFirstLastNextPrevButtons()
        {
            if (_theData.Count == 0)
            {
                // No Data, disable everything
                this.firstPage.Enabled = false;
                this.prevPage.Enabled = false;
                this.nextPage.Enabled = false;
                this.lastPage.Enabled = false;
            }
            else
            {
                this.firstPage.Enabled = true;
                this.prevPage.Enabled = true;
                this.nextPage.Enabled = true;
                this.lastPage.Enabled = true;

                if (_rowNum == 0)
                {
                    // First Record, disable going left
                    this.firstPage.Enabled = false;
                    this.prevPage.Enabled = false;
                }

                if (_theData.Count - 1 == _rowNum)
                {
                    // Last Record, disable going right
                    this.nextPage.Enabled = false;
                    this.lastPage.Enabled = false;
                }

            }
        }

        private void PartialPaymentDetails_Load(object sender, EventArgs e)
        {
            BindingContext[_selectedData].Position = _rowNum;

        }
    }
}
