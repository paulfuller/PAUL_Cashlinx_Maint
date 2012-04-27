/**************************************************************************************************************
* CashlinxDesktop
* LookupCustomer
* This form will show the search criteria for customer lookup
* Sreelatha Rengarajan 3/13/2009 Initial version
 * Sreelatha Rengarajan 6/23/09 Instead of passing the search values from
 *                              this form to the results page, changed the logic to do the db call in this form
 *                              and set the appropriate data tables in cashlinxdesktopsession global
 *                              variables so that the results form can use it to display
 * SR 5/24/2010 Disable Add and View customer butttons when we are in the Hold flow  
 * SR 11/22/2010 Added vendor lookup for the retail sae to vendor flow
 * Madhu 02/07/2011 fix for BZ # 90 To display warning message when user click on cancel button 
**************************************************************************************************************/

using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using CashlinxDesktop.UserControls;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.UserControls;
using Pawn.Logic;
//Odd file lock

namespace Pawn.Forms.Pawn.Customer
{
    public partial class LookupCustomer : Form
    {


        //private variables
        private LookupCustomerSearchData _lookupCustSearch;
        private string _lastName = "";
        private string _loanNumber = "";
        private string _phoneAreaCode = "";
        private string _phoneNumber = "";
        private string _firstName = "";
        private string _custNumber = "";
        private string _dob = "";
        private string _idTypeCode = "";
        private string _idNumber = "";
        private string _idIssuer = "";
        private string _ssn = "";
        private string _idType = "";
        private string _idIssuerCode = "";
        private int _typeOfSearch = -1;
        private string _errorMessage = "";


        private IDType IdType;
        private State idIssuer1;
        private Common.Libraries.Forms.Date dateOfBirth;
        private ComboBox lookupCustomerIDType = new ComboBox();
        private ComboBox lookupCustomerIDIssuer = new ComboBox();
        bool boolLastNameEntered = false;
        bool boolFirstNameEntered = false;
        bool boolDOBEntered = false;
        private Country country;
        private Form ownerFrm;
        private bool isFormValid = false;
        private const int MIN_WILDCARD_LETTERSFORSEARCH = 3;
        private const int MIN_LETTERSFORSEARCH = 1;
        private string strStoreState = "";
        private string strStoreNumber = "";
        string errorCode = "";
        string errorMsg = "";
        DataTable custDatatable = new DataTable();
        DataTable custPhoneDatatable = new DataTable();
        DataTable custIdentDatatable = new DataTable();
        DataTable custAddrDatatable = new DataTable();
        DataTable custEmailDatatable = new DataTable();
        DataTable custNotesDatatable = new DataTable();
        DataTable custStoreCreditDatatable = new DataTable();
        ProcessingMessage procMsg;
        public NavBox NavControlBox;// { get; set; }
        bool retValue = false;
        private bool vendorSearchSelected;


        public LookupCustomer()
        {
            InitializeComponent();
            country = new Country();
            //Set the Error messages for controls that require it
            
            this.phoneAreaCodeTextBox.ErrorMessage = Commons.GetMessageString("LookupPhoneNumberError");
            this.phoneNum1TextBox.ErrorMessage = Commons.GetMessageString("LookupPhoneNumberError");
            this.phoneNum2TextBox.ErrorMessage = Commons.GetMessageString("LookupPhoneNumberError");
            this.dateOfBirth.ErrorMessage = Commons.GetMessageString("InvalidDateOfBirth");

            //Set the IDType and ID Issuer controls
            lookupCustomerIDType = (ComboBox)IdType.Controls[0];
            //Get store state from desktop session
            strStoreState = GlobalDataAccessor.Instance.CurrentSiteId.State;
            this.NavControlBox = new NavBox();
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.RETAIL))
            {
           
                if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.LoanStatus != ProductStatus.LAY)
                {
                    vendorSearchUC.Visible = true;
                    vendorSearchUC.VendorSelectClick += (vendorSearchUC_VendorSelectClick);
                }
            }


            

        }

        void vendorSearchUC_VendorSelectClick()
        {
            this.CustomerPhoneNumRadioBtn.Checked = false;
            this.CustNumberRadioBtn.Checked = false;

            this.IDSearchRadioBtn.Checked = false;
            this.radioButtonSSN.Checked = false;
            this.CustomerInfoRadioBtn.Checked = false;
            this.lookupCustomerLastName.Enabled = false;
            this.lookupCustomerFirstName.Enabled = false;
            this.dateOfBirth.Enabled = false;
            //Set the other search fields disabled
            //Set the Phone number search criteria fields disabled
            this.phoneAreaCodeTextBox.Enabled = false;
            this.phoneNum1TextBox.Enabled = false;
            this.phoneNum2TextBox.Enabled = false;
            //Set the ID search fields disabled
            this.IdType.Enabled = false;
            this.idIssuer1.Enabled = false;
            this.country.Enabled = false;
            this.lookupCustomerIDNumber.Enabled = false;
            //set the customer number search field disabled
            this.lookupCustomerCustNumber.Enabled = false;
            //set the ssn field disabled
            this.socialSecurityNumber.Enabled = false;

            this.lookupCustomerIDType.SelectedIndex = -1;
            vendorSearchSelected = true;


        }


        /// <summary>
        /// When the clear button is clicked, all the fields are cleared
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void lookupCustomerClearButton_Click(object sender, EventArgs e)
        {
            this.lookupCustomerCustNumber.Text = string.Empty;
            this.dateOfBirth.Controls[0].Text = "mm/dd/yyyy";
            this.lookupCustomerFirstName.Text = string.Empty;
            this.lookupCustomerIDIssuer.SelectedIndex = -1;
            this.lookupCustomerIDNumber.Text = string.Empty;
            this.lookupCustomerIDType.SelectedIndex = -1;
            this.lookupCustomerLastName.Text = string.Empty;
            this.phoneAreaCodeTextBox.Text = string.Empty;
            this.phoneNum1TextBox.Text = string.Empty;
            this.phoneNum2TextBox.Text = string.Empty;
            this.socialSecurityNumber.Controls[0].Text = string.Empty;
            this.CustomerInfoRadioBtn.Checked = !vendorSearchUC.Visible;
            this.errorLabel.Text = string.Empty;
            this.customButtonAddCustomer.Visible = false;
            if (vendorSearchUC.Visible)
            vendorSearchUC.VendorSearchClear();
        }





        /// <summary>
        /// When cancel button is clicked, the lookup customer screen should be closed
        /// and the user is shown the cashlinx desktop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookupCustomerCancelButton_Click(object sender, EventArgs e)
        {
            //Madhu to fix the BZ # 90
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.RETAIL) ||
                (GlobalDataAccessor.Instance.DesktopSession.ActivePurchase != null &&
                GlobalDataAccessor.Instance.DesktopSession.ActivePurchase.ProductDataComplete))
            {
                var cancelRes = MessageBox.Show(
                        "Are you sure you want to Cancel this transaction?",
                        "Customer Cancel Message",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                if (cancelRes == DialogResult.No)
                    return;
            }
            isFormValid = true;
            GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
            this.NavControlBox.Action = NavBox.NavAction.CANCEL;
        }


        /// <summary>
        /// When the Find button is clicked, the search criteria information entered  by the 
        /// user is passed to the lookup customer results control by loading it in its member variables
        /// and then the control is loaded in the parent form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookupCustomerFindButton_Click(object sender, EventArgs e)
        {
            errorLabel.Visible = false;
            errorLabel.Text = string.Empty;
            bool vendorSearchCheck = false;
            if (vendorSearchUC.Visible)
            {
                //Check if vendor search criteria is selected
                if (vendorSearchSelected)
                {
                    vendorSearchCheck = true;
                    bool returnValue = vendorSearchUC.VendorFind(out errorMsg);
                    if (errorMsg.Trim().Length > 0)
                    {
                        errorLabel.Text = errorMsg;
                        errorLabel.Visible = true;
                    }

                    if (!returnValue)
                    {
                        if (errorLabel.Text.Length > 0)
                            MessageBox.Show(Commons.GetMessageString("FormErrorSubmitAgain"));

                    }
                    else
                    {
                        //Call the lookup results form
                        if (!errorLabel.Visible)
                        {
                            this.NavControlBox.IsCustom = true;
                            this.NavControlBox.CustomDetail = "LookupVendorResults";
                            this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                        }

                    }
                }
            }
                //Proceed to search results form only if the form is validated
                if (formValidate())
                {
                    isFormValid = true;
                    procMsg = new ProcessingMessage("Loading Customer Search Results");
                    SetButtonState(false);
                    BackgroundWorker bw = new BackgroundWorker();
                    bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                    bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                    bw.RunWorkerAsync();
                    procMsg.ShowDialog(this);
                    if (_errorMessage.Length > 0)
                    {
                        errorLabel.Text = _errorMessage;
                        errorLabel.Visible = true;
                        return;
                    }
                }
                else
                {
                    if (errorLabel.Text.Length > 0 && !vendorSearchCheck)
                    {
                        MessageBox.Show(Commons.GetMessageString("FormErrorSubmitAgain"));
                        errorLabel.Visible = true;
                    }
                }
            

            

        }

        private void SetButtonState(bool enable)
        {
            customButtonFind.Enabled = enable;
            customButtonCancel.Enabled = enable;
            customButtonClear.Enabled = enable;
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            procMsg.Close();
            procMsg.Dispose();
            SetButtonState(true);
            loadLookupCustomerSearchCriteria();
            GlobalDataAccessor.Instance.DesktopSession.ActiveLookupCriteria = _lookupCustSearch;
            if (retValue && custDatatable != null)
            {

                isFormValid = true;
                GlobalDataAccessor.Instance.DesktopSession.CustDataTable=null;
                GlobalDataAccessor.Instance.DesktopSession.CustPhoneDataTable=null;
                GlobalDataAccessor.Instance.DesktopSession.CustAddrDataTable=null;
                GlobalDataAccessor.Instance.DesktopSession.CustIdentDataTable=null;
                GlobalDataAccessor.Instance.DesktopSession.CustEmailDataTable=null;
                 GlobalDataAccessor.Instance.DesktopSession.CustNotesDataTable=null;
                 GlobalDataAccessor.Instance.DesktopSession.CustStoreCreditDataTable=null;
                GlobalDataAccessor.Instance.DesktopSession.CustDataTable = custDatatable;
                if (custAddrDatatable != null)
                    GlobalDataAccessor.Instance.DesktopSession.CustAddrDataTable = custAddrDatatable;
                if (custIdentDatatable != null)
                    GlobalDataAccessor.Instance.DesktopSession.CustIdentDataTable = custIdentDatatable;
                if (custPhoneDatatable != null)
                    GlobalDataAccessor.Instance.DesktopSession.CustPhoneDataTable = custPhoneDatatable;
                if (custEmailDatatable != null)
                    GlobalDataAccessor.Instance.DesktopSession.CustEmailDataTable = custEmailDatatable;
                if (custNotesDatatable != null)
                    GlobalDataAccessor.Instance.DesktopSession.CustNotesDataTable = custNotesDatatable;
                if (custStoreCreditDatatable != null)
                    GlobalDataAccessor.Instance.DesktopSession.CustStoreCreditDataTable = custStoreCreditDatatable;

                //Call the lookup results form
 
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "LookupCustomerResults";
                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {
                //Create the search criteria string to be shown in the error control 
                //If the date of birth is not entered, it should not be shown as '//' in the error message
                //hence need to be formatted
                var strFilteredDOB = string.Empty;
                if (_dob.Equals("mm/dd/yyyy"))
                {
                    strFilteredDOB = string.Empty;
                }
                else
                    strFilteredDOB = _dob;

                var strFilteredPhone = string.Empty;
                if (_phoneAreaCode.Trim().Length > 0 && _phoneNumber.Trim().Length > 0)
                {
                    strFilteredPhone = string.Format("({0}){1}-{2}", _phoneAreaCode, _phoneNumber.Substring(0, 3), _phoneNumber.Substring(3, 4));
                }
                string searchCriteria = string.Format(" [" + " {0} {1} {2} {3} {4} {5} {6} {7} {8} {9} ]", _ssn, _firstName, _lastName, strFilteredDOB, strFilteredPhone, _idTypeCode, _idNumber, _idIssuer, _custNumber, _loanNumber);
                _errorMessage = Commons.GetMessageString("ZeroCustSearchResults") + searchCriteria;
                //SR 5/24/2010 Do not enable the Add button if this is a hold(cust hold, police hold) flow
                //or if this is a shop credit flow
                var cds = GlobalDataAccessor.Instance.DesktopSession;
                string trigger = cds.HistorySession.Trigger;
                if (GlobalDataAccessor.Instance.DesktopSession.ReleaseToClaimant &&
                GlobalDataAccessor.Instance.DesktopSession.PoliceInformation != null || GlobalDataAccessor.Instance.DesktopSession.ShopCreditFlow)
                {
                    this.customButtonAddCustomer.Visible = true;

                }
                else
                {
                    if (!(trigger.Contains("hold")))
                        this.customButtonAddCustomer.Visible = true;
                }
                
            }
            

        }


        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            retValue = getSearchResultsData();
        }


        private bool getSearchResultsData()
        {
            bool b = false;
            CustomerDBProcedures customerDBProcedures = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession);
            if (_lastName.Length != 0 && _firstName.Length != 0 && _dob.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer(_firstName, _lastName, _dob, "", "", "", "", "", "", "", "", strStoreNumber, strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }
            else if (_lastName.Length != 0 && _firstName.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer(_firstName, _lastName, "", "", "", "", "", "", "", "", "", strStoreNumber, strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }

            else if (_lastName.Length != 0 && _dob.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer("", _lastName, _dob, "", "", "", "", "", "", "", "", strStoreNumber, strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }
            else if (_firstName.Length != 0 && _dob.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer(_firstName, "", _dob, "", "", "", "", "", "", "", "", strStoreNumber, strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }
            else if (_phoneNumber.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer("", "", "", "", "", "", "", "", "", _phoneAreaCode, _phoneNumber, strStoreNumber, strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);

            }
            else if (this._idTypeCode.Length != 0 && _idNumber.Length != 0 && _idIssuer.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer("", "", "", "", "", "", _idTypeCode, _idNumber, _idIssuer, "", "", strStoreNumber, strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }
            else if (this._custNumber.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer("", "", "", "", _custNumber, "", "", "", "", "", "", strStoreNumber, strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }
            else if (this._loanNumber.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer("", "", "", "", "", _loanNumber, "", "", "", "", "", strStoreNumber, strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }
            else if (this._ssn.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer("", "", "", _ssn, "", "", "", "", "", "", "", strStoreNumber, strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }
            return b;

        }


        /// <summary>
        /// Function to validate the user input in the lookup form. If there are errors,
        /// the error control of the parent form is set with the message and the function
        /// returns false else returns true
        /// </summary>
        /// <returns></returns>
        private bool formValidate()
        {

            //Flag to specify if the form passed validation
            var boolValidated = true;
            _lastName = string.Empty;
            _firstName = string.Empty;
            _dob = string.Empty;
            _ssn = string.Empty;
            _phoneAreaCode = string.Empty;
            _phoneNumber = string.Empty;
            _idTypeCode = string.Empty;
            _idNumber = string.Empty;
            _idIssuer = string.Empty;
            _custNumber = string.Empty;
            _loanNumber = string.Empty;
            _idType = string.Empty;
            _idIssuerCode = string.Empty;
            //Get Customer Information search criteria
            if (CustomerInfoRadioBtn.Checked)
            {
                _typeOfSearch = (int)searchCriteria.CUSTINFORMATION;
                _lastName = this.lookupCustomerLastName.Text;
                _firstName = this.lookupCustomerFirstName.Text;
                _dob = this.dateOfBirth.Controls[0].Text;
                boolLastNameEntered = true;
                boolFirstNameEntered = true;
                boolDOBEntered = true;

                if (_lastName.Trim().Length == 0)
                {
                    boolLastNameEntered = false;

                }
                if (_firstName.Trim().Length == 0)
                {
                    boolFirstNameEntered = false;
                }
                if (_dob.Equals("mm/dd/yyyy"))
                {
                    boolDOBEntered = false;
                }
                //If the last name is entered but no other information is entered
                if (boolLastNameEntered)
                {
                    if (!boolFirstNameEntered)
                    {
                        if (!boolDOBEntered)
                        {
                            boolValidated = false;
                            this.errorLabel.Text = Commons.GetMessageString("LookupFormCriteriaError");
                        }
                        else
                        {
                            if (!(dateOfBirth.isValid))
                            {
                                boolValidated = false;
                                this.errorLabel.Text = dateOfBirth.ErrorMessage;
                            }
                        }
                    }
                    else
                    //first name is entered
                    //Check that at least 3 characters were entered for first name and 1 character for last name
                    //or 3 characters for last name and 1 character for first name
                    {
                        if (boolDOBEntered)
                        {
                            if (!(dateOfBirth.isValid))
                            {
                                boolValidated = false;
                                this.errorLabel.Text = dateOfBirth.ErrorMessage;
                            }
                            else
                            {
                                boolValidated=checkDateOfBirth();
                                if (!boolValidated)
                                    this.errorLabel.Text = dateOfBirth.ErrorMessage;
                            }
                        }
                        else
                        {
                            if ((_firstName.Trim().Length >= MIN_WILDCARD_LETTERSFORSEARCH && _lastName.Trim().Length >= MIN_LETTERSFORSEARCH) || (_lastName.Trim().Length >= MIN_WILDCARD_LETTERSFORSEARCH && _firstName.Trim().Length >= MIN_LETTERSFORSEARCH))
                            {
                                boolValidated = true;
                                this.errorLabel.Visible = false;
                            }
                            else
                            {
                                boolValidated = false;
                                this.errorLabel.Text = Commons.GetMessageString("LookupWildcardsearchErrorMsg");
                            }
                        }
                    }
                }
                //If first name is entered and no other information is entered
                else if (boolFirstNameEntered)
                {
                    if (!boolDOBEntered)
                    {
                        boolValidated = false;
                        this.errorLabel.Text = Commons.GetMessageString("LookupFormCriteriaError");
                    }
                }
                // if only the DOB is entered
                else if (boolDOBEntered)
                {
                    boolValidated = false;
                    this.errorLabel.Text = Commons.GetMessageString("LookupFormCriteriaError");
                }
                //If no information is entered and submit is clicked after selecting the customer information radio button
                else
                {
                    boolValidated = false;
                    this.errorLabel.Text = Commons.GetMessageString("LookupFormCriteriaError");
                }

            }
            else if (radioButtonSSN.Checked)
            {
                _typeOfSearch = (int)searchCriteria.CUSTSSN;
                if (this.socialSecurityNumber.isValid)
                {
                    boolValidated = true;
                    this.errorLabel.Text = "";
                    _ssn = this.socialSecurityNumber.Controls[0].Text.ToString();
                }
                else if (this.socialSecurityNumber.Controls[0].Text.Length == 9)
                {
                    boolValidated = true;
                    this.errorLabel.Text = "";
                    _ssn = this.socialSecurityNumber.Controls[0].Text.ToString();
                }
                else
                {
                    boolValidated = false;
                    this.socialSecurityNumber.Focus();
                }
            }
            //Get customer phone search criteria
            else if (CustomerPhoneNumRadioBtn.Checked)
            {
                _typeOfSearch = (int)searchCriteria.CUSTPHONENUMBER;
                _phoneAreaCode = phoneAreaCodeTextBox.Text;
                _phoneNumber = this.phoneNum1TextBox.Text + this.phoneNum2TextBox.Text;
                if (_phoneAreaCode.Length == 0 || _phoneNumber.Length == 0)
                {
                    boolValidated = false;
                    this.errorLabel.Text = Commons.GetMessageString("LookupPhoneNumberError");

                }
            }
            //Get Customer ID Search Criteria
            else if (IDSearchRadioBtn.Checked)
            {
                _typeOfSearch = (int)searchCriteria.CUSTID;
                if (lookupCustomerIDType.SelectedIndex >= 1)
                {
                    _idTypeCode = this.lookupCustomerIDType.SelectedValue.ToString();
                    _idType = this.lookupCustomerIDType.Text.ToString();
                    _idNumber = this.lookupCustomerIDNumber.Text;
                    if (Commons.IsStateID(_idTypeCode))
                    {
                        lookupCustomerIDIssuer = (ComboBox)idIssuer1.Controls[0];
                        _idIssuer = this.lookupCustomerIDIssuer.Text.ToString();
                        _idIssuerCode = this.lookupCustomerIDIssuer.SelectedValue.ToString();

                    }
                    else
                    {
                        lookupCustomerIDIssuer = (ComboBox)country.Controls[0];
                        _idIssuer = this.lookupCustomerIDIssuer.SelectedValue.ToString();
                        _idIssuerCode = this.lookupCustomerIDIssuer.Text.ToString();
                    }
                    

                }
                if (_idTypeCode.Length == 0 || _idNumber.Trim().Length == 0 || _idIssuer.Trim().Length == 0)
                {
                    boolValidated = false;
                    this.errorLabel.Text = Commons.GetMessageString("LookupIDError");
                }

            }

            //Get customer Number search criteria
            else if (CustNumberRadioBtn.Checked)
            {
                _typeOfSearch = (int)searchCriteria.CUSTNUMBER;
                _custNumber = this.lookupCustomerCustNumber.Text;
                if (_custNumber.Trim().Length == 0)
                {
                    boolValidated = false;
                    this.errorLabel.Text = Commons.GetMessageString("LookupCustNumberError");
                }
            }

            else
            {
                boolValidated = false;
                this.errorLabel.Text = Commons.GetMessageString("LookupSearchCriteriaError");

            }
            return boolValidated;

        }

        /// <summary>
        /// If arriving at this form from the search results page by clicking on the back button
        /// the original search criteria information must be shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LookupCustomer_Load(object sender, EventArgs e)
        {
            //Set the owner form
            ownerFrm = this.Owner;
            this.NavControlBox.Owner = this;
            strStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            isFormValid = false;
            getSearchCriteria();
            if ( _errorMessage.Length > 0)
            {  
                this.customButtonAddCustomer.Visible = true;
            }         


            if (_typeOfSearch == (int)searchCriteria.CUSTNUMBER)
            {
                this.CustNumberRadioBtn.Checked = true;
                this.lookupCustomerCustNumber.Text = _custNumber;
            }

            else if (_typeOfSearch == (int)searchCriteria.CUSTINFORMATION)
            {
                this.CustomerInfoRadioBtn.Checked = true;
                this.dateOfBirth.Controls[0].Text = _dob;
                this.lookupCustomerFirstName.Text = _firstName;
                this.lookupCustomerLastName.Text = _lastName;
            }

            else if (_typeOfSearch == (int)searchCriteria.CUSTSSN)
            {
                this.radioButtonSSN.Checked = true;
                this.socialSecurityNumber.Controls[0].Text = _ssn;
            }

            else if (_typeOfSearch == (int)searchCriteria.CUSTID)
            {

                this.IDSearchRadioBtn.Checked = true;
                this.lookupCustomerIDNumber.Text = _idNumber;
                if (_idTypeCode != null && _idTypeCode.Trim().Length != 0)
                {

                    foreach (ComboBoxData custidtype in lookupCustomerIDType.Items)
                        if (custidtype.Code == _idTypeCode)
                        {
                            lookupCustomerIDType.SelectedIndex = lookupCustomerIDType.Items.IndexOf(custidtype);
                            break;
                        }
                }
                populateIssuerData();

            }

            else if (_typeOfSearch == (int)searchCriteria.CUSTPHONENUMBER)
            {
                this.CustomerPhoneNumRadioBtn.Checked = true;
                this.phoneAreaCodeTextBox.Text = _phoneAreaCode;
                if (_phoneNumber != null && _phoneNumber.Length == 7)
                {
                    this.phoneNum1TextBox.Text = _phoneNumber.Substring(0, 3);
                    this.phoneNum2TextBox.Text = _phoneNumber.Substring(3, 4);

                }
            }

            //Show error message if any
            if (!string.IsNullOrEmpty(_errorMessage))
            {
                this.errorLabel.Text = _errorMessage;
                _errorMessage = "";
            }
            else
                this.errorLabel.Text = "";
            //By default, the customer information button should be selected 
            this.CustomerInfoRadioBtn.Checked = true;
            this.ActiveControl = this.lookupCustomerCustInfoPanel.Controls["lookupCustomerLastName"];

        }

        private void getSearchCriteria()
        {

                _lookupCustSearch =  GlobalDataAccessor.Instance.DesktopSession.ActiveLookupCriteria;
                if (_lookupCustSearch != null)
                {

                    if (_lookupCustSearch.TypeOfSearch > 0)
                    {
                        _lastName = _lookupCustSearch.LastName;
                        _loanNumber = _lookupCustSearch.LoanNumber.ToString();
                        _phoneAreaCode = _lookupCustSearch.PhoneAreaCode;
                        _phoneNumber = _lookupCustSearch.PhoneNumber;
                        _firstName = _lookupCustSearch.FirstName;
                        _custNumber = _lookupCustSearch.CustNumber;
                        _dob = _lookupCustSearch.DOB;
                        _idTypeCode = _lookupCustSearch.IdTypeCode;
                        _idNumber = _lookupCustSearch.IDNumber;
                        _idIssuer = _lookupCustSearch.IDIssuer;
                        _ssn = _lookupCustSearch.SSN;
                        _typeOfSearch = _lookupCustSearch.TypeOfSearch;
                    }
                    if (_lookupCustSearch.TypeOfSearch == 0)
                        _typeOfSearch = 1;
                }

     
        }


        /// <summary>
        /// Reset the other fields in the form when customer information is selected
        /// as the search criteria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerInfoRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (CustomerInfoRadioBtn.Checked)
            {
                if (vendorSearchUC.Visible)
                {
                    vendorSearchSelected = false;
                    vendorSearchUC.VendorSearchClear();
                }
                this.CustomerPhoneNumRadioBtn.Checked = false;
                this.CustNumberRadioBtn.Checked = false;

                this.IDSearchRadioBtn.Checked = false;
                this.radioButtonSSN.Checked = false;
                //enable the name fields
                this.lookupCustomerLastName.Enabled = true;
                this.lookupCustomerFirstName.Enabled = true;
                this.dateOfBirth.Enabled = true;
                //Set the other search fields disabled
                //Set the Phone number search criteria fields disabled
                this.phoneAreaCodeTextBox.Enabled = false;
                this.phoneNum1TextBox.Enabled = false;
                this.phoneNum2TextBox.Enabled = false;
                //Set the ID search fields disabled
                this.IdType.Enabled = false;
                this.idIssuer1.Enabled = false;
                this.country.Enabled = false;
                this.lookupCustomerIDNumber.Enabled = false;
                //set the customer number search field disabled
                this.lookupCustomerCustNumber.Enabled = false;
                //set the ssn field disabled
                this.socialSecurityNumber.Enabled = false;

                this.lookupCustomerIDType.SelectedIndex = -1;
                this.lookupCustomerLastName.Focus();
            }
        }

        /// <summary>
        /// Reset the other fields when Phone number is selected as the search option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerPhoneNumRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (CustomerPhoneNumRadioBtn.Checked)
            {
                if (vendorSearchUC.Visible)
                {
                    vendorSearchSelected = false;
                    vendorSearchUC.VendorSearchClear();
                }
                this.CustomerInfoRadioBtn.Checked = false;
                this.CustNumberRadioBtn.Checked = false;

                this.IDSearchRadioBtn.Checked = false;
                this.radioButtonSSN.Checked = false;

                //set the Phone number fields enabled
                this.phoneAreaCodeTextBox.Enabled = true;
                this.phoneNum1TextBox.Enabled = true;
                this.phoneNum2TextBox.Enabled = true;

                //Set the name and dob fields disabled
                this.lookupCustomerLastName.Enabled = false;
                this.lookupCustomerFirstName.Enabled = false;
                this.dateOfBirth.Enabled = false;
                //Set the ID search fields disabled
                this.IdType.Enabled = false;
                this.idIssuer1.Enabled = false;
                this.country.Enabled = false;
                this.lookupCustomerIDNumber.Enabled = false;
                //set the customer number search field disabled
                this.lookupCustomerCustNumber.Enabled = false;
                //set the SSN field disabled
                this.socialSecurityNumber.Enabled = false;

                this.lookupCustomerIDType.SelectedIndex = -1;
                this.phoneAreaCodeTextBox.Focus();
            }
        }

        /// <summary>
        /// Reset the other fields in the form when ID is selected as the search option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDSearchRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (IDSearchRadioBtn.Checked)
            {
                if (vendorSearchUC.Visible)
                {
                    vendorSearchSelected = false;
                    vendorSearchUC.VendorSearchClear();
                }
                this.lookupCustomerIDType.SelectedIndex = lookupCustomerIDType.FindString(StateIdTypes.DRIVERLICENSE);
                this.CustomerInfoRadioBtn.Checked = false;
                this.CustNumberRadioBtn.Checked = false;
                this.CustomerPhoneNumRadioBtn.Checked = false;
                this.radioButtonSSN.Checked = false;

                //Set the ID search fields enabled
                this.IdType.Enabled = true;
                this.idIssuer1.Enabled = true;
                this.country.Enabled = true;
                this.lookupCustomerIDNumber.Enabled = true;


                //set the Phone number fields disabled
                this.phoneAreaCodeTextBox.Enabled = false;
                this.phoneNum1TextBox.Enabled = false;
                this.phoneNum2TextBox.Enabled = false;

                //Set the name and dob fields disabled
                this.lookupCustomerLastName.Enabled = false;
                this.lookupCustomerFirstName.Enabled = false;
                this.dateOfBirth.Enabled = false;
                //set the customer number search field disabled
                this.lookupCustomerCustNumber.Enabled = false;
                //set the ssn field disabled
                this.socialSecurityNumber.Enabled = false;

            }
        }

        /// <summary>
        /// Reset the other fields in the form when customer number is selected as the search option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustNumberRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (CustNumberRadioBtn.Checked)
            {
                if (vendorSearchUC.Visible)
                {
                    vendorSearchSelected = false;
                    vendorSearchUC.VendorSearchClear();
                }
                this.CustomerInfoRadioBtn.Checked = false;
                this.IDSearchRadioBtn.Checked = false;
                this.CustomerPhoneNumRadioBtn.Checked = false;
                this.radioButtonSSN.Checked = false;

                //Set the ID search fields disabled
                this.IdType.Enabled = false;
                this.idIssuer1.Enabled = false;
                this.country.Enabled = false;
                this.lookupCustomerIDNumber.Enabled = false;

                //set the Phone number fields disabled
                this.phoneAreaCodeTextBox.Enabled = false;
                this.phoneNum1TextBox.Enabled = false;
                this.phoneNum2TextBox.Enabled = false;
                //Set the name and dob fields disabled
                this.lookupCustomerLastName.Enabled = false;
                this.lookupCustomerFirstName.Enabled = false;
                this.dateOfBirth.Enabled = false;
                //set the customer number search field enabled
                this.lookupCustomerCustNumber.Enabled = true;
                //set the SSN field disabled
                this.socialSecurityNumber.Enabled = false;

                this.lookupCustomerIDType.SelectedIndex = -1;
                this.lookupCustomerCustNumber.Focus();
            }
        }

        /// <summary>
        /// When there are 0 records returned by the search, the Add Customer
        /// button becomes visible and when clicked will take the user to the
        /// Add Customer Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            isFormValid = true;
            //Create a new customer object and set the fields entered for search
            CustomerVO newCustomer = new CustomerVO {NewCustomer = true};

            AddressVO newAddr = new AddressVO
                                    {
                                        State_Code = strStoreState,
                                        ContactTypeCode = CustomerAddressTypes.HOME_ADDRESS,
                                        ContMethodTypeCode = "POSTALADDR"
                                    };
            newCustomer.addAddress(newAddr);

            if (_typeOfSearch == (int)searchCriteria.CUSTINFORMATION)
            {
                newCustomer.FirstName = _firstName;
                newCustomer.LastName = _lastName;
                if (!_dob.Equals("mm/dd/yyyy"))
                    newCustomer.DateOfBirth = DateTime.Parse(_dob);
            }
            else if (_typeOfSearch == (int)searchCriteria.CUSTID)
            {
                IdentificationVO newId = new IdentificationVO
                                             {
                                                 IdType = _idTypeCode,
                                                 IdValue = _idNumber,
                                                 IdIssuer = _idIssuer
                                             };
                newCustomer.addIdentity(newId);
            }
            else if (_typeOfSearch == (int)searchCriteria.CUSTPHONENUMBER)
            {
                ContactVO newContact = new ContactVO
                                           {
                                               ContactAreaCode = _phoneAreaCode,
                                               ContactPhoneNumber = _phoneNumber,
                                               TelecomNumType = CustomerPhoneTypes.VOICE_NUMBER,
                                               ContactType = CustomerPhoneTypes.HOME_NUMBER
                                           };
                newCustomer.addContact(newContact);
            }
            else if (_typeOfSearch == (int)searchCriteria.CUSTSSN)
            {
                newCustomer.SocialSecurityNumber = _ssn;
            }

            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = newCustomer;
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "CreateCustomer";
            this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }



        /// <summary>
        /// Handles the End, Home and Enter keys for this form
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                EventArgs e = new EventArgs();
                this.lookupCustomerFindButton_Click(this, e);
            }
            if (keyData == Keys.Home)
            {
                this.SelectNextControl(this.lookupCustomerFirstName, false, true, true, true);
            }
            if (keyData == Keys.End)
            {
                this.SelectNextControl(this.customButtonFind, true, true, true, true);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }




        /// <summary>
        /// Method to load the Issuer agency combo box with
        /// appropriate values based on the Issue type selected
        /// If the ID type is state issued Id or concealed weapons permit or Driver's license, US states will be shown in Issuer combobox
        /// If the ID type is anything else, a list of countries will be shown in the Issuer combobox
        /// </summary>

        private void IdType_Leave(object sender, EventArgs e)
        {
            populateIssuerData();
            this.lookupCustomerIDNumber.Focus();


        }

        private void populateIssuerData()
        {
            ComboBox idType = new ComboBox();
            string selectedIDType;
            idType = (ComboBox)this.IdType.Controls[0];
            if (idType != null)
            {
                if (idType.SelectedIndex >= 0)
                {
                    selectedIDType = idType.SelectedValue.ToString();
                }
                else
                    selectedIDType = "";

                switch (selectedIDType)
                {
                    case StateIdTypes.STATE_IDENTIFICATION_ID:
                        this.lookupCustomerIDSearchPanel.Controls.Add(this.idIssuer1);
                        this.lookupCustomerIDSearchPanel.Controls.Remove(country);
                        this.idIssuer1.SetBounds(438, 31, idIssuer1.Width, idIssuer1.Height);
                        break;
                    case StateIdTypes.DRIVERLICENSE:
                        this.lookupCustomerIDSearchPanel.Controls.Add(this.idIssuer1);
                        this.lookupCustomerIDSearchPanel.Controls.Remove(country);
                        this.idIssuer1.SetBounds(438, 31, idIssuer1.Width, idIssuer1.Height);
                        break;
                    case StateIdTypes.CONCEALED_WEAPONS_PERMIT:
                        this.lookupCustomerIDSearchPanel.Controls.Add(this.idIssuer1);
                        this.lookupCustomerIDSearchPanel.Controls.Remove(country);
                        this.idIssuer1.SetBounds(438, 31, idIssuer1.Width, idIssuer1.Height);
                        break;
                    default:
                        this.lookupCustomerIDSearchPanel.Controls.Remove(this.idIssuer1);
                        this.lookupCustomerIDSearchPanel.Controls.Add(country);
                        this.country.SetBounds(438, 31, country.Width, country.Height);
                        break;
                }
                //If the form is loaded again, show the selected state or country
                if (!string.IsNullOrEmpty(_idIssuer))
                {
                    if (this.lookupCustomerIDSearchPanel.Controls.Contains(idIssuer1))
                    {
                        ComboBox issuerState = (ComboBox)idIssuer1.Controls[0];
                        if (issuerState != null)
                        {
                            foreach (USState custIssuerState in issuerState.Items)
                                if (custIssuerState.ShortName == _idIssuer)
                                {
                                    issuerState.SelectedIndex = issuerState.Items.IndexOf(custIssuerState);
                                    break;
                                }
                        }
                    }
                    else //it is a country
                    {
                        ComboBox issuerCountry = (ComboBox)country.Controls[0];
                        if (issuerCountry != null)
                        {
                            foreach (CountryData custIssuerCountry in issuerCountry.Items)
                                if (custIssuerCountry.Code == _idIssuer)
                                {
                                    issuerCountry.SelectedIndex = issuerCountry.Items.IndexOf(custIssuerCountry);
                                    break;
                                }
                        }

                    }
                }
            }

        }


        private void radioButtonSSN_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonSSN.Checked)
            {
                if (vendorSearchUC.Visible)
                {
                    vendorSearchSelected = false;
                    vendorSearchUC.VendorSearchClear();
                }
                this.CustomerInfoRadioBtn.Checked = false;
                this.IDSearchRadioBtn.Checked = false;
                this.CustNumberRadioBtn.Checked = false;
                this.CustomerPhoneNumRadioBtn.Checked = false;

                //Set the ID search fields disabled
                this.IdType.Enabled = false;
                this.idIssuer1.Enabled = false;
                this.country.Enabled = false;
                this.lookupCustomerIDNumber.Enabled = false;

                //set the Phone number fields disabled
                this.phoneAreaCodeTextBox.Enabled = false;
                this.phoneNum1TextBox.Enabled = false;
                this.phoneNum2TextBox.Enabled = false;

                //Set the name and dob fields disabled
                this.lookupCustomerLastName.Enabled = false;
                this.lookupCustomerFirstName.Enabled = false;
                this.dateOfBirth.Enabled = false;

                //set the customer number search field disabled
                this.lookupCustomerCustNumber.Enabled = false;


                //set the social security number field enabled
                this.socialSecurityNumber.Enabled = true;

                this.lookupCustomerIDType.SelectedIndex = -1;
                this.socialSecurityNumber.Focus();
            }

        }

        private void phoneAreaCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (phoneAreaCodeTextBox.Text.Length == phoneAreaCodeTextBox.MaxLength)
            {
                this.SelectNextControl(phoneAreaCodeTextBox, true, true, true, true);

            }

        }

        private void phoneNum1TextBox_TextChanged(object sender, EventArgs e)
        {
            if (phoneNum1TextBox.Text.Length == phoneNum1TextBox.MaxLength)
            {
                this.SelectNextControl(phoneNum1TextBox, true, true, true, true);

            }

        }

        private void LookupCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !isFormValid;
        }

        /// <summary>
        /// This method is to load the search criteria object with values entered
        /// in the search criteria fields in the form
        /// </summary>
        /// <param name="lookupCustForm"></param>
        private void loadLookupCustomerSearchCriteria()
        {
            _lookupCustSearch = new LookupCustomerSearchData();
            //If search type was customer information
            if (_typeOfSearch == (int)searchCriteria.CUSTINFORMATION)
            {
                _lookupCustSearch.FirstName = _firstName;
                _lookupCustSearch.LastName = _lastName;
                _lookupCustSearch.DOB = _dob;
            }
            //search type was phone number
            else if (_typeOfSearch == (int)searchCriteria.CUSTPHONENUMBER)
            {
                if (_phoneNumber != null && _phoneNumber.Trim().Length != 0)
                {
                    _lookupCustSearch.PhoneAreaCode = _phoneAreaCode;
                    _lookupCustSearch.PhoneNumber = _phoneNumber;
                }
            }
            //search type was ID search
            else if (_typeOfSearch == (int)searchCriteria.CUSTID)
            {
                _lookupCustSearch.IdTypeCode = _idTypeCode;
                _lookupCustSearch.IDNumber = _idNumber;
                if (Commons.IsStateID(_idTypeCode))
                {
                    _lookupCustSearch.IDIssuer = _idIssuerCode;
                    _lookupCustSearch.IdIssuerCode = _idIssuer;
                }
                else
                {
                    _lookupCustSearch.IDIssuer = _idIssuer;
                    _lookupCustSearch.IdIssuerCode = _idIssuerCode;

                }
                _lookupCustSearch.IdTypeDesc = _idType;
            }
            //search type was customer number
            else if (_typeOfSearch == (int)searchCriteria.CUSTNUMBER)
            {
                _lookupCustSearch.CustNumber = _custNumber;
            }
            //search type was loan number
            else if (_typeOfSearch == (int)searchCriteria.CUSTLOANNUMBER)
            {
                _lookupCustSearch.LoanNumber = _loanNumber.ToString();
            }
            else
                //search was by SSN
                _lookupCustSearch.SSN = _ssn;

            _lookupCustSearch.TypeOfSearch = _typeOfSearch;
        }

        private void dateOfBirth_Leave(object sender, EventArgs e)
        {
            bool retVal=checkDateOfBirth();
        }

        private bool checkDateOfBirth()
        {
            if (dateOfBirth.isValid)
            {
                _dob = this.dateOfBirth.Controls[0].Text;
                if (!_dob.Equals("mm/dd/yyyy"))
                {
                    int age = Commons.getAge(_dob, ShopDateTime.Instance.ShopDate);
                    if (age <= 0)
                    {

                        MessageBox.Show(Commons.GetMessageString("InvalidDateOfBirth"));
                        return false;
                    }
                    return true;
                }
                return true;
            }
            return false;
        }
    }
}
