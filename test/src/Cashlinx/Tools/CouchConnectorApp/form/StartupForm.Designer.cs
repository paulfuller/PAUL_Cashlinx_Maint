namespace form.CouchConsoleApp
{
    partial class CouchConnector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CouchConnector));
            this.couchLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.adminPwdLbl = new System.Windows.Forms.Label();
            this.adminUserLbl = new System.Windows.Forms.Label();
            this.adminPwdTxt = new System.Windows.Forms.TextBox();
            this.connect_cancel = new System.Windows.Forms.Button();
            this.adminUserTxt = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.loginB = new System.Windows.Forms.Button();
            this.pwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.uname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cDBNameTxt = new System.Windows.Forms.TextBox();
            this.cServerPortTxt = new System.Windows.Forms.TextBox();
            this.cServerNameTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // couchLabel
            // 
            this.couchLabel.AutoSize = true;
            this.couchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.couchLabel.Location = new System.Drawing.Point(151, 18);
            this.couchLabel.Name = "couchLabel";
            this.couchLabel.Size = new System.Drawing.Size(256, 29);
            this.couchLabel.TabIndex = 1;
            this.couchLabel.Text = "Couch Connector 2.1";
            this.couchLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.adminPwdLbl);
            this.panel1.Controls.Add(this.adminUserLbl);
            this.panel1.Controls.Add(this.adminPwdTxt);
            this.panel1.Controls.Add(this.connect_cancel);
            this.panel1.Controls.Add(this.adminUserTxt);
            this.panel1.Controls.Add(this.connectButton);
            this.panel1.Controls.Add(this.exitButton);
            this.panel1.Controls.Add(this.loginB);
            this.panel1.Controls.Add(this.pwd);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.uname);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(90, 179);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(392, 205);
            this.panel1.TabIndex = 2;
            // 
            // adminPwdLbl
            // 
            this.adminPwdLbl.AutoSize = true;
            this.adminPwdLbl.Location = new System.Drawing.Point(38, 119);
            this.adminPwdLbl.Name = "adminPwdLbl";
            this.adminPwdLbl.Size = new System.Drawing.Size(85, 13);
            this.adminPwdLbl.TabIndex = 11;
            this.adminPwdLbl.Text = "Admin Password";
            this.adminPwdLbl.Visible = false;
            // 
            // adminUserLbl
            // 
            this.adminUserLbl.AutoSize = true;
            this.adminUserLbl.Location = new System.Drawing.Point(39, 89);
            this.adminUserLbl.Name = "adminUserLbl";
            this.adminUserLbl.Size = new System.Drawing.Size(64, 13);
            this.adminUserLbl.TabIndex = 10;
            this.adminUserLbl.Text = "Admin User:";
            this.adminUserLbl.Visible = false;
            // 
            // adminPwdTxt
            // 
            this.adminPwdTxt.Location = new System.Drawing.Point(156, 116);
            this.adminPwdTxt.Name = "adminPwdTxt";
            this.adminPwdTxt.PasswordChar = '#';
            this.adminPwdTxt.Size = new System.Drawing.Size(160, 20);
            this.adminPwdTxt.TabIndex = 9;
            this.adminPwdTxt.Visible = false;
            // 
            // connect_cancel
            // 
            this.connect_cancel.Location = new System.Drawing.Point(297, 156);
            this.connect_cancel.Name = "connect_cancel";
            this.connect_cancel.Size = new System.Drawing.Size(74, 44);
            this.connect_cancel.TabIndex = 7;
            this.connect_cancel.Text = "Cancel";
            this.connect_cancel.UseVisualStyleBackColor = true;
            this.connect_cancel.Visible = false;
            this.connect_cancel.Click += new System.EventHandler(this.connect_cancel_Click);
            // 
            // adminUserTxt
            // 
            this.adminUserTxt.Location = new System.Drawing.Point(156, 83);
            this.adminUserTxt.Name = "adminUserTxt";
            this.adminUserTxt.Size = new System.Drawing.Size(160, 20);
            this.adminUserTxt.TabIndex = 8;
            this.adminUserTxt.Visible = false;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(206, 156);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(74, 44);
            this.connectButton.TabIndex = 6;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Visible = false;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(297, 156);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(74, 44);
            this.exitButton.TabIndex = 5;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // loginB
            // 
            this.loginB.Location = new System.Drawing.Point(206, 156);
            this.loginB.Name = "loginB";
            this.loginB.Size = new System.Drawing.Size(74, 44);
            this.loginB.TabIndex = 4;
            this.loginB.Text = "Login";
            this.loginB.UseVisualStyleBackColor = true;
            this.loginB.Click += new System.EventHandler(this.loginB_Click);
            // 
            // pwd
            // 
            this.pwd.Location = new System.Drawing.Point(156, 48);
            this.pwd.Name = "pwd";
            this.pwd.PasswordChar = '#';
            this.pwd.Size = new System.Drawing.Size(160, 20);
            this.pwd.TabIndex = 3;
            this.pwd.TextChanged += new System.EventHandler(this.pwd_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // uname
            // 
            this.uname.Location = new System.Drawing.Point(155, 14);
            this.uname.Name = "uname";
            this.uname.Size = new System.Drawing.Size(160, 20);
            this.uname.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "UserName";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cDBNameTxt);
            this.panel2.Controls.Add(this.cServerPortTxt);
            this.panel2.Controls.Add(this.cServerNameTxt);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(90, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(392, 106);
            this.panel2.TabIndex = 3;
            // 
            // cDBNameTxt
            // 
            this.cDBNameTxt.Location = new System.Drawing.Point(156, 77);
            this.cDBNameTxt.Name = "cDBNameTxt";
            this.cDBNameTxt.Size = new System.Drawing.Size(160, 20);
            this.cDBNameTxt.TabIndex = 8;
            // 
            // cServerPortTxt
            // 
            this.cServerPortTxt.Location = new System.Drawing.Point(156, 43);
            this.cServerPortTxt.Name = "cServerPortTxt";
            this.cServerPortTxt.Size = new System.Drawing.Size(160, 20);
            this.cServerPortTxt.TabIndex = 7;
            // 
            // cServerNameTxt
            // 
            this.cServerNameTxt.Location = new System.Drawing.Point(156, 11);
            this.cServerNameTxt.Name = "cServerNameTxt";
            this.cServerNameTxt.Size = new System.Drawing.Size(160, 20);
            this.cServerNameTxt.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Couch DB Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Couch Server port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Couch Server Name";
            // 
            // CouchConnector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(584, 430);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.couchLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CouchConnector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Couch Connector Login";
            this.Load += new System.EventHandler(this.StartupForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label couchLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button loginB;
        private System.Windows.Forms.TextBox pwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox uname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox cDBNameTxt;
        private System.Windows.Forms.TextBox cServerPortTxt;
        private System.Windows.Forms.TextBox cServerNameTxt;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button connect_cancel;
        private System.Windows.Forms.Label adminPwdLbl;
        private System.Windows.Forms.Label adminUserLbl;
        private System.Windows.Forms.TextBox adminPwdTxt;
        private System.Windows.Forms.TextBox adminUserTxt;
    }
}