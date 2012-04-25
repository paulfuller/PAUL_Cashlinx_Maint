using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.Customer
{
    partial class LookupVendor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LookupVendor));
            this.lookupCustomerDialogLabel = new System.Windows.Forms.Label();
            this.lookupCustomerInfoLabel = new System.Windows.Forms.Label();
            this.toolTipLookupCustomer = new System.Windows.Forms.ToolTip(this.components);
            this.labelIDType = new System.Windows.Forms.Label();
            this.IdType = new IDType();
            this.errorLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.vendorSearchGroup = new System.Windows.Forms.Panel();
            this.lookupVendorTaxID = new CustomTextBox();
            this.VendorNameRadiobtn = new System.Windows.Forms.RadioButton();
            this.TaxIDRadiobtn = new System.Windows.Forms.RadioButton();
            this.lookupVendorName = new CustomTextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.VendorRadiobtn = new System.Windows.Forms.RadioButton();
            this.customerResultsPanel = new System.Windows.Forms.Panel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.CustNumberRadioBtn = new System.Windows.Forms.RadioButton();
            this.labelCustomerNumber = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lookupCustomerCustNumberPanel = new System.Windows.Forms.Panel();
            this.lookupCustomerCustNumber = new CustomTextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.LoanNumberRadiobtn = new System.Windows.Forms.RadioButton();
            this.labelLoanNumber = new System.Windows.Forms.Label();
            this.lookupCustomerLoanNumberPanel = new System.Windows.Forms.Panel();
            this.lookupCustomerLoanNumber = new CustomTextBox();
            this.labelCustomerPhoneNumber = new System.Windows.Forms.Label();
            this.CustomerPhoneNumRadioBtn = new System.Windows.Forms.RadioButton();
            this.label1OpenBracket = new System.Windows.Forms.Label();
            this.labelCloseBracket = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelHyphen = new System.Windows.Forms.Label();
            this.lookupCustomerPhonePanel = new System.Windows.Forms.Panel();
            this.phoneNum2TextBox = new CustomTextBox();
            this.phoneNum1TextBox = new CustomTextBox();
            this.phoneAreaCodeTextBox = new CustomTextBox();
            this.CustomerInfoRadioBtn = new System.Windows.Forms.RadioButton();
            this.labelCustLastName = new System.Windows.Forms.Label();
            this.labelCustFirstName = new System.Windows.Forms.Label();
            this.labelCustDateOfBirth = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lookupCustomerCustInfoPanel = new System.Windows.Forms.Panel();
            this.dateOfBirth = new Date();
            this.lookupCustomerFirstName = new CustomTextBox();
            this.lookupCustomerLastName = new CustomTextBox();
            this.IDSearchRadioBtn = new System.Windows.Forms.RadioButton();
            this.labelIDNumber = new System.Windows.Forms.Label();
            this.labelIDIssuer = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lookupCustomerIDSearchPanel = new System.Windows.Forms.Panel();
            this.lookupCustomerIDNumber = new CustomTextBox();
            this.idIssuer1 = new State();
            this.radioButtonSSN = new System.Windows.Forms.RadioButton();
            this.labelSSN = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelSSN = new System.Windows.Forms.Panel();
            this.socialSecurityNumber = new SocialSecurityNumber();
            this.customButtonAddVendor = new CustomButton();
            this.customButtonFind = new CustomButton();
            this.customButtonClear = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.customButtonAddCustomer = new CustomButton();
            this.panel1.SuspendLayout();
            this.vendorSearchGroup.SuspendLayout();
            this.lookupCustomerCustNumberPanel.SuspendLayout();
            this.lookupCustomerLoanNumberPanel.SuspendLayout();
            this.lookupCustomerPhonePanel.SuspendLayout();
            this.lookupCustomerCustInfoPanel.SuspendLayout();
            this.lookupCustomerIDSearchPanel.SuspendLayout();
            this.panelSSN.SuspendLayout();
            this.SuspendLayout();
            // 
            // lookupCustomerDialogLabel
            // 
            this.lookupCustomerDialogLabel.BackColor = System.Drawing.Color.Transparent;
            this.lookupCustomerDialogLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupCustomerDialogLabel.ForeColor = System.Drawing.Color.White;
            this.lookupCustomerDialogLabel.Location = new System.Drawing.Point(342, 18);
            this.lookupCustomerDialogLabel.Name = "lookupCustomerDialogLabel";
            this.lookupCustomerDialogLabel.Size = new System.Drawing.Size(75, 19);
            this.lookupCustomerDialogLabel.TabIndex = 2;
            this.lookupCustomerDialogLabel.Text = "Lookup";
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
            // toolTipLookupCustomer
            // 
            this.toolTipLookupCustomer.AutomaticDelay = 1;
            this.toolTipLookupCustomer.IsBalloon = true;
            this.toolTipLookupCustomer.ShowAlways = true;
            this.toolTipLookupCustomer.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipLookupCustomer.ToolTipTitle = "Help";
            // 
            // labelIDType
            // 
            this.labelIDType.Location = new System.Drawing.Point(9, 37);
            this.labelIDType.Name = "labelIDType";
            this.labelIDType.Size = new System.Drawing.Size(31, 13);
            this.labelIDType.TabIndex = 10;
            this.labelIDType.Text = "Type";
            this.toolTipLookupCustomer.SetToolTip(this.labelIDType, "Enter an ID number");
            // 
            // IdType
            // 
            this.IdType.CausesValidation = false;
            this.IdType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.IdType.Location = new System.Drawing.Point(46, 31);
            this.IdType.MaximumSize = new System.Drawing.Size(165, 21);
            this.IdType.MinimumSize = new System.Drawing.Size(165, 21);
            this.IdType.Name = "IdType";
            this.IdType.Size = new System.Drawing.Size(165, 21);
            this.IdType.TabIndex = 8;
            this.toolTipLookupCustomer.SetToolTip(this.IdType, " Please enter either Driver License Number or SSN or passport Or Other Id");
            this.IdType.Leave += new System.EventHandler(this.IdType_Leave);
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
            this.errorLabel.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.vendorSearchGroup);
            this.panel1.Controls.Add(this.groupBox8);
            this.panel1.Controls.Add(this.VendorRadiobtn);
            this.panel1.Location = new System.Drawing.Point(60, 137);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(622, 100);
            this.panel1.TabIndex = 30;
            // 
            // vendorSearchGroup
            // 
            this.vendorSearchGroup.Controls.Add(this.lookupVendorTaxID);
            this.vendorSearchGroup.Controls.Add(this.VendorNameRadiobtn);
            this.vendorSearchGroup.Controls.Add(this.TaxIDRadiobtn);
            this.vendorSearchGroup.Controls.Add(this.lookupVendorName);
            this.vendorSearchGroup.Location = new System.Drawing.Point(3, 28);
            this.vendorSearchGroup.Name = "vendorSearchGroup";
            this.vendorSearchGroup.Size = new System.Drawing.Size(616, 62);
            this.vendorSearchGroup.TabIndex = 10;
            // 
            // lookupVendorTaxID
            // 
            this.lookupVendorTaxID.BackColor = System.Drawing.Color.White;
            this.lookupVendorTaxID.CausesValidation = false;
            this.lookupVendorTaxID.Enabled = false;
            this.lookupVendorTaxID.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.lookupVendorTaxID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupVendorTaxID.Location = new System.Drawing.Point(164, 34);
            this.lookupVendorTaxID.MaxLength = 20;
            this.lookupVendorTaxID.Name = "lookupVendorTaxID";
            this.lookupVendorTaxID.Size = new System.Drawing.Size(154, 21);
            this.lookupVendorTaxID.TabIndex = 13;
            this.lookupVendorTaxID.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // VendorNameRadiobtn
            // 
            this.VendorNameRadiobtn.AutoSize = true;
            this.VendorNameRadiobtn.Checked = true;
            this.VendorNameRadiobtn.Location = new System.Drawing.Point(31, 12);
            this.VendorNameRadiobtn.Name = "VendorNameRadiobtn";
            this.VendorNameRadiobtn.Size = new System.Drawing.Size(89, 17);
            this.VendorNameRadiobtn.TabIndex = 14;
            this.VendorNameRadiobtn.TabStop = true;
            this.VendorNameRadiobtn.Text = "Vendor Name";
            this.VendorNameRadiobtn.UseVisualStyleBackColor = true;
            this.VendorNameRadiobtn.CheckedChanged += new System.EventHandler(this.VendorNameRadiobtn_CheckedChanged);
            // 
            // TaxIDRadiobtn
            // 
            this.TaxIDRadiobtn.AutoSize = true;
            this.TaxIDRadiobtn.Location = new System.Drawing.Point(31, 35);
            this.TaxIDRadiobtn.Name = "TaxIDRadiobtn";
            this.TaxIDRadiobtn.Size = new System.Drawing.Size(97, 17);
            this.TaxIDRadiobtn.TabIndex = 15;
            this.TaxIDRadiobtn.Text = "Tax ID Number";
            this.TaxIDRadiobtn.UseVisualStyleBackColor = true;
            this.TaxIDRadiobtn.CheckedChanged += new System.EventHandler(this.TaxIDRadiobtn_CheckedChanged);
            // 
            // lookupVendorName
            // 
            this.lookupVendorName.BackColor = System.Drawing.Color.White;
            this.lookupVendorName.CausesValidation = false;
            this.lookupVendorName.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.lookupVendorName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupVendorName.Location = new System.Drawing.Point(164, 8);
            this.lookupVendorName.MaxLength = 20;
            this.lookupVendorName.Name = "lookupVendorName";
            this.lookupVendorName.Size = new System.Drawing.Size(154, 21);
            this.lookupVendorName.TabIndex = 11;
            this.lookupVendorName.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.lookupVendorName.TextChanged += new System.EventHandler(this.searchCriteria_lengthCheck);
            // 
            // groupBox8
            // 
            this.groupBox8.Location = new System.Drawing.Point(0, 24);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(622, 2);
            this.groupBox8.TabIndex = 9;
            this.groupBox8.TabStop = false;
            // 
            // VendorRadiobtn
            // 
            this.VendorRadiobtn.CausesValidation = false;
            this.VendorRadiobtn.Checked = true;
            this.VendorRadiobtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VendorRadiobtn.Location = new System.Drawing.Point(3, 4);
            this.VendorRadiobtn.Name = "VendorRadiobtn";
            this.VendorRadiobtn.Size = new System.Drawing.Size(164, 18);
            this.VendorRadiobtn.TabIndex = 0;
            this.VendorRadiobtn.TabStop = true;
            this.VendorRadiobtn.Text = "Vendor Information";
            this.VendorRadiobtn.UseVisualStyleBackColor = true;
            // 
            // customerResultsPanel
            // 
            this.customerResultsPanel.AutoScroll = true;
            this.customerResultsPanel.AutoScrollMinSize = new System.Drawing.Size(448, 161);
            this.customerResultsPanel.BackgroundImage = global::Pawn.Properties.Resources.dark_dialog_frame;
            this.customerResultsPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.customerResultsPanel.Location = new System.Drawing.Point(144, 250);
            this.customerResultsPanel.MaximumSize = new System.Drawing.Size(448, 161);
            this.customerResultsPanel.MinimumSize = new System.Drawing.Size(448, 0);
            this.customerResultsPanel.Name = "customerResultsPanel";
            this.customerResultsPanel.Size = new System.Drawing.Size(448, 0);
            this.customerResultsPanel.TabIndex = 5;
            // 
            // groupBox7
            // 
            this.groupBox7.Location = new System.Drawing.Point(25, 305);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(669, 2);
            this.groupBox7.TabIndex = 23;
            this.groupBox7.TabStop = false;
            // 
            // CustNumberRadioBtn
            // 
            this.CustNumberRadioBtn.CausesValidation = false;
            this.CustNumberRadioBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustNumberRadioBtn.Location = new System.Drawing.Point(13, 4);
            this.CustNumberRadioBtn.Name = "CustNumberRadioBtn";
            this.CustNumberRadioBtn.Size = new System.Drawing.Size(127, 17);
            this.CustNumberRadioBtn.TabIndex = 0;
            this.CustNumberRadioBtn.Text = "Customer Number";
            this.CustNumberRadioBtn.UseVisualStyleBackColor = true;
            this.CustNumberRadioBtn.CheckedChanged += new System.EventHandler(this.CustNumberRadioBtn_CheckedChanged);
            // 
            // labelCustomerNumber
            // 
            this.labelCustomerNumber.Location = new System.Drawing.Point(16, 36);
            this.labelCustomerNumber.Name = "labelCustomerNumber";
            this.labelCustomerNumber.Size = new System.Drawing.Size(93, 13);
            this.labelCustomerNumber.TabIndex = 1;
            this.labelCustomerNumber.Text = "Customer Number";
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(0, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(622, 2);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            // 
            // lookupCustomerCustNumberPanel
            // 
            this.lookupCustomerCustNumberPanel.BackColor = System.Drawing.Color.Transparent;
            this.lookupCustomerCustNumberPanel.Controls.Add(this.lookupCustomerCustNumber);
            this.lookupCustomerCustNumberPanel.Controls.Add(this.groupBox5);
            this.lookupCustomerCustNumberPanel.Controls.Add(this.labelCustomerNumber);
            this.lookupCustomerCustNumberPanel.Controls.Add(this.CustNumberRadioBtn);
            this.lookupCustomerCustNumberPanel.Enabled = false;
            this.lookupCustomerCustNumberPanel.Location = new System.Drawing.Point(63, 270);
            this.lookupCustomerCustNumberPanel.Name = "lookupCustomerCustNumberPanel";
            this.lookupCustomerCustNumberPanel.Size = new System.Drawing.Size(623, 2);
            this.lookupCustomerCustNumberPanel.TabIndex = 21;
            this.lookupCustomerCustNumberPanel.Visible = false;
            // 
            // lookupCustomerCustNumber
            // 
            this.lookupCustomerCustNumber.BackColor = System.Drawing.Color.White;
            this.lookupCustomerCustNumber.CausesValidation = false;
            this.lookupCustomerCustNumber.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.lookupCustomerCustNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupCustomerCustNumber.Location = new System.Drawing.Point(170, 28);
            this.lookupCustomerCustNumber.MaxLength = 20;
            this.lookupCustomerCustNumber.Name = "lookupCustomerCustNumber";
            this.lookupCustomerCustNumber.Size = new System.Drawing.Size(154, 21);
            this.lookupCustomerCustNumber.TabIndex = 11;
            this.lookupCustomerCustNumber.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // groupBox6
            // 
            this.groupBox6.Location = new System.Drawing.Point(3, 20);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(617, 2);
            this.groupBox6.TabIndex = 17;
            this.groupBox6.TabStop = false;
            // 
            // LoanNumberRadiobtn
            // 
            this.LoanNumberRadiobtn.CausesValidation = false;
            this.LoanNumberRadiobtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoanNumberRadiobtn.Location = new System.Drawing.Point(14, 3);
            this.LoanNumberRadiobtn.Name = "LoanNumberRadiobtn";
            this.LoanNumberRadiobtn.Size = new System.Drawing.Size(99, 17);
            this.LoanNumberRadiobtn.TabIndex = 0;
            this.LoanNumberRadiobtn.Text = "Loan Number";
            this.LoanNumberRadiobtn.UseVisualStyleBackColor = true;
            this.LoanNumberRadiobtn.CheckedChanged += new System.EventHandler(this.LoanNumberRadiobtn_CheckedChanged);
            // 
            // labelLoanNumber
            // 
            this.labelLoanNumber.Location = new System.Drawing.Point(21, 31);
            this.labelLoanNumber.Name = "labelLoanNumber";
            this.labelLoanNumber.Size = new System.Drawing.Size(70, 13);
            this.labelLoanNumber.TabIndex = 1;
            this.labelLoanNumber.Text = "Loan Number";
            // 
            // lookupCustomerLoanNumberPanel
            // 
            this.lookupCustomerLoanNumberPanel.BackColor = System.Drawing.Color.Transparent;
            this.lookupCustomerLoanNumberPanel.Controls.Add(this.lookupCustomerLoanNumber);
            this.lookupCustomerLoanNumberPanel.Controls.Add(this.labelLoanNumber);
            this.lookupCustomerLoanNumberPanel.Controls.Add(this.LoanNumberRadiobtn);
            this.lookupCustomerLoanNumberPanel.Controls.Add(this.groupBox6);
            this.lookupCustomerLoanNumberPanel.Enabled = false;
            this.lookupCustomerLoanNumberPanel.Location = new System.Drawing.Point(62, 280);
            this.lookupCustomerLoanNumberPanel.Name = "lookupCustomerLoanNumberPanel";
            this.lookupCustomerLoanNumberPanel.Size = new System.Drawing.Size(623, 2);
            this.lookupCustomerLoanNumberPanel.TabIndex = 22;
            this.lookupCustomerLoanNumberPanel.Visible = false;
            // 
            // lookupCustomerLoanNumber
            // 
            this.lookupCustomerLoanNumber.AllowOnlyNumbers = true;
            this.lookupCustomerLoanNumber.BackColor = System.Drawing.Color.White;
            this.lookupCustomerLoanNumber.CausesValidation = false;
            this.lookupCustomerLoanNumber.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.lookupCustomerLoanNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupCustomerLoanNumber.Location = new System.Drawing.Point(170, 28);
            this.lookupCustomerLoanNumber.MaxLength = 20;
            this.lookupCustomerLoanNumber.Name = "lookupCustomerLoanNumber";
            this.lookupCustomerLoanNumber.Size = new System.Drawing.Size(154, 21);
            this.lookupCustomerLoanNumber.TabIndex = 12;
            this.lookupCustomerLoanNumber.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // labelCustomerPhoneNumber
            // 
            this.labelCustomerPhoneNumber.Location = new System.Drawing.Point(16, 34);
            this.labelCustomerPhoneNumber.Name = "labelCustomerPhoneNumber";
            this.labelCustomerPhoneNumber.Size = new System.Drawing.Size(126, 13);
            this.labelCustomerPhoneNumber.TabIndex = 0;
            this.labelCustomerPhoneNumber.Text = "Customer Phone Number";
            // 
            // CustomerPhoneNumRadioBtn
            // 
            this.CustomerPhoneNumRadioBtn.CausesValidation = false;
            this.CustomerPhoneNumRadioBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomerPhoneNumRadioBtn.Location = new System.Drawing.Point(13, 4);
            this.CustomerPhoneNumRadioBtn.Name = "CustomerPhoneNumRadioBtn";
            this.CustomerPhoneNumRadioBtn.Size = new System.Drawing.Size(165, 17);
            this.CustomerPhoneNumRadioBtn.TabIndex = 0;
            this.CustomerPhoneNumRadioBtn.Text = "Customer Phone Number";
            this.CustomerPhoneNumRadioBtn.UseVisualStyleBackColor = true;
            this.CustomerPhoneNumRadioBtn.CheckedChanged += new System.EventHandler(this.CustomerPhoneNumRadioBtn_CheckedChanged);
            // 
            // label1OpenBracket
            // 
            this.label1OpenBracket.Location = new System.Drawing.Point(155, 32);
            this.label1OpenBracket.Name = "label1OpenBracket";
            this.label1OpenBracket.Size = new System.Drawing.Size(11, 13);
            this.label1OpenBracket.TabIndex = 0;
            this.label1OpenBracket.Text = "(";
            // 
            // labelCloseBracket
            // 
            this.labelCloseBracket.Location = new System.Drawing.Point(207, 32);
            this.labelCloseBracket.Name = "labelCloseBracket";
            this.labelCloseBracket.Size = new System.Drawing.Size(11, 13);
            this.labelCloseBracket.TabIndex = 6;
            this.labelCloseBracket.Text = ")";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(3, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(622, 2);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // labelHyphen
            // 
            this.labelHyphen.Location = new System.Drawing.Point(273, 34);
            this.labelHyphen.Name = "labelHyphen";
            this.labelHyphen.Size = new System.Drawing.Size(11, 13);
            this.labelHyphen.TabIndex = 12;
            this.labelHyphen.Text = "-";
            // 
            // lookupCustomerPhonePanel
            // 
            this.lookupCustomerPhonePanel.BackColor = System.Drawing.Color.Transparent;
            this.lookupCustomerPhonePanel.Controls.Add(this.phoneNum2TextBox);
            this.lookupCustomerPhonePanel.Controls.Add(this.phoneNum1TextBox);
            this.lookupCustomerPhonePanel.Controls.Add(this.phoneAreaCodeTextBox);
            this.lookupCustomerPhonePanel.Controls.Add(this.labelHyphen);
            this.lookupCustomerPhonePanel.Controls.Add(this.groupBox2);
            this.lookupCustomerPhonePanel.Controls.Add(this.labelCloseBracket);
            this.lookupCustomerPhonePanel.Controls.Add(this.label1OpenBracket);
            this.lookupCustomerPhonePanel.Controls.Add(this.CustomerPhoneNumRadioBtn);
            this.lookupCustomerPhonePanel.Controls.Add(this.labelCustomerPhoneNumber);
            this.lookupCustomerPhonePanel.Enabled = false;
            this.lookupCustomerPhonePanel.Location = new System.Drawing.Point(109, 117);
            this.lookupCustomerPhonePanel.Name = "lookupCustomerPhonePanel";
            this.lookupCustomerPhonePanel.Size = new System.Drawing.Size(550, 3);
            this.lookupCustomerPhonePanel.TabIndex = 18;
            this.lookupCustomerPhonePanel.Visible = false;
            // 
            // phoneNum2TextBox
            // 
            this.phoneNum2TextBox.AllowOnlyNumbers = true;
            this.phoneNum2TextBox.BackColor = System.Drawing.Color.White;
            this.phoneNum2TextBox.CausesValidation = false;
            this.phoneNum2TextBox.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.phoneNum2TextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phoneNum2TextBox.Location = new System.Drawing.Point(289, 30);
            this.phoneNum2TextBox.MaxLength = 4;
            this.phoneNum2TextBox.Name = "phoneNum2TextBox";
            this.phoneNum2TextBox.RegularExpression = true;
            this.phoneNum2TextBox.Size = new System.Drawing.Size(53, 21);
            this.phoneNum2TextBox.TabIndex = 7;
            this.phoneNum2TextBox.ValidationExpression = "^\\d{4}$";
            // 
            // phoneNum1TextBox
            // 
            this.phoneNum1TextBox.AllowOnlyNumbers = true;
            this.phoneNum1TextBox.BackColor = System.Drawing.Color.White;
            this.phoneNum1TextBox.CausesValidation = false;
            this.phoneNum1TextBox.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.phoneNum1TextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phoneNum1TextBox.Location = new System.Drawing.Point(224, 30);
            this.phoneNum1TextBox.MaxLength = 3;
            this.phoneNum1TextBox.Name = "phoneNum1TextBox";
            this.phoneNum1TextBox.RegularExpression = true;
            this.phoneNum1TextBox.Size = new System.Drawing.Size(44, 21);
            this.phoneNum1TextBox.TabIndex = 6;
            this.phoneNum1TextBox.ValidationExpression = "^\\d{3}$";
            this.phoneNum1TextBox.TextChanged += new System.EventHandler(this.phoneNum1TextBox_TextChanged);
            // 
            // phoneAreaCodeTextBox
            // 
            this.phoneAreaCodeTextBox.AllowOnlyNumbers = true;
            this.phoneAreaCodeTextBox.BackColor = System.Drawing.Color.White;
            this.phoneAreaCodeTextBox.CausesValidation = false;
            this.phoneAreaCodeTextBox.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.phoneAreaCodeTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phoneAreaCodeTextBox.Location = new System.Drawing.Point(166, 30);
            this.phoneAreaCodeTextBox.MaxLength = 3;
            this.phoneAreaCodeTextBox.Name = "phoneAreaCodeTextBox";
            this.phoneAreaCodeTextBox.RegularExpression = true;
            this.phoneAreaCodeTextBox.Size = new System.Drawing.Size(41, 21);
            this.phoneAreaCodeTextBox.TabIndex = 5;
            this.phoneAreaCodeTextBox.ValidationExpression = "^\\d{3}$";
            this.phoneAreaCodeTextBox.TextChanged += new System.EventHandler(this.phoneAreaCodeTextBox_TextChanged);
            // 
            // CustomerInfoRadioBtn
            // 
            this.CustomerInfoRadioBtn.CausesValidation = false;
            this.CustomerInfoRadioBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomerInfoRadioBtn.Location = new System.Drawing.Point(13, 4);
            this.CustomerInfoRadioBtn.Name = "CustomerInfoRadioBtn";
            this.CustomerInfoRadioBtn.Size = new System.Drawing.Size(151, 17);
            this.CustomerInfoRadioBtn.TabIndex = 0;
            this.CustomerInfoRadioBtn.Text = "Customer Information";
            this.CustomerInfoRadioBtn.UseVisualStyleBackColor = true;
            this.CustomerInfoRadioBtn.CheckedChanged += new System.EventHandler(this.CustomerInfoRadioBtn_CheckedChanged);
            // 
            // labelCustLastName
            // 
            this.labelCustLastName.Location = new System.Drawing.Point(15, 41);
            this.labelCustLastName.Name = "labelCustLastName";
            this.labelCustLastName.Size = new System.Drawing.Size(57, 13);
            this.labelCustLastName.TabIndex = 1;
            this.labelCustLastName.Text = "Last Name";
            // 
            // labelCustFirstName
            // 
            this.labelCustFirstName.Location = new System.Drawing.Point(15, 63);
            this.labelCustFirstName.Name = "labelCustFirstName";
            this.labelCustFirstName.Size = new System.Drawing.Size(58, 13);
            this.labelCustFirstName.TabIndex = 2;
            this.labelCustFirstName.Text = "First Name";
            // 
            // labelCustDateOfBirth
            // 
            this.labelCustDateOfBirth.Location = new System.Drawing.Point(16, 87);
            this.labelCustDateOfBirth.Name = "labelCustDateOfBirth";
            this.labelCustDateOfBirth.Size = new System.Drawing.Size(68, 13);
            this.labelCustDateOfBirth.TabIndex = 3;
            this.labelCustDateOfBirth.Text = "Date of Birth";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(3, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(622, 2);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // lookupCustomerCustInfoPanel
            // 
            this.lookupCustomerCustInfoPanel.BackColor = System.Drawing.Color.Transparent;
            this.lookupCustomerCustInfoPanel.Controls.Add(this.dateOfBirth);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.lookupCustomerFirstName);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.lookupCustomerLastName);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.groupBox3);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.labelCustDateOfBirth);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.labelCustFirstName);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.labelCustLastName);
            this.lookupCustomerCustInfoPanel.Controls.Add(this.CustomerInfoRadioBtn);
            this.lookupCustomerCustInfoPanel.Enabled = false;
            this.lookupCustomerCustInfoPanel.Location = new System.Drawing.Point(109, 128);
            this.lookupCustomerCustInfoPanel.Name = "lookupCustomerCustInfoPanel";
            this.lookupCustomerCustInfoPanel.Size = new System.Drawing.Size(550, 3);
            this.lookupCustomerCustInfoPanel.TabIndex = 19;
            this.lookupCustomerCustInfoPanel.Visible = false;
            // 
            // dateOfBirth
            // 
            this.dateOfBirth.CausesValidation = false;
            this.dateOfBirth.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.dateOfBirth.Location = new System.Drawing.Point(168, 91);
            this.dateOfBirth.Name = "dateOfBirth";
            this.dateOfBirth.Size = new System.Drawing.Size(100, 20);
            this.dateOfBirth.TabIndex = 3;
            this.dateOfBirth.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.dateOfBirth.ValidationExpression = "^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.](19|20)\\d\\d$";
            this.dateOfBirth.Leave += new System.EventHandler(this.dateOfBirth_Leave);
            // 
            // lookupCustomerFirstName
            // 
            this.lookupCustomerFirstName.BackColor = System.Drawing.Color.White;
            this.lookupCustomerFirstName.CausesValidation = false;
            this.lookupCustomerFirstName.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.lookupCustomerFirstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupCustomerFirstName.Location = new System.Drawing.Point(168, 64);
            this.lookupCustomerFirstName.MaxLength = 40;
            this.lookupCustomerFirstName.Name = "lookupCustomerFirstName";
            this.lookupCustomerFirstName.Size = new System.Drawing.Size(220, 21);
            this.lookupCustomerFirstName.TabIndex = 2;
            this.lookupCustomerFirstName.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // lookupCustomerLastName
            // 
            this.lookupCustomerLastName.BackColor = System.Drawing.Color.White;
            this.lookupCustomerLastName.CausesValidation = false;
            this.lookupCustomerLastName.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.lookupCustomerLastName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupCustomerLastName.Location = new System.Drawing.Point(168, 34);
            this.lookupCustomerLastName.MaxLength = 40;
            this.lookupCustomerLastName.Name = "lookupCustomerLastName";
            this.lookupCustomerLastName.Size = new System.Drawing.Size(220, 21);
            this.lookupCustomerLastName.TabIndex = 1;
            this.lookupCustomerLastName.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // IDSearchRadioBtn
            // 
            this.IDSearchRadioBtn.CausesValidation = false;
            this.IDSearchRadioBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDSearchRadioBtn.Location = new System.Drawing.Point(12, 3);
            this.IDSearchRadioBtn.Name = "IDSearchRadioBtn";
            this.IDSearchRadioBtn.Size = new System.Drawing.Size(80, 17);
            this.IDSearchRadioBtn.TabIndex = 0;
            this.IDSearchRadioBtn.Text = "ID Search";
            this.IDSearchRadioBtn.UseVisualStyleBackColor = true;
            this.IDSearchRadioBtn.CheckedChanged += new System.EventHandler(this.IDSearchRadioBtn_CheckedChanged);
            // 
            // labelIDNumber
            // 
            this.labelIDNumber.Location = new System.Drawing.Point(239, 39);
            this.labelIDNumber.Name = "labelIDNumber";
            this.labelIDNumber.Size = new System.Drawing.Size(44, 13);
            this.labelIDNumber.TabIndex = 3;
            this.labelIDNumber.Text = "Number";
            // 
            // labelIDIssuer
            // 
            this.labelIDIssuer.Location = new System.Drawing.Point(395, 38);
            this.labelIDIssuer.Name = "labelIDIssuer";
            this.labelIDIssuer.Size = new System.Drawing.Size(37, 13);
            this.labelIDIssuer.TabIndex = 5;
            this.labelIDIssuer.Text = "Issuer";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(3, 22);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(622, 2);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
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
            this.lookupCustomerIDSearchPanel.Enabled = false;
            this.lookupCustomerIDSearchPanel.Location = new System.Drawing.Point(64, 259);
            this.lookupCustomerIDSearchPanel.Name = "lookupCustomerIDSearchPanel";
            this.lookupCustomerIDSearchPanel.Size = new System.Drawing.Size(623, 3);
            this.lookupCustomerIDSearchPanel.TabIndex = 20;
            this.lookupCustomerIDSearchPanel.Visible = false;
            // 
            // lookupCustomerIDNumber
            // 
            this.lookupCustomerIDNumber.BackColor = System.Drawing.Color.White;
            this.lookupCustomerIDNumber.CausesValidation = false;
            this.lookupCustomerIDNumber.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.lookupCustomerIDNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupCustomerIDNumber.Location = new System.Drawing.Point(288, 31);
            this.lookupCustomerIDNumber.Name = "lookupCustomerIDNumber";
            this.lookupCustomerIDNumber.Size = new System.Drawing.Size(100, 21);
            this.lookupCustomerIDNumber.TabIndex = 9;
            this.lookupCustomerIDNumber.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // idIssuer1
            // 
            this.idIssuer1.BackColor = System.Drawing.Color.Transparent;
            this.idIssuer1.CausesValidation = false;
            this.idIssuer1.DisplayCode = true;
            this.idIssuer1.ForeColor = System.Drawing.Color.Black;
            this.idIssuer1.Location = new System.Drawing.Point(438, 31);
            this.idIssuer1.Name = "idIssuer1";
            this.idIssuer1.selectedValue = global::Pawn.Properties.Resources.OverrideMachineName;
            this.idIssuer1.Size = new System.Drawing.Size(47, 21);
            this.idIssuer1.TabIndex = 10;
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
            // labelSSN
            // 
            this.labelSSN.ForeColor = System.Drawing.Color.Black;
            this.labelSSN.Location = new System.Drawing.Point(18, 32);
            this.labelSSN.Name = "labelSSN";
            this.labelSSN.Size = new System.Drawing.Size(116, 13);
            this.labelSSN.TabIndex = 1;
            this.labelSSN.Text = "Social Security Number";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(-1, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(622, 2);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // panelSSN
            // 
            this.panelSSN.BackColor = System.Drawing.Color.Transparent;
            this.panelSSN.Controls.Add(this.socialSecurityNumber);
            this.panelSSN.Controls.Add(this.groupBox1);
            this.panelSSN.Controls.Add(this.labelSSN);
            this.panelSSN.Controls.Add(this.radioButtonSSN);
            this.panelSSN.Enabled = false;
            this.panelSSN.Location = new System.Drawing.Point(110, 108);
            this.panelSSN.Name = "panelSSN";
            this.panelSSN.Size = new System.Drawing.Size(550, 3);
            this.panelSSN.TabIndex = 24;
            this.panelSSN.Visible = false;
            // 
            // socialSecurityNumber
            // 
            this.socialSecurityNumber.CausesValidation = false;
            this.socialSecurityNumber.Location = new System.Drawing.Point(158, 32);
            this.socialSecurityNumber.Name = "socialSecurityNumber";
            this.socialSecurityNumber.Size = new System.Drawing.Size(123, 20);
            this.socialSecurityNumber.TabIndex = 9;
            // 
            // customButtonAddVendor
            // 
            this.customButtonAddVendor.BackColor = System.Drawing.Color.Transparent;
            this.customButtonAddVendor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonAddVendor.BackgroundImage")));
            this.customButtonAddVendor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonAddVendor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonAddVendor.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonAddVendor.FlatAppearance.BorderSize = 0;
            this.customButtonAddVendor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonAddVendor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonAddVendor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonAddVendor.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonAddVendor.ForeColor = System.Drawing.Color.White;
            this.customButtonAddVendor.Location = new System.Drawing.Point(163, 311);
            this.customButtonAddVendor.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonAddVendor.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonAddVendor.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonAddVendor.Name = "customButtonAddVendor";
            this.customButtonAddVendor.Size = new System.Drawing.Size(100, 50);
            this.customButtonAddVendor.TabIndex = 31;
            this.customButtonAddVendor.Text = "Add &Vendor";
            this.customButtonAddVendor.UseVisualStyleBackColor = false;
            this.customButtonAddVendor.Visible = false;
            this.customButtonAddVendor.Click += new System.EventHandler(this.addButton_Click);
            // 
            // customButtonFind
            // 
            this.customButtonFind.BackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonFind.BackgroundImage")));
            this.customButtonFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonFind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonFind.Enabled = false;
            this.customButtonFind.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonFind.FlatAppearance.BorderSize = 0;
            this.customButtonFind.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonFind.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonFind.ForeColor = System.Drawing.Color.White;
            this.customButtonFind.Location = new System.Drawing.Point(585, 311);
            this.customButtonFind.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonFind.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonFind.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonFind.Name = "customButtonFind";
            this.customButtonFind.Size = new System.Drawing.Size(100, 50);
            this.customButtonFind.TabIndex = 29;
            this.customButtonFind.Text = "&Find";
            this.customButtonFind.UseVisualStyleBackColor = false;
            this.customButtonFind.Click += new System.EventHandler(this.findButton_Click);
            // 
            // customButtonClear
            // 
            this.customButtonClear.BackColor = System.Drawing.Color.Transparent;
            this.customButtonClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonClear.BackgroundImage")));
            this.customButtonClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonClear.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonClear.FlatAppearance.BorderSize = 0;
            this.customButtonClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonClear.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonClear.ForeColor = System.Drawing.Color.White;
            this.customButtonClear.Location = new System.Drawing.Point(474, 311);
            this.customButtonClear.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonClear.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonClear.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonClear.Name = "customButtonClear";
            this.customButtonClear.Size = new System.Drawing.Size(100, 50);
            this.customButtonClear.TabIndex = 28;
            this.customButtonClear.Text = "C&lear";
            this.customButtonClear.UseVisualStyleBackColor = false;
            this.customButtonClear.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // customButtonCancel
            // 
            this.customButtonCancel.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonCancel.BackgroundImage")));
            this.customButtonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCancel.FlatAppearance.BorderSize = 0;
            this.customButtonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCancel.ForeColor = System.Drawing.Color.White;
            this.customButtonCancel.Location = new System.Drawing.Point(362, 311);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 27;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.cancelButton_Click);
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
            this.customButtonAddCustomer.Location = new System.Drawing.Point(63, 311);
            this.customButtonAddCustomer.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonAddCustomer.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonAddCustomer.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonAddCustomer.Name = "customButtonAddCustomer";
            this.customButtonAddCustomer.Size = new System.Drawing.Size(100, 50);
            this.customButtonAddCustomer.TabIndex = 26;
            this.customButtonAddCustomer.Text = "Add &Customer";
            this.customButtonAddCustomer.UseVisualStyleBackColor = false;
            this.customButtonAddCustomer.Visible = false;
            this.customButtonAddCustomer.Click += new System.EventHandler(this.addButton_Click);
            // 
            // LookupVendor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Blue;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(707, 397);
            this.ControlBox = false;
            this.Controls.Add(this.customButtonAddVendor);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.customButtonFind);
            this.Controls.Add(this.customButtonClear);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.customButtonAddCustomer);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.panelSSN);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.lookupCustomerLoanNumberPanel);
            this.Controls.Add(this.lookupCustomerInfoLabel);
            this.Controls.Add(this.lookupCustomerCustNumberPanel);
            this.Controls.Add(this.lookupCustomerIDSearchPanel);
            this.Controls.Add(this.lookupCustomerCustInfoPanel);
            this.Controls.Add(this.lookupCustomerPhonePanel);
            this.Controls.Add(this.customerResultsPanel);
            this.Controls.Add(this.lookupCustomerDialogLabel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(50, 13);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LookupVendor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lookup Customer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LookupCustomer_FormClosing);
            this.Load += new System.EventHandler(this.LookupVendor_Load);
            this.panel1.ResumeLayout(false);
            this.vendorSearchGroup.ResumeLayout(false);
            this.vendorSearchGroup.PerformLayout();
            this.lookupCustomerCustNumberPanel.ResumeLayout(false);
            this.lookupCustomerCustNumberPanel.PerformLayout();
            this.lookupCustomerLoanNumberPanel.ResumeLayout(false);
            this.lookupCustomerLoanNumberPanel.PerformLayout();
            this.lookupCustomerPhonePanel.ResumeLayout(false);
            this.lookupCustomerPhonePanel.PerformLayout();
            this.lookupCustomerCustInfoPanel.ResumeLayout(false);
            this.lookupCustomerCustInfoPanel.PerformLayout();
            this.lookupCustomerIDSearchPanel.ResumeLayout(false);
            this.lookupCustomerIDSearchPanel.PerformLayout();
            this.panelSSN.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lookupCustomerDialogLabel;
        private System.Windows.Forms.Label lookupCustomerInfoLabel;
        private System.Windows.Forms.ToolTip toolTipLookupCustomer;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Panel panel1;
        private CustomTextBox lookupVendorName;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton VendorRadiobtn;
        private CustomTextBox lookupVendorTaxID;
        private System.Windows.Forms.RadioButton VendorNameRadiobtn;
        private System.Windows.Forms.RadioButton TaxIDRadiobtn;
        private System.Windows.Forms.Panel vendorSearchGroup;
        private System.Windows.Forms.Panel customerResultsPanel;
        private System.Windows.Forms.GroupBox groupBox7;
        private CustomButton customButtonAddVendor;
        private CustomButton customButtonCancel;
        private CustomButton customButtonClear;
        private CustomButton customButtonFind;
        private CustomButton customButtonAddCustomer;
        private System.Windows.Forms.RadioButton CustNumberRadioBtn;
        private System.Windows.Forms.Label labelCustomerNumber;
        private System.Windows.Forms.GroupBox groupBox5;
        private CustomTextBox lookupCustomerCustNumber;
        private System.Windows.Forms.Panel lookupCustomerCustNumberPanel;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton LoanNumberRadiobtn;
        private System.Windows.Forms.Label labelLoanNumber;
        private CustomTextBox lookupCustomerLoanNumber;
        private System.Windows.Forms.Panel lookupCustomerLoanNumberPanel;
        private System.Windows.Forms.Label labelCustomerPhoneNumber;
        private System.Windows.Forms.RadioButton CustomerPhoneNumRadioBtn;
        private System.Windows.Forms.Label label1OpenBracket;
        private System.Windows.Forms.Label labelCloseBracket;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelHyphen;
        private CustomTextBox phoneAreaCodeTextBox;
        private CustomTextBox phoneNum1TextBox;
        private CustomTextBox phoneNum2TextBox;
        private System.Windows.Forms.Panel lookupCustomerPhonePanel;
        private System.Windows.Forms.RadioButton CustomerInfoRadioBtn;
        private System.Windows.Forms.Label labelCustLastName;
        private System.Windows.Forms.Label labelCustFirstName;
        private System.Windows.Forms.Label labelCustDateOfBirth;
        private System.Windows.Forms.GroupBox groupBox3;
        private CustomTextBox lookupCustomerLastName;
        private CustomTextBox lookupCustomerFirstName;
        private Date dateOfBirth;
        private System.Windows.Forms.Panel lookupCustomerCustInfoPanel;
        private System.Windows.Forms.RadioButton IDSearchRadioBtn;
        private System.Windows.Forms.Label labelIDNumber;
        private System.Windows.Forms.Label labelIDIssuer;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label labelIDType;
        private IDType IdType;
        private State idIssuer1;
        private CustomTextBox lookupCustomerIDNumber;
        private System.Windows.Forms.Panel lookupCustomerIDSearchPanel;
        private System.Windows.Forms.RadioButton radioButtonSSN;
        private System.Windows.Forms.Label labelSSN;
        private System.Windows.Forms.GroupBox groupBox1;
        private SocialSecurityNumber socialSecurityNumber;
        private System.Windows.Forms.Panel panelSSN;
        //private IDTypeDataset iDTypeDataset;
        //private CashlinxDesktop.IDTypeDatasetTableAdapters.DATEDIDENTTYPETableAdapter dATEDIDENTTYPETableAdapter;
    }
}
