namespace Common.Libraries.Forms
{
    partial class UserLogin
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
            if(disposing && (components != null))
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
            this.userTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.userLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.loginFormCancelButton = new System.Windows.Forms.Button();
            this.loginFormLoginButton = new System.Windows.Forms.Button();
            this.loginFormLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // userTextBox
            // 
            this.userTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBox.CausesValidation = false;
            this.userTextBox.ForeColor = System.Drawing.Color.Black;
            this.userTextBox.Location = new System.Drawing.Point(91, 68);
            this.userTextBox.MaximumSize = new System.Drawing.Size(141, 21);
            this.userTextBox.MaxLength = 256;
            this.userTextBox.MinimumSize = new System.Drawing.Size(141, 21);
            this.userTextBox.Name = "userTextBox";
            this.userTextBox.ShortcutsEnabled = false;
            this.userTextBox.Size = new System.Drawing.Size(141, 21);
            this.userTextBox.TabIndex = 0;
            this.userTextBox.TextChanged += new System.EventHandler(this.userTextBox_TextChanged);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.passwordTextBox.Enabled = false;
            this.passwordTextBox.ForeColor = System.Drawing.Color.Black;
            this.passwordTextBox.Location = new System.Drawing.Point(91, 108);
            this.passwordTextBox.MaximumSize = new System.Drawing.Size(141, 21);
            this.passwordTextBox.MaxLength = 256;
            this.passwordTextBox.MinimumSize = new System.Drawing.Size(141, 21);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.ShortcutsEnabled = false;
            this.passwordTextBox.Size = new System.Drawing.Size(141, 21);
            this.passwordTextBox.TabIndex = 1;
            this.passwordTextBox.UseSystemPasswordChar = true;
            this.passwordTextBox.TextChanged += new System.EventHandler(this.passwordTextBox_TextChanged);
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.BackColor = System.Drawing.Color.Transparent;
            this.userLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userLabel.ForeColor = System.Drawing.Color.Black;
            this.userLabel.Location = new System.Drawing.Point(56, 71);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(29, 13);
            this.userLabel.TabIndex = 6;
            this.userLabel.Text = "User";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.BackColor = System.Drawing.Color.Transparent;
            this.passwordLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLabel.ForeColor = System.Drawing.Color.Black;
            this.passwordLabel.Location = new System.Drawing.Point(32, 111);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(53, 13);
            this.passwordLabel.TabIndex = 5;
            this.passwordLabel.Text = "Password";
            this.passwordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loginFormCancelButton
            // 
            this.loginFormCancelButton.BackColor = System.Drawing.Color.Transparent;
            this.loginFormCancelButton.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.loginFormCancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.loginFormCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.loginFormCancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.loginFormCancelButton.FlatAppearance.BorderSize = 0;
            this.loginFormCancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.loginFormCancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.loginFormCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginFormCancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginFormCancelButton.ForeColor = System.Drawing.Color.White;
            this.loginFormCancelButton.Location = new System.Drawing.Point(156, 138);
            this.loginFormCancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.loginFormCancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.loginFormCancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.loginFormCancelButton.Name = "loginFormCancelButton";
            this.loginFormCancelButton.Size = new System.Drawing.Size(100, 50);
            this.loginFormCancelButton.TabIndex = 3;
            this.loginFormCancelButton.Text = "&Cancel";
            this.loginFormCancelButton.UseVisualStyleBackColor = false;
            this.loginFormCancelButton.Click += new System.EventHandler(this.loginFormCancelButton_Click);
            // 
            // loginFormLoginButton
            // 
            this.loginFormLoginButton.BackColor = System.Drawing.Color.Transparent;
            this.loginFormLoginButton.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.loginFormLoginButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.loginFormLoginButton.Enabled = false;
            this.loginFormLoginButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.loginFormLoginButton.FlatAppearance.BorderSize = 0;
            this.loginFormLoginButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.loginFormLoginButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.loginFormLoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginFormLoginButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginFormLoginButton.ForeColor = System.Drawing.Color.White;
            this.loginFormLoginButton.Location = new System.Drawing.Point(9, 138);
            this.loginFormLoginButton.Margin = new System.Windows.Forms.Padding(4);
            this.loginFormLoginButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.loginFormLoginButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.loginFormLoginButton.Name = "loginFormLoginButton";
            this.loginFormLoginButton.Size = new System.Drawing.Size(100, 50);
            this.loginFormLoginButton.TabIndex = 2;
            this.loginFormLoginButton.Text = "&Login";
            this.loginFormLoginButton.UseVisualStyleBackColor = false;
            this.loginFormLoginButton.EnabledChanged += new System.EventHandler(this.loginFormLoginButton_EnabledChanged);
            this.loginFormLoginButton.Click += new System.EventHandler(this.loginFormLoginButton_Click);
            // 
            // loginFormLabel
            // 
            this.loginFormLabel.AutoSize = true;
            this.loginFormLabel.BackColor = System.Drawing.Color.Transparent;
            this.loginFormLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginFormLabel.ForeColor = System.Drawing.Color.White;
            this.loginFormLabel.Location = new System.Drawing.Point(108, 12);
            this.loginFormLabel.Name = "loginFormLabel";
            this.loginFormLabel.Size = new System.Drawing.Size(48, 19);
            this.loginFormLabel.TabIndex = 6;
            this.loginFormLabel.Text = "Login";
            this.loginFormLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_320_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.loginFormCancelButton;
            this.ClientSize = new System.Drawing.Size(265, 197);
            this.Controls.Add(this.loginFormLabel);
            this.Controls.Add(this.loginFormLoginButton);
            this.Controls.Add(this.loginFormCancelButton);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userTextBox);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(133, 0);
            this.Name = "UserLogin";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserLogin";
            this.TransparencyKey = System.Drawing.Color.Coral;
            this.Load += new System.EventHandler(this.UserLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Button loginFormCancelButton;
        private System.Windows.Forms.Button loginFormLoginButton;
        private System.Windows.Forms.Label loginFormLabel;
    }
}