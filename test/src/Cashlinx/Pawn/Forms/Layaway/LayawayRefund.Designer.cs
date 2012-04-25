using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Components.EventArgs;

namespace Pawn.Forms.Layaway
{
    partial class LayawayRefund
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayawayRefund));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.titleLabel = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cancelButton = new System.Windows.Forms.Button();
            this.gvPayments = new Common.Libraries.Forms.Components.CustomDataGridView();
            this.colLayawayPaymentNumber = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colDateMade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmployeeNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenderType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmountPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRefundAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLayawayAmount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTransactionDate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLayawayNumber = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblRefundedToDate = new System.Windows.Forms.Label();
            this.lblPaidToDate = new System.Windows.Forms.Label();
            this.lblBalanceOwed = new System.Windows.Forms.Label();
            this.refundButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblBalanceAfterRefundValue = new System.Windows.Forms.Label();
            this.lblBalanceAfterRefund = new System.Windows.Forms.Label();
            this.lblRefundTotalValue = new System.Windows.Forms.Label();
            this.lblRefundTotal = new System.Windows.Forms.Label();
            this.lblRestockingFee = new System.Windows.Forms.Label();
            this.lblRefundAmountValue = new System.Windows.Forms.Label();
            this.lblRefundAmount = new System.Windows.Forms.Label();
            this.txtRestockingFee = new Common.Libraries.Forms.Components.CustomTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gvPayments)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(341, 38);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(124, 19);
            this.titleLabel.TabIndex = 136;
            this.titleLabel.Text = "Layaway Refund";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "colCost";
            this.dataGridViewTextBoxColumn1.FillWeight = 47.31137F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Cost";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "colDescription";
            this.dataGridViewTextBoxColumn2.FillWeight = 12.70586F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Description";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "colRetail";
            this.dataGridViewTextBoxColumn3.FillWeight = 47.31137F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Retail";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "colReason";
            this.dataGridViewTextBoxColumn4.FillWeight = 47.31137F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Reason";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "colTags";
            this.dataGridViewTextBoxColumn5.FillWeight = 47.31137F;
            this.dataGridViewTextBoxColumn5.HeaderText = "# Tags";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.AutoSize = true;
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cancelButton.BackgroundImage")));
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(12, 573);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 40);
            this.cancelButton.TabIndex = 145;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // gvPayments
            // 
            this.gvPayments.AllowUserToAddRows = false;
            this.gvPayments.AllowUserToDeleteRows = false;
            this.gvPayments.AllowUserToResizeColumns = false;
            this.gvPayments.AllowUserToResizeRows = false;
            this.gvPayments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvPayments.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvPayments.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvPayments.CausesValidation = false;
            this.gvPayments.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPayments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPayments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLayawayPaymentNumber,
            this.colDateMade,
            this.colDueDate,
            this.colEmployeeNumber,
            this.colTenderType,
            this.colAmountPaid,
            this.colRefundAmount});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvPayments.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvPayments.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvPayments.GridColor = System.Drawing.Color.LightGray;
            this.gvPayments.Location = new System.Drawing.Point(12, 182);
            this.gvPayments.Margin = new System.Windows.Forms.Padding(0);
            this.gvPayments.MultiSelect = false;
            this.gvPayments.Name = "gvPayments";
            this.gvPayments.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPayments.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvPayments.RowHeadersVisible = false;
            this.gvPayments.RowHeadersWidth = 20;
            this.gvPayments.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPayments.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPayments.Size = new System.Drawing.Size(783, 280);
            this.gvPayments.TabIndex = 146;
            this.gvPayments.GridViewRowSelecting += new System.EventHandler<Common.Libraries.Forms.Components.EventArgs.GridViewRowSelectingEventArgs>(this.gvPayments_GridViewRowSelecting);
            this.gvPayments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPayments_CellClick);
            // 
            // colLayawayPaymentNumber
            // 
            this.colLayawayPaymentNumber.ActiveLinkColor = System.Drawing.Color.Blue;
            this.colLayawayPaymentNumber.HeaderText = "Layaway Payment #";
            this.colLayawayPaymentNumber.Name = "colLayawayPaymentNumber";
            this.colLayawayPaymentNumber.ReadOnly = true;
            this.colLayawayPaymentNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colLayawayPaymentNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colLayawayPaymentNumber.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // colDateMade
            // 
            this.colDateMade.HeaderText = "Date Made";
            this.colDateMade.Name = "colDateMade";
            this.colDateMade.ReadOnly = true;
            // 
            // colDueDate
            // 
            this.colDueDate.HeaderText = "Due Date";
            this.colDueDate.Name = "colDueDate";
            this.colDueDate.ReadOnly = true;
            // 
            // colEmployeeNumber
            // 
            this.colEmployeeNumber.HeaderText = "Employee #";
            this.colEmployeeNumber.Name = "colEmployeeNumber";
            this.colEmployeeNumber.ReadOnly = true;
            // 
            // colTenderType
            // 
            this.colTenderType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colTenderType.HeaderText = "Tender Type";
            this.colTenderType.Name = "colTenderType";
            this.colTenderType.ReadOnly = true;
            this.colTenderType.Width = 86;
            // 
            // colAmountPaid
            // 
            this.colAmountPaid.HeaderText = "Amount Paid";
            this.colAmountPaid.Name = "colAmountPaid";
            this.colAmountPaid.ReadOnly = true;
            // 
            // colRefundAmount
            // 
            this.colRefundAmount.HeaderText = "Refund Amount";
            this.colRefundAmount.Name = "colRefundAmount";
            this.colRefundAmount.ReadOnly = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.24138F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.09195F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.60281F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.64623F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.62452F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.66539F));
            this.tableLayoutPanel1.Controls.Add(this.lblLayawayAmount, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTransactionDate, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblLayawayNumber, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCustomerName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblRefundedToDate, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPaidToDate, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblBalanceOwed, 5, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 77);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(783, 99);
            this.tableLayoutPanel1.TabIndex = 147;
            // 
            // lblLayawayAmount
            // 
            this.lblLayawayAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLayawayAmount.AutoSize = true;
            this.lblLayawayAmount.Location = new System.Drawing.Point(678, 5);
            this.lblLayawayAmount.Name = "lblLayawayAmount";
            this.lblLayawayAmount.Size = new System.Drawing.Size(38, 13);
            this.lblLayawayAmount.TabIndex = 5;
            this.lblLayawayAmount.Text = "$ 0.00";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(564, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Layaway Amount:";
            // 
            // lblTransactionDate
            // 
            this.lblTransactionDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTransactionDate.AutoSize = true;
            this.lblTransactionDate.Location = new System.Drawing.Point(394, 5);
            this.lblTransactionDate.Name = "lblTransactionDate";
            this.lblTransactionDate.Size = new System.Drawing.Size(63, 13);
            this.lblTransactionDate.TabIndex = 3;
            this.lblTransactionDate.Text = "11/08/2010";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(281, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Transaction Date:";
            // 
            // lblLayawayNumber
            // 
            this.lblLayawayNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLayawayNumber.AutoSize = true;
            this.lblLayawayNumber.Location = new System.Drawing.Point(138, 5);
            this.lblLayawayNumber.Name = "lblLayawayNumber";
            this.lblLayawayNumber.Size = new System.Drawing.Size(25, 13);
            this.lblLayawayNumber.TabIndex = 1;
            this.lblLayawayNumber.Text = "291";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Layaway #:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Customer Name:";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Location = new System.Drawing.Point(138, 29);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(63, 13);
            this.lblCustomerName.TabIndex = 7;
            this.lblCustomerName.Text = "Jerry Jones";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(561, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Refunded To Date:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(591, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Paid To Date:";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(584, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Balance Owed:";
            // 
            // lblRefundedToDate
            // 
            this.lblRefundedToDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRefundedToDate.AutoSize = true;
            this.lblRefundedToDate.Location = new System.Drawing.Point(678, 29);
            this.lblRefundedToDate.Name = "lblRefundedToDate";
            this.lblRefundedToDate.Size = new System.Drawing.Size(38, 13);
            this.lblRefundedToDate.TabIndex = 11;
            this.lblRefundedToDate.Text = "$ 0.00";
            // 
            // lblPaidToDate
            // 
            this.lblPaidToDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPaidToDate.AutoSize = true;
            this.lblPaidToDate.Location = new System.Drawing.Point(678, 53);
            this.lblPaidToDate.Name = "lblPaidToDate";
            this.lblPaidToDate.Size = new System.Drawing.Size(38, 13);
            this.lblPaidToDate.TabIndex = 12;
            this.lblPaidToDate.Text = "$ 0.00";
            // 
            // lblBalanceOwed
            // 
            this.lblBalanceOwed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBalanceOwed.AutoSize = true;
            this.lblBalanceOwed.Location = new System.Drawing.Point(678, 79);
            this.lblBalanceOwed.Name = "lblBalanceOwed";
            this.lblBalanceOwed.Size = new System.Drawing.Size(38, 13);
            this.lblBalanceOwed.TabIndex = 13;
            this.lblBalanceOwed.Text = "$ 0.00";
            // 
            // refundButton
            // 
            this.refundButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.refundButton.AutoSize = true;
            this.refundButton.BackColor = System.Drawing.Color.Transparent;
            this.refundButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("refundButton.BackgroundImage")));
            this.refundButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.refundButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.refundButton.FlatAppearance.BorderSize = 0;
            this.refundButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.refundButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.refundButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refundButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refundButton.ForeColor = System.Drawing.Color.White;
            this.refundButton.Location = new System.Drawing.Point(695, 573);
            this.refundButton.Name = "refundButton";
            this.refundButton.Size = new System.Drawing.Size(100, 40);
            this.refundButton.TabIndex = 148;
            this.refundButton.Text = "Refund";
            this.refundButton.UseVisualStyleBackColor = false;
            this.refundButton.Click += new System.EventHandler(this.refundButton_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 661F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 121F));
            this.tableLayoutPanel2.Controls.Add(this.lblBalanceAfterRefundValue, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblBalanceAfterRefund, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblRefundTotalValue, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblRefundTotal, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblRestockingFee, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblRefundAmountValue, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblRefundAmount, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtRestockingFee, 1, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(13, 468);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(782, 100);
            this.tableLayoutPanel2.TabIndex = 149;
            // 
            // lblBalanceAfterRefundValue
            // 
            this.lblBalanceAfterRefundValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblBalanceAfterRefundValue.AutoSize = true;
            this.lblBalanceAfterRefundValue.Location = new System.Drawing.Point(741, 81);
            this.lblBalanceAfterRefundValue.Name = "lblBalanceAfterRefundValue";
            this.lblBalanceAfterRefundValue.Size = new System.Drawing.Size(38, 13);
            this.lblBalanceAfterRefundValue.TabIndex = 7;
            this.lblBalanceAfterRefundValue.Text = "$ 0.00";
            // 
            // lblBalanceAfterRefund
            // 
            this.lblBalanceAfterRefund.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblBalanceAfterRefund.AutoSize = true;
            this.lblBalanceAfterRefund.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceAfterRefund.Location = new System.Drawing.Point(529, 81);
            this.lblBalanceAfterRefund.Name = "lblBalanceAfterRefund";
            this.lblBalanceAfterRefund.Size = new System.Drawing.Size(129, 13);
            this.lblBalanceAfterRefund.TabIndex = 6;
            this.lblBalanceAfterRefund.Text = "Balance After Refund:";
            // 
            // lblRefundTotalValue
            // 
            this.lblRefundTotalValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRefundTotalValue.AutoSize = true;
            this.lblRefundTotalValue.Location = new System.Drawing.Point(741, 56);
            this.lblRefundTotalValue.Name = "lblRefundTotalValue";
            this.lblRefundTotalValue.Size = new System.Drawing.Size(38, 13);
            this.lblRefundTotalValue.TabIndex = 5;
            this.lblRefundTotalValue.Text = "$ 0.00";
            // 
            // lblRefundTotal
            // 
            this.lblRefundTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRefundTotal.AutoSize = true;
            this.lblRefundTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefundTotal.Location = new System.Drawing.Point(576, 56);
            this.lblRefundTotal.Name = "lblRefundTotal";
            this.lblRefundTotal.Size = new System.Drawing.Size(82, 13);
            this.lblRefundTotal.TabIndex = 4;
            this.lblRefundTotal.Text = "Refund Total:";
            // 
            // lblRestockingFee
            // 
            this.lblRestockingFee.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRestockingFee.AutoSize = true;
            this.lblRestockingFee.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRestockingFee.Location = new System.Drawing.Point(562, 31);
            this.lblRestockingFee.Name = "lblRestockingFee";
            this.lblRestockingFee.Size = new System.Drawing.Size(96, 13);
            this.lblRestockingFee.TabIndex = 2;
            this.lblRestockingFee.Text = "Restocking Fee:";
            // 
            // lblRefundAmountValue
            // 
            this.lblRefundAmountValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRefundAmountValue.AutoSize = true;
            this.lblRefundAmountValue.Location = new System.Drawing.Point(741, 6);
            this.lblRefundAmountValue.Name = "lblRefundAmountValue";
            this.lblRefundAmountValue.Size = new System.Drawing.Size(38, 13);
            this.lblRefundAmountValue.TabIndex = 1;
            this.lblRefundAmountValue.Text = "$ 0.00";
            // 
            // lblRefundAmount
            // 
            this.lblRefundAmount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRefundAmount.AutoSize = true;
            this.lblRefundAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefundAmount.Location = new System.Drawing.Point(560, 6);
            this.lblRefundAmount.Name = "lblRefundAmount";
            this.lblRefundAmount.Size = new System.Drawing.Size(98, 13);
            this.lblRefundAmount.TabIndex = 0;
            this.lblRefundAmount.Text = "Refund Amount:";
            // 
            // txtRestockingFee
            // 
            this.txtRestockingFee.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtRestockingFee.CausesValidation = false;
            this.txtRestockingFee.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.txtRestockingFee.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRestockingFee.Location = new System.Drawing.Point(679, 28);
            this.txtRestockingFee.Name = "txtRestockingFee";
            this.txtRestockingFee.Size = new System.Drawing.Size(100, 21);
            this.txtRestockingFee.TabIndex = 8;
            this.txtRestockingFee.Text = "0.00";
            this.txtRestockingFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRestockingFee.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.txtRestockingFee.TextChanged += new System.EventHandler(this.txtRestockingFee_TextChanged);
            this.txtRestockingFee.Leave += new System.EventHandler(this.txtRestockingFee_Leave);
            // 
            // LayawayRefund
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(807, 625);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.refundButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.gvPayments);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.titleLabel);
            this.Name = "LayawayRefund";
            this.Load += new System.EventHandler(this.LayawayRefund_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvPayments)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Button cancelButton;
        private CustomDataGridView gvPayments;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLayawayNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTransactionDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblLayawayAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblRefundedToDate;
        private System.Windows.Forms.Label lblPaidToDate;
        private System.Windows.Forms.Label lblBalanceOwed;
        private System.Windows.Forms.Button refundButton;
        private System.Windows.Forms.DataGridViewLinkColumn colLayawayPaymentNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateMade;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmployeeNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenderType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmountPaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRefundAmount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblRefundAmount;
        private System.Windows.Forms.Label lblRefundAmountValue;
        private System.Windows.Forms.Label lblRestockingFee;
        private System.Windows.Forms.Label lblRefundTotal;
        private System.Windows.Forms.Label lblRefundTotalValue;
        private System.Windows.Forms.Label lblBalanceAfterRefund;
        private System.Windows.Forms.Label lblBalanceAfterRefundValue;
        private CustomTextBox txtRestockingFee;
    }
}
