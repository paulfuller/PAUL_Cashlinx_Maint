using Common.Libraries.Forms.Components;
using Pawn.Forms.Pawn.Tender.Base;

namespace Pawn.Forms.Pawn.Tender
{
    partial class DisbursementDetailsForm
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
            this.customLabel1 = new CustomLabel();
            this.tenderTypePanelDisburse = new TenderTypePanel();
            this.continueButton = new CustomButton();
            this.cancelButton = new CustomButton();
            this.customLabel2 = new CustomLabel();
            this.customLabel3 = new CustomLabel();
            this.bankOrRefLabel = new CustomLabel();
            this.checkNumberLabel = new CustomLabel();
            this.SuspendLayout();
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.ForeColor = System.Drawing.Color.White;
            this.customLabel1.Location = new System.Drawing.Point(221, 23);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(158, 19);
            this.customLabel1.TabIndex = 0;
            this.customLabel1.Text = "Disbursement Details";
            // 
            // tenderTypePanelDisburse
            // 
            this.tenderTypePanelDisburse.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tenderTypePanelDisburse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tenderTypePanelDisburse.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tenderTypePanelDisburse.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenderTypePanelDisburse.ForeColor = System.Drawing.Color.White;
            this.tenderTypePanelDisburse.Location = new System.Drawing.Point(389, 63);
            this.tenderTypePanelDisburse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tenderTypePanelDisburse.Name = "tenderTypePanelDisburse";
            this.tenderTypePanelDisburse.Size = new System.Drawing.Size(200, 224);
            this.tenderTypePanelDisburse.TabIndex = 1;
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
            this.continueButton.Location = new System.Drawing.Point(440, 326);
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
            this.cancelButton.Location = new System.Drawing.Point(60, 326);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            // 
            // customLabel2
            // 
            this.customLabel2.AutoSize = true;
            this.customLabel2.BackColor = System.Drawing.Color.Transparent;
            this.customLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customLabel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel2.Location = new System.Drawing.Point(57, 103);
            this.customLabel2.Name = "customLabel2";
            this.customLabel2.Size = new System.Drawing.Size(122, 16);
            this.customLabel2.TabIndex = 17;
            this.customLabel2.Text = "Pay To Customer:";
            this.customLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabel3
            // 
            this.customLabel3.AutoSize = true;
            this.customLabel3.BackColor = System.Drawing.Color.Transparent;
            this.customLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customLabel3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel3.Location = new System.Drawing.Point(57, 147);
            this.customLabel3.Name = "customLabel3";
            this.customLabel3.Size = new System.Drawing.Size(118, 16);
            this.customLabel3.TabIndex = 18;
            this.customLabel3.Text = "Tender Selected:";
            this.customLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bankOrRefLabel
            // 
            this.bankOrRefLabel.AutoSize = true;
            this.bankOrRefLabel.BackColor = System.Drawing.Color.Transparent;
            this.bankOrRefLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bankOrRefLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bankOrRefLabel.Location = new System.Drawing.Point(57, 194);
            this.bankOrRefLabel.Name = "bankOrRefLabel";
            this.bankOrRefLabel.Size = new System.Drawing.Size(44, 16);
            this.bankOrRefLabel.TabIndex = 19;
            this.bankOrRefLabel.Text = "Bank:";
            this.bankOrRefLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bankOrRefLabel.Visible = false;
            // 
            // checkNumberLabel
            // 
            this.checkNumberLabel.AutoSize = true;
            this.checkNumberLabel.BackColor = System.Drawing.Color.Transparent;
            this.checkNumberLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkNumberLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkNumberLabel.Location = new System.Drawing.Point(57, 238);
            this.checkNumberLabel.Name = "checkNumberLabel";
            this.checkNumberLabel.Size = new System.Drawing.Size(104, 16);
            this.checkNumberLabel.TabIndex = 20;
            this.checkNumberLabel.Text = "Check Number:";
            this.checkNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.checkNumberLabel.Visible = false;
            // 
            // DisbursementDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_480_BlueScale;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.checkNumberLabel);
            this.Controls.Add(this.bankOrRefLabel);
            this.Controls.Add(this.customLabel3);
            this.Controls.Add(this.customLabel2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.tenderTypePanelDisburse);
            this.Controls.Add(this.customLabel1);
            this.Name = "DisbursementDetailsForm";
            this.Text = "DisbursementDetailsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomLabel customLabel1;
        private TenderTypePanel tenderTypePanelDisburse;
        private CustomButton continueButton;
        private CustomButton cancelButton;
        private CustomLabel customLabel2;
        private CustomLabel customLabel3;
        private CustomLabel bankOrRefLabel;
        private CustomLabel checkNumberLabel;
    }
}