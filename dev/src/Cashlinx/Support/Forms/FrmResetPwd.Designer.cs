using Common.Libraries.Forms.Components;

namespace Support.Forms
{
    partial class FrmResetPwd
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
            this.changePasswordHeaderLabel = new System.Windows.Forms.Label();
            this.TxtUserId = new CustomTextBox();
            this.confirmNewPasswordLabel = new CustomLabel();
            this.submitButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // changePasswordHeaderLabel
            // 
            this.changePasswordHeaderLabel.AutoSize = true;
            this.changePasswordHeaderLabel.BackColor = System.Drawing.Color.Transparent;
            this.changePasswordHeaderLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changePasswordHeaderLabel.ForeColor = System.Drawing.Color.White;
            this.changePasswordHeaderLabel.Location = new System.Drawing.Point(90, 27);
            this.changePasswordHeaderLabel.Name = "changePasswordHeaderLabel";
            this.changePasswordHeaderLabel.Size = new System.Drawing.Size(156, 19);
            this.changePasswordHeaderLabel.TabIndex = 1;
            this.changePasswordHeaderLabel.Text = "Reset User Password";
            // 
            // TxtUserId
            // 
            this.TxtUserId.BackColor = System.Drawing.Color.White;
            this.TxtUserId.CausesValidation = false;
            this.TxtUserId.ErrorMessage = "";
            this.TxtUserId.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUserId.ForeColor = System.Drawing.Color.Black;
            this.TxtUserId.Location = new System.Drawing.Point(120, 115);
            this.TxtUserId.MaxLength = 20;
            this.TxtUserId.Name = "TxtUserId";
            this.TxtUserId.Required = true;
            this.TxtUserId.Size = new System.Drawing.Size(175, 21);
            this.TxtUserId.TabIndex = 5;
            this.TxtUserId.ValidationExpression = "";
            // 
            // confirmNewPasswordLabel
            // 
            this.confirmNewPasswordLabel.AutoSize = true;
            this.confirmNewPasswordLabel.BackColor = System.Drawing.Color.Transparent;
            this.confirmNewPasswordLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmNewPasswordLabel.Location = new System.Drawing.Point(54, 119);
            this.confirmNewPasswordLabel.Name = "confirmNewPasswordLabel";
            this.confirmNewPasswordLabel.Required = true;
            this.confirmNewPasswordLabel.Size = new System.Drawing.Size(43, 13);
            this.confirmNewPasswordLabel.TabIndex = 4;
            this.confirmNewPasswordLabel.Text = "User ID";
            // 
            // submitButton
            // 
            this.submitButton.BackColor = System.Drawing.Color.Transparent;
            this.submitButton.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.submitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.submitButton.FlatAppearance.BorderSize = 0;
            this.submitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submitButton.ForeColor = System.Drawing.Color.White;
            this.submitButton.Location = new System.Drawing.Point(202, 179);
            this.submitButton.Margin = new System.Windows.Forms.Padding(0);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(100, 38);
            this.submitButton.TabIndex = 12;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = false;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(40, 179);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 38);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // FrmResetPwd
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Support.Properties.Resources.form_350_240;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(350, 240);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.TxtUserId);
            this.Controls.Add(this.confirmNewPasswordLabel);
            this.Controls.Add(this.changePasswordHeaderLabel);
            this.MaximumSize = new System.Drawing.Size(350, 240);
            this.MinimumSize = new System.Drawing.Size(350, 240);
            this.Name = "FrmResetPwd";
            this.Text = "FrmResetPwd";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label changePasswordHeaderLabel;
        private CustomTextBox TxtUserId;
        private CustomLabel confirmNewPasswordLabel;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button cancelButton;
    }
}