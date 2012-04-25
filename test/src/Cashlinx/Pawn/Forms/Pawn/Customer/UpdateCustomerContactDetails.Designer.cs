using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.Customer
{
    partial class UpdateCustomerContactDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateCustomerContactDetails));
            this.labelContactDetailsHeading = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelAlternateEmail = new System.Windows.Forms.Label();
            this.labelEmailHeading = new System.Windows.Forms.Label();
            this.labelPrimaryEmail = new System.Windows.Forms.Label();
            this.primaryEmailTextBox = new CustomTextBox();
            this.alternateEmailTextBox = new CustomTextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.labelContactPermissions = new System.Windows.Forms.Label();
            this.checkBoxNoCall = new System.Windows.Forms.CheckBox();
            this.checkBoxNoEmail = new System.Windows.Forms.CheckBox();
            this.checkBoxNoFax = new System.Windows.Forms.CheckBox();
            this.checkBoxNoMail = new System.Windows.Forms.CheckBox();
            this.checkBoxOptOut = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.labelPaymentReminder = new System.Windows.Forms.Label();
            this.checkBoxReminder = new System.Windows.Forms.CheckBox();
            this.labelPrefContact = new System.Windows.Forms.Label();
            this.labelPrefCallTime = new System.Windows.Forms.Label();
            this.labelHowdidyouhear = new System.Windows.Forms.Label();
            this.labelReceiveOffers = new System.Windows.Forms.Label();
            this.comboBoxReceivePromotions = new System.Windows.Forms.ComboBox();
            this.comboBoxPrefContact = new System.Windows.Forms.ComboBox();
            this.comboBoxPrefCallTime = new System.Windows.Forms.ComboBox();
            this.hearAboutUs1 = new HearAboutUs();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.phoneData1 = new PhoneData();
            this.customButtonCancel = new CustomButton();
            this.customButtonReset = new CustomButton();
            this.customButtonSubmit = new CustomButton();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelContactDetailsHeading
            // 
            this.labelContactDetailsHeading.AutoSize = true;
            this.labelContactDetailsHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelContactDetailsHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelContactDetailsHeading.ForeColor = System.Drawing.Color.White;
            this.labelContactDetailsHeading.Location = new System.Drawing.Point(12, 18);
            this.labelContactDetailsHeading.Name = "labelContactDetailsHeading";
            this.labelContactDetailsHeading.Size = new System.Drawing.Size(244, 16);
            this.labelContactDetailsHeading.TabIndex = 0;
            this.labelContactDetailsHeading.Text = "Enter Customer Contact Information";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.labelAlternateEmail, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.labelEmailHeading, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelPrimaryEmail, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.primaryEmailTextBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.alternateEmailTextBox, 1, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(13, 227);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(300, 72);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // labelAlternateEmail
            // 
            this.labelAlternateEmail.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAlternateEmail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAlternateEmail.Location = new System.Drawing.Point(3, 49);
            this.labelAlternateEmail.Name = "labelAlternateEmail";
            this.labelAlternateEmail.Size = new System.Drawing.Size(97, 14);
            this.labelAlternateEmail.TabIndex = 7;
            this.labelAlternateEmail.Text = "Alternate Email";
            // 
            // labelEmailHeading
            // 
            this.labelEmailHeading.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelEmailHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEmailHeading.Location = new System.Drawing.Point(3, 0);
            this.labelEmailHeading.Name = "labelEmailHeading";
            this.labelEmailHeading.Size = new System.Drawing.Size(97, 14);
            this.labelEmailHeading.TabIndex = 1;
            this.labelEmailHeading.Text = "Email Account";
            // 
            // labelPrimaryEmail
            // 
            this.labelPrimaryEmail.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPrimaryEmail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrimaryEmail.Location = new System.Drawing.Point(3, 20);
            this.labelPrimaryEmail.Name = "labelPrimaryEmail";
            this.labelPrimaryEmail.Size = new System.Drawing.Size(97, 14);
            this.labelPrimaryEmail.TabIndex = 6;
            this.labelPrimaryEmail.Text = "Primary Email";
            // 
            // primaryEmailTextBox
            // 
            this.primaryEmailTextBox.CausesValidation = false;
            this.primaryEmailTextBox.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.primaryEmailTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.primaryEmailTextBox.Location = new System.Drawing.Point(106, 17);
            this.primaryEmailTextBox.MaxLength = 255;
            this.primaryEmailTextBox.Name = "primaryEmailTextBox";
            this.primaryEmailTextBox.RegularExpression = true;
            this.primaryEmailTextBox.Size = new System.Drawing.Size(183, 21);
            this.primaryEmailTextBox.TabIndex = 24;
            this.primaryEmailTextBox.ValidationExpression = "(\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,6})";
            // 
            // alternateEmailTextBox
            // 
            this.alternateEmailTextBox.CausesValidation = false;
            this.alternateEmailTextBox.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.alternateEmailTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alternateEmailTextBox.Location = new System.Drawing.Point(106, 44);
            this.alternateEmailTextBox.MaxLength = 255;
            this.alternateEmailTextBox.Name = "alternateEmailTextBox";
            this.alternateEmailTextBox.RegularExpression = true;
            this.alternateEmailTextBox.Size = new System.Drawing.Size(183, 21);
            this.alternateEmailTextBox.TabIndex = 25;
            this.alternateEmailTextBox.ValidationExpression = "(\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,6})";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.labelContactPermissions, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxNoCall, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxNoEmail, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxNoFax, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxNoMail, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxOptOut, 4, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(352, 227);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(388, 72);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // labelContactPermissions
            // 
            this.labelContactPermissions.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel3.SetColumnSpan(this.labelContactPermissions, 7);
            this.labelContactPermissions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelContactPermissions.Location = new System.Drawing.Point(3, 6);
            this.labelContactPermissions.Name = "labelContactPermissions";
            this.labelContactPermissions.Size = new System.Drawing.Size(312, 23);
            this.labelContactPermissions.TabIndex = 2;
            this.labelContactPermissions.Text = "Contact Permissions";
            this.labelContactPermissions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxNoCall
            // 
            this.checkBoxNoCall.AutoSize = true;
            this.checkBoxNoCall.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxNoCall.Location = new System.Drawing.Point(3, 39);
            this.checkBoxNoCall.Name = "checkBoxNoCall";
            this.checkBoxNoCall.Size = new System.Drawing.Size(64, 17);
            this.checkBoxNoCall.TabIndex = 26;
            this.checkBoxNoCall.Text = "No Calls";
            this.checkBoxNoCall.UseVisualStyleBackColor = true;
            this.checkBoxNoCall.Leave += new System.EventHandler(this.checkBoxNoCall_Leave);
            this.checkBoxNoCall.Enter += new System.EventHandler(this.checkBoxNoCall_Enter);
            // 
            // checkBoxNoEmail
            // 
            this.checkBoxNoEmail.AutoSize = true;
            this.checkBoxNoEmail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxNoEmail.Location = new System.Drawing.Point(73, 39);
            this.checkBoxNoEmail.Name = "checkBoxNoEmail";
            this.checkBoxNoEmail.Size = new System.Drawing.Size(71, 17);
            this.checkBoxNoEmail.TabIndex = 27;
            this.checkBoxNoEmail.Text = "No Emails";
            this.checkBoxNoEmail.UseVisualStyleBackColor = true;
            this.checkBoxNoEmail.Leave += new System.EventHandler(this.checkBoxNoEmail_Leave);
            this.checkBoxNoEmail.Enter += new System.EventHandler(this.checkBoxNoEmail_Enter);
            // 
            // checkBoxNoFax
            // 
            this.checkBoxNoFax.AutoSize = true;
            this.checkBoxNoFax.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxNoFax.Location = new System.Drawing.Point(150, 39);
            this.checkBoxNoFax.Name = "checkBoxNoFax";
            this.checkBoxNoFax.Size = new System.Drawing.Size(71, 17);
            this.checkBoxNoFax.TabIndex = 28;
            this.checkBoxNoFax.Text = "No Faxes";
            this.checkBoxNoFax.UseVisualStyleBackColor = true;
            this.checkBoxNoFax.Leave += new System.EventHandler(this.checkBoxNoFax_Leave);
            this.checkBoxNoFax.Enter += new System.EventHandler(this.checkBoxNoFax_Enter);
            // 
            // checkBoxNoMail
            // 
            this.checkBoxNoMail.AutoSize = true;
            this.checkBoxNoMail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxNoMail.Location = new System.Drawing.Point(227, 39);
            this.checkBoxNoMail.Name = "checkBoxNoMail";
            this.checkBoxNoMail.Size = new System.Drawing.Size(60, 17);
            this.checkBoxNoMail.TabIndex = 29;
            this.checkBoxNoMail.Text = "No Mail";
            this.checkBoxNoMail.UseVisualStyleBackColor = true;
            this.checkBoxNoMail.Leave += new System.EventHandler(this.checkBoxNoMail_Leave);
            this.checkBoxNoMail.Enter += new System.EventHandler(this.checkBoxNoMail_Enter);
            // 
            // checkBoxOptOut
            // 
            this.checkBoxOptOut.AutoSize = true;
            this.checkBoxOptOut.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxOptOut.Location = new System.Drawing.Point(293, 39);
            this.checkBoxOptOut.Name = "checkBoxOptOut";
            this.checkBoxOptOut.Size = new System.Drawing.Size(65, 17);
            this.checkBoxOptOut.TabIndex = 30;
            this.checkBoxOptOut.Text = "Opt Out";
            this.checkBoxOptOut.UseVisualStyleBackColor = true;
            this.checkBoxOptOut.Leave += new System.EventHandler(this.checkBoxOptOut_Leave);
            this.checkBoxOptOut.Enter += new System.EventHandler(this.checkBoxOptOut_Enter);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel4.ColumnCount = 5;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134F));
            this.tableLayoutPanel4.Controls.Add(this.labelPaymentReminder, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.checkBoxReminder, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.labelPrefContact, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.labelPrefCallTime, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.labelHowdidyouhear, 3, 2);
            this.tableLayoutPanel4.Controls.Add(this.labelReceiveOffers, 3, 3);
            this.tableLayoutPanel4.Controls.Add(this.comboBoxReceivePromotions, 4, 3);
            this.tableLayoutPanel4.Controls.Add(this.comboBoxPrefContact, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.comboBoxPrefCallTime, 2, 3);
            this.tableLayoutPanel4.Controls.Add(this.hearAboutUs1, 4, 2);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(13, 306);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(541, 103);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // labelPaymentReminder
            // 
            this.labelPaymentReminder.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPaymentReminder.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPaymentReminder.Location = new System.Drawing.Point(3, 0);
            this.labelPaymentReminder.Name = "labelPaymentReminder";
            this.labelPaymentReminder.Size = new System.Drawing.Size(127, 16);
            this.labelPaymentReminder.TabIndex = 2;
            this.labelPaymentReminder.Text = "Payment Reminder";
            // 
            // checkBoxReminder
            // 
            this.checkBoxReminder.AutoSize = true;
            this.checkBoxReminder.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxReminder.Location = new System.Drawing.Point(3, 19);
            this.checkBoxReminder.Name = "checkBoxReminder";
            this.checkBoxReminder.Size = new System.Drawing.Size(128, 17);
            this.checkBoxReminder.TabIndex = 31;
            this.checkBoxReminder.Text = "Remind Payment Due";
            this.checkBoxReminder.UseVisualStyleBackColor = true;
            this.checkBoxReminder.Leave += new System.EventHandler(this.checkBoxReminder_Leave);
            this.checkBoxReminder.Enter += new System.EventHandler(this.checkBoxReminder_Enter);
            // 
            // labelPrefContact
            // 
            this.labelPrefContact.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPrefContact.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrefContact.Location = new System.Drawing.Point(3, 46);
            this.labelPrefContact.Name = "labelPrefContact";
            this.labelPrefContact.Size = new System.Drawing.Size(97, 14);
            this.labelPrefContact.TabIndex = 7;
            this.labelPrefContact.Text = "Preferred Contact";
            // 
            // labelPrefCallTime
            // 
            this.labelPrefCallTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPrefCallTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrefCallTime.Location = new System.Drawing.Point(3, 78);
            this.labelPrefCallTime.Name = "labelPrefCallTime";
            this.labelPrefCallTime.Size = new System.Drawing.Size(97, 14);
            this.labelPrefCallTime.TabIndex = 8;
            this.labelPrefCallTime.Text = "Preferred Call Time";
            // 
            // labelHowdidyouhear
            // 
            this.labelHowdidyouhear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelHowdidyouhear.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHowdidyouhear.Location = new System.Drawing.Point(240, 46);
            this.labelHowdidyouhear.Name = "labelHowdidyouhear";
            this.labelHowdidyouhear.Size = new System.Drawing.Size(144, 14);
            this.labelHowdidyouhear.TabIndex = 9;
            this.labelHowdidyouhear.Text = "How did you hear about us?";
            // 
            // labelReceiveOffers
            // 
            this.labelReceiveOffers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelReceiveOffers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReceiveOffers.Location = new System.Drawing.Point(240, 78);
            this.labelReceiveOffers.Name = "labelReceiveOffers";
            this.labelReceiveOffers.Size = new System.Drawing.Size(144, 14);
            this.labelReceiveOffers.TabIndex = 10;
            this.labelReceiveOffers.Text = "Receive Promotional Offers?";
            // 
            // comboBoxReceivePromotions
            // 
            this.comboBoxReceivePromotions.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxReceivePromotions.FormattingEnabled = true;
            this.comboBoxReceivePromotions.Items.AddRange(new object[] {
            "Select",
            "Yes",
            "No"});
            this.comboBoxReceivePromotions.Location = new System.Drawing.Point(410, 74);
            this.comboBoxReceivePromotions.Name = "comboBoxReceivePromotions";
            this.comboBoxReceivePromotions.Size = new System.Drawing.Size(53, 21);
            this.comboBoxReceivePromotions.TabIndex = 35;
            this.comboBoxReceivePromotions.Leave += new System.EventHandler(this.comboBoxReceivePromotions_Leave);
            this.comboBoxReceivePromotions.Enter += new System.EventHandler(this.comboBoxReceivePromotions_Enter);
            // 
            // comboBoxPrefContact
            // 
            this.comboBoxPrefContact.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxPrefContact.FormattingEnabled = true;
            this.comboBoxPrefContact.Location = new System.Drawing.Point(137, 42);
            this.comboBoxPrefContact.Name = "comboBoxPrefContact";
            this.comboBoxPrefContact.Size = new System.Drawing.Size(86, 21);
            this.comboBoxPrefContact.TabIndex = 32;
            this.comboBoxPrefContact.Leave += new System.EventHandler(this.comboBoxPrefContact_Leave);
            this.comboBoxPrefContact.Enter += new System.EventHandler(this.comboBoxPrefContact_Enter);
            // 
            // comboBoxPrefCallTime
            // 
            this.comboBoxPrefCallTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxPrefCallTime.FormattingEnabled = true;
            this.comboBoxPrefCallTime.Location = new System.Drawing.Point(137, 74);
            this.comboBoxPrefCallTime.Name = "comboBoxPrefCallTime";
            this.comboBoxPrefCallTime.Size = new System.Drawing.Size(86, 21);
            this.comboBoxPrefCallTime.TabIndex = 33;
            this.comboBoxPrefCallTime.Leave += new System.EventHandler(this.comboBoxPrefCallTime_Leave);
            this.comboBoxPrefCallTime.Enter += new System.EventHandler(this.comboBoxPrefCallTime_Enter);
            // 
            // hearAboutUs1
            // 
            this.hearAboutUs1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.hearAboutUs1.Location = new System.Drawing.Point(410, 42);
            this.hearAboutUs1.Name = "hearAboutUs1";
            this.hearAboutUs1.Size = new System.Drawing.Size(127, 22);
            this.hearAboutUs1.TabIndex = 34;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(3, 421);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(766, 1);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // phoneData1
            // 
            this.phoneData1.BackColor = System.Drawing.Color.Transparent;
            this.phoneData1.CausesValidation = false;
            this.phoneData1.Location = new System.Drawing.Point(14, 62);
            this.phoneData1.Name = "phoneData1";
            this.phoneData1.ShowFax = true;
            this.phoneData1.ShowHeading = true;
            this.phoneData1.ShowPager = true;
            this.phoneData1.Size = new System.Drawing.Size(613, 159);
            this.phoneData1.TabCountryCode = false;
            this.phoneData1.TabExtension = false;
            this.phoneData1.TabIndex = 39;
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
            this.customButtonCancel.Location = new System.Drawing.Point(376, 444);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 40;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
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
            this.customButtonReset.Location = new System.Drawing.Point(502, 446);
            this.customButtonReset.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonReset.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonReset.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonReset.Name = "customButtonReset";
            this.customButtonReset.Size = new System.Drawing.Size(100, 50);
            this.customButtonReset.TabIndex = 41;
            this.customButtonReset.Text = "&Reset";
            this.customButtonReset.UseVisualStyleBackColor = false;
            this.customButtonReset.Click += new System.EventHandler(this.buttonReset_Click);
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
            this.customButtonSubmit.Location = new System.Drawing.Point(621, 444);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 42;
            this.customButtonSubmit.Text = "&Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // UpdateCustomerContactDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_512_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(769, 505);
            this.ControlBox = false;
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonReset);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.phoneData1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.labelContactDetailsHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateCustomerContactDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Customer Contact Details";
            this.Load += new System.EventHandler(this.UpdateCustomerContactDetails_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelContactDetailsHeading;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelAlternateEmail;
        private System.Windows.Forms.Label labelEmailHeading;
        private System.Windows.Forms.Label labelPrimaryEmail;
        private CustomTextBox primaryEmailTextBox;
        private CustomTextBox alternateEmailTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label labelContactPermissions;
        private System.Windows.Forms.CheckBox checkBoxNoCall;
        private System.Windows.Forms.CheckBox checkBoxNoEmail;
        private System.Windows.Forms.CheckBox checkBoxNoFax;
        private System.Windows.Forms.CheckBox checkBoxNoMail;
        private System.Windows.Forms.CheckBox checkBoxOptOut;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label labelPaymentReminder;
        private System.Windows.Forms.CheckBox checkBoxReminder;
        private System.Windows.Forms.Label labelPrefContact;
        private System.Windows.Forms.Label labelPrefCallTime;
        private System.Windows.Forms.Label labelHowdidyouhear;
        private System.Windows.Forms.Label labelReceiveOffers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxReceivePromotions;
        private System.Windows.Forms.ComboBox comboBoxPrefContact;
        private System.Windows.Forms.ComboBox comboBoxPrefCallTime;
        private HearAboutUs hearAboutUs1;
        private PhoneData phoneData1;
        private CustomButton customButtonCancel;
        private CustomButton customButtonReset;
        private CustomButton customButtonSubmit;
    }
}
