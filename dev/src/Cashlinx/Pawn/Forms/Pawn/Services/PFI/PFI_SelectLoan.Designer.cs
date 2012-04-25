using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.Services.PFI
{
    partial class PFI_SelectLoan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label3 = new System.Windows.Forms.Label();
            this.asOfLabel = new System.Windows.Forms.Label();
            this.totalPanel = new System.Windows.Forms.Panel();
            this.totalSelectedPfiLabel = new System.Windows.Forms.Label();
            this.totalEligiblePfiLabel = new System.Windows.Forms.Label();
            this.totalSelectedPfiLabelLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateFindButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.gvLoans = new System.Windows.Forms.DataGridView();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.deselectAllButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateCalendarSearchDate = new DateCalendar();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colPfiDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMDSE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaidIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 136;
            //Madhu 11/18/2010 fix for defect PWNU00001443
            this.label3.Text = "Select Tickets for PFI ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // asOfLabel
            // 
            this.asOfLabel.AutoSize = true;
            this.asOfLabel.BackColor = System.Drawing.Color.Transparent;
            this.asOfLabel.ForeColor = System.Drawing.Color.White;
            this.asOfLabel.Location = new System.Drawing.Point(123, 29);
            this.asOfLabel.Name = "asOfLabel";
            this.asOfLabel.Size = new System.Drawing.Size(92, 13);
            this.asOfLabel.TabIndex = 137;
            this.asOfLabel.Text = "As of 00/00/0000";
            this.asOfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totalPanel
            // 
            this.totalPanel.BackColor = System.Drawing.Color.Transparent;
            this.totalPanel.Controls.Add(this.totalSelectedPfiLabel);
            this.totalPanel.Controls.Add(this.totalEligiblePfiLabel);
            this.totalPanel.Controls.Add(this.totalSelectedPfiLabelLabel);
            this.totalPanel.Controls.Add(this.label1);
            this.totalPanel.Location = new System.Drawing.Point(612, 12);
            this.totalPanel.Name = "totalPanel";
            this.totalPanel.Size = new System.Drawing.Size(158, 43);
            this.totalPanel.TabIndex = 138;
            // 
            // totalSelectedPfiLabel
            // 
            this.totalSelectedPfiLabel.AutoSize = true;
            this.totalSelectedPfiLabel.BackColor = System.Drawing.Color.Transparent;
            this.totalSelectedPfiLabel.ForeColor = System.Drawing.Color.White;
            this.totalSelectedPfiLabel.Location = new System.Drawing.Point(119, 25);
            this.totalSelectedPfiLabel.Name = "totalSelectedPfiLabel";
            this.totalSelectedPfiLabel.Size = new System.Drawing.Size(19, 13);
            this.totalSelectedPfiLabel.TabIndex = 141;
            this.totalSelectedPfiLabel.Text = "00";
            this.totalSelectedPfiLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totalEligiblePfiLabel
            // 
            this.totalEligiblePfiLabel.AutoSize = true;
            this.totalEligiblePfiLabel.BackColor = System.Drawing.Color.Transparent;
            this.totalEligiblePfiLabel.ForeColor = System.Drawing.Color.White;
            this.totalEligiblePfiLabel.Location = new System.Drawing.Point(119, 3);
            this.totalEligiblePfiLabel.Name = "totalEligiblePfiLabel";
            this.totalEligiblePfiLabel.Size = new System.Drawing.Size(19, 13);
            this.totalEligiblePfiLabel.TabIndex = 139;
            this.totalEligiblePfiLabel.Text = "00";
            this.totalEligiblePfiLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totalSelectedPfiLabelLabel
            // 
            this.totalSelectedPfiLabelLabel.AutoSize = true;
            this.totalSelectedPfiLabelLabel.BackColor = System.Drawing.Color.Transparent;
            this.totalSelectedPfiLabelLabel.ForeColor = System.Drawing.Color.White;
            this.totalSelectedPfiLabelLabel.Location = new System.Drawing.Point(3, 25);
            this.totalSelectedPfiLabelLabel.Name = "totalSelectedPfiLabelLabel";
            this.totalSelectedPfiLabelLabel.Size = new System.Drawing.Size(119, 13);
            this.totalSelectedPfiLabelLabel.TabIndex = 140;
            this.totalSelectedPfiLabelLabel.Text = "Total Selected for PFI:  ";
            this.totalSelectedPfiLabelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 139;
            this.label1.Text = "Total Eligible for PFI:  ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(12, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 139;
            this.label4.Text = "Eligible for PFI As Of: ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateFindButton
            // 
            this.dateFindButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dateFindButton.AutoSize = true;
            this.dateFindButton.BackColor = System.Drawing.Color.Transparent;
            this.dateFindButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.dateFindButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.dateFindButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.dateFindButton.FlatAppearance.BorderSize = 0;
            this.dateFindButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.dateFindButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.dateFindButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dateFindButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateFindButton.ForeColor = System.Drawing.Color.White;
            this.dateFindButton.Location = new System.Drawing.Point(257, 71);
            this.dateFindButton.Name = "dateFindButton";
            this.dateFindButton.Size = new System.Drawing.Size(41, 23);
            this.dateFindButton.TabIndex = 142;
            this.dateFindButton.Text = "Find";
            this.dateFindButton.UseVisualStyleBackColor = false;
            this.dateFindButton.Click += new System.EventHandler(this.dateFindButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cancelButton.AutoSize = true;
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(15, 472);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(95, 40);
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
            this.continueButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.continueButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.continueButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.continueButton.FlatAppearance.BorderSize = 0;
            this.continueButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueButton.ForeColor = System.Drawing.Color.White;
            this.continueButton.Location = new System.Drawing.Point(664, 475);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(95, 40);
            this.continueButton.TabIndex = 144;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = false;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // printButton
            // 
            this.printButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.printButton.AutoSize = true;
            this.printButton.BackColor = System.Drawing.Color.Transparent;
            this.printButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.printButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.printButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.printButton.FlatAppearance.BorderSize = 0;
            this.printButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.printButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.printButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printButton.ForeColor = System.Drawing.Color.White;
            this.printButton.Location = new System.Drawing.Point(566, 475);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(95, 40);
            this.printButton.TabIndex = 145;
            this.printButton.Text = "Print";
            this.printButton.UseVisualStyleBackColor = false;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // backButton
            // 
            this.backButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.backButton.AutoSize = true;
            this.backButton.BackColor = System.Drawing.Color.Transparent;
            this.backButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.backButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.backButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.backButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.ForeColor = System.Drawing.Color.White;
            this.backButton.Location = new System.Drawing.Point(116, 472);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(95, 40);
            this.backButton.TabIndex = 146;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // gvLoans
            // 
            this.gvLoans.AllowUserToAddRows = false;
            this.gvLoans.AllowUserToDeleteRows = false;
            this.gvLoans.AllowUserToResizeColumns = false;
            this.gvLoans.AllowUserToResizeRows = false;
            this.gvLoans.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvLoans.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.gvLoans.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvLoans.BackgroundColor = System.Drawing.Color.White;
            this.gvLoans.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gvLoans.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvLoans.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvLoans.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvLoans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvLoans.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect,
            this.colPfiDate,
            this.colType,
            this.colNumber,
            this.colCustomerName,
            this.colMDSE,
            this.colAmount,
            this.colPaidIn});
            this.gvLoans.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvLoans.DefaultCellStyle = dataGridViewCellStyle9;
            this.gvLoans.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvLoans.GridColor = System.Drawing.Color.LightGray;
            this.gvLoans.Location = new System.Drawing.Point(15, 113);
            this.gvLoans.MultiSelect = false;
            this.gvLoans.Name = "gvLoans";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvLoans.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.gvLoans.RowHeadersVisible = false;
            this.gvLoans.RowHeadersWidth = 20;
            this.gvLoans.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvLoans.RowsDefaultCellStyle = dataGridViewCellStyle11;
            this.gvLoans.RowTemplate.Height = 25;
            this.gvLoans.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvLoans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvLoans.Size = new System.Drawing.Size(744, 300);
            this.gvLoans.TabIndex = 147;
            this.gvLoans.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvLoans_CellValueChanged);
            this.gvLoans.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvLoans_CellContentClick);
            // 
            // selectAllButton
            // 
            this.selectAllButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.selectAllButton.AutoSize = true;
            this.selectAllButton.BackColor = System.Drawing.Color.Transparent;
            this.selectAllButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.selectAllButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.selectAllButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.selectAllButton.FlatAppearance.BorderSize = 0;
            this.selectAllButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.selectAllButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.selectAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectAllButton.ForeColor = System.Drawing.Color.White;
            this.selectAllButton.Location = new System.Drawing.Point(15, 424);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(95, 33);
            this.selectAllButton.TabIndex = 148;
            this.selectAllButton.Text = "Select All";
            this.selectAllButton.UseVisualStyleBackColor = false;
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // deselectAllButton
            // 
            this.deselectAllButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.deselectAllButton.AutoSize = true;
            this.deselectAllButton.BackColor = System.Drawing.Color.Transparent;
            this.deselectAllButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.deselectAllButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.deselectAllButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.deselectAllButton.FlatAppearance.BorderSize = 0;
            this.deselectAllButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.deselectAllButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.deselectAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deselectAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deselectAllButton.ForeColor = System.Drawing.Color.White;
            this.deselectAllButton.Location = new System.Drawing.Point(116, 424);
            this.deselectAllButton.Name = "deselectAllButton";
            this.deselectAllButton.Size = new System.Drawing.Size(95, 33);
            this.deselectAllButton.TabIndex = 149;
            this.deselectAllButton.Text = "Deselect All";
            this.deselectAllButton.UseVisualStyleBackColor = false;
            this.deselectAllButton.Click += new System.EventHandler(this.deselectAllButton_Click);
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
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "colPfiDate";
            this.dataGridViewTextBoxColumn1.HeaderText = "PFI Date";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "colType";
            this.dataGridViewTextBoxColumn2.HeaderText = "Type";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Number";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "Customer Name";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.HeaderText = "MDSE";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.HeaderText = "Paid In";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dateCalendarSearchDate
            // 
            this.dateCalendarSearchDate.AllowKeyUpAndDown = false;
            this.dateCalendarSearchDate.AllowMonthlySelection = false;
            this.dateCalendarSearchDate.AllowWeekends = true;
            this.dateCalendarSearchDate.AutoSize = true;
            this.dateCalendarSearchDate.BackColor = System.Drawing.Color.Transparent;
            this.dateCalendarSearchDate.Location = new System.Drawing.Point(116, 71);
            this.dateCalendarSearchDate.Name = "dateCalendarSearchDate";
            this.dateCalendarSearchDate.SelectedDate = "10/12/2009";
            this.dateCalendarSearchDate.Size = new System.Drawing.Size(135, 26);
            this.dateCalendarSearchDate.TabIndex = 151;
            // 
            // colSelect
            // 
            this.colSelect.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colSelect.DataPropertyName = "colSelect";
            this.colSelect.HeaderText = "Select";
            this.colSelect.MinimumWidth = 80;
            this.colSelect.Name = "colSelect";
            this.colSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSelect.ToolTipText = "Click button to Edit Item.";
            this.colSelect.Width = 80;
            // 
            // colPfiDate
            // 
            this.colPfiDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colPfiDate.DataPropertyName = "colPfiDate";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.colPfiDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.colPfiDate.HeaderText = "PFI Date";
            this.colPfiDate.MinimumWidth = 80;
            this.colPfiDate.Name = "colPfiDate";
            this.colPfiDate.ReadOnly = true;
            this.colPfiDate.Width = 80;
            // 
            // colType
            // 
            this.colType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colType.DataPropertyName = "colType";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.colType.DefaultCellStyle = dataGridViewCellStyle3;
            this.colType.HeaderText = "Type";
            this.colType.MinimumWidth = 80;
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.Width = 80;
            // 
            // colNumber
            // 
            this.colNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.colNumber.DefaultCellStyle = dataGridViewCellStyle4;
            this.colNumber.HeaderText = "Number";
            this.colNumber.MinimumWidth = 80;
            this.colNumber.Name = "colNumber";
            this.colNumber.ReadOnly = true;
            this.colNumber.Width = 80;
            // 
            // colCustomerName
            // 
            this.colCustomerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.colCustomerName.DefaultCellStyle = dataGridViewCellStyle5;
            this.colCustomerName.HeaderText = "Customer Name";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.ReadOnly = true;
            // 
            // colMDSE
            // 
            this.colMDSE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.colMDSE.DefaultCellStyle = dataGridViewCellStyle6;
            this.colMDSE.HeaderText = "MDSE";
            this.colMDSE.Name = "colMDSE";
            this.colMDSE.ReadOnly = true;
            // 
            // colAmount
            // 
            this.colAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colAmount.DefaultCellStyle = dataGridViewCellStyle7;
            this.colAmount.HeaderText = "Amount";
            this.colAmount.MinimumWidth = 90;
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            this.colAmount.Width = 90;
            // 
            // colPaidIn
            // 
            this.colPaidIn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.colPaidIn.DefaultCellStyle = dataGridViewCellStyle8;
            this.colPaidIn.HeaderText = "Paid In";
            this.colPaidIn.MinimumWidth = 90;
            this.colPaidIn.Name = "colPaidIn";
            this.colPaidIn.ReadOnly = true;
            this.colPaidIn.Width = 90;
            // 
            // PFI_SelectLoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_512_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(782, 522);
            this.ControlBox = false;
            this.Controls.Add(this.dateCalendarSearchDate);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.deselectAllButton);
            this.Controls.Add(this.selectAllButton);
            this.Controls.Add(this.gvLoans);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.dateFindButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.totalPanel);
            this.Controls.Add(this.asOfLabel);
            this.Controls.Add(this.label3);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PFI_SelectLoan";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.PFI_SelectLoan_Load);
            this.totalPanel.ResumeLayout(false);
            this.totalPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label asOfLabel;
        private System.Windows.Forms.Panel totalPanel;
        private System.Windows.Forms.Label totalSelectedPfiLabelLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label totalSelectedPfiLabel;
        private System.Windows.Forms.Label totalEligiblePfiLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button dateFindButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.DataGridView gvLoans;
        private System.Windows.Forms.Button selectAllButton;
        private System.Windows.Forms.Button deselectAllButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DateCalendar dateCalendarSearchDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPfiDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMDSE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaidIn;
    }
}
