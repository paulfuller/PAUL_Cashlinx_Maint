using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.Customer
{
    partial class CreateVendor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateVendor));
            this.label1 = new System.Windows.Forms.Label();
            this.vendInfoPanel = new System.Windows.Forms.Panel();
            this.zipcode = new Zipcode();
            this.state = new State();
            this.city = new CustomTextBox();
            this.comment = new CustomTextBox();
            this.contact = new CustomTextBox();
            this.fax = new CustomTextBox();
            this.phone2 = new CustomTextBox();
            this.phone = new CustomTextBox();
            this.ffl = new CustomTextBox();
            this.zip4 = new CustomTextBox();
            this.address1 = new CustomTextBox();
            this.address2 = new CustomTextBox();
            this.taxID = new CustomTextBox();
            this.name = new CustomTextBox();
            this.customLabel12 = new CustomLabel();
            this.customLabel11 = new CustomLabel();
            this.customLabel10 = new CustomLabel();
            this.customLabel9 = new CustomLabel();
            this.customLabel8 = new CustomLabel();
            this.customLabel7 = new CustomLabel();
            this.customLabel6 = new CustomLabel();
            this.customLabel5 = new CustomLabel();
            this.customLabel4 = new CustomLabel();
            this.customLabel3 = new CustomLabel();
            this.customLabel2 = new CustomLabel();
            this.customLabel1 = new CustomLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.customButtonSubmit = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.corporateFFLLabel = new CustomLabel();
            this.vendInfoPanel.SuspendLayout();
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
            this.label1.Size = new System.Drawing.Size(149, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vendor Information";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // vendInfoPanel
            // 
            this.vendInfoPanel.BackColor = System.Drawing.Color.Transparent;
            this.vendInfoPanel.Controls.Add(this.zipcode);
            this.vendInfoPanel.Controls.Add(this.state);
            this.vendInfoPanel.Controls.Add(this.city);
            this.vendInfoPanel.Controls.Add(this.comment);
            this.vendInfoPanel.Controls.Add(this.contact);
            this.vendInfoPanel.Controls.Add(this.fax);
            this.vendInfoPanel.Controls.Add(this.phone2);
            this.vendInfoPanel.Controls.Add(this.phone);
            this.vendInfoPanel.Controls.Add(this.ffl);
            this.vendInfoPanel.Controls.Add(this.zip4);
            this.vendInfoPanel.Controls.Add(this.address1);
            this.vendInfoPanel.Controls.Add(this.address2);
            this.vendInfoPanel.Controls.Add(this.taxID);
            this.vendInfoPanel.Controls.Add(this.name);
            this.vendInfoPanel.Controls.Add(this.customLabel12);
            this.vendInfoPanel.Controls.Add(this.customLabel11);
            this.vendInfoPanel.Controls.Add(this.customLabel10);
            this.vendInfoPanel.Controls.Add(this.customLabel9);
            this.vendInfoPanel.Controls.Add(this.customLabel8);
            this.vendInfoPanel.Controls.Add(this.customLabel7);
            this.vendInfoPanel.Controls.Add(this.customLabel6);
            this.vendInfoPanel.Controls.Add(this.customLabel5);
            this.vendInfoPanel.Controls.Add(this.customLabel4);
            this.vendInfoPanel.Controls.Add(this.customLabel3);
            this.vendInfoPanel.Controls.Add(this.customLabel2);
            this.vendInfoPanel.Controls.Add(this.customLabel1);
            this.vendInfoPanel.Location = new System.Drawing.Point(16, 95);
            this.vendInfoPanel.Name = "vendInfoPanel";
            this.vendInfoPanel.Size = new System.Drawing.Size(685, 363);
            this.vendInfoPanel.TabIndex = 2;
            // 
            // zipcode
            // 
            this.zipcode.BackColor = System.Drawing.Color.Transparent;
            this.zipcode.CausesValidation = false;
            this.zipcode.City = null;
            this.zipcode.Location = new System.Drawing.Point(131, 139);
            this.zipcode.Margin = new System.Windows.Forms.Padding(0);
            this.zipcode.Name = "zipcode";
            this.zipcode.Required = true;
            this.zipcode.Size = new System.Drawing.Size(74, 21);
            this.zipcode.State = null;
            this.zipcode.TabIndex = 6;
            // 
            // state
            // 
            this.state.BackColor = System.Drawing.Color.Transparent;
            this.state.DisplayCode = false;
            this.state.ForeColor = System.Drawing.Color.Black;
            this.state.Location = new System.Drawing.Point(449, 165);
            this.state.Name = "state";
            this.state.Required = true;
            this.state.selectedValue = global::Pawn.Properties.Resources.OverrideMachineName;
            this.state.Size = new System.Drawing.Size(50, 21);
            this.state.TabIndex = 9;
            // 
            // city
            // 
            this.city.CausesValidation = false;
            this.city.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.city.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.city.Location = new System.Drawing.Point(131, 167);
            this.city.MaxLength = 35;
            this.city.Name = "city";
            this.city.Required = true;
            this.city.Size = new System.Drawing.Size(200, 21);
            this.city.TabIndex = 8;
            this.city.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // comment
            // 
            this.comment.CausesValidation = false;
            this.comment.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.comment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comment.Location = new System.Drawing.Point(131, 326);
            this.comment.MaxLength = 255;
            this.comment.Name = "comment";
            this.comment.Size = new System.Drawing.Size(512, 21);
            this.comment.TabIndex = 15;
            this.comment.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // contact
            // 
            this.contact.CausesValidation = false;
            this.contact.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.contact.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contact.Location = new System.Drawing.Point(131, 292);
            this.contact.MaxLength = 40;
            this.contact.Name = "contact";
            this.contact.Required = true;
            this.contact.Size = new System.Drawing.Size(253, 21);
            this.contact.TabIndex = 14;
            this.contact.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // fax
            // 
            this.fax.AllowOnlyNumbers = true;
            this.fax.CausesValidation = false;
            this.fax.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.fax.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fax.FormatAsFullPhone = true;
            this.fax.Location = new System.Drawing.Point(131, 258);
            this.fax.MaxLength = 12;
            this.fax.Name = "fax";
            this.fax.Size = new System.Drawing.Size(253, 21);
            this.fax.TabIndex = 13;
            this.fax.ValidationExpression = "^([0-9][0-9][0-9])[-]([0-9][0-9][0-9][0-9])$";
            this.fax.Leave += new System.EventHandler(this.fax_Leave);
            // 
            // phone2
            // 
            this.phone2.AllowOnlyNumbers = true;
            this.phone2.CausesValidation = false;
            this.phone2.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.phone2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phone2.FormatAsFullPhone = true;
            this.phone2.Location = new System.Drawing.Point(131, 227);
            this.phone2.MaxLength = 12;
            this.phone2.Name = "phone2";
            this.phone2.Size = new System.Drawing.Size(253, 21);
            this.phone2.TabIndex = 12;
            this.phone2.ValidationExpression = "^([0-9][0-9][0-9])[-]([0-9][0-9][0-9][0-9])$";
            this.phone2.Leave += new System.EventHandler(this.phone2_Leave);
            // 
            // phone
            // 
            this.phone.AllowOnlyNumbers = true;
            this.phone.CausesValidation = false;
            this.phone.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.phone.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phone.FormatAsFullPhone = true;
            this.phone.HideSelection = false;
            this.phone.Location = new System.Drawing.Point(131, 200);
            this.phone.MaxLength = 12;
            this.phone.Multiline = true;
            this.phone.Name = "phone";
            this.phone.Required = true;
            this.phone.Size = new System.Drawing.Size(253, 20);
            this.phone.TabIndex = 10;
            this.phone.ValidationExpression = "^([0-9][0-9][0-9])[-]([0-9][0-9][0-9][0-9])$";
            this.phone.Leave += new System.EventHandler(this.phone_Leave);
            // 
            // ffl
            // 
            this.ffl.CausesValidation = false;
            this.ffl.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.ffl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ffl.Location = new System.Drawing.Point(449, 47);
            this.ffl.MaxLength = 15;
            this.ffl.Name = "ffl";
            this.ffl.Size = new System.Drawing.Size(188, 21);
            this.ffl.TabIndex = 3;
            this.ffl.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.ffl.Leave += new System.EventHandler(this.ffl_Leave);
            // 
            // zip4
            // 
            this.zip4.AllowOnlyNumbers = true;
            this.zip4.CausesValidation = false;
            this.zip4.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.zip4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zip4.Location = new System.Drawing.Point(259, 140);
            this.zip4.MaxLength = 4;
            this.zip4.Name = "zip4";
            this.zip4.Size = new System.Drawing.Size(72, 21);
            this.zip4.TabIndex = 7;
            this.zip4.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // address1
            // 
            this.address1.CausesValidation = false;
            this.address1.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.address1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.address1.Location = new System.Drawing.Point(131, 83);
            this.address1.MaxLength = 50;
            this.address1.Name = "address1";
            this.address1.Required = true;
            this.address1.Size = new System.Drawing.Size(253, 21);
            this.address1.TabIndex = 4;
            this.address1.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // address2
            // 
            this.address2.CausesValidation = false;
            this.address2.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.address2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.address2.Location = new System.Drawing.Point(131, 110);
            this.address2.MaxLength = 50;
            this.address2.Name = "address2";
            this.address2.Size = new System.Drawing.Size(253, 21);
            this.address2.TabIndex = 5;
            this.address2.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // taxID
            // 
            this.taxID.CausesValidation = false;
            this.taxID.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.taxID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taxID.Location = new System.Drawing.Point(131, 47);
            this.taxID.MaxLength = 15;
            this.taxID.Name = "taxID";
            this.taxID.Size = new System.Drawing.Size(253, 21);
            this.taxID.TabIndex = 2;
            this.taxID.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // name
            // 
            this.name.CausesValidation = false;
            this.name.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.name.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(131, 20);
            this.name.MaxLength = 40;
            this.name.Name = "name";
            this.name.Required = true;
            this.name.Size = new System.Drawing.Size(253, 21);
            this.name.TabIndex = 1;
            this.name.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customLabel12
            // 
            this.customLabel12.AutoSize = true;
            this.customLabel12.Location = new System.Drawing.Point(88, 261);
            this.customLabel12.Name = "customLabel12";
            this.customLabel12.Size = new System.Drawing.Size(27, 13);
            this.customLabel12.TabIndex = 11;
            this.customLabel12.Text = "Fax:";
            // 
            // customLabel11
            // 
            this.customLabel11.AutoSize = true;
            this.customLabel11.Location = new System.Drawing.Point(68, 295);
            this.customLabel11.Name = "customLabel11";
            this.customLabel11.Required = true;
            this.customLabel11.Size = new System.Drawing.Size(47, 13);
            this.customLabel11.TabIndex = 10;
            this.customLabel11.Text = "Contact:";
            // 
            // customLabel10
            // 
            this.customLabel10.AutoSize = true;
            this.customLabel10.Location = new System.Drawing.Point(61, 329);
            this.customLabel10.Name = "customLabel10";
            this.customLabel10.Size = new System.Drawing.Size(54, 13);
            this.customLabel10.TabIndex = 9;
            this.customLabel10.Text = "Comment:";
            // 
            // customLabel9
            // 
            this.customLabel9.AutoSize = true;
            this.customLabel9.Location = new System.Drawing.Point(228, 143);
            this.customLabel9.Name = "customLabel9";
            this.customLabel9.Size = new System.Drawing.Size(25, 13);
            this.customLabel9.TabIndex = 8;
            this.customLabel9.Text = "Ext:";
            // 
            // customLabel8
            // 
            this.customLabel8.AutoSize = true;
            this.customLabel8.Location = new System.Drawing.Point(395, 50);
            this.customLabel8.Name = "customLabel8";
            this.customLabel8.Size = new System.Drawing.Size(38, 13);
            this.customLabel8.TabIndex = 7;
            this.customLabel8.Text = "FFL #:";
            // 
            // customLabel7
            // 
            this.customLabel7.AutoSize = true;
            this.customLabel7.Location = new System.Drawing.Point(85, 171);
            this.customLabel7.Name = "customLabel7";
            this.customLabel7.Required = true;
            this.customLabel7.Size = new System.Drawing.Size(27, 13);
            this.customLabel7.TabIndex = 6;
            this.customLabel7.Text = "City:";
            // 
            // customLabel6
            // 
            this.customLabel6.AutoSize = true;
            this.customLabel6.Location = new System.Drawing.Point(398, 169);
            this.customLabel6.Name = "customLabel6";
            this.customLabel6.Required = true;
            this.customLabel6.Size = new System.Drawing.Size(35, 13);
            this.customLabel6.TabIndex = 5;
            this.customLabel6.Text = "State:";
            // 
            // customLabel5
            // 
            this.customLabel5.AutoSize = true;
            this.customLabel5.Location = new System.Drawing.Point(73, 204);
            this.customLabel5.Name = "customLabel5";
            this.customLabel5.Required = true;
            this.customLabel5.Size = new System.Drawing.Size(41, 13);
            this.customLabel5.TabIndex = 4;
            this.customLabel5.Text = "Phone:";
            // 
            // customLabel4
            // 
            this.customLabel4.AutoSize = true;
            this.customLabel4.Location = new System.Drawing.Point(63, 143);
            this.customLabel4.Name = "customLabel4";
            this.customLabel4.Required = true;
            this.customLabel4.Size = new System.Drawing.Size(53, 13);
            this.customLabel4.TabIndex = 3;
            this.customLabel4.Text = "Zip Code:";
            // 
            // customLabel3
            // 
            this.customLabel3.AutoSize = true;
            this.customLabel3.Location = new System.Drawing.Point(66, 86);
            this.customLabel3.Name = "customLabel3";
            this.customLabel3.Required = true;
            this.customLabel3.Size = new System.Drawing.Size(48, 13);
            this.customLabel3.TabIndex = 2;
            this.customLabel3.Text = "Address:";
            // 
            // customLabel2
            // 
            this.customLabel2.AutoSize = true;
            this.customLabel2.Location = new System.Drawing.Point(34, 50);
            this.customLabel2.Name = "customLabel2";
            this.customLabel2.Size = new System.Drawing.Size(80, 13);
            this.customLabel2.TabIndex = 1;
            this.customLabel2.Text = "Federal Tax ID:";
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.Location = new System.Drawing.Point(39, 23);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Required = true;
            this.customLabel1.Size = new System.Drawing.Size(75, 13);
            this.customLabel1.TabIndex = 0;
            this.customLabel1.Text = "Vendor Name:";
            // 
            // corporateFFLLabel
            // 
            this.corporateFFLLabel.BackColor = System.Drawing.Color.Transparent;
            this.corporateFFLLabel.CausesValidation = false;
            this.corporateFFLLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.corporateFFLLabel.ForeColor = System.Drawing.Color.Red;
            this.corporateFFLLabel.Location = new System.Drawing.Point(13, 95);
            this.corporateFFLLabel.Name = "corporateFFLLabel";
            this.corporateFFLLabel.Size = new System.Drawing.Size(685, 13);
            this.corporateFFLLabel.TabIndex = 51;
            this.corporateFFLLabel.Text = "For changes to the Vendor FFL please contact Shop Systems Support (SSS)";
            this.corporateFFLLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.corporateFFLLabel.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.RoyalBlue;
            this.groupBox1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.groupBox1.Location = new System.Drawing.Point(9, 477);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(692, 2);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            // 
            // errorLabel
            // 
            this.errorLabel.BackColor = System.Drawing.Color.Transparent;
            this.errorLabel.CausesValidation = false;
            this.errorLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(13, 76);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(685, 13);
            this.errorLabel.TabIndex = 27;
            this.errorLabel.Text = "Error";
            this.errorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.errorLabel.Visible = false;
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
            this.customButtonSubmit.Location = new System.Drawing.Point(569, 482);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 50;
            this.customButtonSubmit.Text = "S&ubmit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
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
            this.customButtonCancel.Location = new System.Drawing.Point(22, 481);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 47;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.createVendorCancelButton_Click);
            // 
            // zipcode
            // 
            /*this.zipcode.BackColor = System.Drawing.Color.Transparent;
            this.zipcode.CausesValidation = false;
            this.zipcode.City = null;
            this.zipcode.Location = new System.Drawing.Point(131, 139);
            this.zipcode.Margin = new System.Windows.Forms.Padding(0);
            this.zipcode.Name = "zipcode";
            this.zipcode.Required = true;
            this.zipcode.Size = new System.Drawing.Size(74, 21);
            this.zipcode.State = null;
            this.zipcode.TabIndex = 6;
            // 
            // state
            // 
            this.state.BackColor = System.Drawing.Color.Transparent;
            this.state.DisplayCode = false;
            this.state.ForeColor = System.Drawing.Color.Black;
            this.state.Location = new System.Drawing.Point(449, 165);
            this.state.Name = "state";
            this.state.Required = true;
            this.state.selectedValue = global::Pawn.Properties.Resources.OverrideMachineName;
            this.state.Size = new System.Drawing.Size(50, 21);
            this.state.TabIndex = 9;*/
            // 
            // CreateVendor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_480_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(709, 546);
            this.ControlBox = false;
            this.Controls.Add(this.corporateFFLLabel);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.vendInfoPanel);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateVendor";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "+";
            this.Load += new System.EventHandler(this.CreateVendor_Load);
            this.vendInfoPanel.ResumeLayout(false);
            this.vendInfoPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel vendInfoPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private CustomButton customButtonCancel;
        private CustomButton customButtonSubmit;
        private CustomLabel customLabel3;
        private CustomLabel customLabel2;
        private CustomLabel customLabel1;
        private CustomLabel customLabel12;
        private CustomLabel customLabel11;
        private CustomLabel customLabel10;
        private CustomLabel customLabel9;
        private CustomLabel customLabel8;
        private CustomLabel customLabel7;
        private CustomLabel customLabel6;
        private CustomLabel customLabel5;
        private CustomLabel customLabel4;
        private CustomTextBox city;
        private CustomTextBox comment;
        private CustomTextBox contact;
        private CustomTextBox fax;
        private CustomTextBox phone2;
        private CustomTextBox phone;
        private CustomTextBox ffl;
        private CustomTextBox zip4;
        private CustomTextBox address1;
        private CustomTextBox address2;
        private CustomTextBox taxID;
        private CustomTextBox name;
        private Zipcode zipcode;
        private State state;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Label corporateFFLLabel;
    }
}
