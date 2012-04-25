using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.Customer
{
    partial class UpdateCustomerDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateCustomerDetails));
            this.labelUpdateCustDetailsHeading = new System.Windows.Forms.Label();
            this.labelHeadingInfo = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelMiddleInitial = new System.Windows.Forms.Label();
            this.labelTitleSuffix = new System.Windows.Forms.Label();
            this.labelSSN = new System.Windows.Forms.Label();
            this.title1 = new Title();
            this.custFirstName = new CustomTextBox();
            this.custMiddleInitial = new CustomTextBox();
            this.custLastName = new CustomTextBox();
            this.titleSuffix1 = new TitleSuffix();
            this.custDateOfBirth = new Date();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.customLabelLastName = new CustomLabel();
            this.customLabelNegotiationLanguage = new CustomLabel();
            this.labelDOB = new CustomLabel();
            this.customLabelFirstName = new CustomLabel();
            this.custSSN = new SocialSecurityNumber();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.customButtonCancel = new CustomButton();
            this.customButtonReset = new CustomButton();
            this.customButtonSubmit = new CustomButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelUpdateCustDetailsHeading
            // 
            this.labelUpdateCustDetailsHeading.AutoSize = true;
            this.labelUpdateCustDetailsHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUpdateCustDetailsHeading.ForeColor = System.Drawing.Color.White;
            this.labelUpdateCustDetailsHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelUpdateCustDetailsHeading.Location = new System.Drawing.Point(13, 14);
            this.labelUpdateCustDetailsHeading.Name = "labelUpdateCustDetailsHeading";
            this.labelUpdateCustDetailsHeading.Size = new System.Drawing.Size(168, 16);
            this.labelUpdateCustDetailsHeading.TabIndex = 0;
            this.labelUpdateCustDetailsHeading.Text = "Update Customer Details";
            // 
            // labelHeadingInfo
            // 
            this.labelHeadingInfo.AutoSize = true;
            this.labelHeadingInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeadingInfo.ForeColor = System.Drawing.Color.White;
            this.labelHeadingInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelHeadingInfo.Location = new System.Drawing.Point(52, 39);
            this.labelHeadingInfo.Name = "labelHeadingInfo";
            this.labelHeadingInfo.Size = new System.Drawing.Size(330, 13);
            this.labelHeadingInfo.TabIndex = 1;
            this.labelHeadingInfo.Text = "Update all necessary fields and click Submit button to save changes";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 155F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 393F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.labelTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelMiddleInitial, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelTitleSuffix, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelSSN, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.title1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.custFirstName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.custMiddleInitial, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.custLastName, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.titleSuffix1, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.custDateOfBirth, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxLanguage, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.customLabelLastName, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.customLabelNegotiationLanguage, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelDOB, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.customLabelFirstName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.custSSN, 1, 7);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(548, 216);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(3, 7);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(27, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Title";
            // 
            // labelMiddleInitial
            // 
            this.labelMiddleInitial.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelMiddleInitial.AutoSize = true;
            this.labelMiddleInitial.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMiddleInitial.Location = new System.Drawing.Point(3, 61);
            this.labelMiddleInitial.Name = "labelMiddleInitial";
            this.labelMiddleInitial.Size = new System.Drawing.Size(66, 13);
            this.labelMiddleInitial.TabIndex = 2;
            this.labelMiddleInitial.Text = "Middle Initial";
            // 
            // labelTitleSuffix
            // 
            this.labelTitleSuffix.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTitleSuffix.AutoSize = true;
            this.labelTitleSuffix.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitleSuffix.Location = new System.Drawing.Point(3, 117);
            this.labelTitleSuffix.Name = "labelTitleSuffix";
            this.labelTitleSuffix.Size = new System.Drawing.Size(58, 13);
            this.labelTitleSuffix.TabIndex = 4;
            this.labelTitleSuffix.Text = "Title Suffix";
            // 
            // labelSSN
            // 
            this.labelSSN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelSSN.AutoSize = true;
            this.labelSSN.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSSN.Location = new System.Drawing.Point(3, 199);
            this.labelSSN.Name = "labelSSN";
            this.labelSSN.Size = new System.Drawing.Size(116, 13);
            this.labelSSN.TabIndex = 7;
            this.labelSSN.Text = "Social Security Number";
            // 
            // title1
            // 
            this.title1.CausesValidation = false;
            this.title1.Location = new System.Drawing.Point(158, 3);
            this.title1.Name = "title1";
            this.title1.Size = new System.Drawing.Size(61, 21);
            this.title1.TabIndex = 1;
            // 
            // custFirstName
            // 
            this.custFirstName.CausesValidation = false;
            this.custFirstName.ErrorMessage = "";
            this.custFirstName.FirstLetterUppercase = true;
            this.custFirstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custFirstName.Location = new System.Drawing.Point(158, 30);
            this.custFirstName.MaxLength = 40;
            this.custFirstName.Name = "custFirstName";
            this.custFirstName.Required = true;
            this.custFirstName.Size = new System.Drawing.Size(222, 21);
            this.custFirstName.TabIndex = 2;
            this.custFirstName.ValidationExpression = "";
            // 
            // custMiddleInitial
            // 
            this.custMiddleInitial.CausesValidation = false;
            this.custMiddleInitial.ErrorMessage = "";
            this.custMiddleInitial.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custMiddleInitial.Location = new System.Drawing.Point(158, 57);
            this.custMiddleInitial.MaxLength = 40;
            this.custMiddleInitial.Name = "custMiddleInitial";
            this.custMiddleInitial.Size = new System.Drawing.Size(100, 21);
            this.custMiddleInitial.TabIndex = 3;
            this.custMiddleInitial.ValidationExpression = "";
            // 
            // custLastName
            // 
            this.custLastName.CausesValidation = false;
            this.custLastName.ErrorMessage = "";
            this.custLastName.FirstLetterUppercase = true;
            this.custLastName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custLastName.Location = new System.Drawing.Point(158, 84);
            this.custLastName.MaxLength = 40;
            this.custLastName.Name = "custLastName";
            this.custLastName.Required = true;
            this.custLastName.Size = new System.Drawing.Size(222, 21);
            this.custLastName.TabIndex = 4;
            this.custLastName.ValidationExpression = "";
            // 
            // titleSuffix1
            // 
            this.titleSuffix1.Location = new System.Drawing.Point(158, 111);
            this.titleSuffix1.Name = "titleSuffix1";
            this.titleSuffix1.Size = new System.Drawing.Size(55, 26);
            this.titleSuffix1.TabIndex = 5;
            // 
            // custDateOfBirth
            // 
            this.custDateOfBirth.CausesValidation = false;
            this.custDateOfBirth.ErrorMessage = "";
            this.custDateOfBirth.Location = new System.Drawing.Point(158, 170);
            this.custDateOfBirth.Name = "custDateOfBirth";
            this.custDateOfBirth.Required = true;
            this.custDateOfBirth.Size = new System.Drawing.Size(100, 20);
            this.custDateOfBirth.TabIndex = 7;
            this.custDateOfBirth.ValidationExpression = "^([1-9]|0[1-9]|1[012])[- /.]([1-9]|0[1-9]|[12][0-9]|3[01])[- /.](19|20)\\d\\d$";
            this.custDateOfBirth.Leave += new System.EventHandler(this.custDateOfBirth_Leave);
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Items.AddRange(new object[] {
            "Select",
            "Yes",
            "No"});
            this.comboBoxLanguage.Location = new System.Drawing.Point(158, 143);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(98, 21);
            this.comboBoxLanguage.TabIndex = 6;
            // 
            // customLabelLastName
            // 
            this.customLabelLastName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelLastName.AutoSize = true;
            this.customLabelLastName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelLastName.Location = new System.Drawing.Point(3, 88);
            this.customLabelLastName.Name = "customLabelLastName";
            this.customLabelLastName.Required = true;
            this.customLabelLastName.Size = new System.Drawing.Size(57, 13);
            this.customLabelLastName.TabIndex = 10;
            this.customLabelLastName.Text = "Last Name";
            // 
            // customLabelNegotiationLanguage
            // 
            this.customLabelNegotiationLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelNegotiationLanguage.AutoSize = true;
            this.customLabelNegotiationLanguage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelNegotiationLanguage.Location = new System.Drawing.Point(3, 147);
            this.customLabelNegotiationLanguage.Name = "customLabelNegotiationLanguage";
            this.customLabelNegotiationLanguage.Required = true;
            this.customLabelNegotiationLanguage.Size = new System.Drawing.Size(112, 13);
            this.customLabelNegotiationLanguage.TabIndex = 11;
            this.customLabelNegotiationLanguage.Text = "Negotiation Language";
            // 
            // labelDOB
            // 
            this.labelDOB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelDOB.AutoSize = true;
            this.labelDOB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDOB.Location = new System.Drawing.Point(3, 173);
            this.labelDOB.Name = "labelDOB";
            this.labelDOB.Required = true;
            this.labelDOB.Size = new System.Drawing.Size(68, 13);
            this.labelDOB.TabIndex = 12;
            this.labelDOB.Text = "Date of Birth";
            // 
            // customLabelFirstName
            // 
            this.customLabelFirstName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelFirstName.AutoSize = true;
            this.customLabelFirstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelFirstName.Location = new System.Drawing.Point(3, 34);
            this.customLabelFirstName.Name = "customLabelFirstName";
            this.customLabelFirstName.Required = true;
            this.customLabelFirstName.Size = new System.Drawing.Size(58, 13);
            this.customLabelFirstName.TabIndex = 9;
            this.customLabelFirstName.Text = "First Name";
            // 
            // custSSN
            // 
            this.custSSN.CausesValidation = false;
            this.custSSN.Location = new System.Drawing.Point(158, 196);
            this.custSSN.Name = "custSSN";
            this.custSSN.Size = new System.Drawing.Size(123, 20);
            this.custSSN.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(7, 322);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(725, 1);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // customButtonCancel
            // 
            this.customButtonCancel.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonCancel.BackgroundImage")));
            this.customButtonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCancel.FlatAppearance.BorderSize = 0;
            this.customButtonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCancel.ForeColor = System.Drawing.Color.White;
            this.customButtonCancel.Location = new System.Drawing.Point(220, 338);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 12;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = true;
            this.customButtonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // customButtonReset
            // 
            this.customButtonReset.BackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonReset.BackgroundImage")));
            this.customButtonReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonReset.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonReset.FlatAppearance.BorderSize = 0;
            this.customButtonReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonReset.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonReset.ForeColor = System.Drawing.Color.White;
            this.customButtonReset.Location = new System.Drawing.Point(336, 339);
            this.customButtonReset.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonReset.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonReset.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonReset.Name = "customButtonReset";
            this.customButtonReset.Size = new System.Drawing.Size(100, 50);
            this.customButtonReset.TabIndex = 13;
            this.customButtonReset.Text = "&Reset";
            this.customButtonReset.UseVisualStyleBackColor = true;
            this.customButtonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // customButtonSubmit
            // 
            this.customButtonSubmit.BackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonSubmit.BackgroundImage")));
            this.customButtonSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonSubmit.FlatAppearance.BorderSize = 0;
            this.customButtonSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonSubmit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonSubmit.ForeColor = System.Drawing.Color.White;
            this.customButtonSubmit.Location = new System.Drawing.Point(455, 339);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 14;
            this.customButtonSubmit.Text = "&Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = true;
            this.customButtonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // UpdateCustomerDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_440_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(578, 401);
            this.ControlBox = false;
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonReset);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.labelHeadingInfo);
            this.Controls.Add(this.labelUpdateCustDetailsHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateCustomerDetails";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateCustomerDetails";
            this.Load += new System.EventHandler(this.UpdateCustomerDetails_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdateCustomerDetails_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUpdateCustDetailsHeading;
        private System.Windows.Forms.Label labelHeadingInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelMiddleInitial;
        private System.Windows.Forms.Label labelTitleSuffix;
        private System.Windows.Forms.Label labelSSN;
        private Title title1;
        private CustomTextBox custFirstName;
        private CustomTextBox custMiddleInitial;
        private CustomTextBox custLastName;
        private TitleSuffix titleSuffix1;
        private Date custDateOfBirth;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private CustomLabel customLabelFirstName;
        private CustomLabel customLabelLastName;
        private CustomLabel customLabelNegotiationLanguage;
        private CustomLabel labelDOB;
        private SocialSecurityNumber custSSN;
        private CustomButton customButtonCancel;
        private CustomButton customButtonReset;
        private CustomButton customButtonSubmit;
    }
}