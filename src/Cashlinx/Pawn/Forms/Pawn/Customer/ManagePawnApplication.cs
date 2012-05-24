/********************************************************************
* CashlinxDesktop
* ManagePawnApplication
* This user control is shown when a user is selected in the 
* lookup customer search results screen or to Add a new customer
* Sreelatha Rengarajan 3/23/2009 Initial version
 * Sreelatha Rengarajan 6/18/09  Changed the required fields of the form
 *                               to be determined by business rules
 *SR 6/1/2010 Added logic to change the back color of the required fields to yellow
 *if they were not entered on submit
 *Madhu 12/10/2010 Fix for bugzilla defect 10 
 *SR 02/18/2011 Add logic to accept FN,LN, ID or DOB or Phone for sale flow over $10 
 *or store credit flow
 *SR 2/24/2011 Adding logic to show phone number as required
 * Madhu 3/24/2011 added code to view customer comments BZ # 387
 * Madhu 4/5/2011 Commented logic to take off the customer comments blink.
 * Madhu 5/2/2011 BZ # 619 The Manage Override logic moved to submit button click.
 * EDW   12/27/2011 BZ# 1383 + Refactoring Changes
******************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Pawn.Loan;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.UserControls;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Customer
{
    public partial class ManagePawnApplication : Form
    {
        private readonly ComboBox custIdIssuer;
        private readonly ComboBox custIdCountry;
        private readonly ComboBox idType;
        private bool nameCheck = false;
        private Form ownerFrm;

        //Local Variables

        private ComboBox identType = new ComboBox();
        private ComboBox identIssuer = new ComboBox();
        private ComboBox title = new ComboBox();
        private ComboBox titsuffix = new ComboBox();
        private ComboBox addrstate = new ComboBox();
        private ComboBox custEyeColor = new ComboBox();
        private ComboBox custHairColor = new ComboBox();
        private ComboBox custRace = new ComboBox();
        private ComboBox custGender = new ComboBox();

        private string strIdentTypeCode = "";
        private string strIdentIssuer = "";
        private string strIdentIssuerName = "";
        private string strIdentNumber = "";
        private string strIdentExpirydate = "";
        private string strIdentId = "";

        private string[] IdentTypeCode = new string[1];
        private string[] IdentIssuer = new string[1];
        private string[] IdentIssuerName = new string[1];
        private string[] IdentNumber = new string[1];
        private string[] IdentExpirydate = new string[1];
        private string[] IdIssuedDate = new string[1];

        private string strFirstName = "";
        private string strLastName = "";
        private string strDOB = "";
        private string strMiddleName = "";
        private string strAddr1 = "";
        private string strAddr2 = "";
        private string strCustAddress = "";
        private string strZipcode = "";
        private string strCity = "";
        private string strCustAddrState = "";
        private string strCustUnit = "";
        private string strTitle = "";
        private string strTitleSuffix = "";
        private string strUserId = "";
        private string strStoreNumber = "";
        private string selectedIDType = "";
        private int customerAge = 0;


        //clothing is not used for now but the database needs it
        //to add the loan application
        private string strClothing = "";
        private string strEyeColor = "";
        private string strHairColor = "";
        private string strHeight = "";
        private string strWeight = "";
        private string strRace = "";
        private string strGender = "";
        private string[] strPrimaryPhone;
        private string[] strContactType;
        private string[] strPhoneNumber;
        private string[] strAreaCode;
        private string[] strCountryCode;
        private string[] strPhoneExtension;
        private string[] strTelecomNumTypeCode;
        private string strNotes = "";
        private string strSSN = "";
        private string errorCode = "";
        private string errorMsg = "";
        private string custNumber = "";
        private string partyID = "";
        private string strIdIssuedDate = "";
        private string strPawnAppId = "";
        private int numberOfPhoneNumbers = 0;
        private bool idCheck = false;
        private bool _ssnDuplicate = false;
        //Boolean values to denote when required fields in controls are entered 
        private bool reqFieldsEntered = false;
        private bool isFormValid = false;
        private bool idNumberError = false;
        private bool retValue = false;
        private bool stateIDSelected = false;
        private DataTable custDatatable = new DataTable();
        private Control ctrl = new Control();
        private List<string> managePawnRequiredFields = new List<string>();
        private List<string> managePawnRemoveReqdFields = new List<string>();
        private CustomerVO FormCustomer = new CustomerVO();
        private string strStoreState = "";
        private IdentificationVO firstIdentity;
        private bool updateRequiredFields;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NavBox NavControlBox;// { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CustomerVO Customer
        {
            get;
            set;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool NameCheck
        {
            get;
            set;
        }

        public ManagePawnApplication()
        {
            if (DesignMode)
            {
                return;
            }

            InitializeComponent();
            this.NavControlBox = new NavBox();
            Customer = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
            strStoreState = GlobalDataAccessor.Instance.CurrentSiteId.State;
            FormCustomer = GlobalDataAccessor.Instance.DesktopSession.MPCustomer ?? Utilities.CloneObject<CustomerVO>(Customer);

            //get the store number from desktop session
            strStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            strUserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
            //Set error messages in the user controls that require it
            this.pwnapp_identificationexpirationdate.ErrorMessage = Commons.GetMessageString("InvalidDate");
            this.pwnapp_dateofbirth.ErrorMessage = Commons.GetMessageString("InvalidDateOfBirth");
            this.pwnapp_height.ErrorMessage = Commons.GetMessageString("InvalidHeightFeet");
            this.pwnapp_heightinches.ErrorMessage = Commons.GetMessageString("InvalidHeightInches");
            this.pwnapp_weight.ErrorMessage = Commons.GetMessageString("InvalidWeight");
            this.pwnapp_address.ErrorMessage = "Please enter valid characters for address line";
            this.pwnapp_address2.ErrorMessage = "Please enter valid characters for address2 line";
            this.pwnapp_city.ErrorMessage = "Please enter valid characters for city";

            //set the ID Type combobox
            idType = (ComboBox)this.pwnapp_identificationtype.Controls[0];
            idType.SelectedIndexChanged += new EventHandler(this.idType1_SelectedIndexChanged);
            //set the country/state controls and its event handlers
            //set the Id Issuer state combobox
            custIdIssuer = (ComboBox)this.pwnapp_identificationstate.Controls[0];
            custIdIssuer.SelectedIndexChanged += new EventHandler(custIdIssuer_SelectedIndexChanged);
            //Set the Id Issuer country combobox
            custIdCountry = (ComboBox)this.pwnapp_identificationcountry.Controls[0];
            custIdCountry.SelectedIndexChanged += new EventHandler(custIdCountry_SelectedIndexChanged);

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void ManagePawnApplication_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.NavControlBox.Owner = this;
                this.NameCheck = GlobalDataAccessor.Instance.DesktopSession.MPNameCheck;
                //set the owner form
                ownerFrm = this.Owner;

                //Load all the customer object data into the respective controls
                managePawnRequiredFields = CustomerProcedures.getPawnRequiredFields(CashlinxDesktopSession.Instance);
                //If the session variable updaterequiredfieldsforcustomer is set to true set the local variable
                if (GlobalDataAccessor.Instance.DesktopSession.UpdateRequiredFieldsForCustomer)
                {
                    updateRequiredFields = true;
                }

                LoadDataInControlsFromObject();
                SetRequiredFieldsInForm();

                //If the trigger is View pawn customer product details
                //Title, first name, middle name, last name, title suffix
                //will be read only
                if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VIEWPAWNCUSTPRODDETAILS, StringComparison.OrdinalIgnoreCase) ||
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.RETURNCUSTOMERBUY, StringComparison.OrdinalIgnoreCase) ||
                    (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.POLICEHOLDRELEASE) && !GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName.Equals("itemrelease") && this.NameCheck))
                {
                    this.setNameReadableState(false);
                }

                //Hide all the panels and controls which are not required for a new customer
                //If it is a new customer but is coming back to this page after being shown
                //the existing customer screen because of a name check show all the panels
                if (Customer != null &&
                    Customer.NewCustomer &&
                    !(NameCheck) &&
                    !GlobalDataAccessor.Instance.DesktopSession.CashSaleCustomer &&
                    GlobalDataAccessor.Instance.DesktopSession.ActiveLayaway == null)
                {
                    this.physicalDescriptionPanel.Visible = false;
                    this.phoneNumPanel.Visible = false;
                    this.panelNotes.Visible = false;
                    this.notesPanel.Visible = false;
                    this.notesRichTextBox.Visible = false;
                    this.tableLayoutPanel3.Visible = false;
                    this.tableLayoutPanel4.Visible = false;
                    this.phoneData1.Visible = false;
                    this.tableLayoutPanel6.Visible = false;

                }
                else
                {
                    if (Customer != null)
                    {
                        if (!Customer.NewCustomer)
                        {
                            this.customButtonReset.Visible = true;
                            //Madhu 12/20/2010 fix for bugzilla defect 20
                            if (Customer.getLatestNote().ContactNote != null)
                            {
                                //BZ # 508
                                /*Timer custCommnetsTm = new Timer();
                                custCommnetsTm.Interval = 300;
                                custCommnetsTm.Tick += new EventHandler(flash_CustCommnet);
                                custCommnetsTm.Enabled = true;
                                 */
                                custCommentAlert.Visible = true;
                                //BZ # 508 end

                                //customer comments view BZ # 387
                                this.custCommentAlert.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelCommentsView_LinkClicked);
                                this.custCommentAlert.LinkColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                custCommentAlert.Visible = false;
                            }
                        }

                        //If this is an existing customer and
                        //we are coming back from a name check from the existing customer
                        //screen continue with save
                        if (!Customer.NewCustomer && NameCheck)
                        {
                            getPersonalIdentificationDataFromForm();
                            getPersonalInfoDataFromForm();
                            getPhoneNumberDataFromForm();
                            getPhysicalDescriptionDataFromForm();
                            getNotesDataFromForm();
                            if (strAddr2 != "")
                            {
                                strCustAddress = strAddr1 + "," + strAddr2;
                            }
                            else
                            {
                                strCustAddress = strAddr1;
                            }

                            custNumber = Customer.CustomerNumber;
                            if (performUpdates())
                            {
                                NavBox.NavAction action = NavBox.NavAction.NONE;
                                this.NavigateUserBasedOnTrigger(ref action);
                                NavControlBox.Action = action;
                            }
                        }
                        else
                        {
                            this.physicalDescriptionPanel.Visible = true;
                            this.phoneNumPanel.Visible = true;
                            this.panelNotes.Visible = true;
                            this.notesPanel.Visible = true;
                            this.notesRichTextBox.Visible = true;
                            this.tableLayoutPanel3.Visible = true;
                            this.tableLayoutPanel4.Visible = true;
                            this.phoneData1.Visible = true;
                            this.tableLayoutPanel6.Visible = true;
                            this.tableLayoutPanel1.Controls[0].Focus();
                        }
                    }
                }
                this.ActiveControl = this.tableLayoutPanel1.Controls["pwnapp_firstname"];

                if (GlobalDataAccessor.Instance.DesktopSession.CashSaleCustomer)
                {
                    if (this.Customer != null && Customer.NewCustomer)
                    {
                        ((ComboBox)this.pwnapp_state.Controls[0]).SelectedIndex = -1;
                    }
                }

            }
        }

        private void SetRequiredFieldsInForm()
        {
            if (updateRequiredFields)
            {
                getPersonalIdentificationDataFromForm();
                getPersonalInfoDataFromForm();
                bool phoneDataEntered = phoneData1.IsPhoneEntered();

                if (!string.IsNullOrEmpty(strFirstName) && !string.IsNullOrEmpty(strLastName))
                {
                    if (phoneDataEntered)
                    {
                        managePawnRequiredFields.Remove("PWNAPP_IDENTIFICATIONTYPE");
                        managePawnRequiredFields.Remove("PWNAPP_IDENTIFICATIONSTATE");
                        managePawnRequiredFields.Remove("PWNAPP_IDENTIFICATIONCOUNTRY");
                        managePawnRequiredFields.Remove("PWNAPP_IDENTIFICATIONNUMBER");
                        managePawnRequiredFields.Remove("PWNAPP_IDENTIFICATIONEXPIRATIONDATE");
                        managePawnRequiredFields.Remove("PWNAPP_DATEOFBIRTH");
                        managePawnRemoveReqdFields.Add("PWNAPP_IDENTIFICATIONTYPE");
                        managePawnRemoveReqdFields.Add("PWNAPP_IDENTIFICATIONSTATE");
                        managePawnRemoveReqdFields.Add("PWNAPP_IDENTIFICATIONCOUNTRY");
                        managePawnRemoveReqdFields.Add("PWNAPP_IDENTIFICATIONNUMBER");
                        managePawnRemoveReqdFields.Add("PWNAPP_IDENTIFICATIONEXPIRATIONDATE");
                        managePawnRemoveReqdFields.Add("PWNAPP_DATEOFBIRTH");
                    }
                    else if (!string.IsNullOrEmpty(strIdentTypeCode) &&
                        !string.IsNullOrEmpty(strIdentIssuerName) &&
                        !string.IsNullOrEmpty(strIdentNumber))
                    {
                        managePawnRequiredFields.Remove("PWNAPP_DATEOFBIRTH");
                        managePawnRequiredFields.Remove("PWNAPP_PHONENUMBER");

                        managePawnRemoveReqdFields.Add("PWNAPP_DATEOFBIRTH");
                        managePawnRemoveReqdFields.Add("PWNAPP_PHONENUMBER");
                    }
                    else if (!string.IsNullOrEmpty(strDOB) &&
                        !strDOB.Equals("mm/dd/yyyy", StringComparison.OrdinalIgnoreCase))
                    {
                        managePawnRequiredFields.Remove("PWNAPP_IDENTIFICATIONTYPE");
                        managePawnRequiredFields.Remove("PWNAPP_IDENTIFICATIONSTATE");
                        managePawnRequiredFields.Remove("PWNAPP_IDENTIFICATIONCOUNTRY");
                        managePawnRequiredFields.Remove("PWNAPP_IDENTIFICATIONNUMBER");
                        managePawnRequiredFields.Remove("PWNAPP_IDENTIFICATIONEXPIRATIONDATE");
                        managePawnRequiredFields.Remove("PWNAPP_PHONENUMBER");

                        managePawnRemoveReqdFields.Add("PWNAPP_IDENTIFICATIONTYPE");
                        managePawnRemoveReqdFields.Add("PWNAPP_IDENTIFICATIONSTATE");
                        managePawnRemoveReqdFields.Add("PWNAPP_IDENTIFICATIONCOUNTRY");
                        managePawnRemoveReqdFields.Add("PWNAPP_IDENTIFICATIONNUMBER");
                        managePawnRemoveReqdFields.Add("PWNAPP_IDENTIFICATIONEXPIRATIONDATE");
                        managePawnRemoveReqdFields.Add("PWNAPP_PHONENUMBER");
                    }
                }
            }

            foreach (string reqdField in managePawnRequiredFields)
            {

                string labelName = (reqdField + "_label").ToLower();
                string fieldName = reqdField.ToLower();
                Control[] labelCtrl = this.Controls.Find(labelName, true);
                if (labelCtrl.Length > 0)
                {
                    CustomLabel customReqdLabel = (CustomLabel)labelCtrl[0];
                    customReqdLabel.Required = labelCtrl.Length > 0;
                    Control[] fieldCtrl = this.Controls.Find(fieldName, true);
                    if (fieldCtrl.Length > 0)
                    {
                        if (fieldCtrl[0].GetType() == typeof(CustomTextBox))
                            ((CustomTextBox)(fieldCtrl[0])).Required = true;
                        if (fieldCtrl[0].GetType() == typeof(IDType))
                            ((IDType)(fieldCtrl[0])).Required = true;
                        if (fieldCtrl[0].GetType() == typeof(State))
                            ((State)(fieldCtrl[0])).Required = true;
                        if (fieldCtrl[0].GetType() == typeof(Country))
                            ((Country)(fieldCtrl[0])).Required = true;
                        if (fieldCtrl[0].GetType() == typeof(Zipcode))
                            ((Zipcode)(fieldCtrl[0])).Required = true;
                        if (fieldCtrl[0].GetType() == typeof(Date))
                            ((Date)(fieldCtrl[0])).Required = true;
                        if (fieldCtrl[0].GetType() == typeof(Race))
                            ((Race)(fieldCtrl[0])).Required = true;
                        if (fieldCtrl[0].GetType() == typeof(Gender))
                            ((Gender)(fieldCtrl[0])).Required = true;
                        if (fieldCtrl[0].GetType() == typeof(Haircolor))
                            ((Haircolor)(fieldCtrl[0])).Required = true;
                        if (fieldCtrl[0].GetType() == typeof(EyeColor))
                            ((EyeColor)(fieldCtrl[0])).Required = true;
                    }
                }

                //Separate processing for phone number since its a user control
                if (reqdField == "PWNAPP_PHONENUMBER")
                {
                    phoneData1.SetPhoneRequired(true);
                }
            }

            foreach (string notReqdField in managePawnRemoveReqdFields)
            {
                string labelName = (notReqdField + "_label").ToLower();
                string fieldName = notReqdField.ToLower();
                Control[] labelCtrl = this.Controls.Find(labelName, true);
                if (labelCtrl.Length > 0)
                {
                    CustomLabel customReqdLabel = (CustomLabel)labelCtrl[0];
                    customReqdLabel.Required = false;
                    Control[] fieldCtrl = this.Controls.Find(fieldName, true);
                    if (fieldCtrl.Length > 0)
                    {
                        if (fieldCtrl[0].GetType() == typeof(CustomTextBox))
                            ((CustomTextBox)(fieldCtrl[0])).Required = false;
                        if (fieldCtrl[0].GetType() == typeof(IDType))
                            ((IDType)(fieldCtrl[0])).Required = false;
                        if (fieldCtrl[0].GetType() == typeof(State))
                            ((State)(fieldCtrl[0])).Required = false;
                        if (fieldCtrl[0].GetType() == typeof(Country))
                            ((Country)(fieldCtrl[0])).Required = false;
                        if (fieldCtrl[0].GetType() == typeof(Zipcode))
                            ((Zipcode)(fieldCtrl[0])).Required = false;
                        if (fieldCtrl[0].GetType() == typeof(Date))
                            ((Date)(fieldCtrl[0])).Required = false;
                        if (fieldCtrl[0].GetType() == typeof(Race))
                            ((Race)(fieldCtrl[0])).Required = false;
                        if (fieldCtrl[0].GetType() == typeof(Gender))
                            ((Gender)(fieldCtrl[0])).Required = false;
                        if (fieldCtrl[0].GetType() == typeof(Haircolor))
                            ((Haircolor)(fieldCtrl[0])).Required = false;
                        if (fieldCtrl[0].GetType() == typeof(EyeColor))
                            ((EyeColor)(fieldCtrl[0])).Required = false;
                    }
                }

                //Separate processing for phone number since its a user control
                if (notReqdField == "PWNAPP_PHONENUMBER")
                {
                    phoneData1.SetPhoneRequired(false);
                }
            }
        }

        /// <summary>
        /// Method to load values in the form to what is retrieved
        /// from the database and stored in the customer object
        /// </summary>
        private void LoadDataInControlsFromObject()
        {
            if (FormCustomer != null)
            {
                if (!FormCustomer.NewCustomer)
                {
                    custNumber = FormCustomer.CustomerNumber;
                }

                this.pwnapp_firstname.Text = FormCustomer.FirstName;
                this.pwnapp_lastname.Text = FormCustomer.LastName;
                this.pwnapp_middleinitial.Text = FormCustomer.MiddleInitial;
                if (FormCustomer.DateOfBirth.Date == DateTime.MaxValue.Date)
                {
                    this.pwnapp_dateofbirth.Controls[0].Text = "";
                }
                else
                {
                    this.pwnapp_dateofbirth.Controls[0].Text = (FormCustomer.DateOfBirth).FormatDate();
                    checkDateOfBirth();
                }

                //Fill in address data
                AddressVO custAddr = FormCustomer.getHomeAddress();
                if (custAddr != null)
                {
                    string customerAddress = custAddr.Address1;
                    if (customerAddress.Length > 40)
                    {
                        this.pwnapp_address.Text = customerAddress.Substring(0, 40);
                        this.pwnapp_address2.Text = customerAddress.Substring(41, customerAddress.Length - 41);
                    }
                    else
                    {
                        this.pwnapp_address.Text = customerAddress;
                        this.pwnapp_address2.Text = custAddr.Address2;
                    }

                    this.pwnapp_city.Text = custAddr.City;
                    this.pwnapp_zip.Controls[0].Text = custAddr.ZipCode;
                    this.pwnapp_unit.Text = custAddr.UnitNum;
                    ComboBox custstate = (ComboBox)this.pwnapp_state.Controls[0];
                    foreach (USState currstate in custstate.Items)
                    {
                        if (currstate.ShortName == custAddr.State_Code)
                        {
                            custstate.SelectedIndex = custstate.Items.IndexOf(currstate);
                            break;
                        }
                    }
                    custstate = null;
                }
                else
                {
                    this.pwnapp_address.Text = "";
                    this.pwnapp_address2.Text = "";
                    this.pwnapp_city.Text = "";
                    this.pwnapp_zip.Controls[0].Text = "";
                    this.pwnapp_unit.Text = "";
                }

                //Parse the feet and inches data from the height data
                if (FormCustomer.Height.Trim().Length != 0)
                {
                    string strHt = FormCustomer.Height.ToString();
                    int feetIndexinHeight = strHt.IndexOf('\'');
                    int inchesIndexinHeight = strHt.IndexOf('\"');
                    if (feetIndexinHeight > 0)
                    {
                        this.pwnapp_height.Text = strHt.Substring(0, feetIndexinHeight);
                    }
                    else
                    {
                        this.pwnapp_height.Text = "";
                    }

                    if (inchesIndexinHeight > 0)
                    {
                        this.pwnapp_heightinches.Text = strHt.Substring(feetIndexinHeight + 1, (inchesIndexinHeight - feetIndexinHeight) - 1);
                    }
                    else
                    {
                        this.pwnapp_heightinches.Text = "";
                    }
                }
                else
                {
                    this.pwnapp_height.Text = "";
                    this.pwnapp_heightinches.Text = "";
                }

                if (FormCustomer.Weight != 0)
                {
                    this.pwnapp_weight.Text = FormCustomer.Weight.ToString();
                }
                else
                {
                    this.pwnapp_weight.Text = "";
                }

                //For an existing customer, SSN is shown but an uneditable field
                //For a new customer, it is an editable field

                if (!FormCustomer.NewCustomer)
                {
                    //Madhu BZ # 116
                    this.pwnapp_socialsecuritynumber.Controls[0].Text = Commons.FormatSSN(FormCustomer.SocialSecurityNumber);
                    this.pwnapp_socialsecuritynumber.Enabled = false;
                }
                else //Madhu BZ # 116
                {
                    this.pwnapp_socialsecuritynumber.Controls[0].Text = FormCustomer.SocialSecurityNumber;
                }

                //populate all phone numbers of the customer
                this.phoneData1.populatePhoneNumber(FormCustomer.CustomerContacts);

                //The following code sets the selected item in the combo boxes
                //for gender, race, title, title suffix, state, eye color, hair color, ID type and ID Issuer
                //to the retrieved value from the database for the existing customer

                custGender = (ComboBox)this.pwnapp_sex.Controls[0];
                foreach (ComboBoxData currgender in custGender.Items)
                {
                    if (currgender.Code == FormCustomer.Gender)
                    {
                        custGender.SelectedIndex = custGender.Items.IndexOf(currgender);
                        break;
                    }
                }

                custGender = null;
                ComboBox custTitle = (ComboBox)this.pwnapp_title.Controls[0];
                if (Customer.CustTitle.Length != 0)
                {
                    foreach (ComboBoxData currTitle in custTitle.Items)
                    {
                        if (currTitle.Code == FormCustomer.CustTitle)
                        {
                            custTitle.SelectedIndex = custTitle.Items.IndexOf(currTitle);
                            break;
                        }
                    }
                }
                else
                {
                    custTitle.SelectedIndex = 0;
                }

                custTitle = null;
                ComboBox custTitSuffix = (ComboBox)this.pwnapp_titlesuffix.Controls[0];
                if (Customer.CustTitleSuffix.Length != 0)
                {
                    foreach (ComboBoxData currTitleSuffix in custTitSuffix.Items)
                    {
                        if (currTitleSuffix.Code == FormCustomer.CustTitleSuffix)
                        {
                            custTitSuffix.SelectedIndex = custTitSuffix.Items.IndexOf(currTitleSuffix);
                            break;
                        }
                    }
                }
                else
                {
                    custTitSuffix.SelectedIndex = 0;
                }

                custTitSuffix = null;

                ComboBox custrace = (ComboBox)this.pwnapp_race.Controls[0];
                foreach (ComboBoxData currrace in custrace.Items)
                {
                    if (currrace.Code == FormCustomer.Race)
                    {
                        custrace.SelectedIndex = custrace.Items.IndexOf(currrace);
                        break;
                    }
                }

                custrace = null;
                ComboBox custEyeColor = (ComboBox)this.pwnapp_eyes.Controls[0];
                foreach (ComboBoxData curreyecolor in custEyeColor.Items)
                {
                    if (curreyecolor.Code == FormCustomer.EyeColor)
                    {
                        custEyeColor.SelectedIndex = custEyeColor.Items.IndexOf(curreyecolor);
                        break;
                    }
                }

                custEyeColor = null;
                ComboBox custHairColor = (ComboBox)this.pwnapp_hair.Controls[0];
                foreach (ComboBoxData currhaircolor in custHairColor.Items)
                {
                    if (currhaircolor.Code == FormCustomer.HairColor)
                    {
                        custHairColor.SelectedIndex = custHairColor.Items.IndexOf(currhaircolor);
                        break;
                    }
                }

                custHairColor = null;

                pwnapp_identificationexpirationdate.Enabled = false;
                pwnapp_identificationnumber.Enabled = false;

                //Populate the id details if the first identity cursor is not empty

                notesRichTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateFlag"></param>
        public void setNameReadableState(bool stateFlag)
        {
            this.pwnapp_firstname.Enabled = stateFlag;
            this.pwnapp_lastname.Enabled = stateFlag;
            this.pwnapp_title.Enabled = stateFlag;
            this.pwnapp_titlesuffix.Enabled = stateFlag;
            this.pwnapp_middleinitial.Enabled = stateFlag;
        }

        /// <summary>
        /// Method to load the Issuer agency combo box with
        /// appropriate values based on the Issue type selected
        /// If nothing is selected in IdType, only the disabled combobox will be shown 
        /// If the ID type is state issued Id or concealed weapons permit, US states will be shown in Issuer combobox
        /// If the ID type is anything else, a list of countries will be shown in the Issuer combobox
        /// If it is an existing customer, the selected state or country for the ID that exists for
        /// the customer is highlighted 
        /// </summary>
        private void populateIssuerType()
        {
            if (idType == null)
            {
                return;
            }

            //Madhu BZ # 116
            if (identIssuer != null)
            {
                identIssuer.Enabled = true;
            }
            //end

            selectedIDType = idType.SelectedIndex > 0 ? idType.SelectedValue.ToString() : "";
            switch (selectedIDType)
            {
                case "":
                    this.tableLayoutPanel2.Controls.Remove(pwnapp_identificationstate);
                    this.tableLayoutPanel2.Controls.Add(pwnapp_identificationcountry, 1, 1);
                    //this.tableLayoutPanel2.ColumnStyles[1].Width = 140;
                    this.pwnapp_identificationcountry.Enabled = false;
                    pwnapp_identificationstate.Visible = false;
                    this.pwnapp_identificationcountry.SetBounds(193, 16, pwnapp_identificationcountry.Width, pwnapp_identificationcountry.Height);
                    break;
                case StateIdTypes.STATE_IDENTIFICATION_ID:
                    this.tableLayoutPanel2.Controls.Remove(pwnapp_identificationcountry);
                    this.tableLayoutPanel2.Controls.Add(pwnapp_identificationstate, 1, 1);
                    pwnapp_identificationstate.TabIndex = this.pwnapp_identificationtype.TabIndex + 1;
                    pwnapp_identificationstate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
                    this.pwnapp_identificationstate.SetBounds(193, 16, pwnapp_identificationstate.Width, pwnapp_identificationstate.Height);
                    pwnapp_identificationstate.Visible = true;
                    //this.tableLayoutPanel2.ColumnStyles[1].Width = 51;
                    break;
                case StateIdTypes.DRIVERLICENSE:
                    this.tableLayoutPanel2.Controls.Remove(pwnapp_identificationcountry);
                    this.tableLayoutPanel2.Controls.Add(pwnapp_identificationstate, 1, 1);
                    pwnapp_identificationstate.TabIndex = this.pwnapp_identificationtype.TabIndex + 1;
                    pwnapp_identificationstate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
                    this.pwnapp_identificationstate.SetBounds(193, 16, pwnapp_identificationstate.Width, pwnapp_identificationstate.Height);
                    pwnapp_identificationstate.Visible = true;
                    //this.tableLayoutPanel2.ColumnStyles[1].Width = 51;
                    break;
                case StateIdTypes.CONCEALED_WEAPONS_PERMIT: ////Madhu BZ # 116
                    this.tableLayoutPanel2.Controls.Remove(pwnapp_identificationcountry);
                    this.tableLayoutPanel2.Controls.Add(pwnapp_identificationstate, 1, 1);
                    pwnapp_identificationstate.TabIndex = this.pwnapp_identificationtype.TabIndex + 1;
                    pwnapp_identificationstate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
                    this.pwnapp_identificationstate.SetBounds(193, 16, pwnapp_identificationstate.Width, pwnapp_identificationstate.Height);
                    pwnapp_identificationstate.Visible = true;
                    //this.tableLayoutPanel2.ColumnStyles[1].Width = 51;
                    break; //end
                case StateIdTypes.MEXICAN_CONSULATE:
                    this.tableLayoutPanel2.Controls.Remove(pwnapp_identificationstate);

                    identIssuer = (ComboBox)this.pwnapp_identificationcountry.Controls[0];
                    identIssuer.SelectedIndex = identIssuer.FindString("Mexico");
                    populateIdNumberAndDate();
                    this.pwnapp_identificationcountry.isValid = true;
                    identIssuer.Enabled = false;

                    this.tableLayoutPanel2.Controls.Add(pwnapp_identificationcountry, 1, 1);
                    pwnapp_identificationcountry.TabIndex = this.pwnapp_identificationtype.TabIndex + 1;
                    pwnapp_identificationcountry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
                    //this.tableLayoutPanel2.ColumnStyles[1].Width = 140;
                    this.pwnapp_identificationcountry.Enabled = true;
                    pwnapp_identificationstate.Visible = false;
                    this.pwnapp_identificationcountry.SetBounds(193, 16, pwnapp_identificationcountry.Width, pwnapp_identificationcountry.Height);
                    break;
                default:
                    this.tableLayoutPanel2.Controls.Remove(pwnapp_identificationstate);
                    this.tableLayoutPanel2.Controls.Add(pwnapp_identificationcountry, 1, 1);
                    pwnapp_identificationcountry.TabIndex = this.pwnapp_identificationtype.TabIndex + 1;
                    pwnapp_identificationcountry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
                    //this.tableLayoutPanel2.ColumnStyles[1].Width = 140;
                    this.pwnapp_identificationcountry.Enabled = true;
                    pwnapp_identificationstate.Visible = false;
                    this.pwnapp_identificationcountry.SetBounds(193, 16, pwnapp_identificationcountry.Width, pwnapp_identificationcountry.Height);
                    break;
            }

            if (selectedIDType == IdNumberTypes.PASSPORT || selectedIDType == IdNumberTypes.CONCEALED_WEAPONS_PERMIT)
            {
                //SR 03/21/2011
                //Set identification expiration date as required
                if (managePawnRequiredFields.IndexOf("PWNAPP_IDENTIFICATIONEXPIRATIONDATE") == -1)
                {
                    managePawnRequiredFields.Add("PWNAPP_IDENTIFICATIONEXPIRATIONDATE");
                }
                managePawnRemoveReqdFields.Remove("PWNAPP_IDENTIFICATIONEXPIRATIONDATE");
                SetRequiredFieldsInForm();
            }
            else
            {
                managePawnRequiredFields.Remove("PWNAPP_IDENTIFICATIONEXPIRATIONDATE");
                if (managePawnRemoveReqdFields.IndexOf("PWNAPP_IDENTIFICATIONEXPIRATIONDATE") == -1)
                {
                    managePawnRemoveReqdFields.Add("PWNAPP_IDENTIFICATIONEXPIRATIONDATE");
                }

                SetRequiredFieldsInForm();
            }

            if (this.tableLayoutPanel2.Controls.Contains(pwnapp_identificationstate))
            {
                pwnapp_identificationstate.TabIndex = this.pwnapp_identificationtype.TabIndex + 1;
                identIssuer = (ComboBox)this.pwnapp_identificationstate.Controls[0];
                foreach (USState custidissuer in identIssuer.Items)
                {
                    custidissuer.Selected = false;
                }

                if (FormCustomer != null && !FormCustomer.NewCustomer)
                {
                    List<IdentificationVO> custIdTypes = FormCustomer.getIdentifications(selectedIDType);
                    //Madhu 12/10/2010 Fix for bugzilla no 10
                    bool primarySelected = false;
                    if (custIdTypes != null && custIdTypes.Count > 0)
                    {
                        foreach (IdentificationVO id in custIdTypes)
                        {
                            if (id.IdIssuerCode.Trim().Length != 0)
                            {
                                foreach (USState custidissuer in identIssuer.Items)
                                {
                                    if (custidissuer.ShortName == id.IdIssuerCode)
                                    {
                                        custidissuer.Selected = true;
                                        if (id.IsLatest)
                                        {
                                            //Madhu 12/06/2010 fix for bugzilla defect 10
                                            identIssuer.SelectedIndex = identIssuer.FindString(custidissuer.ShortName);
                                            pwnapp_identificationnumber.Enabled = false;
                                            pwnapp_identificationnumber.Text = id.IdValue;
                                            this.pwnapp_identificationexpirationdate.Enabled = false;
                                            if (id.IdExpiryData.FormatDate() != string.Empty && (id.IdExpiryData.Date != DateTime.MaxValue.Date))
                                            {
                                                this.pwnapp_identificationexpirationdate.Controls[0].Text = (id.IdExpiryData).FormatDate();
                                            }

                                            primarySelected = true;
                                            break;
                                        }
                                        else if (!primarySelected)
                                        {
                                            //Madhu 12/06/2010 fix for bugzilla defect 10
                                            resetValues();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (!primarySelected)
                    {
                        resetValues();
                    }
                }
                //Madhu 12/06/2010 fix for bugzilla defect 10
                //identIssuer.SelectedIndex = identIssuer.FindString(GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.State);
            }
            else
            {
                pwnapp_identificationcountry.TabIndex = this.pwnapp_identificationtype.TabIndex + 1;
                if (FormCustomer != null && !FormCustomer.NewCustomer)
                {
                    identIssuer = (ComboBox)this.pwnapp_identificationcountry.Controls[0];
                    List<IdentificationVO> custIdTypes = FormCustomer.getIdentifications(selectedIDType);
                    foreach (CountryData custidissuer in custIdCountry.Items)
                    {
                        custidissuer.Selected = false;
                    }

                    bool primarySelected = false;

                    if (custIdTypes != null && custIdTypes.Count > 0)
                    {
                        foreach (IdentificationVO id in custIdTypes)
                        {
                            if (id.IdIssuer.Trim().Length != 0)
                            {
                                foreach (CountryData custidissuer in custIdCountry.Items)
                                {
                                    if (custidissuer.Name == id.IdIssuer)
                                    {
                                        custidissuer.Selected = true;
                                        if (id.IsLatest)
                                        {
                                            //Madhu 12/06/2010 fix for bugzilla defect 10
                                            identIssuer.SelectedIndex = identIssuer.FindString(custidissuer.Name);
                                            pwnapp_identificationnumber.Enabled = false;
                                            this.pwnapp_identificationexpirationdate.Enabled = false;
                                            pwnapp_identificationnumber.Text = id.IdValue;
                                            if (id.IdExpiryData.FormatDate() != string.Empty && (id.IdExpiryData.Date != DateTime.MaxValue.Date))
                                            {
                                                this.pwnapp_identificationexpirationdate.Controls[0].Text = (id.IdExpiryData).FormatDate();
                                            }

                                            primarySelected = true;
                                            break;
                                        }
                                        else if (!primarySelected)
                                        {
                                            //Madhu 12/06/2010 fix for bugzilla defect 10
                                            resetValues();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (!primarySelected)
                    {
                        resetValues();
                    }
                }
            }
        }

        /// <summary>
        /// Method to reset to blank values
        /// </summary>
        /// <returns></returns>
        private void resetValues()
        {
            //this.custIdIssuer.Enabled = true;
            //this.custIdIssuer.SelectedIndex = 0;
            if (identIssuer.Enabled)
            {
                this.identIssuer.SelectedIndex = identIssuer.FindString("Select One");
                this.pwnapp_identificationnumber.Text = "";
                this.pwnapp_identificationexpirationdate.Controls[0].Text = "";
                this.pwnapp_identificationexpirationdate.Enabled = true;
                this.pwnapp_identificationnumber.Enabled = true;
            }
        }
        /// <summary>
        /// Method to check if any phone data was updated
        /// </summary>
        /// <returns></returns>
        private bool phoneDataChanged()
        {
            if (numberOfPhoneNumbers == 0 && numberOfPhoneNumbers == Customer.NumberContacts)
            {
                return false;
            }

            for (int i = 0; i < strTelecomNumTypeCode.Length; i++)
            {
                ContactVO custContInfo = Customer.getContact(strContactType[i], strAreaCode[i],
                    strPhoneNumber[i], strPhoneExtension[i], strCountryCode[i], strPrimaryPhone[i]);
                //If contact object in the current customer's context does not match
                //the data on the form for one of the types(home,cell,work)
                //that means phone data has been changed
                if (custContInfo == null)
                {
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// Method to check if identity information was changed for an existing customer
        /// </summary>
        /// <returns></returns>
        private bool identDataChanged()
        {
            if (strIdentTypeCode.Length > 0 && strIdentNumber.Length > 0 && strIdentIssuerName.Length > 0)
            {
                IdentificationVO custIdInfo = Customer.getIdentity(strIdentTypeCode, strIdentNumber, strIdentIssuerName);
                //if (custIdInfo != null)
                //    custIdInfo.IsLatest = true;
                return custIdInfo == null;
            }
            return false;
        }

        /// <summary>
        /// Method to pull notes value from the form
        /// </summary>
        private void getNotesDataFromForm()
        {
            strNotes = this.notesRichTextBox.Text;
        }

        /// <summary>
        /// Method to pull phone number data from form
        /// </summary>
        private void getPhoneNumberDataFromForm()
        {
            List<ContactVO> customerPhoneNumbers = this.phoneData1.getPhoneData();
            if (phoneData1.IsValid)
            {
                if (customerPhoneNumbers != null)
                {
                    numberOfPhoneNumbers = customerPhoneNumbers.Count;
                    //Array inputs
                    strPrimaryPhone = new string[numberOfPhoneNumbers];
                    strPhoneNumber = new string[numberOfPhoneNumbers];
                    strAreaCode = new string[numberOfPhoneNumbers];
                    strPhoneExtension = new string[numberOfPhoneNumbers];
                    strCountryCode = new string[numberOfPhoneNumbers];
                    strContactType = new string[numberOfPhoneNumbers];
                    strTelecomNumTypeCode = new string[numberOfPhoneNumbers];
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
            }
            else
            {
                isFormValid = false;
            }
        }

        /// <summary>
        /// Method to pull data entered in the physical description panel
        /// </summary>
        private void getPhysicalDescriptionDataFromForm()
        {
            custEyeColor = (ComboBox)this.pwnapp_eyes.Controls[0];
            custHairColor = (ComboBox)this.pwnapp_hair.Controls[0];
            custRace = (ComboBox)this.pwnapp_race.Controls[0];
            custGender = (ComboBox)this.pwnapp_sex.Controls[0];
            strEyeColor = custEyeColor.SelectedValue.ToString();
            strHairColor = custHairColor.SelectedValue.ToString();
            if (this.pwnapp_heightinches.Text.Trim().Length != 0)
            {
                strHeight = this.pwnapp_height.Text.ToString() + "'" + this.pwnapp_heightinches.Text.ToString() + "\"";
            }
            else
            {
                strHeight = this.pwnapp_height.Text.ToString() + "'";
            }

            if (this.pwnapp_weight.Text.Trim().Length != 0)
            {
                strWeight = this.pwnapp_weight.Text;
            }
            else
            {
                strWeight = "0";
            }

            if (Customer.NewCustomer)
            {
                strSSN = this.pwnapp_socialsecuritynumber.Controls[0].Text.ToString();
            }

            strRace = custRace.SelectedValue.ToString();
            strGender = custGender.SelectedValue.ToString();
        }

        /// <summary>
        /// Method to pull id data entered on the form
        /// </summary>
        private void getPersonalIdentificationDataFromForm()
        {
            identType = (ComboBox)this.pwnapp_identificationtype.Controls[0];
            strIdentTypeCode = identType.SelectedValue.ToString();

            //If idissuer1 is the control added in the form that means we need to 
            //pull state data for ID Issuer otherwise it is the country data
            if (tableLayoutPanel2.Controls.Contains(pwnapp_identificationstate))
            {
                identIssuer = (ComboBox)this.pwnapp_identificationstate.Controls[0];
                strIdentIssuer = identIssuer.GetItemText(identIssuer.SelectedItem);
                strIdentIssuerName = identIssuer.SelectedValue.ToString();
            }
            else
            {
                identIssuer = (ComboBox)this.pwnapp_identificationcountry.Controls[0];
                strIdentIssuer = identIssuer.SelectedValue.ToString();
                strIdentIssuerName = identIssuer.GetItemText(identIssuer.SelectedItem);
            }

            strIdentNumber = this.pwnapp_identificationnumber.Text.ToString();
            strIdentExpirydate = this.pwnapp_identificationexpirationdate.Controls[0].Text.ToString();
            try
            {
                DateTime idExpDate = Convert.ToDateTime(strIdentExpirydate);
            }
            catch (Exception)
            {
                //If the date was invalid it would have been caught by the regular expression
                //validation. The time when this exception will happen is when the default value of mm/dd/yyyy
                //remains in the textbox
                strIdentExpirydate = "";
            }

            IdentTypeCode[0] = strIdentTypeCode;
            IdentNumber[0] = strIdentNumber;
            IdentIssuerName[0] = strIdentIssuerName;
            IdentIssuer[0] = strIdentIssuer;
            IdentExpirydate[0] = strIdentExpirydate;
            IdIssuedDate[0] = "";
        }

        /// <summary>
        /// method to pull personal info data from form
        /// </summary>
        private void getPersonalInfoDataFromForm()
        {
            title = (ComboBox)this.pwnapp_title.Controls[0];
            titsuffix = (ComboBox)this.pwnapp_titlesuffix.Controls[0];
            addrstate = (ComboBox)this.pwnapp_state.Controls[0];

            strFirstName = this.pwnapp_firstname.Text;
            strLastName = this.pwnapp_lastname.Text;
            strDOB = this.pwnapp_dateofbirth.Controls[0].Text.ToString();
            strMiddleName = this.pwnapp_middleinitial.Text;
            strAddr1 = this.pwnapp_address.Text.Trim();
            strAddr2 = this.pwnapp_address2.Text.Trim();
            strZipcode = this.pwnapp_zip.Controls[0].Text.Trim();
            strCity = this.pwnapp_city.Text.Trim();
            strCustAddrState = addrstate.GetItemText(addrstate.SelectedItem);
            strCustUnit = this.pwnapp_unit.Text;
            strTitle = title.SelectedIndex > 0 ? title.SelectedValue.ToString() : "";
            strTitleSuffix = titsuffix.SelectedIndex > 0 ? titsuffix.SelectedValue.ToString() : "";
        }

        //Method to load the existing customer screen 
        //after creating a customer object with data entered on the form
        private void LoadExistingCustomer()
        {

            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            cds.EXExistingCustomers = custDatatable;
            FormCustomer.NewCustomer = true;
            //If the customer whose data is being updated is an existing customer
            //set the corresponding property in the existing customer screen
            //so as to get that information when this screen needs to be shown 
            //when Back or continue is clicked in the existing customer screen
            if (Customer != null && !Customer.NewCustomer)
            {
                cds.EXCurrentCustomer = Customer;
                FormCustomer.NewCustomer = false;
            }
            cds.EXNewCustomer = FormCustomer;
            if (idCheck)
            {

                cds.EXMessageToShow = Commons.GetMessageString("IDMatchMessage");
                if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.VIEWPAWNCUSTPRODDETAILS)
                {
                    StringBuilder errorMessage = new StringBuilder();
                    errorMessage.Append(Commons.GetMessageString("DuplicateIDMessage"));
                    errorMessage.Append("  <");
                    errorMessage.Append(strIdentTypeCode);
                    errorMessage.Append(",");
                    errorMessage.Append(strIdentIssuer);
                    errorMessage.Append(",");
                    errorMessage.Append(strIdentNumber);
                    errorMessage.Append(">");
                    cds.EXErrorMessage = errorMessage.ToString();

                }
            }
            if (nameCheck)
            {
                cds.EXMessageToShow = Commons.GetMessageString("NameMatchMessage");

            }
            cds.EXNameCheck = nameCheck;
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "ExistingCustomer";
            this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        /// <summary>
        /// Method to check if the customer is of valid age to get loan
        /// </summary>
        private void checkDateOfBirth()
        {
            string birthdate = pwnapp_dateofbirth.Controls[0].Text.Trim().ToString();
            int validAge = CustomerProcedures.getPawnBRCustomerLegalAge(GlobalDataAccessor.Instance.DesktopSession);
            //if we are here and do not have a value for validAge, the call to get it
            //from business rule engine failed. Hence assign the default value.
            if (validAge == 0)
            {
                validAge = CustomerValidAge.PAWNCUSTLEGALAGE;
            }

            if (birthdate.Length > 0)
            {
                try
                {
                    int age = Commons.getAge(birthdate, ShopDateTime.Instance.ShopDate);
                    this.ageTextbox.Text = age.ToString();
                    if (age < validAge)
                    {
                        if (age <= 0)
                        {
                            this.ageTextbox.Text = "0";
                            MessageBox.Show(Commons.GetMessageString("InvalidDateOfBirth"));
                        }
                        else
                        {
                            MessageBox.Show(Commons.GetMessageString("PawnLoanAgeError"));
                        }

                        this.pwnapp_dateofbirth_label.ForeColor = Color.Red;
                        pwnapp_dateofbirth.isValid = false;
                    }
                    else
                    {
                        this.pwnapp_dateofbirth_label.ForeColor = Color.Black;
                        customerAge = age;
                        if (updateRequiredFields)
                        {
                            SetRequiredFieldsInForm();
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(Commons.GetMessageString("InvalidDateOfBirth"));
                    pwnapp_dateofbirth.Focus();
                }
            }

        }

        /// <summary>
        /// Display Manage Override
        /// </summary>
        private void checkManageOverride()
        {
            //If this form was shown when in viewpawncustomerproduct details
            //Show the manage override form if the store state is not the same as the ID state selected
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VIEWPAWNCUSTPRODDETAILS, StringComparison.OrdinalIgnoreCase) ||
                (GlobalDataAccessor.Instance.DesktopSession.CompleteSale && GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.Any(i => i.IsGun)))
            {
                if ((stateIDSelected && strIdentIssuer != strStoreState) || (!stateIDSelected && !String.IsNullOrEmpty(GlobalDataAccessor.Instance.CurrentSiteId.CountryCode) && strIdentIssuer != GlobalDataAccessor.Instance.DesktopSession.CurrentSiteId.CountryCode))
                {
                    bool passedValidation = false;
                    do
                    {
                        ManageOverrides overrideForm = new ManageOverrides(GlobalDataAccessor.Instance.DesktopSession, ManageOverrides.OVERRIDE_TRIGGER);
                        overrideForm.MessageToShow = Commons.GetMessageString("ManageOverrideDefaultMessage") +
                            System.Environment.NewLine +
                            Commons.GetMessageString("OverrideReasonOutOfStateID");

                        overrideForm.TransactionNumbers = GlobalDataAccessor.Instance.DesktopSession.OverrideTransactionNumbers ?? new List<int>();
                        List<ManagerOverrideType> overrideTypes = new List<ManagerOverrideType> { ManagerOverrideType.OSID };
                        overrideForm.ManagerOverrideTypes = overrideTypes;
                        List<ManagerOverrideTransactionType> tranTypes;

                        if (GlobalDataAccessor.Instance.DesktopSession.CompleteSale)
                        {
                            tranTypes = new List<ManagerOverrideTransactionType> { ManagerOverrideTransactionType.SALE };
                        }
                        else
                        {
                            tranTypes = new List<ManagerOverrideTransactionType> { ManagerOverrideTransactionType.PU };
                        }

                        overrideForm.ManagerOverrideTransactionTypes = tranTypes;
                        overrideForm.ShowDialog(this);
                        passedValidation = overrideForm.OverrideAllowed;

                    } while (!passedValidation);
                }
            }
        }

        #region Event Handlers

        /// <summary>
        /// Submit Button event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void managePawnSubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                CheckReqFields();
                if (!reqFieldsEntered)
                {
                    MessageBox.Show(Commons.GetMessageString("FormRequiredFieldsFilledError"));
                }
                else
                {
                    //BZ # 619
                    checkManageOverride();

                    //if after the check, isFormValid is true, proceed
                    //else show an error message 
                    if (isFormValid)
                    {
                        //Do the age check again in case the user clicked on another field 
                        //instead of tabbing out of the date of birth field
                        if (!GlobalDataAccessor.Instance.DesktopSession.CashSaleCustomer)
                        {
                            checkDateOfBirth();
                        }
                        //Get Personal Information data entered on the form
                        getPersonalInfoDataFromForm();

                        bool partialAddressEntered = false;
                        bool addr1Entered = strAddr1 != string.Empty;
                        bool addr2Entered = strAddr2 != string.Empty;
                        bool cityEntered = strCity != string.Empty;
                        bool stateEntered = strCustAddrState != string.Empty && !strCustAddrState.ToUpper().Contains("SELECT");
                        bool zipEntered = strZipcode != string.Empty;

                        if ((addr1Entered || addr2Entered) && (!cityEntered || !stateEntered || !zipEntered))
                        {
                            partialAddressEntered = true;
                        }

                        if (!partialAddressEntered && cityEntered && !addr1Entered)
                        {
                            partialAddressEntered = true;
                        }

                        if (!partialAddressEntered && GlobalDataAccessor.Instance.DesktopSession.CashSaleCustomer)
                        {
                            if (stateEntered && !addr1Entered)
                            {
                                partialAddressEntered = true;
                            }
                        }

                        if (!partialAddressEntered && zipEntered && !addr1Entered)
                        {
                            partialAddressEntered = true;
                        }

                        if (partialAddressEntered)
                        {
                            MessageBox.Show("Partial address data cannot be entered.Either all fields should be entered or none.");
                            return;

                        }

                        //-----------------------------------------
                        // EDW CR#15278
                        selectedIDType = idType.SelectedIndex > 0 ? idType.SelectedValue.ToString() : "";
                        String selectedIdIssuer = String.Empty;

                        switch (selectedIDType)
                        {
                            case StateIdTypes.DRIVERLICENSE:
                            case StateIdTypes.STATE_IDENTIFICATION_ID:
                            case StateIdTypes.CONCEALED_WEAPONS_PERMIT:
                                selectedIdIssuer = custIdIssuer.Text.ToString();
                                break;
                            case "":
                            case StateIdTypes.MEXICAN_CONSULATE:
                            default:
                                selectedIdIssuer = custIdCountry.SelectedIndex > 0 ? custIdCountry.SelectedValue.ToString() : "";
                                break;
                        }

                        GlobalDataAccessor.Instance.DesktopSession.LastIdUsed = new KeyValuePair<string, string>(selectedIDType, selectedIdIssuer);
                        //-----------------------------------------

                        //Get Personal Identification data from form
                        getPersonalIdentificationDataFromForm();
                        if (Customer == null || Customer.NewCustomer)
                        {
                            ExecuteNewCustomerSubmit();
                        }
                        else
                        //For an existing customer
                        {
                            //Check that the Address entered is valid by calling the web service
                            //4-16-09 Commenting address check as per sukanya
                            //retValue = checkAddress();
                            //Get all values from form
                            //Get Physical Description data entered in form
                            getPhysicalDescriptionDataFromForm();
                            //Get Phone number dta from form
                            getPhoneNumberDataFromForm();
                            //Get Notes data
                            getNotesDataFromForm();
                            //Combine address1 and address 2                                        
                            if (strAddr2 != "")
                            {
                                strCustAddress = strAddr1 + "," + strAddr2;
                            }
                            else
                            {
                                strCustAddress = strAddr1;
                            }

                            if (isFormValid)
                            {
                                ExecuteExistingCustomerSubmit();
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(Commons.GetMessageString("FormErrorSubmitAgain"));
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(Commons.GetMessageString("FormErrorSubmitAgain"));
            }
        }

        /// <summary>
        /// Method to update data for existing customer
        /// </summary>
        private void ExecuteExistingCustomerSubmit()
        {
            bool checkID = false;
            bool checkName = false;
            //Load all the data into the customer object from the form
            //This wil fill up the formcustomer object with data
            partyID = Customer.PartyId;
            LoadPersonalInfoInObject();
            LoadIdentityInfoInObject();
            LoadAddressDataInObject();
            LoadPhoneDataInObject();
            LoadPhysicalDataInObject();

            //If the ID data was changed, check for duplicates
            if (identDataChanged())
            {
                if (doIdCheck())
                {
                    LoadExistingCustomer();
                    checkID = true;
                }
            }
            if (!checkID)
            {
                //ID check came back negative
                //proceed with name and DOB check
                if (Customer.FirstName != strFirstName || Customer.LastName != strLastName || (Customer.DateOfBirth).FormatDate() != strDOB)
                {
                    if (doNameCheck())
                    {
                        LoadExistingCustomer();
                        checkName = true;
                    }
                }
            }

            if (!checkID && !checkName)
            {
                if (performUpdates())
                {
                    isFormValid = true;
                    custNumber = Customer.CustomerNumber;
                    NavBox.NavAction action = NavBox.NavAction.NONE;
                    NavigateUserBasedOnTrigger(ref action);
                    this.NavControlBox.Action = action;
                }
                else
                {
                    MessageBox.Show(Commons.GetMessageString("ProcessingError"));
                }
            }
            //if all updates were successful, proceed
        }

        /// <summary>
        /// Method to check if user is selected 
        /// a previous issuer type and issercode from the drop down
        /// </summary>
        private bool isPreviousIDSelected()
        {
            bool returnVal = false;
            List<IdentificationVO> custIdTypes = Customer.getAllIdentifications();
            if (custIdTypes != null && custIdTypes.Count > 0)
            {
                foreach (IdentificationVO id in custIdTypes)
                {
                    if (id.IdType == strIdentTypeCode && id.IdIssuerCode == strIdentIssuer && id.IdValue == strIdentNumber && !id.IsLatest)
                    {
                        this.strIdentId = id.IdentId;
                        returnVal = true;
                        break;
                    }
                }
            }
            return returnVal;
        }

        /// <summary>
        /// Method that does all the updates in the DB for an existing customer
        /// if the data was changed from the original values
        /// </summary>
        /// <returns></returns>
        private bool performUpdates()
        {
            bool updatePersData = true;
            bool updateIdData = true;
            bool updatePhysData = true;
            bool updatePhoneData = true;

            try
            {
                DialogResult dgr = DialogResult.Retry;
                //Check if anything changed in the personal information section
                if (personalInfoChanged())
                {
                    do
                    {
                        updatePersData = new CustomerDBProcedures(CashlinxDesktopSession.Instance).UpdateCustPersonalInformation(strFirstName, strMiddleName, strLastName, strTitle, strTitleSuffix, strCustAddress, strCustUnit, strCity, strCustAddrState, strZipcode, strUserId, Customer.PartyId, strDOB, out errorCode, out errorMsg);
                        if (updatePersData)
                        {
                            //MessageBox.Show(Commons.GetMessageString("CustPersInfoUpdateSuccess"));
                            break;
                        }
                        else
                        {
                            dgr = MessageBox.Show(Commons.GetMessageString("CustPersInfoUpdateFailure"), "Error", MessageBoxButtons.RetryCancel);
                        }
                    } while (dgr == DialogResult.Retry);
                }

                //Madhu 12/06/2010 fix for bugzilla defect 10
                bool isPreviousIssuerSelected = isPreviousIDSelected();
                if (isPreviousIssuerSelected)
                {
                    //DialogResult dgr = DialogResult.Retry;
                    //bool updateIdData = false;
                    do
                    {
                        updateIdData = new CustomerDBProcedures(CashlinxDesktopSession.Instance).UpdatePrimaryFlagDatedIdent(strIdentId, out errorCode, out errorMsg);
                        if (updateIdData)
                        {
                            setIsLatest();
                            //MessageBox.Show(Commons.GetMessageString("CustIdentUpdateSuccess"));
                            break;
                        }
                        else
                        {
                            dgr = MessageBox.Show(Commons.GetMessageString("CustIdentPrimaryFlagUpdateFailure"), "Error", MessageBoxButtons.RetryCancel);
                        }
                    } while (dgr == DialogResult.Retry);
                }
                else if (identDataChanged())
                {
                    do
                    {
                        updateIdData = new CustomerDBProcedures(CashlinxDesktopSession.Instance).UpdateCustPersonalIdentification(Customer.PartyId, strIdentNumber, strIdentExpirydate, strIdentTypeCode, strIdentIssuer, strUserId, out errorCode, out errorMsg);
                        if (updateIdData)
                        {
                            setIsLatest();
                            //MessageBox.Show(Commons.GetMessageString("CustIdentUpdateSuccess"));
                            break;
                        }
                        else
                        {
                            dgr = MessageBox.Show(Commons.GetMessageString("CustIdentUpdateFailure"), "Error", MessageBoxButtons.RetryCancel);
                        }
                    } while (dgr == DialogResult.Retry);
                }

                if (Customer.Race != strRace || Customer.Gender != strGender || Customer.EyeColor != strEyeColor
                    || Customer.HairColor != strHairColor || Customer.Height != strHeight || Customer.Weight != Int32.Parse(strWeight))
                {
                    do
                    {
                        updatePhysData = new CustomerDBProcedures(CashlinxDesktopSession.Instance).UpdatePersonalDescription(strGender, strHeight, strWeight, strEyeColor, strHairColor, strUserId, Customer.PartyId, strRace, out errorCode, out errorMsg);
                        if (updatePhysData)
                        {
                            //MessageBox.Show(Commons.GetMessageString("CustPhysicalDescUpdateSuccess"));
                            break;
                        }
                        else
                        {
                            dgr = MessageBox.Show(Commons.GetMessageString("CustPhysicalDescUpdateFailure"), "Error", MessageBoxButtons.RetryCancel);
                        }
                    } while (dgr == DialogResult.Retry);
                }

                if (phoneDataChanged())
                {
                    do
                    {
                        updatePhoneData = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).UpdatePhoneDetails(Customer.PartyId, strContactType, strPhoneNumber, strAreaCode, strCountryCode, strPhoneExtension, strTelecomNumTypeCode, strPrimaryPhone, strUserId, out errorCode, out errorMsg);
                        if (updatePhoneData)
                        {
                            //MessageBox.Show(Commons.GetMessageString("CustPhoneUpdateSuccess"));
                            break;
                        }
                        else
                        {
                            dgr = MessageBox.Show(Commons.GetMessageString("CustPhoneUpdateFailure"), "Error", MessageBoxButtons.RetryCancel);
                        }
                    } while (dgr == DialogResult.Retry);
                }

                if (updateIdData && updatePersData && updatePhoneData && updatePhysData)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Failed to update customer data", new ApplicationException(ex.Message));
                return false;
            }
        }

        /// <summary>
        /// Sets isLatest=true for the selected issuer
        /// </summary>
        /// <returns></returns>
        private void setIsLatest()
        {
            //Madhu 12/10/2010 Fix for bugzilla no 10
            List<IdentificationVO> custIdTypes = FormCustomer.getAllIdentifications();
            foreach (IdentificationVO id in custIdTypes)
            {
                //if (id.IdType != strIdentTypeCode && id.IdIssuerCode != strIdentIssuer && id.IdValue != strIdentNumber)
                id.IsLatest = false;
            }
            IdentificationVO custIdInfo = Customer.getIdentity(strIdentTypeCode, strIdentNumber, strIdentIssuer);
            if (custIdInfo != null)
            {
                custIdInfo.IsLatest = true;
            }
        }

        private bool personalInfoChanged()
        {
            AddressVO homeAddr = Customer.getHomeAddress();
            if (homeAddr != null)
            {
                if (homeAddr.Address1 != strAddr1 ||
                    homeAddr.Address2 != strAddr2 ||
                    homeAddr.ZipCode != strZipcode ||
                    homeAddr.City != strCity ||
                    homeAddr.State_Code != strCustAddrState ||
                    homeAddr.UnitNum != strCustUnit)
                    return true;
            }
            else if (strAddr1 != string.Empty && strZipcode != string.Empty &&
                strCity != string.Empty && strCustAddrState != string.Empty)
            {
                return true;
            }

            if (Customer.CustTitle != strTitle || Customer.FirstName != strFirstName || Customer.LastName != strLastName || Customer.MiddleInitial != strMiddleName || (!string.IsNullOrEmpty(strDOB) && (Customer.DateOfBirth).FormatDate() != strDOB) || Customer.CustTitleSuffix != strTitleSuffix)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Method to add new customer data into the database
        /// </summary>
        private void ExecuteNewCustomerSubmit()
        {
            bool showOtherFields = true;
            nameCheck = GlobalDataAccessor.Instance.DesktopSession.MPNameCheck;
            bool checkID = false;
            bool checkName = false;
            LoadPersonalInfoInObject();
            LoadIdentityInfoInObject();
            LoadAddressDataInObject();

            if (doIdCheck())
            {
                nameCheck = false;
                LoadExistingCustomer();
                checkID = true;
                isFormValid = false;
            }

            if (!checkID)
            {
                //ID check came back negative
                //proceed with name and DOB check
                /*The call to check if duplicate ID exists passed
                     There is no ther customer in the system with the same data
                     proceed with checking if a customer exists with the same last name, first name and DOB.
                          The check to see if there is an existing customer with the same
                         last name, first name and Date of birth is done only once
                         If the user clicked on continue in the existing customer screen for 
                         a new customer who has the same name and dob as an existing customer
                         we let them proceed to save*/
                if (nameCheck == false)
                {
                    if (doNameCheck())
                    {
                        LoadExistingCustomer();
                        checkName = true;
                        isFormValid = false;
                    }
                }
                else
                {
                    checkName = false;
                    nameCheck = false;
                }
            }
            if (!checkID && !checkName)
            {
                if (GlobalDataAccessor.Instance.DesktopSession.CashSaleCustomer)
                {
                    if (isFormValid)
                        showOtherFields = false;

                }

                if (showOtherFields)
                {
                    if (this.phoneNumPanel.Visible == false)
                    {
                        isFormValid = false;
                        this.physicalDescriptionPanel.Visible = true;
                        this.phoneNumPanel.Visible = true;
                        this.notesPanel.Visible = true;
                        this.panelNotes.Visible = true;
                        this.notesRichTextBox.Visible = true;
                        this.tableLayoutPanel3.Visible = true;
                        this.tableLayoutPanel4.Visible = true;
                        this.phoneData1.Visible = true;
                        this.tableLayoutPanel6.Visible = true;
                        //this.managePawnSubmitButton.Enabled = false;
                        this.pwnapp_race.Focus();
                        return;
                    }
                }

                //All the data has already been collected..proceed with save

                //Check that the Address entered is valid by calling the web service
                //4/16/09 - commenting out as per sukanya
                //retValue = checkAddress();
                //Proceed to save in the database
                //Get the other values from the form
                getPhysicalDescriptionDataFromForm();
                getPhoneNumberDataFromForm();
                if (!isFormValid)
                {
                    return;
                }

                getNotesDataFromForm();
                //validatePhoneData();
                //Combine address1 and address 2                                        
                if (strAddr2 != "")
                {
                    strCustAddress = strAddr1 + "," + strAddr2;
                }
                else
                {
                    strCustAddress = strAddr1;
                }

                //Call to save the data in the database
                DialogResult dgr = DialogResult.Retry;
                do
                {
                    try
                    {
                        retValue = new CustomerDBProcedures(CashlinxDesktopSession.Instance).InsertCustomer(CustomerTypes.PERSONTYPE,
                                                                        strFirstName, strMiddleName, strLastName,
                                                                        strDOB, strUserId,
                                                                        strEyeColor, strHairColor, strHeight,
                                                                        strWeight, strRace, strContactType,
                                                                        strPhoneNumber,
                                                                        strAreaCode, strCountryCode,
                                                                        strPhoneExtension, strTelecomNumTypeCode,
                                                                        strPrimaryPhone, strCustUnit, strCustAddress,
                                                                        strCity, strCustAddrState,
                                                                        strZipcode, IdentNumber, IdIssuedDate,
                                                                        IdentExpirydate,
                                                                        IdentIssuer, IdentTypeCode, strSSN, strTitle,
                                                                        strGender,
                                                                        strTitleSuffix, "", "", "", "",
                                                                        strStoreNumber, out partyID, out custNumber,
                                                                        out errorCode, out errorMsg);
                    }
                    catch (SystemException ex)
                    {
                        BasicExceptionHandler.Instance.AddException(
                            "Customer Save Failed " + CustomerTypes.PERSONTYPE + " " + strFirstName + " " +
                            strMiddleName + " " + strLastName + " " + strDOB + " " + strUserId + " " + strEyeColor +
                            " " + strHairColor + " " + strHeight + " " + strWeight + " " + strRace + " " +
                            strContactType + " " + strPhoneNumber + " " + strAreaCode + " " + strCountryCode + " " +
                            strPhoneExtension + " " + strTelecomNumTypeCode + " " + strCustUnit + " " +
                            strCustAddress + " " + strCity + " " + strCustAddrState + " " + strZipcode + " " +
                            strIdentNumber + " " + strIdIssuedDate + " " + strIdentExpirydate + " " + strIdentIssuer +
                            " " + strIdentTypeCode + " " + strSSN + " " + strTitle + " " + strGender + " " +
                            strTitleSuffix, ex);
                        retValue = false;
                    }
                    if (!retValue)
                    {
                        if (errorCode == CustomerProcedures.DUPLICATESSNORACLEERRORCODE)
                        {
                            _ssnDuplicate = true;
                            dgr = MessageBox.Show(Commons.GetMessageString("DuplicateSSNMessage"), "Warning",
                                                    MessageBoxButtons.OK);
                        }
                        else
                        {
                            dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error",
                                                    MessageBoxButtons.RetryCancel);
                        }
                    }
                    else
                    {
                        break;
                    }
                } while (dgr == DialogResult.Retry);

                if (retValue)
                {
                    //MessageBox.Show(Commons.GetMessageString("CustCreationSuccess"));
                    isFormValid = true;
                    //check trigger and take the user there accordingly
                    NavBox.NavAction action = NavBox.NavAction.NONE;
                    this.NavigateUserBasedOnTrigger(ref action);
                    this.NavControlBox.Action = action;
                }
                else
                {
                    if (!_ssnDuplicate)
                    {
                        MessageBox.Show(Commons.GetMessageString("ProcessingError"));
                    }
                }
            }
            //The duplicate name check was performed one time hence proceed without check
        }

        /// <summary>
        /// Method that takes the user to the respective area
        /// based on where they came from
        /// </summary>
        /// 
        private void NavigateUserBasedOnTrigger(ref NavBox.NavAction actionTo)
        {
            CashlinxDesktopSession cds = CashlinxDesktopSession.Instance;
            string trigger = cds.HistorySession.Trigger;
            LoadCustomerDataInObject();
            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = this.Customer;
            GlobalDataAccessor.Instance.DesktopSession.MPNameCheck = false;
            GlobalDataAccessor.Instance.DesktopSession.MPCustomer = null;
            GlobalDataAccessor.Instance.DesktopSession.CustomerValidated = true;

            if (trigger.Equals("LookupTicket", StringComparison.OrdinalIgnoreCase))
            {
                //MessageBox.Show("Show View Pawn Customer Product Details");
                //CashlinxDesktop.GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "ViewPawnCustomerProductDetails";
                actionTo = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {
                try
                {
                    if (trigger.Equals(Commons.TriggerTypes.MANAGEMULTIPLEPAWNITEMS, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Show Maintain Shopping Cart");
                        GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                        actionTo = NavBox.NavAction.CANCEL;
                    }
                    else if (trigger.Equals(Commons.TriggerTypes.VIEWPAWNCUSTPRODDETAILS, StringComparison.OrdinalIgnoreCase))//_trigger == TriggerTypes.VIEWPAWNCUSTPRODDETAILS)
                    {
                        //MessageBox.Show("Show View Pawn Customer Product Details");

                        GlobalDataAccessor.Instance.DesktopSession.PickupProcessContinue = true;
                        this.NavControlBox.IsCustom = true;
                        this.NavControlBox.CustomDetail = "ViewPawnCustomerProductDetails";
                        actionTo = NavBox.NavAction.BACKANDSUBMIT;
                    }
                    else if (trigger.Equals(Commons.TriggerTypes.LOOKUPCUSTOMER, StringComparison.OrdinalIgnoreCase))
                    {
                        //MessageBox.Show("Customer Information Recorded ");
                        //GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                        //If this is the path taken during a new loan flow
                        //as in loan is started by selecting item from item history
                        //create the pawn application
                        //Generate Loan application and persist the loan application
                        if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan != null
                            && GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items != null
                            && GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.Count > 0
                            && GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.TicketNumber==0)
                        {
                            DialogResult dgr = DialogResult.Retry;
                            do
                            {
                                retValue = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).InsertPawnApplication(custNumber, strStoreNumber, strClothing, strNotes, strIdentTypeCode, strIdentNumber, strIdentIssuer, strIdentExpirydate, strUserId, out strPawnAppId, out errorCode, out errorMsg);
                                if (!retValue)
                                {
                                    dgr = MessageBox.Show(Commons.GetMessageString("LoanIdCreationError"), "Error", MessageBoxButtons.RetryCancel);
                                    if (dgr == DialogResult.Cancel)
                                    {
                                        GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                                        //GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                                        actionTo = NavBox.NavAction.CANCEL;
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            } while (dgr == DialogResult.Retry);

                            GlobalDataAccessor.Instance.DesktopSession.CurPawnAppId = strPawnAppId;
                            GlobalDataAccessor.Instance.DesktopSession.Clothing = strClothing;
                        }
                        this.NavControlBox.IsCustom = true;
                        this.NavControlBox.CustomDetail = "DescribeMerchandise";
                        actionTo = NavBox.NavAction.BACKANDSUBMIT;
                    }
                    else if (trigger.Equals(Commons.TriggerTypes.NEWPAWNLOAN, StringComparison.OrdinalIgnoreCase) ||
                        trigger.Equals(Commons.TriggerTypes.DESCRIBEMERCHANDISE, StringComparison.OrdinalIgnoreCase))
                    {
                        //Generate Loan application and persist the loan application
                        DialogResult dgr = DialogResult.Retry;
                        do
                        {
                            retValue = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).InsertPawnApplication(custNumber, strStoreNumber, strClothing, strNotes, strIdentTypeCode, strIdentNumber, strIdentIssuer, strIdentExpirydate, strUserId, out strPawnAppId, out errorCode, out errorMsg);
                            if (!retValue)
                            {
                                dgr = MessageBox.Show(Commons.GetMessageString("LoanIdCreationError"), "Error", MessageBoxButtons.RetryCancel);
                                if (dgr == DialogResult.Cancel)
                                {
                                    GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
                                    //GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                                    actionTo = NavBox.NavAction.CANCEL;
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        } while (dgr == DialogResult.Retry);

                        GlobalDataAccessor.Instance.DesktopSession.CurPawnAppId = strPawnAppId;
                        GlobalDataAccessor.Instance.DesktopSession.Clothing = strClothing;
                        this.NavControlBox.IsCustom = true;
                        this.NavControlBox.CustomDetail = "DescribeMerchandise";
                        actionTo = NavBox.NavAction.BACKANDSUBMIT;
                    }
                    else if (trigger.Equals(Commons.TriggerTypes.CUSTOMERPURCHASE, StringComparison.OrdinalIgnoreCase))
                    {
                        this.NavControlBox.IsCustom = true;
                        this.NavControlBox.CustomDetail = "DescribeMerchandise";
                        actionTo = NavBox.NavAction.BACKANDSUBMIT;
                    }
                    else if (trigger.Equals(Commons.TriggerTypes.DESCRIBEITEMCUSTOMERPURCHASE, StringComparison.OrdinalIgnoreCase))
                    {
                        this.NavControlBox.IsCustom = true;
                        this.NavControlBox.CustomDetail = "DescribeMerchandise";
                        actionTo = NavBox.NavAction.BACKANDSUBMIT;
                    }
                    else
                    {
                        actionTo = NavBox.NavAction.SUBMIT;
                    }
                }
                catch (Exception)
                {
                    actionTo = NavBox.NavAction.CANCEL;
                }
            }
        }

        private void LoadCustomerDataInObject()
        {
            LoadPersonalInfoInObject();
            LoadIdentityInfoInObject();
            LoadAddressDataInObject();
            LoadPhoneDataInObject();
            LoadPhysicalDataInObject();
            Customer = FormCustomer;
            Customer.CustomerNumber = custNumber;
            Customer.PartyId = partyID;
            Customer.CustomerSince = ShopDateTime.Instance.ShopDate;
            Customer.NewCustomer = false;
        }

        private void LoadPhysicalDataInObject()
        {
            FormCustomer.SocialSecurityNumber = strSSN;
            FormCustomer.EyeColor = strEyeColor;
            FormCustomer.Gender = strGender;
            FormCustomer.HairColor = strHairColor;
            FormCustomer.Height = strHeight;
            FormCustomer.Race = strRace;
            if (strWeight != string.Empty)
            {
                try
                {
                    FormCustomer.Weight = Convert.ToInt32(strWeight);
                }
                catch (Exception)
                {
                    FormCustomer.Weight = 0;
                }
            }
        }

        private void LoadPhoneDataInObject()
        {
            if (FormCustomer != null & !FormCustomer.NewCustomer)
            {
                FormCustomer.removePhoneNumbers();
            }
            for (int i = 0; i < numberOfPhoneNumbers; i++)
            {
                ContactVO custcontact = new ContactVO();
                custcontact.TelecomNumType = strTelecomNumTypeCode[i];
                custcontact.ContactAreaCode = strAreaCode[i];
                custcontact.ContactPhoneNumber = strPhoneNumber[i];
                custcontact.ContactExtension = strPhoneExtension[i];
                custcontact.ContactType = strContactType[i];
                custcontact.TeleusrDefText = strPrimaryPhone[i];
                custcontact.CountryDialNumCode = strCountryCode[i];
                FormCustomer.addContact(custcontact);
            }
        }

        private void LoadAddressDataInObject()
        {
            AddressVO custHomeAddr = FormCustomer.getHomeAddress();
            if (custHomeAddr != null)
            {
                FormCustomer.updateHomeAddress(strAddr1, strAddr2, strCustUnit, strCity, strZipcode, custHomeAddr.Country_Code, strCustAddrState, "");
            }
            else
            {
                AddressVO newCustAddr = new AddressVO();
                newCustAddr.Address1 = strAddr1;
                newCustAddr.Address2 = strAddr2;
                newCustAddr.City = strCity;
                newCustAddr.State_Code = strCustAddrState;
                newCustAddr.ZipCode = strZipcode;
                newCustAddr.UnitNum = strCustUnit;
                newCustAddr.ContactTypeCode = CustomerAddressTypes.HOME_ADDRESS;
                newCustAddr.ContMethodTypeCode = "POSTALADDR";

                if (strAddr2 != "")
                {
                    newCustAddr.CustAddress = strAddr1 + "," + strAddr2 + "," + strCity + "," + strCustAddrState + "," + strZipcode;
                }
                else
                {
                    newCustAddr.CustAddress = strAddr1 + "," + strCity + "," + strCustAddrState + "," + strZipcode;
                }

                FormCustomer.addAddress(newCustAddr);
            }
        }

        private void LoadIdentityInfoInObject()
        {
            if (FormCustomer.NewCustomer)
            {
                IdentificationVO newCustId = new IdentificationVO
                {
                    IdType = strIdentTypeCode,
                    IdValue = strIdentNumber,
                    IdIssuer = strIdentIssuerName,
                    IdIssuerCode = strIdentIssuer,
                    IsLatest = true
                };
                //Madhu 04/19/2011 Fix for BZ # 549...modified == to >=
                if (strIdentExpirydate.Trim().Length >= 8)
                {
                    newCustId.IdExpiryData = DateTime.Parse(strIdentExpirydate);
                }

                FormCustomer.addIdentity(newCustId);
            }
            else
            {
                IdentificationVO custId = null;
                if (strIdentExpirydate != string.Empty)
                {
                    custId = FormCustomer.getIdentity(strIdentTypeCode, strIdentNumber, strIdentIssuer, strIdentExpirydate);
                }
                else
                {
                    custId = FormCustomer.getIdentity(strIdentTypeCode, strIdentNumber, strIdentIssuerName);
                }


                if (custId == null)
                {
                    IdentificationVO newCustId = new IdentificationVO
                    {
                        IdType = strIdentTypeCode,
                        IdValue = strIdentNumber,
                        IdIssuer = strIdentIssuerName,
                        IdIssuerCode = strIdentIssuer,
                        IsLatest = true
                    };

                    //Madhu 12/10/2010 Fix for bugzilla no 10...modified == to >=
                    if (strIdentExpirydate.Trim().Length >= 8)
                    {
                        newCustId.IdExpiryData = DateTime.Parse(strIdentExpirydate);
                    }

                    FormCustomer.addIdentity(newCustId);
                }
                else
                {
                    custId.IsLatest = true;
                    FormCustomer.updateLatestIdentity(custId);
                }

            }
        }

        private void LoadPersonalInfoInObject()
        {
            FormCustomer.FirstName = strFirstName;
            FormCustomer.LastName = strLastName;
            FormCustomer.MiddleInitial = strMiddleName;
            FormCustomer.CustTitle = strTitle;
            FormCustomer.CustTitleSuffix = strTitleSuffix;
            if (strDOB.Trim().Length == 10 && !strDOB.Equals("mm/dd/yyyy", StringComparison.OrdinalIgnoreCase))
            {
                FormCustomer.DateOfBirth = DateTime.Parse(strDOB);
            }

            FormCustomer.Age = customerAge;
        }

        private bool doNameCheck()
        {
            if (GlobalDataAccessor.Instance.DesktopSession.CashSaleCustomer && (string.IsNullOrEmpty(strDOB) || strDOB.Equals("mm/dd/yyyy", StringComparison.OrdinalIgnoreCase)))
            {
                if (strDOB != null)
                {
                    if (strDOB.Equals("mm/dd/yyyy", StringComparison.OrdinalIgnoreCase))
                    {
                        strDOB = "";
                    }
                }
                return false;
            }

            retValue = new CustomerDBProcedures(CashlinxDesktopSession.Instance).CheckDuplicateNameDOB(strFirstName, strLastName, strDOB, out custDatatable, out errorCode, out errorMsg);
            if (retValue)
            {
                if (custDatatable != null && custDatatable.Rows.Count > 0)
                {
                    //Take them to the existing customer screen
                    nameCheck = true;
                    return true;

                }
                return false;
            }

            FileLogger.Instance.logMessage(LogLevel.ERROR, this, errorMsg);
            throw new SystemException("Call to check duplicate name and dob failed");
        }

        private bool doIdCheck()
        {
            //Check if the customer already exists in the system
            //with the same ID or with the same name and date of birth
            retValue = new CustomerDBProcedures(CashlinxDesktopSession.Instance).CheckDuplicateID(strIdentTypeCode, strIdentNumber, strIdentIssuer, out custDatatable, out errorCode, out errorMsg);
            if (retValue)
            {
                if (custDatatable != null)
                //show existing customer screen
                //call to duplicate id check yielded records...show existing customer screen
                {
                    idCheck = true;
                    return true;
                }
                return false;
            }

            FileLogger.Instance.logMessage(LogLevel.ERROR, this, errorMsg);
            throw new SystemException("Call to check duplicate ID failed");
        }

        /// <summary>
        /// Fired when the control is loaded in the main form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void idType1_Load(object sender, EventArgs e)
        {
            //Load the state combobox with values
            populateIssuerType();
        }

        /// <summary>
        /// When cancel button is clicked, the manage pawn screen should be closed
        /// and the user is shown the cashlinx desktop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void managePawnCancelButton_Click(object sender, EventArgs e)
        {

            DialogResult dR = MessageBox.Show("Do you want to continue processing this customer?", "Describe Item Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dR == DialogResult.No)
            {
                GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();
            }
            GlobalDataAccessor.Instance.DesktopSession.ClearPawnLoan();
            GlobalDataAccessor.Instance.DesktopSession.PawnLoans.Add(new PawnLoan());
            isFormValid = true;
            this.NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        /// <summary>
        /// Fired when the user selects an ID Type and tabs out of the control
        /// Depending on what was selected, the Issuer agency needs to be changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void idType1_Leave_1(object sender, EventArgs e)
        {
            //call to populate the Issuer agency based on what was selected in the Issue type
            populateIssuerType();
            if (updateRequiredFields)
            {
                SetRequiredFieldsInForm();
            }
        }

        private void idType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateIssuerType();
        }

        private void idIssuer_Leave(object sender, EventArgs e)
        {
            populateIdNumberAndDate();
            //BZ # 619 -- This logic has been moved to submit button click event
            //If this form was shown when in viewpawncustomerproduct details
            //Show the manage override form if the store state is not the same as the ID state selected
            /*            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.VIEWPAWNCUSTPRODDETAILS, StringComparison.OrdinalIgnoreCase) ||
                            (GlobalDataAccessor.Instance.DesktopSession.CompleteSale && (GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.Any(i => i.IsGun) 
                            || GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.Any(i => i.IsJewelry))))
                        {
                            if (stateIDSelected && strIdentIssuer != strStoreState)
                            {
                                bool passedValidation = false;
                                do
                                {
                                    ManageOverrides overrideForm = new ManageOverrides(ManageOverrides.OVERRIDE_TRIGGER);
                                    overrideForm.MessageToShow = Commons.GetMessageString("ManageOverrideDefaultMessage") + 
                                        System.Environment.NewLine + 
                                        Commons.GetMessageString("OverrideReasonOutOfStateID");
                        
                                    overrideForm.TransactionNumbers = GlobalDataAccessor.Instance.DesktopSession.OverrideTransactionNumbers ?? new List<int>();
                                    List<ManagerOverrideType> overrideTypes=new List<ManagerOverrideType> {ManagerOverrideType.OSID};
                                    overrideForm.ManagerOverrideTypes = overrideTypes;
                                    List<ManagerOverrideTransactionType> tranTypes;
                                    if (GlobalDataAccessor.Instance.DesktopSession.CompleteSale)
                                    tranTypes= new List<ManagerOverrideTransactionType>
                                                                                         {ManagerOverrideTransactionType.SALE};
                                    else
                                        tranTypes = new List<ManagerOverrideTransactionType> { ManagerOverrideTransactionType.PU };

                        
                                    overrideForm.ManagerOverrideTransactionTypes = tranTypes;
                                    overrideForm.ShowDialog(this);
                                    passedValidation = overrideForm.OverrideAllowed;
                                } while (!passedValidation);
                            }
                        } */
            if (updateRequiredFields)
            {
                SetRequiredFieldsInForm();
            }
        }

        private void custIdIssuer_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateIdNumberAndDate();
        }

        private void custIdCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateIdNumberAndDate();
        }

        private void country_Leave(object sender, EventArgs e)
        {
            populateIdNumberAndDate();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {

            FormCustomer = Utilities.CloneObject<CustomerVO>(Customer);
            LoadDataInControlsFromObject();
            if (FormCustomer.NewCustomer)
            {
                //set the ID fields
                idType.SelectedIndex = 0;
                pwnapp_identificationnumber.Text = "";
                pwnapp_identificationexpirationdate.Controls[0].Text = "";
                pwnapp_identificationnumber.Enabled = false;
                pwnapp_identificationexpirationdate.Enabled = false;
                this.tableLayoutPanel2.ColumnStyles[1].Width = 51;
                //Madhu - BZ # 116
                this.pwnapp_firstname.Text = "";
                this.pwnapp_lastname.Text = "";
                ComboBox state = (ComboBox)this.pwnapp_state.Controls[0];
                state.SelectedIndex = -1;
                this.pwnapp_socialsecuritynumber.Controls[0].Text = "";
                this.pwnapp_dateofbirth.Controls[0].Text = "";
                this.ageTextbox.Text = "";
            }

            managePawnRequiredFields = CustomerProcedures.getPawnRequiredFields(CashlinxDesktopSession.Instance);
            //If the session variable updaterequiredfieldsforcustomer is set to true set the local variable
            if (GlobalDataAccessor.Instance.DesktopSession.UpdateRequiredFieldsForCustomer)
            {
                updateRequiredFields = true;
            }

            SetRequiredFieldsInForm();
        }
        # endregion

        /// <summary>
        /// Method to check if all the required fields are entered 
        /// </summary>
        private void CheckReqFields()
        {
            bool fieldError = false;
            reqFieldsEntered = false;
            bool phoneNumberEntered = false;
            if (managePawnRequiredFields.Count == 0)
            {
                isFormValid = true;
                reqFieldsEntered = true;
                return;
            }

            string phoneField = (from reqField in managePawnRequiredFields
                                 where reqField == "PWNAPP_PHONENUMBER"
                                 select reqField).FirstOrDefault();
            if (phoneField != null)
            {
                if (phoneData1.Visible && phoneData1.IsPhoneEntered())
                {
                    phoneNumberEntered = true;
                    reqFieldsEntered = true;
                }
            }
            else
            {
                phoneNumberEntered = true;
            }

            foreach (string reqdField in managePawnRequiredFields)
            {

                string fieldName = reqdField.ToLower();
                Control[] fieldCtrl = this.Controls.Find(fieldName, true);
                if (fieldCtrl.Length > 0)
                {
                    if (fieldCtrl[0].GetType() == typeof(CustomTextBox))
                    {
                        if (fieldCtrl[0].Visible)
                        {
                            reqFieldsEntered = ((CustomTextBox)(fieldCtrl[0])).isValid;
                            if (!(((CustomTextBox)(fieldCtrl[0])).isValid))
                            {
                                fieldCtrl[0].BackColor = Color.Yellow;
                            }
                            else
                            {
                                fieldCtrl[0].BackColor = Color.White;
                            }

                        }
                    }
                    else if (fieldCtrl[0].GetType() == typeof(IDType))
                    {
                        reqFieldsEntered = ((IDType)(fieldCtrl[0])).isValid;
                        if (!(((IDType)(fieldCtrl[0])).isValid))
                        {
                            (fieldCtrl[0]).BackColor = Color.Yellow;
                        }
                        else
                        {
                            fieldCtrl[0].BackColor = Color.White;
                        }

                    }
                    else if (fieldCtrl[0].GetType() == typeof(State))
                    {
                        if (fieldCtrl[0].Visible == true)
                        {
                            reqFieldsEntered = ((State)(fieldCtrl[0])).isValid;
                            if (!(((State)(fieldCtrl[0])).isValid))
                            {
                                fieldCtrl[0].BackColor = Color.Yellow;
                            }
                            else
                            {
                                fieldCtrl[0].BackColor = Color.White;
                            }
                        }

                    }
                    else if (fieldCtrl[0].GetType() == typeof(Country))
                    {
                        if (fieldCtrl[0].Visible == true)
                        {
                            reqFieldsEntered = ((Country)(fieldCtrl[0])).isValid;
                            if (!(((Country)(fieldCtrl[0])).isValid))
                            {
                                fieldCtrl[0].BackColor = Color.Yellow;
                            }
                            else
                            {
                                fieldCtrl[0].BackColor = Color.White;
                            }
                        }

                    }
                    else if (fieldCtrl[0].GetType() == typeof(Zipcode))
                    {
                        reqFieldsEntered = ((Zipcode)(fieldCtrl[0])).isValid;
                        if (!(((Zipcode)(fieldCtrl[0])).isValid))
                        {
                            fieldCtrl[0].BackColor = Color.Yellow;
                        }
                        else
                        {
                            fieldCtrl[0].BackColor = Color.White;
                        }

                    }
                    else if (fieldCtrl[0].GetType() == typeof(Date))
                    {
                        reqFieldsEntered = ((Date)(fieldCtrl[0])).isValid;
                        if (!(((Date)(fieldCtrl[0])).isValid))
                        {
                            fieldCtrl[0].BackColor = Color.Yellow;
                        }
                        else
                        {
                            fieldCtrl[0].BackColor = Color.White;
                        }

                    }
                    else if (fieldCtrl[0].GetType() == typeof(Race))
                    {
                        if (fieldCtrl[0].Visible)
                        {
                            reqFieldsEntered = ((Race)(fieldCtrl[0])).isValid;
                            if (!(((Race)(fieldCtrl[0])).isValid))
                            {
                                fieldCtrl[0].BackColor = Color.Yellow;
                            }
                            else
                            {
                                fieldCtrl[0].BackColor = Color.White;
                            }
                        }

                    }
                    else if (fieldCtrl[0].GetType() == typeof(Gender))
                    {
                        if (fieldCtrl[0].Visible)
                        {
                            reqFieldsEntered = ((Gender)(fieldCtrl[0])).isValid;
                            if (!(((Gender)(fieldCtrl[0])).isValid))
                            {
                                fieldCtrl[0].BackColor = Color.Yellow;
                            }
                            else
                            {
                                fieldCtrl[0].BackColor = Color.White;
                            }
                        }

                    }
                    else if (fieldCtrl[0].GetType() == typeof(Haircolor))
                    {
                        if (fieldCtrl[0].Visible)
                        {
                            reqFieldsEntered = ((Haircolor)(fieldCtrl[0])).isValid;
                            if (!(((Haircolor)(fieldCtrl[0])).isValid))
                            {
                                fieldCtrl[0].BackColor = Color.Yellow;
                            }
                            else
                            {
                                fieldCtrl[0].BackColor = Color.White;
                            }
                        }

                    }
                    else if (fieldCtrl[0].GetType() == typeof(EyeColor))
                    {
                        if (fieldCtrl[0].Visible)
                        {
                            reqFieldsEntered = ((EyeColor)(fieldCtrl[0])).isValid;
                            if (!(((EyeColor)(fieldCtrl[0])).isValid))
                            {
                                fieldCtrl[0].BackColor = Color.Yellow;
                            }
                            else
                            {
                                fieldCtrl[0].BackColor = Color.White;
                            }
                        }

                    }

                    if (!reqFieldsEntered)
                    {
                        fieldError = true;
                    }
                }
            }

            if (GlobalDataAccessor.Instance.DesktopSession.CashSaleCustomer)
            {
                bool nameEntered = false;
                bool dobEntered = false;
                bool idEntered = false;

                if (pwnapp_firstname.isValid && pwnapp_lastname.isValid)
                {
                    nameEntered = true;
                }

                if (pwnapp_dateofbirth.isValid)
                {
                    dobEntered = true;
                }

                if (!string.IsNullOrEmpty(strIdentTypeCode) && !string.IsNullOrEmpty(strIdentIssuerName) && !string.IsNullOrEmpty(strIdentNumber))
                {
                    idEntered = true;
                }

                if (nameEntered && (idEntered || dobEntered || phoneNumberEntered) && !fieldError)
                {
                    isFormValid = true;
                    reqFieldsEntered = true;
                }
                else
                {
                    this.phoneNumPanel.BackColor = !phoneNumberEntered ? Color.Yellow : Color.MediumBlue;
                    reqFieldsEntered = false;
                    isFormValid = false;

                }
                return;
            }


            if (!fieldError && !idNumberError && phoneNumberEntered)
            {
                isFormValid = true;
            }
            else
            {

                isFormValid = false;
            }
        }

        /// <summary>
        /// Method to fetch the ID number and date for the customer if it exists
        /// </summary>
        private void populateIdNumberAndDate()
        {
            identType = (ComboBox)this.pwnapp_identificationtype.Controls[0];
            strIdentTypeCode = identType.SelectedValue.ToString();
            //If idissuer1 is the control added in the form that means we need to 
            //pull state data for ID Issuer otherwise it is the country data
            if (tableLayoutPanel2.Controls.Contains(pwnapp_identificationstate))
            {
                identIssuer = (ComboBox)this.pwnapp_identificationstate.Controls[0];
                strIdentIssuer = identIssuer.GetItemText(identIssuer.SelectedItem);
                stateIDSelected = true;
            }
            else
            {
                stateIDSelected = false;
                identIssuer = (ComboBox)this.pwnapp_identificationcountry.Controls[0];
                strIdentIssuer = identIssuer.SelectedValue.ToString();
            }

            if (Customer != null && !Customer.NewCustomer)
            {
                IdentificationVO customerId = null;
                if (Customer != null)
                {
                    customerId = Customer.getIdByTypeandIssuer(strIdentTypeCode, strIdentIssuer);
                }
                if (customerId != null)
                {
                    this.pwnapp_identificationnumber.Text = customerId.IdValue.ToString();
                    if (customerId.IdExpiryData.Date != DateTime.MaxValue.Date)
                    {
                        this.pwnapp_identificationexpirationdate.Controls[0].Text = (customerId.IdExpiryData).FormatDate();
                    }
                    else
                    {
                        this.pwnapp_identificationexpirationdate.Controls[0].Text = "";
                    }

                    this.pwnapp_identificationexpirationdate.Enabled = false;
                    this.pwnapp_identificationnumber.Enabled = false;
                    this.SelectNextControl(this.pwnapp_identificationexpirationdate, true, true, true, true);
                }
                else
                {
                    this.pwnapp_identificationnumber.Text = "";
                    this.pwnapp_identificationexpirationdate.Controls[0].Text = "";
                    this.pwnapp_identificationexpirationdate.Enabled = true;
                    this.pwnapp_identificationnumber.Enabled = true;
                }
            }
            else
            {
                pwnapp_identificationnumber.Enabled = true;
                pwnapp_identificationexpirationdate.Enabled = true;
            }
        }

        private void zipcode1_Leave(object sender, EventArgs e)
        {
            try
            {
                ComboBox state = (ComboBox)this.pwnapp_state.Controls[0];
                if (pwnapp_zip.isValid)
                {
                    if (pwnapp_zip.City.Length > 0)
                    {
                        this.pwnapp_city.Text = pwnapp_zip.City;
                    }
                    else
                    {
                        this.pwnapp_city.Text = "";
                    }

                    if (pwnapp_zip.State.Length > 0)
                    {
                        foreach (USState currstate in state.Items)
                        {

                            if (currstate.ShortName == pwnapp_zip.State)
                            {
                                state.SelectedIndex = state.Items.IndexOf(currstate);
                                break;
                            }
                        }
                    }
                    else
                    {
                        state.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Error checking zip code.", ex);
            }
        }

        private void dateOfBirth_Leave(object sender, EventArgs e)
        {
            if (pwnapp_dateofbirth.isValid)
            {
                checkDateOfBirth();
            }
            else
            {
                this.ageTextbox.Text = "0";
            }
        }

        /// <summary>
        /// If the form data is not valid do not allow closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManagePawnApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !isFormValid;
        }

        private void ManagePawnApplication_Shown(object sender, EventArgs e)
        {

            //TODO: Deal with trigger based message context
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.VIEWPAWNCUSTPRODDETAILS)
            {
                MessageBox.Show(Commons.GetMessageString("ViewCustProdDetailsFirearmMsg"));
            }
            //this.NavControlBox.IsCustom = true;
            //this.NavControlBox.CustomDetail = Commons.GetMessageString("ViewCustProdDetailsFirearmMsg");
            //this.NavControlBox.Action = NavBox.NavAction.MSGBOX;
            //TODO: End of trigger
        }

        protected override System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                //call the button click event directly
                this.managePawnCancelButton_Click(null, new EventArgs());
                //return true to indicate that the key has been handled
                return true;
            }
            if (keyData == Keys.Enter)
            {
                if (this.ActiveControl.TabIndex != this.customButtonSubmit.TabIndex)
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                }
                else
                {
                    this.managePawnSubmitButton_Click(null, new EventArgs());
                }

                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void pwnapp_identificationnumber_Leave(object sender, EventArgs e)
        {
            //To Do: When all the rules around the different states is established on
            //ID number this has to change to be obtained from business rules
            idNumberError = false;
            if (strIdentTypeCode.Equals(CustomerIdTypes.DRIVERLIC.ToString()) && strIdentIssuer.Equals("TX"))
            {
                if (pwnapp_identificationnumber.Text.Length < 8)
                {
                    MessageBox.Show(Commons.GetMessageString("InvalidTXLicense"));
                    isFormValid = false;
                    idNumberError = true;
                }
            }

            if (updateRequiredFields)
            {
                SetRequiredFieldsInForm();
            }
        }

        /// <summary>
        /// Method to display flashing customer commnet
        /// </summary>
        private void flash_CustCommnet(object sender, EventArgs e)
        {
            if (custCommentAlert.Visible == true)
            {
                custCommentAlert.Visible = false;
            }
            else
            {
                custCommentAlert.Visible = true;
            }
        }

        //customer comments view BZ # 387
        private void linkLabelCommentsView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UpdateCommentsandNotes commentsForm = new UpdateCommentsandNotes
            {
                ViewCommentsAndNotes = true,
                CustToEdit = FormCustomer
            };
            commentsForm.ShowDialog(this);
        }

        private void phoneData1_Leave(object sender, EventArgs e)
        {
            if (updateRequiredFields)
            {
                SetRequiredFieldsInForm();
            }
        }
    }
}
