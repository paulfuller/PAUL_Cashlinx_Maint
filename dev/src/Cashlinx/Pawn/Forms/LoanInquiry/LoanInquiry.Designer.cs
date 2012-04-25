using Common.Libraries.Forms.Components;

namespace Pawn.Forms.LoanInquiry
{
    partial class LoanInquiry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoanInquiry));
            this.labelHeading = new System.Windows.Forms.Label();
            this.radioButtonCustomerSearch = new System.Windows.Forms.RadioButton();
            this.radioButtonLoanSearch = new System.Windows.Forms.RadioButton();
            this.radioButtonItemSearch = new System.Windows.Forms.RadioButton();
            this.lblAsterisk1 = new System.Windows.Forms.Label();
            this.lblAsterisk2 = new System.Windows.Forms.Label();
            this.lblDataName = new System.Windows.Forms.Label();
            this.lblDataType = new System.Windows.Forms.Label();
            this.lblAsterisk3 = new System.Windows.Forms.Label();
            this.lblSearchType = new System.Windows.Forms.Label();
            this.lblAsterisk4 = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.panelShopPanel = new System.Windows.Forms.Panel();
            this.radioButtonSelectShops = new System.Windows.Forms.RadioButton();
            this.radioButtonSearchShop = new System.Windows.Forms.RadioButton();
            this.panelCriteriaPanel = new System.Windows.Forms.Panel();
            this.panelCriteriaListPanel = new System.Windows.Forms.Panel();
            this.labelMessageLabel = new System.Windows.Forms.Label();
            this.cboSortDirection = new System.Windows.Forms.ComboBox();
            this.cboSortField = new System.Windows.Forms.ComboBox();
            this.lblSortBy = new System.Windows.Forms.Label();
            this.customButtonFind = new CustomButton();
            this.customButtonSave = new CustomButton();
            this.customButtonClear = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.customTextBoxShop = new CustomTextBox();
            this.panelShopPanel.SuspendLayout();
            this.panelCriteriaPanel.SuspendLayout();
            this.panelCriteriaListPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(28, 41);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(158, 19);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Pawn Loan Search";
            // 
            // radioButtonCustomerSearch
            // 
            this.radioButtonCustomerSearch.AutoSize = true;
            this.radioButtonCustomerSearch.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonCustomerSearch.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonCustomerSearch.Location = new System.Drawing.Point(52, 22);
            this.radioButtonCustomerSearch.Name = "radioButtonCustomerSearch";
            this.radioButtonCustomerSearch.Size = new System.Drawing.Size(153, 22);
            this.radioButtonCustomerSearch.TabIndex = 20;
            this.radioButtonCustomerSearch.Text = "Customer Search";
            this.radioButtonCustomerSearch.UseVisualStyleBackColor = false;
            this.radioButtonCustomerSearch.CheckedChanged += new System.EventHandler(this.radioButtonCustomerSearch_CheckedChanged);
            // 
            // radioButtonLoanSearch
            // 
            this.radioButtonLoanSearch.AutoSize = true;
            this.radioButtonLoanSearch.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonLoanSearch.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonLoanSearch.Location = new System.Drawing.Point(394, 22);
            this.radioButtonLoanSearch.Name = "radioButtonLoanSearch";
            this.radioButtonLoanSearch.Size = new System.Drawing.Size(118, 22);
            this.radioButtonLoanSearch.TabIndex = 21;
            this.radioButtonLoanSearch.Text = "Loan Search";
            this.radioButtonLoanSearch.UseVisualStyleBackColor = false;
            this.radioButtonLoanSearch.CheckedChanged += new System.EventHandler(this.radioButtonLoanSearch_CheckedChanged);
            // 
            // radioButtonItemSearch
            // 
            this.radioButtonItemSearch.AutoSize = true;
            this.radioButtonItemSearch.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonItemSearch.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonItemSearch.Location = new System.Drawing.Point(729, 22);
            this.radioButtonItemSearch.Name = "radioButtonItemSearch";
            this.radioButtonItemSearch.Size = new System.Drawing.Size(117, 22);
            this.radioButtonItemSearch.TabIndex = 22;
            this.radioButtonItemSearch.Text = "Item Search";
            this.radioButtonItemSearch.UseVisualStyleBackColor = false;
            this.radioButtonItemSearch.CheckedChanged += new System.EventHandler(this.radioButtonItemSearch_CheckedChanged);
            // 
            // lblAsterisk1
            // 
            this.lblAsterisk1.AutoSize = true;
            this.lblAsterisk1.BackColor = System.Drawing.Color.Transparent;
            this.lblAsterisk1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblAsterisk1.ForeColor = System.Drawing.Color.Red;
            this.lblAsterisk1.Location = new System.Drawing.Point(113, 64);
            this.lblAsterisk1.Name = "lblAsterisk1";
            this.lblAsterisk1.Size = new System.Drawing.Size(14, 13);
            this.lblAsterisk1.TabIndex = 25;
            this.lblAsterisk1.Text = "*";
            // 
            // lblAsterisk2
            // 
            this.lblAsterisk2.AutoSize = true;
            this.lblAsterisk2.BackColor = System.Drawing.Color.Transparent;
            this.lblAsterisk2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblAsterisk2.ForeColor = System.Drawing.Color.Red;
            this.lblAsterisk2.Location = new System.Drawing.Point(234, 64);
            this.lblAsterisk2.Name = "lblAsterisk2";
            this.lblAsterisk2.Size = new System.Drawing.Size(14, 13);
            this.lblAsterisk2.TabIndex = 26;
            this.lblAsterisk2.Text = "*";
            // 
            // lblDataName
            // 
            this.lblDataName.AutoSize = true;
            this.lblDataName.BackColor = System.Drawing.Color.Transparent;
            this.lblDataName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblDataName.Location = new System.Drawing.Point(139, 64);
            this.lblDataName.Name = "lblDataName";
            this.lblDataName.Size = new System.Drawing.Size(84, 17);
            this.lblDataName.TabIndex = 24;
            this.lblDataName.Text = "Data Name";
            // 
            // lblDataType
            // 
            this.lblDataType.AutoSize = true;
            this.lblDataType.BackColor = System.Drawing.Color.Transparent;
            this.lblDataType.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblDataType.Location = new System.Drawing.Point(31, 64);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(78, 17);
            this.lblDataType.TabIndex = 23;
            this.lblDataType.Text = "Data Type";
            // 
            // lblAsterisk3
            // 
            this.lblAsterisk3.AutoSize = true;
            this.lblAsterisk3.BackColor = System.Drawing.Color.Transparent;
            this.lblAsterisk3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblAsterisk3.ForeColor = System.Drawing.Color.Red;
            this.lblAsterisk3.Location = new System.Drawing.Point(389, 64);
            this.lblAsterisk3.Name = "lblAsterisk3";
            this.lblAsterisk3.Size = new System.Drawing.Size(14, 13);
            this.lblAsterisk3.TabIndex = 28;
            this.lblAsterisk3.Text = "*";
            // 
            // lblSearchType
            // 
            this.lblSearchType.AutoSize = true;
            this.lblSearchType.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchType.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblSearchType.Location = new System.Drawing.Point(296, 64);
            this.lblSearchType.Name = "lblSearchType";
            this.lblSearchType.Size = new System.Drawing.Size(93, 17);
            this.lblSearchType.TabIndex = 27;
            this.lblSearchType.Text = "Search Type";
            // 
            // lblAsterisk4
            // 
            this.lblAsterisk4.AutoSize = true;
            this.lblAsterisk4.BackColor = System.Drawing.Color.Transparent;
            this.lblAsterisk4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblAsterisk4.ForeColor = System.Drawing.Color.Red;
            this.lblAsterisk4.Location = new System.Drawing.Point(449, 64);
            this.lblAsterisk4.Name = "lblAsterisk4";
            this.lblAsterisk4.Size = new System.Drawing.Size(14, 13);
            this.lblAsterisk4.TabIndex = 30;
            this.lblAsterisk4.Text = "*";
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.BackColor = System.Drawing.Color.Transparent;
            this.lblValue.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblValue.Location = new System.Drawing.Point(406, 64);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(46, 17);
            this.lblValue.TabIndex = 29;
            this.lblValue.Text = "Value";
            // 
            // panelShopPanel
            // 
            this.panelShopPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelShopPanel.Controls.Add(this.radioButtonSelectShops);
            this.panelShopPanel.Controls.Add(this.radioButtonSearchShop);
            this.panelShopPanel.Controls.Add(this.customTextBoxShop);
            this.panelShopPanel.Location = new System.Drawing.Point(8, 88);
            this.panelShopPanel.Name = "panelShopPanel";
            this.panelShopPanel.Size = new System.Drawing.Size(944, 70);
            this.panelShopPanel.TabIndex = 33;
            // 
            // radioButtonSelectShops
            // 
            this.radioButtonSelectShops.AutoSize = true;
            this.radioButtonSelectShops.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonSelectShops.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSelectShops.Location = new System.Drawing.Point(316, 22);
            this.radioButtonSelectShops.Name = "radioButtonSelectShops";
            this.radioButtonSelectShops.Size = new System.Drawing.Size(117, 17);
            this.radioButtonSelectShops.TabIndex = 14;
            this.radioButtonSelectShops.TabStop = true;
            this.radioButtonSelectShops.Text = "Select Locations";
            this.radioButtonSelectShops.UseVisualStyleBackColor = false;
            this.radioButtonSelectShops.CheckedChanged += new System.EventHandler(this.radioButtonSelectShops_CheckedChanged);
            // 
            // radioButtonSearchShop
            // 
            this.radioButtonSearchShop.AutoSize = true;
            this.radioButtonSearchShop.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonSearchShop.Checked = true;
            this.radioButtonSearchShop.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonSearchShop.Location = new System.Drawing.Point(14, 22);
            this.radioButtonSearchShop.Name = "radioButtonSearchShop";
            this.radioButtonSearchShop.Size = new System.Drawing.Size(110, 17);
            this.radioButtonSearchShop.TabIndex = 13;
            this.radioButtonSearchShop.TabStop = true;
            this.radioButtonSearchShop.Text = "Search Shop #:";
            this.radioButtonSearchShop.UseVisualStyleBackColor = false;
            // 
            // panelCriteriaPanel
            // 
            this.panelCriteriaPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelCriteriaPanel.Controls.Add(this.panelCriteriaListPanel);
            this.panelCriteriaPanel.Controls.Add(this.cboSortDirection);
            this.panelCriteriaPanel.Controls.Add(this.cboSortField);
            this.panelCriteriaPanel.Controls.Add(this.lblSortBy);
            this.panelCriteriaPanel.Controls.Add(this.lblDataType);
            this.panelCriteriaPanel.Controls.Add(this.radioButtonCustomerSearch);
            this.panelCriteriaPanel.Controls.Add(this.customButtonFind);
            this.panelCriteriaPanel.Controls.Add(this.customButtonSave);
            this.panelCriteriaPanel.Controls.Add(this.radioButtonLoanSearch);
            this.panelCriteriaPanel.Controls.Add(this.customButtonClear);
            this.panelCriteriaPanel.Controls.Add(this.radioButtonItemSearch);
            this.panelCriteriaPanel.Controls.Add(this.customButtonCancel);
            this.panelCriteriaPanel.Controls.Add(this.lblAsterisk4);
            this.panelCriteriaPanel.Controls.Add(this.lblDataName);
            this.panelCriteriaPanel.Controls.Add(this.lblValue);
            this.panelCriteriaPanel.Controls.Add(this.lblAsterisk1);
            this.panelCriteriaPanel.Controls.Add(this.lblAsterisk3);
            this.panelCriteriaPanel.Controls.Add(this.lblAsterisk2);
            this.panelCriteriaPanel.Controls.Add(this.lblSearchType);
            this.panelCriteriaPanel.Location = new System.Drawing.Point(8, 160);
            this.panelCriteriaPanel.Name = "panelCriteriaPanel";
            this.panelCriteriaPanel.Size = new System.Drawing.Size(944, 474);
            this.panelCriteriaPanel.TabIndex = 34;
            // 
            // panelCriteriaListPanel
            // 
            this.panelCriteriaListPanel.AutoScroll = true;
            this.panelCriteriaListPanel.Controls.Add(this.labelMessageLabel);
            this.panelCriteriaListPanel.Location = new System.Drawing.Point(-7, 84);
            this.panelCriteriaListPanel.Name = "panelCriteriaListPanel";
            this.panelCriteriaListPanel.Size = new System.Drawing.Size(938, 293);
            this.panelCriteriaListPanel.TabIndex = 36;
            // 
            // labelMessageLabel
            // 
            this.labelMessageLabel.AutoSize = true;
            this.labelMessageLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMessageLabel.Location = new System.Drawing.Point(302, 125);
            this.labelMessageLabel.Name = "labelMessageLabel";
            this.labelMessageLabel.Size = new System.Drawing.Size(288, 13);
            this.labelMessageLabel.TabIndex = 0;
            this.labelMessageLabel.Text = "To include another search condition, select the And button";
            // 
            // cboSortDirection
            // 
            this.cboSortDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSortDirection.FormattingEnabled = true;
            this.cboSortDirection.IntegralHeight = false;
            this.cboSortDirection.Location = new System.Drawing.Point(550, 383);
            this.cboSortDirection.Name = "cboSortDirection";
            this.cboSortDirection.Size = new System.Drawing.Size(122, 21);
            this.cboSortDirection.TabIndex = 35;
            this.cboSortDirection.SelectedIndexChanged += new System.EventHandler(this.cboSortDirection_SelectedIndexChanged);
            // 
            // cboSortField
            // 
            this.cboSortField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSortField.FormattingEnabled = true;
            this.cboSortField.IntegralHeight = false;
            this.cboSortField.Location = new System.Drawing.Point(287, 383);
            this.cboSortField.Name = "cboSortField";
            this.cboSortField.Size = new System.Drawing.Size(165, 21);
            this.cboSortField.TabIndex = 34;
            this.cboSortField.SelectedIndexChanged += new System.EventHandler(this.cboSortField_SelectedIndexChanged);
            // 
            // lblSortBy
            // 
            this.lblSortBy.AutoSize = true;
            this.lblSortBy.BackColor = System.Drawing.Color.Transparent;
            this.lblSortBy.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblSortBy.Location = new System.Drawing.Point(123, 387);
            this.lblSortBy.Name = "lblSortBy";
            this.lblSortBy.Size = new System.Drawing.Size(148, 17);
            this.lblSortBy.TabIndex = 33;
            this.lblSortBy.Text = "Sort By Customers: ";
            // 
            // customButtonFind
            // 
            this.customButtonFind.BackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonFind.BackgroundImage")));
            this.customButtonFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonFind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonFind.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonFind.FlatAppearance.BorderSize = 0;
            this.customButtonFind.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonFind.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonFind.ForeColor = System.Drawing.Color.White;
            this.customButtonFind.Location = new System.Drawing.Point(841, 421);
            this.customButtonFind.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonFind.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonFind.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonFind.Name = "customButtonFind";
            this.customButtonFind.Size = new System.Drawing.Size(100, 50);
            this.customButtonFind.TabIndex = 32;
            this.customButtonFind.Text = "&Find";
            this.customButtonFind.UseVisualStyleBackColor = false;
            this.customButtonFind.Click += new System.EventHandler(this.customButtonFind_Click);
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
            this.customButtonSave.Location = new System.Drawing.Point(741, 421);
            this.customButtonSave.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSave.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSave.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSave.Name = "customButtonSave";
            this.customButtonSave.Size = new System.Drawing.Size(100, 50);
            this.customButtonSave.TabIndex = 18;
            this.customButtonSave.Text = "&Save";
            this.customButtonSave.UseVisualStyleBackColor = false;
            this.customButtonSave.Click += new System.EventHandler(this.customButtonSave_Click);
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
            this.customButtonClear.Location = new System.Drawing.Point(642, 421);
            this.customButtonClear.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonClear.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonClear.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonClear.Name = "customButtonClear";
            this.customButtonClear.Size = new System.Drawing.Size(100, 50);
            this.customButtonClear.TabIndex = 17;
            this.customButtonClear.Text = "C&lear";
            this.customButtonClear.UseVisualStyleBackColor = false;
            this.customButtonClear.Click += new System.EventHandler(this.customButtonClear_Click);
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
            this.customButtonCancel.Location = new System.Drawing.Point(4, 421);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 16;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // customTextBoxShop
            // 
            this.customTextBoxShop.CausesValidation = false;
            this.customTextBoxShop.ErrorMessage = "";
            this.customTextBoxShop.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxShop.Location = new System.Drawing.Point(130, 18);
            this.customTextBoxShop.Name = "customTextBoxShop";
            this.customTextBoxShop.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxShop.TabIndex = 15;
            this.customTextBoxShop.ValidationExpression = "";
            // 
            // LoanInquiry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = Common.Properties.Resources.newDialog_440_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(961, 646);
            this.ControlBox = false;
            this.Controls.Add(this.panelCriteriaPanel);
            this.Controls.Add(this.panelShopPanel);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoanInquiry";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loan Inquiry";
            this.Load += new System.EventHandler(this.LoanInquiry_Load);
            this.panelShopPanel.ResumeLayout(false);
            this.panelShopPanel.PerformLayout();
            this.panelCriteriaPanel.ResumeLayout(false);
            this.panelCriteriaPanel.PerformLayout();
            this.panelCriteriaListPanel.ResumeLayout(false);
            this.panelCriteriaListPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private CustomButton customButtonCancel;
        private CustomButton customButtonClear;
        private CustomButton customButtonSave;
        private System.Windows.Forms.RadioButton radioButtonCustomerSearch;
        private System.Windows.Forms.RadioButton radioButtonLoanSearch;
        private System.Windows.Forms.RadioButton radioButtonItemSearch;
        private System.Windows.Forms.Label lblAsterisk1;
        private System.Windows.Forms.Label lblAsterisk2;
        private System.Windows.Forms.Label lblDataName;
        private System.Windows.Forms.Label lblDataType;
        private System.Windows.Forms.Label lblAsterisk3;
        private System.Windows.Forms.Label lblSearchType;
        private System.Windows.Forms.Label lblAsterisk4;
        private System.Windows.Forms.Label lblValue;
        private CustomButton customButtonFind;
        private System.Windows.Forms.Panel panelShopPanel;
        private System.Windows.Forms.RadioButton radioButtonSelectShops;
        private System.Windows.Forms.RadioButton radioButtonSearchShop;
        private CustomTextBox customTextBoxShop;
        private System.Windows.Forms.Panel panelCriteriaPanel;
        private System.Windows.Forms.Label lblSortBy;
        private System.Windows.Forms.ComboBox cboSortDirection;
        private System.Windows.Forms.ComboBox cboSortField;
        private System.Windows.Forms.Panel panelCriteriaListPanel;
        private System.Windows.Forms.Label labelMessageLabel;
    }
}