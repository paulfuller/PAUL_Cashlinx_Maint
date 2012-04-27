using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Business;

namespace Pawn.Forms.Pawn.Services.MerchandiseTransfer
{
    public partial class SelectDuplicateTransferItems : CustomBaseForm
    {
        private List<TransferItemVO> SearchItems { get; set; }
        public TransferItemVO SelectedItem { get; set; }

        public string ErrorMessage
        {
            get { return lblError.Text; }
            set { lblError.Text = value; }
        }

        public SelectDuplicateTransferItems(List<TransferItemVO> searchItems)
        {
            InitializeComponent();
            SearchItems = searchItems;
            Setup();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            SelectedItem = null;
        }

        private void Setup()
        {
            foreach (TransferItemVO item in SearchItems)
            {
                DataGridViewRow myRow = gvMerchandise.Rows.AddNew();
                myRow.Cells[colICN.Name].Value = item.ICN;
                myRow.Cells[colMerchandiseDescription.Name].Value = item.MdseRecordDesc;
                string itemStatus = item.ItemStatus.ToString();
                //if (!string.IsNullOrEmpty(item.HoldType))
                //    itemStatus = item.ItemStatus + "-" + item.HoldType;
                //else
                //    itemStatus = item.ItemStatus.ToString();
                myRow.Cells[colStatus.Name].Value = itemStatus;
                myRow.Cells[colCost.Name].Value = item.PfiAmount.ToString("c");
                //if (ResultsMode == ItemSearchResultsMode.RETAIL_SALE)
                //{
                //    if (item.ItemStatus != ProductStatus.PFI)
                //    {
                //        myRow.DefaultCellStyle.BackColor = Color.Gray;
                //        myRow.ReadOnly = true;
                //    }
                //    if (item.HoldType == HoldTypes.POLICEHOLD.ToString())
                //    {
                //        if (SearchItems.Count == 1)
                //            lblError.Text = "The item number entered is not eligible for retail sale or layaway.";
                //        myRow.DefaultCellStyle.BackColor = Color.Gray;
                //        myRow.ReadOnly = true;
                //    }
                //}
                myRow.Tag = item;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            SelectedItem = null;
            this.Close();           
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            DataGridViewRow r = gvMerchandise.SelectedRows[0];
            SelectedItem = r.Tag as TransferItemVO;
            this.Close();
        }

        private void gvMerchandise_SelectionChanged(object sender, EventArgs e)
        {
        }
    }
}
