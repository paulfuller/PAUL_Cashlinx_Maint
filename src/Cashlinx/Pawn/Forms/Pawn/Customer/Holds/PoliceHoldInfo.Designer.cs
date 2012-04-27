using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.Customer.Holds
{
    partial class PoliceHoldInfo
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
            this.labelHeading = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBoxReason = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanelOfficerInfo = new System.Windows.Forms.TableLayoutPanel();
            this.labelCaseNumber = new System.Windows.Forms.Label();
            this.customTextBoxPhoneExt = new CustomTextBox();
            this.customTextBoxPhoneAreaCode = new CustomTextBox();
            this.customTextBoxPhoneNumber = new CustomTextBox();
            this.customTextBoxOfficerFirstName = new CustomTextBox();
            this.customTextBoxOfficerLastName = new CustomTextBox();
            this.customTextBoxBadgeNumber = new CustomTextBox();
            this.customTextBoxAgency = new CustomTextBox();
            this.customTextBoxCaseNumber = new CustomTextBox();
            this.comboBoxReqType = new System.Windows.Forms.ComboBox();
            this.labelExt = new System.Windows.Forms.Label();
            this.labelOfficerFirstName = new CustomLabel();
            this.customLabelOfficerLastName = new CustomLabel();
            this.customLabelBadgeNumber = new CustomLabel();
            this.customLabel1 = new CustomLabel();
            this.customLabelAgency = new CustomLabel();
            this.customLabelReqType = new CustomLabel();
            this.dateCalendarRelease = new DateCalendar();
            this.labelReason = new System.Windows.Forms.Label();
            this.labelAsterisk = new System.Windows.Forms.Label();
            this.customLabelReleaseEligible = new CustomLabel();
            this.tableLayoutPanelOfficerInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(13, 22);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(178, 16);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Police Hold - Information";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(-1, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(693, 2);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // richTextBoxReason
            // 
            this.richTextBoxReason.Location = new System.Drawing.Point(15, 354);
            this.richTextBoxReason.MaxLength = 256;
            this.richTextBoxReason.Name = "richTextBoxReason";
            this.richTextBoxReason.Size = new System.Drawing.Size(652, 156);
            this.richTextBoxReason.TabIndex = 11;
            this.richTextBoxReason.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.DarkBlue;
            this.groupBox2.Location = new System.Drawing.Point(3, 552);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(693, 2);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.buttonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.Location = new System.Drawing.Point(23, 561);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 50);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.Transparent;
            this.buttonBack.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.buttonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonBack.CausesValidation = false;
            this.buttonBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonBack.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.buttonBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.ForeColor = System.Drawing.Color.White;
            this.buttonBack.Location = new System.Drawing.Point(131, 561);
            this.buttonBack.Margin = new System.Windows.Forms.Padding(4);
            this.buttonBack.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonBack.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(100, 50);
            this.buttonBack.TabIndex = 13;
            this.buttonBack.Text = "&Back";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.BackColor = System.Drawing.Color.Transparent;
            this.buttonSubmit.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.buttonSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonSubmit.CausesValidation = false;
            this.buttonSubmit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonSubmit.FlatAppearance.BorderSize = 0;
            this.buttonSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSubmit.ForeColor = System.Drawing.Color.White;
            this.buttonSubmit.Location = new System.Drawing.Point(571, 561);
            this.buttonSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(100, 50);
            this.buttonSubmit.TabIndex = 14;
            this.buttonSubmit.Text = "&Submit";
            this.buttonSubmit.UseVisualStyleBackColor = false;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.DarkBlue;
            this.groupBox3.Location = new System.Drawing.Point(3, 315);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(693, 2);
            this.groupBox3.TabIndex = 56;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "Law Enforcement Information";
            // 
            // tableLayoutPanelOfficerInfo
            // 
            this.tableLayoutPanelOfficerInfo.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelOfficerInfo.ColumnCount = 7;
            this.tableLayoutPanelOfficerInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tableLayoutPanelOfficerInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanelOfficerInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanelOfficerInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanelOfficerInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tableLayoutPanelOfficerInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.tableLayoutPanelOfficerInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 264F));
            this.tableLayoutPanelOfficerInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelOfficerInfo.Controls.Add(this.labelCaseNumber, 5, 1);
            this.tableLayoutPanelOfficerInfo.Controls.Add(this.customTextBoxPhoneExt, 4, 3);
            this.tableLayoutPanelOfficerInfo.Controls.Add(this.customTextBoxPhoneAreaCode, 1, 3);
            this.tableLayoutPanelOfficerInfo.Controls.Add(this.customTextBoxPhoneNumber, 2, 3);
            this.tableLayoutPanelOfficerInfo.Controls.Add(this.customTextBoxOfficerFirstName, 1, 0);
            this.tableLayoutPanelOfficerInfo.Controls.Add(this.customTextBoxOfficerLastName, 1, 1);
            this.tableLayoutPanelOfficerInfo.Controls.Add(this.customTextBoxBadgeNumber, 1, 2);
            this.tableLayoutPanelOfficerInfo.Controls.Add(this.customTextBoxAgency, 6, 0);
            this.tableLayoutPanelOfficerInfo.Controls.Add(this.customTextBoxCaseNumber, 6, 1);
            this.tableLayoutPanelOfficerInfo.Controls.Add(this.comboBoxReqType, 6, 2);
            this.tableLayoutPanelOfficerInfo.Controls.Add(this.labelExt, 3, 3);
            this.tableLayoutPanelOfficerInfo.Controls.Add(this.labelOfficerFirstName, 0, 0);
            this.tableLayoutPanelOfficerInfo.Controls.Add(this.customLabelOfficerLastName, 0, 1);
            this.tableLayoutPanelOfficerInfo.Controls.Add(this.customLabelBadgeNumber, 0, 2);
            this.tableLayoutPanelOfficerInfo.Controls.Add(this.customLabel1, 0, 3);
            this.tableLayoutPanelOfficerInfo.Controls.Add(this.customLabelAgency, 5, 0);
            this.tableLayoutPanelOfficerInfo.Controls.Add(this.customLabelReqType, 5, 2);
            this.tableLayoutPanelOfficerInfo.Location = new System.Drawing.Point(15, 189);
            this.tableLayoutPanelOfficerInfo.Name = "tableLayoutPanelOfficerInfo";
            this.tableLayoutPanelOfficerInfo.RowCount = 4;
            this.tableLayoutPanelOfficerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.3125F));
            this.tableLayoutPanelOfficerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.6875F));
            this.tableLayoutPanelOfficerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelOfficerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelOfficerInfo.Size = new System.Drawing.Size(677, 120);
            this.tableLayoutPanelOfficerInfo.TabIndex = 58;
            // 
            // labelCaseNumber
            // 
            this.labelCaseNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelCaseNumber.AutoSize = true;
            this.labelCaseNumber.BackColor = System.Drawing.Color.Transparent;
            this.labelCaseNumber.Location = new System.Drawing.Point(305, 33);
            this.labelCaseNumber.Name = "labelCaseNumber";
            this.labelCaseNumber.Size = new System.Drawing.Size(71, 13);
            this.labelCaseNumber.TabIndex = 57;
            this.labelCaseNumber.Text = "Case Number";
            // 
            // customTextBoxPhoneExt
            // 
            this.customTextBoxPhoneExt.AllowOnlyNumbers = true;
            this.customTextBoxPhoneExt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxPhoneExt.CausesValidation = false;
            this.customTextBoxPhoneExt.ErrorMessage = "";
            this.customTextBoxPhoneExt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxPhoneExt.Location = new System.Drawing.Point(238, 91);
            this.customTextBoxPhoneExt.MaxLength = 5;
            this.customTextBoxPhoneExt.Name = "customTextBoxPhoneExt";
            this.customTextBoxPhoneExt.Size = new System.Drawing.Size(54, 21);
            this.customTextBoxPhoneExt.TabIndex = 7;
            this.customTextBoxPhoneExt.ValidationExpression = "";
            // 
            // customTextBoxPhoneAreaCode
            // 
            this.customTextBoxPhoneAreaCode.AllowOnlyNumbers = true;
            this.customTextBoxPhoneAreaCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxPhoneAreaCode.CausesValidation = false;
            this.customTextBoxPhoneAreaCode.ErrorMessage = "";
            this.customTextBoxPhoneAreaCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxPhoneAreaCode.Location = new System.Drawing.Point(116, 91);
            this.customTextBoxPhoneAreaCode.MaxLength = 3;
            this.customTextBoxPhoneAreaCode.Name = "customTextBoxPhoneAreaCode";
            this.customTextBoxPhoneAreaCode.Required = true;
            this.customTextBoxPhoneAreaCode.Size = new System.Drawing.Size(27, 21);
            this.customTextBoxPhoneAreaCode.TabIndex = 5;
            this.customTextBoxPhoneAreaCode.ValidationExpression = "";
            this.customTextBoxPhoneAreaCode.Leave += new System.EventHandler(this.customTextBoxPhoneAreaCode_Leave);
            // 
            // customTextBoxPhoneNumber
            // 
            this.customTextBoxPhoneNumber.AllowOnlyNumbers = true;
            this.customTextBoxPhoneNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxPhoneNumber.CausesValidation = false;
            this.customTextBoxPhoneNumber.ErrorMessage = "";
            this.customTextBoxPhoneNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxPhoneNumber.FormatAsPhone = true;
            this.customTextBoxPhoneNumber.Location = new System.Drawing.Point(149, 91);
            this.customTextBoxPhoneNumber.MaxLength = 8;
            this.customTextBoxPhoneNumber.Name = "customTextBoxPhoneNumber";
            this.customTextBoxPhoneNumber.Required = true;
            this.customTextBoxPhoneNumber.Size = new System.Drawing.Size(54, 21);
            this.customTextBoxPhoneNumber.TabIndex = 6;
            this.customTextBoxPhoneNumber.ValidationExpression = "";
            this.customTextBoxPhoneNumber.Leave += new System.EventHandler(this.customTextBoxPhoneNumber_Leave);
            // 
            // customTextBoxOfficerFirstName
            // 
            this.customTextBoxOfficerFirstName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxOfficerFirstName.CausesValidation = false;
            this.tableLayoutPanelOfficerInfo.SetColumnSpan(this.customTextBoxOfficerFirstName, 2);
            this.customTextBoxOfficerFirstName.ErrorMessage = "";
            this.customTextBoxOfficerFirstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxOfficerFirstName.Location = new System.Drawing.Point(116, 3);
            this.customTextBoxOfficerFirstName.MaxLength = 20;
            this.customTextBoxOfficerFirstName.Name = "customTextBoxOfficerFirstName";
            this.customTextBoxOfficerFirstName.Required = true;
            this.customTextBoxOfficerFirstName.Size = new System.Drawing.Size(87, 21);
            this.customTextBoxOfficerFirstName.TabIndex = 2;
            this.customTextBoxOfficerFirstName.ValidationExpression = "";
            // 
            // customTextBoxOfficerLastName
            // 
            this.customTextBoxOfficerLastName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxOfficerLastName.CausesValidation = false;
            this.tableLayoutPanelOfficerInfo.SetColumnSpan(this.customTextBoxOfficerLastName, 2);
            this.customTextBoxOfficerLastName.ErrorMessage = "";
            this.customTextBoxOfficerLastName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxOfficerLastName.Location = new System.Drawing.Point(116, 29);
            this.customTextBoxOfficerLastName.MaxLength = 20;
            this.customTextBoxOfficerLastName.Name = "customTextBoxOfficerLastName";
            this.customTextBoxOfficerLastName.Required = true;
            this.customTextBoxOfficerLastName.Size = new System.Drawing.Size(87, 21);
            this.customTextBoxOfficerLastName.TabIndex = 3;
            this.customTextBoxOfficerLastName.ValidationExpression = "";
            // 
            // customTextBoxBadgeNumber
            // 
            this.customTextBoxBadgeNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxBadgeNumber.CausesValidation = false;
            this.tableLayoutPanelOfficerInfo.SetColumnSpan(this.customTextBoxBadgeNumber, 2);
            this.customTextBoxBadgeNumber.ErrorMessage = "";
            this.customTextBoxBadgeNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxBadgeNumber.Location = new System.Drawing.Point(116, 58);
            this.customTextBoxBadgeNumber.MaxLength = 10;
            this.customTextBoxBadgeNumber.Name = "customTextBoxBadgeNumber";
            this.customTextBoxBadgeNumber.Required = true;
            this.customTextBoxBadgeNumber.Size = new System.Drawing.Size(87, 21);
            this.customTextBoxBadgeNumber.TabIndex = 4;
            this.customTextBoxBadgeNumber.ValidationExpression = "";
            // 
            // customTextBoxAgency
            // 
            this.customTextBoxAgency.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxAgency.CausesValidation = false;
            this.customTextBoxAgency.ErrorMessage = "";
            this.customTextBoxAgency.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxAgency.Location = new System.Drawing.Point(416, 3);
            this.customTextBoxAgency.MaxLength = 15;
            this.customTextBoxAgency.Name = "customTextBoxAgency";
            this.customTextBoxAgency.Required = true;
            this.customTextBoxAgency.Size = new System.Drawing.Size(96, 21);
            this.customTextBoxAgency.TabIndex = 8;
            this.customTextBoxAgency.ValidationExpression = "";
            // 
            // customTextBoxCaseNumber
            // 
            this.customTextBoxCaseNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxCaseNumber.CausesValidation = false;
            this.customTextBoxCaseNumber.ErrorMessage = "";
            this.customTextBoxCaseNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCaseNumber.Location = new System.Drawing.Point(416, 29);
            this.customTextBoxCaseNumber.MaxLength = 15;
            this.customTextBoxCaseNumber.Name = "customTextBoxCaseNumber";
            this.customTextBoxCaseNumber.Size = new System.Drawing.Size(96, 21);
            this.customTextBoxCaseNumber.TabIndex = 9;
            this.customTextBoxCaseNumber.ValidationExpression = "";
            // 
            // comboBoxReqType
            // 
            this.comboBoxReqType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxReqType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReqType.FormattingEnabled = true;
            this.comboBoxReqType.Items.AddRange(new object[] {
            "Phone",
            "Written",
            "In Person"});
            this.comboBoxReqType.Location = new System.Drawing.Point(416, 58);
            this.comboBoxReqType.Name = "comboBoxReqType";
            this.comboBoxReqType.Size = new System.Drawing.Size(102, 21);
            this.comboBoxReqType.TabIndex = 10;
            // 
            // labelExt
            // 
            this.labelExt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelExt.AutoSize = true;
            this.labelExt.BackColor = System.Drawing.Color.Transparent;
            this.labelExt.Location = new System.Drawing.Point(209, 95);
            this.labelExt.Name = "labelExt";
            this.labelExt.Size = new System.Drawing.Size(22, 13);
            this.labelExt.TabIndex = 60;
            this.labelExt.Text = "Ext";
            // 
            // labelOfficerFirstName
            // 
            this.labelOfficerFirstName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelOfficerFirstName.AutoSize = true;
            this.labelOfficerFirstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOfficerFirstName.Location = new System.Drawing.Point(9, 6);
            this.labelOfficerFirstName.Name = "labelOfficerFirstName";
            this.labelOfficerFirstName.Required = true;
            this.labelOfficerFirstName.Size = new System.Drawing.Size(94, 13);
            this.labelOfficerFirstName.TabIndex = 67;
            this.labelOfficerFirstName.Text = "Officer First Name";
            // 
            // customLabelOfficerLastName
            // 
            this.customLabelOfficerLastName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.customLabelOfficerLastName.AutoSize = true;
            this.customLabelOfficerLastName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelOfficerLastName.Location = new System.Drawing.Point(10, 33);
            this.customLabelOfficerLastName.Name = "customLabelOfficerLastName";
            this.customLabelOfficerLastName.Required = true;
            this.customLabelOfficerLastName.Size = new System.Drawing.Size(93, 13);
            this.customLabelOfficerLastName.TabIndex = 68;
            this.customLabelOfficerLastName.Text = "Officer Last Name";
            // 
            // customLabelBadgeNumber
            // 
            this.customLabelBadgeNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.customLabelBadgeNumber.AutoSize = true;
            this.customLabelBadgeNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelBadgeNumber.Location = new System.Drawing.Point(12, 62);
            this.customLabelBadgeNumber.Name = "customLabelBadgeNumber";
            this.customLabelBadgeNumber.Required = true;
            this.customLabelBadgeNumber.Size = new System.Drawing.Size(89, 13);
            this.customLabelBadgeNumber.TabIndex = 69;
            this.customLabelBadgeNumber.Text = "Badge Number    ";
            // 
            // customLabel1
            // 
            this.customLabel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.customLabel1.AutoSize = true;
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.Location = new System.Drawing.Point(13, 95);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Required = true;
            this.customLabel1.Size = new System.Drawing.Size(86, 13);
            this.customLabel1.TabIndex = 70;
            this.customLabel1.Text = "Phone Number   ";
            // 
            // customLabelAgency
            // 
            this.customLabelAgency.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelAgency.AutoSize = true;
            this.customLabelAgency.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelAgency.Location = new System.Drawing.Point(305, 6);
            this.customLabelAgency.Name = "customLabelAgency";
            this.customLabelAgency.Required = true;
            this.customLabelAgency.Size = new System.Drawing.Size(43, 13);
            this.customLabelAgency.TabIndex = 71;
            this.customLabelAgency.Text = "Agency";
            // 
            // customLabelReqType
            // 
            this.customLabelReqType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelReqType.AutoSize = true;
            this.customLabelReqType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelReqType.Location = new System.Drawing.Point(305, 62);
            this.customLabelReqType.Name = "customLabelReqType";
            this.customLabelReqType.Required = true;
            this.customLabelReqType.Size = new System.Drawing.Size(74, 13);
            this.customLabelReqType.TabIndex = 72;
            this.customLabelReqType.Text = "Request Type";
            // 
            // dateCalendarRelease
            // 
            this.dateCalendarRelease.AllowKeyUpAndDown = false;
            this.dateCalendarRelease.AllowMonthlySelection = false;
            this.dateCalendarRelease.AllowWeekends = true;
            this.dateCalendarRelease.AutoSize = true;
            this.dateCalendarRelease.BackColor = System.Drawing.Color.Transparent;
            this.dateCalendarRelease.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateCalendarRelease.Location = new System.Drawing.Point(124, 105);
            this.dateCalendarRelease.Name = "dateCalendarRelease";
            this.dateCalendarRelease.PositionPopupCalendarOverTextbox = false;
            this.dateCalendarRelease.SelectedDate = "mm/dd/yyyy";
            this.dateCalendarRelease.Size = new System.Drawing.Size(142, 28);
            this.dateCalendarRelease.TabIndex = 1;
            this.dateCalendarRelease.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.dateCalendarRelease.Leave += new System.EventHandler(this.dateCalendarRelease_Leave);
            // 
            // labelReason
            // 
            this.labelReason.AutoSize = true;
            this.labelReason.BackColor = System.Drawing.Color.Transparent;
            this.labelReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReason.Location = new System.Drawing.Point(16, 328);
            this.labelReason.Name = "labelReason";
            this.labelReason.Size = new System.Drawing.Size(99, 13);
            this.labelReason.TabIndex = 3;
            this.labelReason.Text = "Reason for Hold";
            // 
            // labelAsterisk
            // 
            this.labelAsterisk.AutoSize = true;
            this.labelAsterisk.BackColor = System.Drawing.Color.Transparent;
            this.labelAsterisk.ForeColor = System.Drawing.Color.Red;
            this.labelAsterisk.Location = new System.Drawing.Point(121, 328);
            this.labelAsterisk.Name = "labelAsterisk";
            this.labelAsterisk.Size = new System.Drawing.Size(11, 13);
            this.labelAsterisk.TabIndex = 59;
            this.labelAsterisk.Text = "*";
            // 
            // customLabelReleaseEligible
            // 
            this.customLabelReleaseEligible.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.customLabelReleaseEligible.AutoSize = true;
            this.customLabelReleaseEligible.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelReleaseEligible.Location = new System.Drawing.Point(12, 110);
            this.customLabelReleaseEligible.Name = "customLabelReleaseEligible";
            this.customLabelReleaseEligible.Required = true;
            this.customLabelReleaseEligible.Size = new System.Drawing.Size(99, 13);
            this.customLabelReleaseEligible.TabIndex = 68;
            this.customLabelReleaseEligible.Text = "Eligible For Release";
            // 
            // PoliceHoldInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_400_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(703, 627);
            this.ControlBox = false;
            this.Controls.Add(this.customLabelReleaseEligible);
            this.Controls.Add(this.labelAsterisk);
            this.Controls.Add(this.dateCalendarRelease);
            this.Controls.Add(this.tableLayoutPanelOfficerInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.richTextBoxReason);
            this.Controls.Add(this.labelReason);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PoliceHoldInfo";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomerHoldInfo";
            this.Load += new System.EventHandler(this.PoliceHoldInfo_Load);
            this.tableLayoutPanelOfficerInfo.ResumeLayout(false);
            this.tableLayoutPanelOfficerInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBoxReason;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelOfficerInfo;
        private System.Windows.Forms.Label labelCaseNumber;
        private CustomTextBox customTextBoxPhoneExt;
        private System.Windows.Forms.Label labelExt;
        private CustomTextBox customTextBoxPhoneAreaCode;
        private CustomTextBox customTextBoxPhoneNumber;
        private CustomTextBox customTextBoxOfficerFirstName;
        private CustomTextBox customTextBoxOfficerLastName;
        private CustomTextBox customTextBoxBadgeNumber;
        private CustomTextBox customTextBoxAgency;
        private CustomTextBox customTextBoxCaseNumber;
        private System.Windows.Forms.ComboBox comboBoxReqType;
        private DateCalendar dateCalendarRelease;
        private CustomLabel labelOfficerFirstName;
        private CustomLabel customLabelOfficerLastName;
        private CustomLabel customLabelBadgeNumber;
        private CustomLabel customLabel1;
        private CustomLabel customLabelAgency;
        private CustomLabel customLabelReqType;
        private System.Windows.Forms.Label labelReason;
        private System.Windows.Forms.Label labelAsterisk;
        private CustomLabel customLabelReleaseEligible;
    }
}
