namespace Support.Forms.Customer
{
    partial class FrmLookupCustomer
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
            this.lblHeading = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.rbSSN = new System.Windows.Forms.RadioButton();
            this.rbCustInfo = new System.Windows.Forms.RadioButton();
            this.rbIDSearch = new System.Windows.Forms.RadioButton();
            this.rbCustNum = new System.Windows.Forms.RadioButton();
            this.rbLoan = new System.Windows.Forms.RadioButton();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSSN1 = new System.Windows.Forms.TextBox();
            this.gbLine1 = new System.Windows.Forms.GroupBox();
            this.txtSSN2 = new System.Windows.Forms.TextBox();
            this.txtSSN3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbLine2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSSN4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAreaCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPhonePre = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPhoneSuf = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLastname = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtDOB = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtBankAcct = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbIDType = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtIDNumber = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cbIssuer = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCustNum = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cbProductType = new System.Windows.Forms.ComboBox();
            this.txtShopName = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtLoanNbr = new System.Windows.Forms.TextBox();
            this.cbState = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.BackColor = System.Drawing.Color.Transparent;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(295, 30);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(155, 23);
            this.lblHeading.TabIndex = 4;
            this.lblHeading.Text = "Lookup Customer";
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
            this.cancelButton.Location = new System.Drawing.Point(17, 645);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(91, 32);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Close";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // btnFind
            // 
            this.btnFind.BackColor = System.Drawing.Color.Transparent;
            this.btnFind.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.btnFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFind.FlatAppearance.BorderSize = 0;
            this.btnFind.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnFind.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFind.ForeColor = System.Drawing.Color.White;
            this.btnFind.Location = new System.Drawing.Point(695, 645);
            this.btnFind.Margin = new System.Windows.Forms.Padding(0);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(91, 32);
            this.btnFind.TabIndex = 6;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = false;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(587, 645);
            this.btnClear.Margin = new System.Windows.Forms.Padding(0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(91, 32);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // rbSSN
            // 
            this.rbSSN.AutoSize = true;
            this.rbSSN.BackColor = System.Drawing.Color.Transparent;
            this.rbSSN.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSSN.Location = new System.Drawing.Point(25, 119);
            this.rbSSN.Name = "rbSSN";
            this.rbSSN.Size = new System.Drawing.Size(174, 20);
            this.rbSSN.TabIndex = 8;
            this.rbSSN.TabStop = true;
            this.rbSSN.Text = "Social Security Number";
            this.rbSSN.UseVisualStyleBackColor = false;
            this.rbSSN.CheckedChanged += new System.EventHandler(this.rbSSN_CheckedChanged);
            // 
            // rbCustInfo
            // 
            this.rbCustInfo.AutoSize = true;
            this.rbCustInfo.BackColor = System.Drawing.Color.Transparent;
            this.rbCustInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCustInfo.Location = new System.Drawing.Point(25, 188);
            this.rbCustInfo.Name = "rbCustInfo";
            this.rbCustInfo.Size = new System.Drawing.Size(168, 20);
            this.rbCustInfo.TabIndex = 9;
            this.rbCustInfo.TabStop = true;
            this.rbCustInfo.Text = "Customer Information";
            this.rbCustInfo.UseVisualStyleBackColor = false;
            this.rbCustInfo.CheckedChanged += new System.EventHandler(this.rbCustInfo_CheckedChanged);
            // 
            // rbIDSearch
            // 
            this.rbIDSearch.AutoSize = true;
            this.rbIDSearch.BackColor = System.Drawing.Color.Transparent;
            this.rbIDSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbIDSearch.Location = new System.Drawing.Point(25, 411);
            this.rbIDSearch.Name = "rbIDSearch";
            this.rbIDSearch.Size = new System.Drawing.Size(89, 20);
            this.rbIDSearch.TabIndex = 10;
            this.rbIDSearch.TabStop = true;
            this.rbIDSearch.Text = "ID Search";
            this.rbIDSearch.UseVisualStyleBackColor = false;
            this.rbIDSearch.CheckedChanged += new System.EventHandler(this.rbIDSearch_CheckedChanged);
            // 
            // rbCustNum
            // 
            this.rbCustNum.AutoSize = true;
            this.rbCustNum.BackColor = System.Drawing.Color.Transparent;
            this.rbCustNum.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCustNum.Location = new System.Drawing.Point(25, 484);
            this.rbCustNum.Name = "rbCustNum";
            this.rbCustNum.Size = new System.Drawing.Size(141, 20);
            this.rbCustNum.TabIndex = 11;
            this.rbCustNum.TabStop = true;
            this.rbCustNum.Text = "Customer Number";
            this.rbCustNum.UseVisualStyleBackColor = false;
            this.rbCustNum.CheckedChanged += new System.EventHandler(this.rbCustNum_CheckedChanged);
            // 
            // rbLoan
            // 
            this.rbLoan.AutoSize = true;
            this.rbLoan.BackColor = System.Drawing.Color.Transparent;
            this.rbLoan.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLoan.Location = new System.Drawing.Point(25, 547);
            this.rbLoan.Name = "rbLoan";
            this.rbLoan.Size = new System.Drawing.Size(124, 20);
            this.rbLoan.TabIndex = 12;
            this.rbLoan.TabStop = true;
            this.rbLoan.Text = "Loan / Ticket #";
            this.rbLoan.UseVisualStyleBackColor = false;
            this.rbLoan.CheckedChanged += new System.EventHandler(this.rbLoan_CheckedChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(22, 85);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSearch.Size = new System.Drawing.Size(261, 13);
            this.lblSearch.TabIndex = 18;
            this.lblSearch.Text = "Select the search type and enter your search criteria";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSSN1
            // 
            this.txtSSN1.Location = new System.Drawing.Point(43, 155);
            this.txtSSN1.MaxLength = 3;
            this.txtSSN1.Name = "txtSSN1";
            this.txtSSN1.Size = new System.Drawing.Size(36, 21);
            this.txtSSN1.TabIndex = 19;
            this.txtSSN1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gbLine1
            // 
            this.gbLine1.BackColor = System.Drawing.Color.DarkRed;
            this.gbLine1.Enabled = false;
            this.gbLine1.Location = new System.Drawing.Point(25, 142);
            this.gbLine1.Name = "gbLine1";
            this.gbLine1.Size = new System.Drawing.Size(720, 2);
            this.gbLine1.TabIndex = 24;
            this.gbLine1.TabStop = false;
            // 
            // txtSSN2
            // 
            this.txtSSN2.Location = new System.Drawing.Point(85, 155);
            this.txtSSN2.MaxLength = 2;
            this.txtSSN2.Name = "txtSSN2";
            this.txtSSN2.Size = new System.Drawing.Size(30, 21);
            this.txtSSN2.TabIndex = 20;
            this.txtSSN2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSSN3
            // 
            this.txtSSN3.Location = new System.Drawing.Point(122, 155);
            this.txtSSN3.MaxLength = 4;
            this.txtSSN3.Name = "txtSSN3";
            this.txtSSN3.Size = new System.Drawing.Size(40, 21);
            this.txtSSN3.TabIndex = 21;
            this.txtSSN3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(76, 158);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(14, 16);
            this.label1.TabIndex = 25;
            this.label1.Text = "-";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(113, 158);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(14, 16);
            this.label2.TabIndex = 26;
            this.label2.Text = "-";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // gbLine2
            // 
            this.gbLine2.BackColor = System.Drawing.Color.DarkRed;
            this.gbLine2.Enabled = false;
            this.gbLine2.ForeColor = System.Drawing.Color.DarkRed;
            this.gbLine2.Location = new System.Drawing.Point(25, 211);
            this.gbLine2.Name = "gbLine2";
            this.gbLine2.Size = new System.Drawing.Size(720, 2);
            this.gbLine2.TabIndex = 27;
            this.gbLine2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(240, 192);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(294, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Provide at least two of the following search criteria";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSSN4
            // 
            this.txtSSN4.Enabled = false;
            this.txtSSN4.Location = new System.Drawing.Point(191, 225);
            this.txtSSN4.MaxLength = 4;
            this.txtSSN4.Name = "txtSSN4";
            this.txtSSN4.Size = new System.Drawing.Size(40, 21);
            this.txtSSN4.TabIndex = 29;
            this.txtSSN4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(40, 222);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(116, 26);
            this.label4.TabIndex = 30;
            this.label4.Text = "Social Security Number\r\n(Last 4 of SSN)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(40, 269);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(126, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Customer Phone Number";
            // 
            // txtAreaCode
            // 
            this.txtAreaCode.Enabled = false;
            this.txtAreaCode.Location = new System.Drawing.Point(191, 266);
            this.txtAreaCode.MaxLength = 3;
            this.txtAreaCode.Name = "txtAreaCode";
            this.txtAreaCode.Size = new System.Drawing.Size(36, 21);
            this.txtAreaCode.TabIndex = 32;
            this.txtAreaCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(180, 269);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(11, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "(";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(230, 269);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(11, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = ")";
            // 
            // txtPhonePre
            // 
            this.txtPhonePre.Enabled = false;
            this.txtPhonePre.Location = new System.Drawing.Point(247, 266);
            this.txtPhonePre.MaxLength = 3;
            this.txtPhonePre.Name = "txtPhonePre";
            this.txtPhonePre.Size = new System.Drawing.Size(36, 21);
            this.txtPhonePre.TabIndex = 35;
            this.txtPhonePre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(283, 271);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(14, 16);
            this.label8.TabIndex = 36;
            this.label8.Text = "-";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPhoneSuf
            // 
            this.txtPhoneSuf.Enabled = false;
            this.txtPhoneSuf.Location = new System.Drawing.Point(296, 266);
            this.txtPhoneSuf.MaxLength = 4;
            this.txtPhoneSuf.Name = "txtPhoneSuf";
            this.txtPhoneSuf.Size = new System.Drawing.Size(40, 21);
            this.txtPhoneSuf.TabIndex = 37;
            this.txtPhoneSuf.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(40, 309);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 38;
            this.label9.Text = "Last Name";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(40, 343);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 39;
            this.label10.Text = "First Name";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(40, 377);
            this.label11.Name = "label11";
            this.label11.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label11.Size = new System.Drawing.Size(68, 13);
            this.label11.TabIndex = 40;
            this.label11.Text = "Date of Birth";
            // 
            // txtLastname
            // 
            this.txtLastname.Enabled = false;
            this.txtLastname.Location = new System.Drawing.Point(191, 306);
            this.txtLastname.MaxLength = 40;
            this.txtLastname.Name = "txtLastname";
            this.txtLastname.Size = new System.Drawing.Size(218, 21);
            this.txtLastname.TabIndex = 41;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Enabled = false;
            this.txtFirstName.Location = new System.Drawing.Point(191, 340);
            this.txtFirstName.MaxLength = 40;
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(218, 21);
            this.txtFirstName.TabIndex = 42;
            // 
            // txtDOB
            // 
            this.txtDOB.Enabled = false;
            this.txtDOB.Location = new System.Drawing.Point(191, 374);
            this.txtDOB.MaxLength = 40;
            this.txtDOB.Name = "txtDOB";
            this.txtDOB.Size = new System.Drawing.Size(89, 21);
            this.txtDOB.TabIndex = 43;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(504, 314);
            this.label12.Name = "label12";
            this.label12.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label12.Size = new System.Drawing.Size(26, 13);
            this.label12.TabIndex = 44;
            this.label12.Text = "City";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(497, 343);
            this.label13.Name = "label13";
            this.label13.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label13.Size = new System.Drawing.Size(33, 13);
            this.label13.TabIndex = 45;
            this.label13.Text = "State";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Location = new System.Drawing.Point(418, 377);
            this.label14.Name = "label14";
            this.label14.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label14.Size = new System.Drawing.Size(112, 13);
            this.label14.TabIndex = 46;
            this.label14.Text = "Bank Account Number";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtCity
            // 
            this.txtCity.Enabled = false;
            this.txtCity.Location = new System.Drawing.Point(546, 311);
            this.txtCity.MaxLength = 40;
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(218, 21);
            this.txtCity.TabIndex = 47;
            // 
            // txtBankAcct
            // 
            this.txtBankAcct.Enabled = false;
            this.txtBankAcct.Location = new System.Drawing.Point(546, 374);
            this.txtBankAcct.MaxLength = 40;
            this.txtBankAcct.Name = "txtBankAcct";
            this.txtBankAcct.Size = new System.Drawing.Size(218, 21);
            this.txtBankAcct.TabIndex = 48;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkRed;
            this.groupBox1.Enabled = false;
            this.groupBox1.ForeColor = System.Drawing.Color.DarkRed;
            this.groupBox1.Location = new System.Drawing.Point(25, 438);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(720, 2);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            // 
            // cbIDType
            // 
            this.cbIDType.BackColor = System.Drawing.Color.White;
            this.cbIDType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIDType.Enabled = false;
            this.cbIDType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIDType.ForeColor = System.Drawing.Color.Black;
            this.cbIDType.FormattingEnabled = true;
            this.cbIDType.Location = new System.Drawing.Point(95, 446);
            this.cbIDType.Name = "cbIDType";
            this.cbIDType.Size = new System.Drawing.Size(188, 24);
            this.cbIDType.TabIndex = 50;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Location = new System.Drawing.Point(40, 451);
            this.label15.Name = "label15";
            this.label15.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label15.Size = new System.Drawing.Size(31, 13);
            this.label15.TabIndex = 51;
            this.label15.Text = "Type";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtIDNumber
            // 
            this.txtIDNumber.Enabled = false;
            this.txtIDNumber.Location = new System.Drawing.Point(366, 448);
            this.txtIDNumber.MaxLength = 40;
            this.txtIDNumber.Name = "txtIDNumber";
            this.txtIDNumber.Size = new System.Drawing.Size(164, 21);
            this.txtIDNumber.TabIndex = 52;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Location = new System.Drawing.Point(316, 451);
            this.label16.Name = "label16";
            this.label16.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label16.Size = new System.Drawing.Size(44, 13);
            this.label16.TabIndex = 53;
            this.label16.Text = "Number";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Location = new System.Drawing.Point(567, 451);
            this.label17.Name = "label17";
            this.label17.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label17.Size = new System.Drawing.Size(37, 13);
            this.label17.TabIndex = 54;
            this.label17.Text = "Issuer";
            this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbIssuer
            // 
            this.cbIssuer.BackColor = System.Drawing.Color.White;
            this.cbIssuer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIssuer.Enabled = false;
            this.cbIssuer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIssuer.ForeColor = System.Drawing.Color.Black;
            this.cbIssuer.FormattingEnabled = true;
            this.cbIssuer.Location = new System.Drawing.Point(609, 446);
            this.cbIssuer.Name = "cbIssuer";
            this.cbIssuer.Size = new System.Drawing.Size(53, 24);
            this.cbIssuer.TabIndex = 55;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Location = new System.Drawing.Point(40, 519);
            this.label18.Name = "label18";
            this.label18.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label18.Size = new System.Drawing.Size(93, 13);
            this.label18.TabIndex = 56;
            this.label18.Text = "Customer Number";
            this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.DarkRed;
            this.groupBox3.Enabled = false;
            this.groupBox3.ForeColor = System.Drawing.Color.DarkRed;
            this.groupBox3.Location = new System.Drawing.Point(25, 507);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(720, 2);
            this.groupBox3.TabIndex = 57;
            this.groupBox3.TabStop = false;
            // 
            // txtCustNum
            // 
            this.txtCustNum.Enabled = false;
            this.txtCustNum.Location = new System.Drawing.Point(155, 516);
            this.txtCustNum.MaxLength = 40;
            this.txtCustNum.Name = "txtCustNum";
            this.txtCustNum.Size = new System.Drawing.Size(218, 21);
            this.txtCustNum.TabIndex = 58;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.DarkRed;
            this.groupBox4.Enabled = false;
            this.groupBox4.ForeColor = System.Drawing.Color.DarkRed;
            this.groupBox4.Location = new System.Drawing.Point(25, 571);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(720, 2);
            this.groupBox4.TabIndex = 59;
            this.groupBox4.TabStop = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Location = new System.Drawing.Point(40, 588);
            this.label19.Name = "label19";
            this.label19.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label19.Size = new System.Drawing.Size(71, 13);
            this.label19.TabIndex = 60;
            this.label19.Text = "Product Type";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbProductType
            // 
            this.cbProductType.BackColor = System.Drawing.Color.White;
            this.cbProductType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProductType.Enabled = false;
            this.cbProductType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProductType.ForeColor = System.Drawing.Color.Black;
            this.cbProductType.FormattingEnabled = true;
            this.cbProductType.Location = new System.Drawing.Point(122, 583);
            this.cbProductType.Name = "cbProductType";
            this.cbProductType.Size = new System.Drawing.Size(188, 24);
            this.cbProductType.TabIndex = 61;
            // 
            // txtShopName
            // 
            this.txtShopName.Enabled = false;
            this.txtShopName.Location = new System.Drawing.Point(407, 585);
            this.txtShopName.MaxLength = 20;
            this.txtShopName.Name = "txtShopName";
            this.txtShopName.Size = new System.Drawing.Size(83, 21);
            this.txtShopName.TabIndex = 62;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Location = new System.Drawing.Point(339, 588);
            this.label20.Name = "label20";
            this.label20.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label20.Size = new System.Drawing.Size(61, 13);
            this.label20.TabIndex = 63;
            this.label20.Text = "Shop Name";
            this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Location = new System.Drawing.Point(520, 588);
            this.label21.Name = "label21";
            this.label21.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label21.Size = new System.Drawing.Size(70, 13);
            this.label21.TabIndex = 65;
            this.label21.Text = "Loan Number";
            this.label21.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtLoanNbr
            // 
            this.txtLoanNbr.Enabled = false;
            this.txtLoanNbr.Location = new System.Drawing.Point(595, 585);
            this.txtLoanNbr.MaxLength = 20;
            this.txtLoanNbr.Name = "txtLoanNbr";
            this.txtLoanNbr.Size = new System.Drawing.Size(164, 21);
            this.txtLoanNbr.TabIndex = 64;
            // 
            // cbState
            // 
            this.cbState.BackColor = System.Drawing.Color.White;
            this.cbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbState.Enabled = false;
            this.cbState.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbState.ForeColor = System.Drawing.Color.Black;
            this.cbState.FormattingEnabled = true;
            this.cbState.Location = new System.Drawing.Point(546, 338);
            this.cbState.Name = "cbState";
            this.cbState.Size = new System.Drawing.Size(53, 24);
            this.cbState.TabIndex = 66;
            // 
            // FrmLookupCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Support.Properties.Resources.form_800_700;
            this.ClientSize = new System.Drawing.Size(800, 700);
            this.Controls.Add(this.cbState);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtLoanNbr);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txtShopName);
            this.Controls.Add(this.cbProductType);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtCustNum);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.cbIssuer);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtIDNumber);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cbIDType);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtBankAcct);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtDOB);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.txtLastname);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtPhoneSuf);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPhonePre);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAreaCode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSSN4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gbLine2);
            this.Controls.Add(this.txtSSN2);
            this.Controls.Add(this.txtSSN3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSSN1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbLine1);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.rbLoan);
            this.Controls.Add(this.rbCustNum);
            this.Controls.Add(this.rbIDSearch);
            this.Controls.Add(this.rbCustInfo);
            this.Controls.Add(this.rbSSN);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.lblHeading);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(800, 700);
            this.MinimumSize = new System.Drawing.Size(800, 700);
            this.Name = "FrmLookupCustomer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLookupCustomer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RadioButton rbSSN;
        private System.Windows.Forms.RadioButton rbCustInfo;
        private System.Windows.Forms.RadioButton rbIDSearch;
        private System.Windows.Forms.RadioButton rbCustNum;
        private System.Windows.Forms.RadioButton rbLoan;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSSN1;
        private System.Windows.Forms.GroupBox gbLine1;
        private System.Windows.Forms.TextBox txtSSN2;
        private System.Windows.Forms.TextBox txtSSN3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbLine2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSSN4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAreaCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPhonePre;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPhoneSuf;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtLastname;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtDOB;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtBankAcct;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbIDType;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtIDNumber;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cbIssuer;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtCustNum;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cbProductType;
        private System.Windows.Forms.TextBox txtShopName;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtLoanNbr;
        private System.Windows.Forms.ComboBox cbState;
    }
}