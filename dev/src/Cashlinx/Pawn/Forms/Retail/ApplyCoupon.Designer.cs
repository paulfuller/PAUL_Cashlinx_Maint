using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Retail
{
    partial class ApplyCoupon
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.submitButton = new System.Windows.Forms.Button();
            this.radioButtonPercentage = new System.Windows.Forms.RadioButton();
            this.radioButtonDollar = new System.Windows.Forms.RadioButton();
            this.labelMdseDesc = new System.Windows.Forms.Label();
            this.labelItemAmt = new System.Windows.Forms.Label();
            this.labelCouponAmt = new System.Windows.Forms.Label();
            this.labelTotalAmount = new System.Windows.Forms.Label();
            this.panelLine = new System.Windows.Forms.Panel();
            this.labelItemAmtValue = new System.Windows.Forms.Label();
            this.labelCouponAmtValue = new System.Windows.Forms.Label();
            this.labelTotalAmtValue = new System.Windows.Forms.Label();
            this.customLabelCouponCode = new CustomLabel();
            this.customTextBoxCouponCode = new CustomTextBox();
            this.customTextBoxCouponAmount = new CustomTextBox();
            this.customTextBoxCouponPercentage = new CustomTextBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(210, 39);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(110, 19);
            this.titleLabel.TabIndex = 136;
            this.titleLabel.Text = "Apply Coupon";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cancelButton.AutoSize = true;
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(27, 314);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 40);
            this.cancelButton.TabIndex = 143;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // submitButton
            // 
            this.submitButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.submitButton.AutoSize = true;
            this.submitButton.BackColor = System.Drawing.Color.Transparent;
            this.submitButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.submitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.submitButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.submitButton.FlatAppearance.BorderSize = 0;
            this.submitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.ForeColor = System.Drawing.Color.White;
            this.submitButton.Location = new System.Drawing.Point(403, 314);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(100, 40);
            this.submitButton.TabIndex = 144;
            this.submitButton.Text = "Apply";
            this.submitButton.UseVisualStyleBackColor = false;
            this.submitButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // radioButtonPercentage
            // 
            this.radioButtonPercentage.AutoSize = true;
            this.radioButtonPercentage.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonPercentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonPercentage.Location = new System.Drawing.Point(27, 114);
            this.radioButtonPercentage.Name = "radioButtonPercentage";
            this.radioButtonPercentage.Size = new System.Drawing.Size(102, 17);
            this.radioButtonPercentage.TabIndex = 145;
            this.radioButtonPercentage.TabStop = true;
            this.radioButtonPercentage.Text = "% Off Coupon";
            this.radioButtonPercentage.UseVisualStyleBackColor = false;
            // 
            // radioButtonDollar
            // 
            this.radioButtonDollar.AutoSize = true;
            this.radioButtonDollar.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonDollar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonDollar.Location = new System.Drawing.Point(248, 114);
            this.radioButtonDollar.Name = "radioButtonDollar";
            this.radioButtonDollar.Size = new System.Drawing.Size(146, 17);
            this.radioButtonDollar.TabIndex = 146;
            this.radioButtonDollar.TabStop = true;
            this.radioButtonDollar.Text = "$ Off Coupon Amount";
            this.radioButtonDollar.UseVisualStyleBackColor = false;
            // 
            // labelMdseDesc
            // 
            this.labelMdseDesc.AutoSize = true;
            this.labelMdseDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelMdseDesc.Location = new System.Drawing.Point(83, 77);
            this.labelMdseDesc.Name = "labelMdseDesc";
            this.labelMdseDesc.Size = new System.Drawing.Size(124, 13);
            this.labelMdseDesc.TabIndex = 147;
            this.labelMdseDesc.Text = "Merchandise Description";
            // 
            // labelItemAmt
            // 
            this.labelItemAmt.AutoSize = true;
            this.labelItemAmt.BackColor = System.Drawing.Color.Transparent;
            this.labelItemAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemAmt.Location = new System.Drawing.Point(47, 203);
            this.labelItemAmt.Name = "labelItemAmt";
            this.labelItemAmt.Size = new System.Drawing.Size(114, 13);
            this.labelItemAmt.TabIndex = 152;
            this.labelItemAmt.Text = "Total Item Amount:";
            // 
            // labelCouponAmt
            // 
            this.labelCouponAmt.AutoSize = true;
            this.labelCouponAmt.BackColor = System.Drawing.Color.Transparent;
            this.labelCouponAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCouponAmt.Location = new System.Drawing.Point(61, 232);
            this.labelCouponAmt.Name = "labelCouponAmt";
            this.labelCouponAmt.Size = new System.Drawing.Size(100, 13);
            this.labelCouponAmt.TabIndex = 153;
            this.labelCouponAmt.Text = "Coupon Amount:";
            // 
            // labelTotalAmount
            // 
            this.labelTotalAmount.AutoSize = true;
            this.labelTotalAmount.BackColor = System.Drawing.Color.Transparent;
            this.labelTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalAmount.Location = new System.Drawing.Point(18, 277);
            this.labelTotalAmount.Name = "labelTotalAmount";
            this.labelTotalAmount.Size = new System.Drawing.Size(143, 13);
            this.labelTotalAmount.TabIndex = 154;
            this.labelTotalAmount.Text = "New Item Total Amount:";
            // 
            // panelLine
            // 
            this.panelLine.BackColor = System.Drawing.Color.Transparent;
            this.panelLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelLine.Location = new System.Drawing.Point(12, 260);
            this.panelLine.Name = "panelLine";
            this.panelLine.Size = new System.Drawing.Size(498, 2);
            this.panelLine.TabIndex = 155;
            // 
            // labelItemAmtValue
            // 
            this.labelItemAmtValue.AutoSize = true;
            this.labelItemAmtValue.BackColor = System.Drawing.Color.Transparent;
            this.labelItemAmtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemAmtValue.Location = new System.Drawing.Point(181, 203);
            this.labelItemAmtValue.Name = "labelItemAmtValue";
            this.labelItemAmtValue.Size = new System.Drawing.Size(53, 13);
            this.labelItemAmtValue.TabIndex = 157;
            this.labelItemAmtValue.Text = "$200.00";
            // 
            // labelCouponAmtValue
            // 
            this.labelCouponAmtValue.AutoSize = true;
            this.labelCouponAmtValue.BackColor = System.Drawing.Color.Transparent;
            this.labelCouponAmtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCouponAmtValue.Location = new System.Drawing.Point(187, 232);
            this.labelCouponAmtValue.Name = "labelCouponAmtValue";
            this.labelCouponAmtValue.Size = new System.Drawing.Size(46, 13);
            this.labelCouponAmtValue.TabIndex = 158;
            this.labelCouponAmtValue.Text = "$10.00";
            // 
            // labelTotalAmtValue
            // 
            this.labelTotalAmtValue.AutoSize = true;
            this.labelTotalAmtValue.BackColor = System.Drawing.Color.Transparent;
            this.labelTotalAmtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalAmtValue.Location = new System.Drawing.Point(181, 277);
            this.labelTotalAmtValue.Name = "labelTotalAmtValue";
            this.labelTotalAmtValue.Size = new System.Drawing.Size(53, 13);
            this.labelTotalAmtValue.TabIndex = 159;
            this.labelTotalAmtValue.Text = "$190.00";
            // 
            // customLabelCouponCode
            // 
            this.customLabelCouponCode.AutoSize = true;
            this.customLabelCouponCode.BackColor = System.Drawing.Color.Transparent;
            this.customLabelCouponCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelCouponCode.Location = new System.Drawing.Point(50, 156);
            this.customLabelCouponCode.Name = "customLabelCouponCode";
            this.customLabelCouponCode.Required = true;
            this.customLabelCouponCode.Size = new System.Drawing.Size(87, 13);
            this.customLabelCouponCode.TabIndex = 160;
            this.customLabelCouponCode.Text = "Coupon Code:";
            // 
            // customTextBoxCouponCode
            // 
            this.customTextBoxCouponCode.CausesValidation = false;
            this.customTextBoxCouponCode.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCouponCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCouponCode.Location = new System.Drawing.Point(170, 149);
            this.customTextBoxCouponCode.MaxLength = 20;
            this.customTextBoxCouponCode.Name = "customTextBoxCouponCode";
            this.customTextBoxCouponCode.Required = true;
            this.customTextBoxCouponCode.Size = new System.Drawing.Size(140, 21);
            this.customTextBoxCouponCode.TabIndex = 151;
            this.customTextBoxCouponCode.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customTextBoxCouponAmount
            // 
            this.customTextBoxCouponAmount.AllowDecimalNumbers = true;
            this.customTextBoxCouponAmount.CausesValidation = false;
            this.customTextBoxCouponAmount.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCouponAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCouponAmount.Location = new System.Drawing.Point(403, 114);
            this.customTextBoxCouponAmount.MaxLength = 8;
            this.customTextBoxCouponAmount.Name = "customTextBoxCouponAmount";
            this.customTextBoxCouponAmount.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxCouponAmount.TabIndex = 149;
            this.customTextBoxCouponAmount.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCouponAmount.TextChanged += new System.EventHandler(this.customTextBoxCouponAmount_TextChanged);
            this.customTextBoxCouponAmount.Leave += new System.EventHandler(this.customTextBoxCouponAmount_Leave);
            // 
            // customTextBoxCouponPercentage
            // 
            this.customTextBoxCouponPercentage.AllowDecimalNumbers = true;
            this.customTextBoxCouponPercentage.CausesValidation = false;
            this.customTextBoxCouponPercentage.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCouponPercentage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCouponPercentage.Location = new System.Drawing.Point(148, 114);
            this.customTextBoxCouponPercentage.MaxLength = 6;
            this.customTextBoxCouponPercentage.Name = "customTextBoxCouponPercentage";
            this.customTextBoxCouponPercentage.Size = new System.Drawing.Size(43, 21);
            this.customTextBoxCouponPercentage.TabIndex = 148;
            this.customTextBoxCouponPercentage.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCouponPercentage.TextChanged += new System.EventHandler(this.customTextBoxCouponPercentage_TextChanged);
            this.customTextBoxCouponPercentage.Leave += new System.EventHandler(this.customTextBoxCouponPercentage_Leave);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonDelete.AutoSize = true;
            this.buttonDelete.BackColor = System.Drawing.Color.Transparent;
            this.buttonDelete.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.buttonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonDelete.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelete.ForeColor = System.Drawing.Color.White;
            this.buttonDelete.Location = new System.Drawing.Point(283, 314);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(110, 40);
            this.buttonDelete.TabIndex = 161;
            this.buttonDelete.Text = "Remove Coupon";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // ApplyCoupon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_320_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(549, 366);
            this.ControlBox = false;
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.customLabelCouponCode);
            this.Controls.Add(this.labelTotalAmtValue);
            this.Controls.Add(this.labelCouponAmtValue);
            this.Controls.Add(this.labelItemAmtValue);
            this.Controls.Add(this.panelLine);
            this.Controls.Add(this.labelTotalAmount);
            this.Controls.Add(this.labelCouponAmt);
            this.Controls.Add(this.labelItemAmt);
            this.Controls.Add(this.customTextBoxCouponCode);
            this.Controls.Add(this.customTextBoxCouponAmount);
            this.Controls.Add(this.customTextBoxCouponPercentage);
            this.Controls.Add(this.labelMdseDesc);
            this.Controls.Add(this.radioButtonDollar);
            this.Controls.Add(this.radioButtonPercentage);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.titleLabel);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ApplyCoupon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Items";
            this.Load += new System.EventHandler(this.ApplyCoupon_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.RadioButton radioButtonPercentage;
        private System.Windows.Forms.RadioButton radioButtonDollar;
        private System.Windows.Forms.Label labelMdseDesc;
        private CustomTextBox customTextBoxCouponPercentage;
        private CustomTextBox customTextBoxCouponAmount;
        private CustomTextBox customTextBoxCouponCode;
        private System.Windows.Forms.Label labelItemAmt;
        private System.Windows.Forms.Label labelCouponAmt;
        private System.Windows.Forms.Label labelTotalAmount;
        private System.Windows.Forms.Panel panelLine;
        private System.Windows.Forms.Label labelItemAmtValue;
        private System.Windows.Forms.Label labelCouponAmtValue;
        private System.Windows.Forms.Label labelTotalAmtValue;
        private CustomLabel customLabelCouponCode;
        private System.Windows.Forms.Button buttonDelete;
    }
}
