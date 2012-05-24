using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using CashlinxDesktop.UserControls;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.UserControls;
using Pawn.Logic;

namespace Pawn.Forms.GunUtilities.EditGunBook
{
    public partial class CustomerReplace : CustomBaseForm
    {
        public NavBox NavControlBox;
        private DataTable gunBookData;
        private CustomerVO newCustomer;
        private readonly ComboBox custIdIssuer;
        private readonly ComboBox custIdCountry;
        private readonly ComboBox idType;
        ComboBox identIssuer = new ComboBox();
        string strIdentTypeCode = "";
        string strIdentIssuer = "";
        string strIdentIssuerName = "";
        string strIdentNumber = "";
        bool stateIDSelected = false;
        ComboBox identType = new ComboBox();
        private bool idEditResource = false;
        string strIdentExpirydate = "";

        string selectedIDType = "";
        private bool incompleteData;


        public CustomerReplace()
        {
            InitializeComponent();
            NavControlBox = new NavBox
            {
                Owner = this
            };
            //set the ID Type combobox
            idType = (ComboBox)this.pwnapp_identificationtype.Controls[0];
            idType.SelectedIndexChanged += this.idType1_SelectedIndexChanged;
            //set the country/state controls and its event handlers
            //set the Id Issuer state combobox
            custIdIssuer = (ComboBox)this.pwnapp_identificationstate.Controls[0];
            custIdIssuer.SelectedIndexChanged += custIdIssuer_SelectedIndexChanged;
            //Set the Id Issuer country combobox
            custIdCountry = (ComboBox)this.pwnapp_identificationcountry.Controls[0];
            custIdCountry.SelectedIndexChanged += custIdCountry_SelectedIndexChanged;

        }

        private void custIdCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateIdNumberAndDate();
        }

        private void custIdIssuer_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateIdNumberAndDate();
        }

        private void idType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateIssuerType();
        }

        private void populateIssuerType()
        {
            if (idType == null) return;

            if (identIssuer != null)
                identIssuer.Enabled = true;



            selectedIDType = idType.SelectedIndex > 0 ? idType.SelectedValue.ToString() : "";
            switch (selectedIDType)
            {
                case "":
                    this.tableLayoutPanel2.Controls.Remove(pwnapp_identificationstate);
                    this.tableLayoutPanel2.Controls.Add(pwnapp_identificationcountry, 1, 1);
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
                    break;
                case StateIdTypes.DRIVERLICENSE:
                    this.tableLayoutPanel2.Controls.Remove(pwnapp_identificationcountry);
                    this.tableLayoutPanel2.Controls.Add(pwnapp_identificationstate, 1, 1);
                    pwnapp_identificationstate.TabIndex = this.pwnapp_identificationtype.TabIndex + 1;
                    pwnapp_identificationstate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
                    this.pwnapp_identificationstate.SetBounds(193, 16, pwnapp_identificationstate.Width, pwnapp_identificationstate.Height);
                    pwnapp_identificationstate.Visible = true;
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
            if (this.tableLayoutPanel2.Controls.Contains(pwnapp_identificationstate))
            {
                pwnapp_identificationstate.TabIndex = this.pwnapp_identificationtype.TabIndex + 1;
                identIssuer = (ComboBox)this.pwnapp_identificationstate.Controls[0];
                foreach (USState custidissuer in identIssuer.Items)
                    custidissuer.Selected = false;
                if (newCustomer != null)
                {
                    List<IdentificationVO> custIdTypes = newCustomer.getIdentifications(selectedIDType);
                    bool primarySelected = false;
                    if (custIdTypes != null && custIdTypes.Count > 0)
                    {
                        foreach (IdentificationVO id in custIdTypes)
                        {
                            if (id.IdIssuerCode.Trim().Length != 0)
                            {
                                foreach (USState custidissuer in identIssuer.Items)
                                    if (custidissuer.ShortName == id.IdIssuerCode)
                                    {
                                        custidissuer.Selected = true;
                                        if (id.IsLatest)
                                        {
                                            identIssuer.SelectedIndex = identIssuer.FindString(custidissuer.ShortName);
                                            pwnapp_identificationnumber.Enabled = false;
                                            pwnapp_identificationnumber.Text = id.IdValue;
                                            this.pwnapp_identificationexpirationdate.Enabled = false;
                                            if (id.IdExpiryData.FormatDate() != string.Empty && (id.IdExpiryData.Date != DateTime.MaxValue.Date))
                                                this.pwnapp_identificationexpirationdate.Controls[0].Text = (id.IdExpiryData).FormatDate();
                                            primarySelected = true;
                                            break;
                                        }
                                        if (!primarySelected)
                                        {
                                            resetValues();
                                        }
                                    }

                            }
                        }
                    }
                }
            }
            else
            {
                pwnapp_identificationcountry.TabIndex = this.pwnapp_identificationtype.TabIndex + 1;
                if (newCustomer != null)
                {
                    identIssuer = (ComboBox)this.pwnapp_identificationcountry.Controls[0];
                    List<IdentificationVO> custIdTypes = newCustomer.getIdentifications(selectedIDType);
                    foreach (CountryData custidissuer in custIdCountry.Items)
                        custidissuer.Selected = false;
                    bool primarySelected = false;

                    if (custIdTypes != null && custIdTypes.Count > 0)
                    {
                        foreach (IdentificationVO id in custIdTypes)
                        {
                            if (id.IdIssuerCode.Trim().Length != 0)
                            {
                                foreach (CountryData custidissuer in custIdCountry.Items)
                                {
                                    if (custidissuer.Code == id.IdIssuerCode)
                                    {
                                        custidissuer.Selected = true;
                                        if (id.IsLatest)
                                        {
                                            identIssuer.SelectedIndex = identIssuer.FindString(custidissuer.Name);
                                            pwnapp_identificationnumber.Enabled = false;
                                            this.pwnapp_identificationexpirationdate.Enabled = false;
                                            pwnapp_identificationnumber.Text = id.IdValue;
                                            if (id.IdExpiryData.FormatDate() != string.Empty && (id.IdExpiryData.Date != DateTime.MaxValue.Date))
                                                this.pwnapp_identificationexpirationdate.Controls[0].Text = (id.IdExpiryData).FormatDate();
                                            primarySelected = true;
                                            break;
                                        }
                                        if (!primarySelected)
                                        {
                                            resetValues();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
        }

        private void resetValues()
        {
            if (identIssuer.Enabled)
            {
                this.identIssuer.SelectedIndex = identIssuer.FindString("Select One");
                this.pwnapp_identificationnumber.Text = "";
                this.pwnapp_identificationexpirationdate.Controls[0].Text = "";
                this.pwnapp_identificationexpirationdate.Enabled = true;
                this.pwnapp_identificationnumber.Enabled = true;
            }
        }


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

            if (newCustomer != null)
            {
                IdentificationVO customerId = null;
                    customerId = newCustomer.getIdByTypeandIssuer(strIdentTypeCode, strIdentIssuer);

                if (customerId != null)
                {
                    this.pwnapp_identificationnumber.Text = customerId.IdValue.ToString();
                    if (customerId.IdExpiryData.Date != DateTime.MaxValue.Date)
                        this.pwnapp_identificationexpirationdate.Controls[0].Text = (customerId.IdExpiryData).FormatDate();
                    else
                        this.pwnapp_identificationexpirationdate.Controls[0].Text = "";
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

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void CustomerReplace_Load(object sender, EventArgs e)
        {
            gunBookData = GlobalDataAccessor.Instance.DesktopSession.GunData;
            if (gunBookData != null && gunBookData.Rows.Count > 0)
            {
                if (GlobalDataAccessor.Instance.DesktopSession.GunAcquireCustomer)
                {
                    string acquireCustNumber = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_customer_number"]);
                    customerNumber.Text = acquireCustNumber;
                    string acquireCustFirstName = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_first_name"]);
                    string acquireCustLastName = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_last_name"]);
                    string acquireCustMiddleName = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_middle_initial"]);
                    currentName.Text = string.Format("{0} {1} {2}", acquireCustFirstName, acquireCustMiddleName, acquireCustLastName);
                    string acquireCustomerAddress1 = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_address"]);
                    address1.Text = acquireCustomerAddress1;
                    string acquireCustomerCity = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_city"]);
                    string acquireCustomerState = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_state"]);
                    string acquireCustomerZipcode = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_postal_code"]);
                    address2.Text = string.Format("{0},{1} {2}", acquireCustomerCity, acquireCustomerState, acquireCustomerZipcode);
                    string acquireCustIDType = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_id_type"]);
                    string acquireCustIDNumber = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_id_number"]);
                    string acquireCustIDAgency = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_id_agency"]);
                    id.Text = string.Format("{0} {1} {2}", acquireCustIDType, acquireCustIDAgency, acquireCustIDNumber);
                }
                else
                {
                    string dispositionCustNumber = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_customer_number"]);
                    customerNumber.Text = dispositionCustNumber;
                    
                    string dispositionCustLastName = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_last_name"]);
                    string dispositionCustFirstName = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_first_name"]);
                    string dispositionCustMiddleName = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_middle_initial"]);
                    currentName.Text = string.Format("{0} {1} {2}", dispositionCustFirstName, dispositionCustMiddleName, dispositionCustLastName);
                    
                    string dispositionCustomerAddress1 = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_address"]);
                    address1.Text = dispositionCustomerAddress1;
                    string dispositionCustomerCity = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_city"]);
                    string dispositionCustomerState = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_state"]);
                    string dispositionCustomerZipcode = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_postal_code"]);
                    address2.Text = string.Format("{0},{1} {2}", dispositionCustomerCity, dispositionCustomerState, dispositionCustomerZipcode);
                    
                    string dispositionCustIDType = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_id_type"]);
                    string dispositionCustIDAgency = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_id_agency"]);
                    string dispositionCustIDNumber = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_id_number"]);
                    id.Text = string.Format("{0} {1} {2}", dispositionCustIDType, dispositionCustIDAgency, dispositionCustIDNumber);

                }
            }
            if (!SecurityProfileProcedures.CanUserModifyResource("EDIT RESTRICTED GUN BOOK FIELDS", GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile, GlobalDataAccessor.Instance.DesktopSession))
            {
                tableLayoutPanel2.Visible = false;
            }
            else
                idEditResource = true;

            if (GlobalDataAccessor.Instance.DesktopSession.CustomerEditType == CustomerType.RECEIPT)
                label1.Text = "Edit Receipt Customer Information";
            else if (GlobalDataAccessor.Instance.DesktopSession.CustomerEditType == CustomerType.DISPOSITION)
                label1.Text = "Edit Disposition Customer Information";
            else
            {
                this.label1.Text = GlobalDataAccessor.Instance.DesktopSession.GunAcquireCustomer ? "Replace Receipt Customer Information" : "Replace Disposition Customer Information";
            }


            if (GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer != null &&
                !string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer.CustomerNumber))
            {
                newCustomer = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
                if (newCustomer.DateOfBirth !=  DateTime.MaxValue && newCustomer.Age <= 18)
                {
                    DialogResult dgr=MessageBox.Show("This customer does not meet the age criteria for firearm transactions. An audit event will be generated. Do you want to continue?", "Firearm Eligibility", MessageBoxButtons.YesNo);
                    if (dgr == DialogResult.No)
                        NavControlBox.Action = NavBox.NavAction.CANCEL;
                }
                
                labelCustNumber.Text = newCustomer.CustomerNumber;
                customTextBoxFirstName.Text = newCustomer.FirstName;
                customTextBoxLastName.Text = newCustomer.LastName;
                customTextBoxInitial.Text = newCustomer.MiddleInitial;
                ComboBox custstate = (ComboBox)state1.Controls[0];
                AddressVO custAddr = newCustomer.getHomeAddress();
                if (custAddr != null)
                {
                    customTextBoxAddr1.Text = custAddr.Address1;
                    customTextBoxAddr2.Text = custAddr.Address2;



                    foreach (USState currstate in custstate.Items)
                        if (currstate.ShortName == custAddr.State_Code)
                        {
                            custstate.SelectedIndex = custstate.Items.IndexOf(currstate);
                            break;
                        }
                    customTextBoxCity.Text = custAddr.City;
                    zipcode1.Text = custAddr.ZipCode;
                }
                IdentificationVO firstIdentity = newCustomer.getFirstIdentity();
                //Populate the id details if the first identity cursor is not empty
                if (firstIdentity != null)
                {
                    strIdentIssuerName = firstIdentity.IdIssuer;
                    strIdentNumber = firstIdentity.IdValue;
                    ComboBox custId = (ComboBox)this.pwnapp_identificationtype.Controls[0];

                    foreach (ComboBoxData idtype in custId.Items)
                    {
                        if (idtype.Code == firstIdentity.IdType)
                        {
                            custId.SelectedIndex = custId.Items.IndexOf(idtype);
                            break;
                        }
                    }
                }
                else
                {
                    pwnapp_identificationexpirationdate.Enabled = false;
                    pwnapp_identificationnumber.Enabled = false;

                }

                
            }

        }

        private void customButtonSubmit_Click(object sender, EventArgs e)
        {
            ComboBox addrstate = (ComboBox)this.state1.Controls[0];
            getPersonalIdentificationDataFromForm();
            if (idEditResource)
            {
                if (string.IsNullOrEmpty(customTextBoxLastName.Text.Trim()) ||
                    string.IsNullOrEmpty(customTextBoxFirstName.Text.Trim()) ||
                    string.IsNullOrEmpty(customTextBoxAddr1.Text.Trim()) ||
                    string.IsNullOrEmpty(addrstate.GetItemText(addrstate.SelectedItem)) ||
                    string.IsNullOrEmpty(customTextBoxCity.Text.Trim()) ||
                    string.IsNullOrEmpty(zipcode1.Text) ||
                    string.IsNullOrEmpty(strIdentTypeCode) ||
                    string.IsNullOrEmpty(strIdentIssuer) ||
                    string.IsNullOrEmpty(strIdentNumber.Trim()))
                {
                    incompleteData = true;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(customTextBoxAddr1.Text.Trim()) ||
                    string.IsNullOrEmpty(addrstate.GetItemText(addrstate.SelectedItem)) ||
                    string.IsNullOrEmpty(customTextBoxCity.Text.Trim()) ||
                    string.IsNullOrEmpty(zipcode1.Text))
                {
                    incompleteData = true;
                }
            }
            string errorCode;
            string errorText;
            string custMiddleInitial=string.Empty;
            custMiddleInitial = this.idEditResource ? (!string.IsNullOrEmpty(this.customTextBoxInitial.Text)?this.customTextBoxInitial.Text.Substring(0, 1):"") : this.newCustomer.MiddleInitial;
            var dSession = GlobalDataAccessor.Instance.DesktopSession;
            MerchandiseProcedures.UpdateGunCustomerData(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
             Utilities.GetStringValue(gunBookData.Rows[0]["gun_number"]),
             dSession.GunAcquireCustomer ? "A" : "D",
             idEditResource ? customTextBoxLastName.Text : newCustomer.LastName,
             idEditResource ? customTextBoxFirstName.Text : newCustomer.FirstName,
             custMiddleInitial,
             string.Format("{0} {1}", customTextBoxAddr1.Text, customTextBoxAddr2.Text),
                    customTextBoxCity.Text,
                    addrstate.GetItemText(addrstate.SelectedItem),
                    zipcode1.Text,
                    idEditResource ? strIdentTypeCode : newCustomer.getFirstIdentity().IdType,
                    idEditResource ? strIdentIssuer : newCustomer.getFirstIdentity().IdIssuer,
                    idEditResource ? strIdentNumber : newCustomer.getFirstIdentity().IdValue,
                    dSession.UserName,
             out errorCode,
             out errorText);
            if (errorCode != "0")
                MessageBox.Show("Customer data could not be updated");
            else
            {
                MessageBox.Show("Customer data updated successfully");
                DataTable gunTableData;
                Item gunItem;
                MerchandiseProcedures.GetGunData(GlobalDataAccessor.Instance.OracleDA,
                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                    Utilities.GetIntegerValue(gunBookData.Rows[0]["gun_number"]),                                    
                                    out gunTableData,
                                    out gunItem,
                                    out errorCode,
                                    out errorText);
                dSession.GunData = gunTableData;
                dSession.GunItemData = gunItem;


            }
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

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
            catch (Exception eX)
            {
                //If the date was invalid it would have been caught by the regular expression
                //validation. The time when this exception will happen is when the default value of mm/dd/yyyy
                //remains in the textbox
                strIdentExpirydate = "";
                return;
            }

        }




        private void zipcode1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(zipcode1.Text))
            {
                ComboBox state = (ComboBox)this.state1.Controls[0];
                if (zipcode1.isValid)
                {
                    if (zipcode1.City.Length > 0)
                    {
                        customTextBoxCity.Text = zipcode1.City;
                    }
                    else
                    {
                        this.customTextBoxCity.Text = "";
                    }
                    if (zipcode1.State.Length > 0)
                    {
                        foreach (USState currstate in state.Items)
                            if (currstate.ShortName == zipcode1.State)
                            {
                                state.SelectedIndex = state.Items.IndexOf(currstate);
                                break;
                            }
                    }
                    else
                    {
                        state.SelectedIndex = -1;
                    }
                }
            }
        }
    }
}
