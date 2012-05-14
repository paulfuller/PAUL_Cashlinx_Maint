using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.Services.MerchandiseTransfer
{
    partial class ManageTransferOut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageTransferOut));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.titleLabel = new System.Windows.Forms.Label();
            this.icnLabel = new System.Windows.Forms.Label();
            this.cmdICNSubmit = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.gvMerchandise = new System.Windows.Forms.DataGridView();
            this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RefurbCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colICN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMerchandiseDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colJewelryCaseNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtIcn = new System.Windows.Forms.TextBox();
            this.lblFrom1 = new System.Windows.Forms.Label();
            this.variancePanel = new System.Windows.Forms.Panel();
            this.lblFromFax = new System.Windows.Forms.Label();
            this.lblFromTelephone = new System.Windows.Forms.Label();
            this.lblFrom3 = new System.Windows.Forms.Label();
            this.lblFrom2 = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblStoreFax = new System.Windows.Forms.Label();
            this.storeMgrName = new System.Windows.Forms.Label();
            this.storePhNo = new System.Windows.Forms.Label();
            this.storeAddrLine2 = new System.Windows.Forms.Label();
            this.storeAddrLine1 = new System.Windows.Forms.Label();
            this.tblLayout = new System.Windows.Forms.TableLayoutPanel();
            this.transferToList1 = new TransferTo();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addCommentButton = new System.Windows.Forms.Button();
            this.tblLayout2 = new System.Windows.Forms.TableLayoutPanel();
            this.transferTo1 = new TransferTo();
            this.toTransLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvMerchandise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.variancePanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tblLayout.SuspendLayout();
            this.tblLayout2.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(301, 26);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(158, 19);
            this.titleLabel.TabIndex = 136;
            this.titleLabel.Text = "Manage Transfer Out";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // icnLabel
            // 
            this.icnLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.icnLabel.AutoSize = true;
            this.icnLabel.BackColor = System.Drawing.Color.Transparent;
            this.icnLabel.Location = new System.Drawing.Point(3, 11);
            this.icnLabel.Name = "icnLabel";
            this.icnLabel.Size = new System.Drawing.Size(106, 13);
            this.icnLabel.TabIndex = 139;
            this.icnLabel.Text = "Enter or Scan ICN#: ";
            this.icnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdICNSubmit
            // 
            this.cmdICNSubmit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmdICNSubmit.AutoSize = true;
            this.cmdICNSubmit.BackColor = System.Drawing.Color.Transparent;
            this.cmdICNSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdICNSubmit.BackgroundImage")));
            this.cmdICNSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdICNSubmit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.cmdICNSubmit.FlatAppearance.BorderSize = 0;
            this.cmdICNSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cmdICNSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmdICNSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdICNSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdICNSubmit.ForeColor = System.Drawing.Color.White;
            this.cmdICNSubmit.Location = new System.Drawing.Point(337, 6);
            this.cmdICNSubmit.Name = "cmdICNSubmit";
            this.cmdICNSubmit.Size = new System.Drawing.Size(67, 23);
            this.cmdICNSubmit.TabIndex = 142;
            this.cmdICNSubmit.Text = "Submit";
            this.cmdICNSubmit.UseVisualStyleBackColor = false;
            this.cmdICNSubmit.Click += new System.EventHandler(this.cmdICNSubmit_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
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
            this.cancelButton.Location = new System.Drawing.Point(13, 475);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 40);
            this.cancelButton.TabIndex = 143;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // continueButton
            // 
            this.continueButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.continueButton.AutoSize = true;
            this.continueButton.BackColor = System.Drawing.Color.Transparent;
            this.continueButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("continueButton.BackgroundImage")));
            this.continueButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.continueButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.continueButton.FlatAppearance.BorderSize = 0;
            this.continueButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueButton.ForeColor = System.Drawing.Color.White;
            this.continueButton.Location = new System.Drawing.Point(659, 475);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(100, 40);
            this.continueButton.TabIndex = 144;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = false;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // gvMerchandise
            // 
            this.gvMerchandise.AllowUserToAddRows = false;
            this.gvMerchandise.AllowUserToDeleteRows = false;
            this.gvMerchandise.AllowUserToResizeColumns = false;
            this.gvMerchandise.AllowUserToResizeRows = false;
            this.gvMerchandise.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvMerchandise.BackgroundColor = System.Drawing.Color.White;
            this.gvMerchandise.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gvMerchandise.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvMerchandise.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvMerchandise.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvMerchandise.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNumber,
            this.RefurbCol,
            this.colICN,
            this.colMerchandiseDescription,
            this.colQuantity,
            this.colCost,
            this.colComments,
            this.colJewelryCaseNumber});
            this.gvMerchandise.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvMerchandise.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvMerchandise.GridColor = System.Drawing.Color.LightGray;
            this.gvMerchandise.Location = new System.Drawing.Point(16, 293);
            this.gvMerchandise.MultiSelect = false;
            this.gvMerchandise.Name = "gvMerchandise";
            this.gvMerchandise.RowHeadersVisible = false;
            this.gvMerchandise.RowHeadersWidth = 20;
            this.gvMerchandise.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMerchandise.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gvMerchandise.RowTemplate.Height = 25;
            this.gvMerchandise.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvMerchandise.Size = new System.Drawing.Size(744, 173);
            this.gvMerchandise.TabIndex = 147;
            this.gvMerchandise.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvMerchandise_CellClick);
            this.gvMerchandise.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvMerchandise_CellLeave);
            this.gvMerchandise.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvMerchandise_CellValueChanged);
            // 
            // colNumber
            // 
            this.colNumber.FillWeight = 6.352931F;
            this.colNumber.HeaderText = "Number";
            this.colNumber.Name = "colNumber";
            this.colNumber.ReadOnly = true;
            this.colNumber.Width = 60;
            // 
            // RefurbCol
            // 
            this.RefurbCol.HeaderText = "Refurb";
            this.RefurbCol.Name = "RefurbCol";
            this.RefurbCol.ReadOnly = true;
            this.RefurbCol.Visible = false;
            // 
            // colICN
            // 
            this.colICN.FillWeight = 12.70586F;
            this.colICN.HeaderText = "ICN";
            this.colICN.Name = "colICN";
            this.colICN.ReadOnly = true;
            this.colICN.Width = 140;
            // 
            // colMerchandiseDescription
            // 
            this.colMerchandiseDescription.FillWeight = 40F;
            this.colMerchandiseDescription.HeaderText = "Merchandise Description";
            this.colMerchandiseDescription.Name = "colMerchandiseDescription";
            this.colMerchandiseDescription.ReadOnly = true;
            this.colMerchandiseDescription.Width = 350;
            // 
            // colQuantity
            // 
            this.colQuantity.FillWeight = 18.96205F;
            this.colQuantity.HeaderText = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Width = 80;
            // 
            // colCost
            // 
            this.colCost.FillWeight = 18.96205F;
            this.colCost.HeaderText = "Cost";
            this.colCost.Name = "colCost";
            this.colCost.ReadOnly = true;
            // 
            // colComments
            // 
            this.colComments.HeaderText = "Comments";
            this.colComments.Name = "colComments";
            this.colComments.ReadOnly = true;
            this.colComments.Visible = false;
            // 
            // colJewelryCaseNumber
            // 
            this.colJewelryCaseNumber.HeaderText = "CaseNumber";
            this.colJewelryCaseNumber.Name = "colJewelryCaseNumber";
            this.colJewelryCaseNumber.ReadOnly = true;
            this.colJewelryCaseNumber.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Pawn.Properties.Resources.blue_border;
            this.pictureBox1.Location = new System.Drawing.Point(6, 472);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(770, 3);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 150;
            this.pictureBox1.TabStop = false;
            // 
            // txtIcn
            // 
            this.txtIcn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtIcn.Location = new System.Drawing.Point(122, 7);
            this.txtIcn.Name = "txtIcn";
            this.txtIcn.Size = new System.Drawing.Size(209, 20);
            this.txtIcn.TabIndex = 151;
            this.txtIcn.TextChanged += new System.EventHandler(this.ticketNumber_TextChanged);
            // 
            // lblFrom1
            // 
            this.lblFrom1.AutoSize = true;
            this.lblFrom1.BackColor = System.Drawing.Color.Transparent;
            this.lblFrom1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom1.Location = new System.Drawing.Point(3, 6);
            this.lblFrom1.Name = "lblFrom1";
            this.lblFrom1.Size = new System.Drawing.Size(41, 13);
            this.lblFrom1.TabIndex = 152;
            this.lblFrom1.Text = "From1";
            this.lblFrom1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // variancePanel
            // 
            this.variancePanel.BackColor = System.Drawing.Color.Transparent;
            this.variancePanel.Controls.Add(this.lblFromFax);
            this.variancePanel.Controls.Add(this.lblFromTelephone);
            this.variancePanel.Controls.Add(this.lblFrom3);
            this.variancePanel.Controls.Add(this.lblFrom2);
            this.variancePanel.Controls.Add(this.lblFrom1);
            this.variancePanel.Location = new System.Drawing.Point(13, 87);
            this.variancePanel.Name = "variancePanel";
            this.variancePanel.Size = new System.Drawing.Size(263, 73);
            this.variancePanel.TabIndex = 154;
            // 
            // lblFromFax
            // 
            this.lblFromFax.AutoSize = true;
            this.lblFromFax.BackColor = System.Drawing.Color.Transparent;
            this.lblFromFax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromFax.Location = new System.Drawing.Point(131, 56);
            this.lblFromFax.Name = "lblFromFax";
            this.lblFromFax.Size = new System.Drawing.Size(31, 13);
            this.lblFromFax.TabIndex = 159;
            this.lblFromFax.Text = "Fax:";
            this.lblFromFax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFromFax.Visible = false;
            // 
            // lblFromTelephone
            // 
            this.lblFromTelephone.AutoSize = true;
            this.lblFromTelephone.BackColor = System.Drawing.Color.Transparent;
            this.lblFromTelephone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromTelephone.Location = new System.Drawing.Point(32, 56);
            this.lblFromTelephone.Name = "lblFromTelephone";
            this.lblFromTelephone.Size = new System.Drawing.Size(33, 13);
            this.lblFromTelephone.TabIndex = 158;
            this.lblFromTelephone.Text = "Tel.:";
            this.lblFromTelephone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFromTelephone.Visible = false;
            // 
            // lblFrom3
            // 
            this.lblFrom3.AutoSize = true;
            this.lblFrom3.BackColor = System.Drawing.Color.Transparent;
            this.lblFrom3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom3.Location = new System.Drawing.Point(3, 39);
            this.lblFrom3.Name = "lblFrom3";
            this.lblFrom3.Size = new System.Drawing.Size(41, 13);
            this.lblFrom3.TabIndex = 157;
            this.lblFrom3.Text = "From3";
            this.lblFrom3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFrom2
            // 
            this.lblFrom2.AutoSize = true;
            this.lblFrom2.BackColor = System.Drawing.Color.Transparent;
            this.lblFrom2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom2.Location = new System.Drawing.Point(3, 23);
            this.lblFrom2.Name = "lblFrom2";
            this.lblFrom2.Size = new System.Drawing.Size(41, 13);
            this.lblFrom2.TabIndex = 156;
            this.lblFrom2.Text = "From2";
            this.lblFrom2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.deleteButton.AutoSize = true;
            this.deleteButton.BackColor = System.Drawing.Color.Transparent;
            this.deleteButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("deleteButton.BackgroundImage")));
            this.deleteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deleteButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.deleteButton.FlatAppearance.BorderSize = 0;
            this.deleteButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.deleteButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Location = new System.Drawing.Point(122, 475);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(100, 40);
            this.deleteButton.TabIndex = 158;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.Transparent;
            this.searchPanel.Controls.Add(this.cmdSearch);
            this.searchPanel.Controls.Add(this.icnLabel);
            this.searchPanel.Controls.Add(this.cmdICNSubmit);
            this.searchPanel.Controls.Add(this.txtIcn);
            this.searchPanel.Location = new System.Drawing.Point(13, 260);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(746, 34);
            this.searchPanel.TabIndex = 160;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmdSearch.AutoSize = true;
            this.cmdSearch.BackColor = System.Drawing.Color.Transparent;
            this.cmdSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdSearch.BackgroundImage")));
            this.cmdSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdSearch.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.cmdSearch.FlatAppearance.BorderSize = 0;
            this.cmdSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cmdSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.ForeColor = System.Drawing.Color.White;
            this.cmdSearch.Location = new System.Drawing.Point(410, 6);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(82, 23);
            this.cmdSearch.TabIndex = 152;
            this.cmdSearch.Text = "Display All";
            this.cmdSearch.UseVisualStyleBackColor = false;
            this.cmdSearch.Click += new System.EventHandler(this.cmdDisplayAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 158;
            this.label1.Text = "From:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 161;
            this.label3.Text = "To:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lblStoreFax);
            this.panel1.Controls.Add(this.storeMgrName);
            this.panel1.Controls.Add(this.storePhNo);
            this.panel1.Controls.Add(this.storeAddrLine2);
            this.panel1.Controls.Add(this.storeAddrLine1);
            this.panel1.Location = new System.Drawing.Point(13, 183);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(263, 78);
            this.panel1.TabIndex = 158;
            // 
            // lblStoreFax
            // 
            this.lblStoreFax.AutoSize = true;
            this.lblStoreFax.BackColor = System.Drawing.Color.Transparent;
            this.lblStoreFax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoreFax.Location = new System.Drawing.Point(131, 34);
            this.lblStoreFax.Name = "lblStoreFax";
            this.lblStoreFax.Size = new System.Drawing.Size(0, 13);
            this.lblStoreFax.TabIndex = 159;
            this.lblStoreFax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStoreFax.Visible = false;
            // 
            // storeMgrName
            // 
            this.storeMgrName.AutoSize = true;
            this.storeMgrName.BackColor = System.Drawing.Color.Transparent;
            this.storeMgrName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storeMgrName.Location = new System.Drawing.Point(3, 61);
            this.storeMgrName.Name = "storeMgrName";
            this.storeMgrName.Size = new System.Drawing.Size(0, 13);
            this.storeMgrName.TabIndex = 158;
            this.storeMgrName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // storePhNo
            // 
            this.storePhNo.AutoSize = true;
            this.storePhNo.BackColor = System.Drawing.Color.Transparent;
            this.storePhNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storePhNo.Location = new System.Drawing.Point(3, 48);
            this.storePhNo.Name = "storePhNo";
            this.storePhNo.Size = new System.Drawing.Size(0, 13);
            this.storePhNo.TabIndex = 157;
            this.storePhNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // storeAddrLine2
            // 
            this.storeAddrLine2.AutoSize = true;
            this.storeAddrLine2.BackColor = System.Drawing.Color.Transparent;
            this.storeAddrLine2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storeAddrLine2.Location = new System.Drawing.Point(3, 19);
            this.storeAddrLine2.Name = "storeAddrLine2";
            this.storeAddrLine2.Size = new System.Drawing.Size(0, 13);
            this.storeAddrLine2.TabIndex = 156;
            this.storeAddrLine2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // storeAddrLine1
            // 
            this.storeAddrLine1.AutoSize = true;
            this.storeAddrLine1.BackColor = System.Drawing.Color.Transparent;
            this.storeAddrLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storeAddrLine1.Location = new System.Drawing.Point(3, 6);
            this.storeAddrLine1.Name = "storeAddrLine1";
            this.storeAddrLine1.Size = new System.Drawing.Size(0, 13);
            this.storeAddrLine1.TabIndex = 152;
            this.storeAddrLine1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblLayout
            // 
            this.tblLayout.BackColor = System.Drawing.Color.Transparent;
            this.tblLayout.ColumnCount = 1;
            this.tblLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.77665F));
            this.tblLayout.Controls.Add(this.transferToList1, 0, 0);
            this.tblLayout.Location = new System.Drawing.Point(282, 65);
            this.tblLayout.Name = "tblLayout";
            this.tblLayout.RowCount = 3;
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.60151F));
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.39849F));
            this.tblLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblLayout.Size = new System.Drawing.Size(245, 184);
            this.tblLayout.TabIndex = 164;
            // 
            // transferToList1
            // 
            this.transferToList1.BackColor = System.Drawing.Color.Transparent;
            this.transferToList1.Location = new System.Drawing.Point(3, 3);
            this.transferToList1.Name = "transferToList1";
            this.transferToList1.Size = new System.Drawing.Size(153, 44);
            this.transferToList1.TabIndex = 0;
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
            // addCommentButton
            // 
            this.addCommentButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.addCommentButton.AutoSize = true;
            this.addCommentButton.BackColor = System.Drawing.Color.Transparent;
            this.addCommentButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("addCommentButton.BackgroundImage")));
            this.addCommentButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.addCommentButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.addCommentButton.FlatAppearance.BorderSize = 0;
            this.addCommentButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.addCommentButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.addCommentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addCommentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addCommentButton.ForeColor = System.Drawing.Color.White;
            this.addCommentButton.Location = new System.Drawing.Point(228, 475);
            this.addCommentButton.Name = "addCommentButton";
            this.addCommentButton.Size = new System.Drawing.Size(100, 40);
            this.addCommentButton.TabIndex = 165;
            this.addCommentButton.Text = "Comment";
            this.addCommentButton.UseVisualStyleBackColor = false;
            this.addCommentButton.Click += new System.EventHandler(this.addCommentButton_Click);
            // 
            // tblLayout2
            // 
            this.tblLayout2.BackColor = System.Drawing.Color.Transparent;
            this.tblLayout2.ColumnCount = 1;
            this.tblLayout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.77665F));
            this.tblLayout2.Controls.Add(this.transferTo1, 0, 0);
            this.tblLayout2.Location = new System.Drawing.Point(527, 67);
            this.tblLayout2.Name = "tblLayout2";
            this.tblLayout2.RowCount = 2;
            this.tblLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.80435F));
            this.tblLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.19566F));
            this.tblLayout2.Size = new System.Drawing.Size(233, 184);
            this.tblLayout2.TabIndex = 166;
            // 
            // transferTo1
            // 
            this.transferTo1.BackColor = System.Drawing.Color.Transparent;
            this.transferTo1.Location = new System.Drawing.Point(3, 3);
            this.transferTo1.Name = "transferTo1";
            this.transferTo1.Size = new System.Drawing.Size(153, 1);
            this.transferTo1.TabIndex = 0;
            // 
            // toTransLabel
            // 
            this.toTransLabel.AutoSize = true;
            this.toTransLabel.BackColor = System.Drawing.Color.Transparent;
            this.toTransLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toTransLabel.Location = new System.Drawing.Point(39, 153);
            this.toTransLabel.Name = "toTransLabel";
            this.toTransLabel.Size = new System.Drawing.Size(0, 13);
            this.toTransLabel.TabIndex = 167;
            this.toTransLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ManageTransferOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(778, 522);
            this.ControlBox = false;
            this.Controls.Add(this.toTransLabel);
            this.Controls.Add(this.tblLayout2);
            this.Controls.Add(this.addCommentButton);
            this.Controls.Add(this.tblLayout);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.variancePanel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.gvMerchandise);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.titleLabel);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ManageTransferOut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ManageTransferOut_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvMerchandise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.variancePanel.ResumeLayout(false);
            this.variancePanel.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tblLayout.ResumeLayout(false);
            this.tblLayout2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label icnLabel;
        private System.Windows.Forms.Button cmdICNSubmit;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.DataGridView gvMerchandise;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtIcn;
        private System.Windows.Forms.Label lblFrom1;
        private System.Windows.Forms.Panel variancePanel;
        private System.Windows.Forms.Label lblFrom2;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFrom3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label storeMgrName;
        private System.Windows.Forms.Label storePhNo;
        private System.Windows.Forms.Label storeAddrLine2;
        private System.Windows.Forms.Label storeAddrLine1;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.TableLayoutPanel tblLayout;
        private TransferTo transferToList1;
        private System.Windows.Forms.Button addCommentButton;
        private TransferTo transferTo1;
        private System.Windows.Forms.TableLayoutPanel tblLayout2;
        private System.Windows.Forms.Label toTransLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRefurb;
        private System.Windows.Forms.Label lblStoreFax;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn RefurbCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colICN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMerchandiseDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComments;
        private System.Windows.Forms.DataGridViewTextBoxColumn colJewelryCaseNumber;
        private System.Windows.Forms.Label lblFromFax;
        private System.Windows.Forms.Label lblFromTelephone;
    }
}
