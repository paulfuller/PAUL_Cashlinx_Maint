using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Customer.Holds
{
    partial class PoliceHoldsList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PoliceHoldsList));
            this.labelMainHeading = new System.Windows.Forms.Label();
            this.labelSubHeading = new System.Windows.Forms.Label();
            this.dataGridViewTransactions = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonPrintPickSlip = new System.Windows.Forms.Button();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.buttonLast = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.buttonFirst = new System.Windows.Forms.Button();
            this.labelPageNo = new System.Windows.Forms.Label();
            this.labelMdseUnavailableMsg = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewMdse = new Common.Libraries.Forms.Components.CustomDataGridView();
            this.mdseselect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.icn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mdsestatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mdseamt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.holdid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customButtonDeselectAll = new Common.Libraries.Forms.Components.CustomButton();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mdse = new System.Windows.Forms.DataGridViewImageColumn();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.trandate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trantype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trannumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pfistate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.origtktnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prevtktno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMdse)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMainHeading
            // 
            this.labelMainHeading.AutoSize = true;
            this.labelMainHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelMainHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMainHeading.ForeColor = System.Drawing.Color.White;
            this.labelMainHeading.Location = new System.Drawing.Point(13, 24);
            this.labelMainHeading.Name = "labelMainHeading";
            this.labelMainHeading.Size = new System.Drawing.Size(159, 16);
            this.labelMainHeading.TabIndex = 0;
            this.labelMainHeading.Text = "Select for Police Hold";
            // 
            // labelSubHeading
            // 
            this.labelSubHeading.AutoSize = true;
            this.labelSubHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelSubHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubHeading.Location = new System.Drawing.Point(16, 74);
            this.labelSubHeading.Name = "labelSubHeading";
            this.labelSubHeading.Size = new System.Drawing.Size(98, 16);
            this.labelSubHeading.TabIndex = 1;
            this.labelSubHeading.Text = "Transactions";
            // 
            // dataGridViewTransactions
            // 
            this.dataGridViewTransactions.AllowUserToAddRows = false;
            this.dataGridViewTransactions.AllowUserToDeleteRows = false;
            this.dataGridViewTransactions.AllowUserToResizeColumns = false;
            this.dataGridViewTransactions.AllowUserToResizeRows = false;
            this.dataGridViewTransactions.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.dataGridViewTransactions.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mdse,
            this.select,
            this.trandate,
            this.trantype,
            this.trannumber,
            this.status,
            this.pfistate,
            this.amount,
            this.origtktnumber,
            this.prevtktno});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTransactions.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTransactions.GridColor = System.Drawing.Color.LightGray;
            this.dataGridViewTransactions.Location = new System.Drawing.Point(19, 104);
            this.dataGridViewTransactions.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewTransactions.Name = "dataGridViewTransactions";
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTransactions.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTransactions.RowHeadersVisible = false;
            this.dataGridViewTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewTransactions.Size = new System.Drawing.Size(677, 177);
            this.dataGridViewTransactions.TabIndex = 2;
            this.dataGridViewTransactions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTransactions_CellClick);
            this.dataGridViewTransactions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewTransactions_CellFormatting);
            this.dataGridViewTransactions.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewTransactions_CellPainting);
            this.dataGridViewTransactions.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTransactions_CellValueChanged);
            this.dataGridViewTransactions.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewTransactions_CurrentCellDirtyStateChanged);
            this.dataGridViewTransactions.Sorted += new System.EventHandler(this.dataGridViewTransactions_Sorted);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(6, 366);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(762, 2);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonCancel.BackgroundImage")));
            this.buttonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.Location = new System.Drawing.Point(21, 375);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 50);
            this.buttonCancel.TabIndex = 50;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonPrintPickSlip
            // 
            this.buttonPrintPickSlip.BackColor = System.Drawing.Color.Transparent;
            this.buttonPrintPickSlip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonPrintPickSlip.BackgroundImage")));
            this.buttonPrintPickSlip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonPrintPickSlip.CausesValidation = false;
            this.buttonPrintPickSlip.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonPrintPickSlip.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonPrintPickSlip.FlatAppearance.BorderSize = 0;
            this.buttonPrintPickSlip.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonPrintPickSlip.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonPrintPickSlip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrintPickSlip.ForeColor = System.Drawing.Color.White;
            this.buttonPrintPickSlip.Location = new System.Drawing.Point(470, 382);
            this.buttonPrintPickSlip.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPrintPickSlip.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonPrintPickSlip.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonPrintPickSlip.Name = "buttonPrintPickSlip";
            this.buttonPrintPickSlip.Size = new System.Drawing.Size(100, 50);
            this.buttonPrintPickSlip.TabIndex = 51;
            this.buttonPrintPickSlip.Text = "&Print Pick Slip";
            this.buttonPrintPickSlip.UseVisualStyleBackColor = false;
            this.buttonPrintPickSlip.Click += new System.EventHandler(this.buttonPrintPickSlip_Click);
            // 
            // buttonContinue
            // 
            this.buttonContinue.BackColor = System.Drawing.Color.Transparent;
            this.buttonContinue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonContinue.BackgroundImage")));
            this.buttonContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonContinue.CausesValidation = false;
            this.buttonContinue.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonContinue.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonContinue.FlatAppearance.BorderSize = 0;
            this.buttonContinue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonContinue.ForeColor = System.Drawing.Color.White;
            this.buttonContinue.Location = new System.Drawing.Point(590, 382);
            this.buttonContinue.Margin = new System.Windows.Forms.Padding(4);
            this.buttonContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(100, 50);
            this.buttonContinue.TabIndex = 52;
            this.buttonContinue.Text = "Co&ntinue";
            this.buttonContinue.UseVisualStyleBackColor = false;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
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
            this.buttonLast.Location = new System.Drawing.Point(448, 318);
            this.buttonLast.Name = "buttonLast";
            this.buttonLast.Size = new System.Drawing.Size(60, 21);
            this.buttonLast.TabIndex = 62;
            this.buttonLast.Text = "Last";
            this.buttonLast.UseVisualStyleBackColor = false;
            this.buttonLast.Click += new System.EventHandler(this.lastButton_Click);
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
            this.buttonNext.Location = new System.Drawing.Point(382, 318);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(60, 21);
            this.buttonNext.TabIndex = 61;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = false;
            this.buttonNext.Click += new System.EventHandler(this.nextButton_Click);
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
            this.buttonPrevious.Location = new System.Drawing.Point(316, 318);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(60, 21);
            this.buttonPrevious.TabIndex = 60;
            this.buttonPrevious.Text = "Previous";
            this.buttonPrevious.UseVisualStyleBackColor = false;
            this.buttonPrevious.Click += new System.EventHandler(this.previousButton_Click);
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
            this.buttonFirst.Location = new System.Drawing.Point(250, 318);
            this.buttonFirst.Name = "buttonFirst";
            this.buttonFirst.Size = new System.Drawing.Size(60, 21);
            this.buttonFirst.TabIndex = 59;
            this.buttonFirst.Text = "First";
            this.buttonFirst.UseVisualStyleBackColor = false;
            this.buttonFirst.Click += new System.EventHandler(this.buttonFirst_Click);
            // 
            // labelPageNo
            // 
            this.labelPageNo.AutoSize = true;
            this.labelPageNo.BackColor = System.Drawing.Color.Transparent;
            this.labelPageNo.Location = new System.Drawing.Point(592, 74);
            this.labelPageNo.Name = "labelPageNo";
            this.labelPageNo.Size = new System.Drawing.Size(62, 13);
            this.labelPageNo.TabIndex = 63;
            this.labelPageNo.Text = "Page 1 of 1";
            // 
            // labelMdseUnavailableMsg
            // 
            this.labelMdseUnavailableMsg.AutoSize = true;
            this.labelMdseUnavailableMsg.BackColor = System.Drawing.Color.White;
            this.labelMdseUnavailableMsg.ForeColor = System.Drawing.Color.Red;
            this.labelMdseUnavailableMsg.Location = new System.Drawing.Point(187, 161);
            this.labelMdseUnavailableMsg.Name = "labelMdseUnavailableMsg";
            this.labelMdseUnavailableMsg.Size = new System.Drawing.Size(302, 13);
            this.labelMdseUnavailableMsg.TabIndex = 64;
            this.labelMdseUnavailableMsg.Text = "One or more items for this transactions are unavailable for hold.";
            this.labelMdseUnavailableMsg.Visible = false;
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
            // dataGridViewMdse
            // 
            this.dataGridViewMdse.AllowUserToAddRows = false;
            this.dataGridViewMdse.AllowUserToDeleteRows = false;
            this.dataGridViewMdse.AllowUserToResizeColumns = false;
            this.dataGridViewMdse.AllowUserToResizeRows = false;
            this.dataGridViewMdse.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewMdse.CausesValidation = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewMdse.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewMdse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMdse.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mdseselect,
            this.description,
            this.icn,
            this.mdsestatus,
            this.mdseamt,
            this.holdid});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewMdse.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewMdse.GridColor = System.Drawing.Color.LightGray;
            this.dataGridViewMdse.Location = new System.Drawing.Point(190, 175);
            this.dataGridViewMdse.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewMdse.Name = "dataGridViewMdse";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMdse.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewMdse.RowHeadersVisible = false;
            this.dataGridViewMdse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewMdse.Size = new System.Drawing.Size(490, 78);
            this.dataGridViewMdse.TabIndex = 66;
            this.dataGridViewMdse.Visible = false;
            this.dataGridViewMdse.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMdse_CellDoubleClick);
            this.dataGridViewMdse.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewMdse_CellFormatting);
            this.dataGridViewMdse.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMdse_CellValueChanged);
            this.dataGridViewMdse.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewMdse_CurrentCellDirtyStateChanged);
            // 
            // mdseselect
            // 
            this.mdseselect.FalseValue = "false";
            this.mdseselect.HeaderText = "Select";
            this.mdseselect.Name = "mdseselect";
            this.mdseselect.TrueValue = "true";
            this.mdseselect.Width = 50;
            // 
            // description
            // 
            this.description.HeaderText = "Description";
            this.description.Name = "description";
            this.description.Width = 150;
            // 
            // icn
            // 
            this.icn.HeaderText = "ICN";
            this.icn.Name = "icn";
            // 
            // mdsestatus
            // 
            this.mdsestatus.HeaderText = "Status";
            this.mdsestatus.Name = "mdsestatus";
            // 
            // mdseamt
            // 
            this.mdseamt.HeaderText = "Amount";
            this.mdseamt.Name = "mdseamt";
            this.mdseamt.Width = 70;
            // 
            // holdid
            // 
            this.holdid.HeaderText = "HoldID";
            this.holdid.Name = "holdid";
            this.holdid.Visible = false;
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
            this.customButtonDeselectAll.Location = new System.Drawing.Point(19, 303);
            this.customButtonDeselectAll.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonDeselectAll.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonDeselectAll.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonDeselectAll.Name = "customButtonDeselectAll";
            this.customButtonDeselectAll.Size = new System.Drawing.Size(100, 50);
            this.customButtonDeselectAll.TabIndex = 65;
            this.customButtonDeselectAll.Text = "Deselect All";
            this.customButtonDeselectAll.UseVisualStyleBackColor = false;
            this.customButtonDeselectAll.Click += new System.EventHandler(this.buttonDeselectAll_Click);
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
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "HoldID";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.Visible = false;
            // 
            // mdse
            // 
            this.mdse.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = null;
            this.mdse.DefaultCellStyle = dataGridViewCellStyle2;
            this.mdse.FillWeight = 50F;
            this.mdse.HeaderText = global::Pawn.Properties.Resources.OverrideMachineName;
            this.mdse.Image = ((System.Drawing.Image)(resources.GetObject("mdse.Image")));
            this.mdse.Name = "mdse";
            this.mdse.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.mdse.Width = 5;
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
            // amount
            // 
            this.amount.HeaderText = "Amount";
            this.amount.Name = "amount";
            this.amount.ReadOnly = true;
            this.amount.Width = 150;
            // 
            // origtktnumber
            // 
            this.origtktnumber.HeaderText = "originalloan";
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
            // PoliceHoldsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(763, 444);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridViewMdse);
            this.Controls.Add(this.customButtonDeselectAll);
            this.Controls.Add(this.labelMdseUnavailableMsg);
            this.Controls.Add(this.labelPageNo);
            this.Controls.Add(this.buttonLast);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.buttonFirst);
            this.Controls.Add(this.buttonContinue);
            this.Controls.Add(this.buttonPrintPickSlip);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridViewTransactions);
            this.Controls.Add(this.labelSubHeading);
            this.Controls.Add(this.labelMainHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PoliceHoldsList";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomerHoldsList";
            this.Load += new System.EventHandler(this.PoliceHoldsList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMdse)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMainHeading;
        private System.Windows.Forms.Label labelSubHeading;
        private System.Windows.Forms.DataGridView dataGridViewTransactions;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonPrintPickSlip;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.Button buttonLast;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button buttonFirst;
        private System.Windows.Forms.Label labelPageNo;
        private System.Windows.Forms.Label labelMdseUnavailableMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private CustomButton customButtonDeselectAll;
        private CustomDataGridView dataGridViewMdse;
        private System.Windows.Forms.DataGridViewCheckBoxColumn mdseselect;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn icn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mdsestatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn mdseamt;
        private System.Windows.Forms.DataGridViewTextBoxColumn holdid;
        private System.Windows.Forms.DataGridViewImageColumn mdse;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
        private System.Windows.Forms.DataGridViewTextBoxColumn trandate;
        private System.Windows.Forms.DataGridViewTextBoxColumn trantype;
        private System.Windows.Forms.DataGridViewTextBoxColumn trannumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn pfistate;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn origtktnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn prevtktno;
    }
}
