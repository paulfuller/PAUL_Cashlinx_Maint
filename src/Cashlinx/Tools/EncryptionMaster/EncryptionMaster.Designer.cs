namespace EncryptionMaster
{
    partial class EncryptionMaster
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
            this.publicSeedLabel = new System.Windows.Forms.Label();
            this.publicSeedTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.encryptedKeyTextBox = new System.Windows.Forms.TextBox();
            this.saveWarningLabel = new System.Windows.Forms.Label();
            this.generateKeyButton = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.valueLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.publicKeyLabel = new System.Windows.Forms.Label();
            this.publicKeyTextBox = new System.Windows.Forms.TextBox();
            this.nameValueTextBox = new System.Windows.Forms.RichTextBox();
            this.schemaTextBox = new System.Windows.Forms.TextBox();
            this.schemaLabel = new System.Windows.Forms.Label();
            this.serviceTextBox = new System.Windows.Forms.TextBox();
            this.serviceLabel = new System.Windows.Forms.Label();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.hostLabel = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.userIdTextBox = new System.Windows.Forms.TextBox();
            this.userIdLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.reqLabel1 = new System.Windows.Forms.Label();
            this.reqLabel2 = new System.Windows.Forms.Label();
            this.reqLabel3 = new System.Windows.Forms.Label();
            this.reqLabel4 = new System.Windows.Forms.Label();
            this.reqLabel5 = new System.Windows.Forms.Label();
            this.reqLabel6 = new System.Windows.Forms.Label();
            this.reqLabel7 = new System.Windows.Forms.Label();
            this.decryptButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // publicSeedLabel
            // 
            this.publicSeedLabel.AutoSize = true;
            this.publicSeedLabel.ForeColor = System.Drawing.Color.Black;
            this.publicSeedLabel.Location = new System.Drawing.Point(24, 13);
            this.publicSeedLabel.Name = "publicSeedLabel";
            this.publicSeedLabel.Size = new System.Drawing.Size(61, 13);
            this.publicSeedLabel.TabIndex = 0;
            this.publicSeedLabel.Text = "Public Seed";
            // 
            // publicSeedTextBox
            // 
            this.publicSeedTextBox.BackColor = System.Drawing.Color.White;
            this.publicSeedTextBox.ForeColor = System.Drawing.Color.SteelBlue;
            this.publicSeedTextBox.Location = new System.Drawing.Point(105, 10);
            this.publicSeedTextBox.Name = "publicSeedTextBox";
            this.publicSeedTextBox.Size = new System.Drawing.Size(250, 21);
            this.publicSeedTextBox.TabIndex = 1;
            this.publicSeedTextBox.TextChanged += new System.EventHandler(this.publicSeedTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(24, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Encrypted Key";
            // 
            // encryptedKeyTextBox
            // 
            this.encryptedKeyTextBox.BackColor = System.Drawing.Color.White;
            this.encryptedKeyTextBox.ForeColor = System.Drawing.Color.SteelBlue;
            this.encryptedKeyTextBox.Location = new System.Drawing.Point(105, 234);
            this.encryptedKeyTextBox.Name = "encryptedKeyTextBox";
            this.encryptedKeyTextBox.ReadOnly = true;
            this.encryptedKeyTextBox.Size = new System.Drawing.Size(331, 21);
            this.encryptedKeyTextBox.TabIndex = 10;
            // 
            // saveWarningLabel
            // 
            this.saveWarningLabel.AutoSize = true;
            this.saveWarningLabel.ForeColor = System.Drawing.Color.Red;
            this.saveWarningLabel.Location = new System.Drawing.Point(72, 258);
            this.saveWarningLabel.Name = "saveWarningLabel";
            this.saveWarningLabel.Size = new System.Drawing.Size(283, 13);
            this.saveWarningLabel.TabIndex = 6;
            this.saveWarningLabel.Text = "Save Encrypted Key in GLOBALCONFIG.DATAPUBLICKEY!";
            this.saveWarningLabel.Visible = false;
            // 
            // generateKeyButton
            // 
            this.generateKeyButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.generateKeyButton.ForeColor = System.Drawing.Color.Black;
            this.generateKeyButton.Location = new System.Drawing.Point(361, 171);
            this.generateKeyButton.Name = "generateKeyButton";
            this.generateKeyButton.Size = new System.Drawing.Size(75, 23);
            this.generateKeyButton.TabIndex = 8;
            this.generateKeyButton.Text = "Generate Key";
            this.generateKeyButton.UseVisualStyleBackColor = false;
            this.generateKeyButton.Click += new System.EventHandler(this.generateKeyButton_Click);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.ForeColor = System.Drawing.Color.Black;
            this.nameLabel.Location = new System.Drawing.Point(24, 301);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(34, 13);
            this.nameLabel.TabIndex = 8;
            this.nameLabel.Text = "Name";
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = true;
            this.valueLabel.ForeColor = System.Drawing.Color.Black;
            this.valueLabel.Location = new System.Drawing.Point(24, 325);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(33, 13);
            this.valueLabel.TabIndex = 9;
            this.valueLabel.Text = "Value";
            // 
            // nameTextBox
            // 
            this.nameTextBox.BackColor = System.Drawing.Color.White;
            this.nameTextBox.ForeColor = System.Drawing.Color.SteelBlue;
            this.nameTextBox.Location = new System.Drawing.Point(65, 298);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(348, 21);
            this.nameTextBox.TabIndex = 11;
            // 
            // valueTextBox
            // 
            this.valueTextBox.BackColor = System.Drawing.Color.White;
            this.valueTextBox.ForeColor = System.Drawing.Color.SteelBlue;
            this.valueTextBox.Location = new System.Drawing.Point(65, 322);
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(348, 21);
            this.valueTextBox.TabIndex = 12;
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.addButton.Enabled = false;
            this.addButton.ForeColor = System.Drawing.Color.Black;
            this.addButton.Location = new System.Drawing.Point(419, 321);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 13;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.saveButton.Enabled = false;
            this.saveButton.ForeColor = System.Drawing.Color.Black;
            this.saveButton.Location = new System.Drawing.Point(428, 645);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 14;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // publicKeyLabel
            // 
            this.publicKeyLabel.AutoSize = true;
            this.publicKeyLabel.ForeColor = System.Drawing.Color.Black;
            this.publicKeyLabel.Location = new System.Drawing.Point(24, 209);
            this.publicKeyLabel.Name = "publicKeyLabel";
            this.publicKeyLabel.Size = new System.Drawing.Size(55, 13);
            this.publicKeyLabel.TabIndex = 2;
            this.publicKeyLabel.Text = "Public Key";
            // 
            // publicKeyTextBox
            // 
            this.publicKeyTextBox.BackColor = System.Drawing.Color.White;
            this.publicKeyTextBox.ForeColor = System.Drawing.Color.SteelBlue;
            this.publicKeyTextBox.Location = new System.Drawing.Point(105, 206);
            this.publicKeyTextBox.Name = "publicKeyTextBox";
            this.publicKeyTextBox.ReadOnly = true;
            this.publicKeyTextBox.Size = new System.Drawing.Size(331, 21);
            this.publicKeyTextBox.TabIndex = 9;
            // 
            // nameValueTextBox
            // 
            this.nameValueTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameValueTextBox.ForeColor = System.Drawing.Color.SteelBlue;
            this.nameValueTextBox.Location = new System.Drawing.Point(27, 366);
            this.nameValueTextBox.Name = "nameValueTextBox";
            this.nameValueTextBox.ReadOnly = true;
            this.nameValueTextBox.Size = new System.Drawing.Size(476, 273);
            this.nameValueTextBox.TabIndex = 20;
            this.nameValueTextBox.TabStop = false;
            this.nameValueTextBox.Text = "";
            // 
            // schemaTextBox
            // 
            this.schemaTextBox.BackColor = System.Drawing.Color.White;
            this.schemaTextBox.ForeColor = System.Drawing.Color.SteelBlue;
            this.schemaTextBox.Location = new System.Drawing.Point(105, 37);
            this.schemaTextBox.Name = "schemaTextBox";
            this.schemaTextBox.Size = new System.Drawing.Size(250, 21);
            this.schemaTextBox.TabIndex = 2;
            this.schemaTextBox.TextChanged += new System.EventHandler(this.schemaTextBox_TextChanged);
            // 
            // schemaLabel
            // 
            this.schemaLabel.AutoSize = true;
            this.schemaLabel.ForeColor = System.Drawing.Color.Black;
            this.schemaLabel.Location = new System.Drawing.Point(24, 40);
            this.schemaLabel.Name = "schemaLabel";
            this.schemaLabel.Size = new System.Drawing.Size(44, 13);
            this.schemaLabel.TabIndex = 16;
            this.schemaLabel.Text = "Schema";
            // 
            // serviceTextBox
            // 
            this.serviceTextBox.BackColor = System.Drawing.Color.White;
            this.serviceTextBox.ForeColor = System.Drawing.Color.SteelBlue;
            this.serviceTextBox.Location = new System.Drawing.Point(105, 64);
            this.serviceTextBox.Name = "serviceTextBox";
            this.serviceTextBox.Size = new System.Drawing.Size(250, 21);
            this.serviceTextBox.TabIndex = 3;
            this.serviceTextBox.TextChanged += new System.EventHandler(this.serviceTextBox_TextChanged);
            // 
            // serviceLabel
            // 
            this.serviceLabel.AutoSize = true;
            this.serviceLabel.ForeColor = System.Drawing.Color.Black;
            this.serviceLabel.Location = new System.Drawing.Point(24, 67);
            this.serviceLabel.Name = "serviceLabel";
            this.serviceLabel.Size = new System.Drawing.Size(42, 13);
            this.serviceLabel.TabIndex = 18;
            this.serviceLabel.Text = "Service";
            // 
            // hostTextBox
            // 
            this.hostTextBox.BackColor = System.Drawing.Color.White;
            this.hostTextBox.ForeColor = System.Drawing.Color.SteelBlue;
            this.hostTextBox.Location = new System.Drawing.Point(105, 91);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(250, 21);
            this.hostTextBox.TabIndex = 4;
            this.hostTextBox.TextChanged += new System.EventHandler(this.hostTextBox_TextChanged);
            // 
            // hostLabel
            // 
            this.hostLabel.AutoSize = true;
            this.hostLabel.ForeColor = System.Drawing.Color.Black;
            this.hostLabel.Location = new System.Drawing.Point(24, 94);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(29, 13);
            this.hostLabel.TabIndex = 20;
            this.hostLabel.Text = "Host";
            // 
            // portTextBox
            // 
            this.portTextBox.BackColor = System.Drawing.Color.White;
            this.portTextBox.ForeColor = System.Drawing.Color.SteelBlue;
            this.portTextBox.Location = new System.Drawing.Point(105, 118);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(250, 21);
            this.portTextBox.TabIndex = 5;
            this.portTextBox.TextChanged += new System.EventHandler(this.portTextBox_TextChanged);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.ForeColor = System.Drawing.Color.Black;
            this.portLabel.Location = new System.Drawing.Point(24, 121);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(27, 13);
            this.portLabel.TabIndex = 22;
            this.portLabel.Text = "Port";
            // 
            // userIdTextBox
            // 
            this.userIdTextBox.BackColor = System.Drawing.Color.White;
            this.userIdTextBox.ForeColor = System.Drawing.Color.SteelBlue;
            this.userIdTextBox.Location = new System.Drawing.Point(105, 145);
            this.userIdTextBox.Name = "userIdTextBox";
            this.userIdTextBox.Size = new System.Drawing.Size(250, 21);
            this.userIdTextBox.TabIndex = 6;
            this.userIdTextBox.TextChanged += new System.EventHandler(this.userIdTextBox_TextChanged);
            // 
            // userIdLabel
            // 
            this.userIdLabel.AutoSize = true;
            this.userIdLabel.ForeColor = System.Drawing.Color.Black;
            this.userIdLabel.Location = new System.Drawing.Point(24, 148);
            this.userIdLabel.Name = "userIdLabel";
            this.userIdLabel.Size = new System.Drawing.Size(42, 13);
            this.userIdLabel.TabIndex = 24;
            this.userIdLabel.Text = "User Id";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.BackColor = System.Drawing.Color.White;
            this.passwordTextBox.ForeColor = System.Drawing.Color.SteelBlue;
            this.passwordTextBox.Location = new System.Drawing.Point(105, 172);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(250, 21);
            this.passwordTextBox.TabIndex = 7;
            this.passwordTextBox.TextChanged += new System.EventHandler(this.passwordTextBox_TextChanged);
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.ForeColor = System.Drawing.Color.Black;
            this.passwordLabel.Location = new System.Drawing.Point(24, 176);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(53, 13);
            this.passwordLabel.TabIndex = 26;
            this.passwordLabel.Text = "Password";
            // 
            // reqLabel1
            // 
            this.reqLabel1.AutoSize = true;
            this.reqLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reqLabel1.ForeColor = System.Drawing.Color.Red;
            this.reqLabel1.Location = new System.Drawing.Point(12, 13);
            this.reqLabel1.Name = "reqLabel1";
            this.reqLabel1.Size = new System.Drawing.Size(13, 13);
            this.reqLabel1.TabIndex = 28;
            this.reqLabel1.Text = "*";
            // 
            // reqLabel2
            // 
            this.reqLabel2.AutoSize = true;
            this.reqLabel2.ForeColor = System.Drawing.Color.Red;
            this.reqLabel2.Location = new System.Drawing.Point(12, 40);
            this.reqLabel2.Name = "reqLabel2";
            this.reqLabel2.Size = new System.Drawing.Size(13, 13);
            this.reqLabel2.TabIndex = 29;
            this.reqLabel2.Text = "*";
            // 
            // reqLabel3
            // 
            this.reqLabel3.AutoSize = true;
            this.reqLabel3.ForeColor = System.Drawing.Color.Red;
            this.reqLabel3.Location = new System.Drawing.Point(12, 67);
            this.reqLabel3.Name = "reqLabel3";
            this.reqLabel3.Size = new System.Drawing.Size(13, 13);
            this.reqLabel3.TabIndex = 30;
            this.reqLabel3.Text = "*";
            // 
            // reqLabel4
            // 
            this.reqLabel4.AutoSize = true;
            this.reqLabel4.ForeColor = System.Drawing.Color.Red;
            this.reqLabel4.Location = new System.Drawing.Point(12, 94);
            this.reqLabel4.Name = "reqLabel4";
            this.reqLabel4.Size = new System.Drawing.Size(13, 13);
            this.reqLabel4.TabIndex = 31;
            this.reqLabel4.Text = "*";
            // 
            // reqLabel5
            // 
            this.reqLabel5.AutoSize = true;
            this.reqLabel5.ForeColor = System.Drawing.Color.Red;
            this.reqLabel5.Location = new System.Drawing.Point(12, 121);
            this.reqLabel5.Name = "reqLabel5";
            this.reqLabel5.Size = new System.Drawing.Size(13, 13);
            this.reqLabel5.TabIndex = 32;
            this.reqLabel5.Text = "*";
            // 
            // reqLabel6
            // 
            this.reqLabel6.AutoSize = true;
            this.reqLabel6.ForeColor = System.Drawing.Color.Red;
            this.reqLabel6.Location = new System.Drawing.Point(12, 148);
            this.reqLabel6.Name = "reqLabel6";
            this.reqLabel6.Size = new System.Drawing.Size(13, 13);
            this.reqLabel6.TabIndex = 33;
            this.reqLabel6.Text = "*";
            // 
            // reqLabel7
            // 
            this.reqLabel7.AutoSize = true;
            this.reqLabel7.ForeColor = System.Drawing.Color.Red;
            this.reqLabel7.Location = new System.Drawing.Point(12, 176);
            this.reqLabel7.Name = "reqLabel7";
            this.reqLabel7.Size = new System.Drawing.Size(13, 13);
            this.reqLabel7.TabIndex = 34;
            this.reqLabel7.Text = "*";
            // 
            // decryptButton
            // 
            this.decryptButton.Location = new System.Drawing.Point(428, 57);
            this.decryptButton.Name = "decryptButton";
            this.decryptButton.Size = new System.Drawing.Size(75, 23);
            this.decryptButton.TabIndex = 35;
            this.decryptButton.Text = "Decrypt";
            this.decryptButton.UseVisualStyleBackColor = true;
            this.decryptButton.Click += new System.EventHandler(this.decryptButton_Click);
            // 
            // EncryptionMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(533, 678);
            this.Controls.Add(this.decryptButton);
            this.Controls.Add(this.reqLabel7);
            this.Controls.Add(this.reqLabel6);
            this.Controls.Add(this.reqLabel5);
            this.Controls.Add(this.reqLabel4);
            this.Controls.Add(this.reqLabel3);
            this.Controls.Add(this.reqLabel2);
            this.Controls.Add(this.reqLabel1);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.userIdTextBox);
            this.Controls.Add(this.userIdLabel);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.hostTextBox);
            this.Controls.Add(this.hostLabel);
            this.Controls.Add(this.serviceTextBox);
            this.Controls.Add(this.serviceLabel);
            this.Controls.Add(this.schemaTextBox);
            this.Controls.Add(this.schemaLabel);
            this.Controls.Add(this.nameValueTextBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.valueTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.valueLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.generateKeyButton);
            this.Controls.Add(this.saveWarningLabel);
            this.Controls.Add(this.encryptedKeyTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.publicKeyTextBox);
            this.Controls.Add(this.publicKeyLabel);
            this.Controls.Add(this.publicSeedTextBox);
            this.Controls.Add(this.publicSeedLabel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "EncryptionMaster";
            this.Text = "EncryptionMaster";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label publicSeedLabel;
        private System.Windows.Forms.TextBox publicSeedTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox encryptedKeyTextBox;
        private System.Windows.Forms.Label saveWarningLabel;
        private System.Windows.Forms.Button generateKeyButton;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label publicKeyLabel;
        private System.Windows.Forms.TextBox publicKeyTextBox;
        private System.Windows.Forms.RichTextBox nameValueTextBox;
        private System.Windows.Forms.TextBox schemaTextBox;
        private System.Windows.Forms.Label schemaLabel;
        private System.Windows.Forms.TextBox serviceTextBox;
        private System.Windows.Forms.Label serviceLabel;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox userIdTextBox;
        private System.Windows.Forms.Label userIdLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label reqLabel1;
        private System.Windows.Forms.Label reqLabel2;
        private System.Windows.Forms.Label reqLabel3;
        private System.Windows.Forms.Label reqLabel4;
        private System.Windows.Forms.Label reqLabel5;
        private System.Windows.Forms.Label reqLabel6;
        private System.Windows.Forms.Label reqLabel7;
        private System.Windows.Forms.Button decryptButton;
    }
}

