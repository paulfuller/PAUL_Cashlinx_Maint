using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Customer; 
using Common.Libraries.Utility.Shared;
using Support.Forms.Pawn.Customer;
using Support.Libraries.Objects.Customer;

//using Pawn.Logic;

namespace Support.Forms.Customer
{

    /*__________________________________________________________________________________________*/
    public partial class UpdateCustomerDetails : Form
    {

        private string _title;
        private string _titleSuffix;
        private string _firstName;
        private string _lastName;
        private string _middleInitial;
        private string _language;
        private DateTime _dob;
        private string _ssn;
        private bool _isFormValid;
        private string _partyId = "";
        private CustomerVOForSupportApp _custToEdit;
        private bool _nameChanged;
        private bool _dobChanged;
        private bool _ssnChanged;

        string _strFirstName;
        string _strLastName;
        string _strMiddleName;
        string _strDob;
        string _strTitle;
        string _strTitleSuffix;
        string _strLanguage;
        string _errorCode = "";
        string _errorMsg = "";
        string _strUserId;        
        DateTime _dateOfBirth;
        DateTime _lastVerDate;
        DateTime _nextVerDate;
        string _strSsn;
        bool _dobNotEntered;
        int _age;

        string _marital_status = "";
        string _spouse_first_name = "";
        string _spouse_last_name = "";
        string _spouse_ssn = "";
        string _cust_sequence_number = "";
        string _privacy_notification_date = "";
        string _opt_out_flag = "";
        string _status = "";
        string _reason_code = "";
        string _last_verification_date = "";
        string _next_verification_date = "";
        string _cooling_off_date_pdl = "";
        string _customer_since_pdl = "";
        string _spanish_form = "";
        string _prbc = "";
        string _planbankruptcy_protection = "";
        string _years = "";
        string _months = "";
        string _own_home = "";
        string _monthly_rent = "";
        string _military_stationed_local = "";


        private string ThisFormName = "UpdateCustomerDetails";
        private Form ownerfrm;
        public NavBox NavControlBox;
        #region FormStartup
        /*__________________________________________________________________________________________*/
        public UpdateCustomerDetails()
        {
            InitializeComponent();

            this.NavControlBox = new NavBox();
            this.NavControlBox.CustomDetail = ThisFormName; // "UpdateCustomerDetails";
            

            this.custDateOfBirth.ErrorMessage = Commons.GetMessageString("InvalidDateOfBirth");

            ArrayList LanguageTypes = new ArrayList();
            LanguageTypes.Add(new ComboBoxData("", "Select"));
            LanguageTypes.Add(new ComboBoxData("en", "English"));
            LanguageTypes.Add(new ComboBoxData("es", "Spanish"));

            this.comboBoxLanguage.DataSource = LanguageTypes;
            this.comboBoxLanguage.DisplayMember = "Description";
            this.comboBoxLanguage.ValueMember = "Code";
        }
        /*__________________________________________________________________________________________*/
        private void UpdateCustomerDetails_Load(object sender, EventArgs e)
        {
            

            ownerfrm = this.Owner;
            this.NavControlBox.Owner = this;

            LoadDataInForm();

            //SR 2/16/2010 Roles and resources check added
            //check the privileges of the logged in user to determine
            //if the user can edit any information
            if (!(SecurityProfileProcedures.CanUserModifyResource("UPDATESSN", GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile, GlobalDataAccessor.Instance.DesktopSession)))
            {
                this.labelSSN.Visible=false;
                this.custSSN.Visible=false;
            }

            this.custDateOfBirth.ErrorMessage = Commons.GetMessageString("InvalidDateOfBirth");

        }
    #endregion  
        #region GetData
        /*__________________________________________________________________________________________*/
        public CustomerVOForSupportApp CustToEdit
        {
            set
            {
                _custToEdit = value;
                _title = _custToEdit.CustTitle;
                _titleSuffix = _custToEdit.CustTitleSuffix;
                _firstName = _custToEdit.FirstName;
                _lastName = _custToEdit.LastName;
                _middleInitial = _custToEdit.MiddleInitial;
                _language = _custToEdit.NegotiationLanguage;
                _dob = _custToEdit.DateOfBirth;
                _ssn = _custToEdit.SocialSecurityNumber;
                _partyId = _custToEdit.PartyId;
            }
        }

        private void mapSupportAppFields()
        {
            if (_custToEdit.MaritalStatus != null && 
                _custToEdit.MaritalStatus.Equals(string.Empty))
                this.txtBoxMarStatus.Text = null;
            else
                this.txtBoxMarStatus.Text = _custToEdit.MaritalStatus;

            this.txtBoxSpouseFName.Text = _custToEdit.SpouseFirstName;
            this.txtBoxSpouseLName.Text = _custToEdit.SpouseLastName;
            this.txtBoxSpouseSSN.Text = _custToEdit.SpouseSsn;
            this.txtBoxHLAAYears.Text = _custToEdit.Years.ToString();
            this.txtBoxHLAAMonths.Text = _custToEdit.Months.ToString();

            if (!string.IsNullOrEmpty(_custToEdit.MilitaryStationedLocal) 
                && _custToEdit.MilitaryStationedLocal.Equals("Y"))
                this.MilitaryStationedLocalcheckBox.Checked = true;
            else
                this.MilitaryStationedLocalcheckBox.Checked = false;

            this.txtBoxOwnHome.Text = _custToEdit.OwnHome;
            this.txtBoxCustSeqNumber.Text = _custToEdit.CustSequenceNumber ;

            if (!_custToEdit.PrivacyNotificationDate.Equals(DateTime.MinValue) &&
                !_custToEdit.PrivacyNotificationDate.Equals(DateTime.MaxValue))
                this.txtBoxPrivacyNotifDate.Text = _custToEdit.PrivacyNotificationDate.FormatDate();
            else
            {
                this.txtBoxPrivacyNotifDate.Text = string.Empty;
            }
            
            if (!string.IsNullOrEmpty(_custToEdit.OptOutFlag) && _custToEdit.OptOutFlag.Equals("Y"))
                this.OptOutFlagcheckBox.Checked = true;
            else
            {
                this.OptOutFlagcheckBox.Checked = false;
            }
            this.txtBoxCustStatus.Text = _custToEdit.Status;
            this.txtBoxReasonCode.Text = _custToEdit.ReasonCode;

            if (!_custToEdit.LastVerificationDate.Equals(DateTime.MinValue) &&
                !_custToEdit.LastVerificationDate.Equals(DateTime.MaxValue))
                this.txtBoxLastVerDate.Controls[0].Text = _custToEdit.LastVerificationDate.FormatDate();
            else
                this.txtBoxLastVerDate.Controls[0].Text = "mm/dd/yyyy";//string.Empty;


            if (!_custToEdit.NextVerificationDate.Equals(DateTime.MinValue) &&
                !_custToEdit.NextVerificationDate.Equals(DateTime.MaxValue))
                this.txtBoxNextVerDate.Controls[0].Text = _custToEdit.NextVerificationDate.FormatDate();
            else
                this.txtBoxNextVerDate.Controls[0].Text = "mm/dd/yyyy";//string.Empty;


            if (!_custToEdit.CoolingOffDatePDL.Equals(DateTime.MinValue) &&
                !_custToEdit.CoolingOffDatePDL.Equals(DateTime.MaxValue))
                this.txtBoxPDLCoolingOffDate.Text = _custToEdit.CoolingOffDatePDL.FormatDate();
            else
                this.txtBoxPDLCoolingOffDate.Text = string.Empty;


            if (!_custToEdit.CustomerSincePDL.Equals(DateTime.MinValue) &&
                !_custToEdit.CustomerSincePDL.Equals(DateTime.MaxValue))
                this.txtBoxPDLCustSince.Text = _custToEdit.CustomerSincePDL.FormatDate();
            else
                this.txtBoxPDLCustSince.Text = string.Empty;

            
            if (!string.IsNullOrEmpty(_custToEdit.SpanishForm) && _custToEdit.SpanishForm.Equals("Y"))
                this.SpanishFormcheckBox1.Checked = true;

            if (!string.IsNullOrEmpty(_custToEdit.PRBC) && _custToEdit.PRBC.Equals("Y"))
                this.PRBCcheckBox.Checked = true;
            this.txtBoxMonthlyRent.Text = _custToEdit.MonthlyRent.ToString();

            if (!string.IsNullOrEmpty(_custToEdit.PlanBankruptcyProtection) && _custToEdit.PlanBankruptcyProtection.Equals("Y"))
                this.BankruptcyProtectionradioButton1.Checked = true;
            else
                this.BankruptcyProtectionradioButton2.Checked = true;
        }

        /*__________________________________________________________________________________________*/
        private void LoadDataInForm()
        {
            //_custToEdit = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
            _custToEdit = Support.Logic.CashlinxPawnSupportSession.Instance.ActiveCustomer;

            CustToEdit = Support.Logic.CashlinxPawnSupportSession.Instance.ActiveCustomer;
            //this.custFirstName.Text = _firstName;
            //this.custLastName.Text = _lastName;
            //this.custMiddleInitial.Text = _middleInitial;
            //this.custSSN.Controls[0].Text = _ssn;
            ////this.custDateOfBirth.Controls[0].Text = _dob.FormatDate();
            
            mapSupportAppFields();

            this.custFirstName.Text = _custToEdit.FirstName;
            this.custLastName.Text = _custToEdit.LastName;
            this.custMiddleInitial.Text = _custToEdit.MiddleInitial;
            this.custSSN.Controls[0].Text = _custToEdit.SocialSecurityNumber;
            this.custDateOfBirth.Controls[0].Text = _custToEdit.DateOfBirth.FormatDate();

            ComboBox custTitle = (ComboBox)this.title1.Controls[0];

            Console.WriteLine("_title-->" + _title);
            if (_title.Length != 0)
            {
                Console.WriteLine("after title..");
                foreach (ComboBoxData currTitle in custTitle.Items)
                {
                    Console.WriteLine("after title..for -->" + currTitle.Code);
                    if (currTitle.Code == _title)
                    {
                        custTitle.SelectedIndex = custTitle.Items.IndexOf(currTitle);
                        break;
                    }
                }
            }
            else
                custTitle.SelectedIndex = 0;

            custTitle = null;
            ComboBox custTitSuffix = (ComboBox)this.titleSuffix1.Controls[0];
            if (_titleSuffix.Length != 0)
            {
                foreach (ComboBoxData currTitleSuffix in custTitSuffix.Items)
                    if (currTitleSuffix.Code == _titleSuffix)
                    {
                        custTitSuffix.SelectedIndex = custTitSuffix.Items.IndexOf(currTitleSuffix);
                        break;
                    }
            }
            else
                custTitSuffix.SelectedIndex = 0;
            custTitSuffix = null;


            if (_language.Length != 0)
            {
                foreach (ComboBoxData currData in comboBoxLanguage.Items)
                    if (currData.Code == _language)
                    {
                        comboBoxLanguage.SelectedIndex = comboBoxLanguage.Items.IndexOf(currData);
                        break;
                    }
            }
        }
        /*__________________________________________________________________________________________*/
        private void getFormData()
        {
            ComboBox title = (ComboBox)this.title1.Controls[0];
            ComboBox titsuffix = (ComboBox)this.titleSuffix1.Controls[0];


            _strFirstName = this.custFirstName.Text;
            _strLastName = this.custLastName.Text;
            _strDob = this.custDateOfBirth.Controls[0].Text.ToString();

            try
            {
                _dateOfBirth = DateTime.Parse(_strDob);
            }
            catch (Exception)
            {
                _dateOfBirth = DateTime.MaxValue;
            }
            _strMiddleName = this.custMiddleInitial.Text;

            _strUserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
            _strTitle = title.SelectedIndex > 0 ? title.SelectedValue.ToString() : "";
            _strTitleSuffix = titsuffix.SelectedIndex > 0 ? titsuffix.SelectedValue.ToString() : "";
            _strLanguage = this.comboBoxLanguage.SelectedIndex > 0 ? comboBoxLanguage.SelectedValue.ToString() : "";
            _strSsn = this.custSSN.Controls[0].Text;

        }
        /*__________________________________________________________________________________________*/
        private void checkReqFields()
        {
            if (this.custFirstName.isValid && this.custLastName.isValid &&
                this.comboBoxLanguage.SelectedIndex > 0 && !_dobNotEntered)
                _isFormValid = true;
            else
                _isFormValid = false;
        }
        /*__________________________________________________________________________________________*/
        private void custDateOfBirth_Leave(object sender, EventArgs e)
        {
            checkDateOfBirth();

        }
        private void txtBoxMonthlyRent_Leave(object sender, EventArgs e)
        {
            
            string monthlyRent = checkNull(this.txtBoxMonthlyRent.Text);
            if (!monthlyRent.Equals(string.Empty))
            {
                int Num;
                bool isNum = int.TryParse(monthlyRent, out Num);
                if (!isNum)
                {
                    MessageBox.Show("Invalid Month Rent.");
                    this.txtBoxMonthlyRent.Text = string.Empty;
                    this.txtBoxMonthlyRent.Focus();
                }
            }
        }
        
        private void custHLAMonths_Leave(object sender, EventArgs e)
        {
            string months = checkNull(this.txtBoxHLAAMonths.Text);

            if (!months.Equals(string.Empty))
            {
                try
                {
                    int monthCnt = Convert.ToInt32(months);
                    if (monthCnt > 12)
                    {
                        MessageBox.Show("Invalid Month");
                        this.txtBoxHLAAMonths.Focus();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Invalid Month");
                    this.txtBoxHLAAMonths.Focus();

                }
            }
        }

        private void BankruptcyProtectionradioButton1_Click(object sender, EventArgs e)
        {
            this.BankruptcyProtectionradioButton2.Checked = false;
        }

        private void BankruptcyProtectionradioButton2_Click(object sender, EventArgs e)
        {
            this.BankruptcyProtectionradioButton1.Checked = false;
        }

        private void custHLAMYears_Leave(object sender, EventArgs e)
        {
            try
            {
                string years = checkNull(this.txtBoxHLAAYears.Text);
                if (!years.Equals(string.Empty))
                {
                    Convert.ToInt32(years);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Year");
                this.txtBoxHLAAYears.Focus();

            }
        }

        private void LastVerDate_Leave(object sender, EventArgs e)
        {
            //string date = this.txtBoxLastVerDate.Controls[0].Text.Trim().ToString();
            string date = checkNull(this.txtBoxLastVerDate.Text);
            if (!date.Equals(string.Empty) && !this.txtBoxLastVerDate.isValid)
            {
                MessageBox.Show(Commons.GetMessageString("InvalidDate"));
                this.txtBoxLastVerDate.Focus();
            }
        }

        private void NextVerDate_Leave(object sender, EventArgs e)
        {
            string date = checkNull(this.txtBoxNextVerDate.Text);
            if (!date.Equals(string.Empty) && !this.txtBoxNextVerDate.isValid)
            {
                MessageBox.Show(Commons.GetMessageString("InvalidDate"));
                this.txtBoxNextVerDate.Focus();
            }
        }


        // WCM 3/28/12 Not sure of the purpose business rule of DOB Check 
        /*__________________________________________________________________________________________*/
        private void checkDateOfBirth()
        {
            string birthdate = this.custDateOfBirth.Controls[0].Text.Trim().ToString();
            custDateOfBirth.isValid = true;

            _dobNotEntered = birthdate.Equals("mm/dd/yyyy");
            
            if (!_dobNotEntered && birthdate.Length > 0)
            {
                try
                {
                    DateTime userDate = Convert.ToDateTime(birthdate);
                    DateTime currentDate = DateTime.Now;

                    if(userDate > currentDate)
                    {
                        MessageBox.Show(Commons.GetMessageString("InvalidDateOfBirth"));
                        this.labelDOB.ForeColor = Color.Red;
                        custDateOfBirth.isValid = false;
                    }
                    else
                    {
                        this.labelDOB.ForeColor = Color.Black;
                    }

                    //_age = Commons.getAge(birthdate, ShopDateTime.Instance.ShopDate);
                    //int validAge = CustomerProcedures.getPawnBRCustomerLegalAge(Support.Logic.CashlinxPawnSupportSession.Instance);
                    //if we are here and do not have a value for validAge, the call to get it
                    //from business rule engine failed. Hence assign the default value.
                    //if (validAge == 0)

                    /*int validAge = CustomerValidAge.PAWNCUSTLEGALAGE;
                    if (_age < validAge)
                    {
                        if (_age <= 0)
                        {
                            MessageBox.Show(Commons.GetMessageString("InvalidDateOfBirth"));
                        }
                        else
                        {
                            MessageBox.Show(Commons.GetMessageString("AgeError"));
                        }
                        this.labelDOB.ForeColor = Color.Red;
                        custDateOfBirth.isValid = false;
                    }
                    else
                    {
                        this.labelDOB.ForeColor = Color.Black;


                    }*/


                }
                catch (Exception)
                {
                    MessageBox.Show(Commons.GetMessageString("InvalidDateOfBirth"));
                    custDateOfBirth.Focus();
                }
            }

        }
        /*__________________________________________________________________________________________*/
        private bool doNameCheck()
        {
            DataTable custDatatable = new DataTable();
            bool retValue = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).CheckDuplicateNameDOB(_strFirstName, _strLastName, _strDob, out custDatatable, out _errorCode, out _errorMsg);
            if (retValue)
            {
                return custDatatable != null && custDatatable.Rows.Count > 0;
            }
            else
            {
                throw new ApplicationException("Call to check duplicate name and dob failed");

            }
        }

        /*__________________________________________________________________________________________*/
        private bool custInfoChanged()
        {
            bool otherCustDataChanged = false;
            _nameChanged = false;
            _dobChanged = false;
            _ssnChanged = false;
            if (_firstName != _strFirstName || _lastName != _strLastName ||
                _middleInitial != _strMiddleName)
                _nameChanged = true;
            if (_dob.FormatDate() != _strDob)
                _dobChanged = true;
            if (_ssn != _strSsn)
                _ssnChanged = true;
            if (_title != _strTitle || _titleSuffix != _strTitleSuffix || _language != _strLanguage)
                otherCustDataChanged = true;

            var customerCurrentValues = Support.Logic.CashlinxPawnSupportSession.Instance.ActiveCustomer;
            var oldValue = checkNull(customerCurrentValues.MaritalStatus);
            if (oldValue != _marital_status)
                otherCustDataChanged = true;

            oldValue = checkNull(customerCurrentValues.CustSequenceNumber);
            if (oldValue != _cust_sequence_number)
                otherCustDataChanged = true;

            oldValue = checkNull(customerCurrentValues.OptOutFlag);
            if (oldValue != _opt_out_flag)
                otherCustDataChanged = true;

            //oldValue = customerCurrentValues.LastVerificationDate.ToString();
            oldValue = _custToEdit.LastVerificationDate.FormatDate();
            if (oldValue.Equals(DateTime.MinValue.FormatDate()) ||
                oldValue.Equals(DateTime.MaxValue.FormatDate()))
                oldValue = string.Empty;

            //if (!_last_verification_date.Equals(string.Empty) && oldValue != _last_verification_date)
            if (oldValue != _last_verification_date)
                otherCustDataChanged = true;

            //oldValue = customerCurrentValues.NextVerificationDate.ToString();
            oldValue = _custToEdit.NextVerificationDate.FormatDate();
            if (oldValue.Equals(DateTime.MinValue.FormatDate()) ||
                oldValue.Equals(DateTime.MaxValue.FormatDate()))
                oldValue = string.Empty;
            //if (!_next_verification_date.Equals(string.Empty) && oldValue != _next_verification_date)
            if (oldValue != _next_verification_date)
                otherCustDataChanged = true;

            oldValue = (customerCurrentValues.Years).ToString();
            if (oldValue != _years)
                otherCustDataChanged = true;

            oldValue = (customerCurrentValues.Months).ToString();
            if (oldValue != _months)
                otherCustDataChanged = true;

            oldValue = checkNull(customerCurrentValues.OwnHome);
            if (oldValue != _own_home)
                otherCustDataChanged = true;

            oldValue = checkNull(customerCurrentValues.MilitaryStationedLocal);
            if (oldValue != _military_stationed_local)
                otherCustDataChanged = true;

            oldValue = (customerCurrentValues.MonthlyRent).ToString();
            if (oldValue != _monthly_rent)
                otherCustDataChanged = true;

            return _nameChanged || _dobChanged || _ssnChanged || otherCustDataChanged;

        }
        #endregion
        #region Events
        private void mapFormFields()
        {
            _marital_status = checkNull(this.txtBoxMarStatus.Text);
            _spouse_first_name = checkNull(this.txtBoxSpouseFName.Text);
            _spouse_last_name = checkNull(this.txtBoxSpouseLName.Text);
            _spouse_ssn = checkNull(this.txtBoxSpouseSSN.Text);
            _cust_sequence_number = checkNull(this.txtBoxCustSeqNumber.Text);
            _privacy_notification_date = checkNull(this.txtBoxPrivacyNotifDate.Text);

            if (this.OptOutFlagcheckBox.Checked)
                _opt_out_flag = "Y";
            else
                _opt_out_flag = "N";

            _status = checkNull(this.txtBoxCustStatus.Text);
            _reason_code = checkNull(this.txtBoxReasonCode.Text);

            _last_verification_date = this.txtBoxLastVerDate.Controls[0].Text.ToString();
            _next_verification_date = this.txtBoxNextVerDate.Controls[0].Text.ToString();

            if (_last_verification_date.Equals("mm/dd/yyyy"))
            {
                _last_verification_date = string.Empty;
            }
            //else
            //{
            //    _lastVerDate = DateTime.Parse(_last_verification_date);
            //}

            if (_next_verification_date.Equals("mm/dd/yyyy"))
                _next_verification_date = string.Empty;
            //else
            //{
            //    _nextVerDate = DateTime.Parse(_next_verification_date);
            //}

            //try
            {
                //_lastVerDate = DateTime.Parse(_last_verification_date);
                //_nextVerDate = DateTime.Parse(_next_verification_date);
            }
            //catch (Exception)
            {
                //_lastVerDate = DateTime.MaxValue;
                //_nextVerDate = DateTime.MaxValue;
            }



            _cooling_off_date_pdl = (checkNull(this.txtBoxPDLCoolingOffDate.Text).ToString());
            _customer_since_pdl = (checkNull(this.txtBoxPDLCustSince.Text).ToString());
            _spanish_form = (this.SpanishFormcheckBox1.Checked).ToString();
            _prbc = (this.PRBCcheckBox.Checked).ToString();
            _planbankruptcy_protection = (this.BankruptcyProtectionradioButton1.Checked).ToString();
            _years = checkNull(this.txtBoxHLAAYears.Text);
            _months = checkNull(this.txtBoxHLAAMonths.Text);
            _own_home = checkNull(this.txtBoxOwnHome.Text);
            _monthly_rent = checkNull(this.txtBoxMonthlyRent.Text);
            
            if (this.MilitaryStationedLocalcheckBox.Checked)
                _military_stationed_local = "Y";
            else
                _military_stationed_local = "N";

        }

        private string checkNull(string value)
        {
            if (string.IsNullOrEmpty(value))
                value = string.Empty;

            return value;
        }
        /*__________________________________________________________________________________________*/
        private void buttonSubmit_Click(object sender, EventArgs e)
        {

            DialogResult dgr;
            checkDateOfBirth();
            if (custDateOfBirth.isValid)
            {
                checkReqFields();
                if (_isFormValid)
                {
                    bool retValue = false;
                    getFormData();
                    mapFormFields();
                    if (custInfoChanged())
                    {
                        /* *****
                         * ***** Skip duplicate check. Refer to Bugzilla #117 
                         * ***** (http://192.168.106.16/bugzilla/show_bug.cgi?id=117)
                         * *****
                        //If name and dob is changed check to see if there are
                        //any other customers in the system with the same information
                        if (_nameChanged || _dobChanged)
                        {
                            if (doNameCheck())
                            {
                                //if there are duplicate customers, show the error message and let the user
                                //decide whether they would like to continue with the change or not
                                    dgr = MessageBox.Show(Commons.GetMessageString("DuplicateNameMessage"), "Warning", MessageBoxButtons.YesNo);
                                    if (dgr == DialogResult.No)
                                        return;                                
                            }
                        }
                        ***** */
                        try
                        {
                            do
                            {
                                retValue = new Support.Controllers.Database.Procedures.CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).UpdatePersonalInfoDetails(
                                    _strTitle, _strFirstName, _strMiddleName, _strLastName, 
                                    _strTitleSuffix, _strLanguage, _strDob, _strSsn, _strUserId, _partyId,
                                    _marital_status,
                                    _spouse_first_name,
                                    _spouse_last_name,
                                    _spouse_ssn,
                                    _cust_sequence_number,
                                    _privacy_notification_date,
                                    _opt_out_flag,
                                    _status,
                                    _reason_code,
                                    _last_verification_date,
                                    _next_verification_date,
                                    _cooling_off_date_pdl,
                                    _customer_since_pdl,
                                    _spanish_form,
                                    _prbc,
                                    _planbankruptcy_protection,
                                    _years,
                                    _months,
                                    _own_home,
                                    _monthly_rent,
                                    _military_stationed_local,
                                    out _errorCode, out _errorMsg);
                                if (retValue)
                                {
                                    MessageBox.Show("Customer information updated");
                                    var updatedCustomer = Support.Logic.CashlinxPawnSupportSession.Instance.ActiveCustomer;
                                    /*CustomerVOForSupportApp updatedCustomer = new CustomerVOForSupportApp
                                                                     {
                                                                         FirstName = _strFirstName,
                                                                         MiddleInitial = _strMiddleName,
                                                                         LastName = _strLastName,
                                                                         DateOfBirth = _dateOfBirth,
                                                                         CustTitle = _strTitle,
                                                                         CustTitleSuffix = _strTitleSuffix,
                                                                         NegotiationLanguage = _strLanguage,
                                                                         SocialSecurityNumber = _strSsn
                                                                     };
                                     */
                                    updatedCustomer.FirstName = _strFirstName;
                                    updatedCustomer.MiddleInitial = _strMiddleName;
                                    updatedCustomer.LastName = _strLastName;
                                    updatedCustomer.DateOfBirth = _dateOfBirth;
                                    updatedCustomer.CustTitle = _strTitle;
                                    updatedCustomer.CustTitleSuffix = _strTitleSuffix;
                                    updatedCustomer.NegotiationLanguage = _strLanguage;
                                    updatedCustomer.SocialSecurityNumber = _strSsn;


                                    updatedCustomer.MaritalStatus = _marital_status;
                                    updatedCustomer.CustSequenceNumber = _cust_sequence_number;
                                    updatedCustomer.OptOutFlag = _opt_out_flag;
                                    if (!string.IsNullOrEmpty(_last_verification_date))
                                        updatedCustomer.LastVerificationDate = Convert.ToDateTime(_last_verification_date);
                                    else
                                        updatedCustomer.LastVerificationDate = DateTime.MinValue;

                                    if (!string.IsNullOrEmpty(_next_verification_date))
                                        updatedCustomer.NextVerificationDate = Convert.ToDateTime(_next_verification_date);
                                    else
                                        updatedCustomer.NextVerificationDate = DateTime.MinValue;


                                    //DateTime _lastVerDate;
                                    //DateTime _nextVerDate;

                                    if (!string.IsNullOrEmpty(_years))
                                        updatedCustomer.Years = Convert.ToInt32(_years);

                                    if (!string.IsNullOrEmpty(_months))
                                        updatedCustomer.Months = Convert.ToInt32(_months);
                                    updatedCustomer.OwnHome = _own_home;
                                    updatedCustomer.MilitaryStationedLocal = _military_stationed_local;

                                    /*updatedCustomer.SpouseFirstName = _spouse_first_name;
                                    updatedCustomer.SpouseLastName = _spouse_last_name;
                                    updatedCustomer.SpouseSsn = _spouse_ssn;
                                    updatedCustomer.PrivacyNotificationDate = Convert.ToDateTime(_privacy_notification_date);
                                    updatedCustomer.Status = _status;
                                    updatedCustomer.ReasonCode = _reason_code;
                                    updatedCustomer.CoolingOffDatePDL = Convert.ToDateTime(_cooling_off_date_pdl);
                                    updatedCustomer.CustomerSincePDL = Convert.ToDateTime(_customer_since_pdl);
                                    updatedCustomer.SpanishForm = _spanish_form;
                                    updatedCustomer.PRBC = _prbc;
                                    updatedCustomer.PlanBankruptcyProtection = _planbankruptcy_protection;
                                    */
                                    string monthlyRent = checkNull(_monthly_rent);
                                    if (!monthlyRent.Equals(string.Empty))
                                        updatedCustomer.MonthlyRent = Convert.ToInt32(_monthly_rent);
                                    else
                                        updatedCustomer.MonthlyRent = 0;
                                    //double Num;
                                    //bool isNum = double.TryParse(_monthly_rent, out Num);
                                    //if (isNum)
                                    //{
                                    //    updatedCustomer.MonthlyRent = Convert.ToDouble(_monthly_rent);
                                    //}
                                    //else
                                    //{
                                    //    updatedCustomer.MonthlyRent = 0;
                                    //}
                                    updatedCustomer.Age = _age != 0 ? _age : _custToEdit.Age;
                                    /*Form ownerForm = this.Owner;
                                    if (ownerForm.GetType() == typeof(ViewCustomerInformation))
                                    {

                                        ((ViewCustomerInformation)ownerForm).UpdatedCustomerToView = updatedCustomer;
                                        ((ViewCustomerInformation)ownerForm).ShowUpdates = true;
                                    }
                                    updatedCustomer = null; */

                                    break;
                                }
                                else
                                {
                                    dgr = _errorCode.Equals("1") ? MessageBox.Show(Commons.GetMessageString("CustPersInfoUpdateFailure") + ":  " + _errorMsg, "Warning", MessageBoxButtons.OK) : MessageBox.Show(Commons.GetMessageString("CustPersInfoUpdateFailure") + ":  " + _errorMsg, "Error", MessageBoxButtons.OK);
                                }
                            } while (dgr == DialogResult.Retry);
                            if (_errorCode.Equals("1"))
                                return;

                        }
                        catch (ApplicationException aEx)
                        {
                            //exception already handled at the oracledataaccessor level
                            MessageBox.Show(aEx.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show(Commons.GetMessageString("NoChangesInForm"));
                    }
                    //this.Close();
                    //this.Dispose(true);
                    this.NavControlBox.IsCustom = true;
                    this.NavControlBox.CustomDetail = "UpdateCustomerDetails";
                    this.NavControlBox.Action = NavBox.NavAction.BACK;
                }
                else
                    MessageBox.Show(Commons.GetMessageString("FormRequiredFieldsFilledError"));
            }
            else
                MessageBox.Show(Commons.GetMessageString("FormRequiredFieldsFilledError"));

        }
        /*__________________________________________________________________________________________*/
        private void buttonReset_Click(object sender, EventArgs e)
        {
            LoadDataInForm();
        }
        /*__________________________________________________________________________________________*/
        private void UpdateCustomerDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }
        /*__________________________________________________________________________________________*/
        private void customHistoryButton_Click(object sender, EventArgs e)
        {
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "ViewPersonalInformationHistory";
            this.NavControlBox.Action = NavBox.NavAction.SUBMIT; 
        }
        /*__________________________________________________________________________________________*/
        private void ChangeStatusCustomButton_Click(object sender, EventArgs e)
        {
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "UpdateCustomerStatus";
            this.NavControlBox.Action = NavBox.NavAction.SUBMIT; 
        }
        /*__________________________________________________________________________________________*/
        private void BtnCustomerComments_Click(object sender, EventArgs e)
        {
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "SupportCustomerComment";
            this.NavControlBox.Action = NavBox.NavAction.SUBMIT; 
        }
        /*__________________________________________________________________________________________*/
        private void customBackButton_Click(object sender, EventArgs e)
        {
            Form currForm = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup(this);
            if (currForm.GetType() == typeof(UpdateCustomerDetails))
            {
                this.NavControlBox.IsCustom = true;
                //this.NavControlBox.CustomDetail = "ViewCustomerInformationReadOnly";
                this.NavControlBox.CustomDetail = "ViewCustomerInformationReadOnly"; // ParentFormName;
                this.NavControlBox.Action = NavBox.NavAction.BACK;
            }
        }
        /*__________________________________________________________________________________________*/
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Form currForm = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup(this);
            if (currForm.GetType() == typeof(UpdateCustomerDetails))
            {
                this.NavControlBox.IsCustom = true;
                //this.NavControlBox.CustomDetail = "LookupCustomer";
                this.NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
        }
        #endregion


    }
}
