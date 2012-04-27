using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Oracle.DataAccess.Client;
using Support.Forms.HardwareConfig;
using Support.Logic;

namespace Support.Forms
{
    public partial class FrmGetShop : Form
    {
        //HardwareConfig.Hardware_Config Hardware;
        private const string Smsg = "Please Contact Support";
        public string StoreGuid;
        public string StoreNumber;

        public DesktopSession desktopSession { private set; get; }

        public FrmGetShop(//HardwareConfig.Hardware_Config inHardware,
            DesktopSession dSession)
        {
            InitializeComponent();
            //Hardware = new Hardware_Config();
            desktopSession = dSession;
            txtShop.Select();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            bool RetVal;
            string msg;
            //string StoreGuid;
            //HardwareConfig.Hardware_Config.StoreNumber = txtShop.Text;

            StoreNumber = txtShop.Text.PadLeft(5, '0');
            if (!txtShop.isValid) // for empty string validation
            {
                MessageBox.Show(txtShop.ErrorMessage);
                txtShop.Select();
                return;
            }
            else
            {
                OracleDataAccessor DA = GlobalDataAccessor.Instance.OracleDA;

                // call oracle procedure to get store guid

                //create input parameters
                var iParams = new List<OracleProcParam>();
                iParams.Add(new OracleProcParam("p_shop_number", StoreNumber));
                iParams.Add(new OracleProcParam("o_store_guid", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 50));

                //execute stored procedure
                DataSet OutDataSet;
                try
                {
                    RetVal = DA.issueSqlStoredProcCommand(
                        "ccsowner", "pawn_support_gen_procs", "get_store_guid", iParams,
                        null, "o_return_code", "o_return_text", out OutDataSet);
                }
                catch (OracleException oEx)
                {
                    msg = string.Format("Invocation of get_store_guid stored proc failed{0}{1}{0}{2}",
                             Environment.NewLine, oEx.Message, Smsg);
                    MessageBox.Show(msg, "Error");
                    return;
                }

                if (RetVal == false)
                {
                    msg = DA.ErrorDescription;  // error description that should show only.
                    string ErrorCode = DA.ErrorCode;

                    if (ErrorCode == "1")
                    {
                        MessageBox.Show(msg, "Error",
                                             MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtShop.Select();
                        return;
                    }
                    MessageBox.Show(msg + " -- " + ErrorCode, "Error.  Please Contact Support.");

                    return;

                }

                RetVal = OracleUtilities.GetProcedureOutput(OutDataSet, 0, out StoreGuid);
                if (StoreGuid == "null")
                {
                    MessageBox.Show("Error Retrieving Store GUID.  Please contact support.", "Warning",
                                             MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }  
            }
            

            //Hardware.StoreID = StoreGuid;
            //Hardware_Config.Instance.StoreID = StoreGuid;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
