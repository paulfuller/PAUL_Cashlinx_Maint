using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Customer.ItemRelease
{
    partial class CustomerHoldReleaseList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerHoldReleaseList));
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
            this.labelReleaseDateUpdate = new System.Windows.Forms.Label();
            this.labelPageNo = new System.Windows.Forms.Label();
            this.customDataGridViewMDSE = new Common.Libraries.Forms.Components.CustomDataGridView();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.icn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mdsestatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mdseamt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customDataGridViewTransactions = new Common.Libraries.Forms.Components.CustomDataGridView();
            this.customButtonReleaseHold = new Common.Libraries.Forms.Components.CustomButton();
            this.customButtonUpdateReleaseDate = new Common.Libraries.Forms.Components.CustomButton();
            this.customButtonCancel = new Common.Libraries.Forms.Components.CustomButton();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calendarColumn1 = new Common.Libraries.Forms.Components.CalendarColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonDeselectAll = new Common.Libraries.Forms.Components.CustomButton();
            this.mdse = new System.Windows.Forms.DataGridViewImageColumn();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.trandate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trantype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trannumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pfistate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calendarcolumn = new Common.Libraries.Forms.Components.CalendarColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.holddate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.holdby = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.holdcomment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.origticketnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.labelMainHeading.Size = new System.Drawing.Size(126, 16);
            this.labelMainHeading.TabIndex = 0;
            this.labelMainHeading.Text = "Select for Release";
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
            this.groupBox1.Size = new System.Drawing.Size(848, 2);
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
            this.buttonFirst.Location = new System.Drawing.Point(285, 287);
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
            this.buttonPrevious.Location = new System.Drawing.Point(351, 287);
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
            this.buttonNext.Location = new System.Drawing.Point(417, 287);
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
            this.buttonLast.Location = new System.Drawing.Point(483, 287);
            this.buttonLast.Name = "buttonLast";
            this.buttonLast.Size = new System.Drawing.Size(60, 21);
            this.buttonLast.TabIndex = 58;
            this.buttonLast.Text = "Last";
            this.buttonLast.UseVisualStyleBackColor = false;
            this.buttonLast.Click += new System.EventHandler(this.lastButton_Click);
            // 
            // labelReleaseDateUpdate
            // 
            this.labelReleaseDateUpdate.AutoSize = true;
            this.labelReleaseDateUpdate.BackColor = System.Drawing.Color.Transparent;
            this.labelReleaseDateUpdate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReleaseDateUpdate.ForeColor = System.Drawing.Color.Red;
            this.labelReleaseDateUpdate.Location = new System.Drawing.Point(171, 24);
            this.labelReleaseDateUpdate.Name = "labelReleaseDateUpdate";
            this.labelReleaseDateUpdate.Size = new System.Drawing.Size(230, 16);
            this.labelReleaseDateUpdate.TabIndex = 59;
            this.labelReleaseDateUpdate.Text = "Release dates have been updated";
            this.labelReleaseDateUpdate.Visible = false;
            // 
            // labelPageNo
            // 
            this.labelPageNo.AutoSize = true;
            this.labelPageNo.BackColor = System.Drawing.Color.Transparent;
            this.labelPageNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPageNo.Location = new System.Drawing.Point(680, 77);
            this.labelPageNo.Name = "labelPageNo";
            this.labelPageNo.Size = new System.Drawing.Size(62, 13);
            this.labelPageNo.TabIndex = 64;
            this.labelPageNo.Text = "Page 1 of 1";
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
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewMDSE.DefaultCellStyle = dataGridViewCellStyle2;
            this.customDataGridViewMDSE.GridColor = System.Drawing.Color.LightGray;
            this.customDataGridViewMDSE.Location = new System.Drawing.Point(174, 144);
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
            this.customDataGridViewMDSE.Size = new System.Drawing.Size(529, 81);
            this.customDataGridViewMDSE.TabIndex = 69;
            this.customDataGridViewMDSE.Visible = false;
            this.customDataGridViewMDSE.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMdse_CellDoubleClick);
            this.customDataGridViewMDSE.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewMdse_CellFormatting);
            // 
            // description
            // 
            this.description.HeaderText = "Description";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.Width = 170;
            // 
            // icn
            // 
            this.icn.HeaderText = "ICN";
            this.icn.Name = "icn";
            this.icn.ReadOnly = true;
            this.icn.Width = 120;
            // 
            // mdsestatus
            // 
            this.mdsestatus.HeaderText = "Status";
            this.mdsestatus.Name = "mdsestatus";
            this.mdsestatus.ReadOnly = true;
            this.mdsestatus.Width = 120;
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
            this.trantype,
            this.trannumber,
            this.status,
            this.pfistate,
            this.calendarcolumn,
            this.amount,
            this.holddate,
            this.holdby,
            this.holdcomment,
            this.origticketnumber});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewTransactions.DefaultCellStyle = dataGridViewCellStyle5;
            this.customDataGridViewTransactions.GridColor = System.Drawing.Color.LightGray;
            this.customDataGridViewTransactions.Location = new System.Drawing.Point(20, 104);
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
            this.customDataGridViewTransactions.Size = new System.Drawing.Size(828, 177);
            this.customDataGridViewTransactions.TabIndex = 68;
            this.customDataGridViewTransactions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTransactions_CellClick);
            this.customDataGridViewTransactions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewTransactions_CellFormatting);
            this.customDataGridViewTransactions.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTransactions_CellLeave);
            this.customDataGridViewTransactions.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewTransactions_CellPainting);
            this.customDataGridViewTransactions.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTransactions_CellValueChanged);
            this.customDataGridViewTransactions.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewTransactions_CurrentCellDirtyStateChanged);
            this.customDataGridViewTransactions.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewTransactions_DataError);
            this.customDataGridViewTransactions.Sorted += new System.EventHandler(this.customDataGridViewTransactions_Sorted);
            // 
            // customButtonReleaseHold
            // 
            this.customButtonReleaseHold.BackColor = System.Drawing.Color.Transparent;
            this.customButtonReleaseHold.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonReleaseHold.BackgroundImage")));
            this.customButtonReleaseHold.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonReleaseHold.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonReleaseHold.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonReleaseHold.FlatAppearance.BorderSize = 0;
            this.customButtonReleaseHold.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonReleaseHold.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonReleaseHold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonReleaseHold.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonReleaseHold.ForeColor = System.Drawing.Color.White;
            this.customButtonReleaseHold.Location = new System.Drawing.Point(671, 365);
            this.customButtonReleaseHold.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonReleaseHold.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonReleaseHold.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonReleaseHold.Name = "customButtonReleaseHold";
            this.customButtonReleaseHold.Size = new System.Drawing.Size(100, 50);
            this.customButtonReleaseHold.TabIndex = 67;
            this.customButtonReleaseHold.Text = "&Release Hold";
            this.customButtonReleaseHold.UseVisualStyleBackColor = false;
            this.customButtonReleaseHold.Click += new System.EventHandler(this.buttonReleaseHold_Click);
            // 
            // customButtonUpdateReleaseDate
            // 
            this.customButtonUpdateReleaseDate.BackColor = System.Drawing.Color.Transparent;
            this.customButtonUpdateReleaseDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonUpdateReleaseDate.BackgroundImage")));
            this.customButtonUpdateReleaseDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonUpdateReleaseDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonUpdateReleaseDate.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonUpdateReleaseDate.FlatAppearance.BorderSize = 0;
            this.customButtonUpdateReleaseDate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonUpdateReleaseDate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonUpdateReleaseDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonUpdateReleaseDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonUpdateReleaseDate.ForeColor = System.Drawing.Color.White;
            this.customButtonUpdateReleaseDate.Location = new System.Drawing.Point(549, 365);
            this.customButtonUpdateReleaseDate.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonUpdateReleaseDate.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonUpdateReleaseDate.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonUpdateReleaseDate.Name = "customButtonUpdateReleaseDate";
            this.customButtonUpdateReleaseDate.Size = new System.Drawing.Size(100, 50);
            this.customButtonUpdateReleaseDate.TabIndex = 66;
            this.customButtonUpdateReleaseDate.Text = "&Update Release Date";
            this.customButtonUpdateReleaseDate.UseVisualStyleBackColor = false;
            this.customButtonUpdateReleaseDate.Click += new System.EventHandler(this.buttonUpdateReleaseDate_Click);
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
            // calendarColumn1
            // 
            this.calendarColumn1.HeaderText = "Eligible for Release";
            this.calendarColumn1.Name = "calendarColumn1";
            this.calendarColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.calendarColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.calendarColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Description";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Visible = false;
            this.dataGridViewTextBoxColumn6.Width = 120;
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
            this.dataGridViewTextBoxColumn9.Visible = false;
            this.dataGridViewTextBoxColumn9.Width = 70;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Visible = false;
            this.dataGridViewTextBoxColumn10.Width = 150;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "ICN";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Width = 150;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Status";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Width = 70;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.Width = 150;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.Width = 70;
            // 
            // buttonDeselectAll
            // 
            this.buttonDeselectAll.BackColor = System.Drawing.Color.Transparent;
            this.buttonDeselectAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonDeselectAll.BackgroundImage")));
            this.buttonDeselectAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonDeselectAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDeselectAll.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonDeselectAll.FlatAppearance.BorderSize = 0;
            this.buttonDeselectAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonDeselectAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonDeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeselectAll.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeselectAll.ForeColor = System.Drawing.Color.White;
            this.buttonDeselectAll.Location = new System.Drawing.Point(19, 281);
            this.buttonDeselectAll.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDeselectAll.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonDeselectAll.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonDeselectAll.Name = "buttonDeselectAll";
            this.buttonDeselectAll.Size = new System.Drawing.Size(100, 50);
            this.buttonDeselectAll.TabIndex = 70;
            this.buttonDeselectAll.Text = "Deselect All";
            this.buttonDeselectAll.UseVisualStyleBackColor = false;
            this.buttonDeselectAll.Click += new System.EventHandler(this.buttonDeselectAll_Click);
            // 
            // mdse
            // 
            this.mdse.HeaderText = global::Pawn.Properties.Resources.OverrideMachineName;
            this.mdse.Image = ((System.Drawing.Image)(resources.GetObject("mdse.Image")));
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
            this.trandate.ReadOnly = true;
            // 
            // trantype
            // 
            this.trantype.HeaderText = "Tran. Type";
            this.trantype.Name = "trantype";
            this.trantype.ReadOnly = true;
            // 
            // trannumber
            // 
            this.trannumber.HeaderText = "Tran. Number";
            this.trannumber.Name = "trannumber";
            this.trannumber.ReadOnly = true;
            // 
            // status
            // 
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 150;
            // 
            // pfistate
            // 
            this.pfistate.HeaderText = "pfistate";
            this.pfistate.Name = "pfistate";
            this.pfistate.ReadOnly = true;
            this.pfistate.Visible = false;
            this.pfistate.Width = 20;
            // 
            // calendarcolumn
            // 
            this.calendarcolumn.HeaderText = "Eligible For Release";
            this.calendarcolumn.Name = "calendarcolumn";
            this.calendarcolumn.ReadOnly = true;
            this.calendarcolumn.Width = 150;
            // 
            // amount
            // 
            this.amount.HeaderText = "Amount";
            this.amount.Name = "amount";
            this.amount.ReadOnly = true;
            this.amount.Width = 150;
            // 
            // holddate
            // 
            this.holddate.HeaderText = "Hold Date";
            this.holddate.Name = "holddate";
            this.holddate.ReadOnly = true;
            this.holddate.Visible = false;
            // 
            // holdby
            // 
            this.holdby.HeaderText = "Hold By";
            this.holdby.Name = "holdby";
            this.holdby.ReadOnly = true;
            this.holdby.Visible = false;
            // 
            // holdcomment
            // 
            this.holdcomment.HeaderText = "holdcomment";
            this.holdcomment.Name = "holdcomment";
            this.holdcomment.ReadOnly = true;
            this.holdcomment.Visible = false;
            // 
            // origticketnumber
            // 
            this.origticketnumber.HeaderText = "ORIGTICKETNUMBER";
            this.origticketnumber.Name = "origticketnumber";
            this.origticketnumber.Visible = false;
            // 
            // CustomerHoldReleaseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(857, 428);
            this.ControlBox = false;
            this.Controls.Add(this.buttonDeselectAll);
            this.Controls.Add(this.customDataGridViewMDSE);
            this.Controls.Add(this.customDataGridViewTransactions);
            this.Controls.Add(this.customButtonReleaseHold);
            this.Controls.Add(this.customButtonUpdateReleaseDate);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.labelPageNo);
            this.Controls.Add(this.labelReleaseDateUpdate);
            this.Controls.Add(this.buttonLast);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.buttonFirst);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelSubHeading);
            this.Controls.Add(this.labelMainHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomerHoldReleaseList";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomerHoldReleaseList";
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
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.Label labelReleaseDateUpdate;
        private CalendarColumn calendarColumn1;
        private System.Windows.Forms.Label labelPageNo;
        private CustomButton customButtonCancel;
        private CustomButton customButtonUpdateReleaseDate;
        private CustomButton customButtonReleaseHold;
        private CustomDataGridView customDataGridViewTransactions;
        private CustomDataGridView customDataGridViewMDSE;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn icn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mdsestatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn mdseamt;
        private CustomButton buttonDeselectAll;
        private System.Windows.Forms.DataGridViewImageColumn mdse;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
        private System.Windows.Forms.DataGridViewTextBoxColumn trandate;
        private System.Windows.Forms.DataGridViewTextBoxColumn trantype;
        private System.Windows.Forms.DataGridViewTextBoxColumn trannumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn pfistate;
        private CalendarColumn calendarcolumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn holddate;
        private System.Windows.Forms.DataGridViewTextBoxColumn holdby;
        private System.Windows.Forms.DataGridViewTextBoxColumn holdcomment;
        private System.Windows.Forms.DataGridViewTextBoxColumn origticketnumber;
    }
}