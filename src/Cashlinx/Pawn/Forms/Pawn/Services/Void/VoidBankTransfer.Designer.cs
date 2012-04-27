using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.Void
{
    partial class VoidBankTransfer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoidBankTransfer));
            this.labelHeading = new System.Windows.Forms.Label();
            this.customLabelTransferNoHeading = new CustomLabel();
            this.customLabelDateHeading = new CustomLabel();
            this.customLabelUserIDHeading = new CustomLabel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelDestinationHeading = new System.Windows.Forms.Label();
            this.labelSourceHeading = new System.Windows.Forms.Label();
            this.customLabelSafeHeading = new CustomLabel();
            this.customLabelTrAmtHeading = new CustomLabel();
            this.customLabelBankNameHeading = new CustomLabel();
            this.customLabelAcctNumHeading = new CustomLabel();
            this.customLabelChkNumHeading = new CustomLabel();
            this.customButtonCancel = new CustomButton();
            this.customButtonVoid = new CustomButton();
            this.customLabelTransferNumber = new CustomLabel();
            this.customLabelDate = new CustomLabel();
            this.customLabelUserId = new CustomLabel();
            this.customLabelSafe = new CustomLabel();
            this.customLabelTrAmount = new CustomLabel();
            this.customLabelBankName = new CustomLabel();
            this.customLabelAcctNumber = new CustomLabel();
            this.customLabelCheckNumber = new CustomLabel();
            this.customLabelComment = new CustomLabel();
            this.customTextBoxComment = new CustomTextBox();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(25, 24);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(117, 16);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Transfer To Bank";
            // 
            // customLabelTransferNoHeading
            // 
            this.customLabelTransferNoHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelTransferNoHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelTransferNoHeading.Location = new System.Drawing.Point(70, 75);
            this.customLabelTransferNoHeading.Name = "customLabelTransferNoHeading";
            this.customLabelTransferNoHeading.Size = new System.Drawing.Size(72, 14);
            this.customLabelTransferNoHeading.TabIndex = 124;
            this.customLabelTransferNoHeading.Text = "Transfer #:";
            // 
            // customLabelDateHeading
            // 
            this.customLabelDateHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelDateHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelDateHeading.Location = new System.Drawing.Point(94, 98);
            this.customLabelDateHeading.Name = "customLabelDateHeading";
            this.customLabelDateHeading.Size = new System.Drawing.Size(37, 23);
            this.customLabelDateHeading.TabIndex = 125;
            this.customLabelDateHeading.Text = "Date:";
            // 
            // customLabelUserIDHeading
            // 
            this.customLabelUserIDHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelUserIDHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelUserIDHeading.Location = new System.Drawing.Point(83, 121);
            this.customLabelUserIDHeading.Name = "customLabelUserIDHeading";
            this.customLabelUserIDHeading.Size = new System.Drawing.Size(48, 20);
            this.customLabelUserIDHeading.TabIndex = 126;
            this.customLabelUserIDHeading.Text = "User ID:";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.RoyalBlue;
            this.panelHeader.Controls.Add(this.labelDestinationHeading);
            this.panelHeader.Controls.Add(this.labelSourceHeading);
            this.panelHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelHeader.ForeColor = System.Drawing.Color.White;
            this.panelHeader.Location = new System.Drawing.Point(4, 144);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(587, 23);
            this.panelHeader.TabIndex = 127;
            // 
            // labelDestinationHeading
            // 
            this.labelDestinationHeading.AutoSize = true;
            this.labelDestinationHeading.Location = new System.Drawing.Point(374, 4);
            this.labelDestinationHeading.Name = "labelDestinationHeading";
            this.labelDestinationHeading.Size = new System.Drawing.Size(82, 16);
            this.labelDestinationHeading.TabIndex = 1;
            this.labelDestinationHeading.Text = "Destination";
            // 
            // labelSourceHeading
            // 
            this.labelSourceHeading.AutoSize = true;
            this.labelSourceHeading.Location = new System.Drawing.Point(35, 4);
            this.labelSourceHeading.Name = "labelSourceHeading";
            this.labelSourceHeading.Size = new System.Drawing.Size(53, 16);
            this.labelSourceHeading.TabIndex = 0;
            this.labelSourceHeading.Text = "Source";
            // 
            // customLabelSafeHeading
            // 
            this.customLabelSafeHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelSafeHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelSafeHeading.Location = new System.Drawing.Point(90, 195);
            this.customLabelSafeHeading.Name = "customLabelSafeHeading";
            this.customLabelSafeHeading.Size = new System.Drawing.Size(41, 14);
            this.customLabelSafeHeading.TabIndex = 128;
            this.customLabelSafeHeading.Text = "Safe:";
            // 
            // customLabelTrAmtHeading
            // 
            this.customLabelTrAmtHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelTrAmtHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelTrAmtHeading.Location = new System.Drawing.Point(39, 219);
            this.customLabelTrAmtHeading.Name = "customLabelTrAmtHeading";
            this.customLabelTrAmtHeading.Size = new System.Drawing.Size(92, 23);
            this.customLabelTrAmtHeading.TabIndex = 129;
            this.customLabelTrAmtHeading.Text = "Transfer Amount:";
            // 
            // customLabelBankNameHeading
            // 
            this.customLabelBankNameHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelBankNameHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelBankNameHeading.Location = new System.Drawing.Point(327, 181);
            this.customLabelBankNameHeading.Name = "customLabelBankNameHeading";
            this.customLabelBankNameHeading.Required = true;
            this.customLabelBankNameHeading.Size = new System.Drawing.Size(67, 16);
            this.customLabelBankNameHeading.TabIndex = 130;
            this.customLabelBankNameHeading.Text = "Bank Name:";
            // 
            // customLabelAcctNumHeading
            // 
            this.customLabelAcctNumHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelAcctNumHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelAcctNumHeading.Location = new System.Drawing.Point(308, 202);
            this.customLabelAcctNumHeading.Name = "customLabelAcctNumHeading";
            this.customLabelAcctNumHeading.Size = new System.Drawing.Size(98, 16);
            this.customLabelAcctNumHeading.TabIndex = 131;
            this.customLabelAcctNumHeading.Text = "Account Number:";
            // 
            // customLabelChkNumHeading
            // 
            this.customLabelChkNumHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelChkNumHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelChkNumHeading.Location = new System.Drawing.Point(318, 223);
            this.customLabelChkNumHeading.Name = "customLabelChkNumHeading";
            this.customLabelChkNumHeading.Size = new System.Drawing.Size(83, 23);
            this.customLabelChkNumHeading.TabIndex = 132;
            this.customLabelChkNumHeading.Text = "Check Number:";
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
            this.customButtonCancel.Location = new System.Drawing.Point(42, 326);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 133;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // customButtonVoid
            // 
            this.customButtonVoid.BackColor = System.Drawing.Color.Transparent;
            this.customButtonVoid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonVoid.BackgroundImage")));
            this.customButtonVoid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonVoid.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonVoid.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonVoid.FlatAppearance.BorderSize = 0;
            this.customButtonVoid.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonVoid.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonVoid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonVoid.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonVoid.ForeColor = System.Drawing.Color.White;
            this.customButtonVoid.Location = new System.Drawing.Point(464, 326);
            this.customButtonVoid.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonVoid.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonVoid.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonVoid.Name = "customButtonVoid";
            this.customButtonVoid.Size = new System.Drawing.Size(100, 50);
            this.customButtonVoid.TabIndex = 134;
            this.customButtonVoid.Text = "Void";
            this.customButtonVoid.UseVisualStyleBackColor = false;
            this.customButtonVoid.Click += new System.EventHandler(this.customButtonVoid_Click);
            // 
            // customLabelTransferNumber
            // 
            this.customLabelTransferNumber.AutoSize = true;
            this.customLabelTransferNumber.BackColor = System.Drawing.Color.Transparent;
            this.customLabelTransferNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelTransferNumber.Location = new System.Drawing.Point(158, 75);
            this.customLabelTransferNumber.Name = "customLabelTransferNumber";
            this.customLabelTransferNumber.Size = new System.Drawing.Size(37, 13);
            this.customLabelTransferNumber.TabIndex = 135;
            this.customLabelTransferNumber.Text = "12345";
            // 
            // customLabelDate
            // 
            this.customLabelDate.AutoSize = true;
            this.customLabelDate.BackColor = System.Drawing.Color.Transparent;
            this.customLabelDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelDate.Location = new System.Drawing.Point(158, 98);
            this.customLabelDate.Name = "customLabelDate";
            this.customLabelDate.Size = new System.Drawing.Size(112, 13);
            this.customLabelDate.TabIndex = 136;
            this.customLabelDate.Text = "01/01/2011 09:15 AM";
            // 
            // customLabelUserId
            // 
            this.customLabelUserId.AutoSize = true;
            this.customLabelUserId.BackColor = System.Drawing.Color.Transparent;
            this.customLabelUserId.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelUserId.Location = new System.Drawing.Point(158, 121);
            this.customLabelUserId.Name = "customLabelUserId";
            this.customLabelUserId.Size = new System.Drawing.Size(41, 13);
            this.customLabelUserId.TabIndex = 137;
            this.customLabelUserId.Text = "aagent";
            // 
            // customLabelSafe
            // 
            this.customLabelSafe.AutoSize = true;
            this.customLabelSafe.BackColor = System.Drawing.Color.Transparent;
            this.customLabelSafe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelSafe.Location = new System.Drawing.Point(147, 195);
            this.customLabelSafe.Name = "customLabelSafe";
            this.customLabelSafe.Size = new System.Drawing.Size(64, 13);
            this.customLabelSafe.TabIndex = 138;
            this.customLabelSafe.Text = "02030_safe";
            // 
            // customLabelTrAmount
            // 
            this.customLabelTrAmount.BackColor = System.Drawing.Color.Transparent;
            this.customLabelTrAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelTrAmount.Location = new System.Drawing.Point(147, 219);
            this.customLabelTrAmount.Name = "customLabelTrAmount";
            this.customLabelTrAmount.Size = new System.Drawing.Size(92, 23);
            this.customLabelTrAmount.TabIndex = 139;
            this.customLabelTrAmount.Text = "$100.00";
            // 
            // customLabelBankName
            // 
            this.customLabelBankName.AutoSize = true;
            this.customLabelBankName.BackColor = System.Drawing.Color.Transparent;
            this.customLabelBankName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelBankName.Location = new System.Drawing.Point(400, 181);
            this.customLabelBankName.Name = "customLabelBankName";
            this.customLabelBankName.Size = new System.Drawing.Size(89, 13);
            this.customLabelBankName.TabIndex = 140;
            this.customLabelBankName.Text = "Wells Fargo Bank";
            // 
            // customLabelAcctNumber
            // 
            this.customLabelAcctNumber.BackColor = System.Drawing.Color.Transparent;
            this.customLabelAcctNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelAcctNumber.Location = new System.Drawing.Point(400, 202);
            this.customLabelAcctNumber.Name = "customLabelAcctNumber";
            this.customLabelAcctNumber.Size = new System.Drawing.Size(183, 16);
            this.customLabelAcctNumber.TabIndex = 141;
            this.customLabelAcctNumber.Text = "12345";
            // 
            // customLabelCheckNumber
            // 
            this.customLabelCheckNumber.BackColor = System.Drawing.Color.Transparent;
            this.customLabelCheckNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelCheckNumber.Location = new System.Drawing.Point(400, 223);
            this.customLabelCheckNumber.Name = "customLabelCheckNumber";
            this.customLabelCheckNumber.Size = new System.Drawing.Size(164, 23);
            this.customLabelCheckNumber.TabIndex = 142;
            this.customLabelCheckNumber.Text = "2222";
            // 
            // customLabelComment
            // 
            this.customLabelComment.BackColor = System.Drawing.Color.Transparent;
            this.customLabelComment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelComment.Location = new System.Drawing.Point(61, 262);
            this.customLabelComment.Name = "customLabelComment";
            this.customLabelComment.Required = true;
            this.customLabelComment.Size = new System.Drawing.Size(61, 14);
            this.customLabelComment.TabIndex = 145;
            this.customLabelComment.Text = "Comment:";
            // 
            // customTextBoxComment
            // 
            this.customTextBoxComment.CausesValidation = false;
            this.customTextBoxComment.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxComment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxComment.Location = new System.Drawing.Point(141, 262);
            this.customTextBoxComment.Name = "customTextBoxComment";
            this.customTextBoxComment.Size = new System.Drawing.Size(202, 21);
            this.customTextBoxComment.TabIndex = 146;
            this.customTextBoxComment.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // VoidBankTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 385);
            this.Controls.Add(this.customTextBoxComment);
            this.Controls.Add(this.customLabelComment);
            this.Controls.Add(this.customLabelCheckNumber);
            this.Controls.Add(this.customLabelAcctNumber);
            this.Controls.Add(this.customLabelBankName);
            this.Controls.Add(this.customLabelTrAmount);
            this.Controls.Add(this.customLabelSafe);
            this.Controls.Add(this.customLabelUserId);
            this.Controls.Add(this.customLabelDate);
            this.Controls.Add(this.customLabelTransferNumber);
            this.Controls.Add(this.customButtonVoid);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.customLabelChkNumHeading);
            this.Controls.Add(this.customLabelAcctNumHeading);
            this.Controls.Add(this.customLabelBankNameHeading);
            this.Controls.Add(this.customLabelTrAmtHeading);
            this.Controls.Add(this.customLabelSafeHeading);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.customLabelUserIDHeading);
            this.Controls.Add(this.customLabelTransferNoHeading);
            this.Controls.Add(this.customLabelDateHeading);
            this.Controls.Add(this.labelHeading);
            this.Name = "VoidBankTransfer";
            this.Text = "VoidBankTransfer";
            this.Load += new System.EventHandler(this.VoidBankTransfer_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private CustomLabel customLabelTransferNoHeading;
        private CustomLabel customLabelDateHeading;
        private CustomLabel customLabelUserIDHeading;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelDestinationHeading;
        private System.Windows.Forms.Label labelSourceHeading;
        private CustomLabel customLabelSafeHeading;
        private CustomLabel customLabelTrAmtHeading;
        private CustomLabel customLabelBankNameHeading;
        private CustomLabel customLabelAcctNumHeading;
        private CustomLabel customLabelChkNumHeading;
        private CustomButton customButtonCancel;
        private CustomButton customButtonVoid;
        private CustomLabel customLabelTransferNumber;
        private CustomLabel customLabelDate;
        private CustomLabel customLabelUserId;
        private CustomLabel customLabelSafe;
        private CustomLabel customLabelTrAmount;
        private CustomLabel customLabelBankName;
        private CustomLabel customLabelAcctNumber;
        private CustomLabel customLabelCheckNumber;
        private CustomLabel customLabelComment;
        private CustomTextBox customTextBoxComment;
    }
}