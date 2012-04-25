//using Common.Libraries.Forms.Components;

using Common.Libraries.Forms.Components;
//using Common.Libraries.Objects.Customer;
using Support.Forms;
using Support.Libraries.Forms.Components;

namespace Support.Forms.Customer
{
    partial class LookupCustomer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LookupCustomer));
            this.lookupCustomerDialogLabel = new System.Windows.Forms.Label();
            this.customerResultsPanel = new System.Windows.Forms.Panel();
            this.lookupCustomerInfoLabel = new System.Windows.Forms.Label();
            this.phoneNum2TextBox = new Common.Libraries.Forms.Components.CustomTextBox();
            this.phoneNum1TextBox = new Common.Libraries.Forms.Components.CustomTextBox();
            this.phoneAreaCodeTextBox = new Common.Libraries.Forms.Components.CustomTextBox();
            this.labelHyphen = new System.Windows.Forms.Label();
            this.labelCloseBracket = new System.Windows.Forms.Label();
            this.label1OpenBracket = new System.Windows.Forms.Label();
            this.labelCustomerPhoneNumber = new System.Windows.Forms.Label();
            this.lookupCustomerCustInfoPanel = new System.Windows.Forms.Panel();
            this.dateOfBirth = new Support.Forms.Date();
            this.BnkAcctNum = new Common.Libraries.Forms.Components.CustomTextBox();
            this.BnkAcctNumLabel = new Common.Libraries.Forms.Components.CustomLabel();
            this.CusInfoState = new Support.Forms.State();
            this.StateLabel = new Common.Libraries.Forms.Components.CustomLabel();
            this.City = new Common.Libraries.Forms.Components.CustomTextBox();
            this.CityLabel = new Common.Libraries.Forms.Components.CustomLabel();
            this.SSNShort = new Common.Libraries.Forms.Components.CustomTextBox();
            this.labelSSNShort = new System.Windows.Forms.Label();
            this.lookupCustomerFirstName = new Common.Libraries.Forms.Components.CustomTextBox();
            this.lookupCustomerLastName = new Common.Libraries.Forms.Components.CustomTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelCustDateOfBirth = new System.Windows.Forms.Label();
            this.labelCustFirstName = new System.Windows.Forms.Label();
            this.labelCustLastName = new System.Windows.Forms.Label();
            this.CustomerInfoRadioBtn = new System.Windows.Forms.RadioButton();
            this.lookupCustomerIDSearchPanel = new System.Windows.Forms.Panel();
            this.lookupCustomerIDNumber = new Common.Libraries.Forms.Components.CustomTextBox();
            this.idIssuer1 = new Support.Forms.State();
            this.IdType = new Support.Forms.IDType();
            this.labelIDType = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.labelIDIssuer = new System.Windows.Forms.Label();
            this.labelIDNumber = new System.Windows.Forms.Label();
            this.IDSearchRadioBtn = new System.Windows.Forms.RadioButton();
            this.lookupCustomerCustNumberPanel = new System.Windows.Forms.Panel();
            this.lookupCustomerCustNumber = new Common.Libraries.Forms.Components.CustomTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.labelCustomerNumber = new System.Windows.Forms.Label();
            this.CustNumberRadioBtn = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.panelSSN = new System.Windows.Forms.Panel();
            this.socialSecurityNumber = new Support.Forms.SocialSecurityNumber();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelSSN = new System.Windows.Forms.Label();
            this.radioButtonSSN = new System.Windows.Forms.RadioButton();
            this.toolTipLookupCustomer = new System.Windows.Forms.ToolTip(this.components);
            this.errorLabel = new System.Windows.Forms.Label();
            this.customButtonFind = new Support.Libraries.Forms.Components.SupportButton();
            this.customButtonClear = new Support.Libraries.Forms.Components.SupportButton();
            this.customButtonCancel = new Support.Libraries.Forms.Components.SupportButton();
            this.customButtonAddCustomer = new Common.Libraries.Forms.Components.CustomButton();
            this.LookupCustomerLoanTicketPanel = new System.Windows.Forms.Panel();
            this.ShopNumber = new Common.Libraries.Forms.Components.CustomTextBox();
            this.ShopNumberLabel = new Common.Libraries.Forms.Components.CustomLabel();
            this.ProductType = new System.Windows.Forms.ComboBox();
            this.ProductTypeLabel = new Common.Libraries.Forms.Components.CustomLabel();
            this.LoanNumber = new Common.Libraries.Forms.Components.CustomTextBox();
            this.LoanNumberLabel = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.LoanTicketRadioBtn = new System.Windows.Forms.RadioButton();
            this.lookupCustomerCustInfoPanel.SuspendLayout();
            this.lookupCustomerIDSearchPanel.SuspendLayout();
            this.lookupCustomerCustNumberPanel.SuspendLayout();
            this.panelSSN.SuspendLayout();
            this.LookupCustomerLoanTicketPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lookupCustomerDialogLabel
            // 
            this.lookupCustomerDialogLabel.BackColor = System.Drawing.Color.Transparent;
            this.lookupCustomerDialogLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupCustomerDialogLabel.ForeColor = System.Drawing.Color.White;
            this.lookupCustomerDialogLabel.Location = new System.Drawing.Point(287, 36);
            this.lookupCustomerDialogLabel.Name = "lookupCustomerDialogLabel";
            this.lookupCustomerDialogLabel.Size = new System.Drawing.Size(134, 19);
            this.lookupCustomerDialogLabel.TabIndex = 2;
            this.lookupCustomerDialogLabel.Text = "Lookup Customer";
            // 
            // customerResultsPanel
            // 
            this.customerResultsPanel.AutoScroll = true;
            this.customerResultsPanel.AutoScrollMinSize = new System.Drawing.Size(448, 161);
            this.customerResultsPanel.BackgroundImage = global::Support.Properties.Resources.dark_dialog_frame;
            this.customerResultsPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.customerResultsPanel.Location = new System.Drawing.Point(140, 691);
            this.customerResultsPanel.MaximumSize = new System.Drawing.Size(448, 161);
            this.customerResultsPanel.MinimumSize = new System.Drawing.Size(448, 0);
            this.customerResultsPanel.Name = "customerResultsPanel";
            this.customerResultsPanel.Size = new System.Drawing.Size(448, 0);
            this.customerResultsPanel.TabIndex = 5;
            // 
            // lookupCustomerInfoLabel
            // 
            this.lookupCustomerInfoLabel.BackColor = System.Drawing.Color.Transparent;
            this.lookupCustomerInfoLabel.ForeColor = System.Drawing.Color.Black;
            this.lookupCustomerInfoLabel.Location = new System.Drawing.Point(59, 83);
            this.lookupCustomerInfoLabel.Name = "lookupCustomerInfoLabel";
            this.lookupCustomerInfoLabel.Size = new System.Drawing.Size(261, 13);
            this.lookupCustomerInfoLabel.TabIndex = 15;
            this.lookupCustomerInfoLabel.Text = "Select the search type and enter your search criteria";
            // 
            // phoneNum2TextBox
            // 
            this.phoneNum2TextBox.AllowOnlyNumbers = true;
            this.phoneNum2TextBox.BackColor = System.Drawing.Color.White;
            this.phoneNum2TextBox.CausesValidation = false;
            this.phoneNum2TextBox.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.phoneNum2TextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phoneNum2TextBox.Location = new System.Drawing.Point(293, 70);
            this.phoneNum2TextBox.MaxLength = 4;
            this.phoneNum2TextBox.Name = "phoneNum2TextBox";
            this.phoneNum2TextBox.RegularExpression = true;
            this.phoneNum2TextBox.Size = new System.Drawing.Size(53, 21);
            this.phoneNum2TextBox.TabIndex = 6;
            this.phoneNum2TextBox.ValidationExpression = "^\\d{4}$";
            // 
            // phoneNum1TextBox
            // 
            this.phoneNum1TextBox.AllowOnlyNumbers = true;
            this.phoneNum1TextBox.BackColor = System.Drawing.Color.White;
            this.phoneNum1TextBox.CausesValidation = false;
            this.phoneNum1TextBox.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.phoneNum1TextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phoneNum1TextBox.Location = new System.Drawing.Point(228, 70);
            this.phoneNum1TextBox.MaxLength = 3;
            this.phoneNum1TextBox.Name = "phoneNum1TextBox";
            this.phoneNum1TextBox.RegularExpression = true;
            this.phoneNum1TextBox.Size = new System.Drawing.Size(44, 21);
            this.phoneNum1TextBox.TabIndex = 5;
            this.phoneNum1TextBox.ValidationExpression = "^\\d{3}$";
            this.phoneNum1TextBox.TextChanged += new System.EventHandler(this.phoneNum1TextBox_TextChanged);
            // 
            // phoneAreaCodeTextBox
            // 
            this.phoneAreaCodeTextBox.AllowOnlyNumbers = true;
            this.phoneAreaCodeTextBox.BackColor = System.Drawing.Color.White;
            this.phoneAreaCodeTextBox.CausesValidation = false;
            this.phoneAreaCodeTextBox.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.phoneAreaCodeTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phoneAreaCodeTextBox.Location = new System.Drawing.Point(173, 70);
            this.phoneAreaCodeTextBox.MaxLength = 3;
            this.phoneAreaCodeTextBox.Name = "phoneAreaCodeTextBox";
            this.phoneAreaCodeTextBox.RegularExpression = true;
            this.phoneAreaCodeTextBox.Size = new System.Drawing.Size(41, 21);
            this.phoneAreaCodeTextBox.TabIndex = 4;
            this.phoneAreaCodeTextBox.ValidationExpression = "^\\d{3}$";
            this.phoneAreaCodeTextBox.TextChanged += new System.EventHandler(this.phoneAreaCodeTextBox_TextChanged);
            // 
            // labelHyphen
            // 
            this.labelHyphen.Location = new System.Drawing.Point(278, 77);
            this.labelHyphen.Name = "labelHyphen";
            this.labelHyphen.Size = new System.Drawing.Size(11, 13);
            this.labelHyphen.TabIndex = 20;
            this.labelHyphen.Text = "-";
            // 
            // labelCloseBracket
            // 
            this.labelCloseBracket.Location = new System.Drawing.Point(214, 73);
            this.labelCloseBracket.Name = "labelCloseBracket";
            this.labelCloseBracket.Size = new System.Drawing.Size(10, 13);
            this.labelCloseBracket.TabIndex = 21;
            this.labelCloseBracket.Text = ")";
            // 
            // label1OpenBracket
            // 
            this.label1OpenBracket.Location = new System.Drawing.Point(163, 74);
            this.label1OpenBracket.Name = "label1OpenBracket";
            this.label1OpenBracket.Size = new System.Drawing.Size(11, 13);
            this.label1OpenBracket.TabIndex = 22;
            this.label1OpenBracket.Text = "(";
            // 
            // labelCustomerPhoneNumber
            // 
            this.labelCustomerPhoneNumber.Location = new System.Drawing.Point(16, 70);
            this.labelCustomerPhoneNumber.Name = "labelCustomerPhoneNumber";
            this.labelCustomerPhoneNumber.Size = new System.Drawing.Size(126, 13);
            this.labelCustomerPhoneNumber.TabIndex = 13;
            this.labelCustomerPhoneNumber.Text = "Customer Phone Number";
            // 
            // lookupCustomerCustInfoPanel
            // 
            this.lookupCustomerCustInfoPanel.BackColor = System.Drawing.Color.Transparent;
            this.lookupCustomerCustInfoPanel.Controls.Add(this.dateOfBirth);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.BnkAcctNum);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.BnkAcctNumLabel);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.CusInfoState);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.StateLabel);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.City);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.CityLabel);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.SSNShort);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.labelSSNShort);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.phoneNum2TextBox);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.phoneNum1TextBox);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.phoneAreaCodeTextBox);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.lookupCustomerFirstName);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.labelHyphen);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.lookupCustomerLastName);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.groupBox3);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.labelCloseBracket);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.labelCustDateOfBirth);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.label1OpenBracket);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.labelCustFirstName);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.labelCustLastName);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.CustomerInfoRadioBtn);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.labelCustomerPhoneNumber);
            this.lookupCustomerCustInfoPanel.Location = new System.Drawing.Point(63, 191);
            this.lookupCustomerCustInfoPanel.Name = "lookupCustomerCustInfoPanel";
            this.lookupCustomerCustInfoPanel.Size = new System.Drawing.Size(622, 201);
            this.lookupCustomerCustInfoPanel.TabIndex = 0;
            // 
            // dateOfBirth
            // 
            this.dateOfBirth.CausesValidation = false;
            this.dateOfBirth.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.dateOfBirth.Location = new System.Drawing.Point(173, 150);
            this.dateOfBirth.Name = "dateOfBirth";
            this.dateOfBirth.Size = new System.Drawing.Size(99, 20);
            this.dateOfBirth.TabIndex = 9;
            this.dateOfBirth.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.dateOfBirth.ValidationExpression = "^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.](19|20)\\d\\d$";
            this.dateOfBirth.Leave += new System.EventHandler(this.dateOfBirth_Leave);
            // 
            // BnkAcctNum
            // 
            this.BnkAcctNum.CausesValidation = false;
            this.BnkAcctNum.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.BnkAcctNum.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BnkAcctNum.Location = new System.Drawing.Point(400, 151);
            this.BnkAcctNum.Name = "BnkAcctNum";
            this.BnkAcctNum.Size = new System.Drawing.Size(176, 21);
            this.BnkAcctNum.TabIndex = 10;
            this.BnkAcctNum.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // BnkAcctNumLabel
            // 
            this.BnkAcctNumLabel.AutoSize = true;
            this.BnkAcctNumLabel.Location = new System.Drawing.Point(285, 151);
            this.BnkAcctNumLabel.Name = "BnkAcctNumLabel";
            this.BnkAcctNumLabel.Size = new System.Drawing.Size(112, 13);
            this.BnkAcctNumLabel.TabIndex = 17;
            this.BnkAcctNumLabel.Text = "Bank Account Number";
            // 
            // CusInfoState
            // 
            this.CusInfoState.BackColor = System.Drawing.Color.Transparent;
            this.CusInfoState.CausesValidation = false;
            this.CusInfoState.DisplayCode = false;
            this.CusInfoState.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CusInfoState.ForeColor = System.Drawing.Color.Black;
            this.CusInfoState.Location = new System.Drawing.Point(400, 180);
            this.CusInfoState.Name = "CusInfoState";
            this.CusInfoState.selectedValue = global::Support.Properties.Resources.OverrideMachineName;
            this.CusInfoState.Size = new System.Drawing.Size(100, 21);
            this.CusInfoState.TabIndex = 12;
            // 
            // StateLabel
            // 
            this.StateLabel.AutoSize = true;
            this.StateLabel.Location = new System.Drawing.Point(285, 183);
            this.StateLabel.Name = "StateLabel";
            this.StateLabel.Size = new System.Drawing.Size(33, 13);
            this.StateLabel.TabIndex = 18;
            this.StateLabel.Text = "State";
            // 
            // City
            // 
            this.City.CausesValidation = false;
            this.City.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.City.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.City.Location = new System.Drawing.Point(173, 178);
            this.City.Name = "City";
            this.City.Size = new System.Drawing.Size(99, 21);
            this.City.TabIndex = 11;
            this.City.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // CityLabel
            // 
            this.CityLabel.AutoSize = true;
            this.CityLabel.Location = new System.Drawing.Point(16, 179);
            this.CityLabel.Name = "CityLabel";
            this.CityLabel.Size = new System.Drawing.Size(26, 13);
            this.CityLabel.TabIndex = 19;
            this.CityLabel.Text = "City";
            // 
            // SSNShort
            // 
            this.SSNShort.CausesValidation = false;
            this.SSNShort.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.SSNShort.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SSNShort.Location = new System.Drawing.Point(173, 39);
            this.SSNShort.MaxLength = 4;
            this.SSNShort.Name = "SSNShort";
            this.SSNShort.Size = new System.Drawing.Size(41, 21);
            this.SSNShort.TabIndex = 3;
            this.SSNShort.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // labelSSNShort
            // 
            this.labelSSNShort.AutoSize = true;
            this.labelSSNShort.Location = new System.Drawing.Point(18, 39);
            this.labelSSNShort.Name = "labelSSNShort";
            this.labelSSNShort.Size = new System.Drawing.Size(79, 13);
            this.labelSSNShort.TabIndex = 12;
            this.labelSSNShort.Text = "(Last 4 of SSN)";
            // 
            // lookupCustomerFirstName
            // 
            this.lookupCustomerFirstName.BackColor = System.Drawing.Color.White;
            this.lookupCustomerFirstName.CausesValidation = false;
            this.lookupCustomerFirstName.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.lookupCustomerFirstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupCustomerFirstName.Location = new System.Drawing.Point(173, 123);
            this.lookupCustomerFirstName.MaxLength = 40;
            this.lookupCustomerFirstName.Name = "lookupCustomerFirstName";
            this.lookupCustomerFirstName.Size = new System.Drawing.Size(220, 21);
            this.lookupCustomerFirstName.TabIndex = 8;
            this.lookupCustomerFirstName.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // lookupCustomerLastName
            // 
            this.lookupCustomerLastName.BackColor = System.Drawing.Color.White;
            this.lookupCustomerLastName.CausesValidation = false;
            this.lookupCustomerLastName.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.lookupCustomerLastName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupCustomerLastName.Location = new System.Drawing.Point(173, 97);
            this.lookupCustomerLastName.MaxLength = 40;
            this.lookupCustomerLastName.Name = "lookupCustomerLastName";
            this.lookupCustomerLastName.Size = new System.Drawing.Size(220, 21);
            this.lookupCustomerLastName.TabIndex = 7;
            this.lookupCustomerLastName.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(3, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(622, 2);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            // 
            // labelCustDateOfBirth
            // 
            this.labelCustDateOfBirth.Location = new System.Drawing.Point(16, 150);
            this.labelCustDateOfBirth.Name = "labelCustDateOfBirth";
            this.labelCustDateOfBirth.Size = new System.Drawing.Size(68, 20);
            this.labelCustDateOfBirth.TabIndex = 16;
            this.labelCustDateOfBirth.Text = "Date of Birth";
            // 
            // labelCustFirstName
            // 
            this.labelCustFirstName.Location = new System.Drawing.Point(16, 123);
            this.labelCustFirstName.Name = "labelCustFirstName";
            this.labelCustFirstName.Size = new System.Drawing.Size(58, 21);
            this.labelCustFirstName.TabIndex = 15;
            this.labelCustFirstName.Text = "First Name";
            // 
            // labelCustLastName
            // 
            this.labelCustLastName.Location = new System.Drawing.Point(17, 97);
            this.labelCustLastName.Name = "labelCustLastName";
            this.labelCustLastName.Size = new System.Drawing.Size(67, 21);
            this.labelCustLastName.TabIndex = 14;
            this.labelCustLastName.Text = "Last Name";
            // 
            // CustomerInfoRadioBtn
            // 
            this.CustomerInfoRadioBtn.CausesValidation = false;
            this.CustomerInfoRadioBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomerInfoRadioBtn.Location = new System.Drawing.Point(13, 4);
            this.CustomerInfoRadioBtn.Name = "CustomerInfoRadioBtn";
            this.CustomerInfoRadioBtn.Size = new System.Drawing.Size(151, 17);
            this.CustomerInfoRadioBtn.TabIndex = 2;
            this.CustomerInfoRadioBtn.Text = "Customer Information";
            this.CustomerInfoRadioBtn.UseVisualStyleBackColor = true;
            this.CustomerInfoRadioBtn.CheckedChanged += new System.EventHandler(this.CustomerInfoRadioBtn_CheckedChanged);
            // 
            // lookupCustomerIDSearchPanel
            // 
            this.lookupCustomerIDSearchPanel.BackColor = System.Drawing.Color.Transparent;
            this.lookupCustomerIDSearchPanel.Controls.Add(this.lookupCustomerIDNumber);
            this.lookupCustomerIDSearchPanel.Controls.Add(this.idIssuer1);
            this.lookupCustomerIDSearchPanel.Controls.Add(this.IdType);
            this.lookupCustomerIDSearchPanel.Controls.Add(this.labelIDType);
            this.lookupCustomerIDSearchPanel.Controls.Add(this.groupBox4);
            this.lookupCustomerIDSearchPanel.Controls.Add(this.labelIDIssuer);
            this.lookupCustomerIDSearchPanel.Controls.Add(this.labelIDNumber);
            this.lookupCustomerIDSearchPanel.Controls.Add(this.IDSearchRadioBtn);
            this.lookupCustomerIDSearchPanel.Location = new System.Drawing.Point(63, 397);
            this.lookupCustomerIDSearchPanel.Name = "lookupCustomerIDSearchPanel";
            this.lookupCustomerIDSearchPanel.Size = new System.Drawing.Size(622, 59);
            this.lookupCustomerIDSearchPanel.TabIndex = 20;
            // 
            // lookupCustomerIDNumber
            // 
            this.lookupCustomerIDNumber.BackColor = System.Drawing.Color.White;
            this.lookupCustomerIDNumber.CausesValidation = false;
            this.lookupCustomerIDNumber.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.lookupCustomerIDNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupCustomerIDNumber.Location = new System.Drawing.Point(288, 31);
            this.lookupCustomerIDNumber.Name = "lookupCustomerIDNumber";
            this.lookupCustomerIDNumber.Size = new System.Drawing.Size(100, 21);
            this.lookupCustomerIDNumber.TabIndex = 15;
            this.lookupCustomerIDNumber.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // idIssuer1
            // 
            this.idIssuer1.BackColor = System.Drawing.Color.Transparent;
            this.idIssuer1.CausesValidation = false;
            this.idIssuer1.DisplayCode = true;
            this.idIssuer1.ForeColor = System.Drawing.Color.Black;
            this.idIssuer1.Location = new System.Drawing.Point(438, 31);
            this.idIssuer1.Name = "idIssuer1";
            this.idIssuer1.selectedValue = global::Support.Properties.Resources.OverrideMachineName;
            this.idIssuer1.Size = new System.Drawing.Size(47, 21);
            this.idIssuer1.TabIndex = 16;
            // 
            // IdType
            // 
            this.IdType.CausesValidation = false;
            this.IdType.Location = new System.Drawing.Point(46, 31);
            this.IdType.MaximumSize = new System.Drawing.Size(165, 21);
            this.IdType.MinimumSize = new System.Drawing.Size(165, 21);
            this.IdType.Name = "IdType";
            this.IdType.Size = new System.Drawing.Size(165, 21);
            this.IdType.TabIndex = 14;
            this.toolTipLookupCustomer.SetToolTip(this.IdType, " Please enter either Driver License Number or SSN or passport Or Other Id");
            this.IdType.Leave += new System.EventHandler(this.IdType_Leave);
            // 
            // labelIDType
            // 
            this.labelIDType.Location = new System.Drawing.Point(9, 31);
            this.labelIDType.Name = "labelIDType";
            this.labelIDType.Size = new System.Drawing.Size(31, 13);
            this.labelIDType.TabIndex = 10;
            this.labelIDType.Text = "Type";
            this.toolTipLookupCustomer.SetToolTip(this.labelIDType, "Enter an ID number");
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(3, 22);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(622, 2);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            // 
            // labelIDIssuer
            // 
            this.labelIDIssuer.Location = new System.Drawing.Point(395, 31);
            this.labelIDIssuer.Name = "labelIDIssuer";
            this.labelIDIssuer.Size = new System.Drawing.Size(37, 13);
            this.labelIDIssuer.TabIndex = 5;
            this.labelIDIssuer.Text = "Issuer";
            // 
            // labelIDNumber
            // 
            this.labelIDNumber.Location = new System.Drawing.Point(239, 31);
            this.labelIDNumber.Name = "labelIDNumber";
            this.labelIDNumber.Size = new System.Drawing.Size(44, 13);
            this.labelIDNumber.TabIndex = 3;
            this.labelIDNumber.Text = "Number";
            // 
            // IDSearchRadioBtn
            // 
            this.IDSearchRadioBtn.CausesValidation = false;
            this.IDSearchRadioBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDSearchRadioBtn.Location = new System.Drawing.Point(12, 3);
            this.IDSearchRadioBtn.Name = "IDSearchRadioBtn";
            this.IDSearchRadioBtn.Size = new System.Drawing.Size(80, 17);
            this.IDSearchRadioBtn.TabIndex = 13;
            this.IDSearchRadioBtn.Text = "ID Search";
            this.IDSearchRadioBtn.UseVisualStyleBackColor = true;
            this.IDSearchRadioBtn.CheckedChanged += new System.EventHandler(this.IDSearchRadioBtn_CheckedChanged);
            // 
            // lookupCustomerCustNumberPanel
            // 
            this.lookupCustomerCustNumberPanel.BackColor = System.Drawing.Color.Transparent;
            this.lookupCustomerCustNumberPanel.Controls.Add(this.lookupCustomerCustNumber);
            this.lookupCustomerCustNumberPanel.Controls.Add(this.groupBox5);
            this.lookupCustomerCustNumberPanel.Controls.Add(this.labelCustomerNumber);
            this.lookupCustomerCustNumberPanel.Controls.Add(this.CustNumberRadioBtn);
            this.lookupCustomerCustNumberPanel.Location = new System.Drawing.Point(63, 463);
            this.lookupCustomerCustNumberPanel.Name = "lookupCustomerCustNumberPanel";
            this.lookupCustomerCustNumberPanel.Size = new System.Drawing.Size(622, 71);
            this.lookupCustomerCustNumberPanel.TabIndex = 21;
            // 
            // lookupCustomerCustNumber
            // 
            this.lookupCustomerCustNumber.BackColor = System.Drawing.Color.White;
            this.lookupCustomerCustNumber.CausesValidation = false;
            this.lookupCustomerCustNumber.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.lookupCustomerCustNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupCustomerCustNumber.Location = new System.Drawing.Point(173, 36);
            this.lookupCustomerCustNumber.MaxLength = 20;
            this.lookupCustomerCustNumber.Name = "lookupCustomerCustNumber";
            this.lookupCustomerCustNumber.Size = new System.Drawing.Size(154, 21);
            this.lookupCustomerCustNumber.TabIndex = 18;
            this.lookupCustomerCustNumber.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(0, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(622, 2);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            // 
            // labelCustomerNumber
            // 
            this.labelCustomerNumber.Location = new System.Drawing.Point(16, 36);
            this.labelCustomerNumber.Name = "labelCustomerNumber";
            this.labelCustomerNumber.Size = new System.Drawing.Size(93, 13);
            this.labelCustomerNumber.TabIndex = 1;
            this.labelCustomerNumber.Text = "Customer Number";
            // 
            // CustNumberRadioBtn
            // 
            this.CustNumberRadioBtn.CausesValidation = false;
            this.CustNumberRadioBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustNumberRadioBtn.Location = new System.Drawing.Point(13, 4);
            this.CustNumberRadioBtn.Name = "CustNumberRadioBtn";
            this.CustNumberRadioBtn.Size = new System.Drawing.Size(127, 17);
            this.CustNumberRadioBtn.TabIndex = 17;
            this.CustNumberRadioBtn.Text = "Customer Number";
            this.CustNumberRadioBtn.UseVisualStyleBackColor = true;
            this.CustNumberRadioBtn.CheckedChanged += new System.EventHandler(this.CustNumberRadioBtn_CheckedChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Location = new System.Drawing.Point(15, 625);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(669, 2);
            this.groupBox7.TabIndex = 23;
            this.groupBox7.TabStop = false;
            // 
            // panelSSN
            // 
            this.panelSSN.BackColor = System.Drawing.Color.Transparent;
            this.panelSSN.Controls.Add(this.socialSecurityNumber);
            this.panelSSN.Controls.Add(this.groupBox1);
            this.panelSSN.Controls.Add(this.labelSSN);
            this.panelSSN.Controls.Add(this.radioButtonSSN);
            this.panelSSN.Location = new System.Drawing.Point(63, 126);
            this.panelSSN.Name = "panelSSN";
            this.panelSSN.Size = new System.Drawing.Size(621, 59);
            this.panelSSN.TabIndex = 24;
            // 
            // socialSecurityNumber
            // 
            this.socialSecurityNumber.CausesValidation = false;
            this.socialSecurityNumber.Location = new System.Drawing.Point(173, 32);
            this.socialSecurityNumber.Name = "socialSecurityNumber";
            this.socialSecurityNumber.Size = new System.Drawing.Size(123, 20);
            this.socialSecurityNumber.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(-1, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(622, 2);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // labelSSN
            // 
            this.labelSSN.ForeColor = System.Drawing.Color.Black;
            this.labelSSN.Location = new System.Drawing.Point(18, 32);
            this.labelSSN.Name = "labelSSN";
            this.labelSSN.Size = new System.Drawing.Size(116, 13);
            this.labelSSN.TabIndex = 1;
            this.labelSSN.Text = "Social Security Number";
            // 
            // radioButtonSSN
            // 
            this.radioButtonSSN.CausesValidation = false;
            this.radioButtonSSN.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSSN.Location = new System.Drawing.Point(12, 4);
            this.radioButtonSSN.Name = "radioButtonSSN";
            this.radioButtonSSN.Size = new System.Drawing.Size(155, 17);
            this.radioButtonSSN.TabIndex = 0;
            this.radioButtonSSN.Text = "Social Security Number";
            this.radioButtonSSN.UseVisualStyleBackColor = true;
            this.radioButtonSSN.CheckedChanged += new System.EventHandler(this.radioButtonSSN_CheckedChanged);
            // 
            // toolTipLookupCustomer
            // 
            this.toolTipLookupCustomer.AutomaticDelay = 1;
            this.toolTipLookupCustomer.IsBalloon = true;
            this.toolTipLookupCustomer.ShowAlways = true;
            this.toolTipLookupCustomer.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipLookupCustomer.ToolTipTitle = "Help";
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.BackColor = System.Drawing.Color.Transparent;
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(61, 108);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(31, 13);
            this.errorLabel.TabIndex = 25;
            this.errorLabel.Text = "Error";
            // 
            // customButtonFind
            // 
            this.customButtonFind.BackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.customButtonFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.customButtonFind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonFind.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonFind.FlatAppearance.BorderSize = 0;
            this.customButtonFind.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonFind.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonFind.ForeColor = System.Drawing.Color.White;
            this.customButtonFind.Location = new System.Drawing.Point(580, 654);
            this.customButtonFind.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonFind.MaximumSize = new System.Drawing.Size(90, 40);
            this.customButtonFind.MinimumSize = new System.Drawing.Size(90, 40);
            this.customButtonFind.Name = "customButtonFind";
            this.customButtonFind.Size = new System.Drawing.Size(90, 40);
            this.customButtonFind.TabIndex = 25;
            this.customButtonFind.Text = "&Find";
            this.customButtonFind.UseVisualStyleBackColor = false;
            this.customButtonFind.Click += new System.EventHandler(this.lookupCustomerFindButton_Click);
            // 
            // customButtonClear
            // 
            this.customButtonClear.BackColor = System.Drawing.Color.Transparent;
            this.customButtonClear.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.customButtonClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.customButtonClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonClear.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonClear.FlatAppearance.BorderSize = 0;
            this.customButtonClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonClear.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonClear.ForeColor = System.Drawing.Color.White;
            this.customButtonClear.Location = new System.Drawing.Point(469, 654);
            this.customButtonClear.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonClear.MaximumSize = new System.Drawing.Size(90, 40);
            this.customButtonClear.MinimumSize = new System.Drawing.Size(90, 40);
            this.customButtonClear.Name = "customButtonClear";
            this.customButtonClear.Size = new System.Drawing.Size(90, 40);
            this.customButtonClear.TabIndex = 24;
            this.customButtonClear.Text = "C&lear";
            this.customButtonClear.UseVisualStyleBackColor = false;
            this.customButtonClear.Click += new System.EventHandler(this.lookupCustomerClearButton_Click);
            // 
            // customButtonCancel
            // 
            this.customButtonCancel.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.customButtonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.customButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.customButtonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCancel.FlatAppearance.BorderSize = 0;
            this.customButtonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCancel.ForeColor = System.Drawing.Color.White;
            this.customButtonCancel.Location = new System.Drawing.Point(62, 654);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(90, 40);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(90, 40);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(90, 40);
            this.customButtonCancel.TabIndex = 23;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.lookupCustomerCancelButton_Click);
            // 
            // customButtonAddCustomer
            // 
            this.customButtonAddCustomer.BackColor = System.Drawing.Color.Transparent;
            this.customButtonAddCustomer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonAddCustomer.BackgroundImage")));
            this.customButtonAddCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonAddCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonAddCustomer.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonAddCustomer.FlatAppearance.BorderSize = 0;
            this.customButtonAddCustomer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonAddCustomer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonAddCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonAddCustomer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonAddCustomer.ForeColor = System.Drawing.Color.White;
            this.customButtonAddCustomer.Location = new System.Drawing.Point(58, 646);
            this.customButtonAddCustomer.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonAddCustomer.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonAddCustomer.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonAddCustomer.Name = "customButtonAddCustomer";
            this.customButtonAddCustomer.Size = new System.Drawing.Size(100, 50);
            this.customButtonAddCustomer.TabIndex = 26;
            this.customButtonAddCustomer.Text = "&Add Customer";
            this.customButtonAddCustomer.UseVisualStyleBackColor = false;
            this.customButtonAddCustomer.Visible = false;
            this.customButtonAddCustomer.Click += new System.EventHandler(this.addButton_Click);
            // 
            // LookupCustomerLoanTicketPanel
            // 
            this.LookupCustomerLoanTicketPanel.BackColor = System.Drawing.Color.Transparent;
            this.LookupCustomerLoanTicketPanel.Controls.Add(this.ShopNumber);
            this.LookupCustomerLoanTicketPanel.Controls.Add(this.ShopNumberLabel);
            this.LookupCustomerLoanTicketPanel.Controls.Add(this.ProductType);
            this.LookupCustomerLoanTicketPanel.Controls.Add(this.ProductTypeLabel);
            this.LookupCustomerLoanTicketPanel.Controls.Add(this.LoanNumber);
            this.LookupCustomerLoanTicketPanel.Controls.Add(this.LoanNumberLabel);
            this.LookupCustomerLoanTicketPanel.Controls.Add(this.groupBox6);
            this.LookupCustomerLoanTicketPanel.Controls.Add(this.LoanTicketRadioBtn);
            this.LookupCustomerLoanTicketPanel.Location = new System.Drawing.Point(62, 540);
            this.LookupCustomerLoanTicketPanel.Name = "LookupCustomerLoanTicketPanel";
            this.LookupCustomerLoanTicketPanel.Size = new System.Drawing.Size(622, 71);
            this.LookupCustomerLoanTicketPanel.TabIndex = 30;
            // 
            // ShopNumber
            // 
            this.ShopNumber.CausesValidation = false;
            this.ShopNumber.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.ShopNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShopNumber.Location = new System.Drawing.Point(288, 36);
            this.ShopNumber.Name = "ShopNumber";
            this.ShopNumber.Size = new System.Drawing.Size(100, 21);
            this.ShopNumber.TabIndex = 21;
            this.ShopNumber.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            this.ShopNumber.Leave += new System.EventHandler(this.ShopNumber_Leave);
            // 
            // ShopNumberLabel
            // 
            this.ShopNumberLabel.AutoSize = true;
            this.ShopNumberLabel.Location = new System.Drawing.Point(220, 36);
            this.ShopNumberLabel.Name = "ShopNumberLabel";
            this.ShopNumberLabel.Size = new System.Drawing.Size(61, 13);
            this.ShopNumberLabel.TabIndex = 6;
            this.ShopNumberLabel.Text = "Shop Name";
            // 
            // ProductType
            // 
            this.ProductType.FormattingEnabled = true;
            this.ProductType.Items.AddRange(new object[] {
            "Pawn",
            "PDL/INST",
            "Layaway"});
            this.ProductType.Location = new System.Drawing.Point(92, 36);
            this.ProductType.Name = "ProductType";
            this.ProductType.Size = new System.Drawing.Size(121, 21);
            this.ProductType.TabIndex = 20;
            // 
            // ProductTypeLabel
            // 
            this.ProductTypeLabel.AutoSize = true;
            this.ProductTypeLabel.Location = new System.Drawing.Point(14, 36);
            this.ProductTypeLabel.Name = "ProductTypeLabel";
            this.ProductTypeLabel.Size = new System.Drawing.Size(71, 13);
            this.ProductTypeLabel.TabIndex = 4;
            this.ProductTypeLabel.Text = "Product Type";
            // 
            // LoanNumber
            // 
            this.LoanNumber.CausesValidation = false;
            this.LoanNumber.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.LoanNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoanNumber.Location = new System.Drawing.Point(516, 36);
            this.LoanNumber.MaxLength = 20;
            this.LoanNumber.Name = "LoanNumber";
            this.LoanNumber.Size = new System.Drawing.Size(100, 21);
            this.LoanNumber.TabIndex = 22;
            this.LoanNumber.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // LoanNumberLabel
            // 
            this.LoanNumberLabel.AutoSize = true;
            this.LoanNumberLabel.Location = new System.Drawing.Point(395, 36);
            this.LoanNumberLabel.Name = "LoanNumberLabel";
            this.LoanNumberLabel.Size = new System.Drawing.Size(114, 13);
            this.LoanNumberLabel.TabIndex = 2;
            this.LoanNumberLabel.Text = "Loan or Ticket Number";
            // 
            // groupBox6
            // 
            this.groupBox6.Location = new System.Drawing.Point(0, 26);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(622, 2);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            // 
            // LoanTicketRadioBtn
            // 
            this.LoanTicketRadioBtn.AutoSize = true;
            this.LoanTicketRadioBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoanTicketRadioBtn.Location = new System.Drawing.Point(13, 7);
            this.LoanTicketRadioBtn.Name = "LoanTicketRadioBtn";
            this.LoanTicketRadioBtn.Size = new System.Drawing.Size(105, 17);
            this.LoanTicketRadioBtn.TabIndex = 19;
            this.LoanTicketRadioBtn.TabStop = true;
            this.LoanTicketRadioBtn.Text = "Loan/Ticket #";
            this.LoanTicketRadioBtn.UseVisualStyleBackColor = true;
            this.LoanTicketRadioBtn.CheckedChanged += new System.EventHandler(this.LoanTicketRadioBtn_CheckedChanged);
            // 
            // LookupCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Red;
            this.BackgroundImage = global::Support.Properties.Resources.form_800_700;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.customButtonCancel;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(708, 714);
            this.ControlBox = false;
            this.Controls.Add(this.LookupCustomerLoanTicketPanel);
            this.Controls.Add(this.customButtonFind);
            this.Controls.Add(this.customButtonClear);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.panelSSN);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.lookupCustomerInfoLabel);
            this.Controls.Add(this.lookupCustomerCustNumberPanel);
            this.Controls.Add(this.lookupCustomerIDSearchPanel);
            this.Controls.Add(this.lookupCustomerCustInfoPanel);
            this.Controls.Add(this.customerResultsPanel);
            this.Controls.Add(this.lookupCustomerDialogLabel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(50, 13);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LookupCustomer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lookup Customer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LookupCustomer_FormClosing);
            this.Load += new System.EventHandler(this.LookupCustomer_Load);
            this.lookupCustomerCustInfoPanel.ResumeLayout(false);
            this.lookupCustomerCustInfoPanel.PerformLayout();
            this.lookupCustomerIDSearchPanel.ResumeLayout(false);
            this.lookupCustomerIDSearchPanel.PerformLayout();
            this.lookupCustomerCustNumberPanel.ResumeLayout(false);
            this.lookupCustomerCustNumberPanel.PerformLayout();
            this.panelSSN.ResumeLayout(false);
            this.LookupCustomerLoanTicketPanel.ResumeLayout(false);
            this.LookupCustomerLoanTicketPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lookupCustomerDialogLabel;
        private System.Windows.Forms.Panel customerResultsPanel;
        private System.Windows.Forms.Label lookupCustomerInfoLabel;
        private System.Windows.Forms.Label labelCloseBracket;
        private System.Windows.Forms.Label label1OpenBracket;
        private System.Windows.Forms.Label labelCustomerPhoneNumber;
        private System.Windows.Forms.Panel lookupCustomerCustInfoPanel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelCustDateOfBirth;
        private System.Windows.Forms.Label labelCustFirstName;
        private System.Windows.Forms.Label labelCustLastName;
        private System.Windows.Forms.RadioButton CustomerInfoRadioBtn;
        private System.Windows.Forms.Panel lookupCustomerIDSearchPanel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label labelIDIssuer;
        private System.Windows.Forms.Label labelIDNumber;
        private System.Windows.Forms.RadioButton IDSearchRadioBtn;
        private System.Windows.Forms.Panel lookupCustomerCustNumberPanel;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label labelCustomerNumber;
        private System.Windows.Forms.RadioButton CustNumberRadioBtn;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label labelIDType;

        private System.Windows.Forms.Panel panelSSN;
        private System.Windows.Forms.RadioButton radioButtonSSN;
        private System.Windows.Forms.Label labelSSN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolTip toolTipLookupCustomer;
        private System.Windows.Forms.Label labelHyphen;
        private CustomTextBox phoneNum1TextBox;
        private CustomTextBox phoneAreaCodeTextBox;
        private CustomTextBox phoneNum2TextBox;
        private CustomTextBox lookupCustomerLastName;

        private CustomTextBox lookupCustomerFirstName;
        private CustomTextBox lookupCustomerIDNumber;
        private CustomTextBox lookupCustomerCustNumber;
        private System.Windows.Forms.Label errorLabel;
        private SocialSecurityNumber socialSecurityNumber;
        private CustomButton customButtonAddCustomer;
        private SupportButton customButtonCancel;
        private SupportButton customButtonClear;
        private SupportButton customButtonFind;
        private System.Windows.Forms.Panel LookupCustomerLoanTicketPanel;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton LoanTicketRadioBtn;
        private System.Windows.Forms.Label LoanNumberLabel;
        private CustomTextBox LoanNumber;
        private CustomTextBox SSNShort;
        private System.Windows.Forms.Label labelSSNShort;
        private CustomTextBox ShopNumber;
        private CustomLabel ShopNumberLabel;
        private System.Windows.Forms.ComboBox ProductType;
        private CustomLabel ProductTypeLabel;
        private CustomTextBox BnkAcctNum;
        private CustomLabel BnkAcctNumLabel;
        private State CusInfoState;
        private CustomLabel StateLabel;
        private CustomTextBox City;
        private CustomLabel CityLabel;
        //Madhu Feb 17th
        //private VendorSearch vendorSearchUC;
        //private IDTypeDataset iDTypeDataset;
        //private CashlinxDesktop.IDTypeDatasetTableAdapters.DATEDIDENTTYPETableAdapter dATEDIDENTTYPETableAdapter;
    }
}
