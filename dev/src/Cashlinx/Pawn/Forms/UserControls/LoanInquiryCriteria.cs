using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.UserControls
{
    public partial class LoanInquiryCriteria : UserControl
    {
        //private bool _isValid;
        private int _index;
        //deal with customer id
        private UserControl _idAgentUsercontrol;
        private ComboBox _idTypeCombobox;
        private ComboBox _cboTemp;
        private TextBox _idValueTextbox;

        //[Category("Validation")]
        //[Description("Sets if the control is valid")]
        //[DefaultValue(false)]
        //[Browsable(false)]
        //public bool isValid
        //{
        //    get { return _isValid; }
        //    set { _isValid = value; }
        //}

        [Category("Index")]
        [Description("Assigns an index value to the criteria entered")]
        [DefaultValue(1)]
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        [Category("DataName")]
        [Description("Returns the Data name")]
        [DefaultValue("")]
        public ComboBoxData DataName
        {
            get { return (ComboBoxData)this.loaninquiryDataNameCombobox.SelectedValue; }
            set { this.loaninquiryDataNameCombobox.SelectedItem = value; }
        }

        [Category("DataType")]
        [Description("Returns the Data type")]
        [DefaultValue("")]
        public string[] DataType
        {
            get { return (string[])this.loaninquiryDataTypeCombobox.SelectedValue; }
            set { this.loaninquiryDataTypeCombobox.SelectedItem = value; }
        }

        [Category("SearchType")]
        [Description("Returns the Search type")]
        [DefaultValue("")]
        public string SearchType
        {
            get { return this.loaninquirySearchTypeCombobox.SelectedItem.ToString(); }
            set { this.loaninquirySearchTypeCombobox.SelectedItem = value; }
        }

        public LoanInquiryCriteria()
        {
            InitializeComponent();
        }
        //loads data types (customer, loan, item)
        private void LoanInquiryCriteria_Load(object sender, EventArgs e)
        {
            //load types of searches
            Array lstDataTypes = System.Enum.GetValues(typeof(InquiryDataTypes));
            this.loaninquiryDataTypeCombobox.Items.Add(InquiryConst.SELECT);

            foreach (InquiryDataTypes dataType in lstDataTypes)
            {
                this.loaninquiryDataTypeCombobox.Items.Add(dataType.ToString());
            }
            this.loaninquiryDataTypeCombobox.SelectedIndex = 0;

            //hide objects
            this.loaninquirySearchTypeCombobox.Visible = false;
            this.loaninquiryXLabel.Visible = false;
            this.loaninquiryCustomButtonAnd.Visible = false;

        }
        //loads data names (available columns for the data type selection)
        private void loaninquiryDataTypeCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.loaninquiryDataTypeCombobox.SelectedIndex > 0)
            {
                this.loaninquirySearchTypeCombobox.Visible = false;

                RemoveDataInputObjects();

                string[,] dataNames = null;

                switch ((InquiryDataTypes)Enum.Parse(typeof(InquiryDataTypes), this.loaninquiryDataTypeCombobox.SelectedItem.ToString()))
                {
                    case (InquiryDataTypes.Customer):
                        dataNames = InquiryType.Customer;
                        break;

                    case (InquiryDataTypes.Loan):
                        dataNames = InquiryType.Loan;
                        break;

                    case (InquiryDataTypes.Item):
                        dataNames = InquiryType.Item;
                        break;

                    default:
                        break;
                }

                InquiryCommon inquiryCommon = new InquiryCommon();
                inquiryCommon.PopulateComboBox(this.loaninquiryDataNameCombobox, dataNames, true);

                this.loaninquiryDataNameCombobox.SelectedIndex = 0;
            }
        }
        //calls the loading of objects needed 
        private void loaninquiryDataNameCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loaninquirySearchTypeCombobox.Items.Clear();

            if (this.loaninquiryDataNameCombobox.SelectedIndex > 0)
            {
                switch ((InquiryDataTypes)Enum.Parse(typeof(InquiryDataTypes), this.loaninquiryDataTypeCombobox.SelectedItem.ToString()))
                {
                    case (InquiryDataTypes.Customer):
                        CustomerDataNameSelected();
                        break;

                    case (InquiryDataTypes.Loan):
                        LoanDataNameSelected();
                        break;

                    case (InquiryDataTypes.Item):
                        ItemDataNameSelected();
                        break;
                }

                this.loaninquirySearchTypeCombobox.Visible = true;
                this.loaninquirySearchTypeCombobox.SelectedIndex = 0;
                ShowXandAndButtons();
            }
        }
        //to handle if range is selected
        private void loaninquirySearchTypeCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //need to add the "To" label and the additional data input object if Range is selected
            if (this.loaninquirySearchTypeCombobox.SelectedItem.ToString() == InquirySearchTypes.RANGE)
            {
                //need the type of the only non-"loaninquiry" control 
                List<Control> controls = FindControls(new string[] { "loaninquiry" }, this.Controls);
                //see if we already have 3 controls - if the last selection was Range then no change
                if (!controls.Count.Equals(3))
                {
                    Label lblDataInput = new Label();
                    lblDataInput.Name = "temp";
                    lblDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    lblDataInput.Left = controls[0].Left + controls[0].Width + 10;
                    lblDataInput.Text = "To";
                    lblDataInput.AutoSize = true;
                    this.Controls.Add(lblDataInput);

                    switch (controls[0].GetType().Name)
                    {
                        case "TextBox":
                            TextBox txtDataInput2 = new TextBox();
                            txtDataInput2.Name = "CriteriaValue";
                            txtDataInput2.Top = this.loaninquirySearchTypeCombobox.Top;
                            txtDataInput2.Left = lblDataInput.Left + lblDataInput.Width + 10;
                            txtDataInput2.Width = controls[0].Width;
                            this.Controls.Add(txtDataInput2);
                            break;

                        case "Date":
                            Date dateDataInput2 = new Date();
                            dateDataInput2.Name = "CriteriaValue";
                            dateDataInput2.Top = this.loaninquirySearchTypeCombobox.Top;
                            dateDataInput2.Left = lblDataInput.Left + lblDataInput.Width + 10;
                            dateDataInput2.Width = controls[0].Width;
                            this.Controls.Add(dateDataInput2);
                            break;

                        case "Zipcode":
                            Zipcode zipcodeDataInput2 = new Zipcode();
                            zipcodeDataInput2.Name = "CriteriaValue";
                            zipcodeDataInput2.Top = this.loaninquirySearchTypeCombobox.Top;
                            zipcodeDataInput2.Left = lblDataInput.Left + lblDataInput.Width + 10;
                            zipcodeDataInput2.Width = controls[0].Width;
                            this.Controls.Add(zipcodeDataInput2);
                            break;
                    }
                }
            }
        }
        //method for customer id agency
        private void _idTypeCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_idAgentUsercontrol != null)
            {
                _idAgentUsercontrol.Dispose();
            }
            switch (_idTypeCombobox.SelectedItem.ToString())
            {
                //add State user Control
                case StateIdTypeDescription.CW:
                case StateIdTypeDescription.DRIVER_LIC:
                case StateIdTypeDescription.STATE_ID:
                    _idAgentUsercontrol = new State();

                    break;

                default:
                    _idAgentUsercontrol = new Country();

                    break;
            }
            _idAgentUsercontrol.Top = _idTypeCombobox.Top;
            _idAgentUsercontrol.Left = _idTypeCombobox.Left + _idTypeCombobox.Width + 10;

            _cboTemp.Visible = false;

            this.Controls.Add(_idAgentUsercontrol);

            //position the textbox
            _idValueTextbox.Location = new Point(_idAgentUsercontrol.Top, _idAgentUsercontrol.Left + _idAgentUsercontrol.Width + 10);
            _idValueTextbox.Top = _idAgentUsercontrol.Top;
            _idValueTextbox.Left = _idAgentUsercontrol.Left + _idAgentUsercontrol.Width + 10;
        }
        //triggers adding another criteria
        private void loaninquiryCustomButtonAnd_Click(object sender, EventArgs e)
        {
            LoanInquiry.LoanInquiry loanInquiry = (LoanInquiry.LoanInquiry)this.Parent.Parent.Parent;
            loanInquiry.AddCriteria(this.Index);
        }
        //triggers removal of criteria
        private void loaninquiryXLabel_Click(object sender, EventArgs e)
        {
            LoanInquiry.LoanInquiry loanInquiry = (LoanInquiry.LoanInquiry)this.Parent.Parent.Parent;
            loanInquiry.RemoveCriteria(this.Index);
        }
        //clears out objects
        private void RemoveDataInputObjects()
        {
            //return all objects that are not loaninquiry* - these are the dynamic data entry controls
            List<Control> controls = FindControls(new string[] { "loaninquiry" }, this.Controls);
            //remove objects
            foreach (Control cntrl in controls)
            {
                this.Controls.Remove(cntrl);
                cntrl.Dispose();
            }
        }
        public List<Control> FindControls(string[] omitObjectList, ControlCollection controlCollection)
        {
            List<Control> controls = new List<Control>();

            //find all objects to remove
            for (int i = 0; i < controlCollection.Count; i++)
            {
                bool foundInList = false;
                Control cntrl = controlCollection[i];

                for (int j = 0; j < omitObjectList.Length; j++)
                {
                    if (cntrl.Name.Contains(omitObjectList[j]))
                    {
                        foundInList = true;
                    }
                }

                if (!foundInList)
                {
                    controls.Add(cntrl);
                }
            }

            return controls;
        }
        //handles the X's and And button
        private void ShowXandAndButtons()
        {
            if (this.Index > Convert.ToInt16(Properties.Settings.Default.LoanInquiryMinimumCriteria) - 1)
            {
                if (this.Index > Convert.ToInt16(Properties.Settings.Default.LoanInquiryMinimumCriteria))
                {
                    this.loaninquiryXLabel.Visible = true;
                }
                this.loaninquiryCustomButtonAnd.Visible = true;
            }
            else
            {
                this.loaninquiryXLabel.Visible = false;
                this.loaninquiryCustomButtonAnd.Visible = false;
            }
        }
        //manages customer data type criteria
        private void CustomerDataNameSelected()
        {
            RemoveDataInputObjects();

            TextBox txtDataInput = new TextBox();

            switch (this.loaninquiryDataNameCombobox.SelectedIndex)
            {
                case ((int)InquiryCustomerCriteria.LastName):
                case ((int)InquiryCustomerCriteria.FirstName):
                case ((int)InquiryCustomerCriteria.City):
                    this.loaninquirySearchTypeCombobox.Items.AddRange(new[] { InquirySearchTypes.EQUALS, InquirySearchTypes.INCLUDES });
                    txtDataInput.Name = "CriteriaValue";
                    txtDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    txtDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    txtDataInput.Width = 200;
                    this.Controls.Add(txtDataInput);
                    break;

                case ((int)InquiryCustomerCriteria.CustomerNumber):
                case ((int)InquiryCustomerCriteria.Phone):
                case ((int)InquiryCustomerCriteria.SocialSecurityNumber):
                    this.loaninquirySearchTypeCombobox.Items.Add(InquirySearchTypes.EQUALS);
                    txtDataInput.Name = "CriteriaValue";
                    txtDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    txtDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(txtDataInput);
                    break;

                case ((int)InquiryCustomerCriteria.Address):
                    this.loaninquirySearchTypeCombobox.Items.AddRange(new[] { InquirySearchTypes.INCLUDES, InquirySearchTypes.EQUALS });
                    TextBox txtDataInput1 = new TextBox();
                    txtDataInput1.Name = "CriteriaValue";
                    txtDataInput1.Top = this.loaninquirySearchTypeCombobox.Top;
                    txtDataInput1.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    txtDataInput1.Width = 200;
                    this.Controls.Add(txtDataInput1);
                    TextBox txtDataInput2 = new TextBox();
                    txtDataInput2.Name = "CriteriaValue";
                    txtDataInput2.Top = this.loaninquirySearchTypeCombobox.Top;
                    txtDataInput2.Left = txtDataInput1.Left + txtDataInput1.Width + 10;
                    txtDataInput2.Width = 200;
                    this.Controls.Add(txtDataInput2);
                    break;

                case ((int)InquiryCustomerCriteria.State):
                    this.loaninquirySearchTypeCombobox.Items.Add(InquirySearchTypes.EQUALS);
                    State stateDataInput = new State();
                    stateDataInput.Name = "CriteriaValue";
                    stateDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    stateDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(stateDataInput);
                    break;

                case ((int)InquiryCustomerCriteria.ZipCode):
                    this.loaninquirySearchTypeCombobox.Items.AddRange(new[] { InquirySearchTypes.EQUALS, InquirySearchTypes.GREATERTHAN, 
                                            InquirySearchTypes.LESSTHAN, InquirySearchTypes.RANGE });
                    Zipcode zipDataInput = new Zipcode();
                    zipDataInput.Name = "CriteriaValue";
                    zipDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    zipDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(zipDataInput);
                    break;

                case ((int)InquiryCustomerCriteria.CustomerID):
                    this.loaninquirySearchTypeCombobox.Items.Add(InquirySearchTypes.EQUALS);
                    _idTypeCombobox = new ComboBox();
                    _idTypeCombobox.Name = "CriteriaValue";
                    DataTable idTypeTable = GlobalDataAccessor.Instance.DesktopSession.IdTypeTable;
                    foreach (DataRow dr in idTypeTable.Rows)
                    {
                        _idTypeCombobox.Items.Add(dr["codedesc"]);
                    }
                    _idTypeCombobox.Items.Add(InquiryConst.SELECT);
                    _idTypeCombobox.Top = this.loaninquirySearchTypeCombobox.Top;
                    _idTypeCombobox.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(_idTypeCombobox);
                    _idTypeCombobox.SelectedIndexChanged += this._idTypeCombobox_SelectedIndexChanged;
                    //temp combobox until idType is selected
                    _cboTemp = new ComboBox();
                    _cboTemp.Name = "temp";
                    _cboTemp.Top = this.loaninquirySearchTypeCombobox.Top;
                    _cboTemp.Left = _idTypeCombobox.Left + _idTypeCombobox.Width + 10;
                    this.Controls.Add(_cboTemp);
                    _idValueTextbox = new TextBox();
                    _idValueTextbox.Name = "CriteriaValue";
                    _idValueTextbox.Top = this.loaninquirySearchTypeCombobox.Top;
                    _idValueTextbox.Left = _cboTemp.Left + _cboTemp.Width + 10;
                    this.Controls.Add(_idValueTextbox);
                    break;

                case ((int)InquiryCustomerCriteria.DateOfBirth):
                case ((int)InquiryCustomerCriteria.CustomerSince):
                    this.loaninquirySearchTypeCombobox.Items.AddRange(new[] { InquirySearchTypes.EQUALS, InquirySearchTypes.GREATERTHAN, 
                                            InquirySearchTypes.LESSTHAN, InquirySearchTypes.RANGE });
                    Date dateDataInput = new Date();
                    dateDataInput.Name = "CriteriaValue";
                    dateDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    dateDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(dateDataInput);
                    break;

                case ((int)InquiryCustomerCriteria.Age):
                case ((int)InquiryCustomerCriteria.Weight):
                case ((int)InquiryCustomerCriteria.Height):
                case ((int)InquiryCustomerCriteria.CAEmpNumber):
                    this.loaninquirySearchTypeCombobox.Items.AddRange(new[] { InquirySearchTypes.EQUALS, InquirySearchTypes.GREATERTHAN, 
                                            InquirySearchTypes.LESSTHAN, InquirySearchTypes.RANGE });
                    txtDataInput.Name = "CriteriaValue";
                    txtDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    txtDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(txtDataInput);
                    break;

                case ((int)InquiryCustomerCriteria.Sex):
                    this.loaninquirySearchTypeCombobox.Items.Add(InquirySearchTypes.EQUALS);
                    Gender genderDataInput = new Gender();
                    genderDataInput.Name = "CriteriaValue";
                    genderDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    genderDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(genderDataInput);
                    break;

                case ((int)InquiryCustomerCriteria.Race):
                    this.loaninquirySearchTypeCombobox.Items.Add(InquirySearchTypes.EQUALS);
                    Race raceDataInput = new Race();
                    raceDataInput.Name = "CriteriaValue";
                    raceDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    raceDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(raceDataInput);
                    break;

                case ((int)InquiryCustomerCriteria.Hair):
                    this.loaninquirySearchTypeCombobox.Items.Add(InquirySearchTypes.EQUALS);
                    Haircolor hairDataInput = new Haircolor();
                    hairDataInput.Name = "CriteriaValue";
                    hairDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    hairDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(hairDataInput);
                    break;

                case ((int)InquiryCustomerCriteria.Eyes):
                    this.loaninquirySearchTypeCombobox.Items.Add(InquirySearchTypes.EQUALS);
                    EyeColor eyeDataInput = new EyeColor();
                    eyeDataInput.Name = "CriteriaValue";
                    eyeDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    eyeDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(eyeDataInput);
                    break;

                case ((int)InquiryCustomerCriteria.Comments):
                    this.loaninquirySearchTypeCombobox.Items.AddRange(new string[] { InquirySearchTypes.INCLUDES, InquirySearchTypes.EQUALS });
                    txtDataInput.Name = "CriteriaValue";
                    txtDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    txtDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(txtDataInput);
                    break;
            }
        }
        //manages loan data type criteria
        private void LoanDataNameSelected()
        {
            RemoveDataInputObjects();

            TextBox txtDataInput = new TextBox();
            switch (this.loaninquiryDataNameCombobox.SelectedIndex)
            {
                case ((int)InquiryLoanCriteria.TicketNumber):
                case ((int)InquiryLoanCriteria.TimeMade):
                case ((int)InquiryLoanCriteria.LoanAmount):
                case ((int)InquiryLoanCriteria.PreviousTicketNumber):
                case ((int)InquiryLoanCriteria.InterestAmount):
                case ((int)InquiryLoanCriteria.ServiceCharge):
                case ((int)InquiryLoanCriteria.OriginalTicketNumber):
                case ((int)InquiryLoanCriteria.LateCharge):
                case ((int)InquiryLoanCriteria.RefundAmount):
                    this.loaninquirySearchTypeCombobox.Items.AddRange(new string[] { InquirySearchTypes.EQUALS, InquirySearchTypes.GREATERTHAN, 
                                            InquirySearchTypes.LESSTHAN, InquirySearchTypes.RANGE });
                    txtDataInput.Name = "CriteriaValue";
                    txtDataInput = new TextBox();
                    txtDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    txtDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(txtDataInput);
                    break;

                case ((int)InquiryLoanCriteria.DateMade):
                case ((int)InquiryLoanCriteria.DueDate):
                case ((int)InquiryLoanCriteria.PFIEligibleDateOrLastDayOfGrace):
                case ((int)InquiryLoanCriteria.PFIDay):
                case ((int)InquiryLoanCriteria.StatusDate):
                case ((int)InquiryLoanCriteria.PFINotificationDate):
                    this.loaninquirySearchTypeCombobox.Items.AddRange(new string[] { InquirySearchTypes.EQUALS, InquirySearchTypes.GREATERTHAN, 
                                            InquirySearchTypes.LESSTHAN, InquirySearchTypes.RANGE });
                    Date dateDataInput = new Date();
                    dateDataInput.Name = "CriteriaValue";
                    dateDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    dateDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(dateDataInput);
                    break;

                case ((int)InquiryLoanCriteria.LoanStatus):
                    this.loaninquirySearchTypeCombobox.Items.AddRange(new string[] { InquirySearchTypes.EQUALS, InquirySearchTypes.INCLUDES });
                    ComboBox cboDataInput1 = new ComboBox();
                    cboDataInput1.Name = "CriteriaValue";
                    cboDataInput1.Top = this.loaninquirySearchTypeCombobox.Top;
                    cboDataInput1.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(cboDataInput1);
                    break;

                case ((int)InquiryLoanCriteria.ReasonCode):
                    this.loaninquirySearchTypeCombobox.Items.Add(InquirySearchTypes.EQUALS);
                    ComboBox cboDataInput2 = new ComboBox();
                    cboDataInput2.Name = "CriteriaValue";
                    cboDataInput2.Top = this.loaninquirySearchTypeCombobox.Top;
                    cboDataInput2.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(cboDataInput2);
                    break;

                case ((int)InquiryLoanCriteria.InterestAmountNegotiated):
                    this.loaninquirySearchTypeCombobox.Items.Add(InquirySearchTypes.EQUALS);
                    ComboBox cboDataInput3 = new ComboBox();
                    cboDataInput3.Name = "CriteriaValue";
                    cboDataInput3.Top = this.loaninquirySearchTypeCombobox.Top;
                    cboDataInput3.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(cboDataInput3);
                    break;

                case ((int)InquiryLoanCriteria.ServiceChargeNegotiated):
                    this.loaninquirySearchTypeCombobox.Items.Add(InquirySearchTypes.EQUALS);
                    ComboBox cboDataInput4 = new ComboBox();
                    cboDataInput4.Name = "CriteriaValue";
                    cboDataInput4.Top = this.loaninquirySearchTypeCombobox.Top;
                    cboDataInput4.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(cboDataInput4);
                    break;

                case ((int)InquiryLoanCriteria.Hold):
                    this.loaninquirySearchTypeCombobox.Items.Add(InquirySearchTypes.EQUALS);
                    ComboBox cboDataInput5 = new ComboBox();
                    cboDataInput5.Name = "CriteriaValue";
                    cboDataInput5.Top = this.loaninquirySearchTypeCombobox.Top;
                    cboDataInput5.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(cboDataInput5);
                    break;

                case ((int)InquiryLoanCriteria.Extend):
                    this.loaninquirySearchTypeCombobox.Items.Add(InquirySearchTypes.EQUALS);
                    ComboBox cboDataInput6 = new ComboBox();
                    cboDataInput6.Name = "CriteriaValue";
                    cboDataInput6.Top = this.loaninquirySearchTypeCombobox.Top;
                    cboDataInput6.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(cboDataInput6);
                    break;

                case ((int)InquiryLoanCriteria.Clothing):
                    this.loaninquirySearchTypeCombobox.Items.AddRange(new string[] { InquirySearchTypes.INCLUDES, InquirySearchTypes.EQUALS });
                    txtDataInput.Name = "CriteriaValue";
                    txtDataInput = new TextBox();
                    txtDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    txtDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(txtDataInput);
                    break;
            }
        }
        //manages item data type criteria
        private void ItemDataNameSelected()
        {

            RemoveDataInputObjects();

            TextBox txtDataInput = new TextBox();
            switch (this.loaninquiryDataNameCombobox.SelectedIndex)
            {

                case ((int)InquiryItemCriteria.Category):
                    this.loaninquirySearchTypeCombobox.Items.Add(InquirySearchTypes.EQUALS);
                    txtDataInput.Name = "CriteriaValue";
                    txtDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    txtDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(txtDataInput);
                    break;

                case ((int)InquiryItemCriteria.Status):
                    this.loaninquirySearchTypeCombobox.Items.Add(InquirySearchTypes.EQUALS);
                    ComboBox cboDataInput1 = new ComboBox();
                    cboDataInput1.Name = "CriteriaValue";
                    cboDataInput1.Top = this.loaninquirySearchTypeCombobox.Top;
                    cboDataInput1.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(cboDataInput1);
                    break;

                case ((int)InquiryItemCriteria.LoanAmount):
                case ((int)InquiryItemCriteria.PFIAmount):
                case ((int)InquiryItemCriteria.RetailAmount):
                case ((int)InquiryItemCriteria.ICN):
                    this.loaninquirySearchTypeCombobox.Items.AddRange(new string[] { InquirySearchTypes.EQUALS, InquirySearchTypes.GREATERTHAN, 
                                            InquirySearchTypes.LESSTHAN, InquirySearchTypes.RANGE });
                    txtDataInput.Name = "CriteriaValue";
                    txtDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    txtDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(txtDataInput);
                    break;

                case ((int)InquiryItemCriteria.Location):
                    this.loaninquirySearchTypeCombobox.Items.Add(InquirySearchTypes.EQUALS);
                    TextBox txtDataInput1 = new TextBox();
                    txtDataInput1.Name = "CriteriaValue";
                    txtDataInput1.Top = this.loaninquirySearchTypeCombobox.Top;
                    txtDataInput1.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(txtDataInput1);
                    TextBox txtDataInput2 = new TextBox();
                    txtDataInput2.Name = "CriteriaValue";
                    txtDataInput2.Top = this.loaninquirySearchTypeCombobox.Top;
                    txtDataInput2.Left = txtDataInput1.Left + txtDataInput1.Width + 10; ;
                    this.Controls.Add(txtDataInput2);
                    TextBox txtDataInput3 = new TextBox();
                    txtDataInput3.Name = "CriteriaValue";
                    txtDataInput3.Top = this.loaninquirySearchTypeCombobox.Top;
                    txtDataInput3.Left = txtDataInput2.Left + txtDataInput2.Width + 10; ;
                    this.Controls.Add(txtDataInput3);
                    break;

                case ((int)InquiryItemCriteria.Manufacturer):
                case ((int)InquiryItemCriteria.ModelNumber):
                case ((int)InquiryItemCriteria.SerialNumber):
                    this.loaninquirySearchTypeCombobox.Items.AddRange(new string[] { InquirySearchTypes.EQUALS, InquirySearchTypes.CONTAINS });
                    txtDataInput.Name = "CriteriaValue";
                    txtDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    txtDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(txtDataInput);
                    break;

                case ((int)InquiryItemCriteria.Description):
                    this.loaninquirySearchTypeCombobox.Items.Add(InquirySearchTypes.CONTAINS);
                    txtDataInput.Name = "CriteriaValue";
                    txtDataInput.Top = this.loaninquirySearchTypeCombobox.Top;
                    txtDataInput.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(txtDataInput);
                    break;

                case ((int)InquiryItemCriteria.JewelryCase):
                    this.loaninquirySearchTypeCombobox.Items.Add(InquirySearchTypes.EQUALS);
                    ComboBox cboDataInput2 = new ComboBox();
                    cboDataInput2.Name = "CriteriaValue";
                    cboDataInput2.Top = this.loaninquirySearchTypeCombobox.Top;
                    cboDataInput2.Left = this.loaninquirySearchTypeCombobox.Left + this.loaninquirySearchTypeCombobox.Width + 10;
                    this.Controls.Add(cboDataInput2);
                    break;
            }
        }
    }
}
