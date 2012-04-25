using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Customer.ItemRelease
{
    partial class CustomerHoldReleaseInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerHoldReleaseInfo));
            this.labelHeading = new System.Windows.Forms.Label();
            this.labelReason = new System.Windows.Forms.Label();
            this.richTextBoxReason = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelReleaseCommentHeading = new System.Windows.Forms.Label();
            this.customDataGridViewSelectedTransactions = new CustomDataGridView();
            this.ticketnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.holddate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.releasedate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.holdreason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customButtonSubmit = new CustomButton();
            this.customButtonBack = new CustomButton();
            this.customButtonCancel = new CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewSelectedTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelHeading.Location = new System.Drawing.Point(13, 22);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(157, 16);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Release Customer Hold";
            // 
            // labelReason
            // 
            this.labelReason.AutoSize = true;
            this.labelReason.BackColor = System.Drawing.Color.Transparent;
            this.labelReason.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReason.Location = new System.Drawing.Point(16, 93);
            this.labelReason.Name = "labelReason";
            this.labelReason.Size = new System.Drawing.Size(148, 13);
            this.labelReason.TabIndex = 3;
            this.labelReason.Text = "Selected Customer Holds";
            // 
            // richTextBoxReason
            // 
            this.richTextBoxReason.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxReason.Location = new System.Drawing.Point(19, 294);
            this.richTextBoxReason.MaxLength = 256;
            this.richTextBoxReason.Name = "richTextBoxReason";
            this.richTextBoxReason.Size = new System.Drawing.Size(652, 82);
            this.richTextBoxReason.TabIndex = 4;
            this.richTextBoxReason.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.DarkBlue;
            this.groupBox2.Location = new System.Drawing.Point(3, 416);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(752, 2);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // labelReleaseCommentHeading
            // 
            this.labelReleaseCommentHeading.AutoSize = true;
            this.labelReleaseCommentHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelReleaseCommentHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReleaseCommentHeading.Location = new System.Drawing.Point(19, 263);
            this.labelReleaseCommentHeading.Name = "labelReleaseCommentHeading";
            this.labelReleaseCommentHeading.Size = new System.Drawing.Size(144, 13);
            this.labelReleaseCommentHeading.TabIndex = 57;
            this.labelReleaseCommentHeading.Text = "Hold Release Comments";
            // 
            // customDataGridViewSelectedTransactions
            // 
            this.customDataGridViewSelectedTransactions.AllowUserToAddRows = false;
            this.customDataGridViewSelectedTransactions.AllowUserToDeleteRows = false;
            this.customDataGridViewSelectedTransactions.AllowUserToResizeColumns = false;
            this.customDataGridViewSelectedTransactions.AllowUserToResizeRows = false;
            this.customDataGridViewSelectedTransactions.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.customDataGridViewSelectedTransactions.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewSelectedTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataGridViewSelectedTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridViewSelectedTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ticketnumber,
            this.holddate,
            this.releasedate,
            this.userid,
            this.holdreason});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewSelectedTransactions.DefaultCellStyle = dataGridViewCellStyle2;
            this.customDataGridViewSelectedTransactions.GridColor = System.Drawing.Color.LightGray;
            this.customDataGridViewSelectedTransactions.Location = new System.Drawing.Point(19, 120);
            this.customDataGridViewSelectedTransactions.Margin = new System.Windows.Forms.Padding(0);
            this.customDataGridViewSelectedTransactions.Name = "customDataGridViewSelectedTransactions";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridViewSelectedTransactions.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.customDataGridViewSelectedTransactions.RowHeadersVisible = false;
            this.customDataGridViewSelectedTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.customDataGridViewSelectedTransactions.Size = new System.Drawing.Size(731, 110);
            this.customDataGridViewSelectedTransactions.TabIndex = 62;
            this.customDataGridViewSelectedTransactions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewSelectedTransactions_CellFormatting);
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
            // releasedate
            // 
            this.releasedate.HeaderText = "Eligible For Release";
            this.releasedate.Name = "releasedate";
            this.releasedate.ReadOnly = true;
            this.releasedate.Width = 120;
            // 
            // userid
            // 
            this.userid.HeaderText = "User Id";
            this.userid.Name = "userid";
            this.userid.ReadOnly = true;
            // 
            // holdreason
            // 
            this.holdreason.HeaderText = "Reason For Hold";
            this.holdreason.Name = "holdreason";
            this.holdreason.ReadOnly = true;
            this.holdreason.Width = 280;
            // 
            // customButtonSubmit
            // 
            this.customButtonSubmit.BackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonSubmit.BackgroundImage")));
            this.customButtonSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonSubmit.FlatAppearance.BorderSize = 0;
            this.customButtonSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonSubmit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonSubmit.ForeColor = System.Drawing.Color.White;
            this.customButtonSubmit.Location = new System.Drawing.Point(600, 425);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 61;
            this.customButtonSubmit.Text = "&Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // customButtonBack
            // 
            this.customButtonBack.BackColor = System.Drawing.Color.Transparent;
            this.customButtonBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonBack.BackgroundImage")));
            this.customButtonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonBack.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonBack.FlatAppearance.BorderSize = 0;
            this.customButtonBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonBack.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonBack.ForeColor = System.Drawing.Color.White;
            this.customButtonBack.Location = new System.Drawing.Point(130, 425);
            this.customButtonBack.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonBack.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.Name = "customButtonBack";
            this.customButtonBack.Size = new System.Drawing.Size(100, 50);
            this.customButtonBack.TabIndex = 60;
            this.customButtonBack.Text = "&Back";
            this.customButtonBack.UseVisualStyleBackColor = false;
            this.customButtonBack.Click += new System.EventHandler(this.buttonBack_Click);
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
            this.customButtonCancel.Location = new System.Drawing.Point(16, 425);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 59;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // CustomerHoldReleaseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_400_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(762, 495);
            this.ControlBox = false;
            this.Controls.Add(this.customDataGridViewSelectedTransactions);
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonBack);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.labelReleaseCommentHeading);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.richTextBoxReason);
            this.Controls.Add(this.labelReason);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomerHoldReleaseInfo";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomerHoldReleaseInfo";
            this.Load += new System.EventHandler(this.CustomerHoldInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewSelectedTransactions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label labelReason;
        private System.Windows.Forms.RichTextBox richTextBoxReason;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelReleaseCommentHeading;
        private CustomButton customButtonCancel;
        private CustomButton customButtonBack;
        private CustomButton customButtonSubmit;
        private CustomDataGridView customDataGridViewSelectedTransactions;
        private System.Windows.Forms.DataGridViewTextBoxColumn ticketnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn holddate;
        private System.Windows.Forms.DataGridViewTextBoxColumn releasedate;
        private System.Windows.Forms.DataGridViewTextBoxColumn userid;
        private System.Windows.Forms.DataGridViewTextBoxColumn holdreason;
    }
}