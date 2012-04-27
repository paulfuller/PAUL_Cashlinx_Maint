using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.Services.Pickup
{
    partial class FirearmsBackgroundCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirearmsBackgroundCheck));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.customLabelState = new CustomLabel();
            this.customLabelCWPExpDate = new CustomLabel();
            this.customTextBoxCWPNumber = new CustomTextBox();
            this.customTextBoxRefNumber = new CustomTextBox();
            this.customLabelCWPNumber = new CustomLabel();
            this.customLabelRefNumber = new CustomLabel();
            this.customButtonCancel = new CustomButton();
            this.customButtonSubmit = new CustomButton();
            this.gvMerchandise = new System.Windows.Forms.DataGridView();
            this.colICN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMerchandiseDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dateCWP = new UserControls.Date();
            this.stateCWP = new UserControls.State();
            this.customTextBoxBackgroundCheckFee = new CustomTextBox();
            this.customLabelBackGroundCheckFee = new System.Windows.Forms.Label();
            this.dateExpireWarning = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvMerchandise)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(197, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Firearms Background Check Authorization";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.BackColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(4, 381);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(690, 2);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // customLabelState
            // 
            this.customLabelState.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customLabelState.AutoSize = true;
            this.customLabelState.BackColor = System.Drawing.Color.Transparent;
            this.customLabelState.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelState.Location = new System.Drawing.Point(355, 50);
            this.customLabelState.Name = "customLabelState";
            this.customLabelState.Size = new System.Drawing.Size(33, 13);
            this.customLabelState.TabIndex = 6;
            this.customLabelState.Text = "State";
            // 
            // customLabelCWPExpDate
            // 
            this.customLabelCWPExpDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customLabelCWPExpDate.AutoSize = true;
            this.customLabelCWPExpDate.BackColor = System.Drawing.Color.Transparent;
            this.customLabelCWPExpDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelCWPExpDate.Location = new System.Drawing.Point(484, 50);
            this.customLabelCWPExpDate.Name = "customLabelCWPExpDate";
            this.customLabelCWPExpDate.Size = new System.Drawing.Size(55, 13);
            this.customLabelCWPExpDate.TabIndex = 8;
            this.customLabelCWPExpDate.Text = "Exp. Date";
            // 
            // customTextBoxCWPNumber
            // 
            this.customTextBoxCWPNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxCWPNumber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.customTextBoxCWPNumber.CausesValidation = false;
            this.customTextBoxCWPNumber.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCWPNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCWPNumber.ForeColor = System.Drawing.Color.Black;
            this.customTextBoxCWPNumber.Location = new System.Drawing.Point(235, 46);
            this.customTextBoxCWPNumber.MaxLength = 20;
            this.customTextBoxCWPNumber.Name = "customTextBoxCWPNumber";
            this.customTextBoxCWPNumber.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxCWPNumber.TabIndex = 5;
            this.customTextBoxCWPNumber.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCWPNumber.TextChanged += new System.EventHandler(this.customTextBoxCWPNumber_TextChanged);
            this.customTextBoxCWPNumber.Leave += new System.EventHandler(this.customTextBoxCWPNumber_Leave);
            // 
            // customTextBoxRefNumber
            // 
            this.customTextBoxRefNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxRefNumber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.customTextBoxRefNumber.CausesValidation = false;
            this.customTextBoxRefNumber.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxRefNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxRefNumber.ForeColor = System.Drawing.Color.Black;
            this.customTextBoxRefNumber.Location = new System.Drawing.Point(235, 8);
            this.customTextBoxRefNumber.MaxLength = 20;
            this.customTextBoxRefNumber.Name = "customTextBoxRefNumber";
            this.customTextBoxRefNumber.Required = true;
            this.customTextBoxRefNumber.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxRefNumber.TabIndex = 1;
            this.customTextBoxRefNumber.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customLabelCWPNumber
            // 
            this.customLabelCWPNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customLabelCWPNumber.AutoSize = true;
            this.customLabelCWPNumber.BackColor = System.Drawing.Color.Transparent;
            this.customLabelCWPNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelCWPNumber.ForeColor = System.Drawing.Color.Black;
            this.customLabelCWPNumber.Location = new System.Drawing.Point(47, 50);
            this.customLabelCWPNumber.Name = "customLabelCWPNumber";
            this.customLabelCWPNumber.Size = new System.Drawing.Size(182, 13);
            this.customLabelCWPNumber.TabIndex = 4;
            this.customLabelCWPNumber.Text = "Concealed Weapons Permit Number:";
            // 
            // customLabelRefNumber
            // 
            this.customLabelRefNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customLabelRefNumber.AutoSize = true;
            this.customLabelRefNumber.BackColor = System.Drawing.Color.Transparent;
            this.customLabelRefNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelRefNumber.ForeColor = System.Drawing.Color.Black;
            this.customLabelRefNumber.Location = new System.Drawing.Point(47, 12);
            this.customLabelRefNumber.Name = "customLabelRefNumber";
            this.customLabelRefNumber.Size = new System.Drawing.Size(182, 13);
            this.customLabelRefNumber.TabIndex = 0;
            this.customLabelRefNumber.Text = "Background Check Tracking Number:";
            // 
            // customButtonCancel
            // 
            this.customButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.customButtonCancel.Location = new System.Drawing.Point(45, 398);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 3;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // customButtonSubmit
            // 
            this.customButtonSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.customButtonSubmit.Location = new System.Drawing.Point(533, 398);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 4;
            this.customButtonSubmit.Text = "&Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // gvMerchandise
            // 
            this.gvMerchandise.AllowUserToAddRows = false;
            this.gvMerchandise.AllowUserToDeleteRows = false;
            this.gvMerchandise.AllowUserToResizeColumns = false;
            this.gvMerchandise.AllowUserToResizeRows = false;
            this.gvMerchandise.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvMerchandise.BackgroundColor = System.Drawing.Color.White;
            this.gvMerchandise.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gvMerchandise.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvMerchandise.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvMerchandise.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvMerchandise.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colICN,
            this.colMerchandiseDescription,
            this.colStatus,
            this.colSalePrice});
            this.gvMerchandise.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvMerchandise.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvMerchandise.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvMerchandise.Enabled = false;
            this.gvMerchandise.GridColor = System.Drawing.Color.LightGray;
            this.gvMerchandise.Location = new System.Drawing.Point(13, 90);
            this.gvMerchandise.MultiSelect = false;
            this.gvMerchandise.Name = "gvMerchandise";
            this.gvMerchandise.ReadOnly = true;
            this.gvMerchandise.RowHeadersVisible = false;
            this.gvMerchandise.RowHeadersWidth = 20;
            this.gvMerchandise.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMerchandise.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gvMerchandise.RowTemplate.Height = 25;
            this.gvMerchandise.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvMerchandise.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvMerchandise.Size = new System.Drawing.Size(672, 184);
            this.gvMerchandise.TabIndex = 1;
            // 
            // colICN
            // 
            this.colICN.FillWeight = 12.70586F;
            this.colICN.HeaderText = "ICN";
            this.colICN.Name = "colICN";
            this.colICN.ReadOnly = true;
            this.colICN.Width = 140;
            // 
            // colMerchandiseDescription
            // 
            this.colMerchandiseDescription.FillWeight = 40F;
            this.colMerchandiseDescription.HeaderText = "Merchandise Description";
            this.colMerchandiseDescription.Name = "colMerchandiseDescription";
            this.colMerchandiseDescription.ReadOnly = true;
            this.colMerchandiseDescription.Width = 350;
            // 
            // colStatus
            // 
            this.colStatus.FillWeight = 18.96205F;
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colSalePrice
            // 
            this.colSalePrice.FillWeight = 6.352931F;
            this.colSalePrice.HeaderText = "Sale Price";
            this.colSalePrice.Name = "colSalePrice";
            this.colSalePrice.ReadOnly = true;
            this.colSalePrice.Width = 60;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 232F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.dateCWP, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCWPExpDate, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.stateCWP, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelState, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.customTextBoxCWPNumber, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.customTextBoxRefNumber, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelCWPNumber, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelRefNumber, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.customTextBoxBackgroundCheckFee, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.customLabelBackGroundCheckFee, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 280);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(672, 76);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // dateCWP
            // 
            this.dateCWP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateCWP.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dateCWP.CausesValidation = false;
            this.dateCWP.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.dateCWP.Location = new System.Drawing.Point(545, 47);
            this.dateCWP.Name = "dateCWP";
            this.dateCWP.Size = new System.Drawing.Size(100, 20);
            this.dateCWP.TabIndex = 9;
            this.dateCWP.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.dateCWP.ValidationExpression = "^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.]19[0-9][0-9]|20[0" +
    "-9][0-9]$";
            this.dateCWP.Leave += new System.EventHandler(this.dateCWP_Leave);
            // 
            // stateCWP
            // 
            this.stateCWP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.stateCWP.BackColor = System.Drawing.Color.WhiteSmoke;
            this.stateCWP.DisplayCode = true;
            this.stateCWP.ForeColor = System.Drawing.Color.Black;
            this.stateCWP.Location = new System.Drawing.Point(394, 46);
            this.stateCWP.Name = "stateCWP";
            this.stateCWP.selectedValue = global::Pawn.Properties.Resources.OverrideMachineName;
            this.stateCWP.Size = new System.Drawing.Size(50, 21);
            this.stateCWP.TabIndex = 7;
            // 
            // customTextBoxBackgroundCheckFee
            // 
            this.customTextBoxBackgroundCheckFee.AllowDecimalNumbers = true;
            this.customTextBoxBackgroundCheckFee.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxBackgroundCheckFee.CausesValidation = false;
            this.customTextBoxBackgroundCheckFee.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxBackgroundCheckFee.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxBackgroundCheckFee.Location = new System.Drawing.Point(545, 8);
            this.customTextBoxBackgroundCheckFee.Name = "customTextBoxBackgroundCheckFee";
            this.customTextBoxBackgroundCheckFee.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxBackgroundCheckFee.TabIndex = 3;
            this.customTextBoxBackgroundCheckFee.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxBackgroundCheckFee.Visible = false;
            this.customTextBoxBackgroundCheckFee.Leave += new System.EventHandler(this.customTextBoxBackgroundCheckFee_Leave);
            // 
            // customLabelBackGroundCheckFee
            // 
            this.customLabelBackGroundCheckFee.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customLabelBackGroundCheckFee.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.customLabelBackGroundCheckFee, 2);
            this.customLabelBackGroundCheckFee.Location = new System.Drawing.Point(423, 12);
            this.customLabelBackGroundCheckFee.Name = "customLabelBackGroundCheckFee";
            this.customLabelBackGroundCheckFee.Size = new System.Drawing.Size(116, 13);
            this.customLabelBackGroundCheckFee.TabIndex = 2;
            this.customLabelBackGroundCheckFee.Text = "Background Check Fee";
            this.customLabelBackGroundCheckFee.Visible = false;
            // 
            // dateExpireWarning
            // 
            this.dateExpireWarning.AutoSize = true;
            this.dateExpireWarning.BackColor = System.Drawing.Color.Transparent;
            this.dateExpireWarning.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateExpireWarning.ForeColor = System.Drawing.Color.Red;
            this.dateExpireWarning.Location = new System.Drawing.Point(219, 45);
            this.dateExpireWarning.Name = "dateExpireWarning";
            this.dateExpireWarning.Size = new System.Drawing.Size(227, 13);
            this.dateExpireWarning.TabIndex = 8;
            this.dateExpireWarning.Text = "Concealed Weapon Permit has expired.";
            this.dateExpireWarning.Visible = false;
            // 
            // FirearmsBackgroundCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_400_BlueScale;
            this.ClientSize = new System.Drawing.Size(699, 468);
            this.ControlBox = false;
            this.Controls.Add(this.dateExpireWarning);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.gvMerchandise);
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FirearmsBackgroundCheck";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "FirearmsBackgroundCheck";
            this.Load += new System.EventHandler(this.FirearmsBackgroundCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvMerchandise)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private CustomLabel customLabelRefNumber;
        private CustomLabel customLabelCWPNumber;
        private CustomTextBox customTextBoxRefNumber;
        private CustomTextBox customTextBoxCWPNumber;
        private CustomLabel customLabelCWPExpDate;
        private UserControls.Date dateCWP;
        private System.Windows.Forms.GroupBox groupBox1;
        private CustomLabel customLabelState;
        private UserControls.State stateCWP;
        private CustomButton customButtonCancel;
        private CustomButton customButtonSubmit;
        private System.Windows.Forms.DataGridView gvMerchandise;
        private System.Windows.Forms.DataGridViewTextBoxColumn colICN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMerchandiseDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSalePrice;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label customLabelBackGroundCheckFee;
        private CustomTextBox customTextBoxBackgroundCheckFee;
        private System.Windows.Forms.Label dateExpireWarning;
    }
}