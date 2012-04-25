using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects;
using Oracle.DataAccess.Client;
using Support.Logic;

namespace Support.Forms.ClearTempStatus
{
    public partial class FrmClearTempStatus2 : Form
    {
        private string ShopNbr;
        private string SearchNbr;
        private string Smsg = "Please Contact Support";
        private string RecordType;
        private OracleDataAccessor DA;

        public FrmClearTempStatus2(string inLockType,string inRecordType,string inSearchLbl,
                                   Point inSearchPoint, string inShopNbr, string inICNNBR)
        {
            InitializeComponent();
            lblTempLock.Text = inLockType;
            lblRecordTypeDesc.Text = inRecordType;
            lblSearch.Text = inSearchLbl;
            lblSearch.Location = inSearchPoint;
            lblShopNbr.Text = inShopNbr;
            lblICN.Text = inICNNBR;

            ShopNbr = inShopNbr;
            SearchNbr = inICNNBR;
            RecordType = inRecordType;
            DA = GlobalDataAccessor.Instance.OracleDA;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            bool RetVal = false;

            if (RecordType == "Merchandise")
            {
                RetVal =  Clear_MDSE();
            }
            if (RecordType == "Pawn Loan")
            {
                RetVal = Clear_Pawn();
            }
            if (RecordType == "Purchase")
            {
                RetVal = Clear_Purchase();
            }
            if (RecordType == "Layaway")
            {
                RetVal = Clear_Layaway();
            }
            if (RecordType == "Transfer")
            {
                RetVal = Clear_Transfer();
            }
            if (RecordType == "Retail Sale")
            {
                RetVal = Clear_Retail();
            }

            if (RetVal == true)
            {
                MessageBox.Show("Temp lock cleared successfully.", "Success!",
                              MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                MessageBox.Show("Error Clearing Temp Lock.", "Error!",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private bool Clear_Transfer()
        {
            bool RetVal;
            string UserName = CashlinxPawnSupportSession.Instance.LoggedInUserSecurityProfile.UserID;
            string msg;

            //create input parameters
            List<OracleProcParam> iParams = new List<OracleProcParam>();
            iParams.Add(new OracleProcParam("p_shop_number", ShopNbr));
            iParams.Add(new OracleProcParam("p_ticket_nbr", SearchNbr));
            iParams.Add(new OracleProcParam("p_user_id", UserName));

            CashlinxPawnSupportSession.Instance.beginTransactionBlock();


            //execute stored procedure
            DataSet OutDataSet;

            try
            {
                RetVal = DA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_support_locks", "clear_transfer_lock", iParams,
                    null, "o_return_code", "o_return_text", out OutDataSet);
            }
            catch (OracleException oEx)
            {
                msg = string.Format("Invocation of clear_transfer_lock stored proc failed{0}{1}{0}{2}",
                         Environment.NewLine, oEx.Message, Smsg);
                MessageBox.Show(msg, "Error");
                CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                return false;
            }

            if (RetVal == false)
            {
                msg = DA.ErrorDescription;
                MessageBox.Show(msg, "Error");
                CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                return false;
            }
            CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.COMMIT);

            return true;
        }


        private bool Clear_Retail()
        {
            bool RetVal;
            string UserName = CashlinxPawnSupportSession.Instance.LoggedInUserSecurityProfile.UserID;
            string msg;

            //create input parameters
            List<OracleProcParam> iParams = new List<OracleProcParam>();
            iParams.Add(new OracleProcParam("p_shop_number", ShopNbr));
            iParams.Add(new OracleProcParam("p_ticket_nbr", SearchNbr));
            iParams.Add(new OracleProcParam("p_user_id", UserName));

            CashlinxPawnSupportSession.Instance.beginTransactionBlock();


            //execute stored procedure
            DataSet OutDataSet;

            try
            {
                RetVal = DA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_support_locks", "clear_retail_lock", iParams,
                    null, "o_return_code", "o_return_text", out OutDataSet);
            }
            catch (OracleException oEx)
            {
                msg = string.Format("Invocation of clear_retail_lock stored proc failed{0}{1}{0}{2}",
                         Environment.NewLine, oEx.Message, Smsg);
                MessageBox.Show(msg, "Error");
                CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                return false;
            }

            if (RetVal == false)
            {
                msg = DA.ErrorDescription;
                MessageBox.Show(msg, "Error");
                CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                return false;
            }
            CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.COMMIT);

            return true;
        }

        private bool Clear_Layaway()
        {
            bool RetVal;
            string UserName = CashlinxPawnSupportSession.Instance.LoggedInUserSecurityProfile.UserID;
            string msg;

            //create input parameters
            List<OracleProcParam> iParams = new List<OracleProcParam>();
            iParams.Add(new OracleProcParam("p_shop_number", ShopNbr));
            iParams.Add(new OracleProcParam("p_ticket_nbr", SearchNbr));
            iParams.Add(new OracleProcParam("p_user_id", UserName));

            CashlinxPawnSupportSession.Instance.beginTransactionBlock();
            

            //execute stored procedure
            DataSet OutDataSet;

            try
            {
                RetVal = DA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_support_locks", "clear_layaway_lock", iParams,
                    null, "o_return_code", "o_return_text", out OutDataSet);
            }
            catch (OracleException oEx)
            {
                msg = string.Format("Invocation of clear_layaway_lock stored proc failed{0}{1}{0}{2}",
                         Environment.NewLine, oEx.Message, Smsg);
                MessageBox.Show(msg, "Error");
                CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                return false;
            }

            if (RetVal == false)
            {
                msg = DA.ErrorDescription;
                MessageBox.Show(msg, "Error");
                CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                return false;
            }
            CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.COMMIT);

            return true;
        }

        private bool Clear_Purchase()
        {
            bool RetVal;
            string UserName = CashlinxPawnSupportSession.Instance.LoggedInUserSecurityProfile.UserID;
            string msg;

            //create input parameters
            List<OracleProcParam> iParams = new List<OracleProcParam>();
            iParams.Add(new OracleProcParam("p_shop_number", ShopNbr));
            iParams.Add(new OracleProcParam("p_ticket_nbr", SearchNbr));
            iParams.Add(new OracleProcParam("p_user_id", UserName));

            CashlinxPawnSupportSession.Instance.beginTransactionBlock();
            

            //execute stored procedure
            DataSet OutDataSet;

            try
            {
                RetVal = DA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_support_locks", "clear_purchase_lock", iParams,
                    null, "o_return_code", "o_return_text", out OutDataSet);
            }
            catch (OracleException oEx)
            {
                msg = string.Format("Invocation of clear_purchase_lock stored proc failed{0}{1}{0}{2}",
                         Environment.NewLine, oEx.Message, Smsg);
                MessageBox.Show(msg, "Error");
                CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                return false;
            }

            if (RetVal == false)
            {
                msg = DA.ErrorDescription;
                MessageBox.Show(msg, "Error");
                CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                return false;
            }
            CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.COMMIT);

            return true;
        }

        private bool Clear_Pawn()
        {
            bool RetVal;
            string UserName = CashlinxPawnSupportSession.Instance.LoggedInUserSecurityProfile.UserID;
            string msg;

            //create input parameters
            List<OracleProcParam> iParams = new List<OracleProcParam>();
            iParams.Add(new OracleProcParam("p_shop_number", ShopNbr));
            iParams.Add(new OracleProcParam("p_ticket_nbr", SearchNbr));
            iParams.Add(new OracleProcParam("p_user_id", UserName));

            CashlinxPawnSupportSession.Instance.beginTransactionBlock();

            //execute stored procedure
            DataSet OutDataSet;

            try
            {
                RetVal = DA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_support_locks", "clear_pawn_lock", iParams,
                    null, "o_return_code", "o_return_text", out OutDataSet);
            }
            catch (OracleException oEx)
            {
                msg = string.Format("Invocation of clear_pawn_lock stored proc failed{0}{1}{0}{2}",
                         Environment.NewLine, oEx.Message, Smsg);
                MessageBox.Show(msg, "Error");
                CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                return false;
            }

            if (RetVal == false)
            {
                msg = DA.ErrorDescription;
                MessageBox.Show(msg, "Error");
                CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                return false;
            }
            CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.COMMIT);

            return true;
        }

        private bool Clear_MDSE()
        {
            bool RetVal;
            //CashlinxPawnSupportSession.Instance.LoggedInUserSecurityProfile.UserCurrentPassword
            //CashlinxPawnSupportSession.Instance.LoggedInUserSecurityProfile.UserDN
            string UserName = CashlinxPawnSupportSession.Instance.LoggedInUserSecurityProfile.UserID;
            string msg;

            //create input parameters
            List<OracleProcParam> iParams = new List<OracleProcParam>();
            iParams.Add(new OracleProcParam("p_shop_number", ShopNbr));
            iParams.Add(new OracleProcParam("p_icn_nbr", SearchNbr));
            iParams.Add(new OracleProcParam("p_user_id", UserName));

            CashlinxPawnSupportSession.Instance.beginTransactionBlock();

            //execute stored procedure
            DataSet OutDataSet;

            try
            {
                RetVal = DA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_support_locks", "clear_mdse_lock", iParams,
                    null, "o_return_code", "o_return_text", out OutDataSet);
            }
            catch (OracleException oEx)
            {
                msg = string.Format("Invocation of clear_mdse_lock stored proc failed{0}{1}{0}{2}",
                         Environment.NewLine, oEx.Message, Smsg);
                MessageBox.Show(msg, "Error");
                CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                return false;
            }

            if (RetVal == false)
            {
                msg = DA.ErrorDescription;
                MessageBox.Show(msg, "Error");
                CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                return false;
            }
            CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.COMMIT);

            return true;

        }

    }
}
