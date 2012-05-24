namespace PawnStoreSetupTool
{
    partial class ESBSetupForm
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
            this.esbServiceComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.hostNameTextBox = new System.Windows.Forms.TextBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.endPointURITextBox = new System.Windows.Forms.TextBox();
            this.hostNameLabel = new System.Windows.Forms.Label();
            this.portLabel = new System.Windows.Forms.Label();
            this.endPointURILabel = new System.Windows.Forms.Label();
            this.doneButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.addServiceButton = new System.Windows.Forms.Button();
            this.editServiceButton = new System.Windows.Forms.Button();
            this.existingEsbServicesListBox = new System.Windows.Forms.ListView();
            this.typeColumn = new System.Windows.Forms.ColumnHeader();
            this.hostNameColumn = new System.Windows.Forms.ColumnHeader();
            this.portColumn = new System.Windows.Forms.ColumnHeader();
            this.domainColumn = new System.Windows.Forms.ColumnHeader();
            this.uriColumn = new System.Windows.Forms.ColumnHeader();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.domainTextBox = new System.Windows.Forms.TextBox();
            this.domainLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.endPointNameTextBox = new System.Windows.Forms.TextBox();
            this.endPointNameLabel = new System.Windows.Forms.Label();
            this.endPointName = new System.Windows.Forms.ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "ESB Service Type:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // esbServiceComboBox
            // 
            this.esbServiceComboBox.BackColor = System.Drawing.Color.Gainsboro;
            this.esbServiceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.esbServiceComboBox.ForeColor = System.Drawing.Color.Black;
            this.esbServiceComboBox.FormattingEnabled = true;
            this.esbServiceComboBox.Items.AddRange(new object[] {
            "Address Service ",
            "ProKnow Service ",
            "MDSE Transfer Service"});
            this.esbServiceComboBox.Location = new System.Drawing.Point(122, 9);
            this.esbServiceComboBox.Name = "esbServiceComboBox";
            this.esbServiceComboBox.Size = new System.Drawing.Size(390, 24);
            this.esbServiceComboBox.TabIndex = 1;
            this.esbServiceComboBox.SelectedIndexChanged += new System.EventHandler(this.esbServiceComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(41, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Host Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(80, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Port:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(26, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "End Point URI:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // hostNameTextBox
            // 
            this.hostNameTextBox.Enabled = false;
            this.hostNameTextBox.Location = new System.Drawing.Point(122, 37);
            this.hostNameTextBox.Name = "hostNameTextBox";
            this.hostNameTextBox.Size = new System.Drawing.Size(390, 23);
            this.hostNameTextBox.TabIndex = 2;
            this.hostNameTextBox.TextChanged += new System.EventHandler(this.hostNameTextBox_TextChanged);
            // 
            // portTextBox
            // 
            this.portTextBox.Enabled = false;
            this.portTextBox.Location = new System.Drawing.Point(122, 64);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(139, 23);
            this.portTextBox.TabIndex = 3;
            this.portTextBox.TextChanged += new System.EventHandler(this.portTextBox_TextChanged);
            // 
            // endPointURITextBox
            // 
            this.endPointURITextBox.BackColor = System.Drawing.Color.White;
            this.endPointURITextBox.ForeColor = System.Drawing.Color.Black;
            this.endPointURITextBox.Location = new System.Drawing.Point(122, 118);
            this.endPointURITextBox.Name = "endPointURITextBox";
            this.endPointURITextBox.Size = new System.Drawing.Size(870, 23);
            this.endPointURITextBox.TabIndex = 4;
            this.endPointURITextBox.TextChanged += new System.EventHandler(this.endPointURITextBox_TextChanged);
            // 
            // hostNameLabel
            // 
            this.hostNameLabel.AutoSize = true;
            this.hostNameLabel.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.hostNameLabel.ForeColor = System.Drawing.Color.Red;
            this.hostNameLabel.Location = new System.Drawing.Point(518, 39);
            this.hostNameLabel.Name = "hostNameLabel";
            this.hostNameLabel.Size = new System.Drawing.Size(22, 17);
            this.hostNameLabel.TabIndex = 61;
            this.hostNameLabel.Text = "T";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.portLabel.ForeColor = System.Drawing.Color.Red;
            this.portLabel.Location = new System.Drawing.Point(267, 66);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(22, 17);
            this.portLabel.TabIndex = 62;
            this.portLabel.Text = "T";
            // 
            // endPointURILabel
            // 
            this.endPointURILabel.AutoSize = true;
            this.endPointURILabel.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.endPointURILabel.ForeColor = System.Drawing.Color.Red;
            this.endPointURILabel.Location = new System.Drawing.Point(998, 120);
            this.endPointURILabel.Name = "endPointURILabel";
            this.endPointURILabel.Size = new System.Drawing.Size(22, 17);
            this.endPointURILabel.TabIndex = 63;
            this.endPointURILabel.Text = "T";
            // 
            // doneButton
            // 
            this.doneButton.Location = new System.Drawing.Point(5, 464);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(110, 40);
            this.doneButton.TabIndex = 7;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Gray;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(5, 230);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.MaximumSize = new System.Drawing.Size(1008, 2);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(1008, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1008, 2);
            this.pictureBox1.TabIndex = 87;
            this.pictureBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(2, 243);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(162, 16);
            this.label7.TabIndex = 88;
            this.label7.Text = "Existing ESB Configuration:";
            // 
            // addServiceButton
            // 
            this.addServiceButton.Location = new System.Drawing.Point(5, 187);
            this.addServiceButton.Name = "addServiceButton";
            this.addServiceButton.Size = new System.Drawing.Size(110, 40);
            this.addServiceButton.TabIndex = 5;
            this.addServiceButton.Text = "Add Service";
            this.addServiceButton.UseVisualStyleBackColor = true;
            this.addServiceButton.Click += new System.EventHandler(this.addServiceButton_Click);
            // 
            // editServiceButton
            // 
            this.editServiceButton.Location = new System.Drawing.Point(6, 416);
            this.editServiceButton.Name = "editServiceButton";
            this.editServiceButton.Size = new System.Drawing.Size(110, 40);
            this.editServiceButton.TabIndex = 6;
            this.editServiceButton.Text = "Edit Service";
            this.editServiceButton.UseVisualStyleBackColor = true;
            this.editServiceButton.Click += new System.EventHandler(this.editServiceButton_Click);
            // 
            // existingEsbServicesListBox
            // 
            this.existingEsbServicesListBox.BackColor = System.Drawing.Color.White;
            this.existingEsbServicesListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.typeColumn,
            this.hostNameColumn,
            this.portColumn,
            this.domainColumn,
            this.endPointName,
            this.uriColumn});
            this.existingEsbServicesListBox.ForeColor = System.Drawing.Color.Black;
            this.existingEsbServicesListBox.FullRowSelect = true;
            this.existingEsbServicesListBox.GridLines = true;
            this.existingEsbServicesListBox.Location = new System.Drawing.Point(6, 262);
            this.existingEsbServicesListBox.MultiSelect = false;
            this.existingEsbServicesListBox.Name = "existingEsbServicesListBox";
            this.existingEsbServicesListBox.Size = new System.Drawing.Size(1007, 148);
            this.existingEsbServicesListBox.TabIndex = 0;
            this.existingEsbServicesListBox.TabStop = false;
            this.existingEsbServicesListBox.UseCompatibleStateImageBehavior = false;
            this.existingEsbServicesListBox.View = System.Windows.Forms.View.Details;
            this.existingEsbServicesListBox.SelectedIndexChanged += new System.EventHandler(this.existingEsbServicesListBox_SelectedIndexChanged);
            // 
            // typeColumn
            // 
            this.typeColumn.Text = "Type";
            this.typeColumn.Width = 120;
            // 
            // hostNameColumn
            // 
            this.hostNameColumn.Text = "Host";
            this.hostNameColumn.Width = 120;
            // 
            // portColumn
            // 
            this.portColumn.Text = "Port";
            // 
            // domainColumn
            // 
            this.domainColumn.Text = "Domain";
            // 
            // uriColumn
            // 
            this.uriColumn.Text = "URI";
            this.uriColumn.Width = 440;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Gray;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(5, 459);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.MaximumSize = new System.Drawing.Size(1008, 2);
            this.pictureBox2.MinimumSize = new System.Drawing.Size(1008, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1008, 2);
            this.pictureBox2.TabIndex = 86;
            this.pictureBox2.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(60, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 89;
            this.label5.Text = "Domain:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // domainTextBox
            // 
            this.domainTextBox.Enabled = false;
            this.domainTextBox.Location = new System.Drawing.Point(122, 91);
            this.domainTextBox.Name = "domainTextBox";
            this.domainTextBox.Size = new System.Drawing.Size(139, 23);
            this.domainTextBox.TabIndex = 90;
            this.domainTextBox.TextChanged += new System.EventHandler(this.domainTextBox_TextChanged);
            // 
            // domainLabel
            // 
            this.domainLabel.AutoSize = true;
            this.domainLabel.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.domainLabel.ForeColor = System.Drawing.Color.Red;
            this.domainLabel.Location = new System.Drawing.Point(267, 93);
            this.domainLabel.Name = "domainLabel";
            this.domainLabel.Size = new System.Drawing.Size(22, 17);
            this.domainLabel.TabIndex = 91;
            this.domainLabel.Text = "T";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 16);
            this.label6.TabIndex = 92;
            this.label6.Text = "End Point Name:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // endPointNameTextBox
            // 
            this.endPointNameTextBox.BackColor = System.Drawing.Color.White;
            this.endPointNameTextBox.ForeColor = System.Drawing.Color.Black;
            this.endPointNameTextBox.Location = new System.Drawing.Point(122, 149);
            this.endPointNameTextBox.Name = "endPointNameTextBox";
            this.endPointNameTextBox.Size = new System.Drawing.Size(390, 23);
            this.endPointNameTextBox.TabIndex = 93;
            this.endPointNameTextBox.TextChanged += new System.EventHandler(this.endPointNameTextBox_TextChanged);
            // 
            // endPointNameLabel
            // 
            this.endPointNameLabel.AutoSize = true;
            this.endPointNameLabel.Font = new System.Drawing.Font("Wingdings 2", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.endPointNameLabel.ForeColor = System.Drawing.Color.Red;
            this.endPointNameLabel.Location = new System.Drawing.Point(518, 152);
            this.endPointNameLabel.Name = "endPointNameLabel";
            this.endPointNameLabel.Size = new System.Drawing.Size(22, 17);
            this.endPointNameLabel.TabIndex = 94;
            this.endPointNameLabel.Text = "T";
            // 
            // endPointName
            // 
            this.endPointName.Text = "End Point Name";
            this.endPointName.Width = 200;
            // 
            // ESBSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1018, 516);
            this.ControlBox = false;
            this.Controls.Add(this.endPointNameLabel);
            this.Controls.Add(this.endPointNameTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.domainLabel);
            this.Controls.Add(this.domainTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.existingEsbServicesListBox);
            this.Controls.Add(this.editServiceButton);
            this.Controls.Add(this.addServiceButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.endPointURILabel);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.hostNameLabel);
            this.Controls.Add(this.endPointURITextBox);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.hostNameTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.esbServiceComboBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ESBSetupForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ESB Setup";
            this.Load += new System.EventHandler(this.ESBSetupForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox esbServiceComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox hostNameTextBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.TextBox endPointURITextBox;
        private System.Windows.Forms.Label hostNameLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label endPointURILabel;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button addServiceButton;
        private System.Windows.Forms.Button editServiceButton;
        private System.Windows.Forms.ListView existingEsbServicesListBox;
        private System.Windows.Forms.ColumnHeader typeColumn;
        private System.Windows.Forms.ColumnHeader hostNameColumn;
        private System.Windows.Forms.ColumnHeader portColumn;
        private System.Windows.Forms.ColumnHeader uriColumn;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ColumnHeader domainColumn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox domainTextBox;
        private System.Windows.Forms.Label domainLabel;
        private System.Windows.Forms.ColumnHeader endPointName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox endPointNameTextBox;
        private System.Windows.Forms.Label endPointNameLabel;
    }
}