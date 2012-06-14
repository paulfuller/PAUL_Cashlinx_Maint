using System;
using System.Data;
using System.Windows.Forms;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Security;
using Reports.Inquiry;

namespace  Pawn.Forms.Inquiry.GunBookInquiry
{
    public partial class GunBookDetails : Form
    {
        private DataView    _theData;
        private DataView    _selectedData;
        private int         _rowNum;

        public GunBookDetails(int pawn_ticket_number, DataView s, int rowIdx)
        {
            InitializeComponent();

            #region DATABINDING
            this._theData = s;
            this._rowNum = rowIdx;

            this.ItemsList_dg.ScrollBars = ScrollBars.None;
            this.ItemsList_dg.RowTemplate.MinimumHeight = 100;
            this.ItemsList_dg.RowTemplate.Height = 100;

            this.pageInd.Text = string.Format(this.pageInd.Text, rowIdx + 1, this._theData.Count);

            //_theData.DataViewManager.DataViewSettings["PARTIAL_PAYMENT_INQ"].RowFilter = "TICKET_NUMBER='" + pawn_ticket_number + "'";
            
            this._selectedData = this._theData.DataViewManager.CreateDataView(this._theData.Table);
            BindingContext[this._selectedData].Position = this._rowNum;

            this.lblCustomerNumber.DataBindings.Add("Text", this._selectedData, "CUSTOMERNUMBER");
            this.lblCustomerName.DataBindings.Add("Text", this._selectedData, "CUST_NAME");
            this.lblCustomerAddress.DataBindings.Add("Text", this._selectedData, "ADDR");
            
            this.lblCustomerCity.DataBindings.Add("Text", this._selectedData, "CITY");
            this.lblCustomerState.DataBindings.Add("Text", this._selectedData, "STATE");
            this.lblCustomerZip.DataBindings.Add("Text", this._selectedData, "ZIPCODE");
            this.lblCustomerPhone.DataBindings.Add("Text", this._selectedData, "PHONE");

            this.lblCustomerID.DataBindings.Add("Text", this._selectedData, "ID");
            this.lblCustomerSuppId.DataBindings.Add("Text", this._selectedData, "SUP_ID");
            this.lblCustomerSSN.DataBindings.Add("Text", this._selectedData, "SSN");
            this.lblCustomerDOB.DataBindings.Add("Text", this._selectedData, "BIRTHDATE");
            this.lblCAIEmpNum.DataBindings.Add("Text", this._selectedData, "CAI_EMPL_NUM");

            this.lblCustomerWeight.DataBindings.Add("Text", this._selectedData, "WEIGHT");
            this.lblCustomerSex.DataBindings.Add("Text", this._selectedData, "GENDERCODE");
            this.lblCustomerRace.DataBindings.Add("Text", this._selectedData, "RACEDESC");
            this.lblCustomerHeight.DataBindings.Add("Text", this._selectedData, "HEIGHT");
            this.lblCustomerHair.DataBindings.Add("Text", this._selectedData, "HAIR_COLOR_CODE");
            this.lblCustomerEyes.DataBindings.Add("Text", this._selectedData, "EYE_COLOR_CODE");

            this.lblPrincipalReduction.DataBindings.Add("Text", this._selectedData, "pmt_prin_amount", true);
            this.lblPrincipalReduction.DataBindings[0].FormatString = "c";

            this.lblServiceCharge.DataBindings.Add("Text", this._selectedData, "PMT_SERV_AMT", true);
            this.lblServiceCharge.DataBindings[0].FormatString = "c";

            this.lblInterest.DataBindings.Add("Text", this._selectedData, "PMT_INT_AMT", true);
            this.lblInterest.DataBindings[0].FormatString = "c";

            this.lblLateFee.DataBindings.Add("Text", this._selectedData, "LATE_FEE", true);
            this.lblLateFee.DataBindings[0].FormatString = "c";

            this.lblTotalPayment.DataBindings.Add("Text", this._selectedData, "PMT_AMOUNT", true);
            this.lblTotalPayment.DataBindings[0].FormatString = "c";

            this.lblPaymentDate.DataBindings.Add("Text", this._selectedData, "PAYMENT_DATE_MADE", true);
            this.lblPaymentDate.DataBindings[0].FormatString = "d";

            this.lblStatus.DataBindings.Add("Text", this._selectedData, "STATUS_CD");
            this.lblPaymentPaymentMethod.DataBindings.Add("Text", this._selectedData, "PAYMENT_METHOD");

            this.ItemsList_dg.AutoGenerateColumns = false;
            this.ItemsList_dg.DataSource = this._selectedData;

            #endregion

            if (this._theData.Count > 0)
            {
                this.nextPage.Enabled = true;
                this.lastPage.Enabled = true;
                this.firstPage.Enabled = true;
            }

            if (this._rowNum > 0)
            {
                this.prevPage.Enabled = true;
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

            var key = this._selectedData[this._rowNum].Row.Field<Int64>("RECEIPTDETAIL_NUMBER");

            DataView reportView = this._selectedData.DataViewManager.CreateDataView(this._selectedData.Table);

            reportView.RowFilter = "RECEIPTDETAIL_NUMBER='" + key + "'";

            rpt.CreateReport(reportView);
            this.TopMost = false;
            this.BringToFront();
            Cursor.Current = Cursors.Default;
            DesktopSession.ShowPDFFile(rptDir + "\\" + fileName, false);
        }

        private void prevPage_Click(object sender, EventArgs e)
        {
            if (this._rowNum - 1 >= 0 && this._theData.Count > 0)
            {
                this.pageInd.Text = string.Format("Page {0} of {1}", --this._rowNum + 1, this._theData.Count);
                BindingContext[this._selectedData].Position = this._rowNum;
                //var pawnTicketNumber = _theData[_rowNum].Row.Field<int>("TICKET_NUMBER").ToString();
                //_theData.DefaultViewManager.DataViewSettings["PAWN_INFO"].RowFilter  
                //_selectedData.RowFilter = "TICKET_NUMBER='" + pawnTicketNumber + "'";

                setEnableDisableFirstLastNextPrevButtons();

                ResetBindings();
            }
        }

        private void lastPage_Click(object sender, EventArgs e)
        {
            if (this._theData.Count > 0)
            {
                this.pageInd.Text = string.Format("Page {0} of {0}", this._theData.Count);

                this._rowNum = this._theData.Count - 1;
                BindingContext[this._selectedData].Position = this._rowNum;
                //var pawnTicketNumber = _theData[_rowNum].Row.Field<int>("TICKET_NUMBER").ToString();
                //_theData.DefaultViewManager.DataViewSettings["PAWN_INFO"].RowFilter  
                //_selectedData.RowFilter = "TICKET_NUMBER='" + pawnTicketNumber + "'";

                setEnableDisableFirstLastNextPrevButtons();

                ResetBindings();
            }
        }

        private void nextPage_Click(object sender, EventArgs e)
        {
            if (this._rowNum + 1 < this._theData.Count)
            {
                this.pageInd.Text = string.Format("Page {0} of {1}", ++this._rowNum + 1, this._theData.Count);
                BindingContext[this._selectedData].Position = this._rowNum;
                //var pawnTicketNumber = _theData[_rowNum].Row.Field<int>("TICKET_NUMBER").ToString();
                //_theData.DefaultViewManager.DataViewSettings["PAWN_INFO"].RowFilter  
                // _selectedData.RowFilter = "TICKET_NUMBER='" + pawnTicketNumber + "'";

                setEnableDisableFirstLastNextPrevButtons();

                ResetBindings();
            }

        }

        private void firstPage_Click(object sender, EventArgs e)
        {
            if (this._theData.Count > 0)
            {
                this.pageInd.Text = string.Format("Page {0} of {1}", 1, this._theData.Count);

                this._rowNum = 0;
                BindingContext[this._selectedData].Position = this._rowNum;
                //var pawnTicketNumber = _theData[_rowNum].Row.Field<int>("TICKET_NUMBER").ToString();
                //_theData.DefaultViewManager.DataViewSettings["PAWN_INFO"].RowFilter  
                //_selectedData.RowFilter = "TICKET_NUMBER='" + pawnTicketNumber + "'";

                setEnableDisableFirstLastNextPrevButtons();

                ResetBindings();
            }
        }

        private void setEnableDisableFirstLastNextPrevButtons()
        {
            if (this._theData.Count == 0)
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

                if (this._rowNum == 0)
                {
                    // First Record, disable going left
                    this.firstPage.Enabled = false;
                    this.prevPage.Enabled = false;
                }

                if (this._theData.Count - 1 == this._rowNum)
                {
                    // Last Record, disable going right
                    this.nextPage.Enabled = false;
                    this.lastPage.Enabled = false;
                }

            }
        }

        private void Details_Load(object sender, EventArgs e)
        {
            
            BindingContext[this._selectedData].Position = this._rowNum;

        }
    }
}
