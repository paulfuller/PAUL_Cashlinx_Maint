namespace PawnStoreSetupTool
{
    partial class LDAPSetupForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.ldapLoginTextBox = new System.Windows.Forms.TextBox();
            this.ldapLoginLabel = new System.Windows.Forms.Label();
            this.ldapServerLabel = new System.Windows.Forms.Label();
            this.ldapServerTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ldapPortLabel = new System.Windows.Forms.Label();
            this.ldapPortTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ldapPasswordLabel = new System.Windows.Forms.Label();
            this.ldapPasswordTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pwdPolicyDNLabel = new System.Windows.Forms.Label();
            this.pwdPolicyDNTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.testLDAPCxnButton = new System.Windows.Forms.Button();
            this.userDNLabel = new System.Windows.Forms.Label();
            this.userDNTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.userIdKeyLabel = new System.Windows.Forms.Label();
            this.userIdKeyTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.doneButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.testSearchUserTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.testPasswordSearchTextBox = new System.Windows.Forms.TextBox();
            this.testSearchButton = new System.Windows.Forms.Button();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.testUserSearchLabel = new System.Windows.Forms.Label();
            this.testPasswordSearchLabel = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ldapLoginTextBox
            // 
            this.ldapLoginTextBox.BackColor = System.Drawing.Color.White;
            this.ldapLoginTextBox.Enabled = false;
            this.ldapLoginTextBox.ForeColor = System.Drawing.Color.Black;
            this.ldapLoginTextBox.Location = new System.Drawing.Point(97, 72);
            this.ldapLoginTextBox.Name = "ldapLoginTextBox";
            this.ldapLoginTextBox.Size = new System.Drawing.Size(253, 21);
            this.ldapLoginTextBox.TabIndex = 3;
            this.ldapLoginTextBox.TextChanged += new System.EventHandler(this.ldapLoginTextBox_TextChanged);
            // 
            // ldapLoginLabel
            // 
            this.ldapLoginLabel.AutoSize = true;
            this.ldapLoginLabel.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.ldapLoginLabel.ForeColor = System.Drawing.Color.Red;
            this.ldapLoginLabel.Location = new System.Drawing.Point(358, 75);
            this.ldapLoginLabel.Name = "ldapLoginLabel";
            this.ldapLoginLabel.Size = new System.Drawing.Size(22, 17);
            this.ldapLoginLabel.TabIndex = 57;
            this.ldapLoginLabel.Text = "T";
            // 
            // ldapServerLabel
            // 
            this.ldapServerLabel.AutoSize = true;
            this.ldapServerLabel.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.ldapServerLabel.ForeColor = System.Drawing.Color.Red;
            this.ldapServerLabel.Location = new System.Drawing.Point(358, 16);
            this.ldapServerLabel.Name = "ldapServerLabel";
            this.ldapServerLabel.Size = new System.Drawing.Size(22, 17);
            this.ldapServerLabel.TabIndex = 60;
            this.ldapServerLabel.Text = "T";
            // 
            // ldapServerTextBox
            // 
            this.ldapServerTextBox.BackColor = System.Drawing.Color.White;
            this.ldapServerTextBox.ForeColor = System.Drawing.Color.Black;
            this.ldapServerTextBox.Location = new System.Drawing.Point(97, 13);
            this.ldapServerTextBox.Name = "ldapServerTextBox";
            this.ldapServerTextBox.Size = new System.Drawing.Size(253, 21);
            this.ldapServerTextBox.TabIndex = 1;
            this.ldapServerTextBox.TextChanged += new System.EventHandler(this.ldapServerTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 58;
            this.label3.Text = "LDAP Server:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ldapPortLabel
            // 
            this.ldapPortLabel.AutoSize = true;
            this.ldapPortLabel.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.ldapPortLabel.ForeColor = System.Drawing.Color.Red;
            this.ldapPortLabel.Location = new System.Drawing.Point(358, 46);
            this.ldapPortLabel.Name = "ldapPortLabel";
            this.ldapPortLabel.Size = new System.Drawing.Size(22, 17);
            this.ldapPortLabel.TabIndex = 63;
            this.ldapPortLabel.Text = "T";
            // 
            // ldapPortTextBox
            // 
            this.ldapPortTextBox.BackColor = System.Drawing.Color.White;
            this.ldapPortTextBox.Enabled = false;
            this.ldapPortTextBox.ForeColor = System.Drawing.Color.Black;
            this.ldapPortTextBox.Location = new System.Drawing.Point(97, 43);
            this.ldapPortTextBox.Name = "ldapPortTextBox";
            this.ldapPortTextBox.Size = new System.Drawing.Size(253, 21);
            this.ldapPortTextBox.TabIndex = 2;
            this.ldapPortTextBox.TextChanged += new System.EventHandler(this.ldapPortTextBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 61;
            this.label4.Text = "LDAP Port:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ldapPasswordLabel
            // 
            this.ldapPasswordLabel.AutoSize = true;
            this.ldapPasswordLabel.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.ldapPasswordLabel.ForeColor = System.Drawing.Color.Red;
            this.ldapPasswordLabel.Location = new System.Drawing.Point(358, 103);
            this.ldapPasswordLabel.Name = "ldapPasswordLabel";
            this.ldapPasswordLabel.Size = new System.Drawing.Size(22, 17);
            this.ldapPasswordLabel.TabIndex = 66;
            this.ldapPasswordLabel.Text = "T";
            // 
            // ldapPasswordTextBox
            // 
            this.ldapPasswordTextBox.BackColor = System.Drawing.Color.White;
            this.ldapPasswordTextBox.Enabled = false;
            this.ldapPasswordTextBox.ForeColor = System.Drawing.Color.Black;
            this.ldapPasswordTextBox.Location = new System.Drawing.Point(97, 100);
            this.ldapPasswordTextBox.Name = "ldapPasswordTextBox";
            this.ldapPasswordTextBox.Size = new System.Drawing.Size(253, 21);
            this.ldapPasswordTextBox.TabIndex = 4;
            this.ldapPasswordTextBox.UseSystemPasswordChar = true;
            this.ldapPasswordTextBox.TextChanged += new System.EventHandler(this.ldapPasswordTextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 64;
            this.label5.Text = "Password:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pwdPolicyDNLabel
            // 
            this.pwdPolicyDNLabel.AutoSize = true;
            this.pwdPolicyDNLabel.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.pwdPolicyDNLabel.ForeColor = System.Drawing.Color.Red;
            this.pwdPolicyDNLabel.Location = new System.Drawing.Point(358, 188);
            this.pwdPolicyDNLabel.Name = "pwdPolicyDNLabel";
            this.pwdPolicyDNLabel.Size = new System.Drawing.Size(22, 17);
            this.pwdPolicyDNLabel.TabIndex = 69;
            this.pwdPolicyDNLabel.Text = "T";
            // 
            // pwdPolicyDNTextBox
            // 
            this.pwdPolicyDNTextBox.BackColor = System.Drawing.Color.White;
            this.pwdPolicyDNTextBox.Enabled = false;
            this.pwdPolicyDNTextBox.ForeColor = System.Drawing.Color.Black;
            this.pwdPolicyDNTextBox.Location = new System.Drawing.Point(97, 186);
            this.pwdPolicyDNTextBox.Name = "pwdPolicyDNTextBox";
            this.pwdPolicyDNTextBox.Size = new System.Drawing.Size(253, 21);
            this.pwdPolicyDNTextBox.TabIndex = 6;
            this.pwdPolicyDNTextBox.TextChanged += new System.EventHandler(this.pwdPolicyDNTextBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 26);
            this.label6.TabIndex = 67;
            this.label6.Text = "Password \r\nPolicy DN:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // testLDAPCxnButton
            // 
            this.testLDAPCxnButton.Enabled = false;
            this.testLDAPCxnButton.Location = new System.Drawing.Point(242, 127);
            this.testLDAPCxnButton.Name = "testLDAPCxnButton";
            this.testLDAPCxnButton.Size = new System.Drawing.Size(110, 40);
            this.testLDAPCxnButton.TabIndex = 5;
            this.testLDAPCxnButton.Text = "Test Connection";
            this.testLDAPCxnButton.UseVisualStyleBackColor = true;
            this.testLDAPCxnButton.Click += new System.EventHandler(this.testLDAPCxnButton_Click);
            // 
            // userDNLabel
            // 
            this.userDNLabel.AutoSize = true;
            this.userDNLabel.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.userDNLabel.ForeColor = System.Drawing.Color.Red;
            this.userDNLabel.Location = new System.Drawing.Point(358, 222);
            this.userDNLabel.Name = "userDNLabel";
            this.userDNLabel.Size = new System.Drawing.Size(22, 17);
            this.userDNLabel.TabIndex = 73;
            this.userDNLabel.Text = "T";
            // 
            // userDNTextBox
            // 
            this.userDNTextBox.BackColor = System.Drawing.Color.White;
            this.userDNTextBox.Enabled = false;
            this.userDNTextBox.ForeColor = System.Drawing.Color.Black;
            this.userDNTextBox.Location = new System.Drawing.Point(97, 220);
            this.userDNTextBox.Name = "userDNTextBox";
            this.userDNTextBox.Size = new System.Drawing.Size(253, 21);
            this.userDNTextBox.TabIndex = 7;
            this.userDNTextBox.TextChanged += new System.EventHandler(this.userDNTextBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 224);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 71;
            this.label7.Text = "User DN:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // userIdKeyLabel
            // 
            this.userIdKeyLabel.AutoSize = true;
            this.userIdKeyLabel.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.userIdKeyLabel.ForeColor = System.Drawing.Color.Red;
            this.userIdKeyLabel.Location = new System.Drawing.Point(358, 258);
            this.userIdKeyLabel.Name = "userIdKeyLabel";
            this.userIdKeyLabel.Size = new System.Drawing.Size(22, 17);
            this.userIdKeyLabel.TabIndex = 76;
            this.userIdKeyLabel.Text = "T";
            // 
            // userIdKeyTextBox
            // 
            this.userIdKeyTextBox.BackColor = System.Drawing.Color.White;
            this.userIdKeyTextBox.Enabled = false;
            this.userIdKeyTextBox.ForeColor = System.Drawing.Color.Black;
            this.userIdKeyTextBox.Location = new System.Drawing.Point(97, 256);
            this.userIdKeyTextBox.Name = "userIdKeyTextBox";
            this.userIdKeyTextBox.Size = new System.Drawing.Size(253, 21);
            this.userIdKeyTextBox.TabIndex = 8;
            this.userIdKeyTextBox.TextChanged += new System.EventHandler(this.userIdKeyTextBox_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(44, 253);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 26);
            this.label8.TabIndex = 74;
            this.label8.Text = "     User \r\nId Key:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // doneButton
            // 
            this.doneButton.Location = new System.Drawing.Point(242, 436);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(110, 40);
            this.doneButton.TabIndex = 12;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(58, 309);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "User:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // testSearchUserTextBox
            // 
            this.testSearchUserTextBox.BackColor = System.Drawing.Color.White;
            this.testSearchUserTextBox.Enabled = false;
            this.testSearchUserTextBox.ForeColor = System.Drawing.Color.Black;
            this.testSearchUserTextBox.Location = new System.Drawing.Point(97, 305);
            this.testSearchUserTextBox.Name = "testSearchUserTextBox";
            this.testSearchUserTextBox.Size = new System.Drawing.Size(253, 21);
            this.testSearchUserTextBox.TabIndex = 9;
            this.testSearchUserTextBox.TextChanged += new System.EventHandler(this.testSearchUserTextBox_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(34, 342);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 79;
            this.label10.Text = "Password:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // testPasswordSearchTextBox
            // 
            this.testPasswordSearchTextBox.BackColor = System.Drawing.Color.White;
            this.testPasswordSearchTextBox.Enabled = false;
            this.testPasswordSearchTextBox.ForeColor = System.Drawing.Color.Black;
            this.testPasswordSearchTextBox.Location = new System.Drawing.Point(97, 338);
            this.testPasswordSearchTextBox.Name = "testPasswordSearchTextBox";
            this.testPasswordSearchTextBox.Size = new System.Drawing.Size(253, 21);
            this.testPasswordSearchTextBox.TabIndex = 10;
            this.testPasswordSearchTextBox.UseSystemPasswordChar = true;
            this.testPasswordSearchTextBox.TextChanged += new System.EventHandler(this.testPasswordSearchTextBox_TextChanged);
            // 
            // testSearchButton
            // 
            this.testSearchButton.Enabled = false;
            this.testSearchButton.Location = new System.Drawing.Point(242, 365);
            this.testSearchButton.Name = "testSearchButton";
            this.testSearchButton.Size = new System.Drawing.Size(110, 40);
            this.testSearchButton.TabIndex = 11;
            this.testSearchButton.Text = "Test Search";
            this.testSearchButton.UseVisualStyleBackColor = true;
            this.testSearchButton.Click += new System.EventHandler(this.testSearchButton_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Gray;
            this.pictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox6.InitialImage = null;
            this.pictureBox6.Location = new System.Drawing.Point(10, 288);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox6.MaximumSize = new System.Drawing.Size(368, 2);
            this.pictureBox6.MinimumSize = new System.Drawing.Size(368, 2);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(368, 2);
            this.pictureBox6.TabIndex = 81;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gray;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(10, 417);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.MaximumSize = new System.Drawing.Size(368, 2);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(368, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(368, 2);
            this.pictureBox1.TabIndex = 82;
            this.pictureBox1.TabStop = false;
            // 
            // testUserSearchLabel
            // 
            this.testUserSearchLabel.AutoSize = true;
            this.testUserSearchLabel.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.testUserSearchLabel.ForeColor = System.Drawing.Color.Red;
            this.testUserSearchLabel.Location = new System.Drawing.Point(358, 307);
            this.testUserSearchLabel.Name = "testUserSearchLabel";
            this.testUserSearchLabel.Size = new System.Drawing.Size(22, 17);
            this.testUserSearchLabel.TabIndex = 83;
            this.testUserSearchLabel.Text = "T";
            // 
            // testPasswordSearchLabel
            // 
            this.testPasswordSearchLabel.AutoSize = true;
            this.testPasswordSearchLabel.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.testPasswordSearchLabel.ForeColor = System.Drawing.Color.Red;
            this.testPasswordSearchLabel.Location = new System.Drawing.Point(358, 340);
            this.testPasswordSearchLabel.Name = "testPasswordSearchLabel";
            this.testPasswordSearchLabel.Size = new System.Drawing.Size(22, 17);
            this.testPasswordSearchLabel.TabIndex = 84;
            this.testPasswordSearchLabel.Text = "T";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Gray;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(12, 171);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.MaximumSize = new System.Drawing.Size(368, 2);
            this.pictureBox2.MinimumSize = new System.Drawing.Size(368, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(368, 2);
            this.pictureBox2.TabIndex = 85;
            this.pictureBox2.TabStop = false;
            // 
            // LDAPSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(389, 488);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.testPasswordSearchLabel);
            this.Controls.Add(this.testUserSearchLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.testSearchButton);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.testPasswordSearchTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.userIdKeyLabel);
            this.Controls.Add(this.testSearchUserTextBox);
            this.Controls.Add(this.userIdKeyTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.userDNLabel);
            this.Controls.Add(this.userDNTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.testLDAPCxnButton);
            this.Controls.Add(this.pwdPolicyDNLabel);
            this.Controls.Add(this.pwdPolicyDNTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ldapPasswordLabel);
            this.Controls.Add(this.ldapPasswordTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ldapPortLabel);
            this.Controls.Add(this.ldapPortTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ldapServerLabel);
            this.Controls.Add(this.ldapServerTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ldapLoginLabel);
            this.Controls.Add(this.ldapLoginTextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LDAPSetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LDAP Setup";
            this.Load += new System.EventHandler(this.LDAPSetupForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ldapLoginTextBox;
        private System.Windows.Forms.Label ldapLoginLabel;
        private System.Windows.Forms.Label ldapServerLabel;
        private System.Windows.Forms.TextBox ldapServerTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ldapPortLabel;
        private System.Windows.Forms.TextBox ldapPortTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label ldapPasswordLabel;
        private System.Windows.Forms.TextBox ldapPasswordTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label pwdPolicyDNLabel;
        private System.Windows.Forms.TextBox pwdPolicyDNTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button testLDAPCxnButton;
        private System.Windows.Forms.Label userDNLabel;
        private System.Windows.Forms.TextBox userDNTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label userIdKeyLabel;
        private System.Windows.Forms.TextBox userIdKeyTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox testSearchUserTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.TextBox testPasswordSearchTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button testSearchButton;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label testUserSearchLabel;
        private System.Windows.Forms.Label testPasswordSearchLabel;
        private System.Windows.Forms.PictureBox pictureBox2;

    }
}