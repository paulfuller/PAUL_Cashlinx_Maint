using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Components.EventArgs;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Forms.Retail
{
    public partial class SelectItems : CustomBaseForm
    {
        public bool AllowSubitemSelection { get; set; }
        public ItemSearchResultsMode ResultsMode { get; set; }
        private List<RetailItem> SearchItems { get; set; }
        public RetailItem SelectedItem { get; set; }
        public bool ShowAsIneligibleSingleItem { get; set; }
        public delegate void ShowDescMdse();
        public event ShowDescMdse ShowDescMerchandise;

        public string ErrorMessage
        {
            get { return lblError.Text; }
            set { lblError.Text = value; }
        }

        public SelectItems(List<RetailItem> searchItems, ItemSearchResultsMode mode)
            : this(searchItems, false, mode)
        {
        }

        public SelectItems(List<RetailItem> searchItems, bool showAsIneligibleSingleItem, ItemSearchResultsMode mode)
        {
            InitializeComponent();
            ResultsMode = mode;
            AllowSubitemSelection = ResultsMode == ItemSearchResultsMode.RETAIL_SALE || ResultsMode == ItemSearchResultsMode.REPRINT_TAG;
            lblError.Text = string.Empty;
            SearchItems = searchItems;
            ShowAsIneligibleSingleItem = showAsIneligibleSingleItem;
            btnTempICN.Visible = mode != ItemSearchResultsMode.CHANGE_ITEM_ASSIGNMENT_TYPE;
            Setup();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            SelectedItem = null;
        }

        private void Setup()
        {
            //gvMerchandise.AutoGenerateColumns = false;
            //gvMerchandise.DataSource = SearchItems;
            foreach (RetailItem item in SearchItems)
            {
                int gvIdx = gvMerchandise.Rows.Add();
                DataGridViewRow myRow = gvMerchandise.Rows[gvIdx];
                //string icn = i.Icn;
                myRow.Cells[colICN.Name].Value = item.Icn;
                myRow.Cells[colMerchandiseDescription.Name].Value = item.TicketDescription;
                string itemStatus;
                if (!string.IsNullOrEmpty(item.HoldType))
                    itemStatus = item.ItemStatus + "-" + item.HoldType;
                else
                    itemStatus = item.ItemStatus.ToString();
                myRow.Cells[colStatus.Name].Value = itemStatus;
                myRow.Cells[colRetailPrice.Name].Value = item.RetailPrice.ToString("c");
                Icn icn = new Icn(item.Icn);
                if (ResultsMode == ItemSearchResultsMode.RETAIL_SALE || ResultsMode == ItemSearchResultsMode.CHARGEOFF)
                {
                    if (item.ItemStatus != ProductStatus.PFI)
                    {
                        myRow.DefaultCellStyle.BackColor = Color.Gray;
                        myRow.ReadOnly = true;
                    }
                    if (item.HoldType == HoldTypes.POLICEHOLD.ToString())
                    {
                        if (SearchItems.Count == 1 && ResultsMode == ItemSearchResultsMode.RETAIL_SALE)
                            lblError.Text = "The item number entered is not eligible for retail sale or layaway.";
                        myRow.DefaultCellStyle.BackColor = Color.Gray;
                        myRow.ReadOnly = true;
                    }
                }

                if (!AllowSubitemSelection)
                {
                    if (icn.DocumentType == DocumentType.CaccItem || icn.DocumentType == DocumentType.NxtAndStandardDescriptor || item.HoldType == HoldTypes.POLICEHOLD.ToString() || icn.SubItemNumber != 0)
                    {
                        myRow.DefaultCellStyle.BackColor = Color.Gray;
                        myRow.ReadOnly = true;
                    }
                }
                myRow.Tag = item;
            }

            if (ResultsMode == ItemSearchResultsMode.RETAIL_SALE && ShowAsIneligibleSingleItem)
            {
                continueButton.Visible = false;
                cancelButton.Location = continueButton.Location;
                cancelButton.Text = "OK";
                titleLabel.Text = "Item Search Result";
            }

            btnTempICN.Visible = ResultsMode == ItemSearchResultsMode.RETAIL_SALE;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            SelectedItem = null;
            this.Close();           
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            if (gvMerchandise.SelectedRows.Count == 0)
            {
                return;
            }

            DataGridViewRow r = gvMerchandise.SelectedRows[0];
            SelectedItem = r.Tag as RetailItem;
            this.Close();
        }

        private void gvMerchandise_SelectionChanged(object sender, EventArgs e)
        {
            if (gvMerchandise.SelectedRows.Count == 0)
            {
                return;
            }

            DataGridViewRow r = gvMerchandise.SelectedRows[0];
            RetailItem item = r.Tag as RetailItem;
            if (ResultsMode == ItemSearchResultsMode.RETAIL_SALE && item.ItemStatus != ProductStatus.PFI)
            {
                r.Selected = false;
            }
        }

        private void btnTempICN_Click(object sender, EventArgs e)
        {
            this.Close();
            ShowDescMerchandise();
        }

        private void gvMerchandise_GridViewRowSelecting(object sender, GridViewRowSelectingEventArgs e)
        {
            if (e.Row.DefaultCellStyle.BackColor == Color.Gray)
            {
                e.Cancel = true;
            }
        }

    }
}
