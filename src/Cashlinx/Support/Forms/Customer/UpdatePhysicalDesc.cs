/********************************************************************
* CashlinxDesktop
* ViewCustomerInformation
* This form is shown when a customer's physical description data
* needs to be updated
* Sreelatha Rengarajan 5/14/2009 Initial version
*******************************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Support.Forms.Customer;
//using CashlinxDesktop.UserControls;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Support.Forms;
using Support.Libraries.Objects.Customer;

//using Pawn.Logic;

namespace Support.Forms.Pawn.Customer
{
    public partial class UpdatePhysicalDesc : Form
    {
        private string _strEyeColor;
        private string _strHairColor;
        private string _strHeight;
        private string _strWeight;
        private string _strRace;
        private string _strGender;
        private string _errorCode;
        private string _errorMsg;
        private string _strUserId;
        private string _strOthers;
        private string _strCustNumber;
        private string _strStoreNumber;
        private string _strContactProductNoteId;
        CustomerNotesVO _physicaldescNote;
        private string _strNewContactProductNoteId;
        List<string> _pawnRequiredFields = new List<string>();
        private bool _checkRequiredFields;
        Form ownerFrm;
        public NavBox NavControlBox;

        public CustomerVOForSupportApp CustToView { private get; set; }

        public UpdatePhysicalDesc()
        {
            InitializeComponent();
            NavControlBox = new NavBox();
            this.pwnapp_height.ErrorMessage = Commons.GetMessageString("InvalidHeightFeet");
            this.pwnapp_heightinches.ErrorMessage = Commons.GetMessageString("InvalidHeightInches");
            this.pwnapp_weight.ErrorMessage = Commons.GetMessageString("InvalidWeight");
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Form currForm = GlobalDataAccessor.Instance.DesktopSession.HistorySession.Lookup(this);
            if (currForm.GetType() == typeof(UpdatePhysicalDesc))
            {
                DialogResult dgr = MessageBox.Show(Commons.GetMessageString("CancelConfirmMessage"), "Warning", MessageBoxButtons.YesNo);
                if (dgr == DialogResult.Yes)
                    NavControlBox.Action = NavBox.NavAction.CANCEL;//CashlinxDesktopSession.Instance.HistorySession.Desktop();
                else
                    return;
            }
            else
            {
                this.Close();
                this.Dispose(true);
            }
        }

        private void UpdatePhysicalDesc_Load(object sender, EventArgs e)
        {
            ownerFrm = this.Owner;
            this.NavControlBox.Owner = this;
            if (CustToView == null)
            {
                _checkRequiredFields = false;
                this.customButtonBack.Visible = true;
                this.labelHeading.Text = "Physical Description";
                this.customButtonReset.Text = "Clear";
                //CustToView = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
                CustToView = Support.Logic.CashlinxPawnSupportSession.Instance.ActiveCustomer;
                if (CustToView == null)
                //if the customer object is still null there is something wrong
                //so take the user to desktop
                {
                    BasicExceptionHandler.Instance.AddException("Customer object is missing from session ", new ApplicationException());
                    //CashlinxDesktop.Desktop.CashlinxDesktopSession.Instance.HistorySession.Desktop();
                    NavControlBox.Action = NavBox.NavAction.CANCEL;
                }
            }
            else
            {
                _checkRequiredFields = true;
                showRequiredFields();

                LoadCustData();
            }
        }

        private void showRequiredFields()
        {
            _pawnRequiredFields = CustomerProcedures.getPawnRequiredFields(GlobalDataAccessor.Instance.DesktopSession);
            foreach (string reqdField in _pawnRequiredFields)
            {
                string labelName = (reqdField + "_label").ToLower();
                string fieldName = reqdField.ToLower();
                Control[] labelCtrl = this.Controls.Find(labelName, true);
                if (labelCtrl.Length > 0)
                {
                    CustomLabel customReqdLabel = (CustomLabel)labelCtrl[0];
                    customReqdLabel.Required = true;
                }
                Control[] fieldCtrl = this.Controls.Find(fieldName, true);
                if (fieldCtrl.Length > 0)
                {
                    if (fieldCtrl[0].GetType() == typeof(CustomTextBox))
                        ((CustomTextBox)(fieldCtrl[0])).Required = true;
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
        }

        private void LoadCustData()
        {
            if (CustToView != null)
            {
                //Parse the feet and inches data from the height data
                if (CustToView.Height.Trim().Length != 0)
                {
                    string strHt = CustToView.Height.ToString();
                    int feetIndexinHeight = strHt.IndexOf('\'');
                    int inchesIndexinHeight = strHt.IndexOf('\"');
                    if (feetIndexinHeight > 0)
                        this.pwnapp_height.Text = strHt.Substring(0, feetIndexinHeight);
                    this.pwnapp_heightinches.Text = inchesIndexinHeight > 0 ? strHt.Substring(feetIndexinHeight + 1, strHt.Length - inchesIndexinHeight) : "";
                }
                if (CustToView.Weight != 0)
                    this.pwnapp_weight.Text = CustToView.Weight.ToString();

                //The following code sets the selected item in the combo boxes
                //for gender, race, title, title suffix, state, eye color, hair color, 
                //to the retrieved value from the database for the customer

                ComboBox custGender = (ComboBox)this.pwnapp_sex.Controls[0];
                foreach (ComboBoxData currgender in custGender.Items)
                    if (currgender.Code == CustToView.Gender)
                    {
                        custGender.SelectedIndex = custGender.Items.IndexOf(currgender);
                        break;
                    }
                custGender = null;
                ComboBox custrace = (ComboBox)this.pwnapp_race.Controls[0];
                foreach (ComboBoxData currrace in custrace.Items)
                    if (currrace.Code == CustToView.Race)
                    {
                        custrace.SelectedIndex = custrace.Items.IndexOf(currrace);
                        break;
                    }
                custrace = null;
                ComboBox custEyeColor = (ComboBox)this.pwnapp_eyes.Controls[0];
                foreach (ComboBoxData curreyecolor in custEyeColor.Items)
                    if (curreyecolor.Code == CustToView.EyeColor)
                    {
                        custEyeColor.SelectedIndex = custEyeColor.Items.IndexOf(curreyecolor);
                        break;
                    }
                custEyeColor = null;
                ComboBox custHairColor = (ComboBox)this.pwnapp_hair.Controls[0];
                foreach (ComboBoxData currhaircolor in custHairColor.Items)
                    if (currhaircolor.Code == CustToView.HairColor)
                    {
                        custHairColor.SelectedIndex = custHairColor.Items.IndexOf(currhaircolor);
                        break;
                    }
                custHairColor = null;
                _physicaldescNote = CustToView.getPhysicalDescNote();
                if (_physicaldescNote.ContactNote != null)
                {
                    textBoxOthers.Text = _physicaldescNote.ContactNote;
                    _strContactProductNoteId = _physicaldescNote.CustomerProductNoteId;
                }
                else
                    textBoxOthers.Text = "";
            }
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            bool newCustCreated = false;
            var partyId = string.Empty;
            var custNumber = string.Empty;
            bool updatePhysData = false;
            if (isFormValid())
            {
                getFormData();
                if (string.IsNullOrEmpty(CustToView.PartyId) || physDescDataChanged())
                {
                    _strUserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
                    _strStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                    _strCustNumber = CustToView.CustomerNumber;

                    DateTime noteDate = ShopDateTime.Instance.ShopDate;
                    DialogResult dgr = DialogResult.Retry;
                    bool retVal = false;
                    var errorDesc = string.Empty;
                    //The following is true when this form is called in the Manage customer use case
                    if (CustToView.PartyId == string.Empty)
                    {
                        do
                        {
                            retVal = CustomerProcedures.AddCustomer(GlobalDataAccessor.Instance.DesktopSession, CustToView, _strUserId, _strStoreNumber, out custNumber, out partyId, out errorDesc);
                            if (retVal)
                                break;
                            else
                            {
                                dgr = MessageBox.Show(Commons.GetMessageString("CustDataAddFailure"), "Error", MessageBoxButtons.RetryCancel);
                            }
                        }while (dgr == DialogResult.Retry);

                        if (retVal)
                        {
                            newCustCreated = true;
                            _strCustNumber = custNumber;
                            CustToView.CustomerNumber = _strCustNumber;
                            CustToView.PartyId = partyId;
                            do
                            {
                                retVal = CustomerProcedures.AddCustomerAddress(GlobalDataAccessor.Instance.DesktopSession, CustToView, _strUserId, _strStoreNumber);
                                if (retVal)
                                    break;
                                else
                                {
                                    dgr = MessageBox.Show(Commons.GetMessageString("CustDataAddFailure"), "Error", MessageBoxButtons.RetryCancel);
                                }
                            }while (dgr == DialogResult.Retry);
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
                        retVal = true;

                    if (retVal)
                    {
                        do
                        {
                            updatePhysData = new CustomerDBProcedures(GlobalDataAccessor.Instance.DesktopSession).UpdateCustPhysicalDescription(_strRace, _strGender, _strHairColor, _strEyeColor, _strHeight,
                                                                                                _strWeight, _strUserId, CustToView.PartyId, _strCustNumber, _strOthers, _strContactProductNoteId,
                                                                                                _strStoreNumber, out _strNewContactProductNoteId, out _errorCode, out _errorMsg);
                            if (updatePhysData)
                            {
                                Form ownerForm = this.Owner;
                                if (ownerForm.GetType() == typeof(ViewCustomerInformation))
                                {
                                    CustomerVOForSupportApp updatedCustomer = new CustomerVOForSupportApp();
                                    updatedCustomer.addNotes(CustToView.getPhysicalDescNote());
                                    updatedCustomer.Race = _strRace;
                                    updatedCustomer.Gender = _strGender;
                                    updatedCustomer.HairColor = _strHairColor;
                                    updatedCustomer.EyeColor = _strEyeColor;
                                    updatedCustomer.Height = _strHeight;
                                    if (_strNewContactProductNoteId.Equals(string.Empty))
                                        _strNewContactProductNoteId = _strContactProductNoteId;
                                    try
                                    {
                                        updatedCustomer.Weight = Convert.ToInt32(_strWeight);
                                    }
                                    catch (Exception)
                                    {
                                        updatedCustomer.Weight = 0;
                                    }
                                    updatedCustomer.updatePhysicalDescNote(_strOthers, _strStoreNumber, noteDate, _strUserId, _strNewContactProductNoteId);

                                    ((ViewCustomerInformation)ownerForm).UpdatedCustomerToView = updatedCustomer;
                                    ((ViewCustomerInformation)ownerForm).ShowUpdates = true;
                                }
                                break;
                            }
                            else
                            {
                                dgr = MessageBox.Show(Commons.GetMessageString("CustPhysicalDescUpdateFailure"), "Error", MessageBoxButtons.RetryCancel);
                            }
                        }while (dgr == DialogResult.Retry);
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
                else if (CustToView.PartyId != string.Empty)
                {
                    MessageBox.Show(Commons.GetMessageString("NoChangesInForm"));
                }
            }
            else
            {
                MessageBox.Show(Commons.GetMessageString("FormRequiredFieldsFilledError"));
                return;
            }
            if (newCustCreated)
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
                if (updatePhysData)
                    MessageBox.Show("Physical details updated");
                this.Close();
                this.Dispose(true);
            }
        }

        private bool physDescDataChanged()
        {
            if (_physicaldescNote.ContactNote != null && _physicaldescNote.ContactNote != _strOthers)
                return true;
            else
            {
                if (_physicaldescNote.ContactNote == null && !(_strOthers.Length == 0))
                    return true;
            }

            if (CustToView.Race != _strRace || CustToView.Gender != _strGender || CustToView.EyeColor != _strEyeColor
                || CustToView.HairColor != _strHairColor || CustToView.Height != _strHeight || CustToView.Weight != Int32.Parse(_strWeight))
            {
                return true;
            }

            return false;
        }

        private bool isFormValid()
        {
            if (_checkRequiredFields)
            {
                if (this.pwnapp_race.isValid && this.pwnapp_sex.isValid && this.pwnapp_hair.isValid &&
                    this.pwnapp_eyes.isValid && this.pwnapp_height.isValid)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        private void getFormData()
        {
            ComboBox custEyeColor = (ComboBox)this.pwnapp_eyes.Controls[0];
            ComboBox custHairColor = (ComboBox)this.pwnapp_hair.Controls[0];
            ComboBox custRace = (ComboBox)this.pwnapp_race.Controls[0];
            ComboBox custGender = (ComboBox)this.pwnapp_sex.Controls[0];
            _strEyeColor = custEyeColor.SelectedValue.ToString();
            _strHairColor = custHairColor.SelectedValue.ToString();
            if (this.pwnapp_height.Text.ToString().Length > 0)
            {
                if (this.pwnapp_heightinches.Text.Trim().Length != 0)
                    _strHeight = this.pwnapp_height.Text.ToString() + "'" + this.pwnapp_heightinches.Text.ToString() + "\"";
                else
                    _strHeight = this.pwnapp_height.Text.ToString() + "'";
            }
            else
                _strHeight = "";

            if (this.pwnapp_weight.Text.Trim().Length != 0)
                _strWeight = this.pwnapp_weight.Text;
            else
                _strWeight = "0";
            _strRace = custRace.SelectedValue.ToString();
            _strGender = custGender.SelectedValue.ToString();
            _strOthers = textBoxOthers.Text;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (customButtonReset.Text == "Clear")
                ClearAllFields();
            else
                LoadCustData();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            //CashlinxDesktop.Desktop.CashlinxDesktopSession.Instance.HistorySession.Back();
            NavControlBox.Action = NavBox.NavAction.BACK;
        }

        private void ClearAllFields()
        {
            this.pwnapp_height.Text = "";
            this.pwnapp_heightinches.Text = "";
            this.pwnapp_weight.Text = "";
            this.textBoxOthers.Text = "";
            this.pwnapp_eyes.Clear();
            this.pwnapp_hair.Clear();
            this.pwnapp_race.Clear();
            this.pwnapp_sex.Clear();
        }
    }
}