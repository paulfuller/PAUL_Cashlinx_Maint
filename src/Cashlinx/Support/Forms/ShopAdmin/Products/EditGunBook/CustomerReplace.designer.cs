//using CashlinxDesktop.UserControls;
using Common.Libraries.Forms.Components;
using Support.Forms.UserControls;

namespace Support.Forms.ShopAdmin.EditGunBook
{
    partial class CustomerReplace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerReplace));
            this.customLabel6 = new CustomLabel();
            this.customLabel5 = new CustomLabel();
            this.customLabel4 = new CustomLabel();
            this.customLabel3 = new CustomLabel();
            this.customLabel2 = new CustomLabel();
            this.customLabel1 = new CustomLabel();
            this.pwnapp_identificationstate = new State();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pwnapp_identificationtype = new IDType();
            this.pwnapp_identificationnumber = new CustomTextBox();
            this.pwnapp_identificationtype_label = new CustomLabel();
            this.pwnapp_identificationnumber_label = new CustomLabel();
            this.pwnapp_identificationstate_label = new CustomLabel();
            this.pwnapp_identificationexpirationdate = new Date();
            this.pwnapp_identificationcountry = new Country();
            this.pwnapp_identificationexpirationdate_label = new CustomLabel();
            this.customButtonSubmit = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.state1 = new State();
            this.customTextBoxInitial = new CustomTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.customTextBoxFirstName = new CustomTextBox();
            this.customTextBoxLastName = new CustomTextBox();
            this.zipcode1 = new Zipcode();
            this.customTextBoxCity = new CustomTextBox();
            this.customTextBoxAddr2 = new CustomTextBox();
            this.customTextBoxAddr1 = new CustomTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.labelCustNumber = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.customerNumber = new System.Windows.Forms.Label();
            this.customerNo = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.Label();
            this.idHeader = new System.Windows.Forms.Label();
            this.address2 = new System.Windows.Forms.Label();
            this.address1 = new System.Windows.Forms.Label();
            this.currentName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // customLabel6
            // 
            this.customLabel6.AutoSize = true;
            this.customLabel6.BackColor = System.Drawing.Color.Transparent;
            this.customLabel6.Location = new System.Drawing.Point(426, 360);
            this.customLabel6.Name = "customLabel6";
            this.customLabel6.Required = true;
            this.customLabel6.Size = new System.Drawing.Size(48, 13);
            this.customLabel6.TabIndex = 54;
            this.customLabel6.Text = "Zipcode:";
            // 
            // customLabel5
            // 
            this.customLabel5.AutoSize = true;
            this.customLabel5.BackColor = System.Drawing.Color.Transparent;
            this.customLabel5.Location = new System.Drawing.Point(21, 360);
            this.customLabel5.Name = "customLabel5";
            this.customLabel5.Required = true;
            this.customLabel5.Size = new System.Drawing.Size(30, 13);
            this.customLabel5.TabIndex = 53;
            this.customLabel5.Text = "City:";
            // 
            // customLabel4
            // 
            this.customLabel4.AutoSize = true;
            this.customLabel4.BackColor = System.Drawing.Color.Transparent;
            this.customLabel4.Location = new System.Drawing.Point(12, 303);
            this.customLabel4.Name = "customLabel4";
            this.customLabel4.Required = true;
            this.customLabel4.Size = new System.Drawing.Size(50, 13);
            this.customLabel4.TabIndex = 52;
            this.customLabel4.Text = "Address:";
            // 
            // customLabel3
            // 
            this.customLabel3.AutoSize = true;
            this.customLabel3.BackColor = System.Drawing.Color.Transparent;
            this.customLabel3.Location = new System.Drawing.Point(227, 278);
            this.customLabel3.Name = "customLabel3";
            this.customLabel3.Required = true;
            this.customLabel3.Size = new System.Drawing.Size(62, 13);
            this.customLabel3.TabIndex = 51;
            this.customLabel3.Text = "First Name:";
            // 
            // customLabel2
            // 
            this.customLabel2.AutoSize = true;
            this.customLabel2.BackColor = System.Drawing.Color.Transparent;
            this.customLabel2.Location = new System.Drawing.Point(7, 278);
            this.customLabel2.Name = "customLabel2";
            this.customLabel2.Required = true;
            this.customLabel2.Size = new System.Drawing.Size(61, 13);
            this.customLabel2.TabIndex = 50;
            this.customLabel2.Text = "Last Name:";
            // 
            // customLabel1
            // 
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.Location = new System.Drawing.Point(582, 400);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(38, 13);
            this.customLabel1.TabIndex = 49;
            this.customLabel1.Text = "Issuer";
            this.customLabel1.Visible = false;
            // 
            // pwnapp_identificationstate
            // 
            this.pwnapp_identificationstate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pwnapp_identificationstate.BackColor = System.Drawing.Color.Transparent;
            this.pwnapp_identificationstate.DisplayCode = true;
            this.pwnapp_identificationstate.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_identificationstate.Location = new System.Drawing.Point(585, 416);
            this.pwnapp_identificationstate.Name = "pwnapp_identificationstate";
            this.pwnapp_identificationstate.selectedValue = global::Support.Properties.Resources.OverrideMachineName;
            this.pwnapp_identificationstate.Size = new System.Drawing.Size(50, 21);
            this.pwnapp_identificationstate.TabIndex = 48;
            this.pwnapp_identificationstate.Visible = false;
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 394);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(552, 43);
            this.tableLayoutPanel2.TabIndex = 47;
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
            // 
            // pwnapp_identificationnumber
            // 
            this.pwnapp_identificationnumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pwnapp_identificationnumber.BackColor = System.Drawing.Color.White;
            this.pwnapp_identificationnumber.CausesValidation = false;
            this.pwnapp_identificationnumber.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.pwnapp_identificationnumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_identificationnumber.ForeColor = System.Drawing.Color.Black;
            this.pwnapp_identificationnumber.Location = new System.Drawing.Point(316, 18);
            this.pwnapp_identificationnumber.MaximumSize = new System.Drawing.Size(120, 21);
            this.pwnapp_identificationnumber.MaxLength = 20;
            this.pwnapp_identificationnumber.MinimumSize = new System.Drawing.Size(120, 21);
            this.pwnapp_identificationnumber.Name = "pwnapp_identificationnumber";
            this.pwnapp_identificationnumber.Size = new System.Drawing.Size(120, 21);
            this.pwnapp_identificationnumber.TabIndex = 15;
            this.pwnapp_identificationnumber.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
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
            this.pwnapp_identificationexpirationdate.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
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
            this.customButtonSubmit.Location = new System.Drawing.Point(520, 464);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 32;
            this.customButtonSubmit.Text = "Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.customButtonSubmit_Click);
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
            this.customButtonCancel.Location = new System.Drawing.Point(18, 464);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 31;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // state1
            // 
            this.state1.BackColor = System.Drawing.Color.Transparent;
            this.state1.DisplayCode = false;
            this.state1.ForeColor = System.Drawing.Color.Black;
            this.state1.Location = new System.Drawing.Point(278, 357);
            this.state1.Name = "state1";
            this.state1.selectedValue = global::Support.Properties.Resources.OverrideMachineName;
            this.state1.Size = new System.Drawing.Size(50, 21);
            this.state1.TabIndex = 30;
            // 
            // customTextBoxInitial
            // 
            this.customTextBoxInitial.CausesValidation = false;
            this.customTextBoxInitial.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.customTextBoxInitial.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxInitial.Location = new System.Drawing.Point(520, 273);
            this.customTextBoxInitial.MaxLength = 1;
            this.customTextBoxInitial.Name = "customTextBoxInitial";
            this.customTextBoxInitial.Size = new System.Drawing.Size(28, 21);
            this.customTextBoxInitial.TabIndex = 29;
            this.customTextBoxInitial.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(454, 278);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Initial:";
            // 
            // customTextBoxFirstName
            // 
            this.customTextBoxFirstName.CausesValidation = false;
            this.customTextBoxFirstName.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.customTextBoxFirstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxFirstName.Location = new System.Drawing.Point(301, 273);
            this.customTextBoxFirstName.MaxLength = 40;
            this.customTextBoxFirstName.Name = "customTextBoxFirstName";
            this.customTextBoxFirstName.Size = new System.Drawing.Size(126, 21);
            this.customTextBoxFirstName.TabIndex = 27;
            this.customTextBoxFirstName.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // customTextBoxLastName
            // 
            this.customTextBoxLastName.CausesValidation = false;
            this.customTextBoxLastName.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.customTextBoxLastName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxLastName.Location = new System.Drawing.Point(78, 273);
            this.customTextBoxLastName.MaxLength = 40;
            this.customTextBoxLastName.Name = "customTextBoxLastName";
            this.customTextBoxLastName.Size = new System.Drawing.Size(126, 21);
            this.customTextBoxLastName.TabIndex = 25;
            this.customTextBoxLastName.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // zipcode1
            // 
            this.zipcode1.BackColor = System.Drawing.Color.Transparent;
            this.zipcode1.CausesValidation = false;
            this.zipcode1.City = null;
            this.zipcode1.Location = new System.Drawing.Point(501, 357);
            this.zipcode1.Margin = new System.Windows.Forms.Padding(0);
            this.zipcode1.Name = "zipcode1";
            this.zipcode1.Size = new System.Drawing.Size(70, 23);
            this.zipcode1.State = null;
            this.zipcode1.TabIndex = 24;
            this.zipcode1.Leave += new System.EventHandler(this.zipcode1_Leave);
            // 
            // customTextBoxCity
            // 
            this.customTextBoxCity.CausesValidation = false;
            this.customTextBoxCity.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.customTextBoxCity.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCity.Location = new System.Drawing.Point(78, 357);
            this.customTextBoxCity.MaxLength = 40;
            this.customTextBoxCity.Name = "customTextBoxCity";
            this.customTextBoxCity.Size = new System.Drawing.Size(126, 21);
            this.customTextBoxCity.TabIndex = 23;
            this.customTextBoxCity.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // customTextBoxAddr2
            // 
            this.customTextBoxAddr2.CausesValidation = false;
            this.customTextBoxAddr2.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.customTextBoxAddr2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxAddr2.Location = new System.Drawing.Point(78, 330);
            this.customTextBoxAddr2.MaxLength = 39;
            this.customTextBoxAddr2.Name = "customTextBoxAddr2";
            this.customTextBoxAddr2.Size = new System.Drawing.Size(126, 21);
            this.customTextBoxAddr2.TabIndex = 22;
            this.customTextBoxAddr2.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // customTextBoxAddr1
            // 
            this.customTextBoxAddr1.CausesValidation = false;
            this.customTextBoxAddr1.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.customTextBoxAddr1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxAddr1.Location = new System.Drawing.Point(78, 303);
            this.customTextBoxAddr1.MaxLength = 40;
            this.customTextBoxAddr1.Name = "customTextBoxAddr1";
            this.customTextBoxAddr1.Size = new System.Drawing.Size(126, 21);
            this.customTextBoxAddr1.TabIndex = 21;
            this.customTextBoxAddr1.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(235, 360);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "State:";
            // 
            // labelCustNumber
            // 
            this.labelCustNumber.AutoSize = true;
            this.labelCustNumber.BackColor = System.Drawing.Color.Transparent;
            this.labelCustNumber.Location = new System.Drawing.Point(554, 252);
            this.labelCustNumber.Name = "labelCustNumber";
            this.labelCustNumber.Size = new System.Drawing.Size(43, 13);
            this.labelCustNumber.TabIndex = 11;
            this.labelCustNumber.Text = "200000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(480, 252);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Customer #:";
            // 
            // customerNumber
            // 
            this.customerNumber.AutoSize = true;
            this.customerNumber.BackColor = System.Drawing.Color.Transparent;
            this.customerNumber.Location = new System.Drawing.Point(554, 149);
            this.customerNumber.Name = "customerNumber";
            this.customerNumber.Size = new System.Drawing.Size(43, 13);
            this.customerNumber.TabIndex = 9;
            this.customerNumber.Text = "200000";
            // 
            // customerNo
            // 
            this.customerNo.AutoSize = true;
            this.customerNo.BackColor = System.Drawing.Color.Transparent;
            this.customerNo.Location = new System.Drawing.Point(480, 149);
            this.customerNo.Name = "customerNo";
            this.customerNo.Size = new System.Drawing.Size(68, 13);
            this.customerNo.TabIndex = 8;
            this.customerNo.Text = "Customer #:";
            // 
            // id
            // 
            this.id.AutoSize = true;
            this.id.BackColor = System.Drawing.Color.Transparent;
            this.id.Location = new System.Drawing.Point(288, 149);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(15, 13);
            this.id.TabIndex = 7;
            this.id.Text = "id";
            // 
            // idHeader
            // 
            this.idHeader.AutoSize = true;
            this.idHeader.BackColor = System.Drawing.Color.Transparent;
            this.idHeader.Location = new System.Drawing.Point(250, 149);
            this.idHeader.Name = "idHeader";
            this.idHeader.Size = new System.Drawing.Size(22, 13);
            this.idHeader.TabIndex = 6;
            this.idHeader.Text = "ID:";
            // 
            // address2
            // 
            this.address2.AutoSize = true;
            this.address2.BackColor = System.Drawing.Color.Transparent;
            this.address2.Location = new System.Drawing.Point(18, 194);
            this.address2.Name = "address2";
            this.address2.Size = new System.Drawing.Size(52, 13);
            this.address2.TabIndex = 5;
            this.address2.Text = "Address2";
            // 
            // address1
            // 
            this.address1.AutoSize = true;
            this.address1.BackColor = System.Drawing.Color.Transparent;
            this.address1.Location = new System.Drawing.Point(18, 172);
            this.address1.Name = "address1";
            this.address1.Size = new System.Drawing.Size(52, 13);
            this.address1.TabIndex = 4;
            this.address1.Text = "Address1";
            // 
            // currentName
            // 
            this.currentName.AutoSize = true;
            this.currentName.BackColor = System.Drawing.Color.Transparent;
            this.currentName.Location = new System.Drawing.Point(18, 149);
            this.currentName.Name = "currentName";
            this.currentName.Size = new System.Drawing.Size(33, 13);
            this.currentName.TabIndex = 3;
            this.currentName.Text = "name";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(6, 217);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(667, 31);
            this.panel2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "New Customer Information";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(6, 97);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(667, 31);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Current Customer Information";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Replace Receipt Customer Information";
            // 
            // CustomerReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 523);
            this.Controls.Add(this.customLabel6);
            this.Controls.Add(this.customLabel5);
            this.Controls.Add(this.customLabel4);
            this.Controls.Add(this.customLabel3);
            this.Controls.Add(this.customLabel2);
            this.Controls.Add(this.customLabel1);
            this.Controls.Add(this.pwnapp_identificationstate);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.state1);
            this.Controls.Add(this.customTextBoxInitial);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.customTextBoxFirstName);
            this.Controls.Add(this.customTextBoxLastName);
            this.Controls.Add(this.zipcode1);
            this.Controls.Add(this.customTextBoxCity);
            this.Controls.Add(this.customTextBoxAddr2);
            this.Controls.Add(this.customTextBoxAddr1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.labelCustNumber);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.customerNumber);
            this.Controls.Add(this.customerNo);
            this.Controls.Add(this.id);
            this.Controls.Add(this.idHeader);
            this.Controls.Add(this.address2);
            this.Controls.Add(this.address1);
            this.Controls.Add(this.currentName);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "CustomerReplace";
            this.Text = "GunBookCustomer";
            this.Load += new System.EventHandler(this.CustomerReplace_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label currentName;
        private System.Windows.Forms.Label address1;
        private System.Windows.Forms.Label address2;
        private System.Windows.Forms.Label idHeader;
        private System.Windows.Forms.Label id;
        private System.Windows.Forms.Label customerNo;
        private System.Windows.Forms.Label customerNumber;
        private System.Windows.Forms.Label labelCustNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private CustomTextBox customTextBoxAddr1;
        private CustomTextBox customTextBoxAddr2;
        private CustomTextBox customTextBoxCity;
        private Zipcode zipcode1;
        private CustomTextBox customTextBoxLastName;
        private CustomTextBox customTextBoxFirstName;
        private CustomTextBox customTextBoxInitial;
        private System.Windows.Forms.Label label12;
        private State state1;
        private CustomButton customButtonCancel;
        private CustomButton customButtonSubmit;
        private CustomLabel customLabel1;
        private State pwnapp_identificationstate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private IDType pwnapp_identificationtype;
        private CustomTextBox pwnapp_identificationnumber;
        private CustomLabel pwnapp_identificationtype_label;
        private CustomLabel pwnapp_identificationnumber_label;
        private CustomLabel pwnapp_identificationstate_label;
        private Date pwnapp_identificationexpirationdate;
        private Country pwnapp_identificationcountry;
        private CustomLabel pwnapp_identificationexpirationdate_label;
        private CustomLabel customLabel2;
        private CustomLabel customLabel3;
        private CustomLabel customLabel4;
        private CustomLabel customLabel5;
        private CustomLabel customLabel6;
    }
}