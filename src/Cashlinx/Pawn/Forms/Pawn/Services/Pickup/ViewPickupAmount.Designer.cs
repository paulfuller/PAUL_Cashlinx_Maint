namespace Pawn.Forms.Pawn.Services.Pickup
{
    partial class ViewPickupAmount
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
            this.labelHeading = new System.Windows.Forms.Label();
            this.labelLoanNumber = new System.Windows.Forms.Label();
            this.labelLoanAmtHeading = new System.Windows.Forms.Label();
            this.labelIntAmountHeading = new System.Windows.Forms.Label();
            this.labelGunLockFeeHeading = new System.Windows.Forms.Label();
            this.labelPickupAmountHeading = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.labelLoanAmount = new System.Windows.Forms.Label();
            this.labelInterestAmount = new System.Windows.Forms.Label();
            this.labelGunLockFeeAmount = new System.Windows.Forms.Label();
            this.labelPickupAmount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelHeading.Location = new System.Drawing.Point(13, 13);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(87, 19);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Pawn Loan";
            // 
            // labelLoanNumber
            // 
            this.labelLoanNumber.BackColor = System.Drawing.Color.Transparent;
            this.labelLoanNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoanNumber.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelLoanNumber.Location = new System.Drawing.Point(96, 13);
            this.labelLoanNumber.Name = "labelLoanNumber";
            this.labelLoanNumber.Size = new System.Drawing.Size(100, 19);
            this.labelLoanNumber.TabIndex = 1;
            this.labelLoanNumber.Text = "Loan Number";
            // 
            // labelLoanAmtHeading
            // 
            this.labelLoanAmtHeading.AutoSize = true;
            this.labelLoanAmtHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelLoanAmtHeading.Location = new System.Drawing.Point(63, 63);
            this.labelLoanAmtHeading.Name = "labelLoanAmtHeading";
            this.labelLoanAmtHeading.Size = new System.Drawing.Size(73, 13);
            this.labelLoanAmtHeading.TabIndex = 2;
            this.labelLoanAmtHeading.Text = "Loan Amount:";
            this.labelLoanAmtHeading.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelIntAmountHeading
            // 
            this.labelIntAmountHeading.AutoSize = true;
            this.labelIntAmountHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelIntAmountHeading.Location = new System.Drawing.Point(52, 76);
            this.labelIntAmountHeading.Name = "labelIntAmountHeading";
            this.labelIntAmountHeading.Size = new System.Drawing.Size(84, 13);
            this.labelIntAmountHeading.TabIndex = 3;
            this.labelIntAmountHeading.Text = "Interest Amount:";
            this.labelIntAmountHeading.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelGunLockFeeHeading
            // 
            this.labelGunLockFeeHeading.AutoSize = true;
            this.labelGunLockFeeHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelGunLockFeeHeading.Location = new System.Drawing.Point(58, 89);
            this.labelGunLockFeeHeading.Name = "labelGunLockFeeHeading";
            this.labelGunLockFeeHeading.Size = new System.Drawing.Size(78, 13);
            this.labelGunLockFeeHeading.TabIndex = 4;
            this.labelGunLockFeeHeading.Text = "Gun Lock Fee:";
            // 
            // labelPickupAmountHeading
            // 
            this.labelPickupAmountHeading.AutoSize = true;
            this.labelPickupAmountHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelPickupAmountHeading.Location = new System.Drawing.Point(52, 128);
            this.labelPickupAmountHeading.Name = "labelPickupAmountHeading";
            this.labelPickupAmountHeading.Size = new System.Drawing.Size(82, 13);
            this.labelPickupAmountHeading.TabIndex = 5;
            this.labelPickupAmountHeading.Text = "Pickup Amount:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(1, 176);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 2);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.Transparent;
            this.closeButton.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.closeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.closeButton.CausesValidation = false;
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(13, 185);
            this.closeButton.Margin = new System.Windows.Forms.Padding(4);
            this.closeButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.closeButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(100, 50);
            this.closeButton.TabIndex = 51;
            this.closeButton.Text = "&Close";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // labelLoanAmount
            // 
            this.labelLoanAmount.AutoSize = true;
            this.labelLoanAmount.BackColor = System.Drawing.Color.Transparent;
            this.labelLoanAmount.Location = new System.Drawing.Point(159, 63);
            this.labelLoanAmount.Name = "labelLoanAmount";
            this.labelLoanAmount.Size = new System.Drawing.Size(34, 13);
            this.labelLoanAmount.TabIndex = 52;
            this.labelLoanAmount.Text = "$0.00";
            // 
            // labelInterestAmount
            // 
            this.labelInterestAmount.AutoSize = true;
            this.labelInterestAmount.BackColor = System.Drawing.Color.Transparent;
            this.labelInterestAmount.Location = new System.Drawing.Point(162, 76);
            this.labelInterestAmount.Name = "labelInterestAmount";
            this.labelInterestAmount.Size = new System.Drawing.Size(34, 13);
            this.labelInterestAmount.TabIndex = 53;
            this.labelInterestAmount.Text = "$0.00";
            // 
            // labelGunLockFeeAmount
            // 
            this.labelGunLockFeeAmount.AutoSize = true;
            this.labelGunLockFeeAmount.BackColor = System.Drawing.Color.Transparent;
            this.labelGunLockFeeAmount.Location = new System.Drawing.Point(162, 89);
            this.labelGunLockFeeAmount.Name = "labelGunLockFeeAmount";
            this.labelGunLockFeeAmount.Size = new System.Drawing.Size(34, 13);
            this.labelGunLockFeeAmount.TabIndex = 54;
            this.labelGunLockFeeAmount.Text = "$0.00";
            // 
            // labelPickupAmount
            // 
            this.labelPickupAmount.AutoSize = true;
            this.labelPickupAmount.BackColor = System.Drawing.Color.Transparent;
            this.labelPickupAmount.Location = new System.Drawing.Point(162, 128);
            this.labelPickupAmount.Name = "labelPickupAmount";
            this.labelPickupAmount.Size = new System.Drawing.Size(34, 13);
            this.labelPickupAmount.TabIndex = 55;
            this.labelPickupAmount.Text = "$0.00";
            // 
            // ViewPickupAmount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_400_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(318, 246);
            this.ControlBox = false;
            this.Controls.Add(this.labelPickupAmount);
            this.Controls.Add(this.labelGunLockFeeAmount);
            this.Controls.Add(this.labelInterestAmount);
            this.Controls.Add(this.labelLoanAmount);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelPickupAmountHeading);
            this.Controls.Add(this.labelGunLockFeeHeading);
            this.Controls.Add(this.labelIntAmountHeading);
            this.Controls.Add(this.labelLoanAmtHeading);
            this.Controls.Add(this.labelLoanNumber);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViewPickupAmount";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewPickupAmount";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label labelLoanNumber;
        private System.Windows.Forms.Label labelLoanAmtHeading;
        private System.Windows.Forms.Label labelIntAmountHeading;
        private System.Windows.Forms.Label labelGunLockFeeHeading;
        private System.Windows.Forms.Label labelPickupAmountHeading;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label labelLoanAmount;
        private System.Windows.Forms.Label labelInterestAmount;
        private System.Windows.Forms.Label labelGunLockFeeAmount;
        private System.Windows.Forms.Label labelPickupAmount;
    }
}