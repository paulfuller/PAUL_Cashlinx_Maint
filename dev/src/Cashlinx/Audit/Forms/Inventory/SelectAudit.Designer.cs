using Common.Libraries.Forms.Components;

namespace Audit.Forms.Inventory
{
    partial class SelectAudit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectAudit));
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ddlShopNumber = new System.Windows.Forms.ComboBox();
            this.lblShopNumber = new System.Windows.Forms.Label();
            this.ddlAuditStatus = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gvAudits = new CustomDataGridView();
            this.colAuditNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAuditType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShopNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDateInitiated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastUpdated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInitiatedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnContinue = new CustomButton();
            this.btnCancel = new CustomButton();
            this.btnNewAudit = new CustomButton();
            this.btnAbort = new CustomButton();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvAudits)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(351, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Audit";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.1406F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.79369F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.ddlShopNumber, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblShopNumber, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.ddlAuditStatus, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(21, 69);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(761, 30);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // ddlShopNumber
            // 
            this.ddlShopNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ddlShopNumber.FormattingEnabled = true;
            this.ddlShopNumber.Location = new System.Drawing.Point(573, 4);
            this.ddlShopNumber.Name = "ddlShopNumber";
            this.ddlShopNumber.Size = new System.Drawing.Size(184, 21);
            this.ddlShopNumber.TabIndex = 9;
            this.ddlShopNumber.SelectedIndexChanged += new System.EventHandler(this.ddlShopNumber_SelectedIndexChanged);
            // 
            // lblShopNumber
            // 
            this.lblShopNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblShopNumber.AutoSize = true;
            this.lblShopNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShopNumber.Location = new System.Drawing.Point(517, 8);
            this.lblShopNumber.Name = "lblShopNumber";
            this.lblShopNumber.Size = new System.Drawing.Size(50, 13);
            this.lblShopNumber.TabIndex = 8;
            this.lblShopNumber.Text = "Shop #:";
            // 
            // ddlAuditStatus
            // 
            this.ddlAuditStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ddlAuditStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlAuditStatus.FormattingEnabled = true;
            this.ddlAuditStatus.Items.AddRange(new object[] {
            "Active",
            "Closed"});
            this.ddlAuditStatus.Location = new System.Drawing.Point(103, 4);
            this.ddlAuditStatus.Name = "ddlAuditStatus";
            this.ddlAuditStatus.Size = new System.Drawing.Size(184, 21);
            this.ddlAuditStatus.TabIndex = 7;
            this.ddlAuditStatus.SelectedIndexChanged += new System.EventHandler(this.ddlAuditStatus_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Show Audits:";
            // 
            // gvAudits
            // 
            this.gvAudits.AllowUserToAddRows = false;
            this.gvAudits.AllowUserToDeleteRows = false;
            this.gvAudits.AllowUserToResizeColumns = false;
            this.gvAudits.AllowUserToResizeRows = false;
            this.gvAudits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvAudits.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvAudits.BackgroundColor = System.Drawing.Color.White;
            this.gvAudits.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvAudits.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvAudits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvAudits.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAuditNumber,
            this.colAuditType,
            this.colShopNumber,
            this.colDateInitiated,
            this.colLastUpdated,
            this.colInitiatedBy});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvAudits.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvAudits.GridColor = System.Drawing.Color.LightGray;
            this.gvAudits.Location = new System.Drawing.Point(21, 102);
            this.gvAudits.Margin = new System.Windows.Forms.Padding(0);
            this.gvAudits.MultiSelect = false;
            this.gvAudits.Name = "gvAudits";
            this.gvAudits.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvAudits.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvAudits.RowHeadersVisible = false;
            this.gvAudits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvAudits.Size = new System.Drawing.Size(761, 236);
            this.gvAudits.TabIndex = 9;
            // 
            // colAuditNumber
            // 
            this.colAuditNumber.HeaderText = "Audit #";
            this.colAuditNumber.Name = "colAuditNumber";
            this.colAuditNumber.ReadOnly = true;
            // 
            // colAuditType
            // 
            this.colAuditType.HeaderText = "Audit Type";
            this.colAuditType.Name = "colAuditType";
            this.colAuditType.ReadOnly = true;
            // 
            // colShopNumber
            // 
            this.colShopNumber.HeaderText = "Shop Number";
            this.colShopNumber.Name = "colShopNumber";
            this.colShopNumber.ReadOnly = true;
            // 
            // colDateInitiated
            // 
            this.colDateInitiated.HeaderText = "Date Initiated";
            this.colDateInitiated.Name = "colDateInitiated";
            this.colDateInitiated.ReadOnly = true;
            // 
            // colLastUpdated
            // 
            this.colLastUpdated.HeaderText = "Last Updated";
            this.colLastUpdated.Name = "colLastUpdated";
            this.colLastUpdated.ReadOnly = true;
            // 
            // colInitiatedBy
            // 
            this.colInitiatedBy.HeaderText = "Initiated By";
            this.colInitiatedBy.Name = "colInitiatedBy";
            this.colInitiatedBy.ReadOnly = true;
            // 
            // btnContinue
            // 
            this.btnContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnContinue.Location = new System.Drawing.Point(661, 0);
            this.btnContinue.Margin = new System.Windows.Forms.Padding(0);
            this.btnContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(100, 50);
            this.btnContinue.TabIndex = 13;
            this.btnContinue.Text = "&Continue";
            this.btnContinue.UseVisualStyleBackColor = false;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.btnCancel.Location = new System.Drawing.Point(0, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 50);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cance&l";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNewAudit
            // 
            this.btnNewAudit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewAudit.BackColor = System.Drawing.Color.Transparent;
            this.btnNewAudit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNewAudit.BackgroundImage")));
            this.btnNewAudit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnNewAudit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewAudit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnNewAudit.FlatAppearance.BorderSize = 0;
            this.btnNewAudit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNewAudit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNewAudit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewAudit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewAudit.ForeColor = System.Drawing.Color.White;
            this.btnNewAudit.Location = new System.Drawing.Point(561, 0);
            this.btnNewAudit.Margin = new System.Windows.Forms.Padding(0);
            this.btnNewAudit.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnNewAudit.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnNewAudit.Name = "btnNewAudit";
            this.btnNewAudit.Size = new System.Drawing.Size(100, 50);
            this.btnNewAudit.TabIndex = 12;
            this.btnNewAudit.Text = "&New Audit";
            this.btnNewAudit.UseVisualStyleBackColor = false;
            this.btnNewAudit.Click += new System.EventHandler(this.btnNewAudit_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAbort.BackColor = System.Drawing.Color.Transparent;
            this.btnAbort.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAbort.BackgroundImage")));
            this.btnAbort.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAbort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbort.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnAbort.FlatAppearance.BorderSize = 0;
            this.btnAbort.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAbort.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAbort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbort.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbort.ForeColor = System.Drawing.Color.White;
            this.btnAbort.Location = new System.Drawing.Point(100, 0);
            this.btnAbort.Margin = new System.Windows.Forms.Padding(0);
            this.btnAbort.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnAbort.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(100, 50);
            this.btnAbort.TabIndex = 11;
            this.btnAbort.Text = "&Abort";
            this.btnAbort.UseVisualStyleBackColor = false;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.Transparent;
            this.panelButtons.Controls.Add(this.btnContinue);
            this.panelButtons.Controls.Add(this.btnNewAudit);
            this.panelButtons.Controls.Add(this.btnAbort);
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Location = new System.Drawing.Point(21, 341);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(761, 50);
            this.panelButtons.TabIndex = 15;
            // 
            // SelectAudit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.gvAudits);
            this.Controls.Add(this.label1);
            this.Name = "SelectAudit";
            this.Text = "SelectAudit";
            this.Activated += new System.EventHandler(this.SelectAudit_Activated);
            this.Load += new System.EventHandler(this.SelectAudit_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvAudits)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox ddlShopNumber;
        private System.Windows.Forms.Label lblShopNumber;
        private System.Windows.Forms.ComboBox ddlAuditStatus;
        private System.Windows.Forms.Label label2;
        private CustomDataGridView gvAudits;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuditNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuditType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShopNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateInitiated;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastUpdated;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInitiatedBy;
        private CustomButton btnContinue;
        private CustomButton btnCancel;
        private CustomButton btnNewAudit;
        private CustomButton btnAbort;
        private System.Windows.Forms.Panel panelButtons;
    }
}