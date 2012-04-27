using Common.Libraries.Forms.Components;

namespace Audit.Forms.Inventory
{
    partial class InitiateAudit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitiateAudit));
            this.btnCancel = new CustomButton();
            this.btnContinue = new CustomButton();
            this.label21 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cboAuditReason = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblMarketManagerName = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblAuditorName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblShopNumber = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboReportDetail = new System.Windows.Forms.ComboBox();
            this.cboAuditScope = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cboTypeOfAudit = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboMarketManagerPresent = new Audit.UserControls.YesNoDropDownList();
            this.txtExitingShopManagerName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(9, 453);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 50);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cance&l";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.BackColor = System.Drawing.Color.Transparent;
            this.btnContinue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnContinue.BackgroundImage")));
            this.btnContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnContinue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContinue.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnContinue.FlatAppearance.BorderSize = 0;
            this.btnContinue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinue.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinue.ForeColor = System.Drawing.Color.White;
            this.btnContinue.Location = new System.Drawing.Point(691, 453);
            this.btnContinue.Margin = new System.Windows.Forms.Padding(0);
            this.btnContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(100, 50);
            this.btnContinue.TabIndex = 1;
            this.btnContinue.Text = "&Continue";
            this.btnContinue.UseVisualStyleBackColor = false;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(347, 36);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(107, 18);
            this.label21.TabIndex = 0;
            this.label21.Text = "Initiate Audit";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.cboAuditReason, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label15, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label11, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblMarketManagerName, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblAuditorName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblShopNumber, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboReportDetail, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.cboAuditScope, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label17, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cboTypeOfAudit, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cboMarketManagerPresent, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtExitingShopManagerName, 3, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(22, 61);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.665F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.665F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.665F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.665F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.665F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.675F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(754, 389);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // cboAuditReason
            // 
            this.cboAuditReason.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboAuditReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAuditReason.FormattingEnabled = true;
            this.cboAuditReason.Location = new System.Drawing.Point(567, 21);
            this.cboAuditReason.Name = "cboAuditReason";
            this.cboAuditReason.Size = new System.Drawing.Size(180, 21);
            this.cboAuditReason.TabIndex = 13;
            this.cboAuditReason.SelectedIndexChanged += new System.EventHandler(this.cboAuditReason_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(457, 217);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(104, 13);
            this.label15.TabIndex = 16;
            this.label15.Text = "Additional Scope:";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(394, 153);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(167, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Exiting Shop Manager Name:";
            // 
            // lblMarketManagerName
            // 
            this.lblMarketManagerName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMarketManagerName.AutoSize = true;
            this.lblMarketManagerName.Location = new System.Drawing.Point(191, 153);
            this.lblMarketManagerName.Name = "lblMarketManagerName";
            this.lblMarketManagerName.Size = new System.Drawing.Size(65, 13);
            this.lblMarketManagerName.TabIndex = 5;
            this.lblMarketManagerName.Text = "D Whitemire";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(46, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(139, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Market Manager Name:";
            // 
            // lblAuditorName
            // 
            this.lblAuditorName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAuditorName.AutoSize = true;
            this.lblAuditorName.Location = new System.Drawing.Point(191, 89);
            this.lblAuditorName.Name = "lblAuditorName";
            this.lblAuditorName.Size = new System.Drawing.Size(39, 13);
            this.lblAuditorName.TabIndex = 3;
            this.lblAuditorName.Text = "L Hicks";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(98, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Auditor Name:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(476, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Audit Reason:";
            // 
            // lblShopNumber
            // 
            this.lblShopNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblShopNumber.AutoSize = true;
            this.lblShopNumber.Location = new System.Drawing.Point(191, 25);
            this.lblShopNumber.Name = "lblShopNumber";
            this.lblShopNumber.Size = new System.Drawing.Size(37, 13);
            this.lblShopNumber.TabIndex = 1;
            this.lblShopNumber.Text = "02030";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(100, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Shop Number:";
            // 
            // cboReportDetail
            // 
            this.cboReportDetail.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboReportDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReportDetail.Enabled = false;
            this.cboReportDetail.FormattingEnabled = true;
            this.cboReportDetail.Items.AddRange(new object[] {
            "Include Buy",
            "Include Layaway",
            "Include Both"});
            this.cboReportDetail.Location = new System.Drawing.Point(567, 213);
            this.cboReportDetail.Name = "cboReportDetail";
            this.cboReportDetail.Size = new System.Drawing.Size(180, 21);
            this.cboReportDetail.TabIndex = 17;
            // 
            // cboAuditScope
            // 
            this.cboAuditScope.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboAuditScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAuditScope.Enabled = false;
            this.cboAuditScope.FormattingEnabled = true;
            this.cboAuditScope.Location = new System.Drawing.Point(191, 344);
            this.cboAuditScope.Name = "cboAuditScope";
            this.cboAuditScope.Size = new System.Drawing.Size(177, 21);
            this.cboAuditScope.TabIndex = 11;
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(108, 348);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 13);
            this.label17.TabIndex = 10;
            this.label17.Text = "Audit Scope:";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(100, 281);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(85, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Type of Audit:";
            // 
            // cboTypeOfAudit
            // 
            this.cboTypeOfAudit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboTypeOfAudit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTypeOfAudit.Enabled = false;
            this.cboTypeOfAudit.FormattingEnabled = true;
            this.cboTypeOfAudit.Location = new System.Drawing.Point(191, 277);
            this.cboTypeOfAudit.Name = "cboTypeOfAudit";
            this.cboTypeOfAudit.Size = new System.Drawing.Size(177, 21);
            this.cboTypeOfAudit.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(34, 217);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Market Manager Present:";
            // 
            // cboMarketManagerPresent
            // 
            this.cboMarketManagerPresent.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboMarketManagerPresent.AutoSize = true;
            this.cboMarketManagerPresent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cboMarketManagerPresent.Location = new System.Drawing.Point(191, 212);
            this.cboMarketManagerPresent.Name = "cboMarketManagerPresent";
            this.cboMarketManagerPresent.Size = new System.Drawing.Size(124, 24);
            this.cboMarketManagerPresent.TabIndex = 7;
            // 
            // txtExitingShopManagerName
            // 
            this.txtExitingShopManagerName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtExitingShopManagerName.Location = new System.Drawing.Point(567, 149);
            this.txtExitingShopManagerName.Name = "txtExitingShopManagerName";
            this.txtExitingShopManagerName.Size = new System.Drawing.Size(180, 21);
            this.txtExitingShopManagerName.TabIndex = 18;
            // 
            // InitiateAudit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 512);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "InitiateAudit";
            this.Text = "InitiateAudit";
            this.Load += new System.EventHandler(this.InitiateAudit_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblShopNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblAuditorName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblMarketManagerName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cboTypeOfAudit;
        private System.Windows.Forms.ComboBox cboAuditReason;
        private System.Windows.Forms.ComboBox cboAuditScope;
        private System.Windows.Forms.ComboBox cboReportDetail;
        private CustomButton btnContinue;
        private CustomButton btnCancel;
        private System.Windows.Forms.Label label2;
        private UserControls.YesNoDropDownList cboMarketManagerPresent;
        private System.Windows.Forms.TextBox txtExitingShopManagerName;
    }
}