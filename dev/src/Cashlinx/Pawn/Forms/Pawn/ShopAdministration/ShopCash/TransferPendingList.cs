using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Utility;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.ShopAdministration.ShopCash
{
    public partial class TransferPendingList : Form
    {
        public TransferPendingList()
        {
            InitializeComponent();
        }

        private void TransferPendingList_Load(object sender, EventArgs e)
        {
            DataTable storeTransferData;
            string errorCode;
            string errorText;
            string currentStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            bool retVal = ShopCashProcedures.GetShopTransfers(currentStoreNumber,GlobalDataAccessor.Instance.DesktopSession,
                out storeTransferData, out errorCode, out errorText);
            if (retVal)
            {
                if (storeTransferData.Rows.Count > 0)
                {
                    BindingSource bindingSource1 = new BindingSource
                    {
                        DataSource = storeTransferData
                    };
                    dataGridViewTransfers.AutoGenerateColumns = false;
                    dataGridViewTransfers.DataSource = bindingSource1;
                    dataGridViewTransfers.Columns[1].DataPropertyName = "transfernumber";
                    dataGridViewTransfers.Columns[2].DataPropertyName = "transferdate";
                    dataGridViewTransfers.Columns[3].DataPropertyName = "transferfrom";
                    dataGridViewTransfers.Columns[4].DataPropertyName = "transferamount";
                    dataGridViewTransfers.Columns[5].DataPropertyName = "transferstatus";
                    dataGridViewTransfers.Columns[6].DataPropertyName = "shoptransferid";
                }
                else
                {
                    MessageBox.Show(@"There are no pending cash transfers at this time");
                    this.Close();
                }
            }
        }

        private void customButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

 

        private void dataGridViewTransfers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //If the view button is clicked
            if (e.ColumnIndex == 0)
            {
                var selectedTransferNumber = Utilities.GetIntegerValue(dataGridViewTransfers.Rows[e.RowIndex].Cells[1].Value,0);
                var selectedTransferID = Utilities.GetStringValue(dataGridViewTransfers.Rows[e.RowIndex].Cells[6].Value);
                if (selectedTransferNumber > 0)
                {
                    GlobalDataAccessor.Instance.DesktopSession.SelectedStoreCashTransferNumber = selectedTransferNumber;
                    GlobalDataAccessor.Instance.DesktopSession.SelectedStoreCashTransferID = selectedTransferID;
                    Close();
                    
                }
            }
        }
    }
}
