using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Security;
using Pawn.Logic;
using Reports.Inquiry;

namespace Pawn.Forms.Inquiry.ExtInquiry
{
    public partial class ExtensionSearchResults : SearchResults
    {
        private System.Windows.Forms.DataGridViewTextBoxColumn Shop;
        private System.Windows.Forms.DataGridViewTextBoxColumn ticket;
        private System.Windows.Forms.DataGridViewTextBoxColumn cust_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn LnAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserID;

        public ExtensionSearchResults(DataSet s, PawnExtInquiry criteria, string dataTableName) :
            base(s, criteria, dataTableName)
        {
            #region Data Grid Initialization


            var dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            var dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();

            this.Shop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ticket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cust_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LnAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserID = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.resultsGrid_dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.Shop,
                this.ticket,
                this.cust_name,
                this.DateTime,
                this.LnAmount,
                this.UserID});

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
            this.cust_name.Width = 270;
            // 
            // DateTime
            // 
            this.DateTime.DataPropertyName = "CREATIONDATE";
            dataGridViewCellStyle3.Format = "g";
            dataGridViewCellStyle3.NullValue = null;
            this.DateTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.DateTime.HeaderText = "Extension Date/Time";
            this.DateTime.Name = "DateTime";
            this.DateTime.ReadOnly = true;
            this.DateTime.Width = 175;
            // 
            // LnAmount
            // 
            this.LnAmount.DataPropertyName = "REF_AMT";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.LnAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.LnAmount.HeaderText = "Extension Amount";
            this.LnAmount.Name = "LnAmount";
            this.LnAmount.ReadOnly = true;
            // 
            // UserID
            // 
            this.UserID.DataPropertyName = "ENT_ID";
            this.UserID.HeaderText = "UserID";
            this.UserID.Name = "UserID";
            this.UserID.ReadOnly = true;
            this.UserID.Width = 75;

            this.resultsGrid_dg.CellContentClick += resultsGrid_dg_CellContentClick;
            this.Print_btn.Enabled = true;
            this.Print_btn.Click += Print_btn_Click;
            #endregion


        }

        public ExtensionSearchResults() :base()
        {

        }

        protected void Print_btn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            var dInitDate = Convert.ToDateTime(string.Format("{0:d}", ShopDateTime.Instance.ShopDate));
            var stoNum = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;

            string fileName = @"extension_listing_report_" + dInitDate.Ticks + ".pdf";

            string rptDir =
                SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.
                BaseLogPath;

            var rpt = new ExtensionListingReport(
                rptDir + "\\" + fileName, stoNum,
                GlobalDataAccessor.Instance.CurrentSiteId.StoreName,
                dInitDate, "Extension Inquiry Listing \n\n" + criteriaSummary_txt.Text,
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

                Form details = new ExtensionDetails(key, _theData, e.RowIndex);
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
