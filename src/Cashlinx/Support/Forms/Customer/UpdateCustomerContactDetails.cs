/********************************************************************
* CashlinxDesktop
* UpdateCustomerContactDetails
* This form is shown when a user's contact information like phone, email
* need to be updated
* Sreelatha Rengarajan 5/20/2009 Initial version
*******************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Support.Forms.Pawn.Customer;
using Support.Libraries.Objects.Customer;

namespace Support.Forms.Customer
{

    /*__________________________________________________________________________________________*/
    public partial class UpdateCustomerContactDetails : Form
    {
        private CustomerVO _custToEdit;

        private int _numberOfPhoneNumbers;

        string[] _strPrimaryPhone;
        string[] _strContactType;
        string[] _strPhoneNumber;
        string[] _strAreaCode;
        string[] _strCountryCode;
        string[] _strPhoneExtension;
        string[] _strTelecomNumTypeCode;
        string[] _custEmails;
        string[] _custEmailId;
        string[] _custEmailType;
        string _noCalls;
        string _noEmails;
        string _noFaxes;
        string _noMail;
        string _optOut;
        string _remindPmtDue;
        string _preferContact;
        string _preferCallTime;
        string _howDidYouHear;
        string _receiveOffers;
        readonly string _userId;
        string _errorCode;
        string _errorMsg;
        bool _updatePhoneData = false;
        ProcessingMessage _procMsg;
        DataTable _custPhone = new DataTable();
        DataTable _custEmail = new DataTable();
        bool _isFormValid = false;

       	private Form ownerFrm;
        public NavBox NavControlBox;


        #region FORMSTARTUP
        /*__________________________________________________________________________________________*/
        public UpdateCustomerContactDetails()
        {
            InitializeComponent();

            this.NavControlBox = new NavBox();

            //Load data for preferred contact combo box
            ArrayList PrefContactTypes = new ArrayList();
            PrefContactTypes.Add(new ComboBoxData("", "Select"));
            PrefContactTypes.Add(new ComboBoxData("HPH", "Home Phone"));
            PrefContactTypes.Add(new ComboBoxData("CPH", "Cell Phone"));
            PrefContactTypes.Add(new ComboBoxData("WPH", "Work Phone"));
            PrefContactTypes.Add(new ComboBoxData("EML", "Email"));

            this.comboBoxPrefContact.DataSource = PrefContactTypes;
            this.comboBoxPrefContact.DisplayMember = "Description";
            this.comboBoxPrefContact.ValueMember = "Code";

            //Load data for preferred call time combo box
            ArrayList PrefContactTime = new ArrayList();
            PrefContactTime.Add(new ComboBoxData("", "Select"));
            PrefContactTime.Add(new ComboBoxData("MOR", "Morning"));
            PrefContactTime.Add(new ComboBoxData("EVN", "Evening"));
            PrefContactTime.Add(new ComboBoxData("AFT", "Afternoon"));

            this.comboBoxPrefCallTime.DataSource = PrefContactTime;
            this.comboBoxPrefCallTime.DisplayMember = "Description";
            this.comboBoxPrefCallTime.ValueMember = "Code";

            //Load data for receive promotions combo box
            ArrayList ReceivePromotions = new ArrayList();
            ReceivePromotions.Add(new ComboBoxData("", "Select"));
            ReceivePromotions.Add(new ComboBoxData("YS", "Yes"));
            ReceivePromotions.Add(new ComboBoxData("NO", "No"));

            this.comboBoxReceivePromotions.DataSource = ReceivePromotions;
            this.comboBoxReceivePromotions.DisplayMember = "Description";
            this.comboBoxReceivePromotions.ValueMember = "Code";

            this.primaryEmailTextBox.ErrorMessage = Commons.GetMessageString("InvalidEmail");
            this.alternateEmailTextBox.ErrorMessage = Commons.GetMessageString("InvalidEmail");
            _userId = GlobalDataAccessor.Instance.DesktopSession.UserName;

        }
        /*__________________________________________________________________________________________*/
        private void UpdateCustomerContactDetails_Load(object sender, EventArgs e)
        {

            ownerFrm = this.Owner;
            this.NavControlBox.Owner = this;

            _custToEdit =  GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
            try
            {

                LoadCustomerContactData();
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException(Commons.GetMessageString("ProcessingError"), ex);
                this.Close();
                this.Dispose(true);
            }
        }
        #endregion
        #region MEMBER PROPERTIES
        /*__________________________________________________________________________________________*/
        public CustomerVO CustomerToEdit
        {
            set
            {
                _custToEdit = value;
            }
        }

        #endregion
        # region BOOLEANS
        /*__________________________________________________________________________________________*/
        private void SetButtonState(bool enable)
        {
            customButtonCancel.Enabled = enable;
            //customButtonReset.Enabled = enable;
            //customButtonSubmit.Enabled = enable;
        }
        /*__________________________________________________________________________________________*/
        private bool contactInfoChanged()
        {
            bool phoneDataChanged = false;
            bool emailDataChanged = false;
            bool contactPreferencesChanged = false;
            //Check if any of the phone numbers changed
            if (_strTelecomNumTypeCode.Length != _custToEdit.NumberContacts)
                //If the number of phone numbers now is not the same as number of contacts
                //in the customer object then phone number has changed
                phoneDataChanged = true;
            else if (_strTelecomNumTypeCode[0] == "" && _custToEdit.NumberContacts > 0)
                //If the type is empty but the customer object has contacts that implies
                //that we are deleting all of the phone numbers
                phoneDataChanged = true;
            else
            {
                for (int i = 0; i < _strTelecomNumTypeCode.Length; i++)
                {
                    ContactVO custContInfo = _custToEdit.getContact(_strContactType[i], _strAreaCode[i],
                        _strPhoneNumber[i], _strPhoneExtension[i], _strCountryCode[i], _strPrimaryPhone[i]);
                    //If contact object in the current customer's context does not match
                    //the data on the form for one of the types(home,cell,work,pager,fax)
                    //that means phone data has been changed
                    if (custContInfo == null)
                    {
                        phoneDataChanged = true;
                        break;
                    }
                }
            }
            //Check if Email address changed
            string strPrimaryEmail = _custToEdit.getPrimaryEmail().EmailAddress;
            string strAlternateEmail = _custToEdit.getAlternateEmail().EmailAddress;
            if (strPrimaryEmail != null && strPrimaryEmail != primaryEmailTextBox.Text)
                emailDataChanged = true;
            else if (strPrimaryEmail == null && primaryEmailTextBox.Text.Length > 0)
                emailDataChanged = true;
            if (strAlternateEmail != null && strAlternateEmail != alternateEmailTextBox.Text)
                emailDataChanged = true;
            else if (strAlternateEmail == null && alternateEmailTextBox.Text.Length > 0)
                emailDataChanged = true;


            //Check if contact preferences changed
            if (_custToEdit.NoCallFlag != _noCalls || _custToEdit.NoEmailFlag != _noEmails || _custToEdit.NoFaxFlag != _noFaxes
                || _custToEdit.NoMailFlag != _noMail || _custToEdit.OptOutFlag != _optOut || _custToEdit.ReminderContact != _remindPmtDue
                || _custToEdit.PreferredContactMethod != _preferContact || _custToEdit.PreferredCallTime != _preferCallTime
                || _custToEdit.HearAboutUs != _howDidYouHear || _custToEdit.ReceivePromotionOffers != _receiveOffers)
                contactPreferencesChanged = true;

            if (phoneDataChanged || emailDataChanged || contactPreferencesChanged)
                return true;
            else
                return false;

        }
        #endregion
        #region DATA
        /*__________________________________________________________________________________________*/
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {

            _updatePhoneData = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).UpdateCustomerContactInfo(_strContactType, _strPhoneNumber, _strAreaCode, _strCountryCode, _strPhoneExtension, _strTelecomNumTypeCode,
    _strPrimaryPhone, _custEmails, _custEmailId, _custEmailType, _noCalls, _noEmails, _noFaxes, _noMail, _optOut, _remindPmtDue, _preferContact,
    _preferCallTime, _howDidYouHear, _receiveOffers, _custToEdit.CustomerNumber, _custToEdit.PartyId, _userId, out _custEmail, out _custPhone, out _errorCode, out _errorMsg);

        }
        /*__________________________________________________________________________________________*/
        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _procMsg.Update();
            _procMsg.Close();
            _procMsg.Dispose();
            SetButtonState(true);
            if (_updatePhoneData)
            {
                MessageBox.Show(Commons.GetMessageString("CustContactInfoUpdateSuccess"));
                //Update the customer object with the updated information
                CustomerVOForSupportApp updatedCustomer = new CustomerVOForSupportApp
                {
                    NoCallFlag = _noCalls,
                    NoEmailFlag = _noEmails,
                    NoFaxFlag = _noFaxes,
                    NoMailFlag = _noMail,
                    OptOutFlag = _optOut,
                    ReminderContact = _remindPmtDue,
                    PreferredContactMethod = _preferContact,
                    PreferredCallTime = _preferCallTime,
                    HearAboutUs = _howDidYouHear,
                    ReceivePromotionOffers = _receiveOffers
                };

                //set the phone data for the customer

                if (_custPhone != null)
                {

                    foreach (DataRow contact in _custPhone.Rows)
                    {
                        ContactVO custcontact = new ContactVO
                        {
                            TelecomNumType =
                                Utilities.GetStringValue(
                                contact.ItemArray[
                                    (int)customerphonerecord.TELECOMNUMTYPECODE], ""),
                            ContactAreaCode =
                                Utilities.GetStringValue(
                                contact.ItemArray[(int)customerphonerecord.AREADIALNUMCODE],
                                ""),
                            ContactPhoneNumber =
                                Utilities.GetStringValue(
                                contact.ItemArray[(int)customerphonerecord.TELECOMNUMBER],
                                ""),
                            ContactExtension =
                                Utilities.GetStringValue(
                                contact.ItemArray[(int)customerphonerecord.EXTENSIONNUMBER],
                                ""),
                            ContactType =
                                Utilities.GetStringValue(
                                contact.ItemArray[(int)customerphonerecord.CONTACTTYPECODE],
                                ""),
                            TeleusrDefText =
                                Utilities.GetStringValue(
                                contact.ItemArray[(int)customerphonerecord.TELEUSRDEFTEXT],
                                ""),
                            CountryDialNumCode =
                                Utilities.GetStringValue(
                                contact.ItemArray[
                                    (int)customerphonerecord.COUNTRYDIALNUMCODE], "")
                        };
                        updatedCustomer.addContact(custcontact);

                    }

                }
                //Set the Email data for the customer

                if (_custEmail != null)
                {

                    foreach (DataRow email in _custEmail.Rows)
                    {
                        string emailAddr = Utilities.GetStringValue(email.ItemArray[(int)customeremailrecord.EMAILADDRESS], "");
                        string emailAddrType = Utilities.GetStringValue(email.ItemArray[(int)customeremailrecord.EMAILADDRESSTYPECODE], "");
                        string contactinfoid = Utilities.GetStringValue(email.ItemArray[(int)customeremailrecord.CONTACTINFOID], "");
                        CustomerEmailVO custemail = new CustomerEmailVO(emailAddr, emailAddrType, contactinfoid);
                        updatedCustomer.addEmail(custemail);
                    }

                }

                Form ownerForm = this.Owner;
                if (ownerForm.GetType() == typeof(ViewCustomerInformation))
                {

                    ((ViewCustomerInformation)ownerForm).UpdatedCustomerToView = updatedCustomer;
                    ((ViewCustomerInformation)ownerForm).ShowUpdates = true;
                }
            }




        }
        /*__________________________________________________________________________________________*/
        private void LoadCustomerContactData()
        {

            //populate all phone numbers of the customer
            this.phoneData1.populatePhoneNumber(_custToEdit.CustomerContacts);

            //Populate Email Address
            //Get primary email address
            CustomerEmailVO primaryEmailObj = _custToEdit.getPrimaryEmail();
            string custPrimaryEmail;
            if (primaryEmailObj.EmailAddress != null)
                custPrimaryEmail = primaryEmailObj.EmailAddress.ToString();
            else
                custPrimaryEmail = string.Empty;
            if (custPrimaryEmail != string.Empty)
                this.primaryEmailTextBox.Text = custPrimaryEmail;


            string custAlternateEmail;
            CustomerEmailVO alternateEmailObj = _custToEdit.getAlternateEmail();
            if (alternateEmailObj.EmailAddress != null)
                custAlternateEmail = alternateEmailObj.EmailAddress;
            else
                custAlternateEmail = string.Empty;
            if (custAlternateEmail != string.Empty)
                this.alternateEmailTextBox.Text = custAlternateEmail;



            //Set contact permissions
            if (_custToEdit.NoCallFlag == "Y")
                this.checkBoxNoCall.Checked = true;
            if (_custToEdit.NoFaxFlag == "Y")
                this.checkBoxNoFax.Checked = true;
            if (_custToEdit.NoMailFlag == "Y")
                this.checkBoxNoMail.Checked = true;
            if (_custToEdit.NoEmailFlag == "Y")
                this.checkBoxNoEmail.Checked = true;
            if (_custToEdit.OptOutFlag == "Y")
                this.checkBoxOptOut.Checked = true;


            //Set contact reminder
            if (_custToEdit.ReminderContact == "Y")
                this.checkBoxReminder.Checked = true;

            //Set Preferred Contact
            if (_custToEdit.PreferredContactMethod != null)
            {
                string prefContMethod = _custToEdit.PreferredContactMethod;
                if (prefContMethod.Length != 0)
                {
                    foreach (ComboBoxData currData in comboBoxPrefContact.Items)
                        if (currData.Code == prefContMethod)
                        {
                            comboBoxPrefContact.SelectedIndex = comboBoxPrefContact.Items.IndexOf(currData);
                            break;
                        }

                }
                else
                    comboBoxPrefContact.SelectedIndex = 0;
            }

            //Set Preferred Call time
            if (_custToEdit.PreferredCallTime != null)
            {
                string prefCallTime = _custToEdit.PreferredCallTime;
                if (prefCallTime.Length != 0)
                {
                    foreach (ComboBoxData currData in comboBoxPrefCallTime.Items)
                        if (currData.Code == prefCallTime)
                        {
                            comboBoxPrefCallTime.SelectedIndex = comboBoxPrefCallTime.Items.IndexOf(currData);
                            break;
                        }

                }
                else
                    comboBoxPrefCallTime.SelectedIndex = 0;
            }


            //Set hear about us data
            ComboBox hearAboutUs = (ComboBox)this.hearAboutUs1.Controls[0];
            if (_custToEdit.HearAboutUs != null)
            {
                string strHearAboutUs = _custToEdit.HearAboutUs;
                if (strHearAboutUs.Length != 0)
                {
                    foreach (ComboBoxData currData in hearAboutUs.Items)
                        if (currData.Code == strHearAboutUs)
                        {
                            hearAboutUs.SelectedIndex = hearAboutUs.Items.IndexOf(currData);
                            break;
                        }
                }
                else
                    hearAboutUs.SelectedIndex = 0;
            }
            hearAboutUs = null;

            //Set receive promotional offers
            if (_custToEdit.ReceivePromotionOffers != null)
            {
                string promotionalOffers = _custToEdit.ReceivePromotionOffers;
                if (promotionalOffers.Length != 0)
                {
                    foreach (ComboBoxData currData in comboBoxReceivePromotions.Items)
                        if (currData.Code == promotionalOffers)
                        {
                            comboBoxReceivePromotions.SelectedIndex = comboBoxReceivePromotions.Items.IndexOf(currData);
                            break;
                        }

                }
                else
                    comboBoxReceivePromotions.SelectedIndex = 0;
            }

        }
        /*__________________________________________________________________________________________*/
        private void getContactDetailsFromForm()
        {
            List<ContactVO> customerPhoneNumbers = this.phoneData1.getPhoneData();
            if (this.phoneData1.IsValid)
            {
                _isFormValid = true;
                if (customerPhoneNumbers != null)
                {
                    _numberOfPhoneNumbers = customerPhoneNumbers.Count;
                    //Array inputs
                    _strPrimaryPhone = new string[_numberOfPhoneNumbers];
                    _strPhoneNumber = new string[_numberOfPhoneNumbers];
                    _strAreaCode = new string[_numberOfPhoneNumbers];
                    _strPhoneExtension = new string[_numberOfPhoneNumbers];
                    _strCountryCode = new string[_numberOfPhoneNumbers];
                    _strContactType = new string[_numberOfPhoneNumbers];
                    _strTelecomNumTypeCode = new string[_numberOfPhoneNumbers];
                    int i = 0;
                    foreach (ContactVO phone in customerPhoneNumbers)
                    {
                        _strPrimaryPhone[i] = phone.TeleusrDefText;
                        _strContactType[i] = phone.ContactType;
                        _strAreaCode[i] = phone.ContactAreaCode;
                        _strPhoneNumber[i] = phone.ContactPhoneNumber;
                        _strPhoneExtension[i] = phone.ContactExtension;
                        _strCountryCode[i] = phone.CountryDialNumCode;
                        _strTelecomNumTypeCode[i] = phone.TelecomNumType;
                        i++;
                    }
                }
                else
                {
                    _strPrimaryPhone = new string[1];
                    _strPhoneNumber = new string[1];
                    _strAreaCode = new string[1];
                    _strPhoneExtension = new string[1];
                    _strCountryCode = new string[1];
                    _strContactType = new string[1];
                    _strTelecomNumTypeCode = new string[1];
                    _strPrimaryPhone[0] = "";
                    _strPhoneNumber[0] = "";
                    _strAreaCode[0] = "";
                    _strPhoneExtension[0] = "";
                    _strCountryCode[0] = "";
                    _strContactType[0] = "";
                    _strTelecomNumTypeCode[0] = "";

                }

                //Get Email Address
                string strPrimaryEmail = primaryEmailTextBox.Text;
                string strAlternateEmail = alternateEmailTextBox.Text;
                CustomerEmailVO customerEmail1 = _custToEdit.getPrimaryEmail();
                CustomerEmailVO customerEmail2 = _custToEdit.getAlternateEmail();

                //When both the email address controls is empty on the form
                if (strPrimaryEmail.Equals(string.Empty) && strAlternateEmail.Equals(string.Empty))
                {
                    //The customer did not have any email address previously
                    if (customerEmail1.EmailAddress == null && customerEmail2.EmailAddress == null)
                    {
                        _custEmails = new string[1];
                        _custEmails[0] = "";
                        _custEmailId = new string[1];
                        _custEmailId[0] = "";
                        _custEmailType = new string[1];
                        _custEmailType[0] = "";
                    }
                    //The customer had a primary email before which is being deleted now 
                    else if (customerEmail1.EmailAddress != null)
                    {
                        //If the customer does not have an existing alternate email
                        //Pass only primary to delete
                        if (customerEmail2.EmailAddress == null)
                        {
                            _custEmails = new string[1];
                            _custEmails[0] = "@DELETE@";
                            _custEmailId = new string[1];
                            _custEmailId[0] = customerEmail1.ContactInfoId;
                            _custEmailType = new string[1];
                            _custEmailType[0] = customerEmail1.EmailAddressType;
                        }
                        //If the customer has alternate email then pass both to delete
                        else
                        {
                            _custEmails = new string[2];
                            _custEmails[0] = "@DELETE@";
                            _custEmails[1] = "@DELETE@";
                            _custEmailId = new string[2];
                            _custEmailId[0] = customerEmail1.ContactInfoId;
                            _custEmailId[1] = customerEmail2.ContactInfoId;
                            _custEmailType = new string[2];
                            _custEmailType[0] = customerEmail1.EmailAddressType;
                            _custEmailType[1] = customerEmail2.EmailAddressType;

                        }

                    }
                    //The customer had a secondary email but no primary email before and even the
                    //secondary email is being deleted now
                    else if (customerEmail2.EmailAddress != null)
                    {
                        _custEmails = new string[1];
                        _custEmails[0] = "@DELETE@";
                        _custEmailId = new string[1];
                        _custEmailId[0] = customerEmail2.ContactInfoId;
                        _custEmailType = new string[1];
                        _custEmailType[0] = customerEmail2.EmailAddressType;

                    }
                }
                else //Both the email controls are not empty on the form
                {
                    //If both the email controls has values on the form pass both the values in the arrays
                    if (!strPrimaryEmail.Equals(string.Empty) && !strAlternateEmail.Equals(string.Empty))
                    {
                        _custEmails = new string[2];
                        _custEmails[0] = strPrimaryEmail;
                        _custEmails[1] = strAlternateEmail;
                        _custEmailId = new string[2];
                        if (customerEmail1.ContactInfoId == null)
                            _custEmailId[0] = "";
                        else
                            _custEmailId[0] = customerEmail1.ContactInfoId;
                        if (customerEmail2.ContactInfoId == null)
                            _custEmailId[1] = "";
                        else
                            _custEmailId[1] = customerEmail2.ContactInfoId;
                        _custEmailType = new string[2];
                        if (customerEmail1.EmailAddressType == null)
                            _custEmailType[0] = CustomerEmailTypes.PRIMARY_EMAIL;
                        else
                            _custEmailType[0] = customerEmail1.EmailAddressType;
                        if (customerEmail2.EmailAddressType == null)
                            _custEmailType[1] = CustomerEmailTypes.SECONDARY_EMAIL;
                        else
                            _custEmailType[1] = customerEmail2.EmailAddressType;
                    }
                    //if primary email on the form does not have value and alternate email 
                    //on the form has value
                    else if (strPrimaryEmail.Equals(string.Empty) && !strAlternateEmail.Equals(string.Empty))
                    {
                        //if the customer did not have a primary email before
                        //Only the alternate email values need to be passed to be added
                        if (customerEmail1.EmailAddress == null)
                        {
                            _custEmails = new string[1];
                            _custEmails[0] = strAlternateEmail;
                            _custEmailId = new string[1];
                            if (customerEmail2.ContactInfoId == null)
                                _custEmailId[0] = "";
                            else
                                _custEmailId[0] = customerEmail2.ContactInfoId;
                            _custEmailType = new string[1];
                            _custEmailType[0] = CustomerEmailTypes.PRIMARY_EMAIL;
                        }
                        //if the user had a primary email before then a value of DELETE needs to be passed
                        //along with the contactinfoid of the primary email in order to delete it
                        else
                        {
                            _custEmails = new string[2];
                            _custEmails[0] = "@DELETE@";
                            _custEmails[1] = strAlternateEmail;
                            _custEmailId = new string[2];
                            _custEmailId[0] = customerEmail1.ContactInfoId;
                            if (customerEmail2.ContactInfoId == null)
                                _custEmailId[1] = "";
                            else
                                _custEmailId[1] = customerEmail2.ContactInfoId;
                            _custEmailType = new string[2];
                            _custEmailType[0] = customerEmail1.EmailAddressType;
                            _custEmailType[1] = CustomerEmailTypes.PRIMARY_EMAIL;


                        }

                    }
                    //if primary email on the form has value and alternate email on the form
                    //does not have value 
                    else if (!strPrimaryEmail.Equals(string.Empty) && strAlternateEmail.Equals(string.Empty))
                    {
                        //If the customer did not have alternate email before only the primary email
                        //values need to be passed
                        if (customerEmail2.EmailAddress == null)
                        {
                            _custEmails = new string[1];
                            _custEmails[0] = strPrimaryEmail;
                            _custEmailId = new string[1];
                            if (customerEmail1.ContactInfoId == null)
                                _custEmailId[0] = "";
                            else
                                _custEmailId[0] = customerEmail1.ContactInfoId;
                            _custEmailType = new string[1];
                            if (customerEmail1.EmailAddressType == null)
                                _custEmailType[0] = CustomerEmailTypes.PRIMARY_EMAIL;
                            else
                                _custEmailType[0] = customerEmail1.EmailAddressType;
                        }
                        //if the customer did have alternate email before then the contactinfoid of that
                        //email along with a value of DELETE in the email array needs to be passed
                        //in order to get it deleted from the database
                        else
                        {
                            _custEmails = new string[2];
                            _custEmails[0] = strPrimaryEmail;
                            _custEmails[1] = "@DELETE@";
                            _custEmailId = new string[2];
                            if (customerEmail1.ContactInfoId == null)
                                _custEmailId[0] = "";
                            else
                                _custEmailId[0] = customerEmail1.ContactInfoId;
                            _custEmailId[1] = customerEmail2.ContactInfoId;
                            _custEmailType = new string[2];
                            if (customerEmail1.EmailAddressType == null)
                                _custEmailType[0] = CustomerEmailTypes.PRIMARY_EMAIL;
                            else
                                _custEmailType[0] = customerEmail1.EmailAddressType;
                            _custEmailType[1] = customerEmail2.EmailAddressType;

                        }

                    }
                }
                _noCalls = checkBoxNoCall.Checked ? "Y" : "N";
                _noEmails = checkBoxNoEmail.Checked ? "Y" : "N";
                _noFaxes = checkBoxNoFax.Checked ? "Y" : "N";
                _noMail = checkBoxNoMail.Checked ? "Y" : "N";
                _optOut = checkBoxOptOut.Checked ? "Y" : "";
                _remindPmtDue = checkBoxReminder.Checked ? "Y" : "N";
                _preferContact = this.comboBoxPrefContact.SelectedValue.ToString();
                _preferCallTime = this.comboBoxPrefCallTime.SelectedValue.ToString();
                ComboBox howdidyouHearAbtUs = (ComboBox)this.hearAboutUs1.Controls[0];
                _howDidYouHear = howdidyouHearAbtUs.SelectedValue.ToString();
                _receiveOffers = comboBoxReceivePromotions.SelectedValue.ToString();

            }
            else
            {
                _isFormValid = false;
            }
        }

        #endregion
        #region EVENTS & ACTIONS

        /*__________________________________________________________________________________________*/
        private void checkBoxNoCall_Enter(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(checkBoxNoCall.Bounds.X - 2, checkBoxNoCall.Bounds.Y - 3, checkBoxNoCall.Bounds.Width + 2, checkBoxNoCall.Bounds.Height + 3);
            Commons.CustomPaint(checkBoxNoCall, rect);

        }
        /*__________________________________________________________________________________________*/
        private void checkBoxNoCall_Leave(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(checkBoxNoCall.Bounds.X - 2, checkBoxNoCall.Bounds.Y - 3, checkBoxNoCall.Bounds.Width + 2, checkBoxNoCall.Bounds.Height + 3);
            Commons.RemoveBorder(checkBoxNoCall, rect);
        }
        /*__________________________________________________________________________________________*/
        private void checkBoxNoEmail_Enter(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(checkBoxNoEmail.Bounds.X - 2, checkBoxNoEmail.Bounds.Y - 3, checkBoxNoEmail.Bounds.Width + 2, checkBoxNoEmail.Bounds.Height + 3);
            Commons.CustomPaint(checkBoxNoEmail, rect);
        }
        /*__________________________________________________________________________________________*/
        private void checkBoxNoEmail_Leave(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(checkBoxNoEmail.Bounds.X - 2, checkBoxNoEmail.Bounds.Y - 3, checkBoxNoEmail.Bounds.Width + 2, checkBoxNoEmail.Bounds.Height + 3);
            Commons.RemoveBorder(checkBoxNoEmail, rect);
        }
        /*__________________________________________________________________________________________*/
        private void checkBoxNoFax_Enter(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(checkBoxNoFax.Bounds.X - 2, checkBoxNoFax.Bounds.Y - 3, checkBoxNoFax.Bounds.Width + 2, checkBoxNoFax.Bounds.Height + 3);
            Commons.CustomPaint(checkBoxNoFax, rect);

        }
        /*__________________________________________________________________________________________*/
        private void checkBoxNoFax_Leave(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(checkBoxNoFax.Bounds.X - 2, checkBoxNoFax.Bounds.Y - 3, checkBoxNoFax.Bounds.Width + 2, checkBoxNoFax.Bounds.Height + 3);
            Commons.RemoveBorder(checkBoxNoFax, rect);

        }
        /*__________________________________________________________________________________________*/
        private void checkBoxNoMail_Enter(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(checkBoxNoMail.Bounds.X - 2, checkBoxNoMail.Bounds.Y - 3, checkBoxNoMail.Bounds.Width + 2, checkBoxNoMail.Bounds.Height + 3);
            Commons.CustomPaint(checkBoxNoMail, rect);
        }
        /*__________________________________________________________________________________________*/
        private void checkBoxNoMail_Leave(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(checkBoxNoMail.Bounds.X - 2, checkBoxNoMail.Bounds.Y - 3, checkBoxNoMail.Bounds.Width + 2, checkBoxNoMail.Bounds.Height + 3);
            Commons.RemoveBorder(checkBoxNoMail, rect);
        }
        /*__________________________________________________________________________________________*/
        private void checkBoxOptOut_Enter(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(checkBoxOptOut.Bounds.X - 2, checkBoxOptOut.Bounds.Y - 3, checkBoxOptOut.Bounds.Width + 2, checkBoxOptOut.Bounds.Height + 3);
            Commons.CustomPaint(checkBoxOptOut, rect);

        }
        /*__________________________________________________________________________________________*/
        private void checkBoxOptOut_Leave(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(checkBoxOptOut.Bounds.X - 2, checkBoxOptOut.Bounds.Y - 3, checkBoxOptOut.Bounds.Width + 2, checkBoxOptOut.Bounds.Height + 3);
            Commons.RemoveBorder(checkBoxOptOut, rect);
        }
        /*__________________________________________________________________________________________*/
        private void checkBoxReminder_Enter(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(checkBoxReminder.Bounds.X - 2, checkBoxReminder.Bounds.Y - 3, checkBoxReminder.Bounds.Width + 2, checkBoxReminder.Bounds.Height + 3);
            Commons.CustomPaint(checkBoxReminder, rect);
        }
        /*__________________________________________________________________________________________*/
        private void checkBoxReminder_Leave(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(checkBoxReminder.Bounds.X - 2, checkBoxReminder.Bounds.Y - 3, checkBoxReminder.Bounds.Width + 2, checkBoxReminder.Bounds.Height + 3);
            Commons.RemoveBorder(checkBoxReminder, rect);
        }
        /*__________________________________________________________________________________________*/
        private void comboBoxPrefContact_Enter(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(comboBoxPrefContact.Bounds.X - 2, comboBoxPrefContact.Bounds.Y - 3, comboBoxPrefContact.Bounds.Width + 2, comboBoxPrefContact.Bounds.Height + 3);
            Commons.CustomPaint(comboBoxPrefContact, rect);
        }
        /*__________________________________________________________________________________________*/
        private void comboBoxPrefContact_Leave(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(comboBoxPrefContact.Bounds.X - 2, comboBoxPrefContact.Bounds.Y - 3, comboBoxPrefContact.Bounds.Width + 2, comboBoxPrefContact.Bounds.Height + 3);
            Commons.RemoveBorder(comboBoxPrefContact, rect);
        }
        /*__________________________________________________________________________________________*/
        private void comboBoxPrefCallTime_Enter(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(comboBoxPrefCallTime.Bounds.X - 2, comboBoxPrefCallTime.Bounds.Y - 3, comboBoxPrefCallTime.Bounds.Width + 2, comboBoxPrefCallTime.Bounds.Height + 3);
            Commons.CustomPaint(comboBoxPrefCallTime, rect);
        }
        /*__________________________________________________________________________________________*/
        private void comboBoxPrefCallTime_Leave(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(comboBoxPrefCallTime.Bounds.X - 2, comboBoxPrefCallTime.Bounds.Y - 3, comboBoxPrefCallTime.Bounds.Width + 2, comboBoxPrefCallTime.Bounds.Height + 3);
            Commons.RemoveBorder(comboBoxPrefCallTime, rect);
        }
        /*__________________________________________________________________________________________*/
        private void comboBoxReceivePromotions_Enter(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(comboBoxReceivePromotions.Bounds.X - 2, comboBoxReceivePromotions.Bounds.Y - 3, comboBoxReceivePromotions.Bounds.Width + 2, comboBoxReceivePromotions.Bounds.Height + 3);
            Commons.CustomPaint(comboBoxReceivePromotions, rect);
        }
        /*__________________________________________________________________________________________*/
        private void comboBoxReceivePromotions_Leave(object sender, EventArgs e)
        {
            Rectangle rect = new Rectangle(comboBoxReceivePromotions.Bounds.X - 2, comboBoxReceivePromotions.Bounds.Y - 3, comboBoxReceivePromotions.Bounds.Width + 2, comboBoxReceivePromotions.Bounds.Height + 3);
            Commons.RemoveBorder(comboBoxReceivePromotions, rect);
        }

        #region OBSOLETE BUTTONS
        /*__________________________________________________________________________________________*/
        private void buttonReset_Click(object sender, EventArgs e)
        {
            LoadCustomerContactData();
        }
        /*__________________________________________________________________________________________*/
        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            getContactDetailsFromForm();
            if (_isFormValid)
            {
                if (contactInfoChanged())
                {
                    DialogResult dgr;
                    do
                    {
                        _procMsg = new ProcessingMessage("Updating Customer Contact Details");
                        SetButtonState(false);
                        BackgroundWorker bw = new BackgroundWorker();
                        bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                        bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                        bw.RunWorkerAsync();
                        _procMsg.ShowDialog(this);

                        if (!_updatePhoneData)
                        {
                            dgr = MessageBox.Show(Commons.GetMessageString("CustContactInfoUpdateFailure"), "Error", MessageBoxButtons.RetryCancel);
                        }
                        else
                            break;

                    } while (dgr == DialogResult.Retry);
                }
                else
                {
                    MessageBox.Show(Commons.GetMessageString("NoChangesInForm"));
                }
                this.Close();
                this.Dispose(true);
            }
            else
                return;

        }
        #endregion
        /*__________________________________________________________________________________________*/
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "ViewCustomerInformationReadOnly";
            this.NavControlBox.Action = NavBox.NavAction.BACK;
            //this.Close();
            //this.Dispose(true);
        }
        #endregion

    }
}
