using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Tender.Base
{
    partial class TenderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TenderForm));
            this.tenderFormLabel = new CustomLabel();
            this.tenderTypePanel1 = new TenderTypePanel();
            this.continueButton = new CustomButton();
            this.cancelButton = new CustomButton();
            this.optionButton2 = new CustomButton();
            this.optionButton1 = new CustomButton();
            this.customLabel1 = new CustomLabel();
            this.amountFieldLabel = new CustomLabel();
            this.SuspendLayout();
            // 
            // tenderFormLabel
            // 
            this.tenderFormLabel.AutoSize = true;
            this.tenderFormLabel.BackColor = System.Drawing.Color.Transparent;
            this.tenderFormLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenderFormLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tenderFormLabel.Location = new System.Drawing.Point(300, 18);
            this.tenderFormLabel.Name = "tenderFormLabel";
            this.tenderFormLabel.Size = new System.Drawing.Size(101, 19);
            this.tenderFormLabel.TabIndex = 0;
            this.tenderFormLabel.Text = "Tender Form";
            // 
            // tenderTypePanel1
            // 
            this.tenderTypePanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tenderTypePanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tenderTypePanel1.BackgroundImage")));
            this.tenderTypePanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tenderTypePanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tenderTypePanel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenderTypePanel1.ForeColor = System.Drawing.Color.White;
            this.tenderTypePanel1.Location = new System.Drawing.Point(487, 48);
            this.tenderTypePanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tenderTypePanel1.Name = "tenderTypePanel1";
            this.tenderTypePanel1.Size = new System.Drawing.Size(200, 480);
            this.tenderTypePanel1.TabIndex = 1;
            // 
            // continueButton
            // 
            this.continueButton.BackColor = System.Drawing.Color.Transparent;
            this.continueButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.continueButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.continueButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.continueButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.continueButton.FlatAppearance.BorderSize = 0;
            this.continueButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueButton.ForeColor = System.Drawing.Color.White;
            this.continueButton.Location = new System.Drawing.Point(537, 620);
            this.continueButton.Margin = new System.Windows.Forms.Padding(0);
            this.continueButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.continueButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(100, 50);
            this.continueButton.TabIndex = 2;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = false;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(126, 620);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            // 
            // optionButton2
            // 
            this.optionButton2.BackColor = System.Drawing.Color.Transparent;
            this.optionButton2.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.optionButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.optionButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.optionButton2.Enabled = false;
            this.optionButton2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.optionButton2.FlatAppearance.BorderSize = 0;
            this.optionButton2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.optionButton2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.optionButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optionButton2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionButton2.ForeColor = System.Drawing.Color.White;
            this.optionButton2.Location = new System.Drawing.Point(226, 620);
            this.optionButton2.Margin = new System.Windows.Forms.Padding(0);
            this.optionButton2.MaximumSize = new System.Drawing.Size(100, 50);
            this.optionButton2.MinimumSize = new System.Drawing.Size(100, 50);
            this.optionButton2.Name = "optionButton2";
            this.optionButton2.Size = new System.Drawing.Size(100, 50);
            this.optionButton2.TabIndex = 4;
            this.optionButton2.Text = "Option 2";
            this.optionButton2.UseVisualStyleBackColor = false;
            // 
            // optionButton1
            // 
            this.optionButton1.BackColor = System.Drawing.Color.Transparent;
            this.optionButton1.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.optionButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.optionButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.optionButton1.Enabled = false;
            this.optionButton1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.optionButton1.FlatAppearance.BorderSize = 0;
            this.optionButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.optionButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.optionButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.optionButton1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionButton1.ForeColor = System.Drawing.Color.White;
            this.optionButton1.Location = new System.Drawing.Point(26, 620);
            this.optionButton1.Margin = new System.Windows.Forms.Padding(0);
            this.optionButton1.MaximumSize = new System.Drawing.Size(100, 50);
            this.optionButton1.MinimumSize = new System.Drawing.Size(100, 50);
            this.optionButton1.Name = "optionButton1";
            this.optionButton1.Size = new System.Drawing.Size(100, 50);
            this.optionButton1.TabIndex = 5;
            this.optionButton1.Text = "Option 1";
            this.optionButton1.UseVisualStyleBackColor = false;
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.Location = new System.Drawing.Point(23, 48);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(159, 16);
            this.customLabel1.TabIndex = 6;
            this.customLabel1.Text = "Total Due To Customer:";
            this.customLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // amountFieldLabel
            // 
            this.amountFieldLabel.AutoSize = true;
            this.amountFieldLabel.BackColor = System.Drawing.Color.Transparent;
            this.amountFieldLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amountFieldLabel.Location = new System.Drawing.Point(223, 48);
            this.amountFieldLabel.Name = "amountFieldLabel";
            this.amountFieldLabel.Size = new System.Drawing.Size(96, 16);
            this.amountFieldLabel.TabIndex = 7;
            this.amountFieldLabel.Text = "<amountField>";
            this.amountFieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TenderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_768_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.amountFieldLabel);
            this.Controls.Add(this.customLabel1);
            this.Controls.Add(this.optionButton1);
            this.Controls.Add(this.optionButton2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.tenderTypePanel1);
            this.Controls.Add(this.tenderFormLabel);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(700, 700);
            this.MinimumSize = new System.Drawing.Size(700, 700);
            this.Name = "TenderForm";
            this.Size = new System.Drawing.Size(700, 700);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomLabel tenderFormLabel;
        private TenderTypePanel tenderTypePanel1;
        private CustomButton continueButton;
        private CustomButton cancelButton;
        private CustomButton optionButton2;
        private CustomButton optionButton1;
        private CustomLabel customLabel1;
        private CustomLabel amountFieldLabel;
    }
}