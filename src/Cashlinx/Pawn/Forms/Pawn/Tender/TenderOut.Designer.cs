using Common.Libraries.Forms.Components;
using Pawn.Forms.Pawn.Tender.Base;

namespace Pawn.Forms.Pawn.Tender
{
    partial class TenderOut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TenderOut));
            this.tenderFormLabel = new System.Windows.Forms.Label();
            this.continueButton = new CustomButton();
            this.cancelButton = new CustomButton();
            this.deleteButton = new CustomButton();
            this.customLabel1 = new System.Windows.Forms.Label();
            this.amountFieldLabel = new System.Windows.Forms.Label();
            this.labelCoupon = new System.Windows.Forms.Label();
            this.labelCouponAmt = new System.Windows.Forms.Label();
            this.labelTotal = new System.Windows.Forms.Label();
            this.labelTotAmount = new System.Windows.Forms.Label();
            this.tenderTablePanel = new TenderTablePanel();
            this.tenderTypeSelector = new TenderTypePanel();
            this.SuspendLayout();
            // 
            // tenderFormLabel
            // 
            this.tenderFormLabel.AutoSize = true;
            this.tenderFormLabel.BackColor = System.Drawing.Color.Transparent;
            this.tenderFormLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenderFormLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tenderFormLabel.Location = new System.Drawing.Point(305, 17);
            this.tenderFormLabel.Name = "tenderFormLabel";
            this.tenderFormLabel.Size = new System.Drawing.Size(90, 19);
            this.tenderFormLabel.TabIndex = 4;
            this.tenderFormLabel.Text = "Tender Out";
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
            this.continueButton.Location = new System.Drawing.Point(585, 655);
            this.continueButton.Margin = new System.Windows.Forms.Padding(0);
            this.continueButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.continueButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(100, 50);
            this.continueButton.TabIndex = 6;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = false;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
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
            this.cancelButton.Location = new System.Drawing.Point(28, 655);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.Color.Transparent;
            this.deleteButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.deleteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.deleteButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deleteButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.deleteButton.FlatAppearance.BorderSize = 0;
            this.deleteButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.deleteButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Location = new System.Drawing.Point(128, 655);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(0);
            this.deleteButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.deleteButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(100, 50);
            this.deleteButton.TabIndex = 8;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.Location = new System.Drawing.Point(25, 43);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(123, 16);
            this.customLabel1.TabIndex = 9;
            this.customLabel1.Text = "Due To Customer:";
            this.customLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // amountFieldLabel
            // 
            this.amountFieldLabel.AutoSize = true;
            this.amountFieldLabel.BackColor = System.Drawing.Color.Transparent;
            this.amountFieldLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amountFieldLabel.Location = new System.Drawing.Point(190, 43);
            this.amountFieldLabel.Name = "amountFieldLabel";
            this.amountFieldLabel.Size = new System.Drawing.Size(96, 16);
            this.amountFieldLabel.TabIndex = 10;
            this.amountFieldLabel.Text = "<amountField>";
            this.amountFieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tenderTablePanel
            // 
            this.tenderTablePanel.BackColor = System.Drawing.Color.Transparent;
            this.tenderTablePanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenderTablePanel.ForeColor = System.Drawing.Color.Black;
            this.tenderTablePanel.Location = new System.Drawing.Point(28, 106);
            this.tenderTablePanel.Margin = new System.Windows.Forms.Padding(0);
            this.tenderTablePanel.MaximumSize = new System.Drawing.Size(445, 480);
            this.tenderTablePanel.MinimumSize = new System.Drawing.Size(445, 480);
            this.tenderTablePanel.Name = "tenderTablePanel";
            this.tenderTablePanel.Size = new System.Drawing.Size(445, 480);
            this.tenderTablePanel.TabIndex = 12;
            // 
            // tenderTypeSelector
            // 
            this.tenderTypeSelector.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tenderTypeSelector.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tenderTypeSelector.BackgroundImage")));
            this.tenderTypeSelector.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tenderTypeSelector.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tenderTypeSelector.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenderTypeSelector.ForeColor = System.Drawing.Color.White;
            this.tenderTypeSelector.Location = new System.Drawing.Point(485, 106);
            this.tenderTypeSelector.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tenderTypeSelector.Name = "tenderTypeSelector";
            this.tenderTypeSelector.Size = new System.Drawing.Size(200, 480);
            this.tenderTypeSelector.TabIndex = 11;
            // 
            // TenderOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_768_BlueScale;
            this.ClientSize = new System.Drawing.Size(700, 732);
            this.Controls.Add(this.tenderTablePanel);
            this.Controls.Add(this.tenderTypeSelector);
            this.Controls.Add(this.amountFieldLabel);
            this.Controls.Add(this.customLabel1);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.tenderFormLabel);
            this.Name = "TenderOut";
            this.Text = "TenderOut";
            this.Shown += new System.EventHandler(this.TenderOut_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tenderFormLabel;
        private CustomButton continueButton;
        private CustomButton cancelButton;
        private CustomButton deleteButton;
        private System.Windows.Forms.Label customLabel1;
        private System.Windows.Forms.Label amountFieldLabel;
        private System.Windows.Forms.Label labelCoupon;
        private System.Windows.Forms.Label labelCouponAmt;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label labelTotAmount;
        private TenderTypePanel tenderTypeSelector;
        private TenderTablePanel tenderTablePanel;
    }
}
