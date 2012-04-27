/********************************************************************
* CashlinxDesktop.UserControls
* PhoneData
* This user control can be used in a form to allow the user to enter
* phone data for a customer- Home, Cell, Work, Pager and Fax
* Pager and Fax can be configured to be not shown if the form
* does not need it
* Sreelatha Rengarajan 7/8/2009 Initial version
*******************************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.UserControls
{
    public partial class PhoneData : UserControl
    {

        private int numberOfPhoneNumbers;
        private bool homeNumberEntered = false;
        private bool workNumberEntered = false;
        private bool cellNumberEntered = false;
        private bool pagerEntered = false;
        private bool faxEntered = false;
        private List<ContactVO> custPhoneData;
        private bool _isValid = false;

        public bool ShowFax
        {
            set;
            get;
        }

        public bool ShowPager
        {
            set;
            get;
        }
        public bool ShowHeading
        {
            set;
            get;
        }
        public bool TabExtension
        {
            set;
            get;
        }
        public bool TabCountryCode
        {
            set;
            get;
        }


        public bool IsValid
        {
            get
            {
                return _isValid;
            }
        }

        public void SetPhoneRequired(bool value)
        {
            this.customLabelPrimary.Required = value;
        }

        public bool IsPhoneEntered()
        {
            return ((!string.IsNullOrEmpty(homePhoneAreaCode.Text) && !string.IsNullOrEmpty(homePhoneNumber.Text)) ||
                (!string.IsNullOrEmpty(workPhoneAreaCode.Text) && !string.IsNullOrEmpty(workPhoneNumber.Text)) ||
                (!string.IsNullOrEmpty(cellPhoneAreaCode.Text) && !string.IsNullOrEmpty(cellPhoneNumber.Text)));
        }

        public PhoneData()
        {
            InitializeComponent();
            ShowFax = false;
            ShowPager = false;
            ShowHeading = true;
        }

        public void Clear()
        {
            if (this.homePhoneNumber.Required)
            {
                this.homePhoneAreaCode.Required = false;
                this.homePhoneNumber.Required = false;
            }
            //Clear home phone data
            this.homePhoneAreaCode.Text = "";
            this.homePhoneCountryCode.Text = "";
            this.homePhoneExtn.Text = "";
            this.homePhoneNumber.Text = "";
            this.radioButtonHomePhone.Checked = false;
            //Clear cell phone data
            this.cellPhoneAreaCode.Text = "";
            this.cellPhoneCountryCode.Text = "";
            this.cellPhoneExtn.Text = "";
            this.cellPhoneNumber.Text = "";
            this.radioButtonCellPhone.Checked = false;
            //Clear work phone data
            this.workPhoneAreaCode.Text = "";
            this.workPhoneCountryCode.Text = "";
            this.workPhoneExtn.Text = "";
            this.workPhoneNumber.Text = "";
            this.radioButtonWorkPhone.Checked = false;
            //Clear fax data
            this.faxAreaCode.Text = "";
            this.faxCountryCode.Text = "";
            this.faxExtn.Text = "";
            this.faxNumber.Text = "";
            //Clear pager data
            this.pagerAreaCode.Text = "";
            this.pagerCountryCode.Text = "";
            this.pagerExtn.Text = "";
            this.pagerNumber.Text = "";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //Load the error messages
            this.homePhoneAreaCode.ErrorMessage = Commons.GetMessageString("InvalidPhoneAreaCode");
            this.cellPhoneAreaCode.ErrorMessage = Commons.GetMessageString("InvalidPhoneAreaCode");
            this.workPhoneAreaCode.ErrorMessage = Commons.GetMessageString("InvalidPhoneAreaCode");
            this.faxAreaCode.ErrorMessage = Commons.GetMessageString("InvalidPhoneAreaCode");
            this.pagerAreaCode.ErrorMessage = Commons.GetMessageString("InvalidPhoneAreaCode");
            this.homePhoneNumber.ErrorMessage = Commons.GetMessageString("InvalidPhoneNumber");
            this.cellPhoneNumber.ErrorMessage = Commons.GetMessageString("InvalidPhoneNumber");
            this.workPhoneNumber.ErrorMessage = Commons.GetMessageString("InvalidPhoneNumber");
            this.pagerNumber.ErrorMessage = Commons.GetMessageString("InvalidPhoneNumber");
            this.faxNumber.ErrorMessage = Commons.GetMessageString("InvalidPhoneNumber");
            if (ShowFax)
            {
                //Enable all the fax controls
                labelFax.Visible = true;
                labelFaxCloseBracket.Visible = true;
                labelFaxCountryCode.Visible = true;
                labelFaxExtn.Visible = true;
                labelFaxOpenBracket.Visible = true;
                faxAreaCode.Visible = true;
                faxCountryCode.Visible = true;
                faxExtn.Visible = true;
                faxNumber.Visible = true;

            }
            if (ShowPager)
            {
                //Enable all the pager controls
                labelPagerHeading.Visible = true;
                labelPagerCloseBracket.Visible = true;
                labelPagerCountryCode.Visible = true;
                labelPagerExt.Visible = true;
                labelPagerOpenBracket.Visible = true;
                pagerAreaCode.Visible = true;
                pagerCountryCode.Visible = true;
                pagerExtn.Visible = true;
                pagerNumber.Visible = true;
            }
            if (!ShowHeading)
            {
                phoneHeadingLabel.Visible = false;
            }
            if (!TabExtension)
            {
                this.homePhoneExtn.TabStop = false;
                this.cellPhoneExtn.TabStop = false;
                this.workPhoneExtn.TabStop = false;
                this.faxExtn.TabStop = false;
                this.pagerExtn.TabStop = false;
            }
            else
            {
                this.homePhoneExtn.TabStop = true;
                this.cellPhoneExtn.TabStop = true;
                this.workPhoneExtn.TabStop = true;
                this.faxExtn.TabStop = true;
                this.pagerExtn.TabStop = true;

            }
            if (!TabCountryCode)
            {
                this.homePhoneCountryCode.TabStop = false;
                this.cellPhoneCountryCode.TabStop = false;
                this.workPhoneCountryCode.TabStop = false;
                this.faxCountryCode.TabStop = false;
                this.pagerCountryCode.TabStop = false;
            }
            else
            {
                this.homePhoneCountryCode.TabStop = true;
                this.cellPhoneCountryCode.TabStop = true;
                this.workPhoneCountryCode.TabStop = true;
                this.faxCountryCode.TabStop = true;
                this.pagerCountryCode.TabStop = true;

            }
        }


        public void populatePhoneNumber(string strAreaCode, string strNumber)
        {
            this.homePhoneAreaCode.Text = strAreaCode;
            if (strNumber.Length == 7)
                this.homePhoneNumber.Text = string.Format("{0}-{1}", strNumber.Substring(0, 3), strNumber.Substring(3, 4));

        }

        public void populatePhoneNumber(List<ContactVO> customerPhoneData)
        {
            //populate home phone number of the customer
            ContactVO custHomePhone = customerPhoneData.Find(delegate(ContactVO contactObj)
            {
                return (contactObj.ContactType == CustomerPhoneTypes.HOME_NUMBER &&
                    contactObj.TelecomNumType == CustomerPhoneTypes.VOICE_NUMBER);
            });
            populatePhoneNumbers(custHomePhone, "homePhoneAreaCode", "homePhoneNumber", "homePhoneExtn", "homePhoneCountryCode", "radioButtonHomePhone");

            //populate cell phone number of the customer
            ContactVO custCellPhone = customerPhoneData.Find(delegate(ContactVO contactObj)
            {
                return (contactObj.ContactType == CustomerPhoneTypes.HOME_NUMBER &&
                    contactObj.TelecomNumType == CustomerPhoneTypes.MOBILE_NUMBER);
            });
            populatePhoneNumbers(custCellPhone, "cellPhoneAreaCode", "cellPhoneNumber", "cellPhoneExtn", "cellPhoneCountryCode", "radioButtonCellPhone");
            //populate work phone number of the customer
            ContactVO custWorkPhone = customerPhoneData.Find(delegate(ContactVO contactObj)
            {
                return (contactObj.ContactType == CustomerPhoneTypes.WORK_NUMBER &&
                    contactObj.TelecomNumType == CustomerPhoneTypes.VOICE_NUMBER);
            });
            populatePhoneNumbers(custWorkPhone, "workPhoneAreaCode", "workPhoneNumber", "workPhoneExtn", "workPhoneCountryCode", "radioButtonWorkPhone");
            //populate pager number of the customer
            ContactVO custPager = customerPhoneData.Find(delegate(ContactVO contactObj)
            {
                return (contactObj.ContactType == CustomerPhoneTypes.HOME_NUMBER &&
                    contactObj.TelecomNumType == CustomerPhoneTypes.PAGER_NUMBER);
            });
            populatePhoneNumbers(custPager, "pagerAreaCode", "pagerNumber", "pagerExtn", "pagerCountryCode", "");
            //populate fax phone number of the customer
            ContactVO custFax = customerPhoneData.Find(delegate(ContactVO contactObj)
            {
                return (contactObj.ContactType == CustomerPhoneTypes.HOME_NUMBER &&
                    contactObj.TelecomNumType == CustomerPhoneTypes.FAX_NUMBER);
            });
            populatePhoneNumbers(custFax, "faxAreaCode", "faxNumber", "faxExtn", "faxCountryCode", "");
        }

        private void populatePhoneNumbers(ContactVO custContact, string areacodeTextBoxName,
            string phonenumberTextBoxName, string extensionTextBoxName,
            string countryCodeTextBoxName, string radiobuttonName)
        {

            if (custContact != null)
            {
                this.tableLayoutPanel1.Controls[areacodeTextBoxName].Text = custContact.ContactAreaCode;
                this.tableLayoutPanel1.Controls[phonenumberTextBoxName].Text = Commons.FormatPhoneNumberForUI(custContact.ContactPhoneNumber);
                this.tableLayoutPanel1.Controls[extensionTextBoxName].Text = custContact.ContactExtension;
                this.tableLayoutPanel1.Controls[extensionTextBoxName].Enabled = true;
                this.tableLayoutPanel1.Controls[countryCodeTextBoxName].Text = custContact.CountryDialNumCode;
                this.tableLayoutPanel1.Controls[countryCodeTextBoxName].Enabled = true;
                if (!string.IsNullOrEmpty(radiobuttonName))
                {
                    if (custContact.TeleusrDefText == "true")
                        ((RadioButton)this.tableLayoutPanel1.Controls[radiobuttonName]).Checked = true;
                    else
                        ((RadioButton)this.tableLayoutPanel1.Controls[radiobuttonName]).Checked = false;
                }

            }
            else
            {
                this.tableLayoutPanel1.Controls[areacodeTextBoxName].Text = string.Empty;
                this.tableLayoutPanel1.Controls[phonenumberTextBoxName].Text = string.Empty;
                this.tableLayoutPanel1.Controls[extensionTextBoxName].Text = string.Empty;
                this.tableLayoutPanel1.Controls[extensionTextBoxName].Enabled = false;
                this.tableLayoutPanel1.Controls[countryCodeTextBoxName].Text = string.Empty;
                this.tableLayoutPanel1.Controls[countryCodeTextBoxName].Enabled = false;
                if (!string.IsNullOrEmpty(radiobuttonName))
                    ((RadioButton)this.tableLayoutPanel1.Controls[radiobuttonName]).Checked = false;
            }


        }


        public void checkValid()
        {
            _isValid = true;
            numberOfPhoneNumbers = 0;
            checkPhoneNumbersEntered();
            if (radioButtonHomePhone.Checked && !homeNumberEntered)
            {

                _isValid = false;
            }

            if (radioButtonCellPhone.Checked && !cellNumberEntered)
            {

                _isValid = false;
            }
            if (radioButtonWorkPhone.Checked && !workNumberEntered)
            {
                _isValid = false;
            }
            if (!_isValid)
            {
                var dgr = MessageBox.Show(Commons.GetMessageString("PrimaryPhoneError"), "Prompt", MessageBoxButtons.YesNo);
                if (dgr == DialogResult.Yes)
                    _isValid = true;
                else
                    _isValid = false;
            }

        }

        public List<ContactVO> getPhoneData()
        {
            custPhoneData = new List<ContactVO>();
            numberOfPhoneNumbers = 0;
            checkValid();
            if (_isValid)
            {
                var strPrimaryPhone = string.Empty;
                if (numberOfPhoneNumbers > 0)
                {
                    if (homeNumberEntered)
                    {
                        if (radioButtonHomePhone.Checked)
                            strPrimaryPhone = "true";
                        else
                            strPrimaryPhone = string.Empty;
                        ContactVO custPhone = new ContactVO();
                        custPhone.ContactAreaCode = this.homePhoneAreaCode.Text;
                        custPhone.ContactExtension = this.homePhoneExtn.Text;
                        custPhone.ContactPhoneNumber = Commons.FormatPhoneNumberForDB(this.homePhoneNumber.Text);
                        custPhone.ContactType = CustomerPhoneTypes.HOME_NUMBER;
                        custPhone.CountryDialNumCode = this.homePhoneCountryCode.Text;
                        custPhone.TelecomNumType = CustomerPhoneTypes.VOICE_NUMBER;
                        custPhone.TeleusrDefText = strPrimaryPhone;
                        custPhoneData.Add(custPhone);

                    }

                    if (cellNumberEntered)
                    {
                        if (radioButtonCellPhone.Checked)
                            strPrimaryPhone = "true";
                        else
                            strPrimaryPhone = string.Empty;
                        var custPhone = new ContactVO
                                        {
                                            ContactAreaCode = this.cellPhoneAreaCode.Text,
                                            ContactExtension = this.cellPhoneExtn.Text,
                                            ContactPhoneNumber =
                                                Commons.FormatPhoneNumberForDB(this.cellPhoneNumber.Text),
                                            ContactType = CustomerPhoneTypes.HOME_NUMBER,
                                            CountryDialNumCode = this.cellPhoneCountryCode.Text,
                                            TelecomNumType = CustomerPhoneTypes.MOBILE_NUMBER,
                                            TeleusrDefText = strPrimaryPhone
                                        };
                        custPhoneData.Add(custPhone);

                    }

                    if (workNumberEntered)
                    {
                        if (radioButtonWorkPhone.Checked)
                            strPrimaryPhone = "true";
                        else
                            strPrimaryPhone = string.Empty;
                        ContactVO custPhone = new ContactVO();
                        custPhone.ContactAreaCode = this.workPhoneAreaCode.Text;
                        custPhone.ContactExtension = this.workPhoneExtn.Text;
                        custPhone.ContactPhoneNumber = Commons.FormatPhoneNumberForDB(this.workPhoneNumber.Text);
                        custPhone.ContactType = CustomerPhoneTypes.WORK_NUMBER;
                        custPhone.CountryDialNumCode = this.workPhoneCountryCode.Text;
                        custPhone.TelecomNumType = CustomerPhoneTypes.VOICE_NUMBER;
                        custPhone.TeleusrDefText = strPrimaryPhone;
                        custPhoneData.Add(custPhone);
                    }
                    if (faxEntered)
                    {
                        strPrimaryPhone = string.Empty;
                        ContactVO custPhone = new ContactVO();
                        custPhone.ContactAreaCode = this.faxAreaCode.Text;
                        custPhone.ContactExtension = this.faxExtn.Text;
                        custPhone.ContactPhoneNumber = Commons.FormatPhoneNumberForDB(this.faxNumber.Text);
                        custPhone.ContactType = CustomerPhoneTypes.HOME_NUMBER;
                        custPhone.CountryDialNumCode = this.faxCountryCode.Text;
                        custPhone.TelecomNumType = CustomerPhoneTypes.FAX_NUMBER;
                        custPhone.TeleusrDefText = strPrimaryPhone;
                        custPhoneData.Add(custPhone);
                    }
                    if (pagerEntered)
                    {
                        strPrimaryPhone = string.Empty;
                        ContactVO custPhone = new ContactVO();
                        custPhone.ContactAreaCode = this.pagerAreaCode.Text.ToString();
                        custPhone.ContactExtension = this.pagerExtn.Text.ToString();
                        custPhone.ContactPhoneNumber = Commons.FormatPhoneNumberForDB(this.pagerNumber.Text);
                        custPhone.ContactType = CustomerPhoneTypes.HOME_NUMBER;
                        custPhone.CountryDialNumCode = this.pagerCountryCode.Text.ToString();
                        custPhone.TelecomNumType = CustomerPhoneTypes.PAGER_NUMBER;
                        custPhone.TeleusrDefText = strPrimaryPhone;
                        custPhoneData.Add(custPhone);
                    }

                    return custPhoneData;
                }
            }
            return null;

        }

        private void checkPhoneNumbersEntered()
        {
            numberOfPhoneNumbers = 0;
            if (homePhoneAreaCode.Text.Length > 0 && homePhoneNumber.Text.Length > 0)
            {
                homeNumberEntered = true;
                numberOfPhoneNumbers++;
            }
            if (cellPhoneAreaCode.Text.Length > 0 && cellPhoneNumber.Text.Length > 0)
            {
                cellNumberEntered = true;
                numberOfPhoneNumbers++;
            }
            if (workPhoneAreaCode.Text.Length > 0 && workPhoneNumber.Text.Length > 0)
            {
                workNumberEntered = true;
                numberOfPhoneNumbers++;
            }
            if (ShowFax)
            {
                if (faxAreaCode.Text.Length > 0 && faxNumber.Text.Length > 0)
                {
                    faxEntered = true;
                    numberOfPhoneNumbers++;
                }
            }
            if (ShowPager)
            {
                if (pagerAreaCode.Text.Length > 0 && pagerNumber.Text.Length > 0)
                {
                    pagerEntered = true;
                    numberOfPhoneNumbers++;
                }
            }


        }

        private void homePhoneAreaCode_TextChanged(object sender, EventArgs e)
        {
            if (homePhoneAreaCode.Text.Length > 0)
            {
                if (!radioButtonCellPhone.Checked && !radioButtonWorkPhone.Checked)
                    radioButtonHomePhone.Checked = true;
            }
            else
            {
                //If home phone was set as primary change the primary to cell or work 
                //depending on which one has numbers entered
                if (radioButtonHomePhone.Checked)
                {
                    radioButtonHomePhone.Checked = false;
                    if (cellPhoneAreaCode.Text.Length > 0 && cellPhoneNumber.Text.Length > 0)
                        radioButtonCellPhone.Checked = true;
                    else if (workPhoneAreaCode.Text.Length > 0 && workPhoneNumber.Text.Length > 0)
                        radioButtonWorkPhone.Checked = true;
                }
            }
            if (homePhoneAreaCode.Text.Length == homePhoneAreaCode.MaxLength)
            {
                this.SelectNextControl(homePhoneAreaCode, true, true, true, true);
                homePhoneExtn.Enabled = true;
                this.homePhoneCountryCode.Enabled = true;
            }
            else
            {
                this.homePhoneExtn.Enabled = false;
                homePhoneCountryCode.Enabled = false;
            }
        }

        private void cellPhoneAreaCode_TextChanged(object sender, EventArgs e)
        {
            if (cellPhoneAreaCode.Text.Length > 0)
            {
                if (!radioButtonHomePhone.Checked && !radioButtonWorkPhone.Checked)
                    radioButtonCellPhone.Checked = true;
            }
            else
            {
                //If cell phone was set as primary change the primary to home or work 
                //depending on which one has numbers entered
                if (radioButtonCellPhone.Checked)
                {
                    radioButtonCellPhone.Checked = false;
                    if (homePhoneAreaCode.Text.Length > 0 && homePhoneNumber.Text.Length > 0)
                        radioButtonHomePhone.Checked = true;
                    else if (workPhoneAreaCode.Text.Length > 0 && workPhoneNumber.Text.Length > 0)
                        radioButtonWorkPhone.Checked = true;
                }
            }
            if (cellPhoneAreaCode.Text.Length == cellPhoneAreaCode.MaxLength)
            {
                this.SelectNextControl(cellPhoneAreaCode, true, true, true, true);
                cellPhoneExtn.Enabled = true;
                cellPhoneCountryCode.Enabled = true;
            }
            else
            {
                cellPhoneExtn.Enabled = false;
                cellPhoneCountryCode.Enabled = false;
            }
        }

        private void workPhoneAreaCode_TextChanged(object sender, EventArgs e)
        {
            if (workPhoneAreaCode.Text.Length > 0)
            {
                if (!radioButtonHomePhone.Checked && !radioButtonCellPhone.Checked)
                    radioButtonWorkPhone.Checked = true;
            }
            else
            {
                //If work phone was set as primary change the primary to home or cell 
                //depending on which one has numbers entered
                if (radioButtonWorkPhone.Checked)
                {
                    radioButtonWorkPhone.Checked = false;
                    if (homePhoneAreaCode.Text.Length > 0 && homePhoneNumber.Text.Length > 0)
                        radioButtonHomePhone.Checked = true;
                    else if (workPhoneAreaCode.Text.Length > 0 && workPhoneNumber.Text.Length > 0)
                        radioButtonWorkPhone.Checked = true;
                }
            }
            if (workPhoneAreaCode.Text.Length == workPhoneAreaCode.MaxLength)
            {
                this.SelectNextControl(workPhoneAreaCode, true, true, true, true);
                workPhoneExtn.Enabled = true;
                workPhoneCountryCode.Enabled = true;
            }
            else
            {
                workPhoneExtn.Enabled = false;
                workPhoneCountryCode.Enabled = false;
            }
        }

        private void pagerAreaCode_TextChanged(object sender, EventArgs e)
        {
            if (pagerAreaCode.Text.Length == pagerAreaCode.MaxLength)
            {
                this.SelectNextControl(pagerAreaCode, true, true, true, true);
                pagerExtn.Enabled = true;
                pagerCountryCode.Enabled = true;
            }
            else
            {
                pagerExtn.Enabled = false;
                pagerCountryCode.Enabled = false;
            }
        }

        private void faxAreaCode_TextChanged(object sender, EventArgs e)
        {
            if (faxAreaCode.Text.Length == faxAreaCode.MaxLength)
            {
                this.SelectNextControl(faxAreaCode, true, true, true, true);
                faxExtn.Enabled = true;
                faxCountryCode.Enabled = true;
            }
            else
            {
                faxExtn.Enabled = false;
                faxCountryCode.Enabled = false;
            }
        }
        private void homePhoneAreaCode_Leave(object sender, EventArgs e)
        {
            if (homePhoneAreaCode.Text.Length == 0)
            {
                if (homePhoneNumber.Text.Length > 0)
                    MessageBox.Show(Commons.GetMessageString("HomeAreaCodeNotEntered"));
                //If cell phone was set as primary change the primary to home or work
                //depending on which one has numbers entered
                if (radioButtonHomePhone.Checked)
                {
                    radioButtonHomePhone.Checked = false;
                    if (cellPhoneAreaCode.Text.Length > 0 && cellPhoneNumber.Text.Length > 0)
                        radioButtonCellPhone.Checked = true;
                    else if (workPhoneAreaCode.Text.Length > 0 && workPhoneNumber.Text.Length > 0)
                        radioButtonWorkPhone.Checked = true;
                }
            }

        }

        private void cellPhoneAreaCode_Leave(object sender, EventArgs e)
        {
            if (cellPhoneAreaCode.Text.Length == 0)
            {
                if (cellPhoneNumber.Text.Length > 0)
                    MessageBox.Show(Commons.GetMessageString("CellAreaCodeNotEntered"));
                //If cell phone was set as primary change the primary to home or work
                //depending on which one has numbers entered
                if (radioButtonCellPhone.Checked)
                {
                    radioButtonCellPhone.Checked = false;
                    if (homePhoneAreaCode.Text.Length > 0 && homePhoneNumber.Text.Length > 0)
                        radioButtonHomePhone.Checked = true;
                    else if (workPhoneAreaCode.Text.Length > 0 && workPhoneNumber.Text.Length > 0)
                        radioButtonWorkPhone.Checked = true;
                }
            }
        }

        private void workPhoneAreaCode_Leave(object sender, EventArgs e)
        {
            if (workPhoneAreaCode.Text.Length == 0)
            {
                if (workPhoneNumber.Text.Length > 0)
                    MessageBox.Show(Commons.GetMessageString("WorkAreaCodeNotEntered"));
                //If work phone was set as primary change the primary to home or cell
                //depending on which one has numbers entered
                if (radioButtonWorkPhone.Checked)
                {
                    radioButtonWorkPhone.Checked = false;
                    if (homePhoneAreaCode.Text.Length > 0 && homePhoneNumber.Text.Length > 0)
                        radioButtonHomePhone.Checked = true;
                    else if (cellPhoneAreaCode.Text.Length > 0 && cellPhoneNumber.Text.Length > 0)
                        radioButtonCellPhone.Checked = true;
                }
            }

        }

        private void pagerAreaCode_Leave(object sender, EventArgs e)
        {
            if (pagerAreaCode.Text.Length == 0)
            {
                if (pagerNumber.Text.Length > 0)
                    MessageBox.Show(Commons.GetMessageString("PagerAreaCodeNotEntered"));
            }
        }

        private void faxAreaCode_Leave(object sender, EventArgs e)
        {
            if (faxAreaCode.Text.Length == 0)
            {
                if (faxNumber.Text.Length > 0)
                    MessageBox.Show(Commons.GetMessageString("FaxAreaCodeNotEntered"));
            }
        }

        private void homePhoneNumber_Leave(object sender, EventArgs e)
        {
            if (homePhoneAreaCode.Text.Length > 0)
            {
                if (homePhoneNumber.Text.Length == 0)
                {
                    MessageBox.Show(Commons.GetMessageString("HomeNumberNotEnteredError"));
                    homePhoneNumber.Focus();
                }
            }
            else
            {
                if (homePhoneNumber.Text.Length > 0)
                {
                    MessageBox.Show(Commons.GetMessageString("HomeAreaCodeNotEntered"));
                }
            }
        }

        private void cellPhoneNumber_Leave(object sender, EventArgs e)
        {
            if (cellPhoneAreaCode.Text.Length > 0)
            {
                if (cellPhoneNumber.Text.Length == 0)
                {
                    MessageBox.Show(Commons.GetMessageString("CellNumberNotEnteredError"));
                    cellPhoneNumber.Focus();
                }

            }
            else
            {
                if (cellPhoneNumber.Text.Length > 0)
                {
                    MessageBox.Show(Commons.GetMessageString("CellAreaCodeNotEntered"));
                }
            }
        }

        private void workPhoneNumber_Leave(object sender, EventArgs e)
        {
            if (workPhoneAreaCode.Text.Length > 0)
            {
                if (workPhoneNumber.Text.Length == 0)
                {
                    MessageBox.Show(Commons.GetMessageString("WorkNumberNotEnteredError"));
                    workPhoneNumber.Focus();
                }
            }
            else
            {
                if (workPhoneNumber.Text.Length > 0)
                {
                    MessageBox.Show(Commons.GetMessageString("WorkAreaCodeNotEntered"));
                }
            }
        }

        private void pagerNumber_Leave(object sender, EventArgs e)
        {
            if (pagerAreaCode.Text.Length > 0)
            {
                if (pagerNumber.Text.Length == 0)
                {
                    MessageBox.Show(Commons.GetMessageString("PagerNumberNotEnteredError"));
                    pagerNumber.Focus();
                }
            }
            else
            {
                if (pagerNumber.Text.Length > 0)
                {
                    MessageBox.Show(Commons.GetMessageString("PagerAreaCodeNotEntered"));
                }
            }
        }

        private void faxNumber_Leave(object sender, EventArgs e)
        {
            if (faxAreaCode.Text.Length > 0)
            {
                if (faxNumber.Text.Length == 0)
                {
                    MessageBox.Show(Commons.GetMessageString("FaxNumberNotEnteredError"));
                    faxNumber.Focus();
                }
            }
            else
            {
                if (faxNumber.Text.Length > 0)
                {
                    MessageBox.Show(Commons.GetMessageString("FaxAreaCodeNotEntered"));
                }
            }

        }



        protected override System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                int currentTabIndex = this.ActiveControl.TabIndex;
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
                if (this.ActiveControl.TabIndex == currentTabIndex)
                    return false;
                else
                    return true;
            }


            return base.ProcessDialogKey(keyData);

        }

  






    }
}
