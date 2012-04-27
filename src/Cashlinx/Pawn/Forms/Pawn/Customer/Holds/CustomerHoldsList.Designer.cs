using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Customer.Holds
{
    partial class CustomerHoldsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerHoldsList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelMainHeading = new System.Windows.Forms.Label();
            this.labelSubHeading = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonFirst = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonLast = new System.Windows.Forms.Button();
            this.labelPageNo = new System.Windows.Forms.Label();
            this.customButtonDeselectAll = new CustomButton();
            this.customDataGridViewMDSE = new CustomDataGridView();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.icn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mdsestatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mdseamt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customDataGridViewTransactions = new CustomDataGridView();
            this.mdse = new System.Windows.Forms.DataGridViewImageColumn();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.trandate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tranType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trannumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pfistate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.origtktnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prevtktno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customButtonContinue = new CustomButton();
            this.customButtonPickSlipPrint = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewMDSE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMainHeading
            // 
            this.labelMainHeading.AutoSize = true;
            this.labelMainHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelMainHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMainHeading.ForeColor = System.Drawing.Color.White;
            this.labelMainHeading.Location = new System.Drawing.Point(13, 24);
            this.labelMainHeading.Name = "labelMainHeading";
            this.labelMainHeading.Size = new System.Drawing.Size(169, 16);
            this.labelMainHeading.TabIndex = 0;
            this.labelMainHeading.Text = "Select for Customer Hold";
            // 
            // labelSubHeading
            // 
            this.labelSubHeading.AutoSize = true;
            this.labelSubHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelSubHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubHeading.Location = new System.Drawing.Point(16, 74);
            this.labelSubHeading.Name = "labelSubHeading";
            this.labelSubHeading.Size = new System.Drawing.Size(91, 16);
            this.labelSubHeading.TabIndex = 1;
            this.labelSubHeading.Text = "Transactions";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(4, 341);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 2);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // buttonFirst
            // 
            this.buttonFirst.BackColor = System.Drawing.Color.Transparent;
            this.buttonFirst.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonFirst.BackgroundImage")));
            this.buttonFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonFirst.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonFirst.FlatAppearance.BorderSize = 0;
            this.buttonFirst.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonFirst.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFirst.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFirst.ForeColor = System.Drawing.Color.White;
            this.buttonFirst.Location = new System.Drawing.Point(228, 287);
            this.buttonFirst.Name = "buttonFirst";
            this.buttonFirst.Size = new System.Drawing.Size(60, 21);
            this.buttonFirst.TabIndex = 55;
            this.buttonFirst.Text = "First";
            this.buttonFirst.UseVisualStyleBackColor = false;
            this.buttonFirst.Click += new System.EventHandler(this.buttonFirst_Click);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.BackColor = System.Drawing.Color.Transparent;
            this.buttonPrevious.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonPrevious.BackgroundImage")));
            this.buttonPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPrevious.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonPrevious.FlatAppearance.BorderSize = 0;
            this.buttonPrevious.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonPrevious.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrevious.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrevious.ForeColor = System.Drawing.Color.White;
            this.buttonPrevious.Location = new System.Drawing.Point(294, 287);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(60, 21);
            this.buttonPrevious.TabIndex = 56;
            this.buttonPrevious.Text = "Previous";
            this.buttonPrevious.UseVisualStyleBackColor = false;
            this.buttonPrevious.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.BackColor = System.Drawing.Color.Transparent;
            this.buttonNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonNext.BackgroundImage")));
            this.buttonNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonNext.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonNext.FlatAppearance.BorderSize = 0;
            this.buttonNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNext.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNext.ForeColor = System.Drawing.Color.White;
            this.buttonNext.Location = new System.Drawing.Point(360, 287);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(60, 21);
            this.buttonNext.TabIndex = 57;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = false;
            this.buttonNext.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // buttonLast
            // 
            this.buttonLast.BackColor = System.Drawing.Color.Transparent;
            this.buttonLast.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonLast.BackgroundImage")));
            this.buttonLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonLast.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonLast.FlatAppearance.BorderSize = 0;
            this.buttonLast.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonLast.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLast.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLast.ForeColor = System.Drawing.Color.White;
            this.buttonLast.Location = new System.Drawing.Point(426, 287);
            this.buttonLast.Name = "buttonLast";
            this.buttonLast.Size = new System.Drawing.Size(60, 21);
            this.buttonLast.TabIndex = 58;
            this.buttonLast.Text = "Last";
            this.buttonLast.UseVisualStyleBackColor = false;
            this.buttonLast.Click += new System.EventHandler(this.lastButton_Click);
            // 
            // labelPageNo
            // 
            this.labelPageNo.AutoSize = true;
            this.labelPageNo.BackColor = System.Drawing.Color.Transparent;
            this.labelPageNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPageNo.Location = new System.Drawing.Point(566, 77);
            this.labelPageNo.Name = "labelPageNo";
            this.labelPageNo.Size = new System.Drawing.Size(62, 13);
            this.labelPageNo.TabIndex = 64;
            this.labelPageNo.Text = "Page 1 of 1";
            // 
            // customButtonDeselectAll
            // 
            this.customButtonDeselectAll.BackColor = System.Drawing.Color.Transparent;
            this.customButtonDeselectAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonDeselectAll.BackgroundImage")));
            this.customButtonDeselectAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonDeselectAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonDeselectAll.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonDeselectAll.FlatAppearance.BorderSize = 0;
            this.customButtonDeselectAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonDeselectAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonDeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonDeselectAll.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonDeselectAll.ForeColor = System.Drawing.Color.White;
            this.customButtonDeselectAll.Location = new System.Drawing.Point(9, 277);
            this.customButtonDeselectAll.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonDeselectAll.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonDeselectAll.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonDeselectAll.Name = "customButtonDeselectAll";
            this.customButtonDeselectAll.Size = new System.Drawing.Size(100, 50);
            this.customButtonDeselectAll.TabIndex = 69;
            this.customButtonDeselectAll.Text = "Deselect All";
            this.customButtonDeselectAll.UseVisualStyleBackColor = false;
            this.customButtonDeselectAll.Click += new System.EventHandler(this.buttonDeselectAll_Click);
            // 
            // customDataGridViewMDSE
            // 
            this.customDataGridViewMDSE.AllowUserToAddRows = false;
            this.customDataGridViewMDSE.AllowUserToDeleteRows = false;
            this.customDataGridViewMDSE.AllowUserToResizeColumns = false;
            this.customDataGridViewMDSE.AllowUserToResizeRows = false;
            this.customDataGridViewMDSE.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.customDataGridViewMDSE.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewMDSE.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataGridViewMDSE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridViewMDSE.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.description,
            this.icn,
            this.mdsestatus,
            this.mdseamt});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewMDSE.DefaultCellStyle = dataGridViewCellStyle2;
            this.customDataGridViewMDSE.GridColor = System.Drawing.Color.LightGray;
            this.customDataGridViewMDSE.Location = new System.Drawing.Point(162, 139);
            this.customDataGridViewMDSE.Margin = new System.Windows.Forms.Padding(0);
            this.customDataGridViewMDSE.Name = "customDataGridViewMDSE";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridViewMDSE.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.customDataGridViewMDSE.RowHeadersVisible = false;
            this.customDataGridViewMDSE.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.customDataGridViewMDSE.Size = new System.Drawing.Size(466, 81);
            this.customDataGridViewMDSE.TabIndex = 68;
            this.customDataGridViewMDSE.Visible = false;
            this.customDataGridViewMDSE.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMdse_CellDoubleClick);
            this.customDataGridViewMDSE.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewMdse_CellFormatting);
            // 
            // description
            // 
            this.description.HeaderText = "Description";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.Width = 150;
            // 
            // icn
            // 
            this.icn.HeaderText = "ICN";
            this.icn.Name = "icn";
            this.icn.ReadOnly = true;
            // 
            // mdsestatus
            // 
            this.mdsestatus.HeaderText = "Status";
            this.mdsestatus.Name = "mdsestatus";
            this.mdsestatus.ReadOnly = true;
            // 
            // mdseamt
            // 
            this.mdseamt.HeaderText = "Amount";
            this.mdseamt.Name = "mdseamt";
            this.mdseamt.ReadOnly = true;
            this.mdseamt.Width = 70;
            // 
            // customDataGridViewTransactions
            // 
            this.customDataGridViewTransactions.AllowUserToAddRows = false;
            this.customDataGridViewTransactions.AllowUserToDeleteRows = false;
            this.customDataGridViewTransactions.AllowUserToResizeColumns = false;
            this.customDataGridViewTransactions.AllowUserToResizeRows = false;
            this.customDataGridViewTransactions.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.customDataGridViewTransactions.CausesValidation = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.customDataGridViewTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridViewTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mdse,
            this.select,
            this.trandate,
            this.tranType,
            this.trannumber,
            this.status,
            this.pfistate,
            this.amount,
            this.origtktnumber,
            this.prevtktno});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewTransactions.DefaultCellStyle = dataGridViewCellStyle5;
            this.customDataGridViewTransactions.GridColor = System.Drawing.Color.LightGray;
            this.customDataGridViewTransactions.Location = new System.Drawing.Point(16, 100);
            this.customDataGridViewTransactions.Margin = new System.Windows.Forms.Padding(0);
            this.customDataGridViewTransactions.Name = "customDataGridViewTransactions";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridViewTransactions.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.customDataGridViewTransactions.RowHeadersVisible = false;
            this.customDataGridViewTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.customDataGridViewTransactions.Size = new System.Drawing.Size(627, 177);
            this.customDataGridViewTransactions.TabIndex = 1;
            this.customDataGridViewTransactions.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTransactions_CellValueChanged);
            this.customDataGridViewTransactions.Sorted += new System.EventHandler(this.customDataGridViewTransactions_Sorted);
            this.customDataGridViewTransactions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewTransactions_CellFormatting);
            this.customDataGridViewTransactions.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewTransactions_CellPainting);
            this.customDataGridViewTransactions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTransactions_CellClick);
            this.customDataGridViewTransactions.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewTransactions_CurrentCellDirtyStateChanged);
            // 
            // mdse
            // 
            this.mdse.HeaderText = "";
            this.mdse.Image = global::Common.Properties.Resources.plus_icon_small;
            this.mdse.Name = "mdse";
            this.mdse.Width = 20;
            // 
            // select
            // 
            this.select.FalseValue = "false";
            this.select.HeaderText = "Select";
            this.select.Name = "select";
            this.select.TrueValue = "true";
            this.select.Width = 50;
            // 
            // trandate
            // 
            this.trandate.HeaderText = "Tran. Date";
            this.trandate.Name = "trandate";
            // 
            // tranType
            // 
            this.tranType.HeaderText = "Tran. Type";
            this.tranType.Name = "tranType";
            // 
            // trannumber
            // 
            this.trannumber.HeaderText = "Tran. Number";
            this.trannumber.Name = "trannumber";
            // 
            // status
            // 
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            // 
            // pfistate
            // 
            this.pfistate.HeaderText = "pfistate";
            this.pfistate.Name = "pfistate";
            this.pfistate.ReadOnly = true;
            this.pfistate.Visible = false;
            this.pfistate.Width = 20;
            // 
            // amount
            // 
            this.amount.HeaderText = "Amount";
            this.amount.Name = "amount";
            this.amount.ReadOnly = true;
            // 
            // origtktnumber
            // 
            this.origtktnumber.HeaderText = "Original Loan";
            this.origtktnumber.Name = "origtktnumber";
            this.origtktnumber.ReadOnly = true;
            this.origtktnumber.Visible = false;
            // 
            // prevtktno
            // 
            this.prevtktno.HeaderText = "prevloanno";
            this.prevtktno.Name = "prevtktno";
            this.prevtktno.ReadOnly = true;
            this.prevtktno.Visible = false;
            // 
            // customButtonContinue
            // 
            this.customButtonContinue.BackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonContinue.BackgroundImage")));
            this.customButtonContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonContinue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonContinue.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonContinue.FlatAppearance.BorderSize = 0;
            this.customButtonContinue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonContinue.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonContinue.ForeColor = System.Drawing.Color.White;
            this.customButtonContinue.Location = new System.Drawing.Point(560, 365);
            this.customButtonContinue.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.Name = "customButtonContinue";
            this.customButtonContinue.Size = new System.Drawing.Size(100, 50);
            this.customButtonContinue.TabIndex = 67;
            this.customButtonContinue.Text = "Co&ntinue";
            this.customButtonContinue.UseVisualStyleBackColor = false;
            this.customButtonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // customButtonPickSlipPrint
            // 
            this.customButtonPickSlipPrint.BackColor = System.Drawing.Color.Transparent;
            this.customButtonPickSlipPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonPickSlipPrint.BackgroundImage")));
            this.customButtonPickSlipPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonPickSlipPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonPickSlipPrint.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonPickSlipPrint.FlatAppearance.BorderSize = 0;
            this.customButtonPickSlipPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonPickSlipPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonPickSlipPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonPickSlipPrint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonPickSlipPrint.ForeColor = System.Drawing.Color.White;
            this.customButtonPickSlipPrint.Location = new System.Drawing.Point(450, 365);
            this.customButtonPickSlipPrint.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonPickSlipPrint.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonPickSlipPrint.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonPickSlipPrint.Name = "customButtonPickSlipPrint";
            this.customButtonPickSlipPrint.Size = new System.Drawing.Size(100, 50);
            this.customButtonPickSlipPrint.TabIndex = 66;
            this.customButtonPickSlipPrint.Text = "&Print Pick Slip";
            this.customButtonPickSlipPrint.UseVisualStyleBackColor = false;
            this.customButtonPickSlipPrint.Click += new System.EventHandler(this.buttonPrintPickSlip_Click);
            // 
            // customButtonCancel
            // 
            this.customButtonCancel.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.customButtonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCancel.FlatAppearance.BorderSize = 0;
            this.customButtonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCancel.ForeColor = System.Drawing.Color.White;
            this.customButtonCancel.Location = new System.Drawing.Point(19, 365);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 65;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.FillWeight = 50F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Tran. Date";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Tran. Type";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Tran. Number";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Status";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Visible = false;
            this.dataGridViewTextBoxColumn5.Width = 20;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Description";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "ICN";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Visible = false;
            this.dataGridViewTextBoxColumn7.Width = 150;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Status";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Visible = false;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 70;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Status";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 70;
            // 
            // CustomerHoldsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_400_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(678, 428);
            this.ControlBox = false;
            this.Controls.Add(this.customButtonDeselectAll);
            this.Controls.Add(this.customDataGridViewMDSE);
            this.Controls.Add(this.customDataGridViewTransactions);
            this.Controls.Add(this.customButtonContinue);
            this.Controls.Add(this.customButtonPickSlipPrint);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.labelPageNo);
            this.Controls.Add(this.buttonLast);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.buttonFirst);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelSubHeading);
            this.Controls.Add(this.labelMainHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomerHoldsList";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomerHoldsList";
            this.Load += new System.EventHandler(this.CustomerHoldsList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewMDSE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewTransactions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMainHeading;
        private System.Windows.Forms.Label labelSubHeading;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.Button buttonFirst;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonLast;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.Label labelPageNo;
        private CustomButton customButtonCancel;
        private CustomButton customButtonPickSlipPrint;
        private CustomButton customButtonContinue;
        private CustomDataGridView customDataGridViewTransactions;
        private System.Windows.Forms.DataGridViewImageColumn mdse;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
        private System.Windows.Forms.DataGridViewTextBoxColumn trandate;
        private System.Windows.Forms.DataGridViewTextBoxColumn tranType;
        private System.Windows.Forms.DataGridViewTextBoxColumn trannumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn pfistate;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn origtktnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn prevtktno;
        private CustomDataGridView customDataGridViewMDSE;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn icn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mdsestatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn mdseamt;
        private CustomButton customButtonDeselectAll;
    }
}
