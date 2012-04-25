using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Mapper;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;
using Support.Logic;
//using Oracle.DataAccess.Types;

namespace Support.Forms.HardwareConfig
{
    public partial class FrmConfig : Form
    {
        //Hardware_Config Hardware;
        string SearchType = "PDL";
        string IPAddress;
        bool FirstTime = true;
        private const string Smsg = "Please Contact Support";
        private const string OUTWORKSTATIONS = "OUT_WORKSTATIONS";
        private const string PDLTYPE = "PDL_TYPE";
        OracleDataAccessor DA = GlobalDataAccessor.Instance.OracleDA;
        private const string WORKSTATIONNAME = "WORKSTATION_NAME";
        private const string OUTWORKDETAIL = "OUT_WORK_DETAIL";
        private const string OUTPRINTERS = "OUT_PRINTERS";
        private const string OUTTOPRINTERS = "OUT_TO_PRINTERS";

        //public FrmConfig(Hardware_Config inHardware)
        public FrmConfig() 
        {
            InitializeComponent();
            //Hardware = inHardware;
            //Hardware_Config inHardware = new Hardware_Config() ;
            //inHardware.CurrentTab = 0;
            Hardware_Config.Instance.CurrentTab = 0;
            
            lblHeading.Text = "Hardware Configuration - " + Hardware_Config.Instance.StoreNumber;
            
            //Hardware.PopulatePrinterNames(ComboBox cbPrinterName);
            PopulatePrinterList();
            rbPDL.Checked = true;
            FirstTime = false;
            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbPDL_CheckedChanged(object sender, EventArgs e)
        {
            
            if (rbPDL.Checked && cbPrinterName.Items.Count > 0)
            {
                SearchType = "PDL";
                if (!FirstTime)
                {
                    PopulateHardware();
                }
            }
        }

        private void rbPawn_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPawn.Checked && cbPrinterName.Items.Count > 0)
            {
                SearchType = "Pawn";
                PopulateHardware();
            }
        }

        private void PopulatePrinterList()
        {
            bool RetVal;
            string msg;

            // call oracle procedure to get printers

            //create input parameters
            var iParams = new List<OracleProcParam>();
            //iParams.Add(new OracleProcParam("p_shop_guid", Hardware.StoreID));
            iParams.Add(new OracleProcParam("p_shop_guid", Hardware_Config.Instance.StoreID));

            //setup ref cursor
            var RefCursor = new List<PairType<string, string>>();
            RefCursor.Add(new PairType<string, string>("o_printer_names", OUTPRINTERS));
            RefCursor.Add(new PairType<string, string>("o_printer_to_names", OUTTOPRINTERS));

            try
            {
                RetVal = DA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_support_hardware", "get_store_printers", iParams,
                    RefCursor, "o_return_code", "o_return_text", out Hardware_Config.Instance.PrinterList);
            }
            catch (OracleException oEx)
            {
                msg = string.Format("Invocation of get_store_printers stored proc failed{0}{1}{0}{2}",
                         Environment.NewLine, oEx.Message, Smsg);
                MessageBox.Show(msg, "Error");
                return;
            }

            if (RetVal == false)
            {
                msg = DA.ErrorDescription;
                var ErrorCode = DA.ErrorCode;

                if (ErrorCode == "1")
                {
                    MessageBox.Show(msg, "Error",
                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show(string.Format("{0} -- {1}", msg, ErrorCode), "Error.  Please Contact Support.");
                return;
            }
            if (Hardware_Config.Instance.PrinterList.Tables.Count > 0)
            {
                DataTable mytable = Hardware_Config.Instance.PrinterList.Tables[0];
                DataTable totable = Hardware_Config.Instance.PrinterList.Tables[1];
                cbPrinterName.Items.Clear();
                cbPrinterName.DisplayMember = "printer_name";
                cbPrinterName.ValueMember = "peripheralid";
                cbPrinterName.DataSource = mytable;
                cbTOPrinter.Items.Clear();
                cbTOPrinter.DisplayMember = "printer_name";
                cbTOPrinter.ValueMember = "peripheralid";
                cbTOPrinter.DataSource = totable;
            }
            
        }

        private void cbPrinterName_SelectedIndexChanged(object sender, EventArgs e)
        {
            IPAddress = GetCurrentIP("F");
            lblIP.Text = "IP: " + IPAddress;
            if (cbPrinterName.Items.Count > 0)
            {
                PopulateHardware();
            }
        }

        private string GetCurrentIP(string To_or_From)
        {
            DataTable myTable;

            if (To_or_From == "F")
            {
                myTable = Hardware_Config.Instance.PrinterList.Tables[0];
                if (myTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in myTable.Rows)
                    {
                        if (dr[2].ToString() == cbPrinterName.SelectedValue.ToString())
                        {
                            return dr[1].ToString();
                        }
                    }
                }
            }
            else
            {
                myTable = Hardware_Config.Instance.PrinterList.Tables[1];
                if (myTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in myTable.Rows)
                    {
                        if (dr[2].ToString() == cbTOPrinter.SelectedValue.ToString())
                        {
                            return dr[1].ToString();
                        }
                    }
                }
            }
            return string.Empty;
        }

        private void PopulateHardware()
        {
            string msg;
            bool RetVal;
            dgWorkstations.DataSource = null;
            // call oracle procedure to get hardware
            //create input parameters
            var iParams = new List<OracleProcParam>();
            iParams.Add(new OracleProcParam("p_shop_guid", Hardware_Config.Instance.StoreID));
            iParams.Add(new OracleProcParam("p_peripheral_id", cbPrinterName.SelectedValue.ToString()));
            iParams.Add(new OracleProcParam("p_search_type", SearchType));

            //setup ref cursor
            var RefCursor = new List<PairType<string, string>>();
            RefCursor.Add(new PairType<string, string>("o_workstation_names", "OUT_HARDWARE"));

            try
            {
                RetVal = DA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_support_hardware", "get_printer_workstations", iParams,
                    RefCursor, "o_return_code", "o_return_text", out Hardware_Config.Instance.HardwareList);
            }
            catch (OracleException oEx)
            {
                msg = string.Format("Invocation of get_printer_workstations stored proc failed{0}{1}{0}{2}",
                         Environment.NewLine, oEx.Message, Smsg);
                MessageBox.Show(msg, "Error");
                return;
            }

            if (RetVal == false)
            {
                msg = DA.ErrorDescription;
                var ErrorCode = DA.ErrorCode;

                if (ErrorCode == "1")
                {
                    MessageBox.Show(msg, "Error",
                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show(msg + " -- " + ErrorCode, "Error.  Please Contact Support.");
                return;
            }

            if (Hardware_Config.Instance.HardwareList.Tables.Count > 0)
            {
                DataTable HardwareTable = Hardware_Config.Instance.HardwareList.Tables["OUT_HARDWARE"];
                if (HardwareTable != null && HardwareTable.IsInitialized &&
                    HardwareTable.Rows != null && HardwareTable.Rows.Count > 0)
                {
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = HardwareTable;
                    dgWorkstations.AutoGenerateColumns = false;
                    dgWorkstations.DataSource = bSource;
                    dgWorkstations.Columns[0].DataPropertyName = WORKSTATIONNAME;
                    dgWorkstations.Columns[1].DataPropertyName = "STORE_PERIPHERAL_ID";
                }
            }
            else
            {
                dgWorkstations.DataSource = null;
                //dgWorkstations.Rows.Clear();
            }

        }

        private void cbTOPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            IPAddress = GetCurrentIP("T");
            lblIP2.Text = "IP: " + IPAddress;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string msg;
            bool RetVal;
            bool IsSuccess = true;

            int SelectedRowsCount = dgWorkstations.SelectedRows.Count;

            // make sure user selects at least 1 row
            if (SelectedRowsCount == 0)
            {
                MessageBox.Show("At least one workstation must be selected to continue.", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            // maker sure user does not select the same printer to redirect to.
            var ToPrinter = cbTOPrinter.SelectedValue.ToString();
            var FromPrinter = cbPrinterName.SelectedValue.ToString();
            if (ToPrinter == FromPrinter)
            {
                MessageBox.Show("From and To printers cannot be identical.", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }


            var store_peripheral_array = new string[SelectedRowsCount];
            
            for (int i = 0; i < SelectedRowsCount; i++)
            {
                // build the storeperipheral array
                store_peripheral_array[i] = dgWorkstations[1, dgWorkstations.SelectedRows[i].Index].Value.ToString();
            }

            //create input parameters
            var iParams = new List<OracleProcParam>();
            var storeParam = new OracleProcParam(ParameterDirection.Input, DataTypeConstants.PawnDataType.LISTSTRING, "p_store_peripheral_id", store_peripheral_array.Length);
            for (int i = 0; i < store_peripheral_array.Length; i++)
            {
                storeParam.AddValue(store_peripheral_array[i]);
            }
            iParams.Add(storeParam);
            iParams.Add(new OracleProcParam("p_peripheral_id", cbTOPrinter.SelectedValue.ToString()));
            iParams.Add(new OracleProcParam("p_search_type", SearchType));
            CashlinxPawnSupportSession.Instance.beginTransactionBlock();
            

            try
            {
                RetVal = DA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_support_hardware", "redirect_hardware", iParams,
                    null, "o_return_code", "o_return_text", out Hardware_Config.Instance.HardwareList);
            }
            catch (OracleException oEx)
            {
                msg = string.Format("Invocation of get_printer_workstations stored proc failed{0}{1}{0}{2}",
                         Environment.NewLine, oEx.Message, Smsg);
                MessageBox.Show(msg, "Error");
                CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
                return;
            }

            if (RetVal == false)
            {
                IsSuccess = false;
                msg = DA.ErrorDescription;
                var ErrorCode = DA.ErrorCode;

                if (ErrorCode == "1")
                {
                    MessageBox.Show(msg, "Error",
                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (ErrorCode == "2")
                {
                    MessageBox.Show(msg, "Duplicate Value",
                                         MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    MessageBox.Show(string.Format("{0} -- {1}", msg, ErrorCode), "Error3.  Please Contact Support.");
                }
                
            }

            if (IsSuccess)
            {
                CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.COMMIT);
                MessageBox.Show("Workstations have been redirected successfully.","Success",
                                  MessageBoxButtons.OK,MessageBoxIcon.Information);
                PopulateHardware();
            }
            else
            {
                CashlinxPawnSupportSession.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
            }

        }

        private void llDeselect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow dgr in dgWorkstations.Rows)
            {
                dgr.Selected = false;
            }
        }

        private void llSelect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow dgr in dgWorkstations.Rows)
            {
                dgr.Selected = true;
            }
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Hardware_Config.Instance.CurrentTab == 0)
            {
                UpdateMasterSelect();
            }
            
            Hardware_Config.Instance.CurrentTab = tabControl1.SelectedIndex;
        }

        private void UpdateWorkstations()
        {
            var myTable = Hardware_Config.Instance.AllPrinters.Tables[OUTWORKSTATIONS];
            if (myTable != null && myTable.IsInitialized &&
                myTable.Rows != null && myTable.Rows.Count > 0)
            {
                var PDLTable = myTable.Clone();

                foreach (DataRow dr in myTable.Rows)
                {

                    if (dr[1].ToString() == dgDevices1[5, dgDevices1.SelectedRows[0].Index].Value.ToString() &&
                        dr[0].ToString() == dgDevices1[0, dgDevices1.SelectedRows[0].Index].Value.ToString())
                    {
                        PDLTable.ImportRow(dr);
                    }
                }

                if (PDLTable != null && PDLTable.IsInitialized &&
                    PDLTable.Rows != null && PDLTable.Rows.Count > 0)
                {
                    BindingSource bsource = new BindingSource();
                    bsource.DataSource = PDLTable;
                    dgWorkstations1.AutoGenerateColumns = false;
                    dgWorkstations1.DataSource = bsource;
                    dgWorkstations1.Columns[0].DataPropertyName = WORKSTATIONNAME;
                    dgWorkstations1.Rows[0].Selected = false;

                }
                else
                {
                    dgWorkstations1.DataSource = null;
                    dgWorkstations1.Rows.Clear();
                }
            }
            else
            {
                dgWorkstations1.DataSource = null;
                dgWorkstations1.Rows.Clear();
            }

        }

        private void UpdateDevices()
        {
            DataTable myTable;
            DataTable PDLTable;


            //DataTable HardwareTable = Hardware_Config.Instance.HardwareList.Tables["OUT_HARDWARE"];
            //if (HardwareTable != null && HardwareTable.IsInitialized &&
            //HardwareTable.Rows != null && HardwareTable.Rows.Count > 0)


            myTable = Hardware_Config.Instance.AllPrinters.Tables[OUTWORKDETAIL];
            if (myTable != null && myTable.IsInitialized &&
                myTable.Rows != null && myTable.Rows.Count > 0)
            {
                PDLTable = myTable.Clone();
            


            foreach (DataRow dr in myTable.Rows)
                {

                    if (dr[1].ToString() == dgWorkstations2[1, dgWorkstations2.SelectedRows[0].Index].Value.ToString())
                    {
                        PDLTable.ImportRow(dr);
                    }
                }

                if (PDLTable.IsInitialized &&
                    PDLTable.Rows != null && PDLTable.Rows.Count > 0)
                {
                    var bsource = new BindingSource
                                  {
                                      DataSource = PDLTable
                                  };
                    dgDevices2.AutoGenerateColumns = false;
                    dgDevices2.DataSource = bsource;
                    dgDevices2.Columns[0].DataPropertyName = PDLTYPE;
                    dgDevices2.Columns[1].DataPropertyName = "PRINTER_NAME";
                    dgDevices2.Columns[2].DataPropertyName = "DESCRIPTION";
                    dgDevices2.Columns[3].DataPropertyName = "MODEL_NAME";
                    dgDevices2.Columns[4].DataPropertyName = "IP_ADDRESS";
                    dgDevices2.Rows[0].Selected = false;

                }
                else
                {
                    dgDevices2.DataSource = null;
                    dgDevices2.Rows.Clear();
                }
            }
            else
            {
                dgDevices2.DataSource = null;
                dgDevices2.Rows.Clear();
            }

        }

        private void UpdateMasterSelect()
        {
            bool RetVal;
            string msg;
            string ErrorCode;

            //create input parameters
            List<OracleProcParam> iParams = new List<OracleProcParam>();
            iParams.Add(new OracleProcParam("p_shop_guid", Hardware_Config.Instance.StoreID));
            iParams.Add(new OracleProcParam("p_dummy_var", "X"));

            //setup ref cursor
            var RefCursor = new List<PairType<string, string>>();
            RefCursor.Add(new PairType<string, string>("o_printer_names", OUTPRINTERS));
            RefCursor.Add(new PairType<string, string>("o_workstation_names", OUTWORKSTATIONS));
            RefCursor.Add(new PairType<string, string>("o_distinct_workstations", "OUT_DISTINCT"));
            RefCursor.Add(new PairType<string, string>("o_workstation_detail", OUTWORKDETAIL));

            try
            {
                RetVal = DA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_support_hardware", "get_store_printers", iParams,
                    RefCursor, "o_return_code", "o_return_text", out Hardware_Config.Instance.AllPrinters);
            }
            catch (OracleException oEx)
            {
                msg = string.Format("Invocation of get_store_printers stored proc failed{0}{1}{0}{2}",
                         Environment.NewLine, oEx.Message, Smsg);
                MessageBox.Show(msg, "Error");
                return;
            }

            if (RetVal == false)
            {
                msg = DA.ErrorDescription;
                ErrorCode = DA.ErrorCode;

                MessageBox.Show(msg + " -- " + ErrorCode, "Error.  Please Contact Support.");
                return;
            }

            if (Hardware_Config.Instance.AllPrinters.Tables.Count > 0)
            {
                DataTable HardwareTable = Hardware_Config.Instance.AllPrinters.Tables[OUTPRINTERS];
                if (HardwareTable != null && HardwareTable.IsInitialized &&
                    HardwareTable.Rows != null && HardwareTable.Rows.Count > 0)
                {
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = HardwareTable;
                    dgDevices1.AutoGenerateColumns = false;
                    dgDevices1.Columns[0].DataPropertyName = PDLTYPE;
                    dgDevices1.Columns[1].DataPropertyName = "PRINTER_NAME";
                    dgDevices1.Columns[2].DataPropertyName = "DESCRIPTION";
                    dgDevices1.Columns[3].DataPropertyName = "MODEL_NAME";
                    dgDevices1.Columns[4].DataPropertyName = "IP_ADDRESS";
                    dgDevices1.Columns[5].DataPropertyName = "PERIPHERALID";
                    dgDevices1.Columns[6].DataPropertyName = "SERIAL_NUMBER";
                    dgDevices1.DataSource = bSource;
                }
                else
                {
                    dgDevices1.DataSource = null;
                    dgDevices1.Rows.Clear();
                }

                HardwareTable = Hardware_Config.Instance.AllPrinters.Tables["OUT_DISTINCT"];
                if (HardwareTable != null && HardwareTable.IsInitialized &&
                    HardwareTable.Rows != null && HardwareTable.Rows.Count > 0)
                {
                    BindingSource bSource = new BindingSource();
                    bSource.DataSource = HardwareTable;
                    dgWorkstations2.AutoGenerateColumns = false;
                    dgWorkstations2.Columns[0].DataPropertyName = WORKSTATIONNAME;
                    dgWorkstations2.Columns[1].DataPropertyName = "WORKSTATION_ID";
                    dgWorkstations2.DataSource = bSource;
                }
                else
                {
                    dgWorkstations2.DataSource = null;
                    dgWorkstations2.Rows.Clear();
                }
            }
            else
            {
                dgDevices1.DataSource = null;
                dgDevices1.Rows.Clear();
                dgWorkstations2.DataSource = null;
                dgWorkstations2.Rows.Clear();
                //dgWorkstations.Rows.Clear();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgDevices1_SelectionChanged(object sender, EventArgs e)
        {
            int SelectCount = dgDevices1.SelectedRows.Count;
            if (SelectCount > 0)
            {
                UpdateWorkstations();
            }
        }

        private void dgWorkstations2_SelectionChanged(object sender, EventArgs e)
        {
            int SelectCount = dgWorkstations2.SelectedRows.Count;
            if (SelectCount > 0)
            {
                UpdateDevices();
            }
        }

        private void btnClose3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
