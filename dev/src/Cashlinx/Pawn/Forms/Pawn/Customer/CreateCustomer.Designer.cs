using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.Customer
{
    partial class CreateCustomer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateCustomer));
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.customLabelTitle = new CustomLabel();
            this.customLabelFirstName = new CustomLabel();
            this.customLabelMiddleInitial = new CustomLabel();
            this.customLabelLastName = new CustomLabel();
            this.customLabelTitleSuffix = new CustomLabel();
            this.pwnapp_firstName = new CustomTextBox();
            this.pwnapp_middleInitial = new CustomTextBox();
            this.pwnapp_lastName = new CustomTextBox();
            this.pwnapp_titleSuffix = new TitleSuffix();
            this.customLabelDateOfBirth = new CustomLabel();
            this.customLabelSSN = new CustomLabel();
            this.pwnapp_dateOfBirth = new Date();
            this.pwnapp_title = new Title();
            this.pwnapp_socialsecuritynumber = new SocialSecurityNumber();
            this.custInfoPanel = new System.Windows.Forms.Panel();
            this.idPanel = new System.Windows.Forms.Panel();
            this.customLabelGovernmentIDHeading = new CustomLabel();
            this.identification1 = new Identification();
            this.panel1 = new System.Windows.Forms.Panel();
            this.phoneData1 = new PhoneData();
            this.primaryEmailTextBox = new CustomTextBox();
            this.labelPrimaryEmail = new System.Windows.Forms.Label();
            this.labelContactInfoHeading = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comments = new System.Windows.Forms.RichTextBox();
            this.labelComments = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.labelHowdidyouhear = new System.Windows.Forms.Label();
            this.labelReceivePromotionOffers = new System.Windows.Forms.Label();
            this.comboBoxReceivePromotions = new System.Windows.Forms.ComboBox();
            this.hearAboutUs1 = new HearAboutUs();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.customButtonContinue = new CustomButton();
            this.customButtonSave = new CustomButton();
            this.customButtonClear = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.idPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(270, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer Details";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.67449F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.32552F));
            this.tableLayoutPanel1.Controls.Add(this.customLabelTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelFirstName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelMiddleInitial, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.customLabelLastName, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.customLabelTitleSuffix, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_firstName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_middleInitial, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_lastName, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_titleSuffix, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.customLabelDateOfBirth, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.customLabelSSN, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_dateOfBirth, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_title, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_socialsecuritynumber, 1, 6);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 104);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(682, 193);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // customLabelTitle
            // 
            this.customLabelTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelTitle.AutoSize = true;
            this.customLabelTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelTitle.ForeColor = System.Drawing.Color.Black;
            this.customLabelTitle.Location = new System.Drawing.Point(3, 6);
            this.customLabelTitle.Name = "customLabelTitle";
            this.customLabelTitle.Size = new System.Drawing.Size(27, 13);
            this.customLabelTitle.TabIndex = 0;
            this.customLabelTitle.Text = "Title";
            // 
            // customLabelFirstName
            // 
            this.customLabelFirstName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelFirstName.AutoSize = true;
            this.customLabelFirstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelFirstName.ForeColor = System.Drawing.Color.Black;
            this.customLabelFirstName.Location = new System.Drawing.Point(3, 33);
            this.customLabelFirstName.Name = "customLabelFirstName";
            this.customLabelFirstName.Required = true;
            this.customLabelFirstName.Size = new System.Drawing.Size(58, 13);
            this.customLabelFirstName.TabIndex = 1;
            this.customLabelFirstName.Text = "First Name";
            // 
            // customLabelMiddleInitial
            // 
            this.customLabelMiddleInitial.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelMiddleInitial.AutoSize = true;
            this.customLabelMiddleInitial.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelMiddleInitial.ForeColor = System.Drawing.Color.Black;
            this.customLabelMiddleInitial.Location = new System.Drawing.Point(3, 60);
            this.customLabelMiddleInitial.Name = "customLabelMiddleInitial";
            this.customLabelMiddleInitial.Size = new System.Drawing.Size(66, 13);
            this.customLabelMiddleInitial.TabIndex = 2;
            this.customLabelMiddleInitial.Text = "Middle Initial";
            // 
            // customLabelLastName
            // 
            this.customLabelLastName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelLastName.AutoSize = true;
            this.customLabelLastName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelLastName.ForeColor = System.Drawing.Color.Black;
            this.customLabelLastName.Location = new System.Drawing.Point(3, 87);
            this.customLabelLastName.Name = "customLabelLastName";
            this.customLabelLastName.Required = true;
            this.customLabelLastName.Size = new System.Drawing.Size(57, 13);
            this.customLabelLastName.TabIndex = 3;
            this.customLabelLastName.Text = "Last Name";
            // 
            // customLabelTitleSuffix
            // 
            this.customLabelTitleSuffix.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelTitleSuffix.AutoSize = true;
            this.customLabelTitleSuffix.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelTitleSuffix.ForeColor = System.Drawing.Color.Black;
            this.customLabelTitleSuffix.Location = new System.Drawing.Point(3, 116);
            this.customLabelTitleSuffix.Name = "customLabelTitleSuffix";
            this.customLabelTitleSuffix.Size = new System.Drawing.Size(58, 13);
            this.customLabelTitleSuffix.TabIndex = 4;
            this.customLabelTitleSuffix.Text = "Title Suffix";
            // 
            // pwnapp_firstName
            // 
            this.pwnapp_firstName.CausesValidation = false;
            this.pwnapp_firstName.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_firstName.FirstLetterUppercase = true;
            this.pwnapp_firstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_firstName.Location = new System.Drawing.Point(144, 29);
            this.pwnapp_firstName.MaxLength = 40;
            this.pwnapp_firstName.Name = "pwnapp_firstName";
            this.pwnapp_firstName.Required = true;
            this.pwnapp_firstName.Size = new System.Drawing.Size(183, 21);
            this.pwnapp_firstName.TabIndex = 2;
            this.pwnapp_firstName.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // pwnapp_middleInitial
            // 
            this.pwnapp_middleInitial.CausesValidation = false;
            this.pwnapp_middleInitial.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_middleInitial.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_middleInitial.Location = new System.Drawing.Point(144, 56);
            this.pwnapp_middleInitial.MaxLength = 40;
            this.pwnapp_middleInitial.Name = "pwnapp_middleInitial";
            this.pwnapp_middleInitial.Size = new System.Drawing.Size(183, 21);
            this.pwnapp_middleInitial.TabIndex = 3;
            this.pwnapp_middleInitial.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // pwnapp_lastName
            // 
            this.pwnapp_lastName.CausesValidation = false;
            this.pwnapp_lastName.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_lastName.FirstLetterUppercase = true;
            this.pwnapp_lastName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_lastName.Location = new System.Drawing.Point(144, 83);
            this.pwnapp_lastName.MaxLength = 40;
            this.pwnapp_lastName.Name = "pwnapp_lastName";
            this.pwnapp_lastName.Required = true;
            this.pwnapp_lastName.Size = new System.Drawing.Size(183, 21);
            this.pwnapp_lastName.TabIndex = 4;
            this.pwnapp_lastName.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // pwnapp_titleSuffix
            // 
            this.pwnapp_titleSuffix.Location = new System.Drawing.Point(144, 110);
            this.pwnapp_titleSuffix.Name = "pwnapp_titleSuffix";
            this.pwnapp_titleSuffix.Size = new System.Drawing.Size(55, 26);
            this.pwnapp_titleSuffix.TabIndex = 5;
            // 
            // customLabelDateOfBirth
            // 
            this.customLabelDateOfBirth.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelDateOfBirth.AutoSize = true;
            this.customLabelDateOfBirth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelDateOfBirth.ForeColor = System.Drawing.Color.Black;
            this.customLabelDateOfBirth.Location = new System.Drawing.Point(3, 145);
            this.customLabelDateOfBirth.Name = "customLabelDateOfBirth";
            this.customLabelDateOfBirth.Required = true;
            this.customLabelDateOfBirth.Size = new System.Drawing.Size(70, 13);
            this.customLabelDateOfBirth.TabIndex = 6;
            this.customLabelDateOfBirth.Text = "Date Of Birth";
            // 
            // customLabelSSN
            // 
            this.customLabelSSN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelSSN.AutoSize = true;
            this.customLabelSSN.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelSSN.ForeColor = System.Drawing.Color.Black;
            this.customLabelSSN.Location = new System.Drawing.Point(3, 172);
            this.customLabelSSN.Name = "customLabelSSN";
            this.customLabelSSN.Size = new System.Drawing.Size(116, 13);
            this.customLabelSSN.TabIndex = 7;
            this.customLabelSSN.Text = "Social Security Number";
            // 
            // pwnapp_dateOfBirth
            // 
            this.pwnapp_dateOfBirth.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_dateOfBirth.Location = new System.Drawing.Point(144, 142);
            this.pwnapp_dateOfBirth.Name = "pwnapp_dateOfBirth";
            this.pwnapp_dateOfBirth.Size = new System.Drawing.Size(100, 20);
            this.pwnapp_dateOfBirth.TabIndex = 6;
            this.pwnapp_dateOfBirth.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.pwnapp_dateOfBirth.ValidationExpression = "^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.]19[0-9][0-9]|20[0" +
                "-9][0-9]$";
            this.pwnapp_dateOfBirth.Leave += new System.EventHandler(this.pwnapp_dateOfBirth_Leave);
            // 
            // pwnapp_title
            // 
            this.pwnapp_title.CausesValidation = false;
            this.pwnapp_title.Location = new System.Drawing.Point(144, 3);
            this.pwnapp_title.Name = "pwnapp_title";
            this.pwnapp_title.Size = new System.Drawing.Size(61, 20);
            this.pwnapp_title.TabIndex = 1;
            // 
            // pwnapp_socialsecuritynumber
            // 
            this.pwnapp_socialsecuritynumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_socialsecuritynumber.AutoSize = true;
            this.pwnapp_socialsecuritynumber.CausesValidation = false;
            this.pwnapp_socialsecuritynumber.Location = new System.Drawing.Point(144, 169);
            this.pwnapp_socialsecuritynumber.Name = "pwnapp_socialsecuritynumber";
            this.pwnapp_socialsecuritynumber.Size = new System.Drawing.Size(123, 20);
            this.pwnapp_socialsecuritynumber.TabIndex = 7;
            // 
            // custInfoPanel
            // 
            this.custInfoPanel.BackColor = System.Drawing.Color.Transparent;
            this.custInfoPanel.Location = new System.Drawing.Point(12, 106);
            this.custInfoPanel.Name = "custInfoPanel";
            this.custInfoPanel.Size = new System.Drawing.Size(685, 187);
            this.custInfoPanel.TabIndex = 2;
            // 
            // idPanel
            // 
            this.idPanel.BackColor = System.Drawing.Color.Transparent;
            this.idPanel.Controls.Add(this.customLabelGovernmentIDHeading);
            this.idPanel.Controls.Add(this.identification1);
            this.idPanel.Location = new System.Drawing.Point(12, 298);
            this.idPanel.Name = "idPanel";
            this.idPanel.Size = new System.Drawing.Size(685, 129);
            this.idPanel.TabIndex = 8;
            this.idPanel.TabStop = true;
            // 
            // customLabelGovernmentIDHeading
            // 
            this.customLabelGovernmentIDHeading.AutoSize = true;
            this.customLabelGovernmentIDHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelGovernmentIDHeading.ForeColor = System.Drawing.Color.Black;
            this.customLabelGovernmentIDHeading.Location = new System.Drawing.Point(4, 4);
            this.customLabelGovernmentIDHeading.Name = "customLabelGovernmentIDHeading";
            this.customLabelGovernmentIDHeading.Size = new System.Drawing.Size(132, 13);
            this.customLabelGovernmentIDHeading.TabIndex = 9;
            this.customLabelGovernmentIDHeading.Text = "Government Identification";
            // 
            // identification1
            // 
            this.identification1.AutoSize = true;
            this.identification1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.identification1.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.identification1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.identification1.CausesValidation = false;
            this.identification1.Location = new System.Drawing.Point(140, 20);
            this.identification1.Name = "identification1";
            this.identification1.Size = new System.Drawing.Size(531, 104);
            this.identification1.TabIndex = 8;
            this.identification1.Leave += new System.EventHandler(this.identification1_Leave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.phoneData1);
            this.panel1.Controls.Add(this.primaryEmailTextBox);
            this.panel1.Controls.Add(this.labelPrimaryEmail);
            this.panel1.Controls.Add(this.labelContactInfoHeading);
            this.panel1.Location = new System.Drawing.Point(12, 429);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(685, 167);
            this.panel1.TabIndex = 9;
            this.panel1.TabStop = true;
            // 
            // phoneData1
            // 
            this.phoneData1.BackColor = System.Drawing.Color.Transparent;
            this.phoneData1.CausesValidation = false;
            this.phoneData1.ForeColor = System.Drawing.Color.Black;
            this.phoneData1.Location = new System.Drawing.Point(3, 26);
            this.phoneData1.Name = "phoneData1";
            this.phoneData1.ShowFax = false;
            this.phoneData1.ShowHeading = true;
            this.phoneData1.ShowPager = false;
            this.phoneData1.Size = new System.Drawing.Size(671, 112);
            this.phoneData1.TabCountryCode = false;
            this.phoneData1.TabExtension = false;
            this.phoneData1.TabIndex = 9;
            // 
            // primaryEmailTextBox
            // 
            this.primaryEmailTextBox.CausesValidation = false;
            this.primaryEmailTextBox.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.primaryEmailTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.primaryEmailTextBox.Location = new System.Drawing.Point(116, 141);
            this.primaryEmailTextBox.MaxLength = 255;
            this.primaryEmailTextBox.Name = "primaryEmailTextBox";
            this.primaryEmailTextBox.RegularExpression = true;
            this.primaryEmailTextBox.Size = new System.Drawing.Size(188, 21);
            this.primaryEmailTextBox.TabIndex = 10;
            this.primaryEmailTextBox.ValidationExpression = "(\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,6})";
            // 
            // labelPrimaryEmail
            // 
            this.labelPrimaryEmail.AutoSize = true;
            this.labelPrimaryEmail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrimaryEmail.ForeColor = System.Drawing.Color.Black;
            this.labelPrimaryEmail.Location = new System.Drawing.Point(7, 141);
            this.labelPrimaryEmail.Name = "labelPrimaryEmail";
            this.labelPrimaryEmail.Size = new System.Drawing.Size(70, 13);
            this.labelPrimaryEmail.TabIndex = 3;
            this.labelPrimaryEmail.Text = "Primary Email";
            // 
            // labelContactInfoHeading
            // 
            this.labelContactInfoHeading.AutoSize = true;
            this.labelContactInfoHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelContactInfoHeading.ForeColor = System.Drawing.Color.Black;
            this.labelContactInfoHeading.Location = new System.Drawing.Point(5, 6);
            this.labelContactInfoHeading.Name = "labelContactInfoHeading";
            this.labelContactInfoHeading.Size = new System.Drawing.Size(139, 16);
            this.labelContactInfoHeading.TabIndex = 0;
            this.labelContactInfoHeading.Text = "Contact Information";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.comments);
            this.panel2.Controls.Add(this.labelComments);
            this.panel2.Location = new System.Drawing.Point(12, 599);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(685, 67);
            this.panel2.TabIndex = 10;
            this.panel2.TabStop = true;
            // 
            // comments
            // 
            this.comments.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comments.Location = new System.Drawing.Point(8, 21);
            this.comments.Name = "comments";
            this.comments.Size = new System.Drawing.Size(586, 41);
            this.comments.TabIndex = 11;
            this.comments.Text = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // labelComments
            // 
            this.labelComments.AutoSize = true;
            this.labelComments.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComments.ForeColor = System.Drawing.Color.Black;
            this.labelComments.Location = new System.Drawing.Point(7, 3);
            this.labelComments.Name = "labelComments";
            this.labelComments.Size = new System.Drawing.Size(128, 16);
            this.labelComments.TabIndex = 0;
            this.labelComments.Text = "Comments / Notes";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.tableLayoutPanel3);
            this.panel3.Location = new System.Drawing.Point(12, 668);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(685, 64);
            this.panel3.TabIndex = 11;
            this.panel3.TabStop = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.labelHowdidyouhear, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelReceivePromotionOffers, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.comboBoxReceivePromotions, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.hearAboutUs1, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(328, 57);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // labelHowdidyouhear
            // 
            this.labelHowdidyouhear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelHowdidyouhear.AutoSize = true;
            this.labelHowdidyouhear.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHowdidyouhear.ForeColor = System.Drawing.Color.Black;
            this.labelHowdidyouhear.Location = new System.Drawing.Point(3, 7);
            this.labelHowdidyouhear.Name = "labelHowdidyouhear";
            this.labelHowdidyouhear.Size = new System.Drawing.Size(141, 13);
            this.labelHowdidyouhear.TabIndex = 0;
            this.labelHowdidyouhear.Text = "How did you hear about us?";
            // 
            // labelReceivePromotionOffers
            // 
            this.labelReceivePromotionOffers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelReceivePromotionOffers.AutoSize = true;
            this.labelReceivePromotionOffers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReceivePromotionOffers.ForeColor = System.Drawing.Color.Black;
            this.labelReceivePromotionOffers.Location = new System.Drawing.Point(3, 36);
            this.labelReceivePromotionOffers.Name = "labelReceivePromotionOffers";
            this.labelReceivePromotionOffers.Size = new System.Drawing.Size(138, 13);
            this.labelReceivePromotionOffers.TabIndex = 1;
            this.labelReceivePromotionOffers.Text = "Receive Promotional Offers";
            // 
            // comboBoxReceivePromotions
            // 
            this.comboBoxReceivePromotions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReceivePromotions.FormattingEnabled = true;
            this.comboBoxReceivePromotions.Location = new System.Drawing.Point(167, 31);
            this.comboBoxReceivePromotions.Name = "comboBoxReceivePromotions";
            this.comboBoxReceivePromotions.Size = new System.Drawing.Size(100, 21);
            this.comboBoxReceivePromotions.TabIndex = 13;
            // 
            // hearAboutUs1
            // 
            this.hearAboutUs1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.hearAboutUs1.Location = new System.Drawing.Point(167, 3);
            this.hearAboutUs1.Name = "hearAboutUs1";
            this.hearAboutUs1.Size = new System.Drawing.Size(121, 22);
            this.hearAboutUs1.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.RoyalBlue;
            this.groupBox1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.groupBox1.Location = new System.Drawing.Point(3, 737);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(701, 2);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            // 
            // customButtonContinue
            // 
            this.customButtonContinue.BackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonContinue.BackgroundImage")));
            this.customButtonContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonContinue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonContinue.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonContinue.FlatAppearance.BorderSize = 0;
            this.customButtonContinue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonContinue.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonContinue.ForeColor = System.Drawing.Color.White;
            this.customButtonContinue.Location = new System.Drawing.Point(563, 742);
            this.customButtonContinue.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.Name = "customButtonContinue";
            this.customButtonContinue.Size = new System.Drawing.Size(100, 50);
            this.customButtonContinue.TabIndex = 50;
            this.customButtonContinue.Text = "Co&ntinue";
            this.customButtonContinue.UseVisualStyleBackColor = false;
            this.customButtonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // customButtonSave
            // 
            this.customButtonSave.BackColor = System.Drawing.Color.Transparent;
            this.customButtonSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonSave.BackgroundImage")));
            this.customButtonSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonSave.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonSave.FlatAppearance.BorderSize = 0;
            this.customButtonSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonSave.ForeColor = System.Drawing.Color.White;
            this.customButtonSave.Location = new System.Drawing.Point(463, 742);
            this.customButtonSave.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSave.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSave.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSave.Name = "customButtonSave";
            this.customButtonSave.Size = new System.Drawing.Size(100, 50);
            this.customButtonSave.TabIndex = 49;
            this.customButtonSave.Text = "&Save";
            this.customButtonSave.UseVisualStyleBackColor = false;
            this.customButtonSave.Click += new System.EventHandler(this.buttonSave_Click);
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
            this.customButtonClear.Location = new System.Drawing.Point(363, 742);
            this.customButtonClear.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonClear.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonClear.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonClear.Name = "customButtonClear";
            this.customButtonClear.Size = new System.Drawing.Size(100, 50);
            this.customButtonClear.TabIndex = 48;
            this.customButtonClear.Text = "C&lear";
            this.customButtonClear.UseVisualStyleBackColor = false;
            this.customButtonClear.Click += new System.EventHandler(this.buttonClear_Click);
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
            this.customButtonCancel.Location = new System.Drawing.Point(16, 741);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 47;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.createCustomerCancelButton_Click);
            // 
            // CreateCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = Common.Properties.Resources.newDialog_480_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(709, 800);
            this.ControlBox = false;
            this.Controls.Add(this.customButtonContinue);
            this.Controls.Add(this.customButtonSave);
            this.Controls.Add(this.customButtonClear);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.idPanel);
            this.Controls.Add(this.custInfoPanel);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateCustomer";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Customer";
            this.Load += new System.EventHandler(this.CreateCustomer_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.idPanel.ResumeLayout(false);
            this.idPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomLabel customLabelTitle;
        private CustomLabel customLabelFirstName;
        private CustomLabel customLabelMiddleInitial;
        private CustomLabel customLabelLastName;
        private CustomLabel customLabelTitleSuffix;
        private CustomLabel customLabelDateOfBirth;
        private CustomLabel customLabelSSN;
        private System.Windows.Forms.Panel custInfoPanel;
        private System.Windows.Forms.Panel idPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelContactInfoHeading;
        private System.Windows.Forms.Label labelPrimaryEmail;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox comments;
        private System.Windows.Forms.Label labelComments;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label labelHowdidyouhear;
        private System.Windows.Forms.Label labelReceivePromotionOffers;
        private Title pwnapp_title;
        private CustomTextBox pwnapp_firstName;
        private CustomTextBox pwnapp_middleInitial;
        private CustomTextBox pwnapp_lastName;
        private TitleSuffix pwnapp_titleSuffix;
        private Date pwnapp_dateOfBirth;
        private CustomTextBox primaryEmailTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxReceivePromotions;
        private HearAboutUs hearAboutUs1;
        private SocialSecurityNumber pwnapp_socialsecuritynumber;
        private Identification identification1;
        private PhoneData phoneData1;
        private CustomLabel customLabelGovernmentIDHeading;
        private CustomButton customButtonCancel;
        private CustomButton customButtonClear;
        private CustomButton customButtonSave;
        private CustomButton customButtonContinue;
    }
}
