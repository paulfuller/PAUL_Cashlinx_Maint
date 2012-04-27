/********************************************************************
* Namespace: CashlinxDesktop.DesktopForms.Pawn.ShopAdministration
* FileName: ManageWorkstations
* This form is shown to add new workstations to the system
* Sreelatha Rengarajan 2/23/2010 Initial version
*******************************************************************/

using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Utility.Exception;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    public partial class ManageWorkstations : CustomBaseForm
    {
        private int workstationRowCount;
        public ManageWorkstations()
        {
            InitializeComponent();
        }

        private void ManageWorkstations_Load(object sender, EventArgs e)
        {
            LoadWorkstations();

        }

        private void LoadWorkstations()
        {

            DataTable CDWorkstationsTable;
            string errorcode;
            string errormesg;
            try
            {
                customDataGridViewWorkstations.Rows.Clear();
                bool retval = ShopProcedures.GetAllWorkstations(
                    GlobalDataAccessor.Instance.OracleDA,
                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                    out CDWorkstationsTable, out errorcode, out errormesg);
                if (retval && CDWorkstationsTable != null && CDWorkstationsTable.Rows.Count > 0)
                {
                    workstationRowCount = CDWorkstationsTable.Rows.Count;
                    foreach (DataRow dgvr in CDWorkstationsTable.Rows)
                    {

                        string shopNumber = dgvr.ItemArray[2].ToString();
                        string name = dgvr.ItemArray[1].ToString();
                        string status = dgvr.ItemArray[3].ToString();


                        DataGridViewTextBoxCell shopcell = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell statuscell = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell namecell = new DataGridViewTextBoxCell();
                        shopcell.Value = shopNumber;
                        namecell.Value = name;
                        statuscell.Value = status;

                        namecell.MaxInputLength = 100;
                        namecell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

                        DataGridViewRow dgRow;
                        using (dgRow = new DataGridViewRow())
                        {
                            dgRow.Cells.Insert(0, namecell);
                            dgRow.Cells.Insert(1, statuscell);
                            dgRow.Cells.Insert(2, shopcell);
                            customDataGridViewWorkstations.Rows.Add(dgRow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Error trying to load workstations", new ApplicationException(ex.Message));
                Close();
            }
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customButtonAdd_Click(object sender, EventArgs e)
        {
            customDataGridViewWorkstations.Rows.Add();
            this.customButtonAdd.Enabled = false;
            
        }

        private void customDataGridViewWorkstations_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= workstationRowCount)
            {
                if (e.ColumnIndex == 0)
                {
                    customDataGridViewWorkstations.Rows[e.RowIndex].Cells[1].Value = "New";
                    customDataGridViewWorkstations.Rows[e.RowIndex].Cells[2].Value = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                }
            }
        }

        private void customButtonSubmit_Click(object sender, EventArgs e)
        {
            string errorCode;
            string errorMesg;
            if (customDataGridViewWorkstations.Rows.Count > workstationRowCount)
            {
                string workstationName = customDataGridViewWorkstations.Rows[customDataGridViewWorkstations.Rows.Count - 1].Cells[0].EditedFormattedValue.ToString();
                if (!string.IsNullOrEmpty(workstationName))
                {
                    bool retval = ShopCashProcedures.AddWorkstation(workstationName,
                        GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, GlobalDataAccessor.Instance.DesktopSession,
                        out errorCode, out errorMesg);
                    if (retval)
                        MessageBox.Show("Workstation successfully added");
                    else
                        MessageBox.Show("Error adding workstation " + errorMesg);
                    customButtonAdd.Enabled = true;
                    LoadWorkstations();
                }
            }
            else
                return;
        }
    }
}
