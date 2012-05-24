using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Forms.Components;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Type;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    public partial class CashDrawerStatus : CustomBaseForm
    {
        public List<PairType<string,string>> OpenCDs;
        public List<PairType<string,string>> ClosedUnverifiedCDs;
        private int selectedRow;
        public CashDrawerStatus()
        {
            InitializeComponent();
        }

        private void CashDrawerStatus_Load(object sender, EventArgs e)
        {
            selectedRow = -1;
            DataTable cdData = new DataTable();
            cdData.Columns.Add("id", typeof(string));
            cdData.Columns.Add("cashdrawername",typeof(string));
            cdData.Columns.Add("status",typeof(string));
            foreach (PairType<string,string> s in OpenCDs)
            {
                cdData.Rows.Add(s.Left,s.Right, "Open");
            }
            foreach (PairType<string,string> s in ClosedUnverifiedCDs)
            {
                cdData.Rows.Add(s.Left, s.Right, "Closed Unverified");
            }

            BindingSource _bindingSource2 = new BindingSource();
            _bindingSource2.DataSource = cdData;
            this.dataGridViewCashDrawerStatus.AutoGenerateColumns = false;
            this.dataGridViewCashDrawerStatus.DataSource = _bindingSource2;
            this.dataGridViewCashDrawerStatus.Columns[0].DataPropertyName = "id";
            this.dataGridViewCashDrawerStatus.Columns[1].DataPropertyName = "cashdrawername";
            this.dataGridViewCashDrawerStatus.Columns[2].DataPropertyName = "status";
        }

        private void dataGridViewCashDrawerStatus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
                {
                    string cdId = dataGridViewCashDrawerStatus.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string cdName = dataGridViewCashDrawerStatus.Rows[e.RowIndex].Cells[1].Value.ToString();
                    GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID = cdId;
                    GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerName = cdName;
                    selectedRow = e.RowIndex;
                    
                }
            }
        }

        private void customButtonBalance_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID))
                MessageBox.Show("Please select a cash drawer to balance");
            else
            {
                //check if the cashdrawer has other teller operation in progress
                string wrkId;
                string cdEvent;
                bool retValue;
                string errorCode;
                string errorMesg;
                retValue = ShopCashProcedures.GetTellerEvent(GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID, GlobalDataAccessor.Instance.DesktopSession, out wrkId, out cdEvent, out errorCode, out errorMesg);
                if (retValue)
                {
                        if (errorCode != "100")
                        {

                            MessageBox.Show("There is a cashdrawer event in progress. Please complete that operation first");
                            
                            return;
                        }
                        string workstationID = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId;
                        //Insert an event record
                        retValue = ShopCashProcedures.InsertTellerEvent(GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID, workstationID, "balancecashdrawer", out errorCode, out errorMesg);
                        if (!retValue)
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, this, "Teller Event could not be inserted");



                }

                GlobalDataAccessor.Instance.DesktopSession.OtherCDBalanced = false;
                this.Close();
            }
 

        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID))
            {
                bool retValue;
                string errorCode;
                string errorMesg;
                string workstationID = SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId;
                ShopCashProcedures.RemoveTellerEvent(GlobalDataAccessor.Instance.DesktopSession.BalanceOtherCashDrawerID, workstationID, out errorCode, out errorMesg);
            }
            this.Close();
        }

    }
}
