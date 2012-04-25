using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.ChargeOff
{
    partial class ChargeOffDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChargeOffDetails));
            this.label1 = new System.Windows.Forms.Label();
            this.labelICNHeader = new System.Windows.Forms.Label();
            this.labelICN = new System.Windows.Forms.Label();
            this.labelChargeOffHeader = new System.Windows.Forms.Label();
            this.labelChargeOffAmt = new System.Windows.Forms.Label();
            this.richTextBoxMdDesc = new System.Windows.Forms.RichTextBox();
            this.comboBoxReason = new System.Windows.Forms.ComboBox();
            this.richTextBoxComment = new System.Windows.Forms.RichTextBox();
            this.customTextBoxAuthBy = new CustomTextBox();
            this.customLabelReason = new CustomLabel();
            this.customLabelComment = new CustomLabel();
            this.customButtonCancel = new CustomButton();
            this.customButtonBack = new CustomButton();
            this.customButtonSubmit = new CustomButton();
            this.customLabelAuthBy = new CustomLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inventory Charge Off";
            // 
            // labelICNHeader
            // 
            this.labelICNHeader.AutoSize = true;
            this.labelICNHeader.BackColor = System.Drawing.Color.Transparent;
            this.labelICNHeader.Location = new System.Drawing.Point(27, 97);
            this.labelICNHeader.Name = "labelICNHeader";
            this.labelICNHeader.Size = new System.Drawing.Size(29, 13);
            this.labelICNHeader.TabIndex = 1;
            this.labelICNHeader.Text = "ICN:";
            // 
            // labelICN
            // 
            this.labelICN.AutoSize = true;
            this.labelICN.BackColor = System.Drawing.Color.Transparent;
            this.labelICN.Location = new System.Drawing.Point(63, 97);
            this.labelICN.Name = "labelICN";
            this.labelICN.Size = new System.Drawing.Size(53, 13);
            this.labelICN.TabIndex = 2;
            this.labelICN.Text = "123456.1";
            // 
            // labelChargeOffHeader
            // 
            this.labelChargeOffHeader.AutoSize = true;
            this.labelChargeOffHeader.BackColor = System.Drawing.Color.Transparent;
            this.labelChargeOffHeader.Location = new System.Drawing.Point(357, 97);
            this.labelChargeOffHeader.Name = "labelChargeOffHeader";
            this.labelChargeOffHeader.Size = new System.Drawing.Size(105, 13);
            this.labelChargeOffHeader.TabIndex = 3;
            this.labelChargeOffHeader.Text = "Charge Off Amount:";
            // 
            // labelChargeOffAmt
            // 
            this.labelChargeOffAmt.AutoSize = true;
            this.labelChargeOffAmt.BackColor = System.Drawing.Color.Transparent;
            this.labelChargeOffAmt.Location = new System.Drawing.Point(470, 96);
            this.labelChargeOffAmt.Name = "labelChargeOffAmt";
            this.labelChargeOffAmt.Size = new System.Drawing.Size(47, 13);
            this.labelChargeOffAmt.TabIndex = 4;
            this.labelChargeOffAmt.Text = "$100.00";
            // 
            // richTextBoxMdDesc
            // 
            this.richTextBoxMdDesc.Location = new System.Drawing.Point(30, 131);
            this.richTextBoxMdDesc.Name = "richTextBoxMdDesc";
            this.richTextBoxMdDesc.ReadOnly = true;
            this.richTextBoxMdDesc.Size = new System.Drawing.Size(516, 83);
            this.richTextBoxMdDesc.TabIndex = 5;
            this.richTextBoxMdDesc.Text = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // comboBoxReason
            // 
            this.comboBoxReason.FormattingEnabled = true;
            this.comboBoxReason.Items.AddRange(new object[] {
            "Broken",
            "Destroyed",
            "Donation",
            "NXT",
            "Store Use"});
            this.comboBoxReason.Location = new System.Drawing.Point(194, 227);
            this.comboBoxReason.Name = "comboBoxReason";
            this.comboBoxReason.Size = new System.Drawing.Size(172, 21);
            this.comboBoxReason.TabIndex = 7;
            this.comboBoxReason.SelectedIndexChanged += new System.EventHandler(this.comboBoxReason_SelectedIndexChanged);
            // 
            // richTextBoxComment
            // 
            this.richTextBoxComment.Location = new System.Drawing.Point(194, 263);
            this.richTextBoxComment.MaxLength = 60;
            this.richTextBoxComment.Name = "richTextBoxComment";
            this.richTextBoxComment.Size = new System.Drawing.Size(268, 44);
            this.richTextBoxComment.TabIndex = 9;
            this.richTextBoxComment.Text = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customTextBoxAuthBy
            // 
            this.customTextBoxAuthBy.CausesValidation = false;
            this.customTextBoxAuthBy.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxAuthBy.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxAuthBy.Location = new System.Drawing.Point(194, 336);
            this.customTextBoxAuthBy.MaxLength = 80;
            this.customTextBoxAuthBy.Name = "customTextBoxAuthBy";
            this.customTextBoxAuthBy.Size = new System.Drawing.Size(185, 21);
            this.customTextBoxAuthBy.TabIndex = 11;
            this.customTextBoxAuthBy.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customLabelReason
            // 
            this.customLabelReason.AutoSize = true;
            this.customLabelReason.BackColor = System.Drawing.Color.Transparent;
            this.customLabelReason.Location = new System.Drawing.Point(52, 230);
            this.customLabelReason.Name = "customLabelReason";
            this.customLabelReason.Required = true;
            this.customLabelReason.Size = new System.Drawing.Size(121, 13);
            this.customLabelReason.TabIndex = 12;
            this.customLabelReason.Text = "Reason for Charge Off:";
            // 
            // customLabelComment
            // 
            this.customLabelComment.AutoSize = true;
            this.customLabelComment.BackColor = System.Drawing.Color.Transparent;
            this.customLabelComment.Location = new System.Drawing.Point(98, 279);
            this.customLabelComment.Name = "customLabelComment";
            this.customLabelComment.Required = true;
            this.customLabelComment.Size = new System.Drawing.Size(56, 13);
            this.customLabelComment.TabIndex = 13;
            this.customLabelComment.Text = "Comment:";
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
            this.customButtonCancel.Location = new System.Drawing.Point(30, 387);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 14;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
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
            this.customButtonBack.Location = new System.Drawing.Point(158, 387);
            this.customButtonBack.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonBack.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.Name = "customButtonBack";
            this.customButtonBack.Size = new System.Drawing.Size(100, 50);
            this.customButtonBack.TabIndex = 15;
            this.customButtonBack.Text = "Back";
            this.customButtonBack.UseVisualStyleBackColor = false;
            this.customButtonBack.Click += new System.EventHandler(this.customButtonBack_Click);
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
            this.customButtonSubmit.Location = new System.Drawing.Point(473, 387);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 16;
            this.customButtonSubmit.Text = "Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.customButtonSubmit_Click);
            // 
            // customLabelAuthBy
            // 
            this.customLabelAuthBy.AutoSize = true;
            this.customLabelAuthBy.BackColor = System.Drawing.Color.Transparent;
            this.customLabelAuthBy.Location = new System.Drawing.Point(80, 339);
            this.customLabelAuthBy.Name = "customLabelAuthBy";
            this.customLabelAuthBy.Size = new System.Drawing.Size(78, 13);
            this.customLabelAuthBy.TabIndex = 17;
            this.customLabelAuthBy.Text = "Authorized By:";
            // 
            // ChargeOffDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 463);
            this.Controls.Add(this.customLabelAuthBy);
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonBack);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.customLabelComment);
            this.Controls.Add(this.customLabelReason);
            this.Controls.Add(this.customTextBoxAuthBy);
            this.Controls.Add(this.richTextBoxComment);
            this.Controls.Add(this.comboBoxReason);
            this.Controls.Add(this.richTextBoxMdDesc);
            this.Controls.Add(this.labelChargeOffAmt);
            this.Controls.Add(this.labelChargeOffHeader);
            this.Controls.Add(this.labelICN);
            this.Controls.Add(this.labelICNHeader);
            this.Controls.Add(this.label1);
            this.Name = "ChargeOffDetails";
            this.Text = "ChargeOffDetails";
            this.Load += new System.EventHandler(this.ChargeOffDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelICNHeader;
        private System.Windows.Forms.Label labelICN;
        private System.Windows.Forms.Label labelChargeOffHeader;
        private System.Windows.Forms.Label labelChargeOffAmt;
        private System.Windows.Forms.RichTextBox richTextBoxMdDesc;
        private System.Windows.Forms.ComboBox comboBoxReason;
        private System.Windows.Forms.RichTextBox richTextBoxComment;
        private CustomTextBox customTextBoxAuthBy;
        private CustomLabel customLabelReason;
        private CustomLabel customLabelComment;
        private CustomButton customButtonCancel;
        private CustomButton customButtonBack;
        private CustomButton customButtonSubmit;
        private CustomLabel customLabelAuthBy;
    }
}