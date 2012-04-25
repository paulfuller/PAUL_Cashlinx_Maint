using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Components.EventArgs;

namespace Pawn.Forms.Pawn.Services.Void
{
    partial class VoidTransactionDetail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoidTransactionDetail));
            this.labelHeading = new System.Windows.Forms.Label();
            this.labelTransactionNoHeading = new System.Windows.Forms.Label();
            this.labelTransactionNo = new System.Windows.Forms.Label();
            this.labelUserIDHeading = new System.Windows.Forms.Label();
            this.labelDateHeading = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelUserID = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.dataGridViewMdse = new CustomDataGridView();
            this.mdseDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelTotal = new System.Windows.Forms.Label();
            this.labelTotalHeading = new System.Windows.Forms.Label();
            this.customButtonCancel = new CustomButton();
            this.customButtonVoid = new CustomButton();
            this.labelMessage = new System.Windows.Forms.Label();
            this.customLabelReasonHeading = new CustomLabel();
            this.comboBoxReason = new System.Windows.Forms.ComboBox();
            this.customTextBoxComment = new CustomTextBox();
            this.customLabelComment = new CustomLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMdse)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(325, 37);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(80, 19);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Void Buy";
            // 
            // labelTransactionNoHeading
            // 
            this.labelTransactionNoHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelTransactionNoHeading.AutoSize = true;
            this.labelTransactionNoHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelTransactionNoHeading.Location = new System.Drawing.Point(32, 38);
            this.labelTransactionNoHeading.Name = "labelTransactionNoHeading";
            this.labelTransactionNoHeading.Size = new System.Drawing.Size(95, 13);
            this.labelTransactionNoHeading.TabIndex = 1;
            this.labelTransactionNoHeading.Text = "Purchase Number:";
            // 
            // labelTransactionNo
            // 
            this.labelTransactionNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTransactionNo.AutoSize = true;
            this.labelTransactionNo.BackColor = System.Drawing.Color.Transparent;
            this.labelTransactionNo.Location = new System.Drawing.Point(144, 38);
            this.labelTransactionNo.Name = "labelTransactionNo";
            this.labelTransactionNo.Size = new System.Drawing.Size(37, 13);
            this.labelTransactionNo.TabIndex = 2;
            this.labelTransactionNo.Text = "12345";
            // 
            // labelUserIDHeading
            // 
            this.labelUserIDHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelUserIDHeading.AutoSize = true;
            this.labelUserIDHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelUserIDHeading.Location = new System.Drawing.Point(535, 38);
            this.labelUserIDHeading.Name = "labelUserIDHeading";
            this.labelUserIDHeading.Size = new System.Drawing.Size(47, 13);
            this.labelUserIDHeading.TabIndex = 3;
            this.labelUserIDHeading.Text = "User ID:";
            // 
            // labelDateHeading
            // 
            this.labelDateHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelDateHeading.AutoSize = true;
            this.labelDateHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelDateHeading.Location = new System.Drawing.Point(548, 68);
            this.labelDateHeading.Name = "labelDateHeading";
            this.labelDateHeading.Size = new System.Drawing.Size(34, 13);
            this.labelDateHeading.TabIndex = 4;
            this.labelDateHeading.Text = "Date:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 6);
            this.groupBox1.Location = new System.Drawing.Point(3, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(721, 2);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // labelUserID
            // 
            this.labelUserID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelUserID.AutoSize = true;
            this.labelUserID.BackColor = System.Drawing.Color.Transparent;
            this.labelUserID.Location = new System.Drawing.Point(588, 38);
            this.labelUserID.Name = "labelUserID";
            this.labelUserID.Size = new System.Drawing.Size(41, 13);
            this.labelUserID.TabIndex = 6;
            this.labelUserID.Text = "aagent";
            // 
            // labelDate
            // 
            this.labelDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelDate.AutoSize = true;
            this.labelDate.BackColor = System.Drawing.Color.Transparent;
            this.labelDate.Location = new System.Drawing.Point(588, 68);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(67, 13);
            this.labelDate.TabIndex = 7;
            this.labelDate.Text = "mm/dd/yyyy";
            // 
            // dataGridViewMdse
            // 
            this.dataGridViewMdse.AllowUserToAddRows = false;
            this.dataGridViewMdse.AllowUserToDeleteRows = false;
            this.dataGridViewMdse.AllowUserToResizeColumns = false;
            this.dataGridViewMdse.AllowUserToResizeRows = false;
            this.dataGridViewMdse.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewMdse.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewMdse.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewMdse.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMdse.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewMdse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMdse.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mdseDesc,
            this.cost});
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridViewMdse, 6);
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMdse.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewMdse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMdse.GridColor = System.Drawing.Color.LightGray;
            this.dataGridViewMdse.Location = new System.Drawing.Point(0, 100);
            this.dataGridViewMdse.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewMdse.Name = "dataGridViewMdse";
            this.dataGridViewMdse.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMdse.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewMdse.RowHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMdse.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewMdse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewMdse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMdse.Size = new System.Drawing.Size(727, 228);
            this.dataGridViewMdse.TabIndex = 8;
            this.dataGridViewMdse.GridViewRowSelecting += new System.EventHandler<GridViewRowSelectingEventArgs>(this.dataGridViewMdse_GridViewRowSelecting);
            // 
            // mdseDesc
            // 
            this.mdseDesc.HeaderText = "Merchandise Description";
            this.mdseDesc.Name = "mdseDesc";
            this.mdseDesc.ReadOnly = true;
            // 
            // cost
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.cost.DefaultCellStyle = dataGridViewCellStyle2;
            this.cost.HeaderText = "Item Cost";
            this.cost.Name = "cost";
            this.cost.ReadOnly = true;
            // 
            // labelTotal
            // 
            this.labelTotal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTotal.AutoSize = true;
            this.labelTotal.BackColor = System.Drawing.Color.Transparent;
            this.labelTotal.Location = new System.Drawing.Point(588, 336);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(35, 13);
            this.labelTotal.TabIndex = 10;
            this.labelTotal.Text = "$0.00";
            // 
            // labelTotalHeading
            // 
            this.labelTotalHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelTotalHeading.AutoSize = true;
            this.labelTotalHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelTotalHeading.Location = new System.Drawing.Point(547, 336);
            this.labelTotalHeading.Name = "labelTotalHeading";
            this.labelTotalHeading.Size = new System.Drawing.Size(35, 13);
            this.labelTotalHeading.TabIndex = 9;
            this.labelTotalHeading.Text = "Total:";
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
            this.customButtonCancel.Location = new System.Drawing.Point(83, 474);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 11;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // customButtonVoid
            // 
            this.customButtonVoid.BackColor = System.Drawing.Color.Transparent;
            this.customButtonVoid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonVoid.BackgroundImage")));
            this.customButtonVoid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonVoid.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonVoid.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonVoid.FlatAppearance.BorderSize = 0;
            this.customButtonVoid.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonVoid.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonVoid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonVoid.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonVoid.ForeColor = System.Drawing.Color.White;
            this.customButtonVoid.Location = new System.Drawing.Point(509, 473);
            this.customButtonVoid.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonVoid.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonVoid.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonVoid.Name = "customButtonVoid";
            this.customButtonVoid.Size = new System.Drawing.Size(100, 50);
            this.customButtonVoid.TabIndex = 12;
            this.customButtonVoid.Text = "Void";
            this.customButtonVoid.UseVisualStyleBackColor = false;
            this.customButtonVoid.Click += new System.EventHandler(this.customButtonVoid_Click);
            // 
            // labelMessage
            // 
            this.labelMessage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelMessage.AutoSize = true;
            this.labelMessage.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.labelMessage, 6);
            this.labelMessage.ForeColor = System.Drawing.Color.Red;
            this.labelMessage.Location = new System.Drawing.Point(348, 8);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(31, 13);
            this.labelMessage.TabIndex = 13;
            this.labelMessage.Text = "Error";
            this.labelMessage.Visible = false;
            // 
            // customLabelReasonHeading
            // 
            this.customLabelReasonHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customLabelReasonHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelReasonHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelReasonHeading.Location = new System.Drawing.Point(66, 336);
            this.customLabelReasonHeading.Name = "customLabelReasonHeading";
            this.customLabelReasonHeading.Required = true;
            this.customLabelReasonHeading.Size = new System.Drawing.Size(61, 14);
            this.customLabelReasonHeading.TabIndex = 154;
            this.customLabelReasonHeading.Text = "Reason:";
            // 
            // comboBoxReason
            // 
            this.comboBoxReason.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxReason.FormattingEnabled = true;
            this.comboBoxReason.Items.AddRange(new object[] {
            "Wrong Receiving Location",
            "Test Transaction",
            "System Issue"});
            this.comboBoxReason.Location = new System.Drawing.Point(144, 332);
            this.comboBoxReason.Name = "comboBoxReason";
            this.comboBoxReason.Size = new System.Drawing.Size(178, 21);
            this.comboBoxReason.TabIndex = 153;
            // 
            // customTextBoxComment
            // 
            this.customTextBoxComment.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customTextBoxComment.CausesValidation = false;
            this.customTextBoxComment.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxComment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxComment.Location = new System.Drawing.Point(144, 362);
            this.customTextBoxComment.Name = "customTextBoxComment";
            this.customTextBoxComment.Size = new System.Drawing.Size(187, 21);
            this.customTextBoxComment.TabIndex = 152;
            this.customTextBoxComment.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customLabelComment
            // 
            this.customLabelComment.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customLabelComment.BackColor = System.Drawing.Color.Transparent;
            this.customLabelComment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelComment.Location = new System.Drawing.Point(66, 366);
            this.customLabelComment.Name = "customLabelComment";
            this.customLabelComment.Size = new System.Drawing.Size(61, 14);
            this.customLabelComment.TabIndex = 151;
            this.customLabelComment.Text = "Comment:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 202F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 186F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.labelTransactionNoHeading, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelTransactionNo, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.customTextBoxComment, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.customLabelReasonHeading, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelTotalHeading, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.customLabelComment, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.labelUserIDHeading, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxReason, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelUserID, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelDate, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelDateHeading, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelMessage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewMdse, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelTotal, 5, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 73);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(707, 398);
            this.tableLayoutPanel1.TabIndex = 156;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox3, 6);
            this.groupBox3.Location = new System.Drawing.Point(3, 392);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(721, 2);
            this.groupBox3.TabIndex = 155;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // VoidTransactionDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_512_BlueScale;
            this.ClientSize = new System.Drawing.Size(731, 539);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.customButtonVoid);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.labelHeading);
            this.Name = "VoidTransactionDetail";
            this.Text = "VoidPurchaseReturn";
            this.Load += new System.EventHandler(this.VoidPurchaseReturn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMdse)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label labelTransactionNoHeading;
        private System.Windows.Forms.Label labelTransactionNo;
        private System.Windows.Forms.Label labelUserIDHeading;
        private System.Windows.Forms.Label labelDateHeading;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelUserID;
        private System.Windows.Forms.Label labelDate;
        private CustomDataGridView dataGridViewMdse;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label labelTotalHeading;
        private CustomButton customButtonCancel;
        private CustomButton customButtonVoid;
        private System.Windows.Forms.Label labelMessage;
        private CustomLabel customLabelReasonHeading;
        private System.Windows.Forms.ComboBox comboBoxReason;
        private CustomTextBox customTextBoxComment;
        private CustomLabel customLabelComment;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn mdseDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn cost;
    }
}
