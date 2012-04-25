using Common.Libraries.Forms.Components;

namespace Audit.Forms.Inventory
{
    partial class ProcessUnexpectedItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessUnexpectedItems));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new CustomButton();
            this.btnChargeOn = new CustomButton();
            this.btnReactivate = new CustomButton();
            this.btnUnscan = new CustomButton();
            this.btnUndo = new CustomButton();
            this.gvUnexpectedItems = new CustomDataGridView();
            this.colAuditIndicator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIcn = new CustomDataGridViewIcnColumn();
            this.colScannerNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScanSequenceNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScanLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRetailAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSubmit = new CustomButton();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvUnexpectedItems)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gvUnexpectedItems, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSubmit, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 63);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(776, 525);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnChargeOn);
            this.flowLayoutPanel1.Controls.Add(this.btnReactivate);
            this.flowLayoutPanel1.Controls.Add(this.btnUnscan);
            this.flowLayoutPanel1.Controls.Add(this.btnUndo);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 473);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(664, 49);
            this.flowLayoutPanel1.TabIndex = 1;
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
            this.btnCancel.Location = new System.Drawing.Point(0, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 50);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnChargeOn
            // 
            this.btnChargeOn.BackColor = System.Drawing.Color.Transparent;
            this.btnChargeOn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChargeOn.BackgroundImage")));
            this.btnChargeOn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnChargeOn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChargeOn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnChargeOn.FlatAppearance.BorderSize = 0;
            this.btnChargeOn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnChargeOn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnChargeOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChargeOn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChargeOn.ForeColor = System.Drawing.Color.White;
            this.btnChargeOn.Location = new System.Drawing.Point(100, 0);
            this.btnChargeOn.Margin = new System.Windows.Forms.Padding(0);
            this.btnChargeOn.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnChargeOn.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnChargeOn.Name = "btnChargeOn";
            this.btnChargeOn.Size = new System.Drawing.Size(100, 50);
            this.btnChargeOn.TabIndex = 1;
            this.btnChargeOn.Text = "Charge On";
            this.btnChargeOn.UseVisualStyleBackColor = false;
            this.btnChargeOn.Click += new System.EventHandler(this.btnChargeOn_Click);
            // 
            // btnReactivate
            // 
            this.btnReactivate.BackColor = System.Drawing.Color.Transparent;
            this.btnReactivate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReactivate.BackgroundImage")));
            this.btnReactivate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnReactivate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReactivate.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnReactivate.FlatAppearance.BorderSize = 0;
            this.btnReactivate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnReactivate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnReactivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReactivate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReactivate.ForeColor = System.Drawing.Color.White;
            this.btnReactivate.Location = new System.Drawing.Point(200, 0);
            this.btnReactivate.Margin = new System.Windows.Forms.Padding(0);
            this.btnReactivate.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnReactivate.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnReactivate.Name = "btnReactivate";
            this.btnReactivate.Size = new System.Drawing.Size(100, 50);
            this.btnReactivate.TabIndex = 2;
            this.btnReactivate.Text = "Reactivate";
            this.btnReactivate.UseVisualStyleBackColor = false;
            this.btnReactivate.Click += new System.EventHandler(this.btnReactivate_Click);
            // 
            // btnUnscan
            // 
            this.btnUnscan.BackColor = System.Drawing.Color.Transparent;
            this.btnUnscan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUnscan.BackgroundImage")));
            this.btnUnscan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnUnscan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUnscan.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnUnscan.FlatAppearance.BorderSize = 0;
            this.btnUnscan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUnscan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUnscan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnscan.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnscan.ForeColor = System.Drawing.Color.White;
            this.btnUnscan.Location = new System.Drawing.Point(300, 0);
            this.btnUnscan.Margin = new System.Windows.Forms.Padding(0);
            this.btnUnscan.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnUnscan.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnUnscan.Name = "btnUnscan";
            this.btnUnscan.Size = new System.Drawing.Size(100, 50);
            this.btnUnscan.TabIndex = 3;
            this.btnUnscan.Text = "Unscan";
            this.btnUnscan.UseVisualStyleBackColor = false;
            this.btnUnscan.Click += new System.EventHandler(this.btnUnscan_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.BackColor = System.Drawing.Color.Transparent;
            this.btnUndo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUndo.BackgroundImage")));
            this.btnUndo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnUndo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUndo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnUndo.FlatAppearance.BorderSize = 0;
            this.btnUndo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUndo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUndo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUndo.ForeColor = System.Drawing.Color.White;
            this.btnUndo.Location = new System.Drawing.Point(400, 0);
            this.btnUndo.Margin = new System.Windows.Forms.Padding(0);
            this.btnUndo.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnUndo.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(100, 50);
            this.btnUndo.TabIndex = 4;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = false;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // gvUnexpectedItems
            // 
            this.gvUnexpectedItems.AllowUserToAddRows = false;
            this.gvUnexpectedItems.AllowUserToDeleteRows = false;
            this.gvUnexpectedItems.AllowUserToResizeColumns = false;
            this.gvUnexpectedItems.AllowUserToResizeRows = false;
            this.gvUnexpectedItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvUnexpectedItems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvUnexpectedItems.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvUnexpectedItems.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvUnexpectedItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvUnexpectedItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvUnexpectedItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAuditIndicator,
            this.colIcn,
            this.colScannerNumber,
            this.colScanSequenceNumber,
            this.colScanLocation,
            this.colStatus,
            this.colRetailAmount,
            this.colCost,
            this.colDescription});
            this.tableLayoutPanel1.SetColumnSpan(this.gvUnexpectedItems, 3);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvUnexpectedItems.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvUnexpectedItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvUnexpectedItems.GridColor = System.Drawing.Color.LightGray;
            this.gvUnexpectedItems.Location = new System.Drawing.Point(0, 0);
            this.gvUnexpectedItems.Margin = new System.Windows.Forms.Padding(0);
            this.gvUnexpectedItems.Name = "gvUnexpectedItems";
            this.gvUnexpectedItems.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvUnexpectedItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvUnexpectedItems.RowHeadersVisible = false;
            this.gvUnexpectedItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvUnexpectedItems.Size = new System.Drawing.Size(776, 470);
            this.gvUnexpectedItems.TabIndex = 2;
            this.gvUnexpectedItems.SelectionChanged += new System.EventHandler(this.gvUnexpectedItems_SelectionChanged);
            // 
            // colAuditIndicator
            // 
            this.colAuditIndicator.HeaderText = "Audit Indicator";
            this.colAuditIndicator.Name = "colAuditIndicator";
            this.colAuditIndicator.ReadOnly = true;
            // 
            // colIcn
            // 
            this.colIcn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colIcn.HeaderText = "ICN";
            this.colIcn.Name = "colIcn";
            this.colIcn.ReadOnly = true;
            this.colIcn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // colScannerNumber
            // 
            this.colScannerNumber.HeaderText = "Scanner #";
            this.colScannerNumber.Name = "colScannerNumber";
            this.colScannerNumber.ReadOnly = true;
            // 
            // colScanSequenceNumber
            // 
            this.colScanSequenceNumber.HeaderText = "Scan Sequence #";
            this.colScanSequenceNumber.Name = "colScanSequenceNumber";
            this.colScanSequenceNumber.ReadOnly = true;
            // 
            // colScanLocation
            // 
            this.colScanLocation.HeaderText = "Scan Location";
            this.colScanLocation.Name = "colScanLocation";
            this.colScanLocation.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colRetailAmount
            // 
            this.colRetailAmount.HeaderText = "Retail Amount";
            this.colRetailAmount.Name = "colRetailAmount";
            this.colRetailAmount.ReadOnly = true;
            // 
            // colCost
            // 
            this.colCost.HeaderText = "Cost";
            this.colCost.Name = "colCost";
            this.colCost.ReadOnly = true;
            // 
            // colDescription
            // 
            this.colDescription.HeaderText = "Merchandise Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.Transparent;
            this.btnSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.BackgroundImage")));
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSubmit.FlatAppearance.BorderSize = 0;
            this.btnSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(670, 470);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.btnSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(100, 50);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(297, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Process Unexpected Items";
            // 
            // ProcessUnexpectedItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Audit.Properties.Resources.newDialog_600_BlueScale;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProcessUnexpectedItems";
            this.Text = "ProcessUnexpectedItems";
            this.Load += new System.EventHandler(this.ProcessUnexpectedItems_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvUnexpectedItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private CustomButton btnCancel;
        private CustomButton btnChargeOn;
        private CustomButton btnReactivate;
        private CustomButton btnUnscan;
        private CustomButton btnUndo;
        private CustomDataGridView gvUnexpectedItems;
        private CustomButton btnSubmit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuditIndicator;
        private CustomDataGridViewIcnColumn colIcn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScannerNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScanSequenceNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScanLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRetailAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;

    }
}