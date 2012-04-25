using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Components.EventArgs;

namespace Pawn.Forms.Pawn.Services.Void
{
    partial class VoidLayawayActivity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoidLayawayActivity));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelUserID = new System.Windows.Forms.Label();
            this.labelDateHeading = new System.Windows.Forms.Label();
            this.labelUserIDHeading = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.customButtonVoid = new CustomButton();
            this.dataGridViewMdse = new CustomDataGridView();
            this.icn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gunno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mdseDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.customButtonCancel = new CustomButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.gvTransactions = new CustomDataGridView();
            this.colTransactionNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTransactionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenderType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatusDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanelData = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelUserInfo = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMdse)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransactions)).BeginInit();
            this.tableLayoutPanelData.SuspendLayout();
            this.tableLayoutPanelUserInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelDate
            // 
            this.labelDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelDate.AutoSize = true;
            this.labelDate.BackColor = System.Drawing.Color.Transparent;
            this.labelDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDate.ForeColor = System.Drawing.Color.White;
            this.labelDate.Location = new System.Drawing.Point(103, 22);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(83, 13);
            this.labelDate.TabIndex = 11;
            this.labelDate.Text = "mm/dd/yyyy";
            // 
            // labelUserID
            // 
            this.labelUserID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelUserID.AutoSize = true;
            this.labelUserID.BackColor = System.Drawing.Color.Transparent;
            this.labelUserID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUserID.ForeColor = System.Drawing.Color.White;
            this.labelUserID.Location = new System.Drawing.Point(103, 3);
            this.labelUserID.Name = "labelUserID";
            this.labelUserID.Size = new System.Drawing.Size(47, 13);
            this.labelUserID.TabIndex = 10;
            this.labelUserID.Text = "aagent";
            // 
            // labelDateHeading
            // 
            this.labelDateHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelDateHeading.AutoSize = true;
            this.labelDateHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelDateHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDateHeading.ForeColor = System.Drawing.Color.White;
            this.labelDateHeading.Location = new System.Drawing.Point(60, 22);
            this.labelDateHeading.Name = "labelDateHeading";
            this.labelDateHeading.Size = new System.Drawing.Size(37, 13);
            this.labelDateHeading.TabIndex = 9;
            this.labelDateHeading.Text = "Date:";
            // 
            // labelUserIDHeading
            // 
            this.labelUserIDHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelUserIDHeading.AutoSize = true;
            this.labelUserIDHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelUserIDHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUserIDHeading.ForeColor = System.Drawing.Color.White;
            this.labelUserIDHeading.Location = new System.Drawing.Point(45, 3);
            this.labelUserIDHeading.Name = "labelUserIDHeading";
            this.labelUserIDHeading.Size = new System.Drawing.Size(52, 13);
            this.labelUserIDHeading.TabIndex = 8;
            this.labelUserIDHeading.Text = "User ID:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(316, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Void Transaction";
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
            this.customButtonVoid.Location = new System.Drawing.Point(614, 547);
            this.customButtonVoid.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonVoid.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonVoid.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonVoid.Name = "customButtonVoid";
            this.customButtonVoid.Size = new System.Drawing.Size(100, 50);
            this.customButtonVoid.TabIndex = 13;
            this.customButtonVoid.Text = "Void";
            this.customButtonVoid.UseVisualStyleBackColor = false;
            this.customButtonVoid.Click += new System.EventHandler(this.customButtonVoid_Click);
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
            this.icn,
            this.gunno,
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
            this.dataGridViewMdse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMdse.GridColor = System.Drawing.Color.LightGray;
            this.dataGridViewMdse.Location = new System.Drawing.Point(0, 265);
            this.dataGridViewMdse.Margin = new System.Windows.Forms.Padding(0);
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
            this.dataGridViewMdse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMdse.Size = new System.Drawing.Size(708, 208);
            this.dataGridViewMdse.TabIndex = 14;
            this.dataGridViewMdse.GridViewRowSelecting += new System.EventHandler<GridViewRowSelectingEventArgs>(this.dataGridViewMdse_GridViewRowSelecting);
            // 
            // icn
            // 
            this.icn.FillWeight = 116F;
            this.icn.HeaderText = "ICN";
            this.icn.Name = "icn";
            this.icn.ReadOnly = true;
            // 
            // gunno
            // 
            this.gunno.FillWeight = 81F;
            this.gunno.HeaderText = "Gun Number";
            this.gunno.Name = "gunno";
            this.gunno.ReadOnly = true;
            // 
            // mdseDesc
            // 
            this.mdseDesc.FillWeight = 387F;
            this.mdseDesc.HeaderText = "Merchandise Description";
            this.mdseDesc.Name = "mdseDesc";
            this.mdseDesc.ReadOnly = true;
            // 
            // cost
            // 
            this.cost.FillWeight = 89F;
            this.cost.HeaderText = "Item Amount";
            this.cost.Name = "cost";
            this.cost.ReadOnly = true;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.Gray;
            this.panelHeader.Controls.Add(this.label2);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeader.Location = new System.Drawing.Point(0, 230);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(708, 35);
            this.panelHeader.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Item Detail";
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
            this.customButtonCancel.Location = new System.Drawing.Point(19, 547);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 17;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(708, 35);
            this.panel1.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Transaction History";
            // 
            // gvTransactions
            // 
            this.gvTransactions.AllowUserToAddRows = false;
            this.gvTransactions.AllowUserToDeleteRows = false;
            this.gvTransactions.AllowUserToResizeColumns = false;
            this.gvTransactions.AllowUserToResizeRows = false;
            this.gvTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvTransactions.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvTransactions.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvTransactions.CausesValidation = false;
            this.gvTransactions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTransactionNumber,
            this.colTransactionType,
            this.colTenderType,
            this.colStatus,
            this.colStatusDate,
            this.colAmount});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvTransactions.DefaultCellStyle = dataGridViewCellStyle6;
            this.gvTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvTransactions.GridColor = System.Drawing.Color.LightGray;
            this.gvTransactions.Location = new System.Drawing.Point(0, 35);
            this.gvTransactions.Margin = new System.Windows.Forms.Padding(0);
            this.gvTransactions.Name = "gvTransactions";
            this.gvTransactions.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvTransactions.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gvTransactions.RowHeadersVisible = false;
            this.gvTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvTransactions.Size = new System.Drawing.Size(708, 195);
            this.gvTransactions.TabIndex = 20;
            this.gvTransactions.GridViewRowSelecting += new System.EventHandler<GridViewRowSelectingEventArgs>(this.gvTransactions_GridViewRowSelecting);
            // 
            // colTransactionNumber
            // 
            this.colTransactionNumber.HeaderText = "Transaction #";
            this.colTransactionNumber.Name = "colTransactionNumber";
            this.colTransactionNumber.ReadOnly = true;
            // 
            // colTransactionType
            // 
            this.colTransactionType.HeaderText = "Transaction Type";
            this.colTransactionType.Name = "colTransactionType";
            this.colTransactionType.ReadOnly = true;
            // 
            // colTenderType
            // 
            this.colTenderType.HeaderText = "Tender Type";
            this.colTenderType.Name = "colTenderType";
            this.colTenderType.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colStatusDate
            // 
            this.colStatusDate.HeaderText = "Status Date";
            this.colStatusDate.Name = "colStatusDate";
            this.colStatusDate.ReadOnly = true;
            // 
            // colAmount
            // 
            this.colAmount.HeaderText = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            // 
            // tableLayoutPanelData
            // 
            this.tableLayoutPanelData.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelData.ColumnCount = 1;
            this.tableLayoutPanelData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelData.Controls.Add(this.dataGridViewMdse, 0, 3);
            this.tableLayoutPanelData.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanelData.Controls.Add(this.gvTransactions, 0, 1);
            this.tableLayoutPanelData.Controls.Add(this.panelHeader, 0, 2);
            this.tableLayoutPanelData.Location = new System.Drawing.Point(12, 71);
            this.tableLayoutPanelData.Name = "tableLayoutPanelData";
            this.tableLayoutPanelData.RowCount = 4;
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 195F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanelData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelData.Size = new System.Drawing.Size(708, 473);
            this.tableLayoutPanelData.TabIndex = 21;
            // 
            // tableLayoutPanelUserInfo
            // 
            this.tableLayoutPanelUserInfo.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelUserInfo.ColumnCount = 2;
            this.tableLayoutPanelUserInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelUserInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelUserInfo.Controls.Add(this.labelDate, 1, 1);
            this.tableLayoutPanelUserInfo.Controls.Add(this.labelUserID, 1, 0);
            this.tableLayoutPanelUserInfo.Controls.Add(this.labelDateHeading, 0, 1);
            this.tableLayoutPanelUserInfo.Controls.Add(this.labelUserIDHeading, 0, 0);
            this.tableLayoutPanelUserInfo.Location = new System.Drawing.Point(519, 20);
            this.tableLayoutPanelUserInfo.Name = "tableLayoutPanelUserInfo";
            this.tableLayoutPanelUserInfo.RowCount = 2;
            this.tableLayoutPanelUserInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelUserInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelUserInfo.Size = new System.Drawing.Size(200, 39);
            this.tableLayoutPanelUserInfo.TabIndex = 22;
            // 
            // VoidLayawayActivity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
            this.ClientSize = new System.Drawing.Size(732, 609);
            this.Controls.Add(this.tableLayoutPanelUserInfo);
            this.Controls.Add(this.tableLayoutPanelData);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.customButtonVoid);
            this.Controls.Add(this.label1);
            this.Name = "VoidLayawayActivity";
            this.Text = "VoidLayawayActivity";
            this.Load += new System.EventHandler(this.VoidLayawayActivity_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMdse)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransactions)).EndInit();
            this.tableLayoutPanelData.ResumeLayout(false);
            this.tableLayoutPanelUserInfo.ResumeLayout(false);
            this.tableLayoutPanelUserInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelUserID;
        private System.Windows.Forms.Label labelDateHeading;
        private System.Windows.Forms.Label labelUserIDHeading;
        private System.Windows.Forms.Label label1;
        private CustomButton customButtonVoid;
        private CustomDataGridView dataGridViewMdse;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label label2;
        private CustomButton customButtonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private CustomDataGridView gvTransactions;
        private System.Windows.Forms.DataGridViewTextBoxColumn icn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gunno;
        private System.Windows.Forms.DataGridViewTextBoxColumn mdseDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn cost;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelUserInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransactionNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransactionType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenderType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatusDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
    }
}
