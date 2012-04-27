/**************************************************************************************************************
* CashlinxDesktop
* CreateCustomer
* This form is used to create a customer in the manage customer use case
* Sreelatha Rengarajan 6/29/2009 Initial version
**************************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Customer
{
    public partial class CreateCustomer : Form
    {


        private Form ownerFrm;
        string strIdentTypeCode = "";
        string strIdentIssuer = "";
        string strIdentNumber = "";
        private LookupCustomerSearchData currentSearchData;
        string searchIDType = "";
        string searchIDNumber = "";
        string searchIDIssuer = "";


        string strPrimaryEmail;
        string howDidYouHear;
        string receiveOffers;
        string[] strPrimaryPhone;
        string[] strContactType;
        string[] strPhoneNumber;
        string[] strAreaCode;
        string[] strCountryCode;
        string[] strPhoneExtension;
        string[] strTelecomNumTypeCode;
        string strFirstName = "";
        string strLastName = "";
        string strDOB = "";
        string strSSN = "";
        string strMiddleName = "";
        string strComments = "";
        string strTitle = "";
        string strTitleSuffix = "";
        string strUserId = "";
        string strStoreNumber = "";
        string partyId = "";

        int numRows;
        bool _isFormValid;
        bool _idDuplicate;
        bool _nameDuplicate;
        bool _ssnDuplicate;
        bool _dobEntered;
        bool _dobAndIDEmpty;

        //All the IDs shown in the datagrid
        string[] strIdType;
        string[] strIdIssuer;
        string[] strIdNumber;
        string[] strIdExpiryDate;
        string[] strIdentId;
        string[] strIdTypeDesc;
        string[] strIdIssuerCode;
        string[] strIdIssueDate;
        bool retValue = false;

        ProcessingMessage procMsg;
        public NavBox NavControlBox;// { get; set; }
        string custNumber = "";
        StringBuilder errorMessage;
        CustomerVO Customer;
        public CreateCustomer()
        {
            InitializeComponent();
            NavControlBox = new NavBox();
        }

        private void CreateCustomer_Load(object sender, EventArgs e)
        {
            ownerFrm = this.Owner;
            this.NavControlBox.Owner = this;
            var dSession = GlobalDataAccessor.Instance.DesktopSession;
            strStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            strUserId = dSession.UserName;
            currentSearchData = dSession.ActiveLookupCriteria;

            //Set the required fields
            this.pwnapp_dateOfBirth.Required = true;
            this.pwnapp_firstName.Required = true;
            this.pwnapp_lastName.Required = true;
            this.customLabelGovernmentIDHeading.Required = false;
            this.customLabelDateOfBirth.Required = true;
            this.customLabelFirstName.Required = true;
            this.customLabelLastName.Required = true;

            this.pwnapp_dateOfBirth.ErrorMessage = Commons.GetMessageString("InvalidDateOfBirth");
            this.primaryEmailTextBox.ErrorMessage = Commons.GetMessageString("InvalidEmail");

            //Load data for receive promotions combo box
            var ReceivePromotions = new ArrayList();
            ReceivePromotions.Add(new ComboBoxData("", "Select"));
            ReceivePromotions.Add(new ComboBoxData("YS", "Yes"));
            ReceivePromotions.Add(new ComboBoxData("NO", "No"));

            this.comboBoxReceivePromotions.DataSource = ReceivePromotions;
            this.comboBoxReceivePromotions.DisplayMember = "Description";
            this.comboBoxReceivePromotions.ValueMember = "Code";

            if (currentSearchData != null)
            {
                this.pwnapp_firstName.Text = currentSearchData.FirstName;
                this.pwnapp_lastName.Text = currentSearchData.LastName;
                this.pwnapp_dateOfBirth.Controls[0].Text = currentSearchData.DOB;
                this.pwnapp_socialsecuritynumber.Controls[0].Text = currentSearchData.SSN;
                this.phoneData1.populatePhoneNumber(currentSearchData.PhoneAreaCode, currentSearchData.PhoneNumber);

                if (currentSearchData.IdTypeCode.Length != 0 && currentSearchData.IDNumber.Length != 0 &&
                    currentSearchData.IDIssuer.Length != 0)
                {
                    if (currentSearchData.IdTypeCode.Length != 0)
                    {
                        searchIDType = currentSearchData.IdTypeDesc;
                    }
                    if (currentSearchData.IDNumber.Length != 0)
                    {
                        searchIDNumber = currentSearchData.IDNumber;
                    }
                    if (currentSearchData.IdIssuerCode.Length != 0)
                    {
                        searchIDIssuer = currentSearchData.IdIssuerCode;
                    }
                    this.identification1.populateCustomerIdentification(searchIDType, searchIDIssuer, searchIDNumber, "", "");
                }

            }


        }

        private void createCustomerCancelButton_Click(object sender, EventArgs e)
        {
            //CashlinxDesktopSession.Instance.FormState = CashlinxDesktopSession.CustomerFormStates.CANCEL;
            var dgr = MessageBox.Show(Commons.GetMessageString("CancelConfirmMessage"), "Warning", MessageBoxButtons.YesNo);
            if (dgr == DialogResult.Yes)
            {
                //CustomerController.NavigateUser(ownerFrm);
                this.NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
            else
            {
                return;
            }
        }




        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                _idDuplicate = false;
                _nameDuplicate = false;
                getFormDataAndCheckDuplicates();
                if (_isFormValid)
                {
                    if (_idDuplicate || _nameDuplicate)
                        return;
                    createCustomerObject();
                    if (saveData())
                    {
                        //CashlinxDesktopSession.Instance.FormState = CashlinxDesktopSession.CustomerFormStates.ADDCUSTOMERCOMPLETE;
                        this.NavControlBox.IsCustom = true;
                        this.NavControlBox.CustomDetail = "AddCustomerComplete";
                        this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                    }
                    else
                    {
                        if (_ssnDuplicate)
                            return;
                        //CashlinxDesktopSession.Instance.FormState = CashlinxDesktopSession.CustomerFormStates.CANCEL;
                        this.NavControlBox.Action = NavBox.NavAction.CANCEL;
                    }
                    //CustomerController.NavigateUser(ownerFrm);
                }
                else
                {
                    if (_dobAndIDEmpty)
                    {
                        MessageBox.Show(Commons.GetMessageString("IDorDOBEnterMessage"));
                    }
                    else
                    MessageBox.Show(Commons.GetMessageString("FormRequiredFieldsFilledError"));
                    return;
                }
            }
            catch (Exception)
            {
                //CashlinxDesktopSession.Instance.FormState = CashlinxDesktopSession.CustomerFormStates.CANCEL;
                //CustomerController.NavigateUser(ownerFrm);
                this.NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
        }

        private void getFormDataAndCheckDuplicates()
        {
            //Get all the details from the form
            _isFormValid = true;
            getIdentificationDetails();
            getPersonalInfoDetails();
            getContactInfoDetails();
            getComments();
            checkFormFields();
            
            if (_isFormValid)
            {
                //Check if duplicate Id exists
                //If it does do not allow the user to proceed
                if (checkIDDuplicates())
                {
                    MessageBox.Show(errorMessage.ToString(), "Error");
                    _idDuplicate = true;
                    return;
                }

                //check if a duplicate name/dob exists
                //If it does, user is given an option to proceed or not
                if (checkNameDOBDuplicates())
                {

                    var dgRes = MessageBox.Show(errorMessage.ToString(), "Warning", MessageBoxButtons.YesNo);
                    if (dgRes == DialogResult.No)
                    {
                        _nameDuplicate = true;
                        return;
                    }

                }
                if (checkSSNDuplicate())
                {
                    MessageBox.Show(errorMessage.ToString(), "Error");
                    _ssnDuplicate = true;
                    return;

                }
            }
            else
                return;

        }



        private void checkFormFields()
        {
            if (!this.pwnapp_firstName.isValid ||
                !this.pwnapp_lastName.isValid)
                _isFormValid = false;
            if (!_dobEntered && strIdType.Length == 0)
            {
                _isFormValid = false;
                _dobAndIDEmpty = true;
            }
            if (_dobEntered)
            {
                checkDateOfBirth();
                if (!this.pwnapp_dateOfBirth.isValid)
                    _isFormValid = false;
            }   


            if (strIdType.Length > 0)
            {
                if (!identification1.isValid())
                    _isFormValid = false;
                customLabelDateOfBirth.Required = false;
            }
                
            

        }

        private bool saveData()
        {


            procMsg = new ProcessingMessage("Saving Customer Data");
            SetButtonState(false);
            var bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.RunWorkerAsync();
            procMsg.ShowDialog(this);
            if (retValue)
            {
                MessageBox.Show(Commons.GetMessageString("CustCreationSuccess"));
                var custObject = CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, custNumber);
                if (custObject != null)
                {
                    GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = custObject;
                    return true;
                }
                else
                {
                    //error since the customer object could not be retrieved after save
                    return false;
                }

            }
            else
                return false;


        }

        private void SetButtonState(bool enable)
        {
            customButtonSave.Enabled = enable;
            customButtonContinue.Enabled = enable;
            customButtonCancel.Enabled = enable;
            customButtonClear.Enabled = enable;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var errorDesc = string.Empty;
            DialogResult dgr = DialogResult.Retry;
            //Make a call to save in the database

            do
            {
                retValue = CustomerProcedures.AddCustomer(GlobalDataAccessor.Instance.DesktopSession, Customer, strUserId, strStoreNumber, out custNumber, out partyId, out errorDesc);
                if (retValue)
                    break;
                else
                {
                    if (errorDesc == CustomerProcedures.DUPLICATESSNORACLEERRORCODE)
                    {
                        _ssnDuplicate = true;
                        dgr = MessageBox.Show(Commons.GetMessageString("DuplicateSSNMessage"), "Warning", MessageBoxButtons.OK);
                    }
                    else
                        dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);
                    if (dgr == DialogResult.OK)
                        break;
                }
            } while (dgr == DialogResult.Retry);

        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            procMsg.Update();
            procMsg.Close();
            procMsg.Dispose();
            SetButtonState(true);
        }


        private bool checkNameDOBDuplicates()
        {
            DataTable custDatatable = new DataTable();
            var errorCode = string.Empty;
            var errorMsg = string.Empty;
            bool b = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).CheckDuplicateNameDOB(strFirstName, strLastName, strDOB, out custDatatable, out errorCode, out errorMsg);
            if (b)
            {
                if (custDatatable != null && custDatatable.Rows.Count > 0)
                {
                    errorMessage = new StringBuilder();
                    errorMessage.Append(Commons.GetMessageString("DuplicateNameMessage"));
                    return true;
                }

            }
            else
                throw new ApplicationException("An error occurred in the stored procedure call");
            return false;

        }

        private bool checkIDDuplicates()
        {
            bool checkDuplicates = false;

            for (int i = 0; i < strIdType.Length; i++)
            {
                strIdentTypeCode = strIdType[i];
                strIdentNumber = strIdNumber[i];
                strIdentIssuer = strIdIssuerCode[i];
                var errorCode = string.Empty;
                var errorMsg = string.Empty;
                DataTable custDatatable;
                bool b = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).CheckDuplicateID(strIdentTypeCode, strIdentNumber, strIdentIssuer, out custDatatable, out errorCode, out errorMsg);
                if (b)
                {
                    //call to duplicate id check yielded records which means there are users that match the ID
                    if (custDatatable != null && custDatatable.Rows.Count > 0)
                    {
                        errorMessage = new StringBuilder();
                        errorMessage.Append(Commons.GetMessageString("DuplicateIDMessage"));
                        errorMessage.Append("  <");
                        errorMessage.Append(strIdentTypeCode);
                        errorMessage.Append(",");
                        errorMessage.Append(strIdentIssuer);
                        errorMessage.Append(",");
                        errorMessage.Append(strIdentNumber);
                        errorMessage.Append(">");
                        checkDuplicates = true;
                        break;
                    }
                    else
                    {
                        //remove break so that loop will execute
                        checkDuplicates = false;
                        //break;
                    }
                }
                else
                    throw new ApplicationException("An error occurred in the stored procedure call");
            }

            return checkDuplicates;


        }

        private bool checkSSNDuplicate()
        {
            if (strSSN.Length != 0)
            {
                _ssnDuplicate = CustomerProcedures.checkCustomerExistsBySSN(GlobalDataAccessor.Instance.DesktopSession, strSSN);
                if (_ssnDuplicate)
                {
                    errorMessage = new StringBuilder();
                    errorMessage.Append(Commons.GetMessageString("DuplicateSSNMessage"));

                }

                return _ssnDuplicate;
            }
            return false;
        }

        private void getComments()
        {
            strComments = this.comments.Text;
        }

        private void getContactInfoDetails()
        {

            List<ContactVO> customerPhoneNumbers = this.phoneData1.getPhoneData();
            if (phoneData1.IsValid)
            {
                _isFormValid = true;
                if (customerPhoneNumbers != null)
                {
                    numRows = customerPhoneNumbers.Count;
                    //Array inputs
                    strPrimaryPhone = new string[numRows];
                    strPhoneNumber = new string[numRows];
                    strAreaCode = new string[numRows];
                    strPhoneExtension = new string[numRows];
                    strCountryCode = new string[numRows];
                    strContactType = new string[numRows];
                    strTelecomNumTypeCode = new string[numRows];
                    int i = 0;
                    foreach (ContactVO phone in customerPhoneNumbers)
                    {
                        strPrimaryPhone[i] = phone.TeleusrDefText;
                        strContactType[i] = phone.ContactType;
                        strAreaCode[i] = phone.ContactAreaCode;
                        strPhoneNumber[i] = phone.ContactPhoneNumber;
                        strPhoneExtension[i] = phone.ContactExtension;
                        strCountryCode[i] = phone.CountryDialNumCode;
                        strTelecomNumTypeCode[i] = phone.TelecomNumType;
                        i++;
                    }
                }
                else
                {
                    strPrimaryPhone = new string[1];
                    strPhoneNumber = new string[1];
                    strAreaCode = new string[1];
                    strPhoneExtension = new string[1];
                    strCountryCode = new string[1];
                    strContactType = new string[1];
                    strTelecomNumTypeCode = new string[1];
                    strPrimaryPhone[0] = "";
                    strPhoneNumber[0] = "";
                    strAreaCode[0] = "";
                    strPhoneExtension[0] = "";
                    strCountryCode[0] = "";
                    strContactType[0] = "";
                    strTelecomNumTypeCode[0] = "";

                }
                //Get Email Address
                strPrimaryEmail = primaryEmailTextBox.Text;
                ComboBox howdidyouHearAbtUs = (ComboBox)this.hearAboutUs1.Controls[0];
                howDidYouHear = howdidyouHearAbtUs.SelectedValue.ToString();
                receiveOffers = comboBoxReceivePromotions.SelectedValue.ToString();

            }
            else
            {
                _isFormValid = false;
            }


        }




        private void getIdentificationDetails()
        {
            if (this.identification1.isValid())
            {
                List<IdentificationVO> customerIds = this.identification1.getIdentificationData();
                if (customerIds != null)
                {
                    numRows = customerIds.Count;
                    //Array inputs
                    strIdType = new string[numRows];
                    strIdIssuer = new string[numRows];
                    strIdNumber = new string[numRows];
                    strIdExpiryDate = new string[numRows];
                    strIdentId = new string[numRows];
                    strIdIssuerCode = new string[numRows];
                    strIdTypeDesc = new string[numRows];
                    strIdIssueDate = new string[numRows];
                    int i = 0;
                    foreach (IdentificationVO id in customerIds)
                    {
                        strIdTypeDesc[i] = id.DatedIdentDesc;
                        strIdType[i] = id.IdType;
                        strIdIssuer[i] = id.IdIssuer;
                        strIdNumber[i] = id.IdValue;
                        strIdIssuerCode[i] = id.IdIssuerCode;
                        strIdIssueDate[i] = id.IdIssueDate.FormatDate();
                        strIdExpiryDate[i] = id.IdExpiryData.FormatDate();
                        i++;
                    }
                }
                //if no cust ids then error
                else
                    _isFormValid = false;

            }
            else
            {
                _isFormValid = false;
            }
        }


        private void getPersonalInfoDetails()
        {
            var title = (ComboBox)this.pwnapp_title.Controls[0];
            var titsuffix = (ComboBox)this.pwnapp_titleSuffix.Controls[0];
            strFirstName = this.pwnapp_firstName.Text;
            strLastName = this.pwnapp_lastName.Text;
            var dateofbirth = this.pwnapp_dateOfBirth.Controls[0].Text.ToString();
            strDOB = dateofbirth.Equals("mm/dd/yyyy") ? "" : dateofbirth;
            strMiddleName = this.pwnapp_middleInitial.Text;
            strSSN = this.pwnapp_socialsecuritynumber.Controls[0].Text;
            if (title.SelectedIndex > 0)
                strTitle = title.SelectedValue.ToString();
            else
                strTitle = "";
            if (titsuffix.SelectedIndex > 0)
                strTitleSuffix = titsuffix.SelectedValue.ToString();
            else
                strTitleSuffix = "";

        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            _idDuplicate = false;
            _nameDuplicate = false;
            _ssnDuplicate = false;
            try
            {
                getFormDataAndCheckDuplicates();
                if (_idDuplicate || _nameDuplicate || _ssnDuplicate)
                    return;
                if (_isFormValid)
                {
                    createCustomerObject();
                    if (Customer != null)
                    {
                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = Customer;
                    //this.Hide();
                    //UpdateAddress addrForm = new UpdateAddress();
                    //CashlinxDesktopSession.Instance.HistorySession.AddForm(addrForm);
                    //addrForm.Show(ownerFrm);
                    this.NavControlBox.IsCustom = true;
                    this.NavControlBox.CustomDetail = "UpdateAddress";
                    this.NavControlBox.Action = NavBox.NavAction.HIDEANDSHOW;
                }
                else
                {
                    BasicExceptionHandler.Instance.AddException("Error loading customer data in customer object in session ", new ApplicationException());
                    //CashlinxDesktop.Desktop.CashlinxDesktopSession.Instance.HistorySession.Desktop();
                    this.NavControlBox.Action = NavBox.NavAction.CANCEL;
                    }
                }
                else
                {
                    MessageBox.Show(Commons.GetMessageString("FormRequiredFieldsFilledError"));
                    return;
                }
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Error in Create Customer - Continue Action ", ex);

            }
        }

        private void createCustomerObject()
        {
            Customer = new CustomerVO();
            CustomerProcedures.setCustomerDataInObject(GlobalDataAccessor.Instance.DesktopSession, Customer, strFirstName,
        strMiddleName, strLastName, strDOB, strSSN, strTitle, strTitleSuffix, strComments,
        howDidYouHear, receiveOffers, "", "", "", "", "", "", "");
            CustomerProcedures.setCustomerIDDataInObject(Customer, strIdNumber, strIdIssueDate, strIdExpiryDate, strIdIssuerCode, strIdIssuer, strIdType);
            CustomerProcedures.setCustomerPhoneDataInObject(Customer, strContactType, strPhoneNumber, strAreaCode, strCountryCode, strPhoneExtension, strTelecomNumTypeCode, strPrimaryPhone);
            string[] strEmail = new string[1];
            strEmail[0] = strPrimaryEmail;
            string[] strEmailType = new string[1];
            strEmailType[0] = CustomerEmailTypes.PRIMARY_EMAIL;
            Customer.CustomerNumber = custNumber;
            Customer.PartyId = partyId;
            CustomerProcedures.setCustomerEmailDataInObject(Customer, strEmail, strEmailType);
        }

        private void pwnapp_dateOfBirth_Leave(object sender, EventArgs e)
        {
            bool retVal = checkDateOfBirth();
            if (_dobEntered)
            {
                customLabelGovernmentIDHeading.Required = false;
                customLabelGovernmentIDHeading.Invalidate();
                customLabelGovernmentIDHeading.Update();
                this.Refresh();
                
            }
            else
            {
                customLabelGovernmentIDHeading.Required = true;
                customLabelGovernmentIDHeading.Invalidate();
                customLabelGovernmentIDHeading.Update();
                this.Refresh();

            }


        }

        private bool checkDateOfBirth()
        {
            if (pwnapp_dateOfBirth.isValid)
            {
                string _dob = this.pwnapp_dateOfBirth.Controls[0].Text;
                if (!_dob.Equals("mm/dd/yyyy"))
                {
                    _dobEntered = true;
                    int age = Commons.getAge(_dob, ShopDateTime.Instance.ShopDate);
                    if (age <= 0)
                    {

                        MessageBox.Show(Commons.GetMessageString("InvalidDateOfBirth"));
                        _isFormValid = false;
                        return false;
                    }
                    else
                        return true;
                }
                else
                {
                    
                    _dobEntered = false;
                    return true;
                }
            }
            else
            {

                _isFormValid = false;
                return false;
            }

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.pwnapp_dateOfBirth.Controls[0].Text = "";
            this.pwnapp_firstName.Text = "";
            this.pwnapp_lastName.Text = "";
            this.pwnapp_middleInitial.Text = "";
            this.pwnapp_socialsecuritynumber.Controls[0].Text = "";
            ComboBox custTitle = (ComboBox)this.pwnapp_title.Controls[0];
            custTitle.SelectedIndex = -1;
            ComboBox custTitSuffix = (ComboBox)this.pwnapp_titleSuffix.Controls[0];
            custTitSuffix.SelectedIndex = -1;
            this.identification1.Clear();
            this.phoneData1.Clear();
            this.comments.Text = "";
            ComboBox hrAboutUs = (ComboBox)this.hearAboutUs1.Controls[0];
            hrAboutUs.SelectedIndex = -1;
            this.primaryEmailTextBox.Text = "";
            this.comboBoxReceivePromotions.SelectedIndex = -1;
        }

        private void identification1_Leave(object sender, EventArgs e)
        {
            getIdentificationDetails();
            if (_dobEntered)
            {
                customLabelGovernmentIDHeading.Required = false;
                customLabelGovernmentIDHeading.Invalidate();
                customLabelGovernmentIDHeading.Update();
                this.Refresh();
            }
            else
            {
                if (strIdType.Length > 0)
                {
                    customLabelDateOfBirth.Required = false;
                    customLabelDateOfBirth.Invalidate();
                    customLabelDateOfBirth.Update();
                    this.Refresh();
                }
            }
        }


    }
}


