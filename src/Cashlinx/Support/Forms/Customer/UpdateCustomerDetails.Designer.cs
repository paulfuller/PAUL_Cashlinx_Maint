using Common.Libraries.Forms.Components;
using Common.Libraries.Utility.Shared;
using Support.Forms.UserControls;
using Support.Libraries.Forms.Components;

namespace Support.Forms.Customer
{
    partial class UpdateCustomerDetails
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
            this.labelUpdateCustDetailsHeading = new System.Windows.Forms.Label();
            this.labelHeadingInfo = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelMiddleInitial = new System.Windows.Forms.Label();
            this.labelTitleSuffix = new System.Windows.Forms.Label();
            this.labelSSN = new System.Windows.Forms.Label();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.customLabelFirstName = new Common.Libraries.Forms.Components.CustomLabel();
            this.customLabelLastName = new Common.Libraries.Forms.Components.CustomLabel();
            this.customLabelNegotiationLanguage = new Common.Libraries.Forms.Components.CustomLabel();
            this.labelDOB = new Common.Libraries.Forms.Components.CustomLabel();
            this.custFirstName = new Common.Libraries.Forms.Components.CustomTextBox();
            this.custMiddleInitial = new Common.Libraries.Forms.Components.CustomTextBox();
            this.custLastName = new Common.Libraries.Forms.Components.CustomTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lableMarStatus = new System.Windows.Forms.Label();
            this.lableSpouseFName = new System.Windows.Forms.Label();
            this.lableSpouseLName = new System.Windows.Forms.Label();
            this.lableSpouseSSN = new System.Windows.Forms.Label();
            this.lableHowLongAtAddress = new System.Windows.Forms.Label();
            this.lableHLAAYears = new System.Windows.Forms.Label();
            this.lableHLAAMonths = new System.Windows.Forms.Label();
            this.lableOwnHome = new System.Windows.Forms.Label();
            this.lableMonthlyRent = new System.Windows.Forms.Label();
            this.lableMilitaryStationedLocal = new System.Windows.Forms.Label();
            this.lableCustSeqNumber = new System.Windows.Forms.Label();
            this.lablePrivacyNotifDate = new System.Windows.Forms.Label();
            this.lableOptOutFlag = new System.Windows.Forms.Label();
            this.lableCustStatus = new System.Windows.Forms.Label();
            this.lableReasonCode = new System.Windows.Forms.Label();
            this.lableLastVerDate = new System.Windows.Forms.Label();
            this.lableNextVerDate = new System.Windows.Forms.Label();
            this.lablePDLCoolingOffDate = new System.Windows.Forms.Label();
            this.lablePDLCustSince = new System.Windows.Forms.Label();
            this.lableSpanishForm = new System.Windows.Forms.Label();
            this.lablePRBC = new System.Windows.Forms.Label();
            this.lableBankruptcyProtection = new System.Windows.Forms.Label();
            this.txtBoxSpouseFName = new Common.Libraries.Forms.Components.CustomTextBox();
            this.txtBoxSpouseLName = new Common.Libraries.Forms.Components.CustomTextBox();
            this.txtBoxSpouseSSN = new Common.Libraries.Forms.Components.CustomTextBox();
            this.txtBoxHLAAYears = new Common.Libraries.Forms.Components.CustomTextBox();
            this.txtBoxHLAAMonths = new Common.Libraries.Forms.Components.CustomTextBox();
            this.txtBoxOwnHome = new Common.Libraries.Forms.Components.CustomTextBox();
            this.txtBoxMonthlyRent = new Common.Libraries.Forms.Components.CustomTextBox();
            this.txtBoxCustSeqNumber = new Common.Libraries.Forms.Components.CustomTextBox();
            this.txtBoxPrivacyNotifDate = new Common.Libraries.Forms.Components.CustomTextBox();
            this.txtBoxCustStatus = new Common.Libraries.Forms.Components.CustomTextBox();
            this.txtBoxReasonCode = new Common.Libraries.Forms.Components.CustomTextBox();
            this.txtBoxPDLCoolingOffDate = new Common.Libraries.Forms.Components.CustomTextBox();
            this.txtBoxPDLCustSince = new Common.Libraries.Forms.Components.CustomTextBox();
            this.BankruptcyProtectionradioButton1 = new System.Windows.Forms.RadioButton();
            this.MilitaryStationedLocalcheckBox = new System.Windows.Forms.CheckBox();
            this.OptOutFlagcheckBox = new System.Windows.Forms.CheckBox();
            this.SpanishFormcheckBox1 = new System.Windows.Forms.CheckBox();
            this.PRBCcheckBox = new System.Windows.Forms.CheckBox();
            this.txtBoxMarStatus = new System.Windows.Forms.ComboBox();
            this.BtnCustomerComments = new Support.Libraries.Forms.Components.SupportButton();
            this.ChangeStatusCustomButton = new Support.Libraries.Forms.Components.SupportButton();
            this.custSSN = new Support.Forms.SocialSecurityNumber();
            this.custDateOfBirth = new Support.Forms.Date();
            this.titleSuffix1 = new Support.Forms.UserControls.TitleSuffix();
            this.customHistoryButton = new Support.Libraries.Forms.Components.SupportButton();
            this.customBackButton = new Support.Libraries.Forms.Components.SupportButton();
            this.customButtonSubmit = new Support.Libraries.Forms.Components.SupportButton();
            this.customButtonReset = new Support.Libraries.Forms.Components.SupportButton();
            this.customButtonCancel = new Support.Libraries.Forms.Components.SupportButton();
            this.title1 = new Support.Forms.UserControls.Title();
            this.txtBoxLastVerDate = new Support.Forms.Date();
            this.txtBoxNextVerDate = new Support.Forms.Date();
            this.SuspendLayout();
            // 
            // labelUpdateCustDetailsHeading
            // 
            this.labelUpdateCustDetailsHeading.AutoSize = true;
            this.labelUpdateCustDetailsHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelUpdateCustDetailsHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUpdateCustDetailsHeading.ForeColor = System.Drawing.Color.White;
            this.labelUpdateCustDetailsHeading.Location = new System.Drawing.Point(294, 24);
            this.labelUpdateCustDetailsHeading.Name = "labelUpdateCustDetailsHeading";
            this.labelUpdateCustDetailsHeading.Size = new System.Drawing.Size(149, 19);
            this.labelUpdateCustDetailsHeading.TabIndex = 0;
            this.labelUpdateCustDetailsHeading.Text = "Customer Details";
            // 
            // labelHeadingInfo
            // 
            this.labelHeadingInfo.AutoSize = true;
            this.labelHeadingInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelHeadingInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeadingInfo.ForeColor = System.Drawing.Color.White;
            this.labelHeadingInfo.Location = new System.Drawing.Point(11, 39);
            this.labelHeadingInfo.Name = "labelHeadingInfo";
            this.labelHeadingInfo.Size = new System.Drawing.Size(330, 13);
            this.labelHeadingInfo.TabIndex = 1;
            this.labelHeadingInfo.Text = "Update all necessary fields and click Submit button to save changes";
            this.labelHeadingInfo.Visible = false;
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(28, 89);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(27, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Title";
            // 
            // labelMiddleInitial
            // 
            this.labelMiddleInitial.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelMiddleInitial.AutoSize = true;
            this.labelMiddleInitial.BackColor = System.Drawing.Color.Transparent;
            this.labelMiddleInitial.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMiddleInitial.Location = new System.Drawing.Point(28, 141);
            this.labelMiddleInitial.Name = "labelMiddleInitial";
            this.labelMiddleInitial.Size = new System.Drawing.Size(66, 13);
            this.labelMiddleInitial.TabIndex = 2;
            this.labelMiddleInitial.Text = "Middle Initial";
            // 
            // labelTitleSuffix
            // 
            this.labelTitleSuffix.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTitleSuffix.AutoSize = true;
            this.labelTitleSuffix.BackColor = System.Drawing.Color.Transparent;
            this.labelTitleSuffix.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleSuffix.Location = new System.Drawing.Point(30, 193);
            this.labelTitleSuffix.Name = "labelTitleSuffix";
            this.labelTitleSuffix.Size = new System.Drawing.Size(58, 13);
            this.labelTitleSuffix.TabIndex = 4;
            this.labelTitleSuffix.Text = "Title Suffix";
            // 
            // labelSSN
            // 
            this.labelSSN.BackColor = System.Drawing.Color.Transparent;
            this.labelSSN.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSSN.Location = new System.Drawing.Point(28, 219);
            this.labelSSN.Name = "labelSSN";
            this.labelSSN.Size = new System.Drawing.Size(116, 13);
            this.labelSSN.TabIndex = 7;
            this.labelSSN.Text = "Social Security Number";
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Items.AddRange(new object[] {
            "Select",
            "Yes",
            "No"});
            this.comboBoxLanguage.Location = new System.Drawing.Point(145, 237);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(98, 21);
            this.comboBoxLanguage.TabIndex = 6;
            // 
            // customLabelFirstName
            // 
            this.customLabelFirstName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelFirstName.AutoSize = true;
            this.customLabelFirstName.BackColor = System.Drawing.Color.Transparent;
            this.customLabelFirstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelFirstName.Location = new System.Drawing.Point(28, 115);
            this.customLabelFirstName.Name = "customLabelFirstName";
            this.customLabelFirstName.Required = true;
            this.customLabelFirstName.Size = new System.Drawing.Size(58, 13);
            this.customLabelFirstName.TabIndex = 9;
            this.customLabelFirstName.Text = "First Name";
            // 
            // customLabelLastName
            // 
            this.customLabelLastName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelLastName.AutoSize = true;
            this.customLabelLastName.BackColor = System.Drawing.Color.Transparent;
            this.customLabelLastName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelLastName.Location = new System.Drawing.Point(28, 167);
            this.customLabelLastName.Name = "customLabelLastName";
            this.customLabelLastName.Required = true;
            this.customLabelLastName.Size = new System.Drawing.Size(57, 13);
            this.customLabelLastName.TabIndex = 10;
            this.customLabelLastName.Text = "Last Name";
            // 
            // customLabelNegotiationLanguage
            // 
            this.customLabelNegotiationLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelNegotiationLanguage.AutoSize = true;
            this.customLabelNegotiationLanguage.BackColor = System.Drawing.Color.Transparent;
            this.customLabelNegotiationLanguage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelNegotiationLanguage.Location = new System.Drawing.Point(28, 245);
            this.customLabelNegotiationLanguage.Name = "customLabelNegotiationLanguage";
            this.customLabelNegotiationLanguage.Required = true;
            this.customLabelNegotiationLanguage.Size = new System.Drawing.Size(112, 13);
            this.customLabelNegotiationLanguage.TabIndex = 11;
            this.customLabelNegotiationLanguage.Text = "Negotiation Language";
            // 
            // labelDOB
            // 
            this.labelDOB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelDOB.AutoSize = true;
            this.labelDOB.BackColor = System.Drawing.Color.Transparent;
            this.labelDOB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDOB.Location = new System.Drawing.Point(28, 297);
            this.labelDOB.Name = "labelDOB";
            this.labelDOB.Required = true;
            this.labelDOB.Size = new System.Drawing.Size(68, 13);
            this.labelDOB.TabIndex = 12;
            this.labelDOB.Text = "Date of Birth";
            // 
            // custFirstName
            // 
            this.custFirstName.CausesValidation = false;
            this.custFirstName.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.custFirstName.FirstLetterUppercase = true;
            this.custFirstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custFirstName.Location = new System.Drawing.Point(145, 107);
            this.custFirstName.MaxLength = 40;
            this.custFirstName.Name = "custFirstName";
            this.custFirstName.Required = true;
            this.custFirstName.Size = new System.Drawing.Size(222, 21);
            this.custFirstName.TabIndex = 2;
            this.custFirstName.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // custMiddleInitial
            // 
            this.custMiddleInitial.CausesValidation = false;
            this.custMiddleInitial.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.custMiddleInitial.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custMiddleInitial.Location = new System.Drawing.Point(145, 133);
            this.custMiddleInitial.MaxLength = 40;
            this.custMiddleInitial.Name = "custMiddleInitial";
            this.custMiddleInitial.Size = new System.Drawing.Size(100, 21);
            this.custMiddleInitial.TabIndex = 3;
            this.custMiddleInitial.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // custLastName
            // 
            this.custLastName.CausesValidation = false;
            this.custLastName.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.custLastName.FirstLetterUppercase = true;
            this.custLastName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custLastName.Location = new System.Drawing.Point(145, 159);
            this.custLastName.MaxLength = 40;
            this.custLastName.Name = "custLastName";
            this.custLastName.Required = true;
            this.custLastName.Size = new System.Drawing.Size(222, 21);
            this.custLastName.TabIndex = 4;
            this.custLastName.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Location = new System.Drawing.Point(12, 508);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(749, 10);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // lableMarStatus
            // 
            this.lableMarStatus.AutoSize = true;
            this.lableMarStatus.BackColor = System.Drawing.Color.Transparent;
            this.lableMarStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableMarStatus.Location = new System.Drawing.Point(30, 271);
            this.lableMarStatus.Name = "lableMarStatus";
            this.lableMarStatus.Size = new System.Drawing.Size(73, 13);
            this.lableMarStatus.TabIndex = 17;
            this.lableMarStatus.Text = "Marital Status";
            // 
            // lableSpouseFName
            // 
            this.lableSpouseFName.AutoSize = true;
            this.lableSpouseFName.BackColor = System.Drawing.Color.Transparent;
            this.lableSpouseFName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableSpouseFName.Location = new System.Drawing.Point(28, 323);
            this.lableSpouseFName.Name = "lableSpouseFName";
            this.lableSpouseFName.Size = new System.Drawing.Size(96, 13);
            this.lableSpouseFName.TabIndex = 19;
            this.lableSpouseFName.Text = "Spouse First Name";
            // 
            // lableSpouseLName
            // 
            this.lableSpouseLName.AutoSize = true;
            this.lableSpouseLName.BackColor = System.Drawing.Color.Transparent;
            this.lableSpouseLName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableSpouseLName.Location = new System.Drawing.Point(28, 349);
            this.lableSpouseLName.Name = "lableSpouseLName";
            this.lableSpouseLName.Size = new System.Drawing.Size(95, 13);
            this.lableSpouseLName.TabIndex = 21;
            this.lableSpouseLName.Text = "Spouse Last Name";
            // 
            // lableSpouseSSN
            // 
            this.lableSpouseSSN.AutoSize = true;
            this.lableSpouseSSN.BackColor = System.Drawing.Color.Transparent;
            this.lableSpouseSSN.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableSpouseSSN.Location = new System.Drawing.Point(28, 375);
            this.lableSpouseSSN.Name = "lableSpouseSSN";
            this.lableSpouseSSN.Size = new System.Drawing.Size(62, 13);
            this.lableSpouseSSN.TabIndex = 23;
            this.lableSpouseSSN.Text = "Spouse Ssn";
            // 
            // lableHowLongAtAddress
            // 
            this.lableHowLongAtAddress.AutoSize = true;
            this.lableHowLongAtAddress.BackColor = System.Drawing.Color.Transparent;
            this.lableHowLongAtAddress.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableHowLongAtAddress.Location = new System.Drawing.Point(28, 405);
            this.lableHowLongAtAddress.Name = "lableHowLongAtAddress";
            this.lableHowLongAtAddress.Size = new System.Drawing.Size(110, 13);
            this.lableHowLongAtAddress.TabIndex = 25;
            this.lableHowLongAtAddress.Text = "How Long At Address";
            // 
            // lableHLAAYears
            // 
            this.lableHLAAYears.AutoSize = true;
            this.lableHLAAYears.BackColor = System.Drawing.Color.Transparent;
            this.lableHLAAYears.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableHLAAYears.Location = new System.Drawing.Point(56, 422);
            this.lableHLAAYears.Name = "lableHLAAYears";
            this.lableHLAAYears.Size = new System.Drawing.Size(34, 13);
            this.lableHLAAYears.TabIndex = 27;
            this.lableHLAAYears.Text = "Years";
            // 
            // lableHLAAMonths
            // 
            this.lableHLAAMonths.AutoSize = true;
            this.lableHLAAMonths.BackColor = System.Drawing.Color.Transparent;
            this.lableHLAAMonths.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableHLAAMonths.Location = new System.Drawing.Point(56, 447);
            this.lableHLAAMonths.Name = "lableHLAAMonths";
            this.lableHLAAMonths.Size = new System.Drawing.Size(42, 13);
            this.lableHLAAMonths.TabIndex = 29;
            this.lableHLAAMonths.Text = "Months";
            // 
            // lableOwnHome
            // 
            this.lableOwnHome.AutoSize = true;
            this.lableOwnHome.BackColor = System.Drawing.Color.Transparent;
            this.lableOwnHome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableOwnHome.Location = new System.Drawing.Point(178, 422);
            this.lableOwnHome.Name = "lableOwnHome";
            this.lableOwnHome.Size = new System.Drawing.Size(59, 13);
            this.lableOwnHome.TabIndex = 31;
            this.lableOwnHome.Text = "Own Home";
            // 
            // lableMonthlyRent
            // 
            this.lableMonthlyRent.AutoSize = true;
            this.lableMonthlyRent.BackColor = System.Drawing.Color.Transparent;
            this.lableMonthlyRent.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableMonthlyRent.Location = new System.Drawing.Point(178, 447);
            this.lableMonthlyRent.Name = "lableMonthlyRent";
            this.lableMonthlyRent.Size = new System.Drawing.Size(71, 13);
            this.lableMonthlyRent.TabIndex = 33;
            this.lableMonthlyRent.Text = "Monthly Rent";
            // 
            // lableMilitaryStationedLocal
            // 
            this.lableMilitaryStationedLocal.AutoSize = true;
            this.lableMilitaryStationedLocal.BackColor = System.Drawing.Color.Transparent;
            this.lableMilitaryStationedLocal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableMilitaryStationedLocal.Location = new System.Drawing.Point(28, 472);
            this.lableMilitaryStationedLocal.Name = "lableMilitaryStationedLocal";
            this.lableMilitaryStationedLocal.Size = new System.Drawing.Size(117, 13);
            this.lableMilitaryStationedLocal.TabIndex = 35;
            this.lableMilitaryStationedLocal.Text = "Military Stationed Local";
            // 
            // lableCustSeqNumber
            // 
            this.lableCustSeqNumber.AutoSize = true;
            this.lableCustSeqNumber.BackColor = System.Drawing.Color.Transparent;
            this.lableCustSeqNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableCustSeqNumber.Location = new System.Drawing.Point(372, 89);
            this.lableCustSeqNumber.Name = "lableCustSeqNumber";
            this.lableCustSeqNumber.Size = new System.Drawing.Size(119, 13);
            this.lableCustSeqNumber.TabIndex = 37;
            this.lableCustSeqNumber.Text = "Cust Sequence Number";
            // 
            // lablePrivacyNotifDate
            // 
            this.lablePrivacyNotifDate.AutoSize = true;
            this.lablePrivacyNotifDate.BackColor = System.Drawing.Color.Transparent;
            this.lablePrivacyNotifDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablePrivacyNotifDate.Location = new System.Drawing.Point(372, 115);
            this.lablePrivacyNotifDate.Name = "lablePrivacyNotifDate";
            this.lablePrivacyNotifDate.Size = new System.Drawing.Size(125, 13);
            this.lablePrivacyNotifDate.TabIndex = 39;
            this.lablePrivacyNotifDate.Text = "Privacy Notification Date";
            // 
            // lableOptOutFlag
            // 
            this.lableOptOutFlag.AutoSize = true;
            this.lableOptOutFlag.BackColor = System.Drawing.Color.Transparent;
            this.lableOptOutFlag.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableOptOutFlag.Location = new System.Drawing.Point(372, 141);
            this.lableOptOutFlag.Name = "lableOptOutFlag";
            this.lableOptOutFlag.Size = new System.Drawing.Size(69, 13);
            this.lableOptOutFlag.TabIndex = 41;
            this.lableOptOutFlag.Text = "Opt Out Flag";
            // 
            // lableCustStatus
            // 
            this.lableCustStatus.AutoSize = true;
            this.lableCustStatus.BackColor = System.Drawing.Color.Transparent;
            this.lableCustStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableCustStatus.Location = new System.Drawing.Point(372, 195);
            this.lableCustStatus.Name = "lableCustStatus";
            this.lableCustStatus.Size = new System.Drawing.Size(38, 13);
            this.lableCustStatus.TabIndex = 43;
            this.lableCustStatus.Text = "Status";
            // 
            // lableReasonCode
            // 
            this.lableReasonCode.AutoSize = true;
            this.lableReasonCode.BackColor = System.Drawing.Color.Transparent;
            this.lableReasonCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableReasonCode.Location = new System.Drawing.Point(372, 221);
            this.lableReasonCode.Name = "lableReasonCode";
            this.lableReasonCode.Size = new System.Drawing.Size(71, 13);
            this.lableReasonCode.TabIndex = 45;
            this.lableReasonCode.Text = "Reason Code";
            // 
            // lableLastVerDate
            // 
            this.lableLastVerDate.AutoSize = true;
            this.lableLastVerDate.BackColor = System.Drawing.Color.Transparent;
            this.lableLastVerDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableLastVerDate.Location = new System.Drawing.Point(372, 258);
            this.lableLastVerDate.Name = "lableLastVerDate";
            this.lableLastVerDate.Size = new System.Drawing.Size(109, 13);
            this.lableLastVerDate.TabIndex = 47;
            this.lableLastVerDate.Text = "Last Verification Date";
            // 
            // lableNextVerDate
            // 
            this.lableNextVerDate.AutoSize = true;
            this.lableNextVerDate.BackColor = System.Drawing.Color.Transparent;
            this.lableNextVerDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableNextVerDate.Location = new System.Drawing.Point(372, 284);
            this.lableNextVerDate.Name = "lableNextVerDate";
            this.lableNextVerDate.Size = new System.Drawing.Size(112, 13);
            this.lableNextVerDate.TabIndex = 49;
            this.lableNextVerDate.Text = "Next Verification Date";
            // 
            // lablePDLCoolingOffDate
            // 
            this.lablePDLCoolingOffDate.AutoSize = true;
            this.lablePDLCoolingOffDate.BackColor = System.Drawing.Color.Transparent;
            this.lablePDLCoolingOffDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablePDLCoolingOffDate.Location = new System.Drawing.Point(372, 310);
            this.lablePDLCoolingOffDate.Name = "lablePDLCoolingOffDate";
            this.lablePDLCoolingOffDate.Size = new System.Drawing.Size(112, 13);
            this.lablePDLCoolingOffDate.TabIndex = 51;
            this.lablePDLCoolingOffDate.Text = "Cooling Off Date (Pdl)";
            // 
            // lablePDLCustSince
            // 
            this.lablePDLCustSince.AutoSize = true;
            this.lablePDLCustSince.BackColor = System.Drawing.Color.Transparent;
            this.lablePDLCustSince.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablePDLCustSince.Location = new System.Drawing.Point(372, 336);
            this.lablePDLCustSince.Name = "lablePDLCustSince";
            this.lablePDLCustSince.Size = new System.Drawing.Size(106, 13);
            this.lablePDLCustSince.TabIndex = 53;
            this.lablePDLCustSince.Text = "Customer Since (Pdl)";
            // 
            // lableSpanishForm
            // 
            this.lableSpanishForm.AutoSize = true;
            this.lableSpanishForm.BackColor = System.Drawing.Color.Transparent;
            this.lableSpanishForm.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableSpanishForm.Location = new System.Drawing.Point(372, 373);
            this.lableSpanishForm.Name = "lableSpanishForm";
            this.lableSpanishForm.Size = new System.Drawing.Size(71, 13);
            this.lableSpanishForm.TabIndex = 55;
            this.lableSpanishForm.Text = "Spanish Form";
            // 
            // lablePRBC
            // 
            this.lablePRBC.AutoSize = true;
            this.lablePRBC.BackColor = System.Drawing.Color.Transparent;
            this.lablePRBC.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablePRBC.Location = new System.Drawing.Point(372, 399);
            this.lablePRBC.Name = "lablePRBC";
            this.lablePRBC.Size = new System.Drawing.Size(28, 13);
            this.lablePRBC.TabIndex = 57;
            this.lablePRBC.Text = "Prbc";
            // 
            // lableBankruptcyProtection
            // 
            this.lableBankruptcyProtection.AutoSize = true;
            this.lableBankruptcyProtection.BackColor = System.Drawing.Color.Transparent;
            this.lableBankruptcyProtection.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableBankruptcyProtection.Location = new System.Drawing.Point(372, 447);
            this.lableBankruptcyProtection.Name = "lableBankruptcyProtection";
            this.lableBankruptcyProtection.Size = new System.Drawing.Size(373, 13);
            this.lableBankruptcyProtection.TabIndex = 59;
            this.lableBankruptcyProtection.Text = "Are you In or are Planning to file for Chapter 7 or 13 Bankruptcy Protection?";
            // 
            // txtBoxSpouseFName
            // 
            this.txtBoxSpouseFName.CausesValidation = false;
            this.txtBoxSpouseFName.Enabled = false;
            this.txtBoxSpouseFName.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.txtBoxSpouseFName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxSpouseFName.Location = new System.Drawing.Point(145, 315);
            this.txtBoxSpouseFName.Name = "txtBoxSpouseFName";
            this.txtBoxSpouseFName.Size = new System.Drawing.Size(100, 21);
            this.txtBoxSpouseFName.TabIndex = 20;
            this.txtBoxSpouseFName.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // txtBoxSpouseLName
            // 
            this.txtBoxSpouseLName.CausesValidation = false;
            this.txtBoxSpouseLName.Enabled = false;
            this.txtBoxSpouseLName.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.txtBoxSpouseLName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxSpouseLName.Location = new System.Drawing.Point(145, 341);
            this.txtBoxSpouseLName.Name = "txtBoxSpouseLName";
            this.txtBoxSpouseLName.Size = new System.Drawing.Size(100, 21);
            this.txtBoxSpouseLName.TabIndex = 22;
            this.txtBoxSpouseLName.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // txtBoxSpouseSSN
            // 
            this.txtBoxSpouseSSN.CausesValidation = false;
            this.txtBoxSpouseSSN.Enabled = false;
            this.txtBoxSpouseSSN.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.txtBoxSpouseSSN.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxSpouseSSN.Location = new System.Drawing.Point(145, 367);
            this.txtBoxSpouseSSN.Name = "txtBoxSpouseSSN";
            this.txtBoxSpouseSSN.Size = new System.Drawing.Size(100, 21);
            this.txtBoxSpouseSSN.TabIndex = 24;
            this.txtBoxSpouseSSN.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // txtBoxHLAAYears
            // 
            this.txtBoxHLAAYears.CausesValidation = false;
            this.txtBoxHLAAYears.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.txtBoxHLAAYears.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxHLAAYears.Location = new System.Drawing.Point(147, 414);
            this.txtBoxHLAAYears.MaxLength = 2;
            this.txtBoxHLAAYears.Name = "txtBoxHLAAYears";
            this.txtBoxHLAAYears.Size = new System.Drawing.Size(25, 21);
            this.txtBoxHLAAYears.TabIndex = 9;
            this.txtBoxHLAAYears.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // txtBoxHLAAMonths
            // 
            this.txtBoxHLAAMonths.CausesValidation = false;
            this.txtBoxHLAAMonths.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.txtBoxHLAAMonths.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxHLAAMonths.Location = new System.Drawing.Point(147, 439);
            this.txtBoxHLAAMonths.MaxLength = 2;
            this.txtBoxHLAAMonths.Name = "txtBoxHLAAMonths";
            this.txtBoxHLAAMonths.Size = new System.Drawing.Size(25, 21);
            this.txtBoxHLAAMonths.TabIndex = 11;
            this.txtBoxHLAAMonths.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // txtBoxOwnHome
            // 
            this.txtBoxOwnHome.CausesValidation = false;
            this.txtBoxOwnHome.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.txtBoxOwnHome.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxOwnHome.Location = new System.Drawing.Point(254, 414);
            this.txtBoxOwnHome.Name = "txtBoxOwnHome";
            this.txtBoxOwnHome.Size = new System.Drawing.Size(14, 21);
            this.txtBoxOwnHome.TabIndex = 10;
            this.txtBoxOwnHome.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // txtBoxMonthlyRent
            // 
            this.txtBoxMonthlyRent.CausesValidation = false;
            this.txtBoxMonthlyRent.Enabled = false;
            this.txtBoxMonthlyRent.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.txtBoxMonthlyRent.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxMonthlyRent.Location = new System.Drawing.Point(254, 439);
            this.txtBoxMonthlyRent.Name = "txtBoxMonthlyRent";
            this.txtBoxMonthlyRent.Size = new System.Drawing.Size(97, 21);
            this.txtBoxMonthlyRent.TabIndex = 34;
            this.txtBoxMonthlyRent.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // txtBoxCustSeqNumber
            // 
            this.txtBoxCustSeqNumber.CausesValidation = false;
            this.txtBoxCustSeqNumber.Enabled = false;
            this.txtBoxCustSeqNumber.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.txtBoxCustSeqNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxCustSeqNumber.Location = new System.Drawing.Point(503, 89);
            this.txtBoxCustSeqNumber.Name = "txtBoxCustSeqNumber";
            this.txtBoxCustSeqNumber.Size = new System.Drawing.Size(100, 21);
            this.txtBoxCustSeqNumber.TabIndex = 38;
            this.txtBoxCustSeqNumber.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // txtBoxPrivacyNotifDate
            // 
            this.txtBoxPrivacyNotifDate.CausesValidation = false;
            this.txtBoxPrivacyNotifDate.Enabled = false;
            this.txtBoxPrivacyNotifDate.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.txtBoxPrivacyNotifDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxPrivacyNotifDate.Location = new System.Drawing.Point(503, 115);
            this.txtBoxPrivacyNotifDate.Name = "txtBoxPrivacyNotifDate";
            this.txtBoxPrivacyNotifDate.Size = new System.Drawing.Size(100, 21);
            this.txtBoxPrivacyNotifDate.TabIndex = 40;
            this.txtBoxPrivacyNotifDate.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // txtBoxCustStatus
            // 
            this.txtBoxCustStatus.CausesValidation = false;
            this.txtBoxCustStatus.Enabled = false;
            this.txtBoxCustStatus.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.txtBoxCustStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxCustStatus.Location = new System.Drawing.Point(503, 195);
            this.txtBoxCustStatus.Name = "txtBoxCustStatus";
            this.txtBoxCustStatus.Size = new System.Drawing.Size(100, 21);
            this.txtBoxCustStatus.TabIndex = 44;
            this.txtBoxCustStatus.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // txtBoxReasonCode
            // 
            this.txtBoxReasonCode.CausesValidation = false;
            this.txtBoxReasonCode.Enabled = false;
            this.txtBoxReasonCode.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.txtBoxReasonCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxReasonCode.Location = new System.Drawing.Point(503, 221);
            this.txtBoxReasonCode.Name = "txtBoxReasonCode";
            this.txtBoxReasonCode.Size = new System.Drawing.Size(100, 21);
            this.txtBoxReasonCode.TabIndex = 46;
            this.txtBoxReasonCode.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // txtBoxPDLCoolingOffDate
            // 
            this.txtBoxPDLCoolingOffDate.CausesValidation = false;
            this.txtBoxPDLCoolingOffDate.Enabled = false;
            this.txtBoxPDLCoolingOffDate.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.txtBoxPDLCoolingOffDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxPDLCoolingOffDate.Location = new System.Drawing.Point(503, 310);
            this.txtBoxPDLCoolingOffDate.Name = "txtBoxPDLCoolingOffDate";
            this.txtBoxPDLCoolingOffDate.Size = new System.Drawing.Size(100, 21);
            this.txtBoxPDLCoolingOffDate.TabIndex = 52;
            this.txtBoxPDLCoolingOffDate.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // txtBoxPDLCustSince
            // 
            this.txtBoxPDLCustSince.CausesValidation = false;
            this.txtBoxPDLCustSince.Enabled = false;
            this.txtBoxPDLCustSince.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.txtBoxPDLCustSince.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxPDLCustSince.Location = new System.Drawing.Point(503, 336);
            this.txtBoxPDLCustSince.Name = "txtBoxPDLCustSince";
            this.txtBoxPDLCustSince.Size = new System.Drawing.Size(100, 21);
            this.txtBoxPDLCustSince.TabIndex = 54;
            this.txtBoxPDLCustSince.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // BankruptcyProtectionradioButton1
            // 
            this.BankruptcyProtectionradioButton1.AutoSize = true;
            this.BankruptcyProtectionradioButton1.BackColor = System.Drawing.Color.Transparent;
            this.BankruptcyProtectionradioButton1.Enabled = false;
            this.BankruptcyProtectionradioButton1.Location = new System.Drawing.Point(747, 447);
            this.BankruptcyProtectionradioButton1.Name = "BankruptcyProtectionradioButton1";
            this.BankruptcyProtectionradioButton1.Size = new System.Drawing.Size(14, 13);
            this.BankruptcyProtectionradioButton1.TabIndex = 61;
            this.BankruptcyProtectionradioButton1.TabStop = true;
            this.BankruptcyProtectionradioButton1.UseVisualStyleBackColor = false;
            // 
            // MilitaryStationedLocalcheckBox
            // 
            this.MilitaryStationedLocalcheckBox.AutoSize = true;
            this.MilitaryStationedLocalcheckBox.BackColor = System.Drawing.Color.Transparent;
            this.MilitaryStationedLocalcheckBox.Location = new System.Drawing.Point(147, 471);
            this.MilitaryStationedLocalcheckBox.Name = "MilitaryStationedLocalcheckBox";
            this.MilitaryStationedLocalcheckBox.Size = new System.Drawing.Size(15, 14);
            this.MilitaryStationedLocalcheckBox.TabIndex = 12;
            this.MilitaryStationedLocalcheckBox.UseVisualStyleBackColor = false;
            // 
            // OptOutFlagcheckBox
            // 
            this.OptOutFlagcheckBox.AutoSize = true;
            this.OptOutFlagcheckBox.BackColor = System.Drawing.Color.Transparent;
            this.OptOutFlagcheckBox.Location = new System.Drawing.Point(501, 142);
            this.OptOutFlagcheckBox.Name = "OptOutFlagcheckBox";
            this.OptOutFlagcheckBox.Size = new System.Drawing.Size(15, 14);
            this.OptOutFlagcheckBox.TabIndex = 13;
            this.OptOutFlagcheckBox.UseVisualStyleBackColor = false;
            // 
            // SpanishFormcheckBox1
            // 
            this.SpanishFormcheckBox1.AutoSize = true;
            this.SpanishFormcheckBox1.BackColor = System.Drawing.Color.Transparent;
            this.SpanishFormcheckBox1.Enabled = false;
            this.SpanishFormcheckBox1.Location = new System.Drawing.Point(502, 376);
            this.SpanishFormcheckBox1.Name = "SpanishFormcheckBox1";
            this.SpanishFormcheckBox1.Size = new System.Drawing.Size(15, 14);
            this.SpanishFormcheckBox1.TabIndex = 64;
            this.SpanishFormcheckBox1.UseVisualStyleBackColor = false;
            // 
            // PRBCcheckBox
            // 
            this.PRBCcheckBox.AutoSize = true;
            this.PRBCcheckBox.BackColor = System.Drawing.Color.Transparent;
            this.PRBCcheckBox.Enabled = false;
            this.PRBCcheckBox.Location = new System.Drawing.Point(501, 402);
            this.PRBCcheckBox.Name = "PRBCcheckBox";
            this.PRBCcheckBox.Size = new System.Drawing.Size(15, 14);
            this.PRBCcheckBox.TabIndex = 65;
            this.PRBCcheckBox.UseVisualStyleBackColor = false;
            // 
            // txtBoxMarStatus
            // 
            this.txtBoxMarStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtBoxMarStatus.FormattingEnabled = true;
            this.txtBoxMarStatus.Items.AddRange(new object[] {
            "Divorced",
            "Unknown",
            "Married",
            "Separated",
            "Single",
            "Widower/Widow"});
            this.txtBoxMarStatus.Location = new System.Drawing.Point(145, 263);
            this.txtBoxMarStatus.Name = "txtBoxMarStatus";
            this.txtBoxMarStatus.Size = new System.Drawing.Size(98, 21);
            this.txtBoxMarStatus.TabIndex = 7;
            // 
            // BtnCustomerComments
            // 
            this.BtnCustomerComments.BackColor = System.Drawing.Color.Transparent;
            this.BtnCustomerComments.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.BtnCustomerComments.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCustomerComments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCustomerComments.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BtnCustomerComments.FlatAppearance.BorderSize = 0;
            this.BtnCustomerComments.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnCustomerComments.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnCustomerComments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCustomerComments.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCustomerComments.ForeColor = System.Drawing.Color.White;
            this.BtnCustomerComments.Location = new System.Drawing.Point(627, 366);
            this.BtnCustomerComments.Margin = new System.Windows.Forms.Padding(0);
            this.BtnCustomerComments.MaximumSize = new System.Drawing.Size(90, 40);
            this.BtnCustomerComments.MinimumSize = new System.Drawing.Size(90, 40);
            this.BtnCustomerComments.Name = "BtnCustomerComments";
            this.BtnCustomerComments.Size = new System.Drawing.Size(90, 40);
            this.BtnCustomerComments.TabIndex = 17;
            this.BtnCustomerComments.Text = "Customer Comments";
            this.BtnCustomerComments.UseVisualStyleBackColor = false;
            this.BtnCustomerComments.Click += new System.EventHandler(this.BtnCustomerComments_Click);
            // 
            // ChangeStatusCustomButton
            // 
            this.ChangeStatusCustomButton.BackColor = System.Drawing.Color.Transparent;
            this.ChangeStatusCustomButton.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.ChangeStatusCustomButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ChangeStatusCustomButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChangeStatusCustomButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ChangeStatusCustomButton.FlatAppearance.BorderSize = 0;
            this.ChangeStatusCustomButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.ChangeStatusCustomButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ChangeStatusCustomButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChangeStatusCustomButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeStatusCustomButton.ForeColor = System.Drawing.Color.White;
            this.ChangeStatusCustomButton.Location = new System.Drawing.Point(627, 200);
            this.ChangeStatusCustomButton.Margin = new System.Windows.Forms.Padding(0);
            this.ChangeStatusCustomButton.MaximumSize = new System.Drawing.Size(90, 40);
            this.ChangeStatusCustomButton.MinimumSize = new System.Drawing.Size(90, 40);
            this.ChangeStatusCustomButton.Name = "ChangeStatusCustomButton";
            this.ChangeStatusCustomButton.Size = new System.Drawing.Size(90, 40);
            this.ChangeStatusCustomButton.TabIndex = 16;
            this.ChangeStatusCustomButton.Text = "Change Status/Reason";
            this.ChangeStatusCustomButton.UseVisualStyleBackColor = false;
            this.ChangeStatusCustomButton.Click += new System.EventHandler(this.ChangeStatusCustomButton_Click);
            // 
            // custSSN
            // 
            this.custSSN.CausesValidation = false;
            this.custSSN.Location = new System.Drawing.Point(145, 212);
            this.custSSN.Name = "custSSN";
            this.custSSN.Size = new System.Drawing.Size(123, 20);
            this.custSSN.TabIndex = 13;
            // 
            // custDateOfBirth
            // 
            this.custDateOfBirth.BackColor = System.Drawing.Color.Transparent;
            this.custDateOfBirth.CausesValidation = false;
            this.custDateOfBirth.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.custDateOfBirth.Location = new System.Drawing.Point(145, 290);
            this.custDateOfBirth.Name = "custDateOfBirth";
            this.custDateOfBirth.Required = true;
            this.custDateOfBirth.Size = new System.Drawing.Size(100, 20);
            this.custDateOfBirth.TabIndex = 8;
            this.custDateOfBirth.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.custDateOfBirth.ValidationExpression = "^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.](19|20)\\d\\d$";
            this.custDateOfBirth.Leave += new System.EventHandler(this.custDateOfBirth_Leave);
            // 
            // titleSuffix1
            // 
            this.titleSuffix1.BackColor = System.Drawing.Color.Transparent;
            this.titleSuffix1.Location = new System.Drawing.Point(145, 180);
            this.titleSuffix1.Name = "titleSuffix1";
            this.titleSuffix1.Size = new System.Drawing.Size(55, 26);
            this.titleSuffix1.TabIndex = 5;
            // 
            // customHistoryButton
            // 
            this.customHistoryButton.BackColor = System.Drawing.Color.Transparent;
            this.customHistoryButton.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.customHistoryButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.customHistoryButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customHistoryButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customHistoryButton.FlatAppearance.BorderSize = 0;
            this.customHistoryButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customHistoryButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customHistoryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customHistoryButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customHistoryButton.ForeColor = System.Drawing.Color.White;
            this.customHistoryButton.Location = new System.Drawing.Point(514, 535);
            this.customHistoryButton.Margin = new System.Windows.Forms.Padding(0);
            this.customHistoryButton.MaximumSize = new System.Drawing.Size(90, 40);
            this.customHistoryButton.MinimumSize = new System.Drawing.Size(90, 40);
            this.customHistoryButton.Name = "customHistoryButton";
            this.customHistoryButton.Size = new System.Drawing.Size(90, 40);
            this.customHistoryButton.TabIndex = 21;
            this.customHistoryButton.Text = "History";
            this.customHistoryButton.UseVisualStyleBackColor = false;
            this.customHistoryButton.Visible = false;
            this.customHistoryButton.Click += new System.EventHandler(this.customHistoryButton_Click);
            // 
            // customBackButton
            // 
            this.customBackButton.BackColor = System.Drawing.Color.Transparent;
            this.customBackButton.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.customBackButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.customBackButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customBackButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customBackButton.FlatAppearance.BorderSize = 0;
            this.customBackButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customBackButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customBackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customBackButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customBackButton.ForeColor = System.Drawing.Color.White;
            this.customBackButton.Location = new System.Drawing.Point(129, 535);
            this.customBackButton.Margin = new System.Windows.Forms.Padding(0);
            this.customBackButton.MaximumSize = new System.Drawing.Size(90, 40);
            this.customBackButton.MinimumSize = new System.Drawing.Size(90, 40);
            this.customBackButton.Name = "customBackButton";
            this.customBackButton.Size = new System.Drawing.Size(90, 40);
            this.customBackButton.TabIndex = 19;
            this.customBackButton.Text = "Back";
            this.customBackButton.UseVisualStyleBackColor = false;
            this.customBackButton.Click += new System.EventHandler(this.customBackButton_Click);
            // 
            // customButtonSubmit
            // 
            this.customButtonSubmit.BackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.customButtonSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.customButtonSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonSubmit.FlatAppearance.BorderSize = 0;
            this.customButtonSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonSubmit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonSubmit.ForeColor = System.Drawing.Color.White;
            this.customButtonSubmit.Location = new System.Drawing.Point(627, 535);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(90, 40);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(90, 40);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(90, 40);
            this.customButtonSubmit.TabIndex = 22;
            this.customButtonSubmit.Text = "&Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // customButtonReset
            // 
            this.customButtonReset.BackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.customButtonReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.customButtonReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonReset.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonReset.FlatAppearance.BorderSize = 0;
            this.customButtonReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonReset.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonReset.ForeColor = System.Drawing.Color.White;
            this.customButtonReset.Location = new System.Drawing.Point(401, 535);
            this.customButtonReset.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonReset.MaximumSize = new System.Drawing.Size(90, 40);
            this.customButtonReset.MinimumSize = new System.Drawing.Size(90, 40);
            this.customButtonReset.Name = "customButtonReset";
            this.customButtonReset.Size = new System.Drawing.Size(90, 40);
            this.customButtonReset.TabIndex = 20;
            this.customButtonReset.Text = "&Reset";
            this.customButtonReset.UseVisualStyleBackColor = false;
            this.customButtonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // customButtonCancel
            // 
            this.customButtonCancel.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.customButtonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.customButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCancel.FlatAppearance.BorderSize = 0;
            this.customButtonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCancel.ForeColor = System.Drawing.Color.White;
            this.customButtonCancel.Location = new System.Drawing.Point(22, 535);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(90, 40);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(90, 40);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(90, 40);
            this.customButtonCancel.TabIndex = 18;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // title1
            // 
            this.title1.BackColor = System.Drawing.Color.Transparent;
            this.title1.CausesValidation = false;
            this.title1.Location = new System.Drawing.Point(145, 81);
            this.title1.Name = "title1";
            this.title1.Size = new System.Drawing.Size(61, 21);
            this.title1.TabIndex = 1;
            // 
            // txtBoxLastVerDate
            // 
            this.txtBoxLastVerDate.BackColor = System.Drawing.Color.Transparent;
            this.txtBoxLastVerDate.CausesValidation = false;
            this.txtBoxLastVerDate.ErrorMessage = "Invalid Date entered.";
            this.txtBoxLastVerDate.Location = new System.Drawing.Point(503, 258);
            this.txtBoxLastVerDate.Name = "txtBoxLastVerDate";
            this.txtBoxLastVerDate.Size = new System.Drawing.Size(100, 21);
            this.txtBoxLastVerDate.TabIndex = 14;
            this.txtBoxLastVerDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBoxLastVerDate.ValidationExpression = "^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.](19|20)\\d\\d$";
            // 
            // txtBoxNextVerDate
            // 
            this.txtBoxNextVerDate.BackColor = System.Drawing.Color.Transparent;
            this.txtBoxNextVerDate.CausesValidation = false;
            this.txtBoxNextVerDate.ErrorMessage = "Invalid Date entered.";
            this.txtBoxNextVerDate.Location = new System.Drawing.Point(503, 284);
            this.txtBoxNextVerDate.Name = "txtBoxNextVerDate";
            this.txtBoxNextVerDate.Size = new System.Drawing.Size(100, 21);
            this.txtBoxNextVerDate.TabIndex = 15;
            this.txtBoxNextVerDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBoxNextVerDate.ValidationExpression = "^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.](19|20)\\d\\d$";
            // 
            // UpdateCustomerDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.BackgroundImage = global::Support.Properties.Resources.form_800_600;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(769, 589);
            this.ControlBox = false;
            this.Controls.Add(this.txtBoxMarStatus);
            this.Controls.Add(this.BtnCustomerComments);
            this.Controls.Add(this.ChangeStatusCustomButton);
            this.Controls.Add(this.PRBCcheckBox);
            this.Controls.Add(this.SpanishFormcheckBox1);
            this.Controls.Add(this.OptOutFlagcheckBox);
            this.Controls.Add(this.MilitaryStationedLocalcheckBox);
            this.Controls.Add(this.BankruptcyProtectionradioButton1);
            this.Controls.Add(this.custSSN);
            this.Controls.Add(this.labelSSN);
            this.Controls.Add(this.labelDOB);
            this.Controls.Add(this.custDateOfBirth);
            this.Controls.Add(this.customLabelNegotiationLanguage);
            this.Controls.Add(this.labelTitleSuffix);
            this.Controls.Add(this.comboBoxLanguage);
            this.Controls.Add(this.customLabelLastName);
            this.Controls.Add(this.labelMiddleInitial);
            this.Controls.Add(this.titleSuffix1);
            this.Controls.Add(this.customLabelFirstName);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.custLastName);
            this.Controls.Add(this.customHistoryButton);
            this.Controls.Add(this.customBackButton);
            this.Controls.Add(this.custMiddleInitial);
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonReset);
            this.Controls.Add(this.custFirstName);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.title1);
            this.Controls.Add(this.labelHeadingInfo);
            this.Controls.Add(this.labelUpdateCustDetailsHeading);
            this.Controls.Add(this.lableMarStatus);
            this.Controls.Add(this.lableSpouseFName);
            this.Controls.Add(this.lableSpouseLName);
            this.Controls.Add(this.lableSpouseSSN);
            this.Controls.Add(this.lableHowLongAtAddress);
            this.Controls.Add(this.lableHLAAYears);
            this.Controls.Add(this.lableHLAAMonths);
            this.Controls.Add(this.lableOwnHome);
            this.Controls.Add(this.lableMonthlyRent);
            this.Controls.Add(this.lableMilitaryStationedLocal);
            this.Controls.Add(this.lableCustSeqNumber);
            this.Controls.Add(this.lablePrivacyNotifDate);
            this.Controls.Add(this.lableOptOutFlag);
            this.Controls.Add(this.lableCustStatus);
            this.Controls.Add(this.lableReasonCode);
            this.Controls.Add(this.lableLastVerDate);
            this.Controls.Add(this.lableNextVerDate);
            this.Controls.Add(this.lablePDLCoolingOffDate);
            this.Controls.Add(this.lablePDLCustSince);
            this.Controls.Add(this.lableSpanishForm);
            this.Controls.Add(this.lablePRBC);
            this.Controls.Add(this.lableBankruptcyProtection);
            this.Controls.Add(this.txtBoxSpouseFName);
            this.Controls.Add(this.txtBoxSpouseLName);
            this.Controls.Add(this.txtBoxSpouseSSN);
            this.Controls.Add(this.txtBoxHLAAYears);
            this.Controls.Add(this.txtBoxHLAAMonths);
            this.Controls.Add(this.txtBoxOwnHome);
            this.Controls.Add(this.txtBoxMonthlyRent);
            this.Controls.Add(this.txtBoxCustSeqNumber);
            this.Controls.Add(this.txtBoxPrivacyNotifDate);
            this.Controls.Add(this.txtBoxCustStatus);
            this.Controls.Add(this.txtBoxReasonCode);
            this.Controls.Add(this.txtBoxLastVerDate);
            this.Controls.Add(this.txtBoxNextVerDate);
            this.Controls.Add(this.txtBoxPDLCoolingOffDate);
            this.Controls.Add(this.txtBoxPDLCustSince);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateCustomerDetails";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateCustomerDetails";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdateCustomerDetails_FormClosing);
            this.Load += new System.EventHandler(this.UpdateCustomerDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUpdateCustDetailsHeading;
        private System.Windows.Forms.Label labelHeadingInfo;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelMiddleInitial;
        private System.Windows.Forms.Label labelTitleSuffix;
        private System.Windows.Forms.Label labelSSN;
        private Title title1;
        private CustomTextBox custFirstName;
        private CustomTextBox custMiddleInitial;
        private CustomTextBox custLastName;
        private TitleSuffix titleSuffix1;
        private Date custDateOfBirth;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private CustomLabel customLabelFirstName;
        private CustomLabel customLabelLastName;
        private CustomLabel customLabelNegotiationLanguage;
        private CustomLabel labelDOB;
        private SocialSecurityNumber custSSN;
        private SupportButton customButtonCancel;
        private SupportButton customButtonReset;
        private SupportButton customButtonSubmit;
        private SupportButton customBackButton;
        private SupportButton customHistoryButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lableMarStatus;
        private System.Windows.Forms.Label lableSpouseFName;
        private System.Windows.Forms.Label lableSpouseLName;
        private System.Windows.Forms.Label lableSpouseSSN;
        private System.Windows.Forms.Label lableHowLongAtAddress;

        private System.Windows.Forms.Label lableHLAAYears              ;
private System.Windows.Forms.Label lableHLAAMonths             ;
private System.Windows.Forms.Label lableOwnHome                ;
private System.Windows.Forms.Label lableMonthlyRent            ;
private System.Windows.Forms.Label lableMilitaryStationedLocal ;
private System.Windows.Forms.Label lableCustSeqNumber          ;
private System.Windows.Forms.Label lablePrivacyNotifDate     	 ;
private System.Windows.Forms.Label lableOptOutFlag             ;
private System.Windows.Forms.Label lableCustStatus             ;
private System.Windows.Forms.Label lableReasonCode             ;
private System.Windows.Forms.Label lableLastVerDate        		 ;
private System.Windows.Forms.Label lableNextVerDate        		 ;
private System.Windows.Forms.Label lablePDLCoolingOffDate      ;
private System.Windows.Forms.Label lablePDLCustSince           ;
private System.Windows.Forms.Label lableSpanishForm            ;
private System.Windows.Forms.Label lablePRBC                   ;
private System.Windows.Forms.Label lableBankruptcyProtection;
        private CustomTextBox txtBoxSpouseFName;
        private CustomTextBox txtBoxSpouseLName;
        private CustomTextBox txtBoxSpouseSSN;
        private CustomTextBox txtBoxHLAAYears                           ;

        private CustomTextBox txtBoxHLAAMonths                           ;
private CustomTextBox txtBoxOwnHome                              ;
private CustomTextBox txtBoxMonthlyRent;
private CustomTextBox txtBoxCustSeqNumber                        ;
private CustomTextBox txtBoxPrivacyNotifDate;
private CustomTextBox txtBoxCustStatus                           ;
private CustomTextBox txtBoxReasonCode                           ;
//private CustomTextBox txtBoxLastVerDate                          ;
//private CustomTextBox txtBoxNextVerDate                          ;
private Date txtBoxLastVerDate;
private Date txtBoxNextVerDate;
private CustomTextBox txtBoxPDLCoolingOffDate                    ;
private CustomTextBox txtBoxPDLCustSince;
private System.Windows.Forms.RadioButton BankruptcyProtectionradioButton1;
private System.Windows.Forms.CheckBox MilitaryStationedLocalcheckBox;
private System.Windows.Forms.CheckBox OptOutFlagcheckBox;
private System.Windows.Forms.CheckBox SpanishFormcheckBox1;
private System.Windows.Forms.CheckBox PRBCcheckBox;
private SupportButton ChangeStatusCustomButton;
private SupportButton BtnCustomerComments;
private System.Windows.Forms.ComboBox txtBoxMarStatus;
    }
}