using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.Customer
{
    partial class ManagePawnApplication
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagePawnApplication));
            this.managePawnAppLabel = new System.Windows.Forms.Label();
            this.labelPersonalInfoHeading = new System.Windows.Forms.Label();
            this.personalInfoPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ageLabel = new System.Windows.Forms.Label();
            this.ageTextbox = new System.Windows.Forms.TextBox();
            this.pwnapp_firstname = new CustomTextBox();
            this.pwnapp_lastname = new CustomTextBox();
            this.pwnapp_address2 = new CustomTextBox();
            this.pwnapp_city = new CustomTextBox();
            this.pwnapp_middleinitial_label = new System.Windows.Forms.Label();
            this.pwnapp_state = new UserControls.State();
            this.pwnapp_middleinitial = new CustomTextBox();
            this.pwnapp_title = new UserControls.Title();
            this.pwnapp_unit = new CustomTextBox();
            this.pwnapp_dateofbirth = new UserControls.Date();
            this.pwnapp_firstname_label = new CustomLabel();
            this.pwnapp_lastname_label = new CustomLabel();
            this.pwnapp_address = new CustomTextBox();
            this.pwnapp_address_label = new CustomLabel();
            this.pwnapp_city_label = new CustomLabel();
            this.pwnapp_zip_label = new CustomLabel();
            this.pwnapp_dateofbirth_label = new CustomLabel();
            this.pwnapp_state_label = new CustomLabel();
            this.pwnapp_title_label = new CustomLabel();
            this.pwnapp_titlesuffix_label = new CustomLabel();
            this.pwnapp_unit_label = new CustomLabel();
            this.pwnapp_zip = new UserControls.Zipcode();
            this.pwnapp_titlesuffix = new UserControls.TitleSuffix();
            this.custCommentAlert = new System.Windows.Forms.LinkLabel();
            this.personalIdentPanel = new System.Windows.Forms.Panel();
            this.labelPersonalIDHeading = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pwnapp_identificationtype = new UserControls.IDType();
            this.pwnapp_identificationnumber = new CustomTextBox();
            this.pwnapp_identificationtype_label = new CustomLabel();
            this.pwnapp_identificationnumber_label = new CustomLabel();
            this.pwnapp_identificationstate_label = new CustomLabel();
            this.pwnapp_identificationexpirationdate = new UserControls.Date();
            this.pwnapp_identificationcountry = new UserControls.Country();
            this.pwnapp_identificationexpirationdate_label = new CustomLabel();
            this.physicalDescriptionPanel = new System.Windows.Forms.Panel();
            this.labelPhysicalDescHeading = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.inchesLabel = new System.Windows.Forms.Label();
            this.lbsLabel = new System.Windows.Forms.Label();
            this.pwnapp_height = new CustomTextBox();
            this.pwnapp_heightinches = new CustomTextBox();
            this.pwnapp_weight = new CustomTextBox();
            this.ftLabel = new System.Windows.Forms.Label();
            this.pwnapp_height_label = new CustomLabel();
            this.pwnapp_socialsecuritynumber_label = new CustomLabel();
            this.pwnapp_weight_label = new CustomLabel();
            this.pwnapp_socialsecuritynumber = new UserControls.SocialSecurityNumber();
            this.phoneNumPanel = new System.Windows.Forms.Panel();
            this.labelPhoneNumbersHeading = new System.Windows.Forms.Label();
            this.notesPanel = new System.Windows.Forms.Panel();
            this.pwnapp_comments_label = new CustomLabel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.pwnapp_race = new UserControls.Race();
            this.pwnapp_eyes = new UserControls.EyeColor();
            this.pwnapp_hair = new UserControls.Haircolor();
            this.pwnapp_sex = new UserControls.Gender();
            this.pwnapp_race_label = new CustomLabel();
            this.pwnapp_sex_label = new CustomLabel();
            this.pwnapp_eyes_label = new CustomLabel();
            this.pwnapp_hair_label = new CustomLabel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.phoneData1 = new UserControls.PhoneData();
            this.panelNotes = new System.Windows.Forms.Panel();
            this.notesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.customButtonSubmit = new CustomButton();
            this.customButtonReset = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.customLabel1 = new CustomLabel();
            this.pwnapp_identificationstate = new UserControls.State();
            this.personalInfoPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.personalIdentPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.physicalDescriptionPanel.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.phoneNumPanel.SuspendLayout();
            this.notesPanel.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelNotes.SuspendLayout();
            this.SuspendLayout();
            // 
            // managePawnAppLabel
            // 
            this.managePawnAppLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.managePawnAppLabel.BackColor = System.Drawing.Color.Transparent;
            this.managePawnAppLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.managePawnAppLabel.ForeColor = System.Drawing.Color.White;
            this.managePawnAppLabel.Location = new System.Drawing.Point(283, 20);
            this.managePawnAppLabel.Name = "managePawnAppLabel";
            this.managePawnAppLabel.Size = new System.Drawing.Size(285, 20);
            this.managePawnAppLabel.TabIndex = 0;
            this.managePawnAppLabel.Text = "Pawn Customer Information";
            // 
            // labelPersonalInfoHeading
            // 
            this.labelPersonalInfoHeading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelPersonalInfoHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPersonalInfoHeading.ForeColor = System.Drawing.Color.White;
            this.labelPersonalInfoHeading.Location = new System.Drawing.Point(5, 0);
            this.labelPersonalInfoHeading.Name = "labelPersonalInfoHeading";
            this.labelPersonalInfoHeading.Size = new System.Drawing.Size(150, 16);
            this.labelPersonalInfoHeading.TabIndex = 0;
            this.labelPersonalInfoHeading.Text = "Personal Information";
            // 
            // personalInfoPanel
            // 
            this.personalInfoPanel.AutoSize = true;
            this.personalInfoPanel.BackColor = System.Drawing.Color.MediumBlue;
            this.personalInfoPanel.Controls.Add(this.labelPersonalInfoHeading);
            this.personalInfoPanel.ForeColor = System.Drawing.Color.Black;
            this.personalInfoPanel.Location = new System.Drawing.Point(7, 67);
            this.personalInfoPanel.Name = "personalInfoPanel";
            this.personalInfoPanel.Size = new System.Drawing.Size(799, 23);
            this.personalInfoPanel.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.CausesValidation = false;
            this.tableLayoutPanel1.ColumnCount = 11;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.ageLabel, 6, 3);
            this.tableLayoutPanel1.Controls.Add(this.ageTextbox, 8, 3);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_firstname, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_lastname, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_address2, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_city, 8, 2);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_middleinitial_label, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_state, 10, 2);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_middleinitial, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_title, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_unit, 10, 0);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_dateofbirth, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_firstname_label, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_lastname_label, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_address, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_address_label, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_city_label, 6, 2);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_zip_label, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_dateofbirth_label, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_state_label, 9, 2);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_title_label, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_titlesuffix_label, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_unit_label, 9, 0);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_zip, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_titlesuffix, 3, 3);
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 92);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(797, 119);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // ageLabel
            // 
            this.ageLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ageLabel.Location = new System.Drawing.Point(519, 96);
            this.ageLabel.Name = "ageLabel";
            this.ageLabel.Size = new System.Drawing.Size(26, 13);
            this.ageLabel.TabIndex = 22;
            this.ageLabel.Text = "Age";
            // 
            // ageTextbox
            // 
            this.ageTextbox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ageTextbox.ForeColor = System.Drawing.Color.Black;
            this.ageTextbox.Location = new System.Drawing.Point(565, 92);
            this.ageTextbox.Name = "ageTextbox";
            this.ageTextbox.ReadOnly = true;
            this.ageTextbox.Size = new System.Drawing.Size(28, 21);
            this.ageTextbox.TabIndex = 23;
            this.ageTextbox.TabStop = false;
            // 
            // pwnapp_firstname
            // 
            this.pwnapp_firstname.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pwnapp_firstname.BackColor = System.Drawing.Color.White;
            this.pwnapp_firstname.CausesValidation = false;
            this.pwnapp_firstname.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_firstname.FirstLetterUppercase = true;
            this.pwnapp_firstname.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_firstname.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_firstname.Location = new System.Drawing.Point(76, 33);
            this.pwnapp_firstname.MaxLength = 40;
            this.pwnapp_firstname.Name = "pwnapp_firstname";
            this.pwnapp_firstname.Size = new System.Drawing.Size(114, 21);
            this.pwnapp_firstname.TabIndex = 2;
            this.pwnapp_firstname.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // pwnapp_lastname
            // 
            this.pwnapp_lastname.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pwnapp_lastname.BackColor = System.Drawing.Color.White;
            this.pwnapp_lastname.CausesValidation = false;
            this.pwnapp_lastname.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_lastname.FirstLetterUppercase = true;
            this.pwnapp_lastname.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_lastname.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_lastname.Location = new System.Drawing.Point(76, 92);
            this.pwnapp_lastname.MaxLength = 40;
            this.pwnapp_lastname.Name = "pwnapp_lastname";
            this.pwnapp_lastname.Size = new System.Drawing.Size(114, 21);
            this.pwnapp_lastname.TabIndex = 4;
            this.pwnapp_lastname.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // pwnapp_address2
            // 
            this.pwnapp_address2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_address2.BackColor = System.Drawing.Color.White;
            this.pwnapp_address2.CausesValidation = false;
            this.pwnapp_address2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.pwnapp_address2.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_address2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_address2.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_address2.Location = new System.Drawing.Point(383, 33);
            this.pwnapp_address2.MaxLength = 39;
            this.pwnapp_address2.Name = "pwnapp_address2";
            this.pwnapp_address2.RegularExpression = true;
            this.pwnapp_address2.Size = new System.Drawing.Size(113, 21);
            this.pwnapp_address2.TabIndex = 8;
            this.pwnapp_address2.ValidationExpression = "^[\\w#\\ ]*$";
            // 
            // pwnapp_city
            // 
            this.pwnapp_city.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pwnapp_city.BackColor = System.Drawing.Color.White;
            this.pwnapp_city.CausesValidation = false;
            this.pwnapp_city.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_city.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_city.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_city.Location = new System.Drawing.Point(565, 62);
            this.pwnapp_city.MaxLength = 50;
            this.pwnapp_city.Name = "pwnapp_city";
            this.pwnapp_city.RegularExpression = true;
            this.pwnapp_city.Size = new System.Drawing.Size(113, 21);
            this.pwnapp_city.TabIndex = 10;
            this.pwnapp_city.ValidationExpression = "^[\\w\\ ]*$";
            // 
            // pwnapp_middleinitial_label
            // 
            this.pwnapp_middleinitial_label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pwnapp_middleinitial_label.Location = new System.Drawing.Point(3, 66);
            this.pwnapp_middleinitial_label.Name = "pwnapp_middleinitial_label";
            this.pwnapp_middleinitial_label.Size = new System.Drawing.Size(67, 13);
            this.pwnapp_middleinitial_label.TabIndex = 4;
            this.pwnapp_middleinitial_label.Text = "Middle Name";
            this.pwnapp_middleinitial_label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pwnapp_state
            // 
            this.pwnapp_state.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_state.BackColor = System.Drawing.Color.Transparent;
            this.pwnapp_state.CausesValidation = false;
            this.pwnapp_state.DisplayCode = true;
            this.pwnapp_state.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_state.Location = new System.Drawing.Point(734, 62);
            this.pwnapp_state.Name = "pwnapp_state";
            this.pwnapp_state.selectedValue = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_state.Size = new System.Drawing.Size(49, 21);
            this.pwnapp_state.TabIndex = 11;
            // 
            // pwnapp_middleinitial
            // 
            this.pwnapp_middleinitial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pwnapp_middleinitial.BackColor = System.Drawing.Color.White;
            this.pwnapp_middleinitial.CausesValidation = false;
            this.pwnapp_middleinitial.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_middleinitial.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_middleinitial.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_middleinitial.Location = new System.Drawing.Point(76, 62);
            this.pwnapp_middleinitial.MaxLength = 40;
            this.pwnapp_middleinitial.Name = "pwnapp_middleinitial";
            this.pwnapp_middleinitial.Size = new System.Drawing.Size(114, 21);
            this.pwnapp_middleinitial.TabIndex = 3;
            this.pwnapp_middleinitial.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // pwnapp_title
            // 
            this.pwnapp_title.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_title.CausesValidation = false;
            this.pwnapp_title.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_title.Location = new System.Drawing.Point(76, 3);
            this.pwnapp_title.Name = "pwnapp_title";
            this.pwnapp_title.Size = new System.Drawing.Size(64, 23);
            this.pwnapp_title.TabIndex = 1;
            // 
            // pwnapp_unit
            // 
            this.pwnapp_unit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pwnapp_unit.BackColor = System.Drawing.Color.White;
            this.pwnapp_unit.CausesValidation = false;
            this.pwnapp_unit.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_unit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_unit.Location = new System.Drawing.Point(734, 4);
            this.pwnapp_unit.MaxLength = 5;
            this.pwnapp_unit.Name = "pwnapp_unit";
            this.pwnapp_unit.Size = new System.Drawing.Size(60, 21);
            this.pwnapp_unit.TabIndex = 7;
            this.pwnapp_unit.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // pwnapp_dateofbirth
            // 
            this.pwnapp_dateofbirth.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_dateofbirth.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.pwnapp_dateofbirth.CausesValidation = false;
            this.pwnapp_dateofbirth.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_dateofbirth.Location = new System.Drawing.Point(383, 93);
            this.pwnapp_dateofbirth.Name = "pwnapp_dateofbirth";
            this.pwnapp_dateofbirth.Size = new System.Drawing.Size(100, 20);
            this.pwnapp_dateofbirth.TabIndex = 12;
            this.pwnapp_dateofbirth.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.pwnapp_dateofbirth.ValidationExpression = "^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.]19[0-9][0-9]|20[0" +
                "-9][0-9]$";
            this.pwnapp_dateofbirth.Leave += new System.EventHandler(this.dateOfBirth_Leave);
            // 
            // pwnapp_firstname_label
            // 
            this.pwnapp_firstname_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_firstname_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_firstname_label.Location = new System.Drawing.Point(3, 35);
            this.pwnapp_firstname_label.Name = "pwnapp_firstname_label";
            this.pwnapp_firstname_label.Size = new System.Drawing.Size(58, 17);
            this.pwnapp_firstname_label.TabIndex = 25;
            this.pwnapp_firstname_label.Text = "First Name";
            // 
            // pwnapp_lastname_label
            // 
            this.pwnapp_lastname_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_lastname_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_lastname_label.Location = new System.Drawing.Point(3, 96);
            this.pwnapp_lastname_label.Name = "pwnapp_lastname_label";
            this.pwnapp_lastname_label.Size = new System.Drawing.Size(58, 13);
            this.pwnapp_lastname_label.TabIndex = 26;
            this.pwnapp_lastname_label.Text = "Last Name";
            // 
            // pwnapp_address
            // 
            this.pwnapp_address.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_address.BackColor = System.Drawing.Color.White;
            this.pwnapp_address.CausesValidation = false;
            this.pwnapp_address.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.pwnapp_address.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_address.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_address.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_address.Location = new System.Drawing.Point(383, 4);
            this.pwnapp_address.MaxLength = 40;
            this.pwnapp_address.Name = "pwnapp_address";
            this.pwnapp_address.RegularExpression = true;
            this.pwnapp_address.Size = new System.Drawing.Size(126, 21);
            this.pwnapp_address.TabIndex = 6;
            this.pwnapp_address.ValidationExpression = "^[\\w#\\ ]*$";
            // 
            // pwnapp_address_label
            // 
            this.pwnapp_address_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_address_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_address_label.Location = new System.Drawing.Point(323, 5);
            this.pwnapp_address_label.Name = "pwnapp_address_label";
            this.pwnapp_address_label.Size = new System.Drawing.Size(46, 18);
            this.pwnapp_address_label.TabIndex = 27;
            this.pwnapp_address_label.Text = "Address";
            // 
            // pwnapp_city_label
            // 
            this.pwnapp_city_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_city_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_city_label.Location = new System.Drawing.Point(515, 64);
            this.pwnapp_city_label.Name = "pwnapp_city_label";
            this.pwnapp_city_label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.pwnapp_city_label.Size = new System.Drawing.Size(35, 16);
            this.pwnapp_city_label.TabIndex = 28;
            this.pwnapp_city_label.Text = "City";
            // 
            // pwnapp_zip_label
            // 
            this.pwnapp_zip_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_zip_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_zip_label.Location = new System.Drawing.Point(323, 66);
            this.pwnapp_zip_label.Name = "pwnapp_zip_label";
            this.pwnapp_zip_label.Size = new System.Drawing.Size(46, 13);
            this.pwnapp_zip_label.TabIndex = 29;
            this.pwnapp_zip_label.Text = "Zipcode";
            // 
            // pwnapp_dateofbirth_label
            // 
            this.pwnapp_dateofbirth_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_dateofbirth_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_dateofbirth_label.Location = new System.Drawing.Point(323, 96);
            this.pwnapp_dateofbirth_label.MinimumSize = new System.Drawing.Size(44, 13);
            this.pwnapp_dateofbirth_label.Name = "pwnapp_dateofbirth_label";
            this.pwnapp_dateofbirth_label.Size = new System.Drawing.Size(44, 13);
            this.pwnapp_dateofbirth_label.TabIndex = 30;
            this.pwnapp_dateofbirth_label.Text = "DOB";
            // 
            // pwnapp_state_label
            // 
            this.pwnapp_state_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_state_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_state_label.Location = new System.Drawing.Point(684, 64);
            this.pwnapp_state_label.Name = "pwnapp_state_label";
            this.pwnapp_state_label.Size = new System.Drawing.Size(35, 16);
            this.pwnapp_state_label.TabIndex = 31;
            this.pwnapp_state_label.Text = "State";
            // 
            // pwnapp_title_label
            // 
            this.pwnapp_title_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_title_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_title_label.Location = new System.Drawing.Point(3, 8);
            this.pwnapp_title_label.Name = "pwnapp_title_label";
            this.pwnapp_title_label.Size = new System.Drawing.Size(27, 13);
            this.pwnapp_title_label.TabIndex = 32;
            this.pwnapp_title_label.Text = "Title";
            // 
            // pwnapp_titlesuffix_label
            // 
            this.pwnapp_titlesuffix_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_titlesuffix_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_titlesuffix_label.Location = new System.Drawing.Point(196, 94);
            this.pwnapp_titlesuffix_label.Name = "pwnapp_titlesuffix_label";
            this.pwnapp_titlesuffix_label.Size = new System.Drawing.Size(60, 18);
            this.pwnapp_titlesuffix_label.TabIndex = 33;
            this.pwnapp_titlesuffix_label.Text = "Title Suffix";
            // 
            // pwnapp_unit_label
            // 
            this.pwnapp_unit_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_unit_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_unit_label.Location = new System.Drawing.Point(684, 8);
            this.pwnapp_unit_label.Name = "pwnapp_unit_label";
            this.pwnapp_unit_label.Size = new System.Drawing.Size(26, 13);
            this.pwnapp_unit_label.TabIndex = 34;
            this.pwnapp_unit_label.Text = "Unit";
            // 
            // pwnapp_zip
            // 
            this.pwnapp_zip.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_zip.BackColor = System.Drawing.Color.Transparent;
            this.pwnapp_zip.CausesValidation = false;
            this.pwnapp_zip.City = null;
            this.pwnapp_zip.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_zip.Location = new System.Drawing.Point(380, 62);
            this.pwnapp_zip.Margin = new System.Windows.Forms.Padding(0);
            this.pwnapp_zip.Name = "pwnapp_zip";
            this.pwnapp_zip.Size = new System.Drawing.Size(75, 20);
            this.pwnapp_zip.State = null;
            this.pwnapp_zip.TabIndex = 9;
            this.pwnapp_zip.Leave += new System.EventHandler(this.zipcode1_Leave);
            // 
            // pwnapp_titlesuffix
            // 
            this.pwnapp_titlesuffix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pwnapp_titlesuffix.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.pwnapp_titlesuffix.CausesValidation = false;
            this.pwnapp_titlesuffix.Location = new System.Drawing.Point(262, 92);
            this.pwnapp_titlesuffix.Name = "pwnapp_titlesuffix";
            this.pwnapp_titlesuffix.Size = new System.Drawing.Size(55, 22);
            this.pwnapp_titlesuffix.TabIndex = 5;
            // 
            // custCommentAlert
            // 
            this.custCommentAlert.AutoEllipsis = true;
            this.custCommentAlert.AutoSize = true;
            this.custCommentAlert.BackColor = System.Drawing.Color.Transparent;
            this.custCommentAlert.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.custCommentAlert.ForeColor = System.Drawing.Color.Red;
            this.custCommentAlert.Location = new System.Drawing.Point(315, 42);
            this.custCommentAlert.Name = "custCommentAlert";
            this.custCommentAlert.Size = new System.Drawing.Size(176, 16);
            this.custCommentAlert.TabIndex = 49;
            this.custCommentAlert.TabStop = true;
            this.custCommentAlert.Text = "View Customer Comments";
            this.custCommentAlert.Visible = false;
            // 
            // personalIdentPanel
            // 
            this.personalIdentPanel.AutoSize = true;
            this.personalIdentPanel.BackColor = System.Drawing.Color.MediumBlue;
            this.personalIdentPanel.Controls.Add(this.labelPersonalIDHeading);
            this.personalIdentPanel.ForeColor = System.Drawing.Color.Black;
            this.personalIdentPanel.Location = new System.Drawing.Point(7, 213);
            this.personalIdentPanel.Name = "personalIdentPanel";
            this.personalIdentPanel.Size = new System.Drawing.Size(800, 19);
            this.personalIdentPanel.TabIndex = 3;
            // 
            // labelPersonalIDHeading
            // 
            this.labelPersonalIDHeading.CausesValidation = false;
            this.labelPersonalIDHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPersonalIDHeading.ForeColor = System.Drawing.Color.White;
            this.labelPersonalIDHeading.Location = new System.Drawing.Point(5, 2);
            this.labelPersonalIDHeading.Name = "labelPersonalIDHeading";
            this.labelPersonalIDHeading.Size = new System.Drawing.Size(154, 16);
            this.labelPersonalIDHeading.TabIndex = 0;
            this.labelPersonalIDHeading.Text = "Personal Identification";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.CausesValidation = false;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.pwnapp_identificationtype, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.pwnapp_identificationnumber, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.pwnapp_identificationtype_label, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pwnapp_identificationnumber_label, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.pwnapp_identificationstate_label, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.pwnapp_identificationexpirationdate, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.pwnapp_identificationcountry, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.pwnapp_identificationexpirationdate_label, 3, 0);
            this.tableLayoutPanel2.ForeColor = System.Drawing.Color.Black;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(8, 236);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(552, 43);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // pwnapp_identificationtype
            // 
            this.pwnapp_identificationtype.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pwnapp_identificationtype.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.pwnapp_identificationtype.CausesValidation = false;
            this.pwnapp_identificationtype.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_identificationtype.Location = new System.Drawing.Point(3, 18);
            this.pwnapp_identificationtype.MaximumSize = new System.Drawing.Size(165, 21);
            this.pwnapp_identificationtype.MinimumSize = new System.Drawing.Size(165, 21);
            this.pwnapp_identificationtype.Name = "pwnapp_identificationtype";
            this.pwnapp_identificationtype.Size = new System.Drawing.Size(165, 21);
            this.pwnapp_identificationtype.TabIndex = 13;
            this.pwnapp_identificationtype.Leave += new System.EventHandler(this.idType1_Leave_1);
            // 
            // pwnapp_identificationnumber
            // 
            this.pwnapp_identificationnumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pwnapp_identificationnumber.BackColor = System.Drawing.Color.White;
            this.pwnapp_identificationnumber.CausesValidation = false;
            this.pwnapp_identificationnumber.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_identificationnumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_identificationnumber.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_identificationnumber.Location = new System.Drawing.Point(316, 18);
            this.pwnapp_identificationnumber.MaximumSize = new System.Drawing.Size(120, 21);
            this.pwnapp_identificationnumber.MaxLength = 20;
            this.pwnapp_identificationnumber.MinimumSize = new System.Drawing.Size(120, 21);
            this.pwnapp_identificationnumber.Name = "pwnapp_identificationnumber";
            this.pwnapp_identificationnumber.Size = new System.Drawing.Size(120, 21);
            this.pwnapp_identificationnumber.TabIndex = 15;
            this.pwnapp_identificationnumber.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_identificationnumber.Leave += new System.EventHandler(this.pwnapp_identificationnumber_Leave);
            // 
            // pwnapp_identificationtype_label
            // 
            this.pwnapp_identificationtype_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pwnapp_identificationtype_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_identificationtype_label.Location = new System.Drawing.Point(63, 0);
            this.pwnapp_identificationtype_label.Name = "pwnapp_identificationtype_label";
            this.pwnapp_identificationtype_label.Size = new System.Drawing.Size(45, 15);
            this.pwnapp_identificationtype_label.TabIndex = 15;
            this.pwnapp_identificationtype_label.Text = "ID Type";
            this.pwnapp_identificationtype_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pwnapp_identificationnumber_label
            // 
            this.pwnapp_identificationnumber_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pwnapp_identificationnumber_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_identificationnumber_label.Location = new System.Drawing.Point(347, 0);
            this.pwnapp_identificationnumber_label.Name = "pwnapp_identificationnumber_label";
            this.pwnapp_identificationnumber_label.Size = new System.Drawing.Size(58, 15);
            this.pwnapp_identificationnumber_label.TabIndex = 17;
            this.pwnapp_identificationnumber_label.Text = "ID Number";
            // 
            // pwnapp_identificationstate_label
            // 
            this.pwnapp_identificationstate_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pwnapp_identificationstate_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_identificationstate_label.Location = new System.Drawing.Point(223, 0);
            this.pwnapp_identificationstate_label.Name = "pwnapp_identificationstate_label";
            this.pwnapp_identificationstate_label.Size = new System.Drawing.Size(37, 15);
            this.pwnapp_identificationstate_label.TabIndex = 16;
            this.pwnapp_identificationstate_label.Text = "Issuer";
            // 
            // pwnapp_identificationexpirationdate
            // 
            this.pwnapp_identificationexpirationdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pwnapp_identificationexpirationdate.CausesValidation = false;
            this.pwnapp_identificationexpirationdate.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_identificationexpirationdate.Location = new System.Drawing.Point(455, 18);
            this.pwnapp_identificationexpirationdate.MaximumSize = new System.Drawing.Size(120, 21);
            this.pwnapp_identificationexpirationdate.MinimumSize = new System.Drawing.Size(80, 21);
            this.pwnapp_identificationexpirationdate.Name = "pwnapp_identificationexpirationdate";
            this.pwnapp_identificationexpirationdate.Size = new System.Drawing.Size(80, 21);
            this.pwnapp_identificationexpirationdate.TabIndex = 19;
            this.pwnapp_identificationexpirationdate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.pwnapp_identificationexpirationdate.ValidationExpression = "^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.]19[0-9][0-9]|20[0" +
                "-9][0-9]$";
            // 
            // pwnapp_identificationcountry
            // 
            this.pwnapp_identificationcountry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pwnapp_identificationcountry.BackColor = System.Drawing.Color.Transparent;
            this.pwnapp_identificationcountry.Location = new System.Drawing.Point(174, 18);
            this.pwnapp_identificationcountry.MaximumSize = new System.Drawing.Size(136, 22);
            this.pwnapp_identificationcountry.MinimumSize = new System.Drawing.Size(51, 22);
            this.pwnapp_identificationcountry.Name = "pwnapp_identificationcountry";
            this.pwnapp_identificationcountry.Size = new System.Drawing.Size(136, 22);
            this.pwnapp_identificationcountry.TabIndex = 14;
            this.pwnapp_identificationcountry.Leave += new System.EventHandler(this.country_Leave);
            // 
            // pwnapp_identificationexpirationdate_label
            // 
            this.pwnapp_identificationexpirationdate_label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pwnapp_identificationexpirationdate_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_identificationexpirationdate_label.Location = new System.Drawing.Point(451, 0);
            this.pwnapp_identificationexpirationdate_label.Name = "pwnapp_identificationexpirationdate_label";
            this.pwnapp_identificationexpirationdate_label.Size = new System.Drawing.Size(89, 15);
            this.pwnapp_identificationexpirationdate_label.TabIndex = 18;
            this.pwnapp_identificationexpirationdate_label.Text = "ID Expiration Date";
            this.pwnapp_identificationexpirationdate_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // physicalDescriptionPanel
            // 
            this.physicalDescriptionPanel.AutoSize = true;
            this.physicalDescriptionPanel.BackColor = System.Drawing.Color.MediumBlue;
            this.physicalDescriptionPanel.Controls.Add(this.labelPhysicalDescHeading);
            this.physicalDescriptionPanel.ForeColor = System.Drawing.Color.Black;
            this.physicalDescriptionPanel.Location = new System.Drawing.Point(7, 281);
            this.physicalDescriptionPanel.Name = "physicalDescriptionPanel";
            this.physicalDescriptionPanel.Size = new System.Drawing.Size(800, 23);
            this.physicalDescriptionPanel.TabIndex = 5;
            // 
            // labelPhysicalDescHeading
            // 
            this.labelPhysicalDescHeading.CausesValidation = false;
            this.labelPhysicalDescHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPhysicalDescHeading.ForeColor = System.Drawing.Color.White;
            this.labelPhysicalDescHeading.Location = new System.Drawing.Point(4, 4);
            this.labelPhysicalDescHeading.Name = "labelPhysicalDescHeading";
            this.labelPhysicalDescHeading.Size = new System.Drawing.Size(137, 16);
            this.labelPhysicalDescHeading.TabIndex = 0;
            this.labelPhysicalDescHeading.Text = "Physical Description";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel4.ColumnCount = 16;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.inchesLabel, 12, 0);
            this.tableLayoutPanel4.Controls.Add(this.lbsLabel, 15, 0);
            this.tableLayoutPanel4.Controls.Add(this.pwnapp_height, 8, 0);
            this.tableLayoutPanel4.Controls.Add(this.pwnapp_heightinches, 11, 0);
            this.tableLayoutPanel4.Controls.Add(this.pwnapp_weight, 14, 0);
            this.tableLayoutPanel4.Controls.Add(this.ftLabel, 9, 0);
            this.tableLayoutPanel4.Controls.Add(this.pwnapp_height_label, 6, 0);
            this.tableLayoutPanel4.Controls.Add(this.pwnapp_socialsecuritynumber_label, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.pwnapp_weight_label, 13, 0);
            this.tableLayoutPanel4.Controls.Add(this.pwnapp_socialsecuritynumber, 2, 0);
            this.tableLayoutPanel4.ForeColor = System.Drawing.Color.Black;
            this.tableLayoutPanel4.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(8, 337);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(798, 29);
            this.tableLayoutPanel4.TabIndex = 7;
            // 
            // inchesLabel
            // 
            this.inchesLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.inchesLabel.Location = new System.Drawing.Point(397, 8);
            this.inchesLabel.Name = "inchesLabel";
            this.inchesLabel.Size = new System.Drawing.Size(18, 13);
            this.inchesLabel.TabIndex = 8;
            this.inchesLabel.Text = "In";
            // 
            // lbsLabel
            // 
            this.lbsLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbsLabel.Location = new System.Drawing.Point(534, 8);
            this.lbsLabel.Name = "lbsLabel";
            this.lbsLabel.Size = new System.Drawing.Size(259, 13);
            this.lbsLabel.TabIndex = 11;
            this.lbsLabel.Text = "lbs";
            // 
            // pwnapp_height
            // 
            this.pwnapp_height.AllowOnlyNumbers = true;
            this.pwnapp_height.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pwnapp_height.BackColor = System.Drawing.Color.White;
            this.pwnapp_height.CausesValidation = false;
            this.pwnapp_height.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_height.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_height.Location = new System.Drawing.Point(307, 4);
            this.pwnapp_height.MaxLength = 1;
            this.pwnapp_height.Name = "pwnapp_height";
            this.pwnapp_height.RegularExpression = true;
            this.pwnapp_height.Size = new System.Drawing.Size(21, 21);
            this.pwnapp_height.TabIndex = 22;
            this.pwnapp_height.ValidationExpression = "^([2-9])";
            // 
            // pwnapp_heightinches
            // 
            this.pwnapp_heightinches.AllowOnlyNumbers = true;
            this.pwnapp_heightinches.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pwnapp_heightinches.BackColor = System.Drawing.Color.White;
            this.pwnapp_heightinches.CausesValidation = false;
            this.pwnapp_heightinches.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_heightinches.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_heightinches.Location = new System.Drawing.Point(358, 4);
            this.pwnapp_heightinches.MaxLength = 2;
            this.pwnapp_heightinches.Name = "pwnapp_heightinches";
            this.pwnapp_heightinches.RegularExpression = true;
            this.pwnapp_heightinches.Size = new System.Drawing.Size(32, 21);
            this.pwnapp_heightinches.TabIndex = 23;
            this.pwnapp_heightinches.ValidationExpression = "(^[1-9]{1}$|^[1-9]{1}[0]{1}$|^11$)";
            // 
            // pwnapp_weight
            // 
            this.pwnapp_weight.AllowOnlyNumbers = true;
            this.pwnapp_weight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pwnapp_weight.BackColor = System.Drawing.Color.White;
            this.pwnapp_weight.CausesValidation = false;
            this.pwnapp_weight.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_weight.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_weight.Location = new System.Drawing.Point(483, 4);
            this.pwnapp_weight.MaxLength = 3;
            this.pwnapp_weight.Name = "pwnapp_weight";
            this.pwnapp_weight.RegularExpression = true;
            this.pwnapp_weight.Size = new System.Drawing.Size(44, 21);
            this.pwnapp_weight.TabIndex = 24;
            this.pwnapp_weight.ValidationExpression = "(0*[1-9][0-9]*[0-9]*)";
            // 
            // ftLabel
            // 
            this.ftLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ftLabel.Location = new System.Drawing.Point(334, 7);
            this.ftLabel.Name = "ftLabel";
            this.ftLabel.Size = new System.Drawing.Size(18, 15);
            this.ftLabel.TabIndex = 6;
            this.ftLabel.Text = "ft";
            // 
            // pwnapp_height_label
            // 
            this.pwnapp_height_label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pwnapp_height_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_height_label.Location = new System.Drawing.Point(252, 8);
            this.pwnapp_height_label.Name = "pwnapp_height_label";
            this.pwnapp_height_label.Size = new System.Drawing.Size(38, 13);
            this.pwnapp_height_label.TabIndex = 25;
            this.pwnapp_height_label.Text = "Height";
            // 
            // pwnapp_socialsecuritynumber_label
            // 
            this.pwnapp_socialsecuritynumber_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_socialsecuritynumber_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_socialsecuritynumber_label.Location = new System.Drawing.Point(3, 8);
            this.pwnapp_socialsecuritynumber_label.Name = "pwnapp_socialsecuritynumber_label";
            this.pwnapp_socialsecuritynumber_label.Size = new System.Drawing.Size(36, 13);
            this.pwnapp_socialsecuritynumber_label.TabIndex = 26;
            this.pwnapp_socialsecuritynumber_label.Text = "SSN#";
            // 
            // pwnapp_weight_label
            // 
            this.pwnapp_weight_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_weight_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_weight_label.Location = new System.Drawing.Point(423, 8);
            this.pwnapp_weight_label.Name = "pwnapp_weight_label";
            this.pwnapp_weight_label.Size = new System.Drawing.Size(41, 13);
            this.pwnapp_weight_label.TabIndex = 27;
            this.pwnapp_weight_label.Text = "Weight";
            // 
            // pwnapp_socialsecuritynumber
            // 
            this.pwnapp_socialsecuritynumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pwnapp_socialsecuritynumber.CausesValidation = false;
            this.pwnapp_socialsecuritynumber.Location = new System.Drawing.Point(45, 4);
            this.pwnapp_socialsecuritynumber.Name = "pwnapp_socialsecuritynumber";
            this.pwnapp_socialsecuritynumber.Size = new System.Drawing.Size(94, 20);
            this.pwnapp_socialsecuritynumber.TabIndex = 21;
            // 
            // phoneNumPanel
            // 
            this.phoneNumPanel.AutoSize = true;
            this.phoneNumPanel.BackColor = System.Drawing.Color.MediumBlue;
            this.phoneNumPanel.Controls.Add(this.labelPhoneNumbersHeading);
            this.phoneNumPanel.ForeColor = System.Drawing.Color.Black;
            this.phoneNumPanel.Location = new System.Drawing.Point(7, 368);
            this.phoneNumPanel.Name = "phoneNumPanel";
            this.phoneNumPanel.Size = new System.Drawing.Size(799, 26);
            this.phoneNumPanel.TabIndex = 8;
            // 
            // labelPhoneNumbersHeading
            // 
            this.labelPhoneNumbersHeading.CausesValidation = false;
            this.labelPhoneNumbersHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPhoneNumbersHeading.ForeColor = System.Drawing.Color.White;
            this.labelPhoneNumbersHeading.Location = new System.Drawing.Point(4, 7);
            this.labelPhoneNumbersHeading.Name = "labelPhoneNumbersHeading";
            this.labelPhoneNumbersHeading.Size = new System.Drawing.Size(108, 16);
            this.labelPhoneNumbersHeading.TabIndex = 0;
            this.labelPhoneNumbersHeading.Text = "Phone Numbers";
            // 
            // notesPanel
            // 
            this.notesPanel.AutoSize = true;
            this.notesPanel.BackColor = System.Drawing.Color.MediumBlue;
            this.notesPanel.Controls.Add(this.pwnapp_comments_label);
            this.notesPanel.ForeColor = System.Drawing.Color.Black;
            this.notesPanel.Location = new System.Drawing.Point(3, 3);
            this.notesPanel.Name = "notesPanel";
            this.notesPanel.Size = new System.Drawing.Size(51, 19);
            this.notesPanel.TabIndex = 10;
            // 
            // pwnapp_comments_label
            // 
            this.pwnapp_comments_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_comments_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_comments_label.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.pwnapp_comments_label.Location = new System.Drawing.Point(3, 3);
            this.pwnapp_comments_label.Name = "pwnapp_comments_label";
            this.pwnapp_comments_label.Size = new System.Drawing.Size(45, 16);
            this.pwnapp_comments_label.TabIndex = 0;
            this.pwnapp_comments_label.Text = "Notes";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel3.CausesValidation = false;
            this.tableLayoutPanel3.ColumnCount = 9;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 166F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.pwnapp_race, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.pwnapp_eyes, 6, 0);
            this.tableLayoutPanel3.Controls.Add(this.pwnapp_hair, 8, 0);
            this.tableLayoutPanel3.Controls.Add(this.pwnapp_sex, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.pwnapp_race_label, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.pwnapp_sex_label, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.pwnapp_eyes_label, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.pwnapp_hair_label, 7, 0);
            this.tableLayoutPanel3.ForeColor = System.Drawing.Color.Black;
            this.tableLayoutPanel3.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(8, 307);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(791, 28);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // pwnapp_race
            // 
            this.pwnapp_race.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_race.BackColor = System.Drawing.Color.White;
            this.pwnapp_race.CausesValidation = false;
            this.pwnapp_race.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_race.Location = new System.Drawing.Point(47, 3);
            this.pwnapp_race.Name = "pwnapp_race";
            this.pwnapp_race.Size = new System.Drawing.Size(136, 21);
            this.pwnapp_race.TabIndex = 17;
            // 
            // pwnapp_eyes
            // 
            this.pwnapp_eyes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_eyes.BackColor = System.Drawing.Color.Transparent;
            this.pwnapp_eyes.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_eyes.isValid = true;
            this.pwnapp_eyes.Location = new System.Drawing.Point(432, 3);
            this.pwnapp_eyes.Name = "pwnapp_eyes";
            this.pwnapp_eyes.Size = new System.Drawing.Size(128, 22);
            this.pwnapp_eyes.TabIndex = 19;
            // 
            // pwnapp_hair
            // 
            this.pwnapp_hair.AutoSize = true;
            this.pwnapp_hair.BackColor = System.Drawing.Color.Transparent;
            this.pwnapp_hair.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_hair.Location = new System.Drawing.Point(641, 3);
            this.pwnapp_hair.MaximumSize = new System.Drawing.Size(132, 22);
            this.pwnapp_hair.MinimumSize = new System.Drawing.Size(132, 22);
            this.pwnapp_hair.Name = "pwnapp_hair";
            this.pwnapp_hair.Size = new System.Drawing.Size(132, 22);
            this.pwnapp_hair.TabIndex = 20;
            // 
            // pwnapp_sex
            // 
            this.pwnapp_sex.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_sex.BackColor = System.Drawing.Color.White;
            this.pwnapp_sex.CausesValidation = false;
            this.pwnapp_sex.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_sex.isValid = true;
            this.pwnapp_sex.Location = new System.Drawing.Point(270, 3);
            this.pwnapp_sex.Name = "pwnapp_sex";
            this.pwnapp_sex.Size = new System.Drawing.Size(83, 21);
            this.pwnapp_sex.TabIndex = 18;
            // 
            // pwnapp_race_label
            // 
            this.pwnapp_race_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_race_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_race_label.Location = new System.Drawing.Point(3, 7);
            this.pwnapp_race_label.Name = "pwnapp_race_label";
            this.pwnapp_race_label.Size = new System.Drawing.Size(33, 13);
            this.pwnapp_race_label.TabIndex = 21;
            this.pwnapp_race_label.Text = "Race";
            // 
            // pwnapp_sex_label
            // 
            this.pwnapp_sex_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_sex_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_sex_label.Location = new System.Drawing.Point(213, 7);
            this.pwnapp_sex_label.Name = "pwnapp_sex_label";
            this.pwnapp_sex_label.Size = new System.Drawing.Size(42, 13);
            this.pwnapp_sex_label.TabIndex = 22;
            this.pwnapp_sex_label.Text = "Gender";
            // 
            // pwnapp_eyes_label
            // 
            this.pwnapp_eyes_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_eyes_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_eyes_label.Location = new System.Drawing.Point(365, 6);
            this.pwnapp_eyes_label.Name = "pwnapp_eyes_label";
            this.pwnapp_eyes_label.Size = new System.Drawing.Size(56, 15);
            this.pwnapp_eyes_label.TabIndex = 23;
            this.pwnapp_eyes_label.Text = "Eye Color";
            // 
            // pwnapp_hair_label
            // 
            this.pwnapp_hair_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_hair_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_hair_label.Location = new System.Drawing.Point(573, 7);
            this.pwnapp_hair_label.Name = "pwnapp_hair_label";
            this.pwnapp_hair_label.Size = new System.Drawing.Size(56, 13);
            this.pwnapp_hair_label.TabIndex = 24;
            this.pwnapp_hair_label.Text = "Hair Color";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.BackColor = System.Drawing.Color.MediumBlue;
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.notesPanel, 0, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(7, 510);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(800, 34);
            this.tableLayoutPanel6.TabIndex = 44;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.phoneData1);
            this.panel1.Location = new System.Drawing.Point(7, 395);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(799, 118);
            this.panel1.TabIndex = 25;
            this.panel1.TabStop = true;
            // 
            // phoneData1
            // 
            this.phoneData1.BackColor = System.Drawing.Color.Transparent;
            this.phoneData1.CausesValidation = false;
            this.phoneData1.Location = new System.Drawing.Point(4, 2);
            this.phoneData1.Name = "phoneData1";
            this.phoneData1.ShowFax = false;
            this.phoneData1.ShowHeading = false;
            this.phoneData1.ShowPager = false;
            this.phoneData1.Size = new System.Drawing.Size(643, 112);
            this.phoneData1.TabCountryCode = false;
            this.phoneData1.TabExtension = false;
            this.phoneData1.TabIndex = 25;
            this.phoneData1.Leave += new System.EventHandler(this.phoneData1_Leave);
            // 
            // panelNotes
            // 
            this.panelNotes.BackColor = System.Drawing.Color.Transparent;
            this.panelNotes.Controls.Add(this.notesRichTextBox);
            this.panelNotes.Location = new System.Drawing.Point(7, 544);
            this.panelNotes.Name = "panelNotes";
            this.panelNotes.Size = new System.Drawing.Size(799, 49);
            this.panelNotes.TabIndex = 48;
            // 
            // notesRichTextBox
            // 
            this.notesRichTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.notesRichTextBox.ForeColor = System.Drawing.Color.Black;
            this.notesRichTextBox.Location = new System.Drawing.Point(1, 0);
            this.notesRichTextBox.MaxLength = 1301;
            this.notesRichTextBox.Name = "notesRichTextBox";
            this.notesRichTextBox.Size = new System.Drawing.Size(796, 46);
            this.notesRichTextBox.TabIndex = 26;
            this.notesRichTextBox.Text = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customButtonSubmit
            // 
            this.customButtonSubmit.BackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonSubmit.BackgroundImage")));
            this.customButtonSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonSubmit.FlatAppearance.BorderSize = 0;
            this.customButtonSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonSubmit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonSubmit.ForeColor = System.Drawing.Color.White;
            this.customButtonSubmit.Location = new System.Drawing.Point(681, 606);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 29;
            this.customButtonSubmit.Text = "Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.managePawnSubmitButton_Click);
            // 
            // customButtonReset
            // 
            this.customButtonReset.BackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonReset.BackgroundImage")));
            this.customButtonReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonReset.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonReset.FlatAppearance.BorderSize = 0;
            this.customButtonReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonReset.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonReset.ForeColor = System.Drawing.Color.White;
            this.customButtonReset.Location = new System.Drawing.Point(554, 608);
            this.customButtonReset.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonReset.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonReset.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonReset.Name = "customButtonReset";
            this.customButtonReset.Size = new System.Drawing.Size(100, 50);
            this.customButtonReset.TabIndex = 28;
            this.customButtonReset.Text = "Reset";
            this.customButtonReset.UseVisualStyleBackColor = false;
            this.customButtonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // customButtonCancel
            // 
            this.customButtonCancel.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonCancel.BackgroundImage")));
            this.customButtonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonCancel.CausesValidation = false;
            this.customButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCancel.FlatAppearance.BorderSize = 0;
            this.customButtonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCancel.ForeColor = System.Drawing.Color.White;
            this.customButtonCancel.Location = new System.Drawing.Point(11, 606);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 27;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.managePawnCancelButton_Click);
            // 
            // customLabel1
            // 
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.Location = new System.Drawing.Point(588, 239);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(66, 13);
            this.customLabel1.TabIndex = 46;
            this.customLabel1.Text = "Issuer State";
            this.customLabel1.Visible = false;
            // 
            // pwnapp_identificationstate
            // 
            this.pwnapp_identificationstate.BackColor = System.Drawing.Color.Transparent;
            this.pwnapp_identificationstate.DisplayCode = true;
            this.pwnapp_identificationstate.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_identificationstate.Location = new System.Drawing.Point(591, 255);
            this.pwnapp_identificationstate.Name = "pwnapp_identificationstate";
            this.pwnapp_identificationstate.selectedValue = global::Pawn.Properties.Resources.OverrideMachineName;
            this.pwnapp_identificationstate.Size = new System.Drawing.Size(50, 21);
            this.pwnapp_identificationstate.TabIndex = 45;
            this.pwnapp_identificationstate.Visible = false;
            this.pwnapp_identificationstate.Leave += new System.EventHandler(this.idIssuer_Leave);
            // 
            // ManagePawnApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.Blue;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(811, 668);
            this.ControlBox = false;
            this.Controls.Add(this.custCommentAlert);
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonReset);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.customLabel1);
            this.Controls.Add(this.pwnapp_identificationstate);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Controls.Add(this.phoneNumPanel);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.physicalDescriptionPanel);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.personalIdentPanel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.personalInfoPanel);
            this.Controls.Add(this.managePawnAppLabel);
            this.Controls.Add(this.panelNotes);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManagePawnApplication";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Pawn Application";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManagePawnApplication_FormClosing);
            this.Load += new System.EventHandler(this.ManagePawnApplication_Load);
            this.Shown += new System.EventHandler(this.ManagePawnApplication_Shown);
            this.personalInfoPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.personalIdentPanel.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.physicalDescriptionPanel.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.phoneNumPanel.ResumeLayout(false);
            this.notesPanel.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panelNotes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label managePawnAppLabel;
        private System.Windows.Forms.Label labelPersonalInfoHeading;
        private System.Windows.Forms.Panel personalInfoPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label pwnapp_middleinitial_label;
        private System.Windows.Forms.Label ageLabel;
        private System.Windows.Forms.TextBox ageTextbox;
        private System.Windows.Forms.Panel personalIdentPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

        private System.Windows.Forms.Panel physicalDescriptionPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label ftLabel;
        private System.Windows.Forms.Label inchesLabel;
        private System.Windows.Forms.Panel phoneNumPanel;
        private System.Windows.Forms.Panel notesPanel;
        private UserControls.IDType pwnapp_identificationtype;
        private CustomTextBox pwnapp_firstname;
        private CustomTextBox pwnapp_lastname;


        private UserControls.Gender pwnapp_sex;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private CustomTextBox pwnapp_address;
        private CustomTextBox pwnapp_address2;
        private CustomTextBox pwnapp_city;
        private UserControls.State pwnapp_state;
        private UserControls.TitleSuffix pwnapp_titlesuffix;
        private UserControls.Race pwnapp_race;
        private CustomTextBox pwnapp_identificationnumber;
        private CustomTextBox pwnapp_middleinitial;
        private UserControls.Title pwnapp_title;
        private CustomTextBox pwnapp_height;
        private CustomTextBox pwnapp_heightinches;
        private CustomTextBox pwnapp_unit;
        private CustomTextBox pwnapp_weight;
        private UserControls.Date pwnapp_dateofbirth;
        private UserControls.Zipcode pwnapp_zip;
        private UserControls.EyeColor pwnapp_eyes;
        private UserControls.Haircolor pwnapp_hair;
        private System.Windows.Forms.Label lbsLabel;
        private System.Windows.Forms.Label labelPersonalIDHeading;
        private System.Windows.Forms.Label labelPhysicalDescHeading;
        private System.Windows.Forms.Label labelPhoneNumbersHeading;
        private UserControls.Country pwnapp_identificationcountry;
        private CustomLabel pwnapp_firstname_label;
        private CustomLabel pwnapp_lastname_label;
        private CustomLabel pwnapp_address_label;
        private CustomLabel pwnapp_city_label;
        private CustomLabel pwnapp_identificationtype_label;
        private CustomLabel pwnapp_identificationstate_label;
        private CustomLabel pwnapp_identificationnumber_label;
        private CustomLabel pwnapp_height_label;
        private CustomLabel pwnapp_zip_label;
        private CustomLabel pwnapp_dateofbirth_label;
        private CustomLabel pwnapp_race_label;
        private CustomLabel pwnapp_state_label;
        private CustomLabel pwnapp_sex_label;
        private CustomLabel pwnapp_eyes_label;
        private CustomLabel pwnapp_hair_label;
        private CustomLabel pwnapp_identificationexpirationdate_label;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private CustomLabel pwnapp_title_label;
        private CustomLabel pwnapp_titlesuffix_label;
        private CustomLabel pwnapp_unit_label;
        private CustomLabel pwnapp_socialsecuritynumber_label;
        private CustomLabel pwnapp_comments_label;
        private UserControls.State pwnapp_identificationstate;
        private CustomLabel pwnapp_weight_label;
        private CustomLabel customLabel1;
        private UserControls.Date pwnapp_identificationexpirationdate;
        private UserControls.SocialSecurityNumber pwnapp_socialsecuritynumber;
        private System.Windows.Forms.Panel panel1;
        private UserControls.PhoneData phoneData1;
        private System.Windows.Forms.Panel panelNotes;
        private System.Windows.Forms.RichTextBox notesRichTextBox;
        private CustomButton customButtonCancel;
        private CustomButton customButtonReset;
        private CustomButton customButtonSubmit;
        //private System.Windows.Forms.Label custCommentAlert;
        private System.Windows.Forms.LinkLabel custCommentAlert;


    }
}
