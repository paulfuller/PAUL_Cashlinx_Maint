namespace Support.Forms.Customer
{
    partial class UpdateCustomerStatus
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
            this.BtnCancel = new Common.Libraries.Forms.Components.CustomButton();
            this.BtnSubmit = new Common.Libraries.Forms.Components.CustomButton();
            this.LblHeader = new Common.Libraries.Forms.Components.CustomLabel();
            this.LblCurrCustStatus = new Common.Libraries.Forms.Components.CustomLabel();
            this.TxBCurrCustStatus = new Common.Libraries.Forms.Components.CustomTextBox();
            this.LblChangeCustStatusTo = new Common.Libraries.Forms.Components.CustomLabel();
            this.LblCurrCustReasonCode = new Common.Libraries.Forms.Components.CustomLabel();
            this.LblChangeCustReasonTo = new Common.Libraries.Forms.Components.CustomLabel();
            this.TxBCurrCustReasonCode = new Common.Libraries.Forms.Components.CustomTextBox();
            this.cmbChangeCustStatusTo = new System.Windows.Forms.ComboBox();
            this.cmbChangeCustReasonTo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.Transparent;
            this.BtnCancel.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.BtnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BtnCancel.FlatAppearance.BorderSize = 0;
            this.BtnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.ForeColor = System.Drawing.Color.White;
            this.BtnCancel.Location = new System.Drawing.Point(67, 372);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.BtnCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.BtnCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(100, 50);
            this.BtnCancel.TabIndex = 0;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSubmit
            // 
            this.BtnSubmit.BackColor = System.Drawing.Color.Transparent;
            this.BtnSubmit.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.BtnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BtnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BtnSubmit.FlatAppearance.BorderSize = 0;
            this.BtnSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSubmit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSubmit.ForeColor = System.Drawing.Color.White;
            this.BtnSubmit.Location = new System.Drawing.Point(524, 372);
            this.BtnSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.BtnSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.BtnSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.BtnSubmit.Name = "BtnSubmit";
            this.BtnSubmit.Size = new System.Drawing.Size(100, 50);
            this.BtnSubmit.TabIndex = 1;
            this.BtnSubmit.Text = "Submit";
            this.BtnSubmit.UseVisualStyleBackColor = false;
            this.BtnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // LblHeader
            // 
            this.LblHeader.AutoSize = true;
            this.LblHeader.BackColor = System.Drawing.Color.Transparent;
            this.LblHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHeader.ForeColor = System.Drawing.Color.White;
            this.LblHeader.Location = new System.Drawing.Point(216, 39);
            this.LblHeader.Name = "LblHeader";
            this.LblHeader.Size = new System.Drawing.Size(210, 19);
            this.LblHeader.TabIndex = 2;
            this.LblHeader.Text = "Change Customer Status";
            // 
            // LblCurrCustStatus
            // 
            this.LblCurrCustStatus.AutoSize = true;
            this.LblCurrCustStatus.BackColor = System.Drawing.Color.Transparent;
            this.LblCurrCustStatus.Location = new System.Drawing.Point(178, 145);
            this.LblCurrCustStatus.Name = "LblCurrCustStatus";
            this.LblCurrCustStatus.Size = new System.Drawing.Size(121, 13);
            this.LblCurrCustStatus.TabIndex = 3;
            this.LblCurrCustStatus.Text = "Current Customer Status";
            // 
            // TxBCurrCustStatus
            // 
            this.TxBCurrCustStatus.CausesValidation = false;
            this.TxBCurrCustStatus.Enabled = false;
            this.TxBCurrCustStatus.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.TxBCurrCustStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxBCurrCustStatus.Location = new System.Drawing.Point(344, 140);
            this.TxBCurrCustStatus.Name = "TxBCurrCustStatus";
            this.TxBCurrCustStatus.Size = new System.Drawing.Size(100, 21);
            this.TxBCurrCustStatus.TabIndex = 4;
            this.TxBCurrCustStatus.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // LblChangeCustStatusTo
            // 
            this.LblChangeCustStatusTo.AutoSize = true;
            this.LblChangeCustStatusTo.BackColor = System.Drawing.Color.Transparent;
            this.LblChangeCustStatusTo.Location = new System.Drawing.Point(206, 194);
            this.LblChangeCustStatusTo.Name = "LblChangeCustStatusTo";
            this.LblChangeCustStatusTo.Size = new System.Drawing.Size(93, 13);
            this.LblChangeCustStatusTo.TabIndex = 5;
            this.LblChangeCustStatusTo.Text = "Change Status To";
            // 
            // LblCurrCustReasonCode
            // 
            this.LblCurrCustReasonCode.AutoSize = true;
            this.LblCurrCustReasonCode.BackColor = System.Drawing.Color.Transparent;
            this.LblCurrCustReasonCode.Location = new System.Drawing.Point(190, 248);
            this.LblCurrCustReasonCode.Name = "LblCurrCustReasonCode";
            this.LblCurrCustReasonCode.Size = new System.Drawing.Size(109, 13);
            this.LblCurrCustReasonCode.TabIndex = 7;
            this.LblCurrCustReasonCode.Text = "Current Reason Code";
            // 
            // LblChangeCustReasonTo
            // 
            this.LblChangeCustReasonTo.AutoSize = true;
            this.LblChangeCustReasonTo.BackColor = System.Drawing.Color.Transparent;
            this.LblChangeCustReasonTo.Location = new System.Drawing.Point(199, 300);
            this.LblChangeCustReasonTo.Name = "LblChangeCustReasonTo";
            this.LblChangeCustReasonTo.Size = new System.Drawing.Size(100, 13);
            this.LblChangeCustReasonTo.TabIndex = 8;
            this.LblChangeCustReasonTo.Text = "Change Reason To";
            // 
            // TxBCurrCustReasonCode
            // 
            this.TxBCurrCustReasonCode.CausesValidation = false;
            this.TxBCurrCustReasonCode.Enabled = false;
            this.TxBCurrCustReasonCode.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.TxBCurrCustReasonCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxBCurrCustReasonCode.Location = new System.Drawing.Point(344, 248);
            this.TxBCurrCustReasonCode.Name = "TxBCurrCustReasonCode";
            this.TxBCurrCustReasonCode.Size = new System.Drawing.Size(100, 21);
            this.TxBCurrCustReasonCode.TabIndex = 9;
            this.TxBCurrCustReasonCode.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // cmbChangeCustStatusTo
            // 
            this.cmbChangeCustStatusTo.FormattingEnabled = true;
            this.cmbChangeCustStatusTo.Location = new System.Drawing.Point(344, 194);
            this.cmbChangeCustStatusTo.Name = "cmbChangeCustStatusTo";
            this.cmbChangeCustStatusTo.Size = new System.Drawing.Size(100, 21);
            this.cmbChangeCustStatusTo.TabIndex = 10;
            // 
            // cmbChangeCustReasonTo
            // 
            this.cmbChangeCustReasonTo.FormattingEnabled = true;
            this.cmbChangeCustReasonTo.Location = new System.Drawing.Point(344, 300);
            this.cmbChangeCustReasonTo.Name = "cmbChangeCustReasonTo";
            this.cmbChangeCustReasonTo.Size = new System.Drawing.Size(100, 21);
            this.cmbChangeCustReasonTo.TabIndex = 11;
            // 
            // UpdateCustomerStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Support.Properties.Resources.form_480_340;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(676, 455);
            this.Controls.Add(this.cmbChangeCustReasonTo);
            this.Controls.Add(this.cmbChangeCustStatusTo);
            this.Controls.Add(this.TxBCurrCustReasonCode);
            this.Controls.Add(this.LblChangeCustReasonTo);
            this.Controls.Add(this.LblCurrCustReasonCode);
            this.Controls.Add(this.LblChangeCustStatusTo);
            this.Controls.Add(this.TxBCurrCustStatus);
            this.Controls.Add(this.LblCurrCustStatus);
            this.Controls.Add(this.LblHeader);
            this.Controls.Add(this.BtnSubmit);
            this.Controls.Add(this.BtnCancel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateCustomerStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateCustomerStatus";
            this.Load += new System.EventHandler(this.UpdateCustomerStatus_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Common.Libraries.Forms.Components.CustomButton BtnCancel;
        private Common.Libraries.Forms.Components.CustomButton BtnSubmit;
        private Common.Libraries.Forms.Components.CustomLabel LblHeader;
        private Common.Libraries.Forms.Components.CustomLabel LblCurrCustStatus;
        private Common.Libraries.Forms.Components.CustomTextBox TxBCurrCustStatus;
        private Common.Libraries.Forms.Components.CustomLabel LblChangeCustStatusTo;
        private Common.Libraries.Forms.Components.CustomLabel LblCurrCustReasonCode;
        private Common.Libraries.Forms.Components.CustomLabel LblChangeCustReasonTo;
        private Common.Libraries.Forms.Components.CustomTextBox TxBCurrCustReasonCode;
        private System.Windows.Forms.ComboBox cmbChangeCustStatusTo;
        private System.Windows.Forms.ComboBox cmbChangeCustReasonTo;
    }
}