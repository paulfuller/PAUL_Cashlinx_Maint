using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Security;
using Pawn.Logic;
using Reports.Inquiry;

namespace Pawn.Forms.Inquiry.LoanInquiry
{
    public partial class LoanDetails : Form
    {
        DataView _theData;
        DataView _selectedData;
        private int _rowNum;

        public LoanDetails(int pawn_ticket_number, DataView s, int rowIdx)
        {
            InitializeComponent();

            #region DATABINDING
            _theData = s;
            _rowNum = rowIdx;
            
            pageInd.Text = string.Format(pageInd.Text, rowIdx + 1, _theData.Count);
            

            //_theData.DataViewManager.DataViewSettings["PAWN_INFO"].RowFilter = "TICKET_NUMBER='" + pawn_ticket_number + "'";
            
            _selectedData = _theData.DataViewManager.CreateDataView(_theData.Table);
            BindingContext[_selectedData].Position = _rowNum;
            //_selectedData.DataViewManager.CreateDataView("PAWN_CUST");
            //_theData.DefaultViewManager.DataViewSettings["PAWN_CUST"].RowFilter = "CUSTOMERNUMBER = '" + _selectedData[0]["CUSTOMERNUMBER"] + "'";
            

            //_selectedData = _theData.Tables["PAWN_INFO"].DefaultView;
            //_selectedData.RowFilter = "TICKET_NUMBER='" + pawn_ticket_number + "'";

            org_loan_nr.DataBindings.Add("Text", _selectedData, "ORG_TICKET");
            cur_loan_nr.DataBindings.Add("Text", _selectedData, "TICKET_NUMBER");
            prv_loan_nr.DataBindings.Add("Text", _selectedData, "PREV_TICKET");
            due_date.DataBindings.Add("Text", _selectedData, "DATE_DUE", true);
            due_date.DataBindings[0].FormatString = "d";
            shop.DataBindings.Add("Text", _selectedData, "STORENUMBER");
            cust_no.DataBindings.Add("Text", _selectedData, "CUSTOMERNUMBER");
            status_cd.DataBindings.Add("Text", _selectedData, "STATUS_CD");
            late_chg.DataBindings.Add("Text", _selectedData, "LATE_CHG", true);
            late_chg.DataBindings[0].FormatString = "c";

            other_chgs.DataBindings.Add("Text", _selectedData, "OTH_CHG", true);
            other_chgs.DataBindings[0].FormatString = "c";

            made_by.DataBindings.Add("Text", _selectedData, "ENT_ID");
            svc_charge.DataBindings.Add("Text", _selectedData, "SERV_CHG", true);
            svc_charge.DataBindings[0].FormatString = "c";

            pfi_elig.DataBindings.Add("Text", _selectedData, "PFI_ELIG", true);
            pfi_elig.DataBindings[0].FormatString = "d";
            pfi_notice.DataBindings.Add("Text", _selectedData, "PFI_NOTE", true);
            pfi_notice.DataBindings[0].FormatString = "d";
            status_dt.DataBindings.Add("Text", _selectedData, "STATUS_DATE", true);
            status_dt.DataBindings[0].FormatString = "g";
            loan_amount.DataBindings.Add("Text", _selectedData, "PRIN_AMOUNT", true);
            loan_amount.DataBindings[0].FormatString = "c";
            lblCurrentPrinicipalAmount.DataBindings.Add("Text", _selectedData, "PartPymtPrinAmt", true);
            lblCurrentPrinicipalAmount.DataBindings[0].FormatString = "c";

            interest.DataBindings.Add("Text", _selectedData, "INT_AMT", true);
            interest.DataBindings[0].FormatString = "c";

            org_date_time.DataBindings.Add("Text", _selectedData, "ORG_DATE", true);
            org_date_time.DataBindings[0].FormatString = "g";
            org_cust_no.DataBindings.Add("Text", _selectedData, "ORG_CUST");
            made_drawer.DataBindings.Add("Text", _selectedData, "CASH_DRAWER");
            clothing.DataBindings.Add("Text", _selectedData, "CLOTHING");

            terminal.DataBindings.Add("Text", _selectedData, "TTY_ID");
            pu_cust_no.DataBindings.Add("Text", _selectedData, "PU_CUST_NUM");

            disp_drawer.DataBindings.Add("Text", _selectedData, "DISP_DRAWER");
            disp_by.DataBindings.Add("Text", _selectedData, "DISP_ID");
            org_amount.DataBindings.Add("Text", _selectedData, "ORG_AMOUNT", true);
            org_amount.DataBindings[0].FormatString = "c";

            ref_amount.DataBindings.Add("Text", _selectedData, "REFUND_AMOUNT", true);
            ref_amount.DataBindings[0].FormatString = "c";

            onHold.DataBindings.Add("Text", _selectedData, "HOLD_TYPE");
            extend.DataBindings.Add("Text", _selectedData, "EXTEND");
            neg_int.DataBindings.Add("Text", _selectedData, "FIN_NEG");
            neg_svc.DataBindings.Add("Text", _selectedData, "SERV_NEG");
            notes.DataBindings.Add("Text", _selectedData, "COMMENTS");
            
            //DataRow customer = s.Tables["PAWN_INFO"].Rows[rowIdx].GetChildRows("customerRelation")[0];
            
            /*
            int idx = s.Tables["PAWN_CUST"].DefaultView.Find(s.Tables["PAWN_INFO"].Rows[rowIdx]["CUSTOMERNUMBER"].ToString());
            if (idx >= 0)
            {
                DataRow customer = s.Tables["PAWN_CUST"].Rows[idx];
            */
            //_selectedData.DataViewManager.
            //DataView _customerData = _selectedData[0].CreateChildView("customerRelation");
            cust_name.DataBindings.Add("Text", _selectedData, "customerRelation.CUST_NAME");
            cust_dob.DataBindings.Add("Text", _selectedData, "customerRelation.BIRTHDATE");
            cust_id.DataBindings.Add("Text", _selectedData, "customerRelation.ID");
            cust_since.DataBindings.Add("Text", _selectedData, "customerRelation.SINCE", true);
            cust_since.DataBindings[0].FormatString = "d";
                //cust_name.Text = customer["CUST_NAME"].ToString();
                //cust_dob.Text = customer["BIRTHDATE"].ToString();
                //cust_id.Text = customer["ID"].ToString();
                //cust_since.Text = string.Format ("{0:d}", customer["SINCE"]);
            //}

            //int key =  s.Tables["PAWN_INFO"].Rows[rowIdx].Field<int>("TICKET_NUMBER");
            //s.Tables["PAWN_MDSE"].DefaultView.RowFilter = "TICKET_NUMBER=" + key.ToString();
            //_theData.DefaultViewManager.DataViewSettings["PAWN_MDSE"].RowFilter = "TICKET_NUMBER=" + key.ToString();

            ItemsList_dg.AutoGenerateColumns = false;
            //ItemsList_dg.Columns[""].
            ItemsList_dg.DataSource = _selectedData;
            ItemsList_dg.DataMember = "merchandiseRelation";

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

        private void ItemsList_dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 )
            {

                this.Visible = false;

                var key = _selectedData[_rowNum].Row.Field<int>("TICKET_NUMBER");

                DataView itemView = _selectedData.DataViewManager.CreateDataView(_selectedData.Table);

                itemView.RowFilter = "TICKET_NUMBER='" + key + "'";
                Form details = new ItemDetails(itemView, e.RowIndex);
                details.ShowDialog();                

                switch (details.DialogResult)
                {
                    case DialogResult.OK:
                        this.Visible = true;
                        break;

                    default:
                        this.DialogResult = details.DialogResult;
                        this.Close();
                        break;
                }
            }
        }

        private void Print_btn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var dInitDate = Convert.ToDateTime(string.Format("{0:d}", ShopDateTime.Instance.ShopDate));
            var stoNum = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;

            string fileName = @"loan_detail_report_" + dInitDate.Ticks + ".pdf";

            string rptDir =
                SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.
                BaseLogPath;

            var rpt = new LoanDetailReport(
                rptDir + "\\" + fileName, stoNum,
                GlobalDataAccessor.Instance.CurrentSiteId.StoreName,
                dInitDate, "LOAN PROFILE\nTicket # " + cur_loan_nr.Text
                );

            var key = _selectedData[_rowNum].Row.Field<int>("TICKET_NUMBER");

            DataView reportView = _selectedData.DataViewManager.CreateDataView(_selectedData.Table);

            reportView.RowFilter = "TICKET_NUMBER='" + key + "'";

            rpt.CreateReport(reportView);
            this.TopMost = false;
            Cursor.Current = Cursors.Default;
            CashlinxDesktopSession.ShowPDFFile(rptDir + "\\" + fileName, false);
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
            if ( _theData.Count > 0)
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
            if (_rowNum + 1 < _theData.Count )
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
    }
}
