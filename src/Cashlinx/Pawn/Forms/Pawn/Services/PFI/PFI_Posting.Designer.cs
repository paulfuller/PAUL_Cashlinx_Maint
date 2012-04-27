namespace Pawn.Forms.Pawn.Services.PFI
{
    partial class PFI_Posting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label3 = new System.Windows.Forms.Label();
            this.asOfLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.postButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.chargeOffListButton = new System.Windows.Forms.Button();
            this.gvPostings = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.refurbButton = new System.Windows.Forms.Button();
            this.totalPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.totalCostLabel = new System.Windows.Forms.Label();
            this.totalTicketsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRefurb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAssignmentType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRetail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvPostings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.totalPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(350, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 19);
            this.label3.TabIndex = 136;
            this.label3.Text = "PFI Posting List ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // asOfLabel
            // 
            this.asOfLabel.AutoSize = true;
            this.asOfLabel.BackColor = System.Drawing.Color.Transparent;
            this.asOfLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asOfLabel.ForeColor = System.Drawing.Color.White;
            this.asOfLabel.Location = new System.Drawing.Point(479, 44);
            this.asOfLabel.Name = "asOfLabel";
            this.asOfLabel.Size = new System.Drawing.Size(91, 13);
            this.asOfLabel.TabIndex = 137;
            this.asOfLabel.Text = "As of 00/00/0000";
            this.asOfLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.cancelButton.Location = new System.Drawing.Point(13, 596);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(110, 40);
            this.cancelButton.TabIndex = 143;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // postButton
            // 
            this.postButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.postButton.AutoSize = true;
            this.postButton.BackColor = System.Drawing.Color.Transparent;
            this.postButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.postButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.postButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.postButton.FlatAppearance.BorderSize = 0;
            this.postButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.postButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.postButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.postButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.postButton.ForeColor = System.Drawing.Color.White;
            this.postButton.Location = new System.Drawing.Point(703, 596);
            this.postButton.Name = "postButton";
            this.postButton.Size = new System.Drawing.Size(110, 40);
            this.postButton.TabIndex = 144;
            this.postButton.Text = "Post";
            this.postButton.UseVisualStyleBackColor = false;
            this.postButton.Click += new System.EventHandler(this.postButton_Click);
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
            this.printButton.Location = new System.Drawing.Point(573, 596);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(110, 40);
            this.printButton.TabIndex = 145;
            this.printButton.Text = "Print";
            this.printButton.UseVisualStyleBackColor = false;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // chargeOffListButton
            // 
            this.chargeOffListButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chargeOffListButton.AutoSize = true;
            this.chargeOffListButton.BackColor = System.Drawing.Color.Transparent;
            this.chargeOffListButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.chargeOffListButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.chargeOffListButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.chargeOffListButton.FlatAppearance.BorderSize = 0;
            this.chargeOffListButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.chargeOffListButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.chargeOffListButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chargeOffListButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chargeOffListButton.ForeColor = System.Drawing.Color.White;
            this.chargeOffListButton.Location = new System.Drawing.Point(142, 596);
            this.chargeOffListButton.Name = "chargeOffListButton";
            this.chargeOffListButton.Size = new System.Drawing.Size(110, 40);
            this.chargeOffListButton.TabIndex = 146;
            this.chargeOffListButton.Text = "Charge Off List";
            this.chargeOffListButton.UseVisualStyleBackColor = false;
            this.chargeOffListButton.Click += new System.EventHandler(this.chargeOffListButton_Click);
            // 
            // gvPostings
            // 
            this.gvPostings.AllowUserToAddRows = false;
            this.gvPostings.AllowUserToDeleteRows = false;
            this.gvPostings.AllowUserToResizeColumns = false;
            this.gvPostings.AllowUserToResizeRows = false;
            this.gvPostings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvPostings.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvPostings.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.gvPostings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvPostings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gvPostings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPostings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRefurb,
            this.colAssignmentType,
            this.colNumber,
            this.colDescription,
            this.colCost,
            this.colRetail,
            this.colReason});
            this.gvPostings.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvPostings.DefaultCellStyle = dataGridViewCellStyle6;
            this.gvPostings.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvPostings.GridColor = System.Drawing.Color.LightGray;
            this.gvPostings.Location = new System.Drawing.Point(11, 82);
            this.gvPostings.MultiSelect = false;
            this.gvPostings.Name = "gvPostings";
            this.gvPostings.RowHeadersVisible = false;
            this.gvPostings.RowHeadersWidth = 20;
            this.gvPostings.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPostings.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.gvPostings.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPostings.RowTemplate.Height = 25;
            this.gvPostings.RowTemplate.ReadOnly = true;
            this.gvPostings.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPostings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvPostings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPostings.Size = new System.Drawing.Size(800, 453);
            this.gvPostings.TabIndex = 147;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Pawn.Properties.Resources.blue_border;
            this.pictureBox1.Location = new System.Drawing.Point(6, 590);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(810, 3);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 150;
            this.pictureBox1.TabStop = false;
            // 
            // refurbButton
            // 
            this.refurbButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.refurbButton.AutoSize = true;
            this.refurbButton.BackColor = System.Drawing.Color.Transparent;
            this.refurbButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.refurbButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.refurbButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.refurbButton.FlatAppearance.BorderSize = 0;
            this.refurbButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.refurbButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.refurbButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refurbButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refurbButton.ForeColor = System.Drawing.Color.White;
            this.refurbButton.Location = new System.Drawing.Point(271, 596);
            this.refurbButton.Name = "refurbButton";
            this.refurbButton.Size = new System.Drawing.Size(110, 40);
            this.refurbButton.TabIndex = 151;
            this.refurbButton.Text = "Refurb";
            this.refurbButton.UseVisualStyleBackColor = false;
            this.refurbButton.Click += new System.EventHandler(this.refurbButton_Click);
            // 
            // totalPanel
            // 
            this.totalPanel.BackColor = System.Drawing.Color.Transparent;
            this.totalPanel.Controls.Add(this.label4);
            this.totalPanel.Controls.Add(this.label2);
            this.totalPanel.Controls.Add(this.totalCostLabel);
            this.totalPanel.Controls.Add(this.totalTicketsLabel);
            this.totalPanel.Controls.Add(this.label1);
            this.totalPanel.Location = new System.Drawing.Point(633, 541);
            this.totalPanel.Name = "totalPanel";
            this.totalPanel.Size = new System.Drawing.Size(177, 43);
            this.totalPanel.TabIndex = 152;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(124, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 142;
            this.label4.Text = "Cost";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(50, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 141;
            this.label2.Text = "Loans/Buys";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // totalCostLabel
            // 
            this.totalCostLabel.BackColor = System.Drawing.Color.Transparent;
            this.totalCostLabel.Location = new System.Drawing.Point(115, 24);
            this.totalCostLabel.Name = "totalCostLabel";
            this.totalCostLabel.Size = new System.Drawing.Size(49, 13);
            this.totalCostLabel.TabIndex = 140;
            this.totalCostLabel.Text = "00.00";
            this.totalCostLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totalTicketsLabel
            // 
            this.totalTicketsLabel.AutoSize = true;
            this.totalTicketsLabel.BackColor = System.Drawing.Color.Transparent;
            this.totalTicketsLabel.Location = new System.Drawing.Point(57, 24);
            this.totalTicketsLabel.Name = "totalTicketsLabel";
            this.totalTicketsLabel.Size = new System.Drawing.Size(19, 13);
            this.totalTicketsLabel.TabIndex = 139;
            this.totalTicketsLabel.Text = "00";
            this.totalTicketsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 139;
            this.label1.Text = "Total: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.HeaderText = "Refurb#";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 70;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "colAssignmentType";
            this.dataGridViewTextBoxColumn2.HeaderText = "";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.HeaderText = "Loan#";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 75;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "colDescription";
            this.dataGridViewTextBoxColumn4.HeaderText = "Description";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "colTags";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn5.HeaderText = "#Tags";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 50;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "colCost";
            this.dataGridViewTextBoxColumn6.HeaderText = "Cost";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 75;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "colRetail";
            this.dataGridViewTextBoxColumn7.HeaderText = "Retail";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 75;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "colReason";
            this.dataGridViewTextBoxColumn8.HeaderText = "Reason";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 75;
            // 
            // colRefurb
            // 
            this.colRefurb.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colRefurb.DataPropertyName = "colRefurb";
            this.colRefurb.FillWeight = 70F;
            this.colRefurb.HeaderText = "Refurb#";
            this.colRefurb.Name = "colRefurb";
            this.colRefurb.ReadOnly = true;
            this.colRefurb.Width = 60;
            // 
            // colAssignmentType
            // 
            this.colAssignmentType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colAssignmentType.DataPropertyName = "colAssignmentType";
            this.colAssignmentType.FillWeight = 75F;
            this.colAssignmentType.HeaderText = "Type";
            this.colAssignmentType.Name = "colAssignmentType";
            this.colAssignmentType.ReadOnly = true;
            this.colAssignmentType.Width = 70;
            // 
            // colNumber
            // 
            this.colNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colNumber.DataPropertyName = "colNumber";
            this.colNumber.FillWeight = 75F;
            this.colNumber.HeaderText = "Ticket#";
            this.colNumber.Name = "colNumber";
            this.colNumber.ReadOnly = true;
            this.colNumber.Width = 70;
            // 
            // colDescription
            // 
            this.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colDescription.DataPropertyName = "colDescription";
            this.colDescription.FillWeight = 350F;
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            this.colDescription.Width = 350;
            // 
            // colCost
            // 
            this.colCost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colCost.DataPropertyName = "colCost";
            this.colCost.FillWeight = 50F;
            this.colCost.HeaderText = "Cost";
            this.colCost.Name = "colCost";
            this.colCost.ReadOnly = true;
            this.colCost.Width = 65;
            // 
            // colRetail
            // 
            this.colRetail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colRetail.DataPropertyName = "colRetail";
            this.colRetail.FillWeight = 50F;
            this.colRetail.HeaderText = "Retail";
            this.colRetail.Name = "colRetail";
            this.colRetail.ReadOnly = true;
            this.colRetail.Width = 65;
            // 
            // colReason
            // 
            this.colReason.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colReason.DataPropertyName = "colReason";
            this.colReason.FillWeight = 70F;
            this.colReason.HeaderText = "Reason";
            this.colReason.Name = "colReason";
            this.colReason.ReadOnly = true;
            this.colReason.Width = 65;
            // 
            // PFI_Posting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(822, 648);
            this.ControlBox = false;
            this.Controls.Add(this.totalPanel);
            this.Controls.Add(this.refurbButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.gvPostings);
            this.Controls.Add(this.chargeOffListButton);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.postButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.asOfLabel);
            this.Controls.Add(this.label3);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PFI_Posting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.PFI_Posting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvPostings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.totalPanel.ResumeLayout(false);
            this.totalPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label asOfLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button postButton;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Button chargeOffListButton;
        private System.Windows.Forms.DataGridView gvPostings;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button refurbButton;
        private System.Windows.Forms.Panel totalPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label totalCostLabel;
        private System.Windows.Forms.Label totalTicketsLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRefurb;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAssignmentType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReason;
    }
}