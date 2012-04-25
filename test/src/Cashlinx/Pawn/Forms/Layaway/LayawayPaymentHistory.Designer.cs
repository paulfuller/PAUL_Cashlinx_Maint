namespace Pawn.Forms.Layaway
{
    partial class LayawayPaymentHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.titleLabel = new System.Windows.Forms.Label();
            this.printButton = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.closeButton = new System.Windows.Forms.Button();
            this.gvPayments = new System.Windows.Forms.DataGridView();
            this.colPaymentDueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentAmountDue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentMadeOn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentAmountMade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBalanceDue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReceiptNumber = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTotalLayaway = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblAmountOutstanding = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPaidToDate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tooltipPanel = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.gvPayments)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(306, 38);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(194, 19);
            this.titleLabel.TabIndex = 136;
            this.titleLabel.Text = "Payment Schedule/History";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // printButton
            // 
            this.printButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.printButton.AutoSize = true;
            this.printButton.BackColor = System.Drawing.Color.Transparent;
            this.printButton.BackgroundImage = Common.Properties.Resources.blueglossy_small;
            this.printButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.printButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.printButton.FlatAppearance.BorderSize = 0;
            this.printButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.printButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.printButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printButton.ForeColor = System.Drawing.Color.White;
            this.printButton.Location = new System.Drawing.Point(598, 456);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(100, 40);
            this.printButton.TabIndex = 144;
            this.printButton.Text = "Print";
            this.printButton.UseVisualStyleBackColor = false;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
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
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.closeButton.AutoSize = true;
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.BackgroundImage = Common.Properties.Resources.blueglossy_small;
            this.closeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(695, 456);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(100, 40);
            this.closeButton.TabIndex = 145;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // gvPayments
            // 
            this.gvPayments.AllowUserToAddRows = false;
            this.gvPayments.AllowUserToDeleteRows = false;
            this.gvPayments.AllowUserToResizeColumns = false;
            this.gvPayments.AllowUserToResizeRows = false;
            this.gvPayments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gvPayments.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvPayments.BackgroundColor = System.Drawing.Color.WhiteSmoke;
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
            this.colPaymentDueDate,
            this.colPaymentAmountDue,
            this.colPaymentMadeOn,
            this.colPaymentAmountMade,
            this.colBalanceDue,
            this.colReceiptNumber,
            this.colStatus});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvPayments.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvPayments.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvPayments.GridColor = System.Drawing.Color.LightGray;
            this.gvPayments.Location = new System.Drawing.Point(12, 103);
            this.gvPayments.MultiSelect = false;
            this.gvPayments.Name = "gvPayments";
            this.gvPayments.ReadOnly = true;
            this.gvPayments.RowHeadersVisible = false;
            this.gvPayments.RowHeadersWidth = 20;
            this.gvPayments.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPayments.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gvPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPayments.Size = new System.Drawing.Size(783, 347);
            this.gvPayments.TabIndex = 146;
            this.gvPayments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPayments_CellClick);
            this.gvPayments.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPayments_CellMouseEnter);
            this.gvPayments.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvPayments_CellMouseLeave);
            // 
            // colPaymentDueDate
            // 
            this.colPaymentDueDate.HeaderText = "Payment Due Date";
            this.colPaymentDueDate.Name = "colPaymentDueDate";
            this.colPaymentDueDate.ReadOnly = true;
            this.colPaymentDueDate.Width = 91;
            // 
            // colPaymentAmountDue
            // 
            this.colPaymentAmountDue.HeaderText = "Payment Amount Due";
            this.colPaymentAmountDue.Name = "colPaymentAmountDue";
            this.colPaymentAmountDue.ReadOnly = true;
            this.colPaymentAmountDue.Width = 107;
            // 
            // colPaymentMadeOn
            // 
            this.colPaymentMadeOn.HeaderText = "Payment Made On";
            this.colPaymentMadeOn.Name = "colPaymentMadeOn";
            this.colPaymentMadeOn.ReadOnly = true;
            this.colPaymentMadeOn.Width = 97;
            // 
            // colPaymentAmountMade
            // 
            this.colPaymentAmountMade.HeaderText = "Payment Amount Made";
            this.colPaymentAmountMade.Name = "colPaymentAmountMade";
            this.colPaymentAmountMade.ReadOnly = true;
            this.colPaymentAmountMade.Width = 107;
            // 
            // colBalanceDue
            // 
            this.colBalanceDue.HeaderText = "Balance Due";
            this.colBalanceDue.Name = "colBalanceDue";
            this.colBalanceDue.ReadOnly = true;
            this.colBalanceDue.Width = 84;
            // 
            // colReceiptNumber
            // 
            this.colReceiptNumber.ActiveLinkColor = System.Drawing.Color.Blue;
            this.colReceiptNumber.HeaderText = "Receipt Number";
            this.colReceiptNumber.Name = "colReceiptNumber";
            this.colReceiptNumber.ReadOnly = true;
            this.colReceiptNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colReceiptNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colReceiptNumber.VisitedLinkColor = System.Drawing.Color.Blue;
            this.colReceiptNumber.Width = 99;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 63;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.49484F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.06241F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.73875F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.49484F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.49484F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.52577F));
            this.tableLayoutPanel1.Controls.Add(this.lblTotalLayaway, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblAmountOutstanding, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPaidToDate, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 65);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(783, 32);
            this.tableLayoutPanel1.TabIndex = 147;
            // 
            // lblTotalLayaway
            // 
            this.lblTotalLayaway.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTotalLayaway.AutoSize = true;
            this.lblTotalLayaway.Location = new System.Drawing.Point(646, 9);
            this.lblTotalLayaway.Name = "lblTotalLayaway";
            this.lblTotalLayaway.Size = new System.Drawing.Size(38, 13);
            this.lblTotalLayaway.TabIndex = 5;
            this.lblTotalLayaway.Text = "$ 0.00";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(548, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Total Layaway:";
            // 
            // lblAmountOutstanding
            // 
            this.lblAmountOutstanding.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAmountOutstanding.AutoSize = true;
            this.lblAmountOutstanding.Location = new System.Drawing.Point(388, 9);
            this.lblAmountOutstanding.Name = "lblAmountOutstanding";
            this.lblAmountOutstanding.Size = new System.Drawing.Size(38, 13);
            this.lblAmountOutstanding.TabIndex = 3;
            this.lblAmountOutstanding.Text = "$ 0.00";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(255, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Amount Outstanding:";
            // 
            // lblPaidToDate
            // 
            this.lblPaidToDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPaidToDate.AutoSize = true;
            this.lblPaidToDate.Location = new System.Drawing.Point(132, 9);
            this.lblPaidToDate.Name = "lblPaidToDate";
            this.lblPaidToDate.Size = new System.Drawing.Size(38, 13);
            this.lblPaidToDate.TabIndex = 1;
            this.lblPaidToDate.Text = "$ 0.00";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Paid to Date:";
            // 
            // tooltipPanel
            // 
            this.tooltipPanel.BackColor = System.Drawing.Color.LightYellow;
            this.tooltipPanel.ColumnCount = 2;
            this.tooltipPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tooltipPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tooltipPanel.Location = new System.Drawing.Point(498, 142);
            this.tooltipPanel.Name = "tooltipPanel";
            this.tooltipPanel.RowCount = 2;
            this.tooltipPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tooltipPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tooltipPanel.Size = new System.Drawing.Size(200, 100);
            this.tooltipPanel.TabIndex = 148;
            this.tooltipPanel.Visible = false;
            // 
            // LayawayPaymentHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = Common.Properties.Resources.newDialog_512_BlueScale;
            this.ClientSize = new System.Drawing.Size(807, 508);
            this.ControlBox = false;
            this.Controls.Add(this.tooltipPanel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.gvPayments);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.titleLabel);
            this.Name = "LayawayPaymentHistory";
            this.Load += new System.EventHandler(this.LayawayPaymentHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvPayments)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.DataGridView gvPayments;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPaidToDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAmountOutstanding;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalLayaway;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentDueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentAmountDue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentMadeOn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentAmountMade;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBalanceDue;
        private System.Windows.Forms.DataGridViewLinkColumn colReceiptNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.TableLayoutPanel tooltipPanel;
    }
}
