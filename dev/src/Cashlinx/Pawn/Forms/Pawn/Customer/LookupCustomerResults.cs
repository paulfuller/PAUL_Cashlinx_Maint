/********************************************************************
* CashlinxDesktop
* LookupCustomerResults
* This form will show the results of the search for a customer
* based on criteria entered in the lookup customer search screen
* Sreelatha Rengarajan 3/13/2009 Initial version
 * SR 5/24/2010 Disable Add and View Customer buttons if we are in the customer hold or police hold flow
*********************************************************************************************************/

using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;
//Odd file lock
namespace Pawn.Forms.Pawn.Customer
{
    public partial class LookupCustomerResults : Form
    {


        private BindingSource _bindingSource1;
        private CustomerVO _customerObject;
        private DataTable _customers;
        private DataTable _customerPhoneNumbers;
        private DataTable _customerIdentities;
        private DataTable _customerAddresses;
        private DataTable _customerEmails;
        private DataTable _customerNotes;
        private DataTable _customerStoreCredit;
        private Form ownerFrm;

        private DataTable _custDatatable;
        private DataTable _custPhoneDatatable;
        private DataTable _custIdentDatatable;
        private DataTable _custAddrDatatable;
        private DataTable _custEmailDatatable;
        private DataTable _custNotesDatatable;
        private DataTable _custStoreCreditDatatable;
        public NavBox NavControlBox;// { get; set; }

        public LookupCustomerResults()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
        }


        private void lookupCustomerBackButton_Click(object sender, EventArgs e)
        {
            loadLookupForm();

        }

        private void addCustomerButton_Click(object sender, EventArgs e)
        {
            string trigger = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger;
            //CashlinxDesktopSession.Instance.FormState = CashlinxDesktopSession.CustomerFormStates.ADDCUSTOMER;            
            //CustomerController.NavigateUser(ownerFrm);
            var newCustomer = new CustomerVO
            {
                NewCustomer = true
            };
            //get the Search criteria entered by the user
            var searchCriteria = GlobalDataAccessor.Instance.DesktopSession.ActiveLookupCriteria;
            
            //Get store state from desktop session
            var strStoreState = string.Empty;
            strStoreState = GlobalDataAccessor.Instance.CurrentSiteId.State;
            var newAddr = new AddressVO
            {
                State_Code = strStoreState,
                ContactTypeCode = CustomerAddressTypes.HOME_ADDRESS,
                ContMethodTypeCode = "POSTALADDR"
            };
            newCustomer.addAddress(newAddr);
            newCustomer.FirstName = searchCriteria.FirstName;
            newCustomer.LastName = searchCriteria.LastName;
            if (!string.IsNullOrEmpty(searchCriteria.DOB) && searchCriteria.DOB != "mm/dd/yyyy")
            newCustomer.DateOfBirth = Utilities.GetDateTimeValue(searchCriteria.DOB);
            if (!string.IsNullOrEmpty(searchCriteria.IDIssuer))
            {
                var idData = new IdentificationVO
                {
                    IdIssuer = searchCriteria.IDIssuer,
                    IdValue=searchCriteria.IDNumber,
                    IdIssuerCode=searchCriteria.IdIssuerCode,
                    IdType=searchCriteria.IdTypeCode
                };
            newCustomer.addIdentity(idData);
            if (!string.IsNullOrEmpty(searchCriteria.PhoneNumber))
            {
                var phoneData = new ContactVO
                {
                    ContactPhoneNumber=searchCriteria.PhoneNumber,
                    ContactAreaCode=searchCriteria.PhoneAreaCode
                };
                newCustomer.addContact(phoneData);
            }
                
            newCustomer.SocialSecurityNumber = searchCriteria.SSN;
            }
            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = newCustomer;
            if (trigger.Equals("newpawnloan", StringComparison.OrdinalIgnoreCase))
            {


                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "ManagePawnApplication";
                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {

                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "CreateCustomer";
                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

            }

        }

        private void LookupCustomerResults_Load(object sender, EventArgs e)
        {

            //Set the owner form
            ownerFrm = this.Owner;
            this.NavControlBox.Owner = this;
            //Get data from the desktop session global variables

            _custDatatable = GlobalDataAccessor.Instance.DesktopSession.CustDataTable;
            _custPhoneDatatable = GlobalDataAccessor.Instance.DesktopSession.CustPhoneDataTable;
            _custAddrDatatable = GlobalDataAccessor.Instance.DesktopSession.CustAddrDataTable;
            _custIdentDatatable = GlobalDataAccessor.Instance.DesktopSession.CustIdentDataTable;
            _custEmailDatatable = GlobalDataAccessor.Instance.DesktopSession.CustEmailDataTable;
            _custNotesDatatable = GlobalDataAccessor.Instance.DesktopSession.CustNotesDataTable;
            _custStoreCreditDatatable = GlobalDataAccessor.Instance.DesktopSession.CustStoreCreditDataTable;

            errorLabel.Visible = false;
            try
            {
                if (_custDatatable != null)
                {
                    // If there are more than 15 records returned, show an error message
                    // in the error control of the parent but show only 15 records
                    //
                    var customerFilteredTable = _custDatatable.Clone();
                    //The call to database returns address and phone number in
                    //several fields. They have to be merged in order to show on the form
                    customerFilteredTable.Columns.Add("Address1_text");
                    customerFilteredTable.Columns.Add("Address2_text");
                    customerFilteredTable.Columns.Add("city_name");
                    customerFilteredTable.Columns.Add("state_name");
                    customerFilteredTable.Columns.Add("postal_code");
                    customerFilteredTable.Columns.Add("Phone");
                    customerFilteredTable.Columns.Add("Ident_desc");
                    customerFilteredTable.Columns.Add("Issued_number");
                    customerFilteredTable.Columns.Add("Issuer_Name");

                    //store this for when the user selects a customer
                    _customers = _custDatatable.Copy();
                    //Add primary key to the table
                    DataColumn[] key = new DataColumn[1];
                    key[0] = _customers.Columns["party_id"];
                    _customers.PrimaryKey = key;

                    if (_custPhoneDatatable != null)
                        _customerPhoneNumbers = _custPhoneDatatable.Copy();
                    if (_custIdentDatatable != null)
                        _customerIdentities = _custIdentDatatable.Copy();
                    if (_custAddrDatatable != null)
                        _customerAddresses = _custAddrDatatable.Copy();
                    if (_custEmailDatatable != null)
                        _customerEmails = _custEmailDatatable.Copy();
                    if (_custNotesDatatable != null)
                        _customerNotes = _custNotesDatatable.Copy();
                    if (_custStoreCreditDatatable != null)
                        _customerStoreCredit = _custStoreCreditDatatable.Copy();

                    _bindingSource1 = new BindingSource();
                    if (_custDatatable.Rows.Count > 15)
                    {
                        this.customButtonAddCustomer.Visible = false;
                        errorLabel.Text = "* Warning : " + Commons.GetMessageString("MultipleCustSearchResults");
                        errorLabel.Visible = true;
                        DataRow[] customerFilteredRows = _custDatatable.Select("customer_row_number <=15");
                        foreach (DataRow dr in customerFilteredRows)
                            customerFilteredTable.ImportRow(dr);
                    }
                    else
                    {
                        this.customButtonAddCustomer.Visible = true;
                        foreach (DataRow dr in _custDatatable.Rows)
                            customerFilteredTable.ImportRow(dr);
                    }
                    //SR 5/24/2010 Do not enable the Add button if this is a hold(cust hold, police hold) flow
                    var cds = GlobalDataAccessor.Instance.DesktopSession;
                    string trigger = cds.HistorySession.Trigger;
                    if (GlobalDataAccessor.Instance.DesktopSession.ReleaseToClaimant &&
                    GlobalDataAccessor.Instance.DesktopSession.PoliceInformation != null || GlobalDataAccessor.Instance.DesktopSession.ShopCreditFlow)
                    {
                        this.customButtonAddCustomer.Visible = true;
                        customButtonView.Enabled = false;

                    }
                    else
                    {
                        if (trigger.Contains("hold"))
                        {
                            this.customButtonAddCustomer.Visible = false;
                            customButtonView.Enabled = false;
                        }
                    }

                    //Add the phone number and identity information pertaining to the customer
                    var custPartyId = string.Empty;
                    var custSearchResults = customerFilteredTable.Rows;
                    foreach (DataRow dr in custSearchResults)
                    {

                        custPartyId = dr["party_id"].ToString();
                        //Get the Address info from  address cursor for the party id and
                        //show the home address

                        if (_custAddrDatatable != null)
                        {
                            string expression = string.Format("party_id='{0}' and contact_type_code='HOME'", custPartyId);
                            //string address;
                            var custAddrRows = _custAddrDatatable.Select(expression);
                            if (custAddrRows.Length > 0)
                            {
                                //MHM 8/3/2010 Separate the address into multiple fields
                                //Concatenate the address and phone number fields returned from the stored procedure
                                //address = custAddrRows[0]["Address1_text"] + "," + custAddrRows[0]["Address2_text"] + "," + custAddrRows[0]["city_name"] + "," + custAddrRows[0]["state_name"] + "," + custAddrRows[0]["postal_code"];
                                //dr.SetField("Address", address.ToUpper());
                                dr.SetField("Address1_text", custAddrRows[0]["Address1_text"]);
                                dr.SetField("Address2_text", custAddrRows[0]["Address2_text"]);
                                dr.SetField("city_name", custAddrRows[0]["city_name"]);
                                dr.SetField("state_name", custAddrRows[0]["state_code"]);
                                dr.SetField("postal_code", custAddrRows[0]["postal_code"]);
                            }
                        }
                        //Get the identification data from ident cursor for the party id
                        //and show only the first identitiy
                        if (_custIdentDatatable != null)
                        {
                            var custIdentRows = _custIdentDatatable.Select("party_id='" + custPartyId + "'", "creation_date desc");

                            if (custIdentRows.Length > 0)
                            {
                                dr["ident_desc"] = custIdentRows[0]["datedidenttypedesc"].ToString();
                                dr["Issued_number"] = custIdentRows[0]["issuednumber"];
                                dr["Issuer_name"] = custIdentRows[0]["state_name"];
                            }
                        }
                        else
                        {
                            dr["ident_desc"] = string.Empty;
                            dr["Issued_number"] = string.Empty;
                            dr["Issuer_name"] = string.Empty;
                        }
                        //get the phone number data
                        //The phone number whose teleusrdef0text is set to true will be displayed
                        if (_custPhoneDatatable != null)
                        {
                            var custPhoneRows = _custPhoneDatatable.Select(string.Format("party_id='{0}' and teleusrdef0text=true", custPartyId));
                            if (custPhoneRows.Length > 0)
                            {
                                if (custPhoneRows[0]["telecomnumber"].ToString().Length == 7)
                                    dr["phone"] = string.Format("({0}) {1}-{2}", custPhoneRows[0]["areadialnumcode"], custPhoneRows[0]["telecomnumber"].ToString().Substring(0, 3), custPhoneRows[0]["telecomnumber"].ToString().Substring(3, 4));
                                else
                                    dr["phone"] = string.Format("({0}) {1}", custPhoneRows[0]["areadialnumcode"], custPhoneRows[0]["telecomnumber"].ToString());
                            }
                        }
                        else
                            dr["phone"] = string.Empty;
                    }


                    _bindingSource1.DataSource = customerFilteredTable;

                    this.lookupCustomerResultsGrid.AutoGenerateColumns = false;
                    this.lookupCustomerResultsGrid.DataSource = _bindingSource1;

                    this.lookupCustomerResultsGrid.Columns[1].DataPropertyName = "cust_last_name";
                    this.lookupCustomerResultsGrid.Columns[2].DataPropertyName = "cust_first_name";
                    this.lookupCustomerResultsGrid.Columns[3].DataPropertyName = "Address1_text";
                    this.lookupCustomerResultsGrid.Columns[4].DataPropertyName = "Address2_text";
                    this.lookupCustomerResultsGrid.Columns[5].DataPropertyName = "city_name";
                    this.lookupCustomerResultsGrid.Columns[6].DataPropertyName = "state_name";
                    this.lookupCustomerResultsGrid.Columns[7].DataPropertyName = "postal_code";
                    this.lookupCustomerResultsGrid.Columns[8].DataPropertyName = "date_of_birth";
                    this.lookupCustomerResultsGrid.Columns[9].DataPropertyName = "Ident_desc";
                    this.lookupCustomerResultsGrid.Columns[10].DataPropertyName = "Issued_number";
                    this.lookupCustomerResultsGrid.Columns[11].DataPropertyName = "Issuer_Name";
                    this.lookupCustomerResultsGrid.Columns[12].DataPropertyName = "phone";

                    this.lookupCustomerResultsGrid.Focus();
                }
                else
                //If the customer data table is empty return back to caller
                {
                    return;
                }

            }
            catch (SystemException ex)
            {
                BasicExceptionHandler.Instance.AddException(Commons.GetMessageString("LookupResultsExceptionError"), ex);
                MessageBox.Show(Commons.GetMessageString("ProcessingError"));
                this.NavControlBox.Action = NavBox.NavAction.BACK;
            }

            finally
            {
                _custDatatable = null;
                _custPhoneDatatable = null;
                _custIdentDatatable = null;
            }

        }







        private void viewButton_Click(object sender, EventArgs e)
        {
            //Populate the customer object
            LoadSelectedCustomer();
            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = _customerObject;
            //To Do: Until we figure out how to identify the product
            //The requirement is that if the product identified is PAWN go to
            //view customer information else go to view customer summary
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "ViewCustomerInformationReadOnly";
            this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;


        }

        /// <summary>
        /// This method loads the look up customer control in the parent form
        /// and removes the lookup customer results control from the parent form
        /// </summary>
        private void loadLookupForm()
        {

            this.NavControlBox.Action = NavBox.NavAction.BACK;
        }

        /// <summary>
        /// When a row is double clicked, user should be taken to Edit 
        /// after creating a customer object with all the data of the selected customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookupCustomerResultsGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            showCustomer();
        }

        private void showCustomer()
        {
            var dgr = DialogResult.Retry;
            do
            {
                try
                {
                    LoadSelectedCustomer();

                    
                    if (_customerObject != null)
                    {
                        //Set the selected customer in the desktop session as the ActiveCustomer
                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = _customerObject;
                        NavControlBox.IsCustom = true;
                        NavControlBox.CustomDetail = "Complete";
                        NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;


                        break;
                    }
                    GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                    this.NavControlBox.Action = NavBox.NavAction.CANCEL;
                }
                catch (Exception ex)
                {
                    BasicExceptionHandler.Instance.AddException(Commons.GetMessageString("SelectedCustomerLoadError"), ex);
                    dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);
                    if (dgr == DialogResult.Retry)
                        continue;
                    else
                    {
                        _customerObject = null;
                        break;
                    }
                }
            } while (dgr == DialogResult.Retry);

        }

        private void LoadSelectedCustomer()
        {

            //Get the selected row from the grid and get the corresponding data from the customer table
            //for the selected customer
            DataRowView drv = (DataRowView)(_bindingSource1.Current);
            DataRow selectedCustomerRow = _customers.Rows.Find(drv.Row["party_id"]);
            string selectedPartyId = selectedCustomerRow.ItemArray[(int)customerrecord.PARTY_ID].ToString();
            _customerObject = CustomerProcedures.getCustomerDataInObject(selectedPartyId, _customerIdentities,
                _customerPhoneNumbers, _customerAddresses, _customerEmails,
                _customerNotes, _customerStoreCredit, selectedCustomerRow);

            //return _customerObject;
        }

        //when the select button is clicked, the selected customer's information
        //should be loaded in manage pawn application
        private void lookupCustomerResultsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                showCustomer();
            }
        }






    }
}
