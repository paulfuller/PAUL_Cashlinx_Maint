using Common.Libraries.Forms.Components;

namespace Common.Libraries.Forms
{
    partial class UserChangePassword
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
            this.oldPasswordLabel = new Common.Libraries.Forms.Components.CustomLabel();
            this.newPasswordLabel = new Common.Libraries.Forms.Components.CustomLabel();
            this.confirmNewPasswordLabel = new Common.Libraries.Forms.Components.CustomLabel();
            this.currentPasswordTextBox = new Common.Libraries.Forms.Components.CustomTextBox();
            this.newPasswordTextBox = new Common.Libraries.Forms.Components.CustomTextBox();
            this.confirmNewPasswordTextBox = new Common.Libraries.Forms.Components.CustomTextBox();
            this.cancelButton = new Common.Libraries.Forms.Components.CustomButton();
            this.submitButton = new Common.Libraries.Forms.Components.CustomButton();
            this.SuspendLayout();
            // 
            // changePasswordHeaderLabel
            // 
            this.changePasswordHeaderLabel.AutoSize = true;
            this.changePasswordHeaderLabel.BackColor = System.Drawing.Color.Transparent;
            this.changePasswordHeaderLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changePasswordHeaderLabel.ForeColor = System.Drawing.Color.White;
            this.changePasswordHeaderLabel.Location = new System.Drawing.Point(90, 25);
            this.changePasswordHeaderLabel.Name = "changePasswordHeaderLabel";
            this.changePasswordHeaderLabel.Size = new System.Drawing.Size(171, 19);
            this.changePasswordHeaderLabel.TabIndex = 0;
            this.changePasswordHeaderLabel.Text = "Change User Password";
            // 
            // oldPasswordLabel
            // 
            this.oldPasswordLabel.AutoSize = true;
            this.oldPasswordLabel.BackColor = System.Drawing.Color.Transparent;
            this.oldPasswordLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oldPasswordLabel.Location = new System.Drawing.Point(56, 103);
            this.oldPasswordLabel.Name = "oldPasswordLabel";
            this.oldPasswordLabel.Required = true;
            this.oldPasswordLabel.Size = new System.Drawing.Size(93, 13);
            this.oldPasswordLabel.TabIndex = 1;
            this.oldPasswordLabel.Text = "Current Password";
            // 
            // newPasswordLabel
            // 
            this.newPasswordLabel.AutoSize = true;
            this.newPasswordLabel.BackColor = System.Drawing.Color.Transparent;
            this.newPasswordLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newPasswordLabel.Location = new System.Drawing.Point(72, 164);
            this.newPasswordLabel.Name = "newPasswordLabel";
            this.newPasswordLabel.Required = true;
            this.newPasswordLabel.Size = new System.Drawing.Size(77, 13);
            this.newPasswordLabel.TabIndex = 2;
            this.newPasswordLabel.Text = "New Password";
            // 
            // confirmNewPasswordLabel
            // 
            this.confirmNewPasswordLabel.AutoSize = true;
            this.confirmNewPasswordLabel.BackColor = System.Drawing.Color.Transparent;
            this.confirmNewPasswordLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmNewPasswordLabel.Location = new System.Drawing.Point(32, 225);
            this.confirmNewPasswordLabel.Name = "confirmNewPasswordLabel";
            this.confirmNewPasswordLabel.Required = true;
            this.confirmNewPasswordLabel.Size = new System.Drawing.Size(117, 13);
            this.confirmNewPasswordLabel.TabIndex = 3;
            this.confirmNewPasswordLabel.Text = "Confirm New Password";
            // 
            // currentPasswordTextBox
            // 
            this.currentPasswordTextBox.BackColor = System.Drawing.Color.White;
            this.currentPasswordTextBox.CausesValidation = false;
            this.currentPasswordTextBox.ErrorMessage = string.Empty;
            this.currentPasswordTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentPasswordTextBox.ForeColor = System.Drawing.Color.Black;
            this.currentPasswordTextBox.Location = new System.Drawing.Point(169, 99);
            this.currentPasswordTextBox.MaxLength = 20;
            this.currentPasswordTextBox.Name = "currentPasswordTextBox";
            this.currentPasswordTextBox.Required = true;
            this.currentPasswordTextBox.ShortcutsEnabled = false;
            this.currentPasswordTextBox.Size = new System.Drawing.Size(150, 21);
            this.currentPasswordTextBox.TabIndex = 1;
            this.currentPasswordTextBox.UseSystemPasswordChar = true;
            this.currentPasswordTextBox.ValidationExpression = string.Empty;
            this.currentPasswordTextBox.TextChanged += new System.EventHandler(this.currentPasswordTextBox_TextChanged);
            // 
            // newPasswordTextBox
            // 
            this.newPasswordTextBox.BackColor = System.Drawing.Color.White;
            this.newPasswordTextBox.CausesValidation = false;
            this.newPasswordTextBox.Enabled = false;
            this.newPasswordTextBox.ErrorMessage = string.Empty;
            this.newPasswordTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newPasswordTextBox.ForeColor = System.Drawing.Color.Black;
            this.newPasswordTextBox.Location = new System.Drawing.Point(169, 160);
            this.newPasswordTextBox.MaxLength = 20;
            this.newPasswordTextBox.Name = "newPasswordTextBox";
            this.newPasswordTextBox.PasswordChar = '*';
            this.newPasswordTextBox.Required = true;
            this.newPasswordTextBox.Size = new System.Drawing.Size(150, 21);
            this.newPasswordTextBox.TabIndex = 2;
            this.newPasswordTextBox.UseSystemPasswordChar = true;
            this.newPasswordTextBox.ValidationExpression = string.Empty;
            this.newPasswordTextBox.TextChanged += new System.EventHandler(this.newPasswordTextBox_TextChanged);
            this.newPasswordTextBox.Leave += new System.EventHandler(this.newPasswordTextBox_Leave);
            // 
            // confirmNewPasswordTextBox
            // 
            this.confirmNewPasswordTextBox.BackColor = System.Drawing.Color.White;
            this.confirmNewPasswordTextBox.CausesValidation = false;
            this.confirmNewPasswordTextBox.Enabled = false;
            this.confirmNewPasswordTextBox.ErrorMessage = string.Empty;
            this.confirmNewPasswordTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmNewPasswordTextBox.ForeColor = System.Drawing.Color.Black;
            this.confirmNewPasswordTextBox.Location = new System.Drawing.Point(169, 221);
            this.confirmNewPasswordTextBox.MaxLength = 20;
            this.confirmNewPasswordTextBox.Name = "confirmNewPasswordTextBox";
            this.confirmNewPasswordTextBox.PasswordChar = '*';
            this.confirmNewPasswordTextBox.Required = true;
            this.confirmNewPasswordTextBox.Size = new System.Drawing.Size(150, 21);
            this.confirmNewPasswordTextBox.TabIndex = 3;
            this.confirmNewPasswordTextBox.UseSystemPasswordChar = true;
            this.confirmNewPasswordTextBox.ValidationExpression = string.Empty;
            this.confirmNewPasswordTextBox.TextChanged += new System.EventHandler(this.confirmNewPasswordTextBox_TextChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(40, 281);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // submitButton
            // 
            this.submitButton.BackColor = System.Drawing.Color.Transparent;
            this.submitButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.submitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.submitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submitButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.submitButton.FlatAppearance.BorderSize = 0;
            this.submitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submitButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.ForeColor = System.Drawing.Color.White;
            this.submitButton.Location = new System.Drawing.Point(211, 281);
            this.submitButton.Margin = new System.Windows.Forms.Padding(0);
            this.submitButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.submitButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(100, 50);
            this.submitButton.TabIndex = 10;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = false;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // UserChangePassword
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_320_BlueScale;
            this.ClientSize = new System.Drawing.Size(350, 340);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmNewPasswordTextBox);
            this.Controls.Add(this.newPasswordTextBox);
            this.Controls.Add(this.currentPasswordTextBox);
            this.Controls.Add(this.confirmNewPasswordLabel);
            this.Controls.Add(this.newPasswordLabel);
            this.Controls.Add(this.oldPasswordLabel);
            this.Controls.Add(this.changePasswordHeaderLabel);
            this.Name = "UserChangePassword";
            this.Text = "UserChangePassword";
            this.Load += new System.EventHandler(this.UserChangePassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label changePasswordHeaderLabel;
        private CustomLabel oldPasswordLabel;
        private CustomLabel newPasswordLabel;
        private CustomLabel confirmNewPasswordLabel;
        private CustomTextBox currentPasswordTextBox;
        private CustomTextBox newPasswordTextBox;
        private CustomTextBox confirmNewPasswordTextBox;
        private CustomButton cancelButton;
        private CustomButton submitButton;

    }
}
