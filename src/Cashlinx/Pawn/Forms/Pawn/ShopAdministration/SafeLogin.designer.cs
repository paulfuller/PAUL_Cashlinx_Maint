namespace Pawn.Forms.Pawn.ShopAdministration
{
    partial class SafeLogin
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
            this.safeloginFormCancelButton = new System.Windows.Forms.Button();
            this.safeloginFormLoginButton = new System.Windows.Forms.Button();
            this.loginFormLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // userTextBox
            // 
            this.userTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBox.Enabled = false;
            this.userTextBox.ForeColor = System.Drawing.Color.Black;
            this.userTextBox.Location = new System.Drawing.Point(80, 68);
            this.userTextBox.MaxLength = 12;
            this.userTextBox.Name = "userTextBox";
            this.userTextBox.ReadOnly = true;
            this.userTextBox.Size = new System.Drawing.Size(104, 21);
            this.userTextBox.TabIndex = 0;
            this.userTextBox.Text = "SAFEUSER";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.passwordTextBox.ForeColor = System.Drawing.Color.Black;
            this.passwordTextBox.Location = new System.Drawing.Point(80, 108);
            this.passwordTextBox.MaxLength = 12;
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(104, 21);
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
            this.userLabel.Location = new System.Drawing.Point(45, 71);
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
            this.passwordLabel.Location = new System.Drawing.Point(21, 111);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(53, 13);
            this.passwordLabel.TabIndex = 5;
            this.passwordLabel.Text = "Password";
            this.passwordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // safeloginFormCancelButton
            // 
            this.safeloginFormCancelButton.BackColor = System.Drawing.Color.Transparent;
            this.safeloginFormCancelButton.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.safeloginFormCancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.safeloginFormCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.safeloginFormCancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.safeloginFormCancelButton.FlatAppearance.BorderSize = 0;
            this.safeloginFormCancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.safeloginFormCancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.safeloginFormCancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.safeloginFormCancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.safeloginFormCancelButton.ForeColor = System.Drawing.Color.White;
            this.safeloginFormCancelButton.Location = new System.Drawing.Point(156, 138);
            this.safeloginFormCancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.safeloginFormCancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.safeloginFormCancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.safeloginFormCancelButton.Name = "safeloginFormCancelButton";
            this.safeloginFormCancelButton.Size = new System.Drawing.Size(100, 50);
            this.safeloginFormCancelButton.TabIndex = 3;
            this.safeloginFormCancelButton.Text = "&Cancel";
            this.safeloginFormCancelButton.UseVisualStyleBackColor = false;
            this.safeloginFormCancelButton.Click += new System.EventHandler(this.loginFormCancelButton_Click);
            // 
            // safeloginFormLoginButton
            // 
            this.safeloginFormLoginButton.BackColor = System.Drawing.Color.Transparent;
            this.safeloginFormLoginButton.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.safeloginFormLoginButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.safeloginFormLoginButton.Enabled = false;
            this.safeloginFormLoginButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.safeloginFormLoginButton.FlatAppearance.BorderSize = 0;
            this.safeloginFormLoginButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.safeloginFormLoginButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.safeloginFormLoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.safeloginFormLoginButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.safeloginFormLoginButton.ForeColor = System.Drawing.Color.White;
            this.safeloginFormLoginButton.Location = new System.Drawing.Point(9, 138);
            this.safeloginFormLoginButton.Margin = new System.Windows.Forms.Padding(4);
            this.safeloginFormLoginButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.safeloginFormLoginButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.safeloginFormLoginButton.Name = "safeloginFormLoginButton";
            this.safeloginFormLoginButton.Size = new System.Drawing.Size(100, 50);
            this.safeloginFormLoginButton.TabIndex = 2;
            this.safeloginFormLoginButton.Text = "&Login";
            this.safeloginFormLoginButton.UseVisualStyleBackColor = false;
            this.safeloginFormLoginButton.Click += new System.EventHandler(this.loginFormLoginButton_Click);
            // 
            // loginFormLabel
            // 
            this.loginFormLabel.AutoSize = true;
            this.loginFormLabel.BackColor = System.Drawing.Color.Transparent;
            this.loginFormLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginFormLabel.ForeColor = System.Drawing.Color.White;
            this.loginFormLabel.Location = new System.Drawing.Point(108, 12);
            this.loginFormLabel.Name = "loginFormLabel";
            this.loginFormLabel.Size = new System.Drawing.Size(83, 19);
            this.loginFormLabel.TabIndex = 6;
            this.loginFormLabel.Text = "Safe Login";
            this.loginFormLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SafeLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_320_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.safeloginFormCancelButton;
            this.ClientSize = new System.Drawing.Size(265, 197);
            this.Controls.Add(this.loginFormLabel);
            this.Controls.Add(this.safeloginFormLoginButton);
            this.Controls.Add(this.safeloginFormCancelButton);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userTextBox);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(133, 0);
            this.Name = "SafeLogin";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SafeLogin";
            this.TransparencyKey = System.Drawing.Color.Coral;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Button safeloginFormCancelButton;
        private System.Windows.Forms.Button safeloginFormLoginButton;
        private System.Windows.Forms.Label loginFormLabel;
    }
}