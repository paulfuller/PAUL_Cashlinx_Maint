using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.MerchandiseTransfer
{
    partial class SelectTransferInItems
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.gvTransfers = new CustomDataGridView();
            this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIcn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRefurbNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnComplete = new System.Windows.Forms.Button();
            this.btnRejectTransfer = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanelAddress = new System.Windows.Forms.TableLayoutPanel();
            this.lblCityStateZip = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblShopNumber = new System.Windows.Forms.Label();
            this.lblTransferNumber = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCarrier = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTransferType = new System.Windows.Forms.Label();
            this.flowLayoutPanelIcnEntry = new System.Windows.Forms.FlowLayoutPanel();
            this.lblEnterOrScanIcn = new System.Windows.Forms.Label();
            this.txtShopNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTransactionNumber = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tableLayoutPanelTotals = new System.Windows.Forms.TableLayoutPanel();
            this.lblTotalCostTransfered = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTotalItemsTransfered = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblTotalCostReceived = new System.Windows.Forms.Label();
            this.lblTotalItemsReceived = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblComments = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.flowLayoutPanelComments = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransfers)).BeginInit();
            this.tableLayoutPanelAddress.SuspendLayout();
            this.flowLayoutPanelIcnEntry.SuspendLayout();
            this.tableLayoutPanelTotals.SuspendLayout();
            this.flowLayoutPanelComments.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(255, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Merchandise Transfer In";
            // 
            // gvTransfers
            // 
            this.gvTransfers.AllowUserToAddRows = false;
            this.gvTransfers.AllowUserToDeleteRows = false;
            this.gvTransfers.AllowUserToResizeColumns = false;
            this.gvTransfers.AllowUserToResizeRows = false;
            this.gvTransfers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvTransfers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvTransfers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvTransfers.BackgroundColor = System.Drawing.Color.White;
            this.gvTransfers.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvTransfers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvTransfers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTransfers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNumber,
            this.colIcn,
            this.colRefurbNumber,
            this.colDescription,
            this.colQuantity,
            this.colCost});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvTransfers.DefaultCellStyle = dataGridViewCellStyle5;
            this.gvTransfers.GridColor = System.Drawing.Color.White;
            this.gvTransfers.Location = new System.Drawing.Point(12, 282);
            this.gvTransfers.Margin = new System.Windows.Forms.Padding(0);
            this.gvTransfers.MultiSelect = false;
            this.gvTransfers.Name = "gvTransfers";
            this.gvTransfers.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvTransfers.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gvTransfers.RowHeadersVisible = false;
            this.gvTransfers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvTransfers.ShowEditingIcon = false;
            this.gvTransfers.Size = new System.Drawing.Size(706, 256);
            this.gvTransfers.TabIndex = 1;
            // 
            // colNumber
            // 
            this.colNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.colNumber.DefaultCellStyle = dataGridViewCellStyle2;
            this.colNumber.HeaderText = "Number";
            this.colNumber.Name = "colNumber";
            this.colNumber.ReadOnly = true;
            this.colNumber.Width = 69;
            // 
            // colIcn
            // 
            this.colIcn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.colIcn.DefaultCellStyle = dataGridViewCellStyle3;
            this.colIcn.HeaderText = "ICN #";
            this.colIcn.Name = "colIcn";
            this.colIcn.ReadOnly = true;
            this.colIcn.Width = 61;
            // 
            // colRefurbNumber
            // 
            this.colRefurbNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colRefurbNumber.HeaderText = "Refurb #";
            this.colRefurbNumber.Name = "colRefurbNumber";
            this.colRefurbNumber.ReadOnly = true;
            this.colRefurbNumber.Width = 76;
            // 
            // colDescription
            // 
            this.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colDescription.DefaultCellStyle = dataGridViewCellStyle4;
            this.colDescription.HeaderText = "Merchandise Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            // 
            // colQuantity
            // 
            this.colQuantity.HeaderText = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.ReadOnly = true;
            // 
            // colCost
            // 
            this.colCost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCost.HeaderText = "Cost";
            this.colCost.Name = "colCost";
            this.colCost.ReadOnly = true;
            this.colCost.Width = 54;
            // 
            // btnComplete
            // 
            this.btnComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComplete.AutoSize = true;
            this.btnComplete.BackColor = System.Drawing.Color.Transparent;
            this.btnComplete.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.btnComplete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnComplete.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnComplete.FlatAppearance.BorderSize = 0;
            this.btnComplete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnComplete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnComplete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComplete.ForeColor = System.Drawing.Color.White;
            this.btnComplete.Location = new System.Drawing.Point(618, 544);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(100, 40);
            this.btnComplete.TabIndex = 145;
            this.btnComplete.Text = "Complete";
            this.btnComplete.UseVisualStyleBackColor = false;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // btnRejectTransfer
            // 
            this.btnRejectTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRejectTransfer.AutoSize = true;
            this.btnRejectTransfer.BackColor = System.Drawing.Color.Transparent;
            this.btnRejectTransfer.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.btnRejectTransfer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRejectTransfer.Enabled = false;
            this.btnRejectTransfer.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnRejectTransfer.FlatAppearance.BorderSize = 0;
            this.btnRejectTransfer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRejectTransfer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRejectTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRejectTransfer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRejectTransfer.ForeColor = System.Drawing.Color.White;
            this.btnRejectTransfer.Location = new System.Drawing.Point(492, 544);
            this.btnRejectTransfer.Name = "btnRejectTransfer";
            this.btnRejectTransfer.Size = new System.Drawing.Size(129, 40);
            this.btnRejectTransfer.TabIndex = 146;
            this.btnRejectTransfer.Text = "Reject Transfer";
            this.btnRejectTransfer.UseVisualStyleBackColor = false;
            this.btnRejectTransfer.Click += new System.EventHandler(this.btnRejectTransfer_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(12, 544);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 148;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tableLayoutPanelAddress
            // 
            this.tableLayoutPanelAddress.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelAddress.ColumnCount = 2;
            this.tableLayoutPanelAddress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelAddress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelAddress.Controls.Add(this.lblCityStateZip, 0, 3);
            this.tableLayoutPanelAddress.Controls.Add(this.lblAddress, 0, 2);
            this.tableLayoutPanelAddress.Controls.Add(this.lblShopNumber, 0, 1);
            this.tableLayoutPanelAddress.Controls.Add(this.lblTransferNumber, 0, 0);
            this.tableLayoutPanelAddress.Controls.Add(this.label4, 0, 5);
            this.tableLayoutPanelAddress.Controls.Add(this.label5, 0, 7);
            this.tableLayoutPanelAddress.Controls.Add(this.lblCarrier, 1, 5);
            this.tableLayoutPanelAddress.Controls.Add(this.lblStatus, 1, 7);
            this.tableLayoutPanelAddress.Controls.Add(this.label2, 0, 6);
            this.tableLayoutPanelAddress.Controls.Add(this.lblTransferType, 1, 6);
            this.tableLayoutPanelAddress.Location = new System.Drawing.Point(12, 73);
            this.tableLayoutPanelAddress.Name = "tableLayoutPanelAddress";
            this.tableLayoutPanelAddress.RowCount = 8;
            this.tableLayoutPanelAddress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanelAddress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanelAddress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanelAddress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanelAddress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanelAddress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanelAddress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanelAddress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanelAddress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelAddress.Size = new System.Drawing.Size(252, 143);
            this.tableLayoutPanelAddress.TabIndex = 149;
            // 
            // lblCityStateZip
            // 
            this.lblCityStateZip.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCityStateZip.AutoSize = true;
            this.lblCityStateZip.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelAddress.SetColumnSpan(this.lblCityStateZip, 2);
            this.lblCityStateZip.Location = new System.Drawing.Point(3, 53);
            this.lblCityStateZip.Name = "lblCityStateZip";
            this.lblCityStateZip.Size = new System.Drawing.Size(80, 13);
            this.lblCityStateZip.TabIndex = 3;
            this.lblCityStateZip.Text = "City, State, Zip";
            // 
            // lblAddress
            // 
            this.lblAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAddress.AutoSize = true;
            this.lblAddress.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelAddress.SetColumnSpan(this.lblAddress, 2);
            this.lblAddress.Location = new System.Drawing.Point(3, 36);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(46, 13);
            this.lblAddress.TabIndex = 2;
            this.lblAddress.Text = "Address";
            // 
            // lblShopNumber
            // 
            this.lblShopNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblShopNumber.AutoSize = true;
            this.lblShopNumber.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelAddress.SetColumnSpan(this.lblShopNumber, 2);
            this.lblShopNumber.Location = new System.Drawing.Point(3, 19);
            this.lblShopNumber.Name = "lblShopNumber";
            this.lblShopNumber.Size = new System.Drawing.Size(39, 13);
            this.lblShopNumber.TabIndex = 1;
            this.lblShopNumber.Text = "Shop#";
            // 
            // lblTransferNumber
            // 
            this.lblTransferNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTransferNumber.AutoSize = true;
            this.lblTransferNumber.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelAddress.SetColumnSpan(this.lblTransferNumber, 2);
            this.lblTransferNumber.Location = new System.Drawing.Point(3, 2);
            this.lblTransferNumber.Name = "lblTransferNumber";
            this.lblTransferNumber.Size = new System.Drawing.Size(56, 13);
            this.lblTransferNumber.TabIndex = 0;
            this.lblTransferNumber.Text = "Transfer#";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(3, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Carrier:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Status:";
            // 
            // lblCarrier
            // 
            this.lblCarrier.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCarrier.AutoSize = true;
            this.lblCarrier.BackColor = System.Drawing.Color.Transparent;
            this.lblCarrier.Location = new System.Drawing.Point(129, 87);
            this.lblCarrier.Name = "lblCarrier";
            this.lblCarrier.Size = new System.Drawing.Size(37, 13);
            this.lblCarrier.TabIndex = 6;
            this.lblCarrier.Text = "Fedex";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(129, 124);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(52, 13);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Created";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Transfer Type:";
            // 
            // lblTransferType
            // 
            this.lblTransferType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTransferType.AutoSize = true;
            this.lblTransferType.Location = new System.Drawing.Point(129, 104);
            this.lblTransferType.Name = "lblTransferType";
            this.lblTransferType.Size = new System.Drawing.Size(81, 13);
            this.lblTransferType.TabIndex = 9;
            this.lblTransferType.Text = "SHOP TO SHOP";
            // 
            // flowLayoutPanelIcnEntry
            // 
            this.flowLayoutPanelIcnEntry.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanelIcnEntry.Controls.Add(this.lblEnterOrScanIcn);
            this.flowLayoutPanelIcnEntry.Controls.Add(this.txtShopNumber);
            this.flowLayoutPanelIcnEntry.Controls.Add(this.label3);
            this.flowLayoutPanelIcnEntry.Controls.Add(this.txtTransactionNumber);
            this.flowLayoutPanelIcnEntry.Controls.Add(this.btnAdd);
            this.flowLayoutPanelIcnEntry.Location = new System.Drawing.Point(13, 231);
            this.flowLayoutPanelIcnEntry.Name = "flowLayoutPanelIcnEntry";
            this.flowLayoutPanelIcnEntry.Size = new System.Drawing.Size(390, 48);
            this.flowLayoutPanelIcnEntry.TabIndex = 151;
            // 
            // lblEnterOrScanIcn
            // 
            this.lblEnterOrScanIcn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEnterOrScanIcn.AutoSize = true;
            this.lblEnterOrScanIcn.BackColor = System.Drawing.Color.Transparent;
            this.lblEnterOrScanIcn.Location = new System.Drawing.Point(3, 16);
            this.lblEnterOrScanIcn.Name = "lblEnterOrScanIcn";
            this.lblEnterOrScanIcn.Size = new System.Drawing.Size(101, 13);
            this.lblEnterOrScanIcn.TabIndex = 0;
            this.lblEnterOrScanIcn.Text = "Enter or Scan ICN#";
            // 
            // txtShopNumber
            // 
            this.txtShopNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtShopNumber.Enabled = false;
            this.txtShopNumber.Location = new System.Drawing.Point(110, 12);
            this.txtShopNumber.MaxLength = 5;
            this.txtShopNumber.Name = "txtShopNumber";
            this.txtShopNumber.Size = new System.Drawing.Size(41, 21);
            this.txtShopNumber.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(157, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "-";
            // 
            // txtTransactionNumber
            // 
            this.txtTransactionNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtTransactionNumber.Location = new System.Drawing.Point(174, 12);
            this.txtTransactionNumber.Name = "txtTransactionNumber";
            this.txtTransactionNumber.Size = new System.Drawing.Size(100, 21);
            this.txtTransactionNumber.TabIndex = 7;
            this.txtTransactionNumber.TextChanged += new System.EventHandler(this.txtTransactionNumber_TextChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAdd.AutoSize = true;
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(280, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 40);
            this.btnAdd.TabIndex = 149;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tableLayoutPanelTotals
            // 
            this.tableLayoutPanelTotals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelTotals.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelTotals.ColumnCount = 2;
            this.tableLayoutPanelTotals.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 109F));
            this.tableLayoutPanelTotals.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanelTotals.Controls.Add(this.lblTotalCostTransfered, 1, 1);
            this.tableLayoutPanelTotals.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanelTotals.Controls.Add(this.lblTotalItemsTransfered, 1, 0);
            this.tableLayoutPanelTotals.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanelTotals.Controls.Add(this.label14, 0, 4);
            this.tableLayoutPanelTotals.Controls.Add(this.lblTotalCostReceived, 1, 4);
            this.tableLayoutPanelTotals.Controls.Add(this.lblTotalItemsReceived, 1, 3);
            this.tableLayoutPanelTotals.Controls.Add(this.label12, 0, 3);
            this.tableLayoutPanelTotals.Location = new System.Drawing.Point(429, 75);
            this.tableLayoutPanelTotals.Name = "tableLayoutPanelTotals";
            this.tableLayoutPanelTotals.RowCount = 5;
            this.tableLayoutPanelTotals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelTotals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelTotals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelTotals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelTotals.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelTotals.Size = new System.Drawing.Size(289, 114);
            this.tableLayoutPanelTotals.TabIndex = 152;
            // 
            // lblTotalCostTransfered
            // 
            this.lblTotalCostTransfered.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTotalCostTransfered.AutoSize = true;
            this.lblTotalCostTransfered.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalCostTransfered.Location = new System.Drawing.Point(245, 26);
            this.lblTotalCostTransfered.Name = "lblTotalCostTransfered";
            this.lblTotalCostTransfered.Size = new System.Drawing.Size(41, 13);
            this.lblTotalCostTransfered.TabIndex = 3;
            this.lblTotalCostTransfered.Text = "label11";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(3, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Total Cost:";
            // 
            // lblTotalItemsTransfered
            // 
            this.lblTotalItemsTransfered.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTotalItemsTransfered.AutoSize = true;
            this.lblTotalItemsTransfered.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalItemsTransfered.Location = new System.Drawing.Point(251, 4);
            this.lblTotalItemsTransfered.Name = "lblTotalItemsTransfered";
            this.lblTotalItemsTransfered.Size = new System.Drawing.Size(35, 13);
            this.lblTotalItemsTransfered.TabIndex = 1;
            this.lblTotalItemsTransfered.Text = "label9";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(3, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Total # of Items:";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Location = new System.Drawing.Point(3, 94);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(60, 13);
            this.label14.TabIndex = 6;
            this.label14.Text = "Total Cost:";
            // 
            // lblTotalCostReceived
            // 
            this.lblTotalCostReceived.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTotalCostReceived.AutoSize = true;
            this.lblTotalCostReceived.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalCostReceived.Location = new System.Drawing.Point(245, 94);
            this.lblTotalCostReceived.Name = "lblTotalCostReceived";
            this.lblTotalCostReceived.Size = new System.Drawing.Size(41, 13);
            this.lblTotalCostReceived.TabIndex = 7;
            this.lblTotalCostReceived.Text = "label15";
            // 
            // lblTotalItemsReceived
            // 
            this.lblTotalItemsReceived.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTotalItemsReceived.AutoSize = true;
            this.lblTotalItemsReceived.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalItemsReceived.Location = new System.Drawing.Point(245, 70);
            this.lblTotalItemsReceived.Name = "lblTotalItemsReceived";
            this.lblTotalItemsReceived.Size = new System.Drawing.Size(41, 13);
            this.lblTotalItemsReceived.TabIndex = 5;
            this.lblTotalItemsReceived.Text = "label13";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(3, 70);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Received Items:";
            // 
            // lblComments
            // 
            this.lblComments.AutoSize = true;
            this.lblComments.BackColor = System.Drawing.Color.Transparent;
            this.lblComments.Location = new System.Drawing.Point(3, 0);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(61, 13);
            this.lblComments.TabIndex = 153;
            this.lblComments.Text = "Comments:";
            // 
            // txtComments
            // 
            this.txtComments.Location = new System.Drawing.Point(3, 16);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(283, 61);
            this.txtComments.TabIndex = 154;
            // 
            // flowLayoutPanelComments
            // 
            this.flowLayoutPanelComments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelComments.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanelComments.Controls.Add(this.lblComments);
            this.flowLayoutPanelComments.Controls.Add(this.txtComments);
            this.flowLayoutPanelComments.Location = new System.Drawing.Point(429, 197);
            this.flowLayoutPanelComments.Name = "flowLayoutPanelComments";
            this.flowLayoutPanelComments.Size = new System.Drawing.Size(289, 82);
            this.flowLayoutPanelComments.TabIndex = 155;
            // 
            // SelectTransferInItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_512_BlueScale;
            this.ClientSize = new System.Drawing.Size(730, 596);
            this.Controls.Add(this.flowLayoutPanelComments);
            this.Controls.Add(this.tableLayoutPanelTotals);
            this.Controls.Add(this.flowLayoutPanelIcnEntry);
            this.Controls.Add(this.tableLayoutPanelAddress);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRejectTransfer);
            this.Controls.Add(this.btnComplete);
            this.Controls.Add(this.gvTransfers);
            this.Controls.Add(this.label1);
            this.Name = "SelectTransferInItems";
            this.Text = "SelectTransferInItems";
            this.Shown += new System.EventHandler(this.SelectTransferInItems_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gvTransfers)).EndInit();
            this.tableLayoutPanelAddress.ResumeLayout(false);
            this.tableLayoutPanelAddress.PerformLayout();
            this.flowLayoutPanelIcnEntry.ResumeLayout(false);
            this.flowLayoutPanelIcnEntry.PerformLayout();
            this.tableLayoutPanelTotals.ResumeLayout(false);
            this.tableLayoutPanelTotals.PerformLayout();
            this.flowLayoutPanelComments.ResumeLayout(false);
            this.flowLayoutPanelComments.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private CustomDataGridView gvTransfers;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.Button btnRejectTransfer;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelAddress;
        private System.Windows.Forms.Label lblTransferNumber;
        private System.Windows.Forms.Label lblShopNumber;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblCityStateZip;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelIcnEntry;
        private System.Windows.Forms.Label lblEnterOrScanIcn;
        private System.Windows.Forms.TextBox txtShopNumber;
        private System.Windows.Forms.TextBox txtTransactionNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCarrier;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTotals;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTotalItemsTransfered;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTotalCostTransfered;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblTotalItemsReceived;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblTotalCostReceived;
        private System.Windows.Forms.Label lblComments;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelComments;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTransferType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIcn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRefurbNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCost;
    }
}