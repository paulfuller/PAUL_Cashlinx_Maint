using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Products.ManageMultiplePawnItems
{
    partial class ManageMultiplePawnItems
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageMultiplePawnItems));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.gvPawnItems = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.loanInformation = new System.Windows.Forms.GroupBox();
            this.aprComboBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.aprLabel = new System.Windows.Forms.Label();
            this.serviceChargesLabel = new System.Windows.Forms.Label();
            this.totalTextBox = new System.Windows.Forms.TextBox();
            this.totalLabel = new System.Windows.Forms.Label();
            this.financeChargesLabel = new System.Windows.Forms.Label();
            this.amountFinancedLabel = new System.Windows.Forms.Label();
            this.interestTextBox = new System.Windows.Forms.TextBox();
            this.serviceTextBox = new System.Windows.Forms.TextBox();
            this.feesTextBox = new System.Windows.Forms.TextBox();
            this.amountFinancedTextBox = new System.Windows.Forms.TextBox();
            this.buttonPanel = new System.Windows.Forms.GroupBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.panelPurchase = new System.Windows.Forms.Panel();
            this.labelPurchaseAmtvalue = new System.Windows.Forms.Label();
            this.labelNumOfItemsValue = new System.Windows.Forms.Label();
            this.labelAmount = new System.Windows.Forms.Label();
            this.labelNumberOfItems = new System.Windows.Forms.Label();
            this.labelVendorName = new System.Windows.Forms.Label();
            this.customLabelPONumber = new CustomLabel();
            this.customTextBoxPONumber = new CustomTextBox();
            this.colImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemamount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.retailamount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvPawnItems)).BeginInit();
            this.loanInformation.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.panelPurchase.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "apex_at2014dv.jpg");
            this.imageList.Images.SetKeyName(1, "benelli_nova.jpg");
            this.imageList.Images.SetKeyName(2, "bissell_3576.jpg");
            this.imageList.Images.SetKeyName(3, "bose_301.jpg");
            this.imageList.Images.SetKeyName(4, "bostitch_n64c.jpg");
            this.imageList.Images.SetKeyName(5, "brasseagle_eradicator.jpg");
            this.imageList.Images.SetKeyName(6, "citizen_ecodrive.jpg");
            this.imageList.Images.SetKeyName(7, "compaq_evon600c.jpg");
            this.imageList.Images.SetKeyName(8, "daewoo_dvds151.jpg");
            this.imageList.Images.SetKeyName(9, "echo_srm3100.jpg");
            this.imageList.Images.SetKeyName(10, "glock_30.jpg");
            this.imageList.Images.SetKeyName(11, "haier_hsb03.jpg");
            this.imageList.Images.SetKeyName(12, "huffy_cranbrook.jpg");
            this.imageList.Images.SetKeyName(13, "maestro_mvk441.jpg");
            this.imageList.Images.SetKeyName(14, "mitchell_md100.jpg");
            this.imageList.Images.SetKeyName(15, "motorola_l7c.jpg");
            this.imageList.Images.SetKeyName(16, "nikon_n50.jpg");
            this.imageList.Images.SetKeyName(17, "nintendo_64.jpg");
            this.imageList.Images.SetKeyName(18, "qep_60087.jpg");
            this.imageList.Images.SetKeyName(19, "rca_d52w20.jpg");
            this.imageList.Images.SetKeyName(20, "roland_mc505.jpg");
            this.imageList.Images.SetKeyName(21, "selmer_as300.jpg");
            this.imageList.Images.SetKeyName(22, "shimano_4000fb.jpg");
            this.imageList.Images.SetKeyName(23, "sony_cdx2250.jpg");
            this.imageList.Images.SetKeyName(24, "stihl_ms250.jpg");
            this.imageList.Images.SetKeyName(25, "sylvania_scr4966.jpg");
            this.imageList.Images.SetKeyName(26, "toro_20065.jpg");
            this.imageList.Images.SetKeyName(27, "toshiba_32a35.jpg");
            this.imageList.Images.SetKeyName(28, "bluerectangleglossy.png");
            // 
            // gvPawnItems
            // 
            this.gvPawnItems.AllowUserToAddRows = false;
            this.gvPawnItems.AllowUserToResizeColumns = false;
            this.gvPawnItems.AllowUserToResizeRows = false;
            this.gvPawnItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.gvPawnItems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvPawnItems.BackgroundColor = System.Drawing.Color.White;
            this.gvPawnItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gvPawnItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvPawnItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvPawnItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPawnItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colImage,
            this.Qty,
            this.colDescription,
            this.itemamount,
            this.colAmount,
            this.retailamount});
            this.gvPawnItems.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvPawnItems.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvPawnItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvPawnItems.GridColor = System.Drawing.Color.LightGray;
            this.gvPawnItems.Location = new System.Drawing.Point(15, 77);
            this.gvPawnItems.MultiSelect = false;
            this.gvPawnItems.Name = "gvPawnItems";
            this.gvPawnItems.RowHeadersVisible = false;
            this.gvPawnItems.RowHeadersWidth = 20;
            this.gvPawnItems.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPawnItems.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvPawnItems.RowTemplate.Height = 25;
            this.gvPawnItems.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvPawnItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPawnItems.Size = new System.Drawing.Size(762, 170);
            this.gvPawnItems.StandardTab = true;
            this.gvPawnItems.TabIndex = 1;
            this.gvPawnItems.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvPawnItems_CellMouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(341, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 19);
            this.label1.TabIndex = 40;
            this.label1.Text = "Manage Items";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loanInformation
            // 
            this.loanInformation.BackColor = System.Drawing.Color.Transparent;
            this.loanInformation.Controls.Add(this.aprComboBox);
            this.loanInformation.Controls.Add(this.button1);
            this.loanInformation.Controls.Add(this.aprLabel);
            this.loanInformation.Controls.Add(this.serviceChargesLabel);
            this.loanInformation.Controls.Add(this.totalTextBox);
            this.loanInformation.Controls.Add(this.totalLabel);
            this.loanInformation.Controls.Add(this.financeChargesLabel);
            this.loanInformation.Controls.Add(this.amountFinancedLabel);
            this.loanInformation.Controls.Add(this.interestTextBox);
            this.loanInformation.Controls.Add(this.serviceTextBox);
            this.loanInformation.Controls.Add(this.feesTextBox);
            this.loanInformation.Controls.Add(this.amountFinancedTextBox);
            this.loanInformation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loanInformation.ForeColor = System.Drawing.Color.Black;
            this.loanInformation.Location = new System.Drawing.Point(539, 251);
            this.loanInformation.Name = "loanInformation";
            this.loanInformation.Size = new System.Drawing.Size(241, 148);
            this.loanInformation.TabIndex = 39;
            this.loanInformation.TabStop = false;
            this.loanInformation.Text = "Loan Information";
            // 
            // aprComboBox
            // 
            this.aprComboBox.BackColor = System.Drawing.Color.Gainsboro;
            this.aprComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.aprComboBox.FormattingEnabled = true;
            this.aprComboBox.Items.AddRange(new object[] {
            "60.01"});
            this.aprComboBox.Location = new System.Drawing.Point(122, 122);
            this.aprComboBox.Name = "aprComboBox";
            this.aprComboBox.Size = new System.Drawing.Size(100, 21);
            this.aprComboBox.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(69, 79);
            this.button1.Name = "button1";
            this.button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button1.Size = new System.Drawing.Size(43, 22);
            this.button1.TabIndex = 38;
            this.button1.Text = "Fees";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.feesTextBox_Click);
            // 
            // aprLabel
            // 
            this.aprLabel.AutoSize = true;
            this.aprLabel.BackColor = System.Drawing.Color.Transparent;
            this.aprLabel.Location = new System.Drawing.Point(85, 125);
            this.aprLabel.Name = "aprLabel";
            this.aprLabel.Size = new System.Drawing.Size(31, 13);
            this.aprLabel.TabIndex = 18;
            this.aprLabel.Text = "APR:";
            this.aprLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // serviceChargesLabel
            // 
            this.serviceChargesLabel.AutoSize = true;
            this.serviceChargesLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.serviceChargesLabel.Location = new System.Drawing.Point(70, 61);
            this.serviceChargesLabel.Name = "serviceChargesLabel";
            this.serviceChargesLabel.Size = new System.Drawing.Size(46, 13);
            this.serviceChargesLabel.TabIndex = 16;
            this.serviceChargesLabel.Text = "Service:";
            this.serviceChargesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totalTextBox
            // 
            this.totalTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.totalTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.totalTextBox.Location = new System.Drawing.Point(122, 102);
            this.totalTextBox.Name = "totalTextBox";
            this.totalTextBox.ReadOnly = true;
            this.totalTextBox.Size = new System.Drawing.Size(100, 14);
            this.totalTextBox.TabIndex = 15;
            this.totalTextBox.TabStop = false;
            this.totalTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // totalLabel
            // 
            this.totalLabel.AutoSize = true;
            this.totalLabel.BackColor = System.Drawing.Color.Transparent;
            this.totalLabel.Location = new System.Drawing.Point(81, 104);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(35, 13);
            this.totalLabel.TabIndex = 14;
            this.totalLabel.Text = "Total:";
            this.totalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // financeChargesLabel
            // 
            this.financeChargesLabel.AutoSize = true;
            this.financeChargesLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.financeChargesLabel.Location = new System.Drawing.Point(66, 41);
            this.financeChargesLabel.Name = "financeChargesLabel";
            this.financeChargesLabel.Size = new System.Drawing.Size(50, 13);
            this.financeChargesLabel.TabIndex = 10;
            this.financeChargesLabel.Text = "Interest:";
            this.financeChargesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // amountFinancedLabel
            // 
            this.amountFinancedLabel.AutoSize = true;
            this.amountFinancedLabel.Location = new System.Drawing.Point(22, 21);
            this.amountFinancedLabel.Name = "amountFinancedLabel";
            this.amountFinancedLabel.Size = new System.Drawing.Size(94, 13);
            this.amountFinancedLabel.TabIndex = 7;
            this.amountFinancedLabel.Text = "Amount Financed:";
            this.amountFinancedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // interestTextBox
            // 
            this.interestTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.interestTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.interestTextBox.Location = new System.Drawing.Point(122, 42);
            this.interestTextBox.Name = "interestTextBox";
            this.interestTextBox.ReadOnly = true;
            this.interestTextBox.Size = new System.Drawing.Size(100, 14);
            this.interestTextBox.TabIndex = 4;
            this.interestTextBox.TabStop = false;
            this.interestTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // serviceTextBox
            // 
            this.serviceTextBox.AcceptsReturn = true;
            this.serviceTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.serviceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.serviceTextBox.Location = new System.Drawing.Point(122, 62);
            this.serviceTextBox.Name = "serviceTextBox";
            this.serviceTextBox.ReadOnly = true;
            this.serviceTextBox.Size = new System.Drawing.Size(100, 14);
            this.serviceTextBox.TabIndex = 1;
            this.serviceTextBox.TabStop = false;
            this.serviceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // feesTextBox
            // 
            this.feesTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.feesTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.feesTextBox.Location = new System.Drawing.Point(122, 82);
            this.feesTextBox.Name = "feesTextBox";
            this.feesTextBox.ReadOnly = true;
            this.feesTextBox.Size = new System.Drawing.Size(100, 14);
            this.feesTextBox.TabIndex = 2;
            this.feesTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // amountFinancedTextBox
            // 
            this.amountFinancedTextBox.BackColor = System.Drawing.Color.Gainsboro;
            this.amountFinancedTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.amountFinancedTextBox.Location = new System.Drawing.Point(122, 22);
            this.amountFinancedTextBox.Name = "amountFinancedTextBox";
            this.amountFinancedTextBox.ReadOnly = true;
            this.amountFinancedTextBox.Size = new System.Drawing.Size(100, 14);
            this.amountFinancedTextBox.TabIndex = 0;
            this.amountFinancedTextBox.TabStop = false;
            this.amountFinancedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonPanel
            // 
            this.buttonPanel.BackColor = System.Drawing.Color.Transparent;
            this.buttonPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonPanel.Controls.Add(this.deleteButton);
            this.buttonPanel.Controls.Add(this.cancelButton);
            this.buttonPanel.Controls.Add(this.editButton);
            this.buttonPanel.Controls.Add(this.addButton);
            this.buttonPanel.Controls.Add(this.continueButton);
            this.buttonPanel.ForeColor = System.Drawing.Color.Black;
            this.buttonPanel.Location = new System.Drawing.Point(12, 438);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(768, 55);
            this.buttonPanel.TabIndex = 38;
            this.buttonPanel.TabStop = false;
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.Color.Transparent;
            this.deleteButton.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.deleteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.deleteButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.deleteButton.FlatAppearance.BorderSize = 0;
            this.deleteButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.deleteButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Location = new System.Drawing.Point(433, 6);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(4);
            this.deleteButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.deleteButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(100, 50);
            this.deleteButton.TabIndex = 5;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(34, 6);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.cancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // editButton
            // 
            this.editButton.BackColor = System.Drawing.Color.Transparent;
            this.editButton.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.editButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.editButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.editButton.FlatAppearance.BorderSize = 0;
            this.editButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.editButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editButton.ForeColor = System.Drawing.Color.White;
            this.editButton.Location = new System.Drawing.Point(333, 6);
            this.editButton.Margin = new System.Windows.Forms.Padding(4);
            this.editButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.editButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(100, 50);
            this.editButton.TabIndex = 4;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = false;
            this.editButton.Visible = false;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.Color.Transparent;
            this.addButton.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.addButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.addButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.addButton.FlatAppearance.BorderSize = 0;
            this.addButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.addButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.ForeColor = System.Drawing.Color.White;
            this.addButton.Location = new System.Drawing.Point(233, 6);
            this.addButton.Margin = new System.Windows.Forms.Padding(4);
            this.addButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.addButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(100, 50);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "Add Item";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // continueButton
            // 
            this.continueButton.BackColor = System.Drawing.Color.Transparent;
            this.continueButton.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.continueButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.continueButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.continueButton.FlatAppearance.BorderSize = 0;
            this.continueButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueButton.ForeColor = System.Drawing.Color.White;
            this.continueButton.Location = new System.Drawing.Point(632, 6);
            this.continueButton.Margin = new System.Windows.Forms.Padding(4);
            this.continueButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.continueButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(100, 50);
            this.continueButton.TabIndex = 6;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = false;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // panelPurchase
            // 
            this.panelPurchase.BackColor = System.Drawing.Color.Transparent;
            this.panelPurchase.Controls.Add(this.labelPurchaseAmtvalue);
            this.panelPurchase.Controls.Add(this.labelNumOfItemsValue);
            this.panelPurchase.Controls.Add(this.labelAmount);
            this.panelPurchase.Controls.Add(this.labelNumberOfItems);
            this.panelPurchase.ForeColor = System.Drawing.Color.Transparent;
            this.panelPurchase.Location = new System.Drawing.Point(12, 405);
            this.panelPurchase.Name = "panelPurchase";
            this.panelPurchase.Size = new System.Drawing.Size(768, 39);
            this.panelPurchase.TabIndex = 41;
            // 
            // labelPurchaseAmtvalue
            // 
            this.labelPurchaseAmtvalue.AutoSize = true;
            this.labelPurchaseAmtvalue.ForeColor = System.Drawing.Color.Black;
            this.labelPurchaseAmtvalue.Location = new System.Drawing.Point(676, 14);
            this.labelPurchaseAmtvalue.Name = "labelPurchaseAmtvalue";
            this.labelPurchaseAmtvalue.Size = new System.Drawing.Size(19, 13);
            this.labelPurchaseAmtvalue.TabIndex = 3;
            this.labelPurchaseAmtvalue.Text = "$1";
            // 
            // labelNumOfItemsValue
            // 
            this.labelNumOfItemsValue.AutoSize = true;
            this.labelNumOfItemsValue.ForeColor = System.Drawing.Color.Black;
            this.labelNumOfItemsValue.Location = new System.Drawing.Point(119, 14);
            this.labelNumOfItemsValue.Name = "labelNumOfItemsValue";
            this.labelNumOfItemsValue.Size = new System.Drawing.Size(13, 13);
            this.labelNumOfItemsValue.TabIndex = 2;
            this.labelNumOfItemsValue.Text = "1";
            // 
            // labelAmount
            // 
            this.labelAmount.AutoSize = true;
            this.labelAmount.ForeColor = System.Drawing.Color.Black;
            this.labelAmount.Location = new System.Drawing.Point(549, 14);
            this.labelAmount.Name = "labelAmount";
            this.labelAmount.Size = new System.Drawing.Size(121, 13);
            this.labelAmount.TabIndex = 1;
            this.labelAmount.Text = "Purchase Total Amount:";
            // 
            // labelNumberOfItems
            // 
            this.labelNumberOfItems.AutoSize = true;
            this.labelNumberOfItems.ForeColor = System.Drawing.Color.Black;
            this.labelNumberOfItems.Location = new System.Drawing.Point(25, 14);
            this.labelNumberOfItems.Name = "labelNumberOfItems";
            this.labelNumberOfItems.Size = new System.Drawing.Size(87, 13);
            this.labelNumberOfItems.TabIndex = 0;
            this.labelNumberOfItems.Text = "Number of Items:";
            // 
            // labelVendorName
            // 
            this.labelVendorName.AutoSize = true;
            this.labelVendorName.BackColor = System.Drawing.Color.Transparent;
            this.labelVendorName.ForeColor = System.Drawing.Color.Black;
            this.labelVendorName.Location = new System.Drawing.Point(15, 58);
            this.labelVendorName.Name = "labelVendorName";
            this.labelVendorName.Size = new System.Drawing.Size(64, 13);
            this.labelVendorName.TabIndex = 42;
            this.labelVendorName.Text = "Elite Vendor";
            this.labelVendorName.Visible = false;
            // 
            // customLabelPONumber
            // 
            this.customLabelPONumber.AutoSize = true;
            this.customLabelPONumber.BackColor = System.Drawing.Color.Transparent;
            this.customLabelPONumber.ForeColor = System.Drawing.Color.Black;
            this.customLabelPONumber.Location = new System.Drawing.Point(444, 57);
            this.customLabelPONumber.Name = "customLabelPONumber";
            this.customLabelPONumber.Required = true;
            this.customLabelPONumber.Size = new System.Drawing.Size(101, 13);
            this.customLabelPONumber.TabIndex = 43;
            this.customLabelPONumber.Text = "Purchase Order No:";
            this.customLabelPONumber.Visible = false;
            // 
            // customTextBoxPONumber
            // 
            this.customTextBoxPONumber.CausesValidation = false;
            this.customTextBoxPONumber.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxPONumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxPONumber.Location = new System.Drawing.Point(573, 54);
            this.customTextBoxPONumber.MaxLength = 25;
            this.customTextBoxPONumber.Name = "customTextBoxPONumber";
            this.customTextBoxPONumber.Required = true;
            this.customTextBoxPONumber.Size = new System.Drawing.Size(171, 21);
            this.customTextBoxPONumber.TabIndex = 44;
            this.customTextBoxPONumber.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxPONumber.Visible = false;
            // 
            // colImage
            // 
            this.colImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
            this.colImage.DefaultCellStyle = dataGridViewCellStyle2;
            this.colImage.Description = "Edit";
            this.colImage.HeaderText = global::Pawn.Properties.Resources.OverrideMachineName;
            this.colImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.colImage.Name = "colImage";
            this.colImage.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colImage.ToolTipText = "Click button to Edit Item.";
            this.colImage.Width = 5;
            // 
            // Qty
            // 
            this.Qty.HeaderText = "Quantity";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Visible = false;
            this.Qty.Width = 55;
            // 
            // colDescription
            // 
            this.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDescription.HeaderText = "Merchandise List";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            // 
            // itemamount
            // 
            this.itemamount.HeaderText = "Per Item Amount";
            this.itemamount.Name = "itemamount";
            this.itemamount.ReadOnly = true;
            this.itemamount.Visible = false;
            this.itemamount.Width = 113;
            // 
            // colAmount
            // 
            this.colAmount.HeaderText = "Purchase Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            this.colAmount.Width = 116;
            // 
            // retailamount
            // 
            this.retailamount.HeaderText = "Retail Amount";
            this.retailamount.Name = "retailamount";
            this.retailamount.ReadOnly = true;
            this.retailamount.Visible = false;
            this.retailamount.Width = 99;
            // 
            // ManageMultiplePawnItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(792, 504);
            this.Controls.Add(this.customTextBoxPONumber);
            this.Controls.Add(this.customLabelPONumber);
            this.Controls.Add(this.labelVendorName);
            this.Controls.Add(this.panelPurchase);
            this.Controls.Add(this.gvPawnItems);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loanInformation);
            this.Controls.Add(this.buttonPanel);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Blue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageMultiplePawnItems";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Multiple Pawn Items";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.ManageMultiplePawnItems_Load);
            this.Shown += new System.EventHandler(this.ManageMultiplePawnItems_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gvPawnItems)).EndInit();
            this.loanInformation.ResumeLayout(false);
            this.loanInformation.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            this.panelPurchase.ResumeLayout(false);
            this.panelPurchase.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.DataGridView gvPawnItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox loanInformation;
        private System.Windows.Forms.ComboBox aprComboBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label aprLabel;
        private System.Windows.Forms.Label serviceChargesLabel;
        private System.Windows.Forms.TextBox totalTextBox;
        private System.Windows.Forms.Label totalLabel;
        private System.Windows.Forms.Label financeChargesLabel;
        private System.Windows.Forms.Label amountFinancedLabel;
        private System.Windows.Forms.TextBox interestTextBox;
        private System.Windows.Forms.TextBox serviceTextBox;
        private System.Windows.Forms.TextBox feesTextBox;
        private System.Windows.Forms.TextBox amountFinancedTextBox;
        private System.Windows.Forms.GroupBox buttonPanel;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Panel panelPurchase;
        private System.Windows.Forms.Label labelNumberOfItems;
        private System.Windows.Forms.Label labelAmount;
        private System.Windows.Forms.Label labelPurchaseAmtvalue;
        private System.Windows.Forms.Label labelNumOfItemsValue;
        private System.Windows.Forms.Label labelVendorName;
        private CustomLabel customLabelPONumber;
        private CustomTextBox customTextBoxPONumber;
        private System.Windows.Forms.DataGridViewImageColumn colImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemamount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn retailamount;
    }
}
