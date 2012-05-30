namespace Support.Forms.Customer.Products
{
    partial class ExtendedDepositDate
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblHeaderInfoLine1 = new System.Windows.Forms.Label();
            this.lblDepositDate = new System.Windows.Forms.Label();
            this.tbDepositDate = new System.Windows.Forms.TextBox();
            this.cbReasonCode = new System.Windows.Forms.ComboBox();
            this.dtpExtendedDate = new System.Windows.Forms.DateTimePicker();
            this.lblExtendedDate = new System.Windows.Forms.Label();
            this.lblReasonCode = new System.Windows.Forms.Label();
            this.lblValidExtendedDate = new System.Windows.Forms.Label();
            this.btnSubmit = new Support.Libraries.Forms.Components.SupportButton();
            this.btnCancel = new Support.Libraries.Forms.Components.SupportButton();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblHeader.Location = new System.Drawing.Point(213, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(228, 23);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Extended Deposit Date";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblHeaderInfoLine1
            // 
            this.lblHeaderInfoLine1.AutoSize = true;
            this.lblHeaderInfoLine1.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderInfoLine1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderInfoLine1.ForeColor = System.Drawing.Color.Firebrick;
            this.lblHeaderInfoLine1.Location = new System.Drawing.Point(101, 70);
            this.lblHeaderInfoLine1.Name = "lblHeaderInfoLine1";
            this.lblHeaderInfoLine1.Size = new System.Drawing.Size(214, 16);
            this.lblHeaderInfoLine1.TabIndex = 1;
            this.lblHeaderInfoLine1.Text = "The Maximum extension date is:";
            this.lblHeaderInfoLine1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDepositDate
            // 
            this.lblDepositDate.AutoSize = true;
            this.lblDepositDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDepositDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepositDate.ForeColor = System.Drawing.Color.Black;
            this.lblDepositDate.Location = new System.Drawing.Point(165, 108);
            this.lblDepositDate.Name = "lblDepositDate";
            this.lblDepositDate.Size = new System.Drawing.Size(97, 16);
            this.lblDepositDate.TabIndex = 2;
            this.lblDepositDate.Text = "Deposit Date:";
            // 
            // tbDepositDate
            // 
            this.tbDepositDate.Enabled = false;
            this.tbDepositDate.Location = new System.Drawing.Point(269, 104);
            this.tbDepositDate.Name = "tbDepositDate";
            this.tbDepositDate.Size = new System.Drawing.Size(100, 22);
            this.tbDepositDate.TabIndex = 5;
            // 
            // cbReasonCode
            // 
            this.cbReasonCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReasonCode.FormattingEnabled = true;
            this.cbReasonCode.Location = new System.Drawing.Point(268, 205);
            this.cbReasonCode.Name = "cbReasonCode";
            this.cbReasonCode.Size = new System.Drawing.Size(201, 22);
            this.cbReasonCode.TabIndex = 6;
            // 
            // dtpExtendedDate
            // 
            this.dtpExtendedDate.CustomFormat = "ddd, MMM dd, yyyy";
            this.dtpExtendedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExtendedDate.Location = new System.Drawing.Point(269, 154);
            this.dtpExtendedDate.MaximumSize = new System.Drawing.Size(155, 22);
            this.dtpExtendedDate.Name = "dtpExtendedDate";
            this.dtpExtendedDate.Size = new System.Drawing.Size(155, 22);
            this.dtpExtendedDate.TabIndex = 7;
            // 
            // lblExtendedDate
            // 
            this.lblExtendedDate.AutoSize = true;
            this.lblExtendedDate.BackColor = System.Drawing.Color.Transparent;
            this.lblExtendedDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExtendedDate.ForeColor = System.Drawing.Color.Black;
            this.lblExtendedDate.Location = new System.Drawing.Point(101, 156);
            this.lblExtendedDate.Name = "lblExtendedDate";
            this.lblExtendedDate.Size = new System.Drawing.Size(161, 16);
            this.lblExtendedDate.TabIndex = 11;
            this.lblExtendedDate.Text = "Extended Deposit Date:";
            // 
            // lblReasonCode
            // 
            this.lblReasonCode.AutoSize = true;
            this.lblReasonCode.BackColor = System.Drawing.Color.Transparent;
            this.lblReasonCode.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReasonCode.ForeColor = System.Drawing.Color.Black;
            this.lblReasonCode.Location = new System.Drawing.Point(201, 207);
            this.lblReasonCode.Name = "lblReasonCode";
            this.lblReasonCode.Size = new System.Drawing.Size(61, 16);
            this.lblReasonCode.TabIndex = 12;
            this.lblReasonCode.Text = "Reason:";
            // 
            // lblValidExtendedDate
            // 
            this.lblValidExtendedDate.AutoSize = true;
            this.lblValidExtendedDate.BackColor = System.Drawing.Color.Transparent;
            this.lblValidExtendedDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValidExtendedDate.ForeColor = System.Drawing.Color.Black;
            this.lblValidExtendedDate.Location = new System.Drawing.Point(320, 70);
            this.lblValidExtendedDate.Name = "lblValidExtendedDate";
            this.lblValidExtendedDate.Size = new System.Drawing.Size(78, 16);
            this.lblValidExtendedDate.TabIndex = 15;
            this.lblValidExtendedDate.Text = "mm/dd/yy";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.Transparent;
            this.btnSubmit.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSubmit.FlatAppearance.BorderSize = 0;
            this.btnSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(505, 259);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.btnSubmit.MaximumSize = new System.Drawing.Size(90, 40);
            this.btnSubmit.MinimumSize = new System.Drawing.Size(90, 40);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(90, 40);
            this.btnSubmit.TabIndex = 13;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(32, 259);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.MaximumSize = new System.Drawing.Size(90, 40);
            this.btnCancel.MinimumSize = new System.Drawing.Size(90, 40);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 40);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ExtendedDepositDate
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackgroundImage = global::Support.Properties.Resources.form_800_700;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(622, 317);
            this.Controls.Add(this.lblValidExtendedDate);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.lblReasonCode);
            this.Controls.Add(this.lblExtendedDate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dtpExtendedDate);
            this.Controls.Add(this.cbReasonCode);
            this.Controls.Add(this.tbDepositDate);
            this.Controls.Add(this.lblDepositDate);
            this.Controls.Add(this.lblHeaderInfoLine1);
            this.Controls.Add(this.lblHeader);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtendedDepositDate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExtendedDepositDate";
            this.Load += new System.EventHandler(this.ExtendedDepositDate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblHeaderInfoLine1;
        private System.Windows.Forms.Label lblDepositDate;
        private System.Windows.Forms.TextBox tbDepositDate;
        private System.Windows.Forms.ComboBox cbReasonCode;
        private System.Windows.Forms.DateTimePicker dtpExtendedDate;
        private Libraries.Forms.Components.SupportButton btnCancel;
        private System.Windows.Forms.Label lblExtendedDate;
        private System.Windows.Forms.Label lblReasonCode;
        private Libraries.Forms.Components.SupportButton btnSubmit;
        private System.Windows.Forms.Label lblValidExtendedDate;
    }
}