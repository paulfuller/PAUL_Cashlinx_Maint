namespace Pawn.Forms.Pawn.Services.Pickup
{
    partial class ProrateWaiveFees
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelLoanAmtHeading = new System.Windows.Forms.Label();
            this.labelInterestHeading = new System.Windows.Forms.Label();
            this.labelLoanAmount = new System.Windows.Forms.Label();
            this.labelInterest = new System.Windows.Forms.Label();
            this.labelPickupAmountHeading = new System.Windows.Forms.Label();
            this.labelPickupAmount = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelLateFees = new System.Windows.Forms.Label();
            this.labelLateFeeHeading = new System.Windows.Forms.Label();
            this.labelLostTktFeeHeading = new System.Windows.Forms.Label();
            this.labelLostTicketFeeAmount = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelHeading.Location = new System.Drawing.Point(13, 19);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(211, 16);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Pickup Amount for Pawn Loan";
            // 
            // labelLoanNumber
            // 
            this.labelLoanNumber.AutoSize = true;
            this.labelLoanNumber.BackColor = System.Drawing.Color.Transparent;
            this.labelLoanNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoanNumber.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelLoanNumber.Location = new System.Drawing.Point(222, 19);
            this.labelLoanNumber.Name = "labelLoanNumber";
            this.labelLoanNumber.Size = new System.Drawing.Size(100, 16);
            this.labelLoanNumber.TabIndex = 1;
            this.labelLoanNumber.Text = "Loan Number";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.labelLostTicketFeeAmount, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelLostTktFeeHeading, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelLateFeeHeading, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelLoanAmtHeading, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelLateFees, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelInterestHeading, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelLoanAmount, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelInterest, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(30, 70);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(363, 83);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // labelLoanAmtHeading
            // 
            this.labelLoanAmtHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelLoanAmtHeading.AutoSize = true;
            this.labelLoanAmtHeading.Location = new System.Drawing.Point(105, 3);
            this.labelLoanAmtHeading.Name = "labelLoanAmtHeading";
            this.labelLoanAmtHeading.Size = new System.Drawing.Size(73, 13);
            this.labelLoanAmtHeading.TabIndex = 0;
            this.labelLoanAmtHeading.Text = "Loan Amount:";
            // 
            // labelInterestHeading
            // 
            this.labelInterestHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelInterestHeading.AutoSize = true;
            this.labelInterestHeading.Location = new System.Drawing.Point(133, 23);
            this.labelInterestHeading.Name = "labelInterestHeading";
            this.labelInterestHeading.Size = new System.Drawing.Size(45, 13);
            this.labelInterestHeading.TabIndex = 1;
            this.labelInterestHeading.Text = "Interest:";
            // 
            // labelLoanAmount
            // 
            this.labelLoanAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelLoanAmount.AutoSize = true;
            this.labelLoanAmount.Location = new System.Drawing.Point(184, 3);
            this.labelLoanAmount.Name = "labelLoanAmount";
            this.labelLoanAmount.Size = new System.Drawing.Size(13, 13);
            this.labelLoanAmount.TabIndex = 7;
            this.labelLoanAmount.Text = "$";
            // 
            // labelInterest
            // 
            this.labelInterest.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelInterest.AutoSize = true;
            this.labelInterest.Location = new System.Drawing.Point(184, 23);
            this.labelInterest.Name = "labelInterest";
            this.labelInterest.Size = new System.Drawing.Size(13, 13);
            this.labelInterest.TabIndex = 8;
            this.labelInterest.Text = "$";
            // 
            // labelPickupAmountHeading
            // 
            this.labelPickupAmountHeading.AutoSize = true;
            this.labelPickupAmountHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelPickupAmountHeading.Location = new System.Drawing.Point(101, 173);
            this.labelPickupAmountHeading.Name = "labelPickupAmountHeading";
            this.labelPickupAmountHeading.Size = new System.Drawing.Size(109, 13);
            this.labelPickupAmountHeading.TabIndex = 5;
            this.labelPickupAmountHeading.Text = "Total Pickup Amount:";
            // 
            // labelPickupAmount
            // 
            this.labelPickupAmount.AutoSize = true;
            this.labelPickupAmount.BackColor = System.Drawing.Color.Transparent;
            this.labelPickupAmount.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelPickupAmount.Location = new System.Drawing.Point(211, 173);
            this.labelPickupAmount.Name = "labelPickupAmount";
            this.labelPickupAmount.Size = new System.Drawing.Size(13, 13);
            this.labelPickupAmount.TabIndex = 6;
            this.labelPickupAmount.Text = "$";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(6, 207);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(587, 2);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
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
            this.buttonCancel.Location = new System.Drawing.Point(30, 227);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 50);
            this.buttonCancel.TabIndex = 52;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelLateFees
            // 
            this.labelLateFees.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelLateFees.AutoSize = true;
            this.labelLateFees.Location = new System.Drawing.Point(184, 43);
            this.labelLateFees.Name = "labelLateFees";
            this.labelLateFees.Size = new System.Drawing.Size(13, 13);
            this.labelLateFees.TabIndex = 12;
            this.labelLateFees.Text = "$";
            // 
            // labelLateFeeHeading
            // 
            this.labelLateFeeHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelLateFeeHeading.AutoSize = true;
            this.labelLateFeeHeading.Location = new System.Drawing.Point(121, 43);
            this.labelLateFeeHeading.Name = "labelLateFeeHeading";
            this.labelLateFeeHeading.Size = new System.Drawing.Size(57, 13);
            this.labelLateFeeHeading.TabIndex = 0;
            this.labelLateFeeHeading.Text = "Late Fees:";
            // 
            // labelLostTktFeeHeading
            // 
            this.labelLostTktFeeHeading.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelLostTktFeeHeading.AutoSize = true;
            this.labelLostTktFeeHeading.Location = new System.Drawing.Point(94, 65);
            this.labelLostTktFeeHeading.Name = "labelLostTktFeeHeading";
            this.labelLostTktFeeHeading.Size = new System.Drawing.Size(84, 13);
            this.labelLostTktFeeHeading.TabIndex = 13;
            this.labelLostTktFeeHeading.Text = "Lost Ticket Fee:";
            // 
            // labelLostTicketFeeAmount
            // 
            this.labelLostTicketFeeAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelLostTicketFeeAmount.AutoSize = true;
            this.labelLostTicketFeeAmount.BackColor = System.Drawing.Color.Transparent;
            this.labelLostTicketFeeAmount.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelLostTicketFeeAmount.Location = new System.Drawing.Point(184, 65);
            this.labelLostTicketFeeAmount.Name = "labelLostTicketFeeAmount";
            this.labelLostTicketFeeAmount.Size = new System.Drawing.Size(13, 13);
            this.labelLostTicketFeeAmount.TabIndex = 14;
            this.labelLostTicketFeeAmount.Text = "$";
            // 
            // ProrateWaiveFees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_440_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(593, 311);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelPickupAmount);
            this.Controls.Add(this.labelPickupAmountHeading);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.labelLoanNumber);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProrateWaiveFees";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProrateWaiveLateFees";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label labelLoanNumber;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelLoanAmtHeading;
        private System.Windows.Forms.Label labelInterestHeading;
        private System.Windows.Forms.Label labelLoanAmount;
        private System.Windows.Forms.Label labelInterest;
        private System.Windows.Forms.Label labelPickupAmountHeading;
        private System.Windows.Forms.Label labelPickupAmount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelLateFees;
        private System.Windows.Forms.Label labelLateFeeHeading;
        private System.Windows.Forms.Label labelLostTktFeeHeading;
        private System.Windows.Forms.Label labelLostTicketFeeAmount;
    }
}