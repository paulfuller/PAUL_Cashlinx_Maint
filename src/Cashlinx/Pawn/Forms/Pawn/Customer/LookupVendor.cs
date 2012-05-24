/**************************************************************************************************************
* CashlinxDesktop
* LookupCustomer
* This form will show the search criteria for Vendor lookup
* Tracy McConnell 8/18/10 Initial version
**************************************************************************************************************/

using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.Customer
{
    public partial class LookupVendor : Form
    {


        //private variables
        private LookupVendorSearchData _lookupVendSearch;
        private LookupCustomerSearchData _lookupCustSearch;

        private string _vendName = "";
        private string _vendTaxID = "";
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
        DataTable vendDatatable = new DataTable();
        DataTable custPhoneDatatable = new DataTable();
        DataTable custIdentDatatable = new DataTable();
        DataTable custAddrDatatable = new DataTable();
        DataTable custEmailDatatable = new DataTable();
        DataTable custNotesDatatable = new DataTable();
        DataTable custStoreCreditDatatable = new DataTable();
        ProcessingMessage procMsg;
        public NavBox NavControlBox;// { get; set; }
        bool retValue = false;


        public LookupVendor()
        {
            InitializeComponent();
            country = new Country();
            //Set the Error messages for controls that require it
            
            this.phoneAreaCodeTextBox.ErrorMessage = Commons.GetMessageString("LookupPhoneNumberError");
            this.phoneNum1TextBox.ErrorMessage = Commons.GetMessageString("LookupPhoneNumberError");
            this.phoneNum2TextBox.ErrorMessage = Commons.GetMessageString("LookupPhoneNumberError");
            this.dateOfBirth.ErrorMessage = Commons.GetMessageString("InvalidDateOfBirth");

            //By default, the customer information button should be selected for search 
            //for a pawn loan and the last name textbox should be in focus
            this.VendorRadiobtn.Checked = true;
            ((RadioButton)this.vendorSearchGroup.Controls["VendorNameRadiobtn"]).Checked = true;
            this.ActiveControl = this.vendorSearchGroup.Controls["lookupVendorName"];           
            //Set the IDType and ID Issuer controls
            lookupCustomerIDType = (ComboBox)IdType.Controls[0];
            //Get store state from desktop session
            strStoreState = GlobalDataAccessor.Instance.CurrentSiteId.State;
            this.NavControlBox = new NavBox();
            

        }


        /// <summary>
        /// When the clear button is clicked, all the fields are cleared
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void clearButton_Click(object sender, EventArgs e)
        {
            this.lookupCustomerCustNumber.Text = string.Empty;
            this.dateOfBirth.Controls[0].Text = "mm/dd/yyyy";
            this.lookupCustomerFirstName.Text = string.Empty;
            this.lookupCustomerIDIssuer.SelectedIndex = -1;
            this.lookupCustomerIDNumber.Text = string.Empty;
            this.lookupCustomerIDType.SelectedIndex = -1;
            this.lookupCustomerLastName.Text = string.Empty;
            this.lookupCustomerLoanNumber.Text = string.Empty;
            this.phoneAreaCodeTextBox.Text = string.Empty;
            this.phoneNum1TextBox.Text = string.Empty;
            this.phoneNum2TextBox.Text = string.Empty;
            this.socialSecurityNumber.Controls[0].Text = string.Empty;
            ((TextBox)this.vendorSearchGroup.Controls["lookupVendorName"]).Text = "";
            ((TextBox)this.vendorSearchGroup.Controls["lookupVendorTaxID"]).Text = "";
            this.VendorRadiobtn.Checked = true;
            this.errorLabel.Text = "";
            this.customButtonAddCustomer.Visible = false;
            this.customButtonAddVendor.Visible = false;

            this.CustomerInfoRadioBtn.Checked = false;
            this.IDSearchRadioBtn.Checked = false;
            this.CustNumberRadioBtn.Checked = false;
            this.CustomerPhoneNumRadioBtn.Checked = false;
            this.LoanNumberRadiobtn.Checked = false;
        }





        /// <summary>
        /// When cancel button is clicked, the lookup customer screen should be closed
        /// and the user is shown the cashlinx desktop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
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
                if (errorLabel.Text.Length > 0)
                    MessageBox.Show(Commons.GetMessageString("FormErrorSubmitAgain"));
            }

        }

        /// <summary>
        /// When the Find button is clicked, the search criteria information entered  by the 
        /// user is passed to the lookup customer results control by loading it in its member variables
        /// and then the control is loaded in the parent form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findButton_Click(object sender, EventArgs e)
        {
            if (this.VendorRadiobtn.Checked)
                lookupVendorFindButton_Click(sender, e);
            else
                lookupCustomerFindButton_Click(sender, e);
        }


        /// <summary>
        /// When the Find button is clicked, the search criteria information entered  by the 
        /// user is passed to the lookup customer results control by loading it in its member variables
        /// and then the control is loaded in the parent form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookupVendorFindButton_Click(object sender, EventArgs e)
        {
            errorLabel.Text = "";
            //Proceed to search results form only if the form is validated
            if (formValidate())
            {
                isFormValid = true;
                _errorMessage = "";
                procMsg = new ProcessingMessage("Loading Vendor Search Results");
                SetButtonState(false);
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(bw_findVendor);
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_vendorSearchCompleted);
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
                if (errorLabel.Text.Length > 0)
                    MessageBox.Show(Commons.GetMessageString("FormErrorSubmitAgain"));
            }

        }

        private void SetButtonState(bool enable)
        {
            customButtonFind.Enabled = enable;
            customButtonCancel.Enabled = enable;
            customButtonClear.Enabled = enable;
            
        }

        void bw_vendorSearchCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            procMsg.Close();
            procMsg.Dispose();
            SetButtonState(true);
            loadLookupVendorSearchCriteria();
            GlobalDataAccessor.Instance.DesktopSession.LookupCriteria = _lookupVendSearch;
            if (retValue && vendDatatable != null)
            {

                isFormValid = true;
                GlobalDataAccessor.Instance.DesktopSession.VendDataTable = vendDatatable;
                GlobalDataAccessor.Instance.DesktopSession.VendDataTable.DefaultView.
                        Sort = "ROWNUM ASC";

                //Call the lookup results form

                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "LookupVendorResults";
                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {
                string searchCriteria = string.Format(" [ {0} {1} ]", _vendName, _vendTaxID);

                _errorMessage = Commons.GetMessageString("ZeroVendSearchResults") + searchCriteria;

                this.customButtonAddVendor.Visible = true;

            }
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
                string searchCriteria = string.Format(" [ {0} {1} {2} {3} {4} {5} {6} {7} {8} {9} ]", _ssn, _firstName, _lastName, strFilteredDOB, strFilteredPhone, _idTypeCode, _idNumber, _idIssuer, _custNumber, _loanNumber);
                _errorMessage = Commons.GetMessageString("ZeroCustSearchResults") + searchCriteria;
                //SR 5/24/2010 Do not enable the Add button if this is a hold(cust hold, police hold) flow
                var cds = GlobalDataAccessor.Instance.DesktopSession;
                var trigger = cds.HistorySession.Trigger;
                if (!(trigger.Contains("hold")))
                    this.customButtonAddCustomer.Visible = true;
                
            }
        }


        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            retValue = getSearchResultsData();
        }

        void bw_findVendor(object sender, DoWorkEventArgs e)
        {
            retValue = getVendorSearchResultsData();
        }

        private bool getVendorSearchResultsData ()
        {
            bool b = false;
            if (_vendName.Length != 0)
            {
                b = VendorDBProcedures.ExecuteLookupVendor(_vendName, "", strStoreNumber, out vendDatatable, out errorCode, out errorMsg);
            }
            else if (_vendTaxID.Length != 0)
            {
                b = VendorDBProcedures.ExecuteLookupVendor("", _vendTaxID, strStoreNumber, out vendDatatable, out errorCode, out errorMsg);
            }

            return b;
        }

        private bool getSearchResultsData()
        {
            bool b = false;
            var customerDBProcedures = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession);
            if (_lastName.Length != 0 && _firstName.Length != 0 && _dob.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer(_firstName, _lastName, _dob, "", "", "", "", "", "", "", "", "", strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }
            else if (_lastName.Length != 0 && _firstName.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer(_firstName, _lastName, "", "", "", "", "", "", "", "", "", "", strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }

            else if (_lastName.Length != 0 && _dob.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer("", _lastName, _dob, "", "", "", "", "", "", "", "", "", strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }
            else if (_firstName.Length != 0 && _dob.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer(_firstName, "", _dob, "", "", "", "", "", "", "", "", "", strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }
            else if (_phoneNumber.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer("", "", "", "", "", "", "", "", "", _phoneAreaCode, _phoneNumber, "", strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);

            }
            else if (this._idTypeCode.Length != 0 && _idNumber.Length != 0 && _idIssuer.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer("", "", "", "", "", "", _idTypeCode, _idNumber, _idIssuer, "", "", "", strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }
            else if (this._custNumber.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer("", "", "", "", _custNumber, "", "", "", "", "", "", "", strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }
            else if (this._loanNumber.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer("", "", "", "", "", _loanNumber, "", "", "", "", "", strStoreNumber, strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }
            else if (this._ssn.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer("", "", "", _ssn, "", "", "", "", "", "", "", "", strStoreState, out custDatatable, out custIdentDatatable, out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
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
            bool boolValidated = true;
            _lastName = "";
            _firstName = "";
            _dob = "";
            _ssn = "";
            _phoneAreaCode = "";
            _phoneNumber = "";
            _idTypeCode = "";
            _idNumber = "";
            _idIssuer = "";
            _custNumber = "";
            _loanNumber = "";
            _idType = "";
            _idIssuerCode = "";
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
                        _idIssuer = this.lookupCustomerIDIssuer.Text;
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

            //Get Customer loan number search criteria
            else if (LoanNumberRadiobtn.Checked)
            {
                _typeOfSearch = (int)searchCriteria.CUSTLOANNUMBER;
                _loanNumber = this.lookupCustomerLoanNumber.Text;
                if (_loanNumber.Trim().Length == 0)
                {
                    boolValidated = false;
                    this.errorLabel.Text = Commons.GetMessageString("LookupLoanNumberError");
                }
            }
            else if (VendorRadiobtn.Checked)
            {
                if (((RadioButton)this.vendorSearchGroup.Controls["VendorNameRadiobtn"]).Checked )
                {
                    _typeOfSearch = (int)VendorSearchCriteria.VENDORNAME;
                    _vendName = ((TextBox)this.vendorSearchGroup.Controls["lookupVendorName"]).Text;
                    if (_vendName.Trim().Length == 0)
                    {
                        boolValidated = false;
                        this.errorLabel.Text = Commons.GetMessageString("LookupVendorNameError");
                    }

                }
                else
                {
                    _typeOfSearch = (int)VendorSearchCriteria.VENDORTAXID;
                    _vendTaxID = ((TextBox)this.vendorSearchGroup.Controls["lookupVendorTaxID"]).Text;
                    if (_vendTaxID.Trim().Length == 0)
                    {
                        boolValidated = false;
                        this.errorLabel.Text = Commons.GetMessageString("LookupVendorTaxIDError");
                    }
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
        private void LookupVendor_Load(object sender, EventArgs e)
        {
            //Set the owner form
            ownerFrm = this.Owner;
            this.NavControlBox.Owner = this;
            strStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            isFormValid = false;
            getSearchCriteria();
            if ( _errorMessage.Length > 0)
            {  
                this.customButtonAddVendor.Visible = true;
            }

            this.VendorNameRadiobtn.Checked = true;

            if (_typeOfSearch == (int)VendorSearchCriteria.VENDORNAME)
            {
                ((RadioButton)this.vendorSearchGroup.Controls["VendorNameRadiobtn"]).Checked = true;
                ((TextBox)this.vendorSearchGroup.Controls["lookupVendorName"]).Text = _vendName;
            }

            else if (_typeOfSearch == (int)VendorSearchCriteria.VENDORTAXID)
            {
                ((RadioButton)this.vendorSearchGroup.Controls["TaxIDRadiobtn"]).Checked = true;
                ((TextBox)this.vendorSearchGroup.Controls["lookupVendorTaxID"]).Text = _vendTaxID;
            }

 
            //Show error message if any
            if (!string.IsNullOrEmpty(_errorMessage))
            {
                this.errorLabel.Text = _errorMessage;
                _errorMessage = "";
            }
            else
                this.errorLabel.Text = "";
        }

        private void getSearchCriteria()
        {

                _lookupVendSearch =  GlobalDataAccessor.Instance.DesktopSession.LookupCriteria;
                if (_lookupVendSearch != null && _lookupVendSearch.TypeOfSearch > 0)
                {
                    this._vendTaxID = _lookupVendSearch.TaxID;
                    this._vendName = _lookupVendSearch.VendName;
                    this._typeOfSearch = _lookupVendSearch.TypeOfSearch;                    
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
                this.CustomerPhoneNumRadioBtn.Checked = false;
                this.CustNumberRadioBtn.Checked = false;
                this.LoanNumberRadiobtn.Checked = false;
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
                //set the loan number search field disabled
                this.lookupCustomerLoanNumber.Enabled = false;
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
                this.CustomerInfoRadioBtn.Checked = false;
                this.CustNumberRadioBtn.Checked = false;
                this.LoanNumberRadiobtn.Checked = false;
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
                //set the loan number search field disabled
                this.lookupCustomerLoanNumber.Enabled = false;
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
                this.lookupCustomerIDType.SelectedIndex = lookupCustomerIDType.FindString(StateIdTypes.DRIVERLICENSE);
                this.CustomerInfoRadioBtn.Checked = false;
                this.CustNumberRadioBtn.Checked = false;
                this.LoanNumberRadiobtn.Checked = false;
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
                //set the loan number search field disabled
                this.lookupCustomerLoanNumber.Enabled = false;
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
                this.CustomerInfoRadioBtn.Checked = false;
                this.IDSearchRadioBtn.Checked = false;
                this.LoanNumberRadiobtn.Checked = false;
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
                //set the loan number search field disabled
                this.lookupCustomerLoanNumber.Enabled = false;
                //set the SSN field disabled
                this.socialSecurityNumber.Enabled = false;

                this.lookupCustomerIDType.SelectedIndex = -1;
                this.lookupCustomerCustNumber.Focus();
            }
        }

        /// <summary>
        /// Reset the other fields when loan number is selected as the search option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoanNumberRadiobtn_CheckedChanged(object sender, EventArgs e)
        {
            if (LoanNumberRadiobtn.Checked)
            {
                this.CustomerInfoRadioBtn.Checked = false;
                this.IDSearchRadioBtn.Checked = false;
                this.CustNumberRadioBtn.Checked = false;
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
                //set the customer number search field disabled
                this.lookupCustomerCustNumber.Enabled = false;

                //set the ssn field disabled
                this.socialSecurityNumber.Enabled = false;

                //set the loan number search field enabled
                this.lookupCustomerLoanNumber.Enabled = true;

                this.lookupCustomerIDType.SelectedIndex = -1;
                this.lookupCustomerLoanNumber.Focus();
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
            var newVendor = new VendorVO { NewVendor = true };


            if (_typeOfSearch == (int)VendorSearchCriteria.VENDORNAME)
            {
                newVendor.Name = _vendName;
            }
            else if (_typeOfSearch == (int)VendorSearchCriteria.VENDORTAXID) 
            {
                newVendor.TaxID = _vendTaxID;
            }

            GlobalDataAccessor.Instance.DesktopSession.ActiveVendor = newVendor;
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "CreateVendor";
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
                this.findButton_Click(this, e);
            }
            if (keyData == Keys.Home)
            {
                this.SelectNextControl(this.lookupVendorName, false, true, true, true);
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
                this.CustomerInfoRadioBtn.Checked = false;
                this.IDSearchRadioBtn.Checked = false;
                this.CustNumberRadioBtn.Checked = false;
                this.CustomerPhoneNumRadioBtn.Checked = false;
                this.LoanNumberRadiobtn.Checked = false;

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

                //set the loan number search field disabled
                this.lookupCustomerLoanNumber.Enabled = false;

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


        /// <summary>
        /// This method is to load the search criteria object with values entered
        /// in the search criteria fields in the form
        /// </summary>
        private void loadLookupVendorSearchCriteria()
        {
            _lookupVendSearch = new LookupVendorSearchData();
            //If search type was Vendor Name
            if (_typeOfSearch == (int)VendorSearchCriteria.VENDORNAME)
            {
                if (_vendName != null && _vendName.Trim().Length != 0)
                    _lookupVendSearch.VendName = _vendName;
            }
            //search type was Vendor Tax ID
            else 
            {
                if (_vendTaxID != null && _vendTaxID.Trim().Length != 0)
                {
                    _lookupVendSearch.TaxID = _vendTaxID;
                }
            }
            
            _lookupVendSearch.TypeOfSearch =  _typeOfSearch;
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

        private void VendorNameRadiobtn_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked =
                    ((RadioButton)this.vendorSearchGroup.Controls["VendorNameRadiobtn"]).Checked;


            if (isChecked)
            {
                _vendTaxID = "";
                ((TextBox)this.vendorSearchGroup.Controls["lookupVendorTaxID"]).Text = "";
                ((TextBox)this.vendorSearchGroup.Controls["lookupVendorName"]).Focus();
                this.ActiveControl = this.vendorSearchGroup.Controls["lookupVendorName"];
            }
            else
            {
                ((TextBox)this.vendorSearchGroup.Controls["lookupVendorName"]).Text = "";
                ((TextBox)this.vendorSearchGroup.Controls["lookupVendorTaxID"]).Focus();
                this.ActiveControl = this.vendorSearchGroup.Controls["lookupVendorTaxID"];
            }

            ((TextBox)this.vendorSearchGroup.Controls["lookupVendorName"]).Enabled = isChecked;
            ((TextBox)this.vendorSearchGroup.Controls["lookupVendorTaxID"]).Enabled = ! isChecked;
            customButtonFind.Enabled = false;
        }

        private void searchCriteria_lengthCheck(object sender, EventArgs e)
        {
            if (((CustomTextBox)sender).Text.Length >= 3)
                customButtonFind.Enabled = true;
            else
                customButtonFind.Enabled = false;
        }

        private void TaxIDRadiobtn_CheckedChanged(object sender, EventArgs e)
        {
            _vendName = "";
            customButtonFind.Enabled = true;
        }


    }


}
