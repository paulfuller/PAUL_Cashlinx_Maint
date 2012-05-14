using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Utility.Shared;
using Reports.Inquiry;

namespace Pawn.Forms.Inquiry.LoanInquiry
{
    public partial class LoanSearchResults : SearchResults
    {
        private System.Windows.Forms.DataGridViewTextBoxColumn Shop;
        private System.Windows.Forms.DataGridViewTextBoxColumn ticket;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn LnAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrinicipalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserID;

        public LoanSearchResults(DataSet s, PawnInquiry criteria, string dataTableName)
            : base(s, criteria, dataTableName)
        {
            #region Data Grid Initialization

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();

            //Display grid with current principal if partial payments are allowed.
            if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(GlobalDataAccessor.Instance.CurrentSiteId))
            {
                this.Shop = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.ticket = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.cust_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.DateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.LnAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.PrinicipalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.UserID = new System.Windows.Forms.DataGridViewTextBoxColumn();

                this.resultsGrid_dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                                                         {
                                                             this.Shop,
                                                             this.ticket,
                                                             this.cust_name,
                                                             this.DateTime,
                                                             this.LnAmount,
                                                             this.PrinicipalAmount,
                                                             this.Status,
                                                             this.UserID
                                                         });
            }
            else
            {
                this.Shop = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.ticket = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.cust_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.DateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.LnAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.UserID = new System.Windows.Forms.DataGridViewTextBoxColumn();

                this.resultsGrid_dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                                                         {
                                                             this.Shop,
                                                             this.ticket,
                                                             this.cust_name,
                                                             this.DateTime,
                                                             this.LnAmount,
                                                             this.Status,
                                                             this.UserID
                                                         });
            }

            // 
            // Shop
            // 
            this.Shop.DataPropertyName = "STORENUMBER";
            this.Shop.HeaderText = "Shop";
            this.Shop.Name = "Shop";
            this.Shop.ReadOnly = true;
            this.Shop.Width = 50;
            // 
            // ticket
            // 
            this.ticket.DataPropertyName = "TICKET_NUMBER";
            this.ticket.HeaderText = "Current Tkt #";
            this.ticket.Name = "ticket";
            this.ticket.ReadOnly = true;
            this.ticket.Width = 83;
            // 
            // cust_name
            // 
            this.cust_name.DataPropertyName = "CUST_NAME";
            this.cust_name.HeaderText = "Name";
            this.cust_name.Name = "cust_name";
            this.cust_name.ReadOnly = true;
            this.cust_name.Width = 170;
            // 
            // DateTime
            // 
            this.DateTime.DataPropertyName = "DATE_MADE";
            dataGridViewCellStyle3.Format = "g";
            dataGridViewCellStyle3.NullValue = null;
            this.DateTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.DateTime.HeaderText = "Made Date/Time";
            this.DateTime.Name = "DateTime";
            this.DateTime.ReadOnly = true;
            this.DateTime.Width = 115;
            // 
            // LnAmount
            // 
            this.LnAmount.DataPropertyName = "PRIN_AMOUNT";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.LnAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.LnAmount.HeaderText = "Loan Amount";
            this.LnAmount.Name = "LnAmount";
            this.LnAmount.ReadOnly = true;
            this.LnAmount.Width = 90;
            // 
            // Current Prin. Amount
            // 
            //Only show current principal column if partial payments are allowed.
            if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(GlobalDataAccessor.Instance.CurrentSiteId))
            {
                this.PrinicipalAmount.DataPropertyName = "PartPymtPrinAmt";
                dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                dataGridViewCellStyle5.Format = "C2";
                dataGridViewCellStyle5.NullValue = null;
                this.PrinicipalAmount.DefaultCellStyle = dataGridViewCellStyle5;
                this.PrinicipalAmount.HeaderText = "Current Prin. Amt";
                this.PrinicipalAmount.Name = "PrinAmt";
                this.PrinicipalAmount.ReadOnly = true;
                this.PrinicipalAmount.Width = 100;
            }
            // 
            // Status
            // 
            this.Status.DataPropertyName = "STATUS_CD";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 65;
            // 
            // UserID
            // 
            this.UserID.DataPropertyName = "ENT_ID";
            this.UserID.HeaderText = "UserID";
            this.UserID.Name = "UserID";
            this.UserID.ReadOnly = true;
            this.UserID.Width = 65;

            this.resultsGrid_dg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.resultsGrid_dg_CellContentClick);
            #endregion

            if (criteria.byDate)
            {
                resultsGrid_dg.Columns[4].HeaderText =
                    StringDBMap_Enum<PawnInquiry.searchDateType_enum>.displayValue(criteria.dateType) +
                    " Date / Time";
                resultsGrid_dg.Columns[4].DataPropertyName =
                    StringDBMap_Enum<PawnInquiry.searchDateType_enum>.toDBValue(criteria.dateType);
            }
            else
            {
                resultsGrid_dg.Columns[2].HeaderText =
                    StringDBMap_Enum<PawnInquiry.searchTicketType_enum>.displayValue(criteria.ticketType) +
                    " Tkt #";
                resultsGrid_dg.Columns[2].DataPropertyName =
                    StringDBMap_Enum<PawnInquiry.searchTicketType_enum>.toDBValue(criteria.ticketType);

            }

            this.Print_btn.Enabled = true;
            this.Print_btn.Click += new System.EventHandler(this.Print_btn_Click);
        }

        protected void Print_btn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

            var dInitDate = Convert.ToDateTime(string.Format("{0:d}", ShopDateTime.Instance.ShopDate));
            var stoNum = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;

            string fileName = @"loan_listing_report_" + dInitDate.Ticks + ".pdf";

            string rptDir =
                SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.
                BaseLogPath;

            LoanListingReport rpt = new LoanListingReport(
                rptDir + "\\" + fileName, stoNum,
                GlobalDataAccessor.Instance.CurrentSiteId.StoreName,
                dInitDate, "Loan Listing \n\n" + criteriaSummary_txt.Text,
                 _theCriteria.sortByField()
                );

            rpt.CreateReport(_theData);
            this.TopMost = false;
            System.Windows.Forms.Cursor.Current = Cursors.Default;
            DesktopSession.ShowPDFFile(rptDir + "\\" + fileName, false);
        }

        private void resultsGrid_dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                this.Visible = false;
                int key = (int)((System.Windows.Forms.DataGridView)(sender)).CurrentRow.Cells[2].Value;
                if (((System.Windows.Forms.DataGridView)(sender)).SortedColumn != null)
                {
                    _theData.DefaultViewManager.DataViewSettings["PAWN_INFO"].Sort =
                        ((System.Windows.Forms.DataGridView)(sender)).SortedColumn.DataPropertyName;
                }

                DataView sortedData = _theData.DefaultViewManager.CreateDataView(_theData.Tables["PAWN_INFO"]);
                Form details = new LoanDetails(key, sortedData, e.RowIndex);
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
    }



}
