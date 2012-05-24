using Common.Controllers.Security;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System;
using System.Data;
using System.Windows.Forms;
using Common.Libraries.Utility.Shared;
using Reports.Inquiry;

namespace Pawn.Forms.Inquiry.PartialPaymentInquiry
{
    public partial class PartialPaymentSearchResults : SearchResults
    {
        private System.Windows.Forms.DataGridViewTextBoxColumn LoanNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoanDateMade;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOB;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoanAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentPrincipalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PPDatePaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn PPAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PPStatus;

        public PartialPaymentSearchResults(DataSet s, Inquiry criteria, string dataTableName)
            : base(s, criteria, dataTableName, "Items")
        {
            InitializeComponent();

            #region Data Grid Initialization
            windowHeading_lb.Text = "Partial Payments Inquiry Search Results";
            searchCriteria_lb.Text = "Partial Payments Search Criteria";

            criteriaDetails_panel.Width += 200;
            this.Width += 200;
            this.resultsGrid_dg.Width += 200;

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();

            
            this.LoanNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoanDateMade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoanAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentPrincipalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PPDatePaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PPAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PPStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();

            resultsGrid_dg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //resultsGrid_dg.ShowCellToolTips = true;

            this.resultsGrid_dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.LoanNumber,
                this.LoanDateMade,
                this.CustomerName,
                this.DOB,
                this.LoanAmount,
                this.CurrentPrincipalAmount,
                this.PPDatePaid,
                this.PPAmount,
                this.PPStatus});

            // 
            // LoanNumber
            // 
            this.LoanNumber.DataPropertyName = "TICKET_NUMBER";
            this.LoanNumber.HeaderText = "Loan #";
            this.LoanNumber.Name = "TICKET_NUMBER";
            this.LoanNumber.ReadOnly = true;
            this.LoanNumber.Width = 75;

            // 
            // LoanDateMade
            // 
            this.LoanDateMade.DataPropertyName = "DATE_MADE";
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.LoanDateMade.DefaultCellStyle = dataGridViewCellStyle3;
            this.LoanDateMade.HeaderText = "Loan Date Made";
            this.LoanDateMade.Name = "DATE_MADE";
            this.LoanDateMade.ReadOnly = true;
            this.LoanDateMade.Width = 80;
            //this.TransferType.Visible = false;

            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "cust_name";
            this.CustomerName.HeaderText = "Customer Name";
            this.CustomerName.Name = "cust_name";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Width = 150;

            // 
            // DOB
            // 
            this.DOB.DataPropertyName = "birthdate";
            dataGridViewCellStyle4.Format = "g";
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.DOB.DefaultCellStyle = dataGridViewCellStyle4;
            this.DOB.HeaderText = "Date of Birth";
            this.DOB.Name = "birthdate";
            this.DOB.ReadOnly = true;
            this.DOB.Width = 75;
            //this.TransferType.Visible = false;


            // 
            // LoanAmount
            // 
            this.LoanAmount.DataPropertyName = "LOAN_AMOUNT";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "C2";
            dataGridViewCellStyle5.NullValue = null;
            this.LoanAmount.DefaultCellStyle = dataGridViewCellStyle5;
            this.LoanAmount.HeaderText = "Loan Amount";
            this.LoanAmount.Name = "LOAN_AMOUNT";
            this.LoanAmount.ReadOnly = true;
            this.LoanAmount.Width = 100;

            // 
            // CurrentPrincipalAmount
            // 
            this.CurrentPrincipalAmount.DataPropertyName = "CURRENT_PRIN_AMOUNT";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = "C2";
            dataGridViewCellStyle6.NullValue = null;
            this.CurrentPrincipalAmount.DefaultCellStyle = dataGridViewCellStyle6;
            this.CurrentPrincipalAmount.HeaderText = "Current Principal Amount";
            this.CurrentPrincipalAmount.Name = "CURRENT_PRIN_AMOUNT";
            this.CurrentPrincipalAmount.ReadOnly = true;
            this.CurrentPrincipalAmount.Width = 130;
            //this.TransferType.Visible = false;

            // 
            // PPDatePaid
            // 
            this.PPDatePaid.DataPropertyName = "PP_DATE";
            dataGridViewCellStyle7.Format = "d";
            dataGridViewCellStyle7.NullValue = null;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.PPDatePaid.DefaultCellStyle = dataGridViewCellStyle7;
            this.PPDatePaid.HeaderText = "Partial Payment Date Paid";
            this.PPDatePaid.Name = "PP_DATE";
            this.PPDatePaid.ReadOnly = true;
            this.PPDatePaid.Width = 140;
            //this.TransferType.Visible = false;

            // 
            // PPAmount
            // 
            this.PPAmount.DataPropertyName = "PP_AMOUNT";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.Format = "C2";
            dataGridViewCellStyle8.NullValue = null;
            this.PPAmount.DefaultCellStyle = dataGridViewCellStyle8;
            this.PPAmount.HeaderText = "Partial Payment Amount";
            this.PPAmount.Name = "PP_AMOUNT";
            this.PPAmount.ReadOnly = true;
            this.PPAmount.Width = 135;
            //this.TransferType.Visible = false;

            // 
            // PPStatus
            // 
            this.PPStatus.DataPropertyName = "status_cd";
            this.PPStatus.HeaderText = "Status";
            this.PPStatus.Name = "status_cd";
            this.PPStatus.ReadOnly = true;
            this.PPStatus.Width = 55;

            this.resultsGrid_dg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.resultsGrid_dg_CellContentClick);
            this.resultsGrid_dg.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(resultsGrid_dg_CellContentMouseOver);

            #endregion

            this.Print_btn.Enabled = true;
            this.Print_btn.Click += new System.EventHandler(this.Print_btn_Click);
        }

        protected void Print_btn_Click(object sender, EventArgs e)
        {
            string rptDir = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;

            var ro = new ReportObject();

            //var report = new Reports.Inquiry.PartialPaymentListingReport(ro.ReportTitle, ro.ReportStore, ro.ReportStoreDesc, ro.ReportTitle, string.Empty, string.Empty);
            var report = new Reports.Inquiry.PartialPaymentListingReport(PdfLauncher.Instance);
            ro.CreateTemporaryFullName();
            ro.ReportTempFileFullName = rptDir + ro.ReportTempFileFullName;
            ro.ReportTitle = "Partial Payment Listing";
            ro.ReportStore = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            ro.ReportStoreDesc = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
            ro.ReportSort = "USERID";
            report.reportObject = ro;

            var dv = _theData.DefaultViewManager.CreateDataView(_theData.Tables[0]);
            var isSuccessful = report.CreateReport(_theData.Tables[0]);

            DesktopSession.ShowPDFFile(report.reportObject.ReportTempFileFullName, false);
            this.TopMost = false;
        }

        private void resultsGrid_dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                this.Visible = false;
                
                Form details = null;

                // PARTIAL_PAYMENT_INQ
                
                string loanTicketNumber = resultsGrid_dg.Rows[e.RowIndex].Cells["TICKET_NUMBER"].Value.ToString();
                int iLoanTicketNumber = 0;

                if (!int.TryParse(loanTicketNumber, out iLoanTicketNumber))
                {
                    // Invalid Data
                    return;
                }

                DataView sortedData = _theData.DefaultViewManager.CreateDataView(((DataTable)resultsGrid_dg.DataSource));

                details = new PartialPaymentDetails(iLoanTicketNumber, sortedData, e.RowIndex);

                if (details != null)
                {

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
                else
                {
                    this.Visible = true;
                }
            }
        }

        private void resultsGrid_dg_CellContentMouseOver(object sender, DataGridViewCellToolTipTextNeededEventArgs args)
        {
            //if (args.RowIndex >= 0)
            //{
            //    try
            //    {
            //        if (args.ToolTipText == "")
            //        {
            //            //args.ToolTipText =
            //            //        ((System.Windows.Forms.DataGridView)(sender)).Rows[args.RowIndex].Cells["ICN"].Value.ToString();

            //            //((DataGridView)(sender)).CurrentCell.ToolTipText = args.ToolTipText;

            //            switch (args.ColumnIndex)
            //            {
            //                case 1:
            //                    args.ToolTipText =
            //                            ((System.Windows.Forms.DataGridView)(sender)).Rows[args.RowIndex].Cells["TRANSFERNUMBER"].Value.ToString();

            //                    ((DataGridView)(sender)).CurrentCell.ToolTipText = args.ToolTipText;
            //                    break;

            //                case 4:
            //                case 10:
            //                    args.ToolTipText = ((System.Windows.Forms.DataGridView)(sender)).Rows[args.RowIndex].Cells[args.ColumnIndex].Value.ToString();

            //                    ((DataGridView)(sender)).CurrentCell.ToolTipText = args.ToolTipText;
            //                    break;
            //            }

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Exception occurred accessing Transfer Number Column");
            //    }
            //}
        }
    }
}
