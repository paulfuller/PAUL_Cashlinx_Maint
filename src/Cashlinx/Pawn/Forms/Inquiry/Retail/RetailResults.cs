using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using Common.Libraries.Utility.Shared;
using Reports.Inquiry;

namespace Pawn.Forms.Inquiry.Retail
{

    public partial class RetailSearchResults : SearchResults
    {

        private System.Windows.Forms.DataGridViewTextBoxColumn MSR;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserID;

        private DataSet ds;
        private String sortedByColumn;

        public RetailSearchResults(DataSet s, RetailInquiry criteria, string dataTableName)
            : base(s, criteria, dataTableName, "Records")
        {
            windowHeading_lb.Text = "Retail Inquiry List";

            #region Data Grid Initialization

            ds = s;
            sortedByColumn = criteria.sortBy.ToString();

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();

            this.MSR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserID = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.resultsGrid_dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.MSR,
                this.DateTime,
                this.SaleAmount,
                this.CostAmount,
                this.Status,
                this.UserID});

            // 
            // Shop
            // 
            this.MSR.DataPropertyName = "TICKET_NUMBER";
            this.MSR.HeaderText = "MSR";
            this.MSR.Name = "MSR";
            this.MSR.ReadOnly = true;
            this.MSR.Width = 125;
            // 
            // DateTime
            // 
            this.DateTime.DataPropertyName = "TIME_MADE";
            dataGridViewCellStyle3.Format = "g";
            dataGridViewCellStyle3.NullValue = null;
            this.DateTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.DateTime.HeaderText = "Date Made";
            this.DateTime.Name = "DateTime";
            this.DateTime.ReadOnly = true;
            this.DateTime.Width = 150;
            // 
            // SaleAmount
            // 
            this.SaleAmount.DataPropertyName = "AMOUNT";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.SaleAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.SaleAmount.HeaderText = "Sale Amount";
            this.SaleAmount.Name = "SaleAmount";
            this.SaleAmount.ReadOnly = true;
            this.SaleAmount.Width = 125;
            // 
            // CostAmount
            // 
            this.CostAmount.DataPropertyName = "COST";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.CostAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.CostAmount.HeaderText = "Cost Amount";
            this.CostAmount.Name = "CostAmount";
            this.CostAmount.ReadOnly = true;
            this.CostAmount.Width = 125;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "STATUS_CD";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 125;
            // 
            // UserID
            // 
            this.UserID.DataPropertyName = "ENT_ID";
            this.UserID.HeaderText = "UserID";
            this.UserID.Name = "UserID";
            this.UserID.ReadOnly = true;
            this.UserID.Width = 125;

            this.resultsGrid_dg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.resultsGrid_dg_CellContentClick);
            #endregion

            this.Print_btn.Enabled = true;
            this.Print_btn.Click += new System.EventHandler(this.Print_btn_Click);

        }

        protected void Print_btn_Click(object sender, EventArgs e)
        {
            var rpt = new RetailSaleListingReport(PdfLauncher.Instance);

            var reportObject = new ReportObject();

            reportObject.ReportTempFileFullName = "Retail Sale Listing";

            rpt.ReportObject = reportObject;

            var retailSaleListing = GetRetailSaleListing();

            rpt.CreateReport(sortedByColumn, retailSaleListing);
        
        }

        private List<ReportObject.RetailSaleListing> GetRetailSaleListing()
        {
            var retailSaleListingList  = new List<ReportObject.RetailSaleListing>();

            ReportObject.RetailSaleListing retailSaleListing;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                retailSaleListing = new ReportObject.RetailSaleListing()
                {
                    shopNumber = Convert.ToInt32(dr["storenumber"]),
                    ticketNumber = Convert.ToInt32(dr["ticket_number"]),
                    date = Convert.ToDateTime(dr["time_made"]),
                    saleAmount = Convert.ToDecimal(dr["amount"]),
                    cost = Convert.ToDecimal(dr["cost"]),
                    status = dr["status_cd"].ToString(),
                    entId = dr["ent_id"].ToString(),
                    userId = dr["createdby"].ToString(),
                    terminalId = dr["tty"].ToString(),
                    cashDrawer = dr["cashdrawer"].ToString(),
                    tax = Convert.ToDecimal(dr["tax"]),
                    createType = dr["create_type"].ToString(),
                    layawayTicketNumber = dr["layaway_ticket"].ToString() != String.Empty ? Convert.ToInt32(dr["layaway_ticket"]) : 0,
                };

                retailSaleListingList.Add(retailSaleListing);
            }

            return retailSaleListingList;
        }

        private void resultsGrid_dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                this.Visible = false;
                DataView sortedData = _theData.DefaultViewManager.CreateDataView(_theData.Tables["RETAIL_INFO"]);
                Form details = new RetailDetails(sortedData, e.RowIndex);
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
