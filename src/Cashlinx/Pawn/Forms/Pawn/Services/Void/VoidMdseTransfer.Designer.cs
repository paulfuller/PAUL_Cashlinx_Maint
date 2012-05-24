using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.Void
{
    partial class VoidMdseTransfer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoidMdseTransfer));
            this.labelHeading = new System.Windows.Forms.Label();
            this.labelTransactionNoHeading = new System.Windows.Forms.Label();
            this.labelTransferNo = new System.Windows.Forms.Label();
            this.labelUserIDHeading = new System.Windows.Forms.Label();
            this.labelDateHeading = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelUserID = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.dataGridViewMdse = new System.Windows.Forms.DataGridView();
            this.labelTotal = new System.Windows.Forms.Label();
            this.labelTotalHeading = new System.Windows.Forms.Label();
            this.customButtonCancel = new CustomButton();
            this.customButtonVoid = new CustomButton();
            this.labelMessage = new System.Windows.Forms.Label();
            this.labelDestination = new System.Windows.Forms.Label();
            this.customTextBoxComment = new CustomTextBox();
            this.customLabelComment = new CustomLabel();
            this.comboBoxReason = new System.Windows.Forms.ComboBox();
            this.customLabelReasonHeading = new CustomLabel();
            this.mdseDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMdse)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(303, 37);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(153, 19);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Void Transfer Out";
            // 
            // labelTransactionNoHeading
            // 
            this.labelTransactionNoHeading.AutoSize = true;
            this.labelTransactionNoHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelTransactionNoHeading.Location = new System.Drawing.Point(91, 123);
            this.labelTransactionNoHeading.Name = "labelTransactionNoHeading";
            this.labelTransactionNoHeading.Size = new System.Drawing.Size(92, 13);
            this.labelTransactionNoHeading.TabIndex = 1;
            this.labelTransactionNoHeading.Text = "Transfer Number:";
            // 
            // labelTransferNo
            // 
            this.labelTransferNo.AutoSize = true;
            this.labelTransferNo.BackColor = System.Drawing.Color.Transparent;
            this.labelTransferNo.Location = new System.Drawing.Point(216, 123);
            this.labelTransferNo.Name = "labelTransferNo";
            this.labelTransferNo.Size = new System.Drawing.Size(37, 13);
            this.labelTransferNo.TabIndex = 2;
            this.labelTransferNo.Text = "12345";
            // 
            // labelUserIDHeading
            // 
            this.labelUserIDHeading.AutoSize = true;
            this.labelUserIDHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelUserIDHeading.Location = new System.Drawing.Point(458, 123);
            this.labelUserIDHeading.Name = "labelUserIDHeading";
            this.labelUserIDHeading.Size = new System.Drawing.Size(47, 13);
            this.labelUserIDHeading.TabIndex = 3;
            this.labelUserIDHeading.Text = "User ID:";
            // 
            // labelDateHeading
            // 
            this.labelDateHeading.AutoSize = true;
            this.labelDateHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelDateHeading.Location = new System.Drawing.Point(471, 148);
            this.labelDateHeading.Name = "labelDateHeading";
            this.labelDateHeading.Size = new System.Drawing.Size(34, 13);
            this.labelDateHeading.TabIndex = 4;
            this.labelDateHeading.Text = "Date:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(12, 170);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(710, 2);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // labelUserID
            // 
            this.labelUserID.AutoSize = true;
            this.labelUserID.BackColor = System.Drawing.Color.Transparent;
            this.labelUserID.Location = new System.Drawing.Point(528, 123);
            this.labelUserID.Name = "labelUserID";
            this.labelUserID.Size = new System.Drawing.Size(41, 13);
            this.labelUserID.TabIndex = 6;
            this.labelUserID.Text = "aagent";
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.BackColor = System.Drawing.Color.Transparent;
            this.labelDate.Location = new System.Drawing.Point(528, 148);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(67, 13);
            this.labelDate.TabIndex = 7;
            this.labelDate.Text = "mm/dd/yyyy";
            // 
            // dataGridViewMdse
            // 
            this.dataGridViewMdse.BackgroundColor = System.Drawing.Color.White;
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMdse.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewMdse.Location = new System.Drawing.Point(25, 209);
            this.dataGridViewMdse.Name = "dataGridViewMdse";
            this.dataGridViewMdse.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMdse.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewMdse.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMdse.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewMdse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewMdse.Size = new System.Drawing.Size(607, 150);
            this.dataGridViewMdse.TabIndex = 8;
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.BackColor = System.Drawing.Color.Transparent;
            this.labelTotal.Location = new System.Drawing.Point(541, 372);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(35, 13);
            this.labelTotal.TabIndex = 10;
            this.labelTotal.Text = "$0.00";
            // 
            // labelTotalHeading
            // 
            this.labelTotalHeading.AutoSize = true;
            this.labelTotalHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelTotalHeading.Location = new System.Drawing.Point(471, 372);
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
            this.customButtonCancel.Location = new System.Drawing.Point(94, 420);
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
            this.customButtonVoid.Location = new System.Drawing.Point(520, 420);
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
            this.labelMessage.AutoSize = true;
            this.labelMessage.BackColor = System.Drawing.Color.Transparent;
            this.labelMessage.ForeColor = System.Drawing.Color.Red;
            this.labelMessage.Location = new System.Drawing.Point(36, 92);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(31, 13);
            this.labelMessage.TabIndex = 13;
            this.labelMessage.Text = "Error";
            this.labelMessage.Visible = false;
            // 
            // labelDestination
            // 
            this.labelDestination.AutoSize = true;
            this.labelDestination.BackColor = System.Drawing.Color.Transparent;
            this.labelDestination.Location = new System.Drawing.Point(36, 175);
            this.labelDestination.Name = "labelDestination";
            this.labelDestination.Size = new System.Drawing.Size(99, 13);
            this.labelDestination.TabIndex = 14;
            this.labelDestination.Text = "Destination - Catco";
            // 
            // customTextBoxComment
            // 
            this.customTextBoxComment.CausesValidation = false;
            this.customTextBoxComment.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxComment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxComment.Location = new System.Drawing.Point(106, 393);
            this.customTextBoxComment.Name = "customTextBoxComment";
            this.customTextBoxComment.Size = new System.Drawing.Size(202, 21);
            this.customTextBoxComment.TabIndex = 148;
            this.customTextBoxComment.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customLabelComment
            // 
            this.customLabelComment.BackColor = System.Drawing.Color.Transparent;
            this.customLabelComment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelComment.Location = new System.Drawing.Point(26, 393);
            this.customLabelComment.Name = "customLabelComment";
            this.customLabelComment.Size = new System.Drawing.Size(61, 14);
            this.customLabelComment.TabIndex = 147;
            this.customLabelComment.Text = "Comment:";
            // 
            // comboBoxReason
            // 
            this.comboBoxReason.FormattingEnabled = true;
            this.comboBoxReason.Items.AddRange(new object[] {
            "Wrong Receiving Location",
            "Test Transaction",
            "System Issue"});
            this.comboBoxReason.Location = new System.Drawing.Point(106, 366);
            this.comboBoxReason.Name = "comboBoxReason";
            this.comboBoxReason.Size = new System.Drawing.Size(178, 21);
            this.comboBoxReason.TabIndex = 149;
            // 
            // customLabelReasonHeading
            // 
            this.customLabelReasonHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelReasonHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelReasonHeading.Location = new System.Drawing.Point(26, 369);
            this.customLabelReasonHeading.Name = "customLabelReasonHeading";
            this.customLabelReasonHeading.Required = true;
            this.customLabelReasonHeading.Size = new System.Drawing.Size(61, 14);
            this.customLabelReasonHeading.TabIndex = 150;
            this.customLabelReasonHeading.Text = "Reason:";
            // 
            // mdseDesc
            // 
            this.mdseDesc.HeaderText = "Merchandise Description";
            this.mdseDesc.Name = "mdseDesc";
            this.mdseDesc.ReadOnly = true;
            this.mdseDesc.Width = 302;
            // 
            // cost
            // 
            this.cost.HeaderText = "Item Cost";
            this.cost.Name = "cost";
            this.cost.ReadOnly = true;
            this.cost.Width = 302;
            // 
            // VoidMdseTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 479);
            this.Controls.Add(this.customLabelReasonHeading);
            this.Controls.Add(this.comboBoxReason);
            this.Controls.Add(this.customTextBoxComment);
            this.Controls.Add(this.customLabelComment);
            this.Controls.Add(this.labelDestination);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.customButtonVoid);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.labelTotal);
            this.Controls.Add(this.labelTotalHeading);
            this.Controls.Add(this.dataGridViewMdse);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.labelUserID);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelDateHeading);
            this.Controls.Add(this.labelUserIDHeading);
            this.Controls.Add(this.labelTransferNo);
            this.Controls.Add(this.labelTransactionNoHeading);
            this.Controls.Add(this.labelHeading);
            this.Name = "VoidMdseTransfer";
            this.Text = "VoidPurchaseReturn";
            this.Load += new System.EventHandler(this.VoidMdseTransfer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMdse)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label labelTransactionNoHeading;
        private System.Windows.Forms.Label labelTransferNo;
        private System.Windows.Forms.Label labelUserIDHeading;
        private System.Windows.Forms.Label labelDateHeading;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelUserID;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.DataGridView dataGridViewMdse;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label labelTotalHeading;
        private CustomButton customButtonCancel;
        private CustomButton customButtonVoid;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Label labelDestination;
        private CustomTextBox customTextBoxComment;
        private CustomLabel customLabelComment;
        private System.Windows.Forms.ComboBox comboBoxReason;
        private CustomLabel customLabelReasonHeading;
        private System.Windows.Forms.DataGridViewTextBoxColumn mdseDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn cost;
    }
}