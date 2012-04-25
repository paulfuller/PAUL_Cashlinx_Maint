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
//using CashlinxDesktop.UserControls;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Shared;
//using Pawn.Forms.UserControls;

//using Pawn.Logic;

namespace Support.Forms.Customer
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
        private string _SSNShort = "";
        private string _productType = "";
        private string _bnkacctnum = "";
        private string _city = "";
        private string _state = "";


        private IDType IdType;
        private State idIssuer1;
        private Date dateOfBirth;

        private ComboBox lookupCustomerIDType = new ComboBox();
        private ComboBox lookupCustomerIDIssuer = new ComboBox();
        
        private bool boolLastNameEntered = false;
        private bool boolFirstNameEntered = false;
        private bool boolDOBEntered = false;
        private bool boolPhoneNumber = false;
        private bool boolSSNShort = false;
        private bool boolCity = false;
        private bool boolState = false;
        private bool boolAccountNumber = false;

        private Country country;
        private Form ownerFrm;
        private bool isFormValid = false;
        private const int MIN_WILDCARD_LETTERSFORSEARCH = 3;
        private const int MIN_LETTERSFORSEARCH = 1;
        private string strStoreState = "";
        private string strStoreNumber = "";
        private string errorCode = "";
        private string errorMsg = "";

        private DataTable custDatatable = new DataTable();
        private DataTable custPhoneDatatable = new DataTable();
        private DataTable custIdentDatatable = new DataTable();
        private DataTable custAddrDatatable = new DataTable();
        private DataTable custEmailDatatable = new DataTable();
        private DataTable custNotesDatatable = new DataTable();
        private DataTable custStoreCreditDatatable = new DataTable();

        private ProcessingMessage procMsg;

        public NavBox NavControlBox; // { get; set; }
        private bool retValue = false;
        private bool vendorSearchSelected;
        // WCM 3/30/12 Moved the _lookupCustSearch to form intializer to alway start with a clean container
        /*__________________________________________________________________________________________*/
        public LookupCustomer()
        {
            InitializeComponent();

            _lookupCustSearch = new LookupCustomerSearchData();
            GlobalDataAccessor.Instance.DesktopSession.ActiveLookupCriteria = _lookupCustSearch;
            country = new Country(); // wcm 3/23/12 Remove
            //Set the Error messages for controls that require it

            this.phoneAreaCodeTextBox.ErrorMessage = Commons.GetMessageString("LookupPhoneNumberError");
            this.phoneNum1TextBox.ErrorMessage = Commons.GetMessageString("LookupPhoneNumberError");
            this.phoneNum2TextBox.ErrorMessage = Commons.GetMessageString("LookupPhoneNumberError");
            this.dateOfBirth.ErrorMessage = Commons.GetMessageString("InvalidDateOfBirth");

            //Set the IDType and ID Issuer controls
            lookupCustomerIDType = (ComboBox)IdType.Controls[0];
            
            //TODO - REMOVE HARD CODING LATER
            // wcm 3/23/12 Remove state identifier for support
            GlobalDataAccessor.Instance.CurrentSiteId.State = "TX";
            //Get store state from desktop session
            //strStoreState = GlobalDataAccessor.Instance.CurrentSiteId.State;

            this.NavControlBox = new NavBox();
            //Madhu Feb 17th
            /*
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.RETAIL))
            {
                if (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.LoanStatus != ProductStatus.LAY)
                {
                    vendorSearchUC.Visible = true;
                    vendorSearchUC.VendorSelectClick += (vendorSearchUC_VendorSelectClick);
                }
            } */
        }
        /// <summary>
        /// If arriving at this form from the search results page by clicking on the back button
        /// the original search criteria information must be shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*__________________________________________________________________________________________*/
        private void LookupCustomer_Load(object sender, EventArgs e)
        {
            //Set the owner form
            ownerFrm = this.Owner;
            this.NavControlBox.Owner = this;


            //strStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber; // wcm 3/23/12 Remove
            isFormValid = false;

            getSearchCriteria();

            if (_errorMessage.Length > 0)
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
                this.socialSecurityNumber.Controls[0].Text = _SSNShort;
                this.phoneAreaCodeTextBox.Text = _phoneAreaCode;
                this.City.Text = _city;
                this.CusInfoState.Text = _state;
                this.BnkAcctNum.Text = _bnkacctnum;

                if (_phoneNumber != null && _phoneNumber.Length == 7)
                {
                    this.phoneNum1TextBox.Text = _phoneNumber.Substring(0, 3);
                    this.phoneNum2TextBox.Text = _phoneNumber.Substring(3, 4);
                }
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
                if (_idTypeCode != null && _idTypeCode.Trim().Length != 0)  // logic for dropdown
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
            //else if (_typeOfSearch == (int)searchCriteria.CUSTPHONENUMBER)
            //{
            //    this.CustomerPhoneNumRadioBtn.Checked = true;
            //    this.phoneAreaCodeTextBox.Text = _phoneAreaCode;
            //    if (_phoneNumber != null && _phoneNumber.Length == 7)
            //    {
            //        this.phoneNum1TextBox.Text = _phoneNumber.Substring(0, 3);
            //        this.phoneNum2TextBox.Text = _phoneNumber.Substring(3, 4);
            //    }
            //}
            else if (_typeOfSearch == (int)searchCriteria.CUSTLOANNUMBER)
            {
                this.LoanTicketRadioBtn.Checked = true;
                this.ProductType.Text = _productType;
                this.ShopNumber.Text = strStoreNumber;
                this.LoanNumber.Text = _loanNumber;
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
            //ClearTextBoxFields();
        }
        //WCM 3/8/12 Obsolete logic
        /*__________________________________________________________________________________________*/
        private void vendorSearchUC_VendorSelectClick()
        {
            //this.CustomerPhoneNumRadioBtn.Checked = false;
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
            //Madhu Feb 17th
            //this.socialSecurityNumber.Enabled = false;

            this.lookupCustomerIDType.SelectedIndex = -1;
            vendorSearchSelected = true;
        }
        /// <summary>
        /// When the clear button is clicked, all the fields are cleared
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*__________________________________________________________________________________________*/
        private void lookupCustomerClearButton_Click(object sender, EventArgs e)
        {
            ClearTextBoxFields();

            //this.lookupCustomerCustNumber.Text = string.Empty;
            //this.dateOfBirth.Controls[0].Text = "mm/dd/yyyy";
            //this.lookupCustomerFirstName.Text = string.Empty;
            //this.lookupCustomerLastName.Text = string.Empty;
            //this.City.Text = string.Empty;
            //this.CusInfoState.Text = string.Empty;
            //this.BnkAcctNum.Text = string.Empty;

            //this.lookupCustomerIDIssuer.SelectedIndex = -1;
            //this.lookupCustomerIDType.SelectedIndex = -1;
            //this.lookupCustomerIDNumber.Text = string.Empty;

            //this.phoneAreaCodeTextBox.Text = string.Empty;
            //this.phoneNum1TextBox.Text = string.Empty;
            //this.phoneNum2TextBox.Text = string.Empty;
            //this.socialSecurityNumber.Controls[0].Text = string.Empty;
            //this.SSNShort.Text = string.Empty;
            //this.LoanNumber.Text = string.Empty;
            //this.ShopNumber.Text = string.Empty;
            //this.ProductType.Text = string.Empty;
            
            ////Madhu Feb 17th
            ////this.CustomerInfoRadioBtn.Checked = !vendorSearchUC.Visible;
            //this.errorLabel.Text = string.Empty;
            //this.customButtonAddCustomer.Visible = false;
            //Madhu Feb 17th
            //if (vendorSearchUC.Visible)
            //vendorSearchUC.VendorSearchClear();
        }
        /// <summary>
        /// When cancel button is clicked, the lookup customer screen should be closed
        /// and the user is shown the cashlinx desktop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*__________________________________________________________________________________________*/
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
        /*__________________________________________________________________________________________*/
        private void lookupCustomerFindButton_Click(object sender, EventArgs e)
        {
            errorLabel.Visible = false;
            errorLabel.Text = string.Empty;

            //Madhu Feb 17th
            // bool vendorSearchCheck = false;
            //Madhu Feb 17th
            /*   if (vendorSearchUC.Visible)
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
               } */
            //Proceed to search results form only if the form is validated
            if (formValidate())
            {
                isFormValid = true;
                procMsg = new ProcessingMessage("Loading Customer Search Results");
                SetButtonState(false);
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted); // WCM 3/8/12 obsolete event
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
                //Madhu Feb 17th
                //if (errorLabel.Text.Length > 0 && !vendorSearchCheck)
                if (errorLabel.Text.Length > 0)
                {
                    MessageBox.Show(Commons.GetMessageString("FormErrorSubmitAgain"));
                    errorLabel.Visible = true;
                }
            }
        }
        /*__________________________________________________________________________________________*/
        private void SetButtonState(bool enable)
        {
            customButtonFind.Enabled = enable;
            customButtonCancel.Enabled = enable;
            customButtonClear.Enabled = enable;
        }

        /*__________________________________________________________________________________________*/
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            procMsg.Close();
            procMsg.Dispose();
            SetButtonState(true);
            loadLookupCustomerSearchCriteria();
            GlobalDataAccessor.Instance.DesktopSession.ActiveLookupCriteria = _lookupCustSearch;
            if (retValue && custDatatable != null)
            {

                isFormValid = true;
                GlobalDataAccessor.Instance.DesktopSession.CustDataTable = null;
                GlobalDataAccessor.Instance.DesktopSession.CustPhoneDataTable = null;
                GlobalDataAccessor.Instance.DesktopSession.CustAddrDataTable = null;
                GlobalDataAccessor.Instance.DesktopSession.CustIdentDataTable = null;
                GlobalDataAccessor.Instance.DesktopSession.CustEmailDataTable = null;
                GlobalDataAccessor.Instance.DesktopSession.CustNotesDataTable = null;
                GlobalDataAccessor.Instance.DesktopSession.CustStoreCreditDataTable = null;
                GlobalDataAccessor.Instance.DesktopSession.CustDataTable = custDatatable;

                if (custAddrDatatable != null)
                    GlobalDataAccessor.Instance.DesktopSession.CustAddrDataTable = custAddrDatatable;
                //Support.Logic.CashlinxPawnSupportSession.Instance.CustAddrDataTable = custAddrDatatable;
                if (custIdentDatatable != null)
                    GlobalDataAccessor.Instance.DesktopSession.CustIdentDataTable = custIdentDatatable;
                //Support.Logic.CashlinxPawnSupportSession.Instance.CustIdentDataTable = custIdentDatatable;
                if (custPhoneDatatable != null)
                    GlobalDataAccessor.Instance.DesktopSession.CustPhoneDataTable = custPhoneDatatable;
                //Support.Logic.CashlinxPawnSupportSession.Instance.CustPhoneDataTable = custPhoneDatatable;
                if (custEmailDatatable != null)
                    GlobalDataAccessor.Instance.DesktopSession.CustEmailDataTable = custEmailDatatable;
                //Support.Logic.CashlinxPawnSupportSession.Instance.CustEmailDataTable = custEmailDatatable;
                if (custNotesDatatable != null)
                    GlobalDataAccessor.Instance.DesktopSession.CustNotesDataTable = custNotesDatatable;
                //Support.Logic.CashlinxPawnSupportSession.Instance.CustNotesDataTable = custNotesDatatable;
                if (custStoreCreditDatatable != null)
                    GlobalDataAccessor.Instance.DesktopSession.CustStoreCreditDataTable = custStoreCreditDatatable;
                //Support.Logic.CashlinxPawnSupportSession.Instance.CustStoreCreditDataTable = custStoreCreditDatatable;

                //Call the lookup results form

                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "LookupCustomerResults";
                this.NavControlBox.Action = NavBox.NavAction.SUBMIT; // BACKANDSUBMIT;

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
                string searchCriteria = string.Format(
                    " [" + " {0} {1} {2} {3} {4} {5} {6} {7} {8} {9} ]", _ssn, _firstName, _lastName, strFilteredDOB, strFilteredPhone, _idTypeCode, _idNumber,
                    _idIssuer, _custNumber, _loanNumber);
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
        /*__________________________________________________________________________________________*/
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            retValue = getSearchResultsData();
        }
        /*__________________________________________________________________________________________*/
        private bool getSearchResultsData()
        {
            bool b = false;
            CustomerDBProcedures customerDBProcedures = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession);

            Support.Controllers.Database.Procedures.CustomerDBProcedures customerDBProceduresForSupport
                = new Support.Controllers.Database.Procedures.CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession);

            //if (_typeOfSearch == (int)searchCriteria.CUSTINFORMATION)
            if (_typeOfSearch == (int)searchCriteria.CUSTLOANNUMBER)
            {
                string loanType = _productType.ToUpperInvariant();
                if (loanType.Equals("PDL/INST"))
                    loanType = "PDL";
                else if (loanType.Equals("LAYAWAY"))
                    loanType = "LAY";
                b = customerDBProceduresForSupport.ExecuteLSupporApptookupCustomer(
                    _firstName, _lastName, _dob, _ssn, "", _loanNumber, "", "", "", _phoneAreaCode, _phoneNumber, strStoreNumber, strStoreNumber, _city, _state, _bnkacctnum, loanType, out custDatatable, out custIdentDatatable, out custPhoneDatatable,
                    out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);

            }
            else //if (_typeOfSearch == (int)searchCriteria.CUSTLOANNUMBER)
            {
                b = customerDBProceduresForSupport.ExecuteLSupporApptookupCustomer(
                    _firstName, _lastName, _dob, _ssn, _custNumber, "", _idTypeCode, _idNumber, _idIssuer, _phoneAreaCode, _phoneNumber, strStoreNumber, _productType, _city, _state, _bnkacctnum, getSearchType(), out custDatatable, out custIdentDatatable, out custPhoneDatatable,
                    out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);

            }
            /*else if (_lastName.Length != 0 && _firstName.Length != 0 && _dob.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer(
                    _firstName, _lastName, _dob, "", "", "", "", "", "", "", "",  strStoreNumber,"", out custDatatable, out custIdentDatatable,
                    out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode,
                    out errorMsg);
            }
            else if (_lastName.Length != 0 && _firstName.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer(
                    _firstName, _lastName, "", "", "", "", "", "", "", "", "",  strStoreNumber,"", out custDatatable, out custIdentDatatable,
                    out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode,
                    out errorMsg);
            }

            else if (_lastName.Length != 0 && _dob.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer(
                    "", _lastName, _dob, "", "", "", "", "", "", "", "",  strStoreNumber,"", out custDatatable, out custIdentDatatable, out custPhoneDatatable,
                    out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }
            else if (_firstName.Length != 0 && _dob.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer(
                    _firstName, "", _dob, "", "", "", "", "", "", "", "", strStoreNumber, "", out custDatatable, out custIdentDatatable, out custPhoneDatatable,
                    out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }
            else if (_phoneNumber.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer(
                    "", "", "", "", "", "", "", "", "", _phoneAreaCode, _phoneNumber,  strStoreNumber,"", out custDatatable, out custIdentDatatable,
                    out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode,
                    out errorMsg);

            }
            else if (this._idTypeCode.Length != 0 && _idNumber.Length != 0 && _idIssuer.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer(
                    "", "", "", "", "", "", _idTypeCode, _idNumber, _idIssuer, "", "",  strStoreNumber,"", out custDatatable, out custIdentDatatable,
                    out custPhoneDatatable, out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode,
                    out errorMsg);
            }
            else if (this._custNumber.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer(
                    "", "", "", "", _custNumber, "", "", "", "", "", "",  strStoreNumber,"", out custDatatable, out custIdentDatatable, out custPhoneDatatable,
                    out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }
            else if (this._loanNumber.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer(
                    "", "", "", "", "", _loanNumber, "", "", "", "", "",  strStoreNumber,_productType , out custDatatable, out custIdentDatatable, out custPhoneDatatable,
                    out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            }
            else if (this._ssn.Length != 0)
            {
                b = customerDBProcedures.ExecuteLookupCustomer(
                    "", "", "", _ssn, "", "", "", "", "", "", "", strStoreNumber,"",  out custDatatable, out custIdentDatatable, out custPhoneDatatable,
                    out custAddrDatatable, out custEmailDatatable, out custNotesDatatable, out custStoreCreditDatatable, out errorCode, out errorMsg);
            } */
            return b;
        }
        /*__________________________________________________________________________________________*/
        private string getSearchType()
        {
            //SSN,CUST_INFO,ID,,LOAN_TICKET_NUM
            if (_typeOfSearch == (int)searchCriteria.CUSTNUMBER)
            {
                return "CUST_NUMBER";
            }
            else if (_typeOfSearch == (int)searchCriteria.CUSTSSN)
            {
                return "SSN";
            }
            else if (_typeOfSearch == (int)searchCriteria.CUSTINFORMATION)
            {
                return "CUST_INFO";
            }
            else if (_typeOfSearch == (int)searchCriteria.CUSTID)
            {
                return "ID";
            }
            else if (_typeOfSearch == (int)searchCriteria.CUSTLOANNUMBER)
            {
                return "LOAN_TICKET_NUM";
            }

            return null;
        }

        private int countSelectedCriteria()
        {
            int count = 0;

            if (boolLastNameEntered)
                count++;
            if (boolFirstNameEntered)
                count++;
            if (boolDOBEntered)
                count++;
            if (boolPhoneNumber)
                count++;
            if (boolSSNShort)
                count++;
            if (boolCity && boolState)
                count++;
            if (boolAccountNumber)
                count++;

            return count;
        }

        private string checkNull(string value)
        {
            if (string.IsNullOrEmpty(value))
                value = string.Empty;

            return value;
        }
        /// <summary>
        /// Function to validate the user input in the lookup form. If there are errors,
        /// the error control of the parent form is set with the message and the function
        /// returns false else returns true
        /// </summary>
        /// <returns></returns>
        /*__________________________________________________________________________________________*/
        private bool formValidate()
        {

            //Flag to specify if the form passed validation
            bool boolValidated = true;
            _lastName = string.Empty;
            _firstName = string.Empty;
            _city = string.Empty;
            _state = string.Empty;
            _bnkacctnum = string.Empty;
            _dob = string.Empty;
            _ssn = string.Empty;
            _phoneAreaCode = string.Empty;
            _phoneNumber = string.Empty;
            _idTypeCode = string.Empty;
            _idNumber = string.Empty;
            _idIssuer = string.Empty;
            _custNumber = string.Empty;
            _loanNumber = string.Empty;
            strStoreNumber = string.Empty;
            _productType = string.Empty;

            _idType = string.Empty;
            _idIssuerCode = string.Empty;
            _phoneNumber = string.Empty;
            _phoneAreaCode = string.Empty;
            _SSNShort = string.Empty;

            //Get Customer Information search criteria
            if (CustomerInfoRadioBtn.Checked)
            {
                _typeOfSearch = (int)searchCriteria.CUSTINFORMATION;
                _lastName = this.lookupCustomerLastName.Text;
                _firstName = this.lookupCustomerFirstName.Text;
                _dob = this.dateOfBirth.Controls[0].Text;
                _phoneNumber = this.phoneNum1TextBox.Text + this.phoneNum2TextBox.Text;
                _phoneAreaCode = this.phoneAreaCodeTextBox.Text;
                _SSNShort = this.SSNShort.Text;
                _city = this.City.Text;
                _state = this.CusInfoState.selectedValue.ToString();
                if (!string.IsNullOrEmpty(_state) && _state.Equals("Select One"))
                    _state = string.Empty;
                _bnkacctnum = this.BnkAcctNum.Text;

                //boolLastNameEntered = false;
                //boolFirstNameEntered = false;
                //boolDOBEntered = false;
                //boolPhoneNumber = false;
                //boolSSNShort = false;

                /*  Conditions
                 *  LastName & First Name
                 *  LastName & DOB
                 *  PhoneNumber
                 *  SSN Short
                 *                  _phoneAreaCode = phoneAreaCodeTextBox.Text;
                _phoneNumber = this.phoneNum1TextBox.Text + this.phoneNum2TextBox.Text;
                if (_phoneAreaCode.Length == 0 || _phoneNumber.Length == 0)            switch (Boolean)
                {
                        case boolPhoneNumber

                        case (boolLastNameEntered & boolFirstNameEntered)
                        
                }
                 */
                //if (_lastName.Trim().Length == 0)
                //{
                //    boolLastNameEntered = false;

                //}
                //if (_firstName.Trim().Length == 0)
                //{
                //    boolFirstNameEntered = false;
                //}
                //if (_dob.Equals("mm/dd/yyyy"))
                //{
                //    boolDOBEntered = false;
                //}
                boolLastNameEntered = !(_lastName.Trim().Length == 0);
                boolFirstNameEntered = !(_firstName.Trim().Length == 0);
                boolDOBEntered = !(_dob.Equals("mm/dd/yyyy"));
                boolPhoneNumber = !(_phoneAreaCode.Length == 0 || _phoneNumber.Length == 0);
                boolSSNShort       =!( _SSNShort.Trim().Length == 0 );
                boolCity = !(_city.Trim().Length == 0);
                boolState = !(_state.Trim().Length == 0);
                boolAccountNumber = !(_bnkacctnum.Trim().Length == 0);

                if(boolSSNShort)
                {
                    string ssnTemp = checkNull(_SSNShort);
                    if(ssnTemp.Length < 4)
                    {
                        boolValidated = false;
                        this.errorLabel.Text = "Please enter last 4 digits of SSN.";
                        return false;
                    }
                }
                var countCriteria = countSelectedCriteria();

                if(countCriteria < 2)
                {
                    boolValidated = false;
                    this.errorLabel.Text = Commons.GetMessageString("LookupFormCriteriaError");
                    return false;
                }


                if (boolSSNShort || boolLastNameEntered || boolFirstNameEntered || boolDOBEntered
                    || boolPhoneNumber || boolCity || boolState || boolAccountNumber)
                {
                    if (boolSSNShort)
                        _ssn = _SSNShort;
                    _typeOfSearch = (int)searchCriteria.CUSTINFORMATION;
                }
                else if (boolPhoneNumber)
                {
                    _typeOfSearch = (int)searchCriteria.CUSTPHONENUMBER;
                }
                    //If the last name is entered but no other information is entered
                else if (boolLastNameEntered)
                {
                    if (!boolFirstNameEntered && ((!boolCity || !boolState)))
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
                                boolValidated = checkDateOfBirth();
                                if (!boolValidated)
                                    this.errorLabel.Text = dateOfBirth.ErrorMessage;
                            }
                        }
                        else
                        {
                            if (!boolCity && !boolState)
                            {
                                if ((_firstName.Trim().Length >= MIN_WILDCARD_LETTERSFORSEARCH && _lastName.Trim().Length >= MIN_LETTERSFORSEARCH) ||
                                    (_lastName.Trim().Length >= MIN_WILDCARD_LETTERSFORSEARCH && _firstName.Trim().Length >= MIN_LETTERSFORSEARCH))
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
                else if(boolCity || boolState)
                {
                    boolValidated = false;
                    this.errorLabel.Text = "At least 1 more criteria other then City and State should be selected for searching..";
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
            //else if (CustomerPhoneNumRadioBtn.Checked)
            //{
            //    _typeOfSearch = (int)searchCriteria.CUSTPHONENUMBER;
            //    _phoneAreaCode = phoneAreaCodeTextBox.Text;
            //    _phoneNumber = this.phoneNum1TextBox.Text + this.phoneNum2TextBox.Text;
            //    if (_phoneAreaCode.Length == 0 || _phoneNumber.Length == 0)
            //    {
            //        boolValidated = false;
            //        this.errorLabel.Text = Commons.GetMessageString("LookupPhoneNumberError");

            //    }
            //}
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
                //Get Loan Number Search Criteria
            else if (LoanTicketRadioBtn.Checked)
            {
                _typeOfSearch = (int)searchCriteria.CUSTLOANNUMBER;
                _loanNumber = this.LoanNumber.Text;
                strStoreNumber = this.ShopNumber.Text;
                _productType = this.ProductType.Text;

                if (_loanNumber.Trim().Length == 0)
                {
                    boolValidated = false;
                    //this.errorLable.Text = Commons.GetMessageString("LookupCustNumberError"); //LookupCustLoanError");
                }
            }
            else
            {
                boolValidated = false;
                this.errorLabel.Text = Commons.GetMessageString("LookupSearchCriteriaError");

            }
            return boolValidated;

        }
        // WCM 3/23/12 Note: To add productType input attribute requires change the constants in the 
        //Common.Libraries.Utility.Shared.LookupCustomerSearchData 
        /*__________________________________________________________________________________________*/
        private void getSearchCriteria()
        {

            _lookupCustSearch = GlobalDataAccessor.Instance.DesktopSession.ActiveLookupCriteria;
            if (_lookupCustSearch != null)
            {

                if (_lookupCustSearch.TypeOfSearch > 0)
                {
                    _lastName = _lookupCustSearch.LastName;
                    _loanNumber = _lookupCustSearch.LoanNumber;   //.ToString();
                    _phoneAreaCode = _lookupCustSearch.PhoneAreaCode;
                    _phoneNumber = _lookupCustSearch.PhoneNumber;
                    _firstName = _lookupCustSearch.FirstName;
                    _custNumber = _lookupCustSearch.CustNumber;
                    _dob = _lookupCustSearch.DOB;
                    _idTypeCode = _lookupCustSearch.IdTypeCode;
                    _idNumber = _lookupCustSearch.IDNumber;
                    _idIssuer = _lookupCustSearch.IDIssuer;
                    _ssn = _lookupCustSearch.SSN;
                    _SSNShort = _lookupCustSearch.SSN;
                   
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
        /*__________________________________________________________________________________________*/
        private void CustomerInfoRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (CustomerInfoRadioBtn.Checked)
            {
                //Madhu Feb 17th
                /*if (vendorSearchUC.Visible)
                {
                    vendorSearchSelected = false;
                    vendorSearchUC.VendorSearchClear();
                }*/
                
                this.ClearTextBoxFields();

                //this.CustomerPhoneNumRadioBtn.Checked = false;
                this.CustNumberRadioBtn.Checked = false;

                this.IDSearchRadioBtn.Checked = false;
                this.radioButtonSSN.Checked = false;
                //enable the name fields
                this.lookupCustomerLastName.Enabled = true;
                this.lookupCustomerFirstName.Enabled = true;
                this.dateOfBirth.Enabled = true;
                //Set the other search fields disabled
                //Set the Phone number search criteria fields disabled
                this.phoneAreaCodeTextBox.Enabled = true;
                this.phoneNum1TextBox.Enabled = true;
                this.phoneNum2TextBox.Enabled = true;
                this.SSNShort.Enabled = true;
                this.City.Enabled = true;
                this.CusInfoState.Enabled = true;
                this.BnkAcctNum.Enabled = true;
                //Set the ID search fields disabled
                this.IdType.Enabled = false;
                this.idIssuer1.Enabled = false;
                this.country.Enabled = false;
                this.lookupCustomerIDNumber.Enabled = false;
                //set the customer number search field disabled
                this.lookupCustomerCustNumber.Enabled = false;
                //set the ssn field disabled
                this.socialSecurityNumber.Enabled = false;
                //set the loannumber field 
                this.LoanTicketRadioBtn.Checked = false;
                this.ShopNumber.Enabled = false;
                this.ProductType.Enabled = false;
                this.LoanNumber.Enabled = false;

                this.lookupCustomerIDType.SelectedIndex = -1;
                this.lookupCustomerLastName.Focus();
            }
        }

        /// <summary>
        /// Reset the other fields when Phone number is selected as the search option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // WCM 3/8/12 Obsolete logic
        /*__________________________________________________________________________________________*/
        //private void CustomerPhoneNumRadioBtn_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (CustomerPhoneNumRadioBtn.Checked)
        //    {
        //        //Madhu Feb 17th
        //        /*if (vendorSearchUC.Visible)
        //        {
        //            vendorSearchSelected = false;
        //            vendorSearchUC.VendorSearchClear();
        //        }*/

        //        this.CustomerInfoRadioBtn.Checked = false;
        //        this.CustNumberRadioBtn.Checked = false;

        //        this.IDSearchRadioBtn.Checked = false;
        //        this.radioButtonSSN.Checked = false;

        //        //set the Phone number fields enabled
        //        this.phoneAreaCodeTextBox.Enabled = true;
        //        this.phoneNum1TextBox.Enabled = true;
        //        this.phoneNum2TextBox.Enabled = true;

        //        //Set the name and dob fields disabled
        //        this.lookupCustomerLastName.Enabled = false;
        //        this.lookupCustomerFirstName.Enabled = false;
        //        this.dateOfBirth.Enabled = false;
        //        //Set the ID search fields disabled
        //        this.IdType.Enabled = false;
        //        this.idIssuer1.Enabled = false;
        //        this.country.Enabled = false;
        //        this.lookupCustomerIDNumber.Enabled = false;
        //        //set the customer number search field disabled
        //        this.lookupCustomerCustNumber.Enabled = false;
        //        //set the SSN field disabled
        //        this.socialSecurityNumber.Enabled = false;
        //        //set the loannumber field 
        //        this.LoanTicketRadioBtn.Checked = false;
        //        this.LoanNumber.Enabled = false;

        //        this.lookupCustomerIDType.SelectedIndex = -1;
        //        this.phoneAreaCodeTextBox.Focus();
        //    }
        //}

        /// <summary>
        /// Reset the other fields in the form when ID is selected as the search option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*__________________________________________________________________________________________*/
        private void IDSearchRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (IDSearchRadioBtn.Checked)
            {
                //Madhu Feb 17th
                /*if (vendorSearchUC.Visible)
                {
                    vendorSearchSelected = false;
                    vendorSearchUC.VendorSearchClear();
                }*/
                this.ClearTextBoxFields();

                this.lookupCustomerIDType.SelectedIndex = lookupCustomerIDType.FindString(StateIdTypes.DRIVERLICENSE);
                this.CustomerInfoRadioBtn.Checked = false;
                this.CustNumberRadioBtn.Checked = false;
                //this.CustomerPhoneNumRadioBtn.Checked = false;
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

                this.SSNShort.Enabled = false;
                //Set the name and dob fields disabled
                this.lookupCustomerLastName.Enabled = false;
                this.lookupCustomerFirstName.Enabled = false;
                this.dateOfBirth.Enabled = false;
                this.City.Enabled = false;
                this.CusInfoState.Enabled = false;
                this.BnkAcctNum.Enabled = false;
                //set the customer number search field disabled
                this.lookupCustomerCustNumber.Enabled = false;
                //set the ssn field disabled
                this.socialSecurityNumber.Enabled = false;
                //set the loannumber field 
                this.LoanTicketRadioBtn.Checked = false;
                this.LoanNumber.Enabled = false;
                this.ProductType.Enabled = false;
                this.ShopNumber.Enabled = false;
                this.LoanNumber.Enabled = false;


            }
        }

        /// <summary>
        /// Reset the other fields in the form when customer number is selected as the search option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*__________________________________________________________________________________________*/
        private void CustNumberRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (CustNumberRadioBtn.Checked)
            {
                //Madhu Feb 17th
                /*
                if (vendorSearchUC.Visible)
                {
                    vendorSearchSelected = false;
                    vendorSearchUC.VendorSearchClear();
                }*/

                this.ClearTextBoxFields();

                this.CustomerInfoRadioBtn.Checked = false;
                this.IDSearchRadioBtn.Checked = false;
                //this.CustomerPhoneNumRadioBtn.Checked = false;
                this.radioButtonSSN.Checked = false;
                this.LoanTicketRadioBtn.Checked = false;

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
                this.City.Enabled = false;
                this.CusInfoState.Enabled = false;
                this.BnkAcctNum.Enabled = false;

                //set the customer number search field enabled
                this.lookupCustomerCustNumber.Enabled = true;
                //set the SSN field disabled
                this.SSNShort.Enabled = false;
                this.socialSecurityNumber.Enabled = false;
                //set the loannumber field 
                this.LoanNumber.Enabled = false;
                this.ProductType.Enabled = false;
                this.ShopNumber.Enabled = false;
                this.LoanNumber.Enabled = false;

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
        //  WCM 3/8/12 Obsolete Logic
        /*__________________________________________________________________________________________*/
        private void addButton_Click(object sender, EventArgs e)
        {
            isFormValid = true;
            //Create a new customer object and set the fields entered for search
            CustomerVO newCustomer = new CustomerVO
                                     {
                                         NewCustomer = true
                                     };

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
        /*__________________________________________________________________________________________*/ 
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
        /*__________________________________________________________________________________________*/ 
        private void IdType_Leave(object sender, EventArgs e)
        {
            populateIssuerData();
            this.lookupCustomerIDNumber.Focus();


        }
        /*__________________________________________________________________________________________*/
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
        /*__________________________________________________________________________________________*/
        private void radioButtonSSN_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonSSN.Checked)
            {
                //Madhu Feb 17th
                /*if (vendorSearchUC.Visible)
                {
                    vendorSearchSelected = false;
                    vendorSearchUC.VendorSearchClear();
                }*/
                
                this.ClearTextBoxFields();

                this.CustomerInfoRadioBtn.Checked = false;
                this.IDSearchRadioBtn.Checked = false;
                this.CustNumberRadioBtn.Checked = false;
                //this.CustomerPhoneNumRadioBtn.Checked = false;
                this.LoanTicketRadioBtn.Checked = false;

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
                this.City.Enabled = false;
                this.CusInfoState.Enabled = false;
                this.BnkAcctNum.Enabled = false;


                //set the customer number search field disabled
                this.lookupCustomerCustNumber.Enabled = false;


                //set the social security number field enabled
                this.socialSecurityNumber.Enabled = true;
                this.SSNShort.Enabled = false;
                //set the loannumber field 
                this.LoanNumber.Enabled = false;
                this.ProductType.Enabled = false;
                this.ShopNumber.Enabled = false;

                this.lookupCustomerIDType.SelectedIndex = -1;
                this.socialSecurityNumber.Focus();
            }

        }
        /*__________________________________________________________________________________________*/
        private void phoneAreaCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (phoneAreaCodeTextBox.Text.Length == phoneAreaCodeTextBox.MaxLength)
            {
                this.SelectNextControl(phoneAreaCodeTextBox, true, true, true, true);

            }

        }
        /*__________________________________________________________________________________________*/
        private void phoneNum1TextBox_TextChanged(object sender, EventArgs e)
        {
            if (phoneNum1TextBox.Text.Length == phoneNum1TextBox.MaxLength)
            {
                this.SelectNextControl(phoneNum1TextBox, true, true, true, true);

            }

        }
        /*__________________________________________________________________________________________*/
        private void LookupCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !isFormValid;
        }

        /// <summary>
        /// This method is to load the search criteria object with values entered
        /// in the search criteria fields in the form
        /// </summary>
        /// <param name="lookupCustForm"></param>
        // WCM 3/30/12 Moved the _lookupCustSearch to form intializer to alway start with a clean container 
        /*__________________________________________________________________________________________*/
        private void loadLookupCustomerSearchCriteria()
        {
            //_lookupCustSearch = new LookupCustomerSearchData();
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
                _lookupCustSearch.LoanNumber = _loanNumber.ToString();  //WCM 3/23/12 loannumber in Loan is datatype number

                // TODO add producttype and storenumber to contant _lookupCustSearch
                //strStoreNumber = 
            }
            else
                //search was by SSN
                _lookupCustSearch.SSN = _ssn;

            _lookupCustSearch.TypeOfSearch = _typeOfSearch;
        }
        /*__________________________________________________________________________________________*/
        private void dateOfBirth_Leave(object sender, EventArgs e)
        {
            bool retVal = checkDateOfBirth();
        }
        /*__________________________________________________________________________________________*/
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
        /*__________________________________________________________________________________________*/
        private void LoanTicketRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (LoanTicketRadioBtn.Checked)
            {
                this.ClearTextBoxFields();
                this.CustomerInfoRadioBtn.Checked = false;
                this.IDSearchRadioBtn.Checked = false;
                //this.CustomerPhoneNumRadioBtn.Checked = false;
                this.radioButtonSSN.Checked = false;
                this.CustNumberRadioBtn.Checked = false;

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
                this.City.Enabled = false;
                this.CusInfoState.Enabled = false;
                this.BnkAcctNum.Enabled = false;

                //set the customer number search field enabled
                this.lookupCustomerCustNumber.Enabled = false;
                //set the SSN field disabled
                this.socialSecurityNumber.Enabled = false;
                this.SSNShort.Enabled = false;
                //set the loannumber field 
                this.ProductType.Enabled = true;
                this.ShopNumber.Enabled = true;
                this.LoanNumber.Enabled = true;

                this.lookupCustomerIDType.SelectedIndex = -1;
                this.LoanNumber.Focus();

            }
        }
        /*__________________________________________________________________________________________*/
        private void ClearTextBoxFields()
        {
            // SSN 
            this.socialSecurityNumber.Controls[0].Text = string.Empty;
            // Customer Info
            this.SSNShort.Text = string.Empty;
            this.lookupCustomerFirstName.Text = string.Empty;
            this.lookupCustomerLastName.Text = string.Empty;
            this.phoneAreaCodeTextBox.Text = string.Empty;
            this.phoneNum1TextBox.Text = string.Empty;
            this.phoneNum2TextBox.Text = string.Empty;
            this.dateOfBirth.Controls[0].Text = "mm/dd/yyyy";
            this.City.Text = string.Empty;
            this.CusInfoState.Controls[0].Text = string.Empty;
            this.BnkAcctNum.Text = string.Empty;
            // ID Search
            this.lookupCustomerIDType.SelectedIndex = -1;
            this.lookupCustomerIDNumber.Text = string.Empty;
            this.lookupCustomerIDIssuer.SelectedIndex = -1;
            //this.idIssuer1.Controls[0].Text = string.Empty;
            // Customer Number
            this.lookupCustomerCustNumber.Text = string.Empty;
            // Loan Ticket
            this.ProductType.Text = string.Empty;
            this.ShopNumber.Text = string.Empty;
            this.LoanNumber.Text = string.Empty;

            this.errorLabel.Text = string.Empty;
        }

        private void ShopNumber_Leave(object sender, EventArgs e)
        {
            string str;
            str = this.ShopNumber.Text;
            if (str == string.Empty)
            {
                this.LoanNumber.Focus();
                return;
            }
            int TextLength = str.Length;
            if (TextLength < 5)
            {
                str = str.PadLeft(5, '0');
                this.ShopNumber.Text  = str;
            }
            this.LoanNumber.Select();
        }
    }
}
