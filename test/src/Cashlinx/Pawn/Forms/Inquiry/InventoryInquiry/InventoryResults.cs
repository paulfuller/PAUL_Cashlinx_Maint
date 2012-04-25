using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Security;
using Pawn.Forms.Inquiry.LoanInquiry;
using Pawn.Logic;
using Reports.Inquiry;

namespace Pawn.Forms.Inquiry.InventoryInquiry
{

    public partial class InventorySearchResults : SearchResults
    {
        private System.Windows.Forms.DataGridViewTextBoxColumn shortCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ICN;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusDt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn RetailPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Aisle;
        private System.Windows.Forms.DataGridViewTextBoxColumn Shelf;
        // warning fix
        private new System.Windows.Forms.DataGridViewTextBoxColumn Location;


        public InventorySearchResults(DataSet s, Inquiry criteria, string dataTableName)
            : base(s, criteria, dataTableName, "Items")
        {
            #region Data Grid Initialization
            windowHeading_lb.Text = "Inventory Inquiry Search Results";
            searchCriteria_lb.Text = "Inventory Search Criteria";


            //NrLoans_tb.Text = "0 Items";
            //Print_btn.Left += 200;
            //Cancel_btn.Left += 200;

            criteriaDetails_panel.Width += 200;
            this.Width += 200;
            this.resultsGrid_dg.Width += 200;


            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();

            this.shortCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ICN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusDt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RetailPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Aisle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Shelf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();


            resultsGrid_dg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            resultsGrid_dg.ShowCellToolTips = true;

            this.resultsGrid_dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.shortCode,
                this.StatusDt,
                this.Category,
                this.Descr,
                this.Status,
                this.ItemCost,
                this.RetailPrice,
                this.Aisle,
                this.Shelf,
                this.Location, 
                this.ICN});

            // 
            // shortCode
            // 
            this.shortCode.DataPropertyName = "shortCode";
            this.shortCode.HeaderText = "ICN";
            this.shortCode.Name = "shortCode";
            this.shortCode.ReadOnly = true;
            this.shortCode.Width = 75;

            // 
            // ICN
            // 
            this.ICN.DataPropertyName = "ICN";
            this.ICN.HeaderText = "ICN";
            this.ICN.Name = "ICN";
            this.ICN.Visible = false;
            // 
            // Status Date
            // 
            this.StatusDt.DataPropertyName = "STATUS_TIME";
            dataGridViewCellStyle3.Format = "g";
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.StatusDt.DefaultCellStyle = dataGridViewCellStyle3;
            this.StatusDt.HeaderText = "Status Date";
            this.StatusDt.Name = "statusDt";
            this.StatusDt.ReadOnly = true;
            this.StatusDt.Width = 125;
            // 
            // Category
            // 
            this.Category.DataPropertyName = "CAT_CODE";
            this.Category.HeaderText = "Cateogry";
            this.Category.Name = "cat_cd";
            this.Category.ReadOnly = true;
            this.Category.Width = 75;
            // 
            // Descr
            // 
            this.Descr.DataPropertyName = "MD_DESC";
            this.Descr.HeaderText = "Description";
            this.Descr.Name = "descr";
            this.Descr.ReadOnly = true;
            this.Descr.Width = 250;
            Descr.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "STATUS_CD";
            this.Status.HeaderText = "Status";
            this.Status.Name = "status";
            this.Status.ReadOnly = true;
            this.Status.Width = 50;
            // 
            // ItemCost
            // 
            this.ItemCost.DataPropertyName = "ITEM_AMT";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;

            this.ItemCost.DefaultCellStyle = dataGridViewCellStyle4;
            this.ItemCost.HeaderText = "Item Cost";
            this.ItemCost.Name = "cost";
            this.ItemCost.ReadOnly = true;
            this.ItemCost.Width = 75;
            // 
            // RetailPrice
            // 
            this.RetailPrice.DataPropertyName = "RETAIL_PRICE";
            this.RetailPrice.DefaultCellStyle = dataGridViewCellStyle4;
            this.RetailPrice.HeaderText = "Retail Price";
            this.RetailPrice.Name = "price";
            this.RetailPrice.ReadOnly = true;
            this.RetailPrice.Width = 75;



            // 
            // Aisle
            // 
            this.Aisle.DataPropertyName = "LOC_AISLE";
            this.Aisle.HeaderText = "Aisle";
            this.Aisle.Name = "aisle";
            this.Aisle.ReadOnly = true;
            this.Aisle.Width = 50;
            // 
            // Shelf
            // 
            this.Shelf.DataPropertyName = "LOC_SHELF";
            this.Shelf.HeaderText = "Shelf";
            this.Shelf.Name = "shelf";
            this.Shelf.ReadOnly = true;
            this.Shelf.Width = 50;
            // 
            // Other
            // 
            this.Location.DataPropertyName = "LOCATION";
            this.Location.HeaderText = "Other";
            this.Location.Name = "location";
            this.Location.ReadOnly = true;
            this.Location.Width = 100;

            this.resultsGrid_dg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.resultsGrid_dg_CellContentClick);
            this.resultsGrid_dg.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(resultsGrid_dg_CellContentMouseOver);


            #endregion


            this.Print_btn.Enabled = true;
            this.Print_btn.Click += new System.EventHandler(this.Print_btn_Click);

        }

        protected void Print_btn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            var dInitDate = Convert.ToDateTime(string.Format("{0:d}", ShopDateTime.Instance.ShopDate));
            var stoNum = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;

            //string fileName = @"mdse_listing_report_" + dInitDate.Ticks + ".pdf";

            string rptDir =
                SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.
                BaseLogPath;

            var reportFilterSelect = new ReportType();
            this.TopMost = false;
            reportFilterSelect.ShowDialog();

            if (reportFilterSelect.DialogResult == DialogResult.OK)
            {
                string[] filters = reportFilterSelect.getFilters();
                string[] filterNames = reportFilterSelect.getFilterNames();

                if (filters.GetLength(0) > 0)
                {
                    for (int i = 0; i < filters.GetLength(0); i++)
                    {
                        string fileName = @"mdse_listing_report_" + i + "_" + dInitDate.Ticks + ".pdf";
                        string filter = filters[i];
                        _theData.Tables["MDSE"].DefaultView.RowFilter = filter;

                        MdseListingReport rpt = new MdseListingReport(
                            rptDir + "\\" + fileName, stoNum,
                            GlobalDataAccessor.Instance.CurrentSiteId.StoreName,
                            dInitDate, "Inventory Listing: " + filterNames[i] + "\n\n" + ((InventoryInquiries)_theCriteria).ToString(80),
                             _theCriteria.sortByField(), _theCriteria.sortByName()
                            );

                        rpt.CreateReport(_theData.Tables["MDSE"].DefaultView);

                        System.Windows.Forms.Cursor.Current = Cursors.Default;
                        DesktopSession.ShowPDFFile(rptDir + "\\" + fileName, false);

                        _theData.Tables["MDSE"].DefaultView.RowFilter = "";
                    }

                }
                else
                {
                    MessageBox.Show("No Report Type Selected");
                }
            }
            else
            {
                MessageBox.Show("No Report Type Selected");
            }
        }

        private void resultsGrid_dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                //this.Visible = false;
                //if (sender != null)
                //{
                //    string key = ((DataGridView)(sender)).CurrentRow.Cells["ICN"].Value.ToString();
                //    _theData.Tables["MDSE"].DefaultView.RowFilter = "ICN='" + key + "'";
                //}

                DataView dv = _theData.DefaultViewManager.CreateDataView(_theData.Tables["MDSE"]);
                dv.Sort = _theData.Tables["MDSE"].DefaultView.Sort;

                Form details = new ItemDetails(dv, e.RowIndex);
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

        private void resultsGrid_dg_CellContentMouseOver(object sender, DataGridViewCellToolTipTextNeededEventArgs args)
        {
            //args.ToolTipText = "test";
            //((DataGridView)(sender)).CurrentCell.ToolTipText = args.ToolTipText;
            //((DataGridView)sender).ShowCellToolTips = true;
            if (args.RowIndex >= 0)
            {
                try
                {
                    if (args.ToolTipText == "")
                    {
                        //args.ToolTipText =
                        //        ((System.Windows.Forms.DataGridView)(sender)).Rows[args.RowIndex].Cells["ICN"].Value.ToString();

                        //((DataGridView)(sender)).CurrentCell.ToolTipText = args.ToolTipText;

                        switch (args.ColumnIndex)
                        {
                            case 1:
                                args.ToolTipText =
                                        ((System.Windows.Forms.DataGridView)(sender)).Rows[args.RowIndex].Cells["ICN"].Value.ToString();

                                ((DataGridView)(sender)).CurrentCell.ToolTipText = args.ToolTipText;
                                break;

                            case 4:
                            case 10:
                                args.ToolTipText = ((System.Windows.Forms.DataGridView)(sender)).Rows[args.RowIndex].Cells[args.ColumnIndex].Value.ToString();

                                ((DataGridView)(sender)).CurrentCell.ToolTipText = args.ToolTipText;
                                break;
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception occurred accessing ICN Column");
                }
            }
        }
    }

}
