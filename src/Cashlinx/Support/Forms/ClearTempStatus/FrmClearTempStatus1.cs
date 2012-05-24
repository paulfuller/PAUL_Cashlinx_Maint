using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using CashlinxPawnSupportApp.Forms.ClearTempStatus;
using Common.Controllers.Application;
using Common.Controllers.Database.Oracle;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Oracle.DataAccess.Client;
using Support.Logic;

namespace Support.Forms.ClearTempStatus
{
    public partial class FrmClearTempStatus1 : Form
    {
        private const string Smsg = "Please Contact Support";

        public FrmClearTempStatus1()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmClearTempStatus1_Load(object sender, EventArgs e)
        {
            ArrayList recordTypes = new ArrayList();
            recordTypes.Add(new ComboBoxData("", "Select"));
            recordTypes.Add(new ComboBoxData("me", "Merchandise"));
            recordTypes.Add(new ComboBoxData("pl", "Pawn Loan"));
            recordTypes.Add(new ComboBoxData("pu", "Purchase"));
            recordTypes.Add(new ComboBoxData("la", "Layaway"));
            recordTypes.Add(new ComboBoxData("tr", "Transfer"));
            recordTypes.Add(new ComboBoxData("cu", "Customer"));
            recordTypes.Add(new ComboBoxData("rs", "Retail Sale"));
            this.cbRecordType.DataSource = recordTypes;
            this.cbRecordType.DisplayMember = "Description";
            this.cbRecordType.ValueMember = "Code";
            //this.genderList.DataSource = genderTypes;
            //this.genderList.DisplayMember = "Description";
            //this.genderList.ValueMember = "Code";

        }
        //private void FrmTextBoxEnter( object sender , EventArgs e )
        //{
        //    txtDetail.Select();
        //}


        private void FindButton_Click(object sender, EventArgs e)
        {
            string RecordValue = cbRecordType.SelectedValue.ToString();

            if (RecordValue == string.Empty)
            {
                MessageBox.Show("You must select a Record Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }


            if (RecordValue == "me")
            {
                Process_MDSE_Locks();
                return;
            }

            if (RecordValue == "pl")
            {
                Process_Pawn_Locks();
                return;
            }
            
            if (RecordValue == "pu")
            {
                Process_Purchase_Locks();
                return;
            }
            if (RecordValue == "la")
            {
                Process_Layaway_Locks();
                return;
            }
            if (RecordValue == "tr")
            {
                Process_Transfer_Locks();
                return;
            }
            if (RecordValue == "rs")
            {
                Process_Retail_Locks();
                return;
            }
                MessageBox.Show("This record type not implemented yet");
                return;

        }

        private void Process_Retail_Locks()
        {
            OracleDataAccessor DA = GlobalDataAccessor.Instance.OracleDA;
            string RecordType = cbRecordType.Text;
            string ShopNbr = txtShop.Text.PadLeft(5, '0');
            string TicketNbr = txtDetail.Text;
            Point mdsePoint = new Point();
            string LblSearchName;
            bool RetVal;
            string msg;
            string TempStatus;
            string ErrorCode;
            DialogResult frmResult;
            Int32 nTicketNbr;

            mdsePoint.X = 39;
            mdsePoint.Y = 162;
            LblSearchName = "Shop / MSR Number:";

            try
            {
                nTicketNbr = Convert.ToInt32(TicketNbr);
            }
            catch
            {
                MessageBox.Show("Invalid MSR Number");
                return;
            }



            //create input parameters
            List<OracleProcParam> iParams = new List<OracleProcParam>();
            iParams.Add(new OracleProcParam("p_shop_number", ShopNbr));
            iParams.Add(new OracleProcParam("p_ticket_nbr", nTicketNbr));
            iParams.Add(new OracleProcParam("o_temp_status", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 20));

            //execute stored procedure
            DataSet OutDataSet;
            try
            {
                RetVal = DA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_support_locks", "get_retaillock_record", iParams,
                    null, "o_return_code", "o_return_text", out OutDataSet);
            }
            catch (OracleException oEx)
            {
                msg = string.Format("Invocation of get_retaillock_record stored proc failed{0}{1}{0}{2}",
                         Environment.NewLine, oEx.Message, Smsg);
                MessageBox.Show(msg, "Error");
                return;
            }

            if (RetVal == false)
            {
                msg = DA.ErrorDescription;
                ErrorCode = DA.ErrorCode;

                if (ErrorCode == "1")
                {
                    MessageBox.Show("No record found matching this search criteria.", "No Data Found",
                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show(msg, "Error");
                return;
            }

            RetVal = OracleUtilities.GetProcedureOutput(OutDataSet, 0, out TempStatus);
            if (TempStatus == "null")
            {
                MessageBox.Show("No temp lock found.", "Warning",
                                         MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var frmClearStatus2 = new FrmClearTempStatus2(TempStatus, RecordType, LblSearchName,
                                              mdsePoint, ShopNbr, TicketNbr);
            frmResult = frmClearStatus2.ShowDialog();
            if (frmResult == DialogResult.Cancel)
            {
                this.Close();
            }
        }

        private void Process_Transfer_Locks()
        {
            OracleDataAccessor DA = GlobalDataAccessor.Instance.OracleDA;
            string RecordType = cbRecordType.Text;
            string ShopNbr = txtShop.Text.PadLeft(5, '0');
            string TicketNbr = txtDetail.Text;
            Point mdsePoint = new Point();
            string LblSearchName;
            bool RetVal;
            string msg;
            string TempStatus;
            string ErrorCode;
            DialogResult frmResult;
            Int32 nTicketNbr;

            mdsePoint.X = 16;
            mdsePoint.Y = 162;
            LblSearchName = "Shop / Transfer Number:";

            try
            {
                nTicketNbr = Convert.ToInt32(TicketNbr);
            }
            catch
            {
                MessageBox.Show("Invalid Transfer Number");
                return;
            }



            //create input parameters
            List<OracleProcParam> iParams = new List<OracleProcParam>();
            iParams.Add(new OracleProcParam("p_shop_number", ShopNbr));
            iParams.Add(new OracleProcParam("p_ticket_nbr", nTicketNbr));
            iParams.Add(new OracleProcParam("o_temp_status", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 20));

            //execute stored procedure
            DataSet OutDataSet;
            try
            {
                RetVal = DA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_support_locks", "get_transferlock_record", iParams,
                    null, "o_return_code", "o_return_text", out OutDataSet);
            }
            catch (OracleException oEx)
            {
                msg = string.Format("Invocation of get_transferlock_record stored proc failed{0}{1}{0}{2}",
                         Environment.NewLine, oEx.Message, Smsg);
                MessageBox.Show(msg, "Error");
                return;
            }

            if (RetVal == false)
            {
                msg = DA.ErrorDescription;
                ErrorCode = DA.ErrorCode;

                if (ErrorCode == "1")
                {
                    MessageBox.Show("No record found matching this search criteria.", "No Data Found",
                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show(msg, "Error");
                return;
            }

            RetVal = OracleUtilities.GetProcedureOutput(OutDataSet, 0, out TempStatus);
            if (TempStatus == "null")
            {
                MessageBox.Show("No temp lock found.", "Warning",
                                         MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var frmClearStatus2 = new FrmClearTempStatus2(TempStatus, RecordType, LblSearchName,
                                              mdsePoint, ShopNbr, TicketNbr);
            frmResult = frmClearStatus2.ShowDialog();
            if (frmResult == DialogResult.Cancel)
            {
                this.Close();
            }
        }

        private void Process_Layaway_Locks()
        {
            OracleDataAccessor DA = GlobalDataAccessor.Instance.OracleDA;
            string RecordType = cbRecordType.Text;
            string ShopNbr = txtShop.Text.PadLeft(5, '0');
            string TicketNbr = txtDetail.Text;
            Point mdsePoint = new Point();
            string LblSearchName;
            bool RetVal;
            string msg;
            string TempStatus;
            string ErrorCode;
            DialogResult frmResult;
            Int32 nTicketNbr;

            mdsePoint.X = 14;
            mdsePoint.Y = 162;
            LblSearchName = "Shop / Layaway Number:";

            try
            {
                nTicketNbr = Convert.ToInt32(TicketNbr);
            }
            catch
            {
                MessageBox.Show("Invalid Layaway Number");
                return;
            }



            //create input parameters
            List<OracleProcParam> iParams = new List<OracleProcParam>();
            iParams.Add(new OracleProcParam("p_shop_number", ShopNbr));
            iParams.Add(new OracleProcParam("p_ticket_nbr", nTicketNbr));
            iParams.Add(new OracleProcParam("o_temp_status", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 20));

            //execute stored procedure
            DataSet OutDataSet;
            try
            {
                RetVal = DA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_support_locks", "get_layawaylock_record", iParams,
                    null, "o_return_code", "o_return_text", out OutDataSet);
            }
            catch (OracleException oEx)
            {
                msg = string.Format("Invocation of get_layawaylock_record stored proc failed{0}{1}{0}{2}",
                         Environment.NewLine, oEx.Message, Smsg);
                MessageBox.Show(msg, "Error");
                return;
            }

            if (RetVal == false)
            {
                msg = DA.ErrorDescription;
                ErrorCode = DA.ErrorCode;

                if (ErrorCode == "1")
                {
                    MessageBox.Show("No record found matching this search criteria.", "No Data Found",
                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show(msg, "Error");
                return;
            }

            RetVal = OracleUtilities.GetProcedureOutput(OutDataSet, 0, out TempStatus);
            if (TempStatus == "null")
            {
                MessageBox.Show("No temp lock found.", "Warning",
                                         MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var frmClearStatus2 = new FrmClearTempStatus2(TempStatus, RecordType, LblSearchName,
                                              mdsePoint, ShopNbr, TicketNbr);
            frmResult = frmClearStatus2.ShowDialog();
            if (frmResult == DialogResult.Cancel)
            {
                this.Close();
            }
        }



        private void Process_Purchase_Locks()
        {
            OracleDataAccessor DA = GlobalDataAccessor.Instance.OracleDA;
            string RecordType = cbRecordType.Text;
            string ShopNbr = txtShop.Text.PadLeft(5, '0');
            string TicketNbr = txtDetail.Text;
            Point mdsePoint = new Point();
            string LblSearchName;
            bool RetVal;
            string msg;
            string TempStatus;
            string ErrorCode;
            DialogResult frmResult;
            Int32 nTicketNbr;

            mdsePoint.X = 39;
            mdsePoint.Y = 162;
            LblSearchName = "Shop / Buy Number:";

            try
            {
                nTicketNbr = Convert.ToInt32(TicketNbr);
            }
            catch
            {
                MessageBox.Show("Invalid Buy Number");
                return;
            }



            //create input parameters
            List<OracleProcParam> iParams = new List<OracleProcParam>();
            iParams.Add(new OracleProcParam("p_shop_number", ShopNbr));
            iParams.Add(new OracleProcParam("p_ticket_nbr", nTicketNbr));
            iParams.Add(new OracleProcParam("o_temp_status", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 20));

            //execute stored procedure
            DataSet OutDataSet;
            try
            {
                RetVal = DA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_support_locks", "get_purchaselock_record", iParams,
                    null, "o_return_code", "o_return_text", out OutDataSet);
            }
            catch (OracleException oEx)
            {
                msg = string.Format("Invocation of get_purchaselock_record stored proc failed{0}{1}{0}{2}",
                         Environment.NewLine, oEx.Message, Smsg);
                MessageBox.Show(msg, "Error");
                return;
            }

            if (RetVal == false)
            {
                msg = DA.ErrorDescription;
                ErrorCode = DA.ErrorCode;

                if (ErrorCode == "1")
                {
                    MessageBox.Show("No record found matching this search criteria.", "No Data Found",
                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show(msg, "Error");
                return;
            }

            RetVal = OracleUtilities.GetProcedureOutput(OutDataSet, 0, out TempStatus);
            if (TempStatus == "null")
            {
                MessageBox.Show("No temp lock found.", "Warning",
                                         MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var frmClearStatus2 = new FrmClearTempStatus2(TempStatus, RecordType, LblSearchName,
                                              mdsePoint, ShopNbr, TicketNbr);
            frmResult = frmClearStatus2.ShowDialog();
            if (frmResult == DialogResult.Cancel)
            {
                this.Close();
            }
        }

        private void Process_Pawn_Locks()
        {
            OracleDataAccessor DA = GlobalDataAccessor.Instance.OracleDA;
            string RecordType = cbRecordType.Text;
            string ShopNbr = txtShop.Text.PadLeft(5, '0');
            string TicketNbr = txtDetail.Text;
            Point mdsePoint = new Point();
            string LblSearchName;
            bool RetVal;
            string msg;
            string TempStatus;
            string ErrorCode;
            DialogResult frmResult;
            Int32 nTicketNbr;

            mdsePoint.X = 29;
            mdsePoint.Y = 162;
            LblSearchName = "Shop / Ticket Number:";

            try
            {
                nTicketNbr = Convert.ToInt32(TicketNbr);
            }
            catch
            {
                MessageBox.Show("Invalid Ticket Number");
                return;
            }



            //create input parameters
            List<OracleProcParam> iParams = new List<OracleProcParam>();
            iParams.Add(new OracleProcParam("p_shop_number", ShopNbr));
            iParams.Add(new OracleProcParam("p_ticket_nbr", nTicketNbr));
            iParams.Add(new OracleProcParam("o_temp_status", OracleDbType.Varchar2, DBNull.Value, ParameterDirection.Output, 20));

            //execute stored procedure
            DataSet OutDataSet;
            try
            {
                RetVal = DA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_support_locks", "get_pawnlock_record", iParams,
                    null, "o_return_code", "o_return_text", out OutDataSet);
            }
            catch (OracleException oEx)
            {
                msg = string.Format("Invocation of get_mdse_record stored proc failed{0}{1}{0}{2}",
                         Environment.NewLine, oEx.Message, Smsg);
                MessageBox.Show(msg, "Error");
                return;
            }

            if (RetVal == false)
            {
                msg = DA.ErrorDescription;
                ErrorCode = DA.ErrorCode;

                if (ErrorCode == "1")
                {
                    MessageBox.Show("No record found matching this search criteria.", "No Data Found",
                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show(msg, "Error");
                return;
            }

            RetVal = OracleUtilities.GetProcedureOutput(OutDataSet, 0, out TempStatus);
            if (TempStatus == "null")
            {
                MessageBox.Show("No temp lock found.", "Warning",
                                         MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var frmClearStatus2 = new FrmClearTempStatus2( 
                    TempStatus, 
                    RecordType,                  // "Pawn Loan"
                    LblSearchName,              // "Shop / Ticket Number:"
                    mdsePoint,                      // Screen Locaton
                    ShopNbr,                          // store number
                    TicketNbr                           // ticket number
                    );

            frmResult = frmClearStatus2.ShowDialog();
            if (frmResult == DialogResult.Cancel)
            {
                this.Close();
            }

        }

        private void Process_MDSE_Locks()
        {
            OracleDataAccessor DA = GlobalDataAccessor.Instance.OracleDA;
            string RecordType = cbRecordType.Text;
            string ShopNbr = txtShop.Text.PadLeft(5, '0');
            string ICNNbr = txtDetail.Text;
            string LblSearchName;
            string msg;
            Point mdsePoint = new Point();
            bool RetVal;
            DialogResult frmResult;
            Int32 OutRows;


            mdsePoint.X = 79;
            mdsePoint.Y = 162;
            LblSearchName = "Shop / ICN:";

            //create input parameters
            List<OracleProcParam> iParams = new List<OracleProcParam>();
            iParams.Add(new OracleProcParam("p_shop_number", ShopNbr));
            iParams.Add(new OracleProcParam("p_icn_nbr", ICNNbr));

            //setup ref cursor
            var RefCursor = new List<PairType<string, string>>();
            RefCursor.Add(new PairType<string, string>("o_mdse_data", "OUT_DATA"));


            //execute stored procedure
            DataSet OutDataSet;
            try
            {
                RetVal = DA.issueSqlStoredProcCommand(
                    "ccsowner", "pawn_support_locks", "get_mdse_record", iParams,
                    RefCursor, "o_return_code", "o_return_text", out OutDataSet);
            }
            catch (OracleException oEx)
            {
                msg = string.Format("Invocation of get_mdse_record stored proc failed{0}{1}{0}{2}",
                         Environment.NewLine, oEx.Message, Smsg);
                MessageBox.Show(msg, "Error");
                return;
            }

            if (RetVal == false)
            {
                msg = DA.ErrorDescription;
                MessageBox.Show(msg, "Error");
                return;
            }


            if (OutDataSet != null)
            {
                if (OutDataSet.Tables != null)
                {
                    if (OutDataSet.Tables.Count > 0)
                    {
                        DataTable mdseTable = OutDataSet.Tables["OUT_DATA"];
                        if (mdseTable != null && mdseTable.IsInitialized &&
                            mdseTable.Rows != null && mdseTable.Rows.Count > 0)
                        {
                            // add a full icn column to datatable and populate
                            mdseTable.Columns.Add("FullICN", typeof(string));
                            foreach (DataRow x in mdseTable.Rows)
                            {
                                string ICN_STORE = x[0].ToString();
                                string ICN_YEAR = x[1].ToString();
                                string ICN_DOC = x[2].ToString();
                                string ICN_DOC_TYPE = x[3].ToString();
                                string ICN_ITEM = x[4].ToString();
                                string ICN_SUBITEM = x[5].ToString();
                                string FullICN;

                                FullICN = "";
                                FullICN = FullICN + ICN_STORE.PadLeft(5, '0');
                                FullICN += ICN_YEAR;
                                FullICN += ICN_DOC.PadLeft(6, '0');
                                FullICN += ICN_DOC_TYPE;
                                FullICN += ICN_ITEM.PadLeft(3, '0');
                                FullICN += ICN_SUBITEM.PadLeft(2, '0');

                                x[11] = FullICN;

                            }

                            //find out how many rows were returned
                            OutRows = mdseTable.Rows.Count;
                            if (OutRows == 1)
                            {
                                //process for a single record
                                //make sure record has a temp lock
                                //foreach (DataRow dr in LocData.Tables[0].Rows)

                                string Locktype;
                                DataRow dr = mdseTable.Rows[0];
                                Locktype = dr[10].ToString();
                                string FullICN = dr[11].ToString();
                                if (Locktype != string.Empty)
                                {

                                    var frmClearStatus2 = new FrmClearTempStatus2(Locktype, RecordType, LblSearchName,
                                                                                  mdsePoint, ShopNbr, FullICN);
                                    frmResult = frmClearStatus2.ShowDialog();
                                    if (frmResult == DialogResult.Cancel)
                                    {
                                        this.Close();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No temp lock found.", "Warning",
                                         MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    return;
                                }

                            }
                            else
                            {
                                //process for multiple records
                                var frmDupMerch = new FrmDupMerch(mdseTable);
                                frmResult = frmDupMerch.ShowDialog();

                                if (frmResult == DialogResult.OK)
                                {

                                    var frmClearStatus2 = new FrmClearTempStatus2(frmDupMerch.LockDescription, RecordType, LblSearchName,
                                                                                  mdsePoint, ShopNbr, frmDupMerch.FullICN);
                                    frmResult = frmClearStatus2.ShowDialog();
                                    if (frmResult == DialogResult.Cancel)
                                    {
                                        this.Close();
                                    }
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("Error fetching data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        // no records were returned
                        MessageBox.Show("No record found matching this search criteria.", "No Data Found",
                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Error fetching data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Error fetching data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }




        private void cbRecordType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string myval;
            
            myval = cbRecordType.SelectedValue.ToString();

            lblAst2.Visible = true;
            txtShop.Visible = true;
            txtDetail.Visible = true;
            txtShop.Clear();
            txtDetail.Clear();
            
            if (myval == "me")
            {
                lblSearch.Text = "Shop / ICN:";
                lblSearch.Location = new Point(79, 162);
                txtShop.Select();
                return;
            }
            if (myval == "pl")
            {
                lblSearch.Text = "Shop / Ticket Number:";
                lblSearch.Location = new Point(29, 162);
                txtShop.Select();
                return;
            }
            if (myval == "pu")
            {
                lblSearch.Text = "Shop / Buy Number:";
                lblSearch.Location = new Point(39, 162);
                txtShop.Select();
                return;
            }
            if (myval == "la")
            {
                lblSearch.Text = "Shop / Layaway Number:";
                lblSearch.Location = new Point(14, 162);
                txtShop.Select();
                return;
            }
            if (myval == "tr")
            {
                lblSearch.Text = "Shop / Transfer Number:";
                lblSearch.Location = new Point(16, 162);
                txtShop.Select();
                return;
            }
            if (myval == "rs")
            {
                lblSearch.Text = "Shop / MSR Number:";
                lblSearch.Location = new Point(39, 162);
                txtShop.Select();
                return;
            }
            lblSearch.Text = string.Empty;
            lblAst2.Visible = false;
            txtDetail.Visible = false;
            txtShop.Visible = false;
            
            
        }

        //private void txtShop_Leave(object sender, EventArgs e)
        //{
        //    Int32 TextLength;
        //    string str;
        //    str = txtShop.Text;
        //    TextLength = str.Length;
        //    if (TextLength < 5)
        //    {
        //        str = str.PadLeft(5, '0');
        //        txtShop.Text = str;
        //    }

        //}

    }
}
