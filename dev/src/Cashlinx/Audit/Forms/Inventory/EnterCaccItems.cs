using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Libraries.Objects;
using Common.Libraries.Utility;

namespace Audit.Forms.Inventory
{
    public partial class EnterCaccItems : AuditWindowBase
    {
        public EnterCaccItems()
        {
            InitializeComponent();
            DataSet data;
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            bool dataReturned = InventoryAuditProcedures.GetCACCInfo(ADS.ActiveAudit, dataContext, out data);

            gvItems.AutoGenerateColumns = false;
            gvItems.Columns["colShortCode"].DataPropertyName = "CAT_CODE";
            gvItems.Columns["colCategory"].DataPropertyName = "CAT_DESC";

            gvItems.Columns["colPreQty"].DataPropertyName = "ORG_QTY";
            gvItems.Columns["ttl_cost"].DataPropertyName = "ORG_COST";
            gvItems.Columns["colPreQty"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter;
            gvItems.Columns["colPreAvg"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter;
            gvItems.Columns["colPostQty"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter;
            gvItems.Columns["colPostQty"].DataPropertyName = "NEW_QTY";
            gvItems.Columns["colPostAvgCost"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter;
            
            var bs = new BindingSource();
            
            bs.DataSource = data.Tables["CACC_summary"];
            gvItems.DataSource = bs;

            btnUndo.Enabled = false;

        }

        private void gvItems_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            //TODO: update the cacc counts in database
            CommonDatabaseContext dataContext = CreateCommonDatabaseContext();
            int? nrCD, nrVideo, nrGames, nrPremGame, nrDVD;

            if (gvItems.CurrentCell.IsInEditMode)
                gvItems.EndEdit();

            nrCD = Utilities.GetNullableIntegerValue(gvItems.Rows[0].Cells["colActualQty"].Value.ToString(), null);
            nrVideo = Utilities.GetNullableIntegerValue(gvItems.Rows[1].Cells["colActualQty"].Value.ToString(), null);
            nrGames = Utilities.GetNullableIntegerValue(gvItems.Rows[2].Cells["colActualQty"].Value.ToString(), null);
            nrPremGame = Utilities.GetNullableIntegerValue(gvItems.Rows[3].Cells["colActualQty"].Value.ToString(), null);
            nrDVD = Utilities.GetNullableIntegerValue(gvItems.Rows[4].Cells["colActualQty"].Value.ToString(), null);

            bool retval = InventoryAuditProcedures.persistCACCCounts(dataContext, ADS.ActiveAudit.StoreNumber, ADS.ActiveAudit.AuditId,
                GetWritableValue(nrCD), GetWritableValue(nrVideo), GetWritableValue(nrGames), GetWritableValue(nrPremGame), GetWritableValue(nrDVD));
            NavControlBox.Action = NavBox.NavAction.SUBMIT;
        }

        private string GetWritableValue(int? value)
        {
            if (!value.HasValue)
            {
                return null;
            }

            return value.Value.ToString();
        }

        struct undoItem
        {
            public int rowID;
            public string oldCount;
        }

        Stack<undoItem> undoStack = new Stack<undoItem>();
        bool isUndo = false;

        private void gvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
                {
                if(Utilities.GetIntegerValue(gvItems.Rows[e.RowIndex].Cells["colActualQty"].Value, -1) >= 0)
                {
                    gvItems.Rows[e.RowIndex].Cells["colPostQty"].Value = gvItems.Rows[e.RowIndex].Cells["colActualQty"].Value;
                    
                    calculateFields(e.RowIndex);

                    btnUndo.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Value Entered must be an integer, greater than or equal to 0");
                    undoStack.Pop();
                }
            }        
        }

        private int paintingRow = -1;

        private void gvItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (paintingRow != 99 && paintingRow != e.RowIndex)
            {
                paintingRow = e.RowIndex;

                calculateFields(e.RowIndex);                
            }

        }

        private void calculateFields (int RowIndex)
        {

            string sTtlCost = gvItems.Rows[RowIndex].Cells["ttl_cost"].Value.ToString();
            string sPreQty = gvItems.Rows[RowIndex].Cells["colPreQty"].Value.ToString();
            string sActlQty = gvItems.Rows[RowIndex].Cells["colPostQty"].Value.ToString();
            decimal dpreAvgCost = 0, dTtlCost = 0, dpostAvgCost = 0;
            int dPreQty = 0;
            int dActlQty = 0;

            bool hasValue = decimal.TryParse(sTtlCost, out dTtlCost);

            if (hasValue)
            {
                int.TryParse(sPreQty, out dPreQty);
                int.TryParse(sActlQty, out dActlQty);
            }

            dpreAvgCost = Math.Round(dTtlCost / ((dPreQty == 0) ? 1 : dPreQty), 2);
            gvItems.Rows[RowIndex].Cells["colPreAvg"].Value = string.Format("{0:f}", dpreAvgCost);
            gvItems.Rows[RowIndex].Cells["colActualQty"].Value = gvItems.Rows[RowIndex].Cells["colPostQty"].Value.ToString();

            int dDelta = dPreQty - dActlQty;
            decimal newPFI = Math.Round(dDelta * dpreAvgCost, 2);



            //if (dActlQty > dPreQty )
                dpostAvgCost = Math.Round((dTtlCost - newPFI) / ((dActlQty == 0) ? 1 : dActlQty), 2);
            //else
            //    dpostAvgCost = dpreAvgCost;

           // if (dActlQty == 0 && dPreQty > 0)
           //     dpostAvgCost = Math.Round (dTtlCost / ((dPreQty == 0) ? 1 : dPreQty), 2);

            if (dActlQty == 0)
                gvItems.Rows[RowIndex].Cells["colPostAvgCost"].Value = "";
            else
                gvItems.Rows[RowIndex].Cells["colPostAvgCost"].Value = string.Format("{0:f}", dpostAvgCost);


            gvItems.Rows[RowIndex].Cells["colChargeOffQty"].Value = string.Format("{0}", (dDelta> 0)? dDelta: 0);
            gvItems.Rows[RowIndex].Cells["colChargeOffAmount"].Value = string.Format("{0:f}", (dDelta > 0) ? Math.Round(dDelta * dpreAvgCost, 2) : 0);

            gvItems.Rows[RowIndex].Cells["colChargeOnQty"].Value = string.Format("{0}", (dDelta < 0) ? -dDelta : 0);
            gvItems.Rows[RowIndex].Cells["colChargeOnAmount"].Value = string.Format("{0:f}", (dDelta < 0) ? Math.Round(-dDelta * dpreAvgCost, 2) : 0);
        }

        private void gvItems_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            paintingRow = 99;
            if (!isUndo)
            {
                var undoEntry = new undoItem();
                undoEntry.rowID = e.RowIndex;
                undoEntry.oldCount = Utilities.GetStringValue(gvItems.Rows[e.RowIndex].Cells["colActualQty"].Value);

                undoStack.Push(undoEntry);
            }
            paintingRow = -1;
        }

        private void gvItems_CancelRowEdit(object sender, QuestionEventArgs e)
        {
            undoStack.Pop();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            isUndo = true;
            if (undoStack.Count > 0)
            {            
                var undoEntry = undoStack.Pop();

                gvItems.CurrentCell = gvItems.Rows[undoEntry.rowID].Cells["colActualQty"];
                gvItems.BeginEdit(true);
                gvItems.Rows[undoEntry.rowID].Cells["colActualQty"].Value = undoEntry.oldCount;
                gvItems.EndEdit();

                //calculateFields(undoEntry.rowID);
            }
            
            if (undoStack.Count == 0)
            {
                btnUndo.Enabled = false;
            }
                

            isUndo = false;
        }
    }
}
