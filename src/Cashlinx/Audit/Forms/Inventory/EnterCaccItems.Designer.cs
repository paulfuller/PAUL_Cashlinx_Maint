using Common.Libraries.Forms.Components;

namespace Audit.Forms.Inventory
{
    partial class EnterCaccItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterCaccItems));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new CustomButton();
            this.btnUndo = new CustomButton();
            this.btnContinue = new CustomButton();
            this.gvItems = new CustomDataGridView();
            this.colShortCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPreQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPreAvg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActualQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPostQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPostAvgCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChargeOffQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChargeOffAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChargeOnQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChargeOnAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ttl_cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvItems)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnUndo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnContinue, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 527);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(775, 61);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
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
            this.btnCancel.Location = new System.Drawing.Point(0, 5);
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
            // btnUndo
            // 
            this.btnUndo.Anchor = System.Windows.Forms.AnchorStyles.Left;
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
            this.btnUndo.Location = new System.Drawing.Point(106, 5);
            this.btnUndo.Margin = new System.Windows.Forms.Padding(0);
            this.btnUndo.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnUndo.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(100, 50);
            this.btnUndo.TabIndex = 1;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = false;
            this.btnUndo.Visible = false;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Anchor = System.Windows.Forms.AnchorStyles.Right;
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
            this.btnContinue.Location = new System.Drawing.Point(675, 5);
            this.btnContinue.Margin = new System.Windows.Forms.Padding(0);
            this.btnContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(100, 50);
            this.btnContinue.TabIndex = 2;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = false;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // gvItems
            // 
            this.gvItems.AllowUserToAddRows = false;
            this.gvItems.AllowUserToDeleteRows = false;
            this.gvItems.AllowUserToResizeColumns = false;
            this.gvItems.AllowUserToResizeRows = false;
            this.gvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvItems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvItems.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvItems.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colShortCode,
            this.colCategory,
            this.colPreQty,
            this.colPreAvg,
            this.colActualQty,
            this.colPostQty,
            this.colPostAvgCost,
            this.colChargeOffQty,
            this.colChargeOffAmount,
            this.colChargeOnQty,
            this.colChargeOnAmount,
            this.ttl_cost});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvItems.DefaultCellStyle = dataGridViewCellStyle4;
            this.gvItems.GridColor = System.Drawing.Color.LightGray;
            this.gvItems.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.gvItems.Location = new System.Drawing.Point(12, 63);
            this.gvItems.Margin = new System.Windows.Forms.Padding(0);
            this.gvItems.MultiSelect = false;
            this.gvItems.Name = "gvItems";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gvItems.RowHeadersVisible = false;
            this.gvItems.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.gvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gvItems.Size = new System.Drawing.Size(779, 461);
            this.gvItems.TabIndex = 1;
            this.gvItems.CancelRowEdit += new System.Windows.Forms.QuestionEventHandler(this.gvItems_CancelRowEdit);
            this.gvItems.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gvItems_CellBeginEdit);
            this.gvItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvItems_CellEndEdit);
            this.gvItems.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gvItems_CellPainting);
            this.gvItems.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.gvItems_RowPostPaint);
            // 
            // colShortCode
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colShortCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.colShortCode.FillWeight = 30F;
            this.colShortCode.HeaderText = "Short Code ICN";
            this.colShortCode.MaxInputLength = 10;
            this.colShortCode.Name = "colShortCode";
            this.colShortCode.ReadOnly = true;
            this.colShortCode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // colCategory
            // 
            this.colCategory.HeaderText = "Category";
            this.colCategory.MinimumWidth = 30;
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;
            // 
            // colPreQty
            // 
            this.colPreQty.FillWeight = 27.09216F;
            this.colPreQty.HeaderText = "Qty";
            this.colPreQty.Name = "colPreQty";
            this.colPreQty.ReadOnly = true;
            // 
            // colPreAvg
            // 
            this.colPreAvg.FillWeight = 27.09216F;
            this.colPreAvg.HeaderText = "Avg Cost";
            this.colPreAvg.Name = "colPreAvg";
            this.colPreAvg.ReadOnly = true;
            // 
            // colActualQty
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colActualQty.DefaultCellStyle = dataGridViewCellStyle3;
            this.colActualQty.FillWeight = 27.09216F;
            this.colActualQty.HeaderText = "Actual Qty";
            this.colActualQty.Name = "colActualQty";
            // 
            // colPostQty
            // 
            this.colPostQty.FillWeight = 27.09216F;
            this.colPostQty.HeaderText = "Qty";
            this.colPostQty.Name = "colPostQty";
            this.colPostQty.ReadOnly = true;
            // 
            // colPostAvgCost
            // 
            this.colPostAvgCost.FillWeight = 27.09216F;
            this.colPostAvgCost.HeaderText = "Avg Cost";
            this.colPostAvgCost.Name = "colPostAvgCost";
            this.colPostAvgCost.ReadOnly = true;
            // 
            // colChargeOffQty
            // 
            this.colChargeOffQty.FillWeight = 27.09216F;
            this.colChargeOffQty.HeaderText = "Charge Off Quantity";
            this.colChargeOffQty.Name = "colChargeOffQty";
            this.colChargeOffQty.ReadOnly = true;
            // 
            // colChargeOffAmount
            // 
            this.colChargeOffAmount.FillWeight = 27.09216F;
            this.colChargeOffAmount.HeaderText = "Charge Off Amount";
            this.colChargeOffAmount.Name = "colChargeOffAmount";
            this.colChargeOffAmount.ReadOnly = true;
            // 
            // colChargeOnQty
            // 
            this.colChargeOnQty.FillWeight = 27.09216F;
            this.colChargeOnQty.HeaderText = "Charge On Quantity";
            this.colChargeOnQty.Name = "colChargeOnQty";
            this.colChargeOnQty.ReadOnly = true;
            // 
            // colChargeOnAmount
            // 
            this.colChargeOnAmount.FillWeight = 27.09216F;
            this.colChargeOnAmount.HeaderText = "Charge On Amount";
            this.colChargeOnAmount.Name = "colChargeOnAmount";
            this.colChargeOnAmount.ReadOnly = true;
            // 
            // ttl_cost
            // 
            this.ttl_cost.HeaderText = "Pre_Cost";
            this.ttl_cost.Name = "ttl_cost";
            this.ttl_cost.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(311, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pre-Audit";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(478, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Post-Audit";
            // 
            // EnterCaccItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Audit.Properties.Resources.newDialog_600_BlueScale;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gvItems);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "EnterCaccItems";
            this.Text = "EnterCaccItems";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CustomButton btnCancel;
        private CustomButton btnUndo;
        private CustomButton btnContinue;
        private CustomDataGridView gvItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShortCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPreQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPreAvg;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActualQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPostQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPostAvgCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChargeOffQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChargeOffAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChargeOnQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChargeOnAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ttl_cost;

    }
}