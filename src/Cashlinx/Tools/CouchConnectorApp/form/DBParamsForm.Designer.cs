namespace CouchConsoleApp.form
{
    partial class DBParamsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBParamsForm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.dbSIDTxt = new System.Windows.Forms.TextBox();
            this.dbServerPortTxt = new System.Windows.Forms.TextBox();
            this.dbServerNameTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dbcancelbutton = new System.Windows.Forms.Button();
            this.dbconnectButton = new System.Windows.Forms.Button();
            this.dbpwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dbuname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dbSIDTxt);
            this.panel2.Controls.Add(this.dbServerPortTxt);
            this.panel2.Controls.Add(this.dbServerNameTxt);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(52, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(392, 120);
            this.panel2.TabIndex = 4;
            // 
            // dbSIDTxt
            // 
            this.dbSIDTxt.Location = new System.Drawing.Point(156, 77);
            this.dbSIDTxt.Name = "dbSIDTxt";
            this.dbSIDTxt.Size = new System.Drawing.Size(160, 20);
            this.dbSIDTxt.TabIndex = 8;
            // 
            // dbServerPortTxt
            // 
            this.dbServerPortTxt.Location = new System.Drawing.Point(156, 43);
            this.dbServerPortTxt.Name = "dbServerPortTxt";
            this.dbServerPortTxt.Size = new System.Drawing.Size(160, 20);
            this.dbServerPortTxt.TabIndex = 7;
            // 
            // dbServerNameTxt
            // 
            this.dbServerNameTxt.Location = new System.Drawing.Point(156, 11);
            this.dbServerNameTxt.Name = "dbServerNameTxt";
            this.dbServerNameTxt.Size = new System.Drawing.Size(160, 20);
            this.dbServerNameTxt.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "DB SID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "DB Server port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "DB Server Name";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dbcancelbutton);
            this.panel1.Controls.Add(this.dbconnectButton);
            this.panel1.Controls.Add(this.dbpwd);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dbuname);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(53, 170);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(391, 153);
            this.panel1.TabIndex = 5;
            // 
            // dbcancelbutton
            // 
            this.dbcancelbutton.Location = new System.Drawing.Point(242, 93);
            this.dbcancelbutton.Name = "dbcancelbutton";
            this.dbcancelbutton.Size = new System.Drawing.Size(74, 44);
            this.dbcancelbutton.TabIndex = 7;
            this.dbcancelbutton.Text = "Cancel";
            this.dbcancelbutton.UseVisualStyleBackColor = true;
            this.dbcancelbutton.Click += new System.EventHandler(this.dbcancelbutton_Click);
            // 
            // dbconnectButton
            // 
            this.dbconnectButton.Location = new System.Drawing.Point(156, 93);
            this.dbconnectButton.Name = "dbconnectButton";
            this.dbconnectButton.Size = new System.Drawing.Size(74, 44);
            this.dbconnectButton.TabIndex = 6;
            this.dbconnectButton.Text = "Connect";
            this.dbconnectButton.UseVisualStyleBackColor = true;
            this.dbconnectButton.Click += new System.EventHandler(this.dbconnectButton_Click);
            // 
            // dbpwd
            // 
            this.dbpwd.Location = new System.Drawing.Point(156, 54);
            this.dbpwd.Name = "dbpwd";
            this.dbpwd.PasswordChar = '#';
            this.dbpwd.Size = new System.Drawing.Size(160, 20);
            this.dbpwd.TabIndex = 3;
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
            // dbuname
            // 
            this.dbuname.Location = new System.Drawing.Point(155, 14);
            this.dbuname.Name = "dbuname";
            this.dbuname.Size = new System.Drawing.Size(160, 20);
            this.dbuname.TabIndex = 1;
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
            // DBParamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(543, 360);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DBParamsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter Database Information";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox dbSIDTxt;
        private System.Windows.Forms.TextBox dbServerPortTxt;
        private System.Windows.Forms.TextBox dbServerNameTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button dbcancelbutton;
        private System.Windows.Forms.Button dbconnectButton;
        private System.Windows.Forms.TextBox dbpwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox dbuname;
        private System.Windows.Forms.Label label2;
    }
}