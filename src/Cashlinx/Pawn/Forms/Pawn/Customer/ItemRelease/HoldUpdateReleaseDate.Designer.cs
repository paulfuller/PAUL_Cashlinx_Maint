using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.Customer.ItemRelease
{
    partial class HoldUpdateReleaseDate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HoldUpdateReleaseDate));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelHeading = new System.Windows.Forms.Label();
            this.labelReleaseEligible = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelReason = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.pictureBoxCalendar = new System.Windows.Forms.PictureBox();
            this.dateRelease = new Date();
            this.dataGridViewSelectedTransactions = new System.Windows.Forms.DataGridView();
            this.ticketnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.holddate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.holdreason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCalendar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectedTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelHeading.Location = new System.Drawing.Point(13, 22);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(159, 16);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Update Release Date";
            // 
            // labelReleaseEligible
            // 
            this.labelReleaseEligible.AutoSize = true;
            this.labelReleaseEligible.BackColor = System.Drawing.Color.Transparent;
            this.labelReleaseEligible.Location = new System.Drawing.Point(16, 74);
            this.labelReleaseEligible.Name = "labelReleaseEligible";
            this.labelReleaseEligible.Size = new System.Drawing.Size(100, 13);
            this.labelReleaseEligible.TabIndex = 1;
            this.labelReleaseEligible.Text = "Eligible for Release:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(3, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(693, 2);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // labelReason
            // 
            this.labelReason.AutoSize = true;
            this.labelReason.BackColor = System.Drawing.Color.Transparent;
            this.labelReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReason.Location = new System.Drawing.Point(16, 106);
            this.labelReason.Name = "labelReason";
            this.labelReason.Size = new System.Drawing.Size(149, 13);
            this.labelReason.TabIndex = 3;
            this.labelReason.Text = "Selected Customer Holds";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.DarkBlue;
            this.groupBox2.Location = new System.Drawing.Point(3, 334);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(693, 2);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.buttonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.Location = new System.Drawing.Point(23, 343);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 50);
            this.buttonCancel.TabIndex = 51;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.Transparent;
            this.buttonBack.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.buttonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonBack.CausesValidation = false;
            this.buttonBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonBack.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.buttonBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.ForeColor = System.Drawing.Color.White;
            this.buttonBack.Location = new System.Drawing.Point(131, 343);
            this.buttonBack.Margin = new System.Windows.Forms.Padding(4);
            this.buttonBack.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonBack.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(100, 50);
            this.buttonBack.TabIndex = 52;
            this.buttonBack.Text = "&Back";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.BackColor = System.Drawing.Color.Transparent;
            this.buttonSubmit.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.buttonSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonSubmit.CausesValidation = false;
            this.buttonSubmit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonSubmit.FlatAppearance.BorderSize = 0;
            this.buttonSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSubmit.ForeColor = System.Drawing.Color.White;
            this.buttonSubmit.Location = new System.Drawing.Point(571, 343);
            this.buttonSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(100, 50);
            this.buttonSubmit.TabIndex = 53;
            this.buttonSubmit.Text = "&Submit";
            this.buttonSubmit.UseVisualStyleBackColor = false;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // pictureBoxCalendar
            // 
            this.pictureBoxCalendar.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxCalendar.Image")));
            this.pictureBoxCalendar.Location = new System.Drawing.Point(230, 71);
            this.pictureBoxCalendar.Name = "pictureBoxCalendar";
            this.pictureBoxCalendar.Size = new System.Drawing.Size(23, 20);
            this.pictureBoxCalendar.TabIndex = 55;
            this.pictureBoxCalendar.TabStop = false;
            this.pictureBoxCalendar.Click += new System.EventHandler(this.pictureBoxCalendar_Click);
            // 
            // dateRelease
            // 
            this.dateRelease.ErrorMessage = "";
            this.dateRelease.Location = new System.Drawing.Point(123, 71);
            this.dateRelease.Name = "dateRelease";
            this.dateRelease.Size = new System.Drawing.Size(100, 20);
            this.dateRelease.TabIndex = 54;
            this.dateRelease.ValidationExpression = "^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.](19|20)\\d\\d$";
            // 
            // dataGridViewSelectedTransactions
            // 
            this.dataGridViewSelectedTransactions.AllowUserToAddRows = false;
            this.dataGridViewSelectedTransactions.AllowUserToDeleteRows = false;
            this.dataGridViewSelectedTransactions.AllowUserToResizeRows = false;
            this.dataGridViewSelectedTransactions.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSelectedTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewSelectedTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSelectedTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                                                                                                                     this.ticketnumber,
                                                                                                                     this.holddate,
                                                                                                                     this.userid,
                                                                                                                     this.holdreason});
            this.dataGridViewSelectedTransactions.Location = new System.Drawing.Point(16, 143);
            this.dataGridViewSelectedTransactions.Name = "dataGridViewSelectedTransactions";
            this.dataGridViewSelectedTransactions.ReadOnly = true;
            this.dataGridViewSelectedTransactions.RowHeadersVisible = false;
            this.dataGridViewSelectedTransactions.Size = new System.Drawing.Size(675, 130);
            this.dataGridViewSelectedTransactions.TabIndex = 56;
            // 
            // ticketnumber
            // 
            this.ticketnumber.HeaderText = "Ticket Number";
            this.ticketnumber.Name = "ticketnumber";
            this.ticketnumber.ReadOnly = true;
            this.ticketnumber.Width = 120;
            // 
            // holddate
            // 
            this.holddate.HeaderText = "Hold Date";
            this.holddate.Name = "holddate";
            this.holddate.ReadOnly = true;
            // 
            // userid
            // 
            this.userid.HeaderText = "User Id";
            this.userid.Name = "userid";
            this.userid.ReadOnly = true;
            // 
            // holdreason
            // 
            this.holdreason.HeaderText = "Reason for Hold";
            this.holdreason.Name = "holdreason";
            this.holdreason.ReadOnly = true;
            this.holdreason.Width = 320;
            // 
            // HoldUpdateReleaseDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_400_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(703, 418);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridViewSelectedTransactions);
            this.Controls.Add(this.pictureBoxCalendar);
            this.Controls.Add(this.dateRelease);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.labelReason);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelReleaseEligible);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HoldUpdateReleaseDate";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HoldUpdateReleaseDate";
            this.Load += new System.EventHandler(this.CustomerHoldInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCalendar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectedTransactions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label labelReleaseEligible;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelReason;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonSubmit;
        private Date dateRelease;
        private System.Windows.Forms.PictureBox pictureBoxCalendar;
        private System.Windows.Forms.DataGridView dataGridViewSelectedTransactions;
        private System.Windows.Forms.DataGridViewTextBoxColumn ticketnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn holddate;
        private System.Windows.Forms.DataGridViewTextBoxColumn userid;
        private System.Windows.Forms.DataGridViewTextBoxColumn holdreason;
    }
}