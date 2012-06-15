using Common.Libraries.Forms.Components;
//Odd lock fix
namespace Support.Forms.ShopAdmin.EditGunBook
{
    partial class GunBookSearch
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
            this.txtGunNumber = new Common.Libraries.Forms.Components.CustomTextBox();
            this.customLabelGunNumber = new Common.Libraries.Forms.Components.CustomLabel();
            this.submitButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblShopNumber = new System.Windows.Forms.Label();
            this.tboxShopNumber = new Common.Libraries.Forms.Components.CustomTextBox();
            this.SuspendLayout();
            // 
            // txtGunNumber
            // 
            this.txtGunNumber.AllowOnlyNumbers = true;
            this.txtGunNumber.CausesValidation = false;
            this.txtGunNumber.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.txtGunNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGunNumber.Location = new System.Drawing.Point(206, 80);
            this.txtGunNumber.MaxLength = 9;
            this.txtGunNumber.Name = "txtGunNumber";
            this.txtGunNumber.Required = true;
            this.txtGunNumber.Size = new System.Drawing.Size(122, 21);
            this.txtGunNumber.TabIndex = 2;
            this.txtGunNumber.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // customLabelGunNumber
            // 
            this.customLabelGunNumber.AutoSize = true;
            this.customLabelGunNumber.BackColor = System.Drawing.Color.Transparent;
            this.customLabelGunNumber.Location = new System.Drawing.Point(110, 83);
            this.customLabelGunNumber.Name = "customLabelGunNumber";
            this.customLabelGunNumber.Required = true;
            this.customLabelGunNumber.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.customLabelGunNumber.Size = new System.Drawing.Size(70, 13);
            this.customLabelGunNumber.TabIndex = 0;
            this.customLabelGunNumber.Text = "Gun Number:";
            // 
            // submitButton
            // 
            this.submitButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.submitButton.AutoSize = true;
            this.submitButton.BackColor = System.Drawing.Color.Transparent;
            this.submitButton.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.submitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.submitButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.submitButton.FlatAppearance.BorderSize = 0;
            this.submitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.ForeColor = System.Drawing.Color.White;
            this.submitButton.Location = new System.Drawing.Point(279, 128);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(100, 40);
            this.submitButton.TabIndex = 144;
            this.submitButton.Text = "Find";
            this.submitButton.UseVisualStyleBackColor = false;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cancelButton.AutoSize = true;
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(60, 128);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 40);
            this.cancelButton.TabIndex = 143;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 145;
            this.label1.Text = "Gun Search";
            // 
            // lblShopNumber
            // 
            this.lblShopNumber.AutoSize = true;
            this.lblShopNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblShopNumber.Location = new System.Drawing.Point(110, 48);
            this.lblShopNumber.Name = "lblShopNumber";
            this.lblShopNumber.Size = new System.Drawing.Size(75, 13);
            this.lblShopNumber.TabIndex = 146;
            this.lblShopNumber.Text = "Enter Shop #:";
            // 
            // tboxShopNumber
            // 
            this.tboxShopNumber.CausesValidation = false;
            this.tboxShopNumber.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.tboxShopNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxShopNumber.Location = new System.Drawing.Point(206, 40);
            this.tboxShopNumber.MaxLength = 5;
            this.tboxShopNumber.Name = "tboxShopNumber";
            this.tboxShopNumber.Size = new System.Drawing.Size(71, 21);
            this.tboxShopNumber.TabIndex = 1;
            this.tboxShopNumber.ValidationExpression = global::Support.Properties.Resources.OverrideMachineName;
            // 
            // GunBookSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Support.Properties.Resources.Red_background;
            this.ClientSize = new System.Drawing.Size(439, 178);
            this.ControlBox = false;
            this.Controls.Add(this.tboxShopNumber);
            this.Controls.Add(this.lblShopNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.customLabelGunNumber);
            this.Controls.Add(this.txtGunNumber);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.submitButton);
            this.Name = "GunBookSearch";
            this.Text = "";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomTextBox txtGunNumber;
        private CustomLabel customLabelGunNumber;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblShopNumber;
        private CustomTextBox tboxShopNumber;
    }
}