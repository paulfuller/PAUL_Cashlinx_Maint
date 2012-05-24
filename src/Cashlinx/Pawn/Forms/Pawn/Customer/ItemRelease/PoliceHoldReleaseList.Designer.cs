using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Customer.ItemRelease
{
    partial class PoliceHoldReleaseList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PoliceHoldReleaseList));
            this.labelMainHeading = new System.Windows.Forms.Label();
            this.labelSubHeading = new System.Windows.Forms.Label();
            this.dataGridViewTransactions = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonUpdateReleaseDate = new System.Windows.Forms.Button();
            this.buttonReleaseHold = new System.Windows.Forms.Button();
            this.buttonFirst = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonLast = new System.Windows.Forms.Button();
            this.labelReleaseDateUpdate = new System.Windows.Forms.Label();
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
            this.buttonClaimantRelease = new System.Windows.Forms.Button();
            this.labelPageNo = new System.Windows.Forms.Label();
            this.buttonDeselectAll = new Common.Libraries.Forms.Components.CustomButton();
            this.labelMdseUnavailableMsg = new System.Windows.Forms.Label();
            this.dataGridViewMdse = new Common.Libraries.Forms.Components.CustomDataGridView();
            this.mdseselect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.icn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mdsestatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mdseamt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mdseReleasedate = new Common.Libraries.Forms.Components.CalendarColumn();
            this.mdse = new System.Windows.Forms.DataGridViewImageColumn();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.trandate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trantype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trannumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pfistate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.releasedate = new Common.Libraries.Forms.Components.CalendarColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.holddate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.holdby = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.holdcomment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orgticketnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.labelMainHeading.Size = new System.Drawing.Size(137, 16);
            this.labelMainHeading.TabIndex = 0;
            this.labelMainHeading.Text = "Select for Release";
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
            this.dataGridViewTransactions.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewTransactions.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.releasedate,
            this.amount,
            this.holddate,
            this.holdby,
            this.holdcomment,
            this.orgticketnumber});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTransactions.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTransactions.Location = new System.Drawing.Point(20, 110);
            this.dataGridViewTransactions.Name = "dataGridViewTransactions";
            this.dataGridViewTransactions.RowHeadersVisible = false;
            this.dataGridViewTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewTransactions.Size = new System.Drawing.Size(806, 202);
            this.dataGridViewTransactions.TabIndex = 2;
            this.dataGridViewTransactions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTransactions_CellClick);
            this.dataGridViewTransactions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewTransactions_CellFormatting);
            this.dataGridViewTransactions.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTransactions_CellLeave);
            this.dataGridViewTransactions.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewTransactions_CellPainting);
            this.dataGridViewTransactions.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTransactions_CellValueChanged);
            this.dataGridViewTransactions.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewTransactions_CurrentCellDirtyStateChanged);
            this.dataGridViewTransactions.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewTransactions_DataError);
            this.dataGridViewTransactions.Sorted += new System.EventHandler(this.dataGridViewTransactions_Sorted);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(4, 365);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(852, 2);
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
            this.buttonCancel.Location = new System.Drawing.Point(16, 369);
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
            // buttonUpdateReleaseDate
            // 
            this.buttonUpdateReleaseDate.AllowDrop = true;
            this.buttonUpdateReleaseDate.BackColor = System.Drawing.Color.Transparent;
            this.buttonUpdateReleaseDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonUpdateReleaseDate.BackgroundImage")));
            this.buttonUpdateReleaseDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonUpdateReleaseDate.CausesValidation = false;
            this.buttonUpdateReleaseDate.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonUpdateReleaseDate.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonUpdateReleaseDate.FlatAppearance.BorderSize = 0;
            this.buttonUpdateReleaseDate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonUpdateReleaseDate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonUpdateReleaseDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUpdateReleaseDate.ForeColor = System.Drawing.Color.White;
            this.buttonUpdateReleaseDate.Location = new System.Drawing.Point(393, 369);
            this.buttonUpdateReleaseDate.Margin = new System.Windows.Forms.Padding(4);
            this.buttonUpdateReleaseDate.MaximumSize = new System.Drawing.Size(150, 50);
            this.buttonUpdateReleaseDate.MinimumSize = new System.Drawing.Size(150, 50);
            this.buttonUpdateReleaseDate.Name = "buttonUpdateReleaseDate";
            this.buttonUpdateReleaseDate.Size = new System.Drawing.Size(150, 50);
            this.buttonUpdateReleaseDate.TabIndex = 51;
            this.buttonUpdateReleaseDate.Text = "&Update Release Date";
            this.buttonUpdateReleaseDate.UseVisualStyleBackColor = false;
            this.buttonUpdateReleaseDate.Click += new System.EventHandler(this.buttonUpdateReleaseDate_Click);
            // 
            // buttonReleaseHold
            // 
            this.buttonReleaseHold.BackColor = System.Drawing.Color.Transparent;
            this.buttonReleaseHold.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonReleaseHold.BackgroundImage")));
            this.buttonReleaseHold.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonReleaseHold.CausesValidation = false;
            this.buttonReleaseHold.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonReleaseHold.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonReleaseHold.FlatAppearance.BorderSize = 0;
            this.buttonReleaseHold.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonReleaseHold.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonReleaseHold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReleaseHold.ForeColor = System.Drawing.Color.White;
            this.buttonReleaseHold.Location = new System.Drawing.Point(696, 369);
            this.buttonReleaseHold.Margin = new System.Windows.Forms.Padding(4);
            this.buttonReleaseHold.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonReleaseHold.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonReleaseHold.Name = "buttonReleaseHold";
            this.buttonReleaseHold.Size = new System.Drawing.Size(100, 50);
            this.buttonReleaseHold.TabIndex = 52;
            this.buttonReleaseHold.Text = "Release &Hold";
            this.buttonReleaseHold.UseVisualStyleBackColor = false;
            this.buttonReleaseHold.Click += new System.EventHandler(this.buttonReleaseHold_Click);
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
            this.buttonFirst.Location = new System.Drawing.Point(276, 330);
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
            this.buttonPrevious.Location = new System.Drawing.Point(351, 330);
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
            this.buttonNext.Location = new System.Drawing.Point(417, 330);
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
            this.buttonLast.Location = new System.Drawing.Point(483, 330);
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
            this.labelReleaseDateUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReleaseDateUpdate.ForeColor = System.Drawing.Color.Red;
            this.labelReleaseDateUpdate.Location = new System.Drawing.Point(171, 24);
            this.labelReleaseDateUpdate.Name = "labelReleaseDateUpdate";
            this.labelReleaseDateUpdate.Size = new System.Drawing.Size(248, 16);
            this.labelReleaseDateUpdate.TabIndex = 59;
            this.labelReleaseDateUpdate.Text = "Release dates have been updated";
            this.labelReleaseDateUpdate.Visible = false;
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
            // buttonClaimantRelease
            // 
            this.buttonClaimantRelease.AllowDrop = true;
            this.buttonClaimantRelease.BackColor = System.Drawing.Color.Transparent;
            this.buttonClaimantRelease.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonClaimantRelease.BackgroundImage")));
            this.buttonClaimantRelease.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonClaimantRelease.CausesValidation = false;
            this.buttonClaimantRelease.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClaimantRelease.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonClaimantRelease.FlatAppearance.BorderSize = 0;
            this.buttonClaimantRelease.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonClaimantRelease.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonClaimantRelease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClaimantRelease.ForeColor = System.Drawing.Color.White;
            this.buttonClaimantRelease.Location = new System.Drawing.Point(541, 369);
            this.buttonClaimantRelease.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClaimantRelease.MaximumSize = new System.Drawing.Size(150, 50);
            this.buttonClaimantRelease.MinimumSize = new System.Drawing.Size(150, 50);
            this.buttonClaimantRelease.Name = "buttonClaimantRelease";
            this.buttonClaimantRelease.Size = new System.Drawing.Size(150, 50);
            this.buttonClaimantRelease.TabIndex = 60;
            this.buttonClaimantRelease.Text = "&Release to Claimant";
            this.buttonClaimantRelease.UseVisualStyleBackColor = false;
            this.buttonClaimantRelease.Click += new System.EventHandler(this.buttonClaimantRelease_Click);
            // 
            // labelPageNo
            // 
            this.labelPageNo.AutoSize = true;
            this.labelPageNo.BackColor = System.Drawing.Color.Transparent;
            this.labelPageNo.Location = new System.Drawing.Point(693, 77);
            this.labelPageNo.Name = "labelPageNo";
            this.labelPageNo.Size = new System.Drawing.Size(62, 13);
            this.labelPageNo.TabIndex = 64;
            this.labelPageNo.Text = "Page 1 of 1";
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
            this.buttonDeselectAll.Location = new System.Drawing.Point(16, 315);
            this.buttonDeselectAll.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDeselectAll.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonDeselectAll.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonDeselectAll.Name = "buttonDeselectAll";
            this.buttonDeselectAll.Size = new System.Drawing.Size(100, 50);
            this.buttonDeselectAll.TabIndex = 65;
            this.buttonDeselectAll.Text = "Deselect All";
            this.buttonDeselectAll.UseVisualStyleBackColor = false;
            this.buttonDeselectAll.Click += new System.EventHandler(this.buttonDeselectAll_Click);
            // 
            // labelMdseUnavailableMsg
            // 
            this.labelMdseUnavailableMsg.AutoSize = true;
            this.labelMdseUnavailableMsg.BackColor = System.Drawing.Color.White;
            this.labelMdseUnavailableMsg.ForeColor = System.Drawing.Color.Red;
            this.labelMdseUnavailableMsg.Location = new System.Drawing.Point(187, 161);
            this.labelMdseUnavailableMsg.Name = "labelMdseUnavailableMsg";
            this.labelMdseUnavailableMsg.Size = new System.Drawing.Size(316, 13);
            this.labelMdseUnavailableMsg.TabIndex = 66;
            this.labelMdseUnavailableMsg.Text = "One or more items for this transactions are unavailable for release.";
            this.labelMdseUnavailableMsg.Visible = false;
            // 
            // dataGridViewMdse
            // 
            this.dataGridViewMdse.AllowUserToAddRows = false;
            this.dataGridViewMdse.AllowUserToDeleteRows = false;
            this.dataGridViewMdse.AllowUserToResizeColumns = false;
            this.dataGridViewMdse.AllowUserToResizeRows = false;
            this.dataGridViewMdse.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewMdse.CausesValidation = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewMdse.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewMdse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMdse.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mdseselect,
            this.description,
            this.icn,
            this.mdsestatus,
            this.mdseamt,
            this.mdseReleasedate});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewMdse.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewMdse.GridColor = System.Drawing.Color.LightGray;
            this.dataGridViewMdse.Location = new System.Drawing.Point(190, 175);
            this.dataGridViewMdse.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewMdse.Name = "dataGridViewMdse";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMdse.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewMdse.RowHeadersVisible = false;
            this.dataGridViewMdse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewMdse.Size = new System.Drawing.Size(623, 78);
            this.dataGridViewMdse.TabIndex = 67;
            this.dataGridViewMdse.Visible = false;
            this.dataGridViewMdse.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMdse_CellDoubleClick);
            this.dataGridViewMdse.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewMdse_CellFormatting);
            this.dataGridViewMdse.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMdse_CellLeave);
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
            this.icn.Width = 120;
            // 
            // mdsestatus
            // 
            this.mdsestatus.HeaderText = "Status";
            this.mdsestatus.Name = "mdsestatus";
            this.mdsestatus.Width = 125;
            // 
            // mdseamt
            // 
            this.mdseamt.HeaderText = "Amount";
            this.mdseamt.Name = "mdseamt";
            this.mdseamt.Width = 70;
            // 
            // mdseReleasedate
            // 
            this.mdseReleasedate.HeaderText = "Release Date";
            this.mdseReleasedate.Name = "mdseReleasedate";
            this.mdseReleasedate.ReadOnly = true;
            this.mdseReleasedate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.mdseReleasedate.Visible = false;
            // 
            // mdse
            // 
            this.mdse.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
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
            // 
            // pfistate
            // 
            this.pfistate.HeaderText = "pfistate";
            this.pfistate.Name = "pfistate";
            this.pfistate.ReadOnly = true;
            this.pfistate.Visible = false;
            this.pfistate.Width = 20;
            // 
            // releasedate
            // 
            this.releasedate.HeaderText = "Eligible for Release";
            this.releasedate.Name = "releasedate";
            this.releasedate.ReadOnly = true;
            this.releasedate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.releasedate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.releasedate.Width = 150;
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
            this.holddate.HeaderText = "Holddate";
            this.holddate.Name = "holddate";
            this.holddate.ReadOnly = true;
            this.holddate.Visible = false;
            // 
            // holdby
            // 
            this.holdby.HeaderText = "holdby";
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
            // orgticketnumber
            // 
            this.orgticketnumber.HeaderText = "OrigTicketNumber";
            this.orgticketnumber.Name = "orgticketnumber";
            this.orgticketnumber.Visible = false;
            // 
            // PoliceHoldReleaseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(860, 428);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridViewMdse);
            this.Controls.Add(this.labelMdseUnavailableMsg);
            this.Controls.Add(this.buttonDeselectAll);
            this.Controls.Add(this.labelPageNo);
            this.Controls.Add(this.buttonClaimantRelease);
            this.Controls.Add(this.labelReleaseDateUpdate);
            this.Controls.Add(this.buttonLast);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.buttonFirst);
            this.Controls.Add(this.buttonReleaseHold);
            this.Controls.Add(this.buttonUpdateReleaseDate);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridViewTransactions);
            this.Controls.Add(this.labelSubHeading);
            this.Controls.Add(this.labelMainHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PoliceHoldReleaseList";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PoliceHoldReleaseList";
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
        private System.Windows.Forms.Button buttonUpdateReleaseDate;
        private System.Windows.Forms.Button buttonReleaseHold;
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
        private System.Windows.Forms.Button buttonClaimantRelease;
        private System.Windows.Forms.Label labelPageNo;
        private CustomButton buttonDeselectAll;
        private System.Windows.Forms.Label labelMdseUnavailableMsg;
        private CustomDataGridView dataGridViewMdse;
        private System.Windows.Forms.DataGridViewCheckBoxColumn mdseselect;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn icn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mdsestatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn mdseamt;
        private CalendarColumn mdseReleasedate;
        private System.Windows.Forms.DataGridViewImageColumn mdse;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
        private System.Windows.Forms.DataGridViewTextBoxColumn trandate;
        private System.Windows.Forms.DataGridViewTextBoxColumn trantype;
        private System.Windows.Forms.DataGridViewTextBoxColumn trannumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn pfistate;
        private CalendarColumn releasedate;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn holddate;
        private System.Windows.Forms.DataGridViewTextBoxColumn holdby;
        private System.Windows.Forms.DataGridViewTextBoxColumn holdcomment;
        private System.Windows.Forms.DataGridViewTextBoxColumn orgticketnumber;
    }
}