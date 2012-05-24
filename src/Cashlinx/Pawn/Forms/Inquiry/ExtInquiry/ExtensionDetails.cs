using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Security;
using Pawn.Logic;
using Reports.Inquiry;

namespace Pawn.Forms.Inquiry.ExtInquiry
{
    public partial class ExtensionDetails : Form
    {
        DataSet _theData;
        DataView _selectedData;

        public ExtensionDetails(int pawn_ticket_number, DataSet s, int rowIdx)
        {
            InitializeComponent();

            #region DATABINDING
            _theData = s;

            try
            {
                if (_theData.DefaultViewManager.DataViewSettings["EXT_INFO"] != null)
                {
                    _theData.DefaultViewManager.DataViewSettings["EXT_INFO"].RowFilter = "TICKET_NUMBER='" + pawn_ticket_number + "'";
                    _selectedData = _theData.DefaultViewManager.CreateDataView(_theData.Tables["EXT_INFO"]);

                    cur_loan_nr.DataBindings.Add("Text", _selectedData, "TICKET_NUMBER");
                    org_loan_nr.DataBindings.Add("Text", _selectedData, "ORG_TICKET");
                    terminal.DataBindings.Add("Text", _selectedData, "TTY_ID");
                    shop.DataBindings.Add("Text", _selectedData, "STORENUMBER");
                    cust_no.DataBindings.Add("Text", _selectedData, "CUSTOMERNUMBER");
                    status_cd.DataBindings.Add("Text", _selectedData, "LOAN_STATUS");

                    due_date.DataBindings.Add("Text", _selectedData, "DATE_DUE", true);
                    due_date.DataBindings[0].FormatString = "d";
                    org_date_time.DataBindings.Add("Text", _selectedData, "ORG_DATE", true);
                    org_date_time.DataBindings[0].FormatString = "g";
                    pfi_elig.DataBindings.Add("Text", _selectedData, "PFI_ELIG", true);
                    pfi_elig.DataBindings[0].FormatString = "d";
                    pfi_notice.DataBindings.Add("Text", _selectedData, "PFI_NOTE", true);
                    pfi_notice.DataBindings[0].FormatString = "d";

                    extension_dt.DataBindings.Add("Text", _selectedData, "CREATIONDATE", true);
                    extension_dt.DataBindings[0].FormatString = "g";
                    ext_due_date.DataBindings.Add("Text", _selectedData, "NEW_DUE", true);
                    ext_due_date.DataBindings[0].FormatString = "d";
                    ext_pfi_elig.DataBindings.Add("Text", _selectedData, "NEW_PFI", true);
                    ext_pfi_elig.DataBindings[0].FormatString = "d";

                // TODO: Need to determine source of this data
                    ext_pfi_notice.Text = string.Format("{0:d}", _selectedData[0]["NEW_PFI"]);
                            

                    user_id.DataBindings.Add("Text", _selectedData, "ENT_ID");
                    ext_amount.DataBindings.Add("Text", _selectedData, "REF_AMT", true);
                    ext_amount.DataBindings[0].FormatString = "c";
                }

                if (_theData.DefaultViewManager.DataViewSettings["PAWN_CUST"] != null)
                {
                    _theData.DefaultViewManager.DataViewSettings["PAWN_CUST"].RowFilter = "CUSTOMERNUMBER = '" +
                                                                                          _selectedData[0]["CUSTOMERNUMBER"] + "'";
                    DataRow customer = s.Tables["EXT_INFO"].Rows[rowIdx].GetParentRow("customerRelation");

                    cust_name.Text = customer["CUST_NAME"].ToString();
                    cust_dob.Text = customer["BIRTHDATE"].ToString();
                    cust_id.Text = customer["ID"].ToString();
                    cust_since.Text = string.Format("{0:d}", customer["SINCE"]);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("An Error Occurred on the Pawn Loan Extensions Detail Screen");

                //_theCriteria.errorLevel = (int)LogLevel.ERROR;
                //_theCriteria.errorMessage = string.Format("An error was detected in the {0} data retreived", errorType);
            }


            #endregion
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
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            var dInitDate = Convert.ToDateTime(string.Format("{0:d}", ShopDateTime.Instance.ShopDate));
            var stoNum = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;

            string fileName = @"extension_detail_report_" + dInitDate.Ticks + ".pdf";

            string rptDir =
                SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.
                BaseLogPath;

            var rpt = new ExtensionReport(
                rptDir + "\\" + fileName, stoNum,
                GlobalDataAccessor.Instance.CurrentSiteId.StoreName,
                dInitDate, "Loan Extension\nTicket # " + cur_loan_nr.Text
                );

            rpt.CreateReport(_theData);
            this.TopMost = false;
            System.Windows.Forms.Cursor.Current = Cursors.Default;
            DesktopSession.ShowPDFFile(rptDir + "\\" + fileName, false);
        }
    }
}
