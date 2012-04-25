using Common.Libraries.Forms.Components;

namespace Audit.Forms.Inventory
{
    partial class ProcessMissingItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessMissingItems));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new CustomButton();
            this.btnChargeOff = new CustomButton();
            this.btnReconcile = new CustomButton();
            this.btnFound = new CustomButton();
            this.btnUndo = new CustomButton();
            this.btnPrintShortList = new CustomButton();
            this.btnPreviousAuditTrakker = new CustomButton();
            this.gvMissingItems = new CustomDataGridView();
            this.ddlFilterBy = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new CustomButton();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnFind = new CustomButtonTiny();
            this.txtICN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.colAuditIndicator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIcn = new CustomDataGridViewIcnColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRefNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvMissingItems)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(313, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Process Missing Items";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 440F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.gvMissingItems, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ddlFilterBy, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSubmit, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 63);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(816, 525);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter by:";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 3);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnChargeOff);
            this.flowLayoutPanel1.Controls.Add(this.btnReconcile);
            this.flowLayoutPanel1.Controls.Add(this.btnFound);
            this.flowLayoutPanel1.Controls.Add(this.btnUndo);
            this.flowLayoutPanel1.Controls.Add(this.btnPrintShortList);
            this.flowLayoutPanel1.Controls.Add(this.btnPreviousAuditTrakker);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 472);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(707, 50);
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
            // btnChargeOff
            // 
            this.btnChargeOff.BackColor = System.Drawing.Color.Transparent;
            this.btnChargeOff.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnChargeOff.BackgroundImage")));
            this.btnChargeOff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnChargeOff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChargeOff.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnChargeOff.FlatAppearance.BorderSize = 0;
            this.btnChargeOff.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnChargeOff.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnChargeOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChargeOff.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChargeOff.ForeColor = System.Drawing.Color.White;
            this.btnChargeOff.Location = new System.Drawing.Point(100, 0);
            this.btnChargeOff.Margin = new System.Windows.Forms.Padding(0);
            this.btnChargeOff.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnChargeOff.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnChargeOff.Name = "btnChargeOff";
            this.btnChargeOff.Size = new System.Drawing.Size(100, 50);
            this.btnChargeOff.TabIndex = 1;
            this.btnChargeOff.Text = "Charge Off";
            this.btnChargeOff.UseVisualStyleBackColor = false;
            this.btnChargeOff.Click += new System.EventHandler(this.btnChargeOff_Click);
            // 
            // btnReconcile
            // 
            this.btnReconcile.BackColor = System.Drawing.Color.Transparent;
            this.btnReconcile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReconcile.BackgroundImage")));
            this.btnReconcile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnReconcile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReconcile.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnReconcile.FlatAppearance.BorderSize = 0;
            this.btnReconcile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnReconcile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnReconcile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReconcile.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReconcile.ForeColor = System.Drawing.Color.White;
            this.btnReconcile.Location = new System.Drawing.Point(200, 0);
            this.btnReconcile.Margin = new System.Windows.Forms.Padding(0);
            this.btnReconcile.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnReconcile.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnReconcile.Name = "btnReconcile";
            this.btnReconcile.Size = new System.Drawing.Size(100, 50);
            this.btnReconcile.TabIndex = 2;
            this.btnReconcile.Text = "Reconcile";
            this.btnReconcile.UseVisualStyleBackColor = false;
            this.btnReconcile.Click += new System.EventHandler(this.btnReconcile_Click);
            // 
            // btnFound
            // 
            this.btnFound.BackColor = System.Drawing.Color.Transparent;
            this.btnFound.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFound.BackgroundImage")));
            this.btnFound.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnFound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFound.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnFound.FlatAppearance.BorderSize = 0;
            this.btnFound.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnFound.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnFound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFound.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFound.ForeColor = System.Drawing.Color.White;
            this.btnFound.Location = new System.Drawing.Point(300, 0);
            this.btnFound.Margin = new System.Windows.Forms.Padding(0);
            this.btnFound.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnFound.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnFound.Name = "btnFound";
            this.btnFound.Size = new System.Drawing.Size(100, 50);
            this.btnFound.TabIndex = 3;
            this.btnFound.Text = "Found";
            this.btnFound.UseVisualStyleBackColor = false;
            this.btnFound.Click += new System.EventHandler(this.btnFound_Click);
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
            // btnPrintShortList
            // 
            this.btnPrintShortList.BackColor = System.Drawing.Color.Transparent;
            this.btnPrintShortList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrintShortList.BackgroundImage")));
            this.btnPrintShortList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPrintShortList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintShortList.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPrintShortList.FlatAppearance.BorderSize = 0;
            this.btnPrintShortList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPrintShortList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPrintShortList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintShortList.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintShortList.ForeColor = System.Drawing.Color.White;
            this.btnPrintShortList.Location = new System.Drawing.Point(500, 0);
            this.btnPrintShortList.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrintShortList.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnPrintShortList.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnPrintShortList.Name = "btnPrintShortList";
            this.btnPrintShortList.Size = new System.Drawing.Size(100, 50);
            this.btnPrintShortList.TabIndex = 5;
            this.btnPrintShortList.Text = "Print Short List";
            this.btnPrintShortList.UseVisualStyleBackColor = false;
            this.btnPrintShortList.Click += new System.EventHandler(this.btnPrintShortList_Click);
            // 
            // btnPreviousAuditTrakker
            // 
            this.btnPreviousAuditTrakker.BackColor = System.Drawing.Color.Transparent;
            this.btnPreviousAuditTrakker.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPreviousAuditTrakker.BackgroundImage")));
            this.btnPreviousAuditTrakker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPreviousAuditTrakker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreviousAuditTrakker.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPreviousAuditTrakker.FlatAppearance.BorderSize = 0;
            this.btnPreviousAuditTrakker.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPreviousAuditTrakker.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPreviousAuditTrakker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviousAuditTrakker.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreviousAuditTrakker.ForeColor = System.Drawing.Color.White;
            this.btnPreviousAuditTrakker.Location = new System.Drawing.Point(600, 0);
            this.btnPreviousAuditTrakker.Margin = new System.Windows.Forms.Padding(0);
            this.btnPreviousAuditTrakker.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnPreviousAuditTrakker.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnPreviousAuditTrakker.Name = "btnPreviousAuditTrakker";
            this.btnPreviousAuditTrakker.Size = new System.Drawing.Size(100, 50);
            this.btnPreviousAuditTrakker.TabIndex = 6;
            this.btnPreviousAuditTrakker.Text = "Previous Audit Trakker";
            this.btnPreviousAuditTrakker.UseVisualStyleBackColor = false;
            this.btnPreviousAuditTrakker.Click += new System.EventHandler(this.btnPreviousAuditTrakker_Click);
            // 
            // gvMissingItems
            // 
            this.gvMissingItems.AllowUserToAddRows = false;
            this.gvMissingItems.AllowUserToDeleteRows = false;
            this.gvMissingItems.AllowUserToResizeColumns = false;
            this.gvMissingItems.AllowUserToResizeRows = false;
            this.gvMissingItems.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvMissingItems.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvMissingItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvMissingItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvMissingItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAuditIndicator,
            this.colIcn,
            this.colStatus,
            this.colCost,
            this.colReason,
            this.colDescription,
            this.colRefNumber});
            this.tableLayoutPanel1.SetColumnSpan(this.gvMissingItems, 4);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvMissingItems.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvMissingItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvMissingItems.GridColor = System.Drawing.Color.LightGray;
            this.gvMissingItems.Location = new System.Drawing.Point(0, 41);
            this.gvMissingItems.Margin = new System.Windows.Forms.Padding(0);
            this.gvMissingItems.Name = "gvMissingItems";
            this.gvMissingItems.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMissingItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvMissingItems.RowHeadersVisible = false;
            this.gvMissingItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvMissingItems.Size = new System.Drawing.Size(816, 428);
            this.gvMissingItems.TabIndex = 2;
            this.gvMissingItems.AutoSizeColumnsModeChanged += new System.Windows.Forms.DataGridViewAutoSizeColumnsModeEventHandler(this.gvMissingItems_AutoSizeColumnsModeChanged);
            this.gvMissingItems.SelectionChanged += new System.EventHandler(this.gvMissingItems_SelectionChanged);
            this.gvMissingItems.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.gvMissingItems_SortCompare);
            // 
            // ddlFilterBy
            // 
            this.ddlFilterBy.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ddlFilterBy.FormattingEnabled = true;
            this.ddlFilterBy.Location = new System.Drawing.Point(79, 10);
            this.ddlFilterBy.Name = "ddlFilterBy";
            this.ddlFilterBy.Size = new System.Drawing.Size(121, 21);
            this.ddlFilterBy.TabIndex = 4;
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
            this.btnSubmit.Location = new System.Drawing.Point(713, 469);
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
            // flowLayoutPanel2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel2, 2);
            this.flowLayoutPanel2.Controls.Add(this.btnFind);
            this.flowLayoutPanel2.Controls.Add(this.txtICN);
            this.flowLayoutPanel2.Controls.Add(this.label3);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(276, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(537, 35);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // btnFind
            // 
            this.btnFind.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnFind.BackColor = System.Drawing.Color.Transparent;
            this.btnFind.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFind.BackgroundImage")));
            this.btnFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnFind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFind.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnFind.FlatAppearance.BorderSize = 0;
            this.btnFind.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnFind.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFind.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.ForeColor = System.Drawing.Color.White;
            this.btnFind.Location = new System.Drawing.Point(462, 0);
            this.btnFind.Margin = new System.Windows.Forms.Padding(0);
            this.btnFind.MaximumSize = new System.Drawing.Size(75, 35);
            this.btnFind.MinimumSize = new System.Drawing.Size(75, 35);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 35);
            this.btnFind.TabIndex = 0;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = false;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtICN
            // 
            this.txtICN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtICN.Location = new System.Drawing.Point(286, 7);
            this.txtICN.Name = "txtICN";
            this.txtICN.Size = new System.Drawing.Size(173, 21);
            this.txtICN.TabIndex = 1;
            this.txtICN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtICN_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "ICN:";
            // 
            // colAuditIndicator
            // 
            this.colAuditIndicator.HeaderText = "Audit Indicator";
            this.colAuditIndicator.Name = "colAuditIndicator";
            this.colAuditIndicator.ReadOnly = true;
            this.colAuditIndicator.Width = 103;
            // 
            // colIcn
            // 
            this.colIcn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colIcn.HeaderText = "ICN";
            this.colIcn.Name = "colIcn";
            this.colIcn.ReadOnly = true;
            this.colIcn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 63;
            // 
            // colCost
            // 
            this.colCost.HeaderText = "Cost";
            this.colCost.Name = "colCost";
            this.colCost.ReadOnly = true;
            this.colCost.Width = 54;
            // 
            // colReason
            // 
            this.colReason.HeaderText = "Reason";
            this.colReason.Name = "colReason";
            this.colReason.ReadOnly = true;
            // 
            // colDescription
            // 
            this.colDescription.HeaderText = "Merchandise Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            this.colDescription.Width = 148;
            // 
            // colRefNumber
            // 
            this.colRefNumber.HeaderText = "Temp Ref ICN";
            this.colRefNumber.Name = "colRefNumber";
            this.colRefNumber.ReadOnly = true;
            this.colRefNumber.Width = 99;
            // 
            // ProcessMissingItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Audit.Properties.Resources.newDialog_600_BlueScale;
            this.ClientSize = new System.Drawing.Size(840, 600);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProcessMissingItems";
            this.Text = "ProcessMissingItems";
            this.Load += new System.EventHandler(this.ProcessMissingItems_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvMissingItems)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private CustomDataGridView gvMissingItems;
        private CustomButton btnCancel;
        private CustomButton btnChargeOff;
        private CustomButton btnReconcile;
        private CustomButton btnFound;
        private CustomButton btnUndo;
        private CustomButton btnPrintShortList;
        private CustomButton btnSubmit;
        private System.Windows.Forms.ComboBox ddlFilterBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private CustomButtonTiny btnFind;
        private System.Windows.Forms.TextBox txtICN;
        private System.Windows.Forms.Label label3;
        private CustomButton btnPreviousAuditTrakker;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuditIndicator;
        private CustomDataGridViewIcnColumn colIcn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReason;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRefNumber;
    }
}