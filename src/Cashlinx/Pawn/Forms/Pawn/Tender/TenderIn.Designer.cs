using System;
using Common.Libraries.Forms.Components;
using Pawn.Forms.Pawn.Tender.Base;

namespace Pawn.Forms.Pawn.Tender
{
    partial class TenderIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TenderIn));
            this.continueButton = new CustomButton();
            this.cancelButton = new CustomButton();
            this.deleteButton = new CustomButton();
            this.backButton = new CustomButton();
            this.amountFieldLabel = new System.Windows.Forms.Label();
            this.customLabel1 = new System.Windows.Forms.Label();
            this.tenderFormLabel = new System.Windows.Forms.Label();
            this.tenderTablePanel = new TenderTablePanel();
            this.tenderTypeSelector = new TenderTypePanel();
            this.shopCreditLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // continueButton
            // 
            this.continueButton.BackColor = System.Drawing.Color.Transparent;
            this.continueButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.continueButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.continueButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.continueButton.Enabled = false;
            this.continueButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.continueButton.FlatAppearance.BorderSize = 0;
            this.continueButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueButton.ForeColor = System.Drawing.Color.White;
            this.continueButton.Location = new System.Drawing.Point(585, 732);
            this.continueButton.Margin = new System.Windows.Forms.Padding(0);
            this.continueButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.continueButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(100, 50);
            this.continueButton.TabIndex = 4;
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
            this.cancelButton.Location = new System.Drawing.Point(128, 732);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 5;
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
            this.deleteButton.Location = new System.Drawing.Point(228, 732);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(0);
            this.deleteButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.deleteButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(100, 50);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.Transparent;
            this.backButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.backButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.backButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.backButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.ForeColor = System.Drawing.Color.White;
            this.backButton.Location = new System.Drawing.Point(28, 732);
            this.backButton.Margin = new System.Windows.Forms.Padding(0);
            this.backButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.backButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(100, 50);
            this.backButton.TabIndex = 7;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // amountFieldLabel
            // 
            this.amountFieldLabel.AutoSize = true;
            this.amountFieldLabel.BackColor = System.Drawing.Color.Transparent;
            this.amountFieldLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amountFieldLabel.Location = new System.Drawing.Point(206, 48);
            this.amountFieldLabel.Name = "amountFieldLabel";
            this.amountFieldLabel.Size = new System.Drawing.Size(96, 16);
            this.amountFieldLabel.TabIndex = 9;
            this.amountFieldLabel.Text = "<amountField>";
            this.amountFieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.Location = new System.Drawing.Point(25, 48);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(139, 16);
            this.customLabel1.TabIndex = 8;
            this.customLabel1.Text = "Due From Customer:";
            this.customLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tenderFormLabel
            // 
            this.tenderFormLabel.AutoSize = true;
            this.tenderFormLabel.BackColor = System.Drawing.Color.Transparent;
            this.tenderFormLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenderFormLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tenderFormLabel.Location = new System.Drawing.Point(307, 17);
            this.tenderFormLabel.Name = "tenderFormLabel";
            this.tenderFormLabel.Size = new System.Drawing.Size(79, 19);
            this.tenderFormLabel.TabIndex = 3;
            this.tenderFormLabel.Text = "Tender In";
            // 
            // tenderTablePanel
            // 
            this.tenderTablePanel.AutoSize = true;
            this.tenderTablePanel.BackColor = System.Drawing.Color.Transparent;
            this.tenderTablePanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenderTablePanel.ForeColor = System.Drawing.Color.Black;
            this.tenderTablePanel.Location = new System.Drawing.Point(28, 96);
            this.tenderTablePanel.Margin = new System.Windows.Forms.Padding(0);
            this.tenderTablePanel.MaximumSize = new System.Drawing.Size(445, 542);
            this.tenderTablePanel.MinimumSize = new System.Drawing.Size(445, 480);
            this.tenderTablePanel.Name = "tenderTablePanel";
            this.tenderTablePanel.Size = new System.Drawing.Size(445, 542);
            this.tenderTablePanel.TabIndex = 11;
            // 
            // tenderTypeSelector
            // 
            this.tenderTypeSelector.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tenderTypeSelector.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tenderTypeSelector.BackgroundImage")));
            this.tenderTypeSelector.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tenderTypeSelector.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tenderTypeSelector.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenderTypeSelector.ForeColor = System.Drawing.Color.White;
            this.tenderTypeSelector.Location = new System.Drawing.Point(485, 96);
            this.tenderTypeSelector.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tenderTypeSelector.Name = "tenderTypeSelector";
            this.tenderTypeSelector.Size = new System.Drawing.Size(200, 480);
            this.tenderTypeSelector.TabIndex = 10;
            // 
            // shopCreditLabel
            // 
            this.shopCreditLabel.AutoSize = true;
            this.shopCreditLabel.BackColor = System.Drawing.Color.Transparent;
            this.shopCreditLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shopCreditLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shopCreditLabel.ForeColor = System.Drawing.Color.Red;
            this.shopCreditLabel.Location = new System.Drawing.Point(25, 73);
            this.shopCreditLabel.Name = "shopCreditLabel";
            this.shopCreditLabel.Size = new System.Drawing.Size(485, 16);
            this.shopCreditLabel.TabIndex = 12;
            this.shopCreditLabel.Text = "$XX.XX SHOP CREDIT AVAILABLE FOR THIS CUSTOMER (BALANCE=$XX.XX)";
            this.shopCreditLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.shopCreditLabel.Visible = false;
            // 
            // TenderIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_768_BlueScale;
            this.ClientSize = new System.Drawing.Size(700, 800);
            this.Controls.Add(this.shopCreditLabel);
            this.Controls.Add(this.tenderTablePanel);
            this.Controls.Add(this.tenderTypeSelector);
            this.Controls.Add(this.amountFieldLabel);
            this.Controls.Add(this.customLabel1);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.tenderFormLabel);
            this.MaximumSize = new System.Drawing.Size(700, 800);
            this.MinimumSize = new System.Drawing.Size(700, 700);
            this.Name = "TenderIn";
            this.Text = "TenderIn";
            this.Shown += new System.EventHandler(this.TenderIn_Shown);
            this.VisibleChanged += new System.EventHandler(this.TenderIn_VisibleChanged);
            this.GotFocus += new EventHandler(TenderIn_GotFocus);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomButton continueButton;
        private CustomButton cancelButton;
        private CustomButton deleteButton;
        private CustomButton backButton;
        private TenderTypePanel tenderTypeSelector;
        private System.Windows.Forms.Label amountFieldLabel;
        private System.Windows.Forms.Label customLabel1;
        private System.Windows.Forms.Label tenderFormLabel;
        private TenderTablePanel tenderTablePanel;
        private System.Windows.Forms.Label shopCreditLabel;
    }
}
