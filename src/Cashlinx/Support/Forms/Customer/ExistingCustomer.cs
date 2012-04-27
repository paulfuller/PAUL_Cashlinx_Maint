/********************************************************************
* CashlinxDesktop
* ExistingCustomer
* This form will be shown if there are customer matches
* for ID or Name/DOB when a new customer is submitted
* in the manage pawn screen or if an existing customer info is changed
* Sreelatha Rengarajan 4/1/2009 Initial version
*******************************************************************/

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
//using Pawn.Logic;

namespace Support.Forms.Pawn.Customer
{
    public partial class ExistingCustomer : Form
    {
        private BindingSource _existingCustBindingSource;
        private BindingSource _newCustBindingSouce;
        private string _strMessageToShow;
        private bool _nameCheck;
        private CustomerVO _currentCustomer;
        private CustomerVO _newCustomer;
        private DataTable _existingCustomers;
        private Form ownerFrm;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NavBox NavControlBox;// { get; set; }



        public ExistingCustomer()
        {
            InitializeComponent();
            NavControlBox = new NavBox();


        }

        private void ExistingCustomer_Load(object sender, EventArgs e)
        {

            try
            {
                //set the owner form
                ownerFrm = this.Owner;
                this.NavControlBox.Owner = this;

                //Pull data out of cashlinx desktop session
                var gSess = GlobalDataAccessor.Instance;
                var dSession = gSess.DesktopSession;
                _currentCustomer = dSession.EXCurrentCustomer;
                _existingCustomers = dSession.EXExistingCustomers;
                if (!string.IsNullOrEmpty(dSession.EXErrorMessage))
                {
                    _strMessageToShow = dSession.EXErrorMessage;
                }
                else
                {
                    _strMessageToShow = dSession.EXMessageToShow;
                }
                _nameCheck = dSession.EXNameCheck;
                _newCustomer = dSession.EXNewCustomer;

                //Set the message to show on top
                this.labelRedHeader.Text = Commons.GetMessageString("ExistingCustomerHeader");
                this.labelMessage.Text = _strMessageToShow;
                //Binding new customer data
                _newCustBindingSouce = new BindingSource();
                var dataTableNewCustomer = new DataTable();
                dataTableNewCustomer.Columns.Add("LastName");
                dataTableNewCustomer.Columns.Add("FirstName");
                dataTableNewCustomer.Columns.Add("CustAddress");
                dataTableNewCustomer.Columns.Add("DateOfBirth");
                dataTableNewCustomer.Columns.Add("IDData");
                dataTableNewCustomer.Columns.Add("IssuerName");
                var newCustRow = dataTableNewCustomer.NewRow();
                newCustRow["LastName"] = _newCustomer.LastName;
                newCustRow["FirstName"] = _newCustomer.FirstName;
                if (_newCustomer.getHomeAddress() != null)
                    newCustRow["CustAddress"] = _newCustomer.getHomeAddress().CustAddress;
                else
                    newCustRow["CustAddress"] = string.Empty;
                newCustRow["DateOfBirth"] = (_newCustomer.DateOfBirth).FormatDate();
                var custId = _newCustomer.getFirstIdentity();
                if (custId != null)
                {
                    newCustRow["IDData"] = custId.IDData;
                    newCustRow["IssuerName"] = custId.IdIssuerCode;
                }
                else
                {
                    newCustRow["IDData"] = string.Empty;
                    newCustRow["IssuerName"] = string.Empty;
                }

                dataTableNewCustomer.Rows.Add(newCustRow);

                _newCustBindingSouce.DataSource = dataTableNewCustomer;
                this.customDataGridViewNewCustomer.AutoGenerateColumns = false;
                int newColIndex = 0;
                var selectNewColumn = new DataGridViewCheckBoxColumn
                                                                 {
                                                                     HeaderText = "Select",
                                                                     ReadOnly = false,
                                                                     ThreeState = false,
                                                                     FalseValue = false
                                                                 };

                newColIndex = this.customDataGridViewNewCustomer.Columns.Add(selectNewColumn);

                newColIndex = this.customDataGridViewNewCustomer.Columns.Add("LastName", "Last Name");
                this.customDataGridViewNewCustomer.Columns[newColIndex].DataPropertyName = "LastName";
                this.customDataGridViewNewCustomer.Columns[newColIndex].ReadOnly = true;


                newColIndex = this.customDataGridViewNewCustomer.Columns.Add("FirstName", "First Name");
                this.customDataGridViewNewCustomer.Columns[newColIndex].DataPropertyName = "FirstName";
                this.customDataGridViewNewCustomer.Columns[newColIndex].ReadOnly = true;


                newColIndex = this.customDataGridViewNewCustomer.Columns.Add("CustAddress", "Address");
                this.customDataGridViewNewCustomer.Columns[newColIndex].DataPropertyName = "CustAddress";
                this.customDataGridViewNewCustomer.Columns[newColIndex].ReadOnly = true;



                newColIndex = this.customDataGridViewNewCustomer.Columns.Add("birthdate", "Date of Birth");
                this.customDataGridViewNewCustomer.Columns[newColIndex].DataPropertyName = "DateOfBirth";
                this.customDataGridViewNewCustomer.Columns[newColIndex].ReadOnly = true;


                newColIndex = this.customDataGridViewNewCustomer.Columns.Add("IdTypeNumber", "ID Type & Number");
                this.customDataGridViewNewCustomer.Columns[newColIndex].DataPropertyName = "IDData";
                this.customDataGridViewNewCustomer.Columns[newColIndex].ReadOnly = true;



                newColIndex = this.customDataGridViewNewCustomer.Columns.Add("Issuer_Name", "Issuer");
                this.customDataGridViewNewCustomer.Columns[newColIndex].DataPropertyName = "IssuerName";
                this.customDataGridViewNewCustomer.Columns[newColIndex].ReadOnly = true;


                if (_nameCheck)
                {
                    customDataGridViewNewCustomer.Columns["LastName"].DefaultCellStyle.ForeColor = Color.Red;
                    customDataGridViewNewCustomer.Columns["FirstName"].DefaultCellStyle.ForeColor = Color.Red;
                    customDataGridViewNewCustomer.Columns["birthdate"].DefaultCellStyle.ForeColor = Color.Red;

                }
                else
                {
                    customDataGridViewNewCustomer.Columns["IdTypeNumber"].DefaultCellStyle.ForeColor = Color.Red;
                    customDataGridViewNewCustomer.Columns["Issuer_Name"].DefaultCellStyle.ForeColor = Color.Red;
                }
                customDataGridViewNewCustomer.DataSource = _newCustBindingSouce;
                customDataGridViewNewCustomer.AutoResizeColumns();

                //Binding existing customer data
                _existingCustomers.Columns.Add("IDData");
                _existingCustomers.Columns.Add("Address");

                string idData;
                string address;
                //Concatenate the ID type and number in the ID Data field
                //Concatenate the address fields in one
                foreach (DataRow dr in _existingCustomers.Rows)
                {

                    idData = string.Format("{0}-{1}", dr["Ident_type_code"], dr["Issued_number"]);
                    dr.SetField("idData", idData);
                    string addr2 = dr["address2_text"].ToString();
                    if (addr2.Trim().Length > 0)
                        address = string.Format("{0},{1},{2},{3},{4}", dr["address1_text"], dr["address2_text"], dr["city_name"], dr["state_province_name"], dr["postal_code_text"]);
                    else
                        address = string.Format("{0},{1},{2},{3}", dr["address1_text"], dr["city_name"], dr["state_province_name"], dr["postal_code_text"]);
                    dr.SetField("Address", address);
                }
                _existingCustBindingSource = new BindingSource();
                //Add primary key to the existing customers table
                var key = new DataColumn[1];
                key[0] = _existingCustomers.Columns["customer_number"];
                _existingCustomers.PrimaryKey = key;

                _existingCustBindingSource.DataSource = _existingCustomers;
                this.customDataGridViewExistingCustomer.AutoGenerateColumns = false;
                newColIndex = 0;

                var selectColumn = new DataGridViewCheckBoxColumn
                                                              {
                                                                  HeaderText = "Select",
                                                                  ReadOnly = false,
                                                                  ThreeState = false,
                                                                  FalseValue = false
                                                              };
                newColIndex = this.customDataGridViewExistingCustomer.Columns.Add(selectColumn);
                newColIndex = this.customDataGridViewExistingCustomer.Columns.Add("family_name", "Last Name");
                this.customDataGridViewExistingCustomer.Columns[newColIndex].DataPropertyName = "family_name";
                this.customDataGridViewExistingCustomer.Columns[newColIndex].ReadOnly = true;

                newColIndex = this.customDataGridViewExistingCustomer.Columns.Add("first_name", "First Name");
                this.customDataGridViewExistingCustomer.Columns[newColIndex].DataPropertyName = "first_name";
                this.customDataGridViewExistingCustomer.Columns[newColIndex].ReadOnly = true;

                newColIndex = this.customDataGridViewExistingCustomer.Columns.Add("Address", "Address");
                this.customDataGridViewExistingCustomer.Columns[newColIndex].DataPropertyName = "Address";
                this.customDataGridViewExistingCustomer.Columns[newColIndex].ReadOnly = true;

                newColIndex = this.customDataGridViewExistingCustomer.Columns.Add("birthdate", "Date of Birth");
                this.customDataGridViewExistingCustomer.Columns[newColIndex].DataPropertyName = "birthdate";
                this.customDataGridViewExistingCustomer.Columns[newColIndex].ReadOnly = true;

                newColIndex = this.customDataGridViewExistingCustomer.Columns.Add("IdcodeNumber", "ID Type & Number");
                this.customDataGridViewExistingCustomer.Columns[newColIndex].DataPropertyName = "IDData";
                this.customDataGridViewExistingCustomer.Columns[newColIndex].ReadOnly = true;

                newColIndex = this.customDataGridViewExistingCustomer.Columns.Add("Issuer_Name", "Issuer");
                this.customDataGridViewExistingCustomer.Columns[newColIndex].DataPropertyName = "Issuer_Name";
                this.customDataGridViewExistingCustomer.Columns[newColIndex].ReadOnly = true;

                if (_nameCheck)
                {
                    this.customDataGridViewExistingCustomer.Columns["family_name"].DefaultCellStyle.ForeColor = Color.Red;
                    this.customDataGridViewExistingCustomer.Columns["first_name"].DefaultCellStyle.ForeColor = Color.Red;
                    this.customDataGridViewExistingCustomer.Columns["birthdate"].DefaultCellStyle.ForeColor = Color.Red;

                }
                else
                {
                    this.customDataGridViewExistingCustomer.Columns["IdcodeNumber"].DefaultCellStyle.ForeColor = Color.Red;
                    this.customDataGridViewExistingCustomer.Columns["Issuer_Name"].DefaultCellStyle.ForeColor = Color.Red;

                }

                this.customButtonContinue.Enabled = false;
                this.customDataGridViewExistingCustomer.DataSource = _existingCustBindingSource;
                this.customDataGridViewNewCustomer.ClearSelection();
                this.customDataGridViewExistingCustomer.ClearSelection();
                this.customDataGridViewExistingCustomer.AutoResizeColumns();
                this.ActiveControl = labelRedHeader;
            }
            catch (SystemException ex)
            {
                BasicExceptionHandler.Instance.AddException("Error loading the existing customer screen ", ex);
                clearCustomerSessionValues();
                this.NavControlBox.Action = NavBox.NavAction.CANCEL;

            }

        }

        private void clearCustomerSessionValues()
        {
            var dSession = GlobalDataAccessor.Instance.DesktopSession;
            dSession.EXCurrentCustomer = null;
            dSession.EXErrorMessage = string.Empty;
            dSession.EXExistingCustomers = null;
            dSession.EXMessageToShow = string.Empty;
            dSession.EXNameCheck = false;
            dSession.EXNewCustomer = null;
            dSession.MPNameCheck = false;
            dSession.MPCustomer = null;


        }


        //When the user clicks cancel, take them to the desktop
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult dR = MessageBox.Show("Do you want to continue processing this customer?", "Existing Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dR == DialogResult.No)
            {
                GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();
                GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                _newCustomer = null;
                clearCustomerSessionValues();
                this.NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
            else
            {
                this.Show();
                this.Focus();
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            //Take the user to the New Customer Information Add screen
            //Remove all Identities data from the customer being edited
            //and if its an existing customer add back the identities that already exists for 
            //this customer..This step is done to remove the newly added identity
            _newCustomer.removeIdentities();
            if (_currentCustomer != null)
            {
                foreach (var ident in _currentCustomer.getAllIdentifications())
                    _newCustomer.addIdentity(ident);
            }
            GlobalDataAccessor.Instance.DesktopSession.MPCustomer = _newCustomer;
            GlobalDataAccessor.Instance.DesktopSession.MPNameCheck = _nameCheck;
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "ManagePawnApplication";
            this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }


        private void buttonContinue_Click(object sender, EventArgs e)
        {
            //Take the user to the New Customer Information Add screen
            //ManagePawnApplication managepawn = new ManagePawnApplication();
            if (customDataGridViewNewCustomer.Rows[0].Cells[0].Selected)
            {
                //All the data originally entered should be preserved
                //except for the ID data if it is a new customer        
                //The Identity information need not be persisted 
                //when the user goes back to the manage pawn screen
                //for a new customer but the original IDs of an existing customer
                //should be added back
                _newCustomer.removeIdentities();
                if (_currentCustomer != null)
                {
                    foreach (var ident in _currentCustomer.getAllIdentifications())
                        _newCustomer.addIdentity(ident);
                }

                GlobalDataAccessor.Instance.DesktopSession.MPCustomer = _newCustomer;
                GlobalDataAccessor.Instance.DesktopSession.MPNameCheck = _nameCheck;
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "ManagePawnApplication";
                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

            }
            else
            {
                clearCustomerSessionValues();
                try
                {
                    var cust = PopulateExistingCustomerData();
                    GlobalDataAccessor.Instance.DesktopSession.MPCustomer = cust;
                    GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = cust;
                    GlobalDataAccessor.Instance.DesktopSession.MPNameCheck = false;
                    NavControlBox.IsCustom = true;
                    NavControlBox.CustomDetail = "ManagePawnApplication";
                    NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                }
                catch (SystemException ex)
                {
                    BasicExceptionHandler.Instance.AddException("Error occurred when loading existing customer in manage pawn", new ApplicationException(ex.Message));
                    MessageBox.Show(Commons.GetMessageString("ProcessingError"));
                    this.NavControlBox.Action = NavBox.NavAction.CANCEL;
                }

            }
        }

        private CustomerVO PopulateExistingCustomerData()
        {

            DataTable customerData = (DataTable)_existingCustBindingSource.DataSource;
            DataRowView drv = (DataRowView)(_existingCustBindingSource.Current);
            DataRow selectedCustomerRow = customerData.Rows.Find(drv.Row["customer_number"]);
            string selectedCustNumber = selectedCustomerRow.ItemArray[(int)customerduprecord.CUSTOMER_NUMBER].ToString();
            CustomerVO customerObject = CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, selectedCustNumber);
            return customerObject;

        }


        private void dataGridViewExistingCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                //Commit the changed value of checkbox, it's the key to implement this function. 
                if (customDataGridViewExistingCustomer.IsCurrentCellDirty)
                {
                    customDataGridViewExistingCustomer.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
                DataGridViewCheckBoxCell selectNewColumn = (DataGridViewCheckBoxCell)customDataGridViewExistingCustomer.Rows[e.RowIndex].Cells[0];

                if (selectNewColumn.Value.ToString()=="False")
                {
                    customDataGridViewExistingCustomer.Rows[e.RowIndex].Selected = false;

                    foreach (DataGridViewCell dgCell in customDataGridViewExistingCustomer.Rows[e.RowIndex].Cells)
                        dgCell.Style.BackColor = System.Drawing.Color.White;
                    this.customButtonContinue.Enabled = false;
                    this.customButtonUpdate.Enabled = false;
                }
                else
                {
                    bool existingCustomerSelected = false;
                    foreach (DataGridViewRow dgExistRow in customDataGridViewExistingCustomer.Rows)
                    {
                        if (dgExistRow.Cells[0].Selected)
                        {
                            foreach (DataGridViewCell dgCell in dgExistRow.Cells)
                                dgCell.Style.BackColor = System.Drawing.Color.YellowGreen;
                            existingCustomerSelected = true;
                        }
                        else
                        {
                            foreach (DataGridViewCell dgCell in dgExistRow.Cells)
                                dgCell.Style.BackColor = System.Drawing.Color.White;

                        }
                    }
                    DataGridViewCheckBoxCell checkboxColumn;
                    foreach (DataGridViewRow dgRow in customDataGridViewNewCustomer.Rows)
                    {
                        checkboxColumn = (DataGridViewCheckBoxCell)dgRow.Cells[0];
                        checkboxColumn.Value = false;
                        checkboxColumn.Selected = false;

                        foreach (DataGridViewCell dgCell in dgRow.Cells)
                            dgCell.Style.BackColor = System.Drawing.Color.White;
                    }

                    if (existingCustomerSelected)
                    {
                        this.customButtonContinue.Enabled = true;
                        this.customButtonUpdate.Enabled = true;
                    }
                    else
                    {
                        this.customButtonContinue.Enabled = false;
                        this.customButtonUpdate.Enabled = false;
                    }

                }
            }
        }




        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            //Populate the customer object
            var cust = PopulateExistingCustomerData();
            if (cust != null)
            {
                GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = cust;
                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = "ViewPawnCustomerInformation";
                NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {
                BasicExceptionHandler.Instance.AddException("Failed to load customer object in existing customer screen ", new ApplicationException());
                MessageBox.Show(Commons.GetMessageString("ProcessingError"));
                clearCustomerSessionValues();
                this.NavControlBox.Action = NavBox.NavAction.CANCEL;
            }


        }

        private void dataGridViewNewCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                //Commit the changed value of checkbox, it's the key to implement this function. 
                if (customDataGridViewNewCustomer.IsCurrentCellDirty)
                {
                    customDataGridViewNewCustomer.CommitEdit(DataGridViewDataErrorContexts.Commit);

                }
                DataGridViewCheckBoxCell selectNewColumn = (DataGridViewCheckBoxCell)customDataGridViewNewCustomer.Rows[e.RowIndex].Cells[0];
                bool newcustomerselected = false;
                if (selectNewColumn.Value.ToString()=="False")
                {

                    customDataGridViewNewCustomer.Rows[e.RowIndex].Cells[0].Selected = false;
                    foreach (DataGridViewCell dgCell in customDataGridViewNewCustomer.Rows[e.RowIndex].Cells)
                        dgCell.Style.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    customDataGridViewNewCustomer.Rows[e.RowIndex].Cells[0].Selected = true;
                    newcustomerselected = true;
                    foreach (DataGridViewCell dgCell in customDataGridViewNewCustomer.Rows[e.RowIndex].Cells)
                        dgCell.Style.BackColor = System.Drawing.Color.YellowGreen;
                    foreach (DataGridViewRow dgRow in customDataGridViewExistingCustomer.Rows)
                    {
                        dgRow.Cells[0].Selected = false;
                        foreach (DataGridViewCell dgCell in dgRow.Cells)
                            dgCell.Style.BackColor = System.Drawing.Color.White;
                    }
                    this.customButtonUpdate.Enabled = false;
                }
                customButtonContinue.Enabled = (_nameCheck == false && newcustomerselected);

            }
        }

    }

}
