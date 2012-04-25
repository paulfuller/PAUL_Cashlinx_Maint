/********************************************************************
* CashlinxDesktop
* UpdateCustomerDetails
* This form is shown when a user's personal information like name, date of birth
 * social security number need to be updated
* Sreelatha Rengarajan 5/14/2009 Initial version
 * Sreelatha Rengarajan 7/17/2009   Added logic for checking if sp call 
 *                                  failed because of ssn duplicate
*******************************************************************/

using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Customer
{
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
        private CustomerVO _custToEdit;
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
        string _strSsn;
        bool _dobNotEntered;
        int _age;

        public CustomerVO CustToEdit
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



        public UpdateCustomerDetails()
        {
            InitializeComponent();
            this.custDateOfBirth.ErrorMessage = Commons.GetMessageString("InvalidDateOfBirth");

            ArrayList LanguageTypes = new ArrayList();
            LanguageTypes.Add(new ComboBoxData("", "Select"));
            LanguageTypes.Add(new ComboBoxData("en", "English"));
            LanguageTypes.Add(new ComboBoxData("es", "Spanish"));

            this.comboBoxLanguage.DataSource = LanguageTypes;
            this.comboBoxLanguage.DisplayMember = "Description";
            this.comboBoxLanguage.ValueMember = "Code";
        }



        private void UpdateCustomerDetails_Load(object sender, EventArgs e)
        {
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

        private void LoadDataInForm()
        {
            this.custFirstName.Text = _firstName;
            this.custLastName.Text = _lastName;
            this.custMiddleInitial.Text = _middleInitial;
            this.custDateOfBirth.Controls[0].Text = _dob.FormatDate();
            this.custSSN.Controls[0].Text = _ssn;

            ComboBox custTitle = (ComboBox)this.title1.Controls[0];
            if (_title.Length != 0)
            {
                foreach (ComboBoxData currTitle in custTitle.Items)
                    if (currTitle.Code == _title)
                    {
                        custTitle.SelectedIndex = custTitle.Items.IndexOf(currTitle);
                        break;
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            
            this.Close();
            this.Dispose();
        }

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
                                retValue = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).UpdatePersonalInfoDetails(_strTitle, _strFirstName, _strMiddleName, _strLastName, _strTitleSuffix, _strLanguage, _strDob, _strSsn, _strUserId, _partyId, out _errorCode, out _errorMsg);
                                if (retValue)
                                {
                                    MessageBox.Show("Customer information updated");
                                    CustomerVO updatedCustomer = new CustomerVO
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
                                    updatedCustomer.Age = _age != 0 ? _age : _custToEdit.Age;
                                    Form ownerForm = this.Owner;
                                    if (ownerForm.GetType() == typeof(ViewCustomerInformation))
                                    {

                                        ((ViewCustomerInformation)ownerForm).UpdatedCustomerToView = updatedCustomer;
                                        ((ViewCustomerInformation)ownerForm).ShowUpdates = true;
                                    }
                                    updatedCustomer = null;

                                    break;
                                }
                                else
                                {
                                    dgr = _errorCode.Equals("1") ? MessageBox.Show(Commons.GetMessageString("DuplicateSSNMessage"),"Warning",MessageBoxButtons.OK) : MessageBox.Show(Commons.GetMessageString("CustPersInfoUpdateFailure"), "Error", MessageBoxButtons.RetryCancel);
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
                    this.Close();
                    this.Dispose(true);

                }
                else
                    MessageBox.Show(Commons.GetMessageString("FormRequiredFieldsFilledError"));
            }
            else
                MessageBox.Show(Commons.GetMessageString("FormRequiredFieldsFilledError"));

        }

 

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
                otherCustDataChanged=true;

            return _nameChanged || _dobChanged || _ssnChanged || otherCustDataChanged;
 
            
        }

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

        private void checkReqFields()
        {
            if (this.custFirstName.isValid && this.custLastName.isValid && 
                this.comboBoxLanguage.SelectedIndex > 0 && !_dobNotEntered)
                _isFormValid = true;
            else
                _isFormValid = false;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            LoadDataInForm();
        }

        private void custDateOfBirth_Leave(object sender, EventArgs e)
        {
            checkDateOfBirth();

        }

        private void checkDateOfBirth()
        {
            string birthdate = this.custDateOfBirth.Controls[0].Text.Trim().ToString();
            _dobNotEntered = birthdate.Equals("mm/dd/yyyy");
            if (!_dobNotEntered && birthdate.Length > 0)
            {
                try
                {

                    _age = Commons.getAge(birthdate, ShopDateTime.Instance.ShopDate);
                    int validAge = CustomerProcedures.getPawnBRCustomerLegalAge(GlobalDataAccessor.Instance.DesktopSession);
                    //if we are here and do not have a value for validAge, the call to get it
                    //from business rule engine failed. Hence assign the default value.
                    if (validAge == 0)
                        validAge = CustomerValidAge.PAWNCUSTLEGALAGE;
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


                    }


                }
                catch (Exception)
                {
                    MessageBox.Show(Commons.GetMessageString("InvalidDateOfBirth"));
                    custDateOfBirth.Focus();
                }
            }
 

        }
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

        private void UpdateCustomerDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

 
  
 
    }

 



}
