/********************************************************************
* CashlinxDesktop
* UpdateAddress
* This form is shown a customer's address needs to be updated
* Sreelatha Rengarajan 5/15/2009 Initial version
*******************************************************************/

using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.UserControls;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Customer
{
    public partial class UpdateAddress : Form
    {

        private bool _physicalAddressChange = false;
        private bool _mailingAddressChange = false;
        private bool _workAddressChange = false;
        private bool _allAddressesChange = false;
        
        private CustomerVO _custAddrToView;

        private readonly string strUserId = "";
        private string _errorCode = "";
        private string _errorMsg = "";
        //Physical Address data
        private string _strPhyAddr = "";
        private string _strPhyUnit = "";
        private string _strPhyPostalCode = "";
        private string _strPhyState = "";
        private string _strPhyCountry = "";
        private string _strPhyCountryName = "";
        private string _strPhyNotes = "";
        private string _strPhyCity = "";
        //Work Address data
        private string _strWorkAddr = "";
        private string _strWorkUnit = "";
        private string _strWorkPostalCode = "";
        private string _strWorkState = "";
        private string _strWorkCountry = "";
        private string _strWorkCountryName = "";
        private string _strWorkNotes = "";
        private string _strWorkCity = "";
        //Mailing Address data
        private string _strMailAddr = "";
        private string _strMailUnit = "";
        private string _strMailPostalCode = "";
        private string _strMailState = "";
        private string _strMailCountry = "";
        private string _strMailCountryName = "";
        private string _strMailNotes = "";
        private string _strMailCity = "";
        //Additional Address data
        private string _strAddlAddr = "";
        private string _strAddlUnit = "";
        private string _strAddlPostalCode = "";
        private string _strAddlState = "";
        private string _strAddlCountry = "";
        private string _strAddlCountryName = "";
        private string _strAddlNotes = "";
        private string _strAddlAltString = "";
        private string _strAddlCity = "";

        private int _numberOfAddresses = 0;
        private bool _physicalAddrEntered = false;
        private bool _mailAddrEntered = false;
        private bool _workAddrEntered = false;
        private bool _addlAddrEntered = false;

        private readonly string _strStoreNumber = "";
        private readonly string _strStoreState = "";

        //Array of address data
        string[] _strAddr;
        string[] _strCity;
        string[] _strState;
        string[] _strZip;
        string[] _strUnit;
        string[] _strCountry;
        string[] _strAddrNotes;
        string[] _strAddlString;
        string[] _strAddrType;


        Form ownerFrm;
        public NavBox NavControlBox;


        //Set by the calling program to indicate that the physical address is being edited
        public bool PhysicalAddress
        {
            set
            {
                _physicalAddressChange = value;
            }
        }

        //Set by the calling program to indicate that the mailing address is being edited
        public bool MailingAddress
        {
            set
            {
                _mailingAddressChange = value;
            }
        }

        //Set by the calling program to indicate that the work address is  being edited
        public bool WorkAddress
        {
            set
            {
                _workAddressChange = value;
            }
        }

        //Customer object of the customer whose addresss is being edited
        public CustomerVO CustAddrToView
        {
            set
            {
                _custAddrToView = value;
            }
            
        }

        public UpdateAddress()
        {
            InitializeComponent();
            NavControlBox = new NavBox();
            strUserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
            _strStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            _strStoreState = GlobalDataAccessor.Instance.CurrentSiteId.State;

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Form currForm = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup(this);
            if (currForm.GetType() == typeof(UpdateAddress))
            {
                var dgr = MessageBox.Show(Commons.GetMessageString("CancelConfirmMessage"), "Warning", MessageBoxButtons.YesNo);
                if (dgr == DialogResult.Yes)
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Desktop();
                else
                    return;

            }
            else
            {
                this.Close();
                this.Dispose(true);
            }

        }

        private void checkBoxMailingSameAsPhysical_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMailingSameAsPhysical.Checked)
            {
                EnableMailingAddressControls();
                this.custMailingAddr1.Text = this.custPhysicalAddr1.Text;
                this.custMailingAddr2.Text = this.custPhysicalAddr2.Text;
                this.custMailingAddrCity.Text = this.custPhysicalAddrCity.Text;
                this.custMailingAddrCountry.Text = this.custPhysicalAddrCountry.Text;
                this.custMailingAddrNotes.Text = this.custPhysicalAddrNotes.Text;
                this.custMailingAddrUnit.Text = this.custPhysicalAddrUnit.Text;
                ComboBox custState = (ComboBox)this.custPhysicalAddrState.Controls[0];
                string strPhysState = custState.Text;
                ComboBox custMailState = (ComboBox)this.custMailingAddrState.Controls[0];
                foreach (USState currstate in custMailState.Items)
                    if (currstate.LongName == strPhysState)
                    {
                        custMailState.SelectedIndex = custMailState.Items.IndexOf(currstate);
                        break;
                    }
                custMailState = null;
                custState = null;
                string custPhysZipcode = this.custPhysicalAddrZipcode.Controls[0].Text;
                this.custMailingAddrZipcode.Controls[0].Text = custPhysZipcode;

            }
            else
            {
                this.custMailingAddr1.Text = "";
                this.custMailingAddr2.Text = "";
                this.custMailingAddrCity.Text = "";
                this.custMailingAddrCountry.Text = "";
                this.custMailingAddrNotes.Text = "";
                this.custMailingAddrUnit.Text = "";
                ComboBox custMailState = (ComboBox)this.custMailingAddrState.Controls[0];
                custMailState.SelectedIndex = -1;
                this.custMailingAddrZipcode.Controls[0].Text = "";
            }

            this.custMailingAddrZipcode.checkValid();
            this.tableLayoutPanel2.Invalidate(true);
        }


        private void DisablePhysicalAddressControls()
        {
            this.custPhysicalAddr1.Enabled = false;
            this.custPhysicalAddr2.Enabled = false;
            this.custPhysicalAddrCity.Enabled = false;
            this.custPhysicalAddrCountry.Enabled = false;
            this.custPhysicalAddrNotes.Enabled = false;
            this.custPhysicalAddrState.Enabled = false;
            this.custPhysicalAddrUnit.Enabled = false;
            this.custPhysicalAddrZipcode.Enabled = false;
        }

        private void EnablePhysicalAddressControls()
        {
            this.custPhysicalAddr1.Enabled = true;
            this.custPhysicalAddr2.Enabled = true;
            this.custPhysicalAddrCity.Enabled = true;
            this.custPhysicalAddrCountry.Enabled = true;
            this.custPhysicalAddrNotes.Enabled = true;
            this.custPhysicalAddrState.Enabled = true;
            this.custPhysicalAddrUnit.Enabled = true;
            this.custPhysicalAddrZipcode.Enabled = true;
        }

        private void DisableMailingAddressControls()
        {
            this.custMailingAddr1.Enabled = false;
            this.custMailingAddr2.Enabled = false;
            this.custMailingAddrCity.Enabled = false;
            this.custMailingAddrCountry.Enabled = false;
            this.custMailingAddrNotes.Enabled = false;
            this.custMailingAddrState.Enabled = false;
            this.custMailingAddrUnit.Enabled = false;
            this.custMailingAddrZipcode.Enabled = false;
            this.checkBoxMailingSameAsPhysical.Enabled = false;
        }

        private void DisableAdditionalAddressControls()
        {
            this.custAddlAddr1.Enabled = false;
            this.custAddlAddr2.Enabled = false;
            this.custAddlAddrAlternateString.Enabled = false;
            this.custAddlAddrCity.Enabled = false;
            this.custAddlAddrCountry.Enabled = false;
            this.custAddlAddrNotes.Enabled = false;
            this.custAddlAddrState.Enabled = false;
            this.custAddlAddrUnit.Enabled = false;
            this.custAddlAddrZipcode.Enabled = false;
            
        }

        private void EnableMailingAddressControls()
        {
            this.custMailingAddr1.Enabled = true;
            this.custMailingAddr2.Enabled = true;
            this.custMailingAddrCity.Enabled = true;
            this.custMailingAddrCountry.Enabled = true;
            this.custMailingAddrNotes.Enabled = true;
            this.custMailingAddrState.Enabled = true;
            this.custMailingAddrUnit.Enabled = true;
            this.custMailingAddrZipcode.Enabled = true;
            this.checkBoxMailingSameAsPhysical.Enabled = true;
        }

        private void DisableWorkAddressControls()
        {
            this.custWorkAddr1.Enabled = false;
            this.custWorkAddr2.Enabled = false;
            this.custWorkAddrCity.Enabled = false;
            this.custWorkAddrCountry.Enabled = false;
            this.custWorkAddrNotes.Enabled = false;
            this.custWorkAddrState.Enabled = false;
            this.custWorkAddrUnit.Enabled = false;
            this.custWorkAddrZipcode.Enabled = false;
        }

        private void EnableWorkAddressControls()
        {
            this.custWorkAddr1.Enabled = true;
            this.custWorkAddr2.Enabled = true;
            this.custWorkAddrCity.Enabled = true;
            this.custWorkAddrCountry.Enabled = true;
            this.custWorkAddrNotes.Enabled = true;
            this.custWorkAddrState.Enabled = true;
            this.custWorkAddrUnit.Enabled = true;
            this.custWorkAddrZipcode.Enabled = true;
        }

        private void EnableAdditionalAddressControls()
        {
            this.custAddlAddr1.Enabled = true;
            this.custAddlAddr2.Enabled = true;
            this.custAddlAddrCity.Enabled = true;
            this.custAddlAddrCountry.Enabled = true;
            this.custAddlAddrNotes.Enabled = true;
            this.custAddlAddrState.Enabled = true;
            this.custAddlAddrUnit.Enabled = true;
            this.custAddlAddrZipcode.Enabled = true;
            this.custAddlAddrAlternateString.Enabled = true;
        }

        private void UpdateAddress_Load(object sender, EventArgs e)
        {
            ownerFrm = this.Owner;
            NavControlBox.Owner = this;
            if (_custAddrToView == null)
            {
                _custAddrToView = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
                if (_custAddrToView != null)
                {
                    _allAddressesChange = true;
                    addressFormLabel.Text = "Customer Address";
                    this.customButtonReset.Text = "Clear";
                    removeRequiredCriteriaOnFields();
                    makeFieldsReadOnly();
                }
                else
                {
                    BasicExceptionHandler.Instance.AddException("Active customer object not found in session", new ApplicationException());
                    //CashlinxDesktop.Desktop.CashlinxDesktopSession.Instance.HistorySession.Desktop();
                    NavControlBox.Action = NavBox.NavAction.CANCEL;
                }

            }
            LoadDataInControls();
        }

        private void makeFieldsReadOnly()
        {
            //set all fields except addr line 1 as read only
            //They will be reenabled only when addr line 1 has values
            this.custPhysicalAddr2.ReadOnly = true;
            this.custPhysicalAddrCity.ReadOnly = true;
            this.custPhysicalAddrState.Controls[0].Enabled = false;
            this.custPhysicalAddrUnit.ReadOnly = true;
            this.custPhysicalAddrZipcode.Enabled = false;
            this.custPhysicalAddrNotes.ReadOnly = true;
            this.custPhysicalAddrCountry.Enabled = false;

            this.custMailingAddr2.ReadOnly = true;
            this.custMailingAddrCity.ReadOnly = true;
            this.custMailingAddrState.Controls[0].Enabled = false;
            this.custMailingAddrUnit.ReadOnly = true;
            this.custMailingAddrZipcode.Enabled = false;
            this.custMailingAddrNotes.ReadOnly = true;
            this.custMailingAddrCountry.Enabled = false;

            this.custWorkAddr2.ReadOnly = true;
            this.custWorkAddrCity.ReadOnly = true;
            this.custWorkAddrState.Controls[0].Enabled = false;
            this.custWorkAddrUnit.ReadOnly = true;
            this.custWorkAddrZipcode.Enabled = false;
            this.custWorkAddrNotes.ReadOnly = true;
            this.custWorkAddrCountry.Enabled = false;

            this.custAddlAddr2.ReadOnly = true;
            this.custAddlAddrAlternateString.ReadOnly = true;
            this.custAddlAddrCity.ReadOnly = true;
            this.custAddlAddrState.Controls[0].Enabled = false;
            this.custAddlAddrUnit.ReadOnly = true;
            this.custAddlAddrZipcode.Enabled = false;
            this.custAddlAddrNotes.ReadOnly = true;
            this.custAddlAddrCountry.Enabled = false;


        }

        private void removeRequiredCriteriaOnFields()
        {
            this.customLabelPhysicalAddr.Required = false;
            this.customLabelPhysicalAddrCity.Required = false;
            this.customLabelPhysicalAddrZip.Required = false;
            this.customLabelPhysAddrState.Required = false;
            this.customLabelPhysAddrCountry.Required = false;
            this.customLabelWorkAddr.Required = false;
            this.customLabelWorkAddrCity.Required = false;
            this.customLabelWorkAddrZip.Required = false;
            this.customLabelWorkAddrState.Required = false;
            this.customLabelWorkAddrCountry.Required = false;
            this.customLabelMailingAddr.Required = false;
            this.customLabelMailingAddrCity.Required = false;
            this.customLabelMailingAddrZip.Required = false;
            this.customLabelMailingAddrState.Required = false;
            this.customLabelMailingAddrCountry.Required = false;

        }

        private void LoadDataInControls()
        {

            if (_custAddrToView != null)
            {

                //get physical/home address
                AddressVO custPhysicalAddr = _custAddrToView.getHomeAddress();
                //get work address
                AddressVO custWorkAddr = _custAddrToView.getWorkAddress();
                //get mailing address
                AddressVO custMailAddr = _custAddrToView.getMailingAddress();
                //get additional address
                AddressVO custAddlAddr = _custAddrToView.getAdditionalAddress();

                //Show physical address
                if (custPhysicalAddr != null)
                {
                    string customerAddress = custPhysicalAddr.Address1;
                    if (customerAddress.Length > 40)
                    {
                        this.custPhysicalAddr1.Text = customerAddress.Substring(0, 40);
                        this.custPhysicalAddr2.Text = customerAddress.Substring(41, customerAddress.Length - 41);
                    }
                    else
                    {
                        this.custPhysicalAddr1.Text = customerAddress;
                        this.custPhysicalAddr2.Text = custPhysicalAddr.Address2;
                    }

                    this.custPhysicalAddrCity.Text = custPhysicalAddr.City;
                    this.custPhysicalAddrNotes.Text = custPhysicalAddr.Notes;
                    this.custPhysicalAddrUnit.Text = custPhysicalAddr.UnitNum;
                    this.custPhysicalAddrZipcode.Controls[0].Text = custPhysicalAddr.ZipCode;
                    //Set State
                    ComboBox custstate = (ComboBox)this.custPhysicalAddrState.Controls[0];
                    foreach (USState currstate in custstate.Items)
                        if (currstate.ShortName == custPhysicalAddr.State_Code)
                        {
                            custstate.SelectedIndex = custstate.Items.IndexOf(currstate);
                            break;
                        }
                    custstate = null;
                    //Set country
                    custPhysicalAddrCountry.SelectedIndex = custPhysicalAddrCountry.Items.IndexOf(custPhysicalAddr.Country_Name);
                }
                //Show work address
                if (custWorkAddr != null)
                {
                    string customerAddress = custWorkAddr.Address1;
                    if (customerAddress.Length > 40)
                    {
                        this.custWorkAddr1.Text = customerAddress.Substring(0, 40);
                        this.custWorkAddr2.Text = customerAddress.Substring(41, customerAddress.Length - 41);
                    }
                    else
                    {
                        this.custWorkAddr1.Text = customerAddress;
                        this.custWorkAddr2.Text = custWorkAddr.Address2;
                    }
                    this.custWorkAddrCity.Text = custWorkAddr.City;
                    this.custWorkAddrNotes.Text = custWorkAddr.Notes;
                    this.custWorkAddrUnit.Text = custWorkAddr.UnitNum;
                    this.custWorkAddrZipcode.Controls[0].Text = custWorkAddr.ZipCode;
                    //Set State
                    ComboBox custstate = (ComboBox)this.custWorkAddrState.Controls[0];
                    foreach (USState currstate in custstate.Items)
                        if (currstate.ShortName == custWorkAddr.State_Code)
                        {
                            custstate.SelectedIndex = custstate.Items.IndexOf(currstate);
                            break;
                        }
                    custstate = null;
                    //Set country
                    custWorkAddrCountry.SelectedIndex = custWorkAddrCountry.Items.IndexOf(custWorkAddr.Country_Name);
                }
                //Show mailing address
                if (custMailAddr != null)
                {
                    string customerAddress = custMailAddr.Address1;
                    if (customerAddress.Length > 40)
                    {
                        this.custMailingAddr1.Text = customerAddress.Substring(0, 40);
                        this.custMailingAddr2.Text = customerAddress.Substring(41, customerAddress.Length - 41);
                    }
                    else
                    {
                        this.custMailingAddr1.Text = customerAddress;
                        this.custMailingAddr2.Text = custMailAddr.Address2;
                    }

                    this.custMailingAddrCity.Text = custMailAddr.City;
                    this.custMailingAddrNotes.Text = custMailAddr.Notes;
                    this.custMailingAddrUnit.Text = custMailAddr.UnitNum;
                    this.custMailingAddrZipcode.Controls[0].Text = custMailAddr.ZipCode;
                    //Set State
                    ComboBox custstate = (ComboBox)this.custMailingAddrState.Controls[0];
                    foreach (USState currstate in custstate.Items)
                        if (currstate.ShortName == custMailAddr.State_Code)
                        {
                            custstate.SelectedIndex = custstate.Items.IndexOf(currstate);
                            break;
                        }
                    custstate = null;
                    //Set country
                    custMailingAddrCountry.SelectedIndex = custMailingAddrCountry.Items.IndexOf(custMailAddr.Country_Name);
                }
                //Show Additional Address
                if (custAddlAddr != null)
                {
                    this.custAddlAddr1.Text = custAddlAddr.Address1;
                    this.custAddlAddr2.Text = custAddlAddr.Address2;
                    this.custAddlAddrCity.Text = custAddlAddr.City;
                    this.custAddlAddrNotes.Text = custAddlAddr.Notes;
                    this.custAddlAddrUnit.Text = custAddlAddr.UnitNum;
                    this.custAddlAddrAlternateString.Text = custAddlAddr.AlternateString;
                    this.custAddlAddrZipcode.Controls[0].Text = custAddlAddr.ZipCode;
                    //Set State
                    ComboBox custstate = (ComboBox)this.custAddlAddrState.Controls[0];
                    foreach (USState currstate in custstate.Items)
                        if (currstate.ShortName == custAddlAddr.State_Code)
                        {
                            custstate.SelectedIndex = custstate.Items.IndexOf(currstate);
                            break;
                        }
                    custstate = null;
                    //Set country
                    custAddlAddrCountry.SelectedIndex = custAddlAddrCountry.Items.IndexOf(custAddlAddr.Country_Name);
                }
                if (_allAddressesChange)
                {
                    //Set the states for all the addresses to be the store state
                    //Set state for physical Address
                    ComboBox custstate = (ComboBox)this.custPhysicalAddrState.Controls[0];
                    foreach (USState currstate in custstate.Items)
                        if (currstate.ShortName == _strStoreState)
                        {
                            custstate.SelectedIndex = custstate.Items.IndexOf(currstate);
                            break;
                        }
                    //set mailing address state
                    custstate = (ComboBox)this.custMailingAddrState.Controls[0];
                    foreach (USState currstate in custstate.Items)
                        if (currstate.ShortName == _strStoreState)
                        {
                            custstate.SelectedIndex = custstate.Items.IndexOf(currstate);
                            break;
                        }
                    //set work address state
                    custstate = (ComboBox)this.custWorkAddrState.Controls[0];
                    foreach (USState currstate in custstate.Items)
                        if (currstate.ShortName == _strStoreState)
                        {
                            custstate.SelectedIndex = custstate.Items.IndexOf(currstate);
                            break;
                        }
                    //Set additional address state
                    custstate = (ComboBox)this.custAddlAddrState.Controls[0];
                    foreach (USState currstate in custstate.Items)
                        if (currstate.ShortName == _strStoreState)
                        {
                            custstate.SelectedIndex = custstate.Items.IndexOf(currstate);
                            break;
                        }
                    custstate = null;

                    EnableAdditionalAddressControls();
                    EnablePhysicalAddressControls();
                    EnableWorkAddressControls();
                    EnableMailingAddressControls();
                    this.tableLayoutPanel1.Enabled = true;
                    this.tableLayoutPanel2.Enabled = true;
                    customButtonSubmit.Text = "Save";
                    customButtonContinue.Visible = true;
                    customButtonBack.Visible = true;
                }

                else if (_mailingAddressChange)
                {
                    this.tableLayoutPanel1.Enabled = false;
                    DisableAdditionalAddressControls();
                    this.tableLayoutPanel2.Controls["custMailingAddr1"].Focus();
                }
                else if (_physicalAddressChange)
                {
                    DisableWorkAddressControls();
                    this.tableLayoutPanel2.Enabled = false;
                    this.tableLayoutPanel1.Controls["custPhysicalAddr1"].Focus();
                }
                else if (_workAddressChange)
                {
                    DisablePhysicalAddressControls();
                    this.tableLayoutPanel2.Enabled = false;
                    this.tableLayoutPanel1.Controls["custWorkAddr1"].Focus();
                }
            }

        }

  

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (customButtonReset.Text == "Clear")
            {
                ClearPhysicalAddressData();
                ClearMailingAddressData();
                ClearAdditionlAddressData();
                ClearWorkAddressData();
            }
            else
                LoadDataInControls();
        }

        private void custPhysicalAddrZipcode_Leave(object sender, EventArgs e)
        {
            FillCityandState(custPhysicalAddrState, custPhysicalAddrCity, custPhysicalAddrZipcode, custPhysicalAddrCountry);
        }


        private void FillCityandState(Control ctrlState, Control ctrlCity, Zipcode ctrlZipcode, ComboBox ctrlCountry)
        {
            ComboBox state = (ComboBox)ctrlState.Controls[0];
            if (ctrlZipcode.City != null && ctrlZipcode.State != null)
            {
                if (ctrlZipcode.City.Length > 0 && ctrlZipcode.State.Length > 0)
                {
                    ctrlCity.Text = ctrlZipcode.City;
                    foreach (USState currstate in state.Items)
                        if (currstate.ShortName == ctrlZipcode.State)
                        {
                            state.SelectedIndex = state.Items.IndexOf(currstate);
                            break;
                        }
                }
                else
                {
                    ctrlCity.Text = "";
                    state.SelectedIndex = -1;
                }
            }
            else
            {
                ctrlCity.Text = "";
                state.SelectedIndex = -1;
            }
            //Default country to united states
            ctrlCountry.SelectedIndex = ctrlCountry.Items.IndexOf("United States");


        }

        private void custWorkAddrZipcode_Leave(object sender, EventArgs e)
        {
            FillCityandState(custWorkAddrState, custWorkAddrCity, custWorkAddrZipcode, custWorkAddrCountry);
        }

        private void custMailingAddrZipcode_Leave(object sender, EventArgs e)
        {
            FillCityandState(custMailingAddrState, custMailingAddrCity, custMailingAddrZipcode, custMailingAddrCountry);
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            bool retVal = false;
            DialogResult dgr = DialogResult.Retry;
            if (isFormValid())
            {
                try
                {
                    //Call update address with physical address fields
                    if (_physicalAddressChange)
                    {
                        getPhysicalAddressData();
                        if (_physicalAddrEntered && physicalAddrChanged())
                        {
                            do
                            {
                                retVal = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).UpdateAddress(_strAddrType, _strAddr, _strUnit, _strCity, _strZip, _strState, _strCountry, _strAddrNotes, _strAddlString, _custAddrToView.CustomerNumber, _custAddrToView.PartyId, strUserId, out _errorCode, out _errorMsg);
                                if (retVal)
                                {
                                    MessageBox.Show("Customer Physical Address successfully updated");
                                    _custAddrToView.updateHomeAddress(custPhysicalAddr1.Text, custPhysicalAddr2.Text, _strPhyUnit, _strPhyCity, _strPhyPostalCode, _strPhyCountryName, _strPhyState, _strPhyNotes);
                                    var ownerForm = this.Owner;
                                    if (ownerForm.GetType() == typeof(ViewCustomerInformation))
                                    {
                                        ((ViewCustomerInformation)ownerForm).UpdatedCustomerToView = _custAddrToView;
                                        ((ViewCustomerInformation)ownerForm).ShowUpdates = true;
                                    }
                                    break;
                                }
                                else
                                {
                                    dgr = MessageBox.Show(Commons.GetMessageString("CustPhysicalAddrUpdateFail"), "Error", MessageBoxButtons.RetryCancel);
                                }
                            } while (dgr == DialogResult.Retry);
                        }
                        else
                            MessageBox.Show(Commons.GetMessageString("NoChangesInForm"));

                    }
                    if (_mailingAddressChange)
                    {
                        getMailingAddressData();
                        if (_mailAddrEntered && mailingAddrChanged())
                        {
                            do
                            {
                                retVal = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).UpdateAddress(_strAddrType, _strAddr, _strUnit, _strCity, _strZip, _strState, _strCountry, _strAddrNotes, _strAddlString, _custAddrToView.CustomerNumber, _custAddrToView.PartyId, strUserId, out _errorCode, out _errorMsg);
                                if (retVal)
                                {
                                    MessageBox.Show("Customer Mailing Address successfully updated");
                                    _custAddrToView.updateMailingAddress(custMailingAddr1.Text, custMailingAddr2.Text, _strMailUnit, _strMailCity, _strMailPostalCode, _strMailCountryName, _strMailState, _strMailNotes);
                                    Form ownerForm = this.Owner;
                                    if (ownerForm.GetType() == typeof(ViewCustomerInformation))
                                    {
                                        ((ViewCustomerInformation)ownerForm).UpdatedCustomerToView = _custAddrToView;
                                        ((ViewCustomerInformation)ownerForm).ShowUpdates = true;
                                    }
                                    break;

                                }
                                else
                                {
                                    dgr = MessageBox.Show(Commons.GetMessageString("CustMailingAddrUpdateFail"), "Error", MessageBoxButtons.RetryCancel);
                                }

                            } while (dgr == DialogResult.Retry);

                        }
                        else
                            MessageBox.Show(Commons.GetMessageString("NoChangesInForm"));


                    }
                    if (_workAddressChange)
                    {
                        getWorkAddressData();
                        if (_workAddrEntered && workAddrChanged())
                        {
                            do
                            {
                                retVal = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).UpdateAddress(_strAddrType, _strAddr, _strUnit, _strCity, _strZip, _strState, _strCountry, _strAddrNotes, _strAddlString, _custAddrToView.CustomerNumber, _custAddrToView.PartyId, strUserId, out _errorCode, out _errorMsg);
                                if (retVal)
                                {
                                    MessageBox.Show("Customer Work Address successfully updated");
                                    _custAddrToView.updateWorkAddress(custWorkAddr1.Text, custWorkAddr2.Text, _strWorkUnit, _strWorkCity, _strWorkPostalCode, _strWorkCountryName, _strWorkState, _strWorkNotes);
                                    Form ownerForm = this.Owner;
                                    if (ownerForm.GetType() == typeof(ViewCustomerInformation))
                                    {
                                        ((ViewCustomerInformation)ownerForm).UpdatedCustomerToView = _custAddrToView;
                                        ((ViewCustomerInformation)ownerForm).ShowUpdates = true;
                                    }
                                    break;
                                }
                                else
                                {
                                    dgr = MessageBox.Show(Commons.GetMessageString("CustWorkAddrUpdateFail"), "Error", MessageBoxButtons.RetryCancel);
                                }

                            } while (dgr == DialogResult.Retry);
                        }
                        else
                            MessageBox.Show(Commons.GetMessageString("NoChangesInForm"));


                    }
                    if (_allAddressesChange)
                    {
                        //Add the customer first
                        
                        if (_custAddrToView != null)
                        {
                            var partyId = string.Empty;
                            var custNumber = string.Empty;
                            var errorDesc = string.Empty;
                            do
                            {
                                retVal = CustomerProcedures.AddCustomer(GlobalDataAccessor.Instance.DesktopSession, _custAddrToView, strUserId, _strStoreNumber, out custNumber, out partyId, out errorDesc);
                                if (retVal)
                                    break;
                                else
                                {
                                    dgr = MessageBox.Show(Commons.GetMessageString("CustDataAddFailure"), "Error", MessageBoxButtons.RetryCancel);
                                }

                            } while (dgr == DialogResult.Retry);
                            if (retVal)
                            {
                                _custAddrToView.CustomerNumber = custNumber;
                                _custAddrToView.PartyId = partyId;
                                addAddressDataForCustomer();

                                    do
                                    {
                                        retVal = CustomerProcedures.AddCustomerAddress(GlobalDataAccessor.Instance.DesktopSession, _custAddrToView, strUserId, _strStoreNumber);
                                        if (retVal)
                                        {

                                            break;
                                        }
                                        else
                                        {
                                            dgr = MessageBox.Show(Commons.GetMessageString("CustDataAddFailure"), "Error", MessageBoxButtons.RetryCancel);
                                        }

                                    } while (dgr == DialogResult.Retry);

                                if (retVal)
                                {

                                    MessageBox.Show(Commons.GetMessageString("CustCreationSuccess") + " " + custNumber);
                                    CustomerVO customerObj = CustomerProcedures.getCustomerDataByCustomerNumber(GlobalDataAccessor.Instance.DesktopSession, custNumber);
                                    if (customerObj != null)
                                    {
                                        GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = customerObj;
                                    }
                                    //CashlinxDesktopSession.Instance.FormState = CashlinxDesktopSession.CustomerFormStates.ADDCUSTOMERCOMPLETE;
                                    //CustomerController.NavigateUser(ownerFrm);
                                    NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                                }
                                else
                                {
                                    //Adding customer address was a failure and the user pressed cancel when asked
                                    //if they would like to retry so take them to desktop
                                    //CashlinxDesktopSession.Instance.FormState = CashlinxDesktopSession.CustomerFormStates.CANCEL;
                                    //CustomerController.NavigateUser(ownerFrm);
                                    NavControlBox.Action = NavBox.NavAction.CANCEL;
                                }
                            }
                            else
                            {
                                //the call to add customer failed and the user pressed cancel when asked
                                //if they would like to keep trying so go to desktop
                                //CashlinxDesktopSession.Instance.FormState = CashlinxDesktopSession.CustomerFormStates.CANCEL;
                                //CustomerController.NavigateUser(ownerFrm);
                                NavControlBox.Action = NavBox.NavAction.CANCEL;

                            }


                        }
                        else
                        {
                            BasicExceptionHandler.Instance.AddException("Customer object does not exist in session", new ApplicationException());
                            //CashlinxDesktopSession.Instance.FormState = CashlinxDesktopSession.CustomerFormStates.CANCEL;
                            //CustomerController.NavigateUser(ownerFrm);
                            NavControlBox.Action = NavBox.NavAction.CANCEL;

                        }

                    }


                }
                catch (Exception ex)
                {
                    BasicExceptionHandler.Instance.AddException(ex.Message, new ApplicationException());

                }
                finally
                {

                    this.Close();
                    this.Dispose(true);
                }
            }
            else
            {
                MessageBox.Show(Commons.GetMessageString("FormRequiredFieldsFilledError"));
                this.Invalidate(true);
            }
        }

        private void gatherAllAddressData()
        {
            _numberOfAddresses = 0;
            getPhysicalAddressData();
            getMailingAddressData();
            getWorkAddressData();
            getAdditionalAddressData();
            if (_numberOfAddresses > 0)
            {
                _strAddr = new string[_numberOfAddresses];
                _strCity = new string[_numberOfAddresses];
                _strState = new string[_numberOfAddresses];
                _strCountry = new string[_numberOfAddresses];
                _strAddrType = new string[_numberOfAddresses];
                _strAddrNotes = new string[_numberOfAddresses];
                _strAddlString = new string[_numberOfAddresses];
                _strZip = new string[_numberOfAddresses];
                _strUnit = new string[_numberOfAddresses];
                int i = 0;
                if (_physicalAddrEntered)
                {
                    _strAddr[i] = _strPhyAddr;
                    _strCity[i] = _strPhyCity;
                    _strState[i] = _strPhyState;
                    _strCountry[i] = _strPhyCountry;
                    _strAddrType[i] = CustomerAddressTypes.HOME_ADDRESS;
                    _strAddrNotes[i] = _strPhyNotes;
                    _strAddlString[i] = "";
                    _strZip[i] = _strPhyPostalCode;
                    _strUnit[i] = _strPhyUnit;
                    i++;
                }
                if (_mailAddrEntered)
                {
                    _strAddr[i] = _strMailAddr;
                    _strCity[i] = _strMailCity;
                    _strState[i] = _strMailState;
                    _strCountry[i] = _strMailCountry;
                    _strAddrType[i] = CustomerAddressTypes.MAILING_ADDRESS;
                    _strAddrNotes[i] = _strMailNotes;
                    _strAddlString[i] = "";
                    _strZip[i] = _strMailPostalCode;
                    _strUnit[i] = _strMailUnit;
                    i++;
                }
                if (_workAddrEntered)
                {
                    _strAddr[i] = _strWorkAddr;
                    _strCity[i] = _strWorkCity;
                    _strState[i] = _strWorkState;
                    _strCountry[i] = _strWorkCountry;
                    _strAddrType[i] = CustomerAddressTypes.WORK_ADDRESS;
                    _strAddrNotes[i] = _strWorkNotes;
                    _strAddlString[i] = "";
                    _strZip[i] = _strWorkPostalCode;
                    _strUnit[i] = _strWorkUnit;
                    i++;
                }
                if (_addlAddrEntered)
                {
                    _strAddr[i] = _strAddlAddr;
                    _strCity[i] = _strAddlCity;
                    _strState[i] = _strAddlState;
                    _strCountry[i] = _strAddlCountry;
                    _strAddrType[i] = CustomerAddressTypes.ADDITIONAL_ADDRESS;
                    _strAddrNotes[i] = _strAddlNotes;
                    _strAddlString[i] = _strAddlAltString;
                    _strZip[i] = _strAddlPostalCode;
                    _strUnit[i] = _strAddlUnit;
                }
            }

        }

        private bool workAddrChanged()
        {
            bool addrChanged = false;
            //get work address of the customer
            AddressVO custWorkAddr = _custAddrToView.getWorkAddress();
            if (custWorkAddr != null)
            {
                if (custWorkAddr.getCombinedAddress() != _strWorkAddr ||
                    custWorkAddr.City != _strWorkCity || custWorkAddr.Country_Code != _strWorkCountry ||
                    custWorkAddr.ZipCode != _strWorkPostalCode || custWorkAddr.UnitNum != _strWorkUnit ||
                    custWorkAddr.State_Code != _strWorkState || custWorkAddr.Notes != _strWorkNotes)
                {
                    addrChanged = true;
                }
                else
                    addrChanged = false;
            }
            else
                addrChanged = true;
            if (addrChanged)
            {
                _strAddr = new string[1];
                _strCity = new string[1];
                _strState = new string[1];
                _strCountry = new string[1];
                _strAddrType = new string[1];
                _strAddrNotes = new string[1];
                _strAddlString = new string[1];
                _strZip = new string[1];
                _strUnit = new string[1];
                _strAddr[0] = _strWorkAddr;
                _strCity[0] = _strWorkCity;
                _strState[0] = _strWorkState;
                _strCountry[0] = _strWorkCountry;
                _strAddrType[0] = CustomerAddressTypes.WORK_ADDRESS;
                _strAddrNotes[0] = _strWorkNotes;
                _strAddlString[0] = "";
                _strZip[0] = _strWorkPostalCode;
                _strUnit[0] = _strWorkUnit;

            }

            return addrChanged;
        }

        private bool mailingAddrChanged()
        {
            bool addrChanged = false;
            //get mailing address of the customer
            AddressVO custMailAddr = _custAddrToView.getMailingAddress();
            if (custMailAddr != null)
            {
                if (custMailAddr.getCombinedAddress() != _strMailAddr ||
                    custMailAddr.City != _strMailCity || custMailAddr.Country_Code != _strMailCountry ||
                    custMailAddr.ZipCode != _strMailPostalCode || custMailAddr.UnitNum != _strMailUnit ||
                    custMailAddr.State_Code != _strMailState || custMailAddr.Notes != _strMailNotes)
                {
                    addrChanged = true;
                }
                else
                    addrChanged = false;
            }
            else
                addrChanged = true;

            if (addrChanged)
            {
                _strAddr = new string[1];
                _strCity = new string[1];
                _strState = new string[1];
                _strCountry = new string[1];
                _strAddrType = new string[1];
                _strAddrNotes = new string[1];
                _strAddlString = new string[1];
                _strZip = new string[1];
                _strUnit = new string[1];
                _strAddr[0] = _strMailAddr;
                _strCity[0] = _strMailCity;
                _strState[0] = _strMailState;
                _strCountry[0] = _strMailCountry;
                _strAddrType[0] = CustomerAddressTypes.MAILING_ADDRESS;
                _strAddrNotes[0] = _strMailNotes;
                _strAddlString[0] = "";
                _strZip[0] = _strMailPostalCode;
                _strUnit[0] = _strMailUnit;

            }
            return addrChanged;

        }

        private bool physicalAddrChanged()
        {
            bool addrChanged = false;
            //get physical/home address of the customer
            AddressVO custPhysicalAddr = _custAddrToView.getHomeAddress();
            if (custPhysicalAddr != null)
            {
                if (custPhysicalAddr.getCombinedAddress() != _strPhyAddr ||
                    custPhysicalAddr.City != _strPhyCity || custPhysicalAddr.Country_Code != _strPhyCountry ||
                    custPhysicalAddr.ZipCode != _strPhyPostalCode || custPhysicalAddr.UnitNum != _strPhyUnit ||
                    custPhysicalAddr.State_Code != _strPhyState || custPhysicalAddr.Notes != _strPhyNotes)
                {
                    addrChanged = true;
                }
                else
                    addrChanged = false;
            }
            else
                addrChanged = true;
            if (addrChanged)
            {
                _strAddr = new string[1];
                _strCity = new string[1];
                _strState = new string[1];
                _strCountry = new string[1];
                _strAddrType = new string[1];
                _strAddrNotes = new string[1];
                _strAddlString = new string[1];
                _strZip = new string[1];
                _strUnit = new string[1];
                _strAddr[0] = _strPhyAddr;
                _strCity[0] = _strPhyCity;
                _strState[0] = _strPhyState;
                _strCountry[0] = _strPhyCountry;
                _strAddrType[0] = CustomerAddressTypes.HOME_ADDRESS;
                _strAddrNotes[0] = _strPhyNotes;
                _strAddlString[0] = "";
                _strZip[0] = _strPhyPostalCode;
                _strUnit[0] = _strPhyUnit;

            }
            return addrChanged;

        }

        private bool isFormValid()
        {
            if (_physicalAddressChange)
            {
                if (this.custPhysicalAddr1.isValid && this.custPhysicalAddrCity.isValid && this.custPhysicalAddrCountry.SelectedIndex >= 0
                    && this.custPhysicalAddrState.isValid && this.custPhysicalAddrZipcode.isValid)
                    return true;
                else
                    return false;

            }
            if (_mailingAddressChange)
            {
                if (this.custMailingAddr1.isValid && this.custMailingAddrCity.isValid && this.custMailingAddrCountry.SelectedIndex >= 0
                    && this.custMailingAddrState.isValid && this.custMailingAddrZipcode.isValid)
                    return true;
                else
                    return false;


            }
            if (_workAddressChange)
            {
                if (this.custWorkAddr1.isValid && this.custWorkAddrCity.isValid && this.custWorkAddrCountry.SelectedIndex >= 0
                    && this.custWorkAddrState.isValid && this.custWorkAddrZipcode.isValid)
                    return true;
                else
                    return false;

            }
            if (_allAddressesChange)
            {
                return true;
            }

            return false;
        }

        private void getPhysicalAddressData()
        {
            if (custPhysicalAddr1.Text.Trim().Length > 0)
            {
                if (this.custPhysicalAddr2.Text.Length > 0)
                    _strPhyAddr = this.custPhysicalAddr1.Text + "," + this.custPhysicalAddr2.Text;
                else
                    _strPhyAddr = this.custPhysicalAddr1.Text;

                _strPhyCity = this.custPhysicalAddrCity.Text;
                _strPhyCountryName = custPhysicalAddrCountry.Text;
                _strPhyCountry = Commons.GetCountryData(custPhysicalAddrCountry.SelectedItem.ToString());
                _strPhyNotes = this.custPhysicalAddrNotes.Text;
                ComboBox addrstate = (ComboBox)this.custPhysicalAddrState.Controls[0];
                _strPhyState = addrstate.SelectedValue.ToString();

                _strPhyUnit = this.custPhysicalAddrUnit.Text;
                _strPhyPostalCode = this.custPhysicalAddrZipcode.Controls[0].Text;
                if (_strPhyAddr != string.Empty && _strPhyCity != string.Empty && _strPhyState != string.Empty
                      && _strPhyCountry != string.Empty && _strPhyPostalCode != string.Empty)
                {
                    _physicalAddrEntered = true;
                    _numberOfAddresses++;
                }
            }
        }

        private void getMailingAddressData()
        {
            if (custMailingAddr1.Text.Trim().Length > 0)
            {
                if (this.custMailingAddr2.Text.Length > 0)
                    _strMailAddr = this.custMailingAddr1.Text + "," + this.custMailingAddr2.Text;
                else
                    _strMailAddr = this.custMailingAddr1.Text;

                _strMailCity = this.custMailingAddrCity.Text;
                _strMailCountryName = custMailingAddrCountry.Text;
                _strMailCountry = Commons.GetCountryData(custMailingAddrCountry.SelectedItem.ToString());
                _strMailNotes = this.custMailingAddrNotes.Text;
                ComboBox addrstate = (ComboBox)this.custMailingAddrState.Controls[0];
                _strMailState = addrstate.SelectedValue.ToString();

                _strMailUnit = this.custMailingAddrUnit.Text;
                _strMailPostalCode = this.custMailingAddrZipcode.Controls[0].Text;
                if (_strMailAddr != string.Empty && _strMailCity != string.Empty && _strMailState != string.Empty
                     && _strMailCountry != string.Empty && _strMailPostalCode != string.Empty)
                {
                    _mailAddrEntered = true;
                    _numberOfAddresses++;
                }
            }

        }

        private void getWorkAddressData()
        {
            if (custWorkAddr1.Text.Trim().Length > 0)
            {
                if (this.custWorkAddr2.Text.Length > 0)
                    _strWorkAddr = this.custWorkAddr1.Text + "," + this.custWorkAddr2.Text;
                else
                    _strWorkAddr = this.custWorkAddr1.Text;

                _strWorkCity = this.custWorkAddrCity.Text;
                _strWorkCountryName = custWorkAddrCountry.Text;
                _strWorkCountry = Commons.GetCountryData(custWorkAddrCountry.SelectedItem.ToString());
                _strWorkNotes = this.custWorkAddrNotes.Text;
                ComboBox addrstate = (ComboBox)this.custWorkAddrState.Controls[0];
                _strWorkState = addrstate.SelectedValue.ToString();

                _strWorkUnit = this.custWorkAddrUnit.Text;
                _strWorkPostalCode = this.custWorkAddrZipcode.Controls[0].Text;
                if (_strWorkAddr != string.Empty && _strWorkCity != string.Empty && _strWorkState != string.Empty
                    && _strWorkCountry != string.Empty && _strWorkPostalCode != string.Empty)
                {
                    _workAddrEntered = true;
                    _numberOfAddresses++;
                }
            }

        }

        private void getAdditionalAddressData()
        {
            if (custAddlAddr1.Text.Trim().Length > 0)
            {
                if (this.custAddlAddr2.Text.Length > 0)
                    _strAddlAddr = this.custAddlAddr1.Text + "," + this.custAddlAddr2.Text;
                else
                    _strAddlAddr = this.custAddlAddr1.Text;

                _strAddlCity = this.custAddlAddrCity.Text;
                _strAddlCountryName = custAddlAddrCountry.Text;
                _strAddlCountry = Commons.GetCountryData(custAddlAddrCountry.SelectedItem.ToString());
                _strAddlNotes = this.custAddlAddrNotes.Text;
                ComboBox addrstate = (ComboBox)this.custAddlAddrState.Controls[0];
                _strAddlState = addrstate.SelectedValue.ToString();
                _strAddlAltString = this.custAddlAddrAlternateString.Text.ToString();

                _strAddlUnit = this.custAddlAddrUnit.Text;
                _strAddlPostalCode = this.custAddlAddrZipcode.Controls[0].Text;
                //strAddlContactType = CustomerAddressTypes.ADDITIONAL_ADDRESS;
                if (_strAddlAddr != string.Empty && _strAddlCity != string.Empty && _strAddlState != string.Empty
                    && _strAddlCountry != string.Empty && _strAddlPostalCode != string.Empty)
                {
                    _addlAddrEntered = true;
                    _numberOfAddresses++;
                }
            }

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            //CashlinxDesktop.Desktop.CashlinxDesktopSession.Instance.HistorySession.Back();
            NavControlBox.Action = NavBox.NavAction.BACK;

        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            addAddressDataForCustomer();


            /*this.Hide();
            UpdatePhysicalDesc physdescForm = new UpdatePhysicalDesc();
            CashlinxDesktopSession.Instance.HistorySession.AddForm(physdescForm);
            physdescForm.Show(ownerFrm);*/
            this.NavControlBox.IsCustom = true;
            this.NavControlBox.CustomDetail = "UpdatePhysicalDescription";
            this.NavControlBox.Action = NavBox.NavAction.HIDEANDSHOW;



        }

        private void addAddressDataForCustomer()
        {
            gatherAllAddressData();
            //CustomerVO custObject = CashlinxDesktop.Desktop.CashlinxDesktopSession.Instance.ActiveCustomer;
            if (_custAddrToView != null)
            {
                CustomerProcedures.setCustomerAddressDataInObject(_custAddrToView, _strAddr, null,
                    _strCity, _strState, _strZip, _strUnit,
                     _strCountry, _strAddrNotes, _strAddlString, _strAddrType);
            }
            else
            {
                BasicExceptionHandler.Instance.AddException("Error loading customer data in customer object in session ", new ApplicationException());
                    //CashlinxDesktop.Desktop.CashlinxDesktopSession.Instance.HistorySession.Desktop();
                    NavControlBox.Action = NavBox.NavAction.CANCEL;
            }

        }

        private void custPhysicalAddr1_Leave(object sender, EventArgs e)
        {
            changeReadonlyForPhysicalAddressFields();
        }

        private void changeReadonlyForPhysicalAddressFields()
        {
            //Re-enable all the other fields for physical address
            //if value is entered in addr line 1
            if (this.custPhysicalAddr1.Text.Trim().Length > 0)
            {
                this.custPhysicalAddr2.ReadOnly = false;
                this.custPhysicalAddrCity.ReadOnly = false;
                this.custPhysicalAddrState.Controls[0].Enabled = true;
                this.custPhysicalAddrUnit.ReadOnly = false;
                this.custPhysicalAddrZipcode.Enabled = true;
                this.custPhysicalAddrNotes.ReadOnly = false;
                this.custPhysicalAddrCountry.Enabled = true;
            }
            else
            {
                ClearPhysicalAddressData();
                this.custPhysicalAddr2.ReadOnly = true;
                this.custPhysicalAddrCity.ReadOnly = true;
                this.custPhysicalAddrState.Controls[0].Enabled = false;
                this.custPhysicalAddrUnit.ReadOnly = true;
                this.custPhysicalAddrZipcode.Enabled = false;
                this.custPhysicalAddrNotes.ReadOnly = true;
                this.custPhysicalAddrCountry.Enabled = false;
            }
        }

        private void custWorkAddr1_Leave(object sender, EventArgs e)
        {
            changeReadonlyForWorkAddressFields();
        }

        private void changeReadonlyForWorkAddressFields()
        {
            if (this.custWorkAddr1.Text.Trim().Length > 0)
            {
                this.custWorkAddr2.ReadOnly = false;
                this.custWorkAddrCity.ReadOnly = false;
                this.custWorkAddrState.Controls[0].Enabled = true;
                this.custWorkAddrUnit.ReadOnly = false;
                this.custWorkAddrZipcode.Enabled = true;
                this.custWorkAddrNotes.ReadOnly = false;
                this.custWorkAddrCountry.Enabled = true;
            }
            else
            {
                ClearWorkAddressData();
                this.custWorkAddr2.ReadOnly = true;
                this.custWorkAddrCity.ReadOnly = true;
                this.custWorkAddrState.Controls[0].Enabled = false;
                this.custWorkAddrUnit.ReadOnly = true;
                this.custWorkAddrZipcode.Enabled = false;
                this.custWorkAddrNotes.ReadOnly = true;
                this.custWorkAddrCountry.Enabled = false;
            }


        }

        private void custMailingAddr1_Leave(object sender, EventArgs e)
        {
            changeReadonlyForMailingAddressFields();


        }

        private void changeReadonlyForMailingAddressFields()
        {
            if (custMailingAddr1.Text.Trim().Length > 0)
            {
                this.custMailingAddr2.ReadOnly = false;
                this.custMailingAddrCity.ReadOnly = false;
                this.custMailingAddrState.Controls[0].Enabled = true;
                this.custMailingAddrUnit.ReadOnly = false;
                this.custMailingAddrZipcode.Enabled = true;
                this.custMailingAddrNotes.ReadOnly = false;
                this.custMailingAddrCountry.Enabled = true;
            }
            else
            {
                ClearMailingAddressData();
                this.custMailingAddr2.ReadOnly = true;
                this.custMailingAddrCity.ReadOnly = true;
                this.custMailingAddrState.Controls[0].Enabled = false;
                this.custMailingAddrUnit.ReadOnly = true;
                this.custMailingAddrZipcode.Enabled = false;
                this.custMailingAddrNotes.ReadOnly = true;
                this.custMailingAddrCountry.Enabled = false;
            }
        }

        private void custAddlAddr1_Leave(object sender, EventArgs e)
        {
            changeReadonlyForAdditionalAddressFields();
        }

        private void changeReadonlyForAdditionalAddressFields()
        {
            if (custAddlAddr1.Text.Trim().Length > 0)
            {
                this.custAddlAddr2.ReadOnly = false;
                this.custAddlAddrAlternateString.ReadOnly = false;
                this.custAddlAddrCity.ReadOnly = false;
                this.custAddlAddrState.Controls[0].Enabled = true;
                this.custAddlAddrUnit.ReadOnly = false;
                this.custAddlAddrZipcode.Enabled = true;
                this.custAddlAddrNotes.ReadOnly = false;
                this.custAddlAddrCountry.Enabled = true;
            }
            else
            {
                ClearAdditionlAddressData();
                this.custAddlAddr2.ReadOnly = true;
                this.custAddlAddrAlternateString.ReadOnly = true;
                this.custAddlAddrCity.ReadOnly = true;
                this.custAddlAddrState.Controls[0].Enabled = false;
                this.custAddlAddrUnit.ReadOnly = true;
                this.custAddlAddrZipcode.Enabled = false;
                this.custAddlAddrNotes.ReadOnly = true;
                this.custAddlAddrCountry.Enabled = false;

            }

        }

        private void custAddlAddrZipcode_Leave(object sender, EventArgs e)
        {
            FillCityandState(custAddlAddrState, custAddlAddrCity, custAddlAddrZipcode, custAddlAddrCountry);
        }

        private void custMailingAddr1_TextChanged(object sender, EventArgs e)
        {
            changeReadonlyForMailingAddressFields();
        }

        private void ClearPhysicalAddressData()
        {
            this.custPhysicalAddr1.Text = "";
            this.custPhysicalAddr2.Text = "";
            this.custPhysicalAddrCity.Text = "";
            ((ComboBox)this.custPhysicalAddrState.Controls[0]).SelectedIndex = -1;
            this.custPhysicalAddrUnit.Text = "";
            this.custPhysicalAddrZipcode.Controls[0].Text = "";
            this.custPhysicalAddrNotes.Text = "";
            this.custPhysicalAddrCountry.SelectedIndex = -1;
        }

        private void ClearWorkAddressData()
        {
            this.custWorkAddr1.Text = "";
            this.custWorkAddr2.Text = "";
            this.custWorkAddrCity.Text = "";
            ((ComboBox)this.custWorkAddrState.Controls[0]).SelectedIndex = -1;
            this.custWorkAddrUnit.Text = "";
            this.custWorkAddrZipcode.Controls[0].Text = "";
            this.custWorkAddrNotes.Text = "";
            this.custWorkAddrCountry.SelectedIndex = -1;

        }

        private void ClearMailingAddressData()
        {
            this.custMailingAddr1.Text = "";
            this.custMailingAddr2.Text = "";
            this.custMailingAddrCity.Text = "";
            ((ComboBox)this.custMailingAddrState.Controls[0]).SelectedIndex = -1;
            this.custMailingAddrUnit.Text = "";
            this.custMailingAddrZipcode.Controls[0].Text = "";
            this.custMailingAddrNotes.Text = "";
            this.custMailingAddrCountry.SelectedIndex = -1;

        }

        private void ClearAdditionlAddressData()
        {
            this.custAddlAddr1.Text = "";
            this.custAddlAddr2.Text = "";
            this.custAddlAddrCity.Text = "";
            ((ComboBox)this.custAddlAddrState.Controls[0]).SelectedIndex = -1;
            this.custAddlAddrUnit.Text = "";
            this.custAddlAddrZipcode.Controls[0].Text = "";
            this.custAddlAddrNotes.Text = "";
            this.custAddlAddrCountry.SelectedIndex = -1;
            this.custAddlAddrAlternateString.Text = "";

        }

        private void custPhysicalAddr1_TextChanged(object sender, EventArgs e)
        {
            changeReadonlyForPhysicalAddressFields();
        }

        private void custWorkAddr1_TextChanged(object sender, EventArgs e)
        {
            changeReadonlyForWorkAddressFields();
        }

        private void custAddlAddr1_TextChanged(object sender, EventArgs e)
        {
            changeReadonlyForAdditionalAddressFields();
        }

  

    }
}
