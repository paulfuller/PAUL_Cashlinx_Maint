using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Customer.ItemRelease
{
    partial class PoliceHoldRelease
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PoliceHoldRelease));
            this.labelHeading = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelReason = new System.Windows.Forms.Label();
            this.richTextBoxReason = new System.Windows.Forms.RichTextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.customLabelBadgeNumber = new Common.Libraries.Forms.Components.CustomLabel();
            this.customTextBoxOfficerFirstName = new Common.Libraries.Forms.Components.CustomTextBox();
            this.customTextBoxOfficerLastName = new Common.Libraries.Forms.Components.CustomTextBox();
            this.customTextBoxBadgeNumber = new Common.Libraries.Forms.Components.CustomTextBox();
            this.customTextBoxAgency = new Common.Libraries.Forms.Components.CustomTextBox();
            this.customTextBoxCaseNumber = new Common.Libraries.Forms.Components.CustomTextBox();
            this.customTextBoxPhoneAreaCode = new Common.Libraries.Forms.Components.CustomTextBox();
            this.customTextBoxPhoneNumber = new Common.Libraries.Forms.Components.CustomTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.customTextBoxPhoneExt = new Common.Libraries.Forms.Components.CustomTextBox();
            this.labelOfficerFirstName = new Common.Libraries.Forms.Components.CustomLabel();
            this.customLabelOfficerLastName = new Common.Libraries.Forms.Components.CustomLabel();
            this.customLabelAgency = new Common.Libraries.Forms.Components.CustomLabel();
            this.customLabel1 = new Common.Libraries.Forms.Components.CustomLabel();
            this.customLabelPhone = new Common.Libraries.Forms.Components.CustomLabel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButtonNo = new System.Windows.Forms.RadioButton();
            this.radioButtonYes = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.labelRestitution = new System.Windows.Forms.Label();
            this.labelCurrency = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelRestitution = new System.Windows.Forms.Panel();
            this.customTextBoxResAmount = new Common.Libraries.Forms.Components.CustomTextBox();
            this.labelAsterisk = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelRestitution.SuspendLayout();
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
            this.labelHeading.Size = new System.Drawing.Size(104, 16);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Release Hold";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(3, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(693, 2);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // labelReason
            // 
            this.labelReason.AutoSize = true;
            this.labelReason.BackColor = System.Drawing.Color.Transparent;
            this.labelReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReason.Location = new System.Drawing.Point(16, 299);
            this.labelReason.Name = "labelReason";
            this.labelReason.Size = new System.Drawing.Size(164, 13);
            this.labelReason.TabIndex = 3;
            this.labelReason.Text = "Reason for Release of Hold";
            // 
            // richTextBoxReason
            // 
            this.richTextBoxReason.Location = new System.Drawing.Point(19, 325);
            this.richTextBoxReason.MaxLength = 256;
            this.richTextBoxReason.Name = "richTextBoxReason";
            this.richTextBoxReason.Size = new System.Drawing.Size(652, 156);
            this.richTextBoxReason.TabIndex = 9;
            this.richTextBoxReason.Text = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonCancel.BackgroundImage")));
            this.buttonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.Location = new System.Drawing.Point(13, 609);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 50);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.Transparent;
            this.buttonBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonBack.BackgroundImage")));
            this.buttonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonBack.CausesValidation = false;
            this.buttonBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonBack.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.buttonBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.ForeColor = System.Drawing.Color.White;
            this.buttonBack.Location = new System.Drawing.Point(122, 609);
            this.buttonBack.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonBack.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonBack.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(100, 50);
            this.buttonBack.TabIndex = 14;
            this.buttonBack.Text = "&Back";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.BackColor = System.Drawing.Color.Transparent;
            this.buttonSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonSubmit.BackgroundImage")));
            this.buttonSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonSubmit.CausesValidation = false;
            this.buttonSubmit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonSubmit.FlatAppearance.BorderSize = 0;
            this.buttonSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSubmit.ForeColor = System.Drawing.Color.White;
            this.buttonSubmit.Location = new System.Drawing.Point(571, 610);
            this.buttonSubmit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(100, 50);
            this.buttonSubmit.TabIndex = 13;
            this.buttonSubmit.Text = "&Submit";
            this.buttonSubmit.UseVisualStyleBackColor = false;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.DarkBlue;
            this.groupBox3.Location = new System.Drawing.Point(5, 285);
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
            this.label1.Location = new System.Drawing.Point(16, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "Law Enforcement Information";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 189F));
            this.tableLayoutPanel1.Controls.Add(this.customLabelBadgeNumber, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.customTextBoxOfficerFirstName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.customTextBoxOfficerLastName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.customTextBoxBadgeNumber, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.customTextBoxAgency, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.customTextBoxCaseNumber, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.customTextBoxPhoneAreaCode, 6, 2);
            this.tableLayoutPanel1.Controls.Add(this.customTextBoxPhoneNumber, 7, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 8, 2);
            this.tableLayoutPanel1.Controls.Add(this.customTextBoxPhoneExt, 9, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelOfficerFirstName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelOfficerLastName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelAgency, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabel1, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelPhone, 5, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(19, 140);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(672, 110);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // customLabelBadgeNumber
            // 
            this.customLabelBadgeNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelBadgeNumber.AutoSize = true;
            this.customLabelBadgeNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelBadgeNumber.Location = new System.Drawing.Point(3, 85);
            this.customLabelBadgeNumber.Name = "customLabelBadgeNumber";
            this.customLabelBadgeNumber.Required = true;
            this.customLabelBadgeNumber.Size = new System.Drawing.Size(77, 13);
            this.customLabelBadgeNumber.TabIndex = 70;
            this.customLabelBadgeNumber.Text = "Badge Number";
            // 
            // customTextBoxOfficerFirstName
            // 
            this.customTextBoxOfficerFirstName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxOfficerFirstName.CausesValidation = false;
            this.tableLayoutPanel1.SetColumnSpan(this.customTextBoxOfficerFirstName, 2);
            this.customTextBoxOfficerFirstName.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxOfficerFirstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxOfficerFirstName.Location = new System.Drawing.Point(119, 8);
            this.customTextBoxOfficerFirstName.MaxLength = 20;
            this.customTextBoxOfficerFirstName.Name = "customTextBoxOfficerFirstName";
            this.customTextBoxOfficerFirstName.Required = true;
            this.customTextBoxOfficerFirstName.Size = new System.Drawing.Size(96, 21);
            this.customTextBoxOfficerFirstName.TabIndex = 1;
            this.customTextBoxOfficerFirstName.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customTextBoxOfficerLastName
            // 
            this.customTextBoxOfficerLastName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxOfficerLastName.CausesValidation = false;
            this.tableLayoutPanel1.SetColumnSpan(this.customTextBoxOfficerLastName, 2);
            this.customTextBoxOfficerLastName.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxOfficerLastName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxOfficerLastName.Location = new System.Drawing.Point(119, 45);
            this.customTextBoxOfficerLastName.MaxLength = 20;
            this.customTextBoxOfficerLastName.Name = "customTextBoxOfficerLastName";
            this.customTextBoxOfficerLastName.Required = true;
            this.customTextBoxOfficerLastName.Size = new System.Drawing.Size(96, 21);
            this.customTextBoxOfficerLastName.TabIndex = 2;
            this.customTextBoxOfficerLastName.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customTextBoxBadgeNumber
            // 
            this.customTextBoxBadgeNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxBadgeNumber.CausesValidation = false;
            this.tableLayoutPanel1.SetColumnSpan(this.customTextBoxBadgeNumber, 2);
            this.customTextBoxBadgeNumber.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxBadgeNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxBadgeNumber.Location = new System.Drawing.Point(119, 81);
            this.customTextBoxBadgeNumber.MaxLength = 10;
            this.customTextBoxBadgeNumber.Name = "customTextBoxBadgeNumber";
            this.customTextBoxBadgeNumber.Required = true;
            this.customTextBoxBadgeNumber.Size = new System.Drawing.Size(96, 21);
            this.customTextBoxBadgeNumber.TabIndex = 3;
            this.customTextBoxBadgeNumber.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customTextBoxAgency
            // 
            this.customTextBoxAgency.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxAgency.CausesValidation = false;
            this.tableLayoutPanel1.SetColumnSpan(this.customTextBoxAgency, 2);
            this.customTextBoxAgency.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxAgency.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxAgency.Location = new System.Drawing.Point(396, 8);
            this.customTextBoxAgency.MaxLength = 15;
            this.customTextBoxAgency.Name = "customTextBoxAgency";
            this.customTextBoxAgency.Required = true;
            this.customTextBoxAgency.Size = new System.Drawing.Size(96, 21);
            this.customTextBoxAgency.TabIndex = 4;
            this.customTextBoxAgency.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customTextBoxCaseNumber
            // 
            this.customTextBoxCaseNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxCaseNumber.CausesValidation = false;
            this.tableLayoutPanel1.SetColumnSpan(this.customTextBoxCaseNumber, 2);
            this.customTextBoxCaseNumber.ErrorMessage = "*";
            this.customTextBoxCaseNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCaseNumber.Location = new System.Drawing.Point(396, 45);
            this.customTextBoxCaseNumber.MaxLength = 15;
            this.customTextBoxCaseNumber.Name = "customTextBoxCaseNumber";
            this.customTextBoxCaseNumber.Required = true;
            this.customTextBoxCaseNumber.Size = new System.Drawing.Size(96, 21);
            this.customTextBoxCaseNumber.TabIndex = 5;
            this.customTextBoxCaseNumber.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customTextBoxPhoneAreaCode
            // 
            this.customTextBoxPhoneAreaCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxPhoneAreaCode.CausesValidation = false;
            this.customTextBoxPhoneAreaCode.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxPhoneAreaCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxPhoneAreaCode.Location = new System.Drawing.Point(396, 81);
            this.customTextBoxPhoneAreaCode.MaxLength = 3;
            this.customTextBoxPhoneAreaCode.Name = "customTextBoxPhoneAreaCode";
            this.customTextBoxPhoneAreaCode.Required = true;
            this.customTextBoxPhoneAreaCode.Size = new System.Drawing.Size(29, 21);
            this.customTextBoxPhoneAreaCode.TabIndex = 6;
            this.customTextBoxPhoneAreaCode.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customTextBoxPhoneNumber
            // 
            this.customTextBoxPhoneNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxPhoneNumber.CausesValidation = false;
            this.customTextBoxPhoneNumber.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxPhoneNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxPhoneNumber.Location = new System.Drawing.Point(431, 81);
            this.customTextBoxPhoneNumber.MaxLength = 7;
            this.customTextBoxPhoneNumber.Name = "customTextBoxPhoneNumber";
            this.customTextBoxPhoneNumber.Required = true;
            this.customTextBoxPhoneNumber.Size = new System.Drawing.Size(59, 21);
            this.customTextBoxPhoneNumber.TabIndex = 7;
            this.customTextBoxPhoneNumber.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(501, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 60;
            this.label5.Text = "Ext";
            // 
            // customTextBoxPhoneExt
            // 
            this.customTextBoxPhoneExt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxPhoneExt.CausesValidation = false;
            this.customTextBoxPhoneExt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxPhoneExt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxPhoneExt.Location = new System.Drawing.Point(530, 81);
            this.customTextBoxPhoneExt.MaxLength = 5;
            this.customTextBoxPhoneExt.Name = "customTextBoxPhoneExt";
            this.customTextBoxPhoneExt.Size = new System.Drawing.Size(54, 21);
            this.customTextBoxPhoneExt.TabIndex = 8;
            this.customTextBoxPhoneExt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // labelOfficerFirstName
            // 
            this.labelOfficerFirstName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelOfficerFirstName.AutoSize = true;
            this.labelOfficerFirstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOfficerFirstName.Location = new System.Drawing.Point(3, 12);
            this.labelOfficerFirstName.Name = "labelOfficerFirstName";
            this.labelOfficerFirstName.Required = true;
            this.labelOfficerFirstName.Size = new System.Drawing.Size(94, 13);
            this.labelOfficerFirstName.TabIndex = 68;
            this.labelOfficerFirstName.Text = "Officer First Name";
            // 
            // customLabelOfficerLastName
            // 
            this.customLabelOfficerLastName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelOfficerLastName.AutoSize = true;
            this.customLabelOfficerLastName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelOfficerLastName.Location = new System.Drawing.Point(3, 49);
            this.customLabelOfficerLastName.Name = "customLabelOfficerLastName";
            this.customLabelOfficerLastName.Required = true;
            this.customLabelOfficerLastName.Size = new System.Drawing.Size(93, 13);
            this.customLabelOfficerLastName.TabIndex = 69;
            this.customLabelOfficerLastName.Text = "Officer Last Name";
            // 
            // customLabelAgency
            // 
            this.customLabelAgency.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelAgency.AutoSize = true;
            this.customLabelAgency.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelAgency.Location = new System.Drawing.Point(303, 12);
            this.customLabelAgency.Name = "customLabelAgency";
            this.customLabelAgency.Required = true;
            this.customLabelAgency.Size = new System.Drawing.Size(43, 13);
            this.customLabelAgency.TabIndex = 71;
            this.customLabelAgency.Text = "Agency";
            // 
            // customLabel1
            // 
            this.customLabel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabel1.AutoSize = true;
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.Location = new System.Drawing.Point(303, 49);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Required = true;
            this.customLabel1.Size = new System.Drawing.Size(71, 13);
            this.customLabel1.TabIndex = 73;
            this.customLabel1.Text = "Case Number";
            // 
            // customLabelPhone
            // 
            this.customLabelPhone.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelPhone.AutoSize = true;
            this.customLabelPhone.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelPhone.Location = new System.Drawing.Point(303, 85);
            this.customLabelPhone.Name = "customLabelPhone";
            this.customLabelPhone.Required = true;
            this.customLabelPhone.Size = new System.Drawing.Size(77, 13);
            this.customLabelPhone.TabIndex = 72;
            this.customLabelPhone.Text = "Phone Number";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.DarkBlue;
            this.groupBox4.Location = new System.Drawing.Point(0, 7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(693, 2);
            this.groupBox4.TabIndex = 59;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "groupBox4";
            // 
            // radioButtonNo
            // 
            this.radioButtonNo.AutoSize = true;
            this.radioButtonNo.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonNo.Location = new System.Drawing.Point(90, 47);
            this.radioButtonNo.Name = "radioButtonNo";
            this.radioButtonNo.Size = new System.Drawing.Size(39, 17);
            this.radioButtonNo.TabIndex = 11;
            this.radioButtonNo.TabStop = true;
            this.radioButtonNo.Text = "No";
            this.radioButtonNo.UseVisualStyleBackColor = false;
            this.radioButtonNo.CheckedChanged += new System.EventHandler(this.radioButtonNo_CheckedChanged);
            // 
            // radioButtonYes
            // 
            this.radioButtonYes.AutoSize = true;
            this.radioButtonYes.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonYes.Location = new System.Drawing.Point(31, 47);
            this.radioButtonYes.Name = "radioButtonYes";
            this.radioButtonYes.Size = new System.Drawing.Size(43, 17);
            this.radioButtonYes.TabIndex = 10;
            this.radioButtonYes.TabStop = true;
            this.radioButtonYes.Text = "Yes";
            this.radioButtonYes.UseVisualStyleBackColor = false;
            this.radioButtonYes.CheckedChanged += new System.EventHandler(this.radioButtonYes_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(325, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 63;
            this.label2.Text = "Restitution Amount:";
            // 
            // labelRestitution
            // 
            this.labelRestitution.AutoSize = true;
            this.labelRestitution.BackColor = System.Drawing.Color.Transparent;
            this.labelRestitution.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRestitution.Location = new System.Drawing.Point(28, 20);
            this.labelRestitution.Name = "labelRestitution";
            this.labelRestitution.Size = new System.Drawing.Size(129, 13);
            this.labelRestitution.TabIndex = 60;
            this.labelRestitution.Text = "Restitution Collected:";
            // 
            // labelCurrency
            // 
            this.labelCurrency.AutoSize = true;
            this.labelCurrency.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrency.Location = new System.Drawing.Point(328, 49);
            this.labelCurrency.Name = "labelCurrency";
            this.labelCurrency.Size = new System.Drawing.Size(13, 13);
            this.labelCurrency.TabIndex = 64;
            this.labelCurrency.Text = "$";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.DarkBlue;
            this.groupBox2.Location = new System.Drawing.Point(5, 600);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(693, 2);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // panelRestitution
            // 
            this.panelRestitution.BackColor = System.Drawing.Color.Transparent;
            this.panelRestitution.Controls.Add(this.label2);
            this.panelRestitution.Controls.Add(this.labelCurrency);
            this.panelRestitution.Controls.Add(this.customTextBoxResAmount);
            this.panelRestitution.Controls.Add(this.labelRestitution);
            this.panelRestitution.Controls.Add(this.radioButtonYes);
            this.panelRestitution.Controls.Add(this.groupBox4);
            this.panelRestitution.Controls.Add(this.radioButtonNo);
            this.panelRestitution.Location = new System.Drawing.Point(3, 488);
            this.panelRestitution.Name = "panelRestitution";
            this.panelRestitution.Size = new System.Drawing.Size(695, 100);
            this.panelRestitution.TabIndex = 66;
            this.panelRestitution.Visible = false;
            // 
            // customTextBoxResAmount
            // 
            this.customTextBoxResAmount.CausesValidation = false;
            this.customTextBoxResAmount.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxResAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxResAmount.Location = new System.Drawing.Point(348, 49);
            this.customTextBoxResAmount.Name = "customTextBoxResAmount";
            this.customTextBoxResAmount.Size = new System.Drawing.Size(82, 21);
            this.customTextBoxResAmount.TabIndex = 12;
            this.customTextBoxResAmount.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // labelAsterisk
            // 
            this.labelAsterisk.AutoSize = true;
            this.labelAsterisk.BackColor = System.Drawing.Color.Transparent;
            this.labelAsterisk.ForeColor = System.Drawing.Color.Red;
            this.labelAsterisk.Location = new System.Drawing.Point(10, 299);
            this.labelAsterisk.Name = "labelAsterisk";
            this.labelAsterisk.Size = new System.Drawing.Size(11, 13);
            this.labelAsterisk.TabIndex = 73;
            this.labelAsterisk.Text = "*";
            // 
            // PoliceHoldRelease
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(703, 672);
            this.ControlBox = false;
            this.Controls.Add(this.labelAsterisk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panelRestitution);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.richTextBoxReason);
            this.Controls.Add(this.labelReason);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PoliceHoldRelease";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomerHoldInfo";
            this.Load += new System.EventHandler(this.PoliceHoldInfo_Load);
            this.Shown += new System.EventHandler(this.PoliceHoldReleaseInfo_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelRestitution.ResumeLayout(false);
            this.panelRestitution.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelReason;
        private System.Windows.Forms.RichTextBox richTextBoxReason;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomTextBox customTextBoxPhoneExt;
        private System.Windows.Forms.Label label5;
        private CustomTextBox customTextBoxPhoneAreaCode;
        private CustomTextBox customTextBoxPhoneNumber;
        private CustomTextBox customTextBoxOfficerFirstName;
        private CustomTextBox customTextBoxOfficerLastName;
        private CustomTextBox customTextBoxBadgeNumber;
        private CustomTextBox customTextBoxAgency;
        private CustomTextBox customTextBoxCaseNumber;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButtonNo;
        private System.Windows.Forms.RadioButton radioButtonYes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelRestitution;
        private System.Windows.Forms.Label labelCurrency;
        private CustomTextBox customTextBoxResAmount;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panelRestitution;
        private CustomLabel customLabelBadgeNumber;
        private CustomLabel labelOfficerFirstName;
        private CustomLabel customLabelOfficerLastName;
        private CustomLabel customLabelAgency;
        private CustomLabel customLabelPhone;
        private System.Windows.Forms.Label labelAsterisk;
        private CustomLabel customLabel1;
    }
}
